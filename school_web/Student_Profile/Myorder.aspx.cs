using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class Myorder : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        Session["regid"] = ViewState["regid"].ToString();
                        Bind_data_list();



                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        UsesCode mycode = new UsesCode();
        private void Bind_data_list()
        {
            string query = $"select  *,(Select top 1 unique_no from HMS_INVETORY_SELL_DETAILS_BILLWISE where party_id=Online_Sell_billwise.user_id and Bill_No=Online_Sell_billwise.New_bill_no) as unique_no from Online_Sell_billwise where user_id='{ViewState["regid"]}' and SesssionId='{ ViewState["sesssionid"]}' and Payment_Status='Paid'";
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count != 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
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
            Label lbl_Order_Status = (Label)e.Item.FindControl("lbl_Order_Status");
            Label lbl_unique_no = (Label)e.Item.FindControl("lbl_unique_no");

            Panel Panel1 = (Panel)e.Item.FindControl("Panel1");
            if (lbl_Order_Status.Text == "Pending")
            {
                Panel1.Visible = false;
            }
            else
            {
                Panel1.Visible = true;
            }
            HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("a1");
            string url = My.URL()+"StudentWebview/Invoice_view.aspx?unique_entry_id=" + lbl_unique_no.Text + "+&partyid=" + ViewState["regid"].ToString() + "&from=Desktop";
            a1.HRef = url;
        }

        protected void btn_view_Click(object sender, EventArgs e)
        {
            Button lnk = (Button)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_invoice_no = (Label)row.FindControl("lbl_Bill_No");
            Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
            Label lbl_user_id = (Label)row.FindControl("lbl_userid");



            Label lbl_Total_amount = (Label)row.FindControl("lbl_Total_amount");
            lbl_total_value.Text = lbl_Total_amount.Text;
            find_item_details(lbl_invoice_no.Text, lbl_user_id.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }

        private void find_item_details(string invoice_no, string user_id)
        {
            DataTable dt = mycode.FillData(" select *,(select top 1 Item_Name from HMS_Invetory_item_Master where Item_id=t1.Item_Code) as Item_name,(select top 1 Unit from unit_master where unit_id=t1.Unit_id) as Unit_name from Online_Sell_item_wise t1 where t1.Bill_No='" + invoice_no + "' and Sell_To='" + user_id + "' ");
            if (dt.Rows.Count == 0)
            {
                GrdView_Generate_PO.DataSource = null;
                GrdView_Generate_PO.DataBind();

            }
            else
            {
                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();
            }


        }


    }
}