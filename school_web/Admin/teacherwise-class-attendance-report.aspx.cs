using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class teacherwise_class_attendance_report : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Admin"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["Sessionid"] = My.get_session_id();
                        hd_session_id.Value = ViewState["Sessionid"].ToString();
                        hd_user_id.Value = ViewState["Userid"].ToString();
                        string today_date = mycode.date();
                        DateTime cfromDateTime = Convert.ToDateTime(today_date);
                        DateTime cfinaldate = cfromDateTime.AddDays(-7);
                        string cDate = cfinaldate.ToString("dd/MM/yyyy");
                        txt_date_from.Text = cDate;
                        txt_date_to.Text = mycode.date(); 
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Class_routine_chart");
            }
        }
    }
}