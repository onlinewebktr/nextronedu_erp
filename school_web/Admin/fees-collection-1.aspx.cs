using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class fees_collection_1 : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Admin"] == null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Session["Admin"] = HttpContext.Current.User.Identity.Name;
                    Session["userType"] = HttpContext.Current.User.Identity.getUserData("UserType");
                    Session["branchid"] = HttpContext.Current.User.Identity.getUserData("branchid");
                    Session["firm"] = HttpContext.Current.User.Identity.getUserData("Firm");
                }
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
                        try
                        {
                            if (Session["msg"] != null && Session["msg"] != "")
                            {
                                Alertme(Session["msg"].ToString(), "success");
                                Session["msg"] = null;
                            }
                        }
                        catch
                        {
                        }
                        ViewState["Is_fee_month_auto_select"] = "0";
                        ViewState["Is_back_dues_shows"] = "0";
                        ViewState["isApplyDiscByCoupon"] = "0";
                        ViewState["Is_apply_dis_coupon"] = "0";
                        ViewState["is_separate_panel_hostel"] = "0";
                        ViewState["Is_adm_fee_not_check"] = "0";
                        ViewState["RepeatFine"] = "No";
                        ViewState["IsTransportAssign"] = "0";
                        ViewState["IsHostelAssign"] = "0";
                        ViewState["IsUpdateRollSec"] = "0";
                        ViewState["No_of_student_in_a_section"] = "0";
                        ViewState["Is_auto_assign_sec_roll_no"] = "0";
                        ViewState["Is_quarterwise_payment"] = "0";
                        ViewState["Monthly_bill_type"] = "A4";
                        Session["SlipBkSn"] = "MN8";
                        ViewState["total_split_amount"] = "0";
                        ViewState["ttl_admDisc"] = "0";
                        ViewState["ttl_mnthDisc"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        hd_user_id.Value = Session["Admin"].ToString();
                        hd_user_Type.Value = My.get_user_type(ViewState["Userid"].ToString());
                        find_firm_details();
                        #region PermissionS
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data_fee_collection_page(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }

                        try
                        {
                            ViewState["Is_Edit_Bus"] = (String)dc1["Is_Edit_Bus"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_Edit_Bus"] = "0"; ;
                        }
                        try
                        {
                            ViewState["Is_delete_Bus"] = (String)dc1["Is_delete_Bus"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_delete_Bus"] = "0";
                        }
                        try
                        {
                            ViewState["Is_add_Bus"] = (String)dc1["Is_add_Bus"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_add_Bus"] = "0";
                        }
                        try
                        {
                            ViewState["Is_Edit_Hostel"] = (String)dc1["Is_Edit_Hostel"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_Edit_Hostel"] = "0";
                        }
                        try
                        {
                            ViewState["Is_delete_Hostel"] = (String)dc1["Is_delete_Hostel"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_delete_Hostel"] = "0";
                        }
                        try
                        {
                            ViewState["Is_add_Hostel"] = (String)dc1["Is_add_Hostel"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_add_Hostel"] = "0";
                        }
                        try
                        {
                            ViewState["Is_Edit_Discount"] = (String)dc1["Is_Edit_Discount"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_Edit_Discount"] = "0";
                        }
                        try
                        {
                            ViewState["Is_delete_Discount"] = (String)dc1["Is_delete_Discount"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_delete_Discount"] = "0";
                        }
                        try
                        {
                            ViewState["Is_add_Discount"] = (String)dc1["Is_add_Discount"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_add_Discount"] = "0";
                        }
                        try
                        {
                            ViewState["Is_Edit_Bill"] = (String)dc1["Is_Edit_Bill"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_Edit_Bill"] = "0";
                        }
                        try
                        {
                            ViewState["Is_delete_Bill"] = (String)dc1["Is_delete_Bill"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_delete_Bill"] = "0";
                        }
                        try
                        {
                            ViewState["Is_add_Bill"] = (String)dc1["Is_add_Bill"];
                        }
                        catch (Exception ex)
                        {
                            ViewState["Is_add_Bill"] = "0";
                        }
                        #endregion
                        Session["msg"] = "";
                        ViewState["OnPayDateChange"] = "0";
                        ViewState["no_of_months"] = "1";
                        ViewState["more_months_check_status"] = "No";
                        ViewState["check_one_more_months"] = "0";
                        ViewState["checked_after_frst_mnth"] = "0";
                        ViewState["MnthName"] = "0";
                        ViewState["checked_frst_mnth"] = "0";
                        ViewState["Isadmissionfeetekent"] = My.get_admission_condition();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["branchid"] = ViewState["Branchid"].ToString();
                        ViewState["fineAmt"] = "0";
                        ViewState["checked_mnth"] = "0";
                        ViewState["flags1"] = "0";
                        ViewState["fine_inserted"] = "0";
                        ViewState["Other_Fees"] = "0";
                        ViewState["late_fine_no_of_day_month"] = "0";
                        ViewState["fine_date_From"] = "0";
                        ViewState["fine_date_To"] = "0";
                        ViewState["FineType"] = "0";
                        txt_date.Text = mycode.date();
                        ViewState["adjestamount"] = "0";
                        mycode.bind_all_ddl_with_id(ddl_monthname, "select Month,Month_Id from Month_Index order by Position asc");
                        // ddl_month.SelectedValue = mycode.get_current_month_id();
                        mycode.bind_all_ddl_with_id_notselect(ddl_tr_month, "select Month,Position from Month_Index order by Position asc");
                        txt_payment_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session_student, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddlsessionad, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");

                        ///check paymentMode
                        ///
                        DataTable dtcM = My.dataTable("select Id from Payment_Mode_Master");
                        if (dtcM.Rows.Count > 0)
                        {
                            My.bind_ddl_noselect(ddl_paymentmode, "select Type_Mode from Payment_Mode_Master order by Position asc");
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
                            compLN.bind_ddl_select(ddl_payee_bank, "select Bank_name from Bank_master order by Bank_name asc");
                        }
                        catch (Exception ex)
                        {
                        }

                        ddlsessionad.SelectedValue = My.get_session_id();
                        ddlsession.SelectedValue = ddlsessionad.SelectedValue;
                        hd_session_id_for_std.Value = ddlsessionad.SelectedValue;
                        ddl_session_student.SelectedValue = ddlsessionad.SelectedValue;
                        string query = "";

                        if (Request.QueryString["adm"] != null)
                        {
                            try
                            {
                                ddlsessionad.SelectedValue = Request.QueryString["sessionid"];
                                ddlsession.SelectedValue = ddlsessionad.SelectedValue;
                                hd_session_id_for_std.Value = ddlsessionad.SelectedValue;
                                ddl_session_student.SelectedValue = ddlsessionad.SelectedValue;
                            }
                            catch (Exception ex)
                            {
                            }
                            string admissionno = Request.QueryString["adm"];
                            if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                            {
                                query = "select * from admission_registor where admissionserialnumber='" + admissionno + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' order by id asc";
                            }
                            else
                            {
                                query = "select * from admission_registor where admissionserialnumber='" + admissionno + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' and Status='1' order by id asc";
                            }
                            find_details(query);

                            try
                            {
                                if (Session["IsDiscountOpen"].ToString() == "1")
                                {
                                    Session["IsDiscountOpen"] = "0";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                                }
                            }
                            catch
                            {
                            }
                            try
                            {
                                if (Session["IsExtraFeeOpen"].ToString() == "1")
                                {
                                    Session["IsExtraFeeOpen"] = "0";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openExtraFeeAlert();", true);
                                }
                            }
                            catch
                            {
                            }
                            //======== check is disc pending
                            DataTable dt = My.dataTable("select * from Discount_master_check_dup_temp where Admission_no='" + admissionno + "'");
                            if (dt.Rows.Count > 0)
                            {
                                rp_error_disc.DataSource = dt;
                                rp_error_disc.DataBind();
                                My.exeSql("delete from Discount_master_check_dup_temp where Admission_no='" + admissionno + "'");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDiscError();", true);
                            }
                        }
                        txt_admission_no.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
            }
        }

        private void find_installment()
        {
            insTypes.InnerText = "Select Month";
            DataTable Monthdt = ViewState["duesmonthDT"] as DataTable;
            DataTable dt = My.dataTable("select * from Fee_installment_master");
            dt.Columns.Add("Is_active");
            if (dt.Rows.Count > 0)
            {
                monthsDv.Attributes.Add("class", "hidden");
                insTypes.InnerText = "Select Installment";
                foreach (DataRow dr in dt.Rows)
                {
                    string[] stringSeparatorss = new string[] { "," };
                    string[] arr = dr["Month_name"].ToString().Split(stringSeparatorss, StringSplitOptions.None);
                    foreach (string value in arr)
                    {
                        if (Monthdt.Rows.Count > 0)
                        {
                            foreach (DataRow drm in Monthdt.Rows)
                            {
                                if (value == drm["Month"].ToString())
                                {
                                    dr["Is_active"] = "1";
                                }
                            }
                        }
                    }
                    if (dr["Is_active"].ToString() == "1")
                    { }
                    else { dr.Delete(); }
                }
                dt.AcceptChanges();
                rp_installment.DataSource = dt.DefaultView;
                rp_installment.DataBind();
                instDv.Visible = true;
            }
        }

        private void find_firm_details()
        {
            ViewState["Auto_roll_according_to_section"] = "0";
            ViewState["page_reset"] = "0";
            DataTable dt = PayrollMy.dataTable("select * from Firm_Details");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["firm_id"].ToString() == "NAVY-001")
                {
                    payeeBankNameDV.Visible = true;
                }
                try
                {
                    if (dt.Rows[0]["Is_auto_fill_paid"].ToString() == "True")
                    {
                        hd_is_auto_paid_fill.Value = "1";
                    }
                }
                catch
                {
                    hd_is_auto_paid_fill.Value = "0";
                }
                try
                {
                    if (dt.Rows[0]["Is_back_dues_show_in_fees_page"].ToString() == "True")
                    {
                        ViewState["Is_back_dues_shows"] = "1";
                    }
                }
                catch
                {
                    ViewState["Is_back_dues_shows"] = "0";
                }
                try
                {
                    if (dt.Rows[0]["Is_fee_month_auto_select"].ToString() == "True")
                    {
                        ViewState["Is_fee_month_auto_select"] = "1";
                    }
                }
                catch
                {
                    ViewState["Is_fee_month_auto_select"] = "0";
                }

                try
                {
                    if (dt.Rows[0]["Is_apply_discount_coupon"].ToString() == "True")
                    {
                        ViewState["isApplyDiscByCoupon"] = "1";
                        ViewState["Is_apply_dis_coupon"] = "1";
                    }
                }
                catch
                {
                    ViewState["isApplyDiscByCoupon"] = "0";
                    ViewState["Is_apply_dis_coupon"] = "0";
                }

                try
                {
                    if (dt.Rows[0]["Is_page_reset_after_fee_payment"].ToString() == "")
                    {
                        ViewState["page_reset"] = "0";
                    }
                    else
                    {
                        ViewState["page_reset"] = dt.Rows[0]["Is_page_reset_after_fee_payment"].ToString();
                    }

                }
                catch
                {
                    ViewState["page_reset"] = "0";
                }
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
                ViewState["firm_id"] = dt.Rows[0]["firm_id"].ToString();
                hd_firm_id.Value = dt.Rows[0]["firm_id"].ToString();
                try
                {
                    if (dt.Rows[0]["Is_quarterwise_payment"].ToString() == "True")
                    {
                        ViewState["Is_quarterwise_payment"] = "1";
                    }
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ViewState["Is_checkbx_hide_pay_fee"] = "0";
                    if (dt.Rows[0]["Is_checkbx_hide_pay_fee"].ToString() == "True")
                    {
                        ViewState["Is_checkbx_hide_pay_fee"] = "1";
                        try
                        {
                            chkbxallMonth.Attributes.Add("class", "hidden");
                            tdmonthclspn.Attributes["colspan"] = "2";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ViewState["Fee_collect_mode"] = "Monthly";
                    if (dt.Rows[0]["Fee_collect_mode"].ToString() == "Installment")
                    {
                        ViewState["Fee_collect_mode"] = "Installment";
                    }
                }
                catch (Exception ex)
                {
                }
                try
                {
                    if (dt.Rows[0]["Is_auto_assign_sec_roll_no"].ToString() == "True")
                    {
                        ViewState["Is_auto_assign_sec_roll_no"] = "1";
                        ViewState["No_of_student_in_a_section"] = dt.Rows[0]["No_of_student_in_a_section"].ToString();

                        if (dt.Rows[0]["Auto_roll_according_to_section"].ToString() == "True")
                        {
                            ViewState["Auto_roll_according_to_section"] = "1";
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    ViewState["Is_check_payment_verify"] = "0";
                    if (dt.Rows[0]["Is_check_payment_verify"].ToString() == "True")
                    {
                        ViewState["Is_check_payment_verify"] = "1";
                    }
                }
                catch (Exception ex)
                {
                }
                try
                {
                    ViewState["Monthly_bill_type"] = dt.Rows[0]["Monthly_bill_type"].ToString();
                }
                catch (Exception ex)
                {
                }

                ViewState["Is_month_selection_free"] = "1";
                try
                {
                    if (dt.Rows[0]["Is_month_selection_freez"].ToString() == "True")
                    {
                        ViewState["Is_month_selection_free"] = "0";
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    if (dt.Rows[0]["Is_fine_repeat"].ToString() == "True")
                    {
                        ViewState["RepeatFine"] = "Yes";
                    }
                    else
                    {
                        ViewState["RepeatFine"] = "No";
                    }
                }
                catch (Exception ex)
                {
                }


                try
                {
                    if (dt.Rows[0]["Is_adm_fee_not_check"].ToString() == "True")
                    {
                        ViewState["Is_adm_fee_not_check"] = "1";
                    }
                }
                catch (Exception ex)
                {
                }


                try
                {
                    if (dt.Rows[0]["Is_enter_bill_no"].ToString() == "True")
                    {
                        chk_delete_slip.Checked = true;
                        oldslip_no.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                }



                try
                {
                    if (hd_user_Type.Value == "Admin")
                    {
                        chk_latefineapplay.Enabled = true;
                        txt_payment_date.Enabled = true;
                        txt_payment_date.CssClass = "form-control find-dv-txtbx";
                        paydateDVS.Attributes.Add("class", "col-md-3 padd-lft0");

                        //====
                        txt_date.Enabled = true;
                        txt_date.CssClass = "calender-icon form-control find-dv-txtbx";
                        admpayDateDV.Attributes.Add("class", "col-md-3");
                    }
                    else
                    {
                        if (dt.Rows[0]["Is_user_change_pay_date"].ToString() == "True")
                        {
                            txt_payment_date.Enabled = false;
                            txt_payment_date.CssClass = "form-control find-dv-txtbx noclick";
                            paydateDVS.Attributes.Add("class", "col-md-3 padd-lft0 noclick");

                            //====
                            txt_date.Enabled = false;
                            txt_date.CssClass = "calender-icon form-control find-dv-txtbx noclick";
                            admpayDateDV.Attributes.Add("class", "col-md-3 padd-lft0 noclick");
                        }
                        else
                        {
                            txt_payment_date.Enabled = true;
                            txt_payment_date.CssClass = "form-control find-dv-txtbx";
                            paydateDVS.Attributes.Add("class", "col-md-3 padd-lft0");

                            //====
                            txt_date.Enabled = true;
                            txt_date.CssClass = "calender-icon form-control find-dv-txtbx";
                            admpayDateDV.Attributes.Add("class", "col-md-3");
                        }

                        try
                        {
                            if (dt.Rows[0]["Is_fine_remove_not_allow"].ToString() == "True")
                            {
                                chk_latefineapplay.Enabled = false;
                            }
                            else
                            {
                                chk_latefineapplay.Enabled = true;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                }


                try
                {
                    if (dt.Rows[0]["Is_separate_panel_hostel"].ToString() == "True")
                    {
                        ViewState["is_separate_panel_hostel"] = "1";
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {

                ViewState["page_reset"] = "0";

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


        UsesCode myusecode = new UsesCode();
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                find_prev_payments();
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void find_prev_payments()
        {
            //empty_form();
            string query = "";
            if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
            {
                query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' order by id asc";
            }
            else
            {
                query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' and Status='1' order by id asc";
            }
            find_details(query);
        }

        private void find_details(string query)
        {
            ddl_extra_head_for.Items.Insert(0, new ListItem("MonthlyFee", "1"));
            rl_ledger.DataSource = null;
            rl_ledger.DataBind();
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
                    angStd.Visible = false;
                    ViewState["parameterDisc"] = "3";
                    if (dt.Rows[0]["hosteltaken"].ToString() == "")
                    {
                        ViewState["hostaltakenDues"] = "No";
                    }
                    else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                    {
                        ViewState["hostaltakenDues"] = "No";
                    }
                    else
                    {
                        ViewState["hostaltakenDues"] = dt.Rows[0]["hosteltaken"].ToString();
                    }

                    ViewState["gcm_id"] = dr["gcm_id"].ToString();
                    std_basic_infoS.Visible = true;
                    findSection.Visible = false;
                    ddlclass.SelectedValue = dr["Class_id"].ToString();
                    ddlsession.SelectedValue = dr["Session_id"].ToString();
                    ddl_session_student.SelectedValue = dr["Session_id"].ToString();
                    ddlsessionad.SelectedValue = dr["Session_id"].ToString();
                    hd_session_id.Value = dr["Session_id"].ToString();
                    txt_section.Text = dr["Section"].ToString();
                    txtrollnumber.Text = dr["rollnumber"].ToString();
                    lbl_name.Text = dr["studentname"].ToString();
                    txt_student_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = lbl_father_name1.Text = dr["fathername"].ToString();
                    lbl_mothersname.Text = dr["mothername"].ToString();
                    lbl_addresss.Text = dr["careof"].ToString();

                    lblclass_show.Text = dr["class"].ToString() + " / " + dr["Section"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    txtroll_no.Text = dr["rollnumber"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    lbl_class_id.Text = dr["Class_id"].ToString();
                    lbl_session_id.Text = dr["Session_id"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    hd_admission_no.Value = dr["admissionserialnumber"].ToString();
                    hd_class_id.Value = dr["Class_id"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    try
                    {
                        lbl_password.Text = dr["Pwd"].ToString();
                    }

                    catch
                    {

                    }
                    Image1.ImageUrl = dr["studentimagepath"].ToString();
                    Image2.ImageUrl = dr["studentimagepath"].ToString();
                    if (dr["studentimagepath"].ToString() == "")
                    {
                        Image1.Visible = false;
                        Image2.Visible = false;
                    }
                    else
                    {
                        Image1.Visible = true;
                        Image2.Visible = true;
                    }

                    lbl_admission_no_c.Text = dr["admissionserialnumber"].ToString();
                    lbl_name_c.Text = dr["studentname"].ToString();
                    lblclass_show_c.Text = dr["class"].ToString() + " / " + dr["Section"].ToString();
                    lbl_transport_c.Text = dr["transportationtaken"].ToString();
                    lbl_hostel_c.Text = dr["hosteltaken"].ToString();

                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";


                    ViewState["Admission_no"] = dr["admissionserialnumber"].ToString();
                    lbl_phone.Text = dr["father_mob"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());

                    // confussion 
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["father_mob"] = dr["Father_whatsApp_no"].ToString();

                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["sessionid"] = dr["Session_id"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();
                    if (dr["Transfer_Status"].ToString().ToUpper() == "TRANSFERRED")
                    {
                        ViewState["Transfer_Status"] = dr["Transfer_Status_Old"].ToString();
                    }

                    Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["Admission_no"].ToString());
                    ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                    ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                    ViewState["From_month_name"] = (String)dc1["From_month_name"];
                    ViewState["From_month_id"] = (String)dc1["From_month_id"];
                    ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                    ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];
                    ViewState["Hostel_Bed_id"] = (String)dc1["Bed_id"];
                    if (ViewState["Hostel_id"].ToString() == "0")
                    {
                        ViewState["parameterDisc"] = "4";
                    }
                    else
                    {
                        if (lblhostel.Text.ToUpper() == "YES")
                        {
                            ViewState["IsHostelAssign"] = "1";
                            bind_assigned_hostel_detail(ViewState["Hostel_Bed_id"].ToString());
                        }
                    }



                    //===============================
                    #region For Admission
                    lbl_admissionno.Text = dr["admissionserialnumber"].ToString();
                    lbl_studentname.Text = dr["studentname"].ToString();
                    lbl_section_adm.Text = dr["Section"].ToString();
                    lbl_session.Text = dr["session"].ToString();
                    lbl_class.Text = dr["class"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["category_id"] = dr["Category_id"].ToString();
                    ViewState["SubCategory_id"] = dr["SubCategory_id"].ToString();
                    ViewState["rollnumber"] = dr["rollnumber"].ToString();
                    if (dr["hosteltaken"].ToString() == "")
                    {
                        ViewState["hostaltaken"] = "No";
                        lbl_student_type.Text = "Day Scholer";
                    }
                    else if (dr["hosteltaken"].ToString().ToLower() == "no")
                    {
                        ViewState["hostaltaken"] = "No";
                        lbl_student_type.Text = "Day Scholer";
                    }
                    else
                    {
                        lbl_student_type.Text = "Hostler";
                        ViewState["hostaltaken"] = dr["hosteltaken"].ToString();
                    }

                    #endregion

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
                    Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["Admission_no"].ToString());
                    ViewState["Transport_id"] = (String)dc2["Transport_id"];
                    ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                    ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                    ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                    ViewState["Month_name"] = (String)dc2["Month_name"];
                    ViewState["Month_id"] = (String)dc2["Month_id"];
                    ViewState["Year_month"] = (String)dc2["Year_month"];
                    ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];
                    ViewState["TransportRowId"] = (String)dc2["RowId"];



                    if (ViewState["Transport_Assigned_Id"].ToString() == "0")
                    {
                        transport_data.Visible = false;
                    }
                    else
                    {
                        transport_data.Visible = true;
                        lbl_transportname.Text = (String)dc2["Transportname"];
                        lbl_transport_Route.Text = (String)dc2["Transportpathpath"];
                        lbl_boarding_point.Text = (String)dc2["Boarding_Point"];
                        lbl_start_month.Text = (String)dc2["Month_name"];
                        lbl_seatno.Text = (String)dc2["seatname"];
                        ViewState["IsTransportAssign"] = "1";
                    }



                    ///HOSTEL AND TRANSPORT
                    if (ViewState["IsTransportAssign"].ToString() == "1")
                    {
                        if (ViewState["Is_Edit_Bus"].ToString() == "1" || ViewState["Is_delete_Bus"].ToString() == "1" || ViewState["Is_add_Bus"].ToString() == "1")
                        {
                            TransportBTN.Visible = true;
                        }
                        HostelBTN.Visible = false;

                        try
                        {
                            bind_mapped_transport();
                            mappedTransporTDV.Visible = true;
                            TransportMapping.Visible = false;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else if (ViewState["IsHostelAssign"].ToString() == "1")
                    {
                        TransportBTN.Visible = false;
                        if (ViewState["Is_Edit_Hostel"].ToString() == "1" || ViewState["Is_delete_Hostel"].ToString() == "1" || ViewState["Is_add_Hostel"].ToString() == "1")
                        {
                            HostelBTN.Visible = true;
                        }

                        mappedHostelDV.Visible = true;
                        mappingHostelDV.Visible = false;
                    }
                    else if (ViewState["IsTransportAssign"].ToString() == "1" && ViewState["IsHostelAssign"].ToString() == "1")
                    {
                        if (ViewState["Is_Edit_Bus"].ToString() == "1" || ViewState["Is_delete_Bus"].ToString() == "1" || ViewState["Is_add_Bus"].ToString() == "1")
                        {
                            TransportBTN.Visible = true;
                        }
                        if (ViewState["Is_Edit_Hostel"].ToString() == "1" || ViewState["Is_delete_Hostel"].ToString() == "1" || ViewState["Is_add_Hostel"].ToString() == "1")
                        {
                            HostelBTN.Visible = true;
                        }

                        try
                        {
                            bind_mapped_transport();
                            mappedTransporTDV.Visible = true;
                            TransportMapping.Visible = false;
                        }
                        catch (Exception ex)
                        {
                        }


                        try
                        {
                            mappedHostelDV.Visible = true;
                            mappingHostelDV.Visible = false;
                        }
                        catch (Exception ex)
                        {
                        }

                    }
                    else
                    {
                        fetch_transport_info();
                        fetch_hostel_info_for_assign();

                        if (ViewState["Is_Edit_Bus"].ToString() == "1" || ViewState["Is_delete_Bus"].ToString() == "1" || ViewState["Is_add_Bus"].ToString() == "1")
                        {
                            TransportBTN.Visible = true;
                        }
                        if (ViewState["Is_Edit_Hostel"].ToString() == "1" || ViewState["Is_delete_Hostel"].ToString() == "1" || ViewState["Is_add_Hostel"].ToString() == "1")
                        {
                            HostelBTN.Visible = true;
                        }
                        mappedTransporTDV.Visible = false;
                        TransportMapping.Visible = true;

                        mappedHostelDV.Visible = false;
                        mappingHostelDV.Visible = true;
                    }


                    txttotal.Text = "0";
                    txt_paid_prev.Text = "0";
                    txt_discount.Text = "0";
                    txtfineamount.Text = "0";
                    txttotalbill.Text = "0";
                    txt_monthlyFee.Text = "0";
                    if (ViewState["Transfer_Status"].ToString() == "New")
                    {
                        lbl_dues_pay_type.Text = "AdmissionFees";
                        lbl_studentype.Text = "New";
                        find_admission_dues_fee();
                        lbl_Monthsplit.Text = "Admission Fees";
                        chk_is_admission_disc.Text = "Discount on Admission";
                        hd_fee_group.Value = "1";
                    }
                    else
                    {
                        lbl_Monthsplit.Text = "Annual Fees";
                        lbl_dues_pay_type.Text = "AnnualFees";
                        lbl_studentype.Text = "Old";
                        find_annual_dues_fee();
                        chk_is_admission_disc.Text = "Discount on Annual";
                        hd_fee_group.Value = "2";

                        if (ViewState["Is_back_dues_shows"].ToString() == "1")
                        {
                            find_back_dues();
                        }
                    }

                    try
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        Bind_split_month_data(lbl_admission_no.Text, ViewState["sessionIDs"].ToString(), con);
                    }
                    catch (Exception ex)
                    {
                    }

                    My.exeSql("delete from Typewise_fee_collection where admission_no='" + dr["admissionserialnumber"].ToString() + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and  transection=''");
                    //select transection from Typewise_fee_collection where parameter='MonthlyFee' order by Id desc
                    if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        string slip_id_last = get_last_mnth_slip_id(dr["session"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString());
                        if (slip_id_last != "0")
                        {
                            fetch_bill_details(slip_id_last, dr["Session_id"].ToString(), dr["session"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString());
                            ViewState["monthRevised"] = get_revised_month(slip_id_last);
                        }
                    }

                    #region CheckDiscountT
                    int isDiscountGiven = 0;
                    if (ViewState["Transfer_Status"].ToString() == "New")
                    {
                        string qryCheckDisc = "";
                        if (ViewState["Hostel_id"].ToString() == "0")
                        {
                            //Check In Admission no hostel
                            qryCheckDisc = "select 'Admission' as Type, isnull(sum(convert(float, disc_amount)),'0') as Total_disc from(select isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no = '" + dr["admissionserialnumber"].ToString() + "'  and (parameter_id = '1' or parameter_id = '5') and session = '" + dr["session"].ToString() + "' and fee_head_id = Typewise_fee_collection.content_id and Discount_on = 'Admission' and category_id = '" + dr["category_id"].ToString() + "' and sub_category_id = '" + dr["SubCategory_id"].ToString() + "'), (select top 1 disc_amount from dbo.[Discount_Master] where Class_id = '" + dr["Class_id"].ToString() + "' and admission_no = 'All'  and(parameter_id = '1' or parameter_id = '5') and session = '" + dr["session"].ToString() + "' and fee_head_id = Typewise_fee_collection.content_id and Discount_on = 'Admission' and category_id = '" + dr["category_id"].ToString() + "' and sub_category_id = '" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no = '" + dr["admissionserialnumber"].ToString() + "' and (parameter = 'AdmissionFee' ) and session = '" + dr["session"].ToString() + "' and class_id=" + dr["Class_id"].ToString() + ")t";
                        }
                        else
                        {//Check In Admission yes hostel
                            qryCheckDisc = "select 'Admission' as Type, isnull(sum(convert(float, disc_amount)),'0') as Total_disc from(select isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no = '" + dr["admissionserialnumber"].ToString() + "'  and (parameter_id = '1' or parameter_id = '5') and session = '" + dr["session"].ToString() + "' and fee_head_id = Typewise_fee_collection.content_id and Discount_on = 'Admission' and category_id = '" + dr["category_id"].ToString() + "' and sub_category_id = '" + dr["SubCategory_id"].ToString() + "'), (select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id = '" + dr["Class_id"].ToString() + "' and admission_no = 'All'  and(parameter_id = '1' or parameter_id = '5') and session = '" + dr["session"].ToString() + "' and fee_head_id = Typewise_fee_collection.content_id and Discount_on = 'Admission' and category_id = '" + dr["category_id"].ToString() + "' and sub_category_id = '" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no = '" + dr["admissionserialnumber"].ToString() + "' and (parameter = 'AdmissionFee') and session = '" + dr["session"].ToString() + "' and class_id=" + dr["Class_id"].ToString() + ")t";
                        }
                        DataTable adm_check_disc = My.dataTable(qryCheckDisc);
                        if (adm_check_disc.Rows.Count == 0)
                        {
                            if (ViewState["Hostel_id"].ToString() == "0")
                            {
                                qryCheckDisc = "select 'Admission' as Type, isnull(sum(convert(float, disc_amount)),'0') as Total_disc from(select isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no = '" + dr["admissionserialnumber"].ToString() + "'  and(parameter_id = '1' or parameter_id = '5') and session = '" + dr["session"].ToString() + "' and fee_head_id = cm.content_id and Discount_on = 'Admission' and category_id = '" + dr["category_id"].ToString() + "' and sub_category_id = '" + dr["SubCategory_id"].ToString() + "'), (select top 1 disc_amount from dbo.[Discount_Master] where Class_id = '" + dr["Class_id"].ToString() + "' and admission_no = 'All'  and(parameter_id = '1' or parameter_id = '5') and session = '" + dr["session"].ToString() + "' and fee_head_id = cm.content_id and Discount_on = 'Admission' and category_id = '" + dr["category_id"].ToString() + "' and sub_category_id = '" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id = cm.content_id where and (parameter = 'AdmissionFee' )  and session = '" + dr["session"].ToString() + "' and class_id = '" + dr["Class_id"].ToString() + "' and class_id=" + dr["Class_id"].ToString() + ") t";
                            }
                            else
                            {
                                qryCheckDisc = "select 'Admission' as Type, isnull(sum(convert(float, disc_amount)),'0') as Total_disc from(select isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and admission_no = '" + dr["admissionserialnumber"].ToString() + "'  and(parameter_id = '1' or parameter_id = '5') and session = '" + dr["session"].ToString() + "' and fee_head_id = cm.content_id and Discount_on = 'Admission' and category_id = '" + dr["category_id"].ToString() + "' and sub_category_id = '" + dr["SubCategory_id"].ToString() + "'), (select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id = '" + dr["Class_id"].ToString() + "' and admission_no = 'All'  and(parameter_id = '1' or parameter_id = '5') and session = '" + dr["session"].ToString() + "' and fee_head_id = cm.content_id and Discount_on = 'Admission' and category_id = '" + dr["category_id"].ToString() + "' and sub_category_id = '" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id = cm.content_id whereand (parameter = 'AdmissionFee' )  and session = '" + dr["session"].ToString() + "' and class_id = '" + dr["Class_id"].ToString() + "' and fmc.Hostel_Id=" + ViewState["Hostel_id"].ToString() + ") t";


                            }
                            adm_check_disc = My.dataTable(qryCheckDisc);
                            if (adm_check_disc.Rows.Count > 0)
                            {
                                if (My.toDouble(adm_check_disc.Rows[0]["Total_disc"].ToString()) > 0)
                                {
                                    isDiscountGiven = 1;
                                    rp_admission_discount.DataSource = adm_check_disc;
                                    rp_admission_discount.DataBind();
                                }
                            }
                        }
                        else
                        {
                            if (My.toDouble(adm_check_disc.Rows[0]["Total_disc"].ToString()) > 0)
                            {
                                isDiscountGiven = 1;
                                rp_admission_discount.DataSource = adm_check_disc;
                                rp_admission_discount.DataBind();
                            }
                        }
                    }
                    else
                    {
                        string qryCheckDiscAnn = "";
                        if (ViewState["Hostel_id"].ToString() == "0")
                        {
                            //Check In Annual  no hostel
                            qryCheckDiscAnn = "select 'Annual' as Type,isnull(sum(convert(float, disc_amount)),'0') as Total_disc from(select isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + dr["admissionserialnumber"].ToString() + "'  and (parameter_id='2' or parameter_id='6') and session='" + dr["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + dr["Class_id"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + dr["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + dr["admissionserialnumber"].ToString() + "' and ( parameter='AnnualFee') and session='" + dr["session"].ToString() + "' and class_id=" + dr["Class_id"].ToString() + ")t";
                        }
                        else
                        { //Check In Annual  yes hostel
                            qryCheckDiscAnn = "select 'Annual' as Type,isnull(sum(convert(float, disc_amount)),'0') as Total_disc from(select isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + dr["admissionserialnumber"].ToString() + "'  and (parameter_id='2' or parameter_id='6') and session='" + dr["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + dr["Class_id"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + dr["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + dr["admissionserialnumber"].ToString() + "' and (parameter='HostelAnnualFee') and session='" + dr["session"].ToString() + "' and class_id=" + dr["Class_id"].ToString() + " )t";
                        }


                        DataTable ann_check_disc = My.dataTable(qryCheckDiscAnn);
                        if (ann_check_disc.Rows.Count == 0)
                        {
                            if (ViewState["Hostel_id"].ToString() == "0")
                            {
                                qryCheckDiscAnn = "select 'Annual' as Type,isnull(sum(convert(float, disc_amount)),'0') as Total_disc from(select isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + dr["admissionserialnumber"].ToString() + "'  and (parameter_id='2' or parameter_id='6') and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + dr["Class_id"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where and( parameter = 'AnnualFee') and session='" + dr["session"].ToString() + "' and class_id='" + dr["Class_id"].ToString() + "')t";
                            }


                            else
                            {
                                qryCheckDiscAnn = "select 'Annual' as Type,isnull(sum(convert(float, disc_amount)),'0') as Total_disc from(select isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + dr["admissionserialnumber"].ToString() + "'  and (parameter_id='2' or parameter_id='6') and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + dr["Class_id"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where and(parameter = 'HostelAnnualFee') and session='" + dr["session"].ToString() + "' and class_id='" + dr["Class_id"].ToString() + "' and fmc.Hostel_Id=" + ViewState["Hostel_id"].ToString() + ")t";
                            }



                            ann_check_disc = My.dataTable(qryCheckDiscAnn);
                            if (ann_check_disc.Rows.Count > 0)
                            {
                                if (My.toDouble(ann_check_disc.Rows[0]["Total_disc"].ToString()) > 0)
                                {
                                    isDiscountGiven = 1;
                                    rp_admission_discount.DataSource = ann_check_disc;
                                    rp_admission_discount.DataBind();
                                }
                            }
                        }
                        else
                        {
                            if (My.toDouble(ann_check_disc.Rows[0]["Total_disc"].ToString()) > 0)
                            {
                                isDiscountGiven = 1;
                                rp_admission_discount.DataSource = ann_check_disc;
                                rp_admission_discount.DataBind();
                            }
                        }
                    }

                    //Check In MonthlY   

                    string qryCheckDiscMnthlY = "";
                    if (ViewState["Hostel_id"].ToString() == "0")
                    {
                        qryCheckDiscMnthlY = "select isnull(sum(convert(float, disc_amount)),'0') as Total_disc from Discount_Master where session='" + dr["session"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "' and Discount_on='Monthly'";
                    }
                    else
                    {
                        qryCheckDiscMnthlY = "select isnull(sum(convert(float, disc_amount)),'0') as Total_disc from Discount_Master where session='" + dr["session"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "' and Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Room_Category_id='" + ViewState["Room_Category_id"].ToString() + "' and Discount_on='Monthly'";
                    }


                    DataTable mnthly_check_disc = My.dataTable(qryCheckDiscMnthlY);
                    if (mnthly_check_disc.Rows.Count == 0)
                    {
                        if (ViewState["Hostel_id"].ToString() == "0")
                        {
                            qryCheckDiscMnthlY = "select isnull(sum(convert(float, disc_amount)),'0') as Total_disc from Discount_Master where session='" + dr["session"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "' and Discount_on='Monthly'";
                        }
                        else
                        {
                            qryCheckDiscMnthlY = "select isnull(sum(convert(float, disc_amount)),'0') as Total_disc from Discount_Master where session='" + dr["session"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "' and Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Room_Category_id='" + ViewState["Room_Category_id"].ToString() + "' and Discount_on='Monthly'";
                        }


                        mnthly_check_disc = My.dataTable(qryCheckDiscMnthlY);
                        if (mnthly_check_disc.Rows.Count > 0)
                        {
                            if (My.toDouble(mnthly_check_disc.Rows[0]["Total_disc"].ToString()) > 0)
                            {
                                isDiscountGiven = 1;
                                get_month_discount();

                            }
                        }
                    }
                    else
                    {
                        if (My.toDouble(mnthly_check_disc.Rows[0]["Total_disc"].ToString()) > 0)
                        {
                            isDiscountGiven = 1;
                            get_month_discount();
                        }
                    }

                    discDTbtn.Visible = false;
                    if (isDiscountGiven == 1)
                    {
                        lbl_final_total_disc.Text = (My.toDouble(ViewState["ttl_admDisc"].ToString()) + My.toDouble(ViewState["ttl_mnthDisc"].ToString())).ToString();
                        discDTbtn.Visible = true;
                    }


                    #endregion


                    string isCheckPending = My.isChequePending(ddlsessionad.SelectedValue, lbl_admission_no.Text, txt_bank_date.Text);
                    if (isCheckPending == "1")
                    {
                        check_is_cheque_pending(ddlsessionad.SelectedValue, dr["admissionserialnumber"].ToString());
                    }
                }

                if (ViewState["Is_auto_assign_sec_roll_no"].ToString() == "1")
                {
                    if (ViewState["Section"].ToString().ToUpper() == "NA")
                    {
                        section_roll_auto_assign();
                    }
                }

                if (ViewState["Is_apply_dis_coupon"].ToString() == "1")
                {
                    check_is_apply_coupon(ddlclass.SelectedValue);
                }


                find_all_due_fee();
                if (ViewState["Fee_collect_mode"].ToString() == "Installment")
                {
                    find_installment();
                }
                fetch_ledger();
                Bind_month_name_split();
                for_discount(ddlsession.SelectedItem.Text, ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, lblhostel.Text, lbltransporttion.Text, ViewState["Hostel_id"].ToString(), ViewState["Room_Category_id"].ToString(), ViewState["TransportPath_id"].ToString(), ViewState["Boarding_Point_id"].ToString());
                for_extra_fee_head(ddlsession.SelectedItem.Text, ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text, lblhostel.Text, lbltransporttion.Text, ViewState["Hostel_id"].ToString(), ViewState["Room_Category_id"].ToString(), ViewState["TransportPath_id"].ToString(), ViewState["Boarding_Point_id"].ToString());
                string total_dues_for_month = "0";
                try
                {
                    total_dues_for_month = monthlyDues.get_month_fee_dues_till_months(ddlsession.SelectedValue, ddlclass.SelectedValue, txt_admission_no.Text);
                }
                catch
                {
                    total_dues_for_month = "0";
                }



                lbl_monthly_dues_till_now.Text = "Monthly Dues : " + total_dues_for_month;
                double total_amts = (My.toDouble(total_dues_for_month) + My.toDouble(txt_adm_ann_fee.Text));
                lbl_adission_monthly_dues_till_now.Text = "Net Dues : " + total_amts.ToString("0.00");

                if (ViewState["Is_month_selection_free"].ToString() == "0")
                {
                    int growcountS = rd_months.Items.Count;
                    for (int iS = 0; iS < growcountS; iS++)
                    {
                        CheckBox chk_month = (CheckBox)rd_months.Items[iS].FindControl("chk_month");
                        if (iS > 0)
                        {
                            chk_month.Enabled = false;
                        }
                    }
                }

                try
                {

                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                        flag = true;
                        con.Close();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                }

                if (ViewState["is_separate_panel_hostel"].ToString() == "1")
                {
                    getHostelFee.Visible = false;
                    string hostelDB = ConfigurationManager.AppSettings["HostelConn"];
                    DataTable dtH = My.dataTableHDB("select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsessionad.SelectedValue + "' and hosteltaken='Yes' and StudentStatus='AV' and Is_TC_Taken!='true' and Status='1' and admissionserialnumber in (select Admission_no from HOSTEL_ASSIGN_MASTER where Session_id='" + ddlsessionad.SelectedValue + "' and Class_id='" + ViewState["classid"].ToString() + "' and Admission_no='" + txt_admission_no.Text + "')", hostelDB);
                    if (dtH.Rows.Count > 0)
                    {
                        getHostelFee.Visible = true;
                        string hostelLink = ConfigurationManager.AppSettings["HostelUrl"];
                        string respUrl = hostelLink + "/login.aspx?feePage=fees-collection-1.aspx&snId=" + ddlsessionad.SelectedValue + "&clsid=" + ViewState["classid"].ToString() + "&admno=" + txt_admission_no.Text + "&userid=" + ViewState["Userid"].ToString();
                        //Response.Redirect(respUrl, false);
                        getHostelFee.HRef = respUrl;
                    }
                }

                txt_month_total.Text = "0";
                txt_month_disc_amt.Text = "0";
                txt_month_prev_paid.Text = "0";
                txt_month_net_payble.Text = "0";



                ///==================================================== 
                if (ViewState["Is_fee_month_auto_select"].ToString() == "1")
                {
                    if (ViewState["Fee_collect_mode"].ToString() == "Installment")
                    {
                        int isFirstChecked = 0;
                        int growcountI = rp_installment.Items.Count;
                        for (int ix = 0; ix < growcountI; ix++)
                        {
                            if (isFirstChecked == 0)
                            {
                                isFirstChecked++;
                                CheckBox chk_installment = (CheckBox)rp_installment.Items[ix].FindControl("chk_installment");
                                Label lbl_Month_id = (Label)rp_installment.Items[ix].FindControl("lbl_Month_id");
                                Label lbl_month_name = (Label)rp_installment.Items[ix].FindControl("lbl_month_name");
                                Label lbl_month_position_no = (Label)rp_installment.Items[ix].FindControl("lbl_month_position_no");
                                chk_installment.Checked = true;

                                string[] stringSeparatorss = new string[] { "," };
                                string[] arr = lbl_month_name.Text.Split(stringSeparatorss, StringSplitOptions.None);
                                foreach (string value in arr)
                                {
                                    int growcount = rd_months.Items.Count;
                                    for (int i = 0; i < growcount; i++)
                                    {
                                        CheckBox chk = (CheckBox)rd_months.Items[i].FindControl("chk_month");
                                        Label lbl_Month = (Label)rd_months.Items[i].FindControl("lbl_Month");
                                        if (lbl_Month.Text == value)
                                        {
                                            chk.Checked = true;
                                            bind_monthly_fee();
                                        }
                                    }
                                }
                                fine_calculation();
                            }
                        }
                    }
                    else
                    {
                        string monthName = mycode.get_current_monthname();
                        string monthPosition = My.get_single_column_data("select Position as Column_Name from Month_Index where Month='" + monthName + "'");
                        DataTable dtmnths = My.dataTable("select * from Month_Index where Position<='" + monthPosition + "' order by Position asc");
                        if (dtmnths.Rows.Count > 0)
                        {
                            foreach (DataRow drmnths in dtmnths.Rows)
                            {
                                ViewState["isFineUpdated"] = "0";
                                bool enable_next = true;
                                int kn = 1;
                                int growcount = rd_months.Items.Count;
                                for (int i = 0; i < rd_months.Items.Count; i++)
                                {
                                    Label lbl_Month = (Label)rd_months.Items[i].FindControl("lbl_Month");
                                    CheckBox chk = (CheckBox)rd_months.Items[i].FindControl("chk_month");

                                    if (lbl_Month.Text == drmnths["Month"].ToString())
                                    {
                                        ViewState["repeatMonthPoscc"] = My.month_position(lbl_Month.Text);
                                        if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                                        {
                                            ViewState["Dues"] = "Yes";
                                        }
                                        chk.Checked = true;
                                        try
                                        {
                                            CheckBox chk_month = (CheckBox)rd_months.Items[i + 1].FindControl("chk_month");
                                            chk_month.Enabled = true;
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                        enable_next = false;


                                        if (chk.Checked == true)
                                        {
                                            chk.Checked = true;
                                            //chk.Enabled = false;
                                            ViewState["MnthName"] = lbl_Month.Text;
                                            string month = lbl_Month.Text;
                                            bind_monthly_fee();
                                        }
                                        kn++;
                                    }
                                }
                            }
                            fine_calculation();
                        }
                    }
                }
            }
        }

        private void find_back_dues()
        {
            string qry = "select (sum(convert(float,payable))-sum(convert(float,paid))) as Dues_amt from Typewise_fee_collection where session='" + ViewState["session"].ToString() + "' and admission_no='" + ViewState["Admission_no"].ToString() + "' and feetype in ('Previous Year Dues','Back Year Dues') and class_id='" + ViewState["classid"].ToString() + "'";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                if (My.toDouble(dt.Rows[0]["Dues_amt"].ToString()) > 0)
                {
                    lbl_back_dues.Visible = true;
                    lbl_back_dues.Text = "Back Dues : " + dt.Rows[0]["Dues_amt"].ToString();
                }
                else
                {
                    string query = "select t1.Amount from Misc_Fee_Master_Studentwise t1 join admission_registor t2 on t1.Admission_No=t2.admissionserialnumber and t1.Session_id=t2.Session_id where t1.Session_id='" + ViewState["sessionIDs"].ToString() + "' and t2.Class_id='" + ViewState["classid"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "' and Old_year_Dues_Type='Yes'";
                    DataTable dtt = My.dataTable(query);
                    if (dtt.Rows.Count > 0)
                    {
                        lbl_back_dues.Text = "Back Dues : " + dtt.Rows[0]["Amount"].ToString();
                        if (My.toDouble(dtt.Rows[0]["Amount"].ToString()) > 0)
                        {
                            lbl_back_dues.Visible = true;
                        }
                        else
                        {
                            lbl_back_dues.Visible = false;
                        }
                    }
                    else
                    {
                        lbl_back_dues.Visible = false;
                    }
                }
            }
        }

        private void check_is_apply_coupon(string class_id)
        {
            DataTable dt = My.dataTable("select * from Discount_coupon_apply_classwise where Class_id='" + class_id + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Status"].ToString() == "True")
                {
                    DataTable dtp = My.dataTable("select top 1 * from Student_Payment_History where Session='" + ddlsession.SelectedItem.Text + "' and Addmission_no='" + txt_admission_no.Text + "' order by id desc");
                    if (dtp.Rows.Count > 0)
                    {
                        if (dtp.Rows[0]["Is_coupon_applied"].ToString() == "0")
                        {
                            lnk_apply_coupan.Visible = true;
                        }
                    }
                }
            }
        }

        private void bind_assigned_hostel_detail(string Hostel_Bed_id)
        {
            DataTable dtH = My.dataTable("select *,(select top 1 Hostel_name from Hostels_master where Hostel_id=t1.Hostel_id) as Hostel_name,(select top 1 Category_name from Hostel_room_category_master where Category_id=t1.Category_id) as Category_name,(select top 1 Room_name from Hostel_room_master where Hostel_id=t1.Hostel_id and Category_id=t1.Category_id and Room_id=t1.Room_id) as Room_name from Hostel_room_bed_master t1 where Bed_id='" + Hostel_Bed_id + "'");
            if (dtH.Rows.Count > 0)
            {
                lbl_hostel_name.Text = dtH.Rows[0]["Hostel_name"].ToString();
                lbl_room_type.Text = dtH.Rows[0]["Category_name"].ToString();
                lbl_room_no.Text = dtH.Rows[0]["Room_name"].ToString();
                lbl_bed_no.Text = dtH.Rows[0]["Bed_name"].ToString();
                mycode.bind_all_ddl_with_id(ddl_hostel_remove_month, "select Month,Month_Id from Month_Index where Month in (select month from Typewise_fee_collection where admission_no = '" + ViewState["Admission_no"].ToString() + "' and (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + ViewState["session"].ToString() + "' and class_id = '" + ViewState["class_id"].ToString() + "' and status = 'Dues') or Month not in (select month from Typewise_fee_collection where admission_no = '" + ViewState["Admission_no"].ToString() + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + ViewState["session"].ToString() + "' and class_id = '" + ViewState["class_id"].ToString() + "' and status = 'Paid') order by Position asc");
            }
        }


        private void section_roll_auto_assign()
        {
            AssignRollDV.Visible = false;
            //DataTable dt = PayrollMy.dataTable("select Slip_no from Student_Payment_History where Session='" + ViewState["session"].ToString() + "' and Addmission_no='" + ViewState["Admission_no"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
            //if (dt.Rows.Count == 0)
            //{
            ViewState["IsUpdateRollSec"] = "1";
            if (ViewState["Auto_roll_according_to_section"].ToString() == "1")
            {
                if (txt_section.Text.ToUpper() == "NA")
                {
                    sectionAutpDV.Visible = false;
                    AssignRollDV.Visible = true;
                    rollAutodV.Visible = true;
                    My.bind_ddl_noselect(ddl_section_assign, "select Section from section_master order by Section_order asc");
                }
            }
            else
            {
                rollAutodV.Visible = false;
                AssignRollDV.Visible = true;
                sectionAutpDV.Visible = true;
                string sectionS = "A";
                txt_assign_roll_no.Text = "1";
                txt_assign_section.Text = "A";
                DataTable dts = My.dataTable("select DISTINCT Section from admission_registor where session='" + ViewState["session"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "' order by Section desc");
                if (dts.Rows.Count > 0)
                {
                    if (dts.Rows[0]["Section"].ToString() == "NA")
                    {
                        sectionS = "A";
                    }
                    else
                    {
                        sectionS = dts.Rows[0]["Section"].ToString();
                    }


                    assign_sec_roll(ViewState["session"].ToString(), ViewState["class_id"].ToString(), sectionS, ViewState["No_of_student_in_a_section"].ToString());

                    //char section_running = sectionS[0];
                    //int roll_no = get_vacant_roll_no(ViewState["session"].ToString(), ViewState["class_id"].ToString(), sectionS);
                    //if (roll_no > My.toint(ViewState["No_of_student_in_a_section"].ToString()))
                    //{
                    //    section_running++;
                    //    roll_no = get_vacant_roll_no(ViewState["session"].ToString(), ViewState["class_id"].ToString(), section_running.ToString());
                    //    txt_assign_roll_no.Text = roll_no.ToString();
                    //    txt_assign_section.Text = section_running.ToString();
                    //}
                    //else
                    //{
                    //    txt_assign_roll_no.Text = roll_no.ToString();
                    //    txt_assign_section.Text = section_running.ToString();
                    //}
                }
            }
        }

        private void assign_sec_roll(string session, string class_id, string sectionS, string no_of_student_in_a_section)
        {
            char section_running = sectionS[0];
            bool duplicate = true;
            while (duplicate)
            {
                int roll_no = get_vacant_roll_no(session, class_id, sectionS);
                txt_assign_roll_no.Text = roll_no.ToString();
                txt_assign_section.Text = sectionS;
                if (roll_no > My.toint(no_of_student_in_a_section))
                {
                    section_running++;
                    sectionS = section_running.ToString();
                }
                else
                {
                    duplicate = false;
                }
            }
        }


        private int get_vacant_roll_no(string session, string class_id, string sectionS)
        {
            int will_assign_roll = 1;
            DataTable dts = My.dataTable("select count(rollnumber) as no_of_student from admission_registor where session='" + session + "' and Class_id='" + class_id + "' and Section='" + sectionS + "'");
            if (dts.Rows.Count > 0)
            {
                will_assign_roll = My.toint(dts.Rows[0]["no_of_student"].ToString()) + 1;

                bool dublicate_roll_no = checkdublicateroll_no(session, class_id, sectionS, will_assign_roll);
                if (dublicate_roll_no == true)
                {
                    will_assign_roll = will_assign_roll + 1;
                }

            }
            return will_assign_roll;
        }
        private bool checkdublicateroll_no(string session, string class_id, string sectionS, int rollno)
        {
            DataTable dts = My.dataTable("select Section from admission_registor where session='" + session + "' and Class_id='" + class_id + "' and Section='" + sectionS + "' and rollnumber='" + rollno + "'");
            if (dts.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
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

        private void Bind_split_month_data(string admission_no, string session_id, SqlConnection con)
        {
            DataTable dt = payments.dataTable("select * from Split_Month_Fee_Student where Admission_no='" + admission_no + "' and Session_id='" + session_id + "'", con);
            if (dt.Rows.Count > 0)
            {
                spilt_month_show.Visible = true;
                rep_total_split.DataSource = dt;
                rep_total_split.DataBind();
            }
            else
            {
                spilt_month_show.Visible = false;
                rep_total_split.DataSource = null;
                rep_total_split.DataBind();
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


        private string get_last_mnth_slip_id(string session, string class_id, string admission_no)
        {
            string slpiD = "0";
            DataTable dt = My.dataTable("select transection from Typewise_fee_collection where parameter='" + ViewState["parameter"].ToString() + "' and session='" + session + "' and class_id='" + class_id + "' and admission_no='" + admission_no + "' order by Id desc");
            if (dt.Rows.Count > 0)
            {
                slpiD = dt.Rows[0]["transection"].ToString();
            }
            return slpiD;
        }

        private void get_month_discount()
        {
            DataTable fdt = new DataTable(); DataTable feedt = new DataTable();
            DataTable dtm = PayrollMy.dataTable("select * from Month_Index order by Position asc");
            foreach (DataRow drm in dtm.Rows)
            {
                if (ViewState["transportID"].ToString() == "")
                {
                    ViewState["transportID"] = "0";
                }
                else
                {
                    ViewState["transportID"] = ViewState["transportID"].ToString();
                }
                //ViewState["parameter"] = ViewState["parameter"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";

                ViewState["parameteridS"] = "4";
                if (drm["Month_id"].ToString() != "")
                {
                    ViewState["LunchMnthName"] = drm["Month"].ToString();
                    ViewState["LunchMnthId"] = drm["Month_id"].ToString();
                }



                //====================
                if (ViewState["IsBoarding"].ToString() == "1")
                {
                    int mnthids = My.toint(drm["Month_id"].ToString());
                    if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                    {
                        ViewState["parameteridS"] = "44";
                    }
                    else
                    {
                        ViewState["parameteridS"] = "4";
                    }
                }



                string cunrt_session = ViewState["session"].ToString();
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);



                if (ViewState["IsBoarding"].ToString() == "1")
                {
                    string current_year_month = session_last_year + My.tomonth_numberstring(drm["Month"].ToString());
                    string lunch_taken_year_month = "";
                    if (My.toint(ViewState["LunchMnthId"].ToString()) == 1 || My.toint(ViewState["LunchMnthId"].ToString()) == 2 || My.toint(ViewState["LunchMnthId"].ToString()) == 3)
                    {
                        lunch_taken_year_month = session_last_year + ViewState["LunchMnthId"].ToString();
                    }
                    else
                    {
                        lunch_taken_year_month = session_frst_year + ViewState["LunchMnthId"].ToString();
                    }


                    if (My.toint(lunch_taken_year_month) <= My.toint(current_year_month))
                    {
                        ViewState["parameteridS"] = "44";
                    }
                    else
                    {
                        ViewState["parameteridS"] = "4";
                    }
                }
                string type;
                if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + drm["Month"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' and transection!=''").Rows.Count > 0)
                {
                    feedt = My.dataTable("select 'Monthly' as Type, '" + drm["Month"].ToString() + "' as Month, isnull(sum(convert(float, disc_amount)),'0') as Total_disc from (select Disc as disc_amount from dbo.[Typewise_fee_collection]   where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + drm["Month"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' and content_id!='6121'  and transection!='') t");
                    type = "Calculated";
                }
                else
                {
                    Dictionary<string, object> dc = new Dictionary<string, object>();
                    dc["admission_no"] = txt_admission_no.Text;
                    dc["session_id"] = ViewState["sessionIDs"].ToString();
                    dc["class"] = lblclass.Text;
                    dc["session"] = ViewState["session"].ToString();
                    dc["class_id"] = ViewState["classid"].ToString();
                    dc["hosteltaken"] = lblhostel.Text.ToLower();
                    dc["months"] = drm["Month"].ToString();
                    dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                    dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                    dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                    dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                    dc["day_boarding"] = ViewState["day_bording"].ToString();
                    dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();
                    dc["category_id"] = ViewState["category_id"].ToString();
                    dc["sub_category_id"] = ViewState["sub_category_id"].ToString();
                    dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                    dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                    dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                    dc["parameter_id"] = ViewState["parameteridS"].ToString();
                    string monthid = My.tomonth_numberstring(drm["Month"].ToString());
                    int pay_month = My.toint(monthid);
                    s_year = My.check_start_months(pay_month, s_year);
                    dc["monthid"] = s_year + monthid;
                    feedt = My.dataTableSP("sp_fetch_monthly_fee_discount", dc);
                    type = "NotCalculated";
                }
                foreach (DataRow dr in feedt.Rows)
                {
                    try
                    {
                        fdt.Rows.Add(dr.ItemArray);
                    }
                    catch
                    {
                        foreach (DataColumn dc in feedt.Columns)
                        {
                            fdt.Columns.Add(dc.ColumnName);
                        }
                        fdt.Rows.Add(dr.ItemArray);
                    }
                }
            }


            if (fdt.Rows.Count > 0)
            {
                rp_monthly_discount.DataSource = fdt;
                rp_monthly_discount.DataBind();
            }
            else
            {
                rp_monthly_discount.DataSource = null;
                rp_monthly_discount.DataBind();
            }
        }


        private void find_admission_dues_fee()
        {
            ddl_extra_head_for.Items.Insert(0, new ListItem("AdmissionFee", "2"));
            ddl_extra_head_for.SelectedValue = "1";
            string qry = "";
            string parameter_id = "";
            string parameter_id2 = "";
            string parameter = "";
            string parameter2 = "";

            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                parameter_id = "1";// annulfee
                parameter_id2 = "5";// admission fee for hostel

                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    parameter = "AdmissionFee";
                }
                else
                {
                    parameter = "HostelAdmissionFee";
                }
            }
            else
            {
                parameter_id = "2";// annulfee
                parameter_id2 = "6";// admission fee for hostel 
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    parameter = "AnnualFee";
                }
                else
                {
                    parameter = "HostelAnnualFee";
                }
            }



            if (ViewState["Hostel_id"].ToString() == "0")
            {

                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + txt_admission_no.Text + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + txt_admission_no.Text + "' and (parameter='" + parameter + "')  and session='" + ddlsessionad.SelectedItem.Text + "' and class_id=" + ViewState["classid"].ToString() + ")t";

            }
            else
            {
                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + txt_admission_no.Text + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + txt_admission_no.Text + "' and (parameter='" + parameter + "' )  and session='" + ddlsessionad.SelectedItem.Text + "' and class_id=" + ViewState["classid"].ToString() + ")t";

            }


            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable, '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ddlsessionad.SelectedItem.Text + "' and Admission_No='" + txt_admission_no.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + txt_admission_no.Text + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + ddlsessionad.SelectedItem.Text + "' and class_id='" + ViewState["classid"].ToString() + "') t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable, '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ddlsessionad.SelectedItem.Text + "' and Admission_No='" + txt_admission_no.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + txt_admission_no.Text + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + ddlsessionad.SelectedItem.Text + "' and class_id='" + ViewState["classid"].ToString() + "' and  fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "' ) t";
                }
                fee_dt = My.dataTable(qry);
            }


            DataTable dt = PayrollMy.dataTable(qry);
            if (dt.Rows.Count == 0)
            {
                chk_collect_adm_ann_fee.Visible = false;
                btn_admission_fees.Visible = false;
                lbl_collect_adm_ann_fee.Visible = false;
            }
            else
            {
                lbl_collect_adm_ann_fee.Visible = false;
                chk_collect_adm_ann_fee.Visible = false;
                btn_admission_fees.Visible = false;
                double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0; string Payment_status = "Paid";
                foreach (DataRow dr in dt.Rows)
                {
                    dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                    payable += My.toDouble(dr["payable"]);
                    paid += My.toDouble(dr["paid"]);
                    dues += My.toDouble(dr["dues"]);
                    disc += My.toDouble(dr["disc_amount"]);
                    payble_after_disc += My.toDouble(dr["payable_after_disc"]);
                    if (Payment_status == "Paid")
                    {
                        if (dr["status"].ToString() == "Dues")
                        {
                            Payment_status = "Dues";
                        }
                    }
                }

                chk_collect_adm_ann_fee.Text = "Is collect admission fees : "; // + payble_after_disc.ToString("0.00");
                btn_admission_fees.InnerText = "Admission Dues : " + payble_after_disc.ToString("0.00");
                lbl_amount_to_be_paid.Text = "Amount to be paid for admission Fees : ";
                admfeeheadtittle.InnerText = "Fee Details for Admission";
                lbl_collect_adm_ann_fee.Text = payble_after_disc.ToString();
                txt_adm_ann_fee.Text = "0";
                if (Payment_status == "Dues")
                {
                    txt_is_added.Text = "0";
                    txt_adm_total.Text = payable.ToString();
                    txt_adm_disc_amt.Text = disc.ToString();
                    txt_adm_prev_paid.Text = paid.ToString();
                    txt_adm_net_payble.Text = payble_after_disc.ToString();

                    txt_adm_ann_fee.Text = payble_after_disc.ToString();

                    txttotalbill.Text = "0";
                    lbl_adission_monthly_dues_till_now.Visible = true;
                    chk_collect_adm_ann_fee.Visible = true;

                    if (ViewState["Is_adm_fee_not_check"].ToString() == "0")
                    {
                        chk_collect_adm_ann_fee.Checked = true;
                    }
                    lbl_collect_adm_ann_fee.Visible = true;
                    btn_admission_fees.Visible = true;
                    Bind_fee_details();
                }
            }
        }

        private void find_annual_dues_fee()
        {
            ddl_extra_head_for.Items.Insert(0, new ListItem("AnnualFee", "3"));
            ddl_extra_head_for.SelectedValue = "1";
            string qry = "";
            string parameter_id = "";
            string parameter_id2 = "";
            string parameter = "";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    parameter = "AdmissionFee";
                }
                else
                {
                    parameter = "HostelAdmissionFee";
                }
            }
            else
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    parameter = "AnnualFee";
                }
                else
                {
                    parameter = "HostelAnnualFee";
                }
            }



            parameter_id = "2";// annulfee
            parameter_id2 = "6";// admission fee for hostel

            if (ViewState["Hostel_id"].ToString() == "0")
            {

                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + txt_admission_no.Text + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + txt_admission_no.Text + "' and (parameter='" + parameter + "')  and session='" + ddlsessionad.SelectedItem.Text + "' and class_id=" + ViewState["classid"].ToString() + ")t";

            }
            else
            {
                parameter = "HostelAnnualFee";
                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + txt_admission_no.Text + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + txt_admission_no.Text + "' and (parameter='" + parameter + "' )  and session='" + ddlsessionad.SelectedItem.Text + "' and class_id=" + ViewState["classid"].ToString() + ")t";

            }


            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ddlsessionad.SelectedItem.Text + "' and Admission_No='" + txt_admission_no.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + txt_admission_no.Text + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + ddlsessionad.SelectedItem.Text + "' and class_id='" + ViewState["classid"].ToString() + "') t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ddlsessionad.SelectedItem.Text + "' and Admission_No='" + txt_admission_no.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + txt_admission_no.Text + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + ddlsessionad.SelectedItem.Text + "' and class_id='" + ViewState["classid"].ToString() + "' and  fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t";
                }
                fee_dt = My.dataTable(qry);
            }



            DataTable dt = PayrollMy.dataTable(qry);
            if (dt.Rows.Count == 0)
            {
                lbl_collect_adm_ann_fee.Visible = false;
                chk_collect_adm_ann_fee.Visible = false;
                btn_admission_fees.Visible = false;
            }
            else
            {
                lbl_collect_adm_ann_fee.Visible = false;
                chk_collect_adm_ann_fee.Visible = false;
                btn_admission_fees.Visible = false;
                double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0; string Payment_status = "Paid";
                foreach (DataRow dr in dt.Rows)
                {
                    dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                    payable += My.toDouble(dr["payable"]);
                    paid += My.toDouble(dr["paid"]);
                    dues += My.toDouble(dr["dues"]);
                    disc += My.toDouble(dr["disc_amount"]);
                    payble_after_disc += My.toDouble(dr["payable_after_disc"]);

                    if (Payment_status == "Paid")
                    {
                        if (dr["status"].ToString() == "Dues")
                        {
                            Payment_status = "Dues";
                        }
                    }
                }
                string prev_dues = gt_previous_amount();
                double totalpay = payble_after_disc + My.toDouble(prev_dues);
                btn_admission_fees.InnerText = "Annual DUES : " + totalpay.ToString("0.00");
                if (ViewState["firm_id"].ToString() == "DIS-01")
                {
                    btn_admission_fees.InnerText = "Annual : " + totalpay.ToString("0.00");
                }
                chk_collect_adm_ann_fee.Text = "Is collect annual fees : ";// + totalpay.ToString("0.00"); 
                lbl_collect_adm_ann_fee.Text = totalpay.ToString();
                lbl_amount_to_be_paid.Text = "Amount to be paid for Annual Fees: ";
                admfeeheadtittle.InnerText = "Fee Details for Annual";
                if (Payment_status == "Dues")
                {
                    txt_is_added.Text = "0";
                    txt_adm_total.Text = payable.ToString();
                    txt_adm_disc_amt.Text = disc.ToString();
                    txt_adm_prev_paid.Text = paid.ToString();
                    txt_adm_net_payble.Text = payble_after_disc.ToString();
                    txt_adm_ann_fee.Text = payble_after_disc.ToString();
                    txttotalbill.Text = "0";
                    lbl_adission_monthly_dues_till_now.Visible = true;
                    chk_collect_adm_ann_fee.Visible = true;
                    if (ViewState["Is_adm_fee_not_check"].ToString() == "0")
                    {
                        chk_collect_adm_ann_fee.Checked = true;
                    }
                    lbl_collect_adm_ann_fee.Visible = true;
                    btn_admission_fees.Visible = true;


                    Bind_fee_details();
                }
            }
        }

        private string gt_previous_amount()
        {
            string ReturN = "0";
            DataTable dt = PayrollMy.dataTable("select Dues_Amount from Previous_Year_Dues  where Session_id='" + ViewState["sessionIDs"].ToString() + "' and AdmissionNumber='" + txt_admission_no.Text + "'  and Class_id='" + ViewState["classid"].ToString() + "' and Status='Unpaid'");
            if (dt.Rows.Count > 0)
            {
                ReturN = dt.Rows[0][0].ToString();
            }
            return ReturN;
        }

        private void fetch_ledger()
        {
            DataTable dt = PayrollMy.dataTable("select * from (select Slip_no,Date,Type,mode,Description,Amount,Idate,Type as FeeType,Addmission_no,Class_id," + ViewState["sessionIDs"].ToString() + " as Session_id,Entry_id from Student_Payment_History  where Session='" + ViewState["session"].ToString() + "' and Addmission_no='" + ViewState["Admission_no"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "') t ORDER BY Idate asc");
            if (dt.Rows.Count == 0)
            {
                print1.Visible = false;
                rl_ledger.DataSource = null;
                rl_ledger.DataBind();

            }
            else
            {
                print1.Visible = true;
                rl_ledger.DataSource = dt;
                rl_ledger.DataBind();

            }
        }
        double ttl_ledger_amt = 0;
        protected void rl_ledger_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_fee_type = ((Label)e.Item.FindControl("lbl_fee_type")) as Label;
                Label lbl_admission_no = ((Label)e.Item.FindControl("lbl_admission_no")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_Receipt_No = ((Label)e.Item.FindControl("lbl_Receipt_No")) as Label;
                Label lbl_entry_id = ((Label)e.Item.FindControl("lbl_entry_id")) as Label;
                HtmlAnchor printLnk = (HtmlAnchor)e.Item.FindControl("printLnk");


                //if (lbl_fee_type.Text == "Admission")
                //{
                //    printLnk.HRef = "slip/Admission_slip.aspx?admissionno=" + lbl_admission_no.Text + "&sessionid=" + lbl_session_id.Text + "&classid=" + lbl_class_id.Text + "&Slip_no=" + lbl_Receipt_No.Text;
                //}
                //else if (lbl_fee_type.Text == "Annual")
                //{
                //    printLnk.HRef = "slip/annual-slip.aspx?admissionno=" + lbl_admission_no.Text + "&sessionid=" + lbl_session_id.Text + "&classid=" + lbl_class_id.Text + "&Slip_no=" + lbl_Receipt_No.Text;
                //}

                if (lbl_fee_type.Text == "FormSale")
                {
                    printLnk.HRef = "slip/Form_Sale_Slip.aspx?transaction=" + lbl_entry_id.Text;
                }
                else
                {
                    string page = "slip/monthly-slip.aspx";
                    revisedPayment.Visible = true;
                    if (ViewState["Monthly_bill_type"].ToString() == "A5")
                    {
                        page = "slip/monthly-slip-a5.aspx";
                    }
                    if (ViewState["Monthly_bill_type"].ToString() == "A5D")
                    {
                        page = "slip/receipt-details-a5.aspx";
                    }
                    if (ViewState["Monthly_bill_type"].ToString() == "A5S")
                    {
                        page = "slip/bill-with-scholarship.aspx";
                    }
                    if (ViewState["Monthly_bill_type"].ToString() == "A5I")
                    {
                        page = "slip/bill-installment.aspx";
                    }
                    if (ViewState["Monthly_bill_type"].ToString() == "A5SNGL")
                    {
                        page = "slip/bill-slip-a5-single.aspx";
                    }
                    if (ViewState["Monthly_bill_type"].ToString() == "SPS-001")
                    {
                        page = "slip/payment-slip.aspx";
                    }
                    printLnk.HRef = page + "?admissionno=" + lbl_admission_no.Text + "&sessionid=" + lbl_session_id.Text + "&classid=" + lbl_class_id.Text + "&Slip_no=" + lbl_Receipt_No.Text;
                }

                Label lbl_amount = ((Label)e.Item.FindControl("lbl_amount")) as Label;
                ttl_ledger_amt = ttl_ledger_amt + My.toDouble(lbl_amount.Text);

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    printLnk.Visible = true;
                }
                else
                {
                    revisedPayment.Visible = false;
                    printLnk.Visible = false;
                }
            }
            lbl_ttl_ledger_amt.Text = ttl_ledger_amt.ToString("0.00");
        }
        protected void btn_admission_fee_Click(object sender, EventArgs e)
        {
            DataTable dtBnk = My.dataTable("select * from BANK_MASTER where Status='1' order by Bank_name asc");
            string url = "";
            if (lbl_dues_pay_type.Text == "AdmissionFees")
            {
                Bind_fee_details();
                find_all_paid_fee();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
                //url = "collect-admission-fees.aspx?admissionno=" + lbl_admission_no.Text + "&sessionid=" + ViewState["sessionIDs"].ToString() + "&classid=" + ViewState["classid"].ToString() + "&type=monthleyfee";
            }
            else
            {
                url = "collect-annual-fees.aspx?admissionno=" + lbl_admission_no.Text + "&sessionid=" + ViewState["sessionIDs"].ToString() + "&classid=" + ViewState["classid"].ToString() + "&type=monthleyfee";
                Response.Redirect(url, false);
            }
            // Response.Redirect(url); 
        }

        protected void btn_set_discount_Click(object sender, EventArgs e)
        {
            Response.Redirect("set-student-wise-discount.aspx?adm=" + lbl_admission_no.Text + "&type=monthleyfee", false);
        }


        private void find_all_due_fee()
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("value");
            dtDatas.Columns.Add("fee_amount");
            dtDatas.Columns.Add("discount_per");
            dtDatas.Columns.Add("paid_peev");
            dtDatas.Columns.Add("paid_status");
            dtDatas.Columns.Add("bac_colour");
            int temp;
            List<string> lst = new List<string>();
            string temp_month = My.get_start_month();
            for (temp = 1; temp <= 12; temp++)
            {
                DataTable dtstnG = My.dataTable("select Pair_group_id from Custome_month_selection_setting where Month_name='" + temp_month + "'");
                if (dtstnG.Rows.Count > 0)
                {
                    string months = My.get_months_group(dtstnG.Rows[0]["Pair_group_id"].ToString());
                    DataTable paid_dt = My.dataTable("select month,status from dbo.[Typewise_fee_collection] where session='" + ddlsession.SelectedItem.Text + "' and admission_no='" + txt_admission_no.Text + "' and parameter like '%" + ViewState["parameter"].ToString() + "%' and month in (" + months + ")");
                    if (paid_dt.Rows.Count > 0)
                    {
                        string remove_month = "";
                        foreach (DataRow pdr in paid_dt.Rows)
                        {
                            if (pdr["status"].ToString() == "Dues")
                            {
                                lst.Add(temp_month);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (ViewState["Is_quarterwise_payment"].ToString() == "1")
                        {
                            DataTable dtFee = My.dataTable("select isnull(sum(convert(float, amount)),0) as Total_fee from Fee_master_content_wise where parameter_id='" + ViewState["parameteridS"].ToString() + "' and session_id='" + ViewState["sessionIDs"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and Month='" + temp_month + "'");
                            {
                                if (My.toDouble(dtFee.Rows[0]["Total_fee"].ToString()) == 0)
                                {
                                }
                                else
                                {
                                    lst.Add(temp_month);
                                }
                            }
                        }
                        else
                        {
                            lst.Add(temp_month);
                        }
                    }
                }
                else
                {
                    DataTable paid_dt = My.dataTable("select month,status from dbo.[Typewise_fee_collection] where   session='" + ddlsession.SelectedItem.Text + "' and admission_no='" + txt_admission_no.Text + "' and parameter like '%" + ViewState["parameter"].ToString() + "%' and month='" + temp_month + "'");
                    if (paid_dt.Rows.Count > 0)
                    {
                        string remove_month = "";
                        foreach (DataRow pdr in paid_dt.Rows)
                        {
                            if (pdr["status"].ToString() == "Dues")
                            {
                                lst.Add(temp_month);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (ViewState["Is_quarterwise_payment"].ToString() == "1")
                        {
                            DataTable dtFee = My.dataTable("select isnull(sum(convert(float, amount)),0) as Total_fee from Fee_master_content_wise where parameter_id='" + ViewState["parameteridS"].ToString() + "' and session_id='" + ViewState["sessionIDs"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and Month='" + temp_month + "'");
                            {
                                if (My.toDouble(dtFee.Rows[0]["Total_fee"].ToString()) == 0)
                                {
                                }
                                else
                                {
                                    lst.Add(temp_month);
                                }
                            }
                        }
                        else
                        {
                            lst.Add(temp_month);
                        }
                    }
                }

                temp_month = find_month(temp_month);
            }

            int i = 0;
            foreach (string str in lst)
            {
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["Month"] = lst[i];
                drNewRow["value"] = false;
                drNewRow["paid_status"] = "NotCreated";
                dtDatas.Rows.Add(drNewRow);
                dtDatas.AcceptChanges();
                i++;
            }
            find_prev_dues(dtDatas);
            //GridView2.DataSource = dtDatas.DefaultView;
            //GridView2.DataBind();


            rd_months.DataSource = dtDatas.DefaultView;
            rd_months.DataBind();
            ViewState["duesmonthDT"] = dtDatas;
        }



        DataTable prevdues_dt = new DataTable();
        private void find_prev_dues(DataTable dtDatas)
        {
            prevdues_dt = My.dataTable(" select sum(isnull(cast(dues as float),'0')) dues,month from dbo.[Typewise_fee_collection] where session='" + ddlsession.SelectedItem.Text + "' and status='Dues' and Class='" + ddlclass.Text + "' and admission_no='" + txt_admission_no.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' group by month");
            foreach (DataRow mr in dtDatas.Rows)
            {
                var row = prevdues_dt.Select("month='" + mr["Month"].ToString() + "'");
                if (row.Length > 0)
                {
                    mr["paid_status"] = "Created";
                    mr["bac_colour"] = "Yellow";
                }
            }
            show_dues(dtDatas);
        }


        double anula_dues = 0; double prev_session_dues = 0;
        double admission_dues = 0; string adm_transection = "";
        private void show_dues(DataTable dtDatas)
        {
            double month_dues = 0;
            foreach (DataRow mr in dtDatas.Rows)
            {
                if (My.toBool(mr["Value"]))
                {
                    var row = prevdues_dt.Select("month='" + mr["Month"].ToString() + "'");
                    if (row.Length > 0)
                    {
                        DataRow dr = row[0];
                        month_dues += My.toDouble(dr["dues"]);
                    }
                }
            }
            if (month_dues + admission_dues + anula_dues + prev_session_dues == 0)
            {
                //txt_previousduesmonth.Text = "0";
                //chk_prev_dues.Visibility = Visibility.Collapsed;
                //chk_prev_dues.Uid = "0";
                //txt_view.Visibility = Visibility.Collapsed;
            }
            else
            {
                //chk_prev_dues.IsChecked = true;
                //chk_prev_dues.Visibility = Visibility.Visible;
                //txt_previousduesmonth.Text = (month_dues + admission_dues + anula_dues + prev_session_dues).ToString();
                //chk_prev_dues.Content = "Previous dues (Rs. " + txt_previousduesmonth.Text + ")";
                //chk_prev_dues.Uid = txt_previousduesmonth.Text;
                //txt_view.Visibility = Visibility.Visible;
            }
        }

        private string find_month(string month)
        {
            string next = "nn";
            switch (month)
            {
                case "January":
                    next = "February";
                    return next;

                case "February":
                    next = "March";
                    return next;

                case "March":
                    next = "April";
                    return next;

                case "April":
                    next = "May";
                    return next;

                case "May":
                    next = "June";
                    return next;

                case "June":
                    next = "July";
                    return next;

                case "July":
                    next = "August";
                    return next;

                case "August":
                    next = "September";
                    return next;

                case "September":
                    next = "October";
                    return next;

                case "October":
                    next = "November";
                    return next;

                case "November":
                    next = "December";
                    return next;

                case "December":
                    next = "January";
                    return next;

            }
            return next;
        }

        protected void chk_month_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txt_paid_amount.Focus();
                if (ViewState["check_one_more_months"].ToString() == "0")
                {
                    ViewState["check_one_more_months"] = "1";
                    ViewState["Dues"] = "No";
                    ViewState["DuesCalculate"] = "No";
                }



                ViewState["no_of_months"] = "1";
                ViewState["more_months_check_status"] = "No";
                CheckBox lnkc = (CheckBox)sender;
                RepeaterItem rowc = (RepeaterItem)lnkc.NamingContainer;
                int rowindexc = rowc.ItemIndex;
                Label lbl_Monthc = (Label)rowc.FindControl("lbl_Month");
                CheckBox chkc = (CheckBox)rowc.FindControl("chk_month");
                if (chkc.Checked == true)
                {
                    ViewState["repeatMonthPos"] = My.month_position(lbl_Monthc.Text);
                    DataTable dtms = My.dataTable("select * from Custome_month_selection_setting where Month_name='" + lbl_Monthc.Text + "'");
                    if (dtms.Rows.Count > 0)
                    {
                        ViewState["no_of_months"] = My.toIntS(dtms.Rows[0]["No_of_month_selection"].ToString());
                        ViewState["more_months_check_status"] = "Yes";
                    }
                }




                if (ViewState["Is_month_selection_free"].ToString() == "1")
                {
                    int month_position = My.toIntS(get_month_position(lbl_Monthc.Text));
                    if (chkc.Checked == false)
                    {
                        int growcount_chbx = rd_months.Items.Count;
                        for (int ichk = 0; ichk < growcount_chbx; ichk++)
                        {
                            CheckBox chkbx = (CheckBox)rd_months.Items[ichk].FindControl("chk_month");
                            chkbx.Checked = false;
                        }
                        pnl_month_wise_fee_details.Visible = false;
                        rp_fee_details.DataSource = null;
                        rp_fee_details.DataBind();
                        bind_ttl_fee();
                        month_position = month_position - 1;
                    }


                    DataTable dtmnths = My.dataTable("select * from Month_Index where Position<=" + month_position + " order by Position asc");
                    if (dtmnths.Rows.Count > 0)
                    {
                        foreach (DataRow drmnths in dtmnths.Rows)
                        {
                            CheckBox lnkFine = (CheckBox)sender;
                            RepeaterItem rowFine = (RepeaterItem)lnkFine.NamingContainer;
                            int rowindexFine = rowFine.ItemIndex;
                            Label lbl_MonthFine = (Label)rowFine.FindControl("lbl_Month"); //drmnths["Month"].ToString(); // 

                            ViewState["isFineUpdated"] = "0";
                            bool enable_next = true;
                            int kn = 1;
                            int growcount = rd_months.Items.Count;
                            for (int i = 0; i < growcount; i++)
                            {
                                CheckBox chk = (CheckBox)rd_months.Items[i].FindControl("chk_month");
                                Label lbl_Month = (Label)rd_months.Items[i].FindControl("lbl_Month");
                                if (lbl_Month.Text == drmnths["Month"].ToString())
                                {
                                    ViewState["repeatMonthPoscc"] = My.month_position(lbl_Month.Text);
                                    if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                                    {
                                        ViewState["Dues"] = "Yes";
                                    }

                                    chk.Checked = true;
                                    try
                                    {
                                        CheckBox chk_month = (CheckBox)rd_months.Items[i + 1].FindControl("chk_month");
                                        chk_month.Enabled = true;
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                    enable_next = false;


                                    if (chk.Checked == true)
                                    {
                                        chk.Checked = true;
                                        string month = lbl_Month.Text;
                                        bind_monthly_fee();
                                    }
                                    else
                                    {
                                        bind_monthly_fee();
                                    }
                                    kn++;
                                }
                            }
                        }
                        fine_calculation();
                    }
                    else
                    {
                        fine_calculation();
                    }
                }
                else
                {
                    if (ViewState["more_months_check_status"].ToString() == "Yes")
                    {
                        CheckBox lnkFine = (CheckBox)sender;
                        RepeaterItem rowFine = (RepeaterItem)lnkFine.NamingContainer;
                        int rowindexFine = rowFine.ItemIndex;
                        Label lbl_MonthFine = (Label)rowFine.FindControl("lbl_Month");

                        ViewState["isFineUpdated"] = "0";
                        bool enable_next = true;
                        int kn = 1;
                        int growcount = rd_months.Items.Count;
                        for (int i = 0; i < growcount; i++)
                        {
                            CheckBox chk = (CheckBox)rd_months.Items[i].FindControl("chk_month");
                            Label lbl_Month = (Label)rd_months.Items[i].FindControl("lbl_Month");
                            ViewState["repeatMonthPoscc"] = My.month_position(lbl_Month.Text);
                            if (My.toint(ViewState["repeatMonthPoscc"].ToString()) >= My.toint(ViewState["repeatMonthPos"].ToString()))
                            {
                                int checked_mnths = My.toIntS(ViewState["no_of_months"].ToString());
                                if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                                {
                                    ViewState["Dues"] = "Yes";
                                }

                                if (checked_mnths > kn)
                                {
                                    chk.Checked = true;
                                }
                                if (kn == checked_mnths)
                                {
                                    chk.Checked = true;
                                    if (enable_next == true)
                                    {
                                        try
                                        {
                                            CheckBox chk_month = (CheckBox)rd_months.Items[i + 1].FindControl("chk_month");
                                            chk_month.Enabled = true;
                                        }
                                        catch { }
                                    }
                                    enable_next = false;
                                }

                                if (enable_next == true)
                                {
                                    try
                                    {
                                        CheckBox chk_month = (CheckBox)rd_months.Items[i + 1].FindControl("chk_month");
                                        chk_month.Enabled = true;
                                    }
                                    catch { }
                                }
                                if (chk.Checked == true)
                                {
                                    chk.Checked = true;
                                    chk.Enabled = false;
                                    string month = lbl_Month.Text;
                                    bind_monthly_fee();
                                }
                                else
                                {
                                    bind_monthly_fee();
                                }
                                kn++;
                            }
                        }
                        fine_calculation();
                    }
                    else
                    {
                        CheckBox lnk = (CheckBox)sender;
                        RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                        int rowindex = row.ItemIndex;
                        try
                        {
                            CheckBox chk_month = (CheckBox)rd_months.Items[rowindex + 1].FindControl("chk_month");
                            chk_month.Enabled = true;
                        }
                        catch { }
                        Label lbl_Month = (Label)row.FindControl("lbl_Month");
                        CheckBox chk = (CheckBox)row.FindControl("chk_month");
                        if (chk.Checked == true)
                        {
                            chk.Checked = true;
                            chk.Enabled = false;
                            string month = lbl_Month.Text;
                            bind_monthly_fee();
                        }
                        else
                        {
                            bind_monthly_fee();
                        }

                        fine_calculation();
                    }
                }


            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        protected void chk_installment_CheckedChanged(object sender, EventArgs e)
        {
            string ischecked = "1";
            CheckBox lnkc = (CheckBox)sender;
            RepeaterItem rowc = (RepeaterItem)lnkc.NamingContainer;
            int rowindexc = rowc.ItemIndex;
            Label lbl_month_name = (Label)rowc.FindControl("lbl_month_name");
            CheckBox chkc = (CheckBox)rowc.FindControl("chk_installment");
            if (chkc.Checked == true)
            {
                int growcounts = rp_installment.Items.Count;
                for (int y = 0; y < growcounts; y++)
                {
                    CheckBox chk_installment = (CheckBox)rp_installment.Items[y].FindControl("chk_installment");
                    Label lbl_month_nameL = (Label)rp_installment.Items[y].FindControl("lbl_month_name");

                    if (ischecked == "1")
                    {
                        chk_installment.Checked = true;
                        string[] stringSeparatorss = new string[] { "," };
                        string[] arr = lbl_month_nameL.Text.Split(stringSeparatorss, StringSplitOptions.None);
                        foreach (string value in arr)
                        {
                            int growcount = rd_months.Items.Count;
                            for (int i = 0; i < growcount; i++)
                            {
                                CheckBox chk = (CheckBox)rd_months.Items[i].FindControl("chk_month");
                                Label lbl_Month = (Label)rd_months.Items[i].FindControl("lbl_Month");
                                if (lbl_Month.Text == value)
                                {
                                    chk.Checked = true;
                                    bind_monthly_fee();
                                }
                            }
                        }
                        if (lbl_month_nameL.Text == lbl_month_name.Text)
                        {
                            ischecked = "0";
                        }
                    }
                }
                fine_calculation();
            }
            else
            {
                int growcounts = rp_installment.Items.Count;
                for (int y = growcounts - 1; y >= 0; y--)
                {
                    CheckBox chk_installment = (CheckBox)rp_installment.Items[y].FindControl("chk_installment");
                    Label lbl_month_nameL = (Label)rp_installment.Items[y].FindControl("lbl_month_name");

                    // You can now work with chk_installment and lbl_month_nameL
                    if (ischecked == "1")
                    {
                        chk_installment.Checked = false;
                        string[] stringSeparatorss = new string[] { "," };
                        string[] arr = lbl_month_nameL.Text.Split(stringSeparatorss, StringSplitOptions.None);
                        foreach (string value in arr)
                        {
                            int growcount = rd_months.Items.Count;
                            for (int i = 0; i < growcount; i++)
                            {
                                CheckBox chk = (CheckBox)rd_months.Items[i].FindControl("chk_month");
                                Label lbl_Month = (Label)rd_months.Items[i].FindControl("lbl_Month");
                                if (lbl_Month.Text == value)
                                {
                                    chk.Checked = false;
                                    bind_monthly_fee();
                                }
                            }
                        }
                        if (lbl_month_nameL.Text == lbl_month_name.Text)
                        {
                            ischecked = "0";
                        }
                    }
                }

                //string[] stringSeparatorss = new string[] { "," };
                //string[] arr = lbl_month_name.Text.Split(stringSeparatorss, StringSplitOptions.None);
                //foreach (string value in arr)
                //{
                //    int growcount = rd_months.Items.Count;
                //    for (int i = 0; i < growcount; i++)
                //    {
                //        CheckBox chk = (CheckBox)rd_months.Items[i].FindControl("chk_month");
                //        Label lbl_Month = (Label)rd_months.Items[i].FindControl("lbl_Month");
                //        if (lbl_Month.Text == value)
                //        {
                //            chk.Checked = false;
                //            bind_monthly_fee();
                //        }
                //    }
                //}
                fine_calculation();
            }
        }


        private string get_month_position(string month_name)
        {
            string mnth_position = "0";
            DataTable dt = My.dataTable("select Position from Month_Index where Month='" + month_name + "'");
            if (dt.Rows.Count > 0)
            {
                mnth_position = dt.Rows[0]["Position"].ToString();
            }
            return mnth_position;
        }


        private void bind_monthly_fee()
        {
            ViewState["OnPayDateChange"] = "1";
            double fee_amount = 0, disc_amount = 0, paid_previously = 0;
            bool success = false;
            DataTable fdt = new DataTable();
            int growcountS = rd_months.Items.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                string cunrt_session = ViewState["session"].ToString();
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);

                Label lbl_Month = (Label)rd_months.Items[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)rd_months.Items[iS].FindControl("chk_month");

                fee_amount = 0; disc_amount = 0; paid_previously = 0;
                DataTable feedt = new DataTable();
                if (My.toBool(chk_month.Checked))
                {
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        string current_year_month = session_last_year + My.tomonth_numberstring(lbl_Month.Text);
                        string lunch_taken_year_month = "";
                        if (My.toint(ViewState["LunchMnthId"].ToString()) == 1 || My.toint(ViewState["LunchMnthId"].ToString()) == 2 || My.toint(ViewState["LunchMnthId"].ToString()) == 3)
                        {
                            lunch_taken_year_month = session_last_year + ViewState["LunchMnthId"].ToString();
                        }
                        else
                        {
                            lunch_taken_year_month = session_frst_year + ViewState["LunchMnthId"].ToString();
                        }


                        if (My.toint(lunch_taken_year_month) <= My.toint(current_year_month))
                        {
                            ViewState["parameteridS"] = "44";
                        }
                        else
                        {
                            ViewState["parameteridS"] = "4";
                        }
                    }
                    string type = "";
                    //dr["paid_status"] = "Created";
                    //dr["bac_colour"] = "Yellow";
                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' and transection!=''").Rows.Count > 0)
                    {
                        feedt = My.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter, feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' and transection!=''");
                        type = "Calculated";
                    }
                    else
                    {
                        //dr["bac_colour"] = "White";
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = txt_admission_no.Text;
                        dc["session_id"] = ViewState["sessionIDs"].ToString();
                        dc["class"] = lblclass.Text;
                        dc["session"] = ViewState["session"].ToString();
                        dc["class_id"] = ViewState["classid"].ToString();
                        dc["hosteltaken"] = lblhostel.Text.ToLower();
                        dc["months"] = lbl_Month.Text;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                        dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                        dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();
                        dc["category_id"] = ViewState["category_id"].ToString();
                        dc["sub_category_id"] = ViewState["sub_category_id"].ToString();
                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                        dc["parameter_id"] = ViewState["parameteridS"].ToString();

                        //new08/08/2022 
                        string monthid = My.tomonth_numberstring(lbl_Month.Text);
                        int pay_month = My.toint(monthid);
                        s_year = My.check_start_months(pay_month, s_year);
                        dc["monthid"] = s_year + monthid;

                        feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                        feedt.Columns.Add("previously_paid");
                        type = "NotCalculated";
                    }
                    feedt.Columns.Add("total_payable");

                    string month = "";
                    double total = 0, fee = 0, disc = 0, paid_prev = 0;
                    foreach (DataRow dr in feedt.Rows)
                    {
                        //if (type == "Calculated")
                        //{
                        //    My.exeSql("update dbo.[Typewise_fee_collection] set Disc='" + dr["disc_amount"].ToString() + "' where Id='" + dr["id"].ToString() + "'");
                        //}
                        month = dr["months"].ToString();
                        dr["total_payable"] = My.toDouble(dr["amount"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["previously_paid"]);
                        fee += My.toDouble(dr["amount"]);
                        disc += My.toDouble(dr["disc_amount"]);
                        paid_prev += My.toDouble(dr["previously_paid"]);

                        total += My.toDouble(dr["total_payable"]);
                    }

                    foreach (DataRow dr in feedt.Rows)
                    {
                        try
                        {
                            fdt.Rows.Add(dr.ItemArray);
                        }
                        catch
                        {
                            foreach (DataColumn dc in feedt.Columns)
                            {
                                fdt.Columns.Add(dc.ColumnName);
                            }
                            fdt.Rows.Add(dr.ItemArray);
                        }
                    }

                    //lbl_fee_month.Text = month;
                    //lbl_fee_amount.Text = fee.ToString();
                    //lbl_discount.Text = disc.ToString();
                    //lbl_paid_prev.Text = paid_prev.ToString();
                    //lbl_total.Text = total.ToString();
                }
                else
                {
                }
            }

            if (fdt.Rows.Count.ToString() != "0")
            {
                rp_fee_details.DataSource = fdt.DefaultView;
                rp_fee_details.DataBind();
                bind_ttl_fee();
                pnl_month_wise_fee_details.Visible = true;
                //btn_save_payment.Visible = true;
            }
            else
            {
                pnl_month_wise_fee_details.Visible = false;
                //btn_save_payment.Visible = false;
                lbl_fee_amount.Text = "0";
                lbl_discount.Text = "0";
                lbl_paid_prev.Text = "0";
                lbl_total.Text = "0";

                txttotal.Text = "0";
                txt_paid_prev.Text = "0";
                txt_discount.Text = "0";


                txt_month_total.Text = "0";
                txt_month_disc_amt.Text = "0";
                txt_month_prev_paid.Text = "0";
                txt_month_net_payble.Text = "0";
            }
        }

        private void bind_ttl_fee()
        {
            int i;
            double totalAmt = 0; double totaldisc = 0; double totalPrepAid = 0; double totalpblE = 0;
            int gridview_rowcount = rp_fee_details.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_amount = (Label)rp_fee_details.Items[i].FindControl("lbl_amount");
                Label lbl_disc_amt = (Label)rp_fee_details.Items[i].FindControl("lbl_disc_amt");
                Label lbl_pre_paid = (Label)rp_fee_details.Items[i].FindControl("lbl_pre_paid");
                Label lbl_tot_pble = (Label)rp_fee_details.Items[i].FindControl("lbl_tot_pble");
                if (lbl_amount.Text != "")
                {
                    totalAmt = totalAmt + Convert.ToDouble(lbl_amount.Text);
                }
                if (lbl_disc_amt.Text != "")
                {
                    totaldisc = totaldisc + Convert.ToDouble(lbl_disc_amt.Text);
                }
                if (lbl_pre_paid.Text != "")
                {
                    totalPrepAid = totalPrepAid + Convert.ToDouble(lbl_pre_paid.Text);
                }
                if (lbl_tot_pble.Text != "")
                {
                    totalpblE = totalpblE + Convert.ToDouble(lbl_tot_pble.Text);
                }
            }
            lbl_fee_amount.Text = totalAmt.ToString();
            lbl_discount.Text = totaldisc.ToString();
            lbl_paid_prev.Text = totalPrepAid.ToString();
            lbl_total.Text = totalpblE.ToString();
            txttotal.Text = totalAmt.ToString();
            txt_paid_prev.Text = totalPrepAid.ToString();
            txt_discount.Text = totaldisc.ToString();
            txtfineamount.Text = ViewState["fineAmt"].ToString();
            txt_monthlyFee.Text = (totalpblE + My.toDouble(ViewState["fineAmt"].ToString()) + My.toDouble(txt_other_fee.Text)).ToString();
            txt_is_added.Text = "0";


            txt_month_total.Text = totalAmt.ToString();
            txt_month_disc_amt.Text = totaldisc.ToString();
            txt_month_prev_paid.Text = totalPrepAid.ToString();
            txt_month_net_payble.Text = (totalpblE + My.toDouble(ViewState["fineAmt"].ToString()) + My.toDouble(txt_other_fee.Text)).ToString();


            if (0 >= My.toDouble(txt_monthlyFee.Text))
            {
                txt_paid_amount.Text = "0";
                txt_total_dues.Text = "0";
            }
        }


        protected void rp_fee_details_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string lbl_mnth = ((Label)e.Item.FindControl("lbl_mnth")).Text;
                HtmlTableRow row2 = (HtmlTableRow)e.Item.FindControl("row");

                if (ViewState["Is_checkbx_hide_pay_fee"].ToString() == "1")
                {
                    HtmlTableCell tdHide = (HtmlTableCell)e.Item.FindControl("thHideMnth");
                    tdHide.Style["display"] = "none";  // Hide using CSS
                }




                if (lbl_mnth == "April")
                {
                    row2.Attributes["style"] = "background-color:#CCE1F2";
                }
                if (lbl_mnth == "May")
                {
                    row2.Attributes["style"] = "background-color:#C6F8E5";
                }
                if (lbl_mnth == "June")
                {
                    row2.Attributes["style"] = "background-color:#FBF7D5";
                }
                if (lbl_mnth == "July")
                {
                    row2.Attributes["style"] = "background-color:#F9DED7";
                }
                //===
                if (lbl_mnth == "August")
                {
                    row2.Attributes["style"] = "background-color:#F5CDDE";
                }
                if (lbl_mnth == "September")
                {
                    row2.Attributes["style"] = "background-color:#E2BEF1";
                }
                if (lbl_mnth == "October")
                {
                    row2.Attributes["style"] = "background-color:#FBC5B0";
                }
                if (lbl_mnth == "November")
                {
                    row2.Attributes["style"] = "background-color:#BBD5D3";
                }
                //===
                if (lbl_mnth == "December")
                {
                    row2.Attributes["style"] = "background-color:#FCD0BA";
                }
                if (lbl_mnth == "January")
                {
                    row2.Attributes["style"] = "background-color:#E8CBD9";
                }
                if (lbl_mnth == "February")
                {
                    row2.Attributes["style"] = "background-color:#C9EAE8";
                }
                if (lbl_mnth == "March")
                {
                    row2.Attributes["style"] = "background-color:#99D7D2";
                }
            }
        }



        protected void txtfineamount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txttotalbill.Text = (Convert.ToDouble(lbl_total.Text) + Convert.ToDouble(txtfineamount.Text)).ToString();
            }
            catch (Exception ex)
            {
            }
        }



        protected void btn_save_payment_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_save_payment.Text == "Pay")
                {
                    string isCheckPending = My.isChequePending(ddlsessionad.SelectedValue, lbl_admission_no.Text, txt_bank_date.Text);
                    if (isCheckPending == "1")
                    {
                        check_is_cheque_pending(ddlsessionad.SelectedValue, lbl_admission_no.Text);
                        return;
                    }
                    List<string> month_lst = new List<string>();
                    bool payment = false;
                    if (txt_payment_date.Text == "")
                    {
                        Alertme("Please Select Payment Date", "warning");
                        return;
                    }

                    if (txt_admission_no.Text.ToUpper() == "CANCEL")
                    { }
                    else
                    {
                        if (My.toDouble(txttotalbill.Text) > 0)
                        {
                            if (My.toDouble(txt_paid_amount.Text) <= 0)
                            {
                                txt_paid_amount.Focus();
                                Alertme("Please enter paid amount.", "warning");
                                return;
                            }
                        }
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

                    string slip_no = "", entry_id = "";
                    bool flag = false; double admissionPaid = 0; string paymentType = "Monthly"; double monthlYPaid = 0; string iS_any_payment_done = "0";
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        #region payCode 
                        if (My.toDouble(txttotalbill.Text) >= My.toDouble(txt_paid_amount.Text))
                        {
                            #region ForBackDate
                            //if (chk_delete_slip.Checked == true)
                            //{ }
                            //else
                            //{
                            //    int pay_idate = My.DateConvertToIdate(txt_payment_date.Text);
                            //    bool chek_fee = My.find_mnth_fee_taken_date("Student_Payment_History", pay_idate, lbl_admission_no.Text, ViewState["classid"].ToString(), ddlsessionad.SelectedItem.Text);
                            //    if (chek_fee == false)
                            //    {
                            //        //Alertme("Payment is already done on your chosen date.", "warning");
                            //        //return;
                            //    }
                            //}
                            #endregion
                            monthlYPaid = My.toDouble(txt_paid_amount.Text);
                            string feeTypes = "";
                            if (chk_collect_adm_ann_fee.Checked == true)
                            {
                                #region AdmissionAnnuaL
                                iS_any_payment_done = "1";
                                if (My.toDouble(txt_paid_amount.Text) > My.toDouble(txt_adm_ann_fee.Text))
                                {
                                    monthlYPaid = My.toDouble(txt_paid_amount.Text) - My.toDouble(txt_adm_ann_fee.Text);
                                    admissionPaid = My.toDouble(txt_adm_ann_fee.Text);
                                    paymentType = "MonthlyAdmission";
                                }
                                else
                                {
                                    monthlYPaid = (My.toDouble(txt_paid_amount.Text) - My.toDouble(txt_adm_ann_fee.Text));
                                    admissionPaid = My.toDouble(txt_paid_amount.Text);
                                    paymentType = "Admission";
                                }


                                if (ViewState["Transfer_Status"].ToString() == "New")
                                {
                                    if (chk_delete_slip.Checked == true)
                                    {
                                        if (txt_old_slip_no.Text == "")
                                        {
                                            Alertme("Please enter bill no.", "warning");
                                            txt_old_slip_no.Focus();
                                            return;
                                        }
                                        else
                                        {
                                            slip_no = txt_old_slip_no.Text.Trim();
                                            string checkDupbill = payments.check_manual_dup_bill_no_with_session(slip_no, ViewState["session"].ToString(), con);
                                            if (checkDupbill == "1")
                                            {
                                                Alertme("The bill number you have entered already exists.", "warning");
                                                return;
                                            }
                                            //string chek_bill_no = payments.get_old_bill_no(slip_no, ViewState["Branchid"].ToString(), con);
                                            //if (chek_bill_no != "Yes")
                                            //{
                                            //    Alertme(chek_bill_no, "warning");
                                            //    return;
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        slip_no = payments.invoice_format_admssion("slip_no", con);
                                        if (ViewState["firm_id"].ToString() == "NPI-01")
                                        {
                                            if (slip_no.Length == 3)
                                            {
                                                slip_no = "REC-00000" + slip_no;
                                            }
                                            if (slip_no.Length == 4)
                                            {
                                                slip_no = "REC-0000" + slip_no;
                                            }
                                            if (slip_no.Length == 5)
                                            {
                                                slip_no = "REC-000" + slip_no;
                                            }
                                            if (slip_no.Length == 6)
                                            {
                                                slip_no = "REC-00" + slip_no;
                                            }
                                            if (slip_no.Length == 7)
                                            {
                                                slip_no = "REC-0" + slip_no;
                                            }
                                            if (slip_no.Length == 8)
                                            {
                                                slip_no = "REC-" + slip_no;
                                            }
                                        }
                                    }
                                    feeTypes = "Admission";
                                    make_admission_fee(con, admissionPaid, slip_no, txt_paid_amount.Text, feeTypes);
                                }
                                else
                                {
                                    if (chk_delete_slip.Checked == true)
                                    {
                                        if (txt_old_slip_no.Text == "")
                                        {
                                            Alertme("Please enter bill no.", "warning");
                                            txt_old_slip_no.Focus();
                                            return;
                                        }
                                        else
                                        {
                                            slip_no = txt_old_slip_no.Text.Trim();
                                            string checkDupbill = payments.check_manual_dup_bill_no_with_session(slip_no, ViewState["session"].ToString(), con);
                                            if (checkDupbill == "1")
                                            {
                                                Alertme("The bill number you have entered already exists.", "warning");
                                                return;
                                            }
                                            //string chek_bill_no = payments.get_old_bill_no(slip_no, ViewState["Branchid"].ToString(), con);
                                            //if (chek_bill_no != "Yes")
                                            //{
                                            //    Alertme(chek_bill_no, "warning");
                                            //    return;
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        slip_no = payments.invoice_readmission("slip_no", con);
                                        if (ViewState["firm_id"].ToString() == "NPI-01")
                                        {
                                            if (slip_no.Length == 3)
                                            {
                                                slip_no = "REC-00000" + slip_no;
                                            }
                                            if (slip_no.Length == 4)
                                            {
                                                slip_no = "REC-0000" + slip_no;
                                            }
                                            if (slip_no.Length == 5)
                                            {
                                                slip_no = "REC-000" + slip_no;
                                            }
                                            if (slip_no.Length == 6)
                                            {
                                                slip_no = "REC-00" + slip_no;
                                            }
                                            if (slip_no.Length == 7)
                                            {
                                                slip_no = "REC-0" + slip_no;
                                            }
                                            if (slip_no.Length == 8)
                                            {
                                                slip_no = "REC-" + slip_no;
                                            }
                                        }
                                    }

                                    feeTypes = "Annual";
                                    make_annual_fee(con, admissionPaid, slip_no, txt_paid_amount.Text);
                                }

                                //if (ddl_paymentmode.Text == "Cheque")
                                //{
                                //    payments.exeSql("insert into Fee_payment_by_cheque_status(Session_id,Admission_no,Monthly_slip_no,Yearly_slip_no,Payment_type,Cheque_no,Bank_name,Cheque_date,Status,Created_by,Created_date,Created_time,Created_idate,Is_group_payment,Cheque_amount) values ('" + ViewState["sessionIDs"].ToString() + "','" + txt_admission_no.Text + "','" + slip_no + "','" + ViewState["yearlYSLipNo"].ToString() + "','" + feeTypes + "','" + txt_trans_no.Text + "','" + ddl_bank.Text + "','" + txt_bank_date.Text + "','PENDING','" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "','" + mycode.idate() + "','1','" + admissionPaid.ToString() + "');", con);
                                //}




                                int growcountS = rd_months.Items.Count; string is_month_payment = "0";
                                for (int iS = 0; iS < growcountS; iS++)
                                {
                                    Label lbl_Month = (Label)rd_months.Items[iS].FindControl("lbl_Month");
                                    CheckBox chk_month = (CheckBox)rd_months.Items[iS].FindControl("chk_month");
                                    if (My.toBool(chk_month.Checked))
                                    {
                                        is_month_payment = "1";
                                        month_lst.Add(lbl_Month.Text);
                                    }
                                }


                                if (is_month_payment == "1")
                                {
                                    is_month_payment = "0";
                                    int feedtCount = rp_fee_details.Items.Count;
                                    for (int iSS = 0; iSS < feedtCount; iSS++)
                                    {
                                        CheckBox chk_get_fee = (CheckBox)rp_fee_details.Items[iSS].FindControl("chk_get_fee");
                                        if (My.toBool(chk_get_fee.Checked))
                                        {
                                            is_month_payment = "1";
                                        }
                                    }
                                }


                                if (monthlYPaid < 0)
                                {
                                    monthlYPaid = 0;
                                }

                                //paymentType = "MonthlyAdmission";
                                if (is_month_payment == "1")
                                {
                                    iS_any_payment_done = "1";
                                    if (chk_collect_adm_ann_fee.Checked == true)
                                    {
                                        paymentType = "MonthlyAdmission";
                                    }
                                    string total_paid = monthlYPaid.ToString();


                                    entry_id = payments.auto_serialS("entry_id", con);
                                    calculate_dues_for_new_month(slip_no, con);
                                    payments.exeSql("update Typewise_fee_collection set position=( select top 1 Position from dbo.[Month_Index] where Month=Typewise_fee_collection.month)", con);
                                    send_data_in_student_payment_history("Monthly", slip_no, entry_id, month_lst, total_paid, con, "1");
                                    //payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received Monthly fee :-" + total_paid + " rs,Slip No :- " + slipno + " from " + lbl_name.Text + ", Admission No :-" + lbl_admission_no.Text, con);


                                    //if (ddl_paymentmode.Text == "Cheque")
                                    //{
                                    //    payments.exeSql("insert into Fee_payment_by_cheque_status(Session_id,Admission_no,Monthly_slip_no,Yearly_slip_no,Payment_type,Cheque_no,Bank_name,Cheque_date,Status,Created_by,Created_date,Created_time,Created_idate,Is_group_payment,Cheque_amount) values ('" + ViewState["sessionIDs"].ToString() + "','" + txt_admission_no.Text + "','" + slip_no + "','0','MonthlyFee','" + txt_trans_no.Text + "','" + ddl_bank.Text + "','" + txt_bank_date.Text + "','PENDING','" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "','" + mycode.idate() + "','0','" + txt_paid_amount.Text + "');", con);
                                    //}

                                    //if (paymentType == "MonthlyAdmission")
                                    //{
                                    //    payments.exeSql("insert into Month_yearly_payment_history(Unique_no,Monthly_slip_no,Yearly_slip_no,Payment_type,Admission_no,Created_date,Created_idate,Created_time,Created_by) values ('" + entry_id + "','" + slip_no + "','" + ViewState["yearlYSLipNo"].ToString() + "','" + feeTypes + "','" + lbl_admission_no.Text + "','" + mycode.date() + "','" + mycode.idate() + "','" + mycode.time() + "','" + Session["Admin"].ToString() + "');", con);
                                    //}
                                }
                                //}
                                #endregion
                            }
                            else
                            {
                                if (monthlYPaid >= 0)
                                {
                                    int growcountS = rd_months.Items.Count; string is_month_payment = "0";
                                    for (int iS = 0; iS < growcountS; iS++)
                                    {
                                        Label lbl_Month = (Label)rd_months.Items[iS].FindControl("lbl_Month");
                                        CheckBox chk_month = (CheckBox)rd_months.Items[iS].FindControl("chk_month");
                                        if (My.toBool(chk_month.Checked))
                                        {
                                            is_month_payment = "1";
                                            month_lst.Add(lbl_Month.Text);
                                        }
                                    }
                                    if (is_month_payment == "1")
                                    {
                                        is_month_payment = "0";
                                        int feedtCount = rp_fee_details.Items.Count;
                                        for (int iSS = 0; iSS < feedtCount; iSS++)
                                        {
                                            CheckBox chk_get_fee = (CheckBox)rp_fee_details.Items[iSS].FindControl("chk_get_fee");
                                            if (My.toBool(chk_get_fee.Checked))
                                            {
                                                is_month_payment = "1";
                                            }
                                        }
                                    }

                                    //paymentType = "MonthlyAdmission";
                                    if (is_month_payment == "1")
                                    {
                                        iS_any_payment_done = "1";
                                        if (chk_collect_adm_ann_fee.Checked == true)
                                        {
                                            paymentType = "MonthlyAdmission";
                                        }
                                        string total_paid = monthlYPaid.ToString();
                                        if (chk_delete_slip.Checked == true)
                                        {
                                            if (txt_old_slip_no.Text == "")
                                            {
                                                Alertme("Please enter bill no.", "warning");
                                                txt_old_slip_no.Focus();
                                                return;
                                            }
                                            else
                                            {
                                                slip_no = txt_old_slip_no.Text.Trim();
                                                string checkDupbill = payments.check_manual_dup_bill_no_with_session(slip_no, ViewState["session"].ToString(), con);
                                                if (checkDupbill == "1")
                                                {
                                                    Alertme("The bill number you have entered already exists.", "warning");
                                                    return;
                                                }
                                                //string chek_bill_no = payments.get_old_bill_no(slip_no, ViewState["Branchid"].ToString(), con);
                                                //if (chek_bill_no != "Yes")
                                                //{
                                                //    Alertme(chek_bill_no, "warning");
                                                //    return;
                                                //}
                                            }
                                        }
                                        else
                                        {
                                            slip_no = payments.invoice_monthly("slip_no", con);
                                            if (ViewState["firm_id"].ToString() == "NPI-01")
                                            {
                                                if (slip_no.Length == 3)
                                                {
                                                    slip_no = "REC-00000" + slip_no;
                                                }
                                                if (slip_no.Length == 4)
                                                {
                                                    slip_no = "REC-0000" + slip_no;
                                                }
                                                if (slip_no.Length == 5)
                                                {
                                                    slip_no = "REC-000" + slip_no;
                                                }
                                                if (slip_no.Length == 6)
                                                {
                                                    slip_no = "REC-00" + slip_no;
                                                }
                                                if (slip_no.Length == 7)
                                                {
                                                    slip_no = "REC-0" + slip_no;
                                                }
                                                if (slip_no.Length == 8)
                                                {
                                                    slip_no = "REC-" + slip_no;
                                                }
                                            }
                                        }

                                        entry_id = payments.auto_serialS("entry_id", con);
                                        calculate_dues_for_new_month(slip_no, con);
                                        payments.exeSql("update Typewise_fee_collection set position=( select top 1 Position from dbo.[Month_Index] where Month=Typewise_fee_collection.month)", con);
                                        send_data_in_student_payment_history("Monthly", slip_no, entry_id, month_lst, total_paid, con, "0");
                                        payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received Monthly fee :-" + total_paid + " rs,Slip No :- " + slip_no + " from " + lbl_name.Text + ", Admission No :-" + lbl_admission_no.Text, con);


                                        //if (ddl_paymentmode.Text == "Cheque")
                                        //{
                                        //    payments.exeSql("insert into Fee_payment_by_cheque_status(Session_id,Admission_no,Monthly_slip_no,Yearly_slip_no,Payment_type,Cheque_no,Bank_name,Cheque_date,Status,Created_by,Created_date,Created_time,Created_idate,Is_group_payment,Cheque_amount) values ('" + ViewState["sessionIDs"].ToString() + "','" + txt_admission_no.Text + "','" + slip_no + "','0','MonthlyFee','" + txt_trans_no.Text + "','" + ddl_bank.Text + "','" + txt_bank_date.Text + "','PENDING','" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "','" + mycode.idate() + "','0','" + total_paid + "');", con);
                                        //}

                                        //if (paymentType == "MonthlyAdmission")
                                        //{
                                        //    payments.exeSql("insert into Month_yearly_payment_history(Unique_no,Monthly_slip_no,Yearly_slip_no,Payment_type,Admission_no,Created_date,Created_idate,Created_time,Created_by) values ('" + entry_id + "','" + slip_no + "','" + ViewState["yearlYSLipNo"].ToString() + "','" + feeTypes + "','" + lbl_admission_no.Text + "','" + mycode.date() + "','" + mycode.idate() + "','" + mycode.time() + "','" + Session["Admin"].ToString() + "');", con);
                                        //}
                                    }
                                }
                            }

                            if (ddl_paymentmode.Text == "Cheque")
                            {
                                payments.exeSql("insert into Fee_payment_by_cheque_status(Session_id,Admission_no,Monthly_slip_no,Yearly_slip_no,Payment_type,Cheque_no,Bank_name,Cheque_date,Status,Created_by,Created_date,Created_time,Created_idate,Is_group_payment,Cheque_amount) values ('" + ViewState["sessionIDs"].ToString() + "','" + txt_admission_no.Text + "','" + slip_no + "','0','MonthlyFee','" + txt_trans_no.Text + "','" + ddl_bank.Text + "','" + txt_bank_date.Text + "','PENDING','" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "','" + mycode.idate() + "','0','" + txt_paid_amount.Text + "');", con);
                            }
                            dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);

                            flag = true;
                            con.Close();
                            scope.Complete();
                        }
                        else
                        {
                            Alertme("Sorry you can't pay greater than amount actual paid amount.", "warning");
                        }
                        #endregion
                    }

                    //TRANSITION COMPLETE
                    if (flag == true)
                    {
                        // account voacher entry
                        try
                        {
                            string input = txt_payment_date.Text;  // DD/MM/YYYY
                            string FNsession = My.GetFinancialSessionFromString(input);
                            string unique_entry_id = My.unique_id();
                            string VoucherNo = slip_no;
                            string feeType = "Student Fee Payment";
                            double amountpaid = My.toDouble(txt_paid_amount.Text);
                            string VoucherType = "Receipt";
                            string Description = "Fee collection from " + lbl_name.Text + " Amount : " + amountpaid + "/-";
                            string PayDate = txt_payment_date.Text + " " + mycode.time();
                            int Idate = My.DateConvertToIdate(txt_payment_date.Text);
                            string alternetacc_id = lbl_admission_no.Text;
                            string session_name = FNsession;

                            bool checkbiilentery = My.check_dup_bill_no_entry(VoucherNo, session_name);
                            if (checkbiilentery == true)
                            {
                                if (ddl_paymentmode.Text.ToUpper() == "CASH")
                                {
                                    My.send_to_cash_payment_Voucher_Details(alternetacc_id, "cash", amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo, "");
                                }
                                else
                                {
                                    string toponebank = My.get_bank_id(ddl_bank.Text);
                                    My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo, "");
                                }
                            }
                        }
                        catch
                        {
                        }



                        if (iS_any_payment_done == "1")
                        {
                            try
                            {
                                if (ViewState["IsUpdateRollSec"].ToString() == "1")
                                {
                                    if (chk_is_roll_sec_apply.Checked == true)
                                    {
                                        if (ViewState["Auto_roll_according_to_section"].ToString() == "1")
                                        {
                                            My.exeSql("update admission_registor set rollnumber='" + txt_roll_assign.Text + "',Section='" + ddl_section_assign.Text + "' where admissionserialnumber='" + lbl_admission_no.Text + "' and Session_id='" + ViewState["sessionIDs"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");

                                            #region subject assined auto 
                                            try
                                            {
                                                payments.student_subject_mapping_no_transaction(ViewState["sessionIDs"].ToString(), ViewState["session"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, ddl_section_assign.Text, ViewState["branchid"].ToString());
                                            }
                                            catch
                                            {
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            My.exeSql("update admission_registor set rollnumber='" + txt_assign_roll_no.Text + "',Section='" + txt_assign_section.Text + "' where admissionserialnumber='" + lbl_admission_no.Text + "' and Session_id='" + ViewState["sessionIDs"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
                                            #region subject assined auto 
                                            try
                                            {
                                                payments.student_subject_mapping_no_transaction(ViewState["sessionIDs"].ToString(), ViewState["session"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, txt_assign_section.Text, ViewState["branchid"].ToString());
                                            }
                                            catch
                                            {
                                            }
                                            #endregion
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }


                            #region PushSending
                            string feeTypes = "";

                            #region ForMonthlY

                            if (chk_collect_adm_ann_fee.Checked == true)
                            {
                                #region sms and whatsaap
                                // sms & whatsapp
                                string message = "";
                                try
                                {
                                    Dictionary<string, object> dc2 = My.get_student_info(lbl_admissionno.Text, lbl_session.Text);
                                    string mobilesms = (String)dc2["father_mob"];
                                    string whatsappno = (String)dc2["Father_whatsApp_no"];
                                    //"Dear " + lbl_name.Text + " you have paid fee :- " + txt_paid_amount.Text + " on date :- " + txt_payment_date.Text + " slip no.:-" + slip_no;

                                    string type = "";
                                    //  My mycode = new My();
                                    Dictionary<string, object> autosms = mycode.get_auto_message_template("Pay Fee");
                                    ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                                    ViewState["VariableName"] = (String)autosms["VariableName"];
                                    ViewState["SMSType"] = (String)autosms["SMSType"];
                                    ViewState["Send_From"] = (String)autosms["Send_From"];
                                    ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                                    ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];
                                    ViewState["Wid"] = (String)autosms["Wid"];
                                    var vrls = ViewState["VariableName"].ToString().Split(',');
                                    var lst = new String[vrls.Length];
                                    DataTable dtD = My.dataTable("select *,(convert(float, payable)-(convert(float, paid_amt)+convert(float, disc_amt)+convert(float, prev_paid_amt))) as Dues_amt from (select isnull(sum(convert(float, payable)),0) as payable,isnull(sum(convert(float, disc_amt)),0) as disc_amt,isnull(sum(convert(float, paid)),0) as paid_amt,isnull(sum(convert(float, previously_paid)),0) as prev_paid_amt,(select top 1 Month from Monthly_Fee_Collection_Slip where slipno='" + slip_no + "' order by month_position desc) as Month from Monthly_Fee_Collection_Slip where slipno='" + slip_no + "') t");

                                    if (vrls.Length > 0)
                                    {
                                        lst[0] = lbl_name.Text;
                                    }
                                    if (vrls.Length > 1)
                                    {
                                        lst[1] = lblclass.Text;
                                    }
                                    if (vrls.Length > 2)
                                    {
                                        lst[2] = lbl_admission_no.Text;
                                    }
                                    if (vrls.Length > 3)
                                    {

                                        lst[3] = txtroll_no.Text;
                                    }
                                    if (vrls.Length > 4)
                                    {

                                        lst[4] = dtD.Rows[0]["Month"].ToString();
                                    }
                                    if (vrls.Length > 5)
                                    {

                                        lst[5] = txt_paid_amount.Text;
                                    }
                                    if (vrls.Length > 6)
                                    {

                                        lst[6] = dtD.Rows[0]["Dues_amt"].ToString();
                                    }
                                    if (vrls.Length > 7)
                                    {

                                        lst[7] = txt_payment_date.Text;
                                    }
                                    if (vrls.Length > 8)
                                    {

                                        lst[8] = slip_no;
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
        { "8", lst[7] },
        { "9", lst[8] }
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
                                                        Session_id = ddlsession.SelectedValue,
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
                            }
                            else
                            {
                                string message = "";
                                #region sms and whatsaap
                                // sms & whatsapp
                                try
                                {

                                    Dictionary<string, object> dc2 = My.get_student_info(lbl_admissionno.Text, lbl_session.Text);
                                    string mobilesms = (String)dc2["father_mob"];
                                    string whatsappno = (String)dc2["Father_whatsApp_no"];



                                    string type = "";
                                    //  My mycode = new My();
                                    Dictionary<string, object> autosms = mycode.get_auto_message_template("Pay Fee");
                                    ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                                    ViewState["VariableName"] = (String)autosms["VariableName"];
                                    ViewState["SMSType"] = (String)autosms["SMSType"];
                                    ViewState["Send_From"] = (String)autosms["Send_From"];
                                    ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                                    ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];
                                    ViewState["Wid"] = (String)autosms["Wid"];

                                    var vrls = ViewState["VariableName"].ToString().Split(',');
                                    var lst = new String[vrls.Length];
                                    DataTable dtD = My.dataTable("select *,(convert(float, payable)-(convert(float, paid_amt)+convert(float, disc_amt)+convert(float, prev_paid_amt))) as Dues_amt from (select isnull(sum(convert(float, payable)),0) as payable,isnull(sum(convert(float, disc_amt)),0) as disc_amt,isnull(sum(convert(float, paid)),0) as paid_amt,isnull(sum(convert(float, previously_paid)),0) as prev_paid_amt,(select top 1 Month from Monthly_Fee_Collection_Slip where slipno='" + slip_no + "' order by month_position desc) as Month from Monthly_Fee_Collection_Slip where slipno='" + slip_no + "') t");
                                    if (vrls.Length > 0)
                                    {
                                        lst[0] = lbl_name.Text;
                                    }
                                    if (vrls.Length > 1)
                                    {
                                        lst[1] = lblclass.Text;
                                    }
                                    if (vrls.Length > 2)
                                    {
                                        lst[2] = lbl_admission_no.Text;
                                    }
                                    if (vrls.Length > 3)
                                    {

                                        lst[3] = txtroll_no.Text;
                                    }
                                    if (vrls.Length > 4)
                                    {

                                        lst[4] = dtD.Rows[0]["Month"].ToString();
                                    }
                                    if (vrls.Length > 5)
                                    {

                                        lst[5] = txt_paid_amount.Text;
                                    }
                                    if (vrls.Length > 6)
                                    {

                                        lst[6] = dtD.Rows[0]["Dues_amt"].ToString();
                                    }
                                    if (vrls.Length > 7)
                                    {

                                        lst[7] = txt_payment_date.Text;
                                    }
                                    if (vrls.Length > 8)
                                    {

                                        lst[8] = slip_no;
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
        { "8", lst[7] },
        { "9", lst[8] }
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
                                                        Session_id = ddlsession.SelectedValue,
                                                        Admission_no = lbl_admissionno.Text,
                                                        Status = "Pending",
                                                        Date = mycode.date(),
                                                        Idate = mycode.idate(),
                                                        Time = mycode.time(),
                                                        Send_by = ViewState["Userid"].ToString(),
                                                        Mesage_Type = type,
                                                    });


                                                    //ServicePointManager.Expect100Continue = true;
                                                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                                    //HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                                                    //HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                                                    //StreamReader sr = new StreamReader(httpres.GetResponseStream());
                                                    //string results = sr.ReadToEnd();
                                                    //sr.Close();

                                                    //My.Insert("Message_Details", new
                                                    //{
                                                    //    Mobile_No = whatsappno,
                                                    //    Message = message,
                                                    //    Date = mycode.date(),
                                                    //    Idate = mycode.idate(),
                                                    //    Time = mycode.time(),
                                                    //    Result = results,
                                                    //    User_id = ViewState["Userid"].ToString(),
                                                    //    Mesage_Type = type,
                                                    //    Groupcode = "Wahataap",
                                                    //    Status = "SEND",
                                                    //    Url = _url,
                                                    //    Message_to_Type = "Student",
                                                    //    admin_user_id = lbl_admissionno.Text,


                                                    //});

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
                                    string messge = message;// "Dear " + lbl_name.Text + " you have paid monthly fee :- " + monthlYPaid.ToString() + " date :- " + txt_payment_date.Text + " slip no.:-" + slip_no;
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
                                catch (Exception ex) { }
                            }

                            #endregion

                            #endregion

                            string confirmValue = string.Empty;
                            confirmValue = Request.Form["confirm_value"];
                            if (confirmValue == "Yes")
                            {
                                string page = "slip/monthly-slip.aspx";
                                if (ViewState["Monthly_bill_type"].ToString() == "A5")
                                {
                                    page = "slip/monthly-slip-a5.aspx";
                                }
                                if (ViewState["Monthly_bill_type"].ToString() == "A5D")
                                {
                                    page = "slip/receipt-details-a5.aspx";
                                }
                                if (ViewState["Monthly_bill_type"].ToString() == "A5S")
                                {
                                    page = "slip/bill-with-scholarship.aspx";
                                }
                                if (ViewState["Monthly_bill_type"].ToString() == "A5I")
                                {
                                    page = "slip/bill-installment.aspx";
                                }
                                if (ViewState["Monthly_bill_type"].ToString() == "A5SNGL")
                                {
                                    page = "slip/bill-slip-a5-single.aspx";
                                }
                                if (ViewState["Monthly_bill_type"].ToString() == "SPS-001")
                                {
                                    page = "slip/payment-slip.aspx";
                                }
                                Response.Redirect(page + "?admissionno=" + lbl_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue + "&classid=" + ViewState["classid"].ToString() + "&Slip_no=" + slip_no, false);
                            }
                            else
                            {
                                empty_form();
                                //payment = true;
                                ViewState["Other_Fees"] = "0";
                                Session["msg"] = "Payment has been successfully taken.";
                                if (ViewState["page_reset"].ToString() == "1")
                                {
                                    Response.Redirect("fees-collection-1.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect("fees-collection-1.aspx?adm=" + lbl_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                                }
                            }
                        }
                        else
                        {
                            Alertme("Sorry your payment will not be processed. Please click on month you want to pay.", "warning");
                        }
                    }
                    else
                    {
                        Alertme("Sorry something went wrong. please try again.", "warning");
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }



        private void send_message(string admission_no, string session_id, string msgs)
        {
            SqlCommand cmd;
            string query = "INSERT INTO Auto_notice_template_send_details (Notice_type,Notice_type_id,Admission_no,Session_id,Message,Date,idate,Time) values (@Notice_type,@Notice_type_id,@Admission_no,@Session_id,@Message,@Date,@idate,@Time)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Notice_type", "Fee Payment Confirmation");
            cmd.Parameters.AddWithValue("@Notice_type_id", "4");
            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
            cmd.Parameters.AddWithValue("@Session_id", session_id);
            cmd.Parameters.AddWithValue("@Message", msgs);
            cmd.Parameters.AddWithValue("@Date", mycode.date());
            cmd.Parameters.AddWithValue("@idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Time", mycode.time());
            if (My.InsertUpdateData(cmd))
            {
                ViewState["SendStatuS"] = "1";
            }
        }

        private void calculate_dues_for_new_month(string slipno, SqlConnection con)
        {
            int growcountS = rd_months.Items.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                string cunrt_session = ViewState["session"].ToString();
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);

                Label lbl_Month = (Label)rd_months.Items[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)rd_months.Items[iS].FindControl("chk_month");

                if (My.toBool(chk_month.Checked))
                {
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        string current_year_month = session_last_year + My.tomonth_numberstring(lbl_Month.Text);
                        string lunch_taken_year_month = "";
                        if (My.toint(ViewState["LunchMnthId"].ToString()) == 1 || My.toint(ViewState["LunchMnthId"].ToString()) == 2 || My.toint(ViewState["LunchMnthId"].ToString()) == 3)
                        {
                            lunch_taken_year_month = session_last_year + ViewState["LunchMnthId"].ToString();
                        }
                        else
                        {
                            lunch_taken_year_month = session_frst_year + ViewState["LunchMnthId"].ToString();
                        }
                        if (My.toint(lunch_taken_year_month) <= My.toint(current_year_month))
                        {
                            ViewState["parameteridS"] = "44";
                        }
                        else
                        {
                            ViewState["parameteridS"] = "4";
                        }
                    }

                    if (payments.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' and transection!=''", con).Rows.Count > 0)
                    {
                        continue;
                    }
                    else
                    {
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = txt_admission_no.Text;
                        dc["session_id"] = ViewState["sessionIDs"].ToString();
                        dc["class"] = lblclass.Text;
                        dc["session"] = ViewState["session"].ToString();
                        dc["class_id"] = ViewState["classid"].ToString();
                        dc["hosteltaken"] = lblhostel.Text;
                        dc["months"] = lbl_Month.Text;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                        dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                        dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();

                        dc["category_id"] = ViewState["category_id"].ToString();
                        dc["sub_category_id"] = ViewState["sub_category_id"].ToString();
                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();

                        dc["parameter_id"] = ViewState["parameteridS"].ToString();

                        //new08/08/2022
                        string monthid = My.tomonth_numberstring(lbl_Month.Text);
                        int pay_month = My.toint(monthid);
                        s_year = payments.check_start_months(pay_month, s_year, con);
                        dc["monthid"] = s_year + monthid;
                        DataTable feedt = payments.dataTableSP("sp_fetch_monthly_fee", dc, con);
                        send_in_typewise_fee(feedt, lbl_Month.Text, slipno, con);
                    }
                }
            }
        }

        private void send_in_typewise_fee(DataTable feedt, string month_name, string slipno, SqlConnection con)
        {
            double fine = My.toDouble(txtfineamount.Text);
            if (fine > 0)
            {
                int mnth_idss = My.tomonth_number(month_name);
                string month_id = My.getMonthS_twoDigit(mnth_idss.ToString());
                DataTable dt = payments.dataTable("select Fine_amount from Temp_fine_monthwise where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + feedt.Rows[0]["admission_no"].ToString() + "' and Month_id='" + month_id + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'", con);
                if (dt.Rows.Count > 0)
                {
                    fine = My.toDouble(dt.Rows[0]["Fine_amount"].ToString());
                    payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid) values ('" + feedt.Rows[0]["admission_no"].ToString() + "','" + feedt.Rows[0]["class"].ToString() + "','" + feedt.Rows[0]["session"].ToString() + "','" + feedt.Rows[0]["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine','" + My.toDouble(fine).ToString("0.00") + "','0','" + My.toDouble(fine).ToString("0.00") + "','Dues','" + month_name + "','6121','','School','false','false','false','0.00','" + ViewState["Section"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["Branchid"].ToString() + "')", con);
                }
            }




            foreach (DataRow dr in feedt.Rows)
            {
                double otherfee = My.toDouble(txt_other_fee.Text);
                if (otherfee > 0)
                {
                    if (ViewState["Other_Fees"].ToString() == "0")
                    {
                        payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + dr["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Other Fees','" + My.toDouble(otherfee).ToString("0.00") + "','0','" + My.toDouble(otherfee).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','6122','','School','false','false','false','0.00','" + ViewState["Section"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["branch_id"].ToString() + "')", con);
                        ViewState["Other_Fees"] = "1";
                    }
                }

                string parm = "";
                if (dr["parameter"].ToString() == "AdditionalServices")
                {
                    parm = "HostelMonthlyFee";
                }
                else if (dr["parameter"].ToString() == "DamageCharges")
                {
                    parm = "HostelMonthlyFee";
                }
                else
                {
                    parm = dr["parameter"].ToString();
                }
                payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,parameter2) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + parm + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["content"].ToString() + "','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','" + dr["content_id"].ToString() + "','" + slipno + "','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + ViewState["Section"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["Branchid"].ToString() + "','" + dr["parameter"].ToString() + "')", con);
            }
        }

        void send_data_in_student_payment_history(string type, string slip_no, string entry_id, List<string> month_lst, string total_paid, SqlConnection con, string withAdm)
        {
            if (withAdm == "0")
            {
                SqlDataAdapter ad = new SqlDataAdapter("select top 1 * from Student_Payment_History where Addmission_no='" + txt_admission_no.Text + "'", con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Student_Payment_History");
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr["Addmission_no"] = txt_admission_no.Text;
                dr["Session"] = ddlsession.SelectedItem.Text;
                dr["Date"] = txt_payment_date.Text;
                dr["Idate"] = Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                dr["Description"] = type + " fee collection for " + lbl_name.Text + " Month " + String.Join(",", month_lst) + ", Paid Amount : " + total_paid + " /-";
                dr["Entry_id"] = entry_id;
                dr["Slip_no"] = slip_no;
                dr["Amount"] = My.toDouble(total_paid).ToString("0.00");
                dr["Type"] = type;
                dr["mode"] = ddl_paymentmode.Text;
                dr["Pay_mode_transaction_no"] = txt_trans_no.Text;
                dr["discount"] = My.toDouble(txt_discount.Text).ToString("0.00");
                dr["Discoun_in_School"] = 0;
                dr["Discoun_in_Hostel"] = 0;
                dr["Discoun_in_Transport"] = 0;
                dr["fine"] = txtfineamount.Text;
                dr["is_ofline_sync"] = true;
                dr["Is_online_sync"] = false;
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString();
                dr["Remarks"] = txt_description.Text;
                dr["Class_id"] = ViewState["classid"].ToString();
                dr["Transection_in"] = "Software";
                dr["Branch"] = ViewState["Branchid"].ToString();
                dr["parameter_New"] = ViewState["parameter"].ToString();
                if (ddl_bank.Text == "Cash")
                {
                    dr["Bank_name"] = "";
                    dr["Bank_date"] = "";
                }
                else
                {
                    dr["Bank_name"] = ddl_bank.Text;
                    dr["Bank_date"] = txt_bank_date.Text;
                }

                //=================
                if (ddl_payee_bank.Text == "Select")
                {
                    dr["Payee_bank"] = "";
                }
                else
                {
                    dr["Payee_bank"] = ddl_payee_bank.Text;
                }


                dr["Created_date"] = mycode.date();
                dr["Created_idate"] = mycode.idate();
                dr["Is_coupon_applied"] = "0";
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);

                //send data in school ledger
                update_School_ledger(slip_no, entry_id, total_paid, con);
            }
            //
            string app_payment_type = "Software";//My.session("App_fee_collection_type");
            DataTable sdt = payments.dataTable("select Section,class,rollnumber,Session_id,Class_id,Transfer_Status,hosteltaken,Hostel_id from dbo.[admission_registor] where admissionserialnumber='" + txt_admission_no.Text + "' and session='" + ViewState["session"].ToString() + "'", con);

            #region update type wise fee collection
            // fine calculation has been zero
            submit_transection_in_typewise(txt_admission_no.Text, ViewState["session"].ToString(), My.toDouble(txtfineamount.Text), txt_payment_date.Text, My.DateConvertToIdate(txt_payment_date.Text).ToString(), My.toDouble(total_paid), slip_no, entry_id, sdt.Rows[0]["class"].ToString(), sdt.Rows[0]["Section"].ToString(), sdt.Rows[0]["Class_id"].ToString(), sdt.Rows[0]["hosteltaken"].ToString(), sdt.Rows[0]["Hostel_id"].ToString(), "", app_payment_type, con);
            #endregion
        }

        private void submit_transection_in_typewise(string adno, string session, double fine, string date, string idate, double paid_amount, string slip_no, string entry_id, string classs, string sction, string class_id, string hostel_taken, string hostel_id, string app_transection_id, string app_payment_type, SqlConnection con)
        {
            #region update dues amount in typewise fee collection
            string parameter = "", month = "", late_fine_month = "", month_position = "";
            string qry = " select *  from Typewise_fee_collection  where admission_no='" + adno + "' and session='" + session + "' and status='Dues' and parameter like '%" + ViewState["parameter"].ToString() + "%' order by cast(Position as float)";
            SqlDataAdapter ad = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Typewise_fee_collection");
            DataTable tdt = ds.Tables[0];
            if (tdt.Rows.Count > 0)
            {
                late_fine_month = tdt.Rows[0]["month"].ToString();
                month_position = tdt.Rows[0]["position"].ToString();
                string prev_month = "";
                foreach (DataRow dr in tdt.Rows)
                {
                    parameter = dr["parameter"].ToString();
                    ///=======
                    int feedtCount = rp_fee_details.Items.Count; string isChecked = "0";
                    for (int i = 0; i < feedtCount; i++)
                    {
                        Label lbl_mnth = (Label)rp_fee_details.Items[i].FindControl("lbl_mnth");
                        Label lblcontent_id = (Label)rp_fee_details.Items[i].FindControl("lblcontent_id");
                        CheckBox chk_get_fee = (CheckBox)rp_fee_details.Items[i].FindControl("chk_get_fee");
                        if (My.toBool(chk_get_fee.Checked))
                        {
                            if (lbl_mnth.Text == dr["month"].ToString() && lblcontent_id.Text == dr["content_id"].ToString())
                            {
                                isChecked = "1";
                            }
                        }
                    }

                    if (isChecked == "0")
                    {
                        if (dr["content_id"].ToString() == "6121")
                        {
                            isChecked = "1";
                        }
                    }

                    //
                    ///=======

                    if (isChecked == "1")
                    {
                        if (paid_amount >= 0)
                        {
                            month = dr["month"].ToString();

                            string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";

                            double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);
                            dr["Date"] = date;
                            dr["idate"] = idate;
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
                                #region send in collection slip
                                send_data_in_fee_collection_slip(dr["payable"].ToString(), dues.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, con);
                                discountMaster.save_discount(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admissionno.Text, dr["parameter"].ToString(), dr["content_id"].ToString(), dr["month"].ToString(), dr["payable"].ToString(), dr["Disc"].ToString(), slip_no, dr["position"].ToString(), ViewState["Userid"].ToString(), dr["feetype"].ToString(), con);
                                #endregion
                            }
                            else
                            {
                                if (paid_amount >= 0 || (prev_month != "" && prev_month == month))
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

                                    #region send in collection slip
                                    send_data_in_fee_collection_slip(dr["payable"].ToString(), paid_amount.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, con);
                                    discountMaster.save_discount(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admissionno.Text, dr["parameter"].ToString(), dr["content_id"].ToString(), dr["month"].ToString(), dr["payable"].ToString(), dr["Disc"].ToString(), slip_no, dr["position"].ToString(), ViewState["Userid"].ToString(), dr["feetype"].ToString(), con);
                                    #endregion

                                    paid_amount = 0;


                                }
                                else
                                {
                                    break;
                                }
                            }
                            dr["transection"] = slip_no;
                            dr["is_readyfor_sync"] = true;
                            dr["is_sync"] = false;
                            dr["group_id"] = group_id;
                            dr["class_id"] = class_id;
                            prev_month = month;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";
                        string prevpaid = dr["paid"].ToString();
                        dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                        dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + 0).ToString("0.00");
                        dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                        dr["status"] = "Dues";

                        #region send in collection slip
                        int monthrpcont = rd_months.Items.Count;
                        for (int im = 0; im < monthrpcont; im++)
                        {
                            Label lbl_Monthss = (Label)rd_months.Items[im].FindControl("lbl_Month");
                            CheckBox chk_month = (CheckBox)rd_months.Items[im].FindControl("chk_month");
                            if (My.toBool(chk_month.Checked))
                            {
                                if (lbl_Monthss.Text == dr["month"].ToString())
                                {
                                    send_data_in_fee_collection_slip(dr["payable"].ToString(), "0", dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, con);

                                    discountMaster.save_discount(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admissionno.Text, dr["parameter"].ToString(), dr["content_id"].ToString(), dr["month"].ToString(), dr["payable"].ToString(), dr["Disc"].ToString(), slip_no, dr["position"].ToString(), ViewState["Userid"].ToString(), dr["feetype"].ToString(), con);
                                }
                            }
                        }
                        #endregion

                        dr["transection"] = slip_no;
                        dr["is_readyfor_sync"] = true;
                        dr["is_sync"] = false;
                        dr["group_id"] = group_id;
                        dr["class_id"] = class_id;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(tdt);
            }


            #endregion



            if (fine > 0)
            {
                payments.exeSql("insert into Fine_Fees_collection(Admission_no,Session_id,Date,idate,Description,Slip_no,Amount,Total_no_of_fine_day_month,Fine_date_from,Fine_date_to,Branch_id,User_id,Class_id,Fine_type) values ('" + adno + "','" + ViewState["sessionIDs"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine Fees','" + slip_no + "','" + My.toDouble(fine).ToString("0.00") + "','" + ViewState["late_fine_no_of_day_month"].ToString() + "','" + ViewState["fine_date_From"].ToString() + "','" + ViewState["fine_date_To"].ToString() + "','" + ViewState["firm_id"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["classid"].ToString() + "','" + ViewState["FineType"].ToString() + "')", con);
            }
        }

        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, SqlConnection con)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,Date,Idate,branchid,Hostel_id,Room_category) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + My.toDouble(payable).ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + My.toDouble(prevpaid).ToString("0.00") + "','" + txt_payment_date.Text + "','" + My.DateConvertToIdate(txt_payment_date.Text) + "','" + ViewState["Branchid"].ToString() + "','" + ViewState["Hostel_id"].ToString() + "','" + ViewState["Room_Category_id"].ToString() + "');";
            payments.exeSql(qry, con);
        }

        private void update_School_ledger(string slip_no, string entry_id, string paid, SqlConnection con)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter("select top 1 * from SchoolLedger ", con);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "Month Wise Fee Collection";
            dr[1] = "Monthly  Fee  for  " + lbl_name.Text + "  Adm.No:-" + txt_admission_no.Text + "Total Bill:- " + txttotalbill.Text + " , Paid Amount :-" + paid.ToString() + " ,  Discount Given:-" + txt_discount.Text + " Dues:-" + txt_total_dues.Text + " Slip No:-" + slip_no;
            dr[2] = My.toDouble(paid).ToString("0.00");
            dr[3] = "0";
            dr[4] = My.toDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
            dr[5] = txt_payment_date.Text;
            dr[6] = slip_no;
            dr["entry_id"] = entry_id;
            dr["session"] = ddlsession.SelectedItem.Text;
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Addmission_no"] = txt_admission_no.Text;
            dr["branchid"] = ViewState["Branchid"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad_contactus);
            ad_contactus.Update(dt);
        }

        #region empty form
        private void empty_form()
        {
            txt_paid_prev.Text = "";
            admission_dues = 0;
            lbl_name.Text = "";
            lbl_father_name.Text = "";
            lblclass.Text = "";
            lbl_phone.Text = "";
            //lbl_admission_no.Text = "";
            lblhostel.Text = "";
            lbltransporttion.Text = "";
            txttotal.Text = "";
            txt_discount.Text = "0";
            txttotalbill.Text = "";
            txt_paid_amount.Text = "0";
            txt_description.Text = "";
            txtfineamount.Text = "0";
        }
        #endregion

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
                empty_form();
                string query;
                if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                {
                    query = "select * from admission_registor where class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and section='" + txt_section.Text + "' and rollnumber='" + txtrollnumber.Text + "' and StudentStatus='AV'    and Is_TC_Taken!='true' and   Status='1' order by id asc";
                }
                else
                {
                    query = "select * from admission_registor where class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and section='" + txt_section.Text + "' and rollnumber='" + txtrollnumber.Text + "' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' and   Status='1' order by id asc";
                }

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
                string stdname = txt_student_name.Text.Trim();
                string query = "";
                if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                {
                    query = "select * from admission_registor where studentname like '%" + stdname + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and  Status='1' order by id asc";
                }
                else
                {
                    query = "select * from admission_registor where studentname like '%" + stdname + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' and  Status='1' order by id asc";
                }


                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else if (dt.Rows.Count == 1)
                {
                    if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                    {
                        query = "select * from admission_registor where admissionserialnumber='" + dt.Rows[0]["admissionserialnumber"].ToString() + "' and Session='" + ddl_session_student.SelectedItem.Text + "'  and  Status='1' order by id asc";
                    }
                    else
                    {
                        query = "select * from admission_registor where admissionserialnumber='" + dt.Rows[0]["admissionserialnumber"].ToString() + "' and Session='" + ddl_session_student.SelectedItem.Text + "' and  Status='1' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' order by id asc";
                    }
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
                if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                {
                    query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' order by id asc";
                }
                else
                {
                    query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' order by id asc";
                }
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void rd_months_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["firstrow"] == null)
                {
                    CheckBox chk_month = (CheckBox)e.Item.FindControl("chk_month");
                    chk_month.Enabled = true;
                    ViewState["firstrow"] = "1";
                }
            }
        }


        //=========================================
        double ttlFine = 0;
        private void fine_calculation()
        {
            DataTable dty = mycode.FillData("select Fine_mode from globle_data where Fine_mode='2'");
            if (dty.Rows.Count != 0)
            {
                string qry = "delete from Temp_fine_monthwise where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["Admission_no"].ToString() + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                My.exeSql(qry);
                ViewState["fineAmt"] = "0.00";
                bind_ttl_fee();
                if (chk_latefineapplay.Checked != true)
                {
                    return;
                }

                #region DayRanGEWise
                string pay_date = txt_payment_date.Text;
                int payidate = My.DateConvertToIdate(pay_date);


                //Advance Payment Check
                string crnt_year = mycode.year();
                string cunrt_session = ViewState["session"].ToString();
                string session_frst_year = cunrt_session.Substring(0, 4);
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);



                string CheckedMonth = "0"; string CheckedMonthN = "0";
                int mgrowcount = rd_months.Items.Count;
                for (int ixi = 0; ixi < mgrowcount; ixi++)
                {
                    if (CheckedMonth == "0")
                    {
                        CheckBox chkM = (CheckBox)rd_months.Items[ixi].FindControl("chk_month");
                        if (chkM.Checked == true)
                        {
                            Label lbl_Month = (Label)rd_months.Items[ixi].FindControl("lbl_Month");
                            CheckedMonthN = lbl_Month.Text;
                            CheckedMonth = "1";
                        }
                    }
                }

                int mnth_idss = My.tomonth_number(CheckedMonthN);
                if (mnth_idss <= 0) { }
                else
                {
                    string pay_month_two_digit = My.getMonthS_twoDigit(mnth_idss.ToString());
                    int pay_month = My.toint(pay_month_two_digit);
                    s_year = My.check_start_months(pay_month, s_year);

                    int pay_month_with_year = My.toint(s_year + pay_month_two_digit);
                    int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                    //Advance Payment Check
                    if (crnt_month_with_year >= pay_month_with_year)
                    {
                        DataTable dtz = mycode.FillData("select top 1 * from Fine_master_day_range");
                        if (dtz.Rows.Count != 0)
                        {
                            ViewState["FineType"] = "DayWise";
                            string last_day_of_payments = "01" + "/" + pay_month_two_digit + "/" + s_year;


                            DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            System.TimeSpan diff = enddate1.Subtract(startdate1);
                            int totaldays = Convert.ToInt32(diff.Days);

                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totaldays;
                            ViewState["fine_date_From"] = last_day_of_payments;
                            ViewState["fine_date_To"] = pay_date;



                            DataTable dt_fine = mycode.FillData("select top 1 * from Fine_master_day_range where No_of_day>" + totaldays + " order by No_of_day asc");
                            if (dt_fine.Rows.Count != 0)
                            {
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), pay_month_two_digit, My.toDouble(dt_fine.Rows[0]["Fine_amount"].ToString()));
                                bind_ttl_fee();
                            }
                            else
                            {
                                DataTable dt_fines = mycode.FillData("select top 1 * from Fine_master_day_range order by No_of_day desc");
                                if (dt_fines.Rows.Count != 0)
                                {
                                    save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), pay_month_two_digit, My.toDouble(dt_fines.Rows[0]["Fine_amount"].ToString()));
                                    bind_ttl_fee();
                                }
                                else
                                {
                                    ViewState["fineAmt"] = "0";
                                    bind_ttl_fee();
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region FINES
                DataTable dt = mycode.FillData("select top 1 * from Fine_master where Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "'");
                if (dt.Rows.Count != 0)
                {
                    string pay_date = txt_payment_date.Text;
                    int payidate = My.DateConvertToIdate(pay_date);
                    string fineType = dt.Rows[0]["Fine_type"].ToString();

                    if (fineType == "MonthWise") //===== MonthWise
                    {
                        #region MonthWise
                        string qry = "delete from Temp_fine_monthwise where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["Admission_no"].ToString() + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                        My.exeSql(qry);
                        ViewState["fineAmt"] = "0.00";
                        bind_ttl_fee();
                        if (chk_latefineapplay.Checked != true)
                        {
                            return;
                        }


                        int mgrowcount = rd_months.Items.Count;
                        for (int ixi = 0; ixi < mgrowcount; ixi++)
                        {
                            CheckBox chkM = (CheckBox)rd_months.Items[ixi].FindControl("chk_month");
                            if (chkM.Checked == true)
                            {
                                string cunrt_session = ViewState["session"].ToString();
                                string session_frst_year = cunrt_session.Substring(0, 4);
                                int session_s_year = My.toint(session_frst_year);
                                int s_year = My.toint(session_frst_year);
                                int s_yearFx = s_year;
                                Label lbl_Month = (Label)rd_months.Items[ixi].FindControl("lbl_Month");
                                int mnth_idss = My.tomonth_number(lbl_Month.Text);
                                string pay_month_two_digit = My.getMonthS_twoDigit(mnth_idss.ToString());

                                int pay_month = My.toint(pay_month_two_digit);
                                s_year = My.check_start_months(pay_month, s_year);


                                ViewState["FineType"] = "MonthWise";
                                string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());


                                int last_day_of_payments_year = My.check_start_months(My.toInt(pay_month_two_digit), s_yearFx);
                                string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + pay_month_two_digit + "/" + last_day_of_payments_year;

                                DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                int monthsDifference = GetMonthsDifference(startdate1, enddate1);
                                int monthNumber = startdate1.Month;
                                string till_date_paymnt = dt.Rows[0]["Last_day_to_deposit_fees"].ToString();
                                double ttl_fine = 0; int isfinecalculated = 0;
                                for (int m = 0; m < monthsDifference; m++)
                                {
                                    string monthNumbertwodgt = My.getMonthS_twoDigit(monthNumber.ToString());
                                    string monthName = My.getMonthS_full_name(monthNumbertwodgt);
                                    int updated_year = My.check_start_months(My.toInt(monthNumbertwodgt), s_yearFx);
                                    string last_idate_of_payments = updated_year.ToString() + monthNumbertwodgt + till_date_paymnt;


                                    int pay_month_with_years = My.toint(updated_year + monthNumbertwodgt);
                                    int fine_aplicable_years = My.check_start_months(My.toInt(applicable_month), s_yearFx);
                                    string fine_applicable_months = fine_aplicable_years + applicable_month;
                                    if (My.toint(fine_applicable_months) <= pay_month_with_years)
                                    {
                                        if (ViewState["RepeatFine"].ToString() == "Yes")
                                        {
                                            if (My.dataTable("select * from Typewise_fee_collection where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + monthName + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                                            {
                                            }
                                            else
                                            {
                                                if (My.toIntS(last_idate_of_payments) < payidate)
                                                {
                                                    ttl_fine = ttl_fine + My.toDouble(dt.Rows[0]["Fine_amt_per_day_or_month"].ToString());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (isfinecalculated == 0)
                                            {
                                                isfinecalculated = 1;
                                                if (My.dataTable("select * from Typewise_fee_collection where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + monthName + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                                                {
                                                }
                                                else
                                                {
                                                    if (My.toIntS(last_idate_of_payments) < payidate)
                                                    {
                                                        ttl_fine = My.toDouble(dt.Rows[0]["Fine_amt_per_day_or_month"].ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }


                                    monthNumber++;
                                    if (monthNumber == 13)
                                    {
                                        monthNumber = 1;
                                    }
                                }




                                ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                                ViewState["late_fine_no_of_day_month"] = "1";
                                ViewState["fine_date_From"] = last_day_of_payments;
                                ViewState["fine_date_To"] = pay_date;
                                if (ttl_fine > 0)
                                {
                                    save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), pay_month_two_digit, ttl_fine);
                                    bind_ttl_fee();
                                }
                                else
                                {
                                    save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), pay_month_two_digit, 0);
                                    bind_ttl_fee();
                                }


                                //System.TimeSpan diff = enddate1.Subtract(startdate1);
                                //int totaldays = Convert.ToInt32(diff.Days);
                                //int totalmonths = 0;
                                //int pay_month_with_year = My.toint(s_year + pay_month_two_digit);
                                //int fine_aplicable_year = My.check_start_months(My.toInt(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString()), s_yearFx);
                                //string fine_applicable_month = fine_aplicable_year + applicable_month;
                                //if (My.toint(fine_applicable_month) <= pay_month_with_year)
                                //{
                                //    if (ViewState["RepeatFine"].ToString() == "Yes")
                                //    {
                                //        if (My.dataTable("select * from Typewise_fee_collection where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                                //        {
                                //        }
                                //        else
                                //        {
                                //            if (totaldays > 0)
                                //            {
                                //                if (30 >= totaldays)
                                //                {
                                //                    totalmonths = 1;
                                //                }
                                //                else
                                //                {
                                //                    totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                                //                    double monthdays = 31;
                                //                    double reminder = My.toDouble(totaldays) % monthdays;
                                //                    if (29 > Math.Round(reminder))
                                //                    {
                                //                        totalmonths++;
                                //                    }
                                //                }
                                //            }
                                //        }

                                //        string fineStartFromMonth = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                                //        string fineStartFrom = s_yearFx + fineStartFromMonth + dt.Rows[0]["Last_day_to_deposit_fees"].ToString();
                                //        if (My.toIntS(fineStartFrom) > My.DateConvertToIdate(pay_date))
                                //        {
                                //            totalmonths = 0;
                                //        }

                                //        ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                                //        ViewState["late_fine_no_of_day_month"] = totalmonths;
                                //        ViewState["fine_date_From"] = last_day_of_payments;
                                //        ViewState["fine_date_To"] = pay_date;
                                //        if (My.toDouble(totalmonths) > 0)
                                //        {
                                //            string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                //            double ttl_fine_amt = My.toDouble(fine_amt) * totalmonths;
                                //            save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), pay_month_two_digit, ttl_fine_amt);
                                //            bind_ttl_fee();
                                //        }
                                //        else
                                //        {
                                //            save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), pay_month_two_digit, 0);
                                //            bind_ttl_fee();
                                //        }
                                //    }
                                //    else
                                //    {
                                //        if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                                //        {
                                //        }
                                //        else
                                //        {
                                //            if (totaldays > 0)
                                //            {
                                //                if (30 >= totaldays)
                                //                {
                                //                    totalmonths = 1;
                                //                }
                                //                else
                                //                {
                                //                    totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                                //                }
                                //            }
                                //        }
                                //        ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                                //        ViewState["late_fine_no_of_day_month"] = totalmonths;
                                //        ViewState["fine_date_From"] = last_day_of_payment;
                                //        ViewState["fine_date_To"] = pay_date;
                                //        if (My.toDouble(totalmonths) > 0)
                                //        {
                                //            string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                //            double ttl_fine_amt = My.toDouble(fine_amt) * 1;
                                //            save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), pay_month_two_digit, ttl_fine_amt);
                                //            bind_ttl_fee();
                                //        }
                                //        else
                                //        {
                                //            save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), pay_month_two_digit, 0);
                                //            bind_ttl_fee();
                                //        }
                                //    }
                                //}
                            }
                        }
                        #endregion
                    }
                    if (fineType == "DayWise")
                    {
                        #region DayWise

                        string qry = "delete from Temp_fine_monthwise where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["Admission_no"].ToString() + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                        My.exeSql(qry);
                        ViewState["fineAmt"] = "0.00";
                        bind_ttl_fee();
                        if (chk_latefineapplay.Checked != true)
                        {
                            return;
                        }

                        string isCalculated = "0";
                        int mgrowcount = rd_months.Items.Count;
                        for (int ixi = 0; ixi < mgrowcount; ixi++)
                        {
                            CheckBox chkM = (CheckBox)rd_months.Items[ixi].FindControl("chk_month");
                            if (chkM.Checked == true)
                            {
                                if (isCalculated == "0")
                                {
                                    string cunrt_session = ViewState["session"].ToString();
                                    string session_frst_year = cunrt_session.Substring(0, 4);
                                    int session_s_year = My.toint(session_frst_year);
                                    int s_year = My.toint(session_frst_year);
                                    Label lbl_Month = (Label)rd_months.Items[ixi].FindControl("lbl_Month");
                                    int mnth_idss = My.tomonth_number(lbl_Month.Text);
                                    string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());

                                    int pay_month = My.toint(month_id_in_two_dgts);
                                    s_year = My.check_start_months(pay_month, s_year);

                                    DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    string paymentMonthno = enddate1q.ToString("MM");

                                    int pay_month_with_year = My.toint(s_year + month_id_in_two_dgts);
                                    int crnt_month_with_year = My.toint(mycode.year() + paymentMonthno);
                                    //Advance Payment Check 
                                    int fine_aplicable_year = My.check_start_months(My.toInt(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString()), s_year);

                                    if (crnt_month_with_year >= pay_month_with_year)
                                    {
                                        ViewState["FineType"] = "DayWise";
                                        string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                                        string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;



                                        string fine_applicable_month = fine_aplicable_year + applicable_month;
                                        if (My.toint(fine_applicable_month) <= pay_month_with_year)
                                        {
                                            string last_day_of_payments = "";
                                            if (My.toint(fine_applicable_month) >= pay_month_with_year)
                                            {
                                                last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;
                                            }
                                            else
                                            {
                                                last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + month_id_in_two_dgts + "/" + s_year;
                                            }
                                            DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                            System.TimeSpan diff = enddate1.Subtract(startdate1);
                                            int totaldays = Convert.ToInt32(diff.Days);

                                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                                            ViewState["late_fine_no_of_day_month"] = totaldays;
                                            ViewState["fine_date_From"] = last_day_of_payments;
                                            ViewState["fine_date_To"] = pay_date;
                                            if (My.toDouble(totaldays) > 0)
                                            {
                                                isCalculated = "1";
                                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                                double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, ttl_fine_amt);
                                                bind_ttl_fee();
                                            }
                                            else
                                            {
                                                isCalculated = "1";
                                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, 0);
                                                bind_ttl_fee();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    if (fineType == "QuarterWise")
                    {
                        if (chk_latefineapplay.Checked != true)
                        {
                            string qry = "delete from Temp_fine_monthwise where Session_id = '" + ViewState["sessionIDs"].ToString() + "' and Admission_no = '" + ViewState["Admission_no"].ToString() + "' and Branch_id = '" + ViewState["Branchid"].ToString() + "'";
                            My.exeSql(qry);
                            ViewState["fineAmt"] = "0.00";
                            bind_ttl_fee();
                            return;
                        }


                        #region QuarterWise
                        string uncheckd = "1";
                        int mgrowcount = rd_months.Items.Count;
                        for (int ixi = 0; ixi < mgrowcount; ixi++)
                        {
                            CheckBox chkM = (CheckBox)rd_months.Items[ixi].FindControl("chk_month");
                            if (chkM.Checked == true)
                            {
                                uncheckd = "0";
                                string cunrt_session = ViewState["session"].ToString();
                                string session_frst_year = cunrt_session.Substring(0, 4);
                                int session_s_year = My.toint(session_frst_year);
                                int s_year = My.toint(session_frst_year);

                                Label lbl_Month = (Label)rd_months.Items[ixi].FindControl("lbl_Month");
                                int mnth_idss = My.tomonth_number(lbl_Month.Text);
                                string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());

                                int pay_month = My.toint(month_id_in_two_dgts);
                                s_year = My.check_start_months(pay_month, s_year);

                                #region QuarterWise
                                ViewState["FineType"] = "QuarterWise";
                                double fnl_fine_amt = 0;
                                SqlDataAdapter ad = new SqlDataAdapter("select * from Fine_master where Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "' and Q_start_month='" + month_id_in_two_dgts + "' and Q_start_year='" + s_year + "'  order by Q_start_month asc", My.conn);
                                DataSet ds = new DataSet();
                                ad.Fill(ds, "Fine_master");
                                DataTable dtm = ds.Tables[0];
                                int rowcount = ds.Tables[0].Rows.Count;
                                if (rowcount == 0)
                                {
                                }
                                else
                                {
                                    foreach (DataRow dr in dtm.Rows)
                                    {
                                        if (My.dataTable("select * from Typewise_fee_collection where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                                        {
                                        }
                                        else
                                        {
                                            string endmnth_in_two = My.getMonthS_twoDigit(dr["Q_start_month"].ToString());
                                            string last_day_of_payment_q = dr["Last_day_to_deposit_fees"].ToString() + "/" + endmnth_in_two + "/" + dr["Q_start_year"].ToString();


                                            DateTime startdate1q = DateTime.ParseExact(last_day_of_payment_q, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                            if (dr["Q_payment_mode"].ToString() == "Day")
                                            {
                                                System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                                int totaldays = Convert.ToInt32(diff.Days);
                                                ViewState["late_fine_no_of_day_month"] = totaldays;

                                                if (ViewState["flags1"].ToString() == "0")
                                                {
                                                    ViewState["fine_date_From"] = last_day_of_payment_q;
                                                    ViewState["flags1"] = "1";
                                                }

                                                ViewState["fine_date_To"] = pay_date;

                                                if (My.toDouble(totaldays) > 0)
                                                {
                                                    string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                                    double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                                    fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                                }
                                                else
                                                {
                                                    fnl_fine_amt = fnl_fine_amt += 0;
                                                }
                                            }
                                            else
                                            {
                                                System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                                int totaldays = Convert.ToInt32(diff.Days);
                                                ViewState["late_fine_no_of_day_month"] = dtm.Rows.Count.ToString();

                                                if (ViewState["flags1"].ToString() == "0")
                                                {
                                                    ViewState["fine_date_From"] = last_day_of_payment_q;
                                                    ViewState["flags1"] = "1";
                                                }

                                                ViewState["fine_date_To"] = pay_date;
                                                if (My.toDouble(totaldays) > 0)
                                                {
                                                    string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                                    double ttl_fine_amt = My.toDouble(fine_amt);
                                                    fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                                }
                                                else
                                                {
                                                    fnl_fine_amt = fnl_fine_amt += 0;
                                                }
                                            }
                                        }
                                    }

                                    save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, fnl_fine_amt);
                                    bind_ttl_fee();
                                }
                                #endregion
                            }
                        }
                        if (uncheckd == "1")
                        {
                            string qry = "delete from Temp_fine_monthwise where Session_id = '" + ViewState["sessionIDs"].ToString() + "' and Admission_no = '" + ViewState["Admission_no"].ToString() + "' and Branch_id = '" + ViewState["Branchid"].ToString() + "'";
                            My.exeSql(qry);
                            ViewState["fineAmt"] = "0.00";
                            bind_ttl_fee();
                        }
                        #endregion
                    }
                }
                #endregion
            }
        }

        static int GetMonthsDifference(DateTime startDate, DateTime endDate)
        {
            // Calculate the total months difference
            int monthsApart = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;

            // If the day of the end date is less than the day of the start date, subtract one month
            if (monthsApart <= 0)
            {
                monthsApart = 1;
            }
            else
            {
                monthsApart++;
            }
            return monthsApart;
        }

        private void save_fine_amount(string session_id, string admission_no, string month_id_in_two_dgts, double ttl_fine_amt)
        {

            //if (ViewState["Boarding_Point_id"].ToString() == "0")
            //{

            //}
            //else
            //{
            //    double tranportfine_multiply = My.Get_tranport_fine_amount();
            //    ttl_fine_amt = ttl_fine_amt * tranportfine_multiply;
            //}





            if (mycode.IsUserExist("select Id from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Temp_fine_monthwise (Session_id,Admission_no,Month_id,Fine_amount,Branch_id) values (@Session_id,@Admission_no,@Month_id,@Fine_amount,@Branch_id);";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd.Parameters.AddWithValue("@Month_id", month_id_in_two_dgts);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Temp_fine_monthwise set Fine_amount=@Fine_amount  where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                if (My.InsertUpdateData(cmd))
                {
                }
            }


            double total_fine = 0;
            int growcountS = rd_months.Items.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Month = (Label)rd_months.Items[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)rd_months.Items[iS].FindControl("chk_month");
                int mnth_idss = My.tomonth_number(lbl_Month.Text);
                string month_id = My.getMonthS_twoDigit(mnth_idss.ToString());

                if (chk_month.Checked == true)
                {
                    DataTable dt = PayrollMy.dataTable("select Fine_amount from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        total_fine = total_fine + My.toDouble(dt.Rows[0]["Fine_amount"].ToString());
                    }
                    else
                    {
                        total_fine = total_fine + 0;
                    }
                }
            }
            ViewState["fineAmt"] = total_fine.ToString("0.00");
        }


        protected void txt_payment_date_TextChanged(object sender, EventArgs e)
        {
            fine_calculation();
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("fees-collection-1.aspx", false);
        }

        protected void chk_delete_slip_CheckedChanged(object sender, EventArgs e)
        {
            txt_old_slip_no.Text = "";
            if (chk_delete_slip.Checked == true)
            {
                oldslip_no.Visible = true;
            }
            else
            {
                oldslip_no.Visible = false;
            }
        }

        protected void chk_latefineapplay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                fine_calculation();
            }
            catch
            {
            }
        }

        protected void txt_other_fee_TextChanged(object sender, EventArgs e)
        {
            if (txt_other_fee.Text == "")
            {
                Alertme("Please enter other fee", "warning");
            }
            else
            {
                bind_ttl_fee();
            }
        }


        #region DiscounT
        double ttl_admission_disc = 0;
        protected void rp_admission_discount_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_total_disc = ((Label)e.Item.FindControl("lbl_total_disc")) as Label;
                ttl_admission_disc = ttl_admission_disc + My.toDouble(lbl_total_disc.Text);
            }
            ViewState["ttl_admDisc"] = ttl_admission_disc.ToString("0.00");
        }


        double ttl_monthly_disc = 0;
        protected void rp_monthly_discount_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_total_disc = ((Label)e.Item.FindControl("lbl_total_disc")) as Label;
                ttl_monthly_disc = ttl_monthly_disc + My.toDouble(lbl_total_disc.Text);
            }
            ViewState["ttl_mnthDisc"] = ttl_monthly_disc.ToString("0.00");
        }
        #endregion

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
                        revised_payment();
                        if (ViewState["isSucceSSmsG"].ToString() == "1")
                        {
                            string description = "Monthly fee collection for " + lbl_name.Text + " Month " + ViewState["monthRevised"].ToString() + ", Paid Amount : " + txt_rd_paid_amt.Text + " /-";
                            My.exeSql("update Student_Payment_History set Amount='" + txt_rd_paid_amt.Text + "',Description='" + description + "' where Session='" + ViewState["session"].ToString() + "' and Slip_no='" + ViewState["RvSlipId"].ToString() + "'; insert into Revised_payment_history(Slip_id,Payble_amt,Old_payment,New_payment,Remark,Created_by,Created_date,Created_time) values ('" + ViewState["RvSlipId"].ToString() + "','" + txt_rd_ttl_amt.Text + "','" + lbl_rv_ttl_paid.Text + "','" + txt_rd_paid_amt.Text + "','" + txt_rv_remark.Text + "','" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "');");
                            find_prev_payments();
                            Alertme("Record has been updated successfully.", "success");
                            revisedPayment.Visible = false;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRevised();", true);
                    Alertme("Please enter payment amount.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void revised_payment()
        {
            #region update dues amount in typewise fee collection
            My.exeSql("update Typewise_fee_collection set paid='0', dues='0' where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and transection='" + ViewState["RvSlipId"].ToString() + "' and parameter like '%" + ViewState["parameter"].ToString() + "%'");
            double paid_amount = My.toDouble(txt_rd_paid_amt.Text);
            string parameter = "", month = "", month_position = "";
            string qry = " select *  from Typewise_fee_collection  where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and transection='" + ViewState["RvSlipId"].ToString() + "' and parameter like '%" + ViewState["parameter"].ToString() + "%' order by cast(Position as float)";

            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
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
                    My.exeSql("delete from Monthly_Fee_Collection_Slip where adno='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + dr["parameter"].ToString() + "' and Month='" + dr["month"].ToString() + "' and content_id='" + dr["content_id"].ToString() + "'");
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
                            send_data_in_fee_collection__monthly_slip(dr["payable"].ToString(), dues.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], ViewState["RvSlipId"].ToString(), dr["Ledger"].ToString(), dr["admission_no"].ToString(), dr["session"].ToString(), dr["class_id"].ToString(), dr["section"].ToString(), dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, dr["Date"].ToString(), dr["idate"].ToString());
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
                                send_data_in_fee_collection__monthly_slip(dr["payable"].ToString(), paid_amount.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], ViewState["RvSlipId"].ToString(), dr["Ledger"].ToString(), dr["admission_no"].ToString(), dr["session"].ToString(), dr["class_id"].ToString(), dr["section"].ToString(), dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, dr["Date"].ToString(), dr["idate"].ToString());
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
            #endregion
        }


        private void send_data_in_fee_collection__monthly_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, string date, string idate)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,Date,Idate,branchid) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + My.toDouble(payable).ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + My.toDouble(prevpaid).ToString("0.00") + "','" + date + "','" + idate + "','" + ViewState["Branchid"].ToString() + "');";
            My.exeSql(qry);
        }





        //==========================
        //============================================
        //===========================================
        #region ADMISSION FEE   //=================================********************** 
        private void Bind_fee_details()
        {
            string discountOn = "";
            string parameter = "";
            string parameter_id = "";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                discountOn = "Admission";
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                parameter_id = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "1" : "5";
            }
            else
            {
                discountOn = "Annual";
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                parameter_id = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "2" : "6";
            }
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + discountOn + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + discountOn + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + lbl_admissionno.Text + "' and parameter='" + parameter + "' and session='" + lbl_session.Text + "')t";
            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + lbl_session.Text + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + lbl_admissionno.Text + "'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='" + discountOn + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='" + discountOn + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "' ) t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + lbl_session.Text + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where   Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + lbl_admissionno.Text + "'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='" + discountOn + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='" + discountOn + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t";
                }
                fee_dt = My.dataTable(qry);
            }

            DataTable dt = PayrollMy.dataTable(qry);
            if (dt.Rows.Count == 0)
            {
                grd_fee_adm_fee.DataSource = null;
                grd_fee_adm_fee.DataBind();
            }
            else
            {
                grd_fee_adm_fee.DataSource = dt;
                grd_fee_adm_fee.DataBind();


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



                try
                {
                    if (ViewState["Is_checkbx_hide_pay_fee"].ToString() == "1")
                    {
                        chkalltdadm.Attributes.Add("class", "hidden");
                        admttltdclspN.Attributes["colspan"] = "1";
                        finalftrttladm.Attributes["colspan"] = "3";
                        finalttlftrbtnchk.Attributes.Add("class", "hidden");
                    }
                }
                catch (Exception ex)
                {
                }
            }

        }




        private void find_all_paid_fee()
        {
            DataTable dt = PayrollMy.dataTable("select *,(select top 1 session_id from session_details where Session=Student_Payment_History.Session) as session_id from Student_Payment_History  where Session='" + ViewState["session"].ToString() + "' and Addmission_no='" + lbl_admissionno.Text + "' and Type='Admission'   ORDER BY id ASC");
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


        protected void txt_paid_amount_adm_TextChanged(object sender, EventArgs e)
        {
            try
            {

                lbl_totaldues.Text = (My.toDouble(lbl_paybaleamount.Text) - (My.toDouble(txt_paid_amount_adm.Text) + My.toDouble(ViewState["adjestamount"].ToString()))).ToString("0.00");

                if (My.toDouble(lbl_paybaleamount.Text) > 0)
                {
                    chk_split_month.Visible = true;
                }
                else
                {
                    chk_split_month.Visible = false;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
            }
            catch
            {
            }
        }



        #region pay
        string type = "";
        public bool payment_status = false;
        protected void btn_submit_admission_fee_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_submit_admission_fee.Text == "Pay Now")
                {
                    string isCheckPending = My.isChequePending(ViewState["sessionid"].ToString(), lbl_admissionno.Text, txt_bank_date.Text);
                    if (isCheckPending == "1")
                    {
                        check_is_cheque_pending(ViewState["sessionid"].ToString(), lbl_admissionno.Text);
                        return;
                    }


                    if (txt_date.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
                        payment_status = false;
                        Alertme("Please choose payment date.", "warning");
                        txt_date.Focus();
                        return;
                    }
                    if (txt_paid_amount_adm.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
                        payment_status = false;
                        Alertme("Please enter paid amount first...", "warning");
                        txt_paid_amount_adm.Focus();
                        return;
                    }
                    if (My.toDouble(txt_paid_amount_adm.Text) <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
                        payment_status = false;
                        Alertme("Please enter paid amount first...", "warning");
                        txt_paid_amount_adm.Focus();
                        return;
                    }

                    if (ddl_paymentmode_adm.Text == "Cheque")
                    {
                        if (txt_transaction_no_adm.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
                            payment_status = false;
                            txt_transaction_no_adm.Focus();
                            Alertme("Please enter cheque no.", "warning");
                            return;
                        }
                    }


                    #region AdmFEE
                    if (My.toDouble(lbl_totaldues.Text) >= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
                        if (rd_old_bill.Checked == true)
                        { }
                        else
                        {
                            int pay_idate = My.DateConvertToIdate(txt_date.Text);
                            bool chek_fee = My.find_fee_taken_date("Admission_fee_collection", pay_idate, lbl_admissionno.Text, ViewState["session"].ToString());
                            if (chek_fee == false)
                            {
                                Alertme("Payment is already done on your chosen date.", "warning");
                                return;
                            }
                        }

                        bool flag = false; string slip_no = "";
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                        {
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();
                            if (rd_old_bill.Checked == true)
                            {
                            }
                            else
                            {
                                if (ViewState["Transfer_Status"].ToString() == "New")
                                {
                                    type = "Admission";
                                    slip_no = payments.invoice_format_admssion("slip_no", con);
                                }
                                else
                                {
                                    type = "Annual";
                                    slip_no = payments.invoice_readmission("slip_no", con);
                                }
                            }

                            string ad_no = lbl_admissionno.Text;
                            string entry_id = "AD" + cretesessionid(con);


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
                                    //string chek_bill_no = payments.get_old_bill_no(slip_no, ViewState["branchid"].ToString(), con);
                                    //if (chek_bill_no != "Yes")
                                    //{
                                    //    Alertme(chek_bill_no, "warning");
                                    //    return;
                                    //}
                                }
                            }
                            // payment(slip_no, entry_id, ad_no, con);
                            payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received " + type + " fee :-" + txt_paid_amount_adm.Text + " Adjust Amount" + ViewState["adjestamount"].ToString() + " rs,Slip No :- " + slip_no + " from " + lbl_studentname.Text + ", Admission No :-" + ad_no, con);
                            if (ddl_paymentmode_adm.Text == "Cheque")
                            {
                                payments.exeSql("insert into Fee_payment_by_cheque_status(Session_id,Admission_no,Monthly_slip_no,Yearly_slip_no,Payment_type,Cheque_no,Bank_name,Cheque_date,Status,Created_by,Created_date,Created_time,Created_idate,Is_group_payment,Cheque_amount) values ('" + ViewState["sessionid"].ToString() + "','" + lbl_admissionno.Text + "','" + slip_no + "','0','AdmissionFee','" + lbl_mode_trns_no_adm.Text + "','" + ddl_bank.Text + "','" + txt_bank_date.Text + "','PENDING','" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "','" + mycode.idate() + "','0','" + txt_paid_amount_adm.Text + "');", con);
                            }
                            payment_status = true;
                            flag = true;
                            con.Close();
                            scope.Complete();
                        }

                        //TRANSITION COMPLETE
                        if (flag == true)
                        {
                            #region PushSendinG
                            if (My.toDouble(txt_paid_amount_adm.Text) > 0)
                            {

                                try
                                {
                                    string sub = "";
                                    string messge = "";
                                    if (ViewState["Transfer_Status"].ToString() == "New")
                                    {
                                        sub = "Admission Fee Deposit";
                                        messge = "Dear " + lbl_name.Text + " you have paid admission fee :- " + txt_paid_amount_adm.Text + " date :- " + txt_date.Text + " slip no.:-" + slip_no;
                                    }
                                    else
                                    {
                                        sub = "Annual Fee Deposit";
                                        messge = "Dear " + lbl_name.Text + " you have paid annual fee :- " + txt_paid_amount_adm.Text + " date :- " + txt_date.Text + " slip no.:-" + slip_no;
                                    }
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
                            }
                            #endregion

                            txt_paid_amount_adm.Text = "";
                            string confirmValue = string.Empty;
                            confirmValue = Request.Form["confirm_value_adm"];
                            if (confirmValue == "Yes")
                            {
                                string url = "slip/Admission_slip.aspx?admissionno=" + lbl_admissionno.Text + "&sessionid=" + ViewState["sessionid"].ToString() + "&classid=" + ViewState["classid"].ToString() + "&Slip_no=" + slip_no + "";
                                Response.Redirect(url, false);
                            }
                            else
                            {
                                Session["msg"] = "Payment has been successfully taken.";
                                if (ViewState["page_reset"].ToString() == "1")
                                {
                                    Response.Redirect("fees-collection-1.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect("fees-collection-1.aspx?adm=" + lbl_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);

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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
                        payment_status = false;
                        Alertme("Please enter valid paid amount first...", "warning");
                        txt_paid_amount_adm.Focus();
                        return;
                    }
                    #endregion

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
        }




        private void update_Add_Student_Money_receipt()
        {
            int growcountS = grid_adjustamount.Rows.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Unique_id = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Unique_id");
                mycode.executequery("update Add_Student_Money_receipt set Status='Used',Used_date='" + txt_date.Text + "',Used_Idate='" + mycode.ConvertStringToiDate(txt_date.Text) + "',Used_Time='" + mycode.time() + "' where Unique_id='" + lbl_Unique_id.Text + "' ");
            }
        }

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

        private void send_data_to_annual_fee_collection(string slip_no, string entry_id, string parameter, SqlConnection con)
        {
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and  parameter like '%'" + parameter + "'%' and feetype!='Previous Dues'", con);
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
                dr["session"] = ViewState["session"].ToString(); ;
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
                dr[8] = ddl_paymentmode_adm.Text;
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
        {
            // monyricpt
            string class_id = ViewState["classid"].ToString();
            double paid_amount = My.toDouble(txt_paid_amount_adm.Text);
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
                            send_data_in_fee_collection_slip_adm(My.toDouble(dr["payable"].ToString()).ToString("0.00"), dues.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, con);
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
                            send_data_in_fee_collection_slip_adm(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, con);
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



        private void send_data_to_school_ledger(string transcation, string entry_id, SqlConnection con)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Particulars"] = type + " Fee Collection";
            dr["Discription"] = type + " fee for " + lbl_studentname.Text + " Adm.No:-" + lbl_admissionno.Text + " Total Bill :-" + lbl_paybaleamount.Text + " Paid Amount :-  " + txt_paid_amount_adm.Text;
            dr["AllInput"] = My.toDouble(txt_paid_amount_adm.Text).ToString("0.00");
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
        }


        private void send_data_in_student_payment_history_adm_popup(string type, string slip_no, string entry_id, string ad_no, string parameter_New, SqlConnection con)
        {
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Transection_in,parameter_New,Bank_name,Bank_date,Created_date,Created_idate,Is_coupon_applied,Payee_bank) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,@Transection_in,@parameter_New,@Bank_name,@Bank_date,@Created_date,@Created_idate,@Is_coupon_applied,@Payee_bank)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", ad_no);
            cmd.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
            cmd.Parameters.AddWithValue("@Date", txt_date.Text);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + lbl_studentname.Text + " Paid Amount : " + txt_paid_amount_adm.Text + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", My.toDouble(txt_paid_amount_adm.Text).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@mode", ddl_paymentmode_adm.Text);
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
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", txt_transaction_no_adm.Text);
            cmd.Parameters.AddWithValue("@Transection_in", "Software");
            cmd.Parameters.AddWithValue("@parameter_New", parameter_New);
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Is_coupon_applied", "0");

            if (ddl_bank.Text == "Cash")
            {
                cmd.Parameters.AddWithValue("@Bank_name", "");
                cmd.Parameters.AddWithValue("@Bank_date", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Bank_name", ddl_bank.Text);
                cmd.Parameters.AddWithValue("@Bank_date", txt_bank_date.Text);
            }

            if (ddl_payee_bank.Text == "Select")
            {
                cmd.Parameters.AddWithValue("@Payee_bank", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Payee_bank", ddl_payee_bank.Text);
            }
            if (payments.InsertUpdateData(cmd, con))
            {
                // money recpit
                int growcountS = grid_adjustamount.Rows.Count;
                for (int iS = 0; iS < growcountS; iS++)
                {
                    Label lbl_Unique_id = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Unique_id");
                    Label lbl_date = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_date");
                    Label lbl_Amount = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Amount");
                    Label lbl_idate = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_idate");
                    Label lbl_paymentmode = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Paymentmode");
                    Label lbl_Payment_id = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Payment_id");

                    SqlCommand cmd1;
                    string query1 = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Transection_in,Is_coupon_applied) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Money_Receipt_Date,@Money_Receipt_Idate,@Unique_id,@Adjust_type,@Transection_in,@Is_coupon_applied)";
                    cmd1 = new SqlCommand(query1);
                    cmd1.Parameters.AddWithValue("@Addmission_no", ad_no);
                    cmd1.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
                    cmd1.Parameters.AddWithValue("@Date", lbl_date.Text);
                    cmd1.Parameters.AddWithValue("@Idate", lbl_idate.Text);
                    cmd1.Parameters.AddWithValue("@Description", type + " fee collection for(Money Receipt) " + lbl_studentname.Text + " Paid Amount : " + lbl_Amount.Text + " /-");
                    cmd1.Parameters.AddWithValue("@Entry_id", entry_id);
                    cmd1.Parameters.AddWithValue("@Slip_no", slip_no);
                    cmd1.Parameters.AddWithValue("@Amount", My.toDouble(lbl_Amount.Text).ToString("0.00"));
                    cmd1.Parameters.AddWithValue("@Type", type);
                    cmd1.Parameters.AddWithValue("@mode", lbl_paymentmode.Text);
                    cmd1.Parameters.AddWithValue("@discount", "0");
                    cmd1.Parameters.AddWithValue("@Discoun_in_School", "0.00");
                    cmd1.Parameters.AddWithValue("@Discoun_in_Hostel", "0.00");
                    cmd1.Parameters.AddWithValue("@Discoun_in_Transport", "0.00");
                    cmd1.Parameters.AddWithValue("@fine", "0.00");
                    cmd1.Parameters.AddWithValue("@is_ofline_sync", 0);
                    cmd1.Parameters.AddWithValue("@Is_online_sync", 0);
                    cmd1.Parameters.AddWithValue("@is_update_in_online", 0);
                    cmd1.Parameters.AddWithValue("@Previous_admission_no", 0);
                    cmd1.Parameters.AddWithValue("@App_Transection_id", "");
                    cmd1.Parameters.AddWithValue("@time", mycode.time());
                    cmd1.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                    cmd1.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                    cmd1.Parameters.AddWithValue("@Branch", ViewState["branchid"].ToString());
                    cmd1.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                    cmd1.Parameters.AddWithValue("@User_Slip_no", txt_slip_no.Text);
                    cmd1.Parameters.AddWithValue("@Pay_mode_transaction_no", lbl_Payment_id.Text);
                    cmd1.Parameters.AddWithValue("@Money_Receipt_Date", lbl_date.Text);
                    cmd1.Parameters.AddWithValue("@Money_Receipt_Idate", lbl_idate.Text);
                    cmd1.Parameters.AddWithValue("@Unique_id", lbl_Unique_id.Text);
                    cmd1.Parameters.AddWithValue("@Adjust_type", "Adjust");
                    cmd.Parameters.AddWithValue("@Transection_in", "Software");
                    cmd.Parameters.AddWithValue("@Is_coupon_applied", "0");
                    if (payments.InsertUpdateData(cmd1, con))
                    {
                        SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
                        DataSet ds = new DataSet();
                        ad.Fill(ds, "SchoolLedger");
                        DataTable dt = ds.Tables[0];
                        DataRow dr = dt.NewRow();
                        dr["Particulars"] = type + " Money Receipt";
                        dr["Discription"] = type + " Money Receipt " + lbl_studentname.Text + " Adm.No:-" + lbl_admissionno.Text + " Paid Amount :-  " + My.toDouble(lbl_Amount.Text).ToString("0.00");
                        dr["AllInput"] = My.toDouble(lbl_Amount.Text).ToString("0.00");
                        dr["AllOutput"] = "0";
                        dr["IDate"] = lbl_idate.Text; //Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                        dr["Date"] = lbl_date.Text;
                        dr["TransactionId"] = slip_no;
                        dr["entry_id"] = entry_id;
                        dr["session"] = ViewState["session"].ToString();
                        dr["Ledger_Type"] = "School";
                        dr["time"] = mycode.time();
                        dr["user_id"] = ViewState["Userid"].ToString();
                        dr["Addmission_no"] = lbl_admissionno.Text;
                        dr["branchid"] = ViewState["branchid"].ToString();
                        dr["Unique_id"] = lbl_Unique_id.Text;
                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                    }
                }
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


        private void send_data_in_fee_collection_slip_adm(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, SqlConnection con)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate,Hostel_id) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + payable + "','" + paid + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + prevpaid + "','" + ViewState["branchid"].ToString() + "','0','" + txt_payment_date.Text + "','" + My.DateConvertToIdate(txt_payment_date.Text) + "','" + ViewState["Hostel_id"].ToString() + "');";
            payments.exeSql(qry, con);
        }
        #endregion



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
                DataTable dt = PayrollMy.dataTable(query);
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
                        DataTable dt1 = PayrollMy.dataTable(query1);
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
            DataTable dt = PayrollMy.dataTable("select * from Temp_Adjust_amount  where  Admission_no='" + lbl_admissionno.Text + "'    ");
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


        protected void ddl_paymentmode_adm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_paymentmode_adm.SelectedItem.Text == "Cash")
            {
                pnl_mode_t_n_dv.Visible = false;
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "Netbanking")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "Deposited In Bank")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = true;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "Sbdebit")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "Cheque")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Cheque No.";
                bank_dts_amd.Visible = true;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "NEFT")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "UTR No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "Debitcard")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "Creditcard")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "Otherdcard")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "UPI")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "Demand Draft(DD)")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = true;
            }
            if (ddl_paymentmode_adm.SelectedItem.Text == "Pos")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.Text = "Transaction No.";
                bank_dts_amd.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
        }
        #endregion

        #region
        protected void chk_split_month_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_split_month.Checked == true)
                {
                    splitmonth.Visible = true;
                }
                else
                {
                    splitmonth.Visible = false;
                }
            }
            catch
            {
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdmFee();", true);
        }
        double total_split_data = 0.00;

        protected void grid_split_amount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string sessionid = "0";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Month_name = (Label)e.Row.FindControl("lbl_Month_name");

                TextBox txt_split_month_fee = (TextBox)e.Row.FindControl("txt_split_month_fee");
                try
                {
                    sessionid = My.get_sess_prm(ViewState["session"].ToString());
                }
                catch
                {
                    sessionid = My.get_session_id();
                }


                txt_split_month_fee.Text = My.Bind_data_if_add(lbl_admission_no.Text, sessionid, lbl_Month_name.Text);


                if (txt_split_month_fee.Text != "")
                {
                    total_split_data = total_split_data + My.toDouble(txt_split_month_fee.Text);
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_split_fee = (Label)e.Row.FindControl("lbl_total_split_fee");


                lbl_total_split_fee.Text = total_split_data.ToString("0.00");


            }
        }



        protected void txt_split_month_fee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    TextBox btnc = (TextBox)sender;
                    GridViewRow row = (GridViewRow)btnc.Parent.Parent;
                    string txt_split_month_fee = ((TextBox)row.FindControl("txt_split_month_fee")).Text;

                    if (My.toDouble(txt_split_month_fee) <= (My.toDouble(lbl_totaldues.Text) - My.toDouble(ViewState["total_split_amount"].ToString())))
                    {

                    }
                    else
                    {
                        ((TextBox)row.FindControl("txt_split_month_fee")).Text = "0";
                        Alertme("Please enter amount  less than total dues amount.", "warning");
                    }
                    bind_split_month_amount();
                }
                catch (Exception ex)
                {
                }

            }
            catch (Exception ex)
            {
            }

        }


        private void bind_split_month_amount()
        {
            ViewState["total_split_amount"] = "0";
            int i;
            double totalrate = 0;
            int gridview_rowcount = grid_split_amount.Rows.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                TextBox txt_split_month_fee = (TextBox)grid_split_amount.Rows[i].FindControl("txt_split_month_fee");
                if (txt_split_month_fee.Text != "")
                {
                    totalrate = totalrate + My.toDouble(txt_split_month_fee.Text);
                }
            }


            string total_split_fee = "Total Amount : " + totalrate.ToString("0.00");
            (grid_split_amount.FooterRow.FindControl("lbl_total_split_fee") as Label).Text = total_split_fee;
            ViewState["total_split_amount"] = totalrate.ToString("0.00");
        }

        private void send_data_to_split_month(string slip_no, string feetype, SqlConnection con)
        {
            string sessionid = ViewState["sessionIDs"].ToString();  /* My.get_sess_prm(ViewState["session"].ToString());*/
            for (int i = 0; i < grid_split_amount.Rows.Count; i++)
            {
                Label lbl_Month_name = (Label)grid_split_amount.Rows[i].FindControl("lbl_Month_name");
                TextBox txt_split_month_fee = (TextBox)grid_split_amount.Rows[i].FindControl("txt_split_month_fee");
                if (My.toDouble(txt_split_month_fee.Text) > 0)
                {
                    payments.insert_data_split_month(lbl_Month_name.Text, txt_split_month_fee.Text, feetype, slip_no, lbl_admissionno.Text, sessionid, con);
                }
                else
                {
                }
            }
        }


        private void Bind_month_name_split()
        {

            string query = "Select * from Month_Index order by Position";
            DataTable dt = PayrollMy.dataTable(query);
            if (dt.Rows.Count == 0)
            {

                grid_split_amount.DataSource = null;
                grid_split_amount.DataBind();

            }
            else
            {

                grid_split_amount.DataSource = dt;
                grid_split_amount.DataBind();
            }
        }
        #endregion





        //==================*******************************************
        //==================*******************************************
        //==================*******************************************

        #region MakeAdmissionFEE
        private void make_admission_fee(SqlConnection con, double PaidAmt, string slip_no, string Full_paid_amt, string feeTypes)
        {
            string parameter = "";
            string parameter_id = "";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                parameter_id = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "1" : "5";
            }
            else
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                parameter_id = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "2" : "6";
            }
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + lbl_admissionno.Text + "' and parameter='" + parameter + "' and session='" + lbl_session.Text + "')t";
            DataTable fee_dt = payments.dataTable(qry, con);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + lbl_session.Text + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + lbl_admissionno.Text + "'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "' ) t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + lbl_session.Text + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where   Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + lbl_admissionno.Text + "'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t";
                }
                fee_dt = payments.dataTable(qry, con);
            }

            DataTable dt = payments.dataTable(qry, con);
            if (dt.Rows.Count > 0)
            {
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

                hd_paybaleamount.Value = payble_after_disc.ToString("0.00");
                hd_adjustamount.Value = payble_after_disc.ToString("0.00");
                hd_totalamount.Value = payable.ToString("0.00");
                hd_total_discount.Value = disc.ToString("0.00");



                string ad_no = lbl_admissionno.Text;
                string entry_id = "AD" + cretesessionid(con);
                ViewState["yearlYSLipNo"] = slip_no;
                admission_payment(slip_no, entry_id, ad_no, con, PaidAmt, Full_paid_amt, feeTypes);
                payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received " + type + " fee :-" + Full_paid_amt.ToString() + " Adjust Amount" + hd_total_discount.Value + " rs,Slip No :- " + slip_no + " from " + lbl_studentname.Text + ", Admission No :-" + ad_no, con);
            }
        }


        private void admission_payment(string slip_no, string entry_id, string ad_no, SqlConnection con, double PaidAmt, string Full_paid_amt, string type)
        {
            string Uid = slip_no;
            string Tag = PaidAmt.ToString();
            string parameter = "";
            if (type == "Admission")
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
            }
            else
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
            }

            send_data_in_student_payment_history_admission(type, slip_no, entry_id, ad_no, parameter, con, PaidAmt, Full_paid_amt);
            send_data_to_school_ledger_admission(slip_no, entry_id, con, My.toDouble(Full_paid_amt));
            create_admission_annual_dues_admission(parameter, con, PaidAmt);
            send_data_in_feetypewise_collection_admission(slip_no, entry_id, parameter, con, PaidAmt);
            if (type == "Admission")
            {
                send_data_to_admission_fee_collection_admission(slip_no, entry_id, parameter, con);
            }
            else
            {
                send_data_to_annual_fee_collection(slip_no, entry_id, parameter, con);
            }

            update_data_to_admission_registor(con);

        }

        private void send_data_to_admission_fee_collection_admission(string slip_no, string entry_id, string parameter, SqlConnection con)
        {
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues'", con);
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
                dr[4] = My.toDouble(hd_paybaleamount.Value).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = txt_payment_date.Text;
                dr[8] = ddl_paymentmode.Text;
                dr[9] = slip_no;
                dr["session"] = ViewState["session"].ToString();
                dr["idate"] = Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                dr["remark"] = txt_description.Text;
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



        private void send_data_in_feetypewise_collection_admission(string slip_no, string entry_id, string parameter, SqlConnection con, double PaidAmt)
        {
            string class_id = ViewState["classid"].ToString();
            double paid_amount = PaidAmt;
            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter + "' and status='Dues'", con);
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
                    ///=======
                    int feedtCount = grd_fee_adm_fee.Items.Count; string isChecked = "0";
                    for (int i = 0; i < feedtCount; i++)
                    {
                        Label lblcontent_id = (Label)grd_fee_adm_fee.Items[i].FindControl("lblcontent_id");
                        CheckBox chk_get_fee = (CheckBox)grd_fee_adm_fee.Items[i].FindControl("chkRowData");
                        if (My.toBool(chk_get_fee.Checked))
                        {
                            if (lblcontent_id.Text == dr["content_id"].ToString())
                            {
                                isChecked = "1";
                            }
                        }
                    }
                    ///=======


                    if (isChecked == "1")
                    {
                        if (paid_amount >= 0)
                        {
                            string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";
                            double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);
                            dr["Date"] = txt_payment_date.Text;
                            dr["idate"] = My.toDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
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
                                send_data_in_fee_collection_slip_adm(My.toDouble(dr["payable"].ToString()).ToString("0.00"), dues.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, con);
                                discountMaster.save_discount(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admissionno.Text, dr["parameter"].ToString(), dr["content_id"].ToString(), dr["month"].ToString(), dr["payable"].ToString(), dr["Disc"].ToString(), slip_no, dr["position"].ToString(), ViewState["Userid"].ToString(), dr["feetype"].ToString(), con);
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
                                send_data_in_fee_collection_slip_adm(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, con);
                                discountMaster.save_discount(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admissionno.Text, dr["parameter"].ToString(), dr["content_id"].ToString(), dr["month"].ToString(), dr["payable"].ToString(), dr["Disc"].ToString(), slip_no, dr["position"].ToString(), ViewState["Userid"].ToString(), dr["feetype"].ToString(), con);
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
                    else
                    {
                        string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";
                        string prevpaid = dr["paid"].ToString();
                        dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                        dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + 0).ToString("0.00");
                        dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                        dr["status"] = "Dues";

                        #region send in collection slip
                        send_data_in_fee_collection_slip_adm(My.toDouble(dr["payable"].ToString()).ToString("0.00"), "0.00", dr["parameter"].ToString(), dr["feetype"].ToString(), dr["content_id"].ToString(), slip_no, entry_id, dr["Ledger"].ToString(), lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, con);
                        discountMaster.save_discount(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admissionno.Text, dr["parameter"].ToString(), dr["content_id"].ToString(), dr["month"].ToString(), dr["payable"].ToString(), dr["Disc"].ToString(), slip_no, dr["position"].ToString(), ViewState["Userid"].ToString(), dr["feetype"].ToString(), con);
                        #endregion

                        dr["transection"] = slip_no;
                        dr["is_readyfor_sync"] = true;
                        dr["is_sync"] = false;
                        dr["group_id"] = group_id;
                        dr["class_id"] = class_id;
                    }
                }

                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(tdt);
            }
        }

        private void create_admission_annual_dues_admission(string parameter, SqlConnection con, double PaidAmt)
        {
            if (payments.dataTable("select session,feetype,payable,paid,dues,status,content_id from dbo.[Typewise_fee_collection] WHERE admission_no='" + lbl_admissionno.Text + "' and parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "'", con).Rows.Count == 0)
            {
                string query = "";
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "'";
                }
                else
                {
                    query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id='" + ViewState["Hostel_id"].ToString() + "' and admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id='" + ViewState["Hostel_id"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "'";
                }
                //string query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "'";
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


        private void send_data_to_school_ledger_admission(string transcation, string entry_id, SqlConnection con, double PaidAmt)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Particulars"] = type + " Fee Collection";
            dr["Discription"] = type + " fee for " + lbl_studentname.Text + " Adm.No:-" + lbl_admissionno.Text + " Total Bill :-" + PaidAmt.ToString() + " Paid Amount :-  " + PaidAmt.ToString();
            dr["AllInput"] = PaidAmt.ToString("0.00");
            dr["AllOutput"] = "0";
            dr["IDate"] = Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
            dr["Date"] = txt_payment_date.Text;
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
        }


        private void send_data_in_student_payment_history_admission(string type, string slip_no, string entry_id, string ad_no, string parameter_New, SqlConnection con, double PaidAmt, string Full_paid_amt)
        {
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Transection_in,parameter_New,Bank_name,Bank_date,Created_date,Created_idate,Is_coupon_applied,Payee_bank) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,@Transection_in,@parameter_New,@Bank_name,@Bank_date,@Created_date,@Created_idate,@Is_coupon_applied,@Payee_bank)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", ad_no);
            cmd.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
            cmd.Parameters.AddWithValue("@Date", txt_payment_date.Text);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + lbl_studentname.Text + " Paid Amount : " + Full_paid_amt.ToString() + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", My.toDouble(Full_paid_amt).ToString("0.00"));
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
            cmd.Parameters.AddWithValue("@Remarks", txt_description.Text);
            cmd.Parameters.AddWithValue("@User_Slip_no", txt_slip_no.Text);
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", txt_trans_no.Text);
            cmd.Parameters.AddWithValue("@Transection_in", "Software");
            cmd.Parameters.AddWithValue("@parameter_New", parameter_New);
            if (ddl_bank.Text == "Cash")
            {
                cmd.Parameters.AddWithValue("@Bank_name", "");
                cmd.Parameters.AddWithValue("@Bank_date", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Bank_name", ddl_bank.Text);
                cmd.Parameters.AddWithValue("@Bank_date", txt_bank_date.Text);
            }

            if (ddl_payee_bank.Text == "Select")
            {
                cmd.Parameters.AddWithValue("@Payee_bank", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Payee_bank", ddl_payee_bank.Text);
            }

            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Is_coupon_applied", "0");
            if (payments.InsertUpdateData(cmd, con))
            {
                // money recpit
                SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "SchoolLedger");
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr["Particulars"] = type + " Money Receipt";
                dr["Discription"] = type + " Money Receipt " + lbl_studentname.Text + " Adm.No:-" + lbl_admissionno.Text + " Paid Amount :-  " + PaidAmt.ToString("0.00");
                dr["AllInput"] = PaidAmt.ToString("0.00");
                dr["AllOutput"] = "0";
                dr["IDate"] = Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                dr["Date"] = txt_payment_date.Text;
                dr["TransactionId"] = slip_no;
                dr["entry_id"] = entry_id;
                dr["session"] = ViewState["session"].ToString();
                dr["Ledger_Type"] = "School";
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString();
                dr["Addmission_no"] = lbl_admissionno.Text;
                dr["branchid"] = ViewState["branchid"].ToString();
                dr["Unique_id"] = entry_id;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }
        #endregion



        //==================*******************************************
        //==================*******************************************
        //==================*******************************************
        #region MakeAnnualFEE
        private void make_annual_fee(SqlConnection con, double PaidAmt, string slip_no, string Total_paid_amount)
        {
            string parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='2' or parameter_id='6') and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + lbl_admissionno.Text + "' and parameter='" + parameter + "' and session='" + lbl_session.Text + "')t";
            DataTable fee_dt = payments.dataTable(qry, con);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + lbl_session.Text + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='2' or parameter_id='6') and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where   Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "') t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + lbl_session.Text + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where  Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='2' or parameter_id='6') and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t";
                }
                fee_dt = payments.dataTable(qry, con);
            }
            DataTable dt = payments.dataTable(qry, con);
            if (dt.Rows.Count > 0)
            {
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
                gt_previous_dues_amount(con);
                double totalpay = payble_after_disc + My.toDouble(lbl_previous_year_dues.Text);


                hd_paybaleamount.Value = totalpay.ToString("0.00");
                hd_adjustamount.Value = payble_after_disc.ToString("0.00");
                hd_totalamount.Value = payable.ToString("0.00");
                hd_total_discount.Value = disc.ToString("0.00");



                ///=============================
                ///=============================
                type = "Annual";
                string ad_no = lbl_admissionno.Text;
                string entry_id = "AD" + cretesessionid(con);
                ViewState["yearlYSLipNo"] = slip_no;
                annual_payment(slip_no, entry_id, ad_no, con, PaidAmt, Total_paid_amount);
                payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received " + type + " fee :-" + Total_paid_amount + " Adjust Amount" + hd_total_discount.Value + " rs,Slip No :- " + slip_no + " from " + lbl_studentname.Text + ", Admission No :-" + ad_no, con);

            }
        }



        private void gt_previous_dues_amount(SqlConnection con)
        {
            DataTable dt = payments.dataTable("select Dues_Amount from Previous_Year_Dues  where Session_id='" + ViewState["sessionid"].ToString() + "' and AdmissionNumber='" + lbl_admissionno.Text + "'  and Class_id='" + ViewState["classid"].ToString() + "' and Status='Unpaid'", con);
            if (dt.Rows.Count == 0)
            {
                lbl_previous_year_dues.Text = "0.00";
            }
            else
            {
                lbl_previous_year_dues.Text = dt.Rows[0][0].ToString();
            }
        }

        private void annual_payment(string slip_no, string entry_id, string ad_no, SqlConnection con, double PaidAmt, string Total_paid_amount)
        {
            string Uid = slip_no;
            string Tag = PaidAmt.ToString();
            string parameter = "";
            parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
            send_data_in_student_payment_history_annual(type, slip_no, entry_id, ad_no, parameter, con, My.toDouble(Total_paid_amount));
            send_data_to_school_ledger_annual(slip_no, entry_id, con, My.toDouble(Total_paid_amount));
            create_admission_annual_dues_annual(parameter, con);
            send_data_in_feetypewise_collection_annual(slip_no, entry_id, parameter, con, PaidAmt);
            send_data_to_annual_fee_collection_annual(slip_no, entry_id, parameter, con);
            update_data_to_admission_registor(con);
        }

        private void send_data_to_annual_fee_collection_annual(string slip_no, string entry_id, string parameter, SqlConnection con)
        {
            double old_dues_amount = 0;
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues' ", con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = lbl_admissionno.Text;
                dr[2] = My.toDouble(hd_totalamount.Value).ToString("0.00");
                dr[3] = My.toDouble(hd_total_discount.Value).ToString("0.00");
                dr[4] = My.toDouble(hd_paybaleamount.Value).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = txt_payment_date.Text;
                dr[8] = ddl_paymentmode.Text;
                dr[9] = slip_no;
                dr["session"] = ViewState["session"].ToString();
                dr["idate"] = Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                dr["remark"] = txt_description.Text;
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
                    dr[4] = My.toDouble(My.toDouble(dr[4]) + My.toDouble(old_dues_amount)).ToString("0.00");
                    dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void send_data_in_feetypewise_collection_annual(string slip_no, string entry_id, string parameter, SqlConnection con, double PaidAmt)
        {
            string class_id = ViewState["classid"].ToString();
            double paid_amount = PaidAmt;
            if (My.toDouble(lbl_previous_year_dues.Text) > 0)
            {
                string previusyear = "Previous Year";
                string previousyearcontent_id = "101";
                double paid = 0;
                if (paid_amount > My.toDouble(lbl_previous_year_dues.Text))
                {
                    paid = My.toDouble(lbl_previous_year_dues.Text);
                    //insert
                    paid_amount = paid_amount - My.toDouble(lbl_previous_year_dues.Text);
                    payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + lbl_admissionno.Text + "','" + lbl_class.Text + "','" + lbl_session.Text + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + previusyear + "','" + My.toDouble(lbl_previous_year_dues.Text).ToString("0.00") + "','" + paid.ToString("0.00") + "','" + "0.00" + "','Paid','" + payments.get_start_month(con) + "','" + previousyearcontent_id + "','" + slip_no + "','School','false','false','false','" + 0 + "','" + paid.ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')", con);
                    payments.exeSql("update Previous_Year_Dues set Status='Paid' where  Session_id='" + ViewState["sessionid"].ToString() + "' and AdmissionNumber='" + lbl_admissionno.Text + "' and Class_id='" + ViewState["classid"].ToString() + "'", con);

                    string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate,Hostel_id) values ('" + lbl_admissionno.Text + "','" + lbl_session.Text + "','" + ViewState["classid"].ToString() + "','" + ViewState["Section"] + "','" + parameter + "','" + previusyear + "','" + previousyearcontent_id + "','" + paid.ToString("0.00") + "','" + paid.ToString("0.00") + "','" + slip_no + "','School','" + "0.00" + "','','0','0','" + ViewState["branchid"].ToString() + "','0','" + txt_payment_date.Text + "','" + mycode.ConvertStringToiDate(txt_payment_date.Text) + "','" + ViewState["Hostel_id"].ToString() + "');";
                    payments.exeSql(qry, con);
                }
                else
                {
                    paid = paid_amount;
                    //insert
                    double duesamount = My.toDouble(lbl_previous_year_dues.Text) - paid;
                    payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + lbl_admissionno.Text + "','" + lbl_class.Text + "','" + lbl_session.Text + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + previusyear + "','" + My.toDouble(lbl_previous_year_dues.Text).ToString("0.00") + "','" + paid.ToString("0.00") + "','" + duesamount.ToString("0.00") + "','Dues','" + payments.get_start_month(con) + "','" + previousyearcontent_id + "','" + slip_no + "','School','false','false','false','" + 0 + "','" + paid.ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')", con);
                    payments.exeSql("update Previous_Year_Dues set Status='Dues' where Session_id='" + ViewState["sessionid"].ToString() + "' and AdmissionNumber='" + lbl_admissionno.Text + "' and Class_id='" + ViewState["classid"].ToString() + "' ", con);

                    string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + lbl_admissionno.Text + "','" + lbl_session.Text + "','" + ViewState["classid"].ToString() + "','" + ViewState["Section"] + "','" + parameter + "','" + previusyear + "','" + previousyearcontent_id + "','" + paid.ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','School','" + "0.00" + "','','0','0','" + ViewState["branchid"].ToString() + "','0','" + txt_payment_date.Text + "','" + mycode.ConvertStringToiDate(txt_payment_date.Text) + "');";
                    payments.exeSql(qry, con);
                    paid_amount = 0;
                }
            }


            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and status='Dues' and parameter='" + parameter + "'", con);
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
                    ///=======
                    int feedtCount = grd_fee_adm_fee.Items.Count; string isChecked = "0";
                    for (int i = 0; i < feedtCount; i++)
                    {
                        Label lblcontent_id = (Label)grd_fee_adm_fee.Items[i].FindControl("lblcontent_id");
                        CheckBox chk_get_fee = (CheckBox)grd_fee_adm_fee.Items[i].FindControl("chkRowData");
                        if (My.toBool(chk_get_fee.Checked))
                        {
                            if (lblcontent_id.Text == dr["content_id"].ToString())
                            {
                                isChecked = "1";
                            }
                        }
                    }
                    ///=======

                    if (isChecked == "1")
                    {
                        if (paid_amount >= 0)
                        {
                            string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";
                            double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);
                            dr["Date"] = txt_payment_date.Text;
                            dr["idate"] = My.toDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                            if (paid_amount >= dues)
                            {
                                string prevpaid = My.toDouble(dr["paid"].ToString()).ToString("0.00");
                                paid_amount = paid_amount - dues;
                                //paid amt is gratter than dues so dues amt is actual paid.
                                string paid = My.toDouble(dr["dues"].ToString()).ToString("0.00");
                                dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                                dr["paid"] = My.toDouble(dr["Payable_after_disc"]);
                                dr["dues"] = "0";
                                dr["status"] = "Paid";
                                #region send in collection slip
                                send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), My.toDouble(dues.ToString()).ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, con);
                                discountMaster.save_discount(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admissionno.Text, dr["parameter"].ToString(), dr["content_id"].ToString(), dr["month"].ToString(), dr["payable"].ToString(), dr["Disc"].ToString(), slip_no, dr["position"].ToString(), ViewState["Userid"].ToString(), dr["feetype"].ToString(), con);
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
                                send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, con);
                                discountMaster.save_discount(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admissionno.Text, dr["parameter"].ToString(), dr["content_id"].ToString(), dr["month"].ToString(), dr["payable"].ToString(), dr["Disc"].ToString(), slip_no, dr["position"].ToString(), ViewState["Userid"].ToString(), dr["feetype"].ToString(), con);
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
                    else
                    {
                        string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";
                        string prevpaid = dr["paid"].ToString();
                        dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                        dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + 0).ToString("0.00");
                        dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                        dr["status"] = "Dues";

                        #region send in collection slip
                        send_data_in_fee_collection_slip_adm(My.toDouble(dr["payable"].ToString()).ToString("0.00"), "0.00", dr["parameter"].ToString(), dr["feetype"].ToString(), dr["content_id"].ToString(), slip_no, entry_id, dr["Ledger"].ToString(), lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, con);
                        discountMaster.save_discount(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admissionno.Text, dr["parameter"].ToString(), dr["content_id"].ToString(), dr["month"].ToString(), dr["payable"].ToString(), dr["Disc"].ToString(), slip_no, dr["position"].ToString(), ViewState["Userid"].ToString(), dr["feetype"].ToString(), con);
                        #endregion

                        dr["transection"] = slip_no;
                        dr["is_readyfor_sync"] = true;
                        dr["is_sync"] = false;
                        dr["group_id"] = group_id;
                        dr["class_id"] = class_id;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(tdt);
            }
        }

        private void create_admission_annual_dues_annual(string parameter, SqlConnection con)
        {
            if (payments.dataTable("select session,feetype,payable,paid,dues,status,content_id from dbo.[Typewise_fee_collection] WHERE admission_no='" + lbl_admissionno.Text + "' and parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "'", con).Rows.Count == 0)
            {
                string query = "";
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "'";
                }
                else
                {
                    query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id='" + ViewState["Hostel_id"].ToString() + "' and admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id='" + ViewState["Hostel_id"].ToString() + "' and  Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "'";
                }

                //string query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + lbl_admissionno.Text + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "'";
                DataTable dt = payments.dataTable(query, con);
                if (dt.Rows.Count == 0)
                { }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + lbl_admissionno.Text + "','" + lbl_class.Text + "','" + dr["session"].ToString() + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["feetype"].ToString() + "','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','Dues','" + payments.get_start_month(con) + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + My.toDouble(dr["payable_after_disc"].ToString()).ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')", con);
                    }
                }
            }
        }

        private void send_data_to_school_ledger_annual(string transcation, string entry_id, SqlConnection con, double PaidAmt)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Particulars"] = type + " Fee Collection";
            dr["Discription"] = type + " fee for " + lbl_studentname.Text + " Adm.No:-" + lbl_admissionno.Text + " Total Bill :-" + PaidAmt.ToString() + " Paid Amount :-  " + PaidAmt.ToString();
            dr["AllInput"] = PaidAmt.ToString("0.00");
            dr["AllOutput"] = "0";
            dr["IDate"] = Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
            dr["Date"] = txt_payment_date.Text;
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
        }

        private void send_data_in_student_payment_history_annual(string type, string slip_no, string entry_id, string ad_no, string parameter_New, SqlConnection con, double PaidAmt)
        {
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Transection_in,parameter_New,Bank_name,Bank_date,Created_date,Created_idate,Is_coupon_applied,Payee_bank) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,@Transection_in,@parameter_New,@Bank_name,@Bank_date,@Created_date,@Created_idate,@Is_coupon_applied,@Payee_bank)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", ad_no);
            cmd.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
            cmd.Parameters.AddWithValue("@Date", txt_payment_date.Text);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + lbl_studentname.Text + " Paid Amount : " + PaidAmt.ToString() + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", PaidAmt.ToString("0.00"));
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@mode", ddl_paymentmode.Text);
            cmd.Parameters.AddWithValue("@discount", hd_total_discount.Value);
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
            cmd.Parameters.AddWithValue("@Remarks", txt_description.Text);
            cmd.Parameters.AddWithValue("@User_Slip_no", txt_slip_no.Text);
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", txt_trans_no.Text);
            cmd.Parameters.AddWithValue("@Transection_in", "Software");
            cmd.Parameters.AddWithValue("@parameter_New", parameter_New);
            if (ddl_bank.Text == "Cash")
            {
                cmd.Parameters.AddWithValue("@Bank_name", "");
                cmd.Parameters.AddWithValue("@Bank_date", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Bank_name", ddl_bank.Text);
                cmd.Parameters.AddWithValue("@Bank_date", txt_bank_date.Text);
            }

            if (ddl_payee_bank.Text == "Select")
            {
                cmd.Parameters.AddWithValue("@Payee_bank", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Payee_bank", ddl_payee_bank.Text);
            }
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Is_coupon_applied", "0");
            if (payments.InsertUpdateData(cmd, con))
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "SchoolLedger");
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr["Particulars"] = type + " Money Receipt";
                dr["Discription"] = type + " Money Receipt " + lbl_studentname.Text + " Adm.No:-" + lbl_admissionno.Text + " Paid Amount :-  " + PaidAmt.ToString("0.00");
                dr["AllInput"] = PaidAmt.ToString("0.00");
                dr["AllOutput"] = "0";
                dr["IDate"] = Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                dr["Date"] = txt_payment_date.Text;
                dr["TransactionId"] = slip_no;
                dr["entry_id"] = entry_id;
                dr["session"] = ViewState["session"].ToString();
                dr["Ledger_Type"] = "School";
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString();
                dr["Addmission_no"] = lbl_admissionno.Text;
                dr["branchid"] = ViewState["branchid"].ToString();
                dr["Unique_id"] = entry_id;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }
        #endregion




        //=======================DISCOUNTS
        #region DISCOUNT
        private void for_discount(string session_name, string session_id, string class_id, string admission_no, string hostel_taken, string transport_taken, string hostel_id, string room_category_id, string TransportPath_id, string Boarding_Point_id)
        {
            DataTable dtBR = My.dataTable("select Admission_no from Student_mapping_with_boarding_with_lunch where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "'");
            if (dtBR.Rows.Count > 0)
            {
                ViewState["parameteridS"] = "44";
                ViewState["parameterDisc"] = "44";
            }
            ViewState["hostel_takens"] = hostel_taken;
            string qryDiscMonth = "select * from Month_Index where Month in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Dues') or Month not in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Paid') order by Position asc";
            if (ViewState["Is_quarterwise_payment"].ToString() == "1")
            {
                qryDiscMonth = "select * from (select *,(select isnull(sum(convert(float, amount)),0) from Fee_master_content_wise where (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session_id='" + ViewState["sessionIDs"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and Month=Month_Index.Month)  as Total_fee from Month_Index where Month in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Dues') or Month not in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Paid')) t where convert(float, Total_fee)>0 order by Position asc";
            }
            DataTable dtmntH = My.dataTable(qryDiscMonth);
            if (dtmntH.Rows.Count > 0)
            {
                discBTN.Visible = true;
                rp_month_for_discount.DataSource = dtmntH.DefaultView;
                rp_month_for_discount.DataBind();
            }
            else
            {
                discBTN.Visible = false;
                rp_month_for_discount.DataSource = null;
                rp_month_for_discount.DataBind();
            }

            string qrydiscount = "select distinct MTH.Position,ar.session,ar.Session_id,ar.Class_id,ar.admissionserialnumber as Admission_no,dm.Date,dm.month,dm.Discount_on,(Select Discunt_Type from Student_Discunt_Type where Student_Discunt_Type_id=dm.Student_Discunt_Type_id) as Discunt_Type,fmc.content,dm.fee_head_id as content_id,fmc.amount,dm.disc_amount, (cast(amount as float)-cast(disc_amount as float)) after_disc from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.month=fmc.Month join Add_course_table ac on dm.Class_id=ac.course_id join admission_registor ar on dm.admission_no=ar.admissionserialnumber and dm.session=ar.session join Month_Index MTH on dm.month=MTH.Month where dm.group_id='3' and dm.session='" + session_name + "' and ar.admissionserialnumber='" + admission_no + "' and cast(dm.disc_amount as float) >0 UNION all select MTH.Position,ar.session,ar.Session_id,ar.Class_id,ar.admissionserialnumber as Admission_no,dm.Date,dm.month,dm.Discount_on,(Select Discunt_Type from Student_Discunt_Type where Student_Discunt_Type_id = dm.Student_Discunt_Type_id) as Discunt_Type,dm.Discount_on as content,CONVERT(varchar(100), tfm.parameter_id) as content_id,tfm.amount,dm.disc_amount, (cast(amount as float) - cast(disc_amount as float)) after_disc from Discount_Master_for_bus dm join admission_registor ar on dm.admission_no = ar.admissionserialnumber and dm.session = ar.session join Transportation_Fee_Master tfm on dm.session_id = tfm.session_id and dm.Month = tfm.Month and dm.TransportationPath_id = tfm.Transportation_path_id and dm.Boarding_Point_id = tfm.Boarding_Point_id  join Month_Index MTH on dm.month=MTH.Month where admission_no = '" + admission_no + "' and dm.session = '" + session_name + "' and dm.TransportationPath_id= '" + ViewState["TransportPath_id"].ToString() + "' and dm.Boarding_Point_id= '" + ViewState["Boarding_Point_id"].ToString() + "' and cast(dm.disc_amount as float) > 0 UNION all select '0' as Position,ar.session,ar.Session_id,ar.Class_id,ar.admissionserialnumber as Admission_no,dm.Date,dm.month,dm.Discount_on,(Select Discunt_Type from Student_Discunt_Type where Student_Discunt_Type_id=dm.Student_Discunt_Type_id) as Discunt_Type,fmc.content,dm.fee_head_id as content_id,fmc.amount,dm.disc_amount, (cast(amount as float)-cast(disc_amount as float)) after_disc from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id join Add_course_table ac on dm.Class_id=ac.course_id join admission_registor ar on dm.admission_no=ar.admissionserialnumber and dm.session=ar.session where dm.group_id='" + hd_fee_group.Value + "' and dm.session='" + session_name + "' and ar.admissionserialnumber='" + admission_no + "' and cast(dm.disc_amount as float) >0 order by Position asc";
            if (hostel_taken.ToUpper() == "YES")
            {
                qrydiscount = "select distinct MTH.Position,ar.session,ar.Session_id,ar.Class_id,ar.admissionserialnumber as Admission_no,dm.Date,dm.month,dm.Discount_on,(Select Discunt_Type from Student_Discunt_Type where Student_Discunt_Type_id=dm.Student_Discunt_Type_id) as Discunt_Type,fmc.content,dm.fee_head_id as content_id,fmc.amount,dm.disc_amount, (cast(amount as float)-cast(disc_amount as float)) after_disc from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.month=fmc.Month join Add_course_table ac on dm.Class_id=ac.course_id join admission_registor ar on dm.admission_no=ar.admissionserialnumber and dm.session=ar.session join Month_Index MTH on dm.month=MTH.Month where dm.group_id='3' and dm.session='" + session_name + "' and ar.admissionserialnumber='" + admission_no + "' and cast(dm.disc_amount as float) >0 and fmc.Hostel_id='" + ViewState["Hostel_id"].ToString() + "' and fmc.Room_Category_id='" + ViewState["Room_Category_id"].ToString() + "' UNION all select MTH.Position,ar.session,ar.Session_id,ar.Class_id,ar.admissionserialnumber as Admission_no,dm.Date,dm.month,dm.Discount_on,(Select Discunt_Type from Student_Discunt_Type where Student_Discunt_Type_id = dm.Student_Discunt_Type_id) as Discunt_Type,dm.Discount_on as content,CONVERT(varchar(100), tfm.parameter_id) as content_id,tfm.amount,dm.disc_amount, (cast(amount as float) - cast(disc_amount as float)) after_disc from Discount_Master_for_bus dm join admission_registor ar on dm.admission_no = ar.admissionserialnumber and dm.session = ar.session join Transportation_Fee_Master tfm on dm.session_id = tfm.session_id and dm.Month = tfm.Month and dm.TransportationPath_id = tfm.Transportation_path_id and dm.Boarding_Point_id = tfm.Boarding_Point_id  join Month_Index MTH on dm.month=MTH.Month where admission_no = '" + admission_no + "' and dm.session = '" + session_name + "' and dm.TransportationPath_id= '" + ViewState["TransportPath_id"].ToString() + "' and dm.Boarding_Point_id= '" + ViewState["Boarding_Point_id"].ToString() + "' and cast(dm.disc_amount as float) > 0 UNION all select '0' as Position,ar.session,ar.Session_id,ar.Class_id,ar.admissionserialnumber as Admission_no,dm.Date,dm.month,dm.Discount_on,(Select Discunt_Type from Student_Discunt_Type where Student_Discunt_Type_id=dm.Student_Discunt_Type_id) as Discunt_Type,fmc.content,dm.fee_head_id as content_id,fmc.amount,dm.disc_amount, (cast(amount as float)-cast(disc_amount as float)) after_disc from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id join Add_course_table ac on dm.Class_id=ac.course_id join admission_registor ar on dm.admission_no=ar.admissionserialnumber and dm.session=ar.session where dm.group_id='" + hd_fee_group.Value + "' and dm.session='" + session_name + "' and ar.admissionserialnumber='" + admission_no + "' and cast(dm.disc_amount as float) >0 order by Position asc";
            }
            DataTable dtdiscount = mycode.FillData(qrydiscount);
            if (dtdiscount.Rows.Count == 0)
            {
                discgridDV.Visible = false;
                rd_discount.DataSource = null;
                rd_discount.DataBind();
            }
            else
            {
                discgridDV.Visible = true;
                rd_discount.DataSource = dtdiscount;
                rd_discount.DataBind();
            }


            ViewState["condition"] = "";
            mycode.bind_all_ddl_with_id_no_select(ddl_discount_mode, "select Discunt_Type,Student_Discunt_Type_id from Student_Discunt_Type order by Discunt_Type");
            string startmonthname = My.get_start_month();
            string query1 = ""; string queryAdm = "";
            if (hostel_taken.ToUpper() == "YES")  // hostel month fee
            {
                query1 = "select distinct fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='3'  and fmc.Hostel_Id=" + hostel_id + " and fmc.Room_Category_id=" + room_category_id + ""; // and fmc.Month='" + startmonthname + "' 
            }
            else
            {
                if (transport_taken.ToUpper() == "YES")
                {
                    if (ViewState["parameteridS"].ToString() == "44")
                    {
                        query1 = "select distinct fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='44' UNION all select t1.Parameter as content,parameter_id as content_id,Amount as amount,'0' as group_id,'0' as discount from Transportation_Fee_Master t1 join Month_Index t2 on t1.Month_id=t2.Month_Id where t1.session_id='" + session_id + "' and t1.Transportation_path_id='" + TransportPath_id + "'and t1.Boarding_Point_id='" + Boarding_Point_id + "' and t1.Month='" + startmonthname + "'"; // and fmc.Month='" + startmonthname + "'
                    }
                    else
                    {
                        query1 = "select distinct fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='4' UNION all select t1.Parameter as content,parameter_id as content_id,Amount as amount,'0' as group_id,'0' as discount from Transportation_Fee_Master t1 join Month_Index t2 on t1.Month_id=t2.Month_Id where t1.session_id='" + session_id + "' and t1.Transportation_path_id='" + TransportPath_id + "'and t1.Boarding_Point_id='" + Boarding_Point_id + "' and t1.Month='" + startmonthname + "'"; // and fmc.Month='" + startmonthname + "'
                    }
                }
                else
                {
                    if (ViewState["parameteridS"].ToString() == "44")
                    {
                        query1 = "select distinct fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='44'"; //and fmc.Month='" + startmonthname + "'
                    }
                    else
                    {
                        query1 = "select distinct fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='4'"; //and fmc.Month='" + startmonthname + "'
                    }
                }
            }
            DataTable dt1 = mycode.FillData(query1);
            if (dt1.Rows.Count == 0)
            {
                rd_discount_fee_head.DataSource = null;
                rd_discount_fee_head.DataBind();
            }
            else
            {
                rd_discount_fee_head.DataSource = dt1;
                rd_discount_fee_head.DataBind();
            }

            //=====AdmissionAnnual
            string parameter_id = "";
            if (mycode.IsUserExist("select Id from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and (group_id='1' or group_id='2')"))
            {
                if (ViewState["parameter"].ToString() == "HostelMonthlyFee")
                {
                    if (hd_fee_group.Value == "1") //admission fee hostel
                    {
                        parameter_id = "5";
                        queryAdm = "select * from (select fmc.content,fmc.content_id,fmc.amount,cm.group_id,isnull((select top 1 disc_amount from Discount_Master where fee_head_id=fmc.content_id and session=fmc.session and Class_id=fmc.class_id and admission_no='" + admission_no + "' and parameter_id='" + parameter_id + "'),'0') discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='" + parameter_id + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t where convert(float, amount)>0";
                    }
                    if (hd_fee_group.Value == "2") //Annual fee hostel
                    {
                        parameter_id = "6";
                        queryAdm = "select * from (select fmc.content,fmc.content_id,fmc.amount,cm.group_id,isnull((select top 1 disc_amount from Discount_Master where fee_head_id=fmc.content_id and session=fmc.session and Class_id=fmc.class_id and admission_no='" + admission_no + "' and parameter_id='" + parameter_id + "'),'0') discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='" + parameter_id + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t where convert(float, amount)>0";
                    }
                }
                else
                {
                    if (hd_fee_group.Value == "1") //admission fee
                    {
                        parameter_id = "1";
                    }
                    if (hd_fee_group.Value == "2") //Annual fee
                    {
                        parameter_id = "2";
                    }
                    queryAdm = "select * from (select fmc.content,fmc.content_id,fmc.amount,cm.group_id,isnull((select top 1 disc_amount from Discount_Master where fee_head_id=fmc.content_id and session=fmc.session and Class_id=fmc.class_id and admission_no='" + admission_no + "' and parameter_id='" + parameter_id + "'),'0') discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where fmc.class_id='" + class_id + "' and fmc.session_id='" + session_id + "' and fmc.parameter_id='" + parameter_id + "') t where convert(float, amount)>0";
                }
            }
            else
            {
                queryAdm = "select feetype as content,content_id,(convert(float, payable)-convert(float, paid)) as amount,group_id,Disc as discount from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + session_name + "' and (group_id='1' or group_id='2') and status='Dues'";
            }

            DataTable dtAdm = mycode.FillData(queryAdm);
            if (dtAdm.Rows.Count == 0)
            {
                rp_fee_head_admission.DataSource = null;
                rp_fee_head_admission.DataBind();
            }
            else
            {
                rp_fee_head_admission.DataSource = dtAdm;
                rp_fee_head_admission.DataBind();
            }
        }

        protected void btn_save_discount_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add_Discount"].ToString() == "1")
                {
                    ViewState["issubmit"] = "0";
                    if (chk_is_admission_disc.Checked == true)
                    {
                        if (txt_apply_discount_status.Text == "0") // FOR APPLY COUPON
                        {
                            if (ViewState["isApplyDiscByCoupon"].ToString() == "1") // FOR APPLY COUPON
                            {
                                save_temp_adm_disc();
                            }
                            else
                            {
                                save_adm_discount_data();
                            }
                        }
                        else
                        {
                            save_adm_discount_data();
                        }
                    }
                    else
                    {
                        if (txt_apply_discount_status.Text == "0") // FOR APPLY COUPON
                        {
                            if (ViewState["isApplyDiscByCoupon"].ToString() == "1") // FOR APPLY COUPON
                            {
                                save_temp_monthly_disc();
                            }
                            else
                            {
                                save_data();
                            }
                        }
                        else
                        {
                            save_data();
                        }
                    }
                }
                else
                {
                    Alertme("You are not authorized to do this work.", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                }
            }
            catch (Exception ex)
            {
            }
        }


        #region ADMISSION DISCOUNT
        private void save_adm_discount_data()
        {
            bool isamountfill = false;
            for (int i = 0; i < rp_fee_head_admission.Items.Count; i++)
            {
                TextBox txt_disc_fee = (TextBox)rp_fee_head_admission.Items[i].FindControl("txt_adm_disc_fee");
                if (My.toDouble(txt_disc_fee.Text) > 0)
                {
                    isamountfill = true;
                }
            }
            if (isamountfill == true)
            {
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    string couponId = payments.auto_serialS("Student_discount_entry_id", con);
                    double totla = 0;
                    for (int i = 0; i < rp_fee_head_admission.Items.Count; i++)
                    {
                        TextBox txt_fee = (TextBox)rp_fee_head_admission.Items[i].FindControl("txt_adm_head_fee");
                        TextBox txt_disc_fee = (TextBox)rp_fee_head_admission.Items[i].FindControl("txt_adm_disc_fee");
                        Label lbl_content_id = (Label)rp_fee_head_admission.Items[i].FindControl("lbl_adm_content_id");
                        Label lbl_content = (Label)rp_fee_head_admission.Items[i].FindControl("lbl_adm_content");
                        totla = totla + My.toDouble(txt_fee.Text);

                        //==============*************** 
                        if (My.toDouble(txt_disc_fee.Text) > 0)
                        {
                            string discount_fee = txt_disc_fee.Text;
                            if (My.toDouble(txt_disc_fee.Text) > My.toDouble(txt_fee.Text))
                            {
                                discount_fee = txt_fee.Text;
                            }
                            string query = ""; string discount_on = ""; string parameter_id = ""; string disc_for = ""; string hostel_id = ""; string room_cat_id = "";
                            if (ViewState["parameter"].ToString() == "HostelMonthlyFee")
                            {
                                hostel_id = ViewState["Hostel_id"].ToString();
                                room_cat_id = ViewState["Room_Category_id"].ToString();
                                if (hd_fee_group.Value == "1") //admission fee hostel
                                {
                                    disc_for = "Hosteler";
                                    parameter_id = "5";
                                    discount_on = "Admission";
                                    query = "select * from Discount_Master where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='" + hd_fee_group.Value + "' and month='NA' and admission_no='" + hd_admission_no.Value + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "' and Hostel_id=" + ViewState["Hostel_id"].ToString() + "";
                                }
                                if (hd_fee_group.Value == "2") //Annual fee hostel
                                {
                                    disc_for = "Hosteler";
                                    parameter_id = "6";
                                    discount_on = "Annual";
                                    query = "select * from Discount_Master where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='" + hd_fee_group.Value + "' and month='NA' and admission_no='" + hd_admission_no.Value + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "' and Hostel_id=" + ViewState["Hostel_id"].ToString() + "";
                                }
                            }
                            else
                            {
                                if (hd_fee_group.Value == "1") //admission fee
                                {
                                    disc_for = "Days";
                                    parameter_id = "1";
                                    discount_on = "Admission";
                                    query = "select * from Discount_Master where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='" + hd_fee_group.Value + "' and month='NA' and admission_no='" + hd_admission_no.Value + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "'";
                                }
                                if (hd_fee_group.Value == "2") //Annual fee
                                {
                                    disc_for = "Days";
                                    parameter_id = "2";
                                    discount_on = "Annual";
                                    query = "select * from Discount_Master where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='" + hd_fee_group.Value + "' and month='NA' and admission_no='" + hd_admission_no.Value + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "'";
                                }
                            }


                            ///check
                            bool isDiscwillapply = true; double discount_given = 0;
                            if (My.toDouble(txt_disc_fee.Text) > 0)
                            {
                                DataTable dtcd = payments.dataTable("select * from Discount_master_report where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + hd_admission_no.Value + "' and Content_id='" + lbl_content_id.Text + "' and Discount_on='" + discount_on + "'", con);
                                if (dtcd.Rows.Count > 0)
                                {
                                    isDiscwillapply = false;
                                    foreach (DataRow dr in dtcd.Rows)
                                    {
                                        discount_given = discount_given + My.toDouble(dr["Discount_amt"].ToString());
                                    }
                                    if (My.toDouble(txt_disc_fee.Text) > discount_given)
                                    {
                                        isDiscwillapply = true;
                                    }

                                    //===========================
                                    if (isDiscwillapply == false)
                                    {
                                        double net_amt = (My.toDouble(txt_fee.Text) - discount_given);
                                        double disc_perc = (My.toDouble(discount_given) / My.toDouble(txt_fee.Text) * 100);
                                        SqlCommand cmd;
                                        string querychk = "INSERT INTO Discount_master_check_dup_temp (Session_id,Class_id,Admission_no,Discount_on,Content_id,Month,Amount,Discount_amt,Discount_percnt,Net_amt,Bill_no,Created_by,Created_date,Created_time,Created_idate,Month_position,Content) values (@Session_id,@Class_id,@Admission_no,@Discount_on,@Content_id,@Month,@Amount,@Discount_amt,@Discount_percnt,@Net_amt,@Bill_no,@Created_by,@Created_date,@Created_time,@Created_idate,@Month_position,@Content)";
                                        cmd = new SqlCommand(querychk);
                                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                                        cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                                        cmd.Parameters.AddWithValue("@Admission_no", hd_admission_no.Value);
                                        cmd.Parameters.AddWithValue("@Discount_on", discount_on);
                                        cmd.Parameters.AddWithValue("@Content_id", lbl_content_id.Text);
                                        cmd.Parameters.AddWithValue("@Month", dtcd.Rows[0]["Month"].ToString());
                                        cmd.Parameters.AddWithValue("@Amount", txt_fee.Text);
                                        cmd.Parameters.AddWithValue("@Discount_amt", discount_given);
                                        cmd.Parameters.AddWithValue("@Discount_percnt", disc_perc);
                                        cmd.Parameters.AddWithValue("@Net_amt", net_amt);
                                        cmd.Parameters.AddWithValue("@Bill_no", "0");
                                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                                        cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                                        cmd.Parameters.AddWithValue("@Month_position", dtcd.Rows[0]["Month_position"].ToString());
                                        cmd.Parameters.AddWithValue("@Content", lbl_content.Text);
                                        if (payments.InsertUpdateData(cmd, con))
                                        {
                                        }
                                    }
                                }
                            }
                            ///check

                            if (isDiscwillapply == true)
                            {
                                SqlDataAdapter ad = new SqlDataAdapter(query, con);
                                DataSet ds = new DataSet();
                                ad.Fill(ds, "Discount_Master");
                                DataTable dt = ds.Tables[0];
                                if (dt.Rows.Count == 0)
                                {
                                    SqlCommand cmd;
                                    string queryD = "INSERT INTO Discount_Master (Class_id,Discount_on,session,Discount_per,group_id,admission_no,month,fee_head_id,disc_amount,parameter_id,session_id,Branch_id,User_id,Date,time,discount_for,category_id,sub_category_id,Upload_from,Hostel_id,Room_Category_id,Student_Discunt_Type_id,Student_Discunt_Remarks,Student_discount_type_entry_id) values (@Class_id,@Discount_on,@session,@Discount_per,@group_id,@admission_no,@month,@fee_head_id,@disc_amount,@parameter_id,@session_id,@Branch_id,@User_id,@Date,@time,@discount_for,@category_id,@sub_category_id,@Upload_from,@Hostel_id,@Room_Category_id,@Student_Discunt_Type_id,@Student_Discunt_Remarks,@Student_discount_type_entry_id)";
                                    cmd = new SqlCommand(queryD);
                                    cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                                    cmd.Parameters.AddWithValue("@Discount_on", discount_on);
                                    cmd.Parameters.AddWithValue("@session", ViewState["session"].ToString());
                                    cmd.Parameters.AddWithValue("@Discount_per", My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2));
                                    cmd.Parameters.AddWithValue("@group_id", hd_fee_group.Value);
                                    cmd.Parameters.AddWithValue("@admission_no", hd_admission_no.Value);
                                    cmd.Parameters.AddWithValue("@month", "NA");
                                    cmd.Parameters.AddWithValue("@fee_head_id", lbl_content_id.Text);
                                    cmd.Parameters.AddWithValue("@disc_amount", My.toDouble(discount_fee).ToString("0.00"));
                                    cmd.Parameters.AddWithValue("@parameter_id", parameter_id);
                                    cmd.Parameters.AddWithValue("@session_id", ViewState["sessionIDs"].ToString());
                                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                                    cmd.Parameters.AddWithValue("@time", mycode.time());
                                    cmd.Parameters.AddWithValue("@discount_for", disc_for);
                                    cmd.Parameters.AddWithValue("@category_id", ViewState["category_id"].ToString());
                                    cmd.Parameters.AddWithValue("@sub_category_id", ViewState["sub_category_id"].ToString());
                                    cmd.Parameters.AddWithValue("@Upload_from", "");
                                    cmd.Parameters.AddWithValue("@Hostel_id", hostel_id);
                                    cmd.Parameters.AddWithValue("@Room_Category_id", room_cat_id);
                                    cmd.Parameters.AddWithValue("@Student_Discunt_Type_id", ddl_discount_mode.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Student_Discunt_Remarks", txt_discount_Remarks.Text);
                                    cmd.Parameters.AddWithValue("@Student_discount_type_entry_id", couponId);
                                    if (payments.InsertUpdateData(cmd, con))
                                    {
                                        DataTable dtF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and (group_id='1' or group_id='2') and content_id='" + lbl_content_id.Text + "'", con);
                                        if (dtF.Rows.Count > 0)
                                        {
                                            double payableAfterDisc = My.toDouble(dtF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                            payments.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtF.Rows[0]["Id"].ToString() + "", con);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                        dr["fee_head_id"] = lbl_content_id.Text;
                                        dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                        dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                        dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                    }
                                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                    ad.Update(dt);

                                    DataTable dtF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and (group_id='1' or group_id='2') and content_id='" + lbl_content_id.Text + "'", con);
                                    if (dtF.Rows.Count > 0)
                                    {
                                        double payableAfterDisc = My.toDouble(dtF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                        payments.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtF.Rows[0]["Id"].ToString() + "", con);
                                    }
                                }
                            }
                        }
                    }

                    dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                    ViewState["issubmit"] = "1";
                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                if (flag == true)
                {
                    if (ViewState["issubmit"].ToString() == "1")
                    {
                        Session["IsDiscountOpen"] = "0";
                        Session["msg"] = "Discount Applied successfully for " + txt_student_name.Text;
                        Response.Redirect("fees-collection-1.aspx?adm=" + txt_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                    }
                    else
                    {
                        Session["IsDiscountOpen"] = "0";
                        Response.Redirect("fees-collection-1.aspx?adm=" + txt_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                Alertme("Please enter the amount for which you would like to provide a discount.", "warning");
            }
        }



        #endregion



        private void save_data()
        {
            My.exeSql("delete from Discount_master_check_dup_temp where Admission_no='" + hd_admission_no.Value + "'");
            bool isMonthChecked = false;
            for (int iMC = 0; iMC < rp_month_for_discount.Items.Count; iMC++)
            {
                CheckBox chkM = (CheckBox)rp_month_for_discount.Items[iMC].FindControl("chk_month_discount");
                if (chkM.Checked == true)
                {
                    isMonthChecked = true;
                }
            }

            if (isMonthChecked == true)
            {
                string query = " ";
                bool isamountfill = false;
                for (int i = 0; i < rd_discount_fee_head.Items.Count; i++)
                {
                    TextBox txt_disc_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_disc_fee");
                    if (My.toDouble(txt_disc_fee.Text) > 0)
                    {
                        isamountfill = true;
                    }
                }


                if (isamountfill == true)
                {
                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        string couponId = payments.auto_serialS("Student_discount_entry_id", con);
                        for (int ixi = 0; ixi < rp_month_for_discount.Items.Count; ixi++)
                        {
                            CheckBox chkM = (CheckBox)rp_month_for_discount.Items[ixi].FindControl("chk_month_discount");
                            if (chkM.Checked == true)
                            {
                                Label lbl_month_id = (Label)rp_month_for_discount.Items[ixi].FindControl("lbl_month_id");
                                Label lbl_month_name = (Label)rp_month_for_discount.Items[ixi].FindControl("lbl_Month_discount");

                                #region MonthLY 
                                string Month = lbl_month_name.Text;
                                double totla = 0;
                                for (int i = 0; i < rd_discount_fee_head.Items.Count; i++)
                                {
                                    TextBox txt_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_head_fee");
                                    TextBox txt_disc_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_disc_fee");
                                    Label lbl_content_id = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content_id");
                                    Label lbl_content = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content");
                                    totla = totla + My.toDouble(txt_fee.Text);
                                    //==============*************** 


                                    ///check
                                    bool isDiscwillapply = true; double discount_given = 0;
                                    if (My.toDouble(txt_disc_fee.Text) > 0)
                                    {
                                        DataTable dtcd = payments.dataTable("select * from Discount_master_report where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + hd_admission_no.Value + "' and Content_id='" + lbl_content_id.Text + "' and Month='" + Month + "'", con);
                                        if (dtcd.Rows.Count > 0)
                                        {
                                            isDiscwillapply = false;
                                            foreach (DataRow dr in dtcd.Rows)
                                            {
                                                discount_given = discount_given + My.toDouble(dr["Discount_amt"].ToString());
                                            }
                                            if (My.toDouble(txt_disc_fee.Text) > discount_given)
                                            {
                                                isDiscwillapply = true;
                                            }

                                            //===========================
                                            if (isDiscwillapply == false)
                                            {
                                                double net_amt = (My.toDouble(txt_fee.Text) - discount_given);
                                                double disc_perc = (My.toDouble(discount_given) / My.toDouble(txt_fee.Text) * 100);
                                                SqlCommand cmd;
                                                string querychk = "INSERT INTO Discount_master_check_dup_temp (Session_id,Class_id,Admission_no,Discount_on,Content_id,Month,Amount,Discount_amt,Discount_percnt,Net_amt,Bill_no,Created_by,Created_date,Created_time,Created_idate,Month_position,Content) values (@Session_id,@Class_id,@Admission_no,@Discount_on,@Content_id,@Month,@Amount,@Discount_amt,@Discount_percnt,@Net_amt,@Bill_no,@Created_by,@Created_date,@Created_time,@Created_idate,@Month_position,@Content)";
                                                cmd = new SqlCommand(querychk);
                                                cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                                                cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                                                cmd.Parameters.AddWithValue("@Admission_no", hd_admission_no.Value);
                                                cmd.Parameters.AddWithValue("@Discount_on", dtcd.Rows[0]["Discount_on"].ToString());
                                                cmd.Parameters.AddWithValue("@Content_id", lbl_content_id.Text);
                                                cmd.Parameters.AddWithValue("@Month", dtcd.Rows[0]["Month"].ToString());
                                                cmd.Parameters.AddWithValue("@Amount", txt_fee.Text);
                                                cmd.Parameters.AddWithValue("@Discount_amt", discount_given);
                                                cmd.Parameters.AddWithValue("@Discount_percnt", disc_perc);
                                                cmd.Parameters.AddWithValue("@Net_amt", net_amt);
                                                cmd.Parameters.AddWithValue("@Bill_no", "0");
                                                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                                                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                                                cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                                                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                                                cmd.Parameters.AddWithValue("@Month_position", dtcd.Rows[0]["Month_position"].ToString());
                                                cmd.Parameters.AddWithValue("@Content", lbl_content.Text);
                                                if (payments.InsertUpdateData(cmd, con))
                                                {
                                                }
                                            }
                                        }
                                    }
                                    ///check



                                    if (isDiscwillapply == true)
                                    {
                                        if (lbl_content_id.Text == "1002") // FOR TRANSPORT
                                        {
                                            if (My.toDouble(txt_disc_fee.Text) > 0)
                                            {
                                                save_bus_discount(couponId, con);
                                            }
                                        }
                                        else
                                        {
                                            if (My.toDouble(txt_disc_fee.Text) > 0)
                                            {
                                                string qryChk = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + ViewState["class_id"].ToString() + "' and fmc.session_id='" + ViewState["sessionIDs"].ToString() + "' and fmc.parameter_id='" + ViewState["parameterDisc"].ToString() + "' and fmc.Month='" + Month + "' and fmc.content_id='" + lbl_content_id.Text + "'";
                                                string hostel_taken = ViewState["hostel_takens"].ToString();
                                                if (hostel_taken.ToUpper() == "YES")
                                                {
                                                    qryChk = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + ViewState["class_id"].ToString() + "' and fmc.session_id='" + ViewState["sessionIDs"].ToString() + "' and fmc.parameter_id='3' and fmc.Month='" + Month + "' and fmc.content_id='" + lbl_content_id.Text + "' and fmc.Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and fmc.Room_Category_id=" + ViewState["Room_Category_id"].ToString() + "";
                                                }

                                                DataTable dtFC = payments.dataTable(qryChk, con);
                                                if (dtFC.Rows.Count > 0)
                                                {
                                                    string discount_fee = txt_disc_fee.Text;
                                                    if (My.toDouble(txt_disc_fee.Text) > My.toDouble(txt_fee.Text))
                                                    {
                                                        discount_fee = txt_fee.Text;
                                                    }

                                                    DataTable dtF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Paid'", con);
                                                    if (dtF.Rows.Count == 0)
                                                    {
                                                        //CHECK IN TYPEWISE
                                                        DataTable dtT = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Dues'", con);
                                                        if (dtT.Rows.Count > 0)
                                                        {
                                                            if (My.toDouble(dtT.Rows[0]["dues"].ToString()) > 0)
                                                            {
                                                                double duesamts = (My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString()));
                                                                if (My.toDouble(discount_fee) > duesamts)
                                                                {
                                                                    discount_fee = duesamts.ToString();
                                                                }
                                                            }
                                                        }

                                                        //CHECK IN TYPEWISE 
                                                        if (ViewState["parameterDisc"].ToString() == "3") // hostel
                                                        {
                                                            query = "select * from Discount_Master where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='3' and month='" + Month + "' and admission_no='" + txt_admission_no.Text + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + ViewState["parameterDisc"].ToString() + "' and Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Room_Category_id=" + ViewState["Room_Category_id"].ToString() + "";
                                                        }
                                                        else
                                                        {
                                                            query = "select * from Discount_Master where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='3' and month='" + Month + "' and admission_no='" + txt_admission_no.Text + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + ViewState["parameterDisc"].ToString() + "'";
                                                        }

                                                        SqlDataAdapter ad = new SqlDataAdapter(query, con);
                                                        DataSet ds = new DataSet();
                                                        ad.Fill(ds, "Discount_Master");
                                                        DataTable dt = ds.Tables[0];
                                                        if (dt.Rows.Count == 0)
                                                        {
                                                            DataRow dr = dt.NewRow();
                                                            dr["Class_id"] = ViewState["class_id"].ToString();
                                                            dr["Discount_on"] = "Monthly";
                                                            dr["session"] = ViewState["session"].ToString();
                                                            dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                                            dr["group_id"] = "3";
                                                            dr["admission_no"] = txt_admission_no.Text;
                                                            dr["month"] = Month;
                                                            dr["fee_head_id"] = lbl_content_id.Text;
                                                            dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                                            dr["parameter_id"] = ViewState["parameterDisc"].ToString();
                                                            dr["category_id"] = ViewState["category_id"].ToString();
                                                            dr["sub_category_id"] = ViewState["sub_category_id"].ToString();
                                                            dr["session_id"] = ViewState["sessionIDs"].ToString();
                                                            dr["Branch_id"] = ViewState["Branchid"].ToString();
                                                            dr["User_id"] = ViewState["Userid"].ToString();
                                                            dr["Date"] = mycode.date();
                                                            dr["time"] = mycode.time();
                                                            dr["Hostel_id"] = ViewState["Hostel_id"].ToString();
                                                            dr["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                                                            dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                                            dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                                            dr["Student_discount_type_entry_id"] = couponId;
                                                            dt.Rows.Add(dr);
                                                            DataTable dtFF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "'", con);
                                                            if (dtFF.Rows.Count > 0)
                                                            {
                                                                double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                                                payments.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "", con);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            foreach (DataRow dr in dt.Rows)
                                                            {
                                                                dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                                                dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                                                dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                                                dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                                            }

                                                            DataTable dtFF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "'", con);
                                                            if (dtFF.Rows.Count > 0)
                                                            {
                                                                double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                                                payments.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "", con);
                                                            }
                                                        }
                                                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                                        ad.Update(dt);
                                                        ViewState["issubmit"] = "1";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                        flag = true;
                        con.Close();
                        scope.Complete();
                    }

                    if (flag == true)
                    {
                        if (ViewState["issubmit"].ToString() == "1")
                        {
                            Session["IsDiscountOpen"] = "0";
                            Session["msg"] = "Discount Applied successfully for " + txt_student_name.Text;
                            Response.Redirect("fees-collection-1.aspx?adm=" + txt_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                        }
                        else
                        {
                            Session["IsDiscountOpen"] = "1";
                            Session["msg"] = "Discount not applied. Please choose the month associated with the fee.";
                            Response.Redirect("fees-collection-1.aspx?adm=" + txt_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                    Alertme("Please enter the amount for which you would like to provide a discount.", "warning");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                Alertme("Please select the month during which you want to apply the discount.", "warning");
                return;
            }
        }



        private void save_bus_discount(string couponId, SqlConnection con)
        {
            int mgrowcount = rp_month_for_discount.Items.Count;
            for (int ixi = 0; ixi < mgrowcount; ixi++)
            {
                CheckBox chkM = (CheckBox)rp_month_for_discount.Items[ixi].FindControl("chk_month_discount");
                if (chkM.Checked == true)
                {
                    Label lbl_month_id = (Label)rp_month_for_discount.Items[ixi].FindControl("lbl_month_id");
                    Label lbl_month_name = (Label)rp_month_for_discount.Items[ixi].FindControl("lbl_Month_discount");
                    #region #fff
                    double totla = 0;
                    for (int i = 0; i < rd_discount_fee_head.Items.Count; i++)
                    {
                        TextBox txt_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_head_fee");
                        TextBox txt_disc_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_disc_fee");
                        Label lbl_content_id = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content_id");
                        Label lbl_content = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content");
                        totla = totla + My.toDouble(txt_fee.Text);

                        string Month = lbl_month_name.Text;
                        //==============*************** 
                        if (lbl_content_id.Text == "1002") // FOR TRANSPORT
                        {
                            if (My.toDouble(txt_disc_fee.Text) > 0)
                            {
                                string discount_fee = txt_disc_fee.Text;
                                if (My.toDouble(txt_disc_fee.Text) > My.toDouble(txt_fee.Text))
                                {
                                    discount_fee = txt_fee.Text;
                                }

                                DataTable dtF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Paid'", con);
                                if (dtF.Rows.Count == 0)
                                {
                                    //CHECK IN TYPEWISE
                                    DataTable dtT = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Dues'", con);
                                    if (dtT.Rows.Count > 0)
                                    {
                                        if (My.toDouble(dtT.Rows[0]["dues"].ToString()) > 0)
                                        {
                                            double duesamts = (My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString()));
                                            if (My.toDouble(discount_fee) > duesamts)
                                            {
                                                discount_fee = duesamts.ToString();
                                            }
                                        }
                                    }

                                    //CHECK IN TYPEWISE 
                                    ViewState["discount_on"] = "TransportFee";
                                    SqlDataAdapter ad = new SqlDataAdapter("select * from Discount_Master_for_bus where Bus_path=" + ViewState["TransportPath_id"].ToString() + " and session_id='" + ViewState["sessionIDs"].ToString() + "' and month='" + lbl_month_name.Text + "' and admission_no='" + txt_admission_no.Text + "' and Class_id='" + ViewState["class_id"].ToString() + "' and Boarding_Point_id='" + ViewState["Boarding_Point_id"].ToString() + "'", con);
                                    DataSet ds = new DataSet();
                                    ad.Fill(ds, "Discount_Master_for_bus");
                                    DataTable dt = ds.Tables[0];
                                    if (dt.Rows.Count == 0)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["discount_for"] = "TransportFee";
                                        dr["Class_id"] = ViewState["class_id"].ToString();
                                        dr["Discount_on"] = ViewState["discount_on"].ToString();
                                        dr["session"] = ViewState["session"].ToString();
                                        dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                        dr["group_id"] = "51";
                                        dr["admission_no"] = txt_admission_no.Text;
                                        dr["month"] = lbl_month_name.Text;
                                        dr["fee_head_id"] = lbl_content_id.Text;
                                        dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                        dr["parameter_id"] = "0";
                                        dr["category_id"] = "0";
                                        dr["sub_category_id"] = "0";
                                        dr["Bus_path"] = ViewState["TransportPath_id"].ToString();
                                        dr["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                                        dr["session_id"] = ViewState["sessionIDs"].ToString();
                                        dr["Branch_id"] = ViewState["Branchid"].ToString();
                                        dr["User_id"] = ViewState["Userid"].ToString();
                                        dr["Date"] = mycode.date();
                                        dr["time"] = mycode.time();
                                        dr["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                                        dr["Transportation_Id"] = ViewState["Transport_id"].ToString();
                                        dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                        dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                        dr["Student_discount_type_entry_id"] = couponId;
                                        dt.Rows.Add(dr);
                                        DataTable dtFF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "'", con);
                                        if (dtFF.Rows.Count > 0)
                                        {
                                            double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                            payments.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "", con);
                                        }
                                        ViewState["issubmit"] = "1";
                                    }
                                    else
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                            dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                            dr["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                                            dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                            dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                        }
                                        DataTable dtFF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "'", con);
                                        if (dtFF.Rows.Count > 0)
                                        {
                                            double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                            payments.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "", con);
                                        }
                                        ViewState["issubmit"] = "1";
                                    }
                                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                    ad.Update(dt);
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete_Discount"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    //Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");
                    Label lbl_discount_on = (Label)row.FindControl("lbl_discount_on");
                    Label lbl_month = (Label)row.FindControl("lbl_month");
                    Label lbl_session = (Label)row.FindControl("lbl_session");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_content_id = (Label)row.FindControl("lbl_content_id");
                    if (lbl_discount_on.Text == "Monthly" || lbl_discount_on.Text == "TransportFee")
                    {
                        DataTable dt = My.dataTable("select * from Typewise_fee_collection where admission_no='" + lbl_admission_no.Text + "' and (parameter='MonthlyFee'  or parameter='HostelMonthlyFee') and session='" + lbl_session.Text + "' and class_id='" + lbl_class_id.Text + "' and month='" + lbl_month.Text + "' and content_id='" + lbl_content_id.Text + "' and status='Paid'");
                        if (dt.Rows.Count == 0)
                        {
                            bool flag = false;
                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                            {
                                SqlConnection con = new SqlConnection(My.conn);
                                con.Open();
                                payments.exeSql("delete from Discount_Master where admission_no='" + lbl_admission_no.Text + "' and session_id='" + lbl_session_id.Text + "' and Discount_on='Monthly' and month='" + lbl_month.Text + "' and fee_head_id='" + lbl_content_id.Text + "'; delete from Discount_Master_for_bus where admission_no='" + lbl_admission_no.Text + "' and Class_id='" + lbl_class_id.Text + "'  and session_id='" + lbl_session_id.Text + "' and Discount_on='TransportFee' and month='" + lbl_month.Text + "' and fee_head_id='" + lbl_content_id.Text + "'", con);
                                //CheckinTypewise
                                DataTable dtT = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + lbl_admission_no.Text + "' and (parameter='MonthlyFee'  or parameter='HostelMonthlyFee') and session='" + lbl_session.Text + "' and class_id='" + lbl_class_id.Text + "' and month='" + lbl_month.Text + "' and content_id='" + lbl_content_id.Text + "'", con);
                                if (dtT.Rows.Count > 0)
                                {
                                    double duesAmt = My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString());
                                    payments.exeSql("update Typewise_fee_collection set Disc='0.00',Payable_after_disc=payable,dues='" + duesAmt.ToString("0.00") + "' where Id=" + dtT.Rows[0]["Id"].ToString() + "", con);
                                }

                                dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                                flag = true;
                                con.Close();
                                scope.Complete();
                            }
                            if (flag == true)
                            {
                                Session["msg"] = "Deletion process has been successfully done.";
                                Session["IsDiscountOpen"] = "1";
                                Response.Redirect("fees-collection-1.aspx?adm=" + lbl_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                            Alertme("You can't delete because fee has been taken for this head.", "warning");
                            return;
                        }
                    }
                    else if (lbl_discount_on.Text == "Annual" || lbl_discount_on.Text == "Admission")
                    {
                        DataTable dt = My.dataTable("select * from Typewise_fee_collection where admission_no='" + lbl_admission_no.Text + "' and session='" + lbl_session.Text + "' and class_id='" + lbl_class_id.Text + "' and group_id='" + hd_fee_group.Value + "' and content_id='" + lbl_content_id.Text + "' and status='Paid'");
                        if (dt.Rows.Count == 0)
                        {
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();
                            bool flag = false;
                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                            {
                                payments.exeSql("delete from Discount_Master where admission_no='" + lbl_admission_no.Text + "' and session_id='" + lbl_session_id.Text + "' and Discount_on='" + lbl_discount_on.Text + "' and fee_head_id='" + lbl_content_id.Text + "'", con);
                                //CheckinTypewise
                                DataTable dtT = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + lbl_admission_no.Text + "' and session='" + lbl_session.Text + "' and class_id='" + lbl_class_id.Text + "' and group_id='" + hd_fee_group.Value + "' and content_id='" + lbl_content_id.Text + "'", con);
                                if (dtT.Rows.Count > 0)
                                {
                                    double duesAmt = My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString());
                                    payments.exeSql("update Typewise_fee_collection set Disc='0.00',Payable_after_disc=payable,dues='" + duesAmt.ToString("0.00") + "' where Id=" + dtT.Rows[0]["Id"].ToString() + "", con);
                                }

                                dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                                flag = true;
                                con.Close();
                                scope.Complete();
                            }
                            if (flag == true)
                            {
                                Session["msg"] = "Deletion process has been successfully done.";
                                Session["IsDiscountOpen"] = "1";
                                Response.Redirect("fees-collection-1.aspx?adm=" + lbl_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                            Alertme("You can't delete because fee has been taken for this head.", "warning");
                            return;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                    Alertme("You are not authorized to do this work.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion


        #region EXTRA FEES
        //=============================EXTRA FEE HEAD
        private void for_extra_fee_head(string session_name, string session_id, string class_id, string admission_no, string hostel_taken, string transport_taken, string hostel_id, string room_category_id, string TransportPath_id, string Boarding_Point_id)
        {
            //ExtraHeadBtn.Visible = true;
            ViewState["LedgerEffected"] = "School";
            if (hostel_taken.ToUpper() == "YES") { ViewState["LedgerEffected"] = "Hostel"; }
            string qryExtHeadMonth = "select Month, Position,Month_Id from Month_Index order by Position asc";
            if (ViewState["Is_quarterwise_payment"].ToString() == "1")
            {
                qryExtHeadMonth = "select Month, Position,Month_Id from (select Month, Position,Month_Id,(select isnull(sum(convert(float, amount)),0) from Fee_master_content_wise where (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session_id='" + ViewState["sessionIDs"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and Month=Month_Index.Month) as Total_fee from Month_Index where Month in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Dues') or Month not in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Paid')) t where convert(float, Total_fee)>0 order by Position asc";
            }
            DataTable dtEHM = My.dataTable(qryExtHeadMonth);
            if (dtEHM.Rows.Count > 0)
            {
                rp_extra_head_month.DataSource = dtEHM;
                rp_extra_head_month.DataBind();
            }

            //mycode.bind_all_ddl_with_id(ddl_month, qryExtHeadMonth);
            //try
            //{
            //    ddl_month.SelectedValue = mycode.get_current_month_id_position();
            //}
            //catch
            //{
            //}
            string studentTyle = "AnnualFee";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                studentTyle = "AdmissionFee";
            }
            string qrydiscount = "select * from Misc_Fee_Master_Studentwise where Admission_No='" + admission_no + "' and Session_id='" + session_id + "' and (Type_Mode='MonthlyFee' or Type_Mode='" + studentTyle + "') order by Id  asc";
            DataTable dt = mycode.FillData(qrydiscount);
            if (dt.Rows.Count == 0)
            {
                extrafeeGrid.Visible = false;
                Repeater3.DataSource = null;
                Repeater3.DataBind();
            }
            else
            {
                extrafeeGrid.Visible = true;
                Repeater3.DataSource = dt;
                Repeater3.DataBind();
            }
        }

        double ttl_extra_fee = 0;
        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string fee_for = ((Label)e.Item.FindControl("lbl_fee_for")).Text;
                string lbl_month = ((Label)e.Item.FindControl("lbl_month")).Text;
                LinkButton lnkDelExtFee = ((LinkButton)e.Item.FindControl("lnkDelExtFee")) as LinkButton;
                string lbl_amount = ((Label)e.Item.FindControl("lbl_amount")).Text;
                lnkDelExtFee.Visible = false;
                ttl_extra_fee = ttl_extra_fee + My.toDouble(lbl_amount);

                if (fee_for == "MonthlyFee")
                {
                    DataTable dt = My.dataTable("select Id from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and month='" + lbl_month + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee')");
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtc = My.dataTable("select Id from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and month='" + lbl_month + "' and status='Dues' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee')");
                        if (dtc.Rows.Count > 0)
                        {
                            lnkDelExtFee.Visible = true;
                        }
                        else
                        {
                            lnkDelExtFee.Visible = false;
                        }
                    }
                    else
                    {
                        lnkDelExtFee.Visible = true;
                    }
                }
                else
                {
                    DataTable dt = My.dataTable("select Id from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and (parameter='AdmissionFee' or parameter='HostelAdmissionFee' or parameter='AnnualFee' or parameter='HostelAnnualFee')");
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtc = My.dataTable("select Id from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and status='Dues' and (parameter='AdmissionFee' or parameter='HostelAdmissionFee' or parameter='AnnualFee' or parameter='HostelAnnualFee')");
                        if (dtc.Rows.Count > 0)
                        {
                            lnkDelExtFee.Visible = true;
                        }
                        else
                        {
                            lnkDelExtFee.Visible = false;
                        }
                    }
                    else
                    {
                        lnkDelExtFee.Visible = true;
                    }
                }
            }

            lbl_ttl_extra_fee.Text = ttl_extra_fee.ToString();
        }




        protected void btn_save_extra_head_Click(object sender, EventArgs e)
        {
            try
            {
                Session["IsExtraFeeOpen"] = "0";
                if (txt_fee_title.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openExtraFeeAlert();", true);
                    Alertme("Please enter fee title.", "warning");
                    txt_fee_title.Focus();
                    return;
                }
                if (My.toDouble(txt_ext_fee_amt.Text) <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openExtraFeeAlert();", true);
                    Alertme("Please enter amount.", "warning");
                    txt_ext_fee_amt.Focus();
                    return;
                }

                ViewState["issubmit"] = "0";
                if (ddl_extra_head_for.SelectedValue == "1")
                {
                    bool isMonthChecked = false;
                    for (int iMC = 0; iMC < rp_extra_head_month.Items.Count; iMC++)
                    {
                        CheckBox chkM = (CheckBox)rp_extra_head_month.Items[iMC].FindControl("chk_month_eH");
                        if (chkM.Checked == true)
                        {
                            isMonthChecked = true;
                        }
                    }
                    if (isMonthChecked == true)
                    {
                        save_extra_head_for_fee_for_month();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openExtraFeeAlert();", true);
                        Alertme("Please select the month during which you want to add extra fee.", "warning");
                        return;
                    }
                }
                else
                {
                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        save_extra_head_for_fee(con);

                        dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                        flag = true;
                        con.Close();
                        scope.Complete();
                    }
                    if (flag == true)
                    {
                        if (ViewState["issubmit"].ToString() == "1")
                        {
                            Session["msg"] = "Extra fee title applied successfully for " + txt_student_name.Text;
                            Response.Redirect("fees-collection-1.aspx?adm=" + txt_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_extra_head_for_fee_for_month()
        {
            bool flag = false;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();
                for (int ixi = 0; ixi < rp_extra_head_month.Items.Count; ixi++)
                {
                    CheckBox chkM = (CheckBox)rp_extra_head_month.Items[ixi].FindControl("chk_month_eH");
                    if (chkM.Checked == true)
                    {
                        Label lbl_month_id = (Label)rp_extra_head_month.Items[ixi].FindControl("lbl_month_id");
                        Label lbl_month_name = (Label)rp_extra_head_month.Items[ixi].FindControl("lbl_Month_eH");
                        Label lbl_month_position = (Label)rp_extra_head_month.Items[ixi].FindControl("lbl_month_position");
                        SqlCommand cmd;
                        string query = "INSERT INTO Misc_Fee_Master_Studentwise (Admission_No,Month,Session,Session_id,Perticular,Amount,Ledger,Type_Mode,Date,Idate,Created_by) values (@Admission_No,@Month,@Session,@Session_id,@Perticular,@Amount,@Ledger,@Type_Mode,@Date,@Idate,@Created_by)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Admission_No", hd_admission_no.Value);
                        cmd.Parameters.AddWithValue("@Month", lbl_month_name.Text);
                        cmd.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                        cmd.Parameters.AddWithValue("@Perticular", txt_fee_title.Text);
                        cmd.Parameters.AddWithValue("@Amount", txt_ext_fee_amt.Text);
                        cmd.Parameters.AddWithValue("@Ledger", ViewState["LedgerEffected"].ToString());
                        cmd.Parameters.AddWithValue("@Type_Mode", ddl_extra_head_for.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        if (payments.InsertUpdateData(cmd, con))
                        {
                            ViewState["issubmit"] = "1"; string parameter = ""; string typewise_months = "April"; string typewise_months_position = "1"; string content_id = ""; string group_id = "";
                            if (ViewState["parameter"].ToString() == "MonthlyFee")
                            {
                                if (ddl_extra_head_for.SelectedValue == "2")
                                {
                                    parameter = "AdmissionFee";
                                    content_id = "ADM01";
                                    group_id = "1";
                                }
                                if (ddl_extra_head_for.SelectedValue == "3")
                                {
                                    parameter = "AnnualFee";
                                    content_id = "ANN01";
                                    group_id = "2";
                                }
                            }
                            else
                            {
                                if (ddl_extra_head_for.SelectedValue == "2")
                                {
                                    parameter = "HostelAdmissionFee";
                                    content_id = "ADM01";
                                    group_id = "1";
                                }
                                if (ddl_extra_head_for.SelectedValue == "3")
                                {
                                    parameter = "HostelAnnualFee";
                                    content_id = "ANN01";
                                    group_id = "2";
                                }
                            }

                            string qryChkTypewise = "select Id from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and (parameter='AdmissionFee' or parameter='HostelAdmissionFee' or parameter='AnnualFee' or parameter='HostelAnnualFee')";
                            if (ddl_extra_head_for.SelectedValue == "1")
                            {
                                parameter = ViewState["parameter"].ToString();
                                typewise_months = lbl_month_name.Text;
                                typewise_months_position = lbl_month_position.Text;
                                content_id = "1001";
                                group_id = "3";
                                qryChkTypewise = "select Id from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and month='" + typewise_months + "' and (parameter='MonthlyFee'  or parameter='HostelMonthlyFee')";
                            }
                            if (payments.IsUserExistS(qryChkTypewise, con))
                            { }
                            else
                            {
                                SqlCommand cmd1;
                                string query1 = "INSERT INTO Typewise_fee_collection (admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,user_id,content_id,transection,Ledger,group_id,class_id,position,Disc,Payable_after_disc,branchid) values (@admission_no,@class,@session,@section,@parameter,@Date,@idate,@feetype,@payable,@paid,@dues,@status,@month,@user_id,@content_id,@transection,@Ledger,@group_id,@class_id,@position,@Disc,@Payable_after_disc,@branchid)";
                                cmd1 = new SqlCommand(query1);
                                cmd1.Parameters.AddWithValue("@admission_no", hd_admission_no.Value);
                                cmd1.Parameters.AddWithValue("@class", lblclass_show.Text);
                                cmd1.Parameters.AddWithValue("@session", ViewState["session"].ToString());
                                cmd1.Parameters.AddWithValue("@section", ViewState["Section"].ToString());
                                cmd1.Parameters.AddWithValue("@parameter", parameter);
                                cmd1.Parameters.AddWithValue("@Date", mycode.date());
                                cmd1.Parameters.AddWithValue("@idate", mycode.idate());
                                cmd1.Parameters.AddWithValue("@feetype", txt_fee_title.Text);
                                cmd1.Parameters.AddWithValue("@payable", txt_ext_fee_amt.Text);
                                cmd1.Parameters.AddWithValue("@paid", "0.00");
                                cmd1.Parameters.AddWithValue("@dues", txt_ext_fee_amt.Text);
                                cmd1.Parameters.AddWithValue("@status", "Dues");
                                cmd1.Parameters.AddWithValue("@month", typewise_months);
                                cmd1.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                                cmd1.Parameters.AddWithValue("@content_id", content_id);
                                cmd1.Parameters.AddWithValue("@transection", content_id);
                                cmd1.Parameters.AddWithValue("@Ledger", ViewState["LedgerEffected"].ToString());
                                cmd1.Parameters.AddWithValue("@group_id", group_id);
                                cmd1.Parameters.AddWithValue("@class_id", ViewState["classid"].ToString());
                                cmd1.Parameters.AddWithValue("@position", typewise_months_position);
                                cmd1.Parameters.AddWithValue("@Disc", "0.00");
                                cmd1.Parameters.AddWithValue("@Payable_after_disc", txt_ext_fee_amt.Text);
                                cmd1.Parameters.AddWithValue("@branchid", ViewState["Branchid"].ToString());
                                if (payments.InsertUpdateData(cmd1, con))
                                {
                                }
                            }
                        }
                    }
                }

                dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                flag = true;
                con.Close();
                scope.Complete();
            }
            if (flag == true)
            {
                if (ViewState["issubmit"].ToString() == "1")
                {
                    Session["msg"] = "Extra fee title applied successfully for " + txt_student_name.Text;
                    Response.Redirect("fees-collection-1.aspx?adm=" + txt_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                }
            }
        }

        private void save_extra_head_for_fee(SqlConnection con)
        {
            string month_name = "";
            SqlCommand cmd;
            string query = "INSERT INTO Misc_Fee_Master_Studentwise (Admission_No,Month,Session,Session_id,Perticular,Amount,Ledger,Type_Mode,Date,Idate,Created_by) values (@Admission_No,@Month,@Session,@Session_id,@Perticular,@Amount,@Ledger,@Type_Mode,@Date,@Idate,@Created_by)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Admission_No", hd_admission_no.Value);
            cmd.Parameters.AddWithValue("@Month", month_name);
            cmd.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
            cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
            cmd.Parameters.AddWithValue("@Perticular", txt_fee_title.Text);
            cmd.Parameters.AddWithValue("@Amount", txt_ext_fee_amt.Text);
            cmd.Parameters.AddWithValue("@Ledger", ViewState["LedgerEffected"].ToString());
            cmd.Parameters.AddWithValue("@Type_Mode", ddl_extra_head_for.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Date", mycode.date());
            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            if (payments.InsertUpdateData(cmd, con))
            {
                ViewState["issubmit"] = "1"; string parameter = ""; string typewise_months = month_name; string typewise_months_position = "1"; string content_id = ""; string group_id = "";
                if (ViewState["parameter"].ToString() == "MonthlyFee")
                {
                    if (ddl_extra_head_for.SelectedValue == "2")
                    {
                        parameter = "AdmissionFee";
                        content_id = "ADM01";
                        group_id = "1";
                    }
                    if (ddl_extra_head_for.SelectedValue == "3")
                    {
                        parameter = "AnnualFee";
                        content_id = "ANN01";
                        group_id = "2";
                    }
                }
                else
                {
                    if (ddl_extra_head_for.SelectedValue == "2")
                    {
                        parameter = "HostelAdmissionFee";
                        content_id = "ADM01";
                        group_id = "1";
                    }
                    if (ddl_extra_head_for.SelectedValue == "3")
                    {
                        parameter = "HostelAnnualFee";
                        content_id = "ANN01";
                        group_id = "2";
                    }
                }

                string qryChkTypewise = "select Id from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and (parameter='AdmissionFee' or parameter='HostelAdmissionFee' or parameter='AnnualFee' or parameter='HostelAnnualFee')";
                if (ddl_extra_head_for.SelectedValue == "1")
                {
                    parameter = ViewState["parameter"].ToString();
                    typewise_months = month_name;
                    typewise_months_position = "1";
                    content_id = "1001";
                    group_id = "3";
                    qryChkTypewise = "select Id from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and month='" + month_name + "' and (parameter='MonthlyFee'  or parameter='HostelMonthlyFee')";
                }
                if (payments.IsUserExistS(qryChkTypewise, con))
                { }
                else
                {
                    SqlCommand cmd1;
                    string query1 = "INSERT INTO Typewise_fee_collection (admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,user_id,content_id,transection,Ledger,group_id,class_id,position,Disc,Payable_after_disc,branchid) values (@admission_no,@class,@session,@section,@parameter,@Date,@idate,@feetype,@payable,@paid,@dues,@status,@month,@user_id,@content_id,@transection,@Ledger,@group_id,@class_id,@position,@Disc,@Payable_after_disc,@branchid)";
                    cmd1 = new SqlCommand(query1);
                    cmd1.Parameters.AddWithValue("@admission_no", hd_admission_no.Value);
                    cmd1.Parameters.AddWithValue("@class", lblclass_show.Text);
                    cmd1.Parameters.AddWithValue("@session", ViewState["session"].ToString());
                    cmd1.Parameters.AddWithValue("@section", ViewState["Section"].ToString());
                    cmd1.Parameters.AddWithValue("@parameter", parameter);
                    cmd1.Parameters.AddWithValue("@Date", mycode.date());
                    cmd1.Parameters.AddWithValue("@idate", mycode.idate());
                    cmd1.Parameters.AddWithValue("@feetype", txt_fee_title.Text);
                    cmd1.Parameters.AddWithValue("@payable", txt_ext_fee_amt.Text);
                    cmd1.Parameters.AddWithValue("@paid", "0.00");
                    cmd1.Parameters.AddWithValue("@dues", txt_ext_fee_amt.Text);
                    cmd1.Parameters.AddWithValue("@status", "Dues");
                    cmd1.Parameters.AddWithValue("@month", typewise_months);
                    cmd1.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                    cmd1.Parameters.AddWithValue("@content_id", content_id);
                    cmd1.Parameters.AddWithValue("@transection", content_id);
                    cmd1.Parameters.AddWithValue("@Ledger", ViewState["LedgerEffected"].ToString());
                    cmd1.Parameters.AddWithValue("@group_id", group_id);
                    cmd1.Parameters.AddWithValue("@class_id", ViewState["classid"].ToString());
                    cmd1.Parameters.AddWithValue("@position", typewise_months_position);
                    cmd1.Parameters.AddWithValue("@Disc", "0.00");
                    cmd1.Parameters.AddWithValue("@Payable_after_disc", txt_ext_fee_amt.Text);
                    cmd1.Parameters.AddWithValue("@branchid", ViewState["Branchid"].ToString());
                    if (payments.InsertUpdateData(cmd1, con))
                    {
                    }
                }
            }
        }


        protected void lnkDelExtFee_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");
                Label lbl_perticular = (Label)row.FindControl("lbl_perticular");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_month = (Label)row.FindControl("lbl_month");
                Label lbl_fee_for = (Label)row.FindControl("lbl_fee_for");

                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    if (lbl_fee_for.Text == "MonthlyFee")
                    {
                        payments.exeSql("delete from Misc_Fee_Master_Studentwise where Id='" + lbl_Id.Text + "'; delete from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' and feetype='" + lbl_perticular.Text + "' and content_id='1001' and month='" + lbl_month.Text + "' and status='Dues'", con);
                    }
                    else
                    {
                        payments.exeSql("delete from Misc_Fee_Master_Studentwise where Id='" + lbl_Id.Text + "'; delete from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and feetype='" + lbl_perticular.Text + "' and (content_id='ADM01' or content_id='ANN01') and status='Dues'", con);
                    }
                    string desc = "Extra fee head deleted for head name : " + lbl_perticular.Text + " amount : " + lbl_amount + " month : " + lbl_month.Text;
                    payments.delete_log(lbl_session_id.Text, ViewState["classid"].ToString(), lbl_admission_no.Text, "DeleteExtraFeeHead", desc, "fees-collection-1.aspx", ViewState["Userid"].ToString(), con);
                    dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);


                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                if (flag == true)
                {
                    Session["msg"] = "Deletion process has been successfully done.";
                    Session["IsExtraFeeOpen"] = "1";
                    Response.Redirect("fees-collection-1.aspx?adm=" + lbl_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                }
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit_Bill"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_Receipt_No = (Label)row.FindControl("lbl_Receipt_No");
                    Label lbl_Receipt_Date = (Label)row.FindControl("lbl_Receipt_Date");
                    Label lbl_mode = (Label)row.FindControl("lbl_mode");
                    hd_admission_edt.Value = lbl_admission_no.Text;
                    hd_session_id_edt.Value = lbl_session_id.Text;
                    hd_class_id_edt.Value = lbl_class_id.Text;
                    hd_bill_no_edt.Value = lbl_Receipt_No.Text;
                    hd_old_payment_date.Value = lbl_Receipt_Date.Text;
                    hd_old_payment_mode.Value = lbl_mode.Text;
                    txt_bill_no_edit.Text = lbl_Receipt_No.Text;
                    txt_payment_date_edit.Text = lbl_Receipt_Date.Text;
                    ddl_payment_mode.Text = lbl_mode.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openEditPaymentInfo();", true);
                }
                else
                {
                    Alertme("You are not authorized to do this work.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void btn_update_bill_info_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_payment_date_edit.Text == "")
                {
                    Alertme("Please enter payment date.", "warning");
                    txt_payment_date_edit.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openEditPaymentInfo();", true);
                }
                else
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id,Old_Payment_date,New_Payment_Date) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id,@Old_Payment_date,@New_Payment_Date)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Current_admission_no", hd_admission_edt.Value);
                    cmd.Parameters.AddWithValue("@Session_id", hd_session_id_edt.Value);
                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                    cmd.Parameters.AddWithValue("@Change_type", "Update Payment Date");
                    cmd.Parameters.AddWithValue("@Class_Id_New", hd_class_id_edt.Value);
                    cmd.Parameters.AddWithValue("@Roll_no_New", "0");
                    cmd.Parameters.AddWithValue("@Slip_no", hd_bill_no_edt.Value);
                    cmd.Parameters.AddWithValue("@New_Section", "0");
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Old_Payment_date", hd_old_payment_date.Value);
                    cmd.Parameters.AddWithValue("@New_Payment_Date", txt_payment_date_edit.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                        mycode.executequery("update Monthly_Fee_Collection_Slip set Date='" + txt_payment_date_edit.Text + "',Idate='" + mycode.ConvertStringToiDateup(txt_payment_date_edit.Text) + "' where adno='" + hd_admission_edt.Value + "' and class='" + hd_class_id_edt.Value + "' and session='" + ViewState["session"] + "' and branchid='" + ViewState["Branchid"].ToString() + "' and slipno='" + hd_bill_no_edt.Value + "'");
                        mycode.executequery("update Student_Payment_History set Date='" + txt_payment_date_edit.Text + "',Idate='" + mycode.ConvertStringToiDateup(txt_payment_date_edit.Text) + "',mode='" + ddl_payment_mode.Text + "' where Addmission_no='" + hd_admission_edt.Value + "' and Session='" + ViewState["session"] + "' and Class_id='" + hd_class_id_edt.Value + "' and Branch='" + ViewState["Branchid"].ToString() + "' and Slip_no='" + hd_bill_no_edt.Value + "'");
                        mycode.executequery("update SchoolLedger set Date='" + txt_payment_date_edit.Text + "',IDate='" + mycode.ConvertStringToiDateup(txt_payment_date_edit.Text) + "' where Addmission_no='" + hd_admission_edt.Value + "' and branchid='" + ViewState["Branchid"].ToString() + "' and TransactionId='" + hd_bill_no_edt.Value + "'");
                        Alertme("Payment detail has been updated sucessfully.", "success");


                        My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Bill No :- " + hd_bill_no_edt.Value + ", Old Date= " + hd_old_payment_date.Value + " to New date=" + txt_payment_date_edit.Text + " has been updated successfully.");
                        fetch_ledger();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Update payment date");
            }
        }




        protected void btn_assign_hostel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add_Hostel"].ToString() == "1")
                {
                    if (ddl_hostel.SelectedItem.Text == "Select")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                        Alertme("Please select hostel.", "warning");
                        ddl_hostel.Focus();
                        return;
                    }
                    else if (ddl_room_cat.SelectedItem.Text == "Select")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                        Alertme("Please select room type.", "warning");
                        ddl_room_cat.Focus();
                        return;
                    }
                    else if (ddl_room.SelectedItem.Text == "Select")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                        Alertme("Please select room no.", "warning");
                        ddl_room.Focus();
                        return;
                    }
                    else if (ddl_bed.SelectedItem.Text == "Select")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                        Alertme("Please select bed no.", "warning");
                        ddl_bed.Focus();
                        return;
                    }
                    else
                    {
                        save_hostel_mapping();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                    Alertme("You are not authorized to do this work.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_hostel_mapping()
        {
            My.exeSql("delete from Hostel_assign_master where Session_id='" + ViewState["sessionid"].ToString() + "' and Admission_no='" + ViewState["Admission_no"].ToString() + "'");
            string hostel_assign_id = create_sl_no();

            bool flag = false;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();

                string session_frst_year = ViewState["session"].ToString().Substring(0, 4);
                SqlCommand cmd;
                string query = "INSERT INTO Hostel_assign_master (Session_id,Class_id,Admission_no,From_month_name,From_month_id,Hostel_id,Category_id,Room_id,Bed_id,Hostel_assign_id,Created_by,Created_date,Created_idate,Status,Assined_Year_Month) values (@Session_id,@Class_id,@Admission_no,@From_month_name,@From_month_id,@Hostel_id,@Category_id,@Room_id,@Bed_id,@Hostel_assign_id,@Created_by,@Created_date,@Created_idate,@Status,@Assined_Year_Month)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                cmd.Parameters.AddWithValue("@Admission_no", ViewState["Admission_no"].ToString());
                cmd.Parameters.AddWithValue("@From_month_name", "April");
                cmd.Parameters.AddWithValue("@From_month_id", "04");

                cmd.Parameters.AddWithValue("@Hostel_id", ddl_hostel.SelectedValue);
                cmd.Parameters.AddWithValue("@Category_id", ddl_room_cat.SelectedValue);
                cmd.Parameters.AddWithValue("@Room_id", ddl_room.SelectedValue);
                cmd.Parameters.AddWithValue("@Bed_id", ddl_bed.SelectedValue);
                cmd.Parameters.AddWithValue("@Hostel_assign_id", hostel_assign_id);
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Assined_Year_Month", session_frst_year + "04");
                if (payments.InsertUpdateData(cmd, con))
                {
                    try
                    {
                        payments.save_hostel_aggign_remove_history(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admission_no.Text, "Assign", "", "Hostel Assign", "fees-collection-1.aspx", ViewState["Userid"].ToString(), con);
                        payments.exeSql("update admission_registor set hosteltaken='Yes',transportationtaken='No',Hostel_id='" + ddl_hostel.SelectedValue + "',Hostel_assignD_id='" + hostel_assign_id + "',Transportation_Id='0',Transportationpath='0' where admissionserialnumber='" + ViewState["Admission_no"].ToString() + "' and Session_id='" + ViewState["sessionIDs"].ToString() + "'", con);
                        DataTable dtTns = payments.dataTable("select * from Student_mapping_with_TransportPath where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["Admission_no"].ToString() + "'  and Class_id='" + ViewState["classid"].ToString() + "'", con);
                        if (dtTns.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtTns.Rows)
                            {
                                string qry = "delete from Student_mapping_with_TransportPath where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["Admission_no"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "'; update Transport_Path_Mapping_With_Sheet set Sheet_Status=0 where TransportationPath_id=" + dr["TransportPath_id"].ToString() + " and Transportation_Id=" + dr["transport_id"].ToString() + " and Sheet_Id=" + dr["Sheet_Id"].ToString() + "";
                                payments.exeSql(qry, con);

                                try
                                {
                                    SqlCommand cmd1;
                                    string query2 = "update Student_mapping_with_TransportPath_history set Removed_date=@Removed_date,Removed_Idate=@Removed_Idate,Update_Status=@Update_Status,Remove_Update_Month=@Remove_Update_Month,Remarks=@Remarks where Transport_Assigned_Id=@Transport_Assigned_Id and TransportPath_id=@TransportPath_id and Admission_no=@Admission_no and Session_id=@Session_id";
                                    cmd1 = new SqlCommand(query2);
                                    cmd1.Parameters.AddWithValue("@Removed_date", mycode.date());
                                    cmd1.Parameters.AddWithValue("@Removed_Idate", mycode.idate());
                                    cmd1.Parameters.AddWithValue("@Update_Status", "Transport Removed");
                                    cmd1.Parameters.AddWithValue("@Remove_Update_Month", mycode.get_current_monthname());
                                    cmd1.Parameters.AddWithValue("@Remarks", "Hostel Taken");
                                    cmd1.Parameters.AddWithValue("@TransportPath_id", dr["TransportPath_id"].ToString());
                                    cmd1.Parameters.AddWithValue("@Transport_Assigned_Id", dr["Transport_Assigned_Id"].ToString());
                                    cmd1.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                                    cmd1.Parameters.AddWithValue("@Admission_no", ViewState["Admission_no"].ToString());
                                    if (payments.InsertUpdateData(cmd1, con))
                                    {
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                    }
                    catch
                    {
                    }

                    string disc_parameter = "5";
                    string group_id = "1";
                    string parameter_id = "1";// annulfee
                    string parameter_id2 = "5";// admission fee for hostel
                    string discount_on = "Admission";
                    string parameter = "HostelAdmissionFee";
                    string parameter_old = "AdmissionFee";
                    string studenttype = ViewState["Transfer_Status"].ToString();
                    string parameter_m_old = "MonthlyFee";
                    string parameter_m_new = "HostelMonthlyFee";
                    if (studenttype.ToUpper() == "NEW")
                    {
                        disc_parameter = "5";
                        group_id = "1";
                        parameter_id = "1"; // annulfee
                        parameter_id2 = "5"; // admission fee for hostel
                        parameter = "HostelAdmissionFee";
                        parameter_old = "AdmissionFee";
                        discount_on = "Admission";
                    }
                    else
                    {
                        disc_parameter = "6";
                        group_id = "2";
                        parameter_id = "2"; // annulfee
                        parameter_id2 = "6"; // admission fee for hostel 
                        parameter = "HostelAnnualFee";
                        parameter_old = "AnnualFee";
                        discount_on = "Annual";
                    }
                    payments.exeSql("update Typewise_fee_collection set parameter='" + parameter_m_new + "'  where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter_m_old + "'; update Typewise_fee_collection set parameter='" + parameter + "' where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter_old + "' ", con);


                    if (ViewState["firm_id"].ToString() == "DIS-01")
                    {
                        DataTable dtH = payments.dataTable("select * from Typewise_fee_collection where session='" + ViewState["session"].ToString() + "' and admission_no='" + txt_admission_no.Text + "' and parameter='" + parameter + "' and class_id='" + ViewState["classid"].ToString() + "'", con);
                        if (dtH.Rows.Count > 0)
                        {
                            string qryH = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ddl_hostel.SelectedValue + " and admission_no='" + txt_admission_no.Text + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ddl_hostel.SelectedValue + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + ddlsessionad.SelectedItem.Text + "' and class_id='" + ViewState["classid"].ToString() + "' and  fmc.Hostel_Id='" + ddl_hostel.SelectedValue + "') t";
                            if (parameter_old == "AdmissionFee")
                            {
                                qryH = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ddl_hostel.SelectedValue + " and admission_no='" + txt_admission_no.Text + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ddl_hostel.SelectedValue + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ddlsessionad.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + ddlsessionad.SelectedItem.Text + "' and class_id='" + ViewState["classid"].ToString() + "' and  fmc.Hostel_Id='" + ddl_hostel.SelectedValue + "' ) t";
                            }

                            DataTable dtF = payments.dataTable(qryH, con);
                            if (dtF.Rows.Count > 0)
                            {
                                payments.exeSql("update Discount_Master set parameter_id='" + disc_parameter + "',Hostel_id='" + ddl_hostel.SelectedValue + "',discount_for='Hosteler' where admission_no='" + txt_admission_no.Text + "' and session='" + ViewState["session"].ToString() + "' and Discount_on='" + discount_on + "'", con);
                                foreach (DataRow dr in dtF.Rows)
                                {
                                    if (payments.IsUserExistS("Select Id from Typewise_fee_collection where session='" + ViewState["session"].ToString() + "' and admission_no='" + txt_admission_no.Text + "' and feetype='" + dr["feetype"].ToString() + "' and parameter='" + parameter + "'", con))
                                    {
                                        payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,user_id,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Previous_admission_no,group_id,class_id,position,Disc,Payable_after_disc,delete_sync,branchid,Acamedic_Semester_Id,Is_month_skip,parameter2) values ('" + txt_admission_no.Text + "','" + lblclass.Text + "','" + ViewState["session"].ToString() + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["feetype"].ToString() + "','" + dr["payable"].ToString() + "','0.00','" + dr["net_payable"].ToString() + "','Dues','April','" + ViewState["Userid"].ToString() + "','" + dr["content_id"].ToString() + "','NA00','Hostel','1','','','','" + group_id + "','" + ViewState["classid"].ToString() + "','1','" + dr["disc_amount"].ToString() + "','" + dr["net_payable"].ToString() + "','','1','0','','');", con);
                                    }
                                }
                            }
                        }
                    }
                    dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);

                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                    Alertme("There are no vacant beds in the hostel.Please create the bed first, then map its students.", "warning");
                }
            }

            if (flag == true)
            {
                Session["msg"] = "Hostel has been mapped with student successfully.";
                Response.Redirect("fees-collection-1.aspx?adm=" + ViewState["Admission_no"].ToString() + "&sessionid=" + ddlsessionad.SelectedValue, false);
            }
        }

        private string get_total_adm_fee(string type, string hostel_id, SqlConnection con)
        {
            string TotalFee = "0"; string qry = "";
            //ANNUAL
            if (type == "daysAn")
            {
                qry = "select isnull(sum(convert(float, payable)),0) as TotalFee from (select fmc.session,cm.content feetype,cast(fmc.amount as float) payable,cm.content_id from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='AnnualFee' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "')t";
            }
            if (type == "hostelAn")
            {
                qry = "select isnull(sum(convert(float, payable)),0) as TotalFee from (select fmc.session,cm.content feetype,cast(fmc.amount as float) payable,cm.content_id from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='HostelAnnualFee' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + hostel_id + "')t";
            }

            //ADMISSION
            if (type == "daysAd")
            {
                qry = "select isnull(sum(convert(float, payable)),0) as TotalFee from (select fmc.session,cm.content feetype,cast(fmc.amount as float) payable,cm.content_id from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='AdmissionFee'  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "')t";
            }
            if (type == "hostelAd")
            {
                qry = "select isnull(sum(convert(float, payable)),0) as TotalFee from (select fmc.session,cm.content feetype,cast(fmc.amount as float) payable,cm.content_id from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='HostelAdmissionFee'  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + hostel_id + "')t";
            }
            DataTable dt = payments.dataTable(qry, con);
            if (dt.Rows.Count > 0)
            {
                TotalFee = dt.Rows[0]["TotalFee"].ToString();
            }
            return TotalFee;
        }


        private string create_sl_no()
        {
            bool duplicate = true;
            string hostel_assign_id = My.auto_serialS("Hostel_assign_id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select Hostel_assign_id from Hostel_assign_master where Hostel_assign_id='" + hostel_assign_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    hostel_assign_id = My.auto_serialS("Hostel_assign_id");
                }
            }
            return hostel_assign_id;
        }

        protected void btn_remove_hostel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete_Hostel"].ToString() == "1")
                {
                    if (ddl_hostel_remove_month.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select the month from which student would like to discontinue the hostel.", "warning");
                        ddl_hostel_remove_month.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                        return;
                    }
                    if (txt_h_rmov_remark.Text == "")
                    {
                        Alertme("Please enter remark.", "warning");
                        txt_h_rmov_remark.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                        return;
                    }

                    string parameter = "AdmissionFee";
                    string parameter_old = "HostelAdmissionFee";
                    string studenttype = ViewState["Transfer_Status"].ToString();

                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        save_hostel_transport_dues(ddl_hostel_remove_month.SelectedItem.Text, ddl_hostel_remove_month.SelectedValue, con);
                        payments.exeSql("update Hostel_assign_master set Status='0',Remove_cause='" + txt_h_rmov_remark.Text + "',Updated_by='" + ViewState["Userid"].ToString() + "',Updated_date='" + mycode.date() + "',Updated_idate='" + mycode.idate() + "' where Hostel_assign_id='" + ViewState["Hostel_assign_id"].ToString() + "'", con);
                        payments.exeSql("update admission_registor set hosteltaken='No',Hostel_id='0' where admissionserialnumber='" + ViewState["Admission_no"].ToString() + "' and Session_id='" + ViewState["sessionIDs"].ToString() + "'", con);
                        string disc_parameter = "1";
                        string discount_on = "Admission";
                        string parameter_m_old = "HostelMonthlyFee";
                        string parameter_m_new = "MonthlyFee";
                        if (studenttype == "New")
                        {
                            discount_on = "Admission";
                            disc_parameter = "1";
                            parameter = "AdmissionFee";
                            parameter_old = "HostelAdmissionFee";
                        }
                        else
                        {
                            discount_on = "Annual";
                            disc_parameter = "2";
                            parameter = "AnnualFee";
                            parameter_old = "HostelAnnualFee";
                        }

                        payments.exeSql("update Typewise_fee_collection set parameter='" + parameter_m_new + "'  where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter_m_old + "'; update Typewise_fee_collection set parameter='" + parameter + "'  where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter_old + "' ", con);


                        if (ViewState["firm_id"].ToString() == "DIS-01")
                        {
                            payments.exeSql("update Discount_Master set parameter_id='" + disc_parameter + "',Hostel_id='',discount_for='Days' where admission_no='" + txt_admission_no.Text + "' and session='" + ViewState["session"].ToString() + "' and Discount_on='" + discount_on + "'", con);
                        }



                        payments.save_hostel_aggign_remove_history(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admission_no.Text, "Remove", ddl_hostel_remove_month.SelectedItem.Text, txt_h_rmov_remark.Text, "fees-collection-1.aspx", ViewState["Userid"].ToString(), con);
                        string desc = "Hostel removed for name : " + lbl_name.Text;
                        payments.delete_log(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admission_no.Text, "RemoveHostel", desc, "fees-collection-1.aspx", ViewState["Userid"].ToString(), con);
                        dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);


                        flag = true;
                        con.Close();
                        scope.Complete();
                    }
                    if (flag == true)
                    {
                        Session["msg"] = "Student has been removed from hostel successfully.";
                        Response.Redirect("fees-collection-1.aspx?adm=" + ViewState["Admission_no"].ToString() + "&sessionid=" + ddlsessionad.SelectedValue, false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                    Alertme("You are not authorized to do this work.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }




        #region HosteL
        private void fetch_hostel_info_for_assign()
        {
            try
            {
                mycode.bind_all_ddl_with_id(ddl_hostel, "select DISTINCT Hostel_name,Hostel_id from (select t1.Hostel_name,t1.Hostel_id,t2.Category_id,t2.Room_id,t2.Bed_id from Hostels_master t1 join HOSTEL_ROOM_BED_MASTER t2 on t1.Hostel_id=t2.Hostel_id where t2.Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id=t2.Room_id and Status='1' and Hostel_id=t2.Hostel_id and Category_id=t2.Category_id)) t order by Hostel_name asc");
                try
                {
                    ddl_hostel.SelectedValue = My.top_one_hostel_id();
                }
                catch (Exception ex)
                {
                }
                mycode.bind_all_ddl_with_id_no_select(ddl_room_cat, "select Category_name,Category_id from Hostel_room_category_master order by Category_name asc");
                fetch_rooms(); fetch_bed_details();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_rooms()
        {
            mycode.bind_all_ddl_with_id(ddl_room, "select Room_name,Room_id from Hostel_room_master where Hostel_id='" + ddl_hostel.SelectedValue + "' and Category_id='" + ddl_room_cat.SelectedValue + "' order by Room_name asc");
            string room_id = My.top_one_hostel_room(ddl_hostel.SelectedValue, ddl_room_cat.SelectedValue);
            try
            {
                ddl_room.SelectedValue = room_id; fetch_bed_details();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_bed_details()
        {
            mycode.bind_all_ddl_with_id(ddl_bed, "select 'Bed No. :'+Bed_name+', Bed Position '+Bed_Position,Bed_id from Hostel_room_bed_master where Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id='" + ddl_room.SelectedValue + "' and Status='1' and Hostel_id=" + ddl_hostel.SelectedValue + " and Category_id=" + ddl_room_cat.SelectedValue + " and Session_id='" + ViewState["sessionIDs"].ToString() + "') and Room_id='" + ddl_room.SelectedValue + "' and Hostel_id='" + ddl_hostel.SelectedValue + "'and Category_id='" + ddl_room_cat.SelectedValue + "' order by Id asc");
            string bed_id = My.get_top_on_bed(ddl_hostel.SelectedValue, ddl_room_cat.SelectedValue, ddl_room.SelectedValue, ViewState["sessionIDs"].ToString());
            try
            {
                ddl_bed.SelectedValue = bed_id;
            }
            catch (Exception ex)
            {
            }
        }

        double ttl_adm_amts = 0; double ttl_adm_disc = 0; double ttl_adm_pre_paid = 0; double ttl_adm_payble = 0;
        protected void grd_fee_adm_fee_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["Is_checkbx_hide_pay_fee"].ToString() == "1")
                {
                    HtmlTableCell tdHide = (HtmlTableCell)e.Item.FindControl("tdHideAdm");
                    tdHide.Style["display"] = "none";
                }


                Label lbl_amount = ((Label)e.Item.FindControl("lbl_amount")) as Label;
                Label lbl_disc_amt = ((Label)e.Item.FindControl("lbl_disc_amt")) as Label;
                Label lbl_pre_paid = ((Label)e.Item.FindControl("lbl_pre_paid")) as Label;
                Label lbl_tot_pble = ((Label)e.Item.FindControl("lbl_tot_pble")) as Label;


                ttl_adm_amts = ttl_adm_amts + My.toDouble(lbl_amount.Text);
                ttl_adm_disc = ttl_adm_disc + My.toDouble(lbl_disc_amt.Text);
                ttl_adm_pre_paid = ttl_adm_pre_paid + My.toDouble(lbl_pre_paid.Text);
                ttl_adm_payble = ttl_adm_payble + My.toDouble(lbl_tot_pble.Text);

            }
            lbl_ttl_adm_payble.Text = ttl_adm_amts.ToString("0.00");
            lbl_ttl_adm_disc.Text = ttl_adm_disc.ToString("0.00");
            lbl_ttl_prev_adm_paid.Text = ttl_adm_pre_paid.ToString("0.00");
            lbl_adm_net_payble.Text = ttl_adm_payble.ToString("0.00");
        }

        protected void ddl_hostel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    ddl_hostel.Focus();
                    Alertme("Please choose hostel.", "warning");
                }
                else
                {
                    fetch_rooms();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }

        protected void ddl_room_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    ddl_hostel.Focus();
                    Alertme("Please choose hostel.", "warning");
                }
                else if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    ddl_room_cat.Focus();
                    Alertme("Please choose category.", "warning");
                }
                else
                {
                    fetch_rooms();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }

        protected void ddl_room_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHostel();", true);
                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose hostel.", "warning");
                    ddl_hostel.Focus();
                }
                else if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose category.", "warning");
                    ddl_room_cat.Focus();
                }
                else if (ddl_room.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose room.", "warning");
                    ddl_room.Focus();
                }
                else
                {
                    fetch_bed_details();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }
        #endregion

        double amtss = 0; double disc_amts = 0; double after_disc = 0;
        protected void rd_discount_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string lbl_amount = ((Label)e.Item.FindControl("lbl_amount")).Text;
                string lbl_disc_amount = ((Label)e.Item.FindControl("lbl_disc_amount")).Text;
                string lbl_after_disc = ((Label)e.Item.FindControl("lbl_after_disc")).Text;
                amtss = amtss + My.toDouble(lbl_amount);
                disc_amts = disc_amts + My.toDouble(lbl_disc_amount);
                after_disc = after_disc + My.toDouble(lbl_after_disc);
            }

            lbl_ttl_disc_amtss1.Text = amtss.ToString();
            lbl_ttl_disc_amtss2.Text = disc_amts.ToString();
            lbl_ttl_disc_amtss3.Text = after_disc.ToString();
        }


        ///============================================
        ///
        private void save_hostel_transport_dues(string monthName, string month_id, SqlConnection con)
        {
            string month_position = payments.get_single_column_data("select Position as Column_Name from Month_Index where Month_Id='" + month_id + "'", con);
            DataTable dtm = payments.dataTable("select * from Month_Index where Position<" + month_position + " order by Position asc", con);
            if (dtm.Rows.Count > 0)
            {
                foreach (DataRow dr in dtm.Rows)
                {
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        int mnthids = My.tomonth_number(dr["Month"].ToString());
                        if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                        {
                            ViewState["parameteridS"] = "44";
                        }
                        else
                        {
                            ViewState["parameteridS"] = "4";
                        }
                    }

                    if (payments.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + dr["Month"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' and transection!=''", con).Rows.Count > 0)
                    {
                        continue;
                    }
                    else
                    {
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = txt_admission_no.Text;
                        dc["session_id"] = ViewState["sessionIDs"].ToString();
                        dc["class"] = lblclass.Text;
                        dc["session"] = ViewState["session"].ToString();
                        dc["class_id"] = ViewState["classid"].ToString();
                        dc["hosteltaken"] = lblhostel.Text;
                        dc["months"] = dr["Month"].ToString();
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                        dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                        dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();

                        dc["category_id"] = ViewState["category_id"].ToString();
                        dc["sub_category_id"] = ViewState["sub_category_id"].ToString();
                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();

                        dc["parameter_id"] = ViewState["parameteridS"].ToString();
                        //new08/08/2022

                        string cunrt_session = ViewState["session"].ToString();
                        string session_frst_year = cunrt_session.Substring(0, 4);
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);
                        string monthid = My.tomonth_numberstring(dr["Month"].ToString());
                        int pay_month = My.toint(monthid);
                        s_year = payments.check_start_months(pay_month, s_year, con);
                        dc["monthid"] = s_year + monthid;
                        DataTable feedt = payments.dataTableSP("sp_fetch_monthly_fee", dc, con);
                        send_in_typewise_fee_dues(feedt, dr["Month"].ToString(), "HS00221", ViewState["classid"].ToString(), dr["Position"].ToString(), con);
                    }
                }
            }


            //======================================================= 
            DataTable dtmR = payments.dataTable("select * from Month_Index where Position>='" + month_position + "' order by Position asc", con);
            if (dtmR.Rows.Count > 0)
            {
                foreach (DataRow drR in dtmR.Rows)
                {
                    payments.exeSql("delete from Typewise_fee_collection where admission_no='" + txt_admission_no.Text + "' and session='" + ViewState["session"].ToString() + "' and month in (select month from ( Select sum(cast(paid as float)) paid,month from dbo.[Typewise_fee_collection] where admission_no='" + txt_admission_no.Text + "' and session='" + ViewState["session"].ToString() + "' and month='" + drR["Month"].ToString() + "' group by month) t where paid=0 and status='Dues');", con);
                }
            }
        }

        private void send_in_typewise_fee_dues(DataTable feedt, string month_name, string slipno, string class_id, string month_position, SqlConnection con)
        {
            double fine = My.toDouble(txtfineamount.Text);
            if (fine > 0)
            {
                int mnth_idss = My.tomonth_number(month_name);
                string month_id = My.getMonthS_twoDigit(mnth_idss.ToString());
                DataTable dt = payments.dataTable("select Fine_amount from Temp_fine_monthwise where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + feedt.Rows[0]["admission_no"].ToString() + "' and Month_id='" + month_id + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'", con);
                if (dt.Rows.Count > 0)
                {
                    fine = My.toDouble(dt.Rows[0]["Fine_amount"].ToString());
                    payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,class_id,position) values ('" + feedt.Rows[0]["admission_no"].ToString() + "','" + feedt.Rows[0]["class"].ToString() + "','" + feedt.Rows[0]["session"].ToString() + "','" + feedt.Rows[0]["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine','" + My.toDouble(fine).ToString("0.00") + "','0','" + My.toDouble(fine).ToString("0.00") + "','Dues','" + month_name + "','6121','','School','false','false','false','0.00','" + ViewState["Section"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["Branchid"].ToString() + "','" + class_id + "','" + month_position + "')", con);
                }
            }

            foreach (DataRow dr in feedt.Rows)
            {
                string parm = "";
                if (dr["parameter"].ToString() == "AdditionalServices")
                {
                    parm = "HostelMonthlyFee";
                }
                else if (dr["parameter"].ToString() == "DamageCharges")
                {
                    parm = "HostelMonthlyFee";
                }
                else
                {
                    parm = dr["parameter"].ToString();
                }
                payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,parameter2,class_id,position) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + parm + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["content"].ToString() + "','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','" + dr["content_id"].ToString() + "','" + slipno + "','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + ViewState["Section"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["Branchid"].ToString() + "','" + dr["parameter"].ToString() + "','" + class_id + "','" + month_position + "')", con);
            }
        }

        protected void lnk_apply_coupan_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                ViewState["isApplyDiscByCoupon"] = "1";
                txt_apply_discount_status.Text = "0";
            }
            catch (Exception ex)
            {
            }
        }



        //===============================================================================
        #region TEMP DISCOUNT/COUPON
        private void save_temp_adm_disc()
        {
            bool isamountfill = false;
            for (int i = 0; i < rp_fee_head_admission.Items.Count; i++)
            {
                TextBox txt_disc_fee = (TextBox)rp_fee_head_admission.Items[i].FindControl("txt_adm_disc_fee");
                if (My.toDouble(txt_disc_fee.Text) > 0)
                {
                    isamountfill = true;
                }
            }
            if (isamountfill == true)
            {
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    string couponId = payments.auto_serialS("Student_discount_entry_id", con);
                    double totla = 0;
                    for (int i = 0; i < rp_fee_head_admission.Items.Count; i++)
                    {
                        TextBox txt_fee = (TextBox)rp_fee_head_admission.Items[i].FindControl("txt_adm_head_fee");
                        TextBox txt_disc_fee = (TextBox)rp_fee_head_admission.Items[i].FindControl("txt_adm_disc_fee");
                        Label lbl_content_id = (Label)rp_fee_head_admission.Items[i].FindControl("lbl_adm_content_id");
                        Label lbl_content = (Label)rp_fee_head_admission.Items[i].FindControl("lbl_adm_content");
                        totla = totla + My.toDouble(txt_fee.Text);

                        //==============*************** 
                        if (My.toDouble(txt_disc_fee.Text) > 0)
                        {
                            string discount_fee = txt_disc_fee.Text;
                            if (My.toDouble(txt_disc_fee.Text) > My.toDouble(txt_fee.Text))
                            {
                                discount_fee = txt_fee.Text;
                            }
                            string query = ""; string discount_on = ""; string parameter_id = ""; string disc_for = ""; string hostel_id = ""; string room_cat_id = "";
                            if (ViewState["parameter"].ToString() == "HostelMonthlyFee")
                            {
                                hostel_id = ViewState["Hostel_id"].ToString();
                                room_cat_id = ViewState["Room_Category_id"].ToString();
                                if (hd_fee_group.Value == "1") //admission fee hostel
                                {
                                    disc_for = "Hosteler";
                                    parameter_id = "5";
                                    discount_on = "Admission";
                                    query = "select * from Discount_Master_temp where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='" + hd_fee_group.Value + "' and month='NA' and admission_no='" + hd_admission_no.Value + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "' and Hostel_id=" + ViewState["Hostel_id"].ToString() + "";
                                }
                                if (hd_fee_group.Value == "2") //Annual fee hostel
                                {
                                    disc_for = "Hosteler";
                                    parameter_id = "6";
                                    discount_on = "Annual";
                                    query = "select * from Discount_Master_temp where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='" + hd_fee_group.Value + "' and month='NA' and admission_no='" + hd_admission_no.Value + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "' and Hostel_id=" + ViewState["Hostel_id"].ToString() + "";
                                }
                            }
                            else
                            {
                                if (hd_fee_group.Value == "1") //admission fee
                                {
                                    disc_for = "Days";
                                    parameter_id = "1";
                                    discount_on = "Admission";
                                    query = "select * from Discount_Master_temp where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='" + hd_fee_group.Value + "' and month='NA' and admission_no='" + hd_admission_no.Value + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "'";
                                }
                                if (hd_fee_group.Value == "2") //Annual fee
                                {
                                    disc_for = "Days";
                                    parameter_id = "2";
                                    discount_on = "Annual";
                                    query = "select * from Discount_Master_temp where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='" + hd_fee_group.Value + "' and month='NA' and admission_no='" + hd_admission_no.Value + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "'";
                                }
                            }


                            SqlDataAdapter ad = new SqlDataAdapter(query, con);
                            DataSet ds = new DataSet();
                            ad.Fill(ds, "Discount_Master_temp");
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count == 0)
                            {
                                SqlCommand cmd;
                                string queryD = "INSERT INTO Discount_Master_temp (Type,Class_id,Discount_on,session,Discount_per,group_id,admission_no,month,fee_head_id,disc_amount,parameter_id,session_id,Branch_id,User_id,Date,time,discount_for,category_id,sub_category_id,Upload_from,Hostel_id,Room_Category_id,Student_Discunt_Type_id,Student_Discunt_Remarks,Student_discount_type_entry_id,Fee_amount,Status) values (@Type,@Class_id,@Discount_on,@session,@Discount_per,@group_id,@admission_no,@month,@fee_head_id,@disc_amount,@parameter_id,@session_id,@Branch_id,@User_id,@Date,@time,@discount_for,@category_id,@sub_category_id,@Upload_from,@Hostel_id,@Room_Category_id,@Student_Discunt_Type_id,@Student_Discunt_Remarks,@Student_discount_type_entry_id,@Fee_amount,@Status)";
                                cmd = new SqlCommand(queryD);
                                cmd.Parameters.AddWithValue("@Type", "Admission");
                                cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                                cmd.Parameters.AddWithValue("@Discount_on", discount_on);
                                cmd.Parameters.AddWithValue("@session", ViewState["session"].ToString());
                                cmd.Parameters.AddWithValue("@Discount_per", My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2));
                                cmd.Parameters.AddWithValue("@group_id", hd_fee_group.Value);
                                cmd.Parameters.AddWithValue("@admission_no", hd_admission_no.Value);
                                cmd.Parameters.AddWithValue("@month", "NA");
                                cmd.Parameters.AddWithValue("@fee_head_id", lbl_content_id.Text);
                                cmd.Parameters.AddWithValue("@Fee_amount", My.toDouble(txt_fee).ToString("0.00"));
                                cmd.Parameters.AddWithValue("@disc_amount", My.toDouble(discount_fee).ToString("0.00"));
                                cmd.Parameters.AddWithValue("@parameter_id", parameter_id);
                                cmd.Parameters.AddWithValue("@session_id", ViewState["sessionIDs"].ToString());
                                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Date", mycode.date());
                                cmd.Parameters.AddWithValue("@time", mycode.time());
                                cmd.Parameters.AddWithValue("@discount_for", disc_for);
                                cmd.Parameters.AddWithValue("@category_id", ViewState["category_id"].ToString());
                                cmd.Parameters.AddWithValue("@sub_category_id", ViewState["sub_category_id"].ToString());
                                cmd.Parameters.AddWithValue("@Upload_from", "");
                                cmd.Parameters.AddWithValue("@Hostel_id", hostel_id);
                                cmd.Parameters.AddWithValue("@Room_Category_id", room_cat_id);
                                cmd.Parameters.AddWithValue("@Student_Discunt_Type_id", ddl_discount_mode.SelectedValue);
                                cmd.Parameters.AddWithValue("@Student_Discunt_Remarks", txt_discount_Remarks.Text);
                                cmd.Parameters.AddWithValue("@Student_discount_type_entry_id", couponId);
                                cmd.Parameters.AddWithValue("@Status", "Pending");
                                if (payments.InsertUpdateData(cmd, con))
                                {
                                }
                            }
                            else
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    dr["Type"] = "Admission";
                                    dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                    dr["fee_head_id"] = lbl_content_id.Text;
                                    dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                    dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                    dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                    dr["Fee_amount"] = My.toDouble(txt_fee).ToString("0.00");
                                }
                                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                ad.Update(dt);
                            }
                        }
                    }


                    //===================================================== 
                    DataTable dtaD = payments.dataTable("select isnull(sum(convert(float, Discount_amt)),0) as Discount_amt,isnull(sum(convert(float, Total_amount)),0) as Total_amount from (select isnull(sum(convert(float, disc_amount)),0) as Discount_amt,isnull(sum(convert(float, Fee_amount)),0) as Total_amount from Discount_Master_temp where session='" + ViewState["session"].ToString() + "' and admission_no='" + txt_admission_no.Text + "' and Student_discount_type_entry_id='" + couponId + "') t", con);
                    if (dtaD.Rows.Count > 0)
                    {
                        if (My.toDouble(dtaD.Rows[0]["Discount_amt"].ToString()) > 0)
                        {
                            payments.exeSql("update Student_Payment_History set Is_coupon_applied='1' where Session='" + ViewState["session"].ToString() + "' and Addmission_no='" + txt_admission_no.Text + "'", con);
                            SqlCommand cmds;
                            string queryss = "INSERT INTO Coupon_applied_list (Coupon_id,Session_id,Class_id,Admission_no,Amount,Created_by,Created_date,Created_time,Created_idate,Total_amount,Discount_persent,Status) values (@Coupon_id,@Session_id,@Class_id,@Admission_no,@Amount,@Created_by,@Created_date,@Created_time,@Created_idate,@Total_amount,@Discount_persent,@Status)";
                            cmds = new SqlCommand(queryss);
                            cmds.Parameters.AddWithValue("@Coupon_id", couponId);
                            cmds.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                            cmds.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                            cmds.Parameters.AddWithValue("@Admission_no", lbl_admission_no.Text);
                            cmds.Parameters.AddWithValue("@Amount", dtaD.Rows[0]["Discount_amt"].ToString());
                            cmds.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                            cmds.Parameters.AddWithValue("@Created_date", mycode.date());
                            cmds.Parameters.AddWithValue("@Created_time", mycode.time());
                            cmds.Parameters.AddWithValue("@Created_idate", mycode.idate());

                            cmds.Parameters.AddWithValue("@Total_amount", dtaD.Rows[0]["Total_amount"].ToString());
                            cmds.Parameters.AddWithValue("@Discount_persent", My.Round((My.toDouble(dtaD.Rows[0]["Discount_amt"].ToString()) * 100) / My.toDouble(dtaD.Rows[0]["Total_amount"].ToString()), 2));
                            cmds.Parameters.AddWithValue("@Status", "Pending");
                            if (payments.InsertUpdateData(cmds, con))
                            {
                            }
                        }
                    }

                    ViewState["issubmit"] = "1";
                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                if (flag == true)
                {
                    Session["IsDiscountOpen"] = "0";
                    Session["msg"] = "The coupon discount has been saved and sent to the admin for approval.";
                    Response.Redirect("fees-collection-1.aspx?adm=" + txt_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                Alertme("Please enter the amount for which you would like to provide a discount.", "warning");
            }
        }
        #endregion



        private void save_temp_monthly_disc()
        {
            My.exeSql("delete from Discount_master_check_dup_temp where Admission_no='" + hd_admission_no.Value + "'");
            bool isMonthChecked = false;
            for (int iMC = 0; iMC < rp_month_for_discount.Items.Count; iMC++)
            {
                CheckBox chkM = (CheckBox)rp_month_for_discount.Items[iMC].FindControl("chk_month_discount");
                if (chkM.Checked == true)
                {
                    isMonthChecked = true;
                }
            }

            if (isMonthChecked == true)
            {
                string query = " ";
                bool isamountfill = false;
                for (int i = 0; i < rd_discount_fee_head.Items.Count; i++)
                {
                    TextBox txt_disc_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_disc_fee");
                    if (My.toDouble(txt_disc_fee.Text) > 0)
                    {
                        isamountfill = true;
                    }
                }


                if (isamountfill == true)
                {
                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        string couponId = payments.auto_serialS("Student_discount_entry_id", con);
                        for (int ixi = 0; ixi < rp_month_for_discount.Items.Count; ixi++)
                        {
                            CheckBox chkM = (CheckBox)rp_month_for_discount.Items[ixi].FindControl("chk_month_discount");
                            if (chkM.Checked == true)
                            {
                                Label lbl_month_id = (Label)rp_month_for_discount.Items[ixi].FindControl("lbl_month_id");
                                Label lbl_month_name = (Label)rp_month_for_discount.Items[ixi].FindControl("lbl_Month_discount");

                                #region MonthLY 
                                string Month = lbl_month_name.Text;
                                double totla = 0;
                                for (int i = 0; i < rd_discount_fee_head.Items.Count; i++)
                                {
                                    TextBox txt_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_head_fee");
                                    TextBox txt_disc_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_disc_fee");
                                    Label lbl_content_id = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content_id");
                                    Label lbl_content = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content");
                                    totla = totla + My.toDouble(txt_fee.Text);
                                    //==============*************** 

                                    if (lbl_content_id.Text == "1002") // FOR TRANSPORT
                                    {
                                        if (My.toDouble(txt_disc_fee.Text) > 0)
                                        {
                                            save_bus_discount_temp(couponId, con);
                                        }
                                    }
                                    else
                                    {
                                        if (My.toDouble(txt_disc_fee.Text) > 0)
                                        {
                                            string qryChk = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + ViewState["class_id"].ToString() + "' and fmc.session_id='" + ViewState["sessionIDs"].ToString() + "' and fmc.parameter_id='4' and fmc.Month='" + Month + "' and fmc.content_id='" + lbl_content_id.Text + "'";
                                            string hostel_taken = ViewState["hostel_takens"].ToString();
                                            if (hostel_taken.ToUpper() == "YES")
                                            {
                                                qryChk = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + ViewState["class_id"].ToString() + "' and fmc.session_id='" + ViewState["sessionIDs"].ToString() + "' and fmc.parameter_id='3' and fmc.Month='" + Month + "' and fmc.content_id='" + lbl_content_id.Text + "' and fmc.parameter_id='3'  and fmc.Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and fmc.Room_Category_id=" + ViewState["Room_Category_id"].ToString() + "";
                                            }
                                            DataTable dtFC = payments.dataTable(qryChk, con);
                                            if (dtFC.Rows.Count > 0)
                                            {
                                                string discount_fee = txt_disc_fee.Text;
                                                if (My.toDouble(txt_disc_fee.Text) > My.toDouble(txt_fee.Text))
                                                {
                                                    discount_fee = txt_fee.Text;
                                                }

                                                DataTable dtF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Paid'", con);
                                                if (dtF.Rows.Count == 0)
                                                {
                                                    //CHECK IN TYPEWISE
                                                    DataTable dtT = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Dues'", con);
                                                    if (dtT.Rows.Count > 0)
                                                    {
                                                        if (My.toDouble(dtT.Rows[0]["dues"].ToString()) > 0)
                                                        {
                                                            double duesamts = (My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString()));
                                                            if (My.toDouble(discount_fee) > duesamts)
                                                            {
                                                                discount_fee = duesamts.ToString();
                                                            }
                                                        }
                                                    }

                                                    //CHECK IN TYPEWISE 
                                                    if (ViewState["parameterDisc"].ToString() == "3") // hostel
                                                    {
                                                        query = "select * from Discount_Master_temp where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='3' and month='" + Month + "' and admission_no='" + txt_admission_no.Text + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + ViewState["parameterDisc"].ToString() + "' and Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Room_Category_id=" + ViewState["Room_Category_id"].ToString() + "";
                                                    }
                                                    else
                                                    {
                                                        query = "select * from Discount_Master_temp where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='3' and month='" + Month + "' and admission_no='" + txt_admission_no.Text + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + ViewState["parameterDisc"].ToString() + "'";
                                                    }

                                                    SqlDataAdapter ad = new SqlDataAdapter(query, con);
                                                    DataSet ds = new DataSet();
                                                    ad.Fill(ds, "Discount_Master_temp");
                                                    DataTable dt = ds.Tables[0];
                                                    if (dt.Rows.Count == 0)
                                                    {
                                                        DataRow dr = dt.NewRow();
                                                        dr["Type"] = "Tuition";
                                                        dr["Class_id"] = ViewState["class_id"].ToString();
                                                        dr["Discount_on"] = "Monthly";
                                                        dr["session"] = ViewState["session"].ToString();
                                                        dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                                        dr["group_id"] = "3";
                                                        dr["admission_no"] = txt_admission_no.Text;
                                                        dr["month"] = Month;
                                                        dr["fee_head_id"] = lbl_content_id.Text;
                                                        dr["Fee_amount"] = My.toDouble(txt_fee.Text).ToString("0.00");
                                                        dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                                        dr["parameter_id"] = ViewState["parameterDisc"].ToString();
                                                        dr["category_id"] = ViewState["category_id"].ToString();
                                                        dr["sub_category_id"] = ViewState["sub_category_id"].ToString();
                                                        dr["session_id"] = ViewState["sessionIDs"].ToString();
                                                        dr["Branch_id"] = ViewState["Branchid"].ToString();
                                                        dr["User_id"] = ViewState["Userid"].ToString();
                                                        dr["Date"] = mycode.date();
                                                        dr["time"] = mycode.time();
                                                        dr["Hostel_id"] = ViewState["Hostel_id"].ToString();
                                                        dr["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                                                        dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                                        dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                                        dr["Student_discount_type_entry_id"] = couponId;
                                                        dr["Status"] = "Pending";
                                                        dt.Rows.Add(dr);
                                                    }
                                                    else
                                                    {
                                                        foreach (DataRow dr in dt.Rows)
                                                        {
                                                            dr["Type"] = "Tuition";
                                                            dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                                            dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                                            dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                                            dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                                            dr["Fee_amount"] = My.toDouble(txt_fee.Text).ToString("0.00");
                                                            dr["Status"] = "Pending";
                                                        }
                                                    }
                                                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                                    ad.Update(dt);
                                                    ViewState["issubmit"] = "1";
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }


                        //=====================================================
                        DataTable dtaD = payments.dataTable("select isnull(sum(convert(float, Discount_amt)),0) as Discount_amt,isnull(sum(convert(float, Total_amount)),0) as Total_amount from (select isnull(sum(convert(float, disc_amount)),0) as Discount_amt,isnull(sum(convert(float, Fee_amount)),0) as Total_amount from Discount_Master_temp where session='" + ViewState["session"].ToString() + "' and admission_no='" + txt_admission_no.Text + "' and Student_discount_type_entry_id='" + couponId + "') t", con);
                        if (dtaD.Rows.Count > 0)
                        {
                            if (My.toDouble(dtaD.Rows[0]["Discount_amt"].ToString()) > 0)
                            {
                                payments.exeSql("update Student_Payment_History set Is_coupon_applied='1' where Session='" + ViewState["session"].ToString() + "' and Addmission_no='" + txt_admission_no.Text + "'", con);
                                SqlCommand cmds;
                                string queryss = "INSERT INTO Coupon_applied_list (Coupon_id,Session_id,Class_id,Admission_no,Amount,Created_by,Created_date,Created_time,Created_idate,Total_amount,Discount_persent,Status) values (@Coupon_id,@Session_id,@Class_id,@Admission_no,@Amount,@Created_by,@Created_date,@Created_time,@Created_idate,@Total_amount,@Discount_persent,@Status)";
                                cmds = new SqlCommand(queryss);
                                cmds.Parameters.AddWithValue("@Coupon_id", couponId);
                                cmds.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                                cmds.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                                cmds.Parameters.AddWithValue("@Admission_no", lbl_admission_no.Text);
                                cmds.Parameters.AddWithValue("@Amount", dtaD.Rows[0]["Discount_amt"].ToString());
                                cmds.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                                cmds.Parameters.AddWithValue("@Created_date", mycode.date());
                                cmds.Parameters.AddWithValue("@Created_time", mycode.time());
                                cmds.Parameters.AddWithValue("@Created_idate", mycode.idate());
                                cmds.Parameters.AddWithValue("@Total_amount", dtaD.Rows[0]["Total_amount"].ToString());
                                cmds.Parameters.AddWithValue("@Discount_persent", My.Round((My.toDouble(dtaD.Rows[0]["Discount_amt"].ToString()) * 100) / My.toDouble(dtaD.Rows[0]["Total_amount"].ToString()), 2));
                                cmds.Parameters.AddWithValue("@Status", "Pending");
                                if (payments.InsertUpdateData(cmds, con))
                                {
                                }
                            }
                        }
                        flag = true;
                        con.Close();
                        scope.Complete();
                    }
                    if (flag == true)
                    {
                        Session["IsDiscountOpen"] = "0";
                        Session["msg"] = "The coupon discount has been saved and sent to the admin for approval.";
                        Response.Redirect("fees-collection-1.aspx?adm=" + txt_admission_no.Text + "&sessionid=" + ddlsessionad.SelectedValue, false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                    Alertme("Please enter the amount for which you would like to provide a discount.", "warning");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDiscountAlert();", true);
                Alertme("Please select the month during which you want to apply the discount.", "warning");
                return;
            }
        }



        private void save_bus_discount_temp(string couponId, SqlConnection con)
        {
            int mgrowcount = rp_month_for_discount.Items.Count;
            for (int ixi = 0; ixi < mgrowcount; ixi++)
            {
                CheckBox chkM = (CheckBox)rp_month_for_discount.Items[ixi].FindControl("chk_month_discount");
                if (chkM.Checked == true)
                {
                    Label lbl_month_id = (Label)rp_month_for_discount.Items[ixi].FindControl("lbl_month_id");
                    Label lbl_month_name = (Label)rp_month_for_discount.Items[ixi].FindControl("lbl_Month_discount");
                    #region #fff
                    double totla = 0;
                    for (int i = 0; i < rd_discount_fee_head.Items.Count; i++)
                    {
                        TextBox txt_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_head_fee");
                        TextBox txt_disc_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_disc_fee");
                        Label lbl_content_id = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content_id");
                        Label lbl_content = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content");
                        totla = totla + My.toDouble(txt_fee.Text);

                        string Month = lbl_month_name.Text;
                        //==============*************** 
                        if (lbl_content_id.Text == "1002") // FOR TRANSPORT
                        {
                            if (My.toDouble(txt_disc_fee.Text) > 0)
                            {
                                string discount_fee = txt_disc_fee.Text;
                                if (My.toDouble(txt_disc_fee.Text) > My.toDouble(txt_fee.Text))
                                {
                                    discount_fee = txt_fee.Text;
                                }

                                DataTable dtF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Paid'", con);
                                if (dtF.Rows.Count == 0)
                                {
                                    //CHECK IN TYPEWISE
                                    DataTable dtT = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Dues'", con);
                                    if (dtT.Rows.Count > 0)
                                    {
                                        if (My.toDouble(dtT.Rows[0]["dues"].ToString()) > 0)
                                        {
                                            double duesamts = (My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString()));
                                            if (My.toDouble(discount_fee) > duesamts)
                                            {
                                                discount_fee = duesamts.ToString();
                                            }
                                        }
                                    }

                                    //CHECK IN TYPEWISE 
                                    ViewState["discount_on"] = "TransportFee";
                                    SqlDataAdapter ad = new SqlDataAdapter("select * from Discount_Master_for_bus where Bus_path=" + ViewState["TransportPath_id"].ToString() + " and session_id='" + ViewState["sessionIDs"].ToString() + "' and month='" + lbl_month_name.Text + "' and admission_no='" + txt_admission_no.Text + "' and Class_id='" + ViewState["class_id"].ToString() + "' and Boarding_Point_id='" + ViewState["Boarding_Point_id"].ToString() + "'", con);
                                    DataSet ds = new DataSet();
                                    ad.Fill(ds, "Discount_Master_for_bus");
                                    DataTable dt = ds.Tables[0];
                                    if (dt.Rows.Count == 0)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Type"] = "Transport";
                                        dr["discount_for"] = "TransportFee";
                                        dr["Class_id"] = ViewState["class_id"].ToString();
                                        dr["Discount_on"] = ViewState["discount_on"].ToString();
                                        dr["session"] = ViewState["session"].ToString();
                                        dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                        dr["group_id"] = "51";
                                        dr["admission_no"] = txt_admission_no.Text;
                                        dr["month"] = lbl_month_name.Text;
                                        dr["fee_head_id"] = lbl_content_id.Text;
                                        dr["Fee_amount"] = My.toDouble(txt_fee.Text).ToString("0.00");
                                        dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                        dr["parameter_id"] = "0";
                                        dr["category_id"] = "0";
                                        dr["sub_category_id"] = "0";
                                        dr["Bus_path"] = ViewState["TransportPath_id"].ToString();
                                        dr["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                                        dr["session_id"] = ViewState["sessionIDs"].ToString();
                                        dr["Branch_id"] = ViewState["Branchid"].ToString();
                                        dr["User_id"] = ViewState["Userid"].ToString();
                                        dr["Date"] = mycode.date();
                                        dr["time"] = mycode.time();
                                        dr["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                                        dr["Transportation_Id"] = ViewState["Transport_id"].ToString();
                                        dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                        dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                        dr["Student_discount_type_entry_id"] = couponId;
                                        dt.Rows.Add(dr);
                                        DataTable dtFF = payments.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "'", con);
                                        if (dtFF.Rows.Count > 0)
                                        {
                                            double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                            payments.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "", con);
                                        }
                                        ViewState["issubmit"] = "1";
                                    }
                                    else
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            dr["Type"] = "Transport";
                                            dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                            dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                            dr["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                                            dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                            dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                            dr["Status"] = "Pending";
                                        }
                                        ViewState["issubmit"] = "1";
                                    }
                                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                    ad.Update(dt);
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
        }


        //====================================================
        //TRANSPORT
        #region Transport
        protected void ddl_bus_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
            if (ddl_bus_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus name", "warning");
            }
            else
            {
                fetch_path_root();
            }
        }

        private void fetch_path_root()
        {
            mycode.bind_all_ddl_with_id(ddl_path_root, "select Rootname,TransportationPath_id from TransportationPath where Transportation_Id=" + ddl_bus_name.SelectedValue + " order by Rootname asc");
        }

        protected void ddl_path_root_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
            if (ddl_bus_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus name", "warning");
            }
            else if (ddl_path_root.SelectedItem.Text == "Select")
            {
                Alertme("Please select transportation route", "warning");
            }
            else
            {
                fetch_boarding_point();
            }
        }





        private void fetch_boarding_point()
        {
            mycode.bind_all_ddl_with_id(ddl_boarding_point, " select  Boarding_Point,Boarding_Point_id from  Transportation_Boarding_Point where Transportation_Id=" + ddl_bus_name.SelectedValue + " and TransportationPath_id=" + ddl_path_root.SelectedValue + "  and Session_Id='" + ddlsessionad.SelectedValue + "' order by Boarding_Point");
        }



        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Edit_Bus"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
                if (ddl_monthname.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose month.", "warning");
                    ddl_monthname.Focus();
                }
                else if (ddl_bus_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose vehicle.", "warning");
                    ddl_bus_name.Focus();
                }
                else if (ddl_path_root.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose route.", "warning");
                    ddl_path_root.Focus();
                }
                else if (ddl_boarding_point.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose boarding point.", "warning");
                    ddl_boarding_point.Focus();
                }
                else
                {
                    save_boarding_data();
                }
            }
            else
            {
                Alertme("You are not authorized to do this work.", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
            }
        }

        private void save_boarding_data()
        {
            string cunrt_session = My.get_session();
            string session_frst_year = cunrt_session.Substring(0, 4);
            int session_s_year = My.toint(session_frst_year);
            int s_year = My.toint(session_frst_year);

            string monthid = My.tomonth_numberstring(ddl_monthname.SelectedItem.Text);
            int pay_month = My.toint(monthid);
            s_year = My.check_start_months(pay_month, s_year);
            string final = s_year + monthid;


            bool flag = false;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();

                SqlCommand cmd;
                string query = "update Student_mapping_with_TransportPath set Month_name=@Month_name,Month_id=@Month_id,TransportPath_id=@TransportPath_id,Created_by=@Created_by,Year_month=@Year_month,Change_date=@Change_date,Chnage_idate=@Chnage_idate,Change_by=@Change_by,Changed_type=@Changed_type,Old_Monthname=@Old_Monthname,Old_Transpotid=@Old_Transpotid,transport_id=@transport_id,Boarding_Point_id=@Boarding_Point_id where Id=@id; INSERT INTO Student_mapping_with_TransportPath_history (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,Remark,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Update_Status,Remove_Update_Month) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@Remark,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Update_Status,@Remove_Update_Month)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                cmd.Parameters.AddWithValue("@Admission_no", ViewState["Admission_no"].ToString());
                cmd.Parameters.AddWithValue("@Month_name", ddl_monthname.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Month_id", ddl_monthname.SelectedValue);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Remark", "Transport Changed");
                cmd.Parameters.AddWithValue("@branch_id", ViewState["Branchid"].ToString());
                cmd.Parameters.AddWithValue("@TransportPath_id", ddl_path_root.SelectedValue);

                cmd.Parameters.AddWithValue("@Year_month", final);
                cmd.Parameters.AddWithValue("@Id", ViewState["TransportRowId"].ToString());
                cmd.Parameters.AddWithValue("@Transport_Assigned_Id", ViewState["Transport_Assigned_Id"].ToString());

                cmd.Parameters.AddWithValue("@transport_id", ddl_bus_name.SelectedValue);
                cmd.Parameters.AddWithValue("@Boarding_Point_id", ddl_boarding_point.SelectedValue);


                cmd.Parameters.AddWithValue("@Change_date", mycode.date());
                cmd.Parameters.AddWithValue("@Chnage_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Change_by", ViewState["Userid"].ToString());

                if (rd_change_month_no.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Changed_type", "Month Changed");
                    cmd.Parameters.AddWithValue("@Update_Status", "Month Changed");
                }

                else if (rd_change_month_no.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Changed_type", "Transport Changed");
                    cmd.Parameters.AddWithValue("@Update_Status", "Transport Changed");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Changed_type", "Both Changed");
                    cmd.Parameters.AddWithValue("@Update_Status", "Both Changed");
                }
                cmd.Parameters.AddWithValue("@Old_Monthname", ViewState["Month_id"].ToString());
                cmd.Parameters.AddWithValue("@Old_Transpotid", ViewState["Transport_id"].ToString());
                cmd.Parameters.AddWithValue("@Remove_Update_Month", mycode.get_current_monthname());
                if (payments.InsertUpdateData(cmd, con))
                {
                    SqlCommand cmd1;
                    string query1 = "Update admission_registor set Transportation_Id=@Transportation_Id,Transportationpath=@Transportationpath,transportationtaken=@transportationtaken,hosteltaken=@hosteltaken where admissionserialnumber=@admissionserialnumber and Session_id=@Session_id ";
                    cmd1 = new SqlCommand(query1);
                    cmd1.Parameters.AddWithValue("@Transportation_Id", ddl_path_root.SelectedValue);
                    cmd1.Parameters.AddWithValue("@Transportationpath", ddl_path_root.SelectedItem.Text);
                    cmd1.Parameters.AddWithValue("@transportationtaken", "Yes");
                    cmd1.Parameters.AddWithValue("@hosteltaken", "No");
                    cmd1.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                    cmd1.Parameters.AddWithValue("@admissionserialnumber", ViewState["Admission_no"].ToString());
                    if (payments.InsertUpdateData(cmd1, con))
                    {
                        // chek if dues  
                        DataTable dt = payments.dataTable("Select top 1 * from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='MonthlyFee' and status='Dues' and month='" + ddl_monthname.SelectedItem.Text + "'", con);
                        if (dt.Rows.Count != 0)
                        {
                            string class1 = dt.Rows[0]["class"].ToString();
                            string section = dt.Rows[0]["section"].ToString();
                            string feetype = "TransportationFee";
                            string month = ddl_monthname.SelectedItem.Text;
                            string user_id = ViewState["Userid"].ToString();
                            string group_id = "3";
                            string position = dt.Rows[0]["position"].ToString();
                            string class_id = dt.Rows[0]["class_id"].ToString();
                            string branchid = ViewState["Branchid"].ToString();
                            DataTable feedt = payments.dataTable(@"select 'TransportationFee' as content,tr.parameter_id as content_id,tr.Amount as amount,'Monthelyfee',tr.Ledger,
                
                
                (isnull((select top 1 disc_amount from dbo.[Discount_Master_for_bus] where admission_no='" + ViewState["Admission_no"].ToString() + "' and month='" + ddl_monthname.SelectedItem.Text + "' and session_id='" + ViewState["sessionIDs"].ToString() + "' and Bus_path='" + ddl_path_root.SelectedValue + "'),(select top 1 disc_amount from dbo.[Discount_Master_for_bus] where  admission_no='All' and month='" + ddl_monthname.SelectedItem.Text + "' and session_id='" + ViewState["sessionIDs"].ToString() + "' and Bus_path='" + ddl_path_root.SelectedValue + "'))) disc_amount from admission_registor ar join Transportation_Fee_Master tr on ar.Transportation_Id=tr.Transportation_path_id and ar.transportationtaken='yes' and ar.Session_id='" + ViewState["sessionIDs"].ToString() + "' and admissionserialnumber='" + ViewState["Admission_no"].ToString() + "' and tr.Month='" + ddl_monthname.SelectedItem.Text + "'  join Student_mapping_with_TransportPath smt on  ar.Session_id=smt.Session_id and ar.admissionserialnumber=smt.Admission_no and smt.Year_month<=" + s_year + monthid + "", con);
                            if (feedt.Rows.Count != 0)
                            {
                                foreach (DataRow dr in feedt.Rows)
                                {
                                    string TransportationFeetype = dr["content"].ToString();
                                    string content_id1 = dr["content_id"].ToString();
                                    string Amount = dr["amount"].ToString();
                                    string disc_amount = dr["disc_amount"].ToString();
                                    double afterdicount = My.toDouble(Amount) - My.toDouble(disc_amount);
                                    string get_onemonthback = mycode.get_one_monthback(monthid);

                                    payments.exeSql("delete from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='MonthlyFee' and feetype='TransportationFee' and month='" + get_onemonthback + "'", con);
                                    payments.exeSql(@"insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,
Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,group_id,class_id,position,Payable_after_disc) 
values ('" + ViewState["Admission_no"].ToString() + "','" + class1 + "','" + ViewState["session"].ToString() + "','MonthlyFee','" + mycode.date() + "','" + mycode.idate() + "','" + TransportationFeetype + "','" + My.toDouble(Amount).ToString("0.00") + "','0','" + My.toDouble(afterdicount).ToString("0.00") + "','Dues','" + ddl_monthname.SelectedItem.Text + "','" + content_id1 + "','','School','false','false','false','" + My.toDouble(disc_amount).ToString("0.00") + "','" + section + "','" + ViewState["Userid"].ToString() + "','" + ViewState["Branchid"].ToString() + "','" + group_id + "','" + class_id + "','" + position + "','" + afterdicount.ToString("0.00") + "')", con);
                                }
                            }
                        }
                        else
                        {
                            string get_onemonthback = mycode.get_one_monthback(monthid);
                            payments.exeSql("delete from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='MonthlyFee' and feetype='TransportationFee' and month='" + get_onemonthback + "'", con);
                        }
                    }
                }


                dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                flag = true;
                con.Close();
                scope.Complete();
            }
            if (flag == true)
            {
                Session["msg"] = "Transportation has been successfully updated.";
                Response.Redirect("fees-collection-1.aspx?adm=" + ViewState["Admission_no"].ToString() + "&sessionid=" + ddlsessionad.SelectedValue, false);
            }
        }



        protected void btn_remove_transport_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete_Bus"].ToString() == "1")
                {
                    if (ddl_transport_remove_month.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select the month from which student would like to discontinue the school transport.", "warning");
                        ddl_transport_remove_month.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
                        return;
                    }
                    if (txt_transport_remove_remark.Text == "")
                    {
                        Alertme("Please enter remarks.", "warning");
                        txt_transport_remove_remark.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
                        return;
                    }
                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();

                        save_transport_dues(ddl_transport_remove_month.SelectedItem.Text, ddl_transport_remove_month.SelectedValue, con);

                        string qry = "delete from Student_mapping_with_TransportPath where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["Admission_no"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "'";
                        payments.exeSql(qry, con);
                        string qry2 = "update Transport_Path_Mapping_With_Sheet set Sheet_Status=0 where TransportationPath_id=" + ViewState["TransportPath_id"].ToString() + " and Transportation_Id=" + ViewState["Transport_id"].ToString() + " and Sheet_Id=" + ViewState["Sheet_Id"].ToString() + " ";
                        payments.exeSql(qry2, con);
                        SqlCommand cmd1;
                        string query1 = "Update admission_registor set Transportation_Id=@Transportation_Id,Transportationpath=@Transportationpath,transportationtaken=@transportationtaken where admissionserialnumber=@admissionserialnumber and Session_id=@Session_id and Class_id=@Class_id ";
                        cmd1 = new SqlCommand(query1);
                        cmd1.Parameters.AddWithValue("@Transportation_Id", "0");
                        cmd1.Parameters.AddWithValue("@Transportationpath", "");
                        cmd1.Parameters.AddWithValue("@transportationtaken", "No");
                        cmd1.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                        cmd1.Parameters.AddWithValue("@admissionserialnumber", ViewState["Admission_no"].ToString());
                        cmd1.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                        if (payments.InsertUpdateData(cmd1, con))
                        {
                            string desc = "Bus removed for name : " + lbl_name.Text;
                            payments.delete_log(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admission_no.Text, "RemoveTransport", desc, "fees-collection-1.aspx", ViewState["Userid"].ToString(), con);
                            string query2 = "update Student_mapping_with_TransportPath_history set Removed_date=@Removed_date,Removed_Idate=@Removed_Idate,Update_Status=@Update_Status,Remove_Update_Month=@Remove_Update_Month,Remarks=@Remarks where Transport_Assigned_Id=@Transport_Assigned_Id and TransportPath_id=@TransportPath_id and Admission_no=@Admission_no and Session_id=@Session_id";
                            cmd1 = new SqlCommand(query2);
                            cmd1.Parameters.AddWithValue("@Removed_date", mycode.date());
                            cmd1.Parameters.AddWithValue("@Removed_Idate", mycode.idate());
                            cmd1.Parameters.AddWithValue("@Update_Status", "Transport Removed");
                            cmd1.Parameters.AddWithValue("@Remove_Update_Month", mycode.get_current_monthname());
                            cmd1.Parameters.AddWithValue("@Remarks", txt_transport_remove_remark.Text);
                            cmd1.Parameters.AddWithValue("@TransportPath_id", ViewState["TransportPath_id"].ToString());
                            cmd1.Parameters.AddWithValue("@Transport_Assigned_Id", ViewState["Transport_Assigned_Id"].ToString());
                            cmd1.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                            cmd1.Parameters.AddWithValue("@Admission_no", ViewState["Admission_no"].ToString());
                            if (payments.InsertUpdateData(cmd1, con))
                            {
                            }
                        }
                        dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                        flag = true;
                        con.Close();
                        scope.Complete();
                    }
                    if (flag == true)
                    {
                        Session["msg"] = "Transport has been successfully removed from this student.";
                        Response.Redirect("fees-collection-1.aspx?adm=" + ViewState["Admission_no"].ToString() + "&sessionid=" + ddlsessionad.SelectedValue, false);
                    }
                }
                else
                {
                    Alertme("You are not authorized to do this work.", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void btn_transport_mapping_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add_Bus"].ToString() == "1")
                {
                    if (ddl_trns_vehicle.SelectedItem.Text == "Select")
                    {
                        Alertme("Please choose vehicle.", "warning");
                        ddl_trns_vehicle.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
                    }
                    else if (ddl_trns_route.SelectedItem.Text == "Select")
                    {
                        Alertme("Please choose transport route.", "warning");
                        ddl_trns_route.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
                    }
                    else if (ddl_boarding_point_map.SelectedItem.Text == "Select")
                    {
                        Alertme("Please choose transport boarding point.", "warning");
                        ddl_boarding_point_map.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
                    }
                    else
                    {
                        save_transport_mapping();
                    }
                }
                else
                {
                    Alertme("You are not authorized to do this work.", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void save_transport_mapping()
        {
            string session = ddlsessionad.SelectedItem.Text;
            string tranportassinedid = My.get_transport_assigned_id();
            string cunrt_session = session;
            string session_frst_year = cunrt_session.Substring(0, 4);
            int s_year = My.toint(session_frst_year);
            string monthid = My.tomonth_numberstring(ddl_tr_month.SelectedItem.Text);
            int pay_month = My.toint(monthid);
            int s_yearS = My.check_start_months(pay_month, s_year);

            string final = s_yearS.ToString() + monthid;


            bool flag = false;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();

                DataTable dt = payments.dataTable("select * from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id='" + ddl_trns_route.SelectedValue + "' and Transportation_Id='" + ddl_trns_vehicle.SelectedValue + "' and Sheet_Status='0'", con);
                if (dt.Rows.Count > 0)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Student_mapping_with_TransportPath (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,transport_id,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Academic_Sem_or_Year_id,Mapping_Year,Sheet_Id,Boarding_Point_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@transport_id,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Academic_Sem_or_Year_id,@Mapping_Year,@Sheet_Id,@Boarding_Point_id); INSERT INTO Student_mapping_with_TransportPath_history (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,transport_id,Remark,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Update_Status,Academic_Sem_or_Year_id,Mapping_Year,Sheet_Id,Boarding_Point_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@transport_id,@Remark,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Update_Status,@Academic_Sem_or_Year_id,@Mapping_Year,@Sheet_Id,@Boarding_Point_id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                    cmd.Parameters.AddWithValue("@Admission_no", ViewState["Admission_no"].ToString());
                    cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                    cmd.Parameters.AddWithValue("@Month_name", ddl_tr_month.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Month_id", monthid);
                    cmd.Parameters.AddWithValue("@TransportPath_id", ddl_trns_route.SelectedValue);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@branch_id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Year_month", final);
                    cmd.Parameters.AddWithValue("@Transport_Assigned_Id", tranportassinedid);
                    cmd.Parameters.AddWithValue("@Academic_Sem_or_Year_id", "0");
                    cmd.Parameters.AddWithValue("@Mapping_Year", s_yearS);
                    cmd.Parameters.AddWithValue("@Sheet_Id", dt.Rows[0]["Sheet_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Remark", "Transport assigned");
                    cmd.Parameters.AddWithValue("@Update_Status", "Assigned");
                    cmd.Parameters.AddWithValue("@transport_id", ddl_trns_vehicle.SelectedValue);
                    cmd.Parameters.AddWithValue("@Boarding_Point_id", ddl_boarding_point_map.SelectedValue);
                    if (payments.InsertUpdateData(cmd, con))
                    {
                        try
                        {
                            SqlCommand cmd1;
                            string query1 = "Update admission_registor set Hostel_id=@Hostel_id,Transportation_Id=@Transportation_Id,Transportationpath=@Transportationpath,transportationtaken=@transportationtaken,hosteltaken=@hosteltaken where admissionserialnumber=@admissionserialnumber and Session_id=@Session_id and Class_id=" + ViewState["classid"].ToString() + " ";
                            cmd1 = new SqlCommand(query1);
                            cmd1.Parameters.AddWithValue("@Transportation_Id", ddl_trns_vehicle.SelectedValue);
                            cmd1.Parameters.AddWithValue("@Transportationpath", ddl_boarding_point_map.SelectedValue);
                            cmd1.Parameters.AddWithValue("@transportationtaken", "Yes");
                            cmd1.Parameters.AddWithValue("@hosteltaken", "No");
                            cmd1.Parameters.AddWithValue("@Hostel_id", "0");
                            cmd1.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                            cmd1.Parameters.AddWithValue("@admissionserialnumber", ViewState["Admission_no"].ToString());
                            if (payments.InsertUpdateData(cmd1, con))
                            {

                                DataTable dtM = payments.dataTable("select * from Month_Index where position>='" + ddl_tr_month.SelectedValue + "'", con);
                                if (dtM.Rows.Count > 0)
                                {
                                    foreach (DataRow drM in dtM.Rows)
                                    {
                                        DataTable dtTR = payments.dataTable("Select distinct month,class,position from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter ='MonthlyFee' and class_id=" + ViewState["classid"].ToString() + " and month='" + drM["Month"].ToString() + "' order by position asc", con);//
                                        if (dtTR.Rows.Count > 0)
                                        {
                                            DataTable dtTT = payments.dataTable("select * from Transportation_Boarding_Point where Session_Id='" + ddlsession.SelectedValue + "' and Transportation_Id='" + ddl_trns_vehicle.SelectedValue + "' and TransportationPath_id='" + ddl_trns_route.SelectedValue + "' and Boarding_Point_id='" + ddl_boarding_point_map.SelectedValue + "'", con);
                                            foreach (DataRow drtr in dtTR.Rows)
                                            {
                                                string class1 = drtr["class"].ToString();
                                                string section = ViewState["Section"].ToString();
                                                string feetype = "TransportationFee";
                                                string month = drtr["month"].ToString();
                                                string user_id = ViewState["Userid"].ToString();
                                                string group_id = "3";
                                                string position = drtr["position"].ToString();
                                                string class_id = ViewState["classid"].ToString();
                                                string branchid = ViewState["Branchid"].ToString();


                                                string TransportationFeetype = "TransportationFee";
                                                string content_id1 = "1002";
                                                string Amount = dtTT.Rows[0]["Amount"].ToString();
                                                string disc_amount = "0.00";
                                                double afterdicount = My.toDouble(Amount) - My.toDouble(disc_amount);

                                                payments.exeSql("delete from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='MonthlyFee' and feetype='TransportationFee' and month='" + month + "' and class_id='" + class_id + "'", con);
                                                payments.exeSql(@"insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,
Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,group_id,class_id,position,Payable_after_disc) 
values ('" + ViewState["Admission_no"].ToString() + "','" + class1 + "','" + ViewState["session"].ToString() + "','MonthlyFee','" + mycode.date() + "','" + mycode.idate() + "','" + TransportationFeetype + "','" + My.toDouble(Amount).ToString("0.00") + "','0','" + My.toDouble(afterdicount).ToString("0.00") + "','Dues','" + month + "','" + content_id1 + "','00','School','false','false','false','" + My.toDouble(disc_amount).ToString("0.00") + "','" + section + "','" + ViewState["Userid"].ToString() + "','" + ViewState["Branchid"].ToString() + "','" + group_id + "','" + class_id + "','" + position + "','" + afterdicount.ToString("0.00") + "')", con);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        catch
                        {
                        }
                    }
                }
                dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["class_id"].ToString(), lbl_admission_no.Text, "0", "0", con);
                flag = true;
                con.Close();
                scope.Complete();
            }

            if (flag == true)
            {
                Session["msg"] = "Transport has been mapped with student successfully.";
                Response.Redirect("fees-collection-1.aspx?adm=" + ViewState["Admission_no"].ToString() + "&sessionid=" + ddlsessionad.SelectedValue, false);
            }
        }

        private void fetch_transport_info()
        {
            mycode.bind_all_ddl_with_id_notselect(ddl_tr_month, "select Month,Position from Month_Index order by Position asc");
            mycode.bind_all_ddl_with_id(ddl_trns_vehicle, "select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");


            DataTable dtTransport = My.dataTable("select top 1 * from Transportation_Boarding_Point where Session_Id='" + ViewState["sessionIDs"].ToString() + "' and TransportationPath_id in(select TransportationPath_id from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id=Transportation_Boarding_Point.TransportationPath_id and Transportation_Id=Transportation_Boarding_Point.Transportation_Id and Sheet_Status='0') order by Boarding_Point");
            if (dtTransport.Rows.Count > 0)
            {
                try
                {
                    ddl_trns_vehicle.SelectedValue = dtTransport.Rows[0]["Transportation_Id"].ToString();
                    fetch_transport_route("1", dtTransport.Rows[0]["TransportationPath_id"].ToString(), dtTransport.Rows[0]["Boarding_Point_id"].ToString());
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void fetch_transport_route(string from, string TransportationPath_id, string barding_point)
        {
            mycode.bind_all_ddl_with_id(ddl_trns_route, "select Rootname,TransportationPath_id from TransportationPath where Transportation_Id=" + ddl_trns_vehicle.SelectedValue + " order by Rootname asc");
            try
            {
                if (from == "1")
                {
                    try
                    {
                        ddl_trns_route.SelectedValue = TransportationPath_id;
                        fetch_boarding_point(from, TransportationPath_id);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_boarding_point(string from, string transportationPath_id)
        {
            mycode.bind_all_ddl_with_id(ddl_boarding_point_map, " select Boarding_Point,Boarding_Point_id from  Transportation_Boarding_Point where Transportation_Id=" + ddl_trns_vehicle.SelectedValue + " and TransportationPath_id=" + ddl_trns_route.SelectedValue + "  and Session_Id='" + ViewState["sessionIDs"].ToString() + "' and TransportationPath_id in(select TransportationPath_id from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id=Transportation_Boarding_Point.TransportationPath_id and Transportation_Id=Transportation_Boarding_Point.Transportation_Id and Sheet_Status='0') order by Boarding_Point");
        }
        protected void ddl_trns_vehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
                if (ddl_trns_vehicle.SelectedItem.Text == "Select")
                {
                    ddl_trns_vehicle.Focus();
                    Alertme("Please select vehicle", "warning");
                }
                else
                {
                    ddl_trns_route.Focus();
                    mycode.bind_all_ddl_with_id(ddl_trns_route, " select  Rootname,TransportationPath_id from  TransportationPath where Transportation_Id=" + ddl_trns_vehicle.SelectedValue + " order by Rootname asc");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_trns_route_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openTransport();", true);
                if (ddl_trns_route.SelectedItem.Text == "Select")
                {
                    ddl_trns_route.Focus();
                    Alertme("Please select route", "warning");
                }
                else
                {
                    ddl_boarding_point_map.Focus();
                    mycode.bind_all_ddl_with_id(ddl_boarding_point_map, "select Boarding_Point,Boarding_Point_id from Transportation_Boarding_Point where Transportation_Id=" + ddl_trns_vehicle.SelectedValue + " and TransportationPath_id=" + ddl_trns_route.SelectedValue + "  and Session_Id='" + ViewState["sessionIDs"].ToString() + "' and TransportationPath_id in(select TransportationPath_id from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id=Transportation_Boarding_Point.TransportationPath_id and Transportation_Id=Transportation_Boarding_Point.Transportation_Id and Sheet_Status='0') order by Boarding_Point");
                }
            }
            catch (Exception ex)
            {
            }
        }

        //=====================================TeansporTDueS
        private void save_transport_dues(string monthName, string month_id, SqlConnection con)
        {
            payments.exeSql("delete from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and status='Dues' and month in (select month from ( Select sum(cast(paid as float)) paid,month from dbo.[Typewise_fee_collection] where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' group by month) t where  paid=0 )", con);

            //payments.exeSql("delete from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='MonthlyFee' and feetype='TransportationFee' and class_id='" + ViewState["classid"].ToString() + "' and convert(float, paid)=0 and status='Dues'", con);
            string month_position = payments.get_single_column_data("select Position as Column_Name from Month_Index where Month_Id='" + month_id + "'", con);
            DataTable dtm = payments.dataTable("select * from Month_Index where Position<" + month_position + " order by Position asc", con);
            if (dtm.Rows.Count > 0)
            {
                foreach (DataRow dr in dtm.Rows)
                {
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        int mnthids = My.tomonth_number(dr["Month"].ToString());
                        if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                        {
                            ViewState["parameteridS"] = "44";
                        }
                        else
                        {
                            ViewState["parameteridS"] = "4";
                        }
                    }

                    if (payments.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + dr["Month"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' and transection!=''", con).Rows.Count > 0)
                    {
                        continue;
                    }
                    else
                    {
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = txt_admission_no.Text;
                        dc["session_id"] = ViewState["sessionIDs"].ToString();
                        dc["class"] = lblclass.Text;
                        dc["session"] = ViewState["session"].ToString();
                        dc["class_id"] = ViewState["classid"].ToString();
                        dc["hosteltaken"] = lblhostel.Text;
                        dc["months"] = dr["Month"].ToString();
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                        dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                        dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();

                        dc["category_id"] = ViewState["category_id"].ToString();
                        dc["sub_category_id"] = ViewState["sub_category_id"].ToString();
                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();

                        dc["parameter_id"] = ViewState["parameteridS"].ToString();
                        //new08/08/2022

                        string cunrt_session = ViewState["session"].ToString();
                        string session_frst_year = cunrt_session.Substring(0, 4);
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);
                        string monthid = My.tomonth_numberstring(dr["Month"].ToString());
                        int pay_month = My.toint(monthid);
                        s_year = payments.check_start_months(pay_month, s_year, con);
                        dc["monthid"] = s_year + monthid;
                        DataTable feedt = payments.dataTableSP("sp_fetch_monthly_fee", dc, con);
                        send_in_typewise_fee_dues(feedt, dr["Month"].ToString(), "HS00221", ViewState["classid"].ToString(), dr["Position"].ToString(), con);
                    }
                }
            }


            //===============================
            DataTable dtmm = payments.dataTable("select * from Month_Index where Position>=" + month_position + " order by Position asc", con);
            if (dtmm.Rows.Count > 0)
            {
                foreach (DataRow drm in dtmm.Rows)
                {
                    payments.exeSql("delete from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='MonthlyFee' and feetype='TransportationFee' and class_id='" + ViewState["classid"].ToString() + "' and month='" + drm["Month"].ToString() + "' and convert(float, paid)=0 and status='Dues'", con);
                }
            }
        }

        private void bind_mapped_transport()
        {
            try
            {
                mycode.bind_all_ddl_with_id(ddl_bus_name, "select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");
                btn_Submit.Text = "Update";
                ddl_bus_name.SelectedValue = ViewState["Transport_id"].ToString();
                fetch_path_root();
                ddl_path_root.SelectedValue = ViewState["TransportPath_id"].ToString();
                fetch_boarding_point();
                ddl_boarding_point.SelectedValue = ViewState["Boarding_Point_id"].ToString();
                ddl_monthname.SelectedValue = ViewState["Month_id"].ToString();

                mycode.bind_all_ddl_with_id(ddl_transport_remove_month, "select Month,Month_Id from Month_Index order by Position asc");
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}