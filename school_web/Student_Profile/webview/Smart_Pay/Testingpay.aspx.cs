using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.Smart_Pay
{
    public partial class Testingpay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            //var client = new RestClient("https://smartgatewayuat.hdfcbank.com/session");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AlwaysMultipartFormData = true;
            //request.AddParameter("Authorization", "Basic Q0M4QzA5NDQ5ODI0OTJGOTlBNTBEMjI5MjBDNDZGOg==");
            ////request.AddParameter("password", "");
            //request.AddHeader("x-merchantid", "SG946");
            //request.AddHeader("x-customerid", "TEST12345455");
            //request.AddHeader("Content-Type", "application/json");
            //request.AddJsonBody(new { order_id = "TEST123453102", amount = "1.0", customer_id = "testing-customer-one", customer_email = "test@mail.com", customer_phone = "9876543210", payment_page_client_id = "hdfcmaster", action = "paymentPage", return_url = "https://shop.merchant.com", description= "Complete your payment", first_name= "John", last_name= "wick" }); ;
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //serializer.MaxJsonLength = Int32.MaxValue;
            //var wbResponce = serializer.Deserialize<waybillDT>(response.Content);




            //            var options = new RestClientOptions("")
            //            {
            //                MaxTimeout = -1,
            //            };
            //            var client = new RestClient(options);
            //            var request = new RestRequest("https://smartgatewayuat.hdfcbank.com/session", Method.Post);
            //            request.AddHeader("x-merchantid", "SG946");
            //            request.AddHeader("x-customerid", "TEST12345455");
            //            request.AddHeader("Content-Type", "application/json");
            //            request.AddHeader("Authorization", "Basic Q0M4QzA5NDQ5ODI0OTJGOTlBNTBEMjI5MjBDNDZGOg==");
            //            var body = @"{
            //" + "\n" +
            //            @"  ""order_id"": ""TEST123453109"",
            //" + "\n" +
            //            @"  ""amount"": ""1.0"",
            //" + "\n" +
            //            @"  ""customer_id"": ""testing-customer-one"",
            //" + "\n" +
            //            @"  ""customer_email"": ""test@mail.com"",
            //" + "\n" +
            //            @"  ""customer_phone"": ""9876543210"",
            //" + "\n" +
            //            @"  ""payment_page_client_id"": ""hdfcmaster"",
            //" + "\n" +
            //            @"  ""action"": ""paymentPage"",
            //" + "\n" +
            //            @"  ""return_url"": ""https://shop.merchant.com"",
            //" + "\n" +
            //            @"  ""description"": ""Complete your payment"",
            //" + "\n" +
            //            @"  ""first_name"": ""John"",
            //" + "\n" +
            //            @"  ""last_name"": ""wick""
            //" + "\n" +
            //            @"}";
            //            request.AddStringBody(body, DataFormat.Json);
            //            RestResponse response = await client.ExecuteAsync(request);
            //            Console.WriteLine(response.Content);


            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://smartgatewayuat.hdfcbank.com/session");
            request.Headers.Add("x-merchantid", "SG946");
            request.Headers.Add("x-customerid", "TEST12345455");
            request.Headers.Add("Authorization", "Basic Q0M4QzA5NDQ5ODI0OTJGOTlBNTBEMjI5MjBDNDZGOg==");

            //var bodydata = new { order_id = "TEST123453102", amount = "1.0", customer_id = "testing-customer-one", customer_email = "test@mail.com", customer_phone = "9876543210", payment_page_client_id = "hdfcmaster", action = "paymentPage", return_url = "https://shop.merchant.com", description = "Complete your payment", first_name = "John", last_name = "wick" };

            //var contentdata = (new JavaScriptSerializer()).Serialize(bodydata);

            var contentdata = new StringContent("{\r\n    \"order_id\": \"TEST123453102\",\r\n    \"amount\": \"1.0\",\r\n    \"customer_id\": \"testing-customer-one\",\r\n    \"customer_email\": \"test@mail.com\",\r\n    \"customer_phone\": \"9876543210\",\r\n    \"payment_page_client_id\": \"hdfcmaster\",\r\n    \"action\": \"paymentPage\",\r\n    \"return_url\": \"https://shop.merchant.com\",\r\n    \"description\": \"Complete your payment\",\r\n    \"first_name\": \"John\",\r\n    \"last_name\": \"wick\"\r\n}", null, "application/json");

            request.Content = contentdata; //content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var abc = await response.Content.ReadAsStringAsync();
            
            var myDeserializedClass = JsonConvert.DeserializeObject<Root>(abc);
            string payorder = myDeserializedClass.id;////gateway order id;
            string orderId = myDeserializedClass.order_id;//system id;
            string payment_links = myDeserializedClass.payment_links.web;//system id;
        }

        public class Payload
        {
            public string action { get; set; }
            public string amount { get; set; }
            public string clientAuthToken { get; set; }
            public DateTime clientAuthTokenExpiry { get; set; }
            public string clientId { get; set; }
            public bool collectAvsInfo { get; set; }
            public string currency { get; set; }
            public string customerEmail { get; set; }
            public string customerId { get; set; }
            public string customerPhone { get; set; }
            public string description { get; set; }
            public string displayBusinessAs { get; set; }
            public string environment { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string merchantId { get; set; }
            public string orderId { get; set; }
            public string returnUrl { get; set; }
            public string service { get; set; }
        }

        public class PaymentLinks
        {
            public string web { get; set; }
            public DateTime expiry { get; set; }
        }

        public class Root
        {
            public string status { get; set; }
            public string id { get; set; }
            public string order_id { get; set; }
            public PaymentLinks payment_links { get; set; }
            public SdkPayload sdk_payload { get; set; }
            public DateTime order_expiry { get; set; }
        }

        public class SdkPayload
        {
            public string requestId { get; set; }
            public string service { get; set; }
            public Payload payload { get; set; }
            public DateTime expiry { get; set; }
        }
       
    }
}