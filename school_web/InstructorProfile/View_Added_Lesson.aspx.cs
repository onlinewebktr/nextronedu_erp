using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.InstructorProfile
{
    public partial class View_Added_Lesson : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        ViewState["teacher"] = Session["teacher"].ToString();
                        //code.bind_all_ddl_with_all(ddl_SearchCategory, "Select CategoryName, CategoryID from ClassMaster order by CategoryName");
                        code.bind_all_ddl_with_all(ddl_SearchCategory, "Select CategoryName, CategoryID from ClassMaster where CategoryID in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') and Istatus=1 order by Position");
                        code.bind_ddl(ddl_search_section, "Select distinct section  from Course_or_Subject_Master where CourseID in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by section");
                       // code.bind_ddl_all1(ddl_search_section, "Select distinct section  from Course_or_Subject_Master  order by section");
                        BindGridView();

                    }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindGridView()
        {
            string query = "select   sm.*, (select CategoryName from ClassMaster where CategoryID=sm.CategoryID) as CategoryName, case when IsForQuiz='1' then 'Yes' else 'No' end as ForQuiz, csm.CourseName  from SectionMaster sm join Course_or_Subject_Master csm on sm.CategoryID=csm.CategoryID and sm.CourseID=csm.CourseID and sm.Section_Subject=csm.section  where sm.CourseID in (select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by  csm.CourseName,sm.Section_Subject asc ";


            searchdata(query);


        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        private void searchdata(string query)
        {
            ViewState["query"] = query;
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                Alert("Sorry recode not fund.");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        protected void ddl_SearchCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_SearchCategory.SelectedItem.Text == "ALL")
            {
                code.bind_ddl_all1(ddl_search_section, "Select distinct section  from Course_or_Subject_Master  order by section");
            }
            else
            {
                code.bind_ddl_all1(ddl_search_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_SearchCategory.SelectedValue + "' and Istatus='1' and   CourseID in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by section");
                //code.bind_ddl_all1(ddl_search_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_SearchCategory.SelectedValue + "'  order by section");
            }
        }

        protected void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                if (ddl_SearchCategory.SelectedItem.Text == "ALL" && ddl_search_section.Text == "ALL")
                {
                    query = "select   sm.*, (select CategoryName from ClassMaster where CategoryID=sm.CategoryID) as CategoryName, case when IsForQuiz='1' then 'Yes' else 'No' end as ForQuiz, csm.CourseName  from SectionMaster sm join Course_or_Subject_Master csm on sm.CategoryID=csm.CategoryID and sm.CourseID=csm.CourseID and sm.Section_Subject=csm.section where sm.CourseID in (select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by  csm.CourseName,sm.Section_Subject asc ";


                    searchdata(query);
                }
                else if (ddl_SearchCategory.SelectedItem.Text != "ALL" && ddl_search_section.Text == "ALL")
                {
                    query = "select   sm.*, (select CategoryName from ClassMaster where CategoryID=sm.CategoryID) as CategoryName, case when IsForQuiz='1' then 'Yes' else 'No' end as ForQuiz, csm.CourseName  from SectionMaster sm join Course_or_Subject_Master csm on sm.CategoryID=csm.CategoryID and sm.CourseID=csm.CourseID and sm.Section_Subject=csm.section where sm.CategoryID='" + ddl_SearchCategory.SelectedItem.Text + "' and sm.CourseID in (select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by  csm.CourseName,sm.Section_Subject asc";


                    searchdata(query);
                }
                else if (ddl_SearchCategory.SelectedItem.Text != "ALL" && ddl_search_section.Text != "ALL")
                {
                    query = "select   sm.*, (select CategoryName from ClassMaster where CategoryID=sm.CategoryID) as CategoryName, case when IsForQuiz='1' then 'Yes' else 'No' end as ForQuiz, csm.CourseName  from SectionMaster sm join Course_or_Subject_Master csm on sm.CategoryID=csm.CategoryID and sm.CourseID=csm.CourseID and sm.Section_Subject=csm.section where sm.CategoryID='" + ddl_SearchCategory.SelectedItem.Text + "' and sm.Section_Subject='" + ddl_search_section.Text + "' and sm.CourseID in (select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by  csm.CourseName,sm.Section_Subject asc";
                }
                else if (ddl_SearchCategory.SelectedItem.Text == "ALL" && ddl_search_section.Text != "ALL")
                {
                    query = "select   sm.*, (select CategoryName from ClassMaster where CategoryID=sm.CategoryID) as CategoryName, case when IsForQuiz='1' then 'Yes' else 'No' end as ForQuiz, csm.CourseName  from SectionMaster sm join Course_or_Subject_Master csm on sm.CategoryID=csm.CategoryID and sm.CourseID=csm.CourseID and sm.Section_Subject=csm.section where   sm.Section_Subject='" + ddl_search_section.Text + "' and sm.CourseID in (select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by  csm.CourseName,sm.Section_Subject asc";
                }
                else
                {
                    query = "select   sm.*, (select CategoryName from ClassMaster where CategoryID=sm.CategoryID) as CategoryName, case when IsForQuiz='1' then 'Yes' else 'No' end as ForQuiz, csm.CourseName  from SectionMaster sm join Course_or_Subject_Master csm on sm.CategoryID=csm.CategoryID and sm.CourseID=csm.CourseID and sm.Section_Subject=csm.section where sm.CategoryID='" + ddl_SearchCategory.SelectedItem.Text + "' and sm.CourseID in (select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by  csm.CourseName,sm.Section_Subject asc";

                }

                searchdata(query);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = lbl_id.Text;
                Response.Redirect("Add_Lesson.aspx?id=" + lbl_id.Text, false);


            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Label lbl_SectionID = (Label)row.FindControl("lbl_SectionID");
                if (check_section_is_in_use_or_not(lbl_SectionID))
                {
                    SqlCommand cmd = new SqlCommand("Delete from SectionMaster where Id='" + lbl_id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                    Alert("Record deleted successfully.");
                    searchdata(ViewState["query"].ToString());
                }
                else
                {
                    Alert("This lesson is already used in study material.");
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private bool check_section_is_in_use_or_not(Label lbl_SectionID)
        {
            DataTable dt = code.FillTable("select * from TopicMaster where Id='" + lbl_SectionID.Text + "'");
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}