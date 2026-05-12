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
    public partial class Day_End_Closing_Report : System.Web.UI.Page
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

                    txt_from_Date.Text = mycode.date();
                    txt_to_Date.Text = mycode.date();



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
                if (txt_from_Date.Text == "")
                {
                    Alertme("Please enter start date");
                }
                else if (txt_to_Date.Text == "")
                {
                    Alertme("Please enter end date ");
                }
                else
                {
                    string sdate = txt_from_Date.Text;
                    string sday = sdate.Substring(0, 2);
                    string smonth = sdate.Substring(3, 2);
                    string syear = sdate.Substring(6, 4);

                    string edate = txt_to_Date.Text;
                    string eday = edate.Substring(0, 2);
                    string emonth = edate.Substring(3, 2);
                    string eyear = edate.Substring(6, 4);

                    int idate = Convert.ToInt32(syear + smonth + sday);
                    int idate2 = Convert.ToInt32(eyear + emonth + eday);

                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.");
                    }
                    else
                    {
                        //string 
                        string query = "Select im.Item_Name,um.Unit,ial.Item_Code,ial.Unit_id  from HMS_Item_Account_Ledger ial join HMS_INVETORY_ITEM_MASTER im on im.Item_id=ial.Item_Code and im.Unit_id=ial.Unit_id join unit_master um on im.Unit_id=um.unit_id group by im.Item_Name,um.Unit,ial.Item_Code,ial.Unit_id  ";
                        Bind_grdi_data(query);

                    }
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
                lbl_heading.Text = "From Date" + txt_from_Date.Text + " End Date: " + txt_to_Date.Text;
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
                Label lbl_closingsale_amount = (Label)e.Row.FindControl("lbl_closingsale_amount");

                //if(lbl_item_code.Text== "38")
                //{
                //    string a = lbl_item_code.Text;
                //}
                Bind_item_wise_stock(lbl_item_code.Text, lbl_unit_id.Text, lbl_Purchase, lbl_sales, lbl_closing_stock, lbl_opening_stock, lbl_closingsale_amount);
            }
        }

        private void Bind_item_wise_stock(string item_code, string unit_id, Label lbl_Purchase, Label lbl_sales, Label lbl_closing_stock, Label lbl_opening_stock, Label lbl_closingsale_amount)
        {
            DateTime now = DateTime.Now;
            var startDate = txt_from_Date.Text; //new DateTime(Convert.ToInt32(ddl_year.Text), Convert.ToInt32(ddl_month.SelectedValue), 1);

            var endDate = txt_to_Date.Text; //startDate.AddMonths(1).AddDays(-1);


            //________________________________________Start Closing Stock___________________
            int inputqty = 0, outputqty = 0, openingstock = 0;
            string query1 = "select  isnull(sum(Input_qty),0) inputqty ,isnull(sum(Output_qty),0)outputqty from HMS_Item_Account_Ledger where Item_Code='" + item_code + "' and Unit_id='" + unit_id + "' and Idate<" + mycode.ConvertStringToiDateup(startDate) + " ";
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
            string query2 = "select   isnull(sum(Input_qty),0) inputqty ,isnull(sum(Output_qty),0)outputqty from HMS_Item_Account_Ledger where Item_Code='" + item_code + "' and Unit_id='" + unit_id + "' and Idate>=" + mycode.ConvertStringToiDateup(startDate) + " and Idate<=" + mycode.ConvertStringToiDateup(endDate) + "";
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
            lbl_opening_stock.Text = openingstock.ToString();
            lbl_closing_stock.Text = closing_stock.ToString();

            double persaleamt = get_item_amount(item_code, unit_id);

            double finalamount = outputqty2* persaleamt;

            lbl_closingsale_amount.Text = finalamount.ToString("0.00");

        }

        private double get_item_amount(string item_code, string unit_id)
        {
            DataTable dt = mycode.FillData("Select top 1 Sale_rate from HMS_INVENTORY_STOCK_DETAILS where Item_Code='" + item_code + "' and Unit_id='" + unit_id + "' ");
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return My.toDouble(dt.Rows[0]["Sale_rate"].ToString());
            }
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