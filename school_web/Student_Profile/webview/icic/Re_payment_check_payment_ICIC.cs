using Newtonsoft.Json;
using RestSharp;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace school_web.Student_Profile.webview.icic
{
   
    public class Re_payment_check_payment_ICIC
    {
       
        internal static Dictionary<string, object> get_payment_status_ICIC_Pay(string regid, string sessionid, string merchantTxnNo)
        {
            My mycode = new My();
            Dictionary<string, object> dc2 = mycode.get_icic_gateway_details();
            string merchantIdVal = (String)dc2["ICIC_MID"];
            string aggregatorIDVal = (String)dc2["ICIC_Agg_ID"];
            string secretKey = (String)dc2["ICIC_Key"];
            string transactionType = "STATUS";
            string message = aggregatorIDVal + merchantIdVal + merchantTxnNo + merchantTxnNo + transactionType;
            string hash = My.GenerateHmac(message, secretKey);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // var client = new RestClient("https://pgpayuat.icicibank.com/tsp/pg/api/command");  Localhost
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
            Dictionary<string, object> dc = new Dictionary<string, object>();
            if (data.responseCode == "000" && data.txnStatus == "SUC")
            {
                dc["order_id"] = merchantTxnNo;
                dc["Status"] = "Success";
                dc["Payment_order_id"] = txnId;
                dc["error_description"] = "Success";
            }
            else
            {
                dc["order_id"] = merchantTxnNo;
                dc["Status"] = "Failed";
                dc["Payment_order_id"] = txnId;
                dc["error_description"] = status;
            }
            return dc;
        }
    }


}