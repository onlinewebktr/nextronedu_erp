using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
using System.IO;
namespace school_web.InstructorProfile
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teacher"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");

            }
            else
            {
                if (!IsPostBack)
                {

                    ViewState["id"] = Session["teacher"].ToString();
                    find_name();

                }
            }
        }
        UsesCode code = new UsesCode();
        private void find_name()
        {
            try
            {
                string sql = "select * from PRL_Employee_Master where Emp_Code ='" + ViewState["id"].ToString() + "'";
                DataTable dt = code.FillTable(sql);

                if (dt.Rows.Count == 0)
                {
                    // do nothing
                }
                else
                {
                    lbl_name.Text = dt.Rows[0]["Employee_Name"].ToString();
                    lbl_name1.Text = dt.Rows[0]["Employee_Name"].ToString();
                    lbl_email.Text = dt.Rows[0]["Email"].ToString();
                    lbl_mobile.Text = dt.Rows[0]["Mobile"].ToString();
                    lbl_address.Text = dt.Rows[0]["Address"].ToString();


                    bind_suer_details();





                }
            }
            catch
            {
            }

        }

        private void bind_suer_details()
        {

            string sql = "select * from user_details where user_id ='" + ViewState["id"].ToString() + "'";
            DataTable dt = code.FillTable(sql);

            if (dt.Rows.Count == 0)
            {
                // do nothing
            }
            else
            {
                lbl_yourself.Text = dt.Rows[0]["About_Teacher"].ToString();
                if (dt.Rows[0]["ProfilePhoto"].ToString() == "")
                {
                    img.Src = "/images/blank.png";
                    ViewState["photo"] = "/images/blank.png";
                }
                else
                {
                    img.Src = dt.Rows[0]["ProfilePhoto"].ToString();
                    ViewState["photo"] = dt.Rows[0]["ProfilePhoto"].ToString();
                }

                if (dt.Rows[0]["Signatur"].ToString() == "")
                {
                    ViewState["photo_sig"] = "/images/dummysig.png";
                    imgsig.Src = "/images/dummysig.png";
                }
                else
                {
                    imgsig.Src = dt.Rows[0]["Signatur"].ToString();
                    ViewState["photo_sig"] = dt.Rows[0]["Signatur"].ToString();
                }

            }
        }

        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            btn_empty();


        }

        private void btn_empty()
        {
            lbl_name1.Visible = false;
            lbl_email.Visible = false;
            lbl_mobile.Visible = false;
            lbl_address.Visible = false;
            lbl_yourself.Visible = false;


            txt_name.Visible = true;
            txt_emailid.Visible = true;
            txt_mobileno.Visible = true;
            txt_address.Visible = true;
            txt_yourself.Visible = true;


            txt_name.Text = lbl_name1.Text;
            txt_emailid.Text = lbl_email.Text;
            txt_mobileno.Text = lbl_mobile.Text;
            txt_address.Text = lbl_address.Text;
            txt_yourself.Text = lbl_yourself.Text;
            lnk_edit.Visible = false;
            btn_submit.Visible = true;
            a1.Visible = true;
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (txt_name.Text == "")
            {
                Alert("Please enter name");
            }
            else if (txt_emailid.Text == "")
            {
                Alert("Please enter email id");
            }
            else if (txt_mobileno.Text == "")
            {
                Alert("Please enter mobile no. ");
            }
            else if (txt_address.Text == "")
            {
                Alert("Please enter address ");
            }
            else if (txt_yourself.Text == "")
            {
                Alert("Please enter about yourself ");
            }
            else
            {
                bool image = false;
                string img = "";
                if (FileUpload1.HasFile)
                {

                    img = upload_image(FileUpload1);
                    if (img == "")
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
                    img = ViewState["photo"].ToString();
                    image = true;
                }

                if (image == true)
                {

                    bool image_sig = false;
                    string img_sig = "";


                    if (FileUpload2.HasFile)
                    {

                        img_sig = upload_image(FileUpload2);
                        if (img == "")
                        {
                            image_sig = false;
                        }
                        else
                        {
                            image_sig = true;
                        }
                    }
                    else
                    {
                        img_sig = ViewState["photo_sig"].ToString();
                        image_sig = true;
                    }

                    if (image_sig == true)
                    {
                        SqlCommand cmd;

                        string strQuery = "Update user_details set  name=@name,mobile=@mobile,ProfilePhoto=@ProfilePhoto,About_Teacher=@About_Teacher,Signatur=@Signatur where UserID = '" + ViewState["id"].ToString() + "'";
                        cmd = new SqlCommand(strQuery);
                        cmd.Parameters.AddWithValue("@name", txt_name.Text);
                        cmd.Parameters.AddWithValue("@mobile", txt_mobileno.Text);
                        cmd.Parameters.AddWithValue("@ProfilePhoto", img);
                        cmd.Parameters.AddWithValue("@About_Teacher", txt_yourself.Text);
                        cmd.Parameters.AddWithValue("@Signatur", img_sig);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Alert("Your profile has been successfully updated");
                            btn_empty2();
                            find_name();
                        }
                        string strQuery1 = "Update PRL_Employee_Master set  Employee_Name=@Employee_Name,Email=@Email,Mobile=@Mobile,Address=@Address where Emp_Code = '" + ViewState["id"].ToString() + "'";
                        cmd = new SqlCommand(strQuery);
                        cmd.Parameters.AddWithValue("@Employee_Name", txt_name.Text);
                        cmd.Parameters.AddWithValue("@Email", txt_emailid.Text);
                        cmd.Parameters.AddWithValue("@Mobile", txt_mobileno.Text);
                        cmd.Parameters.AddWithValue("@Address", txt_address.Text);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Alert("Your profile has been successfully updated");
                            btn_empty2();
                            find_name();
                        }
                    }
                    else
                    {
                        Alert("Please choose the signature image");
                    }

                }
                else
                {
                    Alert("Please choose profile photo ");
                }




            }
        }



        private void btn_empty2()
        {
            lbl_name1.Visible = true;
            lbl_email.Visible = true;
            lbl_mobile.Visible = true;
            lbl_address.Visible = true;
            lbl_yourself.Visible = true;


            txt_name.Visible = false;
            txt_emailid.Visible = false;
            txt_mobileno.Visible = false;
            txt_address.Visible = false;
            txt_yourself.Visible = false;


            txt_name.Text = lbl_name1.Text;
            txt_emailid.Text = lbl_email.Text;
            txt_mobileno.Text = lbl_mobile.Text;
            txt_address.Text = lbl_address.Text;
            txt_yourself.Text = lbl_yourself.Text;
            lnk_edit.Visible = true;
            btn_submit.Visible = false;
            a1.Visible = false;
        }
        private string upload_image(FileUpload FileUploadimg)
        {


            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
            string dbfilepath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;

            int k = 0;
            if (FileUploadimg.HasFile)
            {
                if (FileUploadimg.FileBytes.Length < 500000)
                {
                    Session["WorkingImage"] = FileUploadimg.FileName;
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
                    Alert("Please reduce image size (Max 500KB)");


                }
            }
            else
            {



            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../UploadedImage/Document")).ToString();
                    FileUploadimg.SaveAs(path + "/" + Session["WorkingImage1"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    Alert("Image not save");

                }
            }
            else
            {
                Alert("Please select jpg and png image");

            }
            if (FileSaved)
            {
                string originalPath1 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                string fileName = Path.GetFileName(Session["WorkingImage1"].ToString());
                dbfilepath = originalPath1 + "/UploadedImage/Document/" + fileName;
            }
            return dbfilepath;
        }
    }
}