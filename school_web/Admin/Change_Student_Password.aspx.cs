using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Change_Student_Password : System.Web.UI.Page
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
                        ViewState["session_id"] = My.get_session_id();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Change_Student_Password");
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
            if (txt_admission_no.Text == "")
            {
                Alertme("Please enter admission no.", "warning");


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

            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where  admissionserialnumber= @admissionserialnumber and Session_id=@Session_id ", con);

            ad.SelectCommand.Parameters.AddWithValue("@admissionserialnumber", txt_admission_no.Text);
            ad.SelectCommand.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
            DataSet ds = new DataSet();
            ad.Fill(ds, "LoginMaster");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Alertme("Invalid student admission no.", "warning");

            }
            else
            {
                string query = "Update admission_registor set Pwd=@Pwd where   admissionserialnumber=@admissionserialnumber and Session_id=@Session_id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Pwd", txt_reenterpassword.Text);
                cmd.Parameters.AddWithValue("@admissionserialnumber", txt_admission_no.Text);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    try
                    {
                        Alertme("Password has been Changed Successfully", "success");

                        txt_reenterpassword.Text = "";
                        txt_admission_no.Text = "";
                        txt_newpaswword.Text = "";

                    }
                    catch
                    {
                    }

                }
            }
        }
    }
}