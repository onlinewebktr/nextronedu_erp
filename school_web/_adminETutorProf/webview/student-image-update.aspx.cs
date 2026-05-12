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

namespace school_web._adminETutorProf.webview
{
    public partial class student_image_update : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Files["croppedImage"] != null)
            {
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
                string dbfilepath = "";
                Boolean FileSaved = false;

                var file = Request.Files["croppedImage"];
                string filename = Path.GetFileName(file.FileName);
                Session["WorkingImage"] = file.FileName;

                string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                Session["WorkingImage1"] = "stdimages" + idate + time + FileExtension;
                try
                {
                    string path = (Server.MapPath("../../Master_Img/Student")).ToString();
                    file.SaveAs(path + "/" + Session["WorkingImage1"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                }

                if (FileSaved)
                {
                    string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                    string fileName = Path.GetFileName(Session["WorkingImage1"].ToString());
                    dbfilepath = originalPath1 + "/Master_Img/Student/" + fileName;

                    My.exeSql("update admission_registor set studentimagepath='" + dbfilepath + "' where Id='" + Session["stdid"].ToString() + "'");
                    if (mycode.IsUserExist("select Id from Student_image_new where Admission_no='" + Session["admNo"].ToString() + "' and Image_type='Student_image' and Session_id='" + Session["session_id"].ToString() + "'"))
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Student_image_new (Admission_no,Image_name,Image_type,Image_path,Session_id) values (@Admission_no,@Image_name,@Image_type,@Image_path,@Session_id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Admission_no", Session["admNo"].ToString());
                        cmd.Parameters.AddWithValue("@Image_name", "Student Image");
                        cmd.Parameters.AddWithValue("@Image_type", "Student_image");
                        cmd.Parameters.AddWithValue("@Image_path", dbfilepath);
                        cmd.Parameters.AddWithValue("@Session_id", Session["session_id"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "Update Student_image_new set Image_path=@Image_path where Admission_no='" + Session["admNo"].ToString() + "' and Image_Type='Student_image' and Session_id='" + Session["session_id"].ToString() + "'";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Image_path", dbfilepath);
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                    Session["stdid"] = null;
                    Session["admNo"] = null;
                    Session["session_id"] = null;
                    // Request.Files["croppedImage"] = null;
                    Session["msg"] = "Student image has been updated successfully.";
                    //Response.Redirect("student-image-update.aspx?regid=" + Session["tcherId"].ToString(), false);
                }
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["teacher"] = Request.QueryString["regid"].ToString();
                    Session["tcherId"] = ViewState["teacher"].ToString();
                    hd_teacher_id.Value = ViewState["teacher"].ToString();
                    try
                    {
                        ViewState["sessionid"] = My.get_session_id();
                        ViewState["Usertype"] = My.get_user_type(ViewState["teacher"].ToString());
                        if (ViewState["Usertype"].ToString() == "Teacher")
                        {
                            code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  Ptm_class_teacher_mapping   where UserID='" + ViewState["teacher"].ToString() + "'  and Session_id='" + ViewState["sessionid"].ToString() + "')  order by Position asc");
                        }
                        else
                        {
                            code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table  order by Position asc");
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        if (Session["msg"] != null)
                        {
                            Alert(Session["msg"].ToString(), "success");
                            Session["msg"] = null;
                            Bind_grid_data(Session["query"].ToString());
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        private void Alert(string msg, string panel)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class", "warning");
            }
            else
            {
                if (ViewState["Usertype"].ToString() == "Teacher")
                {
                    code.bind_ddl(ddl_section, "Select distinct section  from Ptm_class_teacher_mapping where CategoryID ='" + ddl_class.SelectedValue + "'   and  UserID='" + ViewState["teacher"].ToString() + "'   order by section");
                }
                else
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'  and  Session_id='" + ViewState["sessionid"] + "'   order by Section");
                }
            }
        }

        private void Bind_grid_data(string query)
        {
            try
            {
                ViewState["query"] = query;
                Session["query"] = query;
                DataTable dt = code.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alert("Sorry there are no record exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
            }
            catch
            {
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section", "warning");
            }
            else
            {
                string query = "Select Id,Session_id,session,admissionserialnumber,class,Section,rollnumber,studentname,dob,fathername,studentimagepath from admission_registor where Session_id='" + ViewState["sessionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' ) and Status='1' and Section ='" + ddl_section.Text + "' order by rollnumber";//
                Session["query"] = query;
                Bind_grid_data(query);
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
            Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
            Label lbl_roll = (Label)row.FindControl("lbl_roll");
            Label lbl_names = (Label)row.FindControl("lbl_names");
            Label lbl_img_url = (Label)row.FindControl("lbl_img_url");
            Session["stdid"] = lbl_Id.Text;
            Session["admNo"] = lbl_adm_no.Text;
            Session["session_id"] = lbl_session_id.Text;

            lbl_adm_no_p.Text = lbl_adm_no.Text;
            lbl_roll_p.Text = lbl_roll.Text;
            lbl_lame_p.Text = lbl_names.Text;
            img_std_img_p.ImageUrl = lbl_img_url.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openStdImgs();", true);
        }
    }
}