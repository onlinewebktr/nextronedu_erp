using getepay_sdk;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Payment_admission.Get_Epay
{
    public partial class Response : System.Web.UI.Page
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

                    Response.Redirect("../Student_Online_Registration_Apply.aspx", false);


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
            
            config.url = "https://pay1.getepay.in:8443/getepayPortal/pg/generateInvoice";

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

            string signature = wbResponce.discriminator;
            ViewState["ordertrackingid"] = OrderNo;
            ViewState["regid"] = wbResponce.udf4;
            if (status == "FAILED")
            {
                ViewState["statuS"] = "Failed";
                My.exeSql("update Online_Admission set razorpay_payment_id='" + getepayTxnId + "',status='Failed' where   Order_id='" + ViewState["ordertrackingid"] + "' and Registration_id='"+ ViewState["regid"].ToString() + "'");
                Response.Redirect("../Student_Online_Registration_Apply.aspx", false);
            }
            else
            {

                ViewState["statuS"] = "Paid";
                My.exeSql("update Online_Admission set razorpay_payment_id='" + getepayTxnId + "',Payment_Status='Paid',Steps_done='10'  where   Order_id='" + ViewState["ordertrackingid"] + "'  and Registration_id='" + ViewState["regid"].ToString() + "'");

                
                Response.Redirect("../../print/Print_Page.aspx?PaymentSliP=" + ViewState["regid"].ToString(), false);
            }
             

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

        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();
        

        private void btn_submit()
        {
             
            System.Threading.Thread.Sleep(3000);
            a1.Visible = false;
        }
    }
}