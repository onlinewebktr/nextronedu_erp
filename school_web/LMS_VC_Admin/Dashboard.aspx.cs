using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Session["Admin"] = "EDUN22022";
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
                        hdStartDate.Value = code.iMonthBackdate(); hdEndDate.Value = code.idate();
                        ViewState["sesssionid"] = My.get_session_id();
                        BindCount();
                        // BindChart();

                        // sync and push send 


                        //if (!code.syncdatastatusyes_or_no())
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowProgress();", true);
                        //    string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "load", script, true);
                        //    // ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                        //}



                    }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        #region synch and send push
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                synch_data();
                System.Threading.Thread.Sleep(3000);
                a1.Visible = false;

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }

        }
        public static List<school_web.Global.mysmslis> smsList = new List<school_web.Global.mysmslis>();
        public static List<school_web.Global.myclass> class1 = new List<school_web.Global.myclass>();
        public static List<school_web.Global.sendpush> push = new List<school_web.Global.sendpush>();
        public static List<school_web.Global.sendpushMessage> pushMessage = new List<school_web.Global.sendpushMessage>();
        public static List<school_web.Global.SendpushNoticeTeacher> pushNoticeboard_teacher = new List<school_web.Global.SendpushNoticeTeacher>();
        public static List<school_web.Global.SendpushMessageTeacher> pushmessage_teacher = new List<school_web.Global.SendpushMessageTeacher>();
        public static List<school_web.Global.SendpushMessageAttandance> pushmessage_attandance = new List<school_web.Global.SendpushMessageAttandance>();
        public bool flagstudent = true;
        public bool flagclass = true;
        public bool flagsendpush = true;
        public bool flagsendpushMesaage = true;
        public bool flagsendpush_notice_Teacher = true;
        public bool flagsendpush_message_Teacher = true;
        public bool flagsendpush_message_attandance = true;
        private void synch_data()
        {

            if (flagsendpush == false)
            {
                return;
            }
            else
            {
                bind_list_push();
                foreach (school_web.Global.sendpush item in push.ToList())
                {
                    if (item.Class1 == "0")
                    {
                        flagsendpush = true;
                    }
                    else
                    {
                        flagsendpush = false;
                        send_push(item);
                    }


                }
                flagsendpush = true;

            }

            if (flagsendpushMesaage == false)
            {
                return;
            }
            else
            {
                bind_list_pushmessage();
                foreach (school_web.Global.sendpushMessage item in pushMessage.ToList())
                {
                    if (item.Class1 == "0")
                    {
                        flagsendpushMesaage = true;
                    }
                    else
                    {
                        flagsendpushMesaage = false;
                        send_pushMessage(item);
                    }


                }
                flagsendpushMesaage = true;

            }


            if (flagsendpush_notice_Teacher == false)
            {
                return;
            }
            else
            {
                bind_list_push_notice_teacher();
                foreach (school_web.Global.SendpushNoticeTeacher item in pushNoticeboard_teacher.ToList())
                {
                    if (item.Teacher_Id == "0")
                    {
                        flagsendpush_notice_Teacher = true;
                    }
                    else
                    {
                        flagsendpush_notice_Teacher = false;
                        send_push_notice_teacher(item);
                    }


                }
                flagsendpush_notice_Teacher = true;

            }

            if (flagsendpush_message_Teacher == false)
            {
                return;
            }
            else
            {
                bind_list_push_message_teacher();
                foreach (school_web.Global.SendpushMessageTeacher item in pushmessage_teacher.ToList())
                {
                    if (item.Teacher_Id == "0")
                    {
                        flagsendpush_message_Teacher = true;
                    }
                    else
                    {
                        flagsendpush_message_Teacher = false;
                        send_push_message_teacher(item);
                    }


                }
                flagsendpush_message_Teacher = true;

            }

            //--------------------------------push send_push attndance-----------------
            if (flagsendpush_message_attandance == false)
            {
                return;
            }
            else
            {
                bind_list_push_attndance();
                foreach (school_web.Global.SendpushMessageAttandance item in pushmessage_attandance.ToList())
                {
                    if (item.Id == "0")
                    {
                        flagsendpush_message_attandance = true;
                    }
                    else
                    {
                        flagsendpush_message_attandance = false;
                        send_push_attandance(item);
                    }


                }
                flagsendpush_message_attandance = true;

            }
        }

        private void send_push_attandance(Global.SendpushMessageAttandance item)
        {
            string query = "";
            query = " select top 1 gcm_id,admissionserialnumber from dbo.[admission_registor] where gcm_id is not null and admissionserialnumber='" + item.AdmissionNo + "' order by id desc";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Attendance_Notification set Send_status=1 where Id=" + item.Id + "");
            }
            else
            {
                string gcmid = dt.Rows[0]["gcm_id"].ToString();
                string admissionserialnumber = dt.Rows[0]["admissionserialnumber"].ToString();
                if (gcmid != "")
                {
                    Dictionary<String, String> ss = new Dictionary<string, string>();
                    ss["notification_id"] = Guid.NewGuid().ToString();
                    ss["message"] = item.Details;
                    ss["title"] = "";
                    ss["messagetype"] = "Message";
                    ss["url"] = "";
                    ss["link_url"] = "";
                    ss["UserId"] = admissionserialnumber;
                    UsesCode.SendNotification(gcmid, ss);
                }
                code.executequery("update Attendance_Notification set Send_status=1 where Id=" + item.Id + "");
            }
        }

        private void bind_list_push_attndance()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Attendance_Notification] where Send_status=0 ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushmessage_attandance.Clear();
                pushmessage_attandance.Add(new school_web.Global.SendpushMessageAttandance
                {
                    Details = "0",
                    Id = "0",
                    AdmissionNo = "0",




                });
            }
            else
            {
                pushmessage_attandance.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushmessage_attandance.Add(new school_web.Global.SendpushMessageAttandance
                    {
                        Details = dr["Details"].ToString(),
                        Id = dr["Id"].ToString(),
                        AdmissionNo = dr["AdmissionNo"].ToString(),

                    });
                }
            }
        }


        #region teacher push message for teacher
        private void send_push_message_teacher(Global.SendpushMessageTeacher item)
        {
            string query = "";

            if (item.Teacher_Id == "ALL")
            {

                query = " select gcm_id,user_id from dbo.[user_details] where gcm_id is not null ";



            }
            else
            {
                query = " select gcm_id,user_id from dbo.[user_details] where gcm_id is not null and user_id='" + item.Teacher_Id + "'";

            }




            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Private_Messages_For_Teacher set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string UserID = dt.Rows[i]["user_id"].ToString();

                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Details;
                        ss["title"] = item.Subject;
                        ss["messagetype"] = "Message";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = UserID;
                        UsesCode.SendNotification(gcmid, ss);
                    }

                }
                code.executequery("update Private_Messages_For_Teacher set Send_Status='Send' where Id=" + item.Id + "");
            }
        }
        private void bind_list_push_message_teacher()
        {

            SqlCommand cmd = new SqlCommand(" select * from dbo.[Private_Messages_For_Teacher] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushmessage_teacher.Clear();
                pushmessage_teacher.Add(new school_web.Global.SendpushMessageTeacher
                {

                    Subject = "0",
                    Details = "0",
                    Id = "0",
                    Teacher_Id = "0",




                });
            }
            else
            {
                pushmessage_teacher.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushmessage_teacher.Add(new school_web.Global.SendpushMessageTeacher
                    {

                        Subject = dr["Subject"].ToString(),
                        Details = dr["Details"].ToString(),
                        Id = dr["Id"].ToString(),
                        Teacher_Id = dr["Teacher_Id"].ToString(),

                    });
                }
            }
        }
        #endregion




        #region teacher push bind notice
        private void bind_list_push_notice_teacher()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Notice_Board_Details_Teacher] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushNoticeboard_teacher.Clear();
                pushNoticeboard_teacher.Add(new school_web.Global.SendpushNoticeTeacher
                {

                    Notice = "0",
                    Id = "0",
                    Teacher_Id = "0",




                });
            }
            else
            {
                pushNoticeboard_teacher.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushNoticeboard_teacher.Add(new school_web.Global.SendpushNoticeTeacher
                    {

                        Notice = dr["Notice"].ToString(),
                        Id = dr["Id"].ToString(),
                        Teacher_Id = dr["Teacher_Id"].ToString(),

                    });
                }
            }
        }
        private void send_push_notice_teacher(Global.SendpushNoticeTeacher item)
        {
            string query = "";

            if (item.Teacher_Id == "ALL")
            {

                query = " select gcm_id,user_id from dbo.[user_details] where gcm_id is not null ";



            }
            else
            {
                query = " select gcm_id,user_id from dbo.[user_details] where gcm_id is not null and user_id='" + item.Teacher_Id + "'";

            }




            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Notice_Board_Details_Teacher set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string UserID = dt.Rows[i]["user_id"].ToString();

                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Notice;
                        ss["title"] = "Notice";
                        ss["messagetype"] = "Notice";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = UserID;
                        UsesCode.SendNotification(gcmid, ss);
                    }

                }
                code.executequery("update Notice_Board_Details_Teacher set Send_Status='Send' where Id=" + item.Id + "");
            }
        }
        #endregion
        #region send push notice board
        private void send_push(school_web.Global.sendpush item)
        {
            string query = "";

            if (item.Send_Type == "Class Wise")
            {
                if (item.Class1 == "ALL")
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where gcm_id is not null ";
                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where gcm_id is not null and Section='" + item.Section + "'";
                    }

                }
                else
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where gcm_id is not null and Class_id='" + item.Class1 + "'";

                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where gcm_id is not null and Class_id='" + item.Class1 + "' and Section='" + item.Section + "'";
                    }

                }
            }
            else
            {
                query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where gcm_id is not null and admissionserialnumber='" + item.Admission_no + "'";

            }




            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Notice_Board_Details set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();

                    final_send_push(gcmid, admissionserialnumber, item);

                }
                code.executequery("update Notice_Board_Details set Send_Status='Send' where Id=" + item.Id + "");
            }
        }

        private void final_send_push(string gcmid, string admissionserialnumber, school_web.Global.sendpush item)
        {


            if (gcmid != "")
            {
                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = item.Notice;
                ss["title"] = "Notice";
                ss["messagetype"] = "Notice";
                ss["url"] = "";
                ss["link_url"] = "";
                ss["UserId"] = admissionserialnumber;
                UsesCode.SendNotification(gcmid, ss);
            }
        }

        private void bind_list_push()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Notice_Board_Details] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                push.Clear();
                push.Add(new school_web.Global.sendpush
                {
                    Class1 = "0",
                    Section = "0",
                    Notice = "0",
                    Id = "0",
                    Send_Type = "0",
                    Admission_no = "0",



                });
            }
            else
            {
                push.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    push.Add(new school_web.Global.sendpush
                    {
                        Class1 = dr["Class"].ToString(),
                        Section = dr["Section"].ToString(),
                        Notice = dr["Notice"].ToString(),
                        Id = dr["Id"].ToString(),
                        Send_Type = dr["Send_Type"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),
                    });
                }
            }
        }
        #endregion



        #region send push message to student
        private void send_pushMessage(school_web.Global.sendpushMessage item)
        {
            string query = "";
            //Send_Type = dr["Send_Type"].ToString(),
            //           Admission_no = dr["Admission_no"].ToString(),
            if (item.Send_Type == "Class Wise")
            {

                if (item.Class1 == "ALL")
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where gcm_id is not null ";
                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where gcm_id is not null and Section='" + item.Section + "'";
                    }

                }
                else
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where gcm_id is not null and Class_id='" + item.Class1 + "'";

                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where gcm_id is not null and Class_id='" + item.Class1 + "' and Section='" + item.Section + "'";
                    }

                }
            }
            else
            {
                query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   admissionserialnumber='" + item.Admission_no + "' ";
            }

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Private_Messages set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();

                    final_send_pushmessage(gcmid, admissionserialnumber, item);

                }
                code.executequery("update Private_Messages set Send_Status='Send' where Id=" + item.Id + "");
            }

        }

        private void final_send_pushmessage(string gcmid, string admissionserialnumber, school_web.Global.sendpushMessage item)
        {
            if (gcmid != "")
            {
                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = item.Details;
                ss["title"] = item.Subject;
                ss["messagetype"] = "Message";
                ss["url"] = "";
                ss["link_url"] = "";
                ss["UserId"] = admissionserialnumber;
                UsesCode.SendNotification(gcmid, ss);
            }

        }

        private void bind_list_pushmessage()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Private_Messages] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushMessage.Clear();
                pushMessage.Add(new school_web.Global.sendpushMessage
                {
                    Class1 = "0",
                    Section = "0",
                    Subject = "0",
                    Details = "0",
                    Id = "0",
                    Send_Type = "0",
                    Admission_no = "0",


                });
            }
            else
            {
                pushMessage.Clear();
                foreach (DataRow dr in dt.Rows)
                {

                    pushMessage.Add(new school_web.Global.sendpushMessage
                    {
                        Class1 = dr["Class_Id"].ToString(),
                        Section = dr["Section_Id"].ToString(),
                        Subject = dr["Subject"].ToString(),
                        Details = dr["Details"].ToString(),
                        Id = dr["Id"].ToString(),
                        Send_Type = dr["Send_Type"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),

                    });
                }
            }
        }

        #endregion

        #endregion






        private void BindCount()
        {
            try
            {
                lbl_Teachers.Text = code.Find_Name("select count(id) from user_details where User_Type='Teacher' and Istatus='1'");
                lbl_Students.Text = code.Find_Name("select count(id) from dbo.[admission_registor] where Transfer_Status in ('New','NT') and  StudentStatus='AV' and  Session_id='" + ViewState["sesssionid"].ToString() + "' and   Status='1'");
                lbl_Licence.Text = code.Find_Name("select count(id) from LiveClassCredential");
                lblenroll.Text = code.Find_Name("select count(ip.user_id) from user_details ip join Zoom_Virtual_class_schedule zvcs on ip.user_id=zvcs.Teacher_Id  where   ip.Istatus='1'");
                lbl_duse_syncdate.Text = "0"; //code.Find_Name(" Select count(distinct ar.admissionserialnumber) from admission_registor ar  left join Typewise_fee_collection tfc on ar.admissionserialnumber=tfc.admission_no  where  tfc.admission_no in ( select   distinct admission_no from dbo.[Typewise_fee_collection] where admission_no is not null )  ");


            }
            catch { }
        }

        private void BindChart()
        {
            DataTable dsChartData = new DataTable();
            StringBuilder strScript = new StringBuilder();
            StringBuilder strScriptOnline = new StringBuilder();
            StringBuilder strScriptTeacher = new StringBuilder();
            StringBuilder strScriptStudents = new StringBuilder();

            try
            {
                SqlCommand cmd = new SqlCommand("Chart");
                cmd.Parameters.AddWithValue("@StartDate", hdStartDate.Value);
                cmd.Parameters.AddWithValue("@EndDate", hdEndDate.Value);
                dsChartData = code.GetDatastore(cmd);

                strScript.Append(@" <script type='text/javascript'>  
                            google.load('visualization', '1', {packages: ['corechart']});</script>  
                        <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date', 'Online Classes', 'Students', 'Teacher'],");

                strScriptOnline.Append(@"  
                    <script type='text/javascript'>  
                    function drawOnline() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date', 'Online Classes'],");

                strScriptTeacher.Append(@"  
                    <script type='text/javascript'>  
                    function drawTeacher() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date', 'Teacher'],");

                strScriptStudents.Append(@" 
                    <script type='text/javascript'>  
                    function drawStudents() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date', 'Students'],");

                foreach (DataRow row in dsChartData.Rows)
                {
                    strScript.Append("['" + row["Date"] + "'," + row["OnlineClasses"] + "," + row["TotSTudent"] + "," + row["TotTeacher"] + "],");

                    strScriptOnline.Append("['" + row["Date"] + "'," + row["OnlineClasses"] + "],");

                    strScriptTeacher.Append("['" + row["Date"] + "'," + row["TotTeacher"] + "],");

                    strScriptStudents.Append("['" + row["Date"] + "'," + row["TotSTudent"] + "],");
                }
                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                strScriptOnline.Remove(strScriptOnline.Length - 1, 1);
                strScriptOnline.Append("]);");

                strScriptTeacher.Remove(strScriptTeacher.Length - 1, 1);
                strScriptTeacher.Append("]);");

                strScriptStudents.Remove(strScriptStudents.Length - 1, 1);
                strScriptStudents.Append("]);");

                strScript.Append("var options = { width:1000, height:300,colors: ['#0ba360','#46aef7','#1e3c72'], vAxis: {title: 'Reports'},  hAxis: {title: 'Month'}, seriesType: 'line', series: {3: {type: 'area'}} };");
                strScript.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
                strScript.Append(" </script>");


                strScriptOnline.Append("var options = { width:1000, height:300,colors: ['#0ba360'],vAxis: {title: 'Reports'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {1: {type: 'area'}} };");
                strScriptOnline.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chartOnline'));  chart.draw(data, options); } google.setOnLoadCallback(drawOnline);");
                strScriptOnline.Append(" </script>");

                strScriptTeacher.Append("var options = { width:1000, height:300,colors: ['#1e3c72'],vAxis: {title: 'Reports'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {1: {type: 'area'}} };");
                strScriptTeacher.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chartTeachers'));  chart.draw(data, options); } google.setOnLoadCallback(drawTeacher);");
                strScriptTeacher.Append(" </script>");

                strScriptStudents.Append("var optionsStudents = { width:1000, height:300,colors: ['#46aef7'],vAxis: {title: 'Reports'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {1: {type: 'area'}} };");
                strScriptStudents.Append(" var chartStudent = new google.visualization.ComboChart(document.getElementById('chartStudents'));  chartStudent.draw(data, optionsStudents); } google.setOnLoadCallback(drawStudents);");
                strScriptStudents.Append(" </script>");

                ltScripts.Text = strScript.ToString();
                ltScriptsOnline.Text = strScriptOnline.ToString();
                ltScriptsTeacher.Text = strScriptTeacher.ToString();
                ltScriptsStudents.Text = strScriptStudents.ToString();
            }
            catch
            {
            }
            finally
            {
                dsChartData.Dispose();
                strScript.Clear();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LtTime.Visible = true; lblDateText.Visible = false;
            BindChart(); LtTime.Text = lblDateText.Text = hdIsactive.Value;
        }


    }
}