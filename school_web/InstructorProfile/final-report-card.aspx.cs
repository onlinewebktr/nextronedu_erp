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

namespace school_web.InstructorProfile
{
    public partial class final_report_card : System.Web.UI.Page
    {
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teacher"] == null)
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
                    ViewState["IsPlusTwoChecked"] = "NO";
                    ViewState["sesssionid"] = My.get_session_id();
                    ViewState["Branchid"] = mycode.getbranchid(Session["teacher"].ToString());
                    ViewState["teacher"] = Session["teacher"].ToString();
                    mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from session_details order by  Session asc");
                    ddl_session.SelectedValue = ViewState["sesssionid"].ToString();
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table where course_id in(select CategoryID from TeacherCourseSubjectMaping where UserID='" + ViewState["teacher"].ToString() + "') order by Position asc");
                }
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["IsPlusTwoChecked"] = "NO";
                mycode.bind_ddl(ddl_section, "Select distinct section from TeacherCourseSubjectMaping where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and UserID='" + ViewState["teacher"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "' order by section asc");
                find_terms(); find_page();
            }
            catch (Exception ex)
            {
            }
        }
        private void find_page()
        {
            DataTable dt = mycode.FillData("select Final_single,Final_bulk from Exam_report_card_setting_classwise where Class_id='" + ddl_CourseCat.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                ViewState["SingleRC"] = dt.Rows[0]["Final_single"].ToString();
                ViewState["BulKRC"] = dt.Rows[0]["Final_bulk"].ToString();
            }
        }

        private void find_terms()
        {
            ViewState["TermId"] = "0";
            string query = "select Short_Name,Grade_System_Id,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddl_session.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddl_CourseCat.SelectedValue + " order by Sequence_No asc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 2)
            {
                ViewState["TermId"] = dt.Rows[1]["Exam_Term_Id"].ToString();
            }
            if (dt.Rows.Count == 3)
            {
                ViewState["TermId"] = dt.Rows[2]["Exam_Term_Id"].ToString();
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session.");
                ddl_session.Focus();
            }
            else if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alert("Please select class.");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
                ddl_section.Focus();
            }
            else
            {
                ViewState["IsPlusTwoChecked"] = "NO";
                fetch_reportcards();
            }
        }

        private void fetch_reportcards()
        {
            string qry = "";
            if (ddl_section.SelectedItem.Text == "ALL")
            {
                qry = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddl_session.SelectedValue + " and em.Class_id=" + ddl_CourseCat.SelectedValue + " and em.Term='" + ViewState["TermId"].ToString() + "'  and ar.Status='1' order by ar.rollnumber asc";
            }
            else
            {
                qry = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddl_session.SelectedValue + " and em.Class_id=" + ddl_CourseCat.SelectedValue + " and ar.Section='" + ddl_section.SelectedItem.Text + "' and em.Term='" + ViewState["TermId"].ToString() + "'  and ar.Status='1' order by ar.rollnumber asc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alert("Sorry there are no data list exist");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        protected void btn_print_all_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alert("Please select session");
                    ddl_session.Focus();
                }
                else if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");
                    ddl_CourseCat.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alert("Please select section");
                    ddl_section.Focus();
                }
                else
                {
                    Response.Redirect("../Examination_Admin/slip/" + ViewState["BulKRC"].ToString() + "?Section=" + ddl_section.SelectedItem.Text + "&ssion_id=" + ddl_session.SelectedValue + "&clss_id=" + ddl_CourseCat.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=0&RequestFrom=Teacher", false);
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
                Label lbl_admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_branch_id = ((Label)e.Item.FindControl("lbl_branch_id")) as Label;
                HtmlAnchor rpcard_link = (HtmlAnchor)e.Item.FindControl("rpcard_link");
                rpcard_link.HRef = "../Examination_Admin/slip/" + ViewState["SingleRC"].ToString() + "?admNo=" + lbl_admissionserialnumber.Text + "&ssion_id=" + lbl_session_id.Text + "&clss_id=" + lbl_class_id.Text + "&Section=0&Branch_id=" + lbl_branch_id.Text + "&RequestFrom=Teacher";
            }
        }
    }
}