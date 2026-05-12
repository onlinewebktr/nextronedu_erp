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

namespace school_web.Admin
{
    public partial class view_ledger : System.Web.UI.Page
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

                    txt_e_date.Text = mycode.date();
                    txt_s_date.Text = mycode.date();


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
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> searching_by_ledger_name(string Ledgername)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select distinct top 25  isnull(Account_Name+','+pd.mobile,Account_Name) as Account,Account_Name from dbo.[Account_Ledger_Details] ald left join party_details pd on ald.firm=pd.firm and ald.Account_id=pd.party_id  where  ald.firm='" + My.firm_id() + "' and Account_Name LIKE '%'+@SearchprojectName+'%' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", Ledgername);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Account_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
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
                else if (txt_e_date.Text == "")
                {
                    Alertme("Please choose to date", "warning");
                    txt_e_date.Focus();
                }
                else
                {
                    string sdate = txt_s_date.Text;
                    string sday = sdate.Substring(0, 2);
                    string smonth = sdate.Substring(3, 2);
                    string syear = sdate.Substring(6, 4);

                    string edate = txt_e_date.Text;
                    string eday = edate.Substring(0, 2);
                    string emonth = edate.Substring(3, 2);
                    string eyear = edate.Substring(6, 4);

                    int idate = Convert.ToInt32(syear + smonth + sday);
                    int idate2 = Convert.ToInt32(eyear + emonth + eday);

                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.", "warning");
                    }
                    else
                    {
                        string account_name = txt_account_name.Text;
                        viewledger(account_name);
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }


        private void viewledger(string account_name)
        {
            string acc_id = My.get_table_data(" select Account_id from dbo.[Account_Ledger_Details] where  Account_Name='" + account_name + "' and  firm ='" + My.firm_id() + @"'");
            if (acc_id == "")
            {
                if (account_name == "")
                {
                    string leder_type = "SCHOOL";
                    LedgerSummary(account_name, acc_id, My.get_session(), leder_type);
                }
                else
                {
                    Alertme("Account not found.", "warning");
                    return;
                } 
            }
            else
            {
                string leder_type = "SCHOOL"; 
                LedgerSummary(account_name, acc_id, My.get_session(), leder_type);
            }
        }
        string start_date = "";
        string end_date = "";
        DataTable ldt;
        private void LedgerSummary(string account_name, string Account_id, string current_session, string leder_type)
        {
            //string session = current_session;
            txt_period.Text = txt_s_date.Text + " to " + txt_e_date.Text;
            //start_date = session.Split('-')[0] + "0401";
            //end_date = session.Split('-')[1] + "0331";

            start_date = My.convertidate(txt_s_date.Text);
            end_date = My.convertidate(txt_e_date.Text);


            txt_ledger_name.Text = account_name;
            lbl_class22.Text = account_name + " " + txt_period.Text;
            ldt = My.dataTable("select case when Bill_from='SCHOOL' Then (Select  top 1 name from user_details where user_id=ld.Created_by) else (Select  top 1 name from user_details where user_id=ld.Created_by) end as User_name ,ld.Date,Account_Name,ld.Description,ld.VoucherType,ld.VoucherNo_Manual as VoucherNo,ld.Debit,ld.Credit,ad.Account_id,ld.unique_entry_id from dbo.[Account_Voucher_Details]  ld join  Account_Ledger_Details as ad on ad.Account_id =ld.Account_id   and ld.firm=ad.firm where ld.Account_id='" + Account_id + "' and ld.firm ='" + My.firm_id() + @"' and idate>=" + start_date + " and idate <=" + end_date + "   order by idate,ld.id");//and ld.Bill_from in ('" + leder_type + "')
            if (account_name == "")
            {
                ldt = My.dataTable("select case when Bill_from='SCHOOL' Then (Select  top 1 name from user_details where user_id=ld.Created_by) else (Select  top 1 name from user_details where user_id=ld.Created_by) end as User_name ,ld.Date,Account_Name,ld.Description,ld.VoucherType,ld.VoucherNo_Manual as VoucherNo,ld.Debit,ld.Credit,ad.Account_id,ld.unique_entry_id from dbo.[Account_Voucher_Details]  ld join  Account_Ledger_Details as ad on ad.Account_id =ld.Account_id and ld.firm=ad.firm where ld.firm ='" + My.firm_id() + @"' and idate>=" + start_date + " and idate <=" + end_date + "   order by idate,ld.id");//and ld.Bill_from in ('" + leder_type + "')
            }
            double ob_bal = 0;
            ob_bal += My.toDouble(My.get_table_data("select sum(Credit-Debit) as  total from [Account_Voucher_Details]   where  Account_id='" + Account_id + "' and firm ='" + My.firm_id() + @"'  and IDate<" + start_date + " "));//and Bill_from in ('" + leder_type + "')

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

            if (ob_bal > 0)
            {
                txt_ob_debit.Text = "0.00";
                txt_ob_credit.Text = ob_bal.ToString("0.00");
                total_credit += ob_bal;
            }
            else
            {
                txt_ob_credit.Text = "0.00";
                total_debit += (ob_bal * (-1));
                txt_ob_debit.Text = (ob_bal * (-1)).ToString("0.00");
            }
            if (total_credit > total_debit)
            {
                txt_cb_debit.Text = "0.00";
                txt_cb_credit.Text = (total_credit - total_debit).ToString("0.00");
            }
            else if (total_credit <= total_debit)
            {
                txt_cb_debit.Text = (total_debit - total_credit).ToString("0.00");
                txt_cb_credit.Text = "0.00";
            }

            if (ldt.Rows.Count == 0)
            {
                Panel1.Visible = true;
                grd_view.DataSource = null;
                grd_view.DataBind();
                btn_excels.Visible = true;
                print1.Visible = true;
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