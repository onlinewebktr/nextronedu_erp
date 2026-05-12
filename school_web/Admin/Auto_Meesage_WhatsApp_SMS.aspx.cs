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
    public partial class Auto_Meesage_WhatsApp_SMS : System.Web.UI.Page
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
                        string pagename_current = "Auto_Meesage_WhatsApp_SMS.aspx";
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
            DataTable dt = mycode.FillData("select * from dbo.[SMS_Template_Setting] where Is_edit_delete=1");
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
        protected void btn_update_Click(object sender, EventArgs e)
        {
            
            int i = 0;
            foreach (RepeaterItem row in rd_view.Items)
            {
                int autosms = 0;
                int autowhatsaap = 0;
                CheckBox auto_sms = rd_view.Items[i].FindControl("chk_autosms") as CheckBox;
                CheckBox auto_whatsaap = rd_view.Items[i].FindControl("chk_auto_whatsaap") as CheckBox;
                Label lbl_id = rd_view.Items[i].FindControl("lbl_id") as Label;
                if (auto_sms.Checked == true)
                {
                    autosms = 1;
                }
                if (auto_whatsaap.Checked == true)
                {
                    autowhatsaap = 1;
                }
                SqlCommand cmd = new SqlCommand("Update SMS_Template_Setting set Is_Send_SMS=" + autosms + ",Is_Send_WhatsApp='" + autowhatsaap + "' where Id=" + lbl_id.Text + "");
                InsertUpdate.InsertUpdateData(cmd);
                i++;
            }
            Alertme("Auto Message has been updated successfully", "success");

        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {

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

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
             if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    if (((Label)e.Item.FindControl("lbl_Is_Send_SMS")).Text.ToUpper() == "TRUE")
                    {
                        ((CheckBox)e.Item.FindControl("chk_autosms")).Checked = true;
                        

                    }
                    else
                    {
                        ((CheckBox)e.Item.FindControl("chk_autosms")).Checked = false;

                    }

                    if (((Label)e.Item.FindControl("lbl_Is_Send_WhatsApp")).Text.ToUpper() == "TRUE")
                    {
                        ((CheckBox)e.Item.FindControl("chk_auto_whatsaap")).Checked = true;

                    }
                    else
                    {

                        ((CheckBox)e.Item.FindControl("chk_auto_whatsaap")).Checked = false;
                    }
                }
                catch
                {
                    ((CheckBox)e.Item.FindControl("chk_auto_whatsaap")).Checked = false;
                    ((CheckBox)e.Item.FindControl("chk_autosms")).Checked = false;
                }

            }
        }
    }
}