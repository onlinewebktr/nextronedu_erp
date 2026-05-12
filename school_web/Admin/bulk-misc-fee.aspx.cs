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
    public partial class bulk_misc_fee : System.Web.UI.Page
    {
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
                        ViewState["branch_id"] = mycode.get_branch_id(Session["Admin"].ToString());

                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Mapping_Transportation_with_Student");
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    ddl_session.Focus();
                    Alertme("Please select session.", "warning");
                    return;
                }
                if (FileUpload1.HasFile)
                {
                    btn_final_submit.Visible = true;
                    ViewState["Session_ids"] = ddl_session.SelectedValue;
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
            Session["file"] = ("Upload_transport_student" + filerename + FileExtension);
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
                tblReadCSV.Columns.Add("Student_name");
                tblReadCSV.Columns.Add("Fee_head");
                tblReadCSV.Columns.Add("Is_send_to_admission");
                tblReadCSV.Columns.Add("Month");
                tblReadCSV.Columns.Add("Amount");

                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }
                pnl_grid.Visible = true;
                lbl_total1.Text = "Total no. of uploaded student : " + tblReadCSV.Rows.Count.ToString();
                ViewState["TotalNeedToMap"] = tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();
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
                    string Admission_No = "";
                    string fee_head = "";
                    string Is_send_to_adm = "";
                    string Month_name = "";
                    string Amount = "";

                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        Admission_No = grvExcelData.Rows[i].Cells[0].Text;
                        fee_head = grvExcelData.Rows[i].Cells[2].Text;
                        Is_send_to_adm = grvExcelData.Rows[i].Cells[3].Text;
                        Month_name = grvExcelData.Rows[i].Cells[4].Text;
                        Amount = grvExcelData.Rows[i].Cells[5].Text;

                        Dictionary<string, object> dc1 = My.get_selected_studentinfo(Admission_No, ViewState["Session_ids"].ToString(), ViewState["branch_id"].ToString());
                        string admissionserialnumber = (String)dc1["admissionserialnumber"];
                        string rollnumber = (String)dc1["rollnumber"];
                        string session = (String)dc1["session"];
                        string Section = (String)dc1["Section"];
                        string Class_id = (String)dc1["Class_id"];
                        string classname = (String)dc1["classname"];
                        string hosteltaken = (String)dc1["hosteltaken"];
                        string Transfer_Status = (String)dc1["Transfer_Status"];

                        string Transaction_Id = My.create_random_no_otp();
                        SqlCommand cmd;
                        string query = "INSERT INTO Misc_fee_bulk_upload_status (Admission_no,Class_id,Session_id,User_id,Fee_head,Amount,Status,createddate,Transaction_Id,group_transaction) values (@Admission_no,@Class_id,@Session_id,@User_id,@Fee_head,@Amount,@Status,@createddate,@Transaction_Id,@group_transaction)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                        cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                        cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                        cmd.Parameters.AddWithValue("@Class_id", Class_id);
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_ids"].ToString());
                        cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Fee_head", fee_head);
                        cmd.Parameters.AddWithValue("@Amount", Amount);
                        cmd.Parameters.AddWithValue("@Status", "Failed");
                        cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            save_misc_fee(Admission_No, Class_id, ViewState["Session_ids"].ToString(), hosteltaken, Transfer_Status, fee_head, Is_send_to_adm, Month_name, Amount, classname, Section, session);
                            My.exeSql("update Misc_fee_bulk_upload_status set Status='Success' where group_transaction='" + group_transaction + "' and Admission_no='" + Admission_No + "'");
                        }
                    }
                    Alertme("Record has been updated successfully.", "success");

                    btn_final_submit.Visible = false;
                    string query23 = "Select Admission_no,Fee_head,Amount,Status from Misc_fee_bulk_upload_status where Session_id=" + ViewState["Session_ids"].ToString() + " and  group_transaction='" + group_transaction + "'";
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

        private void save_misc_fee(string admission_No, string class_id, string session_id, string hosteltaken, string transfer_Status, string fee_head, string is_send_to_adm, string month_name, string amount, string classname, string Section, string sessionName)
        {
            string month = month_name; string typeMode = "MonthlyFee"; string parameter = ""; string content_id = ""; string group_id = "";
            string typewise_months = "April"; string typewise_months_position = "1"; string qryChkTypewise = "";

            qryChkTypewise = "select Id from Typewise_fee_collection where admission_no='" + admission_No + "' and session='" + sessionName + "' and (parameter='AdmissionFee' or parameter='HostelAdmissionFee' or parameter='AnnualFee' or parameter='HostelAnnualFee')";
            if (is_send_to_adm.ToUpper() == "YES")
            {
                if (transfer_Status.ToUpper() == "TRANSFERRED")
                {
                    transfer_Status = My.get_single_column_data("select top 1 Transfer_Status_Old as Column_Name from admission_registor where admissionserialnumber='" + admission_No + "' and Session_id='" + session_id + "' and Class_id='" + class_id + "' and  Status='1'");
                }
                month = "";
                typeMode = "AnnualFee";
                parameter = "AnnualFee";
                content_id = "ANN01";
                group_id = "2";
                if (transfer_Status.ToUpper() == "NEW")
                {
                    typeMode = "AdmissionFee";
                    parameter = "AdmissionFee";
                    content_id = "ANN01";
                    group_id = "2";
                }

                if (hosteltaken.ToUpper() == "YES")
                {
                    parameter = "HostelAnnualFee";
                    content_id = "ANN01";
                    group_id = "2";
                    if (transfer_Status.ToUpper() == "NEW")
                    {
                        parameter = "HostelAdmissionFee";
                        content_id = "ADM01";
                        group_id = "1";
                    }
                }
            }
            else
            {
                parameter = "MonthlyFee";
                if (hosteltaken.ToUpper() == "YES")
                {
                    parameter = "HostelMonthlyFee";
                }
                typewise_months = month_name;
                typewise_months_position = My.get_single_column_data("select top 1 Position as Column_Name from Month_Index where Month='" + typewise_months + "'");
                content_id = "1001";
                group_id = "3";
                qryChkTypewise = "select Id from Typewise_fee_collection where admission_no='" + admission_No + "' and session='" + sessionName + "' and month='" + typewise_months + "' and (parameter='MonthlyFee'  or parameter='HostelMonthlyFee')";
            }

            string legder = "School";
            if (hosteltaken.ToUpper() == "YES")
            {
                legder = "Hostel";
            }

            SqlCommand cmd;
            string query = "INSERT INTO Misc_Fee_Master_Studentwise (Admission_No,Month,Session,Session_id,Perticular,Amount,Ledger,Type_Mode,Date,Idate,Created_by) values (@Admission_No,@Month,@Session,@Session_id,@Perticular,@Amount,@Ledger,@Type_Mode,@Date,@Idate,@Created_by)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Admission_No", admission_No);
            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@Session", sessionName);
            cmd.Parameters.AddWithValue("@Session_id", session_id);
            cmd.Parameters.AddWithValue("@Perticular", fee_head);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Ledger", legder);
            cmd.Parameters.AddWithValue("@Type_Mode", typeMode);
            cmd.Parameters.AddWithValue("@Date", mycode.date());
            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            if (My.InsertUpdateData(cmd))
            {
                if (mycode.IsUserExist(qryChkTypewise))
                { }
                else
                {
                    SqlCommand cmd1;
                    string query1 = "INSERT INTO Typewise_fee_collection (admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,user_id,content_id,transection,Ledger,group_id,class_id,position,Disc,Payable_after_disc,branchid) values (@admission_no,@class,@session,@section,@parameter,@Date,@idate,@feetype,@payable,@paid,@dues,@status,@month,@user_id,@content_id,@transection,@Ledger,@group_id,@class_id,@position,@Disc,@Payable_after_disc,@branchid)";
                    cmd1 = new SqlCommand(query1);
                    cmd1.Parameters.AddWithValue("@admission_no", admission_No);
                    cmd1.Parameters.AddWithValue("@class", classname);
                    cmd1.Parameters.AddWithValue("@session", sessionName);
                    cmd1.Parameters.AddWithValue("@section", Section);
                    cmd1.Parameters.AddWithValue("@parameter", parameter);
                    cmd1.Parameters.AddWithValue("@Date", mycode.date());
                    cmd1.Parameters.AddWithValue("@idate", mycode.idate());
                    cmd1.Parameters.AddWithValue("@feetype", fee_head);
                    cmd1.Parameters.AddWithValue("@payable", amount);
                    cmd1.Parameters.AddWithValue("@paid", "0.00");
                    cmd1.Parameters.AddWithValue("@dues", amount);
                    cmd1.Parameters.AddWithValue("@status", "Dues");
                    cmd1.Parameters.AddWithValue("@month", typewise_months);
                    cmd1.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                    cmd1.Parameters.AddWithValue("@content_id", content_id);
                    cmd1.Parameters.AddWithValue("@transection", content_id);
                    cmd1.Parameters.AddWithValue("@Ledger", legder);
                    cmd1.Parameters.AddWithValue("@group_id", group_id);
                    cmd1.Parameters.AddWithValue("@class_id", class_id);
                    cmd1.Parameters.AddWithValue("@position", typewise_months_position);
                    cmd1.Parameters.AddWithValue("@Disc", "0.00");
                    cmd1.Parameters.AddWithValue("@Payable_after_disc", amount);
                    cmd1.Parameters.AddWithValue("@branchid", ViewState["branch_id"].ToString());
                    if (My.InsertUpdateData(cmd1))
                    {
                    }
                }
            }
        }
    }
}