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

namespace school_web.Student_Profile
{
    public partial class apply_for_leave : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        try
                        {
                            get_student_info();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
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

        private void get_student_info()
        {
            DataTable dt = mycode.FillData("Select * from admission_registor where Session_id=" + ViewState["sesssionid"].ToString() + " and admissionserialnumber='" + ViewState["regid"].ToString() + "'");
            if (dt.Rows.Count != 0)
            {
                txt_name.Text = dt.Rows[0]["studentname"].ToString();
                txt_class_name.Text = dt.Rows[0]["class"].ToString();
                txt_section.Text = dt.Rows[0]["Section"].ToString();
                txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                txt_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
            }
        }

        protected void txt_from_date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_from_date.Text != "" && txt_to_date.Text != "")
                {
                    DateTime startdate1q = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime enddate1q = DateTime.ParseExact(txt_to_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                    int totaldays = My.toint(diff.Days);
                    int ttl_leave_day = totaldays + 1;
                    txt_ttl_leave.Text = ttl_leave_day.ToString();
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
                if (ViewState["regid"] == null)
                {
                    Alertme("Something went wrong. Please try again some time later.", "warning");
                }
                else if (txt_from_date.Text == "")
                {
                    Alertme("Please choose from date.", "warning");
                    txt_from_date.Focus();
                }
                else if (txt_to_date.Text == "")
                {
                    Alertme("Please choose to date.", "warning");
                    txt_to_date.Focus();
                }
                else if (txt_leave_subject.Text == "")
                {
                    Alertme("Please enter subject of leave.", "warning");
                    txt_leave_subject.Focus();
                }
                else if (txt_leave_details.Text == "")
                {
                    Alertme("Please enter reason of leave in details.", "warning");
                    txt_leave_details.Focus();
                }
                else
                {
                    string filePath = "";
                    if (FileUpload1.HasFile)
                    {
                        filePath = Upload_doc_images(FileUpload1, "/UploadedImage/Leave/");
                    }
                    save_leave_details(filePath);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_leave_details(string filePath)
        {
            SqlCommand cmd;
            string query = "INSERT INTO Student_Leave_Apply (Name,Class_Name,Section,Roll_No,Leave_From,Leave_To,Cause,Details,Apply_Date,Apply_Idate,Status,Image,Total_day_count,Leave_from_idate,Leave_to_idate,Admission_no,Session_id) values (@Name,@Class_Name,@Section,@Roll_No,@Leave_From,@Leave_To,@Cause,@Details,@Apply_Date,@Apply_Idate,@Status,@Image,@Total_day_count,@Leave_from_idate,@Leave_to_idate,@Admission_no,@Session_id)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Name", txt_name.Text);
            cmd.Parameters.AddWithValue("@Class_Name", txt_class_name.Text);
            cmd.Parameters.AddWithValue("@Section", txt_section.Text);
            cmd.Parameters.AddWithValue("@Roll_No", txt_roll_no.Text);
            cmd.Parameters.AddWithValue("@Leave_From", txt_from_date.Text);
            cmd.Parameters.AddWithValue("@Leave_To", txt_to_date.Text);
            cmd.Parameters.AddWithValue("@Cause", txt_leave_subject.Text);
            cmd.Parameters.AddWithValue("@Details", txt_leave_details.Text);
            cmd.Parameters.AddWithValue("@Apply_Date", mycode.date());
            cmd.Parameters.AddWithValue("@Apply_Idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@Image", filePath);
            cmd.Parameters.AddWithValue("@Total_day_count", txt_ttl_leave.Text);
            cmd.Parameters.AddWithValue("@Leave_from_idate", My.DateConvertToIdate(txt_from_date.Text));
            cmd.Parameters.AddWithValue("@Leave_to_idate", My.DateConvertToIdate(txt_to_date.Text));
            cmd.Parameters.AddWithValue("@Admission_no", ViewState["regid"].ToString());
            cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
            if (My.InsertUpdateData(cmd))
            {
                txt_from_date.Text = "";
                txt_to_date.Text = "";
                txt_leave_subject.Text = "";
                txt_leave_details.Text = "";
                txt_ttl_leave.Text = "";
                Alertme("Your leave application has been successfully submitted for approval.", "success");
            }
        }


        public string Upload_doc_images(FileUpload fileName, string FolderPath)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fileName.HasFile)
            {
                if (fileName.FileBytes.Length < 5000000)
                {
                    string FileExtension = Path.GetExtension(fileName.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".pdf", ".PDF", ".doc", ".docx", ".ppt", ".jpg", ".jpeg", ".png", ".gif" };
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
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {
                        string ServerPath = HttpContext.Current.Server.MapPath("~/" + FolderPath);
                        if (File.Exists(ServerPath))
                        {
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(ServerPath);

                        }
                        fileName.SaveAs(ServerPath + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                    string[] New_originalPath1 = originalPath2.Split('?');
                    string originalPath1 = New_originalPath1[0].ToString(); 
                    ImagePath = originalPath1 + FolderPath + Path.GetFileName(rename);
                }
            }
            return ImagePath;
        }
    }
}