using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class online_reg_admit_card_creation : System.Web.UI.Page
    {
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
                    try
                    {
                        const string quote = "\"";
                        string tinyMC = My.get_single_column_data("select TinyMC_link as Column_Name from Firm_Details");
                        if (tinyMC != "")
                        {
                            lt_meata.Text = "<script src=" + quote + tinyMC + quote + " referrerpolicy=" + quote + "origin" + quote + " ></script>";
                        }
                        else
                        {
                            lt_meata.Text = "<script src=" + quote + "https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/7/tinymce.min.js" + quote + " referrerpolicy=" + quote + "origin" + quote + "></script>";
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    if (Session["SmsGS"] != null)
                    {
                        Alertme(Session["SmsGS"].ToString(), "success");
                        Session["SmsGS"] = null;
                    }
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["sessionid"] = My.get_session_id_onlinereg();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details order by Session asc");
                    ddl_session.SelectedValue = My.get_session_id_onlinereg();
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    mycode.bind_all_ddl_with_id(ddl_room_no, "select Room_name,Room_id from Online_admit_card_room order by Room_name asc");
                    
                    fetch_exam();

                    ddl_class.SelectedValue = My.get_top_one_class();
                    txt_exam_date.Text = mycode.date();
                    My.bind_ddl_select(ddl_shift, "select Shift_name from Online_reg_shift_master where Status=1");
                    if (Request.QueryString["Regid"] != null)
                    {
                        ViewState["regid"] = Request.QueryString["Regid"];
                        // Bind_reg_id_data();
                    }
                }
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    ddl_session.Focus();
                    Alertme("Please select session", "warning");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    ddl_class.Focus();
                    Alertme("Please select class", "warning");
                }
                else if (ddl_test_name.SelectedItem.Text == "Select")
                {
                    ddl_test_name.Focus();
                    Alertme("Please select testname", "warning");
                }
                else if (ddl_room_no.SelectedItem.Text == "Select")
                {
                    ddl_room_no.Focus();
                    Alertme("Please select room.", "warning");
                }
                else if (txt_exam_date.Text == "")
                {
                    txt_exam_date.Focus();
                    Alertme("Please select exam date.", "warning");
                }
                else
                {
                    ViewState["SaveStatus"] = "0";
                    save_data();
                    if (ViewState["SaveStatus"].ToString() == "1")
                    {
                        Session["SmsGS"] = "Admit card has been created successfully.";
                        Response.Redirect("online-reg-admit-card-creation.aspx", false);
                    }
                    else
                    {
                        Alertme("Please check student list.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_data()
        {
            My.exeSql("delete from Online_Reg_Exam_Time_Table where Session_Id=" + ddl_session.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "'  and Class_id=" + ddl_class.SelectedValue + " and Test_id='" + ddl_test_name.SelectedValue + "' and Room_id='" + ddl_room_no.SelectedValue + "' and Shift='" + ddl_shift.Text + "'");
            for (var i = 0; i < GrdView.Rows.Count; i++)
            {
                Label lbl_Registration_id = (Label)GrdView.Rows[i].FindControl("lbl_Registration_id");
                CheckBox rowChkBox = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                if (rowChkBox.Checked == true)
                {
                    string customdate = txt_exam_date.Text + " " + ddl_hours.Text + ":" + ddl_minutes.Text + ":00" + " " + ddl_am_pm.Text;   //  
                    string time = ddl_hours.Text + ":" + ddl_minutes.Text + ":00" + " " + ddl_am_pm.Text;

                    //=============
                    string customdate_e = txt_exam_date.Text + " " + ddl_hours_e.Text + ":" + ddl_minutes_e.Text + ":00" + " " + ddl_am_pm_e.Text;   //  
                    string time_e = ddl_hours_e.Text + ":" + ddl_minutes_e.Text + ":00" + " " + ddl_am_pm_e.Text;

                    DateTime startTime = DateTime.ParseExact(customdate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime endTime = DateTime.ParseExact(customdate_e, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    final_save_or_update(ddl_class.SelectedValue, ddl_session.SelectedValue, txt_exam_date.Text, startTime, time, lbl_Registration_id.Text, ddl_exam_type.Text, txt_info.Value, ddl_room_no.SelectedValue, endTime, time_e);
                    ViewState["SaveStatus"] = "1";
                }
            }
        }

        private void final_save_or_update(string classid, string sessionid, string Examination_Date, DateTime datetime, string time, string Registration_id, string examtype, string remarks, string ddl_rooms, DateTime endDateTime, string time_e)
        {
            SqlCommand cmd = new SqlCommand("Select * from Online_Reg_Exam_Time_Table where Session_Id=" + sessionid + " and Branch_id='" + ViewState["branchid"].ToString() + "'  and Class_id=" + classid + " and Admission_no='" + Registration_id + "' and Test_id='" + ddl_test_name.SelectedValue + "' and Room_id='" + ddl_rooms + "' and Shift='" + ddl_shift.Text + "'");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Online_Reg_Exam_Time_Table (Session_Id,Branch_id,Class_id,Exam_Date,Exam_Time,Exam_Date_time,Created_by,Created_datetime,Day,Admission_no,Exam_Type,Remarks,Room_id,Exam_end_time,Exam_end_date_time,Is_published,Test_id,Shift) values (@Session_Id,@Branch_id,@Class_id,@Exam_Date,@Exam_Time,@Exam_Date_time,@Created_by,@Created_datetime,@Day,@Admission_no,@Exam_Type,@Remarks,@Room_id,@Exam_end_time,@Exam_end_date_time,@Is_published,@Test_id,@Shift)";
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
                cmd.Parameters.AddWithValue("@Test_id", ddl_test_name.SelectedValue);
                cmd.Parameters.AddWithValue("@Shift", ddl_shift.Text);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string query = "Update Online_Reg_Exam_Time_Table set Exam_Date=@Exam_Date,Exam_Time=@Exam_Time,Exam_Date_time=@Exam_Date_time,Updated_by=@Updated_by,Updated_date_time=@Updated_date_time,Day=@Day,Exam_Type=@Exam_Type,Remarks=@Remarks,Room_id=@Room_id,Exam_end_time=@Exam_end_time,Exam_end_date_time=@Exam_end_date_time,Is_published=@Is_published,Test_id=@Test_id,Shift=@Shift where Id = @Id";
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
                cmd.Parameters.AddWithValue("@Test_id", ddl_test_name.SelectedValue);
                cmd.Parameters.AddWithValue("@Shift", ddl_shift.Text);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }



        private void fetch_added_data()
        {
            string query = "Select *,'0' as AddedStatusS,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=Online_Admission.Class_id) as Course_Name from Online_Admission where Class_id=" + ddl_class.SelectedValue + " and Session_id=" + ddl_session.SelectedValue + " and Payment_Status='Paid' and Test_id='" + ddl_test_name.SelectedValue + "' and Registration_id not in(select Admission_no from Online_Reg_Exam_Time_Table where Admission_no=Online_Admission.Registration_id and Session_Id=Online_Admission.Session_id and Class_id=Online_Admission.Class_id and Test_id=Online_Admission.Test_id) order by Name";
            DataTable dtCheck = My.dataTable("select top 1 * from Online_Reg_Exam_Time_Table where Session_Id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Room_id='" + ddl_room_no.SelectedValue + "' and Test_id='" + ddl_test_name.SelectedValue + "'  and Shift='" + ddl_shift.Text + "'");
            if (dtCheck.Rows.Count > 0)
            {
                query = "Select *,'1' as AddedStatusS,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=Online_Admission.Class_id) as Course_Name from Online_Admission where Class_id=" + ddl_class.SelectedValue + " and Session_id=" + ddl_session.SelectedValue + " and Payment_Status='Paid' and Test_id='" + ddl_test_name.SelectedValue + "' and Registration_id in(select Admission_no from Online_Reg_Exam_Time_Table where Admission_no=Online_Admission.Registration_id and Session_Id=Online_Admission.Session_id and Class_id=Online_Admission.Class_id and Test_id=Online_Admission.Test_id and Room_id='" + ddl_room_no.SelectedValue + "' and Shift='" + ddl_shift.Text + "')  UNION all Select *,'0' as AddedStatusS,(select top 1 Session from session_details where session_id = Online_Admission.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id = Online_Admission.Class_id) as Course_Name from Online_Admission where Class_id = " + ddl_class.SelectedValue + " and Session_id = " + ddl_session.SelectedValue + " and Payment_Status = 'Paid' and Test_id = '" + ddl_test_name.SelectedValue + "' and Registration_id not in(select Admission_no from Online_Reg_Exam_Time_Table where Admission_no = Online_Admission.Registration_id and Session_Id = Online_Admission.Session_id and Class_id = Online_Admission.Class_id and Test_id = Online_Admission.Test_id) order by Name";
            }

            DataTable dtstd = My.dataTable(query);
            if (dtstd.Rows.Count == 0)
            {
                btn_add.Visible = false;
                grdDVS.Visible = false;
                Alertme("Sorry there are no student list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                DataTable dt = My.dataTable("select top 1 *,format(Exam_Date_time, 'dd/MM/yyyy') as Exam_datetime2,format(Exam_Date_time, 'dd/MM/yyyy hh:mm:ss tt') as Exam_datetime1,format(Exam_Date_time, 'hh:mm') as time1,format(Exam_Date_time, 'tt') as ampm , format(Exam_end_date_time, 'dd/MM/yyyy') as Exam_datetime22,format(Exam_end_date_time, 'dd/MM/yyyy hh:mm:ss tt') as Exam_datetime11,format(Exam_end_date_time, 'hh:mm') as time11,format(Exam_end_date_time, 'tt') as ampm22 from Online_Reg_Exam_Time_Table where Session_Id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Room_id='" + ddl_room_no.SelectedValue + "' and Test_id='" + ddl_test_name.SelectedValue + "' and Shift='" + ddl_shift.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    txt_info.Value = dt.Rows[0]["Remarks"].ToString();
                    try
                    {
                        ddl_exam_type.Text = dt.Rows[0]["Exam_Type"].ToString();
                    }
                    catch
                    {
                    }

                    txt_exam_date.Text = dt.Rows[0]["Exam_datetime2"].ToString();
                    txt_exam_date.Text = dt.Rows[0]["Exam_datetime2"].ToString();
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

                    // ===========================
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
                }
                btn_add.Visible = true;
                grdDVS.Visible = true;
                GrdView.DataSource = dtstd;
                GrdView.DataBind();
            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox rowChkBox = (CheckBox)e.Row.FindControl("rowChkBox");
                Label lbl_addedStatusS = (Label)e.Row.FindControl("lbl_addedStatusS");
                if (lbl_addedStatusS.Text == "1")
                {
                    rowChkBox.Checked = true;
                }
                else
                {
                    rowChkBox.Checked = false;
                }
            }
        }

        protected void ddl_shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    ddl_shift.Text = "Select";
                    ddl_session.Focus();
                    Alertme("Please select session", "warning");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    ddl_shift.Text = "Select";
                    ddl_class.Focus();
                    Alertme("Please select class", "warning");
                }
                else if (ddl_test_name.SelectedItem.Text == "Select")
                {
                    ddl_shift.Text = "Select";
                    ddl_test_name.Focus();
                    Alertme("Please select testname", "warning");
                }
                else if (ddl_room_no.SelectedItem.Text == "Select")
                {
                    ddl_shift.Text = "Select";
                    ddl_room_no.Focus();
                    Alertme("Please select room.", "warning");
                }
                else if (ddl_shift.SelectedItem.Text == "Select")
                {
                    ddl_shift.Text = "Select";
                    ddl_shift.Focus();
                    Alertme("Please select shift.", "warning");
                }
                else
                {
                    fetch_added_data();
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




