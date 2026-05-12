using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class student_details : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["admNo"] != null && Request.QueryString["ssion"] != null && Request.QueryString["clss"] != null)
                    {
                        string regId = Request.QueryString["admNo"];
                        string ssion = Request.QueryString["ssion"];
                        string clss = Request.QueryString["clss"];
                        fetch_data(regId, ssion, clss); bind_academic(regId, ssion, clss);
                        fetch_images(regId, ssion, clss); Bind_schoolinfo();

                        ViewState["ssionId"] = ssion;
                        ViewState["clssId"] = clss;
                        ViewState["regIdId"] = regId;
                        fetch_submit_doc();
                    }
                    else
                    {
                        Response.Redirect("Home.aspx");
                    }
                }
            }
            catch (Exception exe)
            {
            }
        }


        private void fetch_submit_doc()
        {
            DataTable dt = My.dataTable("select * from Admission_document_submit where Status=1 order by Position asc");
            if (dt.Rows.Count > 0)
            {
                doc_submited.Visible = true;
                rp_document_submit.DataSource = dt;
                rp_document_submit.DataBind();
            }
            else
            {
                rp_document_submit.DataSource = null;
                rp_document_submit.DataBind();
                doc_submited.Visible = false;
            }
        }
        protected void rp_document_submit_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_doc_id = ((Label)e.Item.FindControl("lbl_doc_id")) as Label;
                Label lbl_is_submit = ((Label)e.Item.FindControl("lbl_is_submit")) as Label;
                DataTable dt = My.dataTable("select * from Admission_student_doc_submit where Session_id='" + ViewState["ssionId"].ToString() + "' and Admission_no='" + ViewState["regIdId"].ToString() + "' and Class_id='" + ViewState["clssId"].ToString() + "' and Doc_id='" + lbl_doc_id.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    lbl_is_submit.Text = "Yes";
                }
                else
                {
                    lbl_is_submit.Text = "No";
                }
            }
        }


        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count == 0) { }
            else
            {
                Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_email_school.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
            }
        }
        private void fetch_images(string regId, string ssion, string clss)
        {
            DataTable dt = mycode.FillData("select * from Student_image_new where Admission_no='" + regId + "' and Session_id='" + ssion + "'");
            if (dt.Rows.Count == 0)
            {
                decuments.Visible = false;
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                decuments.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Image_type"].ToString() == "Parent_Sign")
                    {
                        img_father_sig.ImageUrl = dr["Image_path"].ToString();
                    }
                    if (dr["Image_type"].ToString() == "Father_image")
                    {
                        img_father.ImageUrl = dr["Image_path"].ToString();
                    }

                    if (dr["Image_type"].ToString() == "Mother_Sign")
                    {
                        img_mother_sign.ImageUrl = dr["Image_path"].ToString();
                    }
                    if (dr["Image_type"].ToString() == "Mother_image")
                    {
                        img_mother.ImageUrl = dr["Image_path"].ToString();
                    }

                }
            }
        }

        private void fetch_data(string regId, string ssion, string clss)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_Name,(select top 1 Sub_CategoryName from Sub_Category_Details where Sub_CategoryId=admission_registor.SubCategory_id) as Sub_CategoryName,(select top 1 house_name from house_master where house_id=admission_registor.house) as House_name,(select top 1 Course_Name from Add_course_table where course_id=admission_registor.Old_class_id) as Old_Course_Name from admission_registor where Session_id='" + ssion + "' and Class_id='" + clss + "' and admissionserialnumber='" + regId + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                img_s_image.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
                lbl_session1.Text = dt.Rows[0]["session"].ToString();
                lbl_formno.Text = dt.Rows[0]["formserialnumber"].ToString();
                lbl_admissiondate.Text = dt.Rows[0]["dateofadmission"].ToString();
                lbl_session.Text = dt.Rows[0]["father_mob"].ToString();
                lbl_session.Text = dt.Rows[0]["session"].ToString();

                lbl_cat.Text = dt.Rows[0]["Category_Name"].ToString();
                lbl_sub_cat.Text = dt.Rows[0]["Sub_CategoryName"].ToString();
                lbl_pen_no.Text = dt.Rows[0]["Student_pen_no"].ToString();

                if (dt.Rows[0]["hosteltaken"].ToString() == "Yes")
                {
                    lbl_admissionin.Text = "Hostel";
                }
                else
                {
                    lbl_admissionin.Text = "Day Scholar";
                }

                if (dt.Rows[0]["is_applied_dayboarding"].ToString() == "true")
                {
                    lbl_boarding_type.Text = "Day Boarding";
                }
                else if (dt.Rows[0]["day_boarding_with_lunch"].ToString() == "true")
                {
                    lbl_boarding_type.Text = "Day Boarding with Lunch";
                }
                else
                {
                    lbl_boarding_type.Text = "Day Scholar";
                }
                lbl_course.Text = dt.Rows[0]["class"].ToString();
                lbl_section.Text = dt.Rows[0]["Section"].ToString();
                lbl_admissionno.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_rollno.Text = dt.Rows[0]["rollnumber"].ToString();
                lbl_academic_year.Text = dt.Rows[0]["Academic_Sem_or_Year"].ToString();
                if (dt.Rows[0]["transportationtaken"].ToString() == "Yes")
                {
                    lbl_transportation.Text = "Yes";
                }
                else
                {
                    lbl_transportation.Text = "No";
                }

                lbl_student_type.Text = dt.Rows[0]["Transfer_Status"].ToString();
                lbl_uid_no.Text = dt.Rows[0]["UID_No"].ToString();
                lbl_index_no.Text = dt.Rows[0]["Index_no"].ToString();
                lbl_house.Text = dt.Rows[0]["House_name"].ToString();

                //==================
                lbl_student_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_dateofbirth.Text = dt.Rows[0]["dob"].ToString();
                lbl_CertificateNostud.Text = dt.Rows[0]["birth_certificate_number"].ToString();
                lbl_palceof_birth.Text = dt.Rows[0]["place_of_birth"].ToString();
                lbl_bloodgroup.Text = dt.Rows[0]["blood_group"].ToString();
                lbl_aadharno.Text = dt.Rows[0]["aadharno"].ToString();
                lbl_gender.Text = dt.Rows[0]["gender"].ToString();
                lbl_religion.Text = dt.Rows[0]["religion"].ToString();
                lbl_ration_type.Text = dt.Rows[0]["ration_type"].ToString();
                lbl_catogery.Text = dt.Rows[0]["cast"].ToString();
                lbl_caste_jati.Text = dt.Rows[0]["jati"].ToString();
                lbl_certificateno.Text = dt.Rows[0]["cast_certificate_no"].ToString();
                lbl_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();
                lbl_anyillness.Text = dt.Rows[0]["is_illness"].ToString();
                lbl_Illness_Remark.Text = dt.Rows[0]["illness_remark"].ToString();
                lbl_prev_school.Text = dt.Rows[0]["currentschool"].ToString();
                lbl_cast.Text = dt.Rows[0]["cast"].ToString();
                lbl_rte_student.Text = dt.Rows[0]["RTE"].ToString();
                if (lbl_rte_student.Text == "") { lbl_rte_student.Text = ""; }
                else if (lbl_rte_student.Text == "Select") { lbl_rte_student.Text = ""; }
                else
                {
                    lbl_staff_ward.Text = dt.Rows[0]["RTE"].ToString();
                }

                lbl_staff_ward.Text = dt.Rows[0]["staff_ward"].ToString();
                if (lbl_staff_ward.Text == "") { lbl_staff_ward.Text = ""; }
                else if (lbl_staff_ward.Text == "Select") { lbl_staff_ward.Text = ""; }
                else
                {
                    lbl_staff_ward.Text = dt.Rows[0]["staff_ward"].ToString() + " (" + dt.Rows[0]["Staff_employee_code"].ToString() + ")";
                }


                lbl_identy.Text = dt.Rows[0]["Personal_Identymarks"].ToString();
                lbl_nationalty.Text = dt.Rows[0]["Student_nationality"].ToString();


                lbl_height.Text = dt.Rows[0]["Height"].ToString();
                lbl_weight.Text = dt.Rows[0]["Weight"].ToString();

                lbl_sb_name1.Text = dt.Rows[0]["Sibling_name1"].ToString();
                lbl_sb_age1.Text = dt.Rows[0]["Sibling_age1"].ToString();
                lbl_sb_school_name1.Text = dt.Rows[0]["Sibling_school1"].ToString();
                lbl_sb_class1.Text = dt.Rows[0]["Sibling_class1"].ToString();

                lbl_sb_name2.Text = dt.Rows[0]["Sibling_name2"].ToString();
                lbl_sb_age2.Text = dt.Rows[0]["Sibling_age2"].ToString();
                lbl_sb_school_name2.Text = dt.Rows[0]["Sibling_school2"].ToString();
                lbl_sb_class2.Text = dt.Rows[0]["Sibling_class2"].ToString();

                //==========================================
                lbl_last_school.Text = dt.Rows[0]["Prev_school_name"].ToString();
                lbl_prev_admission_date.Text = dt.Rows[0]["Old_Admission_Date"].ToString();
                lbl_prev_board_type.Text = dt.Rows[0]["Prev_board_type"].ToString();
                lbl_prev_board.Text = dt.Rows[0]["Prev_board"].ToString();
                lbl_prev_passout_class.Text = dt.Rows[0]["Old_Course_Name"].ToString();
                lbl_prev_precentage.Text = dt.Rows[0]["Prev_percentage"].ToString();
                lbl_prev_reason_for_shift.Text = dt.Rows[0]["Prev_reason_for_shift"].ToString();
                lbl_prev_year.Text = dt.Rows[0]["Prev_year"].ToString();


                //===================
                lbl_fathername.Text = dt.Rows[0]["fathername"].ToString();
                lbl_occupation.Text = dt.Rows[0]["occuption"].ToString();
                lbl_qulification.Text = dt.Rows[0]["fatherqualification"].ToString();
                lbl_Nationality.Text = dt.Rows[0]["f_nationality"].ToString();
                lbl_martialstatus.Text = dt.Rows[0]["f_marrital_statue"].ToString();
                lbl_mobile_no.Text = dt.Rows[0]["Country_Code_Father"].ToString() + " " + dt.Rows[0]["father_mob"].ToString();
                lbl_emailid.Text = dt.Rows[0]["email_id"].ToString();
                lbl_guardianname.Text = dt.Rows[0]["guardianname"].ToString();
                lbl_parent_income.Text = dt.Rows[0]["parentincome"].ToString();
                lbl_father_aadhar_no.Text = dt.Rows[0]["Father_aadhar_no"].ToString();
                lbl_father_whatsapp.Text = dt.Rows[0]["Father_whatsapp_country_code"].ToString() + " " + dt.Rows[0]["Father_whatsApp_no"].ToString();

                //===================
                lbl_mother_name.Text = dt.Rows[0]["mothername"].ToString();
                lbl_occupation_mother.Text = dt.Rows[0]["m_occupation"].ToString();
                lbl_motherqulification.Text = dt.Rows[0]["motherqualifiaction"].ToString();
                lbl_mother_nationalty.Text = dt.Rows[0]["m_nationality"].ToString();
                lbl_marital_status.Text = dt.Rows[0]["m_marrital_statue"].ToString();
                lbl_mobile_mother.Text = dt.Rows[0]["Country_Code_Mother"].ToString() + " " + dt.Rows[0]["mother_mob"].ToString();
                lbl_emailid_mother.Text = dt.Rows[0]["mother_email"].ToString();
                lbl_mom_whatsapp.Text = dt.Rows[0]["Mother_whatsapp_country_code"].ToString() + " " + dt.Rows[0]["Mother_whatsApp_no"].ToString();
                lbl_mom_aadhar_no.Text = dt.Rows[0]["Mother_aadhar_no"].ToString();
                //=======================
                lbl_current.Text = dt.Rows[0]["careof"].ToString();
                lbl_mobile_no_current.Text = dt.Rows[0]["Country_Code_Current_add"].ToString() + " " + dt.Rows[0]["mobilenumber"].ToString();
                lbl_cityvillage_current.Text = dt.Rows[0]["city"].ToString();
                lbl_distict_current.Text = dt.Rows[0]["district"].ToString();
                lbl_po_current.Text = dt.Rows[0]["postoffice"].ToString();
                lbl_ps_current.Text = dt.Rows[0]["policestation"].ToString();
                lbl_state_current.Text = mycode.getstatename(dt.Rows[0]["state"].ToString());
                lbl_pincode.Text = dt.Rows[0]["pin"].ToString();
                lbl_country.Text = dt.Rows[0]["Present_country"].ToString();

                //==========================
                lbl_permanent_address.Text = dt.Rows[0]["careof_permanent"].ToString();
                lbl_mobile_no_permanent.Text = dt.Rows[0]["Country_Code_Current_Perm_add"].ToString() + " " + dt.Rows[0]["mob2"].ToString();
                lbl_cityvillage_permanent.Text = dt.Rows[0]["city_permanent"].ToString();
                lbl_distict_permanent.Text = dt.Rows[0]["district_permanent"].ToString();
                lbl_po_permanent.Text = dt.Rows[0]["postoffice_permanent"].ToString();
                lbl_ps_permanent.Text = dt.Rows[0]["policestation_permanent"].ToString();
                lbl_state_permanent.Text = mycode.getstatename(dt.Rows[0]["state_permanent"].ToString());
                lbl_pincode_permanent.Text = dt.Rows[0]["pincode"].ToString();
                lbl_countrt_permanent.Text = dt.Rows[0]["Present_country"].ToString();


                //===========================
                lbl_account_holder_name.Text = dt.Rows[0]["Account_Holder_name"].ToString();
                lbl_bankname.Text = dt.Rows[0]["Bnk_Name"].ToString();
                lbl_IFSCCode.Text = dt.Rows[0]["IFSC_Code"].ToString();
                lbl_branch_name.Text = dt.Rows[0]["Branch_Name"].ToString();
                lbl_account_no.Text = dt.Rows[0]["Bank_acount_no"].ToString();


                //==========================
                lbl_hobbie.Text = dt.Rows[0]["Hobbie_of_student"].ToString();
                lbl_last_class_attended.Text = dt.Rows[0]["Prev_class_attended"].ToString();
                lbl_prev_class_pass_fail_status.Text = dt.Rows[0]["Prev_pass_fail_status"].ToString();
                lbl_father_age.Text = dt.Rows[0]["Father_age"].ToString();
                lbl_mother_age.Text = dt.Rows[0]["Mother_age"].ToString();
                lbl_mother_annual_incom.Text = dt.Rows[0]["Mother_annual_income"].ToString();
                lbl_relation_with_student.Text = dt.Rows[0]["Guardian_relation_with_student"].ToString();
                lbl_guardian_occupation.Text = dt.Rows[0]["Guardian_occupation"].ToString();
                lbl_guardian_qualification.Text = dt.Rows[0]["Guardian_qualification"].ToString();
                lbl_guardian_mobile_no.Text = dt.Rows[0]["Guardian_mobile_no"].ToString();
                lbl_guardian_aadhar_no.Text = dt.Rows[0]["Guardian_aadhar_no"].ToString();
                lbl_guardian_annual_income.Text = dt.Rows[0]["Guardian_annual_income"].ToString();
                lbl_guardian_address.Text = dt.Rows[0]["Guardian_address"].ToString();
            }
        }


        //======================
        private void bind_academic(string regId, string ssion, string clss)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Academic_Education_Details_Of_Student where Class_Id='" + clss + "' and Session_Id='" + ssion + "' and Admission_No='" + regId + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                // Response.Redirect("Home.aspx");
            }
            else
            {

            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("student-list.aspx", false);
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_img_dv = ((Label)e.Item.FindControl("lbl_img_dv")) as Label;
                Label lbl_file_type = ((Label)e.Item.FindControl("lbl_file_type")) as Label;

                if (lbl_file_type.Text == "Parent_Sign" || lbl_file_type.Text == "Father_image" || lbl_file_type.Text == "Mother_Sign" || lbl_file_type.Text == "Mother_image" || lbl_file_type.Text == "Student_image")
                {
                    lbl_img_dv.Visible = false;
                }
            }
        }
    }
}