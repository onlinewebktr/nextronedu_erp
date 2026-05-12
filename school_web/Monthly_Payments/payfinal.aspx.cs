using Newtonsoft.Json;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Monthly_Payments
{
    public partial class payfinal : System.Web.UI.Page
    {
        My mycode = new My();
        com.awl.MerchantToolKit.ReqMsgDTO objReqMsgDTO;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {

                    hd_logo.Value = My.get_logo();
                    hd_schoolname.Value = My.get_school_name();
                    ViewState["Getwey_Type"] = mycode.get_payment_Getwey_Type();
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    ViewState["orderid"] = Request.QueryString["orderid"].ToString();
                    Session["regid"] = ViewState["regid"].ToString();
                    Bind_data();



                }
                else
                {
                    Response.Redirect("Monthly_payment_student.aspx", false);
                }
            }
        }

        private void Bind_data()
        {
            string query = "Select * from Payment_transaction_process where Admission_no='" + ViewState["regid"].ToString() + "' and ordertrackingid='" + ViewState["orderid"].ToString() + "'";
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
                        My.exeSql("update Payment_transaction_process set razorpay_order_id='" + odrID + "' where ordertrackingid='" + ViewState["orderid"].ToString() + "'");
                        Session["sytemid"] = ViewState["orderid"].ToString();
                        Response.Redirect("RazorPay/Check_out.aspx", false);// call back 
                        

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
                            objReqMsgDTO.ResponseUrl = My.URL() + "Monthly_Payments/worldline/Response.aspx";
                            objReqMsgDTO.TrnRemarks = "Monthly Payment";
                            objReqMsgDTO.TrnCurrency = "INR";
                            objReqMsgDTO.AddField1 = dr["Admission_no"].ToString();
                            objReqMsgDTO.AddField2 = dr["Name"].ToString();
                            objReqMsgDTO.AddField3 = dr["Emailid"].ToString();
                            objReqMsgDTO.AddField4 = dr["Mobileno"].ToString();
                            objReqMsgDTO.AddField5 = "";
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
                            Response.Redirect("worldline/TrnPay_process.aspx", false);
                        }
                        catch
                        {
                        }



                    }

                }

            }
        }

        //----------------------Rozer pay-----------------------

        public class CreateOrder
        {
            public int amount { get; set; }
            public string currency { get; set; }
            public string receipt { get; set; }
        }

        public class CreatedOrder
        {
            public string id { get; set; }
            public string entity { get; set; }
            public int amount { get; set; }
            public int amount_paid { get; set; }
            public int amount_due { get; set; }
            public string currency { get; set; }
            public string receipt { get; set; }
            public object offer_id { get; set; }
            public string status { get; set; }
            public int attempts { get; set; }
            public List<object> notes { get; set; }
            public int created_at { get; set; }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Student_Monthly_Payment.aspx?regid=" + ViewState["regid"].ToString(), false);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Bind_data();
        }
    }
}