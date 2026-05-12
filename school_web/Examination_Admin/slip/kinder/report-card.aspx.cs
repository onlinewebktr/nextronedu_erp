using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin.slip.kinder
{
    public partial class report_card : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["clss_id"] != null && Request.QueryString["ssion_id"] != null && Request.QueryString["Branch_id"] != null && Request.QueryString["Term_id"] != null && Request.QueryString["admNo"] != null)
                {
                    hd_adm_no.Value = Request.QueryString["admNo"];
                    hd_section.Value = Request.QueryString["Section"];
                    hd_class_id.Value = Request.QueryString["clss_id"];
                    hd_session_id.Value = Request.QueryString["ssion_id"];
                    hd_branch_id.Value = Request.QueryString["Branch_id"];
                    hd_term_id.Value = Request.QueryString["Term_id"];

                    try
                    {
                        if (Request.QueryString["RequestFrom"].ToString() == "1")
                        {
                            printBtns.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../student-result.aspx", false);
        }
    }
}