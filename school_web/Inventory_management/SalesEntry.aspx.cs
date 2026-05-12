using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class SalesEntry : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["userType"] = "Admin";
            //Session["Admin"] = "edunext2021";
            //Session["name"] = "Admin";
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
                    ViewState["OnlineFromAPP"] = "No";
                    lbl_msg2.Text = "";
                    ViewState["Session_id"] = "0";
                    ViewState["avlwallet"] = "0.00";
                    txt_mobileno.Text = "";
                    ViewState["Package_id"] = "";
                    Session["billfrom"] = "1";
                    ViewState["unique_entry_id"] = "";
                    ViewState["order_id"] = "";
                    ViewState["college_name"] = My.get_short_school_name();
                    hd_session_id.Value = My.get_session_id();
                    txt_temp_bill_no.Text = "temp" + My.get_session() + "_" + My.session_wise_auto_serial("Temp_bill_no");
                    lbl_prevsoldbillno.Text = get_last_bill();
                    ViewState["Admin"] = Session["Admin"].ToString();
                    ViewState["User_Name"] = Session["Admin"].ToString();
                    ViewState["Userid"] = Session["Admin"].ToString();

                    Dictionary<string, object> dc1 = mycode.Firm_details();
                    ViewState["state_code"] = (String)dc1["State_code"];
                    ViewState["state_Name"] = (String)dc1["State"];
                    txt_payment_date.Text = mycode.date();
                    hd_saletype.Value = "Item";
                    btn_add_item.Visible = true;
                    mycode.bind_all_ddl_with_id(ddl_classname, "Select   cd.Course_Name,cd.course_id from Add_course_table cd  order by cd.Position asc");
                    txt_float_dis_count.Text = "0.00";
                    // online order process
                    try
                    {
                        string appbillno = Request.QueryString["appbillno"];
                        string party_id = Request.QueryString["user_id"];
                        if (!string.IsNullOrEmpty(appbillno))
                        {
                            txt_trnaction_no.Text = My.get_app_purchase_onlinetran(appbillno, party_id);
                            btn_add_item.Visible = false;

                            txt_payment_date.Text = My.get_app_purchase_date(appbillno, party_id);
                            ViewState["OnlineFromAPP"] = "Yes";
                            ViewState["party_id"] = party_id;
                            ViewState["appbillno"] = appbillno;
                            Dictionary<string, object> dc2 = My.get_selected_studentinfo(party_id, My.get_session_id(), "1");
                            string classname = (String)dc2["classname"];
                            string rollnumber = (String)dc2["rollnumber"];
                            string Section = (String)dc2["Section"];
                            ViewState["Session_id"] = (String)dc2["Session_id"];
                            string qry = @"select *  from party_details where party_id='" + party_id + "' and type='Customer'";
                            DataTable dt = My.dataTable(qry);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {


                                    txt_seal_to.Text = dt.Rows[0]["party_name"].ToString() + " ,Class-NA ,Sec.-NA , Customer Id-" + party_id;


                                    txt_mobileno.Text = dr["mobile"].ToString();
                                    hd_party_id.Value = dt.Rows[0]["party_id"].ToString();
                                    txt_address.Text = dr["address"].ToString() + "," + dr["State"].ToString() + " Mobile:-" + dr["mobile"].ToString();
                                    ViewState["party_name"] = dr["party_name"].ToString();
                                    ViewState["avlwallet"] = "0.00";
                                    txt_avl_Wallet.Text = ViewState["avlwallet"].ToString();
                                }

                            }
                            else
                            {
                                ViewState["party_id"] = "";
                            }
                            Bind_item_by_order_app();
                        }

                    }
                    catch (Exception ex)
                    {

                    }


                }
            }
        }

        private void Bind_item_by_order_app()
        {



            ViewState["unique_entry_id"] = My.unique_id();


            string query = "Select *,(select top 1 Item_Name from HMS_Invetory_item_Master where Item_id=osi.Item_Code and Unit_id=osi.Unit_id) as itemname from Online_Sell_item_wise osi where osi.Bill_No='" + ViewState["appbillno"].ToString() + "' and Sell_To='" + ViewState["party_id"].ToString() + "' and osi.Status='Pending'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Item_Code = dt.Rows[i]["Item_Code"].ToString();
                    string Quantity = dt.Rows[i]["Quantity"].ToString();
                    string Unit_id = dt.Rows[i]["Unit_id"].ToString();
                    string Rate = dt.Rows[i]["Rate"].ToString();
                    string itemname = dt.Rows[i]["itemname"].ToString();
                    string store_id = "2001";
                    string Package_id = ViewState["appbillno"].ToString();
                    add_item_via_package(Item_Code, Unit_id, Quantity, Package_id, Rate, store_id, itemname);
                }

            }
            bind_grid_view(ViewState["unique_entry_id"].ToString());
            empty_form();
        }

        private string get_last_bill()
        {
            string qry = @"select top 1 Bill_No  from HMS_Invetory_Sell_details_billwise order by id desc";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Bill_No"].ToString();

            }
        }

        #region WebMethoD get user
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT)
        {

            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select(party_name + ', Mob.-' + mobile + ',' + party_id) as party_name  from party_details where type = 'Customer' and (party_name like '%'+@SearchprojectName+'%' or party_id  like '%'+@SearchprojectName+'%') and party_id in (select admissionserialnumber from admission_registor where Status='1' and Transfer_Status in('NT','New'))";
                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["party_name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }




        }
        protected void txt_seal_to_TextChanged(object sender, EventArgs e)
        {

            ViewState["party_id"] = "";
            try
            {
                string seal_to = txt_seal_to.Text;
                string[] stringSeparatorss = new string[] { "," };
                string[] arrs = seal_to.Split(stringSeparatorss, StringSplitOptions.None);
                string studentname = arrs[0];
                string mobile = arrs[1];
                string party_id = arrs[2];
                ViewState["party_id"] = party_id;


                Dictionary<string, object> dc1 = My.get_selected_studentinfo(party_id, My.get_session_id(), "1");
                string classname = (String)dc1["classname"];
                string rollnumber = (String)dc1["rollnumber"];
                string Section = (String)dc1["Section"];
                ViewState["Session_id"] = (String)dc1["Session_id"];
                string qry = @"select *  from party_details where party_id='" + party_id + "' and type='Customer'";
                DataTable dt = My.dataTable(qry);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        txt_seal_to.Text = dr["party_name"].ToString() + ", Class: " + classname + " Section: " + Section + " Adm. No. : " + party_id;
                        txt_mobileno.Text = dr["mobile"].ToString();
                        hd_party_id.Value = dt.Rows[0]["party_id"].ToString();
                        txt_address.Text = dr["address"].ToString() + "," + dr["State"].ToString() + " Mobile:-" + dr["mobile"].ToString();
                        ViewState["party_name"] = dr["party_name"].ToString();

                    }


                    try
                    {
                        DataTable dt1 = mycode.FillData(" Select (sum(convert(float, isnull((Wallet_input_amount),'0'))) -sum(convert(float, isnull((Wallet_Out_amount),'0')))) as avlwallet from Student_Wallet where Adm_no='" + party_id + "'");
                        if (dt1.Rows.Count == 0)
                        {
                            ViewState["avlwallet"] = "0.00";

                        }
                        else
                        {

                            ViewState["avlwallet"] = My.toDouble(dt1.Rows[0]["avlwallet"].ToString());
                        }
                    }
                    catch
                    {
                        ViewState["avlwallet"] = "0.00";

                    }

                    txt_avl_Wallet.Text = ViewState["avlwallet"].ToString();


                }
                else
                {
                    ViewState["party_id"] = "";
                }
            }
            catch
            {
                ViewState["party_id"] = "";

            }

        }
        [WebMethod]
        public static List<string> Getitem(string PathRooT, string saletype, string session_id)
        {
            string qry = "";
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {


                    if (saletype == "Item")
                    {
                        qry = @" select(Item_Name + ', Brand-' + ( select  top 1 Brand_name  from dbo.[HMS_Invetory_Brand_Master] where Brand_id=HMS_Invetory_item_Master.Brand_id )+','+Item_id) as itemname  from HMS_Invetory_item_Master where   Item_Name like '%'+@SearchprojectName+'%' ";
                    }
                    else
                    {
                        qry = @" select Package_Name as itemname  from HMS_Package_Summary where   Package_Name like '%'+@SearchprojectName+'%' and Session_id='" + session_id + "' ";
                    }



                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["itemname"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }



        }

        #endregion
        string scrpt;
        private void Alertme(string msg, string panel)
        {
            /*
                        if (type.ToLower() == "success")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", @"
                alertSuccess('Customer information has been saved successfully.');
            ", true);
                            *//* ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", @"
                     Swal.close();

                     setTimeout(function () {

                         alertSuccess('" + message + @"');

                     }, 300);
                 ", true);*//*
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", @"
                    Swal.close();

                    setTimeout(function () {

                        alertError('" + message + @"');

                    }, 300);
                ", true);
                        }
            */



            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(3000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel.ToLower() == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel.ToLower() == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;

            }

        }

        protected void txt_item_Descriptin_TextChanged(object sender, EventArgs e)
        {
            try

            {
                Panel1.Visible = false;
                Panel2.Visible = false;
                rp_std.DataSource = null;
                rp_std.DataBind();
                Repeater1.DataSource = null;
                Repeater1.DataBind();

                if (txt_item_Descriptin.Text == "")
                {
                    Alertme("Please enter item name", "warning");
                }
                else
                {
                    if (hd_saletype.Value == "Item")
                    {

                        string seal_to = txt_item_Descriptin.Text;
                        string[] stringSeparatorss = new string[] { "," };
                        string[] arrs = seal_to.Split(stringSeparatorss, StringSplitOptions.None);
                        string itemname = arrs[0];
                        string brand = arrs[1];
                        string itemid = arrs[2];
                        txt_selected_item.Text = itemname;
                        string qry = @"select *,(select  top 1 Brand_name  from dbo.[HMS_Invetory_Brand_Master] where Brand_id=HMS_Inventory_stock_details.Brand_Id) as Brand_name,('" + itemname + "') as itemname,(select  top 1 Unit  from dbo.[unit_master] where unit_id=HMS_Inventory_stock_details.Unit_id) as Unit  from HMS_Inventory_stock_details where Item_Code='" + itemid + "'";
                        DataTable dt = My.dataTable(qry);
                        if (dt.Rows.Count == 0)
                        {

                            Alertme("Please first add purchase items after that you will be starting billing", "warning");


                            //}
                        }
                        else
                        {
                            Panel1.Visible = true;
                            Panel2.Visible = false;
                            rp_std.DataSource = dt;
                            rp_std.DataBind();
                            Repeater1.DataSource = null;
                            Repeater1.DataBind();
                            myModal2.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_details();", true);

                        }
                    }
                    else
                    {

                        string get_class_id = My.get_class_id_from_student_id(hd_party_id.Value, hd_session_id.Value);
                        string query = " select  ps.*,ct.Course_Name,(Select top 1 Session from session_details where session_id = ps.Session_id) as Session from dbo.[HMS_Package_Summary] ps join Add_course_table ct on ct.course_id=ps.Class_id where ps.Session_id='" + hd_session_id.Value + "' and Class_id='" + get_class_id + "' and  ps.Package_Name like'%" + txt_item_Descriptin.Text + "%'  ";



                        DataTable dt = My.dataTable(query);
                        if (dt.Rows.Count == 0)
                        {

                            Alertme("Please first add purchase items after that you will be starting billing", "warning");


                            //}
                        }
                        else
                        {
                            Panel1.Visible = false;
                            Panel2.Visible = true;
                            rp_std.DataSource = null;
                            rp_std.DataBind();

                            Repeater1.DataSource = dt;
                            Repeater1.DataBind();
                            myModal2.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_details();", true);

                        }

                    }


                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }

        }

        #region item select
        protected void lnk_select_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Store_id = (Label)row.FindControl("lbl_Store_id");
            Label lbl_Stock_id = (Label)row.FindControl("lbl_Stock_id");
            Label lbl_Item_Code = (Label)row.FindControl("lbl_Item_Code");
            Label lbl_Quantity = (Label)row.FindControl("lbl_Quantity");
            Label lbl_HSN = (Label)row.FindControl("lbl_HSN");
            Label lbl_Sale_rate = (Label)row.FindControl("lbl_Sale_rate");
            Label lbl_Unit = (Label)row.FindControl("lbl_Unit");
            Label lbl_GST_Percent = (Label)row.FindControl("lbl_GST_Percent");
            Label lbl_Unit_id = (Label)row.FindControl("lbl_Unit_id");
            Label lbl_Expiry_date = (Label)row.FindControl("lbl_Expiry_date");
            Label lbl_Batch_no = (Label)row.FindControl("lbl_Batch_no");
            Label lbl_Brand_Id = (Label)row.FindControl("lbl_Brand_Id");

            Label lbl_stock_item_unique_entry_id = (Label)row.FindControl("lbl_stock_item_unique_entry_id");
            ViewState["stock_item_unique_entry_id"] = lbl_stock_item_unique_entry_id.Text;


            Label lbl_Purchase_Rate = (Label)row.FindControl("lbl_Purchase_Rate");
            ViewState["Brand_Id"] = lbl_Brand_Id.Text;
            ViewState["Purchase_Rate"] = lbl_Purchase_Rate.Text;
            txt_gst_per.Text = lbl_GST_Percent.Text;
            ViewState["Store_id"] = lbl_Store_id.Text;
            ViewState["Stock_id"] = lbl_Stock_id.Text;
            ViewState["Item_Code"] = lbl_Item_Code.Text;
            ViewState["Item_HSN"] = lbl_HSN.Text;
            ViewState["Unit_id"] = lbl_Unit_id.Text;
            ViewState["Expiry_date"] = lbl_Expiry_date.Text;
            ViewState["batch_no"] = lbl_Batch_no.Text;
            txt_unit.Text = lbl_Unit.Text;
            txt_avl_stock.Text = lbl_Quantity.Text;
            txt_qty.Text = "1";
            txt_discount.Text = "0";
            txt_dis_percentage.Text = "0";
            txt_rate.Text = My.toDouble(lbl_Sale_rate.Text).ToString("0.00");
            calculate_row();
            myModal2.Visible = false;
        }
        protected void lnk_close_Click(object sender, EventArgs e)
        {

            myModal2.Visible = false;

        }

        #endregion

        #region qty   on changed
        protected void txt_qty_TextChanged(object sender, EventArgs e)
        {


            calculate_row();

        }

        private void calculate_row()
        {
            if (txt_qty.Text == "")
            {
                txt_qty.Text = "0";
                txt_total.Text = "0.00";
            }
            else if (txt_qty.Text == "0")
            {
                txt_qty.Text = "0";
                txt_total.Text = "0.00";
            }

            int inputeqty = My.toIntS(txt_qty.Text);



            double dis_percent = 0;
            double discount = 0;


            int avl_qty = My.toIntS(txt_qty.Text);
            try
            {
                int ablqty = Convert.ToInt32(txt_avl_stock.Text);

                if (ablqty >= inputeqty)
                {
                    txt_total.Text = (My.toDouble(txt_qty.Text) * My.toDouble(txt_rate.Text)).ToString("0.00");

                    if (txt_dis_percentage.Text != "")
                    {
                        dis_percent = My.toDouble(txt_dis_percentage.Text);
                    }

                    double total = My.toDouble(txt_total.Text);
                    double qty = My.toDouble(txt_qty.Text);
                    double rate = My.toDouble(txt_rate.Text);
                    double tax_percent = 0;
                    if (qty == 0)
                    {
                        qty = 1;
                    }

                    double discounted_amt = total;
                    if (dis_percent > 0)
                    {
                        discount = My.Round(discounted_amt * dis_percent / 100);
                    }
                    else
                    {
                        if (txt_dis_amount.Text != "")
                        {

                            discount = My.toDouble(txt_dis_amount.Text);
                        }
                    }
                    txt_dis_amount.Text = discount.ToString("0.00");

                    double taxable = total - (discount);
                    txt_taxbalevalue.Text = taxable.ToString("0.00");
                    tax_percent = My.toDouble(txt_gst_per.Text);

                    double total_tax = My.Round(taxable * tax_percent / 100);



                    ViewState["taxable_rate"] = My.Round(taxable / qty);
                    txt_gstvalue.Text = total_tax.ToString("0.00");

                    double net = taxable + total_tax;
                    txt_net_total.Text = net.ToString("0.00");
                }
                else
                {
                    Alertme("oops! please enter valid quantity", "warning");

                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }




        }
        #endregion

        protected void txt_dis_percentage_TextChanged(object sender, EventArgs e)
        {
            calculate_row();
        }

        #region add item
        protected void btn_add_item_Click(object sender, EventArgs e)
        {
            if (ViewState["party_id"] == null)
            {
                Alertme("Please enter valid sale to user", "warning");
                txt_seal_to.Focus();
                return;
            }
            if (ViewState["party_id"].ToString() == "")
            {
                Alertme("Please enter valid user or something is wrong for your selected user, Please refresh page and add again product", "warning");
                txt_seal_to.Focus();
                return;
            }
            if (My.toDouble(txt_dis_percentage.Text) > 100)
            {
                Alertme("Please Enter discount less than 100%", "warning");

                txt_dis_percentage.Focus();
                return;
            }
            //if (My.toDouble(txt_rate.Text) > 0)
            //{
            //    Alertme("Please Enter Valid Rate", "warning");
            //    txt_rate.Focus();
            //    return;
            //}

            if (My.toDouble(txt_qty.Text) == 0)
            {
                Alertme("Please Enter Valid Quantity", "warning");

                txt_qty.Focus();
                return;
            }

            if (ViewState["unique_entry_id"].ToString() == "")
            {
                ViewState["unique_entry_id"] = My.unique_id();
            }

            if (btn_add_item.Text == "Add")
            {
                add_item();
            }
            else
            {
                update_item();

            }

            bind_grid_view(ViewState["unique_entry_id"].ToString());

            empty_form();

        }

        private void update_item()
        {
            int old_qty = 0;
            calculate_row();
            if (ViewState["order_id"].ToString() == "")
            {
                ViewState["order_id"] = My.auto_serialS("order_id");
            }

            SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_Invetory_Sell_details_item_wise where id='" + ViewState["id"] + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Sell_details_item_wise");
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                old_qty = My.toInt(dr["Quantity"]);
                dr["Barcode"] = "";
                dr["Brand_Id"] = ViewState["Brand_Id"].ToString();

                dr["Rate"] = My.toDouble(txt_rate.Text).ToString("0.00");
                dr["taxable_rate"] = My.toDouble(txt_taxbalevalue.Text).ToString("0.00");
                dr["rate_before_discount"] = My.toDouble(txt_total.Text).ToString("0.00"); ;
                dr["Quantity"] = txt_qty.Text;
                dr["Unit_id"] = ViewState["Unit_id"].ToString();
                dr["Total"] = txt_total.Text;
                dr["sale_service_type"] = "Seal";

                dr["Branch_Id"] = "1";
                if (My.toDouble(txt_dis_percentage.Text) > 0)
                {
                    dr["Discount"] = My.toDouble(txt_dis_amount.Text);
                    dr["Discount_Type"] = "Percent";
                    dr["Discount_Percent"] = txt_dis_percentage.Text;
                }
                else
                {
                    dr["Discount"] = My.toDouble(txt_dis_amount.Text);
                    dr["Discount_Type"] = "flat";
                    dr["Discount_Percent"] = "0";
                }

                dr["Taxable"] = My.toDouble(txt_taxbalevalue.Text);
                dr["Total_GST"] = My.toDouble(txt_gstvalue.Text);
                //if (ViewState["state_code"].ToString() == My.state_code)
                //{
                dr["SGST"] = My.Round(My.toDouble(txt_gstvalue.Text) / 2);
                dr["CGST"] = My.Round(My.toDouble(txt_gstvalue.Text) / 2);
                dr["IGST"] = "0";
                //}
                //else
                //{
                //    dr["IGST"] = txt_gst_value.Text;
                //    dr["SGST"] = "0";
                //    dr["CGST"] = "0";
                //}
                // }
                dr["NetTotal"] = My.toDouble(txt_net_total.Text);
                //if (rbt_exclusive.IsChecked == true)
                //{
                dr["TaxCalculationType"] = "Exclusive";
                //}
                //else
                //{
                //    dr["TaxCalculationType"] = "Inclusive";
                //}
                dr["Bill_No"] = txt_temp_bill_no.Text;
                dr["Sell_To"] = ViewState["party_id"].ToString();
                dr["Mobile_no"] = txt_mobileno.Text;
                dr["GST_Percent"] = txt_gst_per.Text;//is_ipd_bill ? "0" : txt_gst_applied.Text;
                dr["Date"] = My.toDateTime(txt_payment_date.Text + " " + mycode.time()).ToString("dd/MMM/yyyy hh:mm:ss tt");
                dr["Idate"] = My.toDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                dr["HSN_Code"] = ViewState["Item_HSN"].ToString();
                dr["Status"] = "Pending";
                dr["firm"] = My.firm_id();
                dr["session"] = My.get_session();
                dr["user_id"] = ViewState["Admin"].ToString();
                dr["unique_entry_id"] = ViewState["unique_entry_id"].ToString();
                dr["sale_type"] = "Stock";
                dr["GST_type"] = txt_gst_per.Text;//
                dr["cess_percent"] = "0";//My.toDouble(txt_cess_applied.Text);
                dr["cess_value"] = "0"; //My.toDouble(txt_cess_value.Text);
                dr["item_type"] = "Goods";
                dr["State_Code"] = ViewState["state_code"].ToString();
                dr["State"] = ViewState["state_Name"].ToString();

                dr["Batch_No"] = ViewState["batch_no"].ToString();
                dr["Exp_Date"] = ViewState["Expiry_date"].ToString();
                dr["Manu_Date"] = "";
                dr["salesman_id"] = ViewState["Admin"].ToString();
                dr["size"] = "";
                dr["imei_no"] = "";
                dr["imei_no2"] = "";
                dr["mapping_id"] = "0";
                dr["product_serial"] = "";
                dr["mrp"] = My.toDouble(txt_rate.Text).ToString("0.00");

                //if (My.bussiness_nature == "Medical Store")
                //{
                //    try
                //    {
                //        dr["Doctor_name"] = txt_doctor_name.Text;
                //        dr["Doctor_id"] = txt_doctor_name.SelectedValue.ToString();
                //    }
                //    catch
                //    {
                //        dr["Doctor_name"] = "Self";
                //        dr["Doctor_id"] = "self";
                //    }
                //}
                dr["Godown_id"] = ViewState["Store_id"].ToString();
                dr["table_id"] = "0";
                dr["order_id"] = ViewState["order_id"];
                dr["s_qty"] = "0";
                dr["f_qty"] = "0";
                dr["scheme_in_per"] = "0";//My.toDouble(txt_scheme_percent.Text);
                dr["Size_length"] = "";//txt_flex_size_length.Text;
                dr["Size_width"] = "";//txt_flex_size_breadth.Text;
                dr["rate_per_sqft"] = "";// txt_rate_per_sqft.Text;
                                         //if (txt_flex_size_length.Text != "" && txt_flex_size_breadth.Text != "")
                                         //{
                dr["flex_size"] = "";// txt_flex_size_length.Text + "X" + txt_flex_size_breadth.Text;
                                     //}

                dr["Is_Stock_effected"] = true;


                dr["is_modification"] = false;
                dr["remarks"] = "";
                dr["is_non_taxable"] = false;
                // Cost_Rate, Trade Rate and Sale rate in Prinmary Unit
                dr["PCost_Rate"] = My.toDouble(txt_rate.Text).ToString("0.00"); // item_cost_rate;
                dr["PTrade_Rate"] = My.toDouble(txt_rate.Text).ToString("0.00"); ;
                dr["PSale_Rate"] = My.toDouble(ViewState["Purchase_Rate"].ToString()).ToString("0.00"); ;
                dr["sec_unit"] = "0"; //sec_unit;
                dr["sec_qty"] = "0";//sec_qty;


                dr["item_size_in_mtr"] = ""; //txt_size_in_mtr1.Text;
                dr["total_item_size_in_mtr"] = ""; //total_size_in_mtr;
                dr["Pur_Type_id"] = "";//pur_type_id;
                #region send data in other mapping table
                //if (is_membership)
                //{
                //    int start_sl = My.toInt(txt_product_sl_start.Text);
                //    int end_sl = start_sl + My.toInt(dr["Quantity"]);
                //    dr["Prod_sl_start"] = start_sl;
                //    My.exeSql("update Product_sl_imei_maping set status='Sold'  where product_sl_no>='" + start_sl + "' and product_sl_no<'" + end_sl + "' and item_id='" + item_code + "' and firm='" + My.firm + "'");
                //}
                //if (mapping_id != "")
                //{
                //    My.exeSql("update Product_sl_imei_maping set status='Sold'  where maping_id='" + mapping_id + "' and firm='" + My.firm + "'");
                //}
                #endregion
                //dt.Rows.Add(dr);


                dr["sale_order_no"] = "";
                dr["with_indent"] = true;
                dr["Description_Item"] = txt_item_Descriptin.Text;
                DateTime dat = My.toDateTime(txt_payment_date.Text + " " + mycode.time());
                Sale_Purchase.update_item_account_ledger(ViewState["Item_Code"].ToString(), dr["Unit_id"].ToString(), "Insert", "Pending", My.toDouble(dr["Quantity"]), "Add New item in sale", dr["session"].ToString(), ViewState["unique_entry_id"].ToString(), dr["Item_unique_entry_id"].ToString(), "5", false, dr["Godown_id"].ToString(), dat, My.toDouble(txt_rate.Text), My.toDouble(txt_rate.Text));

            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void empty_form()
        {
            ViewState["Brand_Id"] = "";
            txt_item_Descriptin.Text = "";
            btn_add_item.Text = "Add";
            txt_selected_item.Text = "";
            txt_avl_stock.Text = "";
            txt_qty.Text = "";
            txt_unit.Text = "";
            txt_rate.Text = "";
            txt_total.Text = "";
            txt_dis_percentage.Text = "";
            txt_dis_amount.Text = "";
            txt_taxbalevalue.Text = "";
            txt_gst_per.Text = "";
            txt_gstvalue.Text = "";
            txt_net_total.Text = "";
            ViewState["taxable_rate"] = "0";
            ViewState["Store_id"] = "";
            ViewState["Stock_id"] = "";
            ViewState["Item_Code"] = "";
            ViewState["Item_HSN"] = "";
            ViewState["Unit_id"] = "";
            ViewState["Expiry_date"] = "";
            ViewState["batch_no"] = "";
            ViewState["stock_item_unique_entry_id"] = "";
            ViewState["Purchase_Rate"] = "";
        }

        private void add_item()
        {
            DataTable it_dt;
            calculate_row();
            if (ViewState["order_id"].ToString() == "")
            {
                ViewState["order_id"] = My.auto_serialS("order_id");
            }

            Dictionary<string, object> dr = new Dictionary<string, object>();
            dr["Brand_Id"] = ViewState["Brand_Id"].ToString();

            dr["Item_code"] = ViewState["Item_Code"];
            dr["Barcode"] = "";
            dr["Rate"] = My.toDouble(txt_rate.Text).ToString("0.00");
            dr["taxable_rate"] = My.toDouble(txt_taxbalevalue.Text).ToString("0.00");
            dr["rate_before_discount"] = My.toDouble(txt_total.Text).ToString("0.00"); ;
            dr["Quantity"] = txt_qty.Text;
            dr["Unit_id"] = ViewState["Unit_id"].ToString();
            dr["Total"] = txt_total.Text;
            dr["sale_service_type"] = "Seal";

            dr["Branch_Id"] = "1";
            if (My.toDouble(txt_dis_percentage.Text) > 0)
            {
                dr["Discount"] = My.toDouble(txt_dis_amount.Text);
                dr["Discount_Type"] = "Percent";
                dr["Discount_Percent"] = txt_dis_percentage.Text;
            }
            else
            {
                dr["Discount"] = My.toDouble(txt_dis_amount.Text);
                dr["Discount_Type"] = "flat";
                dr["Discount_Percent"] = "0";
            }

            dr["Taxable"] = My.toDouble(txt_taxbalevalue.Text);
            dr["Total_GST"] = My.toDouble(txt_gstvalue.Text);
            //if (ViewState["state_code"].ToString() == My.state_code)
            //{
            dr["SGST"] = My.Round(My.toDouble(txt_gstvalue.Text) / 2);
            dr["CGST"] = My.Round(My.toDouble(txt_gstvalue.Text) / 2);
            dr["IGST"] = "0";
            //}
            //else
            //{
            //    dr["IGST"] = txt_gst_value.Text;
            //    dr["SGST"] = "0";
            //    dr["CGST"] = "0";
            //}
            // }
            dr["NetTotal"] = My.toDouble(txt_net_total.Text);
            //if (rbt_exclusive.IsChecked == true)
            //{
            dr["TaxCalculationType"] = "Exclusive";
            //}
            //else
            //{
            //    dr["TaxCalculationType"] = "Inclusive";
            //}
            dr["Bill_No"] = txt_temp_bill_no.Text;
            dr["Sell_To"] = ViewState["party_id"].ToString();
            dr["Mobile_no"] = txt_mobileno.Text;
            dr["GST_Percent"] = txt_gst_per.Text;//is_ipd_bill ? "0" : txt_gst_applied.Text;
            dr["Date"] = My.toDateTime(txt_payment_date.Text + " " + mycode.time()).ToString("dd/MMM/yyyy hh:mm:ss tt");
            dr["Idate"] = My.toDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
            dr["HSN_Code"] = ViewState["Item_HSN"].ToString();
            dr["Status"] = "Pending";
            dr["firm"] = My.firm_id();
            dr["session"] = My.get_session();
            dr["user_id"] = ViewState["Admin"].ToString();
            dr["unique_entry_id"] = ViewState["unique_entry_id"].ToString();
            dr["sale_type"] = "Stock";
            dr["GST_type"] = txt_gst_per.Text;//
            dr["cess_percent"] = "0";//My.toDouble(txt_cess_applied.Text);
            dr["cess_value"] = "0"; //My.toDouble(txt_cess_value.Text);
            dr["item_type"] = "Goods";
            dr["State_Code"] = ViewState["state_code"].ToString();
            dr["State"] = ViewState["state_Name"].ToString();

            dr["Batch_No"] = ViewState["batch_no"].ToString();
            dr["Exp_Date"] = ViewState["Expiry_date"].ToString();
            dr["Manu_Date"] = "";
            dr["salesman_id"] = ViewState["Admin"].ToString();
            dr["size"] = "";
            dr["imei_no"] = "";
            dr["imei_no2"] = "";
            dr["mapping_id"] = "0";
            dr["product_serial"] = "";
            dr["mrp"] = My.toDouble(txt_rate.Text).ToString("0.00");

            //if (My.bussiness_nature == "Medical Store")
            //{
            //    try
            //    {
            //        dr["Doctor_name"] = txt_doctor_name.Text;
            //        dr["Doctor_id"] = txt_doctor_name.SelectedValue.ToString();
            //    }
            //    catch
            //    {
            //        dr["Doctor_name"] = "Self";
            //        dr["Doctor_id"] = "self";
            //    }
            //}

            dr["Godown_id"] = ViewState["Store_id"].ToString();
            dr["table_id"] = "0";
            dr["order_id"] = ViewState["order_id"];
            dr["s_qty"] = "0";
            dr["f_qty"] = "0";
            dr["scheme_in_per"] = "0";//My.toDouble(txt_scheme_percent.Text);
            dr["Size_length"] = "";//txt_flex_size_length.Text;
            dr["Size_width"] = "";//txt_flex_size_breadth.Text;
            dr["rate_per_sqft"] = "";// txt_rate_per_sqft.Text;
                                     //if (txt_flex_size_length.Text != "" && txt_flex_size_breadth.Text != "")
                                     //{
            dr["flex_size"] = "";// txt_flex_size_length.Text + "X" + txt_flex_size_breadth.Text;
                                 //}

            dr["Is_Stock_effected"] = true;
            dr["Item_unique_entry_id"] = My.unique_id();
            dr["stock_item_unique_entry_id"] = ViewState["stock_item_unique_entry_id"].ToString();
            dr["is_modification"] = false;
            dr["remarks"] = "";
            dr["is_non_taxable"] = false;
            // Cost_Rate, Trade Rate and Sale rate in Prinmary Unit
            dr["PCost_Rate"] = My.toDouble(txt_rate.Text).ToString("0.00"); // item_cost_rate;
            dr["PTrade_Rate"] = My.toDouble(txt_rate.Text).ToString("0.00"); ;
            dr["PSale_Rate"] = My.toDouble(ViewState["Purchase_Rate"].ToString()).ToString("0.00"); ;
            dr["sec_unit"] = "0"; //sec_unit;
            dr["sec_qty"] = "0";//sec_qty;


            dr["item_size_in_mtr"] = ""; //txt_size_in_mtr1.Text;
            dr["total_item_size_in_mtr"] = ""; //total_size_in_mtr;
            dr["Pur_Type_id"] = "";//pur_type_id;
            #region send data in other mapping table
            //if (is_membership)
            //{
            //    int start_sl = My.toInt(txt_product_sl_start.Text);
            //    int end_sl = start_sl + My.toInt(dr["Quantity"]);
            //    dr["Prod_sl_start"] = start_sl;
            //    My.exeSql("update Product_sl_imei_maping set status='Sold'  where product_sl_no>='" + start_sl + "' and product_sl_no<'" + end_sl + "' and item_id='" + item_code + "' and firm='" + My.firm + "'");
            //}
            //if (mapping_id != "")
            //{
            //    My.exeSql("update Product_sl_imei_maping set status='Sold'  where maping_id='" + mapping_id + "' and firm='" + My.firm + "'");
            //}
            #endregion
            //dt.Rows.Add(dr);
            dr["sp_status"] = "INSERT";
            dr["user_name"] = ViewState["User_Name"].ToString();
            dr["sale_order_no"] = "";
            dr["with_indent"] = true;
            dr["description"] = txt_item_Descriptin.Text;
            it_dt = My.dataTableSP("sp_HMS_Invetory_insert_Sell_details_item_wise", dr);

            DateTime dat = My.toDateTime(txt_payment_date.Text + " " + mycode.time());
            Sale_Purchase.update_item_account_ledger(ViewState["Item_Code"].ToString(), dr["Unit_id"].ToString(), "Insert", "Pending", My.toDouble(dr["Quantity"]), "Add New item in sale", dr["session"].ToString(), ViewState["unique_entry_id"].ToString(), dr["Item_unique_entry_id"].ToString(), "5", false, dr["Godown_id"].ToString(), dat, My.toDouble(txt_rate.Text), My.toDouble(txt_rate.Text));



        }

        private void bind_grid_view(string unique_entry_id)
        {
            DataTable dt = My.dataTable("select *,(select  top 1 Unit  from dbo.[unit_master] where unit_id=HMS_Invetory_Sell_details_item_wise.unit_id) as Unit from HMS_Invetory_Sell_details_item_wise where  unique_entry_id='" + unique_entry_id + "' ");
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                grd_view.DataSource = null;
                grd_view.DataBind();
            }
            else
            {
                grd_view.DataSource = dt;
                grd_view.DataBind();
            }
            calculate_total_right_side();
        }
        protected void txt_transprtation_TextChanged(object sender, EventArgs e)
        {
            calculate_total_right_side();
        }

        protected void txt_expense_TextChanged(object sender, EventArgs e)
        {
            calculate_total_right_side();
        }

        protected void txt_float_dis_count_TextChanged(object sender, EventArgs e)
        {
            calculate_total_right_side();
        }
        private void calculate_total_right_side()
        {
            lbl_total_items.Text = grd_view.Rows.Count.ToString("00");
            double Quantity = 0;
            double Total = 0;
            double Discount = 0;
            double Taxable = 0;
            double Total_GST = 0;
            double NetTotal = 0;
            double total_gr = My.toDouble(txt_total_gr.Text); ;
            double net_payable = 0;
            double total_cess = 0;
            double round_off = 0;
            double flat_disc = My.toDouble(txt_float_dis_count.Text);
            double extra = My.toDouble(txt_expense.Text);
            double transportation = My.toDouble(txt_transprtation.Text);
            DataTable dt = My.dataTable("select * from HMS_Invetory_Sell_details_item_wise where  unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "' ");
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {

                foreach (DataRow dr in dt.Rows)
                {
                    Quantity += My.toDouble(dr["Quantity"]);
                    Total += My.toDouble(dr["Total"]);
                    Discount += My.toDouble(dr["Discount"]);
                    Taxable += My.toDouble(dr["Taxable"]);
                    //if (state_code == My.state_code)
                    //{
                    Total_GST += My.toDouble(dr["SGST"]) + My.toDouble(dr["CGST"]);
                    //}
                    //else
                    //{
                    //    Total_GST += My.toDouble(dr["Total_GST"]);
                    //}

                    NetTotal += My.toDouble(dr["NetTotal"]);
                    total_cess += My.toDouble(dr["cess_value"]);
                }
            }

            NetTotal += extra;
            NetTotal += transportation;



            lbl_total_qry.Text = Quantity.ToString();
            // txt_goods_wt.Text = Quantity.ToString();
            txt_total_amount.Text = Total.ToString("0.00");
            txt_discount.Text = Discount.ToString("0.00");
            txt_total_taxable.Text = Taxable.ToString("0.00");
            txt_total_tax.Text = Total_GST.ToString("0.00");
            // txt_total_cess.Text = //total_cess.ToString("0.00");
            txt_total_gr.Text = NetTotal.ToString("0.00");


            net_payable = NetTotal - flat_disc;
            round_off = My.Round(net_payable, 0) - My.Round(net_payable, 2);
            txt_net_total_amount.Text = My.Round(net_payable, 0).ToString("0.00");
            txt_round.Text = round_off.ToString("0.00");
            txt_payble_amount.Text = txt_net_total_amount.Text;


        }


        #endregion

        #region edit and delete
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            string id = ((Label)row.FindControl("lbl_Id")).Text;
            string stock_item_unique_entry_id = ((Label)row.FindControl("lbl_stock_item_unique_entry_id")).Text;
            txt_qty.Text = ((Label)row.FindControl("lbl_qty")).Text;
            txt_item_Descriptin.Text = ((Label)row.FindControl("lbl_description")).Text;
            txt_total.Text = ((Label)row.FindControl("lbl_Total")).Text;
            txt_dis_amount.Text = ((Label)row.FindControl("lbl_discount_amount")).Text;
            txt_dis_percentage.Text = ((Label)row.FindControl("lbl_Discount_Percent")).Text;
            string seal_to = txt_item_Descriptin.Text;
            string[] stringSeparatorss = new string[] { "," };
            string[] arrs = seal_to.Split(stringSeparatorss, StringSplitOptions.None);
            string itemname = arrs[0];
            string brand = arrs[1];
            string itemid = arrs[2];
            txt_selected_item.Text = itemname;
            get_avl_stock(stock_item_unique_entry_id);
            calculate_row();
            btn_add_item.Text = "Update";
            ViewState["id"] = id;

        }

        private void get_avl_stock(string stock_item_unique_entry_id)
        {

            string qry = @"select *,(select  top 1 Brand_name  from dbo.[HMS_Invetory_Brand_Master] where Brand_id=HMS_Inventory_stock_details.Brand_Id) as Brand_name,(select  top 1 Unit  from dbo.[unit_master] where unit_id=HMS_Inventory_stock_details.Unit_id) as Unit  from HMS_Inventory_stock_details where stock_item_unique_entry_id='" + stock_item_unique_entry_id + "'";
            DataTable dt = My.dataTable(qry);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {

            }
            else
            {
                txt_gst_per.Text = dt.Rows[0]["GST_Percent"].ToString();
                ViewState["Store_id"] = dt.Rows[0]["Store_id"].ToString();
                ViewState["Stock_id"] = dt.Rows[0]["Stock_id"].ToString();
                ViewState["Item_Code"] = dt.Rows[0]["Item_Code"].ToString();
                ViewState["Item_HSN"] = dt.Rows[0]["hsn_no"].ToString();
                ViewState["Unit_id"] = dt.Rows[0]["Unit_id"].ToString();
                ViewState["Expiry_date"] = dt.Rows[0]["Expiry_date"].ToString();
                ViewState["batch_no"] = dt.Rows[0]["Batch_no"].ToString();
                txt_avl_stock.Text = dt.Rows[0]["Quantity"].ToString();
                txt_unit.Text = dt.Rows[0]["Unit"].ToString();
                txt_rate.Text = dt.Rows[0]["Sale_rate"].ToString();
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.NamingContainer;
                string id = ((Label)row.FindControl("lbl_Id")).Text;
                My.exeSql("delete from HMS_Invetory_Sell_details_item_wise where Id=" + id + "");
                bind_grid_view(ViewState["unique_entry_id"].ToString());
                calculate_total_right_side();
            }
            catch
            {

            }


        }

        #endregion



        protected void btn_seal_fina_Click(object sender, EventArgs e)
        {
            txt_trnaction_no.ReadOnly = false;
            if (ViewState["party_id"] == null)
            {
                Alertme("Please enter valid sale to user", "warning");
                txt_seal_to.Focus();
                return;
            }
            if (ViewState["party_id"].ToString() == "")
            {
                Alertme("Please enter valid user or something is wrong for your selected user, Please refresh page and add again product", "warning");
                txt_seal_to.Focus();
                return;
            }
            else
            {
                ddl_paymentmode.Enabled = true;
                txt_recived_from_bank.ReadOnly = false;
                chk_bank.Checked = false;
                chk_cash.Checked = true;
                chk_bank.Enabled = true;
                chk_cash.Enabled = true;
                txt_total_paid.Text = txt_payble_amount.Text;
                txt_recived.Text = txt_payble_amount.Text;
                txt_recived_from_bank.Text = "0";
                txt_trnaction_no.Text = "";
                txt_dues.Text = "0.00";


                if (ViewState["OnlineFromAPP"].ToString() == "Yes")
                {
                    txt_trnaction_no.Text = My.get_app_purchase_onlinetran(ViewState["appbillno"].ToString(), ViewState["party_id"].ToString());

                    if (txt_trnaction_no.Text == "offline")
                    {
                        ddl_paymentmode.Enabled = true;
                        txt_recived_from_bank.ReadOnly = false;
                        chk_bank.Checked = false;
                        chk_cash.Checked = true;
                        chk_bank.Enabled = true;
                        chk_cash.Enabled = true;
                        txt_total_paid.Text = txt_payble_amount.Text;
                        txt_recived.Text = txt_payble_amount.Text;
                        txt_recived_from_bank.Text = "0";
                        txt_trnaction_no.Text = "";
                        txt_dues.Text = "0.00";
                    }
                    else
                    {
                        ddl_paymentmode.Text = "Online";
                        txt_recived_from_bank.Text = txt_payble_amount.Text;
                        txt_recived_from_bank.ReadOnly = true;
                        txt_trnaction_no.ReadOnly = true;
                        ddl_paymentmode.Enabled = false;
                        txt_recived.Text = "0.00";
                        chk_bank.Checked = true; ;
                        chk_bank.Enabled = false;
                        chk_cash.Checked = false;
                        chk_cash.Enabled = false;
                    }

                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
            }

        }

        protected void btn_pay_now_Click(object sender, EventArgs e)
        {
            if (ViewState["party_id"] == null)
            {
                Alertme("Please enter valid sale to user", "warning");
                txt_seal_to.Focus();
                return;
            }
            if (ViewState["party_id"].ToString() == "")
            {
                Alertme("Please enter valid user or something is wrong for your selected user, Please refresh page and add again product", "warning");
                txt_seal_to.Focus();
                return;
            }

            else
            {

                if (chk_bank.Checked == true)
                {
                    if (ddl_paymentmode.Text == "Select")
                    {
                        Alertme("Please select payment mode", "warning");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                    }
                    else
                    {
                        if (chk_bank.Checked == true)
                        {
                            if (ddl_paymentmode.Text == "Select")
                            {
                                Alertme("Please select payment mode", "warning");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                            }
                            else
                            {
                                if (ddl_paymentmode.Text == "Wallet")
                                {

                                    if (My.toDouble(ViewState["avlwallet"].ToString()) > 0)
                                    {

                                        if (My.toDouble(txt_recived_from_bank.Text) == 0)
                                        {
                                            Alertme("Please enter wallet amount", "warning");
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                                        }
                                        else
                                        {
                                            double Walletamount = My.toDouble(ViewState["avlwallet"].ToString());
                                            if (Walletamount >= My.toDouble(txt_recived_from_bank.Text))
                                            {


                                                final_pay_now();
                                            }
                                            else
                                            {
                                                Alertme(" Sorry you cannot enter more than wallet amount", "warning");
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        Alertme(" Sorry your wallet amount is zero so you can't use wallet", "warning");
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                                    }
                                }
                                else
                                {
                                    if (My.toDouble(txt_recived_from_bank.Text) == 0)
                                    {
                                        Alertme("Please enter bank amount", "warning");
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                                    }
                                    else
                                    {
                                        if (txt_trnaction_no.Text == "")
                                        {
                                            Alertme("Please enter valid " + lbl_mode_trns_no.Text, "warning");
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_item_pay();", true);
                                        }
                                        else
                                        {
                                            final_pay_now();
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            final_pay_now();
                        }
                    }
                }
                else
                {
                    final_pay_now();
                }
            }




        }
        private string invoice_format(string seal)
        {
            string Item_Id = My.global_id_creation(seal);
            string billno = Item_Id + "/" + My.get_session();
            return billno;
        }

        private void final_pay_now()
        {

            try
            {
                bool finalsubmit = false;
                string bill_serial = "";
                string temp_bill_no = txt_temp_bill_no.Text;
                string bill_no = txt_temp_bill_no.Text;
                if (txt_temp_bill_no.Text.StartsWith("temp"))
                {
                    bill_serial = invoice_format("Sealinvoice_no");
                }
                else
                {
                    bill_serial = txt_temp_bill_no.Text.Trim();
                }
                DateTime date;
                if (txt_payment_date.Text != "") { date = My.toDateTime(txt_payment_date.Text + " " + mycode.time()); }
                else { date = My.toDateTime(Sale_Purchase.toDateWithTime(txt_payment_date.Text)); }

                bool check_avl_stock = get_check_avl_stock();
                if (check_avl_stock == true)
                {
                    update_stock(bill_no, temp_bill_no);
                    send_to_bill_wise_details(bill_no, temp_bill_no, bill_serial, date);
                    Inventory_Bill_Payment_Tracking(bill_serial, date);
                    Ledger_Opening_Balance(bill_serial, txt_total_paid.Text, txt_payment_date.Text, "Dr");
                    Ledger_Opening_Balance(bill_serial, txt_total_paid.Text, txt_payment_date.Text, "Cr");
                    finalsubmit = true;
                    if (finalsubmit == true)
                    {



                        /*try
                        {
                            SqlCommand cmd;
                            string query = "update Online_Sell_billwise set Order_Status=@Order_Status,New_bill_no=@New_bill_no,Process_by=@Process_by,Process_date=@Process_date where user_id=@user_id and Bill_No=@Bill_No ";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@user_id", ViewState["party_id"].ToString());
                            cmd.Parameters.AddWithValue("@Bill_No", ViewState["appbillno"].ToString());
                            cmd.Parameters.AddWithValue("@New_bill_no", bill_serial);
                            cmd.Parameters.AddWithValue("@Order_Status", "Delivered");
                            cmd.Parameters.AddWithValue("@Process_by", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Process_date", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
                            if (My.InsertUpdateData(cmd))
                            {
                                My.exeSql("update Online_Sell_item_wise set Status='Delivered' where Bill_No='" + ViewState["appbillno"].ToString() + "' and Sell_To='" + ViewState["party_id"].ToString() + "'");
                            }
                        }
                        catch (Exception ex)
                        {

                        }*/




                        /*       try
                               {
                                   double amount = My.toDouble(txt_total_paid.Text);
                                   string sub = "Item Purchase";
                                   string messge = "Dear " + ViewState["party_name"].ToString() + " you have paid Purchase item. fee :- " + amount.ToString("0.00") + " date :- " + txt_payment_date.Text + " slip no.:-" + bill_serial;
                                   Dictionary<String, String> ss = new Dictionary<string, string>();
                                   ss["notification_id"] = Guid.NewGuid().ToString();
                                   ss["message"] = messge;
                                   ss["title"] = sub;
                                   ss["messagetype"] = "Message";
                                   ss["url"] = "";
                                   ss["link_url"] = "";
                                   ss["UserId"] = ViewState["party_id"].ToString();
                                   UsesCode.SendNotification(My.getgcmid(ViewState["party_id"].ToString()), ss);

                               }
                               catch
                               {

                               }*/
                        string Uri = "Slip/Print_Sale_slip.aspx?unique_entry_id=" + ViewState["unique_entry_id"].ToString() + "&partyid=" + ViewState["party_id"].ToString() + "";
                        Response.Redirect(Uri, false);


                    }

                }
                else
                {
                    Alertme("Your Stock has short please purchase stock. after you can billing", "warning");
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }




        }

        private void Inventory_Bill_Payment_Tracking(string Bill_No, DateTime date)
        {
            SqlCommand cmd;
            string query = "INSERT INTO HMS_Inventory_Bill_Payment_Tracking (party_id,Bill_No,Payment_Vochar_id,Payable_Amount,Total_Paid_Amount,Duse_Amount,Received_from_Cash,Received_from_Bank,Bank_Payment_Mode,Date_time,Idate,Payment_transaction,Remarks,User_Id) values (@party_id,@Bill_No,@Payment_Vochar_id,@Payable_Amount,@Total_Paid_Amount,@Duse_Amount,@Received_from_Cash,@Received_from_Bank,@Bank_Payment_Mode,@Date_time,@Idate,@Payment_transaction,@Remarks,@User_Id)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@party_id", ViewState["party_id"].ToString());
            cmd.Parameters.AddWithValue("@Bill_No", Bill_No);
            cmd.Parameters.AddWithValue("@Payment_Vochar_id", Bill_No);
            cmd.Parameters.AddWithValue("@Payable_Amount", My.toDouble(txt_net_total_amount.Text).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Total_Paid_Amount", My.toDouble(txt_total_paid.Text).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Duse_Amount", My.toDouble(txt_dues.Text).ToString("0.00"));
            if (chk_cash.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Received_from_Cash", My.toDouble(txt_recived.Text).ToString("0.00"));
            }
            else
            {
                cmd.Parameters.AddWithValue("@Received_from_Cash", "0.00");
            }


            if (chk_bank.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Received_from_Bank", My.toDouble(txt_recived_from_bank.Text).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Bank_Payment_Mode", ddl_paymentmode.Text);
                cmd.Parameters.AddWithValue("@Payment_transaction", txt_trnaction_no.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Received_from_Bank", "0.00");
                cmd.Parameters.AddWithValue("@Bank_Payment_Mode", "Cash");
                cmd.Parameters.AddWithValue("@Payment_transaction", "N/A");
            }

            cmd.Parameters.AddWithValue("@Date_time", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@Idate", date.ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Remarks", txt_remarks_amt.Text);
            cmd.Parameters.AddWithValue("@User_Id", ViewState["Userid"].ToString());
            if (My.InsertUpdateData(cmd))
            {
                if (ddl_paymentmode.Text == "Wallet")
                {
                    string rm = "Wallet amount deducted for product purchase. " + txt_recived_from_bank.Text + "/.";
                    SqlCommand cmd2;
                    string query2 = "INSERT INTO Student_Wallet (Adm_no,Session_id,Wallet_input_amount,Wallet_Out_amount,Date_of_entry,Remakes,slipno,Mode) values (@Adm_no,@Session_id,@Wallet_input_amount,@Wallet_Out_amount,@Date_of_entry,@Remakes,@slipno,@Mode)";
                    cmd2 = new SqlCommand(query2);
                    cmd2.Parameters.AddWithValue("@Adm_no", ViewState["party_id"].ToString());
                    cmd2.Parameters.AddWithValue("@Session_id", ViewState["Session_id"].ToString());
                    cmd2.Parameters.AddWithValue("@Wallet_input_amount", "0.00");
                    cmd2.Parameters.AddWithValue("@Wallet_Out_amount", My.toDouble(txt_recived_from_bank.Text).ToString("0.00"));
                    cmd2.Parameters.AddWithValue("@Date_of_entry", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
                    cmd2.Parameters.AddWithValue("@Remakes", rm);
                    cmd2.Parameters.AddWithValue("@slipno", Bill_No);
                    cmd2.Parameters.AddWithValue("@Mode", "Repurchase");
                    if (My.InsertUpdateData(cmd2)) ;
                    {
                    }
                }
            }
        }

        private void Ledger_Opening_Balance(string bill_serial, string amount, string payment_date, string type)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Ledger_Opening_Balance where  Account_id ='" + ViewState["party_id"].ToString() + "' and Bill_id='" + bill_serial + "' and Debit_Credit='" + type + "'  ", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Account_Ledger_Details");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();

                dr["Account_id"] = ViewState["party_id"].ToString();//partyid
                dr["Group_id"] = "22";
                dr["firm"] = My.firm_id();
                dr["Session"] = My.get_session();
                dr["Date"] = payment_date;
                dr["idate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                dr["Debit_Credit"] = "Dr";

                if (type == "Dr")
                {
                    dr["Debit"] = My.toDouble(amount).ToString("0.00");
                    dr["Credit"] = "0.00";
                }
                else
                {
                    dr["Credit"] = My.toDouble(amount).ToString("0.00");
                    dr["Debit"] = "0.00";
                }
                dt.Rows.Add(dr);
            }
            else
            {

                foreach (DataRow dr in dt.Rows)
                {

                    dr["Date"] = payment_date;
                    dr["idate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                    dr["Debit_Credit"] = type;

                    if (type == "Dr")
                    {
                        dr["Debit"] = My.toDouble(amount).ToString("0.00");
                        dr["Credit"] = "0.00";
                    }
                    else
                    {
                        dr["Credit"] = My.toDouble(amount).ToString("0.00");
                        dr["Debit"] = "0.00";
                    }
                    break;
                }

            }

            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private bool get_check_avl_stock()
        {
            bool toreturn = false;
            DataTable dt = My.dataTable(" select  Quantity,(Select sum(cast (  Quantity as int )) from HMS_Inventory_stock_details where Item_Code=HMS_Invetory_Sell_details_item_wise.Item_code and Unit_id=HMS_Invetory_Sell_details_item_wise.Unit_id) as avlstock  from dbo.[HMS_Invetory_Sell_details_item_wise] where  Status='Pending' and unique_entry_id =  '" + ViewState["unique_entry_id"].ToString() + "' ");

            if (dt.Rows.Count == 0)
            {
                toreturn = false;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    int quantity = Convert.ToInt32(dt.Rows[i]["Quantity"].ToString());
                    int avlstock = Convert.ToInt32(dt.Rows[i]["avlstock"].ToString());

                    if (avlstock >= quantity)
                    {
                        toreturn = true;
                    }
                    else
                    {
                        //toreturn = false;
                        toreturn = true;
                        break;
                    }
                }
            }
            return toreturn;
        }

        private void update_stock(string bill_no, string temp_bill_no)
        {
            DataTable dt = My.dataTable("  select  *  from dbo.[HMS_Invetory_Sell_details_item_wise] where  Status='Pending' and unique_entry_id = '" + ViewState["unique_entry_id"].ToString() + "' ");

            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Item_code = dt.Rows[i]["Item_code"].ToString();
                    string unit_id = dt.Rows[i]["unit_id"].ToString();
                    string stock_item_unique_entry_id = dt.Rows[i]["stock_item_unique_entry_id"].ToString();
                    string Id = dt.Rows[i]["Id"].ToString();
                    string Quantity = dt.Rows[i]["Quantity"].ToString();


                    try
                    {

                        bool Deducted_stockstatus = Deducted_stock(stock_item_unique_entry_id, Quantity, Item_code, unit_id);
                        if (Deducted_stockstatus == false)
                        {

                            SqlCommand cmd;
                            cmd = new SqlCommand("update HMS_Invetory_Sell_details_item_wise set Status=@Status, Is_Stock_Delivered=@Is_Stock_Delivered,Delivery_date=@Delivery_date where Id=@Id ");
                            cmd.Parameters.AddWithValue("@Status", "Saved");
                            cmd.Parameters.AddWithValue("@Is_Stock_Delivered", "NotDelivered");
                            cmd.Parameters.AddWithValue("@Delivery_date", My.getdate1());
                            cmd.Parameters.AddWithValue("@Id", Id);
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {


                            }
                            //My.exeSql("update HMS_Invetory_Sell_details_item_wise set Status='Saved',Is_Stock_Delivered='NotDelivered',Delivery_date="+My.getdate1()+" where Id=" + Id + " ");
                        }
                        else
                        {
                            //My.exeSql("update HMS_Invetory_Sell_details_item_wise set Status='Saved',Is_Stock_Delivered='Delivered',,Delivery_date=" + My.getdate1() + " where Id=" + Id + " ");

                            SqlCommand cmd;
                            cmd = new SqlCommand("update HMS_Invetory_Sell_details_item_wise set Status=@Status, Is_Stock_Delivered=@Is_Stock_Delivered,Delivery_date=@Delivery_date where Id=@Id ");
                            cmd.Parameters.AddWithValue("@Status", "Saved");
                            cmd.Parameters.AddWithValue("@Is_Stock_Delivered", "Delivered");
                            cmd.Parameters.AddWithValue("@Delivery_date", My.getdate1());
                            cmd.Parameters.AddWithValue("@Id", Id);
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {


                            }

                            try
                            {
                                Sale_Purchase.HMS_Item_Account_Ledger_udpate("1", Item_code, unit_id, txt_payment_date.Text, ViewState["unique_entry_id"].ToString(), stock_item_unique_entry_id, "0", Quantity, "Sale Entry");
                            }
                            catch
                            {

                            }
                        }

                    }
                    catch
                    {

                    }

                    //stock ledger insert



                }
            }
        }

        public bool Deducted_stock(string stock_item_unique_entry_id, string qty, string Item_code, string unit_id)
        {


            bool stockdDeducted = false;
            DataTable dt1 = mycode.FillData("select sum(cast(Quantity as int))  from HMS_Inventory_stock_details where Item_Code = '" + Item_code + "' and Unit_id='" + unit_id + "'");
            if (dt1.Rows.Count == 0)
            {
                return stockdDeducted = false;

            }
            else
            {
                int totalavlqty = My.toint(dt1.Rows[0][0].ToString());

                if (totalavlqty >= My.toint(qty))
                {

                    SqlDataAdapter ad = new SqlDataAdapter($"select * from HMS_Inventory_stock_details where  Item_Code='{Item_code}' and Unit_id='{unit_id}' and cast(quantity as float)>0", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "stock_details");
                    DataTable dt = ds.Tables[0];
                    double sale_qty = Convert.ToDouble(qty);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            double stock_qty = Convert.ToDouble(dr["quantity"].ToString());
                            if (sale_qty > 0)
                            {
                                if (stock_qty >= sale_qty)
                                {
                                    stock_qty = stock_qty - sale_qty;
                                    dr["quantity"] = stock_qty;
                                    sale_qty = 0;
                                    stockdDeducted = true;
                                }
                                else
                                {
                                    dr["quantity"] = 0;
                                    sale_qty = sale_qty - stock_qty;
                                    stockdDeducted = true;

                                }
                            }
                        }
                    }
                    else
                    {
                        stockdDeducted = false;
                    }
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);

                    return stockdDeducted;
                }
                else
                {
                    return stockdDeducted = false;

                }

            }


        }
        private void send_to_bill_wise_details(string bill_no, string temp_bill_no, string bill_serial, DateTime date)
        {
            double GrandTotal = My.toDouble(txt_total_taxable.Text) + My.toDouble(txt_total_tax.Text);


            double after_flat_discount = My.toDouble(txt_total_gr.Text) - My.toDouble(txt_float_dis_count.Text);


            bool ismodify = false;
            string patient_current_status = "";
            #region patient type
            // This condition apply only for that patient which will transfer from hospital 
            // Find out the current patient status (OT,OPD,IPD) and patient type (General,TPA)

            //if (My.bussiness_nature == "Medical Store")
            //{

            //    if (patient_current_status == "")
            //    {
            //        patient_current_status = with_indent ? indent_type : "General";
            //    }
            //    if (patient_type == "")
            //    {
            //        patient_type = "General";
            //    }

            //    if (isPStatus)
            //    {
            //        patient_current_status = patientStatus;
            //    }
            //}

            #endregion
            SqlDataAdapter ad = new SqlDataAdapter("select top 1 * from HMS_Invetory_Sell_details_billwise  where  firm='" + My.firm_id() + "' and session='" + My.get_session() + "' and Bill_No='" + bill_no + "' and unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Sell_details_billwise");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();



                try
                {
                    if (ismodify)
                    {
                        dr["Bill_No"] = valid(bill_serial);
                    }
                    else
                    {
                        dr["Bill_No"] = valid(bill_serial);
                    }

                    dr["Total_Item"] = valid(lbl_total_items.Text);
                    dr["db_version"] = "";
                    dr["Total_Quantity"] = valid(lbl_total_qry.Text);
                    dr["Total_amount"] = valid(txt_total_amount.Text);
                    dr["Taxable"] = valid(txt_total_taxable.Text);
                    dr["GST"] = valid(txt_total_tax.Text);


                    dr["GrandTotal"] = valid(GrandTotal);


                    dr["Total_GR"] = valid(txt_total_gr.Text);
                    dr["NetPayable"] = valid(txt_net_total_amount.Text);
                    dr["Discount"] = valid(txt_discount.Text);
                    dr["party_id"] = ViewState["party_id"].ToString();
                    dr["Mobile"] = valid(txt_mobileno.Text);
                    dr["GSTIN"] = valid("");
                    dr["firm"] = valid(My.firm_id());
                    dr["session"] = valid(My.get_session());
                    dr["user_id"] = valid(ViewState["Admin"].ToString());
                    dr["Cess"] = My.toDouble("0.00");
                    dr["flat_disc"] = My.toDouble(valid(txt_float_dis_count.Text)).ToString("0.00");
                    //if (state_code == My.state_code)
                    //{
                    dr["SGST"] = My.Round(My.toDouble(txt_total_tax.Text) / 2);
                    dr["CGST"] = My.Round(My.toDouble(txt_total_tax.Text) / 2);
                    dr["IGST"] = "0";
                    //}
                    //else
                    //{
                    //    dr["IGST"] = valid(txt_total_tax.Text);
                    //    dr["SGST"] = "0";
                    //    dr["CGST"] = "0";
                    //}

                    dr["net_taxable"] = valid(dr["Taxable"]);
                    dr["net_igst"] = valid(dr["IGST"]);
                    dr["net_sgst"] = valid(dr["SGST"]);
                    dr["net_cgst"] = valid(dr["CGST"]);
                    dr["net_cess"] = valid(dr["Cess"]);
                    dr["unique_entry_id"] = ViewState["unique_entry_id"].ToString();
                    dr["round_off"] = valid(txt_round.Text);
                    dr["State"] = ViewState["state_Name"].ToString();
                    dr["State_Code"] = ViewState["state_code"].ToString();
                    //if (consignee_id == "")
                    //{
                    dr["Consignee_Id"] = valid(ViewState["party_id"].ToString());
                    //}

                    //else
                    //{
                    //    dr["Consignee_Id"] = valid(consignee_id);
                    //}
                    dr["supply_place"] = ""; //valid(txt_supply_place.Text);
                    dr["ref_inv"] = "";// valid(txt_ref_inv.Text);
                    dr["ccl_challan_no"] = "";// valid(txt_ccl_challan_no.Text);
                    dr["unique_no"] = ViewState["unique_entry_id"].ToString(); //valid(txt_unique_no.Text);
                    dr["challan_no"] = "";//valid(txt_challan_no.Text);
                    dr["tare_wt"] = "";//valid(txt_tare_wt.Text);
                    dr["goods_wt"] = "";//; valid(txt_goods_wt.Text);

                    dr["CustomerName"] = txt_seal_to.Text;
                    dr["remark"] = valid(txt_remarks.Text);
                    dr["pan_no"] = ""; //valid(txt_pan_no.Text);
                    dr["wt_unit"] = ""; //My.bussiness_nature == "Coal Business" ? "Tone" : "";
                                        //if (My.bussiness_nature == "Medical Store")
                                        //{
                                        //    try
                                        //    {
                                        //        dr["Doctor_name"] = txt_doctor_name.Text;
                                        //        dr["Doctor_id"] = txt_doctor_name.SelectedValue.ToString();
                                        //    }
                                        //    catch
                                        //    {
                                        //        dr["Doctor_name"] = "Self";
                                        //        dr["Doctor_id"] = "self";
                                        //    }
                                        //}
                    dr["Address"] = "";//valid(txt_address.Text);
                    dr["Branch_id"] = "1"; //valid(My.default_branch_id);
                    dr["is_non_taxable"] = false;//valid(is_non_taxable);
                    dr["Pur_Type_id"] = "0";
                    dr["Registration_Type"] = "UnRegistered";//My.registration_type;
                    dr["bill_serial"] = My.toDouble(bill_serial);
                    dr["P_Type"] = "General";
                    dr["Patient_TPA_Type"] = "General";
                    dr["with_indent"] = true;
                    dr["sale_order_no"] = valid(ViewState["order_id"].ToString());
                    dr["Patient_Ward"] = "";
                    try { dr["referral_id"] = "self"; }
                    catch { dr["referral_id"] = "self"; }

                    dr["Date"] = date.ToString("yyyy/MM/dd hh:mm:ss tt");
                    dr["Idate"] = date.ToString("yyyyMMdd");
                    dr["HMS_bill_no"] = "";
                    dr["Payment_Mode"] = ddl_paymentmode.Text;
                    dr["Payment_TransactionId"] = txt_trnaction_no.Text;
                    dr["Payment_Remarks"] = txt_remarks_amt.Text;
                    dr["transprtation_charge"] = My.toDouble(txt_transprtation.Text).ToString("0.00");
                    dr["expense"] = My.toDouble(txt_expense.Text).ToString("0.00");
                    dr["After_flat_discount"] = My.toDouble(after_flat_discount).ToString("0.00");
                    dr["Sale_type"] = hd_saletype.Value;
                    dr["Package_id"] = ViewState["Package_id"].ToString();
                }
                catch (Exception ex)
                {
                    My.submitexception(ex.StackTrace);
                }

                dt.Rows.Add(dr);
                send_to_account_ledger(bill_serial, date.ToString("yyyy/MM/dd hh:mm:ss tt"), ismodify);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {

                    if (ismodify)
                    {
                        dr["Bill_No"] = valid(temp_bill_no);
                    }
                    else
                    {
                        dr["Bill_No"] = valid(bill_no);
                    }

                    dr["Total_Item"] = valid(lbl_total_items.Text);
                    dr["db_version"] = "";
                    dr["Total_Quantity"] = valid(lbl_total_qry.Text);
                    dr["Total_amount"] = valid(txt_total_amount.Text);
                    dr["Taxable"] = valid(txt_total_taxable.Text);
                    dr["GST"] = valid(txt_total_tax.Text);
                    dr["GrandTotal"] = valid(GrandTotal);
                    dr["Total_GR"] = valid(txt_total_gr.Text);
                    dr["NetPayable"] = valid(txt_net_total_amount.Text);
                    dr["Discount"] = valid(txt_discount.Text);
                    dr["party_id"] = ViewState["party_id"].ToString();
                    dr["Mobile"] = valid(txt_mobileno.Text);
                    dr["GSTIN"] = valid("");
                    dr["firm"] = valid(My.firm_id());
                    dr["session"] = valid(My.get_session());
                    dr["user_id"] = valid(ViewState["Admin"].ToString());
                    dr["Cess"] = My.toDouble("0.00");
                    dr["flat_disc"] = My.toDouble(valid(txt_float_dis_count.Text)).ToString("0.00");
                    //if (state_code == My.state_code)
                    //{
                    dr["SGST"] = My.Round(My.toDouble(txt_total_tax.Text) / 2);
                    dr["CGST"] = My.Round(My.toDouble(txt_total_tax.Text) / 2);
                    dr["IGST"] = "0";
                    //}
                    //else
                    //{
                    //    dr["IGST"] = valid(txt_total_tax.Text);
                    //    dr["SGST"] = "0";
                    //    dr["CGST"] = "0";
                    //}

                    dr["net_taxable"] = valid(dr["Taxable"]);
                    dr["net_igst"] = valid(dr["IGST"]);
                    dr["net_sgst"] = valid(dr["SGST"]);
                    dr["net_cgst"] = valid(dr["CGST"]);
                    dr["net_cess"] = valid(dr["Cess"]);
                    dr["unique_entry_id"] = ViewState["unique_entry_id"].ToString();
                    dr["round_off"] = valid(txt_round.Text);
                    dr["State"] = ViewState["state_Name"].ToString();
                    dr["State_Code"] = ViewState["state_code"].ToString();
                    //if (consignee_id == "")
                    //{
                    dr["Consignee_Id"] = valid(ViewState["party_id"].ToString());
                    //}

                    //else
                    //{
                    //    dr["Consignee_Id"] = valid(consignee_id);
                    //}
                    dr["supply_place"] = ""; //valid(txt_supply_place.Text);
                    dr["ref_inv"] = "";// valid(txt_ref_inv.Text);
                    dr["ccl_challan_no"] = "";// valid(txt_ccl_challan_no.Text);
                    dr["unique_no"] = ViewState["unique_entry_id"].ToString(); //valid(txt_unique_no.Text);
                    dr["challan_no"] = "";//valid(txt_challan_no.Text);
                    dr["tare_wt"] = "";//valid(txt_tare_wt.Text);
                    dr["goods_wt"] = "";//; valid(txt_goods_wt.Text);

                    dr["CustomerName"] = txt_seal_to.Text;
                    dr["remark"] = valid(txt_remarks.Text);
                    dr["pan_no"] = ""; //valid(txt_pan_no.Text);
                    dr["wt_unit"] = ""; //My.bussiness_nature == "Coal Business" ? "Tone" : "";
                                        //if (My.bussiness_nature == "Medical Store")
                                        //{
                                        //    try
                                        //    {
                                        //        dr["Doctor_name"] = txt_doctor_name.Text;
                                        //        dr["Doctor_id"] = txt_doctor_name.SelectedValue.ToString();
                                        //    }
                                        //    catch
                                        //    {
                                        //        dr["Doctor_name"] = "Self";
                                        //        dr["Doctor_id"] = "self";
                                        //    }
                                        //}
                    dr["Address"] = "";//valid(txt_address.Text);
                    dr["Branch_id"] = "1"; //valid(My.default_branch_id);
                    dr["is_non_taxable"] = false;//valid(is_non_taxable);
                    dr["Pur_Type_id"] = "0";
                    dr["Registration_Type"] = "UnRegistered";//My.registration_type;
                    dr["bill_serial"] = My.toDouble(bill_serial);
                    dr["P_Type"] = "General";
                    dr["Patient_TPA_Type"] = "General";
                    dr["with_indent"] = true;
                    dr["sale_order_no"] = valid(ViewState["order_id"].ToString());
                    dr["Patient_Ward"] = "";
                    try { dr["referral_id"] = "self"; }
                    catch { dr["referral_id"] = "self"; }

                    dr["Date"] = date.ToString("yyyy/MM/dd hh:mm:ss tt");
                    dr["Idate"] = date.ToString("yyyyMMdd");
                    dr["HMS_bill_no"] = "";

                    dr["Payment_Mode"] = ddl_paymentmode.Text;
                    dr["Payment_TransactionId"] = txt_trnaction_no.Text;
                    dr["Payment_Remarks"] = txt_remarks_amt.Text;
                    dr["After_flat_discount"] = My.toDouble(after_flat_discount).ToString("0.00");
                    send_to_account_ledger(bill_serial, date.ToString("yyyy/MM/dd hh:mm:ss tt"), ismodify);
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);





        }

        private void send_to_account_ledger(string bill_no, string date, bool ismodify)
        {

            double net_taxable, net_cgst, net_sgst, net_cess, net_igst;
            // remove_payment_ledger(bill_no, unique_entry_id);
            net_cgst = 0;
            net_sgst = 0;
            net_cess = 0;
            net_igst = 0;
            double flat_disc = My.toDouble(txt_float_dis_count.Text);
            My.fetch_all_account();

            SqlDataAdapter ad = new SqlDataAdapter($"select * from Account_Voucher_Details where unique_entry_id = '{ViewState["unique_entry_id"].ToString()}' and firm='{My.firm_id()}' and VoucherNo='{bill_no}' and Session='{My.get_session()}' and VoucherType='Sales'  ", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Account_Voucher_Details");
            DataTable dt = ds.Tables[0];
            DataTable temp = new DataTable();
            temp.Columns.Add("account_id");
            temp.Columns.Add("group_id");
            temp.Columns.Add("credit");
            temp.Columns.Add("debit");
            temp.Columns.Add("type");
            temp.Columns.Add("voucher_no");
            temp.Columns.Add("Alternet_Account");
            temp.Columns.Add("description");
            temp.Columns.Add("date");
            temp.Columns.Add("payment_id");
            temp.Columns.Add("insert_qry");
            double extra_tax = 0;
            double transsport_tax = 0;


            try
            {
                string party_id = ViewState["party_id"].ToString();
                string cash_payment_date = date;
                string card_payment_date = date;
                if (ismodify)
                {
                    cash_payment_date = Sale_Purchase.toDateWithTime(date);
                    card_payment_date = Sale_Purchase.toDateWithTime(date);
                }

                double net_payable = My.toDouble(txt_net_total_amount.Text);
                double oth_amt = My.toDouble("0.00");
                double bank_amt = My.toDouble("0.00");
                double cash_amt = My.toDouble(txt_payble_amount.Text);
                double prev_cash_amt = My.toDouble("0.00");
                double prev_bank_amt = My.toDouble("0.00");
                double prev_oth_amt = My.toDouble("0.00");
                double sale_amount = My.toDouble(txt_total_taxable.Text);
                net_taxable = sale_amount;

                #region discount ledger
                bool itemwise_discount_save = true;
                if (itemwise_discount_save == true)
                {
                    sale_amount += My.toDouble(txt_discount.Text);
                    if (flat_disc + My.toDouble(txt_discount.Text) > 0)
                    {
                        temp.Rows.Add("disc_allowed", My.group["disc_allowed"], "", (flat_disc + My.toDouble(txt_discount.Text)), "Sales", bill_no, "sales", "", date);
                    }
                }
                else
                {
                    if (flat_disc > 0)
                    {
                        temp.Rows.Add("disc_allowed", My.group["disc_allowed"], "", flat_disc, "Sales", bill_no, "sales", "", date);
                    }
                }
                #endregion

                #region sale ledger
                if (party_id == "" || party_id == "cash")
                {
                    temp.Rows.Add("sales", My.group["sales"], sale_amount, "", "Sales", bill_no, "cash", "", date);
                }
                else
                {
                    temp.Rows.Add("sales", My.group["sales"], sale_amount, "", "Sales", bill_no, party_id, "", date);
                    temp.Rows.Add(party_id, My.group[party_id], "", net_payable, "Sales", bill_no, "sales", "", date);
                }
                #endregion

                #region  cash receipt ledger 
                //p.txt_due.Text=""
                // string  txt_due.Text = "Dues Amount";
                double cash_paid = "Dues Amount" == "Return Amount" ? (net_payable - oth_amt - prev_oth_amt - bank_amt - prev_cash_amt - prev_bank_amt) : cash_amt;
                if (cash_paid > 0)
                {
                    if (party_id != "cash")
                    {
                        temp.Rows.Add("cash", My.group["cash"], "", cash_paid.ToString("0.00"), "Sales", bill_no, "sales", "", date);
                        temp.Rows.Add(party_id, My.group[party_id], cash_paid.ToString("0.00"), "", "Sales", bill_no, "cash", "", date);
                    }
                    else
                    {
                        temp.Rows.Add("cash", My.group["cash"], "", cash_paid.ToString("0.00"), "Sales", bill_no, "sales", "", date);

                    }
                }
                #endregion

                #region  credit note receipt ledger 
                if (oth_amt > 0)
                {
                    temp.Rows.Add("cn", My.group["cn"], "", oth_amt.ToString("0.00"), "Sales", bill_no, "sales", "", date);
                }

                #endregion

                #region  bank receipt ledger 
                if (bank_amt > 0)
                {
                    string bank_id = "0";//p.ddl_bank_details.SelectedValue.ToString();
                    temp.Rows.Add(bank_id, My.group[bank_id], "", bank_amt.ToString("0.00"), "Sales", bill_no, "sales", "", date);
                    if (party_id != "cash")
                    {
                        temp.Rows.Add(party_id, My.group[party_id], bank_amt.ToString("0.00"), "", "Sales", bill_no, bank_id, "", date);
                    }
                }
                #endregion

                #region  extra exp and transport ledger
                DataTable exp_dt = My.dataTable("select * from sales_extracharge_details where    firm ='" + My.firm_id() + "' and session='" + My.get_session() + "' and bill_no='" + bill_no + "'");
                foreach (DataRow dr in exp_dt.Rows)
                {
                    net_taxable += My.toDouble(dr["Taxable"]);
                    temp.Rows.Add("extra_charge_sales", My.group["extra_charge_sales"], dr["Taxable"], "", "Sales", bill_no, "sales", dr["description"] + " on invoice no " + bill_no, date);
                    extra_tax += My.toDouble(dr["GST_value"]);
                }

                DataTable tp_dt = My.dataTable("select * from sales_tranportation_details where    firm ='" + My.firm_id() + "' and session='" + My.get_session() + "' and bill_no='" + bill_no + "'");
                foreach (DataRow dr in tp_dt.Rows)
                {
                    net_taxable += My.toDouble(dr["Taxable"]); ;
                    temp.Rows.Add("transport_in", My.group["transport_in"], dr["Taxable"], "", "Sales", bill_no, "sales", "Transportation for " + dr[0] + " on invoice no " + bill_no, date);
                    transsport_tax += My.toDouble(dr["GST_value"]);
                }
                #endregion

                #region round off,cess transport tax  extra exp tax
                if (My.toDouble(txt_round.Text) < 0)
                {
                    temp.Rows.Add("round_off", My.group["round_off"], "", (My.toDouble(txt_round.Text) * (-1)).ToString("0.00"), "Sales", bill_no, "sales", "", date);
                }
                else if (My.toDouble(txt_round.Text) > 0)
                {
                    temp.Rows.Add("round_off", My.group["round_off"], txt_round.Text, "", "Sales", bill_no, "sales", "", date);
                }
                if (My.toDouble(txt_total_tax.Text) + transsport_tax + extra_tax > 0)
                {

                    //if (state_code == My.state_code)
                    //{
                    double tax = (My.toDouble(txt_total_tax.Text) + transsport_tax + extra_tax) / 2;
                    temp.Rows.Add("sgst", My.group["sgst"], tax.ToString("0.00"), "", "Sales", bill_no, "sales", "", date);
                    temp.Rows.Add("cgst", My.group["cgst"], tax.ToString("0.00"), "", "Sales", bill_no, "sales", "", date);
                    net_cgst = tax;
                    net_sgst = tax;
                    //}
                    //else
                    //{
                    //    temp.Rows.Add("igst", My.group["igst"], ((My.toDouble(txt_total_tax.Text) + transsport_tax + extra_tax)).ToString("0.00"), "", "Sales", bill_no, "sales", "", date);
                    //    net_igst = My.toDouble(txt_total_tax.Text) + transsport_tax + extra_tax;
                    //}
                }
                //txt_total_cess.Text
                string txt_total_cess = "0.00";
                if ("0.00" != "0.00")
                {
                    net_cess = My.toDouble(txt_total_cess);
                    temp.Rows.Add("cess", My.group["cess"], txt_total_cess, "", "Sales", bill_no, "sales", "", date);
                }
                #endregion
            }
            catch (Exception ex)
            {

            }




            int count = 0;
            int r_count = dt.Rows.Count;
            if (temp.Rows.Count >= r_count)
            {
                foreach (DataRow r in temp.Rows)
                {
                    if (count < r_count)
                    {
                        add_to_ledger(r, dt.Rows[count], ismodify);
                        DataRow dr1 = dt.Rows[count];
                        // My.remove_previous_ledger(dr1["Account_id"], dr1["Credit"], dr1["Debit"], dr1["Session"]);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        add_to_ledger(r, dr, ismodify);
                        dt.Rows.Add(dr);
                        count++;
                    }

                    count++;
                }
            }
            else if (temp.Rows.Count < dt.Rows.Count)
            {
                foreach (DataRow r in temp.Rows)
                {
                    add_to_ledger(r, dt.Rows[count], ismodify);
                    DataRow dr1 = dt.Rows[count];
                    // My.remove_previous_ledger(dr1["Account_id"], dr1["Credit"], dr1["Debit"], dr1["Session"]);

                    count++;
                }
                for (int i = count; i < r_count; i++)
                {
                    DataRow dr1 = dt.Rows[i];
                    // My.remove_previous_ledger(dr1["Account_id"], dr1["Credit"], dr1["Debit"], dr1["Session"]);
                    dt.Rows[i].Delete();
                }
            }

            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void add_to_ledger(DataRow tr, DataRow dr, bool ismodify)
        {
            string description = "";

            try
            {
                if (ismodify)
                {
                    description = "Sale to " + txt_seal_to.Text + ", Bill No : " + txt_temp_bill_no.Text;
                }
                else
                {
                    description = "Sale to " + txt_seal_to.Text.Split(',')[0] + ", Bill No : " + tr["voucher_no"].ToString();
                }
                if (tr["description"].ToString() != "")
                {
                    description = tr["description"].ToString();
                }
                if (txt_remarks.Text != "")
                {
                    description += "\nRemarks : " + txt_remarks.Text.Trim();
                }
                if (tr["date"].ToString() == "")
                {
                    dr["Date"] = Sale_Purchase.toDateWithTime(txt_payment_date.Text);
                    dr["IDate"] = My.toDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                }
                else
                {
                    dr["Date"] = Sale_Purchase.toDateWithTime(tr["date"].ToString());
                    dr["IDate"] = My.toDateTime(tr["date"].ToString()).ToString("yyyyMMdd");
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.StackTrace);
            }


            dr["Account_id"] = tr["account_id"];
            dr["Description"] = description;
            dr["Credit"] = My.toDouble(tr["credit"]);
            dr["Debit"] = My.toDouble(tr["debit"]);
            dr["Group_id"] = tr["group_id"];
            dr["unique_entry_id"] = ViewState["unique_entry_id"].ToString();
            dr["VoucherNo"] = tr["voucher_no"];
            dr["VoucherType"] = tr["type"];
            dr["firm"] = My.firm_id();
            dr["Session"] = My.get_session();
            dr["Alternet_Account"] = tr["Alternet_Account"];
            dr["time"] = DateTime.Now.ToString("hh:mm:ss tt");
            dr["Bill_from"] = "";
            dr["Created_by"] = ViewState["Admin"].ToString();
            dr["Ref_Voucher_No"] = tr["voucher_no"];
            //dr["Payment_id"] = tr["payment_id"];
            dr["Bill_entry_id"] = ViewState["unique_entry_id"].ToString();

        }

        private object valid(object p)
        {
            try
            {
                string temp = p.ToString();
                return temp;
            }
            catch
            {
                return "";
            }
        }

        protected void rp_std_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    string Item_Code = ((Label)e.Item.FindControl("lbl_Item_Code")).Text;
                    string lbl_Unit_id = ((Label)e.Item.FindControl("lbl_Unit_id")).Text;
                    bool chekitemad = check_add_item_inthecart(Item_Code, lbl_Unit_id);
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trR");
                    if (My.toInt(((Label)e.Item.FindControl("lbl_Quantity")).Text) == 0)
                    {
                        ((LinkButton)e.Item.FindControl("lnk_select")).Visible = false;

                        tr.Attributes.Add("style", "background-color:#d81a35d4;color:#FFFFFF;");

                    }
                    else if (chekitemad == false)
                    {
                        ((LinkButton)e.Item.FindControl("lnk_select")).Visible = false;
                        tr.Attributes.Add("style", "background-color:#d81a35d4;color:#FFFFFF;");
                    }
                    else
                    {
                        ((LinkButton)e.Item.FindControl("lnk_select")).Visible = true;
                        tr.Attributes.Add("style", "background-color:#fff;color:#000;");

                    }






                }
            }
            catch { }
        }

        private bool check_add_item_inthecart(string item_Code, string Unit_id)
        {


            try
            {
                DataTable dt = My.dataTable("select *,(select  top 1 Unit  from dbo.[unit_master] where unit_id=HMS_Invetory_Sell_details_item_wise.unit_id) as Unit from HMS_Invetory_Sell_details_item_wise where  unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "' and Item_code='" + item_Code + "' and unit_id='" + Unit_id + "' ");

                if (dt.Rows.Count == 0)
                {
                    return true;

                }
                else
                {
                    return false;

                }

            }
            catch
            {
                return false;

            }


        }

        protected void rd_package_wise_CheckedChanged(object sender, EventArgs e)
        {
            hd_saletype.Value = "Package";
        }

        protected void rd_item_wise_CheckedChanged(object sender, EventArgs e)
        {
            hd_saletype.Value = "Item";
        }

        protected void lnk_select_package_Click(object sender, EventArgs e)
        {

            if (ViewState["party_id"] == null)
            {
                Alertme("Please enter valid sale to user", "warning");
                txt_seal_to.Focus();
                return;
            }
            else if (ViewState["party_id"].ToString() == "")
            {
                Alertme("Please enter valid user or something is wrong for your selected use, Please page create and add again product", "warning");
                return;
            }
            if (ViewState["unique_entry_id"].ToString() == "")
            {
                ViewState["unique_entry_id"] = My.unique_id();
            }

            if (btn_add_item.Text == "Add")
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");
                ViewState["Package_id"] = lbl_unique_entry_id.Text;
                string query = "Select *,(select top 1 Item_Name from HMS_Invetory_item_Master where Item_id=HMS_Package_Item_Wise.Item_Code and Unit_id=HMS_Package_Item_Wise.Unit_id) as itemname from HMS_Package_Item_Wise where unique_entry_id='" + lbl_unique_entry_id.Text + "' and Status='Submited'";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Item_Code = dt.Rows[i]["Item_Code"].ToString();
                        string Quantity = dt.Rows[i]["Quantity"].ToString();
                        string Unit_id = dt.Rows[i]["Unit_id"].ToString();
                        string Rate = dt.Rows[i]["Rate"].ToString();
                        string itemname = dt.Rows[i]["itemname"].ToString();

                        string store_id = "2001";
                        string Package_id = lbl_unique_entry_id.Text;
                        add_item_via_package(Item_Code, Unit_id, Quantity, Package_id, Rate, store_id, itemname);
                    }

                }

            }
            else
            {


            }
            myModal2.Visible = false;
            bind_grid_view(ViewState["unique_entry_id"].ToString());

            empty_form();
        }

        private void add_item_via_package(string item_Code, string unit_id, string quantity, string package_id, string Rate, string store_id, string itemname)
        {
            string query = "Select top 1 * from HMS_inventory_purchase_entry_itemwise where Item_Code='" + item_Code + "' and Unit_id='" + unit_id + "' and Status='Submitted'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                double total = My.toDouble((My.toDouble(quantity) * My.toDouble(Rate)).ToString("0.00"));
                double qty = My.toDouble(quantity);
                double rate = My.toDouble(Rate);
                double taxable = total - (0);
                string taxbalevalue = taxable.ToString("0.00");
                double tax_percent = My.toDouble(0);

                double total_tax = My.Round(taxable * tax_percent / 100);



                ViewState["taxable_rate"] = My.Round(taxable / qty);
                string gstvalue = total_tax.ToString("0.00");

                double net = taxable + total_tax;
                string net_total = net.ToString("0.00");



                DataTable it_dt;
                calculate_row();
                if (ViewState["order_id"].ToString() == "")
                {
                    ViewState["order_id"] = My.auto_serialS("order_id");
                }

                Dictionary<string, object> dr = new Dictionary<string, object>();
                dr["Brand_Id"] = dt.Rows[0]["Brand_Id"].ToString();

                dr["Item_code"] = dt.Rows[0]["Item_Code"].ToString();
                dr["Barcode"] = "";
                dr["Rate"] = My.toDouble(Rate).ToString("0.00");


                dr["taxable_rate"] = My.toDouble(taxbalevalue).ToString("0.00");


                dr["rate_before_discount"] = My.toDouble(total).ToString("0.00"); ;
                dr["Quantity"] = quantity;
                dr["Unit_id"] = dt.Rows[0]["Unit_id"].ToString();
                dr["Total"] = My.toDouble(total).ToString("0.00");
                dr["sale_service_type"] = "Seal";

                dr["Branch_Id"] = "1";
                if (My.toDouble(txt_dis_percentage.Text) > 0)
                {
                    dr["Discount"] = My.toDouble("0");
                    dr["Discount_Type"] = "Percent";
                    dr["Discount_Percent"] = "0";
                }
                else
                {
                    dr["Discount"] = My.toDouble("0");
                    dr["Discount_Type"] = "flat";
                    dr["Discount_Percent"] = "0";
                }




                dr["Taxable"] = My.toDouble(taxbalevalue);
                dr["Total_GST"] = My.toDouble(total_tax);
                //if (ViewState["state_code"].ToString() == My.state_code)
                //{
                dr["SGST"] = My.Round(My.toDouble(total_tax) / 2);
                dr["CGST"] = My.Round(My.toDouble(total_tax) / 2);
                dr["IGST"] = "0";
                //}
                //else
                //{
                //    dr["IGST"] = txt_gst_value.Text;
                //    dr["SGST"] = "0";
                //    dr["CGST"] = "0";
                //}
                // }
                dr["NetTotal"] = My.toDouble(net_total);
                //if (rbt_exclusive.IsChecked == true)
                //{
                dr["TaxCalculationType"] = "Exclusive";
                //}
                //else
                //{
                //    dr["TaxCalculationType"] = "Inclusive";
                //}
                dr["Bill_No"] = txt_temp_bill_no.Text;
                dr["Sell_To"] = ViewState["party_id"].ToString();
                dr["Mobile_no"] = txt_mobileno.Text;
                dr["GST_Percent"] = 0;//txt_gst_per.Text;//is_ipd_bill ? "0" : txt_gst_applied.Text;
                dr["Date"] = My.toDateTime(txt_payment_date.Text + " " + mycode.time()).ToString("dd/MMM/yyyy hh:mm:ss tt");
                dr["Idate"] = My.toDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                dr["HSN_Code"] = dt.Rows[0]["Hsn_no"].ToString();
                dr["Status"] = "Pending";
                dr["firm"] = My.firm_id();
                dr["session"] = My.get_session();
                dr["user_id"] = ViewState["Admin"].ToString();
                dr["unique_entry_id"] = ViewState["unique_entry_id"].ToString();
                dr["sale_type"] = "Stock";
                dr["GST_type"] = "0";//
                dr["cess_percent"] = "0";//My.toDouble(txt_cess_applied.Text);
                dr["cess_value"] = "0"; //My.toDouble(txt_cess_value.Text);
                dr["item_type"] = "Goods";
                dr["State_Code"] = ViewState["state_code"].ToString();
                dr["State"] = ViewState["state_Name"].ToString();

                dr["Batch_No"] = dt.Rows[0]["Batch_no"].ToString();
                dr["Exp_Date"] = dt.Rows[0]["Expiry_date"].ToString();
                dr["Manu_Date"] = "";
                dr["salesman_id"] = ViewState["Admin"].ToString();
                dr["size"] = "";
                dr["imei_no"] = "";
                dr["imei_no2"] = "";
                dr["mapping_id"] = "0";
                dr["product_serial"] = "";
                dr["mrp"] = My.toDouble(Rate).ToString("0.00");

                //if (My.bussiness_nature == "Medical Store")
                //{
                //    try
                //    {
                //        dr["Doctor_name"] = txt_doctor_name.Text;
                //        dr["Doctor_id"] = txt_doctor_name.SelectedValue.ToString();
                //    }
                //    catch
                //    {
                //        dr["Doctor_name"] = "Self";
                //        dr["Doctor_id"] = "self";
                //    }
                //}

                dr["Godown_id"] = store_id;
                dr["table_id"] = "0";
                dr["order_id"] = ViewState["order_id"].ToString();
                dr["s_qty"] = "0";
                dr["f_qty"] = "0";
                dr["scheme_in_per"] = "0";//My.toDouble(txt_scheme_percent.Text);
                dr["Size_length"] = "";//txt_flex_size_length.Text;
                dr["Size_width"] = "";//txt_flex_size_breadth.Text;
                dr["rate_per_sqft"] = "";// txt_rate_per_sqft.Text;
                                         //if (txt_flex_size_length.Text != "" && txt_flex_size_breadth.Text != "")
                                         //{
                dr["flex_size"] = "";// txt_flex_size_length.Text + "X" + txt_flex_size_breadth.Text;
                                     //}

                dr["Is_Stock_effected"] = true;
                dr["Item_unique_entry_id"] = My.unique_id();
                dr["stock_item_unique_entry_id"] = My.getstock_item_unique_entry_id(item_Code, unit_id, Rate);
                dr["is_modification"] = false;
                dr["remarks"] = "";
                dr["is_non_taxable"] = false;
                // Cost_Rate, Trade Rate and Sale rate in Prinmary Unit
                dr["PCost_Rate"] = My.toDouble(rate).ToString("0.00"); // item_cost_rate;
                dr["PTrade_Rate"] = My.toDouble(rate).ToString("0.00"); ;
                dr["PSale_Rate"] = My.toDouble(dt.Rows[0]["Purchase_rate"].ToString()).ToString("0.00"); ;
                dr["sec_unit"] = "0"; //sec_unit;
                dr["sec_qty"] = "0";//sec_qty;


                dr["item_size_in_mtr"] = ""; //txt_size_in_mtr1.Text;
                dr["total_item_size_in_mtr"] = ""; //total_size_in_mtr;
                dr["Pur_Type_id"] = "";//pur_type_id;
                #region send data in other mapping table

                #endregion

                dr["sp_status"] = "INSERT";
                dr["user_name"] = ViewState["User_Name"].ToString();
                dr["sale_order_no"] = "";
                dr["with_indent"] = true;
                dr["description"] = itemname;
                dr["Package_id"] = package_id;

                it_dt = My.dataTableSP("sp_HMS_Invetory_insert_Sell_details_item_wise", dr);

                DateTime dat = My.toDateTime(txt_payment_date.Text + " " + mycode.time());
                Sale_Purchase.update_item_account_ledger(item_Code, dr["Unit_id"].ToString(), "Insert", "Pending", My.toDouble(quantity), "Add New item in sale", dr["session"].ToString(), ViewState["unique_entry_id"].ToString(), dr["Item_unique_entry_id"].ToString(), "5", false, dr["Godown_id"].ToString(), dat, My.toDouble(txt_rate.Text), My.toDouble(txt_rate.Text));
            }
        }

        protected void grd_view_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Package_id = (Label)e.Row.FindControl("lbl_Package_id");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");
                if (lbl_Package_id.Text == "")
                {
                    lnkEdit.Visible = true;
                }
                else
                {
                    lnkEdit.Visible = false;
                    if (ViewState["OnlineFromAPP"].ToString() == "Yes")
                    {
                        lnkDel.Visible = false;
                        btn_add_item.Enabled = false;
                    }
                    else
                    {
                        lnkDel.Visible = true;
                    }

                }
            }
        }

        protected void btn_Add_studnt_Click(object sender, EventArgs e)
        {
            string party_id = "";
            lbl_msg2.Text = "";
            My mycodeMy = new My();
            if (ddl_classname.SelectedItem.Text == "Select")
            {
                lbl_msg2.Text = "Please select valid class name ";
                Alertme("Please enter valid class name ", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_add_student();", true);
            }

            else
            {

                /*   DataTable dt = mycodeMy.FillData("SELECT * FROM party_details WHERE mobile='"+ txt_Mobile_no.Text+"'");
                   if (dt.Rows.Count==0)
                       {*/

                party_id = "CM" + My.auto_serialS("party_id");
                hd_party_id.Value = party_id;
                SqlCommand cmd;

                string query = @"INSERT INTO party_details
(
    party_name,
    address,
    mobile,
    gstin,
    party_id,
    date,
    firm,
    Registration_Type,
    State,
    type,
    Care_of,
    class_name,
    Section,
    State_Code
)
VALUES
(
    @party_name,
    @address,
    @mobile,
    @gstin,
    @party_id,
    @date,
    @firm,
    @Registration_Type,
    @State,
    @type,
    @Care_of,
    @class_name,
    @Section,
    @State_Code
)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@party_name", txt_student_name.Text);
                cmd.Parameters.AddWithValue("@address", txt_adress.Text);
                cmd.Parameters.AddWithValue("@mobile", txt_Mobile_no.Text);
                cmd.Parameters.AddWithValue("@gstin", "UnRegistered");
                cmd.Parameters.AddWithValue("@party_id", party_id);
                cmd.Parameters.AddWithValue("@date", mycode.date());
                cmd.Parameters.AddWithValue("@firm", "1");
                cmd.Parameters.AddWithValue("@Registration_Type", "Customer");
                cmd.Parameters.AddWithValue("@State", ViewState["state_Name"].ToString());
                cmd.Parameters.AddWithValue("@type", "Customer");
                cmd.Parameters.AddWithValue("@Care_of", txt_father_name.Text);
                cmd.Parameters.AddWithValue("@class_name", ddl_classname.Text);
                cmd.Parameters.AddWithValue("@Section", txt_section.Text);
                cmd.Parameters.AddWithValue("@State_Code", ViewState["state_code"].ToString());

                if (My.InsertUpdateData(cmd))
                {
                    My.create_student_ledger(party_id, txt_student_name.Text, "", "", "N/A", "N/A", "N/A", txt_Mobile_no.Text, ViewState["state_Name"].ToString(), txt_father_name.Text, mycode.date());
                    txt_seal_to.Text = txt_student_name.Text + " , Class- " + ddl_classname.SelectedItem.Text + " , Sec." + txt_section.Text + " , Customer Id- " + party_id;

                    ViewState["party_id"] = party_id;

                    txt_address.Text = txt_adress.Text + ", State.- " + ViewState["state_Name"].ToString() + ", Mobile- " + txt_Mobile_no.Text;
                    ViewState["party_name"] = txt_student_name.Text;


                    txt_mobileno.Text = txt_Mobile_no.Text;
                    txt_section.Text = "";
                    txt_student_name.Text = "";
                    txt_father_name.Text = "";
                    txt_Mobile_no.Text = "";
                    lbl_msg2.Text = "";
                    txt_adress.Text = "";
                    Alertme("Cutomer information has been saved successfully", "success");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModal", "$('#myModal4').modal('hide');", true);

                }
                /*}
                else
                {
                    lbl_msg2.Text = "Admission no. already exist.";
                    Alertme("Admission no. already exist.", "warning");
                party_id = dt.Rows[0]["party_id"].ToString();
                hd_party_id.Value= dt.Rows[0]["party_id"].ToString();
                txt_seal_to.Text = dt.Rows[0]["party_name"].ToString() + " , Class-" + dt.Rows[0]["class_name"].ToString() + " , Sec.-" + dt.Rows[0]["Section"].ToString() + " , Customer Id-" + party_id;
                ViewState["party_id"] = party_id;
                txt_address.Text = dt.Rows[0]["address"].ToString() + ", State.-" + dt.Rows[0]["State"].ToString() + ", Mobile-" + dt.Rows[0]["mobile"].ToString();
                ViewState["party_name"] = dt.Rows[0]["party_name"].ToString();
                txt_mobileno.Text = txt_Mobile_no.Text;
                txt_section.Text = "";
                txt_student_name.Text = "";
                txt_father_name.Text = "";
                txt_Mobile_no.Text = "";
                lbl_msg2.Text = "";
            }*/





            }


            hdn_new_customer.Value = "0";

        }

        private bool check_duplicate(string adno)
        {
            DataTable dt = My.dataTable(" select admissionserialnumber from admission_registor where admissionserialnumber='" + adno + "' ");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [WebMethod]
        public static List<string> Getcustomername(string PathRooT)
        {
            List<string> itemResult = new List<string>();

            try
            {
                using (SqlConnection con = new SqlConnection(My.conn))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string qry = @"

SELECT 
(
    party_name
    + ' | FATHER NAME - ' + ISNULL(Care_of,'')
    + ' | MOBILE - ' + ISNULL(mobile,'')
    + ' | ID - ' + ISNULL(party_id,'')
) AS party_name

FROM party_details

WHERE 

    type = 'Customer'

    AND ISNULL(mobile,'') <> ''

    AND party_id NOT LIKE '%NNIS%'

    AND
    (
        party_name LIKE '%' + @SearchprojectName + '%'

        OR party_id LIKE '%' + @SearchprojectName + '%'

        OR mobile LIKE '%' + @SearchprojectName + '%'
    )

ORDER BY party_name

";

                        cmd.CommandText = qry;
                        cmd.Connection = con;

                        cmd.Parameters.AddWithValue("@SearchprojectName", PathRooT);

                        con.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            itemResult.Add(dr["party_name"].ToString());
                        }

                        dr.Close();
                    }
                }

                // Agar koi customer nahi mila
                if (itemResult.Count == 0)
                {
                    itemResult.Add("No customer found with this name or mobile number ");
                }
            }
            catch (Exception ex)
            {
                itemResult.Add("Error : " + ex.Message);
            }

            return itemResult;
        }
        protected void txt_Mobile_no_TextChanged(object sender, EventArgs e)
        {
            ViewState["party_id"] = "";
            try
            {
                string seal_to = txt_Mobile_no.Text;
                string[] arrs = seal_to.Split('|');
                string studentname = arrs[0];
                string fathername = arrs[1];
                string mobile = arrs[2];
                string party_id = arrs[3];
                DataTable dt1 = mycode.FillData("Select * from party_details where party_id='" + party_id + "' and Registration_Type='Customer'");
                if (dt1.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_add_student();", true);
                }
                else
                {
                    ViewState["party_id"] = dt1.Rows[0]["party_id"].ToString();
                    txt_seal_to.Text = dt1.Rows[0]["party_name"].ToString() + " , Class-" + dt1.Rows[0]["class_name"].ToString() + " , Sec.-" + dt1.Rows[0]["Section"].ToString() + " , Customer Id-" + ViewState["party_id"].ToString();

                    txt_mobileno.Text = dt1.Rows[0]["mobile"].ToString();
                    hd_party_id.Value = dt1.Rows[0]["party_id"].ToString();
                    txt_address.Text = dt1.Rows[0]["address"].ToString() + ", State.-" + dt1.Rows[0]["State"].ToString() + " Mobile-" + dt1.Rows[0]["mobile"].ToString();
                    ViewState["party_name"] = dt1.Rows[0]["party_name"].ToString();

                    try
                    {
                        DataTable dt2 = mycode.FillData(" Select (sum(convert(float, isnull((Wallet_input_amount),'0'))) -sum(convert(float, isnull((Wallet_Out_amount),'0')))) as avlwallet from Student_Wallet where Adm_no='" + hd_party_id.Value + "'");
                        if (dt2.Rows.Count == 0)
                        {
                            ViewState["avlwallet"] = "0.00";

                        }
                        else
                        {

                            ViewState["avlwallet"] = My.toDouble(dt1.Rows[0]["avlwallet"].ToString());
                        }
                    }
                    catch
                    {
                        ViewState["avlwallet"] = "0.00";

                    }

                    txt_avl_Wallet.Text = ViewState["avlwallet"].ToString();



                    string mobileid = dt1.Rows[0]["mobile"].ToString();
                    string hiddenid = hdn_new_customer.ClientID;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "custalert", @"
setTimeout(function () {

    Swal.fire({
        title: 'Customer Already Exists',
        text: 'This mobile number is already registered. Do you want to continue with this customer or add a new customer?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Continue',
        cancelButtonText: 'Add New',
        allowOutsideClick: false
    }).then((result) => {

        // Continue
        if (result.isConfirmed) {

            Swal.close();

        } 
        // Add New
        else if (result.dismiss === Swal.DismissReason.cancel) {

            
            // set flag
           document.getElementById('hdn_new_customer').value = '1';
$('#txt_Mobile_no').val('');
$('#hdn_new_customer').val('1');
   // sirf mobile number rakho
    document.getElementById('txt_Mobile_no').value = '';
            Swal.close();

            // reopen popup
            setTimeout(function () {

                open_add_student();
 setTimeout(function () {

        document.getElementById('txt_Mobile_no').value = '" + mobile + @"';

    }, 200);

            }, 300);
        }

    });

}, 300);
", true);
                    if (hdn_new_customer.Value == "1")
                    {
                        txt_Mobile_no.Text = mobileid;
                    }
                }

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "open_add_student();", true);
                ViewState["party_id"] = "";

            }
        }
    }
}