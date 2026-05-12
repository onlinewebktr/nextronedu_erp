using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Student_Wise_Sale_Return_Report : System.Web.UI.Page
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
        string studentname = " select top 1 studentname from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and Session_id=isdb.Session_id";

        string classname = "select top 1 class from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and Session_id=isdb.Session_id";


        string rollnumber = "select top 1 rollnumber from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and Session_id=isdb.Session_id";

        string Section = "select top 1 Section from dbo.[admission_registor] where admissionserialnumber=isdb.party_id and Session_id=isdb.Session_id";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('logout.aspx','_parent',replace=true);</script>");
            }
            else
            {


                ViewState["firm_id_N"] = My.get_firm_id();
                ViewState["Userid"] = Session["Admin"].ToString();
                if (!IsPostBack)
                {
                    try
                    {
                        mycode.bind_all_ddl_with_id(ddl_seesion, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_seesion.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_classname, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where   Session_id='" + ddl_seesion.SelectedValue + "'");

                        
                        Session["billfromre"] = "2";
                        txt_from_Date.Text = mycode.date();
                        txt_to_Date.Text = mycode.date();
                        bind_datewise();

                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }


        }
        protected void ddl_classname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_classname.SelectedItem.Text == "ALL")
                {
                    mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor");
                }
                else
                {
                    mycode.bind_ddlall(ddl_section, "Select distinct Section from  admission_registor where Class_id='" + ddl_classname.SelectedValue + "' and Session_id='" + ddl_seesion.SelectedValue + "'");
                }
            }
            catch
            {


            }
         
        }


        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            bind_datewise();
        }
        private void bind_datewise()
        {
            try
            {
                string query = "";
                int idate = My.DateConvertToIdate(txt_from_Date.Text);
                int idate2 = My.DateConvertToIdate(txt_to_Date.Text);
                if (idate > idate2)
                {
                    lbl_heading.Text = "";
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    if (ViewState["firm_id_N"].ToString() == "NNI-01")
                    {
                        if (My.toint(mycode.idate()) >= 20260510)
                        {
                            query = @"Select *,(SELECT TOP 1 party_name FROM party_details WHERE party_id=isdb.User_id) as studentname,(SELECT TOP 1 ac.Course_Name
 FROM HMS_INVETORY_SALE_RETUREN_BILL_WISE h
 INNER JOIN party_details p
     ON h.party_id = p.party_id
 INNER JOIN Add_course_table ac
     ON ac.course_id = p.class_name
 WHERE h.party_id = isdb.party_id
) AS classname ,'0' as rollnumber,(isnull(convert(float, isdb.Pay_Amount),0)) as NetPayable1,format(isdb.Returen_date_time, 'dd/MM/yyyy') as Date1,(SELECT TOP 1 Section FROM party_details WHERE party_id=isdb.User_id) as Section    from HMS_Invetory_Sale_Returen_Bill_wise isdb   where format(isdb.Returen_date_time, 'yyyyMMdd')>=" + idate + " and format(isdb.Returen_date_time, 'yyyyMMdd')<=" + idate2 + " order by cast(format(isdb.Returen_date_time, 'yyyyMMdd') as int) asc ";
                        }
                    }
                    else
                    {
                        query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, isdb.Pay_Amount),0)) as NetPayable1,format(isdb.Returen_date_time, 'dd/MM/yyyy') as Date1,(" + Section + ") as Section    from HMS_Invetory_Sale_Returen_Bill_wise isdb   where format(isdb.Returen_date_time, 'yyyyMMdd')>=" + idate + " and format(isdb.Returen_date_time, 'yyyyMMdd')<=" + idate2 + " order by cast(format(isdb.Returen_date_time, 'yyyyMMdd') as int) asc ";
                    }
                    string query1 = "Select sum(isnull(convert(float, isdb.Pay_Amount),0)) as Paid_amt,isdb.Pay_Mode as mode from HMS_Invetory_Sale_Returen_Bill_wise isdb  where format(isdb.Returen_date_time, 'yyyyMMdd')>=" + idate + " and format(isdb.Returen_date_time, 'yyyyMMdd')<=" + idate2 + " group by isdb.Pay_Mode ";
                    total_count_grid_list(query, query1);
                    lbl_heading.Text = "From Date" + txt_from_Date.Text + " End Date: " + txt_to_Date.Text;
                }
            }
            catch(Exception ex)
            {
                My.submitexception(ex.ToString());
            }

        }

        private void total_count_grid_list(string query, string qrySS)
        {
            Session["query"] = query;
            Session["qrySS"] = qrySS;
            print1.Visible = false;

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_by_cash.Text = "0.00";
                lbl_fnl_paid.Text = "0.00";

                Alertme("Sorry there are no data list exist", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                String Fnl_paid_amt = Convert.ToDouble(dt.Compute("SUM(NetPayable1)", string.Empty)).ToString("0.00");
                lbl_fnl_paid.Text = Fnl_paid_amt;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
                print1.Visible = true;
                DataTable dtSS = mycode.FillData(qrySS);
                if (dtSS.Rows.Count == 0)
                {
                    lbl_by_cash.Text = "00";
                    lbl_by_netbanking.Text = "00";
                    lbl_upi.Text = "00";
                    lbl_Wallet.Text = "00";
                }
                else
                {
                    lbl_by_cash.Text = "00";
                    lbl_by_netbanking.Text = "00";
                    lbl_upi.Text = "00";
                    lbl_Wallet.Text = "00";

                    for (int i = 0; i < dtSS.Rows.Count; i++)
                    {

                        if (dtSS.Rows[i]["mode"].ToString() == "Netbnaking")
                        {
                            lbl_by_netbanking.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "UPI")
                        {
                            lbl_upi.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Cash")
                        {
                            lbl_by_cash.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Wallet")
                        {
                            lbl_Wallet.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }

                    }


                }



            }
        }
        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Sales_Return_Report_" + mycode.date() + "_" + mycode.time() + ".xls");

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    pnl_grid.RenderControl(hw);
                    string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


     




        #endregion download_in_excel
        protected void btn_sale_id_Click(object sender, EventArgs e)
        {
            if(ddl_seesion.SelectedItem.Text=="Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                string query1 = "";
                string query = "";
                if (ddl_classname.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
                {
                    query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, isdb.Pay_Amount),0)) as NetPayable1,format(isdb.Returen_date_time, 'dd/MM/yyyy') as Date1 ,ar.Section   from HMS_Invetory_Sale_Returen_Bill_wise isdb  join admission_registor ar on  isdb.Party_id=ar.admissionserialnumber where ar.Session_id='" + ddl_seesion.SelectedValue + "'  order by cast(format(isdb.Returen_date_time, 'yyyyMMdd') as int) asc ";

                    query1 = "Select sum(isnull(convert(float, isdb.Pay_Amount),0)) as Paid_amt,isdb.Pay_Mode as mode from HMS_Invetory_Sale_Returen_Bill_wise isdb  join admission_registor ar on  isdb.party_id=ar.admissionserialnumber where ar.Session_id='" + ddl_seesion.SelectedValue + "'  group by isdb.Pay_Mode ";
                }
                else if (ddl_classname.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
                {
                    query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, isdb.Pay_Amount),0)) as NetPayable1,format(isdb.Returen_date_time, 'dd/MM/yyyy') as Date1,ar.Section    from HMS_Invetory_Sale_Returen_Bill_wise isdb  join admission_registor ar on  isdb.Party_id=ar.admissionserialnumber where ar.Session_id='" + ddl_seesion.SelectedValue + "' and ar.Class_id='" + ddl_classname.SelectedValue + "' and ar.Section='" + ddl_section.Text + "' order by cast(format(isdb.Returen_date_time, 'yyyyMMdd') as int),ar.rollnumber ";

                    query1 = "Select sum(isnull(convert(float, isdb.Pay_Amount),0)) as Paid_amt,isdb.Pay_Mode as mode from HMS_Invetory_Sale_Returen_Bill_wise isdb  join admission_registor ar on  isdb.party_id=ar.admissionserialnumber  where ar.Session_id='" + ddl_seesion.SelectedValue + "' and ar.Class_id='" + ddl_classname.SelectedValue + "' and ar.Section='" + ddl_section.Text + "' group by isdb.Pay_Mode ";

                }
                else if (ddl_classname.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
                {
                    query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, isdb.Pay_Amount),0)) as NetPayable1,format(isdb.Returen_date_time, 'dd/MM/yyyy') as Date1,ar.Section    from HMS_Invetory_Sale_Returen_Bill_wise isdb  join admission_registor ar on  isdb.Party_id=ar.admissionserialnumber where ar.Session_id='" + ddl_seesion.SelectedValue + "' and ar.Class_id='" + ddl_classname.SelectedValue + "'  order by cast(format(isdb.Returen_date_time, 'yyyyMMdd') as int),ar.rollnumber ";
                
                    query1 = "Select sum(isnull(convert(float, isdb.Pay_Amount),0)) as Paid_amt,isdb.Pay_Mode as mode from HMS_Invetory_Sale_Returen_Bill_wise isdb  join admission_registor ar on  isdb.party_id=ar.admissionserialnumber  where ar.Session_id='" + ddl_seesion.SelectedValue + "' and ar.Class_id='" + ddl_classname.SelectedValue + "'  group by isdb.Pay_Mode";
                }
                else if (ddl_classname.SelectedItem.Text == "ALL" && ddl_section.Text != "ALL")
                {
                    query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, isdb.Pay_Amount),0)) as NetPayable1,format(isdb.Returen_date_time, 'dd/MM/yyyy') as Date1,ar.Section    from HMS_Invetory_Sale_Returen_Bill_wise isdb  join admission_registor ar on  isdb.Party_id=ar.admissionserialnumber where ar.Session_id='" + ddl_seesion.SelectedValue + "'   and ar.Section='" + ddl_section.Text + "' order by cast(format(isdb.Returen_date_time, 'yyyyMMdd') as int),ar.rollnumber ";

                    query1 = "Select sum(isnull(convert(float, isdb.Pay_Amount),0)) as Paid_amt,isdb.Pay_Mode as mode from HMS_Invetory_Sale_Returen_Bill_wise isdb  join admission_registor ar on  isdb.party_id=ar.admissionserialnumber  where ar.Session_id='" + ddl_seesion.SelectedValue + "'   and ar.Section='" + ddl_section.Text + "' group by isdb.Pay_Mode ";
                }
                else
                {

                    query = "Select *,(" + studentname + ") as studentname,(" + classname + ") as classname,(" + rollnumber + ") as rollnumber,(isnull(convert(float, isdb.Pay_Amount),0)) as NetPayable1,format(isdb.Returen_date_time, 'dd/MM/yyyy') as Date1,ar.Section    from HMS_Invetory_Sale_Returen_Bill_wise isdb  join admission_registor ar on  isdb.Party_id=ar.admissionserialnumber where ar.Session_id='" + ddl_seesion.SelectedValue + "' order by cast(format(isdb.Returen_date_time, 'yyyyMMdd') as int) asc ";

                    query1 = "Select sum(isnull(convert(float, isdb.Pay_Amount),0)) as Paid_amt,isdb.Pay_Mode as mode from HMS_Invetory_Sale_Returen_Bill_wise isdb  join admission_registor ar on  isdb.party_id=ar.admissionserialnumberwhere ar.Session_id='" + ddl_seesion.SelectedValue + "'  group by isdb.Pay_Mode ";




                }
                total_count_grid_list(query, query1);
                lbl_heading.Text = "Class :" + ddl_classname.SelectedItem.Text + "  Section : " + ddl_section.Text;

            }
           
        }
    }
}