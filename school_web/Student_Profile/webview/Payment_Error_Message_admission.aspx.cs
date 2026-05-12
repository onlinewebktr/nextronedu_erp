using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class Payment_Error_Message_admission : System.Web.UI.Page
    {
        Payment_update_after_onlinepayment_Admission mypay = new Payment_update_after_onlinepayment_Admission();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["orderid"] != null)
            {
                ViewState["orderid"] = Request.QueryString["orderid"].ToString();
                ViewState["Regid"] = Request.QueryString["Regid"].ToString();

                Dictionary<string, object> dc1 = mypay.getstudentinfo(ViewState["Regid"].ToString(), ViewState["orderid"].ToString());
                string Name = (String)dc1["Name"];
                string Class_id = (String)dc1["Class_id"];
                string Session_id = (String)dc1["Session_id"];
                string Session = (String)dc1["Session"];
                string Total_pay = (String)dc1["Total_pay"];
                string Payment_type = (String)dc1["Payment_type"];
                string category_id = (String)dc1["category_id"];
                string sub_category_id = (String)dc1["sub_category_id"];
                string Date = (String)dc1["Date"];
                string pay_idate1 = (String)dc1["Idate"];
                string hostaltaken = (String)dc1["hosteltaken"];
                int pay_idate = Convert.ToInt32(pay_idate1);
                string Branch_id = (String)dc1["Branch_id"];
                string Section = (String)dc1["Section"];
                string razorpay_payment_id = (String)dc1["razorpay_payment_id"];
                ViewState["payFrom"] = (String)dc1["payFrom"];
                lbl_Order_id.Text = "Order Id:- " + ViewState["orderid"].ToString();
                lbl_ampunt.Text = "Amount :- " + My.toDouble(Total_pay).ToString("0.00");
                lbl_paymentdate.Text = "Date :-" + Date;
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            
          

            try
            {
                Session["User"] = ViewState["Regid"].ToString();
                string url = "";
                if (ViewState["payFrom"].ToString() == "1")
                {
                    url = My.URL() + "Student_Profile/webview/Student_Annual_Payment.aspx?regid=" + ViewState["Regid"].ToString();
                    Response.Redirect(url, false);
                }
                else
                {


                    url = My.URL() + "Student_Profile/student-Annual-payment.aspx";


                    Response.Redirect(url, false);
                }
            }
            catch
            {

            }


        }
    }
}