using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Admin
{
    public partial class Add_Teacher_Routing : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["Admin"] == null)
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                    else
                    {

                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["Sessionid"] = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddl_class, "Select distinct cm.Course_Name,cm.course_id,cm.Position from Add_course_table cm join Class_Routine_Master crm on cm.course_id=crm.Class_id where  crm.Session_id=" + ViewState["Sessionid"].ToString() + " order by cm.Position");






                    


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }
        }
        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_ddl(ddl_section, "Select distinct Section from  Class_Routine_Master where Class_id=" + ddl_class.SelectedValue + " and Session_id=" + ViewState["Sessionid"].ToString() + " order by Section asc");

            }
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else
            {
                mycode.bind_ddl(ddl_day, "Select distinct dm.Day,dm.Position from Day_Master dm  join  Class_Routine_Master  crm  on crm.Day=dm.Day   where crm.Class_id=" + ddl_class.SelectedValue + " and crm.Session_id=" + ViewState["Sessionid"].ToString() + " and crm.Section='" + ddl_section.Text + "' order by dm.Position asc");
                Bind_grid_data();
            }
        }


        protected void ddl_day_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else if (ddl_day.Text == "Select")
            {
                Alertme("Please select day", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_period, "Select distinct crp.Period_Name,crp.Period from Class_Routine_period crp  join  Class_Routine_Master  crm  on crm.Class_period=crp.Period   where crm.Class_id=" + ddl_class.SelectedValue + " and crm.Session_id=" + ViewState["Sessionid"].ToString() + " and crm.Section='" + ddl_section.Text + "' and crm.Day='" + ddl_day.SelectedValue + "' order by crp.Period asc");

            }
        }

        protected void ddl_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else if (ddl_day.Text == "Select")
            {
                Alertme("Please select day", "warning");
            }
            else if (ddl_period.SelectedItem.Text == "Select")
            {
                Alertme("Please select day", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join   Class_Routine_Master crm   on crm.Subject_id=sm.Subject_id   where crm.Class_id=" + ddl_class.SelectedValue + " and crm.Session_id=" + ViewState["Sessionid"].ToString() + " and crm.Section='" + ddl_section.Text + "' and crm.Day='" + ddl_day.SelectedValue + "' and  crm.Class_period='" + ddl_period.SelectedValue + "' order by sm.Subject_position asc");
            }

        }

        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else if (ddl_day.Text == "Select")
            {
                Alertme("Please select day", "warning");
            }
            else if (ddl_period.SelectedItem.Text == "Select")
            {
                Alertme("Please select period", "warning");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alertme("Please select subject", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_teacher, "Select  distinct ud.name,ud.user_id from user_details ud  join TeacherCourseSubjectMaping tcs on tcs.UserID=ud.user_id  join Class_Routine_Master  crm  on crm.Subject_id=tcs.AssignCourseID and crm.Class_id=tcs.CategoryID and crm.Section=tcs.section and crm.Session_id=tcs.Session_id   where crm.Class_id=" + ddl_class.SelectedValue + " and crm.Session_id=" + ViewState["Sessionid"].ToString() + " and crm.Section='" + ddl_section.Text + "' and crm.Day='" + ddl_day.SelectedValue + "' and   crm.Class_period='" + ddl_period.SelectedValue + "' and crm.Subject_id='" + ddl_subject.SelectedValue + "' order by name");

            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string query = "";
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else if (ddl_day.Text == "Select")
            {
                Alertme("Please select day", "warning");
            }
            else if (ddl_period.SelectedItem.Text == "Select")
            {
                Alertme("Please select period", "warning");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alertme("Please select subject", "warning");
            }
            else
            {
               
                if (btn_Submit.Text == "Add")
                {

                    #region add add

                    bool chk_add_or_not = get_status_add_or_not_at_the_time_of_addtion_teacher();
                    bool chk_onether_allocated_sameperiod_other_class = get_teacher_statusonether_allocated_sameperiod_other_class();
                    if (chk_add_or_not == false)// added or not added same period same teacher
                    {
                        Alertme("Sorry! You can't add this teacher, because already added this teacher same period.", "warning");
                    }
                    else if (chk_onether_allocated_sameperiod_other_class == false)
                    {
                        Alertme("Sorry! You can't add this teacher, because theis teacher alredy allocated same period other class. ", "warning");
                    }
                    else
                    {

                        query = "Select *  from Class_Routine_Master_Teacher where Session_id=" + ViewState["Sessionid"].ToString() + "  and Class_period=" + ddl_period.Text + " and Day='" + ddl_day.Text + "' and Subject_id='" + ddl_subject.SelectedValue + "' and  Section='" + ddl_section.Text + "' ";

                        DataTable dt = mycode.FillData(query);
                        if (dt.Rows.Count == 0)
                        {
                            SqlCommand cmd;
                            query = "INSERT INTO Class_Routine_Master_Teacher (Teacher_id,Session_id,Class_id,Section,Subject_id,Class_period,Day,Created_By,Date,Idate,time) values (@Teacher_id,@Session_id,@Class_id,@Section,@Subject_id,@Class_period,@Day,@Created_By,@Date,@Idate,@time )";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Teacher_id", ddl_teacher.SelectedValue);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                            cmd.Parameters.AddWithValue("@Class_period", ddl_period.Text);
                            cmd.Parameters.AddWithValue("@Day", ddl_day.Text);
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@time", mycode.time());
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                Alertme("Teacher Routine has been saved successfully", "success");
                                empty_data();
                            }
                        }
                        else
                        {

                            // update

                            string id = dt.Rows[0]["Id"].ToString();



                            SqlCommand cmd;
                            query = "Update Class_Routine_Master_Teacher set Session_id=@Session_id,Class_id=@Class_id,Section=@Section,Subject_id=@Subject_id,Class_period=@Class_period,Day=@Day,Updated_by=@Updated_by,Updated_Date=@Updated_Date,Updated_Idate=@Updated_Idate,Updated_Time=@Updated_Time,Teacher_id=@Teacher_id where Id = @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Teacher_id", ddl_teacher.SelectedValue);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                            cmd.Parameters.AddWithValue("@Class_period", ddl_period.SelectedValue);
                            cmd.Parameters.AddWithValue("@Day", ddl_day.Text);
                            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Updated_Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Updated_Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@Updated_Time", mycode.time());
                            cmd.Parameters.AddWithValue("@Id", id);
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                Alertme("Teacher Routine has been updated successfully", "success");
                                empty_data();
                            }


                        }


                    }
                    #endregion
                }
                else
                {
                    #region update
                    bool chk_add_or_not2 = get_status_add_or_not_at_the_time_of_addtion_teacher_Update_time();
                    bool chk_onether_allocated_sameperiod_other_class2 = get_teacher_statusonether_allocated_sameperiod_other_class_update();
                    if (chk_add_or_not2 == false)// added or not added same period same teacher
                    {
                        Alertme("Sorry! You can't add this teacher, because already added this teacher same period.", "warning");
                    }
                    else if (chk_onether_allocated_sameperiod_other_class2 == false)
                    {
                        Alertme("Sorry! You can't add this teacher, because theis teacher alredy allocated same period other class. ", "warning");
                    }
                    else
                    {
                        SqlCommand cmd;
                        query = "Update Class_Routine_Master_Teacher set Session_id=@Session_id,Class_id=@Class_id,Section=@Section,Subject_id=@Subject_id,Class_period=@Class_period,Day=@Day,Updated_by=@Updated_by,Updated_Date=@Updated_Date,Updated_Idate=@Updated_Idate,Updated_Time=@Updated_Time,Teacher_id=@Teacher_id where Id = @Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Teacher_id", ddl_teacher.SelectedValue);
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                        cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_period", ddl_period.SelectedValue);
                        cmd.Parameters.AddWithValue("@Day", ddl_day.Text);
                        cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Updated_Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Updated_Time", mycode.time());
                        cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Alertme("Teacher Routine has been updated successfully", "success");
                            empty_data();
                        }

                    }
                    #endregion
                }
               

            
            }
        }

        private bool get_teacher_statusonether_allocated_sameperiod_other_class_update()
        {
            string query = "Select *  from Class_Routine_Master_Teacher where Session_id=" + ViewState["Sessionid"].ToString() + "  and Class_period=" + ddl_period.Text + " and Day='" + ddl_day.Text + "' and Teacher_id='" + ddl_teacher.SelectedValue + "' and   Id!=" + hd_id.Value + "";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool get_status_add_or_not_at_the_time_of_addtion_teacher_Update_time()
        {
            string query = "Select *  from Class_Routine_Master_Teacher where Session_id=" + ViewState["Sessionid"].ToString() + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Subject_id=" + ddl_subject.SelectedValue + " and Class_period=" + ddl_period.Text + " and Day='" + ddl_day.Text + "' and Teacher_id='" + ddl_teacher.SelectedValue + "' and   Id!=" + hd_id.Value + "";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        private void empty_data()
        {
            btn_Submit.Text = "Add";
            ddl_period.SelectedValue = "0";
           
            ddl_subject.SelectedValue = "0";

            grid111.Visible = false;
            Bind_grid_data();
        }

        private void Bind_grid_data()
        {
            string query = "Select crm.*,(Select top 1 name from user_details where user_id=crm.Teacher_id) as teachername, cm.Course_Name as Classname,(select top 1 Subject_name from Subject_Master where course_id=crm.Class_id  and Subject_id=crm.Subject_id) as subjectname,(select top 1 Session from session_details where session_id=crm.Session_id) as Session,(Select top 1 Period_Name from Class_Routine_period where Period=crm.Class_period and Session_id=crm.Session_id) as Period_Name from Class_Routine_Master_Teacher crm join Add_course_table cm on crm.Class_id=cm.course_id where crm.Session_id=" + ViewState["Sessionid"].ToString() + " and crm.Class_id=" + ddl_class.SelectedValue + " and crm.Section='" + ddl_section.Text + "' ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_Submit.Text = "Add";
                grid111.Visible = true;
                grid111.Visible = false;
                RPDetails.DataSource = null;
                RPDetails.DataBind();

            }
            else
            {
                grid111.Visible = false;
                grid111.Visible = true;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }
        private bool get_teacher_statusonether_allocated_sameperiod_other_class()
        {
            string query = "Select *  from Class_Routine_Master_Teacher where Session_id=" + ViewState["Sessionid"].ToString() + "  and Class_period=" + ddl_period.Text + " and Day='" + ddl_day.Text + "' and Teacher_id='" + ddl_teacher.SelectedValue + "'";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool get_status_add_or_not_at_the_time_of_addtion_teacher()
        {
            string query = "Select *  from Class_Routine_Master_Teacher where Session_id=" + ViewState["Sessionid"].ToString() + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Subject_id=" + ddl_subject.SelectedValue + " and Class_period=" + ddl_period.Text + " and Day='" + ddl_day.Text + "' and Teacher_id='" + ddl_teacher.SelectedValue + "'";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }



        #region grid edit and delete
        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label Id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = Id.Text;

                Label lbl_Session_id = (Label)row.FindControl("lbl_Session_id");
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Subject_id = (Label)row.FindControl("lbl_Subject_id");
                Label lbl_section = (Label)row.FindControl("lbl_section");
                Label lbl_Day = (Label)row.FindControl("lbl_Day");
                Label lbl_Class_period = (Label)row.FindControl("lbl_Class_period");
                Label lbl_Teacher_id = (Label)row.FindControl("lbl_Teacher_id");
                ddl_class.SelectedValue = lbl_Class_id.Text;


                mycode.bind_ddl(ddl_section, "Select distinct Section from  Class_Routine_Master where Class_id=" + ddl_class.SelectedValue + " and Session_id=" + ViewState["Sessionid"].ToString() + " order by Section asc");

                ddl_section.Text = lbl_section.Text;

                mycode.bind_ddl(ddl_day, "Select distinct dm.Day,dm.Position from Day_Master dm  join  Class_Routine_Master  crm  on crm.Day=dm.Day   where crm.Class_id=" + ddl_class.SelectedValue + " and crm.Session_id=" + ViewState["Sessionid"].ToString() + " and crm.Section='" + ddl_section.Text + "' order by dm.Position asc");
                ddl_day.Text = lbl_Day.Text;

                mycode.bind_all_ddl_with_id(ddl_period, "Select distinct crp.Period_Name,crp.Period from Class_Routine_period crp  join  Class_Routine_Master  crm  on crm.Class_period=crp.Period   where crm.Class_id=" + ddl_class.SelectedValue + " and crm.Session_id=" + ViewState["Sessionid"].ToString() + " and crm.Section='" + ddl_section.Text + "' and crm.Day='" + ddl_day.SelectedValue + "' order by crp.Period asc");

                ddl_period.Text = lbl_Class_period.Text;

                mycode.bind_all_ddl_with_id(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join   Class_Routine_Master crm   on crm.Subject_id=sm.Subject_id   where crm.Class_id=" + ddl_class.SelectedValue + " and crm.Session_id=" + ViewState["Sessionid"].ToString() + " and crm.Section='" + ddl_section.Text + "' and crm.Day='" + ddl_day.SelectedValue + "' and  crm.Class_period='" + ddl_period.SelectedValue + "' order by sm.Subject_position asc");




                ddl_subject.SelectedValue = lbl_Subject_id.Text;


                mycode.bind_all_ddl_with_id(ddl_teacher, "Select  distinct ud.name,ud.user_id from user_details ud  join TeacherCourseSubjectMaping tcs on tcs.UserID=ud.user_id  join Class_Routine_Master  crm   on crm.Subject_id=tcs.AssignCourseID and crm.Class_id=tcs.CategoryID and crm.Section=tcs.section and crm.Session_id=tcs.Session_id   where crm.Class_id=" + ddl_class.SelectedValue + " and crm.Session_id=" + ViewState["Sessionid"].ToString() + " and crm.Section='" + ddl_section.Text + "' and crm.Day='" + ddl_day.SelectedValue + "' and   crm.Class_period='" + ddl_period.SelectedValue + "' and crm.Subject_id='" + ddl_subject.SelectedValue + "' order by name");

                ddl_teacher.SelectedValue = lbl_Teacher_id.Text;

                btn_Submit.Text = "Update";
                Bind_grid_data();
                grid111.Visible = true;
            }
            catch
            {
            }

        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;

                Label Id = (Label)row.FindControl("lbl_Id");

                mycode.executequery("delete from Class_Routine_Master_Teacher where Id=" + Id.Text + "");

                Alertme("Deletion process has been completed", "success");
                empty_data();
                hd_id.Value = "0";

            }
            catch { }
        }
        #endregion


    }
}