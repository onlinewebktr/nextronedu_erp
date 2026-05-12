using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class Class_Routing : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["periodcount"] = My.get_period(My.get_session_id());
                ViewState["Sessionid"] = My.get_session_id();
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    mycode.bind_ddl(ddl_day, "Select Day   from Day_Master order by Position");
                    ddl_day.Text = mycode.day();
                    mycode.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["regid"].ToString() + "' and Session_id='" + ViewState["Sessionid"].ToString() + "')  order by Position asc");


                    ddl_class.SelectedValue = My.get_top_one_class_teacher(ViewState["regid"].ToString());
                    


                    Bind_Data_chart();


                   

                }
            }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;

            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddl_day.Text == "Select")
            {
                Alert("Please select day");
            }
            else
            {
                 Bind_Data_chart();
            }

        }
        private void Bind_Data_chart()
        {
            DataTable dt = mycode.get_class_routine_day_wise_selectedteacher(ViewState["Sessionid"].ToString(), ddl_day.Text,ViewState["regid"].ToString(), ddl_class.SelectedValue);

            if (dt.Rows.Count == 0)
            {
                Alert("Sorry! There are not class routing found.");
                grd_classchart.DataSource = null;
                grd_classchart.DataBind();

            }
            else
            {

                grd_classchart.DataSource = dt;
                grd_classchart.DataBind();
            }
        }

     


        protected void grd_classchart_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //string get_dayname = System.DateTime.Now.ToString("dddd");
            //if (e.Row.Cells[0].Text == get_dayname)
            //{




            //    for (int i = 0; i <= Convert.ToInt32(ViewState["periodcount"].ToString()); i++)
            //    {
            //        e.Row.Cells[i].BackColor = Color.Red;
            //    }







            //}
        }

      
    }
}