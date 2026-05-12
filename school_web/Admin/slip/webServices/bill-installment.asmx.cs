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
    /// Summary description for bill_installment
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class bill_installment : System.Web.Services.WebService
    {
        public class MyAdmitCardStudent
        {
            public string Installment_name { get; set; }
            public string Fee_head { get; set; }
            public string Net_payble { get; set; }
            public string Paid_amount { get; set; }
            public string rowspan { get; set; }
            public string SlNo { get; set; }
            public string Month_name { get; set; }
            public string IsmonthNameHide { get; set; }
            public string RowspaNHide { get; set; }
            public string TotalPayble { get; set; }
            public string TtlPaid { get; set; }
            public string Ttl_dues { get; set; }
        }

        List<MyAdmitCardStudent> EMySubMark = new List<MyAdmitCardStudent>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_bill_detail(string Session, string Class_id, string Admission_no, string Slip_no, string Is_month_name_hide)
        {
            string ismonthHide = "no";
            if (Is_month_name_hide == "Yes")
            {
                ismonthHide = "hidden";
            }
            DataTable dtI = My.dataTable("select * from Fee_installment_master");
            if (dtI.Rows.Count > 0)
            {
                int slNo = 0; double totalPayble = 0; double ttlPaid = 0; double ttl_dues = 0;
                foreach (DataRow drI in dtI.Rows)
                {
                    int frstRow = 1;
                    string months = "";
                    string month_name = drI["Month_name"].ToString();
                    string month_name1 = "";
                    string[] stringSeparatorss = new string[] { "," };
                    string[] arr = month_name.Split(stringSeparatorss, StringSplitOptions.None);
                    foreach (string value in arr)
                    {
                        months = months + "'" + value + "',";

                        string month3digt = "";
                        if (value.Length >= 3)
                        {
                            month3digt = value.Substring(0, 3);
                        }
                        else
                        {
                            month3digt = value; // or handle the case where the string is shorter than 3 characters
                        }
                        month_name1 = month_name1 + month3digt + ", ";
                    }
                    months = months.Remove(months.Length - 1);

                    month_name1 = month_name1.Remove(month_name1.Length - 2);

                    DataTable dtB = My.dataTable("select * from (select Content,sum(convert(float, payable)) as Payble, sum(convert(float, disc_amt)) as disc_amt,sum(convert(float, paid)) as paid,((sum(convert(float, payable))-sum(convert(float, previously_paid))-sum(convert(float, disc_amt)))) as Net_payble from Monthly_Fee_Collection_Slip where session='" + Session + "' and adno='" + Admission_no + "' and slipno='" + Slip_no + "' and Month in (" + months + ") group by Content)t where Net_payble>0");
                    if (dtB.Rows.Count > 0)
                    {
                        slNo++;
                        foreach (DataRow dr in dtB.Rows)
                        {
                            totalPayble = totalPayble + My.toDouble(dr["Net_payble"].ToString());
                            ttlPaid = ttlPaid + My.toDouble(dr["paid"].ToString());
                            ttl_dues = totalPayble - ttlPaid;
                            string rowspaNHide = "hidden";
                            if (frstRow == 1)
                            {
                                rowspaNHide = "showd";
                                frstRow = 0;
                            }
                            EMySubMark.Add(new MyAdmitCardStudent
                            {
                                Installment_name = drI["Intstallment_name"].ToString(),
                                Month_name = month_name1,
                                IsmonthNameHide = ismonthHide,
                                Fee_head = dr["Content"].ToString(),
                                Net_payble = dr["Net_payble"].ToString(),
                                Paid_amount = dr["paid"].ToString(),
                                rowspan = dtB.Rows.Count.ToString(),
                                SlNo = slNo.ToString(),
                                RowspaNHide = rowspaNHide,
                                TotalPayble = totalPayble.ToString(),
                                TtlPaid = ttlPaid.ToString(),
                                Ttl_dues = ttl_dues.ToString(),
                            });
                        }
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }
    }
}

