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

namespace school_web.Admin
{
    public partial class Day_End_Report_Summery_N : System.Web.UI.Page
    {
        My mycode = new My();
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = "0"; //= (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];



                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        txt_s_date.Text = mycode.date();
                        txt_e_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_session, "select Session,session_id from session_details order by Session asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "select Course_Name,course_id from Add_course_table order by Position asc");
                        get_firmdaetail();
                        Find_data_day_end_report();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                //imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        private void get_firmdaetail()
        {
            try
            {
                DataTable dt = mycode.FillData("select Show_day_end_hostel from Firm_Details   ");
                if (dt.Rows.Count == 0)
                {
                    Panel1.Visible = false;
                }
                else
                {
                    if (dt.Rows[0]["Show_day_end_hostel"].ToString() == "0")
                    {
                        Panel1.Visible = false;
                    }
                    else
                    {
                        Panel1.Visible = true;
                    }
                }
            }
            catch
            {
                Panel1.Visible = false;
            }
        }



        private void Find_data_day_end_report()
        {
            lbl_ttl_cash.Text = "0.00";
            lbl_ttl_cheque.Text = "0.00";
            lbl_exp_cash.Text = "0.00";
            lbl_exp_cheque.Text = "0.00";
            lbl_exp_online.Text = "0.00";
            lbl_ttl_blnce_onlne.Text = "0.00";
            lbl_ttl_blnce_cash.Text = "0.00";
            lbl_balance_amount.Text = "0.00";

            ViewState["ADMISSIONDayScholar"] = "0"; ViewState["ANNUALDayScholar"] = "0"; ViewState["MONTHLYDayScholar"] = "0"; ViewState["HOSTELADMCollection"] = "0"; ViewState["HOSTELANNUALCollection"] = "0"; ViewState["HOSTELMNTHLYCollection"] = "0"; ViewState["FormSaleCollection"] = "0"; ViewState["OTHERFeesCollection"] = "0"; ViewState["EXPENSESFEES"] = "0"; ViewState["EXPENSESFEESNOVENDOR"] = "0";
            lbl_reportdate.Text = " From : " + txt_s_date.Text + " To : " + txt_e_date.Text;
            btn_excels.Visible = true;
            print1.Visible = true;
            NotFoundS.Visible = false;
            Bind_data_two_date_wise_admission_fee_day_boarding_cash();
            Bind_data_two_date_wise_admission_fee_day_boarding_online();
            Bind_data_two_date_wise_admission_fee_day_boarding_cheque();

            Bind_data_two_date_wise_annual_fee_day_boarding_cash();
            Bind_data_two_date_wise_annual_fee_day_boarding_online();
            Bind_data_two_date_wise_annual_fee_day_boarding_cheque();
            //----------------------monthly  fee collection day scholar-------------------

            Bind_data_two_date_wise_monthly_fee_day_boarding_cash();
            Bind_data_two_date_wise_monthly_fee_day_boarding_online();
            Bind_data_two_date_wise_monthly_fee_day_boarding_cheque();
            //--------------------Hosteler fee  admission fee and annual fee--------

            Bind_data_two_date_wise_admission_fee_hostler_cash();
            Bind_data_two_date_wise_admission_fee_hostler_online();
            Bind_data_two_date_wise_admission_fee_hostler_cheque();

            Bind_data_two_date_wise_annual_fee_hostler_cash();
            Bind_data_two_date_wise_annual_fee_hostler_online();
            Bind_data_two_date_wise_annual_fee_hostler_cheque();

            //----------------------monthly  fee colection hostel-------------------
            Bind_data_two_date_wise_monthly_fee_hostler_cash();
            Bind_data_two_date_wise_monthly_fee_hostler_online();
            Bind_data_two_date_wise_monthly_fee_hostler_cheque();
            //-------------------------------------------

            //Form sale
            Bind_data_two_date_wise_form_sale();
            Bind_data_two_date_wise_fee_form_sale();
            Bind_data_two_date_wise_fee_form_cheque();
            //--------------------------------Other Fee collection---------------

            Bind_data_two_date_wise_other_fee_cash();
            Bind_data_two_date_wise_other_fee_online();
            Bind_data_two_date_wise_other_fee_cheque();

            //===============================
            Bind_data_two_date_wise_general_exp_cash();
            Bind_data_two_date_wise_general_exp_online();
            Bind_data_two_date_wise_general_exp_cheque();
            //===============================
            Bind_data_two_date_wise_general_exp_cash_no_vendor();
            Bind_data_two_date_wise_general_exp_online_no_vendor();
            Bind_data_two_date_wise_general_exp_cheque_no_vendor();

            Bind_over_all_summery();
            Bind_over_all_summery_online();
            Bind_over_all_summery_cheque();
            Bind_over_all_summery_expenses();

            //====================ADMISSION DAY SCHOLAR  FEES COLLECTION
            if (ViewState["ADMISSIONDayScholar"].ToString() == "1")
            {
                pnl_admission_day_scholar.Visible = true;
            }
            else
            {
                pnl_admission_day_scholar.Visible = false;
            }
            //===================ANNUAL DAY SCHOLAR  FEES COLLECTION
            if (ViewState["ANNUALDayScholar"].ToString() == "1")
            {
                pnl_annual_day_scholar.Visible = true;
            }
            else
            {
                pnl_annual_day_scholar.Visible = false;
            }

            //===================MONTHLY DAY SCHOLAR  FEES COLLECTION
            if (ViewState["MONTHLYDayScholar"].ToString() == "1")
            {
                pnl_monthly_day_scholar.Visible = true;
            }
            else
            {
                pnl_monthly_day_scholar.Visible = false;
            }

            //===================HOSTEL ADMISSION FEES COLLECTION
            if (ViewState["HOSTELADMCollection"].ToString() == "1")
            {
                pnl_admission_hostel.Visible = true;
            }
            else
            {
                pnl_admission_hostel.Visible = false;
            }

            //===================HOSTER ANNUAL  FEES COLLECTION
            if (ViewState["HOSTELANNUALCollection"].ToString() == "1")
            {
                pnl_annual_hostel.Visible = true;
            }
            else
            {
                pnl_annual_hostel.Visible = false;
            }


            //===================HOSTELMONTHLY  FEES COLLECTION
            if (ViewState["HOSTELMNTHLYCollection"].ToString() == "1")
            {
                pnl_monthly_hostel.Visible = true;
            }
            else
            {
                pnl_monthly_hostel.Visible = false;
            }

            //===================FORMSALE FEES COLLECTION
            if (ViewState["FormSaleCollection"].ToString() == "1")
            {
                pnl_form_sale.Visible = true;
            }
            else
            {
                pnl_form_sale.Visible = false;
            }

            //===================OTHER FEES COLLECTION
            if (ViewState["OTHERFeesCollection"].ToString() == "1")
            {
                pnl_other_fees.Visible = true;
            }
            else
            {
                pnl_other_fees.Visible = false;
            }

            //===================EXPENSES
            if (ViewState["EXPENSESFEES"].ToString() == "1")
            {
                pnl_general_expense.Visible = true;
            }
            else
            {
                pnl_general_expense.Visible = false;
            }

            //===================EXPENSES NO VENDOR
            if (ViewState["EXPENSESFEESNOVENDOR"].ToString() == "1")
            {
                pnl_general_Exp_no_vendor.Visible = true;
            }
            else
            {
                pnl_general_Exp_no_vendor.Visible = false;
            }
        }









        #region day schoolor Scholar admission and annual fee collection
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

        private void Bind_data_two_date_wise_general_exp_cash()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
            cmd.Parameters.AddWithValue("@fromdate ", idate);
            cmd.Parameters.AddWithValue("@todate ", idate2);
            cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Financial_year ", My.get_session_id());
            cmd.Parameters.AddWithValue("@sp_status ", "7");// day school 
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                rp_general_exp_cash.DataSource = dt;
                rp_general_exp_cash.DataBind();
                pnl_general_expense_cash.Visible = true;
                ViewState["EXPENSESFEES"] = "1";
            }
            else
            {
                rp_general_exp_cash.DataSource = null;
                rp_general_exp_cash.DataBind();
                pnl_general_expense_cash.Visible = false;
            }
        }
        private void Bind_data_two_date_wise_general_exp_online()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
            cmd.Parameters.AddWithValue("@fromdate ", idate);
            cmd.Parameters.AddWithValue("@todate ", idate2);
            cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Financial_year ", My.get_session_id());
            cmd.Parameters.AddWithValue("@sp_status ", "8");// day school 
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                rp_general_exp_online.DataSource = dt;
                rp_general_exp_online.DataBind();
                pnl_general_expense_online.Visible = true;
                ViewState["EXPENSESFEES"] = "1";
            }
            else
            {
                rp_general_exp_online.DataSource = null;
                rp_general_exp_online.DataBind();
                pnl_general_expense_online.Visible = false;
            }
        }
        private void Bind_data_two_date_wise_general_exp_cheque()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
            cmd.Parameters.AddWithValue("@fromdate ", idate);
            cmd.Parameters.AddWithValue("@todate ", idate2);
            cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Financial_year ", My.get_session_id());
            cmd.Parameters.AddWithValue("@sp_status ", "80");// day school 
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                rp_general_exp_cheque.DataSource = dt;
                rp_general_exp_cheque.DataBind();
                pnl_general_expense_cheque.Visible = true;
                ViewState["EXPENSESFEES"] = "1";
            }
            else
            {
                rp_general_exp_cheque.DataSource = null;
                rp_general_exp_cheque.DataBind();
                pnl_general_expense_cheque.Visible = false;
            }
        }



        private void Bind_data_two_date_wise_general_exp_cash_no_vendor()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
            cmd.Parameters.AddWithValue("@fromdate ", idate);
            cmd.Parameters.AddWithValue("@todate ", idate2);
            cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Financial_year ", My.get_session_id());
            cmd.Parameters.AddWithValue("@sp_status ", "11");// day school 
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                rp_general_Exp_no_vendor_Cash.DataSource = dt;
                rp_general_Exp_no_vendor_Cash.DataBind();
                pnl_general_Exp_no_vendor_Cash.Visible = true;
                ViewState["EXPENSESFEESNOVENDOR"] = "1";
            }
            else
            {
                rp_general_Exp_no_vendor_Cash.DataSource = null;
                rp_general_Exp_no_vendor_Cash.DataBind();
                pnl_general_Exp_no_vendor_Cash.Visible = false;
            }
        }
        private void Bind_data_two_date_wise_general_exp_online_no_vendor()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
            cmd.Parameters.AddWithValue("@fromdate ", idate);
            cmd.Parameters.AddWithValue("@todate ", idate2);
            cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Financial_year ", My.get_session_id());
            cmd.Parameters.AddWithValue("@sp_status ", "12");// day school 
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                rp_general_Exp_no_vendor_online.DataSource = dt;
                rp_general_Exp_no_vendor_online.DataBind();
                pnl_general_Exp_no_vendor_online.Visible = true;
                ViewState["EXPENSESFEESNOVENDOR"] = "1";
            }
            else
            {
                rp_general_Exp_no_vendor_online.DataSource = null;
                rp_general_Exp_no_vendor_online.DataBind();
                pnl_general_Exp_no_vendor_online.Visible = false;
            }
        }
        private void Bind_data_two_date_wise_general_exp_cheque_no_vendor()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
            cmd.Parameters.AddWithValue("@fromdate ", idate);
            cmd.Parameters.AddWithValue("@todate ", idate2);
            cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Financial_year ", My.get_session_id());
            cmd.Parameters.AddWithValue("@sp_status ", "112");// day school 
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                rp_general_Exp_no_vendor_cheque.DataSource = dt;
                rp_general_Exp_no_vendor_cheque.DataBind();
                pnl_general_Exp_no_vendor_cheque.Visible = true;
                ViewState["EXPENSESFEESNOVENDOR"] = "1";
            }
            else
            {
                rp_general_Exp_no_vendor_cheque.DataSource = null;
                rp_general_Exp_no_vendor_cheque.DataBind();
                pnl_general_Exp_no_vendor_cheque.Visible = false;
            }
        }


        private void Bind_data_two_date_wise_admission_fee_day_boarding_cash()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "AdmissionFee");
                    cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Cash");// day school
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rp_view_admission_day_boarding.DataSource = dt;
                        rp_view_admission_day_boarding.DataBind();
                        pnl_admission_day_scholar_cash.Visible = true;
                        ViewState["ADMISSIONDayScholar"] = "1";
                    }
                    else
                    {
                        rp_view_admission_day_boarding.DataSource = null;
                        rp_view_admission_day_boarding.DataBind();
                        pnl_admission_day_scholar_cash.Visible = false;
                    }
                }
            }
        }
        private void Bind_data_two_date_wise_admission_fee_day_boarding_online()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "AdmissionFee");
                    cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Online");// day school
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rp_view_admission_day_boarding_online.DataSource = dt;
                        rp_view_admission_day_boarding_online.DataBind();
                        pnl_admission_day_scholar_online.Visible = true;
                        ViewState["ADMISSIONDayScholar"] = "1";
                    }
                    else
                    {
                        rp_view_admission_day_boarding_online.DataSource = null;
                        rp_view_admission_day_boarding_online.DataBind();
                        pnl_admission_day_scholar_online.Visible = false;
                    }
                }
            }
        }


        private void Bind_data_two_date_wise_admission_fee_day_boarding_cheque()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Type ", "AdmissionFee");
                cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                cmd.Parameters.AddWithValue("@Paymentmode ", "Cheque");// day school
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount > 0)
                {
                    rp_view_admission_day_boarding_cheque.DataSource = dt;
                    rp_view_admission_day_boarding_cheque.DataBind();
                    pnl_admission_day_scholar_cheque.Visible = true;
                    ViewState["ADMISSIONDayScholar"] = "1";
                }
                else
                {
                    rp_view_admission_day_boarding_cheque.DataSource = null;
                    rp_view_admission_day_boarding_cheque.DataBind();
                    pnl_admission_day_scholar_cheque.Visible = false;
                }
            }
        }


        private void Bind_data_two_date_wise_annual_fee_day_boarding_cash()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "AnnualFee"); // anuual fee collection
                    cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Cash");// day school
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rp_view_annual_day_boarding_cash.DataSource = dt;
                        rp_view_annual_day_boarding_cash.DataBind();
                        pnl_annual_day_scholar_cash.Visible = true;
                        ViewState["ANNUALDayScholar"] = "1";
                    }
                    else
                    {
                        rp_view_annual_day_boarding_cash.DataSource = null;
                        rp_view_annual_day_boarding_cash.DataBind();
                        pnl_annual_day_scholar_cash.Visible = false;
                    }
                }
            }
        }

        private void Bind_data_two_date_wise_annual_fee_day_boarding_online()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "AnnualFee");// annual fee colection
                    cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Online");// day school
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rp_view_annual_day_boarding_online.DataSource = dt;
                        rp_view_annual_day_boarding_online.DataBind();
                        pnl_annual_day_scholar_online.Visible = true;
                        ViewState["ANNUALDayScholar"] = "1";
                    }
                    else
                    {
                        rp_view_annual_day_boarding_online.DataSource = null;
                        rp_view_annual_day_boarding_online.DataBind();
                        pnl_annual_day_scholar_online.Visible = false;
                    }
                }
            }
        }

        private void Bind_data_two_date_wise_annual_fee_day_boarding_cheque()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Type ", "AnnualFee");// annual fee colection
                cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                cmd.Parameters.AddWithValue("@Paymentmode ", "Cheque");// day school
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount > 0)
                {
                    rp_view_annual_day_boarding_cheque.DataSource = dt;
                    rp_view_annual_day_boarding_cheque.DataBind();
                    pnl_annual_day_scholar_cheque.Visible = true;
                    ViewState["ANNUALDayScholar"] = "1";
                }
                else
                {
                    rp_view_annual_day_boarding_cheque.DataSource = null;
                    rp_view_annual_day_boarding_cheque.DataBind();
                    pnl_annual_day_scholar_cheque.Visible = false;
                }
            }
        }


        double total_day_boarding_admission_fee_online = 0;
        protected void grid_view_admission_day_boarding_online_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_day_boarding_admission_fee_online = total_day_boarding_admission_fee_online + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_day_boarding_admission_fee_online.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }
        double total_day_boarding_admission_fee_cash = 0;
        protected void grid_view_admission_day_boarding_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_day_boarding_admission_fee_cash = total_day_boarding_admission_fee_cash + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_day_boarding_admission_fee_cash.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }

        double total_day_boarding_annual_fee_cash = 0;
        protected void grid_view_annual_day_boarding_cash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_day_boarding_annual_fee_cash = total_day_boarding_annual_fee_cash + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_day_boarding_annual_fee_cash.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }

        double total_day_boarding_annual_fee_online = 0;
        protected void grid_view_annual_day_boarding_online_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_day_boarding_annual_fee_online = total_day_boarding_annual_fee_online + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_day_boarding_annual_fee_online.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }

        }
        #endregion

        #region day schoolor Scholar  monthly fee collection
        private void Bind_data_two_date_wise_monthly_fee_day_boarding_cash()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "MonthlyFee");
                    cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Cash");// day school
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rp_view_monthly_day_boarding_cash.DataSource = dt;
                        rp_view_monthly_day_boarding_cash.DataBind();
                        pnl_monthly_day_scholar_cash.Visible = true;
                        ViewState["MONTHLYDayScholar"] = "1";
                    }
                    else
                    {
                        rp_view_monthly_day_boarding_cash.DataSource = null;
                        rp_view_monthly_day_boarding_cash.DataBind();
                        pnl_monthly_day_scholar_cash.Visible = false;

                    }

                }
            }
        }
        double total_day_boarding_monthly_fee_cash = 0;
        protected void grid_view_monthly_day_boarding_cash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_day_boarding_monthly_fee_cash = total_day_boarding_monthly_fee_cash + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_day_boarding_monthly_fee_cash.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }


        private void Bind_data_two_date_wise_monthly_fee_day_boarding_online()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "MonthlyFee");
                    cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Online");// day school
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rp_view_monthly_day_boarding_online.DataSource = dt;
                        rp_view_monthly_day_boarding_online.DataBind();
                        pnl_monthly_day_scholar_online.Visible = true;
                        ViewState["MONTHLYDayScholar"] = "1";
                    }
                    else
                    {
                        rp_view_monthly_day_boarding_online.DataSource = null;
                        rp_view_monthly_day_boarding_online.DataBind();
                        pnl_monthly_day_scholar_online.Visible = false;
                    }
                }
            }
        }

        private void Bind_data_two_date_wise_monthly_fee_day_boarding_cheque()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Type ", "MonthlyFee");
                cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                cmd.Parameters.AddWithValue("@Paymentmode ", "Cheque");// day school
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount > 0)
                {
                    rp_view_monthly_day_boarding_cheque.DataSource = dt;
                    rp_view_monthly_day_boarding_cheque.DataBind();
                    pnl_monthly_day_scholar_cheque.Visible = true;
                    ViewState["MONTHLYDayScholar"] = "1";
                }
                else
                {
                    rp_view_monthly_day_boarding_cheque.DataSource = null;
                    rp_view_monthly_day_boarding_cheque.DataBind();
                    pnl_monthly_day_scholar_cheque.Visible = false;
                }
            }
        }


        double total_day_boarding_monthly_fee_online = 0;
        protected void grid_view_monthly_day_boarding_online_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_day_boarding_monthly_fee_online = total_day_boarding_monthly_fee_online + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_day_boarding_monthly_fee_online.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }

        #endregion



        #region Hostler fee  admission fee and annual fee
        private void Bind_data_two_date_wise_admission_fee_hostler_cash()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    //string query = "select ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.Class_id,Amount,ad.mode,ad.Date,ad.Slip_no from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session where ar.Session_id =" + My.get_session_id() + " and  ad.parameter_New='HostelAdmissionFee'  and ad.Idate>=" + idate + " and ad.Idate<=" + idate2 + " and ad.mode='Cash' order by Idate asc";

                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate", idate);
                    cmd.Parameters.AddWithValue("@todate", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type", "HostelAdmissionFee");// Hostler  student
                    cmd.Parameters.AddWithValue("@sp_status", "1");
                    cmd.Parameters.AddWithValue("@Paymentmode", "Cash");
                    DataSet ds = My.executeReaderDataSet(cmd);

                    //DataSet ds = mycode.Fill_Data_set(query);
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count != 0)
                    {
                        rp_view_admission_hostal.DataSource = dt;
                        rp_view_admission_hostal.DataBind();
                        pnl_admission_hostel_cash.Visible = true;
                        ViewState["HOSTELADMCollection"] = "1";
                    }
                    else
                    {
                        rp_view_admission_hostal.DataSource = null;
                        rp_view_admission_hostal.DataBind();
                        pnl_admission_hostel_cash.Visible = false;

                    }

                }
            }
        }
        double total_hostal_admission_fee_cash = 0;
        protected void grid_view_admission_hostal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_hostal_admission_fee_cash = total_hostal_admission_fee_cash + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_hostal_admission_fee_cash.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }



        private void Bind_data_two_date_wise_admission_fee_hostler_online()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    //string query = "select ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.Class_id,Amount,ad.mode,ad.Date,ad.Slip_no from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session where ar.Session_id =" + My.get_session_id() + " and  ad.parameter_New='HostelAdmissionFee'  and ad.Idate>=" + idate + " and ad.Idate<=" + idate2 + " and ad.mode!='Cash' order by Idate asc";


                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "HostelAdmissionFee");// Hostler  student
                    cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Online");// day school
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    //int rowcount = ds.Tables[0].Rows.Count;
                    //DataSet ds = mycode.Fill_Data_set(query);
                    //DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count != 0)
                    {
                        rp_view_admission_hostal_online.DataSource = dt;
                        rp_view_admission_hostal_online.DataBind();
                        pnl_admission_hostel_online.Visible = true;
                        ViewState["HOSTELADMCollection"] = "1";
                    }
                    else
                    {
                        rp_view_admission_hostal_online.DataSource = null;
                        rp_view_admission_hostal_online.DataBind();
                        pnl_admission_hostel_online.Visible = false;

                    }

                }
            }
        }
        private void Bind_data_two_date_wise_admission_fee_hostler_cheque()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Type ", "HostelAdmissionFee");// Hostler  student
                cmd.Parameters.AddWithValue("@sp_status ", "1");// day school
                cmd.Parameters.AddWithValue("@Paymentmode ", "Cheque");// day school
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                //int rowcount = ds.Tables[0].Rows.Count;
                //DataSet ds = mycode.Fill_Data_set(query);
                //DataTable dt = ds.Tables[0];

                if (dt.Rows.Count != 0)
                {
                    rp_view_admission_hostal_cheque.DataSource = dt;
                    rp_view_admission_hostal_cheque.DataBind();
                    pnl_admission_hostel_cheque.Visible = true;
                    ViewState["HOSTELADMCollection"] = "1";
                }
                else
                {
                    rp_view_admission_hostal_cheque.DataSource = null;
                    rp_view_admission_hostal_cheque.DataBind();
                    pnl_admission_hostel_cheque.Visible = false;
                }
            }
        }


        double total_hostal_admission_fee_online = 0;
        protected void grid_view_admission_hostal_online_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_hostal_admission_fee_online = total_hostal_admission_fee_online + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_hostal_admission_fee_online.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }


        private void Bind_data_two_date_wise_annual_fee_hostler_cash()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    //string query = "select ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.Class_id,Amount,ad.mode,ad.Date,ad.Slip_no from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session where ar.Session_id =" + My.get_session_id() + " and  ad.parameter_New='HostelAnnualFee'  and ad.Idate>=" + idate + " and ad.Idate<=" + idate2 + " and ad.mode='Cash' order by Idate asc";

                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "HostelAnnualFee");// Hostler  student
                    cmd.Parameters.AddWithValue("@sp_status ", "1");
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Cash");
                    DataSet ds = My.executeReaderDataSet(cmd);
                    //DataTable dt = ds.Tables[0];
                    //int rowcount = ds.Tables[0].Rows.Count;
                    //if (rowcount > 0)
                    //{
                    //DataSet ds = mycode.Fill_Data_set(query);
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count != 0)
                    {
                        rp_view_annul_fee_hostal_cash.DataSource = dt;
                        rp_view_annul_fee_hostal_cash.DataBind();
                        pnl_annual_hostel_cash.Visible = true;
                        ViewState["HOSTELANNUALCollection"] = "1";
                    }
                    else
                    {
                        rp_view_annul_fee_hostal_cash.DataSource = null;
                        rp_view_annul_fee_hostal_cash.DataBind();
                        pnl_annual_hostel_cash.Visible = false;
                    }
                }
            }
        }
        double total_hostal_annual_fee_cash = 0;
        protected void grid_view_annul_fee_hostal_cash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_hostal_annual_fee_cash = total_hostal_annual_fee_cash + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_hostal_annual_fee_cash.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }

        private void Bind_data_two_date_wise_annual_fee_hostler_online()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    //string query = "select ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.Class_id,Amount,ad.mode,ad.Date,ad.Slip_no from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session where ar.Session_id =" + My.get_session_id() + " and  ad.parameter_New='HostelAnnualFee'  and ad.Idate>=" + idate + " and ad.Idate<=" + idate2 + " and ad.mode!='Cash' order by Idate asc";

                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "HostelAnnualFee");// Hostler  student
                    cmd.Parameters.AddWithValue("@sp_status ", "1");
                    cmd.Parameters.AddWithValue("@Paymentmode ", "online");
                    DataSet ds = My.executeReaderDataSet(cmd);
                    //DataTable dt = ds.Tables[0];
                    //int rowcount = ds.Tables[0].Rows.Count;
                    //if (rowcount > 0)
                    //{
                    //DataSet ds = mycode.Fill_Data_set(query);
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count != 0)
                    {
                        rp_view_annul_fee_hostal_online.DataSource = dt;
                        rp_view_annul_fee_hostal_online.DataBind();
                        pnl_annual_hostel_online.Visible = true;
                        ViewState["HOSTELANNUALCollection"] = "1";
                    }
                    else
                    {
                        rp_view_annul_fee_hostal_online.DataSource = null;
                        rp_view_annul_fee_hostal_online.DataBind();
                        pnl_annual_hostel_online.Visible = false;
                    }
                }
            }
        }

        private void Bind_data_two_date_wise_annual_fee_hostler_cheque()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Type ", "HostelAnnualFee");// Hostler  student
                cmd.Parameters.AddWithValue("@sp_status ", "1");
                cmd.Parameters.AddWithValue("@Paymentmode ", "Cheque");
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    rp_view_annul_fee_hostal_cheque.DataSource = dt;
                    rp_view_annul_fee_hostal_cheque.DataBind();
                    pnl_annual_hostel_cheque.Visible = true;
                    ViewState["HOSTELANNUALCollection"] = "1";
                }
                else
                {
                    rp_view_annul_fee_hostal_cheque.DataSource = null;
                    rp_view_annul_fee_hostal_cheque.DataBind();
                    pnl_annual_hostel_cheque.Visible = false;
                }
            }
        }


        double total_hostal_annual_fee_online = 0;
        protected void grid_view_annul_fee_hostal_online_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_hostal_annual_fee_online = total_hostal_annual_fee_online + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_hostal_annual_fee_online.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }

        private void Bind_data_two_date_wise_monthly_fee_hostler_cash()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    //string query = "select ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.Class_id,Amount,ad.mode,ad.Date,ad.Slip_no from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session where ar.Session_id =" + My.get_session_id() + " and  ad.parameter_New='HostelMonthlyFee'  and ad.Idate>=" + idate + " and ad.Idate<=" + idate2 + " and ad.mode='Cash' order by Idate asc";

                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "HostelMonthlyFee");// Hostler  student
                    cmd.Parameters.AddWithValue("@sp_status ", "1");
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Cash");
                    DataSet ds = My.executeReaderDataSet(cmd);
                    //DataTable dt = ds.Tables[0];
                    //int rowcount = ds.Tables[0].Rows.Count;
                    //if (rowcount > 0)
                    //{
                    //DataSet ds = mycode.Fill_Data_set(query);
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count != 0)
                    {
                        rp_view_monthley_fee_hostal_cash.DataSource = dt;
                        rp_view_monthley_fee_hostal_cash.DataBind();
                        pnl_monthly_hostel_cash.Visible = true;
                        ViewState["HOSTELMNTHLYCollection"] = "1";
                    }
                    else
                    {
                        rp_view_monthley_fee_hostal_cash.DataSource = null;
                        rp_view_monthley_fee_hostal_cash.DataBind();
                        pnl_monthly_hostel_cash.Visible = false;

                    }

                }
            }
        }



        double total_hostal_monthly_fee_cash = 0;
        protected void grid_view_monthley_fee_hostal_cash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_hostal_monthly_fee_cash = total_hostal_monthly_fee_cash + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_hostal_monthly_fee_cash.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }


        private void Bind_data_two_date_wise_monthly_fee_hostler_online()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    //string query = "select ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.Class_id,Amount,ad.mode,ad.Date,ad.Slip_no from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session where ar.Session_id =" + My.get_session_id() + " and  ad.parameter_New='HostelMonthlyFee'  and ad.Idate>=" + idate + " and ad.Idate<=" + idate2 + " and ad.mode!='Cash' order by Idate asc";
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Type ", "HostelMonthlyFee");// Hostler  student monthly
                    cmd.Parameters.AddWithValue("@sp_status ", "1");
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Online");
                    DataSet ds = My.executeReaderDataSet(cmd);
                    //DataTable dt = ds.Tables[0];
                    //int rowcount = ds.Tables[0].Rows.Count;
                    //if (rowcount > 0)
                    //{
                    //DataSet ds = mycode.Fill_Data_set(query);
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count != 0)
                    {
                        rp_view_monthley_fee_hostal_online.DataSource = dt;
                        rp_view_monthley_fee_hostal_online.DataBind();
                        pnl_monthly_hostel_online.Visible = true;
                        ViewState["HOSTELMNTHLYCollection"] = "1";
                    }
                    else
                    {
                        rp_view_monthley_fee_hostal_online.DataSource = null;
                        rp_view_monthley_fee_hostal_online.DataBind();
                        pnl_monthly_hostel_online.Visible = false;
                    }
                }
            }
        }


        private void Bind_data_two_date_wise_monthly_fee_hostler_cheque()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Type ", "HostelMonthlyFee");// Hostler  student monthly
                cmd.Parameters.AddWithValue("@sp_status ", "1");
                cmd.Parameters.AddWithValue("@Paymentmode ", "Cheque");
                DataSet ds = My.executeReaderDataSet(cmd);
                //DataTable dt = ds.Tables[0];
                //int rowcount = ds.Tables[0].Rows.Count;
                //if (rowcount > 0)
                //{
                //DataSet ds = mycode.Fill_Data_set(query);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count != 0)
                {
                    rp_view_monthley_fee_hostal_cheque.DataSource = dt;
                    rp_view_monthley_fee_hostal_cheque.DataBind();
                    pnl_monthly_hostel_cheque.Visible = true;
                    ViewState["HOSTELMNTHLYCollection"] = "1";
                }
                else
                {
                    rp_view_monthley_fee_hostal_cheque.DataSource = null;
                    rp_view_monthley_fee_hostal_cheque.DataBind();
                    pnl_monthly_hostel_cheque.Visible = false;

                }
            }
        }


        double total_hostal_monthly_fee_online = 0;
        protected void grid_view_monthley_fee_hostal_online_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_hostal_monthly_fee_online = total_hostal_monthly_fee_online + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_hostal_monthly_fee_online.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }


        #endregion


        protected void btn_find_Click1(object sender, EventArgs e)
        {


            btn_excels.Visible = false;
            print1.Visible = false;
            NotFoundS.Visible = false;
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    btn_excels.Visible = false;
                    print1.Visible = false;
                    NotFoundS.Visible = true;
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    btn_excels.Visible = true;

                    NotFoundS.Visible = false;
                    Find_data_day_end_report();

                    if (ViewState["Is_Print"].ToString() == "1")
                    {
                        print1.Visible = true;
                    }
                    else
                    {
                        print1.Visible = false;

                    }
                }
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=DayEndSummary.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        pnl_grid.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {

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

        #region form seal report
        private void Bind_data_two_date_wise_form_sale()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@sp_status ", "2");
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Cash");
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rp_view_form_Sale_cash.DataSource = dt;
                        rp_view_form_Sale_cash.DataBind();
                        pnl_form_sale_cash.Visible = true;
                        ViewState["FormSaleCollection"] = "1";
                    }
                    else
                    {
                        rp_view_form_Sale_cash.DataSource = null;
                        rp_view_form_Sale_cash.DataBind();
                        pnl_form_sale_cash.Visible = false;
                    }
                }
            }
        }


        private void Bind_data_two_date_wise_fee_form_sale()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@sp_status ", "2");
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Online");
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rp_view_form_Sale_online.DataSource = dt;
                        rp_view_form_Sale_online.DataBind();
                        pnl_form_sale_online.Visible = true;
                        ViewState["FormSaleCollection"] = "1";
                    }
                    else
                    {
                        rp_view_form_Sale_online.DataSource = null;
                        rp_view_form_Sale_online.DataBind();
                        pnl_form_sale_online.Visible = false;
                    }
                }
            }
        }
        private void Bind_data_two_date_wise_fee_form_cheque()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "2");
                cmd.Parameters.AddWithValue("@Paymentmode ", "Cheque");
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount > 0)
                {
                    rp_view_form_Sale_cheque.DataSource = dt;
                    rp_view_form_Sale_cheque.DataBind();
                    pnl_form_sale_cheque.Visible = true;
                    ViewState["FormSaleCollection"] = "1";
                }
                else
                {
                    rp_view_form_Sale_cheque.DataSource = null;
                    rp_view_form_Sale_cheque.DataBind();
                    pnl_form_sale_cheque.Visible = false;
                }
            }
        }


        double total_form_Sale_online = 0;
        protected void grid_view_form_Sale_online_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_form_Sale_online = total_form_Sale_online + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_form_Sale_online.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }
        #endregion



        #region fee forther


        private void Bind_data_two_date_wise_other_fee_cash()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@sp_status ", "3");
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Cash");
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rp_view_other_fee.DataSource = dt;
                        rp_view_other_fee.DataBind();
                        pnl_other_fees_cash.Visible = true;
                        ViewState["OTHERFeesCollection"] = "1";
                    }
                    else
                    {
                        rp_view_other_fee.DataSource = null;
                        rp_view_other_fee.DataBind();
                        pnl_other_fees_cash.Visible = false;
                    }
                }
            }
        }

        private void Bind_data_two_date_wise_other_fee_online()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@sp_status ", "3");
                    cmd.Parameters.AddWithValue("@Paymentmode ", "Online");
                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rp_view_other_fee_online.DataSource = dt;
                        rp_view_other_fee_online.DataBind();
                        pnl_other_fees_online.Visible = true;
                        ViewState["OTHERFeesCollection"] = "1";
                    }
                    else
                    {
                        rp_view_other_fee_online.DataSource = null;
                        rp_view_other_fee_online.DataBind();
                        pnl_other_fees_online.Visible = false;
                    }
                }
            }
        }

        private void Bind_data_two_date_wise_other_fee_cheque()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "3");
                cmd.Parameters.AddWithValue("@Paymentmode ", "Cheque");
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount > 0)
                {
                    rp_view_other_fee_cheque.DataSource = dt;
                    rp_view_other_fee_cheque.DataBind();
                    pnl_other_fees_cheque.Visible = true;
                    ViewState["OTHERFeesCollection"] = "1";
                }
                else
                {
                    rp_view_other_fee_cheque.DataSource = null;
                    rp_view_other_fee_cheque.DataBind();
                    pnl_other_fees_cheque.Visible = false;
                }
            }
        }




        #endregion

        private void Bind_over_all_summery_expenses()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "10");// over  all summery
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 1)
                {
                    double a = My.toDouble(dt.Rows[0]["AmountCash"].ToString());
                    double a1 = My.toDouble(dt.Rows[0]["AmountOnline"].ToString());
                    double ach = My.toDouble(dt.Rows[0]["AmountCheque"].ToString());
                    double a2 = My.toDouble(dt.Rows[0]["ttl_amount"].ToString());
                    lbl_exp_cash.Text = a.ToString("0.00");
                    lbl_exp_cheque.Text = ach.ToString("0.00");
                    lbl_exp_online.Text = a1.ToString("0.00");
                    //lbl_exp_both.Text = a2.ToString("0.00");
                }
                else if (rowcount == 2)
                {
                    double a = My.toDouble(dt.Rows[0]["AmountCash"].ToString()) + My.toDouble(dt.Rows[1]["AmountCash"].ToString());
                    double a1 = My.toDouble(dt.Rows[0]["AmountOnline"].ToString()) + My.toDouble(dt.Rows[1]["AmountOnline"].ToString());
                    double ach = My.toDouble(dt.Rows[0]["AmountCheque"].ToString()) + My.toDouble(dt.Rows[1]["AmountCheque"].ToString());
                    double a2 = My.toDouble(dt.Rows[0]["ttl_amount"].ToString()) + My.toDouble(dt.Rows[1]["ttl_amount"].ToString());
                    lbl_exp_cash.Text = a.ToString("0.00");
                    lbl_exp_online.Text = a1.ToString("0.00");
                    lbl_exp_cheque.Text = ach.ToString("0.00");
                    //lbl_exp_both.Text = a2.ToString("0.00");
                }

                else
                {
                    lbl_exp_cash.Text = "0.00";
                    lbl_exp_online.Text = "0.00";
                    lbl_exp_cheque.Text = "0.00";
                    //lbl_exp_both.Text = "0.00";
                }

                lbl_ttl_blnce_cash.Text = (My.toDouble(lbl_ttl_cash.Text) - My.toDouble(lbl_exp_cash.Text)).ToString("0.00");
                lbl_ttl_blnce_onlne.Text = (My.toDouble(lbl_ttl_online.Text) - My.toDouble(lbl_exp_online.Text)).ToString("0.00");
                lbl_ttl_blnce_cheque.Text = (My.toDouble(lbl_ttl_cheque.Text) - My.toDouble(lbl_exp_cheque.Text)).ToString("0.00");
                lbl_balance_amount.Text = (My.toDouble(lbl_ttl_blnce_cash.Text) + My.toDouble(lbl_ttl_blnce_onlne.Text) + My.toDouble(lbl_ttl_blnce_cheque.Text)).ToString("0.00");
            }
        }

        private void Bind_over_all_summery()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                    cmd.Parameters.AddWithValue("@fromdate ", idate);
                    cmd.Parameters.AddWithValue("@todate ", idate2);
                    cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@sp_status ", "14");// over  all summery

                    DataSet ds = My.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount > 0)
                    {
                        rd_view.DataSource = dt;
                        rd_view.DataBind();
                    }
                    else
                    {
                        rd_view.DataSource = null;
                        rd_view.DataBind();
                    }
                }
            }
        }

        private void Bind_over_all_summery_cheque()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "16");// over  all summery

                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount > 0)
                {
                    rd_view_cheque.DataSource = dt;
                    rd_view_cheque.DataBind();
                }
                else
                {
                    rd_view_cheque.DataSource = null;
                    rd_view_cheque.DataBind();
                }
            }
        }


        private void Bind_over_all_summery_online()
        {
            lbl_ttl_online.Text = "0.00";
            lbl_balance_amount.Text = "0.00";
            lbl_ttl_blnce_onlne.Text = "0.00";
            lbl_exp_online.Text = "0.00";
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                lbl_ttl_online.Text = "0.00";
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name ", ddl_class.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session ", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "15");// over  all summery
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount > 0)
                {
                    rd_overall_online.DataSource = dt;
                    rd_overall_online.DataBind();
                }
                else
                {
                    rd_overall_online.DataSource = null;
                    rd_overall_online.DataBind();
                }
            }
        }



        double total_over_all = 0; double total_over_all_online = 0; double total_over_all_ttl = 0;
        protected void grid_data_overall_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                Label lbl_amount_online = (Label)e.Row.FindControl("lbl_amount_online");
                Label lbl_amount_ttl = (Label)e.Row.FindControl("lbl_amount_ttl");
                decimal value;

                if (lbl_amount.Text != "")
                {
                    total_over_all = total_over_all + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }

                //======================
                if (lbl_amount_online.Text != "")
                {
                    total_over_all_online = total_over_all_online + Convert.ToDouble(lbl_amount_online.Text);
                }
                if (decimal.TryParse(lbl_amount_online.Text, out value))
                {
                    lbl_amount_online.Text = value.ToString("0.00");
                }

                //======================
                if (lbl_amount_ttl.Text != "")
                {
                    total_over_all_ttl = total_over_all_ttl + Convert.ToDouble(lbl_amount_ttl.Text);
                }
                if (decimal.TryParse(lbl_amount_ttl.Text, out value))
                {
                    lbl_amount_ttl.Text = value.ToString("0.00");
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_over_all.ToString("0.00");

                //============================
                Label lbl_total_online_amount = (Label)e.Row.FindControl("lbl_total_online_amount");
                lbl_total_online_amount.Text = total_over_all_online.ToString("0.00");

                //============================
                Label lbl_total_online_cash_amount = (Label)e.Row.FindControl("lbl_total_online_cash_amount");
                lbl_total_online_cash_amount.Text = total_over_all_ttl.ToString("0.00");
            }
        }
        double general_exp_online = 0;
        protected void grd_general_exp_online_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[6].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_payment_amount");
                if (lbl_amount.Text != "")
                {
                    general_exp_online = general_exp_online + Convert.ToDouble(lbl_amount.Text);
                }
                decimal value;
                if (decimal.TryParse(lbl_amount.Text.Trim(), out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = general_exp_online.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[6].CssClass = "amountFStyle";
            }
        }

        double general_exp_cash = 0;
        protected void grd_general_exp_cash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[6].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_payment_amount");
                if (lbl_amount.Text != "")
                {
                    general_exp_cash = general_exp_cash + Convert.ToDouble(lbl_amount.Text);
                }
                decimal value;
                if (decimal.TryParse(lbl_amount.Text.Trim(), out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = general_exp_cash.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[6].CssClass = "amountFStyle";
            }
        }



        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                Label lbl_sequence = (Label)e.Item.FindControl("lbl_sequence") as Label;
                decimal value;

                if (lbl_amount.Text != "")
                {
                    total_over_all = total_over_all + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }

                //======================
                //if (lbl_amount_online.Text != "")
                //{
                //    total_over_all_online = total_over_all_online + Convert.ToDouble(lbl_amount_online.Text);
                //}
                //if (decimal.TryParse(lbl_amount_online.Text, out value))
                //{
                //    lbl_amount_online.Text = value.ToString("0.00");
                //}

                //======================
                //if (lbl_amount_ttl.Text != "")
                //{
                //    total_over_all_ttl = total_over_all_ttl + Convert.ToDouble(lbl_amount_ttl.Text);
                //}
                //if (decimal.TryParse(lbl_amount_ttl.Text, out value))
                //{
                //    lbl_amount_ttl.Text = value.ToString("0.00");
                //}
            }

            lbl_ttl_cash.Text = total_over_all.ToString("0.00");

            //============================ 
            //lbl_ttl_online.Text = total_over_all_online.ToString("0.00");

            //============================ 
            //ttl_both.Text = total_over_all_ttl.ToString("0.00");
        }

        double total_over_all_online_c = 0;
        protected void rd_overall_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;

                if (lbl_amount.Text != "")
                {
                    total_over_all_online_c = total_over_all_online_c + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_ttl_online.Text = total_over_all_online_c.ToString("0.00");
        }

        double general_exp_cash_nov = 0;
        protected void grd_general_Exp_no_vendor_Cash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[6].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_payment_amount");
                if (lbl_amount.Text != "")
                {
                    general_exp_cash_nov = general_exp_cash_nov + Convert.ToDouble(lbl_amount.Text);
                }
                decimal value;
                if (decimal.TryParse(lbl_amount.Text.Trim(), out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = general_exp_cash_nov.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[6].CssClass = "amountFStyle";
            }
        }


        double general_exp_online_nov = 0;
        protected void grd_general_Exp_no_vendor_online_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[6].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_payment_amount");
                if (lbl_amount.Text != "")
                {
                    general_exp_online_nov = general_exp_online_nov + Convert.ToDouble(lbl_amount.Text);
                }
                decimal value;
                if (decimal.TryParse(lbl_amount.Text.Trim(), out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = general_exp_online_nov.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[6].CssClass = "amountFStyle";
            }
        }

        double total_day_boarding_annual_fee_cheque = 0;
        protected void grid_view_annual_day_boarding_cheque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_day_boarding_annual_fee_cheque = total_day_boarding_annual_fee_cheque + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_day_boarding_annual_fee_cheque.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }


        double total_day_boarding_admission_fee_cheque = 0;
        protected void grid_view_admission_day_boarding_cheque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_day_boarding_admission_fee_cheque = total_day_boarding_admission_fee_cheque + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_day_boarding_admission_fee_cheque.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
        }


        double total_day_boarding_monthly_fee_cheque = 0;
        protected void grid_view_monthly_day_boarding_cheque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_day_boarding_monthly_fee_cheque = total_day_boarding_monthly_fee_cheque + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_day_boarding_monthly_fee_cheque.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }


        double total_hostal_admission_fee_cheque = 0;
        protected void grid_view_admission_hostal_cheque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_hostal_admission_fee_cheque = total_hostal_admission_fee_cheque + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_hostal_admission_fee_cheque.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }


        double total_hostal_annual_fee_cheque = 0;
        protected void grid_view_annul_fee_hostal_cheque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_hostal_annual_fee_cheque = total_hostal_annual_fee_cheque + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_hostal_annual_fee_cheque.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }



        double total_hostal_monthly_fee_cheque = 0;
        protected void grid_view_monthley_fee_hostal_cheque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_hostal_monthly_fee_cheque = total_hostal_monthly_fee_cheque + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_hostal_monthly_fee_cheque.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }


        double total_form_Sale_cheque = 0;
        protected void grid_view_form_Sale_cheque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");
                if (lbl_amount.Text != "")
                {
                    total_form_Sale_cheque = total_form_Sale_cheque + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = total_form_Sale_cheque.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[10].CssClass = "amountFStyle";
            }
        }



        double general_exp_cheque = 0;
        protected void grd_general_exp_cheque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[6].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_payment_amount");
                if (lbl_amount.Text != "")
                {
                    general_exp_cheque = general_exp_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                decimal value;
                if (decimal.TryParse(lbl_amount.Text.Trim(), out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = general_exp_cheque.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[6].CssClass = "amountFStyle";
            }

        }

        double general_exp_cheque_nov = 0;
        protected void grd_general_Exp_no_vendor_cheque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[6].CssClass = "amountHStyle";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].CssClass = "amountStyle";
                Label lbl_amount = (Label)e.Row.FindControl("lbl_payment_amount");
                if (lbl_amount.Text != "")
                {
                    general_exp_cheque_nov = general_exp_cheque_nov + Convert.ToDouble(lbl_amount.Text);
                }
                decimal value;
                if (decimal.TryParse(lbl_amount.Text.Trim(), out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_cash_amount = (Label)e.Row.FindControl("lbl_total_cash_amount");
                lbl_total_cash_amount.Text = general_exp_cheque_nov.ToString("0.00");
                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[6].CssClass = "amountFStyle";
            }
        }


        double total_over_all_cheque = 0;
        protected void rd_view_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                Label lbl_sequence = (Label)e.Item.FindControl("lbl_sequence") as Label;
                decimal value;

                if (lbl_amount.Text != "")
                {
                    total_over_all_cheque = total_over_all_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_ttl_cheque.Text = total_over_all_cheque.ToString("0.00");
        }



        double total_adm_day_boarding_cash = 0;
        protected void rp_view_admission_day_boarding_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel") as LinkButton;

                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }

                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;

                if (lbl_amount.Text != "")
                {
                    total_adm_day_boarding_cash = total_adm_day_boarding_cash + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_ttl_adm_day_boarding_amt_cash.Text = total_adm_day_boarding_cash.ToString("0.00");
        }
        double total_adm_day_boarding_online = 0;
        protected void rp_view_admission_day_boarding_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEditOnline") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDelOnline") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }

                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_adm_day_boarding_online = total_adm_day_boarding_online + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_ttl_adm_day_boarding_amt_online.Text = total_adm_day_boarding_online.ToString("0.00");
        }


        double total_adm_day_boarding_cheque = 0;
        protected void rp_view_admission_day_boarding_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEditCheque") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDelCheque") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_adm_day_boarding_cheque = total_adm_day_boarding_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_ttl_adm_day_boarding_amt_cheque.Text = total_adm_day_boarding_cheque.ToString("0.00");
        }

        double total_annual_day_boarding_cash = 0;
        protected void rp_view_annual_day_boarding_cash_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit001") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel001") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_annual_day_boarding_cash = total_annual_day_boarding_cash + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_annual_day_boarding_cash.Text = total_annual_day_boarding_cash.ToString("0.00");
        }

        double total_annual_day_boarding_online = 0;
        protected void rp_view_annual_day_boarding_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit002") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel002") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_annual_day_boarding_online = total_annual_day_boarding_online + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_annual_day_boarding_online.Text = total_annual_day_boarding_online.ToString("0.00");
        }
        double total_annual_day_boarding_cheque = 0;
        protected void rp_view_annual_day_boarding_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit003") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel003") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_annual_day_boarding_cheque = total_annual_day_boarding_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_annual_day_boarding_cheque.Text = total_annual_day_boarding_cheque.ToString("0.00");
        }

        double total_monthly_day_boarding_cash = 0;
        protected void rp_view_monthly_day_boarding_cash_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit004") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel004") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_monthly_day_boarding_cash = total_monthly_day_boarding_cash + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_monthly_day_boarding_cash.Text = total_monthly_day_boarding_cash.ToString("0.00");

        }

        double total_monthly_day_boarding_online = 0;
        protected void rp_view_monthly_day_boarding_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit005") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel005") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_monthly_day_boarding_online = total_monthly_day_boarding_online + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_monthly_day_boarding_online.Text = total_monthly_day_boarding_online.ToString("0.00");
        }

        double total_monthly_day_boarding_cheque = 0;
        protected void rp_view_monthly_day_boarding_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit006") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel006") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_monthly_day_boarding_cheque = total_monthly_day_boarding_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_monthly_day_boarding_cheque.Text = total_monthly_day_boarding_cheque.ToString("0.00");
        }

        double total_admission_hostal_cash = 0;
        protected void rp_view_admission_hostal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit007") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel007") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_admission_hostal_cash = total_admission_hostal_cash + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_admission_hostal_cash.Text = total_admission_hostal_cash.ToString("0.00");
        }




        double total_admission_hostal_online = 0;
        protected void rp_view_admission_hostal_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit008") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel008") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_admission_hostal_online = total_admission_hostal_online + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_admission_hostal_online.Text = total_admission_hostal_online.ToString("0.00");
        }


        double total_admission_hostal_cheque = 0;
        protected void rp_view_admission_hostal_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit009") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel009") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_admission_hostal_cheque = total_admission_hostal_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }

            lbl_admission_hostal_cheque.Text = total_admission_hostal_cheque.ToString("0.00");
        }

        double total_annul_fee_hostal_cash = 0;
        protected void rp_view_annul_fee_hostal_cash_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit010") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel010") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_annul_fee_hostal_cash = total_annul_fee_hostal_cash + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_annul_fee_hostal_cash.Text = total_annul_fee_hostal_cash.ToString("0.00");
        }


        double total_annul_fee_hostal_online = 0;
        protected void rp_view_annul_fee_hostal_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit011") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel011") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_annul_fee_hostal_online = total_annul_fee_hostal_online + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_annul_fee_hostal_online.Text = total_annul_fee_hostal_online.ToString("0.00");
        }

        double total_annul_fee_hostal_cheque = 0;
        protected void rp_view_annul_fee_hostal_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit012") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel012") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_annul_fee_hostal_cheque = total_annul_fee_hostal_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_annul_fee_hostal_cheque.Text = total_annul_fee_hostal_cheque.ToString("0.00");
        }


        double total_monthley_fee_hostal_cash = 0;
        protected void rp_view_monthley_fee_hostal_cash_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit013") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel013") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_monthley_fee_hostal_cash = total_monthley_fee_hostal_cash + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_monthley_fee_hostal_cash.Text = total_monthley_fee_hostal_cash.ToString("0.00");
        }


        double total_monthley_fee_hostal_online = 0;
        protected void rp_view_monthley_fee_hostal_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit014") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel014") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_monthley_fee_hostal_online = total_monthley_fee_hostal_online + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_monthley_fee_hostal_online.Text = total_monthley_fee_hostal_online.ToString("0.00");
        }

        double total_monthley_fee_hostal_cheque = 0;
        protected void rp_view_monthley_fee_hostal_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit015") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel015") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_monthley_fee_hostal_cheque = total_monthley_fee_hostal_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_monthley_fee_hostal_cheque.Text = total_monthley_fee_hostal_cheque.ToString("0.00");
        }


        double total_form_sale_cash = 0;
        protected void rp_view_form_Sale_cash_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit016") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel016") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_form_sale_cash = total_form_sale_cash + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_total_form_sale_cash.Text = total_form_sale_cash.ToString("0.00");
        }

        double total_form_sale_online = 0;
        protected void rp_view_form_Sale_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit017") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel017") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_form_sale_online = total_form_sale_online + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_total_form_sale_online.Text = total_form_sale_online.ToString("0.00");
        }

        double total_form_sale_cheque = 0;
        protected void rp_view_form_Sale_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit018") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel018") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_form_sale_cheque = total_form_sale_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_total_form_sale_cheque.Text = total_form_sale_cheque.ToString("0.00");
        }


        double total_other_fee_cash = 0;
        protected void rp_view_other_fee_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit019") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel019") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_other_fee_cash = total_other_fee_cash + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_total_other_fee_cash.Text = total_other_fee_cash.ToString("0.00");
        }

        double total_other_fee_online = 0;
        protected void rp_view_other_fee_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit020") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel020") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_other_fee_online = total_other_fee_online + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_total_other_fee_online.Text = total_other_fee_online.ToString("0.00");
        }

        double total_other_fee_cheque = 0;
        protected void rp_view_other_fee_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit021") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel021") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_other_fee_cheque = total_other_fee_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_total_other_fee_cheque.Text = total_other_fee_cheque.ToString("0.00");
        }

        double total_vendor_general_exp_cash = 0;
        protected void rp_general_exp_cash_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit022") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel022") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_vendor_general_exp_cash = total_vendor_general_exp_cash + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_vendor_general_exp_cash.Text = total_vendor_general_exp_cash.ToString("0.00");
        }

        double total_vendor_general_exp_online = 0;
        protected void rp_general_exp_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit023") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel023") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_vendor_general_exp_online = total_vendor_general_exp_online + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_vendor_general_exp_online.Text = total_vendor_general_exp_online.ToString("0.00");
        }

        double total_vendor_general_exp_cheque = 0;
        protected void rp_general_exp_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit024") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel024") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_vendor_general_exp_cheque = total_vendor_general_exp_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_vendor_general_exp_cheque.Text = total_vendor_general_exp_cheque.ToString("0.00");
        }

        double total_general_exp_no_vendor_cash = 0;
        protected void rp_general_Exp_no_vendor_Cash_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit025") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel025") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_general_exp_no_vendor_cash = total_general_exp_no_vendor_cash + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_total_general_exp_no_vendor_cash.Text = total_general_exp_no_vendor_cash.ToString("0.00");
        }


        double total_general_exp_no_vendor_online = 0;
        protected void rp_general_Exp_no_vendor_online_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit026") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel026") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_general_exp_no_vendor_online = total_general_exp_no_vendor_online + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_total_general_exp_no_vendor_online.Text = total_general_exp_no_vendor_online.ToString("0.00");
        }

        double total_general_exp_no_vendor_cheque = 0;
        protected void rp_general_Exp_no_vendor_cheque_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit027") as LinkButton;
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel027") as LinkButton;
                if (ViewState["Is_delete"].ToString() == "1")
                {

                }
                else
                {
                    lnkDel.CssClass = "lnkdeletEDSB";
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                }
                else
                {
                    lnkEdit.CssClass = "lnkediTDSB";
                }
                Label lbl_amount = (Label)e.Item.FindControl("lbl_amount") as Label;
                decimal value;
                if (lbl_amount.Text != "")
                {
                    total_general_exp_no_vendor_cheque = total_general_exp_no_vendor_cheque + Convert.ToDouble(lbl_amount.Text);
                }
                if (decimal.TryParse(lbl_amount.Text, out value))
                {
                    lbl_amount.Text = value.ToString("0.00");
                }
            }
            lbl_total_general_exp_no_vendor_cheque.Text = total_general_exp_no_vendor_cheque.ToString("0.00");
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }


        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_pay_mode.Text == "Cash")
                {
                    Update_payment();
                }
                else
                {
                    if (txt_transaction_no.Text == "")
                    {
                        txt_transaction_no.Focus();
                        Alertme("Please enter transaction no.", "warning");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                    else
                    {
                        Update_payment();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void Update_payment()
        {
            if (ViewState["UpdateType"].ToString() == "FormSale")
            {
                string qry = "update Form_sale_details set date='" + tx_payment_date.Text + "',idate='" + My.DateConvertToIdate(tx_payment_date.Text) + "',Payment_Mode='" + ddl_pay_mode.Text + "',Transaction_no='" + txt_transaction_no.Text + "',Remarks='" + txt_remarks.Text + "' where Id='" + ViewState["EdtIdS"].ToString() + "'";
                My.exeSql(qry);
                save_history();
                Find_data_day_end_report();
                Alertme("Record has been updated successfully.", "success");
            }
            else if (ViewState["UpdateType"].ToString() == "OtherFee")
            {
                string qry = "update Other_Fee_Taken_For_Student set Payment_date='" + tx_payment_date.Text + "',Payment_Idate='" + My.DateConvertToIdate(tx_payment_date.Text) + "',Payment_mode='" + ddl_pay_mode.Text + "',Transaction_no='" + txt_transaction_no.Text + "',Remarks='" + txt_remarks.Text + "' where Id='" + ViewState["EdtIdS"].ToString() + "'";
                My.exeSql(qry);
                save_history();
                Find_data_day_end_report();
                Alertme("Record has been updated successfully.", "success");
            }
            else if (ViewState["UpdateType"].ToString() == "GeneralExpenseVendor")
            {
                string qry = "update Vendor_general_expense set Date='" + tx_payment_date.Text + "',Payment_idate='" + My.DateConvertToIdate(tx_payment_date.Text) + "',Payment_mode='" + ddl_pay_mode.Text + "',Check_no='" + txt_transaction_no.Text + "',Remarks='" + txt_remarks.Text + "' where Id='" + ViewState["EdtIdS"].ToString() + "'";
                My.exeSql(qry);
                save_history();
                Find_data_day_end_report();
                Alertme("Record has been updated successfully.", "success");
            }
            else if (ViewState["UpdateType"].ToString() == "GeneralExpense")
            {
                string qry = "update General_expense set Date='" + tx_payment_date.Text + "',Idate='" + My.DateConvertToIdate(tx_payment_date.Text) + "',Payment_mode='" + ddl_pay_mode.Text + "',Transaction_no='" + txt_transaction_no.Text + "',Description='" + txt_remarks.Text + "' where Id='" + ViewState["EdtIdS"].ToString() + "'";
                My.exeSql(qry);
                save_history();
                Find_data_day_end_report();
                Alertme("Record has been updated successfully.", "success");
            }
            else
            {
                string qry = "update Student_Payment_History set Date='" + tx_payment_date.Text + "',Idate='" + My.DateConvertToIdate(tx_payment_date.Text) + "',mode='" + ddl_pay_mode.Text + "',Pay_mode_transaction_no='" + txt_transaction_no.Text + "',Remarks='" + txt_remarks.Text + "' where Slip_no='" + ViewState["updtSlipNo"].ToString() + "'; update Admission_fee_collection set Date = '" + tx_payment_date.Text + "', idate = '" + My.DateConvertToIdate(tx_payment_date.Text) + "', Payment_mode = '" + ddl_pay_mode.Text + "', remark = '" + txt_remarks.Text + "' where Slip_no = '" + ViewState["updtSlipNo"].ToString() + "'; update Annual_fee_collection set Date = '" + tx_payment_date.Text + "', idate = '" + My.DateConvertToIdate(tx_payment_date.Text) + "', Payment_mode = '" + ddl_pay_mode.Text + "', remark = '" + txt_remarks.Text + "' where Slip_no = '" + ViewState["updtSlipNo"].ToString() + "'; update Monthly_Fee_Collection_Slip set Date = '" + tx_payment_date.Text + "', idate = '" + My.DateConvertToIdate(tx_payment_date.Text) + "' where slipno = '" + ViewState["updtSlipNo"].ToString() + "'";
                My.exeSql(qry);
                save_history();
                Find_data_day_end_report();
                Alertme("Record has been updated successfully.", "success");
            }
        }



        private void save_history()
        {
            SqlCommand cmd;
            string query = "INSERT INTO Payment_update_history (Slip_no,Old_date,Old_idate,Old_mode,Old_transaction,Old_remarks,Updated_date,Updated_idate,Updated_mode,Updated_transaction,Updated_remarks,Created_by,Created_date,Created_idate,Payment_type) values (@Slip_no,@Old_date,@Old_idate,@Old_mode,@Old_transaction,@Old_remarks,@Updated_date,@Updated_idate,@Updated_mode,@Updated_transaction,@Updated_remarks,@Created_by,@Created_date,@Created_idate,@Payment_type)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Slip_no", ViewState["updtSlipNo"].ToString());
            cmd.Parameters.AddWithValue("@Old_date", ViewState["updtpayDate"].ToString());
            cmd.Parameters.AddWithValue("@Old_idate", My.DateConvertToIdate(ViewState["updtpayDate"].ToString()));
            cmd.Parameters.AddWithValue("@Old_mode", ViewState["updtpayMode"].ToString());
            cmd.Parameters.AddWithValue("@Old_transaction", ViewState["updtpayModeTransactionNo"].ToString());
            cmd.Parameters.AddWithValue("@Old_remarks", ViewState["updtpayRemark"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", tx_payment_date.Text);
            cmd.Parameters.AddWithValue("@Updated_idate", My.DateConvertToIdate(tx_payment_date.Text));
            cmd.Parameters.AddWithValue("@Updated_mode", ddl_pay_mode.Text);
            cmd.Parameters.AddWithValue("@Updated_transaction", txt_transaction_no.Text);
            cmd.Parameters.AddWithValue("@Updated_remarks", txt_remarks.Text);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Payment_type", ViewState["UpdateType"].ToString());
            if (My.InsertUpdateData(cmd))
            {
            }
        }

        #region DeleteBillL
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }
        protected void btn_conf_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_reason_for_delete.Text == "")
                {
                    txt_reason_for_delete.Focus();
                    Alertme("Please enter reason for delete.", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
                }
                else
                {
                    delete_data(ViewState["dltebillNo"].ToString());
                    Find_data_day_end_report();
                    Alertme("Record has been deleted successfully.", "success");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void delete_data(string slip_no)
        {
            if (ViewState["UpdateType"].ToString() == "FormSale")
            {
                string qry = "delete from Form_sale_details where Id=" + ViewState["dlteIdNo"].ToString() + "";
                My.exeSql(qry);
                save_deleted_history();
            }
            else if (ViewState["UpdateType"].ToString() == "OtherFee")
            {
                string qry = "delete from Other_Fee_Taken_For_Student where Id=" + ViewState["dlteIdNo"].ToString() + "";
                My.exeSql(qry);
                save_deleted_history();
            }
            else if (ViewState["UpdateType"].ToString() == "GeneralExpenseVendor")
            {
                string qry = "delete from Vendor_general_expense where Id=" + ViewState["dlteIdNo"].ToString() + "";
                My.exeSql(qry);
                save_deleted_history();
            }

            else if (ViewState["UpdateType"].ToString() == "GeneralExpense")
            {
                string qry = "delete from General_expense where Id=" + ViewState["dlteIdNo"].ToString() + "";
                My.exeSql(qry);
                save_deleted_history();
            }
            else
            {
                #region Delete FEES
                DataTable dtdlt = My.dataTable("select *,(select top 1 hosteltaken from admission_registor where session=Student_Payment_History.Session and admissionserialnumber=Student_Payment_History.Addmission_no and Class_id=Student_Payment_History.Class_id) as hosteltaken from Student_Payment_History where Slip_no='" + slip_no + "'");
                if (dtdlt.Rows.Count > 0)
                {
                    string type = dtdlt.Rows[0]["Type"].ToString(); // "Monthly";
                    string adm_No = dtdlt.Rows[0]["Addmission_no"].ToString();
                    string class_id = dtdlt.Rows[0]["Class_id"].ToString();
                    string Branchid = dtdlt.Rows[0]["Branch"].ToString();
                    string sessions = dtdlt.Rows[0]["Session"].ToString();
                    string amount = dtdlt.Rows[0]["Amount"].ToString();
                    string transection_in = dtdlt.Rows[0]["Transection_in"].ToString();
                    ViewState["hosteltaken"] = dtdlt.Rows[0]["hosteltaken"].ToString();
                    string session_id = get_session_id(sessions);
                    if (type == "Monthly")
                    {
                        #region DeleteMonthlYFeeE
                        SqlCommand cmd;
                        if (transection_in == "App")
                        {
                            Alertme("You Can't Delete Payment Mode(App) transaction.", "warning");
                        }
                        else
                        {
                            string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id,Reason) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id,@Reason)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Current_admission_no", adm_No);
                            cmd.Parameters.AddWithValue("@Session_id", session_id);
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                            cmd.Parameters.AddWithValue("@Change_type", type + " Fees Delete");
                            cmd.Parameters.AddWithValue("@Class_Id_New", class_id);
                            cmd.Parameters.AddWithValue("@Roll_no_New", "");
                            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
                            cmd.Parameters.AddWithValue("@New_Section", "");
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Reason", txt_reason_for_delete.Text);
                            if (My.InsertUpdateData(cmd))
                            {
                                string qery = @"INSERT INTO Student_Payment_History_Save_bakup (Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,insert_time_date,Insert_time_user_id,New_Slip_no)
                    SELECT Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,'" + My.getdate1() + "','" + ViewState["Userid"].ToString() + "','" + slip_no + "' FROM Student_Payment_History where Addmission_no='" + adm_No + "' and Session='" + sessions + "' and Class_id='" + class_id + "' and Branch='" + ViewState["branchid"].ToString() + "' and Slip_no='" + slip_no + "'";

                                mycode.executequery(qery);
                                mycode.executequery("delete from Monthly_Fee_Collection_Slip where adno='" + adm_No + "' and class='" + class_id + "' and session='" + sessions + "' and branchid='" + ViewState["branchid"].ToString() + "' and slipno='" + slip_no + "'");
                                mycode.executequery("delete from Student_Payment_History where  Addmission_no='" + adm_No + "' and Session='" + sessions + "' and Class_id='" + class_id + "' and Branch='" + ViewState["branchid"].ToString() + "' and Slip_no='" + slip_no + "'");
                                mycode.executequery("delete from SchoolLedger where Addmission_no='" + adm_No + "' and branchid='" + ViewState["branchid"].ToString() + "' and TransactionId='" + slip_no + "'");
                                double total_amount = My.toDouble(amount);
                                string parameter = "";
                                parameter = ViewState["hosteltaken"].ToString().ToUpper() == "NO" ? "MonthlyFee" : "HostelMonthlyFee";

                                #region update dues amount
                                string app_payment_type = My.session("App_fee_collection_type");
                                string qry = "";
                                qry = "  select *  from Typewise_fee_collection  where admission_no='" + adm_No + "' and session='" + sessions + "'  and (status='Paid' or cast(paid as float)>0) and parameter like '%" + parameter + "%' and   branchid='" + ViewState["branchid"].ToString() + "' order by cast(Position as float) desc,parameter desc,id desc";
                                SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
                                DataSet ds = new DataSet();
                                ad.Fill(ds, "Typewise_fee_collection");
                                DataTable tdt = ds.Tables[0];
                                if (tdt.Rows.Count == 0)
                                {

                                }
                                else
                                {
                                    string prev_month = "", month = "";
                                    foreach (DataRow dr in tdt.Rows)
                                    {
                                        month = dr["month"].ToString();
                                        if (total_amount > 0)
                                        {
                                            double dues_paid = My.toDouble(dr["paid"]);
                                            if (total_amount >= dues_paid)
                                            {
                                                total_amount = total_amount - dues_paid;
                                                dr["paid"] = "0";
                                                dr["dues"] = dr["Payable_after_disc"];
                                                dr["status"] = "Dues";
                                                dr.Delete();
                                            }
                                            else
                                            {
                                                dr["paid"] = My.toDouble(dr["paid"]) - total_amount;
                                                dr["dues"] = My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"]);
                                                dr["status"] = "Dues";
                                                total_amount = 0;
                                                break;
                                            }
                                            prev_month = month;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                    ad.Update(tdt);
                                }

                                #endregion

                                string remarks = type + " Fees Delete";
                                My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Bill No :- " + slip_no + "," + remarks + " has been deleted successfully.");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region DeleteAdmissionAnnuAL
                        if (transection_in == "App")
                        {
                            Alertme("You Can't Delete Payment Mode(App) transaction ", "warning");
                        }
                        else
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id,Reason) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id,@Reason)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Current_admission_no", adm_No);
                            cmd.Parameters.AddWithValue("@Session_id", session_id);
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                            cmd.Parameters.AddWithValue("@Change_type", type + " Fees Delete");
                            cmd.Parameters.AddWithValue("@Class_Id_New", class_id);
                            cmd.Parameters.AddWithValue("@Roll_no_New", "");
                            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
                            cmd.Parameters.AddWithValue("@New_Section", "");
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Reason", txt_reason_for_delete.Text);
                            if (My.InsertUpdateData(cmd))
                            {
                                string qery = @"INSERT INTO Student_Payment_History_Save_bakup (Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,insert_time_date,Insert_time_user_id,New_Slip_no)
                    SELECT Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,'" + My.getdate1() + "','" + ViewState["Userid"].ToString() + "','" + slip_no + "' FROM Student_Payment_History where Addmission_no='" + adm_No + "' and Session='" + sessions + "' and Class_id='" + class_id + "' and Branch='" + ViewState["branchid"].ToString() + "' and Slip_no='" + slip_no + "'";
                                mycode.executequery(qery);
                                mycode.executequery("delete from Monthly_Fee_Collection_Slip where adno='" + adm_No + "' and class='" + class_id + "' and session='" + sessions + "' and branchid='" + ViewState["branchid"].ToString() + "' and slipno='" + slip_no + "'");
                                mycode.executequery("delete from Student_Payment_History where  Addmission_no='" + adm_No + "' and Session='" + sessions + "' and Class_id='" + class_id + "' and Branch='" + ViewState["branchid"].ToString() + "' and Slip_no='" + slip_no + "'");
                                mycode.executequery("delete from SchoolLedger where Addmission_no='" + adm_No + "' and branchid='" + ViewState["branchid"].ToString() + "' and TransactionId='" + slip_no + "'");
                                double total_amount = My.toDouble(amount);
                                string parameter = "";
                                if (type == "Admission")
                                {
                                    parameter = ViewState["hosteltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                                }
                                else
                                {
                                    parameter = ViewState["hosteltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                                }

                                #region update dues amount
                                string app_payment_type = My.session("App_fee_collection_type");
                                string qry = "";
                                qry = "  select *  from Typewise_fee_collection  where admission_no='" + adm_No + "' and session='" + sessions + "'  and (status='Paid' or cast(paid as float)>0) and parameter like '%" + parameter + "%' and   branchid='" + ViewState["branchid"].ToString() + "' order by cast(Position as float) desc,parameter desc,id desc";
                                SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
                                DataSet ds = new DataSet();
                                ad.Fill(ds, "Typewise_fee_collection");
                                DataTable tdt = ds.Tables[0];
                                if (tdt.Rows.Count == 0)
                                {
                                }
                                else
                                {
                                    string prev_month = "", month = "";
                                    foreach (DataRow dr in tdt.Rows)
                                    {
                                        month = dr["month"].ToString();
                                        if (total_amount > 0)
                                        {
                                            double dues_paid = My.toDouble(dr["paid"]);
                                            if (total_amount >= dues_paid)
                                            {
                                                total_amount = total_amount - dues_paid;
                                                dr["paid"] = "0";
                                                dr["dues"] = dr["Payable_after_disc"];
                                                dr["status"] = "Dues";
                                            }
                                            else
                                            {
                                                dr["paid"] = My.toDouble(dr["paid"]) - total_amount;
                                                dr["dues"] = My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"]);
                                                dr["status"] = "Dues";
                                                total_amount = 0;
                                                break;
                                            }
                                            prev_month = month;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                    ad.Update(tdt);
                                }

                                #endregion

                                if (type == "Annual")
                                {
                                    update_annual_fee(adm_No, sessions, class_id, parameter);
                                    My.no_any_paymnet_then_delete_type_wise_admission_annul_row(adm_No, sessions, class_id, parameter);

                                    My.exeSql("update admission_registor set payment_status='Unpaid' where admissionserialnumber='" + adm_No + "' and Class_id=" + class_id + " and session='" + sessions + "' ");
                                }
                                if (type == "Admission")
                                {
                                    update_admission_fee(adm_No, sessions, class_id, parameter);
                                    My.no_any_paymnet_then_delete_type_wise_admission_annul_row(adm_No, sessions, class_id, parameter);

                                    My.exeSql("update admission_registor set payment_status='Unpaid' where admissionserialnumber='" + adm_No + "' and Class_id=" + class_id + " and session='" + sessions + "' ");

                                    My.exeSql("update admission_registor set payment_status='Unpaid' where admissionserialnumber='" + adm_No + "' and Class_id=" + class_id + " and session='" + sessions + "' ");
                                }
                                string remarks = type + " Fees Delete";
                                My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Bill No :- " + slip_no + "," + remarks + " has been deleted successfully.");
                            }
                        }
                        #endregion
                    }
                }
                #endregion
            }
        }

        private void save_deleted_history()
        {
            SqlCommand cmd;
            string query = "INSERT INTO Deleted_bill_history (Session,Date,Name,Amount,Admission_no,Slip_no,Reason,Payment_mode,Class,Created_date,Created_idate,Payment_type) values (@Session,@Date,@Name,@Amount,@Admission_no,@Slip_no,@Reason,@Payment_mode,@Class,@Created_date,@Created_idate,@Payment_type)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session", ViewState["dlt_session"].ToString());
            cmd.Parameters.AddWithValue("@Date", ViewState["dlt_date"].ToString());
            cmd.Parameters.AddWithValue("@Name", ViewState["dlt_name"].ToString());
            cmd.Parameters.AddWithValue("@Amount", ViewState["dlt_amount"].ToString());
            cmd.Parameters.AddWithValue("@Admission_no", ViewState["dlt_adm_no"].ToString());
            cmd.Parameters.AddWithValue("@Slip_no", ViewState["dltebillNo"].ToString());
            cmd.Parameters.AddWithValue("@Reason", txt_reason_for_delete.Text);
            cmd.Parameters.AddWithValue("@Payment_mode", ViewState["dlt_payment_mode"].ToString());
            cmd.Parameters.AddWithValue("@Class", ViewState["dlt_class"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Payment_type", ViewState["UpdateType"].ToString());
            if (My.InsertUpdateData(cmd))
            {
            }
        }

        private void update_admission_fee(string Addmission_no, string Session, string Class_id, string parameter)
        {

            DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + Addmission_no + "' and session='" + Session + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues' and class_id='" + Class_id + "' and branchid='" + ViewState["branchid"].ToString() + "' ");


            SqlConnection conn = new SqlConnection(My.con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_fee_collection where Admission_no='" + Addmission_no + "' and session='" + Session + "' and branchid='" + ViewState["branchid"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (My.toDouble(pdt.Rows[0]["paid"]) <= 0)
                    {
                        dr.Delete();
                        My.exeSql("update dbo.[admission_registor] set payment_status='Unpaid' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                    }
                    else
                    {
                        dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString());
                        dr[6] = My.toDouble(dr[4]) - My.toDouble(dr[5]);
                        My.exeSql("update dbo.[admission_registor] set payment_status='Dues' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                    }
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }
        private void update_annual_fee(string Addmission_no, string Session, string Class_id, string parameter)
        {

            DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + Addmission_no + "' and session='" + Session + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues' and class_id='" + Class_id + "' and branchid='" + ViewState["branchid"].ToString() + "' ");


            SqlConnection conn = new SqlConnection(My.con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + Addmission_no + "' and session='" + Session + "' and branchid='" + ViewState["branchid"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (My.toDouble(pdt.Rows[0]["paid"]) <= 0)
                    {
                        dr.Delete();
                        My.exeSql("update dbo.[admission_registor] set payment_status='Unpaid' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                    }
                    else
                    {
                        dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString());
                        dr[6] = My.toDouble(dr[4]) - My.toDouble(dr[5]);
                        My.exeSql("update dbo.[admission_registor] set payment_status='Dues' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                    }
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private string get_session_id(string sessions)
        {
            string session_id = "0";
            DataTable dt = My.dataTable("select session_id from session_details where Session='" + sessions + "'");
            if (dt.Rows.Count > 0)
            {
                session_id = dt.Rows[0]["session_id"].ToString();
            }
            return session_id;
        }
        #endregion


        protected void lnkEditOnline_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDelOnline_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEditCheque_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDelCheque_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit001_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AnnualnFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel001_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AnnualnFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit002_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AnnualnFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel002_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AnnualnFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit003_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AnnualnFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel003_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AnnualnFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit004_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel004_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit005_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel005_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit006_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel006_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit007_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel007_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }

        }

        protected void lnkEdit008_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel008_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit009_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel009_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AdmissionFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit010_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AnnualFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }


        protected void lnkDel010_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AnnualFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit011_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AnnualFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel011_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AnnualFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit012_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "AnnualFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel012_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "AnnualFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit013_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel013_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit014_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel014_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit015_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel015_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["UpdateType"] = "MonthlyFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        /// =========================================== FORMSALE
        /// =========================================== FORMSALE
        /// =========================================== FORMSALE
        /// =========================================== FORMSALE
        protected void lnkEdit016_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "FormSale";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel016_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                Label lbl_class = (Label)row.FindControl("lbl_class");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");

                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = lbl_studentname.Text;
                ViewState["dlt_class"] = lbl_class.Text;
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = "0";
                ViewState["UpdateType"] = "FormSale";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit017_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "FormSale";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel017_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                Label lbl_class = (Label)row.FindControl("lbl_class");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");


                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = lbl_studentname.Text;
                ViewState["dlt_class"] = lbl_class.Text;
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = "0";
                ViewState["UpdateType"] = "FormSale";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit018_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "FormSale";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel018_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                Label lbl_class = (Label)row.FindControl("lbl_class");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");


                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = lbl_studentname.Text;
                ViewState["dlt_class"] = lbl_class.Text;
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = "0";
                ViewState["UpdateType"] = "FormSale";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit019_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "OtherFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel019_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                Label lbl_class = (Label)row.FindControl("lbl_class");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");

                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = lbl_studentname.Text;
                ViewState["dlt_class"] = lbl_class.Text;
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = lbl_admission_no.Text;
                ViewState["UpdateType"] = "OtherFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit020_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "OtherFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel020_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                Label lbl_class = (Label)row.FindControl("lbl_class");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");

                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = lbl_studentname.Text;
                ViewState["dlt_class"] = lbl_class.Text;
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = lbl_admission_no.Text;
                ViewState["UpdateType"] = "OtherFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit021_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "OtherFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel021_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                Label lbl_class = (Label)row.FindControl("lbl_class");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");

                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = lbl_studentname.Text;
                ViewState["dlt_class"] = lbl_class.Text;
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = lbl_admission_no.Text;
                ViewState["UpdateType"] = "OtherFee";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit022_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "GeneralExpenseVendor";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel022_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_namep = (Label)row.FindControl("lbl_name");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");

                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = lbl_namep.Text;
                ViewState["dlt_class"] = "0";
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = "0";
                ViewState["UpdateType"] = "GeneralExpenseVendor";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit023_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "GeneralExpenseVendor";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel023_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_namep = (Label)row.FindControl("lbl_name");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");


                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = lbl_namep.Text;
                ViewState["dlt_class"] = "0";
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = "0";
                ViewState["UpdateType"] = "GeneralExpenseVendor";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit024_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "GeneralExpenseVendor";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel024_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_namep = (Label)row.FindControl("lbl_name");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");


                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = lbl_namep.Text;
                ViewState["dlt_class"] = "0";
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = "0";
                ViewState["UpdateType"] = "GeneralExpenseVendor";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }
        protected void lnkEdit025_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "GeneralExpense";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel025_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");


                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = "0";
                ViewState["dlt_class"] = "0";
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = "0";
                ViewState["UpdateType"] = "GeneralExpense";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit026_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                tx_payment_date.Text = lbl_Date.Text;
                ddl_pay_mode.Text = lbl_payment_mode.Text;
                txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                txt_remarks.Text = lbl_remarks.Text;
                ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                ViewState["updtpayDate"] = lbl_Date.Text;
                ViewState["updtpayMode"] = lbl_payment_mode.Text;
                ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                ViewState["updtpayRemark"] = lbl_remarks.Text;
                ViewState["EdtIdS"] = lbl_id.Text;
                ViewState["UpdateType"] = "GeneralExpense";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel026_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() != "1")
                {
                    Alertme("Sorry! You are not authorised user.", "warning");
                    return;
                }
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_date = (Label)row.FindControl("lbl_Date");
                Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_id = (Label)row.FindControl("lbl_id");


                lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                ViewState["dltebillNo"] = lbl_recipt_no.Text;
                ViewState["dlteIdNo"] = lbl_id.Text;
                ViewState["dlt_session"] = lbl_session.Text;
                ViewState["dlt_date"] = lbl_date.Text;
                ViewState["dlt_name"] = "0";
                ViewState["dlt_class"] = "0";
                ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                ViewState["dlt_amount"] = lbl_amount.Text;
                ViewState["dlt_adm_no"] = "0";
                ViewState["UpdateType"] = "GeneralExpense";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
            }
            catch (Exception ex)
            {
            }
        }



        protected void lnkEdit027_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {

                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                    Label lbl_Date = (Label)row.FindControl("lbl_Date");
                    Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                    Label lbl_pay_mode_transaction_no = (Label)row.FindControl("lbl_pay_mode_transaction_no");
                    Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                    Label lbl_id = (Label)row.FindControl("lbl_id");
                    lbl_receipt_no_pop.Text = lbl_recipt_no.Text;
                    tx_payment_date.Text = lbl_Date.Text;
                    ddl_pay_mode.Text = lbl_payment_mode.Text;
                    txt_transaction_no.Text = lbl_pay_mode_transaction_no.Text;
                    txt_remarks.Text = lbl_remarks.Text;
                    ViewState["updtSlipNo"] = lbl_recipt_no.Text;
                    ViewState["updtpayDate"] = lbl_Date.Text;
                    ViewState["updtpayMode"] = lbl_payment_mode.Text;
                    ViewState["updtpayModeTransactionNo"] = lbl_pay_mode_transaction_no.Text;
                    ViewState["updtpayRemark"] = lbl_remarks.Text;
                    ViewState["EdtIdS"] = lbl_id.Text;
                    ViewState["UpdateType"] = "GeneralExpense";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkDel027_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {

                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_session = (Label)row.FindControl("lbl_session");
                    Label lbl_date = (Label)row.FindControl("lbl_Date");
                    Label lbl_recipt_no = (Label)row.FindControl("lbl_recipt_no");
                    Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                    Label lbl_amount = (Label)row.FindControl("lbl_amount");
                    Label lbl_id = (Label)row.FindControl("lbl_id");


                    lbl_reason_bill_no.Text = lbl_recipt_no.Text;
                    ViewState["dltebillNo"] = lbl_recipt_no.Text;
                    ViewState["dlteIdNo"] = lbl_id.Text;
                    ViewState["dlt_session"] = lbl_session.Text;
                    ViewState["dlt_date"] = lbl_date.Text;
                    ViewState["dlt_name"] = "0";
                    ViewState["dlt_class"] = "0";
                    ViewState["dlt_payment_mode"] = lbl_payment_mode.Text;
                    ViewState["dlt_amount"] = lbl_amount.Text;
                    ViewState["dlt_adm_no"] = "0";
                    ViewState["UpdateType"] = "GeneralExpense";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalReason();", true);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}