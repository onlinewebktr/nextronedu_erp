using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Payroll_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                        try
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowProgress();", true); 
                            ViewState["Admin"] = Session["Admin"].ToString();
                            get_uesr_type(); 
                        }
                        catch(Exception ex)
                        {
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        My mycode = new My();
        private void get_uesr_type()
        {
            
            string query = "Select  * from HR_UserProfile where EmployeeCode='" + ViewState["Admin"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = mycode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {

}
            else
            {
                FormsAuthentication.SetAuthCookie(dtTemp.Rows[0]["UserId"].ToString(), false);
                Session["UserType"] = dtTemp.Rows[0]["UserType"].ToString();
                Session["UserName"] = dtTemp.Rows[0]["Name"].ToString();
                Session["Userid"] = dtTemp.Rows[0]["UserId"].ToString();
                Session["IsHR"] = dtTemp.Rows[0]["IsHr"].ToString();
                Session["UserProfileImage"] = dtTemp.Rows[0]["ProfileImage"].ToString();
                string url = "../home";
                 Response.Redirect(url,false);

            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);

            //    Response.Redirect("home.aspx", false);
            }

        }

          
    }
}