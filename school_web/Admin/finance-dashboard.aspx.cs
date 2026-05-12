using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class finance_dashboard : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hd_months.Value = mycode.get_current_month_id();
                int years = My.toIntS(mycode.year());
                int month = My.toIntS(mycode.cmonth());
                string SmonthYear = "01/" + mycode.monthYear();
                int lastDayOfMonth = DateTime.DaysInMonth(years, month);
                string days = lastDayOfMonth.ToString();
                if (lastDayOfMonth.ToString().Length == 1)
                {
                    days = "0" + lastDayOfMonth.ToString();
                }
                string EmonthYear = days + "/" + mycode.monthYear();
                hd_from_date.Value = My.DateConvertToIdate(SmonthYear).ToString();
                hd_to_date.Value = My.DateConvertToIdate(EmonthYear).ToString();

                hd_session_id.Value = My.get_session_id();
                hd_session_name.Value = My.get_session();
                hd_branch_id.Value = mycode.get_branch_id(Session["Admin"].ToString());

                fetch_collection_amount(SmonthYear, EmonthYear);
                fetch_cancelled_bill();
                fetc_today_expance();
            }
        }

        private void fetc_today_expance()
        {
         
            string query = "  select (Select Account_name from Account_Ledger_Details where Account_id=t1.Account_id) as Account_CR,(Select Account_name from Account_Ledger_Details where Account_id=t1.Alternet_Account) as Account_Dr, Account_Id,Alternet_Account,Description,Credit As Amount,unique_entry_id,Date,VoucherNo_Manual as VoucherNo,VoucherNo as v1,t1.ImagePath,(select top 1 name from user_details where user_id=t1.Created_by) Createdby  from dbo.[Account_Voucher_Details] t1  where  VoucherType='Payment' and firm =" + My.firm_id() + " and  Bill_from='SCHOOL'  and IDate>=" + mycode.idate()+ " and  IDate<=" + mycode.idate() + "  and    Credit>0 order by IDate";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                Rep_payment_Receipts.DataSource = dt;
                Rep_payment_Receipts.DataBind();
            }
            else
            {
                Rep_payment_Receipts.DataSource = null ;
                Rep_payment_Receipts.DataBind();
            }

        }

        private void fetch_collection_amount(string smonthYear, string emonthYear)
        {
            string sql = @"select isnull(sum(convert(float, Amount)),0) as Total_totays_collection from Student_Payment_History where Idate='" + mycode.idate() + @"';
                           select isnull(sum(convert(float, Amount)),0) as Total_monthly_collection from Student_Payment_History where Idate>='" + My.DateConvertToIdate(smonthYear) + "' and Idate<='" + My.DateConvertToIdate(emonthYear) + @"';
                           select isnull(sum(convert(float, Amount)),0) as Total_collection_from_app from Student_Payment_History where Idate='" + mycode.idate() + @"' and mode='Online' and Transection_in='App';
                           select count(Id) as TtlTodayNoBill from Student_Payment_History where Idate='" + mycode.idate() + @"';
                           select count(Id) as TtlMnthNoBill from Student_Payment_History where Idate>='" + My.DateConvertToIdate(smonthYear) + "' and Idate<='" + My.DateConvertToIdate(emonthYear) + @"';
                           select count(Id) as TtlOnlineNoBill from Student_Payment_History where Idate='" + mycode.idate() + @"' and mode='Online' and Transection_in='App';
                           select isnull(sum(convert(float, Discount_amt)),0) as Discount_amt from Discount_master_report where Created_idate='" + mycode.idate() + @"'";
            DataSet ds = mycode.Fill_Data_set(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {

                //==== Today collection
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[0];
                    if (dtTemp.Rows.Count > 0)
                    {
                        todyCollc.InnerText = "₹" + dtTemp.Rows[0]["Total_totays_collection"].ToString();
                    }
                    else
                    {
                        todyCollc.InnerText = "₹0.00";
                    }
                }
                else
                {
                    todyCollc.InnerText = "₹0.00";
                }

                //====================  Collection of till month
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[1];
                    if (dtTemp.Rows.Count != 0)
                    {
                        collcofmonth.InnerText = "₹" + dtTemp.Rows[0]["Total_monthly_collection"].ToString();
                    }
                    else
                    {
                        collcofmonth.InnerText = "₹0.00";
                    }
                }
                else
                {
                    collcofmonth.InnerText = "₹0.00";
                }

                //==================== OnlineCollection
                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[2];
                    if (dtTemp.Rows.Count != 0)
                    {
                        appcollc.InnerText = "₹" + dtTemp.Rows[0]["Total_collection_from_app"].ToString();
                    }
                    else
                    {
                        appcollc.InnerText = "₹0.00";
                    }
                }
                else
                {
                    appcollc.InnerText = "₹0.00";
                }




                //==================== Total Today No of Bill
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[3];
                    if (dtTemp.Rows.Count != 0)
                    {
                        todyCollcnobill.InnerText = "Out of " + dtTemp.Rows[0]["TtlTodayNoBill"].ToString() + " Receipts";
                        ttlbildisc.InnerText = "Out of " + dtTemp.Rows[0]["TtlTodayNoBill"].ToString() + " Receipts";
                    }
                    else
                    {
                        todyCollcnobill.InnerText = "Out of 0 Receipts";
                        ttlbildisc.InnerText = "Out of 0 Receipts";
                    }
                }
                else
                {
                    todyCollcnobill.InnerText = "Out of 0 Receipts";
                    ttlbildisc.InnerText = "Out of 0 Receipts";
                }

                //==================== Total till Month No of Bill
                if (ds.Tables[4].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[4];
                    if (dtTemp.Rows.Count != 0)
                    {
                        monthCollcnobill.InnerText = "Out of " + dtTemp.Rows[0]["TtlMnthNoBill"].ToString() + " Receipts"; //"₹" + dtTemp.Rows[0]["TtlMnthNoBill"].ToString();
                    }
                    else
                    {
                        monthCollcnobill.InnerText = "Out of 0 Receipts";
                    }
                }
                else
                {
                    monthCollcnobill.InnerText = "Out of 0 Receipts";
                }


                //==================== Total today No of Bill online
                if (ds.Tables[5].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[5];
                    if (dtTemp.Rows.Count != 0)
                    {
                        AppCollcnobill.InnerText = "Out of " + dtTemp.Rows[0]["TtlOnlineNoBill"].ToString() + " Receipts";
                    }
                    else
                    {
                        AppCollcnobill.InnerText = "Out of 0 Receipts";
                    }
                }
                else
                {
                    AppCollcnobill.InnerText = "Out of 0 Receipts";
                }

                ///TotalDiscount
                if (ds.Tables[6].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[6];
                    if (dtTemp.Rows.Count != 0)
                    {
                        todaysDisc.InnerText = "₹" + dtTemp.Rows[0]["Discount_amt"].ToString();
                    }
                    else
                    {
                        todaysDisc.InnerText = "₹0";
                    }
                }
                else
                {
                    todaysDisc.InnerText = "₹0";
                }
            }
        }



        private void fetch_cancelled_bill()
        {
            DataTable dt = My.dataTable("select top 10 *,(select top 1 name from user_details where user_id=t1.Insert_time_user_id) as Cancelled_by,(select top 1 studentname from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as Student_name,(select top 1 rollnumber from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as Rollnumber,(select top 1 class from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as className,(select top 1 Section from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as Section from Student_Payment_History_Save_bakup t1 order by id desc");
            if (dt.Rows.Count > 0)
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void Rep_payment_Receipts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor a1 = e.Item.FindControl("a1") as HtmlAnchor;

                if (((Label)e.Item.FindControl("lblAttachments")).Text.ToUpper() != "")
                {
                    a1.Visible = true;

                }
                else
                {
                    a1.Visible = false;

                }
            }
        }
    }
}