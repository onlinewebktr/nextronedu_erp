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

namespace school_web.Dvlpr_Prof
{
    public partial class New_payroll : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
        protected void btn_login_Click(object sender, EventArgs e)
        {
            ViewState["Admin"] = "edunext2021";
            string query = "Select  * from user_details where user_id='" + ViewState["Admin"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = mycode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {

            }
            else
            {
                FormsAuthentication.SetAuthCookie(dtTemp.Rows[0]["user_id"].ToString(), false);
                Session["UserType"] = dtTemp.Rows[0]["User_Type"].ToString();
                Session["UserName"] = dtTemp.Rows[0]["name"].ToString();
                Session["Userid"] = dtTemp.Rows[0]["user_id"].ToString();
                Session["IsHR"] = "1";
                Session["UserProfileImage"] = dtTemp.Rows[0]["ProfilePhoto"].ToString();
                string url = "../home";
                Response.Redirect(url, false);

            }
        }
    }
}