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
    public partial class Student_view_remarks : System.Web.UI.Page
    {
        onlinetest online = new onlinetest();
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
                    online.bind_all_ddl_with_id_new(ddl_session, "Select Sessionname,session_id from Session_Master where Set_Current_Session='1' ");
                }
            }
        }


        string scrpt;
        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select session";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {

                online.bind_all_ddl_with_id_new(ddl_exam_Tracking, "Select Exam_Activity_Name,Exam_Activity_Id from Exam_Activity_Master where Session_id=" + ddl_session.SelectedValue + " ");
            }
        }

        protected void ddl_exam_Tracking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select session";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (ddl_exam_Tracking.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select tracking head for exam";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {
                online.bind_all_ddl_with_id_new(ddl_class, "Select distinct cm.CategoryName, cm.CategoryID,cm.Position from Add_course_table cm join  Session_Exam_Class_Mapping secm on cm.course_id=secm.Class_Id where cm.course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  and secm.Session_Id='" + ddl_session.SelectedValue + "'  order by cm.Position asc");
            }
        }


        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select session";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select class";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {
                online.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_class.SelectedValue + "'   and   CourseID in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by section");

            }
        }

        #region find
        protected void btn_find_Click1(object sender, EventArgs e)
        {

        
            if (ddl_session.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select session";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }

            else if (ddl_class.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select class";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (ddl_section.Text == "Text")
            {
                lblmessage.Text = "Please select section";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {
                get_data();
            }
        }

        private void get_data()
        {
            string query = "Select ar.FullName,ar.roll_no,ar.reg_id,sre.* from UserRegistrationMaster ar join Student_Remarks_Entry sre on ar.reg_id=sre.Admission_No where sre.Class_Id='" + ddl_class.SelectedValue + "' and sre.Section='" + ddl_section.Text + "' and sre.Session_id='" + ddl_session.SelectedValue + "'  and sre.Exam_Activity_Id='" + ddl_exam_Tracking.SelectedValue + "' and  ar.status=1    order by ar.roll_no";
         
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = onlinetest.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {
                GrdView.DataSource = null;
                GrdView.DataBind();
                grid111.Visible = false;
                imgexcel2.Visible = false;
            }
            else
            {
                GrdView.DataSource = dtTemp;
                GrdView.DataBind();
                grid111.Visible = true;
                imgexcel2.Visible = true;
            }
        }
        #endregion
        UsesCode mycode = new UsesCode();
        protected void imgexcel2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Student_Remarks_" + mycode.date() + "_" + ".xls");
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