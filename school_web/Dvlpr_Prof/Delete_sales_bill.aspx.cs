using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Delete_sales_bill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admindov"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
                else
                {
                    ViewState["Userid"] = Session["Admindov"].ToString();

                    ViewState["branchid"] = "1";




                }
            }
        }
        #region find data
        string scrpt;
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
          
            if (txt_invoice_number.Text == "")
            {

                lblmessage.Text = "Please enter invoice number";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }

            else
            {
                string query = "Select *,(select top 1 party_name  from party_details where party_id=isdb.party_id) as party_name,(isnull(convert(float, NetPayable),0)) as NetPayable1,format(isdb.Date, 'dd/MM/yyyy') as Date1 from HMS_Invetory_Sell_details_billwise isdb where isdb.Bill_No='" + txt_invoice_number.Text.Trim() + "'  ";
                bind_payment_history(query);

            }

        }



        public void Alert(string Message)
        {
            lblmessage.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        My mycode = new My();
        private void bind_payment_history(string query)
        {
            pnl_payment_history.Visible = false;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                // Alertme("There are no payment history found", "warning");
                lbl_msg.Text = "There are no payment history found";
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                pnl_payment_history.Visible = true;
                lbl_msg.Text = "";
                grd_fee.DataSource = dt;
                grd_fee.DataBind();
            }
        }
        #endregion


        double total = 0;

        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_payable.Text != "")
                {
                    total = total + Convert.ToDouble(lbl_payable.Text);
                }



            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totalamount");

                lbl_totaldiscount.Text = total.ToString("0.00");
            }
        }

        protected void btn_delete_bill_Click(object sender, EventArgs e)
        {
            try
            {
                bool issubmit = false;
                Button lnk = (Button)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_unique_no = (Label)row.FindControl("lbl_unique_no");
                TextBox txt_delete_remarks = (TextBox)row.FindControl("txt_delete_remarks");



                string query = "Insert into [HMS_INVETORY_SELL_DETAILS_ITEM_WISE_delete] (Item_code,Barcode,Rate,Quantity,unit_id,Total,Discount,Discount_Type,Discount_Percent,Taxable,Total_GST,IGST,SGST,CGST,NetTotal,TaxCalculationType,Bill_No,Sell_To,Mobile_no,Date,Idate,Status,firm,session,user_id,GST_Percent,taxable_rate,rate_before_discount,return_reason,unique_entry_id,direct_sale_desc,sale_type,HSN_Code,State_Code,State,GST_type,cess_percent,cess_value,item_type,Batch_No,Exp_Date,Manu_Date,salesman_id,size,imei_no,sale_order_no,mrp,product_serial,Doctor_name,Godown_id,mapping_id,imei_no2,order_id,table_id,s_qty,f_qty,Size_length,Size_width,rate_per_sqft,flex_size,Is_Stock_effected,db_version,update_db_version,sale_service_type,Branch_id,bulk_id,Item_unique_entry_id,is_modification,remarks,is_non_taxable,Prod_sl_start,stock_item_unique_entry_id,PCost_Rate,PTrade_Rate,PSale_Rate,sec_unit,sec_qty,item_size_in_mtr,total_item_size_in_mtr,Pur_Type_id,scheme_in_per,is_commssion_apply,Doctor_id,with_indent,HMS_bill_no,Description_Item,Brand_Id) SELECT  Item_code,Barcode,Rate,Quantity,unit_id,Total,Discount,Discount_Type,Discount_Percent,Taxable,Total_GST,IGST,SGST,CGST,NetTotal,TaxCalculationType,Bill_No,Sell_To,Mobile_no,Date,Idate,Status,firm,session,user_id,GST_Percent,taxable_rate,rate_before_discount,return_reason,unique_entry_id,direct_sale_desc,sale_type,HSN_Code,State_Code,State,GST_type,cess_percent,cess_value,item_type,Batch_No,Exp_Date,Manu_Date,salesman_id,size,imei_no,sale_order_no,mrp,product_serial,Doctor_name,Godown_id,mapping_id,imei_no2,order_id,table_id,s_qty,f_qty,Size_length,Size_width,rate_per_sqft,flex_size,Is_Stock_effected,db_version,update_db_version,sale_service_type,Branch_id,bulk_id,Item_unique_entry_id,is_modification,remarks,is_non_taxable,Prod_sl_start,stock_item_unique_entry_id,PCost_Rate,PTrade_Rate,PSale_Rate,sec_unit,sec_qty,item_size_in_mtr,total_item_size_in_mtr,Pur_Type_id,scheme_in_per,is_commssion_apply,Doctor_id,with_indent,HMS_bill_no,Description_Item,Brand_Id FROM HMS_INVETORY_SELL_DETAILS_ITEM_WISE  WHERE  unique_entry_id='"+ lbl_unique_no.Text + "'";
                mycode.executequery(query);



                string query2 = "Insert into [HMS_INVETORY_SELL_DETAILS_BILLWISE_Delete] (Bill_No,Total_Item,Total_Quantity,Total_amount,Taxable,GST,CGST,SGST,IGST,GrandTotal,Total_GR,NetPayable,party_id,Mobile,GSTIN,firm,session,user_id,Date,Idate,Discount,unique_entry_id,State,State_Code,round_off,Cess,flat_disc,net_taxable,net_igst,net_sgst,net_cgst,net_cess,Consignee_Id,supply_place,ref_inv,ccl_challan_no,unique_no,challan_no,sale_order_no,tare_wt,goods_wt,wt_unit,CustomerName,remark,pan_no,Mobile_No,Doctor_name,Address,is_print,db_version,update_db_version,sale_service_type,Branch_id,is_non_taxable,bill_settlement_status,Pur_Type_id,Registration_Type,bill_serial,P_Type,Patient_TPA_Type,with_indent,referral_id,Doctor_id,Patient_Ward,OP_IP_Bill_No,HMS_bill_no,Payment_Mode,Payment_TransactionId,Payment_Remarks,transprtation_charge,expense,After_flat_discount) Select  Bill_No,Total_Item,Total_Quantity,Total_amount,Taxable,GST,CGST,SGST,IGST,GrandTotal,Total_GR,NetPayable,party_id,Mobile,GSTIN,firm,session,user_id,Date,Idate,Discount,unique_entry_id,State,State_Code,round_off,Cess,flat_disc,net_taxable,net_igst,net_sgst,net_cgst,net_cess,Consignee_Id,supply_place,ref_inv,ccl_challan_no,unique_no,challan_no,sale_order_no,tare_wt,goods_wt,wt_unit,CustomerName,remark,pan_no,Mobile_No,Doctor_name,Address,is_print,db_version,update_db_version,sale_service_type,Branch_id,is_non_taxable,bill_settlement_status,Pur_Type_id,Registration_Type,bill_serial,P_Type,Patient_TPA_Type,with_indent,referral_id,Doctor_id,Patient_Ward,OP_IP_Bill_No,HMS_bill_no,Payment_Mode,Payment_TransactionId,Payment_Remarks,transprtation_charge,expense,After_flat_discount from HMS_INVETORY_SELL_DETAILS_BILLWISE where unique_no='" + lbl_unique_no.Text + "' ";

               
                mycode.executequery(query2);


                mycode.executequery("update HMS_INVETORY_SELL_DETAILS_BILLWISE_Delete set Delete_by='" + ViewState["Userid"].ToString() + "',Delete_date_time='" + My.getdate1() + "',Delete_remarks='" + txt_delete_remarks.Text.Replace("'", "") + "' where unique_no='" + lbl_unique_no.Text + "'");

                string query1 = "Select * from HMS_INVETORY_SELL_DETAILS_ITEM_WISE_delete where  unique_entry_id='" + lbl_unique_no.Text + "'";
                DataTable dt = mycode.FillData(query1);
                if (dt.Rows.Count == 0)
                {
                    
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string item_code = dt.Rows[i]["Item_code"].ToString();
                        string Unit_id = dt.Rows[i]["unit_id"].ToString();
                        string unique_entry_id = dt.Rows[i]["unique_entry_id"].ToString();
                        string stock_item_unique_entry_id = dt.Rows[i]["stock_item_unique_entry_id"].ToString();
                        string Quantity = dt.Rows[i]["Quantity"].ToString();
                        string Date = mycode.date();
                        Sale_Purchase.HMS_Item_Account_Ledger_udpate("1", item_code, Unit_id, Date, unique_entry_id, stock_item_unique_entry_id, Quantity, "0", "Delete bill Entry");

                    }
                }

                issubmit = true;
                if (issubmit == true)
                {
                    string query_delete = " delete FROM HMS_INVETORY_SELL_DETAILS_ITEM_WISE WHERE  unique_entry_id='" + lbl_unique_no.Text + "' ";
                    mycode.executequery(query_delete);

                    string query_delete_2 = "delete   HMS_INVETORY_SELL_DETAILS_BILLWISE WHERE  unique_no='" + lbl_unique_no.Text + "' ";


                    mycode.executequery(query_delete_2);
                    pnl_payment_history.Visible = false;
                    lblmessage.Text = "Invoice has been successfully deleted";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
    }
}