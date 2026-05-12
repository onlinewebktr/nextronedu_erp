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
    public partial class print_character_certificate : System.Web.UI.Page
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
                        ViewState["is_chrcter3"] = "0";
                        try
                        {
                            DataTable dtc = My.dataTable("select Is_character_3 from Firm_Details");
                            if (dtc.Rows.Count > 0)
                            {
                                if (dtc.Rows[0][0].ToString() == "3" || dtc.Rows[0][0].ToString() == "True")
                                {
                                    ViewState["chrcterPage"] = "character-certificate3.aspx"; 
                                    ViewState["is_chrcter3"] = "1";
                                }
                                if (dtc.Rows[0][0].ToString() == "4")
                                {
                                    ViewState["chrcterPage"] = "character-certificate4.aspx";
                                    ViewState["is_chrcter3"] = "1";
                                }
                                if (dtc.Rows[0][0].ToString() == "5")
                                {
                                    ViewState["chrcterPage"] = "character-certificateBD.aspx";
                                    ViewState["is_chrcter3"] = "1";
                                } 
                            }
                        }
                        catch (Exception ex) { }
                        find_firm_details();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "certificate-master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);

                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];

                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlsession.SelectedValue = My.get_session_id();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        My.bind_ddl_select(ddl_old_class, "Select Course_Name from Add_course_table order by Position asc");
                        bind_created_certificate();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Character_certificate");
            } 
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
                ViewState["firm_id"] = dt.Rows[0]["firm_id"].ToString();
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
            string query = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                query = "select *,(select top 1 Certificate_no from Certificate_Master where Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id and Admission_no=admission_registor.admissionserialnumber and Certificate_type='Character') as Certificate_no from admission_registor where admissionserialnumber IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Character') and Session_id=" + ddlsession.SelectedValue + "  order by Certificate_no asc";
            }
            else
            {
                query = "select *,(select top 1 Certificate_no from Certificate_Master where Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id and Admission_no=admission_registor.admissionserialnumber and Certificate_type='Character') as Certificate_no from admission_registor where admissionserialnumber IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Character') and Session_id=" + ddlsession.SelectedValue + " and Class_id='" + ddlclass.SelectedValue + "' order by Certificate_no asc";
            }
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                print1.Visible = false;
                lbl_class22.Text = "";
                btn_excels.Visible = false;
                rd_view.DataSource = null;
                rd_view.DataBind();
                Alertme("Data Not Found...", "warning");
            }
            else
            {
                lbl_class22.Text = " Session :" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
                btn_excels.Visible = true;
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

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl character = e.Item.FindControl("character") as HtmlGenericControl;
                HtmlGenericControl dob = e.Item.FindControl("dob") as HtmlGenericControl;
                HtmlGenericControl bonafied = e.Item.FindControl("bonafied") as HtmlGenericControl;
                HtmlGenericControl bonafied_fees = e.Item.FindControl("bonafied_fees") as HtmlGenericControl;
                HtmlGenericControl leaving = e.Item.FindControl("leaving") as HtmlGenericControl;
                Label lbl_adm_no = e.Item.FindControl("lbl_adm_no") as Label;
                Label lbl_session_id = e.Item.FindControl("lbl_session_id") as Label;
                Label lbl_class_id = e.Item.FindControl("lbl_class_id") as Label;
                HtmlAnchor ccc_link = (HtmlAnchor)e.Item.FindControl("ccc_link");
                LinkButton lnk_edit = e.Item.FindControl("lnk_edit") as LinkButton;
                Label lbl_certificate_no = e.Item.FindControl("lbl_certificate_no") as Label;
                if (ViewState["firm_id"].ToString() == "NAVY-001")
                {
                    lnk_edit.Visible = true;
                    ccc_link.HRef = "slip/bonafide/character.aspx?adm_no=" + lbl_adm_no.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text + "&crtificateno=" + lbl_certificate_no.Text;
                }
                else
                {
                    if (ViewState["is_chrcter3"].ToString() == "1")
                    {
                        lnk_edit.Visible = true;
                        ccc_link.HRef = "slip/" + ViewState["chrcterPage"].ToString() + "?adm_no=" + lbl_adm_no.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text;
                    }

                    else if (ViewState["is_chrcter3"].ToString() == "5")
                    {
                        lnk_edit.Visible = true;
                        ccc_link.HRef = "slip/" + ViewState["chrcterPage"].ToString() + "?adm_no=" + lbl_adm_no.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text;
                    }
                  
                    else
                    {
                        lnk_edit.Visible = false;
                        ccc_link.HRef = "slip/character-certificate.aspx?adm_no=" + lbl_adm_no.Text + "&clssid=" + lbl_class_id.Text + "&sessnid=" + lbl_session_id.Text;
                    }
                }
            }
        }

        private void check_certificate_created(HtmlGenericControl character, HtmlGenericControl dob, HtmlGenericControl bonafied, HtmlGenericControl leaving, HtmlGenericControl bonafied_fees, string admission_no, string session_id, string class_id)
        {
            string query = "select * from Certificate_Master where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Certificate_type"].ToString() == "Character")
                    {
                        character.Visible = true;
                    }
                    else if (dr["Certificate_type"].ToString() == "DOB")
                    {
                        dob.Visible = true;
                    }
                    else if (dr["Certificate_type"].ToString() == "Income")
                    {
                        bonafied.Visible = true;
                        bonafied_fees.Visible = true;
                    }
                    else if (dr["Certificate_type"].ToString() == "Leaving")
                    {
                        leaving.Visible = true;
                    }
                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Data Not Found...", "warning");
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
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_certificate_no = (Label)row.FindControl("lbl_certificate_no");
                mycode.executequery("delete from Certificate_Master where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and Certificate_type='Character'");
                string desc = "Character certificate has been delete for certificate no. " + lbl_certificate_no.Text + ".";
                log_hostory.delete_log(lbl_session_id.Text, lbl_class_id.Text, lbl_adm_no.Text, "Character Certificate", desc, "print-birth-certificate.aspx", ViewState["Userid"].ToString());
                Alertme("Record has been deleted successfully.", "success");
                bind_created_certificate();
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
            }
        }

        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_Certificate_no = (Label)row.FindControl("lbl_certificate_no");
                ViewState["adm_no_e"] = lbl_adm_no.Text;
                ViewState["sessioN_e"] = lbl_session_id.Text;
                ViewState["class_id_e"] = lbl_class_id.Text;
                ViewState["certificate_no_e"] = lbl_Certificate_no.Text;
                string query = "select * from admission_registor where admissionserialnumber='" + lbl_adm_no.Text + "' and Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' order by id desc";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCertificate();", true);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                    DataTable dtc = My.dataTable("select * from Certificate_Master where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and Certificate_no='" + lbl_Certificate_no.Text + "'");
                    if (dtc.Rows.Count > 0)
                    {
                        txt_uid_no.Text = dt.Rows[0]["UID_no"].ToString();
                        try
                        {
                            ddl_old_class.Text = dtc.Rows[0]["Old_class_name"].ToString();
                        }
                        catch (Exception ex) { }

                        txt_date_of_admission.Text = dtc.Rows[0]["Date_of_admission"].ToString();
                        txt_passout_date.Text = dtc.Rows[0]["Passout_date"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        protected void btn_update_certificate_Click(object sender, EventArgs e)
        {
            try
            {
                My.exeSql("update Certificate_Master set Date_of_admission='" + txt_date_of_admission.Text + "',Passout_date='" + txt_passout_date.Text + "',Old_class_name='" + ddl_old_class.Text + "'  where Session_id=" + ViewState["sessioN_e"].ToString() + " and Class_id='" + ViewState["class_id_e"].ToString() + "' and Admission_no='" + ViewState["adm_no_e"].ToString() + "' and Certificate_type='Character'");
                if (txt_uid_no.Text == "") { }
                else
                {
                    My.exeSql("update admission_registor set UID_no='" + txt_uid_no.Text + "' where admissionserialnumber='" + ViewState["adm_no_e"].ToString() + "' and Session_id='" + ViewState["sessioN_e"].ToString() + "' and Class_id='" + ViewState["class_id_e"].ToString() + "'");
                }

                Alertme("Certificate has been updated successfully.", "success");
                string desc = "Character certificate updated by " + ViewState["Userid"].ToString();
                log_hostory.edit_log(ViewState["sessioN_e"].ToString(), "0", ViewState["adm_no_e"].ToString(), "Charactercertificate", desc, "print-character-certificate.aspx", ViewState["Userid"].ToString());
            }
            catch (Exception ex)
            {
            }
        }
    }
}