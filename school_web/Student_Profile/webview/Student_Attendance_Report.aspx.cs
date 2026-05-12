using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Student_Profile.webview
{
    public partial class Student_Attendance_Report : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["sesssionid"] = My.get_session_id();
            if (Request.QueryString["regid"] != null)
            {
                ViewState["regid"] = Request.QueryString["regid"].ToString();
                try
                {
                    if (!IsPostBack)
                    {
                        txt_date.Text = mycode.date();
                        txt_enddate.Text = mycode.date();

                        Bind_data();
                    }
                }
                catch
                {

                }
            }
        }

       

        private void Bind_data()
        {
            if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
            {
                lbl_month_year.Text = "Start Date -" + txt_date.Text + "-" + "End Date -" + txt_enddate.Text;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@studentid", ViewState["regid"].ToString());
                cmd.Parameters.AddWithValue("@cmdstatus", "13");
                cmd.Parameters.AddWithValue("@startdate", mycode.ConvertStringToiDate(txt_date.Text));
                cmd.Parameters.AddWithValue("@enddate", mycode.ConvertStringToiDate(txt_enddate.Text));
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                cmd.CommandText = "sp_VC_class_report";
                DataTable dt = UsesCode.Getdata_sp(cmd);
                if (Convert.ToString(dt.Rows.Count) == "0")
                {
                    lbl_total.Text = "0";
                    Alert("Sorry there are no attendance report available");

                    GrdView.DataSource = null;
                    GrdView.DataBind();

                   

                }
                else
                {
                    lbl_month_year.Text = "Start Date -" + txt_date.Text + "-" + "End Date -" + txt_enddate.Text;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    GrdView.DataSource = dt;
                    GrdView.DataBind();
                }
            }
            else
            {
                Alert("Please select valid date ");
            }
        }

        private void Alert(string msg)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false); 
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Bind_data();
        }
    }
}