using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class bill_delete : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "bill-delete.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session_name, "select Session,session_id from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        ddl_session_name.SelectedValue = ddl_session.SelectedValue;

                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        Session["classchange"] = "2";
                        ViewState["Isbill_no_update"] = My.get_is_billl_no_modify();
                        if (ViewState["Isbill_no_update"].ToString() == "0")
                        {
                            slip_no.Visible = false;
                        }
                        else
                        {
                            slip_no.Visible = true;

                        }
                        try
                        {
                            DataTable dtBnk1 = My.dataTable("select * from bank_details wher  order by bank_name asc");
                            if (dtBnk1.Rows.Count > 0)
                            {
                                ddl_bank.DataSource = dtBnk1;
                                ddl_bank.DataTextField = "bank_name";
                                ddl_bank.DataBind();
                                ddl_bank.Items.Insert(0, new ListItem("Select", "Select"));
                            }
                            else
                            {
                                DataTable dtBnk = My.dataTable("select * from BANK_MASTER where Status='1' order by Bank_name asc");
                                if (dtBnk.Rows.Count > 0)
                                {
                                    ddl_bank.DataSource = dtBnk;
                                    ddl_bank.DataTextField = "Bank_name";
                                    ddl_bank.DataBind();
                                    ddl_bank.Items.Insert(0, new ListItem("Select", "Select"));
                                }
                                else
                                {
                                    compLN.bind_ddl_select(ddl_bank, "select Bank_name from Bank_master order by Bank_name asc");
                                }
                            }
                        }
                        catch
                        {
                        }

                        try
                        {
                            DataTable dtBnk1 = My.dataTable("select * from bank_details wher  order by bank_name asc");
                            if (dtBnk1.Rows.Count > 0)
                            {
                                ddl_bankRevised.DataSource = dtBnk1;
                                ddl_bankRevised.DataTextField = "bank_name";
                                ddl_bankRevised.DataBind();
                                ddl_bankRevised.Items.Insert(0, new ListItem("Select", "Select"));
                            }
                            else
                            {
                                DataTable dtBnk = My.dataTable("select * from BANK_MASTER where Status='1' order by Bank_name asc");
                                if (dtBnk.Rows.Count > 0)
                                {
                                    ddl_bankRevised.DataSource = dtBnk;
                                    ddl_bankRevised.DataTextField = "Bank_name";
                                    ddl_bankRevised.DataBind();
                                    ddl_bankRevised.Items.Insert(0, new ListItem("Select", "Select"));
                                }
                                else
                                {
                                    compLN.bind_ddl_select(ddl_bankRevised, "select Bank_name from Bank_master order by Bank_name asc");
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                }
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
        #region find student data
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    find_data();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    find_data();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void find_data()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                ddl_session.Focus();
                Alertme("Please select session.", "warning");
            }
            else if (txt_admission_no.Text == "")
            {
                Alertme("Please enter admission no.", "warning");
            }
            else
            {
                string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and  Status='1'  and Branch_id='" + ViewState["branchid"].ToString() + "'";
                find_details(query);
            }
        }

        private void find_details(string query)
        {
            pnl_payment_history.Visible = false;
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                pnl_payment_history.Visible = false;
                std_basic_infoS.Visible = false;
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    pnl_payment_history.Visible = false;
                    std_basic_infoS.Visible = true;
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToLower() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToLower() == "yes" ? "3" : "4";
                    lbltransporttion.Text = dr["transportationtaken"].ToString();
                    lbl_phone.Text = dr["mobilenumber"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["admissionserialnumber"] = dr["admissionserialnumber"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    ViewState["id"] = dr["id"].ToString();
                    ViewState["hosteltaken"] = dr["hosteltaken"].ToString();
                    lbl_old_roll_no.Text = dr["rollnumber"].ToString();

                    bind_payment_history();
                }
            }
        }

        private void bind_payment_history()
        {
            pnl_payment_history.Visible = false;
            string query = "  select t1.*  from Student_Payment_History t1 where t1.Class_id='" + ViewState["classid"].ToString() + "' and t1.Session='" + ViewState["session"].ToString() + "' and   t1.Addmission_no='" + ViewState["admissionserialnumber"].ToString() + "' order by id desc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_msg.Text = "There are no payment history found";
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                pnl_payment_history.Visible = true;
                lbl_msg.Text = "";
                grd_fee.DataSource = dt;
                grd_fee.DataBind();
            }
        }
        #endregion



        double total = 0;
        int count = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_payable.Text != "")
                {
                    total = total + Convert.ToDouble(lbl_payable.Text);
                }
                LinkButton btn_delete_bill = (LinkButton)e.Row.FindControl("lnk_delete_bill");
                LinkButton lnk_amount_edit = (LinkButton)e.Row.FindControl("lnk_amount_edit");
                if (count == 0)
                {
                    btn_delete_bill.Visible = true;
                    lnk_amount_edit.Visible = true;
                }
                else
                {
                    btn_delete_bill.Visible = false;
                    lnk_amount_edit.Visible = false;
                }
                count = count + 1;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totalamount");
                lbl_totaldiscount.Text = total.ToString("0.00");
            }
        }


        #region delete bill

        protected void lnk_delete_bill_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_slipno = (Label)row.FindControl("lbl_slipno");
            Label lbl_Addmission_no = (Label)row.FindControl("lbl_Addmission_no");
            Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
            Label lbl_Branchid = (Label)row.FindControl("lbl_Branchid");
            Label lbl_Session = (Label)row.FindControl("lbl_Session");
            Label lbl_Amount = (Label)row.FindControl("lbl_Amount");
            Label lbl_Type = (Label)row.FindControl("lbl_Type");
            Label lbl_Transection_in = (Label)row.FindControl("lbl_Transection_in");
            string type = lbl_Type.Text;
            ViewState["slip_no"] = lbl_slipno.Text;
            ViewState["admission_no"] = lbl_Addmission_no.Text;
            ViewState["class_id"] = lbl_Class_id.Text;
            ViewState["branch_id"] = lbl_Branchid.Text;
            ViewState["session"] = lbl_Session.Text;
            ViewState["bill_amount"] = lbl_Amount.Text;
            ViewState["bill_type"] = lbl_Type.Text;
            ViewState["transaction_id"] = lbl_Transection_in.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDeleteBill();", true);
        }

        private void check_all_head_dues(string admission_no, string session, SqlConnection con)
        {
            ///Monthly Fee Checking
            DataTable dtMonth = payments.dataTable("select Month from dbo.[Month_Index] order by Position desc", con);
            if (dtMonth.Rows.Count > 0)
            {
                foreach (DataRow dr in dtMonth.Rows)
                {
                    string qry = "select sum(convert(float, paid)) as TotalPaid from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and month='" + dr["Month"].ToString() + "'";
                    DataTable dt = payments.dataTable(qry, con);
                    if (dt.Rows.Count > 0)
                    {
                        if (My.toDouble(dt.Rows[0]["TotalPaid"].ToString()) <= 0)
                        {
                            payments.exeSql("delete from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and month='" + dr["Month"].ToString() + "'", con);
                        }
                    }
                }
            }

            ///Admission Fee Checking 
            string qryAd = "select sum(convert(float, paid)) as TotalPaid from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session + "' and (parameter='HostelAnnualFee' or parameter='AnnualFee' or parameter='AdmissionFee' or parameter='HostelAdmissionFee')";
            DataTable dtAD = payments.dataTable(qryAd, con);
            if (dtAD.Rows.Count > 0)
            {
                if (My.toDouble(dtAD.Rows[0]["TotalPaid"].ToString()) <= 0)
                {
                    payments.exeSql("delete from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session + "' and (parameter='HostelAnnualFee' or parameter='AnnualFee' or parameter='AdmissionFee' or parameter='HostelAdmissionFee')", con);
                }
            }
        }

        private void update_admission_fee(string Addmission_no, string Session, string Class_id, SqlConnection con)
        {
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + Addmission_no + "' and session='" + Session + "' and (parameter='AdmissionFee' or parameter='HostelAdmissionFee') and feetype!='Previous Dues' and class_id='" + Class_id + "'", con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_fee_collection where Admission_no='" + Addmission_no + "' and session='" + Session + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (My.toDouble(pdt.Rows[0]["paid"]) <= 0)
                    {
                        dr.Delete();
                        payments.exeSql("update dbo.[admission_registor] set payment_status='Unpaid' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "'", con);
                    }
                    else
                    {
                        dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString());
                        dr[6] = My.toDouble(dr[4]) - My.toDouble(dr[5]);
                        payments.exeSql("update dbo.[admission_registor] set payment_status='Dues' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "'", con);
                    }
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }


        private void update_annual_fee(string Addmission_no, string Session, string Class_id, SqlConnection con)
        {
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + Addmission_no + "' and session='" + Session + "' and (parameter='AnnualFee' or parameter='HostelAnnualFee') and feetype!='Previous Dues' and class_id='" + Class_id + "'", con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + Addmission_no + "' and session='" + Session + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (My.toDouble(pdt.Rows[0]["paid"]) <= 0)
                    {
                        dr.Delete();
                        payments.exeSql("update dbo.[admission_registor] set payment_status='Unpaid' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "'", con);
                    }
                    else
                    {
                        dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString());
                        dr[6] = My.toDouble(dr[4]) - My.toDouble(dr[5]);
                        payments.exeSql("update dbo.[admission_registor] set payment_status='Dues' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "'", con);
                    }
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        #endregion




        #region final update
        protected void btn_update_final_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_date_new.Text == "")
                {
                    Alertme("Please enter date.", "warning");
                    txt_date_new.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
                    return;
                }
                //if (ddl_paymentmode.Text != "Cash")
                //{
                //    if (ddl_bank.Text == "Select")
                //    {
                //        Alertme("Please choose bank.", "warning");
                //        ddl_bank.Focus();
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
                //        return;
                //    }
                //    if (txt_bank_date.Text == "")
                //    {
                //        Alertme("Please enter bank date.", "warning");
                //        txt_bank_date.Focus();
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
                //        return;
                //    }
                //    if (txt_transaction_no.Text == "")
                //    {
                //        Alertme("Please enter transaction no.", "warning");
                //        txt_transaction_no.Focus();
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
                //        return;
                //    }
                //}
                bool final = false;
                string newslipno = ViewState["slipno"].ToString();
                if (ViewState["Isbill_no_update"].ToString() == "1")
                {
                    bool check_bill_dublicate = My.verify_bill_no(txt_slip_no_popup.Text.Trim(), ViewState["id"].ToString());
                    if (check_bill_dublicate == true)
                    {
                        newslipno = txt_slip_no_popup.Text.Trim();
                        final = true;
                    }
                    else
                    {
                        final = false;
                    }
                }
                else
                {
                    final = true;

                }


                if (final == true)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id,Old_Payment_date,New_Payment_Date,New_slip_no) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id,@Old_Payment_date,@New_Payment_Date,@New_slip_no)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Current_admission_no", ViewState["Addmission_no"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                    cmd.Parameters.AddWithValue("@Change_type", "Edit Bill");
                    cmd.Parameters.AddWithValue("@Class_Id_New", ViewState["Class_id"].ToString());
                    cmd.Parameters.AddWithValue("@Roll_no_New", lbl_old_roll_no.Text);
                    cmd.Parameters.AddWithValue("@Slip_no", ViewState["slipno"].ToString());
                    cmd.Parameters.AddWithValue("@New_Section", txtsection.Text);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Old_Payment_date", ViewState["olddate"].ToString());
                    cmd.Parameters.AddWithValue("@New_Payment_Date", txt_date_new.Text);
                    cmd.Parameters.AddWithValue("@New_slip_no", newslipno);
                    if (My.InsertUpdateData(cmd))
                    {
                        mycode.executequery("update Monthly_Fee_Collection_Slip set Date='" + txt_date_new.Text + "',Idate='" + mycode.ConvertStringToiDateup(txt_date_new.Text) + "',slipno='" + newslipno + "' where adno='" + ViewState["Addmission_no"] + "' and class='" + ViewState["Class_id"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and slipno='" + ViewState["slipno"].ToString() + "'");
                        if (ddl_paymentmode.Text == "Cash")
                        {
                            mycode.executequery("update Student_Payment_History set Date='" + txt_date_new.Text + "',Idate='" + mycode.ConvertStringToiDateup(txt_date_new.Text) + "',mode='" + ddl_paymentmode.Text + "',Bank_name='',Bank_date='',Pay_mode_transaction_no='',Slip_no='" + newslipno + "',Remarks='" + txt_remark_update.Text + "' where Addmission_no='" + ViewState["Addmission_no"].ToString() + "' and Session='" + ViewState["Session"].ToString() + "' and Class_id='" + ViewState["Class_id"].ToString() + "' and Slip_no='" + ViewState["slipno"].ToString() + "'");
                        }
                        else
                        {
                            mycode.executequery("update Student_Payment_History set Date='" + txt_date_new.Text + "',Idate='" + mycode.ConvertStringToiDateup(txt_date_new.Text) + "',mode='" + ddl_paymentmode.Text + "',Bank_name='" + ddl_bank.Text + "',Bank_date='" + txt_bank_date.Text + "',Pay_mode_transaction_no='" + txt_transaction_no.Text + "',Slip_no='" + newslipno + "',Remarks='" + txt_remark_update.Text + "' where Addmission_no='" + ViewState["Addmission_no"].ToString() + "' and Session='" + ViewState["Session"].ToString() + "' and Class_id='" + ViewState["Class_id"].ToString() + "'  and Slip_no='" + ViewState["slipno"].ToString() + "'");
                        }
                        ///=========================================
                        My.exeSql("delete from Account_Voucher_Details where VoucherNo_Manual='" + ViewState["slipno"].ToString() + "' and VoucherType='Receipt'");
                        string name = lbl_name.Text;
                        string unique_entry_id = My.unique_id();
                        string VoucherNo = newslipno;//ViewState["slipno"].ToString();
                        string feeType = "Student Fee Payment";
                        double amountpaid = My.toDouble(ViewState["BillAmt"].ToString());
                        string VoucherType = "Receipt";
                        string Description = "Fee collection from " + lbl_name.Text + " Amount : " + amountpaid + "/-";
                        string PayDate = txt_date_new.Text + " " + mycode.time();
                        int Idate = My.DateConvertToIdate(txt_date_new.Text);
                        string alternetacc_id = ViewState["Addmission_no"].ToString();

                        string input = txt_date_new.Text;  // DD/MM/YYYY
                        string FNsession = My.GetFinancialSessionFromString(input);

                        string session_name = FNsession;
                        bool checkbiilentery = My.check_dup_bill_no_entry(VoucherNo, session_name);
                        if (ddl_paymentmode.Text.ToUpper() == "CASH")
                        {
                            My.send_to_cash_payment_Voucher_Details(alternetacc_id, "cash", amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo, "");
                        }
                        else
                        {
                            string toponebank = My.get_bank_id(ddl_bank.Text);
                            My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo, "");
                        }
                        ///=========================================
                        ///
                        mycode.executequery("update SchoolLedger set Date='" + txt_date_new.Text + "',IDate='" + mycode.ConvertStringToiDateup(txt_date_new.Text) + "',TransactionId='" + VoucherNo + "' where Addmission_no='" + ViewState["Addmission_no"].ToString() + "' and TransactionId='" + ViewState["slipno"].ToString() + "'");
                        Alertme("Bill details has been updated sucessfully.", "success");
                        My.send_data_to_user_log_history(ViewState["Userid"].ToString() + " Bill No :- " + ViewState["slipno"].ToString() + ", Old Date= " + ViewState["olddate"].ToString() + " to New date=" + txt_date_new.Text + ", New bill no is " + VoucherNo + " has been updated successfully.", ViewState["Userid"].ToString());
                        bind_payment_history();
                    }
                }
                else
                {
                    Alertme("Sorry your bill no is duplicate. Please change bill no", "warning");
                    txt_transaction_no.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
                    return;
                }

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Update payment date");
            }
        }

        #endregion


        #region update paymentdate
        protected void lnk_edit_bill_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_slipno = (Label)row.FindControl("lbl_slipno");
                Label lbl_Addmission_no = (Label)row.FindControl("lbl_Addmission_no");
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Branchid = (Label)row.FindControl("lbl_Branchid");
                Label lbl_Session = (Label)row.FindControl("lbl_Session");
                Label lbl_date = (Label)row.FindControl("lbl_date");
                Label lbl_paymenetmode = (Label)row.FindControl("lbl_paymenetmode");
                Label lbl_Amount = (Label)row.FindControl("lbl_Amount");
                Label lbl_bank_name = (Label)row.FindControl("lbl_bank_name");
                Label lbl_bank_date = (Label)row.FindControl("lbl_bank_date");
                Label lbl_transaction_no = (Label)row.FindControl("lbl_transaction_no");
                Label lbl_remarkss = (Label)row.FindControl("lbl_remarkss");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                ViewState["slipno"] = lbl_slipno.Text;
                ViewState["Addmission_no"] = lbl_Addmission_no.Text;
                ViewState["Class_id"] = lbl_Class_id.Text;
                ViewState["Branchid"] = lbl_Branchid.Text;
                ViewState["Session"] = lbl_Session.Text;
                ViewState["olddate"] = lbl_date.Text;
                ViewState["BillAmt"] = lbl_Amount.Text;
                ViewState["id"] = lbl_id.Text;



                txt_slip_no_popup.Text = lbl_slipno.Text;
                try
                {
                    ddl_paymentmode.Text = lbl_paymenetmode.Text;
                }
                catch (Exception ex)
                {
                }

                txt_date_new.Text = lbl_date.Text;

                try
                {
                    ddl_bank.Text = lbl_bank_name.Text;
                }
                catch (Exception ex)
                {
                }

                txt_bank_date.Text = lbl_bank_date.Text;
                txt_transaction_no.Text = lbl_transaction_no.Text;
                txt_remark_update.Text = lbl_remarkss.Text;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        protected void btn_conf_delete_Click(object sender, EventArgs e)
        {
            try
            {
                conf_delete_Bill();
            }
            catch (Exception ex)
            {
            }
        }

        private void conf_delete_Bill()
        {
            if (ViewState["bill_type"] != null)
            {
                SqlCommand cmd;
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();

                    string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id,Remark) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id,@Remark)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Current_admission_no", ViewState["admission_no"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                    cmd.Parameters.AddWithValue("@Change_type", ViewState["bill_type"].ToString() + " Fees Delete");
                    cmd.Parameters.AddWithValue("@Class_Id_New", ViewState["class_id"].ToString());
                    cmd.Parameters.AddWithValue("@Roll_no_New", lbl_old_roll_no.Text);
                    cmd.Parameters.AddWithValue("@Slip_no", ViewState["slip_no"].ToString());
                    cmd.Parameters.AddWithValue("@New_Section", txtsection.Text);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Remark", txt_remark.Text);
                    if (payments.InsertUpdateData(cmd, con))
                    {
                        string qery = @"INSERT INTO Student_Payment_History_Save_bakup (Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,insert_time_date,Insert_time_user_id,New_Slip_no,Remark)
                    SELECT Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,'" + My.getdate1() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["slip_no"].ToString() + "','" + txt_remark.Text + "' FROM Student_Payment_History where Addmission_no='" + ViewState["admission_no"].ToString() + "' and Session='" + ViewState["session"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "' and Branch='" + ViewState["branchid"].ToString() + "' and Slip_no='" + ViewState["slip_no"].ToString() + "'";
                        payments.exeSql(qery, con);

                        string qeryM = @"INSERT INTO Deleted_history_headwise (adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,Previous_admission_no,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate,Transport_Boarding_Point_id,Transportation_Id,TransportationPath_id,Hostel_id,Room_category,Created_date,Created_idate,Delete_by,Delete_date,Delete_time)
                    SELECT adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,Previous_admission_no,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate,Transport_Boarding_Point_id,Transportation_Id,TransportationPath_id,Hostel_id,Room_category,Created_date,Created_idate,'" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "' FROM Monthly_Fee_Collection_Slip where adno='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and branchid='" + ViewState["branchid"].ToString() + "' and slipno='" + ViewState["slip_no"].ToString() + "'";
                        payments.exeSql(qeryM, con);


                        #region update dues amount 
                        string isAdmission = "0";


                        DataTable dtMF = payments.dataTable("select * from Monthly_Fee_Collection_Slip where adno='" + ViewState["admission_no"].ToString() + "' and class='" + ViewState["class_id"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and slipno='" + ViewState["slip_no"].ToString() + "'  order by id desc", con);
                        if (dtMF.Rows.Count > 0)
                        {
                            foreach (DataRow drMF in dtMF.Rows)
                            {
                                double total_amt = My.toDouble(drMF["paid"].ToString());
                                string qry11 = "select * from Typewise_fee_collection where admission_no='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "'  and content_id='" + drMF["content_id"].ToString() + "' and month='" + drMF["Month"].ToString() + "'";
                                SqlDataAdapter ad = new SqlDataAdapter(qry11, con);
                                DataSet ds = new DataSet();
                                ad.Fill(ds, "Typewise_fee_collection");
                                DataTable tdt = ds.Tables[0];
                                if (tdt.Rows.Count == 0)
                                {
                                }
                                else
                                {
                                    foreach (DataRow dr in tdt.Rows)
                                    {
                                        double dues_paid = My.toDouble(dr["paid"].ToString());
                                        if (total_amt >= dues_paid)
                                        {
                                            dr["paid"] = "0";
                                            dr["dues"] = dr["Payable_after_disc"];
                                            dr["status"] = "Dues";
                                        }
                                        else
                                        {
                                            dr["paid"] = My.toDouble(dr["paid"]) - total_amt;
                                            dr["dues"] = My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"]);
                                            dr["status"] = "Dues";
                                            break;
                                        }
                                    }
                                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                    ad.Update(tdt);
                                }
                            }
                        }


                        //=========================
                        if (isAdmission == "ADM")
                        {
                            update_admission_fee(ViewState["admission_no"].ToString(), ViewState["session"].ToString(), ViewState["class_id"].ToString(), con);
                        }
                        if (isAdmission == "ANN")
                        {
                            update_annual_fee(ViewState["admission_no"].ToString(), ViewState["session"].ToString(), ViewState["class_id"].ToString(), con);
                        }
                        payments.exeSql("delete from Monthly_Fee_Collection_Slip where adno='" + ViewState["admission_no"].ToString() + "' and class='" + ViewState["class_id"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and slipno='" + ViewState["slip_no"].ToString() + "';delete from Student_Payment_History where  Addmission_no='" + ViewState["admission_no"].ToString() + "' and Session='" + ViewState["session"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'  and Slip_no='" + ViewState["slip_no"].ToString() + "';delete from SchoolLedger where Addmission_no='" + ViewState["admission_no"].ToString() + "' and TransactionId='" + ViewState["slip_no"].ToString() + "';delete from Account_Voucher_Details where VoucherNo='" + ViewState["slip_no"].ToString() + "'; delete from Discount_master_report where Bill_no='" + ViewState["slip_no"].ToString() + "'", con);
                        check_all_head_dues(ViewState["admission_no"].ToString(), ViewState["session"].ToString(), con);
                        dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), ViewState["admission_no"].ToString(), "0", "0", con);
                        #endregion
                    }

                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                if (flag == true)
                {
                    My.exeSql("delete from Account_Voucher_Details where VoucherNo_Manual='" + ViewState["slip_no"].ToString() + "' and VoucherType='Receipt'");
                    Alertme("Your selected bill no has been deleted sucessfully.", "success");
                    string remarks = ViewState["bill_type"].ToString() + " Fees Delete";
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Bill No :- " + ViewState["slip_no"].ToString() + "," + remarks + " has been deleted successfully.");
                    bind_payment_history();
                }
                // }
            }
        }


        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["studentname"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]
        public static List<string> GetRooPathAdmNo(string PathRooT, string Session_id)
        {
            string sessionid = Session_id;
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["admissionserialnumber"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion


        protected void btn_find_by_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session_name.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }

                string stdname = txt_student_name.Text.Trim();
                string query = "select * from admission_registor where studentname like '%" + stdname + "%' and session='" + ddl_session_name.SelectedItem.Text + "' and  Status='1' order by id asc";

                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else if (dt.Rows.Count == 1)
                {
                    query = "select * from admission_registor where admissionserialnumber='" + dt.Rows[0]["admissionserialnumber"].ToString() + "' and Session='" + ddl_session_name.SelectedItem.Text + "' order by studentname asc";
                    find_details(query);
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "studentInfo();", true);
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "'";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
        protected void lnk_amount_edit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_slipno = (Label)row.FindControl("lbl_slipno");
                Label lbl_Addmission_no = (Label)row.FindControl("lbl_Addmission_no");
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Branchid = (Label)row.FindControl("lbl_Branchid");
                Label lbl_Session = (Label)row.FindControl("lbl_Session");
                Label lbl_date = (Label)row.FindControl("lbl_date");
                Label lbl_paymenetmode = (Label)row.FindControl("lbl_paymenetmode");
                Label lbl_Amount = (Label)row.FindControl("lbl_Amount");
                Label lbl_bank_name = (Label)row.FindControl("lbl_bank_name");
                Label lbl_bank_date = (Label)row.FindControl("lbl_bank_date");
                Label lbl_transaction_no = (Label)row.FindControl("lbl_transaction_no");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                ViewState["date"] = lbl_date.Text;
                ViewState["slipno"] = lbl_slipno.Text;
                ViewState["Addmission_no"] = lbl_Addmission_no.Text;
                ViewState["Class_id"] = lbl_Class_id.Text;
                ViewState["Branchid"] = lbl_Branchid.Text;
                ViewState["Session"] = lbl_Session.Text;
                ViewState["olddate"] = lbl_date.Text;
                ViewState["BillAmt"] = lbl_Amount.Text;
                ViewState["id"] = lbl_id.Text;

                ViewState["RvSlipId"] = lbl_slipno.Text;
                ViewState["id"] = lbl_id.Text;


                try
                {
                    ddl_paymentmodeRevised.Text = lbl_paymenetmode.Text;
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ddl_bankRevised.Text = lbl_bank_name.Text;
                }
                catch (Exception ex)
                {
                }

                txt_bank_dateRevised.Text = lbl_bank_date.Text;
                txt_transaction_noRevised.Text = lbl_transaction_no.Text;

                fetch_bill_details(ViewState["slipno"].ToString(), ViewState["sessionIDs"].ToString(), ViewState["Session"].ToString(), ViewState["Class_id"].ToString(), ViewState["Addmission_no"].ToString());

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRevised();", true);
            }
            catch (Exception ex)
            {
            }
        }
        private void fetch_bill_details(string slip_id_last, string v1, string v2, string v3, string v4)
        {
            lbl_rv_slip_id.Text = slip_id_last;
            ViewState["RvSlipId"] = slip_id_last;
            DataTable dt = My.dataTable("select * from Typewise_fee_collection where parameter='" + ViewState["parameter"].ToString() + "' and transection='" + slip_id_last + "'");
            if (dt.Rows.Count > 0)
            {
                rp_revised.DataSource = dt;
                rp_revised.DataBind();
            }
        }

        double rv_payble_amt = 0; double rv_disc_amt = 0; double rv_payble_aftr_disc_amt = 0; double rv_paid_amt = 0; double rv_dues_amt = 0;
        protected void rp_revised_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_payble_amt = ((Label)e.Item.FindControl("lbl_payble_amt")) as Label;
                Label lbl_disc_amt = ((Label)e.Item.FindControl("lbl_disc_amt")) as Label;
                Label lbl_payble_after_disc = ((Label)e.Item.FindControl("lbl_payble_after_disc")) as Label;
                Label lbl_paid = ((Label)e.Item.FindControl("lbl_paid")) as Label;
                Label lbl_dues = ((Label)e.Item.FindControl("lbl_dues")) as Label;

                rv_payble_amt = rv_payble_amt + My.toDouble(lbl_payble_amt.Text);
                rv_disc_amt = rv_disc_amt + My.toDouble(lbl_disc_amt.Text);
                rv_payble_aftr_disc_amt = rv_payble_aftr_disc_amt + My.toDouble(lbl_payble_after_disc.Text);
                rv_paid_amt = rv_paid_amt + My.toDouble(lbl_paid.Text);
                rv_dues_amt = rv_dues_amt + My.toDouble(lbl_dues.Text);
            }
            lbl_rv_ttl_payble_amt.Text = rv_payble_amt.ToString("0.00");
            lbl_rv_ttl_disc_amt.Text = rv_disc_amt.ToString("0.00");
            lbl_rv_ttl_payble_after_disc.Text = rv_payble_aftr_disc_amt.ToString("0.00");
            lbl_rv_ttl_paid.Text = rv_paid_amt.ToString("0.00");
            lbl_rv_ttl_dues.Text = rv_dues_amt.ToString("0.00");

            txt_rd_ttl_amt.Text = rv_payble_aftr_disc_amt.ToString("0.00");
            txt_rd_paid_amt.Text = rv_paid_amt.ToString("0.00");
            txt_rv_dues_amt.Text = rv_dues_amt.ToString("0.00");
        }

        protected void txt_rd_paid_amt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRevised();", true);
                double duesamtS = My.toDouble(txt_rd_ttl_amt.Text) - My.toDouble(txt_rd_paid_amt.Text);
                if (duesamtS > 0)
                {
                    txt_rv_dues_amt.Text = duesamtS.ToString();
                }
                else
                {
                    txt_rv_dues_amt.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btn_revised_pay_Click(object sender, EventArgs e)
        {
            try
            {


                bool flag = false;
                if (btn_revised_pay.Text == "Submit")
                {

                    if (ddl_paymentmodeRevised.Text != "Cash")
                    {
                        if (ddl_bankRevised.Text == "Select")
                        {
                            Alertme("Please choose bank.", "warning");
                            ddl_bankRevised.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRevised();", true);
                            return;
                        }
                        if (txt_bank_dateRevised.Text == "")
                        {
                            Alertme("Please enter bank date.", "warning");
                            txt_bank_dateRevised.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRevised();", true);
                            return;
                        }
                        if (txt_transaction_noRevised.Text == "")
                        {
                            Alertme("Please enter transaction no.", "warning");
                            txt_transaction_no.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRevised();", true);
                            return;
                        }
                    }
                    if (My.toDouble(txt_rd_paid_amt.Text) > 0)
                    {
                        if (txt_rv_remark.Text == "")
                        {
                            txt_rv_remark.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRevised();", true);
                            Alertme("Please enter remarks.", "warning");
                        }
                        else
                        {
                            ViewState["monthRevised"] = get_revised_month(ViewState["slipno"].ToString());
                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                            {
                                SqlConnection con = new SqlConnection(My.conn);
                                con.Open();
                                revised_payment(con);
                                if (ViewState["isSucceSSmsG"].ToString() == "1")
                                {
                                    string description = "Monthly fee collection for " + lbl_name.Text + " Month " + ViewState["monthRevised"].ToString() + ", Paid Amount : " + txt_rd_paid_amt.Text + " /-";
                                    payments.exeSql("update Student_Payment_History set Amount='" + txt_rd_paid_amt.Text + "',Description='" + description + "' where Session='" + ViewState["session"].ToString() + "' and Slip_no='" + ViewState["slipno"].ToString() + "'; insert into Revised_payment_history(Slip_id,Payble_amt,Old_payment,New_payment,Remark,Created_by,Created_date,Created_time) values ('" + ViewState["slipno"].ToString() + "','" + txt_rd_ttl_amt.Text + "','" + lbl_rv_ttl_paid.Text + "','" + txt_rd_paid_amt.Text + "','" + txt_rv_remark.Text + "','" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "');", con);


                                    SqlCommand cmd;
                                    string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id,Old_Payment_date,New_Payment_Date,New_slip_no) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id,@Old_Payment_date,@New_Payment_Date,@New_slip_no)";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@Current_admission_no", ViewState["Addmission_no"].ToString());
                                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                                    cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                                    cmd.Parameters.AddWithValue("@Change_type", "Edit Bill");
                                    cmd.Parameters.AddWithValue("@Class_Id_New", ViewState["Class_id"].ToString());
                                    cmd.Parameters.AddWithValue("@Roll_no_New", lbl_old_roll_no.Text);
                                    cmd.Parameters.AddWithValue("@Slip_no", ViewState["slipno"].ToString());
                                    cmd.Parameters.AddWithValue("@New_Section", txtsection.Text);
                                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                    cmd.Parameters.AddWithValue("@Old_Payment_date", ViewState["olddate"].ToString());
                                    cmd.Parameters.AddWithValue("@New_Payment_Date", txt_date_new.Text);
                                    cmd.Parameters.AddWithValue("@New_slip_no", ViewState["slipno"].ToString());
                                    if (payments.InsertUpdateData(cmd, con))
                                    {
                                        dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["Class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                                    }


                                }

                                flag = true;
                                con.Close();
                                scope.Complete();
                            }
                            if (flag == true)
                            {
                                My.exeSql("delete from Account_Voucher_Details where VoucherNo_Manual='" + ViewState["slipno"].ToString() + "' and VoucherType='Receipt'");
                                string name = lbl_name.Text;
                                string unique_entry_id = My.unique_id();
                                string VoucherNo = ViewState["slipno"].ToString();//ViewState["slipno"].ToString();
                                string feeType = "Student Fee Payment";
                                double amountpaid = My.toDouble(txt_rd_paid_amt.Text);
                                string VoucherType = "Receipt";
                                string Description = "Fee collection from " + lbl_name.Text + " Amount : " + amountpaid + "/-";
                                string PayDate = ViewState["olddate"].ToString() + " " + mycode.time();
                                int Idate = My.DateConvertToIdate(ViewState["olddate"].ToString());
                                string alternetacc_id = ViewState["Addmission_no"].ToString();

                                string input = ViewState["olddate"].ToString();  // DD/MM/YYYY
                                string FNsession = My.GetFinancialSessionFromString(input);

                                string session_name = FNsession;
                                bool checkbiilentery = My.check_dup_bill_no_entry(VoucherNo, session_name);
                                if (ddl_paymentmode.Text.ToUpper() == "CASH")
                                {
                                    My.send_to_cash_payment_Voucher_Details(alternetacc_id, "cash", amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo, "");
                                }
                                else
                                {
                                    string toponebank = My.get_bank_id(ddl_bank.Text);
                                    My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo, "");
                                }


                                Alertme("Bill amount has been modify sucessfully.", "success");
                                My.send_data_to_user_log_history(ViewState["Userid"].ToString() + " Bill No :- " + ViewState["slipno"].ToString() + ", Old Date=" + mycode.date() + " to New date=" + mycode.date() + " has been modify successfully.", ViewState["Userid"].ToString());
                                bind_payment_history();


                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRevised();", true);
                        Alertme("Please enter payment amount.", "warning");
                    }

                }
            }
            catch (Exception ex)
            {
                Alertme("Somthing is wrong", "warning");
            }

        }
        private object get_revised_month(string slip_id_last)
        {
            string months = "";
            DataTable dt = My.dataTable("select DISTINCT month from Typewise_fee_collection where parameter='" + ViewState["parameter"].ToString() + "' and transection='" + slip_id_last + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    months = months + dr["month"].ToString() + ",";
                }
            }
            months = months.Remove(months.Length - 1);
            return months;
        }
        private void revised_payment(SqlConnection con)
        {


            payments.exeSql("update Typewise_fee_collection set paid='0', dues='0' where admission_no='" + ViewState["Addmission_no"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and transection='" + ViewState["RvSlipId"].ToString() + "' and parameter like '%" + ViewState["parameter"].ToString() + "%'", con);

            double paid_amount = My.toDouble(txt_rd_paid_amt.Text);
            string parameter = "", month = "", month_position = "";
            string qry = " select *  from Typewise_fee_collection  where admission_no='" + ViewState["Addmission_no"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and transection='" + ViewState["RvSlipId"].ToString() + "' and parameter like '%" + ViewState["parameter"].ToString() + "%' order by cast(Position as float)";

            SqlDataAdapter ad = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Typewise_fee_collection");
            DataTable tdt = ds.Tables[0];
            if (tdt.Rows.Count == 0)
            {
            }
            else
            {
                month_position = tdt.Rows[0]["position"].ToString();
                string prev_month = "";
                foreach (DataRow dr in tdt.Rows)
                {
                    payments.exeSql("delete from Monthly_Fee_Collection_Slip where adno='" + ViewState["Addmission_no"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and parameter='" + dr["parameter"].ToString() + "' and Month='" + dr["month"].ToString() + "' and content_id='" + dr["content_id"].ToString() + "'", con);
                    if (paid_amount >= 0)
                    {
                        month = dr["month"].ToString();
                        parameter = dr["parameter"].ToString();
                        double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);

                        if (paid_amount >= dues) // && paid_amount > 0
                        {
                            string prevpaid = My.toDouble(dr["paid"].ToString()).ToString("0.00");
                            paid_amount = paid_amount - dues;
                            //paid amt is gratter than dues so dues amt is actual paid.
                            string paid = dr["dues"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(dr["Payable_after_disc"]).ToString("0.00");
                            dr["dues"] = "0";
                            dr["status"] = "Paid";
                            //#region send in collection slip
                            send_data_in_fee_collection__monthly_slip(dr["payable"].ToString(), dues.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], ViewState["RvSlipId"].ToString(), dr["Ledger"].ToString(), dr["admission_no"].ToString(), dr["Session"].ToString(), dr["class_id"].ToString(), dr["section"].ToString(), dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, dr["Date"].ToString(), dr["idate"].ToString(), con);
                            //#endregion
                            ViewState["isSucceSSmsG"] = "1";
                        }
                        else
                        {
                            if (paid_amount > 0 || (prev_month != "" && prev_month == month))
                            {
                                string prevpaid = dr["paid"].ToString();
                                dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                                dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + paid_amount).ToString("0.00");
                                dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                                if (My.toDouble(dr["dues"]) <= 0)
                                {
                                    dr["status"] = "Paid";
                                }
                                else
                                {
                                    dr["status"] = "Dues";
                                }
                                //#region send in collection slip
                                send_data_in_fee_collection__monthly_slip(dr["payable"].ToString(), paid_amount.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], ViewState["RvSlipId"].ToString(), dr["Ledger"].ToString(), dr["admission_no"].ToString(), dr["Session"].ToString(), dr["class_id"].ToString(), dr["section"].ToString(), dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, dr["Date"].ToString(), dr["idate"].ToString(), con);
                                //#endregion 
                                paid_amount = 0;
                                ViewState["isSucceSSmsG"] = "1";
                            }
                            else
                            {
                                break;
                            }
                        }
                        prev_month = month;
                    }
                    else
                    {
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(tdt);
            }

        }

        private void send_data_in_fee_collection__monthly_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, string date, string idate, SqlConnection con)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,Date,Idate,branchid) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + My.toDouble(payable).ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + My.toDouble(prevpaid).ToString("0.00") + "','" + date + "','" + idate + "','" + ViewState["Branchid"].ToString() + "');";
            payments.exeSql(qry, con);
        }
    }
}