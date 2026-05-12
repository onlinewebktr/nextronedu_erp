using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class Home : System.Web.UI.Page
    {
        My mycod = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["Admin"] = "tcsc2025";
            //Session["firm"] = "TCSM-002";
            if (!IsPostBack)
            {
               
                Response.Redirect("exam-setup-home.aspx", false);

            }
        }
    }
}