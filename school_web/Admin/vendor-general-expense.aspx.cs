using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class vendor_general_expense : System.Web.UI.Page
    {
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



                        Session["print_general"] = "1";
                        txt_date.Text = mycode.date();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["user_type"] = My.get_user_type(ViewState["Userid"].ToString());
                        hd_user_Type.Value = My.get_user_type(ViewState["Userid"].ToString());
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_financial_year, "select Session,session_id from session_details");
                        ddl_financial_year.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_vendor, "select (Company_name +', (Mobile No. : '+Mobile_no+')') as Vendor_name,Vendor_id from Vendor_master order by Company_name asc");

                        mycode.bind_all_ddl_with_id(ddl_vendor_type, "select Vendor_type,Vendor_type_id from Vendor_type_master");

                        string pagename_current = "vendor-general-expense.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        if (Request.QueryString["edtsiD"] != null)
                        {
                            btn_Submit.Text = "Update";
                            btn_cancel.Visible = true;
                            ltUsertop.Text = "Edit General Expense";
                            hd_id.Value = Request.QueryString["edtsiD"].ToString();
                            fetch_data_for_edit();
                        }
                        else
                        {
                            btn_Submit.Text = "Submit";
                            btn_cancel.Visible = false;
                            ltUsertop.Text = "Add General Expense";
                        }

                        ViewState["verification_status"] = My.get_general_expenses();

                    }
                }
            }
            catch (Exception exc)
            {
            }
        }



        private void find_firm_details()
        {
            DataTable dt = PayrollMy.dataTable("select * from Firm_Details");
            if (dt.Rows.Count > 0)
            {   
                try
                {
                    if (hd_user_Type.Value == "Admin")
                    {
                        txt_date.Enabled = true;
                        txt_date.CssClass = "form-control find-dv-txtbx";
                        dateDV.Attributes.Add("class", "col-md-3");
                    }
                    else
                    {
                        if (dt.Rows[0]["Is_user_change_pay_date"].ToString() == "True")
                        {
                            txt_date.Enabled = false;
                            txt_date.CssClass = "form-control find-dv-txtbx noclick";
                            dateDV.Attributes.Add("class", "col-md-3 noclick");
                        }
                        else
                        {
                            txt_date.Enabled = true;
                            txt_date.CssClass = "form-control find-dv-txtbx";
                            dateDV.Attributes.Add("class", "col-md-3");
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }


        private void fetch_data_for_edit()
        {
            DataTable dt = mycode.FillData("select * from Vendor_general_expense where Id=" + hd_id.Value + "");
            if (dt.Rows.Count > 0)
            {
                ddl_financial_year.Text = dt.Rows[0]["Financial_year"].ToString();
                txt_date.Text = dt.Rows[0]["Date"].ToString();
                ddl_vendor.SelectedValue = dt.Rows[0]["Vendor_id"].ToString();
                find_vendor_dt();
                ddl_is_bill_no.Text = dt.Rows[0]["Is_bill_no"].ToString();
                txt_bill_no.Text = dt.Rows[0]["Bill_no"].ToString();
                txt_bill_date.Text = dt.Rows[0]["Bill_date"].ToString();
                txt_bill_amount.Text = dt.Rows[0]["Bill_amount"].ToString();
                txt_payment_amount.Text = dt.Rows[0]["Payment_amount"].ToString();
                ddl_payment_mode.Text = dt.Rows[0]["Payment_mode"].ToString();
                txt_cheque_no.Text = dt.Rows[0]["Check_no"].ToString();
                txt_cheque_date.Text = dt.Rows[0]["Check_date"].ToString();
                txt_bank_name.Text = dt.Rows[0]["Check_bank_name"].ToString();
                txt_utr_no.Text = dt.Rows[0]["Utr_no"].ToString();
                txt_pay_hndover_name.Text = dt.Rows[0]["Payment_handover"].ToString();
                txt_emp_code.Text = dt.Rows[0]["Payment_handover_emp_code"].ToString();
                txt_emp_mobile_no.Text = dt.Rows[0]["Payment_handover_mobile_no"].ToString();
                txt_remarks.Text = dt.Rows[0]["Remarks"].ToString();
                ddl_vendor_type.SelectedValue = dt.Rows[0]["Type_master_Id"].ToString();

            }
        }



        protected void ddl_vendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_vendor.SelectedItem.Text == "Select")
                {
                    Alertme("Please select vendor.", "warning");
                }
                else
                {
                    find_vendor_dt();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_vendor_dt()
        {
            DataTable dt = mycode.FillData("select * from Vendor_master where Vendor_id=" + ddl_vendor.SelectedValue + "");
            if (dt.Rows.Count > 0)
            {
                txt_contact_person.Text = dt.Rows[0]["Person_name"].ToString();
                txt_contact_no.Text = dt.Rows[0]["Mobile_no"].ToString();
                txt_address.Text = dt.Rows[0]["Address"].ToString();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_financial_year.SelectedItem.Text == "Select")
                {
                    Alertme("Please select financial year.", "warning");
                    ddl_financial_year.Focus();
                    return;
                }
                if (txt_date.Text == "Select")
                {
                    Alertme("Please choose date.", "warning");
                    txt_date.Focus();
                    return;
                }
                if (ddl_vendor.SelectedItem.Text == "Select")
                {
                    Alertme("Please select vendor.", "warning");
                    ddl_vendor.Focus();
                    return;
                }
                if (ddl_vendor_type.SelectedItem.Text == "Select")
                {
                    Alertme("Please select payment type.", "warning");
                    ddl_vendor_type.Focus();
                    return;
                }

                if (ddl_is_bill_no.SelectedItem.Text == "Yes")
                {
                    if (txt_bill_no.Text == "")
                    {
                        Alertme("Please enter bill no.", "warning");
                        txt_bill_no.Focus();
                        return;
                    }
                    if (txt_bill_date.Text == "")
                    {
                        Alertme("Please enter bill date.", "warning");
                        txt_bill_date.Focus();
                        return;
                    }
                    if (txt_bill_amount.Text == "")
                    {
                        Alertme("Please enter bill amount.", "warning");
                        txt_bill_amount.Focus();
                        return;
                    }
                }
                if (txt_payment_amount.Text == "")
                {
                    Alertme("Please enter payment amount.", "warning");
                    txt_payment_amount.Focus();
                    return;
                }
                if (ddl_payment_mode.Text == "Cheque")
                {
                    if (txt_cheque_no.Text == "")
                    {
                        Alertme("Please enter check no.", "warning");
                        txt_cheque_no.Focus();
                        return;
                    }
                    if (txt_cheque_date.Text == "")
                    {
                        Alertme("Please enter check date.", "warning");
                        txt_cheque_date.Focus();
                        return;
                    }
                    if (txt_bank_name.Text == "")
                    {
                        Alertme("Please enter bank name.", "warning");
                        txt_bank_name.Focus();
                        return;
                    }
                }

                else if (ddl_payment_mode.Text == "NEFT")
                {
                    if (txt_utr_no.Text == "")
                    {
                        Alertme("Please enter UTR no.", "warning");
                        txt_utr_no.Focus();
                        return;
                    }
                }
                else if (ddl_payment_mode.Text == "Netabanking")
                {
                    if (txt_utr_no.Text == "")
                    {
                        Alertme("Please enter UTR no.", "warning");
                        txt_utr_no.Focus();
                        return;
                    }
                }
                else if (ddl_payment_mode.Text == "Deposote In Bank")
                {
                    if (txt_utr_no.Text == "")
                    {
                        Alertme("Please enter Deposote Transaction No.", "warning");
                        txt_utr_no.Focus();
                        return;
                    }
                }
                if (txt_pay_hndover_name.Text == "")
                {
                    Alertme("Please enter payment handover name.", "warning");
                    txt_pay_hndover_name.Focus();
                    return;
                }
                if (txt_emp_mobile_no.Text == "")
                {
                    Alertme("Please enter payment handover mobile no.", "warning");
                    txt_emp_mobile_no.Focus();
                    return;
                }


                //============================
                if (btn_Submit.Text == "Submit")
                {
                    if (txt_remarks.Text == "")
                    {
                        Alertme("Please enter remarks ", "warning");
                        txt_remarks.Focus();
                        return;
                    }
                    else
                    {
                        if (btn_Submit.Text == "Submit")
                        {
                            if (ViewState["Is_add"].ToString() == "1")
                            {
                                save_general_expense();
                            }
                            else
                            {
                                Alertme(My.get_restricted_message(), "warning");
                            }

                        }
                        else
                        {
                            if (ViewState["Is_Edit"].ToString() == "1")
                            {
                                update_expense_details();
                            }
                            else
                            {
                                Alertme(My.get_restricted_message(), "warning");
                            }

                        }
                    }

                }
                else
                {
                    if (txt_remarks.Text == "")
                    {
                        Alertme("Please enter remarks ", "warning");
                        txt_remarks.Focus();
                        return;
                    }
                    else
                    {
                        if (btn_Submit.Text == "Submit")
                        {
                            if (ViewState["Is_add"].ToString() == "1")
                            {
                                save_general_expense();
                            }
                            else
                            {
                                Alertme(My.get_restricted_message(), "warning");
                            } 
                        }
                        else
                        {
                            if (ViewState["Is_Edit"].ToString() == "1")
                            {
                                update_expense_details();
                            }
                            else
                            {
                                Alertme(My.get_restricted_message(), "warning");
                            } 
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_expense_details()
        {
            SqlCommand cmd;
            string query = "Update Vendor_general_expense set Financial_year=@Financial_year,Date=@Date,Vendor_id=@Vendor_id,Is_bill_no=@Is_bill_no,Bill_no=@Bill_no,Bill_date=@Bill_date,Bill_amount=@Bill_amount,Payment_amount=@Payment_amount,Payment_mode=@Payment_mode,Check_no=@Check_no,Check_date=@Check_date,Check_bank_name=@Check_bank_name,Utr_no=@Utr_no,Payment_handover=@Payment_handover,Payment_handover_emp_code=@Payment_handover_emp_code,Payment_handover_mobile_no=@Payment_handover_mobile_no,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate,Payment_idate=@Payment_idate,Remarks=@Remarks,Type_master_Id=@Type_master_Id where Id = '" + hd_id.Value + "'";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Financial_year", ddl_financial_year.SelectedValue);
            cmd.Parameters.AddWithValue("@Date", txt_date.Text);
            cmd.Parameters.AddWithValue("@Payment_idate", My.DateConvertToIdate(txt_date.Text));
            cmd.Parameters.AddWithValue("@Vendor_id", ddl_vendor.SelectedValue);
            cmd.Parameters.AddWithValue("@Is_bill_no", ddl_is_bill_no.Text);
            cmd.Parameters.AddWithValue("@Type_master_Id", ddl_vendor_type.SelectedValue);
            if (ddl_is_bill_no.Text == "Yes")
            {
                cmd.Parameters.AddWithValue("@Bill_no", txt_bill_no.Text);
                cmd.Parameters.AddWithValue("@Bill_date", txt_bill_date.Text);
                cmd.Parameters.AddWithValue("@Bill_amount", txt_bill_amount.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Bill_no", "");
                cmd.Parameters.AddWithValue("@Bill_date", "");
                cmd.Parameters.AddWithValue("@Bill_amount", "");
            }
            cmd.Parameters.AddWithValue("@Payment_amount", txt_payment_amount.Text);
            cmd.Parameters.AddWithValue("@Payment_mode", ddl_payment_mode.Text);

            if (ddl_payment_mode.Text == "Cash")
            {
                cmd.Parameters.AddWithValue("@Check_no", "");
                cmd.Parameters.AddWithValue("@Check_date", "");
                cmd.Parameters.AddWithValue("@Check_bank_name", "");
                cmd.Parameters.AddWithValue("@Utr_no", "");
            }
            else if (ddl_payment_mode.Text == "Cheque")
            {
                cmd.Parameters.AddWithValue("@Check_no", txt_cheque_no.Text);
                cmd.Parameters.AddWithValue("@Check_date", txt_cheque_date.Text);
                cmd.Parameters.AddWithValue("@Check_bank_name", txt_bank_name.Text);
                cmd.Parameters.AddWithValue("@Utr_no", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Check_no", "");
                cmd.Parameters.AddWithValue("@Check_date", "");
                cmd.Parameters.AddWithValue("@Check_bank_name", "");
                cmd.Parameters.AddWithValue("@Utr_no", txt_utr_no.Text);
            }


            cmd.Parameters.AddWithValue("@Payment_handover", txt_pay_hndover_name.Text);
            cmd.Parameters.AddWithValue("@Payment_handover_emp_code", txt_emp_code.Text);
            cmd.Parameters.AddWithValue("@Payment_handover_mobile_no", txt_emp_mobile_no.Text);
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);

            if (My.InsertUpdateData(cmd))
            {
                empty_form();
                Session["successMsgs"] = "General expense details has been updated successfully";
                Response.Redirect("vendor-general-expense-history.aspx", false);
            }
        }


        private void save_general_expense()
        {
            string slip_id = create_sl_no();
            SqlCommand cmd;
            string query = "INSERT INTO Vendor_general_expense (Slip_no,Financial_year,Date,Vendor_id,Is_bill_no,Bill_no,Bill_date,Bill_amount,Payment_amount,Payment_mode,Check_no,Check_date,Check_bank_name,Utr_no,Created_by,Created_date,Created_idate,Payment_handover,Payment_handover_emp_code,Payment_handover_mobile_no,Payment_idate,Remarks,Type_master_Id,Expense_Approval_Status,Verify_By,Expense_Approval_Remarks,Expense_Approval_Date,Expense_Approval_Idate) values (@Slip_no,@Financial_year,@Date,@Vendor_id,@Is_bill_no,@Bill_no,@Bill_date,@Bill_amount,@Payment_amount,@Payment_mode,@Check_no,@Check_date,@Check_bank_name,@Utr_no,@Created_by,@Created_date,@Created_idate,@Payment_handover,@Payment_handover_emp_code,@Payment_handover_mobile_no,@Payment_idate,@Remarks,@Type_master_Id,@Expense_Approval_Status,@Verify_By,@Expense_Approval_Remarks,@Expense_Approval_Date,@Expense_Approval_Idate)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Slip_no", slip_id);
            cmd.Parameters.AddWithValue("@Financial_year", ddl_financial_year.SelectedValue);
            cmd.Parameters.AddWithValue("@Date", txt_date.Text);
            cmd.Parameters.AddWithValue("@Payment_idate", My.DateConvertToIdate(txt_date.Text));
            cmd.Parameters.AddWithValue("@Vendor_id", ddl_vendor.SelectedValue);
            cmd.Parameters.AddWithValue("@Is_bill_no", ddl_is_bill_no.Text);
            if (ddl_is_bill_no.Text == "Yes")
            {
                cmd.Parameters.AddWithValue("@Bill_no", txt_bill_no.Text);
                cmd.Parameters.AddWithValue("@Bill_date", txt_bill_date.Text);
                cmd.Parameters.AddWithValue("@Bill_amount", txt_bill_amount.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Bill_no", "");
                cmd.Parameters.AddWithValue("@Bill_date", "");
                cmd.Parameters.AddWithValue("@Bill_amount", "");
            }
            cmd.Parameters.AddWithValue("@Payment_amount", txt_payment_amount.Text);
            cmd.Parameters.AddWithValue("@Payment_mode", ddl_payment_mode.Text);

            if (ddl_payment_mode.Text == "Cash")
            {
                cmd.Parameters.AddWithValue("@Check_no", "");
                cmd.Parameters.AddWithValue("@Check_date", "");
                cmd.Parameters.AddWithValue("@Check_bank_name", "");
                cmd.Parameters.AddWithValue("@Utr_no", "");
            }
            else if (ddl_payment_mode.Text == "Cheque")
            {
                cmd.Parameters.AddWithValue("@Check_no", txt_cheque_no.Text);
                cmd.Parameters.AddWithValue("@Check_date", txt_cheque_date.Text);
                cmd.Parameters.AddWithValue("@Check_bank_name", txt_bank_name.Text);
                cmd.Parameters.AddWithValue("@Utr_no", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Check_no", "");
                cmd.Parameters.AddWithValue("@Check_date", "");
                cmd.Parameters.AddWithValue("@Check_bank_name", "");
                cmd.Parameters.AddWithValue("@Utr_no", txt_utr_no.Text);
            }
            cmd.Parameters.AddWithValue("@Payment_handover", txt_pay_hndover_name.Text);
            cmd.Parameters.AddWithValue("@Payment_handover_emp_code", txt_emp_code.Text);
            cmd.Parameters.AddWithValue("@Payment_handover_mobile_no", txt_emp_mobile_no.Text);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
            cmd.Parameters.AddWithValue("@Type_master_Id", ddl_vendor_type.SelectedValue);

            if (ViewState["user_type"].ToString() == "Admin")
            {
                cmd.Parameters.AddWithValue("@Expense_Approval_Status", "Verified");
                cmd.Parameters.AddWithValue("@Verify_By", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Expense_Approval_Remarks", "Verified By Admin");
                cmd.Parameters.AddWithValue("@Expense_Approval_Date", mycode.date());
                cmd.Parameters.AddWithValue("@Expense_Approval_Idate", mycode.idate());

            }
            else
            {
                if (ViewState["verification_status"].ToString() == "1")
                {
                    cmd.Parameters.AddWithValue("@Expense_Approval_Status", "Pending");
                    cmd.Parameters.AddWithValue("@Verify_By", "");
                    cmd.Parameters.AddWithValue("@Expense_Approval_Remarks", "");
                    cmd.Parameters.AddWithValue("@Expense_Approval_Date", "");
                    cmd.Parameters.AddWithValue("@Expense_Approval_Idate", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Expense_Approval_Status", "Verified");
                    cmd.Parameters.AddWithValue("@Verify_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Expense_Approval_Remarks", "Verified By Admin");
                    cmd.Parameters.AddWithValue("@Expense_Approval_Date", mycode.date());
                    cmd.Parameters.AddWithValue("@Expense_Approval_Idate", mycode.idate());
                }




            }

            if (My.InsertUpdateData(cmd))
            {
                Alertme("General expense has been added successfully.", "success");
                empty_form();
            }
        }

        private void empty_form()
        {
            txt_bill_no.Text = "";
            txt_bill_date.Text = "";
            txt_bill_amount.Text = "";
            txt_payment_amount.Text = "";
            txt_cheque_no.Text = "";
            txt_cheque_date.Text = "";
            txt_bank_name.Text = "";
            txt_utr_no.Text = "";
            txt_pay_hndover_name.Text = "";
            txt_emp_code.Text = "";
            txt_emp_mobile_no.Text = "";
            txt_remarks.Text = "";

        }



        private string create_sl_no()
        {
            string slip_id = "";
            bool duplicate = true;
            string slips_id = My.auto_serialS("General_exp_slip_no");

            if (slips_id.Length == 1)
            {
                slip_id = "000" + slips_id;
            }
            else if (slips_id.Length == 2)
            {
                slip_id = "00" + slips_id;
            }
            else if (slips_id.Length == 3)
            {
                slip_id = "0" + slips_id;
            }
            else
            {
                slip_id = slips_id;
            }
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select Slip_no from Vendor_general_expense where Slip_no='" + slip_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    string Item_ids = My.auto_serialS("General_exp_slip_no");
                    if (Item_ids.Length == 1)
                    {
                        slip_id = "000" + Item_ids;
                    }
                    else if (Item_ids.Length == 2)
                    {
                        slip_id = "00" + Item_ids;
                    }
                    else if (Item_ids.Length == 3)
                    {
                        slip_id = "0" + Item_ids;
                    }
                    else
                    {
                        slip_id = Item_ids;
                    }
                }
            }
            return slip_id;
        }


        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("vendor-general-expense-history.aspx", false);
        }
    }
}