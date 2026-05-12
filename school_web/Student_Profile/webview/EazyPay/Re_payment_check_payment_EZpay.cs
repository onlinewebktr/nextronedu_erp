using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace school_web.Student_Profile.webview.EazyPay
{
    public class Re_payment_check_payment_EZpay
    {
        internal static Dictionary<string, object> get_payment_status_EZ_Pay(string admission_no, string session_id, string order_id)
        {
            string error_description = "";
            string status = "The user clicked the back button.";
            string ezpaytranid = "";
            Dictionary<string, object> dc = new Dictionary<string, object>();

            string merchent_id = My.MERCHANT_KEY_EZ_PAY();//"354414"; //;
            var orderId = order_id; //"22111905113341324";// YOUR_ORDER_ID"; 
            var endpoint = $"https://eazypay.icicibank.com/EazyPGVerify?ezpaytranid=&amount=&paymentmode=&merchantid=" + merchent_id + "&trandate=&pgreferenceno=" + orderId;

            //var endpoint = $"https://eazypayuat.icicibank.com/EazyPGVerify?ezpaytranid=&amount=&paymentmode=&merchantid=" + merchent_id + "&trandate=&pgreferenceno=" + orderId;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = WebRequest.Create(endpoint);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            var ss = reader.ReadToEnd();
            var dictionary = new Dictionary<string, string>();

            // Split the input string by "&" to get individual key-value pairs
            string[] keyValuePairs = ss.Split('&');

            foreach (string pair in keyValuePairs)
            {
                // Split each key-value pair by "=" to separate keys from values
                string[] parts = pair.Split('=');

                // Ensure the key-value pair is valid
                if (parts.Length == 2)
                {
                    string key = parts[0];
                    string value = parts[1];

                    // Add the key-value pair to the dictionary
                    dictionary.Add(key, value);
                }
            }
              status = dictionary["status"];
              ezpaytranid = dictionary["ezpaytranid"];


            if (status.ToUpper() == "SUCCESS")
            {
                dc["order_id"] = order_id;
                dc["Status"] = "Success";
                dc["Payment_order_id"] = ezpaytranid;
                dc["error_description"] = "Success";
              

                // payment done
            }
            else if (status.ToUpper() == "RIP")
            {
                dc["order_id"] = order_id;
                dc["Status"] = "Success";
                dc["Payment_order_id"] = ezpaytranid;
                dc["error_description"] = "Success";
                // payment done
            }
            else if (status.ToUpper() == "SIP")
            {
                dc["order_id"] = order_id;
                dc["Status"] = "Success";
                dc["Payment_order_id"] = ezpaytranid;
                dc["error_description"] = "Success";
                // payment done
            }
            else
            {
                dc["order_id"] = orderId;
                dc["Status"] = status;
                dc["Payment_order_id"] = ezpaytranid;
                dc["error_description"] = status;
            }
            return dc;
        }
    }
}