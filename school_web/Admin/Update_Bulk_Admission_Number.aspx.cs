using CsvHelper;
using ExcelDataReader;
using Microsoft.VisualBasic.FileIO;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Update_Bulk_Admission_Number : System.Web.UI.Page
    {
        string scrpt;
        My mycode = new My();
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

                }
            }
        }

         

        protected void btn_upload_Click1(object sender, EventArgs e)
        {
            upload_excel_file();
        }

        private void upload_excel_file()
        {
            string file = upload_csv_data();
            if (file != "")
            {
                string csvid  = My.auto_serialS("csvid");
                ViewState["csvid"] = csvid;
                SqlCommand cmd;
                string strQuery = @"INSERT INTO excel_file (file_data,Date,idate,csvid,Status,User_Id) values (@file_data,@Date,@idate,@csvid,@Status,@User_Id)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@file_data", file);
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@csvid", csvid);
                cmd.Parameters.AddWithValue("@Status", "SUBMITTED");
                cmd.Parameters.AddWithValue("@User_Id", ViewState["Userid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
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
            Session["file"] = ("Upload_question_from_excel" + filerename + FileExtension);
            String[] allowedExtensions = { ".csv", ".xlsx" };
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
                    string path = (Server.MapPath("../UploadedImage/uploads")).ToString();
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
                    My.submitException(ex, "check_wrap_or_not");
                    FileSaved = false;
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
                dbfilePath = @"/UploadedImage/uploads/" + fileName;
                Session["fileName"] = fileName;
            }
            return dbfilePath;
        }
        private bool check_wrap_or_not(string path)
        {
            try
            {

                string qry = "";
                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("Old_Admision_No");
                tblReadCSV.Columns.Add("New_Admission_No");
                if (path.EndsWith(".csv"))
                {
                    using (var reader = new StreamReader(path, System.Text.Encoding.UTF8)) // UTF-8 for Unicode
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Read(); // Skip header row
                        csv.ReadHeader();

                        while (csv.Read())
                        {
                            tblReadCSV.Rows.Add(
                                csv.GetField("Old_Admision_No"),
                                csv.GetField("New_Admission_No")
                               
                            );
                        }
                    }

                }
                else if (path.EndsWith(".xlsx"))
                {
                    var tbl = ReadExcelToDataTable(path);
                    foreach (DataRow dr in tbl.Rows)
                    {
                        tblReadCSV.Rows.Add(dr.ItemArray);
                    }
                }
                pnl_grid.Visible = true;
                finalsubmitpnl.Visible = true;
                lbl_total.Text = "Total Admission No:- " + tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();
               
            }
            catch (Exception e)
            {
                My.submitException(e, "check_wrap_or_not enter");
                Alertme(e.ToString(), "warning");
                return false;
            }
            return true;
        }
        public static DataTable ReadExcelToDataTable(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // treat the first row as column names
                        }
                    };

                    var result = reader.AsDataSet(conf);
                    return result.Tables[0]; // returns the first worksheet as DataTable
                }
            }
        }

        protected void btn_Submit_final_Click(object sender, EventArgs e)
        {
            try
            {

                string confirmValue = string.Empty;
                confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    string qry = "";
                    string Old_Admision_No;
                    string New_Admission_No;
                    string Transaction_Id = My.create_random_no_otp();
                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        Old_Admision_No = grvExcelData.Rows[i].Cells[0].Text;
                        New_Admission_No = grvExcelData.Rows[i].Cells[1].Text;
                        // string bookid = cretebookid();
                        string Status = "Pending";
                        Dictionary<string, object> dc1 = My.get_admission_no_status(Old_Admision_No, New_Admission_No);
                        string oldadmissionno = (String)dc1["oldadmission_no"];
                        string newadmissionno = (String)dc1["newadmission_no"];
                        string  Remarks = "";
                        if (oldadmissionno=="Yes" && oldadmissionno=="Yes")
                        {
                            Remarks = "The admission number has been verified.";
                           
                        }
                        else
                        {   if(oldadmissionno=="Yes" && oldadmissionno!="Yes")
                            {
                                Remarks = "New Admission No. Status Status = " + newadmissionno;
                            }
                            else if (oldadmissionno != "Yes" && oldadmissionno == "Yes")
                            {
                                Remarks = "Old Admission No. Status = " + oldadmissionno;
                            }
                            else
                            {
                                Remarks = "Old Admission No. Status = " + oldadmissionno+" New Admission No. Status = "+ newadmissionno;
                            }
                            Status = "Not updated";
                        }

                        My.Insert("Update_admison_number_using_excel", new
                        {
                            
                            Old_admission_no = Old_Admision_No,
                            New_admission_no = New_Admission_No,
                            Status = Status,
                            Remarks = Remarks,
                            Date_time =My.getdate1(),
                            Updated_by = ViewState["Userid"].ToString(),
                            Transaction_Id = Transaction_Id,
                            File_id = ViewState["csvid"].ToString()
                        });
                    }
                    bool final = false;
                    final_update_data(Transaction_Id);

                    string query23 = "Select  * from Update_admison_number_using_excel where  Updated_by='" + ViewState["Userid"].ToString() + "'   and  Transaction_Id='" + Transaction_Id + "' ";
                    DataTable dt1 = mycode.FillData(query23);
                    if (dt1.Rows.Count == 0)
                    {

                        grvExcelData.DataSource = null;
                        grvExcelData.DataBind();
                        GridView2.DataSource = null;
                        GridView2.DataBind();
                    }
                    else
                    {
                        grvExcelData.DataSource = null;
                        grvExcelData.DataBind();

                        GridView2.DataSource = dt1;
                        GridView2.DataBind();

                        final = dt1.AsEnumerable().All(row => row["Status"].ToString() == "Success");


                    }

                    if (final == true)
                    {
                        Alertme("Admission No. has has been updated successfully.", "success");
                        btn_Submit_final.Visible = false;
                        finalsubmitpnl.Visible = false;
                    }
                    else
                    {
                        btn_Submit_final.Visible = false;
                        finalsubmitpnl.Visible = false;
                    }
                }


            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }

        }

        private void final_update_data(string transaction_Id)
        {
            DataTable dt = mycode.FillData("Select * from Update_admison_number_using_excel where Transaction_Id='" + transaction_Id + "'  and Status='Pending' ");
            if (dt.Rows.Count == 0)
            {
                
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Id = dt.Rows[i]["Id"].ToString();
                    string Old_admission_no = dt.Rows[i]["Old_admission_no"].ToString();
                    string New_admission_no = dt.Rows[i]["New_admission_no"].ToString();
                    Final_add_update_admission_no(Id, Old_admission_no, New_admission_no);
                }
            }
        }

        private void Final_add_update_admission_no(string id, string old_admission_no, string new_admission_no)
        {
            bool newadmission_no = get_admission_no_status(new_admission_no);
            if (newadmission_no == true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_update_admission_no_and_Class";
                cmd.Parameters.AddWithValue("@admissionserialnumber_old", old_admission_no.Trim());
                cmd.Parameters.AddWithValue("@admissionserialnumber", new_admission_no.Trim());
                cmd.Parameters.AddWithValue("@session", My.get_session());
                cmd.Parameters.AddWithValue("@Session_id", My.get_session_id());
                cmd.Parameters.AddWithValue("@updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@updated_date", My.getdate1());
                cmd.Parameters.AddWithValue("@status", "updateadmissionno");
                cmd.Parameters.AddWithValue("@Branch_id", "1");
                if (UsesCode.InsertUpdateData_sp(cmd))
                {
                    
                    Alertme("Admission no has been successfully changed", "success");
                    mycode.executequery("update Update_admison_number_using_excel set Status='Success' where Id=" + id + "");


                }
            }
            else
            {
                Alertme("Sorry your new admission no. is already exist. ", "warning");
            }
        }
        private bool get_admission_no_status(string regid)
        {
            DataTable dt = mycode.FillData("Select admissionserialnumber  from admission_registor  where admissionserialnumber='" + regid + "' ");
            if (dt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                if (string.Equals(status, "Success", StringComparison.OrdinalIgnoreCase))
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen; // green for Success
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral; // red for anything else
                }
            }
        }
    }
}