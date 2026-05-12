using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class upload_student_image : System.Web.UI.Page
    {
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

        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(Session["Admin"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from session_details");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StdentRegistration");
            }
        }


        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    ddl_session.Focus();
                    Alertme("Please select session.", "warning");
                }
                else
                {
                    if (FileUpload1.HasFile)
                    {
                        ViewState["isStdFound"] = "0";
                        upload_imagesss();
                        if (ViewState["isStdFound"].ToString() == "1")
                        {
                            Alertme("Image has been updated successfully.", "success");
                        }
                    }
                    else
                    {
                        FileUpload1.Focus();
                        Alertme("Please choose student images.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void upload_imagesss()
        {
            foreach (var file in FileUpload1.PostedFiles)
            {
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
                string dbfilepath = "";
                Boolean FileOK = false;
                Boolean FileSaved = false;
                int k = 0;

                string value = Guid.NewGuid().ToString();
                Session["WorkingImage"] = Path.GetFileName(file.FileName);
                string extension = new FileInfo(file.FileName).Extension;

                string[] stringSeparatorss = new string[] { "." };

                string[] arrs = Session["WorkingImage"].ToString().Split(stringSeparatorss, StringSplitOptions.None);
                string admission_nos = arrs[0];
                string photorename = admission_nos;
                try 
                { 
                    admission_nos = admission_nos.Replace("ob", "/");
                    //string[] ad_Separatorss = new string[] { "ob" }; 
                    //string[] arrs1 = admission_nos.Split(ad_Separatorss, StringSplitOptions.None); 
                    //string adm1 = arrs1[0];
                    //string adm2 = arrs1[1];
                    //string adm3 = arrs1[2];
                    //admission_nos = adm1 + "/" + adm2 + "/" + adm3; 
                    //photorename= adm1 + "ob" + adm2 + "ob" + adm3;

                }
                catch
                { 
                }


                if (mycode.IsUserExist("select Id from admission_registor where admissionserialnumber='" + admission_nos + "' and Session_id='" + ddl_session.SelectedValue + "'"))
                {
                    update_image_save_status(ddl_session.SelectedValue, admission_nos, "Fail");
                    if (ViewState["isStdFound"].ToString() == "0")
                    {
                        Alertme("Student are not mached with selected image.", "warning");
                    }
                }
                else
                {
                    string[] allowedExtensions = { ".png", ".PNG", ".jpeg", ".JPEG", ".jpg", ".JPG", ".gif", ".webp" };
                    Session["WorkingImage1"] = photorename + idate + time + extension;
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (extension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                    }
                    if (FileOK)
                    {
                        try
                        {
                            string path = (Server.MapPath("../Master_Img/Student")).ToString();
                            file.SaveAs(path + "/" + Session["WorkingImage1"]);
                            FileSaved = true;
                        }
                        catch (Exception ex)
                        {
                            FileSaved = false;
                        }
                    }
                    else
                    {
                        Alertme("Please select jpg and png image", "warning");
                    }
                    if (FileSaved)
                    {
                        string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                        string fileName = Path.GetFileName(Session["WorkingImage1"].ToString());
                        dbfilepath = originalPath1 + "/Master_Img/Student/" + fileName;
                    }


                    if (dbfilepath == "") { update_image_save_status(ddl_session.SelectedValue, admission_nos, "Fail"); }
                    else
                    {
                        update_image_save_status(ddl_session.SelectedValue, admission_nos, "Success");
                        string qry = "update admission_registor set studentimagepath='" + dbfilepath + "' where admissionserialnumber='" + admission_nos + "' and Session_id='" + ddl_session.SelectedValue + "'";
                        My.exeSql(qry);
                        try
                        {
                            if (mycode.IsUserExist("select Id from Student_image_new where Admission_no='" + admission_nos + "' and Image_type='Student Image' and Session_id='" + ddl_session.SelectedValue + "'"))
                            {
                                SqlCommand cmd;
                                string query = "INSERT INTO Student_image_new (Admission_no,Image_name,Image_type,Image_path,Session_id) values (@Admission_no,@Image_name,@Image_type,@Image_path,@Session_id)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Admission_no", admission_nos);
                                cmd.Parameters.AddWithValue("@Image_name", "Student Image");
                                cmd.Parameters.AddWithValue("@Image_type", "Student_image");
                                cmd.Parameters.AddWithValue("@Image_path", dbfilepath);
                                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                                if (My.InsertUpdateData(cmd))
                                {
                                }
                            }
                            else
                            {
                                SqlCommand cmd;
                                string query = "Update Student_image_new set Image_path=@Image_path where Admission_no='" + admission_nos + "' and Image_type='Student Image' and Session_id='" + ddl_session.SelectedValue + "'";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Image_path", dbfilepath);
                                if (My.InsertUpdateData(cmd))
                                {
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        ViewState["isStdFound"] = "1";
                    }
                }


                //if (mycode.IsUserExist("select Id from admission_registor where admissionserialnumber='" + admission_nos + "' and Session_id='" + ddl_session.SelectedValue + "'"))
                //{
                //    Alertme("Student not mached with selected image.", "warning");
                //}
                //else
                //{
                //    string qry = "update admission_registor set studentimagepath='" + dbfilepath + "' where admissionserialnumber='" + admission_nos + "' and Session_id='" + ddl_session.SelectedValue + "'";
                //    My.exeSql(qry);
                //    ViewState["isStdFound"] = "1";
                //}
            }
        }

        private void update_image_save_status(string session_id, string admission_nos, string status)
        {
            SqlCommand cmd;
            string query = "INSERT INTO Bulk_img_save_status (Session_id,Admission_no,Status,Date,Time) values (@Session_id,@Admission_no,@Status,@Date,@Time)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", session_id);
            cmd.Parameters.AddWithValue("@Admission_no", admission_nos);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Date", mycode.date());
            cmd.Parameters.AddWithValue("@Time", mycode.time());
            if (My.InsertUpdateData(cmd))
            {
            }
        }
    }
}
