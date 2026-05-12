using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
namespace school_web.Examination_Admin
{
    public partial class Report_Full_Marks : System.Web.UI.Page
    {
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

        UsesCode mycode = new UsesCode();
        My my = new My();
        protected void Page_Load(object sender, EventArgs e)
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
                    ViewState["sesssionid"] = My.get_session_id();
                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());

                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");

                    mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_CourseCat.SelectedValue + "'  order by section");


                }
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {

                Alertme("Please select class.", "warning");

            }
            else
            {
                mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "' order by Section");
                mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");

                mycode.bind_all_ddl_with_id(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position  from Subject_Master sm where sm.course_id='" + ddl_CourseCat.SelectedValue + "' and Is_mandatory='1'    order by sm.Subject_position  ");

            }
        }



        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {

                Alertme("Please select class.", "warning");

            }
            else
            {

                mycode.bind_all_ddl_with_id(ddl_assesment, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Assessment_Name asc");

            }
        }

        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alertme("Please select section.", "warning");
                ddl_section.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alertme("Please select term.", "warning");
                ddl_term.Focus();
            }
            else if (ddl_assesment.SelectedItem.Text == "Select")
            {
                Alertme("Please select Assessment.", "warning");
                ddl_assesment.Focus();
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alertme("Please select subject.", "warning");
                ddl_subject.Focus();
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_exam_level, "select Subject_Activity_Name,Subject_Sub_Level_Id from Exam_Subject_Sub_Level where Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Assessment_Id='" + ddl_assesment.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' order by Subject_Activity_Name asc");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alertme("Please select section.", "warning");
                ddl_section.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alertme("Please select term.", "warning");
                ddl_term.Focus();
            }
            else if (ddl_assesment.SelectedItem.Text == "Select")
            {
                Alertme("Please select Assessment.", "warning");
                ddl_assesment.Focus();
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alertme("Please select subject.", "warning");
                ddl_subject.Focus();
            }
            else if (ddl_exam_level.SelectedItem.Text == "Select")
            {
                Alertme("Please select subject.", "warning");
                ddl_subject.Focus();
            }
            else
            {
                find_data();

            }
        }

        private void find_data()
        {
            string query = " Select em.*,ar.studentname,ar.rollnumber,ar.admissionserialnumber,ar.Section,(Select top 1 Assessment_Name from  Exam_Assessment_Details  where   Session_Id=em.Session_Id and Branch_Id=em.Branch_Id and Exam_Term_Id=em.Term and Assessment_Id=em.Assessment ) as  Assessment_Name,(Select top 1 Subject_name from Subject_Master  where course_id=em.Class_id  and  Subject_id=em.Subject)as Subject_name,(Select top 1 Subject_Activity_Name from Exam_Subject_Sub_Level where Session_id=em.Session_id and Branch_Id=em.Branch_Id and Class_id=em.Class_id and Subject_Sub_Level_Id=em.Subject_activity ) as Subject_Activity_Name,(Select top 1 Maximum_Marks from Exam_Subject_Sub_Level where Session_id=em.Session_id and Branch_Id=em.Branch_Id and Class_id=em.Class_id and Subject_Sub_Level_Id=em.Subject_activity ) as Maximum_Marks from Exam_marks em join admission_registor ar  on ar.admissionserialnumber=em.Admission_no and ar.Session_id=em.Session_id   and ar.Class_id=em.Class_id where  em.Session_id=" + ViewState["sesssionid"].ToString() + " and em.Branch_id=" + ViewState["Branchid"].ToString() + " and em.Class_id=" + ddl_CourseCat.SelectedValue + " and  em.Section='" + ddl_section.Text + "' and em.Term=" + ddl_term.SelectedValue + " and em.Assessment=" + ddl_assesment.SelectedValue + " and em.Subject=" + ddl_subject.SelectedValue + " and em.Subject_activity=" + ddl_exam_level.SelectedValue + " order by ar.rollnumber";
            bind_data_grid(query);
        }

        private void bind_data_grid(string query)
        {

            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }


        #region find excel
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
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
        #endregion
    }
}