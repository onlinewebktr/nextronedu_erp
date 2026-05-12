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
    public partial class PostPgResponsemonth : System.Web.UI.Page
    {
        My mycode = new My();
        SabPaisaIntegration sabPaisaIntegration = new SabPaisaIntegration();
        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();
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
            if (STATUSCODE=="0000")// sucess
            {
                ViewState["statuS"] = "Paid";
                My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + sabpaisaTxnId + "', razorpay_order_id='" + bankTxnId + "',status='Success',razorpay_signature='"+ query + "',Response_String='"+ Response_String + "'  where  ordertrackingid='" + clientTxnld + "' and Admission_no='" + udf4 + "'");
                Bind_data();
            }
            else
            {
                ViewState["statuS"] = "Failed";
                My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + sabpaisaTxnId + "', razorpay_order_id='" + bankTxnId + "',status='Failed',razorpay_signature='" + query + "',Response_String='" + Response_String + "' where  ordertrackingid='" + clientTxnld + "' and Admission_no='"+ udf4 + "'");
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
                DataTable dt = mycode.FillData("Select * from Payment_transaction_process where ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "' ");
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
                System.Threading.Thread.Sleep(3000);
                a1.Visible = false;

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)// testing 
        {
            string query = "Em/kFS3DevHBVPK0yAE9/1cjyl6CfcfTQiUbt1Lm3fFdt/XtimwLEGGxP0iB+ZskhKiisAXbEpk6I4RKCKpy2xwZ+45AJxjPdoYA5iAr3zzRiFc8xU4FJCSbYq3dK14jBR+Fs7zKoNFdOp95ghsI9T6cGNsB3llFaMUL3Kf+aSYYECrmS8fMHbetUJvOYatcx1KY3uDAW7LgCTJbpe423yneWY34UpC00aL/FnNCrB8sq73GlBHy92iaZ34aNs4B2M4mMenOzo0Qx1scSdv4pvD+03AHYBEfYnonCVUmDWicmNRP49KTGFU+Ojm7hGlAUSis407Dh1vI/N4Fsbn6wnui7nzEbGPJd7LdHBzuZ3Lv6xPlYPNzSWwaZFlRTZv/MtZT2BbGox2osx1ABXIWVH46Z/+KwQtVJxs9vf29GDwLq5A3C43oYNEHcD3jtBrEb9HPmXQpYgieXA+QD1Raxb8kvm3pRd9Ao5xBF0OoEalaRmvl43l9CCEP1OvVLhcbFI/qLzph0DFLCpLsqa15o5FvdNAvfe4nFe923P02mGm1591IwnmpM7P/qgUVwjKKtSd9yv8HAEn1RW3uWcOTDTcyxeafNLdsw/Kf0ByApgkP0j5HLrFD91mgqQ7WwgbnFEzswdNiZREwhCKurKAoJKwSpPNXESvzqOP17WanoGaS1DGPNJvsW2Xx1aux9g1AQDCYbqSacbdIk9CU3wPtC9b92S5rPuNMH8+hlfnaIozLoSkm4gcNUO/KKv908AZjzwZSLvziGcubjwAfPwNmorreEEExWVbxraPV638NbJwJbBXtlhVVFl5mPDGBb36+I+8hlMp5C0tOhutmM0eFmvnHoQ22AXBWzppS6O1Gd73O4R6QEonz300MRrwoNv5/xOukFJsUM6KVOn5FrBoaO9jjzjtUjSvlBe/Xs6yc16gyrjH5J9INNg52SyDhopZFUWQx3l/nuULXJOekiWAYPPYmAHso8kKhwdIfUomTA6qNLbt9ViN7bPJP46gAjZ28dL6Dkgi6zKohsX6Ts9Wh4hGdnXqiMeSN7R7TsY+XT17y6GloqHjdImNc5FLWqb/NQzh64oouM6HqwNPTqhNI/g==";
            Dictionary<string, string> sabPaisaRespdict = new Dictionary<string, string>();


            Dictionary<string, object> dc1 = mycode.App_Setting_get_paisa();
            string Sabpaisa_Client_Code = (String)dc1["Sabpaisa_Client_Code"];
            string Sabpaisa_Username = (String)dc1["Sabpaisa_Username"];
            string Sabpaisa_Password = (String)dc1["Sabpaisa_Password"];
            string Sabpaisa_Authentication = (String)dc1["Sabpaisa_Authentication"];
            string Sabpaisa_AuthenticationIV = (String)dc1["Sabpaisa_AuthenticationIV"];


            string authKey = Sabpaisa_Authentication;
            string authIV = Sabpaisa_AuthenticationIV;
            sabPaisaRespdict = sabPaisaIntegration.subPaisaResponse(query, authIV, authKey);
            foreach (KeyValuePair<string, string> pair in sabPaisaRespdict)
            {
                divresponse.InnerHtml = divresponse.InnerHtml + " <br /> " + pair.Key.ToString() + "  -  " + pair.Value.ToString();

                if (pair.Key.ToString().ToUpper() == "STATUSCODE")
                {
                    lblStatus.Text = pair.Value.ToString();//0000//SUCCESS 
                }

                if(pair.Key.ToString().ToUpper() == "clientTxnld")
                {

                }
                if (pair.Key.ToString().ToUpper() == "sabpaisaTxnId")
                {

                }
                if (pair.Key.ToString().ToUpper() == "bankTxnId")
                {

                }
                if (pair.Key.ToString().ToUpper() == "udf4")// Student Admission No
                {

                }
                

            }
        }
    }
}