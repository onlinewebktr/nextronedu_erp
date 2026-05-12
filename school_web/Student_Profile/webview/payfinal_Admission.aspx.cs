using getepay_sdk;
using Newtonsoft.Json;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class payfinal_Admission : System.Web.UI.Page
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
                    ViewState["Getwey_Type"] = mycode.get_payment_Getwey_Type();
                    Session["regid"] = ViewState["regid"].ToString();
                     
                    Bind_data();
                }
                else
                {
                    if (Request.QueryString["payFrom"].ToString() == "1")
                    {
                        Response.Redirect("Student_Annual_Payment.aspx?regid=" + ViewState["regid"].ToString(), false);
                    }
                    else
                    {
                        Response.Redirect("../student-Annual-payment.aspx?regid=" + ViewState["regid"].ToString(), false);
                    }
                }
            }
        }

        private void Bind_data()
        {
            string query = "Select *,(select top 1 rollnumber from  admission_registor where admissionserialnumber=Payment_transaction_process_Admission.Admission_no and Session_id=Payment_transaction_process_Admission.Session_id and Class_id=Payment_transaction_process_Admission.Class_id) as rollno,(select top 1 class from  admission_registor where admissionserialnumber=Payment_transaction_process_Admission.Admission_no and Session_id=Payment_transaction_process_Admission.Session_id and Class_id=Payment_transaction_process_Admission.Class_id) as Class_name from Payment_transaction_process_Admission where Admission_no='" + ViewState["regid"].ToString() + "' and ordertrackingid='" + ViewState["orderid"].ToString() + "'";
            // string query = "Select * from Payment_transaction_process_Admission where Admission_no='" + ViewState["regid"].ToString() + "' and ordertrackingid='" + ViewState["orderid"].ToString() + "'";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
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
                { "Class",  dr["Class_name"].ToString() },
                             { "Section",dr["Section"].ToString() },
                              { "Roll No.", dr["rollno"].ToString() },
                               { "Admission No.",  dr["Admission_no"].ToString() },{ "Order id",ViewState["orderid"].ToString() }
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

                        My.exeSql("update Payment_transaction_process_Admission set razorpay_order_id='" + odrID + "',Pay_from='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        Session["sytemid"] = ViewState["orderid"].ToString();
                        Response.Redirect("RazorPay/Check_out_Admission_annual.aspx", false);// call back 

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
                { "Class",  dr["Class_name"].ToString() },
                             { "Section",dr["Section"].ToString() },
                              { "Roll No.", dr["rollno"].ToString() },
                               { "Admission No.",  dr["Admission_no"].ToString() }
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

                        My.exeSql("update Payment_transaction_process_Admission set razorpay_order_id='" + odrID + "',Pay_from='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        Session["sytemid"] = ViewState["orderid"].ToString();
                        Response.Redirect("RazorPay/Check_out_admbolt.aspx", false);// call back 

                    }
                    else if (ViewState["Getwey_Type"].ToString() == "Worldline")
                    {

                        try
                        {
                            objReqMsgDTO = new com.awl.MerchantToolKit.ReqMsgDTO();
                            objReqMsgDTO.OrderId = dr["ordertrackingid"].ToString();
                            objReqMsgDTO.Mid = My.MERCHANT_KEY();
                            objReqMsgDTO.Enckey = My.autkeyrozorkey();
                            objReqMsgDTO.MeTransReqType = "S";

                            double amt = Convert.ToDouble(dr["Total_pay"].ToString()) * 100;
                            objReqMsgDTO.TrnAmt = amt.ToString();
                            // objReqMsgDTO.TrnAmt = "100";

                            objReqMsgDTO.RecurrPeriod = "";
                            objReqMsgDTO.RecurrDay = "";
                            objReqMsgDTO.ResponseUrl = My.URL() + "Student_Profile/webview/worldline/Response_admission.aspx";
                            objReqMsgDTO.TrnRemarks = dr["Payment_type"].ToString();
                            objReqMsgDTO.TrnCurrency = "INR";
                            objReqMsgDTO.AddField1 = dr["Admission_no"].ToString();
                            objReqMsgDTO.AddField2 = dr["Name"].ToString();
                            objReqMsgDTO.AddField3 = dr["Emailid"].ToString();
                            objReqMsgDTO.AddField4 = dr["Mobileno"].ToString();
                            objReqMsgDTO.AddField5 = ViewState["payFrom"].ToString();
                            objReqMsgDTO.AddField6 = "";
                            objReqMsgDTO.AddField7 = "";
                            objReqMsgDTO.AddField8 = "";
                            string Message;
                            com.awl.MerchantToolKit.AWLMEAPI objawlmerchantkit = new com.awl.MerchantToolKit.AWLMEAPI();
                            objawlmerchantkit.generateTrnReqMsg(objReqMsgDTO);
                            Message = objReqMsgDTO.ReqMsg;
                            Session["response"] = objReqMsgDTO;
                            Session["Message"] = Message;
                            Session["MID"] = objReqMsgDTO.Mid;
                            Response.Redirect("worldline/TrnPay_process_admission.aspx", false);// running

                            // Response.Redirect("worldline/Response_admission.aspx", false);// testing 

                        }
                        catch
                        {
                        }

                    }

                    #region Sabpaisa
                    else if (ViewState["Getwey_Type"].ToString() == "SabPaisa")//SabPaisa
                    {
                        Session["sytemid"] = ViewState["orderid"].ToString();
                        Response.Redirect("Sabpaisa/Poast_Payment_ad_an.aspx?regid=" + ViewState["regid"].ToString() + "&orderid=" + ViewState["orderid"].ToString(), false);// call back  


                    }
                    #endregion

                    #region Get_E_Pay
                    else if (ViewState["Getwey_Type"].ToString() == "Getepay")//Getepay
                    {
                        Dictionary<string, object> dc2 = My.get_student_info(dr["Admission_no"].ToString(), dr["Session"].ToString());
                        string studentname = (String)dc2["studentname"];
                        string Admission_no = (String)dc2["Admission_no"];
                        string Session = (String)dc2["Session"];
                        string classname = (String)dc2["classname"];
                        string rollnumber = (String)dc2["rollnumber"];
                        string Address = (String)dc2["Address"];
                        string email_id = (String)dc2["email_id"];

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
                        request.amount = dr["Total_pay"].ToString();// "1.00"; //dr["Total_pay"].ToString();//"51.00";
                        request.merchantTransactionId = dr["ordertrackingid"].ToString();//"Sample07042023-4";
                        request.transactionDate = My.getdate3();//"2023-04-07 16:16:16";
                        request.terminalId = config.terminalId;
                        request.udf1 = dr["Mobileno"].ToString();//"9999999999";
                        request.udf2 = dr["Emailid"].ToString();
                        request.udf3 = studentname;
                        request.udf4 = dr["Admission_no"].ToString();//"9999999999";
                        request.udf5 = dr["Payment_type"].ToString();
                        request.udf6 = Session;
                        request.udf7 = classname;
                        request.udf8 = "";
                        request.udf9 = "";//"9999999999";
                        request.udf10 = "";
                        request.ru = My.URL() + "Student_Profile/webview/Get_Epay/Response_ad_an.aspx";//"http://localhost:63645/default";
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
                        My.exeSql("update Payment_transaction_process_Admission set razorpay_order_id='" + getpay_id + "',payFrom='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        string paymentUrl = orderResponse.paymentUrl;
                        Response.Redirect(paymentUrl, false);



                    }
                    #endregion

                    #region new Worldline
                    else if (ViewState["Getwey_Type"].ToString() == "Worldline New")
                    {
                        Response.Redirect("worldline/Request_new_worldline_Admission_Annual.aspx?regid=" + ViewState["regid"].ToString() + "&ordertrackingid=" + ViewState["orderid"].ToString(), false);


                    }
                    #endregion

                    #region EGPay
                    else if (ViewState["Getwey_Type"].ToString() == "EGPay")//egpay
                    {
                        My.exeSql("update Payment_transaction_process_Admission set razorpay_order_id='" + dr["ordertrackingid"].ToString() + "',Pay_from='" + ViewState["payFrom"].ToString() + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
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
                            Mobile_No = "9088399919";
                        }
                        if (email_id == "")
                        {
                            email_id = "erp.edunextg@gmail.com";
                        }
                        if (city == "")
                        {
                            city = "WB";
                        }
                        string Session_name = session.Trim();
                        //redirecturl += "https://eazypayuat.icicibank.com/EazyPG?";//local
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




                        string returnurl = My.URL() + "Student_Profile/webview/EazyPay/Response_adm.aspx";
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
                            My.exeSql("update Payment_transaction_process_Admission set Plan_url='" + redirecturl + "',Encrypted_url='" + encryptredirecturl + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        }
                        catch (Exception ex)
                        {

                        }



                        Response.Redirect(encryptredirecturl, false);

                    }
                   
                    #endregion

                }

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
}