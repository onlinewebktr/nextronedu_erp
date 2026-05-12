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
    public partial class Advance_amount_report : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        Session["pageadv"] = "1";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        Session["reprint_otherfee"] = "1";
                        ViewState["branchid"] = Session["branchid"].ToString();
                        ViewState["Sessionid"] = My.get_session_id();
                        txt_s_date.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date();
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        find_firm_details();
                        Bind_data_pageload_date_wise();

                    }


                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Advance_amount_report");
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

        private void Bind_data_pageload_date_wise()
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
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
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

                    int idate1 = Convert.ToInt32(syear + smonth + sday);
                    int idate21 = Convert.ToInt32(eyear + emonth + eday);

                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.", "warning");
                    }
                    else
                    {
                        final_find_report_by_date(idate1, idate21);
                    }
                }
            }
        }
        private void final_find_report_by_date(int idate1, int idate21)
        {
            string qrySS = "select sum(isnull(convert(float, Wallet_input_amount),0)) as Paid_amt,Mode  from STUDENT_WALLET where      format(Date_of_entry,'yyyyMMdd')>='" + idate1 + "' and  format(Date_of_entry,'yyyyMMdd')<='" + idate21 + "' and Add_type='Advance Pay' group by Payment_mode";

            bind_grd_view("select t1.Id,t2.rollnumber, t2.mobilenumber,t2.fathername, t2.studentname,t1.Adm_no as Admission_no,t2.class,t2.session,format(t1.Date_of_entry,'dd/MM/yyyy') as Payment_date,t1.slipno as Slipid,t1.Mode as Payment_mode,t2.Session_id,t1.Wallet_input_amount as Content_Fee,t1.Add_type as Content_Name from STUDENT_WALLET t1 join admission_registor t2 on  t1.Adm_no=t2.admissionserialnumber   where  t2.Transfer_Status in ('NT','New')  and format(t1.Date_of_entry,'yyyyMMdd')>='" + idate1 + "' and format(t1.Date_of_entry,'yyyyMMdd')<='" + idate21 + "'  and t1.Add_type='Advance Pay'  order by  format(t1.Date_of_entry,'yyyyMMdd') desc", qrySS);
        }
        private void bind_grd_view(string query, string qrySS)
        {
            lbl_class22.Text = "From Date:- " + txt_s_date.Text + " To:- " + txt_e_date.Text;
            lbl_by_cash.Text = "00";
            lbl_by_netbanking.Text = "00";
            lbl_by_deposit.Text = "00";
            lbl_by_sb.Text = "00";
            lbl_by_chequeS.Text = "00";
            lbl_by_neft.Text = "00";
            lbl_by_debitcard.Text = "00";
            lbl_by_credit_card.Text = "00";
            lbl_by_other_card.Text = "00";
            lbl_by_upi.Text = "00";
            lbl_by_branch.Text = "00";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                print1.Visible = false;
                btn_excels.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                grd_fee.DataSource = null;
                grd_fee.DataBind();

            }
            else
            {
                print1.Visible = true;
                btn_excels.Visible = true;
                double total = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total = total + My.toDouble(dt.Rows[i]["Content_Fee"].ToString());
                }
                //String Fnl_paid_amt = Convert.ToDouble(dt.Compute("SUM(Amount)", string.Empty)).ToString();

                grd_fee.DataSource = dt;
                grd_fee.DataBind();


                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }


                DataTable dtSS = mycode.FillData(qrySS);
                if (dtSS.Rows.Count == 0)
                {
                    lbl_by_cash.Text = "00";
                    lbl_by_netbanking.Text = "00";
                    lbl_by_deposit.Text = "00";
                    lbl_by_sb.Text = "00";
                    lbl_by_chequeS.Text = "00";
                    lbl_by_neft.Text = "00";
                    lbl_by_debitcard.Text = "00";
                    lbl_by_credit_card.Text = "00";
                    lbl_by_other_card.Text = "00";
                    lbl_by_upi.Text = "00";
                    lbl_by_branch.Text = "00";
                }
                else
                {
                    for (int j = 0; j < dtSS.Rows.Count; j++)
                    {
                        if (dtSS.Rows[j]["mode"].ToString() == "Cash")
                        {
                            double Total1 = My.toDouble(lbl_by_cash.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_cash.Text = Total1.ToString("0.00");
                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "Netbanking")
                        {
                            double Total1 = My.toDouble(lbl_by_netbanking.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_netbanking.Text = Total1.ToString("0.00");


                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "Deposited In Bank")
                        {
                            double Total1 = My.toDouble(lbl_by_deposit.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_deposit.Text = Total1.ToString("0.00");


                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "Sbdebit")
                        {
                            double Total1 = My.toDouble(lbl_by_sb.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_sb.Text = Total1.ToString("0.00");


                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "Cheque")
                        {
                            double Total1 = My.toDouble(lbl_by_chequeS.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_chequeS.Text = Total1.ToString("0.00");


                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "NEFT")
                        {
                            double Total1 = My.toDouble(lbl_by_neft.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_neft.Text = Total1.ToString("0.00");


                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "Debitcard")
                        {
                            double Total1 = My.toDouble(lbl_by_debitcard.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_debitcard.Text = Total1.ToString("0.00");


                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "Creditcard")
                        {
                            double Total1 = My.toDouble(lbl_by_credit_card.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_credit_card.Text = Total1.ToString("0.00");



                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "Otherdcard")
                        {
                            double Total1 = My.toDouble(lbl_by_other_card.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_other_card.Text = Total1.ToString("0.00");


                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "UPI")
                        {
                            double Total1 = My.toDouble(lbl_by_upi.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_upi.Text = Total1.ToString("0.00");


                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "Branch")
                        {
                            double Total1 = My.toDouble(lbl_by_branch.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_by_branch.Text = Total1.ToString("0.00");


                        }
                        if (dtSS.Rows[j]["mode"].ToString() == "Pos")
                        {
                            double Total1 = My.toDouble(lbl_posh.Text) + My.toDouble(dtSS.Rows[j]["Paid_amt"].ToString());
                            lbl_posh.Text = Total1.ToString("0.00");


                        }


                       
                    }
                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data_pageload_date_wise();
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                string excelname=My.with_excel_name("Advance_amount");
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
                        grd_fee.RenderControl(hw);
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
        double total = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Paid_amount = (Label)e.Row.FindControl("lbl_Paid_amount");


                if (lbl_Paid_amount.Text != "")
                {
                    total = total + Convert.ToDouble(lbl_Paid_amount.Text);
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalamount = (Label)e.Row.FindControl("lbl_totalamount");


                lbl_totalamount.Text = total.ToString("0.00");

            }
        }
    }
}