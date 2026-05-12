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
    public partial class Vice_Principal : System.Web.UI.Page
    {
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
                        try
                        {
                            const string quote = "\"";
                            string tinyMC = My.get_single_column_data("select TinyMC_link as Column_Name from Firm_Details");
                            if (tinyMC != "")
                            {
                                lt_meata.Text = "<script src=" + quote + tinyMC + quote + " referrerpolicy=" + quote + "origin" + quote + " ></script>";
                            }
                            else
                            {
                                lt_meata.Text = "<script src=" + quote + "https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/7/tinymce.min.js" + quote + " referrerpolicy=" + quote + "origin" + quote + "></script>";
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        fetch_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_about_colleger");
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



        My mycode = new My();
        UsesCode usesmycode = new UsesCode();
        private void fetch_data()
        {
            DataTable dt = usesmycode.FillTable("select * from dbo.[Content_about_us_Voice_Principal] ");
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
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Content_about_us_Voice_Principal (Content_first_section,Updated_date,Updated_time,Updated_idate) values (@Content_first_section,@Updated_date,@Updated_time,@Updated_idate)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Content_first_section", txt_info.Value);
                        cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                        cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Alertme("Vice-Principal's desk has been successfully added.", "success");
                            fetch_data();
                        }
                    }
                    else
                    {
                        Alertme("SORRY! You have not permission for this work.", "warning");
                    }
                }
                else
                {
                    if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        SqlCommand cmd;
                        string query = "Update Content_about_us_Voice_Principal set Content_first_section=@Content_first_section,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate where Id='" + hd_id.Value + "'";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Content_first_section", txt_info.Value);
                        cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                        cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Alertme("Vice-Principal's desk has been successfully updated.", "success");
                            fetch_data();
                        }
                    }
                    else
                    {
                        Alertme("SORRY! You have not permission for this work.", "warning");

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
                            Alertme("Please choose valid image.", "warning");
                            return;
                        }
                        update_images(filepath);
                    }
                    else
                    {
                        Alertme("Please Reduce or compress size of image max(500 kb).", "warning");
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
                if (ViewState["Is_add"].ToString() == "1")
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Content_about_us_Voice_Principal (ImagePath,Updated_date,Updated_time,Updated_idate) values (@ImagePath,@Updated_date,@Updated_time,@Updated_idate)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@ImagePath", filepath);
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                    cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        fetch_data();
                        Alertme("Image has been added successfully.", "success");
                    }
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    SqlCommand cmd;
                    string query = "Update Content_about_us_Voice_Principal set ImagePath=@ImagePath,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate where Id='" + hd_id.Value + "'";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@ImagePath", filepath);
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                    cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        fetch_data();
                        Alertme("Image has been updated successfully.", "success");
                    }
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
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
                            break;
                        }
                    }
                }
                else
                {
                    Alertme("Please image size maximum 500kb.", "warning");
                }
            }
            else
            {
                Alertme("Please upload image first.", "warning");
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
                    Alertme("File has not save.", "warning");
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
            if (ViewState["Is_delete"].ToString() == "1")
            {
                mycode.executequery("update Content_about_us_Voice_Principal set ImagePath =null where Id=" + hd_id.Value + "");
                fetch_data();
                Alertme("images has been deleted sucessfully", "success");
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");

            }
        }
    }
}