using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Student_Full_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["regid"] != null)
            {
                ViewState["regid"] = Request.QueryString["regid"].ToString();
                try
                {
                    BindDetails();
                }
                catch
                {
                }
            }
        }

        UsesCode mycode = new UsesCode();
        private void BindDetails()
        {
            DataTable dt = mycode.FillTable("Select * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                
            }
            else
            {


                
                //lbl_formslno.Text = dt.Rows[0]["formserialnumber"].ToString();
                lbladmissiondate.Text = dt.Rows[0]["dateofadmission"].ToString();
                lbl_session.Text = dt.Rows[0]["session"].ToString();



                lbl_class.Text = dt.Rows[0]["class"].ToString();
                lbl_Section.Text = dt.Rows[0]["Section"].ToString();
                lbl_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_rollno.Text = dt.Rows[0]["rollnumber"].ToString();
                lbl_house.Text = dt.Rows[0]["house"].ToString();
                if (dt.Rows[0]["hosteltaken"].ToString() == "Yes")
                {
                    lbl_admissionin.Text = "Hostel";
                    lbl_transprtaion.Text = "No";
                }
                else
                {
                    lbl_admissionin.Text = "Day Scholar";
                    if (dt.Rows[0]["transportationtaken"].ToString() == "")
                    {
                        lbl_transprtaion.Text = "No";
                    }
                    else
                    {
                        lbl_transprtaion.Text = dt.Rows[0]["transportationtaken"].ToString();
                    }
                }


                if (dt.Rows[0]["profile_img"].ToString() == "")
                {
                    if (dt.Rows[0]["gender"].ToString() == "Male")
                    {
                        Image1.ImageUrl = "~/images/male.png";
                    }
                    else if (dt.Rows[0]["gender"].ToString() == "Female")
                    {
                        Image1.ImageUrl = "~/images/female.png";
                    }
                    else
                    {
                        Image1.ImageUrl = "~/images/blank.png";
                    }
                }
                else
                {
                    Image1.ImageUrl = dt.Rows[0]["profile_img"].ToString();
                }


                lbl_student_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_dateofbirth.Text = dt.Rows[0]["dob"].ToString();
                lbl_palceof_birth.Text = dt.Rows[0]["place_of_birth"].ToString();
                lbl_CertificateNo.Text = dt.Rows[0]["birth_certificate_number"].ToString();
                lbl_bloodgroup.Text = dt.Rows[0]["blood_group"].ToString();
                lbl_aadharno.Text = dt.Rows[0]["aadharno"].ToString();
                lbl_aadharno.Text = dt.Rows[0]["aadharno"].ToString();
                lblgender.Text = dt.Rows[0]["gender"].ToString();
                lbl_religion.Text = dt.Rows[0]["religion"].ToString();
                lbl_ration_type.Text = dt.Rows[0]["ration_type"].ToString();
                lbl_catogery.Text = dt.Rows[0]["cast"].ToString();
                lbl_certificate.Text = dt.Rows[0]["cast_certificate_no"].ToString();
                lbl_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();

                if (dt.Rows[0]["is_illness"].ToString() == "")
                {
                    lbl_anyillness.Text = "No";
                }
                else
                {
                    lbl_anyillness.Text = dt.Rows[0]["is_illness"].ToString();
                }
                lbl_prevschool.Text = dt.Rows[0]["currentschool"].ToString();

                lbl_cast.Text = dt.Rows[0]["jati"].ToString();//
                lblfathername.Text = dt.Rows[0]["fathername"].ToString();
                lbl_occupation.Text = dt.Rows[0]["occuption"].ToString();
                lbl_qulification.Text = dt.Rows[0]["fatherqualification"].ToString();
                lbl_Nationality.Text = dt.Rows[0]["f_nationality"].ToString();

                lbl_martialstatus.Text = dt.Rows[0]["f_marrital_statue"].ToString();
                lbl_mobile_no.Text = dt.Rows[0]["father_mob"].ToString();
                lbl_emailid.Text = dt.Rows[0]["Email"].ToString();
                lbl_guardianname.Text = dt.Rows[0]["guardianname"].ToString();
                lbl_parent_income.Text = dt.Rows[0]["parentincome"].ToString();
                lbl_mother.Text = dt.Rows[0]["mothername"].ToString();
                lbl_occupation_mother.Text = dt.Rows[0]["m_occupation"].ToString();

                lbl_motherqulification.Text = dt.Rows[0]["motherqualifiaction"].ToString();
                lbl_mobileno_mother.Text = dt.Rows[0]["mother_mob"].ToString();
                lbl_emailcode.Text = dt.Rows[0]["mother_email"].ToString();

                // current address
                lbl_current.Text = dt.Rows[0]["careof"].ToString();
                lbl_mobile_no_current.Text = dt.Rows[0]["mobilenumber"].ToString();
                lbl_cityvillage_current.Text = dt.Rows[0]["city"].ToString();
                lbl_distict_current.Text = dt.Rows[0]["district"].ToString();
                lbl_po_current.Text = dt.Rows[0]["postoffice"].ToString();
                lbl_ps_current.Text = dt.Rows[0]["policestation"].ToString();
                lbl_state_current.Text = dt.Rows[0]["state"].ToString();
                lbl_pincode.Text = dt.Rows[0]["pin"].ToString();

                // permanent address

                lbl_permanent_address.Text = dt.Rows[0]["careof_permanent"].ToString();
                lbl_mobile_no_permanent.Text = dt.Rows[0]["mob2"].ToString();//
                lbl_cityvillage_permanent.Text = dt.Rows[0]["city_permanent"].ToString();
                lbl_distict_permanent.Text = dt.Rows[0]["district_permanent"].ToString();
                lbl_po_permanent.Text = dt.Rows[0]["postoffice_permanent"].ToString();
                lbl_ps_permanent.Text = dt.Rows[0]["policestation_permanent"].ToString();
                lbl_state_permanent.Text = dt.Rows[0]["state_permanent"].ToString();
                lbl_pincode_permanent.Text = dt.Rows[0]["pincode"].ToString();

                lbl_account_holder_name.Text = dt.Rows[0]["Account_Holder_name"].ToString();
                // lbl_account_name.Text = "";//; 
                lbl_IFSCCode.Text = dt.Rows[0]["IFSC_Code"].ToString();
                lbl_bankname.Text = dt.Rows[0]["Bnk_Name"].ToString();
                lbl_branch_name.Text = dt.Rows[0]["Branch_Name"].ToString();
            }
        }
    }
}