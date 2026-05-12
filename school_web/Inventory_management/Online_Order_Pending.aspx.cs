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

namespace school_web.Inventory_management
{
    public partial class Online_Order_Pending : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        txt_from_Date.Text = mycode.date();
                        txt_to_Date.Text = mycode.date();
                        bind_datewise();


                        try
                        {
                            string order = Request.QueryString["order"];

                            if (!string.IsNullOrEmpty(order))
                            {
                                bind_all();
                            }
                        }
                        catch
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }

        private void bind_all()
        {
            string query = "Select osb.*,format(Date,'dd/MM/yyyy') as Date1,ar.studentname,ar.rollnumber,ar.class,ar.Section   from Online_Sell_billwise osb join admission_registor ar on osb.user_id=ar.admissionserialnumber and osb.SesssionId=ar.Session_id where  osb.Order_Status='Pending' and osb.Payment_Status='Paid' order by ar.class,ar.Section,ar.rollnumber, osb.Idate asc ";
            bind_grid(query);

           // (select top 1 studentname from admission_registor where admissionserialnumber = osb.user_id and admissionserialnumber.Session_id = osb.SesssionId order by id desc) as studentname
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
                    

                    string query = "Select osb.*,format(Date, 'dd/MM/yyyy') as Date1,ar.studentname,ar.rollnumber,ar.class,ar.Section from Online_Sell_billwise osb join admission_registor ar on osb.user_id=ar.admissionserialnumber and osb.SesssionId=ar.Session_id where  osb.Order_Status='Pending' and osb.Payment_Status='Paid' and osb.Idate>=" + idate + " and osb.Idate<=" + idate2 + " order by ar.class,ar.Section,ar.rollnumber, osb.Idate asc";


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
                //string query = "Select osb.*,format(Date,'dd/MM/yyyy') as Date1, (select top 1 studentname from admission_registor where admissionserialnumber=osb.user_id order by id desc) as studentname  from Online_Sell_billwise osb  where  osb.user_id='" + txt_sealid.Text.Trim() + "' and Order_Status='Pending' and Payment_Status='Paid' order by osb.Idate asc ";

                string query = "Select osb.*,format(Date, 'dd/MM/yyyy') as Date1,ar.studentname,ar.rollnumber,ar.class,ar.Section from Online_Sell_billwise osb join admission_registor ar on osb.user_id=ar.admissionserialnumber and osb.SesssionId=ar.Session_id where  osb.Order_Status='Pending' and osb.Payment_Status='Paid' and  osb.user_id='" + txt_sealid.Text.Trim() + "' order by ar.class,ar.Section,ar.rollnumber, osb.Idate asc";


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

        protected void lnk_processorder_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_invoice_no = (Label)row.FindControl("lbl_invoice_no");
            Label lbl_user_id = (Label)row.FindControl("lbl_user_id");
            string url = "Sale_Entry.aspx?appbillno=" + lbl_invoice_no.Text + "&user_id=" + lbl_user_id.Text;
            Response.Redirect(url, false);


        }

        protected void lnk_view_item_details_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_invoice_no = (Label)row.FindControl("lbl_invoice_no");
            Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
            Label lbl_user_id = (Label)row.FindControl("lbl_user_id");

            lblparty_name.Text = lbl_studentname.Text;
            lblinvoice_no.Text = lbl_invoice_no.Text;

            Label lbl_Total_amount = (Label)row.FindControl("lbl_Total_amount");
            lbl_total_value.Text = lbl_Total_amount.Text;
            find_item_details(lbl_invoice_no.Text, lbl_user_id.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }

        private void find_item_details(string invoice_no, string user_id)
        {
            DataTable dt = mycode.FillData(" select *,(select top 1 Item_Name from HMS_Invetory_item_Master where Item_id=t1.Item_Code) as Item_name,(select top 1 Unit from unit_master where unit_id=t1.Unit_id) as Unit_name from Online_Sell_item_wise t1 where t1.Bill_No='" + invoice_no + "' and Sell_To='" + user_id + "' ");
            if (dt.Rows.Count == 0)
            {
                GrdView_Generate_PO.DataSource = null;
                GrdView_Generate_PO.DataBind();

            }
            else
            {
                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();
            }


        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                string excelnam = My.with_excel_name("OnlineOrderPending");
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + excelnam + ".xls");
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
        #endregion download_in_excelpnl_grid


    }
}