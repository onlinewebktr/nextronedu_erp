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
    public partial class view_attandance_of_student : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();

        My imp = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["regid"] != null)
            {
                ViewState["regid"] = Request.QueryString["regid"].ToString();
                try
                {
                    if (!IsPostBack)
                    {
                        mycode.bind_all_ddl_with_id(ddl_month, "Select  Month,Month_Id from Month_Index order by Month_Id asc");
                        string a = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MM");
                        ddl_month.SelectedValue = a;
                        //imp.bind_ddl_year(ddlyear);
                        //ddlyear.Text = imp.year();
                        find_student_details();
                    }
                }
                catch
                {
                }
            }
        }

        private void find_student_details()
        {
            string query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and StudentStatus='AV' and Transfer_Status in ('New','NT') order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                std_basic_infoS.Visible = false;
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    std_basic_infoS.Visible = true;
                    hd_admission_no.Value = dr["admissionserialnumber"].ToString();
                    hd_class_id.Value = dr["Class_id"].ToString();
                    hd_session_id.Value = dr["Session_id"].ToString();
                    hd_session_name.Value = dr["session"].ToString();
                }
            }
        }
    }
}