using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Admin
{
    public partial class Event : System.Web.UI.Page
    {
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
                     

                        bind_menus();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Event");
            }
        }
        My mycode = new My();
        private void bind_menus()
        {
            DataTable dt = mycode.FillData("select distinct Menu_page,Menu_icon,Menu_name,Position,CASE WHEN Menu_page = '#!' THEN 'disabled-menu' END AS disabled from Dashboard_report_menu where Group_id='15' and Status='1' order by Position asc");
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