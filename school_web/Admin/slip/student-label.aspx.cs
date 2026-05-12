using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class student_label : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["type"] != null)
                {
                    Session["LabelFrom"] = Request.QueryString["printfrom"]; 
                    if (Request.QueryString["type"].ToString() == "1")
                    { 
                        if (Request.QueryString["session_id"] != null && Request.QueryString["class_id"] != null && Request.QueryString["adm_no"] != null && Request.QueryString["section"] != null && Request.QueryString["prinTtype"] != null)
                        {
                            hd_type.Value = "1";
                            hd_session_id.Value = Request.QueryString["session_id"];
                            hd_class_id.Value = Request.QueryString["class_id"];
                            hd_admission_no.Value = Request.QueryString["adm_no"];
                            hd_section.Value = Request.QueryString["section"];
                            hd_print_type.Value = Request.QueryString["prinTtype"];
                        }
                        else
                        {
                            Response.Redirect("Dashboard.aspx", false);
                        }
                    }
                    else
                    {
                        hd_type.Value = "0";
                        hd_session_id.Value = Session["SessionS"].ToString();
                        string stocksids = Request.QueryString["adm_no"].ToString();
                        string myStockSiD = stocksids.Remove(stocksids.Length - 1);
                        hd_admission_no.Value = myStockSiD;
                    }
                }





            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["LabelFrom"].ToString() == "0") { Response.Redirect("../print-label.aspx", false); }
            else { Response.Redirect("../student-list.aspx", false); }

        }
    }
}