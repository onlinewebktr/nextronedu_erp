using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Contra_entry : System.Web.UI.Page
    {
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_particular(string itemName)
        {

            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select  distinct Account_Name  from dbo.[Account_Ledger_Details] where Group_id in(" + HttpContext.Current.Session["groupList"].ToString() + ") and firm='" + My.firm_id() + "' and  Account_Name LIKE '%'+@SearchprojectName+'%' order by Account_Name asc";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Account_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
        string VoucherType = "Contra";
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
                        if (!IsPostBack)
                        {


                            ViewState["Userid"] = Session["Admin"].ToString();

                            ViewState["name"] = mycode.get_user(ViewState["Userid"].ToString());
                            string pagename_current = "Payment_Voucher.aspx";
                            Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                            ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                            ViewState["Is_delete"] = (String)dc1["Is_delete"];
                            ViewState["Is_Download"] = (String)dc1["Is_Download"];
                            ViewState["Is_Print"] = (String)dc1["Is_Print"];
                            ViewState["Is_add"] = (String)dc1["Is_add"];
                            ViewState["usertype"] = My.get_user_type(ViewState["Userid"].ToString());
                            find_firm_details();
                            txt_fromdate.Text = mycode.date();
                            txt_to_date.Text = mycode.date();
                            txt_date.Text = mycode.date();
                            bind_account_cr();
                            bind_gridview();

                        }




                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_ledger_details");
            }


        }

        private void bind_account_cr()
        {
            string groupList = "";
            ArrayList al = new ArrayList();
            SqlDataAdapter ad = new SqlDataAdapter("select Group_id from Account_Ledger_Groups where Group_id  in('27','28') and firm='" + My.firm_id() + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "binary_status");
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Group_id = dr[0].ToString();
                    al.Add(Group_id);
                    bind_grouplist(al);
                }
                groupList = groupList + ViewState["groupList"].ToString();

            }
            mycode.bind_all_ddl_with_id(ddl_account_cr, "select  Account_Name as DataTextField ,Account_id as DataValueField from dbo.[Account_Ledger_Details] where Group_id in(" + groupList + ") and firm='" + My.firm_id() + "'  order by Account_Name asc");

        }
        private void bind_grouplist(ArrayList al)
        {

            string group = "";
            for (int i = 0; i < al.Count; i++)
            {
                if (ViewState["groupList"] == null)
                    ViewState["groupList"] = "'" + al[i].ToString() + "'";
                else
                    ViewState["groupList"] = ViewState["groupList"].ToString() + ",'" + al[i].ToString() + "'";

                string qry = "select Group_id from Account_Ledger_Groups where Under='" + al[i].ToString() + "' and firm='" + My.firm_id() + "'";
                SqlDataAdapter ad1 = new SqlDataAdapter(qry, My.conn);
                DataSet ds1 = new DataSet();
                ad1.Fill(ds1, "Account_Ledger_Details");
                DataTable dt1 = ds1.Tables[0];
                int rowcount1 = ds1.Tables[0].Rows.Count;
                if (rowcount1 == 0)
                {
                }
                else
                {
                    group = dt1.Rows[0][0].ToString();
                    al.Add(group);
                }
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


                if (ViewState["usertype"].ToString() == "Admin")
                {
                    pnl_datetime.Visible = true;
                }
                else if (ViewState["usertype"].ToString() == "Director")
                {
                    pnl_datetime.Visible = true;
                }
                else if (ViewState["usertype"].ToString() == "Principal")
                {
                    pnl_datetime.Visible = true;
                }
                else
                {
                    if (dt.Rows[0]["Is_user_change_pay_date"].ToString() == "True")
                    {
                        pnl_datetime.Visible = true;

                    }
                    else
                    {
                        pnl_datetime.Visible = false;
                    }

                }



            }
        }
        private void fetch_particular()
        {
            string groupList = "";
            ArrayList al = new ArrayList();
            SqlDataAdapter ad = new SqlDataAdapter("select Group_id from Account_Ledger_Groups where Group_id  in('27','28') and firm='" + My.firm_id() + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "binary_status");
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Group_id = dr[0].ToString();

                    // string Group_id = dt.Rows[0][0].ToString();
                    al.Add(Group_id);
                    bind_grouplist(al);
                }
                groupList = groupList + ViewState["groupList"].ToString();

            }
            Session["groupList"] = groupList;
            mycode.bind_all_ddl_with_id(ddl_particular, "select  Account_Name as DataTextField ,Account_id as DataValueField from dbo.[Account_Ledger_Details] where Group_id in(" + groupList + ") and firm='" + My.firm_id() + "' and Account_id !='" + ddl_account_cr.SelectedValue + "' order by Account_Name asc");

        }
        private void bind_gridview(ArrayList al)
        {

            string group = "";
            for (int i = 0; i < al.Count; i++)
            {
                if (ViewState["groupList"] == null)
                    ViewState["groupList"] = "'" + al[i].ToString() + "'";
                else
                    ViewState["groupList"] = ViewState["groupList"].ToString() + ",'" + al[i].ToString() + "'";


                string qry = "select Group_id from Account_Ledger_Groups where Under='" + al[i].ToString() + "' and firm='" + My.firm_id() + "'";
                SqlDataAdapter ad1 = new SqlDataAdapter(qry, My.conn);
                DataSet ds1 = new DataSet();
                ad1.Fill(ds1, "Account_Ledger_Details");
                DataTable dt1 = ds1.Tables[0];
                int rowcount1 = ds1.Tables[0].Rows.Count;
                if (rowcount1 == 0)
                {
                }
                else
                {
                    group = dt1.Rows[0][0].ToString();
                    if (!al.Contains(group))
                        al.Add(group);
                }
            }

        }

        private void bind_gridview()
        {
            btn_excels.Visible = false;
            print1.Visible = false;
            string fromdate = txt_fromdate.Text + " " + txt_fromtime.Text;
            string todate = txt_to_date.Text + " " + txt_to_time.Text;
            DateTime d1 = My.convert_to_datetime(fromdate, "dd/MMM/yyyy hh:mm tt");
            DateTime d2 = My.convert_to_datetime(todate, "dd/MMM/yyyy hh:mm tt");

            lbl_class22.Text = "From Date : " + txt_fromdate.Text + " To Date : " + txt_to_date.Text;


            //  DataTable dt = mycode.FillData(" select VoucherNo,Description,sum(cast(Credit as float)) Credit ,sum(cast(Debit as float)) Debit,unique_entry_id,ref_name,Date  from dbo.[Account_Voucher_Details]   where  VoucherType='Contra' and firm ='" + My.firm_id() + "' and  Bill_from='SCHOOL'  and IDate>=" + d1.ToString("yyyyMMdd") + " and  IDate<=" + d2.ToString("yyyyMMdd") + "  group by VoucherNo,Date ,idate,unique_entry_id,ref_name,Description   order by idate desc");

            DataTable dt = mycode.FillData("  select (Select top 1 Account_name from Account_Ledger_Details where Account_id=t1.Account_id) as Account_CR,(Select top 1 Account_name from Account_Ledger_Details where Account_id=t1.Alternet_Account) as Account_Dr, Account_Id,Alternet_Account,Description,Credit As Amount,unique_entry_id,Date,VoucherNo_Manual as VoucherNo,VoucherNo as v1  from dbo.[Account_Voucher_Details] t1  where  VoucherType='Contra' and firm =" + My.firm_id() + " and  Bill_from='SCHOOL'  and IDate>=" + d1.ToString("yyyyMMdd") + " and  IDate<=" + d2.ToString("yyyyMMdd") + "  and    Credit>0");



            if (dt.Rows.Count == 0)
            {
                grd_bill_trac.DataSource = null;
                grd_bill_trac.DataBind();
                GridView1.DataSource = null;
                GridView1.DataBind();

            }
            else
            {
                if (rd_with_time.Checked == true)
                {
                    DataRow[] dr = dt.Select("Date>='" + d1 + "' and Date<='" + d2 + "'");
                    if (dr.Length != 0)
                    {
                        DataTable dt1 = dr.CopyToDataTable();
                        grd_bill_trac.DataSource = dt1;
                        grd_bill_trac.DataBind();

                        GridView1.DataSource = dt1;
                        GridView1.DataBind();



                        btn_excels.Visible = true;
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }


                    }
                }
                else
                {
                    grd_bill_trac.DataSource = dt;
                    grd_bill_trac.DataBind();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    btn_excels.Visible = true;
                    if (ViewState["Is_Print"].ToString() == "1")
                    {
                        print1.Visible = true;
                    }
                    else
                    {
                        print1.Visible = false;
                    }

                }

            }


        }
        protected void lnk_print_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Print"].ToString() == "1")
            {
                try
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.NamingContainer;
                    string Unique_entry_id = ((Label)row.FindControl("lbl_Unique_entry_id")).Text;
                    string voucher_no = ((Label)row.FindControl("lbl_VoucherNo")).Text;

                    string path = "Slip/payment_slip.aspx?Unique_entry_id=" + Unique_entry_id + "&voucher_no=" + voucher_no + "&vouchertype=" + VoucherType + "&firm=" + My.firm_id() + "&session=" + My.get_session();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + path + "','_newtab');", true);

                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }
        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.NamingContainer;
                    string Unique_entry_id = ((Label)row.FindControl("lbl_Unique_entry_id")).Text;
                    string voucher_no = ((Label)row.FindControl("lbl_VoucherNo")).Text;
                    string Date = ((Label)row.FindControl("lbl_Date")).Text;
                    string Description = ((Label)row.FindControl("lbl_Description")).Text;
                    string Credit = ((Label)row.FindControl("lbl_Amount")).Text;
                    ViewState["voucher_no"] = voucher_no;
                    ViewState["Unique_entry_id"] = Unique_entry_id;

                    try
                    {
                        string[] stringSeparatorss = new string[] { ":" };
                        string[] arrs = Description.Split(stringSeparatorss, StringSplitOptions.None);
                        string class_teacher_sig = arrs[0];
                        string a1 = arrs[0];
                        string a2 = arrs[1];
                        txt_remarks.Text = a2;
                    }
                    catch
                    {
                        txt_remarks.Text = Description;
                    }


                    DataTable dt = mycode.FillData("Select top 1 ad.*,(Select top 1 Account_Name from  Account_Ledger_Details where Account_id=ad.Alternet_Account ) Dr_account  from Account_Voucher_Details ad where ad.unique_entry_id='" + Unique_entry_id + "' order by ad.id asc ");
                    if (dt.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        ddl_account_cr.SelectedValue = dt.Rows[0]["Account_id"].ToString();
                        fetch_particular();
                        ddl_particular.SelectedValue = dt.Rows[0]["Alternet_Account"].ToString();
                        txt_amount.Text = Credit;

                        btn_update.Visible = true;
                        btn_Submit.Visible = false;
                        txt_date.Text = My.convert_to_datetime(Date, "dd/MMM/yyyy hh:mm:ss tt").ToString("dd/MM/yyyy");
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


        private void reset_data()
        {
            My.ClearInputs(Page.Controls);
            txt_date.Text = mycode.date();
            txt_fromdate.Text = mycode.date();
            txt_to_date.Text = mycode.date();
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            try { reset_data(); }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }
        protected void btn_update_Click(object sender, EventArgs e)
        {

            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    //using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                    //{
                    if (ddl_account_cr.SelectedValue == "0")
                    {
                        Alertme("Please Select Account(Cr.)", "warning");
                        ddl_account_cr.Focus();
                        return;

                    }
                    if (ddl_particular.Text == "")
                    {
                        Alertme("Please Select particular", "warning");
                        ddl_particular.Focus();
                        return;

                    }
                    //if (txt_amount.Text == "0")
                    //{
                    //    Alertme("Please Enter Amount");
                    //    txt_amount.Focus();
                    //    return;

                    //}
                    delete_payment_voucher();
                    Update_data();
                    //    ts.Complete();

                    //}
                }
                else
                {

                    Alertme(My.get_restricted_message(), "warning");

                }
            }
            catch (Exception ex)
            {
                Alertme("Please try again", "warning");
                My.submitexception(ex.ToString());
            }


        }
        private void delete_payment_voucher()
        {
            string qry = "delete from Account_Voucher_Details  where unique_entry_id='" + ViewState["Unique_entry_id"].ToString() + "';";



            My.exeSql(qry);
        }
        private void Update_data()
        {
            double Amount = Convert.ToDouble(txt_amount.Text);

            //if (Amount > 0)
            //{
            string particular = find_txt_particular();
            if (particular == "0")
            {
                Alertme("Please create ledger first.", "");
            }
            else
            {
                string unique_entry_id = ViewState["Unique_entry_id"].ToString();
                string Description = txt_remarks.Text; //$"Amount Payment from {ddl_account_cr.SelectedItem.Text}. Remarks : {txt_remarks.Text}";

                string Date = txt_date.Text + " " + mycode.time();

                // DateTime Date1 = DateTime.ParseExact(Date, "yyyy/MM/dd hh:mm:ss tt", CultureInfo.InvariantCulture);

                DateTime billdate = Convert.ToDateTime((txt_date.Text + " " + mycode.time()));

                string IDate = billdate.ToString("yyyyMMdd");
                string VoucherNo = ViewState["voucher_no"].ToString();
                string alternetacc_id = My.find_account_id(ddl_particular.Text);

                if ((ddl_account_cr.SelectedItem.Text).Equals("Cash", StringComparison.OrdinalIgnoreCase))
                {
                    My.send_to_cash_payment_Voucher_Details(ddl_account_cr.SelectedValue, ddl_particular.SelectedValue, txt_amount.Text, VoucherNo, unique_entry_id, Description, Date, IDate, VoucherType, My.firm_id(), My.get_session(), ViewState["Userid"].ToString(), "SCHOOL", "", VoucherNo, "");
                }
                else
                {
                    My.send_to_bank_payment_Voucher_Details(ddl_account_cr.SelectedValue, ddl_particular.SelectedValue, txt_amount.Text, VoucherNo, unique_entry_id, Description, Date, IDate, VoucherType, My.firm_id(), My.get_session(), ViewState["Userid"].ToString(), "SCHOOL", "", VoucherNo, "");
                }


                Alertme("Data updated successfully", "success");
                reset_data();
                bind_gridview();
            }

            //}
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                bind_gridview();
            }
            catch (Exception ex)
            {

            }
        }
        double totalamt = 0;
        protected void grd_bill_trac_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lbl_Amount = (Label)e.Row.FindControl("lbl_Amount");
                    totalamt = totalamt + My.toDouble(lbl_Amount.Text);
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lbl_total = (Label)e.Row.FindControl("lbl_total");
                    lbl_total.Text = totalamt.ToString("0.00");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Download"].ToString() == "1")
            {


                string pageName = My.with_excel_name("Contra_vouchar") + ".xls";

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + pageName);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GridView1.RenderControl(hw);
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion download_in_excel
        protected void rd_without_time_CheckedChanged(object sender, EventArgs e)
        {
            fromtime.Visible = false;
            totime.Visible = false;
        }
        protected void rd_with_time_CheckedChanged(object sender, EventArgs e)
        {
            fromtime.Visible = true;
            totime.Visible = true;
        }
        //pay
        #region pay

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    //using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                    //{
                    if (ddl_account_cr.SelectedValue == "0")
                    {
                        Alertme("Please Select Account(Cr.)", "warning");
                        ddl_account_cr.Focus();
                        return;

                    }
                    if (ddl_particular.SelectedValue == "0")
                    {
                        Alertme("Please Select particular", "warning");
                        ddl_particular.Focus();
                        return;

                    }
                    //if (txt_amount.Text == "0")
                    //{
                    //    Alertme("Please Enter Amount", "warning");
                    //    txt_amount.Focus();
                    //    return;
                    //}
                    save_data();
                    //ts.Complete();

                    //}
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                Alertme("Please try again", "warning");
                My.submitexception(ex.ToString());
            }
        }

        private void save_data()
        {
            double Amount = Convert.ToDouble(txt_amount.Text);

            //if (Amount > 0)
            //{
            string particular = find_txt_particular();
            if (particular == "0")
            {
                Alertme("Please create ledger first.", "warning");
            }
            else
            {
                string unique_entry_id = My.unique_id();

                string Description = txt_remarks.Text; //$"Amount Payment from {ddl_account_cr.SelectedItem.Text}. Remarks : {txt_remarks.Text}";

                string Date = txt_date.Text + " " + mycode.time();




                DateTime billdate = Convert.ToDateTime((txt_date.Text + " " + mycode.time()));


                string IDate = billdate.ToString("yyyyMMdd");
                string VoucherNo = "";

                VoucherNo = "C" + My.session_wise_view_auto_serial(VoucherType + "_voucher", My.get_session(), My.firm_id());


                if ((ddl_account_cr.SelectedItem.Text).Equals("Cash", StringComparison.OrdinalIgnoreCase))
                {
                    My.send_to_cash_payment_Voucher_Details(ddl_account_cr.SelectedValue, ddl_particular.SelectedValue, txt_amount.Text, VoucherNo, unique_entry_id, Description, Date, IDate, VoucherType, My.firm_id(), My.get_session(), ViewState["Userid"].ToString(), "SCHOOL", "", VoucherNo, "");
                }
                else
                {
                    My.send_to_bank_payment_Voucher_Details(ddl_account_cr.SelectedValue, ddl_particular.SelectedValue, txt_amount.Text, VoucherNo, unique_entry_id, Description, Date, IDate, VoucherType, My.firm_id(), My.get_session(), ViewState["Userid"].ToString(), "SCHOOL", "", VoucherNo, "");
                }


                My.send_data_to_user_log_history(ViewState["name"].ToString() + " Contra entry of amount" + txt_amount.Text + " & Voucher No:- " + VoucherNo, ViewState["Userid"].ToString());

                My.session_wise_auto_serial(VoucherType + "_voucher", My.get_session(), My.firm_id());

                Alertme("Data added successfully", "success");
                reset_data();
                bind_gridview();
            }

            //}
        }

        protected void ddl_account_cr_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetch_particular();
        }

        private string find_txt_particular()
        {
            string qry = "Select Account_id from Account_Ledger_Details where Account_id='" + ddl_particular.Text + "';";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["Account_id"].ToString();
            else
                return "0";
        }





        #endregion pay
        double totalamt1 = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lbl_Amount = (Label)e.Row.FindControl("lbl_Amount");
                    totalamt1 = totalamt1 + My.toDouble(lbl_Amount.Text);
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lbl_total = (Label)e.Row.FindControl("lbl_total");
                    lbl_total.Text = totalamt1.ToString("0.00");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}