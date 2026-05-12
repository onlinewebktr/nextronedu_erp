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
    public partial class Student_Transaction_Summary : System.Web.UI.Page
    {
        My mycode = new My();
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
                if(!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();

                    string pagename_current = Path.GetFileName("collection-sheet-dcr.aspx");
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];
                    txt_from_date.Text = mycode.date();
                    txt_to_date.Text = mycode.date();
                    find_firm_details();

                    find_by_date();

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
            if (txt_from_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_from_date.Focus();
            }
            else if (txt_to_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_to_date.Focus();
            }
            else
            {
                find_by_date();
            }

        }

        private void find_by_date()
        {
            Panel1.Visible = false;
            double totalDebitCash = 0, totalDebitBank = 0, totalCreditCash = 0, totalCreditBank = 0;

            lbl_cash_cr_total.Text = totalCreditCash.ToString("0.00");
            lbl_bank_cr_total.Text = totalCreditBank.ToString("0.00");
            lbl_cash_dr_total.Text = totalDebitCash.ToString("0.00");
            lbl_bank_dr_total.Text = totalDebitBank.ToString("0.00");




            lbl_class22.Text = "From Date:-" + txt_from_date.Text + " To Date:-" + txt_to_date.Text;
            string sdate = txt_from_date.Text;
            string sday = sdate.Substring(0, 2);
            string smonth = sdate.Substring(3, 2);
            string syear = sdate.Substring(6, 4);

            string edate = txt_to_date.Text;
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
              
                string query = @"SELECT 
    SUM(CASE WHEN ld.Debit > 0 AND ld.Account_id = 'cash' THEN ld.Debit ELSE 0 END) AS Debit_CASH,
    SUM(CASE WHEN ld.Debit > 0 AND ld.Account_id != 'cash' THEN ld.Debit ELSE 0 END) AS Debit_BANK,
    SUM(CASE WHEN ld.Credit > 0 AND ld.Account_id = 'cash' THEN ld.Credit ELSE 0 END) AS Credit_CASH,
    SUM(CASE WHEN ld.Credit > 0 AND ld.Account_id != 'cash' THEN ld.Credit ELSE 0 END) AS Credit_BANK,
    (select top 1 studentname from admission_registor where admissionserialnumber=ld.Alternet_Account  ) as studentname,
    ld.Alternet_Account
FROM
    Account_Voucher_Details ld
JOIN
    Account_Ledger_Details ad ON ad.Account_id = ld.Alternet_Account AND ld.firm = ad.firm
WHERE
    ld.firm = 1
    AND ld.idate >= " + idate + @"
    AND ld.idate <= " + idate2 + @"
    AND ad.Group_id = '26'
GROUP BY
     ld.Alternet_Account
HAVING
    (
        SUM(CASE WHEN ld.Debit > 0 AND ld.Account_id = 'cash' THEN ld.Debit ELSE 0 END) +
        SUM(CASE WHEN ld.Debit > 0 AND ld.Account_id != 'cash' THEN ld.Debit ELSE 0 END) +
        SUM(CASE WHEN ld.Credit > 0 AND ld.Account_id = 'cash' THEN ld.Credit ELSE 0 END) +
        SUM(CASE WHEN ld.Credit > 0 AND ld.Account_id != 'cash' THEN ld.Credit ELSE 0 END)
    ) > 0";

                print1.Visible = false;
                ViewState["query"] = query;
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    btn_excels.Visible = false;
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    Panel1.Visible = true;
                    foreach (DataRow row in dt.Rows)
                    {
                        totalDebitCash += Convert.ToDouble(row["Debit_CASH"]);
                        totalDebitBank += Convert.ToDouble(row["Debit_BANK"]);
                        totalCreditCash += Convert.ToDouble(row["Credit_CASH"]);
                        totalCreditBank += Convert.ToDouble(row["Credit_BANK"]);
                    }
                    lbl_cash_cr_total.Text = totalCreditCash.ToString("0.00");
                    lbl_bank_cr_total.Text = totalCreditBank.ToString("0.00");
                    lbl_cash_dr_total.Text = totalDebitCash.ToString("0.00");
                    lbl_bank_dr_total.Text = totalDebitBank.ToString("0.00");

                    btn_excels.Visible = true;
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
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
        }


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                string excelname = My.with_excel_name("Student_Transaction_Summary");
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename="+ excelname + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        string style = @"<style>
                        table { border-collapse:collapse; }
                        td, th { border: 1px solid black; padding:5px; }
                     </style>";
                        Response.Write(style);
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
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        
         
    }
}