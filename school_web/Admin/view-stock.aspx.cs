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
    public partial class view_stock : System.Web.UI.Page
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
                        string pagename_current = "view-stock.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        bind_grd_view_page_load();
                        ViewState["flag"] = "0";
                        txt_transfer_date.Text = mycode.date();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_view_stock");
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
        private void bind_grd_view_page_load()
        {
            bind_grd_view("select t1.Stock_id,(select top 1 Session from session_details where session_id=t1.Session_id) as Financial_year,t1.Date,t1.Stock_qnt,isnull((t1.Transfer_qnt),'0') as Transfer_qnt,Stock_qnt-isnull((Transfer_qnt),'0') as Available_qnt,t2.Item_name,t3.Unit_name,t2.Item_id,t2.Unit_id,(select top 1 Material_name from Inventory_Type_Master where Material_id=t2.Material_id) as Material_name,(select top 1 Brand_name from Inventory_brand_master where Brand_id=t2.Brand_id) as Brand_name,Serial_no,Value_rs,Is_warranty_available,Warranty_end_date,Working_status,t2.Modal_no from Inventory_stock_master t1 join Inventory_item_master t2 on t1.Item_id=t2.Item_id join Inventory_unit_master t3 on t2.Unit_id=t3.Unit_id order by t1.Id desc");
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
        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
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

        protected void lnkTransfer_Click(object sender, EventArgs e)
        {
            try
            {


                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_item_name = (Label)row.FindControl("lbl_item_name");
                    Label lbl_available_qnt = (Label)row.FindControl("lbl_available_qnt");
                    Label lbl_unit_name = (Label)row.FindControl("lbl_unit_name");

                    Label lbl_unit_id = (Label)row.FindControl("lbl_unit_id");
                    Label lbl_item_id = (Label)row.FindControl("lbl_item_id");
                    Label lbl_stock_id = (Label)row.FindControl("lbl_stock_id");
                    Label lbl_trnsfr_qnt = (Label)row.FindControl("lbl_trnsfr_qnt");
                    Label lbl_brand_name = (Label)row.FindControl("lbl_brand_name");
                    Label lbl_material = (Label)row.FindControl("lbl_material");
                    Label lbl_serial_no = (Label)row.FindControl("lbl_serial_no");
                    Label lbl_value = (Label)row.FindControl("lbl_value");
                    Label lbl_is_warranty = (Label)row.FindControl("lbl_is_warranty");
                    Label lbl_expire_date = (Label)row.FindControl("lbl_expire_date");
                    Label lbl_working_status = (Label)row.FindControl("lbl_working_status");
                    lbl_itmcode_p.Text = lbl_item_id.Text;
                    lbl_item_name_p.Text = lbl_item_name.Text;
                    lbl_aval_qnt_p.Text = lbl_available_qnt.Text;
                    txt_unit_p.Text = lbl_unit_name.Text;

                    lbl_brand_name_p.Text = lbl_brand_name.Text;
                    lbl_material_type_p.Text = lbl_material.Text;
                    lbl_serial_no_p.Text = lbl_serial_no.Text;

                    ViewState["UnitId"] = lbl_unit_id.Text;
                    ViewState["ItemSid"] = lbl_item_id.Text;
                    // ViewState["GroupidS"] = lbl_group_id.Text;
                    ViewState["AvalQnt"] = lbl_available_qnt.Text;
                    ViewState["prevTrnsfrQnt"] = lbl_trnsfr_qnt.Text;
                    ViewState["ValuerS"] = lbl_value.Text;
                    ViewState["isWarrantY"] = lbl_is_warranty.Text;
                    ViewState["ExpireDatE"] = lbl_expire_date.Text;
                    ViewState["WrkingStatuS"] = lbl_working_status.Text;
                    ViewState["StockID"] = lbl_stock_id.Text;
                    mycode.bind_ddl(ddl_floor, "select Floor_name from  Inventory_floor_master");
                    mycode.bind_ddl(ddl_section_p, "select Section from Inventory_section_master order by Section asc");
                    mycode.bind_all_ddl_with_id(ddl_room_no_p, "Select (Room_name +', (No. : '+ Room_no+')') as Room_name,Rooom_id from Inventory_room_master order by Room_name asc");
                    create_unique_no();
                    myModal2.Visible = true;
                }
                else if (ViewState["Is_add"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_item_name = (Label)row.FindControl("lbl_item_name");
                    Label lbl_available_qnt = (Label)row.FindControl("lbl_available_qnt");
                    Label lbl_unit_name = (Label)row.FindControl("lbl_unit_name");

                    Label lbl_unit_id = (Label)row.FindControl("lbl_unit_id");
                    Label lbl_item_id = (Label)row.FindControl("lbl_item_id");
                    Label lbl_stock_id = (Label)row.FindControl("lbl_stock_id");
                    Label lbl_trnsfr_qnt = (Label)row.FindControl("lbl_trnsfr_qnt");
                    Label lbl_brand_name = (Label)row.FindControl("lbl_brand_name");
                    Label lbl_material = (Label)row.FindControl("lbl_material");
                    Label lbl_serial_no = (Label)row.FindControl("lbl_serial_no");
                    Label lbl_value = (Label)row.FindControl("lbl_value");
                    Label lbl_is_warranty = (Label)row.FindControl("lbl_is_warranty");
                    Label lbl_expire_date = (Label)row.FindControl("lbl_expire_date");
                    Label lbl_working_status = (Label)row.FindControl("lbl_working_status");
                    lbl_itmcode_p.Text = lbl_item_id.Text;
                    lbl_item_name_p.Text = lbl_item_name.Text;
                    lbl_aval_qnt_p.Text = lbl_available_qnt.Text;
                    txt_unit_p.Text = lbl_unit_name.Text;

                    lbl_brand_name_p.Text = lbl_brand_name.Text;
                    lbl_material_type_p.Text = lbl_material.Text;
                    lbl_serial_no_p.Text = lbl_serial_no.Text;

                    ViewState["UnitId"] = lbl_unit_id.Text;
                    ViewState["ItemSid"] = lbl_item_id.Text;
                    // ViewState["GroupidS"] = lbl_group_id.Text;
                    ViewState["AvalQnt"] = lbl_available_qnt.Text;
                    ViewState["prevTrnsfrQnt"] = lbl_trnsfr_qnt.Text;
                    ViewState["ValuerS"] = lbl_value.Text;
                    ViewState["isWarrantY"] = lbl_is_warranty.Text;
                    ViewState["ExpireDatE"] = lbl_expire_date.Text;
                    ViewState["WrkingStatuS"] = lbl_working_status.Text;
                    ViewState["StockID"] = lbl_stock_id.Text;
                    mycode.bind_ddl(ddl_floor, "select Floor_name from  Inventory_floor_master");
                    mycode.bind_ddl(ddl_section_p, "select Section from Inventory_section_master order by Section asc");
                    mycode.bind_all_ddl_with_id(ddl_room_no_p, "Select (Room_name +', (No. : '+ Room_no+')') as Room_name,Rooom_id from Inventory_room_master order by Room_name asc");
                    create_unique_no();
                    myModal2.Visible = true;
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

        private void create_unique_no()
        {
            bool duplicate = true;
            string unqNo = "";
            unqNo = My.create_random_no_otp();
            txt_unique_key.Text = unqNo;
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select Unique_key from Inventory_transfer_master where Unique_key='" + unqNo + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    unqNo = My.create_random_no_otp();
                    txt_unique_key.Text = unqNo;
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }

        protected void btn_transfer_s_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_transfer_date.Text == "")
                {
                    Alertme("Please enter transfer date.", "warning");
                    txt_transfer_date.Focus();
                    myModal2.Visible = true;
                }
                else if (ddl_floor.SelectedItem.Text == "Select")
                {
                    Alertme("Please select floor.", "warning");
                    ddl_floor.Focus();
                    myModal2.Visible = true;
                }
                else if (ddl_room_no_p.SelectedItem.Text == "Select")
                {
                    Alertme("Please select room name.", "warning");
                    ddl_room_no_p.Focus();
                    myModal2.Visible = true;
                }
                else if (ddl_section_p.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddl_section_p.Focus();
                    myModal2.Visible = true;
                }

                else if (My.toDouble(txt_transfer_qnt.Text) <= 0)
                {
                    Alertme("Please enter transfer quantity.", "warning");
                    txt_transfer_qnt.Focus();
                    myModal2.Visible = true;
                }
                else if (txt_transfer_by.Text == "")
                {
                    Alertme("Please enter transfer by.", "warning");
                    txt_transfer_by.Focus();
                    myModal2.Visible = true;
                }
                else if (My.toDouble(ViewState["AvalQnt"].ToString()) < My.toDouble(txt_transfer_qnt.Text))
                {
                    Alertme("Available quantity is insufficient.", "warning");
                    txt_transfer_qnt.Focus();
                    myModal2.Visible = true;
                }
                else
                {
                    save_transfer_data();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_view_stock");
            }
        }

        private void save_transfer_data()
        {
            SqlCommand cmd;
            string query = "INSERT INTO Inventory_transfer_master (Transfer_date,Item_id,Unit_id,Transfer_group_from,Transfer_group_to,Transfer_quantity,Transfer_by,Transfer_idate,Created_by,Created_date,Created_idate,Transfer_time,Session_id,Room_id,Section,Unique_key,Serial_no,Value,Is_warranty,Expire_date,Floor,Working_status) values (@Transfer_date,@Item_id,@Unit_id,@Transfer_group_from,@Transfer_group_to,@Transfer_quantity,@Transfer_by,@Transfer_idate,@Created_by,@Created_date,@Created_idate,@Transfer_time,@Session_id,@Room_id,@Section,@Unique_key,@Serial_no,@Value,@Is_warranty,@Expire_date,@Floor,@Working_status)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Transfer_date", txt_transfer_date.Text);
            cmd.Parameters.AddWithValue("@Transfer_idate", My.DateConvertToIdate(txt_transfer_date.Text));
            cmd.Parameters.AddWithValue("@Item_id", ViewState["ItemSid"].ToString());

            cmd.Parameters.AddWithValue("@Unit_id", ViewState["UnitId"].ToString());

            cmd.Parameters.AddWithValue("@Transfer_group_from", "0");
            cmd.Parameters.AddWithValue("@Transfer_group_to", "0");

            cmd.Parameters.AddWithValue("@Transfer_quantity", txt_transfer_qnt.Text);
            cmd.Parameters.AddWithValue("@Transfer_by", txt_transfer_by.Text);

            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Transfer_time", mycode.time());
            cmd.Parameters.AddWithValue("@Session_id", My.get_session_id());

            cmd.Parameters.AddWithValue("@Room_id", ddl_room_no_p.SelectedValue);
            cmd.Parameters.AddWithValue("@Section", ddl_section_p.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Unique_key", txt_unique_key.Text);
            cmd.Parameters.AddWithValue("@Working_status", ViewState["WrkingStatuS"].ToString());
            cmd.Parameters.AddWithValue("@Floor", ddl_floor.Text);
            cmd.Parameters.AddWithValue("@Serial_no", lbl_serial_no_p.Text);
            cmd.Parameters.AddWithValue("@Value", ViewState["ValuerS"].ToString());
            cmd.Parameters.AddWithValue("@Is_warranty", ViewState["isWarrantY"].ToString());
            cmd.Parameters.AddWithValue("@Expire_date", ViewState["ExpireDatE"].ToString());
            if (My.InsertUpdateData(cmd))
            {
                update_transfer_qnt();
                Alertme("Stock has been transfer successfully.", "success");
                empty_form();

                if (ViewState["flag"].ToString() == "0")
                {
                    bind_grd_view_page_load();
                }
                if (ViewState["flag"].ToString() == "1")
                {
                    find_by_item_code();
                }
                if (ViewState["flag"].ToString() == "2")
                {
                    find_by_item();
                }
            }
        }

        private void update_transfer_qnt()
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Inventory_stock_master where Stock_id='" + ViewState["StockID"].ToString() + "'", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Typewise_fee_collection");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
            {
                int ctransfr_qnt = My.toint(txt_transfer_qnt.Text);
                foreach (DataRow dr in dt.Rows)
                {
                    if (ctransfr_qnt > 0)
                    {
                        string stock_qnt = dr["Stock_qnt"].ToString();
                        string transferqnt = dr["Transfer_qnt"].ToString();
                        //======
                        int avalQnts = (My.toint(stock_qnt) - My.toint(transferqnt));

                        if (My.toint(avalQnts) < ctransfr_qnt)
                        {
                            if (avalQnts > 0)
                            {
                                dr["Transfer_qnt"] = avalQnts;
                                ctransfr_qnt = ctransfr_qnt - avalQnts;
                            }
                        }
                        else
                        {
                            int fnl_t_qnt = My.toint(transferqnt) + ctransfr_qnt;
                            dr["Transfer_qnt"] = fnl_t_qnt;
                            ctransfr_qnt = 0;
                        }
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }

        private void empty_form()
        {
            myModal2.Visible = false;
            txt_transfer_qnt.Text = "";
            txt_transfer_by.Text = "";
        }

        protected void btn_find_by_group_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_item_code_fnd.Text == "")
                {
                    Alertme("Please enter item code.", "warning");
                    txt_item_code_fnd.Focus();
                }
                else
                {
                    find_by_item_code();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_item_code()
        {
            bind_grd_view("select t1.Stock_id,(select top 1 Session from session_details where session_id=t1.Session_id) as Financial_year,t1.Date,t1.Stock_qnt,isnull((t1.Transfer_qnt),'0') as Transfer_qnt,Stock_qnt-isnull((Transfer_qnt),'0') as Available_qnt,t2.Item_name,t3.Unit_name,t2.Item_id,t2.Unit_id,(select top 1 Material_name from Inventory_Type_Master where Material_id=t2.Material_id) as Material_name,(select top 1 Brand_name from Inventory_brand_master where Brand_id=t2.Brand_id) as Brand_name,Serial_no,Value_rs,Is_warranty_available,Warranty_end_date,Working_status,t2.Modal_no from Inventory_stock_master t1 join Inventory_item_master t2 on t1.Item_id=t2.Item_id join Inventory_unit_master t3 on t2.Unit_id=t3.Unit_id where t2.Item_id='" + txt_item_code_fnd.Text + "' order by t1.Id desc");
        }

        protected void btn_by_item_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_item_name.Text == "")
                {
                    Alertme("Please enter item name.", "warning");
                    txt_item_name.Focus();
                }
                else
                {
                    find_by_item();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_item()
        {
            bind_grd_view("select t1.Stock_id,(select top 1 Session from session_details where session_id=t1.Session_id) as Financial_year,t1.Date,t1.Stock_qnt,isnull((t1.Transfer_qnt),'0') as Transfer_qnt,Stock_qnt-isnull((Transfer_qnt),'0') as Available_qnt,t2.Item_name,t3.Unit_name,t2.Item_id,t2.Unit_id,(select top 1 Material_name from Inventory_Type_Master where Material_id=t2.Material_id) as Material_name,(select top 1 Brand_name from Inventory_brand_master where Brand_id=t2.Brand_id) as Brand_name,Serial_no,Value_rs,Is_warranty_available,Warranty_end_date,Working_status,t2.Modal_no from Inventory_stock_master t1 join Inventory_item_master t2 on t1.Item_id=t2.Item_id join Inventory_unit_master t3 on t2.Unit_id=t3.Unit_id where t2.Item_name='" + txt_item_name.Text + "' order by t1.Id desc");
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
                    cmd.CommandText = "select DISTINCT Item_name from Inventory_item_master where Item_name LIKE ''+@SearchMobNo+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["Item_name"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }


        [WebMethod]
        public static List<string> GetRooPathAdmNo(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select DISTINCT Item_id from Inventory_transfer_master where Item_id LIKE ''+@SearchMobNo+'%'";
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