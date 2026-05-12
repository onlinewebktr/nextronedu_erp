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

namespace school_web.Payroll
{
    public partial class salary_calculate : System.Web.UI.Page
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


                        ddl_month.DataSource = My.bindMonthName();
                        ddl_month.Text = DateTime.Now.ToString("MMMM");
                        ddl_month.DataBind();
                        ddl_year.DataSource = My.bindYear();
                        ddl_year.Text = DateTime.Now.ToString("yyyy");
                        ddl_year.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Designation_Master");
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


        DataTable dt;
        int Days_in_month = 0;
        int year = 0;
        string month = "";
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                year = My.toIntS(ddl_year.Text);
                month = (ddl_month.SelectedIndex + 1).ToString("00");

                SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Salary_chart where month='" + month + "' and year='" + year + "'", My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Employee_Salary_chart");
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    lbl_msg_S.InnerText = "Salary Already Calculated for the month of " + month + "-" + year + ". Would you like to recalculate the salary?";
                    alrt_calculted_slry.Visible = true;

                    //My.exeSql("delete from PRL_Employee_Salary_chart where month='" + month + "' and year='" + year + "'");
                    //My.exeSql("delete from PRL_Emp_Salary_deduction_head_wise where month='" + month + "' and year='" + year + "'");
                    //My.exeSql("delete from PRL_Emp_Salary_Income_head_wise where month='" + month + "' and year='" + year + "'");
                    //adjust_advane();
                    calculate_salary();
                }
                else
                {
                    calculate_salary();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_slry_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                calculate_salary();
                alrt_calculted_slry.Visible = false;
            }
            catch (Exception ex)
            {
            }
        }

        private void calculate_salary()
        {
            year = My.toIntS(ddl_year.Text);
            month = (ddl_month.SelectedIndex + 1).ToString("00");

            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Salary_chart where month='" + month + "' and year='" + year + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Employee_Salary_chart");
            DataTable dt1 = ds.Tables[0];
            if (dt1.Rows.Count > 0)
            {
                My.exeSql("delete from PRL_Employee_Salary_chart where month='" + month + "' and year='" + year + "'");
                My.exeSql("delete from PRL_Emp_Salary_deduction_head_wise where month='" + month + "' and year='" + year + "'");
                My.exeSql("delete from PRL_Emp_Salary_Income_head_wise where month='" + month + "' and year='" + year + "'");
                adjust_advane();
            }
            var days = DateTime.DaysInMonth(year, My.toIntS(month));
            Days_in_month = days;
            Dictionary<string, object> dc1 = My.get_workinglimit();
            string limit_fullday = (String)dc1["limit_fullday"];
            string limit_halfday = (String)dc1["limit_halfday"];


            double full_day_hour_limit = Convert.ToDouble(limit_fullday);
            double half_data_hour_limit = Convert.ToDouble(limit_halfday);
          //  update_attendance_table();
            //   
          

           // String query = @" select * ,isnull((select Round(sum(working_hour)/8,0) over_time_days from dbo.[PRL_Over_Time_Working] ot where Idate like '%" + year + month + @"%' and ot.Employee_id=t.Employee_id),0) over_time_working_days,isnull((select Round(sum(working_hour)/8,0) over_time_days from dbo.[PRL_Over_Time_Working] ot where Idate like '%" + year + month + @"%' and ot.Employee_id=t.Employee_id),0)+present_days+leave+cast(half_days as float)/2.0 total ,salary_calculation_method  from(
// select  Employee_Name,
/// '" + days + @"' Days_in_month,
// 0 total_working_days,
 //( select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where Employee_id=em.Employee_id and (Shift_1_Working>=4) and idate like '" + year + month + @"%')  present_days,
// ( select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where Employee_id=em.Employee_id and ( Shift_1_Working>=3 and Shift_1_Working<4 ) and idate like '" + year + month + @"%')  half_days,
//(  select count(idate) from dbo.[PRL_Emp_Date_wise_leave_entry] where Employee_id=em.Employee_id and Leave_id!='LOP' and idate like '" + year + month + @"%')  leave,
// (select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where Employee_id=em.Employee_id and (  Shift_1_Working=-1)  and idate like '" + year + month + @"%') attendance_issue,
// Grade_id,Employee_id  ,Emp_Code from dbo.[PRL_Employee_Master] em) t join PRL_Grade_Master gm on t.Grade_id=gm.Grade_id";



            String query = @" select * ,isnull((select Round(sum(working_hour)/8,0) over_time_days from dbo.[PRL_Over_Time_Working] ot where Idate like '%" + year + month + @"%' and ot.Employee_id=t.Employee_id),0) over_time_working_days,isnull((select Round(sum(working_hour)/8,0) over_time_days from dbo.[PRL_Over_Time_Working] ot where Idate like '%" + year + month + @"%' and ot.Employee_id=t.Employee_id),0)+present_days+leave+cast(half_days as float)/2.0 total ,salary_calculation_method  from(
 select  Employee_Name,
 '" + days + @"' Days_in_month,
 0 total_working_days,
 ( select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where Employee_id=em.Employee_id and (cast(isnull(Shift_1_Working,0) as float)+cast(isnull(Shift_2_Working,0) as float)>=" + full_day_hour_limit + ") and idate like '" + year + month + @"%')  present_days,
 ( select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where Employee_id=em.Employee_id and ( cast(isnull(Shift_1_Working,0) as float)+cast(isnull(Shift_2_Working,0) as float)>=" + half_data_hour_limit + " and cast(isnull(Shift_1_Working,0) as float)+cast(isnull(Shift_2_Working,0) as float)<" + full_day_hour_limit + ") and idate like '" + year + month + @"%')  half_days,
(  select count(idate) from dbo.[PRL_Emp_Date_wise_leave_entry] where Employee_id=em.Employee_id and Leave_id!='LOP' and idate like '" + year + month + @"%')  leave,
 (select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where Employee_id=em.Employee_id and (  Shift_1_Working=-1  or Shift_2_Working=-1)  and idate like '" + year + month + @"%') attendance_issue,
 Grade_id,Employee_id  ,Emp_Code from dbo.[PRL_Employee_Master] em) t left join PRL_Grade_Master gm on t.Grade_id=gm.Grade_id";



            dt = My.dataTable(query);
            wd = new Dictionary<string, int>();
            foreach (DataRow dr in dt.Rows)
            {
                dr["total_working_days"] = find_working_days(dr["Grade_id"].ToString());
            }

            if (dt.Rows.Count == 0)
            {
                pnl_data_grid.Visible = false;
                grd_salary.DataSource = null;
                grd_salary.DataBind();
            }
            else
            {
                pnl_data_grid.Visible = true;
                grd_salary.DataSource = dt.DefaultView;
                grd_salary.DataBind();
            }
        }


        private void update_attendance_table()
        {
            My.exeSql(" update PRL_Employee_Daily_attendance_chart set Shift_1_Working=isnull(datediff(MINute,cast(Shift_1_in as time), cast(Shift_1_out as time))/60.0,0) , Shift_2_Working=isnull(datediff(MINute,cast(Shift_2_in as time)  , cast(Shift_2_out as time))/60.0,0) where Shift_1_Working is null or  Shift_1_Working = '' or Shift_1_Working>0  and  idate like '" + year + month + "%'");

        }

        private void adjust_advane()
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_advance_adjustment_details where month='" + month + "' and year='" + year + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "advance_adjustment_details");
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                My.exeSql("update PRL_Advance_Payment set Adjusted_Amount=Adjusted_Amount-" + dr["amount"] + " where id='" + dr["id"] + "'");
                dr.Delete();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }


        Dictionary<string, int> wd;
        private object find_working_days(string grade)
        {
            if (wd.Keys.Contains("grade_" + grade))
            {
                return wd["grade_" + grade];
            }
            else
            {
                DataTable ld = My.dataTable("select top 100 * from dbo.[PRL_Grade_wise_weekly_off] where Grade_id=" + grade);
                int working_days = 0;
                int week = 1;
                int _month = My.toIntS(month);
                for (int i = 1; i <= Days_in_month; i++)
                {
                    DateTime date = new DateTime(year, _month, i);
                    string day = date.ToString("dddd");
                    if (i % 7 == 0)
                    {
                        week++;
                    }
                    if (week == 1)
                    {
                        var drs = ld.Select("Day='" + day + "' and First_week=1");
                        if (drs.Length == 0)
                        {
                            working_days++;
                        }
                    }
                    else if (week == 2)
                    {
                        var drs = ld.Select("Day='" + day + "' and Second_week=1");
                        if (drs.Length == 0)
                        {
                            working_days++;
                        }
                    }
                    else if (week == 3)
                    {
                        var drs = ld.Select("Day='" + day + "' and Third_week=1");
                        if (drs.Length == 0)
                        {
                            working_days++;
                        }
                    }
                    else if (week == 4)
                    {
                        var drs = ld.Select("Day='" + day + "' and Fourth_week=1");
                        if (drs.Length == 0)
                        {
                            working_days++;
                        }
                    }
                    else if (week == 5)
                    {
                        var drs = ld.Select("Day='" + day + "' and Fifth_week=1");
                        if (drs.Length == 0)
                        {
                            working_days++;
                        }
                    }
                    else
                    {
                        if (!is_holiday(date.ToString("yyyyMMdd")))
                        {
                            working_days++;
                        }
                    }
                }
                wd["grade_" + grade] = working_days;

                return working_days;
            }
        }

        private bool is_holiday(string idate)
        {
            if (My.dataTable(" select description from dbo.[PRL_holiday_list] where idate='" + idate + "'").Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void link_view_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_employee_id = (Label)row.FindControl("lbl_employee_id");
                find_data(My.toIntS((ddl_month.SelectedIndex + 1).ToString("00")), My.toIntS(ddl_year.Text), lbl_employee_id.Text);
                myModal2.Visible = true;

            }
            catch (Exception ex)
            {
            }
        }


        private void find_data(int month, int year, string emp_code)
        {
            DataTable dtS;
            string employee_id = emp_code;

            dtS = new DataTable();
            dtS.Columns.Add("Employee_id");
            //  dt.Columns.Add("Grade_id");
            dtS.Columns.Add("Date");
            dtS.Columns.Add("idate");
            dtS.Columns.Add("Shift_1_in");
            dtS.Columns.Add("Shift_1_out");
            dtS.Columns.Add("Shift_2_in");
            dtS.Columns.Add("Shift_2_out");
            //  grade_id = My.data("select Grade_id from dbo.[Employee_Master] where Employee_id='"+employee_id+"'");

            string mname = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            int no_of_day = DateTime.DaysInMonth(year, month);
            DataTable at_dt = My.dataTable("select * from dbo.[PRL_Employee_Daily_attendance_chart] where idate like '%" + year.ToString() + month.ToString("00") + "%' and Employee_id='" + employee_id + "'");
            for (int d = 1; d <= no_of_day; d++)
            {
                string idate = year.ToString() + My.toDouble(month).ToString("00") + My.toDouble(d).ToString("00");
                string date = My.toDouble(d).ToString("00") + "/" + My.toDouble(month).ToString("00") + "/" + year.ToString();
                DataView dv = at_dt.DefaultView;
                dv.RowFilter = " idate='" + idate + "' ";
                DataTable adt = dv.ToTable();
                DataRow dr = dtS.NewRow();
                dr["Employee_id"] = employee_id;
                //  dr["Grade_id"] = grade_id;
                dr["Date"] = date;
                dr["idate"] = idate;
                if (adt.Rows.Count == 0)
                {
                    dr["Shift_1_in"] = "AB";
                    dr["Shift_1_out"] = "AB";
                    dr["Shift_2_in"] = "AB";
                    dr["Shift_2_out"] = "AB";
                }
                else
                {
                    foreach (DataRow adr in adt.Rows)
                    {
                        dr["Shift_1_in"] = adr["Shift_1_in"].ToString() != "" ? adr["Shift_1_in"].ToString() : "AB";
                        dr["Shift_1_out"] = adr["Shift_1_out"].ToString() != "" ? adr["Shift_1_out"].ToString() : "AB";
                        dr["Shift_2_in"] = adr["Shift_2_in"].ToString() != "" ? adr["Shift_2_in"].ToString() : "AB";
                        dr["Shift_2_out"] = adr["Shift_2_out"].ToString() != "" ? adr["Shift_2_out"].ToString() : "AB";
                    }
                }
                dtS.Rows.Add(dr);
            }
            grd_dtl.DataSource = dtS.DefaultView;
            grd_dtl.DataBind();
        }

        protected void lnk_close_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }


        bool status = false;
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                status = false;
                try
                {
                    calclualate_salary();
                    status = true;
                }
                catch (Exception ex)
                {
                    My.Save_Exception(ex.ToString());
                }



                if (status)
                {
                    alert_msg.InnerText = "Salary Calculated for the month of " + month + "-" + year + "\n Would you like to view Salary Chart?";
                    conf_alrt.Visible = true;
                }
                else
                {
                    Alertme("Unable to Create Salary", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_yes_Click(object sender, EventArgs e)
        {
            Session["sl_year"] = ddl_year.SelectedIndex;
            Session["sl_month"] = ddl_month.SelectedIndex;
            Response.Redirect("salary-chart.aspx", false);
        }

        private void calclualate_salary()
        {
            year = My.toIntS(ddl_year.Text);
            month = (ddl_month.SelectedIndex + 1).ToString("00");

            SqlDataAdapter adS = new SqlDataAdapter("select * from PRL_Employee_Salary_chart where month='" + month + "' and year='" + year + "'", My.conn);
            DataSet dsS = new DataSet();
            adS.Fill(dsS, "Employee_Salary_chart");
            DataTable dt1S = dsS.Tables[0];
            if (dt1S.Rows.Count > 0)
            {
                My.exeSql("delete from PRL_Employee_Salary_chart where month='" + month + "' and year='" + year + "'");
                My.exeSql("delete from PRL_Emp_Salary_deduction_head_wise where month='" + month + "' and year='" + year + "'");
                My.exeSql("delete from PRL_Emp_Salary_Income_head_wise where month='" + month + "' and year='" + year + "'");
                adjust_advane();

            }
            var daysS = DateTime.DaysInMonth(year, My.toIntS(month));
            Days_in_month = daysS;
            //update_attendance_table();
            //             

            
            Dictionary<string, object> dc1 = My.get_workinglimit();
            string limit_fullday = (String)dc1["limit_fullday"];
            string limit_halfday = (String)dc1["limit_halfday"];


            double full_day_hour_limit = Convert.ToDouble(limit_fullday);
            double half_data_hour_limit = Convert.ToDouble(limit_halfday);



            String query = @" select * ,isnull((select Round(sum(working_hour)/8,0) over_time_days from dbo.[PRL_Over_Time_Working] ot where Idate like '%" + year + month + @"%' and ot.Employee_id=t.Employee_id),0) over_time_working_days,isnull((select Round(sum(working_hour)/8,0) over_time_days from dbo.[PRL_Over_Time_Working] ot where Idate like '%" + year + month + @"%' and ot.Employee_id=t.Employee_id),0)+present_days+leave+cast(half_days as float)/2.0 total ,salary_calculation_method  from(
 select  Employee_Name,
 '" + daysS + @"' Days_in_month,
 0 total_working_days,
 ( select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where Employee_id=em.Employee_id and (cast(isnull(Shift_1_Working,0) as float)+cast(isnull(Shift_2_Working,0) as float)>=" + full_day_hour_limit + ") and idate like '" + year + month + @"%')  present_days,
 ( select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where Employee_id=em.Employee_id and ( cast(isnull(Shift_1_Working,0) as float)+cast(isnull(Shift_2_Working,0) as float)>=" + half_data_hour_limit + " and cast(isnull(Shift_1_Working,0) as float)+cast(isnull(Shift_2_Working,0) as float)<" + full_day_hour_limit + ") and idate like '" + year + month + @"%')  half_days,
(  select count(idate) from dbo.[PRL_Emp_Date_wise_leave_entry] where Employee_id=em.Employee_id and Leave_id!='LOP' and idate like '" + year + month + @"%')  leave,
 (select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where Employee_id=em.Employee_id and (  Shift_1_Working=-1  or Shift_2_Working=-1)  and idate like '" + year + month + @"%') attendance_issue,
 Grade_id,Employee_id  ,Emp_Code from dbo.[PRL_Employee_Master] em) t left join PRL_Grade_Master gm on t.Grade_id=gm.Grade_id";
            dt = My.dataTable(query);
            wd = new Dictionary<string, int>();
            foreach (DataRow dr in dt.Rows)
            {
                dr["total_working_days"] = find_working_days(dr["Grade_id"].ToString());
            }




            //======================
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Salary_chart where month='" + month + "' and year='" + year + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Employee_Salary_chart");
            DataTable dt1 = ds.Tables[0];
            if (dt1.Rows.Count == 0)
            {
                foreach (DataRow dr1 in dt.Rows)
                {
                    string empid = dr1["Employee_id"].ToString();
                    double working_days = 0;
                    if (dr1["salary_calculation_method"].ToString() == "No of Days In Month")
                    {
                        working_days = My.toDouble(dr1["Days_in_month"]);
                    }
                    else if (dr1["salary_calculation_method"].ToString() == "No of Working Days In Month")
                    {
                        working_days = My.toDouble(dr1["total_working_days"]);
                    }
                    else
                    {
                        working_days = My.toDouble(dr1["salary_calculation_method"]);
                        if (working_days == 0)
                        {
                            working_days = My.toDouble(dr1["total_working_days"]);
                        }
                    }
                    DataRow dr = dt1.NewRow();
                    dr["Employee_id"] = dr1["Employee_id"];
                    dr["Days_in_month"] = dr1["Days_in_month"];
                    dr["total_working_days"] = dr1["total_working_days"];
                    dr["over_time_working_days"] = dr1["over_time_working_days"];
                    dr["present_days"] = dr1["present_days"];
                    dr["half_days"] = dr1["half_days"];
                    dr["leave"] = dr1["leave"];
                    dr["total"] = dr1["total"];
                    dr["attendance_issue"] = dr1["attendance_issue"];
                    dr["calculation_date"] = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    dr["month"] = month;
                    dr["year"] = year;
                    dr["user_id"] = ViewState["Userid"].ToString();
                    DataTable st = My.dataTable(" select  * from dbo.[PRL_Employee_wise_salary_structure] where Employee_id=" + dr1["Employee_id"]);
                    string mode_of_payemnt = "Cash";
                    string pf_type = "Both";
                    if (st.Rows.Count > 0)
                    {
                        mode_of_payemnt = st.Rows[0]["Payment_mode"].ToString();
                        pf_type = st.Rows[0]["PF_type"].ToString() == "" ? "Both" : st.Rows[0]["PF_type"].ToString();
                    }
                    double days = My.toDouble(dr1["total"]);
                    double salary = 0, basic_fix = 0, basic = 0, gross = 0, e_pf = 0, e_esi = 0, c_pf = 0, c_esi = 0, pf_on, esi_on, p_tax_on, p_tax = 0, tds = 0;
                    DataTable it = My.dataTable(" select  * from dbo.[PRL_Allowance_Master] am join dbo.[PRL_Employee_Income_head_wise_salary_Structure] ad on am.Income_Type=ad.Income_head_id and am.Grade_id=ad.Grade_id where Employee_id=" + dr1["Employee_id"]);

                    //calculate amount which is not fixed on income head
                    foreach (DataRow r in it.Select("Variable_Head=False"))
                    {
                        double amt = My.toDouble(it.Compute("sum(Paid_Amount)", "Income_head='" + r["Formula_Type"] + "'"));
                        r["Paid_Amount"] = Math.Round(amt * My.toDouble(r["Formula"]) * 0.01, 0);
                    }
                    foreach (DataRow r in it.Rows)
                    {
                        var amt = Math.Round(My.toDouble(r["Paid_Amount"]) * days / working_days, 0);
                        send_to_income_head(r, amt);
                    }
                    basic_fix = Math.Round(My.toDouble(it.Compute("sum(Paid_Amount)", "Income_Type='1'")), 0);
                    DataTable dd = My.dataTable(" select Row_Number() over(order by t.id) sl,t.*,cast(dss.Deducted_Amount as float) Deducted_Amount,dss.Employer_Contribution,dss.Mode_of_deduction     from(select em.Employee_id,dm.* from PRL_Deduction_Master dm join PRL_Employee_Master em on dm.Grade_id=em.Grade_id where Employee_id=" + dr1["Employee_id"] + ")t  left join PRL_Employee_deduction_head_wise_salary_Structure dss on t.Deduction_Type=dss.Deduction_head_id  and t.Employee_id=dss.Employee_id ");
                    pf_on = Math.Round(My.toDouble(it.Compute("sum(Paid_Amount)", "PF=1")), 0);
                    esi_on = Math.Round(My.toDouble(it.Compute("sum(Paid_Amount)", "ESI=1")), 0);
                    p_tax_on = Math.Round(My.toDouble(it.Compute("sum(Paid_Amount)", "P_Tax=1")), 0);
                    double other_deduct = 0;
                    salary = Math.Round(My.toDouble(it.Compute("sum(Paid_Amount)", "")), 0);
                    //calculate amount which is not fixed on deduction head
                    foreach (DataRow r in dd.Rows)
                    {
                        if (r["Deduction_Type"].ToString() == "3")
                        {
                            if (pf_type == "Both")
                            {
                                e_esi = Math.Round(esi_on * My.toDouble(r["Formula"]) * 0.01 * days / working_days, 0);
                                send_to_deduction_head(r, e_esi);
                            }
                        }
                        else if (r["Deduction_Type"].ToString() == "4")
                        {
                            if (pf_type == "Both")
                            {
                                e_pf = Math.Round(pf_on * My.toDouble(r["Formula"]) * 0.01 * days / working_days, 0);
                                send_to_deduction_head(r, e_pf);
                            }
                        }
                        else if (r["Deduction_Type"].ToString() == "2")
                        {
                            if (r["Variable_head"].ToString() == "False")
                            {
                                p_tax = Math.Round(p_tax_on * My.toDouble(r["Formula"]) * 0.01 * days / working_days, 0);
                            }
                            else
                            {
                                p_tax = Math.Round(My.toDouble(r["Deducted_Amount"]) * days / working_days, 0);
                            }
                            send_to_deduction_head(r, p_tax);
                        }
                        else if (r["Deduction_Type"].ToString() == "1")
                        {

                            if (r["Variable_head"].ToString() == "False")
                            {
                                tds = Math.Round(basic_fix * My.toDouble(r["Formula"]) * 0.01 * days / working_days, 0);
                            }
                            else
                            {
                                tds = Math.Round(My.toDouble(r["Deducted_Amount"]) * days / working_days, 0);
                            }
                            send_to_deduction_head(r, tds);
                        }
                        else if (r["Variable_head"].ToString() == "False")
                        {
                            var amt = Math.Round(basic_fix * My.toDouble(r["Formula"]) * 0.01 * days / working_days, 0);
                            other_deduct += amt;
                            send_to_deduction_head(r, amt);
                        }
                        else
                        {
                            var amt = Math.Round(My.toDouble(r["Deducted_Amount"]) * days / working_days, 0);
                            other_deduct += amt;
                            send_to_deduction_head(r, amt);
                        }
                    }
                    dr["Salary"] = salary;
                    gross = Math.Round(My.toDouble(it.Compute("sum(Paid_Amount)", "Income_Type<>'1'")), 0);
                    dr["Salary_Basic_fix"] = basic_fix;
                    basic = Math.Round(basic_fix * days / working_days, 0);
                    gross = Math.Round(gross * days / working_days, 0);
                    dr["Salary_Basic"] = basic;
                    dr["Gross_Salary"] = gross;
                    dr["Total_Salary"] = basic + gross;
                    c_pf = Math.Round(pf_on * My.employer_pf * 0.01 * days / working_days, 0);
                    c_esi = Math.Round(esi_on * My.employer_esi * 0.01 * days / working_days, 0);
                    dr["emp_PF"] = e_pf;
                    dr["emp_esi"] = e_esi;
                    dr["p_tax"] = p_tax;
                    dr["tds"] = tds;
                    dr["emp_contribution"] = e_pf + e_esi;
                    dr["com_PF"] = c_pf;
                    dr["com_ESI"] = c_esi;
                    dr["com_contribution"] = c_pf + c_esi;
                    double total_deduction = (e_pf + e_esi + other_deduct + p_tax + tds);
                    double salary_after_deduction = basic + gross - total_deduction;
                    dr["Salary_After_PF_ESI"] = salary_after_deduction;
                    double advance = advance_ajust(salary_after_deduction, dr1["Employee_id"].ToString());
                    dr["Net_Salary"] = salary_after_deduction - advance;
                    dr["other_deduct"] = other_deduct;
                    dr["Through_Bank"] = 0;
                    dr["Through_Cheque"] = 0;
                    dr["Through_Cash"] = 0;
                    dr["salary_calculate_on"] = working_days;
                    dr["advance"] = advance;
                    if (mode_of_payemnt == "Bank")
                    {
                        dr["Through_Bank"] = dr["Net_Salary"];
                    }
                    else if (mode_of_payemnt == "Cheque")
                    {
                        dr["Through_Cheque"] = dr["Net_Salary"];
                    }
                    else
                    {
                        dr["Through_Cash"] = dr["Net_Salary"];
                    }
                    dr["status"] = "Pending";
                    dr["total_deduction"] = total_deduction;
                    dt1.Rows.Add(dr);
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt1);
            }
        }
        private double advance_ajust(double net_salary, string employee_id)
        {
            year = My.toIntS(ddl_year.Text);
            month = (ddl_month.SelectedIndex + 1).ToString("00");
            double total_advance_adjust = 0;
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Advance_Payment where Employee_id='" + employee_id + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Employee_Salary_chart");
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                double advance = My.toDouble(dr["Advance"]) - My.toDouble(dr["Adjusted_Amount"]);
                if (advance > My.toDouble(dr["Advance_Adjust"]))
                {
                    advance = My.toDouble(dr["Advance_Adjust"]);
                }

                if (net_salary < advance)
                {
                    advance = net_salary;
                }

                //dr["Adjusted_Amount"] = My.toDouble(dr["Adjusted_Amount"]) + net_salary;
                dr["Adjusted_Amount"] = My.toDouble(dr["Adjusted_Amount"]) + advance;

                net_salary = net_salary - advance;
                total_advance_adjust += advance;
                My.exeSql("insert into PRL_advance_adjustment_details(emp_id,month,year,advance_id,amount) values ('" + employee_id + "','" + month + "','" + year + "','" + dr["id"] + "','" + advance + "');");

            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            return total_advance_adjust;
        }

        private void send_to_income_head(DataRow r, double amt)
        {
            year = My.toIntS(ddl_year.Text);
            month = (ddl_month.SelectedIndex + 1).ToString("00");
            My.exeSql("insert into PRL_Emp_Salary_Income_head_wise(Income_head,Grade_id,PF,ESI,P_Tax,Employee_Code,Income_Value,month,year,income_type) values ('" + r["Income_head"] + "','" + r["Grade_id"] + "','" + r["PF"] + "','" + r["ESI"] + "','" + r["P_Tax"] + "','" + r["Employee_id"] + "','" + amt + "','" + month + "','" + year + "','" + r["Income_Type"] + "');");

        }

        private void send_to_deduction_head(DataRow r, double amt)
        {
            year = My.toIntS(ddl_year.Text);
            month = (ddl_month.SelectedIndex + 1).ToString("00");
            My.exeSql("insert into PRL_Emp_Salary_deduction_head_wise(Deduction_head,Grade_id,Deduction_Type,Employee_Code,Deduction_value,month,year) values ('" + r["Deduction_head"] + "','" + r["Grade_id"] + "','" + r["Deduction_Type"] + "','" + r["Employee_id"] + "','" + amt + "','" + month + "','" + year + "');");
        }

        protected void btn_no_Click(object sender, EventArgs e)
        {
            conf_alrt.Visible = false;
            alrt_calculted_slry.Visible = false;
        }






    }
}