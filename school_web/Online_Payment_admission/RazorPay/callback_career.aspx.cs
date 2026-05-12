using RestSharp;
using school_web.AppCode;
using school_web.Student_Profile.webview;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Payment_admission.RazorPay
{
    public partial class callback_career : System.Web.UI.Page
    {
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

                                update_data_base();
                            }

                            else if (status.ToUpper() == "AUTHORIZED")
                            {
                                ViewState["statuS"] = "Paid";
                                update_data_base();
                            }

                            else
                            {
                                ViewState["statuS"] = "Failed";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction Process Cancelled Please Try Again.')", true);
                                Response.Redirect("../../Apply_Career_Application.aspx", false);

                            }
                        }
                        else
                        {
                            ViewState["statuS"] = "Failed";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction Process Cancelled Please Try Again.')", true);
                            Response.Redirect("../../Apply_Career_Application.aspx", false);
                        }


                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction Process Cancelled Please Try Again.')", true);
                        Response.Redirect("../../Apply_Career_Application.aspx", false);
                    }

                }
                else
                {
                    ViewState["statuS"] = "Failed";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction Process Cancelled Please Try Again.')", true);
                    Response.Redirect("../../Apply_Career_Application.aspx", false);
                }

            }
        }

        private void update_data_base()
        {
            DataTable dt = mycode.FillData("select * from Employee_Online_Apply where  razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                ViewState["orderno"] = dt.Rows[0]["Order_id"].ToString();
                ViewState["Admission_no"] = dt.Rows[0]["Apply_id"].ToString();
                ViewState["no_application"] = dt.Rows[0]["no_of_seat"].ToString();

                ViewState["sessionid"] = dt.Rows[0]["Session_id"].ToString();
                ViewState["position_job_for"] = dt.Rows[0]["Apply_for"].ToString();
                ViewState["Hiring_id"] = dt.Rows[0]["Hiring_id"].ToString();
                int fill_no_seat = My.get_no_seat_current_session_employee_hiring(ViewState["sessionid"].ToString(), ViewState["position_job_for"].ToString(), ViewState["Hiring_id"].ToString());// available seat

                if (Convert.ToInt32(ViewState["no_application"].ToString()) >= fill_no_seat)
                {
                    My.exeSql("update Employee_Online_Apply set razorpay_payment_id='" + ViewState["razorpay_payment_id"].ToString() + "',razorpay_signature='" + ViewState["razorpay_signature"].ToString() + "',Payment_Status='Paid',Seat_Remarks='OK' where Apply_id='" + ViewState["Admission_no"].ToString() + "' and Order_id='" + ViewState["orderno"].ToString() + "'");
                    Response.Redirect("../../print/Print_Page_career.aspx?PaymentSliP=" + ViewState["Admission_no"].ToString(), false);

                }
                else
                {
                    My.exeSql("update Employee_Online_Apply set razorpay_payment_id='" + ViewState["razorpay_payment_id"].ToString() + "',razorpay_signature='" + ViewState["razorpay_signature"].ToString() + "',Payment_Status='Paid',Seat_Remarks='Seatfull' where Apply_id='" + ViewState["Admission_no"].ToString() + "' and Order_id='" + ViewState["orderno"].ToString() + "'");
                    Response.Redirect("../../print/Print_Page_career.aspx?PaymentSliP=" + ViewState["Admission_no"].ToString(), false);
                }
                System.Threading.Thread.Sleep(3000);
                a1.Visible = false;
            }
        }

       
    }
}