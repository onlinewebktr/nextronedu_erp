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
    public partial class create_payment_voucher : System.Web.UI.Page
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

                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        ddlsession.SelectedValue = My.get_session_id();

                        bind_month();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void bind_month()
        {
            DataTable dt = mycode.FillData("select Month,'false' as Value,Month_Id from Month_Index order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rp_month.DataSource = null;
                rp_month.DataBind();
            }
            else
            {
                rp_month.DataSource = dt;
                rp_month.DataBind();
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


        protected void chk_all_month_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rp_month.Items.Count; j++)
            {
                CheckBox chk_month_name = rp_month.Items[j].FindControl("chk_month_name") as CheckBox;
                if (chk_all_month.Checked)
                {
                    chk_month_name.Checked = true;
                }
                else
                {
                    chk_month_name.Checked = false;
                }
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
                else
                {
                    int mgrowcount1 = rp_month.Items.Count;
                    int kls = 0;
                    for (int ixi = 0; ixi < mgrowcount1; ixi++)
                    {
                        CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                        if (chkM.Checked == true)
                        {
                            Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                            Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                            //DataTable dt = mycode.FillData("select * from Payment_voucher_slip_history where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Month='" + lbl_month_name.Text + "'");
                            //if (dt.Rows.Count != 0)
                            //{
                            //    Alertme("Payment voucher already created for this month, class & section.", "warning");
                            //    return;
                            //}
                        }
                        else
                        {
                            kls++;
                        }
                    }
                    if (kls == mgrowcount1)
                    {
                        Alertme("Please check minimum one month.", "warning");
                        return;
                    }



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


            // DataTable dt = mycode.FillData(ViewState["query"].ToString());

            if (dt.Rows.Count == 0)
            {
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ViewState["PrevDues_checked"] = "0";


                    int mgrowcount1S = rp_month.Items.Count;
                    int klsS = 0;
                    //
                    double prev_dues_amt = 0;
                    //====================================
                    for (int ixi = 0; ixi < mgrowcount1S; ixi++)
                    {
                        if (ViewState["PrevDues_checked"].ToString() == "0")
                        {
                            CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                            if (chkM.Checked == false)
                            {
                                Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                                Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");

                                string duess = find_dues(lbl_month_name.Text, lbl_month_id.Text, dr);
                                prev_dues_amt = prev_dues_amt + My.toDouble(duess);

                            }
                            else
                            {
                                ViewState["PrevDues_checked"] = "1";
                                klsS++;
                            }
                        }
                        else
                        {
                            klsS++;
                        }
                    }



                    //===========================

                    //======================
                    int mgrowcount1 = rp_month.Items.Count;
                    int kls = 0;
                    //
                    double dues_amt = 0; string months = "";
                    //====================================
                    for (int ixi = 0; ixi < mgrowcount1; ixi++)
                    {
                        CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                        if (chkM.Checked == true)
                        {
                            Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                            Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");

                            months = months + lbl_month_name.Text + ",";
                            string dues = find_dues(lbl_month_name.Text, lbl_month_id.Text, dr);
                            dues_amt = dues_amt + My.toDouble(dues);

                            SqlDataAdapter ad = new SqlDataAdapter("select * from Payment_voucher_slip_history where Session_id=" + ddlsession.SelectedValue + "  and Admission_no='" + dr["Admission_no"].ToString() + "' and Month='" + lbl_month_name.Text + "'", My.conn);
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
                                drs["Month"] = lbl_month_name.Text;
                                drs["Month_id"] = lbl_month_id.Text;
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
                                mycode.executequery("update Payment_voucher_slip_history set Amount='" + dues + "',User_id='" + Session["Admin"].ToString() + "',Created_date='" + mycode.date() + "',Created_idate='" + mycode.idate() + "',Created_time='" + mycode.time() + "' where  Id=" + id + "");
                            }
                        }
                        else
                        {
                            kls++;
                        }
                    }
                    //

                    if (dues_amt > 0)
                    {
                        create_sl_no();
                        months = months.Remove(months.Length - 1);
                        SqlCommand cmd;
                        string query = "INSERT INTO Payment_voucher_slip (Session_id,Class_id,Section,Admission_no,Months,Amount,Slip_id,Created_date,Created_idate,Created_time,User_id,Firm_id,Last_date_of_payment,Previous_dues,Total_amount) values (@Session_id,@Class_id,@Section,@Admission_no,@Months,@Amount,@Slip_id,@Created_date,@Created_idate,@Created_time,@User_id,@Firm_id,@Last_date_of_payment,@Previous_dues,@Total_amount)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                        cmd.Parameters.AddWithValue("@Section", dr["Section"].ToString());
                        cmd.Parameters.AddWithValue("@Admission_no", dr["Admission_no"].ToString());
                        cmd.Parameters.AddWithValue("@Months", months);
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
            string session = dr["session"].ToString();
            string type = "";
            if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + session + "' and month='" + month + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
            {
                feedt = My.dataTable("select (sum(convert(float, amount))-sum(convert(float, isnull((disc_amount),'0')))-sum(convert(float, isnull((previously_paid),'0')))) as Total_Dues from (select payable amount,(isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + dr["Admission_no"].ToString() + "' and month='" + month + "'   and parameter_id='" + ViewState["parameteridS"].ToString() + "' and session='" + session + "' and fee_head_id=Typewise_fee_collection.content_id and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='6' and admission_no='All' and month='March'  and parameter_id='" + ViewState["parameteridS"].ToString() + "' and session='" + session + "' and fee_head_id=Typewise_fee_collection.content_id and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))) disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + session + "' and month='" + month + "' and parameter='MonthlyFee' and content_id!='6121') t");
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
                dc["session_id"] = dr["Session_id"].ToString();
                dc["class"] = dr["class"].ToString();
                dc["session"] = session;
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
            }
            return dues;
        }

        protected void btn_find_by_admission_no_Click(object sender, EventArgs e)
        {
            if(ddlsession.SelectedItem.Text=="Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (txt_admission_no.Text == "")
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
