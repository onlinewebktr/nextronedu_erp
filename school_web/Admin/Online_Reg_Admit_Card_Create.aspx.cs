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

namespace school_web.Admin
{
    public partial class Online_Reg_Admit_Card_Note : System.Web.UI.Page
    {
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
                    ViewState["FindWithAuto"] = "0";
                    ViewState["Userid"] = Session["Admin"].ToString();
                    string pagename_current = "online-reg-admit-card-creation.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];




                    ViewState["sessionid"] = My.get_session_id_onlinereg();

                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                    mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details order by Session asc");
                    ddl_session.SelectedValue = My.get_session_id_onlinereg();



                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");

                    mycode.bind_all_ddl_with_id(ddl_session_auto, "Select  Session,session_id from session_details order by Session asc");
                    ddl_session_auto.SelectedValue = My.get_session_id_onlinereg();

                    mycode.bind_all_ddl_with_id(ddl_class_auto, "Select Course_Name,course_id from Add_course_table order by Position asc");

                    mycode.bind_all_ddl_with_id(ddl_room_no_auto, "select Room_name,Room_id from Online_admit_card_room order by Room_name asc");
                    fetch_exam();



                    mycode.bind_all_ddl_with_id(ddl_exam_name_auto, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddl_session.SelectedValue + "' and Is_active=1   order by  Test_name asc");

                    ddl_class.SelectedValue = My.get_top_one_class();
                    ddl_class_auto.SelectedValue = My.get_top_one_class();

                    txt_exam_date.Text = mycode.date();
                    txt_exam_date_auto.Text = mycode.date();

                    if (Request.QueryString["Regid"] != null)
                    {
                        ViewState["regid"] = Request.QueryString["Regid"];
                        Bind_reg_id_data();
                    }
                }
            }
        }

        private void Bind_reg_id_data()
        {
            string query = "Select oa.*,'0'  as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name  from Online_Admission oa join Online_Reg_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  where oa.Registration_id='" + ViewState["regid"].ToString() + "'  and oa.Payment_Status='Paid' order by oa.Name ";


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
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_test_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select testname", "warning");
            }
            else
            {
                Bind_data();
            }
        }
        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Online_Reg_Admit_Card_Note.aspx", false);
        }
        private void Bind_data()
        {
            string query = "Select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=Online_Admission.Class_id) as Course_Name  from Online_Admission where Class_id=" + ddl_class.SelectedValue + " and Session_id=" + ddl_session.SelectedValue + " and Payment_Status='Paid' and Test_id='" + ddl_test_name.SelectedValue + "' order by Name ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);

            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no any student list found", "warning");
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

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt_Examination_Date = (TextBox)e.Row.FindControl("txt_Examination_Date");
                Label lbl_Registration_id = (Label)e.Row.FindControl("lbl_Registration_id");


                Label lbl_Class_id = (Label)e.Row.FindControl("lbl_Class_id");
                Label lbl_Session_id = (Label)e.Row.FindControl("lbl_Session_id");
                TextBox txt_remarks = (TextBox)e.Row.FindControl("txt_remarks");

                DropDownList ddl_hours = (DropDownList)e.Row.FindControl("ddl_hours");
                DropDownList ddl_minutes = (DropDownList)e.Row.FindControl("ddl_minutes");
                DropDownList ddl_am_pm = (DropDownList)e.Row.FindControl("ddl_am_pm");
                DropDownList ddl_examtype = (DropDownList)e.Row.FindControl("ddl_examtype");
                DropDownList ddl_rooms = (DropDownList)e.Row.FindControl("ddl_rooms");


                DropDownList ddl_hours_e = (DropDownList)e.Row.FindControl("ddl_hours_e");
                DropDownList ddl_minutes_e = (DropDownList)e.Row.FindControl("ddl_minutes_e");
                DropDownList ddl_am_pm_e = (DropDownList)e.Row.FindControl("ddl_am_pm_e");

                mycode.bind_all_ddl_with_id(ddl_rooms, "select Room_name,Room_id from Online_admit_card_room order by Room_name asc");

                bind_data_if_added(txt_Examination_Date, ddl_hours, ddl_minutes, ddl_am_pm, lbl_Class_id.Text, lbl_Session_id.Text, ddl_examtype, lbl_Registration_id.Text, txt_remarks, ddl_rooms, ddl_hours_e, ddl_minutes_e, ddl_am_pm_e);

            }
        }

        private void bind_data_if_added(TextBox txt_Examination_Date, DropDownList ddl_hours, DropDownList ddl_minutes, DropDownList ddl_am_pm, string Class_id, string Session_id, DropDownList ddl_examtype, string Registration_id, TextBox txt_remarks, DropDownList ddl_rooms, DropDownList ddl_hours_e, DropDownList ddl_minutes_e, DropDownList ddl_am_pm_e)
        {
            if (ViewState["FindWithAuto"].ToString() == "0")
            {
                SqlCommand cmd = new SqlCommand("Select *,format(Exam_Date_time, 'dd/MM/yyyy') as Exam_datetime2,format(Exam_Date_time, 'dd/MM/yyyy hh:mm:ss tt') as Exam_datetime1,format(Exam_Date_time, 'hh:mm') as time1,format(Exam_Date_time, 'tt') as ampm , format(Exam_end_date_time, 'dd/MM/yyyy') as Exam_datetime22,format(Exam_end_date_time, 'dd/MM/yyyy hh:mm:ss tt') as Exam_datetime11,format(Exam_end_date_time, 'hh:mm') as time11,format(Exam_end_date_time, 'tt') as ampm22 from Online_Reg_Exam_Time_Table where   Branch_id='" + ViewState["branchid"].ToString() + "'  and Class_id=" + Class_id + " and Session_Id=" + Session_id + " and Admission_no='" + Registration_id + "'  ");
                DataTable dt = mycode.GetData(cmd);
                if (dt.Rows.Count == 0)
                {
                    txt_Examination_Date.Text = txt_exam_date.Text;
                    txt_remarks.Text = "";
                }
                else
                {
                    txt_remarks.Text = dt.Rows[0]["Remarks"].ToString();
                    try
                    {
                        ddl_examtype.Text = dt.Rows[0]["Exam_Type"].ToString();
                    }
                    catch
                    {
                    }

                    txt_exam_date.Text = dt.Rows[0]["Exam_datetime2"].ToString();
                    txt_Examination_Date.Text = dt.Rows[0]["Exam_datetime2"].ToString();
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

                    //===========================
                    string ampm_e = dt.Rows[0]["ampm22"].ToString();
                    string time_e = dt.Rows[0]["time11"].ToString();

                    try
                    {
                        string[] stringSeparators_e = new string[] { ":" };
                        string[] arr_e = time_e.Split(stringSeparators_e, StringSplitOptions.None);
                        string hours_e = arr_e[0];
                        string minutes_e = arr_e[1];
                        ddl_hours_e.Text = hours_e;
                        ddl_minutes_e.Text = minutes_e;
                        ddl_am_pm_e.Text = ampm_e;
                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        ddl_rooms.SelectedValue = dt.Rows[0]["Room_id"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            else
            {
                ddl_session.SelectedValue = ddl_session_auto.SelectedValue;


                ddl_class.SelectedValue = ddl_class_auto.SelectedValue;
                txt_exam_date.Text = txt_exam_date_auto.Text;
                txt_remarks.Text = txt_remarks_auto.Text;
                try
                {
                    ddl_examtype.Text = ddl_examtype_auto.Text;
                }
                catch
                {
                }

                txt_Examination_Date.Text = txt_exam_date_auto.Text;
                try
                {
                    ddl_hours.Text = ddl_s_hour_auto.Text;
                    ddl_minutes.Text = ddl_s_minut_auto.Text;
                    ddl_am_pm.Text = ddl_s_am_pm.Text;
                }
                catch (Exception ex)
                {
                }
                //===========================


                try
                {
                    ddl_hours_e.Text = ddl_e_hour_auto.Text;
                    ddl_minutes_e.Text = ddl_e_minut_auto.Text;
                    ddl_am_pm_e.Text = ddl_e_am_pm.Text;
                }
                catch (Exception ex)
                {
                }

                try
                {
                    ddl_rooms.SelectedValue = ddl_room_no_auto.SelectedValue;
                }
                catch (Exception ex)
                {
                }
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {

            if (ViewState["Is_add"].ToString() == "1")
            {

                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                }
                else
                {
                    save_or_update(ddl_class.SelectedValue, ddl_session.SelectedValue);
                    Alertme("Exam Time table has been saved successfully", "success");
                    btn_save.Visible = false;
                    btn_publish_admitcard.Visible = true;
                }
            }
            else if (ViewState["Is_add"].ToString() == "1")
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                }
                else
                {
                    save_or_update(ddl_class.SelectedValue, ddl_session.SelectedValue);
                    Alertme("Exam Time table has been saved successfully", "success");
                    btn_save.Visible = false;
                    btn_publish_admitcard.Visible = true;
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }


        private void save_or_update(string classid, string sessionid)
        {
            for (var i = 0; i < GrdView.Rows.Count; i++)
            {
                try
                {
                    Label lbl_Registration_id = (Label)GrdView.Rows[i].FindControl("lbl_Registration_id");
                    Label lbl_test_id = (Label)GrdView.Rows[i].FindControl("lbl_test_id");
                    TextBox txt_Examination_Date = (TextBox)GrdView.Rows[i].FindControl("txt_Examination_Date");
                    TextBox txt_remarks = (TextBox)GrdView.Rows[i].FindControl("txt_remarks");
                    DropDownList ddl_hours = (DropDownList)GrdView.Rows[i].FindControl("ddl_hours");
                    DropDownList ddl_minutes = (DropDownList)GrdView.Rows[i].FindControl("ddl_minutes");
                    DropDownList ddl_am_pm = (DropDownList)GrdView.Rows[i].FindControl("ddl_am_pm");
                    DropDownList ddl_examtype = (DropDownList)GrdView.Rows[i].FindControl("ddl_examtype");
                    DropDownList ddl_rooms = (DropDownList)GrdView.Rows[i].FindControl("ddl_rooms");



                    DropDownList ddl_hours_e = (DropDownList)GrdView.Rows[i].FindControl("ddl_hours_e");
                    DropDownList ddl_minutes_e = (DropDownList)GrdView.Rows[i].FindControl("ddl_minutes_e");
                    DropDownList ddl_am_pm_e = (DropDownList)GrdView.Rows[i].FindControl("ddl_am_pm_e");

                    if (txt_Examination_Date.Text == "")
                    {
                    }
                    else
                    {
                        string customdate = txt_Examination_Date.Text + " " + ddl_hours.Text + ":" + ddl_minutes.Text + ":00" + " " + ddl_am_pm.Text;   //  
                        string time = ddl_hours.Text + ":" + ddl_minutes.Text + ":00" + " " + ddl_am_pm.Text;


                        //=============
                        string customdate_e = txt_Examination_Date.Text + " " + ddl_hours_e.Text + ":" + ddl_minutes_e.Text + ":00" + " " + ddl_am_pm_e.Text;   //  
                        string time_e = ddl_hours_e.Text + ":" + ddl_minutes_e.Text + ":00" + " " + ddl_am_pm_e.Text;



                        DateTime startTime = DateTime.ParseExact(customdate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        DateTime endTime = DateTime.ParseExact(customdate_e, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                        final_save_or_update(classid, sessionid, txt_Examination_Date.Text, startTime, time, lbl_Registration_id.Text, ddl_examtype.Text, txt_remarks.Text, ddl_rooms.SelectedValue, endTime, time_e, lbl_test_id.Text);
                    }
                }
                catch
                {
                }
            }
        }

        private void final_save_or_update(string classid, string sessionid, string Examination_Date, DateTime datetime, string time, string Registration_id, string examtype, string remarks, string ddl_rooms, DateTime endDateTime, string time_e, string test_id)
        {
            SqlCommand cmd = new SqlCommand("Select * from Online_Reg_Exam_Time_Table where Session_Id=" + sessionid + " and Branch_id='" + ViewState["branchid"].ToString() + "'  and Class_id=" + classid + " and Admission_no='" + Registration_id + "' and Test_id='" + test_id + "' ");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Online_Reg_Exam_Time_Table (Session_Id,Branch_id,Class_id,Exam_Date,Exam_Time,Exam_Date_time,Created_by,Created_datetime,Day,Admission_no,Exam_Type,Remarks,Room_id,Exam_end_time,Exam_end_date_time,Is_published,Test_id) values (@Session_Id,@Branch_id,@Class_id,@Exam_Date,@Exam_Time,@Exam_Date_time,@Created_by,@Created_datetime,@Day,@Admission_no,@Exam_Type,@Remarks,@Room_id,@Exam_end_time,@Exam_end_date_time,@Is_published,@Test_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_Id", sessionid);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", classid);
                cmd.Parameters.AddWithValue("@Exam_Date", Examination_Date);
                cmd.Parameters.AddWithValue("@Exam_Time", time);
                cmd.Parameters.AddWithValue("@Exam_Date_time", datetime);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_datetime", My.getdate1());
                cmd.Parameters.AddWithValue("@Day", mycode.getdayname(Examination_Date));
                cmd.Parameters.AddWithValue("@Admission_no", Registration_id);
                cmd.Parameters.AddWithValue("@Exam_Type", examtype);
                cmd.Parameters.AddWithValue("@Remarks", remarks);
                cmd.Parameters.AddWithValue("@Room_id", ddl_rooms);

                cmd.Parameters.AddWithValue("@Exam_end_time", time_e);
                cmd.Parameters.AddWithValue("@Exam_end_date_time", endDateTime);
                cmd.Parameters.AddWithValue("@Is_published", "0");
                cmd.Parameters.AddWithValue("@Test_id", test_id);

                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string query = "Update Online_Reg_Exam_Time_Table set Exam_Date=@Exam_Date,Exam_Time=@Exam_Time,Exam_Date_time=@Exam_Date_time,Updated_by=@Updated_by,Updated_date_time=@Updated_date_time,Day=@Day,Exam_Type=@Exam_Type,Remarks=@Remarks,Room_id=@Room_id,Exam_end_time=@Exam_end_time,Exam_end_date_time=@Exam_end_date_time,Is_published=@Is_published,Test_id=@Test_id where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Exam_Date", Examination_Date);
                cmd.Parameters.AddWithValue("@Exam_Time", time);
                cmd.Parameters.AddWithValue("@Exam_Date_time", datetime);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Day", mycode.getdayname(Examination_Date));
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Exam_Type", examtype);
                cmd.Parameters.AddWithValue("@Remarks", remarks);
                cmd.Parameters.AddWithValue("@Room_id", ddl_rooms);
                cmd.Parameters.AddWithValue("@Exam_end_time", time_e);
                cmd.Parameters.AddWithValue("@Exam_end_date_time", endDateTime);
                cmd.Parameters.AddWithValue("@Is_published", "0");
                cmd.Parameters.AddWithValue("@Test_id", test_id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        protected void btn_auto_fill_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_auto.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please select session.", "warning");
                    ddl_session_auto.Focus();
                }

                else if (ddl_exam_name_auto.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please select test name", "warning");
                    ddl_exam_name_auto.Focus();
                }
                else if (ddl_class_auto.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please select session.", "warning");
                    ddl_class_auto.Focus();
                }
                else if (txt_exam_date_auto.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter exam date.", "warning");
                    txt_exam_date_auto.Focus();
                }
                else if (ddl_room_no_auto.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please select room.", "warning");
                    ddl_room_no_auto.Focus();
                }
                else
                {
                    ddl_session.SelectedValue = ddl_session_auto.SelectedValue;
                    ddl_class.SelectedValue = ddl_class_auto.SelectedValue;
                    ddl_exam_name_auto.SelectedValue = ddl_test_name.SelectedValue;
                    txt_exam_date.Text = txt_exam_date_auto.Text;
                    ViewState["FindWithAuto"] = "1";
                    find_by_auto_fill();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_auto_fill()
        {
            string query = "Select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=Online_Admission.Class_id) as Course_Name  from Online_Admission where Class_id=" + ddl_class_auto.SelectedValue + " and Session_id=" + ddl_session_auto.SelectedValue + " and Payment_Status='Paid' and Test_id=" + ddl_exam_name_auto.SelectedValue + "  order by Name ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                Alertme("Data not found.", "Warning");
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

        protected void btn_publish_admitcard_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    if (ddl_session.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select session", "warning");
                    }
                    else if (ddl_class.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select class", "warning");
                    }

                    else if (ddl_test_name.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select test name", "warning");
                    }
                    else
                    {
                        string qry = "update Online_Reg_Exam_Time_Table set Is_published=1 where Session_Id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + "";
                        mycode.executequery(qry);

                        Alertme("Admit card has been published successfully. Student can download now.", "success");
                        btn_save.Visible = false;
                        btn_publish_admitcard.Visible = false;
                        GrdView.DataSource = null;
                        GrdView.DataBind();
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    if (ddl_session.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select session", "warning");
                    }
                    else if (ddl_class.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select class", "warning");
                    }

                    else if (ddl_test_name.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select test name", "warning");
                    }
                    else
                    {
                        string qry = "update Online_Reg_Exam_Time_Table set Is_published=1 where Session_Id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + "";
                        mycode.executequery(qry);

                        Alertme("Admit card has been published successfully. Student can download now.", "success");
                        btn_save.Visible = false;
                        btn_publish_admitcard.Visible = false;
                        GrdView.DataSource = null;
                        GrdView.DataBind();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_exam();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_exam()
        {
            mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddl_session.SelectedValue + "'   order by  Test_name asc");
        }
    }
}