using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class rank_details : System.Web.UI.Page
    {
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
                        ViewState["rankClculteType"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_session();
                        bind_class();
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_term.Items.Insert(0, new ListItem("Select", "0"));
                        find_firm_details();
                        //bind_grd_view();
                        find_rank_calculation_type();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
            }
        }
        private void find_rank_calculation_type()
        {
            try
            {
                DataTable dt = My.dataTable("select Is_rank_calculation_classwise from Exam_report_card_setting where Session_id='" + ddlsession.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "True")
                    {
                        ViewState["rankClculteType"] = "1";
                        secctionDV.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
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
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
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


        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    bind_terms();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_terms()
        {
            mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " order by Sequence_No asc");
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("please select class.", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (ViewState["rankClculteType"].ToString() == "1") { }
                else
                {
                    if (ddl_section.SelectedItem.Text == "Select")
                    {
                        Alertme("please select section.", "warning");
                        ddl_section.Focus();
                        return;
                    }
                }

                if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("please select term.", "warning");
                    ddl_term.Focus();
                    return;
                }
                fetch_rank();

            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_rank()
        {
            string qry = "select t2.class,t2.rollnumber,t2.session,t2.studentname,t2.fathername,t2.mothername,t2.dob,t1.*,(select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=t1.Term_id) as Term_name from Exam_rank_master t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Class_id=" + ddlclass.SelectedValue + " and t1.Section='" + ddl_section.SelectedItem.Text + "' and t1.Term_id=" + ddl_term.SelectedValue + " and t2.Status='1' order by Rank asc";
            if (ViewState["rankClculteType"].ToString() == "1")
            {
                qry = "select t2.class,t2.rollnumber,t2.session,t2.studentname,t2.fathername,t2.mothername,t2.dob,t1.*,(select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=t1.Term_id) as Term_name from Exam_rank_master t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Class_id=" + ddlclass.SelectedValue + " and t1.Term_id=" + ddl_term.SelectedValue + " and t2.Status='1' order by Rank asc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                grdswpr.Visible = false;
                printDV.Visible = false;
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                grdswpr.Visible = true;
                printDV.Visible = true;
            }
        }


        #region FinalRankCalculatE
        protected void btn_calculate_final_rank_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("please select class.", "warning");
                    ddlclass.Focus();
                    return;
                }
                if (ViewState["rankClculteType"].ToString() == "1")
                { }
                else
                {
                    if (ddl_section.SelectedItem.Text == "Select")
                    {
                        Alertme("please select section.", "warning");
                        ddl_section.Focus();
                        return;
                    }
                }

                string qry = "select t2.class,t2.rollnumber,t2.session,t2.studentname,t2.fathername,t2.mothername,t2.dob,t1.Percentage as Mark_percentage,t1.*,'Final Assessment' as Term_name from Exam_rank_master_final_year t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Class_id=" + ddlclass.SelectedValue + " and t1.Section='" + ddl_section.SelectedItem.Text + "' and t2.Status='1' order by Rank asc";
                if (ViewState["rankClculteType"].ToString() == "1")
                {
                    qry = "select t2.class,t2.rollnumber,t2.session,t2.studentname,t2.fathername,t2.mothername,t2.dob,t1.Percentage as Mark_percentage,t1.*,'Final Assessment' as Term_name from Exam_rank_master_final_year t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Class_id=" + ddlclass.SelectedValue + " and t2.Status='1' order by Rank asc";
                }
                DataTable dt = mycode.FillData(qry);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                    grdswpr.Visible = false;
                    printDV.Visible = false;
                }
                else
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    grdswpr.Visible = true;
                    printDV.Visible = true;
                }
            }
            catch (Exception ex)
            {
            }
        }


        #endregion


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=student-rank-list.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    pnl_grid.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
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

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_rank_calculation_type();
            }
            catch (Exception ex)
            {
            }
        }
    }
}