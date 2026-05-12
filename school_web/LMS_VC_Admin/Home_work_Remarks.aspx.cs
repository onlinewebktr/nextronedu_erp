using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Home_work_Remarks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["homeworkid"] != null)
                    {
                        hd_homeworkid_id.Value = Request.QueryString["homeworkid"];
                        hd_studentid.Value = Request.QueryString["studentid"];
                        fetch_req_detsils();
                    }
                    else
                    {
                        Response.Redirect("Dashboard.aspx");
                    }
                }
            }
            catch (Exception exe)
            {
            }
        }
        UsesCode mycode = new UsesCode();
        private void fetch_req_detsils()
        {
            string query = "Select Topic,Description,CompletingDate,Upload_Date,Subject,Class,Section,(select top 1 Subject_name from Subject_Master  where course_id=Homework_Details.Class and Subject_id=Homework_Details.Subject ) as CourseName from Homework_Details where Home_Work_id='" + hd_homeworkid_id.Value + "'";
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count.ToString() == "0")
            {

            }
            else
            {
                lbl_homeworktopic.Text = dt.Rows[0]["Topic"].ToString();
                lbl_description.Text = dt.Rows[0]["Description"].ToString();
                lbl_upload_date.Text = dt.Rows[0]["Upload_Date"].ToString();
                lbl_Completion_Date.Text = dt.Rows[0]["CompletingDate"].ToString();
                lbl_subject.Text = "Subject :" + dt.Rows[0]["CourseName"].ToString();
                Get_attachmet();
                get_student_details();
                get_student_reply();
            }
        }

        private void get_student_reply()
        {
            string query = "Select *,(select top 1 name from user_details where user_id=ReplayHomework.Teacher_id) as Nameteacher  from ReplayHomework where Homework_id='" + hd_homeworkid_id.Value + "' and Student_id='" + hd_studentid.Value + "'";
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count.ToString() == "0")
            {

            }
            else
            {
                lbl_replay.Text = dt.Rows[0]["Replay"].ToString();
                lbl_replaydate.Text = dt.Rows[0]["Date"].ToString();
                Get_reply_attachment(dt.Rows[0]["Rplay_id"].ToString());



                lbl_answer.Text = dt.Rows[0]["Remark"].ToString();
                lbl_replydate.Text = dt.Rows[0]["Remark_date"].ToString();
                lbl_teachername.Text = dt.Rows[0]["Nameteacher"].ToString();
                if (dt.Rows[0]["Status"].ToString() == "Replied")
                {
                    pnl_msg_sends.Visible = true;
                    pnl_closes.Visible = false;
                    admin.Visible = false;
                }
                else
                {
                    admin.Visible = true;
                    pnl_msg_sends.Visible = false;
                    pnl_closes.Visible = true;
                }
                if (dt.Rows[0]["Remarks_attacment"].ToString() == "")
                {
                    teacheraatcment.Visible = false;
                }
                else
                {
                    teacheraatcment.Visible = true;
                    teacheraatcment.HRef = dt.Rows[0]["Remarks_attacment"].ToString();
                }
            }
        }

        private void Get_reply_attachment(string Replayid)
        {
            string query = "Select * from ReplayHomework_multiple_image where Homework_id='" + hd_homeworkid_id.Value + "' and Replay_id='" + Replayid + "'";
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count.ToString() == "0")
            {
                grid_student_repaly.DataSource = null;

                grid_student_repaly.DataBind();
            }
            else
            {
                grid_student_repaly.DataSource = dt;

                grid_student_repaly.DataBind();
            }
        }

        private void get_student_details()
        {
            string query = "Select top 1 ar.studentname,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,class as classname ,ar.gcm_id from admission_registor ar where ar.admissionserialnumber='" + hd_studentid.Value + "' order by id desc";
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count.ToString() == "0")
            {

            }
            else
            {
                lbl_user_name.Text = "Student Name :" + dt.Rows[0]["studentname"].ToString();
                lbl_admission_no.Text = "Admission No. :" + dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_rollno.Text = "Roll No. :" + dt.Rows[0]["rollnumber"].ToString();
                lbl_class.Text = "Class :" + dt.Rows[0]["classname"].ToString();
                ViewState["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
            }
        }

        private void Get_attachmet()
        {
            string query = "Select * from HomeWork_Uploaded_Images where Homework_Id='" + hd_homeworkid_id.Value + "'";
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count.ToString() == "0")
            {
                Grdattache.DataSource = null;

                Grdattache.DataBind();
            }
            else
            {
                Grdattache.DataSource = dt;

                Grdattache.DataBind();
            }
        }

        protected void lnk_back_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Homework_replay.aspx?homework_id=" + hd_homeworkid_id.Value, false);
            }
            catch
            {
            }
        }

        protected void bntn_send_reply_Click(object sender, EventArgs e)
        {
            if (txt_message.Text == "")
            {
                lblmessage.Text = "Please enter your remarks  ";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {
                string ImagePath = GetImagePath();

                SqlCommand cmd = new SqlCommand("Update ReplayHomework set Remark=@Remark,Remark_date=@Remark_date,Remark_Idate=@Remark_Idate,is_Checked=@is_Checked,Status=@Status,Remarks_attacment=@Remarks_attacment where Homework_id = @Homework_id and Student_id=@Student_id");
                cmd.Parameters.AddWithValue("@Remark", txt_message.Text);
                cmd.Parameters.AddWithValue("@Remark_date", mycode.date());
                cmd.Parameters.AddWithValue("@Remark_Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@is_Checked", 1);
                cmd.Parameters.AddWithValue("@Status", "Checked");
                cmd.Parameters.AddWithValue("@Homework_id", hd_homeworkid_id.Value);
                cmd.Parameters.AddWithValue("@Student_id", hd_studentid.Value);
                cmd.Parameters.AddWithValue("@Remarks_attacment", ImagePath);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    lblmessage.Text = "Your remarks has been successfully submitted   ";
                    string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);


                    try
                    {
                        mycode.pushnotification("Your homework has been checked please check now ", "Homework checked", ViewState["gcm_id"].ToString(), hd_homeworkid_id.Value, hd_studentid.Value, "HomeworkcheckedByTeacher");
                    }
                    catch
                    {
                    }
                    fetch_req_detsils();

                }


            }
        }

        private string GetImagePath()
        {
            string Path = mycode.Upload_doc_images(fl_Photo, "/UploadedImage/Homework/");
            return Path;
        }
    }
}