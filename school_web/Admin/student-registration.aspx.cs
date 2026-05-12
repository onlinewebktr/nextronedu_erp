using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class student_registration : System.Web.UI.Page
    {
        ArrayList dist = new ArrayList();
        ArrayList po = new ArrayList();
        ArrayList ps = new ArrayList();
        string transportation = "";
        string typeofstudent = "";
        string hosteltaken = "";
        string transportationtaken = "";
        string transportationpath = "";
        string transferstatus = "";
        int i;
        string admissiondate = "";
        My mycode = new My();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["Admin"] == null)
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                    else
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        txt_admission_date_old.Text = mycode.date();
                        txt_admission_date.Text = mycode.date();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        page_load();
                        // initialize_setting();
                        if (Request.QueryString["stdid"] != null)
                        {

                            btn_Submit.Text = "Update";
                            btn_cancel.Visible = true;
                            ltUsertop.Text = "Edit Registration";
                            HdID.Value = Request.QueryString["stdid"].ToString();
                            ViewState["admno"] = Request.QueryString["admno"].ToString();
                            BindDetails();
                        }
                        else
                        {
                            btn_Submit.Text = "Save"; btn_cancel.Visible = false;
                            ltUsertop.Text = "Student Registration";

                        }

                        ViewState["firm_id"] = Session["firm"].ToString();


                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                       
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StdentRegistration");
            }
        }

        private void BindDetails()
        {
            DataTable dt = mycode.FillData("select * from admission_registor where Id=" + HdID.Value + "");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {

                try
                {
                    ddl_cunterycode1.Text = dt.Rows[0]["Country_Code_Father"].ToString();
                }
                catch
                {
                }
                try
                {
                    ddl_cunterycode2.Text = dt.Rows[0]["Country_Code_Mother"].ToString();
                }
                catch
                {

                }
                try
                {
                    ddl_cunterycode3.Text = dt.Rows[0]["Country_Code_Current_add"].ToString();
                }
                catch
                {
                }
                try
                {
                    ddl_cunterycode4.Text = dt.Rows[0]["Country_Code_Current_Perm_add"].ToString();
                }
                catch
                {
                }
                try
                {
                    bind_c_country();
                    bind_p_country();
                }
                catch
                {

                }

                try
                {
                    txt_form_slno.Text = dt.Rows[0]["formserialnumber"].ToString();
                }
                catch

                {

                }







                ddlsession.SelectedValue = dt.Rows[0]["Session_id"].ToString();

                //rdb_hostel.Checked = true;
                //rdb_dayscholar.Checked = true; 
                if (dt.Rows[0]["hosteltaken"].ToString() == "Yes")
                {
                    rdb_hostel.Checked = true;
                    rdb_dayscholar.Checked = false;
                }
                else
                {
                    rdb_hostel.Checked = false;
                    rdb_dayscholar.Checked = true;
                }

                ddl_category.SelectedValue = dt.Rows[0]["Category_id"].ToString();
                ddl_subcategory.SelectedValue = dt.Rows[0]["SubCategory_id"].ToString();

                if (dt.Rows[0]["is_applied_dayboarding"].ToString() == "True")
                {
                    ddl_day_boarding.Text = "1";
                }
                else if (dt.Rows[0]["day_boarding_with_lunch"].ToString() == "True")
                {
                    ddl_day_boarding.Text = "2";
                }
                else
                {
                    ddl_day_boarding.Text = "0";
                }
                ddl_gender.Text = dt.Rows[0]["gender"].ToString();
                //bind_class();

                mycode.bind_all_ddl_with_id(ddlclass, "Select   cd.Course_Name,cd.course_id from Add_course_table cd  order by cd.Position asc");
                mycode.bind_all_ddl_with_id(ddl_old_class, "Select   cd.Course_Name,cd.course_id from Add_course_table cd  order by cd.Position asc");



                try
                {
                    ddl_old_class.SelectedValue = dt.Rows[0]["Old_class_id"].ToString();
                }
                catch
                {
                }

                ddlclass.SelectedValue = dt.Rows[0]["Class_Id"].ToString();

                txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                txt_rollnumber.Text = dt.Rows[0]["rollnumber"].ToString();

                //ddl_academic_ses_type.SelectedValue = dt.Rows[0]["Academic_Sem_or_Year_id"].ToString();
                if (dt.Rows[0]["transportationtaken"].ToString() == "Yes")
                {
                    transportpath.Visible = true;
                    rdotransyes.Checked = true;
                    rdotransno.Checked = false;
                    ddl_path_root.SelectedValue = dt.Rows[0]["transportation_id"].ToString();
                }
                else
                {
                    transportpath.Visible = false;
                    rdotransno.Checked = true;
                    rdotransyes.Checked = false;
                }
                fetch_academic();
                //======
                txt_student_name.Text = dt.Rows[0]["studentname"].ToString();



                if (dt.Rows[0]["Student_Name_First"].ToString() == "")
                {
                    txt_firstname.Text = txt_student_name.Text;
                }
                else
                {
                    txt_firstname.Text = dt.Rows[0]["Student_Name_First"].ToString();
                    txt_middlename.Text = dt.Rows[0]["Student_Middle_Name"].ToString();
                    txt_lastname.Text = dt.Rows[0]["Student_Name_Last"].ToString();
                }



                txt_dob.Text = dt.Rows[0]["dob"].ToString();
                txt_birth_certificate_no.Text = dt.Rows[0]["birth_certificate_number"].ToString();
                txt_place_of_birth.Text = dt.Rows[0]["place_of_birth"].ToString();
                ddl_blood_group.Text = dt.Rows[0]["blood_group"].ToString();
                txt_aadharno_mark.Text = dt.Rows[0]["aadharno"].ToString();
                ddl_religion.Text = dt.Rows[0]["religion"].ToString();
                ddl_ration_cards_types.Text = dt.Rows[0]["ration_type"].ToString();
                ddl_cast.Text = dt.Rows[0]["cast"].ToString();
                txt_cast_certificate_no.Text = dt.Rows[0]["cast_certificate_no"].ToString();
                ddl_student_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();

                ddl_illness.Text = dt.Rows[0]["is_illness"].ToString();
                txt_illness_remark.Text = dt.Rows[0]["illness_remark"].ToString();

                if (dt.Rows[0]["is_illness"].ToString() == "NO")
                {
                    txt_illness_remark.ReadOnly = true;
                    txt_illness_remark.Text = "";
                }
                else
                {
                    txt_illness_remark.ReadOnly = false;

                }


                try
                {
                    txt_school_current.Text = dt.Rows[0]["currentschool"].ToString();
                }
                catch
                {
                }



                txt_jati.Text = dt.Rows[0]["jati"].ToString();


                ddl_rte.Text = dt.Rows[0]["RTE"].ToString();
                ddl_staff_ward.Text = dt.Rows[0]["staff_ward"].ToString();

                //======= ///Father Info
                txt_father_name.Text = dt.Rows[0]["fathername"].ToString();



                if (dt.Rows[0]["Father_Name_First"].ToString() == "")
                {
                    txt_father_firstname.Text = txt_father_name.Text;
                }
                else
                {
                    txt_father_firstname.Text = dt.Rows[0]["Father_Name_First"].ToString();
                    txt_father_middle_name.Text = dt.Rows[0]["Father_Name_Middle"].ToString();
                    txt_father_lastname.Text = dt.Rows[0]["Father_Name_Last"].ToString();
                }

                ddl_occupation.Text = dt.Rows[0]["occuption"].ToString();
                txt_father_qualification.Text = dt.Rows[0]["fatherqualification"].ToString();
                txt_f_nationality.Text = dt.Rows[0]["f_nationality"].ToString();
                ddl_maritial_status.Text = dt.Rows[0]["f_marrital_statue"].ToString();
                txt_guardian_mob.Text = dt.Rows[0]["father_mob"].ToString();
                txt_guardian_email.Text = dt.Rows[0]["email_id"].ToString();
                txt_guardian_name.Text = dt.Rows[0]["guardianname"].ToString();
                txt_parent_income.Text = dt.Rows[0]["parentincome"].ToString();

                //======= Mother Info
                txt_mother_name.Text = dt.Rows[0]["mothername"].ToString();


                if (dt.Rows[0]["mothername"].ToString() == "")
                {
                    txt_first_name_mother.Text = dt.Rows[0]["Father_Name_First"].ToString();
                }
                else
                {
                    txt_first_name_mother.Text = dt.Rows[0]["Mother_Name_First"].ToString();
                    txt_middle_name_mother.Text = dt.Rows[0]["Mother_Name_Middle"].ToString();
                    txt_last_name_mother.Text = dt.Rows[0]["Mother_Name_Last"].ToString();
                }

                ddl_m_occupation.Text = dt.Rows[0]["m_occupation"].ToString();
                txt_mother_qulalifiaction.Text = dt.Rows[0]["motherqualifiaction"].ToString();
                txt_m_nationality.Text = dt.Rows[0]["m_nationality"].ToString();
                ddl_m_maritial_status.Text = dt.Rows[0]["m_marrital_statue"].ToString();
                txt_mother_mob.Text = dt.Rows[0]["mother_mob"].ToString();
                txt_mother_email.Text = dt.Rows[0]["mother_email"].ToString();

                //======= 
                txt_temp_address.Text = dt.Rows[0]["careof"].ToString();
                txt_temp_mobileno.Text = dt.Rows[0]["mobilenumber"].ToString();
                try
                {
                    txt_temp_city.Text = dt.Rows[0]["city"].ToString();
                }
                catch
                {
                }
                txt_temp_district.Text = dt.Rows[0]["district"].ToString();
                txt_temp_po.Text = dt.Rows[0]["postoffice"].ToString();
                txt_temp_ps.Text = dt.Rows[0]["policestation"].ToString();

                ddl_temp_state.Text = dt.Rows[0]["state"].ToString();
                txt_c_state.Text = dt.Rows[0]["state"].ToString();
                txt_temp_pin.Text = dt.Rows[0]["pin"].ToString();

                //======= 
                txt_par_address.Text = dt.Rows[0]["careof_permanent"].ToString();
                txt_per_mobileno.Text = dt.Rows[0]["mob2"].ToString();
                txt_par_city.Text = dt.Rows[0]["city_permanent"].ToString();
                txt_par_district.Text = dt.Rows[0]["district_permanent"].ToString();
                txt_par_po.Text = dt.Rows[0]["postoffice_permanent"].ToString();
                txt_par_ps.Text = dt.Rows[0]["policestation_permanent"].ToString();

                ddl_par_state.Text = dt.Rows[0]["state_permanent"].ToString();
                txt_p_state.Text = dt.Rows[0]["state_permanent"].ToString();
                txt_par_pin.Text = dt.Rows[0]["pincode"].ToString();

                //======= 
                txt_account_holder_name.Text = dt.Rows[0]["Account_Holder_name"].ToString();
                txt_bank_name.Text = dt.Rows[0]["Bnk_Name"].ToString();
                txt_ifsc_code.Text = dt.Rows[0]["IFSC_Code"].ToString();
                txt_branch_name.Text = dt.Rows[0]["Branch_Name"].ToString();

                ViewState["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                txt_persnal_identfication_marks.Text = dt.Rows[0]["Personal_Identymarks"].ToString();
                txt_account_no.Text = dt.Rows[0]["Bank_acount_no"].ToString();

                try
                {
                    ddl_section.Text = dt.Rows[0]["Section"].ToString();
                }
                catch
                {

                }

                txt_admission_no.ReadOnly = true;
                ddlsession.Enabled = false;
                ddlclass.Enabled = false;
                ddlsession.CssClass = "form-select find-dv-txtbx";
                ddlclass.CssClass = "form-select find-dv-txtbx";
                if (dt.Rows[0]["Transfer_Status"].ToString() == "New")
                {
                    ddl_student_type.Text = "New";
                }
                else
                {
                    ddl_student_type.Text = "Old";
                }

                try
                {
                    mycode.bind_all_ddl_with_id(ddl_house, "select house_name,house_id from dbo.[house_master]");
                    ddl_house.SelectedValue = dt.Rows[0]["house"].ToString();
                }
                catch
                {

                }
                try
                {


                    string date = mycode.convertdattodate(dt.Rows[0]["Old_Admission_Date"].ToString());
                    txt_admission_date_old.Text = date;

                }
                catch
                {
                }

                try
                {
                    string date = mycode.convertdattodate(dt.Rows[0]["dateofadmission"].ToString());
                    txt_admission_date.Text = date;
                }
                catch
                {
                }
                try
                {
                    txt_uid.Text = dt.Rows[0]["UID_No"].ToString();
                }
                catch
                {
                }

            }
        }

        private void fetch_academic()
        {
            DataTable dt = mycode.FillData("select * from Academic_Education_Details_Of_Student where Admission_No='" + ViewState["admno"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                //====TEN
                txt_tenth_board.Text = dt.Rows[0]["Tenth_Board"].ToString();
                txt_tenth_passing_year.Text = dt.Rows[0]["Tenth_year_of_passing"].ToString();
                txt_tenth_total_obtai_mark.Text = dt.Rows[0]["Tenth_total_mark_obtain"].ToString();
                txt_tenth_percentage_mark.Text = dt.Rows[0]["Tenth_percentage_mark"].ToString();
                txt_tenth_devision.Text = dt.Rows[0]["Tenth_division"].ToString();
                txt_tenth_subject.Text = dt.Rows[0]["Tenth_subjects"].ToString();
                ViewState["tenthDoc"] = dt.Rows[0]["tenth_doc"].ToString();


                //=====
                txt_twelve_board.Text = dt.Rows[0]["Twelve_Boards"].ToString();
                txt_twelve_passing_year.Text = dt.Rows[0]["Twelve_year_of_passing"].ToString();
                txt_twelve_total_mark.Text = dt.Rows[0]["Twelve_total_mark_obtain"].ToString();
                txt_twelve_percentage_of_mark.Text = dt.Rows[0]["Twelve_percentage_mark"].ToString();
                txt_twelve_division.Text = dt.Rows[0]["Twelve_division"].ToString();
                txt_twelve_subjects.Text = dt.Rows[0]["Twelve_subjects"].ToString();
                ViewState["ten_plus_doc"] = dt.Rows[0]["twelve_doc"].ToString();


                //====
                txt_graduation_university.Text = dt.Rows[0]["Graduation_Board"].ToString();
                txt_graduation_year_of_passing.Text = dt.Rows[0]["Graduation_year_of_passing"].ToString();
                txt_graduation_total_mark_obtains.Text = dt.Rows[0]["Graduation_total_mark_obtain"].ToString();
                txt_graduation_percentage_of_mark.Text = dt.Rows[0]["Graduation_percentage_mark"].ToString();
                txt_graduation_division.Text = dt.Rows[0]["Graduation_division"].ToString();
                txt_graduation_subject.Text = dt.Rows[0]["Graduation_subjects"].ToString();
                ViewState["graduation_doc"] = dt.Rows[0]["graduation_doc"].ToString();


                //====
                txt_post_graduation_university.Text = dt.Rows[0]["Post_Graduation_Board"].ToString();
                txt_post_graduation_year_of_passing.Text = dt.Rows[0]["Post_Graduation_year_of_passing"].ToString();
                txt_post_graduation_total_mark_obtain.Text = dt.Rows[0]["Post_Graduation_total_mark_obtain"].ToString();
                txt_post_graduation_percentage_of_mark.Text = dt.Rows[0]["Post_Graduation_percentage_mark"].ToString();
                txt_post_graduation_division.Text = dt.Rows[0]["Post_Graduation_division"].ToString();
                txt_post_graduation_subject.Text = dt.Rows[0]["Post_Graduation_subjects"].ToString();
                ViewState["post_graduation_doc"] = dt.Rows[0]["post_graduation_doc"].ToString();

                //====
                txt_other_board.Text = dt.Rows[0]["Other_Board"].ToString();
                txt_other_passing_year.Text = dt.Rows[0]["Other_year_of_passing"].ToString();
                txt_other_mark_obtain.Text = dt.Rows[0]["Other_total_mark_obtain"].ToString();
                txt_other_percentage_of_mark.Text = dt.Rows[0]["Other_percentage_mark"].ToString();
                txt_other_division.Text = dt.Rows[0]["Other_division"].ToString();
                txt_other_subjects.Text = dt.Rows[0]["Other_subjects"].ToString();
                ViewState["other_doc"] = dt.Rows[0]["other_doc"].ToString();


            }
        }

        DataTable adm_dt = new DataTable();
        private void page_load()
        {
            ViewState["college_name"] = My.get_college_name();
            mycode.bind_all_ddl_with_id(ddl_path_root, " select  Pathname,TransportationPath_id from  TransportationPath");
            adm_dt = My.dataTable("select district,postoffice,policestation from dbo.[admission_registor] where session='" + My.get_session() + "'");

            try
            {
                My.bind_ddl_noselect(ddl_section, "select Section from section_master order by Section_order asc");
                My.bind_ddl_noselect(ddl_cunterycode1, "select Country_code from Country_list order by Country_name asc");
                My.bind_ddl_noselect(ddl_cunterycode2, "select Country_code from Country_list order by Country_name asc");
                My.bind_ddl_noselect(ddl_cunterycode3, "select Country_code from Country_list order by Country_name asc");
                My.bind_ddl_noselect(ddl_cunterycode4, "select Country_code from Country_list order by Country_name asc");
                ddl_cunterycode1.SelectedValue = "+91";
                ddl_cunterycode2.SelectedValue = "+91";
                ddl_cunterycode3.SelectedValue = "+91";
                ddl_cunterycode4.SelectedValue = "+91";
            }
            catch
            {

            }

            bind_c_country();
            bind_p_country();

            mycode.bind_all_ddl_with_id(ddl_temp_state, "select State,Code from dbo.[StateList]");
            mycode.bind_all_ddl_with_id(ddl_par_state, "select State,Code from dbo.[StateList]");
            ddl_temp_state.Text = "Bihar";
            ddl_par_state.Text = "Bihar";

            mycode.bind_all_ddl_with_id(ddl_category, "select Category_Name,Category_Id from dbo.[Category_Details] order by Category_Name asc");
            mycode.bind_all_ddl_with_id(ddl_subcategory, "select Sub_CategoryName,Sub_CategoryId from dbo.[Sub_Category_Details] order by Sub_CategoryName asc");
            try
            {
                ddl_category.SelectedValue = "3";
            }
            catch (Exception ex)
            {
            }
            try
            {
                ddl_subcategory.SelectedValue = "4";
            }
            catch (Exception ex)
            {
            }

            chk_tc.Checked = true;
            bind_form_no();
            bind_house();
            bind_session();
            bind_class();
            bind_transportation();

            //My.Check_Amount(txt_paid_amount);

            empty_form();
            //if (My.mobile_mandatory == "Yes")
            //{
            //    pnl_mobile_for_mandatory.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    pnl_mobile_for_mandatory.Visibility = Visibility.Collapsed;
            //}
            //if (My.email_mandatory == "Yes")
            //{
            //    pnl_email_mandatory.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    pnl_email_mandatory.Visibility = Visibility.Collapsed;
            //}
        }

        private void bind_house()
        {
            mycode.bind_all_ddl_with_id(ddl_house, "select house_name,house_id from dbo.[house_master]");
        }
        private void bind_form_no()
        {
            //txt_form_no.Text = My.bindList("select distinct Form_no from Form_sale_details where Form_no not in(select formserialnumber from admission_registor)");
        }
        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by Session asc");
            ddlsession.SelectedValue = My.get_session_id();
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select   cd.Course_Name,cd.course_id from Add_course_table cd  order by cd.Position asc");
            mycode.bind_all_ddl_with_id(ddl_old_class, "Select   cd.Course_Name,cd.course_id from Add_course_table cd  order by cd.Position asc");
        }
        private void bind_transportation()
        {
            //txt_transportation_path.Text = My.bindList("select Pathname+','+'Bus No:-'+busno  as Pathname from TransportationPath");
        }



        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }




        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (rdb_dayscholar.Checked == true)
            {
                typeofstudent = "Day Scholar";
                hosteltaken = "No";
            }
            if (rdb_hostel.Checked == true)
            {
                typeofstudent = "Hostel Student";
                hosteltaken = "Yes";
                transportationtaken = "No";
            }
            if (rdotransyes.Checked == true)
            {
                transportation = "Bus Service";
                hosteltaken = "No";
                transportationtaken = "Yes";
            }
            if (rdotransno.Checked == true)
            {
                transportation = "No Bus Service";
                transportationtaken = "No";
            }
            //if (rdoyearly.IsChecked == true)
            //{
            //    // teachingduration = "Yearly";
            //}
            bool isvalid = check_valid_admission(txt_admission_no.Text);
            if (isvalid == false)
            {
                Alertme("Please Enter Valid Admission Number", "warning");
                txt_admission_no.Focus();
                return;
            }

            if (ddl_category.SelectedItem.Text == "Select")
            {
                Alertme("Please select category name.", "warning");
                ddl_category.Focus();
                return;
            }
            if (ddl_subcategory.SelectedItem.Text == "Select")
            {
                Alertme("Please select sub-category name.", "warning");
                ddl_subcategory.Focus();
                return;
            }

            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Warning !!!! Please fill Session with Valid Data", "warning");
                ddlsession.Focus();
                return;
            }
            if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Warning !!!! Please fill class with Valid Data", "warning");
                ddlclass.Focus();
                return;
            }

            if (ddl_section.SelectedItem.Text == "Select")
            {
                Alertme("Warning !!!!Select Section", "warning");
                return;
            }

            if (txt_dob.Text == "")
            {
                Alertme("Warning !!!! please enter date of birth", "warning");
                return;
            }

            try
            {
                bool chek_dob = My.check_valid_dob(ddlclass.SelectedItem.Text, txt_dob.Text);
                if (chek_dob == false)
                {
                    Alertme("Warning !!!! please enter valid date of birth.", "warning");
                    txt_dob.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                Alertme("Please enter valid formate of dob(dd/mm/yyyy)", "warning");
                return;
            }
            if (txt_admission_date_old.Text == "")
            {
                Alertme("Warning !!!!Please fill admission date", "warning");
                txt_admission_date_old.Focus();
                return;
            }
            if (txt_admission_date.Text == "")
            {
                Alertme("Warning !!!!Please fill current session admission date", "warning");
                txt_admission_date.Focus();
                return;
            }
            if (txt_firstname.Text == "")
            {
                Alertme("Warning !!!!Please fill Student Name with Valid Data", "warning");
                txt_student_name.Focus();
                return;
            }
            if (hosteltaken == "")
            {
                Alertme("Warning !!!!Please fill Hostel mode Yes or No", "warning");
                rdb_hostel.Focus();
                return;
            }


            if (ddl_gender.Text == "" || ddl_gender.Text == "Select")
            {
                Alertme("Please Select Gender.", "warning");
                ddl_gender.Focus();
                return;
            }
            if (txt_father_firstname.Text == "")
            {
                Alertme("Please Enter Father's Name", "warning");
                txt_father_name.Focus();
                return;
            }

            if (My.email_mandatory == "Yes")
            {
                if (txt_guardian_email.Text == "")
                {
                    Alertme("Please Enter Guardian's Email", "warning");
                    txt_guardian_email.Focus();
                    return;
                }
                if (!My.IsValidEmail(txt_guardian_email.Text))
                {
                    Alertme("Please Enter valid Email", "warning");
                    txt_guardian_email.Focus();
                    return;
                }
            }
            if (My.mobile_mandatory == "Yes")
            {
                if (!My.check_valid_mobile(txt_temp_mobileno.Text))
                {
                    Alertme("Please Enter Valid Mobile No. ", "warning");
                    ddl_gender.Focus();
                    return;
                }
            }



            if (btn_Submit.Text == "Save")// btn save first time
            {
                try
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        #region check duplicate
                        string adno = txt_admission_no.Text;
                        string roll_no = txt_rollnumber.Text;
                        while (!check_duplicate(adno))
                        {
                            if (My.Admission_no_auto == "Yes")
                            {
                                My.auto_serialS("Admission_No");
                                adno = My.view_admission_no_format("Admission_No");
                            }
                            else
                            {
                                Alertme("Sorry! Duplicate Admission No", "warning");
                                txt_admission_no.Focus();
                                return;
                            }
                        }


                        while (!check_roll_no(roll_no))
                        {
                            Alertme("Sorry! Duplicate Roll No.", "warning");
                            txt_rollnumber.Focus();
                            return;
                        }
                        txt_admission_no.Text = adno;
                        #endregion
                        register_details();
                        My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " registered a new student in session : " + ddlsession.SelectedItem.Text + " Student Name:- " + txt_student_name.Text + " Admission No : " + txt_admission_no.Text);

                        Alertme("Admission Process Completed Successfully", "success");

                        //if (MessageBox.Show("Do you want to Send SMS to Parent ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        //{
                        //    send_sms(txt_student_name.Text, ddlclass.Text, txt_section.Text, txt_temp_mobileno.Text);
                        //}

                        empty_form();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }



                }
                catch (Exception eee)
                {
                    My.Save_Exception(eee.ToString());
                }
            }
            else// update
            {

                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    string roll_no = txt_rollnumber.Text;
                    while (!check_roll_no_on_update(roll_no, HdID.Value))
                    {
                        Alertme("Sorry! Duplicate Roll No.", "warning");
                        txt_rollnumber.Focus();
                        return;
                    }

                    SqlConnection conn = new SqlConnection(My.conn);
                    SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where Id='" + HdID.Value + "'", conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {




                        dr["UID_No"] = txt_uid.Text;
                        dr["Old_class_id"] = ddl_old_class.SelectedValue;
                        dr["house"] = ddl_house.SelectedValue;
                        dr["formserialnumber"] = txt_form_slno.Text;

                        dr["dateofadmission"] = txt_admission_date.Text;
                        dr["admission_idate"] = mycode.ConvertStringToiDate(txt_admission_date.Text);


                        dr["session"] = ddlsession.SelectedItem.Text;



                        if (My.section_auto == "Yes")
                        {
                            ArrayList ar = find_section(ddlclass.Text, ddlsession.Text);
                            dr["rollnumber"] = ar[1].ToString();
                            dr["Section"] = ar[0].ToString();
                        }
                        else
                        {
                            dr["rollnumber"] = txt_rollnumber.Text;
                            dr["Section"] = ddl_section.Text;
                        }

                        if (rdb_hostel.Checked == true)
                        {
                            dr["hosteltaken"] = "Yes";
                        }
                        else
                        {
                            dr["hosteltaken"] = "No";
                        }


                        dr["Category_id"] = ddl_category.SelectedValue;
                        dr["SubCategory_id"] = ddl_subcategory.SelectedValue;


                        if (ddl_day_boarding.SelectedValue != "0")
                        {
                            if (ddl_day_boarding.SelectedValue == "1")
                            {
                                dr["is_applied_dayboarding"] = true;
                                dr["day_boarding_with_lunch"] = false;
                            }
                            else if (ddl_day_boarding.SelectedIndex == 2)
                            {
                                dr["is_applied_dayboarding"] = false;
                                dr["day_boarding_with_lunch"] = true;
                            }
                        }
                        else
                        {
                            dr["is_applied_dayboarding"] = false;
                            dr["day_boarding_with_lunch"] = false;
                        }
                        dr["class"] = ddlclass.SelectedItem.Text;

                        dr["admissionserialnumber"] = txt_admission_no.Text;
                        dr["rollnumber"] = txt_rollnumber.Text;
                        dr["Academic_Sem_or_Year_id"] = "0";

                        dr["gender"] = ddl_gender.Text;

                        //if (rdotransyes.Checked == true)
                        //{
                        //    dr["transportationtaken"] = "Yes";
                        //    dr["Transportation_Id"] = ddl_path_root.SelectedValue;
                        //    dr["Transportationpath"] = ddl_path_root.SelectedItem.Text;
                        //}
                        //else
                        //{
                        //    dr["transportationtaken"] = "No";
                        //    dr["Transportation_Id"] = "";
                        //    dr["Transportationpath"] = "";
                        //}

                        //======
                        dr["studentname"] = txt_firstname.Text + " " + txt_middlename.Text + " " + txt_lastname.Text;

                        dr["dob"] = txt_dob.Text;
                        dr["birth_certificate_number"] = txt_birth_certificate_no.Text;
                        dr["place_of_birth"] = txt_place_of_birth.Text;
                        dr["blood_group"] = ddl_blood_group.Text;

                        dr["aadharno"] = txt_aadharno_mark.Text;
                        dr["religion"] = ddl_religion.Text;
                        dr["ration_type"] = ddl_ration_cards_types.Text;
                        dr["cast"] = ddl_cast.Text;

                        dr["cast_certificate_no"] = txt_cast_certificate_no.Text;
                        dr["student_mother_tounge"] = ddl_student_mother_tongue.Text;
                        dr["is_illness"] = ddl_illness.Text;
                        dr["illness_remark"] = txt_illness_remark.Text;

                        dr["currentschool"] = txt_school_current.Text;
                        dr["jati"] = txt_jati.Text;
                        dr["RTE"] = ddl_rte.Text;
                        dr["staff_ward"] = ddl_staff_ward.Text;

                        //======= ///Father Info
                        dr["fathername"] = txt_father_firstname.Text + " " + txt_father_middle_name.Text + " " + txt_father_lastname.Text;

                        dr["occuption"] = ddl_occupation.Text;
                        dr["fatherqualification"] = txt_father_qualification.Text;
                        dr["f_nationality"] = txt_f_nationality.Text;

                        dr["f_marrital_statue"] = ddl_maritial_status.Text;
                        dr["father_mob"] = txt_guardian_mob.Text;
                        dr["email_id"] = txt_guardian_email.Text;
                        dr["guardianname"] = txt_guardian_name.Text;
                        dr["parentincome"] = txt_parent_income.Text;

                        //======= Mother Info
                        dr["mothername"] = txt_first_name_mother.Text + " " + txt_middle_name_mother.Text + " " + txt_last_name_mother.Text;
                        dr["m_occupation"] = ddl_m_occupation.Text;
                        dr["motherqualifiaction"] = txt_mother_qulalifiaction.Text;

                        dr["m_nationality"] = txt_m_nationality.Text;
                        dr["m_marrital_statue"] = ddl_m_maritial_status.Text;
                        dr["mother_mob"] = txt_mother_mob.Text;
                        dr["mother_email"] = txt_mother_email.Text;

                        //======= 
                        dr["careof"] = txt_temp_address.Text;
                        dr["mobilenumber"] = txt_temp_mobileno.Text;
                        dr["city"] = txt_temp_city.Text;
                        dr["district"] = txt_temp_district.Text;

                        dr["postoffice"] = txt_temp_po.Text;
                        dr["policestation"] = txt_temp_ps.Text;
                        if (ddl_cunterycode3.Text == "+91")
                        {
                            dr["state"] = ddl_temp_state.Text;
                        }
                        else
                        {
                            dr["state"] = txt_c_state.Text;
                        }
                        dr["pin"] = txt_temp_pin.Text;

                        //======= 
                        dr["careof_permanent"] = txt_par_address.Text;
                        dr["mob2"] = txt_per_mobileno.Text;
                        dr["city_permanent"] = txt_par_city.Text;
                        dr["district_permanent"] = txt_par_district.Text;

                        dr["postoffice_permanent"] = txt_par_po.Text;
                        dr["policestation_permanent"] = txt_par_ps.Text;
                        if (ddl_cunterycode4.Text == "+91")
                        {
                            dr["state_permanent"] = ddl_par_state.Text;
                        }
                        else
                        {
                            dr["state_permanent"] = txt_p_state.Text;
                        }
                        dr["pincode"] = txt_par_pin.Text;

                        //======= 
                        dr["Account_Holder_name"] = txt_account_holder_name.Text;
                        dr["Bnk_Name"] = txt_bank_name.Text;
                        dr["IFSC_Code"] = txt_ifsc_code.Text;
                        dr["Branch_Name"] = txt_branch_name.Text;
                        dr["StudentStatus"] = "AV";
                        dr["Branch_id"] = ViewState["branchid"].ToString();


                        dr["Student_Name_First"] = txt_firstname.Text.Trim();
                        dr["Student_Middle_Name"] = txt_middlename.Text.Trim();
                        dr["Student_Name_Last"] = txt_lastname.Text.Trim();

                        dr["Father_Name_First"] = txt_father_firstname.Text;
                        dr["Father_Name_Middle"] = txt_father_middle_name.Text;
                        dr["Father_Name_Last"] = txt_father_lastname.Text;

                        dr["Mother_Name_First"] = txt_first_name_mother.Text;
                        dr["Mother_Name_Middle"] = txt_middle_name_mother.Text;
                        dr["Mother_Name_Last"] = txt_last_name_mother.Text;
                        dr["Personal_Identymarks"] = txt_persnal_identfication_marks.Text;

                        dr["Country_Code_Father"] = ddl_cunterycode1.Text;
                        dr["Country_Code_Mother"] = ddl_cunterycode2.Text;
                        dr["Country_Code_Current_add"] = ddl_cunterycode3.Text;
                        dr["Country_Code_Current_Perm_add"] = ddl_cunterycode4.Text;
                        dr["Bank_acount_no"] = txt_account_no.Text;

                        if (ddl_student_type.Text == "New")
                        {
                            dr["Transfer_Status"] = "New";
                        }
                        else
                        {
                            dr["Transfer_Status"] = "NT";
                        }
                        dr["College_School_Name"] = ViewState["college_name"].ToString();
                        dr["User_id"] = ViewState["Userid"].ToString();

                        dr["Old_Admission_Date"] = txt_admission_date_old.Text;
                        dr["OLd_Admission_Idate"] = mycode.ConvertStringToiDate(txt_admission_date_old.Text);
                    }
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);

                    update_old_class(txt_admission_no.Text);
                    update_academic();
                    save_images();

                    empty_form();
                    Session["MsgeS"] = "Student details has been updated successfully.";
                    Response.Redirect("student-list.aspx", false);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");

                }




            }
        }

        private void update_old_class(string admission_no)
        {
            string qrys = "update admission_registor set Old_class_id='" + ddl_old_class.SelectedValue + "',Old_Admission_Date='" + txt_admission_date_old.Text + "',OLd_Admission_Idate='" + My.DateConvertToIdate(txt_admission_date_old.Text) + "',UID_No='" + txt_uid.Text + "' where admissionserialnumber='" + admission_no + "'";
            My.exeSql(qrys);
        }


        private void update_academic()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Academic_Education_Details_Of_Student where Admission_No='" + ViewState["admno"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count.ToString() != "0")
            {
                foreach (DataRow dr in dt.Rows)
                {

                    if (tenthDoc.HasFile)
                    {
                        dr["tenth_doc"] = upload_image(tenthDoc, "tenth_doc");
                    }

                    if (ten_plus_doc.HasFile)
                    {
                        dr["twelve_doc"] = upload_image(ten_plus_doc, "twelve_doc");
                    }

                    if (graduation_doc.HasFile)
                    {
                        dr["graduation_doc"] = upload_image(graduation_doc, "graduation_doc");
                    }

                    if (post_graduation_doc.HasFile)
                    {
                        dr["post_graduation_doc"] = upload_image(post_graduation_doc, "post_graduation_doc");
                    }

                    if (other_doc.HasFile)
                    {
                        dr["other_doc"] = upload_image(other_doc, "other_doc");
                    }


                    dr["Tenth_Board"] = txt_tenth_board.Text;
                    dr["Tenth_year_of_passing"] = txt_tenth_passing_year.Text;
                    dr["Tenth_total_mark_obtain"] = txt_tenth_total_obtai_mark.Text;
                    dr["Tenth_percentage_mark"] = txt_tenth_percentage_mark.Text;
                    dr["Tenth_division"] = txt_tenth_devision.Text;
                    dr["Tenth_subjects"] = txt_tenth_subject.Text;
                    //====TEN

                    //=====
                    dr["Twelve_Boards"] = txt_twelve_board.Text;
                    dr["Twelve_year_of_passing"] = txt_twelve_passing_year.Text;
                    dr["Twelve_total_mark_obtain"] = txt_twelve_total_mark.Text;
                    dr["Twelve_percentage_mark"] = txt_twelve_percentage_of_mark.Text;
                    dr["Twelve_division"] = txt_twelve_division.Text;
                    dr["Twelve_subjects"] = txt_twelve_subjects.Text;

                    //==== 
                    dr["Graduation_Board"] = txt_graduation_university.Text;
                    dr["Graduation_year_of_passing"] = txt_graduation_year_of_passing.Text;
                    dr["Graduation_total_mark_obtain"] = txt_graduation_total_mark_obtains.Text;
                    dr["Graduation_percentage_mark"] = txt_graduation_percentage_of_mark.Text;
                    dr["Graduation_division"] = txt_graduation_division.Text;
                    dr["Graduation_subjects"] = txt_graduation_subject.Text;

                    //====
                    dr["Post_Graduation_Board"] = txt_post_graduation_university.Text;
                    dr["Post_Graduation_year_of_passing"] = txt_post_graduation_year_of_passing.Text;
                    dr["Post_Graduation_total_mark_obtain"] = txt_post_graduation_total_mark_obtain.Text;
                    dr["Post_Graduation_percentage_mark"] = txt_post_graduation_percentage_of_mark.Text;
                    dr["Post_Graduation_division"] = txt_post_graduation_division.Text;
                    dr["Post_Graduation_subjects"] = txt_post_graduation_subject.Text;

                    //====
                    dr["Other_Board"] = txt_other_board.Text;
                    dr["Other_year_of_passing"] = txt_other_passing_year.Text;
                    dr["Other_total_mark_obtain"] = txt_other_mark_obtain.Text;
                    dr["Other_percentage_mark"] = txt_other_percentage_of_mark.Text;
                    dr["Other_division"] = txt_other_division.Text;
                    dr["Other_subjects"] = txt_other_subjects.Text;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
            else
            {
                //save_academic_details();
            }
        }


        string type;
        private void register_details()
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Academic_Education_Details_Of_Student");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["UID_No"] = txt_uid.Text;
                dr["Old_class_id"] = ddl_old_class.SelectedValue;
                dr["class"] = ddlclass.SelectedItem.Text;
                dr["formserialnumber"] = txt_form_slno.Text;
                dr["admissionserialnumber"] = txt_admission_no.Text;
                if (My.section_auto == "Yes")
                {
                    ArrayList ar = find_section(ddlclass.Text, ddlsession.Text);
                    dr["rollnumber"] = ar[1].ToString();
                    dr["Section"] = ar[0].ToString();
                }
                else
                {
                    dr["rollnumber"] = txt_rollnumber.Text;
                    dr["Section"] = ddl_section.Text;
                }

                dr["session"] = ddlsession.SelectedItem.Text;
                dr["dateofadmission"] = txt_admission_date.Text;
                dr["studentname"] = txt_firstname.Text + " " + txt_middlename.Text + " " + txt_lastname.Text;

                dr["Category_id"] = ddl_category.SelectedValue;
                dr["SubCategory_id"] = ddl_subcategory.SelectedValue;

                if (chk_tc.Checked == true)
                {
                    dr["dobverificationdocument1"] = "Transfercetificate";
                }
                else
                {
                    dr["dobverificationdocument1"] = "";
                }

                if (chkaffidivit.Checked == true)
                {
                    dr["dobverificationdocument2"] = "Affidivit";
                }
                else
                {
                    dr["dobverificationdocument2"] = "";
                }
                dr["gender"] = ddl_gender.Text;
                dr["identifacationmark"] = "Not Available";
                dr["currentschool"] = txt_school_current.Text;

                dr["dob"] = txt_dob.Text;
                dr["fathername"] = txt_father_firstname.Text + " " + txt_father_middle_name.Text + " " + txt_father_lastname.Text;
                dr["fatherqualification"] = txt_father_qualification.Text;

                dr["mothername"] = txt_first_name_mother.Text + " " + txt_middle_name_mother.Text + " " + txt_last_name_mother.Text;
                dr["motherqualifiaction"] = txt_mother_qulalifiaction.Text;
                dr["guardianname"] = txt_guardian_name.Text;

                dr["relation"] = "Not Available";
                dr["occuption"] = ddl_occupation.Text;
                dr["religion"] = ddl_religion.Text;

                dr["cast"] = ddl_cast.Text;
                dr["parentincome"] = txt_parent_income.Text;
                dr["careof"] = txt_temp_address.Text;

                dr["careof"] = txt_temp_address.Text;
                dr["city"] = txt_temp_city.Text;
                dr["postoffice"] = txt_temp_po.Text;

                dr["policestation"] = txt_temp_ps.Text;
                dr["district"] = txt_temp_district.Text;
                dr["state"] = ddl_temp_state.Text;
                dr["pin"] = txt_temp_pin.Text;
                dr["mobilenumber"] = txt_temp_mobileno.Text;
                dr["careof_permanent"] = txt_par_address.Text;

                dr["city_permanent"] = txt_par_city.Text;
                dr["postoffice_permanent"] = txt_par_po.Text;
                dr["policestation_permanent"] = txt_par_ps.Text;
                dr["district_permanent"] = txt_par_district.Text;
                dr["state_permanent"] = ddl_par_state.Text;
                dr["pincode"] = txt_par_pin.Text;


                dr["payment_status"] = "Unpaid";
                if (rdb_hostel.Checked == true)
                {
                    dr["hosteltaken"] = "Yes";
                }
                else
                {
                    dr["hosteltaken"] = "No";
                }

                dr["aadharno"] = txt_aadharno_mark.Text;
                dr["RTE"] = ddl_rte.Text;
                dr["house"] = ddl_house.SelectedValue;


                dr["admission_idate"] = mycode.ConvertStringToiDate(txt_admission_date.Text);
                dr["staff_ward"] = ddl_staff_ward.Text;
                dr["jati"] = txt_jati.Text;
                dr["mob2"] = txt_per_mobileno.Text;
                dr["Hostel_id"] = "0";
                dr["Session_id"] = ddlsession.SelectedValue;

                dr["Class_id"] = ddlclass.SelectedValue;
                dr["Is_TC_Taken"] = false;
                dr["Student_id"] = txt_admission_no.Text;


                if (ddl_day_boarding.SelectedValue != "0")
                {
                    if (ddl_day_boarding.SelectedValue == "1")
                    {
                        dr["is_applied_dayboarding"] = true;
                        dr["day_boarding_with_lunch"] = false;
                    }
                    else if (ddl_day_boarding.SelectedIndex == 2)
                    {
                        dr["is_applied_dayboarding"] = false;
                        dr["day_boarding_with_lunch"] = true;
                    }
                }
                else
                {
                    dr["is_applied_dayboarding"] = false;
                    dr["day_boarding_with_lunch"] = false;
                }

                dr["email_id"] = txt_guardian_email.Text;
                dr["birth_certificate_number"] = txt_birth_certificate_no.Text;
                dr["place_of_birth"] = txt_place_of_birth.Text;
                dr["blood_group"] = ddl_blood_group.Text;
                dr["cast_certificate_no"] = txt_cast_certificate_no.Text;
                dr["student_mother_tounge"] = ddl_student_mother_tongue.Text;
                dr["ration_type"] = ddl_ration_cards_types.Text;
                dr["is_illness"] = ddl_illness.Text;
                dr["f_nationality"] = txt_f_nationality.Text;
                dr["f_marrital_statue"] = ddl_maritial_status.Text;
                dr["m_marrital_statue"] = ddl_m_maritial_status.Text;
                dr["m_nationality"] = txt_m_nationality.Text;
                dr["m_occupation"] = ddl_m_occupation.Text;
                dr["illness_remark"] = txt_illness_remark.Text;
                dr["father_mob"] = txt_guardian_mob.Text;
                dr["mother_mob"] = txt_mother_mob.Text;
                dr["mother_email"] = txt_account_holder_name.Text;
                dr["Account_Holder_name"] = txt_m_nationality.Text;
                dr["Bnk_Name"] = txt_bank_name.Text;
                dr["IFSC_Code"] = txt_ifsc_code.Text;
                dr["Branch_Name"] = txt_branch_name.Text;
                dr["Course_Type"] = type;
                dr["Academic_Sem_or_Year"] = "";
                dr["Academic_Sem_or_Year_id"] = "0";

                dr["Branch_id"] = ViewState["branchid"].ToString();
                dr["Student_Name_First"] = txt_firstname.Text.Trim();
                dr["Student_Middle_Name"] = txt_middlename.Text.Trim();
                dr["Student_Name_Last"] = txt_lastname.Text.Trim();

                dr["Father_Name_First"] = txt_father_firstname.Text;
                dr["Father_Name_Middle"] = txt_father_middle_name.Text;
                dr["Father_Name_Last"] = txt_father_lastname.Text;

                dr["Mother_Name_First"] = txt_first_name_mother.Text;
                dr["Mother_Name_Middle"] = txt_middle_name_mother.Text;
                dr["Mother_Name_Last"] = txt_last_name_mother.Text;
                dr["Personal_Identymarks"] = txt_persnal_identfication_marks.Text;


                dr["Country_Code_Father"] = ddl_cunterycode1.Text;
                dr["Country_Code_Mother"] = ddl_cunterycode2.Text;
                dr["Country_Code_Current_add"] = ddl_cunterycode3.Text;
                dr["Country_Code_Current_Perm_add"] = ddl_cunterycode4.Text;
                dr["StudentStatus"] = "AV";
                dr["Pwd"] = My.create_random_no_otp();
                if (ddl_student_type.Text == "New")
                {
                    dr["Transfer_Status"] = "New";
                }
                else
                {
                    dr["Transfer_Status"] = "NT";
                }
                dr["Bank_acount_no"] = txt_account_no.Text;
                dr["Verification_Istatus"] = "0";
                dr["Bank_acount_no"] = "0";
                dr["Status"] = "1";
                dr["College_School_Name"] = ViewState["college_name"].ToString();
                dr["Old_Admission_Date"] = txt_admission_date_old.Text;
                dr["OLd_Admission_Idate"] = mycode.ConvertStringToiDate(txt_admission_date_old.Text);
                dt.Rows.Add(dr);
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            // save_academic_details();
            save_images();


        }
        private bool check_valid_admission(string p)
        {
            bool valid = false;
            var r = new Regex("[a-zA-Z0-9/]");
            if (r.IsMatch(p))
            {
                valid = true;
            }
            if (p.Contains("'") || p.Contains("%"))
            {
                valid = false;
            }
            return valid;
        }
        private void save_academic_details()
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Academic_Education_Details_Of_Student where Class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.Text + "' and Admission_No='" + txt_admission_no.Text + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Academic_Education_Details_Of_Student");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["Admission_No"] = txt_admission_no.Text;
                dr["Class"] = ddlclass.SelectedItem.Text;
                dr["Class_Id"] = ddlclass.SelectedValue;
                dr["Roll_No"] = txt_rollnumber.Text;
                dr["Section"] = ddl_section.Text;
                dr["Session"] = ddlsession.SelectedItem.Text;
                dr["Session_Id"] = ddlsession.SelectedValue;
                dr["Tenth_Board"] = txt_tenth_board.Text;
                dr["Tenth_year_of_passing"] = txt_tenth_passing_year.Text;
                dr["Tenth_total_mark_obtain"] = txt_tenth_total_obtai_mark.Text;
                dr["Tenth_percentage_mark"] = txt_tenth_percentage_mark.Text;
                dr["Tenth_division"] = txt_tenth_devision.Text;
                dr["Tenth_subjects"] = txt_tenth_subject.Text;
                dr["Twelve_Boards"] = txt_twelve_board.Text;
                dr["Twelve_year_of_passing"] = txt_twelve_passing_year.Text;
                dr["Twelve_total_mark_obtain"] = txt_twelve_total_mark.Text;
                dr["Twelve_percentage_mark"] = txt_twelve_percentage_of_mark.Text;
                dr["Twelve_division"] = txt_twelve_division.Text;
                dr["Twelve_subjects"] = txt_twelve_subjects.Text;
                dr["Graduation_Board"] = txt_graduation_university.Text;
                dr["Graduation_year_of_passing"] = txt_graduation_year_of_passing.Text;
                dr["Graduation_total_mark_obtain"] = txt_graduation_total_mark_obtains.Text;
                dr["Graduation_percentage_mark"] = txt_graduation_percentage_of_mark.Text;
                dr["Graduation_division"] = txt_graduation_division.Text;
                dr["Graduation_subjects"] = txt_graduation_subject.Text;
                dr["Post_Graduation_Board"] = txt_post_graduation_university.Text;
                dr["Post_Graduation_year_of_passing"] = txt_post_graduation_year_of_passing.Text;
                dr["Post_Graduation_total_mark_obtain"] = txt_post_graduation_total_mark_obtain.Text;
                dr["Post_Graduation_percentage_mark"] = txt_post_graduation_percentage_of_mark.Text;
                dr["Post_Graduation_division"] = txt_post_graduation_division.Text;
                dr["Post_Graduation_subjects"] = txt_post_graduation_subject.Text;
                dr["Other_Board"] = txt_other_board.Text;
                dr["Other_year_of_passing"] = txt_other_passing_year.Text;
                dr["Other_total_mark_obtain"] = txt_other_mark_obtain.Text;
                dr["Other_percentage_mark"] = txt_other_percentage_of_mark.Text; ;
                dr["Other_division"] = txt_other_division.Text;
                dr["Other_subjects"] = txt_other_subjects.Text;
                dr["user_id"] = ViewState["Userid"].ToString();

                if (tenthDoc.HasFile)
                {
                    dr["tenth_doc"] = upload_image(tenthDoc, "tenth_doc");
                }
                else
                {
                    dr["tenth_doc"] = null;
                }
                if (ten_plus_doc.HasFile)
                {
                    dr["twelve_doc"] = upload_image(ten_plus_doc, "twelve_doc");
                }
                else
                {
                    dr["twelve_doc"] = null;
                }
                if (graduation_doc.HasFile)
                {
                    dr["graduation_doc"] = upload_image(graduation_doc, "graduation_doc");
                }
                else
                {
                    dr["graduation_doc"] = null;
                }
                if (post_graduation_doc.HasFile)
                {
                    dr["post_graduation_doc"] = upload_image(post_graduation_doc, "post_graduation_doc");
                }
                else
                {
                    dr["post_graduation_doc"] = null;
                }
                if (other_doc.HasFile)
                {
                    dr["other_doc"] = upload_image(other_doc, "other_doc");
                }
                else
                {
                    dr["other_doc"] = null;
                }
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (tenthDoc.HasFile)
                    {
                        dr["tenth_doc"] = upload_image(tenthDoc, "tenth_doc");
                    }
                    else { }
                    if (ten_plus_doc.HasFile)
                    {
                        dr["twelve_doc"] = upload_image(ten_plus_doc, "twelve_doc");
                    }
                    else { }
                    if (graduation_doc.HasFile)
                    {
                        dr["graduation_doc"] = upload_image(graduation_doc, "graduation_doc");
                    }
                    else { }
                    if (post_graduation_doc.HasFile)
                    {
                        dr["post_graduation_doc"] = upload_image(post_graduation_doc, "post_graduation_doc");
                    }
                    else { }
                    if (other_doc.HasFile)
                    {
                        dr["other_doc"] = upload_image(other_doc, "other_doc");
                    }
                    else { }
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private ArrayList find_section(string classs, string session)
        {
            ArrayList ar = new ArrayList();
            DataTable dt = My.dataTable("select top 1 Class,Section,cast((Select count(*) from admission_registor where session='" + session + "' and class='" + classs + "' and Section=strength_master.Section) as float)+1 roll,No_of_student,(Select count(*) from admission_registor where session='" + session + "' and class='" + classs + "' and Section=strength_master.Section) rewg_std,(cast(No_of_student as float)-cast(((Select count(*) from admission_registor where session='" + session + "' and class='" + classs + "' and Section=strength_master.Section)) as float))available from strength_master where Class='" + classs + "' and (cast(No_of_student as float)-cast(((Select count(*) from admission_registor where session='" + session + "' and class='" + classs + "' and Section=strength_master.Section)) as float))>0 order by Section");
            if (dt.Rows.Count == 0)
            {
                Alertme("Please Create Strength Master First...", "warning");
            }
            ar.Add(dt.Rows[0]["Section"].ToString());
            ar.Add(dt.Rows[0]["roll"].ToString());
            return ar;

        }

        private void save_images()
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Student_Image_List where Admission_no='" + txt_admission_no.Text + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Image_List");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["Admission_no"] = txt_admission_no.Text;
                dr["Date"] = DateTime.UtcNow.AddHours(5).AddMinutes(30);

                if (FileUpload1.HasFile)
                {
                    string img = upload_image(FileUpload1, "StudentImg");
                    if (img == "")
                    {
                        dr["Student_image"] = "";
                    }
                    else
                    {
                        dr["Student_image"] = img;
                    }
                }



                if (FileUpload2.HasFile)
                {
                    string img = upload_image(FileUpload2, "Parent_Sign");
                    if (img == "")
                    {
                        dr["Parent_Sign"] = "";
                    }
                    else
                    {
                        dr["Parent_Sign"] = img;
                    }
                }


                if (FileUpload3.HasFile)
                {
                    string img = upload_image(FileUpload3, "Tc_image");
                    if (img == "")
                    {
                        dr["Tc_image"] = "";
                    }
                    else
                    {
                        dr["Tc_image"] = img;
                    }

                }

                if (FileUpload4.HasFile)
                {
                    string img = upload_image(FileUpload4, "DOB_image");
                    if (img == "")
                    {
                        dr["DOB_image"] = "";
                    }
                    else
                    {
                        dr["DOB_image"] = img;
                    }


                }

                if (FileUpload5.HasFile)
                {
                    string img = upload_image(FileUpload5, "Adm_form");
                    if (img == "")
                    {
                        dr["Adm_form"] = "";
                    }
                    else
                    {
                        dr["Adm_form"] = img;
                    }


                }



                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (FileUpload1.HasFile)
                    {
                        string img = upload_image(FileUpload1, "StudentImg");
                        if (img == "")
                        {

                        }
                        else
                        {
                            dr["Student_image"] = img;
                        }

                    }
                    else
                    {

                    }
                    if (FileUpload2.HasFile)
                    {
                        string img = upload_image(FileUpload2, "Parent_Sign");
                        if (img == "")
                        {

                        }
                        else
                        {
                            dr["Parent_Sign"] = img;
                        }

                    }
                    else { }
                    if (FileUpload3.HasFile)
                    {
                        string img = upload_image(FileUpload3, "Tc_image");
                        if (img == "")
                        {

                        }
                        else
                        {
                            dr["Tc_image"] = img;
                        }

                    }
                    else { }
                    if (FileUpload4.HasFile)
                    {
                        string img = upload_image(FileUpload4, "DOB_image");
                        if (img == "")
                        {

                        }
                        else
                        {
                            dr["DOB_image"] = img;
                        }

                    }
                    else { }
                    if (FileUpload5.HasFile)
                    {
                        string img = upload_image(FileUpload5, "DOB_image");
                        if (img == "")
                        {

                        }
                        else
                        {
                            dr["Adm_form"] = img;
                        }


                    }
                    else { }
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);


            if (FileUpload1.HasFile)
            {
                string img = upload_image(FileUpload1, "StudentImg");
                if (img == "")
                {

                }
                else
                {
                    mycode.executequery("update admission_registor set studentimagepath='" + img + "' where admissionserialnumber='" + txt_admission_no.Text + "'");

                }
            }
        }




        private bool check_duplicate(string adno)
        {
            DataTable dt = My.dataTable(" select admissionserialnumber from admission_registor where admissionserialnumber='" + adno + "' ");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool check_roll_no(string roll)
        {
            DataTable dt = My.dataTable(" select admissionserialnumber from admission_registor where rollnumber='" + roll + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + ddlsession.SelectedValue + "'");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool check_roll_no_on_update(string roll, string rowid)
        {
            DataTable dt = My.dataTable(" select admissionserialnumber from admission_registor where rollnumber='" + roll + "' and Class_id='" + ddlclass.SelectedValue + "' and  Section='" + ddl_section.Text + "' and Session_id='" + ddlsession.SelectedValue + "'  and Id!=" + rowid + " and Status='1'");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void rdotransyes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_hostel.Checked == true)
            {
                Alertme("Sorry you can't choos transportaion", "warning");
                rdotransno.Checked = true;
                rdotransyes.Checked = false;

            }
            else
            {
                rdb_hostel.Checked = false;
                if (rdotransyes.Checked == true)
                {
                    transportpath.Visible = true;
                }
                else
                {
                    transportpath.Visible = false;
                }
            }
        }

        protected void rdotransno_CheckedChanged(object sender, EventArgs e)
        {
            if (rdotransyes.Checked == true)
            {
                transportpath.Visible = true;
            }
            else
            {
                transportpath.Visible = false;
            }
            //pnl_transportation_path.Visibility = Visibility.Collapsed;
        }

        #region FileUploaD
        private string upload_image(FileUpload FU, string FNmae)
        {
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
            string dbfilepath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;

            int k = 0;
            if (FU.HasFile)
            {
                if (FU.FileBytes.Length < 2000000)
                {
                    Session["WorkingImage"] = FU.FileName;
                    string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                    string[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif", ".webp" };
                    Session["WorkingImage1"] = FNmae + idate + time + FileExtension;
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                    }
                }
                else
                {
                    Alertme("Please reduce image size (Max 200kb)", "warning");
                    return "";
                }
            }
            else
            {
            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    FU.SaveAs(path + "/" + Session["WorkingImage1"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
            else
            {
                Alertme("Please select jpg and png image", "warning");
            }
            if (FileSaved)
            {
                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                string fileName = Path.GetFileName(Session["WorkingImage1"].ToString());
                dbfilepath = originalPath1 + "/Master_Img/Student/" + fileName;
            }
            return dbfilepath;
        }
        #endregion

        #region EmptyForM
        private void empty_form()
        {
            btn_Submit.Text = "Save";
            btn_cancel.Visible = false;
            txt_rollnumber.Text = "";
            txt_admission_no.ReadOnly = false;
            ddlsession.Enabled = true;
            ddlclass.Enabled = true;
            txt_form_slno.Text = "";
            if (My.Admission_no_auto == "Yes")
            {
                txt_admission_no.Text = My.view_admission_no_format("Admission_No");
            }
            else
            {
                txt_admission_no.Text = "";
                txt_admission_no.ReadOnly = false;
            }
            txt_student_name.Text = "";
            ddl_gender.SelectedIndex = 0;
            txt_school_current.Text = "";
            txt_dob.Text = "";
            txt_father_name.Text = "";
            txt_father_qualification.Text = "";
            txt_mother_name.Text = "";
            txt_mother_qulalifiaction.Text = "";
            txt_guardian_name.Text = "";
            ddl_occupation.SelectedIndex = 0;
            txt_parent_income.Text = "";
            txt_temp_address.Text = "";
            txt_temp_city.Text = "";
            txt_temp_po.Text = "";
            txt_temp_ps.Text = "";
            txt_temp_district.Text = "";

            txt_temp_pin.Text = "";
            txt_temp_mobileno.Text = "";
            txt_par_address.Text = "";
            txt_per_mobileno.Text = "";
            txt_par_city.Text = "";
            txt_par_po.Text = "";
            txt_par_ps.Text = "";
            txt_par_district.Text = "";
            txt_par_pin.Text = "";
            txt_aadharno_mark.Text = "";


            txt_place_of_birth.Text = "";
            ddl_blood_group.SelectedIndex = 0;
            txt_cast_certificate_no.Text = "";
            ddl_ration_cards_types.SelectedIndex = 0;

            txt_illness_remark.Text = "";
            txt_f_nationality.Text = "";
            txt_m_nationality.Text = "";

            txt_guardian_mob.Text = "";
            txt_mother_mob.Text = "";
            txt_mother_email.Text = "";

            txt_account_holder_name.Text = "";
            txt_bank_name.Text = "";
            txt_ifsc_code.Text = "";
            txt_branch_name.Text = "";


            txt_tenth_board.Text = "";
            txt_tenth_passing_year.Text = "";
            txt_tenth_total_obtai_mark.Text = "";
            txt_tenth_percentage_mark.Text = "";
            txt_tenth_devision.Text = "";
            txt_tenth_subject.Text = "";
            txt_twelve_board.Text = "";
            txt_twelve_passing_year.Text = "";
            txt_twelve_total_mark.Text = "";
            txt_twelve_percentage_of_mark.Text = "";
            txt_twelve_division.Text = "";
            txt_twelve_subjects.Text = "";
            txt_graduation_university.Text = "";
            txt_graduation_year_of_passing.Text = "";
            txt_graduation_total_mark_obtains.Text = "";
            txt_graduation_percentage_of_mark.Text = "";
            txt_graduation_division.Text = "";
            txt_graduation_subject.Text = "";
            txt_post_graduation_university.Text = "";
            txt_post_graduation_year_of_passing.Text = "";
            txt_post_graduation_total_mark_obtain.Text = "";
            txt_post_graduation_percentage_of_mark.Text = "";
            txt_post_graduation_division.Text = "";
            txt_post_graduation_subject.Text = "";
            txt_other_board.Text = "";
            txt_other_passing_year.Text = "";
            txt_other_mark_obtain.Text = "";
            txt_other_percentage_of_mark.Text = "";
            txt_other_division.Text = "";
            txt_other_subjects.Text = "";
           

            txt_firstname.Text = "";
            txt_middlename.Text = "";
            txt_lastname.Text = "";
            txt_persnal_identfication_marks.Text = "";


            txt_father_firstname.Text = "";
            txt_father_middle_name.Text = "";
            txt_father_lastname.Text = "";

            txt_first_name_mother.Text = "";
            txt_middle_name_mother.Text = "";
            txt_last_name_mother.Text = "";
        }
        #endregion








        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
            btn_cancel.Visible = false;
            btn_Submit.Text = "Save";
            Response.Redirect("student-list.aspx", false);
        }

        protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id(ddl_subcategory, "select Sub_CategoryName,Sub_CategoryId from dbo.[Sub_Category_Details] where Category_Id='" + ddl_category.SelectedValue + "' order by Sub_CategoryName asc");
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_illness_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_illness.Text == "NO")
            {
                txt_illness_remark.ReadOnly = true;
            }
            else
            {
                txt_illness_remark.ReadOnly = false;
            }

        }



        protected void ddl_cunterycode3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_c_country();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_c_country()
        {
            My.bind_ddl_noselect(ddl_country_c, "select Country_name from Country_list where Country_code='" + ddl_cunterycode3.Text + "'");
            if (ddl_cunterycode3.SelectedValue == "+91")
            {
                txt_c_state.Visible = false;
                ddl_temp_state.Visible = true;
            }
            else
            {
                txt_c_state.Visible = true;
                ddl_temp_state.Visible = false;
            }
        }

        protected void ddl_cunterycode4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_p_country();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_p_country()
        {
            My.bind_ddl_noselect(ddl_country_p, "select Country_name from Country_list where Country_code='" + ddl_cunterycode4.Text + "'");
            if (ddl_cunterycode4.SelectedValue == "+91")
            {
                txt_p_state.Visible = false;
                ddl_par_state.Visible = true;
            }
            else
            {
                txt_p_state.Visible = true;
                ddl_par_state.Visible = false;
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBox1.Checked == true)
                {
                    txt_par_address.Text = txt_temp_address.Text;
                    ddl_cunterycode4.Text = ddl_cunterycode3.Text;
                    txt_per_mobileno.Text = txt_temp_mobileno.Text;
                    txt_par_city.Text = txt_temp_city.Text;
                    txt_par_district.Text = txt_temp_district.Text;
                    txt_par_po.Text = txt_temp_po.Text;
                    txt_par_ps.Text = txt_temp_ps.Text;

                    ddl_par_state.Text = ddl_temp_state.Text;
                    txt_par_pin.Text = txt_temp_pin.Text;
                    ddl_country_p.Text = ddl_country_c.Text;
                }
                else
                {

                    txt_par_address.Text = "";
                    ddl_cunterycode4.Text = "Select";
                    txt_per_mobileno.Text = "";
                    txt_par_city.Text = "";
                    txt_par_district.Text = "";
                    txt_par_po.Text = "";
                    txt_par_ps.Text = "";
                    txt_par_ps.Text = "";
                    ddl_par_state.Text = "Select";
                    txt_par_pin.Text = "";
                    ddl_country_p.Text = "Select";
                }
            }
            catch
            {

            }

        }
    }
}