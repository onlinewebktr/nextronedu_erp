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
    public partial class payment_history : System.Web.UI.Page
    {
        My imp = new My();
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");


                        ViewState["sesssionid"] = My.get_session_id();
                        ddl_session.SelectedValue = ViewState["sesssionid"].ToString();
                        ViewState["regid"] = Session["User"].ToString(); 
                        Bind_student_details();
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
         


        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }


        private void Bind_student_details()
        {
            pnl1.Visible = false;
            string query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV' and Transfer_Status in ('New','NT') and Session_id='"+ ddl_session.SelectedValue + "' order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
               
                Alertme("Sorry, your selected session does not have any payment history.", "warning");
                return;
            }
            else
            {
                pnl1.Visible = true;
                foreach (DataRow dr in dt.Rows)
                {
                    hd_admission_no.Value = ViewState["regid"].ToString();
                    hd_session.Value = dr["session"].ToString();
                    hd_branch.Value = dr["Branch_id"].ToString(); 
                } 
            }
        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {

            Bind_student_details();
        }
    }
}