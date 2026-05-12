using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Data;
using school_web.AppCode;
using System.Globalization;
namespace school_web.LMS_VC_Admin
{
    public partial class AdminLogin : System.Web.UI.Page
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

            }
        }
        public static string conn = UsesCode.cononlinetest;
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string T = txt_AdminUser.Text;
            try
            {
                char[] UnallowedCharacters = { '-', '#', '\'', '/', '\\', '*', '!', ';', ':', '(', ')', '{', '}', '[', ']', '+', '|', '&', '%', '=' };
                if (textContainsUnallowedCharacter(T, UnallowedCharacters))
                {
                    lblmessage.Text = "Wrong Entry";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
                else
                {

                    SqlDataAdapter ad = new SqlDataAdapter("select * from UserRegistrationMaster where email='" + txt_AdminUser.Text + "'  ", conn);// or mobile='" + txt_username.Text + "'  and status='1'
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    int rowcount = dt.Rows.Count;
                    if (rowcount == 0)
                    {
                        lblmessage.Text = "Email id or Password is incorrect.";
                        scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    }
                    else
                    {
                        bool loginstatus = find_expirydate_status();
                        if (loginstatus == true)
                        {
                            lblmessage.Text = "";
                            string pwd = dt.Rows[0]["password"].ToString();
                            string type = dt.Rows[0]["type"].ToString();
                            string status = dt.Rows[0]["status"].ToString();
                            if (pwd == txt_adminPasswd.Text)
                            {
                                if (type == "ADMIN")
                                {
                                    if (status == "1")
                                    {
                                        lblmessage.Text = "";
                                        Session["admin_onine_test"] = txt_AdminUser.Text;
                                        Response.Redirect("http://onlinetest.sfxschoolandal.com/masterAdmin1/Home.aspx");
                                    }
                                    else
                                    {
                                        lblmessage.Text = "Your Account is Deactivated";
                                        scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                                    }
                                }

                                else
                                {
                                    lblmessage.Text = "User Id or Password is incorrect.";
                                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                                }

                            }
                        }
                        else
                        {

                        }

                    }
                }
            }
            catch (Exception)
            {
                lblmessage.Text = "Email id or Password is incorrect.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
        }
        private bool textContainsUnallowedCharacter(string T, char[] UnallowedCharacters)
        {
            for (int i = 0; i < UnallowedCharacters.Length; i++)

                if (T.Contains(UnallowedCharacters[i]))

                    return true;


            return false;
        }

        public static bool find_expirydate_status()// login check
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string Datetimesystem = dtm.ToString("dd/MM/yyyy hh:mm:ss tt");
            string usersetdatetime = "31/03/2022 11:59:59 PM";
            DateTime startdate = DateTime.ParseExact(Datetimesystem, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            DateTime enddate = DateTime.ParseExact(usersetdatetime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            int result1 = DateTime.Compare(enddate, startdate);
            if (result1 >= 0)
            {
                return true;// form submit

            }
            else
            {
                return false;// form not submit
            }
        }
        
    }
}