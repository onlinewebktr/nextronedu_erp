using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class transfer_certificate_report : System.Web.UI.Page
    {
        My mycode = new My();
        compLN comP = new compLN();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    if (!IsPostBack)
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "transfer-certificate-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by session_id asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");

                        mycode.bind_all_ddl_with_id(ddl_class_c_6, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_class_c_8, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_class_c_7, "Select Course_Name,course_id from Add_course_table order by Position asc");

                        ddlsession.SelectedValue = My.get_session_id();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        find_tc_type();
                        comP.bind_ddl_no_select(ddl_nationality, "select Country_name from Country_list");
                        bind_created_certificate();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Transfer_certificate");
            }
        }
        private void find_tc_type()
        {
            hd_tc_type.Value = "1";
            DataTable dt = My.dataTable("select * from Transfer_certificate_setting where Status=1");
            if (dt.Rows.Count > 0)
            {
                hd_tc_type.Value = dt.Rows[0]["Tc_type_id"].ToString();
                if (hd_tc_type.Value == "5")
                {
                    hd_tc_type.Value = "4";
                }
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();

                hd_firm_id.Value = dt.Rows[0]["firm_id"].ToString();
            }
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

        private void bind_created_certificate()
        {
            lbl_class22.Text = "";
            btn_excels.Visible = false;
            string query = "";
            if (ddlclass.SelectedItem.Text.ToUpper() == "ALL")
            {
                query = "select t1.*,t2.studentname,t2.fathername,t2.admissionserialnumber,t2.class,t2.Section,t2.rollnumber,t2.session from dbo.[Transfer_certificate] t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Certificate_type='Transfer'    order by t2.rollnumber asc";
            }
            else
            {
                query = "select t1.*,t2.studentname,t2.fathername,t2.admissionserialnumber,t2.class,t2.Section,t2.rollnumber,t2.session from dbo.[Transfer_certificate] t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Class_id=" + ddlclass.SelectedValue + " and t1.Certificate_type='Transfer'   order by t2.rollnumber asc";

            }

            Bind_data(query);
        }

        private void Bind_data(string query)
        {
            ViewState["query"] = query;
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
                Alertme("Data Not Found...", "warning");
            }
            else
            {
                btn_excels.Visible = true;
                lbl_class22.Text = " Session :" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
                rd_view.DataSource = dt;
                rd_view.DataBind();

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }
        }
        protected void btn_find_by_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                }
                else
                {
                    string query = "select select t1.*,t2.studentname,t2.fathername,t2.admissionserialnumber,t2.class,t2.Section,t2.rollnumber,t2.session from dbo.[Transfer_certificate] t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Admission_no='" + txt_admission_no.Text + "' and t1.Certificate_type='Transfer'   order by t2.rollnumber asc";
                    Bind_data(query);

                }

            }
            catch
            {

            }
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddlclass.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");

            }
            else
            {
                bind_created_certificate();

            }
        }
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_certificate_nos = ((Label)e.Item.FindControl("lbl_certificate_nos")) as Label;
                HtmlAnchor tcc_link = (HtmlAnchor)e.Item.FindControl("tcc_link");
                if (hd_firm_id.Value == "AWD-01")
                {
                    tcc_link.HRef = "slip/TC/transfer-certificate-offline.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                }
                else
                {
                    if (hd_tc_type.Value == "2")
                    {
                        tcc_link.HRef = "slip/transfer-certificate2.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                    }
                    else if (hd_tc_type.Value == "3")
                    {
                        tcc_link.HRef = "slip/transfer-certificate3.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                    }
                    else if (hd_tc_type.Value == "4")
                    {
                        tcc_link.HRef = "slip/TC/transfer-certificate-004.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                    }
                    else if (hd_tc_type.Value == "5")
                    {
                        tcc_link.HRef = "slip/TC/transfer-certificate-005.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                    }
                    else if (hd_tc_type.Value == "6")
                    {
                        tcc_link.HRef = "slip/TC/transfer-certificate-006.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                    }
                    else if (hd_tc_type.Value == "7")
                    {
                        tcc_link.HRef = "slip/TC/transfer-certificate-007.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                    }
                    else if (hd_tc_type.Value == "8")
                    {
                        tcc_link.HRef = "slip/TC/transfer-certificate-008.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                    }
                    else if (hd_tc_type.Value == "10")
                    {
                        tcc_link.HRef = "slip/TC/transfer-certificate-010.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                    }
                    else
                    {
                        tcc_link.HRef = "slip/transfer-certificate.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                    }
                }
            }
        }

        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_Certificate_no = (Label)row.FindControl("lbl_Certificate_no");
                ViewState["adm_no"] = lbl_adm_no.Text;
                ViewState["sessioN"] = lbl_session_id.Text;
                ViewState["class_id"] = lbl_class_id.Text;
                ViewState["certificate_no"] = lbl_Certificate_no.Text;
                string query = "select * from admission_registor where admissionserialnumber='" + lbl_adm_no.Text + "' and Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' order by id desc";
                DataTable dt = My.dataTable(query);


                if (dt.Rows.Count > 0)
                {
                    if (hd_tc_type.Value == "2")
                    {
                        Alertme("This work is currently in progress and will be completed very soon.", "warning");
                    }
                    else if (hd_tc_type.Value == "3")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                        Repeater4.DataSource = dt;
                        Repeater4.DataBind();

                        txt_name3.Text = dt.Rows[0]["studentname"].ToString();
                        txt_father_name3.Text = dt.Rows[0]["fathername"].ToString();
                        txt_mother_name3.Text = dt.Rows[0]["mothername"].ToString();
                        ddl_religion3.Text = dt.Rows[0]["religion"].ToString();
                        try
                        {
                            ddl_nationality3.Text = dt.Rows[0]["Student_nationality"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_dob3.Text = dt.Rows[0]["dob"].ToString();


                        DataTable dtT = My.dataTable("select * from Transfer_certificate where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and  Certificate_type='Transfer' ");
                        if (dtT.Rows.Count > 0)
                        {
                            txt_class_in_last_studied3.Text = dtT.Rows[0]["class_in_last_studied"].ToString();
                            txt_result_of_board3.Text = dtT.Rows[0]["annual_exam_taken_with_result"].ToString();
                            txt_subject_studied3.Text = dtT.Rows[0]["subject_studied"].ToString();
                            txt_whether_qualified3.Text = dtT.Rows[0]["whether_qualified_for_promotion"].ToString();
                            txt_fee_dues3.Text = dtT.Rows[0]["Dues_fee"].ToString();
                            txt_any_fee_concession3.Text = dtT.Rows[0]["AnyConcession"].ToString();
                            txt_date_of_birth3.Text = dtT.Rows[0]["Date_of_births"].ToString();
                            txt_meeting_up_to_date3.Text = dtT.Rows[0]["ttl_working_days"].ToString();
                            txt_attendance_during_the_session3.Text = dtT.Rows[0]["present_days"].ToString();
                            txt_date_of_issue3.Text = dtT.Rows[0]["date_of_issue_certificate"].ToString();
                            txt_reason_for_leaving_school3.Text = dtT.Rows[0]["reason_for_leaving"].ToString();
                            txt_any_other_remarks3.Text = dtT.Rows[0]["any_other_remarks"].ToString();
                            txt_general_conduct3.Text = dtT.Rows[0]["general_conduct"].ToString();
                            txt_date_of_admission3.Text = dtT.Rows[0]["Date_of_admission_and_class"].ToString();
                            txt_ncc3.Text = dtT.Rows[0]["ncc_cadet"].ToString();
                            txt_sc_st3.Text = dtT.Rows[0]["Belongs_t_sc_st_obc"].ToString();
                            txt_Nationality3.Text = dtT.Rows[0]["Nationality"].ToString();
                            txt_srn_no3.Text = dtT.Rows[0]["srn_no"].ToString();
                            txt_reg_no_ix_xii3.Text = dtT.Rows[0]["reg_no_ix_xii"].ToString();
                        }
                    }
                    else if (hd_tc_type.Value == "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                        rp_tc_4.DataSource = dt;
                        rp_tc_4.DataBind();
                        txt_name4.Text = dt.Rows[0]["studentname"].ToString();
                        txt_father_name4.Text = dt.Rows[0]["fathername"].ToString();
                        txt_mother_name4.Text = dt.Rows[0]["mothername"].ToString();
                        ddl_religion4.Text = dt.Rows[0]["religion"].ToString();




                        DataTable dtT = My.dataTable("select * from Transfer_certificate where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and  Certificate_type='Transfer'");
                        if (dtT.Rows.Count > 0)
                        {
                            txt_date_of_birth4.Text = dtT.Rows[0]["Date_of_births"].ToString();
                            if (txt_date_of_birth4.Text == "")
                            {
                                txt_date_of_birth4.Text = dt.Rows[0]["dob"].ToString();
                            }
                            txt_nationality_4.Text = dtT.Rows[0]["Nationality"].ToString();
                            txt_class_in_last_studied4.Text = dtT.Rows[0]["class_in_last_studied"].ToString();
                            txt_annual_exam_taken_with_result4.Text = dtT.Rows[0]["annual_exam_taken_with_result"].ToString();


                            txt_whether_failed_in_same_class4.Text = dtT.Rows[0]["whether_failed_in_same_class"].ToString();
                            txt_subject_studied4.Text = dtT.Rows[0]["subject_studied"].ToString();
                            txt_optional_subject4.Text = dtT.Rows[0]["optional_subject"].ToString();
                            txt_whether_qualified_for_promotion4.Text = dtT.Rows[0]["whether_qualified_for_promotion"].ToString();

                            txt_ttl_working_days4.Text = dtT.Rows[0]["ttl_working_days"].ToString();
                            txt_present_days4.Text = dtT.Rows[0]["present_days"].ToString();
                            txt_ncc_cadet4.Text = dtT.Rows[0]["ncc_cadet"].ToString();
                            txt_game_played_of_extra4.Text = dtT.Rows[0]["game_played_of_extra"].ToString();

                            txt_general_conduct4.Text = dtT.Rows[0]["general_conduct"].ToString();
                            txt_date_of_application4.Text = dtT.Rows[0]["date_of_application"].ToString();
                            txt_date_of_issue_certificate4.Text = dtT.Rows[0]["date_of_issue_certificate"].ToString();
                            txt_reason_for_leaving4.Text = dtT.Rows[0]["reason_for_leaving"].ToString();

                            txt_date_of_admission4.Text = dtT.Rows[0]["Date_of_admission_and_class"].ToString();
                            txt_board_roll_no4.Text = dtT.Rows[0]["Board_roll_no"].ToString();
                            txt_cbse_reg_no4.Text = dtT.Rows[0]["Cbse_reg_no"].ToString();

                            txt_belong_to_sc_st_obc4.Text = dtT.Rows[0]["Belongs_t_sc_st_obc"].ToString();
                            txt_aadhar_no.Text = dtT.Rows[0]["Aadhar_no"].ToString();
                            txt_student_pen.Text = dtT.Rows[0]["Student_PEN"].ToString();
                            txt_any_other_remarks4.Text = dtT.Rows[0]["any_other_remarks"].ToString();
                        }
                    }
                    else if (hd_tc_type.Value == "6")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_6();", true);

                        Repeater1_tc6.DataSource = dt;
                        Repeater1_tc6.DataBind();

                        string date_of_admission_and_class_id = find_date_of_admission(lbl_adm_no.Text);
                        string[] stringSeparatorss = new string[] { "~" };
                        string[] arrs = date_of_admission_and_class_id.Split(stringSeparatorss, StringSplitOptions.None);
                        string date_of_admission = arrs[0];
                        string class_id = arrs[1];

                        txt_date_of_admission_6.Text = date_of_admission;

                        try
                        {
                            ddl_class_c_6.SelectedValue = class_id;
                        }
                        catch (Exception ex)
                        {
                        }



                        DataTable dtT = My.dataTable("select * from Transfer_certificate where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and  Certificate_type='Transfer'");
                        if (dtT.Rows.Count > 0)
                        {
                            txt_pen_no_6.Text = dtT.Rows[0]["Student_PEN"].ToString();
                            txt_belong_to_sc_st_obc_6.Text = dtT.Rows[0]["Belongs_t_sc_st_obc"].ToString();


                            txt_class_in_last_studied_6.Text = dtT.Rows[0]["class_in_last_studied"].ToString();
                            txt_annual_exam_taken_with_result_6.Text = dtT.Rows[0]["annual_exam_taken_with_result"].ToString();

                            txt_subject_studied_6.Text = dtT.Rows[0]["subject_studied"].ToString();
                            txt_optional_subject_6.Text = dtT.Rows[0]["optional_subject"].ToString();
                            txt_whether_qualified_for_promotion_6.Text = dtT.Rows[0]["whether_qualified_for_promotion"].ToString();
                            txt_ttl_working_days_6.Text = dtT.Rows[0]["ttl_working_days"].ToString();
                            txt_present_days_6.Text = dtT.Rows[0]["present_days"].ToString();

                            txt_general_conduct_6.Text = dtT.Rows[0]["general_conduct"].ToString();
                            txt_date_of_application_6.Text = dtT.Rows[0]["date_of_application"].ToString();
                            txt_date_of_issue_certificate_6.Text = dtT.Rows[0]["date_of_issue_certificate"].ToString();
                            txt_reason_for_leaving_6.Text = dtT.Rows[0]["reason_for_leaving"].ToString();

                        }
                    }

                    else if (hd_tc_type.Value == "7")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_7();", true);
                        Repeater7.DataSource = dt;
                        Repeater7.DataBind();
                        string date_of_admission_and_class_id = find_date_of_admission(lbl_adm_no.Text);
                        string[] stringSeparatorss = new string[] { "~" };
                        string[] arrs = date_of_admission_and_class_id.Split(stringSeparatorss, StringSplitOptions.None);
                        string date_of_admission = arrs[0];
                        string class_id = arrs[1];
                        txt_date_of_admission_7.Text = date_of_admission;
                        try
                        {
                            ddl_class_c_7.SelectedValue = class_id;
                        }
                        catch (Exception ex)
                        {
                        }
                        DataTable dtT = My.dataTable("select * from Transfer_certificate where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and  Certificate_type='Transfer'");
                        if (dtT.Rows.Count > 0)
                        {
                            txt_pen_no_7.Text = dtT.Rows[0]["Student_PEN"].ToString();
                            txt_belong_to_sc_st_obc_7.Text = dtT.Rows[0]["Belongs_t_sc_st_obc"].ToString();
                            txt_game_played_of_extra_7.Text = dtT.Rows[0]["game_played_of_extra"].ToString();
                            txt_ncc_cadet_7.Text = dtT.Rows[0]["ncc_cadet"].ToString();
                            txt_class_in_last_studied_7.Text = dtT.Rows[0]["class_in_last_studied"].ToString();
                            txt_annual_exam_taken_with_result_7.Text = dtT.Rows[0]["annual_exam_taken_with_result"].ToString();
                            txt_whether_failed_in_same_class_7.Text = dtT.Rows[0]["whether_failed_in_same_class"].ToString();
                            txt_subject_studied_7.Text = dtT.Rows[0]["subject_studied"].ToString();
                            txt_optional_subject_7.Text = dtT.Rows[0]["optional_subject"].ToString();
                            txt_whether_qualified_for_promotion_7.Text = dtT.Rows[0]["whether_qualified_for_promotion"].ToString();
                            txt_general_conduct_7.Text = dtT.Rows[0]["general_conduct"].ToString();
                            txt_date_of_application_7.Text = dtT.Rows[0]["date_of_application"].ToString();
                            txt_date_of_issue_certificate_7.Text = dtT.Rows[0]["date_of_issue_certificate"].ToString();
                            txt_reason_for_leaving_7.Text = dtT.Rows[0]["reason_for_leaving"].ToString();

                        }
                    }
                    else if (hd_tc_type.Value == "8")
                    {


                        Repeater_8.DataSource = dt;
                        Repeater_8.DataBind();



                        txt_dob_8.Text = dt.Rows[0]["dob"].ToString();

                        DataTable dtT = My.dataTable("select * from Transfer_certificate where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and  Certificate_type='Transfer' ");
                        if (dtT.Rows.Count > 0)
                        {
                            txt_pen_no_8.Text = dtT.Rows[0]["Student_PEN"].ToString();
                            txt_belong_to_sc_st_obc_8.Text = dtT.Rows[0]["Belongs_t_sc_st_obc"].ToString();
                            txt_date_of_admission_8.Text = dtT.Rows[0]["Date_of_admission_and_class"].ToString();
                            txt_class_in_last_studied_8.Text = dtT.Rows[0]["class_in_last_studied"].ToString();
                            txt_whether_qualified_for_promotion_8.Text = dtT.Rows[0]["whether_qualified_for_promotion"].ToString();
                            txt_monthuptopaid_8.Text = dtT.Rows[0]["Monthuptopaid"].ToString();
                            txt_general_conduct_8.Text = dtT.Rows[0]["general_conduct"].ToString();
                            txt_date_of_left_school_8.Text = dtT.Rows[0]["Data_of_left_school"].ToString();
                            txt_date_of_application_8.Text = dtT.Rows[0]["date_of_application"].ToString();
                            txt_date_of_issue_certificate_8.Text = dtT.Rows[0]["date_of_issue_certificate"].ToString();
                            txt_reason_for_leaving_8.Text = dtT.Rows[0]["reason_for_leaving"].ToString();
                            txt_any_other_remarks_8.Text = dtT.Rows[0]["any_other_remarks"].ToString();
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_8();", true);
                    } 
                    else if (hd_tc_type.Value == "10")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTEN();", true);
                        rp_trnsf_six.DataSource = dt;
                        rp_trnsf_six.DataBind();
                        txt_name4.Text = dt.Rows[0]["studentname"].ToString();
                        txt_father_name4.Text = dt.Rows[0]["fathername"].ToString();
                        txt_mother_name4.Text = dt.Rows[0]["mothername"].ToString();
                        ddl_religion4.Text = dt.Rows[0]["religion"].ToString();




                        DataTable dtT = My.dataTable("select * from Transfer_certificate where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and  Certificate_type='Transfer'");
                        if (dtT.Rows.Count > 0)
                        {
                            txt_class_last_studies_ten.Text = dtT.Rows[0]["class_in_last_studied"].ToString();
                            txt_qualified_higher_class_ten.Text = dtT.Rows[0]["whether_qualified_for_promotion"].ToString();
                            txt_general_conduct_ten.Text = dtT.Rows[0]["general_conduct"].ToString();
                            txt_date_of_application_ten.Text = dtT.Rows[0]["date_of_application"].ToString();
                            txt_date_of_issue_ten.Text = dtT.Rows[0]["date_of_issue_certificate"].ToString();
                            txt_reason_for_leaving_ten.Text = dtT.Rows[0]["reason_for_leaving"].ToString();
                            txt_any_other_remarks_ten.Text = dtT.Rows[0]["any_other_remarks"].ToString();

                            try
                            {
                                string[] stringSeparatorss = new string[] { "," };
                                string[] arrs = dtT.Rows[0]["Date_of_admission_and_class"].ToString().Split(stringSeparatorss, StringSplitOptions.None);
                                string doa = arrs[0];
                                string doc = arrs[1]; 
                                txt_date_of_adm_ten.Text = doa;
                                txt_adm_class_ten.Text = doc;
                            }
                            catch (Exception ex)
                            {
                            }

                            txt_belongs_to_scst_ten.Text = dtT.Rows[0]["Belongs_t_sc_st_obc"].ToString();
                            txt_nationality_ten.Text = dtT.Rows[0]["Nationality"].ToString();
                            txt_dob_ten.Text = dtT.Rows[0]["Date_of_births"].ToString();
                            txt_dob_in_word.Text = dtT.Rows[0]["Date_of_birth_word"].ToString();
                            txt_school_board_exam_taken_ten.Text = dtT.Rows[0]["annual_exam_taken_with_result"].ToString();
                            txt_failed_once_twice_ten.Text = dtT.Rows[0]["whether_failed_in_same_class"].ToString();
                            txt_subj_studies_ten.Text = dtT.Rows[0]["subject_studied"].ToString();
                            txt_paid_fee_till_month_ten.Text = dtT.Rows[0]["Dues_fee"].ToString();
                            txt_any_fee_concession_ten.Text = dtT.Rows[0]["Concession"].ToString();
                            txt_ncc_cadet_by_ten.Text = dtT.Rows[0]["ncc_cadet"].ToString();
                            txt_games_played.Text = dtT.Rows[0]["game_played_of_extra"].ToString();
                            txt_ttl_no_of_workind_days_ten.Text = dtT.Rows[0]["ttl_working_days"].ToString();
                            txt_ttl_persent_days.Text = dtT.Rows[0]["present_days"].ToString(); 
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);

                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                        txt_pen_no.Text = dt.Rows[0]["Student_PEN"].ToString();
                        txt_name.Text = dt.Rows[0]["studentname"].ToString();
                        txt_father_name.Text = dt.Rows[0]["fathername"].ToString();
                        txt_mother_name.Text = dt.Rows[0]["mothername"].ToString();
                        try
                        {
                            ddl_religion.Text = dt.Rows[0]["religion"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_nationality.Text = dt.Rows[0]["Student_nationality"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }

                        txt_dob.Text = dt.Rows[0]["dob"].ToString();

                        DataTable dtT = My.dataTable("select * from Transfer_certificate where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and  Certificate_type='Transfer'");
                        if (dtT.Rows.Count > 0)
                        {
                            txt_belong_to_sc_st_obc.Text = dtT.Rows[0]["Belongs_t_sc_st_obc"].ToString();
                            txt_date_of_admission_and_class.Text = dtT.Rows[0]["Date_of_admission_and_class"].ToString();
                            txt_class_in_last_studied.Text = dtT.Rows[0]["class_in_last_studied"].ToString();
                            txt_annual_exam_taken_with_result.Text = dtT.Rows[0]["annual_exam_taken_with_result"].ToString();
                            txt_whether_failed_in_same_class.Text = dtT.Rows[0]["whether_failed_in_same_class"].ToString();
                            txt_subject_studied.Text = dtT.Rows[0]["subject_studied"].ToString();
                            txt_optional_subject.Text = dtT.Rows[0]["optional_subject"].ToString();
                            txt_whether_qualified_for_promotion.Text = dtT.Rows[0]["whether_qualified_for_promotion"].ToString();
                            txt_ttl_working_days.Text = dtT.Rows[0]["ttl_working_days"].ToString();
                            txt_present_days.Text = dtT.Rows[0]["present_days"].ToString();
                            txt_ncc_cadet.Text = dtT.Rows[0]["ncc_cadet"].ToString();
                            txt_game_played_of_extra.Text = dtT.Rows[0]["game_played_of_extra"].ToString();
                            txt_general_conduct.Text = dtT.Rows[0]["general_conduct"].ToString();
                            txt_date_of_application.Text = dtT.Rows[0]["date_of_application"].ToString();
                            txt_date_of_issue_certificate.Text = dtT.Rows[0]["date_of_issue_certificate"].ToString();
                            txt_reason_for_leaving.Text = dtT.Rows[0]["reason_for_leaving"].ToString();
                            txt_board_roll_no.Text = dtT.Rows[0]["Board_roll_no"].ToString();
                            txt_cbse_reg_no.Text = dtT.Rows[0]["Cbse_reg_no"].ToString();
                            txt_any_other_remarks.Text = dtT.Rows[0]["any_other_remarks"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
        private string find_date_of_admission(string admission_no)
        {
            string returN = "NA/0";
            DataTable dt = My.dataTable("select dateofadmission,Class_id from admission_registor where admissionserialnumber='" + admission_no + "'");
            if (dt.Rows.Count > 0)
            {
                returN = dt.Rows[0]["dateofadmission"].ToString() + "~" + dt.Rows[0]["Class_id"].ToString();
            }
            return returN;
        }
        protected void btn_make_tc_3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_name3.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_name3.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                }
                else if (txt_father_name3.Text == "")
                {
                    Alertme("Please enter father name.", "warning");
                    txt_father_name3.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                }
                else if (txt_mother_name3.Text == "")
                {
                    Alertme("Please enter mother name.", "warning");
                    txt_mother_name3.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                }
                else if (txt_dob3.Text == "")
                {
                    Alertme("Please enter date of birth.", "warning");
                    txt_dob3.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                }
                else if (txt_date_of_admission3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter date of admission & class.", "warning");
                    txt_date_of_admission3.Focus();
                }
                else if (txt_date_of_birth3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter date of birth.", "warning");
                    txt_date_of_birth3.Focus();
                }
                else if (txt_Nationality3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter Student Nationality", "warning");
                    txt_Nationality3.Focus();
                }
                else if (txt_sc_st3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter student  SC/ST/OBC", "warning");
                    txt_sc_st3.Focus();
                }
                else if (txt_class_in_last_studied3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter class in which the Studying/passed.", "warning");
                    txt_class_in_last_studied3.Focus();
                }
                else if (txt_result_of_board3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter Result Of Board/School Examination (Passed/Detained).", "warning");
                    txt_result_of_board3.Focus();
                }
                else if (txt_subject_studied3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter subject studied, compulsory.", "warning");
                    txt_subject_studied3.Focus();
                }
                else if (txt_whether_qualified3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter whether qualified for promotion to higher class.", "warning");
                    txt_whether_qualified3.Focus();
                }
                else if (txt_fee_dues3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter whether the pupil paid all the fees due.", "warning");
                    txt_fee_dues3.Focus();
                }
                else if (txt_any_fee_concession3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter any fee concession/scholarship availed of, if so, the nature of such concession?", "warning");
                    txt_any_fee_concession3.Focus();
                }
                else if (txt_meeting_up_to_date3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter No. of Meeting up to date", "warning");
                    txt_meeting_up_to_date3.Focus();
                }
                else if (txt_attendance_during_the_session3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter Number of school-days the pupil attended", "warning");
                    txt_attendance_during_the_session3.Focus();
                }

                else if (txt_ncc3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter whether NCC Cadet / Boy Scout / Girl Guide", "warning");
                    txt_ncc3.Focus();
                }
                else if (txt_general_conduct3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter general conduct.", "warning");
                    txt_general_conduct3.Focus();
                }

                else if (txt_date_of_issue3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter date of issue of certificate.", "warning");
                    txt_date_of_issue3.Focus();
                }
                else if (txt_reason_for_leaving_school3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter Reason for leaving the school.", "warning");
                    txt_reason_for_leaving_school3.Focus();
                }
                else if (txt_srn_no3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter SRN No", "warning");
                    txt_srn_no3.Focus();
                }
                else
                {
                    update_certificate();
                    Alertme("Certificate details have been updated successfully.", "success");


                    Bind_data(ViewState["query"].ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_certificate()
        {
            SqlCommand cmd;
            string query = "update Transfer_certificate set class_in_last_studied=@class_in_last_studied,annual_exam_taken_with_result=@annual_exam_taken_with_result,subject_studied=@subject_studied,whether_qualified_for_promotion=@whether_qualified_for_promotion, Dues_fee = @Dues_fee,AnyConcession = @AnyConcession,Date_of_births = @Date_of_births,ttl_working_days = @ttl_working_days,present_days= @present_days,date_of_issue_certificate = @date_of_issue_certificate,reason_for_leaving = @reason_for_leaving, any_other_remarks = @any_other_remarks,general_conduct = @general_conduct,Date_of_admission_and_class = @Date_of_admission_and_class,ncc_cadet = @ncc_cadet,Belongs_t_sc_st_obc = @Belongs_t_sc_st_obc,Nationality = @Nationality,srn_no = @srn_no,reg_no_ix_xii = @reg_no_ix_xii where Session_id=@Session_id and Class_id=@Class_id and Admission_no=@Admission_no and Certificate_no=@Certificate_no";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ViewState["sessioN"].ToString());
            cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
            cmd.Parameters.AddWithValue("@Admission_no", ViewState["adm_no"].ToString());
            cmd.Parameters.AddWithValue("@Certificate_no", ViewState["certificate_no"].ToString());

            cmd.Parameters.AddWithValue("@class_in_last_studied", txt_class_in_last_studied3.Text);
            cmd.Parameters.AddWithValue("@annual_exam_taken_with_result", txt_result_of_board3.Text);
            cmd.Parameters.AddWithValue("@subject_studied", txt_subject_studied3.Text);
            cmd.Parameters.AddWithValue("@whether_qualified_for_promotion", txt_whether_qualified3.Text);
            cmd.Parameters.AddWithValue("@Dues_fee", txt_fee_dues3.Text);
            cmd.Parameters.AddWithValue("@AnyConcession", txt_any_fee_concession3.Text);
            cmd.Parameters.AddWithValue("@Date_of_births", txt_date_of_birth3.Text);
            cmd.Parameters.AddWithValue("@ttl_working_days", txt_meeting_up_to_date3.Text);
            cmd.Parameters.AddWithValue("@present_days", txt_attendance_during_the_session3.Text);


            cmd.Parameters.AddWithValue("@date_of_issue_certificate", txt_date_of_issue3.Text);
            cmd.Parameters.AddWithValue("@reason_for_leaving", txt_reason_for_leaving_school3.Text);
            cmd.Parameters.AddWithValue("@any_other_remarks", txt_any_other_remarks3.Text);
            cmd.Parameters.AddWithValue("@general_conduct", txt_general_conduct3.Text);
            cmd.Parameters.AddWithValue("@Date_of_admission_and_class", txt_date_of_admission3.Text);
            cmd.Parameters.AddWithValue("@ncc_cadet", txt_ncc3.Text);
            cmd.Parameters.AddWithValue("@Belongs_t_sc_st_obc", txt_sc_st3.Text);
            cmd.Parameters.AddWithValue("@Nationality", txt_Nationality3.Text);

            cmd.Parameters.AddWithValue("@srn_no", txt_srn_no3.Text);
            cmd.Parameters.AddWithValue("@reg_no_ix_xii", txt_reg_no_ix_xii3.Text);
            if (My.InsertUpdateData(cmd))
            {
                My.exeSql("update admission_registor set studentname='" + txt_name3.Text + "', fathername='" + txt_father_name3.Text + "',mothername='" + txt_mother_name3.Text + "',religion='" + ddl_religion3.Text + "',Student_nationality='" + ddl_nationality3.Text + "',dob='" + txt_dob3.Text + "' where Session_id='" + ViewState["sessioN"].ToString() + "' and admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                string desc = "Transfer certificate updated for certificate no. : " + ViewState["certificate_no"].ToString() + " by " + ViewState["Userid"].ToString();
                log_hostory.edit_log(ViewState["sessioN"].ToString(), ViewState["class_id"].ToString(), ViewState["adm_no"].ToString(), "TransferCertificateEdit", desc, "transfer-certificate-report.aspx", ViewState["Userid"].ToString());
            }
        }

        protected void btn_update_certificate1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_name.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_father_name.Text == "")
                {
                    Alertme("Please enter father name.", "warning");
                    txt_father_name.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_mother_name.Text == "")
                {
                    Alertme("Please enter mother name.", "warning");
                    txt_mother_name.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_dob.Text == "")
                {
                    Alertme("Please enter date of birth.", "warning");
                    txt_dob.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }





                else if (txt_date_of_admission_and_class.Text == "")
                {
                    Alertme("Please enter date of admission & class name.", "warning");
                    txt_date_of_admission_and_class.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_class_in_last_studied.Text == "")
                {
                    Alertme("Please enter class in which the pupil last studied.", "warning");
                    txt_class_in_last_studied.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_annual_exam_taken_with_result.Text == "")
                {
                    Alertme("Please enter School / Birth Annual Examination last taken with result.", "warning");
                    txt_annual_exam_taken_with_result.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_whether_failed_in_same_class.Text == "")
                {
                    Alertme("Please enter Whether failed in same class.", "warning");
                    txt_whether_failed_in_same_class.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_subject_studied.Text == "")
                {
                    Alertme("Please enter Subject studied, Compulsory.", "warning");
                    txt_subject_studied.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_whether_qualified_for_promotion.Text == "")
                {
                    Alertme("Please enter Whether qualified for promotion / to which class.", "warning");
                    txt_whether_qualified_for_promotion.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_ttl_working_days.Text == "")
                {
                    Alertme("Please enter Total no. of working days.", "warning");
                    txt_ttl_working_days.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_present_days.Text == "")
                {
                    Alertme("Please enter Total no. of working days present.", "warning");
                    txt_present_days.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_ncc_cadet.Text == "")
                {
                    Alertme("Please enter Whether NCC Cadet / Boy Scout / Girl Guide.", "warning");
                    txt_ncc_cadet.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_game_played_of_extra.Text == "")
                {
                    Alertme("Please enter Games played or extra curricular activities in which the pupil usually took apart.", "warning");
                    txt_game_played_of_extra.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_general_conduct.Text == "")
                {
                    Alertme("Please enter General conduct.", "warning");
                    txt_general_conduct.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_date_of_application.Text == "")
                {
                    Alertme("Please enter Date of application for certificate.", "warning");
                    txt_date_of_application.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_date_of_issue_certificate.Text == "")
                {
                    Alertme("Please enter Date of issue of certificate.", "warning");
                    txt_date_of_issue_certificate.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else if (txt_reason_for_leaving.Text == "")
                {
                    Alertme("Please enter Reason for leaving the school.", "warning");
                    txt_reason_for_leaving.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                }
                else
                {
                    update_tc_one();
                    Alertme("Certificate details have been updated successfully.", "success");
                    Bind_data(ViewState["query"].ToString());
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void update_tc_one()
        {
            SqlCommand cmd;
            string query = "update Transfer_certificate set Belongs_t_sc_st_obc= @Belongs_t_sc_st_obc,Date_of_admission_and_class= @Date_of_admission_and_class,class_in_last_studied= @class_in_last_studied,annual_exam_taken_with_result= @annual_exam_taken_with_result,whether_failed_in_same_class= @whether_failed_in_same_class,subject_studied=@subject_studied,optional_subject=@optional_subject,whether_qualified_for_promotion=@whether_qualified_for_promotion,ttl_working_days=@ttl_working_days,present_days=@present_days,ncc_cadet=@ncc_cadet,game_played_of_extra=@game_played_of_extra,general_conduct=@general_conduct,date_of_application=@date_of_application,date_of_issue_certificate=@date_of_issue_certificate,reason_for_leaving=@reason_for_leaving,Board_roll_no=@Board_roll_no,Cbse_reg_no=@Cbse_reg_no,any_other_remarks=@any_other_remarks,Student_PEN=@Student_PEN where Session_id=@Session_id and Class_id=@Class_id and Admission_no=@Admission_no and Certificate_no=@Certificate_no";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Student_PEN", txt_pen_no.Text);
            cmd.Parameters.AddWithValue("@Session_id", ViewState["sessioN"].ToString());
            cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
            cmd.Parameters.AddWithValue("@Admission_no", ViewState["adm_no"].ToString());
            cmd.Parameters.AddWithValue("@Certificate_no", ViewState["certificate_no"].ToString());

            cmd.Parameters.AddWithValue("@Belongs_t_sc_st_obc", txt_belong_to_sc_st_obc.Text);
            cmd.Parameters.AddWithValue("@Date_of_admission_and_class", txt_date_of_admission_and_class.Text);
            cmd.Parameters.AddWithValue("@class_in_last_studied", txt_class_in_last_studied.Text);
            cmd.Parameters.AddWithValue("@annual_exam_taken_with_result", txt_annual_exam_taken_with_result.Text);
            cmd.Parameters.AddWithValue("@whether_failed_in_same_class", txt_whether_failed_in_same_class.Text);
            cmd.Parameters.AddWithValue("@subject_studied", txt_subject_studied.Text);
            cmd.Parameters.AddWithValue("@optional_subject", txt_optional_subject.Text);


            cmd.Parameters.AddWithValue("@whether_qualified_for_promotion", txt_whether_qualified_for_promotion.Text);
            cmd.Parameters.AddWithValue("@ttl_working_days", txt_ttl_working_days.Text);
            cmd.Parameters.AddWithValue("@present_days", txt_present_days.Text);
            cmd.Parameters.AddWithValue("@ncc_cadet", txt_ncc_cadet.Text);
            cmd.Parameters.AddWithValue("@game_played_of_extra", txt_game_played_of_extra.Text);
            cmd.Parameters.AddWithValue("@general_conduct", txt_general_conduct.Text);
            cmd.Parameters.AddWithValue("@date_of_application", txt_date_of_application.Text);

            cmd.Parameters.AddWithValue("@date_of_issue_certificate", txt_date_of_issue_certificate.Text);
            cmd.Parameters.AddWithValue("@reason_for_leaving", txt_reason_for_leaving.Text);
            cmd.Parameters.AddWithValue("@Board_roll_no", txt_board_roll_no.Text);
            cmd.Parameters.AddWithValue("@Cbse_reg_no", txt_cbse_reg_no.Text);
            cmd.Parameters.AddWithValue("@any_other_remarks", txt_any_other_remarks.Text);
            if (My.InsertUpdateData(cmd))
            {
                My.exeSql("update admission_registor set studentname='" + txt_name.Text + "', fathername='" + txt_father_name.Text + "',mothername='" + txt_mother_name.Text + "',religion='" + ddl_religion.Text + "',Student_nationality='" + ddl_nationality.Text + "',dob='" + txt_dob.Text + "',Student_pen_no='" + txt_pen_no.Text + "' where Session_id='" + ViewState["sessioN"].ToString() + "' and admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                string desc = "Transfer certificate updated for certificate no. : " + ViewState["certificate_no"].ToString() + " by " + ViewState["Userid"].ToString();
                log_hostory.edit_log(ViewState["sessioN"].ToString(), ViewState["class_id"].ToString(), ViewState["adm_no"].ToString(), "TransferCertificateEdit", desc, "transfer-certificate-report.aspx", ViewState["Userid"].ToString());
            }
        }

        protected void lnk_delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_Certificate_no = (Label)row.FindControl("lbl_Certificate_no");

                My.exeSql("delete from Transfer_certificate where Session_id='" + lbl_session_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "'  and Certificate_no='" + lbl_Certificate_no.Text + "' and Certificate_type='Transfer'; update admission_registor set Status='1',Is_TC_Taken=0 where admissionserialnumber='" + lbl_adm_no.Text + "' and  Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "'");
                string desc = "Transfer certificate deleted for certificate no. : " + lbl_Certificate_no.Text + " & user id : " + ViewState["Userid"].ToString();
                log_hostory.delete_log(lbl_session_id.Text, lbl_class_id.Text, lbl_adm_no.Text, "TrensferCertificateDelete", desc, "transfer-certificate-report.aspx", ViewState["Userid"].ToString());
                Alertme("Transfer Certificate has been deleted successfully.", "success");
                bind_created_certificate();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_save_fouth_tc_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_name4.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_name4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_father_name4.Text == "")
                {
                    Alertme("Please enter father name.", "warning");
                    txt_father_name4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_mother_name4.Text == "")
                {
                    Alertme("Please enter mother name.", "warning");
                    txt_mother_name4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_nationality_4.Text == "")
                {
                    Alertme("Please enter nationality.", "warning");
                    txt_nationality_4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_date_of_birth4.Text == "")
                {
                    Alertme("Please enter date of birth.", "warning");
                    txt_date_of_birth4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_date_of_admission4.Text == "")
                {
                    Alertme("Please enter date of admission & class name.", "warning");
                    txt_date_of_admission4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_class_in_last_studied4.Text == "")
                {
                    Alertme("Please enter class in which the pupil last studied.", "warning");
                    txt_class_in_last_studied4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_annual_exam_taken_with_result4.Text == "")
                {
                    Alertme("Please enter School / Annual Examination last taken with result.", "warning");
                    txt_annual_exam_taken_with_result4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_whether_failed_in_same_class4.Text == "")
                {
                    Alertme("Please enter Whether failed, if so once/twice in the same class.", "warning");
                    txt_whether_failed_in_same_class4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_subject_studied4.Text == "")
                {
                    Alertme("Please enter Subject studied, Compulsory.", "warning");
                    txt_subject_studied4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_whether_qualified_for_promotion4.Text == "")
                {
                    Alertme("Please enter Whether qualified for promotion / to which class.", "warning");
                    txt_whether_qualified_for_promotion4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_ncc_cadet4.Text == "")
                {
                    Alertme("Please enter Whether NCC Cadet / Boy Scout / Girl Guide.", "warning");
                    txt_ncc_cadet4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_game_played_of_extra4.Text == "")
                {
                    Alertme("Please enter Games played or extra curricular activities in which the pupil usually took apart.", "warning");
                    txt_game_played_of_extra4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_general_conduct4.Text == "")
                {
                    Alertme("Please enter General conduct.", "warning");
                    txt_general_conduct4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_date_of_application4.Text == "")
                {
                    Alertme("Please enter Date of application for certificate.", "warning");
                    txt_date_of_application4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_date_of_issue_certificate4.Text == "")
                {
                    Alertme("Please enter Date of issue of certificate.", "warning");
                    txt_date_of_issue_certificate4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else if (txt_reason_for_leaving4.Text == "")
                {
                    Alertme("Please enter Reason for leaving the school.", "warning");
                    txt_reason_for_leaving4.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                }
                else
                {
                    update_tc_fourth();
                    Alertme("Certificate details have been updated successfully.", "success");
                    Bind_data(ViewState["query"].ToString());
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void update_tc_fourth()
        {
            SqlCommand cmd;
            string query = "update Transfer_certificate set Date_of_births=@Date_of_births,Nationality=@Nationality,class_in_last_studied=@class_in_last_studied,annual_exam_taken_with_result=@annual_exam_taken_with_result,whether_failed_in_same_class=@whether_failed_in_same_class,subject_studied=@subject_studied,optional_subject=@optional_subject,whether_qualified_for_promotion=@whether_qualified_for_promotion,ttl_working_days=@ttl_working_days,present_days=@present_days,ncc_cadet=@ncc_cadet,game_played_of_extra=@game_played_of_extra,general_conduct=@general_conduct,date_of_application=@date_of_application,date_of_issue_certificate=@date_of_issue_certificate,reason_for_leaving=@reason_for_leaving,Date_of_admission_and_class=@Date_of_admission_and_class,Board_roll_no=@Board_roll_no,Cbse_reg_no=@Cbse_reg_no,Belongs_t_sc_st_obc=@Belongs_t_sc_st_obc,Aadhar_no=@Aadhar_no,Student_PEN=@Student_PEN,any_other_remarks=@any_other_remarks where Session_id=@Session_id and Class_id=@Class_id and Admission_no=@Admission_no and Certificate_no=@Certificate_no";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ViewState["sessioN"].ToString());
            cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
            cmd.Parameters.AddWithValue("@Admission_no", ViewState["adm_no"].ToString());
            cmd.Parameters.AddWithValue("@Certificate_no", ViewState["certificate_no"].ToString());
            cmd.Parameters.AddWithValue("@Date_of_births", txt_date_of_birth4.Text);
            cmd.Parameters.AddWithValue("@Nationality", txt_nationality_4.Text);
            cmd.Parameters.AddWithValue("@class_in_last_studied", txt_class_in_last_studied4.Text);
            cmd.Parameters.AddWithValue("@annual_exam_taken_with_result", txt_annual_exam_taken_with_result4.Text);
            cmd.Parameters.AddWithValue("@whether_failed_in_same_class", txt_whether_failed_in_same_class4.Text);
            cmd.Parameters.AddWithValue("@subject_studied", txt_subject_studied4.Text);
            cmd.Parameters.AddWithValue("@optional_subject", txt_optional_subject4.Text);
            cmd.Parameters.AddWithValue("@whether_qualified_for_promotion", txt_whether_qualified_for_promotion4.Text);
            cmd.Parameters.AddWithValue("@ttl_working_days", txt_ttl_working_days4.Text);
            cmd.Parameters.AddWithValue("@present_days", txt_present_days4.Text);
            cmd.Parameters.AddWithValue("@ncc_cadet", txt_ncc_cadet4.Text);
            cmd.Parameters.AddWithValue("@game_played_of_extra", txt_game_played_of_extra4.Text);
            cmd.Parameters.AddWithValue("@general_conduct", txt_general_conduct4.Text);
            cmd.Parameters.AddWithValue("@date_of_application", txt_date_of_application4.Text);
            cmd.Parameters.AddWithValue("@date_of_issue_certificate", txt_date_of_issue_certificate4.Text);
            cmd.Parameters.AddWithValue("@reason_for_leaving", txt_reason_for_leaving4.Text);
            cmd.Parameters.AddWithValue("@Date_of_admission_and_class", txt_date_of_admission4.Text);
            cmd.Parameters.AddWithValue("@Board_roll_no", txt_board_roll_no4.Text);
            cmd.Parameters.AddWithValue("@Cbse_reg_no", txt_cbse_reg_no4.Text);
            cmd.Parameters.AddWithValue("@Belongs_t_sc_st_obc", txt_belong_to_sc_st_obc4.Text);
            cmd.Parameters.AddWithValue("@Aadhar_no", txt_aadhar_no.Text);
            cmd.Parameters.AddWithValue("@Student_PEN", txt_student_pen.Text);
            cmd.Parameters.AddWithValue("@any_other_remarks", txt_any_other_remarks4.Text);
            if (My.InsertUpdateData(cmd))
            {
                My.exeSql("update admission_registor set studentname='" + txt_name4.Text + "', fathername='" + txt_father_name4.Text + "',mothername='" + txt_mother_name4.Text + "',religion='" + ddl_religion4.Text + "',dob='" + txt_date_of_birth4.Text + "' where Session_id='" + ViewState["sessioN"].ToString() + "' and admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                string desc = "Transfer certificate updated for certificate no. : " + ViewState["certificate_no"].ToString() + " by " + ViewState["Userid"].ToString();
                log_hostory.edit_log(ViewState["sessioN"].ToString(), ViewState["class_id"].ToString(), ViewState["adm_no"].ToString(), "TransferCertificateEdit", desc, "transfer-certificate-report.aspx", ViewState["Userid"].ToString());
            }
        }

        protected void btn_update_certificate_six_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_6();", true);
                if (txt_date_of_admission_6.Text == "")
                {
                    Alertme("Please enter date of admission.", "warning");
                    txt_date_of_admission_6.Focus();
                }

                else if (txt_class_in_last_studied_6.Text == "")
                {
                    Alertme("Please enter class in which the pupil last studied.", "warning");
                    txt_class_in_last_studied_6.Focus();
                }
                else if (txt_annual_exam_taken_with_result_6.Text == "")
                {
                    Alertme("Please enter Examination Result of the class last studied", "warning");
                    txt_annual_exam_taken_with_result.Focus();
                }

                else if (txt_subject_studied_6.Text == "")
                {
                    Alertme("Please enter Subject studied, Compulsory.", "warning");
                    txt_subject_studied.Focus();
                }
                else if (txt_whether_qualified_for_promotion_6.Text == "")
                {
                    Alertme("Please enter Whether qualified for promotion / to which class.", "warning");
                    txt_whether_qualified_for_promotion.Focus();
                }
                else if (txt_ttl_working_days_6.Text == "")
                {
                    Alertme("Please enter Total no. of working days.", "warning");
                    txt_ttl_working_days.Focus();
                }
                else if (txt_present_days_6.Text == "")
                {
                    Alertme("Please enter Total no. of working days present.", "warning");
                    txt_present_days.Focus();
                }


                else if (txt_general_conduct_6.Text == "")
                {
                    Alertme("Please enter General conduct.", "warning");
                    txt_general_conduct.Focus();
                }
                else if (txt_date_of_application_6.Text == "")
                {
                    Alertme("Please enter Date of application for certificate.", "warning");
                    txt_date_of_application.Focus();
                }
                else if (txt_date_of_issue_certificate_6.Text == "")
                {
                    Alertme("Please enter Date of issue of certificate.", "warning");
                    txt_date_of_issue_certificate.Focus();
                }
                else if (txt_reason_for_leaving_6.Text == "")
                {
                    Alertme("Please enter Reason for leaving the school.", "warning");
                    txt_reason_for_leaving.Focus();
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Transfer_certificate set class_in_last_studied=@class_in_last_studied,annual_exam_taken_with_result=@annual_exam_taken_with_result,subject_studied=@subject_studied,optional_subject=@optional_subject,whether_qualified_for_promotion=@whether_qualified_for_promotion,ttl_working_days=@ttl_working_days,present_days=@present_days,general_conduct=@general_conduct,date_of_application=@date_of_application,date_of_issue_certificate=@date_of_issue_certificate,reason_for_leaving=@reason_for_leaving,Date_of_admission_and_class=@Date_of_admission_and_class,Belongs_t_sc_st_obc=@Belongs_t_sc_st_obc,Student_PEN=@Student_PEN where Certificate_no = @Certificate_no";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Certificate_no", ViewState["certificate_no"].ToString());
                    cmd.Parameters.AddWithValue("@class_in_last_studied", txt_class_in_last_studied_6.Text);
                    cmd.Parameters.AddWithValue("@annual_exam_taken_with_result", txt_annual_exam_taken_with_result_6.Text);
                    cmd.Parameters.AddWithValue("@subject_studied", txt_subject_studied_6.Text);
                    cmd.Parameters.AddWithValue("@optional_subject", txt_optional_subject_6.Text);
                    cmd.Parameters.AddWithValue("@whether_qualified_for_promotion", txt_whether_qualified_for_promotion_6.Text);
                    cmd.Parameters.AddWithValue("@ttl_working_days", txt_ttl_working_days_6.Text);
                    cmd.Parameters.AddWithValue("@present_days", txt_present_days_6.Text);
                    cmd.Parameters.AddWithValue("@general_conduct", txt_general_conduct_6.Text);
                    cmd.Parameters.AddWithValue("@date_of_application", txt_date_of_application_6.Text);
                    cmd.Parameters.AddWithValue("@date_of_issue_certificate", txt_date_of_issue_certificate_6.Text);
                    cmd.Parameters.AddWithValue("@reason_for_leaving", txt_reason_for_leaving_6.Text);
                    if (ddl_class_c_6.SelectedItem.Text == "Select")
                    {
                        cmd.Parameters.AddWithValue("@Date_of_admission_and_class", txt_date_of_admission_6.Text);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Date_of_admission_and_class", txt_date_of_admission_6.Text + " in " + ddl_class_c_6.SelectedItem.Text);
                    }
                    cmd.Parameters.AddWithValue("@Belongs_t_sc_st_obc", txt_belong_to_sc_st_obc_6.Text);
                    cmd.Parameters.AddWithValue("@Student_PEN", txt_pen_no_6.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                        mycode.executequery("update admission_registor set Status='0',Student_pen_no='" + txt_pen_no_6.Text + "' where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                        Alertme("Certificate details have been updated successfully.", "success");
                        Bind_data(ViewState["query"].ToString());

                        txt_class_in_last_studied_6.Text = "";
                        txt_annual_exam_taken_with_result_6.Text = "";
                        txt_subject_studied_6.Text = "";
                        txt_optional_subject_6.Text = "";
                        txt_whether_qualified_for_promotion_6.Text = "";
                        txt_ttl_working_days_6.Text = "";
                        txt_present_days_6.Text = "";

                        txt_general_conduct_6.Text = "";
                        txt_date_of_application_6.Text = "";
                        txt_date_of_issue_certificate_6.Text = "";
                        txt_reason_for_leaving_6.Text = "";
                        txt_date_of_admission_6.Text = "";

                        txt_date_of_admission_6.Text = "";
                        txt_pen_no_6.Text = "";
                        txt_belong_to_sc_st_obc_6.Text = "";


                    }



                }

            }

            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }

        }

        protected void btn_create_certificate_7_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_7();", true);
                if (txt_belong_to_sc_st_obc_7.Text == "")
                {
                    Alertme("Please enter candidate belongs to SC/ST/OBC", "warning");
                    txt_belong_to_sc_st_obc_7.Focus();
                }
                if (txt_date_of_admission_7.Text == "")
                {
                    Alertme("Please enter date of admission.", "warning");
                    txt_date_of_admission_7.Focus();
                }
                if (ddl_class_c_7.Text == "Select")
                {
                    Alertme("Please enter date of class.", "warning");
                    txt_date_of_admission_7.Focus();
                }
                else if (txt_class_in_last_studied_7.Text == "")
                {
                    Alertme("Please enter class in which the pupil last studied.", "warning");
                    txt_class_in_last_studied_7.Focus();
                }
                else if (txt_annual_exam_taken_with_result_7.Text == "")
                {
                    Alertme("Please enter School / Birth Annual Examination last taken with result.", "warning");
                    txt_annual_exam_taken_with_result_7.Focus();
                }
                else if (txt_whether_failed_in_same_class_7.Text == "")
                {
                    Alertme("Please enter Whether failed in same class.", "warning");
                    txt_whether_failed_in_same_class_7.Focus();
                }
                else if (txt_subject_studied_7.Text == "")
                {
                    Alertme("Please enter Subject studied, Compulsory.", "warning");
                    txt_subject_studied_7.Focus();
                }
                else if (txt_whether_qualified_for_promotion_7.Text == "")
                {
                    Alertme("Please enter Whether qualified for promotion / to which class.", "warning");
                    txt_whether_qualified_for_promotion_7.Focus();
                }

                else if (txt_ncc_cadet_7.Text == "")
                {
                    Alertme("Please enter Whether NCC Cadet / Boy Scout / Girl Guide.", "warning");
                    txt_ncc_cadet.Focus();
                }
                else if (txt_game_played_of_extra_7.Text == "")
                {
                    Alertme("Please enter Games played or extra curricular activities in which the pupil usually took apart.", "warning");
                    txt_game_played_of_extra_7.Focus();
                }
                else if (txt_general_conduct_7.Text == "")
                {
                    Alertme("Please enter General conduct.", "warning");
                    txt_general_conduct.Focus();
                }
                else if (txt_date_of_application_7.Text == "")
                {
                    Alertme("Please enter Date of application for certificate.", "warning");
                    txt_date_of_application.Focus();
                }
                else if (txt_date_of_issue_certificate_7.Text == "")
                {
                    Alertme("Please enter Date of issue of certificate.", "warning");
                    txt_date_of_issue_certificate_7.Focus();
                }
                else if (txt_reason_for_leaving_7.Text == "")
                {
                    Alertme("Please enter Reason for leaving the school.", "warning");
                    txt_reason_for_leaving_7.Focus();
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Transfer_certificate set class_in_last_studied=@class_in_last_studied,annual_exam_taken_with_result=@annual_exam_taken_with_result,subject_studied=@subject_studied,optional_subject=@optional_subject,whether_qualified_for_promotion=@whether_qualified_for_promotion,ttl_working_days=@ttl_working_days,present_days=@present_days,general_conduct=@general_conduct,date_of_application=@date_of_application,date_of_issue_certificate=@date_of_issue_certificate,reason_for_leaving=@reason_for_leaving,Date_of_admission_and_class=@Date_of_admission_and_class,Belongs_t_sc_st_obc=@Belongs_t_sc_st_obc,Student_PEN=@Student_PEN where Certificate_no = @Certificate_no";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Certificate_no", ViewState["certificate_no"].ToString());
                    cmd.Parameters.AddWithValue("@class_in_last_studied", txt_class_in_last_studied_7.Text);
                    cmd.Parameters.AddWithValue("@annual_exam_taken_with_result", txt_annual_exam_taken_with_result_7.Text);
                    cmd.Parameters.AddWithValue("@subject_studied", txt_subject_studied_7.Text);
                    cmd.Parameters.AddWithValue("@optional_subject", txt_optional_subject_7.Text);
                    cmd.Parameters.AddWithValue("@whether_qualified_for_promotion", txt_whether_qualified_for_promotion_7.Text);
                    cmd.Parameters.AddWithValue("@ttl_working_days", "");
                    cmd.Parameters.AddWithValue("@present_days", "");
                    cmd.Parameters.AddWithValue("@general_conduct", txt_general_conduct_7.Text);
                    cmd.Parameters.AddWithValue("@date_of_application", txt_date_of_application_7.Text);
                    cmd.Parameters.AddWithValue("@date_of_issue_certificate", txt_date_of_issue_certificate_7.Text);
                    cmd.Parameters.AddWithValue("@reason_for_leaving", txt_reason_for_leaving_7.Text);
                    if (ddl_class_c_6.SelectedItem.Text == "Select")
                    {
                        cmd.Parameters.AddWithValue("@Date_of_admission_and_class", txt_date_of_admission_7.Text);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Date_of_admission_and_class", txt_date_of_admission_7.Text + " in " + ddl_class_c_7.SelectedItem.Text);
                    }
                    cmd.Parameters.AddWithValue("@Belongs_t_sc_st_obc", txt_belong_to_sc_st_obc_7.Text);
                    cmd.Parameters.AddWithValue("@Student_PEN", txt_pen_no_7.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                        if (txt_pen_no_7.Text == "")
                        {
                        }
                        else
                        {
                            mycode.executequery("update admission_registor set Status='0',Student_pen_no='" + txt_pen_no_7.Text + "' where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                        }
                        Alertme("Certificate details have been updated successfully.", "success");
                        Bind_data(ViewState["query"].ToString());
                        txt_class_in_last_studied_7.Text = "";
                        txt_annual_exam_taken_with_result_7.Text = "";
                        txt_subject_studied_7.Text = "";
                        txt_optional_subject_7.Text = "";
                        txt_whether_qualified_for_promotion_7.Text = "";
                        txt_general_conduct_7.Text = "";
                        txt_date_of_application_7.Text = "";
                        txt_date_of_issue_certificate_7.Text = "";
                        txt_reason_for_leaving_7.Text = "";
                        txt_date_of_admission_7.Text = "";
                        txt_date_of_admission_7.Text = "";
                        txt_pen_no_7.Text = "";
                        txt_belong_to_sc_st_obc_7.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        #region certificate no 8
        protected void btn_create_certificate_8_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_8();", true);
                if (txt_belong_to_sc_st_obc_8.Text == "")
                {
                    Alertme("Please enter whether the student belongs to schedule caste or scheduled trible ", "warning");
                    txt_belong_to_sc_st_obc_8.Focus();
                }

                if (txt_date_of_admission_8.Text == "")
                {
                    Alertme("Please enter Date of admission in school with class", "warning");
                    txt_date_of_admission_8.Focus();
                }

                else if (txt_class_in_last_studied_8.Text == "")
                {
                    Alertme("Please enter class in which the student last studied", "warning");
                    txt_class_in_last_studied_8.Focus();
                }
                else if (txt_whether_qualified_for_promotion_8.Text == "")
                {
                    Alertme("Please enter whether qualified for promotion to the higher class", "warning");
                    txt_whether_qualified_for_promotion_8.Focus();
                }
                else if (txt_monthuptopaid_8.Text == "")
                {
                    Alertme("Please enter month Upto which school dues paid", "warning");
                    txt_monthuptopaid_8.Focus();
                }
                else if (txt_general_conduct_8.Text == "")
                {
                    Alertme("Please enter general conduct", "warning");
                    txt_general_conduct_8.Focus();
                }
                else if (txt_date_of_left_school_8.Text == "")
                {
                    Alertme("Please enter date on which the student left school", "warning");
                    txt_date_of_left_school_8.Focus();
                }
                else if (txt_date_of_application_8.Text == "")
                {
                    Alertme("Please enter date of application of the certificate", "warning");
                    txt_date_of_application.Focus();
                }
                else if (txt_date_of_issue_certificate_8.Text == "")
                {
                    Alertme("Please enter date of issue of this certificate.", "warning");
                    txt_date_of_issue_certificate_8.Focus();
                }
                else if (txt_reason_for_leaving_8.Text == "")
                {
                    Alertme("Please enter Reason for leaving the school.", "warning");
                    txt_reason_for_leaving_8.Focus();
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Transfer_certificate set Date_of_birth=@Date_of_birth,class_in_last_studied=@class_in_last_studied,whether_qualified_for_promotion=@whether_qualified_for_promotion,general_conduct=@general_conduct,Data_of_left_school=@Data_of_left_school,Monthuptopaid=@Monthuptopaid,date_of_application=@date_of_application,date_of_issue_certificate=@date_of_issue_certificate,reason_for_leaving=@reason_for_leaving,Date_of_admission_and_class=@Date_of_admission_and_class,Belongs_t_sc_st_obc=@Belongs_t_sc_st_obc,Student_PEN=@Student_PEN,any_other_remarks=@any_other_remarks  where Certificate_no = @Certificate_no";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Date_of_birth", txt_dob_8.Text);
                    cmd.Parameters.AddWithValue("@Certificate_no", ViewState["certificate_no"].ToString());
                    cmd.Parameters.AddWithValue("@class_in_last_studied", txt_class_in_last_studied_8.Text);
                    cmd.Parameters.AddWithValue("@general_conduct", txt_general_conduct_8.Text);
                    cmd.Parameters.AddWithValue("@whether_qualified_for_promotion", txt_whether_qualified_for_promotion_8.Text);
                    cmd.Parameters.AddWithValue("@Data_of_left_school", txt_date_of_left_school_8.Text);
                    cmd.Parameters.AddWithValue("@date_of_application", txt_date_of_application_8.Text);
                    cmd.Parameters.AddWithValue("@date_of_issue_certificate", txt_date_of_issue_certificate_8.Text);
                    cmd.Parameters.AddWithValue("@Monthuptopaid", txt_monthuptopaid_8.Text);
                    cmd.Parameters.AddWithValue("@reason_for_leaving", txt_reason_for_leaving_8.Text);
                    if (ddl_class_c_8.SelectedItem.Text == "Select")
                    {
                        cmd.Parameters.AddWithValue("@Date_of_admission_and_class", txt_date_of_admission_8.Text);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Date_of_admission_and_class", txt_date_of_admission_8.Text + " in " + ddl_class_c_8.SelectedItem.Text);
                    }
                    cmd.Parameters.AddWithValue("@Belongs_t_sc_st_obc", txt_belong_to_sc_st_obc_8.Text);
                    cmd.Parameters.AddWithValue("@Student_PEN", txt_pen_no_8.Text);
                    cmd.Parameters.AddWithValue("@any_other_remarks", txt_any_other_remarks_8.Text);


                    if (My.InsertUpdateData(cmd))
                    {
                        mycode.executequery("update admission_registor set Status='0',dob='" + txt_dob_8.Text + "',Student_pen_no='" + txt_pen_no_8.Text + "'   where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                        Response.Redirect("slip/TC/transfer-certificate-008.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + ViewState["certificate_no"].ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        #endregion

        protected void btn_make_tcTen_update_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTEN();", true);
                if (txt_dob_ten.Text == "")
                {
                    Alertme("Please enter date of birth", "warning");
                    txt_dob_ten.Focus();
                }
                else if (txt_dob_in_word.Text == "")
                {
                    Alertme("Please enter date of birth in word", "warning");
                    txt_dob_in_word.Focus();
                }
                else if (txt_adm_class_ten.Text == "")
                {
                    Alertme("Please enter admission in class", "warning");
                    txt_adm_class_ten.Focus();
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Transfer_certificate set class_in_last_studied=@class_in_last_studied,whether_qualified_for_promotion=@whether_qualified_for_promotion,general_conduct=@general_conduct,date_of_application=@date_of_application,date_of_issue_certificate=@date_of_issue_certificate,reason_for_leaving=@reason_for_leaving,any_other_remarks=@any_other_remarks,Date_of_admission_and_class=@Date_of_admission_and_class,Belongs_t_sc_st_obc=@Belongs_t_sc_st_obc,Nationality=@Nationality,Date_of_births=@Date_of_births,Date_of_birth_word=@Date_of_birth_word,annual_exam_taken_with_result=@annual_exam_taken_with_result,whether_failed_in_same_class=@whether_failed_in_same_class,subject_studied=@subject_studied,Dues_fee=@Dues_fee,Concession=@Concession,ncc_cadet=@ncc_cadet,game_played_of_extra=@game_played_of_extra,ttl_working_days=@ttl_working_days,present_days=@present_days where Admission_no=@Admission_no and Certificate_no=@Certificate_no and Certificate_type='Transfer'";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@class_in_last_studied", txt_class_last_studies_ten.Text);
                    cmd.Parameters.AddWithValue("@whether_qualified_for_promotion", txt_qualified_higher_class_ten.Text);
                    cmd.Parameters.AddWithValue("@general_conduct", txt_general_conduct_ten.Text);
                    cmd.Parameters.AddWithValue("@date_of_application", txt_date_of_application_ten.Text);
                    cmd.Parameters.AddWithValue("@date_of_issue_certificate", txt_date_of_issue_ten.Text);
                    cmd.Parameters.AddWithValue("@reason_for_leaving", txt_reason_for_leaving_ten.Text);
                    cmd.Parameters.AddWithValue("@any_other_remarks", txt_any_other_remarks_ten.Text);
                    cmd.Parameters.AddWithValue("@Date_of_admission_and_class", txt_date_of_adm_ten.Text + ", " + txt_adm_class_ten.Text);
                    cmd.Parameters.AddWithValue("@Belongs_t_sc_st_obc", txt_belongs_to_scst_ten.Text);
                    cmd.Parameters.AddWithValue("@Nationality", txt_nationality_ten.Text);
                    cmd.Parameters.AddWithValue("@Date_of_births", txt_dob_ten.Text);
                    cmd.Parameters.AddWithValue("@Date_of_birth_word", txt_dob_in_word.Text);
                    cmd.Parameters.AddWithValue("@annual_exam_taken_with_result", txt_school_board_exam_taken_ten.Text);
                    cmd.Parameters.AddWithValue("@whether_failed_in_same_class", txt_failed_once_twice_ten.Text);
                    cmd.Parameters.AddWithValue("@subject_studied", txt_subj_studies_ten.Text);
                    cmd.Parameters.AddWithValue("@Dues_fee", txt_paid_fee_till_month_ten.Text);
                    cmd.Parameters.AddWithValue("@Concession", txt_any_fee_concession_ten.Text);
                    cmd.Parameters.AddWithValue("@ncc_cadet", txt_ncc_cadet_by_ten.Text); 
                    cmd.Parameters.AddWithValue("@game_played_of_extra", txt_games_played.Text);
                    cmd.Parameters.AddWithValue("@ttl_working_days", txt_ttl_no_of_workind_days_ten.Text);
                    cmd.Parameters.AddWithValue("@present_days", txt_ttl_persent_days.Text);
                    cmd.Parameters.AddWithValue("@Certificate_no", ViewState["certificate_no"].ToString());
                    cmd.Parameters.AddWithValue("@Admission_no", ViewState["adm_no"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        mycode.executequery("update admission_registor set dob='" + txt_dob_ten.Text + "' where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                        Response.Redirect("slip/TC/transfer-certificate-010.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + ViewState["certificate_no"].ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
    }
}