using Newtonsoft.Json;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Admin.webServices
{
    /// <summary>
    /// Summary description for routine
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class routine : System.Web.Services.WebService
    {
        public class Fetch_routine_chart_period_head
        {
            public string Period_Name { get; set; }
            public string Period_type { get; set; }
            public string period_times { get; set; }
        }
        List<Fetch_routine_chart_period_head> Show_routine_chart_period_head = new List<Fetch_routine_chart_period_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_class_routine_details(string Session_id, string Class_id, string Section)
        {
            string qry = "select t1.Period_Name,t1.Period,t1.Period_type,t1.Period_no,t2.Start_Time,t2.End_time,t2.Timespan from Class_Routine_period_Master t1 join Class_Routine_period t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.course_id and t1.Period=t2.Period where t1.Class_id='" + Class_id + "' and t1.Session_id='" + Session_id + "' and t1.Class_id in (select Class_id from Class_Routine_Master where Class_id=t1.Class_id and Session_id=t1.Session_id and Class_period=t1.Period and Section='" + Section + "') order by t1.Period_no asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_period_Master"); 
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string period_time = get_time_span(dr["Period"].ToString(), Class_id, Section, Session_id);
                    Show_routine_chart_period_head.Add(new Fetch_routine_chart_period_head
                    {
                        Period_Name = dr["Period_Name"].ToString(),
                        Period_type = dr["Period_type"].ToString(),
                        period_times = period_time,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_routine_chart_period_head));
            }
        }


        private string get_time_span(string period_id, string classid, string session, string Session_id)
        {
            string query = "Select format(Start_Time, 'hh:mm tt') as Start_Time_show,format(End_time, 'hh:mm tt') as End_time_show  from Class_Routine_period where Session_id='"+ Session_id + "' and Period='" + period_id + "' and course_id='" + classid + "'   ";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Start_Time_show"].ToString() + "-" + dt.Rows[0]["End_time_show"].ToString();
            }
        }



        ///========================================
        ///
        public class MyRoutineDay
        {
            public string Day_name { get; set; }
            public string DaYcolors { get; set; }
            public List<MyRoutineSubject> MyRoutineSubjectItem { get; set; }
        }

        public class MyRoutineSubject
        {
            public string Subjects_name { get; set; }
            public string Tblrowcount { get; set; }
            public string Isclass_or_break { get; set; }
            public string Period_type { get; set; }
            public string Period_type_td { get; set; }
            public string Teachers_name { get; set; }
        }


        List<MyRoutineDay> EMySubMark = new List<MyRoutineDay>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_class_routine_details_day(string Session_id, string Class_id, string Section)
        {
            string query = "select * from Day_Master order by Position asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Day_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                int rowconts = 1; int daysPlus = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyRoutineSubject> MBdetails = findmyRoutineDt(dr["Day"].ToString(), Session_id, Class_id, Section, rowconts, daysPlus);
                    string daYcolors = "";
                    if (rowconts == 1)
                    {
                        daYcolors = "dys-monday";
                    }
                    if (rowconts == 2)
                    {
                        daYcolors = "dys-tuesday";
                    }
                    if (rowconts == 3)
                    {
                        daYcolors = "dys-wednesday";
                    }
                    if (rowconts == 4)
                    {
                        daYcolors = "dys-thursday";
                    }
                    if (rowconts == 5)
                    {
                        daYcolors = "dys-friday";
                    }
                    if (rowconts == 6)
                    {
                        daYcolors = "dys-saturday";
                    }



                    EMySubMark.Add(new MyRoutineDay
                    {
                        Day_name = dr["Day"].ToString(),
                        DaYcolors = daYcolors,
                        MyRoutineSubjectItem = MBdetails
                    });
                    rowconts++; daysPlus++;
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }


        private List<MyRoutineSubject> findmyRoutineDt(string Day, string Session_id, string Class_id, string Section, int rowconts, int daysPlus)
        {
            List<MyRoutineSubject> MyRoutineSubjectItem = new List<MyRoutineSubject>();
            string query = "select t1.Period_Name,t1.Period,t1.Period_type,t1.Period_no,t2.Start_Time,t2.End_time,t2.Timespan from Class_Routine_period_Master t1 join Class_Routine_period t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.course_id and t1.Period=t2.Period where t1.Class_id='" + Class_id + "' and t1.Session_id='" + Session_id + "' and t1.Class_id in (select Class_id from Class_Routine_Master where Class_id=t1.Class_id and Session_id=t1.Session_id and Class_period=t1.Period and Section='" + Section + "') order by t1.Period_no asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_period_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)DateTime.Today.DayOfWeek);
                //string result = string.Join("," + Environment.NewLine, Enumerable.Range(0, 7).Select(i => startOfWeek.AddDays(i).ToString("dd-MMMM-yyyy")));
                DateTime finaldate = startOfWeek.AddDays(daysPlus);
                int cidate = My.toIntS(finaldate.ToString("yyyyMMdd"));
                string period_type_in_calender = get_period_type_in_calender(Day, Session_id, Class_id, cidate);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (rowconts == 1)
                    {
                        string tblrowcount = "10"; string isclass_or_break = "shows"; string period_type = "BreakStyle"; string period_type_td = "BreakStyleTd";
                        string subjects = "Break"; string teachers_name = "NA";
                        if (period_type_in_calender == "Class")
                        {
                            if (dt.Rows[i]["Period_type"].ToString() == "Class")
                            {
                                //======CHECK IN EDIT ROUTINE
                                DataTable dtedt = My.dataTable("select *,(select top 1 name from user_details where user_id=t1.Teacher_id  and status='Active') as Teacher_name,(select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as Subject_name from Class_Routine_Master_Teacher_DateItemwise t1 where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and Class_period_id=" + dt.Rows[i]["Period"].ToString() + " and Routine_idate=" + cidate + "");
                                if (dtedt.Rows.Count > 0)
                                {
                                    string subj_name = ""; string teacher_name = "";
                                    tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class";
                                    foreach (DataRow dredt in dtedt.Rows)
                                    {
                                        subj_name = subj_name + dredt["Subject_name"].ToString() + ",";
                                        teacher_name = teacher_name + dredt["Teacher_name"].ToString() + ",";
                                    }
                                    subj_name = subj_name.Remove(subj_name.Length - 1);
                                    teacher_name = teacher_name.Remove(teacher_name.Length - 1);
                                    subjects = subj_name;
                                    teachers_name = teacher_name;
                                }
                                else
                                {
                                    tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class";
                                    subjects = get_routine_subject(Day, Session_id, Class_id, Section, dt.Rows[i]["Period"].ToString());
                                    string[] stringSeparatorss = new string[] { "~" };
                                    string[] arrs = subjects.Split(stringSeparatorss, StringSplitOptions.None);
                                    subjects = arrs[0];
                                    teachers_name = arrs[1];
                                }
                            }
                        }
                        else
                        {
                            if (dt.Rows[i]["Period_type"].ToString() != "Break")
                            {
                                tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class";
                                subjects = period_type_in_calender;
                                teachers_name = "";
                            }
                        }
                        MyRoutineSubjectItem.Add(new MyRoutineSubject
                        {
                            Subjects_name = subjects,
                            Tblrowcount = tblrowcount,
                            Isclass_or_break = isclass_or_break,
                            Period_type = period_type,
                            Period_type_td = period_type_td,
                            Teachers_name = teachers_name,
                        });
                    }
                    else
                    {
                        string tblrowcount = "1"; string isclass_or_break = "hidden"; string period_type = "BreakStyle"; string period_type_td = "BreakStyleTd";
                        string subjects = "Break"; string teachers_name = "NA";
                        if (period_type_in_calender == "Class")
                        {
                            if (dt.Rows[i]["Period_type"].ToString() == "Class")
                            {
                                //======CHECK IN EDIT ROUTINE
                                DataTable dtedt = My.dataTable("select *,(select top 1 name from user_details where user_id=t1.Teacher_id  and status='Active') as Teacher_name,(select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as Subject_name from Class_Routine_Master_Teacher_DateItemwise t1 where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and Class_period_id=" + dt.Rows[i]["Period"].ToString() + " and Routine_idate=" + cidate + "");
                                if (dtedt.Rows.Count > 0)
                                {
                                    string subj_name = ""; string teacher_name = "";
                                    tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class";
                                    foreach (DataRow dredt in dtedt.Rows)
                                    {
                                        subj_name = subj_name + dredt["Subject_name"].ToString() + ",";
                                        teacher_name = teacher_name + dredt["Teacher_name"].ToString() + ",";
                                    }
                                    subj_name = subj_name.Remove(subj_name.Length - 1);
                                    teacher_name = teacher_name.Remove(teacher_name.Length - 1);
                                    subjects = subj_name;
                                    teachers_name = teacher_name;
                                }
                                else
                                {
                                    period_type = "Class";
                                    tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class";
                                    subjects = get_routine_subject(Day, Session_id, Class_id, Section, dt.Rows[i]["Period"].ToString());

                                    string[] stringSeparatorss = new string[] { "~" };
                                    string[] arrs = subjects.Split(stringSeparatorss, StringSplitOptions.None);
                                    subjects = arrs[0];
                                    teachers_name = arrs[1];
                                }
                            }
                        }
                        else
                        {
                            if (dt.Rows[i]["Period_type"].ToString() != "Break")
                            {
                                tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class";
                                subjects = period_type_in_calender;
                                teachers_name = "";
                            }
                        }
                        MyRoutineSubjectItem.Add(new MyRoutineSubject
                        {
                            Subjects_name = subjects,
                            Tblrowcount = tblrowcount,
                            Isclass_or_break = isclass_or_break,
                            Period_type = period_type,
                            Period_type_td = period_type_td,
                            Teachers_name = teachers_name,
                        });
                    }
                }
            }
            return MyRoutineSubjectItem;
        }

        private string get_period_type_in_calender(string day, string session_id, string class_id, int cidate)
        {
            string returN = "Class";
            //string query = "select Type from School_Holiday_Calendar where Idate='" + cidate + "' and Session_id='" + session_id + "' and Day='" + day + "' and Class_id='" + class_id + "'";
            //DataTable dt = My.dataTable(query);
            //if (dt.Rows.Count > 0)
            //{
            //    returN = dt.Rows[0]["Type"].ToString();
            //}
            return returN;
        }

        My mycode = new My();
        private string get_routine_subject(string day, string session_id, string class_id, string section, string period_id)
        {
            string subj_name = ""; string teacher_name = "";
            SqlCommand cmd = new SqlCommand("Select  *,(select top 1 Subject_name from Subject_Master where course_id=Class_routine_period_subject_mapping.Class_id  and Subject_id=Class_routine_period_subject_mapping.Subject_id) as subjectname from Class_routine_period_subject_mapping where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Class_period='" + period_id + "' and Day='" + day + "'");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    subj_name = subj_name + dr["subjectname"].ToString() + ",";
                    string teachers = get_teacher(day, session_id, class_id, section, period_id, dr["Subject_id"].ToString());
                    teacher_name = teacher_name + teachers + ",";
                }
                subj_name = subj_name.Remove(subj_name.Length - 1);
                teacher_name = teacher_name.Remove(teacher_name.Length - 1);
            }
            else
            {
                subj_name = "-";
            }
            return subj_name + "~" + teacher_name;
        }

        private string get_teacher(string day, string session_id, string class_id, string section, string period_id, string subject_id)
        {
            string teacher_name = "";
            SqlCommand cmd = new SqlCommand("select (select top 1 name from user_details where user_id=Class_Routine_Master_Teacher.Teacher_id  and status='Active') as Teacher_name from Class_Routine_Master_Teacher where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Subject_id='" + subject_id + "' and Class_period='" + period_id + "' and Day='" + day + "'");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    teacher_name = dr["Teacher_name"].ToString();
                }
            }
            else
            {
                teacher_name = "NA";
            }
            return teacher_name;
        }



        ///==========================================WITH TEACHER
        ///
        public class Fetch_routine_chart_period_head_teacher
        {
            public string Period_Name { get; set; }
            public string Period_type { get; set; }
            public string period_times { get; set; }
        }
        List<Fetch_routine_chart_period_head_teacher> Show_routine_chart_period_head_teacher = new List<Fetch_routine_chart_period_head_teacher>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_class_routine_details_teacher(string Session_id, string Class_id, string Section)
        {
            string qry = "select t1.Period_Name,t1.Period,t1.Period_type,t1.Period_no,t2.Start_Time,t2.End_time,t2.Timespan from Class_Routine_period_Master t1 join Class_Routine_period t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.course_id and t1.Period=t2.Period where t1.Class_id='" + Class_id + "' and t1.Session_id='" + Session_id + "' and t1.Class_id in (select Class_id from Class_Routine_Master where Class_id=t1.Class_id and Session_id=t1.Session_id and Class_period=t1.Period and Section='" + Section + "') order by t1.Period_no asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_period_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            { 
                foreach (DataRow dr in dt.Rows)
                {
                    string period_time = get_time_span(dr["Period"].ToString(), Class_id, Section, Session_id);
                    Show_routine_chart_period_head_teacher.Add(new Fetch_routine_chart_period_head_teacher
                    {
                        Period_Name = dr["Period_Name"].ToString(),
                        Period_type = dr["Period_type"].ToString(),
                        period_times = period_time,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_routine_chart_period_head_teacher));
            }
        }


        public class MyRoutineDayTeacher
        {
            public string Day_name { get; set; }
            public string Day_date { get; set; }
            public string DaYcolors { get; set; }
            public string Teachers { get; set; }
            public string Day_date_actual_format { get; set; }
            public string Is_editable { get; set; }
            public List<MyRoutineSubjectTeacher> MyRoutineSubjectTeacherItem { get; set; }
        }

        public class MyRoutineSubjectTeacher
        {
            public string Period_Name { get; set; }
            public string Subjects_name { get; set; }
            public string Tblrowcount { get; set; }
            public string Isclass_or_break { get; set; }
            public string Period_type { get; set; }
            public string Period_type_td { get; set; }
            public string Teachers_name { get; set; }
            public string Hide_if_break { get; set; }
            public string Period_id { get; set; }
        }


        List<MyRoutineDayTeacher> EMySubMarkTeacher = new List<MyRoutineDayTeacher>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_class_routine_details_day_teacher(string Session_id, string Class_id, string Section)
        {
            string query = "select * from Day_Master order by Position asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Day_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                int rowconts = 1; int daysPlus = 0; string is_editable = "NotEditable";
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyRoutineSubjectTeacher> MBdetails = findmyRoutineDtTeacher(dr["Day"].ToString(), Session_id, Class_id, Section, rowconts, daysPlus);
                    string daYcolors = "";
                    if (rowconts == 1)
                    {
                        daYcolors = "dys-monday";
                    }
                    if (rowconts == 2)
                    {
                        daYcolors = "dys-tuesday";
                    }
                    if (rowconts == 3)
                    {
                        daYcolors = "dys-wednesday";
                    }
                    if (rowconts == 4)
                    {
                        daYcolors = "dys-thursday";
                    }
                    if (rowconts == 5)
                    {
                        daYcolors = "dys-friday";
                    }
                    if (rowconts == 6)
                    {
                        daYcolors = "dys-saturday";
                    }

                    DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)DateTime.Today.DayOfWeek);
                    DateTime finaldate = startOfWeek.AddDays(daysPlus);
                    string day_date = finaldate.ToString("dd-MM-yy");
                    string day_date_actual_format = finaldate.ToString("dd/MM/yyyy");
                    int cidate = My.toint(finaldate.ToString("yyyyMMdd"));
                    int today_idate = My.toInt(mycode.idate());
                    if (cidate >= today_idate)
                    {
                        is_editable = "YesEditable";
                    }
                    EMySubMarkTeacher.Add(new MyRoutineDayTeacher
                    {
                        Day_name = dr["Day"].ToString(),
                        Day_date = day_date,
                        DaYcolors = daYcolors,
                        Teachers = "Teacher",
                        Day_date_actual_format = day_date_actual_format,
                        Is_editable = is_editable,
                        MyRoutineSubjectTeacherItem = MBdetails
                    });
                    rowconts++; daysPlus++;
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMarkTeacher));
            }
        }


        private List<MyRoutineSubjectTeacher> findmyRoutineDtTeacher(string Day, string Session_id, string Class_id, string Section, int rowconts, int daysPlus)
        {
            List<MyRoutineSubjectTeacher> MyRoutineSubjectTeacherItem = new List<MyRoutineSubjectTeacher>(); 
            string query = "select t1.Period_Name,t1.Period,t1.Period_type,t1.Period_no,t2.Start_Time,t2.End_time,t2.Timespan from Class_Routine_period_Master t1 join Class_Routine_period t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.course_id and t1.Period=t2.Period where t1.Class_id='" + Class_id + "' and t1.Session_id='" + Session_id + "' and t1.Class_id in (select Class_id from Class_Routine_Master where Class_id=t1.Class_id and Session_id=t1.Session_id and Class_period=t1.Period and Section='" + Section + "') order by t1.Period_no asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_period_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            { 
                DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)DateTime.Today.DayOfWeek);
                DateTime finaldate = startOfWeek.AddDays(daysPlus);
                int cidate = My.toIntS(finaldate.ToString("yyyyMMdd"));
                string period_type_in_calender = get_period_type_in_calender(Day, Session_id, Class_id, cidate);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (rowconts == 1)
                    {
                        string tblrowcount = "10"; string isclass_or_break = "shows"; string period_type = "BreakStyle"; string period_type_td = "BreakStyleTd";
                        string subjects = "Break"; string teachers_name = "hidden"; string hide_if_break = "hidden";
                        if (period_type_in_calender == "Class")
                        {
                            if (dt.Rows[i]["Period_type"].ToString() == "Class") //Class
                            {
                                //======CHECK IN EDIT ROUTINE
                                DataTable dtedt = My.dataTable("select *,(select top 1 name from user_details where user_id=t1.Teacher_id and status='Active') as Teacher_name,(select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as Subject_name from Class_Routine_Master_Teacher_DateItemwise t1 where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and Class_period_id=" + dt.Rows[i]["Period"].ToString() + " and Routine_idate=" + cidate + "");
                                if (dtedt.Rows.Count > 0)
                                {
                                    string subj_name = ""; string teacher_name = "";
                                    tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class"; hide_if_break = "showd";
                                    foreach (DataRow dredt in dtedt.Rows)
                                    {
                                        subj_name = subj_name + dredt["Subject_name"].ToString() + ",";
                                        teacher_name = teacher_name + dredt["Teacher_name"].ToString() + ",";
                                    }
                                    subj_name = subj_name.Remove(subj_name.Length - 1);
                                    teacher_name = teacher_name.Remove(teacher_name.Length - 1);
                                    subjects = subj_name;
                                    teachers_name = teacher_name;
                                }
                                else
                                {
                                    tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class"; hide_if_break = "showd";
                                    subjects = get_routine_subject(Day, Session_id, Class_id, Section, dt.Rows[i]["Period"].ToString());
                                    string[] stringSeparatorss = new string[] { "~" };
                                    string[] arrs = subjects.Split(stringSeparatorss, StringSplitOptions.None);
                                    subjects = arrs[0];
                                    teachers_name = arrs[1];
                                }
                            }
                            //ELSE  Break
                        }
                        else
                        {
                            if (dt.Rows[i]["Period_type"].ToString() != "Break")
                            {
                                tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class";
                                subjects = period_type_in_calender;
                                teachers_name = "hidden";
                                hide_if_break = "hidden";
                            }
                        }
                        MyRoutineSubjectTeacherItem.Add(new MyRoutineSubjectTeacher
                        {
                            Subjects_name = subjects,
                            Tblrowcount = tblrowcount,
                            Isclass_or_break = isclass_or_break,
                            Period_type = period_type,
                            Period_type_td = period_type_td,
                            Teachers_name = teachers_name,
                            Hide_if_break = hide_if_break,
                            Period_Name = dt.Rows[i]["Period_Name"].ToString(),
                            Period_id = dt.Rows[i]["Period"].ToString(),
                        });
                    }
                    else
                    {
                        string tblrowcount = "1"; string isclass_or_break = "hidden"; string period_type = "BreakStyle"; string period_type_td = "BreakStyleTd"; string hide_if_break = "hidden";
                        string subjects = "Break"; string teachers_name = "hidden";
                        if (period_type_in_calender == "Class")
                        {
                            if (dt.Rows[i]["Period_type"].ToString() == "Class") //CLASS
                            {
                                //======CHECK IN EDIT ROUTINE
                                DataTable dtedt = My.dataTable("select *,(select top 1 name from user_details where user_id=t1.Teacher_id and status='Active') as Teacher_name,(select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as Subject_name from Class_Routine_Master_Teacher_DateItemwise t1 where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and Class_period_id=" + dt.Rows[i]["Period"].ToString() + " and Routine_idate=" + cidate + "");
                                if (dtedt.Rows.Count > 0)
                                {
                                    string subj_name = ""; string teacher_name = "";
                                    tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class"; hide_if_break = "showd";
                                    foreach (DataRow dredt in dtedt.Rows)
                                    {
                                        subj_name = subj_name + dredt["Subject_name"].ToString() + ",";
                                        teacher_name = teacher_name + dredt["Teacher_name"].ToString() + ",";
                                    }
                                    subj_name = subj_name.Remove(subj_name.Length - 1);
                                    teacher_name = teacher_name.Remove(teacher_name.Length - 1);
                                    subjects = subj_name;
                                    teachers_name = teacher_name;
                                }
                                else
                                {
                                    period_type = "Class";
                                    tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class"; hide_if_break = "showd";
                                    subjects = get_routine_subject(Day, Session_id, Class_id, Section, dt.Rows[i]["Period"].ToString());
                                    string[] stringSeparatorss = new string[] { "~" };
                                    string[] arrs = subjects.Split(stringSeparatorss, StringSplitOptions.None);
                                    subjects = arrs[0];
                                    teachers_name = arrs[1];
                                }
                            }
                            //ELSE   BREAKE
                        }
                        else
                        {
                            if (dt.Rows[i]["Period_type"].ToString() != "Break")
                            {
                                tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class"; hide_if_break = "hidden";
                                subjects = period_type_in_calender;
                                teachers_name = "hidden";
                            }
                        }
                        MyRoutineSubjectTeacherItem.Add(new MyRoutineSubjectTeacher
                        {
                            Subjects_name = subjects,
                            Tblrowcount = tblrowcount,
                            Isclass_or_break = isclass_or_break,
                            Period_type = period_type,
                            Period_type_td = period_type_td,
                            Teachers_name = teachers_name,
                            Hide_if_break = hide_if_break,
                            Period_Name = dt.Rows[i]["Period_Name"].ToString(),
                            Period_id = dt.Rows[i]["Period"].ToString(),
                        });
                    }
                }
            }
            return MyRoutineSubjectTeacherItem;
        }


        //FETCH SUBJECTS
        public class Fetch_Details_of_subjects
        {
            public string Subject_name { get; set; }
            public string Subject_id { get; set; }
        }


        List<Fetch_Details_of_subjects> Show_of_subjects = new List<Fetch_Details_of_subjects>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_subjects(string Session_id, string Class_id, string Section)
        {
            string query = "Select Subject_name,Subject_id from Subject_Master where course_id='" + Class_id + "' order by Subject_position";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Distributor_Dealer");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_subjects.Add(new Fetch_Details_of_subjects
                    {
                        Subject_name = dr["Subject_name"].ToString(),
                        Subject_id = dr["Subject_id"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_subjects));
            }

        }

        //FETCH Teachers
        public class Fetch_Details_of_teacher
        {
            public string Teacher_name { get; set; }
            public string UserID { get; set; }
        }
        List<Fetch_Details_of_teacher> Show_of_teacher = new List<Fetch_Details_of_teacher>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_teacher_by_subject(string Session_id, string Class_id, string Section, string Subject_list)
        {
            string query = "select *,(select top 1 name from user_details where user_id=TeacherCourseSubjectMaping.UserID and status='Active') as Teacher_name from TeacherCourseSubjectMaping where Session_id='" + Session_id + "' and Istatus=1 and CategoryID='" + Class_id + "' and section='" + Section + "' and AssignCourseID='" + Subject_list + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "TeacherCourseSubjectMaping");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_teacher.Add(new Fetch_Details_of_teacher
                    {
                        Teacher_name = dr["Teacher_name"].ToString(),
                        UserID = dr["UserID"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_teacher));
            }
        }

        //SAVE UPDATED ROUTINE
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void save_updated_routine(string Session_id, string Class_id, string Section, string Subject_id, string Teacher_id, string Date_from, string Date_to, string Period_id, string User_id)
        {
            //==============From
            DateTime fromDateTime = Convert.ToDateTime(Date_from);
            string fromDate = fromDateTime.ToString("dd/MM/yyyy");
            int fromiDate = My.toint(fromDateTime.ToString("yyyyMMdd"));

            //==============To
            DateTime ToDateTime = Convert.ToDateTime(Date_to);
            string ToDate = ToDateTime.ToString("dd/MM/yyyy");
            int ToiDate = My.toint(ToDateTime.ToString("yyyyMMdd"));

            int y = 0; int x = 0;
            for (int i = fromiDate; i <= ToiDate; i++)
            {
                DateTime cfromDateTime = Convert.ToDateTime(Date_from);
                DateTime cfinaldate = cfromDateTime.AddDays(y);
                string cDate = cfinaldate.ToString("dd/MM/yyyy");
                int ciDate = My.toint(cfinaldate.ToString("yyyyMMdd"));
                string cDay = cfinaldate.ToString("dddd");

                if (mycode.IsUserExist("select Id from Class_Routine_Master_Teacher_DateItemwise where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and Subject_id=" + Subject_id + " and Class_period_id=" + Period_id + " and Routine_idate=" + ciDate + ""))
                {
                    SqlCommand cmd;
                    string queryt = "INSERT INTO Class_Routine_Master_Teacher_DateItemwise (Session_id,Class_id,Section,Subject_id,Class_period_id,Teacher_id,Routine_date,Routine_idate,Routine_day,Created_by,Created_date,Created_idate) values (@Session_id,@Class_id,@Section,@Subject_id,@Class_period_id,@Teacher_id,@Routine_date,@Routine_idate,@Routine_day,@Created_by,@Created_date,@Created_idate)";
                    cmd = new SqlCommand(queryt);
                    cmd.Parameters.AddWithValue("@Session_id", Session_id);
                    cmd.Parameters.AddWithValue("@Class_id", Class_id);
                    cmd.Parameters.AddWithValue("@Section", Section);
                    cmd.Parameters.AddWithValue("@Subject_id", Subject_id);
                    cmd.Parameters.AddWithValue("@Class_period_id", Period_id);
                    cmd.Parameters.AddWithValue("@Teacher_id", Teacher_id);
                    cmd.Parameters.AddWithValue("@Routine_date", cDate);
                    cmd.Parameters.AddWithValue("@Routine_idate", ciDate);
                    cmd.Parameters.AddWithValue("@Routine_day", cDay);
                    cmd.Parameters.AddWithValue("@Created_by", User_id);
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    if (My.InsertUpdateData(cmd))
                    {
                        x++;
                    }
                }
                y++;
            }
            if (x > 0)
            {
                SqlCommand cmd;
                string queryt = "INSERT INTO Class_Routine_Master_Teacher_Datewise (Session_id,Class_id,Section,Subject_id,Class_period_id,Teacher_id,From_date,From_idate,To_date,To_idate,Created_by,Created_date,Created_idate) values (@Session_id,@Class_id,@Section,@Subject_id,@Class_period_id,@Teacher_id,@From_date,@From_idate,@To_date,@To_idate,@Created_by,@Created_date,@Created_idate)";
                cmd = new SqlCommand(queryt);
                cmd.Parameters.AddWithValue("@Session_id", Session_id);
                cmd.Parameters.AddWithValue("@Class_id", Class_id);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@Subject_id", Subject_id);
                cmd.Parameters.AddWithValue("@Class_period_id", Period_id);
                cmd.Parameters.AddWithValue("@Teacher_id", Teacher_id);
                cmd.Parameters.AddWithValue("@From_date", fromDate);
                cmd.Parameters.AddWithValue("@From_idate", fromiDate);
                cmd.Parameters.AddWithValue("@To_date", ToDate);
                cmd.Parameters.AddWithValue("@To_idate", ToiDate);
                cmd.Parameters.AddWithValue("@Created_by", User_id);
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }



        //FETCH Teachers
        public class Fetch_Details_of_teacher_subject_table
        {
            public string Id { get; set; }
            public string Teacher_name { get; set; }
            public string Subject_name { get; set; }
            public string Class_name { get; set; }
            public string Period_name { get; set; }
            public string From_date { get; set; }
            public string To_date { get; set; }
            public string Created_date { get; set; }
        }
        List<Fetch_Details_of_teacher_subject_table> Show_of_teacher_subject_table = new List<Fetch_Details_of_teacher_subject_table>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_added_subjects_table(string Session_id, string Class_id, string Section, string Period_id)
        {
            string query = "select *,(select top 1 Course_Name from Add_course_table where course_id=Class_Routine_Master_Teacher_Datewise.Class_id) as Class_name,(select top 1 Period_Name from Class_Routine_period_Master where Period=Class_Routine_Master_Teacher_Datewise.Class_period_id and Session_id=Class_Routine_Master_Teacher_Datewise.Session_id and Class_id=Class_Routine_Master_Teacher_Datewise.Class_id) as Period_name,(select top 1 name from user_details where user_id=Class_Routine_Master_Teacher_Datewise.Teacher_id and status='Active') as Teacher_name,(select top 1 Subject_name from Subject_Master where course_id=Class_Routine_Master_Teacher_Datewise.Class_id and Subject_id=Class_Routine_Master_Teacher_Datewise.Subject_id) as Subject_name from Class_Routine_Master_Teacher_Datewise where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and Class_period_id=" + Period_id + " and To_idate>=" + mycode.idate() + "";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_Master_Teacher_Datewise");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_teacher_subject_table.Add(new Fetch_Details_of_teacher_subject_table
                    {
                        Id = dr["Id"].ToString(),
                        Teacher_name = dr["Teacher_name"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        Class_name = dr["Class_name"].ToString(),
                        Period_name = dr["Period_name"].ToString(),
                        From_date = dr["From_date"].ToString(),
                        To_date = dr["To_date"].ToString(),
                        Created_date = dr["Created_date"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_teacher_subject_table));
            }
        }


        //====DELETE SUBJETC
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void delete_subjects(string Id)
        {
            DataTable dt = My.dataTable("select * from Class_Routine_Master_Teacher_Datewise where Id=" + Id + "");
            if (dt.Rows.Count > 0)
            {
                string From_date = dt.Rows[0]["From_date"].ToString();
                string To_date = dt.Rows[0]["To_date"].ToString();
                //==============From
                DateTime fromDateTime = Convert.ToDateTime(From_date);
                string fromDate = fromDateTime.ToString("dd/MM/yyyy");
                int fromiDate = My.toint(fromDateTime.ToString("yyyyMMdd"));

                //==============To
                DateTime ToDateTime = Convert.ToDateTime(To_date);
                string ToDate = ToDateTime.ToString("dd/MM/yyyy");
                int ToiDate = My.toint(ToDateTime.ToString("yyyyMMdd"));

                int y = 0;
                for (int i = fromiDate; i <= ToiDate; i++)
                {
                    DateTime cfromDateTime = Convert.ToDateTime(From_date);
                    DateTime cfinaldate = cfromDateTime.AddDays(y);
                    string cDate = cfinaldate.ToString("dd/MM/yyyy");
                    int ciDate = My.toint(cfinaldate.ToString("yyyyMMdd"));
                    My.exeSql("delete from Class_Routine_Master_Teacher_DateItemwise where Session_id=" + dt.Rows[0]["Session_id"].ToString() + " and Class_id=" + dt.Rows[0]["Class_id"].ToString() + " and Subject_id=" + dt.Rows[0]["Subject_id"].ToString() + "  and Class_period_id=" + dt.Rows[0]["Class_period_id"].ToString() + " and Routine_idate=" + ciDate + "");
                    y++;
                }
                if (y > 0)
                {
                    My.exeSql("delete from Class_Routine_Master_Teacher_Datewise where Id=" + Id + "");
                }
            }
        }



        //FETCH DDL CLASS
        public class Fetch_Details_of_ddl_class
        {
            public string Class_name { get; set; }
            public string Class_id { get; set; }
        }


        List<Fetch_Details_of_ddl_class> Show_of_ddl_class = new List<Fetch_Details_of_ddl_class>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_ddl_class(string Session_id)
        {
            string query = "Select Course_Name,course_id from Add_course_table order by Position";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Distributor_Dealer");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_ddl_class.Add(new Fetch_Details_of_ddl_class
                    {
                        Class_name = dr["Course_Name"].ToString(),
                        Class_id = dr["course_id"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_ddl_class));
            }
        }

        //FETCH DDL SECTION
        public class Fetch_Details_of_ddl_section
        {
            public string Section { get; set; }
        }


        List<Fetch_Details_of_ddl_section> Show_of_ddl_section = new List<Fetch_Details_of_ddl_section>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_section_by_class(string Session_id, string Class_id)
        {
            string query = "Select distinct Section from Class_Routine_Master where Session_id=" + Session_id + " and Class_id='" + Class_id + "' order by Section";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Distributor_Dealer");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_ddl_section.Add(new Fetch_Details_of_ddl_section
                    {
                        Section = dr["Section"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_ddl_section));
            }

        }


        //Fetch Teacher class taken
        public class MyRoutineTeacherTakenClass
        {
            public string Session_id { get; set; }
            public string Class_id { get; set; }
            public string Section { get; set; }
            public string Subject_type { get; set; }
            public string Subject_name { get; set; }
            public string Subject_id { get; set; }
            public string Total_class_of_subject { get; set; }
            public string RowSpan { get; set; }
            public List<MyRoutineTeacherTakenClassStepII> MyRoutineTeacherTakenClassStepIIItem { get; set; }
        }

        public class MyRoutineTeacherTakenClassStepII
        {
            public string Teacher_id { get; set; }
            public string Teacher_name { get; set; }
            public string No_of_class_taken { get; set; }
        }


        List<MyRoutineTeacherTakenClass> EMySubMarkTeachers = new List<MyRoutineTeacherTakenClass>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_class_taken_by_teacher(string From_date, string To_date, string Session_id, string Class_id, string Section)
        {
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Section,t2.Subject_name,t2.Subject_type,t1.Subject_id,(select count(Id) from Class_routine_teacher_attendance where Session_id=t1.Session_id and Class_id=t1.Class_id and Section=t1.Section and Subject_id=t1.Subject_id  and Attendance_idate>=" + My.DateConvertToIdate(From_date) + " and Attendance_idate<=" + My.DateConvertToIdate(To_date) + ") as Total_class_of_subject,(select count(Teacher_id) from (select DISTINCT Teacher_id from Class_routine_teacher_attendance where Session_id=t1.Session_id and Class_id=t1.Class_id and Section=t1.Section and Subject_id=t1.Subject_id) t) as No_of_tacher from Class_routine_teacher_attendance t1 join Subject_Master t2 on t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id where t1.Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_routine_teacher_attendance");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyRoutineTeacherTakenClassStepII> MBdetails = findmyRoutineDtTeacherClassTaken(dr["Subject_id"].ToString(), Session_id, Class_id, Section, From_date, To_date);
                    EMySubMarkTeachers.Add(new MyRoutineTeacherTakenClass
                    {
                        Session_id = dr["Session_id"].ToString(),
                        Class_id = dr["Class_id"].ToString(),
                        Section = dr["Section"].ToString(),
                        Subject_type = dr["Subject_type"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        Subject_id = dr["Subject_id"].ToString(),
                        Total_class_of_subject = dr["Total_class_of_subject"].ToString(),
                        RowSpan = dr["No_of_tacher"].ToString(),
                        MyRoutineTeacherTakenClassStepIIItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMarkTeachers));
            }
        }


        private List<MyRoutineTeacherTakenClassStepII> findmyRoutineDtTeacherClassTaken(string Subject_id, string Session_id, string Class_id, string Section, string From_date, string To_date)
        {
            List<MyRoutineTeacherTakenClassStepII> MyRoutineTeacherTakenClassStepIIItem = new List<MyRoutineTeacherTakenClassStepII>();
            string query = "select DISTINCT Teacher_id,(select top 1 name from user_details where user_id=t1.Teacher_id and status='Active') as Teacher_name,(select count(Id) from Class_routine_teacher_attendance where Session_id=t1.Session_id and Class_id=t1.Class_id and Section=t1.Section and Subject_id=t1.Subject_id and Teacher_id=t1.Teacher_id and Attendance_idate>=" + My.DateConvertToIdate(From_date) + " and Attendance_idate<=" + My.DateConvertToIdate(To_date) + ") as No_of_class_taken from Class_routine_teacher_attendance t1 where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Section='" + Section + "' and t1.Subject_id=" + Subject_id + " and Attendance_idate>=" + My.DateConvertToIdate(From_date) + " and Attendance_idate<=" + My.DateConvertToIdate(To_date) + "";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_period_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MyRoutineTeacherTakenClassStepIIItem.Add(new MyRoutineTeacherTakenClassStepII
                    {
                        Teacher_id = dr["Teacher_id"].ToString(),
                        Teacher_name = dr["Teacher_name"].ToString(),
                        No_of_class_taken = dr["No_of_class_taken"].ToString(),
                    });
                }
            }
            return MyRoutineTeacherTakenClassStepIIItem;
        }


        public class Fetch_Details_of_class_taken_teacher
        {
            public string Class_name { get; set; }
            public string Subject_name { get; set; }
            public string Section { get; set; }
            public string Period_Name { get; set; }
            public string Teacher_name { get; set; }
            public string Attendance_date { get; set; }
        }


        List<Fetch_Details_of_class_taken_teacher> Show_of_class_taken_teacher = new List<Fetch_Details_of_class_taken_teacher>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_teacher_detail_class_taken(string From_date, string To_date, string Session_id, string Class_id, string Section, string Subject_id, string Teacher_id)
        {
            string query = "select (select top 1 Course_Name from Add_course_table where course_id=t1.Class_id) as Class_name,(select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as Subject_name,Section,(select top 1 Period_Name from Class_Routine_period_Master where Session_id=t1.Session_id and Class_id=t1.Class_id and Period=t1.Period_id) as Period_Name,(select top 1 name from user_details where user_id=t1.Teacher_id and status='Active') as Teacher_name,Attendance_date from Class_routine_teacher_attendance  t1 where t1.Session_id=" + Session_id + " and  t1.Class_id=" + Class_id + " and Section='" + Section + "' and Subject_id=" + Subject_id + " and Teacher_id='" + Teacher_id + "' and t1.Attendance_idate>=" + My.DateConvertToIdate(From_date) + " and t1.Attendance_idate<=" + My.DateConvertToIdate(To_date) + "";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_routine_teacher_attendance");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_class_taken_teacher.Add(new Fetch_Details_of_class_taken_teacher
                    {
                        Class_name = dr["Class_name"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        Section = dr["Section"].ToString(),
                        Period_Name = dr["Period_Name"].ToString(),
                        Teacher_name = dr["Teacher_name"].ToString(),
                        Attendance_date = dr["Attendance_date"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_class_taken_teacher));
            }

        }


        //================================GRAPH
        [WebMethod(EnableSession = true)]
        public string find_class_taken_by_teacher_report(string Session_id, string Class_id)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Subject");
            dtDatas.Columns.Add("Total Class");
            dtDatas.Columns.Add("Taken Class");


            List<object> datasets = new List<object>(); object labels = "";
            object chartData = "";

            string query = "select DISTINCT t1.Session_id,t2.Subject_Short_Name as Subject_name,t2.Subject_type,t1.Subject_id from Class_routine_teacher_attendance t1 join Subject_Master t2 on t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id where t1.Session_id=1 ";
            DataTable dtm = My.dataTable(query);
            if (dtm.Rows.Count > 0)
            {
                foreach (DataRow dr in dtm.Rows)
                {
                    SqlCommand cmd = new SqlCommand("sp_Graph_report_of_routine");
                    if (Class_id == "0")  // ALL CLASS
                    {
                        cmd.Parameters.AddWithValue("@sp_status", "1");
                    }
                    else // WITH CLASS
                    {
                        cmd.Parameters.AddWithValue("@sp_status", "33");
                    }
                    cmd.Parameters.AddWithValue("@Session_id", Session_id);
                    cmd.Parameters.AddWithValue("@Class_id", Class_id);
                    cmd.Parameters.AddWithValue("@Subject_id", dr["Subject_id"].ToString());
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];

                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["Subject"] = dr["Subject_name"].ToString();
                    drNewRow["Total Class"] = dt.Rows[0]["No_of_class"].ToString();
                    drNewRow["Taken Class"] = dt.Rows[0]["No_of_class_taken"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }



            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Subject")).ToList();
            var colors = new String[] { "#172fa9", "#00c13b", "#ebe400" };
            for (int i = 1; i < dtDatas.Columns.Count; i++)
            {
                datasets.Add(new
                {
                    label = dtDatas.Columns[i].ColumnName,
                    backgroundColor = colors[i - 1],
                    data = dtDatas.AsEnumerable().Select(row => row.Field<object>(dtDatas.Columns[i].ColumnName)).ToList()
                });
            }
            chartData = new
            {
                labels = labels,
                datasets = datasets,
            };

            return JsonConvert.SerializeObject(chartData);
        }



        //FETCH DDL TEACHER
        public class Fetch_Details_of_ddl_teacher
        {
            public string TeacherName { get; set; }
            public string Teacher_id { get; set; }
        }


        List<Fetch_Details_of_ddl_teacher> Show_of_ddl_teacher = new List<Fetch_Details_of_ddl_teacher>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_teachers_ddl(string Session_id)
        {
            string query = "select DISTINCT t2.name,t1.Teacher_id from Class_routine_teacher_attendance t1 join user_details t2 on t1.Teacher_id=t2.user_id where Session_id=" + Session_id + " and t2.status='Active' order by name asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_routine_teacher_attendance");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_ddl_teacher.Add(new Fetch_Details_of_ddl_teacher
                    {
                        TeacherName = dr["name"].ToString(),
                        Teacher_id = dr["Teacher_id"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_ddl_teacher));
            }
        }


        ///=================================
        ///
        //Fetch Teacher class taken By TEACHER
        public class MyRoutineTeacherTakenClassBT
        {
            public string Session_id { get; set; }
            public string Class_name { get; set; }
            public string Class_id { get; set; }
            public string Section { get; set; }
            public string Subject_type { get; set; }
            public string Subject_name { get; set; }
            public string Subject_id { get; set; }
            public string Total_class_of_subject { get; set; }
            public string No_of_class_taken { get; set; }
            public string Teacher_id { get; set; }
            public List<MyRoutineTeacherTakenClassBTStepII> MyRoutineTeacherTakenClassBTStepIIItem { get; set; }
        }

        public class MyRoutineTeacherTakenClassBTStepII
        {
            public string Teacher_id { get; set; }
            public string Teacher_name { get; set; }
            public string No_of_class_taken { get; set; }
        }


        List<MyRoutineTeacherTakenClassBT> EMySubMarkTeachersBT = new List<MyRoutineTeacherTakenClassBT>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_class_taken_by_teacherBT(string From_date, string To_date, string Session_id, string Teacher_id)
        {
            string query = "select DISTINCT t1.Teacher_id,t1.Session_id,t1.Class_id,(select top 1 Course_Name from Add_course_table where course_id=t1.Class_id) as Class_name,t1.Section,t2.Subject_name,t2.Subject_type,t1.Subject_id,(select count(Id) from Class_routine_teacher_attendance where Session_id=t1.Session_id and Class_id=t1.Class_id and Section=t1.Section and Subject_id=t1.Subject_id and Teacher_id=t1.Teacher_id and Attendance_idate>=" + My.DateConvertToIdate(From_date) + " and Attendance_idate<=" + My.DateConvertToIdate(To_date) + ") as Total_class_of_subject,(select top 1 name from user_details where user_id=t1.Teacher_id and status='Active') as Teacher_name,(select count(Id) from Class_routine_teacher_attendance where Session_id=t1.Session_id and Class_id=t1.Class_id and Section=t1.Section and Subject_id=t1.Subject_id and Teacher_id=t1.Teacher_id and Attendance_idate>=" + My.DateConvertToIdate(From_date)+ " and Attendance_idate<=" + My.DateConvertToIdate(To_date) + ") as No_of_class_taken from Class_routine_teacher_attendance t1 join Subject_Master t2 on t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Teacher_id='" + Teacher_id + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_routine_teacher_attendance");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //List<MyRoutineTeacherTakenClassBTStepII> MBdetails = findmyRoutineDtTeacherClassTakenBT(dr["Subject_id"].ToString(), Session_id, dr["Class_id"].ToString(), dr["Section"].ToString(), From_date, To_date, Teacher_id);
                    EMySubMarkTeachersBT.Add(new MyRoutineTeacherTakenClassBT
                    {
                        Session_id = dr["Session_id"].ToString(),
                        Class_name = dr["Class_name"].ToString(),
                        Class_id = dr["Class_id"].ToString(),
                        Section = dr["Section"].ToString(),
                        Subject_type = dr["Subject_type"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        Subject_id = dr["Subject_id"].ToString(),
                        Total_class_of_subject = dr["Total_class_of_subject"].ToString(),
                        No_of_class_taken = dr["No_of_class_taken"].ToString(),
                        Teacher_id = dr["Teacher_id"].ToString(),
                        //MyRoutineTeacherTakenClassBTStepIIItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMarkTeachersBT));
            }
        }


        private List<MyRoutineTeacherTakenClassBTStepII> findmyRoutineDtTeacherClassTakenBT(string Subject_id, string Session_id, string Class_id, string Section, string From_date, string To_date, string Teacher_id)
        {
            List<MyRoutineTeacherTakenClassBTStepII> MyRoutineTeacherTakenClassBTStepIIItem = new List<MyRoutineTeacherTakenClassBTStepII>();
            string query = "select DISTINCT Teacher_id,(select top 1 name from user_details where user_id=t1.Teacher_id and status='Active') as Teacher_name,(select count(Id) from Class_routine_teacher_attendance where Session_id=t1.Session_id and Class_id=t1.Class_id and Section=t1.Section and Subject_id=t1.Subject_id and Teacher_id=t1.Teacher_id and Attendance_idate>=" + My.DateConvertToIdate(From_date) + " and Attendance_idate<=" + My.DateConvertToIdate(To_date) + ") as No_of_class_taken from Class_routine_teacher_attendance t1 where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Section='" + Section + "' and t1.Subject_id=" + Subject_id + " and Attendance_idate>=" + My.DateConvertToIdate(From_date) + " and Attendance_idate<=" + My.DateConvertToIdate(To_date) + " and t1.Teacher_id='" + Teacher_id + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_period_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MyRoutineTeacherTakenClassBTStepIIItem.Add(new MyRoutineTeacherTakenClassBTStepII
                    {
                        Teacher_id = dr["Teacher_id"].ToString(),
                        Teacher_name = dr["Teacher_name"].ToString(),
                        No_of_class_taken = dr["No_of_class_taken"].ToString(),
                    });
                }
            }
            return MyRoutineTeacherTakenClassBTStepIIItem;
        }
    }
}
