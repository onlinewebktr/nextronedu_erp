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
    public partial class Upload_Previous_Year_Month_Wise : System.Web.UI.Page
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

                        ViewState["college_name"] = My.get_college_name();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select Session,session_id from session_details order by Session ");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_ddl(ddl_month, " select  Month from dbo.[Month_Index] order by Position asc");




                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Upload_dues");
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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                btn_excel.Visible = false;
                btn_final_submit.Visible = false;
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (ddl_month.Text == "Select")
                {
                    Alertme("Please select month", "warning");
                    ddl_month.Focus();
                    return;
                }
                if (FileUpload1.HasFile)
                {
                    btn_final_submit.Visible = true;

                    ViewState["dupAdmiD"] = "0";
                    upload_excel_file();
                }
                else
                {
                    Alertme("Please choose excel.csv file.", "warning");
                    ddlsession.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
            }
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
            dr[6] = "Old Year Month Fee";

            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        #region FileSave
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
            Session["file"] = ("Upload_student_dues" + filerename + FileExtension);
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
                Alertme("Choose only csv File", "warning");
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
        #endregion


        private bool check_wrap_or_not(string path)
        {
            try
            {
                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("Admission No");
                tblReadCSV.Columns.Add("Dues");
                tblReadCSV.Columns.Add("Particular_Remarks");
                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }
                pnl_grid.Visible = true;

                lbl_total.Text = "Total Data :- " + tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();

                //==============
                string Admission_no = "";

                for (int i = 0; i < grvExcelData.Rows.Count; i++)
                {

                    Admission_no = grvExcelData.Rows[i].Cells[0].Text;
                    if (ViewState["college_name"].ToString() == "St. Mary school")
                    {
                        if (Admission_no.Length == 4)
                        {
                            Admission_no = "0" + Admission_no;
                        }
                        if (Admission_no.Length == 3)
                        {
                            Admission_no = "00" + Admission_no;
                        }

                    }
                    else if (ViewState["college_name"].ToString() == "ST Mary Itahar")
                    {
                        if (Admission_no.Length == 4)
                        {
                            Admission_no = "0" + Admission_no;
                        }
                        if (Admission_no.Length == 3)
                        {
                            Admission_no = "00" + Admission_no;
                        }
                    }



                    #region check duplicate
                    string adno = Admission_no;
                    DataTable dt = My.dataTable(" select admissionserialnumber,studentname from admission_registor where admissionserialnumber='" + adno + "' and Session_id='" + ddlsession.SelectedValue + "'");
                    if (dt.Rows.Count == 0)
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                        Alertme("Sorry! Admission no. not found to selected session or class.", "warning");


                    }
                    else
                    {
                        ViewState["SName"] = dt.Rows[0]["studentname"].ToString();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            string groupid = My.create_random_no_otp();
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
                ddlsession.Focus();
                return;
            }
            if (ddl_month.Text == "Select")
            {
                Alertme("Please select month", "warning");
                ddlsession.Focus();
                return;
            }
            else
            {
                try
                {
                    string qry = "";
                    string SessionName = ddlsession.SelectedItem.Text;
                    string Session_id = ddlsession.SelectedValue;
                    string month_name = ddlsession.SelectedItem.Text;

                    string AdmissionNo = "";
                    string DuesAmt = "";
                    string Particular_Remarks = "";
                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        AdmissionNo = grvExcelData.Rows[i].Cells[0].Text.Trim();
                        DuesAmt = grvExcelData.Rows[i].Cells[1].Text.Trim();
                        Particular_Remarks = grvExcelData.Rows[i].Cells[2].Text.Trim();


                        if (ViewState["college_name"].ToString() == "St. Mary school")
                        {
                            if (AdmissionNo.Length == 4)
                            {
                                AdmissionNo = "0" + AdmissionNo;
                            }
                            if (AdmissionNo.Length == 3)
                            {
                                AdmissionNo = "00" + AdmissionNo;
                            }

                        }
                        else if (ViewState["college_name"].ToString() == "ST Mary Itahar")
                        {
                            if (AdmissionNo.Length == 4)
                            {
                                AdmissionNo = "0" + AdmissionNo;
                            }
                            if (AdmissionNo.Length == 3)
                            {
                                AdmissionNo = "00" + AdmissionNo;
                            }
                        }

                        string Class_Id = "0";
                        string student_name = ViewState["SName"].ToString();
                        string hosteltaken = "";
                        DataTable dtX = My.dataTable(" select studentname,Class_id,hosteltaken from admission_registor where admissionserialnumber='" + AdmissionNo + "' and Session_id='" + ddlsession.SelectedValue + "'");
                        if (dtX.Rows.Count != 0)
                        {
                            student_name = dtX.Rows[0]["studentname"].ToString();
                            Class_Id = dtX.Rows[0]["Class_id"].ToString();
                            hosteltaken = dtX.Rows[0]["hosteltaken"].ToString();

                            if (hosteltaken == "")
                            {
                                hosteltaken = "School";

                            }
                            else if (hosteltaken.ToLower() == "no")
                            {
                                hosteltaken = "School";


                            }
                            else if (hosteltaken.ToLower() == "no")
                            {
                                hosteltaken = "School";


                            }
                            else if (hosteltaken.ToLower() == "&nbsp;")
                            {
                                hosteltaken = "School";


                            }
                            else
                            {
                                hosteltaken = "Hostel";

                            }
                        }
                        if (Particular_Remarks == "")
                        {
                            Particular_Remarks = "Previous Year Dues";
                        }
                        else if (Particular_Remarks == "&nbsp;")
                        {
                            Particular_Remarks = "Previous Year Dues";
                        }

                        string Transaction_Id = "";
                        SqlDataAdapter ad = new SqlDataAdapter("select * from Month_Wise_Fee_Dues_Studentwise where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + Class_Id + "' and Admission_No='" + AdmissionNo + "' and Month='" + ddl_month.Text + "'", My.conn);
                        DataSet ds = new DataSet();
                        ad.Fill(ds, "Previous_Year_Dues");
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            Transaction_Id = My.create_random_no_otp();
                            DataRow dr = dt.NewRow();
                            dr["Admission_No"] = AdmissionNo;
                            dr["Month"] = ddl_month.Text;
                            dr["Session"] = ddlsession.SelectedItem.Text;
                            dr["Session_id"] = ddlsession.SelectedValue;
                            dr["Amount"] = My.toDouble(DuesAmt).ToString("0.00");
                            dr["Ledger"] = hosteltaken;
                            dr["Date"] = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
                            dr["Idate"] = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                            dr["Class_Id"] = Class_Id;
                            dr["Status"] = "Pending";
                            dr["Row_id"] = Transaction_Id;
                            dr["Particular_Remarks"] = Particular_Remarks;
                            dr["Group_id"] = groupid;
                            dr["User_id"] = ViewState["Userid"].ToString();
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            Transaction_Id = dt.Rows[0]["Row_id"].ToString();
                            string id = dt.Rows[0]["Id"].ToString();
                            foreach (DataRow dr in dt.Rows)
                            {
                                dr["Amount"] = My.toDouble(DuesAmt).ToString("0.00");
                                dr["Status"] = "Pending";
                                dr["Particular_Remarks"] = Particular_Remarks;
                                dr["Group_id"] = groupid;
                                dr["User_id"] = ViewState["Userid"].ToString();
                            }
                        }
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        insert_data_Misc_Fee_Master_Studentwise(hosteltaken, AdmissionNo, Class_Id, Transaction_Id, Particular_Remarks, DuesAmt);
                    }
                    Alertme("Student dues has been uploaded successfully.", "success");
                    btn_final_submit.Visible = false;
                    btn_excel.Visible = false;
                    Bind_uploaded_data(groupid);
                }

                catch (Exception ex)
                {
                    //My.Save_Exception(ex.ToString());

                    My.submitException(ex, "backdues month");
                }
            }
        }

        private void Bind_uploaded_data(string groupid)
        {
            string query = "Select Admission_No,Session,Month,Amount,Status,Remarks from Month_Wise_Fee_Dues_Studentwise where Group_id='" + groupid + "' and User_id='" + ViewState["Userid"].ToString() + "' ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_excel.Visible = false;
                grvExcelData.DataSource = null;
                grvExcelData.DataBind();
            }
            else
            {
                btn_excel.Visible = true;
                grvExcelData.DataSource = dt;
                grvExcelData.DataBind();

                for (int i = 0; i < grvExcelData.Rows.Count; i++)
                {

                    if (dt.Rows[i]["Status"].ToString() == "Pending")

                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.LightGreen;
                    }




                }


            }

        }

        private void insert_data_Misc_Fee_Master_Studentwise(string hosteltaken, string admissionNo, string Class_Id, string Transaction_Id, string Particular_Remarks, string DuesAmt)
        {


            bool chekpy_or_not_pay = get_chek_pay_ro_not_pay_dta(admissionNo);
            if (chekpy_or_not_pay == true)
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Misc_Fee_Master_Studentwise where Admission_No='" + admissionNo + "' and Month='" + ddl_month.Text + "' and Session_id='" + ddlsession.SelectedValue + "'  and Perticular='" + Particular_Remarks + "' ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[1] = admissionNo;
                    dr[2] = ddl_month.Text;
                    dr[3] = ddlsession.SelectedItem.Text;
                    dr[4] = ddlsession.SelectedValue;
                    dr[5] = Particular_Remarks;
                    dr[6] = My.toDouble(DuesAmt).ToString("0.00");
                    dr["Old_year_Dues_Type"] = "Yes";
                    dr["Row_id"] = Transaction_Id;

                    dr["Ledger"] = hosteltaken;
                    dt.Rows.Add(dr);

                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr[1] = admissionNo;
                        dr[2] = ddl_month.Text;
                        dr[3] = ddlsession.SelectedItem.Text;
                        dr[4] = ddlsession.SelectedValue;
                        dr[5] = Particular_Remarks;
                        dr[6] = My.toDouble(DuesAmt).ToString("0.00");
                        dr["Old_year_Dues_Type"] = "Yes";
                        dr["Ledger"] = hosteltaken;
                        dr["Row_id"] = Transaction_Id;

                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                mycode.executequery("update Month_Wise_Fee_Dues_Studentwise set Status='verified',Remarks='OK' where Row_id='" + Transaction_Id + "'");
            }
            else
            {
                string abcd = "Already paid selected month : " + ddl_month.Text;
                mycode.executequery("update Month_Wise_Fee_Dues_Studentwise set Status='Pending',Remarks='" + abcd + "' where Row_id='" + Transaction_Id + "'");

            }


        }

        private bool get_chek_pay_ro_not_pay_dta(string admissionNo)
        {
            DataTable dt = mycode.FillData("Select * from Typewise_fee_collection where admission_no='" + admissionNo + "' and   session='" + ddlsession.SelectedItem.Text + "'  and parameter in ('HostelMonthlyFee','MonthlyFee') and month='" + ddl_month.Text + "'  ");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Upload_Previous_Year_Month_Wise.aspx", false);
        }

        protected void btn_excel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    grvExcelData.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}