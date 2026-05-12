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
    public partial class Add_ledger_details : System.Web.UI.Page
    {
        string scrpt;
        My mycode = new My();
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
                        ViewState["name"] = mycode.get_user(ViewState["Userid"].ToString());
                        string pagename_current = "Add_ledger_details.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        find_firm_details();

                        ViewState["flag"] = "0";
                        txt_date.Text = mycode.date();
                        bind_initial_data();
                        fill_gridview();
                        btn_cancel.Visible = true;

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_ledger_details");
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
        DataTable group_dt;
        DataTable account_dt;
        DataTable state_dt;
        private void bind_initial_data()
        {
            account_dt = My.dataTable("select isnull(Account_Name+','+pd.mobile,Account_Name) as Account,Account_Name from dbo.[Account_Ledger_Details] ald left join party_details pd on ald.firm=pd.firm and ald.Account_id=pd.party_id  where  ald.firm='" + My.firm_id() + "' union select  Group_Name,Group_Name from dbo.[Account_Ledger_Groups] where  firm='" + My.firm_id() + "'");
            group_dt = My.dataTable("select * from dbo.[Account_Ledger_Groups] where  firm = '" + My.firm_id() + "'");
            state_dt = My.dataTable("select * from StateList");

            ViewState["account_dt"] = account_dt;
            ViewState["group_dt"] = group_dt;
            ViewState["state_dt"] = state_dt;


            if (state_dt.Rows.Count > 0)
            {
                ddl_state.DataTextField = "State";
                ddl_state.DataValueField = "Code";
                ddl_state.DataSource = state_dt;
                ddl_state.DataBind();
                ddl_state.Items.Insert(0, new ListItem("Select", "0"));
            }
            //if (group_dt.Rows.Count > 0)
            //{
            //    ddl_group_name.DataTextField = "Group_Name";
            //    ddl_group_name.DataValueField = "Group_id";
            //    ddl_group_name.DataSource = group_dt;
            //    ddl_group_name.DataBind();
            //    ddl_group_name.Items.Insert(0, new ListItem("Select", "0"));
            //}

            ddl_dr_cr.SelectedIndex = 0;
            txt_account_name.Focus();


            ddl_group_name.DataSource = My.bindList("select distinct Group_Name  from dbo.[Account_Ledger_Groups] where  firm = '" + My.firm_id() + "' order by Group_Name", "Primary");
            ddl_group_name.DataBind();
        }
        private void fill_gridview()
        { 
            account_dt = My.dataTable("select distinct lad.*, isnull(Group_Name,'Primary') Group_Name,isnull(Debit,Credit) Amount,Debit_Credit,lob.Date op_date  from Account_Ledger_Details  lad left join Account_Ledger_Groups lg on lg.Group_id=lad.Group_id and lg.firm=lad.firm  left join  Ledger_Opening_Balance lob on lob.Account_id=lad.Account_id and lob.firm=lad.firm and lob.firm = '" + My.firm_id() + "' where lad.Group_id not in ('26') and lad.firm = '" + My.firm_id() + "'");
            if (account_dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = account_dt;
                rd_view.DataBind();

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }
            ViewState["account_dt"] = account_dt;

        }
        #region cancel
        protected void btn_cancel_Click(object sender, EventArgs e)
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
            btn_Submit.Text = "Save";
            My.ClearInputs(Page.Controls);
        }
        #endregion

        protected void ddl_group_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_group_name.SelectedItem.ToString() == "Sundry Creditors" || ddl_group_name.SelectedItem.ToString() == "Sundry Debtors")
            {
                sundry_details.Visible = true;
                pbl_bank_acc.Visible = false;
            }
            else if (ddl_group_name.SelectedItem.ToString() == "Bank Accounts")
            {
                sundry_details.Visible = false;
                pbl_bank_acc.Visible = true;
            }
            else
            {
                sundry_details.Visible = false;
                pbl_bank_acc.Visible = false;
            }
            if (ddl_group_name.SelectedItem != null)
            {
                string under = find_under(ddl_group_name.SelectedItem.ToString());
                if (under == "1" || under == "2" || under == "3")
                {
                    ddl_dr_cr.Text = "Cr";
                }
                else if (under == "4" || under == "5" || under == "6")
                {
                    ddl_dr_cr.Text = "Dr";
                }
            }
        }
        private string find_under(string p)
        {
            group_dt = (DataTable)ViewState["group_dt"];

            var row = group_dt.Select("Group_Name='" + p + "'");
            if (row.Length > 0)
            {
                DataTable t = row.CopyToDataTable();
                return t.Rows.Count > 0 ? t.Rows[0]["Under"].ToString() : "";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// edit delete and update code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        #region download_in_excel
        protected bool IsChecked = true;
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                this.IsChecked = false;
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                    System.IO.FileInfo Info = new System.IO.FileInfo(Path);
                    string pageName = My.with_excel_name("Export") + Info.Name + ".xls";

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
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {

            }
            this.IsChecked = true;

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        #endregion download_in_excel
        #region save_data
        protected void btn_Submit_Click1(object sender, EventArgs e)
        {


            try
            {

                if (btn_Submit.Text == "Save")
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        if (txt_account_name.Text == "")
                        {
                            Alertme("Please Enter Account Name", "warning");
                            txt_account_name.Focus();
                            return;
                        }
                        if (ddl_group_name.Text == "")
                        {
                            Alertme("Please select Group", "warning");
                            ddl_group_name.Focus();
                            return;
                        }

                        txt_account_id.Text = My.firm_id() + "AC" + My.auto_serialS("Account_Id");

                        if (ddl_group_name.SelectedItem.ToString() == "Sundry Creditors" || ddl_group_name.SelectedItem.ToString() == "Sundry Debtors")
                        {
                            if (!valid_sundry())
                            {
                                return;
                            }
                        }
                        if (ddl_group_name.SelectedItem.ToString() == "Bank Accounts" && !valid_bank_details())
                        {
                            return;
                        }
                        save_data();

                        fill_gridview();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
                else
                {

                    try
                    {

                        if (txt_account_name.Text == "")
                        {
                            Alertme("Please Enter Account Name", "warning");
                            txt_account_name.Focus();
                            return;
                        }
                        if (ddl_group_name.Text == "")
                        {
                            Alertme("Please select Group", "warning");
                            ddl_group_name.Focus();
                            return;
                        }
                        if (ddl_group_name.SelectedItem.ToString() == "Sundry Creditors" || ddl_group_name.SelectedItem.ToString() == "Sundry Debtors")
                        {
                            if (!valid_sundry())
                            {
                                return;
                            }
                        }
                        if (ddl_group_name.SelectedItem.ToString() == "Bank Accounts" && !valid_bank_details())
                        {
                            return;
                        }
                        update_data();
                        reset_data();
                        fill_gridview();

                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
        private bool valid_bank_details()
        {

            if (txt_account_holder.Text == "")
            {
                Alertme("Please Enter account name", "warning");
                txt_account_name.Focus();
                return false;
            }
            if (txt_account_no.Text == "")
            {
                Alertme("Please Enter account no", "warning");
                txt_account_no.Focus();
                return false;
            }
            if (txt_branch.Text == "")
            {
                Alertme("Please Enter branch", "warning");
                txt_branch.Focus();
                return false;
            }
            return true;
        }

        private bool valid_sundry()
        {
            if (txt_address.Text == "")
            {
                Alertme("Please Enter address", "warning");
                txt_address.Focus();
                return false;
            }
            if (txt_mobile.Text == "")
            {
                Alertme("Please Enter mobile", "warning");
                txt_mobile.Focus();
                return false;
            }
            if (ddl_registration_type.SelectedItem.ToString() == "Select")
            {
                Alertme("Please Select Registration Type", "warning");
                ddl_registration_type.Focus();
                return false;
            }
            if (ddl_registration_type.SelectedItem.ToString() != "UnRegistered")
            {
                if (txt_gstin.Text == "")
                {
                    Alertme("Please Enter GSTIN", "warning");
                    txt_gstin.Focus();
                    return false;
                }
            }

            if (ddl_state.SelectedItem.ToString() == "")
            {
                Alertme("Please Select State", "warning");
                ddl_state.Focus();
                return false;
            }
            return true;
        }

        private void save_data()

        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Account_Ledger_Details where Account_Name='" + txt_account_name.Text + "'  and firm='" + My.firm_id() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = txt_account_name.Text;
                dr[2] = txt_account_id.Text;
                dr[3] = find_group_id();
                dr[4] = My.firm_id();
                dr["alias"] = txt_alias.Text;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                My.send_data_to_user_log_history(ViewState["name"].ToString() + " added a new account  with  account name : " + dr[1].ToString() + " , account id  : " + dr[2].ToString(), ViewState["Userid"].ToString());

                Opeinig_balance_details(dr["Account_id"].ToString(), dr[3].ToString());
                if (ddl_group_name.SelectedItem.ToString() == "Sundry Creditors")
                {
                    submit_sundry_details(dr[2].ToString(), "party_details", "party_id");
                }
                else if (ddl_group_name.SelectedItem.ToString() == "Sundry Debtors")
                {
                    submit_sundry_details(dr[2].ToString(), "party_details", "party_id");
                }
                else if (ddl_group_name.SelectedItem.ToString() == "Bank Accounts")
                {
                    submit_bank_details(dr[2].ToString());
                }
                Alertme("Account Details Created Successfully", "success");

            }
            else
            {
                Alertme("Duplicate Account Name !", "success");
                txt_account_name.Focus();
                return;
            }
        }
        private object find_group_id()
        {
            if (ddl_group_name.Text == "Primary")
            {
                return "0";
            }
            return My.get_table_data("select Group_id  from dbo.[Account_Ledger_Groups] where  Group_Name='" + ddl_group_name.Text + "' and firm = '" + My.firm_id() + "'");
        }
        private void submit_sundry_details(string ac_id, string table, string column)
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from " + table + " where  " + column + "='" + ac_id + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = txt_account_name.Text;
                dr[2] = txt_address.Text;
                dr[3] = txt_city.Text;
                dr[4] = txt_mobile.Text;
                dr[5] = txt_gstin.Text;
                dr[6] = ac_id;
                dr[7] = mycode.date();
                dr["firm"] = My.firm_id();
                dr[9] = ddl_registration_type.Text;
                dr[10] = ddl_state.Text;
                dr[11] = ddl_state.SelectedValue.ToString();
                dr[12] = "0";
                dr[13] = "";
                if (ddl_group_name.SelectedItem.ToString() == "Sundry Creditors")
                {
                    dr[14] = "Supplier";
                }
                else if (ddl_group_name.SelectedItem.ToString() == "Sundry Debtors")
                {
                    dr[14] = "Customer";
                }
                //    My.save_Account_Ledger_Details(txt_party_name.Text, dr[6].ToString(), "26");
                dt.Rows.Add(dr);

                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = txt_account_name.Text;
                    dr[2] = txt_address.Text;
                    dr[3] = txt_city.Text;
                    dr[4] = txt_mobile.Text;
                    dr[5] = txt_gstin.Text;
                    dr[6] = ac_id;
                    dr[7] = mycode.date();
                    dr["firm"] = My.firm_id();
                    dr[9] = ddl_registration_type.Text;
                    dr[10] = ddl_state.Text;
                    dr[11] = ddl_state.SelectedValue.ToString();
                    dr[12] = "0";
                    dr[13] = "";
                    if (ddl_group_name.SelectedItem.ToString() == "Sundry Creditors")
                    {
                        dr[14] = "Supplier";
                    }
                    else if (ddl_group_name.SelectedItem.ToString() == "Sundry Debtors")
                    {
                        dr[14] = "Customer";
                    }
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                }
            }
        }
        private void submit_bank_details(string acc_id)
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from bank_details where bank_id='" + acc_id + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = txt_account_name.Text;
                dr[2] = txt_account_holder.Text;
                dr[3] = txt_account_no.Text;
                dr[4] = txt_ifc_code.Text;
                dr[5] = txt_branch.Text;
                dr[6] = mycode.date();
                dr["firm"] = My.firm_id();
                dr["bank_id"] = acc_id;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = txt_account_name.Text;
                    dr[2] = txt_account_holder.Text;
                    dr[3] = txt_account_no.Text;
                    dr[4] = txt_ifc_code.Text;
                    dr[5] = txt_branch.Text;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }

        }
        private void Opeinig_balance_details(String Account_id, String Group_id)
        {

            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Ledger_Opening_Balance where  Account_id='" + Account_id + "' and firm='" + My.firm_id() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                if (My.toDouble(txt_opening_balance.Text) == 0)
                {
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["Account_id"] = Account_id;
                    dr["Debit_Credit"] = ddl_dr_cr.Text;
                    if (ddl_dr_cr.Text == "Dr")
                    {
                        dr["Debit"] = My.toAmount(txt_opening_balance.Text);
                        dr["Credit"] = null;
                    }
                    else
                    {
                        dr["Credit"] = My.toAmount(txt_opening_balance.Text);
                        dr["Debit"] = null;
                    }
                    dr["Date"] = txt_date.Text;
                    dr["firm"] = My.firm_id();
                    dr["Session"] = My.get_session();
                    dr["Group_id"] = Group_id;
                    dr["idate"] = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                if (My.toDouble(txt_opening_balance.Text) == 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr.Delete();
                    }
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["Account_id"] = Account_id;
                        if (ddl_dr_cr.Text == "Dr")
                        {
                            dr["Debit"] = My.toAmount(txt_opening_balance.Text);
                            dr["Credit"] = null;
                        }
                        else
                        {
                            dr["Credit"] = My.toAmount(txt_opening_balance.Text);
                            dr["Debit"] = null;
                        }
                        dr["Debit_Credit"] = ddl_dr_cr.Text;
                        dr["Date"] = txt_date.Text;
                        dr["firm"] = My.firm_id();
                        dr["Session"] = My.get_session();
                        dr["Group_id"] = Group_id;
                        dr["idate"] = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
                    }

                }
            }

            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        #endregion save_data

        #region edit_delete_update
        string group_id = "", group_name = "";
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    btn_Submit.Text = "Update";
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    string id = ((Label)row.FindControl("lbl_Id")).Text;
                    string Account_id = ((Label)row.FindControl("lbl_Account_id")).Text;
                    ViewState["Account_id"] = Account_id;
                    ViewState["id"] = id;
                    account_dt = (DataTable)ViewState["account_dt"];

                    var row1 = account_dt.Select("Account_id='" + Account_id + "'");
                    if (row1.Length > 0)
                    {
                        DataTable t = row1.CopyToDataTable();
                        if (t.Rows.Count > 0)
                        {
                            foreach (DataRow dr in t.Rows)
                            {
                                txt_account_name.Text = dr[1].ToString();
                                txt_account_id.Text = dr[2].ToString();
                                ddl_group_name.Text = dr["Group_Name"].ToString();
                                ddl_dr_cr.Text = dr["Debit_Credit"].ToString() == "" ? "Dr" : dr["Debit_Credit"].ToString();
                                txt_opening_balance.Text = dr["Amount"].ToString();

                                txt_date.Text = dr["op_date"].ToString() == "" ? "Dr" : dr["op_date"].ToString();
                                group_id = dr["Group_id"].ToString();
                                group_name = dr["Group_Name"].ToString();
                                if (ddl_group_name.SelectedItem.ToString() == "Sundry Creditors" || ddl_group_name.SelectedItem.ToString() == "Sundry Debtors")
                                {
                                    DataTable dt = new DataTable();
                                    if (group_name == "Sundry Creditors")
                                    {
                                        dt = My.dataTable("select * from dbo.[party_details] where  party_id='" + txt_account_id.Text + "'");
                                    }
                                    else if (group_name == "Sundry Debtors")
                                    {
                                        dt = My.dataTable("select  * from dbo.[party_details] where  party_id='" + txt_account_id.Text + "'");
                                    }
                                    foreach (DataRow dr1 in dt.Rows)
                                    {
                                        txt_address.Text = dr1[2].ToString();
                                        txt_city.Text = dr1[3].ToString();
                                        txt_mobile.Text = dr1[4].ToString();
                                        txt_gstin.Text = dr1[5].ToString();
                                        ddl_registration_type.Text = dr1[9].ToString();
                                        ddl_state.Text = dr1[10].ToString();
                                        //route_id = dr1[12].ToString();
                                        //ddl_route.SelectedItem = dr1[13].ToString();
                                        pbl_bank_acc.Visible = false;
                                        sundry_details.Visible = false;
                                    }
                                }
                                else if (ddl_group_name.SelectedItem.ToString() == "Bank Accounts")
                                {
                                    DataTable dt = My.dataTable("select * from bank_details where  bank_id='" + txt_account_id.Text + "'");

                                    foreach (DataRow dr1 in dt.Rows)
                                    {
                                        txt_account_holder.Text = dr1[2].ToString();
                                        txt_account_no.Text = dr1[3].ToString();
                                        txt_ifc_code.Text = dr1[4].ToString();
                                        txt_branch.Text = dr1[5].ToString();
                                        pbl_bank_acc.Visible = true;
                                        sundry_details.Visible = false;
                                    }
                                }
                            }

                        }
                    }


                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
                 
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }
        }


        private void update_data()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Account_Ledger_Details where  Id='" + ViewState["id"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            string grp_id = "", acc_id = "";
            foreach (DataRow dr in dt.Rows)
            {
                dr[1] = txt_account_name.Text;
                dr[3] = find_group_id();
                dr[4] = My.firm_id();
                grp_id = dr[3].ToString();
                acc_id = dr[2].ToString();
                Opeinig_balance_details(dr["Account_id"].ToString(), dr[3].ToString());
                My.send_data_to_user_log_history(ViewState["name"].ToString() + " updated  account  with  account id  : " + dr[2].ToString(), ViewState["Userid"].ToString());
            }
            if (group_id != grp_id)
            {
                if (group_name == "Sundry Creditors")
                {
                    My.exeSql("delete from dbo.[party_details] where  party_id='" + acc_id + "'");
                }
                else if (group_name == "Sundry Debtors")
                {
                    My.exeSql("delete from dbo.[party_details] where  party_id='" + acc_id + "'");
                }
            }
            if (ddl_group_name.SelectedItem.ToString() == "Sundry Creditors")
            {
                submit_sundry_details(acc_id, "party_details", "party_id");
            }
            else if (ddl_group_name.SelectedItem.ToString() == "Sundry Debtors")
            {
                submit_sundry_details(acc_id, "party_details", "party_id");
            }
            else if (ddl_group_name.SelectedItem.ToString() == "Bank Accounts")
            {
                submit_bank_details(acc_id);
            }

            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Account Details  Updated Successfully", "success");
        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    string id = ((Label)row.FindControl("lbl_Id")).Text;
                    string Account_id = ((Label)row.FindControl("lbl_Account_id")).Text;
                    string Account_Name = ((Label)row.FindControl("lbl_Account_Name")).Text;
                    string Group_id = ((Label)row.FindControl("lbl_Group_id")).Text;


                    delete_data(Account_id, id, Account_Name, Group_id);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }

        }

        private void delete_data(string account_id, string id, string account_Name, string Group_id)
        {
            if (My.dataTable("select Account_Name from Account_Ledger_Details where  Account_id='" + account_id + "' and firm=0").Rows.Count != 0)
            {
                Alertme(account_Name + " account can't be deleted", "warning");
                return;
            }
            if (My.dataTable("select Account_id from Account_Voucher_Details where  Account_id='" + account_id + "' and firm=0").Rows.Count != 0)
            {
                Alertme(account_Name + " account can't be deleted", "warning");
                return;
            }
            if (My.dataTable("select Account_id from Account_Voucher_Details where  Alternet_Account='" + account_id + "' and firm=0").Rows.Count != 0)
            {
                Alertme(account_Name + " account can't be deleted", "warning");
                return;
            }
            //if (Alertme.Show("Are you sure want to delete this Account Details ?", "Warning", AlertmeButton.YesNo, AlertmeImage.Warning) == AlertmeResult.Yes)
            //{
            //    bool is_continue = (is_continue = My.showWarning()) ? true : false;
            //    if (!is_continue)
            //    {
            //        return;
            //    }
            //    if (My.otp_confirmation && My.user_type == "Manager")
            //    {
            //        string region = "User " + My.user_name + " wants to delete Account Ledger details , Account Name : " + txt_account_name.Text + ".";
            //        Verify_OTP a = new Verify_OTP();
            //        My.verifyOTP(this, a, region);
            //        a.btn_validate.Click += (s1, e1) =>
            //        {
            //            if (a.status)
            //            {
            //                delete_account(dr1);
            //            }
            //        };
            //    }
            //    else
            //    {
            //        delete_account(dr1);
            //    }
            //}

            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Account_Ledger_Details where  Id='" + id + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {


                My.send_data_to_user_log_history(ViewState["name"].ToString() + " deleted account  with  account name : " + account_Name + " , account id  : " + account_id, ViewState["Userid"].ToString());
                dr.Delete();
            }
            if (Group_id == "26" || Group_id == "22")
            {
                My.exeSql("delete from dbo.[party_details]  where party_id='" + account_id + "' AND firm=" + My.firm_id());
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            delete_from_opening_balance(account_id);
            Alertme("Account Details deleted Successfully", "success");
            fill_gridview();


        }



        private void delete_from_opening_balance(String Account_id)
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Ledger_Opening_Balance where  Account_id='" + Account_id + "' and firm='" + My.firm_id() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr.Delete();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }
        #endregion edit_delete_update




    }
}