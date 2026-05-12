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

namespace school_web.LMS_VC_Admin
{
    public partial class vc_class_summary : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindCourse();
                    DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    string date = dtm.ToString("dd/MM/yyyy");
                    string day = date.Substring(0, 2);
                    string month = date.Substring(3, 2);
                    string year = date.Substring(6, 4);
                    ddl_year.Text = year;
                    ddl_date.Text = day;
                    ddl_month.Text = dtm.ToString("MMM");

                    rd_day.Checked = true;
                    rd_month.Checked = false;
                    rd_year.Checked = false;
                    date1.Visible = true;
                    month1.Visible = true;
                    year1.Visible = true;
                    find_data();
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        private void BindCourse()
        {
            code.bind_all_ddl_with_id(ddl_teacher, "Select Name, Upper(UserID) as UserID from InstructorProfile order by Name");
            code.bind_all_ddl_with_id(ddl_class, "Select CategoryName, CategoryID from ClassMaster order by Position asc");
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alert("Select class");
                }
                else
                {

                    code.bind_ddl(ddl_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_class.SelectedValue + "' and Istatus='1' order by section");
                }

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Select class");
            }
            else if (ddl_section.Text == "")
            {
                Alert("Select section");
            }
            else
            {
                code.bind_all_ddl_with_id(ddl_subject, "Select CourseName, CourseID from Course_or_Subject_Master where CategoryID ='" + ddl_class.SelectedValue + "' and section='" + ddl_section.Text + "' and Istatus='1' order by CourseName");
            }
        }

        protected void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                find_data();


            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void find_data()
        {
            try
            {

                if (rd_day.Checked == false && rd_month.Checked == false && rd_year.Checked == false)
                {
                    Alert("Please select day, month or year");
                    pnl_view.Visible = false;
                    RpDetailsStudent.DataSource = null;
                    RpDetailsStudent.DataBind();
                    grd_taken_class.DataSource = null;
                    grd_taken_class.DataBind();
                }
                else
                {
                    string date = ddl_year.Text;
                    if (rd_day.Checked == true)
                    {
                        date = ddl_date.Text + "-" + ddl_month.Text + "-" + ddl_year.Text;
                    }
                    else if (rd_month.Checked == true)
                    {
                        date = ddl_month.Text + "-" + ddl_year.Text;
                    }
                    else if (rd_year.Checked == true)
                    {
                        date = ddl_year.Text;
                    }


                    string qry = " select Teacher_Id,Name,count(t1.id) as total_class from dbo.[Zoom_Virtual_class_schedule] t1 join InstructorProfile t2 on t1.Teacher_Id=t2.UserID  where   Date like'%" + date + "%' group by Teacher_Id,Name";


                    SqlCommand cmd = new SqlCommand(qry);
                    DataTable dt = InsertUpdate.GetData(cmd);
                    int rowcount = dt.Rows.Count;
                    if (rowcount == 0)
                    {

                        Alert("Data Not Available");
                        grd_taken_class.DataSource = null;
                        grd_taken_class.DataBind();
                        RpDetailsStudent.DataSource = null;
                        RpDetailsStudent.DataBind();

                        pnl_view.Visible = false;
                        lbnk_summeryteacherclass.Visible = false;
                    }
                    else
                    {
                        lbnk_summeryteacherclass.Visible = true;
                        pnl_view.Visible = false;
                        grd_taken_class.DataSource = dt;
                        grd_taken_class.DataBind();

                        RpDetailsStudent.DataSource = null;
                        RpDetailsStudent.DataBind();
                    }



                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        protected void lnk_view_details_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                string teacher_id = ((Label)row.FindControl("lbl_teacherid")).Text;
                ddl_teacher.SelectedValue = teacher_id.ToUpper();

                // lbl_month_year.Text = ddl_month.Text + "-" + ddl_year.Text;


                if (rd_day.Checked == true)
                {
                    lbl_month_year.Text = ddl_date.Text + "-" + ddl_month.Text + "-" + ddl_year.Text;
                }
                else if (rd_month.Checked == true)
                {
                    lbl_month_year.Text = ddl_month.Text + "-" + ddl_year.Text;
                }
                else if (rd_year.Checked == true)
                {
                    lbl_month_year.Text = ddl_year.Text;
                }





                find_teacher_wise_class_taken(1);
                pnltop.Visible = false;
                pnl_view.Visible = true;
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void find_teacher_wise_class_taken(int p)
        {
            string date = "";
            if (p == 1)
            {
               
                if (rd_day.Checked == true)
                {
                    date = ddl_date.Text + "-" + ddl_month.Text + "-" + ddl_year.Text;
                }
                else if (rd_month.Checked == true)
                {
                    date = ddl_month.Text + "-" + ddl_year.Text;
                }
                else if (rd_year.Checked == true)
                {
                    date = ddl_year.Text;
                }
                string qry = " select t4.section,Teacher_Id,Name,t3.CategoryName,t4.CourseName,t1.Date,t1.Start_Time,t1.Meeting_start_at,t1.CreatedOn,t1.End_Time,T1.Zoom_id from dbo.[Zoom_Virtual_class_schedule] t1 join InstructorProfile t2 on t1.Teacher_Id=t2.UserID join ClassMaster t3 on t1.Class=t3.CategoryID join Course_or_Subject_Master t4 on t1.Class=t4.CategoryID and t1.Subject =t4.CourseID and t1.section=t4.section  where  Teacher_Id='" + ddl_teacher.SelectedValue + "' and t1.Date like'%" + date + "%'";
                code.BindRepeater(qry, RpDetailsStudent);
            }
            if (p == 2)
            {
                if (rd_day.Checked == true)
                {
                    date = ddl_date.Text + "-" + ddl_month.Text + "-" + ddl_year.Text;
                }
                else if (rd_month.Checked == true)
                {
                    date = ddl_month.Text + "-" + ddl_year.Text;
                }
                else if (rd_year.Checked == true)
                {
                    date = ddl_year.Text;
                }

                string qry = "  select t4.section,Teacher_Id,Name,t3.CategoryName,t4.CourseName,t1.Date,t1.Start_Time,t1.Meeting_start_at,t1.CreatedOn,t1.End_Time,T1.Zoom_id from dbo.[Zoom_Virtual_class_schedule] t1 join InstructorProfile t2 on t1.Teacher_Id=t2.UserID join ClassMaster t3 on t1.Class=t3.CategoryID join Course_or_Subject_Master t4 on t1.Class=t4.CategoryID and t1.Subject =t4.CourseID and t1.section=t4.section where  Teacher_Id='" + ddl_teacher.SelectedValue + "'  and t1.Class='" + ddl_class.SelectedValue + "'  and t4.section='"+ddl_section.Text+"' and t1.Date like'%" + date + "%'";
                code.BindRepeater(qry, RpDetailsStudent);
            }
            if (p == 3)
            {
                if (rd_day.Checked == true)
                {
                    date = ddl_date.Text + "-" + ddl_month.Text + "-" + ddl_year.Text;
                }
                else if (rd_month.Checked == true)
                {
                    date = ddl_month.Text + "-" + ddl_year.Text;
                }
                else if (rd_year.Checked == true)
                {
                    date = ddl_year.Text;
                }
                string qry = "  select t4.section,Teacher_Id,Name,t3.CategoryName,t4.CourseName,t1.Date,t1.Start_Time,t1.Meeting_start_at,t1.CreatedOn,t1.End_Time,T1.Zoom_id from dbo.[Zoom_Virtual_class_schedule] t1 join InstructorProfile t2 on t1.Teacher_Id=t2.UserID join ClassMaster t3 on t1.Class=t3.CategoryID join Course_or_Subject_Master t4 on t1.Class=t4.CategoryID and t1.Subject =t4.CourseID and t1.section=t4.section  where  Teacher_Id='" + ddl_teacher.SelectedValue + "'  and t1.Class='" + ddl_class.SelectedValue + "'  and t1.Subject='" + ddl_subject.SelectedValue + "' and t4.section='" + ddl_section.Text + "'  and t1.Date like'%" + date + "%'";
                code.BindRepeater(qry, RpDetailsStudent);
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_teacher.SelectedItem.Text != "Select")
                {
                    if (ddl_class.SelectedItem.Text != "Select")
                    {
                        if (ddl_subject.SelectedItem.Text != "Select")
                        {
                            find_teacher_wise_class_taken(3);
                        }
                        else { find_teacher_wise_class_taken(2); }
                    }
                    else { find_teacher_wise_class_taken(1); }
                }
                else
                {
                    Alert("Please select teacher");
                }


            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                RpDetailsStudent.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion download_in_excel

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            pnltop.Visible = true;
            pnl_view.Visible = false;
        }


        protected void rd_day_CheckedChanged(object sender, EventArgs e)
        {
            rd_day.Checked = true;
            rd_month.Checked = false;
            rd_year.Checked = false;
            date1.Visible = true;
            month1.Visible = true;
            year1.Visible = true;
            lbnk_summeryteacherclass.Visible = false;
            grd_taken_class.DataSource = null;
            grd_taken_class.DataBind();
            pnl_view.Visible = false;

            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();
        }

        protected void rd_month_CheckedChanged(object sender, EventArgs e)
        {
            rd_day.Checked = false;
            rd_month.Checked = true;
            rd_year.Checked = false;

            date1.Visible = false;
            month1.Visible = true;
            year1.Visible = true;
            lbnk_summeryteacherclass.Visible = false;
            grd_taken_class.DataSource = null;
            grd_taken_class.DataBind();
            pnl_view.Visible = false;

            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();

        }

        protected void rd_year_CheckedChanged(object sender, EventArgs e)
        {
            date1.Visible = false;
            month1.Visible = false;
            year1.Visible = true;
            lbnk_summeryteacherclass.Visible = false;
            grd_taken_class.DataSource = null;
            grd_taken_class.DataBind();
            pnl_view.Visible = false;

            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();
        }
        protected void lbnk_summeryteacherclass_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grd_taken_class.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

       



    }
}