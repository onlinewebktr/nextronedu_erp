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
    public partial class hostel_attendance_report : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["sessionid"] = My.get_session_id();
                    code.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details where use_mode='1'");
                    ddl_session.SelectedValue = code.get_session_id(code.get_session());
                    code.bind_all_ddl_with_id(ddl_house, "select house_name,house_id from house_master order by house_name asc");
                    txt_date.Text = code.date();
                }
                catch
                {
                }
            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            lbl_message.Text = "";
            if (ddl_session.SelectedItem.Text == "Select")
            {
                lbl_message.Text = "Please select session.";
            }
            else if (ddl_house.SelectedItem.Text == "Select")
            {
                lbl_message.Text = "Please select house.";
            }
            else if (txt_date.Text == "")
            {
                lbl_message.Text = "Please select date.";
            }
            else
            {
                finally_open_student_list();
            }
        }

        private void finally_open_student_list()
        {
            string Idate = My.DateConvertToIdate(txt_date.Text).ToString();  
            string query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status from admission_registor ar join Student_attendance_saved_hostel_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no and ar.Session_id=sas.Session_id where sas.House_id='" + ddl_house.SelectedValue + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and  sas.Attendance_IDate='" + Idate + "' order by ar.rollnumber asc";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                lbl_message.Text = "Sorry there are no attendance list found.";
                rd_view.DataSource = null;
                rd_view.DataBind();
                //grid111.Visible = false;

                totalStudents.InnerText = "0";
                presentStudents.InnerText = "0";
                absentStudents.InnerText = "0";
                leaveStudents.InnerText = "0";
            }
            else
            {
                totalStudents.InnerText = dt.Rows.Count.ToString();
                rd_view.DataSource = dt;
                rd_view.DataBind();
                //grid111.Visible = true;

                bind_student_count(Idate); 
            }
        }

        private void bind_student_count(string Idate)
        {
            string query = " Select sas.Attendance_Status,count(sas.Id) as total  from admission_registor ar join Student_attendance_saved_hostel_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and  ar.Session_id=sas.Session_id  where sas.House_id='" + ddl_house.SelectedValue + "'  and ar.session='" + ddl_session.SelectedItem.Text + "' and sas.Attendance_IDate='" + Idate + "' group by Attendance_Status";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Attendance_Status"].ToString() == "Present")
                    {
                        presentStudents.InnerText = dr["total"].ToString();
                    }
                    else if (dr["Attendance_Status"].ToString() == "Absent")
                    {
                        absentStudents.InnerText = dr["total"].ToString();
                    }
                    else
                    {
                        leaveStudents.InnerText = dr["total"].ToString();
                    }
                }
            }
        }


        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_Attendance_Status = (Label)e.Item.FindControl("lbl_Attendance_Status");
                if (lbl_Attendance_Status.Text == "Present")
                {
                    lbl_Attendance_Status.CssClass += "status present";
                }
                else if (lbl_Attendance_Status.Text == "Absent")
                {
                    lbl_Attendance_Status.CssClass += "status absent";
                }
                else
                {
                    lbl_Attendance_Status.CssClass += "status leave";
                }
            }
        }
    }
}