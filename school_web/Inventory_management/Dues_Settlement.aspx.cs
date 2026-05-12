using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Dues_Settlement : System.Web.UI.Page
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
                string query = "";
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
                            query = "  Select * from (Select isdb.*, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,( select top 1 party_name from dbo.[party_details] where party_id=isdb.party_id ) as studentname,(SELECT TOP 1 Course_Name FROM Add_course_table WHERE course_id=ar.class_name) as classname,'0' as rollnumber,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section  from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join party_details ar on  isdb.party_id=ar.party_id ) t  where   cast(NetPayable as float) > cast(totalpaidamount as float) and  Idate>=" + idate + " and  Idate<=" + idate2 + " order by  idate asc ";
                        }
                        else
                        {
                            query = "  Select * from (Select isdb.*, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,( select top 1 studentname from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as studentname,(select top 1 class from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as classname,(select top 1 rollnumber from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as rollnumber,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section  from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t  where   cast(NetPayable as float) > cast(totalpaidamount as float) and  Idate>=" + idate + " and  Idate<=" + idate2 + " order by  idate asc ";

                        }

                    }
                    else
                    {
                        query = "  Select * from (Select isdb.*, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,( select top 1 studentname from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as studentname,(select top 1 class from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as classname,(select top 1 rollnumber from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as rollnumber,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section  from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t  where   cast(NetPayable as float) > cast(totalpaidamount as float) and  Idate>=" + idate + " and  Idate<=" + idate2 + " order by  idate asc ";
                    }



                    total_count_grid_list(query);
                    lbl_heading.Text = "From Date" + txt_from_Date.Text + " End Date: " + txt_to_Date.Text;
                }
            }
            catch
            {

            }

        }

        private void total_count_grid_list(string query)
        {
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




            }
        }

        protected void btn_sale_id_Click(object sender, EventArgs e)
        {

            string query = "";
            if (ddl_classname.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
            {
                query = "Select * from (Select isdb.*, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,( select top 1 studentname from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as studentname,(select top 1 class from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as classname,(select top 1 rollnumber from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as rollnumber,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section  from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t  where   cast(NetPayable as float) > cast(totalpaidamount as float)  order by Idate ";

            }
            else if (ddl_classname.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
            {
                query = "Select * from (Select isdb.*, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,( select top 1 studentname from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as studentname,(select top 1 class from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as classname,(select top 1 rollnumber from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as rollnumber,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section  from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t  where   cast(NetPayable as float) > cast(totalpaidamount as float) and  Session_id='" + My.get_session_id() + "' and  Class_id='" + ddl_classname.SelectedValue + "' and  Section='" + ddl_section.Text + "'    order by  Idate, rollnumber ";



            }
            else if (ddl_classname.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
            {
                query = " Select * from (Select isdb.*, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,( select top 1 studentname from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as studentname,(select top 1 class from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as classname,(select top 1 rollnumber from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as rollnumber,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section  from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t  where   cast(NetPayable as float) > cast(totalpaidamount as float) and   Session_id='" + My.get_session_id() + "' and Class_id='" + ddl_classname.SelectedValue + "'   order by  Idate, rollnumber ";


            }
            else if (ddl_classname.SelectedItem.Text == "ALL" && ddl_section.Text != "ALL")
            {
                query = " Select * from (Select isdb.*, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,( select top 1 studentname from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as studentname,(select top 1 class from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as classname,(select top 1 rollnumber from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as rollnumber,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section  from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t  where   cast(NetPayable as float) > cast(totalpaidamount as float)  and   Session_id='" + My.get_session_id() + "'   and  Section='" + ddl_section.Text + "'   order by Idate, rollnumber ";


            }
            else
            {

                query = "Select * from (Select isdb.*, (Select  Sum(cast( Total_Paid_Amount as float)) from HMS_Inventory_Bill_Payment_Tracking   where Bill_No=pt.Bill_No and party_id=pt.party_id ) as totalpaidamount,( select top 1 studentname from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as studentname,(select top 1 class from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as classname,(select top 1 rollnumber from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=isdb.session) as rollnumber,(select top 1 Package_Name from dbo.[HMS_Package_Summary] where unique_entry_id=isdb.Package_id ) as Package_Name,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode,ar.Section  from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  isdb.party_id=ar.admissionserialnumber and isdb.session=ar.session) t  where   cast(NetPayable as float) > cast(totalpaidamount as float)  and  Session_id='" + My.get_session_id() + "'  order by  Idate ";






            }
            total_count_grid_list(query);
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


        #region pay now

        #endregion

        protected void lnk_viewitem_Click(object sender, EventArgs e)
        {
            ViewState["party_id"] = "0";
            ViewState["invoice_no"] = "0";
            ViewState["NetPayable"] = "0";
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_invoice_no = (Label)row.FindControl("lbl_invoice_no");
            Label lbl_party_id1 = (Label)row.FindControl("lbl_party_id1");
            Label lbl_Duse_Amount = (Label)row.FindControl("lbl_Duse_Amount");
            Label lbl_NetPayable = (Label)row.FindControl("lbl_NetPayable");

            ViewState["party_id"] = lbl_party_id1.Text;
            ViewState["invoice_no"] = lbl_invoice_no.Text;
            ViewState["NetPayable"] = lbl_NetPayable.Text;
            txt_payble_amount.Text = lbl_Duse_Amount.Text;
            txt_recived.Text = lbl_Duse_Amount.Text;
            txt_total_paid.Text = lbl_Duse_Amount.Text;
            txt_dues.Text = "0.00";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);
        }
        protected void btn_pay_now_Click(object sender, EventArgs e)
        {
            if (chk_bank.Checked == true)
            {
                if (ddl_paymentmode.Text == "Select")
                {
                    Alertme("Please select payment mode", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);
                }
                else if (txt_trnaction_no.Text == "")
                {
                    Alertme("Please enter valid " + lbl_mode_trns_no.Text, "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);
                }
                else
                {
                    if (txt_remarks_amt.Text == "")
                    {
                        Alertme("Please enter remarks", "warning");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);
                    }
                    else
                    {
                        final_pay_now();
                    }

                }
            }
            else
            {
                if (txt_remarks_amt.Text == "")
                {
                    Alertme("Please enter remarks", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);
                }
                else
                {
                    final_pay_now();
                }
            }

        }
        
        private void final_pay_now()
        {

            if (My.toDouble(txt_dues.Text) > 0)
            {
                Alertme("Sorry! Dues should always amount to zero before settling", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);
            }
            else
            {

                string Settlementid = invoice_format_dues_Settlement();
                SqlCommand cmd;
                string query = "INSERT INTO HMS_Inventory_Bill_Payment_Tracking (party_id,Bill_No,Payment_Vochar_id,Payable_Amount,Total_Paid_Amount,Duse_Amount,Received_from_Cash,Received_from_Bank,Bank_Payment_Mode,Date_time,Idate,Payment_transaction,Remarks,User_Id,Is_Settlement) values (@party_id,@Bill_No,@Payment_Vochar_id,@Payable_Amount,@Total_Paid_Amount,@Duse_Amount,@Received_from_Cash,@Received_from_Bank,@Bank_Payment_Mode,@Date_time,@Idate,@Payment_transaction,@Remarks,@User_Id,@Is_Settlement)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@party_id", ViewState["party_id"].ToString());
                cmd.Parameters.AddWithValue("@Bill_No", ViewState["invoice_no"].ToString());
                cmd.Parameters.AddWithValue("@Payment_Vochar_id", Settlementid);
                cmd.Parameters.AddWithValue("@Payable_Amount", My.toDouble(ViewState["NetPayable"].ToString()).ToString("0.00"));
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
                    cmd.Parameters.AddWithValue("@Bank_Payment_Mode", ddl_paymentmode.Text);

                    cmd.Parameters.AddWithValue("@Payment_transaction", txt_trnaction_no.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Received_from_Bank", "0.00");
                    cmd.Parameters.AddWithValue("@Bank_Payment_Mode", "Cash");
                    cmd.Parameters.AddWithValue("@Payment_transaction", "N/A");
                }
                cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Remarks", txt_remarks_amt.Text);
                cmd.Parameters.AddWithValue("@User_Id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Is_Settlement", 1);
                
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("You have successfully settlement of selected  invoice number is :-" + ViewState["invoice_no"].ToString(), "success");

                    //  total_count_grid_list(Session["query"].ToString());
                    string Uri = "Slip/Settlement_Voucher.aspx?Payment_Vochar_id=" + Settlementid + "&partyid=" + ViewState["party_id"].ToString() + "";
                    Response.Redirect(Uri,false);
                }

            }

        }

        private string invoice_format_dues_Settlement()
        {
            string Item_Id = My.global_id_creation("Settlement_Id");
            string billno = "SAT/" + Item_Id + "/" + My.get_session();
            return billno;
        }
    }
}