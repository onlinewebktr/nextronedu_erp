using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web
{
    public partial class AutoLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            if (!string.IsNullOrEmpty(token))
            {
                try
                { 
                    var claims = JwtValidator.ValidateToken(token);
                    string userId = claims.Identity.Name;
                    // DB check (optional but recommended)
                    processToLogin(userId);
                }
                catch (Exception ex)
                {
                    My.submitexception(ex.ToString());
                    Response.Redirect("https://nninternational.org");
                }
            }
            else
            {
                Response.Redirect("https://nninternational.org");
            }
        }

        private void processToLogin(string userId)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from user_details where user_id='" + userId + "' and Istatus='1'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "user_details");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                go_student_login(userId);
            }
            else
            {
                string pwd = dt.Rows[0]["password"].ToString();
                string type = dt.Rows[0]["User_Type"].ToString();
                string Istatus = dt.Rows[0]["status"].ToString();
                string firm = dt.Rows[0]["firm"].ToString();
                if (type.ToLower() == "teacher")
                {
                    setAuth(userId, new
                    {
                        UserType = "User",
                        branchid = dt.Rows[0]["Branch_id"].ToString(),
                        Userid = userId,
                        Firm = firm,
                    });

                    insert_log_file("Admin", userId, firm, dt.Rows[0]["Branch_id"].ToString());
                    Session["teacher"] = userId;
                    Session["firm"] = firm;
                    Session["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                    Session["userType"] = "Teacher";
                    Response.Redirect("InstructorProfile/Home.aspx", false);

                }
                else if (type == "Admin")
                {
                    setAuth(userId, new
                    {
                        UserType = "User",
                        branchid = dt.Rows[0]["Branch_id"].ToString(),
                        Userid = userId,
                        Firm = firm,
                    });
                    insert_log_file("Admin", userId, firm, dt.Rows[0]["Branch_id"].ToString());
                    Session["Admin"] = userId;
                    Session["firm"] = firm;
                    Session["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                    Session["userType"] = "User";
                    Response.Redirect("Admin/home.aspx", false);
                }
                else
                {
                    setAuth(userId, new
                    {
                        UserType = "User",
                        branchid = dt.Rows[0]["Branch_id"].ToString(),
                        Userid = userId,
                        Firm = firm,
                    });
                    insert_log_file("User", userId, firm, dt.Rows[0]["Branch_id"].ToString());
                    Session["Admin"] = userId;
                    Session["firm"] = firm;
                    Session["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                    Session["userType"] = "User";
                    Response.Redirect("Admin/home.aspx", false);
                }
            }
        }
        private void go_student_login(string userId)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber='" + userId + "' and Status='1'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount > 0)
            {
                string pwd = dt.Rows[0]["Pwd"].ToString();
                string Istatus = dt.Rows[0]["Status"].ToString();
                string Branch_id = dt.Rows[0]["Branch_id"].ToString();

                insert_log_file("User", userId, dt.Rows[0]["Branch_id"].ToString(), Branch_id);
                Session["User"] = userId;
                Session["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                Session["userType"] = "Student";
                Response.Redirect("Student_Profile/home.aspx", false);
            }
            else
            {
                Response.Redirect("https://nninternational.org");
            }
        }

        My mycode = new My();
        private void insert_log_file(string type, string userid, string firm, string Branch_id)
        {
            try
            {
                HttpBrowserCapabilities objBrwInfo = Request.Browser;
                string browser_name = objBrwInfo.Browser;
                string browser_version = objBrwInfo.Version;
                string os = objBrwInfo.Platform;
                string ipaddress = find_ip();
                string url = Request.Url.AbsoluteUri;
                string strQuery = " INSERT INTO User_Log_History (Date,Description,user_id,firm,idate,Branch_id) values (@Date,@Description,@user_id,@firm,@idate,@Branch_id)";
                string message = userid + " Login on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt") + " browser_name: " + browser_name + " Browser version :" + browser_version + " OS :" + os + " IP  Address :" + ipaddress + " url : " + url;
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Date", My.getdate1());
                cmd.Parameters.AddWithValue("@Description", message);
                cmd.Parameters.AddWithValue("@user_id", userid);
                cmd.Parameters.AddWithValue("@firm", firm);
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            catch
            {
            }
        }
        private void setAuth(string user_id, object userData)
        {
            var user_data = (new JavaScriptSerializer()).Serialize(userData);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,                                       // Version
                user_id,                                // Username
                DateTime.Now,                            // Issue time
                DateTime.Now.AddHours(10),              // Expiry time
                false,                                   // Persistent
                user_data                                 // User-specific data
            );
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            Response.Cookies.Add(cookie);
            string[] roles = new string[] { "Admin" };
            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(ticket), roles);
        }
        private string find_ip(bool CheckForward = false)
        {
            string ip = null;
            if (CheckForward)
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }

            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            { // Using X-Forwarded-For last address
                ip = ip.Split(',')
                       .Last()
                       .Trim();
            }
            return ip;
        }

    }
}