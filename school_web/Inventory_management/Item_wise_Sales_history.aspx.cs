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
    public partial class Item_wise_Sales_history : System.Web.UI.Page
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
                ViewState["Userid"] = Session["Admin"].ToString();
                if (!IsPostBack)
                {
                    try
                    {
                        mycode.bind_all_ddl_with_id(ddl_session_name, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session_name.SelectedValue = My.get_session_id();
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

        private void bind_datewise()
        {

            if(txt_from_Date.Text=="")
            {
                Alertme("Select start date", "warning");
            }
            else  if(txt_to_Date.Text=="")
            {
                Alertme("Select end date", "warning");
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
                    string query = "select im.Item_Name,bm.Brand_name,um.Unit,sd.Rate,sd.Quantity,sd.Total,format(sd.Date,'dd/MM/yyyy') as date, sdb.Bill_No as invoice_id, ar.studentname as sellto,sd.Sell_To,ar.class,ar.rollnumber from HMS_Invetory_item_Master im join HMS_Invetory_Brand_Master bm on im.Brand_id = bm.Brand_id join unit_master um on im.Unit_id=um.unit_id  join HMS_Invetory_Sell_details_item_wise sd on sd.Item_code=im.Item_id and sd.unit_id=im.Unit_id join HMS_Invetory_Sell_details_billwise sdb on sdb.unique_entry_id=sd.unique_entry_id join  admission_registor ar on ar.admissionserialnumber=sdb.party_id and ar.session=sdb.session where sd.Status='Saved' and format(sd.Date,'yyyyMMdd')>=" + idate + " and format(sd.Date,'yyyyMMdd')<=" + idate2 + " order by  ar.rollnumber,format(sd.Date,'yyyyMMdd')";
                    total_count_grid_list(query);
                }
            }
        }

        private void total_count_grid_list(string query)
        {
            print1.Visible = false;
            lnk_excel_download.Visible = false;
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
                print1.Visible = false;
                lnk_excel_download.Visible = false;

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
            string excelname = My.with_excel_name("Itemwise_sales_report");
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename="+ excelname + ".xls");
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

         

        protected void Btn_Find_Click1(object sender, EventArgs e)
        {
            bind_datewise();
        }

        protected void btn_sale_id_Click(object sender, EventArgs e)
        {
            if (txt_sealid.Text == "")
            {
                Alertme("Please enter student id", "warning");

            }
            else
            {
                //string query = "select im.Item_Name,bm.Brand_name,um.Unit,sd.Rate,sd.Quantity,sd.Total,format(sd.Date,'dd/MM/yyyy') as date,(select top 1 Bill_No from HMS_Invetory_Sell_details_billwise where unique_no=sd.unique_entry_id ) as invoice_id,(select top 1   party_name from party_details where party_id=sd.Sell_To) as sellto,sd.Sell_To from HMS_Invetory_item_Master im join HMS_Invetory_Brand_Master bm on im.Brand_id = bm.Brand_id join unit_master um on im.Unit_id=um.unit_id  join HMS_Invetory_Sell_details_item_wise sd on sd.Item_code=im.Item_id and sd.unit_id=im.Unit_id where sd.Status='Saved' and sd.Sell_To='" + txt_sealid.Text + "'  order by  format(sd.Date,'yyyyMMdd')";

                string query = "select im.Item_Name,bm.Brand_name,um.Unit,sd.Rate,sd.Quantity,sd.Total,format(sd.Date,'dd/MM/yyyy') as date, sdb.Bill_No as invoice_id, ar.studentname as sellto,sd.Sell_To,ar.class,ar.rollnumber from HMS_Invetory_item_Master im join HMS_Invetory_Brand_Master bm on im.Brand_id = bm.Brand_id join unit_master um on im.Unit_id=um.unit_id  join HMS_Invetory_Sell_details_item_wise sd on sd.Item_code=im.Item_id and sd.unit_id=im.Unit_id join HMS_Invetory_Sell_details_billwise sdb on sdb.unique_entry_id=sd.unique_entry_id join  admission_registor ar on ar.admissionserialnumber=sdb.party_id and ar.session=sdb.session where sd.Status='Saved' and sd.Sell_To='" + txt_sealid.Text + "' and sdb.session='"+ddl_session_name.SelectedItem.Text+"'  order by  ar.rollnumber,format(sd.Date,'yyyyMMdd')";



                total_count_grid_list(query);
            }
        }
    }
}