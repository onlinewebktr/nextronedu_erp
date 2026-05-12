using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Add_GST : System.Web.UI.Page
    {
        string scrpt;
        private void Alertme(string msg)
        {
            lbl_success.Text = msg;
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('logout.aspx','_parent',replace=true);</script>");
            }
            else
            {
                ViewState["Userid"] = Session["Admin"].ToString();
                string pagename_current = Path.GetFileName(Request.Path);
                //Dictionary<string, object> dc1 = //My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                ViewState["Is_Edit"] = "1"; //(String)dc1["Is_Edit"];
                ViewState["Is_delete"] = "1";//(String)dc1["Is_delete"];
                ViewState["Is_Download"] = "1"; //(String)dc1["Is_Download"];
                ViewState["Is_Print"] = "1"; //(String)dc1["Is_Print"];
                ViewState["Is_add"] = "1"; //(String)dc1["Is_add"];
                if (!IsPostBack)
                { bind_grd_view(); }
            }

        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }

        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_tax_type.Text == "")
                {
                    Alertme("Please Select Tax Type");
                    ddl_tax_type.Focus();
                    return;
                }
                if (txt_tax_percent.Text == "")
                {
                    Alertme("Please Enter Tax Value");
                    txt_tax_percent.Focus();
                    return;
                }



                try
                {
                    if (check_for_duplicate_tax(txt_tax_percent.Text, ddl_tax_type.Text))
                    {
                        Alertme("Already added.");
                        return;
                    }
                    else
                    {
                        if (ViewState["Is_add"].ToString() == "1")
                        {
                            submit_details();
                            empty_form();
                        }
                        else
                        {
                            Alertme(My.get_restricted_message());
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            catch (Exception ex)
            {
                Alertme("Please try again..");
                My.submitexception(ex.ToString());
            }

        }
        private bool check_for_duplicate_tax(string tax_percent, string tax_type)
        {
            DataTable dt = My.dataTable("select * from Tax_Master where  Tax_Percent='" + tax_percent + "' and Tax_Type='" + tax_type + "' and firm='" + My.firm_id() + "'");
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void submit_details()
        {
            string tax_name = "" + ddl_tax_type.Text + " @ " + txt_tax_percent.Text + " %";
            string tax_percent = txt_tax_percent.Text;
            string tax_type = ddl_tax_type.Text;
            string insert_qry = "insert into Tax_Master (Tax_Name,Tax_Percent,Tax_Type,firm) values ('" + tax_name + "','" + txt_tax_percent.Text + "','" + ddl_tax_type.Text + "','" + My.firm_id ()+ "'); select * from Tax_Master where firm='" + My.firm_id() + "'; ";

            DataTable dt = My.dataTable(insert_qry);
            Alertme("GST Created Successfully");
            My.send_data_to_user_log_history(Session["name"].ToString() + " Add new GST details : " + tax_name, Session["Admin"].ToString());

            bind_grd_view();
        }
        private void bind_grd_view()
        {
            DataTable dt = My.dataTable("select * from Tax_Master where firm='" + My.firm_id() + "' ;");
            if (dt.Rows.Count > 0)
            {
                GrdView_Add_GST.DataSource = dt;
                GrdView_Add_GST.DataBind();
            }
            else
            {
                GrdView_Add_GST.DataSource = dt;
                GrdView_Add_GST.DataBind();
            }
        }

        private void empty_form()
        {
            Btn_Add.Visible = true;
            Btn_Update.Visible = false;
            My.ClearInputs(Page.Controls);


        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.NamingContainer;
                    string id = ((Label)row.FindControl("lbl_Id")).Text;
                    string Tax_Percent = ((Label)row.FindControl("lbl_Tax_Percent")).Text;
                    string tax_type = ((Label)row.FindControl("lbl_Tax_Type")).Text;

                    ViewState["id"] = id;
                    txt_tax_percent.Text = Tax_Percent;
                    ddl_tax_type.Text = tax_type;

                    Btn_Add.Visible = false;
                    Btn_Update.Visible = true;
                }
                else
                {
                    Alertme(My.get_restricted_message());
                }

            }
            catch (Exception ex)
            {
                Alertme("Please try again..");
                My.submitexception(ex.ToString());
            }

        }
        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            if (ddl_tax_type.Text == "")
            {
                Alertme("Please Select Tax Type");
                ddl_tax_type.Focus();
                return;
            }
            if (txt_tax_percent.Text == "")
            {
                Alertme("Please Enter Tax Value");
                txt_tax_percent.Focus();
                return;
            }
            try
            {
                update_update_details();
                empty_form();
            }
            catch (Exception ex)
            {
                Alertme(ex.Message);
            }

        }
        private void update_update_details()
        {
            if (check_for_duplicate(txt_tax_percent.Text, ddl_tax_type.Text))
            {
                Alertme("Already added.");
                return;
            }
            else
            {
                string tax_name = "" + ddl_tax_type.Text + " @ " + txt_tax_percent.Text + " %";
                string tax_percent = txt_tax_percent.Text;
                string tax_type = ddl_tax_type.Text;
                string update_qry = "update Tax_Master set Tax_Name='" + tax_name + "',Tax_Percent='" + tax_percent + "',Tax_Type='" + tax_type + "' where id='" + ViewState["id"].ToString() + "'; select * from Tax_Master where firm='" + My.firm_id() + "';";

                DataTable dt = My.dataTable(update_qry);
                Alertme("Tax Master Updated Successfully");
                My.send_data_to_user_log_history(Session["name"].ToString() + " Update GST details : " + tax_name, Session["Admin"].ToString());

                bind_grd_view();
            }
        }

        private bool check_for_duplicate(string tax_percent, string tax_type)
        {
            DataTable dt = My.dataTable("select * from Tax_Master where   id != '" + ViewState["id"].ToString() + "' and Tax_Percent='" + tax_percent + "' and Tax_Type='" + tax_type + "' and firm='" + My.firm_id() + "'");
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        GrdView_Add_GST.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message());
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
    }
}