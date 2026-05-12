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
    public partial class Change_password : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from dbo.[admission_registor] where admissionserialnumber ='" + Session["User"].ToString() + "'";
                DataTable dt = code.FillTable(query);
                int rowcount = dt.Rows.Count;
                if (rowcount == 0)
                {
                    Alert("Enter User id and password is not valid.");
                }
                else
                {

                    string pwd = dt.Rows[0]["Password"].ToString();
                    if (pwd != txt_old_password.Text)
                    {
                        Alert("Enter valid old password.");
                    }
                    else 
                    {
                        if (txt_new_password.Text != txt_confirm_password.Text)
                        {
                            Alert("new password & confirm password must be same");
                            return;
                        }

                        SqlCommand cmd = new SqlCommand("Update dbo.[admission_registor] set Pwd='" + txt_new_password.Text + "'  where admissionserialnumber='" + Session["User"].ToString() + "'");
                        InsertUpdate.InsertUpdateData(cmd);

                        Alert("Password has been changed successfully.");
                    }

                }

              
            }
            catch
            {
            }
        }
    }
}