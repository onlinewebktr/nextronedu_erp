using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Scholarship
{
    public partial class Print_Page_Seat_full : System.Web.UI.Page
    {
        UsesCode My = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["PaymentSliP"] != null)
                    {
                        Session["registrstio_Id11"] = null;
                        string id = Request.QueryString["PaymentSliP"];
                        Bind_schoolinfo();
                        fetch_data(id);
                    }
                    else
                    {
                        // Response.Redirect("/Default.aspx");
                    }
                }
            }
            catch (Exception exe)
            {

            }
        }

        private void Bind_schoolinfo()
        {
            var dt = My.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_email_school.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
            }
        }

        private void fetch_data(string id)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Scholarship_Admission where Registration_id='" + id + "' ", connection.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Online_Admission");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("/Default.aspx");
            }
            else
            {

                My mycode = new My();
                lbl_regid.Text = dt.Rows[0]["Registration_id"].ToString();
                lbl_session.Text = mycode.get_session(dt.Rows[0]["Session_id"].ToString());
                lbl_session1.Text = lbl_session.Text;
                lbl_name.Text = dt.Rows[0]["Name"].ToString();
                lbl_class.Text = dt.Rows[0]["Class"].ToString();
                lbl_fathername.Text = dt.Rows[0]["Father_name"].ToString();
                lbl_date.Text = dt.Rows[0]["Date"].ToString();
                lbl_total.Text = dt.Rows[0]["Payable_amount"].ToString();
                lbl_mobile_no.Text = dt.Rows[0]["Student_mob_no"].ToString();
                lbl_email_id.Text = dt.Rows[0]["Student_email_id"].ToString();
                lbl_paymnet_mode.Text = dt.Rows[0]["Payment_mode"].ToString();

                lbl_payment_order_id.Text = dt.Rows[0]["razorpay_payment_id"].ToString() + " order Id : " + dt.Rows[0]["razorpay_order_id"].ToString();

            }
        }
    }
}