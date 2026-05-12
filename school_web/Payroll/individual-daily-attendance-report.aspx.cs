using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class individual_daily_attendance_report : System.Web.UI.Page
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



                        txt_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id(ddl_employee_name, "select (Employee_Name+', '+Emp_Code) as Employee_Name,Employee_id from PRL_Employee_Master order by Employee_Name asc");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Individual_Monthly_Attendance_Report");
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
                if (txt_date.Text == "")
                {
                    Alertme("Please enter date", "warning");
                    txt_date.Focus();
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

        double on_leave = 0, absent = 0, present = 0, half_day = 0;
        string grade_id = "";
        DataTable new_dt;
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
            DataTable dtX = mycode.FillData("select Grade_id from dbo.[PRL_Employee_Master] where Employee_id='" + ddl_employee_name.SelectedValue + "'");

            string Employee_id = ddl_employee_name.SelectedValue.ToString();
            grade_id = dtX.Rows[0]["Grade_id"].ToString();
            string idate = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
            DateTime date = My.toDateTime(txt_date.Text);
            // DataTable dt = new DataTable();

            //  dt = My.dataTable("select '' day,'' working,*, CASE WHEN Shift_1_Working+Shift_2_Working>=7 THEN 'Full day' else CASE WHEN Shift_1_Working+Shift_2_Working>=3 THEN 'Half day' else CASE WHEN Shift_1_Working+Shift_2_Working=-4 THEN 'On Leave' else 'Absent' end end end attendance from dbo.[PRL_Employee_Daily_attendance_chart] where idate  like '%" + idate + "%' and Employee_id='" + Employee_id + "'  order by idate");

            DataTable aadt = My.dataTable("Select id, format(DateTime,'hh:mm tt') time,format(DateTime,'yyyyMMdd') idate,DateTime    from dbo.[PRL_Attendance_Log]  where format(DateTime,'yyyyMMdd') = '" + idate + "' and Employee_id='" + mycode.get_acutal_empid(ddl_employee_name.SelectedValue.ToString()) + "' order by DateTime");


            DataView dv1 = aadt.DefaultView;
            dv1.RowFilter = " idate='" + idate + "'";
            dv1.Sort = "DateTime ASC";
            DataTable aadt1 = dv1.ToTable();

            DataView dv2 = aadt.DefaultView;
            dv2.RowFilter = " idate='" + idate + "' ";
            dv2.Sort = "DateTime DESC";
            DataTable aadt2 = dv2.ToTable();


            DataRow dr = new_dt.NewRow();
            dr["Employee_id"] = ddl_employee_name.SelectedValue.ToString();
            dr["Grade_id"] = grade_id;
            dr["Date"] = txt_date.Text;
            dr["Day"] = date.ToString("dddd");
            DataRow[] drs = new_dt.Select(" Day='" + date.ToString("dddd") + "'");
            int days = drs.Length + 1;
            dr["Working"] = find_workig(date, ddl_employee_name.SelectedValue.ToString(), grade_id, days);
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


            lbl_emp_name.Text = ddl_employee_name.SelectedItem.Text;

        }


        private string find_workig(DateTime dat, string emp_id, string grade_id, int day)
        {

            string status = "";
            DataTable hdt = My.dataTable(" select * from PRL_holiday_list where idate='" + dat.ToString("yyyyMMdd") + "'");
            if (hdt.Rows.Count != 0)
            {
                status = hdt.Rows[0]["description"].ToString();
            }
            else
            {
                DataTable ldt = My.dataTable(" select * from dbo.[PRL_Leave_Entry_details] where Employee_id='" + emp_id + "'   and start_Date = '" + dat.ToString("dd/MMM/yyyy HH:mm") + "'");
                if (ldt.Rows.Count == 0)
                {

                    DataTable dt = My.dataTable("select * from PRL_Grade_wise_weekly_off where Grade_id='" + grade_id + "' and Day='" + dat.ToString("dddd") + "'");
                    if (dt.Rows.Count == 0)
                    {
                        status = "Working";
                    }
                    else
                    {
                        int days = find_days(dat);
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

        private int find_days(DateTime dat)
        {
            int days = 0;
            int month = My.toIntS(dat.ToString("MM"));
            int year = My.toIntS(dat.ToString("yyyy"));
            int dd = My.toIntS(dat.ToString("dd"));
            string day = dat.ToString("dddd");
            int no_of_day = DateTime.DaysInMonth(year, month);
            for (int d = 1; d <= dd; d++)
            {
                string temp_date = new DateTime(year, month, d).ToString(My.Format_Sample);
                string temp_day = My.toDateTime(temp_date).ToString("dddd");
                if (temp_day == day)
                {
                    days += 1;
                }
            }
            return days;
        }
    }
}