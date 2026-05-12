using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Date_Wise_Purchase_Report : System.Web.UI.Page
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
                    Dictionary<string, object> dc1 = mycode.Firm_details();
                    lbl_hospital_name.Text = (String)dc1["firm_name"];
                    lbl_address1.Text = (String)dc1["address"];
                    lbl_address2.Text = "";
                    

                    txt_from_Date.Text = mycode.date();
                    txt_to_Date.Text = mycode.date();
                    find_datewise_sale_report();

                }
            }
        }

        protected void btn_find_Invoice_Click(object sender, EventArgs e)
        {
            if (txt_Invoice.Text == "")
            {
                Alertme("Please enter invoice number", "warning");
            }
            else
            {
                lbl_duration.Text = "Invoice Number : " + txt_Invoice.Text;
                find_invoice_wise_sale_report();
            }
        }

        private void find_invoice_wise_sale_report()
        {

            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_Purchase_report");
            cmd.Parameters.AddWithValue("@firm_id", My.firm_id());
            cmd.Parameters.AddWithValue("@find_by", "Invoice");
            cmd.Parameters.AddWithValue("@Invoice", txt_Invoice.Text);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            RPDetails.DataSource = dt;
            RPDetails.DataBind();

        }
        #region date wise find
        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            try
            {

                if (txt_from_Date.Text == "" || txt_to_Date.Text == "")
                {
                    Alertme("Select Start date and End Date.", "warning");
                }
                else
                {
                    lbl_duration.Text = "Date Wise Purchase Report From : " + txt_from_Date.Text + " To " + txt_to_Date.Text;
                    find_datewise_sale_report();
                }

            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void find_datewise_sale_report()
        {
            int startdate = Convert.ToInt32((Convert.ToDateTime(txt_from_Date.Text)).ToString("yyyyMMdd"));
            int enddate = Convert.ToInt32((Convert.ToDateTime(txt_to_Date.Text)).ToString("yyyyMMdd"));
            if (startdate > enddate)
            {
                Alertme("Start date can't greater than End Date.", "warning");
                return;
            }
            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_Purchase_report");
            if (txt_from_Date.Text != "")
                cmd.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_Date.Text));
            if (txt_to_Date.Text != "")
                cmd.Parameters.AddWithValue("@todate", My.convertidate(txt_to_Date.Text));
            cmd.Parameters.AddWithValue("@firm_id", My.firm_id());
            cmd.Parameters.AddWithValue("@find_by", "date");
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            RPDetails.DataSource = dt;
            RPDetails.DataBind();
        }
        #endregion



        double tot_Rate = 0;
        double tot_quantity = 0;
        double total_amt = 0;
        double tot_discount = 0;
        double tot_taxable = 0;
        double tot_IGST = 0;
        double tot_SGST = 0;
        double tot_CGST = 0;
        double tot_NetTotal = 0;
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Reference the Repeater Item.
                    RepeaterItem item = e.Item;
                    Panel atag = item.FindControl("Panel1") as Panel;
                    string Rate = ((Label)item.FindControl("lbl_Rate")).Text;
                    string quantity = ((Label)item.FindControl("lbl_quantity")).Text;
                    string Total = ((Label)item.FindControl("lbl_Total")).Text;
                    string discount = ((Label)item.FindControl("lbl_discount")).Text;
                    string Taxable = ((Label)item.FindControl("lbl_Taxable")).Text;
                    string IGST = ((Label)item.FindControl("lbl_IGST")).Text;
                    string SGST = ((Label)item.FindControl("lbl_SGST")).Text;
                    string CGST = ((Label)item.FindControl("lbl_CGST")).Text;
                    string NetTotal = ((Label)item.FindControl("lbl_NetTotal")).Text;
                    tot_Rate = tot_Rate + Convert.ToDouble(Rate);
                    tot_quantity = tot_quantity + Convert.ToDouble(quantity);
                    total_amt = total_amt + Convert.ToDouble(Total);
                    tot_discount = tot_discount + Convert.ToDouble(discount);
                    tot_taxable = tot_taxable + Convert.ToDouble(Taxable);
                    tot_IGST = tot_IGST + Convert.ToDouble(IGST);
                    tot_SGST = tot_SGST + Convert.ToDouble(SGST);
                    tot_CGST = tot_CGST + Convert.ToDouble(CGST);
                    tot_NetTotal = tot_NetTotal + Convert.ToDouble(NetTotal);

                }
                if (e.Item.ItemType == ListItemType.Footer)
                {  //Reference the Repeater Item.
                    RepeaterItem item = e.Item;

                    Label lbl_tot_Rate = item.FindControl("lbl_tot_Rate") as Label;
                    lbl_tot_Rate.Text = tot_Rate.ToString();

                    Label lbl_tot_quantity = item.FindControl("lbl_tot_quantity") as Label;
                    lbl_tot_quantity.Text = tot_quantity.ToString();

                    Label lbl_total_amt = item.FindControl("lbl_total_amt") as Label;
                    lbl_total_amt.Text = total_amt.ToString();

                    Label lbl_tot_discount = item.FindControl("lbl_tot_discount") as Label;
                    lbl_tot_discount.Text = tot_discount.ToString();

                    Label lbl_tot_taxable = item.FindControl("lbl_tot_taxable") as Label;
                    lbl_tot_taxable.Text = tot_taxable.ToString();

                    Label lbl_tot_IGST = item.FindControl("lbl_tot_IGST") as Label;
                    lbl_tot_IGST.Text = tot_IGST.ToString();

                    Label lbl_tot_SGST = item.FindControl("lbl_tot_SGST") as Label;
                    lbl_tot_SGST.Text = tot_SGST.ToString();

                    Label lbl_tot_CGST = item.FindControl("lbl_tot_CGST") as Label;
                    lbl_tot_CGST.Text = tot_CGST.ToString();

                    Label lbl_tot_NetTotal = item.FindControl("lbl_tot_NetTotal") as Label;
                    lbl_tot_NetTotal.Text = tot_NetTotal.ToString();

                }

            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion download_in_excel
    }
}