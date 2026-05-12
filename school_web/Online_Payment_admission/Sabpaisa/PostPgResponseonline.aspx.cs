using SabPaisaDotNetIntregreation;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Payment_admission.Sabpaisa
{
    public partial class PostPgResponseonline : System.Web.UI.Page
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
            if (STATUSCODE == "0000")// sucess
            {
                ViewState["statuS"] = "Paid";
                My.exeSql("update Online_Admission set razorpay_payment_id='" + sabpaisaTxnId + "', razorpay_order_id='" + bankTxnId + "',Payment_Status='" + ViewState["statuS"].ToString() + "',razorpay_signature='" + query + "',Response_String='" + Response_String + "'  where  Order_id='" + clientTxnld + "' and Registration_id='" + udf4 + "'");
                Bind_data();
            }
            else
            {
                ViewState["statuS"] = "Failed";
                My.exeSql("update Online_Admission set razorpay_payment_id='" + sabpaisaTxnId + "', razorpay_order_id='" + bankTxnId + "',Payment_Status='Failed',razorpay_signature='" + query + "',Response_String='" + Response_String + "' where  Order_id='" + clientTxnld + "' and Registration_id='" + udf4 + "'");
                Bind_data();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Bind_data();



        }

        private void Bind_data()
        {
            if (ViewState["statuS"].ToString() == "Paid")
            {
                //paid process

                DataTable dt = mycode.FillData("select * from Online_Admission where Order_id='" + ViewState["ordertrackingid"].ToString() + "'");
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

                        string qry = "update Online_Admission set Steps_done='10' where Registration_id='" + Registration_id + "'";
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

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}