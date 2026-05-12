using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class AllEnrolledStudents : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try { if (!IsPostBack) { BindCourse(); BindGridView(); };}
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindCourse()
        {
            code.bind_all_ddl_with_id(ddl_SearchCategory, "Select CategoryName, CategoryID from ClassMaster where Istatus='1' order by CategoryName");
            code.bind_all_ddl_with_id(ddl_searchInstructor, "Select Name, UserID from InstructorProfile where Istatus='1' order by Name");
        }
        protected void ddl_SearchCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            code.bind_all_ddl_with_id(ddl_searchcourse, "Select CourseName, CourseID from Course_or_Subject_Master where CategoryID='" + ddl_SearchCategory.SelectedValue + "' and Istatus='1' order by CourseName");
        }
        private void BindGridView()
        {
            code.bind_gridview(GridView1, " select distinct * from UserProfile U join TrackingMaster T on U.UserID=T.UserID where T.CourseID in(select AssignCourseID from TeacherCourseSubjectMaping)  order by T.Id Desc");
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.BottomPagerRow;
           // GridView1.PageIndex = DDLPage.SelectedIndex;
            code.bind_gridview(GridView1, " select distinct * from UserProfile U join TrackingMaster T on U.UserID=T.UserID where T.CourseID in(select AssignCourseID from TeacherCourseSubjectMaping)  order by T.Id Desc");
            GridView1.DataBind();
        }

        protected void btn_Find_Click(object sender, EventArgs e)
        {
            if (ddl_SearchCategory.SelectedValue == "0") { Alert("Select Category Name."); }
            else if (ddl_searchcourse.SelectedValue == "0") { Alert("Select Course Name."); }
            else if (ddl_searchInstructor.SelectedValue == "0") { Alert("Select Instructor Name."); }
            else{
            code.bind_gridview(GridView1, "select distinct * from UserProfile U join TrackingMaster T on U.UserID=T.UserID where T.CourseID in(select AssignCourseID from TeacherCourseSubjectMaping where UserID='" + ddl_searchInstructor.SelectedValue + "') and  T.CategoryID='" + ddl_SearchCategory.SelectedValue + "' and T.CourseID='" + ddl_searchcourse.SelectedValue + "' order by T.Id Desc");
        }}
    }
}