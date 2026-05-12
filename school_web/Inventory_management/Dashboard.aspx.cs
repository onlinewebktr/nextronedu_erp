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
    public partial class Dashboard : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["userType"] = "Admin";
            ////Session["Admin"] = "Admin";
            ////Session["name"] = "Admin";
            //Session["Admin"] = "edunext2021";
            //Session["name"] = "edunext2021";
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('logout.aspx','_parent',replace=true);</script>");
            }
            else
            {

                if (!IsPostBack)
                {
                    try
                    {
                        ferch_home_count();
                        //bind_generated_demand(); 
                        bind_recent_purchase();
                        bind_recent_order();
                        // fetch_max_sale();
                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }

            }
        }
        My mycode = new My();
        private void bind_recent_order()
        {
            string query = "Select top 10 osb.*,format(Date,'dd/MM/yyyy') as Date1, (select top 1 studentname from admission_registor where admissionserialnumber=osb.user_id order by id desc) as studentname  from Online_Sell_billwise osb  where  Order_Status='Pending' and Payment_Status='Paid' order by osb.Idate desc ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {


                Repeater2.DataSource = null;
                Repeater2.DataBind();
            }
            else
            {
                Repeater2.DataSource = dt;
                Repeater2.DataBind();
            }

        }

        private void fetch_max_sale()
        {
            SqlCommand cmd = new SqlCommand("sp_inventory_home");
            cmd.Parameters.AddWithValue("@sp_status ", "FETCHMax");
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                MostTransfered.InnerText = dt.Rows[0][0].ToString();
            }
            else
            {
                MostTransfered.InnerText = "00";
            }
        }


        private void bind_generated_demand()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Geherate_Demand");
            cmd.Parameters.AddWithValue("@sp_status", "ALLDEMAND5");
            cmd.Parameters.AddWithValue("@Is_accept ", 0);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
                Alertme("Sorry! No Stock entry available.", "Warning");
            RPDetails.DataSource = dt;
            RPDetails.DataBind();
        }

        private void ferch_home_count()
        {
            SqlCommand cmd = new SqlCommand("sp_inventory_home");
            cmd.Parameters.AddWithValue("@sp_status ", "FETCH");
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                in_of_stocK.InnerText = dt.Rows[0][0].ToString();
                out_of_stocK.InnerText = dt.Rows[1][0].ToString();
                demand_requesT.InnerText = dt.Rows[2][0].ToString();
            }
            else
            {
                in_of_stocK.InnerText = "00";
                out_of_stocK.InnerText = "00";
                demand_requesT.InnerText = "00";
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



        private void bind_recent_purchase()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_billwise");
            cmd.Parameters.AddWithValue("@sp_status", "FetchData5");
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
                Alertme("Sorry! No Stock entry available.", "Warning");
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

    }
}