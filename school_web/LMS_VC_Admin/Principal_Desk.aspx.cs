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

namespace school_web.LMS_VC_Admin
{
    public partial class Principal_Desk : System.Web.UI.Page
    {
        string scrpt;
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    fetch_data();
                }
            }
            catch
            {
            }
        }

        private void fetch_data()
        {
            pnl_content_sec.Visible = true;
            DataTable dt = mycode.FillTable("select * from dbo.[Content_about_us_Principal_Desk] ");
            if (dt.Rows.Count != 0)
            {
                txt_info.Value = dt.Rows[0]["Content_first_section"].ToString();
                title_textS.InnerText = dt.Rows[0]["Section"].ToString();

                pnl_img_sec.Visible = true;
                Image1.ImageUrl = dt.Rows[0]["ImagePath"].ToString();
                lbl_image_path.Text = dt.Rows[0]["ImagePath"].ToString();


                hd_id.Value = dt.Rows[0]["Id"].ToString();
                btn_add.Text = "Update";
                btn_update_image.Text = "Upload";
                if (dt.Rows[0]["ImagePath"].ToString() == "")
                {
                    btn_delete_phot.Visible = false;
                    Image1.Visible = false;
                }
                else
                {
                    Image1.Visible = true;
                    btn_delete_phot.Visible = true;
                }
            }
            else
            {
                btn_add.Text = "Add";
                btn_update_image.Text = "Upload";
                hd_id.Value = "";
                btn_delete_phot.Visible = false;
                Image1.Visible = false;
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (hd_id.Value == "")
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Content_about_us_Principal_Desk (Content_first_section,Updated_date,Updated_time,Updated_idate) values (@Content_first_section,@Updated_date,@Updated_time,@Updated_idate)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Content_first_section", txt_info.Value);
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                    cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        lblmessage.Text = "Principal's desk has been successfully added";
                        scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                        fetch_data();
                    }
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Content_about_us_Principal_Desk set Content_first_section=@Content_first_section,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate where Id='" + hd_id.Value + "'";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Content_first_section", txt_info.Value);
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                    cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        lblmessage.Text = "Principal's desk has been successfully updated";
                        scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                        fetch_data();
                    }

                }




            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_update_image_Click(object sender, EventArgs e)
        {
            try
            {

                string filepath = "";
                if (FileUpload1.HasFile)
                {
                    if (FileUpload1.FileBytes.Length < 500000)
                    {
                        filepath = upload_thumb_attachment();
                        if (filepath == "")
                        {
                            lblmessage.Text = "Please choose valid image.";
                            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                            return;
                        }
                        update_images(filepath);
                    }
                    else
                    {
                        lblmessage.Text = "Please Reduce or compress size of image max(500 kb)";
                        scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    }
                }
                else
                {
                    filepath = lbl_image_path.Text;
                    update_images(filepath);
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void update_images(string filepath)
        {
            if (hd_id.Value == "")
            {
                SqlCommand cmd;
                string query = "INSERT INTO Content_about_us_Principal_Desk (ImagePath,Updated_date,Updated_time,Updated_idate) values (@ImagePath,@Updated_date,@Updated_time,@Updated_idate)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@ImagePath", filepath);
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    fetch_data();
                    lblmessage.Text = "Image has been added successfully.";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Content_about_us_Principal_Desk set ImagePath=@ImagePath,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate where Id='" + hd_id.Value + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@ImagePath", filepath);
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    fetch_data();
                    lblmessage.Text = "Image has been updated successfully.";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }

            }
        }


        private string upload_thumb_attachment()
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = date + time;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 500000)
                {
                    Session["WorkingImage"] = FileUpload1.FileName;
                    string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                    Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
                    string[] allowedExtension = { ".png", ".jpeg", ".jpg" };
                    for (int i = 0; i < allowedExtension.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtension[i])
                        {
                            FileOK = true;
                            lblmessage.Text = "";
                            break;
                        }
                    }
                }
                else
                {
                    lblmessage.Text = "Please image size maximum 500kb";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
            else
            {
                lblmessage.Text = "Please upload image first.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../UploadedImage/schoollogo")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    lblmessage.Text = "File has not save.";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
            else
            {

            }
            if (FileSaved)
            {
                string fileName = Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = @"/UploadedImage/schoollogo/" + fileName;
            }
            return dbfilePath;
        }

        protected void btn_delete_phot_Click(object sender, EventArgs e)
        {
            mycode.executequery("update Content_about_us_Principal_Desk set ImagePath =null where Id=" + hd_id.Value + "");
            fetch_data();
            lblmessage.Text = "images has been deleted sucessfully";
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

    }
}