using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class home1 : System.Web.UI.Page
    {
        My mycod = new My();
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
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
                        if (Session["Hadmno"] != null && Session["Hfee_page"] != null && Session["HsessionId"] != null && Session["Hclass_id"] != null)
                        {
                            string HsessionId = Session["HsessionId"].ToString();
                            string Hadmno = Session["Hadmno"].ToString();
                            Session["Hadmno"] = null;
                            Session["Hfee_page"] = null;
                            Session["HsessionId"] = null;
                            Session["Hclass_id"] = null;
                            Response.Redirect("fees-collection-1.aspx?adm=" + Hadmno + "&sessionid=" + HsessionId);
                        }


                        //Session["Admin"] = "EDUN22022";
                        //Session["firm"] = "1";
                        //Session["branchid"] = "1";
                        //if (Session["PgRspnSE"] == null)
                        //{
                        //    get_firm_type();
                        //}
                        hd_is_payment_remainder.Value = "0";
                        Dictionary<string, object> dc1 = My.get_push_credantial();
                        ViewState["type"] = (String)dc1["type"];
                        ViewState["project_id"] = (String)dc1["project_id"];
                        ViewState["private_key_id"] = (String)dc1["private_key_id"];
                        ViewState["client_email"] = (String)dc1["client_email"];
                        ViewState["client_id"] = (String)dc1["client_id"];
                        ViewState["private_key"] = dc1["private_key"].ToString().Replace("\\n", "\n");




                        mycod.bind_all_ddl_with_id_notselect(ddl_months, "select Month,Month_Id from Month_Index order by Position asc");
                        ddl_months.SelectedValue = mycod.get_current_month_id();
                        hd_months.Value = ddl_months.SelectedValue;
                        ViewState["Branchid"] = mycod.get_branch_id(Session["Admin"].ToString());
                        get_firm_type();
                        string TodaydatEtim = mycod.date();
                        mycod.bind_all_ddl_with_id_cap_All(ddl_class_collection_month_overall, "select Course_Name,course_id from Add_course_table order by Position asc");


                        mycod.bind_all_ddl_with_id(ddlsession, "select Session,session_id from session_details");
                        mycod.bind_all_ddl_with_id_cap_All(ddlclass, "select Course_Name,course_id from Add_course_table order by Position asc");
                        mycod.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");

                        hd_session.Value = My.get_session_id();
                        hd_session_id.Value = hd_session.Value;
                        hd_session_name.Value = My.get_session();
                        hd_branch_id.Value = ViewState["Branchid"].ToString();
                        ddlsession.SelectedValue = hd_session.Value;

                        // CalculateExpectedCollection.calculateExpCollectionMonthwise(hd_session.Value, ViewState["Branchid"].ToString());
                        hd_payment_collec_class.Value = "0";
                        hd_payment_estd_class.Value = "0";
                        hd_payment_estd_class_adm.Value = "0";
                        hd_payment_estd_class_annual.Value = "0";
                        hd_payment_estd_class_overall.Value = "0";
                        hd_payment_estd_class_otherfee.Value = "0";
                        hd_form_sale_class_name.Value = "0";
                        hd_overall_collection_mnth_class.Value = "0";
                        //TenDayS
                        DateTime TenstartTime = DateTime.ParseExact(TodaydatEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string TenDaysDate = TenstartTime.AddDays(-10).ToShortDateString();
                        int TenDayS = My.DateConvertToIdate(TenDaysDate);
                        hd_TenDayS.Value = TenDayS.ToString();

                        //7DayS
                        DateTime SevenstartTime = DateTime.ParseExact(TodaydatEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string SevenDaysDate = SevenstartTime.AddDays(-7).ToShortDateString();
                        int SevenDayS = My.DateConvertToIdate(SevenDaysDate);
                        hd_SevenDayS.Value = SevenDayS.ToString();

                        bind_data(); get_today_collection(); //Bind_new_and_old_newsession_student_fee_taken(); 
                        Global gb = new Global();
                        try
                        {

                            bool tureturn2 = gb.send_birthday_message();
                        }
                        catch (Exception ex)
                        {
                        }

                        try
                        {
                            //bool tureturn = gb.update_database_and_version();
                        }
                        catch (Exception ex)
                        {
                        }

                        try
                        {
                            // sync and push send
                            //if (!code.syncdatastatusyes_or_no())
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowProgress();", true);
                            //    string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "load", script, true);
                            //    ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
                            //}
                            save_teacher_attendance();
                            update_teacher_routine();
                        }
                        catch (Exception ex)
                        {
                        }

                        try
                        {
                            fetch_total_new_and_total_old_student();
                        }
                        catch
                        {

                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "DefaultPage");
                }
            }
        }

        private void fetch_total_new_and_total_old_student()
        {
            lbl_total_new_student.InnerText = "0";
            lbl_total_old_student.InnerText = "0";
            string query = "Select top 1  admissionserialnumber,(select count (distinct admissionserialnumber) from admission_registor where Transfer_Status='New' and Status='1' and Session_id='"+ hd_session_id.Value + "' )  as totalnewstudent,(select count (distinct admissionserialnumber) from admission_registor where Transfer_Status='NT' and Status='1' and Session_id='"+ hd_session_id.Value + "') as total_old_student from admission_registor where   Status='1' and Session_id='"+ hd_session_id.Value + "' ";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_total_new_student.InnerText = dt.Rows[0]["totalnewstudent"].ToString();
                lbl_total_old_student.InnerText= dt.Rows[0]["total_old_student"].ToString();
            }

        }

        private void get_today_collection()
        {
            string idates = mycod.idate();  //"20240522"; //mycod.idate();  //"20240508";   //
            string sql = @"select isnull(sum(convert(float, paid)),0) as Total_tuition_fee from Monthly_Fee_Collection_Slip where (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and content_id!='1002' and content_id not in (select Content_id from Fee_type_for_hostel) and Idate='" + idates + @"';
                           select isnull(sum(convert(float, paid)),0) as Total_transportation_fee from Monthly_Fee_Collection_Slip where (parameter = 'MonthlyFee' or parameter = 'HostelMonthlyFee') and content_id = '1002' and Idate='" + idates + @"';
                           select isnull(sum(convert(float, paid)),0) as Total_hostel_fee from Monthly_Fee_Collection_Slip where content_id in (select Content_id from Fee_type_for_hostel where Is_For='Hostel') and Idate='" + idates + @"';
                           select isnull(sum(convert(float, paid)),0) as Total_admission_fee from Monthly_Fee_Collection_Slip where (parameter = 'AdmissionFee' or parameter = 'HostelAdmissionFee' or parameter = 'AnnualFee' or parameter = 'HostelAnnualFee') and content_id not in (select Content_id from Fee_type_for_hostel where Is_For='Hostel') and Idate='" + idates + @"';
                           select count(distinct Addmission_no) from Student_Payment_History where  Session='" + My.get_session() + @"'  and Addmission_no in (select admissionserialnumber from admission_registor where  Status=1 and Transfer_Status='New' );

                           select count(distinct Addmission_no) from dbo.[Student_Payment_History] where Session='" + My.get_session() + @"'  and Addmission_no in (select admissionserialnumber from admission_registor where Status=1 and Transfer_Status='NT');
                           
                           select isnull(sum(convert(float, paid)),0) as Total_tuition_fee from Monthly_Fee_Collection_Slip where (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and content_id!='1002' and content_id not in (select Content_id from Fee_type_for_hostel) and Created_idate='" + idates + @"';
                           select isnull(sum(convert(float, paid)),0) as Total_transportation_fee from Monthly_Fee_Collection_Slip where (parameter = 'MonthlyFee' or parameter = 'HostelMonthlyFee') and content_id = '1002' and Created_idate='" + idates + @"';
                           select isnull(sum(convert(float, paid)),0) as Total_hostel_fee from Monthly_Fee_Collection_Slip where content_id in (select Content_id from Fee_type_for_hostel where Is_For='Hostel') and Created_idate='" + idates + @"';
                           select isnull(sum(convert(float, paid)),0) as Total_admission_fee from Monthly_Fee_Collection_Slip where (parameter = 'AdmissionFee' or parameter = 'HostelAdmissionFee' or parameter = 'AnnualFee' or parameter = 'HostelAnnualFee') and content_id not in (select Content_id from Fee_type_for_hostel where Is_For='Hostel') and Created_idate='" + idates + "';";
            DataSet ds = mycod.Fill_Data_set(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                double tuion_fee = 0, transport_fee = 0, hostel_fee = 0, admission_fee = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[0];
                    if (dtTemp.Rows.Count > 0)
                    {
                        tuion_fee = My.toDouble(dtTemp.Rows[0]["Total_tuition_fee"].ToString());
                        lbl_ttl_tuition_fee.Text = dtTemp.Rows[0]["Total_tuition_fee"].ToString();
                    }
                    else
                    {
                        lbl_ttl_tuition_fee.Text = "0.00";
                    }
                }
                else
                {
                    lbl_ttl_tuition_fee.Text = "0.00";
                }

                //====================
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[1];
                    if (dtTemp.Rows.Count != 0)
                    {
                        transport_fee = My.toDouble(dtTemp.Rows[0]["Total_transportation_fee"].ToString());
                        lbl_ttl_transport_fee.Text = dtTemp.Rows[0]["Total_transportation_fee"].ToString();
                    }
                    else
                    {
                        lbl_ttl_transport_fee.Text = "0.00";
                    }
                }
                else
                {
                    lbl_ttl_transport_fee.Text = "0.00";
                }

                //====================
                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[2];
                    if (dtTemp.Rows.Count != 0)
                    {
                        hostel_fee = My.toDouble(dtTemp.Rows[0]["Total_hostel_fee"].ToString());
                        lbl_hostel_fee.Text = dtTemp.Rows[0]["Total_hostel_fee"].ToString();
                    }
                    else
                    {
                        lbl_hostel_fee.Text = "0.00";
                    }
                }
                else
                {
                    lbl_hostel_fee.Text = "0.00";
                }

                //====================
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[3];
                    if (dtTemp.Rows.Count != 0)
                    {
                        admission_fee = My.toDouble(dtTemp.Rows[0]["Total_admission_fee"].ToString());
                        lbl_admission_fees.Text = dtTemp.Rows[0]["Total_admission_fee"].ToString();
                    }
                    else
                    {
                        lbl_admission_fees.Text = "0.00";
                    }
                }
                else
                {
                    lbl_admission_fees.Text = "0.00";
                }
                lbl_total_collection.Text = (tuion_fee + transport_fee + hostel_fee + admission_fee).ToString("0.00");



                //====================AdmissionFeeAnnualFeeTaken
                if (ds.Tables[4].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[4];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_admission_fee_taken.InnerText = dtTemp.Rows[0][0].ToString();
                    }
                    else
                    {
                        lbl_total_admission_fee_taken.InnerText = "0.00";
                    }
                }
                else
                {
                    lbl_total_admission_fee_taken.InnerText = "0.00";
                }

                if (ds.Tables[5].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[5];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_annwal_fee.InnerText = dtTemp.Rows[0][0].ToString();
                    }
                    else
                    {
                        lbl_total_annwal_fee.InnerText = "0.00";
                    }
                }
                else
                {
                    lbl_total_annwal_fee.InnerText = "0.00";
                }
                //====================AdmissionFeeAnnualFeeTaken




                double tuion_fee_today = 0, transport_fee_today = 0, hostel_fee_today = 0, admission_fee_today = 0;
                if (ds.Tables[6].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[6];
                    if (dtTemp.Rows.Count > 0)
                    {
                        tuion_fee_today = My.toDouble(dtTemp.Rows[0]["Total_tuition_fee"].ToString());
                        lbl_ttl_tuition_fee_entry.Text = dtTemp.Rows[0]["Total_tuition_fee"].ToString();
                    }
                    else
                    {
                        lbl_ttl_tuition_fee_entry.Text = "0.00";
                    }
                }
                else
                {
                    lbl_ttl_tuition_fee_entry.Text = "0.00";
                }

                //====================
                if (ds.Tables[7].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[7];
                    if (dtTemp.Rows.Count != 0)
                    {
                        transport_fee_today = My.toDouble(dtTemp.Rows[0]["Total_transportation_fee"].ToString());
                        lbl_ttl_transport_fee_entry.Text = dtTemp.Rows[0]["Total_transportation_fee"].ToString();
                    }
                    else
                    {
                        lbl_ttl_transport_fee_entry.Text = "0.00";
                    }
                }
                else
                {
                    lbl_ttl_transport_fee_entry.Text = "0.00";
                }

                //====================
                if (ds.Tables[8].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[8];
                    if (dtTemp.Rows.Count != 0)
                    {
                        hostel_fee_today = My.toDouble(dtTemp.Rows[0]["Total_hostel_fee"].ToString());
                        lbl_hostel_fee_entry.Text = dtTemp.Rows[0]["Total_hostel_fee"].ToString();
                    }
                    else
                    {
                        lbl_hostel_fee_entry.Text = "0.00";
                    }
                }
                else
                {
                    lbl_hostel_fee_entry.Text = "0.00";
                }

                //====================
                if (ds.Tables[9].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[9];
                    if (dtTemp.Rows.Count != 0)
                    {
                        admission_fee_today = My.toDouble(dtTemp.Rows[0]["Total_admission_fee"].ToString());
                        lbl_admission_fees_entry.Text = dtTemp.Rows[0]["Total_admission_fee"].ToString();
                    }
                    else
                    {
                        lbl_admission_fees_entry.Text = "0.00";
                    }
                }
                else
                {
                    lbl_admission_fees_entry.Text = "0.00";
                }
                lbl_total_collection_entry.Text = (tuion_fee_today + transport_fee_today + hostel_fee_today + admission_fee_today).ToString("0.00");
            }
        }


        //private void Bind_new_and_old_newsession_student_fee_taken()
        //{
        //    string query = " select count(distinct Addmission_no) from dbo.[Student_Payment_History] where Type='Admission' and Session='" + My.get_session() + "' and Addmission_no in (select admissionserialnumber from admission_registor where session=Student_Payment_History.Session and Status=1)";
        //    SqlCommand cmd = new SqlCommand(query);
        //    DataTable dtTemp = mycod.GetData(cmd);
        //    if (dtTemp.Rows.Count == 0)
        //    {
        //        lbl_total_admission_fee_taken.InnerText = "0";
        //    }
        //    else
        //    {
        //        lbl_total_admission_fee_taken.InnerText = dtTemp.Rows[0][0].ToString();
        //    }

        //    string query1 = " select count(distinct Addmission_no) from dbo.[Student_Payment_History] where Type='Annual' and Session='" + My.get_session() + "'  and Addmission_no in (select admissionserialnumber from admission_registor where session=Student_Payment_History.Session and Status=1)";
        //    SqlCommand cmd1 = new SqlCommand(query1);
        //    DataTable dtTemp1 = mycod.GetData(cmd1);
        //    if (dtTemp1.Rows.Count == 0)
        //    {
        //        lbl_total_annwal_fee.InnerText = "0";
        //    }
        //    else
        //    {
        //        lbl_total_annwal_fee.InnerText = dtTemp1.Rows[0][0].ToString();
        //    }
        //}

        private void update_teacher_routine()
        {
            DataTable dt = My.dataTable("select * from Class_routine_teacher_update where Idate=" + mycod.idate() + "");
            if (dt.Rows.Count == 0)
            {
                routineUpdate.update_teacher_routine(hd_session_id.Value, hd_branch_id.Value, Session["Admin"].ToString());
                My.exeSql("insert into Class_routine_teacher_update(Date,Idate,Updated) values ('" + mycod.date() + "','" + mycod.idate() + "','1');");
            }
        }

        private void save_teacher_attendance()
        {
            DataTable dt = My.dataTable("select is_teacher_attendance_calculate from globle_data");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["is_teacher_attendance_calculate"].ToString() == "1")
                {
                    routineUpdate.update_teacher_attendance(hd_session_id.Value, hd_branch_id.Value, Session["Admin"].ToString());
                }
            }
        }



        private void get_firm_type()
        {
            try
            {
                todaysGathredEntry.Visible = false;
                string query = "Select * from Firm_Details  ";
                SqlCommand cmd = new SqlCommand(query);
                DataTable dtTemp = mycod.GetData(cmd);
                if (dtTemp.Rows.Count != 0)
                {
                    Session["PgRspnSE"] = "1";
                    if (dtTemp.Rows[0]["firm_id"].ToString() == "MSM-01")
                    {
                        todaysGathredEntry.Visible = true;
                    }
                    check_payment(dtTemp.Rows[0]["firm_id"].ToString());




                    if (dtTemp.Rows[0]["firm_id"].ToString() == "NPI-01")
                    {
                        if (Session["userTypeFee"].ToString() == "Admin" || Session["userTypeFee"].ToString() == "Accountant")
                        {
                        
                        }
                        else
                        {
                            countLeft.Attributes.Add("class", "col-xl-12");
                            todayCollection.Visible = false;
                        }
                    }

                    if (dtTemp.Rows[0]["firm_id"].ToString() == "BD-001")
                    {
                        countLeft.Attributes.Add("class", "col-xl-12");
                        todayCollection.Visible = false;
                        graph.Visible = false;
                    }

                        //=======================================================================
                        try
                    {
                        if (dtTemp.Rows[0]["Is_internal_chat_active"].ToString() == "1")
                        {
                            chatDV.Visible = true;
                            chatLink.HRef = "../Chat/home?regid=" + Session["Admin"].ToString();
                        }
                    }
                    catch (Exception ex) { }

                    try
                    {
                        if (dtTemp.Rows[0]["Online_payment_failed_Notification_show"].ToString() == "1")
                        {

                            fetch_payment_failed();
                        }
                        else
                        {
                            notificationBell_payment.Attributes.Add("class", "hidden");
                        }
                    }
                    catch
                    {
                        notificationBell_payment.Attributes.Add("class", "hidden");

                    }
                }
              

            }
            catch
            {
                Session["PgRspnSE"] = "1";
            }
          
            //get_total_student_admission_annual

        }

        private void fetch_payment_failed()
        {
            lblpaymentfailed.InnerText = "0";
            DataTable dt = My.dataTable("select count(Id) as Count from Payment_transaction_process where status='Pending' and Session_id='" + My.get_session_id() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lblpaymentfailed.InnerText= dt.Rows[0][0].ToString();
                 
            }
        }

        compLN complain = new compLN();
        private void check_payment(string firm_id)
        {
            try
            {
                if (complain.IsUserExist("select Firm_id from School_details where Is_remainder_show_home=1 and Firm_id='" + firm_id + "'"))
                {
                    DataTable dt = compLN.dataTable_comp("select *,convert(float, Total_amount) as TtlFloat from School_billing_details where Firm_id='" + firm_id + "' and Payment_status='Dues' order by Month_position asc");
                    if (dt.Rows.Count > 0)
                    {
                        RPPDetails.DataSource = dt;
                        RPPDetails.DataBind();
                        lbl_Total_final_amount.Text = Convert.ToDouble(dt.Compute("SUM(TtlFloat)", string.Empty)).ToString("0.00");
                        hd_is_payment_remainder.Value = "1";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openPaymentAlert();", true);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_data()
        {
            string sessioid = My.get_session_id();
            string datEtim = mycod.date();
            DateTime startTime = DateTime.ParseExact(datEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string OneWeekDate = startTime.AddDays(-7).ToShortDateString();
            int oneWeek = My.DateConvertToIdate(OneWeekDate);


            // int oneWeek = 20210801;

            string sql = @"select  count(id) as ttl_admisiion from admission_registor  where   (Transfer_Status='New' or Transfer_Status='NT') and Status='1' and   Session_id='" + sessioid + @"';
                                       select  count(id) as ttl_admisiion_last_week from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and Status='1'  and   Session_id='" + sessioid + @"' and admission_idate>" + oneWeek + @";

                                       select count(id) as ttl_admisiion_in_hostel from admission_registor where hosteltaken='Yes' and   Session_id='" + sessioid + @"' and (Transfer_Status='New' or Transfer_Status='NT') and Status='1' ;
                                       select count(id) as ttl_admisiion_in_hostel_lst_week from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and Status='1'  and hosteltaken='Yes' and   Session_id='" + sessioid + "' and admission_idate>" + oneWeek + @";

                                       select count(id) as ttl_admisiion_in_days from admission_registor where   admissionserialnumber not in( select Admission_no from dbo.[Student_mapping_with_boarding_with_lunch] where Session_id=" + sessioid + " ) and hosteltaken='No' and (Transfer_Status='New' or Transfer_Status='NT') and Status='1'and Session_id='" + sessioid + @"';

                                        select count(id) as ttl_admisiion_in_days_lst_week from admission_registor where admissionserialnumber not in( select Admission_no from dbo.[Student_mapping_with_boarding_with_lunch] where Session_id='" + sessioid + "' ) and (Transfer_Status='New' or Transfer_Status='NT')  and Status='1'  and hosteltaken='No' and   Session_id='" + sessioid + "' and admission_idate>" + oneWeek + @";
            select count(id) as active from admission_registor where (Transfer_Status='New' or Transfer_Status='NT')  and Status='1' and     Session_id='" + sessioid + @"';
            select count(id) as inactive from admission_registor where (Transfer_Status='New' or Transfer_Status='NT')  and Status in ('0','')  and    Session_id=" + sessioid + @";
         select count(id) as totaldob from admission_registor where (Transfer_Status='New' or Transfer_Status='NT')  and Status in ('1')  and    (dob like'%" + mycod.day_month_2() + "%' or dob like'%" + mycod.day_month() + "%')   and  Session_id=" + sessioid + @";
          select count(id) as totalBus from admission_registor where (Transfer_Status='New' or Transfer_Status='NT')  and Status in ('1')  and    transportationtaken='Yes'   and  Session_id=" + sessioid + "";





            DataSet ds = mycod.Fill_Data_set(sql);  
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[0];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlodR.InnerText = dtTemp.Rows[0]["ttl_admisiion"].ToString();
                }
                else
                {
                    ttlodR.InnerText = "00";
                }
            }
            else
            {
                ttlodR.InnerText = "00"; ;
            }


            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    DataTable dtTemp = ds.Tables[1];
            //    if (dtTemp.Rows.Count != 0)
            //    {
            //        ttlodRLstWeeK.InnerText = dtTemp.Rows[0]["ttl_admisiion_last_week"].ToString() + "+ from last week";
            //    }
            //    else
            //    {
            //        ttlodRLstWeeK.InnerText = "00";
            //    }
            //}
            //else
            //{
            //    ttlodRLstWeeK.InnerText = "00"; ;
            //}

            //============

            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[2];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlRvnuE.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_hostel"].ToString();
                }
                else
                {
                    ttlRvnuE.InnerText = "00";
                }
            }
            else
            {
                ttlRvnuE.InnerText = "00"; ;
            }

            //if (ds.Tables[3].Rows.Count > 0)
            //{
            //    DataTable dtTemp = ds.Tables[3];
            //    if (dtTemp.Rows.Count != 0)
            //    {
            //        ttlRevenueLstWeeK.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_hostel_lst_week"].ToString() + "+ from last week";
            //    }
            //    else
            //    {
            //        ttlRevenueLstWeeK.InnerText = "00";
            //    }
            //}
            //else
            //{
            //    ttlRevenueLstWeeK.InnerText = "00"; ;
            //}


            //============

            if (ds.Tables[4].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[4];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlCancelAmt.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_days"].ToString();
                }
                else
                {
                    ttlCancelAmt.InnerText = "00";
                }
            }
            else
            {
                ttlCancelAmt.InnerText = "00"; ;
            }


            //if (ds.Tables[5].Rows.Count > 0)
            //{
            //    DataTable dtTemp = ds.Tables[5];
            //    if (dtTemp.Rows.Count != 0)
            //    {
            //        ttlCancelAmtLstWeek.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_days_lst_week"].ToString() + "+ from last week";
            //    }
            //    else
            //    {
            //        ttlCancelAmtLstWeek.InnerText = "00";
            //    }
            //}
            //else
            //{
            //    ttlCancelAmtLstWeek.InnerText = "00"; ;
            //}
            // active
            //if (ds.Tables[6].Rows.Count > 0)
            //{
            //    DataTable dtTemp = ds.Tables[6];
            //    if (dtTemp.Rows.Count != 0)
            //    {
            //        H1active.InnerText = dtTemp.Rows[0]["active"].ToString();
            //    }
            //    else
            //    {
            //        H1active.InnerText = "00";
            //    }
            //}
            //else
            //{
            //    H1active.InnerText = "00"; ;
            //}
            // inactive
            if (ds.Tables[7].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[7];
                if (dtTemp.Rows.Count != 0)
                {
                    h2_inactive.InnerText = dtTemp.Rows[0]["inactive"].ToString();
                }
                else
                {
                    h2_inactive.InnerText = "00";
                }
            }
            else
            {
                h2_inactive.InnerText = "00"; ;
            }

            // dob

            if (ds.Tables[8].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[8];
                if (dtTemp.Rows.Count != 0)
                {
                    lbl_todaybirtday.InnerText = dtTemp.Rows[0]["totaldob"].ToString();
                }
                else
                {
                    lbl_todaybirtday.InnerText = "00";
                }
            }
            else
            {
                lbl_todaybirtday.InnerText = "00"; ;
            }

            //BUS
            if (ds.Tables[9].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[9];
                if (dtTemp.Rows.Count != 0)
                {
                    lbl_total_bus.InnerText = dtTemp.Rows[0]["totalBus"].ToString();
                }
                else
                {
                    lbl_total_bus.InnerText = "00";
                }
            }
            else
            {
                lbl_todaybirtday.InnerText = "00"; ;
            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool abc1 = PayrollMy.insert_HR_Attendance_log();
                bool abc = PayrollMy.update_HR_Daily_Attendance_Record("Device Attendance");
                bool abc2 = PayrollMy.update_one_Row_data_Attendance_Record();
                synch_data();
                //System.Threading.Thread.Sleep(3000);
                a1.Visible = false;

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }

        }
        public static List<school_web.Global.mysmslis> smsList = new List<school_web.Global.mysmslis>();
        public static List<school_web.Global.myclass> class1 = new List<school_web.Global.myclass>();
        public static List<school_web.Global.sendpush> push = new List<school_web.Global.sendpush>();
        public static List<school_web.Global.sendpushMessage> pushMessage = new List<school_web.Global.sendpushMessage>();
        public static List<school_web.Global.SendpushNoticeTeacher> pushNoticeboard_teacher = new List<school_web.Global.SendpushNoticeTeacher>();
        public static List<school_web.Global.SendpushMessageTeacher> pushmessage_teacher = new List<school_web.Global.SendpushMessageTeacher>();
        public static List<school_web.Global.SendpushMessageAttandance> pushmessage_attandance = new List<school_web.Global.SendpushMessageAttandance>();

        public static List<school_web.Global.update_Student_Payment_History> paymnet_history = new List<school_web.Global.update_Student_Payment_History>();
        public static List<school_web.Global.sendpushEvents> Eventspush = new List<school_web.Global.sendpushEvents>();

        public static List<school_web.Global.sendlog_book> log_book = new List<school_web.Global.sendlog_book>();
        public bool flagstudent = true;
        public bool flagclass = true;
        public bool flagsendpush = true;
        public bool flagsendpushMesaage = true;
        public bool flagsendpush_notice_Teacher = true;
        public bool flagsendpush_message_Teacher = true;
        public bool flagsendpush_message_attandance = true;
        public bool flagshtudent_paymnet_history = true;
        public static List<school_web.Global.Sendclass_Activity> class_Activity = new List<school_web.Global.Sendclass_Activity>();
        public static List<school_web.Global.sendpushcalendar> pushcalendar = new List<school_web.Global.sendpushcalendar>();
        public static List<school_web.Global.sendpushSyllubsh> Syllubsh = new List<school_web.Global.sendpushSyllubsh>();
        public static List<school_web.Global.sendpushChairman> Chairman = new List<school_web.Global.sendpushChairman>();
        public static List<school_web.Global.sendpushSCHOOLHISTORY> SCHOOLHISTORY = new List<school_web.Global.sendpushSCHOOLHISTORY>();

        public static List<school_web.Global.sendpushPRINCIPAL> Principal = new List<school_web.Global.sendpushPRINCIPAL>();
        public static List<school_web.Global.sendpushstudent_routine> student_routine = new List<school_web.Global.sendpushstudent_routine>();
        public bool flagclass_Activity = true;
        public bool flagEventspush = true;
        public bool flaglog_bookpush = true;
        public bool flaglog_calendarpush = true;
        public bool flaglog_Syllubsh = true;
        public bool flaglog_Chairman = true;
        public bool flaglog_schooldetails = true;
        public bool flaglog_Principal = true;
        public bool flaglog_subjectroutine = true;
        public bool flaglog_subjectassined = true;
        public static List<school_web.Global.Sendpushpush_subjectassined> subjectassined = new List<school_web.Global.Sendpushpush_subjectassined>();
        private void synch_data()
        {
            try
            {
                if (flagshtudent_paymnet_history == false)
                {
                    return;
                }
                else
                {
                    bind_list_student_paymnet();
                    foreach (school_web.Global.update_Student_Payment_History item in paymnet_history.ToList())
                    {
                        if (item.parameter_New == "0")
                        {
                            flagshtudent_paymnet_history = true;
                        }
                        else
                        {
                            flagshtudent_paymnet_history = false;
                            send_paymnet_history(item);
                        }
                    }
                    flagshtudent_paymnet_history = true;

                }
            }
            catch
            {
            }

            //if (flaglog_subjectassined == false)
            //{
            //    return;
            //}
            //else
            //{
            //    Bind_push_list.bind_student_subjectassined(subjectassined);
            //    foreach (school_web.Global.Sendpushpush_subjectassined item in subjectassined.ToList())
            //    {
            //        if (item.id == "0")
            //        {
            //            flaglog_subjectassined = true;
            //        }
            //        else
            //        {
            //            flaglog_subjectassined = false;
            //            Bind_push_list.sendpushstudent_subjectassined(item, ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());
            //        } 
            //    }
            //    flaglog_subjectassined = true;
            //}



            //if (flaglog_subjectroutine == false)
            //{
            //    return;
            //}
            //else
            //{
            //    Bind_push_list.bind_studentroutine_data(student_routine);
            //    foreach (school_web.Global.sendpushstudent_routine item in student_routine.ToList())
            //    {
            //        if (item.id == "0")
            //        {
            //            flaglog_subjectroutine = true;
            //        }
            //        else
            //        {
            //            flaglog_subjectroutine = false;
            //            Bind_push_list.sendpushstudent_routine(item, ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());
            //        }
            //    }
            //    flaglog_subjectroutine = true;
            //}
            #region hide school details code
            //if (flaglog_schooldetails == false)
            //{
            //    return;
            //}
            //else
            //{
            //    Bind_push_list.bind_SCHOOLHISTORY_data(SCHOOLHISTORY);
            //    foreach (school_web.Global.sendpushSCHOOLHISTORY item in SCHOOLHISTORY.ToList())
            //    {
            //        if (item.id == "0")
            //        {
            //            flaglog_schooldetails = true;
            //        }
            //        else
            //        {
            //            flaglog_schooldetails = false;
            //            Bind_push_list.send_pushSCHOOLHISTORY_student(item);
            //        }


            //    }
            //    flaglog_schooldetails = true;
            //}

            #endregion
            #region hide school details code
            //if (flaglog_Principal == false)
            //{
            //    return;
            //}
            //else
            //{
            //    Bind_push_list.bind_Principal_data(Principal);
            //    foreach (school_web.Global.sendpushPRINCIPAL item in Principal.ToList())
            //    {
            //        if (item.id == "0")
            //        {
            //            flaglog_Principal = true;
            //        }
            //        else
            //        {
            //            flaglog_Principal = false;
            //            Bind_push_list.send_pushPrincipal_student(item);
            //        }


            //    }
            //    flaglog_Principal = true;
            //}
            //if (flaglog_Chairman == false)
            //{
            //    return;
            //}
            //else
            //{
            //    Bind_push_list.bind_Chairman_data(Chairman);
            //    foreach (school_web.Global.sendpushChairman item in Chairman.ToList())
            //    {
            //        if (item.id == "0")
            //        {
            //            flaglog_Chairman = true;
            //        }
            //        else
            //        {
            //            flaglog_Chairman = false;
            //            Bind_push_list.send_pushChairman_student(item);
            //        }


            //    }
            //    flaglog_Chairman = true;
            //}

            #endregion
            if (flaglog_Syllubsh == false)
            {
                return;
            }
            else
            {
                Bind_push_list.bind_Syllubsh_data(Syllubsh);
                foreach (school_web.Global.sendpushSyllubsh item in Syllubsh.ToList())
                {
                    if (item.Class_id == "0")
                    {
                        flaglog_Syllubsh = true;
                    }
                    else
                    {
                        flaglog_Syllubsh = false;
                        Bind_push_list.send_pushSyllubsh_student(item, ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());
                    }


                }
                flaglog_Syllubsh = true;
            }

            //if (flaglog_calendarpush == false)
            //{
            //    return;
            //}
            //else
            //{
            //    Bind_push_list.bind_calendar_data(pushcalendar);
            //    foreach (school_web.Global.sendpushcalendar item in pushcalendar.ToList())
            //    {
            //        if (item.classid == "0")
            //        {
            //            flaglog_calendarpush = true;
            //        }
            //        else
            //        {
            //            flaglog_calendarpush = false;
            //            Bind_push_list.send_pushcalendar_student(item, ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());
            //        }


            //    }
            //    flaglog_calendarpush = true;
            //}

            if (flaglog_bookpush == false)
            {
                return;
            }
            else
            {
                Bind_push_list.bind_log_book(log_book);
                foreach (school_web.Global.sendlog_book item in log_book.ToList())
                {
                    if (item.Class_id == "0")
                    {
                        flaglog_bookpush = true;
                    }
                    else
                    {
                        flaglog_bookpush = false;
                        Bind_push_list.send_pushlogbook_student(item, ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());
                    }
                }
                flaglog_bookpush = true;
            }
            if (flagEventspush == false)
            {
                return;
            }
            else
            {
                Bind_push_list.bind_News_Events_Details(Eventspush);
                foreach (school_web.Global.sendpushEvents item in Eventspush.ToList())
                {
                    if (item.Class == "0")
                    {
                        flagEventspush = true;
                    }
                    else
                    {
                        flagEventspush = false;
                        Bind_push_list.send_pushEvents_student(item, ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());
                    }


                }
                flagEventspush = true;
            }

            if (flagclass_Activity == false)
            {
                return;
            }
            else
            {
                Bind_push_list.bind_class_Activity_push(class_Activity);
                foreach (school_web.Global.Sendclass_Activity item in class_Activity.ToList())
                {
                    if (item.Class_id == "0")
                    {
                        flagclass_Activity = true;
                    }
                    else
                    {
                        flagclass_Activity = false;
                        Bind_push_list.send_push_class_Activity(item, ViewState["type"].ToString(), ViewState["project_id"].ToString(), ViewState["private_key_id"].ToString(), ViewState["client_email"].ToString(), ViewState["client_id"].ToString(), ViewState["private_key"].ToString());
                    }


                }
                flagclass_Activity = true;
            }


            //try
            //{
            //    My.exeSql("Alter Table dbo.[Student_Payment_History] Add [parameter_New] varchar (500)  ;");
            //}
            //catch
            //{

            //}





            if (flagsendpush == false)
            {
                return;
            }
            else
            {
                bind_list_push();
                foreach (school_web.Global.sendpush item in push.ToList())
                {
                    if (item.Class1 == "0")
                    {
                        flagsendpush = true;
                    }
                    else
                    {
                        flagsendpush = false;
                        send_push(item);
                    }


                }
                flagsendpush = true;

            }

            if (flagsendpushMesaage == false)
            {
                return;
            }
            else
            {
                bind_list_pushmessage();
                foreach (school_web.Global.sendpushMessage item in pushMessage.ToList())
                {
                    if (item.Class1 == "0")
                    {
                        flagsendpushMesaage = true;
                    }
                    else
                    {
                        flagsendpushMesaage = false;
                        send_pushMessage(item);
                    }


                }
                flagsendpushMesaage = true;

            }


            if (flagsendpush_notice_Teacher == false)
            {
                return;
            }
            else
            {
                bind_list_push_notice_teacher();
                foreach (school_web.Global.SendpushNoticeTeacher item in pushNoticeboard_teacher.ToList())
                {
                    if (item.Teacher_Id == "0")
                    {
                        flagsendpush_notice_Teacher = true;
                    }
                    else
                    {
                        flagsendpush_notice_Teacher = false;
                        send_push_notice_teacher(item);
                    }


                }
                flagsendpush_notice_Teacher = true;

            }

            if (flagsendpush_message_Teacher == false)
            {
                return;
            }
            else
            {
                bind_list_push_message_teacher();
                foreach (school_web.Global.SendpushMessageTeacher item in pushmessage_teacher.ToList())
                {
                    if (item.Teacher_Id == "0")
                    {
                        flagsendpush_message_Teacher = true;
                    }
                    else
                    {
                        flagsendpush_message_Teacher = false;
                        send_push_message_teacher(item);
                    }


                }
                flagsendpush_message_Teacher = true;

            }

            //--------------------------------push send_push attndance-----------------
            if (flagsendpush_message_attandance == false)
            {
                return;
            }
            else
            {
                bind_list_push_attndance();
                foreach (school_web.Global.SendpushMessageAttandance item in pushmessage_attandance.ToList())
                {
                    if (item.Id == "0")
                    {
                        flagsendpush_message_attandance = true;
                    }
                    else
                    {
                        flagsendpush_message_attandance = false;
                        send_push_attandance(item);
                    }


                }
                flagsendpush_message_attandance = true;

            }
        }

        private void send_paymnet_history(Global.update_Student_Payment_History item)
        {
            mycod.executequery("update Student_Payment_History set parameter_New='" + item.parameter_New + "' where Slip_no='" + item.slip_id + "' ");
        }

        private void send_push_attandance(Global.SendpushMessageAttandance item)
        {
            string query = "";
            query = " select top 1 gcm_id,admissionserialnumber from dbo.[admission_registor] where   admissionserialnumber='" + item.AdmissionNo + "' order by id desc";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Attendance_Notification set Send_status=1 where Id=" + item.Id + "");
            }
            else
            {
                string gcmid = dt.Rows[0]["gcm_id"].ToString();
                string admissionserialnumber = dt.Rows[0]["admissionserialnumber"].ToString();
                if (gcmid == "")
                {
                    gcmid = "0";
                }
                if (gcmid != "")
                {
                    Dictionary<String, String> ss = new Dictionary<string, string>();
                    ss["notification_id"] = Guid.NewGuid().ToString();
                    ss["message"] = item.Details;
                    ss["title"] = "Attendance Notification";
                    ss["messagetype"] = "Message";
                    ss["url"] = "";
                    ss["link_url"] = "";
                    ss["UserId"] = admissionserialnumber;
                    ss["type"] = ViewState["type"].ToString();
                    ss["project_id"] = ViewState["project_id"].ToString();
                    ss["private_key_id"] = ViewState["private_key_id"].ToString();
                    ss["client_email"] = ViewState["client_email"].ToString();
                    ss["client_id"] = ViewState["client_id"].ToString();
                    ss["private_key"] = ViewState["private_key"].ToString();
                    My.onlypush(gcmid, ss);
                }
                code.executequery("update Attendance_Notification set Send_status=1 where Id=" + item.Id + "");
            }
        }

        private void bind_list_push_attndance()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Attendance_Notification] where Send_status=0 ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushmessage_attandance.Clear();
                pushmessage_attandance.Add(new school_web.Global.SendpushMessageAttandance
                {
                    Details = "0",
                    Id = "0",
                    AdmissionNo = "0",




                });
            }
            else
            {
                pushmessage_attandance.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushmessage_attandance.Add(new school_web.Global.SendpushMessageAttandance
                    {
                        Details = dr["Details"].ToString(),
                        Id = dr["Id"].ToString(),
                        AdmissionNo = dr["AdmissionNo"].ToString(),

                    });
                }
            }
        }


        #region teacher push message for teacher
        private void send_push_message_teacher(Global.SendpushMessageTeacher item)
        {
            string query = "";

            if (item.Teacher_Id == "ALL")
            {

                query = " select gcm_id,user_id from dbo.[user_details] where (gcm_id is not null or gcm_id!='') ";



            }
            else
            {
                query = " select gcm_id,user_id from dbo.[user_details] where   user_id='" + item.Teacher_Id + "'";

            }




            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Private_Messages_For_Teacher set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string UserID = dt.Rows[i]["user_id"].ToString();
                    if (gcmid == "")

                    {
                        gcmid = "";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Details;
                        ss["title"] = item.Subject;
                        ss["messagetype"] = "Message";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = UserID;
                        ss["type"] = ViewState["type"].ToString();
                        ss["project_id"] = ViewState["project_id"].ToString();
                        ss["private_key_id"] = ViewState["private_key_id"].ToString();
                        ss["client_email"] = ViewState["client_email"].ToString();
                        ss["client_id"] = ViewState["client_id"].ToString();
                        ss["private_key"] = ViewState["private_key"].ToString();
                        My.onlypush(gcmid, ss);
                    }

                }
                code.executequery("update Private_Messages_For_Teacher set Send_Status='Send' where Id=" + item.Id + "");
            }
        }
        private void bind_list_push_message_teacher()
        {

            SqlCommand cmd = new SqlCommand(" select * from dbo.[Private_Messages_For_Teacher] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushmessage_teacher.Clear();
                pushmessage_teacher.Add(new school_web.Global.SendpushMessageTeacher
                {

                    Subject = "0",
                    Details = "0",
                    Id = "0",
                    Teacher_Id = "0",




                });
            }
            else
            {
                pushmessage_teacher.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushmessage_teacher.Add(new school_web.Global.SendpushMessageTeacher
                    {

                        Subject = dr["Subject"].ToString(),
                        Details = dr["Details"].ToString(),
                        Id = dr["Id"].ToString(),
                        Teacher_Id = dr["Teacher_Id"].ToString(),

                    });
                }
            }
        }
        #endregion




        #region teacher push bind notice
        private void bind_list_push_notice_teacher()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Notice_Board_Details_Teacher] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushNoticeboard_teacher.Clear();
                pushNoticeboard_teacher.Add(new school_web.Global.SendpushNoticeTeacher
                {

                    Notice = "0",
                    Id = "0",
                    Teacher_Id = "0",




                });
            }
            else
            {
                pushNoticeboard_teacher.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    pushNoticeboard_teacher.Add(new school_web.Global.SendpushNoticeTeacher
                    {

                        Notice = dr["Notice"].ToString(),
                        Id = dr["Id"].ToString(),
                        Teacher_Id = dr["Teacher_Id"].ToString(),

                    });
                }
            }
        }
        private void send_push_notice_teacher(Global.SendpushNoticeTeacher item)
        {
            string query = "";

            if (item.Teacher_Id == "ALL")
            {

                query = " select gcm_id,user_id from dbo.[user_details]  ";



            }
            else
            {
                query = " select gcm_id,user_id from dbo.[user_details] where   user_id='" + item.Teacher_Id + "'";

            }




            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Notice_Board_Details_Teacher set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string UserID = dt.Rows[i]["user_id"].ToString();
                    if (gcmid == "")
                    {
                        gcmid = "0";

                    }


                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = item.Notice;
                        ss["title"] = "Notice";
                        ss["messagetype"] = "Notice";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = UserID;
                        ss["type"] = ViewState["type"].ToString();
                        ss["project_id"] = ViewState["project_id"].ToString();
                        ss["private_key_id"] = ViewState["private_key_id"].ToString();
                        ss["client_email"] = ViewState["client_email"].ToString();
                        ss["client_id"] = ViewState["client_id"].ToString();
                        ss["private_key"] = ViewState["private_key"].ToString();
                        My.onlypush(gcmid, ss);
                    }

                }
                code.executequery("update Notice_Board_Details_Teacher set Send_Status='Send' where Id=" + item.Id + "");
            }
        }
        #endregion
        #region send push notice board
        private void send_push(school_web.Global.sendpush item)
        {
            string query = "";

            if (item.Send_Type == "Class Wise")
            {
                if (item.Class1 == "ALL")
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Session_id='" + My.get_session_id() + "' ";
                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Section='" + item.Section + "' and Session_id='" + My.get_session_id() + "'";
                    }

                }
                else
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Class_id='" + item.Class1 + "' and Session_id='" + My.get_session_id() + "'";

                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Class_id='" + item.Class1 + "' and Section='" + item.Section + "' and Session_id='" + My.get_session_id() + "'";
                    }

                }
            }
            else
            {
                query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   admissionserialnumber='" + item.Admission_no + "' and Session_id='" + My.get_session_id() + "' ";

            }
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Notice_Board_Details set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    final_send_push(gcmid, admissionserialnumber, item);
                }
                code.executequery("update Notice_Board_Details set Send_Status='Send' where Id=" + item.Id + "");
            }
        }

        private void final_send_push(string gcmid, string admissionserialnumber, school_web.Global.sendpush item)
        {
            if (gcmid == "")
            {
                gcmid = "0";
            }

            if (gcmid != "")
            {
                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = item.Notice;
                ss["title"] = "Notice";
                ss["messagetype"] = "Notice";
                ss["url"] = "";
                ss["link_url"] = "";
                ss["UserId"] = admissionserialnumber;
                ss["type"] = ViewState["type"].ToString();
                ss["project_id"] = ViewState["project_id"].ToString();
                ss["private_key_id"] = ViewState["private_key_id"].ToString();
                ss["client_email"] = ViewState["client_email"].ToString();
                ss["client_id"] = ViewState["client_id"].ToString();
                ss["private_key"] = ViewState["private_key"].ToString();
                My.onlypush(gcmid, ss);
            }
        }
        private void bind_list_student_paymnet()
        {
            SqlCommand cmd = new SqlCommand("  select  Slip_no,(select top 1 parameter from Monthly_Fee_Collection_Slip where slipno=Student_Payment_History.Slip_no) as parameter_New from dbo.[Student_Payment_History] where parameter_New is null");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                paymnet_history.Clear();
                paymnet_history.Add(new school_web.Global.update_Student_Payment_History
                {
                    slip_id = "0",
                    parameter_New = "0",
                });
            }
            else
            {
                paymnet_history.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    paymnet_history.Add(new school_web.Global.update_Student_Payment_History
                    {
                        slip_id = dr["Slip_no"].ToString(),
                        parameter_New = dr["parameter_New"].ToString(),
                    });
                }
            }
        }


        private void bind_list_push()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Notice_Board_Details] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                push.Clear();
                push.Add(new school_web.Global.sendpush
                {
                    Class1 = "0",
                    Section = "0",
                    Notice = "0",
                    Id = "0",
                    Send_Type = "0",
                    Admission_no = "0",
                });
            }
            else
            {
                push.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    push.Add(new school_web.Global.sendpush
                    {
                        Class1 = dr["Class"].ToString(),
                        Section = dr["Section"].ToString(),
                        Notice = dr["Notice"].ToString(),
                        Id = dr["Id"].ToString(),
                        Send_Type = dr["Send_Type"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),
                    });
                }
            }
        }
        #endregion



        #region send push message to student
        private void send_pushMessage(school_web.Global.sendpushMessage item)
        {
            string query = "";
            //Send_Type = dr["Send_Type"].ToString(),
            //           Admission_no = dr["Admission_no"].ToString(),
            if (item.Send_Type == "Class Wise")
            {

                if (item.Class1 == "ALL")
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor]  where Session_id='" + My.get_session_id() + "'  ";
                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Section='" + item.Section + "' and Session_id='" + My.get_session_id() + "'";
                    }

                }
                else
                {
                    if (item.Section == "ALL")
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   Class_id='" + item.Class1 + "' and Session_id='" + My.get_session_id() + "'";

                    }
                    else
                    {
                        query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Class_id='" + item.Class1 + "' and Session_id='" + My.get_session_id() + "' and Section='" + item.Section + "' Session_id='" + My.get_session_id() + "'";
                    }

                }
            }
            else
            {
                query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where   admissionserialnumber='" + item.Admission_no + "' and Session_id='" + My.get_session_id() + "' ";
            }

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                code.executequery("update Private_Messages set Send_Status='Send' where Id=" + item.Id + "");
            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string gcmid = dt.Rows[i]["gcm_id"].ToString();

                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();

                    final_send_pushmessage(gcmid, admissionserialnumber, item);

                }
                code.executequery("update Private_Messages set Send_Status='Send' where Id=" + item.Id + "");
            }

        }

        private void final_send_pushmessage(string gcmid, string admissionserialnumber, school_web.Global.sendpushMessage item)
        {
            if (gcmid == "")
            {
                gcmid = "0";
            }
            if (gcmid != "")
            {
                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = item.Details;
                ss["title"] = item.Subject;
                ss["messagetype"] = "Message";
                ss["url"] = "";
                ss["link_url"] = "";
                ss["UserId"] = admissionserialnumber;
                ss["type"] = ViewState["type"].ToString();
                ss["project_id"] = ViewState["project_id"].ToString();
                ss["private_key_id"] = ViewState["private_key_id"].ToString();
                ss["client_email"] = ViewState["client_email"].ToString();
                ss["client_id"] = ViewState["client_id"].ToString();
                ss["private_key"] = ViewState["private_key"].ToString();
                My.onlypush(gcmid, ss);
            }

        }

        private void bind_list_pushmessage()
        {
            SqlCommand cmd = new SqlCommand(" select * from dbo.[Private_Messages] where Send_Status='Notsend' ");
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                pushMessage.Clear();
                pushMessage.Add(new school_web.Global.sendpushMessage
                {
                    Class1 = "0",
                    Section = "0",
                    Subject = "0",
                    Details = "0",
                    Id = "0",
                    Send_Type = "0",
                    Admission_no = "0",


                });
            }
            else
            {
                pushMessage.Clear();
                foreach (DataRow dr in dt.Rows)
                {

                    pushMessage.Add(new school_web.Global.sendpushMessage
                    {
                        Class1 = dr["Class_Id"].ToString(),
                        Section = dr["Section_Id"].ToString(),
                        Subject = dr["Subject"].ToString(),
                        Details = dr["Details"].ToString(),
                        Id = dr["Id"].ToString(),
                        Send_Type = dr["Send_Type"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),

                    });
                }
            }
        }

        #endregion



        protected void ddl_months_SelectedIndexChanged(object sender, EventArgs e)
        {
            hd_months.Value = ddl_months.SelectedValue;
        }

        protected void ddl_class_collection_month_overall_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class_collection_month_overall.SelectedItem.Text == "ALL")
            {
                hd_overall_collection_mnth_class.Value = "0";
            }
            else
            {
                hd_overall_collection_mnth_class.Value = ddl_class_collection_month_overall.SelectedValue;
            }
        }
    }
}