using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class admission_form : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_schoolinfo();
            }
        }

        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (dt.Rows[0]["Is_slip_header"].ToString() == "True")
                    {
                        textheader.Visible = false;
                        printheader.Visible = true;
                        img_header.Visible = true;
                        img_header.ImageUrl = dt.Rows[0]["Header_images"].ToString();
                    }
                    else
                    {
                        printheader.Visible = false;
                        textheader.Visible = true;
                        printheader.Visible = printheader.Visible = false;
                    }
                }
                catch
                {
                    textheader.Visible = true;
                    printheader.Visible = false;
                }

                Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                img_watermark.ImageUrl = dt.Rows[0]["Watermark_image"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                // lbl_email.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString() + ", 9296978885" + " | Email Id : " + dt.Rows[0]["email"].ToString();
                lbl_reg_udise_id.Text = "Regd No. : " + dt.Rows[0]["school_no"].ToString() + " | UDISE No. : " + dt.Rows[0]["Udise_no"].ToString();



                lbl_estd.Text = "ESTD : " + dt.Rows[0]["estd"].ToString();
                lbl_aff_text.Text = dt.Rows[0]["Affiliated_by_full_text"].ToString();
                lbl_aff_no.Text = "UDISE No.  : 10100101407";
                lbl_website.Text = dt.Rows[0]["website"].ToString();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../form-sale.aspx", false);
        }
    }
}