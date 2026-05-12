using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Monthly_Payments.worldline
{
    public partial class Payment_Error_Message : System.Web.UI.Page
    {
        Payment_update_after_onlinepayment mypay = new Payment_update_after_onlinepayment();
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
                string month = (String)dc1["month"];
                string Total_pay = (String)dc1["Total_pay"];
                string parameter = (String)dc1["parameter"];
                string parameter_id = (String)dc1["parameter_id"];
                string Class_name = (String)dc1["Class_name"];
                string hosteltaken = (String)dc1["hosteltaken"];

                string hostel_id = (String)dc1["hostel_id"];
                string day_boarding = (String)dc1["day_boarding"];
                string day_boarding_lunch = (String)dc1["day_boarding_lunch"];
                string category_id = (String)dc1["category_id"];
                string sub_category_id = (String)dc1["sub_category_id"];
                string transportportation_id = (String)dc1["transportportation_id"];
                string group_id = (String)dc1["group_id"];
                string Section = (String)dc1["Section"];
                string Date = (String)dc1["Date"];
                string pay_idate1 = (String)dc1["Idate"];
                string totaldiscount = (String)dc1["totaldiscount"];
                string totallatefine = (String)dc1["totallatefine"];


                lbl_Order_id.Text = "Order Id:- " + ViewState["orderid"].ToString();
                lbl_ampunt.Text = "Amount :- " + My.toDouble(Total_pay).ToString("0.00");
                lbl_paymentdate.Text = "Date :-" + Date;

            }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Monthly_payment_student.aspx", false);
        }
    }
}