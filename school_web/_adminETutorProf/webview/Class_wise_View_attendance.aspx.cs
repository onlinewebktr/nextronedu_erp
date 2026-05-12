using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class Class_wise_View_attendance : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["regid"] != null)
            {
                ViewState["teacher"] = Request.QueryString["regid"].ToString();
                if (!IsPostBack)
                {
                    ViewState["Is_roll_no_class_attendance"] = My.get_Is_roll_no_class_attendance();
                    code.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details where use_mode='1'");
                    ddl_session.SelectedValue = code.get_session_id(code.get_session());
                    ViewState["sessionid"] = My.get_session_id();
                    string gettypeteacher = code.get_teacher_type(ViewState["teacher"]);
                    ViewState["teachertye"] = gettypeteacher;
                    if (gettypeteacher == "0")
                    {
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "')  order by Position asc");

                    }
                    else
                    {
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  Ptm_class_teacher_mapping   where UserID='" + ViewState["teacher"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "')     order by Position asc");

                    }
                    txt_date.Text = code.date(); 
                }
            }

        } 

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                lbl_message.Text = "Please select class"; 
            }
            else
            {
                string get_sectionty = get_section_type();
                if (get_sectionty.ToUpper() == "ALL")
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'  and Session_id='" + ddl_session.SelectedValue + "' order by Section");
                }
                else
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from Ptm_class_teacher_mapping where CategoryID ='" + ddl_class.SelectedValue + "' and Section!='ALL'  and UserID='" + ViewState["teacher"].ToString() + "'  order by Section");
                }
            }
        }

        private string get_section_type()
        {
            string valueS = "A";
            string query = "Select Section from Ptm_class_teacher_mapping where CategoryID='" + ddl_class.SelectedValue + "' and Session_Id='" + ddl_session.SelectedValue + "' and UserID='" + ViewState["teacher"].ToString() + "'";
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count > 0)
            {
                valueS = dt.Rows[0][0].ToString();
            }
            return valueS;
        }






        protected void btn_find_Click(object sender, EventArgs e)
        {
            lbl_message.Text = "";
            if (ddl_session.SelectedItem.Text == "Select")
            {
                lbl_message.Text = "Please select session"; 
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                lbl_message.Text = "Please select class"; 
            }
            else if (ddl_section.SelectedItem.Text == "Text")
            {
                lbl_message.Text = "Please select section"; 
            } 
            else if (txt_date.Text == "")
            {
                lbl_message.Text = "Please select date"; 
            }
            else
            { 
                finally_open_student_list(); 
            }
        }

        private void finally_open_student_list()
        {
            string query = "";
            if (ViewState["Is_roll_no_class_attendance"].ToString() == "1")// roll no wise 
            {
                query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "'    order by ar.rollnumber asc";
            }
            else
            {
                query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "'    order by ar.studentname asc";
            }
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                lbl_message.Text = "Sorry there are no attendance list found."; 
                // imgexcel2.Visible = false;
                rd_view.DataSource = null;
                rd_view.DataBind();
                // grid111.Visible = false;
                totalStudents.InnerText = "0";
                presentStudents.InnerText = "0";
                absentStudents.InnerText = "0";
                leaveStudents.InnerText = "0";
            }
            else
            {
                //imgexcel2.Visible = true;
                totalStudents.InnerText = dt.Rows.Count.ToString();
                rd_view.DataSource = dt;
                rd_view.DataBind();
                // grid111.Visible = true;

                bind_student_count();
            }
        }

        private void bind_student_count()
        {
            presentStudents.InnerText = "0";
            absentStudents.InnerText = "0";
            leaveStudents.InnerText = "0";
            string query = " Select   sas.Attendance_Status,count(sas.Id) as total  from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and  ar.Session_id=sas.Session_id  where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and   sas.Attendance_Date='" + txt_date.Text + "'    group by Attendance_Status";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Attendance_Status"].ToString() == "Present")
                    {
                        presentStudents.InnerText = dr["total"].ToString();
                    }
                    else if (dr["Attendance_Status"].ToString() == "Absent")
                    {
                        absentStudents.InnerText = dr["total"].ToString();
                    }
                    else
                    {
                        leaveStudents.InnerText = dr["total"].ToString();
                    }
                }
            }
        }
         



        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_Attendance_Status = (Label)e.Item.FindControl("lbl_Attendance_Status");
                if (lbl_Attendance_Status.Text == "Present")
                {
                    lbl_Attendance_Status.CssClass += "status present";
                }
                else if (lbl_Attendance_Status.Text == "Absent")
                {
                    lbl_Attendance_Status.CssClass += "status absent";
                }
                else
                {
                    lbl_Attendance_Status.CssClass += "status leave";
                }
            }
        }
    }
}