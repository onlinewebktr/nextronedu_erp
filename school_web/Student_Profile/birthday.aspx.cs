using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class birthday : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        check_is_birthday();
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

        private void check_is_birthday()
        {
            DataTable dt = mycode.FillData("select Id from admission_registor where dob like '%" + day_month() + "%' and admissionserialnumber='" + Session["User"].ToString() + "' and Session_id='" + My.get_session_id() + "'");
            if (dt.Rows.Count != 0)
            {
                is_birthday_yes.Visible = true;
                is_birthday_no.Visible = false;
            }
            else
            {
                is_birthday_yes.Visible = false;
                is_birthday_no.Visible = true;
            }
        }

        private string day_month()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM");
        }
    }
}