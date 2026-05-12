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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
 
namespace school_web.LMS_VC_Admin
{
    public partial class View_teacherwise_topic : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        string sessionname = "Select top 1 Session from session_details where session_id=t1.Session_id";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    code.bind_all_ddl_with_all(ddl_teacher_list, "select name as Name,user_id as  UserID from user_details where (User_Type='Teacher' or User_Type='Principal')  order by Name  asc");
                    txt_date.Text = code.date();
                    txt_enddate.Text = code.date();
                    BindGridView(1);
                

                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }



        

        protected void ddl_teacher_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                BindGridView(1);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        private void BindGridView(int p)
        {
            ViewState["flag"] = p.ToString();

            if (p == 1)
            {
                //code.BindRepeater("Select t1.* ,cm.CategoryName,csm.CourseName,(Select SetionName from SectionMaster where SectionID=t1.SectionID)  as SetionName,(select Name from InstructorProfile where UserID=t1.UserID) as teachername from TopicMaster t1 join ClassMaster cm on t1.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on t1.CategoryID=csm.CategoryID and t1.CourseID=csm.CourseID and t1.Section_Subject=csm.section  where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " order by t1.Idate desc ", RPDetails);

                code.BindRepeater("Select (" + sessionname + ") as Session ,t1.* ,cm.Course_Name as CategoryName,csm.Subject_name as CourseName,(select name from user_details where user_id=t1.UserID) as teachername from TopicMaster t1 join Add_course_table cm on t1.CategoryID=cm.course_id join Subject_Master csm on t1.CategoryID=csm.course_id and t1.CourseID=csm.Subject_id  where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " order by t1.Idate desc ", RPDetails);


            }


            else if (p == 2)
            {
                if (ddl_teacher_list.SelectedItem.Text == "ALL")
                {
                    code.BindRepeater("Select (" + sessionname + ") as Session ,t1.* ,cm.Course_Name as CategoryName,csm.Subject_name as CourseName,(select name from user_details where user_id=t1.UserID) as teachername from TopicMaster t1 join Add_course_table cm on t1.CategoryID=cm.course_id join Subject_Master csm on t1.CategoryID=csm.course_id and t1.CourseID=csm.Subject_id  where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " order by t1.Idate desc ", RPDetails);
                }
                else
                {
                    code.BindRepeater("Select (" + sessionname + ") as Session,t1.* ,cm.Course_Name as CategoryName,csm.Subject_name as CourseName,(select name from user_details where user_id=t1.UserID) as teachername from TopicMaster t1 join Add_course_table cm on t1.CategoryID=cm.course_id join Subject_Master csm on t1.CategoryID=csm.course_id and t1.CourseID=csm.Subject_id  where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and t1.UserID='" + ddl_teacher_list.SelectedValue + "' order by t1.Idate desc", RPDetails);
                }
            }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }


        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                SqlCommand cmd = new SqlCommand("Delete from TopicMaster where Id='" + lbl_id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                if (ViewState["flag"].ToString() == "1")
                    BindGridView(1);
                else
                    BindGridView(2);
                Alert("successfully deleted.");

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridView(2);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_Details = (Label)row.FindControl("lbl_Details");
                Label lbl_SetionNamee = (Label)row.FindControl("lbl_SetionNamee");
                Label lbl_TopicName = (Label)row.FindControl("lbl_TopicName");
                txt_info1.Text = lbl_Details.Text;
                lbl_lession.Text = lbl_SetionNamee.Text;
                lbl_topic.Text = lbl_TopicName.Text;
                Label lbl_TopicID = (Label)row.FindControl("lbl_TopicID");
                ViewState["TopicID"] = lbl_TopicID.Text;
                BindGridView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindGridView()
        {
            DataTable dt = code.FillTable("select * from Topic_Uploaded_images where Topic_Id='" + ViewState["TopicID"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                GrdViewimg.DataSource = null;
                //GrdViewimg.EmptyDataText = "Data Not Available";
                GrdViewimg.DataBind();
            }
            else
            {
                GrdViewimg.DataSource = dt;

                GrdViewimg.DataBind();
            }
        }

        protected void BtnDeleteimg_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "";
                string dir_path = "";
                Button lnk = (Button)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_imp = (Label)row.FindControl("lbl_imp");


                try
                {
                    string patha1 = lbl_imp.Text.Replace(code.geturl(), "");

                    path = (Server.MapPath("../")).ToString() + patha1;
                    dir_path = Path.GetDirectoryName(path);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                }
                catch
                {
                }

                SqlCommand cmd = new SqlCommand("Delete from Topic_Uploaded_images where Id='" + lbl_Id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                Alert("successfully deleted.");
                BindGridView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Response.Redirect("Upload_Studey_Material.aspx?Id=" + lbl_Id.Text, false);
            }
            catch
            {
            }
        }

        protected void GrdViewimg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Type = (Label)e.Row.FindControl("lbl_Type");
                HtmlAnchor a2 = e.Row.FindControl("a2") as HtmlAnchor;
                HtmlAnchor a1 = e.Row.FindControl("a1") as HtmlAnchor;

                if (lbl_Type.Text == "Attachment File")
                {
                    a2.Visible = true;
                    a1.Visible = false;
                }
                else
                {
                    a2.Visible = false;
                    a1.Visible = true;
                }
            }
        }
    }
}