using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class my_leave : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        txt_from_date.Text = "01/" + mycode.monthYear();
                        txt_to_date.Text = mycode.date();
                        get_all_leave_and_datewise();
                        ViewState["flag"] = "0";
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
            catch (Exception ex)
            {
            }
        }

        private void get_all_leave_and_datewise()
        {
            get_leave_info("select * from Student_Leave_Apply where Admission_no='" + ViewState["regid"].ToString() + "' and Apply_Idate>=" + My.DateConvertToIdate(txt_from_date.Text) + " and Apply_Idate<=" + My.DateConvertToIdate(txt_to_date.Text) + " order by id desc");
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


        private void get_leave_info(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_total_leave.Text = dt.Rows.Count.ToString();
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                lbl_total_leave.Text = dt.Rows.Count.ToString();
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        int pending = 0; int approved = 0; int rejected = 0;
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_status = (Label)e.Item.FindControl("lbl_status");

                if (lbl_status.Text == "Pending")
                {
                    pending++;
                }
                if (lbl_status.Text == "Approved")
                {
                    approved++;
                }
                if (lbl_status.Text == "Rejected")
                {
                    rejected++;
                }
            }
            lbltotal_pending.Text = pending.ToString();
            lbltotal_approved.Text = approved.ToString();
            lbl_rejected_leave.Text = rejected.ToString();
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Status.SelectedItem.Text == "ALL")
                {
                    get_all_leave_and_datewise();
                    ViewState["flag"] = "0";
                }
                else
                {
                    get_leave_status_and_datewise();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void get_leave_status_and_datewise()
        {
            get_leave_info("select * from Student_Leave_Apply where Admission_no='" + ViewState["regid"].ToString() + "' and Status='" + ddl_Status.SelectedItem.Text + "' and Apply_Idate>=" + My.DateConvertToIdate(txt_from_date.Text) + " and Apply_Idate<=" + My.DateConvertToIdate(txt_to_date.Text) + " order by id desc");
        }
    }
}