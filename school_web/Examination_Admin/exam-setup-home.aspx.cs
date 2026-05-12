using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class exam_setup_home1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Session["Admin"] = "edunext2021";
               
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["sesssionid"] = My.get_session_id();

                        bin_header_data();
                        bind_menus();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Report-Menus");
            }
        }

        private void bin_header_data()
        {
            string sql = " select  * from Exam_Menu_Group_List_web where Type=1 and Group_id in ( select distinct  MenuID from Exam_MenuPermissionForUser_web where UserID = '" + ViewState["Userid"] + "') order by Position  ";
            DataTable dt = mycode.FillData(sql);
            if (dt.Rows.Count == 0)
            {
                rd_menus.Visible = false;
            }
            else
            {
                if(dt.Rows.Count==5)
                {
                    rd_menus.Visible = true;
                }
                else
                {
                    rd_menus.Visible = false;

                }
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

        My mycode = new My();
        private void bind_menus()
        {
            DataTable dt = mycode.FillData("select *,CASE WHEN Menu_page = '#!' THEN 'disabled-menu' END AS disabled from Exam_Dashboard_report_menu where Group_id='1' and Status='1' order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rd_menus.DataSource = null;
                rd_menus.DataBind();
            }
            else
            {
                rd_menus.DataSource = dt;
                rd_menus.DataBind();
            }
        }
    }
}