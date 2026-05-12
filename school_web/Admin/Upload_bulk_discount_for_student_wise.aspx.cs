using Microsoft.VisualBasic.FileIO;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Upload_bulk_discount_for_student_wise : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                    ddlsession.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_fnd_fee_group, "select Group_name,group_id from dbo.[Fee_group_master] where group_id!='4'");
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    mycode.bind_all_ddl_with_id(ddl_month_name, "Select Month,Position from Month_Index order by Position");

                }
            }
        }
        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }
        protected void btn_getcsvexcel_Click(object sender, EventArgs e)
        {


            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
                ddlsession.Focus();
                return;
            }
            //else if (ddl_class.SelectedItem.Text == "Select")
            //{
            //    Alertme("Please select class name", "warning");
            //    ddlsession.Focus();
            //    return;
            //}
            else if (ddl_fnd_fee_group.SelectedItem.Text == "Select")
            {
                Alertme("Please select discount for", "warning");
                ddlsession.Focus();
                return;
            }
            else
            {
                try
                {
                    btn_get_csv_excel();
                }
                catch
                {

                }

            }
        }

        private void btn_get_csv_excel()
        {
            string classid = "";
            if (ddl_class.SelectedItem.Text == "Select")
            {
                classid = "0";
            }
            else
            {
                classid = "0";

            }
            string query = mycode.get_fee_heading_admission_annual_month_bus(classid, ddlsession.SelectedValue, ViewState["branchid"].ToString(), ddl_fnd_fee_group.SelectedItem.Text, ddl_schooltype.Text);
            Final_bind_grid_data(query);

        }

        private void Final_bind_grid_data(string query)
        {
            ViewState["query"] = query;
            DataTable dt1 = mycode.FillData(query);
            if (dt1.Rows.Count == 0)
            {
                panlfileupload.Visible = false;
                Alertme("Sorry there are no student data list exist", "warning");

            }
            else
            {



                panlfileupload.Visible = true;


                using (SqlConnection con = new SqlConnection(My.conn))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);

                                //Build the CSV file data as a Comma separated string.
                                string csv = string.Empty;

                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Header row for CSV file.
                                    csv += column.ColumnName + ',';
                                }

                                //Add new line.
                                csv += "\r\n";

                                foreach (DataRow row in dt.Rows)
                                {
                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        //Add the Data rows.
                                        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                    }

                                    //Add new line.
                                    csv += "\r\n";
                                }

                                //Download the CSV file.
                                Response.Clear();
                                Response.Buffer = true;
                                Response.AddHeader("content-disposition", "attachment;filename=" + ddl_fnd_fee_group.SelectedItem.Text + "_Discount" + ".csv");
                                Response.Charset = "";
                                Response.ContentType = "application/text";
                                Response.Output.Write(csv);
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                }







            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                    return;
                }
                else if (ddl_fnd_fee_group.SelectedItem.Text == "Select")
                {
                    Alertme("Please select discount for", "warning");
                    ddlsession.Focus();
                    return;
                }
                else
                {
                    if (FileUpload1.HasFile)
                    {
                        btn_final_submit.Visible = true;
                        ViewState["dupAdmiD"] = "0";
                        upload_excel_file();
                    }
                    else
                    {
                        Alertme("Please choose excel.csv file.", "warning");
                        return;
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void upload_excel_file()
        {
            lbl_type.Text = ddl_fnd_fee_group.SelectedItem.Text;
            string file = upload_csv_data();
            string csvid = My.auto_serialS("Upload_csvid");
            SqlDataAdapter ad = new SqlDataAdapter("Select * from excel_file", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Csv_file");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = file;
            dr[2] = mycode.date();
            dr[3] = mycode.idate();
            dr[4] = csvid;
            dr[5] = "SUBMITTED";
            dr[6] = ddl_fnd_fee_group.SelectedItem.Text + "_Discount";
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private string upload_csv_data()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("ddMMyyyy");
            string time = dtm.ToString("hhmmss");
            String filerename = date + time;
            string dbfilePath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            Session["file1"] = FileUpload1.FileName;
            String FileExtension = Path.GetExtension(Session["file1"].ToString()).ToLower();
            Session["file"] = (ddl_fnd_fee_group.SelectedItem.Text + "_Discount" + filerename + FileExtension);
            String[] allowedExtensions = { ".csv" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtensions[i])
                {

                    FileOK = true;
                    break;
                }
                else
                {
                }
            }

            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/doc")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["file"]);

                    if (check_wrap_or_not((path + "/" + Session["file"])))
                    {
                        FileSaved = true;
                    }
                    else
                    {
                        File.Delete((path + "/" + Session["file"]));
                        FileSaved = false;
                    }
                }
                catch (Exception ex)
                {
                    FileSaved = false;

                    Alertme(ex.ToString(), "warning");
                }
            }
            else
            {
                dbfilePath = "Choose only csv File";
                return dbfilePath;
            }
            if (FileSaved)
            {

                string fileName = Path.GetFileName(Session["file"].ToString());
                dbfilePath = @"/Master_Img/doc/" + fileName;
                Session["fileName"] = fileName;
            }
            return dbfilePath;
        }
        private bool check_wrap_or_not(string path)
        {
            try
            {
                ViewState["header"] = "";
                string query = "";
                DataTable tblReadCSV = new DataTable();

                string header = "";
                string classid = "0";

                query = mycode.get_fee_heading_admission_annual_month_top1_blanck(classid, ddlsession.SelectedValue, ViewState["branchid"].ToString(), ddl_fnd_fee_group.SelectedItem.Text, ddl_schooltype.Text);
                SqlCommand cmd = new SqlCommand(query);
                DataTable dt = mycode.GetData(cmd);
                if (dt.Rows.Count == 0)
                {

                }
                else
                {
                    int Columnscount = dt.Columns.Count;
                    int j = 0;
                    for (int i = 0; i < Columnscount; i++)
                    {
                        tblReadCSV.Columns.Add(dt.Rows[0][j].ToString());

                        if (ViewState["header"].ToString() == "")
                        {
                            ViewState["header"] = dt.Rows[0][j].ToString();
                        }
                        else
                        {
                            ViewState["header"] = ViewState["header"].ToString() + "," + dt.Rows[0][j].ToString();
                        }
                        j++;
                    }
                }

                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }
                pnl_grid.Visible = true;
                lbl_total1.Text = "Total Data :- " + tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();
                //==============

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                    return;
                }
                //else if (ddl_class.SelectedItem.Text == "Select")
                //{
                //    Alertme("Please select class name", "warning");
                //    ddlsession.Focus();
                //    return;
                //}
                else if (ddl_fnd_fee_group.SelectedItem.Text == "Select")
                {
                    Alertme("Please select discount for", "warning");
                    ddlsession.Focus();
                    return;
                }


                else
                {
                    if (ddl_fnd_fee_group.SelectedItem.Text == "Monthly")
                    {
                        if (ddl_month_name.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select month name", "warning");
                            ddlsession.Focus();
                            return;
                        }
                        else
                        {


                        }
                    }
                    else
                    {

                    }

                    bool finlsubmit = false; ;
                    string date1 = mycode.date();
                    string time1 = mycode.time();
                    string month = "NA";
                    if (ddl_fnd_fee_group.SelectedItem.Text == "Monthly")
                    {
                        month = ddl_month_name.SelectedItem.Text;
                    }

                    string parameter_id = My.get_parameterid(ddl_schooltype.Text, ddl_fnd_fee_group.SelectedItem.Text);
                    string parameter_name = My.get_parametername(ddl_schooltype.Text, ddl_fnd_fee_group.SelectedItem.Text);

                    string confirmValue = string.Empty;
                    confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {

                        string admissionserialnumber = "";
                        string rollnumber = "";
                        string session = "";
                        string Section = "";
                        string Class_id = "";
                        string classname = "";
                        string coursefee = "0";
                        string Category_id = "";
                        string SubCategory_id = "";
                        string qry = "";
                        string group_transaction = "group_transaction" + My.create_random_no_otp();
                        string headname = "";
                        DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        string[] data = (ViewState["header"].ToString()).Split(',');
                        for (int i = 0; i < grvExcelData.Rows.Count; i++)
                        {
                            qry = "";
                            string a1 = grvExcelData.Rows[i].Cells[1].Text;
                            int Columnscount = grvExcelData.Rows[0].Cells.Count;
                            Dictionary<string, object> dc1 = My.get_selected_studentinfo(a1, ddlsession.SelectedValue, ViewState["branchid"].ToString());
                            admissionserialnumber = (String)dc1["admissionserialnumber"];
                            rollnumber = (String)dc1["rollnumber"];
                            session = (String)dc1["session"];
                            Section = (String)dc1["Section"];
                            Class_id = (String)dc1["Class_id"];
                            classname = (String)dc1["classname"];
                            Category_id = (String)dc1["Category_id"];
                            SubCategory_id = (String)dc1["SubCategory_id"];

                            for (int j = 3; j < Columnscount; j++)
                            {



                                headname = data[j].ToString();
                                string disc_fee = grvExcelData.Rows[i].Cells[j].Text;

                                string feeid_coursefee = My.get_feeid(headname, ddl_fnd_fee_group.SelectedItem.Text, ddlsession.SelectedValue, Class_id, ddl_schooltype.SelectedValue);

                                string[] stringSeparators = new string[] { "/" };
                                string[] arr = feeid_coursefee.Split(stringSeparators, StringSplitOptions.None);
                                string feeid = arr[0];
                                coursefee = arr[1];

                                if (feeid != "0")
                                {
                                    if (ddl_fnd_fee_group.SelectedItem.Text == "Monthly")
                                    {
                                        bool chek_fee = My.find_mnth_disc_fee_collected_of_student(parameter_name, Category_id, SubCategory_id, ddlsession.SelectedItem.Text, Class_id, feeid, parameter_id, month, admissionserialnumber);
                                        if (chek_fee == false)
                                        {
                                        }
                                        else
                                        {
                                            if (My.toDouble(disc_fee) <= My.toDouble(coursefee))
                                            {
                                                double Discount_per = My.Round((My.toDouble(disc_fee) * 100) / My.toDouble(coursefee), 2);
                                                if (Discount_per.ToString() == "NaN" || Discount_per.ToString() == "∞")
                                                {
                                                    Discount_per = 0;
                                                }
                                                if (Discount_per == 0)
                                                {

                                                }
                                                else
                                                {
                                                    qry = qry + "insert into Discount_Master(Class_id,Discount_on,session,Discount_per,group_id,admission_no,month,fee_head_id,disc_amount,parameter_id,session_id,Branch_id,User_id,Date,time,discount_for,category_id,sub_category_id,Upload_from) values ('" + Class_id + "','" + ddl_fnd_fee_group.SelectedItem.Text + "','" + ddlsession.SelectedItem.Text + "','" + Discount_per + "','" + ddl_fnd_fee_group.SelectedValue + "','" + admissionserialnumber + "','" + month + "','" + feeid + "','" + My.toDouble(disc_fee).ToString("0.00") + "','" + parameter_id + "','" + ddlsession.SelectedValue + "','" + ViewState["branchid"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + date1 + "','" + time1 + "','" + ddl_schooltype.Text + "','" + Category_id + "','" + SubCategory_id + "','" + group_transaction + "');";
                                                }
                                            }
                                            else
                                            {
                                                qry = qry + "insert into Discount_Master(Class_id,Discount_on,session,Discount_per,group_id,admission_no,month,fee_head_id,disc_amount,parameter_id,session_id,Branch_id,User_id,Date,time,discount_for,category_id,sub_category_id,Upload_from) values ('" + Class_id + "','" + ddl_fnd_fee_group.SelectedItem.Text + "','" + ddlsession.SelectedItem.Text + "','100','" + ddl_fnd_fee_group.SelectedValue + "','" + admissionserialnumber + "','" + month + "','" + feeid + "','" + My.toDouble(coursefee).ToString("0.00") + "','" + parameter_id + "','" + ddlsession.SelectedValue + "','" + ViewState["branchid"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + date1 + "','" + time1 + "','" + ddl_schooltype.Text + "','" + Category_id + "','" + SubCategory_id + "','" + group_transaction + "');";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        bool chek_fee = My.find_disc_fee_collected_stdntwise(parameter_name, Category_id, SubCategory_id, ddlsession.SelectedItem.Text, Class_id, feeid, parameter_id, admissionserialnumber);
                                        if (chek_fee == false)
                                        {
                                        }
                                        else
                                        {
                                            if (My.toDouble(disc_fee) <= My.toDouble(coursefee))
                                            {
                                                double Discount_per = My.Round((My.toDouble(disc_fee) * 100) / My.toDouble(coursefee), 2);
                                                if (Discount_per.ToString() == "NaN" || Discount_per.ToString() == "∞")
                                                {
                                                    Discount_per = 0;
                                                } 
                                                if (Discount_per == 0)
                                                {

                                                }
                                                else
                                                { 
                                                    qry = qry + "insert into Discount_Master(Class_id,Discount_on,session,Discount_per,group_id,admission_no,month,fee_head_id,disc_amount,parameter_id,session_id,Branch_id,User_id,Date,time,discount_for,category_id,sub_category_id,Upload_from) values ('" + Class_id + "','" + ddl_fnd_fee_group.SelectedItem.Text + "','" + ddlsession.SelectedItem.Text + "','" + Discount_per + "','" + ddl_fnd_fee_group.SelectedValue + "','" + admissionserialnumber + "','" + month + "','" + feeid + "','" + My.toDouble(disc_fee).ToString("0.00") + "','" + parameter_id + "','" + ddlsession.SelectedValue + "','" + ViewState["branchid"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + date1 + "','" + time1 + "','" + ddl_schooltype.Text + "','" + Category_id + "','" + SubCategory_id + "','" + group_transaction + "');";
                                                } 
                                            }
                                            else
                                            {
                                                qry = qry + "insert into Discount_Master(Class_id,Discount_on,session,Discount_per,group_id,admission_no,month,fee_head_id,disc_amount,parameter_id,session_id,Branch_id,User_id,Date,time,discount_for,category_id,sub_category_id,Upload_from) values ('" + Class_id + "','" + ddl_fnd_fee_group.SelectedItem.Text + "','" + ddlsession.SelectedItem.Text + "','100','" + ddl_fnd_fee_group.SelectedValue + "','" + admissionserialnumber + "','" + month + "','" + feeid + "','" + My.toDouble(coursefee).ToString("0.00") + "','" + parameter_id + "','" + ddlsession.SelectedValue + "','" + ViewState["branchid"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + date1 + "','" + time1 + "','" + ddl_schooltype.Text + "','" + Category_id + "','" + SubCategory_id + "','" + group_transaction + "');";
                                            }
                                        }
                                    }
                                }
                            }
                            if (qry != "")
                            {
                                My.exeSql(qry);
                                finlsubmit = true;
                            }
                        }
                        if (finlsubmit == true)
                        {
                            Alertme("Student discount has been uploaded successfully.", "success");
                            btn_final_submit.Visible = false;
                            pnl_grid.Visible = false;
                            grvExcelData.DataSource = null;
                            grvExcelData.DataBind();
                        }
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_fnd_fee_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_fnd_fee_group.SelectedItem.Text == "Select")
            {
                Alertme("Please select discount for", "warning");
                monthname.Visible = false;
            }
            else
            {
                if (ddl_fnd_fee_group.SelectedItem.Text == "Monthly")
                {
                    monthname.Visible = true;
                }
                else
                {
                    monthname.Visible = false;
                }
            }
        }
    }
}
