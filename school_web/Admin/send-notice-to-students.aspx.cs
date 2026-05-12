using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class send_notice_to_students : System.Web.UI.Page
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
                        ViewState["userid"] = Session["Admin"].ToString();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["userid"].ToString());
                        ViewState["session_id"] = My.get_session_id();
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["sesssion"] = My.get_session();
                        string pagename_current = "Notice.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        //mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
                        fetch_class();
                        mycode.bind_ddlall(ddl_section, "Select distinct section  from section_master   order by section");
                        txt_date.Text = mycode.date();

                        try
                        {
                            if (Request.QueryString["Id"] != null && Request.QueryString["noticeid"] != null)
                            {
                                btn_cancel.Visible = true;
                                ViewState["id"] = Request.QueryString["Id"].ToString();
                                ViewState["noticeid"] = Request.QueryString["noticeid"].ToString();
                                BindDetails();
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Send_Notice_to_Students");
            }
        }

        private void fetch_class()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Course_Name,course_id from Add_course_table order by Position", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                ddl_classs.DataTextField = "Course_Name";
                ddl_classs.DataValueField = "course_id";
                ddl_classs.DataSource = reader;
                ddl_classs.DataBind();
            }
        }


        private void BindDetails()
        {
            DataTable dt = My.dataTable("select * from Notice_Board_Details where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                btn_Submit.Text = "Update";
                btn_cancel.Visible = true;
                txt_notice_details.Text = dt.Rows[0]["Notice"].ToString();
                txt_notice_subject.Text = dt.Rows[0]["Subject"].ToString();
                txt_link.Text = dt.Rows[0]["Link"].ToString();
                Hd_Photo.Value = dt.Rows[0]["Attachment"].ToString();

                //mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");

                try
                {
                    fetch_class_for_update();
                    //if (dt.Rows[0]["Class"].ToString() == "ALL")
                    //{
                    //    ddl_class.Text = "0";
                    //}
                    //else
                    //{
                    //    ddl_class.Text = dt.Rows[0]["Class"].ToString();
                    //    My.bind_ddl_all(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "' order by Section");
                    //}
                    ddl_section.Text = dt.Rows[0]["Section"].ToString();
                }
                catch
                {
                }

                try
                {
                    fetch_user_type();
                }
                catch
                {

                }
                txt_date.Text = dt.Rows[0]["Posted_Date"].ToString();
                if (dt.Rows[0]["Send_Type"].ToString() == "Class Wise")
                {
                    class1.Visible = true;

                    admissionno.Visible = false;
                    rd_Class_wise.Checked = true;
                    rd_individual.Checked = false;
                }
                else
                {
                    rd_Class_wise.Checked = false;
                    rd_individual.Checked = true;
                    class1.Visible = false;

                    admissionno.Visible = true;
                    txt_admission_no.Text = dt.Rows[0]["Admission_no"].ToString();
                }
            }
        }

        private void fetch_user_type()
        {
            DataTable dt = My.dataTable("select distinct Teacher_Id from Notice_Board_Details_Teacher where Notice_id='" + ViewState["noticeid"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                chk_user_Type.Checked = true;
            }
        }


        private void fetch_class_for_update()
        {
            DataTable dt = My.dataTable("select distinct Class from Notice_Board_Details where Notice_id='" + ViewState["noticeid"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Class"].ToString().ToUpper() == "ALL")
                {
                    foreach (ListItem item in ddl_classs.Items)
                    {
                        item.Selected = true;
                    }
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (ListItem item in ddl_classs.Items)
                        {
                            if (item.Value == dr["Class"].ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
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

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            try
            {
                if (rd_Class_wise.Checked == true)
                {
                    //For Class
                    bool isClassSelectd = false; string selectClassid = ""; int all_selected_class = 2;
                    foreach (ListItem item in ddl_classs.Items)
                    {
                        if (item.Selected)
                        {
                            selectClassid = selectClassid + item.Value + ",";
                            isClassSelectd = true;

                            if (all_selected_class == 0) { } else { all_selected_class = 1; }
                        }
                        else
                        {
                            all_selected_class = 0;
                        }
                    }
                    if (isClassSelectd == false)
                    {
                        ddl_classs.Focus();
                        Alertme("Please select class.", "warning");
                        return;
                    }


                    if (txt_date.Text == "")
                    {
                        Alertme("Please enter notice date ", "warning");
                    }
                    else if (txt_notice_subject.Text == "")
                    {
                        Alertme("Please enter subject. ", "warning");
                    }
                    else if (txt_notice_details.Text == "")
                    {
                        Alertme("Please enter notice ", "warning");
                    }
                    else
                    {
                        string ImagePath = GetImagePath();
                        if (btn_Submit.Text == "Update")
                        {
                            My.exeSql("delete from Notice_Board_Details where Notice_id='" + ViewState["noticeid"].ToString() + "'; delete from Notice_Board_Details_Teacher where Notice_id='" + ViewState["noticeid"].ToString() + "'");
                            if (fl_Photo.HasFile)
                            {
                                ImagePath = GetImagePath();
                            }
                            else
                            {
                                ImagePath = Hd_Photo.Value;
                            }
                        }
                        string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
                        string randomNo = My.create_random_no_otp();
                        string noticeId = randomNo + itime;
                        final_save_data(all_selected_class, noticeId, ImagePath);
                        send_events_staff(noticeId, ImagePath);
                        if (ViewState["isSend"].ToString() == "1")
                        {
                            Alertme("Notice details has been sent successfully.", "success");
                            txt_admission_no.Text = "";
                            txt_notice_details.Text = "";
                            txt_notice_subject.Text = "";
                            txt_link.Text = "";
                        }
                    }
                }
                else
                {
                    int all_selected_class = 0;
                    if (txt_admission_no.Text == "")
                    {
                        Alertme("Please enter admission no.", "warning");
                    }
                    else if (txt_date.Text == "")
                    {
                        Alertme("Please enter notice date ", "warning");
                    }
                    else if (txt_notice_subject.Text == "")
                    {
                        Alertme("Please enter subject. ", "warning");
                    }
                    else if (txt_notice_details.Text == "")
                    {
                        Alertme("Please enter notice ", "warning");
                    }
                    else
                    {
                        string ImagePath = GetImagePath();
                        if (btn_Submit.Text == "Update")
                        {
                            My.exeSql("delete from Notice_Board_Details where Notice_id='" + ViewState["noticeid"].ToString() + "'; delete from Notice_Board_Details_Teacher where Notice_id='" + ViewState["noticeid"].ToString() + "'");
                            if (fl_Photo.HasFile)
                            {
                                ImagePath = GetImagePath();
                            }
                            else
                            {
                                ImagePath = Hd_Photo.Value;
                            }
                        }
                        string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
                        string randomNo = My.create_random_no_otp();
                        string noticeId = randomNo + itime;
                        final_save_data(all_selected_class, noticeId, ImagePath);
                        send_events_staff(noticeId, ImagePath);
                        if (ViewState["isSend"].ToString() == "1")
                        {
                            Alertme("Notice details has been sent successfully.", "success");
                            txt_admission_no.Text = "";
                            txt_notice_details.Text = "";
                            txt_notice_subject.Text = "";
                            txt_link.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }



        private void final_save_data(int all_selected_class, string noticeId,string ImagePath)
        {
            //if (btn_Submit.Text == "Send" || btn_Submit.Text == "Update")
            //{
            if (ViewState["Is_add"].ToString() == "1")
            {
                if (rd_Class_wise.Checked == true)
                {
                    send_notice_classwise(all_selected_class, noticeId, ImagePath);
                }
                else
                { 
                    SqlCommand cmd = new SqlCommand("INSERT INTO Notice_Board_Details (Notice,Posted_Date,Posted_Idate,Posted_Time,Class,Section,Send_Status,Attachment,Date_Main,Send_Type,Admission_no,Session_Id,Session_name,Branch_id,Link,Subject,SenderId,Notice_id) values (@Notice,@Posted_Date,@Posted_Idate,@Posted_Time,@Class,@Section,@Send_Status,@Attachment,@Date_Main,@Send_Type,@Admission_no,@Session_Id,@Session_name,@Branch_id,@Link,@Subject,@SenderId,@Notice_id)");
                    cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);
                    cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                    cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                    cmd.Parameters.AddWithValue("@Posted_Idate", My.DateConvertToIdate(txt_date.Text));
                    cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
                    cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                    cmd.Parameters.AddWithValue("@Attachment", ImagePath);
                    cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
                    cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                    cmd.Parameters.AddWithValue("@Class", "N/A");
                    cmd.Parameters.AddWithValue("@Section", "N/A");
                    cmd.Parameters.AddWithValue("@Send_Type", "Individual");
                    cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                    cmd.Parameters.AddWithValue("@Session_Id", ViewState["sesssionid"].ToString());
                    cmd.Parameters.AddWithValue("@Session_name", ViewState["sesssion"].ToString());
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                    cmd.Parameters.AddWithValue("@SenderId", ViewState["userid"].ToString());
                    cmd.Parameters.AddWithValue("@Notice_id", noticeId);
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        ViewState["isSend"] = "1";
                    }
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }

            //if (btn_Submit.Text == "Update")
            //{
            //    if (ViewState["Is_Edit"].ToString() == "1")
            //    {
            //        if (fl_Photo.HasFile)
            //        {
            //            Hd_Photo.Value = GetImagePath();
            //        }
            //        SqlCommand cmd = new SqlCommand("Update Notice_Board_Details set Notice=@Notice,Posted_Date=@Posted_Date,Posted_Idate=@Posted_Idate,Posted_Time=@Posted_Time,Class=@Class,Section=@Section,Send_Status=@Send_Status,Attachment=@Attachment,Date_Main=@Date_Main,Send_Type=@Send_Type,Link=@Link,Subject=@Subject,SenderId=@SenderId where Id = @Id");
            //        cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);
            //        cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
            //        cmd.Parameters.AddWithValue("@Link", txt_link.Text);
            //        cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
            //        cmd.Parameters.AddWithValue("@Posted_Idate", My.DateConvertToIdate(txt_date.Text));
            //        cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
            //        cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
            //        cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
            //        if (rd_Class_wise.Checked == true)
            //        {
            //            cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
            //            cmd.Parameters.AddWithValue("@Admission_no", "N/A");
            //            if (ddl_class.SelectedItem.Text == "ALL")
            //            {
            //                cmd.Parameters.AddWithValue("@Class", "ALL");
            //            }
            //            else
            //            {
            //                cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);
            //            }
            //            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
            //        }
            //        else
            //        {
            //            cmd.Parameters.AddWithValue("@Class", "N/A");
            //            cmd.Parameters.AddWithValue("@Section", "N/A");
            //            cmd.Parameters.AddWithValue("@Send_Type", "Individual");
            //            cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
            //        }
            //        cmd.Parameters.AddWithValue("@Attachment", Hd_Photo.Value);
            //        cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
            //        cmd.Parameters.AddWithValue("@SenderId", ViewState["userid"].ToString());
            //        if (InsertUpdate.InsertUpdateData(cmd))
            //        {
            //            btn_Submit.Text = "Send";
            //            txt_notice_subject.Text = "";
            //            txt_notice_details.Text = "";
            //            txt_admission_no.Text = "";
            //            Session["SmsG"] = "Notice details has been successfully Updated.";
            //            Response.Redirect("View_All_Sent_Notice_to_Students.aspx", false);
            //        }
            //    }
            //    else
            //    {
            //        Alertme(My.get_restricted_message(), "warning");
            //    }
            //}
        }


        //CLASSWISE
        private void send_notice_classwise(int all_selected_class, string noticeId,string ImagePath)
        { 
            if (all_selected_class == 1)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Notice_Board_Details (Notice,Posted_Date,Posted_Idate,Posted_Time,Class,Section,Send_Status,Attachment,Date_Main,Send_Type,Admission_no,Session_Id,Session_name,Branch_id,Link,Subject,SenderId,Notice_id) values (@Notice,@Posted_Date,@Posted_Idate,@Posted_Time,@Class,@Section,@Send_Status,@Attachment,@Date_Main,@Send_Type,@Admission_no,@Session_Id,@Session_name,@Branch_id,@Link,@Subject,@SenderId,@Notice_id)");
                cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);
                cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Posted_Idate", My.DateConvertToIdate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                cmd.Parameters.AddWithValue("@Attachment", ImagePath);
                cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
                cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
                cmd.Parameters.AddWithValue("@Admission_no", "N/A");
                cmd.Parameters.AddWithValue("@Class", "ALL");
                cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                cmd.Parameters.AddWithValue("@Session_Id", ViewState["sesssionid"].ToString());
                cmd.Parameters.AddWithValue("@Session_name", ViewState["sesssion"].ToString());
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                cmd.Parameters.AddWithValue("@SenderId", ViewState["userid"].ToString());
                cmd.Parameters.AddWithValue("@Notice_id", noticeId);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    ViewState["isSend"] = "1";
                }
            }
            else
            {
                foreach (ListItem item in ddl_classs.Items)
                {
                    if (item.Selected)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Notice_Board_Details (Notice,Posted_Date,Posted_Idate,Posted_Time,Class,Section,Send_Status,Attachment,Date_Main,Send_Type,Admission_no,Session_Id,Session_name,Branch_id,Link,Subject,SenderId,Notice_id) values (@Notice,@Posted_Date,@Posted_Idate,@Posted_Time,@Class,@Section,@Send_Status,@Attachment,@Date_Main,@Send_Type,@Admission_no,@Session_Id,@Session_name,@Branch_id,@Link,@Subject,@SenderId,@Notice_id)");
                        cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);
                        cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                        cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                        cmd.Parameters.AddWithValue("@Posted_Idate", My.DateConvertToIdate(txt_date.Text));
                        cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
                        cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                        cmd.Parameters.AddWithValue("@Attachment", ImagePath);
                        cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
                        cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                        cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
                        cmd.Parameters.AddWithValue("@Admission_no", "N/A");
                        cmd.Parameters.AddWithValue("@Class", item.Value);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Session_Id", ViewState["sesssionid"].ToString());
                        cmd.Parameters.AddWithValue("@Session_name", ViewState["sesssion"].ToString());
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                        cmd.Parameters.AddWithValue("@SenderId", ViewState["userid"].ToString());
                        cmd.Parameters.AddWithValue("@Notice_id", noticeId);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            ViewState["isSend"] = "1";
                        }
                    }
                }
            }
        }

        public string GetImagePath()
        {
            string Path = mycode.Upload_doc_images(fl_Photo, "/UploadedImage/Notice_board/");
            return Path;
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("View_All_Sent_Notice_to_Students.aspx", false);
        }
        protected void rd_individual_CheckedChanged(object sender, EventArgs e)
        {
            class1.Visible = false;
            admissionno.Visible = true;
        }
        protected void rd_Class_wise_CheckedChanged(object sender, EventArgs e)
        {
            class1.Visible = true;
            admissionno.Visible = false;
        }



        private void send_events_staff(string noticeId,string ImagePath)
        {
            if (chk_user_Type.Checked == true)
            { 
                SqlCommand cmd = new SqlCommand("INSERT INTO Notice_Board_Details_Teacher (Notice,Posted_Date,Posted_Idate,Posted_Time,Send_Status,Attachment,Date_Main,Teacher_Id,Session_id,Branch_id,Link,Subject,SenderId,Notice_id) values (@Notice,@Posted_Date,@Posted_Idate,@Posted_Time,@Send_Status,@Attachment,@Date_Main,@Teacher_Id,@Session_id,@Branch_id,@Link,@Subject,@SenderId,@Notice_id)");
                cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);
                cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                cmd.Parameters.AddWithValue("@Posted_Idate", My.DateConvertToIdate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                cmd.Parameters.AddWithValue("@Attachment", ImagePath);
                cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
                cmd.Parameters.AddWithValue("@SenderId", ViewState["userid"].ToString());
                cmd.Parameters.AddWithValue("@Teacher_Id", "ALL");
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                cmd.Parameters.AddWithValue("@Notice_id", noticeId);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                }
            }
        }
    }
}
