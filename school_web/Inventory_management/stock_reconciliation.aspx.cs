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
    public partial class stock_reconciliation : System.Web.UI.Page
    {
        My mycode = new My();
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
                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["sessionid"] = My.get_session_id();
                   
                    

                    

                }
            }
        }


        #region auto text box
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_inventory_item(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select distinct im.Item_Name  from dbo.[HMS_Invetory_item_Master] im join  HMS_Inventory_stock_details isd on isd.Item_Code=im.Item_id  where im.Item_Name     LIKE ''+@SearchprojectName+'%' order by im.Item_Name asc";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Item_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
        #endregion

        protected void txt_Item_TextChanged(object sender, EventArgs e)
        {


            try { find_item_details(); }
            catch (Exception ex)
            { My.submitexception(ex.ToString()); }
        }
        private void find_item_details()
        {
            txt_qty.Text = "0";
            DataTable dt = My.dataTable(" select * from dbo.[HMS_Invetory_item_Master] where Item_Name='" + txt_Item.Text + "'   ");
            if (dt.Rows.Count == 0)
            {

              
               
            }
            else
            {
                DataRow dr = dt.Rows[0];
                hd_itemcode.Value = dr["Item_id"].ToString();
                Bind_avl_qty();
                //ddl_unit.SelectedValue = dr["Unit_id"].ToString();

            }
        }
       
        private void Bind_avl_qty()
        {
            ViewState["id"] = "0";
            DataTable dt = My.dataTable(" select top 1 * from dbo.[HMS_INVENTORY_STOCK_DETAILS] where Item_Code='" + hd_itemcode.Value + "'  order by id desc ");
            if (dt.Rows.Count == 0)
            {


                txt_avlqty.Text = "0";
            }
            else
            {
                txt_avlqty.Text = dt.Rows[0]["Quantity"].ToString();
                ViewState["id"]= dt.Rows[0]["id"].ToString();

                ViewState["Stock_ID"] = dt.Rows[0]["Stock_ID"].ToString();
                 
            }

        }

        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_action.Text == "Select")
                {
                    Alertme("Please select action type", "warning");
                    ddl_action.Focus();
                }
                else
                {
                    int avlstock = My.toInt(txt_avlqty.Text);
                    int changeqty = My.toInt(txt_qty.Text);
                    int newqty = 0;
                    if (ddl_action.Text== "DECREASE")
                    {
                        newqty = avlstock - changeqty;
                    }
                     else
                    {
                        newqty = avlstock + changeqty;
                    }


                    if (newqty < 0)
                    {
                        newqty = -newqty; // negative ko positive bana dega
                    }
                  
                    My.Insert("Inventory_Stock_Log", new
                    {
                        Stock_ID = ViewState["Stock_ID"].ToString(),
                        Old_Quantity = avlstock.ToString(),
                        Changed_Quantity = changeqty.ToString(),
                        New_Quantity = newqty.ToString(),
                        Action_Type = ddl_action.Text,
                        Reason = txt_remarks.Text,
                        Changed_By = ViewState["Userid"].ToString(),
                        Changed_On = My.getdate1(),
                        Item_Code = hd_itemcode.Value
                    });
                    My.Update("HMS_INVENTORY_STOCK_DETAILS", new
                    {
                        Quantity = newqty,

                    },
    "id = '" + ViewState["id"].ToString() + "'");

                    Alertme("Stock reconciliation has been completed successfully.", "success");
                    hd_itemcode.Value = "0";
                }
            }
            catch(Exception ex)
            {
                Alertme(ex.ToString(), "warning");
            }



            //if(My.toint(txt_remarks.Text))
          
        }
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
    }
}