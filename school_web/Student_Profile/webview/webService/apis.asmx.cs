using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Student_Profile.webview.webService
{
    /// <summary>
    /// Summary description for apis
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class apis : System.Web.Services.WebService
    {
        My mycode = new My();
        public class MyEvents
        {
            public string Dates { get; set; }
            public string Days { get; set; }
            public string Festival { get; set; }
            public string Event_details { get; set; }
            public string IsFestival { get; set; }
            public string bdrcolors { get; set; }
        }

        List<MyEvents> MyEventsItem = new List<MyEvents>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_events(string Session_id, string Adm_no, string Class_id)
        {
            string qry = "SELECT *, FORMAT(CONVERT(DateTime, Event_date, 103), 'dd-MMM-yyyy') AS new_date, FORMAT(CONVERT(DateTime, Event_date, 103), 'dddd') AS dayName, 1 AS OrderFlag FROM Festival_events WHERE Session_id = '" + Session_id + "' AND Class_id = '" + Class_id + "' AND CONVERT(DateTime, Event_date, 103) = (SELECT TOP 1 CONVERT(DateTime, Event_date, 103) FROM Festival_events WHERE Session_id = '9' AND Class_id = '31' AND CONVERT(DateTime, Event_date, 103) >= CAST(GETDATE() AS DATE) ORDER BY CONVERT(DateTime, Event_date, 103) ASC) UNION ALL SELECT *, FORMAT(CONVERT(DateTime, Event_date, 103), 'dd-MMM-yyyy') AS new_date, FORMAT(CONVERT(DateTime, Event_date, 103), 'dddd') AS dayName, 2 AS OrderFlag FROM Festival_events WHERE Session_id = '" + Session_id + "' AND Class_id = '" + Class_id + "' ORDER BY OrderFlag ASC, Event_idate ASC";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string randomColor = "";
                    string bdrcolors = "";
                    string isFestival = "hidden";
                    if (dr["Is_festeval"].ToString() == "Yes")
                    {
                        isFestival = "Shows";
                    }
                    randomColor = GetRandomColorCode();
                    bdrcolors = "border-left-color: " + randomColor;
                    MyEventsItem.Add(new MyEvents
                    {
                        Dates = dr["new_date"].ToString(),
                        Days = dr["dayName"].ToString(),
                        Festival = dr["Festival"].ToString(),
                        Event_details = dr["Event_details"].ToString(),
                        IsFestival = isFestival,
                        bdrcolors = bdrcolors,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyEventsItem));
            }
        }


        private static readonly Random random = new Random();
        public static string GetRandomColorCode()
        {
            int r = random.Next(0, 256);
            int g = random.Next(0, 256);
            int b = random.Next(0, 256);
            return $"#{r:X2}{g:X2}{b:X2}";
        }


        ///===============================STUDENT LEDGERS
        ///

        public class MyPaymentLedger
        {
            public string FirstThreeLetters { get; set; }
            public string Months { get; set; }
            public string Content { get; set; }
            public string Amount { get; set; }
            public string Disc_amount { get; set; }
            public string Prev_paid { get; set; }
            public string Dues_amt { get; set; }
            public string MonthsLower { get; set; }
            public string Month_Range { get; set; }
            public string Session_name { get; set; }
            public string RowSpan { get; set; }
            public string IsnxtRowHide { get; set; }
        }

        List<MyPaymentLedger> MyPaymentLedgerItem = new List<MyPaymentLedger>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_PaymentLedger(string session_id, string Adm_no, string Class_id)
        {
            DataTable dt = My.dataTable("select *,(select top 1 session from admission_registor where Session_id='"+ session_id + "' and Class_id='"+ Class_id + "' and  admissionserialnumber='" + Adm_no + "') as Session_name, (SELECT TOP 1 Months FROM STUDENT_WISE_DUES_AMOUNT WHERE Session_id='" + session_id + "' and Class_id='" + Class_id + "' and admission_no='" + Adm_no + "' ORDER BY Month_position ASC) + ' - ' + (SELECT TOP 1 Months FROM STUDENT_WISE_DUES_AMOUNT WHERE Session_id='" + session_id + "' and Class_id='" + Class_id + "' and admission_no='" + Adm_no + "' ORDER BY Month_position DESC) AS Month_Range from STUDENT_WISE_DUES_AMOUNT where Session_id='" + session_id + "' and Class_id='" + Class_id + "' and admission_no='" + Adm_no + "' order by Month_position asc");
            if (dt.Rows.Count > 0)
            {
                string mountGrpCount = "";
                int ischecked = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    string IsnxtRowHide = "nohide"; 
                    if (ischecked == 1)
                    {
                        mountGrpCount = My.get_single_columndata("select count(admission_no) as TotalRow from STUDENT_WISE_DUES_AMOUNT where Session_id='" + session_id + "' and Class_id='" + Class_id + "' and admission_no='" + Adm_no + "' and months='" + dr["months"].ToString() + "'");
                    }
                    int mountGrpCountINT = My.toIntS(mountGrpCount);
                    if (mountGrpCountINT > ischecked)
                    {
                        if (ischecked > 1)
                        {
                            IsnxtRowHide = "hidden";
                        } 
                        ischecked++;
                    }
                    else
                    {
                        if (ischecked > 1)
                        {
                            IsnxtRowHide = "hidden";
                        }
                        ischecked = 1;
                    }


                    string Mname = dr["months"].ToString(); 
                    string firstThreeLetters = Mname.Substring(0, 3);

                    string contentS = dr["content"].ToString();
                    if(contentS== "TransportationFee")
                    {
                        contentS = "Transport Fee";
                    }
                    MyPaymentLedgerItem.Add(new MyPaymentLedger
                    { 
                        FirstThreeLetters= firstThreeLetters.ToUpper(),
                        Months = dr["months"].ToString(),
                        Content = contentS,
                        Amount = dr["amount"].ToString(),
                        Disc_amount = dr["disc_amount"].ToString(),
                        Prev_paid = dr["Prev_paid"].ToString(),
                        Dues_amt = dr["Dues_amt"].ToString(),
                        Month_Range = dr["Month_Range"].ToString(),
                        MonthsLower = dr["months"].ToString().ToLower(),
                        Session_name = dr["Session_name"].ToString().ToLower(),
                        RowSpan = mountGrpCount,
                        IsnxtRowHide = IsnxtRowHide,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyPaymentLedgerItem));
            }
        }
    }
}
