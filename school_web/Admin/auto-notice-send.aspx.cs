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
    public partial class auto_notice_send : System.Web.UI.Page
    {
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["Session_name"] = My.get_session();
                        ViewState["Session_id"] = My.get_session_id();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
            }
        }


        private string get_c_month_id()
        {
            string month_id = "0";
            DataTable dt = mycode.FillData("select Position from Month_Index where Month_Id='" + mycode.cmonth() + "'");
            if (dt.Rows.Count > 0)
            {
                month_id = dt.Rows[0]["Position"].ToString();
            }
            return month_id;
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


        protected void btn_find_Click(object sender, EventArgs e)
        {
            ViewState["SendStatuS"] = "0";
            find_student_dues_by_month();
            if (ViewState["SendStatuS"].ToString() == "1")
            {
                My.exeSql("insert into Auto_notice_send_status(Notice_type,Send_date,Send_idate,Status,Time) values ('1','" + mycode.date() + "','" + mycode.idate() + "','1','" + mycode.time() + "');");
            }
        }

        private void find_student_dues_by_month()
        { 
            if (mycode.IsUserExist("select Id from Auto_notice_send_status where Notice_type='1' and Send_idate=" + mycode.idate() + " and Status=1"))
            {
                string tamplateS = "";
                DataTable dtTemplate = My.dataTable("select *,(select top 1 Notice_type from Auto_notice_type where Notice_type_id=Auto_notice_send_setting.Notice_type) as Notice_type_name,isnull((select top 1 Notice_message from Auto_notice_template where Notice_type=Auto_notice_send_setting.Notice_type and Status=1),'NA') as Message_template from Auto_notice_send_setting where Notice_type='1' and Status=1");
                if (dtTemplate.Rows.Count > 0)
                {
                    foreach (DataRow drmsgS in dtTemplate.Rows)
                    {
                        string Setday = drmsgS["Send_date_day"].ToString() + drmsgS["Time_hr"].ToString() + drmsgS["Time_mn"].ToString() + drmsgS["Time_ap_pm"].ToString();
                        string cdatetimE = mycode.daysingle() + hours() + "00" + ampm();
                        if (cdatetimE == Setday)
                        {
                            tamplateS = drmsgS["Message_template"].ToString();
                            DataTable fdt = new DataTable();
                            fdt.Columns.Add("Admission_no");
                            fdt.Columns.Add("Session");
                            fdt.Columns.Add("Class");
                            string qry = "select top 5 ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ViewState["Session_id"].ToString() + "' and Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";

                            SqlDataAdapter ad_contactus = new SqlDataAdapter(qry, My.conn);
                            DataSet ds = new DataSet();
                            ad_contactus.Fill(ds);
                            DataTable dt = ds.Tables[0];
                            int srowcount = dt.Rows.Count;

                            if (srowcount > 0)
                            {
                                dt.Columns.Add("Ad/An", Type.GetType("System.Double"));
                                fdt.Columns.Add("Ad/An", Type.GetType("System.Double"));


                                string monthId = get_c_month_id();
                                DataTable dtmnth = mycode.FillData("select * from Month_Index where Position<=" + monthId + "  order by Position asc");
                                foreach (DataRow drmnth in dtmnth.Rows)
                                {
                                    string lbl_month_id = drmnth["Month_Id"].ToString();
                                    string lbl_month_name = drmnth["Month"].ToString();
                                    dt.Columns.Add(lbl_month_name, Type.GetType("System.Double"));
                                    fdt.Columns.Add(lbl_month_name, Type.GetType("System.Double"));
                                }

                                dt.Columns.Add("Total", Type.GetType("System.Double"));
                                fdt.Columns.Add("Total", Type.GetType("System.Double"));



                                foreach (DataRow dr in dt.Rows)
                                {
                                    string adm_annual_dues = get_adm_annual_dues(dr);
                                    dr["Ad/An"] = My.toDouble(adm_annual_dues);
                                    double total_amt = 0;
                                    foreach (DataRow drmnthS in dtmnth.Rows)
                                    {
                                        string lbl_month_id = drmnthS["Month_Id"].ToString();
                                        string lbl_month_name = drmnthS["Month"].ToString();

                                        dr[lbl_month_name] = find_dues(lbl_month_name, lbl_month_id, dr);
                                        total_amt += My.toDouble(dr[lbl_month_name].ToString());
                                    }

                                    total_amt = total_amt + My.toDouble(dr["Ad/An"].ToString());
                                    dr["Total"] = total_amt;
                                    if (total_amt == 0)
                                    {
                                        dr.Delete();
                                    }

                                }
                                //================
                                dt.AcceptChanges();
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow drstd in dt.Rows)
                                    {
                                        string Student_name = drstd["Student_Name"].ToString();
                                        string class_name = drstd["class"].ToString();
                                        string section = drstd["Section"].ToString();
                                        string roll_no = drstd["rollnumber"].ToString();
                                        string admission_no = drstd["Admission_no"].ToString();
                                        string dues_amount = drstd["Total"].ToString();
                                        string cdate = mycode.date();
                                        string msgs = tamplateS.Replace("#student_name#", Student_name).Replace("#class_name#", class_name).Replace("#section#", section).Replace("#roll_no#", roll_no).Replace("#dues_amount#", dues_amount).Replace("#admission_no#", admission_no).Replace("#date#", cdate);

                                        send_message(admission_no, ViewState["Session_id"].ToString(), msgs);
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        private void send_message(string admission_no, string session_id, string msgs)
        {
            SqlCommand cmd;
            string query = "INSERT INTO Auto_notice_template_send_details (Notice_type,Notice_type_id,Admission_no,Session_id,Message,Date,idate,Time) values (@Notice_type,@Notice_type_id,@Admission_no,@Session_id,@Message,@Date,@idate,@Time)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Notice_type", "Dues Fee");
            cmd.Parameters.AddWithValue("@Notice_type_id", "1");
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

        private string hours()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh");
        }
        private string ampm()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("tt");
        }

        private string get_adm_annual_dues(DataRow drx)
        {
            string duesamt = "0";
            SqlDataAdapter ad_contactus = new SqlDataAdapter("select * from admission_registor where admissionserialnumber='" + drx["Admission_no"].ToString() + "' and Session_Id='" + drx["Session_Id"].ToString() + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' order by id asc", My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return duesamt;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
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

                    ViewState["class_id"] = dr["Class_id"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());

                    // confussion 
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);


                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();
                }


                if (ViewState["Transfer_Status"].ToString() == "New")
                {
                    string parameter = "HostelAdmissionFee";
                    if (ViewState["hostaltakenDues"].ToString().ToUpper() == "NO")
                    {
                        parameter = "AdmissionFee";
                    }
                    string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + drx["Admission_no"].ToString() + "'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + drx["Admission_no"].ToString() + "' and parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "')t";
                    DataTable fee_dt = My.dataTable(qry);
                    if (fee_dt.Rows.Count == 0)
                    {
                        qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + drx["Admission_no"].ToString() + "'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "')t";
                        fee_dt = My.dataTable(qry);
                    }


                    DataTable dtadmdues = mycode.FillData(qry);
                    if (dtadmdues.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0;
                        foreach (DataRow dr in dtadmdues.Rows)
                        {
                            dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                            payable += My.toDouble(dr["payable"]);
                            paid += My.toDouble(dr["paid"]);
                            dues += My.toDouble(dr["dues"]);
                            disc += My.toDouble(dr["disc_amount"]);
                            payble_after_disc += My.toDouble(dr["payable_after_disc"]);
                        }
                        duesamt = payble_after_disc.ToString("0.00");
                    }
                }
                else  ///ANNUAL FEES
                {
                    string parameter = "HostelAnnualFee";
                    if (ViewState["hostaltakenDues"].ToString().ToUpper() == "NO")
                    {
                        parameter = "AnnualFee";
                    }
                    string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + drx["Admission_no"].ToString() + "'  and parameter_id='2' and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='2' and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + drx["Admission_no"].ToString() + "' and parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "')t";
                    DataTable fee_dt = My.dataTable(qry);
                    if (fee_dt.Rows.Count == 0)
                    {
                        qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + drx["Admission_no"].ToString() + "'  and parameter_id='2' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='2' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "')t";
                        fee_dt = My.dataTable(qry);
                    }

                    DataTable dtanndues = mycode.FillData(qry);
                    if (dtanndues.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0;
                        foreach (DataRow drr in dtanndues.Rows)
                        {
                            drr["payable_after_disc"] = My.toDouble(drr["payable"]) - My.toDouble(drr["disc_amount"]) - My.toDouble(drr["paid"]);
                            payable += My.toDouble(drr["payable"]);
                            paid += My.toDouble(drr["paid"]);
                            dues += My.toDouble(drr["dues"]);
                            disc += My.toDouble(drr["disc_amount"]);
                            payble_after_disc += My.toDouble(drr["payable_after_disc"]);
                        }
                        string prev_dues = gt_previous_amount(drx);
                        double totalpay = payble_after_disc + My.toDouble(prev_dues);
                        duesamt = totalpay.ToString("0.00");
                    }
                }
                return duesamt;
            }
        }
        private string gt_previous_amount(DataRow dr)
        {
            string ReturN = "0";
            DataTable dt = mycode.FillData("select Dues_Amount from Previous_Year_Dues  where Session_id='" + ViewState["sessionIDs"].ToString() + "' and AdmissionNumber='" + dr["Admission_no"].ToString() + "'  and Class_id='" + ViewState["classid"].ToString() + "' and Status='Unpaid'");
            if (dt.Rows.Count > 0)
            {
                ReturN = dt.Rows[0][0].ToString();
            }
            return ReturN;
        }
        private string find_dues(string month, string month_id, DataRow dr)
        {
            DataTable feedt = new DataTable();
            if (dr["Transportation_Id"].ToString() == "")
            {
                ViewState["transportID"] = "0";
            }
            else
            {
                ViewState["transportID"] = dr["Transportation_Id"].ToString();
            }
            ViewState["parameter"] = dr["hosteltaken"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
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


            string type = "";
            if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + ViewState["Session_name"].ToString() + "' and month='" + month + "' and (parameter='" + ViewState["parameter"].ToString() + "' or parameter='HostelMonthlyFee')").Rows.Count > 0)
            {
                feedt = My.dataTable("select (sum(convert(float, amount))-sum(convert(float, isnull((disc_amount),'0')))-sum(convert(float, isnull((previously_paid),'0')))) as Total_Dues from (select payable amount,isnull(paid,'0') previously_paid,(select  sum(convert(float, disc_amt)) from Monthly_Fee_Collection_Slip where slipno=Typewise_fee_collection.transection and content_id=Typewise_fee_collection.content_id and Month=Typewise_fee_collection.month) as disc_amount from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + ViewState["Session_name"].ToString() + "' and month='" + month + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and content_id!='6121') t");
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
                dc["session_id"] = ViewState["Session_id"].ToString();
                dc["class"] = dr["class"].ToString();
                dc["session"] = ViewState["Session_name"].ToString();
                dc["class_id"] = dr["Class_id"].ToString();
                dc["hosteltaken"] = dr["hosteltaken"].ToString();
                dc["months"] = month;
                dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                dc["hostel_id"] = dr["hosteltaken"].ToString() == "yes" ? My.toint(ViewState["hostel_id"].ToString()) : 0;
                dc["day_boarding"] = My.toBool(dr["is_applied_dayboarding"]);
                dc["day_boarding_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                dc["category_id"] = dr["category_id"].ToString();
                dc["sub_category_id"] = dr["SubCategory_id"].ToString();
                dc["transportportation_id"] = ViewState["transportID"].ToString();
                dc["parameter_id"] = ViewState["parameteridS"].ToString();
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
                if (My.toDouble(dues) == 0)
                {
                    dues = "0";
                }


                //======================
                DataTable dtf = mycode.FillData("select Month_selection,Is_fine_repeat from Firm_Details");
                if (My.toIntS(dtf.Rows[0]["Month_selection"].ToString()) > 1)
                {
                    ViewState["more_months_check_status"] = "Yes";
                    ViewState["no_of_months"] = My.toIntS(dtf.Rows[0]["Month_selection"].ToString());
                }
                if (dtf.Rows[0]["Is_fine_repeat"].ToString() == "True")
                {
                    ViewState["RepeatFine"] = "Yes";
                }
                else
                {
                    ViewState["RepeatFine"] = "No";
                }
                fine_calculation(month, "1", mycode.date(), dr["Admission_no"].ToString());
                dues = (My.toDouble(dues) + My.toDouble(ViewState["fineAmt"].ToString())).ToString();

            }
            return dues;
        }

        #region FineCalculatioN
        double ttlFine = 0;
        private void fine_calculation(string monthName, string from, string date, string admission_no)
        {
            ViewState["fineAmt"] = "0";
            int mnth_idss = My.tomonth_number(monthName);
            string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());
            if (from == "1")
            {
                int mnth_ids = My.tomonth_number(monthName);
                string month_id_in_two_dgt = My.getMonthS_twoDigit(mnth_ids.ToString());
                ViewState["checked_after_frst_mnth"] = month_id_in_two_dgt;
            }

            string Session = ViewState["Session_name"].ToString();
            DataTable dty = mycode.FillData("select Fine_mode from globle_data where Fine_mode='2'");
            if (dty.Rows.Count != 0)
            {
                #region DayRanGEWise
                string pay_date = date;
                int payidate = My.DateConvertToIdate(pay_date);

                //Advance Payment Check
                string crnt_year = mycode.year();
                string cunrt_session = Session;
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
                        //string last_day_of_payment = dtz.Rows[0]["No_of_day"].ToString() + "/" + applicable_month + "/" + session_s_year; 

                        string last_day_of_payments = "01" + "/" + month_id_in_two_dgts + "/" + s_year;
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
                            ViewState["fineAmt"] = dt_fine.Rows[0]["Fine_amount"].ToString();
                        }
                        else
                        {
                            DataTable dt_fines = mycode.FillData("select top 1 * from Fine_master_day_range order by No_of_day desc");
                            if (dt_fines.Rows.Count != 0)
                            {
                                ViewState["fineAmt"] = dt_fines.Rows[0]["Fine_amount"].ToString();
                            }
                            else
                            {
                                ViewState["fineAmt"] = "0";
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                DataTable dt = mycode.FillData("select * from Fine_master where Status='1' and Session_id='" + ViewState["Session_id"].ToString() + "'");
                if (dt.Rows.Count != 0)
                {
                    string pay_date = date;
                    int payidate = My.DateConvertToIdate(pay_date);
                    string fineType = dt.Rows[0]["Fine_type"].ToString();

                    //Advance Payment Check
                    string crnt_year = mycode.year();
                    string cunrt_session = Session;
                    string session_frst_year = cunrt_session.Substring(0, 4);
                    int session_s_year = My.toint(session_frst_year);
                    int s_year = My.toint(session_frst_year);

                    int pay_month = My.toint(ViewState["checked_after_frst_mnth"].ToString());
                    s_year = My.check_start_months(pay_month, s_year);

                    if (fineType == "DayWise")//===== Days
                    {
                        #region DayWise

                        int pay_month_with_year = My.toint(s_year + ViewState["checked_after_frst_mnth"].ToString());
                        int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                        //Advance Payment Check

                        if (crnt_month_with_year >= pay_month_with_year)
                        {
                            ViewState["FineType"] = "DayWise";
                            string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                            string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;

                            string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + month_id_in_two_dgts + "/" + s_year;

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
                            }
                            else
                            {
                                ViewState["fineAmt"] = "0";
                            }

                        }
                        #endregion
                    }
                    else if (fineType == "MonthWise")//===== MonthWise
                    {
                        #region MonthWise
                        string pay_month_two_digit = My.getMonthS_twoDigit(pay_month.ToString());
                        ViewState["FineType"] = "MonthWise";
                        string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                        string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");

                        string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + pay_month_two_digit + "/" + s_year;
                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);
                        int totalmonths = 0;




                        if (ViewState["RepeatFine"].ToString() == "Yes")
                        {
                            if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + admission_no + "' and session='" + ViewState["Session_name"].ToString() + "' and month='" + monthName + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                            {
                            }
                            else
                            {
                                if (totaldays > 0)
                                {
                                    if (30 >= totaldays)
                                    {
                                        totalmonths = 1;
                                    }
                                    else
                                    {
                                        totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                                        double monthdays = 31;
                                        double reminder = My.toDouble(totaldays) % monthdays;
                                        if (29 > Math.Round(reminder))
                                        {
                                            totalmonths++;
                                        }
                                    }
                                }
                            }

                            string fineStartFromMonth = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                            string fineStartFrom = s_year + fineStartFromMonth + dt.Rows[0]["Last_day_to_deposit_fees"].ToString();
                            if (My.toIntS(fineStartFrom) > My.DateConvertToIdate(pay_date))
                            {
                                totalmonths = 0;
                            }

                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totalmonths;
                            ViewState["fine_date_From"] = last_day_of_payments;
                            ViewState["fine_date_To"] = pay_date;
                            if (My.toDouble(totalmonths) > 0)
                            {
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * totalmonths;
                                ViewState["fineAmt"] = ttl_fine_amt;
                            }
                            else
                            {
                                ViewState["fineAmt"] = "0";
                            }
                        }
                        else
                        {
                            if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + admission_no + "' and session='" + ViewState["Session_name"].ToString() + "' and month='" + monthName + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                            {
                            }
                            else
                            {
                                if (totaldays > 0)
                                {
                                    if (30 >= totaldays)
                                    {
                                        totalmonths = 1;
                                    }
                                    else
                                    {
                                        totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                                    }
                                }
                            }
                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totalmonths;
                            ViewState["fine_date_From"] = last_day_of_payment;
                            ViewState["fine_date_To"] = pay_date;
                            if (My.toDouble(totalmonths) > 0)
                            {
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * 1;
                                ViewState["fineAmt"] = ttl_fine_amt;
                            }
                            else
                            {
                                ViewState["fineAmt"] = "0";
                            }
                        }
                        #endregion
                    }
                    else
                    {

                        #region QuarterWise
                        ViewState["FineType"] = "QuarterWise";
                        double fnl_fine_amt = 0;
                        SqlDataAdapter ad = new SqlDataAdapter("select * from Fine_master where Status='1' and Session_id='" + ViewState["Session_id"].ToString() + "' and Q_start_month<='" + ViewState["checked_after_frst_mnth"].ToString() + "'  order by Q_start_month asc", My.conn);
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
                        }
                        #endregion
                    }
                }
            }
        }
        #endregion


    }
}