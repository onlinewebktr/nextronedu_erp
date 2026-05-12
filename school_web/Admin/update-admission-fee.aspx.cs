using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class update_admission_fee : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        hd_user_Type.Value = Session["userTypeFee"].ToString();

                        Session["reprint_otherfee"] = "2";
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session_student, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddlsessionad, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsessionad.SelectedValue = My.get_session_id();
                        ddlsession.SelectedValue = My.get_session_id();
                        ddl_session_student.SelectedValue = My.get_session_id();
                    }
                }
            }
            catch (Exception ex)
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

        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE ''+@SearchMobNo+'%' and Session_id='" + Session_id + "' and Status='1'";
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
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE ''+@SearchMobNo+'%' and Session_id='" + Session_id + "' and Status='1'";
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
                    ViewState["Admission_no"] = dr["admissionserialnumber"].ToString();
                    ViewState["Session_name"] = dr["session"].ToString();
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

                    lbl_catogery.Text = mycode.get_catogery(dr["Category_id"].ToString());
                    lbl_subcatogery.Text = mycode.get_subcatogery(dr["Category_id"].ToString(), dr["SubCategory_id"].ToString());

                    lbltransporttion.Text = dr["transportationtaken"].ToString();

                    if (dt.Rows[0]["hosteltaken"].ToString() == "")
                    {
                        ViewState["hostaltaken"] = "No";
                    }
                    else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                    {
                        ViewState["hostaltaken"] = "No";
                    }
                    else
                    {
                        ViewState["hostaltaken"] = dt.Rows[0]["hosteltaken"].ToString();
                    }
                    find_admission_fee(dr["Session_id"].ToString(), dr["session"].ToString(), dr["admissionserialnumber"].ToString(), dr["Class_id"].ToString());

                }
            }
        }

        private void find_admission_fee(string session_id, string Session, string admission_No, string class_id)
        {
            string parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
            ViewState["paymentTypes"] = parameter;
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_No + "'  and (parameter_id='1' or parameter_id='5') and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + class_id + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_No + "' and parameter='" + parameter + "' and session='" + Session + "')t";
            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count > 0)
            {
                rd_fees.DataSource = fee_dt;
                rd_fees.DataBind();
                pnl_fee_details.Visible = true;
            }
            else
            {
                rd_fees.DataSource = null;
                rd_fees.DataBind();
                pnl_fee_details.Visible = false;
                Alertme("Not taken any installment of Admission fee.", "warning");
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



        double total_payble_amt = 0; double total_disc_amt = 0; double total_paid_amount = 0; double total_net_payble = 0;
        protected void rd_fees_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txt_payble = ((TextBox)e.Item.FindControl("txt_payble")) as TextBox;
                TextBox txt_disc_amount = ((TextBox)e.Item.FindControl("txt_disc_amount")) as TextBox;
                Label lbl_paid_amt = ((Label)e.Item.FindControl("lbl_paid_amt")) as Label;
                Label lbl_net_payble = ((Label)e.Item.FindControl("lbl_net_payble")) as Label;

                total_payble_amt = total_payble_amt + My.toDouble(txt_payble.Text);
                total_disc_amt = total_disc_amt + My.toDouble(txt_disc_amount.Text);
                total_paid_amount = total_paid_amount + My.toDouble(lbl_paid_amt.Text);
                total_net_payble = total_net_payble + My.toDouble(lbl_net_payble.Text);
            }
            lbl_total_payble.Text = total_payble_amt.ToString("0.00");
            lbl_total_disc.Text = total_disc_amt.ToString("0.00");
            lbl_total_paid_amt.Text = total_paid_amount.ToString("0.00");
            lbl_total_net_payble.Text = total_net_payble.ToString("0.00");
        }

        protected void lbl_payble_TextChanged(object sender, EventArgs e)
        {
            try
            {
                calculate_fees_greed();
            }
            catch (Exception ex)
            {

            }
        }

        private void calculate_fees_greed()
        {
            double total_payble_amt_cl = 0; double total_disc_amt_cl = 0; double total_paid_amount_cl = 0; double total_net_payble_cl = 0;
            int i;
            int gridview_rowcount = rd_fees.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                TextBox txt_payble = (TextBox)rd_fees.Items[i].FindControl("txt_payble");
                TextBox txt_disc_amount = (TextBox)rd_fees.Items[i].FindControl("txt_disc_amount");
                Label lbl_paid_amt = (Label)rd_fees.Items[i].FindControl("lbl_paid_amt");
                Label lbl_net_payble = (Label)rd_fees.Items[i].FindControl("lbl_net_payble");


                if (My.toDouble(txt_payble.Text) >= My.toDouble(txt_disc_amount.Text))
                {
                    double net_payble = My.toDouble(txt_payble.Text) - (My.toDouble(txt_disc_amount.Text) + My.toDouble(lbl_paid_amt.Text));

                    total_payble_amt_cl = total_payble_amt_cl + Convert.ToDouble(txt_payble.Text);
                    total_disc_amt_cl = total_disc_amt_cl + Convert.ToDouble(txt_disc_amount.Text);
                    total_paid_amount_cl = total_paid_amount_cl + Convert.ToDouble(lbl_paid_amt.Text);
                    total_net_payble_cl = total_net_payble_cl + net_payble;
                    if (net_payble > 0)
                    {
                        lbl_net_payble.Text = net_payble.ToString("0.00");
                    }
                    else
                    {
                        lbl_net_payble.Text = "0.00";
                    }
                }
                else
                {
                    Alertme("Discount amount can not be greater than fee amount.", "warning");
                    return;
                }


            }
            lbl_total_payble.Text = total_payble_amt_cl.ToString("0.00");
            lbl_total_disc.Text = total_disc_amt_cl.ToString("0.00");
            lbl_total_paid_amt.Text = total_paid_amount_cl.ToString("0.00");
            lbl_total_net_payble.Text = total_net_payble_cl.ToString("0.00");

        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_remarks.Text == "")
                {
                    txt_remarks.Focus();
                    Alertme("Please enter remarks.", "warning");
                    return;
                }

                ViewState["successMSG"] = "0";
                update_in_discount_master();
                update_payment();
                if (ViewState["successMSG"].ToString() == "1")
                {
                    manage_old_peyment();
                    My.exeSql("insert into Admission_annual_revised_payment_history(Admission_no,Session_id,Remark,Type,Created_by,Created_date,Created_time,Time) values ('" + ViewState["Admission_no"].ToString() + "','" + ViewState["Sessionid"].ToString() + "','" + txt_remarks.Text + "','" + ViewState["paymentTypes"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','" + mycode.time() + "');");
                    Alertme("Payment has been updated.", "success");
                    find_admission_fee(ViewState["Sessionid"].ToString(), ViewState["Session_name"].ToString(), ViewState["Admission_no"].ToString(), ViewState["class_id"].ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void manage_old_peyment()
        {
            string qry = "delete from Monthly_Fee_Collection_Slip where adno='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session_name"].ToString() + "' and parameter='" + ViewState["paymentTypes"].ToString() + "'; delete from Admission_fee_collection  where Admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session_name"].ToString() + "'";
            My.exeSql(qry);

            DataTable dt = My.dataTable("select * from Student_Payment_History where Addmission_no='" + ViewState["Admission_no"].ToString() + "' and Session='" + ViewState["Session_name"].ToString() + "' and parameter_New='" + ViewState["paymentTypes"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //send_data_to_school_ledger(slip_no, entry_id);
                    // create_admission_annual_dues(parameter);
                    send_data_in_feetypewise_collection(dr["Slip_no"].ToString(), dr["Entry_id"].ToString(), ViewState["paymentTypes"].ToString(), dr["Amount"].ToString(), dr["Date"].ToString());
                    string ttl_discount = get_ttl_discount(dr["Slip_no"].ToString(), ViewState["paymentTypes"].ToString());

                    if (ViewState["paymentTypes"].ToString() == "Admission")
                    {
                        send_data_to_admission_fee_collection(dr["Slip_no"].ToString(), dr["Entry_id"].ToString(), ViewState["paymentTypes"].ToString(), dr["Amount"].ToString(), ttl_discount, dr["Date"].ToString(), dr["Remarks"].ToString(), dr["mode"].ToString());
                    }
                    else
                    {
                        send_data_to_annual_fee_collection(dr["Slip_no"].ToString(), dr["Entry_id"].ToString(), dr["Amount"].ToString(), ttl_discount, dr["Date"].ToString(), dr["Remarks"].ToString(), dr["mode"].ToString());
                    }

                    My.exeSql("update Student_Payment_History set discount='" + My.toDouble(ttl_discount).ToString("0.00") + "' where Id=" + dr["Id"].ToString() + "");
                }
            }

            // send_data_in_student_payment_history(type, slip_no, entry_id, ad_no, parameter);

        }

        private string get_ttl_discount(string slip_no, string parameter)
        {
            string discount_amt = "0";
            DataTable dt = My.dataTable("select isnull(sum(convert(float, disc_amt)),0) as Discount_amt from Monthly_Fee_Collection_Slip where slipno='" + slip_no + "' and parameter='" + parameter + "'");
            if (dt.Rows.Count > 0)
            {
                discount_amt = dt.Rows[0]["Discount_amt"].ToString();
            }
            return discount_amt;
        }

        private void update_payment()
        {
            int i;
            int gridview_rowcount = rd_fees.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_content_id = (Label)rd_fees.Items[i].FindControl("lbl_content_id");
                TextBox txt_payble = (TextBox)rd_fees.Items[i].FindControl("txt_payble");
                TextBox txt_disc_amount = (TextBox)rd_fees.Items[i].FindControl("txt_disc_amount");
                Label lbl_paid_amt = (Label)rd_fees.Items[i].FindControl("lbl_paid_amt");
                Label lbl_net_payble = (Label)rd_fees.Items[i].FindControl("lbl_net_payble");

                double payble_after_disc = (My.toDouble(txt_payble.Text) - My.toDouble(txt_disc_amount.Text));
                string qry = "update Typewise_fee_collection set payable='" + My.toDouble(txt_payble.Text).ToString("0.00") + "',Disc='" + My.toDouble(txt_disc_amount.Text).ToString("0.00") + "',Payable_after_disc='" + payble_after_disc.ToString("0.00") + "',paid='0.00',dues='" + payble_after_disc.ToString("0.00") + "',status='Dues' where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session_name"].ToString() + "' and class_id='" + ViewState["class_id"].ToString() + "' and content_id='" + lbl_content_id.Text + "' and parameter='" + ViewState["paymentTypes"].ToString() + "'";
                My.exeSql(qry);
                ViewState["successMSG"] = "1";
            }
        }


        private void send_data_in_feetypewise_collection(string slip_no, string entry_id, string parameter, string Amount, string payment_date)
        {
            string class_id = ViewState["classid"].ToString();
            double paid_amount = My.toDouble(Amount);
            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session_name"].ToString() + "' and status='Dues' and parameter='" + parameter + "'", My.conn);
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
                        dr["Date"] = payment_date;
                        dr["idate"] = My.toDateTime(payment_date).ToString("yyyyMMdd");
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
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), My.toDouble(dr["paid"].ToString()).ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], ViewState["Admission_no"].ToString(), ViewState["Session_name"].ToString(), ViewState["class_id"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, payment_date);
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
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], ViewState["Admission_no"].ToString(), ViewState["Session_name"].ToString(), ViewState["class_id"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, payment_date);
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
        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, string payment_date)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + payable + "','" + paid + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + prevpaid + "','" + ViewState["Branchid"].ToString() + "','0','" + payment_date + "','" + My.DateConvertToIdate(payment_date) + "');";
            My.exeSql(qry);
        }

        private void send_data_to_admission_fee_collection(string slip_no, string entry_id, string parameter, string Amount, string ttl_discount, string Date, string Remarks, string mode)
        {
            DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session_name"].ToString() + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues' ");

            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_fee_collection where Admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session_name"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                double ttl_amts = My.toDouble(Amount) + My.toDouble(ttl_discount);
                DataRow dr = dt.NewRow();
                dr[1] = ViewState["Admission_no"].ToString();
                dr[2] = My.toDouble(ttl_amts).ToString("0.00");
                dr[3] = My.toDouble(ttl_discount).ToString("0.00");
                dr[4] = My.toDouble(Amount).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = Date;
                dr[8] = mode;
                dr[9] = slip_no;
                dr["session"] = ViewState["Session_name"].ToString();

                dr["idate"] = Convert.ToDateTime(Date).ToString("yyyyMMdd");
                dr["remark"] = Remarks;
                dr["entry_id"] = entry_id;
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString(); ;
                dr["Slip_no"] = slip_no;
                dr["Acamedic_Semester_Id"] = "0";
                dr["branchid"] = ViewState["Branchid"].ToString();
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

        private void send_data_to_annual_fee_collection(string slip_no, string entry_id, string Amount, string ttl_discount, string Date, string Remarks, string mode)
        {
            DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session_name"].ToString() + "' and  parameter like '%AnnualFee%' and feetype!='Previous Dues' ");

            //.ToString("0.00")
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session_name"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                double ttl_amts = My.toDouble(Amount) + My.toDouble(ttl_discount);
                DataRow dr = dt.NewRow();
                dr[1] = ViewState["Admission_no"].ToString();
                dr[2] = My.toDouble(ttl_amts).ToString("0.00");
                dr[3] = My.toDouble(ttl_discount).ToString("0.00");
                dr[4] = My.toDouble(Amount).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = Date;
                dr[8] = mode;
                dr[9] = slip_no;
                dr["session"] = ViewState["Session_name"].ToString(); ;

                dr["idate"] = Convert.ToDateTime(Date).ToString("yyyyMMdd");
                dr["remark"] = Remarks;
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString(); ;
                dr["Acamedic_Semester_Id"] = "0";
                dr["branchid"] = ViewState["Branchid"].ToString();
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


        private void update_in_discount_master()
        {
            bool isamountfill = false;

            int y;
            int gridview_rowcount = rd_fees.Items.Count;
            for (y = 0; y < gridview_rowcount; y++)
            {
                TextBox txt_disc_amount = (TextBox)rd_fees.Items[y].FindControl("txt_disc_amount");
                if (mycode.chkenum(txt_disc_amount.Text) == true)
                {
                    isamountfill = true;
                }
            }
            if (isamountfill == true)
            {
                double totla = 0;
                for (int i = 0; i < rd_fees.Items.Count; i++)
                {
                    TextBox txt_fee = (TextBox)rd_fees.Items[i].FindControl("txt_payble");
                    TextBox txt_disc_fee = (TextBox)rd_fees.Items[i].FindControl("txt_disc_amount");
                    Label lbl_content_id = (Label)rd_fees.Items[i].FindControl("lbl_content_id");
                    Label lbl_content = (Label)rd_fees.Items[i].FindControl("lbl_feetype");
                    totla = totla + My.toDouble(txt_fee.Text);
                    //==============*************** 

                    string group_id = "1"; string para_id = "5"; string group_name = "Admission";
                    if (ViewState["paymentTypes"].ToString() == "AdmissionFee")
                    {
                        para_id = "1";
                        group_name = "Annual";
                    }
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Discount_Master where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["Session_name"].ToString() + "' and group_id='" + group_id + "' and month='NA' and admission_no='" + ViewState["Admission_no"].ToString() + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + para_id + "'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Discount_Master");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Discount_on"] = group_name;
                        dr["session"] = ViewState["Session_name"].ToString();
                        dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                        dr["group_id"] = group_id;
                        dr["admission_no"] = ViewState["Admission_no"].ToString();
                        dr["month"] = "NA";
                        dr["fee_head_id"] = lbl_content_id.Text;
                        dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
                        dr["parameter_id"] = para_id;
                        dr["category_id"] = ViewState["Category_id"].ToString();
                        dr["sub_category_id"] = ViewState["sub_category_id"].ToString();
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                            dr["fee_head_id"] = lbl_content_id.Text;
                            dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
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