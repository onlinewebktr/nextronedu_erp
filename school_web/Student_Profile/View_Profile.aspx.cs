using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class View_Profile : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["sesssionid"] = My.get_session_id();
                    BindDetails();
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindDetails()
        {
            string sql = "select * from dbo.[admission_registor] where admissionserialnumber='" + Session["User"].ToString() + "' and Transfer_Status in ('New','NT') and  StudentStatus='AV'   and Session_id=" + ViewState["sesssionid"].ToString() + " ";
            DataTable dt = code.FillTable(sql);
            txt_Name.Text = dt.Rows[0]["Original_Name"].ToString();
            txt_fathername.Text = dt.Rows[0]["fathername"].ToString();
            txt_mothername.Text = dt.Rows[0]["mothername"].ToString();
            txt_mobilenumber.Text = dt.Rows[0]["mobilenumber"].ToString();
            ddl_Gender.Text = dt.Rows[0]["gender"].ToString();
            txt_City.Text = dt.Rows[0]["city"].ToString();
            Image1.ImageUrl = dt.Rows[0]["profile_img"].ToString();
            if (dt.Rows[0]["profile_img"].ToString() != "")
                a1_img.HRef = dt.Rows[0]["profile_img"].ToString();

        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void btn_photo1_Click(object sender, EventArgs e)
        {
            try
            {
                string DocPath = GetPath();
                SqlCommand cmd = new SqlCommand("Update dbo.[admission_registor] set profile_img='" + DocPath + "'  where admissionserialnumber='" + Session["User"].ToString() + "' and Transfer_Status in ('New','NT') and  StudentStatus='AV'   and Session_id=" + ViewState["sesssionid"].ToString() + "  ");
                InsertUpdate.InsertUpdateData(cmd);

                Alert("Record saved successfully.");
            }
            catch
            {
            }
        }

        public string GetPath()
        {
            string Path = code.UploadPDF(fl_photo, "/UploadedImage/Document/");
            return Path;
        }
    }
}