using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class print_result : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["session_Id"] != null && Request.QueryString["classid"] != null && Request.QueryString["admin"] != null && Request.QueryString["type"] != null)
                {
                    ViewState["branch_id"] = "1";
                    hd_session_id.Value = Request.QueryString["session_Id"].ToString();
                    hd_class_id.Value = Request.QueryString["classid"].ToString();
                    hd_admission_no.Value = Request.QueryString["admin"].ToString();
                    string types = Request.QueryString["type"].ToString();
                    try
                    {
                        if (types == "InOut")
                        {
                            hd_from.Value = "out";
                            A1.HRef = "../Online_Reg_View_Result.aspx";
                        }
                        else if (types == "In")
                        {
                            hd_from.Value = "InBulk";
                            A1.HRef = "../Online_Reg_View_Result.aspx";
                        }
                        else
                        {
                            hd_from.Value = "out";
                            A1.HRef = "../../Download_Result_Card.aspx";
                            hd_from.Value = Request.QueryString["type"].ToString();
                        }
                    }
                    catch
                    {
                       
                    }
                }
                else
                {
                }
            }
        } 
    }
}