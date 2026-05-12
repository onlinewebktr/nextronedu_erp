using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Generated_OP : System.Web.UI.Page
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
                        txt_from_Date.Text = mycode.date();
                        txt_to_Date.Text = mycode.date();
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

            if (Session["userType"].ToString() == "Admin")
            {
                SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Purchase_order");
                cmd.Parameters.AddWithValue("@sp_status ", "Fetch3");
                cmd.Parameters.AddWithValue("@Status", "Submitted");
                cmd.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_Date.Text));
                cmd.Parameters.AddWithValue("@todate", My.convertidate(txt_to_Date.Text));
                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Purchase_order");
                cmd.Parameters.AddWithValue("@sp_status ", "Fetch4");
                cmd.Parameters.AddWithValue("@Status", "Submitted");
                cmd.Parameters.AddWithValue("@fromdate", My.convertidate(txt_from_Date.Text));
                cmd.Parameters.AddWithValue("@todate", My.convertidate(txt_to_Date.Text));
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();
            }
        }

        protected void lnk_print_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            string PO_no = ((Label)row.FindControl("lbl_Purchase_id")).Text;
            string firm = ((Label)row.FindControl("lbl_firm")).Text;
            string Session = ((Label)row.FindControl("lbl_Session")).Text;
            string entry_id_po = ((Label)row.FindControl("lbl_entry_id_po")).Text;

            //string path = "Slip/Print_generated_PO.aspx?PO_no=" + PO_no + "&entryid=" + entry_id_po + "&Session=" + Session + "&firmid=" + firm;

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + path + "','_newtab');", true);
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
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