using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Inventory_management.Slip
{
    public partial class Print_Sale_slip : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string unique_entry_id = Request.QueryString["unique_entry_id"];
                string party_id = Request.QueryString["partyid"];
                ViewState["firm_id_N"] = My.get_firm_id();
                if (!string.IsNullOrEmpty(unique_entry_id))
                {

                    ViewState["unique_entry_id"] = unique_entry_id;
                    ViewState["party_id"] = party_id;
                    bind_gridview();

                }
               
                Dictionary<string, object> dc1 = Sale_Purchase.Firm_details_sale_purchase();
                lbl_hospital_name.Text = lbl_hospital_name1.Text = (String)dc1["firm_name"];
                lbl_address1.Text = lbl_address11.Text = (String)dc1["address"];


                lbl_address2.Text = "";
                img_logo.ImageUrl = img_logo1.ImageUrl = (String)dc1["logo"];

                lbl_email_mobile.Text = lbl_email_mobile1.Text = "Email:" + (String)dc1["email"].ToString() + ", Tel No.:" + (String)dc1["contact_no"].ToString();
                check_print_type();
                if ((String)dc1["Is_slip_package_wise"].ToString() == "0")
                {
                    pnltab_1_item.Visible = true;
                    pnltab_2_item.Visible = true;
                    pnltab_2_package.Visible = false;
                    pnltab_1_package.Visible = false;
                }
                else
                {
                  
                    Bind_package_name(ViewState["Package_id"].ToString());






                }
            }
        }

        private void Bind_package_name(string Package_id)
        {
            DataTable dt = My.dataTable("select * from HMS_PACKAGE_SUMMARY where unique_entry_id='"+ Package_id + "'");
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {

                pnltab_1_item.Visible = true;
                pnltab_2_item.Visible = true;
                pnltab_2_package.Visible = false;
                pnltab_1_package.Visible = false;

                Repeater_package_1.DataSource = null;
                Repeater_package_1.DataBind();
                Repeater_package_2.DataSource = null;
                Repeater_package_2.DataBind();
            }
            else
            {
                pnltab_1_item.Visible = false;
                pnltab_2_item.Visible = false;
                pnltab_2_package.Visible = true;
                pnltab_1_package.Visible = true;

                Repeater_package_1.DataSource = dt;
                Repeater_package_1.DataBind();
                Repeater_package_2.DataSource = dt;
                Repeater_package_2.DataBind();
            }
        }


        private void check_print_type()
        {
            hd_print_type.Value = "1";
            try
            {
                DataTable dt = mycode.FillData("select slip_print_type from Firm_details_sale_purchase");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["slip_print_type"].ToString() != "")
                    {
                        hd_print_type.Value = dt.Rows[0]["slip_print_type"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (hd_print_type.Value == "1")
            {
                rdo_both.Checked = true;
                rdo_office_copy.Checked = false;
                rdo_student_copy.Checked = false;
            }
            else if (hd_print_type.Value == "2")
            {
                rdo_both.Checked = false;
                rdo_office_copy.Checked = true;
                rdo_student_copy.Checked = false;
            }
            else
            {
                rdo_both.Checked = false;
                rdo_office_copy.Checked = false;
                rdo_student_copy.Checked = true;
            }
        }
        private void bind_gridview()
        {
            string query = "select *,(select top 1 mobile from party_details where party_id=t1.Sell_To ) as mobile,(select top 1 address from party_details where party_id=t1.Sell_To ) as address,(select top 1 party_name from party_details where party_id=t1.Sell_To ) as party_name,(select top 1 Item_Name from HMS_Invetory_item_Master where Item_id=t1.Item_Code) as Item_name,(select top 1 Brand_name from HMS_Invetory_Brand_Master where Brand_id=t1.Brand_Id) as Brand_name,(select top 1 Unit from unit_master where unit_id=t1.unit_id and firm=t1.firm) as Unit_name,format(t1.Delivery_date, 'dd/MM/yyyy') as Delivery_datetime from HMS_Invetory_Sell_details_item_wise t1 where   unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "' and Sell_To='" + ViewState["party_id"].ToString() + "' and Is_Stock_Delivered in ('Delivered', 'DeliveredAfter')";

            DataTable dt = My.dataTable(query);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                GrdView_Generate_PO.DataSource = null;
                GrdView_Generate_PO.DataBind();
                GrdView_Generate_PO1.DataSource = null;
                GrdView_Generate_PO1.DataBind();
            }
            else
            {

                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();
                GrdView_Generate_PO1.DataSource = dt;
                GrdView_Generate_PO1.DataBind();
            }
            Bind_data_not_Delivered();

            string query2 = "select *,format(Date, 'dd/MM/yyyy') date1 from HMS_Invetory_Sell_details_billwise  where  unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "' and party_id='" + ViewState["party_id"].ToString() + "'";
            DataTable dt1 = My.dataTable(query2);
            int rowcount1 = dt1.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {
                lbl_date.Text = lbl_date1.Text = "DATE:- " + dt1.Rows[0]["date1"].ToString();

                lbl_Total_rate.Text = lbl_Total_rate1.Text = dt1.Rows[0]["Total_amount"].ToString();
                lbl_discount.Text = lbl_discount1.Text = dt1.Rows[0]["Discount"].ToString();

                lbl_taxbale.Text = lbl_taxbale1.Text = My.toDouble(dt1.Rows[0]["net_taxable"].ToString()).ToString("0.00");
                lbl_po_no.Text = lbl_po_no1.Text = "INVOICE NO.:-" + dt1.Rows[0]["Bill_No"].ToString();

                lbl_user.Text = lbl_user1.Text = "BY.:-" + dt1.Rows[0]["user_id"].ToString();


                lbl_GST.Text = lbl_GST1.Text = dt1.Rows[0]["GST"].ToString();
                lbl_sgst.Text = lbl_sgst1.Text = dt1.Rows[0]["SGST"].ToString();
                lbl_cgst.Text = lbl_cgst1.Text = dt1.Rows[0]["CGST"].ToString();

                lbl_trancharge.Text = lbl_trancharge1.Text = dt1.Rows[0]["transprtation_charge"].ToString();
                lbl_expense.Text = lbl_expense1.Text = dt1.Rows[0]["expense"].ToString();
                lbl_flat_dis.Text = lbl_flat_dis1.Text = dt1.Rows[0]["flat_disc"].ToString();
                lbl_grandtotal.Text = lbl_grandtotal1.Text = dt1.Rows[0]["After_flat_discount"].ToString();
                lbl_round_of.Text = lbl_round_of1.Text = dt1.Rows[0]["round_off"].ToString();
                lbl_remarks.Text = lbl_remarks1.Text = dt1.Rows[0]["Payment_Remarks"].ToString();
                lbl_net_total.Text = lbl_net_total1.Text = dt1.Rows[0]["NetPayable"].ToString();

                Get_student_full_details(dt1.Rows[0]["session"].ToString());
                Bind_paymnet_tracking_id(dt1.Rows[0]["Bill_No"].ToString());
                ViewState["Package_id"] = dt1.Rows[0]["Package_id"].ToString();
               
            }


        }
        private void Bind_paymnet_tracking_id(string Bill_No)
        {
            string query = "select *   from HMS_Inventory_Bill_Payment_Tracking t1 where   Bill_No='" + Bill_No + "' and Payment_Vochar_id='" + Bill_No + "' and party_id='" + ViewState["party_id"].ToString() + "' ";

            DataTable dt = My.dataTable(query);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                lbl_paid_amount.Text = "0.00";
                lbl_duesamount.Text = "0.00";
                lbl_paid_amount1.Text = "0.00";
                lbl_duesamount1.Text = "0.00";
            }
            else
            {
                lbl_paid_amount.Text = lbl_paid_amount1.Text = dt.Rows[0]["Total_Paid_Amount"].ToString();
                lbl_duesamount.Text = lbl_duesamount1.Text = dt.Rows[0]["Duse_Amount"].ToString();

                lbl_payment_mode.Text = lbl_payment_mode1.Text = "MODE OF PAYMENT.:-" + dt.Rows[0]["Bank_Payment_Mode"].ToString();

                //  lbl_payment_remarks.Text = lbl_payment_remarks1.Text = "Cash Received:" + dt.Rows[0]["Received_from_Cash"].ToString() + " " + dt.Rows[0]["Bank_Payment_Mode"].ToString() + " Received: " + dt.Rows[0]["Received_from_Bank"].ToString()+" Trn. No.:-"+ dt.Rows[0]["Payment_transaction"].ToString();

                decimal cash = Convert.ToDecimal(dt.Rows[0]["Received_from_Cash"]);
                string bankMode = dt.Rows[0]["Bank_Payment_Mode"].ToString();
                decimal bankAmount = Convert.ToDecimal(dt.Rows[0]["Received_from_Bank"]);
                string transactionNo = dt.Rows[0]["Payment_transaction"].ToString();

                string remarks = "";


                // CASH

                if (cash > 0)
                {
                    remarks += $"Cash Received: ₹{cash:0.00}";
                }


                // BANK

                if (bankAmount > 0)
                {
                    if (!string.IsNullOrEmpty(remarks))
                        remarks += " | ";

                    remarks += $"{bankMode} Received: ₹{bankAmount:0.00}";
                }


                // TRANSACTION

                if (!string.IsNullOrWhiteSpace(transactionNo)
                    && transactionNo != "0"
                    && transactionNo.ToUpper() != "N/A")
                {
                    remarks += $" | Trn. No.: {transactionNo}";
                }


                lbl_payment_remarks.Text = remarks;
                lbl_payment_remarks1.Text = remarks;
            }
        }
        private void Bind_data_not_Delivered()
        {
            string query = "select *,(select top 1 mobile from party_details where party_id=t1.Sell_To ) as mobile,(select top 1 address from party_details where party_id=t1.Sell_To ) as address,(select top 1 party_name from party_details where party_id=t1.Sell_To ) as party_name,(select top 1 Item_Name from HMS_Invetory_item_Master where Item_id=t1.Item_Code) as Item_name,(select top 1 Brand_name from HMS_Invetory_Brand_Master where Brand_id=t1.Brand_Id) as Brand_name,(select top 1 Unit from unit_master where unit_id=t1.unit_id and firm=t1.firm) as Unit_name from HMS_Invetory_Sell_details_item_wise t1 where   unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "' and Sell_To='" + ViewState["party_id"].ToString() + "' and Is_Stock_Delivered in('NotDelivered')";

            DataTable dt = My.dataTable(query);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                Panel1.Visible = false;
                Panel11.Visible = false;
                Repeater1.DataSource = null;
                Repeater1.DataBind();
                Repeater11.DataSource = null;
                Repeater11.DataBind();
            }
            else
            {
                Panel1.Visible = true;
                Panel11.Visible = true;
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                Repeater11.DataSource = dt;
                Repeater11.DataBind();

            }
        }

        private void Get_student_full_details(string session)
        {
            string query2 = "";
             
            if (ViewState["firm_id_N"].ToString() == "NNI-01")
            {
                if (My.toint(mycode.idate()) >= 20260510)
                {
                    query2 = "select  *,(SELECT TOP 1 Course_Name FROM Add_course_table WHERE course_id=party_details.class_name) AS CLASSNAME from  party_details where    party_id='" + ViewState["party_id"].ToString() + "' ";
                }
                else
                {
                    query2 = "select top 1 *,(select top 1 mobile from party_details where party_id=admission_registor.admissionserialnumber ) as mobile,(select top 1 address from party_details where party_id=admission_registor.admissionserialnumber  ) as address,(select top 1 party_name from party_details where party_id=admission_registor.admissionserialnumber  ) as party_name from admission_registor  where    admissionserialnumber='" + ViewState["party_id"].ToString() + "' and session='" + session + "' order by id desc ";

                }
                   
            }
           else
            {
                query2 = "select top 1 *,(select top 1 mobile from party_details where party_id=admission_registor.admissionserialnumber ) as mobile,(select top 1 address from party_details where party_id=admission_registor.admissionserialnumber  ) as address,(select top 1 party_name from party_details where party_id=admission_registor.admissionserialnumber  ) as party_name from admission_registor  where    admissionserialnumber='" + ViewState["party_id"].ToString() + "' and session='" + session + "' order by id desc ";
            }
            DataTable dt1 = My.dataTable(query2);
            int rowcount1 = dt1.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {
                if (ViewState["firm_id_N"].ToString() == "NNI-01")
                {
                    if (My.toint(mycode.idate()) >= 20260510)
                    {
                        lbl_admission_no.Text = lbl_admission_no1.Text = "CUSTOMER ID. :- " + ViewState["party_id"].ToString().ToUpper();
                        lbl_class_name.Text = lbl_class_name1.Text = "CLASS :- " + dt1.Rows[0]["CLASSNAME"].ToString();
                        lbl_section.Text = lbl_section1.Text = "SECTION :- " + dt1.Rows[0]["Section"].ToString();

                        lbl_partyname.Text = lbl_partyname1.Text = "NAME :-" + dt1.Rows[0]["party_name"].ToString();
                        lbl_mobile_no.Text = lbl_mobile_no1.Text = "MOBILE NO.:- " + dt1.Rows[0]["mobile"].ToString();
                        lbl_address.Text = lbl_address22.Text = "ADD.:- " + dt1.Rows[0]["address"].ToString();
                    }
                    else
                    {
                        lbl_admission_no.Text = lbl_admission_no1.Text = "ADMISSION NO. :- " + ViewState["party_id"].ToString().ToUpper();
                        lbl_class_name.Text = lbl_class_name1.Text = "CLASS :- " + dt1.Rows[0]["class"].ToString();
                        lbl_section.Text = lbl_section1.Text = "SECTION :- " + dt1.Rows[0]["Section"].ToString();
                        lbl_roll_no.Text = lbl_roll_no1.Text = "ROLL NO. :- " + dt1.Rows[0]["rollnumber"].ToString();
                        lbl_partyname.Text = lbl_partyname1.Text = "NAME :-" + dt1.Rows[0]["party_name"].ToString();
                        lbl_mobile_no.Text = lbl_mobile_no1.Text = "MOBILE NO.:- " + dt1.Rows[0]["mobile"].ToString();
                        lbl_address.Text = lbl_address22.Text  = "ADD.:- " + dt1.Rows[0]["address"].ToString();

                    }
                
                }
                else
                {
                    lbl_admission_no.Text = lbl_admission_no1.Text = "ADMISSION NO. :- " + ViewState["party_id"].ToString().ToUpper();
                    lbl_class_name.Text = lbl_class_name1.Text = "CLASS :- " + dt1.Rows[0]["class"].ToString();
                    lbl_section.Text = lbl_section1.Text = "SECTION :- " + dt1.Rows[0]["Section"].ToString();
                    lbl_roll_no.Text = lbl_roll_no1.Text = "ROLL NO. :- " + dt1.Rows[0]["rollnumber"].ToString();
                    lbl_partyname.Text = lbl_partyname1.Text = "NAME :-" + dt1.Rows[0]["party_name"].ToString();
                    lbl_mobile_no.Text = lbl_mobile_no1.Text = "MOBILE NO.:- " + dt1.Rows[0]["mobile"].ToString();
                    lbl_address.Text = lbl_address22.Text = "ADD.:- " + dt1.Rows[0]["address"].ToString();

                  
                }
            }
        }

        protected void btn_back_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (ViewState["firm_id_N"].ToString() == "NNI-01")
                {
                    if (Session["billfrom"].ToString() == "1")
                    {
                        Response.Redirect("../SalesEntry.aspx", false);

                    }

                    else if (Session["billfrom"].ToString() == "2")
                    {
                        Response.Redirect("../Sale_history.aspx", false);

                    }
                    else if (Session["billfrom"].ToString() == "3")
                    {
                        Response.Redirect("../Student_Wise_Sales_Report.aspx", false);

                    }
                    else if (Session["billfrom"].ToString() == "5")
                    {
                        Response.Redirect("../Undelivered_Item.aspx", false);

                    }
                    else if (Session["billfrom"].ToString() == "6")
                    {
                        Response.Redirect("../Online_Order_Delivered.aspx", false);

                    }
                    else
                    {
                        Response.Redirect("../Sale_history.aspx", false);

                    }
                }
                else
                {
                    if (Session["billfrom"].ToString() == "1")
                    {
                        Response.Redirect("../Sale_Entry.aspx", false);

                    }

                    else if (Session["billfrom"].ToString() == "2")
                    {
                        Response.Redirect("../Sale_history.aspx", false);

                    }
                    else if (Session["billfrom"].ToString() == "3")
                    {
                        Response.Redirect("../Student_Wise_Sales_Report.aspx", false);

                    }
                    else if (Session["billfrom"].ToString() == "5")
                    {
                        Response.Redirect("../Undelivered_Item.aspx", false);

                    }
                    else if (Session["billfrom"].ToString() == "6")
                    {
                        Response.Redirect("../Online_Order_Delivered.aspx", false);

                    }
                    else
                    {
                        Response.Redirect("../Sale_history.aspx", false);

                    }
                }
            }
            catch
            {
                if (ViewState["firm_id_N"].ToString() == "NNI-01")
                {
                    Response.Redirect("../SalesEntry.aspx", false);
                }
                else
                {
                    Response.Redirect("../Sale_Entry.aspx", false);
                }
                   
            }

        }


    }
}