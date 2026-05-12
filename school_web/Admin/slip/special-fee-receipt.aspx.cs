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
    public partial class special_fee_receipt : System.Web.UI.Page
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
                    ViewState["fromPage"] = Request.QueryString["from"];

                    hd_print_type.Value = "1";


                    Bind_schoolinfo();
                    student_details();
                    bind_amount();
                    check_print_type();
                }
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
            DataTable dt = mycode.FillData("select  top 1 *,isnull((select top 1 name from user_details where user_id=Special_fee_collection.user_id),'NA') as Issued_by from Special_fee_collection where Admission_no='" + ViewState["admissionno"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "' and Receipt_no='" + ViewState["Slip_no"].ToString() + "' order by id asc");
            if (dt.Rows.Count == 0)
            {
                ViewState["Amount"] = "0";
            }
            else
            {
                feeHead.InnerText = feeHead1.InnerText = dt.Rows[0]["Fee_head"].ToString();
                toPayAmt.InnerText = toPayAmt1.InnerText = dt.Rows[0]["Payable"].ToString();
                toPaidAmt.InnerText = toPaidAmt1.InnerText = dt.Rows[0]["Paid_amount"].ToString();

                lbl_ttl_to_pay.Text = lbl_ttl_to_pay1.Text = dt.Rows[0]["Payable"].ToString();
                lbl_ttl_paid.Text = lbl_ttl_paid1.Text = dt.Rows[0]["Paid_amount"].ToString();

                int number = (int)Convert.ToDouble(dt.Rows[0]["Paid_amount"].ToString());
                string inword_number = mycode.NumberToWords(number);
                string inword = inword_number + " Rupees Only.";
                lbl_amt_in_word.Text = lbl_amt_in_word1.Text = inword;


                lbl_slipno.Text = lbl_slipno1.Text = dt.Rows[0]["Receipt_no"].ToString();
                lbl_paymentdate.Text = lbl_paymentdate1.Text = dt.Rows[0]["Date"].ToString();
                lbl_mode.Text = lbl_mode1.Text = dt.Rows[0]["Payment_mode"].ToString();

                lbl_tr_no.Text = lbl_tr_no1.Text = dt.Rows[0]["Transaction_no"].ToString();
                lbl_tr_date.Text = lbl_tr_date1.Text = dt.Rows[0]["Transaction_date"].ToString();
                lbl_tr_bank.Text = lbl_tr_bank1.Text = dt.Rows[0]["Payee_bank_name"].ToString();


                lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remark"].ToString();
                paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                if (dt.Rows[0]["Remark"].ToString() == "")
                {
                    rmrkdV.Visible = rmrkdV1.Visible = false;
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "Cash")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = false;
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "Netbanking")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "Deposited In Bank")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = true;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "Sbdebit")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "Cheque")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = true;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Cheque No.";
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "NEFT")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "UTR No.";
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "Debitcard")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "Creditcard")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "Otherdcard")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "UPI")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "UTR No.";
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "Demand Draft(DD)")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    // payBankName.Visible = true;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
                if (dt.Rows[0]["Payment_mode"].ToString() == "Pos")
                {
                    payChekOnline.Visible = payChekOnline1.Visible = true;
                    //payBankName.Visible = false;
                    paytrnoname.InnerText = paytrnoname1.InnerText = "Transaction No.";
                }
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
                Response.Redirect("../" + ViewState["fromPage"].ToString(), false); 
                //Response.Redirect("../fee-collection-monthly-wise.aspx?adm=" + lbl_aadmissionno.Text, false);
            }
            catch
            {
            }
        }
    }
}