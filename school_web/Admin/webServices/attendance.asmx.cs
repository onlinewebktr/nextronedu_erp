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
    /// Summary description for attendance
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class attendance : System.Web.Services.WebService
    {
        #region MonthWisE
        My mycode = new My();
        public class Fetch_routine_chart_period_head
        {
            public string daY { get; set; }
            public string dayName { get; set; }
            public string dayNameClass { get; set; }
        }
        List<Fetch_routine_chart_period_head> Show_routine_chart_period_head = new List<Fetch_routine_chart_period_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_attendance_of_monthwise_heading(string Session_id, string Class_id, string Section, string Years, string Months)
        {
            string curent_month_y = mycode.get_current_month_year_id();
            if (My.toint(curent_month_y) == My.toint(Years + Months))
            {
                int daysinmonth = My.toint(mycode.daysingle()); //DateTime.DaysInMonth(My.toint(Years), My.toint(Months));
                for (int i = 1; i <= daysinmonth; i++)
                {
                    string day = i.ToString("00");
                    string dates = day + "/" + Months + "/" + Years;
                    DateTime finaldate = DateTime.ParseExact(dates, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dayName = finaldate.ToString("ddd");
                    string daynameClass = "";
                    if (dayName == "Sun")
                    {
                        daynameClass = "daySunday";
                    }
                    Show_routine_chart_period_head.Add(new Fetch_routine_chart_period_head
                    {
                        daY = day,
                        dayName = dayName,
                        dayNameClass = daynameClass,
                    });
                }
            }
            else
            {
                int daysinmonth = DateTime.DaysInMonth(My.toint(Years), My.toint(Months));
                for (int i = 1; i <= daysinmonth; i++)
                {
                    string day = i.ToString("00");
                    string dates = day + "/" + Months + "/" + Years;
                    DateTime finaldate = DateTime.ParseExact(dates, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dayName = finaldate.ToString("ddd");
                    string daynameClass = "";
                    if (dayName == "Sun")
                    {
                        daynameClass = "daySunday";
                    }
                    Show_routine_chart_period_head.Add(new Fetch_routine_chart_period_head
                    {
                        daY = day,
                        dayName = dayName,
                        dayNameClass = daynameClass,
                    });
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_routine_chart_period_head));
        }






        public class MyStudent_infO
        {
            public string Class_name { get; set; }
            public string Admission_no { get; set; }
            public string Student_name { get; set; }
            public string Section { get; set; }
            public string Roll_no { get; set; }
            public List<MyAttendanceS> MyAttendanceSItem { get; set; }
        }

        public class MyAttendanceS
        {
            public string AttendanceS { get; set; }
            public string dayNameClass { get; set; }
            public string Total_no_of_days { get; set; }
            public string Total_holiday_days { get; set; }
            public string Total_persent_days { get; set; }
            public string Total_absent_days { get; set; }
            public string Total_leave_days { get; set; }
            public string Total_no_of_days_less_one { get; set; }
            public string Total_working_days { get; set; }
            public string Attendance_perc { get; set; }
        }


        List<MyStudent_infO> EMySubMark = new List<MyStudent_infO>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_attendance_of_monthwise(string Session_id, string Class_id, string Section, string Years, string Months)
        {
            string qry = "select * from admission_registor where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Status='1' order by rollnumber asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyAttendanceS> MBdetails = findmyAttendanceDt(dr["admissionserialnumber"].ToString(), Session_id, Class_id, Section, Years, Months);
                    EMySubMark.Add(new MyStudent_infO
                    {
                        Class_name = dr["class"].ToString(),
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        Student_name = dr["studentname"].ToString(),
                        Section = dr["Section"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),
                        MyAttendanceSItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }


        private List<MyAttendanceS> findmyAttendanceDt(string admission_no, string Session_id, string Class_id, string Section, string Years, string Months)
        {
            List<MyAttendanceS> MyAttendanceSItem = new List<MyAttendanceS>();
            string idates = "";
            string curent_month_y = mycode.get_current_month_year_id();
            if (My.toint(curent_month_y) == My.toint(Years + Months))
            {
                int daysinmonth = My.toint(mycode.daysingle());
                int total_no_of_days = My.toint(daysinmonth);
                int total_no_of_days_less_one = total_no_of_days - 1;
                int total_holiday_days = 0;
                int total_persent_days = 0;
                int total_absent_days = 0;
                int total_leave_days = 0;
                int total_working_days = 0;
                double attendance_perc = 0;
                for (int i = 1; i <= daysinmonth; i++)
                {
                    string day = i.ToString("00");
                    string mnth = My.toint(Months).ToString("00");
                    string dates = day + "/" + mnth + "/" + Years;
                    idates = Years + mnth + day;
                    DateTime finaldate = DateTime.ParseExact(dates, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dayName = finaldate.ToString("dddd");

                    string query = "select * from Student_Attendance_saved_Class_Wise where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Admission_no='" + admission_no + "' and Attendance_IDate='" + idates + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Class_Routine_period_Master");
                    DataTable dt = ds.Tables[0];
                    int rowcount1 = dt.Rows.Count;
                    if (rowcount1 == 0)
                    {
                        string AttendanceS_type = "";
                        string period_type_in_calender = get_period_type_in_calender(dayName, Session_id, Class_id, My.toint(idates));
                        string daynameClass = "";
                        if (period_type_in_calender == "Holiday")
                        {
                            daynameClass = "daySunday";
                        }
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                        }

                        if (period_type_in_calender == "Class")
                        {
                            AttendanceS_type = "NA";
                            total_holiday_days++;
                        }
                        else if (period_type_in_calender == "Holiday")
                        {
                            AttendanceS_type = period_type_in_calender;
                            total_holiday_days++;
                        }
                        else
                        {
                            AttendanceS_type = period_type_in_calender;
                            total_holiday_days++;
                        }

                        if (AttendanceS_type == "NA")
                        {
                            daynameClass = "notattendances";
                        }


                        total_working_days = total_no_of_days - total_holiday_days;
                        attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                        MyAttendanceSItem.Add(new MyAttendanceS
                        {
                            AttendanceS = AttendanceS_type,
                            dayNameClass = daynameClass,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),
                        }); ;
                    }
                    else
                    {
                        string daynameClass = "";
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                            total_holiday_days++;
                        }
                        string attt_type = "";
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                        {
                            attt_type = "P"; daynameClass = "daypresenT";
                            total_persent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                        {
                            attt_type = "A"; daynameClass = "dayabsenT";
                            total_absent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                        {
                            attt_type = "L"; daynameClass = "dayleavE";
                            total_leave_days++;
                        }
                        total_working_days = total_no_of_days - total_holiday_days;
                        attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                        MyAttendanceSItem.Add(new MyAttendanceS
                        {
                            AttendanceS = attt_type,
                            dayNameClass = daynameClass,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),
                        });
                    }
                }
            }
            else
            {
                int daysinmonth = DateTime.DaysInMonth(My.toint(Years), My.toint(Months));
                int total_no_of_days = My.toint(daysinmonth);
                int total_no_of_days_less_one = total_no_of_days - 1;
                int total_holiday_days = 0;
                int total_persent_days = 0;
                int total_absent_days = 0;
                int total_leave_days = 0;
                int total_working_days = 0;
                double attendance_perc = 0;
                for (int i = 1; i <= daysinmonth; i++)
                {
                    string day = i.ToString("00");
                    string mnth = My.toint(Months).ToString("00");
                    string dates = day + "/" + mnth + "/" + Years;
                    idates = Years + mnth + day;

                    DateTime finaldate = DateTime.ParseExact(dates, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dayName = finaldate.ToString("dddd");
                    string query = "select * from Student_Attendance_saved_Class_Wise where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Admission_no='" + admission_no + "' and Attendance_IDate='" + idates + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Class_Routine_period_Master");
                    DataTable dt = ds.Tables[0];
                    int rowcount1 = dt.Rows.Count;
                    if (rowcount1 == 0)
                    {
                        string daynameClass = "";
                        string AttendanceS_type = "";
                        string period_type_in_calender = get_period_type_in_calender(dayName, Session_id, Class_id, My.toint(idates));
                        if (period_type_in_calender == "Class")
                        {
                            AttendanceS_type = "NA";
                            daynameClass = "notattendances";
                            total_absent_days++;
                        }
                        else if (period_type_in_calender == "Holiday")
                        {
                            AttendanceS_type = period_type_in_calender;
                            total_holiday_days++;
                        }
                        else
                        {
                            AttendanceS_type = period_type_in_calender;
                            total_holiday_days++;
                        }

                        if (period_type_in_calender == "Holiday")
                        {
                            daynameClass = "daySunday";
                        }
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                        }
                        total_working_days = total_no_of_days - total_holiday_days;
                        attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                        MyAttendanceSItem.Add(new MyAttendanceS
                        {
                            AttendanceS = AttendanceS_type,
                            dayNameClass = daynameClass,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),
                        });
                    }
                    else
                    {
                        string daynameClass = "";
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                            total_holiday_days++;
                        }
                        string attt_type = "";
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                        {
                            attt_type = "P"; daynameClass = "daypresenT";
                            total_persent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                        {
                            attt_type = "A"; daynameClass = "dayabsenT";
                            total_absent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                        {
                            attt_type = "L"; daynameClass = "dayleavE";
                            total_leave_days++;
                        }
                        total_working_days = total_no_of_days - total_holiday_days;
                        attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                        MyAttendanceSItem.Add(new MyAttendanceS
                        {
                            AttendanceS = attt_type,
                            dayNameClass = daynameClass,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),
                        });
                    }
                }
            }
            return MyAttendanceSItem;
        }


        private string get_period_type_in_calender(string day, string session_id, string class_id, int cidate)
        {
            string returN = "Class";
            string query = "select Type from School_Holiday_Calendar where Idate='" + cidate + "' and Session_id='" + session_id + "' and Day='" + day + "' and Class_id='" + class_id + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                returN = dt.Rows[0]["Type"].ToString();
            }
            return returN;
        }
        #endregion 

        #region MonthWisE 
        public class Fetch_attendance_chart_period_head_student
        {
            public string daY { get; set; }
            public string dayName { get; set; }
            public string dayNameClass { get; set; }
        }
        List<Fetch_attendance_chart_period_head_student> Show_attendance_chart_period_head_student = new List<Fetch_attendance_chart_period_head_student>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_attendance_of_studentwise_heading(string Session_id, string Class_id, string Admission_no, string Years, string Months)
        {
            string curent_month_y = mycode.get_current_month_year_id();
            if (My.toint(curent_month_y) == My.toint(Years + Months))
            {
                int daysinmonth = My.toint(mycode.daysingle()); //DateTime.DaysInMonth(My.toint(Years), My.toint(Months));
                for (int i = 1; i <= daysinmonth; i++)
                {
                    string day = i.ToString("00");
                    string dates = day + "/" + Months + "/" + Years;
                    DateTime finaldate = DateTime.ParseExact(dates, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dayName = finaldate.ToString("ddd");
                    string daynameClass = "";
                    if (dayName == "Sun")
                    {
                        daynameClass = "daySunday";
                    }
                    Show_attendance_chart_period_head_student.Add(new Fetch_attendance_chart_period_head_student
                    {
                        daY = day,
                        dayName = dayName,
                        dayNameClass = daynameClass,
                    });
                }
            }
            else
            {
                int daysinmonth = DateTime.DaysInMonth(My.toint(Years), My.toint(Months));
                for (int i = 1; i <= daysinmonth; i++)
                {
                    string day = i.ToString("00");
                    string dates = day + "/" + Months + "/" + Years;
                    DateTime finaldate = DateTime.ParseExact(dates, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dayName = finaldate.ToString("ddd");
                    string daynameClass = "";
                    if (dayName == "Sun")
                    {
                        daynameClass = "daySunday";
                    }
                    Show_attendance_chart_period_head_student.Add(new Fetch_attendance_chart_period_head_student
                    {
                        daY = day,
                        dayName = dayName,
                        dayNameClass = daynameClass,
                    });
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_attendance_chart_period_head_student));
        }





        public class MyStudent_student_wise_infO
        {
            public string Month_name { get; set; }
            public string Month_Id { get; set; }
            public List<MyStudentWiseAttendanceS> MyStudentWiseAttendanceSItem { get; set; }
        }

        public class MyStudentWiseAttendanceS
        {
            public string AttendanceS { get; set; }
            public string dayNameClass { get; set; }
            public string Total_no_of_days { get; set; }
            public string Total_holiday_days { get; set; }
            public string Total_persent_days { get; set; }
            public string Total_absent_days { get; set; }
            public string Total_leave_days { get; set; }
            public string Total_no_of_days_less_one { get; set; }
            public string Total_working_days { get; set; }
            public string Attendance_perc { get; set; }

            //HeadinG
            public string daYHead { get; set; }
            public string dayNameHead { get; set; }
            public string dayNameClassHead { get; set; }
        }


        List<MyStudent_student_wise_infO> EMySubMarkSTD = new List<MyStudent_student_wise_infO>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_attendance_of_monthwise_studentwise(string Session_id, string Session_name, string Class_id, string Admission_no)
        {
            string month_id = mycode.cmonth();
            string month_position = month_positions(month_id);

            string qry = "select * from Month_Index where Position<=" + month_position + " order by Position asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Month_Index");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyStudentWiseAttendanceS> MBdetails = findmyAttendanceDtSTD(Session_id, Session_name, Class_id, Admission_no, dr["Month"].ToString(), dr["Month_Id"].ToString());
                    EMySubMarkSTD.Add(new MyStudent_student_wise_infO
                    {
                        Month_name = dr["Month"].ToString(),
                        Month_Id = dr["Month_Id"].ToString(),
                        MyStudentWiseAttendanceSItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMarkSTD));
            }
        }

        private string month_positions(string month_id)
        {
            string month_positions = "0";
            DataTable dtM = My.dataTable("select Position from Month_Index where Month_Id='" + month_id + "'");
            if (dtM.Rows.Count > 0)
            {
                month_positions = dtM.Rows[0][0].ToString();
            }
            return month_positions;
        }

        private List<MyStudentWiseAttendanceS> findmyAttendanceDtSTD(string Session_id, string Session_name, string Class_id, string Admission_no, string MonthName, string Month_Id)
        {
            List<MyStudentWiseAttendanceS> MyStudentWiseAttendanceSItem = new List<MyStudentWiseAttendanceS>();
            string cunrt_session = Session_name;
            string[] stringSeparators = new string[] { "-" };
            string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
            string session_frst_year = arr[0];
            string session_last_year = arr[1];
            int session_s_year = My.toint(session_frst_year);
            int s_year = My.toint(session_frst_year);
            s_year = My.check_start_months(My.toint(Month_Id), s_year);

            string idates = "";
            string curent_month_y = mycode.get_current_month_year_id();
            if (My.toint(curent_month_y) == My.toint(s_year + Month_Id))
            {
                int daysinmonth = My.toint(mycode.daysingle());
                int total_no_of_days = My.toint(daysinmonth);
                int total_no_of_days_less_one = total_no_of_days - 1;
                int total_holiday_days = 0;
                int total_persent_days = 0;
                int total_absent_days = 0;
                int total_leave_days = 0;
                int total_working_days = 0;
                double attendance_perc = 0;
                for (int i = 1; i <= daysinmonth; i++)
                {
                    string day = i.ToString("00");
                    string mnth = My.toint(Month_Id).ToString("00");
                    string dates = day + "/" + mnth + "/" + s_year.ToString();
                    idates = s_year.ToString() + mnth + day;
                    DateTime finaldate = DateTime.ParseExact(dates, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dayName = finaldate.ToString("dddd");
                    string dayNameHead = finaldate.ToString("ddd");


                    string query = "select * from Student_Attendance_saved_Class_Wise where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Attendance_IDate='" + idates + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Class_Routine_period_Master");
                    DataTable dt = ds.Tables[0];
                    int rowcount1 = dt.Rows.Count;
                    if (rowcount1 == 0)
                    {
                        string AttendanceS_type = "";
                        string period_type_in_calender = get_period_type_in_calender(dayName, Session_id, Class_id, My.toint(idates));
                        string daynameClass = "";
                        if (period_type_in_calender == "Holiday")
                        {
                            daynameClass = "daySunday";
                        }
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                        }

                        if (period_type_in_calender == "Class")
                        {
                            AttendanceS_type = "NA";
                            total_holiday_days++;
                        }
                        else if (period_type_in_calender == "Holiday")
                        {
                            AttendanceS_type = period_type_in_calender;
                            total_holiday_days++;
                        }
                        else
                        {
                            AttendanceS_type = period_type_in_calender;
                            total_holiday_days++;
                        }
                        if (AttendanceS_type == "NA")
                        {
                            daynameClass = "notattendances";
                        }


                        //Head
                        string daynameClassHead = "";
                        if (dayNameHead == "Sun")
                        {
                            daynameClassHead = "daySunday";
                        }


                        total_working_days = total_no_of_days - total_holiday_days;
                        attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                        MyStudentWiseAttendanceSItem.Add(new MyStudentWiseAttendanceS
                        {
                            AttendanceS = AttendanceS_type,
                            dayNameClass = daynameClass,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),

                            //Head
                            daYHead = day,
                            dayNameHead = dayNameHead,
                            dayNameClassHead = daynameClassHead,
                        }); ;
                    }
                    else
                    {
                        string daynameClass = "";
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                            total_holiday_days++;
                        }
                        string attt_type = "";
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                        {
                            attt_type = "P"; daynameClass = "daypresenT";
                            total_persent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                        {
                            attt_type = "A"; daynameClass = "dayabsenT";
                            total_absent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                        {
                            attt_type = "L"; daynameClass = "dayleavE";
                            total_leave_days++;
                        }

                        //Head
                        string daynameClassHead = "";
                        if (dayNameHead == "Sun")
                        {
                            daynameClassHead = "daySunday";
                        }

                        total_working_days = total_no_of_days - total_holiday_days;
                        attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                        MyStudentWiseAttendanceSItem.Add(new MyStudentWiseAttendanceS
                        {
                            AttendanceS = attt_type,
                            dayNameClass = daynameClass,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),

                            //Head
                            daYHead = day,
                            dayNameHead = dayNameHead,
                            dayNameClassHead = daynameClassHead,
                        });
                    }
                }
            }
            else
            {
                int daysinmonth = DateTime.DaysInMonth(My.toint(s_year), My.toint(Month_Id));
                int total_no_of_days = My.toint(daysinmonth);
                int total_no_of_days_less_one = total_no_of_days - 1;
                int total_holiday_days = 0;
                int total_persent_days = 0;
                int total_absent_days = 0;
                int total_leave_days = 0;
                int total_working_days = 0;
                double attendance_perc = 0;
                for (int i = 1; i <= daysinmonth; i++)
                {
                    string day = i.ToString("00");
                    string mnth = My.toint(Month_Id).ToString("00");
                    string dates = day + "/" + mnth + "/" + s_year.ToString();
                    idates = s_year.ToString() + mnth + day;

                    DateTime finaldate = DateTime.ParseExact(dates, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dayName = finaldate.ToString("dddd");
                    string dayNameHead = finaldate.ToString("ddd");
                    string query = "select * from Student_Attendance_saved_Class_Wise where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Attendance_IDate='" + idates + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Class_Routine_period_Master");
                    DataTable dt = ds.Tables[0];
                    int rowcount1 = dt.Rows.Count;
                    if (rowcount1 == 0)
                    {
                        string daynameClass = "";
                        string AttendanceS_type = "";
                        string period_type_in_calender = get_period_type_in_calender(dayName, Session_id, Class_id, My.toint(idates));
                        if (period_type_in_calender == "Class")
                        {
                            AttendanceS_type = "NA";
                            daynameClass = "notattendances";
                            total_absent_days++;
                        }
                        else if (period_type_in_calender == "Holiday")
                        {
                            AttendanceS_type = period_type_in_calender;
                            total_holiday_days++;
                        }
                        else
                        {
                            AttendanceS_type = period_type_in_calender;
                            total_holiday_days++;
                        }

                        if (period_type_in_calender == "Holiday")
                        {
                            daynameClass = "daySunday";
                        }
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                        }
                        //Head
                        string daynameClassHead = "";
                        if (dayNameHead == "Sun")
                        {
                            daynameClassHead = "daySunday";
                        }
                        total_working_days = total_no_of_days - total_holiday_days;
                        attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                        MyStudentWiseAttendanceSItem.Add(new MyStudentWiseAttendanceS
                        {
                            AttendanceS = AttendanceS_type,
                            dayNameClass = daynameClass,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),

                            //Head
                            daYHead = day,
                            dayNameHead = dayNameHead,
                            dayNameClassHead = daynameClassHead,
                        });
                    }
                    else
                    {
                        string daynameClass = "";
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                            total_holiday_days++;
                        }
                        string attt_type = "";
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                        {
                            attt_type = "P"; daynameClass = "daypresenT";
                            total_persent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                        {
                            attt_type = "A"; daynameClass = "dayabsenT";
                            total_absent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                        {
                            attt_type = "L"; daynameClass = "dayleavE";
                            total_leave_days++;
                        }
                        //Head
                        string daynameClassHead = "";
                        if (dayNameHead == "Sun")
                        {
                            daynameClassHead = "daySunday";
                        }
                        total_working_days = total_no_of_days - total_holiday_days;
                        attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                        MyStudentWiseAttendanceSItem.Add(new MyStudentWiseAttendanceS
                        {
                            AttendanceS = attt_type,
                            dayNameClass = daynameClass,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),

                            //Head
                            daYHead = day,
                            dayNameHead = dayNameHead,
                            dayNameClassHead = daynameClassHead,
                        });
                    }
                }
            }
            return MyStudentWiseAttendanceSItem;
        }

        #endregion 

        #region DateWisE  

        public class MyStudent_info_DateWise
        {
            public string Class_name { get; set; }
            public string Admission_no { get; set; }
            public string Student_name { get; set; }
            public string Section { get; set; }
            public string Roll_no { get; set; }
            public List<MyAttendanceSDateWise> MyAttendanceSDateWiseItem { get; set; }
        }

        public class MyAttendanceSDateWise
        {
            public string AttendanceS { get; set; }
            public string dayNameClass { get; set; }
            public string Total_no_of_days { get; set; }
            public string Total_holiday_days { get; set; }
            public string Total_persent_days { get; set; }
            public string Total_absent_days { get; set; }
            public string Total_leave_days { get; set; }
            public string Total_no_of_days_less_one { get; set; }
            public string Total_working_days { get; set; }
            public string Attendance_perc { get; set; }
        }


        List<MyStudent_info_DateWise> EMySubMarkDateWise = new List<MyStudent_info_DateWise>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_attendance_of_two_date(string Session_id, string Class_id, string Section, string From_date, string To_date, string Find_by, string Admission_no)
        {
            string qry = "select * from admission_registor where Session_id='" + Session_id + "' and admissionserialnumber='" + Admission_no + "' and Status='1' order by rollnumber asc";
            if (Find_by == "1")
            {
                qry = "select * from admission_registor where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Status='1' order by rollnumber asc";
            }

            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyAttendanceSDateWise> MBdetails = findmyAttendanceDtDateWise(dr["admissionserialnumber"].ToString(), Session_id, dr["Class_id"].ToString(), dr["Section"].ToString(), From_date, To_date);
                    EMySubMarkDateWise.Add(new MyStudent_info_DateWise
                    {
                        Class_name = dr["class"].ToString(),
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        Student_name = dr["studentname"].ToString(),
                        Section = dr["Section"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),
                        MyAttendanceSDateWiseItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMarkDateWise));
            }
        }

        private List<MyAttendanceSDateWise> findmyAttendanceDtDateWise(string admission_no, string Session_id, string Class_id, string Section, string From_date, string To_date)
        {
            List<MyAttendanceSDateWise> MyAttendanceSDateWiseItem = new List<MyAttendanceSDateWise>();
            int fromidate = My.DateConvertToIdate(From_date);
            int toidate = My.DateConvertToIdate(To_date);



            string idates = "";
            int total_no_of_days = 0;
            int total_no_of_days_less_one = (toidate - fromidate);
            int total_holiday_days = 0;
            int total_persent_days = 0;
            int total_absent_days = 0;
            int total_leave_days = 0;
            int total_working_days = 0;
            double attendance_perc = 0;

            DateTime finaldate = DateTime.ParseExact(From_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = fromidate; i <= toidate; i++)
            {
                total_no_of_days++;
                //string day = i.ToString("00");
                //string mnth = My.toint(Months).ToString("00");
                string dates = finaldate.ToString("dd/MM/yyyy"); //day + "/" + mnth + "/" + Years;
                idates = My.DateConvertToIdate(dates).ToString();

                if (toidate == My.toint(idates))
                {
                    i = My.toint(idates);
                }
                //finaldate = DateTime.ParseExact(dates, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string dayName = finaldate.ToString("dddd");
                string query = "select * from Student_Attendance_saved_Class_Wise where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Admission_no='" + admission_no + "' and Attendance_IDate='" + idates + "'";
                SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Class_Routine_period_Master");
                DataTable dt = ds.Tables[0];
                int rowcount1 = dt.Rows.Count;
                if (rowcount1 == 0)
                {
                    string daynameClass = "";
                    string AttendanceS_type = "";
                    string period_type_in_calender = get_period_type_in_calender(dayName, Session_id, Class_id, My.toint(idates));
                    if (period_type_in_calender == "Class")
                    {
                        AttendanceS_type = "NA";
                        daynameClass = "notattendances";
                        total_absent_days++;
                    }
                    else if (period_type_in_calender == "Holiday")
                    {
                        AttendanceS_type = period_type_in_calender;
                        total_holiday_days++;
                    }
                    else
                    {
                        AttendanceS_type = period_type_in_calender;
                        total_holiday_days++;
                    }

                    if (period_type_in_calender == "Holiday")
                    {
                        daynameClass = "daySunday";
                    }
                    if (dayName == "Sunday")
                    {
                        daynameClass = "daySunday";
                    }
                    total_working_days = total_no_of_days - total_holiday_days;
                    attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                }
                else
                {
                    string daynameClass = "";
                    if (dayName == "Sunday")
                    {
                        daynameClass = "daySunday";
                        total_holiday_days++;
                    }
                    string attt_type = "";
                    if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                    {
                        attt_type = "P"; daynameClass = "daypresenT";
                        total_persent_days++;
                    }
                    if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                    {
                        attt_type = "A"; daynameClass = "dayabsenT";
                        total_absent_days++;
                    }
                    if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                    {
                        attt_type = "L"; daynameClass = "dayleavE";
                        total_leave_days++;
                    }
                    total_working_days = total_no_of_days - total_holiday_days;
                    attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                }
                finaldate = finaldate.AddDays(1);
            }


            MyAttendanceSDateWiseItem.Add(new MyAttendanceSDateWise
            {
                Total_no_of_days = total_no_of_days.ToString(),
                Total_holiday_days = total_holiday_days.ToString(),
                Total_persent_days = total_persent_days.ToString(),
                Total_absent_days = total_absent_days.ToString(),
                Total_leave_days = total_leave_days.ToString(),
                Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                Total_working_days = total_working_days.ToString(),
                Attendance_perc = attendance_perc.ToString("0.00"),
            });

            return MyAttendanceSDateWiseItem;
        }

        #endregion

        public class My_classwise_attendance_info
        {
            public string Class_name { get; set; }
            public string Section { get; set; }
            public string TtlStd { get; set; }
            public string TtlStudents { get; set; }
            public string TtlPresent { get; set; }
            public string TtlAbsent { get; set; }
            public string TtlLeave { get; set; }
            public string Marked { get; set; }
            public string MarkedBy { get; set; }
        }


        List<My_classwise_attendance_info> My_classwise_attendance_infoItem = new List<My_classwise_attendance_info>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_classwise_attendance_summary(string Session, string Session_id, string Class_id, string Section, string Idate)
        {
            string qoute = "'";
            string Class_ids = "";
            string[] stringSeparatorss = new string[] { "," };
            string[] arr = Class_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                Class_ids = Class_ids + qoute + value + qoute + ",";
            }
            Class_ids = Class_ids.Remove(Class_ids.Length - 1);

            //string qry = "WITH admission_counts AS (SELECT Session_id, Class_id, Section, COUNT(Id) AS TtlStd FROM admission_registor WHERE Status = 1 GROUP BY Session_id, Class_id, Section ), attendance_counts AS (SELECT  Session_id, Class_id, Section, COUNT(Id) AS TtlStudents, SUM(CASE WHEN Attendance_Status = 'Present' THEN 1 ELSE 0 END) AS TtlPresent, SUM(CASE WHEN Attendance_Status ='Absent' THEN 1 ELSE 0 END) AS TtlAbsent, SUM(CASE WHEN Attendance_Status = 'Leave' THEN 1 ELSE 0 END) AS TtlLeave FROM Student_Attendance_saved_Class_Wise WHERE Attendance_IDate='" + Idate + "' GROUP BY Session_id, Class_id, Section) SELECT  t.*, ISNULL(ac.TtlStd, 0) AS TtlStd, ISNULL(at.TtlStudents, 0) AS TtlStudents, ISNULL(at.TtlPresent, 0) AS TtlPresent, ISNULL(at.TtlAbsent, 0) AS TtlAbsent, ISNULL(at.TtlLeave, 0) AS TtlLeave FROM (SELECT DISTINCT t1.Session_id, t1.class, t1.Class_id, t1.Section, t2.Position FROM admission_registor t1 JOIN Add_course_table t2 ON t1.Class_id = t2.course_id WHERE t1.Session_id = '" + Session_id + "' AND t1.Status = '1' and t1.Class_id in (" + Class_ids + ")) t LEFT JOIN admission_counts ac ON t.Session_id = ac.Session_id AND t.Class_id = ac.Class_id AND t.Section = ac.Section LEFT JOIN attendance_counts at ON t.Session_id = at.Session_id AND t.Class_id = at.Class_id AND t.Section = at.Section ORDER BY t.Position, t.Section ASC";
            string qry = "WITH admission_counts AS (SELECT Session_id, Class_id, Section, COUNT(Id) AS TtlStd FROM admission_registor WHERE Status = 1 GROUP BY Session_id, Class_id, Section), attendance_counts AS (SELECT Session_id, Class_id, Section, COUNT(Id) AS TtlStudents, SUM(CASE WHEN Attendance_Status = 'Present' THEN 1 ELSE 0 END) AS TtlPresent, SUM(CASE WHEN Attendance_Status ='Absent' THEN 1 ELSE 0 END) AS TtlAbsent, SUM(CASE WHEN Attendance_Status = 'Leave' THEN 1 ELSE 0 END) AS TtlLeave, MAX(Created_By) AS Created_By FROM Student_Attendance_saved_Class_Wise WHERE Attendance_IDate = '" + Idate + "' GROUP BY Session_id, Class_id, Section) SELECT t.*, ISNULL(ac.TtlStd, 0) AS TtlStd, ISNULL(at.TtlStudents, 0) AS TtlStudents, ISNULL(at.TtlPresent, 0) AS TtlPresent, ISNULL(at.TtlAbsent, 0) AS TtlAbsent, ISNULL(at.TtlLeave, 0) AS TtlLeave, ISNULL(at.Created_By, 0) AS Created_By, ISNULL(ud.name, '-') AS CreatedByName FROM (SELECT DISTINCT t1.Session_id, t1.class, t1.Class_id, t1.Section, t2.Position FROM admission_registor t1 JOIN Add_course_table t2 ON t1.Class_id = t2.course_id WHERE t1.Session_id = '" + Session_id + "' AND t1.Status = '1' AND t1.Class_id IN (" + Class_ids + ")) t LEFT JOIN admission_counts ac ON t.Session_id = ac.Session_id AND t.Class_id = ac.Class_id AND t.Section = ac.Section LEFT JOIN attendance_counts at ON t.Session_id = at.Session_id AND t.Class_id = at.Class_id AND t.Section = at.Section LEFT JOIN user_details ud ON ud.user_id = at.Created_By ORDER BY t.Position, t.Section ASC;";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Marked = "Yes";
                    if (My.toDouble(dr["TtlStudents"].ToString()) == 0)
                    {
                        if (My.toDouble(dr["TtlStd"].ToString()) > 0)
                        {
                            Marked = "No";
                        }
                    }
                    My_classwise_attendance_infoItem.Add(new My_classwise_attendance_info
                    {
                        Class_name = dr["class"].ToString(),
                        Section = dr["Section"].ToString(),
                        TtlStd = dr["TtlStd"].ToString(),
                        TtlStudents = dr["TtlStudents"].ToString(),
                        TtlPresent = dr["TtlPresent"].ToString(),
                        TtlAbsent = dr["TtlAbsent"].ToString(),
                        TtlLeave = dr["TtlLeave"].ToString(),
                        Marked = Marked,
                        MarkedBy = dr["CreatedByName"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(My_classwise_attendance_infoItem));
            }
        }


        //==================================================================================
        public class My_classwise_attendance_status_head
        {
            public string DayMonth { get; set; }
            public string DayS { get; set; }
        }
        List<My_classwise_attendance_status_head> My_classwise_attendance_status_headItem = new List<My_classwise_attendance_status_head>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_classwise_attendance_status_header(string Session, string Session_id, string Class_id, string Section, string From_date, string To_date)
        {
            string qry = "DECLARE @StartDate DATE = CONVERT(DATE, '" + From_date + "', 103); DECLARE @EndDate DATE = CONVERT(DATE, '" + To_date + "', 103); WITH  DateSequence AS (SELECT @StartDate AS DateValue UNION ALL SELECT DATEADD(DAY, 1, DateValue) FROM DateSequence WHERE DATEADD(DAY, 1, DateValue) <= @EndDate) SELECT FORMAT(DateValue, 'dd-MMM') AS FormattedDate,FORMAT(DateValue, 'ddd') AS FormattediDays,FORMAT(DateValue, 'yyyyMMdd') AS FormattediDate FROM DateSequence OPTION (MAXRECURSION 1000)";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    My_classwise_attendance_status_headItem.Add(new My_classwise_attendance_status_head
                    {
                        DayMonth = dr["FormattedDate"].ToString(),
                        DayS = dr["FormattediDays"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(My_classwise_attendance_status_headItem));
            }
        }


        public class My_classwise_attendance_status
        {
            public string Class_name { get; set; }
            public string Section { get; set; }
            public List<MyAttendanceMarkedDate> MyAttendanceMarkedDateItem { get; set; }
        }
        public class MyAttendanceMarkedDate
        {
            public string IsMarked { get; set; }
        }
        List<My_classwise_attendance_status> My_classwise_attendance_statusItem = new List<My_classwise_attendance_status>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_classwise_attendance_status(string Session, string Session_id, string Class_id, string Section, string Idate, string To_idate, string From_date, string To_date)
        {
            string qoute = "'";
            string Class_ids = "";
            string[] stringSeparatorss = new string[] { "," };
            string[] arr = Class_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                Class_ids = Class_ids + qoute + value + qoute + ",";
            }
            Class_ids = Class_ids.Remove(Class_ids.Length - 1);
            string qry = "SELECT distinct t2.Course_Name,t1.Class_id,t1.Section,t2.Position FROM  admission_registor t1 JOIN  Add_course_table t2 ON t1.Class_id=t2.course_id WHERE t1.Session_id='" + Session_id + "' AND t1.Class_id in (" + Class_ids + ") AND t1.Status=1 ORDER BY t2.Position, t1.Section ASC";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyAttendanceMarkedDate> MBdetails = findmyAttendanceStatus(Session_id, dr["Class_id"].ToString(), dr["Section"].ToString(), Idate, To_idate, From_date, To_date);
                    My_classwise_attendance_statusItem.Add(new My_classwise_attendance_status
                    {
                        Class_name = dr["Course_Name"].ToString(),
                        Section = dr["Section"].ToString(),
                        MyAttendanceMarkedDateItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(My_classwise_attendance_statusItem));
            }
        }


        private List<MyAttendanceMarkedDate> findmyAttendanceStatus(string Session_id, string Class_id, string Section, string Idate, string To_idate, string From_date, string To_date)
        {
            List<MyAttendanceMarkedDate> MyAttendanceMarkedDateItem = new List<MyAttendanceMarkedDate>();
            string qry = "DECLARE @StartDate DATE = CONVERT(DATE, '" + From_date + "', 103); DECLARE @EndDate DATE = CONVERT(DATE, '" + To_date + "', 103); WITH  DateSequence AS (SELECT @StartDate AS DateValue UNION ALL SELECT DATEADD(DAY, 1, DateValue) FROM DateSequence WHERE DATEADD(DAY, 1, DateValue) <= @EndDate) SELECT FORMAT(DateValue, 'dd-MMM') AS FormattedDate,FORMAT(DateValue, 'ddd') AS FormattediDays,FORMAT(DateValue, 'yyyyMMdd') AS FormattediDate FROM DateSequence OPTION (MAXRECURSION 1000)";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string status = getIsmarked(Session_id, Class_id, Section, dr["FormattediDate"].ToString());
                    MyAttendanceMarkedDateItem.Add(new MyAttendanceMarkedDate
                    {
                        IsMarked = status,
                    });
                }
            }
            return MyAttendanceMarkedDateItem;
        }

        private string getIsmarked(string session_id, string class_id, string section, string idate)
        {
            string IsMarked = "No";
            string qry = "select count(Id) as ttl_Std from Student_Attendance_saved_Class_Wise where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Attendance_IDate='" + idate + "'";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                if (My.toDouble(dt.Rows[0]["ttl_Std"].ToString()) > 0)
                {
                    IsMarked = "Yes";
                }
            }
            return IsMarked;
        }



        ///==========================================================
        ///
        [WebMethod(EnableSession = true)]
        public string find_attendance_status_report(string Session_id, string Class_ids, string FindIdate)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Type");
            dtDatas.Columns.Add("Total");
            List<object> datasets = new List<object>(); object labels = "";
            object chartData = "";
            DataTable dt = My.dataTable("select isnull(count(Id),0) as TotalStd,'Total Student' as Type from admission_registor where Session_id='" + Session_id + "' and Class_id in (" + Class_ids + ") and Status=1 and Transfer_Status in ('New','NT','NEW') union all select isnull(count(Id),0) as TotalStd, 'Attendance Marked' as Type from Student_Attendance_saved_Class_Wise where Session_id='" + Session_id + "'  and Class_id in (" + Class_ids + ") and Attendance_IDate='" + FindIdate + "' union all select isnull(count(Id),0) as TotalStd,'Present' as Type from Student_Attendance_saved_Class_Wise where Session_id='" + Session_id + "' and Class_id in (" + Class_ids + ") and Attendance_IDate='" + FindIdate + "' and Attendance_Status='Present' union all select isnull(count(Id),0) as TotalStd,'Absent' as Type from Student_Attendance_saved_Class_Wise where Session_id='" + Session_id + "' and Class_id in (" + Class_ids + ") and Attendance_IDate='" + FindIdate + "' and Attendance_Status='Absent' union all select isnull(count(Id),0) as TotalLeave,'Leave' as Type from Student_Attendance_saved_Class_Wise where Session_id='" + Session_id + "' and Class_id in (" + Class_ids + ") and Attendance_IDate='" + FindIdate + "' and Attendance_Status='Leave'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["Type"] = dr["Type"].ToString();
                    drNewRow["Total"] = dr["TotalStd"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }

            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Type")).ToList();
            var colors = new String[] { "#0a66c2", "rgba(54, 162, 235)", "#3cbf2d", "rgba(255, 99, 132)", "rgba(75, 192, 192)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)" };
            for (int i = 1; i < dtDatas.Columns.Count; i++)
            {
                datasets.Add(new
                {
                    label = dtDatas.Columns[i].ColumnName,
                    backgroundColor = colors,
                    borderColor = colors,
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
    }
}
