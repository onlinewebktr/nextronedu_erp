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
    /// Summary description for discount_report
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class discount_report : System.Web.Services.WebService
    {
        #region MonthlY



        My mycode = new My();
        public class Fetch_Details_of_day_end_report_head_amts
        {
            public string Payment_date { get; set; }
            public string Slip_no { get; set; }
            public string Student_status { get; set; }
            public string Student_name { get; set; }
            public string Father_name { get; set; }
            public string Class_name { get; set; }
            public string Sections { get; set; }
            public string Admission_no { get; set; }
            public string Roll_no { get; set; }
            public string Payment_mode { get; set; }
            public string PaidFesAmt { get; set; }
            public string IssuedBy { get; set; }
            public string Student_Discunt_Type { get; set; }
            public string Student_Discunt_Remarks { get; set; }
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
            if (Type == "0")
            {
                if (Class_id == "0")
                {
                    query = "select distinct t2.Transfer_Status,t1.Class_id,t1.admission_no,t2.class,t2.rollnumber,t2.Section,t2.studentname,t2.fathername,t1.Student_Discunt_Type_id,t3.Discunt_Type,t1.Discount_on,t1.Student_Discunt_Remarks,t1.parameter_id from Discount_Master t1 join admission_registor t2 on t1.admission_no=t2.admissionserialnumber join STUDENT_DISCUNT_TYPE t3 on t3.Student_Discunt_Type_id=t1.Student_Discunt_Type_id where t1.session='" + Session_name + "' and t2.session='" + Session_name + "' group by t2.Transfer_Status,t1.Class_id,t1.admission_no,t2.class,t2.rollnumber,t2.Section,t2.studentname,t2.fathername,t1.Student_Discunt_Type_id,t1.Student_Discunt_Remarks,t3.Discunt_Type,t1.Discount_on,t1.parameter_id";
                }
                else
                {
                    query = "select distinct t2.Transfer_Status,t1.Class_id,t1.admission_no,t2.class,t2.rollnumber,t2.Section,t2.studentname,t2.fathername,t1.Student_Discunt_Type_id,t3.Discunt_Type,t1.Discount_on,t1.Student_Discunt_Remarks,t1.parameter_id from Discount_Master t1 join admission_registor t2 on t1.admission_no=t2.admissionserialnumber join STUDENT_DISCUNT_TYPE t3 on t3.Student_Discunt_Type_id=t1.Student_Discunt_Type_id where t1.session='" + Session_name + "' and t2.session='" + Session_name + "' and t2.Class_id='" + Class_id + "' group by t2.Transfer_Status,t1.Class_id,t1.admission_no,t2.class,t2.rollnumber,t2.Section,t2.studentname,t2.fathername,t1.Student_Discunt_Type_id,t1.Student_Discunt_Remarks,t3.Discunt_Type,t1.Discount_on,t1.parameter_id";
                }
            }
            else
            {
                if (Class_id == "0")
                {
                    query = "select distinct t2.Transfer_Status,t1.Class_id,t1.admission_no,t2.class,t2.rollnumber,t2.Section,t2.studentname,t2.fathername,t1.Student_Discunt_Type_id,t3.Discunt_Type,t1.Discount_on,t1.Student_Discunt_Remarks,t1.parameter_id from Discount_Master t1 join admission_registor t2 on t1.admission_no=t2.admissionserialnumber join STUDENT_DISCUNT_TYPE t3 on t3.Student_Discunt_Type_id=t1.Student_Discunt_Type_id where t1.session='" + Session_name + "' and t2.session='" + Session_name + "' and t2.Transfer_Status='" + Type + "' group by t2.Transfer_Status,t1.Class_id,t1.admission_no,t2.class,t2.rollnumber,t2.Section,t2.studentname,t2.fathername,t1.Student_Discunt_Type_id,t3.Discunt_Type,t1.Discount_on,t1.Student_Discunt_Remarks,t1.parameter_id";
                }
                else
                {
                    query = "select distinct t2.Transfer_Status,t1.Class_id,t1.admission_no,t2.class,t2.rollnumber,t2.Section,t2.studentname,t2.fathername,t1.Student_Discunt_Type_id,t3.Discunt_Type,t1.Discount_on,t1.Student_Discunt_Remarks,t1.parameter_id from Discount_Master t1 join admission_registor t2 on t1.admission_no=t2.admissionserialnumber join STUDENT_DISCUNT_TYPE t3 on t3.Student_Discunt_Type_id=t1.Student_Discunt_Type_id where t1.session='" + Session_name + "' and t2.session='" + Session_name + "' and t2.Class_id='" + Class_id + "'  and t2.Transfer_Status='" + Type + "' group by t2.Transfer_Status,t1.Class_id,t1.admission_no,t2.class,t2.rollnumber,t2.Section,t2.studentname,t2.fathername,t1.Student_Discunt_Type_id,t3.Discunt_Type,t1.Discount_on,t1.Student_Discunt_Remarks,t1.parameter_id";
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
                    string student_status = dr["Transfer_Status"].ToString();
                    if (student_status == "NT")
                    {
                        student_status = "Old";
                    }
                    List<MyFeeReport> MBdetails = findmyCollectionDt(dr["admission_no"].ToString(), Session_id, dr["Class_id"].ToString(), FromDate, ToDate, Type, Session_name, Class_name, dr["Student_Discunt_Type_id"].ToString(), dr["parameter_id"].ToString());

                    string paids_amt = get_ttl_amt_of_a_std(dr["admission_no"].ToString(), Session_id, dr["Class_id"].ToString(), FromDate, ToDate, Type, Session_name, Class_name, dr["Student_Discunt_Type_id"].ToString());
                    Show_of_day_end_report_head_amts.Add(new Fetch_Details_of_day_end_report_head_amts
                    {
                        Student_status = student_status,
                        Student_name = dr["studentname"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Sections = dr["Section"].ToString(),
                        Admission_no = dr["admission_no"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),
                        //Payment_mode = dr["Payment_mode"].ToString(),
                        PaidFesAmt = paids_amt,

                        //IssuedBy = dr["By_user_name"].ToString(),
                        //==========================

                        MyFeeReportItem = MBdetails,
                        Student_Discunt_Type = dr["Discunt_Type"].ToString(),
                        Student_Discunt_Remarks = dr["Student_Discunt_Remarks"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts));
            }
        }

        private string get_ttl_amt_of_a_std(string Admission_no, string session_id, string class_id, string fromDate, string toDate, string type, string session_name, string class_name, string Student_Discunt_Type_id)
        {
            string querys = "select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + session_name + "' and admission_no='" + Admission_no + "' and Class_id='" + class_id + "' and Student_Discunt_Type_id='" + Student_Discunt_Type_id + "'";
            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count > 0)
            {
                paids_amt = dt.Rows[0]["Discount"].ToString();
            }
            return paids_amt;
        }

        private List<MyFeeReport> findmyCollectionDt(string Admission_no, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Student_Discunt_Type_id, string parmatersid)
        {
            List<MyFeeReport> MyFeeReportItem = new List<MyFeeReport>();



            string monthfee = get_month_fee(Admission_no, Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name, Student_Discunt_Type_id, parmatersid);

            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = monthfee.Split(stringSeparatorss, StringSplitOptions.None);
            string totalfee = arrs[0];
            string totaldiscount = arrs[1];
            string totalpablefee = arrs[2];


            string admissionfee = get_admission_fee(Admission_no, Session_id, Class_id, FromDate, ToDate, Type, Session_name, Class_name, Student_Discunt_Type_id, parmatersid);

            string[] stringSeparatorss1 = new string[] { "/" };
            string[] arrs1 = admissionfee.Split(stringSeparatorss1, StringSplitOptions.None);

            string totaladdfee = arrs1[0];
            string totaladddiscount = arrs1[1];
            string totaladdpablefee = arrs1[2];



            MyFeeReportItem.Add(new MyFeeReport
            {
                HeadFees = totalfee,
            });

            MyFeeReportItem.Add(new MyFeeReport
            {
                HeadFees = totaldiscount,
            });

            MyFeeReportItem.Add(new MyFeeReport
            {
                HeadFees = totalpablefee,
            });


            MyFeeReportItem.Add(new MyFeeReport
            {
                HeadFees = totaladdfee,
            });

            MyFeeReportItem.Add(new MyFeeReport
            {
                HeadFees = totaladddiscount,
            });

            MyFeeReportItem.Add(new MyFeeReport
            {
                HeadFees = totaladdpablefee,
            });



            return MyFeeReportItem;
        }

        private string get_admission_fee(string admission_no, string session_id, string class_id, string fromDate, string toDate, string type, string session_name, string class_name, string student_Discunt_Type_id, string parmatersid)
        {
            string query = "";
            string totalfee = "0", totalDiscount = "0";
            double payableamount = 0;



            if (parmatersid == "1")//AdmissionFee
            {
                query = "select(select  isnull(sum(convert(float, amount)), 0)  from Fee_master_content_wise where session = '" + session_name + "' and class_id = '" + class_id + "' and parameter_id = '" + parmatersid + "') as totalamount ,isnull(sum(convert(float, disc_amount)), 0) Discount from Discount_Master where session = '" + session_name + "' and admission_no = '" + admission_no + "' and Class_id = '" + class_id + "' and    Discount_on in ('Admission', 'Annual') and Student_Discunt_Type_id = '" + student_Discunt_Type_id + "'";
            }
            else if (parmatersid == "2")//AnnualFee
            {
                query = "select(select  isnull(sum(convert(float, amount)), 0)  from Fee_master_content_wise where session = '" + session_name + "' and class_id = '" + class_id + "' and parameter_id = '" + parmatersid + "') as totalamount ,isnull(sum(convert(float, disc_amount)), 0) Discount from Discount_Master where session = '" + session_name + "' and admission_no = '" + admission_no + "' and Class_id = '" + class_id + "' and    Discount_on in ('Admission', 'Annual') and Student_Discunt_Type_id = '" + student_Discunt_Type_id + "'";

            }
            else if (parmatersid == "5")//HostelAdmissionFee
            {
                query = "select(select  isnull(sum(convert(float, amount)), 0)  from Fee_master_content_wise where session = '" + session_name + "' and class_id = '" + class_id + "' and parameter_id = '" + parmatersid + "') as totalamount ,isnull(sum(convert(float, disc_amount)), 0) Discount from Discount_Master where session = '" + session_name + "' and admission_no = '" + admission_no + "' and Class_id = '" + class_id + "' and    Discount_on in ('Admission', 'Annual') and Student_Discunt_Type_id = '" + student_Discunt_Type_id + "'";
            }
            else if (parmatersid == "6")//HostelAnnualFee
            {
                query = "select(select  isnull(sum(convert(float, amount)), 0)  from Fee_master_content_wise where session = '" + session_name + "' and class_id = '" + class_id + "' and parameter_id = '" + parmatersid + "') as totalamount ,isnull(sum(convert(float, disc_amount)), 0) Discount from Discount_Master where session = '" + session_name + "' and admission_no = '" + admission_no + "' and Class_id = '" + class_id + "' and    Discount_on in ('Admission', 'Annual') and Student_Discunt_Type_id = '" + student_Discunt_Type_id + "'";
            }
            try
            {
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {

                }
                else
                {
                    totalfee = dt.Rows[0]["totalamount"].ToString();
                    totalDiscount = dt.Rows[0]["Discount"].ToString();
                    payableamount = My.toDouble(dt.Rows[0]["totalamount"].ToString()) - My.toDouble(dt.Rows[0]["Discount"].ToString());
                }
            }
            catch
            {

            }
            
            return totalfee + "/" + totalDiscount + "/" + payableamount;
        }

        private string get_month_fee(string admission_no, string session_id, string class_id, string fromDate, string toDate, string type, string session_name, string class_name, string student_Discunt_Type_id, string parmatersid)
        {
            string query="";
            string totalfee = "0", totalDiscount = "0";
            double payableamount = 0;
            if (parmatersid == "3") //HostelMonthlyFee = 3
            {
                query = "select(select  isnull(sum(convert(float, amount)), 0)  from Fee_master_content_wise where session = '" + session_name + "' and class_id = '" + class_id + "' and parameter_id = '" + parmatersid + "') as totalamount ,isnull(sum(convert(float, disc_amount)), 0) Discount from Discount_Master where session = '" + session_name + "' and admission_no = '" + admission_no + "' and Class_id = '" + class_id + "' and Discount_on = 'Monthly' and Student_Discunt_Type_id = '" + student_Discunt_Type_id + "'";

            }
            else if (parmatersid == "4")//MonthlyFee=4
            {
                query = "select(select  isnull(sum(convert(float, amount)), 0)  from Fee_master_content_wise where session = '" + session_name + "' and class_id = '" + class_id + "' and parameter_id = '" + parmatersid + "') as totalamount ,isnull(sum(convert(float, disc_amount)), 0) Discount from Discount_Master where session = '" + session_name + "' and admission_no = '" + admission_no + "' and Class_id = '" + class_id + "' and Discount_on = 'Monthly' and Student_Discunt_Type_id = '" + student_Discunt_Type_id + "'";
            }
            try
            {
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {

                }
                else
                {
                    totalfee = dt.Rows[0]["totalamount"].ToString();
                    totalDiscount = dt.Rows[0]["Discount"].ToString();
                    payableamount = My.toDouble(dt.Rows[0]["totalamount"].ToString()) - My.toDouble(dt.Rows[0]["Discount"].ToString());
                }
            }
            catch
            {

            }
            return totalfee + "/" + totalDiscount + "/" + payableamount;

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
            public string Student_Discunt_Type { get; set; }
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
            string query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip]";
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
                        MyFeeReportOverAllItem = MBdetails,


                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts_final));
            }
        }

        private string get_ttl_amt_of_overall(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string querys = "";
            if (Type == "0")
            {
                if (Class_id == "0")
                {
                    querys = "select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "' and admission_no in (select admissionserialnumber from admission_registor where Session_id='" + Session_id + "')";
                }
                else
                {
                    querys = "select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "'  and Class_id='" + Class_id + "' and admission_no in (select admissionserialnumber from admission_registor where Session_id='" + Session_id + "' and  Class_id='" + Class_id + "')";
                }
            }
            else
            {
                if (Class_id == "0")
                {
                    querys = "select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "'  and admission_no in (select admissionserialnumber from admission_registor where Transfer_Status='" + Type + "' and Session_id='" + Session_id + "')";
                }
                else
                {
                    querys = "select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "' and admission_no in (select admissionserialnumber from admission_registor where Transfer_Status='" + Type + "' and Session_id='" + Session_id + "'  and Class_id='" + Class_id + "')";
                }
            }
            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Discount"].ToString();
            }
            return paids_amt;
        }

        private List<MyFeeReportOverAll> FindMyFeeReportOverAllDt(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            List<MyFeeReportOverAll> MyFeeReportOverAllItem = new List<MyFeeReportOverAll>();
            string querys = "";
            if (Type == "0")
            {
                if (Class_id == "0")
                {


                    querys = "select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "' and Discount_on='Monthly'  and admission_no in (select admissionserialnumber from admission_registor where Session_id='" + Session_id + "') union all select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "' and Discount_on in ('Admission','Annual') and admission_no in (select admissionserialnumber from admission_registor where Session_id='" + Session_id + "')";
                }
                else
                {
                    querys = "select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "'  and Class_id='" + Class_id + "' and Discount_on='Monthly' and admission_no in (select admissionserialnumber from admission_registor where Session_id='" + Session_id + "' and Class_id='" + Class_id + "') union all select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "' and Class_id='" + Class_id + "' and Discount_on in ('Admission','Annual') and admission_no in (select admissionserialnumber from admission_registor where Session_id='" + Session_id + "' and Class_id='" + Class_id + "')";
                }
            }
            else
            {
                if (Class_id == "0")
                {
                    querys = "select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "' and Discount_on='Monthly'  and admission_no in (select admissionserialnumber from admission_registor where Transfer_Status='" + Type + "' and Session_id='" + Session_id + "' ) union all select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "' and Discount_on in ('Admission','Annual')  and admission_no in (select admissionserialnumber from admission_registor where Transfer_Status='" + Type + "' and Session_id='" + Session_id + "')";
                }
                else
                {
                    querys = "select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "'  and Class_id='" + Class_id + "'  and Class_id='" + Class_id + "' and Discount_on='Monthly' and admission_no in (select admissionserialnumber from admission_registor where Transfer_Status='" + Type + "' and Class_id='" + Class_id + "'  and Session_id='" + Session_id + "') union all select  isnull(sum(convert(float, disc_amount)),0) Discount from Discount_Master where session='" + Session_name + "' and Class_id='" + Class_id + "' and Discount_on in ('Admission','Annual')  and admission_no in (select admissionserialnumber from admission_registor where Transfer_Status='" + Type + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "')";
                }
            }

            DataTable dtD = My.dataTable(querys);
            if (dtD.Rows.Count > 0)
            {
                foreach (DataRow drr in dtD.Rows)
                {
                    MyFeeReportOverAllItem.Add(new MyFeeReportOverAll
                    {
                        HeadFees = drr["Discount"].ToString(),
                    });
                }
            }
            return MyFeeReportOverAllItem;
        }



        private string get_final_fees_amt_contentwise(string Content, string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where (class='" + Class_id + "' or class='" + Class_name + "') and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                query = "select isnull(sum(convert(float, payable)),0) as payable_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and (class='" + Class_id + "' or class='" + Class_name + "') and Content='" + Content + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
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
        public void fetch_report_modewise(string Session_id, string Class_id, string FromDate, string ToDate, string Type, string Session_name, string Class_name, string Collection_type)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where t1.session='" + Session_name + "' and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where (t1.class='" + Class_id + "' or t1.class='" + Class_name + "') and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode";
            }
            else
            {
                query = "select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt,Payment_mode from (select paid as Paid_amt,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip  t1 where t1.session='" + Session_name + "' and (t1.class='" + Class_id + "' or t1.class='" + Class_name + "')  and t1.Idate>='" + My.DateConvertToIdate(FromDate) + "' and t1.Idate<='" + My.DateConvertToIdate(ToDate) + "') t group by Payment_mode";
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
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_FeeReportPaymentMode));
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
                query = @"select distinct mode from Student_Payment_History where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select distinct mode from Student_Payment_History where  Session='" + Session_name + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select distinct mode from Student_Payment_History where  Class_id='" + Class_id + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }
            else
            {
                query = @"select distinct mode from Student_Payment_History where  Session='" + Session_name + "' and   Class_id='" + Class_id + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
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
                query = "select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History  where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by User_name asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = "select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History where Session='" + Session_name + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by User_name asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = "select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History where Class_id='" + Class_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by User_name asc";
            }
            else
            {
                query = "select * from (select distinct user_id,(select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "') t order by User_name asc";
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
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_user_amt));
            }
        }

        private string get_ttl_amt_of_a_user(string user_id, string Session_id, string Class_id, string FromDate, string ToDate, string type, string Session_name, string class_name)
        {
            string query = "";
            if (Session_id == "0" && Class_id == "0")
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Class_id='" + Class_id + "' and user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and user_id='" + user_id + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
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
                query = @"select distinct mode from Student_Payment_History where Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select distinct mode from Student_Payment_History where  Session='" + Session_name + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select distinct mode from Student_Payment_History where  Class_id='" + Class_id + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
            }
            else
            {
                query = @"select distinct mode from Student_Payment_History where  Session='" + Session_name + "' and   Class_id='" + Class_id + "' and  Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + @"' group by mode order by mode asc";
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
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id != "0" && Class_id == "0")
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else if (Session_id == "0" && Class_id != "0")
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Class_id='" + Class_id + "' and user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
            }
            else
            {
                query = @"select isnull(sum(convert(float, Amount)),0) as Total_paid from Student_Payment_History where Session='" + Session_name + "' and Class_id='" + Class_id + "' and user_id='" + user_id + "' and mode='" + mode + "' and Idate>='" + My.DateConvertToIdate(FromDate) + "' and Idate<='" + My.DateConvertToIdate(ToDate) + "'";
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
