 
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
    public partial class student_full_details : System.Web.UI.Page
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
                        fetch_data(regId, ssion, clss); bind_academic(regId, ssion, clss); fetch_studnt_img(regId);
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

        private void fetch_studnt_img(string regId)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Student_Image_List where Admission_no='" + regId + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                if (dt.Rows[0]["Student_image"].ToString() == "")
                {
                    img_studentimages.ImageUrl = "../assets/images/no-image.png";
                }
                else
                {
                    img_studentimages.ImageUrl = dt.Rows[0]["Student_image"].ToString();
                }
              
                doc_studentimg.HRef = dt.Rows[0]["Student_image"].ToString();
                doc_signaturee.HRef = dt.Rows[0]["Parent_Sign"].ToString();
                doc_transfer.HRef = dt.Rows[0]["Tc_image"].ToString();
                doc_dobcert.HRef = dt.Rows[0]["DOB_image"].ToString();
                doc_admission.HRef = dt.Rows[0]["Adm_form"].ToString();
            }
        }

        private void fetch_data(string regId, string ssion, string clss)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_Name,(select top 1 Sub_CategoryName from Sub_Category_Details where Sub_CategoryId=admission_registor.SubCategory_id) as Sub_CategoryName from admission_registor  where Session_id='" + ssion + "' and Class_id='" + clss + "' and admissionserialnumber='" + regId + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lbl_formno.Text = dt.Rows[0]["formserialnumber"].ToString();
                lbl_admissiondate.Text = dt.Rows[0]["dateofadmission"].ToString();
                lbl_session.Text = dt.Rows[0]["father_mob"].ToString();
                lbl_session.Text = dt.Rows[0]["session"].ToString();

                lbl_cat.Text = dt.Rows[0]["Category_Name"].ToString();
                lbl_sub_cat.Text = dt.Rows[0]["Sub_CategoryName"].ToString();

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
               
                if (dt.Rows[0]["transportationtaken"].ToString() == "Yes")
                {
                    lbl_transportation.Text = "Yes";
                }
                else
                {
                    lbl_transportation.Text = "No";
                }

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
                lbl_certificateno.Text = dt.Rows[0]["cast_certificate_no"].ToString();
                lbl_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();
                lbl_anyillness.Text = dt.Rows[0]["is_illness"].ToString();
                lbl_Illness_Remark.Text = dt.Rows[0]["illness_remark"].ToString();
                lbl_prev_school.Text = dt.Rows[0]["currentschool"].ToString();
                lbl_cast.Text = dt.Rows[0]["jati"].ToString();
                lbl_rte_student.Text = dt.Rows[0]["RTE"].ToString();
                lbl_staff_ward.Text = dt.Rows[0]["staff_ward"].ToString();

                //===================
                lbl_fathername.Text = dt.Rows[0]["fathername"].ToString();
                lbl_occupation.Text = dt.Rows[0]["occuption"].ToString();
                lbl_qulification.Text = dt.Rows[0]["fatherqualification"].ToString();
                lbl_Nationality.Text = dt.Rows[0]["f_nationality"].ToString();
                lbl_martialstatus.Text = dt.Rows[0]["f_marrital_statue"].ToString();
                lbl_mobile_no.Text = dt.Rows[0]["father_mob"].ToString();
                lbl_emailid.Text = dt.Rows[0]["email_id"].ToString();
                lbl_guardianname.Text = dt.Rows[0]["guardianname"].ToString();
                lbl_parent_income.Text = dt.Rows[0]["parentincome"].ToString();

                //===================
                lbl_mother_name.Text = dt.Rows[0]["mothername"].ToString();
                lbl_occupation_mother.Text = dt.Rows[0]["m_occupation"].ToString();
                lbl_motherqulification.Text = dt.Rows[0]["motherqualifiaction"].ToString();
                lbl_mother_nationalty.Text = dt.Rows[0]["m_nationality"].ToString();
                lbl_marital_status.Text = dt.Rows[0]["m_marrital_statue"].ToString();
                lbl_mobile_mother.Text = dt.Rows[0]["mother_mob"].ToString();
                lbl_emailid_mother.Text = dt.Rows[0]["mother_email"].ToString();

                //=======================
                lbl_current.Text = dt.Rows[0]["careof"].ToString();
                lbl_mobile_no_current.Text = dt.Rows[0]["mobilenumber"].ToString();
                lbl_cityvillage_current.Text = dt.Rows[0]["city"].ToString();
                lbl_distict_current.Text = dt.Rows[0]["district"].ToString();
                lbl_po_current.Text = dt.Rows[0]["postoffice"].ToString();
                lbl_ps_current.Text = dt.Rows[0]["policestation"].ToString();

                lbl_state_current.Text = mycode.getstatename(dt.Rows[0]["state"].ToString());

                lbl_pincode.Text = dt.Rows[0]["pin"].ToString();

                //==========================
                lbl_permanent_address.Text = dt.Rows[0]["careof_permanent"].ToString();
                lbl_mobile_no_permanent.Text = dt.Rows[0]["mob2"].ToString();
                lbl_cityvillage_permanent.Text = dt.Rows[0]["city_permanent"].ToString();
                lbl_distict_permanent.Text = dt.Rows[0]["district_permanent"].ToString();
                lbl_po_permanent.Text = dt.Rows[0]["postoffice_permanent"].ToString();
                lbl_ps_permanent.Text = dt.Rows[0]["policestation_permanent"].ToString();
                lbl_state_permanent.Text = mycode.getstatename(dt.Rows[0]["state_permanent"].ToString());  
                lbl_pincode_permanent.Text = dt.Rows[0]["pincode"].ToString();

                //===========================
                lbl_account_holder_name.Text = dt.Rows[0]["Account_Holder_name"].ToString();
                lbl_bankname.Text = dt.Rows[0]["Bnk_Name"].ToString();
                lbl_IFSCCode.Text = dt.Rows[0]["IFSC_Code"].ToString();
                lbl_branch_name.Text = dt.Rows[0]["Branch_Name"].ToString();
                lbl_identy.Text = dt.Rows[0]["Personal_Identymarks"].ToString();


                lbl_cunterycode1.Text = dt.Rows[0]["Country_Code_Father"].ToString();
                lbl_cunterycode2.Text = dt.Rows[0]["Country_Code_Mother"].ToString();
                lbl_cunterycode3.Text = dt.Rows[0]["Country_Code_Current_add"].ToString();
                lbl_cunterycode4.Text = dt.Rows[0]["Country_Code_Current_Perm_add"].ToString();


                if (dt.Rows[0]["Country_Code_Father"].ToString() == "")
                {
                    lbl_cunterycode1.Text = "+91";
                    lbl_cunterycode2.Text = "+91";
                    lbl_cunterycode3.Text = "+91";
                    lbl_cunterycode4.Text = "+91";
                }
                else
                {
                    lbl_cunterycode1.Text = dt.Rows[0]["Country_Code_Father"].ToString();
                    lbl_cunterycode2.Text = dt.Rows[0]["Country_Code_Mother"].ToString();
                    lbl_cunterycode3.Text = dt.Rows[0]["Country_Code_Current_add"].ToString();
                    lbl_cunterycode4.Text = dt.Rows[0]["Country_Code_Current_Perm_add"].ToString();
                }
                doc_mother.HRef = dt.Rows[0]["mother_signature"].ToString();

                lbl_accountno.Text = dt.Rows[0]["Bank_acount_no"].ToString();
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
                 


                //=========================
            

            }
        }

    }
}