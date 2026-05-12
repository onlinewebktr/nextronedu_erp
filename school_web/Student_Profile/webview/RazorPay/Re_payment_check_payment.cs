using Newtonsoft.Json;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace school_web.Student_Profile.webview.RazorPay
{
    public class Re_payment_check_payment
    {
        public static Dictionary<string, object> get_payment_status_Razorpay(string admission_no, string session_id, string order_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string rozorkey = My.MERCHANT_KEY();
            string aouthkey = My.autkeyrozorkey();

            var apiKey = aouthkey;//"cnpwX2xpdmVfeGcxS0VYOFJ0SXgya2Q6S0VydVpvdmhuR2hudG96eWI0czU4Mjgx";// YOUR_API_KEY";
            var orderId = order_id;// YOUR_ORDER_ID"; 
            var endpoint = $"https://api.razorpay.com/v1/orders/{orderId}/payments";
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = WebRequest.Create(endpoint);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Basic " + apiKey);

            string Status = "";
            string id = "";
            string error_description="";
            try
            {

                var response = (HttpWebResponse)request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream()).ReadToEnd();
                var cr = JsonConvert.DeserializeObject<dynamic>(reader);
                int count = cr.count;
                foreach (var ifo in cr["items"])
                {
                    order_id = ifo["order_id"];
                    Status = ifo["status"];
                    id = ifo["id"];
                    error_description = ifo["error_description"].ToString();
                    
                }
                if (Status == "captured")
                {
                    //payment done

                    dc["order_id"] = order_id;
                    dc["Status"] = "Success";
                    dc["Payment_order_id"] = id;
                    dc["error_description"] = "Success";

                }
                else if (Status == "Success")
                {
                    dc["order_id"] = order_id;
                    dc["Status"] = "Success";
                    dc["Payment_order_id"] = id;
                    // payment done
                }
                else if (Status == "failed")
                {
                    dc["order_id"] = order_id;
                    dc["Status"] = "failed";
                    dc["Payment_order_id"] = order_id;
                    dc["error_description"] = error_description.Replace("'", "");
                    // payment done
                }

                else
                {
                    dc["order_id"] = order_id;
                    dc["Status"] = "The user clicked the back button.";
                    dc["Payment_order_id"] = order_id;
                    dc["error_description"] = "The user clicked the back button.";
                }


            }
            catch (WebException ex)
            {
                dc["order_id"] = order_id;
                dc["Status"] = "The user clicked the back button.";
                dc["Payment_order_id"] = order_id;
                dc["error_description"] = "The user clicked the back button.";
                Console.WriteLine($"Failed to fetch payments. Error: {ex.Message}");

            }
            return dc;
        }
        public class Item_return_respons
        {
            public string id { get; set; }
            public string entity { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string status { get; set; }
            public string order_id { get; set; }
            public object invoice_id { get; set; }
            public bool international { get; set; }
            public string method { get; set; }
            public int amount_refunded { get; set; }
            public object refund_status { get; set; }
            public bool captured { get; set; }
            public string description { get; set; }
            public object card_id { get; set; }
            public object bank { get; set; }
            public object wallet { get; set; }
            public string vpa { get; set; }
            public string email { get; set; }
            public string contact { get; set; }
            public List<object> notes { get; set; }
            public int fee { get; set; }
            public object tax { get; set; }
            public string error_code { get; set; }
            public string error_description { get; set; }
            public string error_source { get; set; }
            public string error_step { get; set; }
            public string error_reason { get; set; }
            public AcquirerData acquirer_data { get; set; }
            public int created_at { get; set; }
        }
        public class AcquirerData
        {
            public object rrn { get; set; }
        }
        public class Response_details
        {
            public string entity { get; set; }
            public int count { get; set; }
            public List<Item_return_respons> items { get; set; }
        }



    }
}