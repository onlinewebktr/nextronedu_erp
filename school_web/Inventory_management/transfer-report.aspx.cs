using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class transfer_report : System.Web.UI.Page
    {
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
                ViewState["Userid"] = Session["Admin"].ToString();
                if (!IsPostBack)
                {
                    try
                    {
                        bind_store();
                        find_all_data();

                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }

        private void bind_report()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch2");
            cmd.Parameters.AddWithValue("@Status ", "APPROVED");
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Store_id ", ddl_store.SelectedValue);
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            ViewState["item_dt"] = dt;
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                grd_view.DataSource = dt;
                grd_view.DataBind();
            }
            else
            {
                grd_view.DataSource = null;
                grd_view.DataBind();
            }
        }

        private void bind_store()
        {
            mycode.bind_all_ddl_with_id(ddl_store, " select Store_name,Store_id from dbo.[HMS_Invetory_Create_Store] where Is_default=0");
        }

        protected void Btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_store.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose store.", "Warning");
                }
                else
                {
                    bind_report();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_all_data()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch3");
            cmd.Parameters.AddWithValue("@Status ", "APPROVED");
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            ViewState["item_dt"] = dt;
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                grd_view.DataSource = dt;
                grd_view.DataBind();
            }
            else
            {
                grd_view.DataSource = null;
                grd_view.DataBind();
            }
        }
    }
}