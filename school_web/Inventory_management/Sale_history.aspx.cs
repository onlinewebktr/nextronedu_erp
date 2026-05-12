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
    public partial class Sale_history : System.Web.UI.Page
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
                        Session["billfrom"] = "2";
                        txt_from_Date.Text = my.date();
                        txt_to_Date.Text = my.date();
                        bind_datewise();


                        // bind_all_data();
                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }

        }
        My my = new My();
        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            bind_datewise();
        }
        private void bind_datewise()
        {
            string query = "";


            int idate = My.DateConvertToIdate(txt_from_Date.Text);
            int idate2 = My.DateConvertToIdate(txt_to_Date.Text);

         

            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {

                if (ViewState["firm_id_N"].ToString() == "NNI-01")
                {
                    if (My.toint(mycode.idate()) >= 20260510)
                    {

                          query = "Select sd.*,pt.Total_Paid_Amount,pt.Duse_Amount,'0' as rollnumber,(SELECT TOP 1 Course_Name FROM Add_course_table WHERE course_id=ar.class_name) AS class,ar.Section,ar.party_name as studentname from HMS_Invetory_Sell_details_billwise sd join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=sd.party_id and pt.Payment_Vochar_id=sd.Bill_No join party_details ar on  pt.party_id=ar.party_id  where sd.Idate>=" + idate + " and sd.Idate<=" + idate2 + " order by ar.Section,sd.Idate asc ";
                    }
                }
                else
                {
                      query = "Select sd.*,pt.Total_Paid_Amount,pt.Duse_Amount,ar.rollnumber,ar.class,ar.Section,ar.studentname from HMS_Invetory_Sell_details_billwise sd join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=sd.party_id and pt.Payment_Vochar_id=sd.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber  where sd.Idate>=" + idate + " and sd.Idate<=" + idate2 + " and   ar.Transfer_Status in ('NT','New') order by ar.class,ar.Section,ar.rollnumber,sd.Idate asc ";

                }

                    total_count_grid_list(query);
            }
        }

        private void total_count_grid_list(string query)
        {
            print1.Visible = false;

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                Alertme("Sorry there are no data list exist", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
                print1.Visible = true;

            }
        }

        protected void btn_sale_id_Click(object sender, EventArgs e)
        {
            string query = "";
            if (txt_sealid.Text=="")
            {
                Alertme("Please enter student id", "warning");
            }
            else
            {
                if (ViewState["firm_id_N"].ToString() == "NNI-01")
                {
                    if (My.toint(mycode.idate()) >= 20260510)
                    {

                        query = "Select sd.*,pt.Total_Paid_Amount,pt.Duse_Amount,'0' as rollnumber,(SELECT TOP 1 Course_Name FROM Add_course_table WHERE course_id=ar.class_name) AS class,ar.Section,ar.party_name as studentname from HMS_Invetory_Sell_details_billwise sd join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=sd.party_id and pt.Payment_Vochar_id=sd.Bill_No join party_details ar on  pt.party_id=ar.party_id  where sd.party_id='" + txt_sealid.Text + "' order by  sd.Idate asc ";
                    }
                    else
                    {
                        query = "Select sd.*,pt.Total_Paid_Amount,pt.Duse_Amount,ar.rollnumber,ar.class,ar.Section,ar.studentname from HMS_Invetory_Sell_details_billwise sd join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=sd.party_id and pt.Payment_Vochar_id=sd.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where sd.party_id='" + txt_sealid.Text + "' and ar.Transfer_Status in ('NT','New') ";
                    }
                }
                else
                {
                      query = "Select sd.*,pt.Total_Paid_Amount,pt.Duse_Amount,ar.rollnumber,ar.class,ar.Section,ar.studentname from HMS_Invetory_Sell_details_billwise sd join HMS_Inventory_Bill_Payment_Tracking pt on pt.party_id=sd.party_id and pt.Payment_Vochar_id=sd.Bill_No join admission_registor ar on  pt.party_id=ar.admissionserialnumber where sd.party_id='" + txt_sealid.Text + "' and ar.Transfer_Status in ('NT','New') ";
                    
                }

                total_count_grid_list(query);
            }
           
        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    pnl_grid.RenderControl(hw);
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


        protected void lnk_excel_download1_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                pnl_grid.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        #endregion download_in_excel
    }
}