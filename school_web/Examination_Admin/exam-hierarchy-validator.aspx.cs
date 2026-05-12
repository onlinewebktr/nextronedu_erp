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
    public partial class exam_hierarchy_validator1 : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["sesssionid"] = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by position asc");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_User");
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

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_terms();
                make_active_term();
                find_exam_structure();
            }
            catch (Exception ex)
            {
            }
        }



        private void make_active_term()
        {
            int growcount = rp_terms.Items.Count;
            for (int ix = 0; ix < growcount; ix++)
            {
                LinkButton lnk_terms = (LinkButton)rp_terms.Items[ix].FindControl("lnk_terms");
                Label lbl_term_id = (Label)rp_terms.Items[ix].FindControl("lbl_term_id");
                if (ix == 0)
                {
                    lnk_terms.CssClass = "chosen-btns-active chosen-btns";
                    ViewState["TerMid"] = lbl_term_id.Text;
                    ViewState["TerMnamE"] = lnk_terms.Text;
                }
            }
        }
        protected void lnk_terms_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                LinkButton lnk_terms = (LinkButton)row.FindControl("lnk_terms") as LinkButton;
                Label lbl_term_id = (Label)row.FindControl("lbl_term_id") as Label;
                foreach (RepeaterItem item in rp_terms.Items)
                {
                    LinkButton lnk_termss = (LinkButton)item.FindControl("lnk_terms") as LinkButton;
                    lnk_termss.CssClass = "chosen-btns";
                }
                lnk_terms.CssClass = "chosen-btns-active chosen-btns";

                ViewState["TerMid"] = lbl_term_id.Text;
                ViewState["TerMnamE"] = lnk_terms.Text;
                find_terms(); find_exam_structure();
            }
            catch
            {
            }
        }


        private void find_terms()
        {
            string query = "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 order by Term_Name asc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                rp_terms.DataSource = null;
                rp_terms.DataBind();
            }
            else
            {
                rp_terms.DataSource = dt;
                rp_terms.DataBind();
            }
        }

        private void find_exam_structure()
        {
            lbl_trm_heading.Text = ViewState["TerMnamE"].ToString();
            string query = "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ViewState["TerMid"].ToString() + "' and Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rd_subjects = ((Repeater)e.Item.FindControl("rd_subjects")) as Repeater;
                Label lbl_assessment_id = ((Label)e.Item.FindControl("lbl_assessment_id")) as Label;
                HtmlGenericControl subjectDV = ((HtmlGenericControl)e.Item.FindControl("subjectDV")) as HtmlGenericControl;
                Label lbl_subjectDV = ((Label)e.Item.FindControl("lbl_subjectDV")) as Label;

                string query = "Select DISTINCT sm.Subject_name,sm.Subject_id," + lbl_assessment_id.Text + " as Assessment_id from Subject_Master sm join Exam_Subject_Sub_Level sbl on sm.Subject_id=sbl.Subject_id where sbl.Class_id='" + ddl_class.SelectedValue + "' and sbl.Exam_Term_Id='" + ViewState["TerMid"].ToString() + "' and sbl.Assessment_Id='" + lbl_assessment_id.Text + "' and sbl.Session_Id='" + ViewState["sesssionid"].ToString() + "' and sbl.Branch_Id='" + ViewState["branchid"].ToString() + "'";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    //   subjectDV.Attributes.Add("class", "hidden"); 
                    lbl_subjectDV.Visible = false;
                    rd_subjects.DataSource = null;
                    rd_subjects.DataBind();
                }
                else
                {
                    //subjectDV.Attributes.Add("class", "show");
                    lbl_subjectDV.Visible = true;
                    rd_subjects.DataSource = dt;
                    rd_subjects.DataBind();
                }
            }
        }

        protected void rd_subjects_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rd_activity = ((Repeater)e.Item.FindControl("rd_activity")) as Repeater;
                Label lbl_subject_id = ((Label)e.Item.FindControl("lbl_subject_id")) as Label;
                Label lbl_assessment_id = ((Label)e.Item.FindControl("lbl_assessment_id")) as Label;
                Label lbl_activityDV = ((Label)e.Item.FindControl("lbl_activityDV")) as Label;

                string query = "Select sm.Subject_id,sbl.Subject_Activity_Name as Subject_name,sbl.Subject_Sub_Level_Id from Subject_Master sm join Exam_Subject_Sub_Level sbl on sm.Subject_id=sbl.Subject_id where sbl.Class_id='" + ddl_class.SelectedValue + "' and sbl.Exam_Term_Id='" + ViewState["TerMid"].ToString() + "' and sbl.Assessment_Id='" + lbl_assessment_id.Text + "' and sbl.Session_Id='" + ViewState["sesssionid"].ToString() + "' and sbl.Branch_Id='" + ViewState["branchid"].ToString() + "' order by sbl.Sequence_No asc";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    lbl_activityDV.Visible = false;
                    rd_activity.DataSource = null;
                    rd_activity.DataBind();
                }
                else
                {
                    lbl_activityDV.Visible = true;
                    rd_activity.DataSource = dt;
                    rd_activity.DataBind();
                }
            }
        }
    }
}