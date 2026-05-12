using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using MKB.TimePicker;
using System.Globalization;
namespace school_web.Examination_Admin
{
    public partial class Create_Exam_Time_Table : System.Web.UI.Page
    {
        Examination em = new Examination();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
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

                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();

                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();


                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    if (Request.QueryString["Exam_Term_Id"] != null)
                    {
                        ddl_class.SelectedValue = Request.QueryString["Class_id"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_examtearm, "Select Term_Name,Exam_Term_Id from Exam_Term_Details  where Class_id=" + ddl_class.SelectedValue + " and Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["branchid"].ToString() + "' order by Sequence_No asc");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Class_Id=" + ddl_class.SelectedValue + "  and Session_Id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "'");

                        ddl_section.Text = Request.QueryString["Section"].ToString();
                        ddl_examtearm.Text = Request.QueryString["Exam_Term_Id"].ToString();
                        bind_exam();
                        ddl_exam.SelectedValue = Request.QueryString["examid"].ToString();
                        try
                        {
                            ddl_exam_shift_type.SelectedValue = Request.QueryString["Shifts"].ToString();
                        }
                        catch (Exception ex) { }
                        btn_save.Text = "Update";
                        Bind_data();
                    }
                    else
                    {
                        btn_save.Text = "Save";
                    }

                }
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


        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                ddlsession.Focus();
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                ddl_class.Focus();
                Alertme("Please select class", "warning");
            }
            else if (ddl_examtearm.SelectedItem.Text == "Select")
            {
                ddl_examtearm.Focus();
                Alertme("Please select exam term", "warning");
            }
            else if (ddl_exam.SelectedItem.Text == "Select")
            {
                ddl_exam.Focus();
                Alertme("Please select exam", "warning");
            }
            else
            {
                Bind_data();
            }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Create_Exam_Time_Table.aspx", false);
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_examtearm, "Select Term_Name,Exam_Term_Id from Exam_Term_Details  where Class_id=" + ddl_class.SelectedValue + " and Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["branchid"].ToString() + "' order by Sequence_No asc");
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Class_Id=" + ddl_class.SelectedValue + "  and Session_Id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "'");

            }

        }

        private void Bind_data()
        {
            string query = "Select * from Subject_Master where course_id=" + ddl_class.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "' order by Subject_position ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                btn_save.Visible = false;
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                btn_save.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                ddlsession.Focus();
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                ddl_class.Focus();
                Alertme("Please select class", "warning");
            }
            else if (ddl_examtearm.SelectedItem.Text == "Select")
            {
                ddl_examtearm.Focus();
                Alertme("Please select exam term", "warning");
            }
            else if (ddl_exam.SelectedItem.Text == "Select")
            {
                ddl_exam.Focus();
                Alertme("Please select exam", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                ddl_section.Focus();
                Alertme("Please select section", "warning");
            }
            else
            {
                if (ddl_section.Text == "ALL")
                {
                    SqlCommand cmd1 = new SqlCommand("Select distinct Section from admission_registor where Class_Id=" + ddl_class.SelectedValue + "  and Session_Id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "'");
                    DataTable dt = mycode.GetData(cmd1);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string section = dt.Rows[i]["Section"].ToString();
                            My.exeSql("delete from Exam_Time_Table where Session_Id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ddl_examtearm.SelectedValue + "' and Section='" + section + "' and Exam_id='" + ddl_exam.SelectedValue + "'");
                            save_or_update(ddl_class.SelectedValue, ddl_examtearm.SelectedValue, section, ddl_exam.SelectedValue);
                        }
                        Alertme("Exam Time table has been saved successfully", "success");
                        btn_save.Visible = false;
                        GrdView.DataSource = null;
                        GrdView.DataBind();
                    }
                }
                else
                {
                    My.exeSql("delete from Exam_Time_Table where Session_Id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ddl_examtearm.SelectedValue + "' and Section='" + ddl_section.Text + "' and Exam_id='" + ddl_exam.SelectedValue + "'");
                    save_or_update(ddl_class.SelectedValue, ddl_examtearm.SelectedValue, ddl_section.Text, ddl_exam.SelectedValue);
                    Alertme("Exam Time table has been saved successfully", "success");
                    btn_save.Visible = false;
                    GrdView.DataSource = null;
                    GrdView.DataBind();
                }
            }
        }

        private void save_or_update(string classid, string examtermid, string section, string exam_id)
        {
            for (var i = 0; i < GrdView.Rows.Count; i++)
            { 
                try
                {
                    Label Subject_id = (Label)GrdView.Rows[i].FindControl("Subject_id");
                    TextBox txt_Examination_Date = (TextBox)GrdView.Rows[i].FindControl("txt_Examination_Date");

                    DropDownList ddl_hours = (DropDownList)GrdView.Rows[i].FindControl("ddl_hours");
                    DropDownList ddl_minutes = (DropDownList)GrdView.Rows[i].FindControl("ddl_minutes");
                    DropDownList ddl_am_pm = (DropDownList)GrdView.Rows[i].FindControl("ddl_am_pm");

                    //===============
                    DropDownList ddl_hours_e = (DropDownList)GrdView.Rows[i].FindControl("ddl_hours_e");
                    DropDownList ddl_minutes_e = (DropDownList)GrdView.Rows[i].FindControl("ddl_minutes_e");
                    DropDownList ddl_am_pm_e = (DropDownList)GrdView.Rows[i].FindControl("ddl_am_pm_e");

                    DropDownList ddl_shift = (DropDownList)GrdView.Rows[i].FindControl("ddl_shift");
                    if (txt_Examination_Date.Text == "")
                    {



                    }
                    else
                    {
                        string customdate = txt_Examination_Date.Text + " " + ddl_hours.Text + ":" + ddl_minutes.Text + ":00" + " " + ddl_am_pm.Text;   // time_StartTime.Date.ToString("hh:mm:ss tt");
                        //  TimeSelector time_StartTime = (TimeSelector)GrdView.Rows[i].FindControl("time_StartTime");
                        // string mergeStartTime = txt_Examination_Date.Text + " " + time_StartTime.Date.ToString("hh:mm:ss tt");
                        string time = ddl_hours.Text + ":" + ddl_minutes.Text + ":00" + " " + ddl_am_pm.Text;
                        DateTime startTime = DateTime.ParseExact(customdate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);



                        string customdateE = txt_Examination_Date.Text + " " + ddl_hours_e.Text + ":" + ddl_minutes_e.Text + ":00" + " " + ddl_am_pm_e.Text;   // time_StartTime.Date.ToString("hh:mm:ss tt");
                        DateTime startTimeE = DateTime.ParseExact(customdateE, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        string Etime = ddl_hours_e.Text + ":" + ddl_minutes_e.Text + ":00" + " " + ddl_am_pm_e.Text;


                        final_save_or_update(classid, examtermid, section, Subject_id.Text, txt_Examination_Date.Text, startTime, time, Etime, ddl_shift.SelectedValue, startTimeE, exam_id);
                    }

                }
                catch
                {
                }
            }
        }

        private void final_save_or_update(string classid, string examtermid, string section, string Subject_id, string Examination_Date, DateTime datetime, string time, string Etime, string shift, DateTime startTimeE, string exam_id)
        {
            SqlCommand cmd = new SqlCommand("Select * from Exam_Time_Table where Session_Id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "'  and Class_id=" + classid + " and Exam_Term_Id=" + examtermid + " and Section='" + section + "' and Exam_id='" + exam_id + "' and Subject_id='"+ Subject_id + "'");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Exam_Time_Table (Session_Id,Branch_id,Class_id,Section,Subject_id,Exam_Date,Exam_Time,Exam_Date_time,Created_by,Created_datetime,Exam_Term_Id,Day,Shift_type,Exam_end_time,Shift,Exam_E_Date_time,Exam_id) values (@Session_Id,@Branch_id,@Class_id,@Section,@Subject_id,@Exam_Date,@Exam_Time,@Exam_Date_time,@Created_by,@Created_datetime,@Exam_Term_Id,@Day,@Shift_type,@Exam_end_time,@Shift,@Exam_E_Date_time,@Exam_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_Id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", classid);
                cmd.Parameters.AddWithValue("@Section", section);
                cmd.Parameters.AddWithValue("@Subject_id", Subject_id);
                cmd.Parameters.AddWithValue("@Exam_Date", Examination_Date);
                cmd.Parameters.AddWithValue("@Exam_Time", time);
                cmd.Parameters.AddWithValue("@Exam_Date_time", datetime);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_datetime", My.getdate1());
                cmd.Parameters.AddWithValue("@Exam_Term_Id", examtermid);
                cmd.Parameters.AddWithValue("@Day", mycode.getdayname(Examination_Date));
                cmd.Parameters.AddWithValue("@Shift_type", ddl_exam_shift_type.SelectedValue);
                cmd.Parameters.AddWithValue("@Exam_end_time", Etime);
                if (ddl_exam_shift_type.SelectedValue == "1")
                {
                    cmd.Parameters.AddWithValue("@Shift", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Shift", shift);
                }
                cmd.Parameters.AddWithValue("@Exam_E_Date_time", startTimeE);
                cmd.Parameters.AddWithValue("@Exam_id", exam_id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string query = "update Exam_Time_Table set Exam_Date=@Exam_Date,Exam_Time=@Exam_Time,Exam_Date_time=@Exam_Date_time ,Updated_by=@Updated_by,Updated_date_time=@Updated_date_time,Day=@Day,Shift_type=@Shift_type,Exam_end_time=@Exam_end_time,Shift=@Shift,Exam_E_Date_time=@Exam_E_Date_time where Id=@id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Exam_Date", Examination_Date);
                cmd.Parameters.AddWithValue("@Exam_Time", time);
                cmd.Parameters.AddWithValue("@Exam_Date_time", datetime);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Day", mycode.getdayname(Examination_Date));
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Shift_type", ddl_exam_shift_type.SelectedValue);
                cmd.Parameters.AddWithValue("@Exam_end_time", Etime);
                if (ddl_exam_shift_type.SelectedValue == "1")
                {
                    cmd.Parameters.AddWithValue("@Shift", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Shift", shift);
                }
                cmd.Parameters.AddWithValue("@Exam_E_Date_time", startTimeE);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }


        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt_Examination_Date = (TextBox)e.Row.FindControl("txt_Examination_Date");
                Label Subject_id = (Label)e.Row.FindControl("Subject_id");
                //TimeSelector time_StartTime = (TimeSelector)e.Row.FindControl("time_StartTime");
                DropDownList ddl_hours = (DropDownList)e.Row.FindControl("ddl_hours");
                DropDownList ddl_minutes = (DropDownList)e.Row.FindControl("ddl_minutes");
                DropDownList ddl_am_pm = (DropDownList)e.Row.FindControl("ddl_am_pm");
                DropDownList ddl_shift = (DropDownList)e.Row.FindControl("ddl_shift");
                if (ddl_exam_shift_type.SelectedValue == "1")
                {
                    ddl_shift.Enabled = false;
                }
                else
                {
                    ddl_shift.Enabled = true;
                }

                DropDownList ddl_hours_e = (DropDownList)e.Row.FindControl("ddl_hours_e");
                DropDownList ddl_minutes_e = (DropDownList)e.Row.FindControl("ddl_minutes_e");
                DropDownList ddl_am_pm_e = (DropDownList)e.Row.FindControl("ddl_am_pm_e");

                bind_data_if_added(Subject_id.Text, txt_Examination_Date, ddl_hours, ddl_minutes, ddl_am_pm, ddl_hours_e, ddl_minutes_e, ddl_am_pm_e, ddl_shift);

            }
        }

        private void bind_data_if_added(string subjectid, TextBox lbl_Examination_Date, DropDownList ddl_hours, DropDownList ddl_minutes, DropDownList ddl_am_pm, DropDownList ddl_hours_e, DropDownList ddl_minutes_e, DropDownList ddl_am_pm_e, DropDownList ddl_shift)
        {
            string section = "";
            if (ddl_section.Text == "ALL")
            {
                section = "A";
            }
            else
            {
                section = ddl_section.Text;
            }
            SqlCommand cmd = new SqlCommand("Select *,format(Exam_Date_time, 'dd/MM/yyyy') as Exam_datetime2,format(Exam_Date_time, 'dd/MM/yyyy hh:mm:ss tt') as Exam_datetime1,format(Exam_Date_time, 'hh:mm') as time1,format(Exam_Date_time, 'tt') as ampm,format(Exam_E_Date_time, 'hh:mm') as time2E,format(Exam_E_Date_time, 'tt') as ampmE from Exam_Time_Table where Session_Id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "'  and Class_id=" + ddl_class.SelectedValue + " and Exam_Term_Id=" + ddl_examtearm.Text + " and Section='" + section + "' and Subject_id=" + subjectid + " and Exam_id='" + ddl_exam.SelectedValue + "' ");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {

                lbl_Examination_Date.Text = dt.Rows[0]["Exam_datetime2"].ToString();
                string Exam_datetime1 = dt.Rows[0]["Exam_datetime1"].ToString();
                string ampm = dt.Rows[0]["ampm"].ToString();
                string time = dt.Rows[0]["time1"].ToString();

                string[] stringSeparators = new string[] { ":" };
                string[] arr = time.Split(stringSeparators, StringSplitOptions.None);
                string hours = arr[0];
                string minutes = arr[1];

                ddl_hours.Text = hours;
                ddl_minutes.Text = minutes;
                ddl_am_pm.Text = ampm;



                //======================
                string hoursE = ""; string minutesE = ""; string ampmE = "";
                try
                {
                    ampmE = dt.Rows[0]["ampmE"].ToString();
                    string timeE = dt.Rows[0]["time2E"].ToString();
                    string[] stringSeparatorsE = new string[] { ":" };
                    string[] arrE = timeE.Split(stringSeparatorsE, StringSplitOptions.None);
                    hoursE = arrE[0];
                    minutesE = arrE[1];
                }
                catch (Exception ex)
                { }

                try
                {
                    ddl_hours_e.Text = hoursE;
                }
                catch (Exception ex)
                { }
                try
                {
                    ddl_minutes_e.Text = minutesE;
                }
                catch (Exception ex)
                { }
                try
                {
                    ddl_am_pm_e.Text = ampmE;
                }
                catch (Exception ex)
                { }
                try
                {
                    ddl_shift.SelectedValue = dt.Rows[0]["Shift"].ToString();
                }
                catch (Exception ex)
                { }


                //MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                //DateTime startTime = DateTime.ParseExact(Exam_datetime1, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                //string am1 = startTime.ToString("tt");
                //string hh = startTime.ToString("hh");
                //string mm = startTime.ToString("mm");
                //string ss = startTime.ToString("ss");
                //if (am1 == "AM")
                //{
                //    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                //}
                //else
                //{
                //    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                //}
                //time_StartTime.SetTime(Convert.ToInt32(hh), Convert.ToInt32(mm), Convert.ToInt32(ss), am_pm);


            }
        }

        protected void ddl_exam_shift_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_examtearm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_exam();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_exam()
        {
            mycode.bind_all_ddl_with_id(ddl_exam, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 and Exam_Term_Id='" + ddl_examtearm.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' order by Sequence_No asc");
        }
    }
}