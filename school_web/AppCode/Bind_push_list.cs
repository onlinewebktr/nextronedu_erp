using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class Bind_push_list
    {

        #region class activity
        internal static void bind_class_Activity_push(List<Global.Sendclass_Activity> class_Activity)
        {


            SqlCommand cmd = new SqlCommand(" select * from dbo.[Activity_Class_Details] where Send_status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                class_Activity.Clear();
                class_Activity.Add(new school_web.Global.Sendclass_Activity
                {
                    Class_id = "0",
                    Section = "0",
                    Subject_id = "0",
                    Title = "0",
                    Message_subject = "0",
                    Id = "0",
                    sendstatus = "0",




                });
            }
            else
            {
                class_Activity.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    class_Activity.Add(new school_web.Global.Sendclass_Activity
                    {
                        Class_id = dr["Class_id"].ToString(),
                        Section = dr["Section_data"].ToString(),
                        Subject_id = dr["Subject_id"].ToString(),
                        Id = dr["Id"].ToString(),
                        Title = dr["Title"].ToString(),
                        Message_subject = dr["Message_subject"].ToString(),


                    });
                }
            }
        }

        internal static void send_push_class_Activity(Global.Sendclass_Activity item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            UsesCode uc = new UsesCode();
            string query = "";
            query = " Select distinct ar.gcm_id,ar.admissionserialnumber,acd.Subject_id from dbo.[admission_registor] ar join Activity_Class_Details  acd on ar.Class_id=acd.Class_id and ar.Section=acd.Section_data   and ar.Session_id=acd.Session_id join Subject_Mapping_New sm on sm.Sub_id=acd.Subject_id and sm.Session_id=ar.Session_id and sm.Section=ar.Section and sm.Class_id=ar.Class_id and sm.Admission_no=ar.admissionserialnumber where acd.Class_id='" + item.Class_id + "' and acd.Section_data='" + item.Section + "' and acd.Subject_id='" + item.Subject_id + "'  and ar.Session_id='" + My.get_session_id() + "' ";


            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                uc.executequery("update Activity_Class_Details set Send_status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();

                    final_send_push(gcmid, admissionserialnumber, item, type, project_id, private_key_id, client_email, client_id, private_key);

                }
                uc.executequery("update Activity_Class_Details set Send_status='Send' where Id=" + item.Id + "");
            }
        }

        private static void final_send_push(string gcmid, string admissionserialnumber, Global.Sendclass_Activity item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            if (gcmid == "")
            {
                gcmid = "0";
            }
            if (gcmid != "")
            {
                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = item.Message_subject;
                ss["title"] = item.Title;
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

        #endregion




        // module


        // student events
        internal static void bind_News_Events_Details(List<Global.sendpushEvents> eventspush)
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[News_Events_Details] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                eventspush.Clear();
                eventspush.Add(new school_web.Global.sendpushEvents
                {

                    Class = "0",
                    Section = "0",
                    Session_id = "0",
                    Heading = "0",
                    Details = "0",
                    Id = "0",



                });
            }
            else
            {
                eventspush.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    eventspush.Add(new school_web.Global.sendpushEvents
                    {
                        Class = dr["Class"].ToString(),
                        Section = dr["Section"].ToString(),
                        Session_id = dr["Session_Id"].ToString(),
                        Id = dr["Id"].ToString(),
                        Heading = dr["Heading"].ToString(),
                        Details = dr["Details"].ToString(),
                    });
                }
            }
        }

        internal static void send_pushEvents_student(Global.sendpushEvents item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            UsesCode uc = new UsesCode();
            string query = "";


            if (item.Class == "ALL")
            {
                if (item.Section == "ALL")
                {
                    query = " select gcm_id,admissionserialnumber from dbo.[admission_registor]  and Session_id=" + item.Session_id + " ) ";
                }
                else
                {
                    query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Section='" + item.Section + "' and Session_id=" + item.Session_id + "";
                }

            }
            else
            {
                if (item.Section == "ALL")
                {
                    query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Class_id='" + item.Class + "' and Session_id=" + item.Session_id + " ";

                }
                else
                {
                    query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Session_id=" + item.Session_id + " and Class_id='" + item.Class + "' and Section='" + item.Section + "'";
                }

            }
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                uc.executequery("update News_Events_Details set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();




                    if (gcmid == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Details;
                        ss["title"] = item.Heading;
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
                uc.executequery("update News_Events_Details set Send_Status='Send' where Id=" + item.Id + "");
            }
        }


        // loog book
        internal static void bind_log_book(List<Global.sendlog_book> log_book)
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Teacher_log_book] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                log_book.Clear();
                log_book.Add(new school_web.Global.sendlog_book
                {

                    Class_id = "0",
                    Section = "0",
                    Session_id = "0",
                    Remark = "0",

                    Id = "0",



                });
            }
            else
            {
                log_book.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    log_book.Add(new school_web.Global.sendlog_book
                    {
                        Class_id = dr["Class_id"].ToString(),
                        Section = dr["Section"].ToString(),
                        Session_id = dr["Session_id"].ToString(),
                        Id = dr["Id"].ToString(),
                        Remark = dr["Remark"].ToString(),

                    });
                }
            }
        }

        internal static void send_pushlogbook_student(Global.sendlog_book item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
           

            UsesCode uc = new UsesCode();
            string query = "";
            query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Session_id=" + item.Session_id + " and Class_id='" + item.Class_id + "' and Section='" + item.Section + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                uc.executequery("update Teacher_log_book set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();




                    if (gcmid == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Remark;
                        ss["title"] = "Class Log Book";
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
                uc.executequery("update Teacher_log_book set Send_Status='Send' where Id=" + item.Id + "");
            }
        }
        // send calendar push
        internal static void send_pushcalendar_student(Global.sendpushcalendar item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            UsesCode uc = new UsesCode();
            string query = "";
            query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Session_id=" + item.session_id + " and Class_id='" + item.classid + "'  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                uc.executequery("update School_Holiday_Calendar set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    if (gcmid == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Details;
                        ss["title"] = "Update School Calendar";
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
                uc.executequery("update School_Holiday_Calendar set Send_Status='Send' where Id=" + item.Id + "");
            }

        }

        internal static void bind_calendar_data(List<Global.sendpushcalendar> pushcalendar)
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[School_Holiday_Calendar] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushcalendar.Clear();
                pushcalendar.Add(new school_web.Global.sendpushcalendar
                {

                    classid = "0",
                    session_id = "0",
                    Details = "0",

                    Id = "0",



                });
            }
            else
            {
                pushcalendar.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushcalendar.Add(new school_web.Global.sendpushcalendar
                    {
                        classid = dr["Class_id"].ToString(),

                        session_id = dr["Session_id"].ToString(),
                        Id = dr["Id"].ToString(),
                        Details = "Dear Student, your calendar is updated, Date " + dr["Date"].ToString() + " day " + dr["Day"].ToString() + " Details " + dr["Details"].ToString(),

                    });
                }
            }
        }
        //Syllubsh
        internal static void send_pushSyllubsh_student(Global.sendpushSyllubsh item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            UsesCode uc = new UsesCode();
            string query = "";
            query = " Select distinct ar.gcm_id,ar.admissionserialnumber,acd.Subject_id from dbo.[admission_registor] ar join Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher  acd on ar.Class_id=acd.Class_id and ar.Section=acd.Section   and ar.Session_id=acd.Session_id join Subject_Mapping_New sm on sm.Sub_id=acd.Subject_id and sm.Session_id=ar.Session_id and sm.Section=ar.Section and sm.Class_id=ar.Class_id and sm.Admission_no=ar.admissionserialnumber  where acd.Class_id='" + item.Class_id + "' and acd.Section='" + item.Section + "' and acd.Subject_id='" + item.Subject_id + "' and   acd.Session_id='" + item.Session_id + "'  ";


            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                uc.executequery("update Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher set Send_status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();

                    if (gcmid == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Remarks;
                        ss["title"] = "Syllabus Update";
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
                uc.executequery("update Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher set Send_status='Send' where Id=" + item.Id + "");
            }
        }

        internal static void bind_Syllubsh_data(List<Global.sendpushSyllubsh> Syllubsh)
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                Syllubsh.Clear();
                Syllubsh.Add(new school_web.Global.sendpushSyllubsh
                {

                    Session_id = "0",
                    Class_id = "0",
                    Section = "0",
                    Subject_id = "0",
                    Status = "0",
                    Id = "0",
                    Remarks = "0",

                });
            }
            else
            {
                Syllubsh.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    Syllubsh.Add(new school_web.Global.sendpushSyllubsh
                    {
                        Session_id = dr["Session_id"].ToString(),
                        Class_id = dr["Class_id"].ToString(),
                        Section = dr["Section"].ToString(),
                        Subject_id = dr["Subject_id"].ToString(),
                        Id = dr["Id"].ToString(),
                        //Dear Student, your syllabus is updated on, Date 
                        Remarks = "Dear Student, your syllabus is updated on date " + dr["Date"].ToString() + ", remarks " + dr["Remarks"].ToString() + ". status " + dr["Status"].ToString(),

                    });
                }
            }

        }
        //Chairman
        internal static void bind_Chairman_data(List<Global.sendpushChairman> chairman)
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Content_about_us_Chairman_Desk] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                chairman.Clear();
                chairman.Add(new school_web.Global.sendpushChairman
                {

                    id = "0",
                    Details = "0"

                }); ;
            }
            else
            {
                chairman.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    chairman.Add(new school_web.Global.sendpushChairman
                    {

                        id = dr["Id"].ToString(),
                        //Dear Student, your syllabus is updated on, Date 
                        Details = "Dear Student, chairman desk has been updated please check your chairman  desk",

                    });
                }
            }
        }

        internal static void send_pushChairman_student(Global.sendpushChairman item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            UsesCode uc = new UsesCode();
            string query = "";
            query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Session_id=" + My.get_session_id() + "   ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                uc.executequery("update Content_about_us_Chairman_Desk set Send_Status='Send' where Id=" + item.id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();




                    if (gcmid == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Details;
                        ss["title"] = "Chairman Desk ";
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
                uc.executequery("update Content_about_us_Chairman_Desk set Send_Status='Send' where Id=" + item.id + "");
            }
        }

        internal static void bind_Principal_data(List<Global.sendpushPRINCIPAL> principal)
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Content_about_us_Principal_Desk] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                principal.Clear();
                principal.Add(new school_web.Global.sendpushPRINCIPAL
                {

                    id = "0",
                    Details = "0"

                }); ;
            }
            else
            {
                principal.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    principal.Add(new school_web.Global.sendpushPRINCIPAL
                    {

                        id = dr["Id"].ToString(),
                        //Dear Student, your syllabus is updated on, Date 
                        Details = "Dear Student, principal desk has been updated please check your principal desk",

                    });
                }
            }
        }

        internal static void send_pushPrincipal_student(Global.sendpushPRINCIPAL item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            UsesCode uc = new UsesCode();
            string query = "";
            query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Session_id=" + My.get_session_id() + "   ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                uc.executequery("update Content_about_us_Principal_Desk set Send_Status='Send' where Id=" + item.id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();




                    if (gcmid == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Details;
                        ss["title"] = "Chairman Desk ";
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
                uc.executequery("update Content_about_us_Principal_Desk set Send_Status='Send' where Id=" + item.id + "");
            }
        }

        internal static void sendpushstudent_subjectassined(Global.Sendpushpush_subjectassined item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            UsesCode uc = new UsesCode();
            string query = "";
            query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Session_id=" + My.get_session_id() + "   ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                uc.executequery("update Subject_Mapping_New set Send_Status='Send' where Id=" + item.id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    if (gcmid == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.message;
                        ss["title"] = "Subject Assigned";
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
                uc.executequery("update Subject_Mapping_New set Send_Status='Send' where Id=" + item.id + "");
            }
        }

        internal static void bind_student_subjectassined(List<Global.Sendpushpush_subjectassined> subjectassined)
        {
            SqlCommand cmd = new SqlCommand("Select id,(select  top 1 Subject_name from Subject_Master where course_id=Subject_Mapping_New.Class_id and Subject_id=Subject_Mapping_New.Sub_id) as subjectname from Subject_Mapping_New  where Send_Status='Notsend'");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                subjectassined.Clear();
                subjectassined.Add(new school_web.Global.Sendpushpush_subjectassined
                {

                    id = "0",
                    subjectname = "0",
                    message = "0",

                });
            }
            else
            {
                subjectassined.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    subjectassined.Add(new school_web.Global.Sendpushpush_subjectassined
                    {
                        id = dr["id"].ToString(),
                        message = "Dear Student, your class subject has been assigned subject name is-" + dr["subjectname"].ToString(),
                    });
                }
            }
        }

        internal static void send_pushSCHOOLHISTORY_student(Global.sendpushSCHOOLHISTORY item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            UsesCode uc = new UsesCode();
            string query = "";
            query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Session_id=" + My.get_session_id() + "   ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                uc.executequery("update Content_about_us_School set Send_Status='Send' where Id=" + item.id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    if (gcmid == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Details;
                        ss["title"] = "About School ";
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
                uc.executequery("update Content_about_us_School set Send_Status='Send' where Id=" + item.id + "");
            }
        }

        internal static void bind_SCHOOLHISTORY_data(List<Global.sendpushSCHOOLHISTORY> sCHOOLHISTORY)
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Content_about_us_School] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                sCHOOLHISTORY.Clear();
                sCHOOLHISTORY.Add(new school_web.Global.sendpushSCHOOLHISTORY
                {

                    id = "0",
                    Details = "0"

                }); ;
            }
            else
            {
                sCHOOLHISTORY.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    sCHOOLHISTORY.Add(new school_web.Global.sendpushSCHOOLHISTORY
                    {

                        id = dr["Id"].ToString(),
                        //Dear Student, your syllabus is updated on, Date 
                        Details = "Dear Student, school details desk has been updated please check your school details",

                    });
                }
            }
        }

        internal static void bind_studentroutine_data(List<Global.sendpushstudent_routine> student_routine)
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Class_Routine_Master] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                student_routine.Clear();
                student_routine.Add(new school_web.Global.sendpushstudent_routine
                {

                    id = "0",
                    Session_id = "0",
                    Details = "0",
                    Class_id = "0",
                    section = "0"
                });
            }
            else
            {
                student_routine.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    student_routine.Add(new school_web.Global.sendpushstudent_routine
                    {
                        Class_id = dr["Class_id"].ToString(),
                        Session_id = dr["Session_id"].ToString(),
                        section = dr["Section"].ToString(),
                        id = dr["Id"].ToString(),
                        Details = "Dear Student, class routine updated please check your class routine details",

                    });
                }
            }
        }

        internal static void sendpushstudent_routine(Global.sendpushstudent_routine item, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            UsesCode uc = new UsesCode();
            string query = "";
            query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Session_id=" + item.Session_id + " and Class_id=" + item.Class_id + "  and Section=" + item.section + "  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                uc.executequery("update Class_Routine_Master set Send_Status='Send' where Id=" + item.id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    if (gcmid == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Details;
                        ss["title"] = "Chairman Desk ";
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
                uc.executequery("update Class_Routine_Master set Send_Status='Send' where Id=" + item.id + "");
            }
        }




    }
}