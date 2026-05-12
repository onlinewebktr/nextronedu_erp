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
    public partial class purchase_history : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    try
                    {
                        txt_from_Date.Text = mycode.date();
                        txt_to_Date.Text = mycode.date();
                        bind_datewise();
                        bind_suppier();
                        Bind_Item();
                        bind_datewise();
                        // bind_all_data();
                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }
        My my = new My();
        private void bind_suppier()
        {
            string qry = "SELECT party_name,party_id FROM party_details where type='Supplier';";
            my.bind_all_ddl_with_id_All(ddl_supplier, qry);

        }
        private void Bind_Item()
        {
            mycode.bind_all_ddl_with_id(ddl_Item, "select  t1.Item_Name,t1.Item_id from HMS_Invetory_item_Master t1  order by Item_Name asc");
            //mycode.bind_all_ddl_with_id(ddl_Item, "select DISTINCT t1.Item_Name,t1.Item_id from HMS_Invetory_item_Master t1 join HMS_Inventory_stock_details t2 on t1.Item_id=t2.Item_Code where t2.Store_id='" + hd_cart_id.Value + "' and Format(convert(DateTime,Expiry_date,103), 'yyyyMMdd')>=" + mycode.idate() + " and Quantity>0 order by Item_Name asc");
        }
        private void bind_all_data()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_billwise");
            cmd.Parameters.AddWithValue("@sp_status", "FetchData");
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
                Alertme("Sorry! No Stock entry available.", "Warning");
            GrdView.DataSource = dt;
            GrdView.DataBind();
        }



        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                bind_datewise();

            }
            catch (Exception ex)
            {
            }
        }

        private void bind_datewise()
        {

            if (txt_from_Date.Text == "")
            {
                Alertme("Please enter from date.", "warning");
                txt_from_Date.Focus();
            }
            else if (txt_to_Date.Text == "")
            {
                Alertme("Please enter to date.", "warning");
                txt_to_Date.Focus();
            }
            else
            {
                int Sdate = My.DateConvertToIdate(txt_from_Date.Text);
                int Edate = My.DateConvertToIdate(txt_to_Date.Text);
                SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_billwise");
                if (ddl_supplier.SelectedValue == "0")
                    cmd.Parameters.AddWithValue("@sp_status", "FetchDataByDate");
                else
                    cmd.Parameters.AddWithValue("@sp_status", "FetchDataBysupplier");
                cmd.Parameters.AddWithValue("@fromdate", Sdate);
                cmd.Parameters.AddWithValue("@todate", Edate);
                cmd.Parameters.AddWithValue("@firm ", My.firm_id());
                cmd.Parameters.AddWithValue("@party_id ", ddl_supplier.SelectedValue);

                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                    Alertme("Sorry! No Stock entry available.", "Warning");
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        protected void lnk_view_items_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_party_name = (Label)row.FindControl("lbl_party_name");
                Label lbl_partyid = (Label)row.FindControl("lbl_partyid");
                Label lbl_invoice_no = (Label)row.FindControl("lbl_invoice_no");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_firm = (Label)row.FindControl("lbl_firm");
                Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");


                SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_billwise");
                cmd.Parameters.AddWithValue("@sp_status", "FetchItems");
                cmd.Parameters.AddWithValue("@invoice_no", lbl_invoice_no.Text);
                cmd.Parameters.AddWithValue("@firm ", My.firm_id());
                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                    Alertme("Sorry! No Stock entry available.", "Warning");


                lblparty_name.Text = lbl_party_name.Text;
                lblinvoice_no.Text = lbl_invoice_no.Text;


                string Path = "Slip/Print_purchase_history.aspx?PO_no=" + lbl_invoice_no.Text + "&session=" + lbl_session.Text + "&firm=" + lbl_firm.Text + "&partyid=" + lbl_partyid.Text + "&unique_entry_id=" + lbl_unique_entry_id.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + Path + "','_newtab');", true);


                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }
        protected void lnk_view_item_details_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
             
            Label lbl_party_name = (Label)row.FindControl("lbl_party_name");
            Label lbl_partyid = (Label)row.FindControl("lbl_partyid");
            Label lbl_invoice_no = (Label)row.FindControl("lbl_invoice_no");
            Label lbl_session = (Label)row.FindControl("lbl_session");
            Label lbl_firm = (Label)row.FindControl("lbl_firm");
            Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");
            lblparty_name.Text = lbl_party_name.Text;
            lblinvoice_no.Text = lbl_invoice_no.Text;

            Label lbl_Total_netamount = (Label)row.FindControl("lbl_Total_netamount");
            lbl_total_value.Text = lbl_Total_netamount.Text;
            find_item_details(lbl_invoice_no, lbl_session, GrdView_Generate_PO, lbl_unique_entry_id, lbl_partyid);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


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
                    GrdView.RenderControl(hw);
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
                GrdView.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        #endregion download_in_excelpnl_grid

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Reference the Repeater Item.
                    RepeaterItem item = e.Item;

                    //Reference the Controls.

                    //Label lbl_session = (Label)e.Item.FindControl("lbl_session");
                    //Label lbl_invoice_no = (Label)e.Item.FindControl("lbl_invoice_no");
                    //Label lbl_unique_entry_id = (Label)e.Item.FindControl("lbl_unique_entry_id");
                    //Label lbl_partyid = (Label)e.Item.FindControl("lbl_partyid");

                    //Repeater rp = (Repeater)e.Item.FindControl("GrdView_Generate_PO");
                    //find_item_details(lbl_invoice_no, lbl_session, rp, lbl_unique_entry_id, lbl_partyid);
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void find_item_details(Label lbl_invoice_no, Label lbl_session, Repeater rp, Label lbl_unique_entry_id, Label lbl_partyid)
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_billwise");
            cmd.Parameters.AddWithValue("@sp_status", "FetchItems");
            cmd.Parameters.AddWithValue("@invoice_no", lbl_invoice_no.Text);
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@session ", lbl_session.Text);
            cmd.Parameters.AddWithValue("@unique_entry_id ", lbl_unique_entry_id.Text);
            cmd.Parameters.AddWithValue("@party_id ", lbl_partyid.Text);


            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            rp.DataSource = dt;
            rp.DataBind();
        }

        protected void btn_itemwise_Click(object sender, EventArgs e)
        {
            try
            {
                fetch_data_itemwise();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }


        }

        private void fetch_data_itemwise()
        {
            //if (txt_from_Date.Text == "")
            //{
            //    Alertme("Please enter from date.", "warning");
            //    txt_from_Date.Focus();
            //}
            //else if (txt_to_Date.Text == "")
            //{
            //    Alertme("Please enter to date.", "warning");
            //    txt_to_Date.Focus();
            //}
            if (ddl_Item.SelectedValue == "0")
            {
                Alertme("Please select item name", "warning");
                ddl_Item.Focus();
            }
            else
            {
                //int Sdate = My.DateConvertToIdate(txt_from_Date.Text);
                //int Edate = My.DateConvertToIdate(txt_to_Date.Text);
                SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_billwise");
                cmd.Parameters.AddWithValue("@sp_status", "FetchDataByItem");
                //if (ddl_supplier.SelectedValue == "0")
                //    cmd.Parameters.AddWithValue("@sp_status", "FetchDataByDate");
                //else
                //    cmd.Parameters.AddWithValue("@sp_status", "FetchDataBysupplier");
                //cmd.Parameters.AddWithValue("@fromdate", Sdate);
                //cmd.Parameters.AddWithValue("@todate", Edate);
                //cmd.Parameters.AddWithValue("@firm ", My.firm_id());
                cmd.Parameters.AddWithValue("@Item_Code ", ddl_Item.SelectedValue);

                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                    Alertme("Sorry! No Stock entry available.", "Warning");
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
    }
}