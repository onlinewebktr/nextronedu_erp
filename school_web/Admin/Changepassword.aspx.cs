using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Admin
{
    public partial class chnagepassword : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "chnagepassword");
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
        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_oldpassword.Text == "")
            {
                Alertme("Please enter current password", "warning");


            }
            else if (txt_newpaswword.Text == "")
            {
                Alertme("Please enter new password", "warning");

            }
            else if (txt_reenterpassword.Text == "")
            {
                Alertme("Please enter new password", "warning");

            }
            else
            {
                if (txt_newpaswword.Text == txt_reenterpassword.Text)
                {

                    change_admin_password();


                }
                else
                {
                    Alertme("New password and confirm password didn't  match", "warning");

                }
            }
        }

        private void change_admin_password()
        {
            SqlConnection con = new SqlConnection(connection.conn);
            SqlCommand cmd;

            SqlDataAdapter ad = new SqlDataAdapter("select * from user_details where  user_id= @user_id and password= @password ", con);

            ad.SelectCommand.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
            ad.SelectCommand.Parameters.AddWithValue("@password", txt_oldpassword.Text);
            DataSet ds = new DataSet();
            ad.Fill(ds, "user_details");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Alertme("Invalid Current Password", "warning");
                
            }
            else
            {
                string query = "Update user_details set password=@password where    user_id= @user_id ";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@password", txt_reenterpassword.Text);
                cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    PayrollMy.execute("update HR_UserProfile set Password='" + txt_reenterpassword.Text + "' where EmployeeCode ='" + ViewState["Userid"].ToString() + "'");

                    PayrollMy.execute("update HR_Employee_Master set Password='" + txt_reenterpassword.Text + "' where Emp_Code ='" + ViewState["Userid"].ToString() + "'");
                    Alertme("Password has been successfully changed", "success");


                    Alertme("Password has been successfully changed", "success");
                    
                    FormReset();

                }
            }
        }

        private void FormReset()
        {
            txt_oldpassword.Text = "";
            txt_reenterpassword.Text = "";
            txt_newpaswword.Text = "";
        }
    }
}