using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class make_attendance : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        ViewState["Is_roll_no_class_attendance"] = My.get_Is_roll_no_class_attendance();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "make-attendance.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_teacher, "select name,user_id from user_details where User_Type='Teacher' and Branch_id='" + ViewState["Branchid"].ToString() + "' order by name asc");

                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details where use_mode='1'");
                        ddl_session.SelectedValue = My.get_session_id();
                        txt_date.Text = mycode.date();


                        Dictionary<string, object> autosms = mycode.get_auto_message_template("Attendance/Leave/Absent Confirmation");
                        ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                        ViewState["VariableName"] = (String)autosms["VariableName"];
                        ViewState["SMSType"] = (String)autosms["SMSType"];
                        ViewState["Send_From"] = (String)autosms["Send_From"];
                        ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                        ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];
                        ViewState["schoolname"] = (String)autosms["schoolname"];


                        var dt2 = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                        if (dt2.Rows.Count == 1)
                        {
                            ViewState["whatsapp_mobile_no"] = dt2.Rows[0]["SMS_API"].ToString();
                            ViewState["Whatsapp_api_url"] = dt2.Rows[0]["url"].ToString();
                        }
                        else
                        {
                            ViewState["whatsapp_mobile_no"] = "";
                            ViewState["Whatsapp_api_url"] = "";
                        }

                        var dt1 = mycode.FillData("select top 1 * from message_config where Status='running'");
                        if (dt1.Rows.Count == 1)
                        {
                            ViewState["api_key"] = dt1.Rows[0]["uid"].ToString();
                            ViewState["Sender_id"] = dt1.Rows[0]["sender"].ToString();
                        }
                        else
                        {
                            ViewState["api_key"] = "0";
                            ViewState["Sender_id"] = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Collection_Report");
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

        protected void ddl_teacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // mycode.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ddl_teacher.SelectedValue + "')  order by Position asc");

                code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  Ptm_class_teacher_mapping   where UserID='" + ddl_teacher.SelectedValue + "')  order by Position asc");

            }
            catch (Exception ex)
            {
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

                string get_sectionty = get_section_type();
                if (get_sectionty == "ALL")
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'  order by Section");
                }
                else
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from Ptm_class_teacher_mapping where CategoryID ='" + ddl_class.SelectedValue + "' and Section!='ALL'    order by Section");
                }
            }
        }

        private string get_section_type()
        {
            string query = "Select * from Ptm_class_teacher_mapping where CategoryID='" + ddl_class.SelectedValue + "' and Session_Id='" + ddl_session.SelectedValue + "' and Section='ALL'";
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                return "A";
            }
            else
            {
                return "ALL";
            }
        }

        //==================================================
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
            else if (ddl_section.SelectedItem.Text == "Text")
            {
                Alertme("Please select section", "warning");
            }

            else if (txt_date.Text == "")
            {
                Alertme("Please select date", "warning");
            }
            else
            {
                finally_open_student_list();
            }
        }

        private void finally_open_student_list()
        {
            string query = "";
            if (ViewState["Is_roll_no_class_attendance"].ToString() == "1")// roll no wise 
            {
                query = "Select admissionserialnumber,studentname,rollnumber,CASE WHEN studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN studentimagepath is not null THEN studentimagepath END AS Student_img,(select top 1 Course_Name from Add_course_table  where course_id=admission_registor.Class_id) as classname,father_mob,Father_whatsApp_no   from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and status='1' and (Transfer_Status='New' or Transfer_Status='NT') order by rollnumber asc";
            }
            else
            {
                query = "Select admissionserialnumber,studentname,rollnumber,CASE WHEN studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN studentimagepath is not null THEN studentimagepath END AS Student_img,(select top 1 Course_Name from Add_course_table  where course_id=admission_registor.Class_id) as classname,father_mob,Father_whatsApp_no   from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and status='1' and (Transfer_Status='New' or Transfer_Status='NT') order by studentname asc";
            }
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                GrdView.DataSource = null;
                GrdView.DataBind();
                grid111.Visible = false;
            }
            else
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
                grid111.Visible = true;
            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Bind_option1((RadioButtonList)e.Row.FindControl("RadioButtonList1"));
                Label lbl_reg_id = (Label)e.Row.FindControl("lbl_reg_id");
                RadioButtonList rb = (RadioButtonList)e.Row.FindControl("RadioButtonList1");
                bind_if_answerd(rb, lbl_reg_id.Text);
            }
        }

        private void Bind_option1(RadioButtonList radioButtonList)
        {
            String strConnString = connection.conn;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                string query = @"select Typeid,Type from Type_of_Attendance";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    radioButtonList.DataSource = cmd.ExecuteReader();
                    radioButtonList.DataTextField = "Type";
                    radioButtonList.DataValueField = "Typeid";
                    //radioButtonList.CssClass = "Type";
                    radioButtonList.DataBind();
                    //radioButtonList.SelectedItem.Value = "1";
                    con.Close();
                }
            }
        }
        protected void RadioButtonList1_DataBound(object sender, EventArgs e)
        {
            RadioButtonList list = (RadioButtonList)sender;
        }

        private void bind_if_answerd(RadioButtonList rb, string admission)
        {
            string day = code.getdayname(txt_date.Text);
            string query = "Select * from Student_Attendance_saved_Class_Wise where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + ddl_session.SelectedValue + "'   and Admission_no='" + admission + "'   and Attendance_Date='" + txt_date.Text + "'  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                rb.SelectedValue = "1";
            }
            else
            {
                if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                {
                    rb.SelectedValue = "1";
                }
                else if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                {
                    rb.SelectedValue = "2";
                }
                else if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                {
                    rb.SelectedValue = "3";
                }
            }
        }


        ///==============================================
        protected void btn_save_all_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (txt_date.Text == "")
            {
                Alertme("Please select date", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_section.SelectedItem.Text == "Text")
            {
                Alertme("Please select section", "warning");
            }
            else
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    final_save_data();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    final_save_data();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }

        private void final_save_data()
        {
            DataTable dtc = My.dataTable("select Type from School_Holiday_Calendar  where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Idate='" + My.DateConvertToIdate(txt_date.Text) + "'");
            if (dtc.Rows.Count > 0)
            {
                if (dtc.Rows[0][0].ToString().ToUpper() == "HOLIDAY")
                {
                    Alertme("The date you selected is a holiday, and attendance entry is therefore not permitted.", "warning");
                    return;
                }
            }

            string student_attendance = "Absent";
            int growcount = GrdView.Rows.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                RadioButtonList RadioButtonList1 = (RadioButtonList)GrdView.Rows[i].FindControl("RadioButtonList1") as RadioButtonList;
                if (RadioButtonList1.SelectedItem.Text == "P")
                {
                    student_attendance = "Present";
                }
                else if (RadioButtonList1.SelectedItem.Text == "A")
                {
                    student_attendance = "Absent";
                }
                else if (RadioButtonList1.SelectedItem.Text == "L")
                {
                    student_attendance = "Leave";
                }
                Label lbl_reg_id = (Label)GrdView.Rows[i].FindControl("lbl_reg_id");
                Label lbl_FullName = (Label)GrdView.Rows[i].FindControl("lbl_FullName");
                Label lbl_father_mob = (Label)GrdView.Rows[i].FindControl("lbl_father_mob");
                Label lbl_Father_whatsApp_no = (Label)GrdView.Rows[i].FindControl("lbl_Father_whatsApp_no");
                Label lbl_classname = (Label)GrdView.Rows[i].FindControl("lbl_classname");
                finalattendance(student_attendance, lbl_reg_id.Text, lbl_FullName.Text, lbl_father_mob.Text, lbl_Father_whatsApp_no.Text, lbl_classname.Text);
                k++;
            }

            Alertme("The attendance record was saved successfully.", "success");
            finally_open_student_list();
            fetch_Attendance_summary();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        private void fetch_Attendance_summary()
        {
            presentStudents.InnerText = "0";
            absentStudents.InnerText = "0";
            leaveStudents.InnerText = "0";
            DataTable dt = My.dataTable("Select sas.Attendance_Status,count(sas.Id) as total from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no and ar.Session_id=sas.Session_id where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.Session_id='" + ddl_session.SelectedValue + "' and sas.Attendance_IDate='" + My.DateConvertToIdate(txt_date.Text) + "' group by Attendance_Status");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Attendance_Status"].ToString() == "Present")
                    {
                        presentStudents.InnerText = dr["total"].ToString();
                    }
                    else if (dr["Attendance_Status"].ToString() == "Absent")
                    {
                        absentStudents.InnerText = dr["total"].ToString();
                    }
                    else
                    {
                        leaveStudents.InnerText = dr["total"].ToString();
                    }
                }

                totalStudents.InnerText = (My.toDouble(presentStudents.InnerText) + My.toDouble(absentStudents.InnerText) + My.toDouble(leaveStudents.InnerText)).ToString();
            }
        }

        private void finalattendance(string student_attendance, string admission, string studentname, string mobilesms, string whatsappno, string classname)
        {
            string message = "";
            string msge1 = "";
            if (student_attendance == "Present")
            {
                msge1 = "Present"; //= "Dear Parent, Your child is in the school and is present in the class today. Date:-" + txt_date.Text;
            }
            else if (student_attendance == "Absent")
            {
                msge1 = "Absent";// msge1 = "Dear Parents your child has not in the school, and absent in the class. Date:-" + txt_date.Text;
            }
            else if (student_attendance == "Leave")
            {
                msge1 = "Leave";//msge1 = "Dear Parents your child has not in the school due to leave. Date:-" + txt_date.Text;
            }
            string type = "";


            var vrls = ViewState["VariableName"].ToString().Split(',');
            var lst = new String[vrls.Length];

            if (vrls.Length > 0)
            {
                lst[0] = studentname;
            }
            if (vrls.Length > 1)
            {
                lst[1] = classname;
            }
            if (vrls.Length > 2)
            {
                lst[2] = msge1;
            }
            if (vrls.Length > 3)
            {

                lst[3] = txt_date.Text;
            }
            if (vrls.Length > 4)
            {
                lst[4] = ""; //ViewState["schoolname"].ToString();
                //lst[4] = ViewState["schoolname"].ToString();
            }
            if (vrls.Length > 5)
            {
                lst[5] = "";
            }
            if (vrls.Length > 6)
            {

                lst[6] = "";
            }
            if (vrls.Length > 7)
            {

                lst[7] = "";
            }
            if (vrls.Length > 8)
            {

                lst[8] = "";
            }
            message = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
            if (ViewState["SMSType"].ToString() == "Unicode")
            {
                type = "unicode";
            }
            else
            {
                type = "english";
            }

            string day = code.getdayname(txt_date.Text);
            string query = "Select * from Student_Attendance_saved_Class_Wise where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and  Admission_no='" + admission + "'  and Attendance_Date='" + txt_date.Text + "'  ";
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                string strQuery = "INSERT INTO Student_Attendance_saved_Class_Wise (Session_id,Class_id,Section,Class_period,Day,Attendance_Date,Attendance_IDate,Date,Idate,time,Created_By,Attendance_Status,Admission_no) values (@Session_id,@Class_id,@Section,@Class_period,@Day,@Attendance_Date,@Attendance_IDate,@Date,@Idate,@time,@Created_By,@Attendance_Status,@Admission_no)";
                SqlCommand cmd;
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                cmd.Parameters.AddWithValue("@Class_period", "0");
                cmd.Parameters.AddWithValue("@Day", day);
                cmd.Parameters.AddWithValue("@Attendance_Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Attendance_IDate", code.ConvertStringToiDate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Date", code.date());
                cmd.Parameters.AddWithValue("@Idate", code.idate());
                cmd.Parameters.AddWithValue("@time", code.time());
                cmd.Parameters.AddWithValue("@Created_By", ddl_teacher.SelectedValue);
                cmd.Parameters.AddWithValue("@Attendance_Status", student_attendance);
                cmd.Parameters.AddWithValue("@Admission_no", admission);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string strQuery = "Update Student_Attendance_saved_Class_Wise set Updated_Date=@Updated_Date,Updated_IDate=@Updated_IDate,Updated_Time=@Updated_Time,Updated_By=@Updated_By,Attendance_Status=@Attendance_Status where Id=@Id";
                SqlCommand cmd1 = new SqlCommand(strQuery);
                cmd1.Parameters.AddWithValue("@Updated_Date", code.date());
                cmd1.Parameters.AddWithValue("@Updated_IDate", code.idate());
                cmd1.Parameters.AddWithValue("@Updated_Time", code.time());
                cmd1.Parameters.AddWithValue("@Updated_By", ddl_teacher.SelectedValue);
                cmd1.Parameters.AddWithValue("@Attendance_Status", student_attendance);
                cmd1.Parameters.AddWithValue("@Id", id);
                if (InsertUpdate.InsertUpdateData(cmd1))
                {
                }
            }
        }


        //=======================================
        protected void btn_send_notification_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = My.dataTable("Select ar.Father_whatsApp_no,ar.father_mob,ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.Session_id='" + ddl_session.SelectedValue + "' and  sas.Attendance_IDate='" + My.DateConvertToIdate(txt_date.Text) + "' order by ar.rollnumber asc");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string student_attendance = dr["Attendance_Status"].ToString();
                        string message = "";
                        string msge1 = "";
                        if (student_attendance == "Present")
                        {
                            msge1 = "Present"; //= "Dear Parent, Your child is in the school and is present in the class today. Date:-" + txt_date.Text;
                        }
                        else if (student_attendance == "Absent")
                        {
                            msge1 = "Absent";// msge1 = "Dear Parents your child has not in the school, and absent in the class. Date:-" + txt_date.Text;
                        }
                        else if (student_attendance == "Leave")
                        {
                            msge1 = "Leave";//msge1 = "Dear Parents your child has not in the school due to leave. Date:-" + txt_date.Text;
                        }
                        string studentname = dr["studentname"].ToString();
                        string classname = dr["classname"].ToString();
                        string admission = dr["admissionserialnumber"].ToString();
                        string mobilesms = dr["father_mob"].ToString();
                        string whatsappno = dr["Father_whatsApp_no"].ToString();
                        string type = "";
                        var vrls = ViewState["VariableName"].ToString().Split(',');
                        var lst = new String[vrls.Length];
                        if (vrls.Length > 0)
                        {
                            lst[0] = studentname;
                        }
                        if (vrls.Length > 1)
                        {
                            lst[1] = classname;
                        }
                        if (vrls.Length > 2)
                        {
                            lst[2] = msge1;
                        }
                        if (vrls.Length > 3)
                        {
                            lst[3] = txt_date.Text;
                        }
                        if (vrls.Length > 4)
                        {
                            lst[4] = "";
                        }
                        if (vrls.Length > 5)
                        {
                            lst[5] = "";
                        }
                        if (vrls.Length > 6)
                        {
                            lst[6] = "";
                        }
                        if (vrls.Length > 7)
                        {
                            lst[7] = "";
                        }
                        if (vrls.Length > 8)
                        {
                            lst[8] = "";
                        }
                        message = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                        if (ViewState["SMSType"].ToString() == "Unicode")
                        {
                            type = "unicode";
                        }
                        else
                        {
                            type = "english";
                        }
                        #region sms and whatsaap
                        // sms & whatsapp 
                        try
                        {
                            if (ViewState["Is_Send_SMS"].ToString().ToUpper() == "TRUE")
                            {
                                string api_key = ViewState["api_key"].ToString();
                                string Sender_id = ViewState["Sender_id"].ToString();
                                string msgtype = type;
                                string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + message + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobilesms + "&smsContentType=" + type;

                                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                                string results = sr.ReadToEnd();
                                sr.Close();
                                My.Insert("Message_Details", new
                                {
                                    Mobile_No = mobilesms,
                                    Message = message,
                                    Date = mycode.date(),
                                    Idate = mycode.idate(),
                                    Time = mycode.time(),
                                    Result = results,
                                    User_id = ddl_teacher.SelectedValue,
                                    Mesage_Type = msgtype,
                                    Groupcode = "SMS",
                                    Status = "SEND",
                                    Url = url,
                                    Message_to_Type = "Student",
                                    admin_user_id = admission,
                                });
                            }
                            if (ViewState["Is_Send_WhatsApp"].ToString().ToUpper() == "TRUE")
                            {
                                string sms = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                                try
                                {
                                    if (whatsappno.Length > 9)
                                    {
                                        string message1 = Uri.EscapeDataString(message);
                                        string mobile_no = "91" + whatsappno;
                                        string _url = "";
                                        if (ViewState["Whatsapp_api_url"].ToString().Contains("app.allexpert.in"))
                                        {
                                            //exampe url
                                            //https://app.allexpert.in/send-message?api_key=q0hRX9FA1z5S7JttEIzLtxJU3lvoOv&sender=62888xxxx&number=9162888xxxx&message=Hello World
                                            _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), mobile_no, message1);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                        }
                                        if (ViewState["Whatsapp_api_url"].ToString().Contains("api4ws.com"))
                                        {
                                            // _url = My.Whatsapp_api_url + My.whatsapp_mobile_no + "&message=" + message + "&phone=91" + mobile_no;
                                            //https://api4ws.com/sendMessage.php?AUTH_KEY=918877804016&message=User Defined Whatsapp Message&phone=919812345678
                                            _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message1, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                        }
                                        else
                                        {
                                            //https://fastwsapi.com/sendMessage.php?AUTH_KEY=DEMOEDUNEXTG@123&instance_id=575051&message=User Defined Whatsapp Message&phone=919812345678
                                            _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message1, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                        }
                                        My.Insert("WhatsApp_send", new
                                        {
                                            Mobile_no = whatsappno,
                                            Message = message,
                                            Message_url = _url,
                                            Session_id = ddl_session.SelectedValue,
                                            Admission_no = admission,
                                            Status = "Pending",
                                            Date = mycode.date(),
                                            Idate = mycode.idate(),
                                            Time = mycode.time(),
                                            Send_by = ddl_teacher.SelectedValue,
                                            Mesage_Type = type,
                                        });
                                    }
                                }
                                catch (Exception ex)
                                {
                                    My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandText = "sp_Attendance_Notification_Save";
                        cmd1.Parameters.AddWithValue("@Heading", "Attendance Notification");
                        cmd1.Parameters.AddWithValue("@Details", message);
                        cmd1.Parameters.AddWithValue("@Posted_By", ddl_teacher.SelectedValue);
                        cmd1.Parameters.AddWithValue("@Posted_Date", code.date());
                        cmd1.Parameters.AddWithValue("@Posted_Idate", code.idate());
                        cmd1.Parameters.AddWithValue("@Posted_Time", code.time());
                        cmd1.Parameters.AddWithValue("@AdmissionNo", admission);
                        cmd1.Parameters.AddWithValue("@Send_status", 0);
                        cmd1.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                        if (UsesCode.InsertUpdateData_sp(cmd1))
                        {
                        }
                    }
                    Alertme("The notification has been successfully sent.", "success");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}