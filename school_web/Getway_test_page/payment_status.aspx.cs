using RestSharp;
using school_web.AppCode;
using school_web.Student_Profile.webview;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Getway_test_page
{
    public partial class payment_status : System.Web.UI.Page
    {
        string rozorkey = My.MERCHANT_KEY();
        string aouthkey = My.autkeyrozorkey();
        string secret = My.MERCHANT_KEY_rozeror_secret();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        My mycode = new My();
        protected void Button1_Click(object sender, EventArgs e)
        {
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //var client = new RestClient("https://api.razorpay.com/v1/payments/" + ViewState["razorpay_payment_id"].ToString());
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Username", rozorkey);
            //request.AddHeader("Password", secret);
            //request.AddHeader("Authorization", "Basic " + aouthkey);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //serializer.MaxJsonLength = Int32.MaxValue;
            //var wbResponce = serializer.Deserialize<PayinFos.PayinFossigle>(response.Content);


            //RazorpayClient client = new RazorpayClient("[YOUR_KEY_ID]", "[YOUR_KEY_SECRET]");
            //Payment payment = client.Payment.Fetch(paymentId);

            //Dictionary <`string`, object> options = new Dictionary<`string`, object>();
            //options.Add("amount", "<amount>");
            //options.Add("currency", "<currency>");
            //Payment paymentCaptured = payment.Capture(options);


            //
            ViewState["razorpay_orderid"] = "order_M6YEVxlBBIyQVy";
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ////var client = new RestClient("https://api.razorpay.com/v1/payments/" + ViewState["razorpay_payment_id"].ToString());
            //var client = new RestClient("https://api.razorpay.com/v1/orders/" + ViewState["razorpay_orderid"].ToString() + "/");
            //// https://api.razorpay.com/v1/orders/order_M6YEVxlBBIyQVy/payments
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Username", rozorkey);
            //request.AddHeader("Password", secret);
            //request.AddHeader("Authorization", "Basic " + aouthkey);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //serializer.MaxJsonLength = Int32.MaxValue;
            //var wbResponce = serializer.Deserialize<PayinFos.PayinFossigle>(response.Content);



            var apiKey = aouthkey;//"cnpwX2xpdmVfeGcxS0VYOFJ0SXgya2Q6S0VydVpvdmhuR2hudG96eWI0czU4Mjgx";// YOUR_API_KEY";
            var orderId = ViewState["razorpay_orderid"].ToString();// YOUR_ORDER_ID"; 
            var endpoint = $"https://api.razorpay.com/v1/orders/{orderId}/payments";
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = WebRequest.Create(endpoint);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Basic " + apiKey);

            try
            {
                // Get the response from the request
                var response = (HttpWebResponse)request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var content = reader.ReadToEnd();

                Console.WriteLine(content);
 

            }
            catch (WebException ex)
            {
                Console.WriteLine($"Failed to fetch payments. Error: {ex.Message}");

            }

        }
    }
}