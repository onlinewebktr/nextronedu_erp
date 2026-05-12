using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Change_Student_Password : System.Web.UI.Page
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

                hdfUserID.Value = Session["Admin"].ToString();


            }
        }

        protected void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtstudent_admission_no.Text == "")
            {
                lblmessage.Text = "Please enter student admission no.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txtnewpassword.Text == "")
            {
                lblmessage.Text = "Please enter new password";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }

            else
            {


                change_admin_password();



            }
        }
        private void change_admin_password()
        {
            SqlConnection con = new SqlConnection(connection.conn);
            SqlCommand cmd;

            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where  admissionserialnumber= @admissionserialnumber ", con);

            ad.SelectCommand.Parameters.AddWithValue("@admissionserialnumber", txtstudent_admission_no.Text);

            DataSet ds = new DataSet();
            ad.Fill(ds, "LoginMaster");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                lblmsg.Text = "Invalid student admission no.";
            }
            else
            {
                string query = "Update admission_registor set Password=@Password where   admissionserialnumber=@admissionserialnumber";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Password", txtnewpassword.Text);
                cmd.Parameters.AddWithValue("@admissionserialnumber", txtstudent_admission_no.Text);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    try
                    {
                        SqlCommand cmd1;
                        string query2 = "Update UserRegistrationMaster set Password=@Password where   reg_id=@reg_id and type='STUDENT'";
                        cmd1 = new SqlCommand(query2, con);
                        cmd1.Parameters.AddWithValue("@Password", txtnewpassword.Text);
                        cmd1.Parameters.AddWithValue("@reg_id", txtstudent_admission_no.Text);
                        if (InsertUpdate.InsertUpdateData_onlinetest(cmd1))
                        {
                            lblmessage.Text = "Password has been Changed Successfully";
                            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                            FormReset();
                        }

                    }
                    catch
                    {
                    }


                    lblmessage.Text = "Password has been Changed Successfully";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    FormReset();




                }
            }
        }

        private void FormReset()
        {

            txtnewpassword.Text = "";
            txtstudent_admission_no.Text = "";
        }

    }
}