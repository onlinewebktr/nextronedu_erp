using Newtonsoft.Json;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Admin.webServices
{
    /// <summary>
    /// Summary description for finance
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class finance : System.Web.Services.WebService
    {
        public class Fetch_Details_of_student_paid_status
        {
            public string Session_name { get; set; }
            public string Fee_head { get; set; }
            public string Totays_collection { get; set; }
            public string Month_collection { get; set; }
        }

        List<Fetch_Details_of_student_paid_status> Show_of_student_paid_status = new List<Fetch_Details_of_student_paid_status>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_headwise_fee(string FromiDate, string ToiDate)
        {
            string query = "select * from (select *,(select isnull(sum(convert(float, paid)),0) from Monthly_Fee_Collection_Slip where content_id!='1002' and Idate>='" + mycode.idate() + "' and Content=t.Content and session=t.session) as Totay_collection from (select session,Content,isnull(sum(convert(float, paid)),0) as Total_fee from Monthly_Fee_Collection_Slip where content_id!='1002' and Idate>='" + FromiDate + "' and Idate<=" + ToiDate + " group by session,Content)t where Total_fee>0)u  order by Totay_collection desc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //string todaysCollection = get_todays_collection(dr["session"].ToString(), dr["Content"].ToString());
                    Show_of_student_paid_status.Add(new Fetch_Details_of_student_paid_status
                    {
                        Session_name = dr["session"].ToString(),
                        Fee_head = dr["Content"].ToString(),
                        Totays_collection = dr["Totay_collection"].ToString(),
                        Month_collection = dr["Total_fee"].ToString(),
                    });
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_of_student_paid_status));
        }


        My mycode = new My();
        private string get_todays_collection(string session, string content)
        {
            string totalAmt = "0";
            string query = "select session,Content,isnull(sum(convert(float, paid)),0) as Total_fee from Monthly_Fee_Collection_Slip where content_id!='1002' and Idate>='" + mycode.idate() + "' and Content='" + content + "' and session='" + session + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                totalAmt = dt.Rows[0]["Total_fee"].ToString();
            }
            return totalAmt;
        }



        ///==========================================================
        ///
        [WebMethod(EnableSession = true)]
        public string find_months_collections_report_days(string Session_id, string Session_name, string Branch_id, string Monthsdays)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Day");
            dtDatas.Columns.Add("Total Collected");
            List<object> datasets = new List<object>(); object labels = "";


            string TodaydatEtim = mycode.date();
            DateTime SevenstartTime = DateTime.ParseExact(TodaydatEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string FifteenDaysDate = SevenstartTime.AddDays(-14).ToShortDateString();
            int fifteenDayS = My.DateConvertToIdate(FifteenDaysDate);
            string fifteenDaysBackDate = fifteenDayS.ToString();
            int todayIdate = My.DateConvertToIdate(TodaydatEtim);

            int y = 0;
            object chartData = "";
            for (int i = 1; i <= 15; i++)
            {
                DateTime cfromDateTime = Convert.ToDateTime(FifteenDaysDate);
                DateTime cfinaldate = cfromDateTime.AddDays(y);
                string cDate = cfinaldate.ToString("dd/MM/yyyy");
                int ciDate = My.toint(cfinaldate.ToString("yyyyMMdd"));

                string visiablemonth = cfinaldate.ToString("MMM");
                string visiableDateDD = cfinaldate.ToString("dd");
                string visibleDM = visiableDateDD + " " + visiablemonth;


                string idateS = ciDate.ToString();
                DataTable dtD = My.dataTable("select isnull(sum(convert(float, Amount)),0) as Total from Student_Payment_History where Idate>=" + idateS + " and Idate<=" + idateS + "");

                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["Day"] = visibleDM;
                drNewRow["Total Collected"] = dtD.Rows[0]["Total"].ToString();
                dtDatas.Rows.Add(drNewRow);
                dtDatas.AcceptChanges();
                y++;
            }



            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Day")).ToList();
            var colors = new String[] { "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)" };
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



        ///MonthWise
        ///
        [WebMethod(EnableSession = true)]
        public string find_overall_collections_report_monthwise(string Session_id, string Session_name, string Branch_id, string Payment_estd_class_overall)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("Total Collected");
            List<object> datasets = new List<object>(); object labels = "";
            string cunrt_session = My.get_session();
            string session_frst_year = cunrt_session.Substring(0, 4);

            object chartData = "";
            int session_s_year = My.toint(session_frst_year);
            int s_year = My.toint(session_frst_year);
            int s_year_real = s_year;
            string s_months = get_s_months();
            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = s_months.Split(stringSeparatorss, StringSplitOptions.None);
            string s_month = arrs[0];
            string s_month_id = arrs[1];
            string s_month_position = arrs[2];



            DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string MonthName = dtime.ToString("MMMM");
            string MonthNumber = dtime.ToString("MM"); //"04";//
            string month_position = get_month_position(MonthName, MonthNumber);

            if (My.toint(s_month_position) >= My.toint(month_position))
            {
                MonthName = s_month;
                month_position = s_month_position;
            }


            string query = "select * from Month_Index where Position<='" + month_position + "' order by Position asc";
            DataTable dtm = My.dataTable(query);
            if (dtm.Rows.Count > 0)
            {
                foreach (DataRow dr in dtm.Rows)
                {
                    string month_id_in_two_dgt = My.getMonthS_twoDigit(dr["Month_Id"].ToString());
                    int chart_month = My.toint(dr["Month_Id"].ToString());
                    s_year = My.check_start_months(chart_month, s_year_real);
                    int month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                    int month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");
                    DataTable dtfee = new DataTable();
                    if (Payment_estd_class_overall == "0")
                    {
                        dtfee = My.dataTable("select isnull(sum(convert(float, Amount)),0) as Total from Student_Payment_History where Idate>=" + month_s_idate + " and Idate<=" + month_e_idate + "");
                    }
                    else
                    {
                        dtfee = My.dataTable("select isnull(sum(convert(float, Amount)),0) as Total from Student_Payment_History where Idate>=" + month_s_idate + " and Idate<=" + month_e_idate + " and Class_id='" + Payment_estd_class_overall + "'");
                    }
                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["Month"] = dr["Month"].ToString();
                    drNewRow["Total Collected"] = dtfee.Rows[0]["Total"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }



            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Month")).ToList();
            var colors = new String[] { "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)", "rgba(255, 99, 132)", "rgba(54, 162, 235)", "rgba(255, 206, 86)", "rgba(75, 192, 192)" };
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
                last_data = new
                {
                    month = dtDatas.Rows[dtDatas.Rows.Count - 1][0],
                    ttl_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][1],
                }
            };

            return JsonConvert.SerializeObject(chartData);
        }

        private string get_s_months()
        {
            string months = "0/0/0";
            string query = "select Month,Month_Id,Position from Month_Index order by Position asc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                months = dt.Rows[0]["Month"].ToString() + "/" + dt.Rows[0]["Month_Id"].ToString() + "/" + dt.Rows[0]["Position"].ToString();
            }
            return months;
        }
        private string get_month_position(string MonthName, string MonthNumber)
        {
            string Position = "0";
            string query = "select Position from Month_Index where Month_Id='" + MonthNumber + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                Position = dt.Rows[0]["Position"].ToString();
            }
            return Position;
        }



        //=======================================
        [WebMethod(EnableSession = true)]
        public string find_old_new_student_count(string Session, string Class_id, string Section, string Student_type)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Status");
            dtDatas.Columns.Add("Total Student");
            List<object> datasets = new List<object>(); object labels = "";
            object chartData = "";
            string query = "";
            if (Class_id == "0")
            {
                if (Student_type == "ALL")
                {
                    query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New Student' WHEN Transfer_Status = 'NT' THEN 'Old Student' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')  group by Transfer_Status";
                }
                else
                {
                    query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New Student' WHEN Transfer_Status = 'NT' THEN 'Old Student' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Transfer_Status ='" + Student_type + "'  and Status='1'  group by Transfer_Status";
                }
            }
            else
            {
                if (Section == "0")
                {
                    if (Student_type == "ALL")
                    {
                        query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New Student' WHEN Transfer_Status = 'NT' THEN 'Old Student' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')  group by Transfer_Status";
                    }
                    else
                    {
                        query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New Student' WHEN Transfer_Status = 'NT' THEN 'Old Student' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Status='1' and Transfer_Status ='" + Student_type + "'  group by Transfer_Status";
                    }
                }
                else
                {
                    if (Student_type == "ALL")
                    {
                        query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New Student' WHEN Transfer_Status = 'NT' THEN 'Old Student' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')  group by Transfer_Status";
                    }
                    else
                    {
                        query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New Student' WHEN Transfer_Status = 'NT' THEN 'Old Student' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Status='1' and Transfer_Status ='" + Student_type + "' group by Transfer_Status";
                    }
                }
            }

            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["Status"] = dr["Status"].ToString();
                    drNewRow["Total Student"] = dr["total"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }

            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Status")).ToList();
            var colors = new String[] { "#6cff99", "#fff82b", "#3cffdb", "#9966FF", "#FF9F40" };
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
            dtDatas.Columns.Add("Total Amount");
            List<object> datasets = new List<object>(); object labels = "";
            object chartData = "";
            string query = "select mode,isnull(sum(convert(float, Amount)),0) as Total from Student_Payment_History where Idate>="+mycode.idate()+ " and Idate<=" + mycode.idate() + " group by mode"; 
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["mode"] = dr["mode"].ToString();
                    drNewRow["Total Amount"] = dr["Total"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }

            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("mode")).ToList();
            var colors = new String[] { "#6cff99", "#fff82b", "#3cffdb", "#9966FF", "#FF9F40", "#6cff99", "#fff82b", "#3cffdb", "#9966FF", "#FF9F40" };
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
