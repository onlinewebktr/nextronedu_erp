using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class festival_events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    hd_session_id.Value = My.get_session_id();
                    hd_adm_no.Value = Request.QueryString["regid"].ToString();
                    hd_class_id.Value = My.get_single_column_data("select top 1 Class_id as Column_Name from admission_registor where Session_id='" + hd_session_id.Value + "' and admissionserialnumber='" + hd_adm_no.Value + "' and Status='1' order by id desc");
                }
            }
        }
    }
}