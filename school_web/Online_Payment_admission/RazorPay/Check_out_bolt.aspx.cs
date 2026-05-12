using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Payment_admission.RazorPay
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

            string query = "Select * from Online_Admission where Order_id='" + Session["sytemid"] + "'  ";
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
                    Session["regid"] = dr["Registration_id"].ToString();
                    double amtForRazorrounD = Math.Round(Convert.ToDouble(dr["Payable_amount"].ToString()));
                    int aftrrounD = (Convert.ToInt32(amtForRazorrounD) * 100);

                    hd_amount.Value = aftrrounD.ToString();

                    if (dr["Student_email_id"].ToString() == "")
                    {
                        hd_email.Value = "apskhankitta.sabour@gmail.com";
                    }
                    else
                    {
                        hd_email.Value = dr["Student_email_id"].ToString();  
                    }

                    if (dr["Student_mob_no"].ToString() == "")
                    {
                        hd_mobile.Value = "";
                    }
                    else
                    {
                        hd_mobile.Value = dr["Student_mob_no"].ToString(); 
                    }


                    hd_student_name.Value = dr["Name"].ToString();
                    hd_regid.Value = dr["Registration_id"].ToString();
                    hd_razor_odr_id.Value = dr["razorpay_order_id"].ToString();
                  
                    hd_order_id.Value = dr["Order_id"].ToString();
                    callback_url.Value = My.url() + "Online_Payment_admission/RazorPay/PayProcess_blank_callback.aspx";

                    //amount.Value = aftrrounD.ToString();
                    //email.Value = dr["Student_email_id"].ToString();
                    //contact.Value = dr["Student_mob_no"].ToString();
                    //transaction_id.Value = dr["Registration_id"].ToString();
                    //order_id.Value = dr["razorpay_order_id"].ToString();
                    //callback_url.Value = My.url() + "Online_Payment_admission/RazorPay/PayProcess_blank_callback.aspx";

                }
            }
        }
    }
}