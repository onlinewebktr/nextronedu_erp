using Newtonsoft.Json;
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

namespace school_web.Admin.webServices.account
{
    /// <summary>
    /// Summary description for dcr_adm
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class dcr_adm : System.Web.Services.WebService
    {
        #region MonthlY
        public class Fetch_Details_of_day_end_report_head
        {
            public string Content { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head> Show_of_day_end_report_head = new List<Fetch_Details_of_day_end_report_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type, string Std_type)
        {
            string fee_Type = "";
            if (Std_type == "0")
            {
                fee_Type = "'AdmissionFee','AnnualFee','HostelAdmissionFee','HostelAnnualFee'";
            }
            if (Std_type == "1")
            {
                fee_Type = "'AdmissionFee','HostelAdmissionFee'";
            }
            if (Std_type == "2")
            {
                fee_Type = "'AnnualFee','HostelAnnualFee'";
            }
            string query = "";
            //if (Collection_type == "1")
            //{
            if (Session_id == "0" && Class_id == "0")
            {
                query = @"select distinct Content from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in (" + fee_Type + ") group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in (" + fee_Type + ") group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in (" + fee_Type + ") group by Content,content_id order by Content desc";
            }
            else
            {
                query = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in (" + fee_Type + ") group by Content,content_id order by Content desc";
            }

            DataSet ds = mycode.Fill_Data_set(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtD = ds.Tables[0];
                if (dtD.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtD.Rows)
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
                }
            } 
            string json = JsonConvert.SerializeObject(Show_of_day_end_report_head);
            Context.Response.Write(json);
        }



        My mycode = new My();
        public class Fetch_Details_of_day_end_report_head_amts
        {
            public string Payment_date { get; set; }
            public string Slip_no { get; set; }
            public string Months { get; set; }
            public string Student_name { get; set; }
            public string Father_name { get; set; }
            public string Class_name { get; set; }
            public string Sections { get; set; }
            public string Admission_no { get; set; }
            public string Roll_no { get; set; }
            public string Payment_mode { get; set; }
            public string PaidFesAmt { get; set; }
            public string IssuedBy { get; set; }
            public string Transaction_Id { get; set; }
            public string StudentStatus { get; set; }

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
        public void fetch_report_heading_day_end_amts(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type, string Std_type)
        {
            string fee_Type = "";
            if (Std_type == "0")
            {
                fee_Type = "'AdmissionFee','AnnualFee','HostelAdmissionFee','HostelAnnualFee'";
            }
            if (Std_type == "1")
            {
                fee_Type = "'AdmissionFee','HostelAdmissionFee'";
            }
            if (Std_type == "2")
            {
                fee_Type = "'AnnualFee','HostelAnnualFee'";
            }
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select * from (select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.Transfer_Status,t2.class,t2.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode,(select top 1 Pay_mode_transaction_no from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Transaction_Id,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as RowId,'0' as Type,'0' as Amount from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and t1.parameter in (" + fee_Type + ")) t) t order by try_cast(slipno as int ) asc";//order by RowId asc
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select * from (select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.Transfer_Status,t2.class,t2.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode,(select top 1 Pay_mode_transaction_no from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Transaction_Id,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as RowId,'0' as Type,'0' as Amount from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and t1.parameter in (" + fee_Type + ")) t) t order by try_cast(slipno as int ) asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select * from (select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.Transfer_Status,t2.class,t2.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode,(select top 1 Pay_mode_transaction_no from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Transaction_Id,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as RowId,'0' as Type,'0' as Amount from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and t1.parameter in (" + fee_Type + ")) t) t order by try_cast(slipno as int ) asc";
            }
            else
            {
                query = "select * from (select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.Transfer_Status,t2.class,t2.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode,(select top 1 Pay_mode_transaction_no from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Transaction_Id,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as RowId,'0' as Type,'0' as Amount from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and t1.parameter in (" + fee_Type + "))t) t  order by try_cast(slipno as int ) asc";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            { }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeReport> MBdetails = findmyCollectionDt(dr["adno"].ToString(), dr["slipno"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name, dr["Type"].ToString(), dr["Amount"].ToString(), Std_type);

                    string months = "-";
                    string paids_amt = get_ttl_amt_of_a_std(dr["adno"].ToString(), dr["slipno"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name, Std_type);

                    string transaction = dr["Transaction_Id"].ToString();
                    if (dr["Payment_mode"].ToString().ToUpper() == "CASH")
                    {
                        transaction = "";
                    }
                    string studentStatus = "Old";
                    if (dr["Transfer_Status"].ToString().ToUpper() == "NEW")
                    {
                        studentStatus = "New";
                    }

                    Show_of_day_end_report_head_amts.Add(new Fetch_Details_of_day_end_report_head_amts
                    {
                        Payment_date = dr["Date"].ToString(),
                        Slip_no = dr["slipno"].ToString(),
                        Months = months,
                        Student_name = dr["studentname"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Sections = dr["Section"].ToString(),
                        Admission_no = dr["adno"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),
                        Payment_mode = dr["Payment_mode"].ToString(),
                        PaidFesAmt = paids_amt,
                        IssuedBy = dr["By_user_name"].ToString(),
                        Transaction_Id = transaction,
                        StudentStatus= studentStatus,
                        //==========================

                        MyFeeReportItem = MBdetails
                    });
                }
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts));

                string json = JsonConvert.SerializeObject(Show_of_day_end_report_head_amts);
                Context.Response.Write(json);
            }
        }

        private string get_ttl_amt_of_a_std(string Admission_no, string Slip_no, string session_id, string class_id, string fromDate, string toDate, string type, string session_name, string class_name, string Std_type)
        {
            string fee_Type = "";
            if (Std_type == "0")
            {
                fee_Type = "'AdmissionFee','AnnualFee','HostelAdmissionFee','HostelAnnualFee'";
            }
            if (Std_type == "1")
            {
                fee_Type = "'AdmissionFee','HostelAdmissionFee'";
            }
            if (Std_type == "2")
            {
                fee_Type = "'AnnualFee','HostelAnnualFee'";
            }
            string querys = "";
            querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + Admission_no + "' and slipno='" + Slip_no + "' and Session='" + session_name + "' and parameter in (" + fee_Type + ")";
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

        private List<MyFeeReport> findmyCollectionDt(string Admission_no, string Slip_no, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string FormSaleType, string formSaleAmt, string Std_type)
        {
            List<MyFeeReport> MyFeeReportItem = new List<MyFeeReport>();
            string fee_Type = "";
            if (Std_type == "0")
            {
                fee_Type = "'AdmissionFee','AnnualFee','HostelAdmissionFee','HostelAnnualFee'";
            }
            if (Std_type == "1")
            {
                fee_Type = "'AdmissionFee','HostelAdmissionFee'";
            }
            if (Std_type == "2")
            {
                fee_Type = "'AnnualFee','HostelAnnualFee'";
            }
            string querys = "";
            if (Session_id == "0" && Class_id == "0")
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in (" + fee_Type + @") order by Content desc; 
                           select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + Admission_no + "' and slipno='" + Slip_no + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in (" + fee_Type + @") order by Content desc; 
                           select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + Admission_no + "' and slipno='" + Slip_no + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "'   and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in (" + fee_Type + @") order by Content desc;
                           select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + Admission_no + "' and slipno='" + Slip_no + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in (" + fee_Type + @") order by Content desc;
                           select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and adno='" + Admission_no + "' and slipno='" + Slip_no + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee'); 
                           select distinct Content from Monthly_Fee_Collection_Slip where session = '" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee') group by Content,content_id order by Content desc";
            }
            DataSet ds = mycode.Fill_Data_set(querys);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtD = ds.Tables[0];
                if (dtD.Rows.Count > 0)
                {
                    foreach (DataRow drr in dtD.Rows)
                    {
                        string paid_amt = get_fees_amt(drr["Content"].ToString(), Admission_no, "0", Type, Slip_no, formSaleAmt, Std_type);
                        MyFeeReportItem.Add(new MyFeeReport
                        {
                            HeadFees = paid_amt,
                        });
                    }
                }
            }
            return MyFeeReportItem;
        }



        private string get_fees_amt(string content, string admission_no, string Dates, string Type, string slip_no, string formSaleAmt, string Std_type)
        {
            string fee_Type = "";
            if (Std_type == "0")
            {
                fee_Type = "'AdmissionFee','AnnualFee','HostelAdmissionFee','HostelAnnualFee'";
            }
            if (Std_type == "1")
            {
                fee_Type = "'AdmissionFee','HostelAdmissionFee'";
            }
            if (Std_type == "2")
            {
                fee_Type = "'AnnualFee','HostelAnnualFee'";
            }
            string querys = "";
            querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + admission_no + "' and Content='" + content + "' and slipno='" + slip_no + "'  and parameter in (" + fee_Type + ")";
            string paids_amt = "0";
            if (content == "Form Sale")
            {
                paids_amt = formSaleAmt;
            }
            else
            {
                DataTable dt = mycode.FillData(querys);
                if (dt.Rows.Count == 0)
                {
                    paids_amt = "0";
                }
                else
                {
                    paids_amt = dt.Rows[0]["Paid_amt"].ToString();
                }
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
        public void fetch_report_heading_day_end_final_amts(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type, string Std_type)
        {
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
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeReportOverAll> MBdetails = FindMyFeeReportOverAllDt(Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name, Std_type);
                    string paidFesAmt = get_ttl_amt_of_overall(Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name, Std_type);
                    Show_of_day_end_report_head_amts_final.Add(new Fetch_Details_of_day_end_report_head_amts_final
                    {
                        TotaLFinaLPaidFeeS = paidFesAmt,
                        MyFeeReportOverAllItem = MBdetails
                    });
                }
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts_final));

                string json = JsonConvert.SerializeObject(Show_of_day_end_report_head_amts_final);
                Context.Response.Write(json);
            }
        }

        private string get_ttl_amt_of_overall(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Std_type)
        {
            string fee_Type = "";
            if (Std_type == "0")
            {
                fee_Type = "'AdmissionFee','AnnualFee','HostelAdmissionFee','HostelAnnualFee'";
            }
            if (Std_type == "1")
            {
                fee_Type = "'AdmissionFee','HostelAdmissionFee'";
            }
            if (Std_type == "2")
            {
                fee_Type = "'AnnualFee','HostelAnnualFee'";
            }
            string querys = "";
            if (Session_id == "0" && Class_id == "0")
            {
                querys = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in (" + fee_Type + ")) t";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in (" + fee_Type + ")) t";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                querys = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in (" + fee_Type + ")) t";
            }
            else
            {
                querys = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in (" + fee_Type + ")) t";
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

        private List<MyFeeReportOverAll> FindMyFeeReportOverAllDt(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Std_type)
        {
            List<MyFeeReportOverAll> MyFeeReportOverAllItem = new List<MyFeeReportOverAll>();
            string fee_Type = "";
            if (Std_type == "0")
            {
                fee_Type = "'AdmissionFee','AnnualFee','HostelAdmissionFee','HostelAnnualFee'";
            }
            if (Std_type == "1")
            {
                fee_Type = "'AdmissionFee','HostelAdmissionFee'";
            }
            if (Std_type == "2")
            {
                fee_Type = "'AnnualFee','HostelAnnualFee'";
            }
            string querys = ""; 
            if (Session_id == "0" && Class_id == "0")
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in (" + fee_Type + @") order by Content desc;
                           select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where session = '" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in (" + fee_Type + @")  order by Content desc;
                           select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session = '" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where session = '" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            { 
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in (" + fee_Type + @")  order by Content desc;
                           select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in (" + fee_Type + @")  order by Content desc;
                           select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee'); 
                           select distinct Content from Monthly_Fee_Collection_Slip where session = '" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee') group by Content,content_id order by Content desc";
            }
            DataSet ds = mycode.Fill_Data_set(querys);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtD = ds.Tables[0];
                if (dtD.Rows.Count > 0)
                {
                    foreach (DataRow drr in dtD.Rows)
                    {
                        string paid_amt = get_final_fees_amt_contentwise(drr["Content"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name, Std_type);
                        MyFeeReportOverAllItem.Add(new MyFeeReportOverAll
                        {
                            HeadFees = paid_amt,
                        });
                    }
                }
            }
            return MyFeeReportOverAllItem;
        }



        private string get_final_fees_amt_contentwise(string Content, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Std_type)
        {
            string fee_Type = "";
            if (Std_type == "0")
            {
                fee_Type = "'AdmissionFee','AnnualFee','HostelAdmissionFee','HostelAnnualFee'";
            }
            if (Std_type == "1")
            {
                fee_Type = "'AdmissionFee','HostelAdmissionFee'";
            }
            if (Std_type == "2")
            {
                fee_Type = "'AnnualFee','HostelAnnualFee'";
            }
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in (" + fee_Type + ")";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in (" + fee_Type + ")";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in (" + fee_Type + ")";
            }
            else
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in (" + fee_Type + ")";
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

        #endregion
    }
}
