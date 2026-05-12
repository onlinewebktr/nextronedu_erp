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
    public partial class Student_attendance_log_history_Student_Month_Wise : System.Web.UI.Page
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
                        lbl_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        mycode.bind_all_ddl_with_id(ddlclass, " Select Course_Name, course_id from Add_course_table order by Position ");


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
            catch
            {
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

         
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddlclass.SelectedValue + "'  order by Section");
                    

                }
            }
            catch (Exception ex)
            {
            }
        }
        string type;

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session.", "warning");
            }
            else if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_employee_name, "select (studentname+', '+admissionserialnumber) as Employee_Name,admissionserialnumber from admission_registor where Class_id='" + ddlclass.SelectedValue + "' and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "'  order by rollnumber asc");
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
                else if (ddl_section.Text == "Select")
                {
                    Alertme("Please select section", "warning");
                    ddl_section.Focus();
                }
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
                student_info();
            }
            catch (Exception ex)
            {
            }
        }

        private void student_info()
        {
            string query = "Select studentname,class,rollnumber,Section,session,admissionserialnumber   Status='1' and  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber='" + ddl_employee_name.SelectedValue + "' and Section='" + ddl_section.SelectedValue + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            try
            {
                if (dt.Rows.Count == 0)
                {


                }
                else
                {
                    lbl_ttl_studentname.Text = dt.Rows[0]["studentname"].ToString();
                    lbl_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                    lbl_session.Text = dt.Rows[0]["session"].ToString();
                   
                    lbl_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
                    lbl_section.Text = dt.Rows[0]["Section"].ToString();
                }

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


            DataTable dt = My.dataTable("select format(sal.DateTime,'hh:mm tt') Time,format(sal.DateTime,'yyyyMMdd') DateTime  from dbo.[Student_Attendance_Log]   sal join admission_registor ar on sal.admissionserialnumber=ar.admissionserialnumber  where format(sal.DateTime,'yyyyMM') = '" + year.ToString() + month.ToString("00") + "' and sal.admissionserialnumber='" + emp_code + "' and ar.Status='1'  order by sal.DateTime ");
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


            lbl_ttl_studentname.Text = ddl_employee_name.SelectedItem.Text;
            if (new_dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                grd_attendance.DataSource = null;
                grd_attendance.DataBind();
                pnl_grids.Visible = false;

            }
            else
            {

                grd_attendance.DataSource = new_dt.DefaultView;
                grd_attendance.DataBind();
                pnl_grids.Visible = true;
            }
        }


        string empid;
        private object find_workig(DateTime dat, object emp_code, int days)
        {

            

            string status = "";
            DataTable hdt = My.dataTable(" select * from School_Holiday_Calendar where Idate='" + dat.ToString("yyyyMMdd") + "'");
            if (hdt.Rows.Count != 0)
            {
                status = hdt.Rows[0]["Type"].ToString();
            }
            else
            {
                status = "N/A";



            }
            return status;
        }

       

       



    }
}