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
    /// Summary description for collection_sheet_dcr
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class collection_sheet_dcr : System.Web.Services.WebService
    {
        #region MonthlY
        public class Fetch_Details_of_day_end_report_head
        {
            public string Content { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head> Show_of_day_end_report_head = new List<Fetch_Details_of_day_end_report_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type)
        {
            string query = "";
            //if (Collection_type == "1")
            //{
            if (Session_id == "0" && Class_id == "0")
            {
                query = @"select distinct Content from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee')  group by Content,content_id union all select top 1 'Form Sale' as Content from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc; 
                          select distinct Content from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                //select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee')  group by Content,content_id order by Content desc; 
                query = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee')  group by Content,content_id union all select top 1 'Form Sale' as Content from Form_sale_details where session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc;
                          select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                //select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee')  group by Content,content_id order by Content desc;
                query = @"select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee')  group by Content,content_id union all select top 1 'Form Sale' as Content from Form_sale_details where Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc;
                          select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else
            {
                //select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee')  group by Content,content_id order by Content desc; 
                query = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee')  group by Content,content_id union all select top 1 'Form Sale' as Content from Form_sale_details where session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc
                          select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            //}
            //else
            //{
            //    query = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class in (" + Class_id + ") or class in (" + Class_name + ")) and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee') group by Content,content_id order by Content desc; 
            //              select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class in (" + Class_id + ") or class in (" + Class_name + ")) and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee') group by Content,content_id order by Content desc";
            //}

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

            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtD = ds.Tables[1];
                if (dtD.Rows.Count > 0)
                {
                    Show_of_day_end_report_head.Add(new Fetch_Details_of_day_end_report_head
                    {
                        Content = "Adm./Annual Fee",
                    });
                }
            }
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //Context.Response.Write(js.Serialize(Show_of_day_end_report_head));


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
        public void fetch_report_heading_day_end_amts(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select * from (select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.class,t2.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as RowId,'0' as Type,'0' as Amount from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t union all select recpt_no as slipno,student_name as studentname,fathers_name as fathername,class,'-' as Section,'-' as adno,'-' as rollnumber,Payment_Mode as Payment_mode,Created_by as By_user_id,Created_date as Date,Created_idate as Idate,'0' as RowId,'1' as Type,Amount,(select top 1 name from user_details where user_id=t1.Created_by) as By_user_name from Form_sale_details t1 where t1.idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int ) asc";//order by RowId asc
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select * from (select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.class,t2.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as RowId,'0' as Type,'0' as Amount from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t union all select recpt_no as slipno,student_name as studentname,fathers_name as fathername,class,'-' as Section,'-' as adno,'-' as rollnumber,Payment_Mode as Payment_mode,Created_by as By_user_id,Created_date as Date,Created_idate as Idate,'0' as RowId,'1' as Type,Amount,(select top 1 name from user_details where user_id=t1.Created_by) as By_user_name from Form_sale_details t1 where t1.session='"+Session_name+ "' and t1.idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int ) asc";
                //query = "select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno) as RowId from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and  t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int ) asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select * from (select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.class,t2.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as RowId,'0' as Type,'0' as Amount from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t union all select recpt_no as slipno,student_name as studentname,fathers_name as fathername,class,'-' as Section,'-' as adno,'-' as rollnumber,Payment_Mode as Payment_mode,Created_by as By_user_id,Created_date as Date,Created_idate as Idate,'0' as RowId,'1' as Type,Amount,(select top 1 name from user_details where user_id=t1.Created_by) as By_user_name from Form_sale_details t1 where t1.Class_id='" + Class_id + "' and t1.idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int ) asc";
                //query = "select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno) as RowId from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and  t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int ) asc";
            }
            else
            {
                query = "select * from (select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.class,t2.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as RowId,'0' as Type,'0' as Amount from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t union all select recpt_no as slipno,student_name as studentname,fathers_name as fathername,class,'-' as Section,'-' as adno,'-' as rollnumber,Payment_Mode as Payment_mode,Created_by as By_user_id,Created_date as Date,Created_idate as Idate,'0' as RowId,'1' as Type,Amount,(select top 1 name from user_details where user_id=t1.Created_by) as By_user_name from Form_sale_details t1 where t1.session='" + Session_name + "' and t1.Class_id='" + Class_id + "' and t1.idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int ) asc";
                //query = "select *,(select top 1 name from user_details where user_id=t.By_user_id) as By_user_name from (select DISTINCT t1.slipno,t2.studentname,t2.fathername,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,(select top 1 user_id from Student_Payment_History where Slip_no=t1.slipno) as By_user_id,t1.Date,t1.Idate,(select top 1 Id from Student_Payment_History where Slip_no=t1.slipno) as RowId from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int ) asc";
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
                    List<MyFeeReport> MBdetails = findmyCollectionDt(dr["adno"].ToString(), dr["slipno"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name, dr["Type"].ToString(), dr["Amount"].ToString());

                    string months = "-";
                    if (dr["Type"].ToString() == "1")
                    { }
                    else
                    {
                        months = get_months_of_invoice(dr["slipno"].ToString());
                    }
                    string paids_amt = "";
                    if (dr["Type"].ToString() == "1")
                    {
                        paids_amt = dr["Amount"].ToString();
                    }
                    else
                    {
                        paids_amt = get_ttl_amt_of_a_std(dr["adno"].ToString(), dr["slipno"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
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

        private string get_ttl_amt_of_a_std(string Admission_no, string Slip_no, string session_id, string class_id, string fromDate, string toDate, string type, string session_name, string class_name)
        {
            string querys = "";
            querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + Admission_no + "' and slipno='" + Slip_no + "' and Session='"+ session_name + "'";

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

        private List<MyFeeReport> findmyCollectionDt(string Admission_no, string Slip_no, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string FormSaleType, string formSaleAmt)
        {
            List<MyFeeReport> MyFeeReportItem = new List<MyFeeReport>();
            string querys = "";
            if (Session_id == "0" && Class_id == "0")
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee') union all select top 1 'Form Sale' as Content from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc; 
                           select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + Admission_no + "' and slipno='" + Slip_no + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                //select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "'   and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee') order by Content desc; 
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee') union all select top 1 'Form Sale' as Content from Form_sale_details where session='" + Session_name + "'   and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc; 
                           select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + Admission_no + "' and slipno='" + Slip_no + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "'   and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                //select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee') order by Content desc; 
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee') union all select top 1 'Form Sale' as Content from Form_sale_details where Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc;
                           select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + Admission_no + "' and slipno='" + Slip_no + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else
            {
                //select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee') order by Content desc; 
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('MonthlyFee','HostelMonthlyFee') union all select top 1 'Form Sale' as Content from Form_sale_details where session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc;
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
                        string paid_amt = get_fees_amt(drr["Content"].ToString(), Admission_no, "0", Type, Slip_no, formSaleAmt);
                        MyFeeReportItem.Add(new MyFeeReport
                        {
                            HeadFees = paid_amt,
                        });
                    }
                }
            }


            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTable dtD2 = ds.Tables[2];
                if (dtD2.Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataTable dtD = ds.Tables[1];
                        if (dtD.Rows.Count > 0)
                        {
                            MyFeeReportItem.Add(new MyFeeReport
                            {
                                HeadFees = dtD.Rows[0]["Paid_amt"].ToString(),
                            });
                        }
                    }
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


        private string get_fees_amt(string content, string admission_no, string Dates, string Type, string slip_no, string formSaleAmt)
        {
            string querys = "";
            querys = "select (isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, previously_paid)),0)) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + admission_no + "' and Content='" + content + "' and slipno='" + slip_no + "'";
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
        public void fetch_report_heading_day_end_final_amts(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type)
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
                    List<MyFeeReportOverAll> MBdetails = FindMyFeeReportOverAllDt(Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                    string paidFesAmt = get_ttl_amt_of_overall(Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
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

        private string get_ttl_amt_of_overall(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string querys = "";
            if (Session_id == "0" && Class_id == "0")
            {
                querys = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Paid_amt from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Paid_amt from Form_sale_details where session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
                //querys = "select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                querys = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Paid_amt from Form_sale_details where Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
               // querys = "select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                querys = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Paid_amt from Form_sale_details where session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
               // querys = "select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
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
            if (Session_id == "0" && Class_id == "0")
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in ('MonthlyFee','HostelMonthlyFee') union all select top 1 'Form Sale' as Content from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc;
                           select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where session = '" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in ('MonthlyFee','HostelMonthlyFee') union all select top 1 'Form Sale' as Content from Form_sale_details where session = '" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc;
                           select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session = '" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where session = '" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                //select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in ('MonthlyFee','HostelMonthlyFee') order by Content desc;
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in ('MonthlyFee','HostelMonthlyFee') union all select top 1 'Form Sale' as Content from Form_sale_details where Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc;
                           select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee');
                           select distinct Content from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and parameter in ('HostelAnnualFee','AdmissionFee','AnnualFee','HostelAdmissionFee')  group by Content,content_id order by Content desc";
            }
            else
            {
                querys = @"select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"'  and parameter in ('MonthlyFee','HostelMonthlyFee') union all select top 1 'Form Sale' as Content from Form_sale_details where session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' order by Content desc;
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
                        string paid_amt = get_final_fees_amt_contentwise(drr["Content"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                        MyFeeReportOverAllItem.Add(new MyFeeReportOverAll
                        {
                            HeadFees = paid_amt,
                        });
                    }
                }
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTable dtD2 = ds.Tables[2];
                if (dtD2.Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataTable dtD = ds.Tables[1];
                        if (dtD.Rows.Count > 0)
                        {
                            MyFeeReportOverAllItem.Add(new MyFeeReportOverAll
                            {
                                HeadFees = dtD.Rows[0]["Paid_amt"].ToString(),
                            });
                        }
                    }
                }
            }


            return MyFeeReportOverAllItem;
        }



        private string get_final_fees_amt_contentwise(string Content, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                if (Content == "Form Sale")
                {
                    query = "select isnull(sum(convert(float, Amount)),0) as Paid_amt from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
                }
                else
                {
                    query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
                }
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                if (Content == "Form Sale")
                {
                    query = "select isnull(sum(convert(float, Amount)),0) as Paid_amt from Form_sale_details where session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
                }
                else
                {
                    query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
                }
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                if (Content == "Form Sale")
                {
                    query = "select isnull(sum(convert(float, Amount)),0) as Paid_amt from Form_sale_details where Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
                }
                else
                {
                    query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
                }
            }
            else
            {
                if (Content == "Form Sale")
                {
                    query = "select isnull(sum(convert(float, Amount)),0) as Paid_amt from Form_sale_details where session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
                }
                else
                {
                    query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
                }
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
            query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no and t1.Session=t2.Session where t1.Idate='" + My.DateConvertToIdate(Dates) + "' group by t2.mode";

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
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //Context.Response.Write(js.Serialize(Show_of_day_end_mode));


                string json = JsonConvert.SerializeObject(Show_of_day_end_mode);
                Context.Response.Write(json);
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
        public void fetch_report_modewise(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode union all select isnull(sum(convert(float, Amount)),0) as Paid_amt,Payment_Mode as Payment_mode from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' group by Payment_mode) tu group by Payment_mode";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where t1.session='" + Session_name + "' and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode union all select isnull(sum(convert(float, Amount)),0) as Paid_amt,Payment_Mode as Payment_mode from Form_sale_details where session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' group by Payment_mode) tu group by Payment_mode";
                //query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where t1.session='" + Session_name + "' and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode union all select isnull(sum(convert(float, Amount)),0) as Paid_amt,Payment_Mode as Payment_mode from Form_sale_details where Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' group by Payment_mode) tu group by Payment_mode";
                //query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode";
            }
            else
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno and Session=t1.Session) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where t1.session='" + Session_name + "' and (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode union all select isnull(sum(convert(float, Amount)),0) as Paid_amt,Payment_Mode as Payment_mode from Form_sale_details where session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' group by Payment_mode) tu group by Payment_mode";
                //query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where t1.session='" + Session_name + "' and (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode";
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
                    Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                    {
                        Payment_mode = dr["Payment_mode"].ToString(),
                        Amounts = dr["Paid_amt"].ToString(),
                    });
                }
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //Context.Response.Write(js.Serialize(Show_FeeReportPaymentMode));

                string json = JsonConvert.SerializeObject(Show_FeeReportPaymentMode);
                Context.Response.Write(json);
            }
        }



        //CollectionheetHead
        public class Fetch_Details_of_collection_mode_head
        {
            public string Content { get; set; }
        }

        List<Fetch_Details_of_collection_mode_head> Show_of_collection_mode_head = new List<Fetch_Details_of_collection_mode_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_userwise_collection_head(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = @"select distinct mode from (select distinct mode from Student_Payment_History where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' group by mode  union all select distinct Payment_Mode as mode from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t order by mode asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select distinct mode from (select distinct mode from Student_Payment_History where Session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' group by mode  union all select distinct Payment_Mode as mode from Form_sale_details where Session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t order by mode asc";
                //query = @"select distinct mode from Student_Payment_History where  Session='" + Session_name + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select distinct mode from (select distinct mode from Student_Payment_History where Class_id='" + Class_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' group by mode  union all select distinct Payment_Mode as mode from Form_sale_details where Class_id='" + Class_id + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t order by mode asc";
                //query = @"select distinct mode from Student_Payment_History where  Class_id='" + Class_id + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }
            else
            {
                query = @"select distinct mode from (select distinct mode from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' group by mode  union all select distinct Payment_Mode as mode from Form_sale_details where Session='" + Session_name + "' and Class_id='" + Class_id + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t order by mode asc";
                //query = @"select distinct mode from Student_Payment_History where  Session='" + Session_name + "' and   Class_id='" + Class_id + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }


            DataTable dtD = My.dataTable(query); ;
            if (dtD.Rows.Count > 0)
            {
                foreach (DataRow dr in dtD.Rows)
                {

                    Show_of_collection_mode_head.Add(new Fetch_Details_of_collection_mode_head
                    {
                        Content = dr["mode"].ToString(),
                    });
                }
            }


            //JavaScriptSerializer js = new JavaScriptSerializer();
            //Context.Response.Write(js.Serialize(Show_of_collection_mode_head));

            string json = JsonConvert.SerializeObject(Show_of_collection_mode_head);
            Context.Response.Write(json);
        }


        /// <summary>
        /// USER MODEWISE
        /// </summary>
        public class Fetch_Details_of_user_amt
        {
            public string User_name { get; set; }
            public string User_id { get; set; }
            public string PaidFesAmt { get; set; }
            //============-----------==============
            public List<MyFeeReport_user_amt> MyFeeReportuser_amtItem { get; set; }

        }
        public class MyFeeReport_user_amt
        {
            public string HeadFees { get; set; }
        }


        List<Fetch_Details_of_user_amt> Show_of_day_end_report_user_amt = new List<Fetch_Details_of_user_amt>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_userwise_collection_amt(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select distinct user_id,User_name from (select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History  where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t union all select distinct Created_by as user_id, (select top 1 name from user_details where user_id=Form_sale_details.Created_by) as User_name from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t  order by User_name asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select distinct user_id,User_name from (select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History where Session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t union all select distinct Created_by as user_id, (select top 1 name from user_details where user_id=Form_sale_details.Created_by) as User_name from Form_sale_details where Session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t  order by User_name asc";
                //query = "select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History where Session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by User_name asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select distinct user_id,User_name from (select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History where Class_id='" + Class_id + "' and Session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t union all select distinct Created_by as user_id, (select top 1 name from user_details where user_id=Form_sale_details.Created_by) as User_name from Form_sale_details where Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t  order by User_name asc";
                //query = "select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History where Class_id='" + Class_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by User_name asc";
            }
            else
            {
                query = "select distinct user_id,User_name from (select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and Session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t union all select distinct Created_by as user_id, (select top 1 name from user_details where user_id=Form_sale_details.Created_by) as User_name from Form_sale_details where Session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t  order by User_name asc";
                //query = "select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by User_name asc";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Payment_History");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeReport_user_amt> MBdetails = findmyCollectionusermodewise(dr["user_id"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                    string paids_amt = get_ttl_amt_of_a_user(dr["user_id"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);


                    Show_of_day_end_report_user_amt.Add(new Fetch_Details_of_user_amt
                    {
                        User_name = dr["User_name"].ToString(),
                        User_id = dr["user_id"].ToString(),
                        PaidFesAmt = paids_amt,
                        MyFeeReportuser_amtItem = MBdetails
                    });
                }
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //Context.Response.Write(js.Serialize(Show_of_day_end_report_user_amt));

                string json = JsonConvert.SerializeObject(Show_of_day_end_report_user_amt);
                Context.Response.Write(json);
            }
        }

        private string get_ttl_amt_of_a_user(string user_id, string Session_id, string Class_id, string FromDate, string ToDate, string type, string Session_name, string class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = @"select isnull(sum(convert(float, Total_paid)),0) as Total_paid from (select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Created_by='" + user_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select isnull(sum(convert(float, Total_paid)),0) as Total_paid from (select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Session='" + Session_name + "' and Created_by='" + user_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
                //query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select isnull(sum(convert(float, Total_paid)),0) as Total_paid from (select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Class_id='" + Class_id + "' and user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Class_id='" + Class_id + "' and Created_by='" + user_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
                //query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Class_id='" + Class_id + "' and user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                query = @"select isnull(sum(convert(float, Total_paid)),0) as Total_paid from (select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Session='" + Session_name + "' and Class_id='" + Class_id + "' and Created_by='" + user_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
                //query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            string paids_amt = "0";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Total_paid"].ToString();
            }
            return paids_amt;
        }

        private List<MyFeeReport_user_amt> findmyCollectionusermodewise(string User_id, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            List<MyFeeReport_user_amt> MyFeeReportuser_amtItem = new List<MyFeeReport_user_amt>();
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = @"select distinct mode from (select distinct mode from Student_Payment_History where Idate>='" + My.DateConvertToIdate(FromDate) + @"' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode union all select distinct Payment_Mode as mode from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + @"' and idate<='" + My.DateConvertToIdate(ToDate) + @"') t order by mode asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select distinct mode from (select distinct mode from Student_Payment_History where Session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + @"' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode union all select distinct Payment_Mode as mode from Form_sale_details where session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + @"' and idate<='" + My.DateConvertToIdate(ToDate) + @"') t order by mode asc";
                //query = @"select distinct mode from Student_Payment_History where  Session='" + Session_name + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select distinct mode from (select distinct mode from Student_Payment_History where Class_id='" + Class_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + @"' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode union all select distinct Payment_Mode as mode from Form_sale_details where Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + @"' and idate<='" + My.DateConvertToIdate(ToDate) + @"') t order by mode asc";
                //query = @"select distinct mode from Student_Payment_History where  Class_id='" + Class_id + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }
            else
            {
                query = @"select distinct mode from (select distinct mode from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + @"' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode union all select distinct Payment_Mode as mode from Form_sale_details where Session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + @"' and idate<='" + My.DateConvertToIdate(ToDate) + @"') t order by mode asc";
                //query = @"select distinct mode from Student_Payment_History where  Session='" + Session_name + "' and   Class_id='" + Class_id + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }

            DataTable dtD = My.dataTable(query);
            if (dtD.Rows.Count > 0)
            {
                foreach (DataRow drr in dtD.Rows)
                {
                    string paid_amt = get_fees_amt_user_mode(drr["mode"].ToString(), User_id, Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                    MyFeeReportuser_amtItem.Add(new MyFeeReport_user_amt
                    {
                        HeadFees = paid_amt,
                    });
                }
            }
            return MyFeeReportuser_amtItem;
        }

        private string get_fees_amt_user_mode(string mode, string user_id, string Session_id, string Class_id, string FromDate, string ToDate, string type, string Session_name, string class_name)
        {
            string CollectAmt = "0";
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = @"select isnull(sum(convert(float, Total_paid)),0) as Total_paid from (select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Created_by='" + user_id + "' and Payment_Mode='" + mode + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select isnull(sum(convert(float, Total_paid)),0) as Total_paid from (select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Session='" + Session_name + "' and Created_by='" + user_id + "' and Payment_Mode='" + mode + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
                //query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select isnull(sum(convert(float, Total_paid)),0) as Total_paid from (select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Class_id='" + Class_id + "' and user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Class_id='" + Class_id + "' and Created_by='" + user_id + "' and Payment_Mode='" + mode + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
                //query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Class_id='" + Class_id + "' and user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                query = @"select isnull(sum(convert(float, Total_paid)),0) as Total_paid from (select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' union all select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Session='" + Session_name + "' and Class_id='" + Class_id + "' and Created_by='" + user_id + "' and Payment_Mode='" + mode + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "') t";
                //query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }

            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                CollectAmt = dt.Rows[0]["Total_paid"].ToString();
            }
            return CollectAmt;
        }
    }
}
