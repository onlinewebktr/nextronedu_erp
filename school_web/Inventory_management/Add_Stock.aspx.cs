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
    public partial class Add_Stock : System.Web.UI.Page
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
                    bind_all_data();

                    bind_Item();

                    bind_Supplier();
                }

            }

        }

        private void bind_Supplier()
        {
            mycode.bind_all_ddl_with_id(ddl_supplier, "Select party_name,party_name from party_details");

        }

       
        private void bind_Item()
        {
            mycode.bind_all_ddl_with_id(Ddl_Item_name, "Select Item_name,Item_id from HMS_Invetory_item_Master");

        }

       
        private void bind_all_data()
        {
            bind_grd_view("select tm.*,bm.Brand_name,um.Unit_name,sm.Store_name,im.Item_name from HMS_Invetory_Stock_Entry tm join HMS_Invetory_Brand_Master bm  on tm.Brand_id = bm.Brand_id join HMS_Invetory_Unit_Master um on tm.Unit_id = um.Unit_id join HMS_Invetory_Create_Store sm on tm.Store_id = sm.Store_id join HMS_Invetory_item_Master im on tm.Item_id = im.Item_id ");
        }
        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                GrdView_Create_Stock.DataSource = null;
                GrdView_Create_Stock.DataBind();
            }
            else
            {
                GrdView_Create_Stock.DataSource = dt;
              GrdView_Create_Stock.DataBind();
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
        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                save_data();
            }
            catch(Exception ex)
            {

            }


        }


  

           

        private void save_data()
        {
            string Stock_Id = My.global_id_creation("Stock_id");
            SqlConnection cn = new SqlConnection(My.conn);
            SqlCommand cmd = new SqlCommand("Create_Stock", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Insert");
            cmd.Parameters.AddWithValue("@Stock_Id", Stock_Id);
            cmd.Parameters.AddWithValue("@Item_Id", Ddl_Item_name.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Brand_Id", Ddl_select_brand.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Unit_Id", Ddl_unit.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Quantity", Txt_Quantity.Text);
            cmd.Parameters.AddWithValue("@Quantity", Txt_Quantity.Text);
            cmd.Parameters.AddWithValue("@Quantity", Txt_Quantity.Text);
            cmd.Parameters.AddWithValue("@Quantity", Txt_Quantity.Text);
            cmd.Parameters.AddWithValue("@Quantity",Txt_Quantity.Text);
         
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            Alertme("Data Inserted Sucessfully", "sucess");
            bind_all_data();
            refresh();
            

        }

        private void refresh()
        {
            Ddl_Item_name.SelectedValue = "0";
            Ddl_select_brand.SelectedValue = "0";
            Ddl_unit.SelectedValue = "0";
            Txt_Quantity.Text = "";

            }
        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                update_data();
            }
            catch(Exception ex)
            {

            }

        }
        private void update_data()
        {
          
            SqlConnection cn = new SqlConnection(My.conn);
            SqlCommand cmd = new SqlCommand("Create_Stock", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Update");
            cmd.Parameters.AddWithValue("@Stock_Id", HdID.Value);
            cmd.Parameters.AddWithValue("@Item_Id", Ddl_Item_name.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Brand_Id", Ddl_select_brand.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Unit_Id", Ddl_unit.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Quantity", Txt_Quantity.Text);
        
            cmd.Parameters.AddWithValue("@Updated_by ", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_Date ", DateTime.Now);      

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            Alertme("Record updated successfully", "success");
            Btn_Cancel.Visible = false;
            Btn_Add.Visible = true;
            Btn_Update.Visible = false;
            bind_all_data();
            refresh();

        }
        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_Stock.aspx");
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                Label lbl_Item_id = (Label)row.FindControl("lbl_Item_id");
               
                Label lbl_Brand_Id = (Label)row.FindControl("lbl_Brand_Id");
                Label lbl_Unit_Id = (Label)row.FindControl("lbl_Unit_Id");
                Label lbl_Stock_id = (Label)row.FindControl("lbl_Stock_id");
            
                Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
             
                HdID.Value = lbl_Stock_id.Text;


               Ddl_Item_name.Text = lbl_Item_id.Text;
                Ddl_select_brand.SelectedValue = lbl_Brand_Id.Text;
                Ddl_unit.SelectedValue = lbl_Unit_Id.Text;
               
              
                Txt_Quantity.Text = lbl_Quantity.Text;
            
                Btn_Cancel.Visible = true;
                Btn_Add.Visible = false;
                Btn_Update.Visible = true;
                return;




            }
            catch (Exception ex)
            {

            }

        }

        protected void Btn_submit_Click(object sender, EventArgs e)
        {

        }

        protected void Ddl_Item_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = mycode.FillData("select im.*,bm.Brand_name,um.Unit_name from HMS_Invetory_item_Master im join HMS_Invetory_Brand_Master bm  on im.Brand_id = bm.Brand_id join HMS_Invetory_Unit_Master um on im.Unit_id = um.Unit_id where im.Item_id='" + Ddl_Item_name.SelectedValue+"'");
            if (dt.Rows.Count == 0)
            {




            }
            else
            {
                Ddl_select_brand.SelectedValue= dt.Rows[0]["Brand_id"].ToString();
                Ddl_unit.SelectedValue = dt.Rows[0]["Unit_id"].ToString();
                Txt_GST_Value.Text = dt.Rows[0]["GST"].ToString();
                Txt_HSN.Text = dt.Rows[0]["HSN"].ToString();

            }



        }
    }
}