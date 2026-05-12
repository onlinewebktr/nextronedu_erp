using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Day_End_Report_Summary : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["Admin"] == null)
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                    else
                    {

                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        ViewState["flag"] = "0";
                        txt_date1.Text = mycode.date();
                        find_data();
                        find_data_admission_fee_and_readmission_fee();
                        fee_head_wise_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Today_Fee_Collection_Summary");
            }
        }
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (txt_date1.Text == "")
            {
                Alertme("Please select date", "warning");
            }
            else
            {
                find_data();
                find_data_admission_fee_and_readmission_fee();
                fee_head_wise_data();
            }
        }

        private void fee_head_wise_data()
        {
            hd_date.Value = mycode.ConvertStringToiDate(txt_date1.Text).ToString();
            SqlCommand cmd3 = new SqlCommand();
            cmd3.Parameters.AddWithValue("@typestatus", "1");
            cmd3.Parameters.AddWithValue("@feetpe", "11");//totalreadmission fee colection

            cmd3.Parameters.AddWithValue("@Idate", mycode.ConvertStringToiDate(txt_date1.Text));
            cmd3.CommandText = "sp_Fee_Collection_Report";
            DataTable dt3 = UsesCode.Getdata_sp(cmd3);
            if (dt3.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                lbl_print_headS.Text = "Day End Report Summery of Date : " + txt_date1.Text;
                lbl_print_dateS.Text = "(Print on : " + mycode.date() + ")";
                grd_fee.DataSource = dt3;
                grd_fee.DataBind();
            }
        }
        double totalamount = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_payable.Text != "")
                {
                    totalamount = totalamount + Convert.ToDouble(lbl_payable.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalamount = (Label)e.Row.FindControl("lbl_totalamount");

                lbl_totalamount.Text = totalamount.ToString("0.00");

            }
        }
        private void find_data_admission_fee_and_readmission_fee()
        {

            //--------------------Total Admission Fee--------------------
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Parameters.AddWithValue("@typestatus", "1");
            cmd2.Parameters.AddWithValue("@feetpe", "9");//totaladmission fee colection
            cmd2.Parameters.AddWithValue("@Idate", mycode.ConvertStringToiDate(txt_date1.Text));
            cmd2.CommandText = "sp_Fee_Collection_Report";
            DataTable dt2 = UsesCode.Getdata_sp(cmd2);
            if (dt2.Rows.Count == 0)
            {
                lbl_totaladmissionfee1.Text = "0.00";
            }
            else
            {
                lbl_totaladmissionfee1.Text = My.toDouble(dt2.Rows[0][0].ToString()).ToString("0.00");
            }
            //--------------------------Total readmission fee-------------------


            SqlCommand cmd3 = new SqlCommand();
            cmd3.Parameters.AddWithValue("@typestatus", "1");
            cmd3.Parameters.AddWithValue("@feetpe", "10");//totalreadmission fee colection

            cmd3.Parameters.AddWithValue("@Idate", mycode.ConvertStringToiDate(txt_date1.Text));
            cmd3.CommandText = "sp_Fee_Collection_Report";
            DataTable dt3 = UsesCode.Getdata_sp(cmd3);
            if (dt3.Rows.Count == 0)
            {
                lbl_readmissionfeecolection1.Text = "0.00";
            }
            else
            {
                lbl_readmissionfeecolection1.Text = My.toDouble(dt3.Rows[0][0].ToString()).ToString("0.00");
            }

            SqlCommand cmd4 = new SqlCommand();
            cmd4.Parameters.AddWithValue("@typestatus", "1");
            cmd4.Parameters.AddWithValue("@feetpe", "13");//monthely fee

            cmd4.Parameters.AddWithValue("@Idate", mycode.ConvertStringToiDate(txt_date1.Text));
            cmd4.CommandText = "sp_Fee_Collection_Report";
            DataTable dt4 = UsesCode.Getdata_sp(cmd4);
            if (dt4.Rows.Count == 0)
            {
                lbl_monthley.Text = "0.00";
            }
            else
            {
                lbl_monthley.Text = My.toDouble(dt4.Rows[0][0].ToString()).ToString("0.00");
            }
        }

        private void find_data()
        {
            SqlCommand cmd3 = new SqlCommand();
            cmd3.Parameters.AddWithValue("@typestatus", "1");
            cmd3.Parameters.AddWithValue("@feetpe", "12");//totalreadmission fee colection

            cmd3.Parameters.AddWithValue("@Idate", mycode.ConvertStringToiDate(txt_date1.Text));
            cmd3.CommandText = "sp_Fee_Collection_Report";
            DataTable dt3 = UsesCode.Getdata_sp(cmd3);
            if (dt3.Rows.Count == 0)
            {
                grid_payment_mode.DataSource = null;
                grid_payment_mode.DataBind();
            }
            else
            {
                grid_payment_mode.DataSource = dt3;
                grid_payment_mode.DataBind();
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

        double total = 0;
        protected void grid_payment_mode_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_payable.Text != "")
                {
                    total = total + Convert.ToDouble(lbl_payable.Text);
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalamount = (Label)e.Row.FindControl("lbl_totalamount");

                lbl_totalamount.Text = total.ToString("0.00");

            }
        }
    }
}