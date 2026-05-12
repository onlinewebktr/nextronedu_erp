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
    public partial class notice_template_setting : System.Web.UI.Page
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

                        mycode.bind_all_ddl_with_id(ddl_templete_type, "select Notice_type,Notice_type_id from Auto_notice_type where Status='1'");
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
            DataTable dt = mycode.FillData("select *,(select top 1  Notice_type from Auto_notice_type where Notice_type_id=Auto_notice_template.Notice_type) as Notice_type_name from Auto_notice_template order by Id desc");
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
                fetch_variables();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }
        }

        private void fetch_variables()
        {
            DataTable dt = My.dataTable("select * from Auto_notice_type where Notice_type_id='" + ddl_templete_type.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                lbl_variables.Text = dt.Rows[0]["All_variable"].ToString();
                lbl_variables.Visible = true;
            }
            else
            {
                lbl_variables.Text = "";
                lbl_variables.Visible = false;
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_templete_type.SelectedItem.Text == "Select")
                {
                    Alertme("Please select templete type.", "warning");
                    ddl_templete_type.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
                }
                else if (txt_notice.Text == "")
                {
                    Alertme("Please enter templete.", "warning");
                    txt_notice.Focus();
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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdds();", true);
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
            SqlCommand cmd;
            string query = "INSERT INTO Auto_notice_template (Notice_type,Notice_message,Status,Created_by,Created_date,Created_idate) values (@Notice_type,@Notice_message,@Status,@Created_by,@Created_date,@Created_idate)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Notice_type", ddl_templete_type.SelectedValue);
            cmd.Parameters.AddWithValue("@Notice_message", txt_notice.Text);
            cmd.Parameters.AddWithValue("@Status", 0);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Templete has been created successfully.", "success");
                empty_form();
                bind_grd_view();
            }
        }
        private void update_details()
        {
            SqlCommand cmd;
            string query = "Update Auto_notice_template set Notice_type=@Notice_type,Notice_message=@Notice_message  where Id = '" + hd_id.Value + "'";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Notice_type", ddl_templete_type.SelectedValue);
            cmd.Parameters.AddWithValue("@Notice_message", txt_notice.Text);
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Item details has been updated successfully", "success");
                empty_form();
                bind_grd_view();
            }
        }

        private void empty_form()
        {
            txt_notice.Text = "";
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
                    Label lbl_notice_message = (Label)row.FindControl("lbl_notice_message");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;
                    ddl_templete_type.SelectedValue = lbl_notice_id.Text;
                    txt_notice.Text = lbl_notice_message.Text;
                    fetch_variables();
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
                My.exeSql("delete from Auto_notice_template where Id='" + lbl_Id.Text + "'");
                Alertme("Template has been deleted successfully.", "success");
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
                    Label lbl_notice_id = (Label)row.FindControl("lbl_notice_id");
                    string status = "1";
                    if (lbl_status.Text == "1")
                    {
                        status = "0";
                    }
                    mycode.executequery("update Auto_notice_template set Status='" + status + "' where Id='" + lbl_id.Text + "'; update Auto_notice_template set Status='0' where Id!='" + lbl_id.Text + "' and Notice_type='" + lbl_notice_id.Text + "'");
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