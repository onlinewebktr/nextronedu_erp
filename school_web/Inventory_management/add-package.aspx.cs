using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class add_package : System.Web.UI.Page
    {
        My mycode = new My();
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
                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["sessionid"] = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_unit, "select Unit,unit_id from dbo.[unit_master] where firm ='" + My.firm_id() + "'");
                    mycode.bind_all_ddl_with_id(ddl_class_name, "select Course_Name,course_id from dbo.[Add_course_table] order by Position asc  ");

                    try
                    {
                        if (Request.QueryString["pck"] != null)
                        {
                            string pck = Request.QueryString["pck"];
                            ViewState["unique_entry_id"] = pck;
                            

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
            DataTable dt = My.dataTable(" select * from dbo.[HMS_Package_Summary] where unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "'   ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                btn_submit.Text = "Update";
                txt_packagename.Text = dt.Rows[0]["Package_Name"].ToString();
                ddl_class_name.SelectedValue= dt.Rows[0]["Class_id"].ToString();
                Bind_item_package();
            }
        }

        protected void txt_Item_TextChanged(object sender, EventArgs e)
        {


            try { find_item_details(); }
            catch (Exception ex)
            { My.submitexception(ex.ToString()); }
        }
        private void find_item_details()
        {
            DataTable dt = My.dataTable(" select * from dbo.[HMS_Invetory_item_Master] where Item_Name='" + txt_Item.Text + "'   ");
            if (dt.Rows.Count == 0)
            {

                ddl_unit.SelectedValue = "0";

                txt_qty.Text = "0";



            }
            else
            {
                DataRow dr = dt.Rows[0];
                hd_itemcode.Value = dr["Item_id"].ToString(); ;
                ddl_unit.SelectedValue = dr["Unit_id"].ToString();

            }
        }
        #region add and edi delete
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_qty = (Label)row.FindControl("lbl_qty");
                Label lbl_Unit_id = (Label)row.FindControl("lbl_Unit_id");
                Label lbl_Item_name = (Label)row.FindControl("lbl_Item_name");
                Btn_Add.Text = "Update";
                txt_Item.Text = lbl_Item_name.Text;
                find_item_details();
                ViewState["Id"] = lbl_Id.Text;
                txt_qty.Text = lbl_qty.Text;
                ddl_unit.SelectedValue = lbl_Unit_id.Text;
            }
            catch
            {

            }



        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                mycode.executequery("delete from HMS_Package_Item_Wise where Id=" + lbl_Id.Text + "");
                Bind_item_package();
            }
            catch
            {

            }
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("add-package.aspx", false);
        }

        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            if (txt_Item.Text == "")
            {
                Alertme("Please enter item", "warning");
                return;
            }
            if (ddl_unit.SelectedItem.Text == "Select")
            {
                Alertme("Please select unit name", "warning");
                return;
            }
            if (txt_qty.Text == "" || Convert.ToDouble(txt_qty.Text) == 0)
            {
                Alertme("Please Enter quantity", "warning");
                txt_qty.Focus();

            }
            else
            {
                if (Btn_Add.Text == "Add")
                {
                    insert_data();
                }
                else
                {
                    insert_data();
                }

            }

        }

        private void insert_data()
        {
            string query = "";
            double itemrate = Get_item_rate();
            double totalrate = itemrate * Convert.ToInt32(txt_qty.Text);
            SqlCommand cmd;
            if (ViewState["unique_entry_id"] == null)
            {
                ViewState["unique_entry_id"] = My.unique_id();
            }
            DataTable dt1 = mycode.FillData("Select * from HMS_Package_Item_Wise where Item_Code='" + hd_itemcode.Value + "'  and Status='Pending' and  unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "'  and Unit_id='"+ ddl_unit.SelectedValue + "'    ");
            if (dt1.Rows.Count == 0)
            {
                query = "INSERT INTO HMS_Package_Item_Wise (Item_Code,Quantity,Rate,Total_Rate,unique_entry_id,Status,user_id,Date,Unit_id) values (@Item_Code,@Quantity,@Rate,@Total_Rate,@unique_entry_id,@Status,@user_id,@Date,@Unit_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Item_Code", hd_itemcode.Value);
                cmd.Parameters.AddWithValue("@Quantity", txt_qty.Text);
                cmd.Parameters.AddWithValue("@Rate", itemrate.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Total_Rate", totalrate);
                cmd.Parameters.AddWithValue("@unique_entry_id", ViewState["unique_entry_id"].ToString());
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", My.getdate1());
                cmd.Parameters.AddWithValue("@Unit_id", ddl_unit.SelectedValue);
                if (My.InsertUpdateData(cmd))
                {
                    empty_data();
                }
            }
            else
            {
                query = "Update HMS_Package_Item_Wise set Quantity=@Quantity,Rate=@Rate,Total_Rate=@Total_Rate,user_id=@user_id,Date=@Date where Item_Code=@Item_Code and Unit_id='"+ ddl_unit.SelectedValue + "' and unique_entry_id=@unique_entry_id ";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Item_Code", hd_itemcode.Value);
                cmd.Parameters.AddWithValue("@Quantity", txt_qty.Text);
                cmd.Parameters.AddWithValue("@Rate", itemrate.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Total_Rate", totalrate);
                cmd.Parameters.AddWithValue("@unique_entry_id", ViewState["unique_entry_id"].ToString());
                cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Unit_id", ddl_unit.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", My.getdate1());
                if (My.InsertUpdateData(cmd))
                {
                    empty_data();
                }


            }

        }

        private void empty_data()
        {
            txt_qty.Text = "";
            ddl_unit.SelectedValue = "0";
            txt_Item.Text = "";
            Btn_Add.Text = "Add";
            Bind_item_package();
        }

        private void Bind_item_package()
        {
            double Total_Rate = 0.00;
            DataTable dt1 = mycode.FillData("Select *,(select top 1 Item_Name from HMS_Invetory_item_Master where Item_id=HMS_Package_Item_Wise.Item_Code and Unit_id=HMS_Package_Item_Wise.Unit_id) as itemname,(select top 1 Unit from unit_master where  Unit_id=HMS_Package_Item_Wise.Unit_id) as Unitname from HMS_Package_Item_Wise where  unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "'     ");
            if (dt1.Rows.Count == 0)
            {
                grd_view.DataSource = null;
                grd_view.DataBind();
                finaltotal.Visible = false;
            }
            else
            {
                grd_view.DataSource = dt1;
                grd_view.DataBind();
                finaltotal.Visible = true;

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    Total_Rate = Total_Rate + My.toDouble(dt1.Rows[i]["Total_Rate"].ToString());
                }

                ViewState["Package_Amount"] = Total_Rate.ToString("0.00");
            }
        }

        private double Get_item_rate()
        {
            string query = "Select Sale_rate from HMS_Inventory_stock_details where Item_Code='" + hd_itemcode.Value + "' and Unit_id='" + ddl_unit.SelectedValue + "'";
            DataTable dt1 = mycode.FillData(query);
            if (dt1.Rows.Count == 0)
            {
                return 0;
            }
            else
            {

                return My.toDouble(dt1.Rows[0]["Sale_rate"].ToString());

            }

        }
        #endregion



        #region auto text box
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_inventory_item(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select distinct im.Item_Name  from dbo.[HMS_Invetory_item_Master] im join  HMS_Inventory_stock_details isd on isd.Item_Code=im.Item_id  where im.Item_Name     LIKE ''+@SearchprojectName+'%' order by im.Item_Name asc";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Item_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
        #endregion
        double total_amount = 0;
        protected void grd_view_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Net_total = (Label)e.Row.FindControl("lbl_Net_total");
                total_amount = total_amount + My.toDouble(lbl_Net_total.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_amount = (Label)e.Row.FindControl("lbl_total_amount");
                lbl_total_amount.Text = total_amount.ToString("0.00");
            }
        }

        #region final submit
        protected void btn_submit_Click(object sender, EventArgs e)
        {

            if (txt_packagename.Text == "")
            {
                Alertme("Please package name", "warning");
                txt_packagename.Focus();

            }
            else if (ddl_class_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select class name", "warning");
                txt_packagename.Focus();
            }

            else
            {
                if (btn_submit.Text == "Final Submit")
                {
                    SqlCommand cmd;
                    string query = "Select * from HMS_Package_Summary where Package_Name='" + txt_packagename.Text.Trim() + "' and Class_id='" + ddl_class_name.SelectedValue + "' and Session_id='" + ViewState["sessionid"].ToString() + "' ";
                    DataTable dt1 = mycode.FillData(query);
                    if (dt1.Rows.Count == 0)
                    {

                        if (grd_view.Rows.Count == 0)
                        {
                            Alertme("Please add item in the package before you can final submit.", "warning");

                        }
                        else
                        {
                            query = "INSERT INTO HMS_Package_Summary (unique_entry_id,Class_id,Package_Name,Package_Amount,user_id,Date,Session_id) values (@unique_entry_id,@Class_id,@Package_Name,@Package_Amount,@user_id,@Date,@Session_id)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@unique_entry_id", ViewState["unique_entry_id"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", ddl_class_name.SelectedValue);
                            cmd.Parameters.AddWithValue("@Package_Name", txt_packagename.Text.Trim());
                            cmd.Parameters.AddWithValue("@Package_Amount", ViewState["Package_Amount"].ToString());
                            cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Date", My.getdate1());
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());

                            if (My.InsertUpdateData(cmd))
                            {
                                mycode.executequery("update HMS_Package_Item_Wise set Status='Submited' where unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "'");
                                Alertme("Record has been saved successfully", "success");
                                empty_data_final();
                            }

                            
                        }
                    }
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Select * from HMS_Package_Summary where Package_Name='" + txt_packagename.Text.Trim() + "' and Class_id='" + ddl_class_name.SelectedValue + "' and Session_id='" + ViewState["sessionid"].ToString() + "' and unique_entry_id!='" + ViewState["unique_entry_id"].ToString() + "' ";
                    DataTable dt1 = mycode.FillData(query);
                    if (dt1.Rows.Count == 0)
                    {
                        query = "Update HMS_Package_Summary set Class_id=@Class_id,Package_Name=@Package_Name,Package_Amount=@Package_Amount,user_id=@user_id,Date=@Date where unique_entry_id = @unique_entry_id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@unique_entry_id", ViewState["unique_entry_id"].ToString());
                        cmd.Parameters.AddWithValue("@Class_id", ddl_class_name.SelectedValue);
                        cmd.Parameters.AddWithValue("@Package_Name", txt_packagename.Text.Trim());
                        cmd.Parameters.AddWithValue("@Package_Amount", ViewState["Package_Amount"].ToString());
                        cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Date", My.getdate1());
                        if (My.InsertUpdateData(cmd))
                        {
                            mycode.executequery("update HMS_Package_Item_Wise set Status='Submited' where unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "'");
                            Alertme("Record has been saved successfully", "success");
                            Session["msg1"] = "Record has been updated successfully";
                            Response.Redirect("package-list.aspx");
                            empty_data_final();
                        }

                    }
                    else
                    {

                    }
                }
            }
        }

        private void empty_data_final()
        {
            ddl_class_name.SelectedValue = "0";
            txt_packagename.Text = "";
            ViewState["unique_entry_id"] = My.unique_id();
            btn_submit.Text = "Final Submit";
            Bind_item_package();
        }
        #endregion
    }
}