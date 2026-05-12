using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Stock_Transfer : System.Web.UI.Page
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
                        txt_Date.Text = mycode.date();
                        bind_store();
                        //fill_gridview();
                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }

        private void bind_store()
        {
            mycode.bind_all_ddl_with_id(ddl_store, " select Store_name,Store_id from dbo.[HMS_Invetory_Create_Store] where Is_default=0");
        }

        private void fill_gridview()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Inventory_stock_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch1");
            cmd.Parameters.AddWithValue("@central_storeid ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@Demand_no ", ddl_demand_no.SelectedValue);
            cmd.Parameters.AddWithValue("@Store_id ", ddl_store.SelectedValue);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            ViewState["item_dt"] = dt;
            int rowcount = ds.Tables[0].Rows.Count;
            grd_view.DataSource = dt;
            grd_view.DataBind();
            btn_submit.Enabled = true;
        }

        #region update_data

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_store.SelectedValue == "0")
                {
                    Alertme("Please select store.", "warning");
                    return;

                }
                if (ddl_demand_no.SelectedValue == "0")
                {
                    Alertme("Please select demand no.", "warning");
                    return;

                }
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    transfer_stock();
                    fill_gridview();
                    ts.Complete();
                    btn_print.Visible = true;
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
        private void transfer_stock()
        {
            if (ViewState["unique_entry_id"] == null)
            {
                ViewState["unique_entry_id"] = My.unique_id();
            }

            double total_Cost_rate = 0;
            double total_rate = 0;
            double Total_gst = 0;
            double Final_total = 0;

            for (int i = 0; i < grd_view.Rows.Count; i++)
            {
                string item_code = ((Label)grd_view.Rows[i].FindControl("lbl_Item_Code")).Text;
                string Brand_Id = ((Label)grd_view.Rows[i].FindControl("lbl_Brand_Id")).Text;
                string Unit_id = ((Label)grd_view.Rows[i].FindControl("lbl_Unit_id")).Text;
                string HSN = ((Label)grd_view.Rows[i].FindControl("lbl_Hsn_no")).Text;
                string avl_qty = ((Label)grd_view.Rows[i].FindControl("lbl_qty")).Text;
                string Accept_qty = ((Label)grd_view.Rows[i].FindControl("lbl_Accept_qty")).Text;

                string Batch_no = "0";
                string Expiry_date = "0";

                string Cost_rate = "0";
                string gst_per = "0";
                string stock_id = "0";
                string Id = "0";
                string quantity = "0";
                string inv_row_id = "";
                if (Convert.ToDouble(avl_qty) > 0 && Convert.ToDouble(avl_qty) >= Convert.ToDouble(Accept_qty))
                {
                    string qry = " select * from dbo.[HMS_Inventory_stock_details] where Item_Code='" + item_code + "' and Brand_Id='" + Brand_Id + "' and Unit_id='" + Unit_id + "' and Store_id='2001' and Quantity>0 and Format(convert(DateTime,Expiry_date,103), 'yyyyMMdd')>=" + mycode.idate() + " ";
                    DataTable dt = My.dataTable(qry);
                    foreach (DataRow dr in dt.Rows)
                    {
                        inv_row_id = dr["Id"].ToString();
                        Batch_no = dr["Batch_no"].ToString();
                        Expiry_date = dr["Expiry_date"].ToString();

                        Cost_rate = dr["Purchase_Rate"].ToString();
                        gst_per = dr["GST_Percent"].ToString();
                        stock_id = dr["Stock_id"].ToString();
                        Id = dr["id"].ToString();

                        if (Convert.ToDouble(Accept_qty) >= Convert.ToDouble(dr["Quantity"].ToString()))
                        {
                            Accept_qty = (Convert.ToDouble(Accept_qty) - Convert.ToDouble(dr["Quantity"].ToString())).ToString();
                            quantity = dr["Quantity"].ToString();
                        }
                        else
                        {
                            //Accept_qty = (Convert.ToDouble(dr["Quantity"].ToString()) - Convert.ToDouble(Accept_qty)).ToString();
                            quantity = Accept_qty;
                            Accept_qty = "0";
                        }


                        string Total_amount = (Convert.ToDouble(Cost_rate) * Convert.ToDouble(quantity)).ToString();
                        string Gts_amount = (((Convert.ToDouble(Cost_rate) * Convert.ToDouble(quantity)) * Convert.ToDouble(gst_per)) / 100).ToString();
                        string Final_amount = (Convert.ToDouble(Total_amount) + Convert.ToDouble(Gts_amount)).ToString();
                        total_Cost_rate += Convert.ToDouble(Cost_rate);


                        total_rate += Convert.ToDouble(Cost_rate);
                        Total_gst += Convert.ToDouble(Gts_amount);
                        Final_total += Convert.ToDouble(Final_amount);

                        SqlCommand cmd = new SqlCommand("sp_HMS_Inventory_stock_details");
                        cmd.Parameters.AddWithValue("@sp_status ", "TRANSFER");
                        cmd.Parameters.AddWithValue("@Store_id ", ddl_store.SelectedValue);
                        cmd.Parameters.AddWithValue("@Stock_id ", stock_id);
                        cmd.Parameters.AddWithValue("@Item_Code ", item_code);
                        cmd.Parameters.AddWithValue("@Brand_Id ", Brand_Id);
                        cmd.Parameters.AddWithValue("@Unit_id ", Unit_id);
                        cmd.Parameters.AddWithValue("@hsn_no", HSN);
                        cmd.Parameters.AddWithValue("@Quantity ", quantity);
                        cmd.Parameters.AddWithValue("@Purchase_Rate ", Cost_rate);
                        cmd.Parameters.AddWithValue("@GST_Percent ", gst_per);
                        cmd.Parameters.AddWithValue("@date ", mycode.date());
                        cmd.Parameters.AddWithValue("@idate ", mycode.idate());
                        cmd.Parameters.AddWithValue("@session ", My.get_session());
                        cmd.Parameters.AddWithValue("@firm ", My.firm_id());
                        cmd.Parameters.AddWithValue("@Userid ", Session["Admin"].ToString());
                        cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
                        cmd.Parameters.AddWithValue("@Transfer_from_store ", hd_cart_id.Value);
                        cmd.Parameters.AddWithValue("@Demand_id ", ddl_demand_no.SelectedValue);
                        cmd.Parameters.AddWithValue("@Batch_no", Batch_no);
                        cmd.Parameters.AddWithValue("@Expiry_date", Expiry_date);
                        cmd.Parameters.AddWithValue("@id ", Id);
                        int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                        if (rowsAffected > 0)
                        {
                            Alertme("Stock has been transfered successfully", "success");
                        }
                    }
                   
                }

            }
            save_final_total_billwise(total_rate, Total_gst, Final_total);
            fill_gridview();
        }
        private void save_final_total_billwise(double total_rate, double Total_gst, double Final_total)
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERTTotal");
            cmd.Parameters.AddWithValue("@Invoice_no ", ddl_demand_no.SelectedValue);
            cmd.Parameters.AddWithValue("@Date ", mycode.date());
            cmd.Parameters.AddWithValue("@Transfer_from ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@Transfer_to ", ddl_store.SelectedValue);
            cmd.Parameters.AddWithValue("@Total_amount ", total_rate.ToString());
            cmd.Parameters.AddWithValue("@Gst_amount", Total_gst.ToString());
            cmd.Parameters.AddWithValue("@Final_amount ", Final_total.ToString());
            cmd.Parameters.AddWithValue("@Created_by ", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@Created_date ", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@Receivername ", txt_receivername.Text);
            cmd.Parameters.AddWithValue("@Remarks ", txt_remarks.Text);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
            }
        }


        private void save_stock_transfer_item_wise(string inv_row_id, string stock_id, string item_code, string brand_Id, string unit_id, string hSN, string quantity, string cost_rate, string gst_per, string batch_no, string expiry_date)
        {
            string Total_amount = (Convert.ToDouble(cost_rate) * Convert.ToDouble(quantity)).ToString();
            string Gts_amount = (((Convert.ToDouble(cost_rate) * Convert.ToDouble(quantity)) * Convert.ToDouble(gst_per)) / 100).ToString();
            string Final_amount = (Convert.ToDouble(Total_amount) + Convert.ToDouble(Gts_amount)).ToString();

            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
            cmd.Parameters.AddWithValue("@invoice_no ", ddl_demand_no.SelectedValue);
            cmd.Parameters.AddWithValue("@Store_id ", ddl_store.SelectedValue);
            cmd.Parameters.AddWithValue("@Stock_id ", stock_id);
            cmd.Parameters.AddWithValue("@Item_Code ", item_code);
            cmd.Parameters.AddWithValue("@Brand_Id ", brand_Id);
            cmd.Parameters.AddWithValue("@Unit_id ", unit_id);
            cmd.Parameters.AddWithValue("@hsn_no", hSN);
            cmd.Parameters.AddWithValue("@Quantity ", quantity);
            cmd.Parameters.AddWithValue("@Purchase_Rate ", cost_rate);
            cmd.Parameters.AddWithValue("@GST_Percent ", gst_per);
            cmd.Parameters.AddWithValue("@date ", mycode.date());
            cmd.Parameters.AddWithValue("@idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Userid ", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
            cmd.Parameters.AddWithValue("@Transfer_from_store ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@Batch_no", batch_no);
            cmd.Parameters.AddWithValue("@Expiry_date", expiry_date);
            cmd.Parameters.AddWithValue("@Status", "PENDING");
            cmd.Parameters.AddWithValue("@Inv_row_id", inv_row_id);
            cmd.Parameters.AddWithValue("@Total_amount", Total_amount);
            cmd.Parameters.AddWithValue("@Gts_amount", Gts_amount);
            cmd.Parameters.AddWithValue("@Final_amount", Final_amount);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {

            }

        }


        #endregion update_data

        protected void ddl_store_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_demand_no();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void bind_demand_no()
        {
            string qry = " select distinct Demand_no,Demand_no from dbo.[HMS_Geherate_Demand] where Store_id='" + ddl_store.SelectedValue + "' and Is_stock_stranfered is null";
            mycode.bind_all_ddl_with_id_All(ddl_demand_no, qry);
        }

        protected void ddl_demand_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fill_gridview();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        protected void grd_view_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string Accept_qty = ((Label)e.Row.FindControl("lbl_Accept_qty")).Text;
                    string avl_qty = ((Label)e.Row.FindControl("lbl_qty")).Text;
                    if (avl_qty == "0")
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                    }
                    else if (Convert.ToDouble(avl_qty) < Convert.ToDouble(Accept_qty))
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            string Path = "Slip/Print_item_stransfer_slip.aspx?unique_entry_id=" + ViewState["unique_entry_id"].ToString() + "&Demand_id=" + ddl_demand_no.SelectedValue;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + Path + "','_newtab');", true);

        }


    }

}
