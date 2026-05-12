using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Parents_Wise_Dues_Details : System.Web.UI.Page
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






                        pnl_0.Visible = pnl_1.Visible = pnl_2.Visible = pnl_3.Visible = pnl_4.Visible = pnl_5.Visible = pnl_6.Visible = pnl_7.Visible = pnl_8.Visible = false;
                        var vrls = ViewState["VariableName"].ToString().Split(',');
                        txt_message.Text = ViewState["SMS_Tempate"].ToString();
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
                else if (txt_Parents_id.Text == "")
                {
                    Alertme("Please enter parents id", "warning");
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
            lbl_count_student.Text = "";
            lbl_parentsid.Text = "";
            lbl_parents_name.Text = "";
            string query = "select pld.User_id,psm.Student_id,ar.admissionserialnumber,ar.Class_id,ar.Section,ar.fathername,ar.father_mob,ar.Father_whatsApp_no from Parent_Login_Details pld join Parent_Student_Mapping psm on psm.Parent_id=pld.User_id join admission_registor ar on ar.admissionserialnumber=psm.Student_id where pld.User_id='" + txt_Parents_id.Text + "' and ar.Session_id='" + ddl_session.SelectedValue + "' ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry your enterd parents id not assocate with student", "warning");
                lbl_ttl_dues.Text = "0.00";
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                lbl_count_student.Text = dt.Rows.Count.ToString();
                lbl_parentsid.Text = txt_Parents_id.Text;
                lbl_parents_name.Text = dt.Rows[0]["fathername"].ToString();
                ViewState["father_mob"] = dt.Rows[0]["Father_whatsApp_no"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    string Class_id = dt.Rows[i]["Class_id"].ToString();
                    string Section = dt.Rows[i]["Section"].ToString();
                    ViewState["studentid"] = ViewState["studentid"] + "'" + admissionserialnumber + "',";
                    Find_data_dues(admissionserialnumber, Class_id, Section);
                }
                find_students_dues();
            }








        }

        private void Find_data_dues(string admissionserialnumber, string class_id, string section)
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
                            dues_update_headwise_transaction.update_student_dues(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString(), "0", "0", con);
                        }
                    }
                    flag = true;
                    con.Close();
                    scope.Complete();
                }


            }
            catch (Exception ex)
            {
            }
        }

        private void find_students_dues()
        {
            string student_id = ViewState["studentid"].ToString().TrimEnd(',');
            string qry = "";

            lbl_class22.Text = "Session :-" + ddl_session.SelectedItem.Text + " Dues List Up To " + ddl_month.SelectedItem.Text; 
            qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.Father_whatsApp_no,t1.father_mob,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + ddl_month.SelectedValue + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddl_session.SelectedValue + "' and t1.Status=1 and t1.admissionserialnumber in (" + student_id + ") order by t2.Position,t1.Section,t1.rollnumber asc";


            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Panel2.Visible = false;
                btn_excels.Visible = false;
                lbl_ttl_dues.Text = "0.00";
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
                lbl_ttl_dues.Text = My.toDouble(Total_mrp).ToString("0.00");
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
        protected void btn_find_by_admission_no_Click(object sender, EventArgs e)
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
                else if (txt_serch_by_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                }
                else
                {
                    lbl_class22.Text = "";
                    ViewState["studentid"] = "";
                    lbl_count_student.Text = "";
                    lbl_parentsid.Text = "";
                    lbl_parents_name.Text = "";

                    ViewState["father_mob"] = "";
                    string parentsid = My.get_parentsid(txt_serch_by_admission_no.Text);
                    // string query = "select pld.User_id,psm.Student_id,ar.admissionserialnumber,ar.Class_id,ar.Section,ar.fathername from Parent_Login_Details pld join Parent_Student_Mapping psm on psm.Parent_id=pld.User_id join admission_registor ar on ar.admissionserialnumber=psm.Student_id where psm.Student_id='" + txt_serch_by_admission_no.Text + "' and ar.Session_id='" + ddl_session.SelectedValue + "' ";
                    string query = "select pld.User_id,psm.Student_id,ar.admissionserialnumber,ar.Class_id,ar.Section,ar.fathername,ar.father_mob,ar.Father_whatsApp_no from Parent_Login_Details pld join Parent_Student_Mapping psm on psm.Parent_id=pld.User_id join admission_registor ar on ar.admissionserialnumber=psm.Student_id where pld.User_id='" + parentsid + "' and ar.Session_id='" + ddl_session.SelectedValue + "' and Status='1' ";

                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count == 0)
                    {
                        Alertme("Sorry your enterd parents id not assocate with student", "warning");
                        lbl_ttl_dues.Text = "0.00";
                        rd_view.DataSource = null;
                        rd_view.DataBind();
                    }
                    else
                    {
                        lbl_count_student.Text = dt.Rows.Count.ToString();
                        lbl_parentsid.Text = txt_Parents_id.Text;
                        lbl_parents_name.Text = dt.Rows[0]["fathername"].ToString();
                        ViewState["father_mob"] = dt.Rows[0]["Father_whatsApp_no"].ToString();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                            string Class_id = dt.Rows[i]["Class_id"].ToString();
                            string Section = dt.Rows[i]["Section"].ToString();
                            ViewState["studentid"] = ViewState["studentid"] + "'" + admissionserialnumber + "',";
                            Find_data_dues(admissionserialnumber, Class_id, Section);
                        }
                        find_students_dues();
                    }

                }
            }
            catch (Exception ex)
            {

            }





        }

        #region webmothod
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct User_id from Parent_Login_Details where User_id LIKE '%'+@User_id+'%' and Status='Active'  ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@User_id", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["User_id"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        public static List<string> GetRooPathAdmNo(string PathRooT, string Session_id)
        {
            string sessionid = Session_id;
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE ''+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["admissionserialnumber"].ToString());
                    }
                    con.Close();
                    return MobResult;
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
        protected void btn_msgPreview_Click(object sender, EventArgs e)
        {
            try
            {
                var vrls = ViewState["VariableName"].ToString().Split(',');
                var lst = new String[vrls.Length];
                if (vrls.Length > 0)
                {
                    lst[0] = ViewState["monthName"].ToString();
                }
                if (vrls.Length > 1)
                {
                    lst[1] = lbl_ttl_dues.Text;
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
                txt_message.Text = String.Format(ViewState["SMS_Tempate"].ToString(), lst);

                pnl_msg.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
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


                string message = "";
                if (send == true)
                {
                    string confirmValue = string.Empty;
                    confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {
                        string temp1 = "";
                        if (My.toDouble(lbl_ttl_dues.Text) > 0)
                        {
                            int growcount = rd_view.Items.Count;
                            for (int i = 0; i < growcount; i++)
                            {

                                RepeaterItem item = rd_view.Items[i];
                                Label lbl_studentname = item.FindControl("lbl_studentname") as Label;
                                Label lbl_amount = item.FindControl("lbl_amount") as Label; 
                                temp1 = temp1 + $"{lbl_studentname.Text}: ₹{lbl_amount.Text},";

                            }

                            //                            message =
                            //$"Dear {lbl_parents_name.Text},\n" +
                            //$"This is to inform you that the total fees due up to the month of {ViewState["monthName"].ToString()} amount to ₹ {lbl_ttl_dues.Text}, as detailed below:\n" +
                            //$"{temp1}\n" +
                            //$"Kindly ensure the payment is made by {lbl_ttl_dues.Text}.\n\n" +
                            //"Regards, "+ ViewState["firm_name"].ToString();


                            //                          message =
                            //$"Dear Parents,\n" +
                            //$"This is a gentle reminder that the cumulative school fee amounting to ₹{lbl_ttl_dues.Text}, for your ward(s) {temp1} is currently outstanding.\n\n" +
                            //$"We kindly request you to settle the due amount at your earliest convenience to avoid any disruption in academic services.\n\n" +
                            //$"Thank you for your prompt attention to this matter.\n\n" +
                            //"Regards,\n" + ViewState["firm_name"].ToString();

                            if (ViewState["firm_name"].ToString() == "Toppers Convent School")
                            {
                                message =
 $"Dear Parents,\n" +
 $"This is a gentle reminder that the cumulative school fee amounting to ₹{lbl_ttl_dues.Text}, for your ward(s) {temp1} covering the period of 2nd installment -upto {ddl_month.SelectedItem} is currently outstanding.\n\n" +
 $"We kindly request you to settle the due amount at your earliest convenience to avoid any disruption in academic services.\n\n" +
 $"Thank you for your prompt attention to this matter.\n" +
 $"Please ignore if already paid.\n\n" +
 "Regards,\n" + ViewState["firm_name"].ToString();
                            }
                            else
                            {
                                message =
 $"Dear Parents,\n" +
 $"This is a gentle reminder that the cumulative school fee amounting to ₹{lbl_ttl_dues.Text}, for your ward(s) {temp1} covering the period up to the month of {ddl_month.SelectedItem} is currently outstanding.\n\n" +
 $"We kindly request you to settle the due amount at your earliest convenience to avoid any disruption in academic services.\n\n" +
 $"Thank you for your prompt attention to this matter.\n" +
 $"Please ignore if already paid.\n\n" +
 "Regards,\n" + ViewState["firm_name"].ToString();
                            }





                            if (rd_notification.Checked == true)
                            {
                                sendSMS(ViewState["monthName"].ToString(), lbl_ttl_dues.Text, ViewState["father_mob"].ToString(), message);
                            }
                            else
                            {
                                if (ViewState["father_mob"].ToString() != "")
                                {
                                    sendSMS(ViewState["monthName"].ToString(), lbl_ttl_dues.Text, ViewState["father_mob"].ToString(), message);
                                }
                            }


                        }
                        this.Alertme("Dues message send successfully", "success");
                        pnl_msg.Visible = false;
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
            catch
            {

            }


        }

        private void sendSMS(string monthname, string total, string father_mob, string mes)
        {
            string gcmId = "";
            if (father_mob == "N/A")
            {

            }
            else if (father_mob == "")
            {

            }
            else if (My.toDouble(total) == 0)
            {

            }
            else
            {
                string mobno = father_mob;
                string admissionId = lbl_parentsid.Text;
                txt_0.Text = monthname;
                txt_1.Text = total;
                //txt_2.Text = class_name;
                ////txt_3.Text = section;
                ////txt_4.Text = rollnumber;
                ////txt_5.Text = admissionId;
                ////txt_6.Text = monthname;
                ////txt_7.Text = total;




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
                var vrls = ViewState["VariableName"].ToString().Split(',');
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
                // txt_message.Text = String.Format(ViewState["SMS_Tempate"].ToString(), lst);




                try
                {
                    if (rd_sms.Checked == true)
                    {
                        string api_key = ViewState["api_key"].ToString();
                        string Sender_id = ViewState["Sender_id"].ToString();
                        string msgtype = ViewState["type"].ToString();


                        string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + mes + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobno + "&smsContentType=" + type;

                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        this.Alertme("SMS Send Successfully", "success");
                        My.Insert("Message_Details", new
                        {
                            Mobile_No = mobno,
                            Message = mes,
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
                        string sms = mes;// String.Format(ViewState["SMS_Tempate"].ToString(), lst);

                        try
                        {
                            if (father_mob.Length > 9)
                            {
                                string message = Uri.EscapeDataString(sms);
                                string mobile_no = "91" + father_mob;
                                string _url = "";


                                if (ViewState["Whatsapp_api_url"].ToString().Contains("app.allexpert.in"))
                                {
                                    //exampe url
                                    //https://app.allexpert.in/send-message?api_key=q0hRX9FA1z5S7JttEIzLtxJU3lvoOv&sender=62888xxxx&number=9162888xxxx&message=Hello World
                                    _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), mobile_no, message);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                }
                                if (ViewState["Whatsapp_api_url"].ToString().Contains("api4ws.com"))
                                {
                                    // _url = My.Whatsapp_api_url + My.whatsapp_mobile_no + "&message=" + message + "&phone=91" + mobile_no;
                                    //https://api4ws.com/sendMessage.php?AUTH_KEY=918877804016&message=User Defined Whatsapp Message&phone=919812345678
                                    _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                }
                                else
                                {

                                    //https://fastwsapi.com/sendMessage.php?AUTH_KEY=DEMOEDUNEXTG@123&instance_id=575051&message=User Defined Whatsapp Message&phone=919812345678

                                    _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                }




                                My.Insert("WhatsApp_send", new
                                {
                                    Mobile_no = mobno,
                                    Message = mes,
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
                            ss["message"] = mes;
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
        }
        #endregion
    }
}