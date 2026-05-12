using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.IO;
using System.Data;
using System.Drawing;

namespace school_web.Inventory_management
{
    public partial class Undelivered_Item : System.Web.UI.Page
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
        string studentname = " select top 1 studentname from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=ar.session";

        string classname = "select top 1 class from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=ar.session";


        string rollnumber = "select top 1 rollnumber from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and session=ar.session";

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
                        mycode.bind_all_ddl_with_id_cap_All(ddl_classname, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor");
                        Session["billfrom"] = "5";
                        txt_from_Date.Text = mycode.date();
                        txt_to_Date.Text = mycode.date();
                        bind_datewise();

                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }

        }
        protected void ddl_classname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_classname.SelectedItem.Text == "ALL")
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor");
            }
            else
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from  admission_registor where Class_id='" + ddl_classname.SelectedValue + "'");
            }
        }
        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            bind_datewise();
        }
        private void bind_datewise()
        {
            try
            {
                int idate = My.DateConvertToIdate(txt_from_Date.Text);
                int idate2 = My.DateConvertToIdate(txt_to_Date.Text);
                if (idate > idate2)
                {
                    lbl_heading.Text = "";
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    string query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No   where pt.Idate>=" + idate + " and pt.Idate<=" + idate2 + " and isdb.unique_entry_id in (Select unique_entry_id from HMS_Invetory_Sell_details_item_wise where Is_Stock_Delivered='NotDelivered') order by pt.idate asc ";



                    total_count_grid_list(query);
                    lbl_heading.Text = "From Date" + txt_from_Date.Text + " End Date: " + txt_to_Date.Text;
                }
            }
            catch
            {

            }

        }

        private void total_count_grid_list(string query)
        {
            Session["query"] = query;
            ViewState["query"] = query;
            print1.Visible = false;
            lbl_total_dues.Text = "0.00";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                try
                {
                    if (Session["msg222"] == null)
                    {

                        Alertme("Sorry there are no data list exist", "warning");

                    }
                    else
                    {
                        Alertme(Session["msg222"].ToString(), "success");
                        Session["msg222"] = null;
                    }

                }
                catch
                {

                }
                lbl_fnl_paid.Text = "0.00";
                RPDetails.DataSource = null;
                RPDetails.DataBind();

            }
            else
            {
                String Fnl_paid_amt = Convert.ToDouble(dt.Compute("SUM(NetPayable1)", string.Empty)).ToString("0.00");
                lbl_fnl_paid.Text = Fnl_paid_amt;
                String Duse_Amount1 = Convert.ToDouble(dt.Compute("SUM(Duse_Amount1)", string.Empty)).ToString("0.00");
                lbl_total_dues.Text = Duse_Amount1;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
                print1.Visible = true;






            }
        }

        protected void btn_sale_id_Click(object sender, EventArgs e)
        {
            try
            {
                string query1 = "";
                string query = "";
                if (ddl_classname.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
                {
                    query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber ,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where ar.Session_id='" + My.get_session_id() + "' and isdb.unique_entry_id in (Select unique_entry_id from HMS_Invetory_Sell_details_item_wise where Is_Stock_Delivered='NotDelivered') order by pt.Idate ";
                }
                else if (ddl_classname.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
                {
                    query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where ar.Session_id='" + My.get_session_id() + "' and ar.Class_id='" + ddl_classname.SelectedValue + "' and ar.Section='" + ddl_section.Text + "' and isdb.unique_entry_id in (Select unique_entry_id from HMS_Invetory_Sell_details_item_wise where Is_Stock_Delivered='NotDelivered') order by pt.Idate,ar.rollnumber ";
                }
                else if (ddl_classname.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
                {
                    query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where ar.Session_id='" + My.get_session_id() + "' and ar.Class_id='" + ddl_classname.SelectedValue + "' and isdb.unique_entry_id in (Select unique_entry_id from HMS_Invetory_Sell_details_item_wise where Is_Stock_Delivered='NotDelivered')  order by pt.Idate,ar.rollnumber ";
                }
                else if (ddl_classname.SelectedItem.Text == "ALL" && ddl_section.Text != "ALL")
                {
                    query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where ar.Session_id='" + My.get_session_id() + "'   and ar.Section='" + ddl_section.Text + "' and isdb.unique_entry_id in (Select unique_entry_id from HMS_Invetory_Sell_details_item_wise where Is_Stock_Delivered='NotDelivered') order by pt.Idate,ar.rollnumber ";
                }
                else
                {
                    query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, pt.Total_Paid_Amount),0)) as NetPayable1,(isnull(convert(float, pt.Received_from_Cash),0)) as Cashpayment,(isnull(convert(float, pt.Duse_Amount),0)) as Duse_Amount1,format(pt.Date_time, 'dd/MM/yyyy') as Date1,pt.Total_Paid_Amount,pt.Duse_Amount,pt.Received_from_Cash,pt.Received_from_Bank,pt.Payment_transaction,pt.Bank_Payment_Mode from HMS_Invetory_Sell_details_billwise isdb join HMS_Inventory_Bill_Payment_Tracking pt   on pt.party_id=isdb.party_id and pt.Payment_Vochar_id=isdb.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where ar.Session_id='" + My.get_session_id() + "' and isdb.unique_entry_id in (Select unique_entry_id from HMS_Invetory_Sell_details_item_wise where Is_Stock_Delivered='NotDelivered') order by pt.Idate ";
                }
                total_count_grid_list(query);
                lbl_heading.Text = "Class :" + ddl_classname.SelectedItem.Text + "  Section : " + ddl_section.Text;
            }
            catch
            {

            }
        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export_Undelivered_" + mycode.date() + "_" + mycode.time() + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    pnl_grid.RenderControl(hw);
                    string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        protected void lnk_excel_download1_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Export_Undelieverd_" + mycode.date() + "_" + mycode.time() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                pnl_grid.RenderControl(hw);
                string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }




        #endregion download_in_excel

        protected void GrdView_item_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label lbl_unit_id = (Label)e.Row.FindControl("lbl_unit_id");
                Label lbl_Item_code = (Label)e.Row.FindControl("lbl_Item_code");
                Label lbl_Is_Stock_Delivered = (Label)e.Row.FindControl("lbl_Is_Stock_Delivered");
                Label lbl_status = (Label)e.Row.FindControl("lbl_status");
                Label lbl_avlQuantity = (Label)e.Row.FindControl("lbl_avlQuantity");
                Label lbl_stock_item_unique_entry_id = (Label)e.Row.FindControl("lbl_stock_item_unique_entry_id");
                Label lbl_Quantity = (Label)e.Row.FindControl("lbl_Quantity");
                int get_Available = get_avl_qty(lbl_stock_item_unique_entry_id.Text, lbl_unit_id.Text, lbl_Item_code.Text);
                lbl_avlQuantity.Text = get_Available.ToString();
                CheckBox rowChkBox = (CheckBox)e.Row.FindControl("rowChkBox");

                if (lbl_Is_Stock_Delivered.Text == "NotDelivered")
                {
                    lbl_status.Text = "Not Delivered";
                    rowChkBox.Visible = true;
                    e.Row.BackColor = Color.OrangeRed;

                    if (get_Available >= My.toint(lbl_Quantity.Text))
                    {
                        lbl_status.Text = "Not Delivered";
                        rowChkBox.Visible = true;
                        e.Row.BackColor = Color.OrangeRed;
                    }
                    else
                    {
                        lbl_status.Text = "Not Delivered";
                        rowChkBox.Visible = false;
                        e.Row.BackColor = Color.OrangeRed;
                    }
                }
                else
                {
                    rowChkBox.Visible = false;
                    lbl_status.Text = "Delivered";
                    e.Row.BackColor = Color.Cyan;
                }



            }
        }

        private int get_avl_qty(string stockid, string unit_id, string Item_code)
        {
            DataTable dt = mycode.FillData("select sum(cast(Quantity as int))  from HMS_Inventory_stock_details where Item_Code = '" + Item_code + "' and Unit_id='" + unit_id + "'");
            if (dt.Rows.Count == 0)
            {
                return 0;

            }
            else
            {
                return My.toint(dt.Rows[0][0].ToString());

            }

        }

        protected void lnk_viewitem_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_unique_entry_id1 = (Label)row.FindControl("lbl_unique_entry_id1");
            ViewState["unique_entry_id"] = lbl_unique_entry_id1.Text;
            view_data_item_wise(lbl_unique_entry_id1.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);
        }

        private void view_data_item_wise(string unique_entry_id)
        {
            string query = "select *,(select top 1 mobile from party_details where party_id=t1.Sell_To ) as mobile,(select top 1 address from party_details where party_id=t1.Sell_To ) as address,(select top 1 party_name from party_details where party_id=t1.Sell_To ) as party_name,(select top 1 Item_Name from HMS_Invetory_item_Master where Item_id=t1.Item_Code) as Item_name,(select top 1 Brand_name from HMS_Invetory_Brand_Master where Brand_id=t1.Brand_Id) as Brand_name,(select top 1 Unit from unit_master where unit_id=t1.unit_id and firm=t1.firm) as Unit_name from HMS_Invetory_Sell_details_item_wise t1 where   unique_entry_id='" + unique_entry_id + "' ";

            DataTable dt = My.dataTable(query);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {

                GrdView_item.DataSource = null;
                GrdView_item.DataBind();
            }
            else
            {

                GrdView_item.DataSource = dt;
                GrdView_item.DataBind();

            }

        }

        protected void btn_btn_delver_process_Click(object sender, EventArgs e)
        {
            int growcount = GrdView_item.Rows.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                CheckBox chk = (CheckBox)GrdView_item.Rows[i].FindControl("rowChkBox");
                if (chk.Checked == true)
                {

                    Label lbl_stock_item_unique_entry_id = (Label)GrdView_item.Rows[i].FindControl("lbl_stock_item_unique_entry_id");
                    Label lbl_Item_unique_entry_id = (Label)GrdView_item.Rows[i].FindControl("lbl_Item_unique_entry_id");
                    Label lbl_Quantity = (Label)GrdView_item.Rows[i].FindControl("lbl_Quantity");
                    Label lbl_Item_code = (Label)GrdView_item.Rows[i].FindControl("lbl_Item_code");
                    Label lbl_unit_id = (Label)GrdView_item.Rows[i].FindControl("lbl_unit_id");
                    Label lbl_Rate = (Label)GrdView_item.Rows[i].FindControl("lbl_Rate");
                    bool Deducted_stockstatus = Deducted_stock(lbl_stock_item_unique_entry_id.Text, lbl_Quantity.Text, lbl_Item_code.Text, lbl_unit_id.Text, lbl_Rate.Text);
                    if (Deducted_stockstatus == false)
                    {

                    }
                    else
                    {
                        //My.exeSql("update HMS_Invetory_Sell_details_item_wise set  Is_Stock_Delivered='Delivered',Delivery_date='"+My.getdate1()+"' where Item_unique_entry_id=" + Item_unique_entry_id.Text + " ");
                        SqlCommand cmd;
                        cmd = new SqlCommand("update HMS_Invetory_Sell_details_item_wise set Is_Stock_Delivered=@Is_Stock_Delivered,Delivery_date=@Delivery_date where Item_unique_entry_id=@Item_unique_entry_id ");
                        cmd.Parameters.AddWithValue("@Is_Stock_Delivered", "DeliveredAfter");
                        cmd.Parameters.AddWithValue("@Delivery_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Item_unique_entry_id", lbl_Item_unique_entry_id.Text);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            try
                            {
                                Sale_Purchase.HMS_Item_Account_Ledger_udpate("1", lbl_Item_code.Text, lbl_unit_id.Text, mycode.date(), ViewState["unique_entry_id"].ToString(), lbl_stock_item_unique_entry_id.Text, "0", lbl_Quantity.Text, "Sale Entry");
                            }
                            catch
                            {

                            }
                        }

                    }






                }
                else
                {
                    k++;
                }
            }
            if (k == growcount)
            {
                Alertme("Please select at least one item from item list", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);
            }
            else
            {
                Alertme("item has been successfully delivered", "success");
                Session["msg222"] = "item has been successfully delivered";
                total_count_grid_list(ViewState["query"].ToString());


            }
        }

        public bool Deducted_stock(string stock_item_unique_entry_id, string qty, string Item_code, string unit_id, string Rate)
        {



            bool stockdDeducted = false;
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
    }
}