using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Monthly_Payments
{
    public partial class Monthly_payment_student : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {



                    ViewState["Isadmissionfeetekent"] = My.get_admission_condition();
                    ViewState["Userid"] = "Self";
                    // ViewState["firm_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    // ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    ViewState["fineAmt"] = "0";
                    ViewState["checked_mnth"] = "0";
                    ViewState["flags1"] = "0";
                    ViewState["fine_inserted"] = "0";
                    ViewState["Other_Fees"] = "0";
                    ViewState["late_fine_no_of_day_month"] = "0";
                    ViewState["fine_date_From"] = "0";
                    ViewState["fine_date_To"] = "0";
                    ViewState["FineType"] = "0";
                    ViewState["month"] = "";
                    txt_payment_date.Text = mycode.date();

                    mycode.bind_all_ddl_with_id(ddlsessionad, "Select Session,session_id from session_details");

                    ddlsessionad.SelectedValue = My.get_session_id();

                }

            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
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
        public static List<string> GetRooPath(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select studentname from admission_registor where studentname LIKE ''+@SearchMobNo+'%' and Session_id='" + My.get_session_id() + "' and Status='1'";
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

        public static List<string> GetRooPathAdmNo(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select admissionserialnumber from admission_registor where admissionserialnumber LIKE ''+@SearchMobNo+'%' and Session_id='" + My.get_session_id() + "' and Status='1'";
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


                if (txt_fathermobileno.Text == "")
                {
                    Alertme("Enter Father Mobile No.", "warning");
                }
                else if (txt_admission_no.Text == "")
                {
                    Alertme("Enter Student Admission No.", "warning");
                }
                else
                {

                    //empty_form();
                    string query = "";
                    if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                    {
                        query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and father_mob='"+txt_fathermobileno.Text+"' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' order by id desc";
                    }
                    else
                    {

                        query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and father_mob='" + txt_fathermobileno.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' and Status='1' order by id desc";
                    }



                    find_details(query);
                }
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
                    ViewState["Session"] = dr["session"].ToString();
                    ViewState["Class_id"] = dr["Class_id"].ToString();

                    ViewState["Class_name"] = mycode.get_classname(ViewState["Class_id"].ToString());
                    ViewState["Session_id"] = dr["Session_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["Userid"] = dr["admissionserialnumber"].ToString();
                    ViewState["regid"] = dr["admissionserialnumber"].ToString();
                    ViewState["firm_id"] = "1";
                    ViewState["Branchid"] = dr["Branch_id"].ToString();
                    ViewState["studentname"] = dr["studentname"].ToString();
                    ViewState["rollnumber"] = dr["rollnumber"].ToString();




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

                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();

                    if (ViewState["Transfer_Status"].ToString() == "New")
                    {
                        lbl_studentype.Text = "New";
                    }
                    else
                    {
                        lbl_studentype.Text = "Old";
                    }

                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
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



                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["Session"] = dr["session"].ToString();
                    ViewState["email_id"] = dr["email_id"].ToString();
                    ViewState["Session_id"] = dr["Session_id"].ToString();
                    ViewState["rollnumber"] = dr["rollnumber"].ToString();
                    ViewState["studentname"] = dr["studentname"].ToString();
                    ViewState["fathername"] = dr["fathername"].ToString();
                    ViewState["class"] = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    ViewState["hosteltaken"] = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                    ViewState["transportationtaken"] = dr["transportationtaken"].ToString();
                    ViewState["mobilenumber"] = dr["father_mob"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();

                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else if (dr["Transportation_Id"].ToString() == "&nbsp;")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
                    }
                    if (dr["Section"].ToString() == "")
                    {

                        ViewState["Section"] = "A";
                    }
                    else if (dr["Section"].ToString() == "&nbsp;")
                    {

                        ViewState["Section"] = "A";
                    }
                    else
                    {
                        ViewState["Section"] = dr["Section"].ToString();
                    }









                }


                find_all_due_fee();
                find_all_paid_fee();
            }
        }

        private void find_all_paid_fee()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 session_id from session_details where Session=Student_Payment_History.Session) as session_id from Student_Payment_History  where Session='" + ViewState["Session"].ToString() + "' and Addmission_no='" + ViewState["regid"].ToString() + "' and Type='Monthly'   ORDER BY id ASC");
            if (dt.Rows.Count == 0)
            {
                lbl_msg.Text = " There are no any payment history";
                grid_payment_history.DataSource = null;
                grid_payment_history.DataBind();
            }
            else
            {
                lbl_msg.Text = "";
                grid_payment_history.DataSource = dt;
                grid_payment_history.DataBind();
            }
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
            //  lst.Add(temp_month);



            for (temp = 1; temp <= 12; temp++)
            {
                DataTable paid_dt = My.dataTable(" select month,status from dbo.[Typewise_fee_collection] where   session='" + ViewState["Session"].ToString() + "' and admission_no='" + txt_admission_no.Text + "' and parameter like '%" + ViewState["parameter"].ToString() + "%' and month='" + temp_month + "'");
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
                    lst.Add(temp_month);
                }
                temp_month = find_month(temp_month);
            }

            int i = 0;
            foreach (string str in lst)
            {
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["Month"] = lst[i];
                drNewRow["value"] = false;
                //   drNewRow["discount_per"] = find_discount(lst[i].ToString(), txt_admission_no.Text, ddlsession.Text, class_id);
                drNewRow["paid_status"] = "NotCreated";
                dtDatas.Rows.Add(drNewRow);
                dtDatas.AcceptChanges();
                i++;
            }
            find_prev_dues(dtDatas);
            GridView2.DataSource = dtDatas.DefaultView;
            GridView2.DataBind();

        }



        DataTable prevdues_dt = new DataTable();
        private void find_prev_dues(DataTable dtDatas)
        {
            prevdues_dt = My.dataTable(" select sum(isnull(cast(dues as float),'0')) dues,month from dbo.[Typewise_fee_collection] where session='" + ViewState["Session"].ToString() + "' and status='Dues' and Class='" + ViewState["Class_name"].ToString() + "' and admission_no='" + txt_admission_no.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' group by month");
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
                CheckBox lnk = (CheckBox)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                int rowindex = row.RowIndex;
                try
                {
                    CheckBox chk_month = (CheckBox)GridView2.Rows[rowindex + 1].FindControl("chk_month");
                    chk_month.Enabled = true;
                }
                catch { }
                Label lbl_Month = (Label)row.FindControl("lbl_Month");
                CheckBox chk = (CheckBox)row.FindControl("chk_month");
                if (chk.Checked == true)
                {
                    string month1 = lbl_Month.Text;
                    if (ViewState["month"].ToString() == "")
                    {
                        ViewState["month"] = "'" + month1 + "'";


                    }
                    else
                    {
                        ViewState["month"] = ViewState["month"] + "," + "'" + month1 + "'";

                    }

                    chk.Checked = true;
                    chk.Enabled = false;
                    string month = lbl_Month.Text;

                    bind_monthly_fee();
                    ViewState["MnthName"] = lbl_Month.Text;
                    fine_calculation(lbl_Month.Text, "1");

                }
                else
                {
                    bind_monthly_fee();
                    ViewState["MnthName"] = lbl_Month.Text;
                    fine_calculation(lbl_Month.Text, "1");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
        private void bind_monthly_fee()
        {
            double fee_amount = 0, disc_amount = 0, paid_previously = 0;
            bool success = false;
            DataTable fdt = new DataTable();
            int growcountS = GridView2.Rows.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Month = (Label)GridView2.Rows[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)GridView2.Rows[iS].FindControl("chk_month");



                fee_amount = 0; disc_amount = 0; paid_previously = 0;
                DataTable feedt = new DataTable();
                if (My.toBool(chk_month.Checked))
                {
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        int mnthids = My.tomonth_number(lbl_Month.Text);
                        if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
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
                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                    {
                        feedt = My.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter,      feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsessionad.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' and content_id!='6121' ");
                        type = "Calculated";
                    }
                    else
                    {
                        //dr["bac_colour"] = "White";
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = txt_admission_no.Text;
                        dc["session_id"] = My.get_session_id();
                        dc["class"] = lblclass.Text;
                        dc["session"] = My.get_session();
                        dc["class_id"] = ViewState["classid"].ToString();
                        dc["hosteltaken"] = lblhostel.Text;
                        dc["months"] = lbl_Month.Text;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = lblhostel.Text == "yes" ? My.toint(ViewState["hostel_id"].ToString()) : 0;
                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();
                        dc["category_id"] = ViewState["category_id"].ToString();
                        dc["sub_category_id"] = ViewState["sub_category_id"].ToString();
                        dc["transportportation_id"] = ViewState["transportID"].ToString();
                        dc["parameter_id"] = ViewState["parameteridS"].ToString();

                        //new08/08/2022

                        string cunrt_session = My.get_session();
                        string session_frst_year = cunrt_session.Substring(0, 4);
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);

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
                        if (type == "Calculated")
                        {
                            My.exeSql("update dbo.[Typewise_fee_collection] set Disc='" + dr["disc_amount"].ToString() + "' where Id='" + dr["id"].ToString() + "'");
                        }
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
                btn_save_payment.Visible = true;
            }
            else
            {
                pnl_month_wise_fee_details.Visible = false;
                btn_save_payment.Visible = false;
                lbl_fee_amount.Text = "0";
                lbl_discount.Text = "0";
                lbl_paid_prev.Text = "0";
                lbl_total.Text = "0";

                txttotal.Text = "0";
                txt_paid_prev.Text = "0";
                txt_discount.Text = "0";
                txttotalbill.Text = "0";
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

            txttotalbill.Text = (totalpblE + My.toDouble(ViewState["fineAmt"].ToString()) + My.toDouble(txt_other_fee.Text)).ToString();

            txt_paid_amount.Text = txttotalbill.Text;

            txt_total_dues.Text = "0.00";
            ViewState["total"] = totalAmt.ToString();
            ViewState["paid_prev"] = totalPrepAid.ToString();
            ViewState["discount"] = totaldisc.ToString();
            ViewState["totalbill"] = My.toDouble(txttotalbill.Text).ToString("0.00");




        }
        private void fine_calculation(string monthName, string from)
        {
            if (from == "1")
            {
                int mnth_ids = My.tomonth_number(monthName);
                string month_id_in_two_dgt = My.getMonthS_twoDigit(mnth_ids.ToString());
                if (ViewState["checked_mnth"].ToString() == "0")
                {
                    ViewState["checked_frst_mnth"] = month_id_in_two_dgt;
                    ViewState["checked_after_frst_mnth"] = month_id_in_two_dgt;
                    ViewState["checked_mnth"] = "1";
                }
                else
                {
                    ViewState["checked_after_frst_mnth"] = month_id_in_two_dgt;
                }
            }

            DataTable dty = mycode.FillData("select Fine_mode from globle_data where Fine_mode='2'");
            if (dty.Rows.Count != 0)
            {
                string pay_date = txt_payment_date.Text;
                int payidate = My.DateConvertToIdate(pay_date);



                #region DayRanGEWise

                //Advance Payment Check
                string crnt_year = mycode.year();
                string cunrt_session = My.get_session();
                string session_frst_year = cunrt_session.Substring(0, 4);
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);


                int pay_month = My.toint(ViewState["checked_after_frst_mnth"].ToString());
                s_year = My.check_start_months(pay_month, s_year);
                int pay_month_with_year = My.toint(s_year + ViewState["checked_after_frst_mnth"].ToString());
                int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                //Advance Payment Check

                if (crnt_month_with_year >= pay_month_with_year)
                {
                    DataTable dtz = mycode.FillData("select top 1 * from Fine_master_day_range");
                    if (dtz.Rows.Count != 0)
                    {
                        ViewState["FineType"] = "DayWise";
                        string applicable_month = "05";
                        string last_day_of_payment = dtz.Rows[0]["No_of_day"].ToString() + "/" + applicable_month + "/" + session_s_year;

                        string last_day_of_payments = "01" + "/" + ViewState["checked_frst_mnth"].ToString() + "/" + session_s_year;


                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);

                        ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                        ViewState["late_fine_no_of_day_month"] = totaldays;
                        ViewState["fine_date_From"] = last_day_of_payment;
                        ViewState["fine_date_To"] = pay_date;


                        if (chk_latefineapplay.Checked != true)
                        {
                            ViewState["fineAmt"] = "0.00";
                            bind_ttl_fee();
                            return;
                        }


                        DataTable dt_fine = mycode.FillData("select top 1 * from Fine_master_day_range where No_of_day>" + totaldays + " order by No_of_day asc");
                        if (dt_fine.Rows.Count != 0)
                        {
                            ViewState["fineAmt"] = My.toDouble(dt_fine.Rows[0]["Fine_amount"].ToString()).ToString("0.00");
                            bind_ttl_fee();
                        }
                        else
                        {
                            DataTable dt_fines = mycode.FillData("select top 1 * from Fine_master_day_range order by No_of_day desc");
                            if (dt_fines.Rows.Count != 0)
                            {
                                ViewState["fineAmt"] = My.toDouble(dt_fines.Rows[0]["Fine_amount"].ToString()).ToString("0.00");
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
                #endregion

            }
            else
            {

                DataTable dt = mycode.FillData("select * from Fine_master where Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "'");
                if (dt.Rows.Count != 0)
                {
                    //if (dt.Rows[0]["Fine_Apply_Student_Type"].ToString() == "Both")
                    //{

                    //}
                    //if (dt.Rows[0]["Fine_Apply_Student_Type"].ToString() == ViewState["Transfer_Status"].ToString())
                    //{

                    //}
                    //else
                    //{
                    //    return;
                    //}
                    if (chk_latefineapplay.Checked != true)
                    {

                        ViewState["fineAmt"] = "0.00";
                        bind_ttl_fee();
                        return;
                    }

                    string pay_date = txt_payment_date.Text;
                    int payidate = My.DateConvertToIdate(pay_date);



                    string fineType = dt.Rows[0]["Fine_type"].ToString();
                    if (fineType == "DayWise")//===== Days
                    {
                        #region DayWise

                        //Advance Payment Check
                        string crnt_year = mycode.year();
                        string cunrt_session = My.get_session();
                        string session_frst_year = cunrt_session.Substring(0, 4);
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);


                        int pay_month = My.toint(ViewState["checked_after_frst_mnth"].ToString());
                        s_year = My.check_start_months(pay_month, s_year);

                        int pay_month_with_year = My.toint(s_year + ViewState["checked_after_frst_mnth"].ToString());
                        int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                        //Advance Payment Check

                        if (crnt_month_with_year >= pay_month_with_year)
                        {

                            ViewState["FineType"] = "DayWise";
                            string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                            string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + session_s_year;


                            //==========
                            //int last_iday_of_payment = My.toIntS(session_s_year + applicable_month + dt.Rows[0]["Last_day_to_deposit_fees"].ToString());
                            //int payment_iday = My.DateConvertToIdate(last_day_of_payment);
                            //==========


                            string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + ViewState["checked_frst_mnth"].ToString() + "/" + session_s_year;

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
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                ViewState["fineAmt"] = ttl_fine_amt.ToString("0.00");
                                bind_ttl_fee();
                            }
                            else
                            {
                                ViewState["fineAmt"] = "0";
                                bind_ttl_fee();
                            }

                        }
                        #endregion
                    }
                    else if (fineType == "MonthWise")//===== MonthWise
                    {
                        #region MonthWise
                        ViewState["FineType"] = "MonthWise";
                        string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                        string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");

                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payment, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        int totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                        ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                        ViewState["late_fine_no_of_day_month"] = totalmonths;
                        ViewState["fine_date_From"] = last_day_of_payment;
                        ViewState["fine_date_To"] = pay_date;
                        if (My.toDouble(totalmonths) > 0)
                        {
                            string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                            double ttl_fine_amt = My.toDouble(fine_amt) * totalmonths;
                            ViewState["fineAmt"] = ttl_fine_amt.ToString("0.00");
                            bind_ttl_fee();
                        }
                        else
                        {
                            ViewState["fineAmt"] = "0";
                            bind_ttl_fee();
                        }
                        #endregion
                    }
                    else
                    {

                        #region QuarterWise
                        ViewState["FineType"] = "QuarterWise";
                        double fnl_fine_amt = 0;
                        SqlDataAdapter ad = new SqlDataAdapter("select * from Fine_master where Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "' and Q_start_month<='" + ViewState["checked_after_frst_mnth"].ToString() + "'  order by Q_start_month asc", My.conn);
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
                                string endmnth_in_two = My.getMonthS_twoDigit(dr["Q_end_month"].ToString());
                                string last_day_of_payment_q = dr["Last_day_to_deposit_fees"].ToString() + "/" + endmnth_in_two + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");




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
                                        //double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                        double ttl_fine_amt = My.toDouble(fine_amt);
                                        fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                    }
                                    else
                                    {
                                        fnl_fine_amt = fnl_fine_amt += 0;
                                    }
                                }
                            }

                            ViewState["fineAmt"] = fnl_fine_amt.ToString("0.00");
                            bind_ttl_fee();
                        }
                        #endregion
                    }
                }
            }
        }

        protected void rp_fee_details_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string lbl_mnth = ((Label)e.Item.FindControl("lbl_mnth")).Text;
                HtmlTableRow row2 = (HtmlTableRow)e.Item.FindControl("row");

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

        private void find_fine(string date, string month)
        {
            try
            {
                double late_fine = 0;
                //DataRow[] drs = dtDatas.Select(" value='true' ");
                int count = 0;
                string m = (My.toDouble(month) + 00).ToString("00");
                string year1 = ddlsessionad.SelectedItem.Text.Split('-')[0];
                string year2 = ddlsessionad.SelectedItem.Text.Split('-')[1];
                string year = "";
                if (My.toDouble(m) < My.toDouble(My.tomonth_number(My.get_start_month())))
                {
                    year = year2;
                }
                else
                {
                    year = year1;
                }
                DateTime start_date = Convert.ToDateTime("01/" + month + "/" + year);
                DateTime end_date = Convert.ToDateTime(txt_payment_date.Text);
                int days = Convert.ToInt32((end_date - start_date).TotalDays);

                DateTime start_date1 = Convert.ToDateTime(DateTime.Now.ToString("01/MM/yyyy"));
                DateTime end_date1 = Convert.ToDateTime(txt_payment_date.Text);


                int days1 = Convert.ToInt32((end_date1 - start_date1).TotalDays);
                days1 += 1;
                DataTable dt = My.dataTable("select top 1 * from dbo.[Fine_Setup] where Session='" + ViewState["Session"].ToString() + "' and Status='Active' and Date_Limitation<='" + days1 + "'");

                int limit_days = 0;
                if (dt.Rows.Count.ToString() != "0")
                {
                    limit_days = Convert.ToInt32(dt.Rows[0]["Date_Limitation"].ToString());
                }

                int growcountS = GridView2.Rows.Count;
                for (int iS = 0; iS < growcountS; iS++)
                {
                    Label lbl_Month = (Label)GridView2.Rows[iS].FindControl("lbl_Month");
                    CheckBox chk_month = (CheckBox)GridView2.Rows[iS].FindControl("chk_month");
                    if (chk_month.Checked == true)
                    {
                        double mm = My.toDouble(My.tomonth_number(lbl_Month.Text));
                        string y1 = ddlsessionad.SelectedItem.Text.Split('-')[0];
                        string y2 = ddlsessionad.SelectedItem.Text.Split('-')[1];
                        string y = "";
                        if (mm < My.toDouble(My.tomonth_number(My.get_start_month())))
                        {
                            y = y2;
                        }
                        else
                        {
                            y = y1;
                        }
                        DateTime sd = Convert.ToDateTime("01/" + mm.ToString("00") + "/" + y);
                        DateTime ed = Convert.ToDateTime(txt_payment_date.Text);
                        int d = Convert.ToInt32((ed - sd).TotalDays);
                        if (d > limit_days)
                        {
                            count += 1;
                        }
                    }
                }


                if (days > limit_days)
                {
                    if (dt.Rows.Count.ToString() != "0")
                    {
                        late_fine = My.toDouble(dt.Rows[0]["Fine"].ToString()) * count;
                        txtfineamount.Text = late_fine.ToString();
                        txttotalbill.Text = (My.toDouble(txttotalbill.Text) + My.toDouble(txtfineamount.Text)).ToString();
                    }
                    else
                    {
                        late_fine = 0;
                        txtfineamount.Text = late_fine.ToString();
                        txttotalbill.Text = (My.toDouble(txttotalbill.Text) + My.toDouble(txtfineamount.Text)).ToString();
                    }
                }
                else
                {
                    if (count == 0)
                    {
                        late_fine = 0;
                    }
                    else
                    {
                        if (dt.Rows.Count.ToString() != "0")
                        {
                            late_fine = My.toDouble(dt.Rows[0]["Fine"].ToString()) * count;
                        }
                        else
                        {
                            late_fine = 0;
                        }
                    }
                    txtfineamount.Text = late_fine.ToString();
                    txttotalbill.Text = (My.toDouble(txttotalbill.Text) + My.toDouble(txtfineamount.Text)).ToString();
                }
            }
            catch
            {
                txtfineamount.Text = "0";
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

        protected void txt_paid_amount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_total_dues.Text = (Convert.ToDouble(txttotalbill.Text) - Convert.ToDouble(txt_paid_amount.Text)).ToString();
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnfind_Click(object sender, EventArgs e)
        {
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["firstrow"] == null)
                {
                    CheckBox chk_month = (CheckBox)e.Row.FindControl("chk_month");
                    chk_month.Enabled = true;
                    ViewState["firstrow"] = "1";
                }
            }
        }
        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Monthly_payment_student.aspx", false);
        }
        protected void chk_latefineapplay_CheckedChanged(object sender, EventArgs e)
        {
            fine_calculation(ViewState["MnthName"].ToString(), "2");
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
        protected void txt_other_fee_TextChanged(object sender, EventArgs e)
        {

            if (txt_other_fee.Text == "")
            {
                Alertme("Please enter other fee", "warning");
            }
            else
            {
                bind_ttl_fee();
                //double total = My.toDouble(txtfineamount.Text) + My.toDouble(txt_other_fee.Text);


            }
        }
        protected void txt_payment_date_TextChanged(object sender, EventArgs e)
        {
            fine_calculation(ViewState["MnthName"].ToString(), "2");
        }


        #region final pay
        protected void btn_save_payment_Click(object sender, EventArgs e)
        {
            try
            {

                bool finalsbumnit = false;
                int chekmonthpaycount = My.get_paymnet_chekseeting();

                int rowcoun = GridView2.Rows.Count;

                int seclectioncount = 0;
                for (int iS = 0; iS < rowcoun; iS++)
                {
                    CheckBox chk_month = (CheckBox)GridView2.Rows[iS].FindControl("chk_month");
                    if (chk_month.Checked == true)
                    {
                        seclectioncount = seclectioncount + 1;
                    }
                }
                if (seclectioncount >= chekmonthpaycount)
                {

                    if (chekmonthpaycount == 1)
                    {

                        finalsbumnit = true;
                    }
                    else
                    {
                        if (chekmonthpaycount == 2)
                        {
                            if (!mycode.IsDivisible(seclectioncount, 2))
                            {
                                finalsbumnit = false;
                                Alertme("Your monthly selection is a multiple of " + chekmonthpaycount + ".So please select valid month.", "warning");

                            }
                            else
                            {
                                finalsbumnit = true;
                            }
                        }
                        else if (chekmonthpaycount == 3)
                        {
                            if (!mycode.IsDivisible(seclectioncount, 3))
                            {
                                finalsbumnit = false;
                                Alertme("Your monthly selection is a multiple of " + chekmonthpaycount + ".So please select valid month.", "warning");

                            }
                            else
                            {
                                finalsbumnit = true;
                            }
                        }
                    }


                    if (finalsbumnit == true)// pay function
                    {


                        bool isonlinepymnet = My.get_status_ispaymnet_on(ViewState["class_id"].ToString());
                        if (isonlinepymnet == true)
                        {
                            Random r = new Random(DateTime.Now.Millisecond);
                            string order_id = DateTime.UtcNow.ToString("yyMMddHHMMss") + r.Next(12346, 48749);
                            SqlCommand cmd;
                            string query = " INSERT INTO Payment_transaction_process (Name,Admission_no,Session,Class_id,Session_id,totalAmount,totalpaid_perivius,totaldiscount,totallatefine,Total_pay,Date,Time,Idate,ordertrackingid,month,status,parameter,parameter_id,Class_name,hosteltaken,day_boarding,day_boarding_lunch,category_id,sub_category_id,transportportation_id,hostel_id,group_id,Section,Emailid,Mobileno,Fine_amount,Total_no_of_fine_day_month,Fine_date_from,Fine_date_to,Fine_type,Lunch_Boarding_Parmeter) values (@Name,@Admission_no,@Session,@Class_id,@Session_id,@totalAmount,@totalpaid_perivius,@totaldiscount,@totallatefine,@Total_pay,@Date,@Time,@Idate,@ordertrackingid,@month,@status,@parameter,@parameter_id,@Class_name,@hosteltaken,@day_boarding,@day_boarding_lunch,@category_id,@sub_category_id,@transportportation_id,@hostel_id,@group_id,@Section,@Emailid,@Mobileno,@Fine_amount,@Total_no_of_fine_day_month,@Fine_date_from,@Fine_date_to,@Fine_type,@Lunch_Boarding_Parmeter)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Name", ViewState["studentname"].ToString());
                            cmd.Parameters.AddWithValue("@Admission_no", ViewState["regid"].ToString());
                            cmd.Parameters.AddWithValue("@Session", ViewState["Session"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", ViewState["Class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_id"].ToString());
                            cmd.Parameters.AddWithValue("@totalAmount", ViewState["total"].ToString());
                            cmd.Parameters.AddWithValue("@totalpaid_perivius", ViewState["paid_prev"].ToString());
                            cmd.Parameters.AddWithValue("@totaldiscount", ViewState["discount"].ToString());
                            cmd.Parameters.AddWithValue("@totallatefine", txtfineamount.Text);
                            cmd.Parameters.AddWithValue("@Total_pay", ViewState["totalbill"].ToString());
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Time", mycode.time());
                            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@ordertrackingid", order_id);
                            cmd.Parameters.AddWithValue("@month", ViewState["month"].ToString());
                            cmd.Parameters.AddWithValue("@status", "Pending");

                            cmd.Parameters.AddWithValue("@parameter", ViewState["parameter"].ToString());
                            cmd.Parameters.AddWithValue("@parameter_id", ViewState["parameter_id"].ToString());
                            cmd.Parameters.AddWithValue("@Class_name", ViewState["Class_name"].ToString());

                            cmd.Parameters.AddWithValue("@hosteltaken", ViewState["hosteltaken"].ToString());
                            cmd.Parameters.AddWithValue("@hostel_id", ViewState["hostel_id"].ToString());

                            cmd.Parameters.AddWithValue("@day_boarding", ViewState["day_bording"].ToString());
                            cmd.Parameters.AddWithValue("@day_boarding_lunch", ViewState["day_bording_with_lunch"].ToString());
                            cmd.Parameters.AddWithValue("@category_id", ViewState["category_id"].ToString());
                            cmd.Parameters.AddWithValue("@sub_category_id", ViewState["sub_category_id"].ToString());
                            cmd.Parameters.AddWithValue("@transportportation_id", ViewState["transportID"].ToString());
                            cmd.Parameters.AddWithValue("@group_id", ViewState["group_id"].ToString());
                            cmd.Parameters.AddWithValue("@Section", ViewState["Section"].ToString());

                            cmd.Parameters.AddWithValue("@Emailid", ViewState["email_id"].ToString());
                            cmd.Parameters.AddWithValue("@Mobileno", ViewState["mobilenumber"].ToString());


                            cmd.Parameters.AddWithValue("@Fine_amount", txtfineamount.Text);
                            cmd.Parameters.AddWithValue("@Total_no_of_fine_day_month", ViewState["late_fine_no_of_day_month"].ToString());
                            cmd.Parameters.AddWithValue("@Fine_date_from", ViewState["fine_date_From"].ToString());
                            cmd.Parameters.AddWithValue("@Fine_date_to", ViewState["fine_date_To"].ToString());
                            cmd.Parameters.AddWithValue("@Fine_type", ViewState["FineType"].ToString());
                            cmd.Parameters.AddWithValue("@Lunch_Boarding_Parmeter", ViewState["parameteridS"].ToString());

                            if (My.InsertUpdateData(cmd))
                            {
                                //------------------------------ pay---------------------------- 
                                Response.Redirect("payfinal.aspx?regid=" + ViewState["regid"].ToString() + "&orderid=" + order_id, false);








                            }
                        }
                        else
                        {
                            Alertme("Sorry! Online payment is not enabled", "warning");
                        }


                    }
                }
            }
            catch
            {
            }

        }

        #endregion

        double total = 0;
        protected void grid_payment_history_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Amount = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_Amount.Text != "")
                {
                    total = total + My.toDouble(lbl_Amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totaldiscount");


                lbl_totaldiscount.Text = total.ToString("0.00");
            }
        }
    }
}