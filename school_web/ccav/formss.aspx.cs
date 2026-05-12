using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.ccav
{
    public partial class formss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string ccaRequest = "";
        public string strEncRequest = "";
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                #region ccavenue_payment_gateway

                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                string amt = Convert.ToString(Convert.ToDecimal(txt_amount.Text)); //// for testing  
                Random r = new Random();
                string orderid = r.Next(10000, 99999).ToString();
                string ti = "145" + r.Next(100000, 999999).ToString();

                // ccaRequest += "tid=1453199731904&";
                ccaRequest = ccaRequest + "tid" + "= " + ti + "&";
                ccaRequest = ccaRequest + "merchant_id" + "=2348104&";
                ccaRequest = ccaRequest + "order_id" + "=" + orderid + "&";
                ccaRequest = ccaRequest + "amount" + "= " + amt + "&";
                ccaRequest = ccaRequest + "currency=INR&";
                ccaRequest = ccaRequest + "redirect_url" + "=https://school.epicpublicschool.co.in/ccav/Success.aspx&";
                ccaRequest = ccaRequest + "cancel_url" + "=https://school.epicpublicschool.co.in/cancle_payment.aspx&";
                ccaRequest = ccaRequest + "billing_name" + "=" + txt_name.Text + "&";
                ccaRequest = ccaRequest + "billing_address" + "=&";
                ccaRequest = ccaRequest + "billing_city" + "=&";
                ccaRequest = ccaRequest + "billing_state" + "=&";
                ccaRequest = ccaRequest + "billing_zip" + "=&";
                ccaRequest = ccaRequest + "billing_country" + "=India&";
                ccaRequest = ccaRequest + "billing_tel" + "=" + txt_mobile.Text + "&";
                ccaRequest = ccaRequest + "billing_email" + "=" + txt_email.Text + "&";
                ccaRequest = ccaRequest + "customer_identifier" + "=" + hd_bookingid.Value + "&";
                ccaRequest = ccaRequest + "delivery_name" + "=" + txt_name.Text + "&";
                ccaRequest = ccaRequest + "delivery_address" + "=&";
                ccaRequest = ccaRequest + "delivery_city" + "=&";
                ccaRequest = ccaRequest + "delivery_state" + "=&";
                ccaRequest = ccaRequest + "delivery_zip" + "=&";
                ccaRequest = ccaRequest + "delivery_country" + "=India&";
                ccaRequest = ccaRequest + "delivery_tel" + "=" + txt_mobile.Text + "&";
                ccaRequest = ccaRequest + "merchant_param1" + "=" + hd_bookingid.Value + "&";
                ccaRequest = ccaRequest + "merchant_param2" + "=&";
                ccaRequest = ccaRequest + "merchant_param3" + "=&";
                ccaRequest = ccaRequest + "merchant_param4" + "=&";
                ccaRequest = ccaRequest + "merchant_param5" + "=&";
                ccaRequest = ccaRequest + "promo_code" + "=&";
                Session["test"] = ccaRequest; // set  parameters values in session without encription  
                try
                {
                    Response.Redirect("ccavRequest_Handler.aspx", false); // redirect to ccavRequestHandler.aspx for encription and next process 
                    //Response.Redirect("ccavRequestHandler.aspx?enc=" + strEncRequest+" ", false);
                }
                catch (Exception ex)
                {
                } 

                #endregion   ccavenue_payment_gateway
            }
            catch (Exception ex)
            {

            }
        }
    }
}