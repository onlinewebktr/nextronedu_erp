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
    public partial class stock_storewise : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_store, "select Store_name,Store_id from HMS_Invetory_Create_Store order by Store_name asc");
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
            SqlCommand cmd = new SqlCommand("sp_HMS_Inventory_stock_details");
            cmd.Parameters.AddWithValue("@sp_status", "Fetch_all_stock");
            cmd.Parameters.AddWithValue("@Sector_id", "1");
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
                if (ddl_store.SelectedItem.Text == "Select")
                {
                    Alertme("Please select store.", "warning");
                    ddl_store.Focus();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_HMS_Inventory_stock_details");
                    cmd.Parameters.AddWithValue("@sp_status", "Fetch_Stock_StoreWise");
                    cmd.Parameters.AddWithValue("@Store_id", ddl_store.SelectedValue);
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