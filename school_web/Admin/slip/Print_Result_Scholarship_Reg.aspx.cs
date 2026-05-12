using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class Print_Result_Scholarship_Reg : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Scholorshipid"] != null && Request.QueryString["admin"] != null)
                {
                    ViewState["branch_id"] = "1";
                    hd_admission_no.Value = Request.QueryString["admin"].ToString();
                    hd_Scholarshipid.Value = Request.QueryString["Scholorshipid"].ToString();
                    hd_branch_id.Value = "1";
                    try
                    {
                        if (Request.QueryString["type"].ToString() == "in_s")
                        {
                            hd_from.Value = "in_s";
                            A1.HRef = "../Scholarship_Result_Print.aspx";
                        }
                        else
                        {
                            A1.HRef = "../../Scholarship/Download_Scholarship_Result.aspx";
                            hd_from.Value = Request.QueryString["type"].ToString();
                        }
                    }
                    catch
                    {
                        hd_from.Value = "in_s";
                        A1.HRef = "../Scholarship_Result_Print.aspx";
                    }
                }
                else
                {
                }
            }
        }
    }
}