
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace LMS.print
{
    public partial class unsuccess : System.Web.UI.Page
    {
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
                        fetch_data(id);
                    }
                    else
                    {
                        Response.Redirect("/Default.aspx");
                    }
                }
            }
            catch (Exception exe)
            {
                
            }
        }

        private void fetch_data(string id)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Online_Admission where Registration_id='" + id + "'", connection.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Online_Admission");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("/Default.aspx");
            }
            else
            {
                lbl_registrationid.Text = dt.Rows[0]["Registration_id"].ToString();
                lbl_name.Text = dt.Rows[0]["Name"].ToString();
                lbl_gender.Text = dt.Rows[0]["Gender"].ToString();
                lbl_dob.Text = dt.Rows[0]["DOB"].ToString();
                lbl_class.Text = dt.Rows[0]["Class"].ToString();
                lbl_lastschool.Text = dt.Rows[0]["Last_school"].ToString();
                lbl_fathername.Text = dt.Rows[0]["Father_name"].ToString();
                lbl_mothername.Text = dt.Rows[0]["Mother_name"].ToString();
                lbl_adress.Text = dt.Rows[0]["Address"].ToString();
                lbl_city.Text = dt.Rows[0]["City"].ToString();
                lbl_pincode.Text = dt.Rows[0]["Pincode"].ToString();
                lbl_student_mobile.Text = dt.Rows[0]["Student_mobile"].ToString();
                lbl_email.Text = dt.Rows[0]["Email"].ToString();
                lbl_date.Text = dt.Rows[0]["Date"].ToString();


                lbl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                lbl_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                lbl_religion.Text = dt.Rows[0]["Religion"].ToString();
                lbl_cast.Text = dt.Rows[0]["Cast"].ToString();
                lbl_category.Text = dt.Rows[0]["Category"].ToString();
                lbl_father_mobile.Text = dt.Rows[0]["Father_mobile"].ToString();
                lbl_mother_mobile.Text = dt.Rows[0]["Mother_mobile"].ToString();
                lbl_no_for_use_of_sms.Text = dt.Rows[0]["Mobile_for_sms"].ToString();
                lbl_whatsapp_no.Text = dt.Rows[0]["Whatsapp_no"].ToString();
                img_s_image.ImageUrl = dt.Rows[0]["Student_image"].ToString();
                img_s_sig.ImageUrl = dt.Rows[0]["Student_signature"].ToString();

                lbl_admissiontype.Text = dt.Rows[0]["Services"].ToString();
                lbl_total.Text = dt.Rows[0]["Payable_amount"].ToString();
                lbl_paybaltype.Text = dt.Rows[0]["Pay_Type"].ToString();
                lbl_tranid.Text = dt.Rows[0]["Pay_orderid"].ToString();
            }
        }



        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Default.aspx", false);
        }
    }
}