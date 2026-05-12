using school_web.AppCode;
using school_web.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace school_web
{
    public class Global : System.Web.HttpApplication
    {

        public static List<mysmslis> smsList = new List<mysmslis>();
        public static List<myclass> class1 = new List<myclass>();
        public static List<sendpush> push = new List<sendpush>();
        public static List<sendpushMessage> pushMessage = new List<sendpushMessage>();
        public static List<SendpushNoticeTeacher> pushNoticeboard_teacher = new List<SendpushNoticeTeacher>();
        public static List<SendpushMessageTeacher> pushmessage_teacher = new List<SendpushMessageTeacher>();
        public bool flagstudent = true;
        public class mysmslis
        {
            public string classname { get; set; }
            public string admissionserialnumber { get; set; }
            public string rollnumber { get; set; }
            public string Section { get; set; }
            public string session { get; set; }
            public string gender { get; set; }
            public string fathername { get; set; }
            public string mobilenumber { get; set; }
            public string Class_id { get; set; }
            public string Password { get; set; }
            public string Original_Name { get; set; }
            public string profile_img { get; set; }
            public string Email { get; set; }

        }

        public bool flagclass = true;
        public bool flagsendpush = true;
        public class myclass
        {
            public string CategoryID { get; set; }
            public string CategoryName { get; set; }


        }
        public class sendpush
        {
            public string Notice { get; set; }
            public string Class1 { get; set; }
            public string Section { get; set; }
            public string Id { get; set; }
            public string Send_Type { get; set; }
            public string Admission_no { get; set; }

        }

        public class update_Student_Payment_History
        {
            public string slip_id { get; set; }
            public string parameter_New { get; set; }


        }

        public class Sendclass_Activity
        {
            public string Title { get; set; }
            public string Message_subject { get; set; }
            public string Session_id { get; set; }
            public string Class_id { get; set; }
            public string Section { get; set; }
            public string Subject_id { get; set; }
            public string Id { get; set; }
            public string sendstatus { get; set; }
        }
        public class SendpushNoticeTeacher
        {
            public string Notice { get; set; }
            public string Id { get; set; }
            public string Teacher_Id { get; set; }


        }

        public class sendpushEvents
        {
            public string Class { get; set; }
            public string Section { get; set; }
            public string Session_id { get; set; }
            public string Heading { get; set; }
            public string Details { get; set; }
            public string Id { get; set; }

        }
        public class sendlog_book
        {
            public string Class_id { get; set; }
            public string Section { get; set; }
            public string Session_id { get; set; }
            public string Remark { get; set; }
            public string Id { get; set; }

        }

        public class sendpushMessage
        {
            public string Subject { get; set; }
            public string Details { get; set; }
            public string Class1 { get; set; }
            public string Section { get; set; }
            public string Id { get; set; }
            public string Send_Type { get; set; }
            public string Admission_no { get; set; }
        }
        public class SendpushMessageTeacher
        {
            public string Subject { get; set; }
            public string Details { get; set; }
            public string Id { get; set; }
            public string Teacher_Id { get; set; }
        }
        public class SendpushMessageAttandance
        {
            public string Details { get; set; }
            public string Id { get; set; }
            public string AdmissionNo { get; set; }

        }
        public class sendpushcalendar
        {
            public string classid { get; set; }
            public string session_id { get; set; }
            public string Details { get; set; }
            public string Id { get; set; }

        }
        public class sendpushChairman
        {
            public string id { get; set; }

            public string Details { get; set; }


        }

        public class sendpushSCHOOLHISTORY
        {
            public string id { get; set; }

            public string Details { get; set; }


        }
        public class sendpushPRINCIPAL
        {
            public string id { get; set; }

            public string Details { get; set; }


        }
        public class sendpushstudent_routine
        {
            public string id { get; set; }
            public string Details { get; set; }
            public string Class_id { get; set; }
            public string section { get; set; }
            public string Session_id { get; set; }

        }
        public class sendpushSyllubsh
        {
            public string Session_id { get; set; }
            public string Class_id { get; set; }
            public string Section { get; set; }
            public string Subject_id { get; set; }

            public string Remarks { get; set; }
            public string Status { get; set; }
            public string Id { get; set; }

        }



        public bool flagsendpushMesaage = true;

        public static BackgroundWorker worker = new BackgroundWorker();

        public static bool stopWorker = false;
        public static int ID = 0;
        protected void Application_Start(object sender, EventArgs e)
        {

            //  AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            try
            {
                PurnMPC.MPC.registerFirm("sc", "Firm_Details", "firm_name", "address1", "address2", "email", "contact_no", "website");
                PurnMPC.MPC.RegisterMPC("sc", "~/Areas/hms/Views/Shared/_LayoutSite1.cshtml", "Session", "Admin");
            }
            catch
            {

            }
        }



        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 80;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }








        internal bool update_database_and_version()
        {
            bool tureturn = false;
            #region load version list
            load_version_list("1.0.0.", 100);
            load_version_list("1.0.1.", 100);
            #endregion

            try
            {
                FileVersionInfo fvi;
                string path1 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                fvi = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string version = "Version " + fvi.FileVersion;

                string db_ver = My.getdata("db_version");
                if (db_ver == "")
                {
                    if (My.session("version") == "")
                    {
                        My.setdata("db_version", fvi.FileVersion);
                        db_ver = fvi.FileVersion;
                    }
                    else
                    {
                        My.setdata("db_version", My.session("version"));
                        db_ver = My.session("version");
                    }
                }
                DataBaseVersion.current_version = fvi.FileVersion;
                Version cr_version = new Version(fvi.FileVersion);
                Version db_version = new Version(db_ver);
                var result = cr_version.CompareTo(db_version);
                if (result > 0)
                {

                    Update_Database(db_ver);
                    tureturn = true;
                }
                else
                {
                    tureturn = true;
                }
                return tureturn;
            }
            catch (Exception ex)
            {
                return true;
                //My.submitexception(ex.ToString());
            }
        }
        string db_version = "";
        int count = 0;
        private void Update_Database(string db_ver)
        {
            try
            {


                int version_index = My.version_list.IndexOf(db_ver);
                int current_version_index = My.version_list.IndexOf(DataBaseVersion.current_version);
                DataBaseVersion vdu = new DataBaseVersion();
                using (SqlConnection connector = new SqlConnection(My.conn))
                {
                    connector.Open();
                    using (SqlCommand Cmd = new SqlCommand())
                    {
                        Cmd.Connection = connector;
                        for (int i = version_index + 1; i <= current_version_index; i++)
                        {
                            string version = My.version_list[i];
                            List<String> query_list = vdu.get_query_list(version);
                            for (int j = 0; j < query_list.Count; j++)
                            {
                                string query = query_list[j];
                                try
                                {
                                    Cmd.CommandText = query;
                                    Cmd.ExecuteNonQuery();

                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        My.submitException(ex, " Version Code - " + db_ver + query);

                                    }
                                    catch
                                    {
                                    }


                                }
                            }
                        }
                        My.setdata("db_version", DataBaseVersion.current_version);
                    }
                    connector.Close();
                }
            }
            catch (Exception)
            {
                count++;
                Update_Database(db_ver);
            }
        }
        private void load_version_list(string prefix, int last)
        {
            for (int i = 1; i <= last; i++)
            {
                My.version_list.Add(prefix + i);
            }
        }


        My mycode = new My();
        internal bool send_birthday_message()
        {
            string admission_no2 = My.get_chek_message_send_studentid();

            string birthdaymessage = My.get_birthday_message();
            string school_name = My.get_school_name();
            string query = "";
            if (admission_no2 == "")
            {
                query = "select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id,dob,gcm_id,Session_id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + My.get_session_id() + "  and Status='1'     and  (dob like'%" + mycode.day_month_2() + "%' or dob like'%" + mycode.day_month() + "%')    ";
            }
            else
            {
                query = "select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id,dob,gcm_id,Session_id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + My.get_session_id() + "  and Status='1'     and  (dob like'%" + mycode.day_month_2() + "%' or dob like'%" + mycode.day_month() + "%')  and  admissionserialnumber not in (" + admission_no2 + ")  ";
            }
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {


            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {

                    string studentname = dr["studentname"].ToString();
                    string admission_no = dr["admissionserialnumber"].ToString();
                    string gcm_id = dr["gcm_id"].ToString();
                    string message = "Dear " + studentname + ", " + birthdaymessage + " Regrad " + school_name;
                    Dictionary<String, String> ss = new Dictionary<string, string>();
                    ss["notification_id"] = Guid.NewGuid().ToString();
                    ss["message"] = message;
                    ss["title"] = "Birthday";
                    ss["messagetype"] = "Message";
                    ss["url"] = "";
                    ss["link_url"] = "";
                    ss["UserId"] = admission_no;

                    UsesCode.SendNotification(gcm_id, ss);


                }

            }
            return true;
        }

        public static List<Sendpushpush_subjectassined> subjectassined = new List<Sendpushpush_subjectassined>();

        public class Sendpushpush_subjectassined
        {
            public string subjectname { get; set; }
            public string message { get; set; }

            public string id { get; set; }

        }
    }
}