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
    public partial class change_password : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
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
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
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

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_old_password.Text == "")
                {
                    Alertme("Please enter old password.", "warning");
                    txt_old_password.Focus();
                }
                else if (txt_new_password.Text == "")
                {
                    Alertme("Please enter new password.", "warning");
                    txt_new_password.Focus();
                }
                else if (txt_confirm_password.Text == "")
                {
                    Alertme("Please enter confirm password.", "warning");
                    txt_confirm_password.Focus();
                }
                else
                {
                    string query = "select * from admission_registor where admissionserialnumber ='" + Session["User"].ToString() + "'";
                    DataTable dt = code.FillTable(query);
                    int rowcount = dt.Rows.Count;
                    if (rowcount == 0)
                    {
                        Alertme("Enter User id and password is not valid.", "warning");
                    }
                    else
                    { 
                        string pwd = dt.Rows[0]["Pwd"].ToString();
                        if (pwd != txt_old_password.Text)
                        {
                            Alertme("Enter valid old password.", "warning");
                        }
                        else
                        {
                            if (txt_new_password.Text != txt_confirm_password.Text)
                            {
                                Alertme("new password & confirm password must be same", "warning");
                                return;
                            }

                            SqlCommand cmd = new SqlCommand("Update dbo.[admission_registor] set Pwd='" + txt_new_password.Text + "'  where admissionserialnumber='" + Session["User"].ToString() + "'");
                            InsertUpdate.InsertUpdateData(cmd);

                            Alertme("Password has been changed successfully.", "success");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}