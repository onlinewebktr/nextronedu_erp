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
    public partial class create_payment_voucher_Month_wise : System.Web.UI.Page
    {
        My mycode = new My();

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
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["flag"] = "0";
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_month_name, "Select Month,Position from Month_Index order by Position");

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddl_section.Focus();
                }
                else
                {
                    find_student();
                    ViewState["findby"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_student()
        {
            string query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and  Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc";
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                ViewState["flagdata"] = "0";
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                ViewState["flagdata"] = "1";
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "' order by Section");
            }
            catch (Exception ex)
            {
            }
        }


        string paymentid;
        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["flagdata"].ToString() == "0")
                {
                    Alertme("Please find student data after that you will be find", "warning");
                }
                else if (txt_payment_date.Text == "")
                {
                    Alertme("Please choose last date of payment.", "warning");
                    txt_payment_date.Focus();

                }
                else if (ddl_month_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select month", "warning");
                    txt_payment_date.Focus();
                }
                else
                {
                    save_created_slip();
                    Alertme("Payment voucher has been created successfully.", "success");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_created_slip()
        {
            DataTable dt;
            if (ViewState["findby"].ToString() == "1")
            {
                dt = mycode.FillData("select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.session,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1'");
            }
            else
            {
                dt = mycode.FillData("select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.session,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.admissionserialnumber='" + txt_admission_no.Text + "'  and Status='1'");
            }
            if (dt.Rows.Count == 0)
            {
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //HOSTEL
                    ViewState["parameterDisc"] = "3";
                    Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(ddlsession.SelectedValue, dr["Class_id"].ToString(), dr["Admission_no"].ToString());
                    ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                    ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                    ViewState["From_month_name"] = (String)dc1["From_month_name"];
                    ViewState["From_month_id"] = (String)dc1["From_month_id"];
                    ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                    ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];
                    if (ViewState["Hostel_id"].ToString() == "0")
                    {
                        ViewState["parameterDisc"] = "4";
                    }

                    //Transport
                    Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(ddlsession.SelectedValue, dr["Class_id"].ToString(), dr["Admission_no"].ToString());
                    ViewState["Transport_id"] = (String)dc2["Transport_id"];
                    ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                    ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                    ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                    ViewState["Month_name"] = (String)dc2["Month_name"];
                    ViewState["Month_id"] = (String)dc2["Month_id"];
                    ViewState["Year_month"] = (String)dc2["Year_month"];
                    ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];


                    My.exeSql("delete from Typewise_fee_collection where admission_no='" + dr["Admission_no"].ToString() + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and  transection=''");
                    ViewState["PrevDues_checked"] = "0";
                    int klsS = 0;
                    //
                    double prev_dues_amt = 0;
                    //==================================== 
                    DataTable dt1 = mycode.FillData(" select *  from dbo.[Month_Index] where Position<" + ddl_month_name.SelectedValue + " order by Position");
                    if (dt1.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string Month_Id = dt1.Rows[i]["Month_Id"].ToString();
                            string Month = dt1.Rows[i]["Month"].ToString();
                            string Position = dt1.Rows[i]["Position"].ToString();
                            string duesamount = find_dues(Month, Month_Id, dr);//old dues monthamount
                            prev_dues_amt = prev_dues_amt + My.toDouble(duesamount);
                        }
                    }







                    //=========================== 
                    //======================




                    string month_name = ddl_month_name.SelectedItem.Text;
                    string monyh_id = mycode.get_sttratingmonthid(ddl_month_name.SelectedItem.Text);
                    string dues = find_dues(month_name, monyh_id, dr);
                    double dues_amt = My.toDouble(dues);

                    if (dues_amt > 0)
                    {
                        DataTable dtDup = My.dataTable("select Id from Payment_voucher_slip where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + dr["Class_id"].ToString() + " and Admission_no='" + dr["Admission_no"].ToString() + "' and Months='" + month_name + "'");
                        if (dtDup.Rows.Count > 1)
                        {
                            My.exeSql("delete from Payment_voucher_slip_history where Session_id=" + ddlsession.SelectedValue + " and Class_id='" + dr["Class_id"].ToString() + "' and Admission_no='" + dr["Admission_no"].ToString() + "' and Month='" + month_name + "'; delete from Payment_voucher_slip where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + dr["Class_id"].ToString() + " and Admission_no='" + dr["Admission_no"].ToString() + "' and Months='" + month_name + "'");
                        } 

                        SqlDataAdapter ad = new SqlDataAdapter("select * from Payment_voucher_slip_history where Session_id=" + ddlsession.SelectedValue + "  and Admission_no='" + dr["Admission_no"].ToString() + "' and Month='" + month_name + "'", My.conn);
                        DataSet ds = new DataSet();
                        ad.Fill(ds, "Certificate_Master");
                        DataTable dtx = ds.Tables[0];
                        if (dtx.Rows.Count == 0)
                        {
                            DataRow drs = dtx.NewRow();
                            drs["Session_id"] = ddlsession.Text;
                            drs["Class_id"] = dr["Class_id"].ToString();
                            drs["Section"] = dr["Section"].ToString();
                            drs["Admission_no"] = dr["Admission_no"].ToString();
                            drs["Month"] = month_name;
                            drs["Month_id"] = monyh_id;
                            drs["Amount"] = dues;
                            drs["Created_date"] = mycode.date();
                            drs["Created_idate"] = mycode.idate();
                            drs["Created_time"] = mycode.time();
                            drs["User_id"] = ViewState["Userid"].ToString();
                            drs["Firm_id"] = ViewState["firm_id"].ToString();
                            dtx.Rows.Add(drs);
                            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                            ad.Update(dtx);
                        }
                        else
                        {
                            string id = dtx.Rows[0]["Id"].ToString();
                            mycode.executequery("update Payment_voucher_slip_history set Amount='" + dues + "',User_id='" + ViewState["Userid"].ToString() + "',Created_date='" + mycode.date() + "',Created_idate='" + mycode.idate() + "',Created_time='" + mycode.time() + "' where  Id=" + id + "");
                        }


                        if (mycode.IsUserExist("select Id from Payment_voucher_slip where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + dr["Class_id"].ToString() + " and Admission_no='" + dr["Admission_no"].ToString() + "' and Months='" + month_name + "'"))
                        {
                            create_sl_no();
                            SqlCommand cmd;
                            string query = "INSERT INTO Payment_voucher_slip (Session_id,Class_id,Section,Admission_no,Months,Amount,Slip_id,Created_date,Created_idate,Created_time,User_id,Firm_id,Last_date_of_payment,Previous_dues,Total_amount) values (@Session_id,@Class_id,@Section,@Admission_no,@Months,@Amount,@Slip_id,@Created_date,@Created_idate,@Created_time,@User_id,@Firm_id,@Last_date_of_payment,@Previous_dues,@Total_amount)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                            cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Section", dr["Section"].ToString());
                            cmd.Parameters.AddWithValue("@Admission_no", dr["Admission_no"].ToString());
                            cmd.Parameters.AddWithValue("@Months", month_name);
                            cmd.Parameters.AddWithValue("@Amount", dues_amt.ToString("0.00"));
                            cmd.Parameters.AddWithValue("@Slip_id", paymentid);
                            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Firm_id", ViewState["firm_id"].ToString());
                            cmd.Parameters.AddWithValue("@Last_date_of_payment", txt_payment_date.Text);
                            cmd.Parameters.AddWithValue("@Previous_dues", prev_dues_amt.ToString("0.00"));
                            cmd.Parameters.AddWithValue("@Total_amount", (My.toDouble(dues_amt) + My.toDouble(prev_dues_amt)).ToString("0.00"));
                            if (My.InsertUpdateData(cmd))
                            {

                            }
                        }
                        else
                        {
                            mycode.executequery("update Payment_voucher_slip set Amount='" + dues_amt.ToString("0.00") + "',Previous_dues='" + prev_dues_amt.ToString("0.00") + "',Total_amount='" + (My.toDouble(dues_amt) + My.toDouble(prev_dues_amt)).ToString("0.00") + "',Last_date_of_payment='" + txt_payment_date.Text + "',Created_date='" + mycode.date() + "',Created_idate='" + mycode.idate() + "',Created_time='" + mycode.time() + "' where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + dr["Class_id"].ToString() + " and Admission_no='" + dr["Admission_no"].ToString() + "' and Months='" + month_name + "'");
                        }
                    }
                }
            }
        }


        private void create_sl_no()
        {
            bool duplicate = true;
            paymentid = "PY" + My.toint(My.create_random_no_otp());
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Slip_id from Payment_voucher_slip where Slip_id='" + paymentid + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    paymentid = "PY" + My.toint(My.create_random_no_otp());
                }
            }
        }

        private string find_dues(string month, string month_id, DataRow dr)
        {
            DataTable feedt = new DataTable();
            ViewState["parameter"] = dr["hosteltaken"].ToString().ToLower() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
            ViewState["IsBoarding"] = "0";
            ViewState["parameteridS"] = "4";
            if (dr["Month_id"].ToString() != "")
            {
                ViewState["LunchMnthName"] = dr["Month_name"].ToString();
                ViewState["LunchMnthId"] = dr["Month_id"].ToString();
                ViewState["IsBoarding"] = "1";
            }


            string dues = "0";
            //====================
            if (ViewState["IsBoarding"].ToString() == "1")
            {
                int mnthids = My.toint(month_id);
                if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                {
                    ViewState["parameteridS"] = "44";
                }
                else
                {
                    ViewState["parameteridS"] = "4";
                }
            }
            string session = ddlsession.SelectedItem.Text;
            string type = "";


            string cunrt_session = session;
            string[] stringSeparators = new string[] { "-" };
            string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
            string session_frst_year = arr[0];
            string session_last_year = arr[1];
            int session_s_year = My.toint(session_frst_year);
            int s_year = My.toint(session_frst_year);


            if (My.dataTable("select  * from Typewise_fee_collection where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + ddlsession.SelectedItem.Text + "' and month='" + month + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
            {
                //string qqrryy = "select (sum(convert(float, amount))-sum(convert(float, isnull((disc_amount),'0')))-sum(convert(float, isnull((previously_paid),'0')))) as Total_Dues from (select payable amount,(isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + dr["Admission_no"].ToString() + "' and month='" + month + "'   and parameter_id='" + ViewState["parameteridS"].ToString() + "' and session='" + session + "' and fee_head_id=Typewise_fee_collection.content_id and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + dr["Class_id"].ToString().ToString() + "' and admission_no='All' and month='" + month + "'  and parameter_id='" + ViewState["parameteridS"].ToString() + "' and session='" + session + "' and fee_head_id=Typewise_fee_collection.content_id and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),(isnull((select top 1 disc_amount from dbo.[Discount_Master_for_bus] where fee_head_id=Typewise_fee_collection.content_id and admission_no='" + dr["Admission_no"].ToString() + "' and month='" + month + "' and session_id='" + ddlsession.SelectedValue + "' and Bus_path='1'),(select top 1 disc_amount from dbo.[Discount_Master_for_bus] where fee_head_id=Typewise_fee_collection.content_id and admission_no='All' and month='" + month + "' and session_id='" + ddlsession.SelectedValue + "' and Bus_path='1'))))) disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + session + "' and month='" + month + "' and parameter='MonthlyFee' and content_id!='6121')t";
                feedt = My.dataTable("select (sum(convert(float, amount))-sum(convert(float, isnull((disc_amount),'0')))-sum(convert(float, isnull((previously_paid),'0')))) as Total_Dues from (select payable amount,Disc as disc_amount,isnull(paid,'0') previously_paid from Typewise_fee_collection where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + session + "' and month='" + month + "' and parameter='" + ViewState["parameter"].ToString() + "' and content_id!='6121'  and status='Dues')t");
                if (feedt.Rows.Count.ToString() != "0")
                {
                    dues = feedt.Rows[0]["Total_Dues"].ToString();
                }
                else
                {
                    dues = "0";
                }
            }
            else
            {
                //dr["bac_colour"] = "White";
                Dictionary<string, object> dc = new Dictionary<string, object>();
                dc["admission_no"] = dr["Admission_no"].ToString();
                dc["session_id"] = My.get_session_id();
                dc["class"] = dr["class"].ToString();
                dc["session"] = session;
                dc["class_id"] = dr["Class_id"].ToString();
                dc["hosteltaken"] = dr["hosteltaken"].ToString().ToLower();
                dc["months"] = month;
                dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                dc["day_boarding"] = My.toBool(dr["is_applied_dayboarding"]);
                dc["day_boarding_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                dc["category_id"] = dr["category_id"].ToString();
                dc["sub_category_id"] = dr["SubCategory_id"].ToString();

                dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();


                dc["parameter_id"] = ViewState["parameteridS"].ToString();


                //new08/08/2022



                string monthid = My.tomonth_numberstring(month);
                int pay_month = My.toint(monthid);
                s_year = My.check_start_months(pay_month, s_year);
                dc["monthid"] = s_year + monthid;


                dc["sp_status"] = "1";
                feedt = My.dataTableSP("sp_Fetch_month_dues", dc);
                if (feedt.Rows.Count.ToString() != "0")
                {
                    dues = feedt.Rows[0]["Total_Dues"].ToString();
                }
                else
                {
                    dues = "0";
                }
            }
            return dues;
        }

        protected void btn_find_by_admission_no_Click(object sender, EventArgs e)
        {
            if (txt_admission_no.Text == "")
            {
                Alertme("Sorry your admission no. is not available in the system your session", "warning");
            }
            else
            {
                find_student_select_student();
                ViewState["findby"] = "2";

            }
        }

        private void find_student_select_student()
        {
            string query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and  admissionserialnumber='" + txt_admission_no.Text + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc";
            DataTable dt = mycode.FillData(query);

            ViewState["query"] = query;
            if (dt.Rows.Count == 0)
            {
                ViewState["flagdata"] = "0";
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                ViewState["flagdata"] = "1";
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }
    }
}