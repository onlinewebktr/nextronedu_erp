using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class report_card : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        My my = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["regid"] != null)
                    {
                        ViewState["teacher"] = Request.QueryString["regid"].ToString();
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["IsPageFound"] = "NO";
                        ViewState["Branchid"] = my.get_branch_id(ViewState["teacher"].ToString());

                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from session_details");
                        ddl_session.SelectedValue = ViewState["sesssionid"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by Position asc");

                        ViewState["flag"] = "0";
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }





        private void get_rp_info(string query)
        {
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                btn_all_rp.Visible = false;
                lblMessage.Text = "Sorry there are no data list exist";
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_all_rp.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }



        private string getUrls()
        {
            string returN = "0";
            DataTable dt = My.dataTable("select URL from Global");
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = dt.Rows[0][0].ToString();
                return returN;
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
                Label lbl_term_name = ((Label)e.Item.FindControl("lbl_term_name")) as Label;
                HtmlAnchor rpcard_link = (HtmlAnchor)e.Item.FindControl("rpcard_link");
                find_rp_card_page(lbl_class_id.Text, lbl_branch_id.Text);

                string websiteurL = getUrls();
                if (lbl_term_name.Text == "FINAL")
                {
                    rpcard_link.HRef = websiteurL + "Examination_Admin/slip/" + ViewState["FinalRC"].ToString() + "?admNo=" + lbl_admissionserialnumber.Text + "&ssion_id=" + lbl_session_id.Text + "&clss_id=" + lbl_class_id.Text + "&Branch_id=" + lbl_branch_id.Text + "&Term_id=" + lbl_term_id.Text + "&RequestFrom=1";
                }
                else
                {
                    rpcard_link.HRef = websiteurL + "Examination_Admin/slip/" + ViewState["SingleRC"].ToString() + "?admNo=" + lbl_admissionserialnumber.Text + "&ssion_id=" + lbl_session_id.Text + "&clss_id=" + lbl_class_id.Text + "&Branch_id=" + lbl_branch_id.Text + "&Term_id=" + lbl_term_id.Text + "&RequestFrom=1";
                }
            }
        }


        private void find_rp_card_page(string class_id, string branch_id)
        {
            if (ViewState["IsPageFound"].ToString() == "NO")
            {
                string querym = "select Report_card_single,Final_single from Exam_report_card_setting_classwise where  Class_id=" + class_id + " and Branch_id='" + branch_id + "'";
                DataTable dtm = My.dataTable(querym);
                if (dtm.Rows.Count > 0)
                {
                    ViewState["SingleRC"] = dtm.Rows[0]["Report_card_single"].ToString();
                    try
                    {
                        ViewState["FinalRC"] = dtm.Rows[0]["Final_single"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    ViewState["SingleRC"] = "#!";
                    ViewState["FinalRC"] = "#!";
                }
                ViewState["IsPageFound"] = "YES";
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    lblMessage.Text = "Please select session.";
                    ddl_session.Focus();
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    lblMessage.Text = "Please select class.";
                    ddl_class.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    lblMessage.Text = "Please select section.";
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    lblMessage.Text = "Please select term.";
                    ddl_term.Focus();
                }
                else
                {
                    find_report_card();
                }
            }
            catch (Exception ex)
            { }
        }

        private void find_report_card()
        {
            get_rp_info("select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddl_session.SelectedValue + " and em.Class_id=" + ddl_class.SelectedValue + " and ar.Section= '" + ddl_section.Text + "' and em.Term= " + ddl_term.SelectedValue + " order by ar.rollnumber asc");
        }

        protected void btn_all_rp_Click(object sender, EventArgs e)
        {
            try
            {

                lblMessage.Text = "";
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    lblMessage.Text = "Please select session.";
                    ddl_session.Focus();
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    lblMessage.Text = "Please select class.";
                    ddl_class.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    lblMessage.Text = "Please select section.";
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    lblMessage.Text = "Please select term.";
                    ddl_term.Focus();
                }
                else
                {
                    string websiteurL = getUrls();
                    find_rp_card_page();
                    Response.Redirect(websiteurL + "Examination_Admin/slip/" + ViewState["BulkRC"].ToString() + "?Section=" + ddl_section.SelectedItem.Text + "&ssion_id=" + ddl_session.SelectedValue + "&clss_id=" + ddl_class.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Term_id=" + ddl_term.SelectedValue + "&RequestFrom=1", false);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_rp_card_page()
        {
            string querym = "select Report_card_single,Report_card_bulk from Exam_report_card_setting_classwise where  Class_id=" + ddl_class.SelectedValue + " ";
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
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
                mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_class.SelectedValue + "' and UserID='" + ViewState["teacher"].ToString() + "'  order by section");
                mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ddl_session.SelectedValue + "' order by Term_Name asc");
            }
            catch (Exception ex)
            {
            }
        }
    }
}