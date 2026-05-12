using SabPaisaDotNetIntregreation;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.Sabpaisa
{
    public partial class PostPgResponse_ad_an : System.Web.UI.Page
    {
        My mycode = new My();
        SabPaisaIntegration sabPaisaIntegration = new SabPaisaIntegration();
        Payment_update_after_onlinepayment_Admission mypayment = new Payment_update_after_onlinepayment_Admission();
        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> sabPaisaRespdict = new Dictionary<string, string>();
            string query = Request.Form["encResponse"].ToString();

            Dictionary<string, object> dc1 = mycode.App_Setting_get_paisa();
            string Sabpaisa_Client_Code = (String)dc1["Sabpaisa_Client_Code"];
            string Sabpaisa_Username = (String)dc1["Sabpaisa_Username"];
            string Sabpaisa_Password = (String)dc1["Sabpaisa_Password"];
            string Sabpaisa_Authentication = (String)dc1["Sabpaisa_Authentication"];
            string Sabpaisa_AuthenticationIV = (String)dc1["Sabpaisa_AuthenticationIV"];


            string authKey = Sabpaisa_Authentication;
            string authIV = Sabpaisa_AuthenticationIV;
            sabPaisaRespdict = sabPaisaIntegration.subPaisaResponse(query, authIV, authKey);
            string STATUSCODE = "";
            string clientTxnld = "";
            string sabpaisaTxnId = "";
            string bankTxnId = "";
            string udf4 = "";
            foreach (KeyValuePair<string, string> pair in sabPaisaRespdict)
            {


                divresponse.InnerHtml = divresponse.InnerHtml + " <br /> " + pair.Key.ToString() + "  -  " + pair.Value.ToString();

                if (pair.Key.ToString().ToUpper() == "STATUSCODE")
                {
                    STATUSCODE = pair.Value.ToString();//0000//SUCCESS 
                }

                if (pair.Key.ToString().ToUpper() == "CLIENTTXNID")
                {
                    clientTxnld = pair.Value.ToString();//ordertrackingid

                }
                if (pair.Key.ToString().ToUpper() == "SABPAISATXNID")
                {
                    sabpaisaTxnId = pair.Value.ToString();
                }
                if (pair.Key.ToString().ToUpper() == "BANKTXNID")
                {
                    bankTxnId = pair.Value.ToString();
                }
                if (pair.Key.ToString().ToUpper() == "UDF4")// Student Admission No
                {
                    udf4 = pair.Value.ToString();

                }
            }

            string Response_String = divresponse.InnerHtml;
            Session["regid"] = udf4;
            ViewState["ordertrackingid"] = clientTxnld;
            if (STATUSCODE == "0000")// sucess
            {
                ViewState["statuS"] = "Paid";
                My.exeSql("update Payment_transaction_process_Admission set razorpay_payment_id='" + sabpaisaTxnId + "', razorpay_order_id='" + bankTxnId + "',status='Success',Encrypted_url='" + query + "',razorpay_signature='" + Response_String + "'  where  ordertrackingid='" + clientTxnld + "' and Admission_no='" + udf4 + "'");
                Bind_data();
            }
            else
            {
                ViewState["statuS"] = "Failed";
                My.exeSql("update Payment_transaction_process_Admission set razorpay_payment_id='" + sabpaisaTxnId + "', razorpay_order_id='" + bankTxnId + "',status='Failed',Encrypted_url='" + query + "',razorpay_signature='" + Response_String + "' where  ordertrackingid='" + clientTxnld + "' and Admission_no='" + udf4 + "'");
                Bind_data();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Bind_data();



        }

        private void Bind_data()
        {
            try
            {
                DataTable dt = mycode.FillData("Select * from Payment_transaction_process_Admission where ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "' ");
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
                        Response.Redirect("../Payment_Error_Message_admission.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["Pay_from"].ToString(), false);


                       
                    }
                }
               // System.Threading.Thread.Sleep(30000);
                a1.Visible = false;

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }

    }
}