using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class student_monthly_attendance : System.Web.UI.Page
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
                        string sessions = My.get_session();
                        string[] stringSeparatorss = new string[] { "-" };
                        string[] arrs = sessions.Split(stringSeparatorss, StringSplitOptions.None);
                        string Year1 = arrs[0];
                        string Year2 = arrs[1];


                        mycode.bind_ddl_year(ddlyear);
                        ddlyear.Text = mycode.year();
                        mycode.bind_all_ddl_with_id(ddl_month, "Select  Month,Month_Id from Month_Index order by Position asc");
                        ddl_month.SelectedValue = mycode.get_current_month_id();


                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");


                        int starte = My.toint(Year1);
                        int endyaer = My.toint(Year2);
                        ArrayList ar = new ArrayList();
                        ar.Add("Select");
                        for (int i = starte; i <= endyaer; i++)
                        {
                            ar.Add(i);
                        }
                        ddlyear.DataSource = ar;
                        ddlyear.DataBind(); 
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Class_routine_chart");
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

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "' and Session_id='"+ ViewState["Sessionid"].ToString() + "' order by Section");
            }
        }

    }
}