using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin.slip
{
    public partial class bulk_report_card_termI : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Section"] != null && Request.QueryString["clss_id"] != null && Request.QueryString["ssion_id"] != null && Request.QueryString["Branch_id"] != null && Request.QueryString["Term_id"] != null)
                {
                    ViewState["Section"] = Request.QueryString["Section"];
                    ViewState["classid"] = Request.QueryString["clss_id"];
                    ViewState["sessionid"] = Request.QueryString["ssion_id"];
                    ViewState["Branch_id"] = Request.QueryString["Branch_id"];
                    ViewState["Term_id"] = Request.QueryString["Term_id"];


                    hd_section.Value = Request.QueryString["Section"];
                    hd_session_id.Value = Request.QueryString["ssion_id"];
                    hd_class_id.Value = Request.QueryString["clss_id"];
                    hd_branch_id.Value = Request.QueryString["Branch_id"];
                    hd_term_id.Value = Request.QueryString["Term_id"];
                     
                }

            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../student-result.aspx", false);
        }


    }
}