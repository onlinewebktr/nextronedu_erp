using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
namespace school_web.InstructorProfile
{
    public partial class Student_view_Attendance : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
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
                    code.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details where use_mode='1'");
                    ddl_session.SelectedValue = code.get_session_id(code.get_session());

                    string gettypeteacher = code.get_teacher_type(ViewState["teacher"]);
                    ViewState["teachertye"] = gettypeteacher;
                    if (gettypeteacher == "0")
                    {
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by Position asc");

                    }
                    else
                    {
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  Ptm_class_teacher_mapping   where UserID='" + ViewState["teacher"].ToString() + "') and Istatus=1 order by Position asc");

                    }
                    txt_date.Text = code.date();




                }
            }
        }

        public void Alert(string Message)
        {
            lblmessage.Text = Message;

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



                bool subjectchk = avl_subjectornottoday();
                if (subjectchk == true)
                {
                    if (ViewState["teachertye"].ToString() == "0")
                    {
                        code.bind_all_ddl_with_id(ddl_subject, "Select distinct csm.Subject_name, csm.Subject_id from Subject_Master csm join Class_Routine_Master crm  on crm.Subject_id=csm.Subject_id and  crm.Class_id=csm.course_id   where crm.Class_id='" + ddl_class.SelectedValue + "' and crm.Session_id='" + ddl_session.SelectedValue + "' and crm.Section='" + ddl_section.Text + "' and crm.Day='" + day + "' and  csm.Subject_id in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by csm.Subject_name   ");
                    }
                    else
                    {
                        code.bind_all_ddl_with_id(ddl_subject, "Select distinct csm.Subject_name, csm.Subject_id from Subject_Master csm join Class_Routine_Master crm  on crm.Subject_id=csm.Subject_id and  crm.Class_id=csm.course_id  where crm.Class_id='" + ddl_class.SelectedValue + "' and crm.Session_id='" + ddl_session.SelectedValue + "' and crm.Section='" + ddl_section.Text + "' and crm.Day='" + day + "'  order by csm.Subject_name   ");
                    }
                }
                else
                {
                    Alert("Sorry you can't view attendance selected date, because this date is not class");

                }
            }
        }
        private bool avl_subjectornottoday()
        {
            string day = code.getdayname(txt_date.Text);
            string query = "";
            if (ViewState["teachertye"].ToString() == "0")
            {
                query = "Select distinct csm.Subject_name, csm.Subject_id from Subject_Master csm join Class_Routine_Master crm  on crm.Subject_id=csm.Subject_id and  crm.Class_id=csm.course_id  where crm.Class_id='" + ddl_class.SelectedValue + "' and crm.Session_id='" + ddl_session.SelectedValue + "' and crm.Section='" + ddl_section.Text + "' and crm.Day='" + day + "'   and  csm.Subject_id in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by csm.CourseName   ";

            }
            else
            {
                query = "Select distinct csm.Subject_name, csm.Subject_id from Subject_Master csm join Class_Routine_Master crm  on crm.Subject_id=csm.Subject_id and  crm.Class_id=csm.course_id  where csm.Class_id='" + ddl_class.SelectedValue + "' and csm.Session_id='" + ddl_session.SelectedValue + "' and csm.Section='" + ddl_section.Text + "' and crm.Day='" + day + "' order by csm.CourseName   ";
            }
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


        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
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
            else
            {
                string day = code.getdayname(txt_date.Text);
                code.bind_ddl(ddl_period, "Select distinct  Class_period from Class_Routine_Master    where  Class_id='" + ddl_class.SelectedValue + "' and  Session_id='" + ddl_session.SelectedValue + "' and  Section='" + ddl_section.Text + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Day='" + day + "'  order by  Class_period   ");
            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
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



                finally_open_student_list();



            }
        }

        private void finally_open_student_list()
        {//ar.admissionserialnumber in(select Admission_no from Subject_Mapping_New where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "'and Session='" + ddl_session.SelectedItem.Text + "' and Sub_id='" + ddl_subject.SelectedValue + "')
            string query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status from admission_registor ar join Student_Attendance_saved sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no    where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and sas.Class_period='" + ddl_period.Text + "' and sas.Subject_id='" + ddl_subject.SelectedValue + "' and Status=1  order by ar.rollnumber";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                imgexcel2.Visible = false;
                GrdView.DataSource = null;
                GrdView.DataBind();
                grid111.Visible = false;
                lbltotal_student.Text = "0";
                lbl_persenstudent.Text = "0";
                lbl_totalabsentstudent.Text = "0";
                lbl_leave_student.Text = "0";
            }
            else
            {
                imgexcel2.Visible = true;
                lbltotal_student.Text = dt.Rows.Count.ToString();
                GrdView.DataSource = dt;
                GrdView.DataBind();
                grid111.Visible = true;

                bind_student_count();



            }
        }

        private void bind_student_count()
        {
            string query = " Select   sas.Attendance_Status,count(sas.Id) as total  from admission_registor ar join Student_Attendance_saved sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and  ar.Session_id=sas.Session_id  where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and sas.Class_period='" + ddl_period.Text + "' and sas.Subject_id='" + ddl_subject.SelectedValue + "'     group by Attendance_Status";
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