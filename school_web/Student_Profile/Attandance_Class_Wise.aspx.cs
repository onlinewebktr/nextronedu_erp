using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class Attandance_Class_Wise : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["regid"] = Session["User"].ToString();
                        ViewState["sesssionid"] = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_month, "Select  Month,Month_Id from Month_Index order by Position asc");
                        string a = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MM");
                        ddl_month.SelectedValue = a;
                        find_student_details();
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch (Exception ex)
            {
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
                
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    
                    hd_admission_no.Value = dr["admissionserialnumber"].ToString();
                    hd_class_id.Value = dr["Class_id"].ToString();
                    hd_session_id.Value = dr["Session_id"].ToString();
                    hd_session_name.Value = dr["session"].ToString();
                }
            }
        }
    }
}