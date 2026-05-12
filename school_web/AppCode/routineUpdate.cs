using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{

    public class routineUpdate
    {
        My myc = new My();
        internal static void update_teacher_routine(string session_id, string branch_id, string Userid)
        {
            My.exeSql("delete from Class_Routine_Master_Teacher where Session_id='" + session_id + "'");
            DataTable dt = My.dataTable("select * from Class_Routine_Master where Period_type='Class' and Session_id='" + session_id + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataTable dtsubj = My.dataTable("Select * from Class_routine_period_subject_mapping where Session_id='" + dr["Session_id"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and Section='" + dr["Section"].ToString() + "' and Class_period='" + dr["Class_period"].ToString() + "' and Day='" + dr["Day"].ToString() + "'");
                    if (dtsubj.Rows.Count > 0)
                    {
                        foreach (DataRow drSubj in dtsubj.Rows)
                        {
                            DataTable dtTeachr = My.dataTable("select * from TeacherCourseSubjectMaping where Session_id='" + dr["Session_id"].ToString() + "' and Istatus=1 and CategoryID='" + dr["Class_id"].ToString() + "' and section='" + dr["Section"].ToString() + "' and AssignCourseID='" + drSubj["Subject_id"].ToString() + "' and UserID in (select user_id from user_details where status='Active')");
                            if (dtTeachr.Rows.Count > 0)
                            {
                                int saveStatus = 0;
                                foreach (DataRow drtchr in dtTeachr.Rows)
                                {
                                    if (saveStatus == 0)
                                    {
                                        if (IsUserExist("select Id from Class_Routine_Master_Teacher where Session_id='" + dr["Session_id"].ToString() + "' and Class_period='" + dr["Class_period"].ToString() + "' and Day='" + dr["Day"].ToString() + "' and Teacher_id='" + drtchr["UserID"].ToString() + "'"))
                                        {
                                            SqlCommand cmd;
                                            string queryt = "INSERT INTO Class_Routine_Master_Teacher (Teacher_id,Session_id,Class_id,Section,Subject_id,Class_period,Day,Created_By,Date,Idate,time) values (@Teacher_id,@Session_id,@Class_id,@Section,@Subject_id,@Class_period,@Day,@Created_By,@Date,@Idate,@time )";
                                            cmd = new SqlCommand(queryt);
                                            cmd.Parameters.AddWithValue("@Teacher_id", drtchr["UserID"].ToString());
                                            cmd.Parameters.AddWithValue("@Session_id", dr["Session_id"].ToString());
                                            cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                                            cmd.Parameters.AddWithValue("@Section", dr["section"].ToString());
                                            cmd.Parameters.AddWithValue("@Subject_id", drSubj["Subject_id"].ToString());
                                            cmd.Parameters.AddWithValue("@Class_period", dr["Class_period"].ToString());
                                            cmd.Parameters.AddWithValue("@Day", dr["Day"].ToString());
                                            cmd.Parameters.AddWithValue("@Created_By", Userid);
                                            cmd.Parameters.AddWithValue("@Date", getdate());
                                            cmd.Parameters.AddWithValue("@Idate", getidate());
                                            cmd.Parameters.AddWithValue("@time", gettime());
                                            if (InsertUpdate.InsertUpdateData(cmd))
                                            {
                                                saveStatus++;
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




        ///=======================MAKE ATTENDANCE

        internal static void update_teacher_attendance(string session_id, string branch_id, string Userid)
        {
            DataTable dtatt = My.dataTable("select top 1 * from Class_routine_teacher_attendance order by id desc");
            if (dtatt.Rows.Count > 0)
            {
                DateTime fromDateTime = Convert.ToDateTime(dtatt.Rows[0]["Attendance_date"].ToString());
                DateTime csfinaldate = fromDateTime.AddDays(1);
                string fromDate = csfinaldate.ToString("dd/MM/yyyy");
                int fromiDate = My.toint(csfinaldate.ToString("yyyyMMdd"));
                int toiDate = My.toint(getidate());
                int y = 0;
                for (int i = fromiDate; i < toiDate; i++)
                {
                    DateTime cfinaldate = csfinaldate.AddDays(y);
                    string cDate = cfinaldate.ToString("dd/MM/yyyy");
                    int ciDate = My.toint(cfinaldate.ToString("yyyyMMdd"));
                    string cDay = cfinaldate.ToString("dddd");
                    save_teacher_attendance(cDate, ciDate, cDay, session_id);
                    y++;
                }
            }
            else
            {
                string year = getYear();
                DateTime fromDateTime = Convert.ToDateTime("01/05/" + year);
                DateTime csfinaldate = fromDateTime.AddDays(1);
                string fromDate = csfinaldate.ToString("dd/MM/yyyy");
                int fromiDate = My.toint(csfinaldate.ToString("yyyyMMdd"));
                int toiDate = My.toint(getidate());
                int y = 0;
                for (int i = fromiDate; i < toiDate; i++)
                {
                    DateTime cfinaldate = csfinaldate.AddDays(y);
                    string cDate = cfinaldate.ToString("dd/MM/yyyy");
                    int ciDate = My.toint(cfinaldate.ToString("yyyyMMdd"));
                    string cDay = cfinaldate.ToString("dddd");
                    save_teacher_attendance(cDate, ciDate, cDay, session_id);
                    y++;
                }
            }
        }



        private static void save_teacher_attendance(string Date, int idate, string Day, string session_id)
        {
            DataTable dtatt = My.dataTable("select t1.*,t2.Period_Name,t2.Period_type as Period_types from Class_Routine_Master t1 join Class_Routine_period_Master t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Class_period=t2.Period where t2.Period_type='Class' and t1.Session_id=" + session_id + " and t1.Day='" + Day + "'");
            if (dtatt.Rows.Count > 0)
            {
                foreach (DataRow dr in dtatt.Rows)
                {
                    string period_type_in_calender = get_period_type_in_calender(Day, session_id, dr["Class_id"].ToString(), idate);
                    if (period_type_in_calender == "Class")
                    {
                        DataTable dtedt = My.dataTable("select *,(select top 1 name from user_details where user_id=t1.Teacher_id) as Teacher_name,(select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as Subject_name from Class_Routine_Master_Teacher_DateItemwise t1 where Session_id=" + session_id + " and Class_id=" + dr["Class_id"].ToString() + " and Section='" + dr["Section"].ToString() + "' and Class_period_id=" + dr["Class_period"].ToString() + " and Routine_idate=" + idate + "");
                        if (dtedt.Rows.Count > 0)
                        {
                            foreach (DataRow dredt in dtedt.Rows)
                            {
                                save_teacher_attendance_report(Date, idate, Day, session_id, dr["Class_id"].ToString(), dr["Section"].ToString(), dredt["Subject_id"].ToString(), dr["Class_period"].ToString(), dredt["Teacher_id"].ToString());
                            }
                        }
                        else
                        {
                            DataTable dtsubj = My.dataTable("select * from Class_routine_period_subject_mapping where Session_id=" + session_id + " and Class_id=" + dr["Class_id"].ToString() + " and Section='" + dr["Section"].ToString() + "' and Class_period='" + dr["Class_period"].ToString() + "' and Day='" + dr["Day"].ToString() + "'");
                            if (dtsubj.Rows.Count > 0)
                            {
                                foreach (DataRow drsub in dtsubj.Rows)
                                {
                                    DataTable dttchr = My.dataTable("select * from TeacherCourseSubjectMaping where Session_id=" + dr["Session_id"].ToString() + " and Istatus=1 and CategoryID='" + dr["Class_id"].ToString() + "' and section='" + dr["Section"].ToString() + "' and AssignCourseID='" + drsub["Subject_id"].ToString() + "'");
                                    if (dttchr.Rows.Count > 0)
                                    {
                                        foreach (DataRow drtchr in dttchr.Rows)
                                        {
                                            save_teacher_attendance_report(Date, idate, Day, session_id, dr["Class_id"].ToString(), dr["Section"].ToString(), drsub["Subject_id"].ToString(), drsub["Class_period"].ToString(), drtchr["UserID"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static string get_period_type_in_calender(string day, string session_id, string class_id, int idate)
        {
            string returN = "Class";
            string query = "select Type from School_Holiday_Calendar where Idate='" + idate + "' and Session_id='" + session_id + "' and Day='" + day + "' and Class_id='" + class_id + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                returN = dt.Rows[0]["Type"].ToString();
            }
            return returN;
        }

        private static void save_teacher_attendance_report(string date, int idate, string day, string session_id, string class_id, string section, string subject_id, string period_id, string teacher_id)
        {
            if (IsUserExist("select Id from Class_routine_teacher_attendance where Session_id='" + session_id + "' and Class_id=" + class_id + " and Section='" + section + "' and Period_id='" + period_id + "' and Attendance_idate='" + idate + "' and Teacher_id='" + teacher_id + "' and Subject_id=" + subject_id + ""))
            {
                SqlCommand cmd;
                string queryt = "INSERT INTO Class_routine_teacher_attendance (Session_id,Class_id,Section,Subject_id,Period_id,Teacher_id,Attendance_date,Attendance_idate,Attendance_day,Created_date,Created_idate,Created_time) values (@Session_id,@Class_id,@Section,@Subject_id,@Period_id,@Teacher_id,@Attendance_date,@Attendance_idate,@Attendance_day,@Created_date,@Created_idate,@Created_time)";
                cmd = new SqlCommand(queryt);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Section", section);
                cmd.Parameters.AddWithValue("@Subject_id", subject_id);
                cmd.Parameters.AddWithValue("@Period_id", period_id);
                cmd.Parameters.AddWithValue("@Teacher_id", teacher_id);
                cmd.Parameters.AddWithValue("@Attendance_date", date);
                cmd.Parameters.AddWithValue("@Attendance_idate", idate);
                cmd.Parameters.AddWithValue("@Attendance_day", day);
                cmd.Parameters.AddWithValue("@Created_date", getdate());
                cmd.Parameters.AddWithValue("@Created_idate", getidate());
                cmd.Parameters.AddWithValue("@Created_time", gettime());
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                }
            }
        }

        private static object gettime()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HHmmss");
        }

        private static object getidate()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
        }

        private static object getdate()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
        }
        private static string getYear()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");
        }
        private static bool IsUserExist(string query)
        {
            bool status = false;
            DataTable dtTemp = My.dataTable(query);
            if (dtTemp.Rows.Count == 0)
            {
                status = true;
            }
            return status;
        }
    }
}