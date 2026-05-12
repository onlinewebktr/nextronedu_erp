using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Net;
using System.IO;

namespace school_web.InstructorProfile
{
    public partial class Student_Add_Attendance_class_wise : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teacher"] == null)
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
                    ViewState["teacher"] = Session["teacher"].ToString();
                    ViewState["Is_roll_no_class_attendance"] = My.get_Is_roll_no_class_attendance();
                    code.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details where use_mode='1'");
                    ddl_session.SelectedValue = code.get_session_id(code.get_session());


                    code.bind_all_ddl_with_id(ddl_class, "Select  distinct  Course_Name, course_id,Position from Add_course_table where    course_id in(select CategoryID from  Ptm_class_teacher_mapping   where UserID='" + ViewState["teacher"].ToString() + "')  order by Position asc");
                    txt_date.Text = code.date();

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
                Alert("Please select class");
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


        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.SelectedItem.Text == "Text")
            {
                Alert("Please select section");
            }

            else if (txt_date.Text == "")
            {
                Alert("Please select date");
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
                query = "Select admissionserialnumber,studentname,rollnumber,CASE WHEN (studentimagepath is null or studentimagepath='') THEN '/images/dummy-student.jpg'  WHEN studentimagepath is not null THEN studentimagepath END AS Student_img,(select top 1 Course_Name from Add_course_table  where course_id=admission_registor.Class_id) as classname,father_mob,Father_whatsApp_no   from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and status='1' order by rollnumber asc";
            }
            else
            {
                query = "Select admissionserialnumber,studentname,rollnumber,CASE WHEN (studentimagepath is null or studentimagepath='') THEN '/images/dummy-student.jpg'  WHEN studentimagepath is not null THEN studentimagepath END AS Student_img,(select top 1 Course_Name from Add_course_table  where course_id=admission_registor.Class_id) as classname,father_mob,Father_whatsApp_no   from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and status='1' order by studentname asc";
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
                else if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                {
                    rb.SelectedValue = "3";
                } 
            }
        }

        protected void btn_save_all_Click(object sender, EventArgs e)
        {
            string student_attendance = "Absent";
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (txt_date.Text == "")
            {
                Alert("Please select date");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.SelectedItem.Text == "Text")
            {
                Alert("Please select section");
            } 
            else
            {
                int growcount = GrdView.Rows.Count;
                int k = 0;

                for (int i = 0; i < growcount; i++)
                {
                    RadioButtonList RadioButtonList1 = (RadioButtonList)GrdView.Rows[i].FindControl("RadioButtonList1") as RadioButtonList;
                    try
                    {
                        if (RadioButtonList1.SelectedItem.Text == "P") //Present
                        {
                            student_attendance = "Present";
                        }

                        else if (RadioButtonList1.SelectedItem.Text == "A") //Absent
                        {
                            student_attendance = "Absent";
                        }

                        else if (RadioButtonList1.SelectedItem.Text == "L") //Leave
                        {
                            student_attendance = "Leave";
                        }
                    }
                    catch
                    {
                        student_attendance = "Absent";
                    }

                    Label lbl_reg_id = (Label)GrdView.Rows[i].FindControl("lbl_reg_id");
                    Label lbl_FullName = (Label)GrdView.Rows[i].FindControl("lbl_FullName");
                    Label lbl_father_mob = (Label)GrdView.Rows[i].FindControl("lbl_father_mob");
                    Label lbl_Father_whatsApp_no = (Label)GrdView.Rows[i].FindControl("lbl_Father_whatsApp_no");
                    Label lbl_classname = (Label)GrdView.Rows[i].FindControl("lbl_classname");
                    finalattendance(student_attendance, lbl_reg_id.Text, lbl_FullName.Text, lbl_father_mob.Text, lbl_Father_whatsApp_no.Text, lbl_classname.Text);

                    k++;

                }


                Alert("Successfully attendance saved");
                finally_open_student_list();




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

            My mycode = new My();

            //Dear Parents, your child [0] of [1] is {2} on [3]. Please send your child regularly – {4}. Regards PURNANK SOFTWARE SOLUTIONS

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
                cmd.Parameters.AddWithValue("@Created_By", ViewState["teacher"].ToString());
                cmd.Parameters.AddWithValue("@Attendance_Status", student_attendance);
                cmd.Parameters.AddWithValue("@Admission_no", admission);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
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
                                    User_id = ViewState["teacher"].ToString(),
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


                                        //ServicePointManager.Expect100Continue = true;
                                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                                        string results = sr.ReadToEnd();
                                        sr.Close();

                                        My.Insert("Message_Details", new
                                        {
                                            Mobile_No = whatsappno,
                                            Message = message,
                                            Date = mycode.date(),
                                            Idate = mycode.idate(),
                                            Time = mycode.time(),
                                            Result = results,
                                            User_id = ViewState["teacher"].ToString(),
                                            Mesage_Type = type,
                                            Groupcode = "Wahataap",
                                            Status = "SEND",
                                            Url = _url,
                                            Message_to_Type = "Student",
                                            admin_user_id = admission,
                                        });

                                    }
                                    //return true;
                                }
                                catch (Exception ex)
                                {
                                    My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                                    //return false;
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
                    cmd1.Parameters.AddWithValue("@Posted_By", ViewState["teacher"].ToString());
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
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string strQuery = "Update Student_Attendance_saved_Class_Wise set    Updated_Date=@Updated_Date,Updated_IDate=@Updated_IDate,Updated_Time=@Updated_Time,Updated_By=@Updated_By,Attendance_Status=@Attendance_Status   where Id = @Id";
                SqlCommand cmd1 = new SqlCommand(strQuery);

                cmd1.Parameters.AddWithValue("@Updated_Date", code.date());
                cmd1.Parameters.AddWithValue("@Updated_IDate", code.idate());
                cmd1.Parameters.AddWithValue("@Updated_Time", code.time());
                cmd1.Parameters.AddWithValue("@Updated_By", ViewState["teacher"].ToString());
                cmd1.Parameters.AddWithValue("@Attendance_Status", student_attendance);
                cmd1.Parameters.AddWithValue("@Id", id);
                if (InsertUpdate.InsertUpdateData(cmd1))
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "sp_Attendance_Notification_Save";
                    cmd2.Parameters.AddWithValue("@Heading", "Attendance Notification");
                    cmd2.Parameters.AddWithValue("@Details", message);
                    cmd2.Parameters.AddWithValue("@Posted_By", ViewState["teacher"].ToString());
                    cmd2.Parameters.AddWithValue("@Posted_Date", code.date());
                    cmd2.Parameters.AddWithValue("@Posted_Idate", code.idate());
                    cmd2.Parameters.AddWithValue("@Posted_Time", code.time());
                    cmd2.Parameters.AddWithValue("@AdmissionNo", admission);
                    cmd2.Parameters.AddWithValue("@Send_status", 0);
                    cmd2.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                    if (UsesCode.InsertUpdateData_sp(cmd2))
                    {

                    }
                }
            }
        }
    }
}