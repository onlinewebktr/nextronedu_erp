using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class Admission_Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["session_Id"] != null && Request.QueryString["admission_no"] != null)
                {
                    hd_admission_no.Value = Request.QueryString["admission_no"].ToString();
                    hd_session_id.Value = Request.QueryString["session_Id"].ToString();
                }
            }
        }

        protected void lnk_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../admission.aspx", false);
        }
    }
}