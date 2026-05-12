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
    public partial class View_teacherwise_homework_list : System.Web.UI.Page
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
                    txt_date.Text = code.date();
                    txt_enddate.Text = code.date();
                    Bind_teacher();
                
                    BindGridView(1);
                }

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        


        private void Bind_teacher()
        {
            code.bind_all_ddl_with_all(ddl_teacher_list, "select (Name + ' - ('+user_id+')') as  UserID,user_id from user_details where (User_Type='Teacher' or User_Type='Principal')  order by Name  asc");
        }
        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //code.bind_all_ddl_with_id(ddl_subject, "Select CourseName, CourseID from Course_or_Subject_Master where CategoryID='" + ddl_CourseCat.SelectedValue + "' order by CourseName");
        }
        private void BindGridView(int p)
        {
            ViewState["flag"] = p.ToString();

            ViewState["flag"] = p.ToString();

            if (p == 1)
            {
                code.BindRepeater(" select (" + sessionname + ") as Session,t1.*,t2.Course_Name as CategoryName,t1.Section as Section_Name ,(select Subject_name from Subject_Master where course_id=t1.Class and Subject_id=t1.Subject) as CourseName,(select top 1 name from user_details where user_id=t1.Upload_By) as teachername  from dbo.[Homework_Details] t1 join Add_course_table t2 on t1.Class=t2.course_id where t1.Upload_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Upload_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " order by t1.Upload_Idate desc ", RPDetails);
            }
            else if (p == 2)
            {
                if (ddl_teacher_list.SelectedItem.Text == "ALL")
                {
                    code.BindRepeater("select (" + sessionname + ") as Session,t1.*,t2.Course_Name as CategoryName,t1.Section as Section_Name ,(select Subject_name from Subject_Master where course_id=t1.Class and Subject_id=t1.Subject) as CourseName,(select top 1 name from user_details where user_id=t1.Upload_By) as teachername  from dbo.[Homework_Details] t1 join Add_course_table t2 on t1.Class=t2.course_id where t1.Upload_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Upload_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " order by t1.Upload_Idate desc ", RPDetails);
                }
                else
                {
                    code.BindRepeater("select (" + sessionname + ") as Session,t1.*,t2.Course_Name as CategoryName,t1.Section as Section_Name ,(select Subject_name from Subject_Master where course_id=t1.Class and Subject_id=t1.Subject) as CourseName,(select top 1 name from user_details where user_id=t1.Upload_By) as teachername  from dbo.[Homework_Details] t1 join Add_course_table t2 on t1.Class=t2.course_id where t1.Upload_By='" + ddl_teacher_list.SelectedValue + "' and   t1.Upload_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Upload_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  order by t1.Upload_Idate desc  ", RPDetails);

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

        protected void ddl_teacher_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindGridView(2);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_teacher_list.SelectedItem.Text == "Select")
                {
                    Alert("Please select teacher");
                }
                else if (txt_date.Text == "")
                {
                    Alert("Please select  start date");
                }
                else if (txt_enddate.Text == "")
                {
                    Alert("Please select end date");
                }
                else
                {
                    if (Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)))
                    {
                        BindGridView(2);
                    }
                    else
                    {
                        Alert("Please select date valid");
                    }

                }


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