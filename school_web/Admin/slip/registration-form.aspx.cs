using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class registration_form : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["form_no"] != null)
                {
                    ViewState["form_no"] = Request.QueryString["form_no"];
                    fetch_student_details(ViewState["form_no"].ToString());

                }
                Bind_schoolinfo();
            }
        }

        private void fetch_student_details(string form_no)
        {
            DataTable dt = mycode.FillData("select * from Form_sale_details where Form_no='" + form_no + "'");
            if (dt.Rows.Count > 0)
            {
                try
                {
                    lbl_date_exam.Text = dt.Rows[0]["Exam_date"].ToString();
                }
                catch
                {
                    lbl_date_exam.Text = "";
                }
                try
                {
                    lbl_Second_Language.Text = dt.Rows[0]["Second_Language"].ToString();
                }
                catch
                {

                }



                lbl_application_no.Text = form_no;
                lbl_adm_class.Text = dt.Rows[0]["class"].ToString();
                lbl_candidates_name.Text = dt.Rows[0]["student_name"].ToString();
                lbl_gender.Text = dt.Rows[0]["gender"].ToString();
                lbl_dob.Text = dt.Rows[0]["dob"].ToString();
                lbl_category.Text = dt.Rows[0]["cast"].ToString();
                lbl_father_name.Text = dt.Rows[0]["fathers_name"].ToString();

                if (dt.Rows[0]["Mothers_name"].ToString() == "NA")
                {
                    lbl_mother_name.Text = "";
                }
                else
                {
                    lbl_mother_name.Text = dt.Rows[0]["Mothers_name"].ToString();
                }


                lbl_p_address.Text = dt.Rows[0]["Address"].ToString();


                lbl_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                lbl_height.Text = dt.Rows[0]["Height"].ToString();
                lbl_weight.Text = dt.Rows[0]["Weight"].ToString();
                lbl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                lbl_religion.Text = dt.Rows[0]["Religion"].ToString();
                lbl_aadhar_no.Text = dt.Rows[0]["Aadhar_no"].ToString();
                lbl_mother_tongue.Text = dt.Rows[0]["Mother_tongue"].ToString();
                lbl_language_spoken.Text = dt.Rows[0]["Language_spoken_at_home"].ToString();

                lbl_name_of_sibling.Text = dt.Rows[0]["Name_of_sibling1"].ToString();
                lbl_age_of_sibling.Text = dt.Rows[0]["Age_of_sibling1"].ToString();
                lbl_school_of_sibling.Text = dt.Rows[0]["School_of_sibling1"].ToString();
                lbl_class_of_sibling.Text = dt.Rows[0]["Class_of_sibling1"].ToString();

                lbl_name_of_sibling1.Text = dt.Rows[0]["Name_of_sibling2"].ToString();
                lbl_age_of_sibling1.Text = dt.Rows[0]["Age_of_sibling2"].ToString();
                lbl_school_of_sibling1.Text = dt.Rows[0]["School_of_sibling2"].ToString();
                lbl_class_of_sibling1.Text = dt.Rows[0]["Class_of_sibling2"].ToString();

                lbl_prev_school_name.Text = dt.Rows[0]["Prv_school_name1"].ToString();
                lbl_prev_school_address.Text = dt.Rows[0]["Prv_school_add1"].ToString();
                lbl_prev_school_from_class.Text = dt.Rows[0]["Prv_class_from"].ToString();
                lbl_prev_school_to_class.Text = dt.Rows[0]["Prv_class_to1"].ToString();
                lbl_prev_school_from_year.Text = dt.Rows[0]["Prv_year_from1"].ToString();
                lbl_prev_school_to_year.Text = dt.Rows[0]["Prv_year_to1"].ToString();
                lbl_prev_school_board.Text = dt.Rows[0]["Prv_board1"].ToString();
                lbl_medium.Text = dt.Rows[0]["Prv_medium"].ToString();
                lbl_percent.Text = dt.Rows[0]["Mark_percent1"].ToString();

                lbl_prev_school_name1.Text = dt.Rows[0]["Prv_school_name2"].ToString();
                lbl_prev_school_address1.Text = dt.Rows[0]["Prv_school_add2"].ToString();
                lbl_prev_school_from_class1.Text = dt.Rows[0]["Prv_class_from2"].ToString();
                lbl_prev_school_to_class1.Text = dt.Rows[0]["Prv_class_to2"].ToString();
                lbl_prev_school_from_year1.Text = dt.Rows[0]["Prv_year_from2"].ToString();
                lbl_prev_school_to_year1.Text = dt.Rows[0]["Prv_year_to2"].ToString();
                lbl_prev_school_board1.Text = dt.Rows[0]["Prv_board2"].ToString();
                lbl_medium1.Text = dt.Rows[0]["Prv_medium2"].ToString();
                lbl_percent1.Text = dt.Rows[0]["Mark_percent2"].ToString();




                lbl_f_aadhar.Text = dt.Rows[0]["F_aadhar_no"].ToString();
                lbl_f_qualification.Text = dt.Rows[0]["F_qualification"].ToString();

                try
                {
                    if (dt.Rows[0]["F_occupation"].ToString() == "Select")
                    {
                        lbl_f_occupation.Text = "";
                    }
                    else if (dt.Rows[0]["F_occupation"].ToString().ToUpper() == "OTHERS")
                    {
                        lbl_f_occupation.Text = "";
                    }
                    else
                    {
                        lbl_f_occupation.Text = dt.Rows[0]["F_occupation"].ToString();
                    }
                }
                catch
                {

                }

                try
                {


                    if (dt.Rows[0]["M_occupation"].ToString() == "Select")
                    {
                        lbl_m_occupation.Text = "";
                    }
                    else if (dt.Rows[0]["M_occupation"].ToString().ToUpper() == "OTHERS")
                    {
                        lbl_m_occupation.Text = "";
                    }
                    else
                    {
                        lbl_m_occupation.Text = dt.Rows[0]["M_occupation"].ToString();
                    }
                }
                catch
                {

                }

                try
                {


                    if (dt.Rows[0]["Guardian_occupation"].ToString() == "Select")
                    {
                        lbl_g_occupation.Text = "";
                    }
                    else if (dt.Rows[0]["Guardian_occupation"].ToString().ToUpper() == "OTHERS")
                    {
                        lbl_g_occupation.Text = "";
                    }
                    else
                    {
                        lbl_g_occupation.Text = dt.Rows[0]["Guardian_occupation"].ToString();
                    }
                }
                catch
                {

                }

                lbl_f_annual_income.Text = dt.Rows[0]["F_annual_income"].ToString();
                lbl_f_contact_no.Text = dt.Rows[0]["F_contact_no"].ToString();
                lbl_f_email.Text = dt.Rows[0]["F_email_id"].ToString();


                lbl_m_aadhar.Text = dt.Rows[0]["M_aadhar_no"].ToString();
                lbl_m_qualification.Text = dt.Rows[0]["M_qualification"].ToString();





                lbl_m_annual_income.Text = dt.Rows[0]["M_annual_income"].ToString();
                lbl_m_contact_no.Text = dt.Rows[0]["M_contact_no"].ToString();
                lbl_m_email.Text = dt.Rows[0]["M_email_id"].ToString();

                lbl_guardian_name.Text = dt.Rows[0]["Guardian_first_name"].ToString() + " " + dt.Rows[0]["Guardian_middle_name"].ToString() + " " + dt.Rows[0]["Guardian_last_name"].ToString();
                lbl_g_aadhar.Text = dt.Rows[0]["Guardian_aadhar_no"].ToString();
                lbl_g_qualification.Text = dt.Rows[0]["Guardian_qualification"].ToString();

                lbl_g_annual_income.Text = dt.Rows[0]["Guardian_annual_income"].ToString();
                lbl_g_contact_no.Text = dt.Rows[0]["Guardian_contact_no"].ToString();
                lbl_g_email.Text = dt.Rows[0]["Guardian_email_id"].ToString();



                // lbl_p_address.Text = dt.Rows[0]["Address"].ToString() + ", P.O. : " + dt.Rows[0]["Post_office"].ToString() + ", P.S. : " + dt.Rows[0]["Police_station"].ToString() + ", District : " + dt.Rows[0]["District"].ToString() + ", City : " + dt.Rows[0]["City"].ToString() + ", State : " + dt.Rows[0]["State"].ToString() + ", Pin Code : " + dt.Rows[0]["Pin_code"].ToString() + ", Country : " + dt.Rows[0]["Country"].ToString() + ", Mobile No. : " + dt.Rows[0]["Add_mobile_no"].ToString();

                string Address = "", Post_office = "", Police_station = "", District = "", City = "", State = "", Pin_code = "", Country = "", Add_mobile_no = "";

                try
                {
                    if (dt.Rows[0]["Address"].ToString() != "")
                    {


                        Address = dt.Rows[0]["Address"].ToString();
                    }

                    if (dt.Rows[0]["Post_office"].ToString() != "")
                    {

                        Post_office = ", P.O. : " + dt.Rows[0]["Post_office"].ToString();
                    }

                    if (dt.Rows[0]["Police_station"].ToString() != "")
                    {

                        Police_station = ", P.S. : " + dt.Rows[0]["Police_station"].ToString();
                    }
                    if (dt.Rows[0]["District"].ToString() != "")
                    {
                        District = ", District : " + dt.Rows[0]["District"].ToString();


                    }
                    if (dt.Rows[0]["State"].ToString() != "Select")
                    {
                        State = ", State : " + dt.Rows[0]["State"].ToString();


                    }

                    if (dt.Rows[0]["Pin_code"].ToString() != "")
                    {
                        Pin_code = ", Pin Code: " + dt.Rows[0]["Pin_code"].ToString();

                    }
                    if (dt.Rows[0]["Country"].ToString() != "Select")
                    {
                        Country = ", Country: " + dt.Rows[0]["Country"].ToString();


                    }
                    if (dt.Rows[0]["Add_mobile_no"].ToString() != "")
                    {
                        Add_mobile_no = ", Mobile No.: " + dt.Rows[0]["Add_mobile_no"].ToString();

                    }
                    string a = (Address + Post_office + Police_station + District + State + Pin_code + Country + Add_mobile_no).TrimStart(',');
                    lbl_p_address.Text = a;



                }
                catch
                {

                }



                try
                {
                    string age = get_agess(Convert.ToDateTime(dt.Rows[0]["dob"].ToString()));
                    lbl_age.Text = age;
                }
                catch (Exception ex)
                {
                }

            }
            else
            {
            }
        }

        private string get_agess(DateTime dateTime)
        {
            try
            {
                DateTime today = Convert.ToDateTime(mycode.date());
                DateTime dob = dateTime;
                TimeSpan ts = today - dob;
                DateTime age = DateTime.MinValue + ts;
                int years = age.Year - 1;
                int months = age.Month - 1;
                int days = age.Day - 1;
                string agesss = years + " years " + months + " months  " + days + " days";
                return agesss;
            }
            catch (Exception ex)
            {
                return "";
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
                try
                {
                    if (dt.Rows[0]["Is_Exam_date_show_Form_sale"].ToString() == "1")
                    {
                        ViewState["isexamdate"] = "1";
                        doe.Visible = true;
                    }
                    else
                    {
                        ViewState["isexamdate"] = "0";
                        doe.Visible = false;
                    }

                }
                catch
                {
                    ViewState["isexamdate"] = "0";
                    doe.Visible = false;
                }


                try
                {
                    if (dt.Rows[0]["firm_id"].ToString() == "HIS-001")
                    { }
                    else
                    {
                        if (dt.Rows[0]["Is_slip_header"].ToString() == "True")
                        {
                            textheader.Visible = false;
                            printheader.Visible = true;
                            img_header.Visible = true;
                            img_header.ImageUrl = dt.Rows[0]["Header_images"].ToString();
                        }
                        else
                        {
                            printheader.Visible = false;
                            textheader.Visible = true;
                            printheader.Visible = printheader.Visible = false;
                        }
                    }
                }
                catch
                {
                    textheader.Visible = true;
                    printheader.Visible = false;
                }




                Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                img_watermark.ImageUrl = dt.Rows[0]["Watermark_image"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_email.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
                lbl_estd.Text = "ESTD : " + dt.Rows[0]["estd"].ToString();
                lbl_aff_text.Text = dt.Rows[0]["Affiliated_by_full_text"].ToString();
                lbl_aff_no.Text = "Affiliation No.  : " + dt.Rows[0]["Affiliation"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();


                if (dt.Rows[0]["firm_id"].ToString() == "CKG-01" || dt.Rows[0]["firm_id"].ToString() == "RRKPS-01")
                {
                    Sibling_Details.Visible = false;
                    Previous_Classes.Visible = false;
                    Achievement.Visible = false;
                    about_School.Visible = false;
                    subject_taken.Visible = false; 

                    personaldetails.Attributes["style"] = String.Format("height:765px;");
                    Achievementid.Attributes["style"] = String.Format("height:807px;");
                    subject_takenpnl.Attributes["style"] = String.Format("height:807px;"); 
                }
                else
                {
                    personaldetails.Attributes["style"] = String.Format("height:1085px;");
                    Achievementid.Attributes["style"] = String.Format("height:1300px;");
                    subject_takenpnl.Attributes["style"] = String.Format("height:1185px;");
                    Sibling_Details.Visible = true;
                    Previous_Classes.Visible = true;
                    Achievement.Visible = true;
                    about_School.Visible = true;
                    subject_taken.Visible = true;
                }


                //===================================
                fPhoneNodV.Visible = false;
                localGuardianNo.Visible = false;
                if (dt.Rows[0]["firm_id"].ToString() == "HIS-001")
                {
                    personaldetails.Attributes["style"] = String.Format("height:1085px;");
                    Achievementid.Attributes["style"] = String.Format("height:1300px;");
                    subject_takenpnl.Attributes["style"] = String.Format("height:1185px;");
                    fPhoneNodV.Visible = true;
                    localGuardianNo.Visible = true;
                    Knowabout_School.Visible = true;
                    docSubmitted1.Visible = true; 
                    Achievement.Visible = false;
                    Previous_Classes.Visible = false;
                    parentAccNoDv.Visible = false;
                    languageSpokentAtHomeDv.Visible = false;
                    FannualIncome.Visible = false;
                    FContactNo.Visible = false;
                    FannualIncome2.Visible = false;
                    FContactNo2.Visible = false;
                    FannualIncome3.Visible = false;
                    FContactNo3.Visible = false;
                    about_School.Visible = false;
                    docSubmitted.Visible = false;
                    subject_taken.Visible = false;
                    lbl_aff_no.Visible = false;
                    FannualIncome1.Visible = false;
                    FContactNo1.Visible = false; 
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../form-sale.aspx", false);
        }
    }
}