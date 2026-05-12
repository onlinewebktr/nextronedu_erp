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
    public partial class expense_slip : System.Web.UI.Page
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
            DataTable dt = mycode.FillData("select * from General_expense  where  Slip_no='" + ViewState["ExpnsId"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_slip_no.Text = dt.Rows[0]["Slip_no"].ToString();
                lbl_dates.Text = dt.Rows[0]["Date"].ToString();
                lbl_particular.Text = dt.Rows[0]["Payment_mode"].ToString();
                lbl_amount.Text = dt.Rows[0]["Amount"].ToString();
                lbl_remarks.Text = dt.Rows[0]["Description"].ToString();
                lbl_ttl.Text = dt.Rows[0]["Amount"].ToString();


                lbl_slip_no1.Text = dt.Rows[0]["Slip_no"].ToString();
                lbl_dates1.Text = dt.Rows[0]["Date"].ToString();
                lbl_particular1.Text = dt.Rows[0]["Payment_mode"].ToString();
                lbl_amount1.Text = dt.Rows[0]["Amount"].ToString();
                lbl_remarks1.Text = dt.Rows[0]["Description"].ToString();
                lbl_ttl1.Text = dt.Rows[0]["Amount"].ToString();

                int number = (int)Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                string inword_number = mycode.NumberToWords(number);
                string inword = inword_number + " Only.-";
                lbl_ttl_in_rupee.Text = lbl_ttl_in_rupee1.Text = inword;
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


                Image2.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address1.Text = dt.Rows[0]["address1"].ToString();
                lbl_email1.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name1.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no1.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
                lbl_foor_school_name1.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../general-expense.aspx", false);
        }
    }
}