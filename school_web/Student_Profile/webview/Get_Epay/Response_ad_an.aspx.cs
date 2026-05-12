using getepay_sdk;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.Get_Epay
{
    public partial class Response_ad_an : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Form.Keys.Count > 0)
                {
                    handleResponse();
                }
                else
                {
                    try
                    {
                        Response.Redirect("../Student_Annual_Payment.aspx?regid=" + Session["regid"].ToString(), false);
                    }
                    catch
                    {
                        Response.Redirect("../Student_Annual_Payment.aspx", false);
                    }

                    ViewState["statuS"] = "Failed";
                }
            }
        }

        private void handleResponse()
        {
            ViewState["statuS"] = "Failed";
            GetepayConfig config = new GetepayConfig();
            config.mid = My.Merchant_Key_Get_E_PAY(); //"108";
            config.terminalId = My.TerminalId_Get_E_PAY();// "Getepay.merchant61062@icici";
            config.key = My.Encryptionkey_Get_E_PAY();// "JoYPd+qso9s7T+Ebj8pi4Wl8i+AHLv+5UNJxA3JkDgY=";
            config.iv = My.IV_Get_E_PAY(); //"hlnuyA9b4YxDq6oJSZFl8g==";
            config.url = "https://pay1.getepay.in:8443/getepayPortal/pg/invoiceStatus";//local
            config.url = "https://portal.getepay.in:8443/getepayPortal/pg/invoiceStatus";// live

            string Encrypted_url = Request.Form["response"].ToString();
            string response = Getepay.decryptRequest(Request.Form["response"], config);

            string a = response.Replace(@"\", "");

            string final1 = a.TrimStart('"');
            string final2 = final1.TrimEnd('"');

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var wbResponce = serializer.Deserialize<Rootgetpay>(final2);
            string getepayTxnId = wbResponce.getepayTxnId;
            string status = wbResponce.txnStatus;
            string OrderNo = wbResponce.merchantOrderNo;
            string admission_no = wbResponce.udf1;
            string signature = wbResponce.discriminator;
            ViewState["ordertrackingid"] = OrderNo;
            ViewState["regid"] = wbResponce.udf4;
            
            if (status.ToString() == "SUCCESS")
            {
                ViewState["statuS"] = "Paid";
                My.exeSql("update Payment_transaction_process_Admission set razorpay_payment_id='" + getepayTxnId + "',status='Success',Plan_url='" + response + "',razorpay_signature='" + Encrypted_url + "'  where  ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "' and Admission_no='" + ViewState["regid"].ToString() + "'");

            }
            else
            {
                ViewState["statuS"] = "Failed";
               
                My.exeSql("update Payment_transaction_process_Admission set razorpay_payment_id='" + getepayTxnId + "',status='Failed',Plan_url='" + response + "',razorpay_signature='" + Encrypted_url + "'  where  ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "' and Admission_no='" + ViewState["regid"].ToString() + "'");


            }
            btn_submit();

        }
        public class Rootgetpay
        {
            public string getepayTxnId { get; set; }
            public string mid { get; set; }
            public string txnAmount { get; set; }
            public string txnStatus { get; set; }
            public string merchantOrderNo { get; set; }
            public string udf1 { get; set; }
            public string udf2 { get; set; }
            public string udf3 { get; set; }
            public string udf4 { get; set; }
            public string udf5 { get; set; }
            public string udf6 { get; set; }
            public string udf7 { get; set; }
            public string udf8 { get; set; }
            public string udf9 { get; set; }
            public string udf10 { get; set; }
            public string udf41 { get; set; }
            public string paymentMode { get; set; }
            public string discriminator { get; set; }
            public string message { get; set; }
            public string paymentStatus { get; set; }
        }

        Payment_update_after_onlinepayment_Admission mypayment = new Payment_update_after_onlinepayment_Admission();
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                btn_submit();


            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }

        private void btn_submit()
        {
            DataTable dt = mycode.FillData("Select  *  from Payment_transaction_process_Admission where ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "' ");
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("../Student_Annual_Payment.aspx?regid=" + Session["regid"].ToString(), false);

            }
            else
            {
                // synch_data();
                if (ViewState["statuS"].ToString() == "Paid")
                {
                    //paid process

                    bool status = mypayment.save_final_payment(dt.Rows[0]["Admission_no"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                    if (status == true)
                    {
                        Response.Redirect("../Payment_Success_Message_admission.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["payFrom"].ToString(), false);
                    }
                    else
                    {
                        Response.Redirect("../Payment_Error_Message_admission.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["payFrom"].ToString(), false);
                    }
                }
                else // failed/technila issues ocured
                {
                    Response.Redirect("../Payment_Error_Message_admission.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["payFrom"].ToString(), false);
                }
            }
            System.Threading.Thread.Sleep(3000);
            a1.Visible = false;
        }
    }
}