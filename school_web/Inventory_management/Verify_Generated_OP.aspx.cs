using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Verify_Generated_OP : System.Web.UI.Page
    {
        My mycode = new My();
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
                    try
                    {

                        Bind_Item();
                        Bind_Unit();
                        bind_brand();
                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }

                }
            }

        }
        private void bind_brand()
        {
            mycode.bind_all_ddl_with_id(ddl_brand, "Select Brand_name,Brand_id from HMS_Invetory_Brand_Master");
        }
        private void Bind_Unit()
        {
            mycode.bind_all_ddl_with_id(ddl_unit, "select Unit,unit_id from dbo.[unit_master] where firm ='" + My.firm_id() + "'");

        }

        private void Bind_Item()
        {
            mycode.bind_all_ddl_with_id(ddl_Item, "Select Item_name,Item_id from HMS_Invetory_item_Master");

        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_purchase_order_number.Text == "")
                    Alertme("Please enter P.O. No.", "warning");
                bind_all_data();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
        private void bind_all_data()
        {
            if (Session["userType"].ToString() == "Admin" || Session["userType"].ToString() == "Admin-User")
            {
                SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Purchase_order");
                cmd.Parameters.AddWithValue("@sp_status ", "Fetch7");
                cmd.Parameters.AddWithValue("@Status", "Submitted");
                cmd.Parameters.AddWithValue("@PO_no", txt_purchase_order_number.Text);
                cmd.Parameters.AddWithValue("@Session", My.get_session());
                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);

                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();
                pnl_added_items.Visible = true;
                if (rowcount > 0)
                {
                    ViewState["entry_id_po"] = ds.Tables[0].Rows[0]["entry_id_po"].ToString();
                    txt_suppliername.Text = ds.Tables[0].Rows[0]["party_name"].ToString();
                    hd_party_id.Value = ds.Tables[0].Rows[0]["Party_id"].ToString();
                    txt_Date.Text = My.display_date(ds.Tables[0].Rows[0]["Created_Date"]);
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Purchase_order");
                cmd.Parameters.AddWithValue("@sp_status ", "Fetch8");
                cmd.Parameters.AddWithValue("@Status", "Submitted");
                cmd.Parameters.AddWithValue("@PO_no", txt_purchase_order_number.Text);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Session", My.get_session());
                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();
                pnl_added_items.Visible = true;
                if (rowcount > 0)
                {
                    ViewState["entry_id_po"] = ds.Tables[0].Rows[0]["entry_id_po"].ToString();
                    txt_suppliername.Text = ds.Tables[0].Rows[0]["party_name"].ToString();
                    hd_party_id.Value = ds.Tables[0].Rows[0]["Party_id"].ToString();
                    txt_Date.Text = My.display_date(ds.Tables[0].Rows[0]["Created_Date"]);
                }
            }
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

        protected void ddl_Item_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { find_item_details(); }
            catch (Exception ex)
            { My.submitexception(ex.ToString()); }
        }
        private void find_item_details()
        {
            DataTable dt = My.dataTable(" select *,(select top 1 Purchase_rate from HMS_inventory_purchase_entry_itemwise where Item_Code=t1.Item_id order by id desc) as previous_rate  from dbo.[HMS_Invetory_item_Master] t1 where Item_Name='" + ddl_Item.SelectedItem.Text + "'   ");
            if (dt.Rows.Count == 0)
            {

                ddl_unit.SelectedValue = "0";
                ddl_brand.SelectedValue = "0";
                lbl_previous_rate.Text = "0";


            }
            else
            {
                DataRow dr = dt.Rows[0];
                bind_brand();

                ddl_unit.SelectedValue = dr["Unit_id"].ToString();
                ddl_brand.SelectedValue = dr["Brand_id"].ToString();
                lbl_previous_rate.Text = dr["previous_rate"].ToString();
            }
        }

        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Item.SelectedValue == "0")
                {
                    Alertme("Please select item name", "warning");
                    return;
                }
                if (ddl_unit.SelectedValue == "0")
                {
                    Alertme("Please select unit name", "warning");
                    return;
                }
                save_data();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void save_data()
        {
            SqlConnection cn = new SqlConnection(My.conn);
            SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Purchase_order", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sp_status", "Insert");
            cmd.Parameters.AddWithValue("@PO_no", txt_purchase_order_number.Text);
            cmd.Parameters.AddWithValue("@Item_id", ddl_Item.SelectedValue);
            cmd.Parameters.AddWithValue("@Unit_id", ddl_unit.SelectedValue);
            cmd.Parameters.AddWithValue("@Qty", Txt_quantity.Text);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_Date", My.datetime_new());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Rate", txt_rate.Text);
            cmd.Parameters.AddWithValue("@Total_rate", lbl_total.Text);
            cmd.Parameters.AddWithValue("@Status", "Submitted");
            cmd.Parameters.AddWithValue("@Party_id", hd_party_id.Value);
            cmd.Parameters.AddWithValue("@Brand_id", ddl_brand.SelectedValue);
            cmd.Parameters.AddWithValue("@firm", My.firm_id());
            cmd.Parameters.AddWithValue("@Session", My.get_session());
            cmd.Parameters.AddWithValue("@entry_id_po", ViewState["entry_id_po"].ToString());
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
                Txt_quantity.Text = "";
                ddl_Item.SelectedValue = "0";
                ddl_unit.SelectedValue = "0";
                Alertme("Data Inserted Sucessfully", "success");

                bind_all_data();


            }

        }
        protected void Btn_Update_Click(object sender, EventArgs e)

        {
            try
            {
                if (ddl_Item.SelectedValue == "0")
                {
                    Alertme("Please select item name", "warning");
                    return;
                }
                if (ddl_unit.SelectedValue == "0")
                {
                    Alertme("Please select unit name", "warning");
                    return;
                }
                Update_Data();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }

        }
        private void Update_Data()
        {
            SqlConnection cn = new SqlConnection(My.conn);
            SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Purchase_order", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sp_status", "Update");
            cmd.Parameters.AddWithValue("@Id", HdID.Value);
            cmd.Parameters.AddWithValue("@Item_id", ddl_Item.SelectedValue);
            cmd.Parameters.AddWithValue("@Unit_id", ddl_unit.SelectedValue);
            cmd.Parameters.AddWithValue("@Qty", Txt_quantity.Text);
            cmd.Parameters.AddWithValue("@Rate", txt_rate.Text);
            cmd.Parameters.AddWithValue("@Total_rate", lbl_total.Text);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            Txt_quantity.Text = "";
            ddl_Item.SelectedValue = "0";
            ddl_unit.SelectedValue = "0";
            Alertme("Record updated successfully", "success");
            Btn_Cancel.Visible = false;
            Btn_Add.Visible = true;
            Btn_Update.Visible = false;

            bind_all_data();



        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            reset_data();
        }
        private void reset_data()
        {
            Btn_Add.Visible = true;
            Btn_Update.Visible = false;
            My.ClearInputs(Page.Controls);

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                Label lbl_Item_id = (Label)row.FindControl("lbl_Item_id");
                Label lbl_Unit_id = (Label)row.FindControl("lbl_Unit_id");
                Label lbl_qty = (Label)row.FindControl("lbl_qty");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_rate = (Label)row.FindControl("lbl_rate");
                Label lbl_totalrate = (Label)row.FindControl("lbl_totalrate");
                HdID.Value = lbl_Id.Text;

                txt_rate.Text = lbl_rate.Text;
                lbl_total.Text = lbl_totalrate.Text;
                Txt_quantity.Text = lbl_qty.Text;

                ddl_Item.SelectedValue = lbl_Item_id.Text;
                ddl_unit.SelectedValue = lbl_Unit_id.Text;
                Btn_Cancel.Visible = true;
                Btn_Add.Visible = false;
                Btn_Update.Visible = true;

                return;


            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }

        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                Label lbl_Item_name = (Label)row.FindControl("lbl_Item_name");
                Label lbl_qty = (Label)row.FindControl("lbl_qty");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                delete_data(lbl_Id.Text);
                return;
            }
            catch (Exception ex)
            {
                Alertme("You can't delete this Purchasing Order.", "warning");
                My.submitexception(ex.ToString());

            }
        }

        private void delete_data(string id)
        {
            SqlCommand cmd;
            string query = "delete from  Generate_OP where Id = @id ";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@id", id);

            if (My.InsertUpdateData(cmd))
            {
                Alertme("Purchasing Order has been delete Successfully.", "success");
                bind_all_data();
            }
        }

        protected void btn_final_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdView_Generate_PO.Rows.Count > 0)
                {
                    string PO_no = txt_purchase_order_number.Text;
                    string qry = "Update HMS_Geherate_Purchase_order set Status='Submitted', Is_verify=1,PO_no='" + PO_no + "',Verify_by='" + ViewState["Userid"].ToString() + "',Verificaion_date='" + My.datetime_new() + "',Verificaion_idate='" + mycode.idate() + "' where PO_no='" + txt_purchase_order_number.Text + "'  and firm='" + My.firm_id() + "' and Session='" + My.get_session() + "'";
                    My.exeSql(qry);
                    ViewState["PO_no"] = PO_no;
                    Alertme("Purchasing Order has been verified successfully.", "success");
                    btn_print.Visible = true;
                    bind_all_data();

                }

            }
            catch (Exception ex)
            {
                Alertme("Please try again...", "warning");
                My.submitexception(ex.ToString());
            }
        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "Slip/Print_generated_PO.aspx?PO_no=" + ViewState["PO_no"].ToString() + "&entryid=" + ViewState["entry_id_po"].ToString() + "&Session=" + My.get_session() + "&firmid=" + My.firm_id();
                Response.Redirect(path, false);
            }
            catch (Exception ex)
            {
                Alertme("Please try again.", "warning");
                My.submitexception(ex.ToString());
            }

        }
        protected void txt_rate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                calculate_rate();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void calculate_rate()
        {
            lbl_total.Text = (Convert.ToDouble(Txt_quantity.Text) * Convert.ToDouble(txt_rate.Text)).ToString();
        }

        protected void Txt_quantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                calculate_rate();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

    }
}