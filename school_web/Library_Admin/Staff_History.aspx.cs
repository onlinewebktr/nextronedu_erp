using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Library_Admin
{
    public partial class Staff_History : System.Web.UI.Page
    {
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["flag"] = "0";


                        bind_all_data();
                        find_firm_details();


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }

        }
        private void bind_all_data()
        {
            bind_grd_view("select * from  lib_staff_transaction_details   where Branch_id = '" + ViewState["Branch_id"].ToString() + "' order by id desc");
        }
        #region CountDataA

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
        #endregion
        My mycode = new My();

        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details where Branch_id = '" + ViewState["Branch_id"].ToString() + "' ");
            if (dt.Rows.Count == 0)
            {




            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue == "All")
            {
                bind_grd_view("select * from  lib_staff_transaction_details where library_card_no ='" + Txt_card.Text + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' ");
            }
            else if (ddlstatus.SelectedValue == "Issued")
            {
                bind_grd_view("select * from  lib_staff_transaction_details  where library_card_no ='" + Txt_card.Text + "' and status ='Issued' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
            }
            else if (ddlstatus.SelectedValue == "Returned")
            {
                bind_grd_view("select * from  lib_staff_transaction_details  where library_card_no ='" + Txt_card.Text + "' and status='Returned' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' ");
            }

        }

    }
}