using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class report_mode_headwise_fee_collection : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    if (!IsPostBack)
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = Session["branchid"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddl_class.SelectedValue = My.get_top_one_class();

                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");

                        ddl_section.Text = My.get_top_one_section();


                        txt_s_date.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date();
                        Bind_data_date_wise("OnlyDatE");
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Collection_Report");
            }
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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


        private void Bind_data_date_wise(string type)
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    string sdate = txt_s_date.Text;
                    string sday = sdate.Substring(0, 2);
                    string smonth = sdate.Substring(3, 2);
                    string syear = sdate.Substring(6, 4);

                    string edate = txt_e_date.Text;
                    string eday = edate.Substring(0, 2);
                    string emonth = edate.Substring(3, 2);
                    string eyear = edate.Substring(6, 4);

                    int idate1 = Convert.ToInt32(syear + smonth + sday);
                    int idate21 = Convert.ToInt32(eyear + emonth + eday);

                    //=====
                    if (ddl_class.SelectedItem.Text == "Select")
                    {
                        lbl_class22.Text = txt_s_date.Text + " To " + txt_e_date.Text;
                    }
                    else if (ddl_class.SelectedItem.Text == "ALL")
                    {
                        lbl_class22.Text = txt_s_date.Text + " To " + txt_e_date.Text + ", Class : " + ddl_class.SelectedItem.Text;
                    }
                    else
                    {
                        lbl_class22.Text = txt_s_date.Text + " To " + txt_e_date.Text + ", Class : " + ddl_class.SelectedItem.Text + ", Section : " + ddl_section.SelectedItem.Text;
                    }
                    //////=====


                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.", "warning");
                    }
                    else
                    {
                        final_find_report_by_date(idate1, idate21, type);
                    }
                }
            }
        }

        private void final_find_report_by_date(int idate1, int idate21, string type)
        {
            ViewState["TypES"] = type;
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");
            cmd.Parameters.AddWithValue("@fromdate ", idate1);
            cmd.Parameters.AddWithValue("@todate ", idate21);
            cmd.Parameters.AddWithValue("@Session ", My.get_session());
            cmd.Parameters.AddWithValue("@Type ", "Monthly");
            if (type == "OnlyDatE")
            {
                cmd.Parameters.AddWithValue("@sp_status ", "2");
            }

            if (type == "DatEClasS")
            {
                cmd.Parameters.AddWithValue("@class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "5");
            }
            if (type == "DatEClasSSectioN")
            {
                cmd.Parameters.AddWithValue("@class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@section ", ddl_section.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@sp_status ", "6");
            }
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
                btn_excels.Visible = true;
                print1.Visible = true;
                NotFoundS.Visible = false;
                tblPrintIQ.Visible = true;
            }
            else
            {
                GrdView.DataSource = null;
                GrdView.DataBind();
                btn_excels.Visible = false;
                print1.Visible = false;
                NotFoundS.Visible = true;
                tblPrintIQ.Visible = false;
            }
        }





        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date.", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date.", "warning");
                txt_e_date.Focus();
            }
            else
            {
                Bind_data_date_wise("OnlyDatE");
            }
        }


        double total = 0;
        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl_idate = (e.Row.FindControl("lbl_idate") as Label);
                    GridView GrdViews = (e.Row.FindControl("GrdViews") as GridView);
                    fetch_payment_mode(lbl_idate.Text, GrdViews);
                }
                //if (e.Row.RowType == DataControlRowType.Footer)
                //{
                //    Label lbltotal = (Label)e.Row.FindControl("lbltotal");
                //    lbltotal.Text = total.ToString("0.00");
                //}
            }
            catch (Exception ex)
            {
            }
        }
        double total_pay_mode;
        private void fetch_payment_mode(string idate, GridView GrdViews)
        {
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");
            cmd.Parameters.AddWithValue("@fromdate ", idate);
            cmd.Parameters.AddWithValue("@Session ", My.get_session());
            cmd.Parameters.AddWithValue("@parameter ", "Monthly");
            cmd.Parameters.AddWithValue("@sp_status ", "32");


            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                total_pay_mode = My.toDouble(dt.Compute("Sum(Paid_amt)", ""));
                GrdViews.DataSource = dt;
                GrdViews.DataBind();
            }
            else
            {
                GrdViews.DataSource = null;
                GrdViews.DataBind();
            }
        }




        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    pnl_grid.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btn_find_by_class_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_s_date.Text == "")
                {
                    Alertme("Please choose from date.", "warning");
                    txt_s_date.Focus();
                }
                else if (txt_e_date.Text == "")
                {
                    Alertme("Please choose to date.", "warning");
                    txt_e_date.Focus();
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose class.", "warning");
                    ddl_class.Focus();
                }
                else
                {
                    find_data_by_class();
                }
            }

            catch (Exception ex)
            {
            }
        }

        private void find_data_by_class()
        {
            if (ddl_section.SelectedItem.Text == "ALL")
            {
                Bind_data_date_wise("DatEClasS");
            }
            else
            {
                Bind_data_date_wise("DatEClasSSectioN");
            }
        }

        protected void GrdViews_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl_paymode = (e.Row.FindControl("lbl_paymode") as Label);
                    Label lbl_idate = (e.Row.FindControl("lbl_idates") as Label);

                    GridView Grd_head_amounts = (e.Row.FindControl("Grd_head_amounts") as GridView);
                    fetch_payment_amts(lbl_idate.Text, Grd_head_amounts, lbl_paymode.Text);
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lbltotal = (Label)e.Row.FindControl("lbltotal");
                    lbltotal.Text = total_pay_mode.ToString("0.00");
                    e.Row.CssClass = "ftrbgcolors";

                }
            }
            catch (Exception ex)
            {
            }
        }


        private void fetch_payment_amts(string idate, GridView Grd_head_amounts, string p_mode)
        {
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");
            cmd.Parameters.AddWithValue("@fromdate ", idate);
            cmd.Parameters.AddWithValue("@Session ", My.get_session());
            cmd.Parameters.AddWithValue("@parameter ", "MonthlyFee");
            cmd.Parameters.AddWithValue("@Payment_mode ", p_mode);
            cmd.Parameters.AddWithValue("@sp_status ", "33");

            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                total = My.toDouble(dt.Compute("Sum(Paid_amt)", ""));
                Grd_head_amounts.DataSource = dt;
                Grd_head_amounts.DataBind();
            }
            else
            {
                Grd_head_amounts.DataSource = null;
                Grd_head_amounts.DataBind();
            }
        }

        protected void Grd_head_amounts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lbltotal = (Label)e.Row.FindControl("lbltotal");
                    lbltotal.Text = total.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnk_view_details_Click(object sender, EventArgs e)
        {
            try
            { 
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_paymode = (Label)row.FindControl("lbl_paymode");
                Label lbl_idates = (Label)row.FindControl("lbl_idates");

                string sdate = lbl_idates.Text;
                string syear = sdate.Substring(0,4);
                string smonth = sdate.Substring(4, 2);
                string sday = sdate.Substring(6, 2);



                lbl_pop_info.Text = "Payment Mode : " + lbl_paymode.Text + " & Date : " + sday + "/" + smonth + "/" + syear;

                SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");
                cmd.Parameters.AddWithValue("@fromdate ", lbl_idates.Text);
                cmd.Parameters.AddWithValue("@parameter ", "MonthlyFee");
                cmd.Parameters.AddWithValue("@Payment_mode ", lbl_paymode.Text);
                cmd.Parameters.AddWithValue("@sp_status ", "34");

                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount > 0)
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    Alertme("Something went wrong.", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                } 
            }
            catch
            {

            }

        }

    }
}