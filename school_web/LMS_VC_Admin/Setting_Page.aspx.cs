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
    public partial class Setting_Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind_data();
            }
        }
        UsesCode mycode = new UsesCode();
        private void bind_data()
        {
            string query = "Select * from Comapny_Profile";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {
                ViewState["logo"] = "";
                btn_Submit.Text = "Add";
                txt_schoolname.Text = "";
                txt_footer_copyright.Text = "";
                txt_apikey.Text = "";
                txt_push_sendid.Text = "";
                txt_smsapikey.Text = "";
                txt_sms_senderkey.Text = "";

                chk_class.Checked = false;
                chk_subject.Checked = false;
                chk_teacher.Checked = false;
                chk_student.Checked = false;
                chk_financial.Checked = false;
                Get_sms();
            }
            else
            {
                logo.Src = dtTemp.Rows[0]["Logo"].ToString();
                ViewState["id"] = dtTemp.Rows[0]["Id"].ToString();
                ViewState["logo"] = dtTemp.Rows[0]["Logo"].ToString();
                txt_schoolname.Text = dtTemp.Rows[0]["Name_of_company"].ToString();
                txt_footer_copyright.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();
                txt_apikey.Text = dtTemp.Rows[0]["SERVER_API_KEY"].ToString();
                txt_push_sendid.Text = dtTemp.Rows[0]["SENDER_ID"].ToString();
                txt_smsapikey.Text = "";
                txt_sms_senderkey.Text = "";
                try
                {
                    if (Convert.ToBoolean(dtTemp.Rows[0]["Is_Teacher_Add"].ToString()) == true)
                    {
                        chk_teacher.Checked = true;
                    }
                    else
                    {
                        chk_teacher.Checked = false;
                    }
                    if (Convert.ToBoolean(dtTemp.Rows[0]["Is_Student_Add"].ToString()) == true)
                    {
                        chk_student.Checked = true;
                    }
                    else
                    {
                        chk_student.Checked = false;
                    }

                    if (Convert.ToBoolean(dtTemp.Rows[0]["Is_Add_Class"].ToString()) == true)
                    {
                        chk_class.Checked = true;
                    }
                    else
                    {
                        chk_class.Checked = false;
                    }
                    if (Convert.ToBoolean(dtTemp.Rows[0]["Is_Add_Subject"].ToString()) == true)
                    {
                        chk_subject.Checked = true;
                    }
                    else
                    {
                        chk_subject.Checked = false;
                    }

                    if (Convert.ToBoolean(dtTemp.Rows[0]["Is_Financial_Report"].ToString()) == true)
                    {
                        chk_financial.Checked = true;
                    }
                    else
                    {
                        chk_financial.Checked = false;
                    }
                }
                catch
                {
                }

                txt_studentsms.Text = dtTemp.Rows[0]["Send_student_userid_and_pwd_with_apk_link"].ToString();
                txt_teachersms.Text = dtTemp.Rows[0]["Send_teacher_userid_and_pwd_with_apk_link"].ToString();
                Get_sms();

                btn_Submit.Text = "Update";
            }
        }

        private void Get_sms()
        {
            string query = "Select * from message_config";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {

            }
            else
            {
                txt_smsapikey.Text = dtTemp.Rows[0]["uid"].ToString();
                txt_sms_senderkey.Text = dtTemp.Rows[0]["sender"].ToString();
            }
        }
        string scrpt;
        protected void btn_Submit_Click(object sender, EventArgs e)
        {


            if (btn_Submit.Text == "Add")
            {
                string imagePath = Getimage();
                SqlCommand cmd;
                string query = "INSERT INTO Comapny_Profile (Name_of_company,Address,Logo,Footer_Copy_Right,Footer_URL_Link,SERVER_API_KEY,SENDER_ID,Send_student_userid_and_pwd_with_apk_link,Send_teacher_userid_and_pwd_with_apk_link,Is_Teacher_Add,Is_Student_Add,Is_Add_Class,Is_Add_Subject,Is_Financial_Report) values (@Name_of_company,@Address,@Logo,@Footer_Copy_Right,@Footer_URL_Link,@SERVER_API_KEY,@SENDER_ID,@Send_student_userid_and_pwd_with_apk_link,@Send_teacher_userid_and_pwd_with_apk_link,@Is_Teacher_Add,@Is_Student_Add,@Is_Add_Class,@Is_Add_Subject,@Is_Financial_Report)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Name_of_company", txt_schoolname.Text);
                cmd.Parameters.AddWithValue("@Address", "");
                cmd.Parameters.AddWithValue("@Logo", imagePath);
                cmd.Parameters.AddWithValue("@Footer_Copy_Right", txt_footer_copyright.Text);
                cmd.Parameters.AddWithValue("@Footer_URL_Link", "");
                cmd.Parameters.AddWithValue("@SERVER_API_KEY", txt_apikey.Text);
                cmd.Parameters.AddWithValue("@SENDER_ID", txt_push_sendid.Text);
                cmd.Parameters.AddWithValue("@Send_student_userid_and_pwd_with_apk_link", txt_studentsms.Text);
                cmd.Parameters.AddWithValue("@Send_teacher_userid_and_pwd_with_apk_link", txt_teachersms.Text);
                if (chk_teacher.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Teacher_Add", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Teacher_Add", 0);
                }
                if (chk_teacher.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Student_Add", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Student_Add", 0);
                }
                if (chk_class.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Add_Class", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Add_Class", 0);
                }
                if (chk_subject.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Add_Subject", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Add_Subject",0);
                }
                if (chk_financial.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Financial_Report", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Financial_Report", 0);
                }

               
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    mycode.executequery("update Global set client_logo='" + imagePath + "'");
                    string query1 = "Select * from message_config ";
                    SqlCommand cmd1 = new SqlCommand(query1);
                    DataTable dtTemp = UsesCode.GetData(cmd1);
                    if (dtTemp.Rows.Count == 0)
                    {
                        SqlCommand cmd11;
                        query1 = "INSERT INTO message_config (uid,sender,route,domain) values (@uid,@sender,@route,@domain)";
                        cmd11 = new SqlCommand(query1);
                        cmd11.Parameters.AddWithValue("@uid", txt_smsapikey.Text);
                        cmd11.Parameters.AddWithValue("@sender", txt_sms_senderkey.Text);
                        cmd11.Parameters.AddWithValue("@route", "1");
                        cmd11.Parameters.AddWithValue("@domain", "mysms.msgclub.net");
                        if (InsertUpdate.InsertUpdateData(cmd11))
                        {
                        }
                    }
                    else
                    {
                        SqlCommand cmd11;
                        query1 = "Update message_config set uid=@uid,sender=@sender where Id = @Id";
                        cmd11 = new SqlCommand(query1);
                        cmd11.Parameters.AddWithValue("@uid", txt_smsapikey.Text);
                        cmd11.Parameters.AddWithValue("@sender", txt_sms_senderkey.Text);
                        cmd11.Parameters.AddWithValue("@Id", dtTemp.Rows[0]["Id"].ToString());
                        if (InsertUpdate.InsertUpdateData(cmd11))
                        {
                        }
                    }
                    lblmessage.Text = "School information has been added sucessfully";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }

            }
            else
            {

                string imagePath = Getimage();
                if (imagePath == "")
                {
                    
                }
                else
                {
                    string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                    string[] New_originalPath1 = originalPath2.Split('?');
                    string originalPath1 = New_originalPath1[0].ToString();
                    if (originalPath1 == imagePath)
                    {
                    }
                    else
                    {
                        ViewState["logo"] = imagePath;
                    }
                }
                SqlCommand cmd;
                string query = "Update Comapny_Profile set Name_of_company=@Name_of_company,Address=@Address,Logo=@Logo,Footer_Copy_Right=@Footer_Copy_Right,Footer_URL_Link=@Footer_URL_Link,SERVER_API_KEY=@SERVER_API_KEY,SENDER_ID=@SENDER_ID,Send_student_userid_and_pwd_with_apk_link=@Send_student_userid_and_pwd_with_apk_link,Send_teacher_userid_and_pwd_with_apk_link=@Send_teacher_userid_and_pwd_with_apk_link,Is_Teacher_Add=@Is_Teacher_Add,Is_Student_Add=@Is_Student_Add,Is_Add_Class=@Is_Add_Class,Is_Add_Subject=@Is_Add_Subject,Is_Financial_Report=@Is_Financial_Report where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
                cmd.Parameters.AddWithValue("@Name_of_company", txt_schoolname.Text);
                cmd.Parameters.AddWithValue("@Address", "");
                cmd.Parameters.AddWithValue("@Logo", ViewState["logo"].ToString());
                cmd.Parameters.AddWithValue("@Footer_Copy_Right", txt_footer_copyright.Text);
                cmd.Parameters.AddWithValue("@Footer_URL_Link", "");
                cmd.Parameters.AddWithValue("@SERVER_API_KEY", txt_apikey.Text);
                cmd.Parameters.AddWithValue("@SENDER_ID", txt_push_sendid.Text);
                cmd.Parameters.AddWithValue("@Send_student_userid_and_pwd_with_apk_link", txt_studentsms.Text);
                cmd.Parameters.AddWithValue("@Send_teacher_userid_and_pwd_with_apk_link", txt_teachersms.Text);
                 

                if (chk_teacher.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Teacher_Add", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Teacher_Add", 0);
                }
                if (chk_teacher.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Student_Add", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Student_Add", 0);
                }
                if (chk_class.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Add_Class", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Add_Class", 0);
                }
                if (chk_subject.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Add_Subject", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Add_Subject", 0);
                }
                if (chk_financial.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Financial_Report", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Financial_Report", 0);
                }
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    mycode.executequery("update Global set client_logo='" + imagePath + "'");
                    string query1 = "Select * from message_config ";
                    SqlCommand cmd1 = new SqlCommand(query1);
                    DataTable dtTemp = UsesCode.GetData(cmd1);
                    if (dtTemp.Rows.Count == 0)
                    {
                        SqlCommand cmd11;
                        query1 = "INSERT INTO message_config (uid,sender,route,domain) values (@uid,@sender,@route,@domain)";
                        cmd11 = new SqlCommand(query1);
                        cmd11.Parameters.AddWithValue("@uid", txt_smsapikey.Text);
                        cmd11.Parameters.AddWithValue("@sender", txt_sms_senderkey.Text);
                        cmd11.Parameters.AddWithValue("@route", "1");
                        cmd11.Parameters.AddWithValue("@domain", "mysms.msgclub.net");
                        if (InsertUpdate.InsertUpdateData(cmd11))
                        {
                        }
                    }
                    else
                    {
                        SqlCommand cmd11;
                        query1 = "Update message_config set uid=@uid,sender=@sender where Id = @Id";
                        cmd11 = new SqlCommand(query1);
                        cmd11.Parameters.AddWithValue("@uid", txt_smsapikey.Text);
                        cmd11.Parameters.AddWithValue("@sender", txt_sms_senderkey.Text);
                        cmd11.Parameters.AddWithValue("@Id", dtTemp.Rows[0]["Id"].ToString());
                        if (InsertUpdate.InsertUpdateData(cmd11))
                        {
                        }
                    }

                    lblmessage.Text = "School information has been updated sucessfully";
                   
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }

            }
            bind_data();
        }

        private string Getimage()
        {
            string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
            string[] New_originalPath1 = originalPath2.Split('?');
            string originalPath1 = New_originalPath1[0].ToString();

            string Path = originalPath1 + mycode.UploadImage(FileUpload1, "/UploadedImage/E_Book_images_pdf/");//code.UploadAudio(fl_Audio, "/UploadedImage/AudioFile/");
            return Path;
        }
    }
}