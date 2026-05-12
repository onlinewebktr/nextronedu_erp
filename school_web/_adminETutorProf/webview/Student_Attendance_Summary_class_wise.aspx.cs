using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Collections;

namespace school_web._adminETutorProf.webview
{
    public partial class Student_Attendance_Summary_class_wise : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            ViewState["Sessionid"] = My.get_session_id();
            hd_session_id.Value = My.get_session_id();
            if (Request.QueryString["regid"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    mycode.bind_all_ddl_with_id(ddlclass, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["regid"].ToString() + "' and Session_id='" + ViewState["Sessionid"].ToString() + "')  order by Position asc");
                    ddlclass.SelectedValue = My.get_top_one_class_teacher(ViewState["regid"].ToString());
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id='" + ddlclass.SelectedValue + "' and Session_id='" + hd_session_id.Value + "' order by Section");
                    ddl_section.Text = My.get_Section_via_class(ddlclass.SelectedValue);


                    string sessions = My.get_session();
                    string[] stringSeparatorss = new string[] { "-" };
                    string[] arrs = sessions.Split(stringSeparatorss, StringSplitOptions.None);
                    string Year1 = arrs[0];
                    string Year2 = arrs[1];
                    mycode.bind_ddl_year(ddlyear);
                    ddlyear.Text = mycode.year();
                    mycode.bind_all_ddl_with_id(ddl_month, "Select  Month,Month_Id from Month_Index order by Position asc");
                    ddl_month.SelectedValue = mycode.get_current_month_id();
                    int starte = My.toint(Year1);
                    int endyaer = My.toint(Year2);
                    ArrayList ar = new ArrayList();
                    ar.Add("Select");
                    for (int i = starte; i <= endyaer; i++)
                    {
                        ar.Add(i);
                    }
                    ddlyear.DataSource = ar;
                    ddlyear.DataBind();
                }

            }
        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class");
            }
            else
            {

                mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id='" + ddlclass.SelectedValue + "' and Session_id='"+hd_session_id.Value+"' order by Section");
            }
        }

        private void Alertme(string Message)
        {
            lbl_msg.Text = Message;

            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }


    }
}