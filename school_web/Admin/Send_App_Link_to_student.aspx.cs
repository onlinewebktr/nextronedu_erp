using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.IO;
using System.Data;
using System.Net;

namespace school_web.Admin
{
    public partial class Send_App_Link_to_student : System.Web.UI.Page
    {
        My mycode = new My();
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
                        ViewState["flagPosition"] = "1";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) desc ");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        fetch_section();

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        var dt1 = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                        if (dt1.Rows.Count == 1)
                        {
                            ViewState["whatsapp_mobile_no"] = dt1.Rows[0]["SMS_API"].ToString();
                            ViewState["Whatsapp_api_url"] = dt1.Rows[0]["url"].ToString();
                        }
                        else
                        {
                            ViewState["whatsapp_mobile_no"] = "";
                            ViewState["Whatsapp_api_url"] = "";
                        }
                        var dt = mycode.FillData("select top 1 * from message_config where Status='running'");
                        if (dt.Rows.Count == 1)
                        {
                            ViewState["api_key"] = dt.Rows[0]["uid"].ToString();
                            ViewState["Sender_id"] = dt.Rows[0]["sender"].ToString();
                        }
                        else
                        {
                            ViewState["api_key"] = "0";
                            ViewState["Sender_id"] = "0";

                        }

                        Dictionary<string, object> autosms = mycode.get_auto_message_template("Student User id and Password");
                        ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                        ViewState["VariableName"] = (String)autosms["VariableName"];
                        ViewState["SMSType"] = (String)autosms["SMSType"];
                        ViewState["Send_From"] = (String)autosms["Send_From"];
                        ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                        ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];

                        ViewState["tny_app_link_android"] = (String)autosms["tny_app_link_android"];
                        ViewState["tny_app_link_ios"] = (String)autosms["tny_app_link_ios"];

                        string app_url = "for android-" + ViewState["tny_app_link_android"];
                        if (ViewState["tny_app_link_ios"].ToString() != "")
                        {
                            app_url = app_url + " IOS- " + ViewState["tny_app_link_ios"].ToString();
                        }
                        ViewState["app_url"] = app_url;
                        find_by_c_s_a();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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

        protected void btn_fnd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                }
                else if (ddl_secion.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddlclass.Focus();
                }
                else
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        find_by_c_s_a();
                    }
                    else if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        find_by_c_s_a();
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

        private void find_by_c_s_a()
        { 
            string query;
            if (ddlclass.SelectedItem.Text == "ALL" && ddl_secion.Text == "ALL")
            {
                query = "select ar.class, ar.admissionserialnumber,ar.studentname,ar.Section,ar.Session_id,ar.Class_id,ar.rollnumber,ar.Father_whatsApp_no,ar.Pwd from admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where (ar.Transfer_Status='New' or ar.Transfer_Status='NT') and  ar.Status='1' and ar.Session_id='" + ddlsession.SelectedValue + "' order by ad.Position,ar.Section,ar.rollnumber asc";
            }
            else if (ddlclass.SelectedItem.Text == "ALL" && ddl_secion.Text != "ALL")
            {
                query = "select ar.class,ar.admissionserialnumber,ar.studentname,ar.Section,ar.Session_id,ar.Class_id,ar.rollnumber,ar.Father_whatsApp_no,ar.Pwd from admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + ddlsession.SelectedValue + "' and ar.Section='" + ddl_secion.Text + "'    and (ar.Transfer_Status='New' or ar.Transfer_Status='NT') and  ar.Status='1' and ar.StudentStatus='AV' order by ad.Position,ar.Section,ar.rollnumber asc";
            }
            else if (ddlclass.SelectedItem.Text != "ALL" && ddl_secion.Text == "ALL")
            {
                query = "select ar.class,ar.admissionserialnumber,ar.studentname,ar.Section,ar.Session_id,ar.Class_id,ar.rollnumber,ar.Father_whatsApp_no,ar.Pwd from admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + ddlsession.SelectedValue + "' and ar.Class_id='" + ddlclass.SelectedValue + "'      and (ar.Transfer_Status='New' or ar.Transfer_Status='NT') and  ar.Status='1' and ar.StudentStatus='AV' order by ad.Position,ar.Section,ar.rollnumber asc";
            }
            else if (ddlclass.SelectedItem.Text != "ALL" && ddl_secion.Text != "ALL")
            {
                query = "select ar.class,ar.admissionserialnumber,ar.studentname,ar.Section,ar.Session_id,ar.Class_id,ar.rollnumber,ar.Father_whatsApp_no,ar.Pwd from admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id   where ar.Session_id='" + ddlsession.SelectedValue + "' and ar.Class_id='" + ddlclass.SelectedValue + "' and ar.Section='" + ddl_secion.Text + "'    and (ar.Transfer_Status='New' or ar.Transfer_Status='NT') and  ar.Status='1' and ar.StudentStatus='AV' order by ad.Position,ar.Section,ar.rollnumber asc";
            }
            else
            {
                query = "select ar.class,ar.admissionserialnumber,ar.studentname,ar.Section,ar.Session_id,ar.Class_id,ar.rollnumber,ar.Father_whatsApp_no,ar.Pwd from admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id   where ar.Session_id='" + ddlsession.SelectedValue + "' and ar.Section='" + ddl_secion.Text + "'    and (ar.Transfer_Status='New' or ar.Transfer_Status='NT') and  ar.Status='1' and ar.StudentStatus='AV' order by ad.Position,ar.Section,ar.rollnumber asc";
            }

            bind_grd_view(query);
        }

        private void bind_grd_view(string query)
        {
            ViewState["flag"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_save.Visible = false;
                Alertme("Sorry, there is no data list available.", "warning");
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
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            if (txt_admission_no.Text == "")
            {
                Alertme("Please enter admission no.", "warning");
                txt_admission_no.Focus();
            }
            else
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    find_by_admission_no();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    find_by_admission_no();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }

        }
        private void find_by_admission_no()
        {
            bind_grd_view("select top 1 * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT') and  Status='1' and StudentStatus='AV' and Session_id='" + My.get_session_id() + "' order by id desc");
        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_section();
            }
            catch (Exception ex)
            {
            }
        }
        private void fetch_section()
        {
            try
            {
                if (ddlclass.SelectedItem.Text == "ALL")
                {
                    mycode.bind_ddlall(ddl_secion, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "'   order by Section");
                }
                else
                {
                    mycode.bind_ddlall(ddl_secion, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by Section");
                }

            }
            catch
            {

            }
        }
        #region 
        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    int growcount = GrdView.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_admission_no = (Label)GrdView.Rows[i].FindControl("lbl_admission_no");
                            Label lblstudentname = (Label)GrdView.Rows[i].FindControl("lblstudentname");
                            Label lbl_Father_whatsApp_no = (Label)GrdView.Rows[i].FindControl("lbl_Father_whatsApp_no");

                            Label lbl_Section = (Label)GrdView.Rows[i].FindControl("lbl_Section");

                            Label lbl_pwd = (Label)GrdView.Rows[i].FindControl("lbl_pwd");
                            



                            send_sms_student(lbl_admission_no.Text, lblstudentname.Text, lbl_Father_whatsApp_no.Text, lbl_Section.Text, lbl_pwd.Text);


                           


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from student list", "warning");
                    }
                    else
                    {
                        Alertme("The student app login user ID and Password has been successfully sent.", "success");
                        bind_grd_view(ViewState["flag"].ToString());
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    int growcount = GrdView.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_admission_no = (Label)GrdView.Rows[i].FindControl("lbl_admission_no");
                            Label lblstudentname = (Label)GrdView.Rows[i].FindControl("lblstudentname");
                            Label lbl_Father_whatsApp_no = (Label)GrdView.Rows[i].FindControl("lbl_Father_whatsApp_no");
                            Label lbl_pwd = (Label)GrdView.Rows[i].FindControl("lbl_pwd");
                            Label lbl_Section = (Label)GrdView.Rows[i].FindControl("lbl_Section");
                            send_sms_student(lbl_admission_no.Text, lblstudentname.Text, lbl_Father_whatsApp_no.Text, lbl_Section.Text, lbl_pwd.Text);


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from student list", "warning");
                    }
                    else
                    {
                        Alertme("The student app login user ID and Password has been successfully sent.", "success");
                        bind_grd_view(ViewState["flag"].ToString());
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }

        private void send_sms_student(string admission_no, string stuname, string mobilesms, string Section,string pwd)
        {
            string type;
            var vrls = ViewState["VariableName"].ToString().Split(',');
            var lst = new String[vrls.Length];
            string studentname = stuname;
            if (vrls.Length > 0)
            {
                lst[0] = admission_no;
            }
            if (vrls.Length > 1)
            {
                lst[1] = pwd;
            }
            if (vrls.Length > 2)
            {
                lst[2] = ViewState["app_url"].ToString();
            }
            if (vrls.Length > 3)
            {

                lst[3] = "";
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
            string message = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
            if (ViewState["SMSType"].ToString() == "Unicode")
            {
                type = "unicode";
            }
            else
            {
                type = "english";
            }


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
                        User_id = ViewState["Userid"].ToString(),
                        Mesage_Type = msgtype,
                        Groupcode = "SMS",
                        Status = "SEND",
                        Url = url,
                        Message_to_Type = "Student",
                        admin_user_id = txt_admission_no.Text,
                    });
                }

            }
            catch (Exception ex)
            {
                this.Alertme(ex.Message, "warning");
            }


            try
            {
                if (ViewState["Is_Send_WhatsApp"].ToString().ToUpper() == "TRUE")
                {

                    string sms = String.Format(ViewState["SMS_Tempate"].ToString(), lst);

                    try
                    {
                        if (mobilesms.Length > 9)
                        {
                            string message1 = Uri.EscapeDataString(message);
                            string mobile_no = "91" + mobilesms;
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
                                Mobile_No = mobilesms,
                                Message = message,
                                Date = mycode.date(),
                                Idate = mycode.idate(),
                                Time = mycode.time(),
                                Result = results,
                                User_id = ViewState["Userid"].ToString(),
                                Mesage_Type = type,
                                Groupcode = "Wahataap",
                                Status = "SEND",
                                Url = _url,
                                Message_to_Type = "Student",
                                admin_user_id = txt_admission_no.Text,


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
            catch
            {


            }





        }
        #endregion
    }
}