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
    public partial class ask_doubt : System.Web.UI.Page
    {
        My imp = new My();
        UsesCode mycode = new UsesCode();
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

                        Bind_student_details();
                        get_firm();
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

        private void get_firm()
        {
            try
            {
                DataTable dt = mycode.FillData(" Select * from Firm_Details ");
                if (dt.Rows.Count == 0)
                {
                    ViewState["IsAskdoubt"] = "1";
                }
                else
                {
                    ViewState["IsAskdoubt"] = dt.Rows[0]["IsAskdoubt"].ToString();

                }

            }
            catch
            {
                ViewState["IsAskdoubt"] = "1";

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

        private void Bind_student_details()
        {
            string query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV'    and Transfer_Status in ('New','NT') order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Alertme("Something is wrong", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ViewState["Session_id"] = dr["Session_id"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["Branch_id"] = dr["Branch_id"].ToString();
                    lbl_student_name.Text = dr["studentname"].ToString();
                }
                bind_subject();
            }
        }

        private void bind_subject()
        {
            DataTable dt = mycode.FillData("Select   sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join  Subject_Mapping_New smn on sm.course_id=smn.Class_id and sm.Subject_id=smn.Sub_id   where smn.Admission_no='" + ViewState["regid"].ToString() + "' and smn.Class_id='" + ViewState["class_id"].ToString() + "' and smn.Session_id='" + ViewState["Session_id"].ToString() + "' and smn.Section='" + ViewState["Section"].ToString() + "' order by sm.Subject_position");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rp_subjects.DataSource = null;
                rp_subjects.DataBind();
            }
            else
            {
                rp_subjects.DataSource = dt;
                rp_subjects.DataBind();
            }
        }

        protected void lnk_open_mdl_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_subject_id = (Label)row.FindControl("lbl_subject_id");
                Label lbl_subject_name = (Label)row.FindControl("lbl_subject_name");
                lbl_selected_course.Text = lbl_subject_name.Text;
                ViewState["subjectId"] = lbl_subject_id.Text;
                ViewState["subjectName"] = lbl_subject_name.Text;
                fetch_teacher();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDoubt();", true);
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_teacher()
        {
            DataTable dt = mycode.FillData("select DISTINCT tcsm.UserID,itp.name Name from TeacherCourseSubjectMaping tcsm join user_details itp on tcsm.UserID=itp.user_id and tcsm.Branch_id=itp.Branch_id where tcsm.AssignCourseID='" + ViewState["subjectId"].ToString() + "' and tcsm.CategoryID='" + ViewState["class_id"].ToString() + "' and tcsm.Branch_id='" + ViewState["Branch_id"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "' and section='" + ViewState["Section"] + "'");
            if (dt.Rows.Count == 0)
            {
                rp_teachers.DataSource = null;
                rp_teachers.DataBind();
            }
            else
            {
                rp_teachers.DataSource = dt;
                rp_teachers.DataBind();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {

                if(ViewState["IsAskdoubt"].ToString()=="1")
                {
                    if (txt_question.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDoubt();", true);
                        Alertme("Please enter your question.", "warning");
                        return;
                    }

                    string filePath = "";
                    if (FileUpload1.HasFile)
                    {
                        filePath = Upload_doc_images(FileUpload1, "/UploadedImage/Homework/");
                    }
                    ViewState["is_checked"] = "0";
                    int growcount = rp_teachers.Items.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)rp_teachers.Items[i].FindControl("chk_teacher");
                        if (chk.Checked == true)
                        {
                            Label lbl_teacher_id = (Label)rp_teachers.Items[i].FindControl("lbl_teacher_id");
                            save_doubt(lbl_teacher_id.Text, filePath);

                            ViewState["is_checked"] = "1";
                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (ViewState["is_checked"].ToString() == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDoubt();", true);
                        Alertme("Please select teacher.", "warning");
                    }
                    txt_question.Text = "";

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

        private void save_doubt(string teacher_id, string filePath)
        {
            string doubt_id = get_doubt_id();
            SqlCommand cmd;
            string query = "INSERT INTO Student_doubt_list (Class_Id,Cource_Id,Teacher_Id,Cource_name,Student_question,Question_Image,Status,Date,Idate,User_Id,Doubt_Id,Session_id,Branch) values (@Class_Id,@Cource_Id,@Teacher_Id,@Cource_name,@Student_question,@Question_Image,@Status,@Date,@Idate,@User_Id,@Doubt_Id,@Session_id,@Branch);";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Class_Id", ViewState["class_id"].ToString());
            cmd.Parameters.AddWithValue("@Cource_Id", ViewState["subjectId"].ToString());
            cmd.Parameters.AddWithValue("@Teacher_Id", teacher_id);
            cmd.Parameters.AddWithValue("@Cource_name", ViewState["subjectName"].ToString());
            cmd.Parameters.AddWithValue("@Student_question", txt_question.Text);
            cmd.Parameters.AddWithValue("@Question_Image", filePath);
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@Date", mycode.date());
            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
            cmd.Parameters.AddWithValue("@User_Id", Session["User"].ToString());
            cmd.Parameters.AddWithValue("@Doubt_Id", doubt_id);
            cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_id"].ToString());
            cmd.Parameters.AddWithValue("@Branch", ViewState["Branch_id"].ToString());
            if (My.InsertUpdateData(cmd))
            {

                Alertme("Your question has been submitted successfully.", "success");
            }
        }

        private string get_doubt_id()
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