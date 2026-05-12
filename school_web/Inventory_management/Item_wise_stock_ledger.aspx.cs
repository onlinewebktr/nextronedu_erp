using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Item_wise_stock_ledger : System.Web.UI.Page
    {
        My mycode = new My();
        string scrpt;
        private void Alertme(string msg)
        {
            lbl_success.Text = msg;
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
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
                if (!IsPostBack)
                {

                    mycode.bind_all_ddl_with_id(ddl_month, "select Month,Month_Id from Month_Index order by Position asc");


                    My.Bind_year(ddl_year);
                    ddl_month.SelectedValue = mycode.get_current_month_id();
                    string currentyear = mycode.get_current_year();
                    ddl_year.Text = currentyear;
                    find_button();
                }
            }
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            find_button();
          
             
           
        }

        private void find_button()
        {
            try
            {
                if (ddl_year.Text == "Select")
                {
                    Alertme("Please select year");
                }
                else if (ddl_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please select month");
                }
                else
                {
                    //string 
                    string query = "Select im.Item_Name,um.Unit,ial.Item_Code,ial.Unit_id  from HMS_Item_Account_Ledger ial join HMS_INVETORY_ITEM_MASTER im on im.Item_id=ial.Item_Code and im.Unit_id=ial.Unit_id join unit_master um on im.Unit_id=um.unit_id group by im.Item_Name,um.Unit,ial.Item_Code,ial.Unit_id  ";
                    Bind_grdi_data(query);

                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }
        }

        private void Bind_grdi_data(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_heading.Text = "";
                Alertme("Sorry there are no data list exist");
                GrdView.DataSource = null;
                GrdView.DataBind();
                lnk_excel_download.Visible = false;
                print1.Visible = false;
            }
            else
            {
                lbl_heading.Text = "Month: "+ddl_month.SelectedItem.Text+" Year: "+ddl_year.Text;
                lnk_excel_download.Visible = true;
                print1.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbl_opening_stock = (Label)e.Row.FindControl("lbl_opening_stock");
                Label lbl_item_code = (Label)e.Row.FindControl("lbl_item_code");
                Label lbl_unit_id = (Label)e.Row.FindControl("lbl_unit_id");
                Label lbl_Purchase = (Label)e.Row.FindControl("lbl_Purchase");
                Label lbl_sales = (Label)e.Row.FindControl("lbl_sales");
                Label lbl_closing_stock = (Label)e.Row.FindControl("lbl_closing_stock");


                //if(lbl_item_code.Text== "38")
                //{
                //    string a = lbl_item_code.Text;
                //}
                Bind_item_wise_stock(lbl_item_code.Text, lbl_unit_id.Text, lbl_Purchase, lbl_sales, lbl_closing_stock, lbl_opening_stock);
            }
        }

        private void Bind_item_wise_stock(string item_code, string unit_id, Label lbl_Purchase, Label lbl_sales, Label lbl_closing_stock, Label lbl_opening_stock)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(Convert.ToInt32(ddl_year.Text), Convert.ToInt32(ddl_month.SelectedValue), 1);

            var endDate = startDate.AddMonths(1).AddDays(-1);


            //________________________________________Start Closing Stock___________________
            int inputqty = 0, outputqty = 0, openingstock = 0;
            string query1 = "select  isnull(sum(Input_qty),0) inputqty ,isnull(sum(Output_qty),0)outputqty from HMS_Item_Account_Ledger where Item_Code='" + item_code + "' and Unit_id='" + unit_id + "' and Idate<" + mycode.ConvertStringToiDateup(startDate.ToString("dd/MM/yyyy")) + " ";
            DataTable dt = mycode.FillData(query1);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                inputqty = mycode.ToInt(dt.Rows[0]["inputqty"].ToInt());
                outputqty = mycode.ToInt(dt.Rows[0]["outputqty"].ToInt());

            }
            openingstock = inputqty - outputqty;
            //________________________________________End Closing Stock___________________

            int inputqty2 = 0, outputqty2 = 0, closing_stock = 0;
            string query2 = "select   isnull(sum(Input_qty),0) inputqty ,isnull(sum(Output_qty),0)outputqty from HMS_Item_Account_Ledger where Item_Code='" + item_code + "' and Unit_id='" + unit_id + "' and Idate>=" + mycode.ConvertStringToiDateup(startDate.ToString("dd/MM/yyyy")) + " and Idate<=" + mycode.ConvertStringToiDateup(endDate.ToString("dd/MM/yyyy")) + "";
            DataTable dt2 = mycode.FillData(query2);
            if (dt2.Rows.Count == 0)
            {

            }
            else
            {
                inputqty2 = mycode.ToInt(dt2.Rows[0]["inputqty"].ToInt());
                outputqty2 = mycode.ToInt(dt2.Rows[0]["outputqty"].ToInt());
            }

            closing_stock = (openingstock + inputqty2) - outputqty2;

            lbl_Purchase.Text = inputqty2.ToString();
            lbl_sales.Text = outputqty2.ToString();
            lbl_opening_stock.Text= openingstock.ToString();
            lbl_closing_stock.Text = closing_stock.ToString();
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
                GrdView.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        #endregion download_in_excel
    }
}