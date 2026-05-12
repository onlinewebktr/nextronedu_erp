using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class settled_cheque_list : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        find_firm_details();
                        ViewState["flag"] = "0";
                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        ddl_section.Items.Insert(0, new ListItem("ALL", "0"));
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        if (ViewState["Is_Download"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = true;
                        }

                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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




        My mycode = new My();
        private void bind_grd_view()
        {
            string qry = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                qry = "select * from Fee_payment_by_cheque_status t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddlsession.SelectedValue + "' and (t1.Status='Settled' or t1.Status='Bounce') order by t1.id desc";
            }
            else if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text == "ALL")
            {
                qry = "select * from Fee_payment_by_cheque_status t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddlsession.SelectedValue + "' and t2.Class_id='" + ddlclass.SelectedValue + "' and (t1.Status='Settled' or t1.Status='Bounce') order by t1.id desc";
            }
            else
            {
                qry = "select * from Fee_payment_by_cheque_status t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddlsession.SelectedValue + "' and t2.Class_id='" + ddlclass.SelectedValue + "' and t2.Section='" + ddl_section.Text + "' and (t1.Status='Settled' or t1.Status='Bounce') order by t1.id desc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                lbl_class22.Text = "";
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.Text + " Section : " + ddl_section.Text;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'");
            }
            catch (Exception ex)
            {
            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    bind_grd_view();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
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
                    Response.AddHeader("content-disposition", "attachment;filename=Export" + ddlclass.SelectedItem.Text + ".xls");
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
                    Alertme("SORRY! You have not permission for this work.", "warning");
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

        protected void lnkSettleCheque_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                find_cheque_details(lbl_Id.Text);
                ViewState["RowIDS"] = lbl_Id.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {
            }
        }

        private void find_cheque_details(string row_id)
        {
            DataTable dt = My.dataTable("select * from Fee_payment_by_cheque_status t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber where  t1.id='" + row_id + "'");
            if (dt.Rows.Count > 0)
            {
                lbl_adm_no_p.Text = dt.Rows[0]["Admission_no"].ToString();
                lbl_std_name_p.Text = dt.Rows[0]["studentname"].ToString();
                lbl_slip_no_p.Text = dt.Rows[0]["Monthly_slip_no"].ToString();
                lbl_cheque_no_p.Text = dt.Rows[0]["Cheque_no"].ToString();
                lbl_bank_name_p.Text = dt.Rows[0]["Bank_name"].ToString();
                lbl_cheque_date_p.Text = dt.Rows[0]["Cheque_date"].ToString();
                lbl_cheque_amt_p.Text = dt.Rows[0]["Cheque_amount"].ToString();
                lbl_cheque_status_p.Text = dt.Rows[0]["Status"].ToString();
                lbl_chk_status.Text = dt.Rows[0]["Status"].ToString();
                lbl_cheque_update_date_p.Text = dt.Rows[0]["Settled_date"].ToString();
                lbl_fine_amt_p.Text = dt.Rows[0]["Fine_amount"].ToString();
                lbl_remark_p.Text = dt.Rows[0]["Remark"].ToString();

                fineDV.Visible = false;
                if (dt.Rows[0]["Is_fine_apply"].ToString() == "1")
                {
                    fineDV.Visible = true;
                    lnk_remove_fine.Visible = false;
                    string is_payment_done = check_is_payment_done(lbl_slip_no_p.Text, lbl_adm_no_p.Text);
                    if (is_payment_done == "1")
                    {
                        lnk_remove_fine.Visible = true;
                    }
                }
            }
        }

        protected void lnk_remove_fine_Click(object sender, EventArgs e)
        {
            try
            { 
                string is_payment_done = check_is_payment_done(lbl_slip_no_p.Text, lbl_adm_no_p.Text);
                if (is_payment_done == "1")
                {
                    My.exeSql("delete from Typewise_fee_collection where transection='" + lbl_slip_no_p.Text + "' and admission_no='" + lbl_adm_no_p.Text + "' and content_id='cbf01'; update Fee_payment_by_cheque_status set Is_fine_apply='0', Fine_amount='0' where Id='" + ViewState["RowIDS"].ToString() + "'");
                    find_cheque_details(ViewState["RowIDS"].ToString());
                    Alertme("Fine amount has been removed successfully.", "success");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    Alertme("Fine amount cannot be removed fee has already been taken.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string check_is_payment_done(string slip_no, string admission_no)
        {
            string ReturN = "1";
            DataTable dt = mycode.FillData("select * from Typewise_fee_collection where transection='" + slip_no + "' and admission_no='" + admission_no + "' and status='Paid'");
            if (dt.Rows.Count > 0)
            {
                ReturN = "0";
            }
            return ReturN;
        }
    }
}