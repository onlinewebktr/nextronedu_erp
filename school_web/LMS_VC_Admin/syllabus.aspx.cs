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
    public partial class syllabus : System.Web.UI.Page
    {
        My mycodes = new My();
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["sesssionid"] = My.get_session_id();
                    code.bind_all_ddl_with_id(ddl_session, "select Session,session_id from session_details order by ID asc");
                    code.bind_all_ddl_with_id(ddl_session_search, "select Session,session_id from session_details order by ID asc");
                    ddl_session.SelectedValue = ViewState["sesssionid"].ToString();
                    ddl_session_search.SelectedValue = ViewState["sesssionid"].ToString();
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
            code.BindRepeater("select t1.*,t2.Course_Name from Syllabus_master_new t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddl_session_search.SelectedValue + "' order by t2.Position asc", RPDetails);
        }

        private void BindDetails()
        {
            BtnAdd.Text = "Update";
            DataTable dt = code.FillTable("select * from EBook_Details where Id='" + hd_id.Value + "'");
            ddl_class.SelectedValue = dt.Rows[0]["Class_id"].ToString();
            code.bind_all_ddl_with_id(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
            ddl_section.Text = dt.Rows[0]["section"].ToString();
            code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'   order by section");
            code.bind_all_ddl_with_id(ddl_class, "Select Subject_name, Subject_id from Subject_Master where course_id='" + ddl_class.SelectedValue + "'   order by Subject_position");

            ddl_class.SelectedValue = dt.Rows[0]["Subject_id"].ToString();
            //txt_ebookname.Text = dt.Rows[0]["Book_Name"].ToString();
            Hd_Document.Value = dt.Rows[0]["Path_of_ebook"].ToString();
        }
        private void BindDDL()
        {
            code.bind_all_ddl_with_id(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");
                }
                else
                {
                    My.bind_ddl_all_Cap(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id ='" + ddl_class.SelectedValue + "'   order by section");
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
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    ddl_session.Focus();
                    Alert("Please select session");
                    return;
                }
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    ddl_class.Focus();
                    Alert("Please select class");
                    return;
                }
                if (txt_syllabus_details.Text == "")
                {
                    txt_syllabus_details.Focus();
                    Alert("Please enter syllabus details.");
                    return;
                }
                else
                {
                    if (BtnAdd.Text == "Submit")
                    {
                        string DocPath = GetPath();
                        if (DocPath == "")
                        {
                            Alert("please choose syllabus file.");
                            fl_Document.Focus();
                        }
                        else
                        {
                            if (ddl_section.Text == "ALL")
                            {
                                DataTable dt = My.dataTable("Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id ='" + ddl_class.SelectedValue + "'   order by section");
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        save_syllabus(dr["Section"].ToString(), DocPath);
                                    }
                                }
                            }
                            else
                            {
                                save_syllabus(ddl_section.Text, DocPath);
                            }
                            txt_syllabus_details.Text = "";
                            Alert("Syllabus has been successfully added.");
                            BindRpt();
                        }
                    }
                    if (BtnAdd.Text == "Update")
                    {
                        //if (code.IsExist("Select * from EBook_Details where Book_Name='" + txt_ebookname.Text + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and section='" + ddl_section.Text + "' and Subject_id='" + ddl_Course.SelectedValue + "' and Session_id='" + ViewState["sesssionid"].ToString() + "' and Branch='" + ViewState["Branch"].ToString() + "' and Id!='" + hd_id.Value + "'"))
                        //{

                        //    if (fl_images.HasFile) { hdAudio.Value = Getimage(); }
                        //    else { }
                        //    if (fl_Document.HasFile) { Hd_Document.Value = GetPath(); }
                        //    else { }
                        //    SqlCommand cmd = new SqlCommand("Update EBook_Details set Class_id='" + ddl_CourseCat.SelectedValue + "', Subject_id= '" + ddl_Course.SelectedValue + "'," +
                        //        "Book_Name=N'" + txt_ebookname.Text + "', Cover_Photo='" + hdAudio.Value + "',Path_of_ebook='" + Hd_Document.Value + "',section='" + ddl_section.Text + "',Session_id='" + ViewState["sesssionid"].ToString() + "' where Id='" + hd_id.Value + "'");
                        //    InsertUpdate.InsertUpdateData(cmd);
                        //    BtnAdd.Text = "Submit"; BindRpt();
                        //    Response.Redirect("Viewaddedebook.aspx");
                        //}
                        //else { Alert("already added."); }
                    }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void save_syllabus(string Section, string DocPath)
        {
            SqlCommand cmd;
            string query = "INSERT INTO Syllabus_master_new (Session_id,Class_id,Section,Syllabus_info,Syllabus_filepath,Created_by,Created_date,Created_idate,Created_time) values (@Session_id,@Class_id,@Section,@Syllabus_info,@Syllabus_filepath,@Created_by,@Created_date,@Created_idate,@Created_time)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Section", Section);
            cmd.Parameters.AddWithValue("@Syllabus_info", txt_syllabus_details.Text);
            cmd.Parameters.AddWithValue("@Syllabus_filepath", DocPath);
            cmd.Parameters.AddWithValue("@Created_by", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycodes.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycodes.idate());
            cmd.Parameters.AddWithValue("@Created_time", mycodes.time());
            if (My.InsertUpdateData(cmd))
            {
            }
        }

        public string GetPath()
        {
            string Path = code.UploadPDF(fl_Document, "/UploadedImage/E_Book_images_pdf/");
            return Path;
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            if (ddl_session_search.SelectedItem.Text == "Select")
            {
                ddl_session_search.Focus();
                Alert("Please select session.");
                return;
            }
            BindRpt();
        }


        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                My.exeSql("delete from Syllabus_master_new where Id=" + lbl_id.Text + "");
                Alert("Record has been deleted successfully.");
                BindRpt();
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
    }
}