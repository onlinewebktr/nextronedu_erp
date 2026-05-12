using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class student_attendance_report : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["regid"] = Session["User"].ToString();
                        ViewState["sesssionid"] = My.get_session_id();
                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();
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
            catch (Exception ex)
            {
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

        private void Bind_data()
        {
            if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_from_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_to_date.Text)))
            {
                lbl_month_year.Text = "Start Date -" + txt_from_date.Text + "-" + "End Date -" + txt_to_date.Text;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@studentid", ViewState["regid"].ToString());
                cmd.Parameters.AddWithValue("@cmdstatus", "13");
                cmd.Parameters.AddWithValue("@startdate", mycode.ConvertStringToiDate(txt_from_date.Text));
                cmd.Parameters.AddWithValue("@enddate", mycode.ConvertStringToiDate(txt_to_date.Text));
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                cmd.CommandText = "sp_VC_class_report";
                DataTable dt = UsesCode.Getdata_sp(cmd);
                if (Convert.ToString(dt.Rows.Count) == "0")
                {
                    lbl_total.Text = "0";
                    Alertme("Sorry there are no attendance report available.", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    lbl_month_year.Text = "Start Date -" + txt_from_date.Text + "-" + "End Date -" + txt_to_date.Text;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
            }
            else
            {
                Alertme("Please select valid date.", "warning");
            }
        }



        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Bind_data();
        }
    }
}