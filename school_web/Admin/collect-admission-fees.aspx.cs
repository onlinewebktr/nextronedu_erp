using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class collect_admission_fees : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
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
                    if (!IsPostBack)
                    {
                        if (Session["msg"] != null && Session["msg"] != "")
                        {
                            Alertme(Session["msg"].ToString(), "success");
                            Session["msg"] = null;
                        }

                        ViewState["Userid"] = Session["Admin"].ToString();
                        try
                        {
                            hd_user_Type.Value = My.get_user_type(ViewState["Userid"].ToString());
                        }
                        catch (Exception ex)
                        {
                        }
                        Session["reprintadmission"] = "101";
                        txt_date.Text = mycode.date();
                        ViewState["adjestamount"] = "0";
                        ViewState["firm_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        find_firm_details();
                        DataTable dtBnk = My.dataTable("select * from BANK_MASTER where Status='1' order by Bank_name asc");
                        if (dtBnk.Rows.Count > 0)
                        {
                            ddl_bank.DataSource = dtBnk;
                            ddl_bank.DataTextField = "Bank_name";
                            ddl_bank.DataBind();
                            ddl_bank.Items.Insert(0, new ListItem("Select", "Select"));
                        }
                        else { compLN.bind_ddl_select(ddl_bank, "select Bank_name from Bank_master order by Bank_name asc"); }


                        if (Request.QueryString["admissionno"] != null)
                        {
                            ViewState["admissionno"] = Request.QueryString["admissionno"];
                            ViewState["sessionid"] = Request.QueryString["sessionid"];
                            ViewState["classid"] = Request.QueryString["classid"];
                            ViewState["session"] = mycode.get_session(ViewState["sessionid"].ToString());
                        }
                        get_student_details();
                        Bind_fee_details();
                        find_all_paid_fee();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Type_Master");
            }
        }


        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (hd_user_Type.Value == "Admin")
                    {
                        txt_date.Enabled = true;
                        txt_date.CssClass = "form-control find-dv-txtbx";
                        paydateDVS.Attributes.Add("class", "col-md-3");

                        //====
                        txt_date.Enabled = true;
                        txt_date.CssClass = "calender-icon form-control find-dv-txtbx";
                        admpayDateDV.Attributes.Add("class", "col-md-3");
                    }
                    else
                    {
                        if (dt.Rows[0]["Is_user_change_pay_date"].ToString() == "True")
                        {
                            txt_date.Enabled = false;
                            txt_date.CssClass = "form-control find-dv-txtbx noclick";
                            paydateDVS.Attributes.Add("class", "col-md-3  noclick");

                            //====
                            txt_date.Enabled = false;
                            txt_date.CssClass = "calender-icon form-control find-dv-txtbx noclick";
                            admpayDateDV.Attributes.Add("class", "col-md-3  noclick");
                        }
                        else
                        {
                            txt_date.Enabled = true;
                            txt_date.CssClass = "form-control find-dv-txtbx";
                            paydateDVS.Attributes.Add("class", "col-md-3");

                            //====
                            txt_date.Enabled = true;
                            txt_date.CssClass = "calender-icon form-control find-dv-txtbx";
                            admpayDateDV.Attributes.Add("class", "col-md-3");
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void get_student_details()
        {
            DataTable dt = mycode.FillData("select * from admission_registor where Session_id='" + ViewState["sessionid"].ToString() + "' and admissionserialnumber='" + ViewState["admissionno"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                ViewState["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
                lbl_admissionno.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_studentname.Text = dt.Rows[0]["studentname"].ToString();
                lbl_section.Text = dt.Rows[0]["Section"].ToString();

                lbl_session.Text = dt.Rows[0]["session"].ToString();
                lbl_class.Text = dt.Rows[0]["class"].ToString();
                ViewState["Section"] = dt.Rows[0]["Section"].ToString();
                ViewState["category_id"] = dt.Rows[0]["Category_id"].ToString();
                ViewState["SubCategory_id"] = dt.Rows[0]["SubCategory_id"].ToString();
                ViewState["rollnumber"] = dt.Rows[0]["rollnumber"].ToString();

                Image2.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
                if (dt.Rows[0]["studentimagepath"].ToString() == "")
                {
                    Image2.Visible = false;
                }
                else
                {
                    Image2.Visible = true;
                }

                if (dt.Rows[0]["hosteltaken"].ToString() == "")
                {
                    ViewState["hostaltaken"] = "No";
                    lbl_student_type.Text = "Day Scholer";
                }
                else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                {
                    ViewState["hostaltaken"] = "No";
                    lbl_student_type.Text = "Day Scholer";
                }
                else
                {
                    lbl_student_type.Text = "Hostler";
                    ViewState["hostaltaken"] = dt.Rows[0]["hosteltaken"].ToString();

                }

                a1.HRef = "fee-collection-monthly-wise.aspx?adm=" + lbl_admissionno.Text;
                a2.HRef = "set-student-wise-discount.aspx?adm=" + lbl_admissionno.Text + "&type=admissionfee";
                Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(ViewState["sessionid"].ToString(), ViewState["classid"].ToString(), lbl_admissionno.Text);
                ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                ViewState["From_month_name"] = (String)dc1["From_month_name"];
                ViewState["From_month_id"] = (String)dc1["From_month_id"];
                ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];

                string isCheckPending = My.isChequePending(ViewState["sessionid"].ToString(), dt.Rows[0]["admissionserialnumber"].ToString(), txt_bank_date.Text);
                if (isCheckPending == "1")
                {
                    check_is_cheque_pending(ViewState["sessionid"].ToString(), dt.Rows[0]["admissionserialnumber"].ToString());
                }
            }
        }

        private void check_is_cheque_pending(string session_id, string admission_no)
        {
            try
            {
                string queryChk = "select * from Fee_payment_by_cheque_status where Session_id='" + session_id + "' and  Admission_no='" + admission_no + "' and Status='PENDING'";
                DataTable dtChk = My.dataTable(queryChk);
                if (dtChk.Rows.Count > 0)
                {
                    RPChkDetails.DataSource = dtChk;
                    RPChkDetails.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openChequeAlert();", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Bind_fee_details()
        {


            string parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";


            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='1' or parameter_id='5') and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + lbl_admissionno.Text + "' and parameter='" + parameter + "' and session='" + lbl_session.Text + "')t";
            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + lbl_session.Text + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='1' or parameter_id='5') and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "' )t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + lbl_session.Text + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where   Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='1' or parameter_id='5') and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "')t";
                }
                fee_dt = My.dataTable(qry);
            }


            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                grd_fee.DataSource = dt;
                grd_fee.DataBind();


                double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                    payable += My.toDouble(dr["payable"]);
                    paid += My.toDouble(dr["paid"]);
                    dues += My.toDouble(dr["dues"]);
                    disc += My.toDouble(dr["disc_amount"]);
                    payble_after_disc += My.toDouble(dr["payable_after_disc"]);
                }

                lbl_paybaleamount.Text = payble_after_disc.ToString("0.00");
                lbl_adjustamount.Text = payble_after_disc.ToString("0.00");

                hd_totalamount.Value = payable.ToString("0.00");
                hd_total_discount.Value = disc.ToString("0.00");
            }

        }

        double total_payable = 0, total_disc_amount = 0, total_perviously = 0, total_dues = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_payable");
                Label lbl_disc_amount = (Label)e.Row.FindControl("lbl_disc_amount");
                Label lbl_perviously_paid = (Label)e.Row.FindControl("lbl_perviously_paid");
                Label lbl_dues = (Label)e.Row.FindControl("lbl_dues");
                if (lbl_payable.Text != "")
                {
                    total_payable = total_payable + Convert.ToDouble(lbl_payable.Text);
                }

                if (lbl_disc_amount.Text != "")
                {
                    total_disc_amount = total_disc_amount + Convert.ToDouble(lbl_disc_amount.Text);
                }

                if (lbl_perviously_paid.Text != "")
                {
                    total_perviously = total_perviously + Convert.ToDouble(lbl_perviously_paid.Text);
                }

                if (lbl_dues.Text != "")
                {
                    total_dues = total_dues + Convert.ToDouble(lbl_dues.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalfeeamount = (Label)e.Row.FindControl("lbl_totalfeeamount");
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totaldiscount");
                Label lbl_totalpreviously = (Label)e.Row.FindControl("lbl_totalpreviously");

                Label lbl_totalpaybale = (Label)e.Row.FindControl("lbl_totalpaybale");


                lbl_totalfeeamount.Text = total_payable.ToString("0.00");
                lbl_totaldiscount.Text = total_disc_amount.ToString("0.00");
                lbl_totalpreviously.Text = total_perviously.ToString("0.00");
                lbl_totalpaybale.Text = total_dues.ToString("0.00");

            }



        }
        private void find_all_paid_fee()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 session_id from session_details where Session=Student_Payment_History.Session) as session_id from Student_Payment_History  where Session='" + ViewState["session"].ToString() + "' and Addmission_no='" + ViewState["admissionno"] + "' and Type='Admission'   ORDER BY id ASC");

            if (dt.Rows.Count == 0)
            {
                grid_payment_history.DataSource = null;
                grid_payment_history.DataBind();
            }
            else
            {
                grid_payment_history.DataSource = dt;
                grid_payment_history.DataBind();
            }
        }

        protected void txt_paid_amount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    lbl_totaldues.Text = (My.toDouble(lbl_paybaleamount.Text) - (My.toDouble(txt_paid_amount.Text) + My.toDouble(ViewState["adjestamount"].ToString()))).ToString("0.00");
                }
                catch
                {
                }
            }
            catch
            {
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



        #region pay
        string type = "";
        public bool payment_status = false;
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_Submit.Text == "Pay")
                {
                    string isCheckPending = My.isChequePending(ViewState["sessionid"].ToString(), lbl_admissionno.Text, txt_bank_date.Text);
                    if (isCheckPending == "1")
                    {
                        check_is_cheque_pending(ViewState["sessionid"].ToString(), lbl_admissionno.Text);
                        return;
                    }

                    if (txt_date.Text == "")
                    {
                        payment_status = false;
                        Alertme("Please choose payment date.", "warning");
                        txt_date.Focus();
                        return;
                    }
                    if (txt_paid_amount.Text == "")
                    {
                        payment_status = false;
                        Alertme("Please enter paid amount first...", "warning");
                        txt_paid_amount.Focus();
                        return;
                    }
                    if (My.toDouble(txt_paid_amount.Text) < 0)
                    {
                        payment_status = false;
                        Alertme("Please enter paid amount first...", "warning");
                        txt_paid_amount.Focus();
                        return;
                    }
                    if (ddl_paymentmode.Text == "Cheque")
                    {
                        if (ddl_bank.Text == "Select")
                        {
                            ddl_bank.Focus();
                            Alertme("Please select bank.", "warning");
                            return;
                        }
                        if (txt_bank_date.Text == "")
                        {
                            //txt_bank_date.Focus();
                            Alertme("Please enter cheque date.", "warning");
                            return;
                        }
                        if (txt_trans_no.Text == "")
                        {
                            txt_trans_no.Focus();
                            Alertme("Please enter cheque no.", "warning");
                            return;
                        }
                    }
                    if (My.toDouble(lbl_totaldues.Text) >= 0)
                    {
                        string slip_no = "", entry_id = "";
                        bool flag = false;
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                        {
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();
                            #region PaymentS
                            int pay_idate = My.DateConvertToIdate(txt_date.Text);

                            //bool chek_fee = payments.find_fee_taken_date("Admission_fee_collection", pay_idate, lbl_admissionno.Text, ViewState["session"].ToString(), con);
                            //if (chek_fee == false)
                            //{
                            //    Alertme("Payment is already done on your chosen date.", "warning");
                            //    return;
                            //}

                            type = "Admission";
                            string ad_no = lbl_admissionno.Text;
                            entry_id = "AD" + cretesessionid(con);

                            if (rd_old_bill.Checked == true)
                            {
                            }
                            else
                            {
                                slip_no = payments.invoice_format_admssion("slip_no", con);
                            }

                            if (rd_old_bill.Checked == true)
                            {
                                if (txt_slip_no.Text == "")
                                {
                                    Alertme("Please enter your deleted slip no.", "warning");
                                    return;
                                }
                                else
                                {
                                    slip_no = txt_slip_no.Text.Trim();
                                    string chek_bill_no = payments.get_old_bill_no(slip_no, ViewState["branchid"].ToString(), con);
                                    if (chek_bill_no != "Yes")
                                    {
                                        Alertme(chek_bill_no, "warning");
                                        return;
                                    }
                                }
                            }

                            payment(slip_no, entry_id, ad_no, con);
                            payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received " + type + " fee :-" + txt_paid_amount.Text + " Adjust Amount" + ViewState["adjestamount"].ToString() + " rs,Slip No :- " + slip_no + " from " + lbl_studentname.Text + ", Admission No :-" + ad_no, con);
                            payment_status = true;

                            #endregion


                            dues_update_headwise_transaction.update_student_dues(ViewState["sessionid"].ToString(), ViewState["classid"].ToString(), ad_no, "0", "0", con);

                            flag = true;
                            con.Close();
                            scope.Complete();
                        }

                        //TRANSITION COMPLETE
                        if (flag == true)
                        {
                            try
                            {
                                string input = txt_paid_amount.Text;  // DD/MM/YYYY
                                string FNsession = My.GetFinancialSessionFromString(input);
                                string unique_entry_id = My.unique_id();
                                double amountpaid = My.toDouble(txt_paid_amount.Text);
                                string Description = "Fee collection from " + lbl_studentname.Text + " Amount : " + amountpaid.ToString("0.00") + "/-";
                                string alternetacc_id = lbl_admissionno.Text;
                                string session_name = FNsession;
                                string VoucherNo = slip_no;
                                bool checkbiilentery = My.check_dup_bill_no_entry(VoucherNo, session_name);
                                string VoucherType = "Receipt";
                                string feeType = "Student Fee Payment";
                                if (checkbiilentery == true)
                                {
                                    if (ddl_paymentmode.Text.ToUpper() == "CASH")
                                    {
                                        My.send_to_cash_payment_Voucher_Details(alternetacc_id, "cash", amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, txt_date.Text, My.DateConvertToIdate(txt_date.Text).ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo,"");
                                    }
                                    else
                                    {
                                        string toponebank = My.get_bank_id(ddl_bank.Text);
                                        My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, txt_date.Text, My.DateConvertToIdate(txt_date.Text).ToString().ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo,"");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }

                            #region sms and whatsaap
                            // sms & whatsapp
                            string message = "";
                            try
                            {
                                Dictionary<string, object> dc2 = My.get_student_info(lbl_admissionno.Text, lbl_session.Text);
                                string mobilesms = (String)dc2["father_mob"];
                                string whatsappno = (String)dc2["Father_whatsApp_no"];

                                string classname = (String)dc2["classname"];
                                string rollnumber = (String)dc2["rollnumber"];
                                string Section = (String)dc2["Section"];
                                string Session_id = (String)dc2["Session_id"];

                                //"Dear " + lbl_name.Text + " you have paid fee :- " + txt_paid_amount.Text + " on date :- " + txt_payment_date.Text + " slip no.:-" + slip_no;

                                string type = "";
                                //  My mycode = new My();
                                Dictionary<string, object> autosms = mycode.get_auto_message_template("Admission_Fee");
                                ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                                ViewState["VariableName"] = (String)autosms["VariableName"];
                                ViewState["SMSType"] = (String)autosms["SMSType"];
                                ViewState["Send_From"] = (String)autosms["Send_From"];
                                ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                                ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];
                                ViewState["Wid"] = (String)autosms["Wid"];
                                var vrls = ViewState["VariableName"].ToString().Split(',');
                                var lst = new String[vrls.Length];

                                if (vrls.Length > 0)
                                {
                                    lst[0] = lbl_studentname.Text;
                                }
                                if (vrls.Length > 1)
                                {
                                    lst[1] = classname;
                                }
                                if (vrls.Length > 2)
                                {
                                    lst[2] = lbl_admissionno.Text;
                                }
                                if (vrls.Length > 3)
                                {
                                    lst[3] = rollnumber;
                                }
                                if (vrls.Length > 4)
                                {
                                    lst[4] = txt_paid_amount.Text;
                                }
                                if (vrls.Length > 5)
                                {
                                    lst[5] = lbl_totaldues.Text;
                                }
                                if (vrls.Length > 6)
                                {
                                    lst[6] = txt_date.Text;
                                }
                                if (vrls.Length > 7)
                                {
                                    lst[7] = slip_no;
                                }
                                message = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                                if (ViewState["SMSType"].ToString() == "Unicode")
                                {
                                    type = "unicode";
                                }
                                else
                                {
                                    type = "english";
                                }


                                try
                                {
                                    if (ViewState["Is_Send_SMS"].ToString().ToUpper() == "TRUE")
                                    {
                                        var dt = mycode.FillData("select top 1 * from message_config where Status='running'");
                                        if (dt.Rows.Count == 1)
                                        {
                                            ViewState["api_key"] = dt.Rows[0]["uid"].ToString();
                                            ViewState["Sender_id"] = dt.Rows[0]["sender"].ToString();
                                        }
                                        else
                                        {
                                            ViewState["api_key"] = "0";
                                            ViewState["Sender_id"] = "0";
                                        }


                                        string api_key = ViewState["api_key"].ToString();
                                        string Sender_id = ViewState["Sender_id"].ToString();
                                        string msgtype = type;


                                        string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + message + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobilesms + "&smsContentType=" + type;

                                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                                        string results = sr.ReadToEnd();
                                        sr.Close();

                                        My.Insert("Message_Details", new
                                        {
                                            Mobile_No = mobilesms,
                                            Message = message,
                                            Date = mycode.date(),
                                            Idate = mycode.idate(),
                                            Time = mycode.time(),
                                            Result = results,
                                            User_id = ViewState["Userid"].ToString(),
                                            Mesage_Type = msgtype,
                                            Groupcode = "SMS",
                                            Status = "SEND",
                                            Url = url,
                                            Message_to_Type = "Student",
                                            admin_user_id = lbl_admissionno.Text,
                                        });
                                    }
                                    if (ViewState["Is_Send_WhatsApp"].ToString().ToUpper() == "TRUE")
                                    {
                                        string sms = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                                        try
                                        {
                                            var dt = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                                            if (dt.Rows.Count == 1)
                                            {
                                                ViewState["whatsapp_mobile_no"] = dt.Rows[0]["SMS_API"].ToString();
                                                ViewState["Whatsapp_api_url"] = dt.Rows[0]["url"].ToString();
                                            }
                                            else
                                            {
                                                ViewState["whatsapp_mobile_no"] = "";
                                                ViewState["Whatsapp_api_url"] = "";
                                            }
                                            var query = new Dictionary<string, string>()
    {
        { "authkey", ViewState["whatsapp_mobile_no"].ToString() },
        { "mobile", whatsappno },
        { "country_code", "91" },
        { "wid", ViewState["Wid"].ToString()},
        { "1", lst[0] },
        { "2", lst[1]},
        { "3", lst[2] },
        { "4", lst[3] },
        { "5", lst[4] },
        { "6", lst[5] },
        { "7",lst[6]},
        { "8", lst[7] }
                                                };

                                            string url = ViewState["Whatsapp_api_url"].ToString();
                                            string fullUrl = url + "?" + string.Join("&",
                                                query.Select(kvp => kvp.Key + "=" + Uri.EscapeDataString(kvp.Value)));

                                            if (whatsappno.Length > 9)
                                            {
                                                My.Insert("WhatsApp_send", new
                                                {
                                                    Mobile_no = whatsappno,
                                                    Message = message,
                                                    Message_url = fullUrl,
                                                    Session_id = Session_id,
                                                    Admission_no = lbl_admissionno.Text,
                                                    Status = "Pending",
                                                    Date = mycode.date(),
                                                    Idate = mycode.idate(),
                                                    Time = mycode.time(),
                                                    Send_by = ViewState["Userid"].ToString(),
                                                    Mesage_Type = type,
                                                });

                                            }
                                            //return true;
                                        }
                                        catch (Exception ex)
                                        {
                                            My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                                            //return false;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.Alertme(ex.Message, "warning");
                                }
                            }
                            catch
                            {
                            }
                            #endregion

                            try
                            {
                                string sub = "Fee Payment Confirmation";
                                string messge = message; //"Dear " + lbl_name.Text + " you have paid fee :- " + txt_paid_amount.Text + " on date :- " + txt_payment_date.Text + " slip no.:-" + slip_no;
                                Dictionary<String, String> ss = new Dictionary<string, string>();
                                ss["notification_id"] = Guid.NewGuid().ToString();
                                ss["message"] = messge;
                                ss["title"] = sub;
                                ss["messagetype"] = "Message";
                                ss["url"] = "";
                                ss["link_url"] = "";
                                ss["UserId"] = lbl_admissionno.Text;
                                UsesCode.SendNotification(ViewState["gcm_id"].ToString(), ss);
                            }
                            catch (Exception ex)
                            {
                            }

                            if (payment_status == true)
                            { 
                                txt_paid_amount.Text = "";
                                string confirmValue = string.Empty;
                                confirmValue = Request.Form["confirm_value"];
                                if (confirmValue == "Yes")
                                {
                                    Session["reprintadmission"] = "101";
                                    string url = "slip/Admission_slip.aspx?admissionno=" + ViewState["admissionno"].ToString() + "&sessionid=" + ViewState["sessionid"].ToString() + "&classid=" + ViewState["classid"].ToString() + "&Slip_no=" + slip_no + "";
                                    Response.Redirect(url, false);
                                }
                                else
                                {
                                    Session["msg"] = "Payment has been successfully taken";
                                    //Response.Redirect("admission-fee-collection.aspx", false);

                                    try
                                    {
                                        if (Session["SlipBkSn"].ToString() == "AD1")
                                        {
                                            Response.Redirect("admission-fee-collection.aspx", false);
                                        }
                                        else if (Session["SlipBkSn"].ToString() == "AN1")
                                        {
                                            Response.Redirect("admission-fee-collection.aspx", false);
                                        }
                                        else if (Session["SlipBkSn"].ToString() == "MN1")
                                        {
                                            Response.Redirect("fee-collection-monthly-wise.aspx?adm=" + ViewState["admissionno"].ToString(), false);
                                        }
                                        else if (Session["SlipBkSn"].ToString() == "MN2")
                                        {
                                            Response.Redirect("fee-collection.aspx?adm=" + ViewState["admissionno"].ToString(), false);
                                        }
                                        else
                                        {
                                            Response.Redirect("admission-fee-collection.aspx", false);
                                        }
                                    }
                                    catch
                                    {
                                        Response.Redirect("admission-fee-collection.aspx", false);
                                    }
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
                            Alertme("Sorry something went wrong. please try again.", "warning");
                        }
                    }
                    else
                    {
                        payment_status = false;
                        Alertme("Please enter valid paid amount first...", "warning");
                        txt_paid_amount.Focus();
                        return;
                    }
                }
                else
                {
                }
            }
            catch
            {
            }
        }

        private string hostaltaken = "";
        private void payment(string slip_no, string entry_id, string ad_no, SqlConnection con)
        {
            string Uid = slip_no;
            string Tag = txt_paid_amount.Text;

            string parameter = "";
            if (type == "Admission")
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
            }
            else
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "ReadmissionFee" : "HostelReadmissionFee";
            }
            send_data_in_student_payment_history(type, slip_no, entry_id, ad_no, parameter, con);
            if (ddl_paymentmode.Text == "Cheque")
            {
                payments.exeSql("insert into Fee_payment_by_cheque_status(Session_id,Admission_no,Monthly_slip_no,Yearly_slip_no,Payment_type,Cheque_no,Bank_name,Cheque_date,Status,Created_by,Created_date,Created_time,Created_idate,Is_group_payment,Cheque_amount) values ('" + ViewState["sessionid"].ToString() + "','" + lbl_admissionno.Text + "','" + slip_no + "','0','" + parameter + "','" + txt_trans_no.Text + "','" + ddl_bank.Text + "','" + txt_bank_date.Text + "','PENDING','" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "','" + mycode.idate() + "','1','" + txt_paid_amount.Text + "');", con);
            }
            send_data_to_school_ledger(slip_no, entry_id, con);
            create_admission_annual_dues(parameter, con);
            send_data_in_feetypewise_collection(slip_no, entry_id, parameter, con);
            if (type == "Admission")
            {
                send_data_to_admission_fee_collection(slip_no, entry_id, parameter, con);
            }
            else
            {
                send_data_to_annual_fee_collection(slip_no, entry_id, con);
            }
            update_data_to_admission_registor(con);
        }

        //private void update_Add_Student_Money_receipt()
        //{
        //    int growcountS = grid_adjustamount.Rows.Count;
        //    for (int iS = 0; iS < growcountS; iS++)
        //    {
        //        Label lbl_Unique_id = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Unique_id");
        //        mycode.executequery("update Add_Student_Money_receipt set Status='Used',Used_date='" + txt_date.Text + "',Used_Idate='" + mycode.ConvertStringToiDate(txt_date.Text) + "',Used_Time='" + mycode.time() + "' where Unique_id='" + lbl_Unique_id.Text + "' ");
        //    }
        //}

        private void update_data_to_admission_registor(SqlConnection con)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber = '" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (My.toDouble(lbl_totaldues.Text) <= 0)
                {
                    dr["payment_status"] = "Paid";
                }
                else if (My.toDouble(lbl_totaldues.Text) > 0)
                {
                    dr["payment_status"] = "Dues";
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void send_data_to_annual_fee_collection(string slip_no, string entry_id, SqlConnection con)
        {
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and  parameter like '%AnnualFee%' and feetype!='Previous Dues'", con);
            //.ToString("0.00")
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = lbl_admissionno.Text;
                dr[2] = My.toDouble(hd_totalamount.Value).ToString("0.00");
                dr[3] = "0";
                dr[4] = My.toDouble(lbl_paybaleamount.Text).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = txt_date.Text;
                dr[8] = ddl_paymentmode.Text;
                dr[9] = slip_no;
                dr["session"] = ViewState["session"].ToString();
                dr["idate"] = Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                dr["remark"] = txt_remrks.Text;
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString(); ;
                dr["Acamedic_Semester_Id"] = "0";
                dr["branchid"] = ViewState["branchid"].ToString();
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void send_data_to_admission_fee_collection(string slip_no, string entry_id, string parameter, SqlConnection con)
        {
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues' ", con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_fee_collection where Admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = lbl_admissionno.Text;
                dr[2] = My.toDouble(hd_totalamount.Value).ToString("0.00");
                dr[3] = My.toDouble(hd_total_discount.Value).ToString("0.00");
                dr[4] = My.toDouble(lbl_paybaleamount.Text).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = txt_date.Text;
                dr[8] = ddl_paymentmode.Text;
                dr[9] = slip_no;
                dr["session"] = ViewState["session"].ToString();
                dr["idate"] = Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                dr["remark"] = txt_remrks.Text;
                dr["entry_id"] = entry_id;
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString(); ;
                dr["Slip_no"] = slip_no;
                dr["Acamedic_Semester_Id"] = "0";
                dr["branchid"] = ViewState["branchid"].ToString();
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void send_data_in_feetypewise_collection(string slip_no, string entry_id, string parameter, SqlConnection con)
        {// monyricpt
            string class_id = ViewState["classid"].ToString();
            double paid_amount = My.toDouble(txt_paid_amount.Text);
            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter + "'", con);
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
                    if (paid_amount >= 0)
                    {
                        string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";
                        double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);
                        dr["Date"] = txt_date.Text;
                        dr["idate"] = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
                        if (paid_amount >= dues)
                        {
                            string prevpaid = dr["paid"].ToString();
                            paid_amount = paid_amount - dues;
                            //paid amt is gratter than dues so dues amt is actual paid.
                            string paid = dr["dues"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(dr["Payable_after_disc"]).ToString("0.00");
                            dr["dues"] = "0";
                            dr["status"] = "Paid";
                            #region send in collection slip
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), dues.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, con);
                            #endregion
                        }
                        else
                        {
                            string prevpaid = dr["paid"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + paid_amount).ToString("0.00");
                            dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                            dr["status"] = "Dues";

                            #region send in collection slip
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, con);
                            #endregion

                            paid_amount = 0;

                        }
                        dr["transection"] = slip_no;
                        dr["is_readyfor_sync"] = true;
                        dr["is_sync"] = false;
                        dr["group_id"] = group_id;
                        dr["class_id"] = class_id;
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

        private void create_admission_annual_dues(string parameter, SqlConnection con)
        {
            string query = "";
            if (payments.dataTable("select session,feetype,payable,paid,dues,status,content_id from dbo.[Typewise_fee_collection] WHERE admission_no='" + lbl_admissionno.Text + "' and parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "'", con).Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where Type_Mode='AdmissionFee' and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["admissionno"].ToString() + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["admissionno"].ToString() + "'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "'";
                }
                else
                {
                    query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where Type_Mode='AdmissionFee' and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["admissionno"].ToString() + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["admissionno"].ToString() + "'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and   fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "'";
                }
                DataTable dt = payments.dataTable(query, con);
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + lbl_admissionno.Text + "','" + lbl_class.Text + "','" + dr["session"].ToString() + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["feetype"].ToString() + "','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','Dues','" + payments.get_start_month(con) + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + My.toDouble(dr["payable_after_disc"].ToString()).ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')", con);
                    }
                }
            }
        }

        private void send_data_to_school_ledger(string transcation, string entry_id, SqlConnection con)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Particulars"] = type + " Fee Collection";
            dr["Discription"] = type + " fee for " + lbl_studentname.Text + " Adm.No:-" + lbl_admissionno.Text + " Total Bill :-" + lbl_paybaleamount.Text + " Paid Amount :-  " + txt_paid_amount.Text;
            dr["AllInput"] = My.toDouble(txt_paid_amount.Text).ToString("0.00");
            dr["AllOutput"] = "0";
            dr["IDate"] = Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
            dr["Date"] = txt_date.Text;
            dr["TransactionId"] = transcation;
            dr["entry_id"] = entry_id;
            dr["session"] = ViewState["session"].ToString();
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Addmission_no"] = lbl_admissionno.Text;
            dr["branchid"] = ViewState["branchid"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            // mony recipt
        }

        private void send_data_in_student_payment_history(string type, string slip_no, string entry_id, string ad_no, string parameter_New, SqlConnection con)
        {
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Transection_in,parameter_New,Bank_name,Bank_date) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,@Transection_in,@parameter_New,@Bank_name,@Bank_date)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", ad_no);
            cmd.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
            cmd.Parameters.AddWithValue("@Date", txt_date.Text);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + lbl_studentname.Text + " Paid Amount : " + txt_paid_amount.Text + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", My.toDouble(txt_paid_amount.Text).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@mode", ddl_paymentmode.Text);
            cmd.Parameters.AddWithValue("@discount", My.toDouble(hd_total_discount.Value).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Discoun_in_School", "0.00");
            cmd.Parameters.AddWithValue("@Discoun_in_Hostel", "0.00");
            cmd.Parameters.AddWithValue("@Discoun_in_Transport", "0.00");
            cmd.Parameters.AddWithValue("@fine", "0.00");
            cmd.Parameters.AddWithValue("@is_ofline_sync", 0);
            cmd.Parameters.AddWithValue("@Is_online_sync", 0);
            cmd.Parameters.AddWithValue("@is_update_in_online", 0);
            cmd.Parameters.AddWithValue("@Previous_admission_no", 0);
            cmd.Parameters.AddWithValue("@App_Transection_id", "");
            cmd.Parameters.AddWithValue("@time", mycode.time());
            cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
            cmd.Parameters.AddWithValue("@Branch", ViewState["branchid"].ToString());
            cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
            cmd.Parameters.AddWithValue("@Remarks", txt_remrks.Text);
            cmd.Parameters.AddWithValue("@User_Slip_no", txt_slip_no.Text);
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", txt_trans_no.Text);
            cmd.Parameters.AddWithValue("@Transection_in", "Software");
            cmd.Parameters.AddWithValue("@parameter_New", parameter_New);
            if (ddl_paymentmode.Text == "Cash")
            {
                cmd.Parameters.AddWithValue("@Bank_name", "");
                cmd.Parameters.AddWithValue("@Bank_date", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Bank_name", ddl_bank.Text);
                cmd.Parameters.AddWithValue("@Bank_date", txt_bank_date.Text);
            }
            if (payments.InsertUpdateData(cmd, con))
            {
            }
        }


        payments pays = new payments();
        private string cretesessionid(SqlConnection con)
        {
            bool duplicate = false;
            string Slip_no = pays.auto_serial("admfee_id", con);
            while (!duplicate)
            {
                DataTable cdt = payments.dataTable("select Slip_no from dbo.[Student_Payment_History] where Slip_no='" + Slip_no + "'", con);
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Slip_no = pays.auto_serial("admfee_id", con);
                }
            }
            return Slip_no;

        }
        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, SqlConnection con)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate,Hostel_id) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + payable + "','" + paid + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + prevpaid + "','" + ViewState["branchid"].ToString() + "','0','" + txt_date.Text + "','" + My.DateConvertToIdate(txt_date.Text) + "','" + ViewState["Hostel_id"].ToString() + "');";
            payments.exeSql(qry, con);
        }
        #endregion

        protected void ddl_paymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_paymentmode.SelectedItem.Text == "Cash")
            {
                pnl_mode_t_n_dv.Visible = false;
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode.SelectedItem.Text == "Netbanking")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode.SelectedItem.Text == "Deposited In Bank")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = true;
            }
            if (ddl_paymentmode.SelectedItem.Text == "Sbdebit")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode.SelectedItem.Text == "Cheque")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Cheque No.";
                bank_dts_amd.Visible = true;
            }
            if (ddl_paymentmode.SelectedItem.Text == "NEFT")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "UTR No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode.SelectedItem.Text == "Debitcard")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode.SelectedItem.Text == "Creditcard")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode.SelectedItem.Text == "Otherdcard")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode.SelectedItem.Text == "UPI")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode.SelectedItem.Text == "Demand Draft(DD)")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = true;
            }
            if (ddl_paymentmode.SelectedItem.Text == "Pos")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
        }


        #region adjustamount


        protected void btn_adjustamount_Click(object sender, EventArgs e)
        {
            if (txt_Uniqueno.Text == "")
            {
                Alertme("Please enter unique no", "warning");
            }
            else
            {
                string query = "Select * from Add_Student_Money_receipt where Unique_id='" + txt_Uniqueno.Text + "'";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry you entred unique receipt no. is wrong", "warning");
                }
                else
                {
                    if (dt.Rows[0]["Status"].ToString() == "Pending")
                    {

                        SqlCommand cmd;
                        string query1 = "Select * from Temp_Adjust_amount where Unique_id='" + txt_Uniqueno.Text + "'";
                        DataTable dt1 = mycode.FillData(query1);
                        if (dt1.Rows.Count == 0)
                        {
                            string query2 = "INSERT INTO Temp_Adjust_amount (Branch_id,User_id,Admission_no,Amount,Unique_id,slipdate,slipIdate,Paymentmode,Payment_id) values (@Branch_id,@User_id,@Admission_no,@Amount,@Unique_id,@slipdate,@slipIdate,@Paymentmode,@Payment_id)";
                            cmd = new SqlCommand(query2);
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Admission_no", lbl_admissionno.Text);
                            cmd.Parameters.AddWithValue("@Amount", My.toDouble(dt.Rows[0]["Amount"].ToString()).ToString("0.00"));
                            cmd.Parameters.AddWithValue("@Unique_id", txt_Uniqueno.Text);
                            cmd.Parameters.AddWithValue("@slipdate", dt.Rows[0]["Date"].ToString());
                            cmd.Parameters.AddWithValue("@slipIdate", dt.Rows[0]["Idate"].ToString());
                            cmd.Parameters.AddWithValue("@Paymentmode", dt.Rows[0]["Amount_mode"].ToString());
                            cmd.Parameters.AddWithValue("@Payment_id", dt.Rows[0]["payment_id"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                                Bind_grid_team_adjustamount();
                            }

                        }
                        else
                        {
                            Alertme("Sorry you entred unique receipt no. is already added", "warning");
                        }

                    }
                    else
                    {
                        Alertme("Sorry you entred unique receipt no. is already used", "warning");
                    }
                }
            }
        }

        private void Bind_grid_team_adjustamount()
        {
            DataTable dt = mycode.FillData("select * from Temp_Adjust_amount  where  Admission_no='" + ViewState["admissionno"] + "'    ");
            if (dt.Rows.Count == 0)
            {
                grid_adjustamount.DataSource = null;
                grid_adjustamount.DataBind();
            }
            else
            {
                grid_adjustamount.DataSource = dt;
                grid_adjustamount.DataBind();
            }
        }

        double total_payableadjust = 0;
        protected void grid_adjustamount_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_payable.Text != "")
                {
                    total_payableadjust = total_payableadjust + Convert.ToDouble(lbl_payable.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totaldiscount");

                lbl_totaldiscount.Text = total_payableadjust.ToString("0.00");
                double finalamount = Convert.ToDouble(lbl_paybaleamount.Text) - Convert.ToDouble(lbl_totaldiscount.Text);
                ViewState["adjestamount"] = lbl_totaldiscount.Text;
                lbl_adjustamount.Text = finalamount.ToString("0.00");
            }
        }
        #endregion
    }
}