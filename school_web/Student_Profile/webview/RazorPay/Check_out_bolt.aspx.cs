using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.RazorPay
{
    public partial class Check_out_bolt : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sytemid"] == null)
                {
                }
                else
                {
                    hd_key.Value = My.MERCHANT_KEY();
                    Dictionary<string, object> dc2 = mycode.Firm_details();
                    ViewState["firm_name"] = (String)dc2["firm_name"];
                    ViewState["logo"] = (String)dc2["logo"];
                    ViewState["contact_no"] = (String)dc2["contact_no"];
                    ViewState["email"] = (String)dc2["email"];

                    hd_logo.Value = ViewState["logo"].ToString();
                    hd_fname.Value = ViewState["firm_name"].ToString();
                    Bind_data();
                }
            }
        }
        private void Bind_data()
        {
            string url = My.url();// "http://localhost:1199/";//
            string query = "Select * from Payment_transaction_process where ordertrackingid='" + Session["sytemid"] + "'  ";
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
                    Session["regid"] = dr["Admission_no"].ToString();
                    double amtForRazorrounD = Math.Round(Convert.ToDouble(dr["Total_pay"].ToString()));
                    int aftrrounD = (Convert.ToInt32(amtForRazorrounD) * 100);
                    hd_amount.Value = aftrrounD.ToString();
                    if (dr["Emailid"].ToString() == "")
                    {
                        hd_email.Value = dr["Emailid"].ToString();
                    }
                    else
                    {
                        hd_email.Value = ViewState["email"].ToString();
                    }

                    if (dr["Mobileno"].ToString() == "")
                    {
                        hd_mobile.Value = dr["Mobileno"].ToString();
                    }
                    else
                    {
                        hd_mobile.Value = ViewState["contact_no"].ToString();
                    }
                    hd_student_name.Value = dr["Name"].ToString();
                    hd_regid.Value = dr["Admission_no"].ToString();
                    hd_razor_odr_id.Value = dr["razorpay_order_id"].ToString();
                    ViewState["payFrom"] = dr["Pay_from"].ToString();
                    hd_order_id.Value = dr["ordertrackingid"].ToString();
                    callback_url.Value = url + "Student_Profile/webview/RazorPay/PayProcess_blank_callback.aspx";
                }
            }

        }
    }
}