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
    public partial class Student_Add_Remarks : System.Web.UI.Page
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

                online.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_class.SelectedValue + "' and Session_id='" + ddl_session.SelectedValue + "'   and   CourseID in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by section");

               
            }
        }

        #region find
        protected void btn_find_Click(object sender, EventArgs e)
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
            string query = "Select FullName,roll_no,reg_id from UserRegistrationMaster where Class_id='" + ddl_class.SelectedValue + "' and section='" + ddl_section.Text + "' and session='" + ddl_session.SelectedItem.Text + "' and status=1 order by roll_no";
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



        #region row databound

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_reg_id = (Label)e.Row.FindControl("lbl_reg_id");
                TextBox txt_Remarks = (TextBox)e.Row.FindControl("txt_Remarks");



                Bind_data_if_exist(lbl_reg_id.Text, txt_Remarks);



                




                 

            }
        }

        private void Bind_data_if_exist(string reg_id, TextBox txt_Remarks)
        {
            string query = "Select Remarks from Student_Remarks_Entry where Session_id=" + ddl_session.SelectedValue + "  and Exam_Activity_Id=" + ddl_exam_Tracking.SelectedValue + "  and Admission_No='" + reg_id + "' and Section='"+ddl_section.Text+"'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = onlinetest.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {
                txt_Remarks.Text = "";

            }
            else
            {
                txt_Remarks.Text = dtTemp.Rows[0]["Remarks"].ToString();

            }
        }

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
        #endregion

        #region save all
        protected void btn_save_all_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select session";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (ddl_exam_Tracking.Text == "")
            {
                lblmessage.Text = "Please select tracking";
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
               
                int growcount = GrdView.Rows.Count;
                int k = 0;

                for (int i = 0; i < growcount; i++)
                {
                    Label lbl_reg_id = (Label)GrdView.Rows[i].FindControl("lbl_reg_id");
                    TextBox txt_Remarks = (TextBox)GrdView.Rows[i].FindControl("txt_Remarks");
                    if (txt_Remarks.Text == "")
                    {
                    }
                    else
                    {
                        save_data(lbl_reg_id.Text, txt_Remarks.Text);
                    }
                }

                lblmessage.Text = "Remarks has been sucessfully save";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                get_data();

            }
        }

        private void save_data(string reg_id, string Remarks)
        {

            SqlCommand cmd = new SqlCommand("Select * from Student_Remarks_Entry where  Session_id=" + ddl_session.SelectedValue + " and Exam_Activity_Id=" + ddl_exam_Tracking.Text + " and Admission_No='" + reg_id + "'   and Section='" + ddl_section.Text + "'");
            DataTable dt = onlinetest.GetData(cmd);
            if (dt.Rows.Count == 0)
            {

             
                SqlCommand cmd1;
                string strQuery = @"INSERT INTO Student_Remarks_Entry (Exam_Activity_Id,Session_id,Admission_No,Class_Id,Section,Remarks,Created_By,Created_Date,Created_Idate,Created_Time) values (@Exam_Activity_Id,@Session_id,@Admission_No,@Class_Id,@Section,@Remarks,@Created_By,@Created_Date,@Created_Idate,@Created_Time)";
                cmd1 = new SqlCommand(strQuery);
                cmd1.Parameters.AddWithValue("@Exam_Activity_Id", ddl_exam_Tracking.SelectedValue);
                cmd1.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd1.Parameters.AddWithValue("@Admission_No", reg_id);
                cmd1.Parameters.AddWithValue("@Class_Id", ddl_class.SelectedValue);
                cmd1.Parameters.AddWithValue("@Section", ddl_section.Text);
                cmd1.Parameters.AddWithValue("@Remarks", Remarks);
                cmd1.Parameters.AddWithValue("@Created_By", ViewState["teacher"].ToString());
                cmd1.Parameters.AddWithValue("@Created_Date", mycode.date());
                cmd1.Parameters.AddWithValue("@Created_Idate", mycode.idate());
                cmd1.Parameters.AddWithValue("@Created_Time", mycode.time());
                if (onlinetest.InsertUpdateData(cmd1))
                {
                }
            }
            else
            {
                string Id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd1;
                string strQuery = @"Update Student_Remarks_Entry set Exam_Activity_Id=@Exam_Activity_Id,Session_id=@Session_id,Admission_No=@Admission_No,Class_Id=@Class_Id,Section=@Section,Remarks=@Remarks,Created_Date=@Created_Date,Created_Idate=@Created_Idate,Created_Time=@Created_Time where Id = @Id";
                cmd1 = new SqlCommand(strQuery);
                cmd1.Parameters.AddWithValue("@Exam_Activity_Id", ddl_exam_Tracking.SelectedValue);
                cmd1.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd1.Parameters.AddWithValue("@Admission_No", reg_id);
                cmd1.Parameters.AddWithValue("@Class_Id", ddl_class.SelectedValue);
                cmd1.Parameters.AddWithValue("@Section", ddl_section.Text);
                cmd1.Parameters.AddWithValue("@Remarks", Remarks);
                cmd1.Parameters.AddWithValue("@Created_By", ViewState["teacher"].ToString());
                cmd1.Parameters.AddWithValue("@Created_Date", mycode.date());
                cmd1.Parameters.AddWithValue("@Created_Idate", mycode.idate());
                cmd1.Parameters.AddWithValue("@Created_Time", mycode.time());
                cmd1.Parameters.AddWithValue("@Id", Id);
                if (onlinetest.InsertUpdateData(cmd1))
                {
                }
            }
        }

        #endregion
    }
}