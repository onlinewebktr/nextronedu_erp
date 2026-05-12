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
    public partial class PayProcess_blank_callback : System.Web.UI.Page
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

                                My.exeSql("update Online_Admission set razorpay_payment_id='" + ViewState["razorpay_payment_id"].ToString() + "',razorpay_signature='" + ViewState["razorpay_signature"].ToString() + "',Payment_Status='" + ViewState["statuS"].ToString() + "'  where  razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'");
                            }
                            else if (status.ToUpper() == "AUTHORIZED")
                            {
                                ViewState["statuS"] = "Paid";

                                My.exeSql("update Online_Admission set razorpay_payment_id='" + ViewState["razorpay_payment_id"].ToString() + "',razorpay_signature='" + ViewState["razorpay_signature"].ToString() + "',Payment_Status='" + ViewState["statuS"].ToString() + "' where  razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'");
                            }

                            else
                            {
                                ViewState["statuS"] = "Failed";

                            }
                        }
                        else
                        {
                            ViewState["statuS"] = "Failed";
                            Response.Redirect("../../Student_Online_Registration_Apply.aspx", false);
                        }


                    }
                    catch
                    {
                        Response.Redirect("../../Student_Online_Registration_Apply.aspx", false);
                    }
                }
                else
                {
                   
                    ViewState["statuS"] = "Failed";
                    Response.Redirect("../../Student_Online_Registration_Apply.aspx", false);
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["statuS"].ToString() == "Paid")
            {
                //paid process

                DataTable dt = mycode.FillData("select * from Online_Admission where razorpay_order_id='" + ViewState["razorpay_order_id"].ToString() + "'");
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    string Registration_id = dt.Rows[0]["Registration_id"].ToString();
                    string Session_id = dt.Rows[0]["Session_id"].ToString();
                    string Class_id = dt.Rows[0]["Class_id"].ToString();
                    string testid = dt.Rows[0]["Test_id"].ToString();

                    int fill_no_seat = My.get_no_seat_current_session(Session_id, Class_id, testid);
                    int get_no_fill_seat = My.get_no_of_fill_from_seat(Session_id, Class_id, testid);
                    if (get_no_fill_seat >= fill_no_seat)//seat check
                    {
                       
                        string qry = "update Online_Admission set Steps_done='10',Payment_Status='" + ViewState["statuS"].ToString() + "' where Registration_id='" + Registration_id + "'";
                        mycode.executequery(qry);

                        Response.Redirect("../../print/Print_Page.aspx?PaymentSliP=" + Registration_id, false);
                    }
                    else
                    {
                        string qry = "update Online_Admission set Steps_done='9',Payment_Status='Unpaid',Payment_remarks='SeatFull' where Registration_id='" + Registration_id + "'";
                        mycode.executequery(qry);

                        Response.Redirect("../../print/Print_Page_Seat_full.aspx?PaymentSliP=" + Registration_id, false);

                    }


                  
                   
                }


                  


            }
            else // failed/technila issues ocured
            {
                Response.Redirect("../../Student_Online_Registration_Apply.aspx", false);
            }
            System.Threading.Thread.Sleep(3000);
            a1.Visible = false;
        }
    }
}