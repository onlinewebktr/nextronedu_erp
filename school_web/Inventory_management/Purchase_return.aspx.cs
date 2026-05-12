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
    public partial class Purchase_return : System.Web.UI.Page
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
                       


                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }
      
      



        #region page_event
        protected void txt_suppliername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                find_supplier_details();

                bind_invoice();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void bind_invoice()
        {
            string qry = " select distinct invoice_no,invoice_no from dbo.[HMS_inventory_purchase_entry_itemwise] where party_id='" + hd_party_id.Value + "' and Status!='Pending'";
            mycode.bind_all_ddl_with_id_All(txt_invoice_no, qry);
        }

        private void find_pending_entry()
        {
            string qry = "Select top 1 * from dbo.[HMS_inventory_purchase_entry_itemwise] where party_id='" + hd_party_id.Value + "' and Status!='Pending' and invoice_no='" + txt_invoice_no.Text + "'";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                ViewState["unique_entry_id"] = dt.Rows[0]["unique_entry_id"].ToString();
                hd_cart_id.Value = dt.Rows[0]["Store_id"].ToString();
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
                find_pending_entry();
                
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

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
            double gst = 0;

            DataTable item_dt = (DataTable)ViewState["item_dt"];
            foreach (DataRow dr in item_dt.Rows)
            {
                total_qty += My.toDouble(dr["Quantity"]);
                total += My.toDouble(dr["Purchase_rate"]) * My.toDouble(dr["Quantity"]);
                gst = My.toDouble(dr["Gst_percent"]);
                taxable += My.toDouble(dr["Purchase_rate"]) * My.toDouble(dr["Quantity"]);
                net_total += (My.toDouble(dr["Purchase_rate"]) * My.toDouble(dr["Quantity"])) + (((My.toDouble(dr["Purchase_rate"]) * My.toDouble(dr["Quantity"])) * gst / 100));
                totalgst += ((My.toDouble(dr["Purchase_rate"]) * My.toDouble(dr["Quantity"])) * gst / 100);

            }
            lbl_totl_items.Text = item_dt.Rows.Count.ToString();
            lbl_total_.Text = total.ToString("0.00");
            lbl_total_taxable.Text = taxable.ToString("0.00");
            lbl_total_gst_value.Text = totalgst.ToString("0.00");

            //double sub_total = net_total - flat_disc + tcs;
            double sub_total = net_total;
            lbl_round_off.Text = (My.Round(sub_total, 0) - My.Round(sub_total, 2)).ToString("0.00");
            lbl_grand_total.Text = My.Round(net_total, 0).ToString("0.00");
            lbl_total_qty.Text = total_qty.ToString();

        }
       
      
        private void fill_gridview()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Inventory_stock_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch_items_for_return");
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
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
       
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                //using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                //{
                    update_stock();

                

                //    ts.Complete();
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
                string Item_name = ((Label)grd_view.Rows[i].FindControl("lbl_Item_name")).Text;
                string item_code = ((Label)grd_view.Rows[i].FindControl("lbl_Item_Code")).Text;
                string Batch_no = ((Label)grd_view.Rows[i].FindControl("lbl_Batch_no")).Text;
                string Expiry_date = ((Label)grd_view.Rows[i].FindControl("lbl_Expiry_date")).Text;
                string Brand_Id = ((Label)grd_view.Rows[i].FindControl("lbl_Brand_Id")).Text;
                string Unit_id = ((Label)grd_view.Rows[i].FindControl("lbl_Unit_id")).Text;
                string HSN = ((Label)grd_view.Rows[i].FindControl("lbl_Hsn_no")).Text;
                string quantity = ((TextBox)grd_view.Rows[i].FindControl("txt_qty")).Text;
                string Cost_rate = ((Label)grd_view.Rows[i].FindControl("lbl_rate")).Text;
                string gst_per = ((Label)grd_view.Rows[i].FindControl("lbl_gst_per")).Text;
                string gst_value = ((Label)grd_view.Rows[i].FindControl("lbl_gst_value")).Text;
                string Net_total = ((Label)grd_view.Rows[i].FindControl("lbl_Net_total")).Text;
                string  stock_item_unique_entry_id = ((Label)grd_view.Rows[i].FindControl("lbl_stock_item_unique_entry_id")).Text;
                string orgqty = ((Label)grd_view.Rows[i].FindControl("lbl_qty")).Text;
                


                string Id = ((Label)grd_view.Rows[i].FindControl("lbl_Id")).Text;
                total_Cost_rate += Convert.ToDouble(Cost_rate);


                string qry = "Update HMS_Inventory_stock_details set Quantity=Quantity-" + My.toDouble(quantity) + " where id=" + Id;
                My.exeSql(qry);
                Alertme("Purchase Return Submitted Successfully", "success");


                if (ViewState["unique_entry_id"] == null)
                {
                    ViewState["unique_entry_id"] = My.unique_id();
                }
                SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_return_entry_itemwise");
                cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
                cmd.Parameters.AddWithValue("@Id ", HdID.Value);
                cmd.Parameters.AddWithValue("@Store_id ", hd_cart_id.Value);
                cmd.Parameters.AddWithValue("@invoice_no ", txt_invoice_no.Text);
                cmd.Parameters.AddWithValue("@party_id ", hd_party_id.Value);
                cmd.Parameters.AddWithValue("@session ", My.get_session());
                cmd.Parameters.AddWithValue("@firm ", My.firm_id());
                cmd.Parameters.AddWithValue("@Item_Code ", item_code);
                cmd.Parameters.AddWithValue("@Brand_Id ", Brand_Id);
                cmd.Parameters.AddWithValue("@Hsn_no ", HSN);
                cmd.Parameters.AddWithValue("@Quantity ", quantity);
                cmd.Parameters.AddWithValue("@Unit_id ", Unit_id);
                cmd.Parameters.AddWithValue("@Purchase_rate ", Cost_rate);
                cmd.Parameters.AddWithValue("@Total ", My.toDouble(Cost_rate)*My.toDouble(quantity));
                cmd.Parameters.AddWithValue("@Gst_percent ", gst_per);
                cmd.Parameters.AddWithValue("@Gst_value ", gst_value);
                cmd.Parameters.AddWithValue("@Net_total ", Net_total);
                cmd.Parameters.AddWithValue("@IGST ", 0);
                cmd.Parameters.AddWithValue("@CGST ", My.toDouble(gst_value) / 2);
                cmd.Parameters.AddWithValue("@SGST ", My.toDouble(gst_value) / 2);
                cmd.Parameters.AddWithValue("@date ", mycode.date());
                cmd.Parameters.AddWithValue("@time ", mycode.time());
                cmd.Parameters.AddWithValue("@idate ", mycode.idate());
                cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
                cmd.Parameters.AddWithValue("@user_id ", Session["Admin"].ToString());
                cmd.Parameters.AddWithValue("@Status ", "Pending");
                cmd.Parameters.AddWithValue("@Purchase_date", txt_Date.Text);
                cmd.Parameters.AddWithValue("@Batch_no", txt_Date.Text);
                cmd.Parameters.AddWithValue("@Expiry_date", txt_Date.Text);

                int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                if (rowsAffected > 0)
                {

                    try
                    {
                        if(My.toInt(orgqty) > My.toInt(quantity) )
                        {
                            string item_code11 = item_code;
                            Sale_Purchase.HMS_Item_Account_Ledger_udpate("1", item_code, Unit_id, txt_Date.Text, ViewState["unique_entry_id"].ToString(), stock_item_unique_entry_id, "0", quantity, "Purchase Return");
                        }
                        else
                        {

                        }
                        
                    }
                    catch
                    {

                    }

                    Alertme("Data updated successfully", "success");
                    My.send_data_to_user_log_history(Session["Admin"].ToString() + " item added for purchase return " + Item_name + " with Item code " + item_code + " in Invoice No : " + txt_invoice_no.Text, Session["Admin"].ToString());


                    fill_gridview();
                }


            }
            update_accont_ledger(txt_invoice_no.Text, lbl_grand_total.Text, lbl_round_off.Text, txt_Date.Text, txt_suppliername.Text);
            send_to_product_bill_wise(total_Cost_rate);
            fill_gridview();
        }
        private void send_to_product_bill_wise(double total_sell_price)
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_return_entry_billwise");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
            cmd.Parameters.AddWithValue("@Store_id ", hd_cart_id.Value);
            cmd.Parameters.AddWithValue("@invoice_no ", txt_invoice_no.Text);
            cmd.Parameters.AddWithValue("@party_id ", hd_party_id.Value);
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Total_items ", lbl_totl_items.Text);
            cmd.Parameters.AddWithValue("@Total_qty ", lbl_total_qty.Text);
            cmd.Parameters.AddWithValue("@Total_Purchase_rate ", lbl_total_.Text);
            cmd.Parameters.AddWithValue("@Total_Gst_value ", lbl_total_taxable.Text);
            cmd.Parameters.AddWithValue("@Total_IGST ", 0);
            cmd.Parameters.AddWithValue("@Total_CGST ", Convert.ToDouble(lbl_total_taxable.Text) / 2);
            cmd.Parameters.AddWithValue("@Total_SGST ", Convert.ToDouble(lbl_total_taxable.Text) / 2);
            cmd.Parameters.AddWithValue("@Grand_total ", lbl_grand_total.Text);
            cmd.Parameters.AddWithValue("@date ", mycode.date());
            cmd.Parameters.AddWithValue("@time ", mycode.time());
            cmd.Parameters.AddWithValue("@idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@user_id ", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
            cmd.Parameters.AddWithValue("@Purchase_date ", txt_Date.Text);
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
                    temp.Rows.Add("cash", My.group["cash"], grand_total, "", "Purchase Return", bill_no, "purchase_return");
                    temp.Rows.Add("purchase", My.group["purchase"], "", lbl_total_taxable.Text, "Purchase Return", bill_no, "cash");
                }
                else
                {
                    temp.Rows.Add(hd_party_id.Value, My.group[hd_party_id.Value], grand_total, "", "Purchase Return", bill_no, "purchase_return");
                    temp.Rows.Add("purchase", My.group["purchase"], "", lbl_total_taxable.Text, "Purchase Return", bill_no, hd_party_id.Value);
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
                        temp.Rows.Add("cgst", My.group["cgst"], "", cgst + (transsport_tax / 2) + (expense_tax / 2), "Purchase Return", bill_no, "purchase_return");
                    }
                    if (sgst + (transsport_tax / 2) + (expense_tax / 2) > 0)
                    {
                        temp.Rows.Add("sgst", My.group["sgst"], "", sgst + (transsport_tax / 2) + (expense_tax / 2), "Purchase Return", bill_no, "purchase_return");
                    }
                }
                else
                {

                    if (igst + transsport_tax + expense_tax > 0)
                    {
                        temp.Rows.Add("igst", My.group["igst"], "", igst + transsport_tax + expense_tax, "Purchase Return", bill_no, "purchase_return");
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
                    temp.Rows.Add("round_off", My.group["round_off"], (My.toDouble(round_off) * (-1)).ToString("0.00"), "", "Purchase Return", bill_no, "purchase_return");

                }
                else if (My.toDouble(round_off) > 0)
                {
                    temp.Rows.Add("round_off", My.group["round_off"], "", round_off, "Purchase Return", bill_no, "purchase_return");
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
            string description = "Goods return to " + purchase_from + ", Invoice No : " + bill_no.Replace("'", "''");
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
        double total_qty1 = 0;
        double total1 = 0, net_total1 = 0;
        double taxable1 = 0;
        double totalgst1 = 0;
        double gst1 = 0;
        protected void txt_qty_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GridViewRow row = (GridViewRow)txt.NamingContainer;

                if (txt.Text == "" || txt.Text == "0")
                {
                    return;
                }
                Label lbl_qty = (Label)row.FindControl("lbl_qty");
                //TextBox txt_qty = (TextBox)row.FindControl("txt_qty");
                Label lbl_rate = (Label)row.FindControl("lbl_rate");
                Label lbl_gst_per = (Label)row.FindControl("lbl_gst_per");
                Label lbl_gst_value = (Label)row.FindControl("lbl_gst_value");
                Label lbl_Net_total = (Label)row.FindControl("lbl_Net_total");
                if (My.toDouble(txt.Text) > My.toDouble(lbl_qty.Text))
                {
                    return;
                }

                double rate = My.toDouble(lbl_rate.Text);
                double qty = My.toDouble(txt.Text);
                double tax_percent = My.toDouble(lbl_gst_per.Text);
                double total = rate * qty;


                double taxable = total;
                double total_tax = My.Round(taxable * tax_percent / 100);
                double total_cess = 0;
                double tcs_per = 0;
                double net = taxable + total_tax + total_cess;
                double total_tcs = My.Round((net * tcs_per / 100), 2);
                lbl_gst_value.Text = total_tax.ToString("0.00");
                lbl_Net_total.Text = net.ToString("0.00");



                calculate_after_modify();
            }
            catch (Exception ex)
            {

            }
        }

        private void calculate_after_modify()
        {
            for (int i = 0; i < grd_view.Rows.Count; i++)
            {
                string txtqty = ((TextBox)grd_view.Rows[i].FindControl("txt_qty")).Text;
                string lblrate = ((Label)grd_view.Rows[i].FindControl("lbl_rate")).Text;
                string lblgst_per = ((Label)grd_view.Rows[i].FindControl("lbl_gst_per")).Text;
                string lblgst_value = ((Label)grd_view.Rows[i].FindControl("lbl_gst_value")).Text;
                string lblNet_total = ((Label)grd_view.Rows[i].FindControl("lbl_Net_total")).Text;

                total_qty1 += My.toDouble(txtqty);
                total1 += My.toDouble(lblrate) * My.toDouble(txtqty);
                gst1 = My.toDouble(lblgst_per);
                taxable1 += My.toDouble(lblrate) * My.toDouble(txtqty);
                net_total1 += (My.toDouble(lblrate) * My.toDouble(txtqty)) + (((My.toDouble(lblrate) * My.toDouble(txtqty)) * gst1 / 100));
                totalgst1 += ((My.toDouble(lblrate) * My.toDouble(txtqty)) * gst1 / 100);


            }


            lbl_totl_items.Text = grd_view.Rows.Count.ToString();
            lbl_total_.Text = total1.ToString("0.00");
            lbl_total_taxable.Text = taxable1.ToString("0.00");
            lbl_total_gst_value.Text = totalgst1.ToString("0.00");

            //double sub_total = net_total - flat_disc + tcs;
            double sub_total = net_total1;
            lbl_round_off.Text = (My.Round(sub_total, 0) - My.Round(sub_total, 2)).ToString("0.00");
            lbl_grand_total.Text = My.Round(net_total1, 0).ToString("0.00");
            lbl_total_qty.Text = total_qty1.ToString();
        }

        protected void grd_view_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl_qty = (Label)e.Row.FindControl("lbl_qty");
                    TextBox txtqty = (TextBox)e.Row.FindControl("txt_qty");
                    Label lbl_rate = (Label)e.Row.FindControl("lbl_rate");
                    Label lbl_gst_per = (Label)e.Row.FindControl("lbl_gst_per");
                    Label lbl_gst_value = (Label)e.Row.FindControl("lbl_gst_value");
                    Label lbl_Net_total = (Label)e.Row.FindControl("lbl_Net_total");

                    double rate = My.toDouble(lbl_rate.Text);
                    double qty = My.toDouble(txtqty.Text);
                    double tax_percent = My.toDouble(lbl_gst_per.Text);
                    double total = rate * qty;


                    double taxable = total;
                    double total_tax = My.Round(taxable * tax_percent / 100);
                    double total_cess = 0;
                    double tcs_per = 0;
                    double net = taxable + total_tax + total_cess;
                    double total_tcs = My.Round((net * tcs_per / 100), 2);
                    lbl_gst_value.Text = total_tax.ToString("0.00");
                    lbl_Net_total.Text = net.ToString("0.00");


                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
    }
}