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
namespace school_web.Admin
{
    public partial class Taken_Other_Fee_From_Student : System.Web.UI.Page
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
                        lbl_payment_date.Text = mycode.date();
                        Session["reprint_otherfee"] = "2";
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session_student, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddlsessionad, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsessionad.SelectedValue = My.get_session_id();
                        ddlsession.SelectedValue = My.get_session_id();
                        ddl_session_student.SelectedValue = My.get_session_id();
                        txt_date.Text = mycode.date();

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
        #endregion

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
                    ddlclass.SelectedValue = dr["Class_id"].ToString();
                    ddlsession.SelectedValue = dr["Session_id"].ToString();
                    ViewState["Sessionid"] = dr["Session_id"].ToString();
                    ddlsessionad.SelectedValue = dr["Session_id"].ToString();

                    ddl_session_student.SelectedValue = dr["Session_id"].ToString();


                    txt_section.Text = dr["Section"].ToString();
                    txtrollnumber.Text = dr["rollnumber"].ToString();
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";

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

                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();

                    if (ViewState["Transfer_Status"].ToString() == "New")
                    {
                        lbl_studentype.Text = "New";
                    }
                    else
                    {
                        lbl_studentype.Text = "Old";
                    }
                    ViewState["IsBoarding"] = "0";
                    ViewState["parameteridS"] = "4";
                    string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        ViewState["LunchMnthName"] = dts.Rows[0]["Month_name"].ToString();
                        ViewState["LunchMnthId"] = dts.Rows[0]["Month_id"].ToString();
                        ViewState["IsBoarding"] = "1";
                    }

                    lbl_catogery.Text = mycode.get_catogery(dr["Category_id"].ToString());
                    lbl_subcatogery.Text = mycode.get_subcatogery(dr["Category_id"].ToString(), dr["SubCategory_id"].ToString());




                    lbltransporttion.Text = dr["transportationtaken"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
                    }



                    mycode.bind_all_ddl_with_id(ddl_feetype, "Select Content_Name,Content_id from Other_Fee_For_Special_Condition where Session_id='" + ViewState["Sessionid"].ToString() + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "' order by Content_Name asc");


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

                string query;

                query = "select * from admission_registor where class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and section='" + txt_section.Text + "' and rollnumber='" + txtrollnumber.Text + "' and StudentStatus='AV'    and Is_TC_Taken!='true' and   Status='1' order by id asc";





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
                string query = "";

                query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' and  Status='1' order by id asc";



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

                query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' order by id asc";

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



        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_feetype.SelectedItem.Text == "Select")
            {
                Alertme("Please select fee type", "warning");
            }
            else if (txt_date.Text == "")
            {
                Alertme("Please select payment date", "warning");
            }
            else
            {
                //string query = "Select *  from Other_Fee_Taken_For_Student where Session_id=" + ViewState["Sessionid"].ToString() + " and Branch_id=" + ViewState["Branchid"].ToString() + " and Content_id=" + ddl_feetype.SelectedValue + " and Class_id=" + ViewState["classid"].ToString() + " and Admission_no='" + lbl_admission_no.Text + "'";
                //DataTable dt = mycode.FillData(query);
                //if (dt.Rows.Count == 0)
                //{
                    string slno = Final_booking_sl_no();//My.format_bookingadmission(); 
                    SqlCommand cmd;
                    string query2 = "INSERT INTO Other_Fee_Taken_For_Student (Class_id,Branch_id,Session_id,Content_id,Content_Fee,Created_by,Created_date,Content_Name,Admission_no,Payment_date,Payment_Idate,Remarks,Slipid,Payment_mode) values (@Class_id,@Branch_id,@Session_id,@Content_id,@Content_Fee,@Created_by,@Created_date,@Content_Name,@Admission_no,@Payment_date,@Payment_Idate,@Remarks,@Slipid,@Payment_mode)";
                    cmd = new SqlCommand(query2);
                    cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                    cmd.Parameters.AddWithValue("@Content_id", ddl_feetype.SelectedValue);
                    cmd.Parameters.AddWithValue("@Content_Fee", txt_fee.Text);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                    cmd.Parameters.AddWithValue("@Content_Name", ddl_feetype.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Admission_no", lbl_admission_no.Text);
                    cmd.Parameters.AddWithValue("@Payment_date", txt_date.Text);
                    cmd.Parameters.AddWithValue("@Payment_Idate", mycode.ConvertStringToiDate(txt_date.Text));
                    cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
                    cmd.Parameters.AddWithValue("@Slipid", slno);
                    cmd.Parameters.AddWithValue("@Payment_mode", ddl_paymentmode.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                        try
                        {
                            string notice_type = "5"; string notice_type_name = "Other Fee Deposite";
                            My.send_success_notice_fee(lbl_name.Text, lblclass.Text, txt_section.Text, txtrollnumber.Text, lbl_admission_no.Text, txt_fee.Text, notice_type, notice_type_name, ViewState["sessionIDs"].ToString());
                        }
                        catch (Exception ex)
                        {
                        }
                       


                        try
                        {

                          

                            string name = lbl_name.Text;
                            string unique_entry_id = My.unique_id();
                            string VoucherNo = slno;
                            string feeType = "Other Fee";
                            double amountpaid = My.toDouble(txt_fee.Text);
                            string VoucherType = "Receipt";
                            string Description = "Other fee collection from " + name + " Amount : " + amountpaid + "/-";
                            string PayDate = txt_date.Text + " " + mycode.time();
                            int Idate = My.DateConvertToIdate(txt_date.Text);
                            string alternetacc_id = lbl_admission_no.Text;
                            string session_name = ddlsessionad.SelectedItem.Text;
                            bool checkbiilentery = check_dup_bill_no_entry(VoucherNo);
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
                            else
                            {
                            }
                        }
                        catch
                        {

                        }

                        Response.Redirect("slip/Other_fees_slip.aspx?receiptid=" + slno, false);

                    }
                //}
                //else
                //{

                //    Alertme("Already fee taken this head", "warning");
                //}


            }
        }
        private bool check_dup_bill_no_entry(string voucherNo)
        {
            string query = "Select *   from Account_Voucher_Details where VoucherNo_Manual='" + voucherNo + "' and Bill_from='SCHOOL PAY' and ref_name='Other Fee'  ";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string Final_booking_sl_no()
        {
            bool duplicate = false;
            string Booking_Sl_No = mycode.format_otherfee("Slipno_other");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Slipid from dbo.[Other_Fee_Taken_For_Student] where Slipid='" + Booking_Sl_No + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Booking_Sl_No = mycode.format_otherfee("Slipno_other");
                }
            }
            return Booking_Sl_No;
        }

        protected void ddl_feetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_feetype.SelectedItem.Text == "Select")
                {
                    Alertme("Please select fees type", "warning");
                }
                else
                {
                    string query = "Select *  from Other_Fee_For_Special_Condition where Session_id=" + ViewState["Sessionid"].ToString() + " and Branch_id=" + ViewState["Branchid"].ToString() + " and Content_id=" + ddl_feetype.SelectedValue + " and Class_id=" + ViewState["classid"].ToString() + "  ";
                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count == 0)
                    {
                        txt_fee.Text = "0.00";
                    }
                    else
                    {
                        txt_fee.ReadOnly = true;
                        txt_fee.Text = dt.Rows[0]["Content_Fee"].ToString();
                        if (dt.Rows[0]["Is_edit"].ToString() == "1")
                        {
                            txt_fee.ReadOnly = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
    }
}