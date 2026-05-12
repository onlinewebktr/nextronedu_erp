using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace school_web.Admin
{
    public partial class Send_Events_to_Student : System.Web.UI.Page
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
                        string pagename_current = "Event.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["userid"].ToString());
                        ViewState["session_id"] = My.get_session_id();
                        ViewState["sesssionid"] = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name, course_id from Add_course_table   order by Position");
                        mycode.bind_ddlall(ddl_section, "Select distinct section  from section_master   order by section");
                        txt_date.Text = mycode.date();
                        BindGridView();

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
                My.submitException(ex, "Send_Events_to_Student");
            }
        }

        private void BindGridView()
        {
            mycode.BindRepeater("select top 10 * from News_Events_Details where Session_id=" + ViewState["session_id"].ToString() + "  order by  Posted_Idate Desc", RPDetails);
        }

        private void BindDetails()
        {
            DataTable dt = mycode.FillData("select * from News_Events_Details where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                btn_Submit.Text = "Update";
                txt_notice_subject.Text = dt.Rows[0]["Heading"].ToString();
                txt_notice_details.Text = dt.Rows[0]["Details"].ToString();
                Hd_Photo.Value = dt.Rows[0]["Attachments"].ToString();
                txt_date.Text = dt.Rows[0]["Posted_Date"].ToString();
                txt_link.Text = dt.Rows[0]["Link"].ToString();
                mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name, course_id from Add_course_table   order by Position");
                if (dt.Rows[0]["Class"].ToString() == "ALL")
                {
                    ddl_class.Text = "0";
                    mycode.bind_ddlall(ddl_section, "Select distinct section  from section_master   order by section");
                }
                else
                {
                    ddl_class.Text = dt.Rows[0]["Class"].ToString();
                    mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
                }
                ddl_section.Text = dt.Rows[0]["Section"].ToString();

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
                    Alertme("Please select a date from the calendar", "warning");

                }
                else if (txt_notice_subject.Text == "")
                {
                    Alertme("Please Insert Subject Name.", "warning");
                }
                else if (txt_notice_details.Text == "")
                {
                    Alertme("Please Insert Subject Name.", "warning");
                }
                else
                {
                    if (btn_Submit.Text == "Submit")
                    {

                        if (ViewState["Is_add"].ToString() == "1")
                        {

                            string ImagePath = GetImagePath();

                            SqlCommand cmd = new SqlCommand("INSERT INTO News_Events_Details (Heading,Details,Posted_By,Posted_Date,Posted_Idate,Posted_Time,Attachments,Class,Section,Session_id,Branchid,Link,Send_Status,Date_Main) values (@Heading,@Details,@Posted_By,@Posted_Date,@Posted_Idate,@Posted_Time,@Attachments,@Class,@Section,@Session_id,@Branchid,@Link,@Send_Status,@Date_Main)");
                            cmd.Parameters.AddWithValue("@Heading", txt_notice_subject.Text);
                            cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                            cmd.Parameters.AddWithValue("@Posted_By", Session["Admin"].ToString());
                            cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                            cmd.Parameters.AddWithValue("@Posted_Idate", mycode.ConvertStringToiDate(txt_date.Text));
                            cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
                            cmd.Parameters.AddWithValue("@Attachments", ImagePath);
                            cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                            if (ddl_class.SelectedItem.Text == "ALL")
                            {
                                cmd.Parameters.AddWithValue("@Class", "ALL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);

                            }
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);

                            cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                            cmd.Parameters.AddWithValue("@Branchid", ViewState["Branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Send_Status", "Send");
                            cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                update_add_push();
                                BindGridView();
                                Alertme("Events details have been successfully added.", "success");
                                txt_notice_subject.Text = "";
                                txt_notice_details.Text = "";
                                txt_date.Text = "";
                                txt_link.Text = "";
                                mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name, course_id from Add_course_table   order by Position");
                                if (ddl_class.SelectedItem.Text == "ALL")
                                {
                                    mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor   order by Section");
                                }
                                else
                                {
                                    mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'  order by Section");
                                }

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
                            if (fl_Photo.HasFile) { Hd_Photo.Value = GetImagePath(); }
                            else { }
                            SqlCommand cmd = new SqlCommand("Update News_Events_Details set Heading=@Heading,Details=@Details,Posted_By=@Posted_By,Posted_Date=@Posted_Date,Posted_Idate=@Posted_Idate,Posted_Time=@Posted_Time,Attachments=@Attachments,Class=@Class,Section=@Section,Link=@Link,Send_Status=@Send_Status,Date_Main=@Date_Main where Id = @Id");
                            cmd.Parameters.AddWithValue("@Heading", txt_notice_subject.Text);
                            cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                            cmd.Parameters.AddWithValue("@Posted_By", Session["Admin"].ToString());
                            cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                            cmd.Parameters.AddWithValue("@Posted_Idate", mycode.ConvertStringToiDate(txt_date.Text));
                            cmd.Parameters.AddWithValue("@Posted_Time", mycode.time());
                            cmd.Parameters.AddWithValue("@Attachments", Hd_Photo.Value);
                            cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
                            cmd.Parameters.AddWithValue("@Link", txt_link.Text);
                            cmd.Parameters.AddWithValue("@Send_Status", "Send");
                            if (ddl_class.SelectedItem.Text == "ALL")
                            {
                                cmd.Parameters.AddWithValue("@Class", "ALL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);

                            }
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            cmd.Parameters.AddWithValue("@Date_Main", mycode.getdate2(txt_date.Text));
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                update_add_push();
                                BindGridView();
                                btn_Submit.Text = "Submit";
                                Alertme(" Event details have been successfully Updated.", "success");
                                txt_notice_subject.Text = "";
                                txt_notice_details.Text = "";
                                txt_date.Text = "";
                                mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name, course_id from Add_course_table   order by Position");
                                if (ddl_class.SelectedItem.Text == "ALL")
                                {
                                    mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor   order by Section");
                                }
                                else
                                {
                                    mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'  order by Section");
                                }
                            }
                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                        }

                    }

                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }



        UsesCode code = new UsesCode();
        public string GetImagePath()
        {

            string Path = code.Upload_doc_images(fl_Photo, "/UploadedImage/Noticeboard/");
            return Path;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("View_All_Events_Send_to_Student.aspx", false);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_id = (Label)row.FindControl("lbl_Id");
                    Label lbl_Heading = (Label)row.FindControl("lbl_Heading");
                    Label lbl_Details = (Label)row.FindControl("lbl_Details");
                    Label lbl_Attachments = (Label)row.FindControl("lbl_Attachments");
                    Label lbl_date = (Label)row.FindControl("lbl_date");
                    Label lbl_Classid = (Label)row.FindControl("lbl_Classid");
                    Label lbl_section = (Label)row.FindControl("lbl_section");
                    Label lbl_links = (Label)row.FindControl("lbl_links");
                    ViewState["id"] = lbl_id.Text;
                    btn_Submit.Text = "Update";
                    txt_notice_subject.Text = lbl_Heading.Text;
                    txt_notice_details.Text = lbl_Details.Text;
                    Hd_Photo.Value = lbl_Attachments.Text;
                    txt_date.Text = lbl_date.Text;
                    txt_link.Text = lbl_links.Text;
                    mycode.bind_all_ddl_with_id_cap_All(ddl_class, " Select distinct class, Class_id from admission_registor  order by class");

                    if (lbl_Classid.Text == "ALL")
                    {
                        ddl_class.Text = "0";
                        mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor  order by Section");
                    }
                    else
                    {
                        ddl_class.Text = lbl_Classid.Text;
                        mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
                    }
                    ddl_section.Text = lbl_section.Text;
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_id = (Label)row.FindControl("lbl_Id");
                    SqlCommand cmd = new SqlCommand("Delete from News_Events_Details where Id='" + lbl_id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                    BindGridView();
                    btn_Submit.Text = "Submit";
                    Alertme("successfully deleted.", "success");
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    if (((Label)e.Item.FindControl("lbl_Classid")).Text == "ALL")
                    {
                        ((Label)e.Item.FindControl("lbl_class")).Text = "ALL";
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lbl_class")).Text = code.get_class_name(((Label)e.Item.FindControl("lbl_Classid")).Text);

                    }
                    if (((Label)e.Item.FindControl("lbl_Attachments")).Text == "")
                    {
                        HtmlAnchor a1 = e.Item.FindControl("a1") as HtmlAnchor;
                        a1.Visible = false;
                    }
                    else
                    {
                        HtmlAnchor a1 = e.Item.FindControl("a1") as HtmlAnchor;
                        a1.Visible = true;
                    }



                    if (((Label)e.Item.FindControl("lbl_links")).Text == "")
                    {
                        HtmlAnchor a2 = e.Item.FindControl("a2") as HtmlAnchor;
                        a2.Visible = false;
                    }
                    else
                    {
                        HtmlAnchor a2 = e.Item.FindControl("a2") as HtmlAnchor;
                        a2.Visible = true;
                    }
                }
            }
            catch { }
        }
        private void update_add_push()
        {
            string query = "";


            if (ddl_class.SelectedItem.Text == "ALL")
            {
                if (ddl_section.Text == "ALL")
                {
                    query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Session_id='" + My.get_session_id() + "' ";
                }
                else
                {
                    query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Section='" + ddl_section.Text + "' and Session_id='" + My.get_session_id() + "' and Session_id='" + My.get_session_id() + "'";
                }

            }
            else
            {
                if (ddl_section.Text == "ALL")
                {
                    query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Class_id='" + ddl_class.SelectedValue + "' and Session_id='" + My.get_session_id() + "'";

                }
                else
                {
                    query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + My.get_session_id() + "'";
                }

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
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    final_send_push(gcmid, admissionserialnumber);

                }

            }
        }

        private void final_send_push(string gcmid, string admissionserialnumber)
        {
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
                ss["UserId"] = admissionserialnumber;
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