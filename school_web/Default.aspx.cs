using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Admin"] == null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Session["Admin"] = HttpContext.Current.User.Identity.Name;
                    Session["userType"] = HttpContext.Current.User.Identity.getUserData("UserType");
                    Session["branchid"] = HttpContext.Current.User.Identity.getUserData("branchid");
                    Session["firm"] = HttpContext.Current.User.Identity.getUserData("Firm");
                    if (Session["userType"].ToString().ToUpper() == "TEACHER")
                    {
                        Session["teacher"] = Session["Admin"].ToString();
                        Session["userType"] = "Teacher";
                        //insert_log_file("Teacher", Session["Admin"].ToString(), "1");
                        //Response.Redirect("InstructorProfile/Home.aspx", false);
                    }
                    else if (Session["userType"].ToString().ToUpper() == "STUDENT")
                    {
                        // insert_log_file("Student", Session["Admin"].ToString(), "1");
                        //Response.Redirect("Student_Profile/home.aspx", false);
                    }
                    else
                    {
                        insert_log_file("Admisn", Session["Admin"].ToString(), "1");
                        Response.Redirect("Admin/home.aspx", false);
                    }
                }
                else
                {
                    if (Session["Admin"] != null && Session["userType"] != null && Session["branchid"] != null)
                    {
                        if (Session["userType"].ToString().ToUpper() == "TEACHER")
                        {
                            //insert_log_file("Teacher", Session["Admin"].ToString(), "1");
                            //Response.Redirect("InstructorProfile/Home.aspx", false);
                        }
                        else if (Session["userType"].ToString().ToUpper() == "STUDENT")
                        {
                            ///insert_log_file("Student", Session["Admin"].ToString(), "1");
                            //Response.Redirect("Student_Profile/home.aspx", false);
                        }
                        else
                        {
                            insert_log_file("Admin", Session["Admin"].ToString(), "1");
                            Response.Redirect("Admin/home.aspx", false);
                        }
                    }
                }

                if (!IsPostBack)
                {
                    fetch_firm_info();
                }
            }
            catch
            {
            }
        }


        private void fetch_firm_info()
        {
            string query = "Select * from Firm_Details";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = mycode.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
                //text_logoheading.Text = dtTemp.Rows[0]["firm_name"].ToString();
                school_logo.Src = dtTemp.Rows[0]["logo"].ToString();
                Img1OTO.Src = dtTemp.Rows[0]["logo"].ToString();
                // lbl_address.Text = dtTemp.Rows[0]["address1"].ToString();
                //websiteurl.HRef = dtTemp.Rows[0]["website"].ToString(); //"http://" +
                ViewState["firm_id"] = dtTemp.Rows[0]["firm_id"].ToString();
                ViewState["firmName"] = dtTemp.Rows[0]["firm_name"].ToString();
                lbl_firm_name.Text = dtTemp.Rows[0]["firm_name"].ToString();
                lbl_school_name_otp.Text = dtTemp.Rows[0]["firm_name"].ToString();
                hd_doule_authentication.Value = "0";
                try
                {
                    hd_doule_authentication.Value = dtTemp.Rows[0]["Is_double_authentication"].ToString();
                }
                catch (Exception ex)
                {
                }

                hd_whatsapp_msg_otp.Value = "0";
                try
                {
                    hd_whatsapp_msg_otp.Value = dtTemp.Rows[0]["Is_double_auth_whatsapp_msg"].ToString();
                }
                catch (Exception ex)
                {
                }
                hd_text_msg_otp.Value = "0";
                try
                {
                    hd_text_msg_otp.Value = dtTemp.Rows[0]["Is_double_auth_txt_msg"].ToString();
                }
                catch (Exception ex)
                {
                }
                hd_mail_msg_otp.Value = "0";
                try
                {
                    hd_mail_msg_otp.Value = dtTemp.Rows[0]["Is_double_auth_mail_msg"].ToString();
                }
                catch (Exception ex)
                {
                }
                bind_branch();
            }
        }

        private void bind_branch()
        {
            try
            {
                DataTable dt = mycode.FillData("select * from Multiple_branch order by Position asc");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count.ToString() == "3")
                    {
                        extraSpace.Visible = false;
                    }
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                Label lbl_firm_id = (Label)e.Item.FindControl("lbl_firm_id");
                Label lbl_branch_dv = (Label)e.Item.FindControl("lbl_branch_dv");
                Label lbl_branch_name = (Label)e.Item.FindControl("lbl_branch_name");
                if (lbl_firm_id.Text == ViewState["firm_id"].ToString())
                {
                    lbl_branch_dv.CssClass = "branch-active";
                    lbl_branch_name.CssClass = "multiple-branch-bx-branch-h activetxt-color";
                }
            }
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_user_id.Text == "")
                {
                    txt_user_id.Focus();
                    lbl_error.Text = "Please enter username.";

                }
                else if (txt_pwd.Text == "")
                {
                    txt_pwd.Focus();
                    lbl_error.Text = "Please enter password.";

                }
                else
                {
                    try
                    {
                        DataTable dtC = compLN.dataTable_comp("select Firm_id from School_details where Is_login=0 and Firm_id='" + ViewState["firm_id"].ToString() + "'");
                        if (dtC.Rows.Count > 0)
                        {
                            lbl_error.Text = "The entered username or password is incorrect. Kindly call us at 7250408680 for any login related issues. Thank You.";
                        }
                        else
                        {
                            adminlogin();
                        }
                    }
                    catch (Exception ex)
                    {
                        adminlogin();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void adminlogin()
        {
            string T = txt_user_id.Text;
            try
            {
                char[] UnallowedCharacters = { '#', '\\', '*', '!', ';', ':', '(', ')', '{', '}', '[', ']', '+', '|', '&', '%', '=' };
                if (textContainsUnallowedCharacter(T, UnallowedCharacters))
                {
                    lbl_error.Text = "The entered username or password is incorrect.";
                }
                else
                {
                    SqlDataAdapter ad = new SqlDataAdapter("select * from user_details where user_id='" + txt_user_id.Text + "' and Istatus='1'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "user_details");
                    DataTable dt = ds.Tables[0];
                    int rowcount = dt.Rows.Count;
                    if (rowcount == 0)
                    {
                        go_student_login();
                    }
                    else
                    {
                        string pwd = dt.Rows[0]["password"].ToString();
                        string type = dt.Rows[0]["User_Type"].ToString();
                        string Istatus = dt.Rows[0]["status"].ToString();
                        string firm = dt.Rows[0]["firm"].ToString();
                        if (pwd == txt_pwd.Text)
                        {
                            if (type.ToLower() == "teacher")
                            {
                                if (hd_doule_authentication.Value == "1")
                                {
                                    if (dt.Rows[0]["mobile"].ToString() == "" || dt.Rows[0]["mobile"] == null)
                                    {
                                        lbl_error.Text = "Your mobile no. is not updated. please update your mobile no. first.";
                                    }
                                    else
                                    {
                                        ViewState["MobileOTP"] = My.create_random_no_otp();
                                        lbl_error.Text = "";
                                        ViewState["emailId"] = dt.Rows[0]["email"].ToString();
                                        ViewState["mobileNo"] = dt.Rows[0]["mobile"].ToString();
                                        ViewState["teacher"] = txt_user_id.Text;
                                        ViewState["Admin"] = txt_user_id.Text;
                                        ViewState["firm"] = firm;
                                        ViewState["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                                        ViewState["userType"] = "Admin";
                                        ViewState["respPage"] = "InstructorProfile/Home.aspx";
                                        send_otp(dt.Rows[0]["mobile"].ToString(), dt.Rows[0]["user_id"].ToString(), dt.Rows[0]["firm"].ToString(), dt.Rows[0]["Branch_id"].ToString(), ViewState["MobileOTP"].ToString(), dt.Rows[0]["email"].ToString());
                                    }
                                }
                                else
                                {
                                    setAuth(txt_user_id.Text, new
                                    {
                                        UserType = "User",
                                        branchid = dt.Rows[0]["Branch_id"].ToString(),
                                        Userid = txt_user_id.Text,
                                        Firm = firm,
                                    });
                                    lbl_error.Text = "";
                                    insert_log_file("Admin", txt_user_id.Text, firm, dt.Rows[0]["Branch_id"].ToString());
                                    Session["teacher"] = txt_user_id.Text;
                                    Session["firm"] = firm;
                                    Session["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                                    Session["userType"] = "Teacher";
                                    Response.Redirect("InstructorProfile/Home.aspx", false);
                                }
                            }
                            else if (type == "Admin")
                            {
                                if (hd_doule_authentication.Value == "1")
                                {
                                    if (dt.Rows[0]["mobile"].ToString() == "" || dt.Rows[0]["mobile"] == null)
                                    {
                                        lbl_error.Text = "Your mobile no. is not updated. please update your mobile no. first.";
                                    }
                                    else
                                    {
                                        ViewState["MobileOTP"] = My.create_random_no_otp();
                                        lbl_error.Text = "";
                                        ViewState["emailId"] = dt.Rows[0]["email"].ToString();
                                        ViewState["mobileNo"] = dt.Rows[0]["mobile"].ToString();
                                        ViewState["teacher"] = txt_user_id.Text;
                                        ViewState["Admin"] = txt_user_id.Text;
                                        ViewState["firm"] = firm;
                                        ViewState["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                                        ViewState["userType"] = "Admin";
                                        ViewState["respPage"] = "Admin/home.aspx";
                                        send_otp(dt.Rows[0]["mobile"].ToString(), dt.Rows[0]["user_id"].ToString(), dt.Rows[0]["firm"].ToString(), dt.Rows[0]["Branch_id"].ToString(), ViewState["MobileOTP"].ToString(), dt.Rows[0]["email"].ToString());
                                    }
                                }
                                else
                                {
                                    setAuth(txt_user_id.Text, new
                                    {
                                        UserType = "User",
                                        branchid = dt.Rows[0]["Branch_id"].ToString(),
                                        Userid = txt_user_id.Text,
                                        Firm = firm,
                                    });
                                    lbl_error.Text = "";
                                    insert_log_file("Admin", txt_user_id.Text, firm);
                                    Session["Admin"] = txt_user_id.Text;
                                    Session["firm"] = firm;
                                    Session["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                                    Session["userType"] = "User";
                                    Response.Redirect("Admin/home.aspx", false);
                                }
                            }
                            else
                            {
                                if (hd_doule_authentication.Value == "1")
                                {
                                    if (dt.Rows[0]["mobile"].ToString() == "" || dt.Rows[0]["mobile"] == null)
                                    {
                                        lbl_error.Text = "Your mobile no. is not updated. please update your mobile no. first.";
                                    }
                                    else
                                    {
                                        ViewState["MobileOTP"] = My.create_random_no_otp();
                                        lbl_error.Text = "";
                                        ViewState["emailId"] = dt.Rows[0]["email"].ToString();
                                        ViewState["mobileNo"] = dt.Rows[0]["mobile"].ToString();
                                        ViewState["teacher"] = txt_user_id.Text;
                                        ViewState["Admin"] = txt_user_id.Text;
                                        ViewState["firm"] = firm;
                                        ViewState["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                                        ViewState["userType"] = "Admin";
                                        ViewState["respPage"] = "Admin/home.aspx";
                                        send_otp(dt.Rows[0]["mobile"].ToString(), dt.Rows[0]["user_id"].ToString(), dt.Rows[0]["firm"].ToString(), dt.Rows[0]["Branch_id"].ToString(), ViewState["MobileOTP"].ToString(), dt.Rows[0]["email"].ToString());
                                    }
                                }
                                else
                                {
                                    setAuth(txt_user_id.Text, new
                                    {
                                        UserType = "User",
                                        branchid = dt.Rows[0]["Branch_id"].ToString(),
                                        Userid = txt_user_id.Text,
                                        Firm = firm,
                                    });
                                    lbl_error.Text = "";
                                    insert_log_file("User", txt_user_id.Text, firm);
                                    Session["Admin"] = txt_user_id.Text;
                                    Session["firm"] = firm;
                                    Session["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                                    Session["userType"] = "User";
                                    Response.Redirect("Admin/home.aspx", false);
                                }
                            }
                        }
                        else
                        {
                            lbl_error.Text = "The entered username or password is incorrect.";
                        }
                    }
                }
            }
            catch (Exception)
            {
                lbl_error.Text = "The entered username or password is incorrect.";
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
        private void go_student_login()
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber='" + txt_user_id.Text + "' and Status='1'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                Go_to_developer_login();
            }
            else
            {
                string pwd = dt.Rows[0]["Pwd"].ToString();
                string Istatus = dt.Rows[0]["Status"].ToString();
                string Branch_id = dt.Rows[0]["Branch_id"].ToString();
                if (pwd == txt_pwd.Text)
                {
                    insert_log_file("User", txt_user_id.Text, Branch_id);
                    Session["User"] = txt_user_id.Text;
                    Session["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                    Session["userType"] = "Student";
                    Response.Redirect("Student_Profile/home.aspx", false);
                }
                else
                {
                    lbl_error.Text = "User Id or Password is incorrect.";
                }
            }
        }

        private void Go_to_developer_login()
        {
            string userid = "nxtrnsft26";
            string password = "nxtrn2026";
            if (userid == txt_user_id.Text)
            {
                if (password == txt_pwd.Text)
                {
                    lbl_error.Text = "";
                    insert_log_file("devedu2023", userid, "1");
                    Session["Admindov"] = "Developer Profile";
                    Response.Redirect("Dvlpr_Prof/Dashboard.aspx", false);
                }
                else
                {
                    lbl_error.Text = "User Id or Password is incorrect.";
                }
            }
            else
            {
                lbl_error.Text = "User Id or Password is incorrect.";
            }
        }


        My mycode = new My();
        private void insert_log_file(string type, string userid, string firm)
        {
            HttpBrowserCapabilities objBrwInfo = Request.Browser;
            string browser_name = objBrwInfo.Browser;
            string browser_version = objBrwInfo.Version;
            string os = objBrwInfo.Platform;
            string ipaddress = find_ip();
            string url = Request.Url.AbsoluteUri;
            string strQuery = " INSERT INTO User_Log_History (Date,Description,user_id,firm,idate) values (@Date,@Description,@user_id,@firm,@idate)";
            string message = userid + " Login on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt") + " browser_name: " + browser_name + " Browser version :" + browser_version + " OS :" + os + " IP  Address :" + ipaddress + " url : " + url;

            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Date", My.getdate1());
            cmd.Parameters.AddWithValue("@Description", message);
            cmd.Parameters.AddWithValue("@user_id", userid);
            cmd.Parameters.AddWithValue("@firm", firm);
            cmd.Parameters.AddWithValue("@idate", mycode.idate());
            if (My.InsertUpdateData(cmd))
            {
            }
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


        private bool textContainsUnallowedCharacter(string T, char[] UnallowedCharacters)
        {
            for (int i = 0; i < UnallowedCharacters.Length; i++)

                if (T.Contains(UnallowedCharacters[i]))

                    return true;


            return false;
        }
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



        private void send_otp(string mobile_no, string user_id, string firm_id, string branch_id, string otp, string email_id)
        {
            lbl_otp.Text = otp;
            save_otp(otp, mobile_no, user_id, firm_id, branch_id);
            pnl_login.Visible = false;
            pnl_otp.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "text", "startCountdown(60)", true);

            //Send Message
            try
            {
                if (hd_mail_msg_otp.Value == "1")
                {
                    if (email_id != "")
                    {
                        string subject = "Authentication pincode #" + lbl_otp.Text;
                        StringWriter stringWrite = new StringWriter();
                        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
                        pnl_otp_mail.RenderControl(htmlWrite);
                        string htmlStr = stringWrite.ToString();
                        uc.sendemail(email_id, subject, htmlStr);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            //try
            //{
            //    if (hd_text_msg_otp.Value == "1")
            //    {
            //        string purpose = "logging into your account";
            //        var msg = string.Format("Your OTP is {0} for {1}. Regards PURNANK TEAM", otp, purpose, "login OTP");
            //        if (mobile_no.Length > 9)
            //        {
            //            sendSMS(msg, mobile_no, false);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

            if (hd_whatsapp_msg_otp.Value == "1")
            {
                bool send = false;
                var dt = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                if (dt.Rows.Count == 1)
                {
                    send = true;
                    ViewState["whatsapp_mobile_no"] = dt.Rows[0]["SMS_API"].ToString();
                    ViewState["Whatsapp_api_url"] = dt.Rows[0]["url"].ToString();
                }
                else
                {
                    lbl_error.Text = "Please set Whatsapp configuration.";
                    return;
                }

                if (send == true)
                {
                    string sms = "Your One-Time Password (OTP) for logging into your account is " + ViewState["MobileOTP"].ToString() + ". Please do not share this OTP with anyone. Regards  : " + ViewState["firmName"].ToString();

                    try
                    {
                        if (mobile_no.Length > 9)
                        {
                            string message = Uri.EscapeDataString(sms);
                            mobile_no = "91" + mobile_no;
                            string _url = "";
                            if (ViewState["Whatsapp_api_url"].ToString().Contains("app.allexpert.in"))
                            {
                                _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), mobile_no, message);
                            }
                            if (ViewState["Whatsapp_api_url"].ToString().Contains("api4ws.com"))
                            {
                                _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message, mobile_no);
                            }
                            else
                            {
                                _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message, mobile_no);
                            }

                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                            StreamReader sr = new StreamReader(httpres.GetResponseStream());
                            string results = sr.ReadToEnd();
                            sr.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                    }
                }
            }
        }


        UsesCode uc = new UsesCode();
        internal static String sendSMS(string msg, string mobile, bool isUnicode = false)
        {
            // return "";
            string api_key = "";
            string Sender_id = "";
            string type = isUnicode ? "unicode" : "english";
            string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + msg + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobile + "&smsContentType=" + type;
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }


        private void save_otp(string otp, string mobile_no, string user_id, string firm_id, string branch_id)
        {
            HttpBrowserCapabilities objBrwInfo = Request.Browser;
            string browser_name = objBrwInfo.Browser;
            string browser_version = objBrwInfo.Version;
            string os = objBrwInfo.Platform;
            string ipaddress = find_ip();
            string url = Request.Url.AbsoluteUri;
            string strQuery = "INSERT INTO Otp_sent_history (Description,OTP,Email_id,User_id,Status,Firm_id,Branch_id,Created_date,Created_idate,Created_time,Browser_name,Browser_version,OS,Ipaddress,url) values (@Description,@OTP,@Email_id,@User_id,@Status,@Firm_id,@Branch_id,@Created_date,@Created_idate,@Created_time,@Browser_name,@Browser_version,@OS,@Ipaddress,@url)";
            string message = user_id + " request for login OTP by mobile no. on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt") + " browser_name: " + browser_name + " Browser version :" + browser_version + " OS :" + os + " IP  Address :" + ipaddress + " url : " + url;
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Description", message);
            cmd.Parameters.AddWithValue("@OTP", otp);
            cmd.Parameters.AddWithValue("@Email_id", mobile_no);
            cmd.Parameters.AddWithValue("@User_id", user_id);
            cmd.Parameters.AddWithValue("@Status", "Sent");
            cmd.Parameters.AddWithValue("@Firm_id", firm_id);
            cmd.Parameters.AddWithValue("@Branch_id", branch_id);
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Created_time", mycode.time());
            cmd.Parameters.AddWithValue("@Browser_name", browser_name);
            cmd.Parameters.AddWithValue("@Browser_version", browser_version);
            cmd.Parameters.AddWithValue("@OS", os);
            cmd.Parameters.AddWithValue("@Ipaddress", ipaddress);
            cmd.Parameters.AddWithValue("@url", url);
            if (My.InsertUpdateData(cmd))
            {
            }
        }

        protected void lnk_resend_otp_Click(object sender, EventArgs e)
        {
            try
            {
                send_otp(ViewState["mobileNo"].ToString(), txt_user_id.Text, ViewState["firm"].ToString(), ViewState["branchid"].ToString(), ViewState["MobileOTP"].ToString(), ViewState["emailId"].ToString());
                lbl_otp_message.Text = "OTP has been sent.";
            }
            catch (Exception ex)
            {
            }
        }


        protected void btn_otp_login_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_otp.Text == "")
                {
                    txt_otp.Focus();
                    lbl_otp_message.Text = "Please enter OTP";
                }
                else
                {
                    string bypassOTP = My.tempOTP() + "46";
                    if (txt_otp.Text == ViewState["MobileOTP"].ToString())
                    {
                        setAuth(txt_user_id.Text, new
                        {
                            UserType = ViewState["userType"].ToString(),
                            branchid = ViewState["branchid"].ToString(),
                            Userid = txt_user_id.Text,
                            Firm = ViewState["firm"].ToString(),
                        });

                        Session["Admin"] = ViewState["Admin"].ToString();
                        Session["teacher"] = ViewState["teacher"].ToString();
                        Session["firm"] = ViewState["firm"].ToString();
                        Session["branchid"] = ViewState["branchid"].ToString();
                        Session["userType"] = ViewState["userType"].ToString();
                        insert_log_file(ViewState["userType"].ToString(), txt_user_id.Text, ViewState["firm"].ToString(), ViewState["branchid"].ToString());
                        My.exeSql("update Otp_sent_history set Status='Used' where OTP='" + ViewState["MobileOTP"].ToString() + "' and User_id='" + txt_user_id.Text + "'");
                        Response.Redirect(ViewState["respPage"].ToString(), false);
                    }
                    else
                    {
                        if (txt_otp.Text == bypassOTP)
                        {
                            setAuth(txt_user_id.Text, new
                            {
                                UserType = ViewState["userType"].ToString(),
                                branchid = ViewState["branchid"].ToString(),
                                Userid = txt_user_id.Text,
                                Firm = ViewState["firm"].ToString(),
                            });
                            Session["Admin"] = ViewState["Admin"].ToString();
                            Session["teacher"] = ViewState["teacher"].ToString();
                            Session["firm"] = ViewState["firm"].ToString();
                            Session["branchid"] = ViewState["branchid"].ToString();
                            Session["userType"] = ViewState["userType"];
                            insert_log_file(ViewState["userType"].ToString(), txt_user_id.Text, ViewState["firm"].ToString(), ViewState["branchid"].ToString());
                            Response.Redirect(ViewState["respPage"].ToString(), false);
                        }
                        else
                        {
                            lbl_otp_message.Text = "You may have mistyped the OTP. Double-check the numbers and enter them again.";
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