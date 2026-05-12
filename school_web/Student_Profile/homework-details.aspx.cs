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

namespace school_web.Student_Profile
{
    public partial class homework_details : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["topicid"] != null)
                {
                    ViewState["topic_id"] = Request.QueryString["topicid"];
                    fetch_topic_details(ViewState["topic_id"].ToString());
                    fetch_topic_graphic(ViewState["topic_id"].ToString());
                    get_submitted_answer();
                    get_firm();
                }
                else
                {
                    Response.Redirect("home.aspx", false);
                }
            }
        }
        private void get_firm()
        {
            try
            {
                DataTable dt = mycode.FillData(" Select * from Firm_Details ");
                if (dt.Rows.Count == 0)
                {
                    ViewState["IsReplyHomework"] = "1";
                }
                else
                {
                    ViewState["IsReplyHomework"] = dt.Rows[0]["IsReplyHomework"].ToString();

                }

            }
            catch
            {
                ViewState["IsAskdoubt"] = "1";

            }

        }
        private void get_submitted_answer()
        {
            DataTable dt = mycode.FillData("select * from ReplayHomework where Student_id='" + Session["User"].ToString() + "' and Homework_id='" + ViewState["topic_id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                ViewState["ReplyID"] = get_reply_id();
                upimages1.Visible = true;
                upimages1.Visible = true;
                exppplbl.Visible = true;
                exppplbl1.Visible = false;
                btn_submit_homework.Visible = true;
                statusDV.Visible = false;
            }
            else
            {
                txt_answer.ReadOnly = true;
                btn_add_image.Visible = false;
                lnk_reply.Visible = false;
                replyDV.Visible = true;
                ViewState["ReplyID"] = dt.Rows[0]["Rplay_id"].ToString();
                upimages1.Visible = false;
                upimages1.Visible = false;
                exppplbl.Visible = false;
                exppplbl1.Visible = true;
                btn_submit_homework.Visible = false;
                statusDV.Visible = true;
                lbl_status.Text = dt.Rows[0]["Status"].ToString();
                txt_answer.Text = dt.Rows[0]["Replay"].ToString();
                fetch_saved_file();


                if (dt.Rows[0]["is_Checked"].ToString() == "True")
                {
                    teacherRemarksdV.Visible = true;
                    lbl_checked_on.Text = dt.Rows[0]["Remark_date"].ToString();
                    teacher_rmrks.InnerText = dt.Rows[0]["Remark"].ToString();
                    fetch_remrk_attechemnt();
                }
            }
        }

        private void fetch_remrk_attechemnt()
        {
            DataTable dt = mycode.FillData("select Image from Remark_multiple_image where Homework_Id='" + ViewState["topic_id"].ToString() + "' and Replay_home_work_Id='" + ViewState["ReplyID"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                TrmrkDV.Visible = false;
                rp_checked_att.DataSource = null;
                rp_checked_att.DataBind();
            }
            else
            {
                TrmrkDV.Visible = true;
                rp_checked_att.DataSource = dt;
                rp_checked_att.DataBind();
            }
        }

        private object get_reply_id()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            string a = r.Next(2478, 9758) +
              DateTime.Now.Year.ToString("0000") +
              DateTime.Now.Day.ToString("00") +
              DateTime.Now.Month.ToString("00") +
              DateTime.Now.Hour.ToString("00") +
              DateTime.Now.Minute.ToString("00") +
              DateTime.Now.Second.ToString("00") +
              DateTime.Now.Millisecond.ToString("00") + r.Next(788, 999);
            return a;
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

        private void fetch_topic_graphic(string topic_id)
        {
            DataTable dt = mycode.FillData("select Images from HomeWork_Uploaded_Images where Homework_Id='" + topic_id + "'");
            if (dt.Rows.Count == 0)
            {
                graphicS.Visible = false;
                rd_graphics.DataSource = null;
                rd_graphics.DataBind();
            }
            else
            {
                graphicS.Visible = true;
                rd_graphics.DataSource = dt;
                rd_graphics.DataBind();
            }
        }


        private void fetch_topic_details(string topic_id)
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Subject_name from Subject_Master where Subject_id=Homework_Details.Subject) as Subject_name from Homework_Details where Home_Work_id='" + topic_id + "'");
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("home.aspx", false);
            }
            else
            {
                topic_p.InnerText = dt.Rows[0]["Topic"].ToString();
                topic_desc.InnerHtml = dt.Rows[0]["Description"].ToString();
                SubjectName.InnerText = dt.Rows[0]["Subject_name"].ToString();

                ViewState["teacherId"] = dt.Rows[0]["Upload_By"].ToString();
                ViewState["subjectId"] = dt.Rows[0]["Subject"].ToString();
                ViewState["classId"] = dt.Rows[0]["Class"].ToString();
                ViewState["Section"] = dt.Rows[0]["Section"].ToString();
            }
        }

        protected void lnk_reply_Click(object sender, EventArgs e)
        {
            lnk_reply.Visible = false;
            replyDV.Visible = true;
        }

        protected void btn_submit_homework_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_add_image_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IsReplyHomework"].ToString()=="1")
                {
                    string filePath = "";
                    if (FileUpload1.HasFile)
                    {
                        filePath = Upload_doc_images(FileUpload1, "/UploadedImage/Homework/");
                        if (filePath == "")
                        {
                            Alertme("Please choose your answer.", "warning");
                            return;
                        }
                        save_answer_file(filePath);
                    }
                    else
                    {
                        Alertme("Please choose your answer.", "warning");
                    }
                }
                else
                {
                     
                    Alertme("This feature is not enabled", "warning");
                   

                }
             
            }
            catch (Exception ex)
            {
            }
        }

        private void save_answer_file(string filePath)
        {

            SqlCommand cmd;
            string query = "INSERT INTO ReplayHomework_multiple_image (Homework_id,Replay_id,Image_replay,Date,Idate) values (@Homework_id,@Replay_id,@Image_replay,@Date,@Idate)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Homework_id", ViewState["topic_id"].ToString());
            cmd.Parameters.AddWithValue("@Replay_id", ViewState["ReplyID"].ToString());
            cmd.Parameters.AddWithValue("@Image_replay", filePath);
            cmd.Parameters.AddWithValue("@Date", mycode.date());
            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Record has been saved successfully.", "success");
                fetch_saved_file();
            }
        }

        private void fetch_saved_file()
        {
            DataTable dt = mycode.FillData("select * from ReplayHomework_multiple_image where Homework_id='" + ViewState["topic_id"].ToString() + "' and Replay_id='" + ViewState["ReplyID"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                img_upbyyou.Visible = false;
                pdfsDV.Visible = false;
                rp_pdfs.DataSource = null;
                rp_pdfs.DataBind();
            }
            else
            {
                img_upbyyou.Visible = true;
                pdfsDV.Visible = true;
                rp_pdfs.DataSource = dt;
                rp_pdfs.DataBind();
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
        protected void btn_submit_homework_Click1(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IsReplyHomework"].ToString() == "1")
                {
                    SqlCommand cmd;
                    string query = "insert into ReplayHomework(Homework_id,Student_id,Teacher_id,Replay,Rplay_id,Subject_id,Date,Idate,Status,Class_id,Section) values (@Homework_id,@Student_id,@Teacher_id,@Replay,@Rplay_id,@Subject_id,@Date,@Idate,@Status,@Class_id,@Section);";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Homework_id", ViewState["topic_id"].ToString());
                    cmd.Parameters.AddWithValue("@Student_id", Session["User"].ToString());
                    cmd.Parameters.AddWithValue("@Teacher_id", ViewState["teacherId"].ToString());
                    cmd.Parameters.AddWithValue("@Replay", txt_answer.Text);
                    cmd.Parameters.AddWithValue("@Rplay_id", ViewState["ReplyID"].ToString());
                    cmd.Parameters.AddWithValue("@Subject_id", ViewState["subjectId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                    cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Status", "Replied");
                    cmd.Parameters.AddWithValue("@Class_id", ViewState["classId"].ToString());
                    cmd.Parameters.AddWithValue("@Section", ViewState["Section"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Your answer has been submitted successfully.", "success");
                        get_submitted_answer();
                    }
                }
                else
                {
                    Alertme("This feature is not enabled", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}