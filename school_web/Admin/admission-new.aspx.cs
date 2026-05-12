using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class admission_new : System.Web.UI.Page
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
                        ViewState["IsUpdate"] = "0";
                        ViewState["IsTransfer"] = "0";

                        ViewState["isImageSaved"] = "0";
                        ViewState["IsTransferFormSale"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        //txt_admission_date_old.Text = mycode.date();

                        ViewState["branchid"] = mycodeMy.get_branch_id(ViewState["Userid"].ToString());
                        hd_temp_reg_id.Value = temp_reg_id();
                        txt_admission_date.Text = mycode.date();
                        comP.bind_ddl_no_select(ddl_nationality, "select Country_name from Country_list");
                        comP.bind_ddl(ddl_cast, "select Category_name from Cast_category");
                        //comP.bind_ddl(ddl_student_mother_tongue, "select Language from Language_Master order by Language asc");

                        comP.bind_ddl_NA(ddl_father_qualification, "select Name from Qualification_master");
                        comP.bind_ddl_NA(ddl_mother_qualification, "select Name from Qualification_master");
                        comP.bind_ddl_NA(ddl_g_qualification, "select Name from Qualification_master");
                        comP.bind_ddl_NA(ddl_caste_jati, "select Caste_name from Caste_jati order by Caste_name asc");

                        compLN.bind_ddl_select(ddl_bank, "select Bank_name from Bank_master order by Bank_name asc");
                        My.bind_ddl_select(ddl_location, "select Location_name from Location_master order by Location_name asc");

                        string sessionid = My.get_session_id_onlinereg();
                        ViewState["sessionid"] = sessionid;
                        ViewState["FirmId"] = My.get_firm_id();
                        if (ViewState["FirmId"].ToString() == "DIS-01")
                        {
                            stdTypeDV.Visible = true;
                        }

                        // ddl_session.SelectedValue = sessionid; 
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
                            boarningTypeDV.Visible = false;
                            TransportDV.Visible = false;
                            BindDetails();
                            bind_doc_type();
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


                            mycode.bind_all_ddl_with_id(ddl_hostel, "select DISTINCT Hostel_name,Hostel_id from (select t1.Hostel_name,t1.Hostel_id,t2.Category_id,t2.Room_id,t2.Bed_id from Hostels_master t1 join HOSTEL_ROOM_BED_MASTER t2 on t1.Hostel_id=t2.Hostel_id where t2.Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id=t2.Room_id and Status='1' and Hostel_id=t2.Hostel_id and Category_id=t2.Category_id)) t order by Hostel_name asc");
                            try
                            {
                                ddl_hostel.SelectedValue = My.top_one_hostel_id();
                            }
                            catch (Exception ex)
                            {
                            }
                            mycode.bind_all_ddl_with_id_no_select(ddl_room_cat, "select Category_name,Category_id from Hostel_room_category_master order by Category_name asc");
                            fetch_rooms();
                            fetch_bed_details();
                        }



                        if (ViewState["FirmId"].ToString() == "MCES-001")
                        {
                            admnoDateDV.Visible = true;
                            extraAdd.Visible = true;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
            }
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

                txt_adm_no_date.Text = dt.Rows[0]["Admission_no_date"].ToString();
                txt_m_annual_income.Text = dt.Rows[0]["Mother_annual_income"].ToString();
                txt_g_annual_income.Text = dt.Rows[0]["Guardian_annual_income"].ToString();

                try
                {
                    ddl_g_occupation.Text = dt.Rows[0]["Guardian_occupation"].ToString();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ddl_g_qualification.Text = dt.Rows[0]["Guardian_qualification"].ToString();
                }
                catch (Exception ex)
                {
                }





                ddl_category.SelectedValue = dt.Rows[0]["Category_id"].ToString();
                ddl_subcategory.SelectedValue = dt.Rows[0]["SubCategory_id"].ToString();
                txt_admission_date.Text = dt.Rows[0]["dateofadmission"].ToString();
                ddlsession.SelectedValue = dt.Rows[0]["Session_id"].ToString();

                ddlclass.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                txt_rollnumber.Text = dt.Rows[0]["rollnumber"].ToString();
                ddl_section.Text = dt.Rows[0]["Section"].ToString();
                txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();

                try
                {
                    ddl_house.SelectedValue = dt.Rows[0]["house"].ToString();
                }
                catch
                {
                }

                //Student Info 
                txt_student_name.Text = dt.Rows[0]["Student_Name_First"].ToString();
                txt_dob.Text = dt.Rows[0]["dob"].ToString();

                try
                {
                    if (dt.Rows[0]["gender"].ToString().ToUpper() == "MALE")
                    {
                        ddl_gender.Text = "MALE";
                    }
                    else if (dt.Rows[0]["gender"].ToString().ToUpper() == "FEMALE")
                    {
                        ddl_gender.Text = "FEMALE";
                    }
                    else
                    {
                        ddl_gender.Text = dt.Rows[0]["gender"].ToString();
                    }
                }
                catch
                {
                }

                ddl_blood_group.Text = dt.Rows[0]["blood_group"].ToString();

                ddl_nationality.Text = dt.Rows[0]["Student_nationality"].ToString();
                txt_aadhar_no.Text = dt.Rows[0]["aadharno"].ToString();

                if (dt.Rows[0]["Student_nationality"].ToString().ToUpper() == "INDIA")
                {
                    ddl_nationality.Text = "INDIAN";
                }

                try
                {
                    ddl_religion.Text = dt.Rows[0]["religion"].ToString().ToUpper();
                }
                catch (Exception ex)
                {
                }
                ddl_ration_cards_types.Text = dt.Rows[0]["ration_type"].ToString();

                try
                {
                    ddl_cast.Text = dt.Rows[0]["cast"].ToString().ToString().ToUpper();
                }
                catch (Exception ex)
                {
                }

                txt_height.Text = dt.Rows[0]["Height"].ToString();
                txt_weight.Text = dt.Rows[0]["Weight"].ToString();



                //Previous School Details
                txt_lastschool.Text = dt.Rows[0]["currentschool"].ToString();
                txt_lastschool.Text = dt.Rows[0]["Prev_school_name"].ToString();
                txt_admission_date_old.Text = dt.Rows[0]["Old_Admission_Date"].ToString();
                ddl_prev_board_type.Text = dt.Rows[0]["Prev_board_type"].ToString();
                bind_board_list();
                ddl_board_list.Text = dt.Rows[0]["Prev_board"].ToString();
                ddl_old_class.SelectedValue = dt.Rows[0]["Old_class_id"].ToString();
                txt_percentage.Text = dt.Rows[0]["Prev_percentage"].ToString();
                txt_reason_for_shift.Text = dt.Rows[0]["Prev_reason_for_shift"].ToString();
                txt_year.Text = dt.Rows[0]["Prev_year"].ToString();



                //Father Details
                txt_father_name.Text = dt.Rows[0]["Father_Name_First"].ToString();

                try
                {
                    ddl_occupation.Text = dt.Rows[0]["occuption"].ToString();
                }
                catch
                {

                }

                try
                {
                    ddl_father_qualification.Text = dt.Rows[0]["fatherqualification"].ToString();
                }
                catch
                {

                }

                ddl_nationality.Text = dt.Rows[0]["f_nationality"].ToString();
                if (dt.Rows[0]["f_nationality"].ToString().ToUpper() == "INDIA")
                {
                    ddl_nationality.Text = "INDIAN";
                }

                txt_father_mobile.Text = dt.Rows[0]["father_mob"].ToString();
                txt_father_whatsapp_no.Text = dt.Rows[0]["Father_whatsApp_no"].ToString();
                txt_guardian_email.Text = dt.Rows[0]["email_id"].ToString();
                txt_guardian_name.Text = dt.Rows[0]["guardianname"].ToString();
                txt_annual_income.Text = dt.Rows[0]["parentincome"].ToString();



                //Mother Details
                txt_mother_name.Text = dt.Rows[0]["Mother_Name_First"].ToString();

                try
                {
                    ddl_m_occupation.Text = dt.Rows[0]["m_occupation"].ToString();
                }
                catch
                {
                }

                try
                {
                    ddl_mother_qualification.Text = dt.Rows[0]["motherqualifiaction"].ToString();
                }
                catch
                {
                }

                //Present Address Details
                txt_adress.Text = dt.Rows[0]["careof"].ToString();
                txt_present_po.Text = dt.Rows[0]["postoffice"].ToString();
                txt_temp_ps.Text = dt.Rows[0]["policestation"].ToString();
                txt_present_district.Text = dt.Rows[0]["district"].ToString();
                txt_city.Text = dt.Rows[0]["city"].ToString();
                txt_pincode.Text = dt.Rows[0]["pin"].ToString();
                ddl_location.Text = dt.Rows[0]["Location"].ToString();
                ///=================== 
                txt_pAddress.Text = dt.Rows[0]["careof_permanent"].ToString();
                txt_perma_po.Text = dt.Rows[0]["postoffice_permanent"].ToString();
                txt_par_ps.Text = dt.Rows[0]["policestation_permanent"].ToString();
                txt_perma_disctrict.Text = dt.Rows[0]["district_permanent"].ToString();
                txt_pcity.Text = dt.Rows[0]["city_permanent"].ToString();
                ddl_p_state.Text = dt.Rows[0]["state_permanent"].ToString();
                txt_Ppincod.Text = dt.Rows[0]["pincode"].ToString();



                //Bank Details
                txt_account_no.Text = dt.Rows[0]["Bank_acount_no"].ToString();
                txt_account_holder_name.Text = dt.Rows[0]["Account_Holder_name"].ToString();
                try
                {
                    ddl_bank.Text = dt.Rows[0]["Bnk_Name"].ToString();
                }
                catch (Exception ex) { }

                txt_ifsc_code.Text = dt.Rows[0]["IFSC_Code"].ToString();
                txt_branch_name.Text = dt.Rows[0]["Branch_Name"].ToString();

                if (dt.Rows[0]["hosteltaken"].ToString().ToUpper() == "YES")
                {
                    HS_roll_updateDV.Visible = true;
                    txt_hstl_roll_no_update.Text = dt.Rows[0]["Hostel_roll_no"].ToString();
                }


                try
                {
                    ddl_student_type_new.Text = dt.Rows[0]["Gyandeep_type"].ToString();
                }
                catch (Exception ex)
                {
                }
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
            compLN.bind_ddl_select(ddl_state, "select UPPER(State) as State from dbo.[StateList] order by State asc");
            compLN.bind_ddl_select(ddl_p_state, "select UPPER(State) as State from dbo.[StateList] order by State asc");
            try
            {
                ddl_state.Text = stateName.ToUpper();
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
            mycode.bind_all_ddl_with_id_bus(ddl_route_path, "select Boarding_Point,Boarding_Point_id from Transportation_Boarding_Point where Session_Id='" + ddlsession.SelectedValue + "' and TransportationPath_id in(select TransportationPath_id from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id=Transportation_Boarding_Point.TransportationPath_id and Transportation_Id=Transportation_Boarding_Point.Transportation_Id and Sheet_Status='0') order by Boarding_Point");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select cd.Course_Name,cd.course_id from Add_course_table cd order by cd.Position asc");
            mycode.bind_all_ddl_with_id(ddl_old_class, "Select cd.Course_Name,cd.course_id from Add_course_table cd  order by cd.Position asc");
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


        protected void ddl_room_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    ddl_hostel.Focus();
                    Alertme("Please select hostel.", "warning");
                    ddl_hostel.Focus();
                }
                else if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    ddl_hostel.Focus();
                    Alertme("Please select category.", "warning");
                    ddl_hostel.Focus();
                }
                else
                {
                    fetch_rooms();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }

        private void fetch_rooms()
        {
            mycode.bind_all_ddl_with_id(ddl_room, "select Room_name,Room_id from Hostel_room_master where Hostel_id='" + ddl_hostel.SelectedValue + "' and Category_id='" + ddl_room_cat.SelectedValue + "' order by Room_name asc");
            string room_id = My.top_one_hostel_room(ddl_hostel.SelectedValue, ddl_room_cat.SelectedValue);
            try
            {
                ddl_room.SelectedValue = room_id; fetch_bed_details();
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_room_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    Alertme("Please select hostel.", "warning");
                    ddl_hostel.Focus();
                }
                else if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    Alertme("Please select category.", "warning");
                    ddl_hostel.Focus();
                }
                else if (ddl_room.SelectedItem.Text == "Select")
                {
                    Alertme("Please select room.", "warning");
                    ddl_room.Focus();
                }
                else
                {
                    fetch_bed_details();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }

        private void fetch_bed_details()
        {
            mycode.bind_all_ddl_with_id(ddl_bed, "select 'Bed No. :'+Bed_name+', Bed Position '+Bed_Position,Bed_id from Hostel_room_bed_master where Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id='" + ddl_room.SelectedValue + "' and Status='1' and Hostel_id=" + ddl_hostel.SelectedValue + " and Category_id=" + ddl_room_cat.SelectedValue + ") and Room_id='" + ddl_room.SelectedValue + "' and Hostel_id='" + ddl_hostel.SelectedValue + "'and Category_id='" + ddl_room_cat.SelectedValue + "' order by Id asc");
            string bed_id = My.get_top_on_bed(ddl_hostel.SelectedValue, ddl_room_cat.SelectedValue, ddl_room.SelectedValue, ddlsession.SelectedValue);
            try
            {
                ddl_bed.SelectedValue = bed_id;
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_hostel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_rooms();
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }

        #region ImageSave
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
                if (ViewState["isImageSaved"].ToString() == "1")
                {
                    Alertme("Image has been saved successfully.", "success");
                }
                txt_doc_focus.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDocUpload();", true);
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
                        ViewState["isImageSaved"] = "1";
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
        #endregion


        protected void ddl_prev_board_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_board_list(); ddl_board_list.Focus();
            }
            catch (Exception ex)
            {
            }
        }
        private void bind_board_list()
        {
            comP.bind_ddl(ddl_board_list, "select Board_name from Board_details where Type='" + ddl_prev_board_type.Text + "'");
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_category.SelectedItem.Text == "Select")
                {
                    Alertme("Please select category.", "warning");
                    ddl_category.Focus();
                    return;
                }
                if (ddl_subcategory.SelectedItem.Text == "Select")
                {
                    Alertme("Please select sub-category.", "warning");
                    ddl_subcategory.Focus();
                    return;
                }
                if (txt_admission_date.Text == "")
                {
                    Alertme("Please enter admission date.", "warning");
                    txt_admission_date.Focus();
                    return;
                }
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (ddl_day_boarding.SelectedValue == "3")
                {
                    if (ddl_hostel.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select hostel.", "warning");
                        ddl_hostel.Focus();
                        return;
                    }
                    if (ddl_room_cat.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select room type.", "warning");
                        ddl_room_cat.Focus();
                        return;
                    }
                    if (ddl_room.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select room no.", "warning");
                        ddl_room.Focus();
                        return;
                    }
                    if (ddl_bed.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select bed no.", "warning");
                        ddl_bed.Focus();
                        return;
                    }
                }


                if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_admission_no.Focus();
                    return;
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                    return;
                }

                bool isvalid = check_valid_admission(txt_admission_no.Text);
                if (isvalid == false)
                {
                    Alertme("Please enter valid admission number", "warning");
                    txt_admission_no.Focus();
                    return;
                }



                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }
                //if (txt_dob.Text == "")
                //{
                //    Alertme("Please enter date of birth.", "warning");
                //    txt_dob.Focus();
                //    return;
                //}


                if (txt_dob.Text != "")
                {
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
                }

                if (ddl_gender.SelectedItem.Text == "Select")
                {
                    Alertme("Please select gender.", "warning");
                    ddl_gender.Focus();
                    return;
                }
                if (ddl_nationality.SelectedItem.Text == "Select")
                {
                    Alertme("Please select nationality.", "warning");
                    ddl_nationality.Focus();
                    return;
                }
                if (ddl_religion.Text == "Select")
                {
                    Alertme("Please select religion.", "warning");
                    ddl_religion.Focus();
                    return;
                }
                if (ddl_cast.Text == "Select")
                {
                    Alertme("Please select caste category.", "warning");
                    ddl_cast.Focus();
                    return;
                }

                //===============================

                if (txt_father_name.Text == "")
                {
                    Alertme("Please enter father name.", "warning");
                    txt_father_name.Focus();
                    return;
                }
                if (ddl_father_qualification.SelectedItem.Text == "Select")
                {
                    Alertme("Please select father qualification.", "warning");
                    ddl_father_qualification.Focus();
                    return;
                }
                if (txt_mother_name.Text == "")
                {
                    Alertme("Please enter mother name.", "warning");
                    txt_mother_name.Focus();
                    return;
                }
                if (txt_father_mobile.Text == "")
                {
                    Alertme("Please enter mobile no.", "warning");
                    txt_father_mobile.Focus();
                    return;
                }
                if (txt_adress.Text == "")
                {
                    Alertme("Please enter address.", "warning");
                    txt_adress.Focus();
                    return;
                }


                if (btn_save.Text.ToUpper() == "SAVE")
                {
                    #region check duplicate
                    SqlConnection cons = new SqlConnection(My.conn);
                    cons.Open();
                    string adno = txt_admission_no.Text;
                    while (!check_duplicate(adno, cons))
                    {
                        Alertme("Sorry! Duplicate Admission No", "warning");
                        txt_admission_no.Focus();
                        return;
                    } 
                    #endregion

                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        save_student_info(con);
                        dues_update_headwise_transaction.update_student_dues(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, "0", "0", con);


                        flag = true;
                        con.Close();
                        scope.Complete();
                    }

                    if (flag == true)
                    {
                        #region sms and whatsaap
                        // sms & whatsapp
                        try
                        {
                            string mobilesms = txt_father_mobile.Text;
                            string whatsappno = txt_father_whatsapp_no.Text;
                            string type = "";
                            //  My mycode = new My();
                            Dictionary<string, object> autosms = mycodeMy.get_auto_message_template("Admission Confirmation");
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
                            string studentname = txt_student_name.Text.Trim();
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

                                lst[3] = ViewState["pwds"].ToString();
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
                            My.send_data_Create_ledger_for_student(txt_admission_no.Text, txt_student_name.Text, ddl_gender.Text, txt_dob.Text, txt_city.Text, txt_present_district.Text, txt_pincode.Text, txt_father_mobile.Text, ddl_state.SelectedItem.Text, txt_father_name.Text, txt_admission_date.Text);
                        }
                        catch (Exception ex)
                        {
                        }


                        string desc = "New student added name : " + txt_student_name.Text + ", session : " + ddlsession.SelectedItem.Text + ", class : " + ddlclass.SelectedItem.Text + " admission no : " + txt_admission_no.Text;
                        log_hostory.edit_log(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, "Adding new student", desc, "admission-new.aspx", ViewState["Userid"].ToString());

                        Session["successMsgs"] = "Admission process has been completed successfully";
                        Response.Redirect("admission-new.aspx", false);
                    }
                    else
                    {
                        Alertme("Something went wrong. Please try again.", "warning");
                    }
                }
                else
                {
                    ViewState["isSuccessS"] = "0";
                    bool flag = false;
                    SqlConnection cons = new SqlConnection(My.conn);
                    cons.Open();
                    if (txt_rollnumber.Text == "" || txt_rollnumber.Text == "0") { }
                    else
                    {
                        string roll_no = txt_rollnumber.Text;
                        while (!check_roll_no_on_update(roll_no, HdID.Value, cons))
                        {
                            Alertme("Sorry! Duplicate Roll No.", "warning");
                            txt_rollnumber.Focus();
                            return;
                        }
                    }

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        update_registration(con);
                        dues_update_headwise_transaction.update_student_dues(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, "0", "0", con);

                        if (ViewState["isSuccessS"].ToString() == "1")
                        {
                            flag = true;
                            con.Close();
                            scope.Complete();
                        }
                    }
                    if (flag == true)
                    {
                        try
                        {
                            string desc = "Update student added name : " + txt_student_name.Text + ", session : " + ddlsession.SelectedItem.Text + ", class : " + ddlclass.SelectedItem.Text + " admission no : " + txt_admission_no.Text;
                            log_hostory.edit_log(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, "Updated student", desc, "admission-new.aspx", ViewState["Userid"].ToString());
                        }
                        catch (Exception ex)
                        {
                        }

                        Session["MsgeS"] = "Student details has been updated successfully";

                        if (ViewState["Edtfrom"].ToString() == "edt")
                        {
                            Response.Redirect("edit-student.aspx", false);
                        }
                        else if (ViewState["Edtfrom"].ToString() == "HSedt")
                        {
                            Response.Redirect("hostel-student-list.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("student-list.aspx", false);
                        }
                    }
                    else
                    {
                        Alertme("Something went wrong. Please try again.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region  UPDATE

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
            SqlCommand cmd;
            string query = "Update admission_registor set Gyandeep_type=@Gyandeep_type,Hostel_roll_no=@Hostel_roll_no,jati=@jati,Location=@Location,Transfer_Status=@Transfer_Status,Category_id=@Category_id,SubCategory_id=@SubCategory_id,dateofadmission=@dateofadmission,admission_idate=@admission_idate,session=@session,class=@class,rollnumber=@rollnumber,Section=@Section,house=@house,studentname=@studentname,dob=@dob,gender=@gender,Student_nationality=@Student_nationality,religion=@religion,ration_type=@ration_type,cast=@cast,aadharno=@aadharno,Height=@Height,Weight=@Weight,currentschool=@currentschool,Prev_school_name=@Prev_school_name,Old_Admission_Date=@Old_Admission_Date,OLd_Admission_Idate=@OLd_Admission_Idate,Prev_board_type=@Prev_board_type,Prev_board=@Prev_board,Old_class_id=@Old_class_id,Prev_percentage=@Prev_percentage,Prev_reason_for_shift=@Prev_reason_for_shift,Prev_year=@Prev_year,fathername=@fathername,occuption=@occuption,fatherqualification=@fatherqualification,f_nationality=@f_nationality,father_mob=@father_mob,Father_whatsApp_no=@Father_whatsApp_no,email_id=@email_id,guardianname=@guardianname,parentincome=@parentincome,mothername=@mothername,m_occupation=@m_occupation,motherqualifiaction=@motherqualifiaction,m_nationality=@m_nationality,careof=@careof,postoffice=@postoffice,policestation=@policestation,district=@district,city=@city,state=@state,pin=@pin,Present_country=@Present_country,mobilenumber=@mobilenumber,Bank_acount_no=@Bank_acount_no,Account_Holder_name=@Account_Holder_name,Bnk_Name=@Bnk_Name,IFSC_Code=@IFSC_Code,Branch_Name=@Branch_Name,Session_id=@Session_id,Class_id=@Class_id,Student_Name_First=@Student_Name_First,Father_Name_First=@Father_Name_First,Mother_Name_First=@Mother_Name_First,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate,Admission_no_date=@Admission_no_date,Mother_annual_income=@Mother_annual_income,Guardian_occupation=@Guardian_occupation,Guardian_qualification=@Guardian_qualification,Guardian_annual_income=@Guardian_annual_income,careof_permanent=@careof_permanent,postoffice_permanent=@postoffice_permanent,policestation_permanent=@policestation_permanent,district_permanent=@district_permanent,city_permanent=@city_permanent,state_permanent=@state_permanent,pincode=@pincode where Id=@Id";
            cmd = new SqlCommand(query);
            //Academic Details  
            cmd.Parameters.AddWithValue("@Gyandeep_type", ddl_student_type_new.Text);
            cmd.Parameters.AddWithValue("@Admission_no_date", txt_adm_no_date.Text);
            cmd.Parameters.AddWithValue("@Mother_annual_income", txt_m_annual_income.Text);
            cmd.Parameters.AddWithValue("@Guardian_occupation", ddl_g_occupation.Text);
            cmd.Parameters.AddWithValue("@Guardian_qualification", ddl_g_qualification.Text);
            cmd.Parameters.AddWithValue("@Guardian_annual_income", txt_g_annual_income.Text);
            cmd.Parameters.AddWithValue("@Id", HdID.Value);
            cmd.Parameters.AddWithValue("@jati", ddl_caste_jati.Text);
            cmd.Parameters.AddWithValue("@Location", ddl_location.Text);
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

            cmd.Parameters.AddWithValue("@dateofadmission", txt_admission_date.Text);
            cmd.Parameters.AddWithValue("@admission_idate", My.DateConvertToIdate(txt_admission_date.Text));
            cmd.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);

            cmd.Parameters.AddWithValue("@class", ddlclass.SelectedItem.Text);
            if (My.section_auto == "Yes")
            {
                ArrayList ar = find_section(ddlclass.Text, ddlsession.Text);
                cmd.Parameters.AddWithValue("@rollnumber", ar[1].ToString());
                cmd.Parameters.AddWithValue("@Section", ar[0].ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@rollnumber", txt_rollnumber.Text);
                cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
            }

            cmd.Parameters.AddWithValue("@house", ddl_house.SelectedValue);


            //Student Info 
            cmd.Parameters.AddWithValue("@studentname", txt_student_name.Text);
            cmd.Parameters.AddWithValue("@dob", txt_dob.Text);
            cmd.Parameters.AddWithValue("@gender", ddl_gender.Text);
            cmd.Parameters.AddWithValue("@blood_group", ddl_blood_group.Text);
            cmd.Parameters.AddWithValue("@Student_nationality", ddl_nationality.Text);
            cmd.Parameters.AddWithValue("@religion", ddl_religion.Text);
            cmd.Parameters.AddWithValue("@ration_type", ddl_ration_cards_types.Text);
            cmd.Parameters.AddWithValue("@cast", ddl_cast.Text);
            cmd.Parameters.AddWithValue("@aadharno", txt_aadhar_no.Text);
            cmd.Parameters.AddWithValue("@Height", txt_height.Text);
            cmd.Parameters.AddWithValue("@Weight", txt_weight.Text);

            //Previous School Details
            cmd.Parameters.AddWithValue("@currentschool", txt_lastschool.Text);
            cmd.Parameters.AddWithValue("@Prev_school_name", txt_lastschool.Text);

            int old_adm_idate = 0;
            try
            {
                old_adm_idate = My.DateConvertToIdate(txt_admission_date_old.Text);
            }
            catch (Exception ex)
            {
                old_adm_idate = 0;
            }

            cmd.Parameters.AddWithValue("@Old_Admission_Date", txt_admission_date_old.Text);
            cmd.Parameters.AddWithValue("@OLd_Admission_Idate", old_adm_idate);
            cmd.Parameters.AddWithValue("@Prev_board_type", ddl_prev_board_type.Text);
            cmd.Parameters.AddWithValue("@Prev_board", ddl_board_list.Text);
            cmd.Parameters.AddWithValue("@Old_class_id", ddl_old_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Prev_percentage", txt_percentage.Text);
            cmd.Parameters.AddWithValue("@Prev_reason_for_shift", txt_reason_for_shift.Text);
            cmd.Parameters.AddWithValue("@Prev_year", txt_year.Text);



            //Father Details
            cmd.Parameters.AddWithValue("@fathername", txt_father_name.Text);
            cmd.Parameters.AddWithValue("@occuption", ddl_occupation.Text);
            cmd.Parameters.AddWithValue("@fatherqualification", ddl_father_qualification.Text);
            cmd.Parameters.AddWithValue("@f_nationality", ddl_nationality.Text);
            cmd.Parameters.AddWithValue("@father_mob", txt_father_mobile.Text);
            cmd.Parameters.AddWithValue("@Father_whatsApp_no", txt_father_whatsapp_no.Text);
            cmd.Parameters.AddWithValue("@email_id", txt_guardian_email.Text);
            cmd.Parameters.AddWithValue("@guardianname", txt_guardian_name.Text);
            cmd.Parameters.AddWithValue("@parentincome", txt_annual_income.Text);



            //Mother Details
            cmd.Parameters.AddWithValue("@mothername", txt_mother_name.Text);
            cmd.Parameters.AddWithValue("@m_occupation", ddl_m_occupation.Text);
            cmd.Parameters.AddWithValue("@motherqualifiaction", ddl_mother_qualification.Text);
            cmd.Parameters.AddWithValue("@m_nationality", ddl_nationality.Text);


            //Present Address Details
            cmd.Parameters.AddWithValue("@careof", txt_adress.Text);
            cmd.Parameters.AddWithValue("@postoffice", txt_present_po.Text);
            cmd.Parameters.AddWithValue("@policestation", txt_temp_ps.Text);
            cmd.Parameters.AddWithValue("@district", txt_present_district.Text);
            cmd.Parameters.AddWithValue("@city", txt_city.Text);
            cmd.Parameters.AddWithValue("@state", ddl_state.Text);
            cmd.Parameters.AddWithValue("@pin", txt_pincode.Text);
            cmd.Parameters.AddWithValue("@Present_country", ddl_nationality.Text);
            cmd.Parameters.AddWithValue("@mobilenumber", txt_father_mobile.Text);

            //========
            cmd.Parameters.AddWithValue("@careof_permanent", txt_pAddress.Text);
            cmd.Parameters.AddWithValue("@postoffice_permanent", txt_perma_po.Text);
            cmd.Parameters.AddWithValue("@policestation_permanent", txt_par_ps.Text);
            cmd.Parameters.AddWithValue("@district_permanent", txt_perma_disctrict.Text);
            cmd.Parameters.AddWithValue("@city_permanent", txt_pcity.Text);
            cmd.Parameters.AddWithValue("@state_permanent", ddl_p_state.Text);
            cmd.Parameters.AddWithValue("@pincode", txt_Ppincod.Text);

            //Bank Details
            cmd.Parameters.AddWithValue("@Bank_acount_no", txt_account_no.Text);
            cmd.Parameters.AddWithValue("@Account_Holder_name", txt_account_holder_name.Text);
            cmd.Parameters.AddWithValue("@Bnk_Name", ddl_bank.Text);
            cmd.Parameters.AddWithValue("@IFSC_Code", txt_ifsc_code.Text);
            cmd.Parameters.AddWithValue("@Branch_Name", txt_branch_name.Text);



            //=======================

            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);
            cmd.Parameters.AddWithValue("@Student_Name_First", txt_student_name.Text.Trim());
            cmd.Parameters.AddWithValue("@Father_Name_First", txt_father_name.Text.Trim());
            cmd.Parameters.AddWithValue("@Mother_Name_First", txt_mother_name.Text.Trim());

            cmd.Parameters.AddWithValue("@Hostel_roll_no", txt_hstl_roll_no_update.Text.Trim());


            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
            cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
            if (payments.InsertUpdateData(cmd, con))
            {
                ViewState["isSuccessS"] = "1";
                update_images(con);

                #region subject assined auto 
                try
                {
                    payments.student_subject_mapping(ddlsession.SelectedValue, ddlsession.SelectedItem.Text, ddlclass.SelectedValue, txt_admission_no.Text, ddl_section.Text, ViewState["branchid"].ToString(), con);
                }
                catch
                {
                }
                #endregion
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
                    //    mycode.executequery("update admission_registor set signatureimagepath='" + dt.Rows[i]["Image_path"].ToString() + "' where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
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
        #endregion

        private void save_student_info(SqlConnection con)
        {
            register_details(con);
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




        private void register_details(SqlConnection con)
        {
            string pwd = My.create_random_no_otp();
            ViewState["pwds"] = pwd;
            if (payments.IsUserExistS("select Id from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "'", con))
            {
                SqlCommand cmd;
                string query = "INSERT INTO admission_registor (Transfer_Status,Category_id,SubCategory_id,formserialnumber,UID_No,Index_no,dateofadmission,admission_idate,session,is_applied_dayboarding,day_boarding_with_lunch,class,rollnumber,Section,admissionserialnumber,house,hosteltaken,studentname,dob,place_of_birth,Is_birth_certificate,birth_certificate_number,gender,blood_group,Student_nationality,religion,ration_type,cast,Is_cast_certificate,cast_certificate_no,aadharno,student_mother_tounge,is_illness,illness_remark,Staff_employee_code,RTE,staff_ward,Staff_name,Personal_Identymarks,identifacationmark,Prev_school_name,currentschool,Old_Admission_Date,OLd_Admission_Idate,Prev_board_type,Prev_board,Old_class_id,Prev_percentage,Prev_reason_for_shift,Prev_year,fathername,occuption,fatherqualification,f_nationality,f_marrital_statue,Country_Code_Father,father_mob,Father_whatsapp_country_code,Father_whatsApp_no,email_id,guardianname,parentincome,mothername,m_occupation,motherqualifiaction,m_nationality,m_marrital_statue,Country_Code_Mother,mother_mob,Mother_whatsapp_country_code,Mother_whatsApp_no,mother_email,careof,postoffice,policestation,district,city,state,pin,Present_country,Country_Code_Current_add,mobilenumber,careof_permanent,postoffice_permanent,policestation_permanent,district_permanent,city_permanent,state_permanent,pincode,Permanent_country,Country_Code_Current_Perm_add,mob2,Bank_acount_no,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,payment_status,Hostel_id,Session_id,Class_id,Is_TC_Taken,Student_id,Branch_id,Student_Name_First,Student_Middle_Name,Student_Name_Last,Father_Name_First,Father_Name_Middle,Father_Name_Last,Mother_Name_First,Mother_Name_Middle,Mother_Name_Last,StudentStatus,Pwd,Verification_Istatus,Status,College_School_Name,relation,transportationtaken,Father_aadhar_no,Mother_aadhar_no,Height,Weight,Sibling_name1,Sibling_age1,Sibling_school1,Sibling_class1,Sibling_name2,Sibling_age2,Sibling_school2,Sibling_class2,Created_by,Created_date,Created_time,Created_idate,Location,jati,Admission_no_date,Mother_annual_income,Guardian_occupation,Guardian_qualification,Guardian_annual_income,Gyandeep_type) values (@Transfer_Status,@Category_id,@SubCategory_id,@formserialnumber,@UID_No,@Index_no,@dateofadmission,@admission_idate,@session,@is_applied_dayboarding,@day_boarding_with_lunch,@class,@rollnumber,@Section,@admissionserialnumber,@house,@hosteltaken,@studentname,@dob,@place_of_birth,@Is_birth_certificate,@birth_certificate_number,@gender,@blood_group,@Student_nationality,@religion,@ration_type,@cast,@Is_cast_certificate,@cast_certificate_no,@aadharno,@student_mother_tounge,@is_illness,@illness_remark,@Staff_employee_code,@RTE,@staff_ward,@Staff_name,@Personal_Identymarks,@identifacationmark,@Prev_school_name,@currentschool,@Old_Admission_Date,@OLd_Admission_Idate,@Prev_board_type,@Prev_board,@Old_class_id,@Prev_percentage,@Prev_reason_for_shift,@Prev_year,@fathername,@occuption,@fatherqualification,@f_nationality,@f_marrital_statue,@Country_Code_Father,@father_mob,@Father_whatsapp_country_code,@Father_whatsApp_no,@email_id,@guardianname,@parentincome,@mothername,@m_occupation,@motherqualifiaction,@m_nationality,@m_marrital_statue,@Country_Code_Mother,@mother_mob,@Mother_whatsapp_country_code,@Mother_whatsApp_no,@mother_email,@careof,@postoffice,@policestation,@district,@city,@state,@pin,@Present_country,@Country_Code_Current_add,@mobilenumber,@careof_permanent,@postoffice_permanent,@policestation_permanent,@district_permanent,@city_permanent,@state_permanent,@pincode,@Permanent_country,@Country_Code_Current_Perm_add,@mob2,@Bank_acount_no,@Account_Holder_name,@Bnk_Name,@IFSC_Code,@Branch_Name,@payment_status,@Hostel_id,@Session_id,@Class_id,@Is_TC_Taken,@Student_id,@Branch_id,@Student_Name_First,@Student_Middle_Name,@Student_Name_Last,@Father_Name_First,@Father_Name_Middle,@Father_Name_Last,@Mother_Name_First,@Mother_Name_Middle,@Mother_Name_Last,@StudentStatus,@Pwd,@Verification_Istatus,@Status,@College_School_Name,@relation,@transportationtaken,@Father_aadhar_no,@Mother_aadhar_no,@Height,@Weight,@Sibling_name1,@Sibling_age1,@Sibling_school1,@Sibling_class1,@Sibling_name2,@Sibling_age2,@Sibling_school2,@Sibling_class2,@Created_by,@Created_date,@Created_time,@Created_idate,@Location,@jati,@Admission_no_date,@Mother_annual_income,@Guardian_occupation,@Guardian_qualification,@Guardian_annual_income,@Gyandeep_type)";
                cmd = new SqlCommand(query);
                //Academic Details 
                cmd.Parameters.AddWithValue("@Gyandeep_type", ddl_student_type_new.Text);
                cmd.Parameters.AddWithValue("@Admission_no_date", txt_adm_no_date.Text);
                cmd.Parameters.AddWithValue("@Mother_annual_income", txt_m_annual_income.Text);
                cmd.Parameters.AddWithValue("@Guardian_occupation", ddl_g_occupation.Text);
                cmd.Parameters.AddWithValue("@Guardian_qualification", ddl_g_qualification.Text);
                cmd.Parameters.AddWithValue("@Guardian_annual_income", txt_g_annual_income.Text);

                cmd.Parameters.AddWithValue("@jati", ddl_caste_jati.Text);
                cmd.Parameters.AddWithValue("@Location", ddl_location.Text);
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
                cmd.Parameters.AddWithValue("@dateofadmission", txt_admission_date.Text);
                cmd.Parameters.AddWithValue("@admission_idate", My.DateConvertToIdate(txt_admission_date.Text));
                cmd.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);
                if (ddl_day_boarding.SelectedValue == "3")
                {
                    cmd.Parameters.AddWithValue("@hosteltaken", "Yes");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@hosteltaken", "No");
                }

                if (ddl_day_boarding.SelectedValue != "0")
                {
                    if (ddl_day_boarding.SelectedValue == "2")
                    {
                        cmd.Parameters.AddWithValue("@is_applied_dayboarding", false);
                        cmd.Parameters.AddWithValue("@day_boarding_with_lunch", true);
                    }
                    else if (ddl_day_boarding.SelectedValue == "3")
                    {
                        cmd.Parameters.AddWithValue("@is_applied_dayboarding", false);
                        cmd.Parameters.AddWithValue("@day_boarding_with_lunch", false);
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@is_applied_dayboarding", false);
                    cmd.Parameters.AddWithValue("@day_boarding_with_lunch", false);
                }
                cmd.Parameters.AddWithValue("@class", ddlclass.SelectedItem.Text);

                if (My.section_auto == "Yes")
                {
                    ArrayList ar = find_section(ddlclass.Text, ddlsession.Text);
                    cmd.Parameters.AddWithValue("@rollnumber", ar[1].ToString());
                    cmd.Parameters.AddWithValue("@Section", ar[0].ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@rollnumber", txt_rollnumber.Text);
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                }
                cmd.Parameters.AddWithValue("@admissionserialnumber", txt_admission_no.Text);
                cmd.Parameters.AddWithValue("@house", ddl_house.SelectedValue);


                //Student Info 
                cmd.Parameters.AddWithValue("@studentname", txt_student_name.Text);
                cmd.Parameters.AddWithValue("@dob", txt_dob.Text);
                cmd.Parameters.AddWithValue("@place_of_birth", "");
                cmd.Parameters.AddWithValue("@Is_birth_certificate", "NO");
                cmd.Parameters.AddWithValue("@birth_certificate_number", "");
                cmd.Parameters.AddWithValue("@gender", ddl_gender.Text);
                cmd.Parameters.AddWithValue("@blood_group", ddl_blood_group.Text);
                cmd.Parameters.AddWithValue("@Student_nationality", ddl_nationality.Text);
                cmd.Parameters.AddWithValue("@religion", ddl_religion.Text);
                cmd.Parameters.AddWithValue("@ration_type", ddl_ration_cards_types.Text);
                cmd.Parameters.AddWithValue("@cast", ddl_cast.Text);
                cmd.Parameters.AddWithValue("@Is_cast_certificate", "NO");
                cmd.Parameters.AddWithValue("@cast_certificate_no", "");
                cmd.Parameters.AddWithValue("@aadharno", txt_aadhar_no.Text);
                cmd.Parameters.AddWithValue("@student_mother_tounge", "Hindi");
                cmd.Parameters.AddWithValue("@is_illness", "NO");
                cmd.Parameters.AddWithValue("@illness_remark", "");
                cmd.Parameters.AddWithValue("@Staff_employee_code", "");
                cmd.Parameters.AddWithValue("@RTE", "NO");
                cmd.Parameters.AddWithValue("@staff_ward", "NO");
                cmd.Parameters.AddWithValue("@Staff_name", "");
                cmd.Parameters.AddWithValue("@Personal_Identymarks", "");
                cmd.Parameters.AddWithValue("@identifacationmark", "");





                cmd.Parameters.AddWithValue("@Height", txt_height.Text);
                cmd.Parameters.AddWithValue("@Weight", txt_weight.Text);
                cmd.Parameters.AddWithValue("@Sibling_name1", "");
                cmd.Parameters.AddWithValue("@Sibling_age1", "");
                cmd.Parameters.AddWithValue("@Sibling_school1", "");
                cmd.Parameters.AddWithValue("@Sibling_class1", "");
                cmd.Parameters.AddWithValue("@Sibling_name2", "");
                cmd.Parameters.AddWithValue("@Sibling_age2", "");
                cmd.Parameters.AddWithValue("@Sibling_school2", "");
                cmd.Parameters.AddWithValue("@Sibling_class2", "");


                //Previous School Details
                cmd.Parameters.AddWithValue("@currentschool", txt_lastschool.Text);
                cmd.Parameters.AddWithValue("@Prev_school_name", txt_lastschool.Text);

                int old_adm_idate = 0;
                try
                {
                    old_adm_idate = My.DateConvertToIdate(txt_admission_date_old.Text);
                }
                catch (Exception ex)
                {
                    old_adm_idate = 0;
                }

                cmd.Parameters.AddWithValue("@Old_Admission_Date", txt_admission_date_old.Text);
                cmd.Parameters.AddWithValue("@OLd_Admission_Idate", old_adm_idate);
                cmd.Parameters.AddWithValue("@Prev_board_type", ddl_prev_board_type.Text);
                cmd.Parameters.AddWithValue("@Prev_board", ddl_board_list.Text);
                cmd.Parameters.AddWithValue("@Old_class_id", ddl_old_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Prev_percentage", txt_percentage.Text);
                cmd.Parameters.AddWithValue("@Prev_reason_for_shift", txt_reason_for_shift.Text);
                cmd.Parameters.AddWithValue("@Prev_year", txt_year.Text);

                //Father Details
                cmd.Parameters.AddWithValue("@fathername", txt_father_name.Text);
                cmd.Parameters.AddWithValue("@occuption", ddl_occupation.Text);
                cmd.Parameters.AddWithValue("@fatherqualification", ddl_father_qualification.Text);
                cmd.Parameters.AddWithValue("@f_nationality", ddl_nationality.Text);
                cmd.Parameters.AddWithValue("@f_marrital_statue", "MARRIED");
                cmd.Parameters.AddWithValue("@Country_Code_Father", "+91");
                cmd.Parameters.AddWithValue("@father_mob", txt_father_mobile.Text);
                cmd.Parameters.AddWithValue("@Father_whatsapp_country_code", "+91");
                cmd.Parameters.AddWithValue("@Father_whatsApp_no", txt_father_whatsapp_no.Text);
                cmd.Parameters.AddWithValue("@email_id", txt_guardian_email.Text);
                cmd.Parameters.AddWithValue("@guardianname", txt_guardian_name.Text);
                cmd.Parameters.AddWithValue("@parentincome", txt_annual_income.Text);
                cmd.Parameters.AddWithValue("@Father_aadhar_no", "");

                //Mother Details
                cmd.Parameters.AddWithValue("@mothername", txt_mother_name.Text);
                cmd.Parameters.AddWithValue("@m_occupation", ddl_m_occupation.Text);
                cmd.Parameters.AddWithValue("@motherqualifiaction", ddl_mother_qualification.Text);
                cmd.Parameters.AddWithValue("@m_nationality", ddl_nationality.Text);
                cmd.Parameters.AddWithValue("@m_marrital_statue", "MARRIED");
                cmd.Parameters.AddWithValue("@Country_Code_Mother", "+91");
                cmd.Parameters.AddWithValue("@mother_mob", "");
                cmd.Parameters.AddWithValue("@Mother_whatsapp_country_code", "+91");
                cmd.Parameters.AddWithValue("@Mother_whatsApp_no", "");
                cmd.Parameters.AddWithValue("@mother_email", "");
                cmd.Parameters.AddWithValue("@Mother_aadhar_no", "");


                //Present Address Details
                cmd.Parameters.AddWithValue("@careof", txt_adress.Text);
                cmd.Parameters.AddWithValue("@postoffice", txt_present_po.Text);
                cmd.Parameters.AddWithValue("@policestation", txt_temp_ps.Text);
                cmd.Parameters.AddWithValue("@district", txt_present_district.Text);
                cmd.Parameters.AddWithValue("@city", txt_city.Text);
                cmd.Parameters.AddWithValue("@state", ddl_state.Text);
                cmd.Parameters.AddWithValue("@pin", txt_pincode.Text);
                cmd.Parameters.AddWithValue("@Present_country", ddl_nationality.Text);
                cmd.Parameters.AddWithValue("@Country_Code_Current_add", "+91");
                cmd.Parameters.AddWithValue("@mobilenumber", txt_father_mobile.Text);


                //Permanent Address Details
                cmd.Parameters.AddWithValue("@careof_permanent", txt_pAddress.Text);
                cmd.Parameters.AddWithValue("@postoffice_permanent", txt_perma_po.Text);
                cmd.Parameters.AddWithValue("@policestation_permanent", txt_par_ps.Text);
                cmd.Parameters.AddWithValue("@district_permanent", txt_perma_disctrict.Text);
                cmd.Parameters.AddWithValue("@city_permanent", txt_pcity.Text);
                cmd.Parameters.AddWithValue("@state_permanent", ddl_p_state.Text);
                cmd.Parameters.AddWithValue("@pincode", txt_Ppincod.Text);
                cmd.Parameters.AddWithValue("@Permanent_country", ddl_nationality.Text);
                cmd.Parameters.AddWithValue("@Country_Code_Current_Perm_add", "+91");
                cmd.Parameters.AddWithValue("@mob2", txt_father_mobile.Text);



                //Bank Details
                cmd.Parameters.AddWithValue("@Bank_acount_no", txt_account_no.Text);
                cmd.Parameters.AddWithValue("@Account_Holder_name", txt_account_holder_name.Text);
                cmd.Parameters.AddWithValue("@Bnk_Name", ddl_bank.Text);
                cmd.Parameters.AddWithValue("@IFSC_Code", txt_ifsc_code.Text);
                cmd.Parameters.AddWithValue("@Branch_Name", txt_branch_name.Text);



                //=======================
                cmd.Parameters.AddWithValue("@payment_status", "Unpaid");
                cmd.Parameters.AddWithValue("@Hostel_id", "0");
                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_TC_Taken", false);
                cmd.Parameters.AddWithValue("@Student_id", txt_admission_no.Text);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());

                cmd.Parameters.AddWithValue("@Student_Name_First", txt_student_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Student_Middle_Name", "");
                cmd.Parameters.AddWithValue("@Student_Name_Last", "");

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
                    if (ddl_day_boarding.SelectedValue == "3")
                    {
                        save_hostel_data(con);
                    }
                    if (ddl_route_path.SelectedValue != "0")
                    {
                        save_transport_mapping(con);
                    }

                    save_images(reg_ids, con);
                    payments.exeSql("update Student_image_new set Admission_no='" + txt_admission_no.Text + "' where Admission_no='" + reg_ids + "' and Session_id='" + ddlsession.SelectedValue + "'", con);
                    #region subject assined auto 
                    try
                    {
                        payments.student_subject_mapping(ddlsession.SelectedValue, ddlsession.SelectedItem.Text, ddlclass.SelectedValue, txt_admission_no.Text, ddl_section.Text, ViewState["branchid"].ToString(), con);
                    }
                    catch
                    {
                    }
                    #endregion
                }
            }
            else
            {
                Alertme("Admission no. already exist.", "warning");
                txt_admission_no.Focus();
            }
        }



        private void save_hostel_data(SqlConnection con)
        {
            string hostel_assign_id = create_sl_no(con);
            string session_frst_year = ddlsession.SelectedItem.Text.Substring(0, 4);
            SqlCommand cmd;
            string query = "INSERT INTO Hostel_assign_master (Session_id,Class_id,Admission_no,From_month_name,From_month_id,Hostel_id,Category_id,Room_id,Bed_id,Hostel_assign_id,Created_by,Created_date,Created_idate,Status,Assined_Year_Month) values (@Session_id,@Class_id,@Admission_no,@From_month_name,@From_month_id,@Hostel_id,@Category_id,@Room_id,@Bed_id,@Hostel_assign_id,@Created_by,@Created_date,@Created_idate,@Status,@Assined_Year_Month)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);

            cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
            cmd.Parameters.AddWithValue("@From_month_name", "April");
            cmd.Parameters.AddWithValue("@From_month_id", "04");

            cmd.Parameters.AddWithValue("@Hostel_id", ddl_hostel.SelectedValue);
            cmd.Parameters.AddWithValue("@Category_id", ddl_room_cat.SelectedValue);
            cmd.Parameters.AddWithValue("@Room_id", ddl_room.SelectedValue);
            cmd.Parameters.AddWithValue("@Bed_id", ddl_bed.SelectedValue);
            cmd.Parameters.AddWithValue("@Hostel_assign_id", hostel_assign_id);
            cmd.Parameters.AddWithValue("@Status", 1);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Assined_Year_Month", session_frst_year + "04");
            if (payments.InsertUpdateData(cmd, con))
            {
                try
                {
                    payments.exeSql("update admission_registor set Hostel_roll_no='" + txt_hostel_roll_no.Text + "',hosteltaken='Yes',transportationtaken='No',Hostel_id='" + ddl_hostel.SelectedValue + "',Hostel_assignD_id='" + hostel_assign_id + "' where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsession.SelectedValue + "' ", con);
                }
                catch
                {
                }
            }
        }

        private string create_sl_no(SqlConnection con)
        {
            bool duplicate = true;
            string hostel_assign_id = payments.auto_serialS("Hostel_assign_id", con);
            while (duplicate)
            {
                DataTable cdt = payments.dataTable("select Hostel_assign_id from Hostel_assign_master where Hostel_assign_id='" + hostel_assign_id + "'", con);
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    hostel_assign_id = payments.auto_serialS("Hostel_assign_id", con);
                }
            }
            return hostel_assign_id;
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
                    //if (dt.Rows[i]["Image_type"].ToString() == "Student_sign")
                    //{
                    //    mycode.executequery("update admission_registor set signatureimagepath='" + dt.Rows[i]["Image_path"].ToString() + "' where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsession.SelectedValue + "'");
                    //}
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


        private void save_transport_mapping(SqlConnection con)
        {
            string session = ddlsession.SelectedItem.Text;
            string tranportassinedid = My.get_transport_assigned_id();
            string cunrt_session = session;
            string session_frst_year = cunrt_session.Substring(0, 4);
            int s_year = My.toint(session_frst_year);
            string monthid = My.tomonth_numberstring("April");
            int pay_month = My.toint(monthid);
            string final = s_year.ToString() + monthid;
            DataTable dtTT = payments.dataTable("select * from Transportation_Boarding_Point where Session_Id='" + ddlsession.SelectedValue + "' and Boarding_Point_id='" + ddl_route_path.SelectedValue + "'", con);


            DataTable dt = payments.dataTable("select * from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id='" + dtTT.Rows[0]["TransportationPath_id"].ToString() + "' and Transportation_Id='" + dtTT.Rows[0]["Transportation_Id"].ToString() + "' and Sheet_Status='0'", con);
            if (dt.Rows.Count > 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Student_mapping_with_TransportPath (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,transport_id,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Academic_Sem_or_Year_id,Mapping_Year,Sheet_Id,Boarding_Point_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@transport_id,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Academic_Sem_or_Year_id,@Mapping_Year,@Sheet_Id,@Boarding_Point_id); INSERT INTO Student_mapping_with_TransportPath_history (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,transport_id,Remark,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Update_Status,Academic_Sem_or_Year_id,Mapping_Year,Sheet_Id,Boarding_Point_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@transport_id,@Remark,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Update_Status,@Academic_Sem_or_Year_id,@Mapping_Year,@Sheet_Id,@Boarding_Point_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);
                cmd.Parameters.AddWithValue("@Month_name", "April");
                cmd.Parameters.AddWithValue("@Month_id", monthid);
                cmd.Parameters.AddWithValue("@TransportPath_id", dtTT.Rows[0]["TransportationPath_id"].ToString());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@branch_id", ViewState["branchid"].ToString());
                cmd.Parameters.AddWithValue("@Year_month", final);
                cmd.Parameters.AddWithValue("@Transport_Assigned_Id", tranportassinedid);
                cmd.Parameters.AddWithValue("@Academic_Sem_or_Year_id", "0");
                cmd.Parameters.AddWithValue("@Mapping_Year", s_year);
                cmd.Parameters.AddWithValue("@Sheet_Id", dt.Rows[0]["Sheet_Id"].ToString());
                cmd.Parameters.AddWithValue("@Remark", "Transport assigned");
                cmd.Parameters.AddWithValue("@Update_Status", "Assigned");
                cmd.Parameters.AddWithValue("@transport_id", dtTT.Rows[0]["Transportation_Id"].ToString());
                cmd.Parameters.AddWithValue("@Boarding_Point_id", ddl_route_path.SelectedValue);
                if (payments.InsertUpdateData(cmd, con))
                {
                    try
                    {
                        SqlCommand cmd1;
                        string query1 = "Update admission_registor set Hostel_id=@Hostel_id,Transportation_Id=@Transportation_Id,Transportationpath=@Transportationpath,transportationtaken=@transportationtaken,hosteltaken=@hosteltaken where admissionserialnumber=@admissionserialnumber and Session_id=@Session_id and Class_id=" + ddlclass.SelectedValue + " ";
                        cmd1 = new SqlCommand(query1);
                        cmd1.Parameters.AddWithValue("@Transportation_Id", dtTT.Rows[0]["Transportation_Id"].ToString());
                        cmd1.Parameters.AddWithValue("@Transportationpath", ddl_route_path.SelectedValue);
                        cmd1.Parameters.AddWithValue("@transportationtaken", "Yes");
                        cmd1.Parameters.AddWithValue("@hosteltaken", "No");
                        cmd1.Parameters.AddWithValue("@Hostel_id", "0");
                        cmd1.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                        cmd1.Parameters.AddWithValue("@admissionserialnumber", txt_admission_no.Text);
                        if (payments.InsertUpdateData(cmd1, con))
                        {
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            if (ViewState["Edtfrom"].ToString() == "edt")
            {
                Response.Redirect("edit-student.aspx", false);
            }
            else
            {
                Response.Redirect("student-list.aspx", false);
            }
        }



        #region AutoComplete
        [WebMethod]
        public static List<string> GetAddress(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct careof from admission_registor where careof LIKE '%'+@SearchMobNo+'%' and Status='1'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["careof"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]
        public static List<string> PoliceStation(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct postoffice from admission_registor where postoffice LIKE '%'+@SearchMobNo+'%' and Status='1'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["postoffice"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }


        [WebMethod]
        public static List<string> getdistrict(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct district from admission_registor where district LIKE '%'+@SearchMobNo+'%' and Status='1'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["district"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]
        public static List<string> getcitY(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct city from admission_registor where city LIKE '%'+@SearchMobNo+'%' and Status='1'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["city"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion
    }
}