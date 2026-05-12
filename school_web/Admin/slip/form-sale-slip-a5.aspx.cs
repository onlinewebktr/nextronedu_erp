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
    public partial class form_sale_slip_a5 : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["transaction"] != null)
                {
                    ViewState["transaction"] = Request.QueryString["transaction"];
                    Bind_schoolinfo();
                    bind_bill_amount_amount();
                    check_print_type();
                }
            }
        }

        private void bind_bill_amount_amount()
        {
            string query = "Select asr.*,isnull((select top 1 name from user_details where user_id=asr.user_id),'NA') as Issued_by from Form_sale_details asr where asr.recpt_no='" + ViewState["transaction"].ToString() + "'";
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
                lbl_slipno.Text = lbl_slipno1.Text = dt.Rows[0]["recpt_no"].ToString();
                lbl_paymentdate.Text = lbl_paymentdate1.Text = dt.Rows[0]["date"].ToString();
                lbl_studentname.Text = lbl_studentname1.Text = dt.Rows[0]["student_name"].ToString();
                lbl_fathername.Text = lbl_fathername1.Text = dt.Rows[0]["fathers_name"].ToString();
                lbl_session.Text = lbl_session1.Text = dt.Rows[0]["session"].ToString();
                lbl_class.Text = lbl_class1.Text = dt.Rows[0]["class"].ToString();
                lbl_aadmissionno.Text = lbl_aadmissionno1.Text = dt.Rows[0]["Form_no"].ToString();
                lbl_ttl_to_pay.Text = lbl_paid.Text = lbl_paid1.Text = lbl_ttl_to_pay1.Text = My.toDouble(dt.Rows[0]["Amount"].ToString()).ToString("0.00");
                lbl_tr_no.Text = lbl_tr_no1.Text = dt.Rows[0]["Bank_tran_no"].ToString();
                lbl_bank_name.Text = lbl_bank_name1.Text = dt.Rows[0]["Bank_name"].ToString();
                lbl_issued_by.Text = lbl_issued_by1.Text = dt.Rows[0]["Issued_by"].ToString();
                lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remarks"].ToString();
                lbl_mode.Text = lbl_mode1.Text = dt.Rows[0]["Payment_Mode"].ToString();
                lbl_tr_date.Text = lbl_tr_date1.Text = "";
                if (dt.Rows[0]["Remarks"].ToString() == "")
                {
                    rmrkdV.Visible = false;
                    rmrkdV1.Visible = false;
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "Cash")
                {
                    payChekOnline.Visible = false;
                    payBankName.Visible = false;

                    payChekOnline1.Visible = false;
                    payBankName1.Visible = false;
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "Netbanking")
                {
                    payChekOnline.Visible = true;
                    payBankName.Visible = false;
                    paytrnoname.InnerText = "Tr.No.";

                    payChekOnline1.Visible = true;
                    payBankName1.Visible = false;
                    paytrnoname1.InnerText = "Tr.No.";
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "Deposited In Bank")
                {
                    payChekOnline.Visible = true;
                    payBankName.Visible = true;
                    paytrnoname.InnerText = "Tr.No.";

                    payChekOnline1.Visible = true;
                    payBankName1.Visible = true;
                    paytrnoname1.InnerText = "Tr.No.";
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "Sbdebit")
                {
                    payChekOnline.Visible = true;
                    payBankName.Visible = false;
                    paytrnoname.InnerText = "Tr.No.";

                    payChekOnline1.Visible = true;
                    payBankName1.Visible = false;
                    paytrnoname1.InnerText = "Tr.No.";
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "Cheque")
                {
                    payChekOnline.Visible = true;
                    payBankName.Visible = true;
                    paytrnoname.InnerText = "Cheque No.";

                    payChekOnline1.Visible = true;
                    payBankName1.Visible = true;
                    paytrnoname1.InnerText = "Cheque No.";
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "NEFT")
                {
                    payChekOnline.Visible = true;
                    payBankName.Visible = false;
                    paytrnoname.InnerText = "UTR No.";

                    payChekOnline1.Visible = true;
                    payBankName1.Visible = false;
                    paytrnoname1.InnerText = "UTR No.";
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "Debitcard")
                {
                    payChekOnline.Visible = true;
                    payBankName.Visible = false;
                    paytrnoname.InnerText = "Tr.No.";

                    payChekOnline1.Visible = true;
                    payBankName1.Visible = false;
                    paytrnoname1.InnerText = "Tr.No.";
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "Creditcard")
                {
                    payChekOnline.Visible = true;
                    payBankName.Visible = false;
                    paytrnoname.InnerText = "Tr.No.";

                    payChekOnline1.Visible = true;
                    payBankName1.Visible = false;
                    paytrnoname1.InnerText = "Tr.No.";
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "Otherdcard")
                {
                    payChekOnline.Visible = true;
                    payBankName.Visible = false;
                    paytrnoname.InnerText = "Tr.No.";

                    payChekOnline1.Visible = true;
                    payBankName1.Visible = false;
                    paytrnoname1.InnerText = "Tr.No.";
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "UPI")
                {
                    payChekOnline.Visible = true;
                    payBankName.Visible = false;
                    paytrnoname.InnerText = "UTR No.";

                    payChekOnline1.Visible = true;
                    payBankName1.Visible = false;
                    paytrnoname1.InnerText = "UTR No.";
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "Demand Draft(DD)")
                {
                    payChekOnline.Visible = true;
                    payBankName.Visible = true;
                    paytrnoname.InnerText = "Tr.No.";

                    payChekOnline1.Visible = true;
                    payBankName1.Visible = true;
                    paytrnoname1.InnerText = "Tr.No.";
                }
                if (dt.Rows[0]["Payment_Mode"].ToString() == "Pos")
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
                if (Session["pagew"].ToString() == "1")
                {
                    Response.Redirect("../form-sale.aspx", false);
                }
                else
                {
                    Response.Redirect("../form-sale.aspx", false);
                }
            }
            catch
            {
                Response.Redirect("../form-sale.aspx", false);
            }
        }
    }
}