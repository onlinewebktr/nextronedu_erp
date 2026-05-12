using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class Student_Fee_dues_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["sessionid"] = My.get_session_id();
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    Session["regid"] = ViewState["regid"].ToString();
                    hd_session_id.Value = ViewState["sessionid"].ToString();
                    hd_adm_no.Value = ViewState["regid"].ToString();
                    lbl_date.Text = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd MMM yyyy");
                    Find_student_details();
                    ViewState["month"] = "";
                }
            }
        }

        private void Find_student_details()
        {
            try
            {
                string query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and Transfer_Status in ('New','NT') and   Session_id='" + ViewState["sessionid"].ToString() + "' order by id desc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        My mycode = new My();
        private void find_details(string query)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    dues_update_headwise_transaction.update_student_dues(dt.Rows[0]["Session_id"].ToString(), dt.Rows[0]["Class_id"].ToString(), ViewState["regid"].ToString(), "0", "0", con);
                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                if (flag == true)
                { 
                    hd_class_id.Value = dt.Rows[0]["Class_id"].ToString();
                }
            }
        }
    }
}