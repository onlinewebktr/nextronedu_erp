using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Admin
{
    public partial class Set_Birthday_Message_Template : System.Web.UI.Page
    {
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

                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set_Birthday_Message_Template");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select * from dbo.[Birthday_Message_Template]");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_message.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {

            if (txt_message.Text == "")
            {
                Alertme("Please message", "warning");
                txt_message.Focus();
                return;
            }

            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    submit_details();
                    empty_form();
                    bind_grd_view();
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
                    update_update_details();
                    empty_form();
                    bind_grd_view();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }

        private void update_update_details()
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Birthday_Message_Template where id!='" + hd_id.Value + "' and Message=N'" + txt_message.Text + "' ");
            if (dt.Rows.Count == 0)
            {
                string query = "Update Birthday_Message_Template set Message=@Message,Updated_by=@Updated_by,Updated_Date=@Updated_Date where Id = @Id";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Message", txt_message.Text);
                cmd.Parameters.AddWithValue("@Updated_Date", My.getdate1());
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());

                cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("  Birthay message template has been sucessfully updated ", "success");
                    empty_form();
                }

            }
            else
            {
                Alertme("Sorry your message template already exists ", "warning");
            }

        }

        private void submit_details()
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Birthday_Message_Template where Message=N'" + txt_message.Text + "'  ");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Birthday_Message_Template (Message,Template_id,AddedDate,Created_By,Status) values (@Message,@Template_id,@AddedDate,@Created_By,@Status)";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Message", txt_message.Text);
                cmd.Parameters.AddWithValue("@AddedDate", My.getdate1());
                cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Status", "Inactive");
                cmd.Parameters.AddWithValue("@Template_id", My.auto_serialS("group_id"));
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("  Birthay message template has been sucessfully added ", "success");
                    empty_form();
                }
            }
            else
            {

                Alertme("Sorry your message template already exists ", "warning");
            }


        }

        private void empty_form()
        {
            txt_message.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
            bind_grd_view();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Messagee = (Label)row.FindControl("lbl_Messagee");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;

                    txt_message.Text = lbl_Messagee.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
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
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_cat_id = (Label)row.FindControl("lbl_cat_id");
                    mycode.executequery("Delete from Birthday_Message_Template where Id=" + lbl_Id.Text + "");
                    Alertme("Birthday message has been successfully deleted", "success");
                    bind_grd_view();
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

        protected void lnk_defultsession_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        LinkButton lnk = (LinkButton)sender;
                        RepeaterItem row = (RepeaterItem)lnk.NamingContainer;

                        Label Id = (Label)row.FindControl("lbl_id");
                        mycode.executequery("update Birthday_Message_Template set Status='Active'   where Id=" + Id.Text + "");
                        mycode.executequery("update Birthday_Message_Template set Status='Inactive'   where Id!=" + Id.Text + "");
                        Alertme("Birthday message has been successfully active", "success");

                        bind_grd_view();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");

                    }

                }
                catch { }
            }
            catch
            {
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (((Label)e.Item.FindControl("lbl_active")).Text == "Active")
                {
                    ((LinkButton)e.Item.FindControl("lnk_defultsession")).Visible = false;
                    ((Label)e.Item.FindControl("lbl_active")).CssClass = "badge badge-success ml-2";

                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lnk_defultsession")).Visible = true;
                    ((Label)e.Item.FindControl("lbl_active")).CssClass = "badge badge-danger ml-2";
                }
            }
        }
    }
}