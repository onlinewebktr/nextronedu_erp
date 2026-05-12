
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class AddSlider : System.Web.UI.Page
    {
        UsesCode imp = new UsesCode();
        string scrpt;
        SqlCommand cmd;
        bool image = false;
        string query = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["Admin"] != null)
                    {

                        BindGrid();
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
            catch
            {
            }
        }

        private void BindGrid()
        {
            query = " select  * from Slider_Details  order by Id desc";
            Bind_grid_data(query);
        }

        private void Bind_grid_data(string query)
        {
            ViewState["Query"] = query;
            DataTable dt = imp.FillTable(query);
            imp.GrdData(dt, GrdView);
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_Submit.Text == "Create")
                {


                    insertAll();


                }
                else
                {
                    updateAll();
                }

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }

        private void updateAll()
        {

            bool image = false;
            string img = "";
            if (FileUpload1.HasFile)
            {
                img = upload_image();
                if (img == "")
                {
                    image = false;

                }
                else
                {
                    ViewState["imagepath"] = img;
                    image = true;
                }
            }
            else
            {
                image = true;


            }
            if (image == true)
            {

                query = "Update Slider_Details set Image_path=@Image_path,caption=@caption,status=@status where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Image_path", ViewState["imagepath"].ToString());

                cmd.Parameters.AddWithValue("@caption", txt_description.Text);
                cmd.Parameters.AddWithValue("@status", "1");
                cmd.Parameters.AddWithValue("@Id", ViewState["Id"].ToString());
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    lblmessage.Text = "Slider details has been updated Successfully.";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    txt_description.Text = "";
                    btn_Submit.Text = "Create";
                    btn_cancel.Visible = false;
                    BindGrid();
                }
            }
        }

        private void insertAll()
        {
            string imagepath = "";
            if (FileUpload1.HasFile)
            {
                imagepath = upload_image();
                if (imagepath == "")
                {
                    image = false;
                }
                else
                {
                    image = true;
                }
            }
            else
            {
                lblmessage.Text = "Please select image.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                image = false;

            }
            if (image == true)
            {
                query = "INSERT INTO Slider_Details(Image_path,caption,status) values (@Image_path,@caption,@status)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Image_path", imagepath);
                cmd.Parameters.AddWithValue("@caption", txt_description.Text);
                cmd.Parameters.AddWithValue("@status", "1");


                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    lblmessage.Text = "Slider has been updated Successfully.";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    txt_description.Text = "";
                    BindGrid();
                }
            }
        }

        private string upload_image()
        {
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
            string dbfilepath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;

            int k = 0;
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 500000)
                {
                    Session["WorkingImage"] = FileUpload1.FileName;
                    string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();

                    string[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif" };
                    Session["WorkingImage1"] = idate + time + FileExtension;
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                    }
                }
                else
                {
                    lblmessage.Text = "Please reduce image size (Max 500KB)";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    return dbfilepath;
                }
            }
            else
            {
                lblmessage.Text = "Please select jpg and png image";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                return dbfilepath;
            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../UploadedImage/Slider")).ToString();
                    if (!Directory.Exists(path))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                        FileUpload1.SaveAs(path + "/" + Session["WorkingImage1"]);
                        FileSaved = true;
                    }
                    else
                    {
                        FileUpload1.SaveAs(path + "/" + Session["WorkingImage1"]);
                        FileSaved = true;
                    }

                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
            else
            {
                lblmessage.Text = "Please select jpg and png image";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            if (FileSaved)
            {
                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                string fileName = Path.GetFileName(Session["WorkingImage1"].ToString());
                dbfilepath = originalPath1 + "/UploadedImage/Slider/" + fileName;
            }
            return dbfilepath;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_Submit.Text = "Create";
            txt_description.Text = "";
            btn_cancel.Visible = false;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;


                if (Session["PreviousRowIndex"] != null)
                {
                    var previousRowIndex = (int)Session["PreviousRowIndex"];
                    GridViewRow PreviousRow = GrdView.Rows[previousRowIndex];
                    PreviousRow.ForeColor = Color.Black;
                    PreviousRow.BackColor = Color.White;
                }
                row.ForeColor = Color.White;
                row.BackColor = Color.Red;
                Session["PreviousRowIndex"] = row.RowIndex;



                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Description = (Label)row.FindControl("lbl_Description");
                
                Label lbl_Image = (Label)row.FindControl("lbl_Image");
                
                ViewState["Id"] = lbl_Id.Text;
                txt_description.Text = lbl_Description.Text;
                
                ViewState["imagepath"] = lbl_Image.Text;
                


                btn_Submit.Text = "Update";
                btn_cancel.Visible = true;
            }
            catch { }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                LinkButton lnkDel = (LinkButton)row.FindControl("lnkDel");
                Label Id = (Label)row.FindControl("lbl_Id");
                HdID.Value = Id.Text;
                imp.executequery("delete from Slider_Details where Id=" + HdID.Value + "");
                lblmessage.Text = "Slider has been deleted successfully.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                Bind_grid_data(ViewState["Query"].ToString());
            }
            catch { }
        }

        
    }
}