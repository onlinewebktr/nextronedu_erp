using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class incomplete_purchase_entri : System.Web.UI.Page
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
            try
            {
                SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_itemwise");
                cmd.Parameters.AddWithValue("@sp_status", "INCOMPLETEPE");
                cmd.Parameters.AddWithValue("@firm ", My.firm_id());
                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                    Alertme("Sorry! No data available.", "Warning");
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
            catch
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
                Label lbl_party_id = (Label)row.FindControl("lbl_party_id");

                //SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_billwise");
                //cmd.Parameters.AddWithValue("@sp_status", "FetchItems");
                //cmd.Parameters.AddWithValue("@invoice_no", lbl_invoice_no.Text);
                //cmd.Parameters.AddWithValue("@firm ", My.firm_id());
                //DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                //DataTable dt = ds.Tables[0];
                //int rowcount = ds.Tables[0].Rows.Count;
                //if (rowcount == 0)
                //    Alertme("Sorry! No Stock entry available.", "Warning");
                //Repeater1.DataSource = dt;
                //Repeater1.DataBind();

                //lblparty_name.Text = lbl_party_name.Text;
                //lblinvoice_no.Text = lbl_invoice_no.Text;

                string Path = "Purchase_entry.aspx?PO_no=" + lbl_invoice_no.Text+ "&party_id=" + lbl_party_id.Text;
                Response.Redirect(Path);

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnk_delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_invoice_no = (Label)row.FindControl("lbl_invoice_no");
                Label lbl_party_id = (Label)row.FindControl("lbl_party_id");
                mycode.executequery("Delete from HMS_inventory_purchase_entry_itemwise where Status='Pending' and invoice_no='" + lbl_invoice_no.Text + "' and party_id='" + lbl_party_id.Text + "'");
                Alertme("Record has been deleted successfully", "Warning");
                bind_all_data();
            }
            catch
            {

            }
           
        }
    }
}