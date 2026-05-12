using school_web.AppCode;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.WebPages;
namespace school_web
{
    public enum FileType
    {
        Image, Pdf, Document, All
    }
    public class MyTable
    {

        public MyTable(String qry, SqlConnection con = null, SqlTransaction txn = null)
        {
            this.ds = new DataSet();
            if (con == null)
                this.ad = new SqlDataAdapter(qry, PayrollMy.con);
            else
                this.ad = new SqlDataAdapter(qry, con);
            if (txn != null)
            {
                this.ad.SelectCommand.Transaction = txn;
            }
            ad.Fill(ds);
        }
        DataSet ds;
        SqlDataAdapter ad;
        public DataTable dt
        {
            get
            {
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return new DataTable();
            }
        }
        public DataRowCollection Rows { get { return dt.Rows; } }
        public void Update()
        {
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(ds);
        }

        internal DataRow NewRow()
        {
            return dt.NewRow();
        }
    }
    public static class PayrollMy
    {

        public static bool update_one_Row_data_Attendance_Record()
        {

            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, 1);
            //DateTime date = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //DateTime idate = DateTime.ParseExact(startDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
            DataTable dt = My.dataTable(" select  Employee_Name,Employee_id from HR_Employee_Master  where ActiveStatus='Active'   ");//where Employee_id='GCPF-039' 
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Employee_Name = dt.Rows[i]["Employee_Name"].ToString();
                    string Emp_Code = dt.Rows[i]["Employee_id"].ToString();
                    insert_one_row_every_month(Employee_Name, Emp_Code, startDate);
                }
            }
            return true;
        }

        private static void insert_one_row_every_month(string employee_Name, string emp_Code, DateTime startDate)
        {
            ///idates = My.DateConvertToIdate(fromDateTime.ToString("dd/MM/yyyy"));
            string datef = startDate.ToString("dd-MMM-yyyy");
            string idateF = startDate.ToString("yyyyMMdd");
            DataTable dt = My.dataTable("select  * from HR_Daily_Attendance_Record  where Employee_id='" + emp_Code + "' and Idate=" + idateF + "   ");
            if (dt.Rows.Count == 0)
            {
                execute(" insert into HR_Daily_Attendance_Record(Employee_id, Date, Idate,AttendanceSource,AttendanceStatus,In_Time) values('" + emp_Code + "', '" + datef + "', '" + idateF + "','Manual Attendance','In Office','00:00:00 AM')");
                //execute(" insert into HR_Daily_Attendance_Record(Employee_id, Date, Idate,AttendanceSource,AttendanceStatus) values('" + emp_Code + "', '" + datef + "', '" + idateF + "','Manual Attendance','Present')");
            }
            else
            { 
            }
        }



        //fetch data log
        public static bool insert_HR_Attendance_log()
        {
            int day = 0;
            try
            {
                My.exeSql("Alter Table dbo.[PRL_Attendance_Log] Add [import_status] varchar (500)  ;");
                My.exeSql("update PRL_Attendance_Log set import_status='Pending'");
            }
            catch
            {
                My.exeSql("update PRL_Attendance_Log set import_status='Pending' where (import_status='' or import_status is null)");

            }
            DataTable dt = My.dataTable("select  *,format(DateTime, 'yyyyMMddHHmmss') as DateTime1,format(DateTime, 'yyyyMMdd') as idate,format(DateTime, 'dd-MMM-yyyy') as Date,format(DateTime, 'hh:mm:ss tt') as time,format(DateTime, 'dd/MM/yyyy hh:mm:ss tt') as Datetimemain,(Select top 1 Employee_id from  HR_Employee_Master where Emp_Code=PRL_Attendance_Log.Employee_id) as Employee_idnew  from dbo.[PRL_Attendance_Log]  where import_status='Pending' order by DateTime  ");//where Employee_id='GCPF-039' 
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                //var mt2 = PayrollMy.Table("HR_Daily_Attendance_Record");
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    SqlCommand cmd;
                    string query = "INSERT INTO HR_Attendance_Log (Employee_id,DateTime,Itime,Emp_Code,import_status) values (@Employee_id,@DateTime,@Itime,@Emp_Code,@import_status)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Employee_id", dt.Rows[i]["Employee_idnew"].ToString());
                    cmd.Parameters.AddWithValue("@DateTime", dt.Rows[i]["DateTime"]);
                    cmd.Parameters.AddWithValue("@Itime", dt.Rows[i]["DateTime1"].ToString());
                    cmd.Parameters.AddWithValue("@Emp_Code", dt.Rows[i]["Employee_id"].ToString());
                    cmd.Parameters.AddWithValue("@import_status", "Pending");

                    if (My.InsertUpdateData(cmd))
                    {
                        My.exeSql("update PRL_Attendance_Log set import_status='Done' where Id='" + dt.Rows[i]["Id"].ToString() + "'");
                    }
                    //----------------------
                    //var mt = PayrollMy.Table("HR_Attendance_Log", where: $"DateTime>='{PayrollMy.Now.AddMonths(-3).ToString("yyyy-MM-01")} 00:00:01 AM'");
                    //var drs = mt2.dt.Select($"Employee_id='" + dt.Rows[i]["Employee_id"].ToString() + "' and Idate=" + dt.Rows[i]["idate"].ToString() + "");

                    //DateTime startTime = DateTime.ParseExact(dt.Rows[i]["Datetimemain"].ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    //DateTime Date = DateTime.ParseExact(dt.Rows[i]["Datetimemain"].ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    //if (drs.Length == 0)
                    //{
                    //    var dr = mt2.NewRow();
                    //    dr["Employee_id"] = dt.Rows[i]["Employee_id"].ToString();
                    //    dr["Date"] = dt.Rows[i]["Date"].ToString();
                    //    dr["In_Time"] = dt.Rows[i]["time"].ToString();
                    //    dr["AttendanceSource"] = "Old Payroll Attendance";
                    //    dr["Shift_Id"] = 0;
                    //    dr["Idate"] = dt.Rows[i]["idate"].ToString();
                    //    mt2.Rows.Add(dr);
                    //}
                    //else
                    //{
                    //    var dr = drs[0];
                    //    if (dr["In_Time"].ToString() == dt.Rows[i]["time"].ToString())
                    //    {

                    //    }
                    //    else if (dr["Out_Time"].ToString() == "")
                    //    {
                    //        var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                    //        if ((Date - intime).TotalSeconds > 30)
                    //        {
                    //            dr["Out_Time"] = Date.ToString("hh:mm:ss tt");
                    //        }

                    //    }
                    //    else
                    //    {
                    //        var lastpucnch = Convert.ToDateTime($"{dr["Date"]} {dr["Out_Time"]}");
                    //        if (lastpucnch < Date)
                    //        {
                    //            dr["Out_Time"] = Date.ToString("hh:mm:ss tt");
                    //        }
                    //    }
                    //}




                }

                // mt2.Update();

            }
            return true;
        }


        // fetchdata HR_Daily_Attendance_Record


        public static bool update_HR_Daily_Attendance_Record(string Attendance_surce)
        {
            try
            {
                My.exeSql("Alter Table dbo.[HR_Attendance_Log] Add [import_status] varchar (500)  ;");
                My.exeSql("update HR_Attendance_Log set import_status='Pending'");
            }
            catch
            {
                My.exeSql("update HR_Attendance_Log set import_status='Pending' where (import_status='' or import_status is null)");

            }

            try
            {



                var et = PayrollMy.dataTable("select Emp_Code, Employee_id from HR_Employee_Master");
                var emp = new Dictionary<string, string>();
                foreach (DataRow dr in et.Rows)
                {
                    emp[dr["Emp_Code"].ToString()] = dr["Employee_id"].ToString();
                }
                //var mt = PayrollMy.Table("HR_Attendance_Log", where: $"DateTime>='{PayrollMy.Now.AddMonths(-3).ToString("yyyy-MM-01")} 00:00:01 AM'");

                var mt2 = PayrollMy.Table("HR_Daily_Attendance_Record");
                // var mt2 = PayrollMy.Table("HR_Daily_Attendance_Record", where: $"Idate>='{PayrollMy.Now.AddMonths(-3).ToString("yyyyMM01")}'");

                var sdt = PayrollMy.dataTable($"select dr.EmployeeId,sf.*,Start_IDate,End_IDate from HR_Duty_Roster   dr join HR_ShiftSetting sf on dr.Shift_Id=sf.ShiftId and sf.Status=1 join HR_Rotation_Details rd on dr.RotationId =rd.RotationId and rd.Status=1");



                // var sdt = PayrollMy.dataTable($"select dr.EmployeeId,sf.*,Start_IDate,End_IDate from HR_Duty_Roster   dr join HR_ShiftSetting sf on dr.Shift_Id=sf.ShiftId and sf.Status=1 join HR_Rotation_Details rd on dr.RotationId =rd.RotationId and rd.Status=1 and Start_IDate>={PayrollMy.Now.AddMonths(-3).AddDays(-1).ToString("yyyyMM01")}");

                var cs_shift = PayrollMy.dataTable($"select sc.Employee_Id, ss.*,Idate from HR_Date_Wise_Shift_Customized sc join HR_ShiftSetting  ss on sc.Shift_Id=ss.ShiftId ");
                // var cs_shift = PayrollMy.dataTable($"select sc.Employee_Id, ss.*,Idate from HR_Date_Wise_Shift_Customized sc join HR_ShiftSetting  ss on sc.Shift_Id=ss.ShiftId where idate >=   '{PayrollMy.Now.AddMonths(-3).AddDays(-1).ToString("yyyyMM01")}'");

                var latdate = Convert.ToDateTime(PayrollMy.Now.AddMonths(-3).ToString("yyyy-MM-01") + " 00:00:01 AM");
                // var latdate = Convert.ToDateTime(PayrollMy.Now.AddMonths(-3).ToString("yyyy-MM-01") + " 00:00:01 AM");
                var at = PayrollMy.dataTable("select top 1 * from HR_Salary_And_Attendance_Rule");


                var allow_late_come = 30;
                var allow_early_leave = 30;
                if (at.Rows.Count > 0)
                {
                    allow_late_come = at.Rows[0]["Allowed_Late_Come"].ToInt();
                    allow_early_leave = at.Rows[0]["Allowed_Early_Leave"].ToInt();
                }

                DataTable dt = PayrollMy.dataTable(" select  *,format(DateTime, 'dd/MM/yyyy hh:mm:ss tt') as Datetimemain   from dbo.[HR_Attendance_Log] where import_status='Pending' order by DateTime  ");
                if (dt.Rows.Count == 0)
                {

                }
                else
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var date = DateTime.ParseExact(dt.Rows[i]["Datetimemain"].ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);


                        var prev_date = date.AddDays(-1);
                        var attendance_idate = date.ToString("yyyyMMdd");
                        var attendance_date = date;
                        var time = date.ToString("hh:mm:ss tt");
                        var isNightShift = false;
                        var employee_id = dt.Rows[i]["Employee_id"].ToString();
                        var isInTimeAttendance = true;
                        var isShiftAssigned = false;
                        var isCurrentDay = true;
                        var shift_Id = "0";
                        var shift_intime = DateTime.Now;
                        var shift_outtime = DateTime.Now;
                        #region prev day  
                        var sdrs = cs_shift.Select($"Employee_Id='{employee_id}' and Idate = '{prev_date.ToString("yyyyMMdd")}'");
                        if (sdrs.Length == 0)
                        {
                            sdrs = sdt.Select($"EmployeeId='{employee_id}' and Start_IDate  <= {prev_date.ToString("yyyyMMdd")}   and End_IDate>='{prev_date.ToString("yyyyMMdd")}'");
                        }
                        if (sdrs.Length > 0)
                        {
                            var r = sdrs[0];
                            shift_intime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["In_Time"]}");//8:00pm
                            shift_outtime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["Out_Time"]}");//8:00am

                            //if attendance time near previous day's shift outtime  we take 3 hour range employee may leave 3 hour early or may work 3 hour late work due to over time 
                            if (date >= shift_outtime.AddHours(-3) && date <= shift_outtime.AddHours(3))
                            {
                                isCurrentDay = false;
                                attendance_idate = prev_date.ToString("yyyyMMdd");
                                attendance_date = prev_date;
                            }
                        }
                        #endregion
                        if (isCurrentDay)
                        {
                            sdrs = cs_shift.Select($"Employee_Id='{employee_id}' and Idate = '{attendance_idate}'");
                            if (sdrs.Length == 0)
                            {
                                sdrs = sdt.Select($"EmployeeId='{employee_id}' and Start_IDate  <= {attendance_idate}   and End_IDate>='{attendance_idate}'");



                            }

                            if (sdrs.Length > 0)
                            {
                                var r = sdrs[0];
                                isShiftAssigned = true;
                                shift_intime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["In_Time"]}");
                                shift_outtime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["Out_Time"]}");
                                if (shift_outtime < shift_intime)
                                {
                                    isNightShift = true;
                                    shift_outtime = shift_outtime.AddDays(1);
                                }
                                shift_Id = r["ShiftId"].ToString();
                                if (date >= shift_outtime.AddHours(-3) && date <= shift_outtime.AddHours(3))
                                {
                                    isInTimeAttendance = false;
                                }
                            }

                            var drs = mt2.dt.Select($"Employee_id='{employee_id}' and Idate='{attendance_idate}'");

                            var dr = drs.Length == 0 ? mt2.NewRow() : drs[0];
                            if (drs.Length == 0)
                            {

                                dr["Employee_id"] = employee_id;
                                dr["Date"] = attendance_date.ToString("dd-MMM-yyyy");
                                dr["In_Time"] = time;
                                dr["AttendanceSource"] = Attendance_surce;
                                dr["Idate"] = attendance_idate;
                                dr["IsNightShift"] = isNightShift;
                                dr["Shift_Id"] = shift_Id;
                                dr["AttendanceStatus"] = "In Office";
                                if (isShiftAssigned)
                                {
                                    if (!isInTimeAttendance)
                                    {
                                        dr["In_Time"] = DBNull.Value;
                                        dr["Out_Time"] = time;
                                        //var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                        if ((shift_outtime - date).TotalMinutes >= 1)
                                        {
                                            dr["Early_Leave_Minute"] = (int)(shift_outtime - date).TotalMinutes;
                                        }
                                        if ((date - shift_intime).TotalMinutes >= allow_early_leave)
                                        {
                                            dr["IsEarlyLeave"] = 1;
                                        }
                                        // var Total_Wroking_Time = (int)(date - intime).TotalMinutes;
                                        dr["Total_Wroking_Time"] = 0;
                                        dr["AttendanceStatus"] = "Present";
                                    }
                                    else
                                    {
                                        if ((date - shift_intime).TotalMinutes >= 1)
                                        {
                                            dr["Late_Minute"] = (int)(date - shift_intime).TotalMinutes;
                                        }
                                        if ((date - shift_intime).TotalMinutes >= allow_late_come)
                                        {
                                            dr["IsLate"] = 1;
                                        }
                                    }


                                }
                                mt2.Rows.Add(dr);
                            }
                            else if (dr["In_Time"].ToString() == ""|| dr["In_Time"].ToString() == "00:00:00 AM")
                            {

                                dr["Employee_id"] = employee_id;
                                dr["Date"] = attendance_date.ToString("dd-MMM-yyyy");
                                dr["In_Time"] = time;
                                dr["AttendanceSource"] = Attendance_surce;
                                dr["Idate"] = attendance_idate;
                                dr["IsNightShift"] = isNightShift;
                                dr["Shift_Id"] = shift_Id;
                                dr["AttendanceStatus"] = "In Office";
                                if (isShiftAssigned)
                                {
                                    if (!isInTimeAttendance)
                                    {
                                        dr["In_Time"] = DBNull.Value;
                                        dr["Out_Time"] = time;
                                        //var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                        if ((shift_outtime - date).TotalMinutes >= 1)
                                        {
                                            dr["Early_Leave_Minute"] = (int)(shift_outtime - date).TotalMinutes;
                                        }
                                        if ((date - shift_intime).TotalMinutes >= allow_early_leave)
                                        {
                                            dr["IsEarlyLeave"] = 1;
                                        }
                                        // var Total_Wroking_Time = (int)(date - intime).TotalMinutes;
                                        dr["Total_Wroking_Time"] = 0;
                                        dr["AttendanceStatus"] = "Present";
                                    }
                                    else
                                    {
                                        if ((date - shift_intime).TotalMinutes >= 1)
                                        {
                                            dr["Late_Minute"] = (int)(date - shift_intime).TotalMinutes;
                                        }
                                        if ((date - shift_intime).TotalMinutes >= allow_late_come)
                                        {
                                            dr["IsLate"] = 1;
                                        }
                                    }


                                }

                            }

                            else
                            {

                                if (dr["In_Time"].ToString() == time)
                                {
                                    continue;
                                }
                                bool is_out_time_changed = false;
                                if (dr["Out_Time"].ToString() == "")
                                {
                                    var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                    if ((date - intime).TotalSeconds > 50)
                                    {
                                        dr["Out_Time"] = time;
                                        is_out_time_changed = true;
                                    }
                                }
                                else
                                {
                                    var lastpucnch = Convert.ToDateTime($"{dr["Date"]} {dr["Out_Time"]}");
                                    if (lastpucnch < date)
                                    {
                                        dr["Out_Time"] = time;
                                        is_out_time_changed = true;
                                    }
                                }
                                if (isShiftAssigned && is_out_time_changed)
                                {
                                    var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                    if ((shift_outtime - date).TotalMinutes >= 1)
                                    {
                                        dr["Early_Leave_Minute"] = (int)(shift_outtime - date).TotalMinutes;
                                    }
                                    if ((date - shift_intime).TotalMinutes >= allow_early_leave)
                                    {
                                        dr["IsEarlyLeave"] = 1;
                                    }
                                    var Total_Wroking_Time = (int)(date - intime).TotalMinutes;
                                    dr["Total_Wroking_Time"] = Total_Wroking_Time;
                                    dr["AttendanceStatus"] = "Present";
                                }
                                else
                                {
                                    DataTable dt1 = My.dataTable("Select * from HR_ShiftSetting where ShiftId='" + shift_Id + "'");

                                    if (dt1.Rows.Count == 0)
                                    {

                                    }
                                    else
                                    {
                                        var dr1 = dt1.Rows[0];
                                        var intime = Convert.ToDateTime($"{dr["Date"]} {dr1["In_Time"]}");
                                        if ((shift_outtime - date).TotalMinutes >= 1)
                                        {
                                            dr["Early_Leave_Minute"] = (int)(shift_outtime - date).TotalMinutes;
                                        }
                                        if ((date - shift_intime).TotalMinutes >= allow_early_leave)
                                        {
                                            dr["IsEarlyLeave"] = 1;
                                        }
                                        var Total_Wroking_Time = (int)(date - intime).TotalMinutes;
                                        dr["Total_Wroking_Time"] = Total_Wroking_Time;
                                        dr["AttendanceStatus"] = "Present";
                                    }



                                }

                            }

                        }



                        My.exeSql("update HR_Attendance_Log set import_status='Done' where Employee_id ='" + employee_id + "'");
                    }
                    mt2.Update();

                }
            }
            catch (Exception ex)
            {

            }

            return true;
        }







        internal static string get_user_type_HR_Employee_Online_Apply(string Apply_id)
        {

            string query = "Select Apply_for,Hiring_id from HR_Employee_Online_Apply where Apply_id='" + Apply_id + "' ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = PayrollMy.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return get_actual_name(dt.Rows[0]["Apply_for"].ToString(), dt.Rows[0]["Hiring_id"].ToString());
                //return dt.Rows[0]["Apply_for"].ToString();

            }
        }

        private static string get_actual_name(string Apply_for, string Hiring_id)
        {
            string query = "Select HiringName from HR_HiringVacancy where Vacancy_id='" + Hiring_id + "' ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = PayrollMy.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return Apply_for;
            }
            else
            {
                return dt.Rows[0]["HiringName"].ToString();
            }
        }

        public static string currentSession { get { return $"{PayrollMy.Now.AddMonths(-3).Year}-{PayrollMy.Now.AddMonths(-3).AddYears(1).Year}"; } }
        public static bool isHospital { get { return ConfigurationManager.AppSettings["clientType"] == "hospital"; } }
        public static bool isalaryCalculationManual
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("SalaryCalculation"))
                {
                    return ConfigurationManager.AppSettings["SalaryCalculation"] == "manual";
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool isSchool { get { return ConfigurationManager.AppSettings["clientType"] == "hospital"; } }
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }

            return words;
        }
        public static void getUserInfo(dynamic viewbag, HttpSessionStateBase session, string uid)
        {
            if (session["UserName"] == null)
            {
                var dt = PayrollMy.dataTable($"select * from HR_UserProfile where UserId='{uid}'");
                session["UserType"] = dt.Rows[0]["UserType"].ToString();
                session["UserName"] = dt.Rows[0]["Name"].ToString();
                session["UserProfileImage"] = dt.Rows[0]["ProfileImage"].ToString();
                viewbag.UserName = session["UserName"].ToString();
                viewbag.UserType = session["UserType"].ToString();
                viewbag.UserProfileImage = session["UserProfileImage"].ToString();

            }
            else
            {
                viewbag.UserName = session["UserName"].ToString();
                viewbag.UserType = session["UserType"].ToString();
                viewbag.UserProfileImage = session["UserProfileImage"].ToString();
            }
        }

        public static Firm_Details getFirmDetails()
        {
            var data = PayrollMy.dataTable("select top 1 * from Firm_Details").toJsonString();
            var firm = JsonConvert.DeserializeObject<List<Firm_Details>>(data);
            return firm[0];
        }
        internal static MyTable Table(string table_name, String where = "", int limit = 0, SqlConnection con = null, SqlTransaction txn = null)
        {
            var qry = "select {0} * from {1} {2}";
            if (table_name.ToLower().Trim().StartsWith("select "))
            {
                return new MyTable(table_name, con: con, txn: txn);
            }
            else
            {
                return new MyTable(string.Format(qry, limit == 0 ? "" : "Top " + limit, table_name, where == "" ? "" : " where " + where), con: con, txn: txn);
            }

        }
        public static string con { get { return ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["defaultCon"]].ConnectionString; } }



        public static DateTime Now { get { return DateTime.UtcNow.AddHours(5).AddMinutes(30); } }

        public static object date { get { return PayrollMy.Now.ToString("dd-MMM-yyyy"); } }
        public static object time { get { return PayrollMy.Now.ToString("hh:mm tt"); } }

        public static int idate(this DateTime date)
        {
            return Convert.ToInt32(date.ToString("yyyyMMdd"));
        }
        public static int itime(this DateTime date)
        {
            return Convert.ToInt32(date.ToString("HHmmss")); ;
        }
        public static bool isEqual(this String data, string value)
        {
            return data.ToLower() == value.ToLower();
        }
        public static string isValidDate(this String date, string format = "yyyy-MM-dd")
        {
            try
            {
                return Convert.ToDateTime(date).ToString(format);
            }
            catch
            {
                return "";
            }
        }
        public static Dictionary<string, object> ToDictionary(this object values)
        {
            var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            if (values != null)
            {
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(values))
                {
                    object obj = propertyDescriptor.GetValue(values);
                    dict.Add(propertyDescriptor.Name, obj);
                }
            }

            return dict;
        }
        public static MvcHtmlString LabelEditor(
      this HtmlHelper htmlHelper, String name, String type = "text", String labletext = null, String value = "", bool isRequired = false, string ddl_comma_seperated = null, object[] ddl_selectList = null, string ddl_optionlabel = "Select", string divCssClass = "col-md-12", object labelHtmlAttributes = null, object editorHtmlAttributes = null, bool isReadonly = false, bool isNoLabel = false)
        {
            var ob = new Dictionary<string, object>();
            if (editorHtmlAttributes != null)
            {
                ob = editorHtmlAttributes.ToDictionary();
            }
            if (isReadonly)
            {
                ob["readonly"] = true;
            }
            var sup = isRequired ? "<span class='text-danger spn-require'>*</span>" : "";
            var label = htmlHelper.Label(name, labletext ?? name, new { @class = "form-label", @for = name }).ToString();
            var editor = "";
            if (!ob.ContainsKey("class"))
            {
                ob["class"] = "";
            }
            if (type == "ddl")
            {

                ob["class"] = $"{ob["class"]} form-select";
                editor = htmlHelper.DropDownList(name, ddl_comma_seperated == null ? (ddl_selectList == null ? Enumerable.Empty<SelectListItem>() : new SelectList(ddl_selectList)) : new SelectList(ddl_comma_seperated.Split(',')), ddl_optionlabel, ob).ToString();
                if (value != "")
                    editor += "<script> $('#" + name + "').val('" + value + "')</script>";
            }
            else if (type == "multiline")
            {
                ob["class"] = $"{ob["class"]} form-control";
                editor = htmlHelper.TextArea(name, value, ob).ToString();
            }
            else if (type == "file")
            {
                var hidden = htmlHelper.Hidden(name, value).ToString();
                var filter = "accept = 'image/*'";
                bool preview = true;
                var display = "";
                if (value == "")
                {
                    display = "display:none;";
                }

                var file = $"<input type='file' id='file_{name}' hd-field={name} {filter} onchange='fileupload(this)' class='form-control mb-2' />";
                var img = preview ? $"<img src='{value}' class='img-thumbnail' onerror = 'getTypeImage(this)' style='{display} height:70px;' id = 'img_{name}' />" : "";
                editor = $@"{ hidden}
                        { file}
                        { img} ";
            }
            else
            {
                ob["class"] = $"{ob["class"]} form-control";
                ob["type"] = type;
                editor = htmlHelper.TextBox(name, value, ob).ToString();
            }
            if (isNoLabel)
            {
                label = "";
                sup = "";
            }
            var edc = $@"<div class='{divCssClass}'>
                        {label} {sup}
                        { editor} 
                    </div>";
            return MvcHtmlString.Create(edc);
        }
        public static MvcHtmlString BreadCrumb(
        this HtmlHelper htmlHelper, string title)
        {
            var edc = $@"<div class=""page-breadcrumb d-flex align-items-center mb-2 eeewe"">
    <div class=""ps-3 d-none d-sm-flex"">
        <nav aria-label=""breadcrumb"">
            <ol class=""breadcrumb mb-0 p-0"">
                <li class=""breadcrumb-item"">
                    <a href=""javascript:;""><i class=""bx bx-home-alt""></i></a>
                </li>
                <li class=""breadcrumb-item active"" aria-current=""page"">{title}</li>
            </ol>
        </nav>
    </div>
</div>";
            return MvcHtmlString.Create(edc);
        }

        public static MvcHtmlString DropDownList(
       this HtmlHelper htmlHelper, string name, string commaseperated_value, string optionlabel = "Select",
         object htmlAttributesforLabel = null, object htmlAttributesforddl = null)
        {
            var ddl = htmlHelper.DropDownList(name, new SelectList(commaseperated_value.Split(',')), optionlabel, htmlAttributesforLabel).ToString(); ;
            return MvcHtmlString.Create(ddl);
        }
        public static MvcHtmlString DropDownList(
        this HtmlHelper htmlHelper, string name, string optionlabel = "Select",
          object htmlAttributesforLabel = null, object htmlAttributesforddl = null)
        {
            var ddl = htmlHelper.DropDownList(name, new SelectList(new String[0]), optionlabel, htmlAttributesforLabel).ToString(); ;
            return MvcHtmlString.Create(ddl);
        }

        public static MvcHtmlString FileUploadFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression, FileType fileType = FileType.All, bool preview = false, string divCssClass = "col-md-12", object htmlAttributesforLabel = null, string fileCssClass = "form-control mb-2")
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string propertyName = metadata.PropertyName;

            var sup = metadata.IsRequired ? "<span class='text-danger'>*</span>" : "";
            var label = htmlHelper.LabelFor(expression, htmlAttributesforLabel ?? new { @class = "form-label" }).ToString();
            var hidden = htmlHelper.HiddenFor(expression).ToString();
            var filter = "";
            if (fileType == FileType.Pdf)
            {
                filter = "accept = 'application/pdf'";
                preview = false;
            }
            else if (fileType == FileType.Image)
            {
                filter = "accept = 'image/*'";
            }
            else if (fileType == FileType.Document)
            {
                filter = "accept = '.doc,.docx,.xls,.xlsx,application/pdf'";
                preview = false;
            }
            var file = $"<input type='file' id='file_{propertyName}' hd-field={propertyName} {filter} onchange='fileupload(this)' class='{fileCssClass}' />";
            var img = preview ? $"<img src='' class='img-thumbnail' style='display:none; height:70px;' id = 'img_{propertyName}' />" : "";
            var edc = $@"<div class='{divCssClass}'>
                        {label}
                        {sup}
                        { hidden}
                        { file}
                        { img}
                    </div>";
            return MvcHtmlString.Create(edc);
        }
        public static MvcHtmlString FileUpload(
         this HtmlHelper htmlHelper, String expression, FileType fileType = FileType.All, bool isRequired = false, bool preview = false, string divCssClass = "col-md-12", object htmlAttributesforLabel = null, string fileCssClass = "form-control mb-2")
        {
            // ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string propertyName = expression.Replace(" ", "_");

            var sup = isRequired ? "<span class='text-danger'>*</span>" : "";
            var label = htmlHelper.Label(expression, htmlAttributesforLabel ?? new { @class = "form-label" }).ToString();
            var hidden = htmlHelper.Hidden(propertyName).ToString();
            var filter = "";
            if (fileType == FileType.Pdf)
            {
                filter = "accept = 'application/pdf'";
                preview = false;
            }
            else if (fileType == FileType.Image)
            {
                filter = "accept = 'image/*'";
            }
            else if (fileType == FileType.Document)
            {
                filter = "accept = '.doc,.docx,.xls,.xlsx,application/pdf'";
                preview = false;
            }
            var file = $"<input type='file' id='file_{propertyName}' hd-field={propertyName} {filter} onchange='fileupload(this)' class='{fileCssClass}' />";
            var img = preview ? $"<img src='' class='img-thumbnail' style='display:none; height:80px;' id = 'img_{propertyName}' />" : "";
            var edc = $@"<div class='{divCssClass}'>
                        {label}
                        {sup}
                        { hidden}
                        { file}
                        { img}
                    </div>";
            return MvcHtmlString.Create(edc);
        }

        public static Payroll_User user(this HttpRequestBase request)
        {
            HttpCookie authCookie = request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            Payroll_User u = (new JavaScriptSerializer()).Deserialize<Payroll_User>(ticket.UserData);
            return u;
        }
        public static bool equalIgnoreCase(this String str, String value)
        {
            return str.ToLower() == value.ToLower();
        }

        public static MvcHtmlString LabelEditorFor<TModel, TValue>(
         this HtmlHelper<TModel> htmlHelper,
         Expression<Func<TModel, TValue>> expression, string divCssClass = "col-md-12", object htmlAttributesforLabel = null, object htmlAttributesforEditor = null, bool autocomplete = false, string url = "", int minLength = 3)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sup = metadata.IsRequired ? "<span class='text-danger'>*</span>" : "";
            var label = htmlHelper.LabelFor(expression, htmlAttributesforLabel ?? new { @class = "form-label" }).ToString();
            var editor = htmlHelper.EditorFor(expression, new { htmlAttributes = htmlAttributesforEditor ?? new { @class = "form-control" } }).ToString();
            var suggest = "";
            if (autocomplete && url != "")
            {
                suggest = $"<script>$('#Name').suggest('{url}',{minLength});</script>";
            }
            var edc = $@"<div class='{divCssClass}'> 
                        {label}
                        {sup}
                        { editor} 
                        { suggest} 
                    </div>";
            return MvcHtmlString.Create(edc);
        }

        internal static string AutoId(string fieldName, string prefix = "", string format = "0")
        {
            string tableName = "HR_AutoId";
            string colName = "IdName";
            string con = null;
            string result = "";
            SqlDataAdapter ad = new SqlDataAdapter($"select * from {tableName} where  {colName}='{fieldName}'", con ?? PayrollMy.con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr[1] = fieldName;
                dr[2] = 2;
                result = prefix + 1.ToString(format);
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    var data = Convert.ToInt32(dr[2]);
                    result = prefix + data.ToString(format);

                    dr[2] = data + 1;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(ds.Tables[0]);
            return result;
        }
        internal static string getGlobalData(string fieldName)
        {
            string tableName = "GlobalData";
            string GlobalKey = "GlobalKey";
            string GlobalValue = "GlobalValue";
            return PayrollMy.data($"select {GlobalValue} from {tableName} where {GlobalKey}='{fieldName.Replace("'", "''")}'");
        }
        internal static void setGlobalData(string fieldName, object fieldvalue)
        {
            string tableName = "GlobalData";
            string GlobalKey = "GlobalKey";
            string GlobalValue = "GlobalValue";
            var dict = new Dictionary<string, object>();
            dict[GlobalKey] = fieldName;
            dict[GlobalValue] = fieldvalue;
            PayrollMy.InsertOrUpdate(tableName, dict, GlobalKey);
        }
        internal static bool ToBool(this object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {

            }
            return false;
        }
        internal static bool isPatient(this Controller page)
        {
            if (page.Session["UserType"] == null || page.Session["UserType"].ToString() != "Patient")
            {
                return false;
            }
            return true;
        }
        internal static bool isDoctor(this Controller page)
        {
            if (page.Session["UserType"] == null || page.Session["UserType"].ToString() != "Doctor")
            {
                return false;
            }
            return true;
        }
        internal static bool isAdmin(this Controller page)
        {
            if (page.Session["UserType"] == null || page.Session["UserType"].ToString() != "Admin")
            {
                return false;
            }
            return true;
        }
        internal static int ToInt(this object value, int default_value = 0)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {

            }
            return default_value;
        }
        internal static double ToDouble(this object value, double default_value = 0)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {

            }
            return default_value;
        }

        public static MvcHtmlString LabelDropDownListFor<TModel, TValue>(
         this HtmlHelper<TModel> htmlHelper,
         Expression<Func<TModel, TValue>> expression, string optionlabel = "Select", object[] list = null,
          string divCssClass = "col-md-12", object htmlAttributesforLabel = null, object htmlAttributesforddl = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sup = metadata.IsRequired ? "<span class='text-danger'>*</span>" : "";
            var label = htmlHelper.LabelFor(expression, htmlAttributesforLabel ?? new { @class = "form-label" }).ToString();
            //var ddllist = new SelectList(list);
            var ddl = htmlHelper.DropDownListFor(expression, list == null ? Enumerable.Empty<SelectListItem>() : new SelectList(list), optionlabel, htmlAttributesforddl ?? new { @class = "form-control" }).ToString();
            var edc = $@"<div class='{divCssClass}'>
                        {label} {sup}
                        { ddl} 
                    </div>";
            return MvcHtmlString.Create(edc);
        }

        public static MvcHtmlString MasterField(
       this HtmlHelper htmlHelper, MasterModel m, string divCssClass = "col-md-12")
        {
            var p = m.PCD;
            var label = htmlHelper.Label(p.DisplayText, new { @class = "form-label" }).ToString();
            string editor;

            if (p.FieldType == "TextBox")
            {
                editor = htmlHelper.TextBox(m.FieldName, p.FieldData, new { @class = "form-control", type = "text" }).ToString();
            }
            else if (p.FieldType == "MultiLine")
            {
                editor = htmlHelper.TextArea(m.FieldName, p.FieldData, new { @class = "form-control" }).ToString();
            }
            else if (p.FieldType == "CheckBox")
            {
                editor = "<div class='form-switch p-0 d-inline-block'>" + htmlHelper.CheckBox(m.FieldName, p.FieldData.Contains("True"), new { @class = "ms-5 form-check-input" }).ToString() + "</div>";

            }
            else if (p.FieldType == "CheckBox-List")
            {
                var chks = p.FieldData.Split(',');
                var chkList = "";
                foreach (var txt in chks)
                {
                    chkList += $"<label class = 'me-2'> <input value='{txt}'  class='me-1' name='{m.FieldName}'   type='checkbox'>{txt}</label>  ";
                }
                // var hi = htmlHelper.Hidden(m.FieldName).ToString();
                editor = $"<div class='border rounded p-2'>{chkList}</div>";
            }
            else if (p.FieldType == "RadioButtons")
            {
                var chks = p.FieldData.Split(',');
                var chkList = "";
                foreach (var txt in chks)
                {
                    chkList += $"<label class = 'checkbox-inline'> <input    class='m-0' name='{m.FieldName}' value='{txt}'   type='radio'>{txt}</label>  ";
                }

                editor = $"<div class='border rounded p-2' >{chkList}</div>";
            }
            else if (p.FieldType.Contains("File"))
            {
                var hidden = htmlHelper.Hidden(m.FieldName).ToString();
                var filter = "";
                bool preview = true;
                if (p.FieldType == "File-Pdf")
                {
                    filter = "accept = 'application/pdf'";
                    preview = false;
                }
                else if (p.FieldType == "File-Image")
                {
                    filter = "accept = 'image/*'";
                }
                else if (p.FieldType == "File-Document")
                {
                    filter = "accept = '.doc,.docx,.xls,.xlsx,application/pdf'";
                    preview = false;
                }
                var file = $"<input type='file' id='file_{m.FieldName}' hd-field={m.FieldName} {filter} onchange='fileupload(this)' class='form-control mb-2' />";
                var img = preview ? $"<img src='' class='img-thumbnail' style='display:none; height:70px;' id = 'img_{m.FieldName}' />" : "";
                editor = $@"{ hidden}
                        { file}
                        { img} ";
            }
            else if (p.FieldType == "Static DDL")
            {
                editor = htmlHelper.DropDownList(m.FieldName, new SelectList(p.FieldData.Split(',')), "Select", new { @class = "form-select" }).ToString();
            }
            else if (p.FieldType.Contains("DDL with SQL"))
            {
                try
                {
                    var dt = PayrollMy.dataTable(p.FieldData);
                    if (dt.Columns.Count == 1)
                    {
                        var lst = new List<string>();
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            lst.Add(dr[0].ToString());
                        }
                        editor = htmlHelper.DropDownList(m.FieldName, new SelectList(lst), "Select", new { @class = "form-select" }).ToString();
                    }
                    else
                    {
                        var lst = new List<SelectListItem>();
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            lst.Add(new SelectListItem() { Text = dr[0].ToString(), Value = dr[1].ToString() });
                        }
                        editor = htmlHelper.DropDownList(m.FieldName, lst, "Select", new { @class = "form-select" }).ToString();
                    }
                }
                catch
                {
                    editor = htmlHelper.TextBox(m.FieldName, "", new { @class = "form-control" }).ToString();
                }


            }
            else if (p.FieldType == "DatePicker")
            {
                editor = htmlHelper.Editor(m.FieldName, new { htmlAttributes = new { @class = "form-control", type = "date" } }).ToString();
            }
            else if (p.FieldType == "DatePickerIdate")
            {
                label = "";
                p.IsRequired = false;
                editor = htmlHelper.Editor(m.FieldName, new { htmlAttributes = new { @class = "form-control", type = "hidden", date_field = p.FieldData } }).ToString();
            }
            else if (p.FieldType == "TimePicker")
            {
                editor = htmlHelper.Editor(m.FieldName, new { htmlAttributes = new { @class = "form-control", type = "time" } }).ToString();
            }
            else if (p.FieldType == "TimePicker-Itime")
            {
                label = "";
                p.IsRequired = false;
                editor = htmlHelper.Editor(m.FieldName, new { htmlAttributes = new { @class = "form-control", type = "hidden", time_field = p.FieldData } }).ToString();
            }
            else if (p.FieldType == "DateTimePicker")
            {
                editor = htmlHelper.Editor(m.FieldName, new { htmlAttributes = new { @class = "form-control", type = "text" } }).ToString();
            }
            else if (p.FieldType == "Static")
            {
                editor = htmlHelper.TextBox(m.FieldName, p.FieldData, new { @class = "form-control", type = "text" }).ToString();
            }
            else
            {
                editor = htmlHelper.TextBox(m.FieldName, p.FieldData, new { @class = "form-control", type = "text" }).ToString();
            }
            var sup = p.IsRequired ? "<span class='text-danger'>*</span>" : "";
            var edc = $@"<div class='{divCssClass}'> 
                        {label}
                        {sup}
                        { editor}  
                    </div>";
            return MvcHtmlString.Create(edc);
        }

        internal static void saveException(Exception ex, string remarks = "")
        {
            PayrollMy.Insert("HR_ExceptionDetails", new
            {
                Message = ex.Message,
                ExceptionInfo = ex.ToString(),
                Date = PayrollMy.date,
                Time = PayrollMy.time,
                Remarks = remarks,
            });
        }

        public static MvcHtmlString pageEvents(
        this HtmlHelper htmlHelper, List<Events> events)
        {
            if (events == null)
            {
                events = new List<Events>();
            }
            var script = "";
            var script_tags = "";
            foreach (var f in events)
            {
                if (f.eventType == "script")
                {
                    script_tags += "\r\n" + f.eventData;
                }
                else if (f.eventType == "onclick")
                {
                    script += $@" 
                    $('#{f.fieldName}').on('click', function() {{
                        {f.eventData}
                    }});";
                }
                else if (f.eventType == "onchange")
                {
                    script += $@" 
                    $('#{f.fieldName}').on('change', function() {{
                        {f.eventData}
                    }});";
                }
                else if (f.eventType == "rbt-onchange")
                {
                    script += $@" 
                    $('input[name=""{f.fieldName}""').change(function() {{
                        { f.eventData}
                    }});";

                }
            }

            var edc = $@"<script>  
{script}
                    </script>
{script_tags} ";
            return MvcHtmlString.Create(edc);
        }
        public static MvcHtmlString MasterField(
         this HtmlHelper htmlHelper, FieldConfigData p, string divCssClass = "col-md-12")
        {
            if (p.cssclass != null)
            {
                if (p.cssclass.Trim() != "")
                {
                    divCssClass = p.cssclass;
                }
            }
            var onchangedata = $@"$('#{p.columnName}').val(data.data);
                    if(isEdit)
                    {{
                        $('#{p.columnName}').val($('#{p.onChangedField}').attr('data-{p.columnName}'));
                    }}";
            var label = htmlHelper.Label(p.displayText, new { @class = "form-label", @for = p.columnName }).ToString();
            string editor;
            if (p.fieldType == "TextBox")
            {
                editor = htmlHelper.TextBox(p.columnName, p.defaultValue, new { @class = "form-control", type = "text" }).ToString();
            }
            else if (p.fieldType == "MultiLine")
            {
                editor = htmlHelper.TextArea(p.columnName, p.defaultValue, new { @class = "form-control" }).ToString();
            }
            else if (p.fieldType == "CheckBox")
            {
                //editor = "<div class='form-switch p-0 d-inline-block'>" + htmlHelper.CheckBox(p.columnName, p.requiredData.Contains("True"), new { @class = "ms-5 form-check-input" }).ToString() + "</div>";
                editor = "<input type='checkbox'  id='" + p.columnName + "' value='true' />";

                //editor = htmlHelper.CheckBox(p.columnName,true,  new { @class = "form-check" }).ToString();
            }
            else if (p.fieldType == "CheckBox-List")
            {
                var chks = p.requiredData.Split(',');
                var chkList = "";
                foreach (var txt in chks)
                {
                    chkList += $"<label class = 'me-2'> <input value='{txt}'  class='me-1' name='{p.columnName}'   type='checkbox'>{txt}</label>  ";
                }
                // var hi = htmlHelper.Hidden(p.columnName).ToString();
                editor = $"<div class='border rounded p-2'>{chkList}</div>";
            }
            else if (p.fieldType == "RadioButtons")
            {
                var chks = p.requiredData.Split(',');
                var chkList = "";
                foreach (var txt in chks)
                {
                    chkList += $"<label class = 'checkbox-inline'> <input    class='m-0' name='{p.columnName}' value='{txt}'   type='radio'>{txt}</label>  ";
                }

                editor = $"<div class='border rounded p-2' >{chkList}</div>";
            }
            else if (p.fieldType.Contains("File"))
            {
                var hidden = htmlHelper.Hidden(p.columnName).ToString();
                var filter = "";
                bool preview = true;
                if (p.fieldType == "File-Pdf")
                {
                    filter = "accept = 'application/pdf'";
                    preview = false;
                }
                else if (p.fieldType == "File-Image")
                {
                    filter = "accept = 'image/*'";
                }
                else if (p.fieldType == "File-Document")
                {
                    filter = "accept = '.doc,.docx,.xls,.xlsx,application/pdf'";
                    preview = false;
                }
                var file = $"<input type='file' id='file_{p.columnName}' hd-field={p.columnName} {filter} onchange='fileupload(this)' class='form-control mb-2' />";
                var img = preview ? $"<img src='' class='img-thumbnail' onerror = 'getTypeImage(this)'  style='display:none; height:70px;' id = 'img_{p.columnName}' />" : "";
                editor = $@"{ hidden}
                        { file}
                        { img} ";
            }
            else if (p.fieldType == "DDL")
            {

                if (p.onChanged)
                {

                    onchangedata = $@"$('#{p.columnName}').empty();
                    $('#{p.columnName}').append(data.data);
                    if(isEdit)
                    {{
                        $('#{p.columnName}').val($('#{p.onChangedField}').attr('data-{p.columnName}'));
                    }}";
                    var lst = new List<string>();
                    editor = htmlHelper.DropDownList(p.columnName, new SelectList(lst), "Select", new { @class = "form-select" }).ToString();

                }
                else
                {
                    var dc = p.requiredData;
                    var lst = new List<SelectListItem>();
                    if (dc.Trim().StartsWith("sql:"))
                    {
                        try
                        {
                            dc = dc.Trim().Substring(4);
                            var dt = PayrollMy.dataTable(dc);
                            if (dt.Columns.Count == 1)
                            {
                                foreach (System.Data.DataRow dr in dt.Rows)
                                {
                                    lst.Add(new SelectListItem() { Text = dr[0].ToString(), Value = dr[0].ToString() });
                                }
                            }
                            else
                            {

                                foreach (System.Data.DataRow dr in dt.Rows)
                                {
                                    lst.Add(new SelectListItem() { Text = dr[0].ToString(), Value = dr[1].ToString() });
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        string pattern = @"{([^}]*)}";
                        var matches = Regex.Matches(dc, pattern);
                        if (matches.Count > 0)
                        {
                            foreach (Match m in matches)
                            {
                                var sss = m.Groups[1].Value.Split(',');
                                lst.Add(new SelectListItem() { Text = sss[0].Trim(), Value = sss[1].Trim() });
                            }
                        }
                        else
                        {
                            var sss = dc.Split(',');
                            foreach (var ss in sss)
                            {

                                lst.Add(new SelectListItem() { Text = ss, Value = ss });
                            }
                        }
                    }

                    editor = htmlHelper.DropDownList(p.columnName, lst, "Select", new { @class = "form-select" }).ToString();
                }

            }


            else if (p.fieldType == "DatePicker")
            {
                editor = htmlHelper.Editor(p.columnName, new { htmlAttributes = new { @class = "form-control", type = "date" } }).ToString();
            }
            else if (p.fieldType == "DatePickerIdate")
            {
                label = "";
                p.isRequired = false;
                editor = htmlHelper.Editor(p.columnName, new { htmlAttributes = new { @class = "form-control", type = "hidden", date_field = p.requiredData } }).ToString();
            }
            else if (p.fieldType == "TimePicker")
            {
                editor = htmlHelper.Editor(p.columnName, new { htmlAttributes = new { @class = "form-control", type = "time" } }).ToString();
            }
            else if (p.fieldType == "TimePicker-Itime")
            {
                label = "";
                p.isRequired = false;
                editor = htmlHelper.Editor(p.columnName, new { htmlAttributes = new { @class = "form-control", type = "hidden", time_field = p.requiredData } }).ToString();
            }
            else if (p.fieldType == "DateTimePicker")
            {
                editor = htmlHelper.Editor(p.columnName, new { htmlAttributes = new { @class = "form-control", type = "text" } }).ToString();
            }
            else if (p.fieldType == "Static")
            {
                editor = htmlHelper.TextBox(p.columnName, p.defaultValue, new { @class = "form-control", type = "text" }).ToString();
            }
            else
            {
                editor = htmlHelper.TextBox(p.columnName, p.defaultValue, new { @class = "form-control", type = "text" }).ToString();
            }
            var sup = p.isRequired ? "<span class='text-danger'>*</span>" : "";
            var script = "";

            if (p.onChanged)
            {
                script = $@" <script>
        $('#{p.onChangedField}').on('change', function () {{ 
 
if($(this).val())
{{

            var pageId = $('#frm-page-Id').val();
            var data = {{ frm_pageId: pageId, frm_targetId: '{p.columnName}' }};
            var formData = $('#main-form').serializeArray();
            $.map(formData, function (input) {{
                data[input.name] = input.value;
            }}); 
            var data = {{ data: data }} 
            gowithData('../../data/dataonchange', 'POST', JSON.stringify(data), function (data) {{
                console.log(data);
                {onchangedata}
            }})
}}
 
        }});
    </script>";
            }

            var edc = $@"<div class='{divCssClass}'> 
                        {label}
                        {sup}
                        { editor}  
                        { script}  
                    </div>";
            return MvcHtmlString.Create(edc);
        }

        public static MvcHtmlString MasterField(
        this HtmlHelper htmlHelper, FilterItem p, string divCssClass = "input-group")
        {
            divCssClass = "form-group me-2";
            var labelCssClass = "form-group-text";
            var width = "max-width : 180px;";


            var id = "flt-" + p.name.Replace(" ", "_");
            var label = htmlHelper.Label(p.name, new { @class = labelCssClass, @for = id }).ToString();
            string editor;
            var script = "";
            if (p.type == "TextBox")
            {
                editor = htmlHelper.TextBox(id, p.dataquery, new { @class = "form-control", type = "text", style = width }).ToString();
            }
            else if (p.type == "DDL")
            {
                if (p.datatype == "Static")
                {
                    editor = htmlHelper.DropDownList(id, new SelectList(p.dataquery.Split(',')), "Select", new { @class = "form-select", style = width }).ToString();
                }
                else if (p.datatype == "Static Key Value")
                {
                    var lst = new List<SelectListItem>();
                    string pattern = @"{([^}]*)}";
                    var matches = Regex.Matches(p.dataquery, pattern);
                    foreach (Match m in matches)
                    {
                        var sss = m.Groups[1].Value.Split(',');
                        lst.Add(new SelectListItem() { Text = sss[0].Trim(), Value = sss[1].Trim() });
                    }
                    editor = htmlHelper.DropDownList(id, lst, "Select", new { @class = "form-select", style = width }).ToString();
                }
                else if (p.datatype == "On Change")
                {
                    //editor = htmlHelper.DropDownList(p.name, new SelectList(p.dataquery.Split(',')), "Select", new { @class = "form-select" }).ToString();
                    var lst = new List<SelectListItem>();
                    editor = htmlHelper.DropDownList(id, lst, "Select", new { @class = "form-select", style = width }).ToString();
                    script = $@"<script> 
                         $('#flt-{p.onchange.Replace(" ", "_")}').on('change', function() {{
                        if($(this).val())
{{ bind_ddl('flt-{p.onchange.Replace(" ", "_")}', '{id}', '{p.id}');
                        }}
                         
                    }});
                     </script>";
                }

                else if (p.datatype == "Months")
                {
                    //editor = htmlHelper.DropDownList(p.name, new SelectList(p.dataquery.Split(',')), "Select", new { @class = "form-select" }).ToString();
                    var lst = new List<SelectListItem>();
                    for (int i = 1; i <= 12; i++)
                    {
                        lst.Add(new SelectListItem() { Text = Convert.ToDateTime($"2023-{i.ToString("00")}-01").ToString("MMMM"), Value = i.ToString("00") });
                    }
                    editor = htmlHelper.DropDownList(id, lst, "Select", new { @class = "form-select", style = width }).ToString();

                    script = "";
                }

                else if (p.datatype == "Year")
                {
                    var lst = new List<SelectListItem>();
                    for (int i = 2023; i <= PayrollMy.Now.Year; i++)
                    {
                        lst.Add(new SelectListItem() { Text = i.ToString("0000"), Value = i.ToString("0000") });
                    }
                    editor = htmlHelper.DropDownList(id, lst, "Select", new { @class = "form-select", style = width }).ToString();

                    script = "";
                }
                else if (p.datatype == "SQL")
                {
                    try
                    {
                        var dt = PayrollMy.dataTable(p.dataquery);
                        if (dt.Columns.Count == 1)
                        {
                            var lst = new List<string>();
                            foreach (System.Data.DataRow dr in dt.Rows)
                            {
                                lst.Add(dr[0].ToString());
                            }
                            editor = htmlHelper.DropDownList(id, new SelectList(lst), "Select", new { @class = "form-select", style = width }).ToString();
                        }
                        else
                        {
                            var lst = new List<SelectListItem>();
                            foreach (System.Data.DataRow dr in dt.Rows)
                            {
                                lst.Add(new SelectListItem() { Text = dr[0].ToString(), Value = dr[1].ToString() });
                            }
                            editor = htmlHelper.DropDownList(id, lst, "Select", new { @class = "form-select", style = width }).ToString();
                        }
                    }
                    catch
                    {
                        editor = htmlHelper.TextBox(id, "", new { @class = "form-control", style = width }).ToString();
                    }
                }
                else
                {
                    editor = htmlHelper.TextBox(id, "", new { @class = "form-control" }).ToString();
                }
            }
            else if (p.type == "DatePicker")
            {
                editor = htmlHelper.Editor(id, new { htmlAttributes = new { @class = "form-control", type = "date", style = width } }).ToString();
            }
            else
            {
                editor = htmlHelper.TextBox(id, p.dataquery, new { @class = "form-control", type = "text", style = width }).ToString();
            }

            var edc = $@"<div class='{divCssClass}' > 
                        {label} 
                        { editor}  
{script}
                    </div>";
            return MvcHtmlString.Create(edc);
        }

        internal static bool IsDataExist(string table_or_query, string condition = null)
        {
            var qry = table_or_query.ToLower().Contains(" from ") ? table_or_query : $"select * from {table_or_query}";
            if (PayrollMy.dataTable(condition == null ? qry : $"{qry} where {condition} ").Rows.Count > 0)
            {
                return true;
            }
            return false;
        }


        public static List<Dictionary<string, object>> toJsonObject(this DataTable dt)
        {
            var row = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                var child = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.DataType == typeof(DateTime))
                    {
                        if (dr[col] == DBNull.Value)
                        {
                            child.Add(col.ColumnName, "");
                        }
                        else
                        {
                            var date = Convert.ToDateTime(dr[col]);
                            if (date.Hour == 0 && date.Minute == 0)
                            {
                                child.Add(col.ColumnName, date.ToString("dd-MMM-yyyy"));
                            }
                            else
                            {
                                child.Add(col.ColumnName, date.ToString("dd-MMM-yyyy hh:mm tt"));
                            }
                        }
                    }
                    //else if(col.DataType == typeof(double))//08/12/2023
                    //{

                    //    double abc = My.toDouble(dr[col]);
                    //    child.Add(col.ColumnName, abc.ToString("0.00"));
                    //}
                    //else if (col.DataType == typeof(float))
                    //{
                    //    double abc = My.toDouble(dr[col]);
                    //    child.Add(col.ColumnName, abc.ToString("0.00"));

                    //}
                    else
                    {
                        child.Add(col.ColumnName, dr[col]);
                    }

                }
                row.Add(child);
            }
            return row;
        }
        public static string generate_qr_code(string str, int width, int height)
        {
            string resp;
            string code = str;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    resp = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }

            }
            return resp;
        }
        public static string toJson(this DataRow[] drs, DataColumnCollection columns)
        {
            var row = new List<Dictionary<string, object>>();
            foreach (DataRow dr in drs)
            {
                var child = new Dictionary<string, object>();
                foreach (DataColumn col in columns)
                {
                    child.Add(col.ColumnName, dr[col]);
                }
                row.Add(child);
            }
            return (new JavaScriptSerializer()).Serialize(row);
        }
        public static string toJsonString(this DataTable dt, string conStr = null)
        {
            return (new JavaScriptSerializer()).Serialize(dt.toJsonObject());
        }
        public static List<T> ToList<T>(this DataTable dt) where T : new()
        {
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                T obj = new T();

                foreach (DataColumn column in dt.Columns)
                {
                    PropertyInfo propertyInfo = obj.GetType().GetProperty(column.ColumnName);

                    if (propertyInfo != null)
                    {
                        if (row[column] != DBNull.Value)
                        {
                            propertyInfo.SetValue(obj, row[column], null);
                        }
                        else if (column.DataType == typeof(System.String))
                        {
                            propertyInfo.SetValue(obj, "", null);
                        }
                    }
                }

                list.Add(obj);
            }

            return list;
        }

        public static string displayText(string text)
        {
            var res = "";
            bool isPrevUpper = false;
            int upperCount = 0;
            foreach (char c in text)
            {
                if (c == '_')
                {
                    res = res.Trim() + " ";
                    isPrevUpper = false;
                }
                else if (Char.IsUpper(c) && !isPrevUpper)
                {
                    isPrevUpper = true;
                    upperCount = 1;
                    if (res.Trim() == "")
                        res = $"{c.ToString().ToUpper()}";
                    else
                        res = res.Trim() + " " + c;
                }
                else if (Char.IsUpper(c))
                {
                    upperCount++;
                    res = res.Trim() + c;
                }
                else
                {
                    if (upperCount > 1)
                    {
                        isPrevUpper = true;
                        upperCount = 1;
                        res = res.Trim() + " " + c.ToString().ToUpper();
                    }

                    else
                    {

                        if (res.Trim() == "")
                        {
                            isPrevUpper = true;
                            res = $"{c.ToString().ToUpper()}";
                        }
                        else
                        {
                            isPrevUpper = false;
                            res = res.Trim() + c;
                        }
                    }
                }

            }
            return res;
        }


        public static DataTable dataTable(string query, string conStr = null)
        {
            var ds = dataSet(query, conStr);
            return ds.Tables[0];
        }


        public static string data(string query, string conStr = null)
        {
            var dt = dataTable(query, conStr);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            return dt.Rows[0][0].ToString();
        }

        public static DataSet dataSet(string query, string conStr = null)
        {
            var ad = new SqlDataAdapter(query, conStr ?? PayrollMy.con);
            var ds = new DataSet();
            ad.Fill(ds);
            return ds;
        }
        public static void execute(string query, string conStr = null)
        {
            var ad = new SqlDataAdapter(query, conStr ?? PayrollMy.con);
            var ds = new DataSet();
            ad.Fill(ds);
        }

        public static void Update(string tableName, object jsonData, string where = null, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var js = new JavaScriptSerializer();
            var json = js.Serialize(jsonData);
            var table = js.Deserialize<Dictionary<string, object>>(json);
            Update(tableName, table, where, conStr, sqlCon, ignoreColumn);
        }
        public static void Update(string tableName, Dictionary<string, object> data, string where = null, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var cols = ignoreColumn.Split(',');
            foreach (var c in cols)
            {
                if (data.ContainsKey(c))
                {
                    data.Remove(c);
                }
                else if (data.ContainsKey(c.ToUpper()))
                {
                    data.Remove(c.ToUpper());
                }
                else if (data.ContainsKey(c.ToLower()))
                {
                    data.Remove(c.ToLower());
                }
            }
            var condition = where == null ? "" : "where " + where;
            var cmd = new SqlCommand();
            var qryprm = new List<String>();
            foreach (var key in data.Keys)
            {
                qryprm.Add($"{key}=@{key}");
                cmd.Parameters.AddWithValue($"@{key}", data[key] ?? DBNull.Value);
            }
            cmd.CommandText = $"update {tableName} set {string.Join(",", qryprm)} {condition}";
            InsertUpdate.InsertUpdateData(cmd);
        }
        public static void Delete(string tableName, string where = null, string conStr = null, SqlConnection sqlCon = null)
        {
            var condition = where == null ? "" : "where " + where;
            var cmd = new SqlCommand();
            cmd.CommandText = $"delete from {tableName} {condition}";
            //cmd.ExecuteCommand(conStr, sqlCon);
            InsertUpdate.InsertUpdateData(cmd);
        }
        public static bool ExecuteCommand(this SqlCommand cmd, string conStr = null, SqlConnection sqlCon = null)
        {
            var result = false;
            var scon = sqlCon ?? new SqlConnection(conStr ?? My.con);
            var isConOpenHere = false;
            try
            {
                if (scon.State != System.Data.ConnectionState.Open)
                {
                    cmd.Connection = scon;
                    scon.Open();
                    isConOpenHere = true;
                }
                cmd.ExecuteNonQuery();
                result = true;
            }
            finally
            {
                if (isConOpenHere)
                {
                    scon.Close();
                    scon.Dispose();
                }
            }
            return result;
        }
        internal static string get_emp_code_from_emp_Employee_id(string user_id)
        {
            var dt = PayrollMy.dataTable($"select EmployeeCode from HR_UserProfile where UserId  ='" + user_id + "'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();

            }
        }

        public static String sendPush(string title, string body, String userId = "All", object ext = null, String action = "View")
        {

            var data = new
            {
                NotificationType = "General",
                title = title,
                body = body,
                url = "",
                ext = ext,
            };
            List<string> ids = new List<string>();
            if (userId == "Doctor" || userId == "Patient")
            {
                var dt = PayrollMy.dataTable($"select FCM_ID from LoginProfile where UserType='{userId}' and FCM_ID!=''");
                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(dr["FCM_ID"].ToString());
                }
            }
            else if (userId == "All")
            {
                var dt = PayrollMy.dataTable($"select FCM_ID from LoginProfile where FCM_ID!=''");
                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(dr["FCM_ID"].ToString());
                }
            }
            else
            {
                var fcm = PayrollMy.data($"select FCM_ID from LoginProfile where UserId='{userId}'");
                if (fcm != "")
                    ids.Add(fcm);
            }

            return sendDataNotification(ids, data, userId, action);
        }

        public static String sendNotification(string title, string body, string fcm_id, string imageUrl = "", String userId = "All")
        {
            var data = new
            {
                NotificationType = "General",
                title = title,
                body = body,
                url = imageUrl,
            };
            List<string> ids = new List<string>() { fcm_id };
            return sendDataNotification(ids, data, userId);
        }
        public static String sendNotification(string title, string body, List<string> ids, string imageUrl = "", String userId = "All")
        {
            var data = new
            {
                NotificationType = "General",
                title = title,
                body = body,
                url = imageUrl,
            };
            return sendDataNotification(ids, data, userId);

        }

        public static String sendDataNotification(String fcm_id, object data, String userId = "All")
        {
            List<string> ids = new List<string>() { fcm_id };
            return sendDataNotification(ids, data, userId);
        }
        public static String sendDataNotification(List<string> ids, object data, String userId = "All", String action = "View")
        {
            Dictionary<string, object> dc1 = UsesCode.get_pushsenderid();
            string apikey = (String)dc1["SERVER_API_KEY"];
            string senderid = (String)dc1["SENDER_ID"];

            if (ids.Count == 0)
            {
                return "No Receipent";
            }
            var serializer = new JavaScriptSerializer();
            var dict = PayrollMy.Dictionary(data);
            dict["action"] = action.Split(',');
            //var _data = PayrollMy.Dictionary(data);
            var data_notification = new
            {
                registration_ids = ids,
                // action = action.Split(','),
                data = dict,
                android = new
                {
                    priority = "high"
                },
            };
            var json_data = serializer.Serialize(data_notification);

            // string SERVER_API_KEY = "AAAASZuOcRA:APA91bFnFR3zv_gOHbnXtCB28UbYCTuqfZUn17fsewAYTQkf_JDLyogyP3FxEMDYD9bN3mSfdagMZkOcFfdpSdm9ZI-uafNcA9ciwEAtFbG2jKVeBfl5k062wujuxLLiGnO_yZOl8nXg";


            string SERVER_API_KEY = apikey;
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
            Byte[] byteArray = Encoding.UTF8.GetBytes(json_data);
            tRequest.ContentLength = byteArray.Length;
            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();
            tReader.Close();
            dataStream.Close();
            tResponse.Close();

            if (dict.ContainsKey("title"))
            {
                PayrollMy.Insert("NotificationDetails", new
                {
                    UserId = userId,
                    Title = dict["title"],
                    Message = dict["body"],
                    Image_Url = dict.ContainsKey("url") ? dict["url"] : "",
                    Date = PayrollMy.Now,
                    NotificationId = PayrollMy.AutoId("NotificationId"),
                });
            }
            return sResponseFromServer;
        }
        internal static String sendOTPSMS(string otp, string purpose, string mobile)
        {
            //  var msg = string.Format("Your OTP is {0} for {1}. Regards PURNANK TEAM", otp, purpose);
            var msg = string.Format("Your OTP is {0} for {1}. Regards PURNANK TEAM", otp, purpose, "Online Consultancy");

            var SMSResponce = sendSMS(msg, mobile, false);
            PayrollMy.InsertOrUpdate("OTP_Details", new
            {
                MobileNo = mobile,
                OTP = otp,
                Status = 0,
                aditional_data = purpose,
                SMSResponce = SMSResponce,
                Date_Time = PayrollMy.Now,
                Idate = PayrollMy.Now.ToString("yyyyMMdd"),
            }, new string[] { "MobileNo", "Idate", "Status" });
            return SMSResponce;
        }

        internal static String sendSMS(string msg, string mobile, bool isUnicode = false)
        {
            // return "";
            string api_key = "e54206b887e985cfd4ef57aacabeecd";
            string Sender_id = "PRNSFT";
            string type = isUnicode ? "unicode" : "english";
            string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + msg + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobile + "&smsContentType=" + type;
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }
        public static Dictionary<String, object> Dictionary(object jsonData)
        {
            var js = new JavaScriptSerializer();
            var json = js.Serialize(jsonData);
            var table = js.Deserialize<Dictionary<string, object>>(json);
            return table;
        }
        #region asp codes wrapper
        public static void bind(this DropDownList ddl, string qry, string keyCol = null, string valCol = null, string initial = "Select")
        {
            var dt = PayrollMy.dataTable(qry);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(initial, ""));
            if (dt.Columns.Count == 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
                }
            }
            else if (keyCol == null && valCol == null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(dr[0].ToString(), dr[1].ToString()));
                }
            }
            else if (keyCol != null && valCol == null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(dr[keyCol].ToString(), dr[keyCol].ToString()));
                }
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(dr[keyCol].ToString(), dr[valCol].ToString()));
                }
            }

        }
        #endregion

        public static CardForm BeginCard(this HtmlHelper htmlHelper, string divClass = "")
        {
            var formTag = new TagBuilder("div");
            formTag.AddCssClass($"card {divClass}");
            htmlHelper.ViewContext.Writer.Write(formTag.ToString(TagRenderMode.StartTag));
            return new CardForm(htmlHelper);
        }
        public static ModalForm BeginModal(this HtmlHelper htmlHelper, string modelId, string title, string modelType = "modal-lg", string divClass = "")
        {
            StringWriter writer = new StringWriter();
            //  TextWriter writer = new TextWriter();
            var modal = new TagBuilder("div");
            modal.AddCssClass($"modal fade {divClass}");
            modal.Attributes.Add("aria-hidden", "true");
            modal.Attributes.Add("Id", modelId);
            modal.Attributes.Add("data-bs-backdrop", "static");
            modal.Attributes.Add("data-bs-keyboard", "false");
            writer.Write(modal.ToString(TagRenderMode.StartTag));
            var dialog = new TagBuilder("div");
            dialog.AddCssClass($"modal-dialog {modelType}");
            writer.Write(dialog.ToString(TagRenderMode.StartTag));
            var content = new TagBuilder("div");
            content.AddCssClass($"modal-content");
            writer.Write(content.ToString(TagRenderMode.StartTag));


            return new ModalForm(htmlHelper, title, writer);
        }



        public static void Insert(string tableName, object jsonData, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var js = new JavaScriptSerializer();
            var json = js.Serialize(jsonData);
            var table = js.Deserialize<Dictionary<string, object>>(json);
            Insert(tableName, table, conStr, sqlCon, ignoreColumn);
        }
        public static void InsertOrUpdate(string tableName, object jsonData, string[] compareColumns, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var js = new JavaScriptSerializer();
            var json = js.Serialize(jsonData);
            var table = js.Deserialize<Dictionary<string, object>>(json);
            InsertOrUpdate(tableName, table, compareColumns, conStr, sqlCon, ignoreColumn);
        }
        public static void InsertOrUpdate(string tableName, object jsonData, string compareColumns, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var js = new JavaScriptSerializer();
            var json = js.Serialize(jsonData);
            var table = js.Deserialize<Dictionary<string, object>>(json);
            InsertOrUpdate(tableName, table, compareColumns.Split(','), conStr, sqlCon, ignoreColumn);
        }
        internal static void InsertOrUpdate(string tableName, Dictionary<string, object> table, string[] compareColumns, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var cols = ignoreColumn.Split(',');
            foreach (var c in cols)
            {
                if (table.ContainsKey(c))
                {
                    table.Remove(c);
                }
                else if (table.ContainsKey(c.ToUpper()))
                {
                    table.Remove(c.ToUpper());
                }
                else if (table.ContainsKey(c.ToLower()))
                {
                    table.Remove(c.ToLower());
                }
            }
            var source = new List<string>();
            var updateqry = new List<string>();
            var cmd = new SqlCommand();
            foreach (var key in table.Keys)
            {
                source.Add($"@{key} AS {key}");
                updateqry.Add($"target.{key}=source.{key}");
                cmd.Parameters.AddWithValue($"@{key}", table[key] == null ? DBNull.Value : table[key]);
            }

            var on_condition = new List<string>();
            foreach (var key in compareColumns)
            {
                on_condition.Add($"target.{key}=source.{key}");
            }
            var qry = $@"MERGE INTO {tableName} AS target
USING (SELECT {string.Join(",", source)}) AS source
ON ({string.Join(" and ", on_condition)} )
WHEN MATCHED THEN
    UPDATE SET {string.Join(",", updateqry)}
WHEN NOT MATCHED THEN
    INSERT ({string.Join(",", table.Keys)}) VALUES (source.{string.Join(",source.", table.Keys)});";
            cmd.CommandText = qry;
            //cmd.CommandType = CommandType.Text;
            //cmd.ExecuteCommand(conStr, sqlCon);
            InsertUpdate.InsertUpdateData(cmd);
        }
        internal static void Insert(string tableName, Dictionary<string, object> table, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var cols = ignoreColumn.Split(',');
            foreach (var c in cols)
            {
                if (table.ContainsKey(c))
                {
                    table.Remove(c);
                }
                else if (table.ContainsKey(c.ToUpper()))
                {
                    table.Remove(c.ToUpper());
                }
                else if (table.ContainsKey(c.ToLower()))
                {
                    table.Remove(c.ToLower());
                }
            }
            var qry = $"insert into {tableName} ({string.Join(",", table.Keys)}) values (@{string.Join(",@", table.Keys)})";
            var cmd = new SqlCommand(qry);
            foreach (var key in table.Keys)
            {
                cmd.Parameters.AddWithValue($"@{key}", table[key]);
            }
            //cmd.CommandType = CommandType.Text;
            //cmd.ExecuteCommand(conStr, sqlCon);
            InsertUpdate.InsertUpdateData(cmd);
        }

        internal static string get_employee_id_from_employee_code(object empid)
        {
            string query = "Select Employee_id from HR_Employee_Master where Emp_Code='" + empid + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return empid.ToString();
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
    }
    public class ModalForm
    {
        private readonly HtmlHelper _htmlHelper;
        private readonly StringWriter _writer;
        Func<dynamic, HelperResult> bodyTemplate;
        Func<dynamic, HelperResult> headerTemplate;
        Func<dynamic, HelperResult> footerTemplate;
        private bool headerAdded = false;
        private bool footerAdded = false;
        private bool contentAdded = false;
        private readonly string _title;

        public ModalForm(HtmlHelper htmlHelper, string title, StringWriter writer)
        {
            _htmlHelper = htmlHelper;
            _title = title;
            _writer = writer;
        }

        public ModalForm Content(Func<dynamic, HelperResult> template)
        {

            //   var cardBodyTag = new TagBuilder("div");
            //cardBodyTag.AddCssClass("modal-body");
            //cardBodyTag.InnerHtml = template(null).ToHtmlString(); 
            //_htmlHelper.ViewContext.Writer.Write(cardBodyTag.ToString());
            bodyTemplate = template;
            contentAdded = true;
            return this;
        }

        public ModalForm Header(Func<dynamic, HelperResult> template)
        {
            headerTemplate = template;
            headerAdded = true;
            return this;
        }
        public ModalForm Footer(Func<dynamic, HelperResult> template)
        {

            //var cardFooterTag = new TagBuilder("div");
            //cardFooterTag.AddCssClass("modal-footer p-0  d-block");
            //cardFooterTag.InnerHtml = template(null).ToHtmlString();

            //_htmlHelper.ViewContext.Writer.Write(cardFooterTag.ToString());
            footerTemplate = template;
            footerAdded = true;
            return this;
        }

        public MvcHtmlString Create()
        {



            if (!headerAdded)
            {
                _writer.Write($@"<div class=""modal-header"">
                    <h5 class=""modal-title"">{_title}</h5>
                    <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                </div>");
            }
            else
            {
                _writer.Write($@"<div class=""modal-header"">
                     {headerTemplate(null).ToHtmlString()} 
                    <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                </div>");
            }
            var cardBodyTag = new TagBuilder("div");
            cardBodyTag.AddCssClass("modal-body");
            if (bodyTemplate != null)
                cardBodyTag.InnerHtml = bodyTemplate(null).ToHtmlString();
            _writer.Write(cardBodyTag.ToString());

            if (footerAdded)
            {
                var cardFooterTag = new TagBuilder("div");
                cardFooterTag.AddCssClass("modal-footer d-block");
                cardFooterTag.InnerHtml = footerTemplate(null).ToHtmlString();
                _writer.Write(cardFooterTag.ToString());
            }
            else
            {
                _writer.Write($@"<div class=""modal-footer""> 
                    <button type=""button"" class=""btn btn-secondary  btn-lg"" data-bs-dismiss=""modal""  >Close</button>
                </div>");
            }
            var clossing = new TagBuilder("div");
            _writer.Write(clossing.ToString(TagRenderMode.EndTag));
            _writer.Write(clossing.ToString(TagRenderMode.EndTag));
            _writer.Write(clossing.ToString(TagRenderMode.EndTag));
            return MvcHtmlString.Create(_writer.ToString());
        }
    }
    public class CardForm
    {
        private readonly HtmlHelper _htmlHelper;

        public CardForm(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public CardForm Header(Func<dynamic, HelperResult> template)
        {
            var cardHeaderTag = new TagBuilder("div");
            cardHeaderTag.AddCssClass("card-header");
            cardHeaderTag.InnerHtml = template(null).ToHtmlString();

            _htmlHelper.ViewContext.Writer.Write(cardHeaderTag.ToString());

            return this;
        }

        public CardForm Body(Func<dynamic, HelperResult> template)
        {
            var cardBodyTag = new TagBuilder("div");
            cardBodyTag.AddCssClass("card-body");
            cardBodyTag.InnerHtml = template(null).ToHtmlString();

            _htmlHelper.ViewContext.Writer.Write(cardBodyTag.ToString());

            return this;
        }

        public CardForm Footer(Func<dynamic, HelperResult> template)
        {
            var cardFooterTag = new TagBuilder("div");
            cardFooterTag.AddCssClass("card-footer");
            cardFooterTag.InnerHtml = template(null).ToHtmlString();

            _htmlHelper.ViewContext.Writer.Write(cardFooterTag.ToString());

            return this;
        }

        public MvcHtmlString Create()
        {
            return MvcHtmlString.Create(_htmlHelper.ViewContext.Writer.ToString());
        }
    }
    public class Payroll_User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
    }
    public class MasterModel
    {
        public String FieldName { get; set; }
        public bool IsField { get; set; }
        public PageConfigData PCD { get; set; }
    }
    public class DDLItem
    {
        public String ItemName { get; set; }
        public String ItemId { get; set; }
        public DDLItem(string ItemName, string ItemId)
        {
            this.ItemName = ItemName;
            this.ItemId = ItemId;
        }
        public DDLItem()
        {
            this.ItemName = "";
            this.ItemId = "0";
        }
    }
    public class PageConfigData
    {
        public String DisplayText { get; set; }
        public String FormType { get; set; }
        public String FieldType { get; set; }
        public String FieldData { get; set; }
        public bool AllowOtherData { get; set; }
        public bool IsRequired { get; set; }
        public bool DisplayGrid { get; set; }
    }
    public class MasterViewModel
    {
        public List<MasterModel> List { get; set; }
        public string Data { get; set; }
    }








}