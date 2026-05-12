using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class My_Transaction : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    try
                    {
                        ViewState["Userid"] = Request.QueryString["regid"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session");
                        ddl_session.SelectedValue = My.get_session_id();
                        DataTable dt = PayrollMy.dataTable("select *,(select top 1 URL from Global) as URL  from Firm_Details");
                        if (dt.Rows.Count == 0)
                        {
                            ViewState["Monthly_bill_type"] = "A4";
                            ViewState["URL"] = "";
                        }
                        else
                        {
                            ViewState["URL"] = dt.Rows[0]["URL"].ToString();
                            ViewState["Monthly_bill_type"] = dt.Rows[0]["Monthly_bill_type"].ToString();
                        }

                        bind_grd_view();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void bind_grd_view()
        {
            string query = "select *,CASE WHEN Transection_in = 'Software' THEN 'Offline' WHEN Transection_in = 'App' THEN 'Online'  END AS Paymentin,(select top 1 session_id from  session_details where Session=Student_Payment_History.Session)  as session_id   from  dbo.[Student_Payment_History]  where Addmission_no='" + ViewState["Userid"].ToString() + "' and Session='" + ddl_session.SelectedItem.Text + "'  order by Idate asc";
            try
            {
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    noDataSection.Visible = true;
                    yeSData.Visible = false;
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                }
                else
                {
                    noDataSection.Visible = false;
                    yeSData.Visible = true;
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }
            }
            catch
            {
            }
        }


        string scrpt;
        private void Alertme(string msg, string panel)
        {
            if (panel == "success")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alertSuccess('" + msg + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alertError('" + msg + "');", true);
            }
        }

        protected void btn_find_session_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                bind_grd_view();
            }
        }
        int loopCount = 1;
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        { 
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_admission_no = ((Label)e.Item.FindControl("lbl_Addmission_no")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_Class_id")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_Receipt_No = ((Label)e.Item.FindControl("lbl_Slip_no")) as Label;
                HtmlAnchor printLnk = (HtmlAnchor)e.Item.FindControl("printLnk");
                string page = "Admin/slip/monthly-slip.aspx";
                if (ViewState["Monthly_bill_type"].ToString() == "A5")
                {
                    page = ViewState["URL"] + "Admin/slip/monthly-slip-a5.aspx";
                }
                else if (ViewState["Monthly_bill_type"].ToString() == "A5S")
                {
                    page = ViewState["URL"] + "Admin/slip/bill-with-scholarship.aspx";
                }
                else if (ViewState["Monthly_bill_type"].ToString() == "A5I")
                {
                    page = ViewState["URL"] + "Admin/slip/bill-installment.aspx";
                }
                else
                {
                    page = ViewState["URL"] + "Admin/slip/monthly-slip.aspx";
                }
                printLnk.HRef = page + "?admissionno=" + lbl_admission_no.Text + "&sessionid=" + lbl_session_id.Text + "&classid=" + lbl_class_id.Text + "&Slip_no=" + lbl_Receipt_No.Text + "&mobile=yes";




                HtmlGenericControl paycard = (HtmlGenericControl)e.Item.FindControl("paycard");
                HtmlGenericControl iconBox = (HtmlGenericControl)e.Item.FindControl("iconBox");
                HtmlGenericControl cardFooter = (HtmlGenericControl)e.Item.FindControl("cardFooter");
                if (loopCount == 1)
                { 
                    paycard.Attributes["class"] = "payment-card card-blue";
                    iconBox.Attributes["class"] = "icon-box blue";
                    cardFooter.Attributes["class"] = "card-footer footer-blue";
                }
                if (loopCount == 2)
                { 
                    paycard.Attributes["class"] = "payment-card card-pink";
                    iconBox.Attributes["class"] = "icon-box pink";
                    cardFooter.Attributes["class"] = "card-footer footer-pink";
                }
                if (loopCount == 3)
                {
                    loopCount = 0;
                    paycard.Attributes["class"] = "payment-card card-green";
                    iconBox.Attributes["class"] = "icon-box green";
                    cardFooter.Attributes["class"] = "card-footer footer-green";
                }
                loopCount++;
            }
        }
    }
}