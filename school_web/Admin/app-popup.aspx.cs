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

namespace school_web.Admin
{
    public partial class app_popup : System.Web.UI.Page
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
                        get_popup_images();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "IdCardTemplate");
            }
        }

        private void get_popup_images()
        {
            DataTable dt = mycode.FillData("select * from Popup_Images");
            if (dt.Rows.Count > 0)
            {
                grddv.Visible = true;
                Image1.ImageUrl = dt.Rows[0]["Image_Path"].ToString();
                lnk_status.Text = dt.Rows[0]["Status"].ToString();
                hd_id.Value = dt.Rows[0]["Id"].ToString();
                imgTemp.Visible = true;
                if (dt.Rows[0]["Status"].ToString() == "Active")
                {
                    lnk_status.CssClass = "status-button activated";
                }
                else
                {
                    lnk_status.CssClass = "status-button inactive";
                }
            }
            else
            {
                grddv.Visible = false;
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = "#";
                if (FileUpload1.PostedFile.ContentLength > 0)
                {
                    filepath = upload_template();
                    if (filepath == "")
                    {
                        Alertme("Please ensure the image size does not exceed 500 KB.", "warning");
                    }
                    else
                    {
                        final_submit(filepath);
                        Alertme("Popup image has been updated successfully.", "success");
                        get_popup_images();
                    }
                }
                else
                {
                    Alertme("Please choose Image", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }



        My mycode = new My();
        private void final_submit(string filepath)
        {
            DataTable dt = My.dataTable("select Id from Popup_Images");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Popup_Images (Image_Path,Status) values (@Image_Path,@Status)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Image_Path", filepath);
                cmd.Parameters.AddWithValue("@Status", "Active");
                if (My.InsertUpdateData(cmd))
                {
                    string desc = "New Mobile App popup image added by : " + ViewState["Userid"].ToString();
                    log_hostory.edit_log("0", "0", "0", "Popup Image for App", desc, "app-popup.aspx", ViewState["Userid"].ToString());
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Popup_Images set Image_Path=@Image_Path,Status=@Status where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Image_Path", filepath);
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                    string desc = "Mobile App popup image updated by : " + ViewState["Userid"].ToString();
                    log_hostory.edit_log("0", "0", "0", "Popup Image for App", desc, "app-popup.aspx", ViewState["Userid"].ToString());
                }
            }
        }

        private string upload_template()
        {
            string filepath = "";
            if (FileUpload1.PostedFile.ContentLength <= 600000)
            {
                string extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dt.ToString("dd_MM_yyyy");
                string time = dt.ToString("hh_mm_ss");
                String FileName1 = date + time + extension;
                string originalPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                HttpPostedFile postedf = FileUpload1.PostedFile;
                postedf.SaveAs(MapPath("~/UploadedImage/" + FileName1));
                filepath = originalPath + "/UploadedImage/" + Path.GetFileName(FileName1);
            }
            return filepath;
        }

        protected void lnk_status_Click(object sender, EventArgs e)
        {
            try
            {
                string status = "Active";
                if (lnk_status.Text == "Active")
                {
                    status = "Inactive";
                }
                My.exeSql("update Popup_Images set Status='" + status + "' where Id='" + hd_id.Value + "'");
                Alertme("Status has been updated successfully.", "success");
                string desc = "Mobile App popup image status updated  " + lnk_status.Text + " to " + status;
                log_hostory.edit_log("0", "0", "0", "Popup Image for App", desc, "app-popup.aspx", ViewState["Userid"].ToString());
                get_popup_images();
            }
            catch (Exception ex)
            {
            }
        }
    }
}