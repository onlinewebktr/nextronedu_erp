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
    public partial class Online_Order_Delivered : System.Web.UI.Page
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

                if (!IsPostBack)
                {
                    try
                    {
                        Session["billfrom"] = "6";
                        ViewState["Userid"] = Session["Admin"].ToString();
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
        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            bind_datewise();
        }
        private void bind_datewise()
        {
            if (txt_from_Date.Text == "")
            {
                Alertme("Please enter star date", "warning");
            }
            else if (txt_to_Date.Text == "")
            {
                Alertme("Please enter end date", "warning");
            }
            else
            {
                int idate = My.DateConvertToIdate(txt_from_Date.Text);
                int idate2 = My.DateConvertToIdate(txt_to_Date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    string query = "Select osb.*,format(Date,'dd/MM/yyyy') as Date1,format(Process_date,'dd/MM/yyyy') as Date2,(Select top 1 unique_no from HMS_INVETORY_SELL_DETAILS_BILLWISE where party_id=osb.user_id and Bill_No=osb.New_bill_no) as unique_no,ar.studentname,ar.rollnumber,ar.class,ar.Section from Online_Sell_billwise osb join admission_registor ar on osb.user_id=ar.admissionserialnumber and osb.SesssionId=ar.Session_id  where   format(osb.Process_date,'yyyyMMdd')>=" + idate + " and  format(osb.Process_date,'yyyyMMdd')<=" + idate2 + " and osb.Order_Status='Delivered' and osb.Payment_Status='Paid' order by ar.class,ar.Section,ar.rollnumber,format(osb.Process_date,'yyyyMMdd') asc ";
                    bind_grid(query);
                }

            }
        }
        protected void btn_sale_id_Click(object sender, EventArgs e)
        {
            if (txt_sealid.Text == "")
            {
                Alertme("Please enter student admission no.", "warning");
            }
            else
            {
                string query = "Select osb.*,format(Date,'dd/MM/yyyy') as Date1,format(Process_date,'dd/MM/yyyy') as Date2,(Select top 1 unique_no from HMS_INVETORY_SELL_DETAILS_BILLWISE where party_id=osb.user_id and Bill_No=osb.New_bill_no) as unique_no,ar.studentname,ar.rollnumber,ar.class,ar.Section from Online_Sell_billwise osb join admission_registor ar on osb.user_id=ar.admissionserialnumber and osb.SesssionId=ar.Session_id   where  osb.user_id='" + txt_sealid.Text.Trim() + "' and osb.Order_Status='Delivered' and osb.Payment_Status='Paid' order by ar.class,ar.Section,ar.rollnumber,format(osb.Process_date,'yyyyMMdd') asc ";
                bind_grid(query);
            }
        }
        private void bind_grid(string query)
        {
            Session["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                Alertme("Sorry, there are no records available", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
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
            string excelnam = My.with_excel_name("OnlineOrderDeliverd");
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename="+ excelnam + ".xls");
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
        #endregion download_in_excelpnl_grid
    }
}