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

namespace school_web.Admin
{
    public partial class inventory_items : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["Admin"] == null)
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                    else
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        string pagename_current = "inventory-items.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        mycode.bind_all_ddl_with_id(ddl_unit, "select Unit_name,Unit_id from Inventory_unit_master order by Unit_name asc");
                        mycode.bind_all_ddl_with_id(ddl_brand, "select Brand_name,Brand_id from Inventory_brand_master order by Brand_name asc");
                        mycode.bind_all_ddl_with_id(ddl_material_type, "select Material_name,Material_id from Inventory_Type_Master order by Material_name asc");
                        bind_grd_view();
                        txt_item_name.Focus();

                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_item_master");
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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

        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Unit_name from Inventory_unit_master where Unit_id=Inventory_item_master.Unit_id) as Unit_name,(select top 1 Material_name from Inventory_Type_Master where Material_id=Inventory_item_master.Material_id) as Material_name,(select top 1 Brand_name from Inventory_brand_master where Brand_id=Inventory_item_master.Brand_id) as Brand_name from Inventory_item_master order by Id desc");
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                Alertme("Sorry there are no data exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                btn_excels.Visible = true;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_item_name.Text == "")
            {
                Alertme("Please enter item name.", "warning");
                txt_item_name.Focus();
                return;
            }
            if (ddl_unit.SelectedItem.Text == "Select")
            {
                Alertme("Please select unit name.", "warning");
                ddl_unit.Focus();
                return;
            }

            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    submit_details();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    update_details();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }


        private void update_details()
        {
            if (mycode.IsUserExist("select Item_id from Inventory_item_master where Item_name='" + txt_item_name.Text + "' and Unit_id='" + ddl_unit.SelectedValue + "' and Material_id='" + ddl_material_type.SelectedValue + "' and Brand_id='" + ddl_brand.SelectedValue + "' and Modal_no='" + txt_modal_name.Text + "' and Id!= '" + hd_id.Value + "'"))
            {
                SqlCommand cmd;
                string query = "Update Inventory_item_master set Item_name=@Item_name,Unit_id=@Unit_id,Material_id=@Material_id,Brand_id=@Brand_id,Modal_no=@Modal_no,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id = '" + hd_id.Value + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Item_name", txt_item_name.Text);
                cmd.Parameters.AddWithValue("@Unit_id", ddl_unit.SelectedValue);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Material_id", ddl_material_type.SelectedValue);
                cmd.Parameters.AddWithValue("@Brand_id", ddl_brand.SelectedValue);
                cmd.Parameters.AddWithValue("@Modal_no", txt_modal_name.Text);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Item details has been updated successfully", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("Item already added with this name & unit.", "warning");
            }
        }



        private void submit_details()
        {
            if (mycode.IsUserExist("select Item_id from Inventory_item_master where Unit_id='" + ddl_unit.SelectedValue + "' and Material_id='" + ddl_material_type.SelectedValue + "' and Brand_id='" + ddl_brand.SelectedValue + "' and Modal_no='" + txt_modal_name.Text + "' and Item_name='" + txt_item_name.Text + "'"))
            {
                string Itemid = create_sl_no();
                SqlCommand cmd;
                string query = "INSERT INTO Inventory_item_master (Item_id,Item_name,Unit_id,Created_by,Created_date,Created_idate,Material_id,Brand_id,Modal_no) values (@Item_id,@Item_name,@Unit_id,@Created_by,@Created_date,@Created_idate,@Material_id,@Brand_id,@Modal_no)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Item_id", Itemid);
                cmd.Parameters.AddWithValue("@Item_name", txt_item_name.Text);
                cmd.Parameters.AddWithValue("@Unit_id", ddl_unit.SelectedValue);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());

                cmd.Parameters.AddWithValue("@Material_id", ddl_material_type.SelectedValue);
                cmd.Parameters.AddWithValue("@Brand_id", ddl_brand.SelectedValue);
                cmd.Parameters.AddWithValue("@Modal_no", txt_modal_name.Text);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Item master has been created successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("Item already added with this name & unit.", "warning");
            }
        }


        private string create_sl_no()
        {
            string Item_id = "";
            bool duplicate = true;
            string Items_id = My.auto_serialS("Global_sl_id");

            if (Items_id.Length == 1)
            {
                Item_id = "000" + Items_id;
            }
            else if (Items_id.Length == 2)
            {
                Item_id = "00" + Items_id;
            }
            else if (Items_id.Length == 3)
            {
                Item_id = "0" + Items_id;
            }
            else
            {
                Item_id = Items_id;

            }
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Item_id from Inventory_item_master where Item_id='" + Item_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    string Item_ids = My.auto_serialS("Global_sl_id");

                    if (Items_id.Length == 1)
                    {
                        Item_id = "000" + Items_id;
                    }
                    else if (Items_id.Length == 2)
                    {
                        Item_id = "00" + Items_id;
                    }
                    else if (Items_id.Length == 3)
                    {
                        Item_id = "0" + Items_id;
                    }
                    else
                    {
                        Item_id =  Items_id;
                    }

                }
            }

            return Item_id;
        }



        private void empty_form()
        {
            txt_modal_name.Text = "";
            txt_item_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_item_name = (Label)row.FindControl("lbl_item_name");
                    Label lbl_unit_id = (Label)row.FindControl("lbl_unit_id");

                    Label lbl_material_id = (Label)row.FindControl("lbl_material_id");
                    Label lbl_brand_id = (Label)row.FindControl("lbl_brand_id");
                    Label lbl_modal_no = (Label)row.FindControl("lbl_modal_no");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;
                    txt_item_name.Text = lbl_item_name.Text;
                    ddl_unit.SelectedValue = lbl_unit_id.Text;

                    ddl_material_type.SelectedValue = lbl_material_id.Text;
                    ddl_brand.SelectedValue = lbl_brand_id.Text;
                    txt_modal_name.Text = lbl_modal_no.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_unit_id = (Label)row.FindControl("lbl_unit_id");
                Label lbl_item_id = (Label)row.FindControl("lbl_item_id");


                if (My.dataTable("select Item_id from Inventory_stock_master where Item_id='" + lbl_item_id.Text + "'").Rows.Count == 0)
                {
                    My.exeSql("delete from Inventory_item_master where Id='" + lbl_Id.Text + "'");
                    Alertme("Item has been deleted successfully.", "success");
                }
                else
                {
                    Alertme("You can't delete this item. There is a data associated with stock.", "warning");
                }
                bind_grd_view();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

    }
}