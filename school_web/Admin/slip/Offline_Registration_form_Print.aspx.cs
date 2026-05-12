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
    public partial class Offline_Registration_form_Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["form_no"] != null)
                {
                    ViewState["form_no"] = Request.QueryString["form_no"];
                    bind_data_student_details();
                    Bind_schoolinfo();
                }
                else
                {
                    Response.Redirect("../form-sale.aspx", false);
                }
            }
        }

        private void bind_data_student_details()
        {
            DataTable dt = mycode.FillData("select * from Form_sale_details where  Form_no='"+ ViewState["form_no"].ToString() + "' ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_session.Text= dt.Rows[0]["session"].ToString();
                lbl_slipno.Text = dt.Rows[0]["Form_no"].ToString();
                lbl_date.Text= dt.Rows[0]["date"].ToString();
                txt_indexno.Text= dt.Rows[0]["form_indesx_no"].ToString();
            }
        }

        My mycode = new My();
        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_affiliation_no.Text = dt.Rows[0]["Affiliation"].ToString();
                lbl_schoolno.Text = dt.Rows[0]["school_no"].ToString();
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
                if (dt.Rows[0]["Is_mobile_no_show_in_bill"].ToString() == "True")
                {
                    contact_no.Visible = true;

                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../form-sale.aspx", false);
        }



        protected void btn_print_Click1(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }



    }
}