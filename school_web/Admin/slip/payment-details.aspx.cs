using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class payment_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["clssid"] != null && Request.QueryString["ssionid"] != null && Request.QueryString["ssionname"] != null && Request.QueryString["admNo"] != null && Request.QueryString["section"] != null)
                {
                    hd_session_id.Value = Request.QueryString["ssionid"];
                    hd_session_name.Value = Request.QueryString["ssionname"];
                    hd_class_id.Value = Request.QueryString["clssid"];
                    hd_section.Value = Request.QueryString["section"];
                    hd_adm_no.Value = Request.QueryString["admNo"];
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../print-payment-details.aspx", false);
        }
    }
}