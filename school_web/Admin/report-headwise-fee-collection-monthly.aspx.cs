using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class report_headwise_fee_collection_monthly : System.Web.UI.Page
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


                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

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
                        hd_from_date.Value = idate1.ToString();
                        hd_to_date.Value = idate21.ToString();
                        hd_class_id.Value = ddl_class.SelectedValue;
                        hd_section.Value = ddl_section.SelectedItem.Text;

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
                find_ttl_grid(idate1, idate21, type);
                btn_excels.Visible = true;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
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


        double Grd_total = 0;
        private void find_ttl_grid(int idate1, int idate21, string type)
        {
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");
            cmd.Parameters.AddWithValue("@fromdate ", idate1);
            cmd.Parameters.AddWithValue("@todate ", idate21);
             
            cmd.Parameters.AddWithValue("@parameter ", "MonthlyFee");
            if (type == "OnlyDatE")
            {
                cmd.Parameters.AddWithValue("@sp_status ", "4");
            }

            if (type == "DatEClasS")
            {
                cmd.Parameters.AddWithValue("@class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "7");
            }
            if (type == "DatEClasSSectioN")
            {
                cmd.Parameters.AddWithValue("@class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@section ", ddl_section.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@sp_status ", "8");
            }
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                Grd_total = My.toDouble(dt.Compute("Sum(Paid_amt)", ""));
                grd_ttl.DataSource = dt;
                grd_ttl.DataBind();
            }
            else
            {
                grd_ttl.DataSource = null;
                grd_ttl.DataBind();
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
                    fetch_headwise_fee_by_date(lbl_idate.Text, GrdViews);
                }
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

        private void fetch_headwise_fee_by_date(string idate, GridView GrdViews)
        {
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");
            cmd.Parameters.AddWithValue("@fromdate ", idate);
           
            cmd.Parameters.AddWithValue("@parameter ", "MonthlyFee");
            cmd.Parameters.AddWithValue("@parameter2 ", "HostelMonthlyFee");
            if (ViewState["TypES"].ToString() == "OnlyDatE")
            {
                cmd.Parameters.AddWithValue("@sp_status ", "3");
            }

            if (ViewState["TypES"].ToString() == "DatEClasS")
            {
                cmd.Parameters.AddWithValue("@class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "9");
            }
            if (ViewState["TypES"].ToString() == "DatEClasSSectioN")
            {
                cmd.Parameters.AddWithValue("@class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@section ", ddl_section.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@sp_status ", "10");
            }
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                total = My.toDouble(dt.Compute("Sum(Paid_amt)", ""));
                GrdViews.DataSource = dt;
                GrdViews.DataBind();
            }
            else
            {
                GrdViews.DataSource = null;
                GrdViews.DataBind();
            }
        }

        protected void GrdViews_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void grd_ttl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lbltotals = (Label)e.Row.FindControl("lbltotals");
                    lbltotals.Text = Grd_total.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
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
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
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
    }
}