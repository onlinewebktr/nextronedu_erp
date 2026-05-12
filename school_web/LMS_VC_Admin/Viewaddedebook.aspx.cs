using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Viewaddedebook : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["sesssionid"] = My.get_session_id();
                    BindCourse();
                    BindGridView(1);
                     
                } 
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        private void BindCourse()
        {
            code.bind_all_ddl_with_id(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");

        }
        private void BindGridView(int p)
        {
            ViewState["flag"] = p.ToString();

            if (p == 1)
                code.BindRepeater(" select *,(select Course_Name from Add_course_table where course_id=t1.Class_id) as CategoryName, (select Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as CourseName from dbo.[EBook_Details] t1 where   t1.Session_id='" + ViewState["sesssionid"].ToString() + "'", RPDetails);
            else if (p == 2)
                code.BindRepeater("select *,(select Course_Name from Add_course_table where course_id=t1.Class_id) as CategoryName, (select Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as CourseName from dbo.[EBook_Details] t1 where t1.Class_id='" + ddl_CourseCat.SelectedValue + "' and t1.Session_id='" + ViewState["sesssionid"].ToString() + "'", RPDetails);
            else if (p == 3)
                code.BindRepeater("select *,(select Course_Name from Add_course_table where course_id=t1.Class_id) as CategoryName, (select Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as CourseName from dbo.[EBook_Details] t1 where t1.Class_id='" + ddl_CourseCat.SelectedValue + "' and t1.section='" + ddl_section.Text + "' and t1.Session_id='" + ViewState["sesssionid"].ToString() + "'", RPDetails);

            lbl_total_student.Text = "Total Added E-Book :- " + RPDetails.Items.Count;
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select course");

                }
                else
                {

                    code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'   order by section");

                }
                BindGridView(2);

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Response.Redirect("Upload_E_book.aspx?Id=" + lbl_id.Text);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Label lbl_Path_of_ebook = (Label)row.FindControl("lbl_Path_of_ebook");

                Image Image1 = (Image)row.FindControl("Image1");

                string path = (Server.MapPath("../")).ToString() + Image1.ImageUrl;
                string dir_path = Path.GetDirectoryName(path);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = (Server.MapPath("../")).ToString() + lbl_Path_of_ebook.Text;
                dir_path = Path.GetDirectoryName(path);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                SqlCommand cmd = new SqlCommand("Delete from EBook_Details where Id='" + lbl_id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                if (ViewState["flag"].ToString() == "1")
                    BindGridView(1);
                if (ViewState["flag"].ToString() == "3")
                    BindGridView(3);
                else
                    BindGridView(2);
                Alert("successfully deleted.");

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please class");
                }
                if (ddl_section.Text == "All")
                {
                    BindGridView(2);
                }
                else
                {
                    BindGridView(3);
                }

            }
            catch
            {
            }
        }
    }
}