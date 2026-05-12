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
    public partial class Advance_Pay_slip : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["receiptid"] != null)
                {
                    ViewState["receiptid"] = Request.QueryString["receiptid"];
                    Bind_schoolinfo();
                    bind_bill_amount_amount();
                    check_print_type();
                }
            }

        }

        private void bind_bill_amount_amount()
        {
            bind_grd_view("select t1.Id,t2.rollnumber, t2.mobilenumber,t2.fathername, t2.studentname,t1.Adm_no as Admission_no,t2.class,t2.session,format(t1.Date_of_entry,'dd/MM/yyyy') as Payment_date,t1.slipno as slipno,t1.Mode as Payment_mode,t2.Session_id,t1.Wallet_input_amount as Amount,t1.Add_type as Content_Name,t1.Pay_mode_transaction_no,t1.*  from STUDENT_WALLET t1 join admission_registor t2 on  t1.Adm_no=t2.admissionserialnumber   where  t2.Transfer_Status in ('NT','New')  and t1.slipno='" + ViewState["receiptid"].ToString() + "' and t1.Add_type='Advance Pay' ");
        }

        private void bind_grd_view(string query)
        {
           
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    grd_fees.DataSource = null;
                    grd_fees.DataBind();
                    rp_fees1.DataSource = null;
                    rp_fees1.DataBind();
                }
                else
                {
                    grd_fees.DataSource = dt;
                    grd_fees.DataBind();
                    rp_fees1.DataSource = dt;
                    rp_fees1.DataBind();
                    lbl_slipno.Text = lbl_slipno1.Text = dt.Rows[0]["slipno"].ToString();
                    lbl_paymentdate.Text = lbl_paymentdate1.Text = dt.Rows[0]["Payment_date"].ToString();
                    lbl_studentname.Text = lbl_studentname1.Text = dt.Rows[0]["studentname"].ToString();
                    lbl_fathername.Text = lbl_fathername1.Text = dt.Rows[0]["fathername"].ToString();
                    lbl_session.Text = lbl_session1.Text = dt.Rows[0]["session"].ToString();
                    
                    lbl_aadmissionno.Text = lbl_aadmissionno1.Text = dt.Rows[0]["Admission_no"].ToString();
                    lbl_ttl_to_pay.Text = lbl_paid.Text = lbl_paid1.Text = lbl_ttl_to_pay1.Text = My.toDouble(dt.Rows[0]["Amount"].ToString()).ToString("0.00");
                    lbl_tr_no.Text = lbl_tr_no1.Text = dt.Rows[0]["Pay_mode_transaction_no"].ToString();
                    lbl_bank_name.Text = lbl_bank_name1.Text = dt.Rows[0]["Bank_name"].ToString();
                    lbl_issued_by.Text = lbl_issued_by1.Text = mycode.get_user(dt.Rows[0]["Created_By"].ToString());
                    lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remakes"].ToString();
                    lbl_mode.Text = lbl_mode1.Text = dt.Rows[0]["Payment_mode"].ToString();
                    lbl_tr_date.Text = lbl_tr_date1.Text = "";
                    if (dt.Rows[0]["Remakes"].ToString() == "")
                    {
                        rmrkdV.Visible = false;
                        rmrkdV1.Visible = false;
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Cash")
                    {
                        payChekOnline.Visible = false;
                        payBankName.Visible = false;

                        payChekOnline1.Visible = false;
                        payBankName1.Visible = false;
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Netbanking")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = false;
                        paytrnoname.InnerText = "Tr.No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = false;
                        paytrnoname1.InnerText = "Tr.No.";
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Deposited In Bank")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = true;
                        paytrnoname.InnerText = "Tr.No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = true;
                        paytrnoname1.InnerText = "Tr.No.";
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Sbdebit")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = false;
                        paytrnoname.InnerText = "Tr.No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = false;
                        paytrnoname1.InnerText = "Tr.No.";
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Cheque")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = true;
                        paytrnoname.InnerText = "Cheque No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = true;
                        paytrnoname1.InnerText = "Cheque No.";
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "NEFT")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = false;
                        paytrnoname.InnerText = "UTR No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = false;
                        paytrnoname1.InnerText = "UTR No.";
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Debitcard")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = false;
                        paytrnoname.InnerText = "Tr.No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = false;
                        paytrnoname1.InnerText = "Tr.No.";
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Creditcard")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = false;
                        paytrnoname.InnerText = "Tr.No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = false;
                        paytrnoname1.InnerText = "Tr.No.";
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Otherdcard")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = false;
                        paytrnoname.InnerText = "Tr.No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = false;
                        paytrnoname1.InnerText = "Tr.No.";
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "UPI")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = false;
                        paytrnoname.InnerText = "UTR No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = false;
                        paytrnoname1.InnerText = "UTR No.";
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Demand Draft(DD)")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = true;
                        paytrnoname.InnerText = "Tr.No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = true;
                        paytrnoname1.InnerText = "Tr.No.";
                    }
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Pos")
                    {
                        payChekOnline.Visible = true;
                        payBankName.Visible = false;
                        paytrnoname.InnerText = "Tr.No.";

                        payChekOnline1.Visible = true;
                        payBankName1.Visible = false;
                        paytrnoname1.InnerText = "Tr.No.";
                    }
                }
            
        }

        private void check_print_type()
        {
            hd_print_type.Value = "1";
            if (hd_print_type.Value == "1")
            {
                rdo_both.Checked = true;
                rdo_office_copy.Checked = false;
                rdo_student_copy.Checked = false;
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
                imglogo.ImageUrl = Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_heading.Text = lbl_heading1.Text = dt.Rows[0]["firm_name"].ToString();
                Image2.ImageUrl = dt.Rows[0]["logo"].ToString();
                Image3.ImageUrl = dt.Rows[0]["logo"].ToString();
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
                        ContentHeader.Visible = ContentHeader1.Visible = true;
                    }
                }
                catch
                {
                    ContentHeader.Visible = ContentHeader1.Visible = true;
                    TempleteHeader.Visible = TempleteHeader1.Visible = false;
                }
                try
                {
                    Image2.Visible = true;
                    if (dt.Rows[0]["Is_certificate_watermark_hide"].ToString() == "True")
                    {
                        Image2.Visible = false;
                        Image3.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["pageadv"].ToString() == "1")
                {
                    Response.Redirect("../Advance_amount_report.aspx", false);
                }
                else
                {
                    Response.Redirect("../Add_Advance_Amount.aspx", false);
                }
            }
            catch
            {
                Response.Redirect("../Advance_amount_report.aspx", false);
            }
        }
    }
}