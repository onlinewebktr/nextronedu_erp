using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Monthly_Payments
{
    public partial class monthly_slip_aspx : System.Web.UI.Page
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
                        ViewState["sessionid"] = Request.QueryString["sessionid"];
                        ViewState["classid"] = Request.QueryString["classid"];
                        ViewState["Slip_no"] = Request.QueryString["Slip_no"];
                        ViewState["session"] = mycode.get_session(ViewState["sessionid"].ToString());
                        Bind_schoolinfo();
                        student_details();
                        Bind_adjustamount();
                        bind_amount();
                        Bind_particular();


                        hid_cell();
                    }
                }
            }
        }

        private void hid_cell()
        {
            DataTable dt = mycode.FillData(" Select top 1 cd.Category_Name,cd.Category_Id,cd.hidie_col from Category_Details cd join admission_registor ar on ar.Category_id=cd.Category_Id where cd.hidie_col='1' and ar.admissionserialnumber='" + ViewState["admissionno"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                this.grd_fee.Columns[3].Visible = true;
                this.grd_fee.Columns[4].Visible = true;

                this.grd_fee1.Columns[3].Visible = true;
                this.grd_fee1.Columns[4].Visible = true;
            }
            else
            {

                this.grd_fee.Columns[3].Visible = false;
                this.grd_fee.Columns[4].Visible = false;

                this.grd_fee1.Columns[3].Visible = false;
                this.grd_fee1.Columns[4].Visible = false;
            }

        }

        private void bind_amount()
        {
            DataTable dt = mycode.FillData("select  top 1 * from Student_Payment_History where Addmission_no='" + ViewState["admissionno"].ToString() + "' and Session='" + ViewState["session"].ToString() + "' and Slip_no='" + ViewState["Slip_no"].ToString() + "' order by id asc");
            if (dt.Rows.Count == 0)
            {
                ViewState["Amount"] = "0";
            }
            else
            {
                lbl_slipno.Text = lbl_slipno1.Text = dt.Rows[0]["Slip_no"].ToString();
                lbl_paymentdate.Text = lbl_paymentdate1.Text = dt.Rows[0]["Date"].ToString();
                lbl_paid_amount.Text = lbl_paid_amount1.Text = dt.Rows[0]["Amount"].ToString();

                int number = (int)Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                string inword_number = mycode.NumberToWords(number);
                string inword = inword_number + " Only";
                lbl_amountinword.Text = lbl_amountinword1.Text = inword;
                ViewState["Amount"] = dt.Rows[0]["Amount"].ToString();

                lbl_paymentmode1.Text = lbl_paymentmode.Text = dt.Rows[0]["mode"].ToString();
                if (dt.Rows[0]["Pay_mode_transaction_no"].ToString() != "")
                {
                    lbl_trans_no1.Text = lbl_trans_no.Text = dt.Rows[0]["Pay_mode_transaction_no"].ToString();
                    lbl_trans_no1Dv.Visible = true;
                    lbl_trans_noDv.Visible = true;
                }
                else
                {
                    lbl_trans_no1.Text = lbl_trans_no.Text = dt.Rows[0]["Pay_mode_transaction_no"].ToString();
                    lbl_trans_no1Dv.Visible = false;
                    lbl_trans_noDv.Visible = false;
                }

                if (dt.Rows[0]["Remarks"].ToString() != "")
                {
                    lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remarks"].ToString();
                    remarksDv.Visible = true;
                    remarksDv1.Visible = true;
                }
                else
                {
                    lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remarks"].ToString();
                    remarksDv.Visible = false;
                    remarksDv1.Visible = false;
                }
            }
        }



        private void Bind_adjustamount()
        {
            string query = "Select sum(cast(Amount as float)) as total from Student_Payment_History where Slip_no='" + ViewState["Slip_no"].ToString() + "' and Adjust_type='Adjust'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_Adjustamount.Text = "0.00";
            }
            else
            {
                lbl_Adjustamount.Text = lbl_adjustamount1.Text = My.toDouble(dt.Rows[0][0].ToString()).ToString("0.00");
            }
        }

        private void Bind_particular()
        {
            string quer = "select Month,Content as Particular, cast(payable AS float) fee_amount,cast(previously_paid AS float) previously_paid ,cast(disc_amt AS float) disc_amt,(cast(payable AS float)-cast(previously_paid AS float)-cast(disc_amt AS float)) payable   from Monthly_Fee_Collection_Slip where session='" + ViewState["session"].ToString() + "' and  slipno='" + ViewState["Slip_no"].ToString() + "' and adno='" + ViewState["admissionno"].ToString() + "'";
            DataTable dt = mycode.FillData(quer);
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
            }

        }
        double total_payable1 = 0, total_disc_amount1 = 0, total_perviously1 = 0, total_dues1 = 0;
        protected void grd_fee1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_payable");
                Label lbl_disc_amount = (Label)e.Row.FindControl("lbl_disc_amount");
                Label lbl_perviously_paid = (Label)e.Row.FindControl("lbl_perviously_paid");
                Label lbl_dues = (Label)e.Row.FindControl("lbl_dues");
                if (lbl_payable.Text != "")
                {
                    total_payable1 = total_payable1 + Convert.ToDouble(lbl_payable.Text);
                }

                if (lbl_disc_amount.Text != "")
                {
                    total_disc_amount1 = total_disc_amount1 + Convert.ToDouble(lbl_disc_amount.Text);
                }

                if (lbl_perviously_paid.Text != "")
                {
                    total_perviously1 = total_perviously1 + Convert.ToDouble(lbl_perviously_paid.Text);
                }

                if (lbl_dues.Text != "")
                {
                    total_dues1 = total_dues1 + Convert.ToDouble(lbl_dues.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalfeeamount = (Label)e.Row.FindControl("lbl_totalfeeamount");
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totaldiscount");
                Label lbl_totalpreviously = (Label)e.Row.FindControl("lbl_totalpreviously");

                Label lbl_totalpaybale = (Label)e.Row.FindControl("lbl_totalpaybale");


                lbl_totalfeeamount.Text = total_payable1.ToString("0.00");
                lbl_totaldiscount.Text = total_disc_amount1.ToString("0.00");
                lbl_totalpreviously.Text = total_perviously1.ToString("0.00");
                lbl_totalpaybale.Text = total_dues1.ToString("0.00");


                double abc = My.toDouble(lbl_totalpaybale.Text) - (My.toDouble(ViewState["Amount"].ToString()) + My.toDouble(lbl_Adjustamount.Text));
                lbl_dues.Text = lbl_dues1.Text = abc.ToString("0.00");

            }
        }


        double total_payable = 0, total_disc_amount = 0, total_perviously = 0, total_dues = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_payable");
                Label lbl_disc_amount = (Label)e.Row.FindControl("lbl_disc_amount");
                Label lbl_perviously_paid = (Label)e.Row.FindControl("lbl_perviously_paid");
                Label lbl_dues = (Label)e.Row.FindControl("lbl_dues");
                if (lbl_payable.Text != "")
                {
                    total_payable = total_payable + Convert.ToDouble(lbl_payable.Text);
                }

                if (lbl_disc_amount.Text != "")
                {
                    total_disc_amount = total_disc_amount + Convert.ToDouble(lbl_disc_amount.Text);
                }

                if (lbl_perviously_paid.Text != "")
                {
                    total_perviously = total_perviously + Convert.ToDouble(lbl_perviously_paid.Text);
                }

                if (lbl_dues.Text != "")
                {
                    total_dues = total_dues + Convert.ToDouble(lbl_dues.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalfeeamount = (Label)e.Row.FindControl("lbl_totalfeeamount");
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totaldiscount");
                Label lbl_totalpreviously = (Label)e.Row.FindControl("lbl_totalpreviously");
                Label lbl_totalpaybale = (Label)e.Row.FindControl("lbl_totalpaybale");


                lbl_totalfeeamount.Text = total_payable.ToString("0.00");
                lbl_totaldiscount.Text = total_disc_amount.ToString("0.00");
                lbl_totalpreviously.Text = total_perviously.ToString("0.00");
                lbl_totalpaybale.Text = total_dues.ToString("0.00");
            }
        }

        private void student_details()
        {
            DataTable dt = mycode.FillData("select studentname,admissionserialnumber,class,Section,fathername,session,rollnumber,Academic_Sem_or_Year from admission_registor where admissionserialnumber='" + ViewState["admissionno"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and Class_id=" + ViewState["classid"].ToString() + " ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_studentname.Text = lbl_studentname1.Text = dt.Rows[0]["studentname"].ToString();
                lbl_aadmissionno.Text = lbl_aadmissionno1.Text = dt.Rows[0]["admissionserialnumber"].ToString();

                lbl_class.Text = lbl_class1.Text = dt.Rows[0]["class"].ToString() + " Section-" + dt.Rows[0]["Section"].ToString();
                lbl_fathername.Text = lbl_fathername1.Text = dt.Rows[0]["fathername"].ToString();
                lbl_session.Text = lbl_session1.Text = dt.Rows[0]["session"].ToString();
                lbl_rollno.Text = lbl_rollno1.Text = dt.Rows[0]["rollnumber"].ToString();

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
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {

            Response.Redirect("Monthly_payment_student.aspx", false);

        }


        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }
    }
}