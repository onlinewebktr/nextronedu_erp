using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Admin.webServices.account
{
    /// <summary>
    /// Summary description for incomeExpenseNew1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class incomeExpenseNew1 : System.Web.Services.WebService
    {
        public class MyFeeReportPaymentMode
        {
            public string Content { get; set; }
            public string Amounts { get; set; }
            public string Total_income { get; set; }
            public string RowsCountss { get; set; }
            public string isrowhide { get; set; }
        }

        List<MyFeeReportPaymentMode> Show_FeeReportPaymentMode = new List<MyFeeReportPaymentMode>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_income(string FromDate, string ToDate, string OpBlnce)
        {

            int fromIdate = My.DateConvertToIdate(FromDate);
            int toIdate = My.DateConvertToIdate(ToDate);
            int oneDaysBack = fromIdate; 

            string query = "select distinct Content,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Idate>='" + fromIdate + "' and Idate<='" + toIdate + "' and session='2025-2026' group by Content  union all  select (Select top 1 Account_name from Account_Ledger_Details where Account_id=t.Alternet_Account) as Content,Debit as Paid_amt from (select  Alternet_Account,sum(cast(Credit as float)) Credit ,sum(cast(Debit as float)) Debit from dbo.[Account_Voucher_Details] where  VoucherType='Receipt' and firm='1' and  Bill_from='SCHOOL'  and IDate>='" + fromIdate + "' and  IDate<='" + toIdate + "' and Debit>0 group by Alternet_Account) t";
            DataTable dtc = My.dataTable("select 'Form Sale' as Content,Paid_amt from (select isnull(sum(convert(float, Amount)),0) as Paid_amt from Form_sale_details where idate>='" + fromIdate + "' and Idate<='" + toIdate + "') t");
            if (My.toDouble(dtc.Rows[0]["Paid_amt"].ToString()) > 0)
            {
                query = "select distinct Content,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Idate>='" + fromIdate + "' and Idate<='" + toIdate + "'  group by Content  union all select 'Form Sale' as Content,Paid_amt from (select isnull(sum(convert(float, Amount)),0) as Paid_amt from Form_sale_details where idate>='" + fromIdate + "' and Idate<='" + toIdate + "') t union all  select (Select top 1 Account_name from Account_Ledger_Details where Account_id=t.Alternet_Account) as Content,Debit as Paid_amt from (select  Alternet_Account,sum(cast(Credit as float)) Credit ,sum(cast(Debit as float)) Debit from dbo.[Account_Voucher_Details] where  VoucherType='Receipt' and firm='1' and  Bill_from='SCHOOL'  and IDate>='" + fromIdate + "' and  IDate<='" + toIdate + "' and Debit>0 group by Alternet_Account) t";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
                Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                {
                    Content = "-",
                    Amounts = "0.00",
                    Total_income = "0.00",
                    RowsCountss = "1",
                    isrowhide = "",
                });

                double total_income = 0; int rowsCount = dt.Rows.Count;
                string qrydtob = "select isnull(sum(cast(Amount as float) ),0) as Amount from (Select isnull(sum(cast(sph.Amount as float) ),0) as Amount from Student_Payment_History sph where sph.Idate<'" + oneDaysBack + "' and sph.Idate>'20241201' and sph.mode='Cash' and Slip_no in (select VoucherNo_Manual from Account_Voucher_Details where VoucherType='Receipt') union all  Select isnull(sum(cast(sph.Amount as float) ),0) as Amount from Form_sale_details sph where sph.Idate <'" + oneDaysBack + "'  and sph.Idate>'20241201' and sph.Payment_Mode='Cash' and recpt_no in (select VoucherNo_Manual from Account_Voucher_Details where VoucherType='Receipt')  union all  Select  isnull(sum(cast(sph.Content_Fee as float) ),0) as Amount from Other_Fee_Taken_For_Student sph where sph.Payment_Idate <'" + oneDaysBack + "'  and sph.Payment_Idate>'20241201' and sph.Payment_mode='Cash' and Slipid in (select VoucherNo_Manual from Account_Voucher_Details where VoucherType='Receipt') union all  Select isnull(sum(cast( isnull(Debit,0) as float)),0) as Amount  from Account_Voucher_Details where VoucherType='Receipt' and Bill_from='SCHOOL' and firm=1 and  IDate<'" + oneDaysBack + "' and IDate>'20241201') t";
                //string qrydtob = "select isnull(sum(cast(Amount as float) ),0) as Amount from (Select isnull(sum(cast(sph.Amount as float) ),0) as Amount from Student_Payment_History sph where sph.Idate<'" + oneDaysBack + "' and sph.Idate>'20241201' and Slip_no in (select VoucherNo_Manual from Account_Voucher_Details where VoucherType='Receipt') union all  Select  isnull(sum(cast(sph.Amount as float) ),0) as Amount from Form_sale_details sph where sph.Idate <'" + oneDaysBack + "'  and sph.Idate>'20241201' and recpt_no in (select VoucherNo_Manual from Account_Voucher_Details where VoucherType='Receipt')  union all  Select  isnull(sum(cast(sph.Content_Fee as float) ),0) as Amount from Other_Fee_Taken_For_Student sph where sph.Payment_Idate <'" + oneDaysBack + "'  and sph.Payment_Idate>'20241201' and Slipid in (select VoucherNo_Manual from Account_Voucher_Details where VoucherType='Receipt') union all  Select isnull(sum(cast( isnull(Debit,0) as float)),0) as Amount  from Account_Voucher_Details where VoucherType='Receipt' and Bill_from='SCHOOL' and firm=1 and  IDate<'" + oneDaysBack + "'  and IDate>'20241201') t";
                DataTable dtob = My.dataTable(qrydtob);
                if (dtob.Rows.Count > 0)
                {

                    string qryss = "select 'New Admission (2026-27)' as Content,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Idate>='" + fromIdate + "' and Idate<='" + toIdate + "' and session='2026-2027'";
                    DataTable dtnad = My.dataTable(qryss);
                    if (dtnad.Rows.Count > 0)
                    {
                        total_income = total_income + My.toDouble(dtnad.Rows[0]["Paid_amt"].ToString());
                        Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                        {
                            Content = dtnad.Rows[0]["Content"].ToString(),
                            Amounts = My.toDouble(dtnad.Rows[0]["Paid_amt"].ToString()).ToString("0.00"),
                            Total_income = total_income.ToString("0.00"),
                            RowsCountss = rowsCount.ToString(),
                            isrowhide = "",
                        });
                    }


                    My mycode = new My();
                    double openingBalance = 0;
                    string prev_exp = My.get_single_column_data("Select isnull(sum(cast( isnull(Credit,0) as float)),0) as Column_Name from Account_Voucher_Details where VoucherType='Payment' and Bill_from='SCHOOL' and firm=1 and  IDate<'" + oneDaysBack + "'");


                    openingBalance = 0;
                    if (OpBlnce.ToUpper() == "YES")
                    {
                        openingBalance = (My.toDouble(dtob.Rows[0][0].ToString()) - My.toDouble(prev_exp));
                    }
                    total_income = total_income + openingBalance;


                    if (OpBlnce.ToUpper() == "YES")
                    {
                        Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                        {
                            Content = "Opening Balance",
                            Amounts = openingBalance.ToString("0.00"),
                            Total_income = total_income.ToString("0.00"),
                            RowsCountss = "1",
                            isrowhide = "",
                        });
                    }
                    else
                    {
                        Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                        {
                            Content = "Opening Balance",
                            Amounts = openingBalance.ToString("0.00"),
                            Total_income = total_income.ToString("0.00"),
                            isrowhide = "display:none",
                            RowsCountss = "1",
                        });
                    }
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_FeeReportPaymentMode));
            }
            else
            {
                double total_income = 0; int rowsCount = dt.Rows.Count;
                foreach (DataRow dr in dt.Rows)
                {
                    total_income = total_income + My.toDouble(dr["Paid_amt"].ToString());
                    Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                    {
                        Content = dr["Content"].ToString(),
                        Amounts = My.toDouble(dr["Paid_amt"].ToString()).ToString("0.00"),
                        Total_income = total_income.ToString("0.00"),
                        RowsCountss = rowsCount.ToString(),
                        isrowhide = "",
                    });
                }



                string qryss = "select 'New Admission (2026-27)' as Content,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Idate>='" + fromIdate + "' and Idate<='" + toIdate + "' and session='2026-2027'";
                DataTable dtnad = My.dataTable(qryss);
                if (dtnad.Rows.Count > 0)
                {
                    total_income = total_income + My.toDouble(dtnad.Rows[0]["Paid_amt"].ToString());
                    Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                    {
                        Content = dtnad.Rows[0]["Content"].ToString(),
                        Amounts = My.toDouble(dtnad.Rows[0]["Paid_amt"].ToString()).ToString("0.00"),
                        Total_income = total_income.ToString("0.00"),
                        RowsCountss = rowsCount.ToString(),
                        isrowhide = "",
                    });
                }




                string qrydtob = "select isnull(sum(cast(Amount as float) ),0) as Amount from (Select isnull(sum(cast(sph.Amount as float) ),0) as Amount from Student_Payment_History sph where sph.Idate<'" + oneDaysBack + "' and sph.Idate>'20241201' and sph.mode='Cash' and Slip_no in (select VoucherNo_Manual from Account_Voucher_Details where VoucherType='Receipt') union all  Select isnull(sum(cast(sph.Amount as float) ),0) as Amount from Form_sale_details sph where sph.Idate <'" + oneDaysBack + "'  and sph.Idate>'20241201' and sph.Payment_Mode='Cash' and recpt_no in (select VoucherNo_Manual from Account_Voucher_Details where VoucherType='Receipt')  union all  Select  isnull(sum(cast(sph.Content_Fee as float) ),0) as Amount from Other_Fee_Taken_For_Student sph where sph.Payment_Idate <'" + oneDaysBack + "'  and sph.Payment_Idate>'20241201' and sph.Payment_mode='Cash' and Slipid in (select VoucherNo_Manual from Account_Voucher_Details where VoucherType='Receipt') union all  Select isnull(sum(cast( isnull(Debit,0) as float)),0) as Amount  from Account_Voucher_Details where VoucherType='Receipt' and Bill_from='SCHOOL' and firm=1 and  IDate<'" + oneDaysBack + "' and IDate>'20241201') t";
                DataTable dtob = My.dataTable(qrydtob);
                if (dtob.Rows.Count > 0)
                {
                    My mycode = new My();
                    double openingBalance = 0;
                    string prev_exp = My.get_single_column_data("Select isnull(sum(cast( isnull(Credit,0) as float)),0) as Column_Name from Account_Voucher_Details where VoucherType='Payment' and Bill_from='SCHOOL' and firm=1 and  IDate<'" + oneDaysBack + "'");


                    if (OpBlnce.ToUpper() == "YES")
                    {
                        openingBalance = (My.toDouble(dtob.Rows[0][0].ToString()) - My.toDouble(prev_exp));
                    }

                    total_income = total_income + openingBalance;
                    if (OpBlnce.ToUpper() == "YES")
                    {
                        Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                        {
                            Content = "Opening Balance",
                            Amounts = openingBalance.ToString("0.00"),
                            Total_income = total_income.ToString("0.00"),
                            RowsCountss = rowsCount.ToString(),
                            isrowhide = "",
                        });
                    }
                    else
                    {
                        Show_FeeReportPaymentMode.Add(new MyFeeReportPaymentMode
                        {
                            Content = "Opening Balance",
                            Amounts = openingBalance.ToString("0.00"),
                            Total_income = total_income.ToString("0.00"),
                            isrowhide = "display:none",
                            RowsCountss = rowsCount.ToString(),
                        });
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_FeeReportPaymentMode));
            }
        }


        ///=====================================================

        public class MyExpenseReport
        {
            public string Content { get; set; }
            public string Amounts { get; set; }
            public string Total_Expense { get; set; }
            public string RowsCountss { get; set; }
        }

        List<MyExpenseReport> Show_expense_report = new List<MyExpenseReport>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_expense(string FromDate, string ToDate, string OpBlnce)
        {
            int fromIdate = My.DateConvertToIdate(FromDate);
            int toIdate = My.DateConvertToIdate(ToDate);
            string query = "select (Select top 1 Account_name from Account_Ledger_Details where Account_id=t.Alternet_Account) as Content,Credit as Paid_amt from (select Alternet_Account, sum(cast(Credit as float)) Credit from dbo.[Account_Voucher_Details] where  VoucherType='Payment' and firm ='1' and  Bill_from='SCHOOL'  and IDate>='" + fromIdate + "' and  IDate<='" + toIdate + "' and Credit>0 group by Alternet_Account) t";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Account_Voucher_Details");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
                Show_expense_report.Add(new MyExpenseReport
                {
                    Content = "-",
                    Amounts = "0.00",
                    Total_Expense = "0.00",
                    RowsCountss = "0",
                });

                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_expense_report));
            }
            else
            {
                double total_income = 0; int rowsCount = dt.Rows.Count - 1;
                foreach (DataRow dr in dt.Rows)
                {
                    total_income = total_income + My.toDouble(dr["Paid_amt"].ToString());
                    Show_expense_report.Add(new MyExpenseReport
                    {
                        Content = dr["Content"].ToString(),
                        Amounts = My.toDouble(dr["Paid_amt"].ToString()).ToString("0.00"),
                        Total_Expense = total_income.ToString("0.00"),
                        RowsCountss = rowsCount.ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_expense_report));
            }
        }


        ////////////========================================== 
        public class MyExpenseReportSaprate
        {
            public string VoucherDate { get; set; }
            public string VoucherNo { get; set; }
            public string Content { get; set; }
            public string Description { get; set; }
            public string MOP { get; set; }
            public string Amounts { get; set; }
            public string Total_Expense { get; set; }
            public string RowsCountss { get; set; }
        }

        List<MyExpenseReportSaprate> Show_expense_reportSaprate = new List<MyExpenseReportSaprate>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_expense_saprate(string FromDate, string ToDate)
        {
            int fromIdate = My.DateConvertToIdate(FromDate);
            int toIdate = My.DateConvertToIdate(ToDate);
            string query = "select concat(Substring (cast(IDate as varchar(10)),7,2),'/',Substring (cast(IDate as varchar(10)),5,2),'/',Substring (cast(IDate as varchar(10)),1,4)) as date_one,(Select top 1 Account_name from Account_Ledger_Details where Account_id=Account_Voucher_Details.Alternet_Account) as Content,(Select top 1 Account_Name from Account_Ledger_Details where Account_id=Account_Voucher_Details.Account_id) as MOP,* from Account_Voucher_Details where VoucherType='Payment' and firm ='1' and  Bill_from='SCHOOL' and IDate>='" + fromIdate + "' and  IDate<='" + toIdate + "' and Credit>0";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Account_Voucher_Details");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                double total_income = 0; int rowsCount = dt.Rows.Count - 1;
                foreach (DataRow dr in dt.Rows)
                {
                    total_income = total_income + My.toDouble(dr["Credit"].ToString());
                    Show_expense_reportSaprate.Add(new MyExpenseReportSaprate
                    {
                        VoucherDate = dr["date_one"].ToString(),
                        VoucherNo = dr["VoucherNo"].ToString(),
                        Content = dr["Content"].ToString(),
                        Description = dr["Description"].ToString(),
                        MOP = dr["MOP"].ToString(),
                        Amounts = My.toDouble(dr["Credit"].ToString()).ToString("0.00"),
                        Total_Expense = total_income.ToString("0.00"),
                        RowsCountss = rowsCount.ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_expense_reportSaprate));

            }
        }
    }
}
