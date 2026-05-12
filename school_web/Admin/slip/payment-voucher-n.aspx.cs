using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class payment_voucher_n : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('../../Default.aspx','_parent',replace=true);</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["admissionno"] != null)
                    {
                        ViewState["admissionno"] = Request.QueryString["admissionno"];
                        ViewState["sessionid"] = Request.QueryString["sessionid"];
                        ViewState["classid"] = Request.QueryString["classid"];
                        ViewState["Slip_no"] = Request.QueryString["Slip_no"];
                        ViewState["session"] = mycode.get_session(ViewState["sessionid"].ToString());
                        Bind_schoolinfo();
                        student_details();
                        fetch_fee_details();
                        get_note();
                        lbl_paymentdate.Text = lbl_paymentdate1.Text = mycode.date();
                    }
                }
            }
        }

        private void get_note()
        {
            DataTable dt = mycode.FillData("select * from Slip_note where Type_id='1'");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();

                rd_notes.DataSource = null;
                rd_notes.DataBind();
                notesDt1.Visible = false;
                notesDt.Visible = false;

            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();

                rd_notes.DataSource = dt;
                rd_notes.DataBind();
                notesDt.Visible = true;
                notesDt1.Visible = true;
            }
        }

        private void fetch_fee_details()
        {
            DataTable dt = mycode.FillData("select * from Payment_voucher_slip where Slip_id='" + ViewState["Slip_no"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_slipno.Text = lbl_slipno1.Text = dt.Rows[0]["Slip_id"].ToString();
                lbl_fee_months.Text = lbl_fee_months1.Text = " (" + dt.Rows[0]["Months"].ToString() + ")";
                lbl_fee_rupee.Text = lbl_fee_rupee1.Text = dt.Rows[0]["Amount"].ToString();
                lbl_pay_date.Text = lbl_pay_date1.Text = dt.Rows[0]["Last_date_of_payment"].ToString();
                lbl_prev_dues.Text = lbl_prev_dues1.Text = dt.Rows[0]["Previous_dues"].ToString();
                lbl_ttl_amts.Text = lbl_ttl_amts1.Text = dt.Rows[0]["Total_amount"].ToString();

                try
                {
                    Bind_data_heading(dt.Rows[0]["Months"].ToString());
                }
                catch
                { 
                } 
            }
        }



        private void student_details()
        {
            ViewState["fineAmt"] = "0";
            DataTable dt = mycode.FillData(" select  * from admission_registor  where admissionserialnumber='" + ViewState["admissionno"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and Class_id=" + ViewState["classid"].ToString() + " ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_studentname.Text = lbl_studentname1.Text = dt.Rows[0]["studentname"].ToString();
                lbl_aadmissionno.Text = lbl_aadmissionno1.Text = dt.Rows[0]["admissionserialnumber"].ToString();

                lbl_class.Text = lbl_class1.Text = dt.Rows[0]["class"].ToString();
                lbl_fathername.Text = lbl_fathername1.Text = dt.Rows[0]["fathername"].ToString();
                lbl_session.Text = lbl_session1.Text = dt.Rows[0]["session"].ToString();
                lbl_rollno.Text = lbl_rollno1.Text = dt.Rows[0]["rollnumber"].ToString();
                lbl_phone_no.Text = lbl_phone_no1.Text = dt.Rows[0]["father_mob"].ToString();
                lbl_section.Text = lbl_section1.Text = dt.Rows[0]["Section"].ToString();
                ViewState["transportID"] = dt.Rows[0]["Transportation_Id"].ToString();
                if (dt.Rows[0]["hosteltaken"].ToString() == "")
                {
                    ViewState["hostaltaken"] = "NO";

                }
                else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                {
                    ViewState["hostaltaken"] = "NO";

                }
                else
                {

                    ViewState["hostaltaken"] = "YES";
                }

                ViewState["parameter"] = ViewState["hostaltaken"].ToString() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                ViewState["IsBoarding"] = "0";
                ViewState["parameteridS"] = "4";


                ViewState["hostel_id"] = My.toint(dt.Rows[0]["Hostel_id"].ToString());


                ViewState["day_bording"] = My.toBool(dt.Rows[0]["is_applied_dayboarding"].ToString());
                ViewState["day_bording_with_lunch"] = My.toBool(dt.Rows[0]["day_boarding_with_lunch"].ToString());


                ViewState["group_id"] = "3";








                ViewState["class"] = dt.Rows[0]["class"].ToString();
                ViewState["category_id"] = dt.Rows[0]["category_id"].ToString();
                ViewState["sub_category_id"] = dt.Rows[0]["SubCategory_id"].ToString();
                ViewState["classid"] = dt.Rows[0]["Class_id"].ToString();
                ViewState["Section"] = dt.Rows[0]["Section"].ToString();
                ViewState["sessionIDs"] = dt.Rows[0]["Session_id"].ToString();
                ViewState["session"] = dt.Rows[0]["session"].ToString();
                ViewState["Transfer_Status"] = dt.Rows[0]["Transfer_Status"].ToString();
                ViewState["admission_no"] = dt.Rows[0]["admissionserialnumber"].ToString();



                ViewState["IsBoarding"] = "0";
                string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + dt.Rows[0]["Session_id"].ToString() + "' and Admission_no='" + dt.Rows[0]["admissionserialnumber"].ToString() + "' and Class_id='" + dt.Rows[0]["Class_id"].ToString() + "'";
                DataTable dts = My.dataTable(queryS);
                if (dts.Rows.Count != 0)
                {
                    ViewState["LunchMnthName"] = dts.Rows[0]["Month_name"].ToString();
                    ViewState["LunchMnthId"] = dts.Rows[0]["Month_id"].ToString();
                    ViewState["IsBoarding"] = "1";
                }


            }
        }

        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_affiliation_no.Text = lbl_affiliation_no1.Text = dt.Rows[0]["Affiliation"].ToString();
                lbl_schoolno.Text = lbl_schoolno1.Text = dt.Rows[0]["school_no"].ToString();
                imglogo.ImageUrl = Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = lbl_address1.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = lbl_emaiid1.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = lbl_website1.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = lbl_contact_details1.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = lbl_heading1.Text = dt.Rows[0]["firm_name"].ToString();
                if (dt.Rows[0]["Is_mobile_no_show_in_bill"].ToString() == "True")
                {
                    contact_no.Visible = true;
                    contact_no1.Visible = true;
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../reprint-payment-voucher.aspx", false);
        }


        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }

        #region bind
        private void Bind_data_heading(string Months)
        {
            DataTable dtf = mycode.FillData("select Month_selection,Is_fine_repeat from Firm_Details");
            if (My.toIntS(dtf.Rows[0]["Month_selection"].ToString()) > 1)
            {
                ViewState["more_months_check_status"] = "Yes";
                ViewState["no_of_months"] = My.toIntS(dtf.Rows[0]["Month_selection"].ToString());
            }
            if (dtf.Rows[0]["Is_fine_repeat"].ToString() == "True")
            {
                ViewState["RepeatFine"] = "Yes";
            }
            else
            {
                ViewState["RepeatFine"] = "No";
            }
            ViewState["check_one_more_months"] = "1";
            ViewState["Dues"] = "No";
            ViewState["DuesCalculate"] = "No";


            string get_month = mycode.get_spareat_month(Months);
            string query = " select   * from dbo.[Month_Index] where Month in (" + get_month + ")   order by Position ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                GridView2.DataSource = null;
                GridView2.DataBind(); 
            }
            else
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();

                int kn = 1;
                int growcount = GridView2.Rows.Count;
                for (int i = 0; i < growcount; i++)
                {
                    Label lbl_Month = (Label)GridView2.Rows[i].FindControl("lbl_Month");

                    bind_monthly_fee();
                    ViewState["MnthName"] = lbl_Month.Text;
                    // fine_calculation(lbl_Month.Text, "1"); 
                }
            } 
        }

        private void fine_calculation(string monthName, string from)
        {
            ViewState["checked_mnth"] = "0";
            int mnth_idss = My.tomonth_number(monthName);
            string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());
            if (from == "1")
            {
                int mnth_ids = My.tomonth_number(monthName);
                string month_id_in_two_dgt = My.getMonthS_twoDigit(mnth_ids.ToString());
                if (ViewState["checked_mnth"].ToString() == "0")
                {
                    ViewState["checked_frst_mnth"] = month_id_in_two_dgt;
                    ViewState["checked_after_frst_mnth"] = month_id_in_two_dgt;
                    ViewState["checked_mnth"] = "1";
                }
                else
                {
                    ViewState["checked_after_frst_mnth"] = month_id_in_two_dgt;
                }
            }

            DataTable dty = mycode.FillData("select Fine_mode from globle_data where Fine_mode='2'");
            if (dty.Rows.Count != 0)
            {
                #region DayRanGEWise
                string pay_date = lbl_pay_date.Text;
                int payidate = My.DateConvertToIdate(pay_date);


                //Advance Payment Check
                string crnt_year = mycode.year();
                string cunrt_session = ViewState["sessionIDs"].ToString();
                string session_frst_year = cunrt_session.Substring(0, 4);
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);


                int pay_month = My.toint(ViewState["checked_after_frst_mnth"].ToString());
                s_year = My.check_start_months(pay_month, s_year);

                int pay_month_with_year = My.toint(s_year + ViewState["checked_after_frst_mnth"].ToString());
                int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                //Advance Payment Check



                if (crnt_month_with_year >= pay_month_with_year)
                {
                    DataTable dtz = mycode.FillData("select top 1 * from Fine_master_day_range");
                    if (dtz.Rows.Count != 0)
                    {
                        ViewState["FineType"] = "DayWise";
                        //string last_day_of_payment = dtz.Rows[0]["No_of_day"].ToString() + "/" + applicable_month + "/" + session_s_year; 
                        string last_day_of_payments = "01" + "/" + month_id_in_two_dgts + "/" + s_year;


                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);

                        ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                        ViewState["late_fine_no_of_day_month"] = totaldays;
                        ViewState["fine_date_From"] = last_day_of_payments;
                        ViewState["fine_date_To"] = pay_date;
                        ViewState["fineAmt"] = "0.00";




                        DataTable dt_fine = mycode.FillData("select top 1 * from Fine_master_day_range where No_of_day>" + totaldays + " order by No_of_day asc");
                        if (dt_fine.Rows.Count != 0)
                        {

                            bind_ttl_fee();
                        }
                        else
                        {
                            DataTable dt_fines = mycode.FillData("select top 1 * from Fine_master_day_range order by No_of_day desc");
                            if (dt_fines.Rows.Count != 0)
                            {

                                bind_ttl_fee();
                            }
                            else
                            {
                                ViewState["fineAmt"] = "0";
                                bind_ttl_fee();
                            }
                        }
                    }
                }
                #endregion
            }
            else
            { 
                DataTable dt = mycode.FillData("select * from Fine_master where Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "'");
                if (dt.Rows.Count != 0)
                { 
                    string pay_date = lbl_pay_date.Text;
                    int payidate = My.DateConvertToIdate(pay_date); 
                    string fineType = dt.Rows[0]["Fine_type"].ToString();


                    //Advance Payment Check
                    string crnt_year = mycode.year();
                    string cunrt_session = My.get_session();
                    string session_frst_year = cunrt_session.Substring(0, 4);
                    int session_s_year = My.toint(session_frst_year);
                    int s_year = My.toint(session_frst_year);


                    int pay_month = My.toint(ViewState["checked_after_frst_mnth"].ToString());
                    if (pay_month == 1 || pay_month == 2 || pay_month == 3)
                    {
                        s_year = s_year + 1;
                    }


                    if (fineType == "DayWise")//===== Days
                    {
                        #region DayWise

                        int pay_month_with_year = My.toint(s_year + ViewState["checked_after_frst_mnth"].ToString());
                        int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                        //Advance Payment Check

                        if (crnt_month_with_year >= pay_month_with_year)
                        {

                            ViewState["FineType"] = "DayWise";
                            string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                            string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;



                            string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + month_id_in_two_dgts + "/" + s_year;

                            DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);




                            System.TimeSpan diff = enddate1.Subtract(startdate1);
                            int totaldays = Convert.ToInt32(diff.Days);

                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totaldays;
                            ViewState["fine_date_From"] = last_day_of_payments;
                            ViewState["fine_date_To"] = pay_date;
                            if (My.toDouble(totaldays) > 0)
                            {
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;

                                bind_ttl_fee();
                            }
                            else
                            {

                                bind_ttl_fee();
                            }

                        }
                        #endregion
                    }
                    else if (fineType == "MonthWise")//===== MonthWise
                    {
                        #region MonthWise
                        ViewState["FineType"] = "MonthWise";
                        string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                        string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");

                        string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + month_id_in_two_dgts + "/" + s_year;
                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);
                        int totalmonths = 0;


                        if (ViewState["RepeatFine"].ToString() == "Yes")
                        {
                            if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and month='" + monthName + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                            {
                            }
                            else
                            {
                                if (totaldays > 0)
                                {
                                    if (30 >= totaldays)
                                    {
                                        totalmonths = 1;
                                    }
                                    else
                                    {
                                        totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));

                                        double monthdays = 31;
                                        double reminder = My.toDouble(totaldays) % monthdays;
                                        if (Math.Round(reminder) > 0)
                                        {
                                            totalmonths++;
                                        }
                                    }
                                }
                            }
                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totalmonths;
                            ViewState["fine_date_From"] = last_day_of_payments;
                            ViewState["fine_date_To"] = pay_date;
                            if (My.toDouble(totalmonths) > 0)
                            {
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * totalmonths;

                                bind_ttl_fee();
                            }
                            else
                            {

                                bind_ttl_fee();
                            }
                        }
                        else
                        {
                            if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and month='" + monthName + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                            {
                            }
                            else
                            {
                                if (totaldays > 0)
                                {
                                    if (30 >= totaldays)
                                    {
                                        totalmonths = 1;
                                    }
                                    else
                                    {
                                        totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                                    }
                                }
                            }
                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totalmonths;
                            ViewState["fine_date_From"] = last_day_of_payment;
                            ViewState["fine_date_To"] = pay_date;
                            if (My.toDouble(totalmonths) > 0)
                            {
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * 1;

                                bind_ttl_fee();
                            }
                            else
                            {

                                bind_ttl_fee();
                            }
                        }
                        #endregion
                    }
                    else
                    {

                        #region QuarterWise
                        ViewState["FineType"] = "QuarterWise";
                        double fnl_fine_amt = 0;
                        SqlDataAdapter ad = new SqlDataAdapter("select * from Fine_master where Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "' and Q_start_month<='" + ViewState["checked_after_frst_mnth"].ToString() + "'  order by Q_start_month asc", My.conn);
                        DataSet ds = new DataSet();
                        ad.Fill(ds, "Fine_master");
                        DataTable dtm = ds.Tables[0];
                        int rowcount = ds.Tables[0].Rows.Count;
                        if (rowcount == 0)
                        {
                        }
                        else
                        {
                            foreach (DataRow dr in dtm.Rows)
                            {
                                string endmnth_in_two = My.getMonthS_twoDigit(dr["Q_end_month"].ToString());
                                string last_day_of_payment_q = dr["Last_day_to_deposit_fees"].ToString() + "/" + endmnth_in_two + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");




                                DateTime startdate1q = DateTime.ParseExact(last_day_of_payment_q, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                if (dr["Q_payment_mode"].ToString() == "Day")
                                {
                                    System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                    int totaldays = Convert.ToInt32(diff.Days);
                                    ViewState["late_fine_no_of_day_month"] = totaldays;

                                    if (ViewState["flags1"].ToString() == "0")
                                    {
                                        ViewState["fine_date_From"] = last_day_of_payment_q;
                                        ViewState["flags1"] = "1";
                                    }

                                    ViewState["fine_date_To"] = pay_date;

                                    if (My.toDouble(totaldays) > 0)
                                    {
                                        string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                        double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                        fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                    }
                                    else
                                    {
                                        fnl_fine_amt = fnl_fine_amt += 0;
                                    }
                                }
                                else
                                {
                                    System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                    int totaldays = Convert.ToInt32(diff.Days);
                                    ViewState["late_fine_no_of_day_month"] = dtm.Rows.Count.ToString();

                                    if (ViewState["flags1"].ToString() == "0")
                                    {
                                        ViewState["fine_date_From"] = last_day_of_payment_q;
                                        ViewState["flags1"] = "1";
                                    }

                                    ViewState["fine_date_To"] = pay_date;
                                    if (My.toDouble(totaldays) > 0)
                                    {
                                        string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                        //double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                        double ttl_fine_amt = My.toDouble(fine_amt);
                                        fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                    }
                                    else
                                    {
                                        fnl_fine_amt = fnl_fine_amt += 0;
                                    }
                                }
                            }

                            ViewState["fineAmt"] = fnl_fine_amt.ToString("0.00");
                            bind_ttl_fee();
                        }
                        #endregion
                    }
                }
            }
        }

        private void bind_monthly_fee()
        {
            double fee_amount = 0, disc_amount = 0, paid_previously = 0;
            bool success = false;
            DataTable fdt = new DataTable();
            int growcountS = GridView2.Rows.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                string cunrt_session = ViewState["session"].ToString();
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);

                Label lbl_Month = (Label)GridView2.Rows[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)GridView2.Rows[iS].FindControl("chk_month");

                fee_amount = 0; disc_amount = 0; paid_previously = 0;
                DataTable feedt = new DataTable();
                if (My.toBool(chk_month.Checked))
                {
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        string current_year_month = session_last_year + My.tomonth_numberstring(lbl_Month.Text);
                        string lunch_taken_year_month = "";
                        if (My.toint(ViewState["LunchMnthId"].ToString()) == 1 || My.toint(ViewState["LunchMnthId"].ToString()) == 2 || My.toint(ViewState["LunchMnthId"].ToString()) == 3)
                        {
                            lunch_taken_year_month = session_last_year + ViewState["LunchMnthId"].ToString();
                        }
                        else
                        {
                            lunch_taken_year_month = session_frst_year + ViewState["LunchMnthId"].ToString();
                        }

                        if (My.toint(lunch_taken_year_month) <= My.toint(current_year_month))
                        {
                            ViewState["parameteridS"] = "44";
                        }
                        else
                        {
                            ViewState["parameteridS"] = "4";
                        }
                    }
                    string type = "";
                    //dr["paid_status"] = "Created";
                    //dr["bac_colour"] = "Yellow";
                    if (My.dataTable("select * from dbo.[Typewise_fee_collection]   where admission_no='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                    {
                        feedt = My.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter, feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' and content_id!='6121' ");
                        type = "Calculated";
                    }
                    else
                    {
                        //dr["bac_colour"] = "White";
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = ViewState["admission_no"].ToString();
                        dc["session_id"] = ViewState["sessionIDs"].ToString();
                        dc["class"] = ViewState["class"].ToString();
                        dc["session"] = ViewState["session"].ToString();
                        dc["class_id"] = ViewState["classid"].ToString();
                        dc["hosteltaken"] = ViewState["hostaltaken"].ToString().ToLower();
                        dc["months"] = lbl_Month.Text;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = ViewState["hostaltaken"].ToString().ToLower() == "yes" ? My.toint(ViewState["hostel_id"].ToString()) : 0;
                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();
                        dc["category_id"] = ViewState["category_id"].ToString();
                        dc["sub_category_id"] = ViewState["sub_category_id"].ToString();
                        dc["transportportation_id"] = ViewState["transportID"].ToString();
                        dc["parameter_id"] = ViewState["parameteridS"].ToString(); 

                        //new08/08/2022 
                        string monthid = My.tomonth_numberstring(lbl_Month.Text); 
                        int pay_month = My.toint(monthid);
                        s_year = My.check_start_months(pay_month, s_year); 
                        dc["monthid"] = s_year + monthid; 
                        feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                        feedt.Columns.Add("previously_paid");
                        type = "NotCalculated";
                    }
                    feedt.Columns.Add("total_payable"); 

                    string month = "";
                    double total = 0, fee = 0, disc = 0, paid_prev = 0;
                    foreach (DataRow dr in feedt.Rows)
                    {
                        //if (type == "Calculated")
                        //{
                        //    My.exeSql("update dbo.[Typewise_fee_collection] set Disc='" + dr["disc_amount"].ToString() + "' where Id='" + dr["id"].ToString() + "'");
                        //}
                        month = dr["months"].ToString();
                        dr["total_payable"] = My.toDouble(dr["amount"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["previously_paid"]);
                        fee += My.toDouble(dr["amount"]);
                        disc += My.toDouble(dr["disc_amount"]);
                        paid_prev += My.toDouble(dr["previously_paid"]);

                        total += My.toDouble(dr["total_payable"]);
                    }

                    foreach (DataRow dr in feedt.Rows)
                    {
                        try
                        {
                            fdt.Rows.Add(dr.ItemArray);
                        }
                        catch
                        {
                            foreach (DataColumn dc in feedt.Columns)
                            {
                                fdt.Columns.Add(dc.ColumnName);
                            }
                            fdt.Rows.Add(dr.ItemArray);
                        }
                    }

                    //lbl_fee_month.Text = month;
                    //lbl_fee_amount.Text = fee.ToString();
                    //lbl_discount.Text = disc.ToString();
                    //lbl_paid_prev.Text = paid_prev.ToString();
                    //lbl_total.Text = total.ToString();
                }
                else
                {
                }
            }

            if (fdt.Rows.Count.ToString() != "0")
            {
                rp_fee_details_Office_copy.DataSource = fdt.DefaultView;
                rp_fee_details_Office_copy.DataBind();

                rp_fee_details_Office_copy_part2.DataSource = fdt.DefaultView;
                rp_fee_details_Office_copy_part2.DataBind();



                bind_ttl_fee();
                pnl_month_wise_fee_details.Visible = true;
                pnl_month_wise_fee_details_part2.Visible = true;
            }
            else
            {
                rp_fee_details_Office_copy.DataSource = null;
                rp_fee_details_Office_copy.DataBind();

                rp_fee_details_Office_copy_part2.DataSource = fdt.DefaultView;
                rp_fee_details_Office_copy_part2.DataBind();


                pnl_month_wise_fee_details.Visible = false;
                pnl_month_wise_fee_details_part2.Visible = false;


                lbl_fee_amount.Text = "0";
                lbl_discount.Text = "0";
                lbl_paid_prev.Text = "0";
                lbl_total.Text = "0";


                lbl_fee_amount1.Text = "0";
                lbl_discount1.Text = "0";
                lbl_paid_prev1.Text = "0";
                lbl_total1.Text = "0";
            }
        }



        private void bind_ttl_fee()
        {
            int i;
            double totalAmt = 0; double totaldisc = 0; double totalPrepAid = 0; double totalpblE = 0;
            int gridview_rowcount = rp_fee_details_Office_copy.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_amount = (Label)rp_fee_details_Office_copy.Items[i].FindControl("lbl_amount");
                Label lbl_disc_amt = (Label)rp_fee_details_Office_copy.Items[i].FindControl("lbl_disc_amt");
                Label lbl_pre_paid = (Label)rp_fee_details_Office_copy.Items[i].FindControl("lbl_pre_paid");
                Label lbl_tot_pble = (Label)rp_fee_details_Office_copy.Items[i].FindControl("lbl_tot_pble");
                if (lbl_amount.Text != "")
                {
                    totalAmt = totalAmt + Convert.ToDouble(lbl_amount.Text);
                }
                if (lbl_disc_amt.Text != "")
                {
                    totaldisc = totaldisc + Convert.ToDouble(lbl_disc_amt.Text);
                }
                if (lbl_pre_paid.Text != "")
                {
                    totalPrepAid = totalPrepAid + Convert.ToDouble(lbl_pre_paid.Text);
                }
                if (lbl_tot_pble.Text != "")
                {
                    totalpblE = totalpblE + Convert.ToDouble(lbl_tot_pble.Text);
                }
            }
            lbl_fee_amount.Text = totalAmt.ToString("0.00");
            lbl_discount.Text = totaldisc.ToString("0.00");
            lbl_paid_prev.Text = totalPrepAid.ToString("0.00");
            lbl_total.Text = totalpblE.ToString("0.00");

            lbl_fee_amount1.Text = totalAmt.ToString("0.00");
            lbl_discount1.Text = totaldisc.ToString("0.00");
            lbl_paid_prev1.Text = totalPrepAid.ToString("0.00");
            lbl_total1.Text = totalpblE.ToString("0.00");


            lbl_fee_rupee.Text = (totalpblE + My.toDouble(ViewState["fineAmt"].ToString()) + My.toDouble(0)).ToString("0.00");
            lbl_fee_rupee1.Text = (totalpblE + My.toDouble(ViewState["fineAmt"].ToString()) + My.toDouble(0)).ToString("0.00");
            // lbl_ttl_amts.Text = lbl_fee_rupee.Text;


            int number = (int)Convert.ToDouble(lbl_fee_rupee.Text);
            string inword_number = mycode.NumberToWords(number);
            string inword = inword_number + " Only.-";
            lbl_amt_in_word.Text = lbl_amt_in_word1.Text = inword;

        }
        protected void rp_fee_details_Office_copy_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        #endregion
    }
}