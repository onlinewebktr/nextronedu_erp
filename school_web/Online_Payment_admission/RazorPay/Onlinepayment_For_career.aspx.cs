using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Payment_admission.RazorPay
{

    public partial class Check_out_Onlinepayment_For_career : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sytemid"] == null)
                {

                }
                else
                {

                    key_id.Value = My.MERCHANT_KEY();
                    name.Value = mycode.get_firm_name();
                    Bind_data();



                }
            }
        }
        private void Bind_data()
        {

            string query = "Select * from Employee_Online_Apply where Order_id='" + Session["sytemid"] + "'  ";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Session["regid"] = dr["Apply_id"].ToString();
                    double amtForRazorrounD = Math.Round(Convert.ToDouble(dr["Payable_amount"].ToString()));
                    int aftrrounD = (Convert.ToInt32(amtForRazorrounD) * 100);
                    amount.Value = aftrrounD.ToString();
                    if (dr["Emailid"].ToString() == "")
                    {
                        email.Value = "noreply@abcd.com";
                    }
                    else
                    {
                        email.Value = dr["Emailid"].ToString();
                    }
                    if (dr["mobile_no_CA"].ToString() == "")
                    {
                        contact.Value = "000000000";
                    }
                    else
                    {
                        contact.Value = dr["mobile_no_CA"].ToString();

                    }

                    transaction_id.Value = dr["Registration_id"].ToString();
                    order_id.Value = dr["razorpay_order_id"].ToString();
                    callback_url.Value = My.url() + "Online_Payment_admission/RazorPay/callback_career.aspx";

                }
            }
        }
    }
}