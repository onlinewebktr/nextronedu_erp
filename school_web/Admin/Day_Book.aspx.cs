using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Day_Book : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();
                    string pagename_current = "view_ledger.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];
                    find_firm_details();

                   // txt_e_date.Text = mycode.date();
                    txt_s_date.Text = mycode.date();
                    viewledger();

                }
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_s_date.Text == "")
                {
                    Alertme("Please choose from date", "warning");
                    txt_s_date.Focus();
                }

                else
                {

                    viewledger();


                }

            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void viewledger()
        {
            LedgerSummary(My.get_session());
        }
        string start_date = "";
        string end_date = "";
        DataTable ldt;
        private void LedgerSummary(string current_session)
        {
            //string session = current_session;
            txt_period.Text = txt_s_date.Text;
            //start_date = session.Split('-')[0] + "0401";
            //end_date = session.Split('-')[1] + "0331";
            start_date = My.convertidate(txt_s_date.Text);

            txt_ledger_name.Text = "Day Book";

            lbl_class22.Text = txt_period.Text;

          //  ldt = My.dataTable("select (Select  top 1 name from user_details where user_id=ld.Created_by) User_name ,ld.Date,Account_Name,ld.Description,ld.VoucherType,ld.VoucherNo_Manual as VoucherNo,ld.Debit,ld.Credit,ad.Account_id,ld.unique_entry_id from dbo.[Account_Voucher_Details]  ld join  Account_Ledger_Details as ad on ad.Account_id =ld.Alternet_Account   and ld.firm=ad.firm   where    ld.firm ='" + My.firm_id() + @"' and idate>=" + start_date + " and idate <=" + end_date + "   order by idate,ld.id");


            ldt = My.dataTable("select (Select  top 1 name from user_details where user_id=ld.Created_by) User_name,ld.Description,   ld.Date,Account_Name,ld.VoucherType,ld.VoucherNo_Manual as VoucherNo,ld.Debit,ld.Credit,(Select top 1 Account_name from Account_Ledger_Details where Account_id=ld.Account_id) as particular,(Select top 1 Account_name from Account_Ledger_Details where Account_id=ld.Alternet_Account) as accountledger from dbo.[Account_Voucher_Details]  ld join  Account_Ledger_Details as ad on ad.Account_id =ld.Account_id   and ld.firm=ad.firm  where  ld.IDate='" + Convert.ToDateTime(txt_s_date.Text).ToString("yyyyMMdd") + "' and ld.firm ='" + My.firm_id() + @"' and ld.Group_id in ('27','28') and VoucherType in ('Receipt','Payment')   order by   idate,VoucherType,VoucherNo  ");//'22','26',





            //}
            double total_debit = 0;
            double total_credit = 0;
            foreach (DataRow dr in ldt.Rows)
            {
                double credit = My.toDouble(dr["Credit"].ToString());
                double debit = My.toDouble(dr["Debit"].ToString());
                if (credit > 0)
                {
                    total_credit += credit;
                }
                if (debit > 0)
                {
                    total_debit += debit;
                }
            }
            txt_total_debit.Text = "0.00";
            txt_total_credit.Text = "0.00";
            if (total_debit > 0)
            {
                txt_total_debit.Text = total_debit.ToString("0.00");
            }
            if (total_credit > 0)
            {
                txt_total_credit.Text = total_credit.ToString("0.00");
            }

            //if (ob_bal > 0)
            //{

            //    total_credit += ob_bal;
            //}
            //else
            //{

            //    total_debit += (ob_bal * (-1));

            //}


            if (ldt.Rows.Count == 0)
            {
                Panel1.Visible = false;
                grd_view.DataSource = null;
                grd_view.DataBind();
                btn_excels.Visible = false;
                print1.Visible = false;
            }
            else
            {
                Panel1.Visible = true;
                btn_excels.Visible = true;
                grd_view.DataSource = ldt.DefaultView;
                grd_view.DataBind();
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }

            }

            //UpdateLayout();

            //My.current_cell(grd_view); 
        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Download"].ToString() == "1")
            {
                string pageName = My.with_excel_name("Day_book") + ".xls";

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + pageName);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    Panel1.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion download_in_excel
    }
}