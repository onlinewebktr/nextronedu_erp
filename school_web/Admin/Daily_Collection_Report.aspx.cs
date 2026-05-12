using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Daily_Collection_Report : System.Web.UI.Page
    {
        My mycode = new My();
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
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
            try
            {
                if (Session["Admin"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session asc");
                        ddl_session.SelectedValue = My.get_session_id();

                        //ViewState["sessionid"] = My.get_session_id();
                        // ViewState["sessionname"] = My.get_session();
                        ViewState["endidate"] = My.get_end_idate();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }
                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();
                        find_firm_details();
                        find_by_date();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Daily_Collection_Report");
            }

        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (txt_from_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_from_date.Focus();
            }
            else if (txt_to_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_to_date.Focus();
            }
            else
            {
                find_by_date();
            }
        }

        private void find_by_date()
        {
            string sdate = txt_from_date.Text;
            string sday = sdate.Substring(0, 2);
            string smonth = sdate.Substring(3, 2);
            string syear = sdate.Substring(6, 4);

            string edate = txt_to_date.Text;
            string eday = edate.Substring(0, 2);
            string emonth = edate.Substring(3, 2);
            string eyear = edate.Substring(6, 4);

            int idate = Convert.ToInt32(syear + smonth + sday);
            int idate2 = Convert.ToInt32(eyear + emonth + eday);

            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                lbl_class22.Text = "From Date:-" + txt_from_date.Text + " To Date:-" + txt_to_date.Text;
                if (txt_from_date.Text == txt_to_date.Text)
                {
                    DateTime datatimes = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    lbl_class22.Text = "Date : " + txt_from_date.Text + " - " + datatimes.ToString("dddd");
                }

                SqlCommand cmd = new SqlCommand("sp_Daily_Collection_Report");
                cmd.Parameters.AddWithValue("@status", "Daily_Collection");
                cmd.Parameters.AddWithValue("@status2", "Student_payment");
                cmd.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
                cmd.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));

                cmd.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    pnl_grid_collection.Visible = false;
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    pnl_grid_collection.Visible = true;
                    rd_view.DataSource = dt;
                    rd_view.DataBind();

                }
                //------------form sale--------
                SqlCommand cmd1 = new SqlCommand("sp_Daily_Collection_Report");
                cmd1.Parameters.AddWithValue("@status", "Daily_Collection");
                cmd1.Parameters.AddWithValue("@status2", "Student_form_sale");
                cmd1.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
                cmd1.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
                cmd1.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
                cmd1.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
                DataSet ds1 = Store_procedure_code.executeReaderDataSet(cmd1);
                DataTable dt1 = ds1.Tables[0];
                if (dt1.Rows.Count == 0)
                {
                    pnl_grid_FormSale_Collection.Visible = false;
                    Repeater1_form_sale.DataSource = null;
                    Repeater1_form_sale.DataBind();
                }
                else
                {
                    pnl_grid_FormSale_Collection.Visible = true;
                    Repeater1_form_sale.DataSource = dt1;
                    Repeater1_form_sale.DataBind();
                }

                //------------Special Fee Collection--------
                SqlCommand cmd12 = new SqlCommand("sp_Daily_Collection_Report");
                cmd12.Parameters.AddWithValue("@status", "Daily_Collection");
                cmd12.Parameters.AddWithValue("@status2", "Student_special_fee");
                cmd12.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
                cmd12.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
                cmd12.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
                cmd12.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
                DataSet ds12 = Store_procedure_code.executeReaderDataSet(cmd12);
                DataTable dt12 = ds12.Tables[0];
                if (dt12.Rows.Count == 0)
                {
                    pnl_grid_special_fee.Visible = false;
                    rp_special_Fee.DataSource = null;
                    rp_special_Fee.DataBind();
                }
                else
                {
                    pnl_grid_special_fee.Visible = true;
                    rp_special_Fee.DataSource = dt12;
                    rp_special_Fee.DataBind();
                }

                // student Special fee collection group wise 
                SqlCommand cmd1s = new SqlCommand("sp_Daily_Collection_Report");
                cmd1s.Parameters.AddWithValue("@status", "Daily_Collection");
                cmd1s.Parameters.AddWithValue("@status2", "Student_special_fee_group");
                cmd1s.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
                cmd1s.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
                cmd1s.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
                cmd1s.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
                DataSet ds1s = Store_procedure_code.executeReaderDataSet(cmd1s);
                DataTable dt1s = ds1s.Tables[0];
                if (dt1s.Rows.Count == 0)
                {
                    datalist_special_fee_group.DataSource = null;
                    datalist_special_fee_group.DataBind();
                }
                else
                {
                    datalist_special_fee_group.DataSource = dt1s;
                    datalist_special_fee_group.DataBind();
                }

                //------------other fee colection--------
                SqlCommand cmd2 = new SqlCommand("sp_Daily_Collection_Report");
                cmd2.Parameters.AddWithValue("@status", "Daily_Collection");
                cmd2.Parameters.AddWithValue("@status2", "Student_other_fee");
                cmd2.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
                cmd2.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
                cmd2.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
                cmd2.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
                DataSet ds2 = Store_procedure_code.executeReaderDataSet(cmd2);
                DataTable dt2 = ds2.Tables[0];
                if (dt2.Rows.Count == 0)
                {
                    pnl_grid_other_Collection.Visible = false;
                    Repeater_other_fee_collection.DataSource = null;
                    Repeater_other_fee_collection.DataBind();
                }
                else
                {
                    pnl_grid_other_Collection.Visible = true;
                    Repeater_other_fee_collection.DataSource = dt2;
                    Repeater_other_fee_collection.DataBind();

                }


                // Payment Voucher   
                SqlCommand cmd3 = new SqlCommand("sp_Daily_Collection_Report");
                cmd3.Parameters.AddWithValue("@status", "Daily_Collection");
                cmd3.Parameters.AddWithValue("@status2", "PaymentVoucher");
                cmd3.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
                cmd3.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
                cmd3.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
                cmd3.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
                DataSet ds3 = Store_procedure_code.executeReaderDataSet(cmd3);
                DataTable dt3 = ds3.Tables[0];
                if (dt3.Rows.Count == 0)
                {
                    pnl_grid_Payment_Voucher.Visible = false;
                    grid_payment_Voucher.DataSource = null;
                    grid_payment_Voucher.DataBind();
                }
                else
                {
                    pnl_grid_Payment_Voucher.Visible = true;
                    grid_payment_Voucher.DataSource = dt3;
                    grid_payment_Voucher.DataBind();

                }

                // Receipt Voucher   
                SqlCommand cmd4 = new SqlCommand("sp_Daily_Collection_Report");
                cmd4.Parameters.AddWithValue("@status", "Daily_Collection");
                cmd4.Parameters.AddWithValue("@status2", "ReceiptVoucher");
                cmd4.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
                cmd4.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
                cmd4.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
                cmd4.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
                DataSet ds4 = Store_procedure_code.executeReaderDataSet(cmd4);
                DataTable dt4 = ds4.Tables[0];
                if (dt4.Rows.Count == 0)
                {
                    pnl_grid_Receipt_Voucher.Visible = false;
                    grid_Receipt_Voucher.DataSource = null;
                    grid_Receipt_Voucher.DataBind();
                }
                else
                {
                    pnl_grid_Receipt_Voucher.Visible = true;
                    grid_Receipt_Voucher.DataSource = dt4;
                    grid_Receipt_Voucher.DataBind();

                }

                Bind_group_by_date(idate, idate2);
                Bind_total_summery(idate, idate2);

            }
        }



        private void Bind_group_by_date(int idate, int idate2)
        {
            // student fee collection group wise
            SqlCommand cmd = new SqlCommand("sp_Daily_Collection_Report");
            cmd.Parameters.AddWithValue("@status", "Daily_Collection");
            cmd.Parameters.AddWithValue("@status2", "Student_payment_group");
            cmd.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
            cmd.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
            cmd.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
            cmd.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                data_view.DataSource = null;
                data_view.DataBind();
            }
            else
            {
                data_view.DataSource = dt;
                data_view.DataBind();

            }

            // student form fee collection group wise 
            SqlCommand cmd1 = new SqlCommand("sp_Daily_Collection_Report");
            cmd1.Parameters.AddWithValue("@status", "Daily_Collection");
            cmd1.Parameters.AddWithValue("@status2", "Student_form_sale_group");
            cmd1.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
            cmd1.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
            cmd1.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
            cmd1.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
            DataSet ds1 = Store_procedure_code.executeReaderDataSet(cmd1);
            DataTable dt1 = ds1.Tables[0];
            if (dt1.Rows.Count == 0)
            {
                datalist_form_sale_group.DataSource = null;
                datalist_form_sale_group.DataBind();
            }
            else
            {
                datalist_form_sale_group.DataSource = dt1;
                datalist_form_sale_group.DataBind();
            }
            // student other fee collection group wise
            SqlCommand cmd2 = new SqlCommand("sp_Daily_Collection_Report");
            cmd2.Parameters.AddWithValue("@status", "Daily_Collection");
            cmd2.Parameters.AddWithValue("@status2", "Student_other_fee_group");
            cmd2.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
            cmd2.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
            cmd2.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
            cmd2.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
            DataSet ds2 = Store_procedure_code.executeReaderDataSet(cmd2);
            DataTable dt2 = ds2.Tables[0];
            if (dt2.Rows.Count == 0)
            {
                data_list_other_fee_group.DataSource = null;
                data_list_other_fee_group.DataBind();
            }
            else
            {
                data_list_other_fee_group.DataSource = dt2;
                data_list_other_fee_group.DataBind();

            }

            // Payment Voucher   

            SqlCommand cmd3 = new SqlCommand("sp_Daily_Collection_Report");
            cmd3.Parameters.AddWithValue("@status", "Daily_Collection");
            cmd3.Parameters.AddWithValue("@status2", "PaymentVoucherfee_group");
            cmd3.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
            cmd3.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
            cmd3.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
            cmd3.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
            DataSet ds3 = Store_procedure_code.executeReaderDataSet(cmd3);
            DataTable dt3 = ds3.Tables[0];
            if (dt3.Rows.Count == 0)
            {
                data_list_payment.DataSource = null;
                data_list_payment.DataBind();
            }
            else
            {
                data_list_payment.DataSource = dt3;
                data_list_payment.DataBind();

            }

            // Receipt Voucher   
            SqlCommand cmd4 = new SqlCommand("sp_Daily_Collection_Report");
            cmd4.Parameters.AddWithValue("@status", "Daily_Collection");
            cmd4.Parameters.AddWithValue("@status2", "ReceiptVoucherfee_group");
            cmd4.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
            cmd4.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
            cmd4.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
            cmd4.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
            DataSet ds4 = Store_procedure_code.executeReaderDataSet(cmd4);
            DataTable dt4 = ds4.Tables[0];
            if (dt4.Rows.Count == 0)
            {
                data_list_Receipt.DataSource = null;
                data_list_Receipt.DataBind();
            }
            else
            {
                data_list_Receipt.DataSource = dt4;
                data_list_Receipt.DataBind();

            }



        }

        double tot1 = 0;
        double totl_cash = 0;
        double totl_bank = 0;

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                Label lbl_Payable_amt = item.FindControl("lbl_Amount") as Label;
                tot1 = tot1 + Convert.ToDouble(lbl_Payable_amt.Text);
                Label lbl_mode = item.FindControl("lbl_mode") as Label;


                if (lbl_mode.Text.ToUpper() == "CASH")
                    totl_cash = totl_cash + Convert.ToDouble(lbl_Payable_amt.Text);

                if (lbl_mode.Text.ToUpper() != "CASH")
                    totl_bank = totl_bank + Convert.ToDouble(lbl_Payable_amt.Text);

                Label lbl_bankname = item.FindControl("lbl_bankname") as Label;
                if (lbl_bankname.Text == "")
                {
                    lbl_bankname.Text = "";
                }
                else if (lbl_bankname.Text == "Select")
                {
                    lbl_bankname.Text = "";
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                RepeaterItem item = e.Item;
                Label lbl_tot = item.FindControl("lbl_tot") as Label;
                lbl_tot.Text = tot1.ToString("0.00");
                tot1 = 0;

            }

        }
        double form_sale = 0;
        double formtotal_cash = 0;
        double formtotal_bank = 0;
        protected void Repeater1_form_sale_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                Label lbl_Payable_amt = item.FindControl("lbl_Amount") as Label;
                form_sale = form_sale + Convert.ToDouble(lbl_Payable_amt.Text);
                Label lbl_mode = item.FindControl("lbl_mode") as Label;

                if (lbl_mode.Text.ToUpper() == "CASH")
                    formtotal_cash = formtotal_cash + Convert.ToDouble(lbl_Payable_amt.Text);

                if (lbl_mode.Text.ToUpper() != "CASH")
                    formtotal_bank = formtotal_bank + Convert.ToDouble(lbl_Payable_amt.Text);

                Label lbl_bankname = item.FindControl("lbl_bankname") as Label;
                if (lbl_bankname.Text == "")
                {
                    lbl_bankname.Text = "";
                }
                else if (lbl_bankname.Text == "Select")
                {
                    lbl_bankname.Text = "";
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                RepeaterItem item = e.Item;
                Label lbl_tot = item.FindControl("lbl_tot") as Label;
                lbl_tot.Text = form_sale.ToString("0.00");
                form_sale = 0;

            }
        }
        double other_fee = 0;
        double other_cash = 0;
        double other_bank = 0;
        protected void Repeater_other_fee_collection_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                Label lbl_Payable_amt = item.FindControl("lbl_Amount") as Label;
                other_fee = other_fee + Convert.ToDouble(lbl_Payable_amt.Text);
                Label lbl_mode = item.FindControl("lbl_mode") as Label;

                if (lbl_mode.Text.ToUpper() == "CASH")
                    other_cash = other_cash + Convert.ToDouble(lbl_Payable_amt.Text);

                if (lbl_mode.Text.ToUpper() != "CASH")
                    other_bank = other_bank + Convert.ToDouble(lbl_Payable_amt.Text);
                Label lbl_bankname = item.FindControl("lbl_bankname") as Label;
                if (lbl_bankname.Text == "")
                {
                    lbl_bankname.Text = "";
                }
                else if (lbl_bankname.Text == "Select")
                {
                    lbl_bankname.Text = "";
                }

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                RepeaterItem item = e.Item;
                Label lbl_tot = item.FindControl("lbl_tot") as Label;
                lbl_tot.Text = other_fee.ToString("0.00");
                other_fee = 0;

            }
        }








        double totalpayment = 0;
        double totl_cash_payment = 0;
        double totl_bank_payment = 0;

        double totalreceipt = 0;
        double totl_cash_receipt = 0;
        double totl_bank_receipt = 0;
        protected void grid_payment_Voucher_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                Label lbl_Payable_amt = item.FindControl("lbl_Amount") as Label;
                totalpayment = totalpayment + Convert.ToDouble(lbl_Payable_amt.Text);

                Label lbl_unique_entry_id = item.FindControl("lbl_unique_entry_id") as Label;
                Label lbl_mode = item.FindControl("lbl_mode") as Label;
                lbl_mode.Text = My.get_account_name_Payment(lbl_unique_entry_id.Text);


                if (lbl_mode.Text.ToUpper() == "CASH")
                    totl_cash_payment = totl_cash_payment + Convert.ToDouble(lbl_Payable_amt.Text);

                if (lbl_mode.Text.ToUpper() != "CASH")
                    totl_bank_payment = totl_bank_payment + Convert.ToDouble(lbl_Payable_amt.Text);


            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                RepeaterItem item = e.Item;
                Label lbl_tot = item.FindControl("lbl_tot") as Label;
                lbl_tot.Text = totalpayment.ToString("0.00");
                totalpayment = 0;

            }
        }
        protected void grid_Receipt_Voucher_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                Label lbl_Payable_amt = item.FindControl("lbl_Amount") as Label;
                totalreceipt = totalreceipt + Convert.ToDouble(lbl_Payable_amt.Text);

                Label lbl_unique_entry_id = item.FindControl("lbl_unique_entry_id") as Label;
                Label lbl_mode = item.FindControl("lbl_mode") as Label;
                lbl_mode.Text = My.get_account_name_Receipt(lbl_unique_entry_id.Text);

                if (lbl_mode.Text.ToUpper().Trim() == "CASH")
                    totl_cash_receipt = totl_cash_receipt + Convert.ToDouble(lbl_Payable_amt.Text);

                if (lbl_mode.Text.ToUpper() != "CASH")
                    totl_bank_receipt = totl_bank_receipt + Convert.ToDouble(lbl_Payable_amt.Text);

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                RepeaterItem item = e.Item;
                Label lbl_tot = item.FindControl("lbl_tot") as Label;
                lbl_tot.Text = totalreceipt.ToString("0.00");
                totalreceipt = 0;

            }
        }


        private void Bind_total_summery(int idate, int idate2)
        { 
            lbl_received_in_cash.Text = (totl_cash + formtotal_cash + other_cash + SpecialFee_cash).ToString("0.00");
            lbl_total_cash_receive.Text = (totl_cash).ToString("0.00");
            lbl_total_cash_other_receive.Text = (formtotal_cash + other_cash+ SpecialFee_cash).ToString("0.00");
            lbl_received_in_bank.Text = (totl_bank + formtotal_bank + other_bank + SpecialFee_bank).ToString("0.00");
            lbl_total_received_in_bank.Text = totl_bank.ToString();
            lbl_total_bank_other_receive.Text = (formtotal_bank + other_bank+ SpecialFee_bank).ToString("0.00");
            lbl_totalreceived.Text = (totl_cash + formtotal_cash + other_cash + formtotal_bank + other_bank + totl_bank + SpecialFee_cash + SpecialFee_bank).ToString("0.00");
            lbl_Receipt_in_cash.Text = totl_cash_receipt.ToString("0.00");
            lbl_Receipt_in_bank.Text = totl_bank_receipt.ToString("0.00");
            lbl_totalrefund.Text = (totl_cash_receipt + totl_bank_receipt).ToString("0.00");
            lbl_total_Receipt_in_cash.Text = totl_cash_receipt.ToString("0.00");
            lbl_total_Receipt_in_bank.Text = totl_bank_receipt.ToString("0.00"); ;

            lbl_expense_in_cash.Text = totl_cash_payment.ToString("0.00");
            lbl_expense_in_bank.Text = totl_bank_payment.ToString("0.00");
            lbl_totalexpense.Text = (totl_cash_payment + totl_bank_payment).ToString("0.00");
            lbl_total_expense_in_cash.Text = (totl_cash_payment).ToString("0.00");
            lbl_total_expense_in_bank.Text = (totl_bank_payment).ToString("0.00");


            lbl_total_cash_in_counter.Text = ((totl_cash + formtotal_cash + other_cash + totl_cash_receipt + SpecialFee_cash) - (totl_cash_payment)).ToString();



            // opening balance
            lbl_opening_balance.Text = "0.00";
            SqlCommand cmd1 = new SqlCommand("sp_Daily_Collection_Report");
            cmd1.Parameters.AddWithValue("@status", "Daily_Collection");
            cmd1.Parameters.AddWithValue("@status2", "openingBalance_cash");
            cmd1.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
            cmd1.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
            cmd1.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
            cmd1.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
            cmd1.Parameters.AddWithValue("@endidate", ViewState["endidate"].ToString());
            DataSet ds1 = Store_procedure_code.executeReaderDataSet(cmd1);
            DataTable dt1 = ds1.Tables[0];
            if (dt1.Rows.Count == 0)
            {

            }
            else
            {
                lbl_opening_balance.Text = My.toDouble(dt1.Rows[0][0].ToString()).ToString("0.00");

            }

            lbl_opening_balance_in_bank.Text = "0.00";
            SqlCommand cmd2 = new SqlCommand("sp_Daily_Collection_Report");
            cmd2.Parameters.AddWithValue("@status", "Daily_Collection");
            cmd2.Parameters.AddWithValue("@status2", "openingBalance_bank");
            cmd2.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_date.Text));
            cmd2.Parameters.AddWithValue("@todate", My.convertidate(txt_to_date.Text));
            cmd2.Parameters.AddWithValue("@sessionid", ddl_session.SelectedValue);
            cmd2.Parameters.AddWithValue("@sessionname", ddl_session.SelectedItem.Text);
            cmd2.Parameters.AddWithValue("@endidate", ViewState["endidate"].ToString());
            DataSet ds2 = Store_procedure_code.executeReaderDataSet(cmd2);
            DataTable dt2 = ds2.Tables[0];
            if (dt2.Rows.Count == 0)
            {

            }
            else
            {
                lbl_opening_balance_in_bank.Text = My.toDouble(dt2.Rows[0][0].ToString()).ToString("0.00");

            }

            lbl_gross_cash_in_hand.Text = (Convert.ToDouble(lbl_opening_balance.Text) + Convert.ToDouble(lbl_received_in_cash.Text) + Convert.ToDouble(lbl_Receipt_in_cash.Text)).ToString();

            lbl_closing_balance_in_cash.Text = (Convert.ToDouble(lbl_gross_cash_in_hand.Text) - (Convert.ToDouble(lbl_total_expense_in_cash.Text))).ToString();

            lbl_gross_amount_in_bank.Text = (Convert.ToDouble(lbl_opening_balance_in_bank.Text) + Convert.ToDouble(lbl_total_received_in_bank.Text) + Convert.ToDouble(lbl_total_bank_other_receive.Text)).ToString();
            lbl_closing_balance_in_bank.Text = (Convert.ToDouble(lbl_gross_amount_in_bank.Text) - (Convert.ToDouble(lbl_total_expense_in_bank.Text))).ToString();
        }


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                string Daily_collection = My.with_excel_name("Daily_collection_report");
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + Daily_collection + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        double SpecialFee = 0;
        double SpecialFee_cash = 0;
        double SpecialFee_bank = 0;
        protected void rp_special_Fee_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                Label lbl_Payable_amt = item.FindControl("lbl_Amount") as Label;
                SpecialFee = SpecialFee + Convert.ToDouble(lbl_Payable_amt.Text);
                Label lbl_mode = item.FindControl("lbl_mode") as Label;
                if (lbl_mode.Text.ToUpper() == "CASH")
                    SpecialFee_cash = SpecialFee_cash + Convert.ToDouble(lbl_Payable_amt.Text);

                if (lbl_mode.Text.ToUpper() != "CASH")
                    SpecialFee_bank = SpecialFee_bank + Convert.ToDouble(lbl_Payable_amt.Text);

                Label lbl_bankname = item.FindControl("lbl_bankname") as Label;
                if (lbl_bankname.Text == "")
                {
                    lbl_bankname.Text = "";
                }
                else if (lbl_bankname.Text == "Select")
                {
                    lbl_bankname.Text = "";
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                RepeaterItem item = e.Item;
                Label lbl_tot = item.FindControl("lbl_tot") as Label;
                lbl_tot.Text = SpecialFee.ToString("0.00");
                SpecialFee = 0;
            }
        }
    }
}