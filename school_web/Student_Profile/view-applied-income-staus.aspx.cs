using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class view_applied_income_staus : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    ViewState["sesssionid"] = My.get_session_id();
                    ViewState["regid"] = Session["User"].ToString();
                    Bind_data();
                }
                else
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
            }

        }
        private void Bind_data()
        {
            DataTable dt = mycode.FillData(" Select aft.*,format(aft.Apply_date_time, 'dd/MM/yyyy hh:mm:ss tt') Apply_date_time1 ,ar.admissionserialnumber,ar.studentname from Apply_For_TC aft join admission_registor ar on aft.Admission_no=ar.admissionserialnumber and aft.Session_id=ar.Session_Id where aft.Session_id=" + ViewState["sesssionid"].ToString() + " and aft.Admission_no='" + ViewState["regid"].ToString() + "' and aft.Apply_For='Income Certificate'");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry, You have not apply for income certificate ", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }
        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (((Label)e.Item.FindControl("lbl_Reply_datetime")).Text == "")
            {
                ((Label)e.Item.FindControl("lbl_replydate_time_show")).Text = "N/A";
            }
            else
            {
                DateTime startTime = DateTime.ParseExact(((Label)e.Item.FindControl("lbl_Reply_datetime")).Text, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                ((Label)e.Item.FindControl("lbl_replydate_time_show")).Text = startTime.ToString("dd/MM/yyyy hh:mm:ss tt");
            }
            HtmlAnchor a1 = e.Item.FindControl("a1") as HtmlAnchor;

            if (((Label)e.Item.FindControl("lbl_Attachment")).Text == "")
            {
                a1.Visible = false;
            }
            else
            {
                a1.Visible = true;
            }
        }
    }
}