using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class income_and_expense_report_new1 : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        hd_collection_type.Value = "1";
                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();
                        find_firm_details(); //get_class_id();
                        string pagename_current = "fee-report.aspx";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        lbl_report_type.Text = "Income & Expense Detail for Date : " + txt_from_date.Text + " to " + txt_to_date.Text;
                        if (txt_from_date.Text == txt_to_date.Text)
                        {
                            DateTime datatimes = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            lbl_report_type.Text = "Income & Expense Detail for Date : " + txt_from_date.Text + " - " + datatimes.ToString("dddd");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_group_master");
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_report_type.Text = "Income & Expense Detail for Date : " + txt_from_date.Text + " to " + txt_to_date.Text;
                if (txt_from_date.Text == txt_to_date.Text)
                {
                    DateTime datatimes = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    lbl_report_type.Text = "Income & Expense Detail for Date : " + txt_from_date.Text + " - " + datatimes.ToString("dddd");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}