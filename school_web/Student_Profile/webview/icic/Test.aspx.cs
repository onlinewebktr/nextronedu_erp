using Newtonsoft.Json;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.icic
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        My mycode = new My();
        protected async  void Button1_Click(object sender, EventArgs e)
        {
            await buttnclickdataAsync();   // ✔️ await lagao

            //await CallICICIStatusAPI();
        }

        private async Task CallICICIStatusAPI()
        {
            var url = "https://pgpayuat.icicibank.com/tsp/pg/api/command";

            // JSON body (same as Postman)
            var requestData = new
            {
                aggregatorID = "A100000000007164",
                merchantId = "100000000007164",
                merchantTxnNo = "26032405034937757",
                originalTxnNo = "26032405034937757",
                secureHash = "2f3d05b3bd6396ba54ffcc4b03cb37675c74efe6038075c88e7f604aeb9b23ac",
                transactionType = "STATUS"
            };

            string json = JsonConvert.SerializeObject(requestData);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(url, content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    // Show request + response
                    Label1.Text = "<b>Request:</b><br><pre>" + json + "</pre>";
                    Label1.Text += "<br><br><b>Response:</b><br><pre>" + responseString + "</pre>";
                }
                catch (Exception ex)
                {
                    Label1.Text = "Error: " + ex.Message;
                }
            }
        }

        private async Task buttnclickdataAsync()
        {
            Dictionary<string, object> dc2 = mycode.get_icic_gateway_details();
            string merchantIdVal = (String)dc2["ICIC_MID"];
            string aggregatorIDVal = (String)dc2["ICIC_Agg_ID"];
            string secretKey = (String)dc2["ICIC_Key"];

            String merchantTxnNo = "26032405034937757";
            string transactionType = "STATUS";
            string message = aggregatorIDVal+merchantIdVal + merchantTxnNo + merchantTxnNo + transactionType;


            string hash = My.GenerateHmac(message, secretKey);

            var url = "https://pgpayuat.icicibank.com/tsp/pg/api/command";
            var requestData = new Paymentstatus
            {
                aggregatorID = aggregatorIDVal,
                merchantId = merchantIdVal,
                merchantTxnNo = merchantTxnNo,
                originalTxnNo = merchantTxnNo,
                transactionType = "STATUS",
                secureHash = hash
            };
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(requestData);
                //var content = new StringContent(json, Encoding.UTF8, "application/x-www-form-urlencoded");
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseString);

                Label1.Text = responseString;
               // dynamic result = JsonConvert.DeserializeObject(responseString);


            }
        }

       
    }
}