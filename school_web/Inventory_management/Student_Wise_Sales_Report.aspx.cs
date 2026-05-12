using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.IO;
using System.Data;

namespace school_web.Inventory_management
{
    public partial class Student_Wise_Sales_Report : System.Web.UI.Page
    {
        My mycode = new My();
        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }
        string studentname = " select top 1 studentname from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=ar.session";

        string classname = "select top 1 class from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=ar.session";


        string rollnumber = "select top 1 rollnumber from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=ar.session";

        string Package_Name = "select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('logout.aspx','_parent',replace=true);</script>");
            }
            else
            {


                ViewState["firm_id_N"] = My.get_firm_id();
                ViewState["Userid"] = Session["Admin"].ToString();
                if (!IsPostBack)
                {
                    try
                    {

                        mycode.bind_all_ddl_with_id_cap_All(ddl_classname, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor");
                        Session["billfrom"] = "3";
                        txt_from_Date.Text = mycode.date();
                        txt_to_Date.Text = mycode.date();
                        bind_datewise();

                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }

        }

        protected void ddl_classname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_classname.SelectedItem.Text == "ALL")
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor");
            }
            else
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from  admission_registor where Class_id='" + ddl_classname.SelectedValue + "'");
            }
        }
        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            bind_datewise();
        }
        private void bind_datewise()
        {
            try
            {
                string query, query1;
                int idate = My.DateConvertToIdate(txt_from_Date.Text);
                int idate2 = My.DateConvertToIdate(txt_to_Date.Text);
                if (idate > idate2)
                {
                    lbl_heading.Text = "";
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    if (ViewState["firm_id_N"].ToString() == "NNI-01")
                    {
                        if (My.toint(mycode.idate()) >= 20260510)
                        {
                              query = "Select *,ar.party_name as studentname,(SELECT TOP 1 Course_Name FROM Add_course_table WHERE course_id=ar.class_name) as classname,'0' as rollnumber,(" + Package_Name + ") as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join party_details ar on  pt.party_id=ar.party_id  where   pt.Idate>=" + idate + " and pt.Idate<=" + idate2 + " order by pt.Idate ";

                              query1 = "Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Payment_Vochar_id=isdb.Bill_No join party_details ar on  pt.party_id=ar.party_id where pt.Idate>=" + idate + " and pt.Idate<=" + idate2 + "   group by pt.Bank_Payment_Mode ";
                        }
                        else
                        {
                              query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(" + Package_Name + ") as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber and ar.session=isdb.session where   pt.Idate>=" + idate + " and pt.Idate<=" + idate2 + " order by pt.Idate ";

                              query1 = "Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber and ar.session=isdb.session where pt.Idate>=" + idate + " and pt.Idate<=" + idate2 + "   group by pt.Bank_Payment_Mode ";
                        }
                    }
                    else
                    {
                          query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(" + Package_Name + ") as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber and ar.session=isdb.session where   pt.Idate>=" + idate + " and pt.Idate<=" + idate2 + " order by pt.Idate ";

                          query1 = "Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber and ar.session=isdb.session where pt.Idate>=" + idate + " and pt.Idate<=" + idate2 + "   group by pt.Bank_Payment_Mode ";

                    }




                    total_count_grid_list(query, query1);

                    lbl_heading.Text = "From Date" + txt_from_Date.Text + " End Date: " + txt_to_Date.Text;
                }
            }
            catch
            {

            }

        }

        private void total_count_grid_list(string query, string qrySS)
        {
            Session["query"] = query;
            Session["qrySS"] = qrySS;
            print1.Visible = false;

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_by_cash.Text = "0.00";
                lbl_fnl_paid.Text = "0.00";
                lbl_total_dues.Text = "0.00";
                Alertme("Sorry there are no data list exist", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                String Fnl_paid_amt = Convert.ToDouble(dt.Compute("SUM(NetPayable1)", string.Empty)).ToString("0.00");
                String Cashpayment = Convert.ToDouble(dt.Compute("SUM(Cashpayment)", string.Empty)).ToString("0.00");
                String Duse_Amount1 = Convert.ToDouble(dt.Compute("SUM(Duse_Amount1)", string.Empty)).ToString("0.00");

                lbl_fnl_paid.Text = Fnl_paid_amt;
                lbl_total_dues.Text = Duse_Amount1;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
                print1.Visible = true;


                lbl_by_cash.Text = Cashpayment;
                DataTable dtSS = mycode.FillData(qrySS);
                if (dtSS.Rows.Count == 0)
                {
                    // lbl_by_cash.Text = "00";
                    lbl_by_netbanking.Text = "00";
                    lbl_by_deposit.Text = "00";
                    lbl_by_sb.Text = "00";
                    lbl_by_chequeS.Text = "00";
                    lbl_by_neft.Text = "00";
                    lbl_by_debitcard.Text = "00";
                    lbl_by_credit_card.Text = "00";
                    lbl_by_other_card.Text = "00";
                    lbl_by_upi.Text = "00";
                    lbl_by_branch.Text = "00";
                    lbl_pos.Text = "00";
                    lbl_online.Text = "00";
                }
                else
                {
                    lbl_online.Text = "00";
                    // lbl_by_cash.Text = "00";
                    lbl_by_netbanking.Text = "00";
                    lbl_by_deposit.Text = "00";
                    lbl_by_sb.Text = "00";
                    lbl_by_chequeS.Text = "00";
                    lbl_by_neft.Text = "00";
                    lbl_by_debitcard.Text = "00";
                    lbl_by_credit_card.Text = "00";
                    lbl_by_other_card.Text = "00";
                    lbl_by_upi.Text = "00";
                    lbl_by_branch.Text = "00";
                    lbl_pos.Text = "00";
                    for (int i = 0; i < dtSS.Rows.Count; i++)
                    {
                        //if (dtSS.Rows[i]["mode"].ToString() == "Cash")
                        //{
                        //    lbl_by_cash.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        //}
                        if (dtSS.Rows[i]["mode"].ToString() == "Netbanking")
                        {
                            lbl_by_netbanking.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Deposited In Bank")
                        {
                            lbl_by_deposit.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Sbdebit")
                        {
                            lbl_by_sb.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Cheque")
                        {
                            lbl_by_chequeS.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "NEFT")
                        {
                            lbl_by_neft.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Debitcard")
                        {
                            lbl_by_debitcard.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Creditcard") 
                        {
                            lbl_by_credit_card.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Otherdcard")
                        {
                            lbl_by_other_card.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString().ToUpper() == "UPI")
                        {
                            lbl_by_upi.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Branch")
                        {
                            lbl_by_branch.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString().ToUpper() == "POS")
                        {
                            lbl_pos.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Online")
                        {
                            lbl_online.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Wallet")
                        {
                            lbl_Wallet.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                    }
                }
            }
        }

        protected void btn_sale_id_Click(object sender, EventArgs e)
        {
            string query1 = "";
            string query = "";
            if (ddl_classname.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
            {
                query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(" + Package_Name + ") as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber  where ar.Session_id='" + My.get_session_id() + "' order by pt.Idate ";

                query1 = "Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where ar.Session_id='" + My.get_session_id() + "'  group by pt.Bank_Payment_Mode ";
            }
            else if (ddl_classname.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
            {
                query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(" + Package_Name + ") as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber and ar.session=isdb.session where ar.Session_id='" + My.get_session_id() + "' and ar.Class_id='" + ddl_classname.SelectedValue + "' and ar.Section='" + ddl_section.Text + "' order by pt.Idate,ar.rollnumber ";

                query1 = "Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber  where ar.Session_id='" + My.get_session_id() + "' and ar.Class_id='" + ddl_classname.SelectedValue + "' and ar.Section='" + ddl_section.Text + "' group by pt.Bank_Payment_Mode ";

            }
            else if (ddl_classname.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
            {
                query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(" + Package_Name + ") as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where ar.Session_id='" + My.get_session_id() + "' and ar.Class_id='" + ddl_classname.SelectedValue + "'  order by pt.Idate,ar.rollnumber ";

                query1 = "Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber  where ar.Session_id='" + My.get_session_id() + "' and ar.Class_id='" + ddl_classname.SelectedValue + "'   group by pt.Bank_Payment_Mode ";
            }
            else if (ddl_classname.SelectedItem.Text == "ALL" && ddl_section.Text != "ALL")
            {
                query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(" + Package_Name + ") as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where ar.Session_id='" + My.get_session_id() + "'   and ar.Section='" + ddl_section.Text + "' order by pt.Idate,ar.rollnumber ";

                query1 = "Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber  where ar.Session_id='" + My.get_session_id() + "'   and ar.Section='" + ddl_section.Text + "' group by pt.Bank_Payment_Mode ";
            }
            else
            {

                query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(" + Package_Name + ") as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where ar.Session_id='" + My.get_session_id() + "' order by pt.Idate ";

                query1 = "Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where ar.Session_id='" + My.get_session_id() + "'  group by pt.Bank_Payment_Mode ";




            }
            total_count_grid_list(query, query1);
            lbl_heading.Text = "Class :" + ddl_classname.SelectedItem.Text + "  Section : " + ddl_section.Text;

        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Student_Wise_Sales_Report_" + mycode.date() + mycode.time() + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    pnl_grid.RenderControl(hw);
                    string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        protected void lnk_excel_download1_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Student_Sales_Report_" + mycode.date() + "_" + mycode.time() + ".xls");

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                pnl_grid.RenderControl(hw);
                string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }




        #endregion download_in_excel


        #region update date and paymnetmode
        protected void lnk_update_date_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["partyid"] = "";
                ViewState["unique_entry_id"] = "";
                ViewState["invoice_no"] = "";
                ViewState["avlwallet"] = "";
                ddl_paymentmode.Enabled = true;
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_invoice_no = (Label)row.FindControl("lbl_invoice_no");
                Label lbl_Date1 = (Label)row.FindControl("lbl_Date1");
                Label lbl_Payment_Mode = (Label)row.FindControl("lbl_Payment_Mode");
                Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");
                Label lbl_Payment_TransactionId = (Label)row.FindControl("lbl_Payment_TransactionId");
                Label lbl_NetPayable = (Label)row.FindControl("lbl_NetPayable");
                Label lbl_partyid = (Label)row.FindControl("lbl_partyid");
                lblinvoice_no.Text = lbl_invoice_no.Text;
                lblpaymentmode.Text = lbl_Payment_Mode.Text;
                lbl_Paymentdate.Text = lbl_Date1.Text;


                txt_newdate.Text = lbl_Paymentdate.Text;
                ddl_paymentmode.Text = lblpaymentmode.Text;

                ViewState["partyid"] = lbl_partyid.Text;

                txt_paydate.Text = lbl_Date1.Text;
                ViewState["unique_entry_id"] = lbl_unique_entry_id.Text;
                ViewState["invoice_no"] = lbl_invoice_no.Text;


                txt_payble_amount.Text = lbl_NetPayable.Text;

                if (ddl_paymentmode.Text == "Wallet")
                {
                    txt_tran_id.Text = "";
                    lavel_tran.Visible = false;
                    txt_tran_id.Visible = false;
                    ddl_paymentmode.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);

                }
                else
                {

                    Get_data_HMS_Inventory_Bill_Payment_Tracking(ViewState["invoice_no"].ToString(), lbl_partyid.Text);


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                }







            }
            catch (Exception ex)
            {
            }
        }

        private void Get_data_HMS_Inventory_Bill_Payment_Tracking(string invoice_no, string partyid)
        {
            DataTable dt = mycode.FillData("Select * from HMS_Inventory_Bill_Payment_Tracking where Payment_Vochar_id='" + invoice_no + "' and Bill_No='" + invoice_no + "' and Payment_Vochar_id='" + invoice_no + "' and party_id='" + partyid + "'");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                txt_payble_amount.Text = dt.Rows[0]["Payable_Amount"].ToString();
                if (dt.Rows[0]["Bank_Payment_Mode"].ToString() == "Cash")
                {
                    chk_cash.Checked = true;
                    chk_bank.Checked = false;
                }
                else
                {
                    ddl_paymentmode1.Text = dt.Rows[0]["Bank_Payment_Mode"].ToString();
                    chk_bank.Checked = true;
                    txt_recived_from_bank.Text = dt.Rows[0]["Received_from_Bank"].ToString();
                    txt_trnaction_no.Text = dt.Rows[0]["Payment_transaction"].ToString();
                    lbl_mode_trns_no.Text = "Transaction No.";
                    if (My.toDouble(dt.Rows[0]["Received_from_Cash"].ToString()) > 0)
                    {
                        chk_cash.Checked = true;
                        txt_recived.Text = dt.Rows[0]["Received_from_Cash"].ToString();
                    }
                    txt_total_paid.Text= dt.Rows[0]["Total_Paid_Amount"].ToString();
                    txt_dues.Text= dt.Rows[0]["Duse_Amount"].ToString();
                    txt_remarks_amt.Text= dt.Rows[0]["Remarks"].ToString();
                }
            }
        }

        protected void btn_conf_remove_Click(object sender, EventArgs e)
        {
            try
            {



                mycode.executequery("update HMS_INVETORY_SELL_DETAILS_BILLWISE set Payment_Mode='" + ddl_paymentmode.Text + "',Idate=" + My.convertidate(txt_newdate.Text) + ",Date='" + mycode.getdate2(txt_newdate.Text) + "',Update_date_time='" + My.getdate1() + "',Update_by='" + ViewState["Userid"].ToString() + "',Payment_TransactionId='" + txt_tran_id.Text + "' where unique_no='" + ViewState["unique_entry_id"].ToString() + "'");

                mycode.executequery("update HMS_Inventory_Bill_Payment_Tracking set Bank_Payment_Mode='" + ddl_paymentmode.Text + "',Idate=" + My.convertidate(txt_newdate.Text) + ",Date_time='" + mycode.getdate2(txt_newdate.Text) + "', Payment_transaction='" + txt_tran_id.Text + "' where Payment_Vochar_id='" + ViewState["invoice_no"].ToString() + "'");
                Alertme("Updation process has been successfully", "success");

                total_count_grid_list(Session["query"].ToString(), Session["qrySS"].ToString());
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddl_paymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_paymentmode.SelectedItem.Text == "Cash")
            {
                lavel_tran.Visible = false;
                txt_tran_id.Visible = false;
            }
            else
            {
                lavel_tran.Visible = true;
                txt_tran_id.Visible = true;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);

        }


        #endregion

        protected void btn_pay_now_Click(object sender, EventArgs e)
        {
            try
            {
                if (chk_bank.Checked == true)
                {
                    if (ddl_paymentmode.Text == "Select")
                    {
                        Alertme("Please select payment mode", "warning");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                    }
                    else
                    {
                        if (ddl_paymentmode.Text == "Wallet")
                        {

                            double Walletamount = My.toDouble(ViewState["avlwallet"].ToString());
                            if (Walletamount >= My.toDouble(txt_recived_from_bank.Text))
                            {
                                final_pay_now();
                            }
                            else
                            {
                                Alertme(" Sorry you cannot enter more than wallet amount", "warning");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                            }



                        }
                        else
                        {
                            if (txt_trnaction_no.Text == "")
                            {
                                Alertme("Please enter valid " + lbl_mode_trns_no.Text, "warning");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                            }
                            else
                            {
                                final_pay_now();
                            }

                        }
                    }

                }
                else
                {
                    final_pay_now();
                }
            }
            catch(Exception ex)
            {
                Alertme(ex.ToString(), "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
            }
           

        }

        private void final_pay_now()
        { 
            mycode.executequery("update HMS_INVETORY_SELL_DETAILS_BILLWISE set Payment_Mode='" + ddl_paymentmode1.Text + "',Idate=" + My.convertidate(txt_paydate.Text) + ",Date='" + My.toDateTime(txt_paydate.Text + " " + mycode.time()).ToString("MM/dd/yyyy hh:mm:ss tt") + "',Update_date_time='" + My.getdate1() + "',Update_by='" + ViewState["Userid"].ToString() + "',Payment_TransactionId='" + txt_tran_id.Text + "' where unique_no='" + ViewState["unique_entry_id"].ToString() + "'");
            SqlCommand cmd;
            string query = "Update HMS_Inventory_Bill_Payment_Tracking set   Total_Paid_Amount=@Total_Paid_Amount,Duse_Amount=@Duse_Amount,Received_from_Cash=@Received_from_Cash,Received_from_Bank=@Received_from_Bank,Bank_Payment_Mode=@Bank_Payment_Mode,Date_time=@Date_time,Idate=@Idate,Payment_transaction=@Payment_transaction,Remarks=@Remarks,User_Id=@User_Id where party_id=@party_id and Bill_No=@Bill_No and Payment_Vochar_id=@Payment_Vochar_id ";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@party_id", ViewState["partyid"].ToString());
            cmd.Parameters.AddWithValue("@Bill_No", ViewState["invoice_no"].ToString());
            cmd.Parameters.AddWithValue("@Payment_Vochar_id", ViewState["invoice_no"].ToString());
            cmd.Parameters.AddWithValue("@Total_Paid_Amount", My.toDouble(txt_total_paid.Text).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Duse_Amount", My.toDouble(txt_dues.Text).ToString("0.00"));
            if (chk_cash.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Received_from_Cash", My.toDouble(txt_recived.Text).ToString("0.00"));
            }
            else
            {
                cmd.Parameters.AddWithValue("@Received_from_Cash", "0.00");
            } 
            if (chk_bank.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Received_from_Bank", My.toDouble(txt_recived_from_bank.Text).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Bank_Payment_Mode", ddl_paymentmode1.Text);
                cmd.Parameters.AddWithValue("@Payment_transaction", txt_trnaction_no.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Received_from_Bank", "0.00");
                cmd.Parameters.AddWithValue("@Bank_Payment_Mode", "Cash");
                cmd.Parameters.AddWithValue("@Payment_transaction", "N/A");
            }

            cmd.Parameters.AddWithValue("@Date_time", mycode.getdate2(txt_paydate.Text));
            cmd.Parameters.AddWithValue("@Idate", My.convertidate(txt_paydate.Text));
            cmd.Parameters.AddWithValue("@Remarks", txt_remarks_amt.Text);
            cmd.Parameters.AddWithValue("@User_Id", ViewState["Userid"].ToString());
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Updation process has been successfully", "success");
                total_count_grid_list(Session["query"].ToString(), Session["qrySS"].ToString());
            } 
        }
    }
}