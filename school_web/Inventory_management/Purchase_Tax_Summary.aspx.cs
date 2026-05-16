using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.IO;
using System.Data;

namespace school_web.Inventory_management
{
    public partial class Purchase_Tax_Summary : System.Web.UI.Page
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
                    Dictionary<string, object> dc1 = Sale_Purchase.Firm_details_sale_purchase();
                    lbl_hospital_name.Text = (String)dc1["firm_name"];
                    lbl_address1.Text = (String)dc1["address"];
                    lbl_address2.Text = "";
                    

                   

                    txt_from_Date.Text = mycode.date();
                    txt_to_Date.Text = mycode.date();
                    mycode.bind_all_ddl_with_id_All(ddl_GST, "Select Tax_Percent,Tax_Percent from Tax_Master where firm ='" + My.firm_id() + "'");
                    find_datewise_sale_report();

                }
            }
        }
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
                    lbl_duration.Text = "Date Wise Purchase Tax Summary From : " + txt_from_Date.Text + " To " + txt_to_Date.Text;
                    int startdate = Convert.ToInt32((Convert.ToDateTime(txt_from_Date.Text)).ToString("yyyyMMdd"));
                    int enddate = Convert.ToInt32((Convert.ToDateTime(txt_to_Date.Text)).ToString("yyyyMMdd"));
                    if (startdate > enddate)
                    {
                        Alertme("Start date can't greater than End Date.", "warning");
                        return;
                    }
                    else
                    {
                        find_datewise_sale_report();

                    }

                }

            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
        private void find_datewise_sale_report()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_Purchase_report");
            if (txt_from_Date.Text != "")
                cmd.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_Date.Text));
            if (txt_to_Date.Text != "")
                cmd.Parameters.AddWithValue("@todate", My.convertidate(txt_to_Date.Text));
            cmd.Parameters.AddWithValue("@firm_id", My.firm_id());
            cmd.Parameters.AddWithValue("@gst_per", ddl_GST.SelectedValue);
            cmd.Parameters.AddWithValue("@find_by", "Purchase_tax_summary");



            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            RPDetails.DataSource = dt;
            RPDetails.DataBind();
        }
        double taxable = 0;
        double igst = 0;
        double cgst = 0;
        double sgst = 0;
        double cess = 0;
        double net_total = 0;
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Reference the Repeater Item.
                    RepeaterItem item = e.Item;
                    Panel atag = item.FindControl("Panel1") as Panel;
                    string lbl_taxable = ((Label)item.FindControl("lbl_taxable")).Text;
                    string lbl_igst = ((Label)item.FindControl("lbl_igst")).Text;
                    string lbl_cgst = ((Label)item.FindControl("lbl_cgst")).Text;
                    string lbl_sgst = ((Label)item.FindControl("lbl_sgst")).Text;
                    string lbl_cess_value = "0";//((Label)item.FindControl("lbl_cess")).Text;
                    string lbl_net_total =  ((Label)item.FindControl("lbl_total")).Text;
                    taxable = taxable + Convert.ToDouble(lbl_taxable);
                    igst = igst + Convert.ToDouble(lbl_igst);
                    cgst = cgst + Convert.ToDouble(lbl_cgst);
                    sgst = sgst + Convert.ToDouble(lbl_sgst);
                    cess = cess + Convert.ToDouble(lbl_cess_value);
                    net_total = net_total + Convert.ToDouble(lbl_net_total);
                }
                if (e.Item.ItemType == ListItemType.Footer)
                {  //Reference the Repeater Item.
                    RepeaterItem item = e.Item;


                    Label lbl_total_taxable = item.FindControl("lbl_total_taxable") as Label;
                    lbl_total_taxable.Text = taxable.ToString();

                    Label lbl_total_igst = item.FindControl("lbl_total_igst") as Label;
                    lbl_total_igst.Text = igst.ToString();

                    Label lbl_total_cgst = item.FindControl("lbl_total_cgst") as Label;
                    lbl_total_cgst.Text = cgst.ToString();

                    Label lbl_total_sgst = item.FindControl("lbl_total_sgst") as Label;
                    lbl_total_sgst.Text = sgst.ToString();

                    Label lbl_total_cess = item.FindControl("lbl_total_cess") as Label;
                    lbl_total_cess.Text = cess.ToString();

                    Label lbl_net_total = item.FindControl("lbl_net_total") as Label;
                    lbl_net_total.Text = net_total.ToString();

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