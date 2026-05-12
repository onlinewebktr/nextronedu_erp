using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class classwise_routine : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    hd_session_id.Value = My.get_session_id();
                    hd_teacher_id.Value = Request.QueryString["regid"].ToString();
                    mycode.bind_all_ddl_with_id(ddl_classs, "Select Course_Name,course_id from Add_course_table order by Position");
                }
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                My.bind_ddl_select(ddl_sections, "Select distinct Section from Class_Routine_Master where Session_id=" + hd_session_id.Value + " and Class_id='" + ddl_classs.SelectedValue + "' order by Section");
            }
            catch (Exception ex)
            {
            }
        }
    }
}