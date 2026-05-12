using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Update_stock_ledger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        My mycode = new My();
        protected void btn_update_purchase_stock_ledger_Click(object sender, EventArgs e)
        {
            string query = "select   pei.*,peb.Purchase_date as Purchase_date_main from HMS_INVENTORY_PURCHASE_ENTRY_ITEMWISE pei join HMS_INVENTORY_PURCHASE_ENTRY_BILLWISE peb on pei.invoice_no=peb.invoice_no  where pei.Status='Submitted'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string Item_Code = dt.Rows[i]["Item_Code"].ToString();
                    string Unit_id = dt.Rows[i]["Unit_id"].ToString();
                    string inputQuantity = dt.Rows[i]["Quantity"].ToString();
                    string unique_entry_id = dt.Rows[i]["unique_entry_id"].ToString();
                    string Purchase_date = dt.Rows[i]["Purchase_date_main"].ToString();
                    string stockid = Sale_Purchase.get_stock_id(Item_Code, Unit_id, unique_entry_id);
                    Sale_Purchase.HMS_Item_Account_Ledger_udpate("1", Item_Code, Unit_id, Purchase_date, unique_entry_id, stockid, inputQuantity, "0", "Purchase Entry");
                }
            }
        }

        protected void btn_update_sale_stock_Click(object sender, EventArgs e)
        {
            string query = "select   *,format(sdb.Date, 'dd/MM/yyyy') as sealdate from HMS_INVETORY_SELL_DETAILS_ITEM_WISE sdi join HMS_INVETORY_SELL_DETAILS_BILLWISE sdb on sdb.unique_entry_id=sdi.unique_entry_id where sdi.Status='Saved'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Item_Code = dt.Rows[i]["Item_code"].ToString();
                    string Unit_id = dt.Rows[i]["unit_id"].ToString();
                    string outQuantity = dt.Rows[i]["Quantity"].ToString();
                    string stockid = dt.Rows[i]["stock_item_unique_entry_id"].ToString();
                    string unique_entry_id = dt.Rows[i]["unique_entry_id"].ToString();
                    string sealdate = dt.Rows[i]["sealdate"].ToString();

                    Sale_Purchase.HMS_Item_Account_Ledger_udpate("1", Item_Code, Unit_id, sealdate, unique_entry_id, stockid, "0", outQuantity, "Sale Entry");
                }
            }
        }
    }
}