using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Inventory_management
{
    public partial class SupplierWisePurchaseItemDetails : System.Web.UI.Page
    {
        My imp = new My();
        private void Alertme(string msg)
        {
            lblmessage.Text = msg;
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

        }
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int startdate = Convert.ToInt32((Convert.ToDateTime(Request.QueryString["start_idate"])).ToString("yyyyMMdd"));
                    int enddate = Convert.ToInt32((Convert.ToDateTime(Request.QueryString["end_idate"])).ToString("yyyyMMdd"));
                    string party_id = Request.QueryString["party_id"].ToString();

                    Dictionary<string, object> dc1 = imp.Firm_details();
                    lbl_hospital_name.Text = (String)dc1["firm_name"];
                    lbl_address1.Text = (String)dc1["address"];
                    lbl_address2.Text = "";

                    SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_Purchase_report");
                    cmd.Parameters.AddWithValue("@firm_id", My.firm_id());
                    cmd.Parameters.AddWithValue("@fromdate", startdate);
                    cmd.Parameters.AddWithValue("@todate", enddate);
                    cmd.Parameters.AddWithValue("@filterby", party_id);
                    cmd.Parameters.AddWithValue("@find_by", "Supplier_Wise_Details");

                    DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        RPDetails.DataSource = dt;
                        RPDetails.DataBind();


                    }
                    else
                    {
                        Alertme("Sorry! no record found!");
                        RPDetails.DataSource = null;
                        RPDetails.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    My.submitexception(ex.ToString());
                }
            }
        }
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
                    string amount = ((Label)item.FindControl("lbl_final_value")).Text;
                    total_amount = total_amount + Convert.ToDouble(amount);

                }
                if (e.Item.ItemType == ListItemType.Footer)
                {  //Reference the Repeater Item.
                    RepeaterItem item = e.Item;


                    Label lbl_total_amount = item.FindControl("lbl_total_amount") as Label;
                    lbl_total_amount.Text = total_amount.ToString();

                }

            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
    }
}