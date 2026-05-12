using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Admin
{
    public partial class Send_Notice_to_Students : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name, course_id from Add_course_table   order by Position");
                        mycode.bind_ddlall(ddl_section, "Select distinct section  from section_master   order by section");
                        txt_date.Text = mycode.date();
                        try
                        {
                            if (Request.QueryString["Id"] != null)
                            {
                                btn_cancel.Visible = true;
                                ViewState["id"] = Request.QueryString["Id"].ToString();
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


        private void BindDetails()
        {
            DataTable dt = My.dataTable("select * from Notice_Board_Details where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                btn_Submit.Text = "Update";
                btn_cancel.Visible = true;
                txt_notice_details.Text = dt.Rows[0]["Notice"].ToString();
                txt_notice_subject.Text = dt.Rows[0]["Subject"].ToString();
                txt_link.Text = dt.Rows[0]["Link"].ToString();
                Hd_Photo.Value = dt.Rows[0]["Attachment"].ToString();

                mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name, course_id from Add_course_table   order by Position");

                try
                {
                    if (dt.Rows[0]["Class"].ToString() == "ALL")
                    {
                        ddl_class.Text = "0";
                    }
                    else
                    {
                        ddl_class.Text = dt.Rows[0]["Class"].ToString();
                        My.bind_ddl_all(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "' order by Section");
                    }
                    ddl_section.Text = dt.Rows[0]["Section"].ToString();
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

                    if (ddl_class.SelectedItem.Text == "Select")
                    {
                        Alertme("Select class", "warning");
                    }
                    else if (ddl_section.Text == "Select")
                    {
                        Alertme("Select section", "warning");
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
                        final_save_data();
                    }
                }
                else
                {
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
                        final_save_data();
                    }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }



        private void final_save_data()
        {
            if (btn_Submit.Text == "Send")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    string ImagePath = GetImagePath();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Notice_Board_Details (Notice,Posted_Date,Posted_Idate,Posted_Time,Class,Section,Send_Status,Attachment,Date_Main,Send_Type,Admission_no,Session_Id,Session_name,Branch_id,Link,Subject,SenderId) values (@Notice,@Posted_Date,@Posted_Idate,@Posted_Time,@Class,@Section,@Send_Status,@Attachment,@Date_Main,@Send_Type,@Admission_no,@Session_Id,@Session_name,@Branch_id,@Link,@Subject,@SenderId)");
                    cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);
                    cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                    cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                    cmd.Parameters.AddWithValue("@Posted_Idate", My.DateConvertToIdate(txt_date.Text));
                    cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
                    cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                    cmd.Parameters.AddWithValue("@Attachment", ImagePath);
                    cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
                    cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                    cmd.Parameters.AddWithValue("@SenderId", ViewState["userid"].ToString());
                    if (rd_Class_wise.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
                        cmd.Parameters.AddWithValue("@Admission_no", "N/A");
                        if (ddl_class.SelectedItem.Text == "ALL")
                        {
                            cmd.Parameters.AddWithValue("@Class", "ALL");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);
                        }
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    }
                    else
                    {
                        //Dictionary<string, object> dc1 = code.getstudent(txt_admission_no.Text);
                        //string Class_id = (String)dc1["Class_id"];
                        //string Section = (String)dc1["Section"];
                        cmd.Parameters.AddWithValue("@Class", "N/A");
                        cmd.Parameters.AddWithValue("@Section", "N/A");
                        cmd.Parameters.AddWithValue("@Send_Type", "Individual");
                        cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                    }

                    cmd.Parameters.AddWithValue("@Session_Id", ViewState["sesssionid"].ToString());
                    cmd.Parameters.AddWithValue("@Session_name", ViewState["sesssion"].ToString());
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        Alertme("Notice details have been send sucessfully.", "success");
                        txt_admission_no.Text = "";
                        txt_notice_details.Text = "";
                        txt_notice_subject.Text = "";
                        txt_link.Text = "";
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            if (btn_Submit.Text == "Update")
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    if (fl_Photo.HasFile)
                    {
                        Hd_Photo.Value = GetImagePath();
                    }
                    else
                    {

                    }
                    SqlCommand cmd = new SqlCommand("Update Notice_Board_Details set Notice=@Notice,Posted_Date=@Posted_Date,Posted_Idate=@Posted_Idate,Posted_Time=@Posted_Time,Class=@Class,Section=@Section,Send_Status=@Send_Status,Attachment=@Attachment,Date_Main=@Date_Main,Send_Type=@Send_Type,Link=@Link,Subject=@Subject where Id = @Id");
                    cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);
                    cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                    cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                    cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                    cmd.Parameters.AddWithValue("@Posted_Idate", My.DateConvertToIdate(txt_date.Text));
                    cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
                    cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                    cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
                    if (rd_Class_wise.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
                        cmd.Parameters.AddWithValue("@Admission_no", "N/A");
                        if (ddl_class.SelectedItem.Text == "ALL")
                        {
                            cmd.Parameters.AddWithValue("@Class", "ALL");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);
                        }
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Class", "N/A");
                        cmd.Parameters.AddWithValue("@Section", "N/A");
                        cmd.Parameters.AddWithValue("@Send_Type", "Individual");
                        cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                    }
                    cmd.Parameters.AddWithValue("@Attachment", Hd_Photo.Value);
                    cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        btn_Submit.Text = "Send";
                        txt_notice_subject.Text = "";
                        txt_notice_details.Text = "";
                        txt_admission_no.Text = "";
                        Session["SmsG"] = "Notice details have been successfully Updated.";
                        Response.Redirect("View_All_Sent_Notice_to_Students.aspx", false);
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
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
    }
}