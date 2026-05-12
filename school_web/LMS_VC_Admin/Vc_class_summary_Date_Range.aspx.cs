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
    public partial class Vc_class_summary_Date_Range : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txt_datestart.Text = code.date();
                    txt_end_date.Text = code.date();

                    BindCourse();
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
                if (Convert.ToInt32(code.ConvertStringToiDate(txt_datestart.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_end_date.Text)))
                {
                    find_data();
                }
                else
                {
                    Alert("Please select date valid");
                }


            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void find_data()
        {
            try
            {
                if (txt_datestart.Text == "")
                {
                    Alert("Please select start date");
                    pnl_view.Visible = false;
                    RpDetailsStudent.DataSource = null;
                    RpDetailsStudent.DataBind();
                    grd_taken_class.DataSource = null;
                    grd_taken_class.DataBind();
                }
                else if (txt_end_date.Text == "")
                {
                    Alert("Please select end date");
                    pnl_view.Visible = false;
                    RpDetailsStudent.DataSource = null;
                    RpDetailsStudent.DataBind();
                    grd_taken_class.DataSource = null;
                    grd_taken_class.DataBind();
                }

                else
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@date1", code.ConvertStringToiDate(txt_datestart.Text));
                    cmd.Parameters.AddWithValue("@date2", code.ConvertStringToiDate(txt_end_date.Text));
                    cmd.Parameters.AddWithValue("@status", "1");
                    cmd.CommandText = "sp_metting_summary_date_wise";
                    DataTable dt = UsesCode.Getdata_sp(cmd);
                    if (Convert.ToString(dt.Rows.Count) == "0")
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
                lbl_month_year.Text = txt_datestart.Text + " To " + txt_end_date.Text;
                find_teacher_wise_class_taken(1);
                pnltop.Visible = false;
                pnl_view.Visible = true;
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void find_teacher_wise_class_taken(int p)
        {
            SqlCommand cmd = new SqlCommand();

            if (p == 1)
            {
                cmd.Parameters.AddWithValue("@status", "2");
                cmd.Parameters.AddWithValue("@Teacher_Id", ddl_teacher.SelectedValue);

            }
            if (p == 2)
            {
                cmd.Parameters.AddWithValue("@status", "3");
                cmd.Parameters.AddWithValue("@Teacher_Id", ddl_teacher.SelectedValue);
                cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);

            }
            if (p == 3)
            {
                cmd.Parameters.AddWithValue("@status", "4");
                cmd.Parameters.AddWithValue("@Teacher_Id", ddl_teacher.SelectedValue);
                cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@section", ddl_section.Text);
                cmd.Parameters.AddWithValue("@Subject", ddl_subject.SelectedValue);

            }
            cmd.Parameters.AddWithValue("@date1", code.ConvertStringToiDate(txt_datestart.Text));
            cmd.Parameters.AddWithValue("@date2", code.ConvertStringToiDate(txt_end_date.Text));
            cmd.CommandText = "sp_metting_summary_date_wise";
            DataTable dt = UsesCode.Getdata_sp(cmd);
            if (Convert.ToString(dt.Rows.Count) == "0")
            {
                RpDetailsStudent.DataSource = null;
                RpDetailsStudent.DataBind();
            }
            else
            {
                RpDetailsStudent.DataSource = dt;
                RpDetailsStudent.DataBind();
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