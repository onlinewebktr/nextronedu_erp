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
using System.Text.RegularExpressions;

namespace school_web.Admin
{
    public partial class send_sms : System.Web.UI.Page
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
                        ViewState["sessionid"] = My.get_session_id();
                        ViewState["Userid"] = Session["Admin"].ToString();
                   
                        mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_All_New(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddl_class.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        //ddl_section.Text = My.get_top_one_section();

                        //  mycode.bind_all_ddl_with_id(ddl_template, "select   SMS_Tempate, id from SMS_Template_Setting where Is_Send_WhatsApp!=1 ");
                        mycode.bind_all_ddl_with_id_na(ddl_template, "select   Send_From, id from SMS_Template_Setting where Is_Send_WhatsApp!=1 ");
                        ddl_template.SelectedValue = "0";
                        bind_data_if_student();
                        section1.Visible = true;

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Active_Inactive_Student");
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
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                bind_data_if_student();
            }
            //else if (ddl_class.SelectedItem.Text == "ALL")
            //{
            //    query = "Select ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.class,ar.studentname,ar.father_mob,ar.mother_mob from admission_registor ar join Add_course_table act on ar.Class_id=act.course_id  where ar.Session_id=" + ddl_session.SelectedValue + " order by act.Position ";
            //    Bind_studnet_grid_data(query);
            //}
            //else
            //{
            //    query = "Select ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.class,ar.studentname,ar.father_mob,ar.mother_mob from admission_registor ar join Add_course_table act on ar.Class_id=act.course_id where ar.Session_id=" + ddl_session.SelectedValue + " and ar.Class_id=" + ddl_class.SelectedValue + " order by act.Position ";
            //    Bind_studnet_grid_data(query);
            //}
        }

        private void Bind_studnet_grid_data(string query)
        {
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_studentname.DataSource = null;
                rd_studentname.DataBind();
            }
            else
            {
                rd_studentname.DataSource = dt;
                rd_studentname.DataBind();
            }

        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_data_if_student();

        }

        private void bind_data_if_student()
        {
            string query = "";
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                try
                {
                    if (ddl_class.SelectedItem.Text == "ALL")
                    {
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' order by Section");
                    }
                    else
                    {
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' order by Section");
                    }

                }
                catch
                {
                    mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' order by Section");
                }

                if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
                {
                    query = "Select ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.class,ar.studentname,ar.father_mob,ar.mother_mob,ar.Father_whatsApp_no,ar.Mother_whatsApp_no from admission_registor ar join Add_course_table act on ar.Class_id=act.course_id  where ar.Session_id=" + ddl_session.SelectedValue + " and ar.Status=1 order by act.Position,ar.rollnumber ";
                    Bind_studnet_grid_data(query);

                }
                else if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.Text != "ALL")
                {
                    query = "Select ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.class,ar.studentname,ar.father_mob,ar.mother_mob,ar.Father_whatsApp_no,ar.Mother_whatsApp_no from admission_registor ar join Add_course_table act on ar.Class_id=act.course_id  where ar.Session_id=" + ddl_session.SelectedValue + " and ar.Section='" + ddl_section.Text + "' and ar.Status=1 order by act.Position,ar.rollnumber ";
                    Bind_studnet_grid_data(query);

                }
                else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
                {
                    query = "Select ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.class,ar.studentname,ar.father_mob,ar.mother_mob,ar.Father_whatsApp_no,ar.Mother_whatsApp_no from admission_registor ar join Add_course_table act on ar.Class_id=act.course_id  where ar.Session_id=" + ddl_session.SelectedValue + " and ar.Class_id=" + ddl_class.SelectedValue + " and ar.Status=1 order by act.Position,ar.rollnumber  ";
                    Bind_studnet_grid_data(query);

                }
                else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
                {
                    query = "Select ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.class,ar.studentname,ar.father_mob,ar.mother_mob,ar.Father_whatsApp_no,ar.Mother_whatsApp_no from admission_registor ar join Add_course_table act on ar.Class_id=act.course_id  where ar.Session_id=" + ddl_session.SelectedValue + " and ar.Class_id=" + ddl_class.SelectedValue + " and ar.Section='" + ddl_section.Text + "' and ar.Status=1 order by act.Position,ar.rollnumber  ";
                    Bind_studnet_grid_data(query);

                }
                else
                {
                    query = "Select ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.class,ar.studentname,ar.father_mob,ar.mother_mob,ar.Father_whatsApp_no,ar.Mother_whatsApp_no from admission_registor ar join Add_course_table act on ar.Class_id=act.course_id  where ar.Session_id=" + ddl_session.SelectedValue + " and ar.Class_id=" + ddl_class.SelectedValue + " and ar.Section='" + ddl_section.Text + "' and ar.Status=1  order by act.Position,ar.rollnumber  ";
                    Bind_studnet_grid_data(query);
                }


            }
        }

        protected void lbl_mobile_no_Click(object sender, EventArgs e)
        {

            LinkButton lnk = (LinkButton)sender;
            if (lnk.CssClass == "text-black")
            {
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");

                var mobile = lnk.Text;
                if (mobile.Length > 10)
                {
                    mobile = mobile.Substring(mobile.Length - 10, 10);
                }
                if (validMobile(mobile))
                {

                    var mais = new List<String>();
                    if (txt_sent.Text.Trim() != "")
                        mais.AddRange(txt_sent.Text.Split(','));

                    var mais_userid = new List<String>();

                    if (lbl_admission_nooutput.Text.Trim() != "")
                        mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));

                    if (!mais.Contains(mobile))
                    {
                        mais.Add(mobile);
                    }

                    if (!mais_userid.Contains(lbl_admissionserialnumber.Text))
                    {
                        mais_userid.Add(lbl_admissionserialnumber.Text);
                    }
                    txt_sent.Text = string.Join(",", mais);
                    lbl_admission_nooutput.Text = string.Join(",", mais_userid);
                    lnk.CssClass = "btn btn-sm  btn-success";


                }
                else
                {


                    Alertme("Invalid Mobile No", "warning");
                }
            }
            else
            {
                try
                {

                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                    var mobile = lnk.Text;
                    if (mobile.Length > 10)
                    {
                        mobile = mobile.Substring(mobile.Length - 10, 10);
                    }
                    var mais = new List<String>();
                    if (txt_sent.Text.Trim() != "")
                        mais.AddRange(txt_sent.Text.Split(','));


                    if (mais.Contains(mobile))
                    {
                        mais.Remove(mobile);
                    }
                    var mais_userid = new List<String>();

                    if (lbl_admission_nooutput.Text.Trim() != "")
                        mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));

                    if (!mais_userid.Contains(lbl_admissionserialnumber.Text))
                    {
                        mais_userid.Remove(lbl_admissionserialnumber.Text);
                    }
                    txt_sent.Text = string.Join(",", mais);
                    lbl_admission_nooutput.Text = string.Join(",", mais_userid);

                }
                catch
                {
                }
                //  lnk.Text = "Select";
                lnk.CssClass = "text-black";
            }
        }
        protected void chk_mobile_CheckedChanged(object sender, EventArgs e)
        {
            ViewState["admission_no"] = "";
            CheckBox chk = (CheckBox)sender;
            var lbl = "lbl_mobile_no";

            var regid = "lbl_admissionserialnumber";


            if (chk.ID == "chk_alt_mobile")
            {
                lbl = "lbl_AltMobileNo";
            }
            else if (chk.ID == "chk_wa_mobile")
            {
                lbl = "lbl_WatsappNo";
            }

            if (chk.Checked)
            {
                var mais = new List<String>();
                if (txt_sent.Text.Trim() != "")
                    mais.AddRange(txt_sent.Text.Split(','));


                var mais_userid = new List<String>();
                if (lbl_admission_nooutput.Text.Trim() != "")
                    mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));

                foreach (RepeaterItem itm in rd_studentname.Items)
                {
                    var mobile_no = (LinkButton)itm.FindControl(lbl);
                    var admission_no = (Label)itm.FindControl(regid);

                    var mobile = mobile_no.Text;
                    if (mobile.Length > 10)
                    {
                        mobile = mobile.Substring(mobile.Length - 10, 10);
                    }
                    if (validMobile(mobile))
                    {
                        if (!mais.Contains(mobile))
                        {
                            mais.Add(mobile);
                        }
                        mobile_no.CssClass = "btn btn-sm  btn-success";
                    }

                    if (!mais_userid.Contains(admission_no.Text))
                    {
                        mais_userid.Add(admission_no.Text);
                    }



                }
                txt_sent.Text = string.Join(",", mais);
                lbl_admission_nooutput.Text = string.Join(",", mais_userid);

            }
            else
            {
                ViewState["admission_no"] = "";
                var mais = new List<String>();
                if (txt_sent.Text.Trim() != "")
                    mais.AddRange(txt_sent.Text.Split(','));


                var mais_userid = new List<String>();
                if (lbl_admission_nooutput.Text.Trim() != "")
                    mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));

                foreach (RepeaterItem itm in rd_studentname.Items)
                {
                    var mobile_no = (LinkButton)itm.FindControl(lbl);
                    var admission_no = (Label)itm.FindControl(regid);
                    mobile_no.CssClass = "text-black";
                    var mobile = mobile_no.Text;
                    if (mobile.Length > 10)
                    {
                        mobile = mobile.Substring(mobile.Length - 10, 10);
                    }
                    if (mais.Contains(mobile))
                    {
                        mais.Remove(mobile);
                    }
                    if (mais_userid.Contains(admission_no.Text))
                    {
                        mais_userid.Remove(admission_no.Text);
                    }

                }

                txt_sent.Text = string.Join(",", mais);
                lbl_admission_nooutput.Text = string.Join(",", mais_userid);
            }
        }


        private bool validMobile(string text)
        {
            if (text.Trim().Length == 10)
            {
                string strRegex = @"^[6-9][0-9]{9}$";
                Regex re = new Regex(strRegex);
                if (re.IsMatch(text))
                    return (true);
                else
                    return (false);
            }
            return false;
        }
        protected void ddl_template_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_template.SelectedItem.Text == "NA")
            {
                whatassp.Visible = true;
                sms.Visible = false;
                txt_WhatsApp_message.Text = "";
            }
            else
            {
                whatassp.Visible = false;
                sms.Visible = true;
                find_templatedata();
            }


        }

        private void find_templatedata()
        {
            btn_msgPreview.Visible = true;
            btn_Submit.Visible = true;

            pnl_0.Visible = pnl_1.Visible = pnl_2.Visible = pnl_3.Visible = pnl_4.Visible = pnl_5.Visible = pnl_6.Visible = pnl_7.Visible = pnl_8.Visible = false;

            if (ddl_template.SelectedIndex <= 0)
                return;
            var id = ddl_template.SelectedValue;
            var dt = mycode.FillData("select  * from SMS_Template_Setting where id='" + id + "'");
            var vrls = dt.Rows[0]["VariableName"].ToString().Split(',');
            txt_message.Text = ddl_template.SelectedItem.Text;
            if (vrls.Length > 0)
            {
                pnl_0.Visible = true;
                lbl_0.Text = vrls[0];
                txt_0.Text = "";
            }
            if (vrls.Length > 1)
            {
                pnl_1.Visible = true;
                lbl_1.Text = vrls[1];
                txt_1.Text = "";
            }
            if (vrls.Length > 2)
            {
                pnl_2.Visible = true;
                lbl_2.Text = vrls[2];
                txt_2.Text = "";
            }
            if (vrls.Length > 3)
            {
                pnl_3.Visible = true;
                lbl_3.Text = vrls[3];
                txt_3.Text = "";
            }
            if (vrls.Length > 4)
            {
                pnl_4.Visible = true;
                lbl_4.Text = vrls[4];
                txt_4.Text = "";
            }
            if (vrls.Length > 5)
            {
                pnl_5.Visible = true;
                lbl_5.Text = vrls[5];
                txt_5.Text = "";
            }
            if (vrls.Length > 6)
            {
                pnl_6.Visible = true;
                lbl_6.Text = vrls[6];
                txt_6.Text = "";
            }
            if (vrls.Length > 7)
            {
                pnl_7.Visible = true;
                lbl_7.Text = vrls[7];
                txt_7.Text = "";
            }
            if (vrls.Length > 8)
            {
                pnl_8.Visible = true;
                lbl_8.Text = vrls[8];
                txt_8.Text = "";
            }
        }

        protected void btn_msgPreview_Click(object sender, EventArgs e)
        {
            if (ddl_template.SelectedItem.Text == "Select")
            {
                Alertme("Please choose sms template", "warning");
            }
            else
            {
                var id = ddl_template.SelectedValue;
                var dt = mycode.FillData("select  * from SMS_Template_Setting where id='" + id + "'");
                if (dt.Rows.Count == 0)
                    return;
                var vrls = dt.Rows[0]["VariableName"].ToString().Split(',');
                var lst = new String[vrls.Length];
                if (vrls.Length > 0)
                {
                    lst[0] = txt_0.Text;
                }
                if (vrls.Length > 1)
                {
                    lst[1] = txt_1.Text;
                }
                if (vrls.Length > 2)
                {
                    lst[2] = txt_2.Text;
                }
                if (vrls.Length > 3)
                {

                    lst[3] = txt_3.Text;
                }
                if (vrls.Length > 4)
                {

                    lst[4] = txt_4.Text;
                }
                if (vrls.Length > 5)
                {

                    lst[5] = txt_5.Text;
                }
                if (vrls.Length > 6)
                {

                    lst[6] = txt_6.Text;
                }
                if (vrls.Length > 7)
                {

                    lst[7] = txt_7.Text;
                }
                if (vrls.Length > 8)
                {

                    lst[8] = txt_8.Text;
                }
                txt_message.Text = String.Format(dt.Rows[0]["SMS_Tempate"].ToString(), lst);
                pnl_msg.Visible = true;

            }

        }

        #region send message
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_sent.Text == "")
            {
                Alertme("Please select mobile", "warning");
            }
            else if (ddl_template.SelectedItem.Text == "Select")
            {
                Alertme("Please select template", "warning");
            }
            else
            {
                string numbers = txt_sent.Text;
                string admission_nooutput = lbl_admission_nooutput.Text;

                string[] mobileList = numbers.Split(',');
                string[] admissionList = admission_nooutput.Split(',');

                int limit = mobileList.Length; //Math.Min(mobileList.Length, admissionList.Length);

                for (int i = 0; i < limit; i++)
                {
                    string cleanMobile = mobileList[i].Trim();

                    // 🔹 null / empty check
                    string admissionNo = "0";

                    if (i < admissionList.Length && !string.IsNullOrWhiteSpace(admissionList[i]))
                    {
                        admissionNo = admissionList[i].Trim();
                    }

                    sendSMS(cleanMobile, admissionNo);
                }

                if (ddl_type_by.Text == "Student")
                {
                    bind_data_if_student();
                }
                else
                {
                    Find_all_uaer();
                }

                txt_sent.Text = "";
                ddl_template.SelectedValue = "0";
                find_templatedata();
                pnl_msg.Visible = false;
                txt_message.Text = "";
                lbl_admission_nooutput.Text = "";
                txt_WhatsApp_message.Text = "";
                btn_msgPreview.Visible = false;
                btn_Submit.Visible = false;
                Alertme("Message has been sent successfully.", "warning");




            }

        }

        private void sendSMS(string mobileno,string admissionNo)
        {
            var id = ddl_template.SelectedValue;
            var dt1 = mycode.FillData("select top 1 * from SMS_Template_Setting where id='" + id + "'");
            if (dt1.Rows.Count == 0)
                return;
            ViewState["Wid"] = dt1.Rows[0]["Wid"].ToString();
            var vrls = dt1.Rows[0]["VariableName"].ToString().Split(',');
           

            var lst = new String[vrls.Length];
            if (vrls.Length > 0)
            {
                lst[0] = txt_0.Text;
            }
            if (vrls.Length > 1)
            {
                lst[1] = txt_1.Text;
            }
            if (vrls.Length > 2)
            {
                lst[2] = txt_2.Text;
            }
            if (vrls.Length > 3)
            {

                lst[3] = txt_3.Text;
            }
            if (vrls.Length > 4)
            {

                lst[4] = txt_4.Text;
            }
            if (vrls.Length > 5)
            {

                lst[5] = txt_5.Text;
            }
            if (vrls.Length > 6)
            {

                lst[6] = txt_6.Text;
            }
            if (vrls.Length > 7)
            {

                lst[7] = txt_7.Text;
            }
            if (vrls.Length > 8)
            {

                lst[8] = txt_8.Text;
            }
            txt_message.Text = String.Format(dt1.Rows[0]["SMS_Tempate"].ToString(), lst);


            if (rd_whatassp.Checked == true)
            {
                if (txt_sent.Text == "")
                {
                    Alertme("Please select or enter mobile", "warning");
                    txt_sent.Focus();
                }
                else
                {
                    var dt = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                    if (dt.Rows.Count == 1)
                    {
                        string whatsapp_mobile_no = dt.Rows[0]["SMS_API"].ToString();
                        string Whatsapp_api_url = dt.Rows[0]["url"].ToString();
                        try
                        {
                            //if (father_mob.Length > 9)
                            //{
                            string message = txt_message.Text;

                            var query = new Dictionary<string, string>()
{
    { "authkey", whatsapp_mobile_no },
    { "mobile", mobileno },
    { "country_code", "91" },
    { "wid", ViewState["Wid"].ToString()}
};

                            // 🔹 dynamic variables add (same output जैसा आपका चाहिए)
                            if (lst != null && lst.Length > 0)
                            {
                                int limit = Math.Min(lst.Length, 9);

                                for (int i = 0; i < limit; i++)
                                {
                                    if (!string.IsNullOrEmpty(lst[i]))
                                    {
                                        query.Add((i + 1).ToString(), lst[i]);
                                    }
                                }
                            }
                            string url = Whatsapp_api_url;
                            string fullUrl = url + "?" + string.Join("&",
                                query.Select(kvp => kvp.Key + "=" + Uri.EscapeDataString(kvp.Value)));
                            if (mobileno.Length > 9)
                            {

                                My.Insert("WhatsApp_send", new
                                {
                                    Mobile_no = mobileno,
                                    Message = message,
                                    Message_url = fullUrl,
                                    Session_id = ViewState["sessionid"].ToString(),
                                    Admission_no = admissionNo,
                                    Status = "Pending",
                                    Date = mycode.date(),
                                    Idate = mycode.idate(),
                                    Time = mycode.time(),
                                    Send_by = ViewState["Userid"].ToString(),
                                    Mesage_Type = "English",
                                });
                            }


                          

                            //}
                            //return true;
                        }
                        catch (Exception ex)
                        {
                            My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                            //return false;
                        }

                    }
                    else
                    {
                        this.Alertme("Please set WhatsApp configuration", "warning");
                    }
                }

            }
            else
            {


                var dt = mycode.FillData("select top 1 * from message_config where Status='running'");
                if (dt.Rows.Count == 1)
                {
                    try
                    {
                        string api_key = dt.Rows[0]["uid"].ToString();
                        string Sender_id = dt.Rows[0]["sender"].ToString();
                        string type = "";
                        if (dt1.Rows[0]["SMSType"].ToString() == "Unicode")
                        {
                            type = "unicode";
                        }
                        else
                        {
                            type = "english";
                        }

                        string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + txt_message.Text + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + txt_sent.Text + "&smsContentType=" + type;
                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        this.Alertme("SMS Send Successfully", "success");
                        My.Insert("Message_Details", new
                        {
                            Mobile_No = txt_sent.Text,
                            Message = txt_message.Text,
                            Date = mycode.date(),
                            Idate = mycode.idate(),
                            Time = mycode.time(),
                            Result = results,
                            User_id = ViewState["Userid"].ToString(),
                            Mesage_Type = type,
                            Groupcode = "Bulk",
                            Status = "SEND",
                            Url = url,
                            Message_to_Type = ddl_type_by.Text,
                            admin_user_id = lbl_admission_nooutput.Text,


                        });

                        if (ddl_type_by.Text == "Student")
                        {
                            bind_data_if_student();
                        }
                        else
                        {
                            Find_all_uaer();
                        }

                        txt_sent.Text = "";
                        ddl_template.SelectedValue = "0";
                        find_templatedata();
                        pnl_msg.Visible = false;
                        txt_message.Text = "";
                        lbl_admission_nooutput.Text = "";
                        btn_msgPreview.Visible = false;
                        btn_Submit.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        this.Alertme(ex.Message, "warning");
                    }
                }
                else
                {
                    this.Alertme("Please set sms configuration", "warning");
                }
            }
        }
        #endregion

        protected void ddl_type_by_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_type_by.Text == "Student")
            {
                section1.Visible = true;
                section2.Visible = true;
                section2_user.Visible = false;
                find_templatedata();
            }
            else
            {
                section1.Visible = false;
                section2.Visible = false;
                section2_user.Visible = true;
                Find_all_uaer();

            }
        }


        #region user
        private void Find_all_uaer()
        {
            string query = "Select * from PRL_Employee_Master where Status='Active' ";
            Bind_user_grid_data(query);

        }

        private void Bind_user_grid_data(string query)
        {
            ViewState["query2"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                Repeater1.DataSource = null;
                Repeater1.DataBind();

            }
            else
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        protected void CheckBox1_user_CheckedChanged(object sender, EventArgs e)
        {
            ViewState["admission_no"] = "";
            CheckBox chk = (CheckBox)sender;
            var lbl = "lbl_mobile_no_user";
            var regid = "lbl_Emp_Code";
            if (chk.ID == "chk_alt_mobile")
            {
                lbl = "lbl_AltMobileNo";
            }
            else if (chk.ID == "chk_wa_mobile")
            {
                lbl = "lbl_WatsappNo";
            }

            if (chk.Checked)
            {
                var mais = new List<String>();
                if (txt_sent.Text.Trim() != "")
                    mais.AddRange(txt_sent.Text.Split(','));

                var mais_userid = new List<String>();
                if (lbl_admission_nooutput.Text.Trim() != "")
                    mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));


                foreach (RepeaterItem itm in Repeater1.Items)
                {
                    var mobile_no = (LinkButton)itm.FindControl(lbl);
                    var admission_no = (Label)itm.FindControl(regid);
                    var mobile = mobile_no.Text;
                    if (mobile.Length > 10)
                    {
                        mobile = mobile.Substring(mobile.Length - 10, 10);
                    }
                    if (validMobile(mobile))
                    {
                        if (!mais.Contains(mobile))
                        {
                            mais.Add(mobile);
                        }
                        mobile_no.CssClass = "btn btn-sm  btn-success";
                    }
                    if (mais_userid.Contains(admission_no.Text))
                    {
                        mais_userid.Remove(admission_no.Text);
                    }
                }
                txt_sent.Text = string.Join(",", mais);
                lbl_admission_nooutput.Text = string.Join(",", mais_userid);

            }
            else
            {
                var mais = new List<String>();
                if (txt_sent.Text.Trim() != "")
                    mais.AddRange(txt_sent.Text.Split(','));

                var mais_userid = new List<String>();
                if (lbl_admission_nooutput.Text.Trim() != "")
                    mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));

                foreach (RepeaterItem itm in Repeater1.Items)
                {
                    var mobile_no = (LinkButton)itm.FindControl(lbl);
                    var admission_no = (Label)itm.FindControl(regid);
                    mobile_no.CssClass = "text-black";
                    var mobile = mobile_no.Text;
                    if (mobile.Length > 10)
                    {
                        mobile = mobile.Substring(mobile.Length - 10, 10);
                    }
                    if (mais.Contains(mobile))
                    {
                        mais.Remove(mobile);
                    }
                    if (mais_userid.Contains(admission_no.Text))
                    {
                        mais_userid.Remove(admission_no.Text);
                    }

                }
                txt_sent.Text = string.Join(",", mais);
                lbl_admission_nooutput.Text = string.Join(",", mais_userid);
            }
        }



        protected void lbl_mobile_no_user_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Emp_Code = (Label)row.FindControl("lbl_Emp_Code");

            if (lnk.CssClass == "text-black")
            {
                var mobile = lnk.Text;
                if (mobile.Length > 10)
                {
                    mobile = mobile.Substring(mobile.Length - 10, 10);
                }
                if (validMobile(mobile))
                {

                    var mais = new List<String>();
                    if (txt_sent.Text.Trim() != "")
                        mais.AddRange(txt_sent.Text.Split(','));

                    var mais_userid = new List<String>();
                    if (lbl_admission_nooutput.Text.Trim() != "")
                        mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));


                    if (!mais.Contains(mobile))
                    {
                        mais.Add(mobile);
                    }
                    if (!mais_userid.Contains(lbl_Emp_Code.Text))
                    {
                        mais_userid.Add(lbl_Emp_Code.Text);
                    }



                    txt_sent.Text = string.Join(",", mais);
                    lbl_admission_nooutput.Text = string.Join(",", mais_userid);
                    lnk.CssClass = "btn btn-sm  btn-success";
                }
                else
                {


                    Alertme("Invalid Mobile No", "warning");
                }
            }
            else
            {
                try
                {
                    var mobile = lnk.Text;
                    if (mobile.Length > 10)
                    {
                        mobile = mobile.Substring(mobile.Length - 10, 10);
                    }
                    var mais = new List<String>();
                    if (txt_sent.Text.Trim() != "")
                        mais.AddRange(txt_sent.Text.Split(','));

                    var mais_userid = new List<String>();
                    if (lbl_admission_nooutput.Text.Trim() != "")
                        mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));

                    if (mais.Contains(mobile))
                    {
                        mais.Remove(mobile);
                    }
                    if (mais_userid.Contains(lbl_Emp_Code.Text))
                    {
                        mais_userid.Remove(lbl_Emp_Code.Text);
                    }
                    txt_sent.Text = string.Join(",", mais);
                    lbl_admission_nooutput.Text = string.Join(",", mais_userid);


                }
                catch
                {
                }
                //  lnk.Text = "Select";
                lnk.CssClass = "text-black";
            }
        }

        #endregion


        #region send_WhatsApp


        protected void rd_whatassp_CheckedChanged(object sender, EventArgs e)
        {


            mycode.bind_all_ddl_with_id_na(ddl_template, "select   Send_From, id from SMS_Template_Setting where Is_Send_WhatsApp!=1 ");

            whatassp.Visible = false;
            sms.Visible = true;
            txt_WhatsApp_message.Text = "";


        }

        protected void rd_sms_CheckedChanged(object sender, EventArgs e)
        {
            mycode.bind_all_ddl_with_id(ddl_template, "select   Send_From, id from SMS_Template_Setting where Is_Send_WhatsApp!=1 ");
            whatassp.Visible = false;
            sms.Visible = true;

        }

        protected void btn_send_WhatsApp_Click(object sender, EventArgs e)
        {
            try
            {



                if (txt_sent.Text == "")
                {
                    Alertme("Please select or enter mobile", "warning");
                    txt_sent.Focus();
                }
                else if (txt_WhatsApp_message.Text == "")
                {
                    Alertme("Please enter message", "warning");
                    txt_WhatsApp_message.Focus();
                }
                else
                {
                    var dt = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                    if (dt.Rows.Count == 1)
                    {
                        string whatsapp_mobile_no = dt.Rows[0]["SMS_API"].ToString();
                        string Whatsapp_api_url = dt.Rows[0]["url"].ToString();
                        try
                        {
                            //if (father_mob.Length > 9)
                            //{
                            string message = Uri.EscapeDataString(txt_WhatsApp_message.Text);
                            string mobile_no = txt_sent.Text; //"91" + father_mob;
                            string _url = "";




                            if (Whatsapp_api_url.Contains("app.allexpert.in"))
                            {
                                //exampe url
                                //https://app.allexpert.in/send-message?api_key=q0hRX9FA1z5S7JttEIzLtxJU3lvoOv&sender=62888xxxx&number=9162888xxxx&message=Hello World
                                _url = String.Format(Whatsapp_api_url, whatsapp_mobile_no, mobile_no, message);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                            }
                            else if (Whatsapp_api_url.Contains("api4ws.com"))
                            {
                                // _url = My.Whatsapp_api_url + My.whatsapp_mobile_no + "&message=" + message + "&phone=91" + mobile_no;
                                //https://api4ws.com/sendMessage.php?AUTH_KEY=918877804016&message=User Defined Whatsapp Message&phone=919812345678
                                _url = String.Format(Whatsapp_api_url, whatsapp_mobile_no, message, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                            }
                            else
                            {

                                //https://fastwsapi.com/sendMessage.php?AUTH_KEY=DEMOEDUNEXTG@123&instance_id=575051&message=User Defined Whatsapp Message&phone=919812345678

                                _url = String.Format(Whatsapp_api_url, whatsapp_mobile_no, message, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                            }

                            //ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                            StreamReader sr = new StreamReader(httpres.GetResponseStream());
                            string results = sr.ReadToEnd();
                            sr.Close();
                            this.Alertme("WhatsApp Message Send Successfully", "success");
                            My.Insert("Message_Details", new
                            {
                                Mobile_No = txt_sent.Text,
                                Message = txt_WhatsApp_message.Text,
                                Date = mycode.date(),
                                Idate = mycode.idate(),
                                Time = mycode.time(),
                                Result = results,
                                User_id = ViewState["Userid"].ToString(),
                                Mesage_Type = "English",
                                Groupcode = "Wahataap",
                                Status = "SEND",
                                Url = _url,
                                Message_to_Type = ddl_type_by.Text,
                                admin_user_id = lbl_admission_nooutput.Text,


                            }); ;
                            if (ddl_type_by.Text == "Student")
                            {
                                bind_data_if_student();
                            }
                            else
                            {
                                Find_all_uaer();
                            }

                            txt_sent.Text = "";
                            ddl_template.SelectedValue = "0";
                            find_templatedata();
                            pnl_msg.Visible = false;
                            txt_message.Text = "";
                            lbl_admission_nooutput.Text = "";
                            txt_WhatsApp_message.Text = "";


                            //}
                            //return true;
                        }
                        catch (Exception ex)
                        {
                            My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                            //return false;
                        }

                    }
                    else
                    {
                        this.Alertme("Please set WhatsApp configuration", "warning");
                    }
                }

            }

            catch
            {

            }
        }
        #endregion



        protected void lnk_father_whatsApp_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            if (lnk.CssClass == "text-black")
            {
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");

                var mobile = lnk.Text;
                if (mobile.Length > 10)
                {
                    mobile = mobile.Substring(mobile.Length - 10, 10);
                }
                if (validMobile(mobile))
                {

                    var mais = new List<String>();
                    if (txt_sent.Text.Trim() != "")
                        mais.AddRange(txt_sent.Text.Split(','));

                    var mais_userid = new List<String>();

                    if (lbl_admission_nooutput.Text.Trim() != "")
                        mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));

                    if (!mais.Contains(mobile))
                    {
                        mais.Add(mobile);
                    }

                    if (!mais_userid.Contains(lbl_admissionserialnumber.Text))
                    {
                        mais_userid.Add(lbl_admissionserialnumber.Text);
                    }
                    txt_sent.Text = string.Join(",", mais);
                    lbl_admission_nooutput.Text = string.Join(",", mais_userid);
                    lnk.CssClass = "btn btn-sm  btn-success";


                }
                else
                {


                    Alertme("Invalid Mobile No", "warning");
                }
            }
            else
            {
                try
                {

                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                    var mobile = lnk.Text;
                    if (mobile.Length > 10)
                    {
                        mobile = mobile.Substring(mobile.Length - 10, 10);
                    }
                    var mais = new List<String>();
                    if (txt_sent.Text.Trim() != "")
                        mais.AddRange(txt_sent.Text.Split(','));


                    if (mais.Contains(mobile))
                    {
                        mais.Remove(mobile);
                    }
                    var mais_userid = new List<String>();

                    if (lbl_admission_nooutput.Text.Trim() != "")
                        mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));

                    if (!mais_userid.Contains(lbl_admissionserialnumber.Text))
                    {
                        mais_userid.Remove(lbl_admissionserialnumber.Text);
                    }
                    txt_sent.Text = string.Join(",", mais);
                    lbl_admission_nooutput.Text = string.Join(",", mais_userid);

                }
                catch
                {
                }
                //  lnk.Text = "Select";
                lnk.CssClass = "text-black";
            }
        }

        protected void chk_mobilefatherwhatsaap_CheckedChanged(object sender, EventArgs e)
        {
            ViewState["admission_no"] = "";
            CheckBox chk = (CheckBox)sender;
            var lbl = "lbl_mobile_no";

            var regid = "lbl_admissionserialnumber";


            if (chk.ID == "chk_alt_mobile")
            {
                lbl = "lbl_AltMobileNo";
            }
            else if (chk.ID == "chk_mobilefatherwhatsaap")
            {
                lbl = "lnk_father_whatsApp";
            }

            if (chk.Checked)
            {
                var mais = new List<String>();
                if (txt_sent.Text.Trim() != "")
                    mais.AddRange(txt_sent.Text.Split(','));


                var mais_userid = new List<String>();
                if (lbl_admission_nooutput.Text.Trim() != "")
                    mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));

                foreach (RepeaterItem itm in rd_studentname.Items)
                {
                    var mobile_no = (LinkButton)itm.FindControl(lbl);
                    var admission_no = (Label)itm.FindControl(regid);

                    var mobile = mobile_no.Text;
                    if (mobile.Length > 10)
                    {
                        mobile = mobile.Substring(mobile.Length - 10, 10);
                    }
                    if (validMobile(mobile))
                    {
                        if (!mais.Contains(mobile))
                        {
                            mais.Add(mobile);
                        }
                        mobile_no.CssClass = "btn btn-sm  btn-success";
                    }

                    if (!mais_userid.Contains(admission_no.Text))
                    {
                        mais_userid.Add(admission_no.Text);
                    }



                }
                txt_sent.Text = string.Join(",", mais);
                lbl_admission_nooutput.Text = string.Join(",", mais_userid);

            }
            else
            {
                ViewState["admission_no"] = "";
                var mais = new List<String>();
                if (txt_sent.Text.Trim() != "")
                    mais.AddRange(txt_sent.Text.Split(','));


                var mais_userid = new List<String>();
                if (lbl_admission_nooutput.Text.Trim() != "")
                    mais_userid.AddRange(lbl_admission_nooutput.Text.Split(','));

                foreach (RepeaterItem itm in rd_studentname.Items)
                {
                    var mobile_no = (LinkButton)itm.FindControl(lbl);
                    var admission_no = (Label)itm.FindControl(regid);
                    mobile_no.CssClass = "text-black";
                    var mobile = mobile_no.Text;
                    if (mobile.Length > 10)
                    {
                        mobile = mobile.Substring(mobile.Length - 10, 10);
                    }
                    if (mais.Contains(mobile))
                    {
                        mais.Remove(mobile);
                    }
                    if (mais_userid.Contains(admission_no.Text))
                    {
                        mais_userid.Remove(admission_no.Text);
                    }

                }

                txt_sent.Text = string.Join(",", mais);
                lbl_admission_nooutput.Text = string.Join(",", mais_userid);
            }
        }
    }
}