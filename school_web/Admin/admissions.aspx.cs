using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class admissions : System.Web.UI.Page
    {
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
        My mycodeMY = new My();
        compLN comP = new compLN();
        UsesCode mycode = new UsesCode();
        My mycodeMy = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Session["Admin"] = HttpContext.Current.User.Identity.Name;
                Session["userType"] = HttpContext.Current.User.Identity.getUserData("UserType");
                Session["branchid"] = HttpContext.Current.User.Identity.getUserData("branchid");
                Session["firm"] = HttpContext.Current.User.Identity.getUserData("Firm");
            }
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
                        if (Session["successMsgs"] != null)
                        {
                            Alertme(Session["successMsgs"].ToString(), "success");
                            Session["successMsgs"] = null;
                        }
                        try
                        {
                            ViewState["printslip"] = My.getprint_slip_permission();
                        }
                        catch
                        {

                        }
                        ViewState["IsFromEnq"] = "0";
                        ViewState["IsUpdate"] = "0";
                        ViewState["IsTransfer"] = "0";

                        ViewState["isImageSaved"] = "0";
                        ViewState["IsTransferFormSale"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        //txt_admission_date_old.Text = mycode.date();


                        firm_details();

                        ViewState["branchid"] = mycodeMy.get_branch_id(ViewState["Userid"].ToString());
                        //hd_temp_reg_id.Value = temp_reg_id();
                        txt_admission_date.Text = mycode.date();
                        comP.bind_ddl_no_select(ddl_nationality, "select Country_name from Country_list");
                        comP.bind_ddl(ddl_cast, "select Category_name from Cast_category");
                        comP.bind_ddl(ddl_student_mother_tongue, "select Language from Language_Master order by Language asc");

                        comP.bind_ddl_NA(ddl_father_qualification, "select Name from Qualification_master");
                        comP.bind_ddl_NA(ddl_mother_qualification, "select Name from Qualification_master");

                        comP.bind_ddl_no_select(ddl_f_nationality, "select Country_name from Country_list");
                        comP.bind_ddl_no_select(ddl_m_nationality, "select Country_name from Country_list");



                        string sessionid = My.get_session_id_onlinereg();
                        ViewState["sessionid"] = sessionid;
                        ViewState["FirmId"] = My.get_firm_id();

                        page_load();


                        txt_admission_no.ReadOnly = false;
                        if (Request.QueryString["stdid"] != null)
                        {
                            ViewState["Edtfrom"] = "0";
                            try
                            {
                                ViewState["Edtfrom"] = Request.QueryString["from"].ToString();
                            }
                            catch (Exception ex)
                            { }

                            ViewState["IsUpdate"] = "1";
                            txt_admission_no.ReadOnly = true;
                            btn_save.Text = "Update";
                            btn_cancel.Visible = true;
                            ltUsertop.Text = "Edit Student";
                            HdID.Value = Request.QueryString["stdid"].ToString();
                            ViewState["admno"] = Request.QueryString["admno"].ToString();

                            BindDetails();
                            bind_doc_type();
                            fetch_subject();
                        }
                        else
                        {
                            bind_doc_type();
                            if (ViewState["FirmId"].ToString() == "IPSP-01")
                            {
                                fetch_admission_no_oldF();
                            }
                            else
                            {
                                fetch_admission_no();
                            }


                            //mycode.bind_all_ddl_with_id(ddl_hostel, "select DISTINCT Hostel_name,Hostel_id from (select t1.Hostel_name,t1.Hostel_id,t2.Category_id,t2.Room_id,t2.Bed_id from Hostels_master t1 join HOSTEL_ROOM_BED_MASTER t2 on t1.Hostel_id=t2.Hostel_id where t2.Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id=t2.Room_id and Status='1' and Hostel_id=t2.Hostel_id and Category_id=t2.Category_id)) t order by Hostel_name asc");
                            //try
                            //{
                            //    ddl_hostel.SelectedValue = My.top_one_hostel_id();
                            //}
                            //catch (Exception ex)
                            //{
                            //}
                            //mycode.bind_all_ddl_with_id_no_select(ddl_room_cat, "select Category_name,Category_id from Hostel_room_category_master order by Category_name asc");
                            //fetch_rooms();
                            //fetch_bed_details();
                        }

                    }
                }
            }
            catch (Exception exc)
            {
            }
        }

        private void firm_details()
        {
            DataTable dt = My.dataTable("select * from Firm_Details");
            if (dt.Rows.Count > 0)
            {
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_school_name1.Text = dt.Rows[0]["firm_name"].ToString();
                
            }
        }

        DataTable adm_dt = new DataTable();
        private void page_load()
        {
            ViewState["college_name"] = My.get_college_name();
            adm_dt = My.dataTable("select district,postoffice,policestation from admission_registor where session='" + My.get_session() + "'");
            try
            {
                My.bind_ddl_noselect(ddl_section, "select Section from section_master order by Section_order asc");
            }
            catch
            {
            }


            string stateName = My.get_state_name();
            compLN.bind_ddl_select(ddl_temp_state, "select UPPER(State) as State from dbo.[StateList] order by State asc");
            compLN.bind_ddl_select(ddl_par_state, "select UPPER(State) as State from dbo.[StateList] order by State asc");
            try
            {
                ddl_temp_state.Text = stateName.ToUpper();
            }
            catch (Exception ex)
            {
            }
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

            bind_house();
            bind_session();
            bind_class();
        }
        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select Session,session_id from session_details order by Session asc");
            ddlsession.SelectedValue = My.get_session_id_for_admission();
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select cd.Course_Name,cd.course_id from Add_course_table cd order by cd.Position asc");
            mycode.bind_all_ddl_with_id(ddl_old_class, "Select cd.Course_Name,cd.course_id from Add_course_table cd order by cd.Position asc");
        }
        private void bind_house()
        {
            My mycode = new My();
            mycode.bind_all_ddl_with_id_cap_NA(ddl_house, "select house_name,house_id from house_master");

            SqlDataAdapter ad = new SqlDataAdapter("select *,(select top 1 Color_code from Color_master where Color_id=house_master.Color_id) as Color_code from house_master", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "house_master");
            DataTable dt = ds.Tables[0];

            DataView dv = ds.Tables[0].DefaultView;
            foreach (DataRowView dr in dv)
            {
                foreach (ListItem item in ddl_house.Items)
                {
                    if (dr["house_id"].ToString() == item.Value.ToString())
                    {
                        item.Attributes.Add("style", "background-color:" + dr["Color_code"].ToString() + ";" + "color:white");
                    }
                }
            }
        }

        public static string temp_reg_id()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random();
            int tempo = random.Next(100000, 999999);
            return (tempo).ToString() + date;
        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetch_admission_no();
        }

        private void fetch_admission_no_oldF()
        {
            string admNo = "";
            try
            {
                txt_admission_no.Enabled = true;
                DataTable dt = mycode.FillData("select * from Admission_no_setting where Status=1 and Session_id='" + ddlsession.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    string sl_no = dt.Rows[0]["Admission_no_start_from_update"].ToString();
                    if (sl_no.Length == 1)
                    {
                        sl_no = "00" + sl_no;
                    }
                    else if (sl_no.Length == 2)
                    {
                        sl_no = "0" + sl_no;
                    }
                    admNo = dt.Rows[0]["Prefix_Code"].ToString() + dt.Rows[0]["Session_code"].ToString() + sl_no;
                    check_dup_admission_no_oldF(admNo);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_admission_no()
        {
            try
            {
                DataTable dt = mycode.FillData("select * from Admission_no_setting where Status=1"); // and Session_id='" + ddlsession.SelectedValue + "'
                if (dt.Rows.Count > 0)
                {
                    string sl_no = dt.Rows[0]["Admission_no_start_from_update"].ToString();
                    string session = ddlsession.SelectedItem.Text;
                    string[] stringSeparatorss = new string[] { "-" };
                    string[] arrs = session.Split(stringSeparatorss, StringSplitOptions.None);
                    string fYear = arrs[0];
                    fYear = ddlsession.SelectedItem.Text.Substring(2, 2);

                    string admNo = admNo = sl_no;
                    if (ViewState["FirmId"].ToString() == "DIS-01")
                    {
                        admNo = sl_no + "/" + fYear;
                    }

                    if (ViewState["FirmId"].ToString() == "MCES-002")
                    {
                        if (sl_no.Length == 1)
                        {
                            sl_no = "00" + sl_no;
                        }
                        if (sl_no.Length == 2)
                        {
                            sl_no = "0" + sl_no;
                        }
                        admNo = "MCESK" + sl_no + "/" + fYear;
                    }
                    check_dup_admission_no(admNo);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void check_dup_admission_no(string admNo)
        {
            bool duplicate = true;
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select admissionserialnumber from admission_registor where admissionserialnumber='" + admNo + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    txt_admission_no.Text = admNo;
                    duplicate = false;
                }
                else
                {
                    string admNoss = increase_adm_sl_no();
                    DataTable dt = mycode.FillData("select * from Admission_no_setting where Status=1"); //and Session_id='" + ddlsession.SelectedValue + "'
                    if (dt.Rows.Count > 0)
                    {
                        string sl_no = dt.Rows[0]["Admission_no_start_from_update"].ToString();

                        string session = ddlsession.SelectedItem.Text;
                        string[] stringSeparatorss = new string[] { "-" };
                        string[] arrs = session.Split(stringSeparatorss, StringSplitOptions.None);
                        string fYear = arrs[0];
                        fYear = ddlsession.SelectedItem.Text.Substring(2, 2);

                        admNo = admNo = sl_no;
                        if (ViewState["FirmId"].ToString() == "DIS-01")
                        {
                            admNo = sl_no + "/" + fYear;
                        }
                        if (ViewState["FirmId"].ToString() == "MCES-002")
                        {
                            if (sl_no.Length == 1)
                            {
                                sl_no = "00" + sl_no;
                            }
                            if (sl_no.Length == 2)
                            {
                                sl_no = "0" + sl_no;
                            }
                            admNo = "MCESK" + sl_no + "/" + fYear;
                        }
                    }
                }
            }
        }


        private void check_dup_admission_no_oldF(string admNo)
        {
            bool duplicate = true;
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select admissionserialnumber from admission_registor where admissionserialnumber='" + admNo + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    txt_admission_no.Text = admNo;
                    txt_admission_no.Enabled = false;
                    txt_admission_no.CssClass = "form-control txtbx-style mandatory";
                    duplicate = false;
                }
                else
                {
                    string admNoss = increase_adm_sl_no();
                    DataTable dt = mycode.FillData("select * from Admission_no_setting where Status=1 and Session_id='" + ddlsession.SelectedValue + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string sl_no = dt.Rows[0]["Admission_no_start_from_update"].ToString();
                        if (sl_no.Length == 1)
                        {
                            sl_no = "00" + sl_no;
                        }
                        else if (sl_no.Length == 2)
                        {
                            sl_no = "0" + sl_no;
                        }
                        admNo = dt.Rows[0]["Prefix_Code"].ToString() + dt.Rows[0]["Session_code"].ToString() + sl_no;
                    }
                }
            }
        }

        private string increase_adm_sl_no()
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_no_setting where Status=1", conn);// and Session_id='" + ddlsession.SelectedValue + "'
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dr["Admission_no_start_from_update"] = Convert.ToDouble(dr["Admission_no_start_from_update"]) + 1;
                        result = dr["Admission_no_start_from_update"].ToString();
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception e)
            {
            }
            return result;
        }




        private void BindDetails()
        {
            DataTable dt = mycode.FillData("select * from admission_registor where Id=" + HdID.Value + "");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                if (dt.Rows[0]["Transfer_Status"].ToString().ToUpper() == "NEW")
                {
                    ddl_student_type.Text = "New";
                }
                else
                {
                    ddl_student_type.Text = "Old";
                }
                try
                {
                    ddl_category.SelectedValue = dt.Rows[0]["Category_id"].ToString();
                    ddl_subcategory.SelectedValue = dt.Rows[0]["SubCategory_id"].ToString();
                }
                catch
                {
                }

                txt_admission_date.Text = dt.Rows[0]["dateofadmission"].ToString();
                ddlsession.SelectedValue = dt.Rows[0]["Session_id"].ToString();
                try
                {
                    ddl_house.SelectedValue = dt.Rows[0]["house"].ToString();
                    txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                }
                catch
                {
                }
               
                ddlclass.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                ddlclass.Enabled = false;
                ddlclass.CssClass = "form-select txtbx-style mandatory";
                try
                {
                    ddl_section.Text = dt.Rows[0]["Section"].ToString();
                }
                catch
                {
                }
                txt_rollnumber.Text = dt.Rows[0]["rollnumber"].ToString();

                 
                txt_std_first_name.Text = dt.Rows[0]["Student_Name_First"].ToString();
                txt_std_middle_name.Text = dt.Rows[0]["Student_Middle_Name"].ToString();
                txt_student_last_name.Text = dt.Rows[0]["Student_Name_Last"].ToString();

                txt_dob.Text = dt.Rows[0]["dob"].ToString();
                ddl_gender.Text = dt.Rows[0]["gender"].ToString();
                ddl_cast.Text = dt.Rows[0]["cast"].ToString();

                ddl_nationality.Text = dt.Rows[0]["Student_nationality"].ToString();
                ddl_religion.Text = dt.Rows[0]["religion"].ToString();
                ddl_blood_group.Text = dt.Rows[0]["blood_group"].ToString();
                ddl_student_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();
                ddl_second_language.Text = dt.Rows[0]["Second_Language"].ToString();
                txt_place_of_birth.Text = dt.Rows[0]["place_of_birth"].ToString();
                txt_illness_remark.Text = dt.Rows[0]["illness_remark"].ToString();
                txt_hobbies.Text = dt.Rows[0]["Hobbie_of_student"].ToString();


                txt_adress.Text = dt.Rows[0]["careof"].ToString();
                txt_temp_mobileno.Text = dt.Rows[0]["mobilenumber"].ToString();
                txt_temp_email_id.Text = dt.Rows[0]["Residential_email"].ToString();
                txt_temp_emergency_contact_no.Text = dt.Rows[0]["Residential_emergency_contact_no"].ToString();
                txt_present_po.Text = dt.Rows[0]["postoffice"].ToString();
                txt_temp_ps.Text = dt.Rows[0]["policestation"].ToString();
                txt_present_district.Text = dt.Rows[0]["district"].ToString();
                ddl_temp_state.Text = dt.Rows[0]["state"].ToString();
                txt_pincode.Text = dt.Rows[0]["pin"].ToString();


                txt_pAddress.Text = dt.Rows[0]["careof_permanent"].ToString();
                txt_p_mobile_no.Text = dt.Rows[0]["mob2"].ToString();
                txt_p_email_id.Text = dt.Rows[0]["Corresp_email_id"].ToString();
                txt_p_emergancy_contact_no.Text = dt.Rows[0]["Corresp_emergency_contact_no"].ToString();
                txt_perma_po.Text = dt.Rows[0]["postoffice_permanent"].ToString();
                txt_par_ps.Text = dt.Rows[0]["policestation_permanent"].ToString();
                txt_perma_disctrict.Text = dt.Rows[0]["district_permanent"].ToString();
                ddl_par_state.Text = dt.Rows[0]["state_permanent"].ToString();
                txt_Ppincod.Text = dt.Rows[0]["pincode"].ToString();


                txt_lastschool.Text = dt.Rows[0]["Prev_school_name"].ToString();
                ddl_old_class.SelectedValue = dt.Rows[0]["Old_class_id"].ToString();
                txt_prev_any_achievement.Text = dt.Rows[0]["Prev_any_achievement"].ToString();
                txt_prev_eng_fm.Text = dt.Rows[0]["Prev_eng_FM"].ToString();
                txt_prev_eng_mo.Text = dt.Rows[0]["Prev_eng_OM"].ToString();
                txt_prev_hin_fm.Text = dt.Rows[0]["Prev_hin_FM"].ToString();
                txt_prev_hin_mo.Text = dt.Rows[0]["Prev_hin_OM"].ToString();
                txt_prev_math_fm.Text = dt.Rows[0]["Prev_math_FM"].ToString();
                txt_prev_math_mo.Text = dt.Rows[0]["Prev_math_OM"].ToString();
                txt_prev_sc_fm.Text = dt.Rows[0]["Prev_sc_FM"].ToString();
                txt_prev_sc_mo.Text = dt.Rows[0]["Prev_sc_OM"].ToString();
                txt_prev_sci_fm.Text = dt.Rows[0]["Prev_sci_FM"].ToString();
                txt_prev_sci_mo.Text = dt.Rows[0]["Prev_sci_OM"].ToString();


                //Father Details
                txt_father_name.Text = dt.Rows[0]["fathername"].ToString();
                txt_father_age.Text = dt.Rows[0]["Father_age"].ToString();
                ddl_f_nationality.Text = dt.Rows[0]["f_nationality"].ToString();
                ddl_father_qualification.Text = dt.Rows[0]["fatherqualification"].ToString();
                txt_Father_institution.Text = dt.Rows[0]["Father_institution"].ToString();
                txt_father_organization.Text = dt.Rows[0]["Father_organization"].ToString();
                txt_father_working_for.Text = dt.Rows[0]["Father_working_for"].ToString();
                txt_father_office_address.Text = dt.Rows[0]["Father_office_address"].ToString();
                ddl_occupation.Text = dt.Rows[0]["occuption"].ToString();
                txt_annual_income.Text = dt.Rows[0]["parentincome"].ToString();
                txt_father_mobile.Text = dt.Rows[0]["father_mob"].ToString();
                txt_father_no_of_hours_intrest_child.Text = dt.Rows[0]["Father_hour_intection_of_child"].ToString();

                //Mother Details
                txt_mother_name.Text = dt.Rows[0]["mothername"].ToString();
                txt_mother_age.Text = dt.Rows[0]["Mother_age"].ToString();
                ddl_m_nationality.Text = dt.Rows[0]["m_nationality"].ToString();
                ddl_mother_qualification.Text = dt.Rows[0]["motherqualifiaction"].ToString();
                txt_mother_institution.Text = dt.Rows[0]["Mother_institution"].ToString();
                txt_mother_organization.Text = dt.Rows[0]["Mother_organization"].ToString();
                txt_mother_working_for.Text = dt.Rows[0]["Mother_working_for"].ToString();
                txt_mother_office_address.Text = dt.Rows[0]["Mother_office_address"].ToString();
                ddl_m_occupation.Text = dt.Rows[0]["m_occupation"].ToString();
                txt_mother_annual_income.Text = dt.Rows[0]["Mother_annual_income"].ToString();
                txt_mother_mobile_no.Text = dt.Rows[0]["mother_mob"].ToString();
                txt_mother_no_of_hours_intrest_child.Text = dt.Rows[0]["Mother_hour_intection_of_child"].ToString();
                txt_if_parents_are_devorced.Text = dt.Rows[0]["If_parents_are_devorced"].ToString();

                txt_what_reason_for_choosing.Text = dt.Rows[0]["Reason_for_choosing_school"].ToString();
                txt_how_did_learn_about_school.Text = dt.Rows[0]["Learn_about_school"].ToString();

            }
        }



        private void bind_doc_type()
        {
            DataTable dt = mycode.FillData("select * from Upload_document_type order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
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

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                bool final_requiredfilecheck = true;
                // step 1 chheck 
                if (txt_admission_date.Text == "")
                {
                    txt_admission_date.Focus();
                    Alertme("Please enter admission date.", "warning");
                    return;
                }
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    ddlsession.Focus();
                    Alertme("Please choose admission date.", "warning");
                    return;
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    ddlclass.Focus();
                    Alertme("Please choose class.", "warning");
                    return;
                }
                if (txt_admission_no.Text == "")
                {
                    txt_admission_no.Focus();
                    Alertme("Please enter admission no.", "warning");
                    return;
                }

                if (!check_valid_admission(txt_admission_no.Text))
                {
                    txt_admission_no.Focus();
                    Alertme("Admission no. is already exists.", "warning");
                    return;
                }


                if (txt_std_first_name.Text == "")
                {
                    txt_std_first_name.Focus();
                    Alertme("Please enter student name.", "warning");
                    return;
                }

                if (txt_dob.Text == "")
                {
                    txt_std_first_name.Focus();
                    Alertme("Please enter student date of birth.", "warning");
                    return;
                }
                if (ddl_gender.Text == "Select")
                {
                    ddl_gender.Focus();
                    Alertme("Please select student gender.", "warning");
                    return;
                }
                if (ddl_cast.Text == "Select")
                {
                    ddl_cast.Focus();
                    Alertme("Please select student category.", "warning");
                    return;
                }
                if (txt_adress.Text == "")
                {
                    txt_adress.Focus();
                    Alertme("Please enter residential address.", "warning");
                    return;
                }
                if (txt_adress.Text == "")
                {
                    txt_adress.Focus();
                    Alertme("Please enter residential address.", "warning");
                    return;
                }
                if (txt_temp_mobileno.Text == "")
                {
                    txt_temp_mobileno.Focus();
                    Alertme("Please enter residential mobile no.", "warning");
                    return;
                }



                if (txt_father_name.Text == "")
                {
                    txt_father_name.Focus();
                    Alertme("Please enter father's name.", "warning");
                    return;
                }
                if (txt_father_age.Text == "")
                {
                    txt_father_age.Focus();
                    Alertme("Please enter father's age.", "warning");
                    return;
                }

                if (ddl_father_qualification.Text == "")
                {
                    ddl_father_qualification.Focus();
                    Alertme("Please choose father's qualification.", "warning");
                    return;
                }
                if (ddl_occupation.Text == "")
                {
                    ddl_occupation.Focus();
                    Alertme("Please choose father's designation.", "warning");
                    return;
                }
                if (txt_annual_income.Text == "")
                {
                    txt_annual_income.Focus();
                    Alertme("Please enter father's annual income.", "warning");
                    return;
                }
                if (txt_father_mobile.Text == "")
                {
                    txt_father_mobile.Focus();
                    Alertme("Please enter father's mobile no.", "warning");
                    return;
                }
                if (txt_mother_name.Text == "")
                {
                    txt_mother_name.Focus();
                    Alertme("Please enter mother's name.", "warning");
                    return;
                }
                if (txt_mother_age.Text == "")
                {
                    txt_mother_age.Focus();
                    Alertme("Please enter mother's age.", "warning");
                    return;
                }
                if (ddl_mother_qualification.Text == "")
                {
                    ddl_mother_qualification.Focus();
                    Alertme("Please choose mother's qualification.", "warning");
                    return;
                }
                if (ddl_m_occupation.Text == "")
                {
                    ddl_m_occupation.Focus();
                    Alertme("Please choose mother's designation.", "warning");
                    return;
                }

                string roll_no = "0";
                if (txt_rollnumber.Text == "")
                {
                    txt_rollnumber.Text = "0";
                }
                else
                {
                    roll_no = txt_rollnumber.Text;
                }






                if (btn_save.Text == "Submit")
                {
                    #region check duplicate
                    SqlConnection cons = new SqlConnection(My.conn);
                    string adno = txt_admission_no.Text;
                    while (!check_duplicate(adno, cons))
                    {
                        if (My.Admission_no_auto == "Yes")
                        {
                            payments.auto_serialS("Admission_No", cons);
                            adno = payments.view_admission_no_format("Admission_No", cons);
                        }
                        else
                        {
                            Alertme("Sorry! Duplicate Admission No", "warning");
                            txt_admission_no.Focus();
                            return;
                        }
                    }
                    txt_admission_no.Text = adno;
                    #endregion

                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();

                        register_details(con);
                        dues_update_headwise_transaction.update_student_dues(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, "0", "0", con);
                        payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " registered a new student in session : " + ddlsession.SelectedItem.Text + " Student Name:- " + txt_std_first_name.Text + " Admission No : " + txt_admission_no.Text, con);
                        flag = true;
                        con.Close();
                        scope.Complete();
                    }

                    if (flag == true)
                    {
                        subject_mapping(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text);
                        #region sms and whatsaap
                        // sms & whatsapp
                        try
                        {
                            string mobilesms = txt_father_mobile.Text;
                            string whatsappno = txt_father_mobile.Text;
                            string type = "";
                            //  My mycode = new My();
                            Dictionary<string, object> autosms = mycodeMY.get_auto_message_template("Admission Confirmation");
                            ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                            ViewState["VariableName"] = (String)autosms["VariableName"];
                            ViewState["SMSType"] = (String)autosms["SMSType"];
                            ViewState["Send_From"] = (String)autosms["Send_From"];
                            ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                            ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];

                            ViewState["tny_app_link_android"] = (String)autosms["tny_app_link_android"];
                            ViewState["tny_app_link_ios"] = (String)autosms["tny_app_link_ios"];

                            string app_url = "for android-" + ViewState["tny_app_link_android"];
                            if (ViewState["tny_app_link_ios"].ToString() != "")
                            {
                                app_url = app_url + " IOS- " + ViewState["tny_app_link_ios"].ToString();
                            }


                            //Dear Parents, We confirm enrolment of your child: {0} in class: {1} , Admission No: {2} and Password: {3}.Please go to the Play Store and install our app using the following link: {4}. Regards PURNANK SOFTWARE SOLUTIONS
                            var vrls = ViewState["VariableName"].ToString().Split(',');
                            var lst = new String[vrls.Length];
                            string studentname = txt_std_first_name.Text + " " + txt_student_last_name.Text;
                            if (vrls.Length > 0)
                            {
                                lst[0] = studentname;
                            }
                            if (vrls.Length > 1)
                            {
                                lst[1] = ddlclass.SelectedItem.Text + "-" + ddl_section.SelectedItem.Text;
                            }
                            if (vrls.Length > 2)
                            {
                                lst[2] = txt_admission_no.Text;
                            }
                            if (vrls.Length > 3)
                            {
                                lst[3] = ViewState["pwd"].ToString();
                            }
                            if (vrls.Length > 4)
                            {
                                lst[4] = app_url;
                            }
                            if (vrls.Length > 5)
                            {

                                lst[5] = "";
                            }
                            if (vrls.Length > 6)
                            {

                                lst[6] = "";
                            }
                            if (vrls.Length > 7)
                            {

                                lst[7] = "";
                            }
                            if (vrls.Length > 8)
                            {

                                lst[8] = "";
                            }
                            string message = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                            if (ViewState["SMSType"].ToString() == "Unicode")
                            {
                                type = "unicode";
                            }
                            else
                            {
                                type = "english";
                            }
                            try
                            {
                                if (ViewState["Is_Send_SMS"].ToString().ToUpper() == "TRUE")
                                {
                                    var dt = mycode.FillData("select top 1 * from message_config where Status='running'");
                                    if (dt.Rows.Count == 1)
                                    {
                                        ViewState["api_key"] = dt.Rows[0]["uid"].ToString();
                                        ViewState["Sender_id"] = dt.Rows[0]["sender"].ToString();
                                    }
                                    else
                                    {
                                        ViewState["api_key"] = "0";
                                        ViewState["Sender_id"] = "0";
                                    }

                                    string api_key = ViewState["api_key"].ToString();
                                    string Sender_id = ViewState["Sender_id"].ToString();
                                    string msgtype = type;


                                    string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + message + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobilesms + "&smsContentType=" + type;

                                    HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                                    HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                                    StreamReader sr = new StreamReader(httpres.GetResponseStream());
                                    string results = sr.ReadToEnd();
                                    sr.Close();

                                    My.Insert("Message_Details", new
                                    {
                                        Mobile_No = mobilesms,
                                        Message = message,
                                        Date = mycode.date(),
                                        Idate = mycode.idate(),
                                        Time = mycode.time(),
                                        Result = results,
                                        User_id = ViewState["Userid"].ToString(),
                                        Mesage_Type = msgtype,
                                        Groupcode = "SMS",
                                        Status = "SEND",
                                        Url = url,
                                        Message_to_Type = "Student",
                                        admin_user_id = txt_admission_no.Text,
                                    });
                                }
                                if (ViewState["Is_Send_WhatsApp"].ToString().ToUpper() == "TRUE")
                                {
                                    string sms = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                                    try
                                    {
                                        var dt = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                                        if (dt.Rows.Count == 1)
                                        {
                                            ViewState["whatsapp_mobile_no"] = dt.Rows[0]["SMS_API"].ToString();
                                            ViewState["Whatsapp_api_url"] = dt.Rows[0]["url"].ToString();
                                        }
                                        else
                                        {
                                            ViewState["whatsapp_mobile_no"] = "";
                                            ViewState["Whatsapp_api_url"] = "";
                                        }

                                        if (whatsappno.Length > 9)
                                        {
                                            string message1 = Uri.EscapeDataString(message);
                                            string mobile_no = "91" + whatsappno;
                                            string _url = "";
                                            if (ViewState["Whatsapp_api_url"].ToString().Contains("app.allexpert.in"))
                                            {
                                                //exampe url
                                                //https://app.allexpert.in/send-message?api_key=q0hRX9FA1z5S7JttEIzLtxJU3lvoOv&sender=62888xxxx&number=9162888xxxx&message=Hello World
                                                _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), mobile_no, message1);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                            }
                                            if (ViewState["Whatsapp_api_url"].ToString().Contains("api4ws.com"))
                                            {
                                                // _url = My.Whatsapp_api_url + My.whatsapp_mobile_no + "&message=" + message + "&phone=91" + mobile_no;
                                                //https://api4ws.com/sendMessage.php?AUTH_KEY=918877804016&message=User Defined Whatsapp Message&phone=919812345678
                                                _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message1, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                            }
                                            else
                                            {
                                                //https://fastwsapi.com/sendMessage.php?AUTH_KEY=DEMOEDUNEXTG@123&instance_id=575051&message=User Defined Whatsapp Message&phone=919812345678

                                                _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message1, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                            }

                                            //ServicePointManager.Expect100Continue = true;
                                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                                            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                                            StreamReader sr = new StreamReader(httpres.GetResponseStream());
                                            string results = sr.ReadToEnd();
                                            sr.Close();
                                            My.Insert("Message_Details", new
                                            {
                                                Mobile_No = whatsappno,
                                                Message = message,
                                                Date = mycode.date(),
                                                Idate = mycode.idate(),
                                                Time = mycode.time(),
                                                Result = results,
                                                User_id = ViewState["Userid"].ToString(),
                                                Mesage_Type = type,
                                                Groupcode = "Wahataap",
                                                Status = "SEND",
                                                Url = _url,
                                                Message_to_Type = "Student",
                                                admin_user_id = txt_admission_no.Text,
                                            });
                                        }
                                        //return true;
                                    }
                                    catch (Exception ex)
                                    {
                                        My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                                        //return false;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                this.Alertme(ex.Message, "warning");
                            }
                        }
                        catch
                        {
                        }

                        #endregion

                        try
                        {
                            string student_name = txt_std_first_name.Text;
                            if (txt_std_middle_name.Text != "" && txt_std_middle_name.Text != " " && txt_std_middle_name.Text != "  " && txt_std_middle_name.Text != "   ")
                            {
                                student_name = student_name + " " + txt_std_middle_name.Text;
                            }
                            if (txt_student_last_name.Text != "" && txt_student_last_name.Text != " " && txt_student_last_name.Text != "  " && txt_student_last_name.Text != "   ")
                            {
                                student_name = student_name + " " + txt_student_last_name.Text;
                            } 

                            string father_name = txt_father_name.Text; 
                            My.send_data_Create_ledger_for_student(txt_admission_no.Text, student_name, ddl_gender.Text, txt_dob.Text, txt_adress.Text, "", "", txt_father_mobile.Text, "", father_name, txt_admission_date.Text);
                        }
                        catch (Exception ex)
                        {
                        }


                        try
                        {
                            if (ViewState["IsFromEnq"].ToString() == "1")
                            {
                                My.exeSql("update Enquiry_Details set Status='Admission Done' where Enquiry_Id='" + ViewState["IsFromEnqId"].ToString() + "'");
                                string query1 = "INSERT INTO Enquiry_flowup (Enquiry_Id,Follow_Up_Date,Next_Follow_Up_Date,Response_Remarks,Created_by,Status) values (@Enquiry_Id,@Follow_Up_Date,@Next_Follow_Up_Date,@Response_Remarks,@Created_by,@Status)";
                                SqlCommand cmd1;
                                cmd1 = new SqlCommand(query1);
                                cmd1.Parameters.AddWithValue("@Enquiry_Id", ViewState["IsFromEnqId"].ToString());
                                cmd1.Parameters.AddWithValue("@Follow_Up_Date", My.getdate1());
                                cmd1.Parameters.AddWithValue("@Next_Follow_Up_Date", My.getdate1());
                                cmd1.Parameters.AddWithValue("@Response_Remarks", "Admission Done");
                                cmd1.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                                cmd1.Parameters.AddWithValue("@Status", "Admission Done");
                                if (My.InsertUpdateData(cmd1))
                                {
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }


                        Session["successMsgs"] = "Admission process has been completed successfully";
                        if (ViewState["printslip"].ToString() == "1")
                        {
                            string url = "slip/Admission_Print.aspx?session_Id=" + ddlsession.SelectedValue + "&admission_no=" + txt_admission_no.Text;
                            Response.Redirect(url, false);
                        }
                        else
                        {
                            Response.Redirect("admission.aspx", false);
                        }
                    }
                }
                else
                {
                    ViewState["isSuccessS"] = "0";
                    SqlConnection cons = new SqlConnection(My.conn);
                    if (txt_rollnumber.Text == "" || txt_rollnumber.Text == "0") { }
                    else
                    {
                        string roll_nos = txt_rollnumber.Text;
                        while (!check_roll_no_on_update(roll_nos, HdID.Value, cons))
                        {
                            Alertme("Sorry! Duplicate Roll No.", "warning");
                            txt_rollnumber.Focus();
                            return;
                        }
                    }

                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        update_registration(con);
                        dues_update_headwise_transaction.update_student_dues(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, "0", "0", con);


                        flag = true;
                        con.Close();
                        scope.Complete();
                    }

                    if (flag == true)
                    {
                        subject_mapping(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text);
                        if (ViewState["isSuccessS"].ToString() == "1")
                        {
                            My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " update student in session : " + ddlsession.SelectedItem.Text + " Student Name:- " + txt_std_first_name.Text + " Admission No : " + txt_admission_no.Text);
                            Session["MsgeS"] = "Student details has been updated successfully";
                            if (ViewState["Edtfrom"].ToString() == "edt")
                            {
                                Response.Redirect("edit-student.aspx?admno=" + txt_admission_no.Text + "&sesnid=" + ddlsession.SelectedValue, false);
                            }
                            else
                            {
                                Response.Redirect("student-list.aspx", false);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Submit update");
            }
        }

        private void subject_mapping(string session_id, string class_id, string adm_no)
        {
            My.exeSql("delete from Subject_Mapping_New where Session_id='" + session_id + "' and Admission_no='" + adm_no + "' and Class_id='" + class_id + "'");
            int growcount = rp_subjects.Items.Count;
            for (int ix = 0; ix < growcount; ix++)
            {
                CheckBox chk = (CheckBox)rp_subjects.Items[ix].FindControl("CheckBox1");
                if (chk.Checked == true)
                {
                    Label lbl_subj_id = (Label)rp_subjects.Items[ix].FindControl("lbl_subj_id");
                    if (mycodeMy.IsUserExist("select Admission_no from Subject_Mapping_New where Session_id='" + session_id + "' and Admission_no='" + adm_no + "' and Sub_id=" + lbl_subj_id.Text + " and   Section='" + ddl_section.Text + "' and Class_id='" + class_id + "'"))
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Subject_Mapping_New (Class_id,Section,Admission_no,Sub_id,Session,date,idate,type,Type_id,Session_id,Branch_id,Send_status) values (@Class_id,@Section,@Admission_no,@Sub_id,@Session,@date,@idate,@type,@Type_id,@Session_id,@Branch_id,@Send_status)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Class_id", class_id);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Admission_no", adm_no);
                        cmd.Parameters.AddWithValue("@Sub_id", lbl_subj_id.Text);
                        cmd.Parameters.AddWithValue("@Session", ddlsession.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@date", mycode.date());
                        cmd.Parameters.AddWithValue("@idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@type", "0");
                        cmd.Parameters.AddWithValue("@Type_id", "0");
                        cmd.Parameters.AddWithValue("@Session_id", session_id);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Send_status", "Send");
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
            }
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

        private bool check_duplicate(string adno, SqlConnection con)
        {
            DataTable dt = payments.dataTable(" select admissionserialnumber from admission_registor where admissionserialnumber='" + adno + "' ", con);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void register_details(SqlConnection con)
        {
            if (payments.IsUserExistS("select Id from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "'", con))
            {
                string student_names = txt_std_first_name.Text;
                if (txt_std_middle_name.Text != "" && txt_std_middle_name.Text != " " && txt_std_middle_name.Text != "  " && txt_std_middle_name.Text != "   ")
                {
                    student_names = student_names + " " + txt_std_middle_name.Text;
                }
                if (txt_student_last_name.Text != "" && txt_student_last_name.Text != " " && txt_student_last_name.Text != "  " && txt_student_last_name.Text != "   ")
                {
                    student_names = student_names + " " + txt_student_last_name.Text;
                }

                //ADMISSION DATE
                DateTime adm_date; string admission_date = txt_admission_date.Text; int admission_idate = 0; DateTime dob; string date_of_birth = txt_dob.Text;
                try
                {
                    adm_date = DateTime.ParseExact(txt_admission_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    admission_date = adm_date.ToString("dd/MM/yyyy");
                    admission_idate = My.DateConvertToIdate(admission_date);
                }
                catch (Exception ex)
                {
                    try
                    {
                        adm_date = DateTime.ParseExact(txt_admission_date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        admission_date = adm_date.ToString("dd/MM/yyyy");
                        admission_idate = My.DateConvertToIdate(admission_date);
                    }
                    catch (Exception exx)
                    {
                    }
                }

                //DOB
                try
                {
                    dob = DateTime.ParseExact(txt_dob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    date_of_birth = dob.ToString("dd/MM/yyyy");
                }
                catch (Exception ex)
                {
                    try
                    {
                        dob = DateTime.ParseExact(txt_dob.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        date_of_birth = dob.ToString("dd/MM/yyyy");
                    }
                    catch (Exception exx)
                    {
                    }
                }

                string pwd = "";
                try
                {
                    pwd = My.Convertdate_to_pwd(txt_dob.Text);
                    if (pwd == "0")
                    {
                        pwd = My.create_random_no_otp();
                    }
                }
                catch
                {
                    pwd = My.create_random_no_otp();
                }
                ViewState["pwd"] = pwd;
                SqlCommand cmd;
                string query = "INSERT INTO admission_registor (Transfer_Status,Category_id,SubCategory_id,formserialnumber,UID_No,Index_no,dateofadmission,admission_idate,session,is_applied_dayboarding,day_boarding_with_lunch,class,rollnumber,Section,admissionserialnumber,house,hosteltaken,studentname,dob,place_of_birth,Is_birth_certificate,birth_certificate_number,gender,blood_group,Student_nationality,religion,ration_type,cast,Is_cast_certificate,cast_certificate_no,aadharno,student_mother_tounge,is_illness,illness_remark,Staff_employee_code,RTE,staff_ward,Staff_name,Personal_Identymarks,identifacationmark,Prev_school_name,currentschool,Old_Admission_Date,OLd_Admission_Idate,Prev_board_type,Prev_board,Old_class_id,Prev_percentage,Prev_reason_for_shift,Prev_year,fathername,occuption,fatherqualification,f_nationality,f_marrital_statue,Country_Code_Father,father_mob,Father_whatsapp_country_code,Father_whatsApp_no,email_id,guardianname,parentincome,mothername,m_occupation,motherqualifiaction,m_nationality,m_marrital_statue,Country_Code_Mother,mother_mob,Mother_whatsapp_country_code,Mother_whatsApp_no,mother_email,careof,postoffice,policestation,district,city,state,pin,Present_country,Country_Code_Current_add,mobilenumber,careof_permanent,postoffice_permanent,policestation_permanent,district_permanent,city_permanent,state_permanent,pincode,Permanent_country,Country_Code_Current_Perm_add,mob2,Bank_acount_no,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,payment_status,Hostel_id,Session_id,Class_id,Is_TC_Taken,Student_id,Branch_id,Student_Name_First,Student_Middle_Name,Student_Name_Last,Father_Name_First,Father_Name_Middle,Father_Name_Last,Mother_Name_First,Mother_Name_Middle,Mother_Name_Last,StudentStatus,Pwd,Verification_Istatus,Status,College_School_Name,relation,transportationtaken,Father_aadhar_no,Mother_aadhar_no,Height,Weight,Sibling_name1,Sibling_age1,Sibling_school1,Sibling_class1,Sibling_name2,Sibling_age2,Sibling_school2,Sibling_class2,Created_by,Created_date,Created_time,Created_idate,Student_pen_no,jati,Hobbie_of_student,Prev_class_attended,Prev_pass_fail_status,Father_age,Mother_age,Mother_annual_income,Guardian_relation_with_student,Guardian_occupation,Guardian_qualification,Guardian_contry_code,Guardian_mobile_no,Guardian_aadhar_no,Guardian_annual_income,Guardian_address,Second_Language,Residential_email,Residential_emergency_contact_no,Corresp_email_id,Corresp_emergency_contact_no,Father_institution,Father_organization,Father_working_for,Father_office_address,Father_hour_intection_of_child,Mother_institution,Mother_organization,Mother_working_for,Mother_office_address,Mother_hour_intection_of_child,Prev_any_achievement,Prev_eng_FM,Prev_eng_OM,Prev_hin_FM,Prev_hin_OM,Prev_math_FM,Prev_math_OM,Prev_sc_FM,Prev_sc_OM,Prev_sci_FM,Prev_sci_OM,Reason_for_choosing_school,Learn_about_school,If_parents_are_devorced) values (@Transfer_Status,@Category_id,@SubCategory_id,@formserialnumber,@UID_No,@Index_no,@dateofadmission,@admission_idate,@session,@is_applied_dayboarding,@day_boarding_with_lunch,@class,@rollnumber,@Section,@admissionserialnumber,@house,@hosteltaken,@studentname,@dob,@place_of_birth,@Is_birth_certificate,@birth_certificate_number,@gender,@blood_group,@Student_nationality,@religion,@ration_type,@cast,@Is_cast_certificate,@cast_certificate_no,@aadharno,@student_mother_tounge,@is_illness,@illness_remark,@Staff_employee_code,@RTE,@staff_ward,@Staff_name,@Personal_Identymarks,@identifacationmark,@Prev_school_name,@currentschool,@Old_Admission_Date,@OLd_Admission_Idate,@Prev_board_type,@Prev_board,@Old_class_id,@Prev_percentage,@Prev_reason_for_shift,@Prev_year,@fathername,@occuption,@fatherqualification,@f_nationality,@f_marrital_statue,@Country_Code_Father,@father_mob,@Father_whatsapp_country_code,@Father_whatsApp_no,@email_id,@guardianname,@parentincome,@mothername,@m_occupation,@motherqualifiaction,@m_nationality,@m_marrital_statue,@Country_Code_Mother,@mother_mob,@Mother_whatsapp_country_code,@Mother_whatsApp_no,@mother_email,@careof,@postoffice,@policestation,@district,@city,@state,@pin,@Present_country,@Country_Code_Current_add,@mobilenumber,@careof_permanent,@postoffice_permanent,@policestation_permanent,@district_permanent,@city_permanent,@state_permanent,@pincode,@Permanent_country,@Country_Code_Current_Perm_add,@mob2,@Bank_acount_no,@Account_Holder_name,@Bnk_Name,@IFSC_Code,@Branch_Name,@payment_status,@Hostel_id,@Session_id,@Class_id,@Is_TC_Taken,@Student_id,@Branch_id,@Student_Name_First,@Student_Middle_Name,@Student_Name_Last,@Father_Name_First,@Father_Name_Middle,@Father_Name_Last,@Mother_Name_First,@Mother_Name_Middle,@Mother_Name_Last,@StudentStatus,@Pwd,@Verification_Istatus,@Status,@College_School_Name,@relation,@transportationtaken,@Father_aadhar_no,@Mother_aadhar_no,@Height,@Weight,@Sibling_name1,@Sibling_age1,@Sibling_school1,@Sibling_class1,@Sibling_name2,@Sibling_age2,@Sibling_school2,@Sibling_class2,@Created_by,@Created_date,@Created_time,@Created_idate,@Student_pen_no,@jati,@Hobbie_of_student,@Prev_class_attended,@Prev_pass_fail_status,@Father_age,@Mother_age,@Mother_annual_income,@Guardian_relation_with_student,@Guardian_occupation,@Guardian_qualification,@Guardian_contry_code,@Guardian_mobile_no,@Guardian_aadhar_no,@Guardian_annual_income,@Guardian_address,@Second_Language,@Residential_email,@Residential_emergency_contact_no,@Corresp_email_id,@Corresp_emergency_contact_no,@Father_institution,@Father_organization,@Father_working_for,@Father_office_address,@Father_hour_intection_of_child,@Mother_institution,@Mother_organization,@Mother_working_for,@Mother_office_address,@Mother_hour_intection_of_child,@Prev_any_achievement,@Prev_eng_FM,@Prev_eng_OM,@Prev_hin_FM,@Prev_hin_OM,@Prev_math_FM,@Prev_math_OM,@Prev_sc_FM,@Prev_sc_OM,@Prev_sci_FM,@Prev_sci_OM,@Reason_for_choosing_school,@Learn_about_school,@If_parents_are_devorced)";
                cmd = new SqlCommand(query);
                //Academic Details 
                if (ddl_student_type.Text.ToUpper() == "NEW")
                {
                    cmd.Parameters.AddWithValue("@Transfer_Status", "New");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Transfer_Status", "NT");
                }
                cmd.Parameters.AddWithValue("@Category_id", ddl_category.SelectedValue);
                cmd.Parameters.AddWithValue("@SubCategory_id", ddl_subcategory.SelectedValue);
                cmd.Parameters.AddWithValue("@formserialnumber", "");
                cmd.Parameters.AddWithValue("@UID_No", "");
                cmd.Parameters.AddWithValue("@Index_no", "");
                cmd.Parameters.AddWithValue("@Student_pen_no", "");
                cmd.Parameters.AddWithValue("@dateofadmission", admission_date);
                cmd.Parameters.AddWithValue("@admission_idate", admission_idate);
                cmd.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@hosteltaken", "No");
                cmd.Parameters.AddWithValue("@is_applied_dayboarding", false);
                cmd.Parameters.AddWithValue("@day_boarding_with_lunch", false);
                cmd.Parameters.AddWithValue("@class", ddlclass.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@rollnumber", txt_rollnumber.Text);
                cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                cmd.Parameters.AddWithValue("@admissionserialnumber", txt_admission_no.Text);
                cmd.Parameters.AddWithValue("@house", ddl_house.SelectedValue);

                //Student Info 
                cmd.Parameters.AddWithValue("@studentname", student_names);
                cmd.Parameters.AddWithValue("@dob", date_of_birth);
                cmd.Parameters.AddWithValue("@place_of_birth", txt_place_of_birth.Text);
                cmd.Parameters.AddWithValue("@Is_birth_certificate", "");
                cmd.Parameters.AddWithValue("@birth_certificate_number", "");
                cmd.Parameters.AddWithValue("@gender", ddl_gender.Text);
                cmd.Parameters.AddWithValue("@blood_group", ddl_blood_group.Text);
                cmd.Parameters.AddWithValue("@Student_nationality", ddl_nationality.Text);
                cmd.Parameters.AddWithValue("@religion", ddl_religion.Text);
                cmd.Parameters.AddWithValue("@ration_type", "");
                cmd.Parameters.AddWithValue("@cast", ddl_cast.Text);
                cmd.Parameters.AddWithValue("@jati", "");
                cmd.Parameters.AddWithValue("@Is_cast_certificate", "");
                cmd.Parameters.AddWithValue("@cast_certificate_no", "");
                cmd.Parameters.AddWithValue("@aadharno", "");
                cmd.Parameters.AddWithValue("@student_mother_tounge", ddl_student_mother_tongue.Text);
                cmd.Parameters.AddWithValue("@Second_Language", ddl_second_language.Text);
                cmd.Parameters.AddWithValue("@is_illness", "");
                cmd.Parameters.AddWithValue("@illness_remark", txt_illness_remark.Text);
                cmd.Parameters.AddWithValue("@Staff_employee_code", "");
                cmd.Parameters.AddWithValue("@RTE", "No");
                cmd.Parameters.AddWithValue("@staff_ward", "");
                cmd.Parameters.AddWithValue("@Staff_name", "");
                cmd.Parameters.AddWithValue("@Personal_Identymarks", "");
                cmd.Parameters.AddWithValue("@identifacationmark", "");
                cmd.Parameters.AddWithValue("@Hobbie_of_student", txt_hobbies.Text);


                //Present Address Details
                cmd.Parameters.AddWithValue("@careof", txt_adress.Text);
                cmd.Parameters.AddWithValue("@postoffice", txt_present_po.Text);
                cmd.Parameters.AddWithValue("@policestation", txt_temp_ps.Text);
                cmd.Parameters.AddWithValue("@district", txt_present_district.Text);
                cmd.Parameters.AddWithValue("@city", txt_present_district.Text);
                cmd.Parameters.AddWithValue("@state", ddl_temp_state.Text);
                cmd.Parameters.AddWithValue("@pin", txt_pincode.Text);
                cmd.Parameters.AddWithValue("@Present_country", "");
                cmd.Parameters.AddWithValue("@Country_Code_Current_add", "");
                cmd.Parameters.AddWithValue("@mobilenumber", txt_temp_mobileno.Text);
                cmd.Parameters.AddWithValue("@Residential_email", txt_temp_email_id.Text);
                cmd.Parameters.AddWithValue("@Residential_emergency_contact_no", txt_temp_emergency_contact_no.Text);

                //Permanent Address Details
                cmd.Parameters.AddWithValue("@careof_permanent", txt_pAddress.Text);
                cmd.Parameters.AddWithValue("@postoffice_permanent", txt_perma_po.Text);
                cmd.Parameters.AddWithValue("@policestation_permanent", txt_par_ps.Text);
                cmd.Parameters.AddWithValue("@district_permanent", txt_perma_disctrict.Text);
                cmd.Parameters.AddWithValue("@city_permanent", txt_perma_disctrict.Text);
                cmd.Parameters.AddWithValue("@state_permanent", ddl_par_state.Text);
                cmd.Parameters.AddWithValue("@pincode", txt_Ppincod.Text);
                cmd.Parameters.AddWithValue("@Permanent_country", "");
                cmd.Parameters.AddWithValue("@Country_Code_Current_Perm_add", "");
                cmd.Parameters.AddWithValue("@mob2", txt_p_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Corresp_email_id", txt_p_email_id.Text);
                cmd.Parameters.AddWithValue("@Corresp_emergency_contact_no", txt_p_emergancy_contact_no.Text);

                //Father Details
                cmd.Parameters.AddWithValue("@fathername", txt_father_name.Text);
                cmd.Parameters.AddWithValue("@Father_age", txt_father_age.Text);
                cmd.Parameters.AddWithValue("@f_nationality", ddl_f_nationality.Text);
                cmd.Parameters.AddWithValue("@fatherqualification", ddl_father_qualification.Text);
                cmd.Parameters.AddWithValue("@occuption", ddl_occupation.Text);
                cmd.Parameters.AddWithValue("@f_marrital_statue", "");
                cmd.Parameters.AddWithValue("@Country_Code_Father", "");
                cmd.Parameters.AddWithValue("@father_mob", txt_father_mobile.Text);
                cmd.Parameters.AddWithValue("@Father_whatsapp_country_code", "");
                cmd.Parameters.AddWithValue("@Father_whatsApp_no", txt_father_mobile.Text);
                cmd.Parameters.AddWithValue("@email_id", "");
                cmd.Parameters.AddWithValue("@guardianname", txt_father_name.Text);
                cmd.Parameters.AddWithValue("@parentincome", txt_annual_income.Text);
                cmd.Parameters.AddWithValue("@Father_aadhar_no", "");

                cmd.Parameters.AddWithValue("@Father_institution", txt_Father_institution.Text);
                cmd.Parameters.AddWithValue("@Father_organization", txt_father_organization.Text);
                cmd.Parameters.AddWithValue("@Father_working_for", txt_father_working_for.Text);
                cmd.Parameters.AddWithValue("@Father_office_address", txt_father_office_address.Text);
                cmd.Parameters.AddWithValue("@Father_hour_intection_of_child", txt_father_no_of_hours_intrest_child.Text);

                //Mother Details
                cmd.Parameters.AddWithValue("@mothername", txt_mother_name.Text);
                cmd.Parameters.AddWithValue("@Mother_age", txt_mother_age.Text);
                cmd.Parameters.AddWithValue("@m_occupation", ddl_m_occupation.Text);
                cmd.Parameters.AddWithValue("@motherqualifiaction", ddl_mother_qualification.Text);
                cmd.Parameters.AddWithValue("@m_nationality", ddl_m_nationality.Text);
                cmd.Parameters.AddWithValue("@m_marrital_statue", "");
                cmd.Parameters.AddWithValue("@Country_Code_Mother", "");
                cmd.Parameters.AddWithValue("@mother_mob", txt_mother_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Mother_whatsapp_country_code", "");
                cmd.Parameters.AddWithValue("@Mother_whatsApp_no", txt_mother_mobile_no.Text);
                cmd.Parameters.AddWithValue("@mother_email", "");
                cmd.Parameters.AddWithValue("@Mother_aadhar_no", "");
                cmd.Parameters.AddWithValue("@Mother_annual_income", txt_mother_annual_income.Text);

                cmd.Parameters.AddWithValue("@Mother_institution", txt_mother_institution.Text);
                cmd.Parameters.AddWithValue("@Mother_organization", txt_mother_organization.Text);
                cmd.Parameters.AddWithValue("@Mother_working_for", txt_mother_working_for.Text);
                cmd.Parameters.AddWithValue("@Mother_office_address", txt_mother_office_address.Text);
                cmd.Parameters.AddWithValue("@Mother_hour_intection_of_child", txt_mother_no_of_hours_intrest_child.Text);
                cmd.Parameters.AddWithValue("@If_parents_are_devorced", txt_if_parents_are_devorced.Text);

                //Previous School Details
                cmd.Parameters.AddWithValue("@currentschool", txt_lastschool.Text);
                cmd.Parameters.AddWithValue("@Prev_school_name", txt_lastschool.Text);
                cmd.Parameters.AddWithValue("@Old_class_id", ddl_old_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Old_Admission_Date", "");
                cmd.Parameters.AddWithValue("@OLd_Admission_Idate", "0");

                cmd.Parameters.AddWithValue("@Prev_board_type", "");
                cmd.Parameters.AddWithValue("@Prev_board", "");
                cmd.Parameters.AddWithValue("@Prev_percentage", "");
                cmd.Parameters.AddWithValue("@Prev_reason_for_shift", "");
                cmd.Parameters.AddWithValue("@Prev_year", "");
                cmd.Parameters.AddWithValue("@Prev_class_attended", "");
                cmd.Parameters.AddWithValue("@Prev_pass_fail_status", "");


                cmd.Parameters.AddWithValue("@Prev_any_achievement", txt_prev_any_achievement.Text);
                cmd.Parameters.AddWithValue("@Prev_eng_FM", txt_prev_eng_fm.Text);
                cmd.Parameters.AddWithValue("@Prev_eng_OM", txt_prev_eng_mo.Text);
                cmd.Parameters.AddWithValue("@Prev_hin_FM", txt_prev_hin_fm.Text);
                cmd.Parameters.AddWithValue("@Prev_hin_OM", txt_prev_hin_mo.Text);
                cmd.Parameters.AddWithValue("@Prev_math_FM", txt_prev_math_fm.Text);
                cmd.Parameters.AddWithValue("@Prev_math_OM", txt_prev_math_mo.Text);
                cmd.Parameters.AddWithValue("@Prev_sc_FM", txt_prev_sc_fm.Text);
                cmd.Parameters.AddWithValue("@Prev_sc_OM", txt_prev_sc_mo.Text);
                cmd.Parameters.AddWithValue("@Prev_sci_FM", txt_prev_sci_fm.Text);
                cmd.Parameters.AddWithValue("@Prev_sci_OM", txt_prev_sci_mo.Text);


                //==========================================
                cmd.Parameters.AddWithValue("@Guardian_relation_with_student", "");
                cmd.Parameters.AddWithValue("@Guardian_occupation", "");
                cmd.Parameters.AddWithValue("@Guardian_qualification", "");
                cmd.Parameters.AddWithValue("@Guardian_contry_code", "");
                cmd.Parameters.AddWithValue("@Guardian_mobile_no", "");
                cmd.Parameters.AddWithValue("@Guardian_aadhar_no", "");
                cmd.Parameters.AddWithValue("@Guardian_annual_income", "");
                cmd.Parameters.AddWithValue("@Guardian_address", "");


                cmd.Parameters.AddWithValue("@Height", "");
                cmd.Parameters.AddWithValue("@Weight", "");
                cmd.Parameters.AddWithValue("@Sibling_name1", "");
                cmd.Parameters.AddWithValue("@Sibling_age1", "");
                cmd.Parameters.AddWithValue("@Sibling_school1", "");
                cmd.Parameters.AddWithValue("@Sibling_class1", "");
                cmd.Parameters.AddWithValue("@Sibling_name2", "");
                cmd.Parameters.AddWithValue("@Sibling_age2", "");
                cmd.Parameters.AddWithValue("@Sibling_school2", "");
                cmd.Parameters.AddWithValue("@Sibling_class2", "");

                //Bank Details
                cmd.Parameters.AddWithValue("@Bank_acount_no", "");
                cmd.Parameters.AddWithValue("@Account_Holder_name", "");
                cmd.Parameters.AddWithValue("@Bnk_Name", "");
                cmd.Parameters.AddWithValue("@IFSC_Code", "");
                cmd.Parameters.AddWithValue("@Branch_Name", "");

                cmd.Parameters.AddWithValue("@Reason_for_choosing_school", txt_what_reason_for_choosing.Text);
                cmd.Parameters.AddWithValue("@Learn_about_school", txt_how_did_learn_about_school.Text);
                //=======================
                cmd.Parameters.AddWithValue("@payment_status", "Unpaid");
                cmd.Parameters.AddWithValue("@Hostel_id", "0");
                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_TC_Taken", false);
                cmd.Parameters.AddWithValue("@Student_id", txt_admission_no.Text);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());

                cmd.Parameters.AddWithValue("@Student_Name_First", txt_std_first_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Student_Middle_Name", txt_std_middle_name.Text);
                cmd.Parameters.AddWithValue("@Student_Name_Last", txt_student_last_name.Text.Trim());

                cmd.Parameters.AddWithValue("@Father_Name_First", txt_father_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Father_Name_Middle", "");
                cmd.Parameters.AddWithValue("@Father_Name_Last", "");

                cmd.Parameters.AddWithValue("@Mother_Name_First", txt_mother_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Mother_Name_Middle", "");
                cmd.Parameters.AddWithValue("@Mother_Name_Last", "");

                cmd.Parameters.AddWithValue("@StudentStatus", "AV");
                cmd.Parameters.AddWithValue("@Pwd", pwd);
                cmd.Parameters.AddWithValue("@Verification_Istatus", "0");
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.Parameters.AddWithValue("@College_School_Name", ViewState["college_name"].ToString());
                cmd.Parameters.AddWithValue("@relation", "Not Available");
                cmd.Parameters.AddWithValue("@transportationtaken", "No");

                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (payments.InsertUpdateData(cmd, con))
                {
                    string reg_ids = hd_temp_reg_id.Value;
                    #region subject assined auto 
                    try
                    {
                        payments.student_subject_mapping(ddlsession.SelectedValue, ddlsession.SelectedItem.Text, ddlclass.SelectedValue, txt_admission_no.Text, ddl_section.Text, ViewState["branchid"].ToString(), con);
                    }
                    catch
                    {
                    }
                    #endregion 

                    save_images(reg_ids, con);
                    payments.exeSql("update Student_image_new set Admission_no='" + txt_admission_no.Text + "' where Admission_no='" + reg_ids + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
                }
            }
            else
            {
                Alertme("Admission no. already exist.", "warning");
                txt_admission_no.Focus();
            }
        }
        private void save_images(string reg_ids, SqlConnection con)
        {

            DataTable dt = payments.dataTable("select * from Student_image_new where Admission_no='" + reg_ids + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Image_type"].ToString() == "Student_image")
                    {
                        payments.exeSql("update admission_registor set studentimagepath='" + dt.Rows[i]["Image_path"].ToString() + "' where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
                    }
                    if (dt.Rows[i]["Image_type"].ToString() == "Parent_Sign")
                    {
                        payments.exeSql("update admission_registor set signatureimagepath='" + dt.Rows[i]["Image_path"].ToString() + "' where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
                    }
                    if (dt.Rows[i]["Image_type"].ToString() == "DOB_image")
                    {
                        payments.exeSql("update admission_registor set dobproof='" + dt.Rows[i]["Image_path"].ToString() + "' where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
                    }
                }
            }
        }
        private bool check_roll_no_on_update(string roll, string rowid, SqlConnection con)
        {
            DataTable dt = payments.dataTable(" select admissionserialnumber from admission_registor where rollnumber='" + roll + "' and Class_id='" + ddlclass.SelectedValue + "' and  Section='" + ddl_section.Text + "' and Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Id!=" + rowid + "", con);
            if (dt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        private void update_registration(SqlConnection con)
        {
            //ADMISSION DATE
            DateTime adm_date; string admission_date = txt_admission_date.Text; int admission_idate = 0; DateTime dob; string date_of_birth = txt_dob.Text;
            try
            {
                adm_date = DateTime.ParseExact(txt_admission_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                admission_date = adm_date.ToString("dd/MM/yyyy");
                admission_idate = My.DateConvertToIdate(admission_date);
            }
            catch (Exception ex)
            {
                try
                {
                    adm_date = DateTime.ParseExact(txt_admission_date.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    admission_date = adm_date.ToString("dd/MM/yyyy");
                    admission_idate = My.DateConvertToIdate(admission_date);
                }
                catch (Exception exx)
                {
                }
            }

            //DOB
            try
            {
                dob = DateTime.ParseExact(txt_dob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                date_of_birth = dob.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                try
                {
                    dob = DateTime.ParseExact(txt_dob.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    date_of_birth = dob.ToString("dd/MM/yyyy");
                }
                catch (Exception exx)
                {
                }
            }


            string student_name = txt_std_first_name.Text;
            if (txt_std_middle_name.Text != "" && txt_std_middle_name.Text != " " && txt_std_middle_name.Text != "  " && txt_std_middle_name.Text != "   ")
            {
                student_name = student_name + " " + txt_std_middle_name.Text;
            }
            if (txt_student_last_name.Text != "" && txt_student_last_name.Text != " " && txt_student_last_name.Text != "  " && txt_student_last_name.Text != "   ")
            {
                student_name = student_name + " " + txt_student_last_name.Text;
            }

            SqlCommand cmd;
            string query = "Update admission_registor set Student_pen_no=@Student_pen_no,Transfer_Status=@Transfer_Status,Category_id=@Category_id,SubCategory_id=@SubCategory_id,formserialnumber=@formserialnumber,UID_No=@UID_No,Index_no=@Index_no,dateofadmission=@dateofadmission,admission_idate=@admission_idate,session=@session,class=@class,rollnumber=@rollnumber,Section=@Section,house=@house,studentname=@studentname,dob=@dob,place_of_birth=@place_of_birth,Is_birth_certificate=@Is_birth_certificate,birth_certificate_number=@birth_certificate_number,gender=@gender,blood_group=@blood_group,Student_nationality=@Student_nationality,religion=@religion,ration_type=@ration_type,cast=@cast,Is_cast_certificate=@Is_cast_certificate,cast_certificate_no=@cast_certificate_no,aadharno=@aadharno,student_mother_tounge=@student_mother_tounge,is_illness=@is_illness,illness_remark=@illness_remark,Staff_employee_code=@Staff_employee_code,RTE=@RTE,staff_ward=@staff_ward,Staff_name=@Staff_name,Personal_Identymarks=@Personal_Identymarks,identifacationmark=@identifacationmark,Prev_school_name=@Prev_school_name,currentschool=@currentschool,Old_Admission_Date=@Old_Admission_Date,OLd_Admission_Idate=@OLd_Admission_Idate,Prev_board_type=@Prev_board_type,Prev_board=@Prev_board,Old_class_id=@Old_class_id,Prev_percentage=@Prev_percentage,Prev_reason_for_shift=@Prev_reason_for_shift,Prev_year=@Prev_year,fathername=@fathername,occuption=@occuption,fatherqualification=@fatherqualification,f_nationality=@f_nationality,f_marrital_statue=@f_marrital_statue,Country_Code_Father=@Country_Code_Father,father_mob=@father_mob,Father_whatsapp_country_code=@Father_whatsapp_country_code,Father_whatsApp_no=@Father_whatsApp_no,email_id=@email_id,guardianname=@guardianname,parentincome=@parentincome,mothername=@mothername,m_occupation=@m_occupation,motherqualifiaction=@motherqualifiaction,m_nationality=@m_nationality,m_marrital_statue=@m_marrital_statue,Country_Code_Mother=@Country_Code_Mother,mother_mob=@mother_mob,Mother_whatsapp_country_code=@Mother_whatsapp_country_code,Mother_whatsApp_no=@Mother_whatsApp_no,mother_email=@mother_email,careof=@careof,postoffice=@postoffice,policestation=@policestation,district=@district,city=@city,state=@state,pin=@pin,Present_country=@Present_country,Country_Code_Current_add=@Country_Code_Current_add,mobilenumber=@mobilenumber,careof_permanent=@careof_permanent,postoffice_permanent=@postoffice_permanent,policestation_permanent=@policestation_permanent,district_permanent=@district_permanent,city_permanent=@city_permanent,state_permanent=@state_permanent,pincode=@pincode,Permanent_country=@Permanent_country,Country_Code_Current_Perm_add=@Country_Code_Current_Perm_add,mob2=@mob2,Bank_acount_no=@Bank_acount_no,Account_Holder_name=@Account_Holder_name,Bnk_Name=@Bnk_Name,IFSC_Code=@IFSC_Code,Branch_Name=@Branch_Name,Session_id=@Session_id,Class_id=@Class_id,Student_Name_First=@Student_Name_First,Student_Middle_Name=@Student_Middle_Name,Student_Name_Last=@Student_Name_Last,Father_Name_First=@Father_Name_First,Mother_Name_First=@Mother_Name_First,College_School_Name=@College_School_Name,Father_aadhar_no=@Father_aadhar_no,Mother_aadhar_no=@Mother_aadhar_no,Height=@Height,Weight=@Weight,Sibling_name1=@Sibling_name1,Sibling_age1=@Sibling_age1,Sibling_school1=@Sibling_school1,Sibling_class1=@Sibling_class1,Sibling_name2=@Sibling_name2,Sibling_age2=@Sibling_age2,Sibling_school2=@Sibling_school2,Sibling_class2=@Sibling_class2,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate,jati=@jati,Hobbie_of_student=@Hobbie_of_student,Prev_class_attended=@Prev_class_attended,Prev_pass_fail_status=@Prev_pass_fail_status,Father_age=@Father_age,Mother_age=@Mother_age,Mother_annual_income=@Mother_annual_income,Guardian_relation_with_student=@Guardian_relation_with_student,Guardian_occupation=@Guardian_occupation,Guardian_qualification=@Guardian_qualification,Guardian_contry_code=@Guardian_contry_code,Guardian_mobile_no=@Guardian_mobile_no,Guardian_aadhar_no=@Guardian_aadhar_no,Guardian_annual_income=@Guardian_annual_income,Guardian_address=@Guardian_address,Second_Language=@Second_Language,Residential_email=@Residential_email,Residential_emergency_contact_no=@Residential_emergency_contact_no,Corresp_email_id=@Corresp_email_id,Corresp_emergency_contact_no=@Corresp_emergency_contact_no,Father_institution=@Father_institution,Father_organization=@Father_organization,Father_working_for=@Father_working_for,Father_office_address=@Father_office_address,Father_hour_intection_of_child=@Father_hour_intection_of_child,Mother_institution=@Mother_institution,Mother_organization=@Mother_organization,Mother_working_for=@Mother_working_for,Mother_office_address=@Mother_office_address,Mother_hour_intection_of_child=@Mother_hour_intection_of_child,Prev_any_achievement=@Prev_any_achievement,Prev_eng_FM=@Prev_eng_FM,Prev_eng_OM=@Prev_eng_OM,Prev_hin_FM=@Prev_hin_FM,Prev_hin_OM=@Prev_hin_OM,Prev_math_FM=@Prev_math_FM,Prev_math_OM=@Prev_math_OM,Prev_sc_FM=@Prev_sc_FM,Prev_sc_OM=@Prev_sc_OM,Prev_sci_FM=@Prev_sci_FM,Prev_sci_OM=@Prev_sci_OM,Reason_for_choosing_school=@Reason_for_choosing_school,Learn_about_school=@Learn_about_school,If_parents_are_devorced=@If_parents_are_devorced where Id=@Id";
            cmd = new SqlCommand(query);
            //Academic Details
            cmd.Parameters.AddWithValue("@Id", HdID.Value);
            if (ddl_student_type.Text.ToUpper() == "NEW")
            {
                cmd.Parameters.AddWithValue("@Transfer_Status", "New");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Transfer_Status", "NT");
            }
            cmd.Parameters.AddWithValue("@Category_id", ddl_category.SelectedValue);
            cmd.Parameters.AddWithValue("@SubCategory_id", ddl_subcategory.SelectedValue);

            cmd.Parameters.AddWithValue("@formserialnumber", "");
            cmd.Parameters.AddWithValue("@UID_No", "");
            cmd.Parameters.AddWithValue("@Index_no", "");
            cmd.Parameters.AddWithValue("@Student_pen_no", "");
            cmd.Parameters.AddWithValue("@dateofadmission", admission_date);
            cmd.Parameters.AddWithValue("@admission_idate", admission_idate);
            cmd.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@class", ddlclass.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@rollnumber", txt_rollnumber.Text);
            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
            cmd.Parameters.AddWithValue("@house", ddl_house.SelectedValue);

            //Student Info 
            cmd.Parameters.AddWithValue("@studentname", student_name);
            cmd.Parameters.AddWithValue("@dob", date_of_birth);
            cmd.Parameters.AddWithValue("@place_of_birth", txt_place_of_birth.Text);
            cmd.Parameters.AddWithValue("@Is_birth_certificate", "");
            cmd.Parameters.AddWithValue("@birth_certificate_number", "");
            cmd.Parameters.AddWithValue("@gender", ddl_gender.Text);
            cmd.Parameters.AddWithValue("@blood_group", ddl_blood_group.Text);
            cmd.Parameters.AddWithValue("@Student_nationality", ddl_nationality.Text);
            cmd.Parameters.AddWithValue("@religion", ddl_religion.Text);
            cmd.Parameters.AddWithValue("@ration_type", "");
            cmd.Parameters.AddWithValue("@cast", ddl_cast.Text);
            cmd.Parameters.AddWithValue("@jati", "");
            cmd.Parameters.AddWithValue("@Is_cast_certificate", "");
            cmd.Parameters.AddWithValue("@cast_certificate_no", "");
            cmd.Parameters.AddWithValue("@aadharno", "");
            cmd.Parameters.AddWithValue("@student_mother_tounge", ddl_student_mother_tongue.Text);
            cmd.Parameters.AddWithValue("@Second_Language", ddl_second_language.Text);
            cmd.Parameters.AddWithValue("@is_illness", "");
            cmd.Parameters.AddWithValue("@illness_remark", txt_illness_remark.Text);
            cmd.Parameters.AddWithValue("@Staff_employee_code", "");
            cmd.Parameters.AddWithValue("@RTE", "No");
            cmd.Parameters.AddWithValue("@staff_ward", "");
            cmd.Parameters.AddWithValue("@Staff_name", "");
            cmd.Parameters.AddWithValue("@Personal_Identymarks", "");
            cmd.Parameters.AddWithValue("@identifacationmark", "");
            cmd.Parameters.AddWithValue("@Hobbie_of_student", txt_hobbies.Text);


            //Father Details
            cmd.Parameters.AddWithValue("@fathername", txt_father_name.Text);
            cmd.Parameters.AddWithValue("@Father_age", txt_father_age.Text);
            cmd.Parameters.AddWithValue("@f_nationality", ddl_f_nationality.Text);
            cmd.Parameters.AddWithValue("@fatherqualification", ddl_father_qualification.Text);
            cmd.Parameters.AddWithValue("@occuption", ddl_occupation.Text);
            cmd.Parameters.AddWithValue("@f_marrital_statue", "");
            cmd.Parameters.AddWithValue("@Country_Code_Father", "");
            cmd.Parameters.AddWithValue("@father_mob", txt_father_mobile.Text);
            cmd.Parameters.AddWithValue("@Father_whatsapp_country_code", "");
            cmd.Parameters.AddWithValue("@Father_whatsApp_no", txt_father_mobile.Text);
            cmd.Parameters.AddWithValue("@email_id", "");
            cmd.Parameters.AddWithValue("@guardianname", txt_father_name.Text);
            cmd.Parameters.AddWithValue("@parentincome", txt_annual_income.Text);
            cmd.Parameters.AddWithValue("@Father_aadhar_no", "");
            cmd.Parameters.AddWithValue("@Father_institution", txt_Father_institution.Text);
            cmd.Parameters.AddWithValue("@Father_organization", txt_father_organization.Text);
            cmd.Parameters.AddWithValue("@Father_working_for", txt_father_working_for.Text);
            cmd.Parameters.AddWithValue("@Father_office_address", txt_father_office_address.Text);
            cmd.Parameters.AddWithValue("@Father_hour_intection_of_child", txt_father_no_of_hours_intrest_child.Text);

            //Mother Details
            cmd.Parameters.AddWithValue("@mothername", txt_mother_name.Text);
            cmd.Parameters.AddWithValue("@Mother_age", txt_mother_age.Text);
            cmd.Parameters.AddWithValue("@m_occupation", ddl_m_occupation.Text);
            cmd.Parameters.AddWithValue("@motherqualifiaction", ddl_mother_qualification.Text);
            cmd.Parameters.AddWithValue("@m_nationality", ddl_m_nationality.Text);
            cmd.Parameters.AddWithValue("@m_marrital_statue", "");
            cmd.Parameters.AddWithValue("@Country_Code_Mother", "");
            cmd.Parameters.AddWithValue("@mother_mob", txt_mother_mobile_no.Text);
            cmd.Parameters.AddWithValue("@Mother_whatsapp_country_code", "");
            cmd.Parameters.AddWithValue("@Mother_whatsApp_no", txt_mother_mobile_no.Text);
            cmd.Parameters.AddWithValue("@mother_email", "");
            cmd.Parameters.AddWithValue("@Mother_aadhar_no", "");
            cmd.Parameters.AddWithValue("@Mother_annual_income", txt_mother_annual_income.Text);
            cmd.Parameters.AddWithValue("@Mother_institution", txt_mother_institution.Text);
            cmd.Parameters.AddWithValue("@Mother_organization", txt_mother_organization.Text);
            cmd.Parameters.AddWithValue("@Mother_working_for", txt_mother_working_for.Text);
            cmd.Parameters.AddWithValue("@Mother_office_address", txt_mother_office_address.Text);
            cmd.Parameters.AddWithValue("@Mother_hour_intection_of_child", txt_mother_no_of_hours_intrest_child.Text);
            cmd.Parameters.AddWithValue("@If_parents_are_devorced", txt_if_parents_are_devorced.Text);

            //Present Address Details
            cmd.Parameters.AddWithValue("@careof", txt_adress.Text);
            cmd.Parameters.AddWithValue("@postoffice", txt_present_po.Text);
            cmd.Parameters.AddWithValue("@policestation", txt_temp_ps.Text);
            cmd.Parameters.AddWithValue("@district", txt_present_district.Text);
            cmd.Parameters.AddWithValue("@city", txt_present_district.Text);
            cmd.Parameters.AddWithValue("@state", ddl_temp_state.Text);
            cmd.Parameters.AddWithValue("@pin", txt_pincode.Text);
            cmd.Parameters.AddWithValue("@Present_country", "");
            cmd.Parameters.AddWithValue("@Country_Code_Current_add", "");
            cmd.Parameters.AddWithValue("@mobilenumber", txt_temp_mobileno.Text);
            cmd.Parameters.AddWithValue("@Residential_email", txt_temp_email_id.Text);
            cmd.Parameters.AddWithValue("@Residential_emergency_contact_no", txt_temp_emergency_contact_no.Text);

            //Permanent Address Details
            cmd.Parameters.AddWithValue("@careof_permanent", txt_pAddress.Text);
            cmd.Parameters.AddWithValue("@postoffice_permanent", txt_perma_po.Text);
            cmd.Parameters.AddWithValue("@policestation_permanent", txt_par_ps.Text);
            cmd.Parameters.AddWithValue("@district_permanent", txt_perma_disctrict.Text);
            cmd.Parameters.AddWithValue("@city_permanent", txt_perma_disctrict.Text);
            cmd.Parameters.AddWithValue("@state_permanent", ddl_par_state.Text);
            cmd.Parameters.AddWithValue("@pincode", txt_Ppincod.Text);
            cmd.Parameters.AddWithValue("@Permanent_country", "");
            cmd.Parameters.AddWithValue("@Country_Code_Current_Perm_add", "");
            cmd.Parameters.AddWithValue("@mob2", txt_p_mobile_no.Text);
            cmd.Parameters.AddWithValue("@Corresp_email_id", txt_p_email_id.Text);
            cmd.Parameters.AddWithValue("@Corresp_emergency_contact_no", txt_p_emergancy_contact_no.Text);



            //Previous School Details
            cmd.Parameters.AddWithValue("@currentschool", txt_lastschool.Text);
            cmd.Parameters.AddWithValue("@Prev_school_name", txt_lastschool.Text);
            cmd.Parameters.AddWithValue("@Old_class_id", ddl_old_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Old_Admission_Date", "");
            cmd.Parameters.AddWithValue("@OLd_Admission_Idate", "0");

            cmd.Parameters.AddWithValue("@Prev_board_type", "");
            cmd.Parameters.AddWithValue("@Prev_board", "");
            cmd.Parameters.AddWithValue("@Prev_percentage", "");
            cmd.Parameters.AddWithValue("@Prev_reason_for_shift", "");
            cmd.Parameters.AddWithValue("@Prev_year", "");
            cmd.Parameters.AddWithValue("@Prev_class_attended", "");
            cmd.Parameters.AddWithValue("@Prev_pass_fail_status", "");


            cmd.Parameters.AddWithValue("@Prev_any_achievement", txt_prev_any_achievement.Text);
            cmd.Parameters.AddWithValue("@Prev_eng_FM", txt_prev_eng_fm.Text);
            cmd.Parameters.AddWithValue("@Prev_eng_OM", txt_prev_eng_mo.Text);
            cmd.Parameters.AddWithValue("@Prev_hin_FM", txt_prev_hin_fm.Text);
            cmd.Parameters.AddWithValue("@Prev_hin_OM", txt_prev_hin_mo.Text);
            cmd.Parameters.AddWithValue("@Prev_math_FM", txt_prev_math_fm.Text);
            cmd.Parameters.AddWithValue("@Prev_math_OM", txt_prev_math_mo.Text);
            cmd.Parameters.AddWithValue("@Prev_sc_FM", txt_prev_sc_fm.Text);
            cmd.Parameters.AddWithValue("@Prev_sc_OM", txt_prev_sc_mo.Text);
            cmd.Parameters.AddWithValue("@Prev_sci_FM", txt_prev_sci_fm.Text);
            cmd.Parameters.AddWithValue("@Prev_sci_OM", txt_prev_sci_mo.Text);


            //==========================================
            cmd.Parameters.AddWithValue("@Guardian_relation_with_student", "");
            cmd.Parameters.AddWithValue("@Guardian_occupation", "");
            cmd.Parameters.AddWithValue("@Guardian_qualification", "");
            cmd.Parameters.AddWithValue("@Guardian_contry_code", "");
            cmd.Parameters.AddWithValue("@Guardian_mobile_no", "");
            cmd.Parameters.AddWithValue("@Guardian_aadhar_no", "");
            cmd.Parameters.AddWithValue("@Guardian_annual_income", "");
            cmd.Parameters.AddWithValue("@Guardian_address", "");


            cmd.Parameters.AddWithValue("@Height", "");
            cmd.Parameters.AddWithValue("@Weight", "");
            cmd.Parameters.AddWithValue("@Sibling_name1", "");
            cmd.Parameters.AddWithValue("@Sibling_age1", "");
            cmd.Parameters.AddWithValue("@Sibling_school1", "");
            cmd.Parameters.AddWithValue("@Sibling_class1", "");
            cmd.Parameters.AddWithValue("@Sibling_name2", "");
            cmd.Parameters.AddWithValue("@Sibling_age2", "");
            cmd.Parameters.AddWithValue("@Sibling_school2", "");
            cmd.Parameters.AddWithValue("@Sibling_class2", "");

            //Bank Details
            cmd.Parameters.AddWithValue("@Bank_acount_no", "");
            cmd.Parameters.AddWithValue("@Account_Holder_name", "");
            cmd.Parameters.AddWithValue("@Bnk_Name", "");
            cmd.Parameters.AddWithValue("@IFSC_Code", "");
            cmd.Parameters.AddWithValue("@Branch_Name", "");

            cmd.Parameters.AddWithValue("@Reason_for_choosing_school", txt_what_reason_for_choosing.Text);
            cmd.Parameters.AddWithValue("@Learn_about_school", txt_how_did_learn_about_school.Text);


            //=======================

            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);


            cmd.Parameters.AddWithValue("@Student_Name_First", txt_std_first_name.Text.Trim());
            cmd.Parameters.AddWithValue("@Student_Middle_Name", txt_std_middle_name.Text);
            cmd.Parameters.AddWithValue("@Student_Name_Last", txt_student_last_name.Text.Trim());

            cmd.Parameters.AddWithValue("@Father_Name_First", txt_father_name.Text.Trim());
            //cmd.Parameters.AddWithValue("@Father_Name_Middle", "");
            //cmd.Parameters.AddWithValue("@Father_Name_Last", "");

            cmd.Parameters.AddWithValue("@Mother_Name_First", txt_mother_name.Text.Trim());
            //cmd.Parameters.AddWithValue("@Mother_Name_Middle", "");
            //cmd.Parameters.AddWithValue("@Mother_Name_Last", "");
            cmd.Parameters.AddWithValue("@College_School_Name", ViewState["college_name"].ToString());
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
            cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
            if (payments.InsertUpdateData(cmd, con))
            {
                ViewState["isSuccessS"] = "1";
                update_images(con);
            }
        }


        private void update_images(SqlConnection con)
        {
            DataTable dt = payments.dataTable("select * from Student_image_new where Admission_no='" + ViewState["admno"].ToString() + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Image_type"].ToString() == "Student_image")
                    {
                        payments.exeSql("update admission_registor set studentimagepath='" + dt.Rows[i]["Image_path"].ToString() + "' where admissionserialnumber='" + ViewState["admno"].ToString() + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
                    }
                    //if (dt.Rows[i]["Image_type"].ToString() == "Student_sign")
                    //{
                    //    mycode.executequery("update admission_registor set signatureimagepath='" + dt.Rows[i]["Image_path"].ToString() + "' where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsession.SelectedValue + "'",con);
                    //}
                    if (dt.Rows[i]["Image_type"].ToString() == "Parent_Sign")
                    {
                        payments.exeSql("update admission_registor set signatureimagepath='" + dt.Rows[i]["Image_path"].ToString() + "' where admissionserialnumber='" + ViewState["admno"].ToString() + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
                    }
                    if (dt.Rows[i]["Image_type"].ToString() == "DOB_image")
                    {
                        payments.exeSql("update admission_registor set dobproof='" + dt.Rows[i]["Image_path"].ToString() + "' where admissionserialnumber='" + ViewState["admno"].ToString() + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
                    }
                }
            }
        }

        protected void btn_upload_image_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Name = (Label)row.FindControl("lbl_Name");
                Label lbl_column_name = (Label)row.FindControl("lbl_column_name");
                FileUpload FileUpload1 = (FileUpload)row.FindControl("FileUpload1");
                save_image(FileUpload1, lbl_Name, lbl_column_name);
                bind_doc_type();
            }
            catch (Exception ex)
            {
            }
        }

        private void save_image(FileUpload FileUpload1, Label lbl_Name, Label lbl_column_name)
        {
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 500000)
                {
                    string files_path = upload_imag(FileUpload1, lbl_column_name.Text);
                    if (files_path == "")
                    {
                    }
                    else
                    {
                        string regidS = hd_temp_reg_id.Value;
                        if (ViewState["IsUpdate"].ToString() == "1")
                        {
                            regidS = txt_admission_no.Text;
                        }
                        else if (ViewState["IsTransfer"].ToString() == "1")
                        {
                            regidS = ViewState["regiDs"].ToString();
                        }
                        else if (ViewState["IsTransfer"].ToString() == "2")
                        {
                            regidS = ViewState["regiDs"].ToString();
                        }
                        if (mycodeMy.IsUserExist("select Id from Student_image_new where Admission_no='" + regidS + "' and Image_type='" + lbl_column_name.Text + "' and Session_id='" + ddlsession.SelectedValue + "'"))
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Student_image_new (Admission_no,Image_name,Image_type,Image_path,Session_id) values (@Admission_no,@Image_name,@Image_type,@Image_path,@Session_id)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Admission_no", regidS);
                            cmd.Parameters.AddWithValue("@Image_name", lbl_Name.Text);
                            cmd.Parameters.AddWithValue("@Image_type", lbl_column_name.Text);
                            cmd.Parameters.AddWithValue("@Image_path", files_path);
                            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            SqlCommand cmd;
                            string query = "Update Student_image_new set Image_path=@Image_path where Admission_no='" + regidS + "' and Image_Type='" + lbl_column_name.Text + "' and Session_id='" + ddlsession.SelectedValue + "'";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Image_path", files_path);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                    }
                }
                else
                {
                    Alertme("Please Reduce or compress size of  " + lbl_Name.Text + " max(200kb).", "warning");
                }
            }
            else
            {
                Alertme("Please upload " + lbl_Name.Text, "warning");
            }
        }

        private string upload_imag(FileUpload file, string column_name)
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = date + time;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            Session["WorkingImage"] = file.FileName;
            string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
            Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
            string[] allowedExtension = { ".png", ".PNG", ".jpg", ".JPG", ".JPEG", ".jpeg" };
            for (int i = 0; i < allowedExtension.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtension[i])
                {
                    FileOK = true;
                    break;
                }
            }

            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    file.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception exe)
                {
                    FileSaved = false;
                    Alertme("File has not save.", "warning");
                }
            }
            else
            {

            }
            if (FileSaved)
            {
                // string originalPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

                String originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                string[] New_originalPath1 = originalPath2.Split('?');
                string originalPath1 = New_originalPath1[0].ToString();

                string fileName = Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = originalPath1 + "/Master_Img/Student/" + fileName;
            }
            return dbfilePath;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            if (ViewState["Edtfrom"].ToString() == "edt")
            {
                Response.Redirect("edit-student.aspx?admno=" + txt_admission_no.Text + "&sesnid=" + ddlsession.SelectedValue, false);
            }
            else
            {
                Response.Redirect("student-list.aspx", false);
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlImage Image1 = e.Item.FindControl("stdimages") as HtmlImage;
                Label lbl_column_name = ((Label)e.Item.FindControl("lbl_column_name")) as Label;
                Label lbl_column_name_for_online_reg = ((Label)e.Item.FindControl("lbl_column_name_for_online_reg")) as Label;
                fetch_saved_images(Image1, lbl_column_name);
            }
        }
        private void fetch_saved_images(HtmlImage Image1, Label lbl_column_name)
        {
            string regidS = hd_temp_reg_id.Value;
            if (ViewState["IsTransfer"].ToString() == "1")
            {
                regidS = ViewState["regiDs"].ToString();
            }
            if (ViewState["IsTransfer"].ToString() == "2")
            {
                regidS = ViewState["regiDs"].ToString();
            }
            else if (ViewState["IsUpdate"].ToString() == "1")
            {
                regidS = txt_admission_no.Text;
            }
            DataTable dt = mycode.FillData("select Image_path from Student_image_new where Admission_no='" + regidS + "' and Image_Type='" + lbl_column_name.Text + "' and Session_id='" + ddlsession.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                Image1.Src = dt.Rows[0]["Image_path"].ToString();
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_subject();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_subject()
        {
            subjectsDV.Visible = false;
            DataTable dtc = My.dataTable("select Is_subj_assign from Add_course_table where course_id='" + ddlclass.SelectedValue + "'");
            if (dtc.Rows.Count > 0)
            {
                if (dtc.Rows[0]["Is_subj_assign"].ToString() == "True")
                {
                    DataTable dt = My.dataTable("select * from Subject_Master where course_id='" + ddlclass.SelectedValue + "' order by Subject_position asc");
                    if (dt.Rows.Count > 0)
                    {
                        subjectsDV.Visible = true;
                        rp_subjects.DataSource = dt;
                        rp_subjects.DataBind();
                    }
                    else
                    {
                        subjectsDV.Visible = false;
                        rp_subjects.DataSource = null;
                        rp_subjects.DataBind();
                    }
                }
            }
        }

        protected void rp_subjects_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox CheckBox1 = ((CheckBox)e.Item.FindControl("CheckBox1")) as CheckBox;
                Label lbl_subj_id = ((Label)e.Item.FindControl("lbl_subj_id")) as Label;
                DataTable dt = My.dataTable("select * from Subject_Mapping_New where Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Admission_no='" + txt_admission_no.Text + "' and Session_id='" + ddlsession.SelectedValue + "' and Sub_id='" + lbl_subj_id.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    if (lbl_subj_id.Text == dt.Rows[0]["Sub_id"].ToString())
                    {
                        CheckBox1.Checked = true;
                    }
                }
            }
        }
    }
}