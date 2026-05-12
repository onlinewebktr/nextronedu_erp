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
    public partial class Send_Events_To_Staff : System.Web.UI.Page
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
                        Dictionary<string, object> dc2 = My.get_push_credantial();
                        ViewState["type"] = (String)dc2["type"];
                        ViewState["project_id"] = (String)dc2["project_id"];
                        ViewState["private_key_id"] = (String)dc2["private_key_id"];
                        ViewState["client_email"] = (String)dc2["client_email"];
                        ViewState["client_id"] = (String)dc2["client_id"];
                        ViewState["private_key"] = dc2["private_key"].ToString().Replace("\\n", "\n");


                        ViewState["userid"] = Session["Admin"].ToString();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["userid"].ToString());
                        ViewState["session_id"] = My.get_session_id();
                        ViewState["sesssionid"] = ViewState["session_id"].ToString();
                        ViewState["sesssion"] = My.get_session();
                        string pagename_current = "Notice.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        mycode.bind_all_ddl_with_id_cap_All(ddl_Teacherlist, "select name as Name,user_id as  UserID from user_details where  User_Type!='Admin' and Istatus='1'  order by Name  asc");
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
            DataTable dt = mycode.FillData("select * from News_Events_Details_Teacher where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                btn_Submit.Text = "Update";
                txt_link.Text= dt.Rows[0]["Link"].ToString();
                txt_notice_subject.Text = dt.Rows[0]["Heading"].ToString();
                txt_notice_details.Text = dt.Rows[0]["Details"].ToString();
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
                txt_date.Text = dt.Rows[0]["Posted_Date"].ToString();

                Hd_Photo.Value = dt.Rows[0]["Attachment"].ToString();
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
        protected void btn_Submit_Click(object sender, EventArgs e)
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
                    Alertme("Please enter events details ", "warning");
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
                    string filePath = "";
                    if (fl_Photo.HasFile)
                    {
                        filePath = GetImagePath();
                        if (filePath == "")
                        {
                            Alertme("Please choose attachment.", "warning");
                            return;
                        }
                    }
                   
                    SqlCommand cmd = new SqlCommand("INSERT INTO News_Events_Details_Teacher (Heading,Details,Posted_Date,Posted_Idate,Posted_Time,Send_Status,Attachment,Date_Main,Teacher_Id,Session_id,Branch_id,Posted_By,Link) values (@Heading,@Details,@Posted_Date,@Posted_Idate,@Posted_Time,@Send_Status,@Attachment,@Date_Main,@Teacher_Id,@Session_id,@Branch_id,@Posted_By,@Link)");
                    cmd.Parameters.AddWithValue("@Link",txt_link.Text);

                    
                    cmd.Parameters.AddWithValue("@Heading", txt_notice_subject.Text);
                    cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                    cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                    cmd.Parameters.AddWithValue("@Posted_Idate", mycode.ConvertStringToiDate(txt_date.Text));
                    cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
                    cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                    cmd.Parameters.AddWithValue("@Attachment", filePath);
                    cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
                    if (ddl_Teacherlist.SelectedItem.Text == "ALL")
                    {
                        cmd.Parameters.AddWithValue("@Teacher_Id", "ALL");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Teacher_Id", ddl_Teacherlist.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());

                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                    cmd.Parameters.AddWithValue("@Posted_By", ViewState["userid"].ToString());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        update_add_push();
                        Alertme("Notice have been send sucessfully.", "success");
                        txt_link.Text = "";
                        txt_notice_details.Text = "";
                        txt_notice_subject.Text = "";
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            if (btn_Submit.Text == "Update")
            {
                string filePath = "";
                if (fl_Photo.HasFile)
                {
                    filePath = GetImagePath();
                    if (filePath == "")
                    {
                        Alertme("Please choose attachment.", "warning");
                        return;
                    }
                }
                else
                {
                    filePath = Hd_Photo.Value;
                }
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                   
                    SqlCommand cmd = new SqlCommand("Update News_Events_Details_Teacher set Heading=@Heading,Details=@Details,Posted_Date=@Posted_Date,Posted_Idate=@Posted_Idate,Posted_Time=@Posted_Time,Send_Status=@Send_Status,Attachment=@Attachment,Date_Main=@Date_Main,Teacher_Id=@Teacher_Id,Posted_By=@Posted_By,Link=@Link where Id = @Id");
                    cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                    cmd.Parameters.AddWithValue("@Heading", txt_notice_subject.Text);
                    cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);

                    cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                    cmd.Parameters.AddWithValue("@Posted_Idate", mycode.ConvertStringToiDate(txt_date.Text));
                    cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
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
                    cmd.Parameters.AddWithValue("@Attachment", filePath);
                    cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
                    cmd.Parameters.AddWithValue("@Posted_By", ViewState["userid"].ToString());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        update_add_push();
                        txt_link.Text = "";
                        btn_Submit.Text = "Send";
                        btn_cancel.Visible = false;
                        txt_notice_subject.Text = "";
                        txt_notice_details.Text = "";
                        Session["SmsG"] = "Events details have been successfully Updated.";
                        Response.Redirect("View_All_Sent_Events_to_staff.aspx", false);
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }

        private void update_add_push()
        {
            string query = "";

            if (ddl_Teacherlist.SelectedItem.Text == "ALL")
            {

                query = " select gcm_id,user_id from dbo.[user_details] where User_Type!='Admin' and Istatus='1' ";



            }
            else
            {
                query = " select gcm_id,user_id from dbo.[user_details] where   user_id='" + ddl_Teacherlist.SelectedValue + "' and Istatus='1'";

            }


           

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string UserID = dt.Rows[i]["user_id"].ToString();
                    if (gcmid == "")
                    {
                        gcmid = "0";

                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = txt_notice_details.Text;
                        ss["title"] = "Events";
                        ss["messagetype"] = "Events";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = UserID;
                        ss["type"] = ViewState["type"].ToString();
                        ss["project_id"] = ViewState["project_id"].ToString();
                        ss["private_key_id"] = ViewState["private_key_id"].ToString();
                        ss["client_email"] = ViewState["client_email"].ToString();
                        ss["client_id"] = ViewState["client_id"].ToString();
                        ss["private_key"] = ViewState["private_key"].ToString();

                        My.onlypush(gcmid, ss);
                    }

                }

            }
        }

        UsesCode code = new UsesCode();
        private string GetImagePath()
        {
            string Path = code.Upload_doc_images(fl_Photo, "/UploadedImage/Notice_board/");
            return Path;
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Send_Events_To_Staff.aspx",false);
        }
    }
}