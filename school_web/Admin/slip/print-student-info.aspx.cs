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
    public partial class print_student_info : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["admNo"] != null && Request.QueryString["ssion"] != null && Request.QueryString["clss"] != null)
                {
                    ViewState["admNo"] = Request.QueryString["admNo"];
                    ViewState["ssion"] = Request.QueryString["ssion"];
                    ViewState["clss"] = Request.QueryString["clss"];
                    fetch_student_details(ViewState["admNo"].ToString(), ViewState["ssion"].ToString(), ViewState["clss"].ToString());
                }
                Bind_schoolinfo();
            }
        }


        private void fetch_student_details(string admNo, string session_id, string class_id)
        {
            DataTable dt = mycode.FillData("select *,(select top 1 house_name from house_master where house_id=admission_registor.house) as House_name,(select top 1 Course_Name from Add_course_table where course_id=admission_registor.Old_class_id) as Old_class_name from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and admissionserialnumber='" + admNo + "'");
            if (dt.Rows.Count > 0)
            {
                fetch_images(admNo, session_id); fetch_subjectTaken(admNo, session_id, class_id, dt.Rows[0]["Section"].ToString());
                lbl_std_type.Text = dt.Rows[0]["Transfer_Status"].ToString();
                lbl_date_of_admission.Text = dt.Rows[0]["dateofadmission"].ToString();
                lbl_session.Text = dt.Rows[0]["session"].ToString();
                lbl_house.Text = dt.Rows[0]["House_name"].ToString();
                lbl_adm_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_class.Text = dt.Rows[0]["class"].ToString();
                lbl_section.Text = dt.Rows[0]["Section"].ToString();
                lbl_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
                lbl_first_name.Text = dt.Rows[0]["studentname"].ToString();
                //lbl_first_name.Text = dt.Rows[0]["Student_Name_First"].ToString();
                //lbl_middle_name.Text = dt.Rows[0]["Student_Name_First"].ToString();

                lbl_dob.Text = dt.Rows[0]["dob"].ToString();
                lbl_gender.Text = dt.Rows[0]["gender"].ToString();
                lbl_category.Text = dt.Rows[0]["cast"].ToString();
                lbl_religion.Text = dt.Rows[0]["religion"].ToString();
                lbl_blood_group.Text = dt.Rows[0]["blood_group"].ToString();
                lbl_nationality.Text = dt.Rows[0]["Student_nationality"].ToString();
                lbl_mother_toung.Text = dt.Rows[0]["student_mother_tounge"].ToString();
                lbl_second_language.Text = dt.Rows[0]["Second_Language"].ToString();
                lbl_place_of_birth.Text = dt.Rows[0]["place_of_birth"].ToString();
                lbl_are_there_medical_problem.Text = dt.Rows[0]["illness_remark"].ToString();

                ///==================== 
                lbl_address1.Text = dt.Rows[0]["careof"].ToString();
                lbl_po1.Text = dt.Rows[0]["postoffice"].ToString();
                lbl_ps1.Text = dt.Rows[0]["policestation"].ToString();
                lbl_dist1.Text = dt.Rows[0]["district"].ToString();
                lbl_state1.Text = dt.Rows[0]["state"].ToString();
                lbl_pincode1.Text = dt.Rows[0]["pin"].ToString();
                lbl_tel1.Text = dt.Rows[0]["mobilenumber"].ToString();
                lbl_email1.Text = dt.Rows[0]["Residential_email"].ToString();
                lbl_emerg_contact1.Text = dt.Rows[0]["Residential_emergency_contact_no"].ToString();

                ///==================== 
                lbl_address1.Text = dt.Rows[0]["careof"].ToString();
                lbl_po1.Text = dt.Rows[0]["postoffice"].ToString();
                lbl_ps1.Text = dt.Rows[0]["policestation"].ToString();
                lbl_dist1.Text = dt.Rows[0]["district"].ToString();
                lbl_state1.Text = dt.Rows[0]["state"].ToString();
                lbl_pincode1.Text = dt.Rows[0]["pin"].ToString();
                lbl_tel1.Text = dt.Rows[0]["mobilenumber"].ToString();
                lbl_email1.Text = dt.Rows[0]["Residential_email"].ToString();
                lbl_emerg_contact1.Text = dt.Rows[0]["Residential_emergency_contact_no"].ToString();

                lbl_address2.Text = dt.Rows[0]["careof_permanent"].ToString();
                lbl_po2.Text = dt.Rows[0]["postoffice_permanent"].ToString();
                lbl_ps2.Text = dt.Rows[0]["policestation_permanent"].ToString();
                lbl_dist2.Text = dt.Rows[0]["district_permanent"].ToString();
                lbl_state2.Text = dt.Rows[0]["state_permanent"].ToString();
                lbl_pincode2.Text = dt.Rows[0]["pincode"].ToString();
                lbl_tel2.Text = dt.Rows[0]["mob2"].ToString();
                lbl_email2.Text = dt.Rows[0]["Corresp_email_id"].ToString();
                lbl_emerg_contact2.Text = dt.Rows[0]["Corresp_emergency_contact_no"].ToString();
                lbl_hobbies.Text = dt.Rows[0]["Hobbie_of_student"].ToString();


                lbl_prev_school.Text = dt.Rows[0]["Prev_school_name"].ToString();
                lbl_last_class_attended.Text = dt.Rows[0]["Old_class_name"].ToString();
                lbl_any_achievement.Text = dt.Rows[0]["Prev_any_achievement"].ToString();
                lbl_prev_eng_FM.Text = dt.Rows[0]["Prev_eng_FM"].ToString();
                lbl_prev_eng_OM.Text = dt.Rows[0]["Prev_eng_OM"].ToString();
                lbl_prev_hin_FM.Text = dt.Rows[0]["Prev_hin_FM"].ToString();
                lbl_prev_hin_OM.Text = dt.Rows[0]["Prev_hin_OM"].ToString();
                lbl_prev_math_FM.Text = dt.Rows[0]["Prev_math_FM"].ToString();
                lbl_prev_math_OM.Text = dt.Rows[0]["Prev_math_OM"].ToString();
                lbl_prev_sc_FM.Text = dt.Rows[0]["Prev_sc_FM"].ToString();
                lbl_prev_sc_OM.Text = dt.Rows[0]["Prev_sc_OM"].ToString();
                lbl_prev_sci_FM.Text = dt.Rows[0]["Prev_sci_FM"].ToString();
                lbl_prev_sci_OM.Text = dt.Rows[0]["Prev_sci_OM"].ToString();

                ///================================================
                lbl_f_name.Text = dt.Rows[0]["fathername"].ToString();
                lbl_f_age.Text = dt.Rows[0]["Father_age"].ToString();
                lbl_f_nationality.Text = dt.Rows[0]["f_nationality"].ToString();
                lbl_f_edu_qualification.Text = dt.Rows[0]["fatherqualification"].ToString();
                lbl_f_institution.Text = dt.Rows[0]["Father_institution"].ToString();
                lbl_f_organization.Text = dt.Rows[0]["Father_organization"].ToString();
                lbl_f_work_for.Text = dt.Rows[0]["Father_working_for"].ToString();
                lbl_f_office_add.Text = dt.Rows[0]["Father_office_address"].ToString();
                lbl_f_designation.Text = dt.Rows[0]["occuption"].ToString();
                lbl_f_annual_income.Text = dt.Rows[0]["parentincome"].ToString();
                lbl_f_Tel.Text = dt.Rows[0]["father_mob"].ToString();
                lbl_f_hour_of_interection.Text = dt.Rows[0]["Father_hour_intection_of_child"].ToString();


                ///================================================
                lbl_m_name.Text = dt.Rows[0]["mothername"].ToString();
                lbl_m_age.Text = dt.Rows[0]["Mother_age"].ToString();
                lbl_m_nationality.Text = dt.Rows[0]["m_nationality"].ToString();
                lbl_m_edu_qualification.Text = dt.Rows[0]["motherqualifiaction"].ToString();
                lbl_m_institution.Text = dt.Rows[0]["Mother_institution"].ToString();
                lbl_m_organization.Text = dt.Rows[0]["Mother_organization"].ToString();
                lbl_m_work_for.Text = dt.Rows[0]["Mother_working_for"].ToString();
                lbl_m_office_add.Text = dt.Rows[0]["Mother_office_address"].ToString();
                lbl_m_designation.Text = dt.Rows[0]["m_occupation"].ToString();
                lbl_m_annual_income.Text = dt.Rows[0]["Mother_annual_income"].ToString();
                lbl_m_Tel.Text = dt.Rows[0]["mother_mob"].ToString();
                lbl_m_hour_of_interection.Text = dt.Rows[0]["Mother_hour_intection_of_child"].ToString();

                lbl_if_parents_are_devorced.Text = dt.Rows[0]["If_parents_are_devorced"].ToString();
                lbl_reason_for_choosing.Text = dt.Rows[0]["Reason_for_choosing_school"].ToString();
                lbl_how_did_you_learn.Text = dt.Rows[0]["Learn_about_school"].ToString();
            }
        }

        private void fetch_subjectTaken(string admNo, string session_id, string class_id, string section)
        {
            subjDV.Visible = false;
            DataTable dtc = My.dataTable("select Is_subj_assign from Add_course_table where course_id='" + class_id + "'");
            if (dtc.Rows.Count > 0)
            {
                if (dtc.Rows[0]["Is_subj_assign"].ToString() == "True")
                {
                    DataTable dt = My.dataTable("select t2.Subject_name,t2.Subject_Code from Subject_Mapping_New t1 join Subject_Master t2 on t1.Class_id=t2.course_id and t1.Sub_id=t2.Subject_id where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Admission_no='" + admNo + "'");
                    if (dt.Rows.Count > 0)
                    {
                        subjDV.Visible = true;
                        rp_subjects.DataSource = dt;
                        rp_subjects.DataBind();
                    }
                }
            }
        }

        private void fetch_images(string admNo, string session_id)
        {
            DataTable dt = My.dataTable("select * from Student_image_new where Session_id='" + session_id + "' and Admission_no='" + admNo + "' and Image_type in('Father_image','Student_image','Mother_image')");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Image_type"].ToString() == "Father_image")
                    {
                        Image2.ImageUrl = dr["Image_path"].ToString();
                        Image2.Visible = true;
                    }
                    if (dr["Image_type"].ToString() == "Mother_image")
                    {
                        Image3.ImageUrl = dr["Image_path"].ToString();
                        Image3.Visible = true;
                    }
                    if (dr["Image_type"].ToString() == "Student_image")
                    {
                        Image4.ImageUrl = dr["Image_path"].ToString();
                        Image4.Visible = true;
                    }
                }
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
            if (dt.Rows.Count > 0)
            {
                try
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
                //lbl_aff_no.Text = "Affiliation No.  : " + dt.Rows[0]["Affiliation"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();

                lbl_shool_name1.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_school_name1.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../student-list.aspx", false);
        }
    }
}