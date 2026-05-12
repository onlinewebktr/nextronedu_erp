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
    public partial class Add_Class_Routine_Student : System.Web.UI.Page
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

                        string pagename_current = "Add_Class_Routine_Student.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["Sessionid"] = My.get_session_id();
                        mycode.bind_ddl(ddl_section, "Select Section from  section_master order by Section asc");
                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        Bind_day_grid();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Class_Routine_Student");
            }
        }

        private void Bind_day_grid()
        {
            string query = "Select * from Day_Master order by Position ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                rp_day.DataSource = null;
                rp_day.DataBind();
                btn_Submit.Visible = false;
            }
            else
            {
                rp_day.DataSource = dt;
                rp_day.DataBind();
                btn_Submit.Visible = true;
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
                bind_subjects();
                mycode.bind_all_ddl_with_id(ddl_period, "Select t1.Period_Name,t1.Period from Class_Routine_period t1 join Class_Routine_period_Master t2 on t1.Period=t2.Period where t1.course_id='" + ddl_class.SelectedValue + "' and t1.Session_id=" + ViewState["Sessionid"].ToString() + " order by t2.Period_no asc");
            }
        }

        private void bind_subjects()
        {
            string query = "Select Subject_name,Subject_id from Subject_Master where course_id='" + ddl_class.SelectedValue + "' order by Subject_position";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                rp_subjects.DataSource = null;
                rp_subjects.DataBind();
            }
            else
            {
                rp_subjects.DataSource = dt;
                rp_subjects.DataBind();
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
                Bind_grid_data();
            }
        }





        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_add"].ToString() == "1")
            {
                final_submit_data();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                final_submit_data();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }

        }

        private void final_submit_data()
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
                Alertme("Please period", "warning");
            }
            else
            {
                ViewState["pageq"] = "0";

                if (ViewState["PriodTpe"].ToString() == "Class")    ////CLASS
                {
                    #region ClassPeriod
                    int SubrowcountS = rp_subjects.Items.Count;
                    int kSUb = 0; int kSD = 0;
                    for (int iSub = 0; iSub < SubrowcountS; iSub++)
                    {
                        CheckBox chkSub = (CheckBox)rp_subjects.Items[iSub].FindControl("rowChkBox1");
                        if (chkSub.Checked == true)
                        {
                            Label lbl_subject_name = (Label)rp_subjects.Items[iSub].FindControl("lbl_subject_name");
                            Label lbl_subj_id = (Label)rp_subjects.Items[iSub].FindControl("lbl_subj_id");
                            kSUb++;

                            //DAY
                            int growcountSD = rp_day.Items.Count;
                            for (int iSD = 0; iSD < growcountSD; iSD++)
                            {
                                CheckBox chkS = (CheckBox)rp_day.Items[iSD].FindControl("rowChkBox1");
                                if (chkS.Checked == true)
                                {
                                    Label lbl_Day = (Label)rp_day.Items[iSD].FindControl("lbl_Day");
                                    //if (btn_Submit.Text == "Add")
                                    //{
                                    //    bool chk_add_or_not = get_status_add_or_not_at_the_time_of_addtion(lbl_Day.Text, lbl_subj_id.Text);
                                    //    if (chk_add_or_not == false)
                                    //    {
                                    //        Alertme("Sorry! You can't add this subject, because already added this subject same period.", "warning");
                                    //        return;
                                    //    }
                                    //}
                                    kSD++;
                                }
                            }
                        }
                    }
                    if (kSUb == 0)
                    {
                        Alertme("Please choose subject.", "warning");
                        return;
                    }
                    if (kSD == 0)
                    {
                        Alertme("Please choose day.", "warning");
                        return;
                    }



                    #region   DayGridS
                    int growcountS = rp_day.Items.Count;
                    int kS = 0;
                    for (int iS = 0; iS < growcountS; iS++)
                    {
                        CheckBox chkS = (CheckBox)rp_day.Items[iS].FindControl("rowChkBox1");
                        if (chkS.Checked == true)
                        {
                            Label lbl_Day = (Label)rp_day.Items[iS].FindControl("lbl_Day");
                            #region add add
                            if (btn_Submit.Text == "Add")
                            {
                                ViewState["pageq"] = "1";
                                //if (check_time_slot(lbl_Day.Text))
                                //{
                                if (mycode.IsUserExist("select * from Class_Routine_Master where Session_id=" + ViewState["Sessionid"].ToString() + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Class_period='" + ddl_period.SelectedValue + "' and Day='" + lbl_Day.Text + "'"))
                                {
                                    SqlCommand cmd;
                                    string query = "INSERT INTO Class_Routine_Master (Session_id,Class_id,Section,Subject_id,Class_period,Day,Created_By,Date,Idate,time,Period_type) values (@Session_id,@Class_id,@Section,@Subject_id,@Class_period,@Day,@Created_By,@Date,@Idate,@time,@Period_type)";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                                    cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                                    cmd.Parameters.AddWithValue("@Subject_id", "");
                                    cmd.Parameters.AddWithValue("@Class_period", ddl_period.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Day", lbl_Day.Text);
                                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                                    cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                                    cmd.Parameters.AddWithValue("@time", mycode.time());
                                    cmd.Parameters.AddWithValue("@Period_type", "Class");
                                    if (InsertUpdate.InsertUpdateData(cmd))
                                    {
                                        map_subject(lbl_Day.Text);
                                    }
                                    //}
                                    //else
                                    //{
                                    //    // Alertme("Sorry! You can't add any subject this period because this period already allocated ", "warning");
                                    //}
                                }
                                else
                                {
                                    map_subject(lbl_Day.Text);
                                }
                            }
                            kS++;
                            #endregion
                        }
                    }


                    if (kS == 0)
                    {
                        Alertme("Please choose day.", "warning");
                    }
                    #endregion


                    #region update
                    if (btn_Submit.Text == "Update")
                    {

                        ViewState["pageq"] = "2";
                        bool chk_add_or_not = get_status_add_or_not_at_the_time_of_updateion();
                        if (chk_add_or_not == false)
                        {
                            Alertme("Sorry! You can't add this subject, because already added this subject same period.", "warning");
                        }
                        else
                        {
                            if (check_update_time())
                            {
                                SqlCommand cmd;
                                string query = "Update Class_Routine_Master set Session_id=@Session_id,Class_id=@Class_id,Section=@Section,Subject_id=@Subject_id,Class_period=@Class_period,Day=@Day,Updated_by=@Updated_by,Updated_Date=@Updated_Date,Updated_Idate=@Updated_Idate,Updated_Time=@Updated_Time,Send_Status=@Send_Status where Id = @Id";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                                cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                                cmd.Parameters.AddWithValue("@Subject_id", "");
                                cmd.Parameters.AddWithValue("@Class_period", ddl_period.SelectedValue);
                                cmd.Parameters.AddWithValue("@Day", ddl_day.Text);
                                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Updated_Date", mycode.date());
                                cmd.Parameters.AddWithValue("@Updated_Idate", mycode.idate());
                                cmd.Parameters.AddWithValue("@Updated_Time", mycode.time());
                                cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                                
                                if (InsertUpdate.InsertUpdateData(cmd))
                                {



                                }
                            }
                            else
                            {
                                // Alertme("Sorry! You can't add any subject this period because this period already allocated ", "warning");
                            }
                        }


                    }
                    #endregion

                    #endregion
                }
                else    ////Break
                {
                    #region BreakPeriod

                    int kSD = 0;
                    //DAY
                    int growcountSD = rp_day.Items.Count;
                    for (int iSD = 0; iSD < growcountSD; iSD++)
                    {
                        CheckBox chkS = (CheckBox)rp_day.Items[iSD].FindControl("rowChkBox1");
                        if (chkS.Checked == true)
                        {
                            Label lbl_Day = (Label)rp_day.Items[iSD].FindControl("lbl_Day");
                            kSD++;
                        }
                    }
                    if (kSD == 0)
                    {
                        Alertme("Please choose day.", "warning");
                        return;
                    }

                    #region   DayGridS
                    int growcountS = rp_day.Items.Count;
                    int kS = 0;
                    for (int iS = 0; iS < growcountS; iS++)
                    {
                        CheckBox chkS = (CheckBox)rp_day.Items[iS].FindControl("rowChkBox1");
                        if (chkS.Checked == true)
                        {
                            Label lbl_Day = (Label)rp_day.Items[iS].FindControl("lbl_Day");
                            #region add add
                            if (btn_Submit.Text == "Add")
                            {
                                ViewState["pageq"] = "1";

                                if (mycode.IsUserExist("select * from Class_Routine_Master where Session_id=" + ViewState["Sessionid"].ToString() + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Class_period='" + ddl_period.SelectedValue + "' and Day='" + lbl_Day.Text + "'"))
                                {
                                    SqlCommand cmd;
                                    string query = "INSERT INTO Class_Routine_Master (Session_id,Class_id,Section,Subject_id,Class_period,Day,Created_By,Date,Idate,time,Period_type) values (@Session_id,@Class_id,@Section,@Subject_id,@Class_period,@Day,@Created_By,@Date,@Idate,@time,@Period_type)";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                                    cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                                    cmd.Parameters.AddWithValue("@Subject_id", "");
                                    cmd.Parameters.AddWithValue("@Class_period", ddl_period.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Day", lbl_Day.Text);
                                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                                    cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                                    cmd.Parameters.AddWithValue("@time", mycode.time());
                                    cmd.Parameters.AddWithValue("@Period_type", "Break");
                                    if (InsertUpdate.InsertUpdateData(cmd))
                                    {
                                    }
                                }
                                else
                                {
                                    map_subject(lbl_Day.Text);
                                }
                            }
                            kS++;
                            #endregion
                        }
                    }

                    if (kS == 0)
                    {
                        Alertme("Please choose day.", "warning");
                    }
                    #endregion


                    #region update
                    if (btn_Submit.Text == "Update")
                    {

                        ViewState["pageq"] = "2";
                        bool chk_add_or_not = get_status_add_or_not_at_the_time_of_updateion();
                        if (chk_add_or_not == false)
                        {
                            Alertme("Sorry! You can't add this subject, because already added this subject same period.", "warning");
                        }
                        else
                        {
                            if (check_update_time())
                            {
                                SqlCommand cmd;
                                string query = "Update Class_Routine_Master set Session_id=@Session_id,Class_id=@Class_id,Section=@Section,Subject_id=@Subject_id,Class_period=@Class_period,Day=@Day,Updated_by=@Updated_by,Updated_Date=@Updated_Date,Updated_Idate=@Updated_Idate,Updated_Time=@Updated_Time,Send_Status=@Send_Status where Id = @Id";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                                cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                                cmd.Parameters.AddWithValue("@Subject_id", "");
                                cmd.Parameters.AddWithValue("@Class_period", ddl_period.SelectedValue);
                                cmd.Parameters.AddWithValue("@Day", ddl_day.Text);
                                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Updated_Date", mycode.date());
                                cmd.Parameters.AddWithValue("@Updated_Idate", mycode.idate());
                                cmd.Parameters.AddWithValue("@Updated_Time", mycode.time());
                                cmd.Parameters.AddWithValue("@Id", hd_id.Value);

                                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                                if (InsertUpdate.InsertUpdateData(cmd))
                                {

                                }
                            }
                            else
                            {
                                // Alertme("Sorry! You can't add any subject this period because this period already allocated ", "warning");
                            }
                        }


                    }
                    #endregion

                    #endregion
                }


                if (ViewState["pageq"].ToString() == "1")
                {
                    Alertme("Routine has been saved successfully", "success");
                    empty_data();
                }
                else if (ViewState["pageq"].ToString() == "2")
                {
                    Alertme("Routine has been updated successfully", "success");
                    empty_data();
                }
            }
        }

        private void map_subject(string day)
        {
            int SubrowcountS = rp_subjects.Items.Count;
            for (int iSub = 0; iSub < SubrowcountS; iSub++)
            {
                CheckBox chkSub = (CheckBox)rp_subjects.Items[iSub].FindControl("rowChkBox1");
                if (chkSub.Checked == true)
                {
                    Label lbl_subject_name = (Label)rp_subjects.Items[iSub].FindControl("lbl_subject_name");
                    Label lbl_subj_id = (Label)rp_subjects.Items[iSub].FindControl("lbl_subj_id");

                    if (mycode.IsUserExist("select * from Class_routine_period_subject_mapping where Session_id=" + ViewState["Sessionid"].ToString() + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Class_period='" + ddl_period.SelectedValue + "' and Day='" + day + "' and Subject_id=" + lbl_subj_id.Text + ""))
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Class_routine_period_subject_mapping (Session_id,Class_id,Section,Subject_id,Class_period,Day,Created_By,Date,Idate,time) values (@Session_id,@Class_id,@Section,@Subject_id,@Class_period,@Day,@Created_By,@Date,@Idate,@time)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                        cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Subject_id", lbl_subj_id.Text);
                        cmd.Parameters.AddWithValue("@Class_period", ddl_period.SelectedValue);
                        cmd.Parameters.AddWithValue("@Day", day);
                        cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@time", mycode.time());
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {



                        }
                    }
                }
            }
        }



        private bool get_status_add_or_not_at_the_time_of_updateion()
        {
            string query = "Select *  from Class_Routine_Master where Session_id=" + ViewState["Sessionid"].ToString() + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Subject_id='' and Class_period=" + ddl_period.SelectedValue + " and Day='" + ddl_day.Text + "' and Id!=" + hd_id.Value + "";

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
        private bool check_update_time()
        {
            string query = "Select *  from Class_Routine_Master where Session_id=" + ViewState["Sessionid"].ToString() + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Class_period=" + ddl_period.SelectedValue + " and Day='" + ddl_day.Text + "' and Id!=" + hd_id.Value + "";

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
            try
            {
                grid111.Visible = false;
                daygrid.Visible = true;
                ddl_day.Visible = false;
                Bind_grid_data();

                btn_Submit.Text = "Add";
                ddl_period.Text = "0";

                //ddl_subject.SelectedValue = "0";

            }
            catch
            {
            }
        }

        private void Bind_grid_data()
        {
            string query = "Select crm.*, cm.Course_Name as Classname,(select top 1 Subject_name from Subject_Master where course_id=crm.Class_id  and Subject_id=crm.Subject_id) as subjectname,(select top 1 Session from session_details where session_id=crm.Session_id) as Session,(Select top 1 Period_Name from Class_Routine_period where Period=crm.Class_period and Session_id=crm.Session_id) as Period_Name from Class_Routine_Master crm join Add_course_table cm on crm.Class_id=cm.course_id where crm.Session_id=" + ViewState["Sessionid"].ToString() + " and crm.Class_id=" + ddl_class.SelectedValue + " and crm.Section='" + ddl_section.Text + "' ";
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


        private bool get_status_add_or_not_at_the_time_of_addtion(string Day, string SubjId)
        {
            string query = "Select *  from Class_routine_period_subject_mapping where Session_id=" + ViewState["Sessionid"].ToString() + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Subject_id=" + SubjId + " and Class_period=" + ddl_period.Text + " and Day='" + Day + "'";

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

        private bool check_time_slot(string Day)
        {
            string query = "Select *  from Class_Routine_Master where Session_id=" + ViewState["Sessionid"].ToString() + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.Text + "' and Class_period=" + ddl_period.Text + " and Day='" + Day + "'";

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

            if (ViewState["Is_Edit"].ToString() == "1")
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


                daygrid.Visible = false;
                ddl_day.Visible = true;


                ddl_class.SelectedValue = lbl_Class_id.Text;

                ddl_section.Text = lbl_section.Text;
                // mycode.bind_all_ddl_with_id(ddl_subject, "Select Subject_name,Subject_id from Subject_Master order by Subject_position");
                mycode.bind_all_ddl_with_id(ddl_period, "Select Period_Name,Period from  Class_Routine_period where course_id='" + ddl_class.SelectedValue + "' and Session_id=" + ViewState["Sessionid"].ToString() + "  order by Period asc");

                //ddl_subject.SelectedValue = lbl_Subject_id.Text;
                ddl_day.Text = lbl_Day.Text;
                ddl_period.Text = lbl_Class_period.Text;

                btn_Submit.Text = "Update";
                Bind_grid_data();
                grid111.Visible = true;
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }

        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label Id = (Label)row.FindControl("lbl_Id");

                    Label lbl_Session_id = (Label)row.FindControl("lbl_Session_id");
                    Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                    Label lbl_Day = (Label)row.FindControl("lbl_Day");
                    Label lbl_Class_period_id = (Label)row.FindControl("lbl_Class_period");
                    Label lbl_section = (Label)row.FindControl("lbl_section");

                    mycode.executequery("delete from Class_Routine_Master where Id=" + Id.Text + "; delete from Class_routine_period_subject_mapping where Session_id='" + lbl_Session_id.Text + "' and Class_id='" + lbl_Class_id.Text + "' and Section='" + lbl_section.Text + "' and Class_period='" + lbl_Class_period_id.Text + "' and Day='" + lbl_Day.Text + "'; delete from Class_routine_period_subject_mapping where Session_id='" + lbl_Session_id.Text + "' and Class_id='" + lbl_Class_id.Text + "' and Section='" + lbl_section.Text + "' and Class_period='" + lbl_Class_period_id.Text + "' and Day='" + lbl_Day.Text + "'");
                    Alertme("Deletion process has been completed", "success");
                    empty_data();
                    hd_id.Value = "0";
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch { }
        }
        #endregion

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_subject = ((Label)e.Item.FindControl("lbl_subject")) as Label;
                Label lbl_Session_id = ((Label)e.Item.FindControl("lbl_Session_id")) as Label;
                Label lbl_Class_id = ((Label)e.Item.FindControl("lbl_Class_id")) as Label;
                Label lbl_Day = ((Label)e.Item.FindControl("lbl_Day")) as Label;
                Label lbl_Class_period_id = ((Label)e.Item.FindControl("lbl_Class_period")) as Label;
                Label lbl_section = ((Label)e.Item.FindControl("lbl_section")) as Label;
                Label lbl_period_type = ((Label)e.Item.FindControl("lbl_period_type")) as Label;



                string subject = "";
                if (lbl_period_type.Text == "Class")
                {
                    string query = "select *,(select top 1 Subject_name from Subject_Master where course_id=Class_routine_period_subject_mapping.Class_id  and Subject_id=Class_routine_period_subject_mapping.Subject_id) as subjectname from Class_routine_period_subject_mapping where Session_id='" + lbl_Session_id.Text + "' and Class_id='" + lbl_Class_id.Text + "' and Section='" + lbl_section.Text + "' and Class_period='" + lbl_Class_period_id.Text + "' and Day='" + lbl_Day.Text + "'";
                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            subject = subject + dr["subjectname"].ToString() + ",";
                        }
                        subject = subject.Remove(subject.Length - 1);
                    }
                }
                else
                {
                    subject = "Break";
                }
                lbl_subject.Text = subject;
            }
        }

        protected void ddl_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = mycode.FillData("select Period_type from Class_Routine_period_Master where Class_id=" + ddl_class.SelectedValue + " and Period='" + ddl_period.SelectedValue + "' ");
            if (dt.Rows.Count > 0)
            {
                ViewState["PriodTpe"] = dt.Rows[0]["Period_type"].ToString();
                txt_period_type.Text = dt.Rows[0]["Period_type"].ToString();
                if (dt.Rows[0]["Period_type"].ToString() == "Class")
                {
                    subjectdV.Visible = true;
                }
                else
                {
                    subjectdV.Visible = false;
                }
            }
        }
    }
}