using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class marks_entry_report : System.Web.UI.Page
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
                        hd_find_status.Value = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_session();

                        ddlsession.SelectedValue = My.get_session_id();
                        bind_class();
                        mycode.bind_ddl(ddl_term, "select distinct Term_Name from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");
                        // mycode.bind_all_ddl_with_id_cap_All(ddl_term, "select distinct Term_Name,Term_Name from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Sequence_No asc");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
            }
        }


        private void bind_class()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Course_Name,course_id from Add_course_table order by Position", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_classs.DataTextField = "Course_Name";
                ddl_classs.DataValueField = "course_id";
                ddl_classs.DataSource = reader;
                ddl_classs.DataBind();
            }
        }


        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
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




        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hd_find_status.Value = "0";
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("Please select term.", "warning");
                    ddl_term.Focus();
                    return;
                }
                fetch_exam();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_exam()
        {
            bool isClassSelectd = false; string selectClassid = "";
            foreach (ListItem item in ddl_classs.Items)
            {
                if (item.Selected)
                {
                    selectClassid = selectClassid + item.Value + ",";
                    isClassSelectd = true;
                }
            }
            if (isClassSelectd == false)
            {
                ddl_classs.Focus();
                Alertme("Please select class.", "warning");
                return;
            }
            if (isClassSelectd == true)
            {
                selectClassid = selectClassid.Remove(selectClassid.Length - 1);
            }

            string term_id = ""; bool istermAvl = false;
            DataTable dt = My.dataTable("select distinct Exam_Term_Id from Exam_Term_Details where Session_Id='" + ddlsession.SelectedValue + "' and Istatus=1 and Term_Name='" + ddl_term.Text + "' and Class_id in(" + selectClassid + ")");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    term_id = term_id + dr["Exam_Term_Id"].ToString() + ",";
                    istermAvl = true;
                }
            }
            if (istermAvl == true)
            {
                term_id = term_id.Remove(term_id.Length - 1);
            }
            else
            {
                term_id = "0";
            }

            mycode.bind_ddl(ddl_exam, "select distinct Assessment_Name from Exam_Assessment_Details where Session_Id='" + ddlsession.SelectedValue + "' and Istatus=1 and Class_id in(" + selectClassid + ") and Exam_Term_Id in (" + term_id + ") order by Assessment_Name asc");
        }



        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    ddlsession.Focus();
                    Alertme("Please select session.", "warning");
                    return;
                }

                bool isClassSelectd = false; string selectClassid = "";
                foreach (ListItem item in ddl_classs.Items)
                {
                    if (item.Selected)
                    {
                        selectClassid = selectClassid + item.Value + ",";
                        isClassSelectd = true;
                    }
                }
                if (isClassSelectd == false)
                {
                    ddl_classs.Focus();
                    Alertme("Please select class.", "warning");
                    return;
                }
                if (isClassSelectd == true)
                {
                    selectClassid = selectClassid.Remove(selectClassid.Length - 1);
                }

                string term_id = ""; bool istermAvl = false;
                DataTable dt = My.dataTable("select distinct Exam_Term_Id from Exam_Term_Details where Session_Id='" + ddlsession.SelectedValue + "' and Istatus=1 and Term_Name='" + ddl_term.Text + "' and Class_id in(" + selectClassid + ")");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        term_id = term_id + dr["Exam_Term_Id"].ToString() + ",";
                        istermAvl = true;
                    }
                }
                if (istermAvl == true)
                {
                    term_id = term_id.Remove(term_id.Length - 1);
                }
                if (istermAvl == false)
                {
                    ddl_term.Focus();
                    Alertme("Please select term.", "warning");
                    return;
                }

                if (ddl_exam.SelectedItem.Text == "Select")
                {
                    ddl_exam.Focus();
                    Alertme("Please select exam.", "warning");
                    return;
                }


                hd_find_status.Value = "1";
                hd_session_id.Value = ddlsession.SelectedValue; 
                hd_class_id.Value = selectClassid; 
                hd_term_id.Value = term_id;
                hd_exam.Value = ddl_exam.SelectedItem.Text;
            }
            catch (Exception ex)
            {
            }
        }
    }
}