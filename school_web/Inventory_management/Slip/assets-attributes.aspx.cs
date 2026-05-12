using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management.Slip
{
    public partial class assets_attributes : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string unique_entry_id = Request.QueryString["unique_entry_id"];
                if (!string.IsNullOrEmpty(unique_entry_id))
                {
                    ViewState["unique_entry_id"] = unique_entry_id;

                    Dictionary<string, object> dc1 = Sale_Purchase.Firm_details_sale_purchase();
                    lbl_hospital_name.Text = (String)dc1["firm_name"];
                    lbl_address1.Text = (String)dc1["address"];
                    lbl_address2.Text = "";
                    img_logo.ImageUrl = (String)dc1["logo"];

                    lbl_email_mobile.Text = "Email:" + (String)dc1["email"].ToString() + ", Tel No.:" + (String)dc1["contact_no"].ToString();
                    Bind_data();
                    Bind_attribute_name();

                }
            }
        }

        private void Bind_attribute_name()
        {
            DataTable cdt = My.dataTable(" select   ad.*,am.Attribute_Name from Asset_Attribute_details ad join  Asset_Attribute_Master am on ad.Attribute_id=am.Attribute_id where ad.unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "' order by am.Attribute_Name,ad.id desc");

            if (cdt.Rows.Count == 0)
            {
                rp_attribute.DataSource = null;
                rp_attribute.DataBind();
            }
            else
            {
                rp_attribute.DataSource = cdt;
                rp_attribute.DataBind();
            }
        }

        private void Bind_data()
        {

            SqlCommand cmd = new SqlCommand("sp_asset_list");
            cmd.Parameters.AddWithValue("@sp_status ", "fetch_asset_id");
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();

            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();


            }
        }

        protected void rp_attribute_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = e.Item;
                    Label lbl_attribute_valid_to_idate = (Label)e.Item.FindControl("lbl_attribute_valid_to_idate");
                    Label lbl_status = (Label)e.Item.FindControl("lbl_status");

                    lbl_status.Text = "Expired";
                    if (My.toIntS(lbl_attribute_valid_to_idate.Text) >= My.toIntS(mycode.idate()))
                    {
                        lbl_status.Text = "Active";
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Asset_List.aspx", false);
        }
    }
}