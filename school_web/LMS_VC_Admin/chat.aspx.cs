
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class chat : System.Web.UI.Page
    {
        UsesCode mycod = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["reqid"] != null)
                    {
                        hd_request_id.Value = Request.QueryString["reqid"];
                        hd_studentid .Value= Request.QueryString["studentid"];
                        fetch_req_detsils();
                    }
                    else
                    {
                        Response.Redirect("Dashboard.aspx");
                    }
                }
            }
            catch (Exception exe)
            {
            }
        }

        private void fetch_req_detsils()
        {
            string sql = @"select t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.admissionserialnumber,t2.Section,t2.rollnumber,t2.class  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Request_id='" + hd_request_id.Value + "' and t1.User_id='" + hd_studentid.Value + "'";
            DataTable dt = mycod.FillTable(sql);
            if (dt.Rows.Count.ToString() != "0")
            {
                lbl_req_id.Text = "Request Id : " + hd_request_id.Value;
                lbl_user_name.Text = "User Name : " + dt.Rows[0]["Full_name"].ToString();
                lbl_user_mobile_no.Text = "Mobile No. : " + dt.Rows[0]["Mobile_no"].ToString();


                lbl_admission_no.Text = "Admission No. :" + dt.Rows[0]["admissionserialnumber"].ToString() + " Class :" + dt.Rows[0]["class"].ToString() + " Section :" + dt.Rows[0]["Section"].ToString() + " Roll No. :" + dt.Rows[0]["rollnumber"].ToString();

                if (dt.Rows[0]["Is_related_with_order"].ToString() == "1")
                {
                    lbl_order_id.Text = "Order Id : " + dt.Rows[0]["Order_id"].ToString();
                }
                else
                {
                    lbl_order_id.Visible = false;
                }
                string comp_or_feed = "";
                if (dt.Rows[0]["Type"].ToString() == "1")
                {
                    comp_or_feed = "Complain Date : ";
                    ViewState["flag"] = "Complain List";
                }
                else
                {
                    comp_or_feed = "Feedback Date : ";
                    ViewState["flag"] = "Feedback List";
                }
                lbl_complain_date.Text = comp_or_feed + dt.Rows[0]["Created_date"].ToString() + " - " + dt.Rows[0]["Created_time"].ToString();
                lnk_back.Text = ViewState["flag"].ToString();
                //if (dt.Rows[0]["Status"].ToString() == "1")
                //{ 
                //    pnl_msg_sends.Attributes.Add("class", "hidden");
                //    pnl_closes.Attributes.Add("class", "show"); 
                //}
                //else
                //{
                //    pnl_closes.Attributes.Add("class", "hidden");
                //    pnl_msg_sends.Attributes.Add("class", "show");  
                //}
                Bind_attchmnet();
            }
            else
            {
                lbl_req_id.Text = "";
                lbl_user_name.Text = "";
                lbl_user_mobile_no.Text = "";
                lbl_order_id.Text = "";
                lbl_complain_date.Text = "";
            }
        }

        private void Bind_attchmnet()
        {
            string sql = @"select * from Complain_feedback_Attachment  where Request_id='" + hd_request_id.Value + "' and Attachment!='' ";
            DataTable dt = mycod.FillTable(sql);
            if (dt.Rows.Count == 0)
            {
                Grdattache.DataSource = null;

                Grdattache.DataBind();
            }
            else
            {
                Grdattache.DataSource = dt;

                Grdattache.DataBind();
            }
        }

        protected void lnk_back_Click(object sender, EventArgs e)
        {
            if (ViewState["flag"].ToString() == "Complain List")
            {
                Response.Redirect("complain-list.aspx", false);
            }
            else
            {
                Response.Redirect("feedback-list.aspx", false);
            }
        }
    }
}