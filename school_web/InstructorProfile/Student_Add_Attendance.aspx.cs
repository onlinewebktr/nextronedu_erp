using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using school_web.AppCode;
using System.Data;
namespace school_web.InstructorProfile
{
    public partial class Student_Add_Attendance : System.Web.UI.Page
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


                    code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by Position asc");
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
                code.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_class.SelectedValue + "'   and   CourseID in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by section");
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
                    code.bind_all_ddl_with_id(ddl_subject, "Select distinct csm.CourseName, csm.CourseID from Add_course_table csm join Class_Routine_Master crm  on crm.Subject_id=csm.Subject_id and  crm.Class_id=csm.course_id and    where csm.CategoryID='" + ddl_class.SelectedValue + "' and csm.session_id='" + ddl_session.SelectedValue + "' and csm.section='" + ddl_section.Text + "' and Day='" + day + "' and  csm.CourseID in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "' and Session_id='"+ddl_session.SelectedValue+"') order by csm.CourseName   ");
                }
                else
                {
                    Alert("Sorry you can't make attendance your selection date, because this date  is not class scheduled");

                }
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
        private bool avl_subjectornottoday()
        {
            string day = code.getdayname(txt_date.Text);
            string query = "Select   csm.CourseName, csm.CourseID from Subject_Master csm join Class_Routine_Master crm  on crm.Subject_id=csm.Subject_id and  crm.Class_id=csm.course_id   where csm.CategoryID='" + ddl_class.SelectedValue + "' and csm.session_id='" + ddl_session.SelectedValue + "' and csm.section='" + ddl_section.Text + "' and Day='" + day + "'   and  csm.CourseID in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "') order by csm.CourseName   ";

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
                string chek = holiday_or_leave();
                if (chek == "")
                {
                    Alert("Sorry holiday calendar is not updated");
                }

                else if (chek.ToUpper() == "H")
                {
                    Alert("Sorry you can't make attendance , because selected date is holiday");
                }
                else if (chek.ToUpper() == "NA")
                {
                    Alert("Sorry you can't make attendance , because selected date is not class");
                }
                else if (chek.ToUpper() == "NO CLASS")
                {
                    Alert("Sorry you can't make attendance, because selected date is not class");
                }
                else
                {
                    if (chek.ToUpper() == "CLASS")
                    {
                        bool subjectchk = avl_subjectornottodayfind();
                        if (subjectchk == true)
                        {
                            finally_open_student_list();
                        }
                        else
                        {
                            Alert("Sorry you can't make attendance , because selected date is not class");

                        }
                    }
                    else if (chek.ToUpper() == "CL")
                    {
                        bool subjectchk = avl_subjectornottodayfind();
                        if (subjectchk == true)
                        {
                            finally_open_student_list();
                        }
                        else
                        {
                            Alert("Sorry you can't make attendance , because selected date is not class");

                        }
                    }

                        
                    else
                    {
                        Alert("Sorry you can't make attendance , because selected date is not class");
                    }
                }

            }
        }

        private bool avl_subjectornottodayfind()
        {
            string day = code.getdayname(txt_date.Text);
            string query = "Select   csm.CourseName, csm.CourseID from Course_or_Subject_Master csm join Class_Routine_Master crm  on crm.Subject_id=csm.Subject_id and  crm.Class_id=csm.course_id    where csm.CategoryID='" + ddl_class.SelectedValue + "' and csm.session_id='" + ddl_session.SelectedValue + "' and csm.section='" + ddl_section.Text + "' and Day='" + day + "' and crm.Subject_id='" + ddl_subject.SelectedValue + "' and  crm.Class_period='" + ddl_period.Text + "' and  csm.CourseID in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "') order by csm.CourseName   ";

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


        private void finally_open_student_list()
        {


            string query = "Select admissionserialnumber,rollnumber,Section,class  as classname,studentname from admission_registor where Class_id='" + ddl_class.SelectedValue + "' and section='" + ddl_section.Text + "' and session='" + ddl_session.SelectedItem.Text + "' and admissionserialnumber in(select Admission_no from Subject_Mapping_New where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "'and Session='" + ddl_session.SelectedItem.Text + "' and Sub_id='" + ddl_subject.SelectedValue + "') and Status=1 order by rollnumber";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                GrdView.DataSource = null;
                GrdView.DataBind();
                grid111.Visible = false;

            }
            else
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
                grid111.Visible = true;

            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Bind_option1((RadioButtonList)e.Row.FindControl("RadioButtonList1"));

                Label lbl_reg_id = (Label)e.Row.FindControl("lbl_reg_id");
                RadioButtonList rb = (RadioButtonList)e.Row.FindControl("RadioButtonList1");

                bind_if_answerd(rb, lbl_reg_id.Text);

            }
        }

        private void bind_if_answerd(RadioButtonList rb, string admission)
        {
            string day = code.getdayname(txt_date.Text);
            string query = "Select * from Student_Attendance_saved where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Admission_no='" + admission + "' and Class_period='" + ddl_period.Text + "' and Attendance_Date='" + txt_date.Text + "'  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {

                if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                {
                    rb.SelectedValue = "1";
                }
                else if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                {
                    rb.SelectedValue = "2";
                }
                else if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                {
                    rb.SelectedValue = "3";
                }

            }
        }

        private void Bind_option1(RadioButtonList radioButtonList)
        {
            String strConnString = connection.conn;

            using (SqlConnection con = new SqlConnection(strConnString))
            {
                //string query = "SELECT Option_EN, Option_Id FROM Quiz_id where  Quiz_id='" + questionid + "' and Training_id='" + ViewState["Training_id"].ToString() + "' and Training_sub_id='" + ViewState["Training_sub_id"].ToString() + "'";
                string query = @"select Typeid,Type from Type_of_Attendance";


                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    radioButtonList.DataSource = cmd.ExecuteReader();
                    radioButtonList.DataTextField = "Type";
                    radioButtonList.DataValueField = "Typeid";
                    radioButtonList.DataBind();

                    //radioButtonList.SelectedItem.Value = "1";
                    con.Close();
                }
            }
        }
        protected void RadioButtonList1_DataBound(object sender, EventArgs e)
        {
            RadioButtonList list = (RadioButtonList)sender;

        }

        protected void btn_save_all_Click(object sender, EventArgs e)
        {
            string student_attendance = "Absent";
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (txt_date.Text == "")
            {
                Alert("Please select date");
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
            else if (ddl_period.Text == "Text")
            {
                Alert("Please select class period");
            }
            else
            {
                int growcount = GrdView.Rows.Count;
                int k = 0;

                for (int i = 0; i < growcount; i++)
                {
                    RadioButtonList RadioButtonList1 = (RadioButtonList)GrdView.Rows[i].FindControl("RadioButtonList1") as RadioButtonList;

                    if (RadioButtonList1.SelectedItem.Text == "Present")
                    {
                        student_attendance = "Present";
                    }

                    else if (RadioButtonList1.SelectedItem.Text == "Absent")
                    {
                        student_attendance = "Absent";
                    }

                    else if (RadioButtonList1.SelectedItem.Text == "Leave")
                    {
                        student_attendance = "Leave";
                    }
                    Label lbl_reg_id = (Label)GrdView.Rows[i].FindControl("lbl_reg_id");
                    finalattendance(student_attendance, lbl_reg_id.Text);

                    k++;

                }


                Alert("Successfully attendance saved");
                finally_open_student_list();




            }

        }

        private void finalattendance(string student_attendance, string admission)
        {
            string day = code.getdayname(txt_date.Text);
            string query = "Select * from Student_Attendance_saved where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Admission_no='" + admission + "' and Class_period='" + ddl_period.Text + "' and Attendance_Date='" + txt_date.Text + "'  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {

                string strQuery = "INSERT INTO Student_Attendance_saved (Session_id,Class_id,Section,Subject_id,Class_period,Day,Attendance_Date,Attendance_IDate,Date,Idate,time,Created_By,Attendance_Status,Admission_no) values (@Session_id,@Class_id,@Section,@Subject_id,@Class_period,@Day,@Attendance_Date,@Attendance_IDate,@Date,@Idate,@time,@Created_By,@Attendance_Status,@Admission_no)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_period", ddl_period.Text);
                cmd.Parameters.AddWithValue("@Day", day);
                cmd.Parameters.AddWithValue("@Attendance_Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Attendance_IDate", code.ConvertStringToiDate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Date", code.date());
                cmd.Parameters.AddWithValue("@Idate", code.idate());
                cmd.Parameters.AddWithValue("@time", code.time());
                cmd.Parameters.AddWithValue("@Created_By", ViewState["teacher"].ToString());
                cmd.Parameters.AddWithValue("@Attendance_Status", student_attendance);
                cmd.Parameters.AddWithValue("@Admission_no", admission);
                if (InsertUpdate.InsertUpdateData(cmd))
                {

                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string strQuery = "Update Student_Attendance_saved set    Updated_Date=@Updated_Date,Updated_IDate=@Updated_IDate,Updated_Time=@Updated_Time,Updated_By=@Updated_By,Attendance_Status=@Attendance_Status   where Id = @Id";
                SqlCommand cmd1 = new SqlCommand(strQuery);

                cmd1.Parameters.AddWithValue("@Updated_Date", code.date());
                cmd1.Parameters.AddWithValue("@Updated_IDate", code.idate());
                cmd1.Parameters.AddWithValue("@Updated_Time", code.time());
                cmd1.Parameters.AddWithValue("@Updated_By", ViewState["teacher"].ToString());
                cmd1.Parameters.AddWithValue("@Attendance_Status", student_attendance);
                cmd1.Parameters.AddWithValue("@Id", id);
                if (InsertUpdate.InsertUpdateData(cmd1))
                {

                }
            }
        }





    }
}