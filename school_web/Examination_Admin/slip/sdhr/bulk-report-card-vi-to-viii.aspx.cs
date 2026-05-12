using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin.slip.sdhr
{
    public partial class bulk_report_card_vi_to_viii : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["clss_id"] != null && Request.QueryString["ssion_id"] != null && Request.QueryString["Branch_id"] != null && Request.QueryString["Term_id"] != null)
                { 
                    ViewState["RequestFrom"] = Request.QueryString["RequestFrom"]; 
                    hd_admission_no.Value = "0";
                    hd_section.Value = "0";
                    if (Request.QueryString["admNo"] != null)
                    {
                        hd_admission_no.Value = Request.QueryString["admNo"];
                    }
                    if (Request.QueryString["Section"] != null)
                    {
                        hd_section.Value = Request.QueryString["Section"];
                    }
                    hd_session_id.Value = Request.QueryString["ssion_id"];
                    hd_class_id.Value = Request.QueryString["clss_id"];
                    hd_branch_id.Value = Request.QueryString["Branch_id"];
                    hd_term_id.Value = Request.QueryString["Term_id"];

                    if (ViewState["RequestFrom"] != null)
                    {
                        if (ViewState["RequestFrom"].ToString() == "1")
                        {
                            printBtns.Visible = false;
                        }
                    }
                }
            }
        }





        protected void btn_back_Click1(object sender, EventArgs e)
        {
            Response.Redirect("../../student-result.aspx", false);
        }
    }
}