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

namespace school_web.Admin.slip.webServices
{
    /// <summary>
    /// Summary description for paymentDetail
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class paymentDetail : System.Web.Services.WebService
    {
        public class MyPaymentDetails
        {
            public string SchoolName { get; set; }
            public string SchoolAddress { get; set; }
            public string Contact_no { get; set; }
            public string Logo { get; set; }


            public string Student_name { get; set; }
            public string Class_name { get; set; }
            public string Section { get; set; }
            public string Rollnumber { get; set; }
            public string Admission_no { get; set; }
            public string Father_name { get; set; }
            public string Mother_name { get; set; }
            public string Blood_group { get; set; }
            public string Date_of_birth { get; set; }
            public string Date_of_admission { get; set; }
            public string Aadharno { get; set; }
            public string Address { get; set; }
            public string Pincode { get; set; }
            public string Mobile_no { get; set; }
            public string castCaltegory { get; set; }
            public string Session_name { get; set; }
            public List<MyGetMonths> MyGetMonthsItem { get; set; }
            public List<MyGetMonthwiseFee> MyGetMonthwiseFeeItem { get; set; }
            public List<MyGetMonthwiseFeeTotal> MyGetMonthwiseFeeTotalItem { get; set; }
            public List<MyGetPaidDetails> MyGetPaidDetailsItem { get; set; }
        }

        public class MyGetMonths
        {
            public string Month_name { get; set; }
        }
        public class MyGetMonthwiseFee
        {
            public string Contents { get; set; }
            public List<MyGetMonthwiseFeeAmounts> MyGetMonthwiseFeeAmountsItem { get; set; }
        }
        public class MyGetMonthwiseFeeAmounts
        {
            public string Contents { get; set; }
        }
        public class MyGetMonthwiseFeeTotal
        {
            public string Contents { get; set; }
            public string ContentsDisc { get; set; }
            public string Fee_payable_amount { get; set; }
        }

        public class MyGetPaidDetails
        {
            public string Bill_no { get; set; }
            public string Payment_mode { get; set; }
            public string Payment_date { get; set; }
            public string Received_by { get; set; }
            public string Paid_amt { get; set; }
            public string Duesamts { get; set; }
        }

        List<MyPaymentDetails> EMySubMark = new List<MyPaymentDetails>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_payments_of_student(string Session_id, string Session_name, string Class_id, string Section, string Adm_no)
        {
            string[] stringSeparators = new string[] { "-" };
            string[] arr = Session_name.Split(stringSeparators, StringSplitOptions.None);
            string session_frst_year = arr[0];
            int s_year = My.toint(session_frst_year);

            string query = "";
            if (Adm_no == "0")
            {
                query = "select * from admission_registor where Session_id='" + Session_id + "' and Class_id='" + Class_id + "'  and Section='" + Section + "' and Status=1";
            }
            else
            {
                query = "select * from admission_registor where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Status=1 and admissionserialnumber='" + Adm_no + "'";
            }
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                DataTable dtF = My.dataTable("select * from Firm_Details");
                foreach (DataRow dr in dt.Rows)
                {
                    string category = dr["cast"].ToString();
                    if (dr["cast"].ToString() == "Select")
                    {
                        category = "";
                    }
                    List<MyGetMonths> MBdetails = findMonthDetails();
                    List<MyGetMonthwiseFee> MBFeedetails = findMonthFeeDetails(Session_id, dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString());
                    List<MyGetMonthwiseFeeTotal> MBFeedetailsTotal = findMonthFeeDetailsTotal(Session_id, dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString());
                    List<MyGetPaidDetails> MBFeePaiddetails = findpaidDetails(Session_id, Session_name, Class_id, dr["admissionserialnumber"].ToString(), s_year);
                    EMySubMark.Add(new MyPaymentDetails
                    {
                        SchoolName = dtF.Rows[0]["firm_name"].ToString(),
                        SchoolAddress = dtF.Rows[0]["address1"].ToString(),
                        Contact_no = dtF.Rows[0]["contact_no"].ToString(),
                        Logo = dtF.Rows[0]["logo"].ToString(),
                        Student_name = dr["studentname"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Section = dr["Section"].ToString(),
                        Rollnumber = dr["rollnumber"].ToString(),
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Mother_name = dr["mothername"].ToString(),
                        Blood_group = dr["blood_group"].ToString(),
                        Date_of_birth = dr["dob"].ToString(),
                        Date_of_admission = dr["dateofadmission"].ToString(),
                        Aadharno = dr["aadharno"].ToString(),
                        Address = dr["careof"].ToString(),
                        Pincode = dr["pin"].ToString(),
                        Mobile_no = dr["mobilenumber"].ToString(),
                        castCaltegory = category,
                        Session_name = Session_name,
                        MyGetMonthsItem = MBdetails,
                        MyGetMonthwiseFeeItem = MBFeedetails,
                        MyGetMonthwiseFeeTotalItem = MBFeedetailsTotal,
                        MyGetPaidDetailsItem = MBFeePaiddetails,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }

        private List<MyGetMonths> findMonthDetails()
        {
            List<MyGetMonths> MyGetMonthsItem = new List<MyGetMonths>();
            string query = "select Month from Month_Index order by Position asc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string firstThree = dr["Month"].ToString().Substring(0, 3);
                    MyGetMonthsItem.Add(new MyGetMonths
                    {
                        Month_name = firstThree,
                    });
                }
            }
            return MyGetMonthsItem;
        }


        private List<MyGetMonthwiseFee> findMonthFeeDetails(string Session_id, string Class_id, string Adm_no)
        {
            List<MyGetMonthwiseFee> MyGetMonthwiseFeeItem = new List<MyGetMonthwiseFee>();
            DataTable ftF = My.dataTable("select * from Fee_head_type_master order by Position asc");
            if (ftF.Rows.Count > 0)
            {
                foreach (DataRow drF in ftF.Rows)
                {
                    List<MyGetMonthwiseFeeAmounts> MBFeedetails = findMonthFeeDetailsAmounts(Session_id, Class_id, Adm_no, drF["Fee_content"].ToString(), drF["Fee_content_id"].ToString(), drF["Fee_content_id"].ToString());
                    MyGetMonthwiseFeeItem.Add(new MyGetMonthwiseFee
                    {
                        Contents = drF["Fee_content"].ToString(),
                        MyGetMonthwiseFeeAmountsItem = MBFeedetails,
                    });
                }
            }
            return MyGetMonthwiseFeeItem;
        }


        private List<MyGetMonthwiseFeeAmounts> findMonthFeeDetailsAmounts(string Session_id, string Class_id, string Adm_no, string content, string content_id, string parameter)
        {
            List<MyGetMonthwiseFeeAmounts> MyGetMonthwiseFeeAmountsItem = new List<MyGetMonthwiseFeeAmounts>();
            string query = "select Month from Month_Index order by Position asc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string fee_amount = get_fees(Session_id, Class_id, Adm_no, dr["Month"].ToString(), content_id);
                    if (fee_amount == "0")
                    {
                        fee_amount = "";
                    }
                    MyGetMonthwiseFeeAmountsItem.Add(new MyGetMonthwiseFeeAmounts
                    {
                        Contents = fee_amount,
                    });
                }
            }
            return MyGetMonthwiseFeeAmountsItem;
        }

        private string get_fees(string session_id, string class_id, string adm_no, string Month, string content_id)
        {
            string content_ids = " and content_id in ('0-00-0-0-0')";
            if (content_id == "Admission")
            {
                content_ids = " and parameter in ('HostelAdmissionFee','AdmissionFee','HostelAnnualFee','AnnualFee') and content='Admission Fee' and content_id not in ('1001','ADM01')";
            }
            if (content_id == "Annual")
            {
                content_ids = " and parameter in ('HostelAnnualFee','AnnualFee','HostelAdmissionFee','AdmissionFee') and content='Annual Fee' and content_id not in ('1001','ADM01')";
            }
            if (content_id == "MonthlyFee")
            {
                content_ids = " and parameter in ('MonthlyFee','HostelMonthlyFee') and content_id not in ('1001','ADM01','1002','74')";
            }
            if (content_id == "TransportFee")
            {
                content_ids = " and content_id in ('1002')";
            }
            if (content_id == "HostelFee")
            {
                content_ids = " and content_id in ('74')";
            }
            if (content_id == "MiscFee")
            {
                content_ids = " and content_id in ('1001','ADM01')";
            }
            string fee_amount = "";
            DataTable dt = My.dataTable("select isnull(sum(cast(amount as float)),0) as Fee_amount from STUDENT_WISE_DUES_AMOUNT where Session_id='" + session_id + "' and Class_id='" + class_id + "' and admission_no='" + adm_no + "' and months='" + Month + "'" + content_ids);
            if (dt.Rows.Count > 0)
            {
                fee_amount = dt.Rows[0]["Fee_amount"].ToString();
            }
            return fee_amount;
        }


        private List<MyGetMonthwiseFeeTotal> findMonthFeeDetailsTotal(string Session_id, string Class_id, string Adm_no)
        {
            List<MyGetMonthwiseFeeTotal> MyGetMonthwiseFeeTotalItem = new List<MyGetMonthwiseFeeTotal>();
            //DataTable ft = My.dataTable("select months,isnull(sum(cast(amount as float)),0) as Fee_amount,isnull(sum(cast(disc_amount as float)),0) as Fee_disc_amount,(isnull(sum(cast(amount as float)),0)-isnull(sum(cast(disc_amount as float)),0)) as Fee_payable_amount from STUDENT_WISE_DUES_AMOUNT where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + Adm_no + "' group by months");
            DataTable ft = My.dataTable("SELECT months, CASE WHEN ISNULL(Month_position, 0) = 0 THEN 1 ELSE Month_position END AS Month_position, ISNULL(SUM(CAST(amount AS float)), 0) AS Fee_amount, ISNULL(SUM(CAST(disc_amount AS float)), 0) AS Fee_disc_amount, ISNULL(SUM(CAST(amount AS float)), 0) -ISNULL(SUM(CAST(disc_amount AS float)), 0) AS Fee_payable_amount FROM STUDENT_WISE_DUES_AMOUNT WHERE Session_id = '" + Session_id + "' AND Class_id = '" + Class_id + "' AND admission_no = '" + Adm_no + "' GROUP BY months, CASE WHEN ISNULL(Month_position, 0) = 0 THEN 1 ELSE Month_position END ORDER BY CASE WHEN ISNULL(Month_position, 0) = 0 THEN 1 ELSE Month_position END ASC");
            if (ft.Rows.Count > 0)
            {
                foreach (DataRow dr in ft.Rows)
                {
                    string disc = dr["Fee_disc_amount"].ToString();
                    if (dr["Fee_disc_amount"].ToString() == "0")
                    {
                        disc = "";
                    }

                    MyGetMonthwiseFeeTotalItem.Add(new MyGetMonthwiseFeeTotal
                    {
                        Contents = dr["Fee_amount"].ToString(),
                        ContentsDisc = disc,
                        Fee_payable_amount = dr["Fee_payable_amount"].ToString(),
                    });
                }
            }
            return MyGetMonthwiseFeeTotalItem;
        }


        private List<MyGetPaidDetails> findpaidDetails(string Session_id, string Session_name, string Class_id, string Adm_No, int s_year)
        {
            List<MyGetPaidDetails> MyGetPaidDetailsItem = new List<MyGetPaidDetails>();
            string query = "select Month from Month_Index order by Position asc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string monthid = My.tomonth_numberstring(dr["Month"].ToString());
                    int pay_month = My.toint(monthid);
                    s_year = My.check_start_months(pay_month, s_year);
                    string sidate = s_year + monthid + "01";
                    string eidate = s_year + monthid + "31";
                    string bill_no = "";
                    string bill_Date = "";
                    string bill_mode = "";
                    string Received_by = "";
                    string paid_amt = "";
                    string Duesamts = "";
                    DataTable ftF = My.dataTable(@"SELECT 
    SUM(TRY_CONVERT(decimal(18, 2), t1.paid)) AS Total_Paid,

    --Distinct slip numbers
    (
    SELECT STRING_AGG(d.slipno, ',')
    FROM (
        SELECT DISTINCT CAST(
            SUBSTRING(m.slipno, 12, LEN(m.slipno)) AS varchar(50)
        ) AS slipno
        FROM Monthly_Fee_Collection_Slip m
        JOIN Student_Payment_History h 
            ON m.session = h.session 
           AND m.adno    = h.Addmission_no 
           AND m.slipno  = h.Slip_no
        WHERE m.[Session] = '" + Session_name + @"'
          AND m.[Class]   = '" + Class_id + @"'
          AND m.adno      = '" + Adm_No + @"'
          AND m.[Month]   = '" + dr["Month"].ToString() + @"'
    ) d
) AS SlipNos,

    --Distinct Dates
    (
        SELECT STRING_AGG(d.dt, ',')
        FROM(
            SELECT DISTINCT CONVERT(varchar(10), m.[Date], 105) AS dt
            FROM Monthly_Fee_Collection_Slip m
            JOIN Student_Payment_History h
                ON m.session = h.session
               AND m.adno = h.Addmission_no
               AND m.slipno = h.Slip_no
            WHERE m.[Session] = '" + Session_name + @"'
              AND m.[Class] = '" + Class_id + @"'
              AND m.adno = '" + Adm_No + @"'
              AND m.[Month] = '" + dr["Month"].ToString() + @"'
        ) d
    ) AS DistinctDates,

    --Distinct Payment Modes
    (
        SELECT STRING_AGG(d.mode, ',')
        FROM(
            SELECT DISTINCT h.Mode AS mode
            FROM Monthly_Fee_Collection_Slip m
            JOIN Student_Payment_History h
                ON m.session = h.session
               AND m.adno = h.Addmission_no
               AND m.slipno = h.Slip_no
            WHERE m.[Session] = '" + Session_name + @"'
              AND m.[Class] = '" + Class_id + @"'
              AND m.adno = '" + Adm_No + @"'
              AND m.[Month] = '" + dr["Month"].ToString() + @"'
        ) d
    ) AS DistinctModes,

    --Distinct User IDs
    (
        SELECT STRING_AGG(d.user_id, ',')
        FROM(
            SELECT DISTINCT CAST(h.user_id AS varchar(50)) AS user_id
            FROM Monthly_Fee_Collection_Slip m
            JOIN Student_Payment_History h
                ON m.session = h.session
               AND m.adno = h.Addmission_no
               AND m.slipno = h.Slip_no
            WHERE m.[Session] = '" + Session_name + @"'
              AND m.[Class] = '" + Class_id + @"'
              AND m.adno = '" + Adm_No + @"'
              AND m.[Month] = '" + dr["Month"].ToString() + @"'
        ) d
    ) AS DistinctUserIDs,

    --Distinct User Names
    (
        SELECT STRING_AGG(d.name, ',')
        FROM(
            SELECT DISTINCT u.name
            FROM Monthly_Fee_Collection_Slip m
            JOIN Student_Payment_History h
                ON m.session = h.session
               AND m.adno = h.Addmission_no
               AND m.slipno = h.Slip_no
            JOIN user_details u
                ON h.user_id = u.user_id
            WHERE m.[Session] = '" + Session_name + @"'
              AND m.[Class] = '" + Class_id + @"'
              AND m.adno = '" + Adm_No + @"'
              AND m.[Month] = '" + dr["Month"].ToString() + @"'
        ) d
    ) AS DistinctUserNames

FROM Monthly_Fee_Collection_Slip t1
JOIN Student_Payment_History t2
    ON t1.session = t2.session
   AND t1.adno = t2.Addmission_no
   AND t1.slipno = t2.Slip_no
WHERE t1.[Session] = '" + Session_name + @"'
  AND t1.[Class] = '" + Class_id + @"'
  AND t1.adno = '" + Adm_No + @"'
  AND t1.[Month] = '" + dr["Month"].ToString() + @"'; ");

                    if (ftF.Rows.Count > 0)
                    {
                        foreach (DataRow drF in ftF.Rows)
                        {
                            bill_no = bill_no + drF["SlipNos"].ToString() + ", ";
                            bill_Date = bill_Date + drF["DistinctDates"].ToString() + ", ";
                            bill_mode = bill_mode + drF["DistinctModes"].ToString() + ", ";
                            Received_by = Received_by + drF["DistinctUserNames"].ToString() + ", ";
                            paid_amt = paid_amt + drF["Total_Paid"].ToString() + ", ";
                        }
                        bill_no = bill_no.Remove(bill_no.Length - 2);
                        bill_Date = bill_Date.Remove(bill_Date.Length - 2);
                        bill_mode = bill_mode.Remove(bill_mode.Length - 2);
                        Received_by = Received_by.Remove(Received_by.Length - 2);
                        paid_amt = paid_amt.Remove(paid_amt.Length - 2);
                        string duesamts = Get_dues_amts(Session_name, Adm_No, dr["Month"].ToString());
                        Duesamts = duesamts;
                    }
                    else
                    {
                        string duesamts = Get_dues_amts(Session_name, Adm_No, dr["Month"].ToString());
                        Duesamts = duesamts;
                    }

                    string pdamt = My.toDouble(paid_amt).ToString("0");
                    if (pdamt == "0")
                    {
                        pdamt = "";
                    }
                    MyGetPaidDetailsItem.Add(new MyGetPaidDetails
                    {
                        Bill_no = bill_no,
                        Payment_mode = bill_Date,
                        Payment_date = bill_mode,
                        Received_by = Received_by,
                        Paid_amt = pdamt,
                        Duesamts = Duesamts,
                    });
                }
            }
            return MyGetPaidDetailsItem;
        }

        private string Get_dues_amts(string session_name, string adm_No, string Month)
        {
            string duesAmt = "";
            DataTable dtc = My.dataTable("select month from Typewise_fee_collection where session='" + session_name + "' and admission_no='" + adm_No + "' and month='" + Month + "'");
            if (dtc.Rows.Count > 0)
            {
                DataTable dt = My.dataTable("select Payble_amt,Paid_amt,(convert(float, Payble_amt)-convert(float, Paid_amt)) as Dues_amt from (select (isnull(sum(convert(float, payable)),0)-(+isnull(sum(convert(float, Disc)),0))) as Payble_amt,isnull(sum(convert(float, paid)),0) as Paid_amt from Typewise_fee_collection where session='" + session_name + "' and admission_no='" + adm_No + "' and month='" + Month + "') t");
                if (dt.Rows.Count > 0)
                {
                    duesAmt = dt.Rows[0]["Dues_amt"].ToString();
                }
            }
            return duesAmt;
        }
    }
}
