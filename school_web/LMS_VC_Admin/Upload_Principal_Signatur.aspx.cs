using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web.LMS_VC_Admin
{
    public partial class Upload_Principal_Signatur : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind_Principal_signature();
            }
        }
        UsesCode code = new UsesCode();
        private void bind_Principal_signature()
        {
            string query = "Select *   from Principal_Signatur";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {

                imgsig.Src = "/images/blank.png";
                btn_Submit.Text = "Add";
            }
            else
            {
                hd_id.Value = dtTemp.Rows[0]["Id"].ToString();
                btn_Submit.Text = "Update";
                imgsig.Src = dtTemp.Rows[0]["Signatur"].ToString();
                hd_photo.Value = dtTemp.Rows[0]["Signatur"].ToString();
            }
        }

        public void Alert(string Message)
        {

            lblmessage.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {


            if (btn_Submit.Text == "Add")
            {
                string imagePath = Getimage();
                SqlCommand cmd;
                string query = "INSERT INTO Principal_Signatur (Signatur,Date,Idate,time) values (@Signatur,@Date,@Idate,@time)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Signatur", imagePath);
                cmd.Parameters.AddWithValue("@Date",code.date());
                cmd.Parameters.AddWithValue("@Idate", code.idate());
                cmd.Parameters.AddWithValue("@time", code.time());
          


                if (InsertUpdate.InsertUpdateData(cmd))
                {

                    Alert("Signature has been successfully uploaded");

                }

            }
            else
            {

                string imagePath = Getimage();
                if (imagePath == "")
                {

                }
                else
                {
                    string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                    string[] New_originalPath1 = originalPath2.Split('?');
                    string originalPath1 = New_originalPath1[0].ToString();
                    if (originalPath1 == imagePath)
                    {
                    }
                    else
                    {
                        hd_photo.Value = imagePath;
                    }
                }
                SqlCommand cmd;
                string query = "Update Principal_Signatur set Signatur=@Signatur,Date=@Date,Idate=@Idate,time=@time where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                cmd.Parameters.AddWithValue("@Signatur", imagePath);
                cmd.Parameters.AddWithValue("@Date", code.date());
                cmd.Parameters.AddWithValue("@Idate", code.idate());
                cmd.Parameters.AddWithValue("@time", code.time());

                if (InsertUpdate.InsertUpdateData(cmd))
                {

                    Alert("Signature has been successfully updated");

                }

            }
            bind_Principal_signature();
        }

        private string Getimage()
        {
            string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
            string[] New_originalPath1 = originalPath2.Split('?');
            string originalPath1 = New_originalPath1[0].ToString();

            string Path = originalPath1 + code.UploadImage(FileUpload1, "/UploadedImage/Principal/");//code.UploadAudio(fl_Audio, "/UploadedImage/AudioFile/");
            return Path;
        }
    }
}