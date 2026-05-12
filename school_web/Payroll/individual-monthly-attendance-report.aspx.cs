using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cms_web.Payroll
{
    public partial class individual_monthly_attendance_report : System.Web.UI.Page
    {
        bool is_double_shift = false;
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

                        if (My.Working_shift == "Single")
                        {
                            is_double_shift = false;
                            grd_attendance.Columns[5].Visible = false;
                            grd_attendance.Columns[6].Visible = false;
                            grd_attendance.Columns[3].HeaderText = "Shift in";
                            grd_attendance.Columns[4].HeaderText = "Shift out";
                        }
                        else
                        {
                            is_double_shift = true;
                            grd_attendance.Columns[5].Visible = true;
                            grd_attendance.Columns[6].Visible = true;
                        }



                        ddl_month.DataSource = My.bindMonthName();
                        string mm = String.Format("{0:MMMM}", DateTime.Now);
                        ddl_month.Text = mm;
                        ddl_month.DataBind();

                        ddl_year.DataSource = My.bindYear();
                        ddl_year.Text = DateTime.Now.ToString("yyyy");
                        ddl_year.DataBind();
                        mycode.bind_all_ddl_with_id(ddl_employee_name, "select (Employee_Name+', '+Emp_Code) as Employee_Name,Employee_id from PRL_Employee_Master order by Employee_Name asc");

                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Individual_Monthly_Attendance_Report");
            }
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {

                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();

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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_year.Text == "")
                {
                    Alertme("Please Select Year", "warning");
                    ddl_year.Focus();
                    return;
                }
                if (ddl_month.Text == "")
                {
                    Alertme("Please Select Month", "warning");
                    ddl_month.Focus();
                    return;
                }
                if (ddl_employee_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please Select Employee", "warning");
                    ddl_employee_name.Focus();
                    return;
                }

                find_data();
            }
            catch (Exception ex)
            {
            }
        }

        DataTable new_dt;
        string grade_id = "";
        int month;
        int year;
        int on_leave = 0, absent = 0, present = 0, half_day = 0;
        private void find_data()
        {
            new_dt = new DataTable();
            new_dt.Columns.Add("Employee_id");
            new_dt.Columns.Add("Grade_id");
            new_dt.Columns.Add("Date");
            new_dt.Columns.Add("Day");
            new_dt.Columns.Add("Working");
            new_dt.Columns.Add("idate");
            new_dt.Columns.Add("Shift_1_in");
            new_dt.Columns.Add("Shift_1_out");
            new_dt.Columns.Add("Shift_2_in");
            new_dt.Columns.Add("Shift_2_out");
            new_dt.Columns.Add("Shift_1_Working");
            new_dt.Columns.Add("Shift_2_Working");
            new_dt.Columns.Add("Attendance");

            on_leave = 0; absent = 0; present = 0; half_day = 0;
            grade_id = My.data("select Grade_id from dbo.[PRL_Employee_Master] where Employee_id='" + ddl_employee_name.SelectedValue + "'");
            month = ddl_month.SelectedIndex + 1;
            year = My.toIntS(ddl_year.Text);
            int total_working = 0;
            string emp_id = ddl_employee_name.SelectedValue.ToString();// ddl_employee_name.SelectedValue.ToString();
            int no_of_day = DateTime.DaysInMonth(year, month);


            //DataTable dt = My.dataTable("select '' Day,'' Working,*, CASE WHEN isnull(Shift_1_Working,0)+isnull(Shift_2_Working,0)>=7 THEN 'Present' else CASE WHEN isnull(Shift_1_Working,0)+isnull(Shift_2_Working,0)>=3 THEN 'Present' else CASE WHEN isnull(Shift_1_Working,0)+isnull(Shift_2_Working,0)=-4 THEN 'On Leave' else 'Absent' end end end attendance from dbo.[PRL_Employee_Daily_attendance_chart] where idate like '%" + year.ToString() + month.ToString("00") + "%' and Employee_id='" + emp_id + "'  order by idate");


            DataTable aadt = My.dataTable("Select id, format(DateTime,'hh:mm tt') time,format(DateTime,'yyyyMMdd') idate,DateTime    from dbo.[PRL_Attendance_Log]  where format(DateTime,'yyyyMM') = '" + year.ToString() + month.ToString("00") + "' and Employee_id='" + mycode.get_acutal_empid(ddl_employee_name.SelectedValue.ToString()) + "' order by DateTime");

            for (int d = 1; d <= no_of_day; d++)
            {
                string idate = year.ToString() + My.toDouble(month).ToString("00") + My.toDouble(d).ToString("00");
                string _date = new DateTime(year, month, d).ToString(My.Format_Sample);
                DateTime date = new DateTime(year, month, d);

                DataView dv1 = aadt.DefaultView;
                dv1.RowFilter = " idate='" + idate + "'";
                dv1.Sort = "DateTime ASC";
                DataTable aadt1 = dv1.ToTable();

                DataView dv2 = aadt.DefaultView;
                dv2.RowFilter = " idate='" + idate + "' ";
                dv2.Sort = "DateTime DESC";
                DataTable aadt2 = dv2.ToTable();


                DataRow dr = new_dt.NewRow();
                dr["Employee_id"] = emp_id;
                dr["Grade_id"] = grade_id;
                dr["Date"] = _date;
                dr["Day"] = date.ToString("dddd");
                DataRow[] drs = new_dt.Select(" Day='" + date.ToString("dddd") + "'");
                int days = drs.Length + 1;
                dr["Working"] = find_workig(date, emp_id, grade_id, days);
                dr["idate"] = idate;
                if (aadt1.Rows.Count == 0)
                {
                    dr["Shift_1_in"] = "AB";
                    dr["Shift_1_out"] = "AB";
                    dr["Shift_2_in"] = "AB";
                    dr["Shift_2_out"] = "AB";
                    dr["Shift_1_Working"] = "";
                    dr["Shift_2_Working"] = "";
                    dr["Attendance"] = "Absent";
                }
                else
                {
                    DateTime dateTime1 = Convert.ToDateTime(aadt1.Rows[0]["DateTime"]);
                    DateTime dateTime2 = Convert.ToDateTime(aadt2.Rows[0]["DateTime"]);
                    TimeSpan diff = dateTime2 - dateTime1;
                    int result = DateTime.Compare(dateTime2, dateTime1);
                    int i = My.toInt(diff);
                    if (result > 0)
                    {
                        dr["Attendance"] = "Present";



                    }
                    else
                    {
                        
                        dr["Attendance"] = "Absent";
                    }

                    dr["Shift_1_in"] = aadt1.Rows[0]["time"].ToString() != "" ? aadt1.Rows[0]["time"].ToString() : "AB";
                    dr["Shift_1_out"] = aadt2.Rows[0]["time"].ToString() != "" ? aadt2.Rows[0]["time"].ToString() : "AB";
                    dr["Shift_2_in"] = aadt1.Rows[0]["time"].ToString() != "" ? aadt1.Rows[0]["time"].ToString() : "AB";
                    dr["Shift_2_out"] = aadt2.Rows[0]["time"].ToString() != "" ? aadt2.Rows[0]["time"].ToString() : "AB";





                }
                new_dt.Rows.Add(dr);
            }
            half_day = new_dt.Select("Attendance = 'Half day'").Length;
            //present = new_dt.Select("Attendance = 'Full day'").Length;
            present = new_dt.Select("Attendance = 'Present'").Length;
            on_leave = new_dt.Select("Attendance = 'On Leave'").Length;

            string totadateinmonth = DateTime.DaysInMonth(year, month).ToString();

            total_working = find_weakly_off(grade_id, no_of_day);
            absent = total_working - (present + half_day + on_leave);

            lbl_emp_name.Text = ddl_employee_name.SelectedItem.Text;
            lbl_ttl_days_in_mnth.Text = totadateinmonth.ToString();
            lbl_ttl_present.Text = present.ToString();
            lbl_absent.Text = absent.ToString();
            lbl_ttl_working_days.Text = total_working.ToString();
            lbl_half_day_working.Text = half_day.ToString();
            lbl_on_leave.Text = on_leave.ToString();

            if (new_dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                grd_attendance.DataSource = null;
                grd_attendance.DataBind();
                pnl_grids.Visible = false;

            }
            else
            {
                // lbl_firm_name.Text = Session["firm_name"].ToString();
                // lbl_add.Text = Session["firmLocation"].ToString();
                // lbl_salary_date.Text = "Attendance chart of " + ddl_month.SelectedItem.Text + " " + ddl_year.SelectedItem.Text;
                grd_attendance.DataSource = new_dt.DefaultView;
                grd_attendance.DataBind();
                pnl_grids.Visible = true;
            }
        }

        protected void grd_attendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Working = (Label)e.Row.FindControl("lbl_Working");
                Label lbl_attendance = (Label)e.Row.FindControl("lbl_attendance");
                if(lbl_Working.Text.ToUpper()== "HOLIDAY")
                {
                    lbl_attendance.Text = "HOLIDAY";
                }
            }
        }

        private string find_workig(DateTime dat, object emp_id, string grade_id, int days)
        {
            string status = "";
            DataTable hdt = My.dataTable(" select * from PRL_holiday_list where idate='" + dat.ToString("yyyyMMdd") + "'");
            if (hdt.Rows.Count != 0)
            {
                status = hdt.Rows[0]["description"].ToString();
            }
            else
            {
                DataTable ldt = My.dataTable(" select * from dbo.[PRL_Leave_Entry_details] where Employee_id='" + emp_id + "'  and start_Date <= '" + dat.ToString("dd/MMM/yyyy HH:mm") + "' and end_Date>= '" + dat.ToString("dd/MMM/yyyy HH:mm") + "'  ");
                if (ldt.Rows.Count == 0)
                {
                    DataTable dt = My.dataTable("select * from PRL_Grade_wise_weekly_off where Grade_id='" + grade_id + "' and Day='" + dat.ToString("dddd") + "'");
                    if (dt.Rows.Count == 0)
                    {
                        status = "Working";
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (My.toBool(dr["All_week"].ToString()))
                            {
                                status = "Holiday";
                            }
                            else
                            {
                                if (days == 1)
                                {
                                    if (My.toBool(dr["First_week"].ToString()))
                                    {
                                        status = "1st " + dat.ToString("dddd");
                                    }
                                    else
                                    {
                                        status = "Working";
                                    }
                                }
                                if (days == 2)
                                {
                                    if (My.toBool(dr["Second_week"].ToString()))
                                    {

                                        status = "2nd " + dat.ToString("dddd");
                                    }
                                    else
                                    {
                                        status = "Working";
                                    }
                                }
                                if (days == 3)
                                {
                                    if (My.toBool(dr["Third_week"].ToString()))
                                    {
                                        status = "3rd " + dat.ToString("dddd");
                                    }
                                    else
                                    {
                                        status = "Working";
                                    }
                                }
                                if (days == 4)
                                {
                                    if (My.toBool(dr["Fourth_week"].ToString()))
                                    {
                                        status = "4th " + dat.ToString("dddd");
                                    }
                                    else
                                    {
                                        status = "Working";
                                    }
                                }

                                if (days == 5)
                                {
                                    if (My.toBool(dr["Fifth_week"].ToString()))
                                    {
                                        status = "5th " + dat.ToString("dddd");
                                    }
                                    else
                                    {
                                        status = "Working";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in ldt.Rows)
                    {
                        if (My.toDateTime(dr["start_Date"].ToString()) <= dat && My.toDateTime(dr["end_Date"].ToString()) >= dat)
                        {
                            status = "Not Working";
                        }
                        else
                        {
                            status = "Working";
                        }
                    }
                }
            }
            return status;
        }


        private int find_weakly_off(string grade_id, int days)
        {
            DataTable ld = My.dataTable(" select  * from dbo.[PRL_Grade_wise_weekly_off] where Grade_id=" + grade_id + "");
            int working_days = 0;
            int week = 1;
            for (int i = 1; i <= days; i++)
            {
                DateTime date = new DateTime(year, month, i);
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
            return working_days;
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
    }
}