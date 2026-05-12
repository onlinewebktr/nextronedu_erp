using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management.Slip
{
    public partial class Print_item_stransfer_slip : System.Web.UI.Page
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
                Response.Write("<script language=javascript>window.open('../../Default.aspx','_parent',replace=true);</script>");
            }
            else
            {
                try
                {
                    //if (My.Is_show_print_datetime == "Yes")
                    //{ pnl_datetime.Visible = true; pnl_datetime0.Visible = true; }
                    //else { pnl_datetime.Visible = false; pnl_datetime0.Visible = true; }


                    
                    string unique_entry_id = Request.QueryString["unique_entry_id"];
                    string Demand_id = Request.QueryString["Demand_id"];

                    if (!string.IsNullOrEmpty(unique_entry_id))
                    {
                        ViewState["unique_entry_id"] = unique_entry_id;
                        ViewState["Demand_id"] = Demand_id;
                        lbl_po_no.Text = Demand_id;

                        bind_gridview();

                    }
                    //lbl_email_mobile.Text = "Email:" + My.Hopital_email + " Tel No.:" + My.Hopital_mobile;
                    
                    Dictionary<string, object> dc1 = Sale_Purchase.Firm_details_sale_purchase();
                    lbl_hospital_name.Text = (String)dc1["firm_name"];
                    lbl_address1.Text = (String)dc1["address"];
                    lbl_address2.Text = "";

                    find_print_details();
                }
                catch (Exception ex)
                {
                    My.submitexception(ex.ToString());
                }
            }
        }
        private void find_print_details()
        {
            //SqlCommand cmd = new SqlCommand("sp_HMS_user_details");
            //cmd.Parameters.AddWithValue("@sp_status", "Fetch1");
            //cmd.Parameters.AddWithValue("@login_Id", Session["Admin"].ToString());
            //DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            //DataTable dt = ds.Tables[0];
            //int rowcount = ds.Tables[0].Rows.Count;
            //if (dt.Rows.Count > 0)
            //{
            //    lbl_printed_by.Text = dt.Rows[0]["name"].ToString();
            //    lbl_Printed_date.Text = My.printdatetime();

            //    lbl_printed_by0.Text = dt.Rows[0]["name"].ToString();
            //    lbl_Printed_date0.Text = My.printdatetime();
            //}
        }
     
        private void bind_gridview()
        {

            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch4");
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            ViewState["item_dt"] = dt;
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                lbl_date.Text = ds.Tables[0].Rows[0]["date"].ToString();
                lbl_Total_rate.Text = dt.Compute("Sum(Total_amount)", "").ToString();
                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();
                lbl_transfer_to.Text = ds.Tables[0].Rows[0]["Store_name"].ToString();
                lbl_user_name.Text = ds.Tables[0].Rows[0]["user_name"].ToString();
                lbl_remarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                lbl_receivername.Text = ds.Tables[0].Rows[0]["Receivername"].ToString();
            }
            else
            {
                GrdView_Generate_PO.DataSource = null;
                GrdView_Generate_PO.DataBind();
            }

        }



        My my = new My();
        private string find_amount_words(string p)
        {
            if (p == "")
            {
                p = "0";
                string amount_in_words = "Zero";
                return amount_in_words;

            }
            else
            {
                Double number = Double.Parse(p);
                number = Math.Round(number, 0);

                string amount_in_words = my.AmountInWords(number.ToString());
                return amount_in_words;
            }
        }


        protected string Getamount_comma_seperated(string amount)
        {
            try
            {
                string amt = String.Format("{0:n}", Convert.ToDouble(amount));
                return amt;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
    }
}