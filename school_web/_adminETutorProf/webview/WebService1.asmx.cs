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

namespace school_web._adminETutorProf.webview
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        public class Fetch_routine_chart_period_head
        {
            public string Class_name { get; set; }
            public string Section { get; set; }
            public string Period_Name { get; set; }
            public string Period_type { get; set; }
            public string period_times { get; set; }
            public string Subjects_name { get; set; }
            public string Isclass_or_break { get; set; }
            public string Period_type_td { get; set; }
            public string Teachers_name { get; set; }
        }
        List<Fetch_routine_chart_period_head> Show_routine_chart_period_head = new List<Fetch_routine_chart_period_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_class_routine_details(string Session_id, string Teacher_id)
        {
            DateTime cfromDateTime = Convert.ToDateTime(mycode.date());
            string cDate = cfromDateTime.ToString("dd/MM/yyyy");
            int ciDate = My.toint(cfromDateTime.ToString("yyyyMMdd"));
            string cDay = cfromDateTime.ToString("dddd");

            string qry = "select t1.Period_Name,t1.Period,t1.Period_type,t1.Period_no,t2.Start_Time,t2.End_time,t2.Timespan,t1.Class_id,t3.Section,(select top 1 Course_Name from Add_course_table where course_id=t1.Class_id) as Class_name from Class_Routine_period_Master t1 join Class_Routine_period t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.course_id and t1.Period=t2.Period join Class_Routine_Master_Teacher t3 on t1.Session_id=t3.Session_id and t1.Class_id=t3.Class_id and t1.Period=t3.Class_period where t1.Session_id='" + Session_id + "' and t3.Teacher_id='" + Teacher_id + "' and t3.Day='" + cDay + "'  order by t1.Period_no asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_period_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string period_time = get_time_span(Session_id, dr["Period"].ToString(), dr["Class_id"].ToString(), dr["Section"].ToString());
                    string Subjects_name = "";
                    string Isclass_or_break = "";
                    string Period_type_td = "";
                    string Teachers_name = "";


                    string period_type_in_calender = get_period_type_in_calender(cDay, Session_id, dr["Class_id"].ToString(), ciDate);

                    string tblrowcount = "10"; string isclass_or_break = "shows"; string period_type = "BreakStyle"; string period_type_td = "BreakStyleTd";
                    string subjects = "Break"; string teachers_name = "NA";
                    if (period_type_in_calender == "Class")
                    {
                        if (dr["Period_type"].ToString() == "Class")
                        {
                            //======CHECK IN EDIT ROUTINE
                            DataTable dtedt = My.dataTable("select *,(select top 1 name from user_details where user_id=t1.Teacher_id and status='Active') as Teacher_name,(select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as Subject_name from Class_Routine_Master_Teacher_DateItemwise t1 where Session_id=" + Session_id + " and Class_id=" + dr["Class_id"].ToString() + " and Section='" + dr["Section"].ToString() + "' and Class_period_id=" + dr["Period"].ToString() + " and Routine_idate=" + ciDate + "");
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
                                subjects = get_routine_subject(cDay, Session_id, dr["Class_id"].ToString(), dr["Section"].ToString(), dr["Period"].ToString());
                                string[] stringSeparatorss = new string[] { "~" };
                                string[] arrs = subjects.Split(stringSeparatorss, StringSplitOptions.None);
                                subjects = arrs[0];
                                teachers_name = arrs[1];
                            }
                        }
                    }
                    else
                    {
                        if (dr["Period_type"].ToString() != "Break")
                        {
                            tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class";
                            subjects = period_type_in_calender;
                            teachers_name = "";
                        }
                    }


                    Subjects_name = subjects;
                    Isclass_or_break = isclass_or_break;
                    Period_type_td = period_type_td;
                    Teachers_name = teachers_name;



                    Show_routine_chart_period_head.Add(new Fetch_routine_chart_period_head
                    {
                        Class_name = dr["Class_name"].ToString(),
                        Section = dr["Section"].ToString(),
                        Period_Name = dr["Period_Name"].ToString(),
                        Period_type = dr["Period_type"].ToString(),
                        period_times = period_time,

                        Subjects_name = Subjects_name,
                        Isclass_or_break = Isclass_or_break,
                        Period_type_td = Period_type_td,
                        Teachers_name = Teachers_name,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_routine_chart_period_head));
            }
        }

        private string get_time_span(string Session_id, string period_id, string classid, string session)
        {
            string query = "Select format(Start_Time, 'hh:mm:tt') as Start_Time_show,format(End_time, 'hh:mm:tt') as End_time_show  from Class_Routine_period where Session_id='" + Session_id + "' and Period='" + period_id + "' and course_id='" + classid + "'   ";
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
            SqlCommand cmd = new SqlCommand("select (select top 1 name from user_details where user_id=Class_Routine_Master_Teacher.Teacher_id and status='Active') as Teacher_name from Class_Routine_Master_Teacher where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Subject_id='" + subject_id + "' and Class_period='" + period_id + "' and Day='" + day + "'");
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




        ///==========================================WITH TEACHER
        ///==========================================WITH TEACHER
        ///==========================================WITH TEACHER
        ///==========================================WITH TEACHER
        ///==========================================WITH TEACHER
        public class Fetch_routine_chart_period_head_teacher
        {
            public string Period_Name { get; set; }
            public string Period_type { get; set; }
            public string period_times { get; set; }
            public string TblwidtH { get; set; }
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
            if (rowcount1 == 0)
            {
            }
            else
            {
                int Tblwidth = 170 * rowcount1;
                //int Tblwidth = 100 + Tblwidths;
                foreach (DataRow dr in dt.Rows)
                {
                    string period_time = get_time_span(Session_id, dr["Period"].ToString(), Class_id, Section);
                    Show_routine_chart_period_head_teacher.Add(new Fetch_routine_chart_period_head_teacher
                    {
                        Period_Name = dr["Period_Name"].ToString(),
                        Period_type = dr["Period_type"].ToString(),
                        period_times = period_time,
                        TblwidtH = Tblwidth.ToString() + "px",
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
            public string IsMyClass { get; set; }
        }


        List<MyRoutineDayTeacher> EMySubMarkTeacher = new List<MyRoutineDayTeacher>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_class_routine_details_day_teacher(string Session_id, string Class_id, string Section, string Teacher_id)
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
                    List<MyRoutineSubjectTeacher> MBdetails = findmyRoutineDtTeacher(dr["Day"].ToString(), Session_id, Class_id, Section, rowconts, daysPlus, Teacher_id);
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


        private List<MyRoutineSubjectTeacher> findmyRoutineDtTeacher(string Day, string Session_id, string Class_id, string Section, int rowconts, int daysPlus, string Teacher_id)
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
                string isMyClass = "";
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
                                        if (dredt["Teacher_id"].ToString() == Teacher_id)
                                        {
                                            isMyClass = "yesMyClass";
                                        }
                                    }
                                    subj_name = subj_name.Remove(subj_name.Length - 1);
                                    teacher_name = teacher_name.Remove(teacher_name.Length - 1);
                                    subjects = subj_name;
                                    teachers_name = teacher_name;
                                }
                                else
                                {
                                    tblrowcount = "1"; isclass_or_break = "shows"; period_type = "Class"; period_type_td = "Class"; hide_if_break = "showd";
                                    subjects = get_routine_subject_with_teacher(Day, Session_id, Class_id, Section, dt.Rows[i]["Period"].ToString(), Teacher_id);
                                    string[] stringSeparatorss = new string[] { "~" };
                                    string[] arrs = subjects.Split(stringSeparatorss, StringSplitOptions.None);
                                    subjects = arrs[0];
                                    teachers_name = arrs[1];
                                    isMyClass = arrs[2];
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
                            IsMyClass = isMyClass,
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
                                        if (dredt["Teacher_id"].ToString() == Teacher_id)
                                        {
                                            isMyClass = "yesMyClass";
                                        }
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
                                    subjects = get_routine_subject_with_teacher(Day, Session_id, Class_id, Section, dt.Rows[i]["Period"].ToString(), Teacher_id);
                                    string[] stringSeparatorss = new string[] { "~" };
                                    string[] arrs = subjects.Split(stringSeparatorss, StringSplitOptions.None);
                                    subjects = arrs[0];
                                    teachers_name = arrs[1];
                                    isMyClass = arrs[2];
                                }
                            }
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
                            IsMyClass = isMyClass,
                        });
                    }
                }
            }
            return MyRoutineSubjectTeacherItem;
        }


        private string get_routine_subject_with_teacher(string day, string session_id, string class_id, string section, string period_id, string Teacher_id)
        {
            string subj_name = ""; string teacher_name = ""; string IsMyClass = "NA";
            SqlCommand cmd = new SqlCommand("Select  *,(select top 1 Subject_name from Subject_Master where course_id=Class_routine_period_subject_mapping.Class_id  and Subject_id=Class_routine_period_subject_mapping.Subject_id) as subjectname from Class_routine_period_subject_mapping where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Class_period='" + period_id + "' and Day='" + day + "'");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    subj_name = subj_name + dr["subjectname"].ToString() + ",";
                    string teachers = get_teacher_t(day, session_id, class_id, section, period_id, dr["Subject_id"].ToString(), Teacher_id);

                    string[] stringSeparatorss = new string[] { "~" };
                    string[] arrs = teachers.Split(stringSeparatorss, StringSplitOptions.None);
                    teachers = arrs[0];
                    string teachers_id = arrs[1];

                    teacher_name = teacher_name + teachers + ",";
                    if (teachers_id == Teacher_id)
                    {
                        IsMyClass = "yesMyClass";
                    }
                }
                subj_name = subj_name.Remove(subj_name.Length - 1);
                teacher_name = teacher_name.Remove(teacher_name.Length - 1);
            }
            else
            {
                subj_name = "-";
            }
            return subj_name + "~" + teacher_name + "~" + IsMyClass;
        }


        private string get_teacher_t(string day, string session_id, string class_id, string section, string period_id, string subject_id, string Teacher_id)
        {
            string teacher_name = "";
            string teacher_id = "";
            SqlCommand cmd = new SqlCommand("select (select top 1 name from user_details where user_id=Class_Routine_Master_Teacher.Teacher_id and status='Active') as Teacher_name,Teacher_id from Class_Routine_Master_Teacher where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Subject_id='" + subject_id + "' and Class_period='" + period_id + "' and Day='" + day + "'");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    teacher_name = dr["Teacher_name"].ToString();
                    teacher_id = dr["Teacher_id"].ToString();
                }
            }
            else
            {
                teacher_name = "NA"; teacher_id = "NA";
            }
            return teacher_name + "~" + teacher_id;
        }
    }
}
