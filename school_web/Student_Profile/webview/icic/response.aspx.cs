using Newtonsoft.Json;
using RestSharp;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.icic
{
    public partial class response : System.Web.UI.Page
    {
        My mycode = new My();
        protected async void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, object> dc2 = mycode.get_icic_gateway_details();
            string merchantIdVal = (String)dc2["ICIC_MID"];
            string aggregatorIDVal = (String)dc2["ICIC_Agg_ID"];
            string secretKey = (String)dc2["ICIC_Key"];
            // ICICI response form data
            NameValueCollection form = Request.Form;
            string merchantTxnNo = form["merchantTxnNo"];
            string amount = form["amount"];
            string txnStatus = form["respDescription"];//status
            string secureHash = form["secureHash"];
            ViewState["ordertrackingid"] = merchantTxnNo;
            // optional params
            string addlParam1 = form["addlParam1"];// admission no
            string transactionType= "STATUS";
            string message = aggregatorIDVal + merchantIdVal + merchantTxnNo + merchantTxnNo + transactionType;
            string hash = My.GenerateHmac(message, secretKey);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // var client = new RestClient("https://pgpayuat.icicibank.com/tsp/pg/api/command");
            var client = new RestClient("https://pgpay.icicibank.com/pg/api/command");
            var request = new RestRequest(Method.POST);

            // Header
            request.AddHeader("Content-Type", "application/json");

            // Request Object
            var body = new
            {
                aggregatorID = aggregatorIDVal,
                merchantId = merchantIdVal,
                merchantTxnNo = merchantTxnNo,
                originalTxnNo = merchantTxnNo,
                transactionType = "STATUS",
                secureHash = hash
            };

            // Convert to JSON
            string jsonBody = JsonConvert.SerializeObject(body);

            // Add JSON Body
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

            // Execute
            IRestResponse response = client.Execute(request);

            // Debug (IMPORTANT)
            string result = response.Content;
            PaymentResponse data = JsonConvert.DeserializeObject<PaymentResponse>(result);
            string status = data.txnStatus;          // SUC
            string amount_final = data.amount;             // 110.00
            string txnId = data.txnID;               // 7700223989293
            string responseCode = data.responseCode; // 000
            string remarks = data.txnRespDescription;// Transaction successful
            if (data.responseCode == "000" && data.txnStatus == "SUC")
            {
                // SUCCESS
                ViewState["statuS"] = "Paid";
                My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + txnId + "',status='Success',Remarks='Received successful confirmation in real time for the transaction.'  where  ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "'");

                btn_submit();
            }
            else
            {
                ViewState["statuS"] = "Unpaid";
                // FAILED
                My.exeSql("update Payment_transaction_process set  razorpay_payment_id='"+ txnId + "',status='Failed',Remarks='" + remarks + "'  where  ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "'");
                /* Response.Redirect("../Student_Monthly_Payment.aspx?regid=" + ViewState["regid"].ToString(), false);*/
                btn_submit();

            }

        }
        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();


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
            DataTable dt = mycode.FillData("Select  *  from Payment_transaction_process where ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "' ");
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("../Student_Monthly_Payment.aspx?regid=" + ViewState["regid"].ToString(), false);
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
                        Response.Redirect("../Payment_Success_Message.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["Pay_from"].ToString(), false);
                    }
                    else
                    {
                        Response.Redirect("../Payment_Error_Message.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["Pay_from"].ToString(), false);
                    }
                }
                else // failed/technila issues ocured
                {
                    Response.Redirect("../Payment_Error_Message.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["Pay_from"].ToString(), false);
                }
            }
            System.Threading.Thread.Sleep(3000);
            a1.Visible = false;
        }

    }
    public class PaymentResponse
    {
        public string txnRespDescription { get; set; }
        public string secureHash { get; set; }
        public string amount { get; set; }
        public string authCode { get; set; }
        public string txnAuthID { get; set; }
        public string txnResponseCode { get; set; }
        public string customerEmailID { get; set; }
        public string paymentMode { get; set; }
        public string respDescription { get; set; }
        public string aggregatorID { get; set; }
        public string TransmissionDateTime { get; set; }
        public string oth_charge { get; set; }
        public string customerMobileNo { get; set; }
        public string responseCode { get; set; }
        public string transactionType { get; set; }
        public string txnStatus { get; set; }
        public string paymentSubInstType { get; set; }
        public string merchantId { get; set; }
        public string merchantTxnNo { get; set; }
        public string paymentDateTime { get; set; }
        public string txnID { get; set; }
    }

}