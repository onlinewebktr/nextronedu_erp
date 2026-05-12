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
    public partial class ChangePassword : System.Web.UI.Page
    {
        string scrpt;
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
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
                if (!IsPostBack)
                {
                    hdfUserID.Value = Session["Admin"].ToString();
                }


            }
        }

        protected void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtcurrentPassword.Text == "")
            {
                lblmessage.Text = "Please enter current password";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txtnewpassword.Text == "")
            {
                lblmessage.Text = "Please enter new password";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txtretypepassword.Text == "")
            {
                lblmessage.Text = "Please enter new password";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {
                if (txtnewpassword.Text == txtretypepassword.Text)
                {

                    change_admin_password();


                }
                else
                {
                    lblmessage.Text = "New password and confirm password didn't  match";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

                }
            }
        }
        private void change_admin_password()
        {
            SqlConnection con = new SqlConnection(connection.conn);
            SqlCommand cmd;

            SqlDataAdapter ad = new SqlDataAdapter("select * from LoginMaster where  UserId= @UserId and Pswd= @Pswd and Type='Admin'", con);

            ad.SelectCommand.Parameters.AddWithValue("@UserId", hdfUserID.Value);
            ad.SelectCommand.Parameters.AddWithValue("@Pswd", txtcurrentPassword.Text);
            DataSet ds = new DataSet();
            ad.Fill(ds, "LoginMaster");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                lblmsg.Text = "Invalid Current Password";
            }
            else
            {
                string query = "Update LoginMaster set Pswd=@Pswd where   UserId=@UserId";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Pswd", txtretypepassword.Text);
                cmd.Parameters.AddWithValue("@UserId", hdfUserID.Value);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    lblmessage.Text = "Password Successfully Changed";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    FormReset();

                }
            }
        }

        private void FormReset()
        {
            txtcurrentPassword.Text = "";
            txtnewpassword.Text = "";
            txtretypepassword.Text = "";
        }

        
    }
}