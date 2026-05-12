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
    public partial class SMS_Template_Setting : System.Web.UI.Page
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
                        string pagename_current = "SMS_Template_Setting.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        Bind_data_firm_detials();


                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "SMS_Template_Setting");
            }
        }
        private void Bind_data_firm_detials()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        My mycode = new My();
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
        private void bind_grd_view()
        {
            btn_excels.Visible = false;
            print11.Visible = false;
            DataTable dt = mycode.FillData("select * from dbo.[SMS_Template_Setting]");
            if (dt.Rows.Count == 0)
            {

                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                print11.Visible = false;
                rd_view.DataSource = dt;
                rd_view.DataBind();
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print11.Visible = true;
                }
                else
                {
                    print11.Visible = false;
                }
            }

        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();

        }

        private void empty_form()
        {
            txt_type_module.Text = "";
            txt_smstemplate.Text = "";
            txt_sms_variablename.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";

        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_type_module.Text == "")
            {
                Alertme("Please enter message type", "warning");
                txt_smstemplate.Focus();
                return;
            }
            if (txt_smstemplate.Text == "")
            {
                Alertme("Please enter SMS Template", "warning");
                txt_smstemplate.Focus();
                return;
            }
            if (txt_sms_variablename.Text == "")
            {
                Alertme("Please enter sms variable name ", "warning");
                txt_sms_variablename.Focus();
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
            if (btn_Submit.Text == "Add")
            {

                string query = "Select * from SMS_Template_Setting where  Send_From='" + txt_type_module.Text.Trim() + "'";//SMS_Tempate='" + txt_smstemplate.Text + "' and
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    string strQuery = "INSERT INTO SMS_Template_Setting (SMS_Tempate,VariableName,SMSType,Send_From,Is_edit_delete) values (@SMS_Tempate,@VariableName,@SMSType,@Send_From,@Is_edit_delete)";
                    cmd = new SqlCommand(strQuery);
                    cmd.Parameters.AddWithValue("@SMS_Tempate", txt_smstemplate.Text);
                    cmd.Parameters.AddWithValue("@VariableName", txt_sms_variablename.Text);
                    cmd.Parameters.AddWithValue("@SMSType", ddl_type.Text);
                    cmd.Parameters.AddWithValue("@Send_From", txt_type_module.Text.Trim());
                    cmd.Parameters.AddWithValue("@Is_edit_delete", 0);
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("SMS template has been add successfully", "success");
                    }
                }
                else
                {
                    Alertme("SMS Configuration has been set successfully", "success");
                }
            }
            else
            {
                string query = "Select * from SMS_Template_Setting where   Send_From='" + txt_type_module.Text.Trim() + "' and Id!=" + hd_id.Value + "";//SMS_Tempate='" + txt_smstemplate.Text + "' and 
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    string strQuery = "update SMS_Template_Setting set SMS_Tempate=@SMS_Tempate,VariableName=@VariableName,SMSType=@SMSType,Send_From=@Send_From where Id=@Id";
                    cmd = new SqlCommand(strQuery);
                    cmd.Parameters.AddWithValue("@SMS_Tempate", txt_smstemplate.Text);
                    cmd.Parameters.AddWithValue("@VariableName", txt_sms_variablename.Text);
                    cmd.Parameters.AddWithValue("@SMSType", ddl_type.Text);
                    cmd.Parameters.AddWithValue("@Send_From", txt_type_module.Text.Trim());

                    cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("SMS template has been update successfully", "success");
                    }
                }
                else
                {
                    Alertme("This template already added", "success");
                }

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
                    My.exeSql("delete from SMS_Template_Setting where Id=" + lbl_Id.Text + "");
                    Alertme("SMS Template has been deleted Successfully", "success");
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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_SMS_Tempate = (Label)row.FindControl("lbl_SMS_Tempate");
                    Label lbl_SMSType = (Label)row.FindControl("lbl_SMSType");
                    Label lbl_VariableName = (Label)row.FindControl("lbl_VariableName");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_Send_From = (Label)row.FindControl("lbl_Send_From");

                    txt_type_module.Text = lbl_Send_From.Text;
                    hd_id.Value = lbl_Id.Text;
                    txt_smstemplate.Text = lbl_SMS_Tempate.Text;
                    txt_sms_variablename.Text = lbl_VariableName.Text;
                    ddl_type.Text = lbl_SMSType.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);


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

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    if (((Label)e.Item.FindControl("lbl_Is_edit_delete")).Text.ToUpper() == "FALSE")
                    {
                        ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
                        ((LinkButton)e.Item.FindControl("lnkDel")).Visible = true;
                    }
                    else
                    {
                        ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = false;
                        ((LinkButton)e.Item.FindControl("lnkDel")).Visible = false;
                    }
                }
                catch
                {
                    ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
                    ((LinkButton)e.Item.FindControl("lnkDel")).Visible = true;
                }
               
            }
        }

        protected bool IsChecked = true;
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                this.IsChecked = false;
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Message_Template" + mycode.date() + "_" + mycode.itime() + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    Panel1.RenderControl(hw);
                    string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }

            }
            catch
            {
            }
            this.IsChecked = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}