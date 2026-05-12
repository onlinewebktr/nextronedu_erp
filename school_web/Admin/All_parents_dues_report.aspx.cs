using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Transactions;
using System.Net;
using System.IO;

namespace school_web.Admin
{
    public partial class All_parents_dues_report : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        
                        mycode.bind_all_ddl_with_id(ddl_month, "select Month,Position from Month_Index order by Position asc");

                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();

                        Dictionary<string, object> dc3 = My.get_push_credantial();
                        ViewState["type"] = (String)dc3["type"];
                        ViewState["project_id"] = (String)dc3["project_id"];
                        ViewState["private_key_id"] = (String)dc3["private_key_id"];
                        ViewState["client_email"] = (String)dc3["client_email"];
                        ViewState["client_id"] = (String)dc3["client_id"];
                        ViewState["private_key"] = dc3["private_key"].ToString().Replace("\\n", "\n");


                        Dictionary<string, object> dc2 = mycode.Firm_details();
                        ViewState["firm_name"] = (String)dc2["firm_name"];
                        #region sms
                        Dictionary<string, object> autosms = mycode.get_auto_message_template("Parents Dues");
                        ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                        ViewState["VariableName"] = (String)autosms["VariableName"];
                        ViewState["SMSType"] = (String)autosms["SMSType"];
                        ViewState["Send_From"] = (String)autosms["Send_From"];
                        ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                        ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                //  My.saveExceptionDetails(ex, "StudentList");
            }

        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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

        protected void btn_find_parentsid_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_session.Focus();
                }
                else if (ddl_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please select month.", "warning");
                    ddl_month.Focus();
                }

                else
                {
                    find_students_data_parents_wise_deus();
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region parents_wise_dues
        private void find_students_data_parents_wise_deus()
        {
            lbl_class22.Text = "";
            ViewState["father_mob"] = "";
            ViewState["studentid"] = "";
            ViewState["Parent_id"] = "";
            /*  lbl_count_student.Text = "";
              lbl_parentsid.Text = "";
              lbl_parents_name.Text = "";*/
            string query = "select pld.User_id,psm.Student_id,ar.admissionserialnumber,ar.Class_id,ar.Section,ar.fathername,ar.father_mob,ar.Father_whatsApp_no,ar.father_mob from Parent_Login_Details pld join Parent_Student_Mapping psm on psm.Parent_id=pld.User_id join admission_registor ar on ar.admissionserialnumber=psm.Student_id where ar.Session_id='" + ddl_session.SelectedValue + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no records found", "warning");
                lbl_ttl_dues.Text = "Total :" + "0.00";
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                /* lbl_count_student.Text = dt.Rows.Count.ToString();
                 lbl_parentsid.Text = txt_Parents_id.Text;
                 lbl_parents_name.Text = dt.Rows[0]["fathername"].ToString();*/
                ViewState["father_mob"] = dt.Rows[0]["Father_whatsApp_no"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    string Class_id = dt.Rows[i]["Class_id"].ToString();
                    string Section = dt.Rows[i]["Section"].ToString();
                    ViewState["studentid"] = ViewState["studentid"] + "'" + admissionserialnumber + "',";
                    ViewState["Parent_id"] = ViewState["Parent_id"] + "'" + dt.Rows[i]["User_id"].ToString() + "',";
                    Find_data_dues(admissionserialnumber, Class_id, Section, dt.Rows[i]["User_id"].ToString());
                }
                find_students_dues();
            }








        }

        private void Find_data_dues(string admissionserialnumber, string class_id, string section, string Parent_id)
        {
            try
            {
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    string qry = "";

                    qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + class_id + "' and Section='" + section + "' and Status=1 and admissionserialnumber='" + admissionserialnumber + "'";

                    DataTable dt = payments.dataTable(qry, con);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            // dues_update_headwise_transaction.update_student_dues(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString(), "0", "0", con);


                            payments.exeSql("update STUDENT_WISE_DUES_AMOUNT set Parent_id='" + Parent_id + "' where admission_no='" + dr["admissionserialnumber"].ToString() + "'", con);
                        }
                    }
                    flag = true;
                    con.Close();
                    scope.Complete();
                }

                /*  bool flag = false;
                  using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                  {
                      SqlConnection con = new SqlConnection(My.conn);
                      con.Open();
                      string qry = "";

                      qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + class_id + "' and Section='" + section + "' and Status=1 and admissionserialnumber='" + admissionserialnumber + "'";

                      DataTable dt = payments.dataTable(qry, con);
                      if (dt.Rows.Count > 0)
                      {
                          foreach (DataRow dr in dt.Rows)
                          {
                              dues_update_headwise_transaction.update_student_dues(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString(), "0", "0", con);
                              payments.exeSql("update STUDENT_WISE_DUES_AMOUNT set Parent_id='"+ Parent_id + "' where admission_no='"+ dr["admissionserialnumber"].ToString() + "'", con);
                          }
                      }
                      flag = true;
                      con.Close();
                      scope.Complete();
                  }*/
            }
            catch (Exception ex)
            {
            }
        }

        private void find_students_dues()
        {
            string Parent_id = ViewState["Parent_id"].ToString().TrimEnd(',');
            string qry = "";
            lbl_class22.Text = "Session :-" + ddl_session.SelectedItem.Text + " Dues List Up To " + ddl_month.SelectedItem.Text;
            //  qry = "Select s1.Parent_id,t1.Father_whatsApp_no,t1.session,(select top 1 Name from Parent_Login_Details where User_id=s1. Parent_id ) as fathername,sum(convert(float, s1.Dues_amt)) Total_dues from Student_wise_dues_amount s1 join admission_registor t1 on s1.Session_id=t1.Session_id and s1.Class_id=t1.Class_id and s1.Admission_no=t1.admissionserialnumber and s1.Month_position<=" + ddl_month.SelectedValue + "   where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Status=1 and s1.Parent_id in (" + Parent_id + ") group by  s1.Parent_id,t1.Father_whatsApp_no,t1.session HAVING SUM(CONVERT(FLOAT, s1.Dues_amt)) > 0";
            qry = @"SELECT 
    s1.Parent_id,
    t1.Father_whatsApp_no, t1.father_mob, 
    t1.session,
    (SELECT TOP 1 Name
     FROM Parent_Login_Details
     WHERE User_id = s1.Parent_id) AS fathername,
    SUM(CONVERT(FLOAT, s1.Dues_amt)) AS Total_dues,
    STUFF((
        SELECT
            ',' + ISNULL(ar.studentname, '') + ' : ' + CAST(SUM(CONVERT(FLOAT, s2.Dues_amt)) AS VARCHAR)
        FROM
            Student_wise_dues_amount s2
        LEFT JOIN
            admission_registor ar
            ON s2.Admission_no = ar.admissionserialnumber
            AND s2.Session_id = ar.Session_id
        WHERE
            s2.Parent_id = s1.Parent_id
            AND s2.Session_id = '" + ddl_session.SelectedValue + "' AND s2.Month_position <= " + ddl_month.SelectedValue + " GROUP BY s2.Admission_no, ar.studentname FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS AdmissionWise_Dues FROM Student_wise_dues_amount s1 JOIN  admission_registor t1 ON s1.Session_id = t1.Session_id AND s1.Admission_no = t1.admissionserialnumber WHERE t1.Session_id = '" + ddl_session.SelectedValue + "' AND t1.Status = 1 AND s1.Parent_id IN(" + Parent_id + ") AND s1.Month_position <= " + ddl_month.SelectedValue + " GROUP BY s1.Parent_id,  t1.Father_whatsApp_no,t1.father_mob, t1.session HAVING SUM(CONVERT(FLOAT, s1.Dues_amt)) > 0";


            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Panel2.Visible = false;
                btn_excels.Visible = false;
                lbl_ttl_dues.Text = "Total :" + " 0.00";
                rd_view.DataSource = null;
                rd_view.DataBind();
                sendsms.Visible = false;
            }
            else
            {
                sendsms.Visible = true;
                Panel2.Visible = true;
                btn_excels.Visible = true;
                ViewState["monthName"] = ddl_month.SelectedItem.Text;
                String Total_mrp = Convert.ToDouble(dt.Compute("SUM(Total_dues)", string.Empty)).ToString();
                lbl_ttl_dues.Text = "Total :" + My.toDouble(Total_mrp).ToString("0.00");
                rd_view.DataSource = dt;
                rd_view.DataBind();

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }

        }


        #endregion


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Dues_list" + mycode.date() + "_" + mycode.time() + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        #region MESSAGE SENDING

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_paydate.Text == "")
                {
                    Alertme("Please enter pay date", "warning");
                }
                else
                {
                    bool send = false;
                    if (rd_sms.Checked == true)
                    {
                        var dt = mycode.FillData("select top 1 * from message_config where Status='running'");
                        if (dt.Rows.Count == 1)
                        {
                            ViewState["api_key"] = dt.Rows[0]["uid"].ToString();
                            ViewState["Sender_id"] = dt.Rows[0]["sender"].ToString();
                            send = true;
                        }
                        else
                        {
                            this.Alertme("Please set sms configuration", "warning");
                            return;
                        }
                    }
                    else if (rd_whatassp.Checked == true)
                    {
                        var dt = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                        if (dt.Rows.Count == 1)
                        {
                            send = true;
                            ViewState["whatsapp_mobile_no"] = dt.Rows[0]["SMS_API"].ToString();
                            ViewState["Whatsapp_api_url"] = dt.Rows[0]["url"].ToString();
                        }
                        else
                        {
                            this.Alertme("Please set Whatsapp configuration", "warning");
                            return;
                        }
                    }
                    else
                    {
                        send = true;
                    }
                    if (send == true)
                    {
                        string confirmValue = string.Empty;
                        confirmValue = Request.Form["confirm_value"];
                        if (confirmValue == "Yes")
                        {

                            int growcount = rd_view.Items.Count;
                            for (int i = 0; i < growcount; i++)
                            { 
                                RepeaterItem item = rd_view.Items[i];
                                Label lbl_fathername = item.FindControl("lbl_fathername") as Label;
                                Label lbl_Parent_id = item.FindControl("lbl_Parent_id") as Label;
                                Label lbl_father_mob = item.FindControl("lbl_father_mob") as Label;
                                Label lbl_total_amount = item.FindControl("lbl_total_amount") as Label;
                                Label lbl_monthname = item.FindControl("lbl_monthname") as Label;
                                Label lbl_AdmissionWise_Dues = item.FindControl("lbl_AdmissionWise_Dues") as Label;
                                CheckBox chk = item.FindControl("chkRowData") as CheckBox;

                                string message = "";
                                if (chk.Checked == true)
                                {
                                    string temp1 = "";
                                    string query = "select  t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,('aa') as fathername ,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + ddl_month.SelectedValue + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddl_session.SelectedValue + "' and t1.Status=1 and t1.admissionserialnumber in (select admission_no from Student_wise_dues_amount where Parent_id='" + lbl_Parent_id.Text + "' ) order by t2.Position,t1.Section,t1.rollnumber asc";
                                    DataTable dt = mycode.FillData(query);
                                    if (dt.Rows.Count > 0)
                                    { 
                                        for (int k = 0; k < dt.Rows.Count; k++)
                                        {
                                            string studentname = dt.Rows[k]["studentname"].ToString();
                                            string Total_dues = dt.Rows[k]["Total_dues"].ToString();
                                            temp1 = temp1 + $"{studentname}: ₹{Total_dues}, ";
                                        }
                                       

                                        if(ViewState["firm_name"].ToString()== "Toppers Convent School")
                                        {
                                            message =
 $"Dear Parents,\n" +
 $"This is a gentle reminder that the cumulative school fee amounting to ₹{lbl_total_amount.Text}, for your ward(s) {temp1} covering the period of 2nd installment -upto {ddl_month.SelectedItem} is currently outstanding.\n\n" +
 $"We kindly request you to settle the due amount at your earliest convenience to avoid any disruption in academic services.\n\n" +
 $"Thank you for your prompt attention to this matter.\n" +
 $"Please ignore if already paid.\n\n" +
 "Regards,\n" + ViewState["firm_name"].ToString();
                                        }
                                        else
                                        {

                                            message =
$"Dear Parents,\n" +
$"This is a gentle reminder that the cumulative school fee amounting to ₹{lbl_total_amount.Text}, for your ward(s) {temp1} covering the period up to the month of {ddl_month.SelectedItem} is currently outstanding.\n\n" +
$"We kindly request you to settle the due amount at your earliest convenience to avoid any disruption in academic services.\n\n" +
$"Thank you for your prompt attention to this matter.\n" +
$"Please ignore if already paid.\n\n" +
"Regards,\n" + ViewState["firm_name"].ToString();

                                        }
                                       






                                    }
                                    if (rd_notification.Checked == true)
                                    {
                                        sendSMS(ddl_month.SelectedItem.Text, lbl_total_amount.Text, lbl_father_mob.Text, lbl_fathername.Text, lbl_Parent_id.Text, message);
                                    }
                                    else
                                    {
                                        if (lbl_father_mob.Text != "")
                                        {
                                            sendSMS(ddl_month.SelectedItem.Text, lbl_total_amount.Text, lbl_father_mob.Text, lbl_fathername.Text, lbl_Parent_id.Text, message);
                                        }
                                    }
                                }
                            }
                            this.Alertme("Dues message send successfully", "success");
                            sendsms.Visible = false;
                        }
                        else
                        {
                            Alertme("Sorry you have clicked no", "warning");
                        }
                    }
                    else
                    {
                        this.Alertme("Please check sms and whatsapp configuration", "warning");
                    }
                }
            }
            catch
            {
            }
        }

        private void sendSMS(string monthname, string total, string father_mob, string fathername, string parentsid, string message)
        {
            string gcmId = "";
            string mobno = father_mob;
            string admissionId = parentsid;
            string type = "";
            type = "english";
            try
            {
                if (ViewState["SMSType"].ToString() == "Unicode")
                {
                    type = "unicode";
                }
                else
                {
                    type = "english";
                }
            }
            catch (Exception ex)
            {
            }


            ViewState["type"] = type;

            try
            {
                if (rd_sms.Checked == true)
                {
                    string api_key = ViewState["api_key"].ToString();
                    string Sender_id = ViewState["Sender_id"].ToString();
                    string msgtype = ViewState["type"].ToString();
                    string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + message + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobno + "&smsContentType=" + type;

                    HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                    StreamReader sr = new StreamReader(httpres.GetResponseStream());
                    string results = sr.ReadToEnd();
                    sr.Close();
                    this.Alertme("SMS Send Successfully", "success");
                    My.Insert("Message_Details", new
                    {
                        Mobile_No = mobno,
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
                        admin_user_id = admissionId,
                    });
                }
                else if (rd_whatassp.Checked == true)
                {


                    try
                    {
                        if (father_mob.Length > 9)
                        {
                            string mess = Uri.EscapeDataString(message);
                            string mobile_no = "91" + father_mob;
                            string _url = "";


                            if (ViewState["Whatsapp_api_url"].ToString().Contains("app.allexpert.in"))
                            {
                                //exampe url
                                //https://app.allexpert.in/send-message?api_key=q0hRX9FA1z5S7JttEIzLtxJU3lvoOv&sender=62888xxxx&number=9162888xxxx&message=Hello World
                                _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), mobile_no, mess);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                            }
                            if (ViewState["Whatsapp_api_url"].ToString().Contains("api4ws.com"))
                            {


                                _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), mess, mobile_no);  //+  + "&message=" + message +  
                            }
                            else
                            {



                                _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message, mobile_no);
                            }




                            My.Insert("WhatsApp_send", new
                            {
                                Mobile_no = mobno,
                                Message = message,
                                Message_url = _url,
                                Session_id = ddl_session.SelectedValue,
                                Admission_no = admissionId,
                                Status = "Pending",
                                Date = mycode.date(),
                                Idate = mycode.idate(),
                                Time = mycode.time(),
                                Send_by = ViewState["Userid"].ToString(),
                                Mesage_Type = ViewState["type"].ToString(),
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
                else
                {
                    if (gcmId == "")
                    {
                        gcmId = "0";
                    }
                    if (gcmId != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = message;
                        ss["title"] = "Dues Message";
                        ss["messagetype"] = "Message";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = admissionId;
                        ss["type"] = ViewState["type"].ToString();
                        ss["project_id"] = ViewState["project_id"].ToString();
                        ss["private_key_id"] = ViewState["private_key_id"].ToString();
                        ss["client_email"] = ViewState["client_email"].ToString();
                        ss["client_id"] = ViewState["client_id"].ToString();
                        ss["private_key"] = ViewState["private_key"].ToString();
                        My.onlypush(gcmId, ss);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Alertme(ex.Message, "warning");
            }

        }
        #endregion

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_monthname = (Label)e.Item.FindControl("lbl_monthname");
                lbl_monthname.Text = ddl_month.SelectedItem.Text;
            }
        }

        protected void lnk_student_view_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Parent_id = (Label)row.FindControl("lbl_Parent_id");
            Label lbl_fathername = (Label)row.FindControl("lbl_fathername");
            string qry = "select  t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,('" + lbl_fathername.Text + "') as fathername ,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + ddl_month.SelectedValue + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddl_session.SelectedValue + "' and t1.Status=1 and t1.admissionserialnumber in (select admission_no from Student_wise_dues_amount where Parent_id='" + lbl_Parent_id.Text + "' ) order by t2.Position,t1.Section,t1.rollnumber asc";
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                studentdetails.Visible = false;
                lbl_total_dues_student_wise.Text = "0.00";
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
            else
            {
                studentdetails.Visible = true;
                String Total_mrp = Convert.ToDouble(dt.Compute("SUM(Total_dues)", string.Empty)).ToString();
                lbl_total_dues_student_wise.Text = My.toDouble(Total_mrp).ToString("0.00");

                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal3();", true);

        }
    }
}