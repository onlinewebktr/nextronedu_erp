using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class ledger : System.Web.UI.Page
    {
        My mycode = new My();
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
                        txt_from_date.Text = "01/" + mycode.monthYear();
                        txt_to_date.Text = mycode.date();
                        get_all_ledger();
                        ViewState["flag"] = "0";
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

        private void get_all_ledger()
        {
            bind_grids("select * from Student_Payment_History  where Session='" + My.get_session() + "' and Addmission_no='" + ViewState["regid"].ToString() + "' ORDER BY Idate ASC");
        }

        private void bind_grids(string qrys)
        {
            DataTable dt = mycode.FillData(qrys);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry! There are no payment history exists", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
            try
            {
                if (txt_from_date.Text == "")
                {
                    Alertme("Please choose from date.", "warning");
                    txt_from_date.Focus();
                }
                else if (txt_to_date.Text == "")
                {
                    Alertme("Please choose to date.", "warning");
                    txt_from_date.Focus();
                }
                else
                {
                    get_ledger_by_date();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void get_ledger_by_date()
        {
            bind_grids("select * from Student_Payment_History  where Session='" + My.get_session() + "' and Addmission_no='" + ViewState["regid"].ToString() + "' and Idate>=" + My.DateConvertToIdate(txt_from_date.Text) + " and Idate<=" + My.DateConvertToIdate(txt_to_date.Text) + " ORDER BY Idate ASC");
        }

        double ttl_amt = 0;
        protected void rd_view_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_amts = ((Label)e.Item.FindControl("lbl_amts")) as Label;
                ttl_amt = ttl_amt + My.toDouble(lbl_amts.Text);
            }

            lbl_ttl_amts.Text = "Total Amount : ₹ " + ttl_amt.ToString("0.00");
        }
    }
}