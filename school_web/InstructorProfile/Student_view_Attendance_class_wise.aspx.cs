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
    public partial class Student_view_Attendance_class_wise : System.Web.UI.Page
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
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  Ptm_class_teacher_mapping   where UserID='" + ViewState["teacher"].ToString() + "')     order by Position asc");

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
                string get_sectionty = get_section_type();
                if (get_sectionty == "ALL")
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'  order by Section");
                }
                else
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from Ptm_class_teacher_mapping where CategoryID ='" + ddl_class.SelectedValue + "' and Section!='ALL'    order by Section");
                }
            }
        }

        private string get_section_type()
        {
            string query = "Select * from Ptm_class_teacher_mapping where CategoryID='" + ddl_class.SelectedValue + "' and Session_Id='" + ddl_session.SelectedValue + "' and Section='ALL'";

            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                return "A";

            }
            else
            {
                return "ALL";
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
        {
            string query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "' and ar.status='1'   order by ar.rollnumber";
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
            string query = " Select   sas.Attendance_Status,count(sas.Id) as total  from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and  ar.Session_id=sas.Session_id  where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and sas.Attendance_Date='" + txt_date.Text+"'  group by Attendance_Status";
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

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Attendance_Status = (Label)e.Row.FindControl("lbl_Attendance_Status");
                if (lbl_Attendance_Status.Text == "Present")
                {
                    lbl_Attendance_Status.Style.Add("background", "#009f25");
                    lbl_Attendance_Status.Style.Add("padding", "3px 5px 3px 5px");
                    lbl_Attendance_Status.Style.Add("color", "#fff8f8");
                    lbl_Attendance_Status.Style.Add("width", "69px");
                    lbl_Attendance_Status.Style.Add("float", "left");
                    lbl_Attendance_Status.Style.Add("text-align", "center");



                }
                else if (lbl_Attendance_Status.Text == "Absent")
                {
                    lbl_Attendance_Status.Style.Add("background", "#f00");
                    lbl_Attendance_Status.Style.Add("padding", "3px 5px 3px 5px");
                    lbl_Attendance_Status.Style.Add("color", "#fff8f8");
                    lbl_Attendance_Status.Style.Add("width", "69px");
                    lbl_Attendance_Status.Style.Add("float", "left");
                    lbl_Attendance_Status.Style.Add("text-align", "center");

                }
                else
                {
                    lbl_Attendance_Status.Style.Add("background", "#ff6a00");
                    lbl_Attendance_Status.Style.Add("padding", "3px 5px 3px 5px");
                    lbl_Attendance_Status.Style.Add("color", "#fff8f8");
                    lbl_Attendance_Status.Style.Add("width", "69px");
                    lbl_Attendance_Status.Style.Add("float", "left");
                    lbl_Attendance_Status.Style.Add("text-align", "center");

                }
            }
        }
    }
}