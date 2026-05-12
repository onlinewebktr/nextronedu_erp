using Newtonsoft.Json;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static school_web.Monthly_Payments.payfinal;

namespace school_web.Scholarship
{
    public partial class Scholarship_preview : System.Web.UI.Page
    {
        com.awl.MerchantToolKit.ReqMsgDTO objReqMsgDTO;
        UsesCode mycode = new UsesCode();
        My imp = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["Getwey_Type"] = imp.get_payment_Getwey_Type();
                    fetch_company_name();
                    if (Request.QueryString["regNo"] != null)
                    {
                        string regiD = Request.QueryString["regNo"];

                        string sessionid = My.get_session_id_onlinereg();
                        ViewState["Session_id"] = sessionid;
                        My.exeSql("update Scholarship_Admission set Session_id='" + ViewState["Session_id"].ToString() + "' where Registration_id='" + regiD + "'");
                        fetch_preview(regiD);
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

        private void fetch_company_name()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            { }
            else
            {
                lbl_schoolname.Text = dt.Rows[0]["firm_name"].ToString();
                school_logo.Src = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();

                schBranchDv.Visible = false;
                lbl_address_2.Visible = false;
                try
                {
                    if (dt.Rows[0]["Is_2nd_address"].ToString() == "True")
                    {
                        schBranchDv.Visible = true;
                        lbl_address_2.Visible = true;
                        lbl_address_2.Text = dt.Rows[0]["address2"].ToString();
                    }
                }
                catch (Exception ex)
                {
                }
            }

        }
        private void fetch_preview(string regiD)
        {
            DataTable dt = mycode.FillData("Select * from Scholarship_Admission where Registration_id='" + regiD + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                ViewState["Reg_payment_type"] = "0";
                if (dt.Rows[0]["Reg_payment_type"].ToString() == "1")
                {
                    pnl_payment_dv.Visible = false;
                    ViewState["Reg_payment_type"] = "1";
                }


                ViewState["testid"] = dt.Rows[0]["Test_id"].ToString();
                ViewState["Session_id"] = dt.Rows[0]["Session_id"].ToString();
                ViewState["Student_email_id"] = dt.Rows[0]["Student_email_id"].ToString();
                ViewState["Student_mob_no"] = dt.Rows[0]["Student_mob_no"].ToString();
                ViewState["Class_id"] = dt.Rows[0]["Class_id"].ToString();

                ViewState["Name"] = dt.Rows[0]["Name"].ToString();
                ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                ViewState["Order_id"] = dt.Rows[0]["Order_id"].ToString();
                ViewState["Payable_amount"] = dt.Rows[0]["Payable_amount"].ToString();
                //=====2

                lbl_selected_branch.Text = dt.Rows[0]["School_branch_address"].ToString();

                txt_first_name.Text = dt.Rows[0]["Student_first_name"].ToString();
                txt_middle_name.Text = dt.Rows[0]["Student_middle_name"].ToString();
                txt_last_name.Text = dt.Rows[0]["Student_last_name"].ToString();
                txt_dob.Text = dt.Rows[0]["DOB"].ToString();
                lbl_gender.Text = dt.Rows[0]["Gender"].ToString().ToUpper();
                txt_nationality.Text = dt.Rows[0]["Nationality"].ToString().ToUpper();
                txt_blood_group.Text = dt.Rows[0]["Blood_group"].ToString().ToUpper();
                lbl_religion.Text = dt.Rows[0]["Religion"].ToString().ToUpper();

                lbl_category.Text = dt.Rows[0]["Category"].ToString().ToUpper();
                txt_aadharcarno.Text = dt.Rows[0]["Aadhar_no"].ToString();
                txt_identification_marks.Text = dt.Rows[0]["Identification_marks"].ToString();
                lbl_adm_type.Text = dt.Rows[0]["Services"].ToString().ToUpper();

                lbl_class.Text = dt.Rows[0]["Class"].ToString();
                txt_reg_fees.Text = dt.Rows[0]["Payable_amount"].ToString();


                lbl_height.Text = dt.Rows[0]["Height"].ToString();
                lbl_weight.Text = dt.Rows[0]["Weight"].ToString();

                lbl_sb_name1.Text = dt.Rows[0]["Sibling_name1"].ToString();
                lbl_sb_age1.Text = dt.Rows[0]["Sibling_age1"].ToString();
                lbl_sb_school1.Text = dt.Rows[0]["Sibling_school1"].ToString();
                lbl_sb_class1.Text = dt.Rows[0]["Sibling_class1"].ToString();

                lbl_sb_name2.Text = dt.Rows[0]["Sibling_name2"].ToString();
                lbl_sb_age2.Text = dt.Rows[0]["Sibling_age2"].ToString();
                lbl_sb_school2.Text = dt.Rows[0]["Sibling_school2"].ToString();
                lbl_sb_class2.Text = dt.Rows[0]["Sibling_class2"].ToString();
                //============3


                //===============3
                txt_lastschool.Text = dt.Rows[0]["Last_school"].ToString();
                txt_board.Text = dt.Rows[0]["Last_school_board"].ToString();
                txt_passout_classs.Text = dt.Rows[0]["Passout_classs"].ToString();
                txt_percentage.Text = dt.Rows[0]["Percentage"].ToString();
                txt_reason_for_shift.Text = dt.Rows[0]["Reason_for_shift"].ToString();
                txt_year.Text = dt.Rows[0]["Passed_year"].ToString();
                //================3


                //=================4
                txt_father_first_name.Text = dt.Rows[0]["Father_first_name"].ToString();
                txt_father_middle_name.Text = dt.Rows[0]["Father_middle_name"].ToString();
                txt_father_last_name.Text = dt.Rows[0]["Father_last_name"].ToString();
                txt_occupation.Text = dt.Rows[0]["Father_occupation"].ToString();
                txt_qualitication.Text = dt.Rows[0]["Father_qualitication"].ToString();
                txt_designation.Text = dt.Rows[0]["Father_designation"].ToString();



                lbl_father_mob.Text = dt.Rows[0]["Country_Code_Father"].ToString() + " " + dt.Rows[0]["Father_mobile"].ToString();
                txt_email.Text = dt.Rows[0]["Email"].ToString();
                txt_annual_income.Text = dt.Rows[0]["Father_annual_income"].ToString();
                //=================4

                //================5
                txt_mother_first_name.Text = dt.Rows[0]["Mother_first_name"].ToString();
                txt_mother_middle_name.Text = dt.Rows[0]["Mother_middle_name"].ToString();
                txt_mother_last_name.Text = dt.Rows[0]["Mother_last_name"].ToString();
                txt_Mother_Occupation.Text = dt.Rows[0]["Mother_occupation"].ToString();
                txt_mother_Qualification.Text = dt.Rows[0]["Mother_qualification"].ToString();
                txt_mother_Designation.Text = dt.Rows[0]["Mother_designation"].ToString();


                lbl_mther_mob.Text = dt.Rows[0]["Country_Code_Mother"].ToString() + " " + dt.Rows[0]["Mother_mobile"].ToString();
                txt_mother_emailid.Text = dt.Rows[0]["Mother_emailid"].ToString();
                txt_mother_income.Text = dt.Rows[0]["Mother_income"].ToString();
                //===============5


                //============6
                txt_adress.Text = dt.Rows[0]["Persent_adress"].ToString();
                txt_city.Text = dt.Rows[0]["Persent_city"].ToString();
                txt_State.Text = dt.Rows[0]["Persent_state"].ToString();
                txt_pincode.Text = dt.Rows[0]["Persent_pincode"].ToString();

                txt_present_po.Text = dt.Rows[0]["Persent_po"].ToString();
                txt_present_district.Text = dt.Rows[0]["Persent_district"].ToString();

                txt_pAddress.Text = dt.Rows[0]["Permanent_adress"].ToString();
                txt_pcity.Text = dt.Rows[0]["Permanent_city"].ToString();
                txt_Pstate.Text = dt.Rows[0]["Permanent_state"].ToString();
                txt_Ppincod.Text = dt.Rows[0]["Permanent_pincod"].ToString();

                txt_perma_po.Text = dt.Rows[0]["Permanent_po"].ToString();
                txt_perma_disctrict.Text = dt.Rows[0]["Permanent_district"].ToString();
                //============6

                lbl_is_handicap.Text = dt.Rows[0]["Is_student_handicapped"].ToString();

                txt_medicalremarks.Text = dt.Rows[0]["Medical_remarks"].ToString();
                txt_about_theschool.Text = dt.Rows[0]["About_theschool"].ToString();
                //===========7


                //===========8
                if (dt.Rows[0]["Student_image"].ToString() == "")
                {
                    Image1.Visible = false;
                }
                else
                {
                    Image1.Visible = true;
                    Image1.ImageUrl = dt.Rows[0]["Student_image"].ToString();
                }
                if (dt.Rows[0]["father_signature"].ToString() == "")
                {
                    img_father_sig.Visible = false;
                }
                else
                {
                    img_father_sig.Visible = true;
                    img_father_sig.ImageUrl = dt.Rows[0]["father_signature"].ToString();
                }

                if (dt.Rows[0]["mother_signature"].ToString() == "")
                {
                    img_mother_signature.Visible = false;
                }
                else
                {
                    img_mother_signature.Visible = true;
                    img_mother_signature.ImageUrl = dt.Rows[0]["mother_signature"].ToString();
                }




                if (dt.Rows[0]["mother_photo"].ToString() == "")
                {
                    img_mother_photo.Visible = false;
                }
                else
                {
                    img_mother_photo.Visible = true;
                    img_mother_photo.ImageUrl = dt.Rows[0]["mother_photo"].ToString();
                }

                if (dt.Rows[0]["father_photo"].ToString() == "")
                {
                    img_fathers_image.Visible = false;
                }
                else
                {
                    img_fathers_image.Visible = true;
                    img_fathers_image.ImageUrl = dt.Rows[0]["father_photo"].ToString();
                }

                if (dt.Rows[0]["birth_certificate"].ToString() == "")
                {
                    img_birthcertificate.Visible = false;
                }
                else
                {
                    img_birthcertificate.Visible = true;
                    img_birthcertificate.ImageUrl = dt.Rows[0]["birth_certificate"].ToString();
                }

                if (dt.Rows[0]["Family_photo"].ToString() == "")
                {
                    img_family_image.Visible = false;
                }
                else
                {
                    img_family_image.Visible = true;
                    img_family_image.ImageUrl = dt.Rows[0]["Family_photo"].ToString();
                }

                if (dt.Rows[0]["residential_certificate"].ToString() == "")
                {
                    img_residental.Visible = false;
                }
                else
                {
                    img_residental.Visible = true;
                    img_residental.ImageUrl = dt.Rows[0]["residential_certificate"].ToString();
                }


                lbl_mom_name.Text = dt.Rows[0]["Mother_name"].ToString();
                lbl_term_c_std_name.Text = dt.Rows[0]["Name"].ToString();








                if (dt.Rows[0]["Payment_mode"].ToString() == "Online")
                {
                    rd_payment_mode_ofline.Checked = false;
                    rd_payment_mode_online.Checked = true;
                    paymentid1.Visible = false;
                    paymentid2.Visible = false;
                    txt_transaction_no.Text = "";
                    lbl_type_transaction.Text = "";
                    ViewState["payment_slip"] = "#";
                }
                else
                {
                    rd_payment_mode_ofline.Checked = true;
                    rd_payment_mode_online.Checked = false;
                    txt_transaction_no.Text = dt.Rows[0]["razorpay_payment_id"].ToString();
                    lbl_type_transaction.Text = dt.Rows[0]["razorpay_order_id"].ToString();
                    paymentid1.Visible = true;
                    paymentid2.Visible = true;
                    ViewState["payment_slip"] = dt.Rows[0]["payment_slip"].ToString();

                    if (dt.Rows[0]["payment_slip"].ToString() == "")
                    {
                        ViewState["payment_slip"] = "#";
                    }
                    else if (dt.Rows[0]["payment_slip"].ToString() == "#")
                    {
                        ViewState["payment_slip"] = "#";
                    }
                    else
                    {
                        ViewState["payment_slip"] = dt.Rows[0]["payment_slip"].ToString();

                    }
                }





            }
        }

        protected void btn_modify_Click(object sender, EventArgs e)
        {

            // Response.Redirect("Scholarship-registration.aspx?regiDs=" + ViewState["regiD"].ToString(), false);
            string url = "Scholarship-registration.aspx?classid=" + ViewState["Class_id"].ToString() + "&regiDs=" + ViewState["regiD"].ToString() + "&testid=" + ViewState["testid"].ToString();
            Response.Redirect(url, false);
        }
        string scrpt;
        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            int fill_no_seat = My.get_no_seat_current_session_Scholarship(ViewState["Session_id"].ToString(), ViewState["Class_id"].ToString(), ViewState["testid"].ToString());
            int get_no_fill_seat = My.get_no_of_fill_from_seat_Scholarship(ViewState["Session_id"].ToString(), ViewState["Class_id"].ToString(), ViewState["testid"].ToString());





            int avl = Convert.ToInt32(get_no_fill_seat) - fill_no_seat;
            if (get_no_fill_seat > fill_no_seat)
            {
                if (ViewState["Reg_payment_type"].ToString() == "1")
                {
                    string qry = "update Scholarship_Admission set Steps_done='10',Payment_Status='Unpaid' where Registration_id='" + ViewState["regiD"].ToString() + "'";
                    mycode.executequery(qry);

                    double amont = My.toDouble(txt_reg_fees.Text);
                    if (amont == 0)
                    {
                        string qry1 = "update Scholarship_Admission set Payment_Status='Paid',Payment_remarks='Paid',razorpay_payment_id='NA',Payment_mode='NA' where Registration_id='" + ViewState["regiD"].ToString() + "'";
                        My.exeSql(qry1);

                    }
                    else
                    {
                    }


                    Response.Redirect("Print_Page.aspx?PaymentSliP=" + ViewState["regiD"].ToString(), false);
                }
                else
                {
                    if (CheckBox1.Checked == true)
                    {
                        if (rd_payment_mode_online.Checked == true)
                        {
                            if (ViewState["Getwey_Type"].ToString() == "Razorpay")// if razor Pay getwey
                            {

                                string ayouthkey = My.autkeyrozorkey();
                                double amtForRazorrounD = Math.Round(Convert.ToDouble(ViewState["Payable_amount"].ToString()));
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


                                //================

                                My.exeSql("update Scholarship_Admission set razorpay_order_id='" + odrID + "' where Order_id='" + ViewState["Order_id"].ToString() + "' and Registration_id='" + ViewState["regiD"].ToString() + "'");
                                Session["sytemid"] = ViewState["Order_id"].ToString();
                                Response.Redirect("Online_Payment_admission/RazorPay/Check_out_Onlinepayment.aspx", false);// call back  
                            }
                            else if (ViewState["Getwey_Type"].ToString() == "Worldline")
                            {
                                try
                                {
                                    //  ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                                    //  ViewState["Order_id"] = dt.Rows[0]["Order_id"].ToString();
                                    //  ViewState["Payable_amount"] = dt.Rows[0]["Payable_amount"].ToString();

                                    objReqMsgDTO = new com.awl.MerchantToolKit.ReqMsgDTO();
                                    objReqMsgDTO.OrderId = ViewState["Order_id"].ToString();
                                    objReqMsgDTO.Mid = My.MERCHANT_KEY();
                                    objReqMsgDTO.Enckey = My.autkeyrozorkey();
                                    objReqMsgDTO.MeTransReqType = "S";

                                    double amt = Convert.ToDouble(ViewState["Payable_amount"].ToString()) * 100;
                                    objReqMsgDTO.TrnAmt = amt.ToString();
                                    // objReqMsgDTO.TrnAmt = "100";

                                    objReqMsgDTO.RecurrPeriod = "";
                                    objReqMsgDTO.RecurrDay = "";
                                    objReqMsgDTO.ResponseUrl = My.URL() + "Online_Payment_admission/worldline/Response.aspx";
                                    objReqMsgDTO.TrnRemarks = "Monthly Payment";
                                    objReqMsgDTO.TrnCurrency = "INR";
                                    objReqMsgDTO.AddField1 = ViewState["regiD"].ToString();
                                    objReqMsgDTO.AddField2 = ViewState["Name"].ToString();
                                    objReqMsgDTO.AddField3 = ViewState["Student_email_id"].ToString();
                                    objReqMsgDTO.AddField4 = ViewState["Student_mob_no"].ToString();
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
                                    Response.Redirect("Online_Payment_admission/worldline/TrnPay_process.aspx", false);
                                }
                                catch
                                {
                                }
                            }
                        }
                        else
                        {


                            string qry = "update Scholarship_Admission set Steps_done='10',razorpay_payment_id='" + txt_transaction_no.Text + "',razorpay_order_id='" + lbl_type_transaction.Text + "',Payment_Status='Paid'  where Registration_id='" + ViewState["regiD"].ToString() + "'";

                            try
                            {
                                //double amont = My.toDouble(txt_reg_fees.Text);
                                //if (amont == 0)
                                //{
                                //    string qry1 = "update Scholarship_Admission set Payment_Status='Paid',Payment_remarks='Paid',razorpay_payment_id='NA',Payment_mode='NA' where Registration_id='" + ViewState["regiD"].ToString() + "'";
                                //    My.exeSql(qry1);

                                //}
                                //else
                                //{
                                //}

                            }
                            catch
                            {

                            }


                            mycode.executequery(qry);
                            Response.Redirect("print/Print_Page.aspx?PaymentSliP=" + ViewState["regiD"].ToString(), false);
                        }
                    }
                    else
                    {
                        CheckBox1.Focus();
                        lblmessage.Text = "Please read and accept the declaration.";
                        scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    }
                }
            }
            else
            {
                lblmessage.Text = "Sorry seat is full";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
        }
    }
}