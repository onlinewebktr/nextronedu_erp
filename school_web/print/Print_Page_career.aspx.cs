using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.print
{
    public partial class Print_Page_career : System.Web.UI.Page
    {
        UsesCode My = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["PaymentSliP"] != null)
                    {
                        string PaymentSliP = Request.QueryString["PaymentSliP"];
                        bind_data(PaymentSliP);
                        firm_data();
                    }
                }
            }
            catch
            {

            }
        }

        private void firm_data()
        {
            DataTable dt = My.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_affiliation_no.Text = dt.Rows[0]["Affiliation"].ToString();
                lbl_schoolno.Text = dt.Rows[0]["school_no"].ToString();
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();

            }
        }
        My mycode = new My();
        private void bind_data(string PaymentSliP)
        {
            DataTable dt = My.FillData("select * from Employee_Online_Apply where Apply_id='" + PaymentSliP + "' ");
            if (dt.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                lbl_session.Text = mycode.get_session(dt.Rows[0]["Session_id"].ToString());
                lbl_slipno.Text = dt.Rows[0]["Apply_id"].ToString();
                lbl_paymentdate.Text = dt.Rows[0]["Date"].ToString();


                lbl_studentname.Text = dt.Rows[0]["First_Name"].ToString() + " " + dt.Rows[0]["Middle_Name"].ToString() + " " + dt.Rows[0]["Last_Name"].ToString();

                lbl_fathername.Text = dt.Rows[0]["fathername"].ToString();

                lbl_mobileno.Text = dt.Rows[0]["mobile_no_CA"].ToString();

                lbl_adress.Text = dt.Rows[0]["Address_CA"].ToString() + " " + dt.Rows[0]["City_CA"].ToString() + " " + dt.Rows[0]["State_CA"].ToString() + " " + dt.Rows[0]["Pincode_CA"].ToString();

                grd_fee.DataSource = dt;
                grd_fee.DataBind();

                int number = (int)Convert.ToDouble(dt.Rows[0]["Payable_amount"].ToString());
                string inword_number = mycode.NumberToWords(number);
                string inword = inword_number + " Only";
                lbl_amountinword.Text = inword;

                if (dt.Rows[0]["Seat_Remarks"].ToString() == "OK")
                {
                    lbl_msg.Text = "";
                }
                else
                {
                    lbl_msg.Text = "Sorry your application is not confirmed due to seat full, Please contact the school administration fee refund.";
                    
                }

            }

        }


        protected void btn_back_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Default.aspx", false);

        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }
    }
}