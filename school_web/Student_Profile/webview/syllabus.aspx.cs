using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class syllabus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    string session_id = My.get_session_id();
                    string adm_no = Request.QueryString["regid"].ToString();

                    pnl_syllabus.Visible = false;
                    pnl_bo_found.Visible = true;
                    DataTable dt = My.dataTable("select top 1 Class_id,Section from admission_registor where Session_id='" + session_id + "' and admissionserialnumber='" + adm_no + "' and Status='1' order by id desc");
                    if (dt.Rows.Count > 0)
                    {
                        fetch_syllabus(session_id, adm_no, dt.Rows[0]["Class_id"].ToString(), dt.Rows[0]["Section"].ToString());
                    }

                }
            }
        }

        private void fetch_syllabus(string session_id, string adm_no, string Class_id, string Section)
        {
            pnl_syllabus.Visible = false;
            pnl_bo_found.Visible = true;
            DataTable dt = My.dataTable("select * from Syllabus_master_new where Session_id='" + session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "'");
            if (dt.Rows.Count > 0)
            {
                pnl_syllabus.Visible = true;
                pnl_bo_found.Visible = false; 
                rp_syllabus.DataSource = dt;
                rp_syllabus.DataBind();
            }
        }
    }
}