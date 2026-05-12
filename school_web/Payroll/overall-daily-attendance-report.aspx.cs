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
    public partial class overall_daily_attendance_report : System.Web.UI.Page
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
                            grd_attendance.Columns[6].Visible = false;
                            grd_attendance.Columns[7].Visible = false;

                            grd_attendance.Columns[4].HeaderText = "Shift in";
                            grd_attendance.Columns[5].HeaderText = "Shift out";
                        }
                        else
                        {
                            is_double_shift = true;
                            grd_attendance.Columns[6].Visible = true;
                            grd_attendance.Columns[7].Visible = true;
                        }



                        txt_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id(ddl_employee, "select (Employee_Name+', '+Emp_Code) as Employee_Name,Employee_id from PRL_Employee_Master order by Employee_Name asc");
                        mycode.bind_all_ddl_with_id_All(ddl_grade, "select grade_name,grade_id from PRL_Grade_Master order by grade_name asc");

                        bind_collegedata();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Individual_Monthly_Attendance_Report");
            }
        }

        private void bind_collegedata()
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
                if (txt_date.Text == "")
                {
                    Alertme("Please choose date", "warning");
                    txt_date.Focus();
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
        DataTable dt = new DataTable();
        DataTable aadt = new DataTable();
        private void find_data()
        {
            on_leave = 0; absent = 0; present = 0; half_day = 0;
            string idate = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
            DateTime date = My.toDateTime(txt_date.Text);
            string grade_id = ddl_grade.SelectedValue.ToString();

            if (grade_id == "0")
            {
                // dt = My.dataTable(" select  '' day,'' working,'' attendance,Employee_Name,Emp_Code,Grade_id,(select grade_name from dbo.[PRL_Grade_Master] gr where gr.grade_id=em.Grade_id) grade_name,ac.* from dbo.[PRL_Employee_Master] em left join PRL_Employee_Daily_attendance_chart  ac on em. Employee_id= ac. Employee_id and idate='" + idate + "' where em.Status='Active' order by   Employee_Name");

                dt = My.dataTable(" select  '' day,'' working,'' attendance,Employee_Name,Emp_Code,Grade_id,em.Employee_id,(select grade_name from dbo.[PRL_Grade_Master] gr where gr.grade_id=em.Grade_id) grade_name,ac.* from dbo.[PRL_Employee_Master] em   left   join  PRL_Employee_Daily_attendance_chart  ac on em. Employee_id= ac. Employee_id   and idate='" + idate + "' where em.Status='Active' order by   Employee_Name");

            }



            else
            {
                // dt = My.dataTable(" select  '' day,'' working,'' attendance,Employee_Name,Emp_Code,em.Grade_id,(select grade_name from dbo.[PRL_Grade_Master] gr where gr.grade_id=em.Grade_id) grade_name,ac.* from dbo.[PRL_Employee_Master] em join PRL_Grade_Master gm on gm.grade_id=em.Grade_id left join PRL_Employee_Daily_attendance_chart  ac on em. Employee_id= ac. Employee_id and idate='" + idate + "'     where gm.grade_id='" + grade_id + "' and em.Status='Active' order by   Employee_Name");

                dt = My.dataTable(" select  '' day,'' working,'' attendance,Employee_Name,Emp_Code,em.Grade_id,em.Employee_id,(select grade_name from dbo.[PRL_Grade_Master] gr where gr.grade_id=em.Grade_id) grade_name,ac.* from dbo.[PRL_Employee_Master] em join Grade_Master gm on gm.grade_id=em.Grade_id   left   join  Employee_Daily_attendance_chart  ac on em. Employee_id= ac. Employee_id and idate='" + idate + "'     where gm.grade_id='" + grade_id + "' and em.Status='Active' order by   Employee_Name");
            }

            aadt = My.dataTable("Select id, format(DateTime,'hh:mm tt') time,format(DateTime,'yyyyMMdd') idate,DateTime,Employee_id    from dbo.[PRL_Attendance_Log]  where format(DateTime,'yyyyMMdd') = '" + idate + "'order by DateTime ");

            foreach (DataRow dr in dt.Rows)
            {

                dr["Employee_Name"] = mycode.get_employyenameand_code(dr["Employee_id"].ToString());
                dr["Date"] = date.ToString("dd/MM/yyyy");
                dr["day"] = date.ToString("dddd");
                dr["working"] = find_workig(date, dr["Employee_id"].ToString(), dr["Grade_id"].ToString());


                DataView dv1 = aadt.DefaultView;
                dv1.RowFilter = " idate='" + idate + "' and Employee_id='" + dr["Emp_Code"].ToString() + "'";
                dv1.Sort = "id ASC";
                DataTable aadt1 = dv1.ToTable();

                DataView dv2 = aadt.DefaultView;
                dv2.RowFilter = " idate='" + idate + "' and Employee_id='" + dr["Emp_Code"].ToString() + "' ";
                dv2.Sort = "id DESC";
                DataTable aadt2 = dv1.ToTable();

                if (aadt1.Rows.Count > 0)
                {
                    dr["Shift_1_in"] = aadt1.Rows[0]["time"].ToString();

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
                }
                else
                {
                    dr["Shift_1_in"] = "AB";
                }

                if (aadt2.Rows.Count > 1)
                {
                    dr["Shift_1_out"] = aadt2.Rows[0]["time"].ToString();
                    present++;
                }
                else
                {
                    dr["Shift_1_out"] = "AB";
                    absent++;
                }



            }


            lbl_date.Text = txt_date.Text;
            lbl_no_of_emp.Text = dt.Rows.Count.ToString();
            lbl_absent.Text = absent.ToString();
            lbl_ttl_present.Text = present.ToString();
            lbl_on_leave.Text = on_leave.ToString();

            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                grd_attendance.DataSource = null;
                grd_attendance.DataBind();
                pnl_grids.Visible = false;

            }
            else
            {
                
                grd_attendance.DataSource = dt.DefaultView;
                grd_attendance.DataBind();
                pnl_grids.Visible = true;
            }
        }


        private string find_workig(DateTime dat, object emp_id, string grade_id)
        {
            string status = "";
            DataTable hdt = My.dataTable(" select * from PRL_holiday_list where idate='" + dat.ToString("yyyyMMdd") + "'");
            if (hdt.Rows.Count != 0)
            {
                status = hdt.Rows[0]["description"].ToString();
            }
            else
            {
                DataTable ldt = My.dataTable(" select * from dbo.[PRL_Leave_Entry_details] where Employee_id='" + emp_id + "' and start_Date <= '" + dat.ToString("dd/MMM/yyyy HH:mm") + "' and end_Date>= '" + dat.ToString("dd/MMM/yyyy HH:mm") + "' ");
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

        protected void btn_find_by_emp_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_employee.Text == "")
                {
                    Alertme("Please Select Employee", "warning");
                    ddl_employee.Focus();
                    return;
                }
                if (dt.Rows.Count == 0)
                {
                    Alertme("Please Find Attendance of any date", "warning");
                    btn_find.Focus();
                    return;
                }

                string idate = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
                string emp_code = ddl_employee.SelectedValue.ToString();
                DataTable ndt = new DataTable();


                DataView dv = dt.DefaultView;
                dv.RowFilter = "Emp_Code='" + emp_code + "'";
                ndt = dv.ToTable();


                lbl_no_of_emp.Text = ndt.Rows.Count.ToString();

                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no data list exist", "warning");
                    grd_attendance.DataSource = null;
                    grd_attendance.DataBind();
                    pnl_grids.Visible = false;

                }
                else
                {
                   
                    grd_attendance.DataSource = ndt.DefaultView;
                    grd_attendance.DataBind();
                    pnl_grids.Visible = true;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}