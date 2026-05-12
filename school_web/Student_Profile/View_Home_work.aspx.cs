using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class View_Home_work : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hdDIO.Value = Session["User"].ToString();
                    find_student_details();
                    BindDDL(); BindDetails(1);
                };
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void find_student_details()
        {
            DataTable dt = code.FillTable("Select * from dbo.[admission_registor] where admissionserialnumber='" + hdDIO.Value + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    hd_classid.Value = dr["Class_id"].ToString();
                    hd_section_id.Value = dr["Session_id"].ToString();
                    lbl_class.Text = dr["class"].ToString();
                    lbl_section.Text = dr["Section"].ToString();
                }
            }

        }


        private void BindDDL()
        {
            code.bind_all_ddl_with_id(ddl_subject, "Select CourseName, CourseID from Course_or_Subject_Master  where CategoryID='" + hd_classid.Value + "' order by CourseName");
        }

        private void BindDetails(int p)
        {
            ViewState["flag"] = p.ToString();

            if (p == 1)
                code.bind_gridview(grd_view, " select t1.*,t2.CategoryName, Section as Section_Name ,(select CourseName from Course_or_Subject_Master where CourseID=t1.Subject and CategoryID=t1.Class and section=t1.Section) as CourseName  from dbo.[Homework_Details] t1 join ClassMaster t2 on t1.Class=t2.CategoryID where Class='" + hd_classid.Value + "' and Section='" + hd_section_id.Value + "' ");
            else if (p == 2)
                code.bind_gridview(grd_view, " select t1.*,t2.CategoryName,Section as Section_Name ,(select CourseName from Course_or_Subject_Master where CourseID=t1.Subject and CategoryID=t1.Class and section=t1.Section) as CourseName  from dbo.[Homework_Details] t1 join ClassMaster t2 on t1.Class=t2.CategoryID where Class='" + hd_classid.Value + "' and Section='" + hd_section_id.Value + "'  and Subject='" + ddl_subject.SelectedValue + "'");

            lbl_total_student.Text = "Total Home Work :- " + grd_view.Rows.Count;
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
                if (ddl_subject.SelectedItem.Text == "Select")
                {
                    Alert("Please select subject"); return;
                }
                BindDetails(2);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                DataTable dt = code.FillTable("select * from Homework_Details where Id='" + lbl_id.Text + "'");
                txt_info.Text = dt.Rows[0]["Description"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

    }
}