using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
using Newtonsoft.Json;

using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using getepay_sdk;

using school_web.Student_Profile.webview.Billdesk;
using System.Net.Http;
using System.Net.Http.Headers;
using Jose;
using SabPaisaDotNetIntregreation;

namespace school_web.Student_Profile.webview
{
    public partial class payfinal : System.Web.UI.Page
    {
        com.awl.MerchantToolKit.ReqMsgDTO objReqMsgDTO;
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    hd_logo.Value = My.get_logo();
                    hd_schoolname.Value = My.get_school_name();
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    ViewState["orderid"] = Request.QueryString["orderid"].ToString();
                    ViewState["payFrom"] = Request.QueryString["payFrom"].ToString();
                    Session["regid"] = ViewState["regid"].ToString();
                    string class_id = get_class_id(ViewState["regid"].ToString(), ViewState["orderid"].ToString());
                    ViewState["Getwey_Type"] = mycode.get_payment_Getwey_Type_monthly(class_id);
                    Bind_data();
                }
                else
                {
                    try
                    {
                        if (Request.QueryString["payFrom"].ToString() == "1")
                        {
                            Response.Redirect("Student_Monthly_Payment.aspx?regid=" + ViewState["regid"].ToString(), false);
                        }
                        else
                        {
                            Response.Redirect("../student-monthly-payment.aspx", false);
                        }
                    }
                    catch
                    {
                        Response.Redirect("Student_Monthly_Payment.aspx", false);

                    }

                }
            }
        }

        private string get_class_id(string admission_no, string order_id)
        {
            string returN = "0";
            string query = "select top 1 Class_id from admission_registor where admissionserialnumber='" + admission_no + "'   and StudentStatus='AV' and  Status='1'  and Is_TC_Taken!='true' and Transfer_Status in ('New','NT') order by id desc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count > 0)
            {
                returN = dt.Rows[0][0].ToString();
            }
            return returN;
        }

        string ccaRequest = "";
        public string strEncRequest = "";
        private async void Bind_data()
        {
            string query = "select   *,(select top 1 rollnumber from  admission_registor where admissionserialnumber=Payment_transaction_process.Admission_no and Session_id=Payment_transaction_process.Session_id and Class_id=Payment_transaction_process.Class_id) as rollno from  Payment_transaction_process where Admission_no='" + ViewState["regid"].ToString() + "' and ordertrackingid='" + ViewState["orderid"].ToString() + "'";
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
                    if (ViewState["Getwey_Type"].ToString() == "Razorpay")// if razor Pay getwey
                    {
                        ViewState["Session"] = dr["session"].ToString();
                        string ayouthkey = My.autkeyrozorkey();
                        double amtForRazorrounD = Math.Round(Convert.ToDouble(dr["Total_pay"].ToString()));
                        int aftrrounD = (Convert.ToInt32(amtForRazorrounD) * 100);
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        CreateOrder co = new CreateOrder();
                        co.amount = aftrrounD;
                        co.currency = "INR";
                        co.receipt = "rcptid_11";
                        co.notes = new Dictionary<string, string>
            {
                { "Name", dr["Name"].ToString() },
                { "Class", dr["Class_name"].ToString() },
                             { "Section",dr["Section"].ToString() },
                              { "Roll No.", dr["rollno"].ToString() },
                               { "Admission No.",  dr["Admission_no"].ToString() }, { "Payment Month",dr["month"].ToString()   }
           ,{ "Order id",ViewState["orderid"].ToString() } };
                        string jsondata = JsonConvert.SerializeObject(co);
                        string url = "https://api.razorpay.com/v1/orders";
                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        //httpWebRequest.Headers["authorization"] = "Basic cnpwX2xpdmVfald6a0dQNGNUcld1Ulg6eUZJanJYV0h5bG02aUJ5WktwblNXZWNR";
                        httpWebRequest.Headers["authorization"] = "Basic " + ayouthkey;

                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.MediaType = "application/json";
                        httpWebRequest.Accept = "application/json";

                        var data = Encoding.ASCII.GetBytes(jsondata);
                        httpWebRequest.Method = "POST";
                        httpWebRequest.ContentLength = data.Length;

                        using (var stream = httpWebRequest.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }

                        var response = (HttpWebResponse)httpWebRequest.GetResponse();
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                        var cr = JsonConvert.DeserializeObject<CreatedOrder>(responseString);
                        string odrID = cr.id;

                        hd_key.Value = My.MERCHANT_KEY();
                        // hd_amount.Value =  (Convert.ToDouble(1) * 100).ToString();
                        hd_amount.Value = (Convert.ToDouble(dr["Total_pay"].ToString()) * 100).ToString();
                        hd_fname.Value = dr["Name"].ToString();
                        hd_email.Value = dr["Emailid"].ToString();
                        hd_mobile.Value = dr["Mobileno"].ToString();
                        hd_razor_odr_id.Value = odrID;
                        hd_regid.Value = dr["Admission_no"].ToString();
                        hd_order_id.Value = dr["ordertrackingid"].ToString();
                        //================

                        My.exeSql("update Payment_transaction_process set razorpay_order_id='" + odrID + "',Pay_from='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        Session["sytemid"] = ViewState["orderid"].ToString();
                        Response.Redirect("RazorPay/Check_out.aspx", false);// call back   
                    }
                    else if (ViewState["Getwey_Type"].ToString() == "Razorpay_new")// if razor Pay getwey
                    {
                        ViewState["Session"] = dr["session"].ToString();
                        string ayouthkey = My.autkeyrozorkey();
                        double amtForRazorrounD = Math.Round(Convert.ToDouble(dr["Total_pay"].ToString()));
                        int aftrrounD = (Convert.ToInt32(amtForRazorrounD) * 100);
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        CreateOrder co = new CreateOrder();
                        co.amount = aftrrounD;
                        co.currency = "INR";
                        co.receipt = "rcptid_11";
                        co.notes = new Dictionary<string, string>
            {
                { "Name", dr["Name"].ToString() },
                { "Class", dr["Class_name"].ToString() },
                             { "Section",dr["Section"].ToString() },
                              { "Roll No.", dr["rollno"].ToString() },
                               { "Admission No.",  dr["Admission_no"].ToString() }, { "Payment Month",dr["month"].ToString()   }
            };
                        string jsondata = JsonConvert.SerializeObject(co);
                        string url = "https://api.razorpay.com/v1/orders";
                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        //httpWebRequest.Headers["authorization"] = "Basic cnpwX2xpdmVfald6a0dQNGNUcld1Ulg6eUZJanJYV0h5bG02aUJ5WktwblNXZWNR";
                        httpWebRequest.Headers["authorization"] = "Basic " + ayouthkey;

                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.MediaType = "application/json";
                        httpWebRequest.Accept = "application/json";

                        var data = Encoding.ASCII.GetBytes(jsondata);
                        httpWebRequest.Method = "POST";
                        httpWebRequest.ContentLength = data.Length;

                        using (var stream = httpWebRequest.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }

                        var response = (HttpWebResponse)httpWebRequest.GetResponse();
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                        var cr = JsonConvert.DeserializeObject<CreatedOrder>(responseString);
                        string odrID = cr.id;

                        hd_key.Value = My.MERCHANT_KEY();
                        // hd_amount.Value =  (Convert.ToDouble(1) * 100).ToString();
                        hd_amount.Value = (Convert.ToDouble(dr["Total_pay"].ToString()) * 100).ToString();
                        hd_fname.Value = dr["Name"].ToString();
                        hd_email.Value = dr["Emailid"].ToString();
                        hd_mobile.Value = dr["Mobileno"].ToString();
                        hd_razor_odr_id.Value = odrID;
                        hd_regid.Value = dr["Admission_no"].ToString();
                        hd_order_id.Value = dr["ordertrackingid"].ToString();
                        //================

                        My.exeSql("update Payment_transaction_process set razorpay_order_id='" + odrID + "',Pay_from='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        Session["sytemid"] = ViewState["orderid"].ToString();


                        Response.Redirect("RazorPay/Check_out_bolt.aspx", false);// call back  


                    }
                    else if (ViewState["Getwey_Type"].ToString() == "Worldline")
                    {

                        try
                        {
                            My.exeSql("update Payment_transaction_process set razorpay_order_id='" + dr["ordertrackingid"].ToString() + "',Pay_from='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                            objReqMsgDTO = new com.awl.MerchantToolKit.ReqMsgDTO();
                            objReqMsgDTO.OrderId = dr["ordertrackingid"].ToString();
                            objReqMsgDTO.Mid = My.Merchant_Key_Get_Worldline();
                            objReqMsgDTO.Enckey = My.Autkeyrozorkey_Get_Worldline(); //autkeyrozorkey();
                            objReqMsgDTO.MeTransReqType = "S";

                            double amt = Convert.ToDouble(dr["Total_pay"].ToString()) * 100;
                            objReqMsgDTO.TrnAmt = amt.ToString();
                            // objReqMsgDTO.TrnAmt = "100";

                            objReqMsgDTO.RecurrPeriod = "";
                            objReqMsgDTO.RecurrDay = "";
                            objReqMsgDTO.ResponseUrl = My.URL() + "Student_Profile/webview/worldline/Response.aspx";
                            objReqMsgDTO.TrnRemarks = "Monthly Payment";
                            objReqMsgDTO.TrnCurrency = "INR";
                            objReqMsgDTO.AddField1 = dr["Admission_no"].ToString();
                            objReqMsgDTO.AddField2 = dr["Name"].ToString();
                            objReqMsgDTO.AddField3 = dr["Emailid"].ToString();
                            objReqMsgDTO.AddField4 = dr["Mobileno"].ToString();
                            objReqMsgDTO.AddField5 = ViewState["payFrom"].ToString();
                            objReqMsgDTO.AddField6 = "";
                            objReqMsgDTO.AddField7 = "";
                            objReqMsgDTO.AddField8 = "";
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            string Message;
                            com.awl.MerchantToolKit.AWLMEAPI objawlmerchantkit = new com.awl.MerchantToolKit.AWLMEAPI();
                            objawlmerchantkit.generateTrnReqMsg(objReqMsgDTO);
                            Message = objReqMsgDTO.ReqMsg;
                            Session["response"] = objReqMsgDTO;
                            Session["Message"] = Message;
                            Session["MID"] = objReqMsgDTO.Mid;
                            Response.Redirect("worldline/TrnPay_process.aspx", false);
                        }
                        catch
                        {
                        }
                    }
                    else if (ViewState["Getwey_Type"].ToString() == "CCAV")
                    {
                        if (ViewState["regid"].ToString() == "1370/2022-23")
                        {
                            try
                            {
                                My.exeSql("update Payment_transaction_process set razorpay_order_id='" + dr["ordertrackingid"].ToString() + "',Pay_from='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                                #region ccavenue_payment_gateway
                                string website = My.URL();
                                string CCAV_MERCHANT_KEY = My.MERCHANT_KEY_CCAV();
                                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                                double amt = My.toDouble(dr["Total_pay"].ToString());
                                Random r = new Random();
                                string orderid = r.Next(10000, 99999).ToString();
                                string ti = "145" + r.Next(100000, 999999).ToString();

                                // ccaRequest += "tid=1453199731904&";
                                ccaRequest = ccaRequest + "tid" + "= " + ti + "&";
                                ccaRequest = ccaRequest + "merchant_id" + "=" + CCAV_MERCHANT_KEY + "&";
                                ccaRequest = ccaRequest + "order_id" + "=" + dr["ordertrackingid"].ToString() + "&";
                                ccaRequest = ccaRequest + "amount" + "= " + amt + "&";
                                ccaRequest = ccaRequest + "currency=INR&";
                                ccaRequest = ccaRequest + "redirect_url" + "=" + website + "Student_Profile/webview/ccavenue/ccavResponseHandler.aspx&";
                                ccaRequest = ccaRequest + "cancel_url" + "=&";
                                ccaRequest = ccaRequest + "billing_name" + "=" + dr["Name"].ToString() + "&";
                                ccaRequest = ccaRequest + "billing_address" + "=&";
                                ccaRequest = ccaRequest + "billing_city" + "=&";
                                ccaRequest = ccaRequest + "billing_state" + "=&";
                                ccaRequest = ccaRequest + "billing_zip" + "=&";
                                ccaRequest = ccaRequest + "billing_country" + "=India&";
                                ccaRequest = ccaRequest + "billing_tel" + "=" + dr["Mobileno"].ToString() + "&";
                                ccaRequest = ccaRequest + "billing_email" + "=" + dr["Emailid"].ToString() + "&";
                                ccaRequest = ccaRequest + "customer_identifier" + "=" + dr["ordertrackingid"].ToString() + "&";
                                ccaRequest = ccaRequest + "delivery_name" + "=" + dr["Name"].ToString() + "&";
                                ccaRequest = ccaRequest + "delivery_address" + "=&";
                                ccaRequest = ccaRequest + "delivery_city" + "=&";
                                ccaRequest = ccaRequest + "delivery_state" + "=&";
                                ccaRequest = ccaRequest + "delivery_zip" + "=&";
                                ccaRequest = ccaRequest + "delivery_country" + "=India&";
                                ccaRequest = ccaRequest + "delivery_tel" + "=" + dr["Mobileno"].ToString() + "&";
                                ccaRequest = ccaRequest + "merchant_param1" + "=" + dr["ordertrackingid"].ToString() + "&";
                                ccaRequest = ccaRequest + "merchant_param2" + "=&";
                                ccaRequest = ccaRequest + "merchant_param3" + "=&";
                                ccaRequest = ccaRequest + "merchant_param4" + "=&";
                                ccaRequest = ccaRequest + "merchant_param5" + "=&";
                                ccaRequest = ccaRequest + "promo_code" + "=&";
                                Session["test"] = ccaRequest; // set  parameters values in session without encription  
                                try
                                {
                                    Response.Redirect("ccavenue/ccavRequest_Handler.aspx", false);
                                    // redirect to ccavRequestHandler.aspx for encription and next process  
                                    //Response.Redirect("ccavRequestHandler.aspx?enc=" + strEncRequest+" ", false);
                                }
                                catch (Exception ex)
                                {
                                }
                                #endregion   ccavenue_payment_gateway
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            Response.Redirect("Dues_List.aspx?regid=" + ViewState["regid"].ToString(), false);
                        }
                    }
                    #region EGPay
                    else if (ViewState["Getwey_Type"].ToString() == "EGPay")//egpay
                    {
                        My.exeSql("update Payment_transaction_process set razorpay_order_id='" + dr["ordertrackingid"].ToString() + "',Pay_from='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");


                        string MERCHANT_KEY = My.MERCHANT_KEY_EZ_PAY();
                        string autkeyrozorkey = My.Encryptionkey_EZPAY();
                        string Admission_no = dr["Admission_no"].ToString();
                        Session["regid"] = dr["Admission_no"].ToString();
                        string branchid = "1";
                        string sessionid = dr["Session_id"].ToString();
                        Dictionary<string, object> dc1 = My.get_selected_studentinfo(Admission_no, sessionid, branchid);
                        string classname = (String)dc1["classname"];
                        string Section = (String)dc1["Section"];
                        string rollnumber = (String)dc1["rollnumber"];
                        string session = (String)dc1["session"];

                        string redirecturl = "";  // this is to check what url is coming before encryption
                        string encryptredirecturl = "";

                        string ASEKEY = autkeyrozorkey;

                        string Reference_no, pgamount, Mobile_No, city, name, email_id;

                        Reference_no = dr["ordertrackingid"].ToString();

                        pgamount = dr["Total_pay"].ToString();// "1";//
                        Mobile_No = dr["Mobileno"].ToString();

                        city = "";
                        email_id = dr["Emailid"].ToString().Trim();
                        name = dr["Name"].ToString().Trim();
                        string class_and_section = classname.Trim() + Section.Trim();
                        string class_name = classname.Trim();
                        string Section_name = Section.Trim();
                        string sub_merchant_id = "12345";
                        string rollno = rollnumber;
                        string transpotfee = "0";
                        string latefee = "0";
                        string classfee = pgamount;

                        if (Mobile_No == "")
                        {
                            Mobile_No = "0000000000";
                        }
                        if (email_id == "")
                        {
                            email_id = "na@gmail.com";
                        }
                        if (city == "")
                        {
                            city = "WB";
                        }
                        string Session_name = session.Trim();
                        //  redirecturl += "https://eazypayuat.icicibank.com/EazyPG?";//local
                        redirecturl += "https://eazypay.icicibank.com/EazyPG?";//server
                        redirecturl += "merchantid=" + MERCHANT_KEY;

                        if (My.get_firm_id() == "BHC-1")
                        {
                            redirecturl += "&mandatory fields=" + Reference_no + "|" + sub_merchant_id + "|" + pgamount + "|" + name + "|" + Admission_no + "|" + class_name + "|" + Section_name + "|" + Session_name + "|" + email_id + "|" + Mobile_No + "|" + rollno;
                            redirecturl += "&optional fields=" + city;
                        }

                        if (My.get_firm_id() == "NNI-01")
                        {

                            redirecturl += "&mandatory fields=" + Reference_no + "|" + sub_merchant_id + "|" + pgamount + "|" + Admission_no + "|" + name + "|" + class_and_section + "|" + rollno + "|" + transpotfee + "|" + latefee + "|" + classfee;
                            redirecturl += "&optional fields=" + city;
                        }
                        else
                        {

                            redirecturl += "&mandatory fields=" + Reference_no + "|" + sub_merchant_id + "|" + pgamount + "|" + name + "|" + Admission_no + "|" + class_name + "|" + Section_name + "|" + rollno + "|" + Session_name + "|" + email_id + "|" + Mobile_No;
                            redirecturl += "&optional fields=" + "";
                        }
                        string returnurl = My.URL() + "Student_Profile/webview/EazyPay/Response.aspx";
                        // string returnurl = My.URL(); //+ "Student_Profile/webview/EazyPay/Response.aspx";
                        redirecturl += "&returnurl=" + returnurl;
                        redirecturl += "&Reference No=" + Reference_no;
                        redirecturl += "&submerchantid=" + sub_merchant_id;
                        redirecturl += "&transaction amount=" + pgamount;
                        redirecturl += "&paymode=9";
                        //encryptredirecturl += "https://eazypayuat.icicibank.com/EazyPG?";//local
                        encryptredirecturl += "https://eazypay.icicibank.com/EazyPG?";//server
                        encryptredirecturl += "merchantid=" + MERCHANT_KEY;

                        if (My.get_firm_id() == "BHC-1")
                        {
                            encryptredirecturl += "&mandatory fields=" + encryptFile(Reference_no + "|" + sub_merchant_id + "|" + pgamount + "|" + name + "|" + Admission_no + "|" + class_name + "|" + Section_name + "|" + Session_name + "|" + email_id + "|" + Mobile_No + "|" + rollno, ASEKEY);
                            encryptredirecturl += "&optional fields=" + encryptFile(city, ASEKEY);
                        }
                        if (My.get_firm_id() == "NNI-01")// Nn internation school
                        {
                            encryptredirecturl += "&mandatory fields=" + encryptFile(Reference_no + "|" + sub_merchant_id + "|" + pgamount + "|" + Admission_no + "|" + name + "|" + class_and_section + "|" + rollno + "|" + transpotfee + "|" + latefee + "|" + classfee, ASEKEY);
                            encryptredirecturl += "&optional fields=" + encryptFile(city, ASEKEY);

                        }
                        else
                        {



                            encryptredirecturl += "&mandatory fields=" + encryptFile(Reference_no + "|" + sub_merchant_id + "|" + pgamount + "|" + name + "|" + Admission_no + "|" + class_name + "|" + Section_name + "|" + rollno + "|" + Session_name + "|" + email_id + "|" + Mobile_No, ASEKEY);

                            encryptredirecturl += "&optional fields=" + encryptFile("", ASEKEY);
                        }

                        //"|" + rollno +

                        encryptredirecturl += "&returnurl=" + encryptFile(returnurl, ASEKEY);
                        encryptredirecturl += "&Reference No=" + encryptFile(Reference_no, ASEKEY);
                        encryptredirecturl += "&submerchantid=" + encryptFile(sub_merchant_id, ASEKEY);
                        encryptredirecturl += "&transaction amount=" + encryptFile(pgamount, ASEKEY);
                        encryptredirecturl += "&paymode=" + encryptFile("9", ASEKEY);

                        try
                        {
                            My.exeSql("update Payment_transaction_process set Plan_url='" + redirecturl + "',Encrypted_url='" + encryptredirecturl + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        }
                        catch (Exception ex)
                        {

                        }



                        Response.Redirect(encryptredirecturl, false);

                    }

                    #endregion
                    #region Get_E_Pay
                    else if (ViewState["Getwey_Type"].ToString() == "Getepay")//Getepay
                    {


                        GetepayConfig config = new GetepayConfig();
                        config.mid = My.Merchant_Key_Get_E_PAY();//108;
                        config.terminalId = My.TerminalId_Get_E_PAY();// "Getepay.merchant61062@icici";
                        // config.terminalId = "anand@gmai.com";
                        config.key = My.Encryptionkey_Get_E_PAY();// "JoYPd+qso9s7T+Ebj8pi4Wl8i+AHLv+5UNJxA3JkDgY=";
                        config.iv = My.IV_Get_E_PAY();//"hlnuyA9b4YxDq6oJSZFl8g==";





                        // config.url = "https://pay1.getepay.in:8443/getepayPortal/pg/generateInvoice";local
                        config.url = "https://portal.getepay.in:8443/getepayPortal/pg/generateInvoice";//live
                        GetepayRequest request = new GetepayRequest();

                        request.mid = config.mid;
                        request.amount = dr["Total_pay"].ToString();//"1.00"; //dr["Total_pay"].ToString();//"51.00";
                        request.merchantTransactionId = dr["ordertrackingid"].ToString();//"Sample07042023-4";
                        request.transactionDate = My.getdate3();//"2023-04-07 16:16:16";
                        request.terminalId = config.terminalId;
                        request.udf1 = dr["Mobileno"].ToString();//"9999999999";
                        request.udf2 = dr["Emailid"].ToString();
                        request.udf3 = dr["Name"].ToString();
                        request.udf4 = dr["Admission_no"].ToString();//"9999999999";
                        request.udf5 = "Month Fee Pay";
                        request.udf6 = dr["Session"].ToString();
                        request.udf7 = dr["Class_name"].ToString();
                        request.udf8 = dr["month"].ToString();
                        request.udf9 = "";//"9999999999";
                        request.udf10 = "";
                        request.ru = My.URL() + "Student_Profile/webview/Get_Epay/Response.aspx";//"http://localhost:63645/default";
                        request.callbackUrl = "";
                        request.currency = "INR";
                        request.paymentMode = "ALL";
                        request.bankId = "";
                        request.txnType = "single";
                        request.productType = "IPG";
                        request.txnNote = "Test Txn";
                        request.vpa = config.terminalId;
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                        GetepayOrderResponse orderResponse = Getepay.generateRequest(config, request);

                        string getpay_id = orderResponse.paymentId;
                        My.exeSql("update Payment_transaction_process set razorpay_order_id='" + getpay_id + "',Pay_from='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");

                        string paymentUrl = orderResponse.paymentUrl;
                        Response.Redirect(paymentUrl, false);



                    }
                    #endregion
                    #region BillDesk
                    else if (ViewState["Getwey_Type"].ToString() == "Getepay")//Getepay
                    {
                        My.exeSql("update Payment_transaction_process set razorpay_order_id='" + dr["ordertrackingid"].ToString() + "'  where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        {
                            //var json = File.ReadAllText(@"config/config.json");

                            //var config = JsonConvert.DeserializeObject<Config>(json);

                            //if (config == null)
                            //{
                            //    Console.WriteLine("Failed to read configuration");
                            //    return;
                            //}

                            var handler = GetProxySetting(My.Billdesk_ProxyUrl());

                            HttpClient client = new HttpClient(handler);

                            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                            var payload = new
                            {
                                mercid = My.Billdesk_MerchantID(),
                                orderid = dr["ordertrackingid"].ToString(),//"ORD" + DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
                                amount = dr["Total_pay"].ToString(),
                                order_date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                                currency = "356",//Indian Rupee
                                ru = "https://merchant.com/payment/process",
                                additional_info = new
                                {
                                    additional_info1 = dr["Admission_no"].ToString(),
                                    additional_info2 = dr["Name"].ToString(),
                                    additional_info3 = dr["Session"].ToString(),
                                    additional_info4 = dr["Class_name"].ToString()
                                },
                                itemcode = "DIRECT",
                                device = new
                                {
                                    init_channel = "internet",
                                    ip = "17.233.107.92",
                                    mac = "11-AC-58-21-1B-AA",
                                    imei = "990000112233445",
                                    user_agent = "Mozilla/5.0",
                                    accept_header = "text/html",
                                    fingerprintid = "61b12c18b5d0cf901be34a23ca64bb19"
                                }
                            };

                            var headers = new Dictionary<string, object>()
            {
                { "clientid", My.Billdesk_ClientID() }
            };

                            string secretKey = My.Billdesk_SecretKey();

                            Jwk key = new Jwk(Encoding.UTF8.GetBytes(secretKey));
                            var token = Jose.JWT.Encode(payload, key, JwsAlgorithm.HS256, headers);
                            Console.WriteLine("JTW: " + token);

                            client.DefaultRequestHeaders.Add("Accept", "application/jose");

                            HttpContent _Body = new StringContent(token);
                            _Body.Headers.ContentType = new MediaTypeHeaderValue("application/jose");

                            var traceid = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                            var bdTimestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                            Console.WriteLine("BD-Traceid: " + traceid);
                            Console.WriteLine("BD-Timestamp: " + bdTimestamp);
                            _Body.Headers.Add("BD-Traceid", traceid);
                            _Body.Headers.Add("BD-Timestamp", bdTimestamp);

                            var url = "https://pguat.billdesk.io/payments/ve1_2/orders/create";

                            Console.WriteLine("Before client");
                            var response = await client.PostAsync(url, _Body);
                            Console.WriteLine("After client");

                            string result = response.Content.ReadAsStringAsync().Result;

                            var pgresponse = Jose.JWT.Decode(result, key, JwsAlgorithm.HS256);
                            Console.WriteLine("PG Response encrypted: " + result);
                            Console.WriteLine("PG Response: " + pgresponse);
                        }
                    }
                    #endregion
                    #region Sabpaisa
                    else if (ViewState["Getwey_Type"].ToString() == "SabPaisa")//SabPaisa
                    {
                        Session["sytemid"] = ViewState["orderid"].ToString();
                        Response.Redirect("Sabpaisa/Poast_Payment_Month.aspx?regid=" + ViewState["regid"].ToString() + "&orderid=" + ViewState["orderid"].ToString(), false);// call back  


                    }
                    #endregion
                    #region new Worldline
                    else if (ViewState["Getwey_Type"].ToString() == "Worldline New")
                    {
                        Response.Redirect("worldline/Req_NewMonthley_worldline.aspx?regid=" + ViewState["regid"].ToString() + "&orderid=" + ViewState["orderid"].ToString(), false);


                    }
                    #endregion


                    if (ViewState["Getwey_Type"].ToString() == "ICICI")
                    {
                        My.exeSql("update Payment_transaction_process set txnDate='" + DateTime.Now.ToString("yyyyMMddHHmmss") + "',razorpay_order_id='" + dr["ordertrackingid"].ToString() + "',Pay_from='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        // Session["razorpay_order_id"] = dr["ordertrackingid"].ToString();
                        // Response.Redirect("icic/Check_out_icic.aspx", false);// call back   

                        Dictionary<string, object> dc2 = mycode.get_icic_gateway_details();
                        string merchantIdVal = (String)dc2["ICIC_MID"];
                        string aggregatorIDVal = (String)dc2["ICIC_Agg_ID"];
                        string secretKey = (String)dc2["ICIC_Key"];
                        DataTable dt1 = mycode.FillData("Select *,(select top 1 Course_Name  from Add_course_table where course_id=Payment_transaction_process.Class_id) as classname from Payment_transaction_process where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        if (dt1.Rows.Count == 0)
                        {

                        }
                        else
                        {
                            // 🔹 Values
                            string email = "";
                            string txnNo = dt1.Rows[0]["ordertrackingid"].ToString();
                            string amountVal = dt1.Rows[0]["Total_pay"].ToString();
                            string currencyVal = "356";
                            string payTypeVal = "0";

                            if (dt1.Rows[0]["Emailid"].ToString() == "")
                            {
                                email = "na@gmail.com";
                            }
                            else
                            {
                                email = dt1.Rows[0]["Emailid"].ToString();
                            }

                            string txnType = "SALE";
                            string returnUrlVal = My.URL() + "Student_Profile/webview/icic/response.aspx";

                            //string returnUrlVal = "https://pgpayuat.icicibank.com/tsp/pg/api/merchant";

                            string txnDateVal = dt1.Rows[0]["txnDate"].ToString();
                            string mobile = dt1.Rows[0]["Mobileno"].ToString();
                            string name = dt1.Rows[0]["Name"].ToString();
                            string add1 = dt1.Rows[0]["Admission_no"].ToString();
                            string add2 = dt1.Rows[0]["classname"].ToString();

                            string add4 = dt1.Rows[0]["month"].ToString();
                            string message =
                            add1 + add2 + aggregatorIDVal + amountVal + currencyVal + email + mobile + name + merchantIdVal + txnNo + payTypeVal + returnUrlVal + txnType + txnDateVal;
                            string hash = My.GenerateHmac(message, secretKey);

                            //  var url = "https://pgpayuat.icicibank.com/tsp/pg/api/v2/initiateSale";// test for local host
                            var url = "https://pgpay.icicibank.com/pg/api/v2/initiateSale";

                            var requestData = new PaymentRequest
                            {
                                merchantId = merchantIdVal,
                                aggregatorID = aggregatorIDVal,
                                merchantTxnNo = txnNo,
                                amount = amountVal,
                                currencyCode = currencyVal,
                                payType = payTypeVal,
                                customerEmailID = email,
                                transactionType = txnType,
                                returnURL = returnUrlVal,
                                txnDate = txnDateVal,
                                customerMobileNo = mobile,
                                customerName = name,
                                addlParam1 = add1,
                                addlParam2 = add2,

                                secureHash = hash
                            };
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            using (HttpClient client = new HttpClient())
                            {
                                var json = JsonConvert.SerializeObject(requestData);
                                var content = new StringContent(json, Encoding.UTF8, "application/json");

                                var response = await client.PostAsync(url, content);
                                var responseString = await response.Content.ReadAsStringAsync();

                                Console.WriteLine(responseString);

                                dynamic result = JsonConvert.DeserializeObject(responseString);

                                if (result.responseCode == "R1000")
                                {
                                    string tranCtx = result.tranCtx;

                                    // Redirect URL
                                    //string redirectUrl = result.redirectURI+"?tranCtx=" + tranCtx;  string redirectUrl = "https://pgpayuat.icicibank.com/tsp/pg/api/v2/authRedirect?tranCtx=" + tranCtx;
                                    string redirectUrl = result.redirectURI + "?tranCtx=" + tranCtx;

                                    Console.WriteLine("Redirect URL: " + redirectUrl);

                                    // ASP.NET redirect
                                    System.Web.HttpContext.Current.Response.Redirect(redirectUrl);
                                }
                            } 
                        } 
                    }

                }
            }
        }






        //------------------------Billdesk-----------------
        public static HttpClientHandler GetProxySetting(string proxyUrl)
        {
            if (proxyUrl != "")
            {
                WebProxy proxy = new WebProxy
                {
                    Address = new Uri(proxyUrl)
                };

                HttpClientHandler handler = new HttpClientHandler()
                {
                    Proxy = proxy
                };

                return handler;
            }
            else
            { 
                HttpClientHandler handler = new HttpClientHandler(); 
                return handler;
            }
        }


        private string encryptFile(string textToEncrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.ECB;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText,
            0, plainText.Length));

        }


        //----------------------Rozer pay-----------------------

        public class CreateOrder
        {
            public int amount { get; set; }
            public string currency { get; set; }
            public string receipt { get; set; }
            public Dictionary<string, string> notes { get; set; }
        }

        public class CreatedOrder
        {
            //public string id { get; set; }
            //public string entity { get; set; }
            //public int amount { get; set; }
            //public int amount_paid { get; set; }
            //public int amount_due { get; set; }
            //public string currency { get; set; }
            //public string receipt { get; set; }
            //public object offer_id { get; set; }
            //public string status { get; set; }
            //public int attempts { get; set; }
            //public List<object> notes { get; set; }
            //public int created_at { get; set; }
            public int amount { get; set; }
            public int amount_due { get; set; }
            public int amount_paid { get; set; }
            public int attempts { get; set; }
            public long created_at { get; set; } // Use long for Unix timestamp
            public string currency { get; set; }
            public string entity { get; set; }
            public string id { get; set; }
            public Dictionary<string, string> notes { get; set; } // This matches the notes object in the JSON
            public object offer_id { get; set; } // Use object if it can be null
            public string receipt { get; set; }
            public string status { get; set; }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            if (ViewState["payFrom"].ToString() == "1")
            {
                Response.Redirect("Student_Monthly_Payment.aspx?regid=" + ViewState["regid"].ToString(), false);
            }
            else
            {
                Response.Redirect("../student-monthly-payment.aspx", false);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Bind_data();
        }
    }

    internal class PaymentRequest
    {
        public string merchantId { get; set; }
        public string aggregatorID { get; set; }
        public string merchantTxnNo { get; set; }
        public string amount { get; set; }
        public string currencyCode { get; set; }
        public string payType { get; set; }
        public string customerEmailID { get; set; }
        public string transactionType { get; set; }
        public string returnURL { get; set; }
        public string txnDate { get; set; }
        public string customerMobileNo { get; set; }
        public string customerName { get; set; }
        public string addlParam1 { get; set; }
        public string addlParam2 { get; set; } 
        public string secureHash { get; set; } 
    }



    internal class Paymentstatus
    {



        public string aggregatorID { get; set; }
        public string merchantId { get; set; }
        public string merchantTxnNo { get; set; }
        public string originalTxnNo { get; set; }
        public string transactionType { get; set; }
        public string secureHash { get; set; }


    }
}