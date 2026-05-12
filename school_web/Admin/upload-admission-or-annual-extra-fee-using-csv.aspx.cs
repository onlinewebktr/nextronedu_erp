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
    public partial class upload_admission_or_annual_extra_fee_using_csv : System.Web.UI.Page
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
                        ddlsession.SelectedValue = My.get_session_id();
                        ViewState["RepeatFine"] = My.get_fine_repat_no();
                        ViewState["firm_id"] = My.get_firm_id(); 
                    }
                }
            }
            catch
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

            catch
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
            dr[6] = "Admission_Or_Annual";
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
            Session["file"] = ("Upload_Admission_Or_Annual" + filerename + FileExtension);
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

                tblReadCSV.Columns.Add("Admission_No");
                tblReadCSV.Columns.Add("Payment_Date");
                tblReadCSV.Columns.Add("Payment_Mode");
                tblReadCSV.Columns.Add("Payment_Amount");
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

                    string group_transaction = My.create_random_no_otp();
                    string qry = "";


                    string Admission_No = "";
                    string Payment_Date = "";
                    string Payment_Mode = "";
                    string Payment_Amount = "";


                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        Admission_No = grvExcelData.Rows[i].Cells[0].Text;
                        Payment_Date = grvExcelData.Rows[i].Cells[1].Text;
                        Payment_Mode = grvExcelData.Rows[i].Cells[2].Text;
                        Payment_Amount = grvExcelData.Rows[i].Cells[3].Text;
                        // ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        // ViewState["session_id"] = My.get_session_id();
                        Dictionary<string, object> dc1 = My.get_selected_studentinfo(Admission_No, ViewState["session_id"].ToString(), ViewState["branchid"].ToString());
                        string admissionserialnumber = (String)dc1["admissionserialnumber"];
                        string rollnumber = (String)dc1["rollnumber"];
                        string session = (String)dc1["session"];
                        string Section = (String)dc1["Section"];
                        string Class_id = (String)dc1["Class_id"];
                        string classname = (String)dc1["classname"];
                        ViewState["Sessionid"] = ViewState["session_id"].ToString();
                        if (Admission_No != "0")
                        {
                            string query2 = "Select *  from Payment_transaction_process_admission_bulk where Session_id=" + ViewState["Sessionid"].ToString() + "  and Class_id=" + Class_id + " and Admission_no='" + admissionserialnumber + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Status='Pending' and User_id='" + ViewState["Userid"].ToString() + "' and Branch_id='" + ViewState["branchid"].ToString() + "'  and group_transaction='" + group_transaction + "' ";

                            DataTable dt = mycode.FillData(query2);
                            if (dt.Rows.Count == 0)
                            {
                                string Transaction_Id = My.create_random_no_otp();
                                SqlCommand cmd;
                                string query = "INSERT INTO Payment_transaction_process_admission_bulk (Admission_no,Class_id,Session_id,totalAmount,Branch_id,User_id,Status,createddate,Transaction_Id,Payment_Mode,Payment_Date,group_transaction) values (@Admission_no,@Class_id,@Session_id,@totalAmount,@Branch_id,@User_id,@Status,@createddate,@Transaction_Id,@Payment_Mode,@Payment_Date,@group_transaction); ";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                                cmd.Parameters.AddWithValue("@Class_id", Class_id);
                                cmd.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
                                cmd.Parameters.AddWithValue("@totalAmount", Payment_Amount);
                                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Status", "Pending");
                                cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                                cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                                cmd.Parameters.AddWithValue("@Payment_Mode", Payment_Mode);
                                cmd.Parameters.AddWithValue("@Payment_Date", Payment_Date);
                                cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                                if (InsertUpdate.InsertUpdateData(cmd))
                                {
                                    // call payment  payment
                                    Update_student_payment(Transaction_Id, Admission_No, Class_id, Payment_Mode, Payment_Date, Payment_Amount, ViewState["session_id"].ToString(), Transaction_Id);
                                }

                            }

                            else
                            {

                                string Transaction_Id = dt.Rows[0]["Transaction_Id"].ToString();
                                Payment_Amount = dt.Rows[0]["totalAmount"].ToString();
                                Payment_Mode = dt.Rows[0]["Payment_Mode"].ToString();
                                Payment_Date = dt.Rows[0]["Payment_Date"].ToString();
                                admissionserialnumber = dt.Rows[0]["Admission_no"].ToString();
                                Update_student_payment(Transaction_Id, Admission_No, Class_id, Payment_Mode, Payment_Date, Payment_Amount, ViewState["session_id"].ToString(), Transaction_Id);
                            }

                        }
                        else
                        {
                            string Transaction_Id = My.create_random_no_otp();
                            SqlCommand cmd;
                            string query = "INSERT INTO Payment_transaction_process_admission_bulk (Admission_no,Class_id,Session_id,totalAmount,Branch_id,User_id,Status,createddate,Transaction_Id,Payment_Mode,Payment_Date,group_transaction) values (@Admission_no,@Class_id,@Session_id,@totalAmount,@Branch_id,@User_id,@Status,@createddate,@Transaction_Id,@Payment_Mode,@Payment_Date,@group_transaction); ";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                            cmd.Parameters.AddWithValue("@Class_id", Class_id);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
                            cmd.Parameters.AddWithValue("@totalAmount", Payment_Amount);
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Status", "Failed");
                            cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                            cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                            cmd.Parameters.AddWithValue("@Payment_Mode", Payment_Mode);
                            cmd.Parameters.AddWithValue("@Payment_Date", Payment_Date);
                            cmd.Parameters.AddWithValue("@group_transaction", group_transaction);

                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                Update_student_payment(Transaction_Id, Admission_No, Class_id, Payment_Mode, Payment_Date, Payment_Amount, ViewState["session_id"].ToString(), Transaction_Id);

                            }
                        }
                    }

                    Alertme("Student has been uploaded successfully.", "success");
                    btn_final_submit.Visible = false;
                    string query23 = "Select Admission_no,totalAmount,Status,Payment_Date,Payment_Mode  from Payment_transaction_process_admission_bulk where Session_id=" + ViewState["Sessionid"].ToString() + " and User_id='" + ViewState["Userid"].ToString() + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and  group_transaction='" + group_transaction + "' ";
                    DataTable dt1 = mycode.FillData(query23);
                    if (dt1.Rows.Count == 0)
                    {

                        grvExcelData.DataSource = null;
                        grvExcelData.DataBind();
                    }
                    else
                    {
                        grvExcelData.DataSource = dt1;
                        grvExcelData.DataBind();
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


        extra_fee pu = new extra_fee(); 
        private void Update_student_payment(string transaction_Id, string admission_No, string class_id, string payment_Mode, string payment_Date, string payment_Amount, string session_id, string Transaction_Id)
        {
            string pay_idate1 = "0";
            Dictionary<string, object> dc1 = My.getstudentinfo(admission_No, session_id);
            string Name = (String)dc1["Name"];
            string Class_id = (String)dc1["Class_id"];
            string Session_id = (String)dc1["Session_id"];
            string Session = (String)dc1["Session"];
            string Total_pay = payment_Amount;
            string Payment_type = payment_Mode;
            string category_id = (String)dc1["category_id"];
            string sub_category_id = (String)dc1["sub_category_id"];
            string Transfer_Status = (String)dc1["Transfer_Status"];
            string Admission_no = (String)dc1["Admission_no"];
            string classname = (String)dc1["classname"];

            string Date = payment_Date;
            try
            {
                pay_idate1 = payment_Date.Substring(6, 4) + payment_Date.Substring(3, 2) + payment_Date.Substring(0, 2);

            }
            catch
            {
                pay_idate1 = "0";


            }

            string hostaltaken = (String)dc1["hosteltaken"];
            int pay_idate = Convert.ToInt32(pay_idate1);
            string Branch_id = (String)dc1["Branch_id"];
            string Section = (String)dc1["Section"];
            string parameter = "";
            if (Transfer_Status == "New")
            {
                parameter = hostaltaken.ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
            }
            else
            {
                parameter = hostaltaken.ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
            }
            if (Admission_no == "0")
            { 
            }
            else
            {
                Dictionary<string, object> dc12 = mycode.Bind_hostel_data_for_assined_student(Session_id, Class_id, admission_No);
                ViewState["Hostel_id"] = (String)dc12["Hostel_id"];
                string Hostel_id = (String)dc12["Hostel_id"]; 
                pu.Bind_fee_details(parameter, Session, Session_id, Class_id, admission_No, category_id, sub_category_id, Total_pay, Date, Name, Branch_id, Transaction_Id, Section, Transaction_Id, Payment_type, hostaltaken, Hostel_id, "Software", classname);
            }
        }
    }
}