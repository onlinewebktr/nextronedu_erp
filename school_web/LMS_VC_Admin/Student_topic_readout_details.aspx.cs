using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Student_topic_readout_details : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try { if (!IsPostBack) { BindDDL(); BindGridView(1); };}
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindDDL()
        {
            code.bind_all_ddl_with_id(ddl_CourseCat, "Select CategoryName, CategoryID from ClassMaster where CategoryID in(select CategoryID from  TeacherCourseSubjectMaping) and Istatus=1 order by CategoryName");
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            code.bind_all_ddl_with_id(ddl_Course, "Select CourseName, CourseID from Course_or_Subject_Master where CategoryID='" + ddl_CourseCat.SelectedValue + "' order by CourseName");
        }

        protected void ddl_Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            code.bind_all_ddl_with_id(ddlSectionname, "Select SetionName, SectionID from SectionMaster where CategoryID='" + ddl_CourseCat.SelectedValue + "' and CourseID='" + ddl_Course.SelectedValue + "' order by SetionName");
        }


        private void BindGridView(int p)
        {
            try
            {
                string query;
                ViewState["flag"] = p.ToString();

                if (p == 1)
                {
                    query = "select T1.Date,T1.Time,T1.Status,T1.EstTime,T1.ReadOut,T1.ReadOut,T1.Start_at,T1.Finished_at,t2.admissionserialnumber,t2.Original_Name,t2.mobilenumber,t2.class,t2.Section,(Select CategoryName from ClassMaster where CategoryID=t1.CategoryID)  as CategoryName,(Select CourseName from Course_or_Subject_Master where CourseID=t1.CourseID)  as CourseName,(Select SetionName from SectionMaster where SectionID=t1.SectionID)  as SetionName,(Select TopicName from TopicMaster where TopicID=t1.TopicID)  as TopicName  from dbo.[TrackingMaster] t1 join dbo.[admission_registor] t2 on t1.UserID=t2.admissionserialnumber";
                }
                else
                {
                    query = "select T1.Date,T1.Time,T1.Status,T1.EstTime,T1.ReadOut,T1.ReadOut,T1.Start_at,T1.Finished_at,t2.admissionserialnumber,t2.Original_Name,t2.mobilenumber,t2.class,t2.Section,(Select CategoryName from ClassMaster where CategoryID=t1.CategoryID)  as CategoryName,(Select CourseName from Course_or_Subject_Master where CourseID=t1.CourseID)  as CourseName,(Select SetionName from SectionMaster where SectionID=t1.SectionID)  as SetionName,(Select TopicName from TopicMaster where TopicID=t1.TopicID)  as TopicName  from dbo.[TrackingMaster] t1 join dbo.[admission_registor] t2 on t1.UserID=t2.admissionserialnumber where CategoryID='" + ddl_CourseCat.SelectedValue + "' and CourseID='" + ddl_Course.SelectedValue + "' and SectionID='" + ddlSectionname.SelectedValue + "'";
                }
                ViewState["query"] = query;
                DataTable dt = code.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                    img_expor_excel.Visible = false;

                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                    ViewState["Data"] = null;
                    lbl_total_student.Text = "Total Records :- " + 0;
                }
                else
                {
                    ViewState["Data"] = dt;
                    img_expor_excel.Visible = true;
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                    lbl_total_student.Text = "Total Records :- " + RPDetails.Items.Count;

                }

            }
            catch(Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }

            

        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }





        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select class."); return;
                }
                if (ddl_Course.SelectedItem.Text == "Select")
                {
                    Alert("Please select subject."); return;
                }
                if (ddlSectionname.SelectedItem.Text == "Select")
                {
                    Alert("Please select section/lesson"); return;
                }
                BindGridView(2);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        protected void img_expor_excel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
               

                DataTable dt = ViewState["Data"] as DataTable;

                export_to_excel(dt, "Student_Learning_Report");
            }
            catch
            {
            }
        }
        private void export_to_excel(DataTable dt, string file)
        {

            string FileName = file + DateTime.Now + ".xls";

            string attachment = "attachment; filename=" + FileName;

            Response.ClearContent();

            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/vnd.ms-excel";

            Response.ContentEncoding = System.Text.Encoding.Unicode;

            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            string tab = "";

            foreach (DataColumn dc in dt.Columns)
            {

                Response.Write(tab + dc.ColumnName);

                tab = "\t";

            }

            Response.Write("\n");

            int i;

            foreach (DataRow dr in dt.Rows)
            {

                tab = "";

                for (i = 0; i < dt.Columns.Count; i++)
                {

                    Response.Write(tab + dr[i].ToString());

                    tab = "\t";

                }

                Response.Write("\n");

            }

            Response.End();

        }


    }
}