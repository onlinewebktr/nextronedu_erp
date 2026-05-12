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
    public partial class SMS_Config : System.Web.UI.Page
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
                        string pagename_current = "Application_List_For_Career.aspx";
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
                My.submitException(ex, "SMS_Config");
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
            DataTable dt = mycode.FillData("select * from dbo.[message_config]");
            if (dt.Rows.Count == 0)
            {
              
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
            txt_senderid.Text = "";
            txt_sms_authorizationid.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {

            if (txt_senderid.Text == "")
            {
                Alertme("Please enter sender id", "warning");
                txt_senderid.Focus();
                return;
            }
            if (txt_sms_authorizationid.Text == "")
            {
                Alertme("Please enter sms authorization key", "warning");
                txt_senderid.Focus();
                return;
            }


            if (ViewState["Is_add"].ToString() == "1")
            {
                submit_details();
                empty_form();
                bind_grd_view();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
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



        private void submit_details()
        {
            string query = "Select * from message_config";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string strQuery = "INSERT INTO message_config (sender,uid,Status) values (@sender,@uid,@Status)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@sender", txt_senderid.Text);
                cmd.Parameters.AddWithValue("@uid", txt_sms_authorizationid.Text);
                cmd.Parameters.AddWithValue("@Status", "running");
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("SMS Configuration has been set successfully", "success");
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd;
                string strQuery = "Update message_config set uid=@uid,sender=@sender where Id = @Id";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@sender", txt_senderid.Text);
                cmd.Parameters.AddWithValue("@uid", txt_sms_authorizationid.Text);
                cmd.Parameters.AddWithValue("@Id", id);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("SMS Configuration has been updated successfully", "success");
                }
            }
        }

        private void empty_form()
        {

            txt_senderid.Text = "";
            txt_sms_authorizationid.Text = "";
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
                    Label lbl_sender = (Label)row.FindControl("lbl_sender");
                    Label lbl_uid = (Label)row.FindControl("lbl_uid");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;
                    txt_senderid.Text = lbl_sender.Text;
                    txt_sms_authorizationid.Text = lbl_uid.Text;
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
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    My.exeSql("delete from message_config where Id=" + lbl_Id.Text + "");
                    Alertme("SMS Configuration has been deleted Successfully", "success");
                    bind_grd_view();
                    empty_form();
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
    }
}
