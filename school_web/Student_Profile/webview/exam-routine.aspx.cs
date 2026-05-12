using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class exam_routine : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    hd_admission_no.Value = ViewState["regid"].ToString();
                    student_info(ViewState["regid"].ToString());
                }
            }
        }

        private void student_info(string p)
        {
            string query = "select top 1 Session_id,Class_id,Section from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV'    and Transfer_Status in ('New','NT') order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                hd_session_id.Value = dt.Rows[0]["Session_id"].ToString();
                hd_class.Value = dt.Rows[0]["Class_id"].ToString();
                hd_section.Value = dt.Rows[0]["Section"].ToString();
                fetch_active_exam();
            }
        }

        private void fetch_active_exam()
        {
            DataTable dt = My.dataTable("select * from Active_exam_setting where Session_id='" + hd_session_id.Value + "' and Class_id='" + hd_class.Value + "'");
            if (dt.Rows.Count > 0)
            {
                hd_term_id.Value = dt.Rows[0]["Term_id"].ToString();
                hd_exam_id.Value = dt.Rows[0]["Exam_id"].ToString();
                check_exam_type();
            }
            else
            {
                hd_term_id.Value = "0";
                hd_exam_id.Value = "0";
                hd_shift.Value = "0";
            }
        }

        private void check_exam_type()
        {
            DataTable dt = My.dataTable("select Shift_type from Exam_Time_Table where Session_Id='" + hd_session_id.Value + "' and Class_id='" + hd_class.Value + "' and Exam_Term_Id='" + hd_term_id.Value + "' and Exam_id='" + hd_exam_id.Value + "'");
            if (dt.Rows.Count > 0)
            {
                hd_shift.Value = dt.Rows[0]["Shift_type"].ToString();
            }
        }
    }
}