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

namespace school_web._adminETutorProf.principle_profile
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
        My mycode = new My();
        //======================================= AcademiC
        [WebMethod(EnableSession = true)]
        public string find_academics(string Session)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("mode");
            dtDatas.Columns.Add("Total");
            List<object> datasets = new List<object>(); object labels = "";
            object chartData = "";
            string idate = mycode.idate();
            DataTable dt = My.dataTable("select 'Enquiry' as Name,isnull(count(Id),0) as Number from Enquiry_Details where Format(convert(DateTime,Created_date,103), 'yyyyMMdd')='" + idate + "' union all select 'Prospectus Sale' as Name,isnull(count(Id),0) as Number from Form_sale_details where Created_idate='" + idate + "' union all select 'Admission' as Name,isnull(count(Id),0) as Number from admission_registor where Created_idate='" + idate + "' and Transfer_Status='New'  and Status=1 union all select 'TC' as Name,isnull(count(Id),0) as Number from Transfer_certificate where Create_idate='" + idate + "' union all select 'Inactive' as Name,isnull(count(Id),0) as Number from admission_registor where Inactive_idate='" + idate + "' and Is_TC_Taken=0 and Status=0 union all select 'Receipt' as Name,isnull(count(Id),0) as Number from Student_Payment_History where Idate='" + idate + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["mode"] = dr["Name"].ToString();
                    drNewRow["Total"] = dr["Number"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }

            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("mode")).ToList();
            var colors = new String[] { "#36d1dc", "#ffd200", "#43cea2", "#ff416c", "#ff6cfa", "#b661ff", "#fff82b", "#3cffdb", "#9966FF", "#FF9F40" };
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

        //======================================= Modewise Amount
        [WebMethod(EnableSession = true)]
        public string find_modewise_amount(string Session)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("mode");
            dtDatas.Columns.Add("Total");
            List<object> datasets = new List<object>(); object labels = "";
            object chartData = "";
            string query = "select 'Prospectus Fee' as Name,isnull(sum(cast(Amount as float) ),0) as Number from Form_sale_details where Created_idate='" + mycode.idate() + "' union all  select 'School Fee' as Name,isnull(sum(cast(Amount as float) ),0) as Number from Student_Payment_History where Idate='" + mycode.idate() + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["mode"] = dr["Name"].ToString();
                    drNewRow["Total"] = dr["Number"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }

            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("mode")).ToList();
            var colors = new String[] { "#ffd200", "#49e3b2", "#3cffdb", "#9966FF", "#FF9F40", "#ff416c", "#fff82b", "#3cffdb", "#9966FF", "#FF9F40" };
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


        //===========================================
        [WebMethod(EnableSession = true)]
        public string find_academics_session(string Session, string Session_name)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("mode");
            dtDatas.Columns.Add("Total");
            List<object> datasets = new List<object>(); object labels = "";
            object chartData = "";

            DataTable dt = My.dataTable("select 'Enquiry' as Name,isnull(count(Id),0) as Number from Enquiry_Details where Format(convert(DateTime,Created_date,103), 'yyyyMMdd')>'20250101' union all select 'Prospectus Sale' as Name,isnull(count(Id),0) as Number from Form_sale_details where Session_id='" + Session + "' union all select 'Admission' as Name,isnull(count(Id),0) as Number from admission_registor where Session_id='" + Session + "' and Transfer_Status='New'  and Status=1 union all select 'TC' as Name,isnull(count(Id),0) as Number from Transfer_certificate where Session_id='" + Session + "' union all select 'Inactive' as Name,isnull(count(Id),0) as Number from admission_registor where Session_id='" + Session + "' and Is_TC_Taken=0 and Status=0 union all select 'Receipt' as Name,isnull(count(Id),0) as Number from Student_Payment_History where Session='" + Session_name + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["mode"] = dr["Name"].ToString();
                    drNewRow["Total"] = dr["Number"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }

            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("mode")).ToList();
            var colors = new String[] { "#36d1dc", "#ffd200", "#43cea2", "#ff416c", "#ff6cfa", "#b661ff", "#fff82b", "#3cffdb", "#9966FF", "#FF9F40" };
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


        //======================================= Modewise Amount
        [WebMethod(EnableSession = true)]
        public string find_modewise_amount_session(string Session, string Session_name)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("mode");
            dtDatas.Columns.Add("Total");
            List<object> datasets = new List<object>(); object labels = "";
            object chartData = "";
            string query = "select 'Prospectus Fee' as Name,isnull(sum(cast(Amount as float) ),0) as Number from Form_sale_details where Session_id='" + Session + "' union all  select 'School Fee' as Name,isnull(sum(cast(Amount as float) ),0) as Number from Student_Payment_History where Session='" + Session_name + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["mode"] = dr["Name"].ToString();
                    drNewRow["Total"] = dr["Number"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }

            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("mode")).ToList();
            var colors = new String[] { "#ffd200", "#49e3b2", "#3cffdb", "#9966FF", "#FF9F40", "#ff416c", "#fff82b", "#3cffdb", "#9966FF", "#FF9F40" };
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



        ///===============================
        ///
        public class Fetch_Details_of_attendance
        {
            public string Studentname { get; set; }
            public string Admission_no { get; set; }
            public string Roll_number { get; set; }
            public string Attendance_Status { get; set; }
            public string Status { get; set; }
            public string Status_teg { get; set; }
            public string Status_bdr { get; set; }
            public string Class_name { get; set; }
            public string Sections { get; set; }
        }

        List<Fetch_Details_of_attendance> Show_of_report_attendance = new List<Fetch_Details_of_attendance>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_attendance_status_of_std(string Session_id, string Class_id, string Section, string Idate, string StatusS)
        {
            string query = "";
            if (Section == "OverALL")
            {
                if (StatusS == "NotTaken")
                {
                    query = "select admissionserialnumber as Admission_no,studentname,t2.Course_Name,Section,rollnumber,'Not Taken' as Attendance_Status,'_not_taken' as status,'tag_not_taken' as status_teg,'bdr_not_taken' as status_bdr from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + Session_id + "' and Class_id in (" + Class_id + ")  and Status='1' and admissionserialnumber not in (select Admission_no from Student_Attendance_saved_Class_Wise where Idate='" + Idate + "' and Session_id='" + Session_id + "') order by t2.Position,t1.Section, t1.rollnumber";
                }
                else if (StatusS == "TotalALL")
                {
                    query = "SELECT admissionserialnumber as Admission_no,studentname,t2.Course_Name,Section,rollnumber,ISNULL((SELECT top 1 Attendance_Status FROM Student_Attendance_saved_Class_Wise WHERE Idate='" + Idate + "' AND Session_id=t1.Session_id AND Class_id=t1.Class_id AND Admission_no=t1.admissionserialnumber),'Not Taken') AS Attendance_Status, CASE ISNULL((SELECT top 1 Attendance_Status FROM Student_Attendance_saved_Class_Wise WHERE Idate='" + Idate + "' AND Session_id=t1.Session_id AND Class_id=t1.Class_id AND Admission_no=t1.admissionserialnumber),'Not Taken') WHEN 'Present' THEN '_present' WHEN 'Absent' THEN '_absent' WHEN 'Leave' THEN '_leave' WHEN 'Not Taken' THEN '_not_taken' ELSE '' END AS status , CASE ISNULL((SELECT top 1 Attendance_Status FROM Student_Attendance_saved_Class_Wise WHERE Idate='" + Idate + "' AND Session_id=t1.Session_id AND Class_id=t1.Class_id AND Admission_no=t1.admissionserialnumber),'Not Taken') WHEN 'Present' THEN 'tag_present' WHEN 'Absent' THEN 'tag_absent' WHEN 'Leave' THEN 'tag_leave' WHEN 'Not Taken' THEN 'tag_not_taken' ELSE '' END AS status_teg , CASE ISNULL((SELECT top 1 Attendance_Status FROM Student_Attendance_saved_Class_Wise WHERE Idate='" + Idate + "' AND Session_id=t1.Session_id AND Class_id=t1.Class_id AND Admission_no=t1.admissionserialnumber),'Not Taken') WHEN 'Present' THEN 'bdr_present' WHEN 'Absent' THEN 'bdr_absent' WHEN 'Leave' THEN 'bdr_leave' WHEN 'Not Taken' THEN 'bdr_not_taken' ELSE '' END AS status_bdr FROM admission_registor t1 JOIN Add_course_table t2 ON t1.Class_id=t2.course_id WHERE Session_id='" + Session_id + "' AND Class_id IN (" + Class_id + ") AND Status='1' ORDER BY t2.Position,t1.Section,t1.rollnumber;";
                }
                else
                {
                    query = "SELECT t3.Course_Name, t2.Section,t2.studentname, t1.Admission_no, t2.rollnumber, t1.Attendance_Status, CASE WHEN t1.Attendance_Status = 'Present' THEN '_present' WHEN t1.Attendance_Status = 'Leave'   THEN '_leave' WHEN t1.Attendance_Status = 'Absent'  THEN '_absent' ELSE '_unknown'   END AS status, CASE WHEN t1.Attendance_Status = 'Present' THEN 'tag_present' WHEN t1.Attendance_Status = 'Leave'   THEN 'tag_leave' WHEN t1.Attendance_Status = 'Absent'  THEN 'tag_absent' ELSE 'tag_unknown' END AS status_teg, CASE WHEN t1.Attendance_Status = 'Present' THEN 'bdr_present' WHEN t1.Attendance_Status = 'Leave'   THEN 'bdr_leave' WHEN t1.Attendance_Status = 'Absent'  THEN 'bdr_absent' ELSE 'tag_unknown' END AS status_bdr FROM Student_Attendance_saved_Class_Wise t1 JOIN admission_registor t2  ON t1.Session_id = t2.Session_id  AND t1.Class_id = t2.Class_id  AND t1.Admission_no = t2.admissionserialnumber join Add_course_table t3 on t2.Class_id=t3.course_id WHERE t1.Session_id = '" + Session_id + "' AND t1.Class_id in (" + Class_id + ") AND t1.Attendance_IDate = '" + Idate + "' and t1.Attendance_Status='" + StatusS + "' order by t3.Position, t2.Section,t2.rollnumber asc";
                }
            }
            else
            {
                query = "SELECT 'hidden' as Course_Name,'hidden' as Section,t2.studentname, t1.Admission_no, t2.rollnumber, t1.Attendance_Status, CASE WHEN t1.Attendance_Status = 'Present' THEN '_present' WHEN t1.Attendance_Status = 'Leave'   THEN '_leave' WHEN t1.Attendance_Status = 'Absent'  THEN '_absent' ELSE '_unknown'   END AS status, CASE WHEN t1.Attendance_Status = 'Present' THEN 'tag_present' WHEN t1.Attendance_Status = 'Leave'   THEN 'tag_leave' WHEN t1.Attendance_Status = 'Absent'  THEN 'tag_absent' ELSE 'tag_unknown' END AS status_teg, CASE WHEN t1.Attendance_Status = 'Present' THEN 'bdr_present' WHEN t1.Attendance_Status = 'Leave'   THEN 'bdr_leave' WHEN t1.Attendance_Status = 'Absent'  THEN 'bdr_absent' ELSE 'tag_unknown' END AS status_bdr FROM Student_Attendance_saved_Class_Wise t1 JOIN admission_registor t2  ON t1.Session_id = t2.Session_id  AND t1.Class_id = t2.Class_id  AND t1.Admission_no = t2.admissionserialnumber WHERE  t1.Session_id = '" + Session_id + "'  AND t1.Class_id = '" + Class_id + "'  AND t1.Section = '" + Section + "'  AND t1.Attendance_IDate = '" + Idate + "' and t1.Attendance_Status='" + StatusS + "' order by t2.rollnumber asc";
                if (StatusS == "TotalS")
                {
                    query = "SELECT 'hidden' as Course_Name,'hidden' as Section,t2.studentname, t1.Admission_no, t2.rollnumber, t1.Attendance_Status, CASE WHEN t1.Attendance_Status = 'Present' THEN '_present' WHEN t1.Attendance_Status = 'Leave'   THEN '_leave' WHEN t1.Attendance_Status = 'Absent'  THEN '_absent' ELSE '_unknown'   END AS status, CASE WHEN t1.Attendance_Status = 'Present' THEN 'tag_present' WHEN t1.Attendance_Status = 'Leave'   THEN 'tag_leave' WHEN t1.Attendance_Status = 'Absent'  THEN 'tag_absent' ELSE 'tag_unknown' END AS status_teg, CASE WHEN t1.Attendance_Status = 'Present' THEN 'bdr_present' WHEN t1.Attendance_Status = 'Leave'   THEN 'bdr_leave' WHEN t1.Attendance_Status = 'Absent'  THEN 'bdr_absent' ELSE 'tag_unknown' END AS status_bdr FROM Student_Attendance_saved_Class_Wise t1 JOIN admission_registor t2  ON t1.Session_id = t2.Session_id  AND t1.Class_id = t2.Class_id  AND t1.Admission_no = t2.admissionserialnumber WHERE  t1.Session_id = '" + Session_id + "'  AND t1.Class_id = '" + Class_id + "'  AND t1.Section = '" + Section + "'  AND t1.Attendance_IDate = '" + Idate + "' order by t2.rollnumber asc";
                }
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_report_attendance.Add(new Fetch_Details_of_attendance
                    {
                        Studentname = dr["studentname"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),
                        Roll_number = dr["rollnumber"].ToString(),
                        Attendance_Status = dr["Attendance_Status"].ToString(),
                        Status = dr["status"].ToString(),
                        Status_teg = dr["status_teg"].ToString(),
                        Status_bdr = dr["status_bdr"].ToString(),

                        Class_name = dr["Course_Name"].ToString(),
                        Sections = dr["Section"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_attendance));
            }
        }


        //================================HOSTEL
        public class Fetch_Details_of_attendance_hostel
        {
            public string Studentname { get; set; }
            public string Admission_no { get; set; }
            public string Roll_number { get; set; }
            public string Hostel_Roll_number { get; set; }
            public string Attendance_Status { get; set; }
            public string Status { get; set; }
            public string Status_teg { get; set; }
            public string Status_bdr { get; set; }
            public string Class_name { get; set; }
            public string Sections { get; set; }
        }

        List<Fetch_Details_of_attendance_hostel> Show_of_report_attendance_hostel = new List<Fetch_Details_of_attendance_hostel>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_attendance_status_of_std_hostel(string Session_id, string Idate, string StatusS)
        {
            string query = "";
            if (StatusS == "NotTaken")
            {
                query = "select admissionserialnumber as Admission_no,studentname,t2.Course_Name,Section,rollnumber,t1.Hostel_roll_no,'Not Taken' as Attendance_Status,'_not_taken' as status,'tag_not_taken' as status_teg,'bdr_not_taken' as status_bdr from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + Session_id + "' and Status='1' and hosteltaken='Yes' and admissionserialnumber not in (select Admission_no from Student_attendance_saved_hostel_Wise where Idate='" + Idate + "' and Session_id='" + Session_id + "') order by t1.Hostel_roll_no asc";
            }
            else if (StatusS == "TotalALL")
            {
                query = "SELECT admissionserialnumber as Admission_no,studentname,t2.Course_Name,Section,rollnumber,t1.Hostel_roll_no,ISNULL((SELECT top 1 Attendance_Status FROM Student_attendance_saved_hostel_Wise WHERE Idate='" + Idate + "' AND Session_id=t1.Session_id AND Class_id=t1.Class_id AND Admission_no=t1.admissionserialnumber),'Not Taken') AS Attendance_Status, CASE ISNULL((SELECT top 1 Attendance_Status FROM Student_attendance_saved_hostel_Wise WHERE Idate='" + Idate + "' AND Session_id=t1.Session_id AND Class_id=t1.Class_id AND Admission_no=t1.admissionserialnumber),'Not Taken') WHEN 'Present' THEN '_present' WHEN 'Absent' THEN '_absent' WHEN 'Leave' THEN '_leave' WHEN 'Not Taken' THEN '_not_taken' ELSE '' END AS status , CASE ISNULL((SELECT top 1 Attendance_Status FROM Student_attendance_saved_hostel_Wise WHERE Idate='" + Idate + "' AND Session_id=t1.Session_id AND Class_id=t1.Class_id AND Admission_no=t1.admissionserialnumber),'Not Taken') WHEN 'Present' THEN 'tag_present' WHEN 'Absent' THEN 'tag_absent' WHEN 'Leave' THEN 'tag_leave' WHEN 'Not Taken' THEN 'tag_not_taken' ELSE '' END AS status_teg , CASE ISNULL((SELECT top 1 Attendance_Status FROM Student_attendance_saved_hostel_Wise WHERE Idate='" + Idate + "' AND Session_id=t1.Session_id AND Class_id=t1.Class_id AND Admission_no=t1.admissionserialnumber),'Not Taken') WHEN 'Present' THEN 'bdr_present' WHEN 'Absent' THEN 'bdr_absent' WHEN 'Leave' THEN 'bdr_leave' WHEN 'Not Taken' THEN 'bdr_not_taken' ELSE '' END AS status_bdr FROM admission_registor t1 JOIN Add_course_table t2 ON t1.Class_id=t2.course_id WHERE Session_id='" + Session_id + "' AND Status='1' and hosteltaken='Yes' ORDER BY t1.Hostel_roll_no";
            }
            else
            {
                query = "SELECT t3.Course_Name, t2.Section,t2.studentname, t1.Admission_no, t2.rollnumber,t2.Hostel_roll_no, t1.Attendance_Status, CASE WHEN t1.Attendance_Status = 'Present' THEN '_present' WHEN t1.Attendance_Status = 'Leave'   THEN '_leave' WHEN t1.Attendance_Status = 'Absent'  THEN '_absent' ELSE '_unknown'   END AS status, CASE WHEN t1.Attendance_Status = 'Present' THEN 'tag_present' WHEN t1.Attendance_Status = 'Leave'   THEN 'tag_leave' WHEN t1.Attendance_Status = 'Absent'  THEN 'tag_absent' ELSE 'tag_unknown' END AS status_teg, CASE WHEN t1.Attendance_Status = 'Present' THEN 'bdr_present' WHEN t1.Attendance_Status = 'Leave'   THEN 'bdr_leave' WHEN t1.Attendance_Status = 'Absent'  THEN 'bdr_absent' ELSE 'tag_unknown' END AS status_bdr FROM Student_attendance_saved_hostel_Wise t1 JOIN admission_registor t2  ON t1.Session_id = t2.Session_id  AND t1.Class_id = t2.Class_id  AND t1.Admission_no = t2.admissionserialnumber join Add_course_table t3 on t2.Class_id=t3.course_id WHERE t1.Session_id = '" + Session_id + "'  AND t1.Attendance_IDate = '" + Idate + "' and t1.Attendance_Status='" + StatusS + "' and t2.hosteltaken='Yes' order by t2.Hostel_roll_no asc";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_report_attendance_hostel.Add(new Fetch_Details_of_attendance_hostel
                    {
                        Studentname = dr["studentname"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),
                        Roll_number = dr["rollnumber"].ToString(),
                        Hostel_Roll_number = dr["Hostel_roll_no"].ToString(),
                        Attendance_Status = dr["Attendance_Status"].ToString(),
                        Status = dr["status"].ToString(),
                        Status_teg = dr["status_teg"].ToString(),
                        Status_bdr = dr["status_bdr"].ToString(), 
                        Class_name = dr["Course_Name"].ToString(),
                        Sections = dr["Section"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_attendance_hostel));
            }
        }
    }
}
