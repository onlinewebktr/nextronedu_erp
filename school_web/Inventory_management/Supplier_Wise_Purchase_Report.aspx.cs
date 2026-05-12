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
    public partial class Supplier_Wise_Purchase_Report : System.Web.UI.Page
    {
        My imp = new My();

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


                    Dictionary<string, object> dc1 = imp.Firm_details();
                    lbl_hospital_name.Text = (String)dc1["firm_name"];
                    lbl_address1.Text = (String)dc1["address"];
                    lbl_address2.Text = "";
                    txt_from_Date.Text = imp.date();
                    txt_to_Date.Text = imp.date();
                   


                    DataTable supplier_dt = My.dataTable("select 'All' as party_name,'All' party_id union all select party_name+','+address as party_name,party_id from dbo.[party_details]   where type='Supplier' and  firm='" + My.firm_id() + "'");
                    ddl_Supplier_Name.DataTextField = "party_name";
                    ddl_Supplier_Name.DataValueField = "party_id";
                    ddl_Supplier_Name.DataSource = supplier_dt;
                    ddl_Supplier_Name.DataBind();

                    find_datewise_purchase_report();
                }
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
        private void find_datewise_purchase_report()
        {
            int startdate = Convert.ToInt32((Convert.ToDateTime(txt_from_Date.Text)).ToString("yyyyMMdd"));
            int enddate = Convert.ToInt32((Convert.ToDateTime(txt_to_Date.Text)).ToString("yyyyMMdd"));
            if (startdate > enddate)
            {
                Alertme("Start date can't greater than End Date.", "warning");
                return;
            }

            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_Purchase_report");
            cmd.Parameters.AddWithValue("@firm_id", My.firm_id());
            if (txt_from_Date.Text != "")
                cmd.Parameters.AddWithValue("@fromdate", startdate);
            if (txt_to_Date.Text != "")
                cmd.Parameters.AddWithValue("@todate", enddate);
            
            cmd.Parameters.AddWithValue("@filterby", ddl_Supplier_Name.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@find_by", "Supplier_Wise_Purchase_Report");

            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];


            if (dt.Rows.Count > 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();


            }
            else
            {
                Alertme("Sorry! no record found!", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
        }
        int bill_count = 0;
        double total_amount = 0;
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Reference the Repeater Item.
                    RepeaterItem item = e.Item;
                    Panel atag = item.FindControl("Panel1") as Panel;
                    string count = ((Label)item.FindControl("lbl_count")).Text;
                    string amount = ((Label)item.FindControl("lbl_final_value")).Text;
                    bill_count = bill_count + Convert.ToInt32(count);
                    total_amount = total_amount + Convert.ToDouble(amount);

                }
                if (e.Item.ItemType == ListItemType.Footer)
                {  //Reference the Repeater Item.
                    RepeaterItem item = e.Item;

                    Label lbl_total_count = item.FindControl("lbl_total_count") as Label;
                    lbl_total_count.Text = bill_count.ToString();

                    Label lbl_total_amount = item.FindControl("lbl_total_amount") as Label;
                    lbl_total_amount.Text = total_amount.ToString();

                }

            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
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
                    lbl_duration.Text = "Date Wise Purchase Report From : " + txt_from_Date.Text + " To " + txt_to_Date.Text;
                    find_datewise_purchase_report();
                }

            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
    }
}