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
    public partial class print_demand_bill : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["session_Id"] != null && Request.QueryString["branch_id"] != null && Request.QueryString["classid"] != null && Request.QueryString["section"] != null && Request.QueryString["admno"] != null && Request.QueryString["checked"] != null && Request.QueryString["month"] != null && Request.QueryString["paydate"] != null && Request.QueryString["stdType"] != null)
                {
                    hd_session_id.Value = Request.QueryString["session_Id"].ToString();
                    hd_branch_id.Value = Request.QueryString["branch_id"].ToString();
                    hd_class_id.Value = Request.QueryString["classid"].ToString();
                    hd_section.Value = Request.QueryString["section"].ToString();
                    hd_admission_no.Value = Request.QueryString["admno"].ToString();
                    hd_student_type.Value = Request.QueryString["stdType"].ToString();
                    hd_chcked.Value = Request.QueryString["checked"].ToString();
                    hd_month.Value = Request.QueryString["month"].ToString();
                    hd_paydate.Value = Request.QueryString["paydate"].ToString();
                    hd_with.Value = Request.QueryString["billType"].ToString();
                    hd_session_name.Value = mycode.get_session(hd_session_id.Value);
                    hd_std_school_copy.Value = "0";

                     
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

                    try
                    {
                        moreInfo.Visible = false;
                        string firm_id = My.get_single_column_data("select firm_id as Column_Name from Firm_Details");

                        if (firm_id == "BD-001")
                        {
                            hd_std_school_copy.Value = "1";
                        }
                        if (firm_id == "DEEP-1" || firm_id == "DEEP-2")
                        {
                            moreInfo.Visible = true;
                        }
                        if (firm_id == "KIDS-01")
                        {
                            reqHin.Visible = true;
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
            Response.Redirect("~/Admin/create-demand-bill.aspx", false);
        }
    }
}