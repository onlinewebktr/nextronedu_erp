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

namespace school_web.Student_Profile.webview
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
        public void fetch_class_routine_details(string Session_id, string Class_id, string Section)
        { 
            string qry = "select t1.Period_Name,t1.Period,t1.Period_type,t1.Period_no,t2.Start_Time,t2.End_time,t2.Timespan from Class_Routine_period_Master t1 join Class_Routine_period t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.course_id and t1.Period=t2.Period where t1.Class_id='" + Class_id + "' and t1.Session_id='" + Session_id + "' order by t1.Period_no asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_period_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string period_time = get_time_span(Session_id, dr["Period"].ToString(), Class_id, Section);
                    string Subjects_name = "";
                    string Isclass_or_break = "";
                    string Period_type_td = "";
                    string Teachers_name = "";

                    DateTime cfromDateTime = Convert.ToDateTime(mycode.date());
                    string cDate = cfromDateTime.ToString("dd/MM/yyyy");
                    int ciDate = My.toint(cfromDateTime.ToString("yyyyMMdd"));
                    string cDay = cfromDateTime.ToString("dddd");
                    string period_type_in_calender = get_period_type_in_calender(cDay, Session_id, Class_id, ciDate);

                    string tblrowcount = "10"; string isclass_or_break = "shows"; string period_type = "BreakStyle"; string period_type_td = "BreakStyleTd";
                    string subjects = "Break"; string teachers_name = "NA";
                    if (period_type_in_calender == "Class")
                    {
                        if (dr["Period_type"].ToString() == "Class")
                        {
                            //======CHECK IN EDIT ROUTINE
                            DataTable dtedt = My.dataTable("select *,(select top 1 name from user_details where user_id=t1.Teacher_id) as Teacher_name,(select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as Subject_name from Class_Routine_Master_Teacher_DateItemwise t1 where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and Class_period_id=" + dr["Period"].ToString() + " and Routine_idate=" + ciDate + "");
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
                                subjects = get_routine_subject(cDay, Session_id, Class_id, Section, dr["Period"].ToString());
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
            SqlCommand cmd = new SqlCommand("select (select top 1 name from user_details where user_id=Class_Routine_Master_Teacher.Teacher_id) as Teacher_name from Class_Routine_Master_Teacher where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Subject_id='" + subject_id + "' and Class_period='" + period_id + "' and Day='" + day + "'");
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



        public class MyExamRoutineDetails
        {
            public string Subject_name { get; set; }
            public string Day { get; set; }
            public string Exam_date { get; set; }
            public string Exam_time { get; set; }
            public string Exam_end_time1 { get; set; }
            public string Exam_name { get; set; }
        }
        List<MyExamRoutineDetails> MyExamRoutineDetailsShow = new List<MyExamRoutineDetails>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_exam_routine_details(string Session_id, string Class_id, string Section, string Term_id, string Exam_id, string Shift_Type, string Admission_no)
        {
            string query = "Select es.Day,sm.Subject_name,format(es.Exam_Date_time, 'dd/MM/yyyy') as Exam_datetime1,format(es.Exam_Date_time, 'hh:mm tt') as Exam_time1,format(es.Exam_E_Date_time, 'hh:mm tt') as Exam_end_time1 from Subject_Master sm join Exam_Time_Table es on es.Subject_id=sm.Subject_id and es.Class_id=sm.course_id join Subject_Mapping_New smn on smn.Class_id=es.Class_id and smn.Sub_id=es.Subject_id and smn.Session_id=es.Session_Id  where es.Class_id=" + Class_id + " and es.Session_Id=" + Session_id + " and es.Section='" + Section + "' and smn.Admission_no='" + Admission_no + "' and es.Exam_Term_Id=" + Term_id + " and es.Exam_id='" + Exam_id + "' order by cast( (Substring (Exam_Date,7,4)+Substring (Exam_Date,4,2)+Substring (Exam_Date,1,2)) as int),format(es.Exam_Date_time, 'mmhh') asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_Time_Table");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string exam_name = get_exam_name(Session_id, Class_id, Term_id, Exam_id);
                foreach (DataRow dr in dt.Rows)
                {
                    MyExamRoutineDetailsShow.Add(new MyExamRoutineDetails
                    {
                        Subject_name = dr["Subject_name"].ToString(),
                        Day = dr["Day"].ToString(),
                        Exam_date = dr["Exam_datetime1"].ToString(),
                        Exam_time = dr["Exam_time1"].ToString(),
                        Exam_end_time1 = dr["Exam_end_time1"].ToString(),
                        Exam_name = exam_name,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyExamRoutineDetailsShow));
            }
        }

        private string get_exam_name(string session_id, string class_id, string term_id, string exam_id)
        {
            string exam_Name = "";
            DataTable dtt = My.dataTable("select top 1 Assessment_Name from Exam_Assessment_Details where Session_Id='" + session_id + "' and Class_id='" + class_id + "' and Exam_Term_Id='" + term_id + "' and Assessment_Id='" + exam_id + "'");
            if (dtt.Rows.Count > 0)
            {
                exam_Name = dtt.Rows[0]["Assessment_Name"].ToString();
            }
            return exam_Name;
        }



        public class MyExamRoutineDetailsDShift
        {
            public string Subject_name1 { get; set; }
            public string Subject_name2 { get; set; }
            public string Day { get; set; }
            public string Exam_date { get; set; }
            public string Exam_name { get; set; }
            public string Shift1_start { get; set; }
            public string Shift1_end { get; set; }
            public string Shift2_start { get; set; }
            public string Shift2_end { get; set; }
        }
        List<MyExamRoutineDetailsDShift> MyExamRoutineDetailsShowDShift = new List<MyExamRoutineDetailsDShift>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_exam_routine_details_double_shift(string Session_id, string Class_id, string Section, string Term_id, string Exam_id, string Shift_Type, string Admission_no)
        {
            string query = "Select DISTINCT format(es.Exam_Date_time, 'dd/MM/yyyy') as Exam_date,format(es.Exam_Date_time, 'yyyyMMdd') as Exam_idate from Subject_Master sm join Exam_Time_Table es on es.Subject_id = sm.Subject_id and es.Class_id = sm.course_id join Subject_Mapping_New smn on smn.Class_id = es.Class_id and smn.Sub_id = es.Subject_id and smn.Session_id = es.Session_Id  where es.Class_id=" + Class_id + " and es.Session_Id=" + Session_id + " and es.Section= '" + Section + "' and smn.Admission_no = '" + Admission_no + "' and es.Exam_Term_Id =" + Term_id + " and es.Exam_id='" + Exam_id + "' order by format(es.Exam_Date_time, 'yyyyMMdd') asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_Time_Table");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string shift_time = get_shift_time(Session_id, Class_id, Term_id, Section, Exam_id);
                string[] stringSeparatorss = new string[] { "~" };
                string[] arrs = shift_time.Split(stringSeparatorss, StringSplitOptions.None);
                string shift1_start = arrs[0];
                string shift1_end = arrs[1];

                string shift2_start = arrs[2];
                string shift2_end = arrs[3];


                string exam_name = get_exam_name(Session_id, Class_id, Term_id, Exam_id);
                foreach (DataRow dr in dt.Rows)
                {
                    string date = "";
                    string day = "";
                    string subjFirstSHift = "";
                    string subjSecondShift = "";
                    string first_sitting = "";
                    string second_sitting = "";
                    DataTable dtE = My.dataTable("Select es.Day,sm.Subject_name,format(es.Exam_Date_time, 'dd/MM/yyyy') as Exam_datetime1,format(es.Exam_Date_time, 'hh:mm tt') as Exam_time1,format(es.Exam_E_Date_time, 'hh:mm tt') as Exam_E_time1,Shift from Subject_Master sm join Exam_Time_Table es on es.Subject_id=sm.Subject_id and es.Class_id=sm.course_id join   Subject_Mapping_New smn on smn.Class_id=es.Class_id and smn.Sub_id=es.Subject_id and smn.Session_id=es.Session_Id  where es.Class_id=" + Class_id + " and es.Session_Id=" + Session_id + " and es.Section='" + Section + "' and smn.Admission_no='" + Admission_no + "' and es.Exam_Term_Id=" + Term_id + " and format(es.Exam_Date_time, 'yyyyMMdd')='" + dr["Exam_idate"].ToString() + "' and es.Exam_id='" + Exam_id + "' order by Shift asc");

                    if (dtE.Rows.Count == 2)
                    {
                        date = dtE.Rows[0]["Exam_datetime1"].ToString();
                        day = dtE.Rows[0]["Day"].ToString();
                        subjFirstSHift = dtE.Rows[0]["Subject_name"].ToString();
                        subjSecondShift = dtE.Rows[1]["Subject_name"].ToString();
                    }
                    else if (dtE.Rows.Count == 1)
                    {
                        if (dtE.Rows[0]["Shift"].ToString() == "1")
                        {
                            date = dtE.Rows[0]["Exam_datetime1"].ToString();
                            day = dtE.Rows[0]["Day"].ToString();
                            subjFirstSHift = dtE.Rows[0]["Subject_name"].ToString();
                            subjSecondShift = "-";
                        }
                        if (dtE.Rows[0]["Shift"].ToString() == "2")
                        {
                            date = dtE.Rows[0]["Exam_datetime1"].ToString();
                            day = dtE.Rows[0]["Day"].ToString();
                            subjFirstSHift = "-";
                            subjSecondShift = dtE.Rows[0]["Subject_name"].ToString();
                        }
                    }
                    else
                    {
                        date = dtE.Rows[0]["Exam_datetime1"].ToString();
                        day = dtE.Rows[0]["Day"].ToString();
                        subjFirstSHift = "-";
                        subjSecondShift = "-";
                    }
                    MyExamRoutineDetailsShowDShift.Add(new MyExamRoutineDetailsDShift
                    {
                        Subject_name1 = subjFirstSHift,
                        Subject_name2 = subjSecondShift,
                        Day = day,
                        Exam_date = date,
                        Exam_name = exam_name,

                        Shift1_start = shift1_start,
                        Shift1_end = shift1_end,
                        Shift2_start = shift2_start,
                        Shift2_end = shift2_end,
                    });
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyExamRoutineDetailsShowDShift));
            }
        }

        private string get_shift_time(string session_id, string class_id, string term_id, string section, string Exam_id)
        {
            string shift_time = "";
            DataTable dtt = My.dataTable("Select top 1 format(es.Exam_Date_time, 'hh:mm tt') as Exam_time1,format(es.Exam_E_Date_time, 'hh:mm tt') as Exam_E_time1 from Subject_Master sm join Exam_Time_Table es on es.Subject_id=sm.Subject_id and es.Class_id=sm.course_id where es.Class_id=" + class_id + " and es.Session_Id=" + session_id + " and es.Section='" + section + "' and es.Exam_Term_Id=" + term_id + " and es.Exam_id=" + Exam_id + " and Shift=1 UNION all Select top 1 format(es.Exam_Date_time, 'hh:mm tt') as Exam_time1, format(es.Exam_E_Date_time, 'hh:mm tt') as Exam_E_time1 from Subject_Master sm join Exam_Time_Table es on es.Subject_id = sm.Subject_id and es.Class_id = sm.course_id where es.Class_id= " + class_id + "  and es.Session_Id = " + session_id + " and es.Section = '" + section + "' and es.Exam_Term_Id = " + term_id + "  and es.Exam_id=" + Exam_id + " and Shift = 2 ");
            if (dtt.Rows.Count == 2)
            {
                shift_time = dtt.Rows[0]["Exam_time1"].ToString() + "~" + dtt.Rows[0]["Exam_E_time1"].ToString() + "~" + dtt.Rows[1]["Exam_time1"].ToString() + "~" + dtt.Rows[1]["Exam_E_time1"].ToString();
            }
            else if (dtt.Rows.Count == 1)
            {
                shift_time = dtt.Rows[0]["Exam_time1"].ToString() + "~" + dtt.Rows[0]["Exam_E_time1"].ToString() + "~NA" + "~NA";
            }
            else
            {
                shift_time = "NA~NA~NA~NA";
            }
            return shift_time;
        }
    }
}
