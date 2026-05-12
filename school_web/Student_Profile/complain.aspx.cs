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
    public partial class complain : System.Web.UI.Page
    {
        My mycode = new My();
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
                        try
                        {
                            get_std_info();
                            get_complain_info();
                            ViewState["complain_id"] = get_complain_id();
                        }
                        catch (Exception ex)
                        {
                        }
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
                    ViewState["Is_Student_Complain"] = "1";
                }
                else
                {
                    ViewState["Is_Student_Complain"] = dt.Rows[0]["Is_Student_Complain"].ToString();

                }

            }
            catch
            {
                ViewState["Is_Student_Complain"] = "1";

            }

        }
        private void get_std_info()
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
                }
            }
        }

        private object get_complain_id()
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



        private void get_complain_info()
        {
            DataTable dt = mycode.FillData("select t1.*,t2.studentname,CASE WHEN t1.Status='0' THEN '*in progress*'  WHEN t1.Status='1' THEN 'Closed' END AS Status_name,(t1.Created_date+ ' '+t1.Created_time) as New_date from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.User_id='" + ViewState["regid"].ToString() + "' and t2.Session_id='" + ViewState["Session_id"].ToString() + "'  order by t1.Id asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rp_doubt.DataSource = null;
                rp_doubt.DataBind();
            }
            else
            {
                rp_doubt.DataSource = dt;
                rp_doubt.DataBind();
            }
        }

        protected void lnk_ask_again_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_doubt_id = (Label)row.FindControl("lbl_doubt_id");

                DataTable dt = mycode.FillData("select * from Student_doubt_list where Doubt_Id='" + lbl_doubt_id.Text + "' and User_Id='" + Session["User"].ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    string doubt_id = get_doubt_id();
                    SqlCommand cmd;
                    string query = "INSERT INTO Student_doubt_list (Class_Id,Cource_Id,Teacher_Id,Cource_name,Student_question,Question_Image,Status,Date,Idate,User_Id,Doubt_Id,Session_id,Branch) values (@Class_Id,@Cource_Id,@Teacher_Id,@Cource_name,@Student_question,@Question_Image,@Status,@Date,@Idate,@User_Id,@Doubt_Id,@Session_id,@Branch);";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Class_Id", dt.Rows[0]["Class_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Cource_Id", dt.Rows[0]["Cource_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Teacher_Id", dt.Rows[0]["Teacher_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Cource_name", dt.Rows[0]["Cource_name"].ToString());
                    cmd.Parameters.AddWithValue("@Student_question", dt.Rows[0]["Student_question"].ToString());
                    cmd.Parameters.AddWithValue("@Question_Image", dt.Rows[0]["Question_Image"].ToString());
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                    cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@User_Id", dt.Rows[0]["User_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Doubt_Id", doubt_id);
                    cmd.Parameters.AddWithValue("@Session_id", dt.Rows[0]["Session_id"].ToString());
                    cmd.Parameters.AddWithValue("@Branch", dt.Rows[0]["Branch"].ToString());
                    //if (My.InsertUpdateData(cmd))
                    //{
                    //    Alertme("Your question has been submitted successfully. Teacher will reply you soon.", "success");
                    //}
                }
            }
            catch (Exception ex)
            {
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

        protected void btn_save_image_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = "";
                if (FileUpload1.HasFile)
                {
                    filePath = Upload_images(FileUpload1, "/UploadedImage/Homework/");
                    save_image_path(filePath);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDoubt();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDoubt();", true);
                    Alertme("Please choose image.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void save_image_path(string filePath)
        {
            SqlCommand cmd;
            string query = "INSERT INTO Complain_feedback_Attachment (Request_id,Attachment) values (@Request_id,@Attachment);";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Request_id", ViewState["complain_id"].ToString());
            cmd.Parameters.AddWithValue("@Attachment", filePath);
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Your image has been saved successfully.", "success");
                fetch_images();
            }
        }

        private void fetch_images()
        {
            DataTable dt = mycode.FillData(" select * from Complain_feedback_Attachment where Request_id='" + ViewState["complain_id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                pdfsDV.Visible = false;
                rp_pdfs.DataSource = null;
                rp_pdfs.DataBind();
            }
            else
            {
                pdfsDV.Visible = true;
                rp_pdfs.DataSource = dt;
                rp_pdfs.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDoubt();", true);
        }

        public string Upload_images(FileUpload fileName, string FolderPath)
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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Student_Complain"].ToString() == "1")
            {

                try
                {

                    if (txt_complain.Text == "")
                    {
                        txt_complain.Focus();
                        Alertme("Please enter your complain.", "warning");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDoubt();", true);
                    }
                    else
                    {
                        save_complain();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {

                Alertme("This feature is not enabled", "warning");


            }

        }

        private void save_complain()
        {
            SqlCommand cmd;
            string query = "INSERT INTO Complain_feedback (Request_id,User_id,Type,Is_related_with_order,Order_id,Status,Created_date,Created_time,Created_idate,message,Last_reply_date,Last_reply_idate,Last_reply_time,Session_id,Branch) values (@Request_id,@User_id,@Type,@Is_related_with_order,@Order_id,@Status,@Created_date,@Created_time,@Created_idate,@message,@Last_reply_date,@Last_reply_idate,@Last_reply_time,@Session_id,@Branch);";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Request_id", ViewState["complain_id"].ToString());
            cmd.Parameters.AddWithValue("@User_id", ViewState["regid"].ToString());
            cmd.Parameters.AddWithValue("@Type", "1");
            cmd.Parameters.AddWithValue("@Is_related_with_order", "0");
            cmd.Parameters.AddWithValue("@Order_id", "0");
            cmd.Parameters.AddWithValue("@Status", "0");
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_time", mycode.time());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@message", txt_complain.Text);
            cmd.Parameters.AddWithValue("@Last_reply_date", mycode.date());
            cmd.Parameters.AddWithValue("@Last_reply_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Last_reply_time", mycode.time());
            cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_id"].ToString());
            cmd.Parameters.AddWithValue("@Branch", ViewState["Branch_id"].ToString());
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Your complain has been sent successfully.", "success");
                get_complain_info();
            }
        }

        protected void lnk_delete_image_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_attchmnt_id = (Label)row.FindControl("lbl_attchmnt_id");
                string qrys = "delete from Complain_feedback_Attachment where Id=" + lbl_attchmnt_id.Text + "";
                mycode.executequery(qrys);
                fetch_images();
            }
            catch (Exception ex)
            {
            }
        }
    }
}