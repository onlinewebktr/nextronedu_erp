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

namespace school_web.Admin
{
    public partial class Hostel_Wise_Payment_Report : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
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
                    Session["reprintadmission"] = "5";
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["firm_id"] = Session["firm"].ToString();
                    txt_s_date.Text = mycode.sevendaysback();
                    txt_e_date.Text = mycode.date();

                    ViewState["flag"] = "1";
                    // find_all();

                    find_firm_details();
                    string pagename_current = "fee-report.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];
                    mycode.bind_all_ddl_with_id_All_New(ddl_hostel_name, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                    ddl_hostel_name.SelectedValue = My.get_top_one_hostel_id();
                    find_by_date();
                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            find_by_date();

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


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
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
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {




            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    string type = ((Label)e.Item.FindControl("lbl_type")).Text;

                    if (type == "Monthly")
                    {
                        ((Panel)e.Item.FindControl("pnl_month")).Visible = true;
                        ((Panel)e.Item.FindControl("pnl_Admission")).Visible = false;
                        ((Panel)e.Item.FindControl("pnl_annual")).Visible = false;
                    }
                    else if (type == "Admission")
                    {
                        ((Panel)e.Item.FindControl("pnl_Admission")).Visible = true;
                        ((Panel)e.Item.FindControl("pnl_month")).Visible = false;
                        ((Panel)e.Item.FindControl("pnl_annual")).Visible = false;
                    }

                    else if (type == "Annual")
                    {
                        ((Panel)e.Item.FindControl("pnl_Admission")).Visible = false;
                        ((Panel)e.Item.FindControl("pnl_month")).Visible = false;
                        ((Panel)e.Item.FindControl("pnl_annual")).Visible = true;
                    }


                }
                else
                {
                    ((Panel)e.Item.FindControl("pnl_month")).Visible = false;
                    ((Panel)e.Item.FindControl("pnl_Admission")).Visible = false;
                    ((Panel)e.Item.FindControl("pnl_annual")).Visible = false;

                }


                string value1 = ((Label)e.Item.FindControl("lbl_Amount1")).Text;

                decimal value;
                if (decimal.TryParse(value1, out value))
                {
                    ((Label)e.Item.FindControl("lbl_Amount1")).Text = value.ToString("0.00");
                }
            }
        }

        private void find_by_date()
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
                final_find_report_by_date(idate, idate2);
            }
        }

        private void final_find_report_by_date(int idate, int idate2)
        {
            if (ddl_hostel_name.SelectedItem.Text == "ALL")
            {
                if(ddl_fee_type.Text=="ALL")
                {
                    bind_grd_view(" select (Select top 1 Hostel_name from Hostels_master where Hostel_id=t2.Hostel_id) as Hostel_name,t1.Type,t2.studentname,t2.father_mob,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,(isnull(convert(float, t1.Amount),0)) as Amount1 from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.parameter_New in ('HostelAdmissionFee','HostelAnnualFee','HostelMonthlyFee')  and  t2.hosteltaken='Yes'  and  t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' order by t1.Idate desc");
                }
                else
                {
                    bind_grd_view(" select  (Select top 1 Hostel_name from Hostels_master where Hostel_id=t2.Hostel_id) as Hostel_name,t1.Type,t2.studentname,t2.father_mob,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,(isnull(convert(float, t1.Amount),0)) as Amount1 from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.parameter_New in ('HostelAdmissionFee','HostelAnnualFee','HostelMonthlyFee')  and  t2.hosteltaken='Yes'  and  t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.parameter_New='"+ddl_fee_type.Text+ "'  order by t1.Idate desc");
                }
               
            }
            else
            {
                if (ddl_fee_type.Text == "ALL")
                {
                    bind_grd_view(" select  (Select top 1 Hostel_name from Hostels_master where Hostel_id=t2.Hostel_id) as Hostel_name,t1.Type,t2.studentname,t2.father_mob,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,(isnull(convert(float, t1.Amount),0)) as Amount1 from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where  t1.parameter_New in ('HostelAdmissionFee','HostelAnnualFee','HostelMonthlyFee') and  t2.hosteltaken='Yes' and t2.Hostel_id=" + ddl_hostel_name.SelectedValue + " and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' order by t1.Idate desc");
                }
                else
                {
                    bind_grd_view(" select  (Select top 1 Hostel_name from Hostels_master where Hostel_id=t2.Hostel_id) as Hostel_name,t1.Type,t2.studentname,t2.father_mob,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,(isnull(convert(float, t1.Amount),0)) as Amount1 from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where  t1.parameter_New in ('HostelAdmissionFee','HostelAnnualFee','HostelMonthlyFee') and  t2.hosteltaken='Yes' and t2.Hostel_id=" + ddl_hostel_name.SelectedValue + " and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.parameter_New='" + ddl_fee_type.Text + "' order by t1.Idate desc");
                }
            }


        }

        private void bind_grd_view(string qry)
        {
            btn_excels.Visible = false;
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                lbl_class22.Text = "";
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                lbl_fnl_paid.Text = "0.00";
            }
            else
            {
                String Fnl_paid_amt = Convert.ToDouble(dt.Compute("SUM(Amount1)", string.Empty)).ToString("0.00");


                lbl_fnl_paid.Text = Fnl_paid_amt;

                lbl_class22.Text = "Hostel Name" + ddl_hostel_name.SelectedItem.Text +" Form : " + txt_s_date.Text + " To : " + txt_e_date.Text+" Fee Type: "+ ddl_fee_type.Text;
                rd_view.DataSource = dt;
                rd_view.DataBind();

                btn_excels.Visible = true;

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }

                string qryS = ""; string qrySS = "";

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


                if (ddl_hostel_name.SelectedItem.Text == "ALL")
                {
                    if(ddl_fee_type.Text=="ALL")
                    {
                        qrySS = " select sum(isnull(convert(float, sp.Amount),0)) as Paid_amt,mode from Student_Payment_History sp  where sp.parameter_New in ('HostelAdmissionFee','HostelAnnualFee','HostelMonthlyFee')    and sp.Idate>='" + idate + "' and sp.Idate<='" + idate2 + "' and sp.Slip_no in (select distinct slipno from dbo.[Monthly_Fee_Collection_Slip] where parameter in('HostelAdmissionFee','HostelAnnualFee','HostelMonthlyFee')) group by sp.mode";
                    }
                    else
                    {
                        qrySS = " select sum(isnull(convert(float, sp.Amount),0)) as Paid_amt,mode from Student_Payment_History sp  where sp.parameter_New ='"+ddl_fee_type.Text+"'   and sp.Idate>='" + idate + "' and sp.Idate<='" + idate2 + "' and sp.parameter_New = '" + ddl_fee_type.Text + "'  group by sp.mode";
                        
                    }
                   
                }
                else
                {


                    if (ddl_fee_type.Text == "ALL")
                    {
                        qrySS = " select sum(isnull(convert(float, sp.Amount),0)) as Paid_amt,mode from Student_Payment_History sp   where sp.parameter_New in ('HostelAdmissionFee','HostelAnnualFee','HostelMonthlyFee')    and sp.Idate>='" + idate + "' and sp.Idate<='" + idate2 + "' and sp.Slip_no in (select distinct slipno from dbo.[Monthly_Fee_Collection_Slip] where parameter in('HostelAdmissionFee','HostelAnnualFee','HostelMonthlyFee') and Hostel_id="+ddl_hostel_name.SelectedValue+") group by sp.mode";
                    }
                    else
                    {
                        qrySS = " select sum(isnull(convert(float, sp.Amount),0)) as Paid_amt,mode from Student_Payment_History sp    where sp.parameter_New ='" + ddl_fee_type.Text + "'   and sp.Idate>='" + idate + "' and sp.Idate<='" + idate2 + "' and sp.parameter_New = '" + ddl_fee_type.Text + "' and sp.Slip_no in (select distinct slipno from dbo.[Monthly_Fee_Collection_Slip] where    Hostel_id=" + ddl_hostel_name.SelectedValue + ") group by sp.mode";

                    }

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
                    lbl_pos.Text = "00";
                    lbl_online.Text = "00";
                }
                else
                {
                    lbl_online.Text = "00";
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
                    lbl_pos.Text = "00";
                    for (int i = 0; i < dtSS.Rows.Count; i++)
                    {
                        if (dtSS.Rows[i]["mode"].ToString() == "Cash")
                        {
                            lbl_by_cash.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Netbanking")
                        {
                            lbl_by_netbanking.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Deposited In Bank")
                        {
                            lbl_by_deposit.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Sbdebit")
                        {
                            lbl_by_sb.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Cheque")
                        {
                            lbl_by_chequeS.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[0]["mode"].ToString() == "NEFT")
                        {
                            lbl_by_neft.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Debitcard")
                        {
                            lbl_by_debitcard.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Creditcard")
                        {
                            lbl_by_credit_card.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Otherdcard")
                        {
                            lbl_by_other_card.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString().ToUpper() == "UPI")
                        {
                            lbl_by_upi.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Branch")
                        {
                            lbl_by_branch.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString().ToLower() == "pos")
                        {
                            lbl_pos.Text = My.toDouble((dtSS.Rows[i]["Pos"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Online")
                        {
                            lbl_online.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");

                        }
                    }


                }
            }
        }

       
    }
}