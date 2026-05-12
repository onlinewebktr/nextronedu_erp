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

    public partial class Print_delete_bill : System.Web.UI.Page
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
                    if (Request.QueryString["admissionno"] != null)
                    {
                        ViewState["admissionno"] = Request.QueryString["admissionno"];
                        ViewState["Slip_no"] = Request.QueryString["Slip_no"];
                        ViewState["sessionid"] = Request.QueryString["sessionid"];
                        student_details();
                        bind_payment_head();

                        firm_details();

                        try
                        {
                            int number = (int)Convert.ToDouble(lbl_paid_amount.Text);
                            string inword_number = mycode.NumberToWords(number);
                            string inword = inword_number + " Only";
                            lbl_amountinword.Text = inword;

                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void bind_payment_head()
        {
            string quer = " select  *,format(insert_time_date, 'dd/MM/yyyy') as date1   from Student_Payment_History_Save_bakup    where   Addmission_no='" + ViewState["admissionno"].ToString() + "'   and  Slip_no='" + ViewState["Slip_no"].ToString() + "'  ";
            DataTable dt = mycode.FillData(quer);
            if (dt.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
                p1.Visible = false;
                p2.Visible = false;
            }
            else
            {
                p1.Visible = true;
                p2.Visible = true;
                grd_fee.DataSource = dt;
                grd_fee.DataBind();
                lbl_slipno.Text = dt.Rows[0]["New_Slip_no"].ToString();
                lbl_paymentdate.Text = dt.Rows[0]["date1"].ToString();

            }
        }

        private void student_details()
        {
            DataTable dt = mycode.FillData("select studentname,admissionserialnumber,class,Section,fathername,session,rollnumber,Academic_Sem_or_Year from admission_registor where admissionserialnumber='" + ViewState["admissionno"].ToString() + "'  and Session_id='" + ViewState["sessionid"].ToString() + "'     ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_studentname.Text = dt.Rows[0]["studentname"].ToString();
                lbl_fathername.Text = dt.Rows[0]["fathername"].ToString();
                lbl_session.Text = dt.Rows[0]["session"].ToString();
                lbl_admission_no.Text= dt.Rows[0]["admissionserialnumber"].ToString();

                lbl_class.Text = dt.Rows[0]["class"].ToString();
                lbl_section.Text = dt.Rows[0]["Section"].ToString();
            }
        }

        private void firm_details()
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
            }
        }

        double total_payable = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payment = (Label)e.Row.FindControl("lbl_payment");

                if (lbl_payment.Text != "")
                {
                    total_payable = total_payable + Convert.ToDouble(lbl_payment.Text);
                }



            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalpaybale = (Label)e.Row.FindControl("lbl_totalpaybale");
                lbl_totalpaybale.Text = total_payable.ToString("0.00");
                lbl_paid_amount.Text = total_payable.ToString("0.00");
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Redirect("../Report_Delete_Bill_History.aspx", false);

            }
            catch
            {
            }
        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }
    }
}