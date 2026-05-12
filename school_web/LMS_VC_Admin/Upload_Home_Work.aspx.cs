using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;

using System.Data;
using System.IO;
namespace school_web.LMS_VC_Admin
{
    public partial class Upload_Home_Work : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
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
                    string AdminCode = Session["Admin"].ToString();
                    Dictionary<string, object> dc1 = My.get_push_credantial();
                    ViewState["type"] = (String)dc1["type"];
                    ViewState["project_id"] = (String)dc1["project_id"];
                    ViewState["private_key_id"] = (String)dc1["private_key_id"];
                    ViewState["client_email"] = (String)dc1["client_email"];
                    ViewState["client_id"] = (String)dc1["client_id"];
                    ViewState["private_key"] = dc1["private_key"].ToString().Replace("\\n", "\n");
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table where Branch_id='1' order by Position asc");
                    ViewState["sessionid"] = My.get_session_id();
                    if (Request.QueryString["Id"] != null)
                    {
                        hd_id.Value = Request.QueryString["Id"].ToString();
                        BindDetails();
                    }
                    else
                    {
                        hd_homeworkid.Value = UsesCode.tampid();
                    }
                }
            }
        }

        private void BindDetails()
        {
            BtnAdd.Text = "Update";
            DataTable dt = mycode.FillTable("select * from Homework_Details where Id='" + hd_id.Value + "'");
            ddl_CourseCat.SelectedValue = dt.Rows[0]["Class"].ToString();

            mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'   order by Section");

            ddl_section.SelectedValue = dt.Rows[0]["Section"].ToString();

            ddl_subject.SelectedValue = dt.Rows[0]["Subject"].ToString();
            txt_info.InnerText = dt.Rows[0]["Description"].ToString();
            txtTopic.Text = dt.Rows[0]["Topic"].ToString();


            mycode.bind_all_ddl_with_id(ddl_subject, " Select distinct Subject_name, Subject_id,Subject_position from Subject_Master where course_id = '" + ddl_CourseCat.SelectedValue + "' order by Subject_position");

            //mycode.bind_all_ddl_with_id(ddl_teacher, "Select distinct ip.Name,ip.UserID from InstructorProfile ip join TeacherCourseSubjectMaping tcs on ip.UserID=tcs.UserID where tcs.CategoryID='" + ddl_CourseCat.SelectedValue + "' and tcs.section='" + ddl_section.Text + "' and tcs.AssignCourseID='" + ddl_subject.SelectedValue + "' and Session_id='"+ ViewState["sessionid"].ToString() + "' order by Name asc");

            mycode.bind_all_ddl_with_id(ddl_teacher, "Select distinct ip.Name,ip.user_id from user_details ip join TeacherCourseSubjectMaping tcs on ip.user_id = tcs.UserID where tcs.CategoryID = '" + ddl_CourseCat.SelectedValue + "' and tcs.section = '" + ddl_section.Text + "' and tcs.AssignCourseID = '" + ddl_subject.SelectedValue + "' and tcs.Session_id = '" + ViewState["sessionid"].ToString() + "' order by ip.Name asc");

            ddl_teacher.SelectedValue = dt.Rows[0]["Upload_By"].ToString();
            Hd_Document.Value = dt.Rows[0]["Attachments"].ToString();
            hd_homeworkid.Value = dt.Rows[0]["Home_Work_id"].ToString();
            txt_date.Text = dt.Rows[0]["CompletingDate"].ToString();
            BindGridView();
        }

        private void Bind_attacment()
        {

        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else
            {
                mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "' and Session_id='" + ViewState["sessionid"].ToString() + "' order by Section");
                //mycode.bind_ddl(ddl_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and Istatus='1'  order by section");
            }
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_subject, " Select distinct Subject_name, Subject_id,Subject_position from Subject_Master where course_id = '" + ddl_CourseCat.SelectedValue + "' order by Subject_position");
            }
        }

        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alert("Please select subject");
            }
            else
            {
                //mycode.bind_all_ddl_with_id(ddl_teacher, "Select distinct ip.Name,ip.UserID from InstructorProfile ip join TeacherCourseSubjectMaping tcs on ip.UserID=tcs.UserID where tcs.CategoryID='" + ddl_CourseCat.SelectedValue + "' and tcs.section='" + ddl_section.Text + "' and tcs.AssignCourseID='" + ddl_subject.SelectedValue + "' and and tcs.Session_id='" + ViewState["sessionid"].ToString() + "' order by ip.Name asc");
                mycode.bind_all_ddl_with_id(ddl_teacher, "Select distinct ip.name,ip.user_id from user_details ip join TeacherCourseSubjectMaping tcs on ip.user_id=tcs.UserID where tcs.CategoryID='" + ddl_CourseCat.SelectedValue + "' and tcs.section='" + ddl_section.Text + "' and tcs.AssignCourseID='" + ddl_subject.SelectedValue + "' and tcs.Session_id='" + ViewState["sessionid"].ToString() + "' order by ip.name asc");
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");
                }
                else if (ddl_section.Text == "Select")
                {
                    Alert("Please select section");
                }
                else if (ddl_subject.SelectedItem.Text == "Select")
                {
                    Alert("Please select subject");
                }
                else if (ddl_teacher.SelectedItem.Text == "Select")
                {
                    Alert("Please select teacher name");
                }
                else if (txt_date.Text == "")
                {
                    Alert("Please enter Completion Date ");
                }
                else if (txtTopic.Text == "Select")
                {
                    Alert("Please enter topic ");
                }
                else if (txt_info.InnerText == "")
                {
                    Alert("Please enter work details ");
                }
                else if (hd_homeworkid.Value == "")
                {
                    Alert("Something is wrong please try again");
                }
                else
                {
                    if (BtnAdd.Text == "Submit")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Homework_Details (Class,Section,Subject,Topic,Description,CompletingDate,Upload_By,Upload_Date,Upload_Time,Upload_Idate,Attachments,Attachments2,Home_Work_id,Session_id,Branch) values (@Class,@Section,@Subject,@Topic,@Description,@CompletingDate,@Upload_By,@Upload_Date,@Upload_Time,@Upload_Idate,@Attachments,@Attachments2,@Home_Work_id,@Session_id,@Branch)");
                        cmd.Parameters.AddWithValue("@Class", ddl_CourseCat.SelectedValue);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Subject", ddl_subject.SelectedValue);
                        cmd.Parameters.AddWithValue("@Topic", txtTopic.Text);
                        cmd.Parameters.AddWithValue("@Description", txt_info.InnerText);
                        cmd.Parameters.AddWithValue("@CompletingDate", txt_date.Text);
                        cmd.Parameters.AddWithValue("@Upload_By", ddl_teacher.SelectedValue);
                        cmd.Parameters.AddWithValue("@Upload_Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Upload_Time", mycode.time());
                        cmd.Parameters.AddWithValue("@Upload_Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Attachments", "");
                        cmd.Parameters.AddWithValue("@Attachments2", "");
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                        cmd.Parameters.AddWithValue("@Branch", "1");
                        cmd.Parameters.AddWithValue("@Home_Work_id", hd_homeworkid.Value);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            mycode.executequery("update HomeWork_Uploaded_Images set Class_id='" + ddl_CourseCat.SelectedValue + "',Subject_Id='" + ddl_subject.SelectedValue + "',section='" + ddl_section.Text + "' where Homework_Id='" + hd_homeworkid.Value + "'");
                            try
                            {
                                mycode.send_push_to_student_study_material(ddl_CourseCat.SelectedValue, ddl_section.Text, ddl_subject.SelectedItem.Text, txtTopic.Text, ddl_subject.SelectedValue, "", ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());
                            }
                            catch
                            {
                            }
                            Alert("homework has been successfully added.");
                            txt_info.InnerText = "";
                            txtTopic.Text = "";
                            GrdViewimg.DataSource = null;
                            GrdViewimg.DataBind();
                            hd_homeworkid.Value = UsesCode.tampid();
                        }
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Update Homework_Details set Class=@Class,Section=@Section,Subject=@Subject,Topic=@Topic,Description=@Description,CompletingDate=@CompletingDate,Upload_By=@Upload_By,Upload_Date=@Upload_Date,Upload_Time=@Upload_Time,Upload_Idate=@Upload_Idate where Id = @Id");
                        cmd.Parameters.AddWithValue("@Class", ddl_CourseCat.SelectedValue);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Subject", ddl_subject.SelectedValue);
                        cmd.Parameters.AddWithValue("@Topic", txtTopic.Text);
                        cmd.Parameters.AddWithValue("@Description", txt_info.InnerText);
                        cmd.Parameters.AddWithValue("@CompletingDate", txt_date.Text);
                        cmd.Parameters.AddWithValue("@Upload_By", ddl_teacher.SelectedValue);
                        cmd.Parameters.AddWithValue("@Upload_Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Upload_Time", mycode.time());
                        cmd.Parameters.AddWithValue("@Upload_Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            mycode.executequery("update HomeWork_Uploaded_Images set Class_id='" + ddl_CourseCat.SelectedValue + "',Subject_Id='" + ddl_subject.SelectedValue + "',section='" + ddl_section.Text + "' where Homework_Id='" + hd_homeworkid.Value + "'");
                            Alert("homework has been successfully updated.");
                            txt_info.InnerText = "";
                            txtTopic.Text = "";
                            BtnAdd.Text = "Submit";
                            GrdViewimg.DataSource = null;
                            GrdViewimg.DataBind();
                            hd_homeworkid.Value = UsesCode.tampid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }
        private void send_push_to_student(string Class_id, string Class_name, string Section, string subject, string Topic)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from dbo.[admission_registor] where Class_id='" + Class_id + "' and Section='" + ddl_section.Text + "' ", connection.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string message = "Dear " + dr["Original_Name"].ToString() + " homework assign of subject : " + subject + " & Topic : " + Topic;

                    if (dr["gcm_id"] != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = message;
                        ss["title"] = "Homework Assignment";
                        ss["messagetype"] = "Homework";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = dr["admissionserialnumber"].ToString();
                        UsesCode.SendNotification(dr["gcm_id"].ToString(), ss);
                    }

                }

            }
        }
        protected void BtnDeleteimg_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "";
                string dir_path = "";
                Button lnk = (Button)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_imp = (Label)row.FindControl("lbl_imp"); 
                try
                {
                    string patha1 = lbl_imp.Text.Replace(mycode.geturl(), ""); 
                    path = (Server.MapPath("../")).ToString() + patha1;
                    dir_path = Path.GetDirectoryName(path);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch
                {
                }

                SqlCommand cmd = new SqlCommand("Delete from HomeWork_Uploaded_Images where Id='" + lbl_Id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                Alert("successfully deleted.");
                BindGridView(); 
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void btn_uploadimg_Click(object sender, EventArgs e)
        {
            try
            {
                if (hd_homeworkid.Value == "")
                {
                    Alert("Something is wrong please try again");
                }
                else
                {
                    SqlCommand cmd;
                    string upload_student_pic = upload_img();
                    if (upload_student_pic == "")
                    {
                        Alert("Please choose file");
                    }
                    else
                    {

                        string strQuery = @"INSERT INTO HomeWork_Uploaded_Images (Images,Updated_Date,Updated_Idate,Uploaded_by,Homework_Id) values (@Images,@Updated_Date,@Updated_Idate,@Uploaded_by,@Homework_Id)";
                        cmd = new SqlCommand(strQuery);
                        cmd.Parameters.AddWithValue("@Images", upload_student_pic);
                        cmd.Parameters.AddWithValue("@Updated_Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Updated_Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Uploaded_by", ddl_teacher.SelectedValue);
                        cmd.Parameters.AddWithValue("@Homework_Id", hd_homeworkid.Value);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            BindGridView(); 
                        } 
                    }
                }
            }
            catch
            {
            } 
        }

        private void BindGridView()
        {
            DataTable dt = mycode.FillTable("select * from HomeWork_Uploaded_Images where Homework_Id='" + hd_homeworkid.Value + "'");
            if (dt.Rows.Count == 0)
            {

                GrdViewimg.DataSource = null;
                GrdViewimg.EmptyDataText = "Data Not Available";
                GrdViewimg.DataBind();
            }
            else
            {
                //gridview.Visible = true;
                GrdViewimg.DataSource = dt;
                GrdViewimg.DataBind();
            }
        }
        private string upload_img()
        {


            string file_name = "";
            int tempo;
            Random random = new Random(DateTime.Now.Millisecond);


            tempo = random.Next(1, 100);
            file_name = mycode.idate() + mycode.itime() + tempo;



            string file_path = "";
            try
            {
                file_path = "";
                if (fl_Document.PostedFile.ContentLength > 0)
                {
                    if (fl_Document.FileBytes.Length <= 10000000)
                    {
                        string extension = System.IO.Path.GetExtension(fl_Document.PostedFile.FileName);
                        string FileName1 = file_name + extension;
                        HttpPostedFile postedf = fl_Document.PostedFile;
                        postedf.SaveAs(MapPath("../images/uploaded/" + FileName1));

                        string fileName = Path.GetFileName(FileName1);
                        string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                        string[] New_originalPath1 = originalPath2.Split('?');
                        string originalPath1 = New_originalPath1[0].ToString();
                        file_path = originalPath1 + "/images/uploaded/" + FileName1;
                    }



                }
                return file_path;
            }
            catch (Exception ex)
            {
                UsesCode.submitexception1(ex);
                return file_path;
            }
        }
    }
}