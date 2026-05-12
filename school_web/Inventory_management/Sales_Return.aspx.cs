using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Transactions;

namespace school_web.Inventory_management
{
    public partial class Sales_Return : System.Web.UI.Page
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
                ViewState["Userid"] = Session["Admin"].ToString();
                ViewState["unique_entry_id"] = "";
                Session["billfromre"] = "1";
                ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
              
                if (!IsPostBack)
                {
                    ViewState["firm_id_N"] = My.get_firm_id();
                    txt_Date.Text = mycode.date();
                    ViewState["No_of_days_return_product"] = Sale_Purchase.get_No_of_days_return();
                }
            }
        }

        #region find data
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (txt_invoice_no.Text == "")
            {
                Alertme("Please enter invoice no.", "warning");
            }
            else
            {
                string query = "Select *,format(isdb.Date, 'dd/MM/yyyy') as Date1,format(isdb.Date, 'yyyyMMdd') as idateDate1 from HMS_Invetory_Sell_details_billwise isdb where isdb.Bill_No='" + txt_invoice_no.Text.Trim() + "' and Branch_id='" + ViewState["Branch_id"].ToString() + "'  ";
                bind_invoice_history(query);

            }

        }

        private void bind_invoice_history(string query)
        {
            pnl_full.Visible = false;
            finaltotal.Visible = false;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry your entered invoice number wrong please enter valid invoice", "warning");
                grd_view.DataSource = null;
                grd_view.DataBind();
            }
            else
            {
                bool chek_retureninvoice = find_returne_invoice();
                if (chek_retureninvoice == true)
                {

                    lbl_adm_no.Text = dt.Rows[0]["party_id"].ToString();
                    if (ViewState["firm_id_N"].ToString() == "NNI-01")
                    {
                        if (My.toint(mycode.idate()) >= 20260510)
                        {
                            string query2 = "select  *,(SELECT TOP 1 Course_Name FROM Add_course_table WHERE course_id=party_details.class_name) AS CLASSNAME from  party_details where    party_id='" + dt.Rows[0]["party_id"].ToString() + "' ";
                            DataTable dt1 = My.dataTable(query2);
                            int rowcount1 = dt1.Rows.Count;
                            if (rowcount1 == 0)
                            {

                            }
                            else
                            {
                                lbl_roll_no.Text = "0";
                                lbl_section.Text =dt1.Rows[0]["CLASSNAME"].ToString() + "/" + dt1.Rows[0]["Section"].ToString();
                                lbl_session.Text = My.get_session();

                                ViewState["Session_id"] = My.get_session_id();
                                 
                            }
                        }
                        else
                        {
                            Dictionary<string, object> dc2 = My.get_student_info(dt.Rows[0]["party_id"].ToString(), dt.Rows[0]["session"].ToString());
                            string studentname = (String)dc2["studentname"];
                            string Admission_no = (String)dc2["Admission_no"];
                            ViewState["Session_id"] = (String)dc2["Session_id"];
                            lbl_session.Text = (String)dc2["Session"];
                            string classname = (String)dc2["classname"];
                            lbl_roll_no.Text = (String)dc2["rollnumber"];
                            lbl_section.Text = classname + "/" + (String)dc2["Section"];
                        }
                    }
                    else
                    {

                        Dictionary<string, object> dc2 = My.get_student_info(dt.Rows[0]["party_id"].ToString(), dt.Rows[0]["session"].ToString());
                        string studentname = (String)dc2["studentname"];
                        string Admission_no = (String)dc2["Admission_no"];
                        ViewState["Session_id"] = (String)dc2["Session_id"];
                        lbl_session.Text = (String)dc2["Session"];
                        string classname = (String)dc2["classname"];
                        lbl_roll_no.Text = (String)dc2["rollnumber"];
                        lbl_section.Text = classname + "/" + (String)dc2["Section"];
                    }



                    lbl_invoice_no.Text = dt.Rows[0]["Bill_No"].ToString();
                    lbl_date.Text = dt.Rows[0]["Date1"].ToString();
                    lbl_sale_by.Text = mycode.get_user(dt.Rows[0]["user_id"].ToString());
                    get_payment_tracking(dt.Rows[0]["party_id"].ToString(), dt.Rows[0]["Bill_No"].ToString());
                    string unique_entry_id = dt.Rows[0]["unique_entry_id"].ToString();
                    string date = dt.Rows[0]["Date1"].ToString();
                    string idate = dt.Rows[0]["idateDate1"].ToString();
                    int daycount = mycode.get_no_day_towdateselection(date, txt_Date.Text);
                    if (ViewState["No_of_days_return_product"].ToString() == "0")
                    {
                        string query2 = " SELECT t1.*,t2.Item_Name,t4.Brand_name,t3.unit FROM HMS_Invetory_Sell_details_item_wise  t1 join HMS_Invetory_item_Master t2 on t1.Item_Code=t2.Item_id join unit_master t3 on  t1.unit_id=t3.unit_id and t1.firm=t3.firm  join  HMS_Invetory_Brand_Master t4 on t1.Brand_Id=t4.Brand_id where t1.unique_entry_id='" + unique_entry_id + "' and t1.Branch_id='" + ViewState["Branch_id"].ToString() + "'    ";

                        DataTable dt1 = mycode.FillData(query2);
                        ViewState["item_dt"] = dt1;
                        if (dt1.Rows.Count == 0)
                        {

                            grd_view.DataSource = null;
                            grd_view.DataBind();

                        }
                        else
                        {

                            pnl_full.Visible = true;
                            finaltotal.Visible = true;
                            grd_view.DataSource = dt1;
                            grd_view.DataBind();
                            calculate_total();
                        }

                    }
                    else
                    {



                        if (My.toInt(ViewState["No_of_days_return_product"].ToString()) >= daycount)
                        {

                            string query2 = " SELECT t1.*,t2.Item_Name,t4.Brand_name,t3.unit FROM HMS_Invetory_Sell_details_item_wise  t1 join HMS_Invetory_item_Master t2 on t1.Item_Code=t2.Item_id join unit_master t3 on  t1.unit_id=t3.unit_id and t1.firm=t3.firm  join  HMS_Invetory_Brand_Master t4 on t1.Brand_Id=t4.Brand_id where t1.unique_entry_id='" + unique_entry_id + "' and t1.Branch_id='" + ViewState["Branch_id"].ToString() + "'    ";

                            DataTable dt1 = mycode.FillData(query2);
                            ViewState["item_dt"] = dt1;
                            if (dt1.Rows.Count == 0)
                            {

                                grd_view.DataSource = null;
                                grd_view.DataBind();

                            }
                            else
                            {

                                pnl_full.Visible = true;
                                finaltotal.Visible = true;
                                grd_view.DataSource = dt1;
                                grd_view.DataBind();
                                calculate_total();
                            }

                        }
                        else
                        {
                            int remaindays = (My.toInt(ViewState["No_of_days_return_product"].ToString()) - daycount) * (-1);
                            string msg = "Dear User, your return period has expired, so unfortunately, you are unable to return the product.The return deadline was " + remaindays + " days ago.";
                            Alertme(msg, "warning");
                        }
                    }
                }
                else
                {

                    Alertme("Sorry this invoice number already returned", "warning");
                }

            }
        }

        private bool find_returne_invoice()
        {
            DataTable dt = mycode.FillData("Select * from HMS_Invetory_Sale_Returen_Bill_wise where Old_Bill='" + txt_invoice_no.Text + "'     ");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void get_payment_tracking(string admino, string Bill_No)
        {
            DataTable dt = mycode.FillData("Select * from HMS_Inventory_Bill_Payment_Tracking where party_id='" + admino + "' and  Bill_No='" + "' and Branch_id='" + ViewState["Branch_id"].ToString() + Bill_No + "' and Payment_Vochar_id='" + Bill_No + "'   ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_payment_mode.Text = dt.Rows[0]["Bank_Payment_Mode"].ToString();
            }
        }
        protected void rowChkBox_CheckedChanged(object sender, EventArgs e)
        {
            calculate_total();
        }
        private void calculate_total()
        {
            double total_qty = 0;
            double total = 0, net_total = 0;
            double taxable = 0;
            double totalgst = 0;
            double gst = 0;

            for (int i = 0; i < grd_view.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grd_view.Rows[i].FindControl("rowChkBox");
                if (chk.Checked == true)
                {
                    lbl_totl_items.Text = grd_view.Rows.Count.ToString();
                    string txtqty = ((TextBox)grd_view.Rows[i].FindControl("txt_qty")).Text;
                    total_qty += My.toDouble(txtqty);
                    string lblrate = ((Label)grd_view.Rows[i].FindControl("lbl_rate")).Text;
                    total += My.toDouble(lblrate) * My.toDouble(txtqty);

                    string lblgst_per = ((Label)grd_view.Rows[i].FindControl("lbl_gst_per")).Text;
                    string lblgst_value = ((Label)grd_view.Rows[i].FindControl("lbl_gst_value")).Text;
                    string lblNet_total = ((Label)grd_view.Rows[i].FindControl("lbl_Net_total")).Text;


                    gst = My.toDouble(lblgst_per);

                    taxable += My.toDouble(lblrate) * My.toDouble(txtqty);
                    net_total += (My.toDouble(lblrate) * My.toDouble(txtqty)) + (((My.toDouble(lblrate) * My.toDouble(txtqty)) * gst / 100));
                    totalgst += ((My.toDouble(lblrate) * My.toDouble(txtqty)) * gst / 100);
                }
            }

            //DataTable item_dt = (DataTable)ViewState["item_dt"];
            //foreach (DataRow dr in item_dt.Rows)
            //{
            //    total_qty += My.toDouble(dr["Quantity"]);
            //    total += My.toDouble(dr["Rate"]) * My.toDouble(dr["Quantity"]);
            //    gst = My.toDouble(dr["GST_Percent"]);
            //    taxable += My.toDouble(dr["Rate"]) * My.toDouble(dr["Quantity"]);
            //    net_total += (My.toDouble(dr["Rate"]) * My.toDouble(dr["Quantity"])) + (((My.toDouble(dr["Rate"]) * My.toDouble(dr["Quantity"])) * gst / 100));
            //    totalgst += ((My.toDouble(dr["Rate"]) * My.toDouble(dr["Quantity"])) * gst / 100);

            //}

            lbl_total_.Text = total.ToString("0.00");
            lbl_total_taxable.Text = taxable.ToString("0.00");
            lbl_total_gst_value.Text = totalgst.ToString("0.00");
            double sub_total = net_total;
            lbl_round_off.Text = (My.Round(sub_total, 0) - My.Round(sub_total, 2)).ToString("0.00");
            lbl_grand_total.Text = My.Round(net_total, 0).ToString("0.00");
            lbl_total_qty.Text = total_qty.ToString();
        }

        protected void grd_view_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl_qty = (Label)e.Row.FindControl("lbl_qty");
                    TextBox txtqty = (TextBox)e.Row.FindControl("txt_qty");
                    Label lbl_rate = (Label)e.Row.FindControl("lbl_rate");
                    Label lbl_gst_per = (Label)e.Row.FindControl("lbl_gst_per");
                    Label lbl_gst_value = (Label)e.Row.FindControl("lbl_gst_value");
                    Label lbl_Net_total = (Label)e.Row.FindControl("lbl_Net_total");

                    double rate = My.toDouble(lbl_rate.Text);
                    double qty = My.toDouble(txtqty.Text);
                    double tax_percent = My.toDouble(lbl_gst_per.Text);
                    double total = rate * qty;


                    double taxable = total;
                    double total_tax = My.Round(taxable * tax_percent / 100);
                    double total_cess = 0;
                    double tcs_per = 0;
                    double net = taxable + total_tax + total_cess;
                    double total_tcs = My.Round((net * tcs_per / 100), 2);
                    lbl_gst_value.Text = total_tax.ToString("0.00");
                    lbl_Net_total.Text = net.ToString("0.00");


                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
        #endregion

        protected void txt_qty_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GridViewRow row = (GridViewRow)txt.NamingContainer;
                Label lbl_qty = (Label)row.FindControl("lbl_qty");
                if (txt.Text == "" || txt.Text == "0")
                {
                    txt.Text = lbl_qty.Text;
                    return;
                }

                //TextBox txt_qty = (TextBox)row.FindControl("txt_qty");
                Label lbl_rate = (Label)row.FindControl("lbl_rate");
                Label lbl_gst_per = (Label)row.FindControl("lbl_gst_per");
                Label lbl_gst_value = (Label)row.FindControl("lbl_gst_value");
                Label lbl_Net_total = (Label)row.FindControl("lbl_Net_total");
                if (My.toDouble(txt.Text) > My.toDouble(lbl_qty.Text))
                {
                    txt.Text = lbl_qty.Text;
                    return;
                }

                double rate = My.toDouble(lbl_rate.Text);
                double qty = My.toDouble(txt.Text);
                double tax_percent = My.toDouble(lbl_gst_per.Text);
                double total = rate * qty;


                double taxable = total;
                double total_tax = My.Round(taxable * tax_percent / 100);
                double total_cess = 0;
                double tcs_per = 0;
                double net = taxable + total_tax + total_cess;
                double total_tcs = My.Round((net * tcs_per / 100), 2);
                lbl_gst_value.Text = total_tax.ToString("0.00");
                lbl_Net_total.Text = net.ToString("0.00");



                calculate_after_modify();
            }
            catch (Exception ex)
            {

            }
        }



        private void calculate_after_modify()
        {
            double total_qty1 = 0;
            double total1 = 0, net_total1 = 0;
            double taxable1 = 0;
            double totalgst1 = 0;
            double gst1 = 0;
            for (int i = 0; i < grd_view.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grd_view.Rows[i].FindControl("rowChkBox");
                if (chk.Checked == true)
                {
                    string txtqty = ((TextBox)grd_view.Rows[i].FindControl("txt_qty")).Text;
                    string lblrate = ((Label)grd_view.Rows[i].FindControl("lbl_rate")).Text;
                    string lblgst_per = ((Label)grd_view.Rows[i].FindControl("lbl_gst_per")).Text;
                    string lblgst_value = ((Label)grd_view.Rows[i].FindControl("lbl_gst_value")).Text;
                    string lblNet_total = ((Label)grd_view.Rows[i].FindControl("lbl_Net_total")).Text;

                    total_qty1 += My.toDouble(txtqty);
                    total1 += My.toDouble(lblrate) * My.toDouble(txtqty);
                    gst1 = My.toDouble(lblgst_per);
                    taxable1 += My.toDouble(lblrate) * My.toDouble(txtqty);
                    net_total1 += (My.toDouble(lblrate) * My.toDouble(txtqty)) + (((My.toDouble(lblrate) * My.toDouble(txtqty)) * gst1 / 100));
                    totalgst1 += ((My.toDouble(lblrate) * My.toDouble(txtqty)) * gst1 / 100);
                }
                else
                {

                }

            }


            lbl_totl_items.Text = grd_view.Rows.Count.ToString();
            lbl_total_.Text = total1.ToString("0.00");
            lbl_total_taxable.Text = taxable1.ToString("0.00");
            lbl_total_gst_value.Text = totalgst1.ToString("0.00");

            //double sub_total = net_total - flat_disc + tcs;
            double sub_total = net_total1;
            lbl_round_off.Text = (My.Round(sub_total, 0) - My.Round(sub_total, 2)).ToString("0.00");
            lbl_grand_total.Text = My.Round(net_total1, 0).ToString("0.00");
            lbl_total_qty.Text = total_qty1.ToString();
        }



        #region final submit
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            bool finalsubmit = true;
            int growcount = grd_view.Rows.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                CheckBox chk = (CheckBox)grd_view.Rows[i].FindControl("rowChkBox");
                Label lbl_Item_name = (Label)grd_view.Rows[i].FindControl("lbl_Item_name");
                Label lbl_Unit_id = (Label)grd_view.Rows[i].FindControl("lbl_Unit_id");
                Label lbl_Item_Code = (Label)grd_view.Rows[i].FindControl("lbl_Item_Code");
                Label lbl_Brand_Id = (Label)grd_view.Rows[i].FindControl("lbl_Brand_Id");
                DropDownList ddl_returnregion = (DropDownList)grd_view.Rows[i].FindControl("ddl_returnregion");
                Label lbl_stock_item_unique_entry_id = (Label)grd_view.Rows[i].FindControl("lbl_stock_item_unique_entry_id");
                if (chk.Checked == true)
                {
                    if (ddl_returnregion.Text == "Select")
                    {
                        Alertme("Plese select returen type of item name is  " + lbl_Item_name.Text, "warning");
                        finalsubmit = false;
                        break;
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

                return;
            }
            else
            {
                // data cheked
                if (finalsubmit == true)
                {
                    if (ddl_Return_Mode.Text == "Select")
                    {
                        Alertme("Please select at least one item from item list", "warning");
                    }

                    else
                    {
                        if (ddl_Return_Mode.Text == "Cash")
                        {
                            if (txt_remarks.Text == "")
                            {
                                Alertme("Please enter remarks", "warning");
                            }
                            else
                            {
                                send_data_iteme_wise_and_bill_wise();
                            }

                        }
                        else
                        {
                            if (txt_remarks.Text == "")
                            {
                                Alertme("Please enter remarks", "warning");
                            }
                            else
                            {
                                send_data_iteme_wise_and_bill_wise();
                            }
                        }
                    }

                }
                else
                {
                    //Alertme("Something is wrong", "warning");
                }


            }
        }

        private void send_data_iteme_wise_and_bill_wise()
        {
            try
            {



                if (ViewState["unique_entry_id"].ToString() == "")
                {
                    ViewState["unique_entry_id"] = My.unique_id();
                }
                bool flag = false;
                string slipno = "";

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    DateTime date;
                    date = My.toDateTime(txt_Date.Text + " " + mycode.time());
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    slipno = payments.invoice_return_sale("sale_returen", con);
                    int growcount = grd_view.Rows.Count;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)grd_view.Rows[i].FindControl("rowChkBox");
                        Label lbl_Item_name = (Label)grd_view.Rows[i].FindControl("lbl_Item_name");
                        Label lbl_Unit_id = (Label)grd_view.Rows[i].FindControl("lbl_Unit_id");
                        Label lbl_Item_Code = (Label)grd_view.Rows[i].FindControl("lbl_Item_Code");
                        Label lbl_Brand_Id = (Label)grd_view.Rows[i].FindControl("lbl_Brand_Id");
                        DropDownList ddl_returnregion = (DropDownList)grd_view.Rows[i].FindControl("ddl_returnregion");
                        TextBox txt_qty = (TextBox)grd_view.Rows[i].FindControl("txt_qty");
                        Label lbl_rate = (Label)grd_view.Rows[i].FindControl("lbl_rate");
                        Label lbl_gst_per = (Label)grd_view.Rows[i].FindControl("lbl_gst_per");
                        Label lbl_gst_value = (Label)grd_view.Rows[i].FindControl("lbl_gst_value");
                        Label lbl_Net_total = (Label)grd_view.Rows[i].FindControl("lbl_Net_total");


                        Label lbl_stock_item_unique_entry_id = (Label)grd_view.Rows[i].FindControl("lbl_stock_item_unique_entry_id");
                        if (chk.Checked == true)
                        {
                            insert_data_item_wise(slipno, lbl_Unit_id.Text, lbl_Item_Code.Text, lbl_Brand_Id.Text, lbl_stock_item_unique_entry_id.Text, txt_qty.Text, lbl_rate.Text, lbl_gst_per.Text, lbl_gst_value.Text, lbl_Net_total.Text, ddl_returnregion.Text, date, con);
                        }
                    }

                    insert_data_bill_wise_insert(slipno, date, con);
                    if (ddl_Return_Mode.Text == "Wallet") ;
                    {
                        save_data_student_wallet(slipno, date, con);
                    }

                    con.Close();
                    scope.Complete();
                    flag = true;
                }

                if (flag == true)
                {


                    string Uri = "Slip/Print_Return_Sale_slip.aspx?unique_entry_id=" + ViewState["unique_entry_id"].ToString() + "&partyid=" + lbl_adm_no.Text + "";
                    Response.Redirect(Uri, false);

                    Alertme("Item has been successfully returned", "warning");
                    lbl_session.Text = "";
                    lbl_roll_no.Text = "";
                    lbl_section.Text = "";
                    lbl_invoice_no.Text = "";
                    lbl_date.Text = "";
                    lbl_sale_by.Text = "";
                    lbl_adm_no.Text = "";
                    pnl_full.Visible = flag;
                    txt_Date.Text = mycode.date();
                    grd_view.DataSource = null;
                    grd_view.DataBind();
                }

            }
            catch (Exception ex)
            {
                Alertme(ex.ToString(), "warning");
            }
        }

        private void save_data_student_wallet(string slipno, DateTime date, SqlConnection con)
        {
            SqlCommand cmd;

            string query = "INSERT INTO Student_Wallet (Adm_no,Session_id,Wallet_input_amount,Wallet_Out_amount,Date_of_entry,Remakes,slipno,Mode) values (@Adm_no,@Session_id,@Wallet_input_amount,@Wallet_Out_amount,@Date_of_entry,@Remakes,@slipno,@Mode)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Adm_no", lbl_adm_no.Text);
            cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_id"].ToString());
            cmd.Parameters.AddWithValue("@Wallet_input_amount", lbl_grand_total.Text.Trim());
            cmd.Parameters.AddWithValue("@Wallet_Out_amount", "0.00");
            cmd.Parameters.AddWithValue("@Date_of_entry", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@Remakes", txt_remarks.Text);
            cmd.Parameters.AddWithValue("@slipno", slipno);
            cmd.Parameters.AddWithValue("@Mode", "Sale Return");
            if (payments.InsertUpdateData(cmd, con))
            {

            }
        }

        private void insert_data_bill_wise_insert(string slipno, DateTime date, SqlConnection con)
        {

            DataTable dt = payments.dataTable("select * from dbo.[HMS_Invetory_Sale_Returen_Bill_wise] WHERE Old_Bill='" + lbl_invoice_no.Text + "'  and Branch_id='" + ViewState["Branch_id"].ToString() + "' ", con);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;

                string query = "INSERT INTO HMS_Invetory_Sale_Returen_Bill_wise (New_Bill,Old_Bill,Party_id,User_id,Returen_date_time,Remarks_Returen,Total_items,Total_qty,Total_taxable,Total_gst_value,Round_off,Grand_total,Pay_Mode,Pay_Tran_id,Pay_Amount,Branch_id,unique_entry_id,Session_id) values (@New_Bill,@Old_Bill,@Party_id,@User_id,@Returen_date_time,@Remarks_Returen,@Total_items,@Total_qty,@Total_taxable,@Total_gst_value,@Round_off,@Grand_total,@Pay_Mode,@Pay_Tran_id,@Pay_Amount,@Branch_id,@unique_entry_id,@Session_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@New_Bill", slipno);
                cmd.Parameters.AddWithValue("@Old_Bill", lbl_invoice_no.Text);
                cmd.Parameters.AddWithValue("@Party_id", lbl_adm_no.Text);
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Returen_date_time", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
                cmd.Parameters.AddWithValue("@Remarks_Returen", txt_remarks.Text);
                cmd.Parameters.AddWithValue("@Total_items", lbl_totl_items.Text);
                cmd.Parameters.AddWithValue("@Total_qty", lbl_total_qty.Text);
                cmd.Parameters.AddWithValue("@Total_taxable", lbl_total_taxable.Text);
                cmd.Parameters.AddWithValue("@Total_gst_value", lbl_total_gst_value.Text);
                cmd.Parameters.AddWithValue("@Round_off", lbl_round_off.Text);
                cmd.Parameters.AddWithValue("@Grand_total", lbl_grand_total.Text);
                cmd.Parameters.AddWithValue("@Pay_Mode", ddl_Return_Mode.Text);
                cmd.Parameters.AddWithValue("@Pay_Tran_id", txt_transaction_id.Text);
                cmd.Parameters.AddWithValue("@Pay_Amount", lbl_grand_total.Text);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                cmd.Parameters.AddWithValue("@unique_entry_id", ViewState["unique_entry_id"].ToString());
                cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_id"].ToString());

                if (payments.InsertUpdateData(cmd, con))
                {
                }

            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd1;
                string query = "Update HMS_Invetory_Sale_Returen_Bill_wise set New_Bill=@New_Bill,Old_Bill=@Old_Bill,Party_id=@Party_id,User_id=@User_id,Returen_date_time=@Returen_date_time,Remarks_Returen=@Remarks_Returen,Total_items=@Total_items,Total_qty=@Total_qty,Total_taxable=@Total_taxable,Total_gst_value=@Total_gst_value,Round_off=@Round_off,Grand_total=@Grand_total,Pay_Mode=@Pay_Mode,Pay_Tran_id=@Pay_Tran_id,Pay_Amount=@Pay_Amount,Branch_id=@Branch_id where Id = @Id";
                cmd1 = new SqlCommand(query);
                cmd1.Parameters.AddWithValue("@New_Bill", slipno);
                cmd1.Parameters.AddWithValue("@Old_Bill", lbl_invoice_no.Text);
                cmd1.Parameters.AddWithValue("@Party_id", lbl_adm_no.Text);
                cmd1.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd1.Parameters.AddWithValue("@Returen_date_time", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
                cmd1.Parameters.AddWithValue("@Remarks_Returen", txt_remarks.Text);
                cmd1.Parameters.AddWithValue("@Total_items", lbl_totl_items.Text);
                cmd1.Parameters.AddWithValue("@Total_qty", lbl_total_qty.Text);
                cmd1.Parameters.AddWithValue("@Total_taxable", lbl_total_taxable.Text);
                cmd1.Parameters.AddWithValue("@Total_gst_value", lbl_total_gst_value.Text);
                cmd1.Parameters.AddWithValue("@Round_off", lbl_round_off.Text);
                cmd1.Parameters.AddWithValue("@Grand_total", lbl_grand_total.Text);
                cmd1.Parameters.AddWithValue("@Pay_Mode", ddl_Return_Mode.Text);

                if (ddl_Return_Mode.Text == "Cash")
                {
                    cmd1.Parameters.AddWithValue("@Pay_Tran_id", "N/A");
                }
                else if (ddl_Return_Mode.Text == "Wallet")
                {
                    cmd1.Parameters.AddWithValue("@Pay_Tran_id", "N/A");
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@Pay_Tran_id", txt_transaction_id.Text);
                }

                cmd1.Parameters.AddWithValue("@Pay_Amount", lbl_grand_total.Text);
                cmd1.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                cmd1.Parameters.AddWithValue("@Id", id);

                if (payments.InsertUpdateData(cmd1, con))
                {
                }

            }
        }



        private void insert_data_item_wise(string newslipno, string Unit_id, string Item_Code, string Brand_Id, string stock_item_unique_entry_id, string qty, string rate, string gst_per, string gst_value, string Net_total, string returnregion, DateTime date, SqlConnection con)
        {
            double rate1 = My.toDouble(rate);
            double qty1 = My.toDouble(qty);
            double total = rate1 * qty1;





            DataTable dt = payments.dataTable("select * from dbo.[HMS_Invetory_Sale_Returen_Item_wise] WHERE Old_Bill='" + lbl_invoice_no.Text + "' and Item_code='" + Item_Code + "' and unit_id='" + Unit_id + "' and Brand_Id='" + Brand_Id + "' and Branch_id='" + ViewState["Branch_id"].ToString() + "'", con);
            if (dt.Rows.Count == 0)
            {

                SqlCommand cmd;
                string query = "INSERT INTO HMS_Invetory_Sale_Returen_Item_wise (Item_code,unit_id,Brand_Id,Rate,Quantity,Total_Taxable,GST_Percent,GST_Value,Net_Total,stock_item_unique_entry_id,New_Bill,Old_Bill,Party_id,User_id,Remarks_Returen,Returen_date_time,Stock_transfer,Branch_id,unique_entry_id) values (@Item_code,@unit_id,@Brand_Id,@Rate,@Quantity,@Total_Taxable,@GST_Percent,@GST_Value,@Net_Total,@stock_item_unique_entry_id,@New_Bill,@Old_Bill,@Party_id,@User_id,@Remarks_Returen,@Returen_date_time,@Stock_transfer,@Branch_id,@unique_entry_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Item_code", Item_Code);
                cmd.Parameters.AddWithValue("@unit_id", Unit_id);
                cmd.Parameters.AddWithValue("@Brand_Id", Brand_Id);
                cmd.Parameters.AddWithValue("@Rate", rate1.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Quantity", qty);
                cmd.Parameters.AddWithValue("@Total_Taxable", total.ToString("0.00"));
                cmd.Parameters.AddWithValue("@GST_Percent", gst_per);
                cmd.Parameters.AddWithValue("@GST_Value", gst_value);
                cmd.Parameters.AddWithValue("@Net_Total", Net_total);
                cmd.Parameters.AddWithValue("@stock_item_unique_entry_id", stock_item_unique_entry_id);
                cmd.Parameters.AddWithValue("@New_Bill", newslipno);
                cmd.Parameters.AddWithValue("@Old_Bill", lbl_invoice_no.Text);
                cmd.Parameters.AddWithValue("@Party_id", lbl_adm_no.Text);
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Remarks_Returen", returnregion);
                cmd.Parameters.AddWithValue("@Returen_date_time", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
                cmd.Parameters.AddWithValue("@Stock_transfer", "Pending");
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                cmd.Parameters.AddWithValue("@unique_entry_id", ViewState["unique_entry_id"].ToString());

                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd1;
                string query = "update HMS_Invetory_Sale_Returen_Item_wise set Rate=@Rate,Quantity=@Quantity,Total_Taxable=@Total_Taxable,GST_Percent=@GST_Percent,GST_Value=@GST_Value,New_Bill=@New_Bill,Old_Bill=@Old_Bill,Remarks_Returen=@Remarks_Returen,Returen_date_time=@Returen_date_time where Id=@Id  ";
                cmd1 = new SqlCommand(query);
                cmd1.Parameters.AddWithValue("@Rate", rate1.ToString("0.00"));
                cmd1.Parameters.AddWithValue("@Quantity", qty);
                cmd1.Parameters.AddWithValue("@Total_Taxable", total.ToString("0.00"));
                cmd1.Parameters.AddWithValue("@GST_Percent", gst_per);
                cmd1.Parameters.AddWithValue("@GST_Value", gst_value);
                cmd1.Parameters.AddWithValue("@Net_Total", Net_total);
                cmd1.Parameters.AddWithValue("@New_Bill", newslipno);
                cmd1.Parameters.AddWithValue("@Old_Bill", lbl_invoice_no.Text);
                cmd1.Parameters.AddWithValue("@Remarks_Returen", returnregion);
                cmd1.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd1.Parameters.AddWithValue("@Returen_date_time", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
                cmd1.Parameters.AddWithValue("@Id", id);
                if (payments.InsertUpdateData(cmd1, con))
                {
                }

            }

        }
        #endregion

    }
}