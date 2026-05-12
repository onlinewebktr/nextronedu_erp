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

namespace school_web._adminETutorProf.webview
{
    public partial class Update_Class_Activity : System.Web.UI.Page
    {
        My mycode = new My();
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    Dictionary<string, object> dc1 = My.get_push_credantial();
                    ViewState["type"] = (String)dc1["type"];
                    ViewState["project_id"] = (String)dc1["project_id"];
                    ViewState["private_key_id"] = (String)dc1["private_key_id"];
                    ViewState["client_email"] = (String)dc1["client_email"];
                    ViewState["client_id"] = (String)dc1["client_id"];
                    ViewState["private_key"] = dc1["private_key"].ToString().Replace("\\n", "\n");
                    ViewState["teacher"] = Request.QueryString["regid"].ToString();
                    ViewState["sessionid"] = code.get_session_id_use();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["teacher"].ToString());
                    txt_date.Text = mycode.date();

                    ViewState["Usertype"] = My.get_user_type(ViewState["teacher"].ToString());
                    if (ViewState["Usertype"].ToString() == "Teacher")
                    {
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "')  order by Position asc");
                    }
                    else
                    {
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table  order by Position asc");
                    }

                }
            }
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;

            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else
            {
                if (ViewState["Usertype"].ToString() == "Teacher")
                {
                    code.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_class.SelectedValue + "'   and  UserID='" + ViewState["teacher"].ToString() + "'   order by section");
                }
                else
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'   and  Session_id='" + ViewState["sessionid"] + "'   order by Section");
                }
            }

        }
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alert("Please select section");
                }
                else
                {
                    if (ViewState["Usertype"].ToString() == "Teacher")
                    {
                        mycode.bind_all_ddl_with_id(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from TeacherCourseSubjectMaping tcsm join  Subject_Master sm on tcsm.CategoryID=sm.course_id and tcsm.AssignCourseID=sm.Subject_id   where tcsm.UserID='" + ViewState["teacher"].ToString() + "' and tcsm.CategoryID='" + ddl_class.SelectedValue + "' and tcsm.section='" + ddl_section.Text + "' and tcsm.Session_id='" + ViewState["sessionid"].ToString() + "'   order by sm.Subject_position");
                    }
                    else
                    {
                        mycode.bind_all_ddl_with_id(ddl_subject, "Select   Subject_name, Subject_id from Subject_Master  where course_id='" + ddl_class.SelectedValue + "' order by  Subject_position");
                    }

                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Update_class_activity");
            }
        }

        #region save
        protected void btn_save_Click1(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (txt_remarks.Text == "")
            {
                Alert("Please enter remarks");
            }
            else
            {
                string attechmentS = "#";
                attechmentS = upload_attechmentS(FileUpload1, "File1");
                if (attechmentS == "")
                {
                    attechmentS = "";
                }
                save_data(attechmentS);
                txt_remarks.Text = "";
            }

        }

        private void save_data(string attechmentS)
        {
            if (ddl_subject.SelectedItem.Text == "ALL")
            {
                string qrySubj = "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from TeacherCourseSubjectMaping tcsm join  Subject_Master sm on tcsm.CategoryID=sm.course_id and tcsm.AssignCourseID=sm.Subject_id where tcsm.UserID='" + ViewState["teacher"].ToString() + "' and tcsm.CategoryID='" + ddl_class.SelectedValue + "' and tcsm.section='" + ddl_section.Text + "' and tcsm.Session_id='" + ViewState["sessionid"].ToString() + "' order by sm.Subject_position";
                DataTable cdtSubK = My.dataTable(qrySubj);
                foreach (DataRow dr in cdtSubK.Rows)
                {

                    string query = "INSERT INTO Activity_Class_Details (Session_id,Branch_id,Class_id,Subject_id,Section_data,Date,idate,time,Remarks,Teacher_id,Attachment,Send_status) values (@Session_id,@Branch_id,@Class_id,@Subject_id,@Section_data,@Date,@idate,@time,@Remarks,@Teacher_id,@Attachment,@Send_status)";
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Subject_id", dr["Subject_id"].ToString());
                    cmd.Parameters.AddWithValue("@Section_data", ddl_section.Text);
                    cmd.Parameters.AddWithValue("@Date", txt_date.Text);
                    cmd.Parameters.AddWithValue("@idate", mycode.ConvertStringToiDateup(txt_date.Text));
                    cmd.Parameters.AddWithValue("@time", mycode.time());
                    cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
                    cmd.Parameters.AddWithValue("@Teacher_id", ViewState["teacher"].ToString());
                    cmd.Parameters.AddWithValue("@Attachment", attechmentS);
                    cmd.Parameters.AddWithValue("@Send_status", "Send");
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        update_add_push();
                        Alert("Class activity has been successfully added");
                        txt_remarks.Text = "";
                    }

                }
            }
            else
            {


                string title = "Class Activity";
                string Message = txt_remarks.Text + " Date :" + txt_date.Text;
                string query = "INSERT INTO Activity_Class_Details (Session_id,Branch_id,Class_id,Subject_id,Section_data,Date,idate,time,Remarks,Teacher_id,Attachment,Title,Message_subject,Send_status) values (@Session_id,@Branch_id,@Class_id,@Subject_id,@Section_data,@Date,@idate,@time,@Remarks,@Teacher_id,@Attachment,@Title,@Message_subject,@Send_status)";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                cmd.Parameters.AddWithValue("@Section_data", ddl_section.Text);
                cmd.Parameters.AddWithValue("@Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@idate", mycode.ConvertStringToiDateup(txt_date.Text));
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
                cmd.Parameters.AddWithValue("@Teacher_id", ViewState["teacher"].ToString());
                cmd.Parameters.AddWithValue("@Attachment", attechmentS);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Message_subject", Message);
                cmd.Parameters.AddWithValue("@Send_status", "Send");

                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    update_add_push();
                    Alert("Class activity has been successfully added");
                    txt_remarks.Text = "";
                }


            }
            #endregion


        }


        private string upload_attechmentS(FileUpload Files, string name)
        {
            string dbfilePaththumb = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = name + date + time;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (Files.HasFile)
            {
                Session["WorkingImage"] = Files.FileName;
                string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                Session["renamedfileT"] = filerename + "PIMG1" + FileExtension;
                string[] allowedExtension = { ".png", ".PNG", ".jpeg", ".jpg", ".JPG", ".JPEG", ".pdf", ".PDF", ".DOC", ".docs", ".doc", ".DOC" };
                for (int i = 0; i < allowedExtension.Length; i++)
                {
                    k++;
                    if (FileExtension == allowedExtension[i])
                    {
                        FileOK = true;
                        break;
                    }
                }
            }
            else
            {
                Alert("Please upload file first.");
            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../../Master_Img")).ToString();
                    Files.SaveAs(path + "/" + Session["renamedfileT"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    Alert("File has not save.");
                }
            }
            else
            {

            }
            if (FileSaved)
            {
                string originalPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                try
                {
                    string[] stringSeparatorss = new string[] { "?" };
                    string[] arrs = originalPath.Split(stringSeparatorss, StringSplitOptions.None);
                    originalPath = arrs[0];
                }
                catch (Exception ex)
                {
                }

                string fileName = Path.GetFileName(Session["renamedfileT"].ToString());
                dbfilePaththumb = originalPath + "/Master_Img/" + fileName;
            }
            return dbfilePaththumb;
        }

        private void update_add_push()
        {
            string query = "";

            query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "'  and  Session_id='" + My.get_session_id() + "' and admissionserialnumber in (select Admission_no from Subject_Mapping_New where Class_id='"+ddl_class.SelectedValue+"' and Section='"+ddl_section.Text+"' and Session_id='"+ My.get_session_id() + "')";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
               


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    final_send_push(gcmid, admissionserialnumber );

                }

            }
        }

        private void final_send_push(string gcmid, string admissionserialnumber )
        {
            if (gcmid == "")
            {
                gcmid = "0";
            }

            if (gcmid != "")
            {
                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = txt_remarks.Text;
                ss["title"] = "Class Activity";
                ss["messagetype"] = "ClassActivity";
                ss["url"] = "";
                ss["link_url"] = "";
                ss["UserId"] = admissionserialnumber;
                ss["type"] = ViewState["type"].ToString();
                ss["project_id"] = ViewState["project_id"].ToString();
                ss["private_key_id"] = ViewState["private_key_id"].ToString();
                ss["client_email"] = ViewState["client_email"].ToString();
                ss["client_id"] = ViewState["client_id"].ToString();
                ss["private_key"] = ViewState["private_key"].ToString();
                My.onlypush(gcmid, ss);
            }
        }
    }
}