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
    public partial class add_bank_details : System.Web.UI.Page
    {
        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = My.firm_id();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select * from bank_details");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }
        #endregion

        #region Save/Update Bank Details
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_bank_name.Text == "")
                {
                    Alertme("Please enter bank name.", "warning");
                    txt_bank_name.Focus();
                    return;
                }
                if (txt_account_name.Text == "")
                {
                    Alertme("Please enter account name.", "warning");
                    txt_account_name.Focus();
                    return;
                }
                if (txt_account_no.Text == "")
                {
                    Alertme("Please enter account no.", "warning");
                    txt_account_no.Focus();
                    return;
                }
                if (txt_branch.Text == "")
                {
                    Alertme("Please enter branch.", "warning");
                    txt_branch.Focus();
                    return;
                }
                save_data();

            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
        private void save_data()
        {
            if (btn_Submit.Text == "Save")
            {
                string insert_qry = "insert into bank_details (bank_name,account_name,account_no,ifc_code,branch,date,bank_id,firm) values('" + txt_bank_name.Text + "','" + txt_account_name.Text + "','" + txt_account_no.Text + "','" + txt_ifc_code.Text + "','" + txt_branch.Text + "','" + mycode.date() + "','{0}','" + ViewState["firm_id"].ToString() + "'); select * from bank_details";
                string bank_name = txt_bank_name.Text;
                bool status = false;
                DataTable dt = new DataTable();
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    string bank_id = "bnk" + My.auto_serialS("bank_id");
                    My.save_Account_Ledger_Details(bank_name, bank_id, "28");
                    dt = My.dataTable(string.Format(insert_qry, bank_id));
                    ts.Complete();
                    status = true;
                }
                if (status)
                {
                    reset_data();
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    Alertme("Bank details has been created successfully.", "success");
                }
                else
                {
                    Alertme("Unable to create bank details.", "warning");
                }
            }
            else    // UPdate Bank
            {
                string bank_name = txt_bank_name.Text;
                string update_qry = "update bank_details set bank_name='" + txt_bank_name.Text + "',account_name='" + txt_account_name.Text + "',account_no='" + txt_account_no.Text + "',ifc_code='" + txt_ifc_code.Text + "',branch='" + txt_branch.Text + "',date='" + mycode.date() + "' where id='" + ViewState["id"].ToString() + "'; select * from bank_details";
                bool status = false;
                DataTable dt = new DataTable();
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    My.save_Account_Ledger_Details(bank_name, ViewState["bank_id"].ToString(), "28");
                    dt = My.dataTable(string.Format(update_qry));
                    ts.Complete();
                    status = true;
                }
                if (status)
                {
                    reset_data();
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    Alertme("Bank details has been updated successfully.", "success");
                }
                else
                {
                    Alertme("Unable to update bank details", "warning");
                }
            }
        }
        #endregion 

        #region cancel
        protected void btn_cancel_Click1(object sender, EventArgs e)
        {
            try
            {
                reset_data();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }
        }

        private void reset_data()
        {
            btn_Submit.Visible = true;
            btn_cancel.Visible = false;
            My.ClearInputs(Page.Controls);
        }
        #endregion cancel



        #region edit_delete_update

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_bank_name = (Label)row.FindControl("lbl_bank_name");
                    Label lbl_account_name = (Label)row.FindControl("lbl_account_name");
                    Label lbl_account_no = (Label)row.FindControl("lbl_account_no");
                    Label lbl_ifc_code = (Label)row.FindControl("lbl_ifc_code");
                    Label lbl_branch = (Label)row.FindControl("lbl_branch");
                    string id = ((Label)row.FindControl("lbl_Id")).Text;
                    string bank_id = ((Label)row.FindControl("lbl_bank_id")).Text;
                    txt_bank_name.Text = lbl_bank_name.Text;
                    txt_account_name.Text = lbl_account_name.Text;
                    txt_account_no.Text = lbl_account_no.Text;
                    txt_ifc_code.Text = lbl_ifc_code.Text;
                    txt_branch.Text = lbl_branch.Text;
                    ViewState["bank_id"] = bank_id;
                    ViewState["id"] = id;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                }
                else
                {
                    Alertme("SORRY! You have not permission to this work.", "warning");
                }
            }
            catch
            {

            }
        }





        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                string id = ((Label)row.FindControl("lbl_Id")).Text; 
                string bank_id = ((Label)row.FindControl("lbl_bank_id")).Text;
                ViewState["bank_id"] = bank_id;
                ViewState["id"] = id;
                delete_data(); 
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString()); 
            } 
        }

        private void delete_data()
        {
            DataTable dt = My.dataTable("select * from Account_Voucher_Details where Account_id='" + ViewState["bank_id"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                Alertme("This Bank details can't be deleted.", "warning");
            }
            else
            {
                string delete_qry = "delete from bank_details where id='" + ViewState["id"].ToString() + "'; delete from Account_Ledger_Details where Account_id='" + ViewState["bank_id"].ToString() + "'; select * from bank_details;";
                bool status = false;
                dt = new DataTable();
                dt = My.dataTable(string.Format(delete_qry));
                status = true;
                if (status)
                { 
                    reset_data();
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    Alertme("Bank details has been deleted successfully","success");
                }
                else
                {
                    Alertme("Unable to deleted bank details", "success");
                }
            } 
        }
        #endregion edit_delete_update 


        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo Info = new System.IO.FileInfo(Path);
            string pageName = Info.Name + ".xls";

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + pageName);
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion download_in_excel
    }
}