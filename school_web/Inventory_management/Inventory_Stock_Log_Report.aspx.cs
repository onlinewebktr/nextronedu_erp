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
    public partial class Inventory_Stock_Log_Report : System.Web.UI.Page
    {
        My mycode = new My();
        string scrpt;
        private void Alertme(string msg)
        {
            lbl_success.Text = msg;
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
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

                    txt_from_Date.Text = mycode.date();
                    txt_to_Date.Text = mycode.date();
                    find_button();
                }
            }
        }
        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            find_button();
        }
        private void find_button()
        {
            try
            {
                if (txt_from_Date.Text == "")
                {
                    Alertme("Please enter start date");
                }
                else if (txt_to_Date.Text == "")
                {
                    Alertme("Please enter end date ");
                }
                else
                {
                    string sdate = txt_from_Date.Text;
                    string sday = sdate.Substring(0, 2);
                    string smonth = sdate.Substring(3, 2);
                    string syear = sdate.Substring(6, 4);

                    string edate = txt_to_Date.Text;
                    string eday = edate.Substring(0, 2);
                    string emonth = edate.Substring(3, 2);
                    string eyear = edate.Substring(6, 4);

                    int idate = Convert.ToInt32(syear + smonth + sday);
                    int idate2 = Convert.ToInt32(eyear + emonth + eday);

                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.");
                    }
                    else
                    {
                        //string 
                        // string query = "Select im.Item_Name,um.Unit,ial.Old_Quantity,ial.Changed_Quantity,ial.New_Quantity,ial.Action_Type,ial.Changed_By,format(ial.Changed_On,'dd/MM/yyyy hh:mm:tt') as date_time,ial.Reason from Inventory_Stock_Log ial join HMS_INVETORY_ITEM_MASTER im on im.Item_id=ial.Item_Code  join unit_master um on im.Unit_id=um.unit_id  and format(ial.Changed_On,'yyyyMMdd')>=" + idate + " and format(ial.Changed_On,'yyyyMMdd')<=" + idate2 + " ";


                        string query = @" SELECT 
    im.Item_Name,
    um.Unit,

    CASE 
        WHEN ial.Old_Quantity = FLOOR(ial.Old_Quantity) 
            THEN CAST(CAST(ial.Old_Quantity AS INT) AS VARCHAR)
        ELSE CAST(ial.Old_Quantity AS VARCHAR)
    END AS Old_Quantity,

    CASE 
        WHEN ial.Changed_Quantity = FLOOR(ial.Changed_Quantity) 
            THEN CAST(CAST(ial.Changed_Quantity AS INT) AS VARCHAR)
        ELSE CAST(ial.Changed_Quantity AS VARCHAR)
    END AS Changed_Quantity,

    CASE 
        WHEN ial.New_Quantity = FLOOR(ial.New_Quantity) 
            THEN CAST(CAST(ial.New_Quantity AS INT) AS VARCHAR)
        ELSE CAST(ial.New_Quantity AS VARCHAR)
    END AS New_Quantity,

    ial.Action_Type,
    ial.Changed_By,
    FORMAT(ial.Changed_On,'dd/MM/yyyy hh:mm:tt') AS date_time,
    ial.Reason

FROM Inventory_Stock_Log ial
JOIN HMS_INVETORY_ITEM_MASTER im ON im.Item_id = ial.Item_Code  
JOIN unit_master um ON im.Unit_id = um.unit_id where  format(ial.Changed_On,'yyyyMMdd')>=" + idate + " and format(ial.Changed_On,'yyyyMMdd')<=" + idate2 + "";



                        Bind_grdi_data(query);

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }

        }

        private void Bind_grdi_data(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_heading.Text = "";
                Alertme("Sorry there are no data list exist");
                GrdView.DataSource = null;
                GrdView.DataBind();
                lnk_excel_download.Visible = false;
                print1.Visible = false;
            }
            else
            {
                lbl_heading.Text = "From Date" + txt_from_Date.Text + " End Date: " + txt_to_Date.Text;
                lnk_excel_download.Visible = true;
                print1.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                string excelname = My.with_excel_name("Stock_Reconciliation"); 
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
            string excelname = My.with_excel_name("Stock_Reconciliation");
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename="+ excelname + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GrdView.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        #endregion download_in_excel
    }
}