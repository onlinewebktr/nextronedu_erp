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
    public partial class event_details : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    ViewState["Id"] = Request.QueryString["Id"].ToString();
                    try
                    { 
                        Bind_data();
                    }
                    catch(Exception ex)
                    { 
                    }
                } 
            }
        }

        private void Bind_data()
        {
            BindRepeater("select *, Format(convert(DateTime,Posted_Date,103), 'dd-MMM-yyyy') as Date1 from News_Events_Details where Id=" + ViewState["Id"].ToString() + "");
        }

        private void BindRepeater(string query)
        {
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count != 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
            else
            {
                Alert("Currently, there is not update at notice board. Please keep visiting for update");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
        }

        private void Alert(string msg)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        { 
            if (((Label)e.Item.FindControl("lbl_Attachments")).Text == "")
            {
                ((Panel)e.Item.FindControl("Panel1")).Visible = false; 
            }
            else
            {
                ((Panel)e.Item.FindControl("Panel1")).Visible = true; 
            }


            if (((Label)e.Item.FindControl("lbl_link")).Text == "")
            {
                ((Panel)e.Item.FindControl("pnl_link")).Visible = false;
            }
            else
            {
                ((Panel)e.Item.FindControl("pnl_link")).Visible = true;
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Events.aspx?regid=" + Session["regid"].ToString(), false);
        }
    }
}