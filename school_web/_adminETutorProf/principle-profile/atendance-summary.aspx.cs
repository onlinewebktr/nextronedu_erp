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
    public partial class atendance_summary : System.Web.UI.Page
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
                    bind_class();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void bind_class()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Course_Name,course_id from Add_course_table order by Position", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_classs.DataTextField = "Course_Name";
                ddl_classs.DataValueField = "course_id";
                ddl_classs.DataSource = reader;
                ddl_classs.DataBind();
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_message.Text = "";
                string qoute = "'";
                //For Class
                bool isClassSelectd = false; string selectClassid = "";
                foreach (ListItem item in ddl_classs.Items)
                {
                    if (item.Selected)
                    {
                        selectClassid = selectClassid + qoute + item.Value + qoute + ",";
                        isClassSelectd = true;
                    }
                }
                if (isClassSelectd == false)
                {
                    ddl_classs.Focus();
                    lbl_message.Text = "Please select class.";
                    return;
                }
                if (isClassSelectd == true)
                {
                    selectClassid = selectClassid.Remove(selectClassid.Length - 1);
                }
                if (txt_date.Text == "")
                {
                    txt_date.Focus();
                    lbl_message.Text = "Please choose date.";
                    return;
                }
                find_attendance(selectClassid);
            }
            catch (Exception ex)
            {
            }
        }

        private void find_attendance(string selectClassid)
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
            hd_class_id.Value = selectClassid;
            string qry = "WITH admission_counts AS (SELECT Session_id, Class_id, Section, COUNT(Id) AS TtlStd FROM admission_registor WHERE Status = 1 GROUP BY Session_id, Class_id, Section ), attendance_counts AS (SELECT  Session_id, Class_id, Section, COUNT(Id) AS TtlStudents, SUM(CASE WHEN Attendance_Status = 'Present' THEN 1 ELSE 0 END) AS TtlPresent, SUM(CASE WHEN Attendance_Status ='Absent' THEN 1 ELSE 0 END) AS TtlAbsent, SUM(CASE WHEN Attendance_Status = 'Leave' THEN 1 ELSE 0 END) AS TtlLeave FROM Student_Attendance_saved_Class_Wise WHERE Attendance_IDate='" + Idate + "' GROUP BY Session_id, Class_id, Section) SELECT  t.*, ISNULL(ac.TtlStd, 0) AS TtlStd, ISNULL(at.TtlStudents, 0) AS TtlStudents, ISNULL(at.TtlPresent, 0) AS TtlPresent, ISNULL(at.TtlAbsent, 0) AS TtlAbsent, ISNULL(at.TtlLeave, 0) AS TtlLeave FROM (SELECT DISTINCT t1.Session_id, t1.class, t1.Class_id, t1.Section, t2.Position FROM admission_registor t1 JOIN Add_course_table t2 ON t1.Class_id = t2.course_id WHERE t1.Session_id='" + ViewState["session_id"].ToString() + "' AND t1.Status='1' and t1.Class_id in (" + selectClassid + ")) t LEFT JOIN admission_counts ac ON t.Session_id = ac.Session_id AND t.Class_id = ac.Class_id AND t.Section = ac.Section LEFT JOIN attendance_counts at ON t.Session_id = at.Session_id AND t.Class_id = at.Class_id AND t.Section = at.Section ORDER BY t.Position, t.Section ASC";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                fetch_overall(ViewState["session_id"].ToString(), selectClassid, Idate);
            }
        }

        private void fetch_overall(string session_id, string selectClassid, string idate)
        {
            string qry = "select 'Total_Std' as Attendance_Status, count(Id) as TotalCount from admission_registor where Session_id='" + session_id + "' and Status='1' and Class_id in (" + selectClassid + ") union all SELECT s.Attendance_Status, COUNT(t.Attendance_Status) AS TotalCount FROM (VALUES ('Present'),('Absent'),('Leave')) AS s(Attendance_Status) LEFT JOIN Student_Attendance_saved_Class_Wise t ON t.Attendance_Status=s.Attendance_Status AND t.Session_id='" + session_id + "' AND t.Class_id IN (" + selectClassid + ") AND t.Idate='" + idate + "' GROUP BY s.Attendance_Status;";
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

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = e.Item;
                    Label lbl_ttl_std = (Label)e.Item.FindControl("lbl_ttl_std");
                    Label lbl_ttl_std_att = (Label)e.Item.FindControl("lbl_ttl_std_att");
                    Label lbl_is_marked = (Label)e.Item.FindControl("lbl_is_marked");
                    lbl_is_marked.Text = "Yes";
                    if (lbl_ttl_std_att.Text == "0")
                    {
                        if (My.toDouble(lbl_ttl_std.Text) > 0)
                        {
                            lbl_is_marked.Text = "No";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}