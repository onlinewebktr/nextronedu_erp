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
    public partial class Upload_E_book : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Dictionary<string, object> dc1 = My.get_push_credantial();
                    ViewState["type"] = (String)dc1["type"];
                    ViewState["project_id"] = (String)dc1["project_id"];
                    ViewState["private_key_id"] = (String)dc1["private_key_id"];
                    ViewState["client_email"] = (String)dc1["client_email"];
                    ViewState["client_id"] = (String)dc1["client_id"];
                    ViewState["private_key"] = dc1["private_key"].ToString().Replace("\\n", "\n");

                    ViewState["Branch"] = My.get_session_id();
                    ViewState["sesssionid"] = My.get_session_id();
                    BindDDL();
                    BindRpt();
                    if (Request.QueryString["Id"] != null)
                    {
                        hd_id.Value = Request.QueryString["Id"].ToString();
                        BindDetails();
                    } 
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindRpt()
        {
            code.BindRepeater(" select top 10 *,(select Course_Name from Add_course_table where course_id=t1.Class_id) as CategoryName, (select Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as CourseName from dbo.[EBook_Details] t1 where t1.Session_id='" + ViewState["sesssionid"].ToString() + "' order by id desc", RPDetails);
        }

        private void BindDetails()
        {
            BtnAdd.Text = "Update";
            DataTable dt = code.FillTable("select * from EBook_Details where Id='" + hd_id.Value + "'");
            ddl_CourseCat.SelectedValue = dt.Rows[0]["Class_id"].ToString();
            code.bind_all_ddl_with_id(ddl_Course, "Select Course_Name, course_id from Add_course_table order by Position");
            ddl_section.Text = dt.Rows[0]["section"].ToString();
            code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'   order by section");
            code.bind_all_ddl_with_id(ddl_Course, "Select Subject_name, Subject_id from Subject_Master where course_id='" + ddl_CourseCat.SelectedValue + "'   order by Subject_position");

           
           

            ddl_Course.SelectedValue = dt.Rows[0]["Subject_id"].ToString();

            txt_ebookname.Text = dt.Rows[0]["Book_Name"].ToString();

            hdAudio.Value = dt.Rows[0]["Cover_Photo"].ToString();
            Hd_Document.Value = dt.Rows[0]["Path_of_ebook"].ToString();
        }
        private void BindDDL()
        {
            code.bind_all_ddl_with_id(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");
                }
                else
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'   order by section");
                }
            }
            catch
            {
            }
        }
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");
                }
                if (ddl_section.Text == "Select")
                {
                    Alert("Please select sesction");
                }
                else
                {
                    code.bind_all_ddl_with_id(ddl_Course, "Select Subject_name, Subject_id from Subject_Master where course_id='" + ddl_CourseCat.SelectedValue + "'   order by Subject_position");
                }
                
            }
            catch
            {
            }
        }


        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");
                    return;
                }
                if (ddl_section.Text== "Select")
                {
                    Alert("Please select section");
                    return;
                }
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select subject");
                    return;
                }
                if (txt_ebookname.Text == "")
                {
                    Alert("Please enter e-book name");
                    return;
                }


                else
                {
                    if (BtnAdd.Text == "Submit")
                    {
                        if (code.IsExist("Select * from EBook_Details where Book_Name='" + txt_ebookname.Text + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Subject_id='" + ddl_Course.SelectedValue + "' and section='" + ddl_section.Text + "' and Session_id='" + ViewState["sesssionid"].ToString() + "' and Branch='" + ViewState["Branch"].ToString() + "'"))
                        {
                            string book_id = code.auto_serial("Book_id");
                            string DocPath = GetPath();
                            string imagePath = Getimage();


                            if (imagePath == "")
                            {
                                Alert("please choose file");
                            }
                            else  if (DocPath == "")
                            {
                                Alert("please choose file");
                            }
                            else
                            {
                                string qry = @"insert into EBook_Details(Book_id,Book_Name,Cover_Photo,Path_of_ebook,Class_id,Subject_id,Upload_date,section,Session_id,Branch) 
                                       values ('" + book_id + "',N'" + txt_ebookname.Text + "','" + imagePath + "','" + DocPath + "','" + ddl_CourseCat.SelectedValue + "','" + ddl_Course.SelectedValue + "',N'" + code.datetime() + "','" + ddl_section.Text + "','" + ViewState["sesssionid"].ToString() + "','" + ViewState["Branch"].ToString() + "');";
                                SqlCommand cmd = new SqlCommand(qry);
                                InsertUpdate.InsertUpdateData(cmd);

                                try
                                {
                                    code.send_push_to_student_ebook(ddl_CourseCat.SelectedValue, ddl_section.Text, ddl_Course.SelectedItem.Text, txt_ebookname.Text, ddl_Course.SelectedValue, ViewState["sesssionid"].ToString(), ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());


                                }
                                catch
                                {

                                }
                                Alert("successfully added.");
                                BindRpt();
                            }
                           
                        }
                        else { Alert("already added."); }
                    }
                    if (BtnAdd.Text == "Update")
                    {
                        if (code.IsExist("Select * from EBook_Details where Book_Name='" + txt_ebookname.Text + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and section='" + ddl_section.Text + "' and Subject_id='" + ddl_Course.SelectedValue + "' and Session_id='" + ViewState["sesssionid"].ToString() + "' and Branch='" + ViewState["Branch"].ToString() + "' and Id!='" + hd_id.Value + "'"))
                        {

                            if (fl_images.HasFile) { hdAudio.Value = Getimage(); }
                            else { }
                            if (fl_Document.HasFile) { Hd_Document.Value = GetPath(); }
                            else { }
                            SqlCommand cmd = new SqlCommand("Update EBook_Details set Class_id='" + ddl_CourseCat.SelectedValue + "', Subject_id= '" + ddl_Course.SelectedValue + "'," +
                                "Book_Name=N'" + txt_ebookname.Text + "', Cover_Photo='" + hdAudio.Value + "',Path_of_ebook='" + Hd_Document.Value + "',section='" + ddl_section.Text + "',Session_id='" + ViewState["sesssionid"].ToString() + "' where Id='" + hd_id.Value + "'");
                            InsertUpdate.InsertUpdateData(cmd);
                            BtnAdd.Text = "Submit"; BindRpt();
                            Response.Redirect("Viewaddedebook.aspx");
                        }
                        else { Alert("already added."); }
                    }
                    txt_ebookname.Text = "";

                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        public string Getimage()
        {
            string Path = code.UploadImage(fl_images, "/UploadedImage/E_Book_images_pdf/");//code.UploadAudio(fl_Audio, "/UploadedImage/AudioFile/");
            return Path;
        }
        public string GetPath()
        {
            string Path = code.UploadPDF(fl_Document, "/UploadedImage/E_Book_images_pdf/");
            return Path;
        }

       


    }
}