using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class print_bus_pass : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["session_Id"] != null  && Request.QueryString["classid"] != null && Request.QueryString["section"] != null && Request.QueryString["admno"] != null && Request.QueryString["checked"] != null)
                {
                    hd_session_id.Value = Request.QueryString["session_Id"].ToString();
                    hd_class_id.Value = Request.QueryString["classid"].ToString();
                    hd_section.Value = Request.QueryString["section"].ToString();
                    hd_admission_no.Value = Request.QueryString["admno"].ToString();
                    hd_chcked.Value = Request.QueryString["checked"].ToString();
                    hd_session_name.Value = mycode.get_session(hd_session_id.Value);
                   


                    try
                    {
                        if (Request.QueryString["checked"] != null)
                        {
                            hd_chcked.Value = Request.QueryString["checked"].ToString();
                            if (hd_chcked.Value == "1")
                            {
                                hd_admission_no.Value = hd_admission_no.Value.Remove(hd_admission_no.Value.Length - 1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                }
            }
        }

        protected void lnk_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/print-bus-pass.aspx", false);
        }
    }
}