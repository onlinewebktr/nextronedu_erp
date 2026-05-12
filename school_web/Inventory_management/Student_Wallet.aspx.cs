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
    public partial class Student_Wallet : System.Web.UI.Page
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
        string studentname = " select top 1 party_name from dbo.[party_details] where party_id=isdb.Adm_no  ";
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

                        bind_datewise_all_student();

                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }

        private void bind_datewise_all_student()
        {

            string query = "Select party_id as admissionserialnumber,party_name as studentname from party_details where party_id in (Select Adm_no from Student_Wallet )  ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no records exist of the student wallet", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                lbl_heading.Text = "Student Wallet";
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        protected void lnk_view_item_details_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Adm_no = (Label)row.FindControl("lbl_Adm_no");
            Label lbl_CustomerName = (Label)row.FindControl("lbl_CustomerName");

            lblparty_name.Text = lbl_CustomerName.Text;
            lbladmission_no.Text = lbl_Adm_no.Text;
            fetch_wallet_details(lbl_Adm_no.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        private void fetch_wallet_details(string Adm_no)
        {
            DataTable dt = mycode.FillData("Select   *,format(Date_of_entry, 'dd/MM/yyyy') as Date1 from Student_Wallet where Adm_no='" + Adm_no + "'");
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

        double totalamunt = 0.00;
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string lbl_Adm_no = ((Label)e.Item.FindControl("lbl_Adm_no")).Text;
                string amt= get_wallet_amount(lbl_Adm_no);
                ((Label)e.Item.FindControl("lbl_walletamount")).Text = amt;
                totalamunt = totalamunt + My.toDouble(amt);

                lbl_fnl_paid.Text = totalamunt.ToString("0.00");

            }
        }

        private string get_wallet_amount(string lbl_Adm_no)
        {
            try
            {
                DataTable dt = mycode.FillData("Select sum(cast(Wallet_input_amount as float)) as totalinput ,sum(cast(Wallet_Out_amount as float)) as totaloutput from Student_Wallet where Adm_no='" + lbl_Adm_no + "'");
                if (dt.Rows.Count == 0)
                {
                    return "0.00";
                }
                else
                {
                    double rest = My.toDouble(dt.Rows[0]["totalinput"].ToString()) - My.toDouble(dt.Rows[0]["totaloutput"].ToString());
                    return rest.ToString("0.00");
                }
            }
            catch
            {
                return "0.00";
            }

        }

        double totalinput = 0.00, totaloutput = 0.00, restamount=0.00;
        protected void GrdView_Generate_PO_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string Wallet_input_amount = ((Label)e.Item.FindControl("lbl_Wallet_input_amount")).Text;
                string Wallet_Out_amount = ((Label)e.Item.FindControl("lbl_Wallet_Out_amount")).Text;

                totalinput = totalinput + My.toDouble(Wallet_input_amount);
                totaloutput = totaloutput + My.toDouble(Wallet_Out_amount);


                restamount = (My.toDouble(totalinput) - My.toDouble(totaloutput));
                lbl_total_input.Text = totalinput.ToString("0.00");
                lbl_total_output.Text = totaloutput.ToString("0.00");
                lbl_restamount.Text = restamount.ToString("0.00");
            }
        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Student_Wallet_" + mycode.date() + "_" + mycode.time() + ".xls");

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
        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            RPDetails.DataSource = null;
            RPDetails.DataBind();
            if (txt_admission_no.Text=="")
            {
                Alertme("Please enter valid admission no.", "warning");
            }
            else
            {
                string query = "Select admissionserialnumber,studentname from admission_registor where admissionserialnumber in (Select Adm_no from Student_Wallet where Adm_no='"+ txt_admission_no.Text + "' )";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no records exist of the student wallet", "warning");
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                }
                else
                {
                    lbl_heading.Text = "Student Wallet";
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Student_Wallet_History" + mycode.date() + "_" + mycode.time() + ".xls");

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    Panel1.RenderControl(hw);
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

      







        #endregion download_in_excel
    }
}