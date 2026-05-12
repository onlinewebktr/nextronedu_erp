using school_web.AppCode;
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class CourseView : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hdUserId.Value = Session["User"].ToString();
                    //code.BindRepeater("select distinct C.CourseID,C.image, C.CourseName, C.Description, (select CategoryName from CategoryMaster where CategoryID=C.CategoryID) as CategoryName from CourseMaster C join TrackingMaster T on T.CourseID=C.CourseID where C.Istatus=1 and T.UserID='" + hdUserId.Value + "' and T.Status='Enrolled' Order by CategoryName desc", RpEnrollCourse);
                    //code.BindRepeater("select distinct C.CourseID,C.image, C.CourseName, C.Description, (select CategoryName from CategoryMaster where CategoryID=C.CategoryID) as CategoryName from CourseMaster C join TrackingMaster T on T.CourseID=C.CourseID where C.Istatus=1 and T.UserID='" + hdUserId.Value + "' and T.Status='Completed' Order by CategoryName desc", RpCompletedCourse);

                    code.BindRepeater("select distinct C.CourseID,C.image, C.CourseName, C.Description, C.section, (select CategoryName from ClassMaster where CategoryID=C.CategoryID) as CategoryName from Course_or_Subject_Master C where C.Istatus=1 and C.CategoryID in( select Class_id from dbo.[admission_registor] where admissionserialnumber ='" + hdUserId.Value + "') and section in (select Section from dbo.[admission_registor] where admissionserialnumber ='" + hdUserId.Value + "') Order by CategoryName desc", RpEnrollCourse);


                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
    }
}