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
    public partial class Report_Result_Chart : System.Web.UI.Page
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
        Examination EC = new Examination();
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
                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());
                    bind_session();
                    ddl_session.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");
                    
                    mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_CourseCat.SelectedValue + "'  order by section");
                    mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Session_Id='" + ddl_session.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");


                }
            }
        }
        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details");
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
                mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Session_Id='" + ddl_session.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");



            }
        }
        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {

                Alertme("Please select class.", "warning");

            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {

                Alertme("Please select exam term", "warning");

            }
            else
            {
                my.bind_all_ddl_with_id_cap_All(ddl_assesment, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Session_Id='" + ddl_session.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Sequence_No asc");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                { 
                    Alertme("Please select class.", "warning"); 
                }
                else if (ddl_section.Text == "Select")
                { 
                    Alertme("Please select section.", "warning"); 
                }
                else if (ddl_term.Text == "Select")
                { 
                    Alertme("Please select term.", "warning"); 
                }
                else
                {
                    try
                    {
                        string query = "";
                        if (ddl_assesment.SelectedItem.Text == "ALL")
                        {
                            query = EC.get_subject_heading_subjective_new_all(ddl_CourseCat.SelectedValue, ddl_section.Text, ddl_term.SelectedValue, ddl_session.SelectedValue, ViewState["Branchid"].ToString());
                        }
                        else
                        {
                            query = EC.get_subject_heading_subjective_new(ddl_CourseCat.SelectedValue, ddl_section.Text, ddl_term.SelectedValue, ddl_assesment.SelectedValue, ddl_session.SelectedValue, ViewState["Branchid"].ToString());
                        } 
                        DataTable dt = mycode.FillTable(query);
                        if (dt.Rows.Count == 0)
                        { 
                            Alertme("Sorry result not  found", "warning");
                            GrdView.DataSource = null;
                            GrdView.DataBind();
                            btn_excels.Visible = false;
                        }
                        else
                        {
                            GrdView.DataSource = dt;
                            GrdView.DataBind();
                            btn_excels.Visible = true;
                        }
                    }
                    catch
                    {
                        Alertme("Your subject name is not correct format", "warning");
                        GrdView.DataSource = null;
                        GrdView.DataBind();
                        btn_excels.Visible = false;
                    } 
                }
            }
            catch
            {
            } 
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ResultExport" + mycode.date() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GrdView.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");
        }
    }
}