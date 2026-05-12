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
    public partial class Purchase_entry : System.Web.UI.Page
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
                        txt_Date.Text = mycode.date();

                        bind_GST();
                        bind_brand();
                        Bind_Unit();

                        string PO_no = Request.QueryString["PO_no"];
                        string party_id = Request.QueryString["party_id"];
                        if (!string.IsNullOrEmpty(PO_no))
                        {
                            txt_invoice_no.Text = PO_no;
                            hd_party_id.Value = party_id;
                            find_supplier_detail_byid();
                            find_pending_entry();
                        }

                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }

        private void find_supplier_detail_byid()
        {

            string qry = @"select *  from party_details where party_id ='" + hd_party_id.Value + "'";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txt_suppliername.Text = dr["party_name"].ToString();
                    hd_state_code.Value = dt.Rows[0]["State_Code"].ToString();
                    hd_state_code.Value = dt.Rows[0]["State_Code"].ToString();
                }
            }

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




        #region page_event
        protected void txt_suppliername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                find_supplier_details();
                // find_pending_entry();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void find_pending_entry()
        {
            string qry = "Select top 1 * from dbo.[HMS_inventory_purchase_entry_itemwise] where party_id='" + hd_party_id.Value + "' and Status='Pending'";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                txt_invoice_no.Text = dt.Rows[0]["invoice_no"].ToString();
                ViewState["unique_entry_id"] = dt.Rows[0]["unique_entry_id"].ToString();
                txt_Date.Text = dt.Rows[0]["Purchase_date"].ToString();

                fill_gridview();
            }
        }

        private void find_supplier_details()
        {
            string qry = @"select *  from party_details where party_name+', Mob.-'+mobile ='" + txt_suppliername.Text + "' and type='Supplier'";


            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txt_suppliername.Text = dr["party_name"].ToString();
                    hd_party_id.Value = dt.Rows[0]["party_id"].ToString();
                    lbl_supplier_details.Text = "Address:- " + dr["address"].ToString() + ", " + dr["State"].ToString() + "</br>Mobile:-" + dr["mobile"].ToString();
                    hd_state_code.Value = dt.Rows[0]["State_Code"].ToString();
                }
            }
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
        protected void txt_Item_TextChanged(object sender, EventArgs e)
        {


            try { find_item_details(); }
            catch (Exception ex)
            { My.submitexception(ex.ToString()); }
        }
        private void find_item_details()
        {
            DataTable dt = My.dataTable(" select * from dbo.[HMS_Invetory_item_Master] where Item_Name='" + txt_Item.Text + "'   ");
            if (dt.Rows.Count == 0)
            {
                lbl_gst_value.Text = "0";
                lbl_net_amount.Text = "0";
                ddl_gst.SelectedValue = "0";
                lbl_total_rate.Text = "0";
                txt_purchse_rate.Text = "0";
                ddl_unit.SelectedValue = "0";
                ddl_brand.SelectedValue = "0";
                txt_qty.Text = "0";
                lbl_hsn.Text = "";


            }
            else
            {
                DataRow dr = dt.Rows[0];
                bind_brand();
                lbl_hsn.Text = dr["HSN"].ToString();
                ddl_gst.SelectedValue = dr["GST"].ToString();
                hd_itemcode.Value = dr["Item_id"].ToString(); ;
                ddl_unit.SelectedValue = dr["Unit_id"].ToString();
                ddl_brand.SelectedValue = dr["Brand_id"].ToString();
            }
        }
        protected void txt_qty_TextChanged(object sender, EventArgs e)
        {
            try { calculate_rate(); txt_purchse_rate.Focus(); }
            catch (Exception ex)
            {

            }

        }
        public void calculate_rate()
        {

            if (txt_qty.Text == "")
            {
                return;
            }
            double rate = My.toDouble(txt_purchse_rate.Text);
            double qty = My.toDouble(txt_qty.Text);
            double dis_per = My.toDouble(txt_disc_pers.Text);
            double tax_percent = My.toDouble(ddl_gst.SelectedValue);
            double total = rate * qty;
            lbl_total_rate.Text = total.ToString("0.00");
            double discount = 0;
            if (dis_per > 0)
            {
                discount = My.Round(total * dis_per / 100);
            }
            double discounted_total = total - discount;
            //if (ddl_text_type.SelectedIndex == 0)
            //{

            double taxable = total - discount;

            double total_tax = My.Round(taxable * tax_percent / 100);
            double total_cess = 0;
            double tcs_per = 0;
            double net = taxable + total_tax + total_cess;
            double total_tcs = My.Round((net * tcs_per / 100), 2);
            lbl_taxable_amount.Text = taxable.ToString("0.00");
            lbl_gst_value.Text = total_tax.ToString("0.00");
            lbl_net_amount.Text = net.ToString("0.00");

            //}
            //else
            //{
            //    double discounted_total = total - discount;
            //    // double taxable = total - discount;
            //    double taxable = 0;
            //    double total_cess = 0;
            //    taxable = discounted_total - (discounted_total * (tax_percent) / (100 + tax_percent));
            //    double total_tax = My.Round(taxable * tax_percent / 100);
            //    double tcs_per = 0;
            //    double net = taxable + total_tax + total_cess;
            //    double total_tcs = My.Round((net * tcs_per / 100), 2);
            //    lbl_gst_value.Text = total_tax.ToString("0.00");
            //    lbl_net_amount.Text = net.ToString("0.00");
            //}

        }

        protected void txt_purchse_rate_TextChanged(object sender, EventArgs e)
        {
            try { calculate_rate(); txt_sale_rate.Focus(); }
            catch (Exception ex)
            {

            }
        }
        protected void ddl_gst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { calculate_rate(); }
            catch (Exception ex)
            {

            }
        }
        #endregion page_event

        #region save_data
        private void calculate_total()
        {
            double total_qty = 0;
            double total = 0, net_total = 0;
            double taxable = 0;
            double totalgst = 0;
            double totaldiscount = 0;
            double totaldiscountper = 0;
            DataTable item_dt = (DataTable)ViewState["item_dt"];
            foreach (DataRow dr in item_dt.Rows)
            {
                total_qty += My.toDouble(dr["Quantity"]);
                total += My.toDouble(dr["Total"]);
                taxable += My.toDouble(dr["taxable"]);
                net_total += My.toDouble(dr["Net_total"]);
                totalgst += My.toDouble(dr["Gst_value"]);
                totaldiscount += My.toDouble(dr["discount_amount"]);
                totaldiscountper += My.toDouble(dr["discount_perc"]);
            }
            lbl_totl_items.Text = item_dt.Rows.Count.ToString();
            lbl_total_.Text = total.ToString("0.00");

            lbl_total_taxable.Text = taxable.ToString("0.00");
            lbl_total_gst_value.Text = totalgst.ToString("0.00");

            lbl_total_discount.Text = totaldiscount.ToString("0.00");
            hd_totaldis_per.Value = totaldiscountper.ToString("0.00");

            double expense = Convert.ToDouble(txt_freight.Text);
            net_total = net_total + expense;

            //double sub_total = net_total - flat_disc + tcs;
            double sub_total = net_total;
            lbl_net_total.Text = net_total.ToString();
            lbl_round_off.Text = (My.Round(sub_total, 0) - My.Round(sub_total, 2)).ToString("0.00");

            lbl_grand_total.Text = My.Round(net_total, 0).ToString("0.00");
            lbl_total_qty.Text = total_qty.ToString();

        }
        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_suppliername.Text == "")
                {
                    Alertme("Please Enter Stock Input From", "warning");
                    txt_suppliername.Focus();
                    return;
                }
                if (txt_invoice_no.Text == "")
                {
                    Alertme("Please Enter invoice_no. If You don't know invoice number then please enter serial number in invoice number", "warning");
                    txt_invoice_no.Focus();
                    return;
                }
                if (hd_itemcode.Value == "0")
                {
                    Alertme("Please select item name", "warning");
                    return;
                }
                if (ddl_brand.SelectedValue == "0")
                {
                    Alertme("Please select brand name", "warning");
                    return;
                }
                if (ddl_unit.SelectedValue == "0")
                {
                    Alertme("Please select unit name", "warning");
                    return;
                }
                if (txt_qty.Text == "" || Convert.ToDouble(txt_qty.Text) == 0)
                {
                    Alertme("Please Enter quantity", "warning");
                    txt_qty.Focus();
                    return;
                }
                if (txt_purchse_rate.Text == "")
                {
                    Alertme("Please enter purchse rate", "warning");
                    txt_purchse_rate.Focus();
                    return;
                }
                if (check_duplicate_invoice_no())
                {
                    Alertme("Duplicate Invoice No For this 'Stock Input From'.", "warning");
                    txt_invoice_no.Focus();
                    return;
                }
                save_data();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
        private bool check_duplicate_invoice_no()
        {
            if (txt_suppliername.Text.ToLower() == "cash")
            {
                return false;
            }
            else
            {
                if (My.dataTable("select invoice_no from HMS_inventory_purchase_entry_billwise where session='" + My.get_session() + "' and firm='" + My.firm_id() + "' and party_id='" + hd_party_id.Value + "' and invoice_no ='" + txt_invoice_no.Text + "' ").Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }
        private void save_data()
        {

            if (ViewState["unique_entry_id"] == null)
            {
                ViewState["unique_entry_id"] = My.unique_id();
            }
            SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_itemwise");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
            cmd.Parameters.AddWithValue("@Store_id ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@invoice_no ", txt_invoice_no.Text);
            cmd.Parameters.AddWithValue("@party_id ", hd_party_id.Value);
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Item_Code ", hd_itemcode.Value);
            cmd.Parameters.AddWithValue("@Brand_Id ", ddl_brand.SelectedValue);
            cmd.Parameters.AddWithValue("@Hsn_no ", lbl_hsn.Text);
            cmd.Parameters.AddWithValue("@Quantity ", txt_qty.Text);
            cmd.Parameters.AddWithValue("@Unit_id ", ddl_unit.SelectedValue);
            cmd.Parameters.AddWithValue("@Purchase_rate ", txt_purchse_rate.Text);
            cmd.Parameters.AddWithValue("@Total ", lbl_total_rate.Text);
            cmd.Parameters.AddWithValue("@Gst_percent ", ddl_gst.SelectedValue);
            cmd.Parameters.AddWithValue("@Gst_value ", lbl_gst_value.Text);
            cmd.Parameters.AddWithValue("@Net_total ", lbl_net_amount.Text);
            cmd.Parameters.AddWithValue("@IGST ", 0);
            cmd.Parameters.AddWithValue("@CGST ", My.toDouble(lbl_gst_value.Text) / 2);
            cmd.Parameters.AddWithValue("@SGST ", My.toDouble(lbl_gst_value.Text) / 2);
            cmd.Parameters.AddWithValue("@date ", mycode.date());
            cmd.Parameters.AddWithValue("@time ", mycode.time());
            cmd.Parameters.AddWithValue("@idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
            cmd.Parameters.AddWithValue("@user_id ", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Status ", "Pending");
            cmd.Parameters.AddWithValue("@Purchase_date", txt_Date.Text);
            cmd.Parameters.AddWithValue("@Batch_no", txt_batch_no.Text);
            cmd.Parameters.AddWithValue("@Expiry_date", txt_expiry_date.Text);
            cmd.Parameters.AddWithValue("@discount_perc ", txt_disc_pers.Text);
            cmd.Parameters.AddWithValue("@discount_amount ", My.Round(Convert.ToDouble(lbl_total_rate.Text) * Convert.ToDouble(txt_disc_pers.Text) / 100));
            cmd.Parameters.AddWithValue("@taxable", lbl_taxable_amount.Text);
            cmd.Parameters.AddWithValue("@Sale_rate", txt_sale_rate.Text);

            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
                Alertme("Data added successfully", "success");
                My.send_data_to_user_log_history(ViewState["Userid"].ToString() + " add item " + txt_Item.Text + " with Item code " + hd_itemcode.Value + " in Invoice No : " + txt_invoice_no.Text, ViewState["Userid"].ToString());



                fill_gridview();
                reset_data();
            }
        }
        private void reset_data()
        {
            Btn_Add.Visible = true;
            Btn_Update.Visible = false;
            //My.ClearInputs(Page.Controls);
            lbl_gst_value.Text = "";
            lbl_net_amount.Text = "";

            lbl_total_rate.Text = "";
            txt_purchse_rate.Text = "0";
            ddl_unit.SelectedValue = "0";
            ddl_brand.SelectedValue = "0";
            txt_qty.Text = "";
            lbl_hsn.Text = "";
            txt_disc_pers.Text = "0";
            txt_batch_no.Text = "";
            txt_expiry_date.Text = "";
            txt_Item.Text = "";
            lbl_taxable_amount.Text = "";
            txt_sale_rate.Text = "0";

        }
        private void fill_gridview()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_itemwise");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch");
            cmd.Parameters.AddWithValue("@Status ", "Pending");
            cmd.Parameters.AddWithValue("@invoice_no ", txt_invoice_no.Text);
            cmd.Parameters.AddWithValue("@party_id ", hd_party_id.Value);
            cmd.Parameters.AddWithValue("@Store_id ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            ViewState["item_dt"] = dt;
            int rowcount = ds.Tables[0].Rows.Count;

            if (dt.Rows.Count == 0)
            {
                grd_view.DataSource = null;
                grd_view.DataBind();
                finaltotal.Visible = false;
            }
            else
            {
                grd_view.DataSource = dt;
                grd_view.DataBind();
                finaltotal.Visible = true;


            }

            calculate_total();
        }

        #endregion save_data

        #region update_data
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                DataTable item_dt = (DataTable)ViewState["item_dt"];
                DataRow[] dr = item_dt.Select("Id=" + lbl_Id.Text);
                HdID.Value = lbl_Id.Text;
                foreach (DataRow Record in dr)
                {
                    txt_Item.Text = Record["Item_Name"].ToString();
                    hd_itemcode.Value = Record["Item_Code"].ToString();
                    ddl_brand.SelectedValue = Record["Brand_Id"].ToString();
                    ddl_unit.SelectedValue = Record["Unit_id"].ToString();
                    txt_qty.Text = Record["Quantity"].ToString();
                    lbl_hsn.Text = Record["Hsn_no"].ToString();
                    txt_purchse_rate.Text = Record["Purchase_rate"].ToString();
                    lbl_total_rate.Text = Record["Total"].ToString();
                    ddl_gst.SelectedValue = Record["Gst_percent"].ToString();
                    lbl_gst_value.Text = Record["Gst_value"].ToString();
                    lbl_net_amount.Text = Record["Net_total"].ToString();
                    txt_batch_no.Text = Record["Batch_no"].ToString();
                    txt_expiry_date.Text = Record["Expiry_date"].ToString();
                    txt_disc_pers.Text = Record["discount_perc"].ToString();
                    txt_sale_rate.Text = Record["Sale_rate"].ToString();


                    calculate_rate();
                }
                Btn_Cancel.Visible = true;
                Btn_Add.Visible = false;
                Btn_Update.Visible = true;

                return;


            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }

        }

        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_suppliername.Text == "")
                {
                    Alertme("Please Enter Stock Input From", "warning");
                    txt_suppliername.Focus();
                    return;
                }
                if (txt_invoice_no.Text == "")
                {
                    Alertme("Please Enter invoice_no. If You don't know invoice number then please enter serial number in invoice number", "warning");
                    txt_invoice_no.Focus();
                    return;
                }
                if (hd_itemcode.Value == "0")
                {
                    Alertme("Please select item name", "warning");
                    return;
                }
                if (ddl_brand.SelectedValue == "0")
                {
                    Alertme("Please select brand name", "warning");
                    return;
                }
                if (ddl_unit.SelectedValue == "0")
                {
                    Alertme("Please select unit name", "warning");
                    return;
                }
                if (txt_qty.Text == "")
                {
                    Alertme("Please Enter quantity", "warning");
                    txt_qty.Focus();
                    return;
                }
                if (txt_purchse_rate.Text == "")
                {
                    Alertme("Please enter purchse rate", "warning");
                    txt_purchse_rate.Focus();
                    return;
                }
                if (check_duplicate_invoice_no())
                {
                    Alertme("Duplicate Invoice No For this 'Stock Input From'.", "warning");
                    txt_invoice_no.Focus();
                    return;
                }
                Update_Data();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }

        }
        private void Update_Data()
        {

            if (ViewState["unique_entry_id"] == null)
            {
                ViewState["unique_entry_id"] = My.unique_id();
            }
            SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_itemwise");
            cmd.Parameters.AddWithValue("@sp_status ", "UPDATE");
            cmd.Parameters.AddWithValue("@Id ", HdID.Value);
            cmd.Parameters.AddWithValue("@Store_id ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@invoice_no ", txt_invoice_no.Text);
            cmd.Parameters.AddWithValue("@party_id ", hd_party_id.Value);
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Item_Code ", hd_itemcode.Value);
            cmd.Parameters.AddWithValue("@Brand_Id ", ddl_brand.SelectedValue);
            cmd.Parameters.AddWithValue("@Hsn_no ", lbl_hsn.Text);
            cmd.Parameters.AddWithValue("@Quantity ", txt_qty.Text);
            cmd.Parameters.AddWithValue("@Unit_id ", ddl_unit.SelectedValue);
            cmd.Parameters.AddWithValue("@Purchase_rate ", txt_purchse_rate.Text);
            cmd.Parameters.AddWithValue("@Total ", lbl_total_rate.Text);
            cmd.Parameters.AddWithValue("@Gst_percent ", ddl_gst.SelectedValue);
            cmd.Parameters.AddWithValue("@Gst_value ", lbl_gst_value.Text);
            cmd.Parameters.AddWithValue("@Net_total ", lbl_net_amount.Text);
            cmd.Parameters.AddWithValue("@IGST ", 0);
            cmd.Parameters.AddWithValue("@CGST ", My.toDouble(lbl_gst_value.Text) / 2);
            cmd.Parameters.AddWithValue("@SGST ", My.toDouble(lbl_gst_value.Text) / 2);
            cmd.Parameters.AddWithValue("@date ", mycode.date());
            cmd.Parameters.AddWithValue("@time ", mycode.time());
            cmd.Parameters.AddWithValue("@idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
            cmd.Parameters.AddWithValue("@user_id ", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Status ", "Pending");
            cmd.Parameters.AddWithValue("@Purchase_date", txt_Date.Text);
            cmd.Parameters.AddWithValue("@Batch_no", txt_batch_no.Text);
            cmd.Parameters.AddWithValue("@Expiry_date", txt_expiry_date.Text);
            cmd.Parameters.AddWithValue("@discount_perc ", txt_disc_pers.Text);
            cmd.Parameters.AddWithValue("@discount_amount ", My.Round(Convert.ToDouble(lbl_total_rate.Text) * Convert.ToDouble(txt_disc_pers.Text) / 100));
            cmd.Parameters.AddWithValue("@taxable", lbl_taxable_amount.Text);
            cmd.Parameters.AddWithValue("@Sale_rate", txt_sale_rate.Text);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
                Alertme("Data updated successfully", "success");
                My.send_data_to_user_log_history(ViewState["Userid"].ToString() + " Update item " + txt_Item.Text + " with Item code " + hd_itemcode.Value + " in Invoice No : " + txt_invoice_no.Text, ViewState["Userid"].ToString());

                reset_data();
                fill_gridview();
            }
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            reset_data();
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;

                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                delete_data(lbl_Id.Text);
                return;
            }
            catch (Exception ex)
            {
                Alertme("You can't delete this Purchasing Order.", "warning");
                My.submitexception(ex.ToString());

            }
        }
        private void delete_data(string id)
        {
            SqlCommand cmd;
            string query = "delete from  HMS_inventory_purchase_entry_itemwise where Id = @id ";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@id", id);
            if (My.InsertUpdateData(cmd))
            {
                Alertme("item has been delete Successfully.", "success");
                fill_gridview();
            }


        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                //using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                //{
                update_stock();
                reset_data();

                // ts.Complete();
                //}
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void update_stock()
        {
            double total_Cost_rate = 0;
            string stock_id = My.global_id_creation("stock_id");
            for (int i = 0; i < grd_view.Rows.Count; i++)
            {
                string item_code = ((Label)grd_view.Rows[i].FindControl("lbl_Item_Code")).Text;
                string Batch_no = ((Label)grd_view.Rows[i].FindControl("lbl_Batch_no")).Text;
                string Expiry_date = ((Label)grd_view.Rows[i].FindControl("lbl_Expiry_date")).Text;
                string Brand_Id = ((Label)grd_view.Rows[i].FindControl("lbl_Brand_Id")).Text;
                string Unit_id = ((Label)grd_view.Rows[i].FindControl("lbl_Unit_id")).Text;
                string HSN = ((Label)grd_view.Rows[i].FindControl("lbl_Hsn_no")).Text;
                string quantity = ((Label)grd_view.Rows[i].FindControl("lbl_qty")).Text;
                string Cost_rate = ((Label)grd_view.Rows[i].FindControl("lbl_rate")).Text;
                string gst_per = ((Label)grd_view.Rows[i].FindControl("lbl_gst_per")).Text;
                string Sale_rate = ((Label)grd_view.Rows[i].FindControl("lbl_Sale_rate")).Text;

                string Id = ((Label)grd_view.Rows[i].FindControl("lbl_Id")).Text;

                total_Cost_rate += Convert.ToDouble(Cost_rate);

                string stock_item_unique_entry_id = hd_cart_id.Value + "-" + stock_id.ToString() + "-" + item_code + "-" + mycode.itime();
                SqlCommand cmd = new SqlCommand("sp_HMS_Inventory_stock_details");
                cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
                cmd.Parameters.AddWithValue("@Store_id ", hd_cart_id.Value);
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
                cmd.Parameters.AddWithValue("@Userid ", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
                cmd.Parameters.AddWithValue("@Batch_no", Batch_no);
                cmd.Parameters.AddWithValue("@Expiry_date", Expiry_date);
                cmd.Parameters.AddWithValue("@Sale_rate", Sale_rate);



                cmd.Parameters.AddWithValue("@stock_item_unique_entry_id", stock_item_unique_entry_id);//

                int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                if (rowsAffected > 0)
                {
                    //stock ledger insert
                    try
                    {
                        Sale_Purchase.HMS_Item_Account_Ledger_udpate("1", item_code, Unit_id, txt_Date.Text, ViewState["unique_entry_id"].ToString(), stock_item_unique_entry_id, quantity, "0", "Purchase Entry");
                    }
                    catch
                    {

                    }


                    Alertme("Purchase Entry Submitted Successfully", "success");
                    //string Path = "Slip/Print_purchase_history.aspx?PO_no=" + txt_invoice_no.Text + "&session=" + My.get_session() + "&firm=" + My.firm_id();
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + Path + "','_newtab');", true);




                }


            }


            update_accont_ledger(txt_invoice_no.Text, lbl_grand_total.Text, lbl_round_off.Text, txt_Date.Text, txt_suppliername.Text);
            send_to_product_bill_wise(total_Cost_rate);

            string Path = "Slip/Print_purchase_history.aspx?PO_no=" + txt_invoice_no.Text + "&session=" + My.get_session() + "&firm=" + My.firm_id() + "&partyid=" + hd_party_id.Value + "&unique_entry_id=" + ViewState["unique_entry_id"].ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + Path + "','_newtab');", true);

            fill_gridview();
        }
        private void send_to_product_bill_wise(double total_sell_price)
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_billwise");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
            cmd.Parameters.AddWithValue("@Store_id ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@invoice_no ", txt_invoice_no.Text);
            cmd.Parameters.AddWithValue("@party_id ", hd_party_id.Value);
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Total_items ", lbl_totl_items.Text);
            cmd.Parameters.AddWithValue("@Total_qty ", lbl_total_qty.Text);
            cmd.Parameters.AddWithValue("@Total_Purchase_rate ", lbl_total_.Text);
            cmd.Parameters.AddWithValue("@Total_Gst_value ", lbl_total_gst_value.Text);
            cmd.Parameters.AddWithValue("@Total_IGST ", 0);
            cmd.Parameters.AddWithValue("@Total_CGST ", Convert.ToDouble(lbl_total_gst_value.Text) / 2);
            cmd.Parameters.AddWithValue("@Total_SGST ", Convert.ToDouble(lbl_total_gst_value.Text) / 2);
            cmd.Parameters.AddWithValue("@Grand_total ", lbl_grand_total.Text);
            cmd.Parameters.AddWithValue("@date ", mycode.date());
            cmd.Parameters.AddWithValue("@time ", mycode.time());
            cmd.Parameters.AddWithValue("@idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@user_id ", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
            cmd.Parameters.AddWithValue("@Purchase_date ", txt_Date.Text);
            cmd.Parameters.AddWithValue("@txt_freight ", txt_freight.Text);
            cmd.Parameters.AddWithValue("@Remarks ", txt_remarks.Text);

            cmd.Parameters.AddWithValue("@roundoff ", lbl_round_off.Text);
            cmd.Parameters.AddWithValue("@Total_netamount ", lbl_net_total.Text);
            cmd.Parameters.AddWithValue("@Total_taxable ", lbl_total_taxable.Text);
            cmd.Parameters.AddWithValue("@discount_amount", lbl_total_discount.Text);
            cmd.Parameters.AddWithValue("@discount_per ", hd_totaldis_per.Value);


            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {

            }

        }
        private void update_accont_ledger(string bill_no, string grand_total, string round_off, string purchase_date, string purchase_from)
        {
            #region send to account ledger
            {


                // find the all account ledger and group
                My.fetch_all_account();

                SqlDataAdapter ad = new SqlDataAdapter("select * from Account_Voucher_Details where unique_entry_id = '" + ViewState["unique_entry_id"].ToString() + "' ", My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Account_Voucher_Details");
                DataTable dt = ds.Tables[0];
                DataTable temp = new DataTable();
                temp.Columns.Add("account_id");
                temp.Columns.Add("group_id");
                temp.Columns.Add("credit");
                temp.Columns.Add("debit");
                temp.Columns.Add("type");
                temp.Columns.Add("voucher_no");
                temp.Columns.Add("Alternet_Account");
                temp.Columns.Add("description");
                double transsport_tax = 0;
                double expense_tax = 0;
                double cgst = Convert.ToDouble(lbl_total_gst_value.Text) / 2;
                double sgst = cgst;
                double igst = Convert.ToDouble(lbl_total_gst_value.Text);
                if (hd_party_id.Value == "" || hd_party_id.Value == "cash")
                {
                    temp.Rows.Add("cash", My.group["cash"], grand_total, "", "Purchase", bill_no, "purchase");
                    temp.Rows.Add("purchase", My.group["purchase"], "", lbl_total_taxable.Text, "Purchase", bill_no, "cash");
                }
                else
                {
                    temp.Rows.Add(hd_party_id.Value, My.group[hd_party_id.Value], grand_total, "", "Purchase", bill_no, "purchase");
                    temp.Rows.Add("purchase", My.group["purchase"], "", lbl_total_taxable.Text, "Purchase", bill_no, hd_party_id.Value);
                }

                //DataTable exp_dt = My.dataTable("select * from purchase_expance_details where    firm ='" + My.firm + "' and session='" + My.get_session() + "' and serial_no='" + sl_no + "'");
                //foreach (DataRow dr in exp_dt.Rows)
                //{
                //    temp.Rows.Add("direct_exp", My.group["direct_exp"], "", dr["Taxable"], "Purchase", bill_no, "purchase", dr["description"] + " on invoice no " + bill_no);
                //    expense_tax += My.toDouble(dr["GST_value"]);
                //}


                //DataTable tp_dt = My.dataTable("select * from purchase_tranportation_details where    firm ='" + My.firm + "' and session='" + My.get_session() + "' and serial_no='" + sl_no + "'");
                //foreach (DataRow dr in tp_dt.Rows)
                //{
                //    temp.Rows.Add("transport_out", My.group["transport_out"], "", dr["Taxable"], "Purchase", bill_no, "purchase", "Tranportation for " + dr[0] + " on invoice no " + bill_no);
                //    transsport_tax += My.toDouble(dr["GST_value"]);
                //}

                if (hd_state_code.Value == My.state_code)
                {


                    if (cgst + (transsport_tax / 2) + (expense_tax / 2) > 0)
                    {
                        temp.Rows.Add("cgst", My.group["cgst"], "", cgst + (transsport_tax / 2) + (expense_tax / 2), "Purchase", bill_no, "purchase");
                    }
                    if (sgst + (transsport_tax / 2) + (expense_tax / 2) > 0)
                    {
                        temp.Rows.Add("sgst", My.group["sgst"], "", sgst + (transsport_tax / 2) + (expense_tax / 2), "Purchase", bill_no, "purchase");
                    }
                }
                else
                {

                    if (igst + transsport_tax + expense_tax > 0)
                    {
                        temp.Rows.Add("igst", My.group["igst"], "", igst + transsport_tax + expense_tax, "Purchase", bill_no, "purchase");
                    }
                }
                //if (tcs > 0)
                //{
                //    temp.Rows.Add("tcs", My.group["tcs"], "", tcs, "Purchase", bill_no, "purchase");
                //}


                //if (cess > 0)
                //{
                //    temp.Rows.Add("cess", My.group["cess"], "", cess, "Purchase", bill_no, "purchase");
                //}

                //if (flat_disc > 0)
                //{
                //    temp.Rows.Add("disc_received", My.group["disc_received"], flat_disc, "", "Purchase", bill_no, "purchase");
                //}




                if (My.toDouble(round_off) < 0)
                {
                    temp.Rows.Add("round_off", My.group["round_off"], (My.toDouble(round_off) * (-1)).ToString("0.00"), "", "Purchase", bill_no, "purchase");

                }
                else if (My.toDouble(round_off) > 0)
                {
                    temp.Rows.Add("round_off", My.group["round_off"], "", round_off, "Purchase", bill_no, "purchase");
                }


                int count = 0;
                int r_count = dt.Rows.Count;
                if (temp.Rows.Count >= r_count)
                {
                    foreach (DataRow r in temp.Rows)
                    {
                        if (count < r_count)
                        {
                            add_to_ledger(r, dt.Rows[count], bill_no, purchase_date, purchase_from);
                            DataRow dr1 = dt.Rows[count];
                            // My.remove_previous_ledger(dr1["Account_id"], dr1["Credit"], dr1["Debit"], dr1["Session"]);

                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            add_to_ledger(r, dr, bill_no, purchase_date, purchase_from);
                            dt.Rows.Add(dr);
                            count++;
                        }

                        count++;
                    }
                }
                else if (temp.Rows.Count < dt.Rows.Count)
                {
                    foreach (DataRow r in temp.Rows)
                    {
                        add_to_ledger(r, dt.Rows[count], bill_no, purchase_date, purchase_from);
                        DataRow dr1 = dt.Rows[count];
                        //My.remove_previous_ledger(dr1["Account_id"], dr1["Credit"], dr1["Debit"], dr1["Session"]);
                        count++;
                    }
                    for (int i = count; i < r_count; i++)
                    {
                        DataRow dr1 = dt.Rows[i];
                        //My.remove_previous_ledger(dr1["Account_id"], dr1["Credit"], dr1["Debit"], dr1["Session"]);
                        dt.Rows[i].Delete();
                    }
                }

                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
            #endregion send to account ledger
        }
        private void add_to_ledger(DataRow tr, DataRow dr, string bill_no, string purchase_date, string purchase_from)
        {
            string description = "Purchase from " + purchase_from + ", Invoice No : " + bill_no.Replace("'", "''");
            dr["Account_id"] = tr["account_id"];
            if (tr["description"].ToString() != "")
            {
                description = tr["description"].ToString();
            }
            dr["Description"] = description;
            dr["Credit"] = My.toDouble(tr["credit"]);
            dr["Debit"] = My.toDouble(tr["debit"]);
            dr["Date"] = purchase_date;
            dr["IDate"] = My.convertidate(purchase_date);
            dr["Group_id"] = tr["group_id"];
            dr["unique_entry_id"] = ViewState["unique_entry_id"].ToString();
            dr["VoucherNo"] = tr["voucher_no"];
            dr["VoucherType"] = tr["type"];
            dr["firm"] = My.firm_id();
            dr["Session"] = My.get_session();
            dr["Alternet_Account"] = tr["Alternet_Account"];
            dr["time"] = mycode.time();
            dr["Bill_from"] = "SCHOOL";
            dr["Created_by"] = ViewState["Userid"].ToString();
            // My.add_to_closing_balance(dr["unique_entry_id"], dr["Account_id"], dr["Credit"], dr["Debit"]);
        }

        #endregion update_data

        protected void txt_purchase_order_number_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Purchase_order");
                cmd.Parameters.AddWithValue("@sp_status ", "Fetch6");
                cmd.Parameters.AddWithValue("@Status", "Submitted");
                cmd.Parameters.AddWithValue("@PO_no", txt_purchase_order_number.Text);
                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);

                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount > 0)
                {
                    txt_suppliername.Text = ds.Tables[0].Rows[0]["party_name"].ToString();
                    hd_party_id.Value = ds.Tables[0].Rows[0]["Party_id"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnk_add_item_Click(object sender, EventArgs e)
        {
            //string intId = lbl_Patient_Registration_no.Text;

            string strPopup = "<script language='javascript' ID='script1'>"
             // Passing intId to popup window.
             //+ "window.open('Registration_fee_collection.aspx?data=" + HttpUtility.UrlEncode(intId)
             + "window.open('master/additems.aspx"
            + "','new window', 'top=100, left=400, width=800, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }

        protected void lnk_add_party_Click(object sender, EventArgs e)
        {
            //string intId = lbl_Patient_Registration_no.Text;

            string strPopup = "<script language='javascript' ID='script1'>"

             // Passing intId to popup window.
             //+ "window.open('Registration_fee_collection.aspx?data=" + HttpUtility.UrlEncode(intId)
             + "window.open('master/addsupplier.aspx"
            + "','new window', 'top=100, left=400, width=800, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }

        protected void txt_disc_pers_TextChanged(object sender, EventArgs e)
        {
            try { calculate_rate(); ddl_gst.Focus(); }
            catch (Exception ex)
            {

            }
        }

        protected void txt_freight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                calculate_total();
                txt_remarks.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //string intId = lbl_Patient_Registration_no.Text;

            string strPopup = "<script language='javascript' ID='script1'>"

             // Passing intId to popup window.
             //+ "window.open('Registration_fee_collection.aspx?data=" + HttpUtility.UrlEncode(intId)
             + "window.open('master/add_brand.aspx"
            + "','new window', 'top=100, left=400, width=800, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }

        protected void lnk_refersh_Click(object sender, EventArgs e)
        {
            bind_brand();
        }
    }
}