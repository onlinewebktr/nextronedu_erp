using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class student_profile_Edit : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        compLN comP = new compLN();
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["sesssionid"] = My.get_session_id();
            if (Request.QueryString["id"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    ViewState["id"] = Request.QueryString["id"].ToString();

                    try
                    {
                        mycode.bind_ddl(ddl_state_current, "Select distinct State from  state_and_district where Country='India' ");
                        mycode.bind_ddl(ddl_state_permanent, "Select distinct State from  state_and_district where Country='India' ");
                        comP.bind_ddl(ddl_cast_category, "select Category_name from Cast_category");
                        comP.bind_ddl(ddl_student_mother_tongue, "select Language from Language_Master order by Language asc");
                        comP.bind_ddl_NA(ddl_father_qualification, "select Name from Qualification_master");
                        My.bind_ddl_noselect(ddl_section, "select Section from section_master order by Section_order asc");
                        mycode.bind_all_ddl_with_id_cap_NA(ddl_house, "select house_name,house_id from dbo.[house_master]");


                        string stateName = My.get_state_name();
                        compLN.bind_ddl_select(ddl_state_current, "select UPPER(State) as State from dbo.[StateList] order by State asc");
                        compLN.bind_ddl_select(ddl_state_permanent, "select UPPER(State) as State from dbo.[StateList] order by State asc");
                        try
                        {
                            ddl_state_current.Text = stateName.ToUpper();
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_state_permanent.Text = stateName.ToUpper();
                        }
                        catch (Exception ex)
                        {
                        }

                        comP.bind_ddl_NA(ddl_mother_qualification, "select Name from Qualification_master");

                        BindDetails();
                    }
                    catch
                    {
                    }
                }
            }



        }

        private void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            lbl_student_name.Focus();
        }

        private void BindDetails()
        {
            string house = "Select top 1 house_name from house_master where house_id=admission_registor.house";
            DataTable dt = mycode.FillTable("Select *,(" + house + ") as housename from admission_registor where Id='" + ViewState["id"].ToString() + "' ");
            if (dt.Rows.Count == 0)
            {
                //lnk_edit.Visible = false;
            }
            else
            {




                ViewState["id"] = dt.Rows[0]["id"].ToString();
                ddl_section.Text = dt.Rows[0]["Section"].ToString();
                txt_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
                ddl_house.SelectedValue = dt.Rows[0]["house"].ToString();
                lbl_student_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_dateofbirth1.Text = dt.Rows[0]["dob"].ToString();
                lbl_palceof_birth.Text = dt.Rows[0]["place_of_birth"].ToString();

                ddl_blood_group.Text = dt.Rows[0]["blood_group"].ToString();
                lbl_aadharno.Text = dt.Rows[0]["aadharno"].ToString();
                try
                {
                    ddl_gender.Text = dt.Rows[0]["gender"].ToString();
                }
                catch
                {
                }





                ddl_religion.Text = dt.Rows[0]["religion"].ToString();
                try
                {
                    ddl_ration_type.Text = dt.Rows[0]["ration_type"].ToString();
                }
                catch
                {
                }
                try
                {
                    ddl_cast_category.Text = dt.Rows[0]["cast"].ToString();
                }
                catch
                {
                }
                lbl_castjati.Text = dt.Rows[0]["jati"].ToString();


                try
                {
                    ddl_student_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();
                }
                catch
                {
                }
                lblfathername.Text = dt.Rows[0]["fathername"].ToString();
                try
                {
                    ddl_ocupation.Text = dt.Rows[0]["occuption"].ToString();
                }
                catch
                {
                }



                try
                {
                    ddl_martialstatus.Text = dt.Rows[0]["f_marrital_statue"].ToString();
                }
                catch
                {
                }
                lbl_mobile_no.Text = dt.Rows[0]["father_mob"].ToString();
                txt_father_whatsapp_no.Text = dt.Rows[0]["Father_whatsApp_no"].ToString();
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

                ddl_mother_qualification.Text = dt.Rows[0]["motherqualifiaction"].ToString();
                lbl_mobileno_mother.Text = dt.Rows[0]["mother_mob"].ToString();
                lbl_emailcode.Text = dt.Rows[0]["mother_email"].ToString();
                // current address
                lbl_current.Text = dt.Rows[0]["careof"].ToString();
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

                try
                {
                    ddl_father_qualification.Text = dt.Rows[0]["fatherqualification"].ToString();
                }
                catch
                {

                }
            }
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (ddl_house.Text == "Select")
            {
                Alert("Please select house");
            }
            else if (lbl_dateofbirth1.Text == "")
            {
                Alert("Please enter date of birth");
            }
            else if (ddl_cast_category.Text == "Select")
            {
                Alert("Please select cast catogery");
            }
            else if (ddl_student_mother_tongue.Text == "Select")
            {
                Alert("Please select mother tongue");
            }
            else if (ddl_father_qualification.Text == "Select")
            {
                Alert("Please select father qualification ");
            }
            else if (ddl_ocupation.Text == "Select")
            {
                Alert("Please select father Occupation ");
            }

            else if (ddl_father_qualification.Text == "Select")
            {
                Alert("Please select father qualification ");
            }

            else if (ddl_occupation_mother.Text == "Select")
            {
                Alert("Please select mother Occupation ");
            }
            else if (ddl_mother_qualification.Text == "Select")
            {
                Alert("Please select mother qualification ");
            }
            
            else if (ddl_state_current.Text == "Select")
            {
                Alert("Please select current state ");
            }
            else if (ddl_state_permanent.Text == "Select")
            {
                Alert("Please select Permanent state ");
            }
            else
            {
                SqlCommand cmd;
                string query = "update  admission_registor set Section=@Section,rollnumber=@rollnumber,house = @house,studentname = @studentname,Student_Name_First = @Student_Name_First,Student_Middle_Name = @Student_Middle_Name,Student_Name_Last = @Student_Name_Last,fathername = @fathername,Father_Name_First = @Father_Name_First,Father_Name_Middle = @Father_Name_Middle,Father_Name_Last=@Father_Name_Last,mothername=@mothername,Mother_Name_First = @Mother_Name_First,Mother_Name_Middle = @Mother_Name_Middle,Mother_Name_Last = @Mother_Name_Last,dob = @dob,place_of_birth = @place_of_birth,blood_group = @blood_group,aadharno = @aadharno,gender = @gender,religion = @religion,ration_type = @ration_type,cast = @cast,jati = @jati,student_mother_tounge = @student_mother_tounge,occuption = @occuption,fatherqualification=@fatherqualification,f_nationality=@f_nationality,f_marrital_statue = @f_marrital_statue,father_mob = @father_mob,Father_whatsApp_no = @Father_whatsApp_no,email_id = @email_id,guardianname = @guardianname,parentincome = @parentincome,m_occupation = @m_occupation,motherqualifiaction = @motherqualifiaction,mother_mob = @mother_mob,mother_email = @mother_email,careof = @careof,city = @city,district = @district,postoffice = @postoffice,policestation = @policestation,state = @state,pin = @pin,careof_permanent = @careof_permanent,city_permanent = @city_permanent,district_permanent = @district_permanent,postoffice_permanent = @postoffice_permanent,policestation_permanent = @policestation_permanent,state_permanent = @state_permanent,pincode = @pincode,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate where Id='" + ViewState["id"] + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                cmd.Parameters.AddWithValue("@rollnumber", txt_roll_no.Text.Trim());
                cmd.Parameters.AddWithValue("@house", ddl_house.SelectedValue);
                cmd.Parameters.AddWithValue("@studentname", lbl_student_name.Text);
                cmd.Parameters.AddWithValue("@Student_Name_First", lbl_student_name.Text);
                cmd.Parameters.AddWithValue("@Student_Middle_Name", "");
                cmd.Parameters.AddWithValue("@Student_Name_Last", "");
                cmd.Parameters.AddWithValue("@fathername", lblfathername.Text);
                cmd.Parameters.AddWithValue("@Father_Name_First", lblfathername.Text);
                cmd.Parameters.AddWithValue("@Father_Name_Middle", "");
                cmd.Parameters.AddWithValue("@Father_Name_Last", "");
                cmd.Parameters.AddWithValue("@mothername", lbl_mother.Text);
                cmd.Parameters.AddWithValue("@Mother_Name_First", lbl_mother.Text);
                cmd.Parameters.AddWithValue("@Mother_Name_Middle", "");
                cmd.Parameters.AddWithValue("@Mother_Name_Last", "");
                cmd.Parameters.AddWithValue("@dob", lbl_dateofbirth1.Text);
                cmd.Parameters.AddWithValue("@place_of_birth", lbl_palceof_birth.Text);
                cmd.Parameters.AddWithValue("@blood_group", ddl_blood_group.Text);
                cmd.Parameters.AddWithValue("@aadharno", lbl_aadharno.Text);
                cmd.Parameters.AddWithValue("@gender", ddl_gender.Text);
                cmd.Parameters.AddWithValue("@religion", ddl_religion.Text);
                cmd.Parameters.AddWithValue("@ration_type", ddl_ration_type.Text);
                cmd.Parameters.AddWithValue("@cast", ddl_cast_category.Text);
                cmd.Parameters.AddWithValue("@jati", lbl_castjati.Text);
                cmd.Parameters.AddWithValue("@student_mother_tounge", ddl_student_mother_tongue.Text);
                cmd.Parameters.AddWithValue("@occuption", ddl_ocupation.Text);
                cmd.Parameters.AddWithValue("@f_marrital_statue", ddl_martialstatus.Text);
                cmd.Parameters.AddWithValue("@father_mob", lbl_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Father_whatsApp_no", txt_father_whatsapp_no.Text);
                cmd.Parameters.AddWithValue("@email_id", lbl_emailid.Text);
                cmd.Parameters.AddWithValue("@guardianname", lbl_guardianname.Text);
                cmd.Parameters.AddWithValue("@parentincome", lbl_parent_income.Text);
                cmd.Parameters.AddWithValue("@fatherqualification", ddl_father_qualification.Text);
                cmd.Parameters.AddWithValue("@f_nationality", "INDIAN");
                cmd.Parameters.AddWithValue("@m_occupation", ddl_occupation_mother.Text);
                cmd.Parameters.AddWithValue("@motherqualifiaction", ddl_mother_qualification.Text);
                cmd.Parameters.AddWithValue("@mother_mob", lbl_mobileno_mother.Text);
                cmd.Parameters.AddWithValue("@mother_email", lbl_emailcode.Text);
                cmd.Parameters.AddWithValue("@careof", lbl_current.Text);
                cmd.Parameters.AddWithValue("@city", lbl_cityvillage_current.Text);
                cmd.Parameters.AddWithValue("@district", lbl_distict_current.Text);
                cmd.Parameters.AddWithValue("@postoffice", lbl_po_current.Text);
                cmd.Parameters.AddWithValue("@policestation", lbl_ps_current.Text);
                cmd.Parameters.AddWithValue("@state", ddl_state_current.Text);
                cmd.Parameters.AddWithValue("@pin", lbl_pincode.Text);
                cmd.Parameters.AddWithValue("@careof_permanent", lbl_permanent_address.Text);
                cmd.Parameters.AddWithValue("@city_permanent", lbl_cityvillage_permanent.Text);
                cmd.Parameters.AddWithValue("@district_permanent", lbl_distict_permanent.Text);
                cmd.Parameters.AddWithValue("@postoffice_permanent", lbl_po_permanent.Text);
                cmd.Parameters.AddWithValue("@policestation_permanent", lbl_ps_permanent.Text);
                cmd.Parameters.AddWithValue("@state_permanent", ddl_state_permanent.Text);
                cmd.Parameters.AddWithValue("@pincode", lbl_pincode_permanent.Text);

                cmd.Parameters.AddWithValue("@Updated_by", ViewState["regid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
               
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    Session["msg"] = "Student Name : " + lbl_student_name.Text + " information has been successfully updated.";
                    Response.Redirect(My.URL() + "InstructorProfile/webview/Student_List_For_Edit.aspx?regid=" + ViewState["regid"].ToString(), false);
                }

            }
        }
    }
}