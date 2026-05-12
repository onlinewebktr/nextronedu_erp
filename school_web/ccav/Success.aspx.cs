using CCA.Util;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.ccav
{
    public partial class Success : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lbl_date.Text = mycode.date();
                    if (Request.Form["encResp"] != null)
                    {
                        binddatafromcca();
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }


        #region Online_payment_CCA
        private void binddatafromcca()
        {
            string workingKey = "E83ACB90DA0B89B8A16C74271DD48474";//put in the 32bit alpha numeric key in the quotes provided here
            CCACrypto ccaCrypto = new CCACrypto();
            string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
            NameValueCollection Params = new NameValueCollection();
            string[] segments = encResponse.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();
                    Params.Add(Key, Value);
                }
            }

            if (Params["order_status"].ToString() == "Aborted" || Params["order_status"].ToString() == "Failure" || Params["order_status"].ToString() == "Awaited" || Params["order_status"].ToString() == "Invalid" || Params["order_status"].ToString() == "")
            {
                Session["uid"] = Params["merchant_param1"].ToString();
                hd_bookingid.Value = Session["uid"].ToString();

                Session["OrderId"] = Params["order_id"].ToString();
                //string qry = @"insert into Transection_Details(Regid, Order_no,Tracking_id,Bank_Ref_no,Order_status,Failure_message,Payment_mode,Card_or_bank_name,Status_code,Status_message,Curency,Amount,Date,Idate) values
                //    ('" + Params["merchant_param1"].ToString() + "','" + Params["order_id"].ToString() + "','" + Params["tracking_id"].ToString() + "','" + Params["bank_ref_no"].ToString() + "','" + Params["order_status"].ToString() + "','" + Params["failure_message"].ToString() + "','" + Params["payment_mode"].ToString() + "','" + Params["card_name"].ToString() + "','" + Params["status_code"].ToString() + "','" + Params["status_message"].ToString() + "','" + Params["currency"].ToString() + "','" + Params["amount"].ToString() + "','" + My.date() + "','" + My.idate() + "')";

                //My.exeSql(qry);

                //string path = "cancle_payment.aspx?regno=" + hd_bookingid.Value;
                //Response.Redirect(path, false);
                lbl_status.Text = "Cancel 001";
            }
            else if (Params["order_status"].ToString() == "Success")
            {
                Session["uid"] = Params["merchant_param1"].ToString();
                hd_bookingid.Value = Session["uid"].ToString();

                Session["OrderId"] = Params["order_id"].ToString();
                //string qry = @"insert into Transection_Details(Regid, Order_no,Tracking_id,Bank_Ref_no,Order_status,Failure_message,Payment_mode,Card_or_bank_name,Status_code,Status_message,Curency,Amount,Date,Idate) values
                //    ('" + Params["merchant_param1"].ToString() + "','" + Params["order_id"].ToString() + "','" + Params["tracking_id"].ToString() + "','" + Params["bank_ref_no"].ToString() + "','" + Params["order_status"].ToString() + "','" + Params["failure_message"].ToString() + "','" + Params["payment_mode"].ToString() + "','" + Params["card_name"].ToString() + "','" + Params["status_code"].ToString() + "','" + Params["status_message"].ToString() + "','" + Params["currency"].ToString() + "','" + Params["amount"].ToString() + "','" + My.date() + "','" + My.idate() + "')";
                //My.exeSql(qry);

                //string query = @"update Billing_info set Pay_orderid='" + Params["order_id"].ToString() + "',Pay_status='Paid',Pay_desc='" + Params["tracking_id"].ToString() + "' where Booking_id='" + hd_bookingid.Value + "';";

                // My.exeSql(query);
                //calculate_total();
                //finduserdetails();
                //mail_sending();

                lbl_transactionid.Text = Params["tracking_id"].ToString();
                lbl_m_t_id.Text = Params["tracking_id"].ToString();
                lbl_status.Text = "Success  002";
            }
            else
            {
                Session["uid"] = Params["merchant_param1"].ToString();
                hd_bookingid.Value = Session["uid"].ToString();
                lbl_status.Text = "Cancel 002";
                //Session["OrderId"] = Params["order_id"].ToString();
                //string qry = @"insert into Transection_Details(Regid, Order_no,Tracking_id,Bank_Ref_no,Order_status,Failure_message,Payment_mode,Card_or_bank_name,Status_code,Status_message,Curency,Amount,Date,Idate) values
                //    ('" + Params["merchant_param1"].ToString() + "','" + Params["order_id"].ToString() + "','" + Params["tracking_id"].ToString() + "','" + Params["bank_ref_no"].ToString() + "','" + Params["order_status"].ToString() + "','" + Params["failure_message"].ToString() + "','" + Params["payment_mode"].ToString() + "','" + Params["card_name"].ToString() + "','" + Params["status_code"].ToString() + "','" + Params["status_message"].ToString() + "','" + Params["currency"].ToString() + "','" + Params["amount"].ToString() + "','" + My.date() + "','" + My.idate() + "')";

                //My.exeSql(qry);

                //string path = "cancle_payment.aspx?regno=" + hd_bookingid.Value;
                //Response.Redirect(path, false);
            }
        }




        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        #endregion Online_payment_CCA
    }
}