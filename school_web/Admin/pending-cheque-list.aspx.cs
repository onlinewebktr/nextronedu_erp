using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class pending_cheque_list : System.Web.UI.Page
    {
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["branchid"] = ViewState["Branchid"].ToString();
                        find_firm_details();
                        ViewState["flag"] = "0";
                        txt_dettled_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");


                        ddl_section.Items.Insert(0, new ListItem("ALL", "0"));

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        if (ViewState["Is_Download"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = true;

                        }

                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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
            string qry = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                qry = "select * from Fee_payment_by_cheque_status t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Status='PENDING' order by t1.id desc";
            }
            else if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text == "ALL")
            {
                qry = "select * from Fee_payment_by_cheque_status t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddlsession.SelectedValue + "' and t2.Class_id='" + ddlclass.SelectedValue + "' and t1.Status='PENDING' order by t1.id desc";
            }
            else // if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text != "ALL")
            {
                qry = "select * from Fee_payment_by_cheque_status t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddlsession.SelectedValue + "' and t2.Class_id='" + ddlclass.SelectedValue + "' and t2.Section='" + ddl_section.Text + "' and t1.Status='PENDING' order by t1.id desc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                lbl_class22.Text = "";
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.Text + " Section : " + ddl_section.Text;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                    Label lbl_monthly_slip_no = (Label)row.FindControl("lbl_monthly_slip_no");
                    Label lbl_yearly_slip_no = (Label)row.FindControl("lbl_yearly_slip_no");
                    Label lbl_is_group_payment = (Label)row.FindControl("lbl_is_group_payment");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_section = (Label)row.FindControl("lbl_section");
                    Label lbl_class = (Label)row.FindControl("lbl_class");
                    Label lbl_hostel_taken = (Label)row.FindControl("lbl_hostel_taken");
                    Label lbl_session = (Label)row.FindControl("lbl_session");

                    hd_session.Value = lbl_session.Text;
                    hd_hostel_taken.Value = lbl_hostel_taken.Text;
                    hd_admission_no.Value = lbl_admissionserialnumber.Text;
                    hd_class_name.Value = lbl_class.Text;
                    hd_section.Value = lbl_section.Text;
                    hd_session_id.Value = lbl_session_id.Text;
                    hd_bill1.Value = lbl_monthly_slip_no.Text;
                    hd_bill2.Value = lbl_yearly_slip_no.Text;
                    hd_is_group_bill.Value = lbl_is_group_payment.Text;

                    ViewState["EdtIdS"] = lbl_Id.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }






        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'");
            }
            catch (Exception ex)
            {
            }
        }



        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    bind_grd_view();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export" + ddlclass.SelectedItem.Text + ".xls");
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
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btn_settle_cheque_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_cheque_status.Text == "Select")
                {
                    ddl_cheque_status.Focus();
                    Alertme("Please select cheque status.", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    return;
                }
                if (txt_dettled_date.Text == "")
                {
                    txt_dettled_date.Focus();
                    Alertme("Please choose " + date_type_name.InnerText, "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    return;
                }
                if (ddl_cheque_status.Text == "Bounce")
                {
                    if (ddl_fine_apply.Text == "Yes")
                    {
                        if (My.toDouble(txt_fine_amount.Text) <= 0)
                        {
                            txt_fine_amount.Focus();
                            Alertme("Please enter fine for bounce cheque.", "warning");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            return;
                        }
                    }
                }
                if (txt_remark.Text == "")
                {
                    txt_remark.Focus();
                    Alertme("Please enter remarks.", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    return;
                }

                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    update_status(con);

                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                if (flag == true)
                {
                    if (ddl_cheque_status.Text == "Bounce")
                    {
                        My.exeSql("delete from Account_Voucher_Details where VoucherNo_Manual='" + hd_bill1.Value + "' and VoucherType='Receipt'");
                        Alertme("Your selected bill no has been deleted sucessfully.", "success");
                        string remarks = "Fees Delete";
                        My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Bill No :- " + hd_bill1.Value + "," + remarks + " has been deleted successfully.");
                    }

                    Alertme("Check has been settled successfully.", "success");
                    txt_remark.Text = "";
                    txt_fine_amount.Text = "0";
                    if (ViewState["flag"].ToString() == "0")
                    {
                        bind_grd_view();
                    }
                    if (ViewState["flag"].ToString() == "1")
                    {
                        bind_grd_view();
                    }
                }
                else
                {
                    Alertme("Something went wrong", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_status(SqlConnection con)
        {
            if (ddl_cheque_status.Text == "Settled")
            {
                SqlCommand cmd;
                string query = "update Fee_payment_by_cheque_status set Settled_date=@Settled_date,Settled_time=@Settled_time,Remark=@Remark,Settled_by=@Settled_by,Status=@Status where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Settled_date", mycode.date());
                cmd.Parameters.AddWithValue("@Settled_time", mycode.time());
                cmd.Parameters.AddWithValue("@Remark", txt_remark.Text);
                cmd.Parameters.AddWithValue("@Settled_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Status", "Settled");
                cmd.Parameters.AddWithValue("@Id", ViewState["EdtIdS"].ToString());
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
            if (ddl_cheque_status.Text == "Bounce")
            {
                SqlCommand cmd;
                string query = "update Fee_payment_by_cheque_status set Settled_date=@Settled_date,Settled_time=@Settled_time,Remark=@Remark,Settled_by=@Settled_by,Status=@Status,Is_fine_apply=@Is_fine_apply,Fine_amount=@Fine_amount where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Settled_date", mycode.date());
                cmd.Parameters.AddWithValue("@Settled_time", mycode.time());
                cmd.Parameters.AddWithValue("@Remark", txt_remark.Text);
                cmd.Parameters.AddWithValue("@Settled_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Status", "Bounce");
                if (ddl_fine_apply.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Is_fine_apply", "1");
                    cmd.Parameters.AddWithValue("@Fine_amount", txt_fine_amount.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_fine_apply", "0");
                    cmd.Parameters.AddWithValue("@Fine_amount", "0");
                }
                cmd.Parameters.AddWithValue("@Id", ViewState["EdtIdS"].ToString());
                if (payments.InsertUpdateData(cmd, con))
                {
                    manage_bills(con);
                }
            }
        }

        //AdmissionFee
        //AnnualFee
        //MonthlyFee
        //HostelAdmissionFee
        //HostelAnnualFee
        //HostelMonthlyFee

        private void manage_bills(SqlConnection con)
        {
            DataTable dtP = payments.dataTable("select * from Student_Payment_History where Session='" + hd_session.Value + "' and  Addmission_no='" + hd_admission_no.Value + "' and Slip_no='" + hd_bill1.Value + "'", con);
            if (dtP.Rows.Count > 0)
            {
                DataTable dtS = payments.dataTable("select * from admission_registor where session='" + dtP.Rows[0]["Session"].ToString() + "' and admissionserialnumber='" + dtP.Rows[0]["Addmission_no"].ToString() + "' and Class_id='" + dtP.Rows[0]["Class_id"].ToString() + "'", con);
                SqlCommand cmd;
                string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id,Remark) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id,@Remark)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Current_admission_no", hd_admission_no.Value);
                cmd.Parameters.AddWithValue("@Session_id", dtS.Rows[0]["Session_id"].ToString());
                cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Change_type", "Fees Delete");
                cmd.Parameters.AddWithValue("@Class_Id_New", dtS.Rows[0]["Class_id"].ToString());
                cmd.Parameters.AddWithValue("@Roll_no_New", dtS.Rows[0]["rollnumber"].ToString());
                cmd.Parameters.AddWithValue("@Slip_no", hd_bill1.Value);
                cmd.Parameters.AddWithValue("@New_Section", dtS.Rows[0]["Section"].ToString());
                cmd.Parameters.AddWithValue("@Branch_id", "1");
                cmd.Parameters.AddWithValue("@Remark", txt_remark.Text);
                if (payments.InsertUpdateData(cmd, con))
                {
                    string qery = @"INSERT INTO Student_Payment_History_Save_bakup (Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,insert_time_date,Insert_time_user_id,New_Slip_no,Remark)
                    SELECT Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,'" + My.getdate1() + "','" + ViewState["Userid"].ToString() + "','" + dtP.Rows[0]["Slip_no"].ToString() + "','" + txt_remark.Text + "' FROM Student_Payment_History where Addmission_no='" + dtP.Rows[0]["Addmission_no"].ToString() + "' and Session='" + dtP.Rows[0]["Session"].ToString() + "' and Class_id='" + dtS.Rows[0]["Class_id"].ToString() + "' and Branch='" + ViewState["branchid"].ToString() + "' and Slip_no='" + dtP.Rows[0]["Slip_no"].ToString() + "'";
                    payments.exeSql(qery, con);

                    string qeryM = @"INSERT INTO Deleted_history_headwise (adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,Previous_admission_no,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate,Transport_Boarding_Point_id,Transportation_Id,TransportationPath_id,Hostel_id,Room_category,Created_date,Created_idate,Delete_by,Delete_date,Delete_time)
                    SELECT adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,Previous_admission_no,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate,Transport_Boarding_Point_id,Transportation_Id,TransportationPath_id,Hostel_id,Room_category,Created_date,Created_idate,'" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "' FROM Monthly_Fee_Collection_Slip where adno='" + dtP.Rows[0]["Addmission_no"].ToString() + "' and session='" + dtP.Rows[0]["Session"].ToString() + "' and branchid='" + ViewState["branchid"].ToString() + "' and slipno='" + dtP.Rows[0]["Slip_no"].ToString() + "'";
                    payments.exeSql(qeryM, con);


                    #region update dues amount 
                    string isAdmission = "0";
                    DataTable dtMF = payments.dataTable("select * from Monthly_Fee_Collection_Slip where adno='" + dtS.Rows[0]["admissionserialnumber"].ToString() + "' and class='" + dtS.Rows[0]["Class_id"].ToString() + "' and session='" + dtP.Rows[0]["Session"].ToString() + "' and slipno='" + dtP.Rows[0]["Slip_no"].ToString() + "'  order by id desc", con);
                    if (dtMF.Rows.Count > 0)
                    {
                        foreach (DataRow drMF in dtMF.Rows)
                        {
                            ViewState["months"] = drMF["Month"].ToString();
                            ViewState["parameter"] = drMF["parameter"].ToString();
                            double total_amt = My.toDouble(drMF["paid"].ToString());
                            string qry11 = "select * from Typewise_fee_collection where admission_no='" + dtS.Rows[0]["admissionserialnumber"].ToString() + "' and session='" + dtP.Rows[0]["Session"].ToString() + "'  and content_id='" + drMF["content_id"].ToString() + "' and month='" + drMF["Month"].ToString() + "'";
                            SqlDataAdapter ad = new SqlDataAdapter(qry11, con);
                            DataSet ds = new DataSet();
                            ad.Fill(ds, "Typewise_fee_collection");
                            DataTable tdt = ds.Tables[0];
                            if (tdt.Rows.Count > 0)
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
                    isAdmission = "ANN";
                    if (ViewState["parameter"].ToString() == "AdmissionFee" || ViewState["parameter"].ToString() == "HostelAdmissionFee")
                    {
                        isAdmission = "ADM";
                    }
                    if (isAdmission == "ADM")
                    {
                        update_admission_fee(dtS.Rows[0]["admissionserialnumber"].ToString(), dtP.Rows[0]["Session"].ToString(), dtS.Rows[0]["Class_id"].ToString(), con);
                    }
                    if (isAdmission == "ANN")
                    {
                        update_annual_fee(dtS.Rows[0]["admissionserialnumber"].ToString(), dtP.Rows[0]["Session"].ToString(), dtS.Rows[0]["Class_id"].ToString(), con);
                    }
                    payments.exeSql("delete from Monthly_Fee_Collection_Slip where adno='" + dtS.Rows[0]["admissionserialnumber"].ToString() + "' and class='" + dtS.Rows[0]["Class_id"].ToString() + "' and session='" + dtP.Rows[0]["Session"].ToString() + "' and slipno='" + dtP.Rows[0]["Slip_no"].ToString() + "';delete from Student_Payment_History where  Addmission_no='" + dtS.Rows[0]["admissionserialnumber"].ToString() + "' and Session='" + dtP.Rows[0]["Session"].ToString() + "' and Class_id='" + dtS.Rows[0]["Class_id"].ToString() + "'  and Slip_no='" + dtP.Rows[0]["Slip_no"].ToString() + "';delete from SchoolLedger where Addmission_no='" + dtS.Rows[0]["admissionserialnumber"].ToString() + "' and TransactionId='" + dtP.Rows[0]["Slip_no"].ToString() + "';delete from Account_Voucher_Details where VoucherNo='" + dtP.Rows[0]["Slip_no"].ToString() + "'; delete from Discount_master_report where Bill_no='" + dtP.Rows[0]["Slip_no"].ToString() + "'", con);
                    check_all_head_dues(dtS.Rows[0]["admissionserialnumber"].ToString(), dtP.Rows[0]["Session"].ToString(), con);
                    dues_update_headwise_transaction.update_student_dues(dtS.Rows[0]["Session_id"].ToString(), dtS.Rows[0]["Class_id"].ToString(), dtS.Rows[0]["admissionserialnumber"].ToString(), "0", "0", con);
                    #endregion
                }


                //FineInserT 
                if (ddl_fine_apply.Text == "Yes")
                {
                    string monthName = ViewState["months"].ToString();

                    string typeMode = ViewState["parameter"].ToString();
                    if (typeMode == "MonthlyFee" || typeMode == "HostelMonthlyFee")
                    {
                        typeMode = "MonthlyFee";
                    }
                    if (typeMode == "AdmissionFee" || typeMode == "HostelAdmissionFee")
                    {
                        monthName = "";
                        typeMode = "AdmissionFee";
                    }
                    if (typeMode == "HostelAnnualFee" || typeMode == "AnnualFee")
                    {
                        monthName = "";
                        typeMode = "AnnualFee";
                    }
                    SqlCommand cmd1;
                    string querys = "INSERT INTO Misc_Fee_Master_Studentwise (Admission_No,Month,Session,Session_id,Perticular,Amount,Ledger,Type_Mode,Date,Idate,Created_by) values (@Admission_No,@Month,@Session,@Session_id,@Perticular,@Amount,@Ledger,@Type_Mode,@Date,@Idate,@Created_by)";
                    cmd1 = new SqlCommand(querys);
                    cmd1.Parameters.AddWithValue("@Admission_No", dtS.Rows[0]["admissionserialnumber"].ToString());
                    cmd1.Parameters.AddWithValue("@Month", monthName);
                    cmd1.Parameters.AddWithValue("@Session", dtP.Rows[0]["Session"].ToString());
                    cmd1.Parameters.AddWithValue("@Session_id", dtS.Rows[0]["Session_id"].ToString());
                    cmd1.Parameters.AddWithValue("@Perticular", "Cheque Bounce Fine");
                    cmd1.Parameters.AddWithValue("@Amount", txt_fine_amount.Text);
                    cmd1.Parameters.AddWithValue("@Ledger", "School");
                    cmd1.Parameters.AddWithValue("@Type_Mode", typeMode);
                    cmd1.Parameters.AddWithValue("@Date", mycode.date());
                    cmd1.Parameters.AddWithValue("@Idate", mycode.idate());
                    cmd1.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    if (payments.InsertUpdateData(cmd1, con))
                    {
                        ViewState["issubmit"] = "1"; string parameter = ""; string typewise_months = ViewState["months"].ToString(); string typewise_months_position = "1"; string content_id = ""; string group_id = "";
                        if (ViewState["parameter"].ToString() == "AdmissionFee")
                        {
                            parameter = "AdmissionFee";
                            content_id = "ADM01";
                            group_id = "1";
                        }
                        if (ViewState["parameter"].ToString() == "AnnualFee")
                        {
                            parameter = "AnnualFee";
                            content_id = "ANN01";
                            group_id = "2";
                        }
                        if (ViewState["parameter"].ToString() == "HostelAdmissionFee")
                        {
                            parameter = "HostelAdmissionFee";
                            content_id = "ADM01";
                            group_id = "1";
                        }
                        if (ViewState["parameter"].ToString() == "HostelAnnualFee")
                        {
                            parameter = "HostelAnnualFee";
                            content_id = "ANN01";
                            group_id = "2";
                        }


                        string qryChkTypewise = "select Id from Typewise_fee_collection where admission_no='" + dtS.Rows[0]["admissionserialnumber"].ToString() + "' and session='" + dtP.Rows[0]["Session"].ToString() + "' and (parameter='AdmissionFee' or parameter='HostelAdmissionFee' or parameter='AnnualFee' or parameter='HostelAnnualFee')";
                        if (typeMode == "MonthlyFee" || typeMode == "HostelMonthlyFee")
                        {
                            parameter = ViewState["parameter"].ToString();
                            typewise_months = ViewState["months"].ToString();
                            typewise_months_position = "1";
                            content_id = "1001";
                            group_id = "3";
                            qryChkTypewise = "select Id from Typewise_fee_collection where admission_no='" + dtS.Rows[0]["admissionserialnumber"].ToString() + "' and session='" + dtP.Rows[0]["Session"].ToString() + "' and month='" + ViewState["months"].ToString() + "' and (parameter='MonthlyFee'  or parameter='HostelMonthlyFee')";
                        }
                        if (payments.IsUserExistS(qryChkTypewise, con))
                        { }
                        else
                        {
                            SqlCommand cmds;
                            string query1 = "INSERT INTO Typewise_fee_collection (admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,user_id,content_id,transection,Ledger,group_id,class_id,position,Disc,Payable_after_disc,branchid) values (@admission_no,@class,@session,@section,@parameter,@Date,@idate,@feetype,@payable,@paid,@dues,@status,@month,@user_id,@content_id,@transection,@Ledger,@group_id,@class_id,@position,@Disc,@Payable_after_disc,@branchid)";
                            cmds = new SqlCommand(query1);
                            cmds.Parameters.AddWithValue("@admission_no", hd_admission_no.Value);
                            cmds.Parameters.AddWithValue("@class", dtS.Rows[0]["class"].ToString());
                            cmds.Parameters.AddWithValue("@session", dtP.Rows[0]["Session"].ToString());
                            cmds.Parameters.AddWithValue("@section", dtS.Rows[0]["Section"].ToString());
                            cmds.Parameters.AddWithValue("@parameter", parameter);
                            cmds.Parameters.AddWithValue("@Date", mycode.date());
                            cmds.Parameters.AddWithValue("@idate", mycode.idate());
                            cmds.Parameters.AddWithValue("@feetype", "Cheque Bounce Fine");
                            cmds.Parameters.AddWithValue("@payable", txt_fine_amount.Text);
                            cmds.Parameters.AddWithValue("@paid", "0.00");
                            cmds.Parameters.AddWithValue("@dues", txt_fine_amount.Text);
                            cmds.Parameters.AddWithValue("@status", "Dues");
                            cmds.Parameters.AddWithValue("@month", typewise_months);
                            cmds.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                            cmds.Parameters.AddWithValue("@content_id", content_id);
                            cmds.Parameters.AddWithValue("@transection", content_id);
                            cmds.Parameters.AddWithValue("@Ledger", "School");
                            cmds.Parameters.AddWithValue("@group_id", group_id);
                            cmds.Parameters.AddWithValue("@class_id", dtS.Rows[0]["Class_id"].ToString());
                            cmds.Parameters.AddWithValue("@position", typewise_months_position);
                            cmds.Parameters.AddWithValue("@Disc", "0.00");
                            cmds.Parameters.AddWithValue("@Payable_after_disc", txt_fine_amount.Text);
                            cmds.Parameters.AddWithValue("@branchid", ViewState["Branchid"].ToString());
                            if (payments.InsertUpdateData(cmds, con))
                            {
                            }
                        }
                    }
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
    }
}