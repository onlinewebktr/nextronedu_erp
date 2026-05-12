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
    /// Summary description for fee_report
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class fee_report : System.Web.Services.WebService
    {

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
            ad.Fill(ds, "Complain_chat");
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
            //============-----------==============
            public string showFeeS1 { get; set; }
            public string showFeeS2 { get; set; }
            public string showFeeS3 { get; set; }
            public string showFeeS4 { get; set; }
            public string showFeeS5 { get; set; }
            public string showFeeS6 { get; set; }
            public string showFeeS7 { get; set; }
            public string showFeeS8 { get; set; }
            public string showFeeS9 { get; set; }
            public string showFeeS10 { get; set; }
            public string showFeeS11 { get; set; }
            public string showFeeS12 { get; set; }
            public string showFeeS13 { get; set; }
            public string showFeeS14 { get; set; }
            public string showFeeS15 { get; set; }
            public string TotaLFeeS { get; set; }
            public string TtlFinalPaidFeeS { get; set; }
            public string Rest_amount { get; set; }

            //======================================
            public string DshowFeeS1 { get; set; }
            public string DshowFeeS2 { get; set; }
            public string DshowFeeS3 { get; set; }
            public string DshowFeeS4 { get; set; }
            public string DshowFeeS5 { get; set; }
            public string DshowFeeS6 { get; set; }
            public string DshowFeeS7 { get; set; }
            public string DshowFeeS8 { get; set; }
            public string DshowFeeS9 { get; set; }
            public string DshowFeeS10 { get; set; }
            public string DshowFeeS11 { get; set; }
            public string DshowFeeS12 { get; set; }
            public string DshowFeeS13 { get; set; }
            public string DshowFeeS14 { get; set; }
            public string DshowFeeS15 { get; set; }
            public string DTotaLFeeS { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head_amts> Show_of_day_end_report_head_amts = new List<Fetch_Details_of_day_end_report_head_amts>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_amts(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,t1.Date from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelMonthlyFee') order by rollnumber desc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,t1.Date from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelMonthlyFee') order by rollnumber desc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,t1.Date from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and (t1.parameter='" + Type + "' or t1.parameter='HostelMonthlyFee') order by rollnumber desc";
            }
            else
            {
                query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode,t1.Date from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.session='" + Session_name + "' and (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "' and (t1.parameter='" + Type + "' or t1.parameter='HostelMonthlyFee') order by rollnumber desc";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string fees1 = "hidden";
                string fees2 = "hidden";
                string fees3 = "hidden";
                string fees4 = "hidden";
                string fees5 = "hidden";
                string fees6 = "hidden";
                string fees7 = "hidden";
                string fees8 = "hidden";
                string fees9 = "hidden";
                string fees10 = "hidden";
                string fees11 = "hidden";
                string fees12 = "hidden";
                string fees13 = "hidden";
                string fees14 = "hidden";
                string fees15 = "hidden";

                //===============
                string dfees1 = "";
                string dfees2 = "";
                string dfees3 = "";
                string dfees4 = "";
                string dfees5 = "";
                string dfees6 = "";
                string dfees7 = "";
                string dfees8 = "";
                string dfees9 = "";
                string dfees10 = "";
                string dfees11 = "";
                string dfees12 = "";
                string dfees13 = "";
                string dfees14 = "";
                string dfees15 = "";


                //============================
                double Paidfees1 = 0;
                double Paidfees2 = 0;
                double Paidfees3 = 0;
                double Paidfees4 = 0;
                double Paidfees5 = 0;
                double Paidfees6 = 0;
                double Paidfees7 = 0;
                double Paidfees8 = 0;
                double Paidfees9 = 0;
                double Paidfees10 = 0;
                double Paidfees11 = 0;
                double Paidfees12 = 0;
                double Paidfees13 = 0;
                double Paidfees14 = 0;
                double Paidfees15 = 0;

                foreach (DataRow dr in dt.Rows)
                {
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

                    DataTable dts = mycode.FillData(querys);
                    if (dts.Rows.Count != 0)
                    {
                        int i = 0;
                        foreach (DataRow drr in dts.Rows)
                        {
                            string fees_amt = get_fees_amt(drr["content_id"].ToString(), dr["adno"].ToString(), "0", Type, dr["slipno"].ToString());
                            double paidFesAmt = 0;
                            string[] stringSeparatorss = new string[] { "/" };
                            string[] arrs = fees_amt.Split(stringSeparatorss, StringSplitOptions.None);
                            fees_amt = arrs[0];
                            paidFesAmt = My.toDouble(arrs[1]);


                            if (i == 0)
                            {
                                fees1 = fees_amt;
                                dfees1 = fees_amt;
                                Paidfees1 = paidFesAmt;
                            }
                            if (i == 1)
                            {
                                fees2 = fees_amt;
                                dfees2 = fees_amt;
                                Paidfees2 = paidFesAmt;
                            }
                            if (i == 2)
                            {
                                fees3 = fees_amt;
                                dfees3 = fees_amt;
                                Paidfees3 = paidFesAmt;
                            }
                            if (i == 3)
                            {
                                fees4 = fees_amt;
                                dfees4 = fees_amt;
                                Paidfees4 = paidFesAmt;
                            }
                            if (i == 4)
                            {
                                fees5 = fees_amt;
                                dfees5 = fees_amt;
                                Paidfees5 = paidFesAmt;
                            }
                            if (i == 5)
                            {
                                fees6 = fees_amt;
                                dfees6 = fees_amt;
                                Paidfees6 = paidFesAmt;
                            }
                            if (i == 6)
                            {
                                fees7 = fees_amt;
                                dfees7 = fees_amt;
                                Paidfees7 = paidFesAmt;
                            }
                            if (i == 7)
                            {
                                fees8 = fees_amt;
                                dfees8 = fees_amt;
                                Paidfees8 = paidFesAmt;
                            }
                            if (i == 8)
                            {
                                fees9 = fees_amt;
                                dfees9 = fees_amt;
                                Paidfees9 = paidFesAmt;
                            }
                            if (i == 9)
                            {
                                fees10 = fees_amt;
                                dfees10 = fees_amt;
                                Paidfees10 = paidFesAmt;
                            }
                            if (i == 10)
                            {
                                fees11 = fees_amt;
                                dfees11 = fees_amt;
                                Paidfees11 = paidFesAmt;
                            }
                            if (i == 11)
                            {
                                fees12 = fees_amt;
                                dfees12 = fees_amt;
                                Paidfees12 = paidFesAmt;
                            }
                            if (i == 12)
                            {
                                fees13 = fees_amt;
                                dfees13 = fees_amt;
                                Paidfees13 = paidFesAmt;
                            }
                            if (i == 13)
                            {
                                fees14 = fees_amt;
                                dfees14 = fees_amt;
                                Paidfees15 = paidFesAmt;
                            }
                            if (i == 14)
                            {
                                fees15 = fees_amt;
                                dfees15 = fees_amt;
                                Paidfees15 = paidFesAmt;
                            }
                            i++;
                        }
                    }


                    double ttlFinalFeeS = My.toDouble(fees1) + My.toDouble(fees2) + My.toDouble(fees3) + My.toDouble(fees4) + My.toDouble(fees5) + My.toDouble(fees6) + My.toDouble(fees7) + My.toDouble(fees8) + My.toDouble(fees9) + My.toDouble(fees10) + My.toDouble(fees11) + My.toDouble(fees12) + My.toDouble(fees13) + My.toDouble(fees14) + My.toDouble(fees15);
                    double ttlFinalPaidFeeS = My.toDouble(Paidfees1) + My.toDouble(Paidfees2) + My.toDouble(Paidfees3) + My.toDouble(Paidfees4) + My.toDouble(Paidfees5) + My.toDouble(Paidfees6) + My.toDouble(Paidfees7) + My.toDouble(Paidfees8) + My.toDouble(Paidfees9) + My.toDouble(Paidfees10) + My.toDouble(Paidfees11) + My.toDouble(Paidfees12) + My.toDouble(Paidfees13) + My.toDouble(Paidfees14) + My.toDouble(Paidfees15);
                    double rest_amount = (ttlFinalFeeS - ttlFinalPaidFeeS);
                    string months = get_months_of_invoice(dr["slipno"].ToString());
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

                        //==========================
                        showFeeS1 = fees1,
                        showFeeS2 = fees2,
                        showFeeS3 = fees3,
                        showFeeS4 = fees4,
                        showFeeS5 = fees5,
                        showFeeS6 = fees6,
                        showFeeS7 = fees7,
                        showFeeS8 = fees8,
                        showFeeS9 = fees9,
                        showFeeS10 = fees10,
                        showFeeS11 = fees11,
                        showFeeS12 = fees12,
                        showFeeS13 = fees13,
                        showFeeS14 = fees14,
                        showFeeS15 = fees15,

                        //=======================
                        DshowFeeS1 = My.toDouble(dfees1).ToString("0.00"),
                        DshowFeeS2 = My.toDouble(dfees2).ToString("0.00"),
                        DshowFeeS3 = My.toDouble(dfees3).ToString("0.00"),
                        DshowFeeS4 = My.toDouble(dfees4).ToString("0.00"),
                        DshowFeeS5 = My.toDouble(dfees5).ToString("0.00"),
                        DshowFeeS6 = My.toDouble(dfees6).ToString("0.00"),
                        DshowFeeS7 = My.toDouble(dfees7).ToString("0.00"),
                        DshowFeeS8 = My.toDouble(dfees8).ToString("0.00"),
                        DshowFeeS9 = My.toDouble(dfees9).ToString("0.00"),
                        DshowFeeS10 = My.toDouble(dfees10).ToString("0.00"),
                        DshowFeeS11 = My.toDouble(dfees11).ToString("0.00"),
                        DshowFeeS12 = My.toDouble(dfees12).ToString("0.00"),
                        DshowFeeS13 = My.toDouble(dfees13).ToString("0.00"),
                        DshowFeeS14 = My.toDouble(dfees14).ToString("0.00"),
                        DshowFeeS15 = My.toDouble(dfees15).ToString("0.00"),

                        TotaLFeeS = ttlFinalFeeS.ToString("0.00"),
                        TtlFinalPaidFeeS = ttlFinalPaidFeeS.ToString("0.00"),
                        Rest_amount = rest_amount.ToString("0.00"),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts));
            }
        }

        private string get_months_of_invoice(string slipno)
        {
            string month_name = "";
            DataTable dt = My.dataTable("select DISTINCT Month from dbo.[Monthly_Fee_Collection_Slip] where slipno='" + slipno + "'");
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
                querys = "select isnull(sum(convert(float, payable)),0) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + admission_no + "' and content_id='" + content_id + "' and (parameter='" + Type + "'  or parameter='HostelMonthlyFee') and slipno='" + slip_no + "'";
            }
            else
            {
                querys = "select isnull(sum(convert(float, payable)),0) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + admission_no + "' and content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelAnnualFee')  and slipno='" + slip_no + "'";
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
            public string showFeeS1 { get; set; }
            public string showFeeS2 { get; set; }
            public string showFeeS3 { get; set; }
            public string showFeeS4 { get; set; }
            public string showFeeS5 { get; set; }
            public string showFeeS6 { get; set; }
            public string showFeeS7 { get; set; }
            public string showFeeS8 { get; set; }
            public string showFeeS9 { get; set; }
            public string showFeeS10 { get; set; }
            public string showFeeS11 { get; set; }
            public string showFeeS12 { get; set; }
            public string showFeeS13 { get; set; }
            public string showFeeS14 { get; set; }
            public string showFeeS15 { get; set; }

            //=================
            public string DshowFeeS1 { get; set; }
            public string DshowFeeS2 { get; set; }
            public string DshowFeeS3 { get; set; }
            public string DshowFeeS4 { get; set; }
            public string DshowFeeS5 { get; set; }
            public string DshowFeeS6 { get; set; }
            public string DshowFeeS7 { get; set; }
            public string DshowFeeS8 { get; set; }
            public string DshowFeeS9 { get; set; }
            public string DshowFeeS10 { get; set; }
            public string DshowFeeS11 { get; set; }
            public string DshowFeeS12 { get; set; }
            public string DshowFeeS13 { get; set; }
            public string DshowFeeS14 { get; set; }
            public string DshowFeeS15 { get; set; }

            public string TotaLFeeS { get; set; }
            public string TotaLFinaLFeeS { get; set; }


            public string TotaLFinaLPaidFeeS { get; set; }
            public string TotaLFinaLRestFeeS { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head_amts_final> Show_of_day_end_report_head_amts_final = new List<Fetch_Details_of_day_end_report_head_amts_final>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_final_amts(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee')";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee')";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee')";
            }
            else
            {
                query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee')";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string fees1 = "hidden";
                string fees2 = "hidden";
                string fees3 = "hidden";
                string fees4 = "hidden";
                string fees5 = "hidden";
                string fees6 = "hidden";
                string fees7 = "hidden";
                string fees8 = "hidden";
                string fees9 = "hidden";
                string fees10 = "hidden";
                string fees11 = "hidden";
                string fees12 = "hidden";
                string fees13 = "hidden";
                string fees14 = "hidden";
                string fees15 = "hidden";

                //===============================
                string Dfees1 = "";
                string Dfees2 = "";
                string Dfees3 = "";
                string Dfees4 = "";
                string Dfees5 = "";
                string Dfees6 = "";
                string Dfees7 = "";
                string Dfees8 = "";
                string Dfees9 = "";
                string Dfees10 = "";
                string Dfees11 = "";
                string Dfees12 = "";
                string Dfees13 = "";
                string Dfees14 = "";
                string Dfees15 = "";

                double Paidfees1 = 0;
                double Paidfees2 = 0;
                double Paidfees3 = 0;
                double Paidfees4 = 0;
                double Paidfees5 = 0;
                double Paidfees6 = 0;
                double Paidfees7 = 0;
                double Paidfees8 = 0;
                double Paidfees9 = 0;
                double Paidfees10 = 0;
                double Paidfees11 = 0;
                double Paidfees12 = 0;
                double Paidfees13 = 0;
                double Paidfees14 = 0;
                double Paidfees15 = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    string querys = "";
                    if (Session_id == "0" && Class_id == "0")
                    {
                        querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
                    }
                    else if (Session_id == "0" && Class_id != "0")
                    {
                        querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
                    }
                    else if (Session_id != "0" && Class_id == "0")
                    {
                        querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
                    }
                    else
                    {
                        querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
                    }
                    DataTable dts = mycode.FillData(querys);
                    if (dts.Rows.Count != 0)
                    {
                        int i = 0;
                        foreach (DataRow drr in dts.Rows)
                        {
                            string fees_amt = get_final_fees_amt(drr["content_id"].ToString(), Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name);
                            double paidFesAmt = 0;
                            string[] stringSeparatorss = new string[] { "/" };
                            string[] arrs = fees_amt.Split(stringSeparatorss, StringSplitOptions.None);
                            fees_amt = arrs[0];
                            paidFesAmt = My.toDouble(arrs[1]);


                            if (i == 0)
                            {
                                fees1 = fees_amt;
                                Dfees1 = fees_amt;
                                Paidfees1 = paidFesAmt;
                            }
                            if (i == 1)
                            {
                                fees2 = fees_amt;
                                Dfees2 = fees_amt;
                                Paidfees2 = paidFesAmt;
                            }
                            if (i == 2)
                            {
                                fees3 = fees_amt;
                                Dfees3 = fees_amt;
                                Paidfees3 = paidFesAmt;
                            }
                            if (i == 3)
                            {
                                fees4 = fees_amt;
                                Dfees4 = fees_amt;
                                Paidfees4 = paidFesAmt;
                            }
                            if (i == 4)
                            {
                                fees5 = fees_amt;
                                Dfees5 = fees_amt;
                                Paidfees5 = paidFesAmt;
                            }
                            if (i == 5)
                            {
                                fees6 = fees_amt;
                                Dfees6 = fees_amt;
                                Paidfees6 = paidFesAmt;
                            }
                            if (i == 6)
                            {
                                fees7 = fees_amt;
                                Dfees7 = fees_amt;
                                Paidfees7 = paidFesAmt;
                            }
                            if (i == 7)
                            {
                                fees8 = fees_amt;
                                Dfees8 = fees_amt;
                                Paidfees8 = paidFesAmt;
                            }
                            if (i == 8)
                            {
                                fees9 = fees_amt;
                                Dfees9 = fees_amt;
                                Paidfees9 = paidFesAmt;
                            }
                            if (i == 9)
                            {
                                fees10 = fees_amt;
                                Dfees10 = fees_amt;
                                Paidfees10 = paidFesAmt;
                            }
                            if (i == 10)
                            {
                                fees11 = fees_amt;
                                Dfees11 = fees_amt;
                                Paidfees11 = paidFesAmt;
                            }
                            if (i == 11)
                            {
                                fees12 = fees_amt;
                                Paidfees12 = paidFesAmt;
                            }
                            if (i == 12)
                            {
                                fees13 = fees_amt;
                                Dfees13 = fees_amt;
                                Paidfees13 = paidFesAmt;
                            }
                            if (i == 13)
                            {
                                fees14 = fees_amt;
                                Dfees14 = fees_amt;
                                Paidfees14 = paidFesAmt;
                            }
                            if (i == 14)
                            {
                                fees15 = fees_amt;
                                Dfees15 = fees_amt;
                                Paidfees15 = paidFesAmt;
                            }
                            i++;
                        }
                    }


                    double ttlFinalFeeS = My.toDouble(fees1) + My.toDouble(fees2) + My.toDouble(fees3) + My.toDouble(fees4) + My.toDouble(fees5) + My.toDouble(fees6) + My.toDouble(fees7) + My.toDouble(fees8) + My.toDouble(fees9) + My.toDouble(fees10) + My.toDouble(fees11) + My.toDouble(fees12) + My.toDouble(fees13) + My.toDouble(fees14) + My.toDouble(fees15);
                    double ttlFinalPaidFeeS = My.toDouble(Paidfees1) + My.toDouble(Paidfees2) + My.toDouble(Paidfees3) + My.toDouble(Paidfees4) + My.toDouble(Paidfees5) + My.toDouble(Paidfees6) + My.toDouble(Paidfees7) + My.toDouble(Paidfees8) + My.toDouble(Paidfees9) + My.toDouble(Paidfees10) + My.toDouble(Paidfees11) + My.toDouble(Paidfees12) + My.toDouble(Paidfees13) + My.toDouble(Paidfees14) + My.toDouble(Paidfees15);
                    double restAmount = (ttlFinalFeeS - ttlFinalPaidFeeS);
                    Show_of_day_end_report_head_amts_final.Add(new Fetch_Details_of_day_end_report_head_amts_final
                    {
                        showFeeS1 = fees1,
                        showFeeS2 = fees2,
                        showFeeS3 = fees3,
                        showFeeS4 = fees4,
                        showFeeS5 = fees5,
                        showFeeS6 = fees6,
                        showFeeS7 = fees7,
                        showFeeS8 = fees8,
                        showFeeS9 = fees9,
                        showFeeS10 = fees10,
                        showFeeS11 = fees11,
                        showFeeS12 = fees12,
                        showFeeS13 = fees13,
                        showFeeS14 = fees14,
                        showFeeS15 = fees15,

                        //=========================
                        DshowFeeS1 = My.toDouble(Dfees1).ToString("0.00"),
                        DshowFeeS2 = My.toDouble(Dfees2).ToString("0.00"),
                        DshowFeeS3 = My.toDouble(Dfees3).ToString("0.00"),
                        DshowFeeS4 = My.toDouble(Dfees4).ToString("0.00"),
                        DshowFeeS5 = My.toDouble(Dfees5).ToString("0.00"),
                        DshowFeeS6 = My.toDouble(Dfees6).ToString("0.00"),
                        DshowFeeS7 = My.toDouble(Dfees7).ToString("0.00"),
                        DshowFeeS8 = My.toDouble(Dfees8).ToString("0.00"),
                        DshowFeeS9 = My.toDouble(Dfees9).ToString("0.00"),
                        DshowFeeS10 = My.toDouble(Dfees10).ToString("0.00"),
                        DshowFeeS11 = My.toDouble(Dfees11).ToString("0.00"),
                        DshowFeeS12 = My.toDouble(Dfees12).ToString("0.00"),
                        DshowFeeS13 = My.toDouble(Dfees13).ToString("0.00"),
                        DshowFeeS14 = My.toDouble(Dfees14).ToString("0.00"),
                        DshowFeeS15 = My.toDouble(Dfees15).ToString("0.00"),

                        TotaLFinaLFeeS = ttlFinalFeeS.ToString("0.00"),
                        TotaLFinaLPaidFeeS = ttlFinalPaidFeeS.ToString("0.00"),
                        TotaLFinaLRestFeeS = restAmount.ToString("0.00"),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts_final));
            }
        }

        private string get_final_fees_amt(string content_id, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
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
                paids_amt = "0/0";
            }
            else
            {
                paids_amt = dt.Rows[0]["payable_amt"].ToString() + "/" + dt.Rows[0]["Paid_amt"].ToString();
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
            ad.Fill(ds, "Complain_chat");
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
    }
}
