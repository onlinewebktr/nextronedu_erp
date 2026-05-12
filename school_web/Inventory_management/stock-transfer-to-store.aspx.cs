using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class stock_transfer_to_store : System.Web.UI.Page
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
                        Bind_Item();
                        bind_brand();
                        bind_GST();
                        Bind_Unit();
                        string unique_entry_id = Request.QueryString["unique_entry_id"];
                        string Demand_id = Request.QueryString["Demand_id"];

                        if (!string.IsNullOrEmpty(unique_entry_id))
                        {
                            ViewState["ISMODIFY"] = "1";
                            ViewState["unique_entry_id"] = unique_entry_id;
                            ViewState["Demand_id"] = Demand_id;
                            txt_invoice_no.Text = Demand_id;
                            find_previous_details();
                            btn_final_submit.Text = "Final Save Modified Bill";
                        }
                        else
                        {
                            ViewState["ISMODIFY"] = "0";
                        }

                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }

        private void find_previous_details()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch4");
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];

            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                txt_Date.Text = ds.Tables[0].Rows[0]["date"].ToString();
                ddl_store.SelectedValue = ds.Tables[0].Rows[0]["Store_id"].ToString();

                fetch_items();


            }

        }

        private void Bind_Item()
        {
            mycode.bind_all_ddl_with_id(ddl_Item, "select DISTINCT t1.Item_Name,t1.Item_id from HMS_Invetory_item_Master t1 join HMS_Inventory_stock_details t2 on t1.Item_id=t2.Item_Code where t2.Store_id='" + hd_cart_id.Value + "' order by Item_Name asc");
            //mycode.bind_all_ddl_with_id(ddl_Item, "select DISTINCT t1.Item_Name,t1.Item_id from HMS_Invetory_item_Master t1 join HMS_Inventory_stock_details t2 on t1.Item_id=t2.Item_Code where t2.Store_id='" + hd_cart_id.Value + "' and Format(convert(DateTime,Expiry_date,103), 'yyyyMMdd')>=" + mycode.idate() + " and Quantity>0 order by Item_Name asc");
        }
        private void bind_store()
        {
            mycode.bind_all_ddl_with_id(ddl_store, " select Store_name,Store_id from dbo.[HMS_Invetory_Create_Store] where Is_default=0");
        }

        private void bind_brand()
        {
            mycode.bind_all_ddl_with_id(ddl_brand, "Select Brand_name,Brand_id from HMS_Invetory_Brand_Master");
        }

        private void bind_GST()
        {
            SqlDataAdapter ad = new SqlDataAdapter("Select Tax_Percent,Tax_Percent from Tax_Master where firm ='" + My.firm_id() + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "tests");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl_gst.DataTextField = "Select";
                ddl_gst.DataValueField = "0";
            }
            else
            {
                ddl_gst.DataTextField = ds.Tables[0].Columns[0].ToString();
                ddl_gst.DataValueField = ds.Tables[0].Columns[1].ToString();
            }
            ddl_gst.DataSource = ds.Tables[0];
            ddl_gst.DataBind();
        }

        private void Bind_Unit()
        {
            mycode.bind_all_ddl_with_id(ddl_unit, "select Unit,unit_id from dbo.[unit_master] where firm ='" + My.firm_id() + "'");
        }


        protected void txt_invoice_no_TextChanged(object sender, EventArgs e)
        {
            try
            {
                check_duplicate_invoice_no();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }
        }
        private bool check_duplicate_invoice_no()
        {
            if (ddl_store.SelectedItem.Text == "Select")
            {
                txt_invoice_no.Text = "";
                Alertme("Please select store.", "warning");
                return false;
            }
            else
            {
                DataTable dt = mycode.FillData("select invoice_no,Status from HMS_Temp_inventory_stock_transfer_details where session='" + My.get_session() + "' and firm='" + My.firm_id() + "' and Store_id='" + ddl_store.SelectedValue + "' and invoice_no ='" + txt_invoice_no.Text + "'");
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    if (dt.Rows[0]["Status"].ToString() == "PENDING")
                    {
                        fetch_items();
                    }
                    else
                    {
                        txt_invoice_no.Text = "";
                        Alertme("Please enter different invoice no.", "warning");
                    }

                    return true;
                }

            }
        }

        protected void ddl_Item_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { find_item_details(); }
            catch (Exception ex)
            { My.submitexception(ex.ToString()); }
        }
        private void find_item_details()
        {
            DataTable dt = My.dataTable("select t2.* from HMS_Invetory_item_Master t1 join HMS_Inventory_stock_details t2 on t1.Item_id=t2.Item_Code where t2.Store_id='" + hd_cart_id.Value + "' and t1.Item_id='" + ddl_Item.SelectedValue + "' and Quantity>0 order by t2.Id asc");
            if (dt.Rows.Count == 0)
            {
                txt_gst_value.Text = "0";
                txt_net_amount.Text = "0";
                ddl_gst.SelectedValue = "0";
                txt_total_rate.Text = "0";
                txt_purchse_rate.Text = "0";
                ddl_unit.SelectedValue = "0";
                ddl_brand.SelectedValue = "0";
                txt_qty.Text = "NO STOCK";
                lbl_hsn.Text = "";
                hd_stock_id.Value = "0";
                txt_aval_quantity.Text = "";
            }
            else
            {
                DataRow dr = dt.Rows[0];
                lbl_hsn.Text = dr["hsn_no"].ToString();
                ddl_gst.SelectedValue = dr["GST_Percent"].ToString();
                hd_itemcode.Value = dr["Item_Code"].ToString(); ;
                ddl_unit.SelectedValue = dr["Unit_id"].ToString();
                ddl_brand.SelectedValue = dr["Brand_Id"].ToString();

                txt_batch_no.Text = dr["Batch_no"].ToString();
                txt_expiry_date.Text = dr["Expiry_date"].ToString();
                txt_purchse_rate.Text = dr["Purchase_Rate"].ToString();
                hd_stock_id.Value = dr["Stock_id"].ToString();
                txt_aval_quantity.Text = dr["Quantity"].ToString();
                hd_inv_row_id.Value = dr["Id"].ToString();
            }
        }

        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_store.SelectedItem.Text == "Select")
                {
                    Alertme("Please select store.", "warning");
                    ddl_store.Focus();
                }
                else if (ddl_Item.SelectedItem.Text == "Select")
                {
                    Alertme("Please select item.", "warning");
                    ddl_Item.Focus();
                }
                else if (My.toDouble(txt_qty.Text) <= 0)
                {
                    Alertme("Please enter quantity.", "warning");
                    txt_qty.Focus();
                }

                else if (My.toDouble(txt_aval_quantity.Text) < My.toDouble(txt_qty.Text))
                {
                    Alertme("Available quantity will not greater than transfer quantity.", "warning");
                    txt_qty.Focus();
                }
                else
                {
                    save_stock_transfer();
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void save_stock_transfer()
        {
            if (txt_invoice_no.Text == "")
            {
                txt_invoice_no.Text = "DIR-" + My.session_wise_auto_serial_New("Inventory_transfer_no", My.get_session(), My.firm_id());
            }
            if (ViewState["unique_entry_id"] == null)
            {
                ViewState["unique_entry_id"] = My.unique_id();
            }
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
            cmd.Parameters.AddWithValue("@invoice_no ", txt_invoice_no.Text);
            cmd.Parameters.AddWithValue("@Store_id ", ddl_store.SelectedValue);
            cmd.Parameters.AddWithValue("@Stock_id ", hd_stock_id.Value);
            cmd.Parameters.AddWithValue("@Item_Code ", ddl_Item.SelectedValue);
            cmd.Parameters.AddWithValue("@Brand_Id ", ddl_brand.SelectedValue);
            cmd.Parameters.AddWithValue("@Unit_id ", ddl_unit.SelectedValue);
            cmd.Parameters.AddWithValue("@hsn_no", lbl_hsn.Text);
            cmd.Parameters.AddWithValue("@Quantity ", txt_qty.Text);
            cmd.Parameters.AddWithValue("@Purchase_Rate ", txt_purchse_rate.Text);
            cmd.Parameters.AddWithValue("@GST_Percent ", ddl_gst.SelectedValue);
            cmd.Parameters.AddWithValue("@date ", mycode.date());
            cmd.Parameters.AddWithValue("@idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Userid ", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
            cmd.Parameters.AddWithValue("@Transfer_from_store ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@Batch_no", txt_batch_no.Text);
            cmd.Parameters.AddWithValue("@Expiry_date", txt_expiry_date.Text);
            if (ViewState["ISMODIFY"].ToString() == "1")
            {
                cmd.Parameters.AddWithValue("@Status ", "APPROVED");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Status ", "PENDING");
            }
            cmd.Parameters.AddWithValue("@Inv_row_id", hd_inv_row_id.Value);
            cmd.Parameters.AddWithValue("@Total_amount", txt_total_rate.Text);
            cmd.Parameters.AddWithValue("@Gts_amount", txt_gst_value.Text);
            cmd.Parameters.AddWithValue("@Final_amount", txt_net_amount.Text);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
                empty_feild();
                Alertme("Item has been added successfully", "success");
                fetch_items();
            }
        }

        private void fetch_items()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch1");

            if (ViewState["ISMODIFY"].ToString() == "1")
            {

                cmd.Parameters.AddWithValue("@Status ", "APPROVED");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Status ", "PENDING");
            }
            cmd.Parameters.AddWithValue("@Transfer_from_store ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Store_id ", ddl_store.SelectedValue);
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];

            if (ViewState["item_dt"] == null)
            {
                ViewState["item_dt"] = dt;
            }

            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                txt_invoice_no.Text = dt.Rows[0]["invoice_no"].ToString();
                grd_view.DataSource = dt;
                grd_view.DataBind();
                btn_final_submit.Visible = true;
                fetch_total_amt();
            }
            else
            {
                txt_invoice_no.Text = "";
                grd_view.DataSource = null;
                grd_view.DataBind();
                btn_final_submit.Visible = true;
            }
        }



        private void empty_feild()
        {
            Btn_Add.Visible = true;
            Btn_Update.Visible = false;
            ddl_Item.SelectedValue = "0";
            txt_net_amount.Text = "0";
            txt_total_rate.Text = "0";
            txt_purchse_rate.Text = "0";
            txt_qty.Text = "0";
            lbl_hsn.Text = "";
            hd_stock_id.Value = "0";
            find_item_details();
        }

        private void fetch_total_amt()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "FetchTotal");
            cmd.Parameters.AddWithValue("@invoice_no ", txt_invoice_no.Text);
            cmd.Parameters.AddWithValue("@Status ", "PENDING");
            cmd.Parameters.AddWithValue("@Transfer_from_store ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Store_id ", ddl_store.SelectedValue);
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];

            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                lbl_total_rate.Text = "Total Rate : " + dt.Rows[0]["Total_Rate"].ToString();
                lbl_total_gst.Text = "Total GST Value : " + dt.Rows[0]["Total_gst"].ToString();
                lbl_final_toatal.Text = "Net  Total : " + dt.Rows[0]["Final_total"].ToString();

                ViewState["Total_Rate"] = dt.Rows[0]["Total_Rate"].ToString();
                ViewState["Total_gst"] = dt.Rows[0]["Total_gst"].ToString();
                ViewState["Final_total"] = dt.Rows[0]["Final_total"].ToString();
            }
            else
            {
                lbl_total_rate.Text = "Total Rate : 00";
                lbl_total_gst.Text = "Total GST Value : 00";
                lbl_final_toatal.Text = "Net  Total : 00";
            }
        }
        #region edit_and_update
        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Label lbl_Item_Code = (Label)row.FindControl("lbl_Item_Code");
            Label lbl_qty = (Label)row.FindControl("lbl_qty");
            ddl_Item.SelectedValue = lbl_Item_Code.Text;
            txt_qty.Text = lbl_qty.Text;
            find_item_details();

            HdID.Value = lbl_Id.Text;
            SqlCommand cmd;
            string query = "Select * from  HMS_Temp_inventory_stock_transfer_details where Id = @id ";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@id", lbl_Id.Text);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //ddl_Item.SelectedValue = dr["Item_Code"].ToString();

                    //ddl_Item.SelectedValue = dr["Item_Code"].ToString();
                    //ddl_brand.SelectedValue = dr["Brand_Id"].ToString();
                    //ddl_unit.SelectedValue = dr["Unit_id"].ToString();
                    //lbl_hsn.Text = dr["hsn_no"].ToString();
                    //txt_qty.Text = dr["Quantity"].ToString();
                    //txt_purchse_rate.Text = dr["Purchase_Rate"].ToString();
                    //ddl_gst.SelectedValue = dr["GST_Percent"].ToString();

                    //txt_batch_no.Text = dr["Batch_no"].ToString();
                    //txt_expiry_date.Text = dr["Expiry_date"].ToString();


                    txt_total_rate.Text = dr["Total_amount"].ToString();
                    txt_gst_value.Text = dr["Gts_amount"].ToString();
                    txt_net_amount.Text = dr["Final_amount"].ToString();


                    Btn_Cancel.Visible = true;
                    Btn_Add.Visible = false;
                    Btn_Update.Visible = true;

                }
            }
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            empty_feild();
        }
        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_store.SelectedItem.Text == "Select")
                {
                    Alertme("Please select store.", "warning");
                    ddl_store.Focus();
                }
                else if (ddl_Item.SelectedItem.Text == "Select")
                {
                    Alertme("Please select item.", "warning");
                    ddl_Item.Focus();
                }
                else if (My.toDouble(txt_qty.Text) <= 0)
                {
                    Alertme("Please enter quantity.", "warning");
                    txt_qty.Focus();
                }

                else if (My.toDouble(txt_aval_quantity.Text) < My.toDouble(txt_qty.Text))
                {
                    Alertme("Available quantity will not greater than transfer quantity.", "warning");
                    txt_qty.Focus();
                }
                else
                {
                    Update_stock_transfer();
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }


        private void Update_stock_transfer()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "UPDATE");
            cmd.Parameters.AddWithValue("@Id ", HdID.Value);
            cmd.Parameters.AddWithValue("@invoice_no ", txt_invoice_no.Text);
            cmd.Parameters.AddWithValue("@Store_id ", ddl_store.SelectedValue);
            cmd.Parameters.AddWithValue("@Stock_id ", hd_stock_id.Value);
            cmd.Parameters.AddWithValue("@Item_Code ", ddl_Item.SelectedValue);
            cmd.Parameters.AddWithValue("@Brand_Id ", ddl_brand.SelectedValue);
            cmd.Parameters.AddWithValue("@Unit_id ", ddl_unit.SelectedValue);
            cmd.Parameters.AddWithValue("@hsn_no", lbl_hsn.Text);
            cmd.Parameters.AddWithValue("@Quantity ", txt_qty.Text);
            cmd.Parameters.AddWithValue("@Purchase_Rate ", txt_purchse_rate.Text);
            cmd.Parameters.AddWithValue("@GST_Percent ", ddl_gst.SelectedValue);
            cmd.Parameters.AddWithValue("@date ", mycode.date());
            cmd.Parameters.AddWithValue("@idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Userid ", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@Transfer_from_store ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@Batch_no", txt_batch_no.Text);
            cmd.Parameters.AddWithValue("@Expiry_date", txt_expiry_date.Text);
            if (ViewState["ISMODIFY"].ToString() == "1")
            {
                cmd.Parameters.AddWithValue("@Status ", "APPROVED");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Status ", "PENDING");
            }
            cmd.Parameters.AddWithValue("@Inv_row_id", hd_inv_row_id.Value);
            cmd.Parameters.AddWithValue("@Total_amount", txt_total_rate.Text);
            cmd.Parameters.AddWithValue("@Gts_amount", txt_gst_value.Text);
            cmd.Parameters.AddWithValue("@Final_amount", txt_net_amount.Text);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
                empty_feild();
                Alertme("Item has been updated successfully", "success");
                fetch_items();
            }
        }
        #endregion edit_update
        protected void ddl_store_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { fetch_items(); }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Inv_row_id = (Label)row.FindControl("lbl_Inv_row_id");
                Label lbl_qty = (Label)row.FindControl("lbl_qty");
                Label lbl_Stock_id = (Label)row.FindControl("lbl_Stock_id");
                Label lbl_Store_id = (Label)row.FindControl("lbl_Store_id");
                Label lbl_Item_Code = (Label)row.FindControl("lbl_Item_Code");

                if (ViewState["ISMODIFY"].ToString() == "1")
                {
                    string qry1 = "Update HMS_Inventory_stock_details set Quantity=Quantity+" + lbl_qty.Text + " where id=" + lbl_Inv_row_id.Text + "; ";
                    qry1 = qry1 + "delete from HMS_temp_Inventory_stock_details   where Stock_id =" + lbl_Stock_id.Text + " and Store_id='" + lbl_Store_id.Text + "' and Item_Code='" + lbl_Item_Code.Text + "'; ";
                    qry1 = qry1 + "delete from HMS_Inventory_stock_details  where Stock_id =" + lbl_Stock_id.Text + " and Store_id='" + lbl_Store_id.Text + "'  and Item_Code='" + lbl_Item_Code.Text + "'; ";
                    qry1 = qry1 + "delete from HMS_Temp_inventory_stock_transfer_details  where Id =" + lbl_Id.Text + "; ";

                    My.exeSql(qry1);

                    Alertme("Item has been delete Successfully.", "success");
                    fetch_items();
                }
                else
                {
                    string qry1 = "Update HMS_Inventory_stock_details set Quantity=Quantity+" + lbl_qty.Text + " where id=" + lbl_Inv_row_id.Text + "; ";
                    qry1 = qry1 + "delete from HMS_Temp_inventory_stock_transfer_details  where Id =" + lbl_Id.Text + "; ";
                    My.exeSql(qry1);

                    Alertme("Item has been delete Successfully.", "success");
                    fetch_items();
                }


            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                try
                {
                    string qry = " select * from HMS_Temp_inventory_stock_transfer_details where invoice_no='" + txt_invoice_no.Text + "'  and Store_id='" + ddl_store.SelectedValue + "' and firm='" + My.firm_id() + "' and session='" + My.get_session() + "'";
                    DataTable dt = My.dataTable(qry);
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (ViewState["ISMODIFY"].ToString() == "1")
                        {
                            reset_previous_data(dr["Id"].ToString(), dr["Quantity"].ToString());
                        }
                        SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
                        cmd.Parameters.AddWithValue("@sp_status ", "TRANSFER");
                        cmd.Parameters.AddWithValue("@Store_id ", dr["Store_id"].ToString());
                        cmd.Parameters.AddWithValue("@Stock_id ", dr["Stock_id"].ToString());
                        cmd.Parameters.AddWithValue("@Item_Code ", dr["Item_Code"].ToString());
                        cmd.Parameters.AddWithValue("@Brand_Id ", dr["Brand_Id"].ToString());
                        cmd.Parameters.AddWithValue("@Unit_id ", dr["Unit_id"].ToString());
                        cmd.Parameters.AddWithValue("@hsn_no", dr["hsn_no"].ToString());
                        cmd.Parameters.AddWithValue("@Quantity ", dr["Quantity"].ToString());
                        cmd.Parameters.AddWithValue("@Purchase_Rate ", dr["Purchase_Rate"].ToString());
                        cmd.Parameters.AddWithValue("@GST_Percent ", dr["GST_Percent"].ToString());
                        cmd.Parameters.AddWithValue("@date ", mycode.date());
                        cmd.Parameters.AddWithValue("@idate ", mycode.idate());
                        cmd.Parameters.AddWithValue("@session ", dr["session"].ToString());
                        cmd.Parameters.AddWithValue("@firm ", dr["firm"].ToString());
                        cmd.Parameters.AddWithValue("@Userid ", Session["Admin"].ToString());
                        cmd.Parameters.AddWithValue("@unique_entry_id ", dr["unique_entry_id"].ToString());
                        cmd.Parameters.AddWithValue("@Transfer_from_store ", dr["Transfer_from_store"].ToString());
                        cmd.Parameters.AddWithValue("@Demand_id ", txt_invoice_no.Text);
                        cmd.Parameters.AddWithValue("@Batch_no", dr["Batch_no"].ToString());
                        cmd.Parameters.AddWithValue("@Expiry_date", dr["Expiry_date"].ToString());
                        cmd.Parameters.AddWithValue("@inv_row_id", dr["Inv_row_id"].ToString());
                        cmd.Parameters.AddWithValue("@Id", dr["Id"].ToString());
                        int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                        if (rowsAffected > 0)
                        {
                        }
                    }
                    save_final_total_billwise();
                    Alertme("Stock has been transferred successfully. Please Print transfer slip.", "success");
                    fetch_items();
                    ViewState["ISMODIFY"] = "";
                    txt_invoice_no.Text = "";
                    lbl_total_rate.Text = "";
                    lbl_total_gst.Text = "";
                    lbl_final_toatal.Text = "";
                    btn_final_submit.Visible = false;
                    btn_print.Visible = true;
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    Alertme("Please try again...", "warning");
                    scope.Complete();
                    My.submitexception(ex.ToString());
                }
            }

        }
        private void save_final_total_billwise()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERTTotal");
            cmd.Parameters.AddWithValue("@Invoice_no ", txt_invoice_no.Text);
            cmd.Parameters.AddWithValue("@Date ", mycode.date());
            cmd.Parameters.AddWithValue("@Transfer_from ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@Transfer_to ", ddl_store.SelectedValue);
            cmd.Parameters.AddWithValue("@Total_amount ", ViewState["Total_Rate"].ToString());
            cmd.Parameters.AddWithValue("@Gst_amount", ViewState["Total_gst"].ToString());
            cmd.Parameters.AddWithValue("@Final_amount ", ViewState["Final_total"].ToString());
            cmd.Parameters.AddWithValue("@Created_by ", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@Created_date ", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@Receivername ", txt_receivername.Text);
            cmd.Parameters.AddWithValue("@Remarks ", txt_remarks.Text);
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
            }
        }
        private void reset_previous_data(string Id, string current_Quantity)
        {
            DataTable dt = (DataTable)ViewState["item_dt"];
            if (dt.Rows.Count > 0)
            { }
            DataRow[] dr = dt.Select("Id='" + Id + "'");
            {
                if (dr.Length != 0)
                {
                    DataTable dt1 = dr.CopyToDataTable();
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        int Pre_Quantity = Convert.ToInt32(dr1["Quantity"].ToString());
                        string row_id = dr1["Inv_row_id"].ToString();
                        string qry1 = "Update HMS_Inventory_stock_details set Quantity=Quantity+" + Pre_Quantity + " where id=" + row_id + "; ";
                        My.exeSql(qry1);
                    }
                }
            }
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            string Path = "Slip/Print_item_stransfer_slip.aspx?unique_entry_id=" + ViewState["unique_entry_id"].ToString() + "&Demand_id=" + txt_invoice_no.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + Path + "','_newtab');", true);

        }
    }
}