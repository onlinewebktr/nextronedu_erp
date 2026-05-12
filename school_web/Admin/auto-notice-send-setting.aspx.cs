using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class auto_notice_send_setting : System.Web.UI.Page
    {
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
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        mycode.bind_all_ddl_with_id(ddl_templete_type, "select Notice_type,Notice_type_id from Auto_notice_type where Status='1' and Is_send_time_set=1");
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }
        }
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Notice_type from Auto_notice_type where Notice_type_id=Auto_notice_send_setting.Notice_type) as Notice_type_name,isnull((select top 1 Notice_message from Auto_notice_template where Notice_type=Auto_notice_send_setting.Notice_type and Status=1),'NA') as Message_template from Auto_notice_send_setting order by Notice_type asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void ddl_templete_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_template(); 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }
        }

        private void fetch_template()
        {
            DataTable dt = My.dataTable("select * from Auto_notice_template where Notice_type='" + ddl_templete_type.SelectedValue + "' and Status=1");
            if (dt.Rows.Count > 0)
            {
                lbl_msg_template.Text = dt.Rows[0]["Notice_message"].ToString();
                btn_Submit.Visible = true;
                msgTemplate.Visible = true;
            }
            else
            {
                btn_Submit.Visible = false;
                msgTemplate.Visible = false;
                Alertme("Active template not found.", "warning");
            }
        }



        ///===================================
        ///
        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            try
            {
                if (ddl_templete_type.SelectedItem.Text == "Select")
                {
                    Alertme("Please select templete type.", "warning");
                    ddl_templete_type.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
                }
                else if (ddl_date.Text == "Select")
                {
                    Alertme("Please select send date.", "warning");
                    ddl_date.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
                }
                else if (ddl_hour.Text == "Hour")
                {
                    Alertme("Please select send hour.", "warning");
                    ddl_hour.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
                }
                else if (ddl_minute.Text == "Minute")
                {
                    Alertme("Please select send minut.", "warning");
                    ddl_minute.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
                }
                else
                {
                    if (btn_Submit.Text == "Add")
                    {
                        if (ViewState["Is_add"].ToString() == "1")
                        {
                            submit_details();
                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                        }
                    }
                    else
                    {
                        if (ViewState["Is_Edit"].ToString() == "1")
                        {

                            update_details();
                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void submit_details()
        {
            if (mycode.IsUserExist("select Notice_type from Auto_notice_send_setting where Send_date_day='" + ddl_date.Text + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Auto_notice_send_setting (Notice_type,Send_date_day,Time_hr,Time_mn,Time_ap_pm,Status,Created_by,Created_date,Created_idate) values (@Notice_type,@Send_date_day,@Time_hr,@Time_mn,@Time_ap_pm,@Status,@Created_by,@Created_date,@Created_idate)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Notice_type", ddl_templete_type.SelectedValue);
                cmd.Parameters.AddWithValue("@Send_date_day", ddl_date.Text);
                cmd.Parameters.AddWithValue("@Time_hr", ddl_hour.Text);
                cmd.Parameters.AddWithValue("@Time_mn", ddl_minute.Text);
                cmd.Parameters.AddWithValue("@Time_ap_pm", ddl_am_pm.Text);
                cmd.Parameters.AddWithValue("@Status", 0);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Templete setting has been created successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
                Alertme("Templete already added with this type.", "success");
            }
        }
        private void update_details()
        {
            SqlCommand cmd;
            string query = "Update Auto_notice_send_setting set Notice_type=@Notice_type,Send_date_day=@Send_date_day,Time_hr=@Time_hr,Time_mn=@Time_mn,Time_ap_pm=@Time_ap_pm  where Id = '" + hd_id.Value + "'";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Notice_type", ddl_templete_type.SelectedValue);
            cmd.Parameters.AddWithValue("@Send_date_day", ddl_date.Text);
            cmd.Parameters.AddWithValue("@Time_hr", ddl_hour.Text);
            cmd.Parameters.AddWithValue("@Time_mn", ddl_minute.Text);
            cmd.Parameters.AddWithValue("@Time_ap_pm", ddl_am_pm.Text);
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Templete setting has been updated successfully.", "success");
                empty_form();
                bind_grd_view();
            }
        }

        private void empty_form()
        {
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_notice_id = (Label)row.FindControl("lbl_notice_id");
                    Label lbl_send_date_day = (Label)row.FindControl("lbl_send_date_day");
                    Label lbl_time_hr = (Label)row.FindControl("lbl_time_hr");
                    Label lbl_time_mn = (Label)row.FindControl("lbl_time_mn");
                    Label lbl_time_ap_pm = (Label)row.FindControl("lbl_time_ap_pm");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");

                    ddl_templete_type.SelectedValue = lbl_notice_id.Text;
                    ddl_date.Text = lbl_send_date_day.Text;
                    ddl_hour.Text = lbl_time_hr.Text;
                    ddl_minute.Text = lbl_time_mn.Text;
                    ddl_am_pm.Text = lbl_time_ap_pm.Text;
                    hd_id.Value = lbl_Id.Text;

                    fetch_template();
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
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


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                My.exeSql("delete from Auto_notice_send_setting where Id='" + lbl_Id.Text + "'");
                Alertme("Template setting has been deleted successfully.", "success");
                bind_grd_view();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (((Label)e.Item.FindControl("lbl_status")).Text == "1")
                    {
                        ((LinkButton)e.Item.FindControl("lnk_status")).Text = "Active";
                        ((LinkButton)e.Item.FindControl("lnk_status")).CssClass = "isyes";
                    }
                    else
                    {
                        ((LinkButton)e.Item.FindControl("lnk_status")).Text = "Inactive";
                        ((LinkButton)e.Item.FindControl("lnk_status")).CssClass = "isno";
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void lnk_status_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_id = (Label)row.FindControl("lbl_id");
                    Label lbl_status = (Label)row.FindControl("lbl_status");

                    string status = "1";
                    if (lbl_status.Text == "1")
                    {
                        status = "0";
                    }
                    mycode.executequery("update Auto_notice_send_setting set Status='" + status + "' where Id='" + lbl_id.Text + "'");
                    Alertme("Record has been updated successfully", "success");
                    bind_grd_view();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}