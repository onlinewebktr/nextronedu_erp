using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
using System.Transactions;

namespace school_web.Admin
{
    public partial class Add_Advance_Amount : System.Web.UI.Page
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
                        txt_date_new.Text = mycode.date();
                        Session["pageadv"] = "2";
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session_name, "select Session,session_id from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        ddl_session_name.SelectedValue = ddl_session.SelectedValue;

                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        Session["classchange"] = "2";
                        ViewState["Isbill_no_update"] = My.get_is_billl_no_modify();
                        
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
                    ViewState["Class_id"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["admissionserialnumber"] = dr["admissionserialnumber"].ToString();
                    ViewState["Addmission_no"] = dr["admissionserialnumber"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    ViewState["id"] = dr["id"].ToString();
                    ViewState["hosteltaken"] = dr["hosteltaken"].ToString();
                    lbl_old_roll_no.Text = dr["rollnumber"].ToString();

                    bind_payment_history();
                }
            }
        }
        protected void lnk_edit_bill_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
            }
            catch (Exception ex)
            {
            }
        }
        private void bind_payment_history()
        {
            pnl_payment_history.Visible = false;
            string query = "  select  *  from STUDENT_WALLET t1 where     t1.Adm_no='" + ViewState["admissionserialnumber"].ToString() + "' order by id desc";
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
        protected void lnk_delete_bill_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_slipno = (Label)row.FindControl("lbl_slipno");
            Label lbl_Addmission_no = (Label)row.FindControl("lbl_Addmission_no");
            Label lbl_Transection_in = (Label)row.FindControl("lbl_unique_entry_id");
            Label lbl_Amount = (Label)row.FindControl("lbl_Amount");
            ViewState["billamount"] = lbl_Amount.Text;
            ViewState["slip_no"] = lbl_slipno.Text;
            ViewState["admission_no"] = lbl_Addmission_no.Text;
            ViewState["transaction_id"] = lbl_Transection_in.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDeleteBill();", true);
        }
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
                    }

                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                if (flag == true)
                {
                    My.exeSql("delete from Account_Voucher_Details where unique_entry_id='" + ViewState["transaction_id"].ToString() + "' and VoucherType='Receipt'");
                    Alertme("Your selected bill no has been deleted sucessfully.", "success");
                    string remarks =  "Student Advance fee has been deleted  Amount:-"+ ViewState["billamount"].ToString()+" Slip No.-"+ ViewState["slip_no"]+" Admission mo.-"+ ViewState["admission_no"].ToString()+ " unique_entry_id  -"+ ViewState["transaction_id"].ToString();

                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Bill No :- " + ViewState["slip_no"].ToString() + "," + remarks );
                    bind_payment_history();
                }
            
             
        }
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
                 
              
                count = count + 1;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totalamount");
                lbl_totaldiscount.Text = total.ToString("0.00");
            }
        }

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
                else if (txt_amount.Text == "")
                {
                    Alertme("Please amount", "warning");
                    txt_amount.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
                    return;
                }
                else if (txt_inputremarks.Text == "")
                {
                    Alertme("Please enter remarks", "warning");
                    txt_inputremarks.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
                    return;
                }
                if (ddl_paymentmode.Text != "Cash")
                {
                    if (ddl_bank.Text == "Select")
                    {
                        Alertme("Please choose bank.", "warning");
                        ddl_bank.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
                        return;
                    }
                    if (txt_bank_date.Text == "")
                    {
                        Alertme("Please enter bank date.", "warning");
                        txt_bank_date.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
                        return;
                    }
                    if (txt_transaction_no.Text == "")
                    {
                        Alertme("Please enter transaction no.", "warning");
                        txt_transaction_no.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditBill();", true);
                        return;
                    }
                }
                bool final = true;
                if (final == true)
                {
                    double amount = My.toDouble(txt_amount.Text);
                    string VoucherType = "Receipt";
                    string unique_entry_id = My.unique_id_by_user(ViewState["Userid"].ToString());
                    string VoucherNo = "R" + My.session_wise_view_auto_serial(VoucherType + "_voucher", My.get_session(), My.firm_id());
                    bool flag = false;
                   
                       
                        SqlCommand cmd;
                        string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id,Old_Payment_date,New_Payment_Date,New_slip_no,Remark) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id,@Old_Payment_date,@New_Payment_Date,@New_slip_no,@Remark)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Current_admission_no", ViewState["Addmission_no"].ToString());
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                        cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                        cmd.Parameters.AddWithValue("@Change_type", "Add Advance Amount");
                        cmd.Parameters.AddWithValue("@Class_Id_New", ViewState["Class_id"].ToString());
                        cmd.Parameters.AddWithValue("@Roll_no_New", lbl_old_roll_no.Text);
                        cmd.Parameters.AddWithValue("@Slip_no", VoucherNo);
                        cmd.Parameters.AddWithValue("@New_Section", txtsection.Text);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Old_Payment_date", txt_date_new.Text);
                        cmd.Parameters.AddWithValue("@New_Payment_Date", txt_date_new.Text);
                        cmd.Parameters.AddWithValue("@New_slip_no", VoucherNo);
                        cmd.Parameters.AddWithValue("@Remark", txt_inputremarks.Text +" " +VoucherNo + " Amount :-" + amount);
                        if (My.InsertUpdateData(cmd))
                        {
                            
                            DateTime date = My.toDateTime(txt_date_new.Text + " " + mycode.time());
                            SqlCommand cmd2;
                            string query2 = "INSERT INTO Student_Wallet (Adm_no,Session_id,Wallet_input_amount,Wallet_Out_amount,Date_of_entry,Remakes,slipno,Mode,Add_type,Bank_name,Bank_date,Pay_mode_transaction_no,unique_entry_id,Created_By,Created_date) values (@Adm_no,@Session_id,@Wallet_input_amount,@Wallet_Out_amount,@Date_of_entry,@Remakes,@slipno,@Mode,@Add_type,@Bank_name,@Bank_date,@Pay_mode_transaction_no,@unique_entry_id,@Created_By,@Created_date)";
                            cmd2 = new SqlCommand(query2);
                            cmd2.Parameters.AddWithValue("@Adm_no", ViewState["Addmission_no"].ToString());
                            cmd2.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                            cmd2.Parameters.AddWithValue("@Wallet_input_amount", amount.ToString("0.00"));
                            cmd2.Parameters.AddWithValue("@Wallet_Out_amount","0.00");
                            cmd2.Parameters.AddWithValue("@Date_of_entry", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
                            cmd2.Parameters.AddWithValue("@Remakes", txt_inputremarks.Text);
                            cmd2.Parameters.AddWithValue("@slipno", VoucherNo);
                            cmd2.Parameters.AddWithValue("@Mode", ddl_paymentmode.Text);
                            cmd2.Parameters.AddWithValue("@Add_type", "Advance Pay");
                            cmd2.Parameters.AddWithValue("@Bank_name", ddl_bank.Text);
                            cmd2.Parameters.AddWithValue("@Bank_date", txt_bank_date.Text);
                            cmd2.Parameters.AddWithValue("@Pay_mode_transaction_no", txt_transaction_no.Text);
                            cmd2.Parameters.AddWithValue("@unique_entry_id", unique_entry_id);
                            cmd2.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                            cmd2.Parameters.AddWithValue("@Created_date", My.getdate1());
                            if (My.InsertUpdateData(cmd2))
                            {
                            }


                            string name = lbl_name.Text;
                            //string feeType = "Student Fee Payment";
                            double amountpaid = amount;
                            string Description = "Advance Fee collection from " + lbl_name.Text + " Amount : " + amountpaid.ToString() + "/-";
                            string PayDate = txt_date_new.Text + " " + mycode.time();
                            int Idate = My.DateConvertToIdate(txt_date_new.Text);
                            string alternetacc_id = ViewState["Addmission_no"].ToString();

                            string input = txt_date_new.Text;  // DD/MM/YYYY
                            string FNsession = My.GetFinancialSessionFromString(input);

                            string session_name = FNsession;
                           
                            if (ddl_paymentmode.Text.ToUpper() == "CASH")
                            {
                                My.send_to_cash_payment_Voucher_Details(alternetacc_id, "cash", amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL", "", VoucherNo, "");
                            }
                            else
                            {
                                string toponebank = My.get_bank_id(ddl_bank.Text);
                                My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL", "", VoucherNo, "");
                            }
                        My.session_wise_auto_serial(VoucherType + "_voucher", My.get_session(), My.firm_id());
                        ///=========================================
                        ///
                        flag = true;
                           
                        }
                 
                    if (flag == true)
                    {
                        Alertme("Advance amount has been added successfully.", "success");
                        My.send_data_to_user_log_history(ViewState["Userid"].ToString() + " Add amount date=" + txt_date_new.Text + ", bill no is " + VoucherNo + " and amount is -" + amount.ToString("0.00") + "unique id is-" + unique_entry_id + "has been added successfully.", ViewState["Userid"].ToString());
                        Response.Redirect("slip/Advance_Pay_slip.aspx?receiptid=" + VoucherNo, false);

                        bind_payment_history();

                    }
               }
                

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Update payment date");
            }
        }

        #endregion

    }
}