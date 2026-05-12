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

namespace school_web.Inventory_management
{
    public partial class purchase_return_history : System.Web.UI.Page
    {
        My mycode = new My();
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null )
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('logout.aspx','_parent',replace=true);</script>");
            }
            else
            {
                ViewState["Userid"] = Session["Admin"].ToString();
                if (!IsPostBack)
                {
                    try
                    {
                        bind_all_data();
                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }

        private void bind_all_data()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_return_entry_billwise");
            cmd.Parameters.AddWithValue("@sp_status", "FetchData");
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
                Alertme("Sorry! No Stock entry available.", "Warning");
            RPDetails.DataSource = dt;
            RPDetails.DataBind();
        }



        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_from_Date.Text == "")
                {
                    Alertme("Please enter from date.", "warning");
                    txt_from_Date.Focus();
                }
                else if (txt_to_Date.Text == "")
                {
                    Alertme("Please enter to date.", "warning");
                    txt_to_Date.Focus();
                }
                else
                {
                    int Sdate = My.DateConvertToIdate(txt_from_Date.Text);
                    int Edate = My.DateConvertToIdate(txt_to_Date.Text);
                    SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_return_entry_billwise");
                    cmd.Parameters.AddWithValue("@sp_status", "FetchDataByDate");
                    cmd.Parameters.AddWithValue("@fromdate", Sdate);
                    cmd.Parameters.AddWithValue("@todate", Edate);
                    cmd.Parameters.AddWithValue("@firm ", My.firm_id());
                    DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount == 0)
                        Alertme("Sorry! No Stock entry available.", "Warning");
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnk_view_items_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_party_name = (Label)row.FindControl("lbl_party_name");
                Label lbl_invoice_no = (Label)row.FindControl("lbl_invoice_no");

                SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_return_entry_billwise");
                cmd.Parameters.AddWithValue("@sp_status", "FetchItems");
                cmd.Parameters.AddWithValue("@invoice_no", lbl_invoice_no.Text);
                cmd.Parameters.AddWithValue("@firm ", My.firm_id());
                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                    Alertme("Sorry! No Stock entry available.", "Warning");
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

                lblparty_name.Text = lbl_party_name.Text;
                lblinvoice_no.Text = lbl_invoice_no.Text;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
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
            catch (Exception ex)
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        protected void lnk_excel_download1_Click(object sender, EventArgs e)
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
        #endregion download_in_excel
    }
}