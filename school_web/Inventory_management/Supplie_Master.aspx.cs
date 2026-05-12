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

namespace school_web.Inventory_management
{
    public partial class Supplie_Master : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null )
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('logout.aspx','_parent',replace=true);</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();
                    string pagename_current = Path.GetFileName(Request.Path);
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];
                    bind_grd_view();
                    try
                    {
                        if (Session["msg"] == null)
                        {

                        }
                        else
                        {
                            Alertme(Session["msg"].ToString(), "success");
                            Session["msg"] = null;
                        }
                    }
                    catch
                    {

                    }

                }
            }
        }







        private void bind_grd_view()
        {
            DataTable dt = My.dataTable(" select  * from dbo.[party_details] where type='Supplier';");
            if (dt.Rows.Count > 0)
            {
                GrdView_Add_Details.DataSource = dt;
                GrdView_Add_Details.DataBind();
            }
            else
            {
                GrdView_Add_Details.DataSource = dt;
                GrdView_Add_Details.DataBind();
            }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;

                string party_id = ((Label)row.FindControl("lbl_party_id")).Text;

                if (ViewState["Is_Edit"].ToString() == "1")
                {

                    Response.Redirect("Create_Supplier.aspx?partyid=" + party_id, false);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                Alertme("Please try again..", "warning");
                My.submitexception(ex.ToString());
            }

        }



        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                string id = ((Label)row.FindControl("lbl_Id")).Text;
                string party_id = ((Label)row.FindControl("lbl_party_id")).Text;
                string party_name = ((Label)row.FindControl("lbl_party_name")).Text;
                if (Session["is_delete"].ToString() == "False")
                {

                    Alertme("You have no permission to delete.", "warning");
                }
                else
                {
                    if (ViewState["Is_delete"].ToString() == "1")
                    {
                        if (My.toDouble(My.get_table_data("select sum(count) as count from( select count(*)count  from dbo.[Account_Voucher_Details] where Account_id='" + party_id + "') t")) > 0)
                        {
                            Alertme("You can not delete this item, because this item is in use.", "warning");
                        }
                        else
                        {
                            My.exeSql("delete from party_details where id='" + id + "'; delete from Account_Ledger_Details where Account_id='" + party_id + "';");
                            Alertme("Party deleted successfully.", "warning");
                            My.send_data_to_user_log_history(Session["name"].ToString() + " delete details of Party with Party id : " + party_id + " Party name : " + party_name, Session["Admin"].ToString());

                            bind_grd_view();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Alertme(ex.Message, "warning");
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
                        GrdView_Add_Details.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

    }
}