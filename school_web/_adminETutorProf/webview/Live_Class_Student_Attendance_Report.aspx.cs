using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class Live_Class_Student_Attendance_Report : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["teacher"] = Request.QueryString["regid"].ToString();
                    if (!IsPostBack)
                    {
                        ViewState["sessionid"] = My.get_session_id();
                        txt_date.Text = mycode.date();
                        txt_enddate.Text = mycode.date();

                        string gettypeteacher = code.get_teacher_type(ViewState["teacher"]);
                        ViewState["teachertye"] = gettypeteacher;
                        if (gettypeteacher == "0")
                        {
                            code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "'  and Session_id='" + ViewState["sessionid"].ToString() + "')  order by Position asc");

                        }
                        else
                        {
                            code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  Ptm_class_teacher_mapping   where UserID='" + ViewState["teacher"].ToString() + "'  and Session_id='" + ViewState["sessionid"].ToString() + "')  order by Position asc");

                        }
                        txt_date.Text = code.date();

                       // btn_find_data();


                    }
                }
            }
            catch
            {
            }
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;

            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else
            {
                if (ViewState["teachertye"].ToString() == "0")
                {
                    code.bind_ddl(ddl_section, "Select distinct section   from TeacherCourseSubjectMaping  where CategoryID ='" + ddl_class.SelectedValue + "' and Istatus='1' and UserID='" + ViewState["teacher"].ToString() + "'  order by section");

                }
                else
                {
                    code.bind_ddl(ddl_section, "Select distinct section  from section_master    order by section");
                }
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {

            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (txt_date.Text == "")
            {
                Alert("Please select start date");


            }
            else if (txt_enddate.Text == "")
            {
                Alert("Please select end date");
            }
            else
            {
                btn_find_data();
            }

        }

        private void btn_find_data()
        {
            try
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
                {
                    lbl_month_year.Text = "Start Date -" + txt_date.Text + "-" + "End Date -" + txt_enddate.Text;
                    SqlCommand cmd = new SqlCommand();
                    ViewState["teachertye"] = code.get_teacher_type(ViewState["teacher"]);
                    if (ViewState["teachertye"].ToString() == "0")
                    {
                        cmd.Parameters.AddWithValue("@cmdstatus", "14");
                        cmd.Parameters.AddWithValue("@Teacher_Id", ViewState["teacher"].ToString());
                    }
                    else  
                    {
                        cmd.Parameters.AddWithValue("@cmdstatus", "11");
                    }
                    cmd.Parameters.AddWithValue("@startdate", mycode.ConvertStringToiDate(txt_date.Text));
                    cmd.Parameters.AddWithValue("@enddate", mycode.ConvertStringToiDate(txt_enddate.Text));
                    cmd.Parameters.AddWithValue("@section", ddl_section.Text);
                    cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);
                   
                    cmd.CommandText = "sp_VC_class_report";
                    DataTable dt = UsesCode.Getdata_sp(cmd);
                    if (Convert.ToString(dt.Rows.Count) == "0")
                    {
                        lbl_total.Text = "0";
                        Alert("Sorry there are no attendance report available");

                        GrdView.DataSource = null;
                        GrdView.DataBind();



                    }
                    else
                    {
                        lbl_month_year.Text = "Start Date -" + txt_date.Text + "-" + "End Date -" + txt_enddate.Text;
                        lbl_total.Text = dt.Rows.Count.ToString();
                        GrdView.DataSource = dt;
                        GrdView.DataBind();
                    }
                }
                else
                {
                    Alert("Please select valid date ");
                }
            }
            catch
            {
            }
        }




    }
}