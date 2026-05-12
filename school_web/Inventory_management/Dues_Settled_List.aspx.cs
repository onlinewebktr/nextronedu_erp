using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using school_web.AppCode;
using System.Data;

namespace school_web.Inventory_management
{
    public partial class Dues_Settled_List : System.Web.UI.Page
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
                        Session["billfromsettelement"] = "1";
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
                int idate = My.DateConvertToIdate(txt_from_Date.Text);
                int idate2 = My.DateConvertToIdate(txt_to_Date.Text);
                if (idate > idate2)
                {
                    lbl_heading.Text = "";
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    string query = "    Select * from (Select isdb.party_id,isdb.Bill_No,format(isdb.Date, 'dd/MM/yyyy')  as p_date, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,  ar.party_name as studentname ,(SELECT TOP 1 Course_Name FROM Add_course_table WHERE course_id=ar.class_name) as class, '' as rollnumber  ,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section,pt.Is_Settlement,pt.Idate,pt.User_Id,pt.Payment_Vochar_id   from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Bill_No=isdb.Bill_No join party_details ar on  isdb.party_id=ar.party_id  ) t  where  Is_Settlement=1 and   Idate>=" + idate + " and  Idate<=" + idate2 + " order by  idate asc ";

                    string query1 = "Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Bill_No=isdb.Bill_No  where pt.Idate>=" + idate + " and pt.Idate<=" + idate2 + " and pt.Is_Settlement=1 group by pt.Bank_Payment_Mode ";
                    total_count_grid_list(query, query1);

                     
                    lbl_heading.Text = "From Date" + txt_from_Date.Text + " End Date: " + txt_to_Date.Text;
                }
            }
            catch
            {

            }

        }

        private void total_count_grid_list(string query,string query1)
        {
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
            Session["query"] = query;

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

                DataTable dtSS = mycode.FillData(query1);
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
                    }


                }






            }
        }

        protected void btn_sale_id_Click(object sender, EventArgs e)
        {

            string query = "";
            string query1 = "";
            if (ddl_classname.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
            {
                query = "  Select * from (Select isdb.party_id,isdb.Bill_No,format(isdb.Date, 'dd/MM/yyyy')  as p_date, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,  ar.studentname , ar.class, ar.rollnumber  ,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section,pt.Is_Settlement,pt.Idate,pt.Payment_Vochar_id,pt.User_Id,pt.Payment_Vochar_id,ar.session   from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Bill_No=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session)  t  where  Is_Settlement=1 and  ar.session='"+My.get_session() + "'  order by Idate ";



                  query1 = "  Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Bill_No=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session   where ar.session='" + My.get_session() + "' and Is_Settlement=1 group by pt.Bank_Payment_Mode ";
               
            }
            else if (ddl_classname.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
            {
                query = "  Select * from (Select isdb.party_id,isdb.Bill_No,format(isdb.Date, 'dd/MM/yyyy')  as p_date, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,  ar.studentname , ar.class, ar.rollnumber  ,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section,pt.Is_Settlement,pt.Idate,pt.Payment_Vochar_id,pt.User_Id,pt.Payment_Vochar_id,ar.session   from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Bill_No=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t where    Session_id='" + My.get_session_id() + "' and  Class_id='" + ddl_classname.SelectedValue + "' and  Section='" + ddl_section.Text + "' and Is_Settlement=1   order by  Idate, rollnumber ";


                query1 = "  Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Bill_No=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session   where ar.session='" + My.get_session() + "'  and  Class_id='" + ddl_classname.SelectedValue + "' and  Section='" + ddl_section.Text + "' and Is_Settlement=1  group by pt.Bank_Payment_Mode ";



            }
            else if (ddl_classname.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
            {
                query = "   Select * from (Select isdb.party_id,isdb.Bill_No,format(isdb.Date, 'dd/MM/yyyy')  as p_date, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,  ar.studentname , ar.class, ar.rollnumber  ,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section,pt.Is_Settlement,pt.Idate,pt.Payment_Vochar_id,pt.User_Id,ar.session   from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id,pt.Payment_Vochar_id and pt.Bill_No=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t  where      Session_id='" + My.get_session_id() + "' and Class_id='" + ddl_classname.SelectedValue + "' and Is_Settlement=1   order by  Idate, rollnumber ";

                query1 = "  Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Bill_No=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session   where ar.session='" + My.get_session() + "'  and  Class_id='" + ddl_classname.SelectedValue + "'   and Is_Settlement=1  group by pt.Bank_Payment_Mode ";


            }
            else if (ddl_classname.SelectedItem.Text == "ALL" && ddl_section.Text != "ALL")
            {
                query = "   Select * from (Select isdb.party_id,isdb.Bill_No,format(isdb.Date, 'dd/MM/yyyy')  as p_date, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,  ar.studentname , ar.class, ar.rollnumber  ,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section,pt.Is_Settlement,pt.Idate,pt.Payment_Vochar_id,pt.User_Id,pt.Payment_Vochar_id,ar.session   from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Bill_No=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t where      Session_id='" + My.get_session_id() + "'   and  Section='" + ddl_section.Text + "' and Is_Settlement=1   order by Idate, rollnumber ";


                query1 = "  Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Bill_No=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session   where ar.session='" + My.get_session() + "'  and      Section='" + ddl_section.Text + "'   and Is_Settlement=1  group by pt.Bank_Payment_Mode ";


            }
            else
            {

                query = "  Select * from (Select isdb.party_id,isdb.Bill_No,format(isdb.Date, 'dd/MM/yyyy')  as p_date, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,  ar.studentname , ar.class, ar.rollnumber  ,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section,pt.Is_Settlement,pt.Idate,pt.Payment_Vochar_id,pt.User_Id,pt.Payment_Vochar_id,ar.session   from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Bill_No=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t where     Session_id='" + My.get_session_id() + "' and Is_Settlement=1  order by  Idate ";

                query1 = "  Select sum(isnull(convert(float, pt.Received_from_Bank),0)) as Paid_amt,pt.Bank_Payment_Mode as mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=isdb.party_id and   pt.Bill_No=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session   where ar.session='" + My.get_session() + "'    and Is_Settlement=1  group by pt.Bank_Payment_Mode ";






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
                Response.AddHeader("content-disposition", "attachment;filename=Dues_Settled_List_Report_" + mycode.date() + mycode.time() + ".xls");
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
            Response.AddHeader("content-disposition", "attachment;filename=Dues_Sattlement_Report_" + mycode.date() + "_" + mycode.time() + ".xls");
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
    }
}