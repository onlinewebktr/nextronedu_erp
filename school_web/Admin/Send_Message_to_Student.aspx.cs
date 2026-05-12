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
    public partial class Send_Message_to_Student : System.Web.UI.Page
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
                        ViewState["Admin"] = Session["Admin"].ToString();
                        string pagename_current = "Message.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["userid"].ToString());
                        ViewState["sesssionid"] = My.get_session_id();

                        ViewState["sesssion"] = My.get_session();

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
                My.submitException(ex, "Send_Message_to_Student");
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
        UsesCode code = new UsesCode();
        private void BindDetails()
        {
            DataTable dt = code.FillTable("select * from Private_Messages where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                btn_Submit.Text = "Update";
                txt_notice_subject.Text = dt.Rows[0]["Subject"].ToString();
                txt_notice_details.Text = dt.Rows[0]["Details"].ToString();
                txt_link.Text = dt.Rows[0]["Link"].ToString();
                Hd_Photo.Value = dt.Rows[0]["Attachments"].ToString();
                mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name, course_id from Add_course_table   order by Position");
                try
                {

                    if (dt.Rows[0]["Class_Id"].ToString() == "ALL")
                    {
                        ddl_class.SelectedValue = "0";
                        mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor  order by Section");
                    }
                    else
                    {
                        ddl_class.Text = dt.Rows[0]["Class_Id"].ToString();

                        mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
                    }

                    ddl_section.Text = dt.Rows[0]["Section_Id"].ToString();
                }
                catch
                {

                }

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

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "ALL")
            {
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
            }
            else
            {
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
            }
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("View_Sent_Message_to_Student.aspx", false);
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
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
                    else if (txt_notice_subject.Text == "")
                    {
                        Alertme("Please enter subject ", "warning");
                    }
                    else if (txt_notice_details.Text == "")
                    {
                        Alertme("Please enter message details ", "warning");
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

                    else if (txt_notice_subject.Text == "")
                    {
                        Alertme("Please enter subject ", "warning");
                    }
                    else if (txt_notice_details.Text == "")
                    {
                        Alertme("Please enter message details ", "warning");
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
            if (btn_Submit.Text == "Submit")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    string ImagePath = GetImagePath();

                    SqlCommand cmd = new SqlCommand("INSERT INTO Private_Messages (Uid,Message_By,Subject,Details,Sender_Id,Attachments,Date,Idate,Time,Class_Id,Section_Id,Send_Status,Send_Type,Admission_no,Branch_id,Session_id,Created_by,Link) values (@Uid,@Message_By,@Subject,@Details,@Sender_Id,@Attachments,@Date,@Idate,@Time,@Class_Id,@Section_Id,@Send_Status,@Send_Type,@Admission_no,@Branch_id,@Session_id,@Created_by,@Link)");

                    cmd.Parameters.AddWithValue("@Uid", "");
                    cmd.Parameters.AddWithValue("@Message_By", "Admin");
                    cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                    cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                    cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                    cmd.Parameters.AddWithValue("@Sender_Id", ViewState["Admin"].ToString());
                    cmd.Parameters.AddWithValue("@Attachments", ImagePath);
                    cmd.Parameters.AddWithValue("@Date", code.date());
                    cmd.Parameters.AddWithValue("@Idate", code.idate());
                    cmd.Parameters.AddWithValue("@Time", code.time());
                    cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                    if (rd_Class_wise.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
                        cmd.Parameters.AddWithValue("@Admission_no", "N/A");

                        if (ddl_class.SelectedItem.Text == "ALL")
                        {
                            cmd.Parameters.AddWithValue("@Class_Id", "ALL");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Class_Id", ddl_class.SelectedValue);

                        }
                        cmd.Parameters.AddWithValue("@Section_Id", ddl_section.Text);



                    }
                    else
                    {
                        //Dictionary<string, object> dc1 = code.getstudent(txt_admission_no.Text);
                        //string Class_id = (String)dc1["Class_id"];
                        //string Section = (String)dc1["Section"];
                        cmd.Parameters.AddWithValue("@Class_Id", "N/A");
                        cmd.Parameters.AddWithValue("@Section_Id", "N/A");
                        cmd.Parameters.AddWithValue("@Send_Type", "Individual");
                        cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                    }

                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Admin"].ToString());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {


                        Alertme("Message details have been successfully added.", "success");
                        txt_notice_subject.Text = "";
                        txt_notice_details.Text = "";
                        txt_admission_no.Text = "";
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
                    SqlCommand cmd = new SqlCommand("Update Private_Messages set Uid=@Uid,Message_By=@Message_By,Subject=@Subject,Details=@Details,Sender_Id=@Sender_Id,Attachments=@Attachments,Date=@Date,Idate=@Idate,Time=@Time,Class_Id=@Class_Id,Section_Id=@Section_Id,Send_Status=@Send_Status,Send_Type=@Send_Type,Admission_no=@Admission_no,Link=@Link where Id = @Id");
                    cmd.Parameters.AddWithValue("@Uid", "");
                    cmd.Parameters.AddWithValue("@Message_By", "Admin");
                    cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                    cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                    cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                    cmd.Parameters.AddWithValue("@Sender_Id", ViewState["Admin"].ToString());
                    cmd.Parameters.AddWithValue("@Attachments", Hd_Photo.Value);
                    cmd.Parameters.AddWithValue("@Date", code.date());
                    cmd.Parameters.AddWithValue("@Idate", code.idate());
                    cmd.Parameters.AddWithValue("@Time", code.time());




                    cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
                    cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                    if (rd_Class_wise.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
                        cmd.Parameters.AddWithValue("@Admission_no", "N/A");

                        if (ddl_class.SelectedItem.Text == "ALL")
                        {
                            cmd.Parameters.AddWithValue("@Class_Id", "ALL");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Class_Id", ddl_class.SelectedValue);

                        }
                        cmd.Parameters.AddWithValue("@Section_Id", ddl_section.Text);



                    }
                    else
                    {

                        cmd.Parameters.AddWithValue("@Class_Id", "N/A");
                        cmd.Parameters.AddWithValue("@Section_Id", "N/A");
                        cmd.Parameters.AddWithValue("@Send_Type", "Individual");
                        cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                    }
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {

                        btn_Submit.Text = "Submit"; 
                        txt_notice_subject.Text = "";
                        txt_notice_details.Text = "";
                        txt_admission_no.Text = "";

                        Session["SmsG"] = "Message details has been successfully Updated.";
                        Response.Redirect("View_Sent_Message_to_Student.aspx", false);
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
            string Path = code.Upload_doc_images(fl_Photo, "/UploadedImage/Noticeboard/");
            return Path;
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