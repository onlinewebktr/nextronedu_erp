using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Admin.webServices
{
    /// <summary>
    /// Summary description for fee_report1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class fee_report1 : System.Web.Services.WebService
    {
        #region MonthlY
        public class Fetch_Details_of_day_end_report_head
        {
            public string Content { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head> Show_of_day_end_report_head = new List<Fetch_Details_of_day_end_report_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
            }
            else
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_day_end_report_head.Add(new Fetch_Details_of_day_end_report_head
                    {
                        Content = dr["Content"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head));
            }
        }



        My mycode = new My();
        public class Fetch_Details_of_day_end_report_head_amts
        {
            public string Payment_date { get; set; }
            public string Slip_no { get; set; }
            public string Months { get; set; }
            public string Student_name { get; set; }
            public string Class_name { get; set; }
            public string Sections { get; set; }
            public string Admission_no { get; set; }
            public string Roll_no { get; set; }
            public string Payment_mode { get; set; }
            public string Ttl_amts { get; set; }
            public string PaidFesAmt { get; set; }
            public string RestFesAmt { get; set; }


            //============-----------==============
            public List<MyFeeReport> MyFeeReportItem { get; set; }

        }
        public class MyFeeReport
        {
            public string HeadFees { get; set; }
        }


        List<Fetch_Details_of_day_end_report_head_amts> Show_of_day_end_report_head_amts = new List<Fetch_Details_of_day_end_report_head_amts>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_amts(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select * from (select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno) as Payment_id,t1.Date,t1.Idate from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelMonthlyFee')) t order by Payment_id asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select * from (select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno) as Payment_id,t1.Date,t1.Idate from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelMonthlyFee')) t order by Payment_id asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select * from (select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno) as Payment_id,t1.Date,t1.Idate from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and (t1.parameter='" + Type + "' or t1.parameter='HostelMonthlyFee')) t order by Payment_id asc";
            }
            else
            {
                query = "select * from (select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno) as Payment_id,t1.Date,t1.Idate from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and (t1.parameter='" + Type + "' or t1.parameter='HostelMonthlyFee')) t order by Payment_id asc";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeReport> MBdetails = findmyRoutineDt(dr["adno"].ToString(), dr["slipno"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);

                    string months = get_months_of_invoice(dr["slipno"].ToString());
                    string ttl_amts = get_ttl_amt_of_a_std(dr["adno"].ToString(), dr["slipno"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);


                    double paidFesAmt = 0;
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = ttl_amts.Split(stringSeparatorss, StringSplitOptions.None);
                    ttl_amts = arrs[0];
                    paidFesAmt = My.toDouble(arrs[1]);
                    double restAmt = (My.toDouble(ttl_amts) - paidFesAmt);

                    Show_of_day_end_report_head_amts.Add(new Fetch_Details_of_day_end_report_head_amts
                    {
                        Payment_date = dr["Date"].ToString(),
                        Slip_no = dr["slipno"].ToString(),
                        Months = months,
                        Student_name = dr["studentname"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Sections = dr["Section"].ToString(),
                        Admission_no = dr["adno"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),
                        Payment_mode = dr["Payment_mode"].ToString(),

                        Ttl_amts = My.toDouble(ttl_amts).ToString("0.00"),
                        PaidFesAmt = paidFesAmt.ToString("0.00"),
                        RestFesAmt = restAmt.ToString("0.00"),
                        //==========================

                        MyFeeReportItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts));
            }
        }

        private string get_ttl_amt_of_a_std(string Admission_no, string Slip_no, string session_id, string class_id, string fromDate, string toDate, string type, string session_name, string class_name)
        {
            string querys = "";
            querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + Admission_no + "' and slipno='" + Slip_no + "'";

            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0/0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Payble_amt"].ToString() + "/" + dt.Rows[0]["Paid_amt"].ToString();
            }
            return paids_amt;
        }

        private List<MyFeeReport> findmyRoutineDt(string Admission_no, string Slip_no, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            List<MyFeeReport> MyFeeReportItem = new List<MyFeeReport>();
            string querys = "";
            if (Session_id == "0" && Class_id == "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')   group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')   group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where  session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')   group by Content,content_id order by Content desc";
            }
            else
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where  session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')   group by Content,content_id order by Content desc";
            }

            SqlDataAdapter ad = new SqlDataAdapter(querys, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow drr in dt.Rows)
                {
                    string fees_amt = get_fees_amt(drr["content_id"].ToString(), Admission_no, "0", Type, Slip_no);
                    double paidFesAmt = 0;
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = fees_amt.Split(stringSeparatorss, StringSplitOptions.None);
                    fees_amt = arrs[0];
                    paidFesAmt = My.toDouble(arrs[1]);

                    MyFeeReportItem.Add(new MyFeeReport
                    {
                        HeadFees = fees_amt,
                    });
                }
            }
            return MyFeeReportItem;
        }


        private string get_months_of_invoice(string slipno)
        {
            string month_name = "";
            DataTable dt = My.dataTable("select DISTINCT t1.Month,t2.Position from dbo.[Monthly_Fee_Collection_Slip] t1 join Month_Index t2 on t1.Month=t2.Month where slipno='" + slipno + "' order by t2.Position asc");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    month_name = month_name + dr["Month"].ToString() + ",";
                }
            }
            month_name = month_name.Remove(month_name.Length - 1);
            return month_name;
        }


        private string get_fees_amt(string content_id, string admission_no, string Dates, string Type, string slip_no)
        {
            string querys = "";
            if (Type == "MonthlyFee")
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + admission_no + "' and content_id='" + content_id + "' and (parameter='" + Type + "'  or parameter='HostelMonthlyFee') and slipno='" + slip_no + "'";
            }
            else
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + admission_no + "' and content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelAnnualFee')  and slipno='" + slip_no + "'";
            }
            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0/0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Payble_amt"].ToString() + "/" + dt.Rows[0]["Paid_amt"].ToString();
            }
            return paids_amt;
        }




        public class Fetch_Details_of_day_end_report_head_amts_final
        {

            public string TotaLFeeS { get; set; }
            public string TotaLFinaLFeeS { get; set; }


            public string TotaLFinaLPaidFeeS { get; set; }
            public string TotaLFinaLRestFeeS { get; set; }

            public List<MyFeeReportOverAll> MyFeeReportOverAllItem { get; set; }
        }
        public class MyFeeReportOverAll
        {
            public string HeadFees { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head_amts_final> Show_of_day_end_report_head_amts_final = new List<Fetch_Details_of_day_end_report_head_amts_final>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_final_amts(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee')";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeReportOverAll> MBdetails = FindMyFeeReportOverAllDt(Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);

                    string ttl_amts = get_ttl_amt_of_overall(Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                    double paidFesAmt = 0;
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = ttl_amts.Split(stringSeparatorss, StringSplitOptions.None);
                    ttl_amts = arrs[0];
                    paidFesAmt = My.toDouble(arrs[1]);
                    double restAmt = (My.toDouble(ttl_amts) - paidFesAmt);

                    Show_of_day_end_report_head_amts_final.Add(new Fetch_Details_of_day_end_report_head_amts_final
                    {
                        TotaLFinaLFeeS = My.toDouble(ttl_amts).ToString("0.00"),
                        TotaLFinaLPaidFeeS = paidFesAmt.ToString("0.00"),
                        TotaLFinaLRestFeeS = restAmt.ToString("0.00"),
                        MyFeeReportOverAllItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts_final));
            }
        }

        private string get_ttl_amt_of_overall(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string querys = "";
            if (Session_id == "0" && Class_id == "0")
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')";
            }
            else
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')";
            }

            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0/0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Payble_amt"].ToString() + "/" + dt.Rows[0]["Paid_amt"].ToString();
            }
            return paids_amt;
        }

        private List<MyFeeReportOverAll> FindMyFeeReportOverAllDt(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            List<MyFeeReportOverAll> MyFeeReportOverAllItem = new List<MyFeeReportOverAll>();
            string querys = "";
            if (Session_id == "0" && Class_id == "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')   group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')   group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where  session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')   group by Content,content_id order by Content desc";
            }
            else
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where  session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')   group by Content,content_id order by Content desc";
            }

            SqlDataAdapter ad = new SqlDataAdapter(querys, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow drr in dt.Rows)
                {
                    string fees_amt = get_final_fees_amt_contentwise(drr["content_id"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                    MyFeeReportOverAllItem.Add(new MyFeeReportOverAll
                    {
                        HeadFees = fees_amt,
                    });
                }
            }
            return MyFeeReportOverAllItem;
        }



        private string get_final_fees_amt_contentwise(string content_id, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelMonthlyFee') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelMonthlyFee') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelMonthlyFee') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelMonthlyFee') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }

            string paids_amt = "0";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0";
            }
            else
            {
                paids_amt = dt.Rows[0]["payable_amt"].ToString();
            }
            return paids_amt;
        }

        public class Fetch_Details_of_day_end_mode
        {
            public string Paid_amt { get; set; }
            public string Pay_mode { get; set; }
        }

        List<Fetch_Details_of_day_end_mode> Show_of_day_end_mode = new List<Fetch_Details_of_day_end_mode>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_ttl_by_mode(string Dates, string Type)
        {
            string query = "";
            if (Type == "MonthlyFee")
            {
                query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate='" + My.DateConvertToIdate(Dates) + "' and  (t1.parameter='" + Type + "'  or t1.parameter='HostelMonthlyFee') group by t2.mode";
            }
            else
            {
                if (Type == "AnnualFee")
                {
                    query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate='" + My.DateConvertToIdate(Dates) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelAnnualFee') group by t2.mode";
                }
                else
                {
                    query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate='" + My.DateConvertToIdate(Dates) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelAdmissionFee') group by t2.mode";
                }
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_day_end_mode.Add(new Fetch_Details_of_day_end_mode
                    {
                        Paid_amt = dr["Paid_amt"].ToString(),
                        Pay_mode = dr["mode"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_mode));
            }
        }

        #endregion


        ///=================================================================
        ///
        public class Fetch_Details_of_day_end_report_head_admission
        {
            public string Content { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head_admission> Show_of_day_end_report_head_admission = new List<Fetch_Details_of_day_end_report_head_admission>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_admission(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelAdmissionFee') group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelAdmissionFee') group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelAdmissionFee') group by Content,content_id order by Content desc";
            }
            else
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelAdmissionFee') group by Content,content_id order by Content desc";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_day_end_report_head_admission.Add(new Fetch_Details_of_day_end_report_head_admission
                    {
                        Content = dr["Content"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_admission));
            }
        }


         
        public class Fetch_Details_of_day_end_report_head_amts_admission
        {
            public string Payment_date { get; set; }
            public string Slip_no { get; set; } 
            public string Student_name { get; set; }
            public string Class_name { get; set; }
            public string Sections { get; set; }
            public string Admission_no { get; set; }
            public string Roll_no { get; set; }
            public string Payment_mode { get; set; }
            public string Ttl_amts { get; set; }
            public string PaidFesAmt { get; set; }
            public string RestFesAmt { get; set; }


            //============-----------==============
            public List<MyFeeReport_admission> MyFeeReportItem_admission { get; set; }

        }
        public class MyFeeReport_admission
        {
            public string HeadFees { get; set; }
        }


        List<Fetch_Details_of_day_end_report_head_amts_admission> Show_of_day_end_report_head_amts_admission = new List<Fetch_Details_of_day_end_report_head_amts_admission>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_amts_admission(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,t1.Date,t1.Idate from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelAdmissionFee') order by t1.Idate,rollnumber asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,t1.Date,t1.Idate from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelAdmissionFee') order by t1.Idate,rollnumber asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,t1.Date,t1.Idate from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and (t1.parameter='" + Type + "' or t1.parameter='HostelAdmissionFee') order by t1.Idate,rollnumber asc";
            }
            else
            {
                query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,t1.Date,t1.Idate from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and (t1.parameter='" + Type + "' or t1.parameter='HostelAdmissionFee') order by t1.Idate,rollnumber asc";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeReport_admission> MBdetails = findmyRoutineDt_admission(dr["adno"].ToString(), dr["slipno"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                     
                    string ttl_amts = get_ttl_amt_of_a_std_admission(dr["adno"].ToString(), dr["slipno"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);


                    double paidFesAmt = 0;
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = ttl_amts.Split(stringSeparatorss, StringSplitOptions.None);
                    ttl_amts = arrs[0];
                    paidFesAmt = My.toDouble(arrs[1]);
                    double restAmt = (My.toDouble(ttl_amts) - paidFesAmt);

                    Show_of_day_end_report_head_amts_admission.Add(new Fetch_Details_of_day_end_report_head_amts_admission
                    {
                        Payment_date = dr["Date"].ToString(),
                        Slip_no = dr["slipno"].ToString(), 
                        Student_name = dr["studentname"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Sections = dr["Section"].ToString(),
                        Admission_no = dr["adno"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),
                        Payment_mode = dr["Payment_mode"].ToString(),

                        Ttl_amts = My.toDouble(ttl_amts).ToString("0.00"),
                        PaidFesAmt = paidFesAmt.ToString("0.00"),
                        RestFesAmt = restAmt.ToString("0.00"),
                        //==========================

                        MyFeeReportItem_admission = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts_admission));
            }
        }

       

        private string get_ttl_amt_of_a_std_admission(string Admission_no, string Slip_no, string session_id, string class_id, string fromDate, string toDate, string type, string session_name, string class_name)
        {
            string querys = "";
            querys = "select (isnull(sum(convert(float, payable)),0)-(isnull(sum(convert(float, previously_paid)),0)+isnull(sum(convert(float, disc_amt)),0))) as Payble_amt,(isnull(sum(convert(float, paid)),0)-isnull(sum(convert(float, previously_paid)),0)) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + Admission_no + "' and slipno='" + Slip_no + "'";

            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0/0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Payble_amt"].ToString() + "/" + dt.Rows[0]["Paid_amt"].ToString();
            }
            return paids_amt;
        }

        private List<MyFeeReport_admission> findmyRoutineDt_admission(string Admission_no, string Slip_no, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            List<MyFeeReport_admission> MyFeeReportItem_admission = new List<MyFeeReport_admission>();
            string querys = "";
            if (Session_id == "0" && Class_id == "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')   group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')   group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where  session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')   group by Content,content_id order by Content desc";
            }
            else
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where  session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')   group by Content,content_id order by Content desc";
            }

            SqlDataAdapter ad = new SqlDataAdapter(querys, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow drr in dt.Rows)
                {
                    string fees_amt = get_fees_amt_admission(drr["content_id"].ToString(), Admission_no, "0", Type, Slip_no);
                    double paidFesAmt = 0;
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = fees_amt.Split(stringSeparatorss, StringSplitOptions.None);
                    fees_amt = arrs[0];
                    paidFesAmt = My.toDouble(arrs[1]);

                    MyFeeReportItem_admission.Add(new MyFeeReport_admission
                    {
                        HeadFees = fees_amt,
                    });
                }
            }
            return MyFeeReportItem_admission;
        }


        private string get_months_of_invoice_admission(string slipno)
        {
            string month_name = "";
            DataTable dt = My.dataTable("select DISTINCT t1.Month,t2.Position from dbo.[Monthly_Fee_Collection_Slip] t1 join Month_Index t2 on t1.Month=t2.Month where slipno='" + slipno + "' order by t2.Position asc");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    month_name = month_name + dr["Month"].ToString() + ",";
                }
            }
            month_name = month_name.Remove(month_name.Length - 1);
            return month_name;
        }


        private string get_fees_amt_admission(string content_id, string admission_no, string Dates, string Type, string slip_no)
        {
            string querys = "select (isnull(sum(convert(float, payable)),0)-(isnull(sum(convert(float, previously_paid)),0)+isnull(sum(convert(float, disc_amt)),0))) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + admission_no + "' and content_id='" + content_id + "' and (parameter='" + Type + "'  or parameter='HostelAdmissionFee') and slipno='" + slip_no + "'";
            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0/0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Payble_amt"].ToString() + "/" + dt.Rows[0]["Paid_amt"].ToString();
            }
            return paids_amt;
        }




        public class Fetch_Details_of_day_end_report_head_amts_final_admission
        {

            public string TotaLFeeS { get; set; }
            public string TotaLFinaLFeeS { get; set; }


            public string TotaLFinaLPaidFeeS { get; set; }
            public string TotaLFinaLRestFeeS { get; set; }

            public List<MyFeeReportOverAll_admission> MyFeeReportOverAllItem_admission { get; set; }
        }
        public class MyFeeReportOverAll_admission
        {
            public string HeadFees { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head_amts_final_admission> Show_of_day_end_report_head_amts_final_admission = new List<Fetch_Details_of_day_end_report_head_amts_final_admission>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_final_amts_admission(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelAdmissionFee')";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeReportOverAll_admission> MBdetails = FindMyFeeReportOverAllDt_admission(Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);

                    string ttl_amts = get_ttl_amt_of_overall_admission(Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                    double paidFesAmt = 0;
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = ttl_amts.Split(stringSeparatorss, StringSplitOptions.None);
                    ttl_amts = arrs[0];
                    paidFesAmt = My.toDouble(arrs[1]);
                    double restAmt = (My.toDouble(ttl_amts) - paidFesAmt);

                    Show_of_day_end_report_head_amts_final_admission.Add(new Fetch_Details_of_day_end_report_head_amts_final_admission
                    {
                        TotaLFinaLFeeS = My.toDouble(ttl_amts).ToString("0.00"),
                        TotaLFinaLPaidFeeS = paidFesAmt.ToString("0.00"),
                        TotaLFinaLRestFeeS = restAmt.ToString("0.00"),
                        MyFeeReportOverAllItem_admission = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts_final_admission));
            }
        }

        private string get_ttl_amt_of_overall_admission(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string querys = "";
            if (Session_id == "0" && Class_id == "0")
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-(isnull(sum(convert(float, previously_paid)),0)+isnull(sum(convert(float, disc_amt)),0))) as Payble_amt,(isnull(sum(convert(float, paid)),0)-isnull(sum(convert(float, previously_paid)),0)) as Paid_amt from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-(isnull(sum(convert(float, previously_paid)),0)+isnull(sum(convert(float, disc_amt)),0))) as Payble_amt,(isnull(sum(convert(float, paid)),0)-isnull(sum(convert(float, previously_paid)),0)) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-(isnull(sum(convert(float, previously_paid)),0)+isnull(sum(convert(float, disc_amt)),0))) as Payble_amt,(isnull(sum(convert(float, paid)),0)-isnull(sum(convert(float, previously_paid)),0)) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')";
            }
            else
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-(isnull(sum(convert(float, previously_paid)),0)+isnull(sum(convert(float, disc_amt)),0))) as Payble_amt,(isnull(sum(convert(float, paid)),0)-isnull(sum(convert(float, previously_paid)),0)) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')";
            }

            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0/0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Payble_amt"].ToString() + "/" + dt.Rows[0]["Paid_amt"].ToString();
            }
            return paids_amt;
        }

        private List<MyFeeReportOverAll_admission> FindMyFeeReportOverAllDt_admission(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            List<MyFeeReportOverAll_admission> MyFeeReportOverAllItem_admission = new List<MyFeeReportOverAll_admission>();
            string querys = "";
            if (Session_id == "0" && Class_id == "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')   group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')   group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where  session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')   group by Content,content_id order by Content desc";
            }
            else
            {
                querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where  session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "'  or parameter='HostelAdmissionFee')   group by Content,content_id order by Content desc";
            }

            SqlDataAdapter ad = new SqlDataAdapter(querys, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow drr in dt.Rows)
                {
                    string fees_amt = get_final_fees_amt_contentwise_admission(drr["content_id"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                    MyFeeReportOverAllItem_admission.Add(new MyFeeReportOverAll_admission
                    {
                        HeadFees = fees_amt,
                    });
                }
            }
            return MyFeeReportOverAllItem_admission;
        }



        private string get_final_fees_amt_contentwise_admission(string content_id, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select (isnull(sum(convert(float, payable)),0)-(isnull(sum(convert(float, previously_paid)),0)+isnull(sum(convert(float, disc_amt)),0))) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelAdmissionFee') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select (isnull(sum(convert(float, payable)),0)-(isnull(sum(convert(float, previously_paid)),0)+isnull(sum(convert(float, disc_amt)),0))) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelAdmissionFee') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select (isnull(sum(convert(float, payable)),0)-(isnull(sum(convert(float, previously_paid)),0)+isnull(sum(convert(float, disc_amt)),0))) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelAdmissionFee') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                query = "select (isnull(sum(convert(float, payable)),0)-(isnull(sum(convert(float, previously_paid)),0)+isnull(sum(convert(float, disc_amt)),0))) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelAdmissionFee') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }

            string paids_amt = "0";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0";
            }
            else
            {
                paids_amt = dt.Rows[0]["payable_amt"].ToString();
            }
            return paids_amt;
        }

        
    }
}
