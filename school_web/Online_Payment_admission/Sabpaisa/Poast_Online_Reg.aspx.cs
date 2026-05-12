using SabPaisaDotNetIntregreation;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Payment_admission.Sabpaisa
{
    public partial class Poast_Online_Reg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["dfsvdfsvxdftsfhgxgfstdfgh"] != null)
            {
                ViewState["regid"] = Request.QueryString["dfsvdfsvxdftsfhgxgfstdfgh"].ToString();
               
                string query = "Select *, (Select top 1 Session from session_details where session_id=Online_Admission.Session_id) as sessionname  from Online_Admission where Registration_id='" + ViewState["regid"].ToString() + "' ";
                SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad_contactus.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ViewState["orderid"] = dr["Order_id"].ToString();  
                        My mycode = new My();

                     
                        string studentname = dr["Name"].ToString();
                        string Admission_no = dr["Registration_id"].ToString();
                        string Session = dr["sessionname"].ToString();
                        string classname = dr["Class"].ToString();
                        string rollnumber ="Na";
                        string Address = "NA";


                        Dictionary<string, object> dc1 = mycode.App_Setting_get_paisa();
                        string Sabpaisa_Client_Code = (String)dc1["Sabpaisa_Client_Code"];
                        string Sabpaisa_Username = (String)dc1["Sabpaisa_Username"];
                        string Sabpaisa_Password = (String)dc1["Sabpaisa_Password"];
                        string Sabpaisa_Authentication = (String)dc1["Sabpaisa_Authentication"];
                        string Sabpaisa_AuthenticationIV = (String)dc1["Sabpaisa_AuthenticationIV"];

                        SabPaisaRequest requestToGateway = new SabPaisaRequest();
                        SabPaisaIntegration objsb = new SabPaisaIntegration();

                        requestToGateway.clientCode = Sabpaisa_Client_Code;    // Please use the credentials shared by your Account Manager  If not, please contact your Account Manager
                        requestToGateway.transUserName = Sabpaisa_Username;   // Please use the credentials shared by your Account Manager  If not, please contact your Account Manager
                        requestToGateway.transUserPassword = Sabpaisa_Password;   // Please use the credentials shared by your Account Manager  If not, please contact your Account Manager
                        requestToGateway.authKey = Sabpaisa_Authentication;             // Please use the credentials shared by your Account Manager  If not, please contact your Account Manager
                        requestToGateway.authIV = Sabpaisa_AuthenticationIV;              // Please use the credentials shared by your Account Manager  If not, please contact your Account Manager

                        requestToGateway.payerName = studentname;
                        requestToGateway.payerEmail = dr["Student_email_id"].ToString();
                        requestToGateway.payerMobile = dr["Student_mob_no"].ToString();
                        requestToGateway.payerAddress = Address;

                        requestToGateway.clientTxnId = dr["Order_id"].ToString(); //objsb.randomTxnId().ToString();            //will be unique for every transaction
                        requestToGateway.amount = dr["Payable_amount"].ToString();
                        requestToGateway.amountType = "INR";
                        requestToGateway.channelId = "W";
                        requestToGateway.mcc = "8795";
                        requestToGateway.callbackUrl = My.url() + "Online_Payment_admission/Sabpaisa/PostPgResponseonline.aspx";// "http://localhost:56237/PostPgResponse.aspx";
                                                                                                                              // Extra Parameter you can use 20 extra parameters
                        requestToGateway.Class = classname;
                        requestToGateway.Roll = rollnumber;
                        requestToGateway.Session = Session;
                        requestToGateway.Admission_no = Admission_no;
                        requestToGateway.Payment_type = "Online Apply Admission";

                        string sFinalurl = "";
                        sFinalurl = objsb.forwardToSabPaisa(requestToGateway);


                        string respString = "<html>" +
                             "<body>" +
                                 "<form action=\"https://securepay.sabpaisa.in/SabPaisa/sabPaisaInit?v=1\" method=\"post\" >" +
                                 "<input type=\"hidden\" name=\"encData\" value=\"" + sFinalurl + "\" id=\"frm1\">" +
                                 "<input type=\"hidden\" name=\"clientCode\" value=\"" + requestToGateway.clientCode + "\" id =\"frm2\">" +
                                 "<input class='button-29' type=\"submit\" name=\"submit\" value=\"Pay Now\" id=\"submitButton\">" +
                                 "</ form >" +
                             "</body>" +
                        "</html>";

                        Response.Write(respString);
                    }


                }
            }
            else
            {
            }

        }
    }
}