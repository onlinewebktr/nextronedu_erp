using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Create_Item : System.Web.UI.Page
    {
        My mycode = new My();
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

                ViewState["Userid"] = Session["Admin"].ToString();
                if (!IsPostBack)
                {
                    try
                    {
                        ViewState["isupdate"] = "0";
                        //string pagename_current = Path.GetFileName(Request.Path);
                        //Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        //ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        //ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        //ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        //ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        //ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = "1"; //(String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = "1";//(String)dc1["Is_delete"];
                        ViewState["Is_Download"] = "1"; //(String)dc1["Is_Download"];
                        ViewState["Is_Print"] = "1"; //(String)dc1["Is_Print"];
                        ViewState["Is_add"] = "1"; //(String)dc1["Is_add"];
                        //bind_grd_view();
                        mycode.bind_all_ddl_with_id(ddl_item_groupname, "Select Group_Name,Group_Id from HMS_Invetory_Group_Master order by Group_Name");

                        bind_brand();
                        bind_Unit();
                        bind_GST();







                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());

                    }
                    try
                    {
                        if (Request.QueryString["itemid"] != null)
                        {
                            string itemid = Request.QueryString["itemid"];
                            ViewState["itemid"] = itemid;
                            ViewState["isupdate"] = "1";

                            Bind_data();
                        }
                    }
                    catch
                    {

                    }
                }

            }


        }

        private void Bind_data()
        {
            DataTable dt = My.dataTable("select * from HMS_Invetory_item_Master where Item_id='" + ViewState["itemid"].ToString() + "' ;");
            if (dt.Rows.Count == 0)
            {


            }
            else
            {

                HdID.Value = dt.Rows[0]["Item_id"].ToString();
                Txt_Item_Name.Text = dt.Rows[0]["Item_Name"].ToString();
                ddl_item_type.Text = dt.Rows[0]["Item_type"].ToString();
                ddl_item_groupname.SelectedValue = dt.Rows[0]["Group_Id"].ToString();
                Ddl_select_Brand.SelectedValue = dt.Rows[0]["Brand_id"].ToString();
                Ddl_Unit.SelectedValue = dt.Rows[0]["Unit_id"].ToString();
                ddl_GST.Text = dt.Rows[0]["GST"].ToString();
                Txt_HSN.Text = dt.Rows[0]["HSN"].ToString();

                txt_size.Text = dt.Rows[0]["Size"].ToString();
                ddl_color.Text = dt.Rows[0]["Color"].ToString();
                bind_secondryunitdetails();
                Btn_Cancel.Visible = true;
                Btn_Add.Visible = false;
                Btn_Update.Visible = true;
            }
        }

        private void bind_brand()
        {
            mycode.bind_all_ddl_with_id(Ddl_select_Brand, "Select Brand_name,Brand_id from HMS_Invetory_Brand_Master");

        }
        private void bind_GST()
        {
            mycode.bind_ddl(ddl_GST, "Select Tax_Percent from Tax_Master where firm ='" + My.firm_id() + "'");
        }
        private void bind_Unit()
        {
            mycode.bind_all_ddl_with_id(Ddl_Unit, "select Unit,unit_id from dbo.[unit_master] where firm ='" + My.firm_id() + "'");
        }

        private void bind_grd_view()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_item_Master");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch");
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                GrdView_Create_Item.DataSource = null;
                GrdView_Create_Item.DataBind();
            }
            else
            {
                GrdView_Create_Item.DataSource = dt;
                GrdView_Create_Item.DataBind();
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
                if (ddl_item_groupname.SelectedItem.Text == "Select")
                {
                    Alertme("Please select item group name", "warning");
                }
                else if (Ddl_select_Brand.SelectedItem.Text == "Select")
                {
                    Alertme("Please select item brand name", "warning");
                }
                else if (Ddl_Unit.SelectedItem.Text == "Select")
                {
                    Alertme("Please select item unit name", "warning");
                }
                else if (ddl_GST.Text == "Select")
                {
                    Alertme("Please select gst ", "warning");
                }
                else
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        if (mycode.FillData("select * from HMS_INVETORY_ITEM_MASTER where Item_Name ='" + Txt_Item_Name.Text + "' and  Item_Id!=" + HdID.Value + "").Rows.Count == 0)
                        {
                            save_data();
                        }
                        else
                        {
                            Alertme("Sorry you entered item name is duplicate", "warning");
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }

                }

            }
            catch (Exception ex)
            {
                Alertme("Please Try again..", "warning");
                My.submitexception(ex.ToString());

            }


        }

        private void save_data()
        {
            string Item_Id = My.global_id_creation("Item_Id");
            SqlConnection cn = new SqlConnection(My.conn);
            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_item_Master", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sp_status", "Insert");
            cmd.Parameters.AddWithValue("@Item_Id", Item_Id);
            cmd.Parameters.AddWithValue("@Item_Name", Txt_Item_Name.Text);
            cmd.Parameters.AddWithValue("@Brand_Id", Ddl_select_Brand.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@HSN ", Txt_HSN.Text);
            cmd.Parameters.AddWithValue("@GST", ddl_GST.Text);
            cmd.Parameters.AddWithValue("@Unit_Id", Ddl_Unit.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_Date", My.datetime_new());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Item_type", ddl_item_type.Text);
            cmd.Parameters.AddWithValue("@Firm", My.firm_id());
            cmd.Parameters.AddWithValue("@Group_Id", ddl_item_groupname.SelectedValue);
            if (chk_add_more_unit.Checked)
            {
                cmd.Parameters.AddWithValue("@Isaddmoreunit", "Yes");
                cmd.Parameters.AddWithValue("@secondryUnit_id", ddl_Secondry_Unit.SelectedValue);
                cmd.Parameters.AddWithValue("@unit_conversion", txt_unit_convertiontxt_unit_convertion.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Isaddmoreunit", "No");
            }
            cmd.Parameters.AddWithValue("@Color", ddl_color.Text);
            cmd.Parameters.AddWithValue("@Size", txt_size.Text);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {

                Alertme("Data Inserted Sucessfully", "success");
                bind_grd_view();
                empty_form();
            }
        }
        private void empty_form()
        {
            Btn_Add.Visible = true;
            Btn_Update.Visible = false;
            My.ClearInputs(Page.Controls);

        }
        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycode.FillData("select * from HMS_INVETORY_ITEM_MASTER where Item_Name ='" + Txt_Item_Name.Text + "' and  Item_Id!=" + HdID.Value + "").Rows.Count == 0)
                {
                    update_data();
                }
                else
                {
                    Alertme("Sorry you entered item name is duplicate", "warning");
                }
            }
            catch (Exception ex)
            {
                Alertme("Please Try again..", "warning");
                My.submitexception(ex.ToString());
            }
        }
        private void update_data()
        {

            SqlConnection cn = new SqlConnection(My.conn);
            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_item_Master", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sp_status", "Update");
            cmd.Parameters.AddWithValue("@Item_Name", Txt_Item_Name.Text);
            cmd.Parameters.AddWithValue("@Brand_Id", Ddl_select_Brand.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Unit_Id", Ddl_Unit.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@HSN ", Txt_HSN.Text);
            cmd.Parameters.AddWithValue("@GST", ddl_GST.Text);
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_Date", My.datetime_new());
            cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Item_Id", HdID.Value);
            cmd.Parameters.AddWithValue("@Item_type", ddl_item_type.Text);
            cmd.Parameters.AddWithValue("@Firm", My.firm_id());
            cmd.Parameters.AddWithValue("@Group_Id", ddl_item_groupname.SelectedValue);
            cmd.Parameters.AddWithValue("@Color", ddl_color.Text);
            cmd.Parameters.AddWithValue("@Size", txt_size.Text);
            if (chk_add_more_unit.Checked)
            {
                cmd.Parameters.AddWithValue("@Isaddmoreunit", "Yes");
                cmd.Parameters.AddWithValue("@secondryUnit_id", ddl_Secondry_Unit.SelectedValue);
                cmd.Parameters.AddWithValue("@unit_conversion", txt_unit_convertiontxt_unit_convertion.Text);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Isaddmoreunit", "No");

            }
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {


                //Response.Redirect("Item_Mater_List.aspx", false);




                if (ViewState["isupdate"].ToString() == "0")
                {
                    //Response.Redirect("Create_Item.aspx", false);
                    Alertme("Record updated successfully", "success");
                    bind_grd_view();
                    empty_form();
                }
                else
                {
                    Session["msg"] = "Record updated successfully";
                    Response.Redirect("Item_Mater_List.aspx", false);

                }
            }
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {

            if (ViewState["isupdate"].ToString() == "0")
            {
                Response.Redirect("Create_Item.aspx", false);
            }
            else
            {
                Response.Redirect("Item_Mater_List.aspx", false);

            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                Label lbl_Item_id = (Label)row.FindControl("lbl_Item_id");
                Label lbl_Item_name = (Label)row.FindControl("lbl_Item_Name");
                Label lbl_Brand_Id = (Label)row.FindControl("lbl_Brand_Id");
                Label lbl_Unit_Id = (Label)row.FindControl("lbl_Unit_Id");
                Label lbl_HSN = (Label)row.FindControl("lbl_HSN");
                Label lbl_GST = (Label)row.FindControl("lbl_GST");
                Label lbl_Item_type = (Label)row.FindControl("lbl_Item_type");
                HdID.Value = lbl_Item_id.Text;
                Txt_Item_Name.Focus();
                ddl_item_type.Text = lbl_Item_type.Text;
                Txt_Item_Name.Text = lbl_Item_name.Text.Split('-')[0];
                Ddl_select_Brand.SelectedValue = lbl_Brand_Id.Text;
                Ddl_Unit.SelectedValue = lbl_Unit_Id.Text;
                ddl_GST.Text = lbl_GST.Text;
                Txt_HSN.Text = lbl_HSN.Text.Split('-')[0];
                bind_secondryunitdetails();
                Btn_Cancel.Visible = true;
                Btn_Add.Visible = false;
                Btn_Update.Visible = true;
                return;




            }
            catch (Exception ex)
            {

            }

        }

        private void bind_secondryunitdetails()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_item_Master");
            cmd.Parameters.AddWithValue("@sp_status ", "Unitconversion");
            cmd.Parameters.AddWithValue("@Item_Id", HdID.Value);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {
                grd_muntiple_unit.DataSource = null;
                grd_muntiple_unit.DataBind();

            }
            else
            {
                chk_add_more_unit.Enabled = false;
                grd_muntiple_unit.DataSource = dt;
                grd_muntiple_unit.DataBind();
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                Label lbl_Item_id = (Label)row.FindControl("lbl_Item_id");
                Label lbl_Item_name = (Label)row.FindControl("lbl_Item_Name");
                Label lbl_Brand_name = (Label)row.FindControl("lbl_Brand_Name");
                Label lbl_Unit_name = (Label)row.FindControl("lbl_Unit_Name");
                Label lbl_HSN = (Label)row.FindControl("lbl_HSN");
                Label lbl_GST = (Label)row.FindControl("lbl_GST");

                delete_data(lbl_Item_id.Text);
                ViewState["Description"] = " Delete Inventory Item Name =" + lbl_Item_name.Text + "</br> Item Id:-" + lbl_Item_id.Text;
                My.send_data_to_user_log_history(Session["name"].ToString() + ViewState["Description"].ToString() + " on Dated-" + My.datetime_new(), Session["Admin"].ToString(), "", "INVENTORY");

                return;

            }
            catch (Exception ex)
            {

            }
        }

        private void delete_data(string Item_id)
        {
            SqlCommand cmd;
            string query = "delete from  HMS_Invetory_item_Master where Item_id = @Item_id ;";
            query = query + "delete from  Inventory_Item_Unit_Maping where Item_Code = @Item_id ;";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Item_id", Item_id);

            if (My.InsertUpdateData(cmd))
            {
                Alertme("Item Name has been delete Successfully.", "success");
                bind_grd_view();
            }


        }

        protected void chk_add_more_unit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_add_more_unit.Checked)
                    pnl_add_moreunit.Visible = true;
                else
                    pnl_add_moreunit.Visible = false;

                bind_secontryunit();
                lbl_primaryunit.Text = "1 " + Ddl_Unit.SelectedItem.Text + " =";
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_secontryunit()
        {
            mycode.bind_all_ddl_with_id(ddl_Secondry_Unit, "select Unit,unit_id from dbo.[unit_master] where firm ='" + My.firm_id() + "' and unit_id not in  ('" + Ddl_Unit.SelectedValue + "')");
        }

        protected void Ddl_Unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_primaryunit.Text = "1 " + Ddl_Unit.SelectedItem.Text + " =";
            bind_secontryunit();
        }
        protected void ddl_Secondry_Unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblsecondryunit.Text = ddl_Secondry_Unit.SelectedItem.Text;
        }

        protected void lnkDel_unit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                delete_secondry_(lbl_Id.Text);
            }
            catch (Exception ex)
            {
            }
        }

        private void delete_secondry_(string Id)
        {
            string qry = "Delete from Inventory_Item_Unit_Maping where Id='" + Id + "';";
            My.exeSql(qry);
            Alertme("unit has been delete Successfully.", "success");

            bind_secondryunitdetails();
        }
    }

}
