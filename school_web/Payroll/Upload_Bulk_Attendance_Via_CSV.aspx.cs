using Microsoft.VisualBasic.FileIO;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class Upload_Bulk_Attendance_Via_CSV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string url = My.url();
                a1.HRef = url + "home";
                a2.HRef = url + "home";
                //

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
        My mycode = new My();
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
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }
        private string upload_csv_data()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("ddMMyyyy");
            string time = dtm.ToString("hhmmss");
            String filerename = date + "_" + time;

            string dbfilePath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;

            Session["file1"] = FileUpload1.FileName;
            String FileExtension = Path.GetExtension(Session["file1"].ToString()).ToLower();
            Session["file"] = ("Bulk_attendance" + filerename + FileExtension);
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
                    string path = (Server.MapPath("../Master_Img/Apply_career")).ToString();
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
                dbfilePath = @"/Master_Img/Apply_career/" + fileName;
                Session["fileName"] = fileName;
            }
            return dbfilePath;
        }

        private bool check_wrap_or_not(string path)
        {
            try
            {
                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("EmployeeCode");
                tblReadCSV.Columns.Add("Date");
                tblReadCSV.Columns.Add("InTime");
                tblReadCSV.Columns.Add("OutTime");
                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }
                pnl_grid.Visible = true;
                btn_final_submit.Visible = true;

                lbl_total.Text = "Total Data :- " + tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();

                //==============

                string Emp_Code = "";
                string Date = "";
                string InTime = "";
                string OutTime = "";
                string Employee_id = "";
                //string Father_Email_Id = "";
                for (int i = 0; i < grvExcelData.Rows.Count; i++)
                {
                    Emp_Code = grvExcelData.Rows[i].Cells[0].Text;
                    InTime = grvExcelData.Rows[i].Cells[2].Text;
                    OutTime = grvExcelData.Rows[i].Cells[3].Text;

                    #region check duplicate

                    DataTable dt = My.dataTable(" select Emp_Code,Employee_id from HR_Employee_Master where Emp_Code='" + Emp_Code + "'  ");//and Session_id='" + ddlsession.SelectedValue + "'
                    if (dt.Rows.Count == 0)
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;

                    }
                    else
                    {
                        Employee_id = dt.Rows[0]["Employee_id"].ToString();

                    }

                    try
                    {
                        DateTime startTime = DateTime.ParseExact(InTime, "hh:mm:ss tt", CultureInfo.InvariantCulture);

                    }
                    catch (Exception ex)
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    }
                    try
                    {
                        DateTime Endtime = DateTime.ParseExact(OutTime, "hh:mm:ss tt", CultureInfo.InvariantCulture);
                    }
                    catch (Exception ex)
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    }
                    #endregion
                    Emp_Code = grvExcelData.Rows[i].Cells[0].Text;
                    Date = grvExcelData.Rows[i].Cells[1].Text;
                    InTime = grvExcelData.Rows[i].Cells[2].Text;
                    OutTime = grvExcelData.Rows[i].Cells[3].Text;
                    if (Emp_Code == "&nbsp;")
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    }
                    if (Date == "&nbsp;")
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    }
                    if (InTime == "&nbsp;")
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    }
                    if (OutTime == "&nbsp;")
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    }


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
            try
            {
                string Emp_Code = "";
                string Date = "";
                string InTime = "";
                string OutTime = "";
                string Employee_id = "";

                for (int i = 0; i < grvExcelData.Rows.Count; i++)
                {
                    Emp_Code = grvExcelData.Rows[i].Cells[0].Text;
                    DataTable dt = My.dataTable(" select Emp_Code,Employee_id from HR_Employee_Master where Emp_Code='" + Emp_Code + "'  ");//and Session_id='" + ddlsession.SelectedValue + "'
                    if (dt.Rows.Count == 0)
                    {
                        Employee_id = "0";

                    }
                    else
                    {
                        Employee_id = dt.Rows[0]["Employee_id"].ToString();

                    }
                    Emp_Code = grvExcelData.Rows[i].Cells[0].Text;
                    Date = grvExcelData.Rows[i].Cells[1].Text;
                    InTime = grvExcelData.Rows[i].Cells[2].Text;
                    OutTime = grvExcelData.Rows[i].Cells[3].Text;


                    DateTime startTime = DateTime.ParseExact(InTime, "hh:mm:ss tt", CultureInfo.InvariantCulture);


                    DateTime Endtime = DateTime.ParseExact(OutTime, "hh:mm:ss tt", CultureInfo.InvariantCulture);



                    string merge_Time_start = Date + " " + startTime.ToString("hh:mm:ss tt");

                    string merge_Time_End = Date + " " + Endtime.ToString("hh:mm:ss tt");


                    if (Employee_id == "0")
                    {

                    }
                    else
                    {
                        DateTime start_date_time = DateTime.ParseExact(merge_Time_start, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                        DateTime end_date_time = DateTime.ParseExact(merge_Time_End, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                        insert_data_log(Emp_Code, Date, Employee_id, start_date_time);// in office

                        insert_data_log(Emp_Code, Date, Employee_id, end_date_time);// out office
                    }








                }

                btn_final_submit.Visible = false;
                bool abc = PayrollMy.update_HR_Daily_Attendance_Record("Manual Attendance");
                Alertme("Attendance has been uploaded successfully.", "success");

            }
            catch (Exception ex)
            {
                Alertme("Sorry Your data isn't correct format", "success");
                My.submitException(ex, "upload data excel");
            }
        }

        private void insert_data_log(string emp_Code, string date, string Employee_id, DateTime DateTime)
        {

            DataTable dt = My.dataTable(" select  * from dbo.[HR_Attendance_Log] where   Emp_Code = '" + emp_Code + "' and  DateTime = '" + DateTime.ToString("yyyy/MM/dd hh:mm:ss tt") + "' ");
            if (dt.Rows.Count == 0)
            {

                SqlCommand cmd;
                string query = "INSERT INTO HR_Attendance_Log (Employee_id,DateTime,Itime,Emp_Code,import_status,AttendanceSource) values (@Employee_id,@DateTime,@Itime,@Emp_Code,@import_status,@AttendanceSource)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Employee_id", Employee_id);
                cmd.Parameters.AddWithValue("@DateTime", DateTime);
                cmd.Parameters.AddWithValue("@Itime", DateTime.ToString("yyyyMMddHHmmss"));
                cmd.Parameters.AddWithValue("@Emp_Code", emp_Code);
                cmd.Parameters.AddWithValue("@import_status", "Pending");
                cmd.Parameters.AddWithValue("@AttendanceSource", "Manual Attendance");

                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd;

                string query = "update HR_Attendance_Log set Employee_id=@Employee_id,DateTime=@DateTime,Itime=@Itime,Emp_Code=@Emp_Code,import_status=@import_status where Id=@Id";



                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Employee_id", Employee_id);
                cmd.Parameters.AddWithValue("@DateTime", DateTime);
                cmd.Parameters.AddWithValue("@Itime", DateTime.ToString("yyyyMMddHHmmss"));
                cmd.Parameters.AddWithValue("@Emp_Code", emp_Code);
                cmd.Parameters.AddWithValue("@import_status", "Pending");
                cmd.Parameters.AddWithValue("@Id", id);
                if (My.InsertUpdateData(cmd))
                {

                }

            }
        }
    }
}