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

namespace school_web.LMS_VC_Admin
{
    public partial class View_Home_Work_Class_Wise : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txt_date.Text = code.date();
                    txt_enddate.Text = code.date();
                    code.bind_all_ddl_with_all(ddl_class, "Select Course_Name, course_id from Add_course_table  order by Position asc");

                    
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by section");
                    BindGridView(1);
                }

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "ALL")
            {
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor    order by section");
            }
            else
            {
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'   order by section");
            }
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            if (txt_date.Text == "")
            {
                Alert("Please select start date");
            }
            else if (txt_enddate.Text == "")
            {
                Alert("Please select end date");
            }
            else
            {
                if (Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)))
                {
                    if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
                    {
                        BindGridView(1);
                    }
                    else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
                    {
                        BindGridView(2);
                    }
                    else if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.Text != "ALL")
                    {
                        BindGridView(3);
                    }
                    else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
                    {
                        BindGridView(4);
                    }
                }
                else
                {
                    Alert("Please select date valid");
                }


            }
        }

        private void BindGridView(int p)
        {
            ViewState["flag"] = p.ToString();

            if (p == 1)
            {
                code.BindRepeater(" select t1.*,t2.Course_Name as CategoryName,t1.Section as Section_Name ,(select top 1 Subject_name from Subject_Master where Subject_id=t1.Subject and course_id=t1.Class) as CourseName,(select top 1 name from user_details where user_id=t1.Upload_By) as teachername  from dbo.[Homework_Details] t1 join Add_course_table t2 on t1.Class=t2.course_id where t1.Upload_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Upload_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " order by t1.Upload_Idate desc ", RPDetails);
            }
            else if (p == 2)
            {


                code.BindRepeater(" select t1.*,t2.Course_Name as CategoryName,t1.Section as Section_Name ,(select top 1 Subject_name from Subject_Master where Subject_id=t1.Subject and course_id=t1.Class) as CourseName,(select top 1 name from user_details where user_id=t1.Upload_By) as teachername  from dbo.[Homework_Details] t1 join Add_course_table t2 on t1.Class=t2.course_id where t1.Class='" + ddl_class.SelectedValue + "' and   t1.Upload_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Upload_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " order by t1.Upload_Idate desc    ", RPDetails);

            }

            else if (p == 3)
            {


                code.BindRepeater(" select t1.*,t2.Course_Name as CategoryName,t1.Section as Section_Name ,(select top 1 Subject_name from Subject_Master where Subject_id=t1.Subject and course_id=t1.Class) as CourseName,(select top 1 name from user_details where user_id=t1.Upload_By) as teachername  from dbo.[Homework_Details] t1 join Add_course_table t2 on t1.Class=t2.course_id where t1.Section='" + ddl_section.Text + "' and   t1.Upload_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Upload_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " order by t1.Upload_Idate desc    ", RPDetails);

            }
            else if (p == 4)
            {


                code.BindRepeater("select t1.*,t2.Course_Name as CategoryName,t1.Section as Section_Name ,(select top 1 Subject_name from Subject_Master where Subject_id=t1.Subject and course_id=t1.Class) as CourseName,(select top 1 name from user_details where user_id=t1.Upload_By) as teachername  from dbo.[Homework_Details] t1 join Add_course_table t2 on t1.Class=t2.course_id where t1.Section='" + ddl_section.Text + "' and t1.Class='" + ddl_class.SelectedValue + "' and   t1.Upload_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Upload_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " order by t1.Upload_Idate desc    ", RPDetails);

            }
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Label lbl_Home_Work_id = (Label)row.FindControl("lbl_Home_Work_id");

                bool checkhomeworkstatus = UsesCode.check_home_work_status(lbl_Home_Work_id.Text);

                if (checkhomeworkstatus == true)
                {
                    Delete_homeworkimg(lbl_Home_Work_id.Text);
                    SqlCommand cmd = new SqlCommand("Delete from Homework_Details where Id='" + lbl_id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                    SqlCommand cmd1 = new SqlCommand("Delete from HomeWork_Uploaded_Images where Homework_Id='" + lbl_Home_Work_id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd1);

                    if (ViewState["flag"].ToString() == "1")
                        BindGridView(1);
                    else
                        BindGridView(2);
                    Alert("successfully deleted.");

                }
                else
                {
                    Alert("Sorry! You can't delete his homework because the students already replied to this homework.");
                }

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void Delete_homeworkimg(string homwork)
        {
            string path = "";
            string dir_path = "";
            DataTable dt = code.FillTable("select * from HomeWork_Uploaded_Images where Homework_Id='" + homwork + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                try
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Images = dt.Rows[i]["Images"].ToString();
                        string patha1 = Images.Replace(code.geturl(), "");

                        path = (Server.MapPath("../")).ToString() + patha1;


                        dir_path = Path.GetDirectoryName(path);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }


                    }




                }
                catch
                {
                }
            }
        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_Description = (Label)row.FindControl("lbl_Description");
                txt_info1.Text = lbl_Description.Text;
                Label lbl_Home_Work_id = (Label)row.FindControl("lbl_Home_Work_id");
                Label lbl_Topic = (Label)row.FindControl("lbl_Topic");
                lbl_topicdetails.Text = lbl_Topic.Text;

                ViewState["lbl_Home_Work_id"] = lbl_Home_Work_id.Text;
                BindGridView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindGridView()
        {
            DataTable dt = code.FillTable("select *,(select top 1 Topic from Homework_Details where Home_Work_id=HomeWork_Uploaded_Images.Homework_Id) as Topicname,(select top 1 Upload_Date from Homework_Details where Home_Work_id=HomeWork_Uploaded_Images.Homework_Id) as Upload_Date from HomeWork_Uploaded_Images where Homework_Id='" + ViewState["lbl_Home_Work_id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                GrdViewimg.DataSource = null;
                // GrdViewimg.EmptyDataText = "Data Not Available";
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
                GridView row = (GridView)lnk.NamingContainer;
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

                SqlCommand cmd = new SqlCommand("Delete from HomeWork_Uploaded_Images where id='" + lbl_Id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                Alert("successfully deleted.");
                BindGridView();


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
                Response.Redirect("Upload_Home_Work.aspx?Id=" + lbl_Id.Text, false);
            }
            catch
            {
            }
        }
    }
}