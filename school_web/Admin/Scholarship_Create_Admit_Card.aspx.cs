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
    public partial class Scholarship_Create_Admit_Card : System.Web.UI.Page
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
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["sessionid"] = My.get_session_id();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());


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




                    ViewState["admitcard_total"] = "0";
                    mycode.bind_all_ddl_with_id(ddl_Scholorship, "select Test_name,Test_id from Scholarship_Program where Is_active=1 order by  Test_name asc");
                    //ddl_class.SelectedValue = My.get_top_one_class();



                    //fetch_student_by_class();
                    // check_all_publish();
                }
            }
        }

        private void count_ttl_student()
        {
            try
            {
                DataTable dt = mycode.FillData("select count(id) as ttl_student from Scholarship_Admission where   Class_id='" + ddl_class.SelectedValue + "' and Payment_Status='Paid' and  Test_id=" + ddl_Scholorship.SelectedValue + "");
                txt_total_student.Text = dt.Rows[0]["ttl_student"].ToString();
            }
            catch
            {

            }

        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                count_ttl_student();
                fetch_student_by_class();
                check_all_publish();
                try
                {
                    Bind_grid_date_roll();
                }
                catch
                {

                }

            }
            catch (Exception ex)
            {
            }
        }


        private void fetch_student_by_class()
        {
            string query = "Select oa.*,'0'  as admissionnumber,ore.Roll_no,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,ore.Exam_Type,format(Exam_Date_time, 'hh:mm') as exm_time,format(Exam_Date_time, 'tt') as exm_ampm,format(Reporting_datetime, 'hh:mm') as rp_time,format(Reporting_datetime, 'tt') as rp_ampm,format(Gate_close_datetime, 'hh:mm') as gc_time,format(Gate_close_datetime, 'tt') as gc_ampm,format(Exam_end_date_time, 'hh:mm') as ed_time,format(Exam_end_date_time, 'tt') as ed_ampm,ore.Roll_start_from,ore.Exam_Type,ore.Exam_Date,ore.Remarks,ore.Exam_Shift,ore.Exam_end_time,ore.Reporting_time,ore.Exam_Centre_Id,ore.Room_no from Scholarship_Admission oa join Scholarship_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  where oa.Class_id=" + ddl_class.SelectedValue + " and  oa.Test_id=" + ddl_Scholorship.SelectedValue + " and oa.Payment_Status='Paid' order by ore.Roll_no,ore.Room_no  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pnl_created_admit.Visible = false;
                grid_grade.DataSource = null;
                grid_grade.DataBind();
            }
            else
            {
                pnl_created_admit.Visible = true;
                grid_grade.DataSource = dt;
                grid_grade.DataBind();

                ddl_exam_Shift.Text = dt.Rows[0]["Exam_Shift"].ToString();
                //ddl_roll_no_start_from.Text = dt.Rows[0]["Roll_start_from"].ToString();
                //ddl_exam_type.Text = dt.Rows[0]["Exam_Type"].ToString();
                txt_exam_date.Text = dt.Rows[0]["Exam_Date"].ToString();
                txt_info.Value = dt.Rows[0]["Remarks"].ToString();
                //ExamTime
                string exm_ampm = dt.Rows[0]["exm_ampm"].ToString();
                string exm_time = dt.Rows[0]["exm_time"].ToString();
                string[] stringSeparators = new string[] { ":" };
                string[] arre = exm_time.Split(stringSeparators, StringSplitOptions.None);
                string exm_hours = arre[0];
                string exm_minutes = arre[1];
                ddl_hour.Text = exm_hours;
                ddl_minut.Text = exm_minutes;
                ddl_am_pm.Text = exm_ampm;


                //ReportingTime
                string rp_ampm = dt.Rows[0]["rp_ampm"].ToString();
                string rp_time = dt.Rows[0]["rp_time"].ToString();
                string[] arr = rp_time.Split(stringSeparators, StringSplitOptions.None);
                string rp_hours = arr[0];
                string rp_minutes = arr[1];
                ddl_rp_hour.Text = rp_hours;
                ddl_rp_minut.Text = rp_minutes;
                ddl_rp_am_pm.Text = rp_ampm;



                //GateCloseTime
                string ed_ampm = dt.Rows[0]["ed_ampm"].ToString();
                string ed_time = dt.Rows[0]["ed_time"].ToString();
                string[] arrs = ed_time.Split(stringSeparators, StringSplitOptions.None);
                string ed_hours = arrs[0];
                string ed_minutes = arrs[1];


                ddl_exam_end_hour.Text = ed_hours;
                ddl_exam_end_minut.Text = ed_minutes;
                ddl_exam_end_ampm.Text = ed_ampm;

                mycode.bind_all_ddl_with_id(ddl_center_name, "select distinct Centre_Name,Exam_Centre_Id from Scholarship_Exam_Centre where Test_id='" + ddl_Scholorship.SelectedValue + "' order by  Centre_Name asc");
                ddl_center_name.SelectedValue = dt.Rows[0]["Exam_Centre_Id"].ToString();
                mycode.bind_ddl(ddl_room, "select distinct Room_no from Scholarship_Exam_Centre_room_no where Test_id='" + ddl_Scholorship.SelectedValue + "' and Exam_centre_id='" + ddl_center_name.SelectedValue + "' order by  Room_no asc");


                ddl_room.Text = dt.Rows[0]["Room_no"].ToString();

            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                bool check_stop_scloship = find_status_schloership();
                if (check_stop_scloship == true)
                {
                    if (ddl_Scholorship.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select scholorship name.", "warning");
                        ddl_Scholorship.Focus();
                    }


                    else if (ddl_class.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select class.", "warning");
                        ddl_class.Focus();
                    }
                    else if (ddl_center_name.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select exam Centre.", "warning");
                        ddl_center_name.Focus();
                    }
                    else if (txt_exam_date.Text == "")
                    {
                        Alertme("Please choose exam date.", "warning");
                        txt_exam_date.Focus();
                    }

                    else
                    {
                        ViewState["IsMsgShow"] = "0";

                        bool chekroll_no = get_roll_no_add_or_not();
                        if (chekroll_no == false)
                        {
                            Alertme("Please add roll number ", "warning");

                        }
                        else
                        {


                            if (txt_total_student.Text == ViewState["admitcard_total"].ToString())
                            {
                                create_admit_card_new();


                                if (ViewState["IsMsgShow"].ToString() == "1")
                                {
                                    Alertme("Admit card has been created successfully.", "success");
                                    fetch_student_by_class();
                                    check_all_publish();
                                }

                            }
                            else
                            {
                                Alertme("Please check total number of students and number of roll number added for admit card", "warning");
                            }


                        }

                    }
                }
                else
                {
                    Alertme("Please scholarship status must be stopped before creation of admit card", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void create_admit_card_new()
        {
            //string roll_nos = create_roll_no(My.toIntS(txt_total_student.Text), My.toIntS(ddl_roll_no_start_from.Text));
            string query3 = "Select * from Scholarship_Roll_No_Room_allocation where Class_id=" + ddl_class.SelectedValue + " and Test_id=" + ddl_Scholorship.SelectedValue + " and Exam_Centre_Id=" + ddl_center_name.SelectedValue + " ";

            DataTable dt3 = mycode.FillData(query3);
            if (dt3.Rows.Count == 0)
            {


            }
            else
            {
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string Room_no = dt3.Rows[i]["Room_no"].ToString();
                    string Roll_Start_From = dt3.Rows[i]["Roll_Start_From"].ToString();
                    string Roll_End_from = dt3.Rows[i]["Roll_End_from"].ToString();
                    string Admitcard_created = dt3.Rows[i]["Admitcard_created"].ToString();

                    //for (int j = 0; j < My.toIntS(Admitcard_created); j++)

                    string query = "Select top " + Admitcard_created + " *   from Scholarship_Admission where Class_id=" + ddl_class.SelectedValue + " and Test_id=" + ddl_Scholorship.SelectedValue + " and  Payment_Status='Paid' and Registration_id not in (Select Admission_no from Scholarship_Exam_Time_Table where Test_id='" + ddl_Scholorship.SelectedValue + "' and Exam_Centre_Id='" + ddl_center_name.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "') order by id ";
                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string roll_nos = create_roll_no(My.toIntS(Admitcard_created), My.toIntS(Roll_Start_From), Room_no);
                            string Registration_id = dr["Registration_id"].ToString();
                            create_admit_card(roll_nos, Room_no, Roll_Start_From, Roll_End_from, Registration_id);
                        }
                    }
                    else
                    {

                        string query2 = "Select  *   from Scholarship_Admission where Class_id=" + ddl_class.SelectedValue + " and Test_id=" + ddl_Scholorship.SelectedValue + " and  Payment_Status='Paid' and Registration_id in (Select Admission_no from Scholarship_Exam_Time_Table where Test_id='" + ddl_Scholorship.SelectedValue + "' and Exam_Centre_Id='" + ddl_center_name.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "') order by id ";
                        DataTable dt1 = mycode.FillData(query2);
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt1.Rows)
                            {
                                string roll_nos = "0";
                                string Registration_id = dr["Registration_id"].ToString();

                                create_admit_card(roll_nos, Room_no, Roll_Start_From, Roll_End_from, Registration_id);
                            }
                        }
                    }
                    //}

                }


            }


        }

        private bool get_roll_no_add_or_not()
        {
            DataTable dt = My.dataTable("Select Id from Scholarship_Roll_No_Room_allocation where  Class_id='" + ddl_class.SelectedValue + "' and Exam_Centre_Id=" + ddl_center_name.SelectedValue + " and Test_id=" + ddl_Scholorship.SelectedValue + "  ");
            if (dt.Rows.Count == 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        private bool find_status_schloership()
        {
            string query = "select   * from Scholarship_Parameter_fees where  Test_id=" + ddl_Scholorship.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + "";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                if (dt.Rows[0]["Isactive"].ToString() == "1")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        private void create_admit_card(string roll_nos, string Room_no, string Roll_Start_From, string Roll_End_from, string Registration_id)
        {
            string customdate = txt_exam_date.Text + " " + ddl_hour.Text + ":" + ddl_minut.Text + ":00" + " " + ddl_am_pm.Text;   //  
            string time = ddl_hour.Text + ":" + ddl_minut.Text + ":00" + " " + ddl_am_pm.Text;
            DateTime datetime = DateTime.ParseExact(customdate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);


            string rp_customdate = txt_exam_date.Text + " " + ddl_rp_hour.Text + ":" + ddl_rp_minut.Text + ":00" + " " + ddl_rp_am_pm.Text;   //  
            string rp_time = ddl_rp_hour.Text + ":" + ddl_rp_minut.Text + ":00" + " " + ddl_rp_am_pm.Text;

            DateTime rp_datetime = DateTime.ParseExact(rp_customdate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            string ee_customdate = txt_exam_date.Text + " " + ddl_exam_end_hour.Text + ":" + ddl_exam_end_minut.Text + ":00" + " " + ddl_exam_end_ampm.Text;   //  

            string ee_time = ddl_exam_end_hour.Text + ":" + ddl_exam_end_minut.Text + ":00" + " " + ddl_exam_end_ampm.Text;


            DateTime ee_datetime = DateTime.ParseExact(ee_customdate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            string query = "Select *   from Scholarship_Admission where  Registration_id='" + Registration_id + "'";


            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string query_check = "Select Id from Scholarship_Exam_Time_Table where Class_id=" + ddl_class.SelectedValue + "  and Admission_no='" + dr["Registration_id"].ToString() + "' and Test_id=" + ddl_Scholorship.SelectedValue + "";
                    DataTable dt_check = mycode.FillData(query_check);
                    if (dt_check.Rows.Count == 0)
                    {


                        string querys = "INSERT INTO Scholarship_Exam_Time_Table (Session_Id,Branch_id,Class_id,Exam_Date,Exam_Time,Exam_Date_time,Created_by,Created_datetime,Day,Admission_no,Exam_Type,Remarks,Is_published,Roll_no,Reporting_time,Reporting_datetime,Roll_start_from,Exam_Shift,Exam_end_date_time,Test_id,Exam_Centre_Id,Room_no,Exam_end_time) values (@Session_Id,@Branch_id,@Class_id,@Exam_Date,@Exam_Time,@Exam_Date_time,@Created_by,@Created_datetime,@Day,@Admission_no,@Exam_Type,@Remarks,@Is_published,@Roll_no,@Reporting_time,@Reporting_datetime,@Roll_start_from,@Exam_Shift,@Exam_end_date_time,@Test_id,@Exam_Centre_Id,@Room_no,@Exam_end_time)";
                        SqlCommand cmd = new SqlCommand(querys);
                        cmd.Parameters.AddWithValue("@Session_Id", My.get_session_id_from_Scholarship_id(ddl_Scholorship.SelectedValue));
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Exam_Date", txt_exam_date.Text);
                        cmd.Parameters.AddWithValue("@Exam_Time", time);
                        cmd.Parameters.AddWithValue("@Exam_Date_time", datetime);
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_datetime", My.getdate1());
                        cmd.Parameters.AddWithValue("@Day", mycode.getdayname(txt_exam_date.Text));
                        cmd.Parameters.AddWithValue("@Admission_no", dr["Registration_id"].ToString());
                        cmd.Parameters.AddWithValue("@Exam_Type", "Exam");
                        cmd.Parameters.AddWithValue("@Remarks", txt_info.Value);
                        cmd.Parameters.AddWithValue("@Is_published", "0");
                        cmd.Parameters.AddWithValue("@Roll_no", roll_nos);
                        cmd.Parameters.AddWithValue("@Reporting_time", rp_time);

                        cmd.Parameters.AddWithValue("@Reporting_datetime", rp_datetime);



                        cmd.Parameters.AddWithValue("@Roll_start_from", Roll_Start_From);
                        cmd.Parameters.AddWithValue("@Exam_Shift", ddl_exam_Shift.Text);


                        cmd.Parameters.AddWithValue("@Test_id", ddl_Scholorship.SelectedValue);

                        cmd.Parameters.AddWithValue("@Exam_Centre_Id", ddl_center_name.SelectedValue);
                        cmd.Parameters.AddWithValue("@Room_no", Room_no);


                        cmd.Parameters.AddWithValue("@Exam_end_date_time", ee_datetime);
                        cmd.Parameters.AddWithValue("@Exam_end_time", ee_time);



                        if (My.InsertUpdateData(cmd))
                        {
                            ViewState["IsMsgShow"] = "1";
                        }
                    }
                    else
                    {
                        string id = dt_check.Rows[0]["Id"].ToString();
                        string querys = "Update Scholarship_Exam_Time_Table set Exam_Date=@Exam_Date,Exam_Time=@Exam_Time,Exam_Date_time=@Exam_Date_time,Updated_by=@Updated_by,Updated_date_time=@Updated_date_time,Day=@Day,Exam_Type=@Exam_Type,Remarks=@Remarks,Is_published=@Is_published,Reporting_time=@Reporting_time,Reporting_datetime=@Reporting_datetime,Roll_start_from=@Roll_start_from,Exam_Shift=@Exam_Shift,Test_id=@Test_id,Exam_Centre_Id=@Exam_Centre_Id,Exam_end_date_time=@Exam_end_date_time,Exam_end_time=@Exam_end_time where Id = @Id";
                        SqlCommand cmd = new SqlCommand(querys);
                        cmd.Parameters.AddWithValue("@Exam_Date", txt_exam_date.Text);
                        cmd.Parameters.AddWithValue("@Exam_Time", time);
                        cmd.Parameters.AddWithValue("@Exam_Date_time", datetime);
                        cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date_time", My.getdate1());
                        cmd.Parameters.AddWithValue("@Day", mycode.getdayname(txt_exam_date.Text));
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Exam_Type", "Exam");
                        cmd.Parameters.AddWithValue("@Remarks", txt_info.Value);
                        cmd.Parameters.AddWithValue("@Is_published", "0");
                        cmd.Parameters.AddWithValue("@Reporting_time", rp_time);

                        cmd.Parameters.AddWithValue("@Reporting_datetime", rp_datetime);

                        cmd.Parameters.AddWithValue("@Roll_start_from", Roll_Start_From);
                        cmd.Parameters.AddWithValue("@Exam_Shift", ddl_exam_Shift.Text);

                        cmd.Parameters.AddWithValue("@Test_id", ddl_Scholorship.SelectedValue);

                        cmd.Parameters.AddWithValue("@Exam_Centre_Id", ddl_center_name.SelectedValue);
                        //cmd.Parameters.AddWithValue("@Room_no", Room_no);


                        cmd.Parameters.AddWithValue("@Exam_end_date_time", ee_datetime);
                        cmd.Parameters.AddWithValue("@Exam_end_time", ee_time);

                       
                        if (My.InsertUpdateData(cmd))
                        {
                            ViewState["IsMsgShow"] = "1";
                        }
                    }
                }
            }
            else
            {
                Alertme("Student not found.", "Warning");
            }
        }

        private string create_roll_no(int no_of_std, int roll_start_from, string Room_no)
        {
            int no_of_std1 = My.toint(no_of_std);
            int room_start_from1 = My.toint(roll_start_from);
            string Roll_no = "";
            bool duplicateid = false;
            Random rn = new Random();
            int i = roll_start_from;
            //int j = no_of_std;
            int j = no_of_std + i;
            do
            {
                int k = rn.Next(i, j);
                Roll_no = k.ToString();
                if (Roll_no.Length == 1)
                {
                    Roll_no = "000" + Roll_no;
                }
                else if (Roll_no.Length == 2)
                {
                    Roll_no = "00" + Roll_no;
                }
                else if (Roll_no.Length == 3)
                {
                    Roll_no = "0" + Roll_no;
                }
                duplicateid = check_dauplicate_ids(ddl_class.SelectedValue, ddl_center_name.SelectedValue, Roll_no, ddl_Scholorship.SelectedValue, Room_no);
                if (duplicateid == true)
                {
                }
            }

            while (duplicateid == false);
            return Roll_no;
        }

        private bool check_dauplicate_ids(string class_id, string Exam_Centre_Id, string Roll_no, string testid, string Room_id)
        {

            DataTable dt = My.dataTable("Select Roll_no from Scholarship_Exam_Time_Table where Roll_no='" + Roll_no + "' and Class_id='" + class_id + "' and Exam_Centre_Id=" + Exam_Centre_Id + " and Test_id=" + testid + " and Room_no='" + Room_id + "'");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Registration_id = (Label)row.FindControl("lbl_Registration_id");
                Label lbl_roll_no = (Label)row.FindControl("lbl_roll_no");
                Label lbl_Test_id = (Label)row.FindControl("lbl_Test_id");


                mycode.executequery("delete from Scholarship_Exam_Time_Table where  Admission_no='" + lbl_Registration_id.Text + "' and Roll_no='" + lbl_roll_no.Text + "' and Test_id='" + lbl_Test_id.Text + "'   ");
                fetch_student_by_class();
                Alertme("Deletion process has been successfully done", "success");
            }
            catch
            {

            }
        }

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {
                mycode.executequery("update Scholarship_Exam_Time_Table set Is_published=1 where   Class_id='" + ddl_class.SelectedValue + "' and Test_id=" + ddl_Scholorship.SelectedValue + "  ");
                Alertme("Admit card has been publish successfully.", "success");
                check_all_publish();
            }
            catch (Exception ex)
            {
            }
        }

        private void check_all_publish()
        {
            DataTable dt = My.dataTable("Select Id from Scholarship_Exam_Time_Table where Class_id='" + ddl_class.SelectedValue + "' and Test_id=" + ddl_Scholorship.SelectedValue + " and Is_published=0");
            if (dt.Rows.Count == 0)
            {
                btn_final_submit.Visible = false;
            }
            else
            {
                btn_final_submit.Visible = true;
            }
        }



        protected void ddl_Scholorship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Scholorship.SelectedItem.Text == "Select")
            {
                Alertme("Please select scholorship name", "Warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_class, "Select distinct ac.Course_Name,ac.course_id,ac.Position from Add_course_table  ac join Scholarship_Admission ad  on ac.course_id=ad.Class_id where   Test_id=" + ddl_Scholorship.SelectedValue + " and Steps_done='10' order by ac.Position asc");

                mycode.bind_all_ddl_with_id(ddl_center_name, "select distinct Centre_Name,Exam_Centre_Id from Scholarship_Exam_Centre where Test_id='" + ddl_Scholorship.SelectedValue + "' order by  Centre_Name asc");



            }
        }

        protected void ddl_center_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Scholorship.SelectedItem.Text == "Select")
            {
                Alertme("Please select scholorship name", "Warning");
            }
            else if (ddl_center_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam centre", "Warning");
            }

            else
            {
                mycode.bind_ddl(ddl_room, "select distinct Room_no from Scholarship_Exam_Centre_room_no where Test_id='" + ddl_Scholorship.SelectedValue + "' and Exam_centre_id='" + ddl_center_name.SelectedValue + "' order by  Room_no asc");

                try
                {
                    Bind_grid_date_roll();
                }
                catch
                {

                }

            }
        }


        #region add roll no and Edit roll
        protected void btn_Add_roll_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Scholorship.SelectedItem.Text == "Select")
                {
                    Alertme("Please select scholorship name.", "warning");
                    ddl_Scholorship.Focus();
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select scholarship from ", "warning");
                    ddl_class.Focus();
                }
                else if (ddl_center_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select exam centre.", "warning");
                    ddl_center_name.Focus();
                }
                else if (ddl_room.Text == "Select")
                {
                    Alertme("Please select room number", "warning");
                    ddl_room.Focus();
                }
                else
                {
                    add_data();
                }
            }
            catch
            {

            }

        }

        private void add_data()
        {
            int start_roll = My.toint(txt_start_from_roll_no.Text);
            int endroll = My.toint(txt_endroll_no.Text);
            if (start_roll > endroll)
            {
                Alertme("End roll number cannot be less than start roll number.", "warning");

            }

            else
            {
                if (btn_Add_roll_no.Text == "Add")
                {
                    string query = "Select * from Scholarship_Roll_No_Room_allocation where Class_id=" + ddl_class.SelectedValue + " and Test_id=" + ddl_Scholorship.SelectedValue + " and Exam_Centre_Id=" + ddl_center_name.SelectedValue + " and Room_no='" + ddl_room.Text + "'";
                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count == 0)
                    {
                        int Admitcard_created = (My.toint(txt_endroll_no.Text) - My.toint(txt_start_from_roll_no.Text)) + 1;

                        My.Insert("Scholarship_Roll_No_Room_allocation", new
                        {
                            Session_id = My.get_session_id_from_Scholarship_id(ddl_Scholorship.SelectedValue),
                            Class_id = ddl_class.SelectedValue,
                            Test_id = ddl_Scholorship.SelectedValue,
                            Exam_Centre_Id = ddl_center_name.SelectedValue,
                            Room_no = ddl_room.Text,
                            Roll_Start_From = txt_start_from_roll_no.Text,
                            Roll_End_from = txt_endroll_no.Text,

                            Admitcard_created = Admitcard_created
                        }); ;

                        txt_start_from_roll_no.Text = "";
                        txt_endroll_no.Text = "";
                        btn_Add_roll_no.Text = "Add";
                        Bind_grid_date_roll();

                    }
                    else
                    {
                        Alertme("Already roll number added", "warning");

                    }
                }
                else
                {
                    int Admitcard_created = (My.toint(txt_endroll_no.Text) - My.toint(txt_start_from_roll_no.Text)) + 1;
                    My.Update("Scholarship_Roll_No_Room_allocation", new
                    {
                        Session_id = My.get_session_id_from_Scholarship_id(ddl_Scholorship.SelectedValue),
                        Class_id = ddl_class.SelectedValue,
                        Test_id = ddl_Scholorship.SelectedValue,
                        Exam_Centre_Id = ddl_center_name.SelectedValue,
                        Room_no = ddl_room.Text,
                        Roll_Start_From = txt_start_from_roll_no.Text,
                        Roll_End_from = txt_endroll_no.Text,
                        Admitcard_created = Admitcard_created,

                    }, where: $"Id='{hd_id_roll.Value}'");



                    txt_start_from_roll_no.Text = "";
                    txt_endroll_no.Text = "";
                    btn_Add_roll_no.Text = "Add";
                    Bind_grid_date_roll();
                }


            }


        }

        protected void lnk_delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btn.Parent.Parent;
                string Id = ((Label)row.FindControl("lbl_Id")).Text;
                My.exeSql("delete from Scholarship_Roll_No_Room_allocation where Id=" + Id + "");
                Bind_grid_date_roll();
            }
            catch
            {

            }

        }



        protected void lnkEdit_Click1(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btn.Parent.Parent;
                string Id = ((Label)row.FindControl("lbl_Id")).Text;
                string start_roll_from = ((Label)row.FindControl("lbl_start_roll_from")).Text;
                string Roll_End_from = ((Label)row.FindControl("lbl_Roll_End_from")).Text;
                string Room_no = ((Label)row.FindControl("lbl_Room_no")).Text;
                txt_start_from_roll_no.Text = start_roll_from;
                txt_endroll_no.Text = Roll_End_from;
                hd_id_roll.Value = Id;
                ddl_room.Text = Room_no;
                //My.exeSql("delete from Scholarship_Roll_No_Room_allocation where Id=" + Id + "");
                //Bind_grid_date_roll();
            }
            catch
            {

            }
        }

        private void Bind_grid_date_roll()
        {
            string query = "Select * from Scholarship_Roll_No_Room_allocation where Class_id=" + ddl_class.SelectedValue + " and Test_id=" + ddl_Scholorship.SelectedValue + " and Exam_Centre_Id=" + ddl_center_name.SelectedValue + " ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                //Alertme("Sorry there are no data list exist", "warning");
                GrdView_roll.DataSource = null;
                GrdView_roll.DataBind();

            }
            else
            {

                GrdView_roll.DataSource = dt;
                GrdView_roll.DataBind();
            }

        }

        #endregion

        int total = 0;
        protected void GrdView_roll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Admitcard_created = (Label)e.Row.FindControl("lbl_Admitcard_created");
                total = total + My.toint(lbl_Admitcard_created.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_admitcard = (Label)e.Row.FindControl("lbl_total_admitcard");
                lbl_total_admitcard.Text = total.ToString();



                ViewState["admitcard_total"] = lbl_total_admitcard.Text;
            }
        }

        public Dictionary<string, object> get_start_roll_no_and_end_roll_no()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from Scholarship_Roll_No_Room_allocation   where Class_id='" + ddl_class.SelectedValue + "' and Test_id='" + ddl_Scholorship.SelectedValue + "' and Exam_Centre_Id='" + ddl_center_name.SelectedValue + "' and Room_no";
            SqlCommand cmd;

            cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                dc["Roll_Start_From"] = "";
                dc["Roll_End_from"] = "0";
                dc["Room_no"] = "0";

            }
            else
            {
                dc["Roll_Start_From"] = dt.Rows[0]["Roll_Start_From"].ToString();
                dc["Roll_End_from"] = dt.Rows[0]["Roll_End_from"].ToString();
                dc["Room_no"] = dt.Rows[0]["Room_no"].ToString();
            }
            return dc;

        }
    }
}