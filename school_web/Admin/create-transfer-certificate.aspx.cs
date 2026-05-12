using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class create_transfer_certificate : System.Web.UI.Page
    {
        My mycode = new My();
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
                        hd_is_pay_tc_fee.Value = "0";
                        ViewState["gcmid"] = "";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["stdnT"] = "0";
                        find_firm_details();
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc ");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_class_c, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_class_c_6, "Select Course_Name,course_id from Add_course_table order by Position asc");

                        mycode.bind_all_ddl_with_id(ddl_class_c2, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_class_c3, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_class_c4, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_class_c_8, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_class_c_7, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        Bind_all_pending_list_for_approved();
                        find_tc_type();


                        try
                        {
                            My.exeSql("ALTER TABLE Transfer_certificate_setting ADD Is_header int;");
                            My.exeSql("ALTER TABLE Transfer_certificate_setting ADD Sub_location Header_Path (500);");
                            My.exeSql("update Transfer_certificate_setting set Is_header=0");
                        }
                        catch
                        {
                        }

                        txt_payment_date.Text = mycode.date();
                        txt_bank_date.Text = mycode.date();

                        try
                        {
                            DataTable dtBnk1 = My.dataTable("select * from bank_details wher  order by bank_name asc");
                            if (dtBnk1.Rows.Count > 0)
                            {
                                ddl_bank.DataSource = dtBnk1;
                                ddl_bank.DataTextField = "bank_name";
                                ddl_bank.DataBind();
                                ddl_bank.Items.Insert(0, new ListItem("Select", "Select"));
                            }
                            else
                            {
                                DataTable dtBnk = My.dataTable("select * from BANK_MASTER where Status='1' order by Bank_name asc");
                                if (dtBnk.Rows.Count > 0)
                                {
                                    ddl_bank.DataSource = dtBnk;
                                    ddl_bank.DataTextField = "Bank_name";
                                    ddl_bank.DataBind();
                                    ddl_bank.Items.Insert(0, new ListItem("Select", "Select"));
                                }
                                else
                                {
                                    compLN.bind_ddl_select(ddl_bank, "select Bank_name from Bank_master order by Bank_name asc");
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Transfer_certificate");
            }
        }


        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                hd_firm_id.Value = dt.Rows[0]["firm_id"].ToString();
                try
                {
                    if (dt.Rows[0]["Is_pay_tc_fee"].ToString() == "True")
                    {
                        hd_is_pay_tc_fee.Value = "1";
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }


        private void find_tc_type()
        {
            hd_tc_type.Value = "8";
            DataTable dt = My.dataTable("select * from Transfer_certificate_setting where Status=1");
            if (dt.Rows.Count > 0)
            {
                hd_tc_type.Value = dt.Rows[0]["Tc_type_id"].ToString();
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
        #region Approval For pending character certificate admisn
        private void Bind_all_pending_list_for_approved()
        {
            string query = " select ar.*,aft.Reply_remarks,format(aft.Reply_datetime, 'dd/MM/yyyy hh:mm:ss tt') as Reply_datetime1,aft.Id from dbo.[admission_registor] ar join Apply_For_TC aft on aft.Admission_no = ar.admissionserialnumber and aft.Session_id = ar.Session_id  where ar.Session_id = '" + ddl_session.SelectedValue + "' and ar.Transfer_Status in ('New', 'NT') and ar.Status = '1' and ar.admissionserialnumber not IN(SELECT Admission_no FROM Transfer_certificate WHERE Admission_no=ar.admissionserialnumber and Class_id=ar.Class_id and Session_id=ar.Session_id) and ar.admissionserialnumber in(Select Admission_no from Apply_For_TC where Apply_For = 'TC') and aft.Apply_For = 'TC'  order by ar.id desc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                pendingforgeneratedata.Visible = false;
                Repeater3.DataSource = null;
                Repeater3.DataBind();
            }
            else
            {
                pendingforgeneratedata.Visible = true;
                Repeater3.DataSource = dt;
                Repeater3.DataBind();

            }

        }
        protected void lnk_process_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                ViewState["id_process"] = lbl_Id.Text;
                string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + lbl_admissionserialnumber.Text + "' and   Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Transfer_certificate WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id) order by id desc";
                find_details(qry);

            }
            catch
            {
            }
        }
        #endregion






        protected void btn_find_session_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                string qry = "select   * from dbo.[admission_registor] where Session_id='" + ddl_session.SelectedValue + "' and Transfer_Status in ('New','NT') and Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Transfer_certificate WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Transfer') order by id desc";
                find_details(qry);
            }

        }


        #region FindStudenT
        protected void btn_find_admission_no_Click1(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose session.", "warning");
                    ddl_session.Focus();
                    return;
                }
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please Enter Admission No", "warning");
                    txt_admission_no.Focus();
                    return;
                }
                string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Transfer_certificate WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Transfer') order by id desc";
                find_details(qry);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose session.", "warning");
                    ddl_session.Focus();
                    return;
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                    return;
                }
                if (txtrollnumber.Text == "")
                {
                    Alertme("Please enter roll no.", "warning");
                    txtrollnumber.Focus();
                    return;
                }
                string query = "select * from dbo.[admission_registor] where rollnumber='" + txtrollnumber.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and Session_id='" + ddl_session.SelectedValue + "'  and Status='1'";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSTD();", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_find_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_session.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }
                string query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and Status='1' and Session_id='" + ddl_session.SelectedValue + "' order by studentname asc";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSTD();", true);
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                string query = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + lbladmissionserialnumber.Text + "'  and Session_id='" + lbl_session_id.Text + "' and admissionserialnumber not IN  (SELECT Admission_no FROM Transfer_certificate WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Transfer') order by id desc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }



        private void find_details(string qryS)
        {
            DataTable dt = My.dataTable(qryS);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist or certificate already created.", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                //txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                //ddlclass.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                //txtrollnumber.Text = dt.Rows[0]["rollnumber"].ToString();
                //txt_student_name.Text = dt.Rows[0]["studentname"].ToString();
            }
        }
        #endregion


        protected void lnkCreate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_gcmid = (Label)row.FindControl("lbl_gcmid");
                Label lbl_session = (Label)row.FindControl("lbl_session");

                ViewState["gcmid"] = lbl_gcmid.Text;
                ViewState["SessionName"] = lbl_session.Text;
                ViewState["adm_no"] = lbl_adm_no.Text;
                ViewState["sessioN"] = lbl_session_id.Text;
                ViewState["class_id"] = lbl_class_id.Text;






                string query = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + lbl_adm_no.Text + "' and Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' order by id desc";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
                else
                {
                    if (hd_tc_type.Value == "2")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                        Repeater2.DataSource = dt;
                        Repeater2.DataBind();
                        Repeater3.DataSource = dt;
                        Repeater3.DataBind();
                        txt_date_of_birth.Text = dt.Rows[0]["dob"].ToString();
                        txt_general_conduct2.Text = "GOOD";
                        txt_class_in_last_studied2.Text = dt.Rows[0]["class"].ToString() + ", " + dt.Rows[0]["session"].ToString();

                        if (dt.Rows[0]["cast"].ToString() == "Select")
                        {
                            txt_belong_to_sc_st_obc.Text = "NO";
                        }
                        else
                        {
                            txt_belong_to_sc_st_obc.Text = dt.Rows[0]["cast"].ToString();
                        }

                        try
                        {
                            string date_of_admission_and_class_id = find_date_of_admission(lbl_adm_no.Text);
                            string[] stringSeparatorss = new string[] { "~" };
                            string[] arrs = date_of_admission_and_class_id.Split(stringSeparatorss, StringSplitOptions.None);
                            string date_of_admission = arrs[0];
                            string class_id = arrs[1];


                            txt_date_of_admission2.Text = date_of_admission;
                            try
                            {
                                ddl_class_c2.SelectedValue = class_id;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        catch (Exception ex)
                        {
                        }



                        find_subject(ViewState["adm_no"].ToString(), ViewState["sessioN"].ToString(), ViewState["class_id"].ToString());
                    }
                    else if (hd_tc_type.Value == "3")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                        Repeater4.DataSource = dt;
                        Repeater4.DataBind();
                        txt_date_of_birth3.Text = dt.Rows[0]["dob"].ToString();
                        txt_general_conduct3.Text = "GOOD";
                        txt_class_in_last_studied3.Text = dt.Rows[0]["class"].ToString() + ", " + dt.Rows[0]["session"].ToString();

                        txt_Nationality3.Text = "INDIAN";

                        if (dt.Rows[0]["cast"].ToString() == "Select")
                        {
                            txt_sc_st3.Text = "NO";
                        }
                        else
                        {
                            txt_sc_st3.Text = dt.Rows[0]["cast"].ToString();
                        }


                        string date_of_admission_and_class_id = find_date_of_admission(lbl_adm_no.Text);
                        string[] stringSeparatorss = new string[] { "~" };
                        string[] arrs = date_of_admission_and_class_id.Split(stringSeparatorss, StringSplitOptions.None);
                        string date_of_admission = arrs[0];
                        string class_id = arrs[1];

                        txt_date_of_admission3.Text = date_of_admission;

                        try
                        {
                            ddl_class_c3.SelectedValue = class_id;
                        }
                        catch (Exception ex)
                        {
                        }


                        find_subject(ViewState["adm_no"].ToString(), ViewState["sessioN"].ToString(), ViewState["class_id"].ToString());
                    }
                    else if (hd_tc_type.Value == "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                        rp_tc_4.DataSource = dt;
                        rp_tc_4.DataBind();
                        txt_date_of_birth4.Text = dt.Rows[0]["dob"].ToString();
                        txt_general_conduct4.Text = "GOOD";
                        txt_class_in_last_studied4.Text = dt.Rows[0]["class"].ToString() + ", " + dt.Rows[0]["session"].ToString();

                        txt_Nationality4.Text = "INDIAN";
                        if (dt.Rows[0]["cast"].ToString() == "Select")
                        {
                            txt_belong_to_sc_st_obc4.Text = "NO";
                        }
                        else
                        {
                            txt_belong_to_sc_st_obc4.Text = dt.Rows[0]["cast"].ToString();
                        }


                        string date_of_admission_and_class_id = find_date_of_admission(lbl_adm_no.Text);
                        string[] stringSeparatorss = new string[] { "~" };
                        string[] arrs = date_of_admission_and_class_id.Split(stringSeparatorss, StringSplitOptions.None);
                        string date_of_admission = arrs[0];
                        string class_id = arrs[1];

                        txt_date_of_admission4.Text = date_of_admission;

                        try
                        {
                            ddl_class_c4.SelectedValue = class_id;
                        }
                        catch (Exception ex)
                        {
                        }
                        find_subject(ViewState["adm_no"].ToString(), ViewState["sessioN"].ToString(), ViewState["class_id"].ToString());
                    }
                    else if (hd_tc_type.Value == "6")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_6();", true);
                        Repeater1_tc6.DataSource = dt;
                        Repeater1_tc6.DataBind();

                        txt_general_conduct_6.Text = "GOOD";
                        txt_class_in_last_studied_6.Text = dt.Rows[0]["class"].ToString() + ", " + dt.Rows[0]["session"].ToString();


                        if (dt.Rows[0]["cast"].ToString() == "Select")
                        {
                            txt_belong_to_sc_st_obc_6.Text = "NO";
                        }
                        else
                        {
                            txt_belong_to_sc_st_obc_6.Text = dt.Rows[0]["cast"].ToString();
                        }


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

                        find_subject(ViewState["adm_no"].ToString(), ViewState["sessioN"].ToString(), ViewState["class_id"].ToString());
                    }
                    else if (hd_tc_type.Value == "7")
                    {
                        txt_pen_no_7.Text = dt.Rows[0]["Student_PEN"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_7();", true);
                        Repeater7.DataSource = dt;
                        Repeater7.DataBind();
                        txt_ncc_cadet_7.Text = "NA";
                        txt_general_conduct_7.Text = "GOOD";
                        txt_class_in_last_studied_7.Text = dt.Rows[0]["class"].ToString() + ", " + dt.Rows[0]["session"].ToString();
                        if (dt.Rows[0]["cast"].ToString() == "Select")
                        {
                            txt_belong_to_sc_st_obc_7.Text = "NO";
                        }
                        else
                        {
                            txt_belong_to_sc_st_obc_7.Text = dt.Rows[0]["cast"].ToString();
                        }
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

                        find_subject(ViewState["adm_no"].ToString(), ViewState["sessioN"].ToString(), ViewState["class_id"].ToString());

                    }
                    else if (hd_tc_type.Value == "8")
                    {
                        txt_pen_no_8.Text = dt.Rows[0]["Student_PEN"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP_8();", true);
                        Repeater_8.DataSource = dt;
                        Repeater_8.DataBind();
                        txt_general_conduct_8.Text = "GOOD";
                        txt_class_in_last_studied_8.Text = dt.Rows[0]["class"].ToString() + ", " + dt.Rows[0]["session"].ToString();
                        if (dt.Rows[0]["cast"].ToString() == "Select")
                        {
                            txt_belong_to_sc_st_obc_8.Text = "NO";
                        }
                        else
                        {
                            txt_belong_to_sc_st_obc_8.Text = dt.Rows[0]["cast"].ToString();
                        }
                        string date_of_admission_and_class_id = find_date_of_admission(lbl_adm_no.Text);
                        string[] stringSeparatorss = new string[] { "~" };
                        string[] arrs = date_of_admission_and_class_id.Split(stringSeparatorss, StringSplitOptions.None);
                        string date_of_admission = arrs[0];
                        string class_id = arrs[1];
                        txt_date_of_admission_8.Text = date_of_admission;
                        try
                        {
                            ddl_class_c_8.SelectedValue = class_id;
                        }
                        catch (Exception ex)
                        {
                        }

                        find_subject(ViewState["adm_no"].ToString(), ViewState["sessioN"].ToString(), ViewState["class_id"].ToString());
                    }
                    else if (hd_tc_type.Value == "10")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTCTEN();", true);
                        rp_trnsf_six.DataSource = dt;
                        rp_trnsf_six.DataBind();

                        txt_dob_ten.Text = dt.Rows[0]["dob"].ToString();
                        try
                        {
                            string result = ConvertDateToWords(dt.Rows[0]["dob"].ToString());
                            txt_dob_in_word.Text = result;
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_nationality_ten.Text = "INDIAN";
                        if (dt.Rows[0]["cast"].ToString() == "Select")
                        {
                            txt_belongs_to_scst_ten.Text = "NO";
                        }
                        else
                        {
                            if (dt.Rows[0]["cast"].ToString().ToUpper() == "SC" || dt.Rows[0]["cast"].ToString().ToUpper() == "ST" || dt.Rows[0]["cast"].ToString().ToUpper() == "OBC")
                            {
                                txt_belongs_to_scst_ten.Text = "YES";
                            }
                            else
                            {
                                txt_belongs_to_scst_ten.Text = "NO";
                            }
                        }

                        txt_date_of_adm_ten.Text = dt.Rows[0]["dateofadmission"].ToString();
                        txt_adm_class_ten.Text = dt.Rows[0]["Admission_class"].ToString();
                        txt_class_last_studies_ten.Text = dt.Rows[0]["class"].ToString();
                        //txt_school_board_exam_taken_ten.Text = dt.Rows[0]["class"].ToString();
                        txt_failed_once_twice_ten.Text = "NA";
                        txt_qualified_higher_class_ten.Text = "STUDYING IN CLASS " + dt.Rows[0]["class"].ToString();
                        txt_paid_fee_till_month_ten.Text = "";
                        txt_any_fee_concession_ten.Text = "---";
                        txt_ttl_no_of_workind_days_ten.Text = "---";
                        txt_ttl_persent_days.Text = "---";
                        txt_ncc_cadet_by_ten.Text = "NA";
                        txt_games_played.Text = "SCHOOL GAMES";

                        txt_general_conduct_ten.Text = "GOOD";
                        txt_date_of_application_ten.Text = mycode.date();
                        txt_date_of_issue_ten.Text = mycode.date();
                        txt_reason_for_leaving_ten.Text = "";
                        txt_any_other_remarks_ten.Text = "---";

                        find_subject(ViewState["adm_no"].ToString(), ViewState["sessioN"].ToString(), ViewState["class_id"].ToString());
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                        txt_ncc_cadet.Text = "NA";
                        txt_general_conduct.Text = "GOOD";
                        txt_class_in_last_studied.Text = dt.Rows[0]["class"].ToString() + ", " + dt.Rows[0]["session"].ToString();

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


                        if (dt.Rows[0]["cast"].ToString() == "Select")
                        {
                            txt_belong_to_sc_st_obc.Text = "NO";
                        }
                        else
                        {
                            txt_belong_to_sc_st_obc.Text = dt.Rows[0]["cast"].ToString();
                        }


                        string date_of_admission_and_class_id = find_date_of_admission(lbl_adm_no.Text);
                        string[] stringSeparatorss = new string[] { "~" };
                        string[] arrs = date_of_admission_and_class_id.Split(stringSeparatorss, StringSplitOptions.None);
                        string date_of_admission = arrs[0];
                        string class_id = arrs[1];

                        txt_date_of_admission.Text = date_of_admission;

                        try
                        {
                            ddl_class_c.SelectedValue = class_id;
                        }
                        catch (Exception ex)
                        {
                        }

                        find_subject(ViewState["adm_no"].ToString(), ViewState["sessioN"].ToString(), ViewState["class_id"].ToString());
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

        private void find_subject(string adm_no, string session_id, string class_id)
        {
            string subjects = "";
            string query = "select (select top 1 Subject_name from Subject_Master where Subject_id=Subject_Mapping_New.Sub_id) as Subject_name from Subject_Mapping_New where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Admission_no='" + adm_no + "' and Sub_id in (select Subject_id from Subject_Master where Subject_id=Subject_Mapping_New.Sub_id and Is_mandatory=1)";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    subjects += subjects = i + ". " + dr["Subject_name"].ToString() + ", ";
                    i++;
                }
                subjects = subjects.Remove(subjects.Length - 2);
            }


            txt_subject_studied.Text = subjects;
            txt_optional_subject.Text = "-";

            txt_subject_studied2.Text = subjects;
            txt_subject_studied3.Text = subjects;
            txt_subject_studied4.Text = subjects;
            txt_subject_studied_6.Text = subjects;
            txt_subj_studies_ten.Text = subjects;
        }

        protected void btn_create_certificate_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "myMdlCertificateP();", true);
                if (txt_dob.Text == "")
                {
                    Alertme("Please enter date of birth.", "warning");
                    txt_dob.Focus();
                }
                if (txt_date_of_admission.Text == "")
                {
                    Alertme("Please enter date of admission.", "warning");
                    txt_date_of_admission.Focus();
                }
                else if (txt_class_in_last_studied.Text == "")
                {
                    Alertme("Please enter class in which the pupil last studied.", "warning");
                    txt_class_in_last_studied.Focus();
                }
                else if (txt_annual_exam_taken_with_result.Text == "")
                {
                    Alertme("Please enter School / Birth Annual Examination last taken with result.", "warning");
                    txt_annual_exam_taken_with_result.Focus();
                }
                else if (txt_whether_failed_in_same_class.Text == "")
                {
                    Alertme("Please enter Whether failed in same class.", "warning");
                    txt_whether_failed_in_same_class.Focus();
                }
                else if (txt_subject_studied.Text == "")
                {
                    Alertme("Please enter Subject studied, Compulsory.", "warning");
                    txt_subject_studied.Focus();
                }
                else if (txt_whether_qualified_for_promotion.Text == "")
                {
                    Alertme("Please enter Whether qualified for promotion / to which class.", "warning");
                    txt_whether_qualified_for_promotion.Focus();
                }
                else if (txt_ttl_working_days.Text == "")
                {
                    Alertme("Please enter Total no. of working days.", "warning");
                    txt_ttl_working_days.Focus();
                }
                else if (txt_present_days.Text == "")
                {
                    Alertme("Please enter Total no. of working days present.", "warning");
                    txt_present_days.Focus();
                }
                else if (txt_ncc_cadet.Text == "")
                {
                    Alertme("Please enter Whether NCC Cadet / Boy Scout / Girl Guide.", "warning");
                    txt_ncc_cadet.Focus();
                }
                else if (txt_game_played_of_extra.Text == "")
                {
                    Alertme("Please enter Games played or extra curricular activities in which the pupil usually took apart.", "warning");
                    txt_game_played_of_extra.Focus();
                }
                else if (txt_general_conduct.Text == "")
                {
                    Alertme("Please enter General conduct.", "warning");
                    txt_general_conduct.Focus();
                }
                else if (txt_date_of_application.Text == "")
                {
                    Alertme("Please enter Date of application for certificate.", "warning");
                    txt_date_of_application.Focus();
                }
                else if (txt_date_of_issue_certificate.Text == "")
                {
                    Alertme("Please enter Date of issue of certificate.", "warning");
                    txt_date_of_issue_certificate.Focus();
                }
                else if (txt_reason_for_leaving.Text == "")
                {
                    Alertme("Please enter Reason for leaving the school.", "warning");
                    txt_reason_for_leaving.Focus();
                }
                else
                {
                    string certificate_no = My.tc_format("Certificate_sl");
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Transfer_certificate where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Transfer'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Transfer_certificate");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Transfer";
                        dr["Certificate_no"] = certificate_no;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();

                        dr["class_in_last_studied"] = txt_class_in_last_studied.Text;
                        dr["annual_exam_taken_with_result"] = txt_annual_exam_taken_with_result.Text;
                        dr["whether_failed_in_same_class"] = txt_whether_failed_in_same_class.Text;
                        dr["subject_studied"] = txt_subject_studied.Text;
                        dr["optional_subject"] = txt_optional_subject.Text;
                        dr["whether_qualified_for_promotion"] = txt_whether_qualified_for_promotion.Text;
                        dr["ttl_working_days"] = txt_ttl_working_days.Text;
                        dr["present_days"] = txt_present_days.Text;
                        dr["ncc_cadet"] = txt_ncc_cadet.Text;
                        dr["game_played_of_extra"] = txt_game_played_of_extra.Text;
                        dr["general_conduct"] = txt_general_conduct.Text;
                        dr["date_of_application"] = txt_date_of_application.Text;
                        dr["date_of_issue_certificate"] = txt_date_of_issue_certificate.Text;
                        dr["reason_for_leaving"] = txt_reason_for_leaving.Text;
                        dr["any_other_remarks"] = txt_any_other_remarks.Text;

                        if (ddl_class_c.SelectedItem.Text == "Select")
                        {
                            dr["Date_of_admission_and_class"] = txt_date_of_admission.Text;
                        }
                        else
                        {
                            dr["Date_of_admission_and_class"] = txt_date_of_admission.Text + " in " + ddl_class_c.SelectedItem.Text;
                        }

                        dr["Board_roll_no"] = txt_board_roll_no.Text;
                        dr["Cbse_reg_no"] = txt_cbse_reg_no.Text;
                        dr["Belongs_t_sc_st_obc"] = txt_belong_to_sc_st_obc.Text;
                        dr["Student_PEN"] = txt_pen_no.Text;

                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        mycode.executequery("update admission_registor set Status='0',Student_pen_no='" + txt_pen_no.Text + "',religion='" + ddl_religion.Text + "',Student_nationality='" + ddl_nationality.Text + "',dob='" + txt_dob.Text + "',Is_TC_Taken=1 where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");


                        try
                        {
                            string sub = "Transfer Certificate";
                            string messge = "Dear student your transfer certificate has been generated please come to the school and collect the certificate.";
                            if (ViewState["gcmid"].ToString() != "")
                            {
                                Dictionary<String, String> ss = new Dictionary<string, string>();
                                ss["notification_id"] = Guid.NewGuid().ToString();
                                ss["message"] = messge;
                                ss["title"] = sub;
                                ss["messagetype"] = "Message";
                                ss["url"] = "";
                                ss["link_url"] = "";
                                ss["UserId"] = ViewState["adm_no"].ToString();
                                UsesCode.SendNotification(ViewState["gcmid"].ToString(), ss);
                            }
                        }
                        catch
                        {
                        }
                        //string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and Session_id='" + ViewState["sessioN"].ToString() + "' and admissionserialnumber not IN  (SELECT Admission_no FROM Transfer_certificate WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id) order by id desc";
                        //find_details(qry);


                        if (hd_firm_id.Value == "AWD-01")
                        {
                            Response.Redirect("slip/TC/transfer-certificate-offline.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                        }
                        else
                        {
                            Response.Redirect("slip/transfer-certificate.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                        }
                    }
                    else
                    {
                        Alertme("Certificate already created of this student.", "warning");
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
                    string certificate_no = My.tc_format("Certificate_sl");
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Transfer_certificate where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Transfer'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Transfer_certificate");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Transfer";
                        dr["Certificate_no"] = certificate_no;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();

                        dr["class_in_last_studied"] = txt_class_in_last_studied_7.Text;
                        dr["annual_exam_taken_with_result"] = txt_annual_exam_taken_with_result_7.Text;
                        dr["whether_failed_in_same_class"] = txt_whether_failed_in_same_class_7.Text;
                        dr["subject_studied"] = txt_subject_studied_7.Text;
                        dr["optional_subject"] = txt_optional_subject_7.Text;
                        dr["whether_qualified_for_promotion"] = txt_whether_qualified_for_promotion_7.Text;

                        dr["ncc_cadet"] = txt_ncc_cadet_7.Text;
                        dr["game_played_of_extra"] = txt_game_played_of_extra_7.Text;
                        dr["general_conduct"] = txt_general_conduct_7.Text;
                        dr["date_of_application"] = txt_date_of_application_7.Text;
                        dr["date_of_issue_certificate"] = txt_date_of_issue_certificate_7.Text;
                        dr["reason_for_leaving"] = txt_reason_for_leaving_7.Text;
                        dr["any_other_remarks"] = txt_any_other_remarks_7.Text;

                        if (ddl_class_c_7.SelectedItem.Text == "Select")
                        {
                            dr["Date_of_admission_and_class"] = txt_date_of_admission_7.Text;
                        }
                        else
                        {
                            dr["Date_of_admission_and_class"] = txt_date_of_admission_7.Text + " in " + ddl_class_c_7.SelectedItem.Text;
                        }


                        dr["Belongs_t_sc_st_obc"] = txt_belong_to_sc_st_obc_7.Text;
                        dr["Student_PEN"] = txt_pen_no_7.Text;

                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        if (txt_pen_no_7.Text == "")
                        {

                        }
                        else
                        {
                            mycode.executequery("update admission_registor set Status='0',Student_pen_no='" + txt_pen_no_7.Text + "',Is_TC_Taken=1 where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                        }



                        try
                        {
                            string sub = "Transfer Certificate";
                            string messge = "Dear student your transfer certificate has been generated please come to the school and collect the certificate.";
                            if (ViewState["gcmid"].ToString() != "")
                            {
                                Dictionary<String, String> ss = new Dictionary<string, string>();
                                ss["notification_id"] = Guid.NewGuid().ToString();
                                ss["message"] = messge;
                                ss["title"] = sub;
                                ss["messagetype"] = "Message";
                                ss["url"] = "";
                                ss["link_url"] = "";
                                ss["UserId"] = ViewState["adm_no"].ToString();
                                UsesCode.SendNotification(ViewState["gcmid"].ToString(), ss);
                            }
                        }
                        catch
                        {

                        }
                        if (hd_firm_id.Value == "AWD-01")
                        {
                            Response.Redirect("slip/TC/transfer-certificate-offline.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                        }
                        else
                        {
                            Response.Redirect("slip/TC/transfer-certificate007.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                        }
                    }
                    else
                    {
                        Alertme("Certificate already created of this student.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["studentname"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]

        public static List<string> GetRooPathAdmNo(string PathRooT, string Session_id)
        {
            string sessionid = Session_id;
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["admissionserialnumber"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion

        protected void btn_make_tc_2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_date_of_admission2.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter date of admission.", "warning");
                    txt_date_of_admission2.Focus();
                }
                else if (ddl_class_c2.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please select class.", "warning");
                    ddl_class_c2.Focus();
                }
                else if (txt_date_of_birth.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter date of birth.", "warning");
                    txt_date_of_birth.Focus();
                }

                else if (txt_class_in_last_studied2.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter class in which the Studying/passed.", "warning");
                    txt_class_in_last_studied2.Focus();
                }
                else if (txt_result_of_board.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter Result Of Board/School Examination (Passed/Detained).", "warning");
                    txt_result_of_board.Focus();
                }
                else if (txt_subject_studied2.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter subject studied, compulsory.", "warning");
                    txt_subject_studied2.Focus();
                }
                else if (txt_whether_qualified.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter whether qualified for promotion to higher class.", "warning");
                    txt_whether_qualified.Focus();
                }
                else if (txt_fee_dues.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter whether the pupil paid all the fees due.", "warning");
                    txt_fee_dues.Focus();
                }
                else if (txt_any_fee_concession.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter any fee concession/scholarship availed of, if so, the nature of such concession?", "warning");
                    txt_any_fee_concession.Focus();
                }
                else if (txt_attendance_during_the_session.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter attendance during the session.", "warning");
                    txt_attendance_during_the_session.Focus();
                }
                else if (txt_game_played.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter games played or extra curricular activities in which the Student usually took apart.", "warning");
                    txt_game_played.Focus();
                }
                else if (txt_general_conduct2.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter general conduct.", "warning");
                    txt_general_conduct2.Focus();
                }
                else if (txt_date_of_receipt_of_slc.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter date of Receipt of SLC Application.", "warning");
                    txt_date_of_receipt_of_slc.Focus();
                }
                else if (txt_date_of_issue.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter date of issue of certificate.", "warning");
                    txt_date_of_issue.Focus();
                }
                else if (txt_reason_for_leaving_school.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC();", true);
                    Alertme("Please enter Reason for leaving the school.", "warning");
                    txt_reason_for_leaving_school.Focus();
                }
                else
                {
                    string certificate_no = My.tc_format("Certificate_sl");
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Transfer_certificate where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Transfer'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Transfer_certificate");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Transfer";
                        dr["Certificate_no"] = certificate_no;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();

                        dr["class_in_last_studied"] = txt_class_in_last_studied2.Text;
                        dr["annual_exam_taken_with_result"] = txt_result_of_board.Text;
                        dr["subject_studied"] = txt_subject_studied2.Text;
                        dr["whether_qualified_for_promotion"] = txt_whether_qualified.Text;

                        dr["Dues_fee"] = txt_fee_dues.Text;
                        dr["AnyConcession"] = txt_any_fee_concession.Text;
                        dr["Date_of_receipt_of_slc"] = txt_date_of_receipt_of_slc.Text;
                        dr["Date_of_births"] = txt_date_of_birth.Text;

                        dr["ttl_working_days"] = txt_attendance_during_the_session.Text;
                        dr["game_played_of_extra"] = txt_game_played.Text;
                        dr["present_days"] = txt_attendance_during_the_session.Text;
                        dr["date_of_issue_certificate"] = txt_date_of_issue.Text;
                        dr["reason_for_leaving"] = txt_reason_for_leaving_school.Text;
                        dr["any_other_remarks"] = txt_any_other_remarks2.Text;
                        dr["general_conduct"] = txt_general_conduct2.Text;
                        dr["Date_of_admission_and_class"] = txt_date_of_admission2.Text + " in " + ddl_class_c2.SelectedItem.Text;

                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        mycode.executequery("update admission_registor set Status='0',Is_TC_Taken=1 where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");


                        try
                        {
                            string sub = "Transfer Certificate";
                            string messge = "Dear student your transfer certificate has been generated please come to the school and collect the certificate.";
                            if (ViewState["gcmid"].ToString() != "")
                            {
                                Dictionary<String, String> ss = new Dictionary<string, string>();
                                ss["notification_id"] = Guid.NewGuid().ToString();
                                ss["message"] = messge;
                                ss["title"] = sub;
                                ss["messagetype"] = "Message";
                                ss["url"] = "";
                                ss["link_url"] = "";
                                ss["UserId"] = ViewState["adm_no"].ToString();
                                UsesCode.SendNotification(ViewState["gcmid"].ToString(), ss);
                            }
                        }
                        catch
                        {
                        }

                        string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and Session_id='" + ViewState["sessioN"].ToString() + "' and admissionserialnumber not IN  (SELECT Admission_no FROM Transfer_certificate WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id) order by id desc";
                        find_details(qry);
                        Response.Redirect("slip/transfer-certificate2.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                    }
                    else
                    {
                        Alertme("Certificate already created of this student.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void btn_make_tc_3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_date_of_admission3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please enter date of admission.", "warning");
                    txt_date_of_admission3.Focus();
                }
                else if (ddl_class_c3.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC3();", true);
                    Alertme("Please select class.", "warning");
                    ddl_class_c3.Focus();
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
                    string certificate_no = My.tc_format("Certificate_sl");
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Transfer_certificate where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Transfer'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Transfer_certificate");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Transfer";
                        dr["Certificate_no"] = certificate_no;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();

                        dr["class_in_last_studied"] = txt_class_in_last_studied3.Text;
                        dr["annual_exam_taken_with_result"] = txt_result_of_board3.Text;
                        dr["subject_studied"] = txt_subject_studied3.Text;
                        dr["whether_qualified_for_promotion"] = txt_whether_qualified3.Text;

                        dr["Dues_fee"] = txt_fee_dues3.Text;
                        dr["AnyConcession"] = txt_any_fee_concession3.Text;

                        dr["Date_of_births"] = txt_date_of_birth3.Text;

                        dr["ttl_working_days"] = txt_meeting_up_to_date3.Text;

                        dr["present_days"] = txt_attendance_during_the_session3.Text;
                        dr["date_of_issue_certificate"] = txt_date_of_issue3.Text;
                        dr["reason_for_leaving"] = txt_reason_for_leaving_school3.Text;
                        dr["any_other_remarks"] = txt_any_other_remarks3.Text;
                        dr["general_conduct"] = txt_general_conduct3.Text;
                        dr["Date_of_admission_and_class"] = txt_date_of_admission3.Text + " in " + ddl_class_c3.SelectedItem.Text;

                        dr["ncc_cadet"] = txt_ncc3.Text;
                        dr["Belongs_t_sc_st_obc"] = txt_sc_st3.Text;
                        dr["Nationality"] = txt_Nationality3.Text;
                        dr["srn_no"] = txt_srn_no3.Text;
                        dr["reg_no_ix_xii"] = txt_reg_no_ix_xii3.Text;


                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        mycode.executequery("update admission_registor set Status='0',Is_TC_Taken=1 where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");



                        try
                        {
                            string sub = "Transfer Certificate";
                            string messge = "Dear student your transfer certificate has been generated please come to the school and collect the certificate.";
                            if (ViewState["gcmid"].ToString() != "")
                            {
                                Dictionary<String, String> ss = new Dictionary<string, string>();
                                ss["notification_id"] = Guid.NewGuid().ToString();
                                ss["message"] = messge;
                                ss["title"] = sub;
                                ss["messagetype"] = "Message";
                                ss["url"] = "";
                                ss["link_url"] = "";
                                ss["UserId"] = ViewState["adm_no"].ToString();
                                UsesCode.SendNotification(ViewState["gcmid"].ToString(), ss);
                            }
                        }
                        catch
                        {
                        }

                        string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and Session_id='" + ViewState["sessioN"].ToString() + "' and admissionserialnumber not IN  (SELECT Admission_no FROM Transfer_certificate WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id) order by id desc";
                        find_details(qry);
                        Response.Redirect("slip/transfer-certificate3.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                    }
                    else
                    {
                        Alertme("Certificate already created of this student.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void btn_save_fouth_tc_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTC4();", true);
                if (txt_date_of_admission4.Text == "")
                {
                    Alertme("Please enter date of admission.", "warning");
                    txt_date_of_admission4.Focus();
                }
                else if (ddl_class_c4.Text == "")
                {
                    Alertme("Please select class.", "warning");
                    ddl_class_c4.Focus();
                }
                else if (txt_class_in_last_studied4.Text == "")
                {
                    Alertme("Please enter class in which the pupil last studied.", "warning");
                    txt_class_in_last_studied4.Focus();
                }
                else if (txt_annual_exam_taken_with_result4.Text == "")
                {
                    Alertme("Please enter School / Birth Annual Examination last taken with result.", "warning");
                    txt_annual_exam_taken_with_result4.Focus();
                }
                else if (txt_whether_failed_in_same_class4.Text == "")
                {
                    Alertme("Please enter Whether failed in same class.", "warning");
                    txt_whether_failed_in_same_class4.Focus();
                }
                else if (txt_subject_studied4.Text == "")
                {
                    Alertme("Please enter Subject studied, Compulsory.", "warning");
                    txt_subject_studied4.Focus();
                }
                else if (txt_whether_qualified_for_promotion4.Text == "")
                {
                    Alertme("Please enter Whether qualified for promotion / to which class.", "warning");
                    txt_whether_qualified_for_promotion4.Focus();
                }
                else if (txt_ttl_working_days4.Text == "")
                {
                    Alertme("Please enter Total no. of working days.", "warning");
                    txt_ttl_working_days4.Focus();
                }
                else if (txt_present_days4.Text == "")
                {
                    Alertme("Please enter Total no. of working days present.", "warning");
                    txt_present_days4.Focus();
                }
                else if (txt_ncc_cadet4.Text == "")
                {
                    Alertme("Please enter Whether NCC Cadet / Boy Scout / Girl Guide.", "warning");
                    txt_ncc_cadet4.Focus();
                }
                else if (txt_game_played_of_extra4.Text == "")
                {
                    Alertme("Please enter Games played or extra curricular activities in which the pupil usually took apart.", "warning");
                    txt_game_played_of_extra4.Focus();
                }
                else if (txt_general_conduct4.Text == "")
                {
                    Alertme("Please enter General conduct.", "warning");
                    txt_general_conduct4.Focus();
                }
                else if (txt_date_of_application4.Text == "")
                {
                    Alertme("Please enter Date of application for certificate.", "warning");
                    txt_date_of_application4.Focus();
                }
                else if (txt_date_of_issue_certificate4.Text == "")
                {
                    Alertme("Please enter Date of issue of certificate.", "warning");
                    txt_date_of_issue_certificate4.Focus();
                }
                else if (txt_reason_for_leaving4.Text == "")
                {
                    Alertme("Please enter Reason for leaving the school.", "warning");
                    txt_reason_for_leaving4.Focus();
                }
                else
                {
                    string certificate_no = My.tc_format("Certificate_sl");
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Transfer_certificate where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Transfer'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Transfer_certificate");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Transfer";
                        dr["Certificate_no"] = certificate_no;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();


                        dr["Date_of_births"] = txt_date_of_birth4.Text;
                        dr["Nationality"] = txt_Nationality4.Text;



                        dr["class_in_last_studied"] = txt_class_in_last_studied4.Text;
                        dr["annual_exam_taken_with_result"] = txt_annual_exam_taken_with_result4.Text;
                        dr["whether_failed_in_same_class"] = txt_whether_failed_in_same_class4.Text;
                        dr["subject_studied"] = txt_subject_studied4.Text;
                        dr["optional_subject"] = txt_optional_subject4.Text;
                        dr["whether_qualified_for_promotion"] = txt_whether_qualified_for_promotion4.Text;
                        dr["ttl_working_days"] = txt_ttl_working_days4.Text;
                        dr["present_days"] = txt_present_days4.Text;
                        dr["ncc_cadet"] = txt_ncc_cadet4.Text;
                        dr["game_played_of_extra"] = txt_game_played_of_extra4.Text;
                        dr["general_conduct"] = txt_general_conduct4.Text;
                        dr["date_of_application"] = txt_date_of_application4.Text;
                        dr["date_of_issue_certificate"] = txt_date_of_issue_certificate4.Text;
                        dr["reason_for_leaving"] = txt_reason_for_leaving4.Text;
                        dr["any_other_remarks"] = txt_any_other_remarks4.Text;
                        dr["Date_of_admission_and_class"] = txt_date_of_admission4.Text + " in " + ddl_class_c4.SelectedItem.Text;

                        dr["Board_roll_no"] = txt_board_roll_no4.Text;
                        dr["Cbse_reg_no"] = txt_cbse_reg_no4.Text;
                        dr["Belongs_t_sc_st_obc"] = txt_belong_to_sc_st_obc4.Text;
                        dr["Aadhar_no"] = txt_aadhar_no.Text;
                        dr["Student_PEN"] = txt_student_pen.Text;
                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        mycode.executequery("update admission_registor set Status='0',Is_TC_Taken=1 where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");

                        try
                        {
                            string sub = "Transfer Certificate";
                            string messge = "Dear student your transfer certificate has been generated please come to the school and collect the certificate.";
                            if (ViewState["gcmid"].ToString() != "")
                            {
                                Dictionary<String, String> ss = new Dictionary<string, string>();
                                ss["notification_id"] = Guid.NewGuid().ToString();
                                ss["message"] = messge;
                                ss["title"] = sub;
                                ss["messagetype"] = "Message";
                                ss["url"] = "";
                                ss["link_url"] = "";
                                ss["UserId"] = ViewState["adm_no"].ToString();
                                UsesCode.SendNotification(ViewState["gcmid"].ToString(), ss);
                            }
                        }
                        catch
                        {

                        }
                        //string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and Session_id='" + ViewState["sessioN"].ToString() + "' and admissionserialnumber not IN  (SELECT Admission_no FROM Transfer_certificate WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id) order by id desc";
                        //find_details(qry);

                        Response.Redirect("slip/TC/transfer-certificate-004.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                    }
                    else
                    {
                        Alertme("Transfer Certificate already created for this student.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
        protected void btn_create_certificate_six_Click(object sender, EventArgs e)
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
                    string certificate_no = My.tc_format("Certificate_sl");
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Transfer_certificate where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Transfer'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Transfer_certificate");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Transfer";
                        dr["Certificate_no"] = certificate_no;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();

                        dr["class_in_last_studied"] = txt_class_in_last_studied_6.Text;
                        dr["annual_exam_taken_with_result"] = txt_annual_exam_taken_with_result_6.Text;

                        dr["subject_studied"] = txt_subject_studied_6.Text;
                        dr["optional_subject"] = txt_optional_subject_6.Text;
                        dr["whether_qualified_for_promotion"] = txt_whether_qualified_for_promotion_6.Text;
                        dr["ttl_working_days"] = txt_ttl_working_days_6.Text;
                        dr["present_days"] = txt_present_days_6.Text;


                        dr["general_conduct"] = txt_general_conduct_6.Text;
                        dr["date_of_application"] = txt_date_of_application_6.Text;
                        dr["date_of_issue_certificate"] = txt_date_of_issue_certificate_6.Text;
                        dr["reason_for_leaving"] = txt_reason_for_leaving_6.Text;


                        if (ddl_class_c_6.SelectedItem.Text == "Select")
                        {
                            dr["Date_of_admission_and_class"] = txt_date_of_admission_6.Text;
                        }
                        else
                        {
                            dr["Date_of_admission_and_class"] = txt_date_of_admission_6.Text + " in " + ddl_class_c_6.SelectedItem.Text;
                        }


                        dr["Belongs_t_sc_st_obc"] = txt_belong_to_sc_st_obc_6.Text;
                        dr["Student_PEN"] = txt_pen_no_6.Text;

                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        mycode.executequery("update admission_registor set Status='0',Student_pen_no='" + txt_pen_no_6.Text + "',Is_TC_Taken=1 where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");


                        try
                        {
                            string sub = "Transfer Certificate";
                            string messge = "Dear student your transfer certificate has been generated please come to the school and collect the certificate.";
                            if (ViewState["gcmid"].ToString() != "")
                            {
                                Dictionary<String, String> ss = new Dictionary<string, string>();
                                ss["notification_id"] = Guid.NewGuid().ToString();
                                ss["message"] = messge;
                                ss["title"] = sub;
                                ss["messagetype"] = "Message";
                                ss["url"] = "";
                                ss["link_url"] = "";
                                ss["UserId"] = ViewState["adm_no"].ToString();
                                UsesCode.SendNotification(ViewState["gcmid"].ToString(), ss);
                            }
                        }
                        catch
                        {

                        }



                        Response.Redirect("slip/TC/transfer-certificate-006.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);

                    }
                    else
                    {
                        Alertme("Certificate already created of this student.", "warning");
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
                    Alertme("Please enter Date of admission in school with Class", "warning");
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
                    string certificate_no = My.tc_format("Certificate_sl");
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Transfer_certificate where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Transfer'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Transfer_certificate");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Transfer";
                        dr["Certificate_no"] = certificate_no;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();
                        dr["class_in_last_studied"] = txt_class_in_last_studied_8.Text;
                        dr["whether_qualified_for_promotion"] = txt_whether_qualified_for_promotion_8.Text;
                        dr["general_conduct"] = txt_general_conduct_8.Text;
                        dr["date_of_application"] = txt_date_of_application_8.Text;
                        dr["date_of_issue_certificate"] = txt_date_of_issue_certificate_8.Text;
                        dr["reason_for_leaving"] = txt_reason_for_leaving_8.Text;
                        dr["any_other_remarks"] = txt_any_other_remarks_8.Text;
                        if (ddl_class_c_8.SelectedItem.Text == "Select")
                        {
                            dr["Date_of_admission_and_class"] = txt_date_of_admission_8.Text;
                        }
                        else
                        {
                            dr["Date_of_admission_and_class"] = txt_date_of_admission_8.Text + " in " + ddl_class_c_8.SelectedItem.Text;
                        }
                        dr["Belongs_t_sc_st_obc"] = txt_belong_to_sc_st_obc_8.Text;
                        dr["Student_PEN"] = txt_pen_no_8.Text;
                        dr["Data_of_left_school"] = txt_date_of_left_school_8.Text;
                        dr["Monthuptopaid"] = txt_monthuptopaid_8.Text;

                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);

                        mycode.executequery("update admission_registor set Status='0',Student_pen_no='" + txt_pen_no_8.Text + "',Is_TC_Taken=1 where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");




                        try
                        {
                            string sub = "Transfer Certificate";
                            string messge = "Dear student your transfer certificate has been generated please come to the school and collect the certificate.";
                            if (ViewState["gcmid"].ToString() != "")
                            {
                                Dictionary<String, String> ss = new Dictionary<string, string>();
                                ss["notification_id"] = Guid.NewGuid().ToString();
                                ss["message"] = messge;
                                ss["title"] = sub;
                                ss["messagetype"] = "Message";
                                ss["url"] = "";
                                ss["link_url"] = "";
                                ss["UserId"] = ViewState["adm_no"].ToString();
                                UsesCode.SendNotification(ViewState["gcmid"].ToString(), ss);
                            }
                        }
                        catch
                        {

                        }
                        Response.Redirect("slip/TC/transfer-certificate-008.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);

                    }
                    else
                    {
                        Alertme("Certificate already created of this student.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
        #endregion



        public static string ConvertDateToWords(string dateStr)
        {
            DateTime date;
            if (!DateTime.TryParseExact(dateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return "";
            }

            string day = NumberToWords(date.Day);
            string month = date.ToString("MMMM");
            string year = NumberToWords(date.Year);

            return $"{day} of {month}, {year}";
        }

        // Converts numbers to words (up to 9999+)
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                string[] unitsMap = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                                  "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
                                  "Seventeen", "Eighteen", "Nineteen" };
                string[] tensMap = { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty",
                                 "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }
            return words.Trim();
        }

        protected void btn_make_tcTen_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTCTEN();", true);
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
                    string certificate_no = My.tc_format("Certificate_sl");
                    if (hd_firm_id.Value == "NAVY-001")
                    {
                        if (certificate_no.Length == 1)
                        {
                            certificate_no = "000" + certificate_no;
                        }
                        if (certificate_no.Length == 2)
                        {
                            certificate_no = "00" + certificate_no;
                        }
                        if (certificate_no.Length == 3)
                        {
                            certificate_no = "0" + certificate_no;
                        }
                        string[] stringSeparatorss = new string[] { "-" };
                        string[] arrs = ViewState["SessionName"].ToString().Split(stringSeparatorss, StringSplitOptions.None);
                        string yearF = arrs[0];
                        certificate_no = yearF + "/" + certificate_no;
                    }

                    SqlDataAdapter ad = new SqlDataAdapter("select * from Transfer_certificate where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Transfer'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Transfer_certificate");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Transfer";
                        dr["Certificate_no"] = certificate_no;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();

                        dr["class_in_last_studied"] = txt_class_last_studies_ten.Text;
                        dr["whether_qualified_for_promotion"] = txt_qualified_higher_class_ten.Text;
                        dr["general_conduct"] = txt_general_conduct_ten.Text;
                        dr["date_of_application"] = txt_date_of_application_ten.Text;
                        dr["date_of_issue_certificate"] = txt_date_of_issue_ten.Text;
                        dr["reason_for_leaving"] = txt_reason_for_leaving_ten.Text;
                        dr["any_other_remarks"] = txt_any_other_remarks_ten.Text;
                        dr["Date_of_admission_and_class"] = txt_date_of_adm_ten.Text + ", " + txt_adm_class_ten.Text;

                        dr["Belongs_t_sc_st_obc"] = txt_belongs_to_scst_ten.Text;
                        dr["Student_PEN"] = "";
                        dr["Nationality"] = txt_nationality_ten.Text;
                        dr["Date_of_births"] = txt_dob_ten.Text;
                        dr["Date_of_birth_word"] = txt_dob_in_word.Text;
                        dr["annual_exam_taken_with_result"] = txt_school_board_exam_taken_ten.Text;
                        dr["whether_failed_in_same_class"] = txt_failed_once_twice_ten.Text;
                        dr["subject_studied"] = txt_subj_studies_ten.Text;
                        dr["Dues_fee"] = txt_paid_fee_till_month_ten.Text;
                        dr["Concession"] = txt_any_fee_concession_ten.Text;
                        dr["ncc_cadet"] = txt_ncc_cadet_by_ten.Text;
                        dr["game_played_of_extra"] = txt_games_played.Text;

                        dr["ttl_working_days"] = txt_ttl_no_of_workind_days_ten.Text;
                        dr["present_days"] = txt_ttl_persent_days.Text;

                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        mycode.executequery("update admission_registor set Status='0',Is_TC_Taken=1 where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and  Session_id='" + ViewState["sessioN"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                        Response.Redirect("slip/TC/transfer-certificate-010.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                    }
                    else
                    {
                        Alertme("Certificate already created of this student.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void lnk_pay_fee_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_gcmid = (Label)row.FindControl("lbl_gcmid");
                Label lbl_session = (Label)row.FindControl("lbl_session");

                ViewState["gcmid"] = lbl_gcmid.Text;
                ViewState["SessionName"] = lbl_session.Text;
                ViewState["adm_no"] = lbl_adm_no.Text;
                ViewState["sessioNID"] = lbl_session_id.Text;
                ViewState["class_id"] = lbl_class_id.Text;

                txt_fee_amount.Text = "";
                DataTable dtFee = My.dataTable("select * from Special_fee_master where session_id='" + lbl_session_id.Text + "' and class_id='" + lbl_class_id.Text + "' and content_id='TC001'");
                if (dtFee.Rows.Count > 0)
                {
                    txt_fee_amount.Text = dtFee.Rows[0]["amount"].ToString();
                    hd_content_name.Value = dtFee.Rows[0]["content"].ToString();
                    hd_content_id.Value = dtFee.Rows[0]["content_id"].ToString();
                    hd_fee_amount.Value = dtFee.Rows[0]["amount"].ToString();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalPay();", true);
                string query = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + lbl_adm_no.Text + "' and Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' order by id desc";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count > 0)
                {
                    rp_pay_fee.DataSource = dt;
                    rp_pay_fee.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_make_payment_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalPay();", true);
                if (My.toDouble(txt_fee_amount.Text) == 0)
                {
                    txt_fee_amount.Focus();
                    Alertme("Can't payment proceed with 0 amount", "warning");
                    return;
                }
                if (txt_payment_date.Text == "")
                {
                    txt_payment_date.Focus();
                    Alertme("Please enter payment date.", "warning");
                    return;
                }

                //==================
                if (ddl_paymentmode.Text == "Cash")
                {
                    make_payment();
                }
                else
                {
                    if (ddl_bank.Text == "Select")
                    {
                        ddl_bank.Focus();
                        Alertme("Please select bank.", "warning");
                        return;
                    }
                    if (txt_bank_date.Text == "")
                    {
                        txt_bank_date.Focus();
                        Alertme("Please enter bank date.", "warning");
                        return;
                    }
                    if (txt_transaction_no.Text == "")
                    {
                        txt_transaction_no.Focus();
                        Alertme("Please enter transaction no.", "warning");
                        return;
                    }
                    make_payment();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void make_payment()
        {
            string input = txt_payment_date.Text;  // DD/MM/YYYY
            string Fsession = GetFinancialSessionFromString(input);
            string slip_no = "";
            bool flag = false;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();
                slip_no = payments.invoice_monthly("slip_no", con);
                SqlCommand cmd;
                string query = "INSERT INTO Special_fee_collection (Admission_no,Session_id,Class_id,Section,Fee_head,Fee_id,Payable,Paid_amount,Dues,Receipt_no,Date,Idate,Time,Created_date,Created_time,Payment_mode,Payment_bank,Transaction_date,Transaction_no,Remark,Payee_bank_name,user_id,Financial_session) values (@Admission_no,@Session_id,@Class_id,@Section,@Fee_head,@Fee_id,@Payable,@Paid_amount,@Dues,@Receipt_no,@Date,@Idate,@Time,@Created_date,@Created_time,@Payment_mode,@Payment_bank,@Transaction_date,@Transaction_no,@Remark,@Payee_bank_name,@user_id,@Financial_session)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sessioNID"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                cmd.Parameters.AddWithValue("@Section", "");
                cmd.Parameters.AddWithValue("@Admission_no", ViewState["adm_no"].ToString());
                cmd.Parameters.AddWithValue("@Fee_head", hd_content_name.Value);
                cmd.Parameters.AddWithValue("@Fee_id", hd_content_id.Value);
                cmd.Parameters.AddWithValue("@Payable", hd_fee_amount.Value);
                cmd.Parameters.AddWithValue("@Paid_amount", hd_fee_amount.Value);
                cmd.Parameters.AddWithValue("@Dues", "0.00");
                cmd.Parameters.AddWithValue("@Receipt_no", slip_no);
                cmd.Parameters.AddWithValue("@Date", txt_payment_date.Text);
                cmd.Parameters.AddWithValue("@Idate", My.DateConvertToIdate(txt_payment_date.Text));
                cmd.Parameters.AddWithValue("@Time", mycode.time());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                cmd.Parameters.AddWithValue("@Payment_mode", ddl_paymentmode.Text);
                cmd.Parameters.AddWithValue("@Remark", txt_remarks.Text);
                cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Financial_session", Fsession);
                if (ddl_paymentmode.Text == "Cash")
                {
                    cmd.Parameters.AddWithValue("@Payment_bank", "");
                    cmd.Parameters.AddWithValue("@Transaction_date", "");
                    cmd.Parameters.AddWithValue("@Transaction_no", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Payee_bank_name", txt_payee_bank_name.Text);
                    cmd.Parameters.AddWithValue("@Payment_bank", ddl_bank.Text);
                    cmd.Parameters.AddWithValue("@Transaction_date", txt_bank_date.Text);
                    cmd.Parameters.AddWithValue("@Transaction_no", txt_transaction_no.Text);
                }
                if (payments.InsertUpdateData(cmd, con))
                {
                }

                flag = true;
                con.Close();
                scope.Complete();
            }

            if (flag == true)
            {
                Response.Redirect("slip/special-fee-receipt.aspx?admissionno=" + ViewState["adm_no"].ToString() + "&sessionid=" + ViewState["sessioNID"].ToString() + "&classid=" + ViewState["class_id"].ToString() + "&Slip_no=" + slip_no + "&from=create-transfer-certificate.aspx", false);
            }
        }


        public string GetFinancialSessionFromString(string dateString)
        {
            // Parse date from "dd/MM/yyyy" format
            DateTime date;
            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None, out date))
            {
                int year = date.Year;
                if (date.Month < 4)
                {
                    return (year - 1) + "-" + year;
                }
                else
                {
                    return year + "-" + (year + 1);
                }
            }
            else
            {
                return "Invalid date format";
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = e.Item;
                    LinkButton lnk_pay_fee = (LinkButton)e.Item.FindControl("lnk_pay_fee");
                    LinkButton lnkCreate = (LinkButton)e.Item.FindControl("lnkCreate");

                    Label lbl_session_id = (Label)e.Item.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)e.Item.FindControl("lbl_class_id");
                    Label lbl_adm_no = (Label)e.Item.FindControl("lbl_adm_no");
                    if (hd_is_pay_tc_fee.Value == "1")
                    {
                        DataTable dt = My.dataTable("select * from Special_fee_collection where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and Fee_id='TC001'");
                        if (dt.Rows.Count > 0)
                        {
                            lnk_pay_fee.Visible = false;
                            lnkCreate.Visible = true;
                        }
                        else
                        {
                            lnk_pay_fee.Visible = true;
                            lnkCreate.Visible = false;
                        }
                    }
                    else
                    {
                        lnk_pay_fee.Visible = false;
                        lnkCreate.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        [WebMethod]
        public static List<string> GetBankName(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(compLN.comp))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Bank_name from Bank_master where Bank_name LIKE '%'+@SearchMobNo+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["Bank_name"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
    }
}