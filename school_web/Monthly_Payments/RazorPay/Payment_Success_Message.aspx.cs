using RestSharp;
using school_web.AppCode;
using school_web.Student_Profile.webview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Monthly_Payments.RazorPay
{
    public partial class Payment_Success_Message : System.Web.UI.Page
    {
        My mycode = new My();
        string rozorkey = My.MERCHANT_KEY();
        string aouthkey = My.autkeyrozorkey();
        string secret = My.MERCHANT_KEY_rozeror_secret();
        Payment_update_after_onlinepayment mypay = new Payment_update_after_onlinepayment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["orderid"] != null)
                {
                    ViewState["orderid"] = Request.QueryString["orderid"].ToString();
                    ViewState["Regid"] = Request.QueryString["Regid"].ToString();

                    Dictionary<string, object> dc1 = mypay.getstudentinfo(ViewState["Regid"].ToString(), ViewState["orderid"].ToString());
                    string Name = (String)dc1["Name"];
                    string Class_id = (String)dc1["Class_id"];
                    ViewState["classid"] = Class_id;
                    string Session_id = (String)dc1["Session_id"];
                    ViewState["sessionid"] = Session_id;
                    string Session = (String)dc1["Session"];
                    string month = (String)dc1["month"];
                    string Total_pay = (String)dc1["Total_pay"];
                    string parameter = (String)dc1["parameter"];
                    string parameter_id = (String)dc1["parameter_id"];
                    string Class_name = (String)dc1["Class_name"];
                    string hosteltaken = (String)dc1["hosteltaken"];

                    string hostel_id = (String)dc1["hostel_id"];
                    string day_boarding = (String)dc1["day_boarding"];
                    string day_boarding_lunch = (String)dc1["day_boarding_lunch"];
                    string category_id = (String)dc1["category_id"];
                    string sub_category_id = (String)dc1["sub_category_id"];
                    string transportportation_id = (String)dc1["transportportation_id"];
                    string group_id = (String)dc1["group_id"];
                    string Section = (String)dc1["Section"];
                    string Date = (String)dc1["Date"];
                    string pay_idate1 = (String)dc1["Idate"];
                    string totaldiscount = (String)dc1["totaldiscount"];
                    string totallatefine = (String)dc1["totallatefine"];
                    string razorpay_payment_id = (String)dc1["razorpay_payment_id"];
                    string slipno = (String)dc1["slipno"];

                    ViewState["slipno"] = slipno;

                    lbl_Order_id.Text = "System Order Id:- " + ViewState["orderid"].ToString();
                    lbl_ampunt.Text = "Amount :- " + My.toDouble(Total_pay).ToString("0.00");
                    lbl_paymentdate.Text = "Date :-" + Date;





                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var client = new RestClient("https://api.razorpay.com/v1/payments/" + razorpay_payment_id);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Username", rozorkey);
                    request.AddHeader("Password", secret);
                    request.AddHeader("Authorization", "Basic " + aouthkey);
                    IRestResponse response = client.Execute(request);
                    Console.WriteLine(response.Content);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = Int32.MaxValue;
                    var wbResponce = serializer.Deserialize<PayinFos.PayinFossigle>(response.Content);
                    string Amount = wbResponce.amount;
                    string status = wbResponce.status;
                    string razorpayid = wbResponce.id;
                    string order_id = wbResponce.order_id;

                    if (status == "captured")
                    {
                        lbl_status.Text = "Status:-captured";
                        lbl_orderidrozor.Text = "Order Id:-" + order_id;
                    }




                }
            }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("../monthly-slip.aspx?admissionno=" + ViewState["Regid"].ToString() + "&sessionid=" + ViewState["sessionid"].ToString() + "&classid=" + ViewState["classid"].ToString() + "&Slip_no=" + ViewState["slipno"].ToString(), false);
            
        }
    }
}