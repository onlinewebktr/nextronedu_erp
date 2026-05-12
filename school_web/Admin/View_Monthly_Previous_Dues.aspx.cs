using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class View_Monthly_Previous_Dues : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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


                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id from dbo.[session_details]");
                        mycode.bind_all_ddl_with_id(ddlclass, "select Course_Name,course_id from dbo.[Add_course_table] order by Position");
                        ddlsession.SelectedValue = My.get_session_id();

                        ViewState["sessionid"] = My.get_session_id();

                        bind_grd_view_all();
                        ViewState["flag"] = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }

        }
        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    bind_grd_view_all();
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

        private void bind_grd_view_all()
        {
            try
            {
                string query = "  Select mwds.*,cm.Course_Name,ar.studentname from Misc_Fee_Master_Studentwise mwds join admission_registor ar  on ar.Session_id=mwds.Session_id and ar.admissionserialnumber=mwds.Admission_No join Add_course_table cm  on cm.course_id=ar.Class_id  where mwds.Session_id='" + ddlsession.SelectedValue + "'  order by cm.Position   ";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
            }
            catch
            { 
            }
        }

        protected void btn_fnd_by_class_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please choose session", "warning");
                ddlsession.Focus();
            }
            else if(ddlclass.SelectedItem.Text=="Select")
            {
                Alertme("Please choose class", "warning");
                ddlclass.Focus();
            }
            else
            {
                string query = "Select mwds.*,cm.Course_Name,ar.studentname from Misc_Fee_Master_Studentwise mwds join admission_registor ar  on ar.Session_id=mwds.Session_id and ar.admissionserialnumber=mwds.Admission_No join Add_course_table cm  on cm.course_id=ar.Class_id  where mwds.Session_id='" + ddlsession.SelectedValue + "' and ar.Class_id='"+ddlclass.SelectedValue+"'  order by cm.Position ";

                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
            }

        }
    }
}