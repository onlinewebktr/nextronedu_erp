using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Online_Reg_Create_Result : System.Web.UI.Page
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
                    ViewState["Userid"] = Session["Admin"].ToString();
                    string pagename_current = "Online_Reg_Create_Result.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];


                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details order by Session asc");
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    ddl_class.SelectedValue = My.get_top_one_class();
                    ddl_session.SelectedValue = My.get_session_id_onlinereg();
                    ViewState["sessionid"] = ddl_session.SelectedValue;
                     
                    fetch_exam();

                    fetch_remarks();
                    check_all_publish();
                    if (Request.QueryString["Regid"] != null)
                    {
                        ViewState["regid"] = Request.QueryString["Regid"];
                        //Bind_reg_id_data(); 
                    }
                }
            }
        }

        private void fetch_remarks()
        {
            string query = "Select * from Online_exam_result_remarks";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                txt_info.Value = dt.Rows[0]["Remarks"].ToString();
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
            else if (ddl_test_name.SelectedValue == "Select")
            {
                Alertme("Please select test name", "warning");
            }
            else
            {
                Bind_data();
                check_all_publish();
            }
        }
        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Online_Reg_Create_Result.aspx", false);
        }

        private void Bind_data()
        {
            string query = "Select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=Online_Admission.Class_id) as Course_Name  from Online_Admission where Class_id=" + ddl_class.SelectedValue + " and Session_id=" + ddl_session.SelectedValue + " and Payment_Status='Paid' and Test_id=" + ddl_test_name.SelectedValue + " order by Name ";
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


        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt_admssion_Date = (TextBox)e.Row.FindControl("txt_admssion_Date");
                DropDownList ddl_attendance_status = (DropDownList)e.Row.FindControl("ddl_attendance_status");
                DropDownList ddl_exam_result = (DropDownList)e.Row.FindControl("ddl_exam_result");

                DropDownList ddl_hours = (DropDownList)e.Row.FindControl("ddl_hours");
                DropDownList ddl_minutes = (DropDownList)e.Row.FindControl("ddl_minutes");
                DropDownList ddl_am_pm = (DropDownList)e.Row.FindControl("ddl_am_pm");

                Label lbl_Registration_id = (Label)e.Row.FindControl("lbl_Registration_id");
                bind_data_if_added(lbl_Registration_id.Text, ddl_attendance_status, ddl_exam_result, txt_admssion_Date, ddl_hours, ddl_minutes, ddl_am_pm);

            }
        }

        private void bind_data_if_added(string Registration_id, DropDownList ddl_attendance_status, DropDownList ddl_exam_result, TextBox txt_admssion_Date, DropDownList ddl_hours, DropDownList ddl_minutes, DropDownList ddl_am_pm)
        {
            SqlCommand cmd = new SqlCommand("Select *   from Online_Reg_Exam_Result where   Branch_id='" + ViewState["branchid"].ToString() + "'  and Admission_no='" + Registration_id + "' ");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                txt_admssion_Date.Text = txt_adm_date.Text;
            }
            else
            {
                ddl_attendance_status.Text = dt.Rows[0]["Attendance_Status"].ToString();
                ddl_exam_result.Text = dt.Rows[0]["Exam_Result"].ToString();
                txt_admssion_Date.Text = dt.Rows[0]["Admission_date"].ToString();
                ddl_hours.Text = dt.Rows[0]["Hours"].ToString();
                ddl_minutes.Text = dt.Rows[0]["Min"].ToString();
                ddl_am_pm.Text = dt.Rows[0]["AM_PM"].ToString();
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_test_name.SelectedValue == "Select")
            {
                Alertme("Please select test name", "warning");
            }
            else
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    save_or_update(ddl_class.SelectedValue, ddl_session.SelectedValue);
                    Alertme("Result has been saved successfully", "success");
                    btn_save.Visible = false;
                    GrdView.DataSource = null;
                    GrdView.DataBind();
                    check_all_publish();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    save_or_update(ddl_class.SelectedValue, ddl_session.SelectedValue);
                    Alertme("Result has been saved successfully", "success");
                    btn_save.Visible = false;
                    GrdView.DataSource = null;
                    GrdView.DataBind();
                    check_all_publish();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }


        private void save_or_update(string classid, string sessionid)
        {
            for (var i = 0; i < GrdView.Rows.Count; i++)
            {

                try
                {
                    Label lbl_Registration_id = (Label)GrdView.Rows[i].FindControl("lbl_Registration_id");


                    DropDownList ddl_attendance_status = (DropDownList)GrdView.Rows[i].FindControl("ddl_attendance_status");
                    DropDownList ddl_exam_result = (DropDownList)GrdView.Rows[i].FindControl("ddl_exam_result");
                    TextBox txt_admssion_Date = (TextBox)GrdView.Rows[i].FindControl("txt_admssion_Date");

                    DropDownList ddl_hours = (DropDownList)GrdView.Rows[i].FindControl("ddl_hours");
                    DropDownList ddl_minutes = (DropDownList)GrdView.Rows[i].FindControl("ddl_minutes");
                    DropDownList ddl_am_pm = (DropDownList)GrdView.Rows[i].FindControl("ddl_am_pm");


                    final_save_or_update(classid, sessionid, ddl_attendance_status.Text, ddl_exam_result.Text, lbl_Registration_id.Text, txt_admssion_Date.Text, ddl_hours.Text, ddl_minutes.Text, ddl_am_pm.Text);


                }
                catch (Exception ex)
                {
                }
            }
        }

        private void final_save_or_update(string classid, string sessionid, string attendance_status, string exam_result, string Registration_id, string adm_date, string hours, string minutes, string am_pm)
        {
            string time = hours + ":" + minutes + " " + am_pm;
            SqlCommand cmd = new SqlCommand("Select * from Online_Reg_Exam_Result where Session_Id=" + sessionid + " and Branch_id='" + ViewState["branchid"].ToString() + "'  and Class_id=" + classid + " and Admission_no='" + Registration_id + "'  and Test_id=" + ddl_test_name.SelectedValue + "");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Online_Reg_Exam_Result (Session_Id,Branch_id,Class_id,Attendance_Status,Exam_Result,Created_by,Created_datetime,Admission_no,Admission_date,Is_published,Test_id,Admission_time,Hours,Min,AM_PM) values (@Session_Id,@Branch_id,@Class_id,@Attendance_Status,@Exam_Result,@Created_by,@Created_datetime,@Admission_no,@Admission_date,@Is_published,@Test_id,@Admission_time,@Hours,@Min,@AM_PM)";

                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_Id", sessionid);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", classid);
                cmd.Parameters.AddWithValue("@Attendance_Status", attendance_status);
                cmd.Parameters.AddWithValue("@Exam_Result", exam_result);

                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_datetime", My.getdate1());
                cmd.Parameters.AddWithValue("@Admission_no", Registration_id);
                cmd.Parameters.AddWithValue("@Admission_date", adm_date);
                cmd.Parameters.AddWithValue("@Is_published", 0);
                cmd.Parameters.AddWithValue("@Test_id", ddl_test_name.SelectedValue);
                cmd.Parameters.AddWithValue("@Admission_time", time);
                cmd.Parameters.AddWithValue("@Hours", hours);
                cmd.Parameters.AddWithValue("@Min", minutes);
                cmd.Parameters.AddWithValue("@AM_PM", am_pm);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string query = "Update Online_Reg_Exam_Result set Exam_Result=@Exam_Result,Attendance_Status=@Attendance_Status,Updated_by=@Updated_by,Updated_date_time=@Updated_date_time,Admission_date=@Admission_date,Is_published=@Is_published,Admission_time=@Admission_time,Hours=@Hours,Min=@Min,AM_PM=@AM_PM where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Attendance_Status", attendance_status);
                cmd.Parameters.AddWithValue("@Exam_Result", exam_result);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Admission_date", adm_date);
                cmd.Parameters.AddWithValue("@Is_published", 0);
                cmd.Parameters.AddWithValue("@Admission_time", time);
                cmd.Parameters.AddWithValue("@Hours", hours);
                cmd.Parameters.AddWithValue("@Min", minutes);
                cmd.Parameters.AddWithValue("@AM_PM", am_pm);
                cmd.Parameters.AddWithValue("@Id", id);
                if (My.InsertUpdateData(cmd))
                {

                }

            }
        }

        protected void btn_update_remarks_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    save_remarks();
                    Alertme("Remarks has been updated successfully,", "success");
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    save_remarks();
                    Alertme("Remarks has been updated successfully,", "success");
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

        private void save_remarks()
        {
            SqlCommand cmd = new SqlCommand("Select Id from Online_exam_result_remarks");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Online_exam_result_remarks (Remarks) values (@Remarks)";

                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Remarks", txt_info.Value);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string query = "Update Online_exam_result_remarks set Remarks=@Remarks where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Remarks", txt_info.Value);
                cmd.Parameters.AddWithValue("@Id", id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        protected void btn_publish_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {

                    mycode.executequery("update Online_Reg_Exam_Result set Is_published=1 where Session_Id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + " ");
                    Alertme("Result card has been publish successfully.", "success");


                    check_all_publish();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                    mycode.executequery("update Online_Reg_Exam_Result set Is_published=1 where Session_Id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + " ");
                    Alertme("Result card has been publish successfully.", "success");


                    check_all_publish();
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

        private void check_all_publish()
        {
            DataTable dt = My.dataTable("Select Id from Online_Reg_Exam_Result where Session_Id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + " and Is_published=0");
            if (dt.Rows.Count == 0)
            {
                btn_publish.Visible = false;
            }
            else
            {
                btn_publish.Visible = true;
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