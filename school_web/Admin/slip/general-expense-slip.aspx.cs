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
    public partial class general_expense_slip : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ExpnsId"] != null)
                {
                    ViewState["ExpnsId"] = Request.QueryString["ExpnsId"];
                    Bind_expense_info();
                    Bind_schoolinfo();
                }
            }
        }

        private void Bind_expense_info()
        {
            DataTable dt = mycode.FillData("select t1.*,t2.Company_name,t2.Person_name,t2.Mobile_no,t2.Address,t2.Pincode,t2.State,t2.District from Vendor_general_expense t1 join Vendor_master t2 on t1.Vendor_id=t2.Vendor_id where  Slip_no='" + ViewState["ExpnsId"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_slip_no.Text = dt.Rows[0]["Slip_no"].ToString();
                lbl_dates.Text = dt.Rows[0]["Date"].ToString();
                lbl_vendor_name.Text = dt.Rows[0]["Company_name"].ToString();
                lbl_contact_person_name.Text = dt.Rows[0]["Person_name"].ToString();
                lbl_v_mobile_no.Text = dt.Rows[0]["Mobile_no"].ToString();
                lbl_v_address.Text = dt.Rows[0]["Address"].ToString() + ", Pincode : " + dt.Rows[0]["Pincode"].ToString() + ", State : " + dt.Rows[0]["State"].ToString() + ", District : " + dt.Rows[0]["District"].ToString();

                lbl_rec_name.Text = dt.Rows[0]["Payment_handover"].ToString();
                lbl_rec_mobile.Text = dt.Rows[0]["Payment_handover_mobile_no"].ToString();
                lbl_bill_no.Text = dt.Rows[0]["Bill_no"].ToString();
                lbl_bill_date.Text = dt.Rows[0]["Bill_date"].ToString();
                lbl_bill_amt.Text = dt.Rows[0]["Bill_amount"].ToString();

                lbl_pay_amt.Text = dt.Rows[0]["Payment_amount"].ToString();
                lbl_pay_mode.Text = dt.Rows[0]["Payment_mode"].ToString();

                lbl_cheque_no.Text = dt.Rows[0]["Check_no"].ToString();
                lbl_cheque_date.Text = dt.Rows[0]["Check_date"].ToString();
                lbl_cheque_bank_name.Text = dt.Rows[0]["Check_bank_name"].ToString();
                lbl_utr_no.Text = dt.Rows[0]["Utr_no"].ToString();
                 

                int number = (int)Convert.ToDouble(dt.Rows[0]["Payment_amount"].ToString());
                string inword_number = mycode.NumberToWords(number);
                string inword = inword_number + " Only.-";
                lbl_amt_in_word.Text = inword;


                if (dt.Rows[0]["Payment_mode"].ToString() == "Cash")
                {
                    ifcheckdV.Visible = false;
                    ifcheckdV2.Visible = false;
                    ifneftdV.Visible = false;  
                }
                else if (dt.Rows[0]["Payment_mode"].ToString() == "Cheque")
                {
                    ifcheckdV.Visible = true;
                    ifcheckdV2.Visible = true;
                    ifneftdV.Visible = false;
                }
                else
                {
                    ifcheckdV.Visible = false;
                    ifcheckdV2.Visible = false;
                    ifneftdV.Visible = true;  
                }

                //
                if (dt.Rows[0]["Is_bill_no"].ToString() == "Yes")
                {
                    bill_dts.Visible = true;
                }
                else
                {
                    bill_dts.Visible = false;
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
                Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_email.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
                lbl_foor_school_name.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../general-expense.aspx", false);
        }
    }
}