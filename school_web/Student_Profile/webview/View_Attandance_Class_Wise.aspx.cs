using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Globalization;

namespace school_web.Student_Profile.webview
{
    public partial class View_Attandance_Class_Wise : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_month, "Select  Month,Month_Id from Month_Index order by Position asc");
                        string a = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MM");
                        ddl_month.SelectedValue = a; 
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
            ViewState["sessionid"] = My.get_session_id();
            string query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and StudentStatus='AV' and Transfer_Status in ('New','NT') and  Session_id='" + ViewState["sessionid"].ToString() + "' order by id desc";
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

                    lbl_name.Text = dr["studentname"].ToString();

                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    lbl_class.Text = dr["class"].ToString();
                    lbl_roll_no.Text = dr["rollnumber"].ToString();
                    lbl_section.Text = dr["Section"].ToString();
                }
            }
        }
    }
}