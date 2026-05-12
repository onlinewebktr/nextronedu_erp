using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using school_web.AppCode;
using System.Data;

namespace school_web.Inventory_management
{
    public partial class Firm_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
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

                        mycode.bind_all_ddl_with_id(ddl_state, "select State,Code from dbo.[StateList] order by State asc");
                        BindDetails();
                    }
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "College_Details");
                }
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
        private void BindDetails()
        {
            DataTable dt = mycode.FillData("Select * from Firm_details_sale_purchase  ");
            if (dt.Rows.Count == 0)
            {
                Image1.Visible = false;
            }
            else
            {
                txt_Colleg_name.Text = dt.Rows[0]["firm_name"].ToString();
                txt_address1.Text = dt.Rows[0]["address"].ToString();
                txt_contactno.Text = dt.Rows[0]["contact_no"].ToString();
                txt_emailid.Text = dt.Rows[0]["email"].ToString();
                Image1.Visible = false;
                if (dt.Rows[0]["logo"].ToString() == "")
                {
                    lbl_img_path.Text = "";
                }
                else
                {
                    Image1.Visible = true;
                    Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                    lbl_img_path.Text = dt.Rows[0]["logo"].ToString();
                }
                try
                {
                    ddl_state.Text = dt.Rows[0]["State_code"].ToString();
                }
                catch
                {
                }
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {

            SqlCommand cmd;
            try
            {
                if (txt_Colleg_name.Text == "")
                {
                    txt_Colleg_name.Focus();
                    Alertme("Please enter college name.", "warning");
                    return;
                }
                else
                {
                    bool image = false;
                    string imgpath = "";
                    string thumbnail = "";
                    DataTable dt = mycode.FillData("Select * from Firm_details_sale_purchase  ");
                    if (dt.Rows.Count == 0)
                    {
                        if (FileUpload1.HasFile)
                        {
                            decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                            if (size > 500)
                            {
                                Alertme("Image size must be less than or equal to 500kb. Your selected image size is " + size, "warning");
                                return;
                            }

                            imgpath = upload_image();
                            if (imgpath == "")
                            {
                                image = false;
                            }
                            else
                            {
                                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                                thumbnail = originalPath1 + "/Master_Img/thumbnail_" + Session["WorkingImage1"].ToString();
                                image = true;
                            }
                        }
                        else
                        {
                            image = true;
                            string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                            imgpath = originalPath1 + "/Master_Img/noimage1.png";
                            thumbnail = originalPath1 + "/Master_Img/noimage1.png";
                        }

                        if (image == true)
                        {
                            string query = "INSERT INTO Firm_details_sale_purchase (firm_name,address,contact_no,email,logo,State_code,State) values (@firm_name,@address,@contact_no,@email,@logo,@State_code,@State)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@firm_name", txt_Colleg_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@address", txt_address1.Text);
                            cmd.Parameters.AddWithValue("@contact_no", txt_contactno.Text);
                            cmd.Parameters.AddWithValue("@firm_id", "1");
                            cmd.Parameters.AddWithValue("@logo", imgpath);
                            cmd.Parameters.AddWithValue("@email", txt_emailid.Text);
                            cmd.Parameters.AddWithValue("@State", ddl_state.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@State_code", ddl_state.SelectedValue);
                            if (My.InsertUpdateData(cmd))
                            {
                                Alertme("Firm details has been save Successfully.", "success");
                                BindDetails();
                                btn_Submit.Text = "Save";
                            }
                        }
                        else
                        {
                            Alertme("Please choose logo.", "warning");
                        }
                    }
                    else
                    {
                        string id = dt.Rows[0]["id"].ToString();
                        if (FileUpload1.HasFile)
                        {
                            decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                            if (size > 500)
                            {
                                Alertme("Image size must be less than or equal to 500kb. Your selected image size is " + size, "warning");
                                return;
                            }

                            imgpath = upload_image();
                            if (imgpath == "")
                            {
                                image = false;
                            }
                            else
                            {
                                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                                thumbnail = originalPath1 + "/Master_Img/thumbnail_" + Session["WorkingImage1"].ToString();
                                image = true;
                            }
                        }
                        else
                        {
                            image = true;
                            imgpath = lbl_img_path.Text;

                        }

                        if (image == true)
                        {
                            string query = "Update Firm_details_sale_purchase set firm_name=@firm_name,address=@address,contact_no=@contact_no,email=@email,logo=@logo,State=@State,State_code=@State_code where Id = @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@logo", imgpath);
                            cmd.Parameters.AddWithValue("@firm_name", txt_Colleg_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@address", txt_address1.Text);
                            cmd.Parameters.AddWithValue("@contact_no", txt_contactno.Text);
                            cmd.Parameters.AddWithValue("@firm_id", "1");

                            cmd.Parameters.AddWithValue("@email", txt_emailid.Text);
                            cmd.Parameters.AddWithValue("@State", ddl_state.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@State_code", ddl_state.SelectedValue);


                            if (My.InsertUpdateData(cmd))
                            {
                                Alertme("Firm details has been updated Successfully.", "success");
                                BindDetails();
                                btn_Submit.Text = "Save";
                            }
                        }
                        else
                        {
                            Alertme("Please choose logo", "warning");
                        }
                    }
                }
            }
            catch
            {
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
                if (FileUpload1.FileBytes.Length < 1000000)
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
                    Alertme("Please reduce image size (Max 500KB).", "warning");
                    return "";
                }
            }
            else
            {
                Alertme("Please choose image.", "warning");
            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["WorkingImage1"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    Alertme("Folder permission issue.", "warning");
                }
            }
            else
            {
                Alertme("Please select jpg or png image.", "warning");
            }
            if (FileSaved)
            {
                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                string fileName = Path.GetFileName(Session["WorkingImage1"].ToString());
                dbfilepath = originalPath1 + "/Master_Img/" + fileName;
            }
            return dbfilepath;
        }
    }
}