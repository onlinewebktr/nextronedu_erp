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
    public partial class Send_Message_to_Teacher : System.Web.UI.Page
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

                        ViewState["Admin"] = Session["Admin"].ToString();

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Admin"].ToString());
                        ViewState["session_id"] = My.get_session_id();
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["sesssion"] = My.get_session();
                        string pagename_current = "Message.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Admin"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        code.bind_all_ddl_with_all(ddl_Teacherlist, "select name as Name,user_id as  UserID from user_details where  User_Type!='Admin' and Istatus='1'  order by Name  asc");
                        txt_date.Text = mycode.date();


                        if (Request.QueryString["Id"] != null)
                        {
                            btn_cancel.Visible = true;
                            ViewState["id"] = Request.QueryString["Id"].ToString();
                            BindDetails();
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
            DataTable dt = code.FillTable("select * from Private_Messages_For_Teacher where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                btn_Submit.Text = "Update";

                txt_notice_details.Text = dt.Rows[0]["Details"].ToString();
                txt_notice_subject.Text = dt.Rows[0]["Subject"].ToString();
                txt_link.Text = dt.Rows[0]["Link"].ToString();
                Hd_Photo.Value = dt.Rows[0]["Attachments"].ToString();



                try
                {
                    if (dt.Rows[0]["Teacher_Id"].ToString() == "ALL")
                    {
                        ddl_Teacherlist.SelectedValue = "0";

                    }
                    else
                    {
                        ddl_Teacherlist.Text = dt.Rows[0]["Teacher_Id"].ToString();

                    }

                }
                catch
                {
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
                if (txt_date.Text == "")
                {
                    Alertme("Please enter notice date ", "warning");
                }
                else if (txt_notice_subject.Text == "")
                {
                    Alertme("Please enter subject ", "warning");
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
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        private void final_save_data()
        {
            if (btn_Submit.Text == "Send")
            {


                if (ViewState["Is_add"].ToString() == "1")
                {
                    string ImagePath = GetImagePath();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Private_Messages_For_Teacher (Subject,Details,Attachments,Date,Idate,Time,Send_Status,Teacher_Id,Date_Main,Branch_id,Session_id,Createdby,Link) values (@Subject,@Details,@Attachments,@Date,@Idate,@Time,@Send_Status,@Teacher_Id,@Date_Main,@Branch_id,@Session_id,@Createdby,@Link)");
                    cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                    cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                    cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                    cmd.Parameters.AddWithValue("@Attachments", ImagePath);
                    cmd.Parameters.AddWithValue("@Date", code.date());
                    cmd.Parameters.AddWithValue("@Idate", code.idate());
                    cmd.Parameters.AddWithValue("@Time", code.time());
                    cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                    cmd.Parameters.AddWithValue("@Createdby", ViewState["Admin"].ToString());
                    if (ddl_Teacherlist.SelectedItem.Text == "ALL")
                    {
                        cmd.Parameters.AddWithValue("@Teacher_Id", "ALL");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Teacher_Id", ddl_Teacherlist.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@Date_Main", code.getdate1());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        Alertme("Message has been send sucessfully.", "success");
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
                    SqlCommand cmd = new SqlCommand("Update Private_Messages_For_Teacher set Subject=@Subject,Details=@Details,Attachments=@Attachments,Date=@Date,Idate=@Idate,Time=@Time,Send_Status=@Send_Status,Teacher_Id=@Teacher_Id,Date_Main=@Date_Main,Link=@Link where Id = @Id");

                    cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                    cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                    cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                    cmd.Parameters.AddWithValue("@Attachments", Hd_Photo.Value);
                    cmd.Parameters.AddWithValue("@Date", code.date());
                    cmd.Parameters.AddWithValue("@Idate", code.idate());
                    cmd.Parameters.AddWithValue("@Time", code.time());
                    cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                    if (ddl_Teacherlist.SelectedItem.Text == "ALL")
                    {
                        cmd.Parameters.AddWithValue("@Teacher_Id", "ALL");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Teacher_Id", ddl_Teacherlist.SelectedValue);
                    }

                    cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
                    cmd.Parameters.AddWithValue("@Date_Main", code.getdate1());


                    if (InsertUpdate.InsertUpdateData(cmd))
                    {

                        btn_Submit.Text = "Send"; 
                        txt_notice_details.Text = "";
                        txt_notice_subject.Text = "";

                        Session["SmsG"] = "Message details has been successfully Updated.";
                        Response.Redirect("View_Sent_Message_to_Teacher.aspx", false);

                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }
        UsesCode code = new UsesCode();
        public string GetImagePath()
        {

            string Path = code.Upload_doc_images(fl_Photo, "/UploadedImage/Notice_board/");
            return Path;
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("View_Sent_Message_to_Teacher.aspx", false);
        }
    }
}