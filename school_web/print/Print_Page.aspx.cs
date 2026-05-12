
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace LMS.print
{
    public partial class Print_Page : System.Web.UI.Page
    {
        UsesCode uc = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["PaymentSliP"] != null)
                    {
                        Bind_schoolinfo();
                        Session["registrstio_Id11"] = null;
                        string id = Request.QueryString["PaymentSliP"];
                        fetch_data(id);
                        fetch_taken_suubject(id);
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
            DataTable dt = uc.FillData("select * from Firm_Details ");
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
                My mycode = new My();
                lbl_session.Text = mycode.get_session(dt.Rows[0]["Session_id"].ToString());
                lbl_session1.Text = lbl_session.Text;
                lbl_registrationid.Text = dt.Rows[0]["Registration_id"].ToString();
                lbl_name.Text = dt.Rows[0]["Name"].ToString();
                lbl_gender.Text = dt.Rows[0]["Gender"].ToString().ToUpper();
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
                lbl_nationality.Text = dt.Rows[0]["Nationality"].ToString().ToUpper();
                lbl_religion.Text = dt.Rows[0]["Religion"].ToString().ToUpper();
                lbl_cast.Text = dt.Rows[0]["Cast"].ToString().ToUpper();
                lbl_category.Text = dt.Rows[0]["Category"].ToString().ToUpper();

                lbl_height.Text = dt.Rows[0]["Height"].ToString().ToUpper();
                lbl_weight.Text = dt.Rows[0]["Weight"].ToString().ToUpper();

                lbl_sb_name1.Text = dt.Rows[0]["Sibling_name1"].ToString();
                lbl_sb_age1.Text = dt.Rows[0]["Sibling_age1"].ToString();
                lbl_sb_school_name1.Text = dt.Rows[0]["Sibling_school1"].ToString();
                lbl_sb_class1.Text = dt.Rows[0]["Sibling_class1"].ToString();

                lbl_sb_name2.Text = dt.Rows[0]["Sibling_name2"].ToString();
                lbl_sb_age2.Text = dt.Rows[0]["Sibling_age2"].ToString();
                lbl_sb_school_name2.Text = dt.Rows[0]["Sibling_school2"].ToString();
                lbl_sb_class2.Text = dt.Rows[0]["Sibling_class2"].ToString();


                img_s_image.ImageUrl = dt.Rows[0]["Student_image"].ToString();
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
                ViewState["Student_email_id"] = dt.Rows[0]["Student_email_id"].ToString();
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
                lbl_Unpaid.Text = dt.Rows[0]["Payment_Status"].ToString();
                lbl_paymnet_mode.Text = dt.Rows[0]["Payment_mode"].ToString();

                qrcode.Visible = false;
                if (dt.Rows[0]["Payment_mode"].ToString() == "Online")
                {

                    if (dt.Rows[0]["payment_slip"].ToString() == "#")
                    {
                        lbl_payment_order_id.Text = dt.Rows[0]["razorpay_payment_id"].ToString();
                    }
                    else
                    {
                        lbl_paymnet_mode.Text = "Pay Via QR Code";
                        lbl_payment_order_id.Text = "Pay Via QR Code";
                        qrcode.Visible = true;
                        Image2.ImageUrl = dt.Rows[0]["payment_slip"].ToString();
                        lbl_payment_order_id.Text = "Pay Via QR Code";
                        lbl_Unpaid.Text = "Paid Via QR Code";
                    }

                }

                else if (dt.Rows[0]["Reg_payment_type"].ToString() == "1")
                {
                    if (dt.Rows[0]["Payment_Status"].ToString() == "Unpaid")
                    {
                        lbl_payment_order_id.Text = "NA";
                    }
                    else
                    {
                        lbl_payment_order_id.Text = "";
                    }
                }
                else
                {
                    lbl_payment_order_id.Text = dt.Rows[0]["razorpay_payment_id"].ToString() + " Transaction Type : " + dt.Rows[0]["razorpay_order_id"].ToString();
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
            }
            send_mail();



        }



        private void send_mail()
        { //My mycode = new My();

            string subject = "#OnlineApply- " + lbl_registrationid.Text;

            string ulr1 = My.URL() + "print/Print_Page.aspx?PaymentSliP=" + lbl_registrationid.Text;


            StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            Panel1.RenderControl(htmlWrite);
            string htmlStr = stringWrite.ToString() + "<br/><br/>Click here to  Print it <a href=" + ulr1 + " > link </a>";


            uc.sendemail(ViewState["Student_email_id"].ToString(), subject, htmlStr);
        }



        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Default.aspx", false);
        }
    }
}