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
    public partial class Form_Sale_Slip : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    if (Request.QueryString["transaction"] != null)
                    {
                        ViewState["transaction"] = Request.QueryString["transaction"];


                        Bind_schoolinfo();


                        Bind_particular();

                    }
                }
            }
        }

        private void Bind_particular()
        {
            string query = "Select asr.*   from Form_sale_details asr    where asr.recpt_no='" + ViewState["transaction"].ToString() + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();

                grd_fee1.DataSource = null;
                grd_fee1.DataBind();

            }
            else
            {
                grd_fee.DataSource = dt;
                grd_fee.DataBind();

                grd_fee1.DataSource = dt;
                grd_fee1.DataBind();

                int number = (int)Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                string inword_number = mycode.NumberToWords(number);
                string inword = inword_number + " Only";
                lbl_amountinword.Text = lbl_amountinword1.Text = inword;
                lbl_slipno.Text = lbl_slipno1.Text = dt.Rows[0]["form_indesx_no"].ToString();
                lbl_paymentdate.Text = lbl_paymentdate1.Text = dt.Rows[0]["date"].ToString();
                lbl_studentname.Text = lbl_studentname1.Text = dt.Rows[0]["student_name"].ToString();
                lbl_fathername.Text = lbl_fathername1.Text = dt.Rows[0]["fathers_name"].ToString();
                lbl_unique_no.Text = lbl_unique_no_1.Text = dt.Rows[0]["recpt_no"].ToString();
               // lbl_adres1.Text = lbl_adress.Text = dt.Rows[0]["Address"].ToString();
                //lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remarks"].ToString();
                lbl_mobileno1.Text = lbl_mobileno.Text = dt.Rows[0]["mobile"].ToString();
                lbl_guardian_name.Text = lbl_guardian_name1.Text = dt.Rows[0]["Guardian_first_name"].ToString() + " " + dt.Rows[0]["Guardian_middle_name"].ToString() + " " + dt.Rows[0]["Guardian_last_name"].ToString();
                lbl_adres1.Text = lbl_adress.Text = dt.Rows[0]["Address"].ToString() + ", P.O. : " + dt.Rows[0]["Post_office"].ToString() + ", P.S. : " + dt.Rows[0]["Police_station"].ToString() + ", District : " + dt.Rows[0]["District"].ToString() + ", State : " + dt.Rows[0]["State"].ToString() + ", Pin Code : " + dt.Rows[0]["Pin_code"].ToString();
            }
        }

        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_affiliation_no.Text = lbl_affiliation_no1.Text = dt.Rows[0]["Affiliation"].ToString();
                lbl_schoolno.Text = lbl_schoolno1.Text = dt.Rows[0]["school_no"].ToString();
                imglogo.ImageUrl = Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = lbl_address1.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = lbl_emaiid1.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = lbl_website1.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = lbl_contact_details1.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = lbl_heading1.Text = dt.Rows[0]["firm_name"].ToString();
                Image2.ImageUrl = dt.Rows[0]["logo"].ToString();
                Image3.ImageUrl = dt.Rows[0]["logo"].ToString();
                try
                {
                    Image2.Visible = true;
                    if (dt.Rows[0]["Is_certificate_watermark_hide"].ToString() == "True")
                    {
                        Image2.Visible = false;
                        Image3.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        double total_payable = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblamount = (Label)e.Row.FindControl("lblamount");


                if (lblamount.Text != "")
                {
                    total_payable = total_payable + Convert.ToDouble(lblamount.Text);
                }



            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalpay = (Label)e.Row.FindControl("lbl_totalpay");
                lbl_totalpay.Text = total_payable.ToString("0.00");

                lbl_paid_amount.Text = total_payable.ToString("0.00");

            }
        }
        double total_payable1 = 0;
        protected void grd_fee1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblamount = (Label)e.Row.FindControl("lblamount");
                if (lblamount.Text != "")
                {
                    total_payable1 = total_payable1 + Convert.ToDouble(lblamount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalpay = (Label)e.Row.FindControl("lbl_totalpay");
                lbl_totalpay.Text = total_payable1.ToString("0.00");
                lbl_paid_amount1.Text = total_payable1.ToString("0.00");
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            if (Session["pagew"] == "2")
            {
                Response.Redirect("../form-sale.aspx", false);
            }
            else
            {
                Response.Redirect("../Money_receipt_from_student_Report.aspx", false);
            }
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }
    }
}