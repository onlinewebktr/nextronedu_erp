using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class monthly_attendance_report : System.Web.UI.Page
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
                if (ddl_year.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose year", "warning");
                    ddl_year.Focus();
                    return;
                }
                if (ddl_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose month", "warning");
                    ddl_month.Focus();
                    return;
                }

                Calculate_Attendance();
                find_data();


            }
            catch (Exception ex)
            {
            }
        }

        private void Calculate_Attendance()
        {
            DataTable emp_dt = new DataTable();
            DataTable att_dt = new DataTable();
            month = ddl_month.SelectedIndex + 1;
            year = My.toIntS(ddl_year.Text);
            int no_of_day = DateTime.DaysInMonth(year, month);




            emp_dt = My.dataTable("select Employee_id,Emp_Code from dbo.[PRL_Employee_Master] where Status='Active'");
            att_dt = My.dataTable("Select id, format(DateTime,'hh:mm:ss tt') time,format(DateTime,'yyyyMMdd') idate,DateTime,Employee_id    from dbo.[PRL_Attendance_Log]  where format(DateTime,'yyyyMM') = '" + year.ToString() + month.ToString("00") + "' order by DateTime");
            for (int d = 1; d <= no_of_day; d++)
            {

                foreach (DataRow drr in emp_dt.Rows)
                {
                    string idate = year.ToString() + month.ToString("00") + d.ToString("00");
                    DateTime date = My.toDateTime(d.ToString("0") + "/" + month.ToString("00") + "/" + year.ToString());
                    DataView dv1 = att_dt.DefaultView;
                    dv1.RowFilter = " idate='" + idate + "' and Employee_id='" + drr["Emp_Code"].ToString() + "'";
                    dv1.Sort = "DateTime ASC";
                    DataTable aadt1 = dv1.ToTable();

                    DataView dv2 = att_dt.DefaultView;
                    dv2.RowFilter = " idate='" + idate + "' and Employee_id='" + drr["Emp_Code"].ToString() + "' ";
                    dv2.Sort = "DateTime DESC";
                    DataTable aadt2 = dv1.ToTable();

                    SqlDataAdapter ad = new SqlDataAdapter(" select * from dbo.[PRL_Employee_Daily_attendance_chart] where Employee_id='" + drr["Employee_id"] + "'  and idate='" + idate + "'", My.con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Employee_Daily_attendance_chart");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Employee_id"] = drr["Employee_id"].ToString();
                        dr["Date"] = date.ToString("dd-MMM-yyyy");
                        dr["idate"] = idate;
                        if (aadt1.Rows.Count > 0)
                        {

                            dr["Shift_1_in"] = aadt1.Rows[0]["time"].ToString();
                            dr["Shift_1_out"] = aadt2.Rows[0]["time"].ToString();
                        }

                        if (dr["Shift_1_in"].ToString() != "" && dr["Shift_1_out"].ToString() != "")
                        {
                            DateTime _in = My.toDateTime(dr["Shift_1_in"].ToString());
                            DateTime _out = My.toDateTime(dr["Shift_1_out"].ToString());
                            dr["Shift_1_woring"] = (_out - _in).TotalHours.ToString("0.00");
                            dr["Shift_1_Working"] = (_out - _in).TotalHours.ToString("0.00");

                        }
                        else
                        {
                            dr["Shift_1_woring"] = "0.00";
                            dr["Shift_1_Working"] = "0.00";

                        }
                        dr["attendance_type"] = "device";
                        dr["Status"] = "";
                        dr["day"] = "";
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (aadt1.Rows.Count > 0)
                            {

                                dr["Shift_1_in"] = aadt1.Rows[0]["time"].ToString();
                                dr["Shift_1_out"] = aadt2.Rows[0]["time"].ToString();
                            }
                            if (dr["Shift_1_in"].ToString() != "" && dr["Shift_1_out"].ToString() != "")
                            {
                                DateTime _in = My.toDateTime(dr["Shift_1_in"].ToString());
                                DateTime _out = My.toDateTime(dr["Shift_1_out"].ToString());
                                dr["Shift_1_woring"] = (_out - _in).TotalHours.ToString("0.00"); ;
                                dr["Shift_1_Working"] = (_out - _in).TotalHours.ToString("0.00"); ;
                            }
                            else
                            {
                                dr["Shift_1_woring"] = "0.00";
                                dr["Shift_1_Working"] = "0.00";
                            }
                        }
                    }
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);


                }
            }




        }


        string grade_id = "";
        int month;
        int year;
        DataTable dt = new DataTable();
        private void find_data()
        {
            month = ddl_month.SelectedIndex + 1;
            year = My.toIntS(ddl_year.Text);
            string idate = year.ToString() + My.toDouble(month).ToString("00");
            int no_of_day = DateTime.DaysInMonth(year, month);
            // dt = My.dataTable("select Employee_Name,Employee_id,Grade_id,'' Total_Working,(select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where idate like '%" + idate + "%' and Employee_id =PRL_Employee_Master.Employee_id and Shift_1_Working+Shift_2_Working>=7) Full_Day,(select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where idate like '%" + idate + "%' and Employee_id =PRL_Employee_Master.Employee_id and Shift_1_woring+Shift_2_woring>=3) Half_Day,(select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where idate like '%" + idate + "%' and Employee_id =PRL_Employee_Master.Employee_id and Shift_1_Working+Shift_2_Working=-4) On_Leave,'' Absent from dbo.PRL_Employee_Master  group by Employee_Name,Employee_id,Grade_id");

            //dt = My.dataTable("select Employee_Name,Employee_id,Grade_id,'' Total_Working,(select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where idate = '" + idate + "' and Employee_id =PRL_Employee_Master.Employee_id and Shift_1_woring+Shift_2_woring>=7) Full_Day,(select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where idate = '" + idate + "' and Employee_id =PRL_Employee_Master.Employee_id and Shift_1_woring+Shift_2_woring>=3) Half_Day,(select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where idate = '" + idate + "' and Employee_id =PRL_Employee_Master.Employee_id and Shift_1_Working+Shift_2_Working=-4) On_Leave,'' Absent from  PRL_Employee_Master  group by Employee_Name,Employee_id,Grade_id");

            dt = My.dataTable("select Employee_Name,Employee_id,Grade_id,'' Total_Working,(select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where idate like '%" + idate + "%' and Employee_id =PRL_Employee_Master.Employee_id and Shift_1_woring>=7) Full_Day,(select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where idate  like '%" + idate + "%' and Employee_id =PRL_Employee_Master.Employee_id and Shift_1_woring>=3) Half_Day,(select count(Employee_id) from dbo.[PRL_Employee_Daily_attendance_chart] where idate like '%" + idate + "%' and Employee_id =PRL_Employee_Master.Employee_id and Shift_1_Working=-4) On_Leave,'' Absent from  PRL_Employee_Master  group by Employee_Name,Employee_id,Grade_id");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int total_working = find_weakly_off(dr["Grade_id"].ToString(), no_of_day);
                    dr["Total_Working"] = total_working.ToString();
                    dr["Absent"] = (total_working - (My.toIntS(dr["Full_Day"]) + My.toIntS(dr["Half_Day"]) + My.toIntS(dr["On_Leave"]))).ToString();
                }
            }

            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                grd_attendance.DataSource = null;
                grd_attendance.DataBind();
                pnl_grids.Visible = false;

            }
            else
            {
                lbl_ttl_empS.Text = dt.Rows.Count.ToString();
                //lbl_firm_name.Text = Session["firm_name"].ToString();
                //lbl_add.Text = Session["firmLocation"].ToString();
                //lbl_salary_date.Text = "Attendance Summary of  " + ddl_month.SelectedItem.Text + " " + ddl_year.SelectedItem.Text;

                grd_attendance.DataSource = dt.DefaultView;
                grd_attendance.DataBind();
                pnl_grids.Visible = true;
            }
        }


        private int find_weakly_off(string grade_id, int days)
        {

            DataTable ld = My.dataTable(" select  * from dbo.[PRL_Grade_wise_weekly_off] where Grade_id=1");
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