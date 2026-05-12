using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class stock_entry : System.Web.UI.Page
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
                        string pagename_current = "stock-entry.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        mycode.bind_all_ddl_with_id(ddl_items, "select Item_name,Item_id from Inventory_item_master order by Item_name asc");
                        bind_grd_view();
                        txt_date.Text = mycode.date();
                        txt_warnty_end_Date.Text = mycode.date();
                        txt_date.Focus();

                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_stock_master");
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
            DataTable dt = mycode.FillData("select t1.*,isnull((Transfer_qnt),'0') as Transfer_qnts, t2.Item_name,t2.Unit_id,(select top 1 Unit_name from Inventory_unit_master where Unit_id=t2.Unit_id) as Unit_name,(select top 1 Material_name from Inventory_Type_Master where Material_id=t2.Material_id) as Material_name,(select top 1 Brand_name from Inventory_brand_master where Brand_id=t2.Brand_id) as Brand_name  from Inventory_stock_master t1 join Inventory_item_master t2 on t1.Item_id=t2.Item_id order by id desc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                btn_excels.Visible = false;
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
            if (txt_date.Text == "")
            {
                Alertme("Please enter date.", "warning");
                txt_date.Focus();
                return;
            }
            if (txt_item_code.Text == "")
            {
                Alertme("Please enter item code.", "warning");
                txt_item_code.Focus();
                return;
            }
            if (ddl_items.SelectedItem.Text == "Select")
            {
                Alertme("Please select item name.", "warning");
                ddl_items.Focus();
                return;
            }
            if (ddl_brand_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select brand name.", "warning");
                ddl_brand_name.Focus();
                return;
            }
            if (ddl_material_type.SelectedItem.Text == "Select")
            {
                Alertme("Please select material type.", "warning");
                ddl_material_type.Focus();
                return;
            }
            if (txt_serial_no.Text == "Select")
            {
                Alertme("Please enter serial no.", "warning");
                txt_serial_no.Focus();
                return;
            }
            if (txt_value.Text == "Select")
            {
                Alertme("Please enter item value in rs.", "warning");
                txt_value.Focus();
                return;
            }
            if (My.toDouble(txt_qnt.Text) <= 0)
            {
                Alertme("Please enter stock quantity.", "warning");
                txt_qnt.Focus();
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
            SqlCommand cmd;
            string query = "Update Inventory_stock_master set Date=@Date,Entry_type=@Entry_type,Stock_qnt=@Stock_qnt,Remarks=@Remarks,Serial_no=@Serial_no,Value_rs=@Value_rs,Is_warranty_available=@Is_warranty_available,Warranty_end_date=@Warranty_end_date,Warranty_end_idate=@Warranty_end_idate,Working_status=@Working_status,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id = '" + hd_id.Value + "'";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Date", txt_date.Text);
            cmd.Parameters.AddWithValue("@Entry_type", ddl_store_in.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Stock_qnt", txt_qnt.Text);
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);

            cmd.Parameters.AddWithValue("@Working_status", ddl_working_status.Text);

            cmd.Parameters.AddWithValue("@Serial_no", txt_serial_no.Text);
            cmd.Parameters.AddWithValue("@Value_rs", txt_value.Text);
            cmd.Parameters.AddWithValue("@Is_warranty_available", ddl_is_waranty_avalS.SelectedItem.Text);
            if (ddl_is_waranty_avalS.SelectedItem.Text == "Yes")
            {
                cmd.Parameters.AddWithValue("@Warranty_end_date", txt_warnty_end_Date.Text);
                cmd.Parameters.AddWithValue("@Warranty_end_idate", My.DateConvertToIdate(txt_warnty_end_Date.Text));
            }
            else
            {
                cmd.Parameters.AddWithValue("@Warranty_end_date", "0");
                cmd.Parameters.AddWithValue("@Warranty_end_idate", "0");
            }

            if (My.InsertUpdateData(cmd))
            {
                Alertme("Stock details has been updated successfully", "success");
                empty_form();
                bind_grd_view();
            }
        }


        int Stock_id;
        private void submit_details()
        {
            create_sl_no();
            SqlCommand cmd;
            string query = "INSERT INTO Inventory_stock_master (Stock_id,Date,Item_id,Entry_type,Stock_qnt,Created_by,Created_date,Created_idate,Remarks,Session_id,Serial_no,Value_rs,Is_warranty_available,Warranty_end_date,Warranty_end_idate,Working_status) values (@Stock_id,@Date,@Item_id,@Entry_type,@Stock_qnt,@Created_by,@Created_date,@Created_idate,@Remarks,@Session_id,@Serial_no,@Value_rs,@Is_warranty_available,@Warranty_end_date,@Warranty_end_idate,@Working_status)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Stock_id", Stock_id);
            cmd.Parameters.AddWithValue("@Date", txt_date.Text);
            cmd.Parameters.AddWithValue("@Item_id", ViewState["ItemIDS"].ToString());
            cmd.Parameters.AddWithValue("@Entry_type", ddl_store_in.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Stock_qnt", txt_qnt.Text);
            cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Session_id", My.get_session_id());
            cmd.Parameters.AddWithValue("@Serial_no", txt_serial_no.Text);
            cmd.Parameters.AddWithValue("@Value_rs", txt_value.Text);
            cmd.Parameters.AddWithValue("@Working_status", ddl_working_status.Text);
            cmd.Parameters.AddWithValue("@Is_warranty_available", ddl_is_waranty_avalS.SelectedItem.Text);

            if (ddl_is_waranty_avalS.SelectedItem.Text == "Yes")
            {
                cmd.Parameters.AddWithValue("@Warranty_end_date", txt_warnty_end_Date.Text);
                cmd.Parameters.AddWithValue("@Warranty_end_idate", My.DateConvertToIdate(txt_warnty_end_Date.Text));
            }
            else
            {
                cmd.Parameters.AddWithValue("@Warranty_end_date", "0");
                cmd.Parameters.AddWithValue("@Warranty_end_idate", "0");
            }

            if (My.InsertUpdateData(cmd))
            {
                Alertme("Stock master has been created successfully.", "success");
                empty_form();
                bind_grd_view();
            }

        }

        private void create_sl_no()
        {
            bool duplicate = true;
            Stock_id = My.toint(My.auto_serialS("Global_sl_id"));
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Stock_id from Inventory_stock_master where Stock_id='" + Stock_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    Stock_id = My.toint(My.auto_serialS("Global_sl_id"));
                }
            }
        }

        private void empty_form()
        {
            txt_qnt.Text = "";
            txt_remarks.Text = "";
            txt_serial_no.Text = "";
            txt_value.Text = "";
            txt_item_code.Text = "";
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
                    Label lbl_is_warranty_available = (Label)row.FindControl("lbl_is_warranty_available");
                    Label lbl_unit_id = (Label)row.FindControl("lbl_unit_id");
                    Label lbl_date = (Label)row.FindControl("lbl_date");
                    Label lbl_item_id = (Label)row.FindControl("lbl_item_id");

                    Label lbl_entry_type = (Label)row.FindControl("lbl_entry_type");
                    Label lbl_stock_qnt = (Label)row.FindControl("lbl_stock_qnt");
                    Label lbl_remarks = (Label)row.FindControl("lbl_remarks");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");

                    Label lbl_serial_no = (Label)row.FindControl("lbl_serial_no");
                    Label llb_value_rs = (Label)row.FindControl("llb_value_rs");
                    Label lbl_working_status = (Label)row.FindControl("lbl_working_status");
                    hd_id.Value = lbl_Id.Text;
                    //ddl_items.SelectedValue = lbl_item_name.Text;


                    ddl_working_status.Text = lbl_working_status.Text;
                    txt_date.Text = lbl_date.Text;
                    txt_qnt.Text = lbl_stock_qnt.Text;
                    txt_remarks.Text = lbl_remarks.Text;
                    txt_item_code.Text = lbl_item_id.Text;
                    txt_serial_no.Text = lbl_serial_no.Text;
                    txt_value.Text = llb_value_rs.Text;
                    ddl_is_waranty_avalS.Text = lbl_is_warranty_available.Text;
                    isWarrantY();
                    find_item();
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
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Label lbl_unit_id = (Label)row.FindControl("lbl_unit_id");
            Label lbl_item_id = (Label)row.FindControl("lbl_item_id");
            if (My.dataTable("select Damage_goods_item_id from Hostel_student_damage_maping where Damage_goods_item_id='" + lbl_unit_id.Text + "'").Rows.Count == 0)
            {
                My.exeSql("delete from Inventory_item_master where Id='" + lbl_Id.Text + "'");
                Alertme("Item has been deleted successfully.", "success");
            }
            else
            {
                Alertme("You can't delete this unit. There is a data associated with student.", "warning");
            }
            bind_grd_view();
        }



        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select DISTINCT Item_id from Inventory_item_master where Item_id LIKE ''+@SearchMobNo+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["Item_id"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion


        protected void ddl_is_waranty_aval_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                isWarrantY();
            }
            catch (Exception ex)
            {
            }
        }

        private void isWarrantY()
        {
            if (ddl_is_waranty_avalS.SelectedItem.Text == "Yes")
            {
                wrntyDV.Visible = true;
                remrkSDV.Attributes.Add("class", "col-md-2");
            }
            else
            {
                wrntyDV.Visible = false;
                remrkSDV.Attributes.Add("class", "col-md-4");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                find_item();
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_stock_master");
            }
        }

        private void find_item()
        {
            DataTable dt = mycode.FillData("select * from Inventory_item_master where Item_id='" + txt_item_code.Text + "'");
            if (dt.Rows.Count == 0)
            {
                ViewState["ItemStatuS"] = "0";
                ViewState["ItemIDS"] = "0";
            }
            else
            {
                ViewState["ItemStatuS"] = "1";
                ViewState["ItemIDS"] = dt.Rows[0]["Item_id"].ToString();


                ddl_items.SelectedValue = dt.Rows[0]["Item_id"].ToString();
                find_brand(dt.Rows[0]["Item_id"].ToString());
                ddl_brand_name.SelectedValue = dt.Rows[0]["Brand_id"].ToString();
                find_material(dt.Rows[0]["Item_id"].ToString());
                ddl_material_type.SelectedValue = dt.Rows[0]["Material_id"].ToString();
                find_unit(dt.Rows[0]["Item_id"].ToString());
                ddl_unit.SelectedValue = dt.Rows[0]["Unit_id"].ToString();
            }
        }

        private void find_unit(string item_ids)
        {
            mycode.bind_all_ddl_with_id(ddl_unit, "select t2.Unit_name,t1.Unit_id from Inventory_item_master t1 join Inventory_unit_master t2 on t1.Unit_id=t2.Unit_id where t1.Item_id='" + item_ids + "' order by t2.Unit_name asc");
        }

        private void find_material(string item_ids)
        {
            mycode.bind_all_ddl_with_id(ddl_material_type, "select t2.Material_name,t2.Material_id from Inventory_item_master t1 join Inventory_Type_Master t2 on t1.Material_id=t2.Material_id where t1.Item_id='" + item_ids + "' order by t2.Material_name asc");
        }


        private void find_brand(string item_ids)
        {
            mycode.bind_all_ddl_with_id(ddl_brand_name, "select t2.Brand_name,t2.Brand_id from Inventory_item_master t1 join Inventory_brand_master t2 on t1.Brand_id=t2.Brand_id where t1.Item_id='" + item_ids + "' order by t2.Brand_name asc");
        }

        protected void ddl_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_item_code.Text = ddl_items.SelectedValue;
            find_item();
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