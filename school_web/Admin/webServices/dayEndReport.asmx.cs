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
    /// Summary description for dayEndReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class dayEndReport : System.Web.Services.WebService
    {
        #region MonthlY
        public class Fetch_Details_of_day_end_report_head
        {
            public string Content { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head> Show_of_day_end_report_head = new List<Fetch_Details_of_day_end_report_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end(string Sessionid, string Classid, string FromDate, string ToDate, string Type, string Sessionname)
        {
            string Session_id = Sessionid;
            string Class_id = Classid;
            string Class_name = "0";
            string Session_name = Sessionname;



            string class_name = ""; string isClassChecked = "0"; 
            DataTable dtc = My.dataTable("select * from Add_course_table where course_id in (" + Class_id + ")");
            if (dtc.Rows.Count > 0)
            {
                foreach (DataRow dr in dtc.Rows)
                {
                    isClassChecked = "1";
                    class_name = class_name + "'" + dr["Course_Name"].ToString() + "'" + ","; 
                }
                if (isClassChecked == "1")
                {
                    Class_name = class_name.Remove(class_name.Length - 1);
                }
            }



            string query = "";
            if (Session_id == "0")
            {
                query = "select distinct Content  from dbo.[Monthly_Fee_Collection_Slip] where (class in (" + Class_id + ") or class in (" + Class_name + ")) and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'  group by Content,content_id order by Content desc";
            }
            else
            {
                query = "select distinct Content from dbo.[Monthly_Fee_Collection_Slip] where session='" + Session_name + "' and (class in (" + Class_id + ") or class in (" + Class_name + ")) and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' group by Content,content_id order by Content desc";
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
                    string headName = dr["Content"].ToString();
                    if (headName == "TransportationFee")
                    {
                        headName = "Transport";
                    }
                    Show_of_day_end_report_head.Add(new Fetch_Details_of_day_end_report_head
                    {
                        Content = headName,
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
            public string PaidFesAmt { get; set; }
            public string IssuedBy { get; set; }


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
        public void fetch_report_heading_day_end_amts(string Sessionid, string Classid, string FromDate, string ToDate, string Type, string Sessionname)
        {
            string Session_id = Sessionid; 
            string Class_id = Classid; 
            string Session_name = Sessionname;
            string Class_name = "0";
            string query = "";
            if (Session_id == "0")
            {
                query = "select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno) as By_user_id,t1.Date,t1.Idate from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where (t1.class in (" + Class_id + ") or t1.class in (" + Class_name + ")) and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by Idate,rollnumber asc";
            }
            else
            {
                query = "select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno) as By_user_id,t1.Date,t1.Idate from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and (t1.class in (" + Class_id + ") or t1.class in (" + Class_name + ")) and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by Idate,rollnumber asc";
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
                string class_name = ""; string isClassChecked = "0";
                DataTable dtc = My.dataTable("select * from Add_course_table where course_id in (" + Class_id + ")");
                if (dtc.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtc.Rows)
                    {
                        isClassChecked = "1";
                        class_name = class_name + "'" + dr["Course_Name"].ToString() + "'" + ",";
                    }
                    if (isClassChecked == "1")
                    {
                        Class_name = class_name.Remove(class_name.Length - 1);
                    }
                }


                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeReport> MBdetails = findmyRoutineDt(dr["adno"].ToString(), dr["slipno"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                    string months = get_months_of_invoice(dr["slipno"].ToString());
                    string paids_amt = get_ttl_amt_of_a_std(dr["adno"].ToString(), dr["slipno"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);


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
                        PaidFesAmt = paids_amt,
                        IssuedBy = dr["By_user_name"].ToString(),
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
                paids_amt = dt.Rows[0]["Paid_amt"].ToString();
            }
            return paids_amt;
        }

        private List<MyFeeReport> findmyRoutineDt(string Admission_no, string Slip_no, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            List<MyFeeReport> MyFeeReportItem = new List<MyFeeReport>();
            string querys = "";
            if (Session_id == "0")
            {
                querys = "select distinct Content from dbo.[Monthly_Fee_Collection_Slip] where (class in (" + Class_id + ") or class  in (" + Class_name + ")) and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' order by Content desc";
            }
            else
            {
                querys = "select distinct Content from dbo.[Monthly_Fee_Collection_Slip] where  session='" + Session_name + "' and (class in (" + Class_id + ") or class  in (" + Class_name + ")) and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' order by Content desc";
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
                    string paid_amt = get_fees_amt(drr["Content"].ToString(), Admission_no, "0", Type, Slip_no);
                    MyFeeReportItem.Add(new MyFeeReport
                    {
                        HeadFees = paid_amt,
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
                    month_name = month_name + dr["Month"].ToString() + ", ";
                }
            }
            month_name = month_name.Remove(month_name.Length - 2);
            return month_name;
        }


        private string get_fees_amt(string content, string admission_no, string Dates, string Type, string slip_no)
        {
            string querys = "";
            querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + admission_no + "' and Content='" + content + "' and slipno='" + slip_no + "'";
            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0/0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Paid_amt"].ToString();
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
        public void fetch_report_heading_day_end_final_amts(string Sessionid, string Classid, string FromDate, string ToDate, string Type, string Sessionname)
        {
            string Session_id = Sessionid;
            string Class_id = Classid;
            string Session_name = Sessionname;
            string Class_name = "0";

            string query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
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
                string class_name = ""; string isClassChecked = "0";
                DataTable dtc = My.dataTable("select * from Add_course_table where course_id in (" + Class_id + ")");
                if (dtc.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtc.Rows)
                    {
                        isClassChecked = "1";
                        class_name = class_name + "'" + dr["Course_Name"].ToString() + "'" + ",";
                    }
                    if (isClassChecked == "1")
                    {
                        Class_name = class_name.Remove(class_name.Length - 1);
                    }
                }


                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeReportOverAll> MBdetails = FindMyFeeReportOverAllDt(Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);

                    string paidFesAmt = get_ttl_amt_of_overall(Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);


                    Show_of_day_end_report_head_amts_final.Add(new Fetch_Details_of_day_end_report_head_amts_final
                    {
                        TotaLFinaLPaidFeeS = paidFesAmt,
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
            if (Session_id == "0")
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class in (" + Class_id + ") or class  in (" + Class_name + ")) and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class in (" + Class_id + ") or class  in (" + Class_name + ")) and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }

            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Paid_amt"].ToString();
            }
            return paids_amt;
        }

        private List<MyFeeReportOverAll> FindMyFeeReportOverAllDt(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            List<MyFeeReportOverAll> MyFeeReportOverAllItem = new List<MyFeeReportOverAll>();
            string querys = "";
            if (Session_id == "0")
            {
                querys = "select distinct Content from dbo.[Monthly_Fee_Collection_Slip] where (class in (" + Class_id + ") or class in (" + Class_name + ")) and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' order by Content desc";
            }
            else
            {
                querys = "select distinct Content from dbo.[Monthly_Fee_Collection_Slip] where  session='" + Session_name + "' and (class in (" + Class_id + ") or class in (" + Class_name + ")) and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' order by Content desc";
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
                    string paid_amt = get_final_fees_amt_contentwise(drr["Content"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                    MyFeeReportOverAllItem.Add(new MyFeeReportOverAll
                    {
                        HeadFees = paid_amt,
                    });
                }
            }
            return MyFeeReportOverAllItem;
        }



        private string get_final_fees_amt_contentwise(string Content, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0")
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class in (" + Class_id + ") or class in (" + Class_name + ")) and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class in (" + Class_id + ") or class in (" + Class_name + ")) and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            string paids_amt = "0";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Paid_amt"].ToString();
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
            query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate='" + My.DateConvertToIdate(Dates) + "' group by t2.mode";

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


        public class MyFeeReportPaymentMode
        {
            public string Payment_mode { get; set; }
            public string Amounts { get; set; }
        }

        List<MyFeeReportPaymentMode> Show_FeeReportPaymentMode = new List<MyFeeReportPaymentMode>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_modewise(string Sessionid, string Classid, string FromDate, string ToDate, string Type, string Sessionname)
        {
            string Session_id = Sessionid;
            string Class_id = Classid;
            string Session_name = Sessionname;
            string Class_name = "0";

            string query = "";
            if (Session_id == "0")
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where (t1.class in (" + Class_id + ") or t1.class in (" + Class_name + ")) and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode";
            }
            else
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where (t1.class in (" + Class_id + ") or t1.class in (" + Class_name + ")) and t1.session='" + Session_name + "'  and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode";
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
                string class_name = ""; string isClassChecked = "0";
                DataTable dtc = My.dataTable("select * from Add_course_table where course_id in (" + Class_id + ")");
                if (dtc.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtc.Rows)
                    {
                        isClassChecked = "1";
                        class_name = class_name + "'" + dr["Course_Name"].ToString() + "'" + ",";
                    }
                    if (isClassChecked == "1")
                    {
                        Class_name = class_name.Remove(class_name.Length - 1);
                    }
                }

                foreach (DataRow dr in dt.Rows)
                {
                    Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                    {
                        Payment_mode = dr["Payment_mode"].ToString(),
                        Amounts = dr["Paid_amt"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_FeeReportPaymentMode));
            }
        }
    }
}
