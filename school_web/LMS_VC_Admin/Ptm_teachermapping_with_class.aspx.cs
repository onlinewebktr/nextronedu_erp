using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
namespace school_web.LMS_VC_Admin
{
    public partial class Ptm_teachermapping_with_class : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sesssionid"] = My.get_session_id();
                code.bind_all_ddl_with_id(ddl_class, "Select Course_Name, course_id from Add_course_table   order by Position");
                code.bind_all_ddl_with_all(ddl_SearchCategory, "Select Course_Name, course_id from Add_course_table   order by Position");
                code.bind_ddl_all1(ddl_section_serch, "Select distinct section  from section_master   order by section");
                code.bind_ddl(ddl_section, "Select distinct Section  from section_master   order by Section");

                code.bind_all_ddl_with_id(ddl_teacher, " select   name,user_id   from user_details where User_Type in ('Teacher','Principal','Coordinator') and  Istatus='1'  order by name  asc");

                code.bind_all_ddl_with_all(ddl_searchInstructor, " select name,user_id   from user_details where User_Type in ('Teacher','Principal','Coordinator') and  Istatus='1'  order by name  asc");


                search_data();
            }
        }


        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }


        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (ddl_teacher.SelectedItem.Text == "Select")
            {
                Alert("Please select teacher");

            }
            else
            {
                if (btn_submit.Text == "Add")
                {
                    if (code.IsExist("Select * from Ptm_class_teacher_mapping where CategoryID='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and UserID='" + ddl_teacher.SelectedValue + "' "))
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Ptm_class_teacher_mapping (CategoryID,Section,UserID,Date,idate,time,Session_Id) values (@CategoryID,@Section,@UserID,@Date,@idate,@time,@Session_Id)";
                        cmd = new SqlCommand(query);
                        if (ddl_class.SelectedValue == "0")
                        {
                            cmd.Parameters.AddWithValue("@CategoryID", "ALL");

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CategoryID", ddl_class.SelectedValue);
                        }


                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);



                        cmd.Parameters.AddWithValue("@UserID", ddl_teacher.SelectedValue);
                        cmd.Parameters.AddWithValue("@Date", code.date());
                        cmd.Parameters.AddWithValue("@idate", code.idate());
                        cmd.Parameters.AddWithValue("@time", code.time());
                        cmd.Parameters.AddWithValue("@Session_Id", ViewState["sesssionid"].ToString());

                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Alert("Teacher & class has been mapped successfully");
                            try
                            {

                                ddl_class.SelectedValue = "0";
                                ddl_teacher.SelectedValue = "0";
                                ddl_section.Text = "ALL";
                            }
                            catch
                            {
                            }
                            search_data();
                        }
                    }
                    else
                    {
                        Alert("Already added");

                    }
                }
                else
                {


                    if (code.IsExist("Select * from Ptm_class_teacher_mapping where CategoryID='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and UserID='" + ddl_teacher.SelectedValue + "' and Id!='" + hd_id.Value + "' "))
                    {
                        SqlCommand cmd;
                        string query = "Update Ptm_class_teacher_mapping set CategoryID=@CategoryID,Section=@Section,UserID=@UserID,Date=@Date,idate=@idate,time=@time where Id = @Id";
                        cmd = new SqlCommand(query);
                        if (ddl_class.SelectedValue == "0")
                        {
                            cmd.Parameters.AddWithValue("@CategoryID", "ALL");

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CategoryID", ddl_class.SelectedValue);
                        }


                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);


                        cmd.Parameters.AddWithValue("@UserID", ddl_teacher.SelectedValue);
                        cmd.Parameters.AddWithValue("@Date", code.date());
                        cmd.Parameters.AddWithValue("@idate", code.idate());
                        cmd.Parameters.AddWithValue("@time", code.time());
                        cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Alert("Teacher and class has been mapped successfully");
                            btn_submit.Text = "Add";
                            try
                            {

                                ddl_class.SelectedValue = "0";
                                ddl_teacher.SelectedValue = "0";
                                ddl_section.Text = "ALL";
                            }
                            catch
                            {
                            }
                            search_data();
                        }
                    }
                    else
                    {
                        Alert("Already added");

                    }

                }
            }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                btn_submit.Text = "Update";

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_UserID = (Label)row.FindControl("lbl_UserID");
                Label lbl_Section = (Label)row.FindControl("lbl_Section");
                Label lbl_CategoryID = (Label)row.FindControl("lbl_CategoryID");

                hd_id.Value = lbl_Id.Text;
                code.bind_all_ddl_with_all(ddl_class, "Select Course_Name, course_id from Add_course_table   order by Position");

                if (lbl_CategoryID.Text == "ALL")
                {

                    ddl_class.SelectedValue = "0";
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from section_master   order by Section");
                }
                else
                {
                    ddl_class.SelectedValue = lbl_CategoryID.Text;
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from section_master   order by Section");


                }
                ddl_section.Text = lbl_Section.Text;
           
                ddl_teacher.SelectedValue = lbl_UserID.Text;
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
                SqlCommand cmd = new SqlCommand("Delete from Ptm_class_teacher_mapping where Id='" + lbl_id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                search_data(); Alert("successfully Deleted.");
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }





        #region search data

        protected void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                search_data();
            }
            catch
            {
            }

        }

        private void search_data()
        {
            string query;
            if (ddl_SearchCategory.SelectedItem.Text != "ALL" && ddl_searchInstructor.SelectedItem.Text != "ALL")
            {
                if (ddl_section_serch.Text == "ALL")
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm  join Add_course_table cm on ptm.CategoryID=cm.course_id where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' and ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
                else if (ddl_section_serch.Text == "")
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm  join Add_course_table cm on ptm.CategoryID=cm.course_id where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' and ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
                else
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' and ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' and Section='" + ddl_section_serch.Text + "' order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }


            }

            else if (ddl_SearchCategory.SelectedItem.Text == "ALL" && ddl_searchInstructor.SelectedItem.Text != "ALL")
            {
                if (ddl_section_serch.Text == "ALL")
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where  ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
                else if (ddl_section_serch.Text == "")
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm  join Add_course_table cm on ptm.CategoryID=cm.course_id where  ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
                else
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  BHAKAT  where  ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' and Section='" + ddl_section_serch.Text + "' order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
            }

            else if (ddl_SearchCategory.SelectedItem.Text != "ALL" && ddl_searchInstructor.SelectedItem.Text == "ALL")
            {
                if (ddl_section_serch.Text == "ALL")
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "'  order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
                else if (ddl_section_serch.Text == "")
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "'  order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
                else
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "'  and Section='" + ddl_section_serch.Text + "' order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
            }
            else
            {
                if (ddl_section_serch.Text == "ALL")
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
                else if (ddl_section_serch.Text == "")
                {
                    query = " Select ptm.*,cm.Course_Name,(select Name from InstructorProfile where UserID=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id    order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
                else
                {
                    query = " Select ptm.*,cm.Course_Name,(select name from user_details where user_id=ptm.UserID) as UserName  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where  Section='" + ddl_section_serch.Text + "' order by ptm.Section asc";

                    code.BindRepeater(query, RPDetails);
                }
            }
        }
        #endregion

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //try
            //{

            //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //    {

            //        if (((Label)e.Item.FindControl("lbl_CategoryID")).Text == "ALL")
            //        {
            //            ((Label)e.Item.FindControl("lbl_CategoryName")).Text = "ALL";



            //        }
            //        else
            //        {
            //            ((Label)e.Item.FindControl("lbl_CategoryName")).Text = code.get_class_name(((Label)e.Item.FindControl("lbl_CategoryID")).Text);

            //        }



            //    }
            //}
            //catch { }
        }





    }
}