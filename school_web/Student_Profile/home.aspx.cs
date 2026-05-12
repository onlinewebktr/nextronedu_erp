using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class home : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //Session["User"] = "SMI/23-24/1234/340";
                DateTime dt = mycode.datetime();
                lbl_today_activity_desk.Text = dt.ToString("dd-MMM");
                lbl_today_activity_mob.Text = dt.ToString("dd-MMM");
            }
        }
    }
}