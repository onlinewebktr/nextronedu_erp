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
namespace school_web.InstructorProfile
{
    public partial class Upload_Home_Work : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        My my = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teacher"] == null)
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
                    try
                    {
                        const string quote = "\"";
                        string tinyMC = My.get_single_column_data("select TinyMC_link as Column_Name from Firm_Details");
                        if (tinyMC != "")
                        {
                            lt_meata.Text = "<script src=" + quote + tinyMC + quote + " referrerpolicy=" + quote + "origin" + quote + " ></script>";
                        }
                        else
                        {
                            lt_meata.Text = "<script src=" + quote + "https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/7/tinymce.min.js" + quote + " referrerpolicy=" + quote + "origin" + quote + "></script>";
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    Dictionary<string, object> dc1 = My.get_push_credantial();
                    ViewState["type"] = (String)dc1["type"];
                    ViewState["project_id"] = (String)dc1["project_id"];
                    ViewState["private_key_id"] = (String)dc1["private_key_id"];
                    ViewState["client_email"] = (String)dc1["client_email"];
                    ViewState["client_id"] = (String)dc1["client_id"];
                    ViewState["private_key"] = dc1["private_key"].ToString().Replace("\\n", "\n");

                    ViewState["sesssionid"] = My.get_session_id();
                    ViewState["Branchid"] = mycode.getbranchid(Session["teacher"].ToString());
                    ViewState["teacher"] = Session["teacher"].ToString();
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by Position asc");
                    mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and    UserID='" + ViewState["teacher"].ToString() + "'  order by section");

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

            mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_CourseCat.SelectedValue + "'   and   UserID='" + ViewState["teacher"].ToString() + "'   order by section");

            ddl_section.SelectedValue = dt.Rows[0]["Section"].ToString();

            ddl_subject.SelectedValue = dt.Rows[0]["Subject"].ToString();
            txt_info.InnerText = dt.Rows[0]["Description"].ToString();
            txtTopic.Text = dt.Rows[0]["Topic"].ToString();


            mycode.bind_all_ddl_with_id(ddl_subject, "Select sm.Subject_name,sm.Subject_id from TeacherCourseSubjectMaping tcsm join  Subject_Master sm on tcsm.CategoryID=sm.course_id and tcsm.AssignCourseID=sm.Subject_id where tcsm.UserID='" + ViewState["teacher"].ToString() + "' and tcsm.CategoryID='" + ddl_CourseCat.SelectedValue + "' and tcsm.section='" + ddl_section.Text + "' and tcsm.Session_id='" + ViewState["sesssionid"].ToString() + "'   order by sm.Subject_position  ");



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
                mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and    UserID='" + ViewState["teacher"].ToString() + "'  order by section");
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



                mycode.bind_all_ddl_with_id(ddl_subject, "Select sm.Subject_name,sm.Subject_id from TeacherCourseSubjectMaping tcsm join  Subject_Master sm on tcsm.CategoryID=sm.course_id and tcsm.AssignCourseID=sm.Subject_id where tcsm.UserID='" + ViewState["teacher"].ToString() + "' and tcsm.CategoryID='" + ddl_CourseCat.SelectedValue + "' and tcsm.section='" + ddl_section.Text + "' and tcsm.Session_id='" + ViewState["sesssionid"].ToString() + "'   order by sm.Subject_position  ");
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
                        if (mycode.IsExist("Select * from Homework_Details where Class='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Subject='" + ddl_subject.SelectedValue + "' and Topic='" + txtTopic.Text.Replace("'", " ") + "' and  Upload_By='" + ViewState["teacher"].ToString() + "' and Session_id='" + ViewState["sesssionid"].ToString() + "'"))
                        {


                            SqlCommand cmd = new SqlCommand("INSERT INTO Homework_Details (Class,Section,Subject,Topic,Description,CompletingDate,Upload_By,Upload_Date,Upload_Time,Upload_Idate,Attachments,Attachments2,Home_Work_id,Session_id,Branch) values (@Class,@Section,@Subject,@Topic,@Description,@CompletingDate,@Upload_By,@Upload_Date,@Upload_Time,@Upload_Idate,@Attachments,@Attachments2,@Home_Work_id,@Session_id,@Branch)");

                            cmd.Parameters.AddWithValue("@Class", ddl_CourseCat.SelectedValue);
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            cmd.Parameters.AddWithValue("@Subject", ddl_subject.SelectedValue);
                            cmd.Parameters.AddWithValue("@Topic", txtTopic.Text);
                            cmd.Parameters.AddWithValue("@Description", txt_info.InnerText);
                            cmd.Parameters.AddWithValue("@CompletingDate", txt_date.Text);
                            cmd.Parameters.AddWithValue("@Upload_By", ViewState["teacher"].ToString());
                            cmd.Parameters.AddWithValue("@Upload_Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Upload_Time", mycode.time());
                            cmd.Parameters.AddWithValue("@Upload_Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@Attachments", "");
                            cmd.Parameters.AddWithValue("@Attachments2", "");
                            cmd.Parameters.AddWithValue("@Home_Work_id", hd_homeworkid.Value);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                            cmd.Parameters.AddWithValue("@Branch", ViewState["Branchid"].ToString());
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                mycode.executequery("update HomeWork_Uploaded_Images set Class_id='" + ddl_CourseCat.SelectedValue + "',Subject_Id='" + ddl_subject.SelectedValue + "',section='" + ddl_section.Text + "' where Homework_Id='" + hd_homeworkid.Value + "'");
                                hd_homeworkid.Value = UsesCode.tampid();

                                try

                                {
                                    mycode.send_push_to_student_homework(ddl_CourseCat.SelectedValue, ddl_section.Text, ddl_subject.SelectedItem.Text, txtTopic.Text, ddl_subject.SelectedValue, ViewState["sesssionid"].ToString(), ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());
                                }
                                catch
                                {

                                }



                                Alert("homework has been successfully added.");
                                txt_info.InnerText = "";
                                txtTopic.Text = "";

                                GrdViewimg.DataSource = null;

                                GrdViewimg.DataBind();


                            }


                        }
                        else { Alert("This homework detail is already uploaded."); }
                    }
                    else
                    {
                        if (mycode.IsExist("Select * from Homework_Details where Class='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Subject='" + ddl_subject.SelectedValue + "' and Topic='" + txtTopic.Text.Replace("'", " ") + "' and Upload_By='" + ViewState["teacher"].ToString() + "' and   Session_id='" + ViewState["sesssionid"].ToString() + "' and Id!='" + hd_id.Value + "'"))
                        {
                            SqlCommand cmd = new SqlCommand("Update Homework_Details set Class=@Class,Section=@Section,Subject=@Subject,Topic=@Topic,Description=@Description,CompletingDate=@CompletingDate,Upload_By=@Upload_By,Upload_Date=@Upload_Date,Upload_Time=@Upload_Time,Upload_Idate=@Upload_Idate where Id = @Id");

                            cmd.Parameters.AddWithValue("@Class", ddl_CourseCat.SelectedValue);
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            cmd.Parameters.AddWithValue("@Subject", ddl_subject.SelectedValue);
                            cmd.Parameters.AddWithValue("@Topic", txtTopic.Text);
                            cmd.Parameters.AddWithValue("@Description", txt_info.InnerText);
                            cmd.Parameters.AddWithValue("@CompletingDate", txt_date.Text);
                            cmd.Parameters.AddWithValue("@Upload_By", ViewState["teacher"].ToString());
                            cmd.Parameters.AddWithValue("@Upload_Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Upload_Time", mycode.time());
                            cmd.Parameters.AddWithValue("@Upload_Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                mycode.executequery("update HomeWork_Uploaded_Images set Class_id='" + ddl_CourseCat.SelectedValue + "',Subject_Id='" + ddl_subject.SelectedValue + "',section='" + ddl_section.Text + "' where Homework_Id='" + hd_homeworkid.Value + "'");
                                hd_homeworkid.Value = UsesCode.tampid();
                                Alert("homework has been successfully updated.");

                                txt_info.InnerText = "";
                                txtTopic.Text = "";
                                BtnAdd.Text = "Submit";
                                GrdViewimg.DataSource = null;

                                GrdViewimg.DataBind();
                            }

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
            SqlDataAdapter ad = new SqlDataAdapter("select * from dbo.[admission_registor] where Class_id='" + Class_id + "' and Section='" + ddl_section.Text + "' and  Session_id='" + ViewState["sesssionid"].ToString() + "' and Transfer_Status in ('New','NT') and  StudentStatus='AV' ", connection.conn);
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
                        cmd.Parameters.AddWithValue("@Uploaded_by", ViewState["teacher"].ToString());
                        cmd.Parameters.AddWithValue("@Homework_Id", hd_homeworkid.Value);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            BindGridView();

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }

        }

        private void BindGridView()
        {
            DataTable dt = mycode.FillTable("select *,(select top 1 Topic from Homework_Details where Home_Work_id=HomeWork_Uploaded_Images.Homework_Id) as Topicname from HomeWork_Uploaded_Images where Homework_Id='" + hd_homeworkid.Value + "'");
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