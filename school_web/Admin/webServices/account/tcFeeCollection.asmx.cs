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
    /// Summary description for tcFeeCollection
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class tcFeeCollection : System.Web.Services.WebService
    {

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

        }
        List<Fetch_Details_of_day_end_report_head_amts> Show_of_day_end_report_head_amts = new List<Fetch_Details_of_day_end_report_head_amts>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_amts(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select * from (select (select top 1 Session from session_details where session_id=t1.Session_id) as session,Session_id,t1.Class_id,recpt_no as slipno,student_name as studentname,fathers_name as fathername,class,'-' as Section,'-' as adno,'-' as rollnumber,Payment_Mode as Payment_mode,Bank_tran_no as Transaction_Id,Created_by as By_user_id,Created_date as Date,Created_idate as Idate,'0' as RowId,'1' as Type,Amount,(select top 1 name from user_details where user_id=t1.Created_by) as By_user_name from Form_sale_details t1 where t1.idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int) asc";//order by RowId asc
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select * from (select (select top 1 Session from session_details where session_id=t1.Session_id) as session,Session_id,t1.Class_id,recpt_no as slipno,student_name as studentname,fathers_name as fathername,class,'-' as Section,'-' as adno,'-' as rollnumber,Payment_Mode as Payment_mode,Bank_tran_no as Transaction_Id,Created_by as By_user_id,Created_date as Date,Created_idate as Idate,'0' as RowId,'1' as Type,Amount,(select top 1 name from user_details where user_id=t1.Created_by) as By_user_name from Form_sale_details t1 where t1.session='" + Session_name + "' and t1.idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int ) asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select * from (select (select top 1 Session from session_details where session_id=t1.Session_id) as session,Session_id,t1.Class_id,recpt_no as slipno,student_name as studentname,fathers_name as fathername,class,'-' as Section,'-' as adno,'-' as rollnumber,Payment_Mode as Payment_mode,Bank_tran_no as Transaction_Id,Created_by as By_user_id,Created_date as Date,Created_idate as Idate,'0' as RowId,'1' as Type,Amount,(select top 1 name from user_details where user_id=t1.Created_by) as By_user_name from Form_sale_details t1 where t1.Class_id='t1.Class_id='" + Class_id + "' and t1.idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int ) asc";
            }
            else
            {
                query = "select * from (select (select top 1 Session from session_details where session_id=t1.Session_id) as session,Session_id,t1.Class_id,recpt_no as slipno,student_name as studentname,fathers_name as fathername,class,'-' as Section,'-' as adno,'-' as rollnumber,Payment_Mode as Payment_mode,Bank_tran_no as Transaction_Id,Created_by as By_user_id,Created_date as Date,Created_idate as Idate,'0' as RowId,'1' as Type,Amount,(select top 1 name from user_details where user_id=t1.Created_by) as By_user_name from Form_sale_details t1 where t1.session='" + Session_name + "' and t1.Class_id='" + Class_id + "' and t1.idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.idate<='" + My.DateConvertToIdate(ToDate) + "') t order by try_cast(slipno as int ) asc";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string months = "-";
                    string dates = dr["Date"].ToString();
                    months = My.get_month_name_by_date(dates);
                    string paids_amt = dr["Amount"].ToString();
                    string transaction = dr["Transaction_Id"].ToString();
                    if (dr["Payment_mode"].ToString().ToUpper() == "CASH")
                    {
                        transaction = "";
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
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts));
            }
        }


        //=================================================
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
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select isnull(sum(convert(float, Amount)),0) as Paid_amt,Payment_Mode as Payment_mode from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' group by Payment_mode) tu group by Payment_mode";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select isnull(sum(convert(float, Amount)),0) as Paid_amt,Payment_Mode as Payment_mode from Form_sale_details where session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' group by Payment_mode) tu group by Payment_mode";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select isnull(sum(convert(float, Amount)),0) as Paid_amt,Payment_Mode as Payment_mode from Form_sale_details where session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' group by Payment_mode) tu group by Payment_mode";
            }
            else
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select isnull(sum(convert(float, Amount)),0) as Paid_amt,Payment_Mode as Payment_mode from Form_sale_details where session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' group by Payment_mode) tu group by Payment_mode";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
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



        //CollectionheetHead===========================================
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
                query = @"select distinct Payment_Mode as mode from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' order by mode asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select distinct Payment_Mode as mode from Form_sale_details where Session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' order by mode asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select distinct Payment_Mode as mode from Form_sale_details where Class_id='" + Class_id + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' order by mode asc";
            }
            else
            {
                query = @"select distinct Payment_Mode as mode from Form_sale_details where  Session='" + Session_name + "' and Class_id='" + Class_id + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' order by mode asc";
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
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_of_collection_mode_head));
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
                query = "select distinct Created_by as user_id, (select top 1 name from user_details where user_id=Form_sale_details.Created_by) as User_name from Form_sale_details where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' order by User_name asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select distinct Created_by as user_id, (select top 1 name from user_details where user_id=Form_sale_details.Created_by) as User_name from Form_sale_details where Session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' order by User_name asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select distinct Created_by as user_id, (select top 1 name from user_details where user_id=Form_sale_details.Created_by) as User_name from Form_sale_details where Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' order by User_name asc";
            }
            else
            {
                query = "select distinct Created_by as user_id, (select top 1 name from user_details where user_id=Form_sale_details.Created_by) as User_name from Form_sale_details where Session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "' order by User_name asc";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Payment_History");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
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
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_user_amt));
            }
        }

        private string get_ttl_amt_of_a_user(string user_id, string Session_id, string Class_id, string FromDate, string ToDate, string type, string Session_name, string class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Created_by='" + user_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Session='" + Session_name + "' and Created_by='" + user_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Class_id='" + Class_id + "' and Created_by='" + user_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Session='" + Session_name + "' and Class_id='" + Class_id + "' and Created_by='" + user_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
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
                query = @"select distinct Payment_Mode as mode from Form_sale_details where idate>='" + My.DateConvertToIdate(FromDate) + @"' and idate<='" + My.DateConvertToIdate(ToDate) + @"' order by mode asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select distinct Payment_Mode as mode from Form_sale_details where session='" + Session_name + "' and idate>='" + My.DateConvertToIdate(FromDate) + @"' and idate<='" + My.DateConvertToIdate(ToDate) + @"' order by mode asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select distinct Payment_Mode as mode from Form_sale_details where Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + @"' and idate<='" + My.DateConvertToIdate(ToDate) + @"' order by mode asc";
            }
            else
            {
                query = @"select distinct Payment_Mode as mode from Form_sale_details where Session='" + Session_name + "' and Class_id='" + Class_id + "' and idate>='" + My.DateConvertToIdate(FromDate) + @"' and idate<='" + My.DateConvertToIdate(ToDate) + @"' order by mode asc";
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
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Created_by='" + user_id + "' and Payment_Mode='" + mode + "' and idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Session='" + Session_name + "' and Created_by='" + user_id + "' and Payment_Mode='" + mode + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Class_id='" + Class_id + "' and Created_by='" + user_id + "' and Payment_Mode='" + mode + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Form_sale_details where Session='" + Session_name + "' and Class_id='" + Class_id + "' and Created_by='" + user_id + "' and Payment_Mode='" + mode + "' and  idate>='" + My.DateConvertToIdate(FromDate) + "' and idate<='" + My.DateConvertToIdate(ToDate) + "'";
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
