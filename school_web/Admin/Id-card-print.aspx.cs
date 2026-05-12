using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Id_card_print : System.Web.UI.Page
    {
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
                        ViewState["IsPlusTwoChecked"] = "NO";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Id-card-print.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_session();

                        ddlsession.SelectedValue = My.get_session_id();
                        ddl_session_exc.SelectedValue = ddlsession.SelectedValue;
                        bind_class();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        ddl_class_exc.SelectedValue = ddlclass.SelectedValue;
                        bind_Section();
                        find_id_card_type();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
            }
        }

        private void bind_Section()
        {
            mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "' and Session_id='" + ddlsession.SelectedValue + "' order by Section");
            mycode.bind_ddlall(ddl_section_exc, "Select distinct Section from admission_registor where Class_id='" + ddl_class_exc.SelectedValue + "' and Session_id='" + ddl_session_exc.SelectedValue + "' order by Section");
        }

        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
            mycode.bind_all_ddl_with_id(ddl_session_exc, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
            mycode.bind_all_ddl_with_id_cap_All(ddl_class_exc, "Select Course_Name,course_id,Position from Add_course_table order by Position");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            string qry = "";
            if (ddl_section.SelectedItem.Text == "ALL")
            {
                qry = "select * from admission_registor where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + " and Status='1' order by rollnumber asc";
            }
            else
            {
                qry = "select * from admission_registor where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_section.SelectedItem.Text + "' and Status='1' order by rollnumber asc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("please select class.", "warning");
                    ddlclass.Focus();
                }
                else
                {
                    bind_grd_view();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_print_all_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    if (ddlsession.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select session", "warning");
                        ddlsession.Focus();
                    }
                    else if (ddlclass.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select class", "warning");
                        ddlclass.Focus();
                    }
                    else
                    {
                        string adm_ids = "";
                        int growcount = rd_view.Items.Count;
                        int k = 0;
                        for (int i = 0; i < growcount; i++)
                        {
                            CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("chkRowData");
                            if (chk.Checked == true)
                            {
                                Label lbl_id = (Label)rd_view.Items[i].FindControl("lbl_id");
                                adm_ids = adm_ids += lbl_id.Text + ",";
                            }
                            else
                            {
                                k++;
                            }
                        }

                        if (k == growcount)
                        {
                            if (lbl_is_check.Text == "1")
                            {
                                if (chk_is_ckeck.Checked == true)
                                {
                                    Response.Redirect("id-card/test-id-card03.aspx?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + ddlclass.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                                }
                                else
                                {
                                    Response.Redirect(ViewState["IdCardPageBulk"].ToString() + "?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + ddlclass.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                                }
                            }
                            else
                            {
                                Response.Redirect(ViewState["IdCardPageBulk"].ToString() + "?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + ddlclass.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                            }
                        }
                        else
                        {
                            if (lbl_is_check.Text == "1")
                            {
                                if (chk_is_ckeck.Checked == true)
                                {
                                    string reslink = "id-card/test-id-card03.aspx?Type=CHECK&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + ddlclass.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=" + adm_ids;
                                    Response.Redirect(reslink, false);
                                }
                                else
                                {
                                    string reslink = ViewState["IdCardPageBulk"].ToString() + "?Type=CHECK&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + ddlclass.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=" + adm_ids;
                                    Response.Redirect(reslink, false);
                                }
                            }
                            else
                            {
                                string reslink = ViewState["IdCardPageBulk"].ToString() + "?Type=CHECK&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + ddlclass.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=" + adm_ids;
                                Response.Redirect(reslink, false);
                            }

                        }
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }



        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trR");
                Label lbl_admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_branch_id = ((Label)e.Item.FindControl("lbl_branch_id")) as Label;
                HtmlAnchor idcard_link = (HtmlAnchor)e.Item.FindControl("idcard_link");
                Label lbl_Is_Allow_edit = ((Label)e.Item.FindControl("lbl_Is_Allow_edit")) as Label;
                Label lbl_edit_permission = ((Label)e.Item.FindControl("lbl_edit_permission")) as Label;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    idcard_link.HRef = ViewState["IdCardPage"].ToString() + "?admNo=" + lbl_admissionserialnumber.Text + "&ssion_id=" + lbl_session_id.Text + "&clss_id=" + lbl_class_id.Text + "&Branch_id=" + lbl_branch_id.Text + "&Section=ALL&Type=SINGLE";
                }
                else
                {
                    idcard_link.InnerText = "Not permission for print";
                }

                if (lbl_Is_Allow_edit.Text == "1")
                {
                    tr.Attributes.Add("style", "background-color:#e8ffe8;color:#000000;");
                    lbl_edit_permission.Text = "Yes";
                }

                else
                {
                    tr.Attributes.Add("style", "background-color:#ffe5c3;color:#000000;");
                    lbl_edit_permission.Text = "No";
                }
            }
        }



        private void find_id_card_type()
        {
            DataTable dtF = My.dataTable("select firm_name,firm_id from Firm_Details");
            string querym = "select * from Id_card_template_setting where  Branch_id='" + ViewState["Branchid"].ToString() + "' and Type='Student'";
            DataTable dtm = mycode.FillData(querym);
            if (dtm.Rows.Count > 0)
            {
                lbl_is_check.Text = "0";
                chk_is_ckeck.Visible = false;
                if (dtm.Rows[0]["Id_card_type"].ToString() == "Horizontal")
                {
                    if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMI-001" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMILI-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMSU-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMITAM-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMIGO-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMIBUD-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "KIDS-01")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/id-card-gds-image.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-id-card-5.aspx";
                    }

                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMSITAHAR-1" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "CKG-01")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card-new1.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-vr-id-card-new--1.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "CCF-01")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card-new2.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-vr-id-card-new.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "GAP-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "GPSKTR")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/id-card-gds-image.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-id-card-5.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "KES-1")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/id-card-kes-image.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-id-card-kes-bulk.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "MDA-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "MDA-02")  //ManidweeP
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/manidweep/single-id-card.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/manidweep/bulk-id-card.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "EPIC-1")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/epic/id-card-single.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/epic/id-card-bulk.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "STJ-01")  //SaintJhon
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/stjohn/single-id-card.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/stjohn/bulk-id-card.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "BID-001")  //Bidhan
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/bidhan/student-front-back.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/bidhan/bulk-id-card-student.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "TCSM-001" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "TCSM-002" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "TCSM-003")  //Toppers
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/toppers/id-card.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/toppers/id-card.aspx";
                    }
                    else
                    {
                        ViewState["IdCardPage"] = "id-card/print-hr-id-card.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-hr-id-card.aspx";
                    }
                }
                else
                {
                    if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMI-001" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMILI-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMSU-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMITAM-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMIGO-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMIBUD-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "KIDS-01")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/id-card-gds-image.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-id-card-5.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMSITAHAR-1" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "CKG-01")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card-new1.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-vr-id-card-new--1.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "CCF-01")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card-new2.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-vr-id-card-new.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "GAP-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "GPSKTR")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/id-card-gds-image.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-id-card-5.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "KES-1")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/id-card-kes-image.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-id-card-kes-bulk.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "MDA-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "MDA-02")  //ManidweeP
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/manidweep/single-id-card.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/manidweep/bulk-id-card.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "EPIC-1")
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/epic/id-card-single.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/epic/id-card-bulk.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "STJ-01")  //SaintJhon
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/stjohn/single-id-card.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/stjohn/bulk-id-card.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "BID-001")  //Bidhan
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/bidhan/student-front-back.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/bidhan/bulk-id-card-student.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "TCSM-001" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "TCSM-002" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "TCSM-003")  //Toppers
                    {
                        lbl_is_check.Text = "1";
                        chk_is_ckeck.Visible = true;
                        ViewState["IdCardPage"] = "id-card/toppers/id-card.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/toppers/id-card.aspx";
                    }
                    else
                    {
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card.aspx";
                        ViewState["IdCardPageBulk"] = "id-card/print-vr-id-card.aspx";
                    }
                }
            }
            else
            {
                ViewState["IdCardPage"] = "#!";
            }
        }

        protected void lnk_edit_std_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                hd_admission_no.Value = lbl_admissionserialnumber.Text;
                hd_session_id.Value = lbl_session_id.Text;
                hd_class_id.Value = lbl_class_id.Text;
                btn_reset.Visible = false;
                ftech_std_info();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        private void ftech_std_info()
        {
            ddl_transport.Text = "No";
            txt_transport_type.Text = "";
            compLN comP = new compLN();
            comP.bind_ddl(ddl_Category, "select Category_name from Cast_category");
            DataTable dt = My.dataTable("select * from Id_card_updated_student where Session_id='" + hd_session_id.Value + "' and Admission_no='" + hd_admission_no.Value + "' order by id desc");
            if (dt.Rows.Count > 0)
            {
                btn_reset.Visible = true;
                txt_std_name.Text = dt.Rows[0]["Name"].ToString();
                txt_class.Text = dt.Rows[0]["Class_name"].ToString();
                txt_section.Text = dt.Rows[0]["Section"].ToString();
                txt_roll_no.Text = dt.Rows[0]["Roll_no"].ToString();
                txt_dob.Text = dt.Rows[0]["Date_of_birth"].ToString();
                txt_father_name.Text = dt.Rows[0]["Father_name"].ToString();
                txt_mobile_no.Text = dt.Rows[0]["Mobile_no"].ToString();
                txt_address.Text = dt.Rows[0]["Address"].ToString();
                if (dt.Rows[0]["Transport_taken"].ToString().ToUpper() == "YES")
                {
                    ddl_transport.Text = "Yes";
                }

                ddl_gender.Text = dt.Rows[0]["Gender"].ToString();
                ddl_Category.Text = dt.Rows[0]["Caste"].ToString();
                txt_Whatsapp_no.Text = dt.Rows[0]["Father_Whatsapp"].ToString();
                txt_mothersname.Text = dt.Rows[0]["Mother_full_name"].ToString();
                txt_mother_mobile.Text = dt.Rows[0]["Mobile_No_mother"].ToString();
                txt_mother_Whatsapp.Text = dt.Rows[0]["Mother_Wharsapp"].ToString();
                txt_transport_type.Text = dt.Rows[0]["Transport_type"].ToString();
            }
            else
            {
                DataTable dtStd = My.dataTable("select * from admission_registor where Session_id='" + hd_session_id.Value + "' and admissionserialnumber='" + hd_admission_no.Value + "' order by id desc");
                if (dtStd.Rows.Count > 0)
                {
                    txt_std_name.Text = dtStd.Rows[0]["studentname"].ToString();
                    txt_class.Text = dtStd.Rows[0]["class"].ToString();
                    txt_section.Text = dtStd.Rows[0]["Section"].ToString();
                    txt_roll_no.Text = dtStd.Rows[0]["rollnumber"].ToString();
                    txt_dob.Text = dtStd.Rows[0]["dob"].ToString();
                    txt_father_name.Text = dtStd.Rows[0]["fathername"].ToString();
                    txt_mobile_no.Text = dtStd.Rows[0]["mobilenumber"].ToString();
                    txt_address.Text = dtStd.Rows[0]["careof"].ToString();
                    try
                    {
                        if (dtStd.Rows[0]["transportationtaken"].ToString().ToUpper() == "YES")
                        {
                            ddl_transport.Text = "Yes";
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        ddl_gender.Text = dtStd.Rows[0]["gender"].ToString().ToUpper();
                    }
                    catch
                    {

                    }

                    try
                    {
                        ddl_Category.Text = dtStd.Rows[0]["cast"].ToString();
                    }
                    catch
                    {

                    }

                    txt_mobile_no.Text = dtStd.Rows[0]["father_mob"].ToString();
                    txt_Whatsapp_no.Text = dtStd.Rows[0]["Father_whatsApp_no"].ToString();

                    txt_mothersname.Text = dtStd.Rows[0]["mothername"].ToString();
                    txt_mother_mobile.Text = dtStd.Rows[0]["mother_mob"].ToString();
                    txt_mother_Whatsapp.Text = dtStd.Rows[0]["Mother_whatsApp_no"].ToString();
                }
            }
        }

        protected void btn_update_std_info_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_std_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_std_name.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (txt_class.Text == "")
                {
                    Alertme("Please enter class.", "warning");
                    txt_class.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (txt_section.Text == "")
                {
                    Alertme("Please enter section.", "warning");
                    txt_section.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (txt_roll_no.Text == "")
                {
                    Alertme("Please enter roll no.", "warning");
                    txt_roll_no.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (txt_dob.Text == "")
                {
                    Alertme("Please enter student date of birth.", "warning");
                    txt_dob.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (txt_father_name.Text == "")
                {
                    Alertme("Please enter father's name.", "warning");
                    txt_father_name.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (txt_mobile_no.Text == "")
                {
                    Alertme("Please enter mobile no.", "warning");
                    txt_mobile_no.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (txt_address.Text == "")
                {
                    Alertme("Please enter student address.", "warning");
                    txt_address.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    if (ddl_transport.Text == "Yes")
                    {
                        if (txt_transport_type.Text == "")
                        {
                            Alertme("Please enter transport type.", "warning");
                            txt_transport_type.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            return;
                        }
                    }
                    update_student_info();
                    Alertme("Record has been updated successfully", "success");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_student_info()
        {
            DataTable dt = My.dataTable("select Id from Id_card_updated_student where Session_id='" + hd_session_id.Value + "' and Admission_no='" + hd_admission_no.Value + "' order by id desc");
            if (dt.Rows.Count > 0)
            {
                SqlCommand cmd;
                string query = "update Id_card_updated_student set Name=@Name,Class_name=@Class_name,Section=@Section,Roll_no=@Roll_no,Date_of_birth=@Date_of_birth,Father_name=@Father_name,Mobile_no=@Mobile_no,Address=@Address,Transport_taken=@Transport_taken,Gender=@Gender,Caste=@Caste,Father_Whatsapp=@Father_Whatsapp,Mobile_No_mother=@Mobile_No_mother,Mother_Wharsapp=@Mother_Wharsapp,Mother_full_name=@Mother_full_name,Transport_type=@Transport_type where  Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                cmd.Parameters.AddWithValue("@Name", txt_std_name.Text);
                cmd.Parameters.AddWithValue("@Class_name", txt_class.Text);
                cmd.Parameters.AddWithValue("@Section", txt_section.Text);
                cmd.Parameters.AddWithValue("@Roll_no", txt_roll_no.Text);
                cmd.Parameters.AddWithValue("@Transport_taken", ddl_transport.Text);
                cmd.Parameters.AddWithValue("@Date_of_birth", txt_dob.Text);
                cmd.Parameters.AddWithValue("@Father_name", txt_father_name.Text);
                cmd.Parameters.AddWithValue("@Mobile_no", txt_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Address", txt_address.Text);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Gender", ddl_gender.Text);
                cmd.Parameters.AddWithValue("@Caste", ddl_Category.Text);
                cmd.Parameters.AddWithValue("@Father_Whatsapp", txt_Whatsapp_no.Text);
                cmd.Parameters.AddWithValue("@Mobile_No_mother", txt_mother_mobile.Text);
                cmd.Parameters.AddWithValue("@Mother_Wharsapp", txt_mother_Whatsapp.Text);
                cmd.Parameters.AddWithValue("@Mother_full_name", txt_mothersname.Text);
                if (ddl_transport.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Transport_type", txt_transport_type.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Transport_type", "SELF");
                }

                if (My.InsertUpdateData(cmd))
                {
                    SqlCommand cmd2;
                    string update_admission_registor = "update admission_registor set gender=@gender,cast=@cast,careof_permanent=@careof_permanent,careof=@careof,father_mob=@father_mob,mother_mob=@mother_mob,Father_whatsApp_no=@Father_whatsApp_no,Mother_whatsApp_no=@Mother_whatsApp_no where admissionserialnumber='" + hd_admission_no.Value + "' and    Session_id='" + hd_session_id.Value + "' ";
                    cmd2 = new SqlCommand(update_admission_registor);
                    cmd2.Parameters.AddWithValue("@gender", ddl_gender.Text);
                    cmd2.Parameters.AddWithValue("@cast", ddl_Category.Text);
                    cmd2.Parameters.AddWithValue("@careof_permanent", txt_address.Text);
                    cmd2.Parameters.AddWithValue("@careof", txt_address.Text);
                    cmd2.Parameters.AddWithValue("@father_mob", txt_mobile_no.Text);
                    cmd2.Parameters.AddWithValue("@mother_mob", txt_mother_mobile.Text);
                    cmd2.Parameters.AddWithValue("@Father_whatsApp_no", txt_Whatsapp_no.Text);
                    cmd2.Parameters.AddWithValue("@Mother_whatsApp_no", txt_mother_Whatsapp.Text);
                    if (InsertUpdate.InsertUpdateData(cmd2))
                    {
                    }
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "INSERT INTO Id_card_updated_student (Session_id,Class_id,Admission_no,Name,Class_name,Section,Roll_no,Date_of_birth,Father_name,Mobile_no,Address,Created_by,Created_date,Transport_taken,Gender,Caste,Father_Whatsapp,Mobile_No_mother,Mother_Wharsapp,Mother_full_name,Transport_type) values (@Session_id,@Class_id,@Admission_no,@Name,@Class_name,@Section,@Roll_no,@Date_of_birth,@Father_name,@Mobile_no,@Address,@Created_by,@Created_date,@Transport_taken,@Gender,@Caste,@Father_Whatsapp,@Mobile_No_mother,@Mother_Wharsapp,@Mother_full_name,@Transport_type)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", hd_session_id.Value);
                cmd.Parameters.AddWithValue("@Class_id", hd_class_id.Value);
                cmd.Parameters.AddWithValue("@Admission_no", hd_admission_no.Value);
                cmd.Parameters.AddWithValue("@Name", txt_std_name.Text);
                cmd.Parameters.AddWithValue("@Class_name", txt_class.Text);
                cmd.Parameters.AddWithValue("@Section", txt_section.Text);
                cmd.Parameters.AddWithValue("@Roll_no", txt_roll_no.Text);
                cmd.Parameters.AddWithValue("@Transport_taken", ddl_transport.Text);
                cmd.Parameters.AddWithValue("@Date_of_birth", txt_dob.Text);
                cmd.Parameters.AddWithValue("@Father_name", txt_father_name.Text);
                cmd.Parameters.AddWithValue("@Mobile_no", txt_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Address", txt_address.Text);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Gender", ddl_gender.Text);
                cmd.Parameters.AddWithValue("@Caste", ddl_Category.Text);
                cmd.Parameters.AddWithValue("@Father_Whatsapp", txt_Whatsapp_no.Text);
                cmd.Parameters.AddWithValue("@Mobile_No_mother", txt_mother_mobile.Text);
                cmd.Parameters.AddWithValue("@Mother_Wharsapp", txt_mother_Whatsapp.Text);
                cmd.Parameters.AddWithValue("@Mother_full_name", txt_mothersname.Text);

                if (ddl_transport.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Transport_type", txt_transport_type.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Transport_type", "SELF");
                }

                if (My.InsertUpdateData(cmd))
                {
                    SqlCommand cmd2;
                    string update_admission_registor = "update admission_registor set gender=@gender,cast=@cast,careof_permanent=@careof_permanent,father_mob=@father_mob,mother_mob=@mother_mob,Father_whatsApp_no=@Father_whatsApp_no,Mother_whatsApp_no=@Mother_whatsApp_no where admissionserialnumber='" + hd_admission_no.Value + "' and    Session_id='" + hd_session_id.Value + "' ";

                    cmd2 = new SqlCommand(update_admission_registor);
                    cmd2.Parameters.AddWithValue("@gender", ddl_gender.Text);
                    cmd2.Parameters.AddWithValue("@cast", ddl_Category.Text);
                    cmd2.Parameters.AddWithValue("@careof_permanent", txt_address.Text);
                    cmd2.Parameters.AddWithValue("@father_mob", txt_mobile_no.Text);
                    cmd2.Parameters.AddWithValue("@mother_mob", txt_mother_mobile.Text);
                    cmd2.Parameters.AddWithValue("@Father_whatsApp_no", txt_Whatsapp_no.Text);
                    cmd2.Parameters.AddWithValue("@Mother_whatsApp_no", txt_mother_Whatsapp.Text);
                    if (InsertUpdate.InsertUpdateData(cmd2))
                    {
                    }
                }
            }
        }

        #region grant permission
        protected void btn_inactive_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (RepeaterItem row in rd_view.Items)
            {
                CheckBox chk = rd_view.Items[i].FindControl("chkRowData") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    Label lbl_admissionserialnumber = rd_view.Items[i].FindControl("lbl_admissionserialnumber") as Label;
                    Label lbl_session_id = rd_view.Items[i].FindControl("lbl_session_id") as Label;

                    SqlCommand cmd = new SqlCommand("Update admission_registor set Is_Allow_edit=0  where admissionserialnumber ='" + lbl_admissionserialnumber.Text + "' and Session_id='" + lbl_session_id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                }
                i++;
            }
            Alertme("Permission has been removed", "success");
            bind_grd_view();
        }


        protected void btn_active_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (RepeaterItem row in rd_view.Items)
            {
                CheckBox chk = rd_view.Items[i].FindControl("chkRowData") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    Label lbl_admissionserialnumber = rd_view.Items[i].FindControl("lbl_admissionserialnumber") as Label;
                    Label lbl_session_id = rd_view.Items[i].FindControl("lbl_session_id") as Label;
                    SqlCommand cmd = new SqlCommand("Update admission_registor set Is_Allow_edit=1  where admissionserialnumber ='" + lbl_admissionserialnumber.Text + "' and Session_id='" + lbl_session_id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                }
                i++;
            }

            Alertme("Permission has been granted", "success");
            bind_grd_view();
        }
        #endregion

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            try
            {
                My.exeSql("delete from Id_card_updated_student where Session_id='" + hd_session_id.Value + "' and Admission_no='" + hd_admission_no.Value + "'");
                Alertme("Sussess", "success"); btn_reset.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                ftech_std_info();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                    return;
                }
                DataTable dt = mycode.FillData("select (select top 1 Session from session_details where session_id=t1.session_id) as Session,Admission_no,Name,Class_name,Section,Roll_no,Date_of_birth,Gender,Caste,Father_name,Mother_full_name,Mobile_no,Mobile_No_mother,Transport_taken,Transport_type,Address from ID_CARD_UPDATED_STUDENT t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' order by t2.Position,Section,Roll_no asc");
                if (dt.Rows.Count > 0)
                {
                    DateTime dTimet = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    string date = dTimet.ToString("dd_MM_yyyy");
                    string time = dTimet.ToString("hh_mm_ss");
                    String filerename = "Student-list-" + date + time;
                    string attachment = "attachment; filename=" + filerename + ".csv";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "text/csv";
                    var csvContent = My.DataTableToCsv(dt);
                    Response.Write(csvContent);
                    Response.End();
                }
                else
                {
                    Alertme("Data not found.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_Section();
        }

        protected void btn_download_excel_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalExcel();", true);
                string qry = "";
                if (ddl_session_exc.SelectedItem.Text == "Select")
                {
                    ddl_session_exc.Focus();
                    Alertme("Please select session.", "warning");
                }
                else
                {
                    if (ddl_class_exc.SelectedItem.Text == "ALL")
                    {
                        qry = "select * from (select (select top 1 Session from session_details where session_id=t1.session_id) as Session,Admission_no,Name,Class_name,Section,Roll_no,Date_of_birth,Gender,Caste,Father_name,Mother_full_name,Mobile_no,Mobile_No_mother,Transport_taken,Transport_type,Address,'Yes' as Is_updated,t2.Position as Class_sequence from ID_CARD_UPDATED_STUDENT t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddl_session_exc.SelectedValue + "' and Admission_no in (select admissionserialnumber from admission_registor where Session_id='" + ddl_session_exc.SelectedValue + "' and Status='1' and Transfer_Status in('NT','New')) union all select t1.session as Session,t1.admissionserialnumber as Admission_no,t1.studentname as Name,t1.class as Class_name,t1.Section,t1.rollnumber as Roll_no,t1.dob as Date_of_birth,t1.gender as Gender,t1.cast as Caste,t1.fathername as Father_name,t1.mothername as Mother_full_name,t1.mobilenumber as Mobile_no,t1.mother_mob as Mobile_No_mother,isnull((t1.transportationtaken),'No') as Transport_taken,'NA' as Transport_type,t1.careof as Address,'No' as Is_updated,t2.Position as Class_sequence  from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddl_session_exc.SelectedValue + "' and Status=1 and admissionserialnumber not in (select Admission_no from ID_CARD_UPDATED_STUDENT where Session_id='" + ddl_session_exc.SelectedValue + "')) t order by Class_sequence,Section,Roll_no asc";
                    }
                    else
                    {
                        if (ddl_section_exc.SelectedItem.Text == "ALL")
                        {
                            qry = "select * from (select (select top 1 Session from session_details where session_id=t1.session_id) as Session,Admission_no,Name,Class_name,Section,Roll_no,Date_of_birth,Gender,Caste,Father_name,Mother_full_name,Mobile_no,Mobile_No_mother,Transport_taken,Transport_type,Address,'Yes' as Is_updated,t2.Position as Class_sequence from ID_CARD_UPDATED_STUDENT t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddl_session_exc.SelectedValue + "' and t1.Class_id='" + ddl_class_exc.SelectedValue + "' and Admission_no in (select admissionserialnumber from admission_registor where Session_id='" + ddl_session_exc.SelectedValue + "' and Status='1' and Transfer_Status in('NT','New')) union all select t1.session as Session,t1.admissionserialnumber as Admission_no,t1.studentname as Name,t1.class as Class_name,t1.Section,t1.rollnumber as Roll_no,t1.dob as Date_of_birth,t1.gender as Gender,t1.cast as Caste,t1.fathername as Father_name,t1.mothername as Mother_full_name,t1.mobilenumber as Mobile_no,t1.mother_mob as Mobile_No_mother,isnull((t1.transportationtaken),'No') as Transport_taken,'NA' as Transport_type,t1.careof as Address,'No' as Is_updated,t2.Position as Class_sequence  from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddl_session_exc.SelectedValue + "' and t1.Class_id='" + ddl_class_exc.SelectedValue + "' and Status=1 and Transfer_Status in('NT','New')  and admissionserialnumber not in (select Admission_no from ID_CARD_UPDATED_STUDENT where Session_id='" + ddl_session_exc.SelectedValue + "')) t order by Class_sequence,Section,Roll_no asc";
                        }
                        else
                        {
                            qry = "select * from (select (select top 1 Session from session_details where session_id=t1.session_id) as Session,Admission_no,Name,Class_name,Section,Roll_no,Date_of_birth,Gender,Caste,Father_name,Mother_full_name,Mobile_no,Mobile_No_mother,Transport_taken,Transport_type,Address,'Yes' as Is_updated,t2.Position as Class_sequence from ID_CARD_UPDATED_STUDENT t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddl_session_exc.SelectedValue + "' and t1.Class_id='" + ddl_class_exc.SelectedValue + "' and Section='" + ddl_section_exc.SelectedItem.Text + "' and Admission_no in (select admissionserialnumber from admission_registor where Session_id='" + ddl_session_exc.SelectedValue + "' and Status='1' and Transfer_Status in('NT','New')) union all select t1.session as Session,t1.admissionserialnumber as Admission_no,t1.studentname as Name,t1.class as Class_name,t1.Section,t1.rollnumber as Roll_no,t1.dob as Date_of_birth,t1.gender as Gender,t1.cast as Caste,t1.fathername as Father_name,t1.mothername as Mother_full_name,t1.mobilenumber as Mobile_no,t1.mother_mob as Mobile_No_mother,isnull((t1.transportationtaken),'No') as Transport_taken,'NA' as Transport_type,t1.careof as Address,'No' as Is_updated,t2.Position as Class_sequence from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddl_session_exc.SelectedValue + "' and t1.Class_id='" + ddl_class_exc.SelectedValue + "' and Section='" + ddl_section_exc.SelectedItem.Text + "' and Status=1 and Transfer_Status in('NT','New')  and admissionserialnumber not in (select Admission_no from ID_CARD_UPDATED_STUDENT where Session_id='" + ddl_session_exc.SelectedValue + "')) t order by Class_sequence,Section,Roll_no asc";
                        }
                    }

                    DataTable dt = My.dataTable(qry);
                    if (dt.Rows.Count > 0)
                    {
                        DateTime dTimet = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        string date = dTimet.ToString("dd_MM_yyyy");
                        string time = dTimet.ToString("hh_mm_ss");
                        String filerename = "Student-list-" + date + time;
                        string attachment = "attachment; filename=" + filerename + ".csv";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "text/csv";
                        var csvContent = My.DataTableToCsv(dt);
                        Response.Write(csvContent);
                        Response.End();
                        Alertme("Student details has been download.", "warning");
                    }
                    else
                    {
                        Alertme("Student details not found.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_class_exc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalExcel();", true);
                mycode.bind_ddlall(ddl_section_exc, "Select distinct Section from admission_registor where Class_id='" + ddl_class_exc.SelectedValue + "' and Session_id='" + ddl_session_exc.SelectedValue + "' order by Section");
            }
            catch (Exception ex)
            {
            }
        }
    }
}