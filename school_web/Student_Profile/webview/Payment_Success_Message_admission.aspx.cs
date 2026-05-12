using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Net;
using RestSharp;
using System.Web.Script.Serialization;

namespace school_web.Student_Profile.webview
{
    public partial class Payment_Success_Message_admission : System.Web.UI.Page
    {
        My mycode = new My();
        string rozorkey = My.MERCHANT_KEY();
        string aouthkey = My.autkeyrozorkey();
        string secret = My.MERCHANT_KEY_rozeror_secret();
        Payment_update_after_onlinepayment_Admission mypay = new Payment_update_after_onlinepayment_Admission();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["orderid"] != null)
                {
                    ViewState["orderid"] = Request.QueryString["orderid"].ToString();
                    ViewState["Regid"] = Request.QueryString["Regid"].ToString();


                    ViewState["payFrom"] = Request.QueryString["payFrom"].ToString();

                    if (ViewState["payFrom"].ToString() == "0")
                    {
                        btn_back.Visible = false;
                        Button1.Visible = true;
                    }
                    else
                    {
                        btn_back.Visible = true;
                        Button1.Visible = false;
                    }
                    Dictionary<string, object> dc1 = mypay.getstudentinfo(ViewState["Regid"].ToString(), ViewState["orderid"].ToString());

                    string Name = (String)dc1["Name"];
                    string Class_id = (String)dc1["Class_id"];
                    string Session_id = (String)dc1["Session_id"];
                    string Session = (String)dc1["Session"];
                    string Total_pay = (String)dc1["Total_pay"];
                    string Payment_type = (String)dc1["Payment_type"];
                    string category_id = (String)dc1["category_id"];
                    string sub_category_id = (String)dc1["sub_category_id"];
                    string Date = (String)dc1["Date"];
                    string pay_idate1 = (String)dc1["Idate"];
                    string hostaltaken = (String)dc1["hosteltaken"];
                    int pay_idate = Convert.ToInt32(pay_idate1);
                    string Branch_id = (String)dc1["Branch_id"];
                    string Section = (String)dc1["Section"];
                    string razorpay_payment_id = (String)dc1["razorpay_payment_id"];
                    ViewState["Getwey_Type"] = mycode.get_payment_Getwey_Type_monthly(Class_id);

                    lbl_Order_id.Text = "System Order Id:- " + ViewState["orderid"].ToString();
                    lbl_ampunt.Text = "Amount :- " + My.toDouble(Total_pay).ToString("0.00");
                    lbl_paymentdate.Text = "Date :-" + Date;

                    if (ViewState["Getwey_Type"].ToString() == "Razorpay")// if razor Pay getwey
                    {
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
                    else if (ViewState["Getwey_Type"].ToString() == "Worldline")
                    {
                        lbl_status.Text = "Status:-captured";
                        lbl_orderidrozor.Text = "Order Id:-" + ViewState["orderid"].ToString();
                    }
                    else if (ViewState["Getwey_Type"].ToString() == "EGPay")// if razor Pay getwey
                    {
                        lbl_status.Text = "Status:-captured";
                        lbl_orderidrozor.Text = "Order Id:-" + ViewState["orderid"].ToString();
                    }
                    else
                    {
                        lbl_status.Text = "Status:-captured";
                        lbl_orderidrozor.Text = "Order Id:-" + razorpay_payment_id;
                    }
                }
            }

        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            try
            {
                Session["User"] = ViewState["Regid"].ToString();
                string url = "";
                if (ViewState["payFrom"].ToString() == "1")
                {
                    url = My.URL() + "Student_Profile/webview/Student_Annual_Payment.aspx?regid=" + ViewState["Regid"].ToString();
                    Response.Redirect(url, false);
                }
                else
                {


                    url = My.URL() + "Student_Profile/student-Annual-payment.aspx";


                    Response.Redirect(url, false);
                }
            }
            catch
            {

            }
          
        }
    }
}