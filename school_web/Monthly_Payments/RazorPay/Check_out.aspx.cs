using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Monthly_Payments.RazorPay
{
    public partial class Check_out : System.Web.UI.Page
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

            string query = "Select * from Payment_transaction_process where ordertrackingid='" + Session["sytemid"] + "'  ";
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
                    Session["regid"] = dr["Admission_no"].ToString();
                    double amtForRazorrounD = Math.Round(Convert.ToDouble(dr["Total_pay"].ToString()));
                    int aftrrounD = (Convert.ToInt32(amtForRazorrounD) * 100);
                    amount.Value = aftrrounD.ToString();
                    email.Value = dr["Emailid"].ToString();
                    contact.Value = dr["Mobileno"].ToString();
                    transaction_id.Value = dr["Admission_no"].ToString();
                    order_id.Value = dr["razorpay_order_id"].ToString();
                    callback_url.Value = My.url() + "Monthly_Payments/RazorPay/PayProcess_blank_callback.aspx";

                }
            }
        }
    }
}