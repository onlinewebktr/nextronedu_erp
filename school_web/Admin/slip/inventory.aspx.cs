using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class inventory : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Unique_key"] != null)
                {
                    ViewState["Unique_key"] = Request.QueryString["Unique_key"];
                    Bind_inventory_info(); Bind_schoolinfo();
                }
            }
        }

        private void Bind_inventory_info()
        {
            DataTable dt = mycode.FillData("select t1.Transfer_date,t1.Transfer_time,t1.Item_id,t4.Unit_name,t1.Unit_id,t2.Item_name,t2.Modal_no,t1.Serial_no,t1.Working_status,Floor,Section,(select top 1 (Room_name +', (No. : '+ Room_no+')') from Inventory_room_master where Rooom_id=t1.Room_id) as Room_name,t1.Transfer_quantity,t1.Transfer_by,Unique_key,Serial_no,Value,Is_warranty,Expire_date,(select top 1 Material_name from Inventory_Type_Master where Material_id=t2.Material_id) as Material_name,(select top 1 Brand_name from Inventory_brand_master where Brand_id=t2.Brand_id) as Brand_name from Inventory_transfer_master t1 join Inventory_item_master t2 on t1.Item_id=t2.Item_id and t1.Unit_id=t2.Unit_id join Inventory_unit_master t4 on t1.Unit_id=t4.Unit_id where t1.Unique_key='" + ViewState["Unique_key"].ToString() + "' order by t1.id desc");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                lbl_date.Text = dt.Rows[0]["Transfer_date"].ToString();
            }
        }


        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_affiliation_no.Text = dt.Rows[0]["Affiliation"].ToString();
                lbl_schoolno.Text = dt.Rows[0]["school_no"].ToString();
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../transfer-history.aspx", false);
        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }
    }
}