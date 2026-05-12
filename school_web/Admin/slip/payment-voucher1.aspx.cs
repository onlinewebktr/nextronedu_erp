using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class payment_voucher1 : System.Web.UI.Page
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

                lbl_pay_date.Text = lbl_pay_date1.Text = dt.Rows[0]["Last_date_of_payment"].ToString();



                lbl_prev_month_dues.Text = lbl_prev_month_dues1.Text = My.toDouble(dt.Rows[0]["Previous_dues"].ToString()).ToString("0.00") ;

                if (My.toDouble(lbl_prev_month_dues.Text) > 0)
                {
                    Previous_month_level1.Visible = true;
                    Previous_month_val1.Visible = true;

                    Previous_month_level.Visible = true;
                    Previous_month_val.Visible = true;
                }
                else
                {
                    Previous_month_level1.Visible = false;
                    Previous_month_val1.Visible = false;

                    Previous_month_level.Visible = false;
                    Previous_month_val.Visible = false;
                }

                // 
                // double monthfee = My.toDouble(dt.Rows[0]["Amount"].ToString());

                string prevdues = get_data_preview(dt.Rows[0]["Months"].ToString(), dt.Rows[0]["Admission_no"].ToString(), dt.Rows[0]["Session_id"].ToString());

                string[] stringSeparators = new string[] { "," };
                string[] arr = prevdues.Split(stringSeparators, StringSplitOptions.None);
                prevdues = arr[0];
                string type = arr[1];
                double prev_dues = My.toDouble(prevdues);
                lbl_prev_dues.Text = lbl_prev_dues1.Text = prev_dues.ToString("0.00");

                if (My.toDouble(lbl_prev_dues.Text) > 0)
                {
                    Previous_year_level.Visible = true;
                    Previous_year_level1.Visible = true;
                    Previous_year_val.Visible = true;
                    Previous_year_val1.Visible = true;
                }
                else
                {
                    Previous_year_level.Visible = false;
                    Previous_year_level1.Visible = false; 
                    Previous_year_val.Visible = false;
                    Previous_year_val1.Visible = false;
                }
                if (type == "oldyear")//Previous_Year_Dues
                {
                    lbl_fee_rupee.Text = dt.Rows[0]["Amount"].ToString();
                    double total = prev_dues + My.toDouble(dt.Rows[0]["Total_amount"].ToString()) + My.toDouble(lbl_prev_month_dues.Text);
                    lbl_ttl_amts.Text = lbl_ttl_amts1.Text = total.ToString("0.00");
                    lbl_fee_rupee.Text = lbl_fee_rupee1.Text = dt.Rows[0]["Amount"].ToString();
                }
                else
                { 
                    double Amount = My.toDouble(dt.Rows[0]["Amount"].ToString());
                    double monthfee = Amount - prev_dues;
                    lbl_fee_rupee.Text = lbl_fee_rupee1.Text = monthfee.ToString("0.00");
                    double total = prev_dues + monthfee + My.toDouble(lbl_prev_month_dues.Text);
                    lbl_ttl_amts.Text = lbl_ttl_amts1.Text = total.ToString("0.00");
                } 
            }
        }



        private void student_details()
        {
            DataTable dt = mycode.FillData("select studentname,admissionserialnumber,class,Section,fathername,session,rollnumber,Academic_Sem_or_Year,mobilenumber from admission_registor where admissionserialnumber='" + ViewState["admissionno"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and Class_id=" + ViewState["classid"].ToString() + " ");
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
                lbl_phone_no.Text = lbl_phone_no1.Text = dt.Rows[0]["mobilenumber"].ToString();
                lbl_section.Text = lbl_section1.Text = dt.Rows[0]["Section"].ToString();
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

        private string get_data_preview(string Months, string Admission_no, string Session_id)
        {
            string monthnew = Months.TrimEnd(',');
            DataTable dt = mycode.FillData("select Dues_Amount as amountdata from Previous_Year_Dues where Session_id='" + Session_id + "' and AdmissionNumber='" + Admission_no + "' and Status='Unpaid'");
            if (dt.Rows.Count == 0)
            {
                DataTable dt_m = mycode.FillData("select   * from dbo.[Monthly_Fee_Collection_Slip] where adno = '" + Admission_no + "' and Content = 'Previous Year Dues' and session = '" + lbl_session.Text + "'");
                if (dt_m.Rows.Count == 0)
                {
                    DataTable dt1 = mycode.FillData("select Amount as amountdata from Misc_Fee_Master_Studentwise where Session_id='" + Session_id + "' and Admission_No='" + Admission_no + "' and Old_year_Dues_Type='Yes' and Month like '%" + monthnew + "%' ");
                    if (dt1.Rows.Count == 0)
                    {
                        return "0.00" + ",oldyearmonth";
                    }
                    else
                    {
                        return dt1.Rows[0]["amountdata"].ToString() + ",oldyearmonth";
                    }
                }
                else
                { 
                    return "0.00" + ",oldyearmonth";
                }
            }
            else
            {
                DataTable dt_m = mycode.FillData("select   * from dbo.[Monthly_Fee_Collection_Slip] where adno = '" + Admission_no + "' and Content = 'Previous Year Dues' and session = '" + lbl_session.Text + "'");
                if (dt_m.Rows.Count == 0)
                {
                    return dt_m.Rows[0]["amountdata"].ToString() + ",oldyear";
                }
                else
                {
                    return "0.00" + ",oldyear";
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
    }
}