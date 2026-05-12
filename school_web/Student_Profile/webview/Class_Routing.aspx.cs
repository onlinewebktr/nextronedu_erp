using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class Class_Routing : System.Web.UI.Page
    {
        My mycode = new My();
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
            }
        } 
    }
}