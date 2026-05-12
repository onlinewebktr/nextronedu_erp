using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.worldline
{
    public partial class TrnPay_process : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltrPostData.Text = "<input type=\"hidden\" name=\"merchantRequest\" id=\"merchantRequest\" value=\"" + Session["Message"] + "\"     />		<input type=\"hidden\" name=\"MID\" id=\"MID\" value=" + Session["MID"] + " /> ";
            }
        }
    }
}