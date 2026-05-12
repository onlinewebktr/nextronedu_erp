using Microsoft.VisualBasic.FileIO;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class bulk_discount_update : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) ");
                        mycode.bind_all_ddl_with_id(ddl_excel_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) ");
                        ddlsession.SelectedValue = My.get_session_id();
                        ddl_excel_session.SelectedValue = ddlsession.SelectedValue;
                        mycode.bind_all_ddl_with_id_cap_All(ddl_excel_class, "select Course_Name,course_id from Add_course_table order by Position asc");

                        ViewState["RepeatFine"] = My.get_fine_repat_no();
                        ViewState["firm_id"] = My.get_firm_id();
                    }
                }
            }
            catch (Exception ex)
            {
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

        protected void lnk_download_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_excel_session.SelectedItem.Text == "Select")
                {
                    ddl_excel_session.Focus();
                    Alertme("Please select session.", "warning");
                }
                else
                {
                    bool flag = false; string ifstdZero = "0";
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        string qry = "";
                        if (ddl_excel_class.SelectedItem.Text == "ALL")
                        {
                            qry = "select Session_id,Class_id,admissionserialnumber from admission_registor where Session_id='" + ddl_excel_session.SelectedValue + "' and Status=1 and StudentStatus='AV' and Is_TC_Taken!='true'";
                        }
                        else
                        {
                            qry = "select Session_id,Class_id,admissionserialnumber from admission_registor where Session_id='" + ddl_excel_session.SelectedValue + "' and Class_id='" + ddl_excel_class.SelectedValue + "' and Status=1 and StudentStatus='AV' and Is_TC_Taken!='true'";
                        }
                        DataTable dt = payments.dataTable(qry, con);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                dues_update_headwise_transaction.update_student_dues(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString(), "0", "0", con);
                            }
                        }
                        else
                        {
                            ifstdZero = "1";
                        }
                        flag = true;
                        con.Close(); 
                        scope.Complete();
                    }
                    if (flag == true)
                    {
                        if (ifstdZero == "1")
                        {
                            Alertme("Studnt not found.", "warning");
                        }
                        else
                        {
                            string qry = "";
                            if (ddl_excel_class.SelectedItem.Text == "ALL")
                            {
                                qry = "select ar.admissionserialnumber as Admission_no,ar.studentname as Student_name,ar.class as Class_name,ar.Section,ar.rollnumber as Roll_no,(select sum(convert(float, amount)) from STUDENT_WISE_DUES_AMOUNT where admission_no=ar.admissionserialnumber and Session_id=ar.Session_id and Class_id=ar.Class_id) as Total_fee,'' as Total_Discount_Given_Full_Session from admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where Session_id='" + ddl_excel_session.SelectedValue + "' and Status=1 and StudentStatus='AV' and Is_TC_Taken!='true' order by ad.Position,ar.Section,ar.rollnumber asc";
                            }
                            else
                            {
                                qry = "select ar.admissionserialnumber as Admission_no,ar.studentname as Student_name,ar.class as Class_name,ar.Section,ar.rollnumber as Roll_no,(select sum(convert(float, amount)) from STUDENT_WISE_DUES_AMOUNT where admission_no=ar.admissionserialnumber and Session_id=ar.Session_id  and Class_id=ar.Class_id) as Total_fee,'' as Total_Discount_Given_Full_Session from admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where Session_id='" + ddl_excel_session.SelectedValue + "' and Class_id='" + ddl_excel_class.SelectedValue + "' and Status=1 and StudentStatus='AV' and Is_TC_Taken!='true' order by ad.Position,ar.Section,ar.rollnumber asc";
                            }
                            DataTable dt = My.dataTable(qry);
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    string file_name = My.with_excel_name("student-list-for-discount"); 
                                    string attachment = "attachment; filename=" + file_name + ".csv";
                                    Response.ClearContent();
                                    Response.AddHeader("content-disposition", attachment);
                                    Response.ContentType = "text/csv";
                                    var csvContent = My.DataTableToCsv(dt);
                                    Response.Write(csvContent);
                                    Response.End();
                                    Alertme("Student list with fee has been downloaded successfully.", "success");  
                                }
                            }
                            else
                            {
                                Alertme("Student not found.", "warning");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }




        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    ViewState["session_id"] = ddlsession.SelectedValue;
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
            catch (Exception ex)
            { }
        }

        private void upload_excel_file()
        {
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
            dr[6] = "MonthlyFee";
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
            Session["file"] = ("Upload_Monthly_Fee_Mapping" + filerename + FileExtension);
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
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
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
                dbfilePath = @"/Master_Img/Student/" + fileName;
                Session["fileName"] = fileName;
            }
            return dbfilePath;
        }

        private bool check_wrap_or_not(string path)
        {
            try
            {
                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("Admission_no");
                tblReadCSV.Columns.Add("Student_name");
                tblReadCSV.Columns.Add("Class_name");
                tblReadCSV.Columns.Add("Section");
                tblReadCSV.Columns.Add("Roll_no");
                tblReadCSV.Columns.Add("Total_fee");
                tblReadCSV.Columns.Add("Total_Discount_Given_Full_Session");

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
                string confirmValue = string.Empty;
                confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();

                        string Admission_No = "";
                        string Total_amount = "";
                        string Total_discount = "";

                        DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        for (int i = 0; i < grvExcelData.Rows.Count; i++)
                        {
                            Admission_No = grvExcelData.Rows[i].Cells[0].Text;
                            Total_amount = grvExcelData.Rows[i].Cells[5].Text;
                            Total_discount = grvExcelData.Rows[i].Cells[6].Text;

                            Dictionary<string, object> dc1 = payments.get_selected_studentinfo(Admission_No, ViewState["session_id"].ToString(), ViewState["branchid"].ToString(), con);
                            string admissionserialnumber = (String)dc1["admissionserialnumber"];
                            string rollnumber = (String)dc1["rollnumber"];
                            string session = (String)dc1["session"];
                            string Section = (String)dc1["Section"];
                            string Class_id = (String)dc1["Class_id"];
                            string classname = (String)dc1["classname"];
                            string lblhostel = (String)dc1["hosteltaken"];
                            string lbltransporttion = (String)dc1["transportationtaken"];
                            string Transfer_Status = (String)dc1["Transfer_Status"];
                            string category_id = (String)dc1["category_id"];
                            string sub_category_id = (String)dc1["sub_category_id"];
                            string parameter = (String)dc1["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";


                            string fee_group_id = "2";
                            if (Transfer_Status == "New")
                            {
                                fee_group_id = "1";
                            }

                            Dictionary<string, object> dc2 = payments.Bind_Transport_data_for_assined_student(ViewState["session_id"].ToString(), Class_id, admissionserialnumber, con);
                            ViewState["Transport_id"] = (String)dc2["Transport_id"];
                            ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                            ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                            ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                            ViewState["Month_name"] = (String)dc2["Month_name"];
                            ViewState["Month_id"] = (String)dc2["Month_id"];
                            ViewState["Year_month"] = (String)dc2["Year_month"];
                            ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];


                            //==========
                            Dictionary<string, object> dc3 = payments.Bind_hostel_data_for_assined_student(ViewState["session_id"].ToString(), Class_id, admissionserialnumber, con);
                            ViewState["Hostel_id"] = (String)dc3["Hostel_id"];
                            ViewState["Room_Category_id"] = (String)dc3["Room_Category_id"];
                            ViewState["From_month_name"] = (String)dc3["From_month_name"];
                            ViewState["From_month_id"] = (String)dc3["From_month_id"];
                            ViewState["Assined_Year_Month"] = (String)dc3["Assined_Year_Month"];
                            ViewState["Hostel_assign_id"] = (String)dc3["Hostel_assign_id"];
                            if (ViewState["Hostel_id"].ToString() == "0")
                            {
                                ViewState["parameterDisc"] = "4";
                            }
                            for_discount(ddlsession.SelectedItem.Text, ddlsession.SelectedValue, Class_id, admissionserialnumber, lblhostel, lbltransporttion, ViewState["Hostel_id"].ToString(), ViewState["Room_Category_id"].ToString(), ViewState["TransportPath_id"].ToString(), ViewState["Boarding_Point_id"].ToString(), Total_amount, Total_discount, Transfer_Status, fee_group_id, category_id, sub_category_id, parameter, ViewState["Transport_id"].ToString(), con);
                        }

                        flag = true;
                        con.Close();
                        scope.Complete();
                    }

                    if (flag == true)
                    {
                        Alertme("Student discount has been uploaded successfully.", "success");
                        btn_final_submit.Visible = false;
                        //string query23 = "Select Admission_no,totalAmount,Status,Payment_Date,Payment_Mode  from Payment_transaction_process_bulk where Session_id=" + ViewState["Sessionid"].ToString() + " and User_id='" + ViewState["Userid"].ToString() + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and  group_transaction='" + group_transaction + "' ";
                        //DataTable dt1 = mycode.FillData(query23);
                        //if (dt1.Rows.Count == 0)
                        //{

                        //    grvExcelData.DataSource = null;
                        //    grvExcelData.DataBind();
                        //}
                        //else
                        //{
                        //    grvExcelData.DataSource = dt1;
                        //    grvExcelData.DataBind();
                        //}
                    }
                    else
                    {
                        Alertme("Something went wrong please try again.", "warning");
                    }
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void for_discount(string session_name, string session_id, string class_id, string admission_no, string hostel_taken, string transport_taken, string hostel_id, string room_category_id, string TransportPath_id, string Boarding_Point_id, string total_amount, string total_discount, string Transfer_Status, string fee_group_id, string category_id, string sub_category_id, string parameter, string transport_id, SqlConnection con)
        {
            //=====AdmissionAnnual
            string parameter_id = ""; string queryAdm = "";
            if (payments.IsUserExistS("select Id from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and (group_id='1' or group_id='2')", con))
            {
                if (parameter == "HostelMonthlyFee")
                {
                    if (fee_group_id == "1") //admission fee hostel
                    {
                        parameter_id = "5";
                        queryAdm = "select * from (select fmc.content,fmc.content_id,fmc.amount,cm.group_id,isnull((select top 1 disc_amount from Discount_Master where fee_head_id=fmc.content_id and session=fmc.session and Class_id=fmc.class_id and admission_no='" + admission_no + "' and parameter_id='" + parameter_id + "'),'0') discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='" + parameter_id + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t where convert(float, amount)>0";
                    }
                    if (fee_group_id == "2") //Annual fee hostel
                    {
                        parameter_id = "6";
                        queryAdm = "select * from (select fmc.content,fmc.content_id,fmc.amount,cm.group_id,isnull((select top 1 disc_amount from Discount_Master where fee_head_id=fmc.content_id and session=fmc.session and Class_id=fmc.class_id and admission_no='" + admission_no + "' and parameter_id='" + parameter_id + "'),'0') discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='" + parameter_id + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t where convert(float, amount)>0";
                    }
                }
                else
                {
                    if (fee_group_id == "1") //admission fee
                    {
                        parameter_id = "1";
                    }
                    if (fee_group_id == "2") //Annual fee
                    {
                        parameter_id = "2";
                    }
                    queryAdm = "select * from (select fmc.content,fmc.content_id,fmc.amount,cm.group_id,isnull((select top 1 disc_amount from Discount_Master where fee_head_id=fmc.content_id and session=fmc.session and Class_id=fmc.class_id and admission_no='" + admission_no + "' and parameter_id='" + parameter_id + "'),'0') discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='" + parameter_id + "') t where convert(float, amount)>0";
                }
            }
            else
            {
                queryAdm = "select feetype as content,content_id,(convert(float, payable)-convert(float, paid)) as amount,group_id,Disc as discount from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and (group_id='1' or group_id='2') and status='Dues'";
            }


            double total_discount_amount = My.toDouble(total_discount);
            if (total_discount_amount > 0)
            {
                DataTable dt = payments.dataTable(queryAdm, con);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (total_discount_amount > 0)
                        {
                            string fee_content_id = dr["content_id"].ToString();
                            double fee_amount = My.toDouble(dr["amount"].ToString());
                            double disc_given = 0;
                            if (total_discount_amount > fee_amount)
                            {
                                disc_given = fee_amount;
                                total_discount_amount = total_discount_amount - fee_amount;
                            }
                            else
                            {
                                disc_given = total_discount_amount;
                                total_discount_amount = 0;
                            }

                            //==============*************** 
                            if (My.toDouble(disc_given) > 0)
                            {
                                string discount_fee = disc_given.ToString();
                                string query = ""; string discount_on = ""; string parameter_idds = ""; string disc_for = ""; string room_cat_id = room_category_id;
                                if (parameter == "HostelMonthlyFee")
                                {
                                    if (fee_group_id == "1") //admission fee hostel
                                    {
                                        disc_for = "Hosteler";
                                        parameter_idds = "5";
                                        discount_on = "Admission";
                                        query = "select * from Discount_Master where Class_id=" + class_id + " and session='" + session_name + "' and group_id='" + fee_group_id + "' and month='NA' and admission_no='" + admission_no + "' and fee_head_id='" + fee_content_id + "' and parameter_id='" + parameter_idds + "' and Hostel_id=" + hostel_id + "";
                                    }
                                    if (fee_group_id == "2") //Annual fee hostel
                                    {
                                        disc_for = "Hosteler";
                                        parameter_idds = "6";
                                        discount_on = "Annual";
                                        query = "select * from Discount_Master where Class_id=" + class_id + " and session='" + session_name + "' and group_id='" + fee_group_id + "' and month='NA' and admission_no='" + admission_no + "' and fee_head_id='" + fee_content_id + "' and parameter_id='" + parameter_idds + "' and Hostel_id=" + hostel_id + "";
                                    }
                                }
                                else
                                {
                                    if (fee_group_id == "1") //admission fee
                                    {
                                        disc_for = "Days";
                                        parameter_idds = "1";
                                        discount_on = "Admission";
                                        query = "select * from Discount_Master where Class_id=" + class_id + " and session='" + session_name + "' and group_id='" + fee_group_id + "' and month='NA' and admission_no='" + admission_no + "' and fee_head_id='" + fee_content_id + "' and parameter_id='" + parameter_idds + "'";
                                    }
                                    if (fee_group_id == "2") //Annual fee
                                    {
                                        disc_for = "Days";
                                        parameter_idds = "2";
                                        discount_on = "Annual";
                                        query = "select * from Discount_Master where Class_id=" + class_id + " and session='" + session_name + "' and group_id='" + fee_group_id + "' and month='NA' and admission_no='" + admission_no + "' and fee_head_id='" + fee_content_id + "' and parameter_id='" + parameter_idds + "'";
                                    }
                                }



                                SqlDataAdapter ad = new SqlDataAdapter(query, con);
                                DataSet ds = new DataSet();
                                ad.Fill(ds, "Discount_Master");
                                DataTable dtD = ds.Tables[0];
                                if (dtD.Rows.Count == 0)
                                {
                                    SqlCommand cmd;
                                    string queryD = "INSERT INTO Discount_Master (Class_id,Discount_on,session,Discount_per,group_id,admission_no,month,fee_head_id,disc_amount,parameter_id,session_id,Branch_id,User_id,Date,time,discount_for,category_id,sub_category_id,Upload_from,Hostel_id,Room_Category_id,Student_Discunt_Type_id,Student_Discunt_Remarks) values (@Class_id,@Discount_on,@session,@Discount_per,@group_id,@admission_no,@month,@fee_head_id,@disc_amount,@parameter_id,@session_id,@Branch_id,@User_id,@Date,@time,@discount_for,@category_id,@sub_category_id,@Upload_from,@Hostel_id,@Room_Category_id,@Student_Discunt_Type_id,@Student_Discunt_Remarks)";
                                    cmd = new SqlCommand(queryD);
                                    cmd.Parameters.AddWithValue("@Class_id", class_id);
                                    cmd.Parameters.AddWithValue("@Discount_on", discount_on);
                                    cmd.Parameters.AddWithValue("@session", session_name);
                                    cmd.Parameters.AddWithValue("@Discount_per", My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(fee_amount), 2));
                                    cmd.Parameters.AddWithValue("@group_id", fee_group_id);
                                    cmd.Parameters.AddWithValue("@admission_no", admission_no);
                                    cmd.Parameters.AddWithValue("@month", "NA");
                                    cmd.Parameters.AddWithValue("@fee_head_id", fee_content_id);
                                    cmd.Parameters.AddWithValue("@disc_amount", My.toDouble(discount_fee).ToString("0.00"));
                                    cmd.Parameters.AddWithValue("@parameter_id", parameter_idds);
                                    cmd.Parameters.AddWithValue("@session_id", session_id);
                                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                                    cmd.Parameters.AddWithValue("@time", mycode.time());
                                    cmd.Parameters.AddWithValue("@discount_for", disc_for);
                                    cmd.Parameters.AddWithValue("@category_id", category_id);
                                    cmd.Parameters.AddWithValue("@sub_category_id", sub_category_id);
                                    cmd.Parameters.AddWithValue("@Upload_from", "");
                                    cmd.Parameters.AddWithValue("@Hostel_id", hostel_id);
                                    cmd.Parameters.AddWithValue("@Room_Category_id", room_cat_id);
                                    cmd.Parameters.AddWithValue("@Student_Discunt_Type_id", "0");
                                    cmd.Parameters.AddWithValue("@Student_Discunt_Remarks", "Bulk discount");
                                    if (payments.InsertUpdateData(cmd, con))
                                    {
                                    }
                                }
                                else
                                {
                                    foreach (DataRow drd in dtD.Rows)
                                    {
                                        drd["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(fee_amount), 2);
                                        drd["fee_head_id"] = fee_content_id;
                                        drd["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                        drd["Student_Discunt_Type_id"] = "0";
                                        drd["Student_Discunt_Remarks"] = "Bulk discount";
                                    }
                                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                    ad.Update(dt);
                                }
                            }
                        }
                    }
                }
            }





            ////MonthlyDiscount   
            ///
            if (total_discount_amount > 0)
            {
                string qryDiscMonth = "select * from Month_Index where Month in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Dues') or Month not in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Paid') order by Position asc";
                DataTable dtmntH = payments.dataTable(qryDiscMonth, con);
                if (dtmntH.Rows.Count > 0)
                {
                    foreach (DataRow drM in dtmntH.Rows)
                    {
                        string querymF = "";
                        if (hostel_taken.ToUpper() == "YES")  // hostel month fee
                        {
                            querymF = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='3'  and fmc.Hostel_Id=" + hostel_id + " and fmc.Room_Category_id=" + room_category_id + " and fmc.Month='" + drM["Month"].ToString() + "' ";
                        }
                        else
                        {
                            if (transport_taken.ToUpper() == "YES")
                            {
                                querymF = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='4' and fmc.Month='" + drM["Month"].ToString() + "' UNION all select t1.Parameter as content,parameter_id as content_id,Amount as amount,'0' as group_id,'0' as discount from Transportation_Fee_Master t1 join Month_Index t2 on t1.Month_id=t2.Month_Id where t1.session_id='" + session_id + "' and t1.Transportation_path_id='" + TransportPath_id + "'and t1.Boarding_Point_id='" + Boarding_Point_id + "' and t1.Month='" + drM["Month"].ToString() + "'";
                            }
                            else
                            {
                                querymF = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='4' and fmc.Month='" + drM["Month"].ToString() + "'";
                            }
                        }
                        DataTable dtmfeeHD = payments.dataTable(querymF, con);
                        if (dtmfeeHD.Rows.Count > 0)
                        {
                            foreach (DataRow drFh in dtmfeeHD.Rows)
                            {
                                #region MonthLY 
                                string Month = drM["Month"].ToString();
                                string fee_amount = drFh["amount"].ToString(); 
                                string content_id = drFh["content_id"].ToString();
                                string content_name = drFh["content"].ToString();

                                double disc_given = 0;
                                if (total_discount_amount > My.toDouble(fee_amount))
                                {
                                    disc_given = My.toDouble(fee_amount);
                                    total_discount_amount = (total_discount_amount - My.toDouble(fee_amount));
                                }
                                else
                                {
                                    disc_given = total_discount_amount;
                                    total_discount_amount = 0;
                                }

                                if (disc_given > 0)
                                {
                                    if (content_id == "1002") // FOR TRANSPORT
                                    {
                                        if (My.toDouble(disc_given) > 0)
                                        {
                                            string month_id = drM["Month_Id"].ToString();
                                            #region #fff  
                                            DataTable dtF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and group_id='3' and content_id='" + content_id + "' and month='" + Month + "' and status='Paid'", con);
                                            if (dtF.Rows.Count == 0)
                                            {
                                                //CHECK IN TYPEWISE
                                                DataTable dtT = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and group_id='3' and content_id='" + content_id + "' and month='" + Month + "' and status='Dues'", con);
                                                if (dtT.Rows.Count > 0)
                                                {
                                                    if (My.toDouble(dtT.Rows[0]["dues"].ToString()) > 0)
                                                    {
                                                        double duesamts = (My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString()));
                                                        if (My.toDouble(disc_given) > duesamts)
                                                        {
                                                            disc_given = duesamts;
                                                        }
                                                    }
                                                }

                                                //CHECK IN TYPEWISE 
                                                ViewState["discount_on"] = "TransportFee";
                                                SqlDataAdapter ad = new SqlDataAdapter("select * from Discount_Master_for_bus where Bus_path=" + TransportPath_id + " and session_id='" + session_id + "' and month='" + Month + "' and admission_no='" + admission_no + "' and Class_id='" + class_id + "' and Boarding_Point_id='" + Boarding_Point_id + "'", con);
                                                DataSet ds = new DataSet();
                                                ad.Fill(ds, "Discount_Master_for_bus");
                                                DataTable dt = ds.Tables[0];
                                                if (dt.Rows.Count == 0)
                                                {
                                                    DataRow dr = dt.NewRow();
                                                    dr["discount_for"] = "TransportFee";
                                                    dr["Class_id"] = class_id;
                                                    dr["Discount_on"] = ViewState["discount_on"].ToString();
                                                    dr["session"] = session_name;
                                                    dr["Discount_per"] = My.Round((disc_given * 100) / My.toDouble(fee_amount), 2);
                                                    dr["group_id"] = "51";
                                                    dr["admission_no"] = admission_no;
                                                    dr["month"] = Month;
                                                    dr["fee_head_id"] = content_id;
                                                    dr["disc_amount"] = disc_given.ToString("0.00");
                                                    dr["parameter_id"] = "0";
                                                    dr["category_id"] = "0";
                                                    dr["sub_category_id"] = "0";
                                                    dr["Bus_path"] = TransportPath_id;
                                                    dr["TransportationPath_id"] = TransportPath_id;
                                                    dr["session_id"] = session_id;
                                                    dr["Branch_id"] = ViewState["Branchid"].ToString();
                                                    dr["User_id"] = ViewState["Userid"].ToString();
                                                    dr["Date"] = mycode.date();
                                                    dr["time"] = mycode.time();
                                                    dr["Boarding_Point_id"] = Boarding_Point_id;
                                                    dr["Transportation_Id"] = transport_id;
                                                    dr["Student_Discunt_Type_id"] = "1";
                                                    dr["Student_Discunt_Remarks"] = "";
                                                    dt.Rows.Add(dr);

                                                    DataTable dtFF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and group_id='3' and content_id='" + content_id + "' and month='" + Month + "'", con);
                                                    if (dtFF.Rows.Count > 0)
                                                    {
                                                        double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - disc_given;
                                                        payments.exeSql("update Typewise_fee_collection set Disc='" + disc_given.ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "", con);
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (DataRow dr in dt.Rows)
                                                    {
                                                        dr["Discount_per"] = My.Round((disc_given * 100) / My.toDouble(fee_amount), 2);
                                                        dr["disc_amount"] = disc_given.ToString("0.00");
                                                        dr["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                                                        dr["Student_Discunt_Type_id"] = "1";
                                                        dr["Student_Discunt_Remarks"] = "";
                                                    }
                                                    DataTable dtFF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and group_id='3' and content_id='" + content_id + "' and month='" + Month + "'", con);
                                                    if (dtFF.Rows.Count > 0)
                                                    {
                                                        double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - disc_given;
                                                        payments.exeSql("update Typewise_fee_collection set Disc='" + disc_given.ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "", con);
                                                    }
                                                    ViewState["issubmit"] = "1";
                                                }
                                                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                                ad.Update(dt);
                                            }

                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        if (My.toDouble(disc_given) > 0)
                                        {
                                            DataTable dtF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and group_id='3' and content_id='" + content_id + "' and month='" + Month + "' and status='Paid'", con);
                                            if (dtF.Rows.Count == 0)
                                            {
                                                //CHECK IN TYPEWISE
                                                DataTable dtT = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and group_id='3' and content_id='" + content_id + "' and month='" + Month + "' and status='Dues'", con);
                                                if (dtT.Rows.Count > 0)
                                                {
                                                    if (My.toDouble(dtT.Rows[0]["dues"].ToString()) > 0)
                                                    {
                                                        double duesamts = (My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString()));
                                                        if (disc_given > duesamts)
                                                        {
                                                            disc_given = duesamts;
                                                        }
                                                    }
                                                }


                                                //CHECK IN TYPEWISE 
                                                string query = "";
                                                if (hostel_taken.ToUpper() == "YES") // hostel
                                                {
                                                    ViewState["parameterDisc"] = "3";
                                                    query = "select * from Discount_Master where Class_id=" + class_id + " and session='" + session_name + "' and group_id='3' and month='" + Month + "' and admission_no='" + admission_no + "' and fee_head_id='" + content_id + "' and parameter_id='" + ViewState["parameterDisc"].ToString() + "' and Hostel_id=" + hostel_id + " and Room_Category_id=" + room_category_id + "";
                                                }
                                                else
                                                {
                                                    ViewState["parameterDisc"] = "4";
                                                    query = "select * from Discount_Master where Class_id=" + class_id + " and session='" + session_name + "' and group_id='3' and month='" + Month + "' and admission_no='" + admission_no + "' and fee_head_id='" + content_id + "' and parameter_id='" + ViewState["parameterDisc"].ToString() + "'";
                                                }
                                                SqlDataAdapter ad = new SqlDataAdapter(query, con);
                                                DataSet ds = new DataSet();
                                                ad.Fill(ds, "Discount_Master");
                                                DataTable dt = ds.Tables[0];
                                                if (dt.Rows.Count == 0)
                                                {
                                                    DataRow dr = dt.NewRow();
                                                    dr["Class_id"] = class_id;
                                                    dr["Discount_on"] = "Monthly";
                                                    dr["session"] = session_name;
                                                    dr["Discount_per"] = My.Round((disc_given * 100) / My.toDouble(fee_amount), 2);
                                                    dr["group_id"] = "3";
                                                    dr["admission_no"] = admission_no;
                                                    dr["month"] = Month;
                                                    dr["fee_head_id"] = content_id;
                                                    dr["disc_amount"] = disc_given.ToString("0.00");
                                                    dr["parameter_id"] = ViewState["parameterDisc"].ToString();
                                                    dr["category_id"] = category_id;
                                                    dr["sub_category_id"] = sub_category_id;
                                                    dr["session_id"] = session_id;
                                                    dr["Branch_id"] = ViewState["Branchid"].ToString();
                                                    dr["User_id"] = ViewState["Userid"].ToString();
                                                    dr["Date"] = mycode.date();
                                                    dr["time"] = mycode.time();
                                                    dr["Hostel_id"] = hostel_id;
                                                    dr["Room_Category_id"] = room_category_id;
                                                    dr["Student_Discunt_Type_id"] = "1";
                                                    dr["Student_Discunt_Remarks"] = "";
                                                    dt.Rows.Add(dr);
                                                    DataTable dtFF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and group_id='3' and content_id='" + content_id + "' and month='" + Month + "'", con);
                                                    if (dtFF.Rows.Count > 0)
                                                    {
                                                        double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - disc_given;
                                                        payments.exeSql("update Typewise_fee_collection set Disc='" + disc_given.ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "", con);
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (DataRow dr in dt.Rows)
                                                    {
                                                        dr["Discount_per"] = My.Round((disc_given * 100) / My.toDouble(fee_amount), 2);
                                                        dr["disc_amount"] = disc_given.ToString("0.00");
                                                        dr["Student_Discunt_Type_id"] = "1";
                                                        dr["Student_Discunt_Remarks"] = "";
                                                    }

                                                    DataTable dtFF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and group_id='3' and content_id='" + content_id + "' and month='" + Month + "'", con);
                                                    if (dtFF.Rows.Count > 0)
                                                    {
                                                        double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - disc_given;
                                                        payments.exeSql("update Typewise_fee_collection set Disc='" + disc_given.ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "", con);
                                                    }
                                                }
                                                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                                ad.Update(dt);
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
        }
    }
}