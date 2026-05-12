using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class online_reg : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["regiDs"] != null)
                    {
                        Session["registrstio_Id11"] = null;
                        string id = Request.QueryString["regiDs"];
                        fetch_data(id); Bind_schoolinfo();
                        fetch_taken_suubject(id);
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

        private void fetch_taken_suubject(string regiD)
        {
            subject_div.Visible = false;
            try
            {
                DataTable dt = My.dataTable("select * from Online_reg_taken_subject where Registration_id='" + regiD + "'");
                if (dt.Rows.Count > 0)
                {
                    string subjectS = "";
                    subject_div.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        subjectS = subjectS + dr["Subject_name"].ToString() + ", ";
                    }
                    lbl_subject_taken.Text = subjectS.Remove(subjectS.Length - 2);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
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
            SqlDataAdapter ad = new SqlDataAdapter("select * from Online_Admission where Registration_id='" + id + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Online_Admission");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("/Default.aspx");
            }
            else
            {
                lbl_session.Text = mycode.get_session(dt.Rows[0]["Session_id"].ToString());
                lbl_session1.Text = lbl_session.Text;
                lbl_registrationid.Text = dt.Rows[0]["Registration_id"].ToString();
                lbl_name.Text = dt.Rows[0]["Name"].ToString();
                lbl_gender.Text = dt.Rows[0]["Gender"].ToString();
                lbl_dob.Text = dt.Rows[0]["DOB"].ToString();
                lbl_class.Text = dt.Rows[0]["Class"].ToString();
                lbl_lastschool.Text = dt.Rows[0]["Last_school"].ToString();
                lbl_fathername.Text = dt.Rows[0]["Father_name"].ToString();
                lbl_mothername.Text = dt.Rows[0]["Mother_name"].ToString();
                lbl_adress.Text = dt.Rows[0]["Persent_adress"].ToString();
                lbl_city.Text = dt.Rows[0]["Persent_city"].ToString();
                lbl_pincode.Text = dt.Rows[0]["Persent_pincode"].ToString();

                lbl_poS.Text = dt.Rows[0]["Persent_po"].ToString();
                lbl_district.Text = dt.Rows[0]["Persent_district"].ToString();

                lbl_date.Text = dt.Rows[0]["Date"].ToString();
                lbl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                lbl_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                lbl_religion.Text = dt.Rows[0]["Religion"].ToString();
                lbl_cast.Text = dt.Rows[0]["Cast"].ToString();
                lbl_category.Text = dt.Rows[0]["Category"].ToString();



                img_s_sig.ImageUrl = dt.Rows[0]["Student_signature"].ToString();








                lbl_admissiontype.Text = dt.Rows[0]["Services"].ToString();
                lbl_total.Text = dt.Rows[0]["Payable_amount"].ToString();
                // lbl_paybaltype.Text = dt.Rows[0]["Pay_Type"].ToString();
                //lbl_tranid.Text = dt.Rows[0]["Pay_orderid"].ToString();
                lbl_lunch.Text = dt.Rows[0]["Is_Lunch"].ToString();
                if (lbl_admissiontype.Text == "Hosteler")
                {
                    islunch.Visible = false;
                }
                else if (lbl_admissiontype.Text == "Day Boarding")
                {
                    islunch.Visible = true;
                }
                else
                {
                    islunch.Visible = false;
                }


                lbl_occupation.Text = dt.Rows[0]["Father_occupation"].ToString();
                lbl_qualification.Text = dt.Rows[0]["Father_qualitication"].ToString();
                lbl_designation.Text = dt.Rows[0]["Father_designation"].ToString();
                lbl_mobile_no.Text = dt.Rows[0]["Country_Code_Father"].ToString() + dt.Rows[0]["Father_mobile"].ToString();
                lbl_email_id.Text = dt.Rows[0]["Email"].ToString();
                lbl_annual_income.Text = dt.Rows[0]["Father_annual_income"].ToString();

                lbl_is_devorced.Text = dt.Rows[0]["If_parents_are_divorced"].ToString();
                lbl_child_live_with.Text = dt.Rows[0]["Child_live_with"].ToString();
                lbl_divorced_guardian_name.Text = dt.Rows[0]["Name_of_child_lives_with"].ToString();
                if (dt.Rows[0]["If_parents_are_divorced"].ToString() == "Yes")
                {
                    childlivewith.Visible = true;
                    if (dt.Rows[0]["Child_live_with"].ToString() == "Other")
                    {
                        withwhomthechildlives.Visible = true;
                    }
                    else
                    {
                        withwhomthechildlives.Visible = false;
                    }
                }
                else if (dt.Rows[0]["If_parents_are_divorced"].ToString() == "No")
                {
                    childlivewith.Visible = false;
                    withwhomthechildlives.Visible = false;
                }
                else
                {
                    Ifparentsaredivorced.Visible = false;
                    childlivewith.Visible = false;
                    withwhomthechildlives.Visible = false;
                }


                lbl_mon_occupation.Text = dt.Rows[0]["Mother_occupation"].ToString();
                lbl_mon_qualification.Text = dt.Rows[0]["Mother_qualification"].ToString();
                lbl_mom_designation.Text = dt.Rows[0]["Mother_designation"].ToString();
                lbl_mom_mobile.Text = dt.Rows[0]["Country_Code_Mother"].ToString() + dt.Rows[0]["Mother_mobile"].ToString();
                lbl_mom_email.Text = dt.Rows[0]["Mother_emailid"].ToString();
                lbl_mom_income.Text = dt.Rows[0]["Mother_income"].ToString();
                lbl_states.Text = dt.Rows[0]["Persent_state"].ToString();



                lbl_paymnet_mode.Text = dt.Rows[0]["Payment_mode"].ToString();



                if (dt.Rows[0]["Payment_mode"].ToString() == "Online")
                {
                    if (dt.Rows[0]["payment_slip"].ToString() == "#")
                    {
                        lbl_payment_order_id.Text = dt.Rows[0]["razorpay_payment_id"].ToString();
                    }
                    else
                    {
                        Image2.ImageUrl = dt.Rows[0]["payment_slip"].ToString();
                        if (dt.Rows[0]["razorpay_payment_id"].ToString() == "NA")
                        {
                            lbl_payment_order_id.Text = "Pay Via QR Code";
                        }
                        else
                        {
                            lbl_payment_order_id.Text = dt.Rows[0]["razorpay_payment_id"].ToString();
                        }
                        lbl_Unpaid.Text = "Paid Via QR Code";
                    }
                }
                else if (dt.Rows[0]["Payment_mode"].ToString().ToUpper() == "UPI")
                {
                    if (dt.Rows[0]["payment_slip"].ToString() == "#")
                    {
                        lbl_payment_order_id.Text = dt.Rows[0]["razorpay_payment_id"].ToString();
                    }
                    else
                    {
                        Image2.ImageUrl = dt.Rows[0]["payment_slip"].ToString();
                        if (dt.Rows[0]["razorpay_payment_id"].ToString() == "NA")
                        {
                            lbl_payment_order_id.Text = "Pay Via QR Code";
                        }
                        else
                        {
                            lbl_payment_order_id.Text = dt.Rows[0]["razorpay_payment_id"].ToString();
                        }
                        lbl_Unpaid.Text = "Paid Via QR Code";
                    }
                }

                else
                {
                    if (dt.Rows[0]["Payment_mode"].ToString() == "Offline")
                    {
                        lbl_payment_order_id.Text = "";
                        lbl_Unpaid.Text = "NotPaid";
                    }
                    else
                    {
                        lbl_Unpaid.Text = dt.Rows[0]["Payment_Status"].ToString();

                        lbl_payment_order_id.Text = "";

                    }
                    //lbl_payment_order_id.Text = "NotPaid";//dt.Rows[0]["razorpay_payment_id"].ToString()+" Transaction Type : "+ dt.Rows[0]["razorpay_order_id"].ToString();
                }


                if (dt.Rows[0]["father_signature"].ToString() != "")
                {
                    img_father_sig.Visible = true;
                    img_father_sig.ImageUrl = dt.Rows[0]["father_signature"].ToString();
                }
                else
                {
                    img_father_sig.Visible = false;
                }

                if (dt.Rows[0]["father_photo"].ToString() != "")
                {
                    img_fathers_image.Visible = true;
                    img_fathers_image.ImageUrl = dt.Rows[0]["father_photo"].ToString();
                }
                else
                {
                    img_fathers_image.Visible = false;
                }



                if (dt.Rows[0]["mother_signature"].ToString() != "")
                {
                    img_mother_signature.Visible = true;
                    img_mother_signature.ImageUrl = dt.Rows[0]["mother_signature"].ToString();
                }
                else
                {
                    img_mother_signature.Visible = false;
                }


                if (dt.Rows[0]["mother_photo"].ToString() != "")
                {
                    img_mother_photo.Visible = true;
                    img_mother_photo.ImageUrl = dt.Rows[0]["mother_photo"].ToString();
                }
                else
                {
                    img_mother_photo.Visible = false;
                }


                if (dt.Rows[0]["Student_image"].ToString() != "")
                {
                    img_s_image.Visible = true;
                    img_s_image.ImageUrl = dt.Rows[0]["Student_image"].ToString();
                }
                else
                {
                    img_s_image.Visible = false;
                }




                ///====================================== 
                ///
                if (dt.Rows[0]["Parents_sign"].ToString() != "")
                {
                    img_parentSignDV.Visible = true;
                    img_parents_sign.ImageUrl = dt.Rows[0]["Parents_sign"].ToString();
                }
                else
                {
                    img_parentSignDV.Visible = false;
                }

                if (dt.Rows[0]["Family_photo"].ToString() != "")
                {
                    img_family_photoDV.Visible = true;
                    img_family_photo.ImageUrl = dt.Rows[0]["Family_photo"].ToString();
                }
                else
                {
                    img_family_photoDV.Visible = false;
                }
                if (dt.Rows[0]["birth_certificate"].ToString() != "")
                {
                    img_birth_cartificateDv.Visible = true;
                    img_birth_cartificate.ImageUrl = dt.Rows[0]["birth_certificate"].ToString();
                }
                else
                {
                    img_birth_cartificateDv.Visible = false;
                }
                if (dt.Rows[0]["residential_certificate"].ToString() != "")
                {
                    img_residentialDV.Visible = true;
                    img_residential.ImageUrl = dt.Rows[0]["residential_certificate"].ToString();
                }
                else
                {
                    img_residentialDV.Visible = false;
                }

                if (dt.Rows[0]["payment_slip"].ToString() != "" && dt.Rows[0]["payment_slip"].ToString() != "#")
                {
                    paymentslip.Visible = true;
                    Image2.ImageUrl = dt.Rows[0]["payment_slip"].ToString();
                }
                else
                {
                    paymentslip.Visible = false;
                }



            }
        }







        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../online-registration.aspx", false);
        }
    }
}