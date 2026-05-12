using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class student_result : System.Web.UI.Page
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

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_session();

                        ddlsession.SelectedValue = My.get_session_id();
                        bind_class();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_section.Text = My.get_top_one_section();

                        mycode.bind_all_ddl_with_id_cap_All(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " order by Sequence_No asc");
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            string qry = "";
            if (ddlclass.SelectedItem.Text == "Select")  // By Session
            {
                Alertme("Please select class.", "warning");
                ddlclass.Focus();
                return;
            }
            else
            {
                if (ddl_section.SelectedItem.Text == "ALL")
                {
                    if (ddl_term.SelectedItem.Text == "ALL")
                    {
                        qry = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and ar.Status='1' and em.Term in(select Exam_Term_Id from Exam_Term_Details where Exam_Term_Id=em.Term) order by ar.rollnumber asc";
                    }
                    else
                    {
                        qry = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and ar.Status='1' and  em.Term=" + ddl_term.SelectedValue + " order by ar.rollnumber asc";
                    }
                }
                else
                {
                    if (ddl_term.SelectedItem.Text == "ALL")
                    {
                        qry = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and ar.Status='1' and ar.Section='" + ddl_section.SelectedItem.Text + "' and em.Term in(select Exam_Term_Id from Exam_Term_Details where Exam_Term_Id=em.Term) order by ar.rollnumber asc";
                    }
                    else
                    {
                        qry = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and ar.Status='1' and ar.Section='" + ddl_section.SelectedItem.Text + "' and  em.Term=" + ddl_term.SelectedValue + " order by ar.rollnumber asc";
                    }
                }
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
                    ViewState["IsPlusTwoChecked"] = "NO";
                    bind_terms();
                    mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and  Class_id='" + ddlclass.SelectedValue + "' order by Section");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_terms()
        {
            mycode.bind_all_ddl_with_id_cap_All(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " order by Sequence_No asc");
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
                else
                {
                    ViewState["IsPlusTwoChecked"] = "NO";
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
                else if (ddl_section.SelectedItem.Text == "ALL")
                {
                    Alertme("Please select section", "warning");
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "ALL")
                {
                    Alertme("Please select term", "warning");
                    ddl_term.Focus();
                }
                else
                {
                    find_rp_card_page();
                    Response.Redirect("slip/" + ViewState["BulkRC"].ToString() + "?admNo=BULK&Section=" + ddl_section.SelectedItem.Text + "&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + ddlclass.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Term_id=" + ddl_term.SelectedValue + "", false);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_rp_card_page()
        {
            if (ViewState["IsPlusTwoChecked"].ToString() == "NO")
            {
                string querym = "select Report_card_single,Report_card_bulk from Exam_report_card_setting_classwise where  Class_id=" + ddlclass.SelectedValue + " and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                DataTable dtm = mycode.FillData(querym);
                if (dtm.Rows.Count > 0)
                {
                    ViewState["SingleRC"] = dtm.Rows[0]["Report_card_single"].ToString();
                    ViewState["BulkRC"] = dtm.Rows[0]["Report_card_bulk"].ToString();
                }
                else
                {
                    ViewState["SingleRC"] = "#!";
                    ViewState["BulkRC"] = "#!";
                }
                ViewState["IsPlusTwoChecked"] = "YES";
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_branch_id = ((Label)e.Item.FindControl("lbl_branch_id")) as Label;
                Label lbl_term_id = ((Label)e.Item.FindControl("lbl_term_id")) as Label;
                HtmlAnchor rpcard_link = (HtmlAnchor)e.Item.FindControl("rpcard_link");
                find_rp_card_page();


                rpcard_link.HRef = "slip/" + ViewState["SingleRC"].ToString() + "?admNo=" + lbl_admissionserialnumber.Text + "&ssion_id=" + lbl_session_id.Text + "&clss_id=" + lbl_class_id.Text + "&Branch_id=" + lbl_branch_id.Text + "&Term_id=" + lbl_term_id.Text + "&RequestFrom=0";
            }
        }
    }
}