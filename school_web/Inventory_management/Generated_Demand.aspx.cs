using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Generated_Demand : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Demand");
            cmd.Parameters.AddWithValue("@sp_status", "ALLDEMAND");
            cmd.Parameters.AddWithValue("@Is_accept ", 0);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
                Alertme("Sorry! No Stock entry available.", "Warning");
            RPDetails.DataSource = dt;
            RPDetails.DataBind();

        }
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Reference the Repeater Item.
                    RepeaterItem item = e.Item;

                    Label lbl_status = (Label)e.Item.FindControl("lbl_status");
                    Label lbl_Is_accept = (Label)e.Item.FindControl("lbl_Is_accept");
                    if (lbl_Is_accept.Text == "True")
                        lbl_status.Text = "Accepted";
                    else
                        lbl_status.Text = "Not-Accepted";
                }
            }
            catch (Exception ex) { }
        }
        protected void lnk_print_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;

                RepeaterItem item = (RepeaterItem)lnk.NamingContainer;

                Label lbl_Cart_id = (Label)item.FindControl("lbl_Cart_id");
                Label lbl_Demand_no = (Label)item.FindControl("lbl_Demand_no");

                //string path = "../MasterAdmin/Slip/Print_generated_demand.aspx?Demand_no=" + lbl_Demand_no.Text + "&Store_id= " + lbl_Cart_id.Text;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + path + "','_newtab');", true);

            }
            catch (Exception ex) { }

        }
 

        protected void lnk_View_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem item = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Cart_id = (Label)item.FindControl("lbl_Cart_id");
                Label lbl_Demand_no = (Label)item.FindControl("lbl_Demand_no");
                Label lbl_Store_name = (Label)item.FindControl("lbl_Store_name");

                ViewState["Demand_no"] = lbl_Demand_no.Text;
                ViewState["Cart_id"] = lbl_Cart_id.Text;
                lbl_demand_no.Text = lbl_Demand_no.Text;
                lbl_storename.Text = lbl_Store_name.Text;
                hd_view.Value = "1";
                bind_gridview_for_items(lbl_Cart_id, lbl_Demand_no);
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void bind_gridview_for_items(Label lbl_Cart_id, Label lbl_Demand_no)
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Demand");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch2");
            cmd.Parameters.AddWithValue("@Status", "Submitted");
            cmd.Parameters.AddWithValue("@Demand_no", lbl_Demand_no.Text);
            cmd.Parameters.AddWithValue("@Store_id", lbl_Cart_id.Text);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            GrdView_Generate_PO.DataSource = dt;
            GrdView_Generate_PO.DataBind();
            lbl_demand_date.Text = "Date:-" + ds.Tables[0].Rows[0]["Created_Date"].ToString();
        }

        protected void btn_accept_demand_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < GrdView_Generate_PO.Rows.Count; i++)
                {
                    string Id = ((Label)GrdView_Generate_PO.Rows[i].FindControl("lbl_Id")).Text;
                    TextBox txt_remarks = (TextBox)GrdView_Generate_PO.Rows[i].FindControl("txt_remarks");
                    TextBox txt_qty = (TextBox)GrdView_Generate_PO.Rows[i].FindControl("txt_qty");

                    string qry = "Update dbo.[HMS_Geherate_Demand] set Is_accept=1 ,Accept_qty='" + txt_qty.Text + "',Remarks='" + txt_remarks.Text + "', Accepted_by='" + Session["Admin"].ToString() + "', Accepted_date='" + My.datetime_new() + "', Accepted_idate='" + mycode.idate() + "' where Store_id='" + ViewState["Cart_id"].ToString() + "' and Demand_no='" + ViewState["Demand_no"].ToString() + "' and Id='" + Id + "'";
                    My.exeSql(qry);
                    Alertme("Demand accepted successfully", "success");
                    hd_view.Value = "0";
                }
                bind_all_data();
            }
            catch (Exception ex) { }
        }
    }
}