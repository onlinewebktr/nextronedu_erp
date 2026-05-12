using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Online_Form_Edit : System.Web.UI.Page
    {
        string scrpt;

        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string class_id = "0";
                if (!IsPostBack)
                {
                    mycode.bind_ddl(ddl_Category, "select Category_name from Cast_category");
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    mycode.bind_all_ddl_with_id_no_select(ddl_session, "select Session,session_id from session_details where Old_Use_Mode='2'   order by ID asc");//where Use_mode='1'


                    UsesCode.bind_ddl_noselect(ddl_cunterycode1, "select Country_code from Country_list order by Country_name asc");
                    UsesCode.bind_ddl_noselect(ddl_cunterycode2, "select Country_code from Country_list order by Country_name asc");
                    ddl_cunterycode1.SelectedValue = "+91";
                    ddl_cunterycode2.SelectedValue = "+91";
                   
                    process_active("1");
                    ViewState["payment_slip"] = "#";
                    if (Request.QueryString["regiDs"] != "0")
                    {
                        string regiD = Request.QueryString["regiDs"];
                        class_id = get_class_id_from_reg_id(regiD);
                        ViewState["class_id"] = class_id;

                        ddl_class.SelectedValue = class_id;
                        hd_applicationid.Value = regiD;

                        Fee_selection();
                        fetch_prev_fill_data(regiD);
                    }

                    Fee_selection();
                    fetch_company_name();
                }
            }
            catch (Exception exc)
            {
            }
        }

        private string get_class_id_from_reg_id(string regiD)
        {
            DataTable dt = mycode.FillData("select Class_id,Session_id from Online_Admission where Registration_id='" + regiD + "' ");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                ViewState["sessionid"] = dt.Rows[0]["Class_id"].ToString();
                ddl_session.SelectedValue = dt.Rows[0]["Session_id"].ToString();
                return dt.Rows[0]["Class_id"].ToString();
            }
        }

        My imp = new My();
        private void fetch_company_name()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {




            }
            else
            {
                school_logo.Src = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }

        }

        private void fetch_prev_fill_data(string regiD)
        {
            DataTable dt = mycode.FillData("Select * from Online_Admission where Registration_id='" + regiD + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {

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

                if (dt.Rows[0]["Payment_mode"].ToString() == "Online")
                {
                    rd_payment_mode_ofline.Checked = false;
                    rd_payment_mode_online.Checked = true;
                    paymentid1.Visible = false;
                    paymentid2.Visible = false;
                    txt_transaction_no.Text = "";
                    ddl_transactiontype.Text = "Select";
                    ViewState["payment_slip"] = "#";
                }
                else
                {
                    a1.HRef = dt.Rows[0]["payment_slip"].ToString();
                    rd_payment_mode_ofline.Checked = true;
                    rd_payment_mode_online.Checked = false;
                    txt_transaction_no.Text = dt.Rows[0]["razorpay_payment_id"].ToString();
                    ddl_transactiontype.Text = dt.Rows[0]["razorpay_order_id"].ToString();
                    paymentid1.Visible = true;
                    paymentid2.Visible = true;

                    if (dt.Rows[0]["payment_slip"].ToString() == "")
                    {
                        ViewState["payment_slip"] = "#";
                        img_payment_slip.Visible = false;
                    }
                    else if (dt.Rows[0]["payment_slip"].ToString() == "#")
                    {
                        ViewState["payment_slip"] = "#";
                        img_payment_slip.Visible = false;
                    }
                    else
                    {
                        ViewState["payment_slip"] = dt.Rows[0]["payment_slip"].ToString();
                        img_payment_slip.Visible = true;
                        img_payment_slip.ImageUrl = dt.Rows[0]["payment_slip"].ToString();
                    }



                }



                ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();

                txt_std_mob.Text = dt.Rows[0]["Student_mob_no"].ToString();
                txt_std_email_id.Text = dt.Rows[0]["Student_email_id"].ToString();
                //=====2
                txt_first_name.Text = dt.Rows[0]["Student_first_name"].ToString();
                txt_middle_name.Text = dt.Rows[0]["Student_middle_name"].ToString();
                txt_last_name.Text = dt.Rows[0]["Student_last_name"].ToString();
                txt_dob.Text = dt.Rows[0]["DOB"].ToString();
                try
                {
                    ddl_gender.Text = dt.Rows[0]["Gender"].ToString();
                }
                catch (Exception ex)
                {
                }
                txt_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                txt_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                try
                {
                    ddl_religion.Text = dt.Rows[0]["Religion"].ToString();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ddl_Category.Text = dt.Rows[0]["Category"].ToString();
                }
                catch (Exception ex)
                {
                }

                txt_aadharcarno.Text = dt.Rows[0]["Aadhar_no"].ToString();
                txt_identification_marks.Text = dt.Rows[0]["Identification_marks"].ToString();

                try
                {
                    if (dt.Rows[0]["Services"].ToString() == "Day Scholar")
                    {
                        rd_day.Checked = true;
                    }
                    if (dt.Rows[0]["Services"].ToString() == "Day Boarding")
                    {
                        rd_dayboarding.Checked = true;
                    }
                    if (dt.Rows[0]["Services"].ToString() == "Hosteler")
                    {
                        rd_hostel.Checked = true;
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    ddl_class.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                }
                catch (Exception ex)
                {
                }

                txt_reg_fees.Text = dt.Rows[0]["Payable_amount"].ToString();
                ViewState["regfee"] = dt.Rows[0]["Payable_amount"].ToString();
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
                try
                {
                    ddl_cunterycode1.Text = dt.Rows[0]["Country_Code_Father"].ToString();
                }
                catch (Exception ex)
                {
                }

                txt_father_mobile.Text = dt.Rows[0]["Father_mobile"].ToString();
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

                try
                {
                    ddl_cunterycode2.Text = dt.Rows[0]["Country_Code_Mother"].ToString();
                }
                catch (Exception ex)
                {
                }
                txt_mother_mobile_no.Text = dt.Rows[0]["Mother_mobile"].ToString();
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


                //============7
                try
                {
                    if (dt.Rows[0]["Is_student_handicapped"].ToString() == "Yes")
                    {
                        rd_handicp_yes.Checked = true;
                    }
                    else
                    {
                        rd_handicp_no.Checked = false;
                    }
                }
                catch (Exception ex)
                {
                }

                txt_medicalremarks.Text = dt.Rows[0]["Medical_remarks"].ToString();
                txt_about_theschool.Text = dt.Rows[0]["About_theschool"].ToString();
                //===========7


                //===========8
                lbl_std_img.Text = dt.Rows[0]["Student_image"].ToString();

                lbl_father_signature.Text = dt.Rows[0]["father_signature"].ToString();
                lbl_mother_signature.Text = dt.Rows[0]["mother_signature"].ToString();

                //------------------file----------

                if (dt.Rows[0]["Student_image"].ToString() != "")
                {
                    ViewState["Student_image"] = dt.Rows[0]["Student_image"].ToString();
                    img_student_image.Visible = true;
                    img_student_image.ImageUrl = dt.Rows[0]["Student_image"].ToString();

                }
                else
                {
                    ViewState["Student_image"] = "";
                    img_student_image.Visible = false;
                }
                if (dt.Rows[0]["father_signature"].ToString() != "")
                {
                    ViewState["Father_sig"] = dt.Rows[0]["father_signature"].ToString();
                    img_father_sig.Visible = true;
                    img_father_sig.ImageUrl = dt.Rows[0]["father_signature"].ToString();
                }
                else
                {
                    img_father_sig.Visible = false;
                    ViewState["Father_sig"] = "";
                }

                if (dt.Rows[0]["mother_signature"].ToString() != "")
                {
                    ViewState["mother_sig"] = dt.Rows[0]["mother_signature"].ToString();

                    img_mother_signature.Visible = true;
                    img_mother_signature.ImageUrl = dt.Rows[0]["mother_signature"].ToString();
                }

                else
                {
                    img_mother_signature.Visible = false;
                    ViewState["mother_sig"] = "";

                }

                if (dt.Rows[0]["mother_photo"].ToString() != "")
                {
                    img_mother_photo.Visible = true;
                    ViewState["mother_photo"] = dt.Rows[0]["mother_photo"].ToString();
                    img_mother_photo.ImageUrl= dt.Rows[0]["mother_photo"].ToString();
                }

                else
                {
                    img_mother_photo.Visible = false;
                    ViewState["mother_photo"] = "";

                }

                if (dt.Rows[0]["father_photo"].ToString() != "")
                {
                    img_father_photo.Visible = true;
                    ViewState["father_photo"] = dt.Rows[0]["father_photo"].ToString();
                    img_father_photo.ImageUrl = dt.Rows[0]["father_photo"].ToString();
                }

                else
                {
                    img_father_photo.Visible = false;
                    ViewState["father_photo"] = "";

                }


                if (dt.Rows[0]["Family_photo"].ToString() != "")
                {
                    img_Family_photo.Visible = true;
                    ViewState["Family_photo"] = dt.Rows[0]["Family_photo"].ToString();
                    img_Family_photo.ImageUrl = dt.Rows[0]["Family_photo"].ToString();
                }

                else
                {
                    img_Family_photo.Visible = false;
                    ViewState["Family_photo"] = "";

                }

                if (dt.Rows[0]["birth_certificate"].ToString() != "")
                {
                    img_Birth_Certificate.Visible = true;
                    ViewState["birth_certificate"] = dt.Rows[0]["birth_certificate"].ToString();

                    img_Birth_Certificate.ImageUrl = dt.Rows[0]["birth_certificate"].ToString();
                }

                else
                {
                    img_Birth_Certificate.Visible = false;
                    ViewState["birth_certificate"] = "";

                }
                if (dt.Rows[0]["residential_certificate"].ToString() != "")
                {
                    img_Residential_Certificate.Visible = true;
                    ViewState["residential_certificate"] = dt.Rows[0]["residential_certificate"].ToString();

                    img_Residential_Certificate.ImageUrl = dt.Rows[0]["residential_certificate"].ToString();
                }

                else
                {
                    img_Residential_Certificate.Visible = false;
                    ViewState["residential_certificate"] = "";

                }


                //==============8
                process_active("2");
            }
        }

        

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Fee_selection();
            }
            catch (Exception ex)
            {
            }
        }

        private void Fee_selection()
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select class";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {
                DataTable dt = mycode.FillTable("Select * from Online_reg_fees where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "'");
                if (dt.Rows.Count == 0)
                {
                    double regfee = 0;
                    ViewState["regfee"] = regfee.ToString("0.00");
                    txt_reg_fees.Text = regfee.ToString("0.00");
                }
                else
                {
                    double regfee = Convert.ToDouble(dt.Rows[0]["Fees"].ToString());
                    ViewState["regfee"] = regfee.ToString("0.00");
                    txt_reg_fees.Text = regfee.ToString("0.00");
                }
            }
        }

        private void alert(string msg)
        {
            lblmessage.Text = msg;
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        private void save_data(string type)
        {
            SqlCommand cmd;
            #region Step1
            if (type == "1")
            {
                DataTable dt = mycode.FillData("Select * from Online_Admission where Registration_id='" + hd_applicationid.Value + "' ");
                if (dt.Rows.Count == 0)
                {
                    string regi_code = UsesCode.reg_format("Online_reg_id");
                    hd_applicationid.Value = regi_code;
                    string query = "INSERT INTO Online_Admission (Student_mob_no,Student_email_id,Steps_done,Registration_id,Date,idate,Session_id,Class_id,Payable_amount) values (@Student_mob_no,@Student_email_id,@Steps_done,@Registration_id,@Date,@idate,@Session_id,@Class_id,@Payable_amount)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Student_mob_no", txt_std_mob.Text);
                    cmd.Parameters.AddWithValue("@Student_email_id", txt_std_email_id.Text);
                    cmd.Parameters.AddWithValue("@Steps_done", "2");
                    cmd.Parameters.AddWithValue("@Registration_id", regi_code);
                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                    cmd.Parameters.AddWithValue("@idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Payable_amount", txt_reg_fees.Text);




                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        ViewState["regiD"] = regi_code;
                        process_active("2");
                    }

                    ViewState["Student_image"] = "";
                    ViewState["Father_sig"] = "";
                    ViewState["mother_sig"] = "";
                }
                else
                {
                    if (dt.Rows[0]["Steps_done"].ToString() == "10")
                    {
                        //print_page


                        if (dt.Rows[0]["Payment_Status"].ToString() == "Unpaid")
                        {
                            ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                            Response.Redirect("slip/online-reg.aspx?regiDs=" + dt.Rows[0]["Registration_id"].ToString());

                        }
                        if (dt.Rows[0]["Payment_Status"].ToString() == "Paid")
                        {

                            Response.Redirect("slip/online-reg.aspx?regiDs=" + dt.Rows[0]["Registration_id"].ToString());
                        }
                        else
                        {
                            ViewState["Student_image"] = dt.Rows[0]["Student_image"].ToString();
                            ViewState["Father_sig"] = dt.Rows[0]["father_signature"].ToString();
                            ViewState["mother_sig"] = dt.Rows[0]["mother_signature"].ToString();



                            if (ViewState["Student_image"].ToString() == "")
                            {
                                img_student_image.Visible = false;
                            }
                            else if (ViewState["Student_image"].ToString() == "")
                            {
                                img_father_sig.Visible = false;
                            }
                            else if (ViewState["Student_image"].ToString() == "")
                            {
                                img_mother_signature.Visible = false;
                            }


                            ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                            txt_std_mob.Text = dt.Rows[0]["Student_mob_no"].ToString();
                            txt_std_email_id.Text = dt.Rows[0]["Student_email_id"].ToString();
                            //=====2
                            txt_first_name.Text = dt.Rows[0]["Student_first_name"].ToString();
                            txt_middle_name.Text = dt.Rows[0]["Student_middle_name"].ToString();
                            txt_last_name.Text = dt.Rows[0]["Student_last_name"].ToString();
                            txt_dob.Text = dt.Rows[0]["DOB"].ToString();
                            try
                            {
                                ddl_gender.Text = dt.Rows[0]["Gender"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }
                            txt_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                            txt_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                            try
                            {
                                ddl_religion.Text = dt.Rows[0]["Religion"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }
                            try
                            {
                                ddl_Category.Text = dt.Rows[0]["Category"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }

                            txt_aadharcarno.Text = dt.Rows[0]["Aadhar_no"].ToString();
                            txt_identification_marks.Text = dt.Rows[0]["Identification_marks"].ToString();

                            try
                            {
                                if (dt.Rows[0]["Services"].ToString() == "Day Scholar")
                                {
                                    rd_day.Checked = true;
                                }
                                if (dt.Rows[0]["Services"].ToString() == "Day Boarding")
                                {
                                    rd_dayboarding.Checked = true;
                                }
                                if (dt.Rows[0]["Services"].ToString() == "Hosteler")
                                {
                                    rd_hostel.Checked = true;
                                }
                            }
                            catch (Exception ex)
                            {
                            }

                            try
                            {
                                ddl_class.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }

                            txt_reg_fees.Text = dt.Rows[0]["Payable_amount"].ToString();
                            ViewState["regfee"] = dt.Rows[0]["Payable_amount"].ToString();
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
                            try
                            {
                                ddl_cunterycode1.Text = dt.Rows[0]["Country_Code_Father"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }

                            txt_father_mobile.Text = dt.Rows[0]["Father_mobile"].ToString();
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

                            try
                            {
                                ddl_cunterycode2.Text = dt.Rows[0]["Country_Code_Mother"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }
                            txt_mother_mobile_no.Text = dt.Rows[0]["Mother_mobile"].ToString();
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


                            //============7
                            try
                            {
                                if (dt.Rows[0]["Is_student_handicapped"].ToString() == "Yes")
                                {
                                    rd_handicp_yes.Checked = true;
                                }
                                else
                                {
                                    rd_handicp_no.Checked = false;
                                }
                            }
                            catch (Exception ex)
                            {
                            }

                            txt_medicalremarks.Text = dt.Rows[0]["Medical_remarks"].ToString();
                            txt_about_theschool.Text = dt.Rows[0]["About_theschool"].ToString();
                            //===========7


                            //===========8
                            lbl_std_img.Text = dt.Rows[0]["Student_image"].ToString();
                            //==============8

                            process_active(dt.Rows[0]["Steps_done"].ToString());
                        }

                        try
                        {
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

                            if (dt.Rows[0]["payment_slip"].ToString() == "Online")
                            {
                                paymentid1.Visible = false;
                                paymentid2.Visible = false;
                                txt_transaction_no.Text = "";
                                ddl_transactiontype.Text = "Select";
                                ViewState["payment_slip"] = "#";


                                img_payment_slip.Visible = false;


                            }
                            else
                            {
                                txt_transaction_no.Text = dt.Rows[0]["razorpay_payment_id"].ToString();
                                ddl_transactiontype.Text = dt.Rows[0]["razorpay_order_id"].ToString();
                                img_payment_slip.ImageUrl = dt.Rows[0]["payment_slip"].ToString();
                                img_payment_slip.Visible = true;

                                paymentid1.Visible = true;
                                paymentid2.Visible = true;
                            }

                        }
                        catch
                        {

                        }


                    }
                    else
                    {

                        ViewState["Student_image"] = dt.Rows[0]["Student_image"].ToString();
                        ViewState["Father_sig"] = dt.Rows[0]["father_signature"].ToString();
                        ViewState["mother_sig"] = dt.Rows[0]["mother_signature"].ToString();



                        if (ViewState["Student_image"].ToString() == "")
                        {
                            img_student_image.Visible = false;
                        }
                        else
                        {
                            img_student_image.ImageUrl = ViewState["Student_image"].ToString();
                            img_student_image.Visible = true;
                        }

                        if (ViewState["Father_sig"].ToString() == "")
                        {
                            img_father_sig.Visible = false;
                        }
                        else
                        {
                            img_father_sig.ImageUrl = ViewState["Father_sig"].ToString();
                            img_father_sig.Visible = true;

                        }


                        if (ViewState["mother_sig"].ToString() == "")
                        {
                            img_mother_signature.Visible = false;
                        }
                        else
                        {
                            img_mother_signature.ImageUrl = ViewState["mother_sig"].ToString();
                            img_mother_signature.Visible = true;
                        }


                        ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                        txt_std_mob.Text = dt.Rows[0]["Student_mob_no"].ToString();
                        txt_std_email_id.Text = dt.Rows[0]["Student_email_id"].ToString();
                        //=====2
                        txt_first_name.Text = dt.Rows[0]["Student_first_name"].ToString();
                        txt_middle_name.Text = dt.Rows[0]["Student_middle_name"].ToString();
                        txt_last_name.Text = dt.Rows[0]["Student_last_name"].ToString();
                        txt_dob.Text = dt.Rows[0]["DOB"].ToString();
                        try
                        {
                            ddl_gender.Text = dt.Rows[0]["Gender"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                        txt_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                        try
                        {
                            ddl_religion.Text = dt.Rows[0]["Religion"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_Category.Text = dt.Rows[0]["Category"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }

                        txt_aadharcarno.Text = dt.Rows[0]["Aadhar_no"].ToString();
                        txt_identification_marks.Text = dt.Rows[0]["Identification_marks"].ToString();

                        try
                        {
                            if (dt.Rows[0]["Services"].ToString() == "Day Scholar")
                            {
                                rd_day.Checked = true;
                            }
                            if (dt.Rows[0]["Services"].ToString() == "Day Boarding")
                            {
                                rd_dayboarding.Checked = true;
                            }
                            if (dt.Rows[0]["Services"].ToString() == "Hosteler")
                            {
                                rd_hostel.Checked = true;
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        try
                        {
                            ddl_class.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }

                        txt_reg_fees.Text = dt.Rows[0]["Payable_amount"].ToString();
                        ViewState["regfee"] = dt.Rows[0]["Payable_amount"].ToString();
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
                        try
                        {
                            ddl_cunterycode1.Text = dt.Rows[0]["Country_Code_Father"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }

                        txt_father_mobile.Text = dt.Rows[0]["Father_mobile"].ToString();
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

                        try
                        {
                            ddl_cunterycode2.Text = dt.Rows[0]["Country_Code_Mother"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_mother_mobile_no.Text = dt.Rows[0]["Mother_mobile"].ToString();
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


                        //============7
                        try
                        {
                            if (dt.Rows[0]["Is_student_handicapped"].ToString() == "Yes")
                            {
                                rd_handicp_yes.Checked = true;
                            }
                            else
                            {
                                rd_handicp_no.Checked = false;
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        txt_medicalremarks.Text = dt.Rows[0]["Medical_remarks"].ToString();
                        txt_about_theschool.Text = dt.Rows[0]["About_theschool"].ToString();
                        //===========7


                        //===========8
                        lbl_std_img.Text = dt.Rows[0]["Student_image"].ToString();
                        //==============8
                        try
                        {
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

                            if (dt.Rows[0]["Payment_mode"].ToString() == "Online")
                            {
                                rd_payment_mode_online.Checked = true;
                                rd_payment_mode_ofline.Checked = false;
                                paymentid1.Visible = false;
                                paymentid2.Visible = false;
                                txt_transaction_no.Text = "";
                                ddl_transactiontype.Text = "Select";
                                ViewState["payment_slip"] = "#";
                                img_payment_slip.Visible = false;
                            }
                            else
                            {
                                rd_payment_mode_online.Checked = false;
                                rd_payment_mode_ofline.Checked = true;
                                txt_transaction_no.Text = dt.Rows[0]["razorpay_payment_id"].ToString();
                                ddl_transactiontype.Text = dt.Rows[0]["razorpay_order_id"].ToString();
                                img_payment_slip.ImageUrl = dt.Rows[0]["payment_slip"].ToString();
                                img_payment_slip.Visible = true;
                                paymentid1.Visible = true;
                                paymentid2.Visible = true;
                            }

                        }
                        catch
                        {

                        }
                        process_active(dt.Rows[0]["Steps_done"].ToString());
                    }
                }
            }
            #endregion

            #region Student DetailS
            if (type == "2")
            {
                DataTable dt = mycode.FillData("Select Steps_done,Registration_id from Online_Admission where Registration_id='" + ViewState["regiD"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "'");
                if (dt.Rows.Count != 0)
                {
                    string query = "Update Online_Admission set Name=@Name,Student_first_name=@Student_first_name,Student_middle_name=@Student_middle_name,Student_last_name=@Student_last_name,DOB=@DOB,Gender=@Gender,Nationality=@Nationality,Blood_group=@Blood_group,Religion=@Religion,Category=@Category,Aadhar_no=@Aadhar_no,Identification_marks=@Identification_marks,Services=@Services,Class_id=@Class_id,Class=@Class,Payable_amount=@Payable_amount,Steps_done=@Steps_done,Session_id=@Session_id where Registration_id = @Registration_id";
                    cmd = new SqlCommand(query);


                    
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Registration_id", ViewState["regiD"].ToString());
                    if (txt_middle_name.Text != "")
                    {
                        cmd.Parameters.AddWithValue("@Name", txt_first_name.Text + " " + txt_middle_name.Text + " " + txt_last_name.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Name", txt_first_name.Text + " " + txt_last_name.Text);
                    }

                    cmd.Parameters.AddWithValue("@Student_first_name", txt_first_name.Text);
                    cmd.Parameters.AddWithValue("@Student_middle_name", txt_middle_name.Text);
                    cmd.Parameters.AddWithValue("@Student_last_name", txt_last_name.Text);
                    cmd.Parameters.AddWithValue("@DOB", txt_dob.Text);

                    cmd.Parameters.AddWithValue("@Gender", ddl_gender.Text);
                    cmd.Parameters.AddWithValue("@Nationality", txt_nationality.Text);
                    cmd.Parameters.AddWithValue("@Blood_group", txt_blood_group.Text);
                    cmd.Parameters.AddWithValue("@Religion", ddl_religion.Text);
                    cmd.Parameters.AddWithValue("@Category", ddl_Category.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Aadhar_no", txt_aadharcarno.Text);
                    cmd.Parameters.AddWithValue("@Identification_marks", txt_identification_marks.Text);
                    if (rd_day.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Services", "Day Scholar");
                    }
                    else if (rd_dayboarding.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Services", "Day Boarding");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Services", "Hosteler");
                    }
                    cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedItem.Text);
                    double totalamount = Convert.ToDouble(ViewState["regfee"].ToString());
                    cmd.Parameters.AddWithValue("@Payable_amount", totalamount.ToString("0.00"));



                    


                    cmd.Parameters.AddWithValue("@Steps_done", "3");
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                        process_active("3");
                    }
                }
                else
                {


                    string query = "INSERT INTO Online_Admission (Student_mob_no,Student_email_id,Steps_done,Registration_id,Date,idate,Session_id,razorpay_payment_id,razorpay_order_id,payment_slip,Payment_mode) values (@Student_mob_no,@Student_email_id,@Steps_done,@Registration_id,@Date,@idate,@Session_id,@razorpay_payment_id,@razorpay_order_id,@payment_slip,@Payment_mode)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Student_mob_no", txt_std_mob.Text);
                    cmd.Parameters.AddWithValue("@Student_email_id", txt_std_email_id.Text);
                    cmd.Parameters.AddWithValue("@Steps_done", "2");
                    cmd.Parameters.AddWithValue("@Registration_id", hd_applicationid.Value);
                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                    cmd.Parameters.AddWithValue("@idate", mycode.idate());

                    cmd.Parameters.AddWithValue("@razorpay_payment_id", txt_transaction_no.Text);
                    cmd.Parameters.AddWithValue("@razorpay_order_id", ddl_transactiontype.Text);
                    cmd.Parameters.AddWithValue("@payment_slip", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Payment_mode", ddl_class.SelectedValue);


                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        ViewState["regiD"] = hd_applicationid.Value;
                        process_active("2");
                    }
                }
            }
            #endregion

            #region Previous School Details
            if (type == "3")
            {
                DataTable dt = mycode.FillData("Select Steps_done,Registration_id from Online_Admission where Registration_id='" + ViewState["regiD"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "'");
                if (dt.Rows.Count != 0)
                {
                    string query = "Update Online_Admission set Last_school=@Last_school,Last_school_board=@Last_school_board,Passout_classs=@Passout_classs,Percentage=@Percentage,Reason_for_shift=@Reason_for_shift,Passed_year=@Passed_year,Steps_done=@Steps_done,Session_id=@Session_id where Registration_id = @Registration_id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Registration_id", ViewState["regiD"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Last_school", txt_lastschool.Text);
                    cmd.Parameters.AddWithValue("@Last_school_board", txt_board.Text);
                    cmd.Parameters.AddWithValue("@Passout_classs", txt_passout_classs.Text);
                    cmd.Parameters.AddWithValue("@Percentage", txt_percentage.Text);
                    cmd.Parameters.AddWithValue("@Reason_for_shift", txt_reason_for_shift.Text);
                    cmd.Parameters.AddWithValue("@Passed_year", txt_year.Text);
                    cmd.Parameters.AddWithValue("@Steps_done", "4");
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                        process_active("4");
                    }
                }
            }
            #endregion

            #region Father Details
            if (type == "4")
            {
                DataTable dt = mycode.FillData("Select Steps_done,Registration_id from Online_Admission where Registration_id='" + ViewState["regiD"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "'");
                if (dt.Rows.Count != 0)
                {
                    string query = "Update Online_Admission set Father_name=@Father_name,Father_first_name=@Father_first_name,Father_middle_name=@Father_middle_name,Father_last_name=@Father_last_name,Father_occupation=@Father_occupation,Father_qualitication=@Father_qualitication,Father_designation=@Father_designation,Country_Code_Father=@Country_Code_Father,Father_mobile=@Father_mobile,Email=@Email,Father_annual_income=@Father_annual_income,Steps_done=@Steps_done,Session_id=@Session_id where Registration_id = @Registration_id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Registration_id", ViewState["regiD"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);

                    if (txt_father_middle_name.Text != "")
                    {
                        cmd.Parameters.AddWithValue("@Father_name", txt_father_first_name.Text + " " + txt_father_middle_name.Text + " " + txt_father_last_name.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Father_name", txt_father_first_name.Text + " " + txt_father_last_name.Text);
                    }
                    cmd.Parameters.AddWithValue("@Father_first_name", txt_father_first_name.Text);
                    cmd.Parameters.AddWithValue("@Father_middle_name", txt_father_middle_name.Text);
                    cmd.Parameters.AddWithValue("@Father_last_name", txt_father_last_name.Text);
                    cmd.Parameters.AddWithValue("@Father_occupation", txt_occupation.Text);
                    cmd.Parameters.AddWithValue("@Father_qualitication", txt_qualitication.Text);
                    cmd.Parameters.AddWithValue("@Father_designation", txt_designation.Text);
                    cmd.Parameters.AddWithValue("@Country_Code_Father", ddl_cunterycode1.Text);
                    cmd.Parameters.AddWithValue("@Father_mobile", txt_father_mobile.Text);
                    cmd.Parameters.AddWithValue("@Email", txt_email.Text);
                    cmd.Parameters.AddWithValue("@Father_annual_income", txt_annual_income.Text);
                    cmd.Parameters.AddWithValue("@Steps_done", "5");
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                        process_active("5");
                    }
                }
            }
            #endregion

            #region Mother Details
            if (type == "5")
            {
                DataTable dt = mycode.FillData("Select Steps_done,Registration_id from Online_Admission where Registration_id='" + ViewState["regiD"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "'");
                if (dt.Rows.Count != 0)
                {
                    string query = "Update Online_Admission set Mother_name=@Mother_name,Mother_first_name=@Mother_first_name,Mother_middle_name=@Mother_middle_name,Mother_last_name=@Mother_last_name,Mother_occupation=@Mother_occupation,Mother_qualification=@Mother_qualification,Mother_designation=@Mother_designation,Country_Code_Mother=@Country_Code_Mother,Mother_mobile=@Mother_mobile,Mother_emailid=@Mother_emailid,Mother_income=@Mother_income,Steps_done=@Steps_done,Session_id=@Session_id where Registration_id = @Registration_id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Registration_id", ViewState["regiD"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);

                    if (txt_mother_middle_name.Text != "")
                    {
                        cmd.Parameters.AddWithValue("@Mother_name", txt_mother_first_name.Text + " " + txt_mother_middle_name.Text + " " + txt_mother_last_name.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Mother_name", txt_mother_first_name.Text + " " + txt_mother_last_name.Text);
                    }
                    cmd.Parameters.AddWithValue("@Mother_first_name", txt_mother_first_name.Text);
                    cmd.Parameters.AddWithValue("@Mother_middle_name", txt_mother_middle_name.Text);
                    cmd.Parameters.AddWithValue("@Mother_last_name", txt_mother_last_name.Text);
                    cmd.Parameters.AddWithValue("@Mother_occupation", txt_Mother_Occupation.Text);
                    cmd.Parameters.AddWithValue("@Mother_qualification", txt_mother_Qualification.Text);
                    cmd.Parameters.AddWithValue("@Mother_designation", txt_mother_Designation.Text);
                    cmd.Parameters.AddWithValue("@Country_Code_Mother", ddl_cunterycode2.Text);
                    cmd.Parameters.AddWithValue("@Mother_mobile", txt_mother_mobile_no.Text);
                    cmd.Parameters.AddWithValue("@Mother_emailid", txt_mother_emailid.Text);
                    cmd.Parameters.AddWithValue("@Mother_income", txt_mother_income.Text);
                    cmd.Parameters.AddWithValue("@Steps_done", "6");
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                        process_active("6");
                    }
                }
            }
            #endregion

            #region Address Details
            if (type == "6")
            {
                DataTable dt = mycode.FillData("Select Steps_done,Registration_id from Online_Admission where Registration_id='" + ViewState["regiD"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "'");
                if (dt.Rows.Count != 0)
                {
                    string query = "Update Online_Admission set Persent_adress=@Persent_adress,Persent_state=@Persent_state,Persent_pincode=@Persent_pincode,Persent_city=@Persent_city,Persent_po=@Persent_po,Persent_district=@Persent_district,Permanent_adress=@Permanent_adress,Permanent_state=@Permanent_state,Permanent_pincod=@Permanent_pincod,Permanent_city=@Permanent_city,Permanent_po=@Permanent_po,Permanent_district=@Permanent_district,Steps_done=@Steps_done,Session_id=@Session_id where Registration_id = @Registration_id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Registration_id", ViewState["regiD"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Persent_adress", txt_adress.Text);
                    cmd.Parameters.AddWithValue("@Persent_state", txt_State.Text);
                    cmd.Parameters.AddWithValue("@Persent_pincode", txt_Ppincod.Text);
                    cmd.Parameters.AddWithValue("@Persent_city", txt_pcity.Text);
                    cmd.Parameters.AddWithValue("@Persent_po", txt_present_po.Text);
                    cmd.Parameters.AddWithValue("@Persent_district", txt_present_district.Text);

                    cmd.Parameters.AddWithValue("@Permanent_adress", txt_pAddress.Text);
                    cmd.Parameters.AddWithValue("@Permanent_state", txt_Pstate.Text);
                    cmd.Parameters.AddWithValue("@Permanent_pincod", txt_Ppincod.Text);
                    cmd.Parameters.AddWithValue("@Permanent_city", txt_pcity.Text);
                    cmd.Parameters.AddWithValue("@Permanent_po", txt_perma_po.Text);
                    cmd.Parameters.AddWithValue("@Permanent_district", txt_perma_disctrict.Text);
                    cmd.Parameters.AddWithValue("@Steps_done", "7");
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                        process_active("7");
                    }
                }
            }
            #endregion


            #region Misc. Details
            if (type == "7")
            {
                DataTable dt = mycode.FillData("Select Steps_done,Registration_id from Online_Admission where Registration_id='" + ViewState["regiD"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "'");
                if (dt.Rows.Count != 0)
                {
                    string query = "Update Online_Admission set Is_student_handicapped=@Is_student_handicapped,Medical_remarks=@Medical_remarks,About_theschool=@About_theschool,Steps_done=@Steps_done,Session_id=@Session_id where Registration_id = @Registration_id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Registration_id", ViewState["regiD"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                    if (rd_handicp_yes.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Is_student_handicapped", "Yes");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Is_student_handicapped", "No");
                    }
                    cmd.Parameters.AddWithValue("@Medical_remarks", txt_medicalremarks.Text);
                    cmd.Parameters.AddWithValue("@About_theschool", txt_about_theschool.Text);
                    cmd.Parameters.AddWithValue("@Steps_done", "8");
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        ViewState["regiD"] = dt.Rows[0]["Registration_id"].ToString();
                        process_active("8");
                    }
                }
            }
            #endregion

            #region Final Details
            if (type == "8")
            {
                DataTable dt = mycode.FillData("Select Steps_done,Student_image,Registration_id from Online_Admission where Registration_id='" + ViewState["regiD"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "'");
                if (dt.Rows.Count != 0)
                {

                    //if (ViewState["Student_image"].ToString() == "")
                    //{
                    //    alert("Please upload student image");
                    //}
                    //else if (ViewState["Father_sig"].ToString() == "")
                    //{
                    //    alert("Please upload father's signature");
                    //}
                    //else if (ViewState["mother_sig"].ToString() == "")
                    //{
                    //    alert("Please upload mother's signature");
                    //}
                    //else
                    //{
                    save_final_with_img(ViewState["regiD"].ToString(), ViewState["Student_image"].ToString(), ViewState["Father_sig"].ToString(), ViewState["mother_sig"].ToString());
                    //}


                }


            }
            #endregion
        }

        private void save_final_with_img(string regID, string filepath1, string fathersignature, string mothersignature)//
        {
            Random r = new Random(DateTime.Now.Millisecond);
           
            SqlCommand cmd;
            string query = "Update Online_Admission set Student_image=@Student_image,Steps_done=@Steps_done,father_signature=@father_signature,mother_signature=@mother_signature,mother_photo=@mother_photo,father_photo=@father_photo,Family_photo=@Family_photo,birth_certificate=@birth_certificate,residential_certificate=@residential_certificate where Registration_id = @Registration_id";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Registration_id", regID);
            cmd.Parameters.AddWithValue("@Student_image", filepath1);
            cmd.Parameters.AddWithValue("@Steps_done", "10");
            cmd.Parameters.AddWithValue("@father_signature", fathersignature);
            cmd.Parameters.AddWithValue("@mother_signature", mothersignature);
            cmd.Parameters.AddWithValue("@mother_photo", ViewState["mother_photo"].ToString());
            cmd.Parameters.AddWithValue("@father_photo", ViewState["father_photo"].ToString());
            cmd.Parameters.AddWithValue("@Family_photo", ViewState["Family_photo"].ToString());
            cmd.Parameters.AddWithValue("@birth_certificate", ViewState["birth_certificate"].ToString());
            cmd.Parameters.AddWithValue("@residential_certificate", ViewState["residential_certificate"].ToString());



            if (InsertUpdate.InsertUpdateData(cmd))
            {
                ViewState["regiD"] = regID;
                Response.Redirect("slip/online-reg.aspx?regiDs=" + regID, false);
            }
        }



        #region ProceSS
       

        protected void btn_acc_dt_Click(object sender, EventArgs e)
        {
            if (txt_std_mob.Text == "")
            {
                txt_std_mob.Focus();
                alert("Pleaase enter mobile no.");
            }
            else if (txt_std_email_id.Text == "")
            {
                txt_std_email_id.Focus();
                alert("Pleaase enter email id.");
            }
            else
            {
                bool chkedata = get_data_emailidand_mobile_no();
                if (chkedata == true)
                {
                    save_data("1");
                }
                else
                {


                }
            }
        }

        private bool get_data_emailidand_mobile_no()
        {

            DataTable dt = mycode.FillData("Select *,(Select top 1 Session from session_details where session_id=Online_Admission.Session_id) as session from Online_Admission where Student_mob_no='" + txt_std_mob.Text + "' and Student_email_id='" + txt_std_email_id.Text + "'");// and Session_id='" + ddl_session.SelectedValue + "'
            if (dt.Rows.Count == 0)
            {


                rd_view.DataSource = null;
                rd_view.DataBind();
                return true;

            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                return false;

            }
        }

        protected void btn_std_dt_Click(object sender, EventArgs e)
        {
            if (txt_first_name.Text == "")
            {
                alert("Please enter first name.");
                txt_first_name.Focus();
            }
            else if (txt_dob.Text == "")
            {
                alert("Please enter date of birth.");
                txt_dob.Focus();
            }
            else if (txt_nationality.Text == "")
            {
                alert("Please enter nationality.");
                txt_nationality.Focus();
            }


            else if (ddl_Category.Text == "Select")
            {
                alert("Please select caste.");
                ddl_Category.Focus();
            }


            else if (rd_day.Checked == false && rd_hostel.Checked == false && rd_dayboarding.Checked == false)
            {
                alert("Please chose admission type");
                rd_day.Focus();
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                alert("Please select class.");
                ddl_class.Focus();
            }
            if (rd_payment_mode_online.Checked == false && rd_payment_mode_ofline.Checked == false)
            {
                alert("Please select payment mode.");
                rd_payment_mode_online.Focus();
            }

            else
            {
                if (rd_payment_mode_ofline.Checked == true)
                {

                    if (txt_transaction_no.Text == "")
                    {
                        alert("Please enter transaction no.");
                    }
                    else if (ddl_transactiontype.Text == "Select")
                    {
                        alert("Please select type of transaction");
                    }
                    else if (ViewState["payment_slip"].ToString() == "#")
                    {
                        alert("Please upload payment slip");
                    }
                    else
                    {
                        save_data("2");
                    }
                }
                else
                {
                    save_data("2");
                }
            }
        }

        protected void btn_prev_dt_Click(object sender, EventArgs e)
        {
            save_data("3");
        }

        protected void btn_fther_dt_Click(object sender, EventArgs e)
        {
            if (txt_father_first_name.Text == "")
            {
                txt_father_first_name.Focus();
                alert("Please enter father first name");
            }

            else if (txt_father_mobile.Text == "")
            {
                txt_father_mobile.Focus();
                alert("Please enter father mobile no.");
            }
            else if (txt_annual_income.Text == "")
            {
                txt_annual_income.Focus();
                alert("Please enter father annual income.");
            }
            else
            {
                save_data("4");
            }
        }

        protected void btn_mther_dt_Click(object sender, EventArgs e)
        {
            if (txt_mother_first_name.Text == "")
            {
                txt_mother_first_name.Focus();
                alert("Please enter mother first name.");
            }

            else
            {
                save_data("5");
            }
        }
        protected void btn_add_dt_Click(object sender, EventArgs e)
        {
            //if (txt_adress.Text == "")
            //{
            //    txt_adress.Focus();
            //    alert("Please enter present address.");
            //}
            //else if (txt_present_po.Text == "")
            //{
            //    txt_present_po.Focus();
            //    alert("Please enter present post office.");
            //}
            //else if (txt_present_district.Text == "")
            //{
            //    txt_present_district.Focus();
            //    alert("Please enter present district.");
            //}
            //else if (txt_city.Text == "")
            //{
            //    txt_city.Focus();
            //    alert("Please enter present city.");
            //}
            //else if (txt_State.Text == "")
            //{
            //    txt_State.Focus();
            //    alert("Please enter present state.");
            //}
            //else if (txt_pincode.Text == "")
            //{
            //    txt_pincode.Focus();
            //    alert("Please enter present pincode.");
            //}
            ////===
            //else if (txt_pAddress.Text == "")
            //{
            //    txt_pAddress.Focus();
            //    alert("Please enter permanent address.");
            //}
            //else if (txt_perma_po.Text == "")
            //{
            //    txt_perma_po.Focus();
            //    alert("Please enter permanent post office.");
            //}
            //else if (txt_perma_disctrict.Text == "")
            //{
            //    txt_perma_disctrict.Focus();
            //    alert("Please enter permanent district.");
            //}
            //else if (txt_pcity.Text == "")
            //{
            //    txt_pcity.Focus();
            //    alert("Please enter permanent city.");
            //}
            //else if (txt_Pstate.Text == "")
            //{
            //    txt_Pstate.Focus();
            //    alert("Please enter permanent state.");
            //}
            //else if (txt_Ppincod.Text == "")
            //{
            //    txt_Ppincod.Focus();
            //    alert("Please enter permanent pincode.");
            //}
            //else
            //{
            save_data("6");
            //}
        }

        protected void btn_misc_dt_Click(object sender, EventArgs e)
        {
            save_data("7");
        }
        protected void btn_final_submit_Click(object sender, EventArgs e)
        {

            save_data("8");


        }
        protected void btn_cack_std_Click(object sender, EventArgs e)
        {
            process_active("1");
        }

        protected void btn_back_pre_school_Click(object sender, EventArgs e)
        {
            process_active("2");
        }

        protected void btn_back_father_Click(object sender, EventArgs e)
        {
            process_active("3");
        }

        protected void btn_back_mother_Click(object sender, EventArgs e)
        {
            process_active("4");
        }
        protected void btn_back_add_Click(object sender, EventArgs e)
        {
            process_active("5");
        }
        protected void btn_back_misc_Click(object sender, EventArgs e)
        {
            process_active("6");
        }

        protected void btn_back_doc_Click(object sender, EventArgs e)
        {
            process_active("7");
        }

        private void process_active(string type)
        {
            if (type == "1")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");

                pro1.Attributes.Add("class", "steps-root");

                pronumS2.Attributes.Add("class", "steps-bx-number");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p");
                pro2.Attributes.Add("class", "steps-root");

                pronumS3.Attributes.Add("class", "steps-bx-number");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p");
                pro3.Attributes.Add("class", "steps-root");

                pronumS4.Attributes.Add("class", "steps-bx-number");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p");
                pro4.Attributes.Add("class", "steps-root");

                pronumS5.Attributes.Add("class", "steps-bx-number");
                prontxt5.Attributes.Add("class", "steps-bx-txt-p");
                pro5.Attributes.Add("class", "steps-root");

                pronumS6.Attributes.Add("class", "steps-bx-number");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");

                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");



                pnl_account_details.Visible = true;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "2")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root");

                pronumS3.Attributes.Add("class", "steps-bx-number");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p");
                pro3.Attributes.Add("class", "steps-root");

                pronumS4.Attributes.Add("class", "steps-bx-number");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p");
                pro4.Attributes.Add("class", "steps-root");

                pronumS5.Attributes.Add("class", "steps-bx-number");
                prontxt5.Attributes.Add("class", "steps-bx-txt-p");
                pro5.Attributes.Add("class", "steps-root");

                pronumS6.Attributes.Add("class", "steps-bx-number");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");

                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_account_details.Visible = false;
                pnl_student_details.Visible = true;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "3")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root");

                pronumS4.Attributes.Add("class", "steps-bx-number");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p");
                pro4.Attributes.Add("class", "steps-root");

                pronumS5.Attributes.Add("class", "steps-bx-number");
                prontxt5.Attributes.Add("class", "steps-bx-txt-p");
                pro5.Attributes.Add("class", "steps-root");

                pronumS6.Attributes.Add("class", "steps-bx-number");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");

                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_account_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = true;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "4")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root steps-root-done");

                pronumS4.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro4.Attributes.Add("class", "steps-root");

                pronumS5.Attributes.Add("class", "steps-bx-number");
                prontxt5.Attributes.Add("class", "steps-bx-txt-p");
                pro5.Attributes.Add("class", "steps-root");

                pronumS6.Attributes.Add("class", "steps-bx-number");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");

                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_account_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = true;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "5")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root steps-root-done");

                pronumS4.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro4.Attributes.Add("class", "steps-root steps-root-done");

                pronumS5.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt5.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro5.Attributes.Add("class", "steps-root");

                pronumS6.Attributes.Add("class", "steps-bx-number");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");

                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_account_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = true;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "6")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root steps-root-done");

                pronumS4.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro4.Attributes.Add("class", "steps-root steps-root-done");

                pronumS5.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt5.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro5.Attributes.Add("class", "steps-root steps-root-done");

                pronumS6.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");

                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_account_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = true;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "7")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root steps-root-done");

                pronumS4.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro4.Attributes.Add("class", "steps-root steps-root-done");

                pronumS5.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt5.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro5.Attributes.Add("class", "steps-root steps-root-done");

                pronumS6.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro6.Attributes.Add("class", "steps-root steps-root-done");

                pronumS7.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro7.Attributes.Add("class", "steps-root");

                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_account_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = true;
                pnl_docs.Visible = false;
            }
            else if (type == "8" || type == "9")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root steps-root-done");

                pronumS4.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro4.Attributes.Add("class", "steps-root steps-root-done");

                pronumS5.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt5.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro5.Attributes.Add("class", "steps-root steps-root-done");

                pronumS6.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro6.Attributes.Add("class", "steps-root steps-root-done");

                pronumS7.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro7.Attributes.Add("class", "steps-root steps-root-done");

                pronumS8.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p stps-success-name");

                pnl_account_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = true;
            }
        }
        #endregion


        #region UploaD
        private string upload_image(FileUpload Files, string name)
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = name + date + time;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            Session["WorkingImage"] = Files.FileName;
            string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
            Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
            string[] allowedExtension = { ".png", ".jpeg", ".jpg", ".JPEG" };
            for (int i = 0; i < allowedExtension.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtension[i])
                {
                    FileOK = true;
                    lblmessage.Text = "";
                    break;
                }
            }


            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student/")).ToString();
                    Files.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    lblmessage.Text = "File has not save.";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
            else
            {

            }
            if (FileSaved)
            {
                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                string fileName = Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = originalPath1 + "/Master_Img/Student/" + fileName;
            }
            return dbfilePath;
        }
        #endregion

        protected void btn_Submit_Click(object sender, EventArgs e)
        {

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Registration_id = (Label)row.FindControl("lbl_Registration_id");
            Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
            hd_applicationid.Value = lbl_Registration_id.Text;
            ddl_session.SelectedValue = lbl_session_id.Text;

            save_data("1");

        }

        protected void btn_upload_student_image_Click(object sender, EventArgs e)
        {
            string filepath1 = "";
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 200000)
                {
                    filepath1 = upload_image(FileUpload1, "student_img");
                    if (filepath1 == "")
                    {
                        alert("Please upload valid student image.");
                        FileUpload1.Focus();
                        return;
                    }
                    else
                    {
                        img_student_image.Visible = true;
                        ViewState["Student_image"] = filepath1;
                        img_student_image.ImageUrl = filepath1;
                    }

                }
                else
                {
                    alert("Please Reduce or compress size of student image max(500kb)");
                    FileUpload1.Focus();
                }
            }
            else
            {
                alert("Please upload valid student image.");

            }
        }

        protected void btn_upload_father_sig_Click(object sender, EventArgs e)
        {
            string filepath2 = "";
            if (FileUpload2.HasFile)
            {
                if (FileUpload2.FileBytes.Length < 200000)
                {
                    filepath2 = upload_image(FileUpload2, "father_sig");
                    if (filepath2 == "")
                    {
                        alert("Please upload valid father's signature.");
                        FileUpload1.Focus();
                        return;
                    }
                    else
                    {
                        img_father_sig.Visible = true;
                        ViewState["Father_sig"] = filepath2;
                        img_father_sig.ImageUrl = filepath2;
                    }

                }
                else
                {
                    alert("Please Reduce or compress size of father's signature max(500kb)");
                    FileUpload1.Focus();
                }
            }
            else
            {
                alert("Please upload valid father's signature.");

            }
        }

        protected void btn_mother_signature_Click(object sender, EventArgs e)
        {
            string filepath3 = "";
            if (FileUpload3.HasFile)
            {
                if (FileUpload3.FileBytes.Length < 200000)
                {
                    filepath3 = upload_image(FileUpload3, "mother_sig");
                    if (filepath3 == "")
                    {
                        alert("Please upload valid mother's signature.");
                        FileUpload3.Focus();
                        return;
                    }
                    else
                    {
                        img_mother_signature.Visible = true;
                        ViewState["mother_sig"] = filepath3;
                        img_mother_signature.ImageUrl = filepath3;
                    }

                }
                else
                {
                    alert("Please Reduce or compress size of father's signature max(500kb)");
                    FileUpload3.Focus();
                }
            }
            else
            {
                alert("Please upload valid mother's signature.");

            }
        }

        protected void rd_payment_mode_online_CheckedChanged(object sender, EventArgs e)
        {
            paymentid1.Visible = false;
            paymentid2.Visible = false;
        }

        protected void rd_payment_mode_ofline_CheckedChanged(object sender, EventArgs e)
        {
            paymentid1.Visible = true;
            paymentid2.Visible = true;
        }

        protected void btn_payment_slip_Click(object sender, EventArgs e)
        {
            string filepath4 = "";
            if (FileUpload4.HasFile)
            {
                if (FileUpload4.FileBytes.Length < 200000)
                {
                    filepath4 = upload_image(FileUpload4, "payment_slip");
                    if (filepath4 == "")
                    {
                        alert("Please upload valid payment slip");
                        FileUpload4.Focus();
                        return;
                    }
                    else
                    {
                        img_payment_slip.Visible = true;
                        ViewState["payment_slip"] = filepath4;
                        img_payment_slip.ImageUrl = filepath4;
                    }
                }
                else
                {
                    alert("Please Reduce or compress size of payment slip max(500kb)");
                    FileUpload4.Focus();
                }
            }
            else
            {
                alert("Please upload valid payment slip.");

            }
        }

        protected void btn_mothers_photo_Click(object sender, EventArgs e)
        {
            string mother_photo = "";
            if (file_mother_photo.HasFile)
            {
                if (file_mother_photo.FileBytes.Length < 500000)
                {
                    mother_photo = upload_image(file_mother_photo, "mother_photo");
                    if (mother_photo == "")
                    {
                        alert("Please upload valid passport size photo of Mother.");
                        file_mother_photo.Focus();
                        return;
                    }
                    else
                    {
                        img_mother_photo.Visible = true;
                        ViewState["mother_photo"] = mother_photo;
                        img_mother_photo.ImageUrl = mother_photo;
                    }

                }
                else
                {
                    alert("Please Reduce or compress size of mother's photo max(500kb)");
                    file_mother_photo.Focus();
                }
            }
            else
            {
                alert("Please upload valid passport size photo of Mother.");

            }
        }

        protected void btn_upload_father_photo_Click(object sender, EventArgs e)
        {
            string father_photo = "";
            if (file_father_photo.HasFile)
            {
                if (file_father_photo.FileBytes.Length < 500000)
                {
                    father_photo = upload_image(file_father_photo, "father_photo");
                    if (father_photo == "")
                    {
                        alert("Please upload valid passport size photo of Father.");
                        file_father_photo.Focus();
                        return;
                    }
                    else
                    {
                        img_father_photo.Visible = true;
                        ViewState["father_photo"] = father_photo;
                        img_father_photo.ImageUrl = father_photo;
                    }

                }
                else
                {
                    alert("Please Reduce or compress size of father's photo max(500kb)");
                    file_father_photo.Focus();
                }
            }
            else
            {
                alert("Please upload valid passport size photo of Father.");

            }
        }

        protected void btn_upload_Family_photo_Click(object sender, EventArgs e)
        {
            string Family_photo = "";
            if (file_upload_Family_photo.HasFile)
            {
                if (file_upload_Family_photo.FileBytes.Length < 500000)
                {
                    Family_photo = upload_image(file_upload_Family_photo, "Family_photo");
                    if (Family_photo == "")
                    {
                        alert("Please upload valid Family photo(Father,Mother,Ward)");
                        file_upload_Family_photo.Focus();
                        return;
                    }
                    else
                    {
                        img_Family_photo.Visible = true;
                        ViewState["Family_photo"] = Family_photo;
                        img_Family_photo.ImageUrl = Family_photo;
                    }

                }
                else
                {
                    alert("Please Reduce or compress size of  Family photo max(500kb)");
                    file_upload_Family_photo.Focus();
                }
            }
            else
            {
                alert("Please upload valid Family photo(Father,Mother,Ward)");
                file_upload_Family_photo.Focus();

            }

        }

        protected void btn_upload_birth_certificate_Click(object sender, EventArgs e)
        {
            string birth_certificate = "";
            if (file_birth_Certificate.HasFile)
            {
                if (file_birth_Certificate.FileBytes.Length < 500000)
                {
                    birth_certificate = upload_image(file_birth_Certificate, "birth_certificate");
                    if (birth_certificate == "")
                    {
                        alert("Please upload valid birth certificate of the child");
                        file_birth_Certificate.Focus();
                        return;
                    }
                    else
                    {
                        img_Birth_Certificate.Visible = true;
                        ViewState["birth_certificate"] = birth_certificate;
                        img_Birth_Certificate.ImageUrl = birth_certificate;
                    }

                }
                else
                {
                    alert("Please Reduce or compress size of birth certificate of the child max(500kb)");
                    file_birth_Certificate.Focus();
                }
            }
            else
            {
                alert("Please upload valid birth certificate of the child");
                file_birth_Certificate.Focus();

            }

        }

        protected void btn_upload_Residential_Certificate_Click(object sender, EventArgs e)
        {
            string residential_certificate = "";
            if (file_Residential_Certificate.HasFile)
            {
                if (file_Residential_Certificate.FileBytes.Length < 500000)
                {
                    residential_certificate = upload_image(file_Residential_Certificate, "residential_certificate");
                    if (residential_certificate == "")
                    {
                        alert("Please upload valid residential certificate ");
                        file_Residential_Certificate.Focus();
                        return;
                    }
                    else
                    {
                        img_Residential_Certificate.Visible = true;
                        ViewState["residential_certificate"] = residential_certificate;
                        img_Residential_Certificate.ImageUrl = residential_certificate;
                    }

                }
                else
                {
                    alert("Please Reduce or compress size of residential certificate max(500kb)");
                    file_Residential_Certificate.Focus();
                }
            }
            else
            {
                alert("Please upload valid residential certificate");
                file_Residential_Certificate.Focus();

            }
        }



    }
}