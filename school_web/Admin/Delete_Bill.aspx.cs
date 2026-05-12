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
    public partial class Delete_Bill : System.Web.UI.Page
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
                        string pagename_current = "Delete_Bill.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                       
                        Session["classchange"] = "2";
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
                Alertme("Please enter  current admission no.", "warning");
            }
            else
            {
                string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and StudentStatus='AV'  and  Status='1'  and Branch_id='" + ViewState["branchid"].ToString() + "'";
                find_details(query);
            }
        }

        private void find_details(string query)
        {
            pnl_payment_history.Visible = false;
            pnldate.Visible = false;
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
                    ViewState["parameter"] = dr["hosteltaken"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString() == "yes" ? "3" : "4";
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
            string type = "";
            if (rd_addmission_fee.Checked == true)
            {
                type = "Admission";
            }
            else if (rd_monthleyfee.Checked == true)
            {
                type = "Monthly";
            }
            else if (rd_annual_fee.Checked == true)
            {
                type = "Annual";
            }


            string query = "  select t1.*  from Student_Payment_History t1    where     t1.Class_id='" + ViewState["classid"].ToString() + "' and t1.Session='" + ViewState["session"].ToString() + "' and   t1.Addmission_no='" + ViewState["admissionserialnumber"].ToString() + "'  and t1.Branch='" + ViewState["branchid"] + "' and t1.Type='" + type + "' order by t1.Idate desc,id desc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                // Alertme("There are no payment history found", "warning");
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
        protected void rd_addmission_fee_CheckedChanged(object sender, EventArgs e)
        {
            ViewState["paymen_Selection"] = "Admission";
            pnldate.Visible = false;
            bind_payment_history();
        }

        protected void rd_annual_fee_CheckedChanged(object sender, EventArgs e)
        {
            ViewState["paymen_Selection"] = "Annual";
            pnldate.Visible = false;
            bind_payment_history();
        }

        protected void rd_monthleyfee_CheckedChanged(object sender, EventArgs e)
        {
            ViewState["paymen_Selection"] = "Month";
            pnldate.Visible = false;
            bind_payment_history();
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

                Button btn_delete_bill = (Button)e.Row.FindControl("btn_delete_bill");

                if (count == 0)
                {
                    if (ViewState["paymen_Selection"].ToString() == "Admission")
                    {
                        btn_delete_bill.Visible = true;
                    }
                    else if (ViewState["paymen_Selection"].ToString() == "Annual")
                    {
                        btn_delete_bill.Visible = true;
                    }
                    else
                    {
                        btn_delete_bill.Visible = false;
                    }
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

        protected void btn_delete_bill_Click(object sender, EventArgs e)
        {
            string type = "";
            if (rd_addmission_fee.Checked == true)
            {
                type = "Admission";
            }
            else if (rd_monthleyfee.Checked == true)
            {
                type = "Monthly";
            }
            else if (rd_annual_fee.Checked == true)
            {
                type = "Annual";
            }
            if (type != "")
            {
                SqlCommand cmd;
                Button lnk = (Button)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_slipno = (Label)row.FindControl("lbl_slipno");
                Label lbl_Addmission_no = (Label)row.FindControl("lbl_Addmission_no");
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Branchid = (Label)row.FindControl("lbl_Branchid");
                Label lbl_Session = (Label)row.FindControl("lbl_Session");
                Label lbl_Amount = (Label)row.FindControl("lbl_Amount");

                Label lbl_Transection_in = (Label)row.FindControl("lbl_Transection_in");

                if (lbl_Transection_in.Text == "App")
                {
                    Alertme("You Can't Delete Payment Mode(App) transaction ", "warning");

                }
                else
                {
                    string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id)";
                    cmd = new SqlCommand(query);

                    cmd.Parameters.AddWithValue("@Current_admission_no", lbl_Addmission_no.Text);
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                    cmd.Parameters.AddWithValue("@Change_type", type + " Fees Delete");
                    cmd.Parameters.AddWithValue("@Class_Id_New", lbl_Class_id.Text);
                    cmd.Parameters.AddWithValue("@Roll_no_New", lbl_old_roll_no.Text);
                    cmd.Parameters.AddWithValue("@Slip_no", lbl_slipno.Text);
                    cmd.Parameters.AddWithValue("@New_Section", txtsection.Text);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {


                        string qery = @"INSERT INTO Student_Payment_History_Save_bakup (Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,insert_time_date,Insert_time_user_id,New_Slip_no)
                    SELECT Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,'" + My.getdate1() + "','" + ViewState["Userid"].ToString() + "','" + lbl_slipno.Text + "' FROM Student_Payment_History where Addmission_no='" + lbl_Addmission_no.Text + "' and Session='" + lbl_Session.Text + "' and Class_id='" + lbl_Class_id.Text + "' and Branch='" + ViewState["branchid"].ToString() + "' and Slip_no='" + lbl_slipno.Text + "'";

                        mycode.executequery(qery);
                        mycode.executequery("delete from Monthly_Fee_Collection_Slip where adno='" + lbl_Addmission_no.Text + "' and class='" + lbl_Class_id.Text + "' and session='" + lbl_Session.Text + "' and branchid='" + ViewState["branchid"].ToString() + "' and slipno='" + lbl_slipno.Text + "'");
                        mycode.executequery("delete from Student_Payment_History where  Addmission_no='" + lbl_Addmission_no.Text + "' and Session='" + lbl_Session.Text + "' and Class_id='" + lbl_Class_id.Text + "' and Branch='" + ViewState["branchid"].ToString() + "' and Slip_no='" + lbl_slipno.Text + "'");
                        mycode.executequery("delete from SchoolLedger where Addmission_no='" + lbl_Addmission_no.Text + "' and branchid='" + ViewState["branchid"].ToString() + "' and TransactionId='" + lbl_slipno.Text + "'");

                        double total_amount = My.toDouble(lbl_Amount.Text);

                        string parameter = "";

                        if (type == "Admission")
                        {
                            parameter = ViewState["hosteltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                        }
                        else if (type == "Annual")
                        {
                            parameter = ViewState["hosteltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                        }
                        else
                        {

                            parameter = ViewState["hosteltaken"].ToString().ToUpper() == "NO" ? "MonthlyFee" : "HostelMonthlyFee";
                        }


                        string qry = "";
                        #region update dues amount
                        if (parameter == "MonthlyFee")
                        {
                            qry = "  select *  from Typewise_fee_collection  where admission_no='" + lbl_Addmission_no.Text + "' and session='" + lbl_Session.Text + "'  and (status='Paid' or cast(paid as float)>0) and parameter like '%" + parameter + "%' and   branchid='" + ViewState["branchid"].ToString() + "' order by cast(Position as float) desc,parameter desc,id desc";
                            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
                            DataSet ds = new DataSet();
                            ad.Fill(ds, "Typewise_fee_collection");
                            DataTable tdt = ds.Tables[0];
                            if (tdt.Rows.Count == 0)
                            {

                            }
                            else
                            {
                                string prev_month = "", month = "";
                                foreach (DataRow dr in tdt.Rows)
                                {
                                    month = dr["month"].ToString();
                                    if (total_amount > 0)
                                    {
                                        double dues_paid = My.toDouble(dr["paid"]);
                                        if (total_amount >= dues_paid)
                                        {
                                            total_amount = total_amount - dues_paid;
                                            dr["paid"] = "0";
                                            dr["dues"] = dr["Payable_after_disc"];
                                            dr["status"] = "Dues";
                                        }
                                        else
                                        {

                                            dr["paid"] = My.toDouble(dr["paid"]) - total_amount;
                                            dr["dues"] = My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"]);
                                            dr["status"] = "Dues";
                                            total_amount = 0;
                                            break;
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


                                //=========================
                                check_all_head_dues(lbl_Addmission_no.Text, lbl_Session.Text, parameter);
                            }
                        }
                        else if (parameter == "HostelMonthlyFee")
                        {
                            qry = "  select *  from Typewise_fee_collection  where admission_no='" + lbl_Addmission_no.Text + "' and session='" + lbl_Session.Text + "'  and (status='Paid' or cast(paid as float)>0) and parameter like '%" + parameter + "%' and   branchid='" + ViewState["branchid"].ToString() + "' order by cast(Position as float) desc,parameter desc,id desc";
                            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
                            DataSet ds = new DataSet();
                            ad.Fill(ds, "Typewise_fee_collection");
                            DataTable tdt = ds.Tables[0];
                            if (tdt.Rows.Count == 0)
                            {

                            }
                            else
                            {
                                string prev_month = "", month = "";
                                foreach (DataRow dr in tdt.Rows)
                                {
                                    month = dr["month"].ToString();
                                    if (total_amount > 0)
                                    {
                                        double dues_paid = My.toDouble(dr["paid"]);
                                        if (total_amount >= dues_paid)
                                        {
                                            total_amount = total_amount - dues_paid;
                                            dr["paid"] = "0";
                                            dr["dues"] = dr["Payable_after_disc"];
                                            dr["status"] = "Dues"; 
                                        }
                                        else
                                        {

                                            dr["paid"] = My.toDouble(dr["paid"]) - total_amount;
                                            dr["dues"] = My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"]);
                                            dr["status"] = "Dues";
                                            total_amount = 0;

                                            break;

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
                                //=========================
                                check_all_head_dues(lbl_Addmission_no.Text, lbl_Session.Text, parameter);
                            }
                        }
                        else
                        {
                            qry = "  select *  from Typewise_fee_collection  where admission_no='" + lbl_Addmission_no.Text + "' and session='" + lbl_Session.Text + "'  and (status='Paid' or cast(paid as float)>0) and parameter like '%" + parameter + "%' and   branchid='" + ViewState["branchid"].ToString() + "' order by cast(Position as float) desc,parameter desc,id desc";
                            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
                            DataSet ds = new DataSet();
                            ad.Fill(ds, "Typewise_fee_collection");
                            DataTable tdt = ds.Tables[0];
                            if (tdt.Rows.Count == 0)
                            {

                            }
                            else
                            {
                                string prev_month = "", month = "";
                                foreach (DataRow dr in tdt.Rows)
                                {
                                    month = dr["month"].ToString();
                                    if (total_amount > 0)
                                    {
                                        double dues_paid = My.toDouble(dr["paid"]);
                                        if (total_amount >= dues_paid)
                                        {
                                            total_amount = total_amount - dues_paid;
                                            dr["paid"] = "0";
                                            dr["dues"] = dr["Payable_after_disc"];
                                            dr["status"] = "Dues"; 
                                        }
                                        else
                                        {

                                            dr["paid"] = My.toDouble(dr["paid"]) - total_amount;
                                            dr["dues"] = My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"]);
                                            dr["status"] = "Dues";
                                            total_amount = 0; 
                                            break; 
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

                        #endregion
                        if (type == "Annual")
                        {
                            update_annual_fee(lbl_Addmission_no.Text, lbl_Session.Text, lbl_Class_id.Text, parameter);

                            My.no_any_paymnet_then_delete_type_wise_admission_annul_row(lbl_Addmission_no.Text, lbl_Session.Text, lbl_Class_id.Text, parameter);

                            My.exeSql("update admission_registor set payment_status='Unpaid' where admissionserialnumber='" + lbl_Addmission_no.Text + "' and Class_id=" + lbl_Class_id.Text + " and session='" + lbl_Session.Text + "' ");
                        }
                        if (type == "Admission")
                        {
                            update_admission_fee(lbl_Addmission_no.Text, lbl_Session.Text, lbl_Class_id.Text, parameter);
                            My.no_any_paymnet_then_delete_type_wise_admission_annul_row(lbl_Addmission_no.Text, lbl_Session.Text, lbl_Class_id.Text, parameter);

                            My.exeSql("update admission_registor set payment_status='Unpaid' where admissionserialnumber='" + lbl_Addmission_no.Text + "' and Class_id=" + lbl_Class_id.Text + " and session='" + lbl_Session.Text + "' ");
                        }
                        Alertme("Your selected bill no has been deleted sucessfully  ", "success");

                        string remarks = type + " Fees Delete";
                        My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Bill No :- " + lbl_slipno.Text + "," + remarks + " has been deleted successfully.");
                        bind_payment_history();
                    }
                }
            }
        }

        private void check_all_head_dues(string admission_no, string session, string parameter)
        {
            DataTable dtMonth = My.dataTable("select Month from dbo.[Month_Index] order by Position desc");
            if (dtMonth.Rows.Count > 0)
            {
                string is_condition_false = "0";
                foreach (DataRow dr in dtMonth.Rows)
                {
                    if (is_condition_false == "0")
                    {
                        is_condition_false = "1";
                        string qry = "select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session + "' and parameter like '%" + parameter + "%' and month='" + dr["Month"].ToString() + "' and  status='Paid'";
                        DataTable dt = My.dataTable(qry);
                        if (dt.Rows.Count == 0)
                        {
                            is_condition_false = "0";
                            My.exeSql("delete from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session + "' and parameter like '%" + parameter + "%' and month='" + dr["Month"].ToString() + "'");
                        }
                    }
                }
            }
        }

        private void update_admission_fee(string Addmission_no, string Session, string Class_id, string parameter)
        {
            DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + Addmission_no + "' and session='" + Session + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues' and class_id='" + Class_id + "' and branchid='" + ViewState["branchid"].ToString() + "' ");
            SqlConnection conn = new SqlConnection(My.con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_fee_collection where Admission_no='" + Addmission_no + "' and session='" + Session + "' and branchid='" + ViewState["branchid"].ToString() + "'", conn);
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
                        My.exeSql("update dbo.[admission_registor] set payment_status='Unpaid' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                    }
                    else
                    {
                        dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString());
                        dr[6] = My.toDouble(dr[4]) - My.toDouble(dr[5]);
                        My.exeSql("update dbo.[admission_registor] set payment_status='Dues' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                    }
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }
        private void update_annual_fee(string Addmission_no, string Session, string Class_id, string parameter)
        {
            DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + Addmission_no + "' and session='" + Session + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues' and class_id='" + Class_id + "' and branchid='" + ViewState["branchid"].ToString() + "' ");
            SqlConnection conn = new SqlConnection(My.con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + Addmission_no + "' and session='" + Session + "' and branchid='" + ViewState["branchid"].ToString() + "'", conn);
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
                        My.exeSql("update dbo.[admission_registor] set payment_status='Unpaid' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                    }
                    else
                    {
                        dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString());
                        dr[6] = My.toDouble(dr[4]) - My.toDouble(dr[5]);
                        My.exeSql("update dbo.[admission_registor] set payment_status='Dues' where admissionserialnumber='" + Addmission_no + "' and session='" + Session + "' and admissionserialnumber='" + Addmission_no + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
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
                    Alertme("Please enter  current admission no.", "warning");
                }
                else
                { 
                    SqlCommand cmd; 
                    string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id,Old_Payment_date,New_Payment_Date) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id,@Old_Payment_date,@New_Payment_Date)";
                    cmd = new SqlCommand(query); 
                    cmd.Parameters.AddWithValue("@Current_admission_no", ViewState["Addmission_no"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                    cmd.Parameters.AddWithValue("@Change_type", "Update Payment Date");
                    cmd.Parameters.AddWithValue("@Class_Id_New", ViewState["Class_id"].ToString());
                    cmd.Parameters.AddWithValue("@Roll_no_New", lbl_old_roll_no.Text);
                    cmd.Parameters.AddWithValue("@Slip_no", ViewState["slipno"].ToString());
                    cmd.Parameters.AddWithValue("@New_Section", txtsection.Text);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString()); 
                    cmd.Parameters.AddWithValue("@Old_Payment_date", ViewState["olddate"].ToString());
                    cmd.Parameters.AddWithValue("@New_Payment_Date", txt_date_new.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                        mycode.executequery("update Monthly_Fee_Collection_Slip set Date='" + txt_date_new.Text + "',Idate='" + mycode.ConvertStringToiDateup(txt_date_new.Text) + "' where adno='" + ViewState["Addmission_no"] + "' and class='" + ViewState["Class_id"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and branchid='" + ViewState["branchid"].ToString() + "' and slipno='" + ViewState["slipno"].ToString() + "'");
                        mycode.executequery("update Student_Payment_History set Date='" + txt_date_new.Text + "',Idate='" + mycode.ConvertStringToiDateup(txt_date_new.Text) + "',mode='" + ddl_paymentmode.Text + "' where Addmission_no='" + ViewState["Addmission_no"] + "' and Session='" + ViewState["Session"].ToString() + "' and Class_id='" + ViewState["Class_id"].ToString() + "' and Branch='" + ViewState["branchid"].ToString() + "' and Slip_no='" + ViewState["slipno"].ToString() + "'");
                        mycode.executequery("update SchoolLedger set Date='" + txt_date_new.Text + "',IDate='" + mycode.ConvertStringToiDateup(txt_date_new.Text) + "' where Addmission_no='" + ViewState["Addmission_no"] + "' and branchid='" + ViewState["branchid"].ToString() + "' and TransactionId='" + ViewState["slipno"].ToString() + "'");

                        Alertme("Your payment date  has been deleted sucessfully  ", "success");


                        My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Bill No :- " + ViewState["slipno"].ToString() + ", Old Date= " + ViewState["olddate"].ToString() + " to New date=" + txt_date_new.Text + " has been updated successfully.");
                        bind_payment_history();
                        pnldate.Visible = false;

                    }


                }

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Update payment date");
            }
        }

        #endregion


        #region update paymentdate
        protected void btn_update_payment_date_Click(object sender, EventArgs e)
        {
            try
            {

                Button lnk = (Button)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_slipno = (Label)row.FindControl("lbl_slipno");
                Label lbl_Addmission_no = (Label)row.FindControl("lbl_Addmission_no");
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Branchid = (Label)row.FindControl("lbl_Branchid");
                Label lbl_Session = (Label)row.FindControl("lbl_Session");
                Label lbl_date = (Label)row.FindControl("lbl_date");
                Label lbl_paymenetmode = (Label)row.FindControl("lbl_paymenetmode");


                ViewState["slipno"] = lbl_slipno.Text;
                ViewState["Addmission_no"] = lbl_Addmission_no.Text;
                ViewState["Class_id"] = lbl_Class_id.Text;
                ViewState["Branchid"] = lbl_Branchid.Text;
                ViewState["Session"] = lbl_Session.Text;
                ViewState["olddate"] = lbl_date.Text;
                ddl_paymentmode.Text = lbl_paymenetmode.Text;
                txt_date_new.Text = lbl_date.Text;
                pnldate.Visible = true;
            }
            catch
            {
            }
        }
        #endregion
    }
}