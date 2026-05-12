using RestSharp;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.RazorPay
{
    public partial class PayProcess_blank_callback : System.Web.UI.Page
    {
        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();
        My mycode = new My();
        string rozorkey = My.MERCHANT_KEY();
        string aouthkey = My.autkeyrozorkey();
        string secret = My.MERCHANT_KEY_rozeror_secret();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string razorpay_payment_id = "";
                string razorpay_order_id = "";
                string razorpay_signature = "";
                string errordescription = "";
                if (Request.Form["razorpay_order_id"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowProgress();", true);
                    string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load", script, true);
                    try
                    {
    
                        ViewState["razorpay_payment_id"] = Request.Form["razorpay_payment_id"].ToString();
                        ViewState["razorpay_order_id"] = Request.Form["razorpay_order_id"].ToString();
                        ViewState["razorpay_signature"] = Request.Form["razorpay_signature"].ToString();
                        string generated_signature = mycode.hmac_sha256(ViewState["razorpay_order_id"].ToString() + "|" + ViewState["razorpay_payment_id"].ToString(), My.MERCHANT_KEY_rozeror_secret());

                        if (generated_signature == ViewState["razorpay_signature"].ToString())
                        {

                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            var client = new RestClient("https://api.razorpay.com/v1/payments/" + ViewState["razorpay_payment_id"].ToString());
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
                            try
                            {
                                errordescription = wbResponce.error_description.ToString();
                            }
                            catch
                            {
                                errordescription = "";
                            }
                            double finalamount = 0.00;
                            try

                            {
                                  finalamount = My.toDouble(Amount)/100;
                            }
                            catch
                            {

                            }
                            

                            if (status.ToLower() == "captured")
                            {
                                ViewState["statuS"] = "Paid";
                                My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + ViewState["razorpay_payment_id"].ToString() + "', razorpay_signature='" + ViewState["razorpay_signature"].ToString() + "',status='" + ViewState["statuS"].ToString() + "',Remarks='"+ status.ToLower() + "',Total_pay='"+ finalamount.ToString() + "'  where  razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'");
                                update_process();
                            }

                            else if (status.ToUpper() == "AUTHORIZED")
                            {
                                ViewState["statuS"] = "Paid";

                                My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + ViewState["razorpay_payment_id"].ToString() + "',razorpay_signature='" + ViewState["razorpay_signature"].ToString() + "',status='" + ViewState["statuS"].ToString() + "',Remarks='" + status.ToLower() + "',Total_pay='" + finalamount.ToString() + "' where  razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'");
                                update_process();
                            }

                            else
                            {
                                ViewState["statuS"] = "Failed";
                                My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + ViewState["razorpay_payment_id"].ToString() + "',razorpay_signature='" + ViewState["razorpay_signature"].ToString() + "',status='" + status + "',Remarks='" + errordescription.Replace("'", "''") + "' where  razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'"); 

                                try
                                {
                                    My.Insert("failure_payment", new
                                    {

                                        razorpay_order_id = razorpay_order_id,
                                        razorpay_payment_id = razorpay_payment_id,
                                        razorpay_signature = razorpay_signature,
                                        date_time = My.getdate1()
                                    });
                                }
                               catch
                                {

                                }
                               
                                update_process();

                            }
                        }
                        else
                        {

                            My.exeSql("update Payment_transaction_process set  status='The user clicked the back button.',Remarks='The user clicked the back button.' where  razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'");

                            try
                            {
                                razorpay_payment_id = Request.Form["razorpay_payment_id"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                razorpay_order_id = Request.Form["razorpay_order_id"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                razorpay_signature = Request.Form["razorpay_signature"].ToString();
                            }
                            catch
                            {

                            }

                            try
                            {
                                My.Insert("failure_payment", new
                                {

                                    razorpay_order_id = razorpay_order_id,
                                    razorpay_payment_id = razorpay_payment_id,
                                    razorpay_signature = razorpay_signature,
                                    date_time = My.getdate1()
                                });
                            }
                            catch
                            {

                            }
                            ViewState["statuS"] = "Failed";
                            update_process();
                        }
                        //

                    }
                    catch
                    {
                        try
                        {
                            My.exeSql("update Payment_transaction_process set  status='The user clicked the back button.',Remarks='The user clicked the back button.' where  razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'");
                        }
                        catch
                        {

                        }
                       

                        try
                        {
                            razorpay_payment_id = Request.Form["razorpay_payment_id"].ToString();
                        }
                        catch
                        {

                        }
                        try
                        {
                            razorpay_order_id = Request.Form["razorpay_order_id"].ToString();
                        }
                        catch
                        {

                        }
                        try
                        {
                            razorpay_signature = Request.Form["razorpay_signature"].ToString();
                        }
                        catch
                        {

                        }
                        try
                        {
                            My.Insert("failure_payment", new
                            {

                                razorpay_order_id = razorpay_order_id,
                                razorpay_payment_id = razorpay_payment_id,
                                razorpay_signature = razorpay_signature,
                                date_time = My.getdate1()
                            });
                        }
                        catch
                        {

                        }
                        //Response.Redirect("../Student_Monthly_Payment.aspx?regid=" + Session["regid"].ToString(), false);
                        //Response.Redirect("../../student-monthly-payment.aspx", false);
                        Response.Redirect("../Payment_Error_Message.aspx?orderid=na", false);
                    }

                }
                else
                {
                    try
                    {
                        razorpay_payment_id = Request.Form["razorpay_payment_id"].ToString();
                    }
                    catch
                    {

                    }
                    try
                    {
                        razorpay_order_id = Request.Form["razorpay_order_id"].ToString();
                    }
                    catch
                    {

                    }
                    try
                    {
                        razorpay_signature = Request.Form["razorpay_signature"].ToString();
                    }
                    catch
                    {

                    }
                    try
                    {
                        My.Insert("failure_payment", new
                        {

                            razorpay_order_id = razorpay_order_id,
                            razorpay_payment_id = razorpay_payment_id,
                            razorpay_signature = razorpay_signature,
                            date_time = My.getdate1()
                        });
                    }
                    catch
                    {

                    }

                    Response.Redirect("../Payment_Error_Message.aspx?orderid=na", false);
                   

                }

            }
        }



        public void update_process()
        {
            try
            {
                DataTable dt = mycode.FillData("Select * from Payment_transaction_process where razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "' ");
                if (dt.Rows.Count == 0)
                {
                    Response.Redirect("../Student_Monthly_Payment.aspx?regid=" + Session["regid"].ToString(), false);
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

                a1.Visible = false;

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
                Response.Redirect("../Student_Monthly_Payment.aspx?regid=" + Session["regid"].ToString(), false);


            }
        }
    }
}