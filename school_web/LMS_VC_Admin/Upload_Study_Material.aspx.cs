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
using System.Web.UI.HtmlControls;
namespace school_web.LMS_VC_Admin
{
    public partial class Upload_Studey_Material : System.Web.UI.Page
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

                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select CategoryName, CategoryID from ClassMaster where Istatus='1' order by Position asc");
                    if (Request.QueryString["Id"] != null)
                    {
                        hd_id.Value = Request.QueryString["Id"].ToString();
                        BindDetails();
                    }
                    else
                    {
                        hd_topicid.Value = UsesCode.tampid();

                    }



                }
            }
        }

        private void BindDetails()
        {
            BtnAdd.Text = "Update";
            DataTable dt = mycode.FillTable("select * from TopicMaster where Id='" + hd_id.Value + "'");
            ddl_CourseCat.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
            mycode.bind_ddl(ddl_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and Istatus='1'  order by section");
            ddl_section.SelectedValue = dt.Rows[0]["Section_Subject"].ToString();
            mycode.bind_all_ddl_with_id(ddl_subject, " Select distinct csm.CourseName, csm.CourseID from Course_or_Subject_Master csm  where csm.CategoryID='" + ddl_CourseCat.SelectedValue + "' and csm.section='" + ddl_section.Text + "' order by csm.CourseName   ");
            ddl_subject.SelectedValue = dt.Rows[0]["CourseID"].ToString();

            mycode.bind_all_ddl_with_id(ddl_teacher, "Select distinct ip.Name,ip.UserID from InstructorProfile ip join TeacherCourseSubjectMaping tcs on ip.UserID=tcs.UserID where tcs.CategoryID='" + ddl_CourseCat.SelectedValue + "' and tcs.section='" + ddl_section.Text + "' and tcs.AssignCourseID='" + ddl_subject.SelectedValue + "' order by Name asc");
            ddl_teacher.SelectedValue = dt.Rows[0]["UserID"].ToString();
           

            txtTopic.Text = dt.Rows[0]["TopicName"].ToString();
            txt_info.InnerText = dt.Rows[0]["Details"].ToString();
            txt_VideoLink.Text = dt.Rows[0]["VideoLink"].ToString();
           
            ddl_Quiz.SelectedValue = dt.Rows[0]["IsForQuiz"].ToString();
            
            hdVideoLink.Value = dt.Rows[0]["VideoLink"].ToString();
            hd_topicid.Value = dt.Rows[0]["TopicID"].ToString();

            BindGridView();
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
                mycode.bind_ddl(ddl_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and Istatus='1'  order by section");
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
                mycode.bind_all_ddl_with_id(ddl_subject, " Select distinct csm.CourseName, csm.CourseID from Course_or_Subject_Master csm  where csm.CategoryID='" + ddl_CourseCat.SelectedValue + "' and csm.section='" + ddl_section.Text + "' order by csm.CourseName   ");
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
                mycode.bind_all_ddl_with_id(ddl_teacher, "Select distinct ip.Name,ip.UserID from InstructorProfile ip join TeacherCourseSubjectMaping tcs on ip.UserID=tcs.UserID where tcs.CategoryID='" + ddl_CourseCat.SelectedValue + "' and tcs.section='" + ddl_section.Text + "' and tcs.AssignCourseID='" + ddl_subject.SelectedValue + "' order by Name asc");
              
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
                    Alert("Please select teacher");
                }
               
                else if (txtTopic.Text == "")
                {
                    Alert("Please enter Topic Name");

                }
                else
                {
                    if (BtnAdd.Text == "Submit")
                    {
                        if (mycode.IsExist("Select TopicID from TopicMaster where TopicName=N'" + txtTopic.Text + "' and CategoryID='" + ddl_CourseCat.SelectedValue + "' and CourseID='" + ddl_subject.SelectedValue + "' and  Section_Subject='" + ddl_section.Text + "' and UserID='" + ddl_teacher.Text + "' "))
                        {
                            string Video_url = txt_VideoLink.Text;
                            string actual_url = "";
                            if (Video_url.Length > 17)
                            {
                                actual_url = Video_url.Substring(17);
                                Video_url = "https://www.youtube.com/embed/" + actual_url;
                            }

                            string DocPath = "";
                           
                            SqlCommand cmd = new SqlCommand("INSERT INTO TopicMaster (CategoryID,CourseID,SectionID,TopicID,TopicName,Details,VideoLink,AudioFile,Document,VideoPostion,IsForQuiz,Istatus,Date,Idate,UserID,Document_sec,Section_Subject) values (@CategoryID,@CourseID,@SectionID,@TopicID,@TopicName,@Details,@VideoLink,@AudioFile,@Document,@VideoPostion,@IsForQuiz,@Istatus,@Date,@Idate,@UserID,@Document_sec,@Section_Subject)");
                            cmd.Parameters.AddWithValue("@CategoryID", ddl_CourseCat.SelectedValue);
                            cmd.Parameters.AddWithValue("@CourseID", ddl_subject.SelectedValue);
                            cmd.Parameters.AddWithValue("@SectionID","0");
                            cmd.Parameters.AddWithValue("@TopicID", hd_topicid.Value);
                            cmd.Parameters.AddWithValue("@TopicName", txtTopic.Text);
                            cmd.Parameters.AddWithValue("@Details", txt_info.InnerText);
                            cmd.Parameters.AddWithValue("@VideoLink", Video_url);
                            cmd.Parameters.AddWithValue("@AudioFile", "");
                            cmd.Parameters.AddWithValue("@Document", "");
                            cmd.Parameters.AddWithValue("@VideoPostion","top");
                            cmd.Parameters.AddWithValue("@IsForQuiz", "");
                            cmd.Parameters.AddWithValue("@Istatus", "1");
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@UserID", ddl_teacher.SelectedValue);
                            cmd.Parameters.AddWithValue("@Document_sec", "");
                            cmd.Parameters.AddWithValue("@Section_Subject", ddl_section.Text);
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                hd_topicid.Value = UsesCode.tampid();
                                Alert("successfully added.");
                                txtTopic.Text = "";
                                txt_VideoLink.Text = "";
                                txt_info.InnerText = "";
                                txtTopic.Text = "";
                                GrdViewimg.DataSource = null;
                                GrdViewimg.EmptyDataText = "Data Not Available";
                                GrdViewimg.DataBind();
                            }

                        }
                        else { Alert("Duplicate Topic Name."); }
                    }
                    if (BtnAdd.Text == "Update")
                    {
                        if (mycode.IsExist("Select TopicID from TopicMaster where TopicName=N'" + txtTopic.Text + "' and CategoryID='" + ddl_CourseCat.SelectedValue + "' and CourseID='" + ddl_subject.SelectedValue + "'  and Section_Subject='" + ddl_section.Text + "' and UserID='" + ddl_teacher.Text + "' and  Id!='" + hd_id.Value + "'"))
                        {
                            string Video_url = txt_VideoLink.Text;
                            string actual_url = "";
                            if (Video_url.Length > 17)
                            {
                                if (hdVideoLink.Value != txt_VideoLink.Text)
                                {
                                    actual_url = Video_url.Substring(17);
                                    Video_url = "https://www.youtube.com/embed/" + actual_url;
                                }

                            }

                            
                            Hd_Document.Value = "";
                            SqlCommand cmd = new SqlCommand("Update TopicMaster set CategoryID=@CategoryID,CourseID=@CourseID,SectionID=@SectionID,TopicID=@TopicID,TopicName=@TopicName,Details=@Details,VideoLink=@VideoLink,AudioFile=@AudioFile,Document=@Document,VideoPostion=@VideoPostion,IsForQuiz=@IsForQuiz,Istatus=@Istatus,Date=@Date,Idate=@Idate,UserID=@UserID,Document_sec=@Document_sec,Section_Subject=@Section_Subject where  where Id='" + hd_id.Value + "'");
                            cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                            cmd.Parameters.AddWithValue("@CategoryID", ddl_CourseCat.SelectedValue);
                            cmd.Parameters.AddWithValue("@CourseID", ddl_subject.SelectedValue);
                            cmd.Parameters.AddWithValue("@SectionID", "Top");
                            cmd.Parameters.AddWithValue("@TopicID", hd_topicid.Value);
                            cmd.Parameters.AddWithValue("@TopicName", txtTopic.Text);
                            cmd.Parameters.AddWithValue("@Details", txt_info.InnerText);
                            cmd.Parameters.AddWithValue("@VideoLink", Video_url);
                            cmd.Parameters.AddWithValue("@AudioFile", "");
                            cmd.Parameters.AddWithValue("@Document", "");
                            cmd.Parameters.AddWithValue("@VideoPostion", "Top");
                            cmd.Parameters.AddWithValue("@IsForQuiz", "");
                            cmd.Parameters.AddWithValue("@Istatus", "1");
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@UserID", ddl_teacher.SelectedValue);
                            cmd.Parameters.AddWithValue("@Document_sec", "");
                            cmd.Parameters.AddWithValue("@Section_Subject", ddl_section.Text);
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                try
                                {
                                    mycode.send_push_to_student_study_material(ddl_CourseCat.SelectedValue, ddl_section.Text, ddl_subject.SelectedItem.Text, txtTopic.Text, ddl_subject.SelectedValue,"", ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());
                                }
                                catch
                                {
                                }
                                hd_topicid.Value = UsesCode.tampid();
                                BtnAdd.Text = "Submit";
                                Alert("successfully updated.");
                                txtTopic.Text = "";
                                txt_VideoLink.Text = "";
                                txt_info.InnerText = "";
                                txtTopic.Text = "";
                                GrdViewimg.DataSource = null;
                                GrdViewimg.EmptyDataText = "Data Not Available";
                                GrdViewimg.DataBind();

                            }


                        }
                        else { Alert("Duplicate Topic Name."); }
                    }

                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

      
        private void BindGridView()
        {
            DataTable dt = mycode.FillTable("select * from Topic_Uploaded_images where Topic_Id='" + hd_topicid.Value + "'");
            if (dt.Rows.Count == 0)
            {

                GrdViewimg.DataSource = null;
                GrdViewimg.EmptyDataText = "Data Not Available";
                GrdViewimg.DataBind();
            }
            else
            {

                GrdViewimg.DataSource = dt;
                GrdViewimg.DataBind();
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
                SqlCommand cmd = new SqlCommand("Delete from Topic_Uploaded_images where Id='" + lbl_Id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                Alert("successfully deleted.");
                BindGridView();


            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

     

        protected void btn_upload_attacment_Click(object sender, EventArgs e)
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
                else
                {
                    SqlCommand cmd;
                    string upload_student_pic = upload_attcament();
                    if (upload_student_pic == "")
                    {
                        Alert("Please choose file");
                    }
                    else
                    {
                        string strQuery = @"INSERT INTO Topic_Uploaded_images (Class_Id,Course_Id,SectionID,Images,Uploaded_Date,Uploaded_Idate,Uploaded_By,Topic_Id,Section_Subject,Type) values (@Class_Id,@Course_Id,@SectionID,@Images,@Uploaded_Date,@Uploaded_Idate,@Uploaded_By,@Topic_Id,@Section_Subject,@Type)";
                        cmd = new SqlCommand(strQuery);
                        cmd.Parameters.AddWithValue("@Class_Id", ddl_CourseCat.SelectedValue);
                        cmd.Parameters.AddWithValue("@Course_Id", ddl_subject.SelectedValue);
                        cmd.Parameters.AddWithValue("@SectionID", "0");
                        cmd.Parameters.AddWithValue("@Images", upload_student_pic);
                        cmd.Parameters.AddWithValue("@Uploaded_Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Uploaded_Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Uploaded_By", ViewState["teacher"].ToString());
                        cmd.Parameters.AddWithValue("@Topic_Id", hd_topicid.Value);
                        cmd.Parameters.AddWithValue("@Section_Subject", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Type", "Attachment File");

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

        private string upload_attcament()
        {
            string file_name = "";
            int tempo;
            Random random = new Random(DateTime.Now.Millisecond);


            tempo = random.Next(1, 1000);
            file_name = mycode.idate() + mycode.itime() + tempo;


            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 5000000)
                {
                    string FileExtension = Path.GetExtension(FileUpload1.FileName.ToLower());
                    rename = file_name + FileExtension;
                    string[] allowedExtensions = { ".pdf", ".PDF" };
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
                        string ServerPath = HttpContext.Current.Server.MapPath("../UploadedImage/Document/");
                        if (File.Exists(ServerPath))
                        {
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(ServerPath);

                        }
                        FileUpload1.SaveAs(ServerPath + rename);
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
                    ImagePath = originalPath1 + "/UploadedImage/Document/" + rename;

                }
            }
            return ImagePath;
        }
        protected void btn_uploadimg_Click(object sender, EventArgs e)
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

                        string strQuery = @"INSERT INTO Topic_Uploaded_images (Class_Id,Course_Id,SectionID,Images,Uploaded_Date,Uploaded_Idate,Uploaded_By,Topic_Id,Section_Subject,Type) values (@Class_Id,@Course_Id,@SectionID,@Images,@Uploaded_Date,@Uploaded_Idate,@Uploaded_By,@Topic_Id,@Section_Subject,@Type)";
                        cmd = new SqlCommand(strQuery);
                        cmd.Parameters.AddWithValue("@Class_Id", ddl_CourseCat.SelectedValue);
                        cmd.Parameters.AddWithValue("@Course_Id", ddl_subject.SelectedValue);
                        cmd.Parameters.AddWithValue("@SectionID", "0");
                        cmd.Parameters.AddWithValue("@Images", upload_student_pic);
                        cmd.Parameters.AddWithValue("@Uploaded_Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Uploaded_Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Uploaded_By", ViewState["teacher"].ToString());
                        cmd.Parameters.AddWithValue("@Topic_Id", hd_topicid.Value);
                        cmd.Parameters.AddWithValue("@Section_Subject", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Type", "Graphic");

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

        private string upload_img()
        {


            string file_name = "";
            int tempo;
            Random random = new Random(DateTime.Now.Millisecond);


            tempo = random.Next(1, 1000);
            file_name = mycode.idate() + mycode.itime() + tempo;


            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fl_img.HasFile)
            {
                if (fl_img.FileBytes.Length < 5000000)
                {
                    string FileExtension = Path.GetExtension(fl_img.FileName.ToLower());
                    rename = file_name + FileExtension;
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
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
                        string ServerPath = HttpContext.Current.Server.MapPath("../UploadedImage/Document/");
                        if (File.Exists(ServerPath))
                        {
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(ServerPath);

                        }
                        fl_img.SaveAs(ServerPath + rename);
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
                    ImagePath = originalPath1 + "/UploadedImage/Document/" + rename;

                }
            }
            return ImagePath;

            //string file_path = "";
            //try
            //{
            //    file_path = "";
            //    if (fl_Document.PostedFile.ContentLength > 0)
            //    {
            //        if (fl_Document.FileBytes.Length <= 5000000)
            //        {
            //            string extension = System.IO.Path.GetExtension(fl_Document.PostedFile.FileName);
            //            string FileName1 = file_name + extension;
            //            HttpPostedFile postedf = fl_Document.PostedFile;
            //            postedf.SaveAs(MapPath("../UploadedImage/Document/" + FileName1));

            //            string fileName = Path.GetFileName(FileName1);
            //            string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
            //            string[] New_originalPath1 = originalPath2.Split('?');
            //            string originalPath1 = New_originalPath1[0].ToString();
            //            file_path = originalPath1 + "/UploadedImage/Document/" + FileName1;
            //        }



            //    }
            //    return file_path;
            //}
            //catch (Exception ex)
            //{
            //    UsesCode.submitexception1(ex);
            //    return file_path;
            //}
        }

        private void send_push_to_student(string Class_id, string Class_name, string Lesson, string Topic)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from dbo.[admission_registor] where Class_id='" + Class_id + "' and Section='" + ddl_section.Text + "'", connection.conn);
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
                    string message = "Dear " + dr["Original_Name"].ToString() + " homework assign of Lesson : " + Lesson + " & Topic : " + Topic;

                    if (dr["gcm_id"].ToString() != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = message;
                        ss["title"] = "Study Material";
                        ss["messagetype"] = "Study Material";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = dr["admissionserialnumber"].ToString();
                        UsesCode.SendNotification(dr["gcm_id"].ToString(), ss);
                    }

                }

            }
        }

        protected void GrdViewimg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Type = (Label)e.Row.FindControl("lbl_Type");
                HtmlAnchor a2 = e.Row.FindControl("a2") as HtmlAnchor;
                HtmlAnchor a1 = e.Row.FindControl("a1") as HtmlAnchor;

                if (lbl_Type.Text == "Attachment File")
                {
                    a2.Visible = true;
                    a1.Visible = false;
                }
                else
                {
                    a2.Visible = false;
                    a1.Visible = true;
                }
            }
        }
    }
}