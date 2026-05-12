using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
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
    public partial class Admission_form_BD : System.Web.UI.Page
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

                        ViewState["IsTransferFormSale"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        //txt_admission_date_old.Text = mycode.date();
                        get_firm_details();
                        ViewState["branchid"] = mycodeMy.get_branch_id(ViewState["Userid"].ToString());
                        hd_temp_reg_id.Value = temp_reg_id();
                        txt_admission_date.Text = mycode.date();
                        comP.bind_ddl_no_select(ddl_nationality, "select Country_name from Country_list");
                        comP.bind_ddl(ddl_cast_category, "select Category_name from Cast_category");
                        comP.bind_ddl(ddl_student_mother_tongue, "select Language from Language_Master order by Language asc");

                        comP.bind_ddl_NA(ddl_guardian_qualification, "select Name from Qualification_master");
                        comP.bind_ddl_NA(ddl_father_qualification, "select Name from Qualification_master");
                        comP.bind_ddl_no_select(ddl_father_nationality, "select Country_name from Country_list");
                        comP.bind_ddl_NA(ddl_mother_qualification, "select Name from Qualification_master");
                        comP.bind_ddl_no_select(ddl_mother_nationality, "select Country_name from Country_list");
                        compLN.bind_ddl_select(ddl_bank, "select Bank_name from Bank_master order by Bank_name asc");
                        comP.bind_ddl_NA(ddl_caste_jati, "select Caste_name from Caste_jati order by Caste_name asc");

                        string sessionid = My.get_session_id_onlinereg();
                        ViewState["sessionid"] = sessionid;
                        // ddl_session.SelectedValue = sessionid;
                        UsesCode.bind_ddl_noselect(ddl_cunterycode1, "select Country_code from Country_list order by Country_name asc");
                        UsesCode.bind_ddl_noselect(ddl_cunterycode2, "select Country_code from Country_list order by Country_name asc");
                        UsesCode.bind_ddl_noselect(ddl_fthr_whatsapp_c_Code, "select Country_code from Country_list order by Country_name asc");
                        UsesCode.bind_ddl_noselect(ddl_mthr_whatsapp_c_Code, "select Country_code from Country_list order by Country_name asc");
                        UsesCode.bind_ddl_noselect(ddl_guardian_contry_code, "select Country_code from Country_list order by Country_name asc");
                        ddl_cunterycode1.SelectedValue = "+91";
                        ddl_cunterycode2.SelectedValue = "+91";
                        ddl_fthr_whatsapp_c_Code.SelectedValue = "+91";
                        ddl_mthr_whatsapp_c_Code.SelectedValue = "+91";
                        ddl_guardian_contry_code.SelectedValue = "+91";
                        process_active("1");
                        page_load();
                        fetch_transport_info();
                        fil_Capctha_text();
                        btn_cancel.Visible = false;

                        txt_admission_no.ReadOnly = false;
                        ViewState["Edtfrom"] = "0";
                        if (Request.QueryString["stdid"] != null)
                        {
                            ViewState["Edtfrom"] = "0";
                            try
                            {
                                ViewState["Edtfrom"] = Request.QueryString["from"].ToString();
                            }
                            catch (Exception ex)
                            { }
                            DataTable dtPage = My.dataTable("select * from MenuMaster_web where Menu_page='admission-new.aspx' and Type='1'");
                            if (dtPage.Rows.Count > 0)
                            {
                                Response.Redirect("admission-new.aspx?stdid=" + Request.QueryString["stdid"].ToString() + "&admno=" + Request.QueryString["admno"].ToString() + "&from=" + ViewState["Edtfrom"].ToString(), false);
                            }

                            dayBoardingWithLunchDV.Visible = false;
                            ViewState["IsUpdate"] = "1";
                            txt_admission_no.ReadOnly = true;
                            btn_final_submit.Text = "Update";
                            btn_cancel.Visible = true;
                            ltUsertop.Text = "Edit Registration";
                            HdID.Value = Request.QueryString["stdid"].ToString();
                            ViewState["admno"] = Request.QueryString["admno"].ToString();
                            BindDetails();
                            bind_doc_type();
                        }
                        else if (Request.QueryString["transfrScholarship"] != null) //======================================Scholarship Transfer
                        {
                            ViewState["IsTransfer"] = "2";
                            ViewState["IsTransferFormSale"] = "2";
                            btn_final_submit.Text = "Transfer to Admission";
                            btn_cancel.Visible = true;
                            ltUsertop.Text = "Scholarship Transfer Registration";
                            ViewState["regiDs"] = Request.QueryString["regiDs"].ToString();
                            ViewState["admno"] = Request.QueryString["regiDs"].ToString();
                            BindTransferDetails_Scholarship();
                            bind_doc_type();
                            fetch_admission_no();
                        }

                        else if (Request.QueryString["transfr"] != null) //======================================Transfer
                        {
                            ViewState["IsTransfer"] = "1";
                            ViewState["IsTransferFormSale"] = "0";
                            btn_final_submit.Text = "Transfer";
                            btn_cancel.Visible = true;
                            ltUsertop.Text = "Transfer Registration";
                            ViewState["regiDs"] = Request.QueryString["regiDs"].ToString();
                            BindTransferDetails();
                            bind_doc_type();
                            fetch_admission_no();
                        }
                        else if (Request.QueryString["transfrFormSale"] != null) //======================================TransferFormSale
                        {
                            ViewState["IsTransfer"] = "0";
                            ViewState["IsTransferFormSale"] = "1";
                            btn_final_submit.Text = "Transfer";
                            btn_cancel.Visible = true;
                            ltUsertop.Text = "Transfer Registration";
                            ViewState["trnsferFrmSlaeId"] = Request.QueryString["regiDs"].ToString();
                            ViewState["regiDs"] = Request.QueryString["regiDs"].ToString() + temp_reg_id();
                            BindTransferFormSaleDetails();
                            bind_doc_type();
                            fetch_admission_no();
                        }
                        else if (Request.QueryString["enqid"] != null) //======================================TransferFormEnquiry
                        {
                            ViewState["IsFromEnq"] = "1";
                            ViewState["IsFromEnqId"] = Request.QueryString["enqid"].ToString();
                            BindStdEnqDetails(ViewState["IsFromEnqId"].ToString());
                            bind_doc_type();
                            fetch_admission_no();
                        }
                        else
                        {
                            bind_doc_type();
                            fetch_admission_no();
                        }


                        if (Request.QueryString["stdid"] != null)
                        { }
                        else
                        {
                            mycode.bind_all_ddl_with_id(ddl_hostel, "select DISTINCT Hostel_name,Hostel_id from (select t1.Hostel_name,t1.Hostel_id,t2.Category_id,t2.Room_id,t2.Bed_id from Hostels_master t1 join HOSTEL_ROOM_BED_MASTER t2 on t1.Hostel_id=t2.Hostel_id where t2.Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id=t2.Room_id and Status='1' and Hostel_id=t2.Hostel_id and Category_id=t2.Category_id)) t order by Hostel_name asc");
                            try
                            {
                                ddl_hostel.SelectedValue = My.top_one_hostel_id();
                            }
                            catch (Exception ex)
                            {
                            }
                            mycode.bind_all_ddl_with_id_no_select(ddl_room_cat, "select Category_name,Category_id from Hostel_room_category_master order by Category_name asc");
                            fetch_rooms(); fetch_bed_details();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
            }
        }

        private void get_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count == 0)
            {
                ViewState["firm_id"] = "0";

            }
            else
            {
                ViewState["firm_id"] = dt.Rows[0]["firm_id"].ToString();
            }
        }


        //FetchFrom Enquiry
        private void BindStdEnqDetails(string enquiry_id)
        {
            DataTable dt = mycode.FillData("select * from Enquiry_Details where Enquiry_Id='" + enquiry_id + "'");
            if (dt.Rows.Count == 0)
            {
                Session["msG"] = "Student not found.";
                Response.Redirect("Enquiry_List.aspx", false);
            }
            else
            {
                try
                {
                    ddl_student_type.Text = "NEW";
                }
                catch (Exception ex) { }

                try
                {
                    ddlclass.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                }
                catch (Exception ex) { }

                txt_firstname.Text = dt.Rows[0]["Name"].ToString();
                txt_father_mobile.Text = dt.Rows[0]["Phone"].ToString();
                txt_guardian_email.Text = dt.Rows[0]["Email"].ToString();
                txt_adress.Text = dt.Rows[0]["Address"].ToString();
                txt_present_district.Text = dt.Rows[0]["districtname"].ToString();
                txt_pincode.Text = dt.Rows[0]["Pincode"].ToString();
                txt_temp_mobileno.Text = dt.Rows[0]["Phone"].ToString();
                txt_father_first_name.Text = dt.Rows[0]["Father_name"].ToString();
                try
                {
                    ddl_temp_state.SelectedValue = dt.Rows[0]["statecode"].ToString();
                }
                catch (Exception ex) { }
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState["firm_id"].ToString() == "BD-001")
            {
                if (ViewState["Edtfrom"].ToString() != "edt")
                {
                    fetch_admission_no();
                }
            }

        }

        private void fetch_admission_no()
        {
            string admNo = "";
            try
            {
                txt_admission_no.Enabled = true;
                DataTable dt = mycode.FillData("select * from Admission_no_setting where Status=1 and Session_id='" + ddlsession.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    string sl_no = dt.Rows[0]["Admission_no_start_from_update"].ToString();


                    if (ViewState["firm_id"].ToString() == "BD-001")
                    {

                        if (ddlclass.SelectedValue == "55" || ddlclass.SelectedValue == "56" || ddlclass.SelectedValue == "57")//11th student
                        {
                            DataTable dt1 = mycode.FillData("select * from BD_Academy_eleventh_admission_no where Status=1 and Session_id='" + ddlsession.SelectedValue + "'");
                            if (dt1.Rows.Count > 0)
                            {
                                sl_no = dt1.Rows[0]["Admission_no_start_from_update"].ToString();
                                if (sl_no.Length == 1)
                                {
                                    sl_no = "0" + sl_no;
                                }
                            }
                            admNo = sl_no + dt1.Rows[0]["Postfix"].ToString() + dt1.Rows[0]["Session_code"].ToString();
                        }
                        else
                        {
                            if (sl_no.Length == 1)
                            {
                                sl_no = "0" + sl_no;
                            }
                            admNo = sl_no + dt.Rows[0]["Session_code"].ToString();
                        }






                    }
                    else if (ViewState["firm_id"].ToString() == "KIDS-01")
                    {
                        admNo = sl_no + dt.Rows[0]["Session_code"].ToString();
                    }

                    else if (ViewState["firm_id"].ToString() == "RODEM-001")
                    {
                        if (sl_no.Length == 1)
                        {
                            sl_no = "000" + sl_no;
                        }
                        else if (sl_no.Length == 2)
                        {
                            sl_no = "00" + sl_no;
                        }
                        else if (sl_no.Length == 3)
                        {
                            sl_no = "0" + sl_no;
                        }
                        admNo = dt.Rows[0]["Prefix_Code"].ToString() + dt.Rows[0]["Session_code"].ToString() + sl_no;
                    }
                    else
                    {
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
                    txt_admission_no.Enabled = false;
                    txt_admission_no.CssClass = "form_control";
                    duplicate = false;
                }
                else
                {
                    string admNoss = increase_adm_sl_no();
                    DataTable dt = mycode.FillData("select * from Admission_no_setting where Status=1 and Session_id='" + ddlsession.SelectedValue + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string sl_no = dt.Rows[0]["Admission_no_start_from_update"].ToString();

                        if (ViewState["firm_id"].ToString() == "BD-001")
                        {
                            if (ddlclass.SelectedValue == "55" || ddlclass.SelectedValue == "56" || ddlclass.SelectedValue == "57")//11th student
                            {
                                DataTable dt1 = mycode.FillData("select * from BD_Academy_eleventh_admission_no where Status=1 and Session_id='" + ddlsession.SelectedValue + "'");
                                if (dt1.Rows.Count > 0)
                                {
                                    sl_no = dt1.Rows[0]["Admission_no_start_from_update"].ToString();
                                    if (sl_no.Length == 1)
                                    {
                                        sl_no = "0" + sl_no;
                                    }
                                }
                                admNo = sl_no + dt1.Rows[0]["Postfix"].ToString() + dt1.Rows[0]["Session_code"].ToString();
                            }
                            else
                            {
                                if (sl_no.Length == 1)
                                {
                                    sl_no = "0" + sl_no;
                                }
                                admNo = sl_no + dt.Rows[0]["Session_code"].ToString();
                            }


                            //if (sl_no.Length == 1)
                            //{
                            //    sl_no = "0" + sl_no;
                            //}

                            //admNo = sl_no + dt.Rows[0]["Session_code"].ToString();
                        }
                        else
                        {
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
        }

        private string increase_adm_sl_no()
        {
            string result = "";

            //
            if (ViewState["firm_id"].ToString() == "BD-001")
            {
                if (ddlclass.SelectedValue == "55" || ddlclass.SelectedValue == "56" || ddlclass.SelectedValue == "57")//11th student
                {

                    try
                    {
                        SqlConnection conn = new SqlConnection(My.conn);
                        SqlDataAdapter ad = new SqlDataAdapter("select * from BD_Academy_eleventh_admission_no where Status=1 and Session_id='" + ddlsession.SelectedValue + "'", conn);
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

                }
                else
                {
                    try
                    {
                        SqlConnection conn = new SqlConnection(My.conn);
                        SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_no_setting where Status=1 and Session_id='" + ddlsession.SelectedValue + "'", conn);
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
                }
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection(My.conn);
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_no_setting where Status=1 and Session_id='" + ddlsession.SelectedValue + "'", conn);
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

            }

            return result;
        }

        private void BindTransferFormSaleDetails()
        {
            DataTable dt = mycode.FillData("select * from Form_sale_details where Id='" + ViewState["trnsferFrmSlaeId"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                Session["msG"] = "Student not found.";
                Response.Redirect("form-sale.aspx", false);
            }
            else
            {
                try
                {
                    ddl_student_type.Text = "NEW";
                }
                catch (Exception ex) { }
                //ddl_category.SelectedValue = dt.Rows[0]["Category_id"].ToString();
                //ddl_subcategory.SelectedValue = dt.Rows[0]["SubCategory_id"].ToString();

                //txt_uid.Text = dt.Rows[0]["UID_No"].ToString();

                txt_form_slno.Text = dt.Rows[0]["Form_no"].ToString();
                txt_index_no.Text = dt.Rows[0]["form_indesx_no"].ToString();

                // txt_admission_date.Text = dt.Rows[0]["Date"].ToString();
                try
                {
                    string session_id = get_session_id_by_session(dt.Rows[0]["session"].ToString());
                    ddlsession.SelectedValue = session_id;
                }
                catch (Exception ex) { }
                //if (dt.Rows[0]["Services"].ToString() == "Hosteler")
                //{
                //    rdb_hostel.Checked = true;
                //    rdb_dayscholar.Checked = false;
                //}
                //else
                //{
                //    rdb_hostel.Checked = false;
                //    rdb_dayscholar.Checked = true;
                //}


                //if (dt.Rows[0]["is_applied_dayboarding"].ToString() == "true")
                //{
                //    ddl_day_boarding.SelectedValue = "2";
                //}
                try
                {
                    string class_id = get_class_id_by_class(dt.Rows[0]["class"].ToString());
                    ddlclass.SelectedValue = class_id;
                }
                catch (Exception ex) { }
                //txt_rollnumber.Text = dt.Rows[0]["rollnumber"].ToString();
                //ddl_section.Text = dt.Rows[0]["Section"].ToString();
                //txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                //ddl_house.SelectedValue = dt.Rows[0]["house"].ToString();



                //Student Info 
                txt_firstname.Text = dt.Rows[0]["Student_first_name"].ToString();
                txt_middlename.Text = dt.Rows[0]["Student_middle_name"].ToString();
                txt_lastname.Text = dt.Rows[0]["Student_last_name"].ToString();

                txt_dob.Text = dt.Rows[0]["dob"].ToString();
                //txt_place_of_birth.Text = dt.Rows[0]["place_of_birth"].ToString();
                //ddl_is_birth_certificate.Text = dt.Rows[0]["Is_birth_certificate"].ToString();
                //txt_birth_certificate_no.Text = dt.Rows[0]["birth_certificate_number"].ToString();
                try
                {
                    ddl_gender.Text = dt.Rows[0]["gender"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_religion.Text = dt.Rows[0]["Religion"].ToString();
                }
                catch (Exception ex) { }

                //ddl_ration_cards_types.Text = dt.Rows[0]["ration_type"].ToString();
                try
                {
                    ddl_cast_category.Text = dt.Rows[0]["cast"].ToString();
                }
                catch (Exception ex) { }

                // ddl_is_cast_certificate.Text = dt.Rows[0]["Is_cast_certificate"].ToString();
                //txt_cast_certificate_no.Text = dt.Rows[0]["cast_certificate_no"].ToString();
                txt_aadharno_mark.Text = dt.Rows[0]["Aadhar_no"].ToString();
                //ddl_student_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();
                //ddl_illness.Text = dt.Rows[0]["is_illness"].ToString();
                //txt_illness_remark.Text = dt.Rows[0]["illness_remark"].ToString();
                //ddl_jati.Text = dt.Rows[0]["jati"].ToString();
                // ddl_rte.Text = dt.Rows[0]["RTE"].ToString();
                // ddl_staff_ward.Text = dt.Rows[0]["staff_ward"].ToString();
                // txt_staff_name.Text = dt.Rows[0]["Staff_name"].ToString();
                //txt_identification_marks.Text = dt.Rows[0]["Identification_marks"].ToString();


                txt_height.Text = dt.Rows[0]["Height"].ToString();
                txt_weight.Text = dt.Rows[0]["Weight"].ToString();
                txt_name_of_sibling1.Text = dt.Rows[0]["Name_of_sibling1"].ToString();
                txt_age_sibling1.Text = dt.Rows[0]["Age_of_sibling1"].ToString();
                txt_school_sibling1.Text = dt.Rows[0]["School_of_sibling1"].ToString();
                try
                {
                    ddl_class_sb1.Text = dt.Rows[0]["Class_of_sibling1"].ToString();
                }
                catch (Exception ex)
                {
                }
                txt_name_of_sibling2.Text = dt.Rows[0]["Name_of_sibling2"].ToString();
                txt_age_sibling2.Text = dt.Rows[0]["Age_of_sibling2"].ToString();
                txt_school_sibling2.Text = dt.Rows[0]["School_of_sibling2"].ToString();
                try
                {
                    ddl_class_sb2.Text = dt.Rows[0]["Class_of_sibling2"].ToString();
                }
                catch (Exception ex)
                {
                }

                //Previous School Details 
                //txt_lastschool.Text = dt.Rows[0]["Prev_school_name"].ToString();
                //txt_admission_date_old.Text = dt.Rows[0]["Old_Admission_Date"].ToString();
                //ddl_prev_board_type.Text = dt.Rows[0]["Prev_board_type"].ToString();
                //bind_board_list();
                //ddl_board_list.Text = dt.Rows[0]["Prev_board"].ToString();
                //ddl_old_class.SelectedValue = dt.Rows[0]["Old_class_id"].ToString();
                //txt_percentage.Text = dt.Rows[0]["Prev_percentage"].ToString();
                //txt_reason_for_shift.Text = dt.Rows[0]["Prev_reason_for_shift"].ToString();
                //txt_year.Text = dt.Rows[0]["Prev_year"].ToString();



                //Father Details
                txt_father_first_name.Text = dt.Rows[0]["Father_first_name"].ToString();
                txt_father_middle_name.Text = dt.Rows[0]["Father_middle_name"].ToString();
                txt_father_last_name.Text = dt.Rows[0]["Father_last_name"].ToString();
                try
                {
                    ddl_occupation.Text = dt.Rows[0]["F_occupation"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_father_qualification.Text = dt.Rows[0]["F_qualification"].ToString();
                }
                catch (Exception ex) { }

                //ddl_maritial_status.Text = dt.Rows[0]["f_marrital_statue"].ToString();
                //ddl_cunterycode1.Text = dt.Rows[0]["Country_Code_Father"].ToString();
                txt_father_mobile.Text = dt.Rows[0]["F_contact_no"].ToString();
                txt_father_aadhar_no.Text = dt.Rows[0]["F_aadhar_no"].ToString();
                //ddl_fthr_whatsapp_c_Code.Text = dt.Rows[0]["Father_whatsapp_country_code"].ToString();
                //txt_father_whatsapp_no.Text = dt.Rows[0]["Father_whatsApp_no"].ToString();
                txt_guardian_email.Text = dt.Rows[0]["Guardian_email_id"].ToString();
                txt_guardian_name.Text = dt.Rows[0]["Guardian_first_name"].ToString() + " " + dt.Rows[0]["Guardian_middle_name"].ToString() + " " + dt.Rows[0]["Guardian_last_name"].ToString();
                txt_annual_income.Text = dt.Rows[0]["F_annual_income"].ToString();


                //Mother Details
                txt_mother_first_name.Text = dt.Rows[0]["Mother_first_name"].ToString();
                txt_mother_middle_name.Text = dt.Rows[0]["Mother_middle_name"].ToString();
                txt_mother_last_name.Text = dt.Rows[0]["Mother_last_name"].ToString();
                try
                {
                    ddl_m_occupation.Text = dt.Rows[0]["M_occupation"].ToString();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ddl_mother_qualification.Text = dt.Rows[0]["M_qualification"].ToString();
                }
                catch (Exception ex)
                {
                }

                try
                {
                    ddl_student_mother_tongue.Text = dt.Rows[0]["Mother_tongue"].ToString();
                }
                catch (Exception ex)
                {
                }

                //ddl_m_maritial_status.Text = dt.Rows[0]["m_marrital_statue"].ToString();
                //ddl_cunterycode2.Text = dt.Rows[0]["Country_Code_Mother"].ToString();
                txt_mother_mobile_no.Text = dt.Rows[0]["M_contact_no"].ToString();
                //ddl_mthr_whatsapp_c_Code.Text = dt.Rows[0]["Mother_whatsapp_country_code"].ToString();
                //txt_mother_whatsapp_no.Text = dt.Rows[0]["Mother_whatsApp_no"].ToString();
                txt_mother_emailid.Text = dt.Rows[0]["M_email_id"].ToString();
                txt_mother_aadhar_no.Text = dt.Rows[0]["M_aadhar_no"].ToString();


                //Present Address Details
                txt_adress.Text = dt.Rows[0]["Address"].ToString();
                //txt_city.Text = dt.Rows[0]["Persent_city"].ToString();
                //txt_present_district.Text = dt.Rows[0]["Persent_district"].ToString();
                //txt_present_po.Text = dt.Rows[0]["Persent_po"].ToString();
                //try
                //{
                //    ddl_temp_state.Text = dt.Rows[0]["Persent_state"].ToString();
                //}
                //catch (Exception ex)
                //{
                //}
                //txt_pincode.Text = dt.Rows[0]["Persent_pincode"].ToString();


                //txt_temp_ps.Text = dt.Rows[0]["policestation"].ToString();  
                //ddl_country_c.Text = dt.Rows[0]["Present_country"].ToString();
                //ddl_cunterycode3.Text = dt.Rows[0]["Country_Code_Current_add"].ToString();
                //txt_temp_mobileno.Text = dt.Rows[0]["mobilenumber"].ToString();


                //Permanent Address Details
                txt_pAddress.Text = dt.Rows[0]["Address"].ToString();
                //txt_pcity.Text = dt.Rows[0]["Permanent_city"].ToString();
                //txt_perma_disctrict.Text = dt.Rows[0]["Permanent_district"].ToString();
                //txt_perma_po.Text = dt.Rows[0]["Permanent_po"].ToString();
                //ddl_par_state.Text = dt.Rows[0]["Permanent_state"].ToString();
                //txt_Ppincod.Text = dt.Rows[0]["Permanent_pincod"].ToString();


                //txt_par_ps.Text = dt.Rows[0]["policestation_permanent"].ToString();   
                //ddl_country_p.Text = dt.Rows[0]["Permanent_country"].ToString();
                //ddl_cunterycode4.Text = dt.Rows[0]["Country_Code_Current_Perm_add"].ToString();
                //txt_p_mobile_no.Text = dt.Rows[0]["mob2"].ToString();


                //Bank Details
                //txt_account_no.Text = dt.Rows[0]["Bank_acount_no"].ToString();
                //txt_account_holder_name.Text = dt.Rows[0]["Account_Holder_name"].ToString();
                //txt_bank_name.Text = dt.Rows[0]["Bnk_Name"].ToString();
                //txt_ifsc_code.Text = dt.Rows[0]["IFSC_Code"].ToString();
                //txt_branch_name.Text = dt.Rows[0]["Branch_Name"].ToString(); 

                // transfer_images(ViewState["regiDs"].ToString());

                if (dt.Rows[0]["Prv_school_name2"].ToString() == "")
                {
                    txt_lastschool.Text = dt.Rows[0]["Prv_school_name1"].ToString();
                    ddl_prev_board_type.Text = dt.Rows[0]["Prv_board_type1"].ToString();
                    bind_board_list();
                    ddl_board_list.Text = dt.Rows[0]["Prv_board1"].ToString();
                    string class_id = get_class_id_by_class(dt.Rows[0]["Prv_class_to1"].ToString());
                    try
                    {
                        ddl_old_class.Text = class_id;
                    }
                    catch (Exception ex)
                    {
                    }
                    txt_percentage.Text = dt.Rows[0]["Mark_percent1"].ToString();
                    txt_year.Text = dt.Rows[0]["Prv_year_to1"].ToString();
                }
                else
                {
                    txt_lastschool.Text = dt.Rows[0]["Prv_school_name2"].ToString();
                    ddl_prev_board_type.Text = dt.Rows[0]["Prv_board_type2"].ToString();
                    bind_board_list();
                    ddl_board_list.Text = dt.Rows[0]["Prv_board2"].ToString();
                    string class_id = get_class_id_by_class(dt.Rows[0]["Prv_class_to2"].ToString());
                    try
                    {
                        ddl_old_class.Text = class_id;
                    }
                    catch (Exception ex)
                    {
                    }
                    txt_percentage.Text = dt.Rows[0]["Mark_percent2"].ToString();
                    txt_year.Text = dt.Rows[0]["Prv_year_to2"].ToString();
                }

                txt_present_po.Text = dt.Rows[0]["Post_office"].ToString();
                txt_temp_ps.Text = dt.Rows[0]["Police_station"].ToString();
                txt_present_district.Text = dt.Rows[0]["District"].ToString();
                txt_city.Text = dt.Rows[0]["City"].ToString();
                try { ddl_temp_state.Text = dt.Rows[0]["State"].ToString(); }
                catch (Exception ex) { }
                txt_pincode.Text = dt.Rows[0]["Pin_code"].ToString();
                try
                {
                    ddl_country_c.Text = dt.Rows[0]["Country"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_cunterycode3.Text = dt.Rows[0]["Country_code"].ToString();
                }
                catch (Exception ex) { }
                txt_temp_mobileno.Text = dt.Rows[0]["Add_mobile_no"].ToString();



                try
                {
                    ddl_nationality.Text = dt.Rows[0]["Country"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_father_nationality.Text = dt.Rows[0]["Country"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_mother_nationality.Text = dt.Rows[0]["Country"].ToString();
                }
                catch (Exception ex) { }
            }
        }

        private string get_class_id_by_class(string class_name)
        {
            string classid = "0";
            DataTable dt = mycode.FillData("select course_id from Add_course_table where Course_Name='" + class_name + "'");
            if (dt.Rows.Count > 0)
            {
                classid = dt.Rows[0]["course_id"].ToString();
            }
            return classid;
        }

        private string get_session_id_by_session(string session)
        {
            string sessionid = "0";
            DataTable dt = mycode.FillData("select session_id from session_details where Session='" + session + "'");
            if (dt.Rows.Count > 0)
            {
                sessionid = dt.Rows[0]["session_id"].ToString();
            }
            return sessionid;
        }

        private void BindTransferDetails_Scholarship()
        {
            DataTable dt = mycode.FillData("select * from Scholarship_Admission where Registration_id='" + ViewState["regiDs"].ToString() + "' and (Is_transfer='' or Is_transfer is null)");
            if (dt.Rows.Count == 0)
            {
                Session["msG1"] = "Scholarship Student not found.";
                Response.Redirect("Scholarship_All_Qualified_Student_List.aspx", false);
            }
            else
            {
                try
                {
                    ddl_student_type.Text = "NEW";
                }
                catch (Exception ex) { }
                //ddl_category.SelectedValue = dt.Rows[0]["Category_id"].ToString();
                //ddl_subcategory.SelectedValue = dt.Rows[0]["SubCategory_id"].ToString();
                //txt_form_slno.Text = dt.Rows[0]["formserialnumber"].ToString();
                //txt_uid.Text = dt.Rows[0]["UID_No"].ToString();
                //txt_index_no.Text = dt.Rows[0]["Index_no"].ToString();
                //txt_admission_date.Text = dt.Rows[0]["Date"].ToString();
                try
                {
                    ddlsession.SelectedValue = dt.Rows[0]["Session_id"].ToString();
                }
                catch (Exception ex) { }
                if (dt.Rows[0]["Services"].ToString() == "Hosteler")
                {
                    rdb_hostel.Checked = true;
                    rdb_dayscholar.Checked = false;
                }
                else
                {
                    rdb_hostel.Checked = false;
                    rdb_dayscholar.Checked = true;
                }


                //if (dt.Rows[0]["is_applied_dayboarding"].ToString() == "true")
                //{
                //    ddl_day_boarding.SelectedValue = "2";
                //}
                try
                {
                    ddlclass.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                }
                catch (Exception ex) { }
                //txt_rollnumber.Text = dt.Rows[0]["rollnumber"].ToString();
                //ddl_section.Text = dt.Rows[0]["Section"].ToString();
                //txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                //ddl_house.SelectedValue = dt.Rows[0]["house"].ToString();



                //Student Info 
                txt_firstname.Text = dt.Rows[0]["Student_first_name"].ToString();
                txt_middlename.Text = dt.Rows[0]["Student_middle_name"].ToString();
                txt_lastname.Text = dt.Rows[0]["Student_last_name"].ToString();

                txt_dob.Text = dt.Rows[0]["DOB"].ToString();
                //txt_place_of_birth.Text = dt.Rows[0]["place_of_birth"].ToString();
                //ddl_is_birth_certificate.Text = dt.Rows[0]["Is_birth_certificate"].ToString();
                //txt_birth_certificate_no.Text = dt.Rows[0]["birth_certificate_number"].ToString();
                try
                {
                    ddl_gender.Text = dt.Rows[0]["Gender"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_religion.Text = dt.Rows[0]["Religion"].ToString();
                }
                catch (Exception ex) { }

                //ddl_ration_cards_types.Text = dt.Rows[0]["ration_type"].ToString();
                try
                {
                    ddl_cast_category.Text = dt.Rows[0]["Category"].ToString();
                }
                catch (Exception ex) { }
                // ddl_is_cast_certificate.Text = dt.Rows[0]["Is_cast_certificate"].ToString();
                //txt_cast_certificate_no.Text = dt.Rows[0]["cast_certificate_no"].ToString();
                txt_aadharno_mark.Text = dt.Rows[0]["Aadhar_no"].ToString();
                //ddl_student_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();
                //ddl_illness.Text = dt.Rows[0]["is_illness"].ToString();
                //txt_illness_remark.Text = dt.Rows[0]["illness_remark"].ToString();
                //ddl_jati.Text = dt.Rows[0]["jati"].ToString();
                // ddl_rte.Text = dt.Rows[0]["RTE"].ToString();
                // ddl_staff_ward.Text = dt.Rows[0]["staff_ward"].ToString();
                // txt_staff_name.Text = dt.Rows[0]["Staff_name"].ToString();
                txt_identification_marks.Text = dt.Rows[0]["Identification_marks"].ToString();


                txt_height.Text = dt.Rows[0]["Height"].ToString();
                txt_weight.Text = dt.Rows[0]["Weight"].ToString();
                txt_name_of_sibling1.Text = dt.Rows[0]["Sibling_name1"].ToString();
                txt_age_sibling1.Text = dt.Rows[0]["Sibling_age1"].ToString();
                txt_school_sibling1.Text = dt.Rows[0]["Sibling_school1"].ToString();
                try
                {
                    ddl_class_sb1.Text = dt.Rows[0]["Sibling_class1"].ToString();
                }
                catch (Exception ex)
                {
                }
                txt_name_of_sibling2.Text = dt.Rows[0]["Sibling_name2"].ToString();
                txt_age_sibling2.Text = dt.Rows[0]["Sibling_age2"].ToString();
                txt_school_sibling2.Text = dt.Rows[0]["Sibling_school2"].ToString();
                try
                {
                    ddl_class_sb2.Text = dt.Rows[0]["Sibling_class2"].ToString();
                }
                catch (Exception ex)
                {
                }

                //Previous School Details 
                //txt_lastschool.Text = dt.Rows[0]["Prev_school_name"].ToString();
                //txt_admission_date_old.Text = dt.Rows[0]["Old_Admission_Date"].ToString();
                //ddl_prev_board_type.Text = dt.Rows[0]["Prev_board_type"].ToString();
                //bind_board_list();
                //ddl_board_list.Text = dt.Rows[0]["Prev_board"].ToString();
                //ddl_old_class.SelectedValue = dt.Rows[0]["Old_class_id"].ToString();
                //txt_percentage.Text = dt.Rows[0]["Prev_percentage"].ToString();
                //txt_reason_for_shift.Text = dt.Rows[0]["Prev_reason_for_shift"].ToString();
                //txt_year.Text = dt.Rows[0]["Prev_year"].ToString();



                //Father Details
                txt_father_first_name.Text = dt.Rows[0]["Father_first_name"].ToString();
                txt_father_middle_name.Text = dt.Rows[0]["Father_middle_name"].ToString();
                txt_father_last_name.Text = dt.Rows[0]["Father_last_name"].ToString();
                try
                {
                    ddl_occupation.Text = dt.Rows[0]["Father_occupation"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_father_qualification.Text = dt.Rows[0]["Father_qualitication"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_father_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                }
                catch (Exception ex) { }
                //ddl_maritial_status.Text = dt.Rows[0]["f_marrital_statue"].ToString();
                //ddl_cunterycode1.Text = dt.Rows[0]["Country_Code_Father"].ToString();
                txt_father_mobile.Text = dt.Rows[0]["Father_mobile"].ToString();
                //ddl_fthr_whatsapp_c_Code.Text = dt.Rows[0]["Father_whatsapp_country_code"].ToString();
                //txt_father_whatsapp_no.Text = dt.Rows[0]["Father_whatsApp_no"].ToString();
                txt_guardian_email.Text = dt.Rows[0]["Email"].ToString();
                txt_guardian_name.Text = dt.Rows[0]["guardianname"].ToString();
                txt_annual_income.Text = dt.Rows[0]["Father_annual_income"].ToString();


                //Mother Details
                txt_mother_first_name.Text = dt.Rows[0]["Mother_first_name"].ToString();
                txt_mother_middle_name.Text = dt.Rows[0]["Mother_middle_name"].ToString();
                txt_mother_last_name.Text = dt.Rows[0]["Mother_last_name"].ToString();
                try
                {
                    ddl_m_occupation.Text = dt.Rows[0]["Mother_occupation"].ToString();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ddl_mother_qualification.Text = dt.Rows[0]["Mother_qualification"].ToString();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ddl_mother_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                }
                catch (Exception ex)
                {
                }
                //ddl_m_maritial_status.Text = dt.Rows[0]["m_marrital_statue"].ToString();
                //ddl_cunterycode2.Text = dt.Rows[0]["Country_Code_Mother"].ToString();
                txt_mother_mobile_no.Text = dt.Rows[0]["Mother_mobile"].ToString();
                //ddl_mthr_whatsapp_c_Code.Text = dt.Rows[0]["Mother_whatsapp_country_code"].ToString();
                //txt_mother_whatsapp_no.Text = dt.Rows[0]["Mother_whatsApp_no"].ToString();
                txt_mother_emailid.Text = dt.Rows[0]["Mother_emailid"].ToString();



                //Present Address Details
                txt_adress.Text = dt.Rows[0]["Persent_adress"].ToString();
                txt_city.Text = dt.Rows[0]["Persent_city"].ToString();
                txt_present_district.Text = dt.Rows[0]["Persent_district"].ToString();
                txt_present_po.Text = dt.Rows[0]["Persent_po"].ToString();
                try
                {
                    ddl_temp_state.Text = dt.Rows[0]["Persent_state"].ToString();
                }
                catch (Exception ex)
                {
                }
                txt_pincode.Text = dt.Rows[0]["Persent_pincode"].ToString();


                //txt_temp_ps.Text = dt.Rows[0]["policestation"].ToString();  
                //ddl_country_c.Text = dt.Rows[0]["Present_country"].ToString();
                //ddl_cunterycode3.Text = dt.Rows[0]["Country_Code_Current_add"].ToString();
                //txt_temp_mobileno.Text = dt.Rows[0]["mobilenumber"].ToString();


                //Permanent Address Details
                txt_pAddress.Text = dt.Rows[0]["Permanent_adress"].ToString();
                txt_pcity.Text = dt.Rows[0]["Permanent_city"].ToString();
                txt_perma_disctrict.Text = dt.Rows[0]["Permanent_district"].ToString();
                txt_perma_po.Text = dt.Rows[0]["Permanent_po"].ToString();
                ddl_par_state.Text = dt.Rows[0]["Permanent_state"].ToString();
                txt_Ppincod.Text = dt.Rows[0]["Permanent_pincod"].ToString();


                //txt_par_ps.Text = dt.Rows[0]["policestation_permanent"].ToString();   
                //ddl_country_p.Text = dt.Rows[0]["Permanent_country"].ToString();
                //ddl_cunterycode4.Text = dt.Rows[0]["Country_Code_Current_Perm_add"].ToString();
                //txt_p_mobile_no.Text = dt.Rows[0]["mob2"].ToString();


                //Bank Details
                //txt_account_no.Text = dt.Rows[0]["Bank_acount_no"].ToString();
                //txt_account_holder_name.Text = dt.Rows[0]["Account_Holder_name"].ToString();
                //txt_bank_name.Text = dt.Rows[0]["Bnk_Name"].ToString();
                //txt_ifsc_code.Text = dt.Rows[0]["IFSC_Code"].ToString();
                //txt_branch_name.Text = dt.Rows[0]["Branch_Name"].ToString(); 

                transfer_images_Scholarship(ViewState["regiDs"].ToString());
            }
        }

        private void transfer_images_Scholarship(string transfr_reg_id)
        {
            DataTable dt = mycode.FillData("select * from Upload_document_type order by Position asc");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (mycode.IsExist("select Admission_no from Student_image_new where Admission_no='" + transfr_reg_id + "' and Image_type='" + dr["Column_name"].ToString() + "'"))
                    {
                        DataTable dti = mycode.FillData("select " + dr["Online_reg_column"] + " as imagesPaths from Scholarship_Admission where Registration_id='" + transfr_reg_id + "'");
                        if (dti.Rows.Count > 0)
                        {
                            if (dti.Rows[0]["imagesPaths"].ToString() == "" || dti.Rows[0]["imagesPaths"].ToString() == null)
                            {


                            }
                            else
                            {
                                SqlCommand cmd;
                                string query = " INSERT INTO Student_image_new (Admission_no,Image_name,Image_type,Image_path,Session_id) values (@Admission_no,@Image_name,@Image_type,@Image_path,@Session_id)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Admission_no", transfr_reg_id);
                                cmd.Parameters.AddWithValue("@Image_name", dr["Document_type"].ToString());
                                cmd.Parameters.AddWithValue("@Image_type", dr["Column_name"].ToString());
                                cmd.Parameters.AddWithValue("@Image_path", dti.Rows[0]["imagesPaths"].ToString());
                                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                                if (InsertUpdate.InsertUpdateData(cmd))
                                {
                                }
                            }
                        }
                    }
                }
            }
        }

        private void BindTransferDetails()
        {
            DataTable dt = mycode.FillData("select * from Online_Admission where Registration_id='" + ViewState["regiDs"].ToString() + "' and (Is_transfer='' or Is_transfer is null)");
            if (dt.Rows.Count == 0)
            {
                Session["msG"] = "Student not found.";
                Response.Redirect("online-registration.aspx", false);
            }
            else
            {
                try
                {
                    ddl_student_type.Text = "NEW";
                }
                catch (Exception ex) { }
                //ddl_category.SelectedValue = dt.Rows[0]["Category_id"].ToString();
                //ddl_subcategory.SelectedValue = dt.Rows[0]["SubCategory_id"].ToString();
                //txt_form_slno.Text = dt.Rows[0]["formserialnumber"].ToString();
                //txt_uid.Text = dt.Rows[0]["UID_No"].ToString();
                //txt_index_no.Text = dt.Rows[0]["Index_no"].ToString();
                //txt_admission_date.Text = dt.Rows[0]["Date"].ToString();
                try
                {
                    ddlsession.SelectedValue = dt.Rows[0]["Session_id"].ToString();
                }
                catch (Exception ex) { }
                if (dt.Rows[0]["Services"].ToString() == "Hosteler")
                {
                    rdb_hostel.Checked = true;
                    rdb_dayscholar.Checked = false;
                }
                else
                {
                    rdb_hostel.Checked = false;
                    rdb_dayscholar.Checked = true;
                }


                //if (dt.Rows[0]["is_applied_dayboarding"].ToString() == "true")
                //{
                //    ddl_day_boarding.SelectedValue = "2";
                //}
                try
                {
                    ddlclass.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                }
                catch (Exception ex) { }
                //txt_rollnumber.Text = dt.Rows[0]["rollnumber"].ToString();
                //ddl_section.Text = dt.Rows[0]["Section"].ToString();
                //txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                //ddl_house.SelectedValue = dt.Rows[0]["house"].ToString();


                //Student Info 
                txt_firstname.Text = dt.Rows[0]["Student_first_name"].ToString();
                txt_middlename.Text = dt.Rows[0]["Student_middle_name"].ToString();
                txt_lastname.Text = dt.Rows[0]["Student_last_name"].ToString();

                txt_dob.Text = dt.Rows[0]["DOB"].ToString();
                //txt_place_of_birth.Text = dt.Rows[0]["place_of_birth"].ToString();
                //ddl_is_birth_certificate.Text = dt.Rows[0]["Is_birth_certificate"].ToString();
                //txt_birth_certificate_no.Text = dt.Rows[0]["birth_certificate_number"].ToString();
                try
                {
                    ddl_gender.Text = dt.Rows[0]["Gender"].ToString().ToUpper();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_religion.Text = dt.Rows[0]["Religion"].ToString();
                }
                catch (Exception ex) { }

                //ddl_ration_cards_types.Text = dt.Rows[0]["ration_type"].ToString();
                try
                {
                    ddl_cast_category.Text = dt.Rows[0]["Category"].ToString();
                }
                catch (Exception ex) { }
                // ddl_is_cast_certificate.Text = dt.Rows[0]["Is_cast_certificate"].ToString();
                //txt_cast_certificate_no.Text = dt.Rows[0]["cast_certificate_no"].ToString();
                txt_aadharno_mark.Text = dt.Rows[0]["Aadhar_no"].ToString();
                //ddl_student_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();
                //ddl_illness.Text = dt.Rows[0]["is_illness"].ToString();
                //txt_illness_remark.Text = dt.Rows[0]["illness_remark"].ToString();
                //ddl_jati.Text = dt.Rows[0]["jati"].ToString();
                // ddl_rte.Text = dt.Rows[0]["RTE"].ToString();
                // ddl_staff_ward.Text = dt.Rows[0]["staff_ward"].ToString();
                // txt_staff_name.Text = dt.Rows[0]["Staff_name"].ToString();
                txt_identification_marks.Text = dt.Rows[0]["Identification_marks"].ToString();


                txt_height.Text = dt.Rows[0]["Height"].ToString();
                txt_weight.Text = dt.Rows[0]["Weight"].ToString();
                txt_name_of_sibling1.Text = dt.Rows[0]["Sibling_name1"].ToString();
                txt_age_sibling1.Text = dt.Rows[0]["Sibling_age1"].ToString();
                txt_school_sibling1.Text = dt.Rows[0]["Sibling_school1"].ToString();
                try
                {
                    ddl_class_sb1.Text = dt.Rows[0]["Sibling_class1"].ToString();
                }
                catch (Exception ex)
                {
                }
                txt_name_of_sibling2.Text = dt.Rows[0]["Sibling_name2"].ToString();
                txt_age_sibling2.Text = dt.Rows[0]["Sibling_age2"].ToString();
                txt_school_sibling2.Text = dt.Rows[0]["Sibling_school2"].ToString();
                try
                {
                    ddl_class_sb2.Text = dt.Rows[0]["Sibling_class2"].ToString();
                }
                catch (Exception ex)
                {
                }

                //Previous School Details 
                //txt_lastschool.Text = dt.Rows[0]["Prev_school_name"].ToString();
                //txt_admission_date_old.Text = dt.Rows[0]["Old_Admission_Date"].ToString();
                //ddl_prev_board_type.Text = dt.Rows[0]["Prev_board_type"].ToString();
                //bind_board_list();
                //ddl_board_list.Text = dt.Rows[0]["Prev_board"].ToString();
                //ddl_old_class.SelectedValue = dt.Rows[0]["Old_class_id"].ToString();
                //txt_percentage.Text = dt.Rows[0]["Prev_percentage"].ToString();
                //txt_reason_for_shift.Text = dt.Rows[0]["Prev_reason_for_shift"].ToString();
                //txt_year.Text = dt.Rows[0]["Prev_year"].ToString();



                //Father Details
                txt_father_first_name.Text = dt.Rows[0]["Father_first_name"].ToString();
                txt_father_middle_name.Text = dt.Rows[0]["Father_middle_name"].ToString();
                txt_father_last_name.Text = dt.Rows[0]["Father_last_name"].ToString();
                try
                {
                    ddl_occupation.Text = dt.Rows[0]["Father_occupation"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_father_qualification.Text = dt.Rows[0]["Father_qualitication"].ToString();
                }
                catch (Exception ex) { }
                try
                {
                    ddl_father_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                }
                catch (Exception ex) { }
                //ddl_maritial_status.Text = dt.Rows[0]["f_marrital_statue"].ToString();
                //ddl_cunterycode1.Text = dt.Rows[0]["Country_Code_Father"].ToString();
                txt_father_mobile.Text = dt.Rows[0]["Father_mobile"].ToString();
                //ddl_fthr_whatsapp_c_Code.Text = dt.Rows[0]["Father_whatsapp_country_code"].ToString();
                //txt_father_whatsapp_no.Text = dt.Rows[0]["Father_whatsApp_no"].ToString();
                txt_guardian_email.Text = dt.Rows[0]["Email"].ToString();
                txt_guardian_name.Text = dt.Rows[0]["Father_name"].ToString();
                txt_annual_income.Text = dt.Rows[0]["Father_annual_income"].ToString();


                //Mother Details
                txt_mother_first_name.Text = dt.Rows[0]["Mother_first_name"].ToString();
                txt_mother_middle_name.Text = dt.Rows[0]["Mother_middle_name"].ToString();
                txt_mother_last_name.Text = dt.Rows[0]["Mother_last_name"].ToString();
                try
                {
                    ddl_m_occupation.Text = dt.Rows[0]["Mother_occupation"].ToString();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ddl_mother_qualification.Text = dt.Rows[0]["Mother_qualification"].ToString();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ddl_mother_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                }
                catch (Exception ex)
                {
                }
                //ddl_m_maritial_status.Text = dt.Rows[0]["m_marrital_statue"].ToString();
                //ddl_cunterycode2.Text = dt.Rows[0]["Country_Code_Mother"].ToString();
                txt_mother_mobile_no.Text = dt.Rows[0]["Mother_mobile"].ToString();
                //ddl_mthr_whatsapp_c_Code.Text = dt.Rows[0]["Mother_whatsapp_country_code"].ToString();
                //txt_mother_whatsapp_no.Text = dt.Rows[0]["Mother_whatsApp_no"].ToString();
                txt_mother_emailid.Text = dt.Rows[0]["Mother_emailid"].ToString();



                //Present Address Details
                txt_adress.Text = dt.Rows[0]["Persent_adress"].ToString();
                txt_city.Text = dt.Rows[0]["Persent_city"].ToString();
                txt_present_district.Text = dt.Rows[0]["Persent_district"].ToString();
                txt_present_po.Text = dt.Rows[0]["Persent_po"].ToString();
                try
                {
                    ddl_temp_state.Text = dt.Rows[0]["Persent_state"].ToString();
                }
                catch (Exception ex)
                {
                }
                txt_pincode.Text = dt.Rows[0]["Persent_pincode"].ToString();


                //txt_temp_ps.Text = dt.Rows[0]["policestation"].ToString();  
                //ddl_country_c.Text = dt.Rows[0]["Present_country"].ToString();
                //ddl_cunterycode3.Text = dt.Rows[0]["Country_Code_Current_add"].ToString();
                //txt_temp_mobileno.Text = dt.Rows[0]["mobilenumber"].ToString();


                //Permanent Address Details
                txt_pAddress.Text = dt.Rows[0]["Permanent_adress"].ToString();
                txt_pcity.Text = dt.Rows[0]["Permanent_city"].ToString();
                txt_perma_disctrict.Text = dt.Rows[0]["Permanent_district"].ToString();
                txt_perma_po.Text = dt.Rows[0]["Permanent_po"].ToString();
                ddl_par_state.Text = dt.Rows[0]["Permanent_state"].ToString();
                txt_Ppincod.Text = dt.Rows[0]["Permanent_pincod"].ToString();


                //txt_par_ps.Text = dt.Rows[0]["policestation_permanent"].ToString();   
                //ddl_country_p.Text = dt.Rows[0]["Permanent_country"].ToString();
                //ddl_cunterycode4.Text = dt.Rows[0]["Country_Code_Current_Perm_add"].ToString();
                //txt_p_mobile_no.Text = dt.Rows[0]["mob2"].ToString();


                //Bank Details
                //txt_account_no.Text = dt.Rows[0]["Bank_acount_no"].ToString();
                //txt_account_holder_name.Text = dt.Rows[0]["Account_Holder_name"].ToString();
                //txt_bank_name.Text = dt.Rows[0]["Bnk_Name"].ToString();
                //txt_ifsc_code.Text = dt.Rows[0]["IFSC_Code"].ToString();
                //txt_branch_name.Text = dt.Rows[0]["Branch_Name"].ToString(); 

                transfer_images(ViewState["regiDs"].ToString());
            }
        }

        private void transfer_images(string transfr_reg_id)
        {
            DataTable dt = mycode.FillData("select * from Upload_document_type order by Position asc");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (mycode.IsExist("select Admission_no from Student_image_new where Admission_no='" + transfr_reg_id + "' and Image_type='" + dr["Column_name"].ToString() + "'"))
                    {
                        DataTable dti = mycode.FillData("select " + dr["Online_reg_column"] + " as imagesPaths from Online_Admission where Registration_id='" + transfr_reg_id + "'");
                        if (dti.Rows.Count > 0)
                        {
                            if (dti.Rows[0]["imagesPaths"].ToString() == "" || dti.Rows[0]["imagesPaths"].ToString() == null) { }
                            else
                            {
                                SqlCommand cmd;
                                string query = " INSERT INTO Student_image_new (Admission_no,Image_name,Image_type,Image_path,Session_id) values (@Admission_no,@Image_name,@Image_type,@Image_path,@Session_id)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Admission_no", transfr_reg_id);
                                cmd.Parameters.AddWithValue("@Image_name", dr["Document_type"].ToString());
                                cmd.Parameters.AddWithValue("@Image_type", dr["Column_name"].ToString());
                                cmd.Parameters.AddWithValue("@Image_path", dti.Rows[0]["imagesPaths"].ToString());
                                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                                if (InsertUpdate.InsertUpdateData(cmd))
                                {
                                }
                            }
                        }
                    }
                }
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
                if (dt.Rows[0]["Transfer_Status"].ToString().ToUpper() == "NEW")
                {
                    ddl_student_type.Text = "NEW";
                }
                else
                {
                    ddl_student_type.Text = "OLD";
                }

                //==============================================================
                txt_hobbies.Text = dt.Rows[0]["Hobbie_of_student"].ToString();
                txt_prev_last_class_attended.Text = dt.Rows[0]["Prev_class_attended"].ToString();
                try
                {
                    ddl_prev_pass_fail_status.Text = dt.Rows[0]["Prev_pass_fail_status"].ToString();
                }
                catch (Exception ex)
                {
                }
                txt_father_age.Text = dt.Rows[0]["Father_age"].ToString();
                txt_mother_age.Text = dt.Rows[0]["Mother_age"].ToString();
                txt_mother_annual_income.Text = dt.Rows[0]["Mother_annual_income"].ToString();
                txt_relation_with_student.Text = dt.Rows[0]["Guardian_relation_with_student"].ToString();
                try
                {
                    ddl_guardian_occupation.Text = dt.Rows[0]["Guardian_occupation"].ToString();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ddl_guardian_qualification.Text = dt.Rows[0]["Guardian_qualification"].ToString();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ddl_guardian_contry_code.Text = dt.Rows[0]["Guardian_contry_code"].ToString();
                }
                catch (Exception ex)
                {
                }
                txt_guardian_mobile_no.Text = dt.Rows[0]["Guardian_mobile_no"].ToString();
                txt_guardian_aadhar_no.Text = dt.Rows[0]["Guardian_aadhar_no"].ToString();
                txt_guardian_annual_income.Text = dt.Rows[0]["Guardian_annual_income"].ToString();
                txt_guardian_address.Text = dt.Rows[0]["Guardian_address"].ToString();
                //===========================================================================


                ddl_category.SelectedValue = dt.Rows[0]["Category_id"].ToString();
                ddl_subcategory.SelectedValue = dt.Rows[0]["SubCategory_id"].ToString();
                txt_form_slno.Text = dt.Rows[0]["formserialnumber"].ToString();
                txt_uid.Text = dt.Rows[0]["UID_No"].ToString();
                txt_index_no.Text = dt.Rows[0]["Index_no"].ToString();
                txt_admission_date.Text = dt.Rows[0]["dateofadmission"].ToString();
                ddlsession.SelectedValue = dt.Rows[0]["Session_id"].ToString();
                txt_student_pen_no.Text = dt.Rows[0]["Student_pen_no"].ToString();

                //if (dt.Rows[0]["hosteltaken"].ToString() == "Yes")
                //{
                //    dayBoardingWithLunchDV.Visible = false;
                //    rdb_hostel.Checked = true;
                //}
                //else
                //{
                //    dayBoardingWithLunchDV.Visible = true;
                //    rdb_hostel.Checked = false;
                //} 
                //if (dt.Rows[0]["is_applied_dayboarding"].ToString() == "true")
                //{
                //    try
                //    {
                //        ddl_day_boarding.SelectedValue = "2";
                //    }
                //    catch
                //    {
                //        ddl_day_boarding.SelectedValue = "0";
                //    }
                //}

                ddlclass.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                txt_rollnumber.Text = dt.Rows[0]["rollnumber"].ToString();
                ddl_section.Text = dt.Rows[0]["Section"].ToString();
                txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();

                try
                {
                    ddl_house.SelectedValue = dt.Rows[0]["house"].ToString();
                }
                catch (Exception ex)
                {
                }



                //Student Info 
                txt_firstname.Text = dt.Rows[0]["Student_Name_First"].ToString();
                txt_middlename.Text = dt.Rows[0]["Student_Middle_Name"].ToString();
                txt_lastname.Text = dt.Rows[0]["Student_Name_Last"].ToString();

                txt_dob.Text = dt.Rows[0]["dob"].ToString();
                txt_place_of_birth.Text = dt.Rows[0]["place_of_birth"].ToString();
                ddl_is_birth_certificate.Text = dt.Rows[0]["Is_birth_certificate"].ToString();
                txt_birth_certificate_no.Text = dt.Rows[0]["birth_certificate_number"].ToString();
                ddl_gender.Text = dt.Rows[0]["gender"].ToString();
                ddl_blood_group.Text = dt.Rows[0]["blood_group"].ToString();

                try
                {
                    ddl_nationality.Text = dt.Rows[0]["Student_nationality"].ToString();
                }
                catch (Exception ex)
                {
                }

                try
                {
                    if (dt.Rows[0]["Student_nationality"].ToString().ToUpper() == "INDIA")
                    {
                        ddl_nationality.Text = "INDIAN";
                    }
                }
                catch (Exception ex)
                {
                }


                try
                {

                    ddl_religion.Text = dt.Rows[0]["religion"].ToString();
                }
                catch (Exception ex)
                {
                }

                try
                {
                    ddl_ration_cards_types.Text = dt.Rows[0]["ration_type"].ToString();
                }
                catch (Exception ex)
                {
                }

                try
                {
                    ddl_cast_category.Text = dt.Rows[0]["cast"].ToString();
                }
                catch (Exception ex)
                {
                }

                try
                {
                    ddl_caste_jati.Text = dt.Rows[0]["jati"].ToString();
                }
                catch (Exception ex)
                {
                }




                ddl_is_cast_certificate.Text = dt.Rows[0]["Is_cast_certificate"].ToString();
                txt_cast_certificate_no.Text = dt.Rows[0]["cast_certificate_no"].ToString();
                txt_aadharno_mark.Text = dt.Rows[0]["aadharno"].ToString();
                ddl_student_mother_tongue.Text = dt.Rows[0]["student_mother_tounge"].ToString();
                ddl_illness.Text = dt.Rows[0]["is_illness"].ToString();
                txt_illness_remark.Text = dt.Rows[0]["illness_remark"].ToString();
                txt_employee_code.Text = dt.Rows[0]["Staff_employee_code"].ToString();
                ddl_rte.Text = dt.Rows[0]["RTE"].ToString();
                ddl_staff_ward.Text = dt.Rows[0]["staff_ward"].ToString();
                txt_staff_name.Text = dt.Rows[0]["Staff_name"].ToString();
                txt_identification_marks.Text = dt.Rows[0]["Personal_Identymarks"].ToString();
                txt_identification_marks.Text = dt.Rows[0]["identifacationmark"].ToString();

                txt_height.Text = dt.Rows[0]["Height"].ToString();
                txt_weight.Text = dt.Rows[0]["Weight"].ToString();
                txt_name_of_sibling1.Text = dt.Rows[0]["Sibling_name1"].ToString();
                txt_age_sibling1.Text = dt.Rows[0]["Sibling_age1"].ToString();
                txt_school_sibling1.Text = dt.Rows[0]["Sibling_school1"].ToString();
                try
                {
                    ddl_class_sb1.Text = dt.Rows[0]["Sibling_class1"].ToString();
                }
                catch (Exception ex)
                {
                }
                txt_name_of_sibling2.Text = dt.Rows[0]["Sibling_name2"].ToString();
                txt_age_sibling2.Text = dt.Rows[0]["Sibling_age2"].ToString();
                txt_school_sibling2.Text = dt.Rows[0]["Sibling_school2"].ToString();
                try
                {
                    ddl_class_sb2.Text = dt.Rows[0]["Sibling_class2"].ToString();
                }
                catch (Exception ex)
                {
                }

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
                txt_father_first_name.Text = dt.Rows[0]["Father_Name_First"].ToString();
                txt_father_middle_name.Text = dt.Rows[0]["Father_Name_Middle"].ToString();
                txt_father_last_name.Text = dt.Rows[0]["Father_Name_Last"].ToString();

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

                ddl_father_nationality.Text = dt.Rows[0]["f_nationality"].ToString();
                if (dt.Rows[0]["f_nationality"].ToString().ToUpper() == "INDIA")
                {
                    ddl_father_nationality.Text = "INDIAN";
                }

                ddl_maritial_status.Text = dt.Rows[0]["f_marrital_statue"].ToString();
                ddl_cunterycode1.Text = dt.Rows[0]["Country_Code_Father"].ToString();
                txt_father_mobile.Text = dt.Rows[0]["father_mob"].ToString();
                ddl_fthr_whatsapp_c_Code.Text = dt.Rows[0]["Father_whatsapp_country_code"].ToString();
                txt_father_whatsapp_no.Text = dt.Rows[0]["Father_whatsApp_no"].ToString();
                txt_guardian_email.Text = dt.Rows[0]["email_id"].ToString();
                txt_guardian_name.Text = dt.Rows[0]["guardianname"].ToString();
                txt_annual_income.Text = dt.Rows[0]["parentincome"].ToString();
                txt_father_aadhar_no.Text = dt.Rows[0]["Father_aadhar_no"].ToString();


                //Mother Details
                txt_mother_first_name.Text = dt.Rows[0]["Mother_Name_First"].ToString();
                txt_mother_middle_name.Text = dt.Rows[0]["Mother_Name_Middle"].ToString();
                txt_mother_last_name.Text = dt.Rows[0]["Mother_Name_Last"].ToString();

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


                ddl_mother_nationality.Text = dt.Rows[0]["m_nationality"].ToString();
                if (dt.Rows[0]["m_nationality"].ToString().ToUpper() == "INDIA")
                {
                    ddl_mother_nationality.Text = "INDIAN";
                }
                ddl_m_maritial_status.Text = dt.Rows[0]["m_marrital_statue"].ToString();
                ddl_cunterycode2.Text = dt.Rows[0]["Country_Code_Mother"].ToString();
                txt_mother_mobile_no.Text = dt.Rows[0]["mother_mob"].ToString();
                ddl_mthr_whatsapp_c_Code.Text = dt.Rows[0]["Mother_whatsapp_country_code"].ToString();
                txt_mother_whatsapp_no.Text = dt.Rows[0]["Mother_whatsApp_no"].ToString();
                txt_mother_emailid.Text = dt.Rows[0]["mother_email"].ToString();
                txt_mother_aadhar_no.Text = dt.Rows[0]["Mother_aadhar_no"].ToString();

                //Present Address Details
                txt_adress.Text = dt.Rows[0]["careof"].ToString();
                txt_present_po.Text = dt.Rows[0]["postoffice"].ToString();
                txt_temp_ps.Text = dt.Rows[0]["policestation"].ToString();
                txt_present_district.Text = dt.Rows[0]["district"].ToString();
                txt_city.Text = dt.Rows[0]["city"].ToString();
                ddl_temp_state.Text = dt.Rows[0]["state"].ToString();
                txt_pincode.Text = dt.Rows[0]["pin"].ToString();
                ddl_country_c.Text = dt.Rows[0]["Present_country"].ToString();
                if (dt.Rows[0]["Present_country"].ToString().ToUpper() == "INDIA")
                {
                    ddl_country_c.Text = "INDIAN";
                }
                ddl_cunterycode3.Text = dt.Rows[0]["Country_Code_Current_add"].ToString();
                txt_temp_mobileno.Text = dt.Rows[0]["mobilenumber"].ToString();



                //Permanent Address Details
                txt_pAddress.Text = dt.Rows[0]["careof_permanent"].ToString();
                txt_perma_po.Text = dt.Rows[0]["postoffice_permanent"].ToString();
                txt_par_ps.Text = dt.Rows[0]["policestation_permanent"].ToString();
                txt_perma_disctrict.Text = dt.Rows[0]["district_permanent"].ToString();
                txt_pcity.Text = dt.Rows[0]["city_permanent"].ToString();
                ddl_par_state.Text = dt.Rows[0]["state_permanent"].ToString();
                txt_Ppincod.Text = dt.Rows[0]["pincode"].ToString();
                ddl_country_p.Text = dt.Rows[0]["Permanent_country"].ToString();
                if (dt.Rows[0]["Permanent_country"].ToString().ToUpper() == "INDIA")
                {
                    ddl_country_p.Text = "INDIAN";
                }
                ddl_cunterycode4.Text = dt.Rows[0]["Country_Code_Current_Perm_add"].ToString();
                txt_p_mobile_no.Text = dt.Rows[0]["mob2"].ToString();


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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            fil_Capctha_text();
            txt_inputcatcha.Focus();
        }
        private void fil_Capctha_text()
        {
            txt_inputcatcha.Text = "";
            Random random = new Random(DateTime.Now.Millisecond);
            int tempo = random.Next(1, 10);
            lbl_captch1.Text = tempo.ToString();


            Random random1 = new Random(DateTime.Now.Millisecond);
            int tempo1 = random.Next(10, 20);
            lbl_captch3.Text = tempo1.ToString();
            int total = tempo + tempo1;
            ViewState["totcaptc"] = total.ToString();

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

            adm_dt = My.dataTable("select district,postoffice,policestation from dbo.[admission_registor] where session='" + My.get_session() + "'");

            try
            {
                My.bind_ddl_noselect(ddl_section, "select Section from section_master order by Section_order asc");
                //try
                //{
                //    ddl_section.SelectedItem.Text = "NA";
                //}
                //catch
                //{
                //}
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
            try
            {
                ddl_par_state.Text = stateName.ToUpper();
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

            //chk_tc.Checked = true;
            //bind_form_no();
            bind_house();
            bind_session();
            bind_class();


            //My.Check_Amount(txt_paid_amount); 
            // empty_form();
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
        private void bind_c_country()
        {
            comP.bind_ddl_no_select(ddl_country_c, "select Country_name from Country_list");
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
        private void bind_p_country()
        {
            comP.bind_ddl_no_select(ddl_country_p, "select Country_name from Country_list");
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

        private void bind_house()
        {
            My mycode = new My();
            mycode.bind_all_ddl_with_id_cap_NA(ddl_house, "select house_name,house_id from dbo.[house_master]");

        }
        private void bind_form_no()
        {
            //txt_form_no.Text = My.bindList("select distinct Form_no from Form_sale_details where Form_no not in(select formserialnumber from admission_registor)");
        }
        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by Session asc");
            ddlsession.SelectedValue = My.get_session_id_for_admission();
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select   cd.Course_Name,cd.course_id from Add_course_table cd  order by cd.Position asc");
            mycode.bind_all_ddl_with_id(ddl_old_class, "Select   cd.Course_Name,cd.course_id from Add_course_table cd  order by cd.Position asc");

            mycode.bind_all_ddl_with_id(ddl_class_sb1, "Select cd.Course_Name,cd.Course_Name from Add_course_table cd  order by cd.Position asc");
            mycode.bind_all_ddl_with_id(ddl_class_sb2, "Select cd.Course_Name,cd.Course_Name from Add_course_table cd  order by cd.Position asc");
        }

        protected void btn_academic_dt_Click(object sender, EventArgs e)
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

                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                    return;
                }
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_admission_no.Focus();
                    return;
                }


                bool isvalid = check_valid_admission(txt_admission_no.Text);
                if (isvalid == false)
                {
                    Alertme("Please enter valid admission number", "warning");
                    txt_admission_no.Focus();
                    return;
                }
                chnage_color_fisrt_button();
                process_active("2");
            }
            catch (Exception ex)
            {
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


        protected void btn_std_dt_Click(object sender, EventArgs e)
        {
            if (txt_firstname.Text == "")
            {
                chnage_color_fisrt_button();
                Alertme("Please enter first name.", "warning");
                txt_firstname.Focus();
                return;
            }
            if (txt_dob.Text == "")
            {
                chnage_color_fisrt_button();
                Alertme("Please enter date of birth.", "warning");
                txt_dob.Focus();
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

            //if (ddl_is_birth_certificate.SelectedItem.Text == "YES")
            //{
            //    if (txt_birth_certificate_no.Text == "")
            //    {
            //        Alertme("Please enter birth certificate no.", "warning");
            //        txt_birth_certificate_no.Focus();
            //        return;
            //    }
            //}
            if (ddl_nationality.SelectedItem.Text == "Select")
            {
                Alertme("Please select nationality.", "warning");
                ddl_nationality.Focus();
                return;
            }
            if (ddl_cast_category.Text == "Select")
            {
                chnage_color_fisrt_button();
                Alertme("Please select category.", "warning");
                ddl_cast_category.Focus();
                return;
            }


            //if (ddl_is_cast_certificate.SelectedItem.Text == "YES")
            //{
            //    if (txt_cast_certificate_no.Text == "")
            //    {
            //        Alertme("Please enter cast certificate no.", "warning");
            //        txt_cast_certificate_no.Focus();
            //        return;
            //    }
            //}
            if (ddl_illness.SelectedItem.Text == "YES")
            {
                if (txt_illness_remark.Text == "")
                {
                    Alertme("Please enter illness remark.", "warning");
                    txt_illness_remark.Focus();
                    return;
                }
            }
            if (ddl_staff_ward.SelectedItem.Text == "YES")
            {
                if (txt_staff_name.Text == "")
                {
                    Alertme("Please enter staff name.", "warning");
                    txt_staff_name.Focus();
                    return;
                }
            }

            chnage_color_fisrt_button();
            process_active("3");

        }
        //=========================
        protected void btn_prev_dt_Click(object sender, EventArgs e)
        {
            process_active("4");
        }
        //=========================
        protected void btn_fther_dt_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_father_first_name.Text == "")
                {
                    Alertme("Please enter father's name.", "warning");
                    txt_father_first_name.Focus();
                    return;
                }
                if (ddl_father_qualification.SelectedItem.Text == "Select")
                {
                    Alertme("Please select father qualification.", "warning");
                    ddl_father_qualification.Focus();
                    return;
                }
                if (ddl_father_nationality.SelectedItem.Text == "Select")
                {
                    Alertme("Please select father nationality.", "warning");
                    ddl_father_nationality.Focus();
                    return;
                }
                if (txt_father_mobile.Text == "")
                {
                    Alertme("Please enter father mobile no.", "warning");
                    txt_father_mobile.Focus();
                    return;
                }
                //if (txt_guardian_name.Text == "")
                //{
                //    Alertme("Please enter guardian's name.", "warning");
                //    txt_guardian_name.Focus();
                //    return;
                //}
                //if (txt_annual_income.Text == "")
                //{
                //    Alertme("Please enter annual income.", "warning");
                //    txt_annual_income.Focus();
                //    return;
                //}
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

                process_active("5");
            }
            catch (Exception ex)
            {
            }

        }
        //=========================
        protected void btn_mther_dt_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_father_first_name.Text == "")
                {
                    Alertme("Please enter father's name.", "warning");
                    txt_father_first_name.Focus();
                    return;
                }
                if (ddl_father_qualification.SelectedItem.Text == "Select")
                {
                    Alertme("Please select father qualification.", "warning");
                    ddl_father_qualification.Focus();
                    return;
                }
                if (ddl_father_nationality.SelectedItem.Text == "Select")
                {
                    Alertme("Please select father nationality.", "warning");
                    ddl_father_nationality.Focus();
                    return;
                }
                if (txt_father_mobile.Text == "")
                {
                    Alertme("Please enter father mobile no.", "warning");
                    txt_father_mobile.Focus();
                    return;
                }
                //if (txt_guardian_name.Text == "")
                //{
                //    Alertme("Please enter guardian's name.", "warning");
                //    txt_guardian_name.Focus();
                //    return;
                //}

                if (txt_mother_first_name.Text == "")
                {
                    Alertme("Please enter mother's name.", "warning");
                    txt_mother_first_name.Focus();
                    return;
                }
                if (ddl_mother_qualification.SelectedItem.Text == "Select")
                {
                    Alertme("Please select mother qualification.", "warning");
                    ddl_mother_qualification.Focus();
                    return;
                }
                if (ddl_mother_nationality.Text == "")
                {
                    Alertme("Please enter mother nationality.", "warning");
                    ddl_mother_nationality.Focus();
                    return;
                }
                if (txt_father_mobile.Text != "") { if (txt_temp_mobileno.Text == "") { txt_temp_mobileno.Text = txt_father_mobile.Text; } }

                process_active("6");
            }
            catch (Exception ex)
            {
            }
        }

        //=========================
        protected void btn_add_dt_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_adress.Text == "")
                {
                    Alertme("Please enter present address.", "warning");
                    txt_adress.Focus();
                    return;
                }
                //if (txt_present_po.Text == "")
                //{
                //    Alertme("Please enter present P.O.", "warning");
                //    txt_present_po.Focus();
                //    return;
                //}
                //if (txt_present_district.Text == "")
                //{
                //    Alertme("Please enter present district.", "warning");
                //    txt_present_district.Focus();
                //    return;
                //}
                //if (txt_city.Text == "")
                //{
                //    Alertme("Please enter present city.", "warning");
                //    txt_city.Focus();
                //    return;
                //}
                //if (ddl_temp_state.SelectedItem.Text == "Select")
                //{
                //    Alertme("Please select  present state.", "warning");
                //    ddl_temp_state.Focus();
                //    return;
                //}
                //if (txt_pincode.Text == "")
                //{
                //    Alertme("Please enter  present pincode.", "warning");
                //    txt_pincode.Focus();
                //    return;
                //}
                //if (ddl_country_c.Text == "")
                //{
                //    Alertme("Please select  present country.", "warning");
                //    ddl_country_c.Focus();
                //    return;
                //}
                //if (txt_temp_mobileno.Text == "")
                //{
                //    Alertme("Please enter present mobile no.", "warning");
                //    txt_temp_mobileno.Focus();
                //    return;
                //}
                process_active("7");
            }
            catch (Exception ex)
            {
            }

        }
        //=========================
        protected void btn_misc_dt_Click(object sender, EventArgs e)
        {
            process_active("8");
        }
        //=========================

        #region link panle
        protected void lnk_step1_Click(object sender, EventArgs e)
        {
            process_active("1");
        }

        protected void lnk_step2_Click(object sender, EventArgs e)
        {
            process_active("2");
        }

        protected void lnk_step3_Click(object sender, EventArgs e)
        {
            process_active("3");
        }

        protected void lnk_step4_Click(object sender, EventArgs e)
        {
            process_active("4");
            process_active("5");
        }

        protected void lnk_step5_Click(object sender, EventArgs e)
        {
            process_active("6");
        }

        protected void lnk_step6_Click(object sender, EventArgs e)
        {
            process_active("7");
        }

        protected void lnk_step7_Click(object sender, EventArgs e)
        {
            process_active("8");
        }
        #endregion
        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {
                bool final_requiredfilecheck = true;
                // step 1 chheck
                if (ddl_category.SelectedItem.Text == "Select")
                {
                    Categories.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    Categories.Visible = false;
                }
                if (ddl_subcategory.SelectedItem.Text == "Select")
                {
                    subCategories.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    subCategories.Visible = false;
                }

                if (txt_admission_date.Text == "")
                {
                    admissiondate.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    admissiondate.Visible = false;
                }
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    session.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    session.Visible = false;
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {

                    Class_name.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    Class_name.Visible = false;
                }
                if (txt_admission_no.Text == "")
                {
                    admission_no.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    admission_no.Visible = false;
                    if (!check_valid_admission(txt_admission_no.Text))
                    {
                        admission_no_dublicate.Visible = true;
                        final_requiredfilecheck = false;
                    }
                    else
                    {
                        admission_no_dublicate.Visible = false;
                    }
                }

                if (txt_firstname.Text == "")
                {
                    studentname.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    studentname.Visible = false;
                }

                if (txt_dob.Text == "")
                {
                    dateofbirth.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    dateofbirth.Visible = false;

                }
                if (ddl_cast_category.Text == "Select")
                {
                    caste.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    caste.Visible = false;
                }
                if (txt_father_first_name.Text == "")
                {
                    fathername.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    fathername.Visible = false;

                }
                if (txt_father_mobile.Text == "")
                {
                    fathermobileno.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    fathermobileno.Visible = false;
                }

                if (txt_mother_first_name.Text == "")
                {
                    mothername.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    mothername.Visible = false;
                }

                if (txt_adress.Text == "")
                {
                    address.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    address.Visible = false;

                }
                if (ddl_temp_state.Text == "")
                {
                    statename.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    statename.Visible = false;

                }
                if (txt_temp_mobileno.Text == "")
                {
                    mobilenotemp.Visible = true;
                    final_requiredfilecheck = false;
                }
                else
                {
                    mobilenotemp.Visible = false;

                }

                if (final_requiredfilecheck == false)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
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






                if (final_requiredfilecheck == true)
                {
                    if (btn_final_submit.Text == "Submit")
                    {
                        bool flag = false;
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                        {
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();

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
                            else
                            {
                                if (ddl_istransport_assign.SelectedValue == "1")
                                {
                                    if (ddl_trns_vehicle.SelectedItem.Text == "Select")
                                    {
                                        Alertme("Please select vehicle.", "warning");
                                        ddl_trns_vehicle.Focus();
                                        return;
                                    }
                                    if (ddl_trns_route.SelectedItem.Text == "Select")
                                    {
                                        Alertme("Please select Route.", "warning");
                                        ddl_trns_route.Focus();
                                        return;
                                    }
                                    if (ddl_trns_boarding_point.SelectedItem.Text == "Select")
                                    {
                                        Alertme("Please select boarding point.", "warning");
                                        ddl_trns_boarding_point.Focus();
                                        return;
                                    }
                                }
                            }

                            #region check duplicate
                            string adno = txt_admission_no.Text;



                            while (!check_duplicate(adno, con))
                            {
                                if (My.Admission_no_auto == "Yes")
                                {
                                    payments.auto_serialS("Admission_No", con);
                                    adno = payments.view_admission_no_format("Admission_No", con);
                                }
                                else
                                {
                                    Alertme("Sorry! Duplicate Admission No", "warning");
                                    txt_admission_no.Focus();
                                    return;
                                }
                            }

                            //if (txt_rollnumber.Text == "")
                            //{ 

                            //}
                            //else
                            //{
                            //    while (!check_roll_no(roll_no))
                            //    {
                            //        Alertme("Sorry! Duplicate Roll No.", "warning");
                            //        txt_rollnumber.Focus();
                            //        return;
                            //    }
                            //}

                            txt_admission_no.Text = adno;
                            #endregion
                            register_details(con);

                            dues_update_headwise_transaction.update_student_dues(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, "0", "0", con);

                            payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " registered a new student in session : " + ddlsession.SelectedItem.Text + " Student Name:- " + txt_firstname.Text + " Admission No : " + txt_admission_no.Text, con);
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
                                string studentname = txt_firstname.Text + " " + txt_middlename.Text + " " + txt_lastname.Text;
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
                                string student_name = txt_firstname.Text;
                                if (txt_middlename.Text != "" && txt_middlename.Text != " " && txt_middlename.Text != "  " && txt_middlename.Text != "   ")
                                {
                                    student_name = student_name + " " + txt_middlename.Text;
                                }
                                if (txt_lastname.Text != "" && txt_lastname.Text != " " && txt_lastname.Text != "  " && txt_lastname.Text != "   ")
                                {
                                    student_name = student_name + " " + txt_lastname.Text;
                                }


                                string father_name = txt_father_first_name.Text;
                                if (txt_father_middle_name.Text != "" && txt_father_middle_name.Text != " " && txt_father_middle_name.Text != "  " && txt_father_middle_name.Text != "   ")
                                {
                                    father_name = father_name + " " + txt_father_middle_name.Text;
                                }
                                if (txt_father_last_name.Text != "" && txt_father_last_name.Text != " " && txt_father_last_name.Text != "  " && txt_father_last_name.Text != "   ")
                                {
                                    father_name = father_name + " " + txt_father_last_name.Text;
                                }
                                My.send_data_Create_ledger_for_student(txt_admission_no.Text, student_name, ddl_gender.Text, txt_dob.Text, txt_city.Text, txt_present_district.Text, txt_pincode.Text, txt_father_mobile.Text, txt_p_state.Text, father_name, txt_admission_date.Text);
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
                    else if (btn_final_submit.Text == "Transfer")
                    {
                        bool flag = false;
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                        {
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();
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
                            #region check duplicate
                            string adno = txt_admission_no.Text;

                            while (!check_duplicate(adno, con))
                            {
                                if (My.Admission_no_auto == "Yes")
                                {
                                    payments.auto_serialS("Admission_No", con);
                                    adno = payments.view_admission_no_format("Admission_No", con);
                                }
                                else
                                {
                                    Alertme("Sorry! Duplicate Admission No", "warning");
                                    txt_admission_no.Focus();
                                    return;
                                }
                            }

                            //if (txt_rollnumber.Text == "") { }
                            //else
                            //{
                            //    while (!check_roll_no(roll_no))
                            //    {
                            //        Alertme("Sorry! Duplicate Roll No.", "warning");
                            //        txt_rollnumber.Focus();
                            //        return;
                            //    }
                            //}

                            txt_admission_no.Text = adno;
                            #endregion
                            register_details(con);

                            dues_update_headwise_transaction.update_student_dues(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, "0", "0", con);

                            payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " registered a new student in session : " + ddlsession.SelectedItem.Text + " Student Name:- " + txt_firstname.Text + " Admission No : " + txt_admission_no.Text, con);
                            update_in_online_re_tbl(con);


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
                                string studentname = txt_firstname.Text + " " + txt_middlename.Text + " " + txt_lastname.Text;
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
                                string student_name = txt_firstname.Text;
                                if (txt_middlename.Text != "" && txt_middlename.Text != " " && txt_middlename.Text != "  " && txt_middlename.Text != "   ")
                                {
                                    student_name = student_name + " " + txt_middlename.Text;
                                }
                                if (txt_lastname.Text != "" && txt_lastname.Text != " " && txt_lastname.Text != "  " && txt_lastname.Text != "   ")
                                {
                                    student_name = student_name + " " + txt_lastname.Text;
                                }


                                string father_name = txt_father_first_name.Text;
                                if (txt_father_middle_name.Text != "" && txt_father_middle_name.Text != " " && txt_father_middle_name.Text != "  " && txt_father_middle_name.Text != "   ")
                                {
                                    father_name = father_name + " " + txt_father_middle_name.Text;
                                }
                                if (txt_father_last_name.Text != "" && txt_father_last_name.Text != " " && txt_father_last_name.Text != "  " && txt_father_last_name.Text != "   ")
                                {
                                    father_name = father_name + " " + txt_father_last_name.Text;
                                }
                                My.send_data_Create_ledger_for_student(txt_admission_no.Text, student_name, ddl_gender.Text, txt_dob.Text, txt_city.Text, txt_present_district.Text, txt_pincode.Text, txt_father_mobile.Text, txt_p_state.Text, father_name, txt_admission_date.Text);
                            }
                            catch (Exception ex)
                            {
                            }

                            Session["msG"] = "Transfer process has been completed successfully";
                            if (ViewState["printslip"].ToString() == "1")
                            {
                                string url = "slip/Admission_Print.aspx?session_Id=" + ddlsession.SelectedValue + "&admission_no=" + txt_admission_no.Text;
                                Response.Redirect(url, false);
                            }
                            else
                            {
                                if (ViewState["IsTransferFormSale"].ToString() == "1")
                                {
                                    Response.Redirect("form-sale.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect("online-registration.aspx", false);
                                }
                            }
                        }
                    }
                    else if (btn_final_submit.Text == "Transfer to Admission")
                    {
                        bool flag = false;
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                        {
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();

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
                            #region check duplicate
                            string adno = txt_admission_no.Text;

                            while (!check_duplicate(adno, con))
                            {
                                if (My.Admission_no_auto == "Yes")
                                {
                                    payments.auto_serialS("Admission_No", con);
                                    adno = payments.view_admission_no_format("Admission_No", con);
                                }
                                else
                                {
                                    Alertme("Sorry! Duplicate Admission No", "warning");
                                    txt_admission_no.Focus();
                                    return;
                                }
                            }

                            //if (txt_rollnumber.Text == "") { }
                            //else
                            //{
                            //    while (!check_roll_no(roll_no))
                            //    {
                            //        Alertme("Sorry! Duplicate Roll No.", "warning");
                            //        txt_rollnumber.Focus();
                            //        return;
                            //    }
                            //}

                            txt_admission_no.Text = adno;
                            #endregion
                            register_details(con);

                            dues_update_headwise_transaction.update_student_dues(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, "0", "0", con);

                            payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " registered a new student in session : " + ddlsession.SelectedItem.Text + " Student Name:- " + txt_firstname.Text + " Admission No : " + txt_admission_no.Text, con);
                            update_in_Scholarship_Registration_tbl(con);

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
                                string studentname = txt_firstname.Text + " " + txt_middlename.Text + " " + txt_lastname.Text;
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
                                string student_name = txt_firstname.Text;
                                if (txt_middlename.Text != "" && txt_middlename.Text != " " && txt_middlename.Text != "  " && txt_middlename.Text != "   ")
                                {
                                    student_name = student_name + " " + txt_middlename.Text;
                                }
                                if (txt_lastname.Text != "" && txt_lastname.Text != " " && txt_lastname.Text != "  " && txt_lastname.Text != "   ")
                                {
                                    student_name = student_name + " " + txt_lastname.Text;
                                }


                                string father_name = txt_father_first_name.Text;
                                if (txt_father_middle_name.Text != "" && txt_father_middle_name.Text != " " && txt_father_middle_name.Text != "  " && txt_father_middle_name.Text != "   ")
                                {
                                    father_name = father_name + " " + txt_father_middle_name.Text;
                                }
                                if (txt_father_last_name.Text != "" && txt_father_last_name.Text != " " && txt_father_last_name.Text != "  " && txt_father_last_name.Text != "   ")
                                {
                                    father_name = father_name + " " + txt_father_last_name.Text;
                                }
                                My.send_data_Create_ledger_for_student(txt_admission_no.Text, student_name, ddl_gender.Text, txt_dob.Text, txt_city.Text, txt_present_district.Text, txt_pincode.Text, txt_father_mobile.Text, txt_p_state.Text, father_name, txt_admission_date.Text);
                            }
                            catch (Exception ex)
                            {
                            }

                            Session["msG1"] = "Transfer process has been completed successfully";
                            if (ViewState["printslip"].ToString() == "1")
                            {
                                string url = "slip/Admission_Print.aspx?session_Id=" + ddlsession.SelectedValue + "&admission_no=" + txt_admission_no.Text;
                                Response.Redirect(url, false);
                            }
                            else
                            {
                                if (ViewState["IsTransferFormSale"].ToString() == "1")
                                {
                                    Response.Redirect("form-sale.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect("online-registration.aspx", false);
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewState["isSuccessS"] = "0";

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
                            if (ViewState["isSuccessS"].ToString() == "1")
                            {
                                My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " update student in session : " + ddlsession.SelectedItem.Text + " Student Name:- " + txt_firstname.Text + " Admission No : " + txt_admission_no.Text);
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
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Submit update");
            }
        }
        private void update_in_Scholarship_Registration_tbl(SqlConnection con)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Scholarship_Admission where Registration_id='" + ViewState["regiDs"].ToString() + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr["Gender"] = ddl_gender.Text;
                dr["DOB"] = txt_dob.Text;

                dr["Blood_group"] = ddl_blood_group.Text;
                dr["Religion"] = ddl_religion.SelectedItem.Text;
                dr["Father_mobile"] = txt_father_mobile.Text;
                dr["Mother_mobile"] = txt_mother_mobile_no.Text;

                dr["Student_first_name"] = txt_firstname.Text.Trim();
                dr["Student_middle_name"] = txt_middlename.Text.Trim();
                dr["Student_last_name"] = txt_lastname.Text.Trim();

                dr["Father_name"] = txt_father_first_name.Text + " " + txt_father_middle_name.Text + " " + txt_father_last_name.Text;
                dr["Father_first_name"] = txt_father_first_name.Text;
                dr["Father_middle_name"] = txt_father_middle_name.Text;
                dr["Father_last_name"] = txt_father_last_name.Text;

                dr["Mother_name"] = txt_mother_first_name.Text + " " + txt_mother_middle_name.Text + " " + txt_mother_last_name.Text;
                dr["Mother_first_name"] = txt_mother_first_name.Text;
                dr["Mother_middle_name"] = txt_mother_middle_name.Text;
                dr["Mother_last_name"] = txt_mother_last_name.Text;

                dr["Country_Code_Father"] = ddl_cunterycode1.Text;
                dr["Country_Code_Mother"] = ddl_cunterycode2.Text;

                dr["Is_transfer"] = "1";
                dr["Transfer_date"] = mycode.date();
                dr["Transfer_idate"] = mycode.idate();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }


        private void update_in_online_re_tbl(SqlConnection con)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Online_Admission where Registration_id='" + ViewState["regiDs"].ToString() + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr["Gender"] = ddl_gender.Text;
                dr["DOB"] = txt_dob.Text;

                dr["Blood_group"] = ddl_blood_group.Text;
                dr["Religion"] = ddl_religion.SelectedItem.Text;
                dr["Father_mobile"] = txt_father_mobile.Text;
                dr["Mother_mobile"] = txt_mother_mobile_no.Text;

                dr["Student_first_name"] = txt_firstname.Text.Trim();
                dr["Student_middle_name"] = txt_middlename.Text.Trim();
                dr["Student_last_name"] = txt_lastname.Text.Trim();

                dr["Father_name"] = txt_father_first_name.Text + " " + txt_father_middle_name.Text + " " + txt_father_last_name.Text;
                dr["Father_first_name"] = txt_father_first_name.Text;
                dr["Father_middle_name"] = txt_father_middle_name.Text;
                dr["Father_last_name"] = txt_father_last_name.Text;

                dr["Mother_name"] = txt_mother_first_name.Text + " " + txt_mother_middle_name.Text + " " + txt_mother_last_name.Text;
                dr["Mother_first_name"] = txt_mother_first_name.Text;
                dr["Mother_middle_name"] = txt_mother_middle_name.Text;
                dr["Mother_last_name"] = txt_mother_last_name.Text;

                dr["Country_Code_Father"] = ddl_cunterycode1.Text;
                dr["Country_Code_Mother"] = ddl_cunterycode2.Text;

                dr["Is_transfer"] = "1";
                dr["Transfer_date"] = mycode.date();
                dr["Transfer_idate"] = mycode.idate();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
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

        private bool check_roll_no(string roll)
        {
            DataTable dt = My.dataTable("select admissionserialnumber from admission_registor where rollnumber='" + roll + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + ddlsession.SelectedValue + "'");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
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

        protected void btn_cack_std_Click(object sender, EventArgs e)
        {
            process_active("1");
        }

        protected void btn_back_pre_school_Click(object sender, EventArgs e)
        {
            process_active("2");
        }

        protected void btn_back_father_Click(object sender, EventArgs e)
        {
            process_active("3");
        }

        protected void btn_back_mother_Click(object sender, EventArgs e)
        {
            process_active("5");
            process_active("4");
        }
        protected void btn_back_add_Click(object sender, EventArgs e)
        {
            process_active("5");
        }
        protected void btn_back_misc_Click(object sender, EventArgs e)
        {
            process_active("6");
        }

        protected void btn_back_doc_Click(object sender, EventArgs e)
        {
            process_active("7");
        }



        #region ProcessActiveDeactive
        private void process_active(string type)
        {
            ViewState["branchid"] = mycodeMy.get_branch_id(ViewState["Userid"].ToString());
            Session["Admin"] = Session["Admin"].ToString();

            if (type == "1")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");

                pro1.Attributes.Add("class", "steps-root");

                pronumS2.Attributes.Add("class", "steps-bx-number");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p");
                pro2.Attributes.Add("class", "steps-root");

                pronumS3.Attributes.Add("class", "steps-bx-number");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p");
                pro3.Attributes.Add("class", "steps-root");

                pronumS4.Attributes.Add("class", "steps-bx-number");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p");
                pro4.Attributes.Add("class", "steps-root");

                //pronumS5.Attributes.Add("class", "steps-bx-number");
                //prontxt5.Attributes.Add("class", "steps-bx-txt-p");
                //pro5.Attributes.Add("class", "steps-root");

                pronumS6.Attributes.Add("class", "steps-bx-number");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");
                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");



                pnl_academic_details.Visible = true;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "2")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root");

                pronumS3.Attributes.Add("class", "steps-bx-number");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p");
                pro3.Attributes.Add("class", "steps-root");

                pronumS4.Attributes.Add("class", "steps-bx-number");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p");
                pro4.Attributes.Add("class", "steps-root");

                //pronumS5.Attributes.Add("class", "steps-bx-number");
                //prontxt5.Attributes.Add("class", "steps-bx-txt-p");
                //pro5.Attributes.Add("class", "steps-root");

                pronumS6.Attributes.Add("class", "steps-bx-number");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");
                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_academic_details.Visible = false;
                pnl_student_details.Visible = true;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "3")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root");

                pronumS4.Attributes.Add("class", "steps-bx-number");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p");
                pro4.Attributes.Add("class", "steps-root");

                //pronumS5.Attributes.Add("class", "steps-bx-number");
                //prontxt5.Attributes.Add("class", "steps-bx-txt-p");
                //pro5.Attributes.Add("class", "steps-root");

                pronumS6.Attributes.Add("class", "steps-bx-number");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");
                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_academic_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = true;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "4")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root steps-root-done");

                pronumS4.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro4.Attributes.Add("class", "steps-root");

                //pronumS5.Attributes.Add("class", "steps-bx-number");
                //prontxt5.Attributes.Add("class", "steps-bx-txt-p");
                //pro5.Attributes.Add("class", "steps-root");

                pronumS6.Attributes.Add("class", "steps-bx-number");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");
                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_academic_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = true;
                pnl_mther_dt.Visible = true;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "5")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root steps-root-done");

                pronumS4.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro4.Attributes.Add("class", "steps-root steps-root-done");

                //pronumS5.Attributes.Add("class", "steps-bx-number stps-success-num");
                //prontxt5.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                //pro5.Attributes.Add("class", "steps-root");

                pronumS6.Attributes.Add("class", "steps-bx-number");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");
                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_academic_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = true;
                pnl_mther_dt.Visible = true;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "6")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root steps-root-done");

                pronumS4.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro4.Attributes.Add("class", "steps-root steps-root-done");

                //pronumS5.Attributes.Add("class", "steps-bx-number stps-success-num");
                //prontxt5.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                //pro5.Attributes.Add("class", "steps-root steps-root-done");

                pronumS6.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro6.Attributes.Add("class", "steps-root");

                pronumS7.Attributes.Add("class", "steps-bx-number");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p");
                pro7.Attributes.Add("class", "steps-root");
                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_academic_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = true;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = false;
            }
            else if (type == "7")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root steps-root-done");

                pronumS4.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro4.Attributes.Add("class", "steps-root steps-root-done");

                //pronumS5.Attributes.Add("class", "steps-bx-number stps-success-num");
                //prontxt5.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                //pro5.Attributes.Add("class", "steps-root steps-root-done");

                pronumS6.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro6.Attributes.Add("class", "steps-root steps-root-done");

                pronumS7.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro7.Attributes.Add("class", "steps-root");
                pronumS8.Attributes.Add("class", "steps-bx-number");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p");

                pnl_academic_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = true;
                pnl_docs.Visible = false;
            }
            else if (type == "8" || type == "9")
            {
                pronumS1.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt1.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro1.Attributes.Add("class", "steps-root steps-root-done");

                pronumS2.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt2.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro2.Attributes.Add("class", "steps-root steps-root-done");

                pronumS3.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt3.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro3.Attributes.Add("class", "steps-root steps-root-done");

                pronumS4.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt4.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro4.Attributes.Add("class", "steps-root steps-root-done");

                //pronumS5.Attributes.Add("class", "steps-bx-number stps-success-num");
                //prontxt5.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                //pro5.Attributes.Add("class", "steps-root steps-root-done");

                pronumS6.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt6.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro6.Attributes.Add("class", "steps-root steps-root-done");

                pronumS7.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt7.Attributes.Add("class", "steps-bx-txt-p stps-success-name");
                pro7.Attributes.Add("class", "steps-root steps-root-done");

                pronumS8.Attributes.Add("class", "steps-bx-number stps-success-num");
                prontxt8.Attributes.Add("class", "steps-bx-txt-p stps-success-name");

                pnl_academic_details.Visible = false;
                pnl_student_details.Visible = false;
                pn_prev_info.Visible = false;
                pnl_father_dt.Visible = false;
                pnl_mther_dt.Visible = false;
                pnl_address_dt.Visible = false;
                pnl_misc_dt.Visible = false;
                pnl_docs.Visible = true;
            }
        }


        public void chnage_color_fisrt_button()
        {
            if (txt_firstname.Text == "")
            {
                //txt_first_name.BackColor = HexColor("#f91111");
                txt_firstname.BorderColor = HexColor("#f91111");
            }
            else
            {
                txt_firstname.BackColor = HexColor("#ffffff");
                txt_firstname.BorderColor = HexColor("#000000");

            }
            if (txt_dob.Text == "")
            {
                txt_dob.BorderColor = HexColor("#f91111");
            }
            else
            {
                txt_dob.BackColor = HexColor("#ffffff");
                txt_dob.BorderColor = HexColor("#000000");

            }

            if (ddl_cast_category.Text == "Select")
            {
                ddl_cast_category.BorderColor = HexColor("#f91111");

            }
            else
            {

                ddl_cast_category.BackColor = HexColor("#ffffff");
                ddl_cast_category.BorderColor = HexColor("#000000");
            }


            //if (rd_day.Checked == false && rd_hostel.Checked == false && rd_dayboarding.Checked == false)
            //{
            //    rd_day.BorderColor = HexColor("#f91111");
            //    rd_hostel.BorderColor = HexColor("#f91111");
            //    rd_dayboarding.BorderColor = HexColor("#f91111");
            //}
            //else
            //{
            //    rd_day.BackColor = HexColor("#ffffff");
            //    rd_hostel.BackColor = HexColor("#ffffff");
            //    rd_dayboarding.BackColor = HexColor("#ffffff");


            //}


            if (ddlclass.SelectedItem.Text == "Select")
            {
                ddlclass.BorderColor = HexColor("#f91111");
            }
            else
            {
                ddlclass.BackColor = HexColor("#ffffff");
                ddlclass.BorderColor = HexColor("#000000");
            }


            //------------------Father’s Contact Details-------------------------------------------------


            if (txt_father_first_name.Text == "")
            {
                txt_father_first_name.BorderColor = HexColor("#f91111");
            }
            else
            {
                txt_father_first_name.BackColor = HexColor("#ffffff");
                txt_father_first_name.BorderColor = HexColor("#000000");

            }

            if (txt_father_mobile.Text == "")
            {
                txt_father_mobile.BorderColor = HexColor("#f91111");
            }
            else
            {
                txt_father_mobile.BackColor = HexColor("#ffffff");
                txt_father_mobile.BorderColor = HexColor("#000000");
            }

            //if (txt_annual_income.Text == "")
            //{
            //    txt_annual_income.BorderColor = HexColor("#f91111");

            //}

            //else
            //{
            //    txt_annual_income.BackColor = HexColor("#ffffff");
            //    txt_annual_income.BorderColor = HexColor("#000000");
            //}

            if (txt_mother_first_name.Text == "")
            {
                txt_mother_first_name.BorderColor = HexColor("#f91111");

            }
            //-------------txt_Mother_name------------------
            else
            {
                txt_mother_first_name.BackColor = HexColor("#ffffff");
                txt_mother_first_name.BorderColor = HexColor("#000000");
            }


            if (txt_mother_first_name.Text == "")
            {
                txt_mother_first_name.BorderColor = HexColor("#f91111");

            }

            else
            {
                txt_mother_first_name.BackColor = HexColor("#ffffff");
                txt_mother_first_name.BorderColor = HexColor("#000000");
            }
        }


        private Color HexColor(string hex)
        {
            //remove the # at the front
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(a, r, g, b);
        }

        protected void ddl_prev_board_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_board_list();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_board_list()
        {
            comP.bind_ddl(ddl_board_list, "select Board_name from Board_details where Type='" + ddl_prev_board_type.Text + "'");
        }
        #endregion

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
            }
            catch (Exception ex)
            {
            }
        }

        My mycodeMy = new My();
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

        private void register_details(SqlConnection con)
        {
            if (payments.IsUserExistS("select Id from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "'", con))
            {
                string student_names = txt_firstname.Text;
                if (txt_middlename.Text != "" && txt_middlename.Text != " " && txt_middlename.Text != "  " && txt_middlename.Text != "   ")
                {
                    student_names = student_names + " " + txt_middlename.Text;
                }
                if (txt_lastname.Text != "" && txt_lastname.Text != " " && txt_lastname.Text != "  " && txt_lastname.Text != "   ")
                {
                    student_names = student_names + " " + txt_lastname.Text;
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
                    else
                    {

                    }
                }
                catch
                {
                    pwd = My.create_random_no_otp();
                }





                ViewState["pwd"] = pwd;
                SqlCommand cmd;
                string query = "INSERT INTO admission_registor (Transfer_Status,Category_id,SubCategory_id,formserialnumber,UID_No,Index_no,dateofadmission,admission_idate,session,is_applied_dayboarding,day_boarding_with_lunch,class,rollnumber,Section,admissionserialnumber,house,hosteltaken,studentname,dob,place_of_birth,Is_birth_certificate,birth_certificate_number,gender,blood_group,Student_nationality,religion,ration_type,cast,Is_cast_certificate,cast_certificate_no,aadharno,student_mother_tounge,is_illness,illness_remark,Staff_employee_code,RTE,staff_ward,Staff_name,Personal_Identymarks,identifacationmark,Prev_school_name,currentschool,Old_Admission_Date,OLd_Admission_Idate,Prev_board_type,Prev_board,Old_class_id,Prev_percentage,Prev_reason_for_shift,Prev_year,fathername,occuption,fatherqualification,f_nationality,f_marrital_statue,Country_Code_Father,father_mob,Father_whatsapp_country_code,Father_whatsApp_no,email_id,guardianname,parentincome,mothername,m_occupation,motherqualifiaction,m_nationality,m_marrital_statue,Country_Code_Mother,mother_mob,Mother_whatsapp_country_code,Mother_whatsApp_no,mother_email,careof,postoffice,policestation,district,city,state,pin,Present_country,Country_Code_Current_add,mobilenumber,careof_permanent,postoffice_permanent,policestation_permanent,district_permanent,city_permanent,state_permanent,pincode,Permanent_country,Country_Code_Current_Perm_add,mob2,Bank_acount_no,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,payment_status,Hostel_id,Session_id,Class_id,Is_TC_Taken,Student_id,Branch_id,Student_Name_First,Student_Middle_Name,Student_Name_Last,Father_Name_First,Father_Name_Middle,Father_Name_Last,Mother_Name_First,Mother_Name_Middle,Mother_Name_Last,StudentStatus,Pwd,Verification_Istatus,Status,College_School_Name,relation,transportationtaken,Father_aadhar_no,Mother_aadhar_no,Height,Weight,Sibling_name1,Sibling_age1,Sibling_school1,Sibling_class1,Sibling_name2,Sibling_age2,Sibling_school2,Sibling_class2,Created_by,Created_date,Created_time,Created_idate,Student_pen_no,jati,Hobbie_of_student,Prev_class_attended,Prev_pass_fail_status,Father_age,Mother_age,Mother_annual_income,Guardian_relation_with_student,Guardian_occupation,Guardian_qualification,Guardian_contry_code,Guardian_mobile_no,Guardian_aadhar_no,Guardian_annual_income,Guardian_address) values (@Transfer_Status,@Category_id,@SubCategory_id,@formserialnumber,@UID_No,@Index_no,@dateofadmission,@admission_idate,@session,@is_applied_dayboarding,@day_boarding_with_lunch,@class,@rollnumber,@Section,@admissionserialnumber,@house,@hosteltaken,@studentname,@dob,@place_of_birth,@Is_birth_certificate,@birth_certificate_number,@gender,@blood_group,@Student_nationality,@religion,@ration_type,@cast,@Is_cast_certificate,@cast_certificate_no,@aadharno,@student_mother_tounge,@is_illness,@illness_remark,@Staff_employee_code,@RTE,@staff_ward,@Staff_name,@Personal_Identymarks,@identifacationmark,@Prev_school_name,@currentschool,@Old_Admission_Date,@OLd_Admission_Idate,@Prev_board_type,@Prev_board,@Old_class_id,@Prev_percentage,@Prev_reason_for_shift,@Prev_year,@fathername,@occuption,@fatherqualification,@f_nationality,@f_marrital_statue,@Country_Code_Father,@father_mob,@Father_whatsapp_country_code,@Father_whatsApp_no,@email_id,@guardianname,@parentincome,@mothername,@m_occupation,@motherqualifiaction,@m_nationality,@m_marrital_statue,@Country_Code_Mother,@mother_mob,@Mother_whatsapp_country_code,@Mother_whatsApp_no,@mother_email,@careof,@postoffice,@policestation,@district,@city,@state,@pin,@Present_country,@Country_Code_Current_add,@mobilenumber,@careof_permanent,@postoffice_permanent,@policestation_permanent,@district_permanent,@city_permanent,@state_permanent,@pincode,@Permanent_country,@Country_Code_Current_Perm_add,@mob2,@Bank_acount_no,@Account_Holder_name,@Bnk_Name,@IFSC_Code,@Branch_Name,@payment_status,@Hostel_id,@Session_id,@Class_id,@Is_TC_Taken,@Student_id,@Branch_id,@Student_Name_First,@Student_Middle_Name,@Student_Name_Last,@Father_Name_First,@Father_Name_Middle,@Father_Name_Last,@Mother_Name_First,@Mother_Name_Middle,@Mother_Name_Last,@StudentStatus,@Pwd,@Verification_Istatus,@Status,@College_School_Name,@relation,@transportationtaken,@Father_aadhar_no,@Mother_aadhar_no,@Height,@Weight,@Sibling_name1,@Sibling_age1,@Sibling_school1,@Sibling_class1,@Sibling_name2,@Sibling_age2,@Sibling_school2,@Sibling_class2,@Created_by,@Created_date,@Created_time,@Created_idate,@Student_pen_no,@jati,@Hobbie_of_student,@Prev_class_attended,@Prev_pass_fail_status,@Father_age,@Mother_age,@Mother_annual_income,@Guardian_relation_with_student,@Guardian_occupation,@Guardian_qualification,@Guardian_contry_code,@Guardian_mobile_no,@Guardian_aadhar_no,@Guardian_annual_income,@Guardian_address)";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Hobbie_of_student", txt_hobbies.Text);
                cmd.Parameters.AddWithValue("@Prev_class_attended", txt_prev_last_class_attended.Text);
                cmd.Parameters.AddWithValue("@Prev_pass_fail_status", ddl_prev_pass_fail_status.Text);
                cmd.Parameters.AddWithValue("@Father_age", txt_father_age.Text);
                cmd.Parameters.AddWithValue("@Mother_age", txt_mother_age.Text);
                cmd.Parameters.AddWithValue("@Mother_annual_income", txt_mother_annual_income.Text);
                cmd.Parameters.AddWithValue("@Guardian_relation_with_student", txt_relation_with_student.Text);
                cmd.Parameters.AddWithValue("@Guardian_occupation", ddl_guardian_occupation.Text);
                cmd.Parameters.AddWithValue("@Guardian_qualification", ddl_guardian_qualification.Text);
                cmd.Parameters.AddWithValue("@Guardian_contry_code", ddl_guardian_contry_code.Text);
                cmd.Parameters.AddWithValue("@Guardian_mobile_no", txt_guardian_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Guardian_aadhar_no", txt_guardian_aadhar_no.Text);
                cmd.Parameters.AddWithValue("@Guardian_annual_income", txt_guardian_annual_income.Text);
                cmd.Parameters.AddWithValue("@Guardian_address", txt_guardian_address.Text);

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

                cmd.Parameters.AddWithValue("@formserialnumber", txt_form_slno.Text);
                cmd.Parameters.AddWithValue("@UID_No", txt_uid.Text);
                cmd.Parameters.AddWithValue("@Index_no", txt_index_no.Text);

                cmd.Parameters.AddWithValue("@Student_pen_no", txt_student_pen_no.Text);
                cmd.Parameters.AddWithValue("@dateofadmission", admission_date);
                cmd.Parameters.AddWithValue("@admission_idate", admission_idate);
                cmd.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);
                if (rdb_hostel.Checked == true)
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
                cmd.Parameters.AddWithValue("@studentname", student_names);
                cmd.Parameters.AddWithValue("@dob", date_of_birth);
                cmd.Parameters.AddWithValue("@place_of_birth", txt_place_of_birth.Text);
                cmd.Parameters.AddWithValue("@Is_birth_certificate", ddl_is_birth_certificate.Text);
                cmd.Parameters.AddWithValue("@birth_certificate_number", txt_birth_certificate_no.Text);
                cmd.Parameters.AddWithValue("@gender", ddl_gender.Text);
                cmd.Parameters.AddWithValue("@blood_group", ddl_blood_group.Text);
                cmd.Parameters.AddWithValue("@Student_nationality", ddl_nationality.Text);
                cmd.Parameters.AddWithValue("@religion", ddl_religion.Text);
                cmd.Parameters.AddWithValue("@ration_type", ddl_ration_cards_types.Text);
                cmd.Parameters.AddWithValue("@cast", ddl_cast_category.Text);
                cmd.Parameters.AddWithValue("@jati", ddl_caste_jati.Text);
                cmd.Parameters.AddWithValue("@Is_cast_certificate", ddl_is_cast_certificate.Text);
                cmd.Parameters.AddWithValue("@cast_certificate_no", txt_cast_certificate_no.Text);
                cmd.Parameters.AddWithValue("@aadharno", txt_aadharno_mark.Text);
                cmd.Parameters.AddWithValue("@student_mother_tounge", ddl_student_mother_tongue.Text);
                cmd.Parameters.AddWithValue("@is_illness", ddl_illness.Text);
                cmd.Parameters.AddWithValue("@illness_remark", txt_illness_remark.Text);
                cmd.Parameters.AddWithValue("@Staff_employee_code", txt_employee_code.Text);
                cmd.Parameters.AddWithValue("@RTE", ddl_rte.Text);
                cmd.Parameters.AddWithValue("@staff_ward", ddl_staff_ward.Text);
                cmd.Parameters.AddWithValue("@Staff_name", txt_staff_name.Text);
                cmd.Parameters.AddWithValue("@Personal_Identymarks", txt_identification_marks.Text);
                cmd.Parameters.AddWithValue("@identifacationmark", txt_identification_marks.Text);





                cmd.Parameters.AddWithValue("@Height", txt_height.Text);
                cmd.Parameters.AddWithValue("@Weight", txt_weight.Text);
                cmd.Parameters.AddWithValue("@Sibling_name1", txt_name_of_sibling1.Text);
                cmd.Parameters.AddWithValue("@Sibling_age1", txt_age_sibling1.Text);
                cmd.Parameters.AddWithValue("@Sibling_school1", txt_school_sibling1.Text);
                cmd.Parameters.AddWithValue("@Sibling_class1", ddl_class_sb1.Text);
                cmd.Parameters.AddWithValue("@Sibling_name2", txt_name_of_sibling2.Text);
                cmd.Parameters.AddWithValue("@Sibling_age2", txt_age_sibling2.Text);
                cmd.Parameters.AddWithValue("@Sibling_school2", txt_school_sibling2.Text);
                cmd.Parameters.AddWithValue("@Sibling_class2", ddl_class_sb2.Text);


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
                cmd.Parameters.AddWithValue("@fathername", txt_father_first_name.Text + " " + txt_father_middle_name.Text + " " + txt_father_last_name.Text);
                cmd.Parameters.AddWithValue("@occuption", ddl_occupation.Text);
                cmd.Parameters.AddWithValue("@fatherqualification", ddl_father_qualification.Text);
                cmd.Parameters.AddWithValue("@f_nationality", ddl_father_nationality.Text);
                cmd.Parameters.AddWithValue("@f_marrital_statue", ddl_maritial_status.Text);
                cmd.Parameters.AddWithValue("@Country_Code_Father", ddl_cunterycode1.Text);
                cmd.Parameters.AddWithValue("@father_mob", txt_father_mobile.Text);
                cmd.Parameters.AddWithValue("@Father_whatsapp_country_code", ddl_fthr_whatsapp_c_Code.Text);
                cmd.Parameters.AddWithValue("@Father_whatsApp_no", txt_father_whatsapp_no.Text);
                cmd.Parameters.AddWithValue("@email_id", txt_guardian_email.Text);
                cmd.Parameters.AddWithValue("@guardianname", txt_guardian_name.Text);
                cmd.Parameters.AddWithValue("@parentincome", txt_annual_income.Text);
                cmd.Parameters.AddWithValue("@Father_aadhar_no", txt_father_aadhar_no.Text);

                //Mother Details
                cmd.Parameters.AddWithValue("@mothername", txt_mother_first_name.Text + " " + txt_mother_middle_name.Text + " " + txt_mother_last_name.Text);
                cmd.Parameters.AddWithValue("@m_occupation", ddl_m_occupation.Text);
                cmd.Parameters.AddWithValue("@motherqualifiaction", ddl_mother_qualification.Text);
                cmd.Parameters.AddWithValue("@m_nationality", ddl_mother_nationality.Text);
                cmd.Parameters.AddWithValue("@m_marrital_statue", ddl_m_maritial_status.Text);
                cmd.Parameters.AddWithValue("@Country_Code_Mother", ddl_cunterycode2.Text);
                cmd.Parameters.AddWithValue("@mother_mob", txt_mother_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Mother_whatsapp_country_code", ddl_mthr_whatsapp_c_Code.Text);
                cmd.Parameters.AddWithValue("@Mother_whatsApp_no", txt_mother_whatsapp_no.Text);
                cmd.Parameters.AddWithValue("@mother_email", txt_mother_emailid.Text);
                cmd.Parameters.AddWithValue("@Mother_aadhar_no", txt_mother_aadhar_no.Text);


                //Present Address Details
                cmd.Parameters.AddWithValue("@careof", txt_adress.Text);
                cmd.Parameters.AddWithValue("@postoffice", txt_present_po.Text);
                cmd.Parameters.AddWithValue("@policestation", txt_temp_ps.Text);
                cmd.Parameters.AddWithValue("@district", txt_present_district.Text);
                cmd.Parameters.AddWithValue("@city", txt_city.Text);
                cmd.Parameters.AddWithValue("@state", ddl_temp_state.Text);
                cmd.Parameters.AddWithValue("@pin", txt_pincode.Text);
                cmd.Parameters.AddWithValue("@Present_country", ddl_country_c.Text);
                cmd.Parameters.AddWithValue("@Country_Code_Current_add", ddl_cunterycode3.Text);
                cmd.Parameters.AddWithValue("@mobilenumber", txt_temp_mobileno.Text);

                //Permanent Address Details
                cmd.Parameters.AddWithValue("@careof_permanent", txt_pAddress.Text);
                cmd.Parameters.AddWithValue("@postoffice_permanent", txt_perma_po.Text);
                cmd.Parameters.AddWithValue("@policestation_permanent", txt_par_ps.Text);
                cmd.Parameters.AddWithValue("@district_permanent", txt_perma_disctrict.Text);
                cmd.Parameters.AddWithValue("@city_permanent", txt_pcity.Text);
                cmd.Parameters.AddWithValue("@state_permanent", ddl_par_state.Text);
                cmd.Parameters.AddWithValue("@pincode", txt_Ppincod.Text);
                cmd.Parameters.AddWithValue("@Permanent_country", ddl_country_p.Text);
                cmd.Parameters.AddWithValue("@Country_Code_Current_Perm_add", ddl_cunterycode4.Text);
                cmd.Parameters.AddWithValue("@mob2", txt_p_mobile_no.Text);



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

                cmd.Parameters.AddWithValue("@Student_Name_First", txt_firstname.Text.Trim());
                cmd.Parameters.AddWithValue("@Student_Middle_Name", txt_middlename.Text.Trim());
                cmd.Parameters.AddWithValue("@Student_Name_Last", txt_lastname.Text.Trim());

                cmd.Parameters.AddWithValue("@Father_Name_First", txt_father_first_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Father_Name_Middle", txt_father_middle_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Father_Name_Last", txt_father_last_name.Text.Trim());

                cmd.Parameters.AddWithValue("@Mother_Name_First", txt_mother_first_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Mother_Name_Middle", txt_mother_middle_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Mother_Name_Last", txt_mother_last_name.Text.Trim());

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


                    if (ViewState["IsTransfer"].ToString() == "1")
                    {
                        reg_ids = ViewState["regiDs"].ToString();
                    }
                    else if (ViewState["IsTransfer"].ToString() == "2")//Scholarship
                    {
                        reg_ids = ViewState["regiDs"].ToString();
                        payments.exeSql("update Scholarship_Admission set Admission_no='" + txt_admission_no.Text + "', Admission_no_updated_on='" + mycode.date() + "',Is_admission_no_updated='1',Admission_date='" + txt_admission_date.Text + "', Admission_idate='" + My.DateConvertToIdate(txt_admission_date.Text) + "',Admission_time='" + mycode.time() + "' where Registration_id='" + ViewState["regiDs"].ToString() + "'", con);
                    }
                    if (ViewState["IsTransferFormSale"].ToString() == "1")
                    {
                        payments.exeSql("update Form_sale_details set Admission_no='" + txt_admission_no.Text + "', Admission_no_updated_on='" + mycode.date() + "',Is_admission_no_updated='1',Admission_date='" + txt_admission_date.Text + "', Admission_idate='" + My.DateConvertToIdate(txt_admission_date.Text) + "',Admission_time='" + mycode.time() + "' where Id='" + ViewState["trnsferFrmSlaeId"].ToString() + "'", con);
                    }

                    if (ddl_day_boarding.SelectedValue == "3")
                    {
                        save_hostel_data(con);
                    }
                    else
                    {
                        if (ddl_istransport_assign.SelectedValue == "1")
                        {
                            save_transport_data(con);
                        }
                    }

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

        private void save_transport_data(SqlConnection con)
        {
            string session = ddlsession.SelectedItem.Text;
            string tranportassinedid = payments.get_transport_assigned_id(con);
            string cunrt_session = session;
            string session_frst_year = cunrt_session.Substring(0, 4);
            int s_year = My.toint(session_frst_year);
            string monthid = My.tomonth_numberstring(ddl_trns_month.SelectedItem.Text);
            int pay_month = My.toint(monthid);
            string final = s_year.ToString() + monthid;


            DataTable dt = payments.dataTable("select * from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id='" + ddl_trns_route.SelectedValue + "' and Transportation_Id='" + ddl_trns_vehicle.SelectedValue + "' and Sheet_Status='0'", con);
            if (dt.Rows.Count > 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Student_mapping_with_TransportPath (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,transport_id,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Academic_Sem_or_Year_id,Mapping_Year,Sheet_Id,Boarding_Point_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@transport_id,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Academic_Sem_or_Year_id,@Mapping_Year,@Sheet_Id,@Boarding_Point_id); INSERT INTO Student_mapping_with_TransportPath_history (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,transport_id,Remark,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Update_Status,Academic_Sem_or_Year_id,Mapping_Year,Sheet_Id,Boarding_Point_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@transport_id,@Remark,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Update_Status,@Academic_Sem_or_Year_id,@Mapping_Year,@Sheet_Id,@Boarding_Point_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);
                cmd.Parameters.AddWithValue("@Month_name", ddl_trns_month.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Month_id", monthid);
                cmd.Parameters.AddWithValue("@TransportPath_id", ddl_trns_route.SelectedValue);
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
                cmd.Parameters.AddWithValue("@transport_id", ddl_trns_vehicle.SelectedValue);
                cmd.Parameters.AddWithValue("@Boarding_Point_id", ddl_trns_boarding_point.SelectedValue);
                if (payments.InsertUpdateData(cmd, con))
                {
                    try
                    {
                        SqlCommand cmd1;
                        string query1 = "Update admission_registor set Hostel_id=@Hostel_id,Transportation_Id=@Transportation_Id,Transportationpath=@Transportationpath,transportationtaken=@transportationtaken,hosteltaken=@hosteltaken where admissionserialnumber=@admissionserialnumber and Session_id=@Session_id";
                        cmd1 = new SqlCommand(query1);
                        cmd1.Parameters.AddWithValue("@Transportation_Id", ddl_trns_vehicle.SelectedValue);
                        cmd1.Parameters.AddWithValue("@Transportationpath", ddl_trns_route.SelectedValue);
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
                    payments.exeSql("update admission_registor set hosteltaken='Yes',transportationtaken='No',Hostel_id='" + ddl_hostel.SelectedValue + "',Hostel_assignD_id='" + hostel_assign_id + "' where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsession.SelectedValue + "' ", con);
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


        #region  UPDATE
        private void update_registration(SqlConnection con)
        {
            if (txt_rollnumber.Text == "" || txt_rollnumber.Text == "0") { }
            else
            {
                string roll_no = txt_rollnumber.Text;
                while (!check_roll_no_on_update(roll_no, HdID.Value, con))
                {
                    Alertme("Sorry! Duplicate Roll No.", "warning");
                    txt_rollnumber.Focus();
                    return;
                }
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


            string student_name = txt_firstname.Text;
            if (txt_middlename.Text != "" && txt_middlename.Text != " " && txt_middlename.Text != "  " && txt_middlename.Text != "   ")
            {
                student_name = student_name + " " + txt_middlename.Text;
            }
            if (txt_lastname.Text != "" && txt_lastname.Text != " " && txt_lastname.Text != "  " && txt_lastname.Text != "   ")
            {
                student_name = student_name + " " + txt_lastname.Text;
            }


            SqlCommand cmd;
            string query = "Update admission_registor set Student_pen_no=@Student_pen_no,Transfer_Status=@Transfer_Status,Category_id=@Category_id,SubCategory_id=@SubCategory_id,formserialnumber=@formserialnumber,UID_No=@UID_No,Index_no=@Index_no,dateofadmission=@dateofadmission,admission_idate=@admission_idate,session=@session,class=@class,rollnumber=@rollnumber,Section=@Section,house=@house,studentname=@studentname,dob=@dob,place_of_birth=@place_of_birth,Is_birth_certificate=@Is_birth_certificate,birth_certificate_number=@birth_certificate_number,gender=@gender,blood_group=@blood_group,Student_nationality=@Student_nationality,religion=@religion,ration_type=@ration_type,cast=@cast,Is_cast_certificate=@Is_cast_certificate,cast_certificate_no=@cast_certificate_no,aadharno=@aadharno,student_mother_tounge=@student_mother_tounge,is_illness=@is_illness,illness_remark=@illness_remark,Staff_employee_code=@Staff_employee_code,RTE=@RTE,staff_ward=@staff_ward,Staff_name=@Staff_name,Personal_Identymarks=@Personal_Identymarks,identifacationmark=@identifacationmark,Prev_school_name=@Prev_school_name,currentschool=@currentschool,Old_Admission_Date=@Old_Admission_Date,OLd_Admission_Idate=@OLd_Admission_Idate,Prev_board_type=@Prev_board_type,Prev_board=@Prev_board,Old_class_id=@Old_class_id,Prev_percentage=@Prev_percentage,Prev_reason_for_shift=@Prev_reason_for_shift,Prev_year=@Prev_year,fathername=@fathername,occuption=@occuption,fatherqualification=@fatherqualification,f_nationality=@f_nationality,f_marrital_statue=@f_marrital_statue,Country_Code_Father=@Country_Code_Father,father_mob=@father_mob,Father_whatsapp_country_code=@Father_whatsapp_country_code,Father_whatsApp_no=@Father_whatsApp_no,email_id=@email_id,guardianname=@guardianname,parentincome=@parentincome,mothername=@mothername,m_occupation=@m_occupation,motherqualifiaction=@motherqualifiaction,m_nationality=@m_nationality,m_marrital_statue=@m_marrital_statue,Country_Code_Mother=@Country_Code_Mother,mother_mob=@mother_mob,Mother_whatsapp_country_code=@Mother_whatsapp_country_code,Mother_whatsApp_no=@Mother_whatsApp_no,mother_email=@mother_email,careof=@careof,postoffice=@postoffice,policestation=@policestation,district=@district,city=@city,state=@state,pin=@pin,Present_country=@Present_country,Country_Code_Current_add=@Country_Code_Current_add,mobilenumber=@mobilenumber,careof_permanent=@careof_permanent,postoffice_permanent=@postoffice_permanent,policestation_permanent=@policestation_permanent,district_permanent=@district_permanent,city_permanent=@city_permanent,state_permanent=@state_permanent,pincode=@pincode,Permanent_country=@Permanent_country,Country_Code_Current_Perm_add=@Country_Code_Current_Perm_add,mob2=@mob2,Bank_acount_no=@Bank_acount_no,Account_Holder_name=@Account_Holder_name,Bnk_Name=@Bnk_Name,IFSC_Code=@IFSC_Code,Branch_Name=@Branch_Name,Session_id=@Session_id,Class_id=@Class_id,Student_Name_First=@Student_Name_First,Student_Middle_Name=@Student_Middle_Name,Student_Name_Last=@Student_Name_Last,Father_Name_First=@Father_Name_First,Father_Name_Middle=@Father_Name_Middle,Father_Name_Last=@Father_Name_Last,Mother_Name_First=@Mother_Name_First,Mother_Name_Middle=@Mother_Name_Middle,Mother_Name_Last=@Mother_Name_Last,College_School_Name=@College_School_Name,Father_aadhar_no=@Father_aadhar_no,Mother_aadhar_no=@Mother_aadhar_no,Height=@Height,Weight=@Weight,Sibling_name1=@Sibling_name1,Sibling_age1=@Sibling_age1,Sibling_school1=@Sibling_school1,Sibling_class1=@Sibling_class1,Sibling_name2=@Sibling_name2,Sibling_age2=@Sibling_age2,Sibling_school2=@Sibling_school2,Sibling_class2=@Sibling_class2,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate,jati=@jati,Hobbie_of_student=@Hobbie_of_student,Prev_class_attended=@Prev_class_attended,Prev_pass_fail_status=@Prev_pass_fail_status,Father_age=@Father_age,Mother_age=@Mother_age,Mother_annual_income=@Mother_annual_income,Guardian_relation_with_student=@Guardian_relation_with_student,Guardian_occupation=@Guardian_occupation,Guardian_qualification=@Guardian_qualification,Guardian_contry_code=@Guardian_contry_code,Guardian_mobile_no=@Guardian_mobile_no,Guardian_aadhar_no=@Guardian_aadhar_no,Guardian_annual_income=@Guardian_annual_income,Guardian_address=@Guardian_address where Id=@Id";
            cmd = new SqlCommand(query);
            //Academic Details
            cmd.Parameters.AddWithValue("@Id", HdID.Value);
            cmd.Parameters.AddWithValue("@Hobbie_of_student", txt_hobbies.Text);
            cmd.Parameters.AddWithValue("@Prev_class_attended", txt_prev_last_class_attended.Text);
            cmd.Parameters.AddWithValue("@Prev_pass_fail_status", ddl_prev_pass_fail_status.Text);
            cmd.Parameters.AddWithValue("@Father_age", txt_father_age.Text);
            cmd.Parameters.AddWithValue("@Mother_age", txt_mother_age.Text);
            cmd.Parameters.AddWithValue("@Mother_annual_income", txt_mother_annual_income.Text);
            cmd.Parameters.AddWithValue("@Guardian_relation_with_student", txt_relation_with_student.Text);
            cmd.Parameters.AddWithValue("@Guardian_occupation", ddl_guardian_occupation.Text);
            cmd.Parameters.AddWithValue("@Guardian_qualification", ddl_guardian_qualification.Text);
            cmd.Parameters.AddWithValue("@Guardian_contry_code", ddl_guardian_contry_code.Text);
            cmd.Parameters.AddWithValue("@Guardian_mobile_no", txt_guardian_mobile_no.Text);
            cmd.Parameters.AddWithValue("@Guardian_aadhar_no", txt_guardian_aadhar_no.Text);
            cmd.Parameters.AddWithValue("@Guardian_annual_income", txt_guardian_annual_income.Text);
            cmd.Parameters.AddWithValue("@Guardian_address", txt_guardian_address.Text);

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

            cmd.Parameters.AddWithValue("@formserialnumber", txt_form_slno.Text);
            cmd.Parameters.AddWithValue("@UID_No", txt_uid.Text);
            cmd.Parameters.AddWithValue("@Index_no", txt_index_no.Text);
            cmd.Parameters.AddWithValue("@Student_pen_no", txt_student_pen_no.Text);
            cmd.Parameters.AddWithValue("@dateofadmission", admission_date);
            cmd.Parameters.AddWithValue("@admission_idate", admission_idate);
            cmd.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);

            //if (ddl_day_boarding.SelectedValue != "0")
            //{
            //    if (ddl_day_boarding.SelectedValue == "1")
            //    {
            //        cmd.Parameters.AddWithValue("@is_applied_dayboarding", true);
            //        cmd.Parameters.AddWithValue("@day_boarding_with_lunch", false);
            //    }
            //    else if (ddl_day_boarding.SelectedIndex == 2)
            //    {
            //        cmd.Parameters.AddWithValue("@is_applied_dayboarding", false);
            //        cmd.Parameters.AddWithValue("@day_boarding_with_lunch", true);
            //    }
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@is_applied_dayboarding", false);
            //    cmd.Parameters.AddWithValue("@day_boarding_with_lunch", false);
            //}
            cmd.Parameters.AddWithValue("@class", ddlclass.SelectedItem.Text);


            cmd.Parameters.AddWithValue("@rollnumber", txt_rollnumber.Text);
            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);

            //cmd.Parameters.AddWithValue("@admissionserialnumber", txt_admission_no.Text);
            cmd.Parameters.AddWithValue("@house", ddl_house.SelectedValue);


            //Student Info 
            cmd.Parameters.AddWithValue("@studentname", student_name);
            cmd.Parameters.AddWithValue("@dob", date_of_birth);
            cmd.Parameters.AddWithValue("@place_of_birth", txt_place_of_birth.Text);
            cmd.Parameters.AddWithValue("@Is_birth_certificate", ddl_is_birth_certificate.Text);
            cmd.Parameters.AddWithValue("@birth_certificate_number", txt_birth_certificate_no.Text);
            cmd.Parameters.AddWithValue("@gender", ddl_gender.Text);
            cmd.Parameters.AddWithValue("@blood_group", ddl_blood_group.Text);
            cmd.Parameters.AddWithValue("@Student_nationality", ddl_nationality.Text);
            cmd.Parameters.AddWithValue("@religion", ddl_religion.Text);
            cmd.Parameters.AddWithValue("@ration_type", ddl_ration_cards_types.Text);
            cmd.Parameters.AddWithValue("@cast", ddl_cast_category.Text);
            cmd.Parameters.AddWithValue("@jati", ddl_caste_jati.Text);
            cmd.Parameters.AddWithValue("@Is_cast_certificate", ddl_is_cast_certificate.Text);
            cmd.Parameters.AddWithValue("@cast_certificate_no", txt_cast_certificate_no.Text);
            cmd.Parameters.AddWithValue("@aadharno", txt_aadharno_mark.Text);
            cmd.Parameters.AddWithValue("@student_mother_tounge", ddl_student_mother_tongue.Text);
            cmd.Parameters.AddWithValue("@is_illness", ddl_illness.Text);
            cmd.Parameters.AddWithValue("@illness_remark", txt_illness_remark.Text);
            cmd.Parameters.AddWithValue("@Staff_employee_code", txt_employee_code.Text);
            cmd.Parameters.AddWithValue("@RTE", ddl_rte.Text);
            cmd.Parameters.AddWithValue("@staff_ward", ddl_staff_ward.Text);
            cmd.Parameters.AddWithValue("@Staff_name", txt_staff_name.Text);
            cmd.Parameters.AddWithValue("@Personal_Identymarks", txt_identification_marks.Text);
            cmd.Parameters.AddWithValue("@identifacationmark", txt_identification_marks.Text);



            cmd.Parameters.AddWithValue("@Height", txt_height.Text);
            cmd.Parameters.AddWithValue("@Weight", txt_weight.Text);
            cmd.Parameters.AddWithValue("@Sibling_name1", txt_name_of_sibling1.Text);
            cmd.Parameters.AddWithValue("@Sibling_age1", txt_age_sibling1.Text);
            cmd.Parameters.AddWithValue("@Sibling_school1", txt_school_sibling1.Text);
            cmd.Parameters.AddWithValue("@Sibling_class1", ddl_class_sb1.Text);
            cmd.Parameters.AddWithValue("@Sibling_name2", txt_name_of_sibling2.Text);
            cmd.Parameters.AddWithValue("@Sibling_age2", txt_age_sibling2.Text);
            cmd.Parameters.AddWithValue("@Sibling_school2", txt_school_sibling2.Text);
            cmd.Parameters.AddWithValue("@Sibling_class2", ddl_class_sb2.Text);


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
            cmd.Parameters.AddWithValue("@fathername", txt_father_first_name.Text + " " + txt_father_middle_name.Text + " " + txt_father_last_name.Text);
            cmd.Parameters.AddWithValue("@occuption", ddl_occupation.Text);
            cmd.Parameters.AddWithValue("@fatherqualification", ddl_father_qualification.Text);
            cmd.Parameters.AddWithValue("@f_nationality", ddl_father_nationality.Text);
            cmd.Parameters.AddWithValue("@f_marrital_statue", ddl_maritial_status.Text);
            cmd.Parameters.AddWithValue("@Country_Code_Father", ddl_cunterycode1.Text);
            cmd.Parameters.AddWithValue("@father_mob", txt_father_mobile.Text);
            cmd.Parameters.AddWithValue("@Father_whatsapp_country_code", ddl_fthr_whatsapp_c_Code.Text);
            cmd.Parameters.AddWithValue("@Father_whatsApp_no", txt_father_whatsapp_no.Text);
            cmd.Parameters.AddWithValue("@email_id", txt_guardian_email.Text);
            cmd.Parameters.AddWithValue("@guardianname", txt_guardian_name.Text);
            cmd.Parameters.AddWithValue("@parentincome", txt_annual_income.Text);
            cmd.Parameters.AddWithValue("@Father_aadhar_no", txt_father_aadhar_no.Text);


            //Mother Details
            cmd.Parameters.AddWithValue("@mothername", txt_mother_first_name.Text + " " + txt_mother_middle_name.Text + " " + txt_mother_last_name.Text);
            cmd.Parameters.AddWithValue("@m_occupation", ddl_m_occupation.Text);
            cmd.Parameters.AddWithValue("@motherqualifiaction", ddl_mother_qualification.Text);
            cmd.Parameters.AddWithValue("@m_nationality", ddl_mother_nationality.Text);
            cmd.Parameters.AddWithValue("@m_marrital_statue", ddl_m_maritial_status.Text);
            cmd.Parameters.AddWithValue("@Country_Code_Mother", ddl_cunterycode2.Text);
            cmd.Parameters.AddWithValue("@mother_mob", txt_mother_mobile_no.Text);
            cmd.Parameters.AddWithValue("@Mother_whatsapp_country_code", ddl_mthr_whatsapp_c_Code.Text);
            cmd.Parameters.AddWithValue("@Mother_whatsApp_no", txt_mother_whatsapp_no.Text);
            cmd.Parameters.AddWithValue("@mother_email", txt_mother_emailid.Text);
            cmd.Parameters.AddWithValue("@Mother_aadhar_no", txt_mother_aadhar_no.Text);


            //Present Address Details
            cmd.Parameters.AddWithValue("@careof", txt_adress.Text);
            cmd.Parameters.AddWithValue("@postoffice", txt_present_po.Text);
            cmd.Parameters.AddWithValue("@policestation", txt_temp_ps.Text);
            cmd.Parameters.AddWithValue("@district", txt_present_district.Text);
            cmd.Parameters.AddWithValue("@city", txt_city.Text);
            cmd.Parameters.AddWithValue("@state", ddl_temp_state.Text);
            cmd.Parameters.AddWithValue("@pin", txt_pincode.Text);
            cmd.Parameters.AddWithValue("@Present_country", ddl_country_c.Text);
            cmd.Parameters.AddWithValue("@Country_Code_Current_add", ddl_cunterycode3.Text);
            cmd.Parameters.AddWithValue("@mobilenumber", txt_temp_mobileno.Text);

            //Permanent Address Details
            cmd.Parameters.AddWithValue("@careof_permanent", txt_pAddress.Text);
            cmd.Parameters.AddWithValue("@postoffice_permanent", txt_perma_po.Text);
            cmd.Parameters.AddWithValue("@policestation_permanent", txt_par_ps.Text);
            cmd.Parameters.AddWithValue("@district_permanent", txt_perma_disctrict.Text);
            cmd.Parameters.AddWithValue("@city_permanent", txt_pcity.Text);
            cmd.Parameters.AddWithValue("@state_permanent", ddl_par_state.Text);
            cmd.Parameters.AddWithValue("@pincode", txt_Ppincod.Text);
            cmd.Parameters.AddWithValue("@Permanent_country", ddl_country_p.Text);
            cmd.Parameters.AddWithValue("@Country_Code_Current_Perm_add", ddl_cunterycode4.Text);
            cmd.Parameters.AddWithValue("@mob2", txt_p_mobile_no.Text);



            //Bank Details
            cmd.Parameters.AddWithValue("@Bank_acount_no", txt_account_no.Text);
            cmd.Parameters.AddWithValue("@Account_Holder_name", txt_account_holder_name.Text);
            cmd.Parameters.AddWithValue("@Bnk_Name", ddl_bank.Text);
            cmd.Parameters.AddWithValue("@IFSC_Code", txt_ifsc_code.Text);
            cmd.Parameters.AddWithValue("@Branch_Name", txt_branch_name.Text);



            //=======================

            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);


            cmd.Parameters.AddWithValue("@Student_Name_First", txt_firstname.Text.Trim());
            cmd.Parameters.AddWithValue("@Student_Middle_Name", txt_middlename.Text.Trim());
            cmd.Parameters.AddWithValue("@Student_Name_Last", txt_lastname.Text.Trim());

            cmd.Parameters.AddWithValue("@Father_Name_First", txt_father_first_name.Text.Trim());
            cmd.Parameters.AddWithValue("@Father_Name_Middle", txt_father_middle_name.Text.Trim());
            cmd.Parameters.AddWithValue("@Father_Name_Last", txt_father_last_name.Text.Trim());

            cmd.Parameters.AddWithValue("@Mother_Name_First", txt_mother_first_name.Text.Trim());
            cmd.Parameters.AddWithValue("@Mother_Name_Middle", txt_mother_middle_name.Text.Trim());
            cmd.Parameters.AddWithValue("@Mother_Name_Last", txt_mother_last_name.Text.Trim());
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



        #endregion

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetch_admission_no(); fetch_transport_info();
        }

        protected void ddl_room_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    ddl_hostel.Focus();
                    Alertme("Please select hostel.", "warning");
                }
                else if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    ddl_room_cat.Focus();
                    Alertme("Please select category.", "warning");
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
                    ddl_room_cat.Focus();
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
            mycode.bind_all_ddl_with_id(ddl_bed, "select 'Bed No. :'+Bed_name+', Bed Position '+Bed_Position,Bed_id from Hostel_room_bed_master where Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id='" + ddl_room.SelectedValue + "' and Status='1' and Hostel_id=" + ddl_hostel.SelectedValue + " and Category_id=" + ddl_room_cat.SelectedValue + " and Session_id='" + ddlsession.SelectedValue + "') and Room_id='" + ddl_room.SelectedValue + "' and Hostel_id='" + ddl_hostel.SelectedValue + "'and Category_id='" + ddl_room_cat.SelectedValue + "' order by Id asc");
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





        #region TRANSPORT
        private void fetch_transport_info()
        {
            mycodeMY.bind_all_ddl_with_id_notselect(ddl_trns_month, "select Month,Position from Month_Index order by Position asc");
            mycodeMY.bind_all_ddl_with_id(ddl_trns_vehicle, "select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");

            DataTable dtTransport = My.dataTable("select top 1 * from Transportation_Boarding_Point where Session_Id='" + ddlsession.SelectedValue + "' and TransportationPath_id in(select TransportationPath_id from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id=Transportation_Boarding_Point.TransportationPath_id and Transportation_Id=Transportation_Boarding_Point.Transportation_Id and Sheet_Status='0') order by Boarding_Point");
            if (dtTransport.Rows.Count > 0)
            {
                try
                {
                    ddl_trns_vehicle.SelectedValue = dtTransport.Rows[0]["Transportation_Id"].ToString();
                    fetch_transport_route("1", dtTransport.Rows[0]["TransportationPath_id"].ToString(), dtTransport.Rows[0]["Boarding_Point_id"].ToString());
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void fetch_transport_route(string from, string TransportationPath_id, string barding_point)
        {
            mycode.bind_all_ddl_with_id(ddl_trns_route, "select Rootname,TransportationPath_id from TransportationPath where Transportation_Id=" + ddl_trns_vehicle.SelectedValue + " order by Rootname asc");
            try
            {
                if (from == "1")
                {
                    try
                    {
                        ddl_trns_route.SelectedValue = TransportationPath_id;
                        fetch_boarding_point(from, TransportationPath_id);
                    }
                    catch (Exception ex)
                    {
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_boarding_point(string from, string transportationPath_id)
        {
            mycode.bind_all_ddl_with_id(ddl_trns_boarding_point, " select Boarding_Point,Boarding_Point_id from  Transportation_Boarding_Point where Transportation_Id=" + ddl_trns_vehicle.SelectedValue + " and TransportationPath_id=" + ddl_trns_route.SelectedValue + "  and Session_Id='" + ddlsession.SelectedValue + "' and TransportationPath_id in(select TransportationPath_id from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id=Transportation_Boarding_Point.TransportationPath_id and Transportation_Id=Transportation_Boarding_Point.Transportation_Id and Sheet_Status='0') order by Boarding_Point");
        }
        protected void ddl_trns_vehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_trns_vehicle.SelectedItem.Text == "Select")
                {
                    ddl_trns_vehicle.Focus();
                    Alertme("Please select vehicle", "warning");
                }
                else
                {
                    ddl_trns_route.Focus();
                    mycode.bind_all_ddl_with_id(ddl_trns_route, " select  Rootname,TransportationPath_id from  TransportationPath where Transportation_Id=" + ddl_trns_vehicle.SelectedValue + " order by Rootname asc");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_trns_route_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_trns_route.SelectedItem.Text == "Select")
                {
                    ddl_trns_route.Focus();
                    Alertme("Please select route", "warning");
                }
                else
                {
                    ddl_trns_boarding_point.Focus();
                    mycode.bind_all_ddl_with_id(ddl_trns_boarding_point, "select Boarding_Point,Boarding_Point_id from Transportation_Boarding_Point where Transportation_Id=" + ddl_trns_vehicle.SelectedValue + " and TransportationPath_id=" + ddl_trns_route.SelectedValue + "  and Session_Id='" + ddlsession.SelectedValue + "' and TransportationPath_id in(select TransportationPath_id from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id=Transportation_Boarding_Point.TransportationPath_id and Transportation_Id=Transportation_Boarding_Point.Transportation_Id and Sheet_Status='0') order by Boarding_Point");
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion



        [WebMethod]
        public static List<string> GetRooPath(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(compLN.comp))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct Caste_name from Caste_jati where Caste_name LIKE ''+@SearchMobNo+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["Caste_name"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        protected void btn_add_caste_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_caste_jati.Text == "")
                {
                    Alertme("Please enter caste name.", "warning");
                    txt_caste_jati.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalJati();", true);
                }
                else
                {
                    DataTable dt = compLN.dataTable_comp("select Caste_name from Caste_jati where Caste_name='" + txt_caste_jati.Text + "'");
                    if (dt.Rows.Count == 0)
                    {
                        compLN.exeSql_comp("insert into Caste_jati(Caste_name) values ('" + txt_caste_jati.Text + "');");
                        comP.bind_ddl_NA(ddl_caste_jati, "select Caste_name from Caste_jati order by Caste_name asc");
                        try
                        {
                            ddl_caste_jati.Text = txt_caste_jati.Text;
                            ddl_caste_jati.Focus();
                        }
                        catch (Exception ex)
                        {
                        }
                        Alertme("Caste has been added successfully.", "success");
                    }
                    else
                    {
                        try
                        {
                            ddl_caste_jati.Text = txt_caste_jati.Text;
                            ddl_caste_jati.Focus();
                        }
                        catch (Exception ex)
                        {
                        }
                        Alertme("Caste already exist.", "success");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}