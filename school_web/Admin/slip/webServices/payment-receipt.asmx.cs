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
    /// Summary description for payment_receipt
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class payment_receipt : System.Web.Services.WebService
    {
        public class MyFeeReportPaymentMode
        {
            public string Particular { get; set; }
            public string Fee_amount { get; set; }
            public string Paid { get; set; }
            public string Fee_after_disc { get; set; }
        }

        List<MyFeeReportPaymentMode> Show_FeeReportPaymentMode = new List<MyFeeReportPaymentMode>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_billing(string Session, string Admission_no, string Slip_no)
        {
            string query = "select Particular,(sum(convert(float, fee_amount))-sum(convert(float, previously_paid))) as fee_amount,sum(convert(float, previously_paid)) as previously_paid,sum(convert(float, disc_amt)) as disc_amt,sum(convert(float, payable)) as payable,sum(convert(float, paid)) as paid from (select Month,Content as Particular, cast(payable AS float) fee_amount,cast(previously_paid AS float) previously_paid,cast(disc_amt AS float) disc_amt,(cast(payable AS float)-cast(previously_paid AS float)-cast(disc_amt AS float)) payable, cast(paid AS float)  as paid from Monthly_Fee_Collection_Slip where session='" + Session + "' and slipno='" + Slip_no + "' and adno='" + Admission_no + "') t group by Particular";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    double fee_after_disc = (My.toDouble(dr["fee_amount"].ToString()) - My.toDouble(dr["disc_amt"].ToString()));
                    Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                    {
                        Particular = dr["Particular"].ToString(),
                        Fee_amount = dr["fee_amount"].ToString(),
                        Paid = dr["paid"].ToString(),
                        Fee_after_disc = fee_after_disc.ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_FeeReportPaymentMode));
            }
        }





        //=================================================

        public class MyFeeReportPaymentFeeDt
        {
            public string Particular { get; set; }
            public string Fee_amount { get; set; }
            public string Paid { get; set; }
            public string Fee_after_disc { get; set; }

            public string Disc_amt { get; set; }
            public string Payable { get; set; }
        }

        List<MyFeeReportPaymentFeeDt> MyFeeReportPaymentFeeDtItem = new List<MyFeeReportPaymentFeeDt>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_billing_fee_dt(string Session, string Admission_no, string Slip_no)
        {
            string query = "select Particular,(sum(convert(float, fee_amount))-sum(convert(float, previously_paid))) as fee_amount,sum(convert(float, previously_paid)) as previously_paid,sum(convert(float, disc_amt)) as disc_amt,sum(convert(float, payable)) as payable,sum(convert(float, paid)) as paid from (select Month,Content as Particular, cast(payable AS float) fee_amount,cast(previously_paid AS float) previously_paid,cast(disc_amt AS float) disc_amt,(cast(payable AS float)-cast(previously_paid AS float)-cast(disc_amt AS float)) payable, cast(paid AS float)  as paid from Monthly_Fee_Collection_Slip where session='" + Session + "' and slipno='" + Slip_no + "' and adno='" + Admission_no + "') t group by Particular";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    double fee_after_disc = (My.toDouble(dr["fee_amount"].ToString()) - My.toDouble(dr["disc_amt"].ToString()));
                    MyFeeReportPaymentFeeDtItem.Add(new MyFeeReportPaymentFeeDt
                    {
                        Particular = dr["Particular"].ToString(),
                        Fee_amount = dr["fee_amount"].ToString(),
                        Disc_amt = dr["disc_amt"].ToString(),
                        Payable = dr["payable"].ToString(),
                        Paid = dr["paid"].ToString(),
                        Fee_after_disc = fee_after_disc.ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyFeeReportPaymentFeeDtItem));
            }
        }
    }
}
