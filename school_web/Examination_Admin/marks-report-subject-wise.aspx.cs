using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class marks_report_subject_wise : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        Session["Branchid"] = ViewState["Branchid"].ToString();
                        bind_session();
                        ddl_session.SelectedValue = My.get_session_id();
                        bind_class();
                        //ddl_class.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_section.Text = My.get_top_one_section();

                        mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddl_session.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddl_class.SelectedValue + " order by Term_Name asc");


                        mycode.bind_all_ddl_with_id(ddl_subject, "select DISTINCT t2.Subject_name,t2.Subject_id from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + ddl_session.SelectedValue + " and t1.Class_id=" + ddl_class.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic'");
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
            mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id,Position from Add_course_table order by Position");
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e) 
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddl_session.Focus();
                }
                else
                {
                    bind_terms(); bind_subject();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_subject()
        {
            mycode.bind_all_ddl_with_id(ddl_subject, "select DISTINCT t2.Subject_name,t2.Subject_id from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + ddl_session.SelectedValue + " and t1.Class_id=" + ddl_class.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic'");


            DataTable dt = mycode.FillData("select distinct Subject_group from Subject_Master where course_id=" + ddl_class.SelectedValue + " and (Subject_group='1001' or Subject_group='2001') and Branch_id='" + ViewState["Branchid"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Subject_group"].ToString() == "1001")
                    {
                        ddl_subject.Items.Insert(1, new ListItem("Science", "1001"));
                    }

                    if (dr["Subject_group"].ToString() == "2001")
                    {
                        ddl_subject.Items.Insert(2, new ListItem("Social Science", "2001"));
                    }
                } 
            }
        }

        private void bind_terms()
        {
            mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddl_session.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddl_class.SelectedValue + " order by Term_Name asc");
        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_class();
        }
    }
}