using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Drawing;
using System.Transactions;
using System.IO;

namespace school_web.Inventory_management
{
    public partial class Return_Item_Wise_Stock : System.Web.UI.Page
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
                ViewState["stockpending"] = "0";
                Session["billfromre"] = "5";
                ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                if (!IsPostBack)
                {
                    Fetch_data_stock_return();

                }
            }
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            Fetch_data_stock_return();
        }
        private void Fetch_data_stock_return()
        {
            try
            {
                string query = "";
                if (ddl_stock.Text == "ALL")
                {
                    query = "SELECT t1.*,t2.Item_Name,t4.Brand_name,t3.unit,format(t1.Stock_Tranfer_Date, 'dd/MM/yyyy') as date12   FROM HMS_Invetory_Sale_Returen_Item_wise  t1 join HMS_Invetory_item_Master t2 on t1.Item_Code=t2.Item_id join unit_master t3 on  t1.unit_id=t3.unit_id    join  HMS_Invetory_Brand_Master t4 on t1.Brand_Id=t4.Brand_id   order by t2.Item_Name ";
                }
                else
                {
                    query = "SELECT t1.*,t2.Item_Name,t4.Brand_name,t3.unit,format(t1.Stock_Tranfer_Date, 'dd/MM/yyyy') as date12 FROM HMS_Invetory_Sale_Returen_Item_wise  t1 join HMS_Invetory_item_Master t2 on t1.Item_Code=t2.Item_id join unit_master t3 on  t1.unit_id=t3.unit_id    join  HMS_Invetory_Brand_Master t4 on t1.Brand_Id=t4.Brand_id  where     t1.Stock_transfer='" + ddl_stock.Text + "' order by t2.Item_Name ";
                }
                bind_grid_data(query);
            }
            catch (Exception ex)
            { 
            } 
        }

        private void bind_grid_data(string query)
        {
            btn_submit.Visible = false;
            ViewState["query"] = query;
            pnl_grid.Visible = false;
            DataTable dt1 = mycode.FillData(query);
            ViewState["item_dt"] = dt1;
            if (dt1.Rows.Count == 0)
            {
                Alertme("Sorry, there are no records found.", "warning");
                grd_view.DataSource = null;
                grd_view.DataBind();

            }
            else
            {
                pnl_grid.Visible = true;
                grd_view.DataSource = dt1;
                grd_view.DataBind();


                if(ViewState["stockpending"].ToString()=="1")
                {
                    btn_submit.Visible = true;
                }
            }
        }
        protected void grd_view_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Stock_transfer = (Label)e.Row.FindControl("lbl_Stock_transfer");
                CheckBox rowChkBox = (CheckBox)e.Row.FindControl("rowChkBox");
                if (lbl_Stock_transfer.Text == "Pending")
                {
                    ViewState["stockpending"] = "1";
                    rowChkBox.Visible = true;
                    e.Row.BackColor = Color.LightGreen;
                }
                else
                {
                    rowChkBox.Visible = false;

                    e.Row.BackColor = Color.Cyan;
                }
            }
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            bool finalsubmit = true;
            string confirmValue = string.Empty;
            confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {

                int growcount = grd_view.Rows.Count;
                int k = 0;
                for (int i = 0; i < growcount; i++)
                {
                    CheckBox chk = (CheckBox)grd_view.Rows[i].FindControl("rowChkBox");
                    Label lbl_stock_item_unique_entry_id = (Label)grd_view.Rows[i].FindControl("lbl_stock_item_unique_entry_id");
                    Label lbl_Id = (Label)grd_view.Rows[i].FindControl("lbl_Id");
                    Label lbl_Unit_id = (Label)grd_view.Rows[i].FindControl("lbl_Unit_id");
                    Label lbl_Item_Code = (Label)grd_view.Rows[i].FindControl("lbl_Item_Code");
                    Label lbl_unique_entry_id = (Label)grd_view.Rows[i].FindControl("lbl_unique_entry_id");
                    Label lbl_qty = (Label)grd_view.Rows[i].FindControl("lbl_qty");

                    if (chk.Checked == true)
                    {
                        finalsubmit = true;
                        // inser_HMS_Inventory_stock_details(lbl_stock_item_unique_entry_id.Text, lbl_Unit_id.Text, lbl_Item_Code.Text, lbl_unique_entry_id.Text, lbl_qty.Text, lbl_Id.Text);
                    }
                    else
                    {
                        k++;
                    }

                }
                if (k == growcount)
                {
                    Alertme("Please select at least one item from item list", "warning");


                }
                if (finalsubmit == true)
                {
                    uodate_data();
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
        }

        private void uodate_data()
        {
            bool flag = false;
            DateTime date;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    date = My.toDateTime(mycode.date() + " " + mycode.time());
                    int growcount = grd_view.Rows.Count;
                    for (int i = 0; i < growcount; i++)
                    {


                        date = My.toDateTime(mycode.date() + " " + mycode.time());
                        CheckBox chk = (CheckBox)grd_view.Rows[i].FindControl("rowChkBox");
                        Label lbl_stock_item_unique_entry_id = (Label)grd_view.Rows[i].FindControl("lbl_stock_item_unique_entry_id");
                        Label lbl_Id = (Label)grd_view.Rows[i].FindControl("lbl_Id");
                        Label lbl_Unit_id = (Label)grd_view.Rows[i].FindControl("lbl_Unit_id");
                        Label lbl_Item_Code = (Label)grd_view.Rows[i].FindControl("lbl_Item_Code");
                        Label lbl_unique_entry_id = (Label)grd_view.Rows[i].FindControl("lbl_unique_entry_id");
                        Label lbl_qty = (Label)grd_view.Rows[i].FindControl("lbl_qty");

                        if (chk.Checked == true)
                        {
                            inser_HMS_Inventory_stock_details(lbl_stock_item_unique_entry_id.Text, lbl_Unit_id.Text, lbl_Item_Code.Text, lbl_unique_entry_id.Text, lbl_qty.Text, lbl_Id.Text, date, con);
                        }


                    }
                    con.Close();
                    scope.Complete();
                    flag = true;

                }

                if (flag == true)
                {
                    Alertme("Stock has been transferred to the main stock successfully.", "success");
                    // sucess

                    bind_grid_data(ViewState["query"].ToString());
                }

            }
            catch (Exception ex)
            {
                Alertme(ex.ToString(), "warning");
            }

        }




        private void inser_HMS_Inventory_stock_details(string stock_item_unique_entry, string Unit_id, string item_code, string unique_entry_id, string quantity, string Id, DateTime date, SqlConnection con)
        {

            update_stock_final(stock_item_unique_entry, quantity, Id, date, con);

            Sale_Purchase.HMS_Item_Account_Ledger_udpate_tarn("1", item_code, Unit_id, mycode.date(), unique_entry_id, stock_item_unique_entry, quantity, "0", "Stock Transfer", con);





        }

        private void update_stock_final(string stock_item_unique_entry, string quantity, string Id, DateTime date, SqlConnection con)
        {

            DataTable dt = payments.dataTable("select * from dbo.[HMS_Inventory_stock_details] WHERE stock_item_unique_entry_id='" + stock_item_unique_entry + "'", con);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                int avlstoc = My.toint(dt.Rows[0]["Quantity"].ToString());
                SqlCommand cmd1;
                string query = "update HMS_Inventory_stock_details set Quantity=@Quantity  where stock_item_unique_entry_id=@stock_item_unique_entry_id  ";
                cmd1 = new SqlCommand(query);
                cmd1.Parameters.AddWithValue("@Quantity", avlstoc + My.toint(quantity));
                cmd1.Parameters.AddWithValue("@stock_item_unique_entry_id", stock_item_unique_entry);
                if (payments.InsertUpdateData(cmd1, con))
                {
                    SqlCommand cmd2;
                    string query2 = "update HMS_Invetory_Sale_Returen_Item_wise set Stock_Tranfer_Date=@Stock_Tranfer_Date,Stock_Transfer_by=@Stock_Transfer_by,Stock_transfer=@Stock_transfer  where Id=@Id  ";
                    cmd2 = new SqlCommand(query2);
                    cmd2.Parameters.AddWithValue("@Stock_Tranfer_Date", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
                    cmd2.Parameters.AddWithValue("@Stock_Transfer_by", ViewState["Userid"].ToString());
                    cmd2.Parameters.AddWithValue("@Stock_transfer", "Transferred");
                    cmd2.Parameters.AddWithValue("@Id", Id);
                    if (payments.InsertUpdateData(cmd2, con))
                    {

                    }
                }
            }



        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Stock_Return_Report_" + mycode.date() + "_" + mycode.time() + ".xls");

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    grd_view.RenderControl(hw);
                    string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                    Response.Write(style);
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
        #endregion

    }
}