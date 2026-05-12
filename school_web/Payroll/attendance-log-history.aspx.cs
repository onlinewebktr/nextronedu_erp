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
    public partial class attendance_log_history : System.Web.UI.Page
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
                        string mm = String.Format("{0:MMMM}", DateTime.Now);
                        ddl_month.Text = mm;
                        ddl_month.DataBind();

                        ddl_year.DataSource = My.bindYear();
                        ddl_year.Text = DateTime.Now.ToString("yyyy");
                        ddl_year.DataBind();
                        mycode.bind_all_ddl_with_id(ddl_employee_name, "select (Employee_Name+', '+Emp_Code) as Employee_Name,Emp_Code from PRL_Employee_Master where Status='Active' order by Employee_Name asc");
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


        private void find_data()
        {
            DataTable new_dt, emp_dt;
            new_dt = new DataTable();
            new_dt.Columns.Add("Date");
            new_dt.Columns.Add("idate");
            new_dt.Columns.Add("Attendance");
            new_dt.Columns.Add("Day");
            new_dt.Columns.Add("Working");
            int month = ddl_month.SelectedIndex + 1;
            int year = My.toIntS(ddl_year.Text);
            string emp_code = ddl_employee_name.SelectedValue;
            int no_of_day = DateTime.DaysInMonth(year, month);


            DataTable dt = My.dataTable("select format(DateTime,'hh:mm tt') Time,format(DateTime,'yyyyMMdd') DateTime  from dbo.[PRL_Attendance_Log]  where format(DateTime,'yyyyMM') = '" + year.ToString() + month.ToString("00") + "' and Employee_id='" + emp_code + "'  order by DateTime ");
            for (int i = 1; i <= no_of_day; i++)
            {
                string idate = year.ToString() + My.toDouble(month).ToString("00") + My.toDouble(i).ToString("00");
                string date = new DateTime(year, month, i).ToString(My.Format_Sample);
                DateTime _date = new DateTime(year, month, i);
                DataView dv = dt.DefaultView;
                dv.RowFilter = " DateTime='" + idate + "' ";
                DataTable adt = dv.ToTable();

                DataRow dr = new_dt.NewRow();
                dr["Date"] = date;
                dr["idate"] = _date.ToString("yyyyMMdd");
                dr["Day"] = _date.ToString("dddd");
                DataRow[] drs = new_dt.Select(" Day='" + _date.ToString("dddd") + "'");
                int days = drs.Length + 1;
                dr["Working"] = find_workig(_date, emp_code, days);
                if (adt.Rows.Count == 0)
                {
                    dr["Attendance"] = "Not Available";
                }
                else
                {
                    List<string> lst = new List<string>();
                    foreach (DataRow adr in adt.Rows)
                    {
                        lst.Add(adr["Time"].ToString());
                    }
                    dr["Attendance"] = string.Join(" , ", lst); ;
                }
                new_dt.Rows.Add(dr);
            }


            lbl_emp_name.Text = ddl_employee_name.SelectedItem.Text;
            if (new_dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                grd_attendance.DataSource = null;
                grd_attendance.DataBind();
                pnl_grids.Visible = false;

            }
            else
            {
                lbl_firm_name.Text = Session["firm_name"].ToString();
                lbl_add.Text = Session["firmLocation"].ToString();
                lbl_salary_date.Text = "Attendance Log History of " + ddl_month.SelectedItem.Text + " " + ddl_year.SelectedItem.Text;
                grd_attendance.DataSource = new_dt.DefaultView;
                grd_attendance.DataBind();
                pnl_grids.Visible = true;
            }
        }


        string empid;
        private object find_workig(DateTime dat, object emp_code, int days)
        {

            DataTable empdt = My.dataTable("select Employee_id from dbo.[PRL_Employee_Master] where Emp_Code='" + emp_code + "'");
            foreach (DataRow empdr in empdt.Rows)
            {
                empid = empdr["Employee_id"].ToString();
            }


            string status = "";
            DataTable hdt = My.dataTable(" select * from PRL_holiday_list where idate='" + dat.ToString("yyyyMMdd") + "'");
            if (hdt.Rows.Count != 0)
            {
                status = hdt.Rows[0]["description"].ToString();
            }
            else
            {

                DataTable ldt = My.dataTable(" select * from dbo.[PRL_Leave_Entry_details] where Employee_id='" + empid + "' ");
                if (ldt.Rows.Count == 0)
                {
                    DataTable dt = My.dataTable("select * from PRL_Grade_wise_weekly_off where  Day='" + dat.ToString("dddd") + "'");
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
         
    }
}