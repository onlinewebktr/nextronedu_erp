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
    public partial class payment_slip1 : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["admissionno"] != null)
                {
                    ViewState["mobile"] = "0";
                    midredio.Visible = true;
                    ViewState["admissionno"] = Request.QueryString["admissionno"];
                    ViewState["sessionid"] = Request.QueryString["sessionid"];
                    ViewState["classid"] = Request.QueryString["classid"];
                    ViewState["Slip_no"] = Request.QueryString["Slip_no"];
                    ViewState["session"] = mycode.get_session(ViewState["sessionid"].ToString());

                    hd_session.Value = ViewState["session"].ToString();
                    hd_adm_no.Value = ViewState["admissionno"].ToString();
                    hd_slip_no.Value = ViewState["Slip_no"].ToString();
                    try
                    {
                        string isopenFromMBL = Request.QueryString["mobile"];
                        if (isopenFromMBL.ToUpper() == "YES")
                        {
                            ViewState["mobile"] = isopenFromMBL;
                            midredio.Visible = false;
                            hd_print_type.Value = "3";
                        }
                    }
                    catch
                    {
                        hd_print_type.Value = "1";
                    }

                    Bind_schoolinfo();
                    student_details();
                    bind_amount();
                    Bind_particular();
                    get_months();
                    check_print_type();


                }
            }
        }

        private void get_months()
        {
            string months = "";
            DataTable dt = My.dataTable("select distinct t1.Month,t2.Position from Monthly_Fee_Collection_Slip t1 join Month_Index t2 on t1.Month=t2.Month where t1.session='" + ViewState["session"].ToString() + "' and  t1.slipno='" + ViewState["Slip_no"].ToString() + "' and t1.adno='" + ViewState["admissionno"].ToString() + "' order by t2.Position asc");
            if (dt.Rows.Count > 0)
            {
                months = dt.Rows[0]["Month"].ToString();
                if (dt.Rows.Count == 1)
                {
                    months = dt.Rows[0]["Month"].ToString();
                }
                else
                {
                    months = months + " to " + dt.Rows[dt.Rows.Count - 1]["Month"].ToString();
                }
                lbl_months.Text = lbl_months1.Text = months;
            }
        }

        private void check_print_type()
        {

            try
            {
                DataTable dt = mycode.FillData("select Monthly_slip_print_type from globle_data");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Monthly_slip_print_type"].ToString() != "")
                    {
                        hd_print_type.Value = dt.Rows[0]["Monthly_slip_print_type"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (hd_print_type.Value == "1")
            {
                rdo_both.Checked = true;
                rdo_office_copy.Checked = false;
                rdo_student_copy.Checked = false;
            }
            else if (hd_print_type.Value == "2")
            {
                rdo_both.Checked = false;
                rdo_office_copy.Checked = true;
                rdo_student_copy.Checked = false;
            }
            else
            {
                rdo_both.Checked = false;
                rdo_office_copy.Checked = false;
                rdo_student_copy.Checked = true;
            }
        }


        private void bind_amount()
        {
            DataTable dt = mycode.FillData("select  top 1 *,isnull((select top 1 name from user_details where user_id=Student_Payment_History.user_id),'NA') as Issued_by from Student_Payment_History where Addmission_no='" + ViewState["admissionno"].ToString() + "' and Session='" + ViewState["session"].ToString() + "' and Slip_no='" + ViewState["Slip_no"].ToString() + "' order by id asc");
            if (dt.Rows.Count == 0)
            {
                ViewState["Amount"] = "0";
            }
            else
            {
                lbl_slipno.Text = lbl_slipno1.Text = dt.Rows[0]["Slip_no"].ToString();
                lbl_paymentdate.Text = lbl_paymentdate1.Text = dt.Rows[0]["Date"].ToString();
                lbl_mode.Text = lbl_mode1.Text = dt.Rows[0]["mode"].ToString();

                lbl_tr_no.Text = lbl_tr_no1.Text = dt.Rows[0]["Pay_mode_transaction_no"].ToString();
                lbl_tr_date.Text = lbl_tr_date1.Text = dt.Rows[0]["Bank_date"].ToString();
                //lbl_bank_name.Text = dt.Rows[0]["Bank_name"].ToString();
                // lbl_issued_by.Text = dt.Rows[0]["Issued_by"].ToString();
                lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remarks"].ToString();

                paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                if (dt.Rows[0]["Remarks"].ToString() == "")
                {
                    rmrkdV.Visible = rmrkdV1.Visible = false;
                }
                if (dt.Rows[0]["mode"].ToString() == "Cash")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = false;
                    //payBankName.Visible = false;
                }
                if (dt.Rows[0]["mode"].ToString() == "Netbanking")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["mode"].ToString() == "Deposited In Bank")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = true;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["mode"].ToString() == "Sbdebit")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["mode"].ToString() == "Cheque")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = true;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Cheque No.";
                }
                if (dt.Rows[0]["mode"].ToString() == "NEFT")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "UTR No.";
                }
                if (dt.Rows[0]["mode"].ToString() == "Debitcard")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["mode"].ToString() == "Creditcard")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["mode"].ToString() == "Otherdcard")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["mode"].ToString() == "UPI")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "UTR No.";
                }
                if (dt.Rows[0]["mode"].ToString() == "Demand Draft(DD)")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = true;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["mode"].ToString() == "Pos")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    //payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
            }
        }




        private void Bind_particular()
        {
            string quer = "select Particular,(sum(convert(float, fee_amount))-(sum(convert(float, previously_paid))+sum(convert(float, disc_amt)))) as fee_amount,sum(convert(float, previously_paid)) as previously_paid,sum(convert(float, disc_amt)) as disc_amt,sum(convert(float, payable)) as payable,sum(convert(float, paid)) as paid from (select Month,Content as Particular, cast(payable AS float) fee_amount,cast(previously_paid AS float) previously_paid,cast(disc_amt AS float) disc_amt,(cast(payable AS float)-cast(previously_paid AS float)-cast(disc_amt AS float)) payable,      cast(paid AS float)  as paid from Monthly_Fee_Collection_Slip where session='" + ViewState["session"].ToString() + "' and  slipno='" + ViewState["Slip_no"].ToString() + "' and adno='" + ViewState["admissionno"].ToString() + "') t group by Particular";
            DataTable dt = mycode.FillData(quer);
            if (dt.Rows.Count == 0)
            {
                grd_fees.DataSource = null;
                grd_fees.DataBind();
            }
            else
            {
                grd_fees.DataSource = dt;
                grd_fees.DataBind();

                String Total_to_pay = Convert.ToDouble(dt.Compute("SUM(fee_amount)", string.Empty)).ToString();
                String Total_paid = Convert.ToDouble(dt.Compute("SUM(paid)", string.Empty)).ToString();
                String Total_discount = Convert.ToDouble(dt.Compute("SUM(disc_amt)", string.Empty)).ToString();
                lbl_ttl_to_pay.Text = lbl_ttl_to_pay1.Text = Total_to_pay;
                lbl_ttl_paid.Text = lbl_ttl_paid1.Text = Total_paid;


                double remaining_amt = (My.toDouble(Total_to_pay) - My.toDouble(Total_paid));
                lbl_remaining_amt.Text = lbl_remaining_amt1.Text = remaining_amt.ToString(); // - My.toDouble(Total_discount)).ToString();
                lbl_discount_amt.Text = lbl_discount_amt1.Text = Total_discount.ToString();


                int number = (int)Convert.ToDouble(Total_paid);
                string inword_number = mycode.NumberToWords(number);
                string inword = inword_number + " Only.";
                lbl_amt_in_word.Text = lbl_amt_in_word1.Text = inword;
            }
        }


        private void student_details()
        {
            DataTable dt = mycode.FillData("select studentname,admissionserialnumber,class,Section,fathername,session,rollnumber,Academic_Sem_or_Year from admission_registor where admissionserialnumber='" + ViewState["admissionno"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and Class_id=" + ViewState["classid"].ToString() + " ");
            if (dt.Rows.Count > 0)
            {
                lbl_studentname.Text = lbl_studentname1.Text = dt.Rows[0]["studentname"].ToString();
                lbl_aadmissionno.Text = lbl_aadmissionno1.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_class.Text = lbl_class1.Text = dt.Rows[0]["class"].ToString();
                lbl_section.Text = lbl_section1.Text = dt.Rows[0]["Section"].ToString();
                lbl_fathername.Text = lbl_fathername1.Text = dt.Rows[0]["fathername"].ToString();
                lbl_session.Text = lbl_session1.Text = dt.Rows[0]["session"].ToString();
                // lbl_rollno.Text = dt.Rows[0]["rollnumber"].ToString();
            }
        }

        private void Bind_schoolinfo()
        {
            ViewState["page_reset"] = "0";
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                try
                {
                    if (dt.Rows[0]["Is_page_reset_after_fee_payment"].ToString() == "")
                    {
                        ViewState["page_reset"] = "0";
                    }
                    else
                    {
                        ViewState["page_reset"] = dt.Rows[0]["Is_page_reset_after_fee_payment"].ToString();
                    }

                }
                catch
                {
                    ViewState["page_reset"] = "0";
                }

                imglogo.ImageUrl = imglogo1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_heading.Text = lbl_heading1.Text = dt.Rows[0]["firm_name"].ToString();
                Image1.ImageUrl = Image2.ImageUrl = dt.Rows[0]["logo"].ToString();



                try
                {
                    if (dt.Rows[0]["Is_slip_header"].ToString() == "True")
                    {
                        ContentHeader.Visible = ContentHeader1.Visible = false;
                        TempleteHeader.Visible = TempleteHeader1.Visible = true;
                        img_header.ImageUrl = img_header1.ImageUrl = dt.Rows[0]["Header_images"].ToString();
                    }
                    else
                    {
                        TempleteHeader.Visible = TempleteHeader1.Visible = false;
                        ContentHeader1.Visible = ContentHeader.Visible = true;
                    }
                }
                catch
                {
                    ContentHeader1.Visible = ContentHeader.Visible = true;
                    TempleteHeader.Visible = TempleteHeader1.Visible = false;
                }

                try
                {
                    Image2.Visible = Image1.Visible = true;
                    if (dt.Rows[0]["Is_certificate_watermark_hide"].ToString() == "True")
                    {
                        Image2.Visible = Image1.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["mobile"].ToString().ToUpper() == "YES")
                {
                    string url = My.url() + "Student_Profile/webview/My_Transaction.aspx?regid=" + lbl_aadmissionno.Text;
                    Response.Redirect(url, false);
                }
                else
                {
                    if (ViewState["page_reset"].ToString() == "1")
                    {
                        if (Session["SlipBkSn"].ToString() == "MN1")
                        {
                            Response.Redirect("../fee-collection-monthly-wise.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN2")
                        {
                            Response.Redirect("../fee-collection.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN3")
                        {
                            Response.Redirect("../reprint-monthly-fees.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN4")
                        {
                            Response.Redirect("../Online_Monthly_Payment_History.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN5")
                        {
                            Response.Redirect("../fee-collections.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN6")
                        {
                            Response.Redirect("../fees-collection.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN7")
                        {
                            Response.Redirect("../fees-collections.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN8")
                        {
                            Response.Redirect("../fees-collection-1.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN9")
                        {
                            Response.Redirect("../fees-collection-2.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN33")
                        {
                            Response.Redirect("../reprint-bill.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN88")
                        {
                            Response.Redirect("../fees-collection-3.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("../reprint-monthly-fees.aspx", false);
                        }
                    }
                    else
                    {

                        if (Session["SlipBkSn"].ToString() == "MN1")
                        {
                            Response.Redirect("../fee-collection-monthly-wise.aspx?adm=" + lbl_aadmissionno.Text, false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN2")
                        {
                            Response.Redirect("../fee-collection.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN3")
                        {
                            Response.Redirect("../reprint-monthly-fees.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN4")
                        {
                            Response.Redirect("../Online_Monthly_Payment_History.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN5")
                        {
                            Response.Redirect("../fee-collections.aspx?adm=" + lbl_aadmissionno.Text, false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN6")
                        {
                            Response.Redirect("../fees-collection.aspx?adm=" + lbl_aadmissionno.Text, false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN7")
                        {
                            Response.Redirect("../fees-collections.aspx?adm=" + lbl_aadmissionno.Text, false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN8")
                        {
                            Response.Redirect("../fees-collection-1.aspx?adm=" + lbl_aadmissionno.Text + "&sessionid=" + ViewState["sessionid"].ToString(), false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN9")
                        {
                            Response.Redirect("../fees-collection-2.aspx?adm=" + lbl_aadmissionno.Text, false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN33")
                        {
                            Response.Redirect("../reprint-bill.aspx", false);
                        }
                        else if (Session["SlipBkSn"].ToString() == "MN88")
                        {
                            Response.Redirect("../fees-collection-3.aspx?adm=" + lbl_aadmissionno.Text + "&sessionid=" + ViewState["sessionid"].ToString(), false);
                        }
                        else
                        {
                            Response.Redirect("../reprint-monthly-fees.aspx", false);
                        }
                    }
                }
            }
            catch
            {
                Response.Redirect("../Online_Monthly_Payment_History.aspx", false);
            }
        }
    }
}