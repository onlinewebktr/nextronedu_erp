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
    public partial class Print_generated_PO : System.Web.UI.Page
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
                    //|| My.Financial_Year == null || My.firm_id == null


                    string PO_no = Request.QueryString["PO_no"];
                    string entryid = Request.QueryString["entryid"];
                    string session = Request.QueryString["Session"];
                    string firmid = Request.QueryString["firmid"];

                    if (!string.IsNullOrEmpty(PO_no))
                    {
                        ViewState["PO_no"] = PO_no;
                        ViewState["entryid"] = entryid;
                        ViewState["Session"] = session;
                        ViewState["firmid"] = firmid;
                        lbl_po_no.Text = "P.O. No:-" + PO_no;

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

            SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Purchase_order");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch2");
            cmd.Parameters.AddWithValue("@Status", "Submitted");
            cmd.Parameters.AddWithValue("@PO_no", ViewState["PO_no"].ToString());
            cmd.Parameters.AddWithValue("@entry_id_po", ViewState["entryid"].ToString());
            cmd.Parameters.AddWithValue("@Session", ViewState["Session"].ToString());
            cmd.Parameters.AddWithValue("@firm", ViewState["firmid"].ToString());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);

            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();
                lbl_date.Text = "Date:-" + My.display_date(ds.Tables[0].Rows[0]["Created_Date"]);
                lbl_Total_rate.Text = dt.Compute("Sum(Total_rate)", "").ToString();
                lbl_partyname.Text = ds.Tables[0].Rows[0]["party_name"].ToString();
                lbl_mobile_no.Text = "Mobile No.:-" + ds.Tables[0].Rows[0]["mobile"].ToString();
                lbl_address.Text = "Add.:-" + ds.Tables[0].Rows[0]["address"].ToString();
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


        protected void btn_back_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["flag"].ToString() == "1")
            {
                string path = "../General_billing.aspx";
                Response.Redirect(path, false);
            }
            if (ViewState["flag"].ToString() == "2")
            {
                string path = "../General_othersvr_patient_revenue_report.aspx";
                Response.Redirect(path, false);
            }
            else
            {
                string path = "../General_Patient_List.aspx";
                Response.Redirect(path, false);
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