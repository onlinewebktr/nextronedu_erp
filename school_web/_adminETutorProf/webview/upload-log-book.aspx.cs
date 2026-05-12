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
    public partial class upload_log_book : System.Web.UI.Page
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
                    code.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
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
                code.bind_ddl(ddl_section, "select DISTINCT Section from admission_registor where Class_id='" + ddl_class.SelectedValue + "' order by Section asc");
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
                if (FileUpload1.HasFile)
                {
                    attechmentS = upload_attechmentS(FileUpload1, "File1");
                    if (attechmentS == "")
                    {
                        Alert("Please select valid attachment.");
                        attechmentS = "#";
                        return;
                    }
                    else
                    {
                        save_data(attechmentS);
                        txt_remarks.Text = "";
                    }
                }
                else
                {
                    save_data(attechmentS);
                    txt_remarks.Text = "";
                }
                //else
                //{
                //    Alert("Please select attachment.");
                //}
            }
        }



        private void save_data(string attechmentS)
        {
            int idates = mycode.ConvertStringToiDateup(txt_date.Text);
            if (mycode.IsUserExist("select Id from Teacher_log_book where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and idate='" + idates + "'  and Created_by='" + ViewState["teacher"].ToString() + "'"))
            {
                string query = "INSERT INTO Teacher_log_book (Session_id,Class_id,Section,Remark,Date,idate,Created_by,Created_date,Created_time,Attachments,Send_Status) values (@Session_id,@Class_id,@Section,@Remark,@Date,@idate,@Created_by,@Created_date,@Created_time,@Attachments,@Send_Status)";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                cmd.Parameters.AddWithValue("@Remark", txt_remarks.Text);
                cmd.Parameters.AddWithValue("@Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@idate", mycode.ConvertStringToiDateup(txt_date.Text));
                cmd.Parameters.AddWithValue("@Created_by", ViewState["teacher"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                cmd.Parameters.AddWithValue("@Attachments", attechmentS);
                cmd.Parameters.AddWithValue("@Send_Status", "Send");
                
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    update_add_push();
                    Alert("Log book has been successfully uploaded.");
                }
            }
            else
            {
                string query = "update Teacher_log_book set Remark=@Remark,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_time=@Updated_time,Attachments=@Attachments,Send_Status=@Send_Status where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and idate='" + idates + "'  and Created_by='" + ViewState["teacher"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Remark", txt_remarks.Text);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["teacher"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                cmd.Parameters.AddWithValue("@Attachments", attechmentS);
                cmd.Parameters.AddWithValue("@Send_Status", "Send");
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    update_add_push();
                    Alert("Log book has been successfully uploaded.");
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

            query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "'  and  Session_id='" + My.get_session_id() + "'";
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
                    final_send_push(gcmid, admissionserialnumber);

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
                ss["title"] = "Log Book";
                ss["messagetype"] = "LogBook";
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