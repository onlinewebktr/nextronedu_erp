using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.id_card.kdes
{
    public partial class bulk_emp_id : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Type"] != null)
                    {
                        if (Request.QueryString["Type"].ToString() == "CHECK")
                        {
                            hd_userType.Value = Request.QueryString["UserType"];
                            hd_branch.Value = Request.QueryString["Branch_id"];
                            hd_type.Value = Request.QueryString["Type"];
                            string empid = Request.QueryString["empid"].ToString();
                            hd_emp_id.Value = empid.Remove(empid.Length - 1);
                        }
                        else
                        {
                            if (Request.QueryString["UserType"] != null || Request.QueryString["empid"] != null || Request.QueryString["Branch_id"] != null || Request.QueryString["Type"] != null)
                            {

                                hd_userType.Value = Request.QueryString["UserType"];
                                hd_emp_id.Value = Request.QueryString["empid"];
                                hd_branch.Value = Request.QueryString["Branch_id"];
                                hd_type.Value = Request.QueryString["Type"];
                            }
                            else
                            {
                                Response.Redirect("../Id-card-employee-print.aspx", false);
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("../Id-card-employee-print.aspx", false);
                    }
                }

                catch (Exception exe)
                {
                }
            }

        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../Id-card-employee-print.aspx", false);
        }
    }
}