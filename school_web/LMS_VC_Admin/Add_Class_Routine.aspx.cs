using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Globalization;
namespace school_web.LMS_VC_Admin
{
    public partial class Add_Class_Routine : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admin"] != null)
                {
                    ViewState["Admin"] = Session["Admin"].ToString();
                    code.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details where use_mode='1'");


                    code.bind_all_ddl_with_id(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");

                    code.bind_ddl(ddl_period, " Select   Period  from Class_Routine_period order by id asc");
                }
                else
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
            }

        }
        public void Alert(string Message)
        {

            lblmessage.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else
            {
                code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
            }
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else
            {
                code.bind_all_ddl_with_id(ddl_subject, " Select distinct csm.Subject_name, csm.Subject_id from Subject_Master csm  where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position   ");
            }
        }
        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            add.Visible = false;
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (ddlday.SelectedItem.Text == "Select")
            {
                Alert("Please select day");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alert("Please select subject");
            }
            else
            {
                hd_subjectid.Value = ddl_subject.SelectedValue;
                ddl_day.Text = ddlday.Text;

                Bind_grid_data();
                // get_data();



            }
        }
        protected void ddlday_SelectedIndexChanged(object sender, EventArgs e)
        {
            add.Visible = false;
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alert("Please select subject");
            }
            else
            {
                hd_subjectid.Value = ddl_subject.SelectedValue;
                ddl_day.Text = ddlday.Text;

                Bind_grid_data();
                // get_data();



            }
        }


        private void get_data()
        {
            // string query = "Select *,format(Start_Time, 'hh:mm:ss tt') as Start_Time1,format(End_time, 'hh:mm:ss tt') as End_time1 from Class_Routine_Master where Session_id=" + ddl_session.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and  Subject_id=" + ddl_subject.SelectedValue + "   ";
            string query = "Select *,format(Start_Time, 'hh:mm:ss tt') as Start_Time1,format(End_time, 'hh:mm:ss tt') as End_time1 from Class_Routine_Master where  Id=" + hd_id.Value + " ";

            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {


            }
            else
            {

                ddl_period.Text = dt.Rows[0]["Class_period"].ToString();
                ddl_day.Text = dt.Rows[0]["Day"].ToString();
                ddlday.Text = dt.Rows[0]["Day"].ToString();
                try
                {
                    MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                    DateTime startTime = DateTime.ParseExact(dt.Rows[0]["Start_Time1"].ToString(), "hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string am1 = startTime.ToString("tt");
                    string hh = startTime.ToString("hh");
                    string mm = startTime.ToString("mm");
                    string ss = startTime.ToString("ss");
                    if (am1 == "AM")
                    {
                        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                    }
                    else
                    {
                        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                    }
                    lbl_starttime.Text = dt.Rows[0]["Start_Time1"].ToString();
                    // time_StartTime.SetTime(Convert.ToInt32(hh), Convert.ToInt32(mm), Convert.ToInt32(ss), am_pm);
                }
                catch
                {
                }

                try
                {
                    MKB.TimePicker.TimeSelector.AmPmSpec am_pm1;
                    DateTime EndTime = DateTime.ParseExact(dt.Rows[0]["End_time1"].ToString(), "hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string E_am1 = EndTime.ToString("tt");
                    string E_hh = EndTime.ToString("hh");
                    string E_mm = EndTime.ToString("mm");
                    string E_ss = EndTime.ToString("ss");
                    if (E_am1 == "AM")
                    {
                        am_pm1 = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                    }
                    else
                    {
                        am_pm1 = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                    }
                    //time_StartTime.SetTime(Convert.ToInt32(E_hh), Convert.ToInt32(E_mm), Convert.ToInt32(E_ss), am_pm1);

                    lbl_starttime.Text = dt.Rows[0]["End_time1"].ToString();
                }
                catch
                {
                }
            }
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            add_and_update_class_routine();
        }
        private void add_and_update_class_routine()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alert("Please select subject");
            }
            else if (ddl_period.Text == "Select")
            {
                Alert("Please select class period");
            }

            else if (ddl_day.Text == "Select")
            {
                Alert("Please select day");
            }
            else
            {

                DateTime startTime = DateTime.ParseExact(lbl_starttime.Text, "hh:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime EndTime = DateTime.ParseExact(lbl_endtime.Text, "hh:mm:ss tt", CultureInfo.InvariantCulture);
                if (btn_Add.Text == "Add")
                {
                    bool chk_add_or_not = get_status_add_or_not_at_the_time_of_addtion();
                    if (chk_add_or_not == false)
                    {
                        Alert("Sorry! You can't add this subject, because already added this subject same period.");
                    }
                    else
                    {
                        if (check_time_slot())
                        {

                            SqlCommand cmd;
                            string query = "INSERT INTO Class_Routine_Master (Session_id,Class_id,Section,Subject_id,Class_period,Day,Start_Time,End_time,Created_By,Date,Idate,time) values (@Session_id,@Class_id,@Section,@Subject_id,@Class_period,@Day,@Start_Time,@End_time,@Created_By,@Date,@Idate,@time)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                            cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                            cmd.Parameters.AddWithValue("@Class_period", ddl_period.Text);
                            cmd.Parameters.AddWithValue("@Day", ddl_day.Text);
                            cmd.Parameters.AddWithValue("@Start_Time", startTime.ToString("hh:mm:ss tt"));
                            cmd.Parameters.AddWithValue("@End_time", EndTime.ToString("hh:mm:ss tt"));
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["Admin"].ToString());
                            cmd.Parameters.AddWithValue("@Date", code.date());
                            cmd.Parameters.AddWithValue("@Idate", code.idate());
                            cmd.Parameters.AddWithValue("@time", code.time());
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                Alert("Routine has been saved successfully");
                                empty_data();
                            }
                        }
                        else
                        {
                            Alert("Sorry! You can't add any subject this period because this period already allocated ");
                        }


                    }

                }
                else
                {
                    bool chk_add_or_not = get_status_add_or_not_at_the_time_of_updateion();
                    if (chk_add_or_not == false)
                    {
                        Alert("Sorry! You can't add this subject, because already added this subject same period.");
                    }
                    else
                    {
                        if (check_time_slotupdate())
                        {
                            SqlCommand cmd;
                            string query = "Update Class_Routine_Master set Session_id=@Session_id,Class_id=@Class_id,Section=@Section,Subject_id=@Subject_id,Class_period=@Class_period,Day=@Day,Start_Time=@Start_Time,End_time=@End_time,Updated_by=@Updated_by,Updated_Date=@Updated_Date,Updated_Idate=@Updated_Idate,Updated_Time=@Updated_Time where Id = @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                            cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                            cmd.Parameters.AddWithValue("@Class_period", ddl_period.Text);
                            cmd.Parameters.AddWithValue("@Day", ddl_day.Text);
                            cmd.Parameters.AddWithValue("@Start_Time", startTime.ToString("hh:mm:ss tt"));
                            cmd.Parameters.AddWithValue("@End_time", EndTime.ToString("hh:mm:ss tt"));
                            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Admin"].ToString());
                            cmd.Parameters.AddWithValue("@Updated_Date", code.date());
                            cmd.Parameters.AddWithValue("@Updated_Idate", code.idate());
                            cmd.Parameters.AddWithValue("@Updated_Time", code.time());
                            cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                Alert("Routine has been updated successfully");
                                empty_data();
                            }
                        }
                        else
                        {
                            Alert("Sorry! You can't add any subject this period because this period already allocated ");
                        }

                    }
                }


            }
        }

        private bool get_status_add_or_not_at_the_time_of_updateion()
        {
            string query = "Select *  from Class_Routine_Master where Session_id=" + ddl_session.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Subject_id=" + ddl_subject.SelectedValue + " and Class_period=" + ddl_period.Text + " and Day='" + ddl_day.Text + "' and Id!=" + hd_id.Value + "";

            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {


                return true;



            }
            else
            {
                return false;
            }
        }

        private bool check_time_slotupdate()
        {
            string query = "Select *  from Class_Routine_Master where Session_id=" + ddl_session.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Class_period=" + ddl_period.Text + " and Day='" + ddl_day.Text + "' and Id!=" + hd_id.Value + "";

            DataTable dt = code.FillTable(query);
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
            btn_Add.Text = "Add";
            ddl_period.Text = "Select";
            ddl_day.Text = "Select";
            ddl_subject.SelectedValue = "0";
            
            add.Visible = false;
            Bind_grid_data();
        }

        private void Bind_grid_data()
        {
            string query = "Select crm.*,format(crm.Start_Time, 'hh:mm:ss tt') as Start_Time1,format(crm.End_time, 'hh:mm:ss tt') as End_time1,cm.Course_Name as Classname,(select top 1 Subject_name from Subject_Master where course_id=crm.Class_id and Subject_id=crm.Subject_id) as subjectname,(select top 1 Session from session_details where session_id=crm.Session_id) as Session from Class_Routine_Master crm join Add_course_table cm on crm.Class_id=cm.course_id where crm.Session_id=" + ddl_session.SelectedValue + " and crm.Class_id=" + ddl_class.SelectedValue + " and crm.Section='" + ddl_section.Text + "' and  crm.Subject_id=" + hd_subjectid.Value + " and Day='" + ddlday.Text + "'";
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                btn_Add.Text = "Add";
                add.Visible = true;
                grid111.Visible = false;
                RPDetails.DataSource = null;
                RPDetails.DataBind();

            }
            else
            {
                add.Visible = false;
                grid111.Visible = true;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        private bool get_status_add_or_not_at_the_time_of_addtion()
        {
            string query = "Select *  from Class_Routine_Master where Session_id=" + ddl_session.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Subject_id=" + ddl_subject.SelectedValue + " and Class_period=" + ddl_period.Text + " and Day='" + ddl_day.Text + "'";

            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool check_time_slot()
        {
            string query = "Select *  from Class_Routine_Master where Session_id=" + ddl_session.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Class_period=" + ddl_period.Text + " and Day='" + ddl_day.Text + "'";

            DataTable dt = code.FillTable(query);
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
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label Id = (Label)row.FindControl("lbl_Id");
            hd_id.Value = Id.Text;

            Label lbl_Session_id = (Label)row.FindControl("lbl_Session_id");
            Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
            Label lbl_Subject_id = (Label)row.FindControl("lbl_Subject_id");
            Label lbl_section = (Label)row.FindControl("lbl_section");




            ddl_session.SelectedValue = lbl_Session_id.Text;

            code.bind_all_ddl_with_id(ddl_class, "Select CategoryName, CategoryID from ClassMaster order by Position");

            ddl_class.SelectedValue = lbl_Class_id.Text;
            code.bind_ddl(ddl_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_class.SelectedValue + "' and Istatus='1'  order by section");

            ddl_section.Text = lbl_section.Text;

            code.bind_all_ddl_with_id(ddl_subject, " Select distinct csm.CourseName, csm.CourseID from Course_or_Subject_Master csm  where csm.CategoryID='" + ddl_class.SelectedValue + "' and csm.section='" + ddl_section.Text + "' and csm.session_id=" + ddl_session.SelectedValue + "  order by csm.CourseName   ");
            ddl_subject.SelectedValue = lbl_Subject_id.Text;


            btn_Add.Text = "Update";
            get_data();
            add.Visible = true;

        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;

                Label Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Subject_id = (Label)row.FindControl("lbl_Subject_id");
                hd_subjectid.Value = lbl_Subject_id.Text;
                code.executequery("delete from Class_Routine_Master where Id=" + Id.Text + "");

                Alert("Deletion process has been completed");
                empty_data();
                hd_id.Value = "0";

            }
            catch { }
        }
        #endregion

        protected void ddl_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_period.Text == "Select")
            {
                Alert("Please select class period");
            }
            else
            {
                string query = "Select format(Start_Time, 'hh:mm:ss tt') as Start_Time1,format(End_time, 'hh:mm:ss tt') as End_time1  from Class_Routine_period where  Period=" + ddl_period.Text + "   ";

                DataTable dt = code.FillTable(query);
                if (dt.Rows.Count == 0)
                {


                }
                else
                {
                    lbl_starttime.Text = dt.Rows[0]["Start_Time1"].ToString();
                    lbl_endtime.Text = dt.Rows[0]["End_time1"].ToString();
                }

            }
        }

        









    }
}