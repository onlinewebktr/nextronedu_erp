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
    public partial class published_result : System.Web.UI.Page
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
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_session();
                        bind_class();
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_term.Items.Insert(0, new ListItem("ALL", "0"));
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
            mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
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
            if (ddlclass.SelectedItem.Text == "ALL")  // By Session
            {
                if (ddl_term.SelectedItem.Text == "ALL")
                {
                    qry = "select DISTINCT t3.Id,t3.Status,Format(convert(DateTime,t3.Published_date,103), 'dd/MM/yyyy') as new_date,em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber join Exam_publish_result t3 on em.Admission_no=t3.Admission_no and em.Session_id=t3.Session_id and em.Branch_id=t3.Branch_id and em.Term=t3.Term_id where em.Session_id='" + ddlsession.SelectedValue + "' order by t3.Id desc";
                }
                else
                {
                    qry = "select DISTINCT t3.Id,t3.Status,Format(convert(DateTime,t3.Published_date,103), 'dd/MM/yyyy') as new_date,em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber join Exam_publish_result t3 on em.Admission_no=t3.Admission_no and em.Session_id=t3.Session_id and em.Branch_id=t3.Branch_id and em.Term=t3.Term_id where em.Session_id='" + ddlsession.SelectedValue + "' and em.Term=" + ddl_term.SelectedValue + " order by t3.Id desc"; 
                }
            }
            else
            {
                if (ddl_section.SelectedItem.Text == "ALL")
                {
                    if (ddl_term.SelectedItem.Text == "ALL")
                    {
                        qry = "select DISTINCT t3.Id,t3.Status,Format(convert(DateTime,t3.Published_date,103), 'dd/MM/yyyy') as new_date,em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber join Exam_publish_result t3 on em.Admission_no=t3.Admission_no and em.Session_id=t3.Session_id and em.Branch_id=t3.Branch_id and em.Term=t3.Term_id where em.Session_id='" + ddlsession.SelectedValue + "' and em.Class_id=" + ddlclass.SelectedValue + " order by t3.Id desc";  
                    }
                    else
                    {
                        qry = "select DISTINCT t3.Id,t3.Status,Format(convert(DateTime,t3.Published_date,103), 'dd/MM/yyyy') as new_date,em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber join Exam_publish_result t3 on em.Admission_no=t3.Admission_no and em.Session_id=t3.Session_id and em.Branch_id=t3.Branch_id and em.Term=t3.Term_id where em.Session_id='" + ddlsession.SelectedValue + "' and em.Class_id=" + ddlclass.SelectedValue + " and  em.Term=" + ddl_term.SelectedValue + " order by t3.Id desc";  
                    }
                }
                else
                {
                    if (ddl_term.SelectedItem.Text == "ALL")
                    {
                        qry = "select DISTINCT t3.Id,t3.Status,Format(convert(DateTime,t3.Published_date,103), 'dd/MM/yyyy') as new_date,em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber join Exam_publish_result t3 on em.Admission_no=t3.Admission_no and em.Session_id=t3.Session_id and em.Branch_id=t3.Branch_id and em.Term=t3.Term_id where em.Session_id='" + ddlsession.SelectedValue + "' and em.Class_id=" + ddlclass.SelectedValue + " and ar.Section='" + ddl_section.SelectedItem.Text + "' order by t3.Id desc";   
                    }
                    else
                    {
                        qry = "select DISTINCT t3.Id,t3.Status,Format(convert(DateTime,t3.Published_date,103), 'dd/MM/yyyy') as new_date,em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber join Exam_publish_result t3 on em.Admission_no=t3.Admission_no and em.Session_id=t3.Session_id and em.Branch_id=t3.Branch_id and em.Term=t3.Term_id where em.Session_id='" + ddlsession.SelectedValue + "' and em.Class_id=" + ddlclass.SelectedValue + " and ar.Section='" + ddl_section.SelectedItem.Text + "' and  em.Term=" + ddl_term.SelectedValue + " order by t3.Id desc";  
                        
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
                    bind_terms();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_terms()
        {
            mycode.bind_all_ddl_with_id_cap_All(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " order by Term_Name asc");
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
                    bind_grd_view();
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
                if (((Label)e.Item.FindControl("lbl_status")).Text == "1")
                {
                    ((LinkButton)e.Item.FindControl("lnk_status")).Text = "Yes";
                    ((LinkButton)e.Item.FindControl("lnk_status")).CssClass = "isyes";
                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lnk_status")).Text = "No";
                    ((LinkButton)e.Item.FindControl("lnk_status")).CssClass = "isno";
                }
            }
        }


        protected void lnk_status_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label Id = (Label)row.FindControl("lbl_Id");
                Label lbl_status = (Label)row.FindControl("lbl_status");
                string status = "1";
                if (lbl_status.Text == "1")
                {
                    status = "0";
                }
                string qry = "update Exam_publish_result set Status='" + status + "' where Id=" + Id.Text + "";
                mycode.executequery(qry);
                Alertme("Status has been updated successfully.", "success");
                bind_grd_view();
            }
            catch (Exception ex)
            {
            }
        }

    }
}