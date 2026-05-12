using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class userwise_payment_collection_report : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        mycode.bind_all_ddl_with_id_All(ddl_user, "select DISTINCT t2.name,t1.user_id from Student_Payment_History t1 join user_details t2 on t1.user_id=t2.user_id order by t2.name asc");
                        ddl_user.SelectedValue = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        txt_s_date.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date();
                        hd_session.Value = My.get_session_id();

                        ViewState["flag"] = "0";
                        find_firm_details();

                        string sdate = txt_s_date.Text;
                        string sday = sdate.Substring(0, 2);
                        string smonth = sdate.Substring(3, 2);
                        string syear = sdate.Substring(6, 4);

                        string edate = txt_e_date.Text;
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
                            final_find_report_by_date(idate, idate2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        private void find_by_date()
        {
            string sdate = txt_s_date.Text;
            string sday = sdate.Substring(0, 2);
            string smonth = sdate.Substring(3, 2);
            string syear = sdate.Substring(6, 4);

            string edate = txt_e_date.Text;
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
                final_find_report_by_date(idate, idate2);
            }
        }

        private void final_find_report_by_date(int idate, int idate2)
        {
            if (ddl_user.SelectedItem.Text == "All")
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Session ", My.get_session());
                cmd.Parameters.AddWithValue("@Session_id ", My.get_session_id());
                cmd.Parameters.AddWithValue("@Class_id ", 0);
                cmd.Parameters.AddWithValue("@sp_status ", "6");// day school
                cmd.Parameters.AddWithValue("@userid ", "All");// day school
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    pnl_contnt_dv.Visible = false;
                    lbl_paid_amount.Text = "0.00";
                    lbl_ttl_admission_fee.Text = "0.00";
                    lbl_ttl_annual_fee.Text = "0.00";
                    lbl_ttl_mnthly_fee.Text = "0.00";
                    lbl_form_seal.Text = "0.00";
                    lbl_total_otherfee.Text = "0.00";
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();

                }
                else
                {
                    pnl_contnt_dv.Visible = true;
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    if (ViewState["Is_Print"].ToString() == "1")
                    {
                        print1.Visible = true;
                    }
                    else
                    {
                        print1.Visible = false;
                    } 
                    lbl_p_date.Text = mycode.date() + "-" + mycode.time();
                    if (ddl_user.SelectedItem.Text.ToUpper() == "ALL")
                    { lbl_sig_name.Text = ""; }
                    else { lbl_sig_name.Text = ddl_user.SelectedItem.Text + "-(" + ddl_user.SelectedValue + ")"; }
                    lbl_report_of.Text = txt_s_date.Text + " To " + txt_e_date.Text;
                    lbl_paid_amount.Text = "0.00";
                    lbl_ttl_admission_fee.Text = "0.00";
                    lbl_ttl_annual_fee.Text = "0.00";
                    lbl_ttl_mnthly_fee.Text = "0.00";
                    lbl_form_seal.Text = "0.00";
                    lbl_total_otherfee.Text = "0.00";
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["parameter_New"].ToString() == "AnnualFee" || dt.Rows[j]["parameter_New"].ToString() == "HostelAnnualFee")
                        {
                            double Total1 = My.toDouble(lbl_ttl_annual_fee.Text) + My.toDouble(dt.Rows[j]["Amount"].ToString());
                            lbl_ttl_annual_fee.Text = Total1.ToString("0.00");
                        }
                        if (dt.Rows[j]["parameter_New"].ToString() == "AdmissionFee" || dt.Rows[j]["parameter_New"].ToString() == "HostelAdmissionFee")
                        {
                            double Total1 = My.toDouble(lbl_ttl_admission_fee.Text) + My.toDouble(dt.Rows[j]["Amount"].ToString());
                            lbl_ttl_admission_fee.Text = Total1.ToString("0.00");
                        }
                        if (dt.Rows[j]["parameter_New"].ToString() == "MonthlyFee" || dt.Rows[j]["parameter_New"].ToString() == "HostelMonthlyFee")
                        {
                            double Total1 = My.toDouble(lbl_ttl_mnthly_fee.Text) + My.toDouble(dt.Rows[j]["Amount"].ToString());
                            lbl_ttl_mnthly_fee.Text = Total1.ToString("0.00");
                        }
                        if (dt.Rows[j]["parameter_New"].ToString() == "Form Sale")
                        {
                            double Total1 = My.toDouble(lbl_form_seal.Text) + My.toDouble(dt.Rows[j]["Amount"].ToString());
                            lbl_form_seal.Text = Total1.ToString("0.00");
                        }
                        if (dt.Rows[j]["parameter_New"].ToString() == "Other Fee")
                        {
                            double Total1 = My.toDouble(lbl_total_otherfee.Text) + My.toDouble(dt.Rows[j]["Amount"].ToString());
                            lbl_total_otherfee.Text = Total1.ToString("0.00"); 
                        } 
                    } 
                    double total = My.toDouble(lbl_ttl_annual_fee.Text) + My.toDouble(lbl_ttl_admission_fee.Text) + My.toDouble(lbl_ttl_mnthly_fee.Text) + My.toDouble(lbl_form_seal.Text) + My.toDouble(lbl_total_otherfee.Text);
                    lbl_paid_amount.Text = total.ToString("0.00");
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_Fee_collection_day_end_report");
                cmd.Parameters.AddWithValue("@fromdate ", idate);
                cmd.Parameters.AddWithValue("@todate ", idate2);
                cmd.Parameters.AddWithValue("@Session ", My.get_session());
                cmd.Parameters.AddWithValue("@Session_id ", My.get_session_id());
                cmd.Parameters.AddWithValue("@sp_status ", "17");// day school
                cmd.Parameters.AddWithValue("@userid ", ddl_user.SelectedValue);// day school
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    pnl_contnt_dv.Visible = false;
                    lbl_paid_amount.Text = "0.00";
                    lbl_ttl_admission_fee.Text = "0.00";
                    lbl_ttl_annual_fee.Text = "0.00";
                    lbl_ttl_mnthly_fee.Text = "0.00";
                    lbl_form_seal.Text = "0.00";
                    lbl_total_otherfee.Text = "0.00";
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();

                }
                else
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();


                    pnl_contnt_dv.Visible = true;
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    lbl_p_date.Text = mycode.date() + "-" + mycode.time();
                    if (ddl_user.SelectedItem.Text.ToUpper() == "ALL")
                    { lbl_sig_name.Text = ""; }
                    else { lbl_sig_name.Text = ddl_user.SelectedItem.Text + "-(" + ddl_user.SelectedValue + ")"; }
                   
                    lbl_report_of.Text = txt_s_date.Text + " To " + txt_e_date.Text;
                    lbl_paid_amount.Text = "0.00";
                    lbl_ttl_admission_fee.Text = "0.00";
                    lbl_ttl_annual_fee.Text = "0.00";
                    lbl_ttl_mnthly_fee.Text = "0.00";
                    lbl_form_seal.Text = "0.00";
                    lbl_total_otherfee.Text = "0.00";

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["parameter_New"].ToString() == "AnnualFee" || dt.Rows[j]["parameter_New"].ToString() == "HostelAnnualFee")
                        {
                            double Total1 = My.toDouble(lbl_ttl_annual_fee.Text) + My.toDouble(dt.Rows[j]["Amount"].ToString());
                            lbl_ttl_annual_fee.Text = Total1.ToString("0.00");
                        }
                        if (dt.Rows[j]["parameter_New"].ToString() == "AdmissionFee" || dt.Rows[j]["parameter_New"].ToString() == "HostelAdmissionFee")
                        {
                            double Total1 = My.toDouble(lbl_ttl_admission_fee.Text) + My.toDouble(dt.Rows[j]["Amount"].ToString());
                            lbl_ttl_admission_fee.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["parameter_New"].ToString() == "MonthlyFee" || dt.Rows[j]["parameter_New"].ToString() == "HostelMonthlyFee")
                        {
                            double Total1 = My.toDouble(lbl_ttl_mnthly_fee.Text) + My.toDouble(dt.Rows[j]["Amount"].ToString());
                            lbl_ttl_mnthly_fee.Text = Total1.ToString("0.00");


                        }

                        if (dt.Rows[j]["parameter_New"].ToString() == "Form Sale")
                        {
                            double Total1 = My.toDouble(lbl_form_seal.Text) + My.toDouble(dt.Rows[j]["Amount"].ToString());
                            lbl_form_seal.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["parameter_New"].ToString() == "Other Fee")
                        {
                            double Total1 = My.toDouble(lbl_total_otherfee.Text) + My.toDouble(dt.Rows[j]["Amount"].ToString());
                            lbl_total_otherfee.Text = Total1.ToString("0.00");


                        }



                    }

                    double total = My.toDouble(lbl_ttl_annual_fee.Text) + My.toDouble(lbl_ttl_admission_fee.Text) + My.toDouble(lbl_ttl_mnthly_fee.Text) + My.toDouble(lbl_form_seal.Text) + My.toDouble(lbl_total_otherfee.Text);


                    lbl_paid_amount.Text = total.ToString("0.00");




                }



                // bind_grd_view("select t1.*,t2.class,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,(select top 1 name from user_details where user_id=t1.user_id) as User_name from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t2.Session_id='" + hd_session.Value + "'  and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.user_id='" + ddl_user.SelectedValue + "'", "select isnull(sum(convert(float, Amount)),0) as Total_Amount from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t2.Session_id='" + hd_session.Value + "'  and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.user_id='" + ddl_user.SelectedValue + "' UNION all select isnull(sum(convert(float, Amount)),0) as Total_Amount from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t2.Session_id='" + hd_session.Value + "'  and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.user_id='" + ddl_user.SelectedValue + "'  UNION all select isnull(sum(convert(float, Amount)),0) as Total_Amount from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Annual' and t2.Session_id='" + hd_session.Value + "'  and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.user_id='" + ddl_user.SelectedValue + "' UNION all select isnull(sum(convert(float, Amount)),0) as Total_Amount from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Monthly' and t2.Session_id='" + hd_session.Value + "'  and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.user_id='" + ddl_user.SelectedValue + "'");
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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_user.SelectedItem.Text == "Select")
            {
                Alertme("please select user.", "warning");
                ddl_user.Focus();
            }
            else if (txt_s_date.Text == "")
            {
                Alertme("please choose from date.", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                find_by_date();
            }
        }
    }
}