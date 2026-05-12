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
    public partial class manual_attendance : System.Web.UI.Page
    {
        bool is_double_shift = false;
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

                        ddl_month.DataSource = My.bindMonth();
                        ddl_month.Text = DateTime.Now.ToString("MM");
                        ddl_month.DataBind();
                        ddl_year.DataSource = My.bindYear();
                        ddl_year.Text = DateTime.Now.ToString("yyyy");
                        ddl_year.DataBind();
                        mycode.bind_all_ddl_with_id(ddl_employee_name, "select (Employee_Name+', '+Emp_Code) as Employee_Name,Employee_id from PRL_Employee_Master order by Employee_Name asc");
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


        My mycode = new My();


        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_year.Text == "Select")
            {
                Alertme("Please Select Year", "warning");
                ddl_year.Focus();
                return;
            }
            if (ddl_month.Text == "Select")
            {
                Alertme("Please Select Month", "warning");
                ddl_month.Focus();
                return;
            }
            if (ddl_employee_name.Text == "Select")
            {
                Alertme("Please Select Employee", "warning");
                ddl_employee_name.Focus();
                return;
            }

            find_data();
        }



        private void find_data()
        {
            DataTable dt;
            int employee_id = 0;
            string grade_id = "";

            dt = new DataTable();
            dt.Columns.Add("Employee_id");
            dt.Columns.Add("Grade_id");
            dt.Columns.Add("Date");
            dt.Columns.Add("Day");
            dt.Columns.Add("Working");
            dt.Columns.Add("idate");
            dt.Columns.Add("Shift_1_in");
            dt.Columns.Add("Shift_1_out");
            dt.Columns.Add("Shift_2_in");
            dt.Columns.Add("Shift_2_out");
            grade_id = My.data("select Grade_id from dbo.[PRL_Employee_Master] where Employee_id='" + ddl_employee_name.SelectedValue + "'");
            int month = My.toIntS(ddl_month.Text);
            int year = My.toIntS(ddl_year.Text);
            string mname = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            int no_of_day = DateTime.DaysInMonth(year, month);
            DataTable at_dt = My.dataTable("select * from dbo.[PRL_Employee_Daily_attendance_chart] where idate like '%" + year.ToString() + month.ToString("00") + "%' and Employee_id='" + ddl_employee_name.SelectedValue + "'");
            for (int d = 1; d <= no_of_day; d++)
            {
                string idate = year.ToString() + My.toDouble(month).ToString("00") + My.toDouble(d).ToString("00");
                DateTime _date = new DateTime(year, month, d);

                string date = _date.ToString(My.Format_Sample);
                DataView dv = at_dt.DefaultView;
                dv.RowFilter = " idate='" + idate + "' ";
                DataTable adt = dv.ToTable();
                DataRow dr = dt.NewRow();
                dr["Employee_id"] = ddl_employee_name.SelectedValue;
                dr["Grade_id"] = grade_id;
                dr["Date"] = date;
                dr["Day"] = My.toDateTime(date).ToString("dddd"); DataRow[] drs = dt.Select(" Day='" + My.toDateTime(date).ToString("dddd") + "'");
                int days = drs.Length + 1;
                dr["Working"] = find_workig(_date, ddl_employee_name.SelectedValue, grade_id, days);
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
                dt.Rows.Add(dr);
            }
            grd_attendance.DataSource = dt.DefaultView;
            grd_attendance.DataBind();
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

                DataTable ldt = My.dataTable(" select * from dbo.[PRL_Leave_Entry_details] where Employee_id='" + emp_id + "' and start_Date <= '" + dat.ToString("dd-MMM-yyyy HH:mm") + "' and end_Date>= '" + dat.ToString("dd-MMM-yyyy HH:mm") + "'  ");
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

        protected void lnk_close_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }

        protected void link_make_attendance_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_date = (Label)row.FindControl("lbl_date");
                Label lbl_employee_id = (Label)row.FindControl("lbl_employee_id");
                Label lbl_grade_id = (Label)row.FindControl("lbl_grade_id");
                Label lbl_idate = (Label)row.FindControl("lbl_idate");

                ViewState["Date"] = lbl_date.Text;
                ViewState["Emp_id"] = lbl_employee_id.Text;
                ViewState["grade_id"] = lbl_grade_id.Text;
                ViewState["idate"] = lbl_idate.Text;
                if (My.Working_shift == "Single")
                {
                    is_double_shift = false;
                    chk_first_shift.Text = "Working Shift";
                    grp_shift_1.Visible = true;
                    grp_shift_2.Visible = false;
                    chk_first_shift.Enabled = false;
                    chk_first_shift.Checked = true;
                }
                else
                {
                    is_double_shift = true;
                    grp_shift_1.Visible = true;
                    grp_shift_2.Visible = true;
                    chk_first_shift.Enabled = true;
                }
                bind_applicable_leave();
                bind_default_time();
                myModal2.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_default_time()
        {
            string emp_id = ViewState["Emp_id"].ToString();
            string idate = ViewState["idate"].ToString();
            string grade_id = ViewState["grade_id"].ToString();

            DataTable dt = My.dataTable("select * from(select em.Grade_id,da.Employee_id,da.idate,format(cast(da.Shift_1_in as datetime),'hh:mm tt') Shift_1_in,format(cast(da.Shift_1_out as datetime),'hh:mm tt') Shift_1_out,format(cast(da.Shift_2_in as datetime),'hh:mm tt') Shift_2_in,format(cast(da.Shift_2_out as datetime),'hh:mm tt') Shift_2_out from PRL_Employee_Daily_attendance_chart da join PRL_Employee_Master em on da.Employee_id = em.Employee_id)t where idate='" + idate + "' and Employee_id='" + emp_id + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Shift_1_in"].ToString() == "") { fill_default(grade_id, "s1in"); } else { txt_morning_in.Text = dr["Shift_1_in"].ToString(); }
                    if (dr["Shift_1_out"].ToString() == "") { fill_default(grade_id, "s1out"); } else { txt_morning_out.Text = dr["Shift_1_out"].ToString(); }
                    if (dr["Shift_2_in"].ToString() == "") { fill_default(grade_id, "s2in"); } else { txt_evening_in.Text = dr["Shift_2_in"].ToString(); }
                    if (dr["Shift_2_out"].ToString() == "") { fill_default(grade_id, "s2out"); } else { txt_evening_out.Text = dr["Shift_2_out"].ToString(); }
                }
            }
            else
            {
                DataTable adt = My.dataTable("select Grade_id,format(Morning_in,'hh:mm tt') Morning_in,format(Morning_out,'hh:mm tt') Morning_out,format(Evening_in,'hh:mm tt') Evening_in,format(Evening_out,'hh:mm tt') Evening_out from PRL_Attendance_Timing_Setting where Grade_id='" + grade_id + "'");
                try
                {
                    txt_morning_in.Text = adt.Rows[0]["Morning_in"].ToString();

                    txt_morning_out.Text = adt.Rows[0]["Morning_out"].ToString();

                    txt_evening_in.Text = adt.Rows[0]["Evening_in"].ToString();

                    txt_evening_out.Text = adt.Rows[0]["Evening_out"].ToString();
                }
                catch
                {
                    fill_default(grade_id, "s1in");
                    fill_default(grade_id, "s1out");
                    fill_default(grade_id, "s2in");
                    fill_default(grade_id, "s2out");
                }

            }
        }

        private void fill_default(string grade_id, string sss)
        {
            DataTable dt = My.dataTable("select Grade_id,format(Morning_in,'hh:mm tt') Morning_in,format(Morning_out,'hh:mm tt') Morning_out,format(Evening_in,'hh:mm tt') Evening_in,format(Evening_out,'hh:mm tt') Evening_out from PRL_Attendance_Timing_Setting where Grade_id='" + grade_id + "'");

            if (dt.Rows.Count == 0)
            {
                // txt_morning_in.Text = "9:30 AM";
            }
            else
            {
                if (sss == "s1in")
                {
                    txt_morning_in.Text = dt.Rows[0]["Morning_in"].ToString();
                }
                if (sss == "s1out")
                {
                    txt_morning_out.Text = dt.Rows[0]["Morning_out"].ToString();
                }
                if (sss == "s2in")
                {
                    txt_evening_in.Text = dt.Rows[0]["Evening_in"].ToString();
                }
                if (sss == "s2out")
                {
                    txt_evening_out.Text = dt.Rows[0]["Evening_out"].ToString();
                }
            }
        }


        RadioButton chk;
        private void bind_applicable_leave()
        {
            mycode.bind_all_ddl_with_id(ddl_apl_leave, "select ln.Short_Name,al.Leave_Name_id from PRL_Employee_Applicabe_Leave al join PRL_Leave_Name_Master ln on al.Leave_Name_id=ln.Leave_Name_Id join PRL_Staff_Leave_Setup sl on al.Leave_Name_id=sl.Leave_id  where al.Is_applicable=1 and al.Employee_id='" + ViewState["Emp_id"].ToString() + "'");
        }

        protected void btn_save_att_Click(object sender, EventArgs e)
        {
            try
            {
                finally_make_attendance();
                Alertme("Attendance has been updated.", "success");
                myModal2.Visible = false;
                find_data();
            }
            catch (Exception ex)
            {
            }
        }


        private void finally_make_attendance()
        {
            string emp_id = ViewState["Emp_id"].ToString();
            string idate = ViewState["idate"].ToString();
            string date = ViewState["Date"].ToString();

            if (chk_is_leave_taken.Checked == true)
            {
                if (ddl_apl_leave.Text == "")
                {
                    Alertme("Please Select Applicable Leave Name", "warning");
                    chk_is_leave_taken.Focus();
                    return;
                }
            }


            string date_in = date + " " + txt_morning_in.Text;
            string date_out = date + " " + txt_morning_out.Text;

            DateTime date_in1 = Convert.ToDateTime(date_in, CultureInfo.InvariantCulture);//, "dd/MMM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            DateTime date_ou2 = Convert.ToDateTime(date, CultureInfo.InvariantCulture); //DateTime.ParseExact(date, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);


            // string finadate1 = date_in1.ToString("dd/MMM/yyyy hh:mm:ss tt");
            //  string finadate2= date_ou2.ToString("dd/MMM/yyyy hh:mm:ss tt");





            string empid = mycode.get_acutal_empid(emp_id);
            string a1 = "insert into PRL_Attendance_Log(Employee_id, DateTime) values('" + empid + "', '" + date_in1 + "')";
            mycode.executequery(a1);
            string a2 = "insert into PRL_Attendance_Log(Employee_id, DateTime) values('" + empid + "', '" + date_ou2 + "')";
            mycode.executequery(a2);

            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Daily_attendance_chart where Employee_id='" + emp_id + "' and idate='" + idate + "'", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Employee_Daily_attendance_chart");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["Employee_id"] = emp_id;
                dr["Date"] = date;
                dr["idate"] = idate;

                if (chk_is_absent.Checked == true)
                {
                    dr["Shift_1_in"] = null;
                    dr["Shift_1_out"] = null;
                    dr["Shift_1_Working"] = DBNull.Value;

                    if (is_double_shift)
                    {
                        dr["Shift_2_in"] = null;
                        dr["Shift_2_out"] = null;
                        dr["Shift_2_Working"] = DBNull.Value;
                    }
                    dr["attendance_type"] = "";
                }
                else
                {
                    if (chk_first_shift.Checked == true)
                    {
                        dr["Shift_1_in"] = txt_morning_in.Text;
                        dr["Shift_1_out"] = txt_morning_out.Text;
                        dr["Shift_1_Working"] = (Convert.ToDateTime(txt_morning_out.Text) - Convert.ToDateTime(txt_morning_in.Text)).TotalHours;
                    }
                    else
                    {
                        dr["Shift_1_in"] = null;
                        dr["Shift_1_out"] = null;
                        dr["Shift_1_Working"] = DBNull.Value;
                    }
                    if (is_double_shift)
                    {
                        if (chk_sec_shift.Checked == true)
                        {
                            dr["Shift_2_in"] = txt_evening_in.Text;
                            dr["Shift_2_out"] = txt_evening_out.Text;
                            dr["Shift_2_Working"] = (Convert.ToDateTime(txt_evening_out.Text) - Convert.ToDateTime(txt_evening_in.Text)).TotalHours;
                        }
                        else
                        {
                            dr["Shift_2_in"] = null;
                            dr["Shift_2_out"] = null;
                            dr["Shift_2_Working"] = DBNull.Value;
                        }
                    }
                    dr["attendance_type"] = "manual";
                }
                string year = idate.Substring(0, 4);
                string month = idate.Substring(4, 2);
                int day = My.toIntS(idate.Substring(6, 2));
                string qry1 = "", qry2 = "", qry3 = "";
                if (is_double_shift)
                {
                    qry1 = " ,S2_" + day + "_in,S2_" + day + "_out ";
                    qry2 = " ,'" + dr["Shift_2_in"] + "','" + dr["Shift_2_out"] + "' ";
                    qry3 = " ,S2_" + day + "_in='" + dr["Shift_2_in"] + "',S2_" + day + "_out='" + dr["Shift_2_out"] + "' ";
                }
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["Employee_id"] = emp_id;
                    dr["Date"] = date;
                    dr["idate"] = idate;
                    if (chk_is_absent.Checked == true)
                    {
                        dr["Shift_1_in"] = null;
                        dr["Shift_1_out"] = null;
                        dr["Shift_1_Working"] = DBNull.Value;
                        if (is_double_shift)
                        {
                            dr["Shift_2_in"] = null;
                            dr["Shift_2_out"] = null;
                            dr["Shift_2_Working"] = DBNull.Value;
                        }
                        dr["attendance_type"] = "";
                    }
                    else
                    {

                        if (chk_first_shift.Checked == true)
                        {
                            dr["Shift_1_in"] = txt_morning_in.Text;
                            dr["Shift_1_out"] = txt_morning_out.Text;
                            dr["Shift_1_Working"] = (Convert.ToDateTime(txt_morning_out.Text) - Convert.ToDateTime(txt_morning_in.Text)).TotalHours;
                        }
                        else
                        {
                            dr["Shift_1_in"] = null;
                            dr["Shift_1_out"] = null;
                            dr["Shift_1_Working"] = DBNull.Value;
                        }
                        if (is_double_shift)
                        {
                            if (chk_sec_shift.Checked == true)
                            {
                                dr["Shift_2_in"] = txt_evening_in.Text;
                                dr["Shift_2_out"] = txt_evening_out.Text;
                                dr["Shift_2_Working"] = (Convert.ToDateTime(txt_evening_out.Text) - Convert.ToDateTime(txt_evening_in.Text)).TotalHours;
                            }
                            else
                            {
                                dr["Shift_2_in"] = null;
                                dr["Shift_2_out"] = null;
                                dr["Shift_2_Working"] = DBNull.Value;
                            }
                        }
                        dr["attendance_type"] = "manual";
                    }
                    string year = idate.Substring(0, 4);
                    string month = idate.Substring(4, 2);
                    int day = My.toIntS(idate.Substring(6, 2));
                    string qry1 = "", qry2 = "", qry3 = "";
                    if (is_double_shift)
                    {
                        qry1 = " ,S2_" + day + "_in,S2_" + day + "_out ";
                        qry2 = " ,'" + dr["Shift_2_in"] + "','" + dr["Shift_2_out"] + "' ";
                        qry3 = " ,S2_" + day + "_in='" + dr["Shift_2_in"] + "',S2_" + day + "_out='" + dr["Shift_2_out"] + "' ";
                    }
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        protected void chk_is_leave_taken_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_is_leave_taken.Checked == true)
            {
                pnl_applicable_leave.Visible = false;
            }
            else
            {
                pnl_applicable_leave.Visible = false;
            }
        }
    }
}