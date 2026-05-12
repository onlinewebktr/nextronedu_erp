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
    public partial class print_SLC_certificate : System.Web.UI.Page
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
                        string pagename_current = "certificate-creation.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by session_id asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddl_class_c_8, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        ddlsession.SelectedValue = My.get_session_id();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        find_tc_type();
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
            hd_tc_type.Value = "8";
            DataTable dt = My.dataTable("select * from SLC_certificate_setting where Status=1");
            if (dt.Rows.Count > 0)
            {
                hd_tc_type.Value = dt.Rows[0]["Tc_type_id"].ToString();

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
                query = "select * from dbo.[Transfer_certificate] t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Certificate_type='SLC'    order by t2.rollnumber asc";
            }
            else
            {
                query = "select * from dbo.[Transfer_certificate] t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Class_id=" + ddlclass.SelectedValue + " and t1.Certificate_type='SLC'   order by t2.rollnumber asc";

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
                    string query = "select * from dbo.[Transfer_certificate] t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Admission_no='" + txt_admission_no.Text + "' and t1.Certificate_type='SLC'   order by t2.rollnumber asc";
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
                if (hd_tc_type.Value == "8")
                {
                    tcc_link.HRef = "slip/TC/slc008.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
                }
                if (hd_tc_type.Value == "10")
                {
                    tcc_link.HRef = "slip/TC/leaving-certificate-010.aspx?adm_no=" + lbl_admissionserialnumber.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_nos.Text;
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
                    if (hd_tc_type.Value == "8")
                    {
                        Repeater_8.DataSource = dt;
                        Repeater_8.DataBind();

                        txt_dob_8.Text = dt.Rows[0]["dob"].ToString();

                        DataTable dtT = My.dataTable("select * from Transfer_certificate where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and  Certificate_type='SLC' ");
                        if (dtT.Rows.Count > 0)
                        {
                            txt_pen_no_8.Text = dtT.Rows[0]["Student_PEN"].ToString();
                            txt_belong_to_sc_st_obc_8.Text = dtT.Rows[0]["Belongs_t_sc_st_obc"].ToString();
                            txt_date_of_admission_8.Text = dtT.Rows[0]["Date_of_admission_and_class"].ToString();
                            txt_class_in_last_studied_8.Text = dtT.Rows[0]["class_in_last_studied"].ToString();
                            txt_whether_qualified_for_promotion_8.Text = dtT.Rows[0]["whether_qualified_for_promotion"].ToString();
                            txt_monthuptopaid_8.Text = dtT.Rows[0]["Monthuptopaid"].ToString();
                            txt_general_conduct_8.Text = dtT.Rows[0]["general_conduct"].ToString();
                            txt_date_of_application_8.Text = dtT.Rows[0]["date_of_application"].ToString();
                            txt_date_of_issue_certificate_8.Text = dtT.Rows[0]["date_of_issue_certificate"].ToString();
                            txt_reason_for_leaving_8.Text = dtT.Rows[0]["reason_for_leaving"].ToString();
                            txt_any_other_remarks_8.Text = dtT.Rows[0]["any_other_remarks"].ToString();
                            txt_annual_exam_taken_with_result_8.Text = dtT.Rows[0]["annual_exam_taken_with_result"].ToString();
                            txt_whether_failed_in_same_class_8.Text = dtT.Rows[0]["whether_failed_in_same_class"].ToString();
                            txt_subject_studied_8.Text = dtT.Rows[0]["subject_studied"].ToString();
                            txt_permote_class.Text = dtT.Rows[0]["Promotion_class"].ToString();
                            txt_ttl_working_days_8.Text = dtT.Rows[0]["ttl_working_days"].ToString();
                            txt_present_days_8.Text = dtT.Rows[0]["present_days"].ToString();
                            txt_ncc_cadet_8.Text = dtT.Rows[0]["ncc_cadet"].ToString();
                            txt_game_played_of_extra_8.Text = dtT.Rows[0]["game_played_of_extra"].ToString();

                            txt_general_conduct_8.Text = dtT.Rows[0]["general_conduct"].ToString();

                            txt_date_of_application_8.Text = dtT.Rows[0]["date_of_application"].ToString();
                            txt_date_of_issue_certificate_8.Text = dtT.Rows[0]["date_of_issue_certificate"].ToString();
                            txt_reason_for_leaving_8.Text = dtT.Rows[0]["reason_for_leaving"].ToString();

                            txt_any_other_remarks_8.Text = dtT.Rows[0]["any_other_remarks"].ToString();

                            txt_concession.Text = dtT.Rows[0]["concession"].ToString();
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_8();", true);
                    }
                    if (hd_tc_type.Value == "10")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTEN();", true);
                        rp_trnsf_six.DataSource = dt;
                        rp_trnsf_six.DataBind();

                        DataTable dtT = My.dataTable("select * from Transfer_certificate where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and  Certificate_type='SLC'");
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

                My.exeSql("delete from Transfer_certificate where Admission_no='" + lbl_adm_no.Text + "' and Certificate_no='" + lbl_Certificate_no.Text + "' and Certificate_type='SLC'; update admission_registor set Status='1',Is_TC_Taken=0 where admissionserialnumber='" + lbl_adm_no.Text + "' and  Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "'");
                string desc = "Transfer certificate deleted for certificate no. : " + lbl_Certificate_no.Text + " & user id : " + ViewState["Userid"].ToString();
                log_hostory.delete_log(lbl_session_id.Text, lbl_class_id.Text, lbl_adm_no.Text, "SLCDelete", desc, "print-SLC-certificate-report.aspx", ViewState["Userid"].ToString());
                Alertme("SLC Certificate has been deleted successfully.", "success");
                bind_created_certificate();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_create_certificate_8_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_8();", true);
                if (txt_belong_to_sc_st_obc_8.Text == "")
                {
                    Alertme("Please enter whether the candidate belongs to schedule caste or schedule trible or OBC", "warning");
                    txt_belong_to_sc_st_obc_8.Focus();
                }
                if (txt_date_of_admission_8.Text == "")
                {
                    Alertme("Please enter date of admission.", "warning");
                    txt_date_of_admission_8.Focus();
                }

                else if (txt_class_in_last_studied_8.Text == "")
                {
                    Alertme("Please enter class in which the pupil last studied", "warning");
                    txt_class_in_last_studied_8.Focus();
                }
                else if (txt_annual_exam_taken_with_result_8.Text == "")
                {
                    Alertme("Please enter School/Board Annual examination last taken with result", "warning");
                    txt_annual_exam_taken_with_result_8.Focus();
                }
                else if (txt_whether_failed_in_same_class_8.Text == "")
                {
                    Alertme("Please enter whether failed, if so once/twice in the same class", "warning");
                    txt_whether_failed_in_same_class_8.Focus();
                }
                else if (txt_subject_studied_8.Text == "")
                {
                    Alertme("Please enter subject studied", "warning");
                    txt_subject_studied_8.Focus();
                }
                else if (txt_whether_qualified_for_promotion_8.Text == "")
                {
                    Alertme("Please enter whether qualified for promotion to the higher class.", "warning");
                    txt_whether_qualified_for_promotion_8.Focus();
                }
                else if (txt_permote_class.Text == "Select")
                {
                    Alertme("Please enter if so, to which class", "warning");
                    txt_permote_class.Focus();
                }

                else if (txt_monthuptopaid_8.Text == "")
                {
                    Alertme("Please enter month Upto which the pupil has paid school", "warning");
                    txt_monthuptopaid_8.Focus();
                }


                else if (txt_ttl_working_days_8.Text == "")
                {
                    Alertme("Please enter total working days in the academic session", "warning");
                    txt_ttl_working_days_8.Focus();
                }
                else if (txt_present_days_8.Text == "")
                {
                    Alertme("Please enter total no. of days pupil present in the school", "warning");
                    txt_present_days_8.Focus();
                }
                else if (txt_ncc_cadet_8.Text == "")
                {
                    Alertme("Please enter whether NCC Cadet/Boy Scout/Girl Guide", "warning");
                    txt_ncc_cadet_8.Focus();
                }
                else if (txt_game_played_of_extra_8.Text == "")
                {
                    Alertme("Please enter games played or extra curricular activites in which the pupil usually took apart", "warning");
                    txt_game_played_of_extra_8.Focus();
                }
                else if (txt_general_conduct_8.Text == "")
                {
                    Alertme("Please enter General conduct.", "warning");
                    txt_general_conduct_8.Focus();
                }
                else if (txt_date_of_application_8.Text == "")
                {
                    Alertme("Please enter date of application for certificate", "warning");
                    txt_date_of_application_8.Focus();
                }
                else if (txt_date_of_issue_certificate_8.Text == "")
                {
                    Alertme("Please enter Date of issue of certificate.", "warning");
                    txt_date_of_issue_certificate_8.Focus();
                }
                else if (txt_reason_for_leaving_8.Text == "")
                {
                    Alertme("Please enter Reason for leaving the school.", "warning");
                    txt_reason_for_leaving_8.Focus();
                }
                else if (txt_any_other_remarks_8.Text == "")
                {
                    Alertme("Please enter any other remarks", "warning");
                    txt_any_other_remarks_8.Focus();
                }
                else
                {

                    SqlDataAdapter ad = new SqlDataAdapter("select * from Transfer_certificate where  Admission_no='" + ViewState["adm_no"].ToString() + "'   Certificate_no='" + ViewState["certificate_no"].ToString() + "' and Certificate_type='SLC'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Transfer_certificate");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["Create_date"] = mycode.date();
                            dr["Create_idate"] = mycode.idate();
                            dr["User_id"] = Session["Admin"].ToString();
                            dr["Belongs_t_sc_st_obc"] = txt_belong_to_sc_st_obc_8.Text;
                            if (ddl_class_c_8.SelectedItem.Text == "Select")
                            {
                                dr["Date_of_admission_and_class"] = txt_date_of_admission_8.Text;
                            }
                            else
                            {
                                dr["Date_of_admission_and_class"] = txt_date_of_admission_8.Text + " in " + ddl_class_c_8.SelectedItem.Text;
                            }
                            dr["class_in_last_studied"] = txt_class_in_last_studied_8.Text;
                            dr["annual_exam_taken_with_result"] = txt_annual_exam_taken_with_result_8.Text;
                            dr["whether_failed_in_same_class"] = txt_whether_failed_in_same_class_8.Text;
                            dr["subject_studied"] = txt_subject_studied_8.Text;
                            dr["whether_qualified_for_promotion"] = txt_whether_qualified_for_promotion_8.Text;
                            dr["Promotion_class"] = txt_permote_class.Text;
                            dr["Monthuptopaid"] = txt_monthuptopaid_8.Text;
                            dr["ttl_working_days"] = txt_ttl_working_days_8.Text;
                            dr["present_days"] = txt_present_days_8.Text;
                            dr["ncc_cadet"] = txt_ncc_cadet_8.Text;
                            dr["game_played_of_extra"] = txt_game_played_of_extra_8.Text;
                            dr["general_conduct"] = txt_general_conduct_8.Text;
                            dr["date_of_application"] = txt_date_of_application_8.Text;
                            dr["date_of_issue_certificate"] = txt_date_of_issue_certificate_8.Text;
                            dr["reason_for_leaving"] = txt_reason_for_leaving_8.Text;
                            dr["any_other_remarks"] = txt_any_other_remarks_8.Text;
                            dr["Student_PEN"] = txt_pen_no_8.Text;
                            dr["Concession"] = txt_concession.Text;
                        }
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        mycode.executequery("update admission_registor set Status='0',Student_pen_no='" + txt_pen_no_8.Text + "' where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                        Response.Redirect("slip/TC/slc008.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + ViewState["certificate_no"].ToString(), false);

                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }



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
                    string query = "Update Transfer_certificate set class_in_last_studied=@class_in_last_studied,whether_qualified_for_promotion=@whether_qualified_for_promotion,general_conduct=@general_conduct,date_of_application=@date_of_application,date_of_issue_certificate=@date_of_issue_certificate,reason_for_leaving=@reason_for_leaving,any_other_remarks=@any_other_remarks,Date_of_admission_and_class=@Date_of_admission_and_class,Belongs_t_sc_st_obc=@Belongs_t_sc_st_obc,Nationality=@Nationality,Date_of_births=@Date_of_births,Date_of_birth_word=@Date_of_birth_word,annual_exam_taken_with_result=@annual_exam_taken_with_result,whether_failed_in_same_class=@whether_failed_in_same_class,subject_studied=@subject_studied,Dues_fee=@Dues_fee,Concession=@Concession,ncc_cadet=@ncc_cadet,game_played_of_extra=@game_played_of_extra,ttl_working_days=@ttl_working_days,present_days=@present_days where Admission_no=@Admission_no and Certificate_no=@Certificate_no and Certificate_type='SLC'";
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
                        Response.Redirect("slip/TC/leaving-certificate-010.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + ViewState["certificate_no"].ToString(), false);
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