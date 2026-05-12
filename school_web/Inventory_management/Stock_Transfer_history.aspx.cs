using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Stock_Transfer_history : System.Web.UI.Page
    {
        My mycode = new My();
        string scrpt;
        private void Alertme(string msg, string panel)
        {

            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "tmp", scrpt, false);
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
                ViewState["Userid"] = (object)this.Session["Admin"].ToString();
                if (this.IsPostBack)
                    return;
                try
                {
                    txt_from_Date.Text = mycode.date();
                    txt_to_Date.Text = mycode.date();
                    bind_all_data();
                }
                catch (Exception ex)
                {
                    My.submitexception(ex.ToString());
                }
            }

        }
        private void bind_all_data()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Inventory_stock_details");
            cmd.Parameters.AddWithValue("@sp_status", (object)"TRANSFERH");
            cmd.Parameters.AddWithValue("@fromdate", (object)My.convertidate(this.txt_from_Date.Text));
            cmd.Parameters.AddWithValue("@todate", (object)My.convertidate(this.txt_to_Date.Text));
            DataSet dataSet = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dataTable = dataSet.Tables[0];
            if (dataSet.Tables[0].Rows.Count == 0)
                Alertme("Sorry! No Stock entry available.", "Warning");
            RPDetails.DataSource = (object)dataTable;
            RPDetails.DataBind();
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                bind_all_data();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
        protected void lnk_view_Click(object sender, EventArgs e)
        {
            try
            {
                RepeaterItem repeaterItem = (RepeaterItem)((Control)sender).NamingContainer;
                string text1 = ((Label)repeaterItem.FindControl("lbl_unique_entry_id")).Text;
                string text2 = ((Label)repeaterItem.FindControl("lbl_Sector")).Text;
                string text3 = ((Label)repeaterItem.FindControl("lbl_date")).Text;
                string text4 = ((Label)repeaterItem.FindControl("lbl_Demand_id")).Text;
                lbl_storename.Text = text2;
                lbl_demand_no.Text = text4;
                lbl_demand_date.Text = text3;
                SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
                cmd.Parameters.AddWithValue("@sp_status ", (object)"Fetch4");
                cmd.Parameters.AddWithValue("@unique_entry_id ", (object)text1);
                DataSet dataSet = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dataTable = dataSet.Tables[0];
                ViewState["item_dt"] = (object)dataTable;
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    hd_view.Value = "1";
                    grd_view.DataSource = (object)dataTable;
                    grd_view.DataBind();
                }
                else
                {
                    grd_view.DataSource = (object)null;
                    grd_view.DataBind();
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
        protected void lnk_print_Click(object sender, EventArgs e)
        {
            try
            {
                RepeaterItem repeaterItem = (RepeaterItem)((Control)sender).NamingContainer;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + ("Slip/Print_item_stransfer_slip.aspx?unique_entry_id=" + ((Label)repeaterItem.FindControl("lbl_unique_entry_id")).Text + "&Demand_id=" + ((Label)repeaterItem.FindControl("lbl_Demand_id")).Text) + "','_newtab');", true);
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        protected void lbk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                RepeaterItem repeaterItem = (RepeaterItem)((Control)sender).NamingContainer;
                Response.Redirect("stock-transfer-to-store.aspx?unique_entry_id=" + ((Label)repeaterItem.FindControl("lbl_unique_entry_id")).Text + "&Demand_id=" + ((Label)repeaterItem.FindControl("lbl_Demand_id")).Text);
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }


        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                    return;
                RepeaterItem repeaterItem = e.Item;
                string text = (repeaterItem.FindControl("lbl_Demand_id") as Label).Text;
                string unique_entry_id = ((Label)repeaterItem.FindControl("lbl_unique_entry_id")).Text;
                GridView gr = (GridView)repeaterItem.FindControl("grd_view");

                LinkButton linkButton = (LinkButton)repeaterItem.FindControl("lbk_edit");
                if (text.Contains("DIR"))
                    linkButton.Visible = true;
                else
                    linkButton.Visible = false;

                bind_items(gr, unique_entry_id);
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_items(GridView gr, string unique_entry_id)
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch4");
            cmd.Parameters.AddWithValue("@unique_entry_id ", unique_entry_id);
            DataSet dataSet = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dataTable = dataSet.Tables[0];
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                gr.DataSource = (object)dataTable;
                gr.DataBind();
            }
            else
            {
                gr.DataSource = (object)null;
                gr.DataBind();
            }
        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    pnl_grid.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        protected void lnk_excel_download1_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                pnl_grid.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        #endregion download_in_excel
    }
}