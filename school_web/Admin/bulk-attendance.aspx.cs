using Microsoft.VisualBasic.FileIO;
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
    public partial class bulk_attendance : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "bulk-attendance.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


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


        private void finally_open_student_list()
        {
            grd_student_list.Visible = true;
            download_in_excel();
            grd_student_list.Visible = false;
        }

        private void download_in_excel()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "student-list.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grd_student_list.AllowPaging = false;

            fetch_grid_Data();

            //grd_barcodes_list.Columns[8].Visible = false;
            //Change the Header Row back to white color
            grd_student_list.HeaderRow.Style.Add("background-color", "#00AEFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < grd_student_list.HeaderRow.Cells.Count; i++)
            {
                grd_student_list.HeaderRow.Cells[i].Style.Add("background-color", "#00AEFF");
            }
            grd_student_list.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        private void fetch_grid_Data()
        {
            string query = "";
            if (ddl_section.Text == "ALL")
            {
                query = "Select class,Section,admissionserialnumber,studentname,rollnumber,CASE WHEN studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN studentimagepath is not null THEN studentimagepath END AS Student_img,(select top 1 Course_Name from Add_course_table  where course_id=admission_registor.Class_id) as classname,father_mob,Father_whatsApp_no   from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and status='1' and (Transfer_Status='New' or Transfer_Status='NT') order by rollnumber asc";
            }
            else
            {
                query = "Select class,Section,admissionserialnumber,studentname,rollnumber,CASE WHEN studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN studentimagepath is not null THEN studentimagepath END AS Student_img,(select top 1 Course_Name from Add_course_table  where course_id=admission_registor.Class_id) as classname,father_mob,Father_whatsApp_no   from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and status='1' and (Transfer_Status='New' or Transfer_Status='NT') order by rollnumber asc";
            }

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                grd_student_list.DataSource = null;
                grd_student_list.DataBind();

            }
            else
            {
                grd_student_list.DataSource = dt;
                grd_student_list.DataBind();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        ///==============================================


        protected void btn_download_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (txt_date.Text == "")
                {
                    Alertme("Please select date", "warning");
                }
                else if (ddl_teacher.SelectedItem.Text == "Select")
                {
                    Alertme("Please select teacher", "warning");
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
                        finally_open_student_list();
                    }
                    else if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        finally_open_student_list();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }


                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_upload_attendance_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (txt_date.Text == "")
                {
                    Alertme("Please select date", "warning");
                }
                else if (ddl_teacher.SelectedItem.Text == "Select")
                {
                    Alertme("Please select teacher", "warning");
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

                        if (FileUpload1.HasFile)
                        {
                            btn_final_submit.Visible = true;
                            ViewState["dupAdmiD"] = "0";
                            upload_excel_file();
                        }
                        else
                        {
                            Alertme("Please choose excel.csv file.", "warning");
                            return;
                        }
                    }
                    else if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        if (FileUpload1.HasFile)
                        {
                            btn_final_submit.Visible = true;
                            ViewState["dupAdmiD"] = "0";
                            upload_excel_file();
                        }
                        else
                        {
                            Alertme("Please choose excel.csv file.", "warning");
                            return;
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void upload_excel_file()
        {
            string file = upload_csv_data();
            string csvid = My.auto_serialS("Upload_csvid");
            SqlDataAdapter ad = new SqlDataAdapter("Select * from excel_file", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Csv_file");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = file;
            dr[2] = mycode.date();
            dr[3] = mycode.idate();
            dr[4] = csvid;
            dr[5] = "SUBMITTED";
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }


        private bool check_wrap_or_not(string path)
        {
            try
            {
                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("Admission No.");
                tblReadCSV.Columns.Add("Student Name");
                tblReadCSV.Columns.Add("Roll No.");
                tblReadCSV.Columns.Add("Class");
                tblReadCSV.Columns.Add("Section");
                tblReadCSV.Columns.Add("Attendance");
                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }

                pnl_grid.Visible = true;
                lbl_total.Text = "Total No. of Student : " + tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();

                //==============
                string Admission_no = "";
                string Attendance = "";
                for (int i = 0; i < grvExcelData.Rows.Count; i++)
                {
                    Admission_no = grvExcelData.Rows[i].Cells[0].Text;
                    Attendance = grvExcelData.Rows[i].Cells[5].Text;
                    #region check duplicate
                    string adno = Admission_no;
                    DataTable dt = My.dataTable(" select admissionserialnumber from admission_registor where admissionserialnumber='" + adno + "' and Class_id='" + ddl_class.SelectedValue + "' and  Session_id='" + ddl_session.SelectedValue + "'");
                    if (dt.Rows.Count == 0)
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                        Alertme("Sorry! wrong data.", "warning");
                        btn_final_submit.Visible = false;
                    }
                    else
                    {
                        if (Attendance.ToUpper() == "P" || Attendance.ToUpper() == "A" || Attendance.ToUpper() == "L")
                        {
                        }
                        else
                        {
                            grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                            Alertme("Sorry! wrong attendance data. Only indicate attendance with (P, A, L)", "warning");
                            btn_final_submit.Visible = false;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }



        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (txt_date.Text == "")
                {
                    Alertme("Please select date", "warning");
                }
                else if (ddl_teacher.SelectedItem.Text == "Select")
                {
                    Alertme("Please select teacher", "warning");
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
                    int rocount = grvExcelData.Rows.Count;
                    if (rocount != 0)
                    {
                        for (int i = 0; i < rocount; i++)
                        {
                            string Admission_no = grvExcelData.Rows[i].Cells[0].Text;
                            string Name = grvExcelData.Rows[i].Cells[1].Text;
                            string Roll_no = grvExcelData.Rows[i].Cells[2].Text;
                            string Class_name = grvExcelData.Rows[i].Cells[3].Text;
                            string Section = grvExcelData.Rows[i].Cells[4].Text;
                            string Attendance = grvExcelData.Rows[i].Cells[5].Text;

                            string student_attendance = "A";
                            if (Attendance.ToUpper() == "P")
                            {
                                student_attendance = "Present";
                            }
                            else if (Attendance.ToUpper() == "A")
                            {
                                student_attendance = "Absent";
                            }
                            else if (Attendance.ToUpper() == "L")
                            {
                                student_attendance = "Leave";
                            }

                            finalattendance(student_attendance, Admission_no, Name, Class_name);
                        }
                        Alertme("Result submitted successfully.", "success");
                        pnl_grid.Visible = false;
                        grvExcelData.DataSource = null;
                        grvExcelData.DataBind();
                        finally_open_attendance_list();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }




        private void finalattendance(string student_attendance, string admission, string studentname, string classname)
        {

            Dictionary<string, object> dc2 = My.get_student_info(admission, ddl_session.SelectedValue);
            string mobilesms = (String)dc2["father_mob"];
            string whatsappno = (String)dc2["Father_whatsApp_no"];

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
                    #region sms and whatsaap
                    // sms & whatsapp
                    try
                    {


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
                                            User_id = ddl_teacher.SelectedValue,
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
                    }
                    catch
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
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string strQuery = "Update Student_Attendance_saved_Class_Wise set    Updated_Date=@Updated_Date,Updated_IDate=@Updated_IDate,Updated_Time=@Updated_Time,Updated_By=@Updated_By,Attendance_Status=@Attendance_Status   where Id = @Id";
                SqlCommand cmd1 = new SqlCommand(strQuery);

                cmd1.Parameters.AddWithValue("@Updated_Date", code.date());
                cmd1.Parameters.AddWithValue("@Updated_IDate", code.idate());
                cmd1.Parameters.AddWithValue("@Updated_Time", code.time());
                cmd1.Parameters.AddWithValue("@Updated_By", ddl_teacher.SelectedValue);
                cmd1.Parameters.AddWithValue("@Attendance_Status", student_attendance);
                cmd1.Parameters.AddWithValue("@Id", id);
                if (InsertUpdate.InsertUpdateData(cmd1))
                {


                    #region sms and whatsaap
                    // sms & whatsapp
                    try
                    {


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
                                            User_id = ddl_teacher.SelectedValue,
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
                    }
                    catch
                    {


                    }

                    #endregion



                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "sp_Attendance_Notification_Save";
                    cmd2.Parameters.AddWithValue("@Heading", "Attendance Notification");
                    cmd2.Parameters.AddWithValue("@Details", message);
                    cmd2.Parameters.AddWithValue("@Posted_By", ddl_teacher.SelectedValue);
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


        #region FileSave
        private string upload_csv_data()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("ddMMyyyy");
            string time = dtm.ToString("hhmmss");
            String filerename = date + time;

            string dbfilePath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;

            Session["file1"] = FileUpload1.FileName;
            String FileExtension = Path.GetExtension(Session["file1"].ToString()).ToLower();
            Session["file"] = ("Upload_excel_Class_Subject_Mapping" + filerename + FileExtension);
            String[] allowedExtensions = { ".csv" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtensions[i])
                {

                    FileOK = true;
                    break;
                }
                else
                {
                }
            }

            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["file"]);

                    if (check_wrap_or_not((path + "/" + Session["file"])))
                    {
                        FileSaved = true;
                    }
                    else
                    {
                        File.Delete((path + "/" + Session["file"]));
                        FileSaved = false;
                    }
                }
                catch (Exception ex)
                {
                    FileSaved = false;

                    Alertme(ex.ToString(), "warning");
                }
            }
            else
            {
                dbfilePath = "Choose only csv File";
                return dbfilePath;
            }
            if (FileSaved)
            {

                string fileName = Path.GetFileName(Session["file"].ToString());
                dbfilePath = @"/Master_Img/Student/" + fileName;
                Session["fileName"] = fileName;
            }
            return dbfilePath;
        }
        #endregion




        private void finally_open_attendance_list()
        {
            string query = "Select admissionserialnumber,studentname,rollnumber,CASE WHEN studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN studentimagepath is not null THEN studentimagepath END AS Student_img   from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and status='1' order by rollnumber asc";
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
    }
}