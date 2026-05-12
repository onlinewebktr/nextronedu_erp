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

namespace school_web.Student_Profile.webview.webService
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
        My mycode = new My();
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
            public string Holidaydetails { get; set; }

            //HeadinG
            public string daYHead { get; set; }
            public string dayNameHead { get; set; }
            public string dayNameClassHead { get; set; }
        }


        List<MyStudent_student_wise_infO> EMySubMarkSTD = new List<MyStudent_student_wise_infO>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_attendance_of_monthwise_studentwise(string Session_id, string Session_name, string Class_id, string Admission_no, string MonthId)
        {
            string month_id = mycode.cmonth();
            string month_position = month_positions(month_id);

            string qry = "select * from Month_Index where Month_Id='" + MonthId + "' order by Position asc";
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
                    string showDate = finaldate.ToString("dd-MMM");

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

                        string[] stringSeparatorss = new string[] { "~" };
                        string[] arrs = period_type_in_calender.Split(stringSeparatorss, StringSplitOptions.None);
                        period_type_in_calender = arrs[0];
                        string Holidaydetails = arrs[1];
                        if (Holidaydetails.ToUpper() == "CLASS"|| Holidaydetails == "")
                        {
                            Holidaydetails = "hidden";
                        }
                       

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
                            AttendanceS_type = "Attendance not taken by teacher.";
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
                            if (dayName == "Sunday")
                            {
                                total_holiday_days++;
                            }
                            else
                            {
                                total_absent_days++;
                            }
                        }
                        if (AttendanceS_type == "Attendance not taken by teacher.")
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
                            Holidaydetails = Holidaydetails,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),

                            //Head
                            daYHead = showDate,
                            dayNameHead = dayNameHead,
                            dayNameClassHead = daynameClassHead,
                        }); ;
                    }
                    else
                    {
                        string daynameClass = ""; string Holidaydetails = "hidden";
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                            total_holiday_days++;
                        }
                        string attt_type = "";
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                        {
                            attt_type = "Present"; daynameClass = "daypresenT";
                            total_persent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                        {
                            attt_type = "Absent"; daynameClass = "dayabsenT";
                            total_absent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                        {
                            attt_type = "Leave"; daynameClass = "dayleavE";
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
                            Holidaydetails = Holidaydetails,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),

                            //Head
                            daYHead = showDate,
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
                    string showDate = finaldate.ToString("dd-MMM");

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
                        string[] stringSeparatorss = new string[] { "~" };
                        string[] arrs = period_type_in_calender.Split(stringSeparatorss, StringSplitOptions.None);
                        period_type_in_calender = arrs[0];
                        string Holidaydetails = arrs[1];
                        if (Holidaydetails.ToUpper() == "CLASS" || Holidaydetails == "")
                        {
                            Holidaydetails = "hidden";
                        }


                        if (period_type_in_calender == "Class")
                        {
                            AttendanceS_type = "Attendance not taken by teacher.";
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
                            if (dayName == "Sunday")
                            {
                                total_holiday_days++;
                            }
                            else
                            {
                                total_absent_days++;
                            }
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
                            Holidaydetails = Holidaydetails,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),

                            //Head
                            daYHead = showDate,
                            dayNameHead = dayNameHead,
                            dayNameClassHead = daynameClassHead,
                        });
                    }
                    else
                    {
                        string Holidaydetails = "hidden";
                        string daynameClass = "";
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                            total_holiday_days++;
                        }
                        string attt_type = "";
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                        {
                            attt_type = "Present"; daynameClass = "daypresenT";
                            total_persent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                        {
                            attt_type = "Absent"; daynameClass = "dayabsenT";
                            total_absent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                        {
                            attt_type = "Leave"; daynameClass = "dayleavE";
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
                            Holidaydetails = Holidaydetails,
                            Total_no_of_days = total_no_of_days.ToString(),
                            Total_holiday_days = total_holiday_days.ToString(),
                            Total_persent_days = total_persent_days.ToString(),
                            Total_absent_days = total_absent_days.ToString(),
                            Total_leave_days = total_leave_days.ToString(),
                            Total_no_of_days_less_one = total_no_of_days_less_one.ToString(),
                            Total_working_days = total_working_days.ToString(),
                            Attendance_perc = attendance_perc.ToString("0.00"),

                            //Head
                            daYHead = showDate,
                            dayNameHead = dayNameHead,
                            dayNameClassHead = daynameClassHead,
                        });
                    }
                }
            }
            return MyStudentWiseAttendanceSItem;
        }


        private string get_period_type_in_calender(string day, string session_id, string class_id, int cidate)
        {
            string returN = "Class~Class";
            string query = "select Type,Details from School_Holiday_Calendar where Idate='" + cidate + "' and Session_id='" + session_id + "' and Day='" + day + "' and Class_id='" + class_id + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Type"].ToString().ToUpper() == "CLASS")
                {
                    returN = dt.Rows[0]["Type"].ToString() + "~Class";
                }
                else
                {
                    returN = dt.Rows[0]["Type"].ToString() + "~" + dt.Rows[0]["Details"].ToString();
                }
            }

            return returN;
        }
    }
}
