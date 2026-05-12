using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web.Student_Profile.webview
{
    public partial class biometric : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    student_info(ViewState["regid"].ToString());
                }
            }

        }
        My mycode = new My();
        private void student_info(string regid)
        {
            string query = "Select top 1 studentname,class,rollnumber,Section,session,admissionserialnumber,Current_Semester_or_Year from admission_registor where  admissionserialnumber='" + regid + "' order by id desc ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            try
            {
                if (dt.Rows.Count == 0)
                {


                }
                else
                {
                    //lbl_ttl_studentname.Text = dt.Rows[0]["studentname"].ToString();
                    //lbl_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                    //lbl_session.Text = dt.Rows[0]["session"].ToString();
                    //lbl_academy_semester.Text = dt.Rows[0]["Current_Semester_or_Year"].ToString();
                    //lbl_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
                    //lbl_section.Text = dt.Rows[0]["Section"].ToString();
                
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
            string emp_code = ViewState["regid"].ToString();
            int no_of_day = DateTime.DaysInMonth(year, month);


            DataTable dt = My.dataTable("select format(DateTime,'hh:mm tt') Time,format(DateTime,'yyyyMMdd') DateTime  from dbo.[Student_Attendance_Log]  where format(DateTime,'yyyyMM') = '" + year.ToString() + month.ToString("00") + "' and admissionserialnumber='" + emp_code + "'  order by DateTime ");
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


           
            if (new_dt.Rows.Count == 0)
            {
                lbl_msg.Text = "Sorry there are no data list exist";
             
                grd_attendance.DataSource = null;
                grd_attendance.DataBind();
                

            }
            else
            {

                grd_attendance.DataSource = new_dt.DefaultView;
                grd_attendance.DataBind();
                
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
        private void Alert(string msg)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddl_year.Text == "")
            {
                Alert("Please Select Year");
                ddl_year.Focus();
                return;
            }
            if (ddl_month.Text == "")
            {
                Alert("Please Select Month");
               
                ddl_month.Focus();
                return;
            }
            else
            {
                find_data();
            }
           
        }
    }
}