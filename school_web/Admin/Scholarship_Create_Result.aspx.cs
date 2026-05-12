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
    public partial class Scholarship_Create_Result : System.Web.UI.Page
    {
        My mycode = new My();

        string centername = "Select top 1 Centre_Name from Scholarship_Exam_Centre where Exam_Centre_Id=ore.Exam_Centre_Id and Test_id=ore.Test_id ";
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Scholarship_Create_Result.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["session_id"] = My.get_session_id_onlinereg();
                        mycode.bind_all_ddl_with_id(ddl_Scholarship_name, "select Test_name,Test_id from Scholarship_Program   order by  Test_name asc");
                        ddl_Scholarship_name.SelectedValue = My.get_top_one_Scholarship_name(My.get_session_id_onlinereg());

                        mycode.bind_all_ddl_with_id(ddl_class, "Select distinct ac.Course_Name,ac.course_id,ac.Position from Add_course_table  ac join Scholarship_Admission ad  on ac.course_id=ad.Class_id where Test_id=" + ddl_Scholarship_name.SelectedValue + " order by ac.Position asc");

                        txt_adm_date.Text = mycode.date();


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set_Course_Fee");
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
        protected void ddl_Scholarship_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Scholarship_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select scholorship name", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id_All_New(ddl_class, "Select distinct ac.Course_Name,ac.course_id,ac.Position from Add_course_table  ac join Scholarship_Admission ad  on ac.course_id=ad.Class_id where Test_id=" + ddl_Scholarship_name.SelectedValue + " order by ac.Position asc");

            }
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_Scholarship_name.SelectedItem.Text == "")
            {
                Alertme("Please select scholorship name", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select scholorship for", "warning");
            }
            else
            {
                find_data();
            }
        }

        private void find_data()
        {
            bool check_publish_result = get_result_publised();
            if (check_publish_result == false)
            {
                Alertme("Sorry you can't find student list because result has been published", "warning");
            }
            else

            {

                string query = "Select oa.*,'0'  as admissionnumber,ore.Roll_no,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,ore.Exam_Type,format(Exam_Date_time, 'hh:mm') as exm_time,format(Exam_Date_time, 'tt') as exm_ampm,format(Reporting_datetime, 'hh:mm') as rp_time,format(Reporting_datetime, 'tt') as rp_ampm,format(Gate_close_datetime, 'hh:mm') as gc_time,format(Gate_close_datetime, 'tt') as gc_ampm,format(Exam_end_date_time, 'hh:mm') as ed_time,format(Exam_end_date_time, 'tt') as ed_ampm,ore.Roll_start_from,ore.Exam_Type,ore.Exam_Date,ore.Remarks,ore.Exam_Shift,ore.Exam_end_time,ore.Reporting_time,Room_no,(" + centername + ") as Centre_Name,ore.Exam_Centre_Id from Scholarship_Admission oa join Scholarship_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  where   oa.Test_id=" + ddl_Scholarship_name.SelectedValue + "   and ore.Class_id=" + ddl_class.SelectedValue + " order by ore.Roll_no ";

                bind_grid_data(query);
            }
        }

        private bool get_result_publised()
        {
            string query = "Select * from Scholarship_Exam_Result where Class_id=" + ddl_class.SelectedValue + " and Test_id=" + ddl_Scholarship_name.SelectedValue + " and Is_published=1 ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void bind_grid_data(string query)
        {
            ViewState["query"] = query;
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                btn_save.Visible = false;
                Alertme("Sorry there are no scholarship student list exist", "warning");
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
                Label lbl_testid = (Label)e.Row.FindControl("lbl_testid");

                TextBox txt_full_marks = (TextBox)e.Row.FindControl("txt_full_marks");
                TextBox txt_Obtain_marks = (TextBox)e.Row.FindControl("txt_Obtain_marks");
                Label lbl_Obtain_percentage = (Label)e.Row.FindControl("lbl_Obtain_percentage");
                Label lbl_Rank = (Label)e.Row.FindControl("lbl_Rank");

                bind_data_if_added(lbl_Registration_id.Text, ddl_attendance_status, ddl_exam_result, txt_admssion_Date, ddl_hours, ddl_minutes, ddl_am_pm, lbl_testid.Text, txt_full_marks, txt_Obtain_marks, lbl_Obtain_percentage, lbl_Rank);

            }
        }

        private void bind_data_if_added(string Registration_id, DropDownList ddl_attendance_status, DropDownList ddl_exam_result, TextBox txt_admssion_Date, DropDownList ddl_hours, DropDownList ddl_minutes, DropDownList ddl_am_pm, string testid, TextBox txt_full_marks, TextBox txt_Obtain_marks, Label lbl_Obtain_percentage, Label lbl_Rank)
        {
            SqlCommand cmd = new SqlCommand("Select *   from Scholarship_Exam_Result where  Admission_no='" + Registration_id + "' and Test_id=" + testid + " ");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                txt_admssion_Date.Text = txt_adm_date.Text;
                txt_full_marks.Text = txtfull_marks.Text;
            }
            else
            {
                ddl_attendance_status.Text = dt.Rows[0]["Attendance_Status"].ToString();
                ddl_exam_result.Text = dt.Rows[0]["Exam_Result"].ToString();
                txt_admssion_Date.Text = dt.Rows[0]["Admission_date"].ToString();
                ddl_hours.Text = dt.Rows[0]["Hours"].ToString();
                ddl_minutes.Text = dt.Rows[0]["Min"].ToString();
                ddl_am_pm.Text = dt.Rows[0]["AM_PM"].ToString();
                txt_full_marks.Text = dt.Rows[0]["Full_Marks"].ToString();
                txt_Obtain_marks.Text = dt.Rows[0]["Obtain_Marks"].ToString();
                lbl_Obtain_percentage.Text = dt.Rows[0]["Obtain_percentage"].ToString();
                lbl_Rank.Text = dt.Rows[0]["Obtain_rank"].ToString();

            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (ddl_Scholarship_name.SelectedValue == "Select")
            {
                Alertme("Please select scholarship name", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    save_or_update();
                    update_rank();
                    Alertme("Result has been saved successfully", "success");
                    find_data();

                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    save_or_update();
                    update_rank();
                    Alertme("Result has been saved successfully", "success");
                    find_data();

                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }


            }
        }

        private void update_rank()
        {
            SqlCommand cmd = new SqlCommand("Select * from Scholarship_Exam_Result where  Class_id=" + ddl_class.SelectedValue + "   and Test_id=" + ddl_Scholarship_name.SelectedValue + " order by (convert(float, Obtain_percentage))  desc");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                double prev_mark = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    double percentage = My.toDouble(dr["Obtain_percentage"].ToString());
                    if (prev_mark != percentage)
                    {
                        i++;
                        prev_mark = percentage;
                    }


                    My.exeSql("update Scholarship_Exam_Result set Obtain_rank='" + i + "' where Admission_no='" + dr["Admission_no"].ToString() + "' and Class_id=" + ddl_class.SelectedValue + "   and Test_id=" + ddl_Scholarship_name.SelectedValue + "");
                }


            }
        }

        private void save_or_update()
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
                    Label lbl_testid = (Label)GrdView.Rows[i].FindControl("lbl_testid");
                    Label lbl_Class_id = (Label)GrdView.Rows[i].FindControl("lbl_Class_id");
                    Label lbl_Session_id = (Label)GrdView.Rows[i].FindControl("lbl_Session_id");

                    TextBox txt_full_marks = (TextBox)GrdView.Rows[i].FindControl("txt_full_marks");
                    TextBox txt_Obtain_marks = (TextBox)GrdView.Rows[i].FindControl("txt_Obtain_marks");



                    final_save_or_update(lbl_Class_id.Text, lbl_Session_id.Text, ddl_attendance_status.Text, ddl_exam_result.Text, lbl_Registration_id.Text, txt_admssion_Date.Text, ddl_hours.Text, ddl_minutes.Text, ddl_am_pm.Text, lbl_testid.Text, txt_full_marks.Text, txt_Obtain_marks.Text);


                }
                catch (Exception ex)
                {
                }
            }
        }

        private void final_save_or_update(string classid, string sessionid, string attendance_status, string exam_result, string Registration_id, string adm_date, string hours, string minutes, string am_pm, string testid, string full_marks, string Obtain_marks)
        {
            double percentage = (My.toDouble(Obtain_marks) / My.toDouble(full_marks)) * 100;
            double percentage2 = Math.Round(Convert.ToDouble(percentage), 2);

            string time = hours + ":" + minutes + " " + am_pm;
            SqlCommand cmd = new SqlCommand("Select * from Scholarship_Exam_Result where  Class_id=" + classid + " and Admission_no='" + Registration_id + "'  and Test_id=" + testid + "");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Scholarship_Exam_Result (Class_id,Attendance_Status,Exam_Result,Created_by,Created_datetime,Admission_no,Admission_date,Is_published,Test_id,Admission_time,Hours,Min,AM_PM,Full_Marks,Obtain_Marks,Obtain_percentage) values (@Class_id,@Attendance_Status,@Exam_Result,@Created_by,@Created_datetime,@Admission_no,@Admission_date,@Is_published,@Test_id,@Admission_time,@Hours,@Min,@AM_PM,@Full_Marks,@Obtain_Marks,@Obtain_percentage)";

                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Class_id", classid);
                cmd.Parameters.AddWithValue("@Attendance_Status", attendance_status);
                cmd.Parameters.AddWithValue("@Exam_Result", exam_result);

                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_datetime", My.getdate1());
                cmd.Parameters.AddWithValue("@Admission_no", Registration_id);
                cmd.Parameters.AddWithValue("@Admission_date", adm_date);
                cmd.Parameters.AddWithValue("@Is_published", 0);
                cmd.Parameters.AddWithValue("@Test_id", testid);
                cmd.Parameters.AddWithValue("@Admission_time", time);
                cmd.Parameters.AddWithValue("@Hours", hours);
                cmd.Parameters.AddWithValue("@Min", minutes);
                cmd.Parameters.AddWithValue("@AM_PM", am_pm);

                cmd.Parameters.AddWithValue("@Full_Marks", full_marks);
                cmd.Parameters.AddWithValue("@Obtain_Marks", Obtain_marks);
                cmd.Parameters.AddWithValue("@Obtain_percentage", percentage2.ToString());

                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string query = "Update Scholarship_Exam_Result set Exam_Result=@Exam_Result,Attendance_Status=@Attendance_Status,Updated_by=@Updated_by,Updated_date_time=@Updated_date_time,Admission_date=@Admission_date,Is_published=@Is_published,Admission_time=@Admission_time,Hours=@Hours,Min=@Min,AM_PM=@AM_PM,Full_Marks=@Full_Marks,Obtain_Marks=@Obtain_Marks,Obtain_percentage=@Obtain_percentage where Id = @Id";
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
                cmd.Parameters.AddWithValue("@Full_Marks", full_marks);
                cmd.Parameters.AddWithValue("@Obtain_Marks", Obtain_marks);
                cmd.Parameters.AddWithValue("@Obtain_percentage", percentage2.ToString());
                cmd.Parameters.AddWithValue("@Id", id);
                if (My.InsertUpdateData(cmd))
                {

                }

            }

        }
    }
}