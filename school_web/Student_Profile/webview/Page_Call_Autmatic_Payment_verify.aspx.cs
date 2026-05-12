using school_web.AppCode;
using school_web.Student_Profile.webview.EazyPay;
using school_web.Student_Profile.webview.RazorPay;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class Page_Call_Autmatic_Payment_verify_update_data : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    Dictionary<string, object> dc1 = My.get_selected_studentinfo(Session["regid"].ToString(), ViewState["sessionid"].ToString(), "1");
                    string Class_Id = (String)dc1["Class_id"];
                    string section = (String)dc1["Section"];
                    ViewState["sessionid"] = My.get_session_id();
                    ViewState["Getwey_Type"] = mycode.get_payment_Getwey_Type_monthly(Class_Id);

                    if (ViewState["Getwey_Type"].ToString() != "0")
                    {
                        bool status_paymnet = mycode.bind_not_paymnet_datat(ViewState["regid"].ToString(), ViewState["sessionid"].ToString());
                        if (status_paymnet == false)
                        {

                        }
                        {

                            automatic_payment();
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();
        private void automatic_payment()
        {
            string order_id = "";
            string Status = "";
            string Payment_order_id = "";
            string query = " select  top  1 * from dbo.[Payment_transaction_process] where Admission_no='" + ViewState["regid"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "' and status='Pending' order by Id desc ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {


            }
            else
            {

                if (ViewState["Getwey_Type"].ToString() == "Razorpay")
                {
                    Dictionary<string, object> dc1 = Re_payment_check_payment.get_payment_status_Razorpay(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), dt.Rows[0]["razorpay_order_id"].ToString());
                    order_id = (String)dc1["order_id"];
                    Status = (String)dc1["Status"];
                    Payment_order_id = (String)dc1["Payment_order_id"];
                }
                else if (ViewState["Getwey_Type"].ToString() == "EGPay")
                {
                    Dictionary<string, object> dc1 = Re_payment_check_payment_EZpay.get_payment_status_EZ_Pay(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                    order_id = (String)dc1["order_id"];
                    Status = (String)dc1["Status"];
                    Payment_order_id = (String)dc1["Payment_order_id"];
                }
                if (Status == "Success")
                {

                    My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + Payment_order_id + "',status='Success'  where  razorpay_order_id='" + order_id + "'");
                    bool status = mypayment.save_final_payment(dt.Rows[0]["Admission_no"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                    if (status == true)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + Payment_order_id + "',status='Response Not Found'  where  razorpay_order_id='" + order_id + "'");

                }

            }

        }
    }
}