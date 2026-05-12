using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace school_web.Admin.webServices
{
    /// <summary>
    /// Summary description for BackGroundProcess
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class BackGroundProcess : System.Web.Services.WebService
    {
        UsesCode code = new UsesCode();
        [WebMethod]
        public void send_pushNotification()
        {
            if (!code.syncdatastatusyes_or_no())
            {
                try
                {
                    bool abc1 = PayrollMy.insert_HR_Attendance_log();
                    bool abc = PayrollMy.update_HR_Daily_Attendance_Record("Device Attendance");
                    bool abc2 = PayrollMy.update_one_Row_data_Attendance_Record();
                    synch_data();
                }
                catch (Exception ex)
                {
                    UsesCode.submitexception(ex.ToString());
                }
            }
            //--------------------------------Student attndance----------------- 
            try
            {
                fetch_student_attndance();
            }
            catch
            {

            }
          

        }


        public static List<school_web.Global.mysmslis> smsList = new List<school_web.Global.mysmslis>();
        public static List<school_web.Global.myclass> class1 = new List<school_web.Global.myclass>();
        public static List<school_web.Global.sendpush> push = new List<school_web.Global.sendpush>();
        public static List<school_web.Global.sendpushMessage> pushMessage = new List<school_web.Global.sendpushMessage>();
        public static List<school_web.Global.SendpushNoticeTeacher> pushNoticeboard_teacher = new List<school_web.Global.SendpushNoticeTeacher>();
        public static List<school_web.Global.SendpushMessageTeacher> pushmessage_teacher = new List<school_web.Global.SendpushMessageTeacher>();
        public static List<school_web.Global.SendpushMessageAttandance> pushmessage_attandance = new List<school_web.Global.SendpushMessageAttandance>();

        public static List<school_web.Global.update_Student_Payment_History> paymnet_history = new List<school_web.Global.update_Student_Payment_History>();
        public static List<school_web.Global.sendpushEvents> Eventspush = new List<school_web.Global.sendpushEvents>();

        public static List<school_web.Global.sendlog_book> log_book = new List<school_web.Global.sendlog_book>();
        public bool flagstudent = true;
        public bool flagclass = true;
        public bool flagsendpush = true;
        public bool flagsendpushMesaage = true;
        public bool flagsendpush_notice_Teacher = true;
        public bool flagsendpush_message_Teacher = true;
        public bool flagsendpush_message_attandance = true;
        public bool flagshtudent_paymnet_history = true;
        public static List<school_web.Global.Sendclass_Activity> class_Activity = new List<school_web.Global.Sendclass_Activity>();
        public static List<school_web.Global.sendpushcalendar> pushcalendar = new List<school_web.Global.sendpushcalendar>();
        public static List<school_web.Global.sendpushSyllubsh> Syllubsh = new List<school_web.Global.sendpushSyllubsh>();
        public static List<school_web.Global.sendpushChairman> Chairman = new List<school_web.Global.sendpushChairman>();
        public static List<school_web.Global.sendpushSCHOOLHISTORY> SCHOOLHISTORY = new List<school_web.Global.sendpushSCHOOLHISTORY>();

        public static List<school_web.Global.sendpushPRINCIPAL> Principal = new List<school_web.Global.sendpushPRINCIPAL>();
        public static List<school_web.Global.sendpushstudent_routine> student_routine = new List<school_web.Global.sendpushstudent_routine>();
        public bool flagclass_Activity = true;
        public bool flagEventspush = true;
        public bool flaglog_bookpush = true;
        public bool flaglog_calendarpush = true;
        public bool flaglog_Syllubsh = true;
        public bool flaglog_Chairman = true;
        public bool flaglog_schooldetails = true;
        public bool flaglog_Principal = true;
        public bool flaglog_subjectroutine = true;
        public bool flaglog_subjectassined = true;
        public static List<school_web.Global.Sendpushpush_subjectassined> subjectassined = new List<school_web.Global.Sendpushpush_subjectassined>();
        private void synch_data()
        {
            Dictionary<string, object> dc1 = My.get_push_credantial();
            string type = (String)dc1["type"];
            string project_id = (String)dc1["project_id"];
            string private_key_id = (String)dc1["private_key_id"];
            string client_email = (String)dc1["client_email"];
            string client_id = (String)dc1["client_id"];
            string private_key = dc1["private_key"].ToString().Replace("\\n", "\n");
            try
            {
                if (flagshtudent_paymnet_history == false)
                {
                    return;
                }
                else
                {
                    bind_list_student_paymnet();
                    foreach (school_web.Global.update_Student_Payment_History item in paymnet_history.ToList())
                    {
                        if (item.parameter_New == "0")
                        {
                            flagshtudent_paymnet_history = true;
                        }
                        else
                        {
                            flagshtudent_paymnet_history = false;
                            send_paymnet_history(item);
                        }
                    }
                    flagshtudent_paymnet_history = true;

                }
            }
            catch
            {
            }

            if (flaglog_Syllubsh == false)
            {
                return;
            }
            else
            {
                Bind_push_list.bind_Syllubsh_data(Syllubsh);
                foreach (school_web.Global.sendpushSyllubsh item in Syllubsh.ToList())
                {
                    if (item.Class_id == "0")
                    {
                        flaglog_Syllubsh = true;
                    }
                    else
                    {
                        flaglog_Syllubsh = false;
                        Bind_push_list.send_pushSyllubsh_student(item, type, project_id, private_key_id, client_email, client_id, private_key);
                    }
                }
                flaglog_Syllubsh = true;
            }


            if (flaglog_bookpush == false)
            {
                return;
            }
            else
            {
                Bind_push_list.bind_log_book(log_book);
                foreach (school_web.Global.sendlog_book item in log_book.ToList())
                {
                    if (item.Class_id == "0")
                    {
                        flaglog_bookpush = true;
                    }
                    else
                    {
                        flaglog_bookpush = false;
                        Bind_push_list.send_pushlogbook_student(item, type, project_id, private_key_id, client_email, client_id, private_key);
                    }
                }
                flaglog_bookpush = true;
            }
            if (flagEventspush == false)
            {
                return;
            }
            else
            {
                Bind_push_list.bind_News_Events_Details(Eventspush);
                foreach (school_web.Global.sendpushEvents item in Eventspush.ToList())
                {
                    if (item.Class == "0")
                    {
                        flagEventspush = true;
                    }
                    else
                    {
                        flagEventspush = false;
                        Bind_push_list.send_pushEvents_student(item, type, project_id, private_key_id, client_email, client_id, private_key);
                    }
                }
                flagEventspush = true;
            }

            if (flagclass_Activity == false)
            {
                return;
            }
            else
            {
                Bind_push_list.bind_class_Activity_push(class_Activity);
                foreach (school_web.Global.Sendclass_Activity item in class_Activity.ToList())
                {
                    if (item.Class_id == "0")
                    {
                        flagclass_Activity = true;
                    }
                    else
                    {
                        flagclass_Activity = false;
                        Bind_push_list.send_push_class_Activity(item, type, project_id, private_key_id, client_email, client_id, private_key);
                    }
                }
                flagclass_Activity = true;
            }


            if (flagsendpush == false)
            {
                return;
            }
            else
            {
                bind_list_push();
                foreach (school_web.Global.sendpush item in push.ToList())
                {
                    if (item.Class1 == "0")
                    {
                        flagsendpush = true;
                    }
                    else
                    {
                        flagsendpush = false;
                        send_push(item, type, project_id, private_key_id, client_email, client_id, private_key);
                    }


                }
                flagsendpush = true;

            }

            if (flagsendpushMesaage == false)
            {
                return;
            }
            else
            {
                bind_list_pushmessage();
                foreach (school_web.Global.sendpushMessage item in pushMessage.ToList())
                {
                    if (item.Class1 == "0")
                    {
                        flagsendpushMesaage = true;
                    }
                    else
                    {
                        flagsendpushMesaage = false;
                        send_pushMessage(item, type, project_id, private_key_id, client_email, client_id, private_key);
                    }
                }
                flagsendpushMesaage = true;
            }


            if (flagsendpush_notice_Teacher == false)
            {
                return;
            }
            else
            {
                bind_list_push_notice_teacher();
                foreach (school_web.Global.SendpushNoticeTeacher item in pushNoticeboard_teacher.ToList())
                {
                    if (item.Teacher_Id == "0")
                    {
                        flagsendpush_notice_Teacher = true;
                    }
                    else
                    {
                        flagsendpush_notice_Teacher = false;
                        send_push_notice_teacher(item, type, project_id, private_key_id, client_email, client_id, private_key);
                    }


                }
                flagsendpush_notice_Teacher = true;

            }

            if (flagsendpush_message_Teacher == false)
            {
                return;
            }
            else
            {
                bind_list_push_message_teacher();
                foreach (school_web.Global.SendpushMessageTeacher item in pushmessage_teacher.ToList())
                {
                    if (item.Teacher_Id == "0")
                    {
                        flagsendpush_message_Teacher = true;
                    }
                    else
                    {
                        flagsendpush_message_Teacher = false;
                        send_push_message_teacher(item, type, project_id, private_key_id, client_email, client_id, private_key);
                    }


                }
                flagsendpush_message_Teacher = true;

            }

            //--------------------------------push send_push attndance-----------------
            if (flagsendpush_message_attandance == false)
            {
                return;
            }
            else
            {
                bind_list_push_attndance();
                foreach (school_web.Global.SendpushMessageAttandance item in pushmessage_attandance.ToList())
                {
                    if (item.Id == "0")
                    {
                        flagsendpush_message_attandance = true;
                    }
                    else
                    {
                        flagsendpush_message_attandance = false;
                        send_push_attandance(item, type, project_id, private_key_id, client_email, client_id, private_key);
                    }
                }
                flagsendpush_message_attandance = true;

            } 
        }

        My mycode = new My();
        private void fetch_student_attndance()
        {
            DataTable dtF = My.dataTable("select Is_student_attendance_biomatric from Firm_Details");
            if (dtF.Rows.Count > 0)
            {
                if (dtF.Rows[0]["Is_student_attendance_biomatric"].ToString() == "True")
                {
                    #region ComenteD
                    //DataTable dtattcheck = My.dataTable("select top 1 Session_id from Student_Attendance_saved_Class_Wise where Attendance_IDate='" + mycode.idate() + "'");
                    //if (dtattcheck.Rows.Count == 0)
                    //{
                    //    DataTable dtstd = My.dataTable("select admissionserialnumber,Session_id,Class_id,Section from admission_registor where Session_id='" + My.get_session_id() + "' and Status='1'");
                    //    if (dtstd.Rows.Count > 0)
                    //    {
                    //        string day = code.getdayname(mycode.date());
                    //        foreach (DataRow dr in dtstd.Rows)
                    //        {
                    //            string strQuery = "INSERT INTO Student_Attendance_saved_Class_Wise (Session_id,Class_id,Section,Class_period,Day,Attendance_Date,Attendance_IDate,Date,Idate,time,Created_By,Attendance_Status,Admission_no) values (@Session_id,@Class_id,@Section,@Class_period,@Day,@Attendance_Date,@Attendance_IDate,@Date,@Idate,@time,@Created_By,@Attendance_Status,@Admission_no)";
                    //            SqlCommand cmd;
                    //            cmd = new SqlCommand(strQuery);
                    //            cmd.Parameters.AddWithValue("@Session_id", dr["Session_id"].ToString());
                    //            cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                    //            cmd.Parameters.AddWithValue("@Section", dr["Section"].ToString());
                    //            cmd.Parameters.AddWithValue("@Class_period", "0");
                    //            cmd.Parameters.AddWithValue("@Day", day);
                    //            cmd.Parameters.AddWithValue("@Attendance_Date", mycode.date());
                    //            cmd.Parameters.AddWithValue("@Attendance_IDate", mycode.idate());
                    //            cmd.Parameters.AddWithValue("@Date", code.date());
                    //            cmd.Parameters.AddWithValue("@Idate", code.idate());
                    //            cmd.Parameters.AddWithValue("@time", code.time());
                    //            cmd.Parameters.AddWithValue("@Created_By", "admin");
                    //            cmd.Parameters.AddWithValue("@Attendance_Status", "Absent");
                    //            cmd.Parameters.AddWithValue("@Admission_no", dr["admissionserialnumber"].ToString());
                    //            if (InsertUpdate.InsertUpdateData(cmd))
                    //            {
                    //            }
                    //        }
                    //    }
                    //}


                    //DataTable dt = My.dataTable("select *, Format(convert(DateTime,DateTime,103), 'dd/MM/yyyy') as date, Format(convert(DateTime,DateTime,103), 'yyyyMMdd') as idate, Format(convert(DateTime,DateTime,103), 'hh:mm:ss tt') as time from Student_Attendance_Log  where (Status='' or Status is null)");
                    //if (dt.Rows.Count > 0)
                    //{
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        string day = code.getdayname(dr["date"].ToString());
                    //        DataTable dts = My.dataTable("select admissionserialnumber,Session_id,Class_id,Section from admission_registor where Session_id='" + My.get_session_id() + "' and Status='1' and admissionserialnumber='" + dr["admissionserialnumber"].ToString() + "'");
                    //        if (dts.Rows.Count > 0)
                    //        {
                    //            if (mycode.IsUserExist("select Id from Student_Attendance_saved_Class_Wise where Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Attendance_IDate='" + dr["idate"].ToString() + "'"))
                    //            {
                    //                string strQuery = "INSERT INTO Student_Attendance_saved_Class_Wise (Session_id,Class_id,Section,Class_period,Day,Attendance_Date,Attendance_IDate,Date,Idate,time,Created_By,Attendance_Status,Admission_no) values (@Session_id,@Class_id,@Section,@Class_period,@Day,@Attendance_Date,@Attendance_IDate,@Date,@Idate,@time,@Created_By,@Attendance_Status,@Admission_no)";
                    //                SqlCommand cmd;
                    //                cmd = new SqlCommand(strQuery);
                    //                cmd.Parameters.AddWithValue("@Session_id", dts.Rows[0]["Session_id"].ToString());
                    //                cmd.Parameters.AddWithValue("@Class_id", dts.Rows[0]["Class_id"].ToString());
                    //                cmd.Parameters.AddWithValue("@Section", dts.Rows[0]["Section"].ToString());
                    //                cmd.Parameters.AddWithValue("@Class_period", "0");
                    //                cmd.Parameters.AddWithValue("@Day", day);
                    //                cmd.Parameters.AddWithValue("@Attendance_Date", dr["date"].ToString());
                    //                cmd.Parameters.AddWithValue("@Attendance_IDate", dr["idate"].ToString());
                    //                cmd.Parameters.AddWithValue("@Date", code.date());
                    //                cmd.Parameters.AddWithValue("@Idate", code.idate());
                    //                cmd.Parameters.AddWithValue("@time", dr["time"].ToString());
                    //                cmd.Parameters.AddWithValue("@Created_By", "admin");
                    //                cmd.Parameters.AddWithValue("@Attendance_Status", "Present");
                    //                cmd.Parameters.AddWithValue("@Admission_no", dts.Rows[0]["admissionserialnumber"].ToString());
                    //                if (InsertUpdate.InsertUpdateData(cmd))
                    //                {
                    //                    My.exeSql("update Student_Attendance_Log set Status='Sync' where Id='" + dr["Id"].ToString() + "'");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                My.exeSql("update Student_Attendance_Log set Status='Sync' where Id='" + dr["Id"].ToString() + "'");
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion



                    //======================================= 
                    string session_id = My.get_session_id();
                    DataTable dtd = My.dataTable("select distinct Format(convert(DateTime,DateTime,103), 'dd/MM/yyyy') as date,Format(convert(DateTime,DateTime,103), 'yyyyMMdd') as idate from Student_Attendance_Log where (Status='' or Status is null)");
                    if (dtd.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtd.Rows)
                        {
                            string day = code.getdayname(dr["date"].ToString());
                            DataTable dts = My.dataTable("select admissionserialnumber,Session_id,Class_id,Section from admission_registor where Session_id='" + session_id + "' and Status='1'");
                            if (dts.Rows.Count > 0)
                            {
                                foreach (DataRow drstd in dts.Rows)
                                {
                                    DataTable dtchk = My.dataTable("select *, Format(convert(DateTime,DateTime,103), 'hh:mm:ss tt') as time from Student_Attendance_Log where admissionserialnumber='" + drstd["admissionserialnumber"].ToString() + "' and Format(convert(DateTime,DateTime,103), 'yyyyMMdd')='" + dr["idate"].ToString() + "'");
                                    if (dtchk.Rows.Count > 0) //Persent
                                    {
                                        if (mycode.IsUserExist("select Id from Student_Attendance_saved_Class_Wise where Admission_no='" + drstd["admissionserialnumber"].ToString() + "' and Attendance_IDate='" + dr["idate"].ToString() + "'"))
                                        {
                                            string strQuery = "INSERT INTO Student_Attendance_saved_Class_Wise (Session_id,Class_id,Section,Class_period,Day,Attendance_Date,Attendance_IDate,Date,Idate,time,Created_By,Attendance_Status,Admission_no) values (@Session_id,@Class_id,@Section,@Class_period,@Day,@Attendance_Date,@Attendance_IDate,@Date,@Idate,@time,@Created_By,@Attendance_Status,@Admission_no)";
                                            SqlCommand cmd;
                                            cmd = new SqlCommand(strQuery); 
                                            cmd.Parameters.AddWithValue("@Session_id", session_id);
                                            cmd.Parameters.AddWithValue("@Class_id", drstd["Class_id"].ToString());
                                            cmd.Parameters.AddWithValue("@Section", drstd["Section"].ToString());
                                            cmd.Parameters.AddWithValue("@Class_period", "0");
                                            cmd.Parameters.AddWithValue("@Day", day);
                                            cmd.Parameters.AddWithValue("@Attendance_Date", dr["date"].ToString());
                                            cmd.Parameters.AddWithValue("@Attendance_IDate", dr["idate"].ToString());
                                            cmd.Parameters.AddWithValue("@Date", code.date());
                                            cmd.Parameters.AddWithValue("@Idate", code.idate());
                                            cmd.Parameters.AddWithValue("@time", dtchk.Rows[0]["time"].ToString());
                                            cmd.Parameters.AddWithValue("@Created_By", "admin");
                                            cmd.Parameters.AddWithValue("@Attendance_Status", "Present");
                                            cmd.Parameters.AddWithValue("@Admission_no", drstd["admissionserialnumber"].ToString());
                                            if (InsertUpdate.InsertUpdateData(cmd))
                                            {
                                                My.exeSql("update Student_Attendance_Log set Status='Sync' where admissionserialnumber='" + drstd["admissionserialnumber"].ToString() + "' and Format(convert(DateTime,DateTime,103), 'yyyyMMdd')='" + dr["idate"].ToString() + "'");
                                            }
                                        }
                                        else
                                        {
                                            My.exeSql("update Student_Attendance_saved_Class_Wise set time='" + dtchk.Rows[0]["time"].ToString() + "', Attendance_Status='Present' where Admission_no='" + drstd["admissionserialnumber"].ToString() + "' and Attendance_IDate='" + dr["idate"].ToString() + "';   update Student_Attendance_Log set Status='Sync' where admissionserialnumber='" + drstd["admissionserialnumber"].ToString() + "' and Format(convert(DateTime,DateTime,103), 'yyyyMMdd')='" + dr["idate"].ToString() + "'");
                                        }
                                    }
                                    else  //Absent
                                    {
                                        if (mycode.IsUserExist("select Id from Student_Attendance_saved_Class_Wise where Admission_no='" + drstd["admissionserialnumber"].ToString() + "' and Attendance_IDate='" + dr["idate"].ToString() + "'"))
                                        {
                                            string strQuery = "INSERT INTO Student_Attendance_saved_Class_Wise (Session_id,Class_id,Section,Class_period,Day,Attendance_Date,Attendance_IDate,Date,Idate,time,Created_By,Attendance_Status,Admission_no) values (@Session_id,@Class_id,@Section,@Class_period,@Day,@Attendance_Date,@Attendance_IDate,@Date,@Idate,@time,@Created_By,@Attendance_Status,@Admission_no)";
                                            SqlCommand cmd;
                                            cmd = new SqlCommand(strQuery);
                                            cmd.Parameters.AddWithValue("@Session_id", session_id);
                                            cmd.Parameters.AddWithValue("@Class_id", drstd["Class_id"].ToString());
                                            cmd.Parameters.AddWithValue("@Section", drstd["Section"].ToString());
                                            cmd.Parameters.AddWithValue("@Class_period", "0");
                                            cmd.Parameters.AddWithValue("@Day", day);
                                            cmd.Parameters.AddWithValue("@Attendance_Date", dr["date"].ToString());
                                            cmd.Parameters.AddWithValue("@Attendance_IDate", dr["idate"].ToString());
                                            cmd.Parameters.AddWithValue("@Date", code.date());
                                            cmd.Parameters.AddWithValue("@Idate", code.idate());
                                            cmd.Parameters.AddWithValue("@time", code.time());
                                            cmd.Parameters.AddWithValue("@Created_By", "admin");
                                            cmd.Parameters.AddWithValue("@Attendance_Status", "Absent");
                                            cmd.Parameters.AddWithValue("@Admission_no", drstd["admissionserialnumber"].ToString());
                                            if (InsertUpdate.InsertUpdateData(cmd))
                                            {
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void send_paymnet_history(Global.update_Student_Payment_History item)
        {
            My.exeSql("update Student_Payment_History set parameter_New='" + item.parameter_New + "' where Slip_no='" + item.slip_id + "' ");
        }





        #region send push message to student
        private void send_pushMessage(school_web.Global.sendpushMessage item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            string query = "";
            //Send_Type = dr["Send_Type"].ToString(),
            //           Admission_no = dr["Admission_no"].ToString(),
            if (item.Send_Type == "Class Wise")
            {
                if (item.Class1 == "ALL")
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor]  where Session_id='" + My.get_session_id() + "'  ";
                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Section='" + item.Section + "' and Session_id='" + My.get_session_id() + "'";
                    }
                }
                else
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Class_id='" + item.Class1 + "' and Session_id='" + My.get_session_id() + "'";

                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Class_id='" + item.Class1 + "' and Session_id='" + My.get_session_id() + "' and Section='" + item.Section + "' Session_id='" + My.get_session_id() + "'";
                    }
                }
            }
            else
            {
                query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   admissionserialnumber='" + item.Admission_no + "' and Session_id='" + My.get_session_id() + "' ";
            }

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Private_Messages set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {
                code.executequery("update Private_Messages set Send_Status='Send' where Id=" + item.Id + "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    final_send_pushmessage(gcmid, admissionserialnumber, item, type, project_id, private_key_id, client_email, client_id, private_key);
                }
            }
        }

        private void final_send_pushmessage(string gcmid, string admissionserialnumber, school_web.Global.sendpushMessage item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            if (gcmid == "")
            {
                gcmid = "0";
            }
            if (gcmid != "")
            {
                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = item.Details;
                ss["title"] = item.Subject;
                ss["messagetype"] = "Message";
                ss["url"] = "";
                ss["link_url"] = "";
                ss["UserId"] = admissionserialnumber;
                ss["type"] = type;
                ss["project_id"] = project_id;
                ss["private_key_id"] = private_key_id;
                ss["client_email"] = client_email;
                ss["client_id"] = client_id;
                ss["private_key"] = private_key;
                My.onlypush(gcmid, ss);
            }
        }

        private void bind_list_pushmessage()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Private_Messages] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushMessage.Clear();
                pushMessage.Add(new school_web.Global.sendpushMessage
                {
                    Class1 = "0",
                    Section = "0",
                    Subject = "0",
                    Details = "0",
                    Id = "0",
                    Send_Type = "0",
                    Admission_no = "0",
                });
            }
            else
            {
                pushMessage.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushMessage.Add(new school_web.Global.sendpushMessage
                    {
                        Class1 = dr["Class_Id"].ToString(),
                        Section = dr["Section_Id"].ToString(),
                        Subject = dr["Subject"].ToString(),
                        Details = dr["Details"].ToString(),
                        Id = dr["Id"].ToString(),
                        Send_Type = dr["Send_Type"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),
                    });
                }
            }
        }

        #endregion



        #region teacher push message for teacher
        private void send_push_message_teacher(Global.SendpushMessageTeacher item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            string query = "";

            if (item.Teacher_Id == "ALL")
            {
                query = " select gcm_id,user_id from dbo.[user_details] where (gcm_id is not null or gcm_id!='')";
            }
            else
            {
                query = " select gcm_id,user_id from dbo.[user_details] where   user_id='" + item.Teacher_Id + "'";
            }

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Private_Messages_For_Teacher set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {
                code.executequery("update Private_Messages_For_Teacher set Send_Status='Send' where Id=" + item.Id + "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string UserID = dt.Rows[i]["user_id"].ToString();
                    if (gcmid == "")
                    {
                        gcmid = "";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Details;
                        ss["title"] = item.Subject;
                        ss["messagetype"] = "Message";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = UserID;
                        ss["type"] = type;
                        ss["project_id"] = project_id;
                        ss["private_key_id"] = private_key_id;
                        ss["client_email"] = client_email;
                        ss["client_id"] = client_id;
                        ss["private_key"] = private_key;
                        My.onlypush(gcmid, ss);
                    }
                }
            }
        }

        private void bind_list_push_message_teacher()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Private_Messages_For_Teacher] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushmessage_teacher.Clear();
                pushmessage_teacher.Add(new school_web.Global.SendpushMessageTeacher
                {
                    Subject = "0",
                    Details = "0",
                    Id = "0",
                    Teacher_Id = "0",
                });
            }
            else
            {
                pushmessage_teacher.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushmessage_teacher.Add(new school_web.Global.SendpushMessageTeacher
                    {
                        Subject = dr["Subject"].ToString(),
                        Details = dr["Details"].ToString(),
                        Id = dr["Id"].ToString(),
                        Teacher_Id = dr["Teacher_Id"].ToString(),
                    });
                }
            }
        }
        #endregion




        #region teacher push bind notice
        private void bind_list_push_notice_teacher()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Notice_Board_Details_Teacher] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushNoticeboard_teacher.Clear();
                pushNoticeboard_teacher.Add(new school_web.Global.SendpushNoticeTeacher
                {
                    Notice = "0",
                    Id = "0",
                    Teacher_Id = "0",
                });
            }
            else
            {
                pushNoticeboard_teacher.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushNoticeboard_teacher.Add(new school_web.Global.SendpushNoticeTeacher
                    {
                        Notice = dr["Notice"].ToString(),
                        Id = dr["Id"].ToString(),
                        Teacher_Id = dr["Teacher_Id"].ToString(),
                    });
                }
            }
        }
        private void send_push_notice_teacher(Global.SendpushNoticeTeacher item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            string query = "";
            if (item.Teacher_Id == "ALL")
            {
                query = " select gcm_id,user_id from dbo.[user_details]  ";
            }
            else
            {
                query = " select gcm_id,user_id from dbo.[user_details] where   user_id='" + item.Teacher_Id + "'";
            }
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Notice_Board_Details_Teacher set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {
                code.executequery("update Notice_Board_Details_Teacher set Send_Status='Send' where Id=" + item.Id + "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string UserID = dt.Rows[i]["user_id"].ToString();
                    if (gcmid == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Notice;
                        ss["title"] = "Notice";
                        ss["messagetype"] = "Notice";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = UserID;
                        ss["type"] = type;
                        ss["project_id"] = project_id;
                        ss["private_key_id"] = private_key_id;
                        ss["client_email"] = client_email;
                        ss["client_id"] = client_id;
                        ss["private_key"] = private_key;
                        My.onlypush(gcmid, ss);
                    }
                }
            }
        }
        #endregion
        #region send push notice board
        private void send_push(school_web.Global.sendpush item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            string query = "";
            if (item.Send_Type == "Class Wise")
            {
                if (item.Class1 == "ALL")
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where Session_id='" + My.get_session_id() + "' ";
                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where Section='" + item.Section + "' and Session_id='" + My.get_session_id() + "'";
                    }
                }
                else
                {
                    if (item.Section == "ALL")
                    {
                        query = "select gcm_id,admissionserialnumber from dbo.[admission_registor] where Class_id='" + item.Class1 + "' and Session_id='" + My.get_session_id() + "'";
                    }
                    else
                    {
                        query = "select gcm_id,admissionserialnumber from dbo.[admission_registor] where Class_id='" + item.Class1 + "' and Section='" + item.Section + "' and Session_id='" + My.get_session_id() + "'";
                    }
                }
            }
            else
            {
                query = "select gcm_id,admissionserialnumber from dbo.[admission_registor] where admissionserialnumber='" + item.Admission_no + "' and Session_id='" + My.get_session_id() + "' ";
            }
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Notice_Board_Details set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {
                code.executequery("update Notice_Board_Details set Send_Status='Send' where Id=" + item.Id + "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    final_send_push(gcmid, admissionserialnumber, item, type, project_id, private_key_id, client_email, client_id, private_key);
                }
            }
        }

        private void final_send_push(string gcmid, string admissionserialnumber, school_web.Global.sendpush item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            if (gcmid == "")
            {
                gcmid = "0";
            }
            if (gcmid != "")
            {
                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = item.Notice;
                ss["title"] = "Notice";
                ss["messagetype"] = "Notice";
                ss["url"] = "";
                ss["link_url"] = "";
                ss["UserId"] = admissionserialnumber;
                ss["type"] = type;
                ss["project_id"] = project_id;
                ss["private_key_id"] = private_key_id;
                ss["client_email"] = client_email;
                ss["client_id"] = client_id;
                ss["private_key"] = private_key;
                My.onlypush(gcmid, ss);
            }
        }
        private void bind_list_student_paymnet()
        {
            SqlCommand cmd = new SqlCommand("select Slip_no,(select top 1 parameter from Monthly_Fee_Collection_Slip where slipno=Student_Payment_History.Slip_no) as parameter_New from dbo.[Student_Payment_History] where parameter_New is null");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                paymnet_history.Clear();
                paymnet_history.Add(new school_web.Global.update_Student_Payment_History
                {
                    slip_id = "0",
                    parameter_New = "0",
                });
            }
            else
            {
                paymnet_history.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    paymnet_history.Add(new school_web.Global.update_Student_Payment_History
                    {
                        slip_id = dr["Slip_no"].ToString(),
                        parameter_New = dr["parameter_New"].ToString(),
                    });
                }
            }
        }


        private void bind_list_push()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Notice_Board_Details] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                push.Clear();
                push.Add(new school_web.Global.sendpush
                {
                    Class1 = "0",
                    Section = "0",
                    Notice = "0",
                    Id = "0",
                    Send_Type = "0",
                    Admission_no = "0",
                });
            }
            else
            {
                push.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    push.Add(new school_web.Global.sendpush
                    {
                        Class1 = dr["Class"].ToString(),
                        Section = dr["Section"].ToString(),
                        Notice = dr["Notice"].ToString(),
                        Id = dr["Id"].ToString(),
                        Send_Type = dr["Send_Type"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),
                    });
                }
            }
        }
        #endregion

        private void bind_list_push_attndance()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Attendance_Notification] where Send_status=0 ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushmessage_attandance.Clear();
                pushmessage_attandance.Add(new school_web.Global.SendpushMessageAttandance
                {
                    Details = "0",
                    Id = "0",
                    AdmissionNo = "0",
                });
            }
            else
            {
                pushmessage_attandance.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushmessage_attandance.Add(new school_web.Global.SendpushMessageAttandance
                    {
                        Details = dr["Details"].ToString(),
                        Id = dr["Id"].ToString(),
                        AdmissionNo = dr["AdmissionNo"].ToString(),
                    });
                }
            }
        }

        private void send_push_attandance(Global.SendpushMessageAttandance item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            string query = "";
            query = " select top 1 gcm_id,admissionserialnumber from dbo.[admission_registor] where   admissionserialnumber='" + item.AdmissionNo + "' order by id desc";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Attendance_Notification set Send_status=1 where Id=" + item.Id + "");
            }
            else
            {
                code.executequery("update Attendance_Notification set Send_status=1 where Id=" + item.Id + "");
                string gcmid = dt.Rows[0]["gcm_id"].ToString();
                string admissionserialnumber = dt.Rows[0]["admissionserialnumber"].ToString();
                if (gcmid == "")
                {
                    gcmid = "0";
                }
                if (gcmid != "")
                {
                    Dictionary<String, String> ss = new Dictionary<string, string>();
                    ss["notification_id"] = Guid.NewGuid().ToString();
                    ss["message"] = item.Details;
                    ss["title"] = "Attendance Notification";
                    ss["messagetype"] = "Message";
                    ss["url"] = "";
                    ss["link_url"] = "";
                    ss["UserId"] = admissionserialnumber;
                    ss["type"] = type;
                    ss["project_id"] = project_id;
                    ss["private_key_id"] = private_key_id;
                    ss["client_email"] = client_email;
                    ss["client_id"] = client_id;
                    ss["private_key"] = private_key;
                    My.onlypush(gcmid, ss);
                }
            }
        }
    }
}
