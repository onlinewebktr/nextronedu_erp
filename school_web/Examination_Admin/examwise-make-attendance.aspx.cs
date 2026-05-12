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

namespace school_web.Examination_Admin
{
    public partial class examwise_make_attendance : System.Web.UI.Page
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
        UsesCode mycode = new UsesCode();
        My my = new My();
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
                    ViewState["Fetch_From_ATT"] = "0";
                    txt_from_date.Text = mycode.date();
                    txt_to_date.Text = mycode.date();
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());
                    string firmId = My.get_firm_id();
                    if (firmId == "NNI-01")
                    {
                        Response.Redirect("examwise-attendance.aspx", false);
                    }
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");

                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");
                    mycode.bind_all_ddl_with_id(ddl_session_p, "select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) desc");
                    ddl_session_p.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_CourseCat_p, "Select Course_Name, course_id from Add_course_table order by Position asc");
                }
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_sectionTerm();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_sectionTerm()
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
            }
            else
            {
                mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");
                mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {

                Alertme("Please select class.", "warning");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
                ddl_section.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alertme("Please select term", "warning");
                ddl_term.Focus();
            }
            else if (ddl_exam.SelectedItem.Text == "Select")
            {
                Alertme("Please select assessment", "warning");
                ddl_exam.Focus();
            }
            else
            {
                fetch_students();
            }
        }

        private void fetch_students()
        {
            string query = "Select admissionserialnumber,studentname,rollnumber from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and   Status='1' and StudentStatus!='TC' order by rollnumber";

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                txt_mm.Text = "0";
                lbl_max_marks.Text = "0";
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                //lbl_max_marks.Text = dt.Rows[0]["No_of_Class"].ToString();
                //txt_mm.Text = dt.Rows[0]["No_of_Class"].ToString();
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_adm_no = ((Label)e.Item.FindControl("lbl_adm_no")) as Label;
                TextBox txt_marks = ((TextBox)e.Item.FindControl("txt_marks")) as TextBox;
                TextBox txt_no_of_class = ((TextBox)e.Item.FindControl("txt_no_of_class")) as TextBox;


                if (ViewState["Fetch_From_ATT"].ToString() == "0")
                {
                    DataTable dt = mycode.FillData("select * from Exam_Wise_Attendance where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Exam_id='" + ddl_exam.SelectedValue + "' and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                    if (dt.Rows.Count == 0)
                    {
                        txt_marks.Text = "";
                    }
                    else
                    {
                        txt_marks.Text = dt.Rows[0]["No_of_class_Attendance"].ToString();
                        try
                        {
                            if (dt.Rows[0]["Total_no_of_class"].ToString() == "")
                            {
                            }
                            else
                            {
                                txt_no_of_class.Text = dt.Rows[0]["Total_no_of_class"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else
                {
                    DataTable dt = mycode.FillData("select * from TEMP_ATTENDANCE_Examwise where Session_id='" + ddlsession.SelectedValue + "'  and Term_id='" + ddl_term.SelectedValue + "' and Exam_id='" + ddl_exam.SelectedValue + "' and Admission='" + lbl_adm_no.Text + "'");
                    if (dt.Rows.Count == 0)
                    {
                        txt_marks.Text = "";
                    }
                    else
                    {
                        txt_marks.Text = dt.Rows[0]["Total_persent_days"].ToString();
                        try
                        {
                            txt_no_of_class.Text = dt.Rows[0]["Total_working_days"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                save_marks();
                Alertme("Attendance has been saved successfully.", "success");
            }
            catch (Exception ex)
            {
            }

        }

        private void save_marks()
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alertme("Please select section.", "warning");
                ddl_section.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alertme("Please select term.", "warning");
                ddl_term.Focus();
            }
            else if (ddl_exam.SelectedItem.Text == "Select")
            {
                Alertme("Please select Assessment.", "warning");
                ddl_term.Focus();
            }
            else
            {
                int i;
                int gridview_rowcount = RPDetails.Items.Count;
                for (i = 0; i < gridview_rowcount; i++)
                {
                    Label lbl_adm_no = (Label)RPDetails.Items[i].FindControl("lbl_adm_no");
                    TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks");
                    TextBox txt_no_of_class = (TextBox)RPDetails.Items[i].FindControl("txt_no_of_class");
                    if (txt_marks.Text != "" || txt_no_of_class.Text != "")
                    {

                        DataTable dt = mycode.FillData("select Id from Exam_Wise_Attendance where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddl_CourseCat.SelectedValue + " and Section='" + ddl_section.Text + "' and Exam_Term_Id=" + ddl_term.SelectedValue + "  and Exam_Id='" + ddl_exam.SelectedValue + "' and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                        if (dt.Rows.Count == 0)
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Exam_Wise_Attendance (Session_id,Branch_id,Section,Admission_no,Class_id,No_of_class_Attendance,Cretaed_by,Created_date,Exam_Term_Id,Exam_Id,Total_no_of_class) values (@Session_id,@Branch_id,@Section,@Admission_no,@Class_id,@No_of_class_Attendance,@Cretaed_by, @Created_date,@Exam_Term_Id,@Exam_Id,@Total_no_of_class)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                            cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            cmd.Parameters.AddWithValue("@Exam_Term_Id", ddl_term.SelectedValue);
                            cmd.Parameters.AddWithValue("@Exam_Id", ddl_exam.SelectedValue);
                            cmd.Parameters.AddWithValue("@No_of_class_Attendance", txt_marks.Text);
                            cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_date", mycode.getdate1());
                            cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);
                            cmd.Parameters.AddWithValue("@Total_no_of_class", txt_no_of_class.Text);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            string id = dt.Rows[0]["Id"].ToString();
                            SqlCommand cmd;
                            string query = "Update Exam_Wise_Attendance set  No_of_class_Attendance=@No_of_class_Attendance,Cretaed_by=@Cretaed_by,Created_date=@Created_date,Total_no_of_class=@Total_no_of_class  where  Id= @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@No_of_class_Attendance", txt_marks.Text);
                            cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_date", mycode.getdate1());
                            cmd.Parameters.AddWithValue("@Total_no_of_class", txt_no_of_class.Text);
                            cmd.Parameters.AddWithValue("@Id", id);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                    }
                }
            }
        }
         

        protected void ddl_CourseCat_p_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    ddl_CourseCat_p.Focus();
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    mycode.bind_ddl(ddl_section_p, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat_p.SelectedValue + "'  order by Section");
                    mycode.bind_all_ddl_with_id(ddl_term_p, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat_p.SelectedValue + "' and Session_Id='" + ddl_session_p.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_fetch_attendance_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    ddl_session_p.Focus();
                    Alertme("Please choose session.", "warning");
                }
                else if (ddl_CourseCat_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    ddl_CourseCat_p.Focus();
                    Alertme("Please choose class.", "warning");
                }
                else if (ddl_section_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    ddl_section_p.Focus();
                    Alertme("Please choose section.", "warning");
                }
                else if (ddl_term_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    ddl_term_p.Focus();
                    Alertme("Please choose term.", "warning");
                }
                else if (ddl_exam_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    ddl_exam_p.Focus();
                    Alertme("Please choose assessment.", "warning");
                }
                else if (txt_from_date.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    txt_from_date.Focus();
                    Alertme("Please choose term start from date.", "warning");
                }
                else if (txt_to_date.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    txt_to_date.Focus();
                    Alertme("Please choose term end to date.", "warning");
                }
                else
                {
                    fetch_allendances();
                    ViewState["Fetch_From_ATT"] = "1";
                    ddlsession.SelectedValue = ddl_session_p.SelectedValue;
                    ddl_CourseCat.SelectedValue = ddl_CourseCat_p.SelectedValue;
                    fetch_sectionTerm();
                    ddl_section.Text = ddl_section_p.Text;
                    ddl_term.SelectedValue = ddl_term_p.SelectedValue;
                    fetch_students();
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void fetch_allendances()
        {
            string qry = "select * from admission_registor where Session_id='" + ddl_session_p.SelectedValue + "' and Class_id='" + ddl_CourseCat_p.SelectedValue + "' and Section='" + ddl_section_p.Text + "' and Status='1' order by studentname asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    fetch_no_of_days(dr["admissionserialnumber"].ToString(), ddl_session_p.SelectedValue, ddl_CourseCat_p.SelectedValue, ddl_section_p.Text, txt_from_date.Text, txt_to_date.Text);
                }
            }
        }

        private void fetch_no_of_days(string admission_no, string session_id, string class_id, string section, string from_date, string to_date)
        {
            DateTime fromDateTime = DateTime.ParseExact(from_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDateTime = DateTime.ParseExact(to_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TimeSpan t = toDateTime.Subtract(fromDateTime);


            int daysinmonth = My.toIntS(t.TotalDays + 1);
            int total_no_of_days = My.toint(daysinmonth);
            int total_no_of_days_less_one = total_no_of_days - 1;
            int total_holiday_days = 0;
            int total_persent_days = 0;
            int total_absent_days = 0;
            int total_leave_days = 0;
            int total_working_days = 0;
            double attendance_perc = 0;
            int idates = 0;
            for (int i = 1; i <= daysinmonth; i++)
            {

                idates = My.DateConvertToIdate(fromDateTime.ToString("dd/MM/yyyy"));
                string dayName = fromDateTime.ToString("dddd");

                string query = "select * from Student_Attendance_saved_Class_Wise where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Admission_no='" + admission_no + "' and Attendance_IDate='" + idates + "'";
                SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Class_Routine_period_Master");
                DataTable dt = ds.Tables[0];
                int rowcount1 = dt.Rows.Count;
                if (rowcount1 == 0)
                {
                    string daynameClass = "";
                    string AttendanceS_type = "";
                    string period_type_in_calender = get_period_type_in_calender(dayName, session_id, class_id, idates);
                    if (period_type_in_calender == "Class")
                    {
                        AttendanceS_type = "NA";
                        daynameClass = "notattendances";
                        total_absent_days++;
                    }
                    else if (period_type_in_calender == "Holiday")
                    {
                        AttendanceS_type = period_type_in_calender;
                        total_holiday_days++;
                    }
                    else
                    {
                        AttendanceS_type = period_type_in_calender;
                        if (dayName == "Sunday")
                        {
                            total_holiday_days++;
                        }
                        else
                        {
                            total_holiday_days++;
                        }
                    }

                    if (period_type_in_calender == "Holiday")
                    {
                        daynameClass = "daySunday";
                    }
                    if (dayName == "Sunday")
                    {
                        daynameClass = "daySunday";
                    }
                    total_working_days = total_no_of_days - total_holiday_days;
                    attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                    ///========================
                    ///=========================== 
                }
                else
                {
                    string period_type_in_calender = get_period_type_in_calender(dayName, session_id, class_id, idates);
                    if (period_type_in_calender == "Holiday")
                    {
                        dayName = "Sunday";
                    }


                    string daynameClass = "";
                    if (dayName == "Sunday")
                    {
                        daynameClass = "daySunday";
                        total_holiday_days++;
                    }
                    else
                    {
                        string attt_type = "";
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                        {
                            attt_type = "P"; daynameClass = "daypresenT";
                            total_persent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                        {
                            attt_type = "A"; daynameClass = "dayabsenT";
                            total_absent_days++;
                        }
                        if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                        {
                            attt_type = "L"; daynameClass = "dayleavE";
                            total_leave_days++;
                        }
                    }
                    total_working_days = total_no_of_days - total_holiday_days;
                    attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                }
                string plusOne = fromDateTime.AddDays(+1).ToShortDateString();
                fromDateTime = DateTime.ParseExact(plusOne, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }


            DataTable dtcheck = mycode.FillData("select Id from TEMP_ATTENDANCE_Examwise where Session_id=" + session_id + " and Admission='" + admission_no + "' and Term_id=" + ddl_term_p.SelectedValue + " and Exam_id='" + ddl_exam_p.SelectedValue + "'");
            if (dtcheck.Rows.Count == 0)
            {
                SqlCommand cmd;
                string queryinsrt = "INSERT INTO TEMP_ATTENDANCE_Examwise (Session_id,Admission,Term_id,Exam_id,From_date,To_date,Total_no_of_days,Total_holiday_days,Total_persent_days,Total_absent_days,Total_leave_days,Total_no_of_days_less_one,Total_working_days,Attendance_perc,Created_by,Created_date,Created_idate) values (@Session_id,@Admission,@Term_id,@Exam_id,@From_date,@To_date,@Total_no_of_days,@Total_holiday_days,@Total_persent_days,@Total_absent_days,@Total_leave_days,@Total_no_of_days_less_one,@Total_working_days,@Attendance_perc,@Created_by,@Created_date,@Created_idate)";
                cmd = new SqlCommand(queryinsrt);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Admission", admission_no);
                cmd.Parameters.AddWithValue("@Term_id", ddl_term_p.SelectedValue);
                cmd.Parameters.AddWithValue("@Exam_id", ddl_exam_p.SelectedValue);
                cmd.Parameters.AddWithValue("@From_date", txt_from_date.Text);
                cmd.Parameters.AddWithValue("@To_date", txt_to_date.Text);
                cmd.Parameters.AddWithValue("@Total_no_of_days", total_no_of_days.ToString());
                cmd.Parameters.AddWithValue("@Total_holiday_days", total_holiday_days.ToString());
                cmd.Parameters.AddWithValue("@Total_persent_days", total_persent_days.ToString());
                cmd.Parameters.AddWithValue("@Total_absent_days", total_absent_days.ToString());
                cmd.Parameters.AddWithValue("@Total_leave_days", total_leave_days.ToString());
                cmd.Parameters.AddWithValue("@Total_no_of_days_less_one", total_no_of_days_less_one.ToString());
                cmd.Parameters.AddWithValue("@Total_working_days", total_working_days.ToString());
                cmd.Parameters.AddWithValue("@Attendance_perc", attendance_perc.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dtcheck.Rows[0]["Id"].ToString();
                SqlCommand cmd;
                string queryUpdate = "Update TEMP_ATTENDANCE_Examwise set From_date=@From_date,To_date=@To_date,Total_no_of_days=@Total_no_of_days,Total_holiday_days= @Total_holiday_days,Total_persent_days=@Total_persent_days,Total_absent_days= @Total_absent_days,Total_leave_days= @Total_leave_days,Total_no_of_days_less_one= @Total_no_of_days_less_one,Total_working_days= @Total_working_days,Attendance_perc= @Attendance_perc,Created_by= @Created_by,Created_date= @Created_date,Created_idate= @Created_idate where  Id= @Id";
                cmd = new SqlCommand(queryUpdate);
                cmd.Parameters.AddWithValue("@From_date", txt_from_date.Text);
                cmd.Parameters.AddWithValue("@To_date", txt_to_date.Text);
                cmd.Parameters.AddWithValue("@Total_no_of_days", total_no_of_days.ToString());
                cmd.Parameters.AddWithValue("@Total_holiday_days", total_holiday_days.ToString());
                cmd.Parameters.AddWithValue("@Total_persent_days", total_persent_days.ToString());
                cmd.Parameters.AddWithValue("@Total_absent_days", total_absent_days.ToString());
                cmd.Parameters.AddWithValue("@Total_leave_days", total_leave_days.ToString());
                cmd.Parameters.AddWithValue("@Total_no_of_days_less_one", total_no_of_days_less_one.ToString());
                cmd.Parameters.AddWithValue("@Total_working_days", total_working_days.ToString());
                cmd.Parameters.AddWithValue("@Attendance_perc", attendance_perc.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Id", id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }



        private string get_period_type_in_calender(string day, string session_id, string class_id, int cidate)
        {
            string returN = "Class";
            string query = "select Type from School_Holiday_Calendar where Idate='" + cidate + "' and Session_id='" + session_id + "' and Day='" + day + "' and Class_id='" + class_id + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                returN = dt.Rows[0]["Type"].ToString();
            }
            return returN;
        }

        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            RPDetails.DataSource = null;
            RPDetails.DataBind();
            mycode.bind_all_ddl_with_id(ddl_exam, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Assessment_Name asc");
        }

        protected void ddl_term_p_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycode.bind_all_ddl_with_id(ddl_exam_p, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_CourseCat_p.SelectedValue + "' and Exam_Term_Id='" + ddl_term_p.SelectedValue + "' and Session_Id='" + ddl_session_p.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Assessment_Name asc");
        }
    }
}