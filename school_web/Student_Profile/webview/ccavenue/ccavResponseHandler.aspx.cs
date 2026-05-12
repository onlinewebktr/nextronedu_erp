using CCA.Util;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.ccavenue
{
    public partial class ccavResponseHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.Form["encResp"] != null)
                    {
                        ViewState["payFrom"] = "1";
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
            string workingKey = My.Working_KEY_CCAV(); // "E83ACB90DA0B89B8A16C74271DD48474";//put in the 32bit alpha numeric key in the quotes provided here
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
                ViewState["statuS"] = Params["order_status"].ToString();
                Session["orderno"] = Params["merchant_param1"].ToString();
                //hd_bookingid.Value = Session["uid"].ToString(); 
                //Session["OrderId"] = Params["order_id"].ToString();
                string qry = @"insert into Transection_Details(Regid, Order_no,Tracking_id,Bank_Ref_no,Order_status,Failure_message,Payment_mode,Card_or_bank_name,Status_code,Status_message,Curency,Amount,Date,Idate) values
                    ('" + Params["merchant_param1"].ToString() + "','" + Params["order_id"].ToString() + "','" + Params["tracking_id"].ToString() + "','" + Params["bank_ref_no"].ToString() + "','" + Params["order_status"].ToString() + "','" + Params["failure_message"].ToString() + "','" + Params["payment_mode"].ToString() + "','" + Params["card_name"].ToString() + "','" + Params["status_code"].ToString() + "','" + Params["status_message"].ToString() + "','" + Params["currency"].ToString() + "','" + Params["amount"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "')";
                My.exeSql(qry);
                find_std_details(Session["orderno"].ToString());
                btn_send_data();
            }
            else if (Params["order_status"].ToString() == "Success")
            {
                Session["orderno"] = Params["merchant_param1"].ToString();
                find_std_details(Session["orderno"].ToString());
                ViewState["statuS"] = "Paid";
                Session["OrderId"] = Params["order_id"].ToString();
                My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + Params["order_id"].ToString() + "',razorpay_signature='" + Params["tracking_id"].ToString() + "',status='" + ViewState["statuS"].ToString() + "' where Admission_no='" + hd_admission_no.Value + "' and ordertrackingid='" + Params["merchant_param1"].ToString() + "'");

                //======================
                string qry = @"insert into Transection_Details(Regid, Order_no,Tracking_id,Bank_Ref_no,Order_status,Failure_message,Payment_mode,Card_or_bank_name,Status_code,Status_message,Curency,Amount,Date,Idate) values
                    ('" + Params["merchant_param1"].ToString() + "','" + Params["order_id"].ToString() + "','" + Params["tracking_id"].ToString() + "','" + Params["bank_ref_no"].ToString() + "','" + Params["order_status"].ToString() + "','" + Params["failure_message"].ToString() + "','" + Params["payment_mode"].ToString() + "','" + Params["card_name"].ToString() + "','" + Params["status_code"].ToString() + "','" + Params["status_message"].ToString() + "','" + Params["currency"].ToString() + "','" + Params["amount"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "')";
                My.exeSql(qry);

                btn_send_data();
            }
            else
            {
                ViewState["statuS"] = Params["order_status"].ToString();
                Session["uid"] = Params["merchant_param1"].ToString();

                string qry = @"insert into Transection_Details(Regid, Order_no,Tracking_id,Bank_Ref_no,Order_status,Failure_message,Payment_mode,Card_or_bank_name,Status_code,Status_message,Curency,Amount,Date,Idate) values
                    ('" + Params["merchant_param1"].ToString() + "','" + Params["order_id"].ToString() + "','" + Params["tracking_id"].ToString() + "','" + Params["bank_ref_no"].ToString() + "','" + Params["order_status"].ToString() + "','" + Params["failure_message"].ToString() + "','" + Params["payment_mode"].ToString() + "','" + Params["card_name"].ToString() + "','" + Params["status_code"].ToString() + "','" + Params["status_message"].ToString() + "','" + Params["currency"].ToString() + "','" + Params["amount"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "')";
                My.exeSql(qry);

                find_std_details(Session["orderno"].ToString());
                btn_send_data();
            }
        }

        

        My mycode = new My();
        private void find_std_details(string order_id)
        {
            string query = "select Admission_no from Payment_transaction_process  where ordertrackingid='" + order_id + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count > 0)
            {
                hd_admission_no.Value = dt.Rows[0]["Admission_no"].ToString();
            }
        }

        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();
        private void btn_send_data()
        {
            if (ViewState["statuS"].ToString() == "Paid")
            {
                bool status = mypayment.save_final_payment(hd_admission_no.Value, Session["orderno"].ToString());
                if (status == true)
                {
                    Response.Redirect("../Payment_Success_Message.aspx?orderid=" + Session["orderno"].ToString() + "&Regid=" + hd_admission_no.Value + "&payFrom=" + ViewState["payFrom"].ToString(), false);
                }
                else
                {
                    Response.Redirect("../Payment_Error_Message.aspx?orderid=" + Session["orderno"].ToString() + "&Regid=" + hd_admission_no.Value + "&payFrom=" + ViewState["payFrom"].ToString(), false);
                }
            }
            else // failed/technila issues ocured
            {
                Response.Redirect("../Payment_Error_Message.aspx?orderid=" + Session["orderno"].ToString() + "&Regid=" + hd_admission_no.Value + "&payFrom=" + ViewState["payFrom"].ToString(), false);
            }
            System.Threading.Thread.Sleep(3000);
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        #endregion Online_payment_CCA
    }
}