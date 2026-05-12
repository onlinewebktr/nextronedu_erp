using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class coupon_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sesssionid"] = My.get_session_id();
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    Session["regid"] = ViewState["regid"].ToString();
                    try
                    {
                        Bind_data(ViewState["regid"].ToString());
                        My.exeSql("update Coupon_applied_list set View_status='1' where Session_id='" + ViewState["sesssionid"].ToString() + "' and Admission_no='" + ViewState["regid"].ToString() + "' and Status='Approved'");
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void Bind_data(string admission_no)
        {
            string couponAval = "0";
            pnl_new_coupon.Visible = false; pnl_prev_coupont_list.Visible = false;
            DataTable dt = My.dataTable("select top 1 * from Coupon_applied_list where Session_id='" + ViewState["sesssionid"].ToString() + "' and Admission_no='" + admission_no + "' and Status='Approved' and View_status is null order by id desc");
            if (dt.Rows.Count > 0)
            {
                couponAval = "1";
                pnl_new_coupon.Visible = true;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }


            DataTable dtx = My.dataTable("select * from Coupon_applied_list where Session_id='" + ViewState["sesssionid"].ToString() + "' and Admission_no='" + admission_no + "' and Status='Approved' and View_status='1' order by id desc");
            if (dtx.Rows.Count > 0)
            { 
                couponAval = "1";
                pnl_prev_coupont_list.Visible = true;
                rp_prev_coupon_list.DataSource = dtx;
                rp_prev_coupon_list.DataBind();
            }


            if (couponAval == "0")
            {
                pnl_no_data.Visible = true;
            }
        }
    }
}