using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class payment_slip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                fetch_generral_setting();
                
                string unique_entry_id = Request.QueryString["unique_entry_id"];
                string vouchertype = Request.QueryString["vouchertype"];
                string firm = Request.QueryString["firm"];
                string session = Request.QueryString["session"];
                string voucher_no = Request.QueryString["voucher_no"];

                if (!string.IsNullOrEmpty(unique_entry_id))
                {
                    ViewState["unique_entry_id"] = unique_entry_id;
                    ViewState["firm"] = firm;
                    ViewState["session"] = session;
                    ViewState["vouchertype"] = vouchertype;
                    lbl_vouchertype.Text = vouchertype;
                    lbl_receipt_no.Text = voucher_no;
                    bind_gridview();
                }
                
            }

        }
        My mycode = new My();
        private void fetch_generral_setting()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                try
                {
                    if (dt.Rows[0]["Is_slip_header"].ToString() == "True")
                    {
                        img_header.Visible = img_header.Visible = false;

                        headertext.Visible = headertext.Visible = true;
                        img_header.ImageUrl = img_header.ImageUrl = dt.Rows[0]["Header_images"].ToString();
                    }
                    else
                    {
                        headertext.Visible = headertext.Visible = true;
                        img_header.Visible = img_header.Visible = false;
                    }
                }
                catch
                {
                    headertext.Visible = headertext.Visible = true;
                    img_header.Visible = img_header.Visible = false;
                }
               // lbl_affiliation_no.Text = lbl_affiliation_no1.Text = dt.Rows[0]["Affiliation"].ToString();
               // lbl_schoolno.Text = lbl_schoolno1.Text = dt.Rows[0]["school_no"].ToString();
                img_logo.ImageUrl = img_logo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address1.Text = lbl_address1.Text = dt.Rows[0]["address1"].ToString();
                lbl_email.Text = lbl_email.Text = dt.Rows[0]["email"].ToString();
                //lbl_website.Text = lbl_website1.Text = dt.Rows[0]["website"].ToString();
                lbl_mobile.Text = lbl_mobile.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_hospital_name.Text = lbl_hospital_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_companyname.Text = "for " + lbl_hospital_name.Text + " School";
            }

            
        }

        private void bind_gridview()
        {
            SqlCommand cmd = new SqlCommand("sp_payment_Voucher_Details");
            cmd.Parameters.AddWithValue("@sp_status", 4);
            cmd.Parameters.AddWithValue("@firm", ViewState["firm"].ToString());
            cmd.Parameters.AddWithValue("@vouchertype", ViewState["vouchertype"].ToString());
            cmd.Parameters.AddWithValue("@unique_entry_id", ViewState["unique_entry_id"].ToString());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable tdt = ds.Tables[1];
                grd_view.DataSource = dt;
                grd_view.DataBind();

                lbl_date.Text = dt.Rows[0]["Date"].ToString();
                lbl_remarks.Text = dt.Rows[0]["Description"].ToString();
                lbl_total_amount.Text = tdt.Rows[0]["Credit"].ToString();
                lbl_through.Text = tdt.Rows[0]["Account_Name"].ToString();
                double rs = My.toDouble(lbl_total_amount.Text);
                lbl_inwords.Text = "₹ " + find_amount_words(lbl_total_amount.Text) + "/-";
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
    }
}