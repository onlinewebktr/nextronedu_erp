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
    public partial class View_Attendance_Subject_Wise : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["teacher"] = Request.QueryString["regid"].ToString();
                    try
                    {
                        ViewState["Is_roll_no_class_attendance"] = My.get_Is_roll_no_class_attendance();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["teacher"].ToString());
                        code.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details where Use_mode='1'");
                        ddl_session.SelectedValue = code.get_session_id(code.get_session());


                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "')  order by Position asc");
                        txt_date.Text = code.date();


                    }
                    catch
                    {
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
                code.bind_ddl(ddl_section, "Select distinct section   from TeacherCourseSubjectMaping  where CategoryID ='" + ddl_class.SelectedValue + "' and Istatus='1' and  UserID='" + ViewState["teacher"].ToString() + "'  order by section");
            }
        }
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.SelectedItem.Text == "Text")
            {
                Alert("Please select section");
            }
            else
            {
                string day = code.getdayname(txt_date.Text);
                code.bind_all_ddl_with_id(ddl_period, "Select distinct  crp.Period_Name,crm.Class_period  from Class_Routine_period crp join Class_Routine_Master crm on  crm.Class_period=crp.Period and crm.Session_id=crp.Session_id and crm.Class_id=crp.course_id       where  crm.Class_id='" + ddl_class.SelectedValue + "' and  crm.Session_id='" + ddl_session.SelectedValue + "' and  crm.Section='" + ddl_section.Text + "' and crm.Day='" + day + "' and crp.Branch_id='" + ViewState["branchid"].ToString() + "'  order by  crm.Class_period   ");
            }
        }

        private bool avl_subjectornottoday()
        {
            string day = code.getdayname(txt_date.Text);
            string query = "Select    csm.Subject_id from Subject_Master csm join Class_Routine_Master crm  on crm.Subject_id=csm.Subject_id and  crm.Class_id=csm.course_id  where csm.course_id='" + ddl_class.SelectedValue + "' and crm.Session_id='" + ddl_session.SelectedValue + "' and crm.Section='" + ddl_section.Text + "' and crm.Day='" + day + "'  and crm.Class_period='" + ddl_period.SelectedValue + "'  and  csm.Subject_id in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')     ";
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {

                return false;
            }
            else
            {
                return true;

            }

        }

        protected void ddl_period_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.SelectedItem.Text == "Text")
            {
                Alert("Please select section");
            }

            else if (txt_date.Text == "")
            {
                Alert("Please select date");
            }
            else if (ddl_period.SelectedItem.Text == "Select")
            {
                Alert("Please select period");
            }
            else
            {
                string day = code.getdayname(txt_date.Text);
                bool subjectchk = avl_subjectornottoday();
                if (subjectchk == true)
                {
                    code.bind_all_ddl_with_id(ddl_subject, "Select distinct csm.Subject_name, csm.Subject_id,csm.Subject_position from Subject_Master csm join Class_Routine_Master crm  on crm.Subject_id=csm.Subject_id and  crm.Class_id=csm.course_id   where csm.course_id='" + ddl_class.SelectedValue + "' and crm.Session_id='" + ddl_session.SelectedValue + "' and crm.Class_period='" + ddl_period.SelectedValue + "' and crm.Section='" + ddl_section.Text + "' and crm.Day='" + day + "' and  csm.Subject_id in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by csm.Subject_position   ");
                }
                else
                {
                    Alert("Sorry you can't make attendance your selection date, because this date  is not class scheduled");

                }

            }
        }
        private bool avl_subjectornottodayfind()
        {
            string day = code.getdayname(txt_date.Text);
            string query = "Select     csm.course_id from Subject_Master csm join Class_Routine_Master crm  on crm.Subject_id=csm.Subject_id and  crm.Class_id=csm.course_id  where   csm.course_id='" + ddl_class.SelectedValue + "' and crm.Session_id='" + ddl_session.SelectedValue + "' and crm.Section='" + ddl_section.Text + "' and Day='" + day + "' and crm.Subject_id='" + ddl_subject.SelectedValue + "' and  crm.Class_period='" + ddl_period.Text + "' and  csm.Subject_id in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')    ";

            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {

                return false;
            }
            else
            {
                return true;
            }
        }
        private string holiday_or_leave()
        {
            string query = "Select Day,Type from School_Holiday_Calendar where Date='" + txt_date.Text + "'";

            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {

                return "";
            }
            else
            {
                return dt.Rows[0]["Type"].ToString();
            }


        }




        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alert("Please select session");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");
                }
                else if (ddl_section.SelectedItem.Text == "Text")
                {
                    Alert("Please select section");
                }
                else if (ddl_subject.SelectedItem.Text == "Text")
                {
                    Alert("Please select subject");
                }
                else if (txt_date.Text == "")
                {
                    Alert("Please select date");
                }
                else
                {
                    string query = "";
                    if (ViewState["Is_roll_no_class_attendance"].ToString() == "1")// roll no wise 
                    {
                        query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section,(select top 1 CategoryName from ClassMaster where CategoryID=ar.Class_id) as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status from admission_registor ar join Student_Attendance_saved sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no    where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and sas.Class_period='" + ddl_period.Text + "' and sas.Subject_id='" + ddl_subject.SelectedValue + "' and Status=1  order by ar.rollnumber asc";
                    }

                    else
                    {
                        query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section,(select top 1 CategoryName from ClassMaster where CategoryID=ar.Class_id) as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status from admission_registor ar join Student_Attendance_saved sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no    where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and sas.Class_period='" + ddl_period.Text + "' and sas.Subject_id='" + ddl_subject.SelectedValue + "' and Status=1  order by ar.studentname asc";
                    }



                    SqlCommand cmd = new SqlCommand(query);
                    DataTable dt = code.FillTable(query);
                    if (dt.Rows.Count == 0)
                    {
                        // imgexcel2.Visible = false;
                        GrdView.DataSource = null;
                        GrdView.DataBind();
                        grid111.Visible = false;
                        lbltotal_student.Text = "0";
                        lbl_persenstudent.Text = "0";
                        lbl_totalabsentstudent.Text = "0";
                        lbl_leave_student.Text = "0";
                        Alert("Attendance not found.");
                    }
                    else
                    {
                        //imgexcel2.Visible = true;
                        lbltotal_student.Text = dt.Rows.Count.ToString();
                        GrdView.DataSource = dt;
                        GrdView.DataBind();
                        grid111.Visible = true;

                        bind_student_count();



                    }

                }
            }
            catch
            {

            }
        }

        private void bind_student_count()
        {
            string query = " Select   sas.Attendance_Status,count(sas.Id) as total  from admission_registor ar join Student_Attendance_saved sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no    where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and sas.Class_period='" + ddl_period.Text + "' and sas.Subject_id='" + ddl_subject.SelectedValue + "' and ar.Status='1' group by Attendance_Status";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Attendance_Status"].ToString() == "Present")
                    {
                        lbl_persenstudent.Text = dr["total"].ToString();
                    }
                    else if (dr["Attendance_Status"].ToString() == "Absent")
                    {
                        lbl_totalabsentstudent.Text = dr["total"].ToString();
                    }
                    else
                    {
                        lbl_leave_student.Text = dr["total"].ToString();
                    }
                }
            }
        }

        protected void imgexcel2_Click(object sender, ImageClickEventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=AttendanceExport" + "_" + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GrdView.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}