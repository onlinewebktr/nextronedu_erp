using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Web.Services;
using System.Data;
using System.Transactions;

namespace school_web.Admin
{
    public partial class special_fee_collection : System.Web.UI.Page
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
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        hd_user_Type.Value = Session["userTypeFee"].ToString();

                        Session["reprint_otherfee"] = "2";
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session_student, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddlsessionad, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsessionad.SelectedValue = My.get_session_id();
                        ddlsession.SelectedValue = ddlsessionad.SelectedValue;
                        ddl_session_student.SelectedValue = ddlsessionad.SelectedValue;


                        txt_payment_date.Text = mycode.date();
                        txt_bank_date.Text = mycode.date();

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
                catch
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



        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' order by id asc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        private void find_details(string query)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                std_basic_infoS.Visible = false;
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    std_basic_infoS.Visible = true;
                    plnStdFindDv.Visible = false;
                    ddlclass.SelectedValue = dr["Class_id"].ToString();
                    ddlsession.SelectedValue = dr["Session_id"].ToString();
                    ViewState["Sessionid"] = dr["Session_id"].ToString();
                    ddlsessionad.SelectedValue = dr["Session_id"].ToString();
                    ddl_session_student.SelectedValue = dr["Session_id"].ToString();

                    lbl_roll_no.Text = dr["rollnumber"].ToString();
                    txt_section.Text = dr["Section"].ToString();
                    txtrollnumber.Text = dr["rollnumber"].ToString();
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    ViewState["adm_no"] = dr["admissionserialnumber"].ToString();


                    lbl_phone.Text = dr["mobilenumber"].ToString();
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();


                    mycode.bind_all_ddl_with_id(ddl_feetype, "select Fee_head,Fee_head_id from Special_fee_head where Status='1' and Fee_head_id in (select content_id from Special_fee_master where session_id='" + ViewState["sessionIDs"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "') order by Fee_head asc");
                }
            }
        }



        protected void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                    ddlclass.Focus();
                    return;
                }
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (txt_section.Text == "")
                {
                    Alertme("Please enter section", "warning");
                    txt_section.Focus();
                    return;
                }
                if (txtrollnumber.Text == "")
                {
                    Alertme("Please enter roll no", "warning");
                    txtrollnumber.Focus();
                    return;
                }
                string query = "select * from admission_registor where class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and section='" + txt_section.Text + "' and rollnumber='" + txtrollnumber.Text + "' and Status='1' order by id asc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void btn_find_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_student.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session_student.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }
                string query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and  Status='1' order by id asc";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                    myModal2.Visible = false;
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    myModal2.Visible = true;
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
                query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and Status='1' order by id asc";
                find_details(query);
                myModal2.Visible = false;
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }



        protected void ddl_feetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["PayableAmt"] = "0";
                if (ddl_feetype.SelectedItem.Text == "Select")
                {
                    txt_fee_amount.Text = "0.00";
                    Alertme("Please select fees head", "warning");
                }
                else
                {
                    string query = "select * from Special_fee_master where content_id='" + ddl_feetype.SelectedValue + "' and session_id='" + ViewState["sessionIDs"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "'";
                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count == 0)
                    {
                        txt_fee_amount.Text = "0.00";
                    }
                    else
                    {
                        ViewState["PayableAmt"] = dt.Rows[0]["amount"].ToString();
                        txt_fee_amount.ReadOnly = true;
                        txt_fee_amount.Text = dt.Rows[0]["amount"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
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
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%' and Session_id='" + Session_id + "' and Status='1'";
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
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%' and Session_id='" + Session_id + "' and Status='1'";
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
        [WebMethod]
        public static List<string> GetBankName(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(compLN.comp))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Bank_name from Bank_master where Bank_name LIKE '%'+@SearchMobNo+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["Bank_name"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion



        protected void btn_make_payment_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalPay();", true);
                if (My.toDouble(txt_fee_amount.Text) == 0)
                {
                    txt_fee_amount.Focus();
                    Alertme("Can't payment proceed with 0 amount", "warning");
                    return;
                }
                if (txt_payment_date.Text == "")
                {
                    txt_payment_date.Focus();
                    Alertme("Please enter payment date.", "warning");
                    return;
                }

                //==================
                if (ddl_paymentmode.Text == "Cash")
                {
                    make_payment();
                }
                else
                {
                    if (ddl_bank.Text == "Select")
                    {
                        ddl_bank.Focus();
                        Alertme("Please select bank.", "warning");
                        return;
                    }
                    if (txt_bank_date.Text == "")
                    {
                        txt_bank_date.Focus();
                        Alertme("Please enter bank date.", "warning");
                        return;
                    }
                    if (txt_transaction_no.Text == "")
                    {
                        txt_transaction_no.Focus();
                        Alertme("Please enter transaction no.", "warning");
                        return;
                    }
                    make_payment();
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void make_payment()
        {
            string input = txt_payment_date.Text;  // DD/MM/YYYY
            string FNsession = My.GetFinancialSessionFromString(input);
            string slip_no = "";
            bool flag = false;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();
                slip_no = payments.invoice_monthly("slip_no", con);
                SqlCommand cmd;
                string query = "INSERT INTO Special_fee_collection (Admission_no,Session_id,Class_id,Section,Fee_head,Fee_id,Payable,Paid_amount,Dues,Receipt_no,Date,Idate,Time,Created_date,Created_time,Payment_mode,Payment_bank,Transaction_date,Transaction_no,Remark,Payee_bank_name,user_id,Financial_session) values (@Admission_no,@Session_id,@Class_id,@Section,@Fee_head,@Fee_id,@Payable,@Paid_amount,@Dues,@Receipt_no,@Date,@Idate,@Time,@Created_date,@Created_time,@Payment_mode,@Payment_bank,@Transaction_date,@Transaction_no,@Remark,@Payee_bank_name,@user_id,@Financial_session)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                cmd.Parameters.AddWithValue("@Section", "");
                cmd.Parameters.AddWithValue("@Admission_no", ViewState["adm_no"].ToString());
                cmd.Parameters.AddWithValue("@Fee_head", ddl_feetype.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Fee_id", ddl_feetype.SelectedValue);
                cmd.Parameters.AddWithValue("@Payable", ViewState["PayableAmt"].ToString());
                cmd.Parameters.AddWithValue("@Paid_amount", ViewState["PayableAmt"].ToString());
                cmd.Parameters.AddWithValue("@Dues", "0.00");
                cmd.Parameters.AddWithValue("@Receipt_no", slip_no);
                cmd.Parameters.AddWithValue("@Date", txt_payment_date.Text);
                cmd.Parameters.AddWithValue("@Idate", My.DateConvertToIdate(txt_payment_date.Text));
                cmd.Parameters.AddWithValue("@Time", mycode.time());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                cmd.Parameters.AddWithValue("@Payment_mode", ddl_paymentmode.Text);
                cmd.Parameters.AddWithValue("@Remark", txt_remarks.Text);
                cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Financial_session", FNsession);
                if (ddl_paymentmode.Text == "Cash")
                {
                    cmd.Parameters.AddWithValue("@Payee_bank_name", "");
                    cmd.Parameters.AddWithValue("@Payment_bank", "");
                    cmd.Parameters.AddWithValue("@Transaction_date", "");
                    cmd.Parameters.AddWithValue("@Transaction_no", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Payee_bank_name", txt_payee_bank_name.Text);
                    cmd.Parameters.AddWithValue("@Payment_bank", ddl_bank.Text);
                    cmd.Parameters.AddWithValue("@Transaction_date", txt_bank_date.Text);
                    cmd.Parameters.AddWithValue("@Transaction_no", txt_transaction_no.Text);
                }
                if (payments.InsertUpdateData(cmd, con))
                {
                }
                flag = true;
                con.Close();
                scope.Complete();
            }

            if (flag == true)
            {
                // account voacher entry
                try
                {
                    string unique_entry_id = My.unique_id();
                    string VoucherNo = slip_no;
                    string feeType = "Special Fee Payment";
                    double amountpaid = My.toDouble(ViewState["PayableAmt"].ToString());
                    string VoucherType = "Receipt";
                    string Description = ddl_feetype.SelectedItem.Text + " fee collection from " + lbl_name.Text + " Amount : " + amountpaid + "/-";

                    string PayDate = txt_payment_date.Text + " " + mycode.time();
                    int Idate = My.DateConvertToIdate(txt_payment_date.Text);
                    string alternetacc_id = lbl_admission_no.Text;
                    string session_name = FNsession;


                    bool checkbiilentery = My.check_dup_bill_no_entry(VoucherNo, session_name);
                    My.find_account_id_for_student(ViewState["adm_no"].ToString());
                    if (checkbiilentery == true)
                    {
                        if (ddl_paymentmode.Text.ToUpper() == "CASH")
                        {
                            My.send_to_cash_payment_Voucher_Details(alternetacc_id, "cash", amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo,"");
                        }
                        else
                        {
                            string toponebank = My.get_bank_id(ddl_bank.Text);
                            My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo,"");
                        }
                    }
                }
                catch
                {
                }
                Response.Redirect("slip/special-fee-receipt.aspx?admissionno=" + ViewState["adm_no"].ToString() + "&sessionid=" + ViewState["sessionIDs"].ToString() + "&classid=" + ViewState["classid"].ToString() + "&Slip_no=" + slip_no + "&from=special-fee-collection.aspx", false);
            }
        }


        //public string GetFinancialSessionFromString(string dateString)
        //{
        //    // Parse date from "dd/MM/yyyy" format
        //    DateTime date;
        //    if (DateTime.TryParseExact(dateString, "dd/MM/yyyy",
        //                               System.Globalization.CultureInfo.InvariantCulture,
        //                               System.Globalization.DateTimeStyles.None, out date))
        //    {
        //        int year = date.Year;
        //        if (date.Month < 4)
        //        {
        //            return (year - 1) + "-" + year;
        //        }
        //        else
        //        {
        //            return year + "-" + (year + 1);
        //        }
        //    }
        //    else
        //    {
        //        return "Invalid date format";
        //    }
        //}
    }
}