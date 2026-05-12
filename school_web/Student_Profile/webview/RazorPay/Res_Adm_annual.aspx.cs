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
    public partial class Res_Adm_annual : System.Web.UI.Page
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

                if (Request.Form["razorpay_order_id"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowProgress();", true);
                    string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load", script, true);
                    try
                    {
                        //  ViewState["orderid"] = Request.QueryString["orderid"].ToString();
                        //  ViewState["Regid"] = Request.QueryString["Regid"].ToString();


                        ViewState["razorpay_payment_id"] = Request.Form["razorpay_payment_id"].ToString();
                        ViewState["razorpay_order_id"] = Request.Form["razorpay_order_id"].ToString();
                        ViewState["razorpay_signature"] = Request.Form["razorpay_signature"].ToString();

                        //  ViewState["statuS"] = Request.QueryString["statuS"].ToString();

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


                            if (status == "captured")
                            {
                                ViewState["statuS"] = "Paid"; 
                                My.exeSql("update Payment_transaction_process_Admission set razorpay_payment_id='" + ViewState["razorpay_payment_id"].ToString() + "', razorpay_signature='" + ViewState["razorpay_signature"].ToString() + "',status='" + ViewState["statuS"].ToString() + "'  where  razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'");
                            }

                            else if (status.ToUpper() == "AUTHORIZED")
                            {
                                ViewState["statuS"] = "Paid"; 
                                My.exeSql("update Payment_transaction_process_Admission set razorpay_payment_id='" + ViewState["razorpay_payment_id"].ToString() + "',razorpay_signature='" + ViewState["razorpay_signature"].ToString() + "',status='" + ViewState["statuS"].ToString() + "' where  razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'");
                            }

                            else
                            {
                                ViewState["statuS"] = "Failed";

                            }
                        }
                        else
                        {
                            ViewState["statuS"] = "Failed";
                        }


                    }
                    catch
                    {
                        Response.Redirect("../Student_Annual_Payment.aspx?regid=" + Session["regid"].ToString(), false);

                    }

                }
                else
                {
                    try
                    {
                        Response.Redirect("../Student_Annual_Payment.aspx?regid=" + Session["regid"].ToString(), false);
                    }
                    catch
                    {
                        Response.Redirect("../Student_Annual_Payment.aspx", false);
                    }

                    //DataTable dt = mycode.FillData("Select  *  from Payment_transaction_process_Admission where razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "' ");
                    //if (dt.Rows.Count == 0)
                    //{
                    //    Session["regid"] = "0";
                    //}
                    //else

                    //{
                    //    Session["regid"] = dt.Rows[0]["Admission_no"].ToString(); ;
                    //}
                    //ViewState["statuS"] = "Failed";
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = mycode.FillData("Select * from Payment_transaction_process_Admission where razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "' ");
                if (dt.Rows.Count == 0)
                {
                    Response.Redirect("../Student_Annual_Payment.aspx?regid=" + Session["regid"].ToString(), false);

                }
                else
                {
                    // synch_data();
                    if (ViewState["statuS"].ToString() == "Paid")
                    {
                        //paid process

                        Payment_update_after_onlinepayment_Admission mypayment = new Payment_update_after_onlinepayment_Admission();
                        bool status = mypayment.save_final_payment(dt.Rows[0]["Admission_no"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                        if (status == true)
                        {
                            Response.Redirect("../Payment_Success_Message_admission.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["payFrom"].ToString(), false);
                        }
                        else
                        {
                            Response.Redirect("../Payment_Error_Message_admission.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["payFrom"].ToString(), false);
                        }
                    }
                    else // failed/technila issues ocured
                    {
                        Response.Redirect("../Payment_Error_Message_admission.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["payFrom"].ToString(), false);
                    }
                }
                System.Threading.Thread.Sleep(3000);
                a1.Visible = false;

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }
    }
}