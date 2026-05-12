using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.principle_profile
{
    public partial class hostel_atendance_summary : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["session_id"] = My.get_session_id();
                    txt_date.Text = mycode.date_with_slash(); 
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        } 

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_message.Text = "";
                if (txt_date.Text == "")
                {
                    txt_date.Focus();
                    lbl_message.Text = "Please choose date.";
                    return;
                }
                find_attendance();
            }
            catch (Exception ex)
            {
            }
        }

        private void find_attendance()
        {
            string Idate = "0";
            try
            {
                string day = txt_date.Text.Substring(8, 2);
                string month = txt_date.Text.Substring(5, 2);
                string year = txt_date.Text.Substring(0, 4);
                Idate = year + month + day;
            }
            catch (Exception ex) { }
            hd_idate.Value = Idate;
            hd_session_id.Value = ViewState["session_id"].ToString();
            fetch_overall(ViewState["session_id"].ToString(), Idate);
        }

        private void fetch_overall(string session_id, string idate)
        {
            string qry = "select 'Total_Std' as Attendance_Status, count(Id) as TotalCount from admission_registor where Session_id='" + session_id + "' and Status='1' and hosteltaken='Yes' union all SELECT s.Attendance_Status, COUNT(t.Attendance_Status) AS TotalCount FROM (VALUES ('Present'),('Absent'),('Leave')) AS s(Attendance_Status) LEFT JOIN Student_attendance_saved_hostel_Wise t ON t.Attendance_Status=s.Attendance_Status AND t.Session_id='" + session_id + "' AND t.Idate='" + idate + "' GROUP BY s.Attendance_Status;";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                double ttlStdss = 0;
                double ttlTaken = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Attendance_Status"].ToString() == "Total_Std")
                    {
                        ttlStd.InnerText = dr["TotalCount"].ToString();
                        ttlStdss = My.toDouble(dr["TotalCount"].ToString());
                    }
                    if (dr["Attendance_Status"].ToString() == "Present")
                    {
                        ttlpers.InnerText = dr["TotalCount"].ToString();
                        ttlTaken = ttlTaken + My.toDouble(dr["TotalCount"].ToString());
                    }
                    if (dr["Attendance_Status"].ToString() == "Absent")
                    {
                        ttlAbsnt.InnerText = dr["TotalCount"].ToString();
                        ttlTaken = ttlTaken + My.toDouble(dr["TotalCount"].ToString());
                    }
                    if (dr["Attendance_Status"].ToString() == "Leave")
                    {
                        ttlleavE.InnerText = dr["TotalCount"].ToString();
                        ttlTaken = ttlTaken + My.toDouble(dr["TotalCount"].ToString());
                    }
                }
                ttllNotTkn.InnerText = (ttlStdss - ttlTaken).ToString();
            }
        }
    }
}