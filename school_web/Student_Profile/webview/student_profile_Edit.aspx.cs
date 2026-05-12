using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class student_profile_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["sesssionid"] = My.get_session_id();
            if (Request.QueryString["regid"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    try
                    {
                        mycode.bind_ddl(ddl_state_current, "Select distinct State from  state_and_district where Country='India' ");
                        mycode.bind_ddl(ddl_state_permanent, "Select distinct State from  state_and_district where Country='India' ");
                        BindDetails();
                    }
                    catch
                    {
                    }
                }
            }
        }
        UsesCode mycode = new UsesCode();
        private void BindDetails()
        {
            string house = "Select top 1 house_name from house_master where house_id=admission_registor.house";
            DataTable dt = mycode.FillTable("Select *,(" + house + ") as housename from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and Session_id=" + ViewState["sesssionid"].ToString() + " and Transfer_Status in ('New','NT') and  StudentStatus='AV' order by id desc ");
            if (dt.Rows.Count == 0)
            {
                //lnk_edit.Visible = false;
            }
            else
            {
                ViewState["id"] = dt.Rows[0]["id"].ToString();

                txt_account_no.Text = dt.Rows[0]["Bank_acount_no"].ToString();
                if (dt.Rows[0]["Edit_Istatus"].ToString() == "1")
                {
                    //  lnk_edit.Visible = true;
                }
                else
                {
                    // lnk_edit.Visible = false;
                }

                lbladmissiondate.Text = dt.Rows[0]["dateofadmission"].ToString();
                lbl_session.Text = dt.Rows[0]["session"].ToString();



                lbl_class.Text = dt.Rows[0]["class"].ToString();
                lbl_Section.Text = dt.Rows[0]["Section"].ToString();
                lbl_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_rollno.Text = dt.Rows[0]["rollnumber"].ToString();
                lbl_house.Text = dt.Rows[0]["housename"].ToString();
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
                    else if (dt.Rows[0]["transportationtaken"].ToString() == "&nbsp;")
                    {
                        lbl_transprtaion.Text = "No";
                    }

                    else
                    {
                        lbl_transprtaion.Text = dt.Rows[0]["transportationtaken"].ToString();
                    }
                }


                if (dt.Rows[0]["studentimagepath"].ToString() == "")
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
                    Image1.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
                }


                lbl_student_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_dateofbirth1.Text = dt.Rows[0]["dob"].ToString();

                lbl_palceof_birth.Text = dt.Rows[0]["place_of_birth"].ToString();
                lbl_CertificateNo.Text = dt.Rows[0]["birth_certificate_number"].ToString();
                lbl_bloodgroup.Text = dt.Rows[0]["blood_group"].ToString();
                lbl_aadharno.Text = dt.Rows[0]["aadharno"].ToString();
                try
                {
                    ddl_gender.Text = dt.Rows[0]["gender"].ToString();
                }
                catch
                {
                }
                lbl_religion.Text = dt.Rows[0]["religion"].ToString();
                try
                {
                    ddl_ration_type.Text = dt.Rows[0]["ration_type"].ToString();
                }
                catch
                {
                }
                try
                {
                    ddl_Category.Text = dt.Rows[0]["cast"].ToString();
                }
                catch
                {
                }
                lbl_certificate.Text = dt.Rows[0]["cast_certificate_no"].ToString();
                try
                {
                    ddl_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();
                }
                catch
                {
                }

                try
                {
                    if (dt.Rows[0]["is_illness"].ToString() == "")
                    {
                        ddl_anyillness.Text = "No";
                    }
                    else
                    {
                        ddl_anyillness.Text = dt.Rows[0]["is_illness"].ToString();
                    }
                }
                catch
                {
                }
                lbl_prevschool.Text = dt.Rows[0]["currentschool"].ToString();

                lbl_cast.Text = dt.Rows[0]["jati"].ToString();//
                lblfathername.Text = dt.Rows[0]["fathername"].ToString();
                try
                {
                    ddl_ocupation.Text = dt.Rows[0]["occuption"].ToString();
                }
                catch
                {
                }
                lbl_qulification.Text = dt.Rows[0]["fatherqualification"].ToString();
                lbl_Nationality.Text = dt.Rows[0]["f_nationality"].ToString();
                try
                {
                    ddl_martialstatus.Text = dt.Rows[0]["f_marrital_statue"].ToString();
                }
                catch
                {
                }
                lbl_mobile_no.Text = dt.Rows[0]["father_mob"].ToString();
                lbl_emailid.Text = dt.Rows[0]["email_id"].ToString();
                lbl_guardianname.Text = dt.Rows[0]["guardianname"].ToString();
                lbl_parent_income.Text = dt.Rows[0]["parentincome"].ToString();
                lbl_mother.Text = dt.Rows[0]["mothername"].ToString();
                try
                {
                    ddl_occupation_mother.Text = dt.Rows[0]["m_occupation"].ToString();
                }
                catch
                {
                }

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

                try
                {
                    ddl_state_current.Text = dt.Rows[0]["state"].ToString();

                }
                catch
                {
                }
                lbl_pincode.Text = dt.Rows[0]["pin"].ToString();

                // permanent address

                lbl_permanent_address.Text = dt.Rows[0]["careof_permanent"].ToString();
                lbl_mobile_no_permanent.Text = dt.Rows[0]["mob2"].ToString();//
                lbl_cityvillage_permanent.Text = dt.Rows[0]["city_permanent"].ToString();
                lbl_distict_permanent.Text = dt.Rows[0]["district_permanent"].ToString();
                lbl_po_permanent.Text = dt.Rows[0]["postoffice_permanent"].ToString();
                lbl_ps_permanent.Text = dt.Rows[0]["policestation_permanent"].ToString();

                try
                {
                    ddl_state_permanent.Text = dt.Rows[0]["state_permanent"].ToString();
                }
                catch
                {
                }

                lbl_pincode_permanent.Text = dt.Rows[0]["pincode"].ToString();

                lbl_account_holder_name.Text = dt.Rows[0]["Account_Holder_name"].ToString();
                // lbl_account_name.Text = "";//; 
                lbl_IFSCCode.Text = dt.Rows[0]["IFSC_Code"].ToString();
                lbl_bankname.Text = dt.Rows[0]["Bnk_Name"].ToString();
                lbl_branch_name.Text = dt.Rows[0]["Branch_Name"].ToString();


                txt_password.Text = dt.Rows[0]["Pwd"].ToString();

            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            string query = @"Update admission_registor set Edit_Istatus=@Edit_Istatus,Verification_Istatus=@Verification_Istatus,studentname=@studentname,dob=@dob,place_of_birth=@place_of_birth,birth_certificate_number=@birth_certificate_number,blood_group=@blood_group,aadharno=@aadharno,gender=@gender,religion=@religion,cast=@cast,ration_type=@ration_type,cast_certificate_no=@cast_certificate_no,student_mother_tounge=@student_mother_tounge,
is_illness=@is_illness,currentschool=@currentschool,jati=@jati,fathername=@fathername,occuption=@occuption,fatherqualification=@fatherqualification,f_nationality=@f_nationality,f_marrital_statue=@f_marrital_statue,father_mob=@father_mob,email_id=@email_id,guardianname=@guardianname,parentincome=@parentincome,mothername=@mothername,m_occupation=@m_occupation,motherqualifiaction=@motherqualifiaction,mother_mob=@mother_mob,mother_email=@mother_email,careof=@careof,mobilenumber=@mobilenumber,city=@city,district=@district,postoffice=@postoffice,policestation=@policestation,state=@state,pin=@pin,careof_permanent=@careof_permanent,mob2=@mob2,city_permanent=@city_permanent,district_permanent=@district_permanent,postoffice_permanent=@postoffice_permanent,policestation_permanent=@policestation_permanent,state_permanent=@state_permanent,pincode=@pincode,Account_Holder_name=@Account_Holder_name,IFSC_Code=@IFSC_Code,Bnk_Name=@Bnk_Name,Branch_Name=@Branch_Name,Pwd=@Pwd,Bank_acount_no=@Bank_acount_no where      id=" + ViewState["id"].ToString() + " ";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Edit_Istatus", "0");
            cmd.Parameters.AddWithValue("@Verification_Istatus", "1");
            cmd.Parameters.AddWithValue("@admissionserialnumber", ViewState["regid"].ToString());
            cmd.Parameters.AddWithValue("@studentname", lbl_student_name.Text);
            cmd.Parameters.AddWithValue("@dob", lbl_dateofbirth1.Text);
            cmd.Parameters.AddWithValue("@place_of_birth", lbl_palceof_birth.Text);
            cmd.Parameters.AddWithValue("@birth_certificate_number", lbl_CertificateNo.Text);
            cmd.Parameters.AddWithValue("@blood_group", lbl_bloodgroup.Text);
            cmd.Parameters.AddWithValue("@aadharno", lbl_aadharno.Text);
            cmd.Parameters.AddWithValue("@gender", ddl_gender.Text);
            cmd.Parameters.AddWithValue("@religion", lbl_religion.Text);
            cmd.Parameters.AddWithValue("@ration_type", ddl_ration_type.Text);
            cmd.Parameters.AddWithValue("@cast", ddl_Category.Text);
            cmd.Parameters.AddWithValue("@cast_certificate_no", lbl_certificate.Text);
            cmd.Parameters.AddWithValue("@student_mother_tounge", ddl_mother_tongue.Text);
            cmd.Parameters.AddWithValue("@is_illness", ddl_anyillness.Text);
            cmd.Parameters.AddWithValue("@currentschool", lbl_prevschool.Text);
            cmd.Parameters.AddWithValue("@jati", lbl_cast.Text);
            cmd.Parameters.AddWithValue("@fathername", lblfathername.Text);
            cmd.Parameters.AddWithValue("@occuption", ddl_ocupation.Text);
            cmd.Parameters.AddWithValue("@fatherqualification", lbl_qulification.Text);
            cmd.Parameters.AddWithValue("@f_nationality", lbl_Nationality.Text);
            cmd.Parameters.AddWithValue("@f_marrital_statue", ddl_martialstatus.Text);
            cmd.Parameters.AddWithValue("@father_mob", lbl_mobile_no.Text);
            cmd.Parameters.AddWithValue("@email_id", lbl_emailid.Text);
            cmd.Parameters.AddWithValue("@guardianname", lbl_guardianname.Text);
            cmd.Parameters.AddWithValue("@parentincome", lbl_parent_income.Text);
            cmd.Parameters.AddWithValue("@mothername", lbl_mother.Text);
            cmd.Parameters.AddWithValue("@m_occupation", ddl_occupation_mother.Text);
            cmd.Parameters.AddWithValue("@motherqualifiaction", lbl_motherqulification.Text);
            cmd.Parameters.AddWithValue("@mother_mob", lbl_mobileno_mother.Text);
            cmd.Parameters.AddWithValue("@mother_email", lbl_emailcode.Text);
            cmd.Parameters.AddWithValue("@careof", lbl_current.Text);
            cmd.Parameters.AddWithValue("@mobilenumber", lbl_mobile_no_current.Text);
            cmd.Parameters.AddWithValue("@city", lbl_cityvillage_current.Text);
            cmd.Parameters.AddWithValue("@district", lbl_distict_current.Text);
            cmd.Parameters.AddWithValue("@postoffice", lbl_po_current.Text);
            cmd.Parameters.AddWithValue("@policestation", lbl_ps_current.Text);
            cmd.Parameters.AddWithValue("@state", ddl_state_current.Text);
            cmd.Parameters.AddWithValue("@pin", lbl_pincode.Text);
            cmd.Parameters.AddWithValue("@careof_permanent", lbl_permanent_address.Text);
            cmd.Parameters.AddWithValue("@mob2", lbl_mobile_no_permanent.Text);
            cmd.Parameters.AddWithValue("@city_permanent", lbl_cityvillage_permanent.Text);
            cmd.Parameters.AddWithValue("@district_permanent", lbl_distict_permanent.Text);
            cmd.Parameters.AddWithValue("@postoffice_permanent", lbl_po_permanent.Text);
            cmd.Parameters.AddWithValue("@policestation_permanent", lbl_ps_permanent.Text);
            cmd.Parameters.AddWithValue("@state_permanent", ddl_state_permanent.Text);
            cmd.Parameters.AddWithValue("@pincode", lbl_pincode_permanent.Text);


            cmd.Parameters.AddWithValue("@Account_Holder_name", lbl_account_holder_name.Text);
            cmd.Parameters.AddWithValue("@IFSC_Code", lbl_IFSCCode.Text);
            cmd.Parameters.AddWithValue("@Bnk_Name", lbl_bankname.Text);
            cmd.Parameters.AddWithValue("@Branch_Name", lbl_branch_name.Text);
            cmd.Parameters.AddWithValue("@Bank_acount_no", txt_account_no.Text);
            if (txt_password.Text == "")
            {
                cmd.Parameters.AddWithValue("@Pwd", "12345");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Pwd", txt_password.Text);
            }

            if (InsertUpdate.InsertUpdateData(cmd))
            {
                Alert("Your Information has been submitted successfully and pending for verification by school Management.");
            }
        }

        private void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            lbl_student_name.Focus();
        }
    }
}