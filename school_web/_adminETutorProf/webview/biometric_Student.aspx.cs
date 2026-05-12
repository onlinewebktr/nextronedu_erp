using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class biometric_Student : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["regid"] != null)
            {
                ViewState["teacher"] = Request.QueryString["regid"].ToString();
                if (!IsPostBack)
                {

                    txt_date.Text = code.date();
                    txt_enddate.Text = code.date();

                    string gettypeteacher = code.get_teacher_type(ViewState["teacher"]);
                    ViewState["teachertye"] = gettypeteacher;
                    if (gettypeteacher == "0")
                    {
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by Position asc");

                    }
                    else
                    {
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  Ptm_class_teacher_mapping   where UserID='" + ViewState["teacher"].ToString() + "')   order by Position asc");

                    }



                }
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
                    code.bind_ddl(ddl_section, "Select distinct section   from TeacherCourseSubjectMaping  where CategoryID ='" + ddl_class.SelectedValue + "' and Istatus='1' and   CategoryID in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by section");

                }
                else
                {
                    code.bind_ddl(ddl_section, "Select distinct section  from section_master    order by section");
                }
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
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
                    if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
                    {
                        find_data();
                    }
                    else
                    {
                        Alert("Please select valid date ");
                    }
                }
            }

            catch
            {
            }
        }

        private void find_data()
        {
            DataTable dt = new DataTable();
            string startidate = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
            string endidate = My.toDateTime(txt_enddate.Text).ToString("yyyyMMdd");
            dt = My.dataTable("select  Row_Number() over(order by id) sl,studentname,admissionserialnumber,class,rollnumber,session,ISNULL(STUFF((SELECT ', ' + cast(format(DateTime,'hh:mm tt') as varchar(50)) FROM Student_Attendance_Log WHERE admissionserialnumber = em.admissionserialnumber and    CONVERT(int, format(DateTime, 'yyyyMMdd'))>=" + startidate + " and CONVERT(int,format(DateTime, 'yyyyMMdd'))<=" + endidate + "       order by DateTime  FOR XML PATH('')), 1, 1,''), 'Not Available') AS Attendance from admission_registor em where em.Session_id='" + My.get_session_id() + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.SelectedValue + "' and em.Status='1'");
             


            if (dt.Rows.Count == 0)
            {
                lbl_total.Text = "0";
                Alert("Sorry there are no data list exist");
                grd_attendance.DataSource = null;
                grd_attendance.DataBind();

            }
            else
            {
                lbl_total.Text = dt.Rows.Count.ToString();
                grd_attendance.DataSource = dt.DefaultView;
                grd_attendance.DataBind();

            }
        }


    }
}