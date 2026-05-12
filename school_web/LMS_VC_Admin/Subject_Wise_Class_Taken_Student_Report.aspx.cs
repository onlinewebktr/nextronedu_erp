using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web.LMS_VC_Admin
{
    public partial class Subject_Wise_Class_Taken_Student_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["System_id"] != null)
                {
                    ViewState["System_id"] = Request.QueryString["System_id"].ToString();
                    bind_data();
                }
                else
                {

                }
            }
        }

        private void bind_data()
        {
            string query = " Select distinct  zms.User_id,zms.Zoom_Meeting_id,zms.System_id,zvcs.Date,zvcs.Start_Time,zvcs.Meeting_start_at,zvcs.CreatedOn,zvcs.End_Time,cm.Course_Name as  CategoryName,zvcs.section,ar.studentname,t4.Subject_name as CourseName,(Select top 1   name from user_details where user_id=zvcs.Teacher_Id ) as teachername from Zoom_Meeting_Joining_Status zms  join Zoom_Virtual_class_schedule zvcs on zms.Zoom_Meeting_id=zvcs.Zoom_Meeting_id and zms.System_id=zvcs.System_id join Add_course_table cm on zvcs.Class=cm.course_id join Subject_Master t4 on  zvcs.Class=t4.course_id and zvcs.Subject=t4.Subject_id  join  admission_registor ar on zms.User_id=ar.admissionserialnumber where zms.Type='Student' and   zvcs.System_id='" + ViewState["System_id"].ToString() + "' ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {

                lbl_total.Text = "0";
                Alert("Data Not Available");
                lbl_month_year.Text = "";
                RpDetailsStudent.DataSource = null;
                RpDetailsStudent.DataBind();

                pnl_view.Visible = false;

            }
            else
            {
                lbl_total.Text = dtTemp.Rows.Count.ToString();
                pnl_view.Visible = true;
                lbl_month_year.Text = "Class Name:-" + dtTemp.Rows[0]["CategoryName"].ToString() + "Subject:-" + dtTemp.Rows[0]["CourseName"].ToString() + " Section:-" + dtTemp.Rows[0]["section"].ToString() + " Teacher Name:-" + dtTemp.Rows[0]["teachername"].ToString();

                RpDetailsStudent.DataSource = dtTemp;
                RpDetailsStudent.DataBind();
            }

        }

        private void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void RpDetailsStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                string userid = ((Label)e.Item.FindControl("lbl_userid")).Text;
                ((Label)e.Item.FindControl("lbl_metingstartinme")).Text = get_actualtime(userid, ViewState["System_id"].ToString());

            }
        }

        private string get_actualtime(string userid, string sytemid)
        {
            SqlCommand cmd = new SqlCommand("  Select  top 1 format( Date, 'hh:mm:ss tt') from Zoom_Meeting_Joining_Status where User_id='" + userid + "' and System_id='" + sytemid + "' order by id asc");
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
                return dtTemp.Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }

        protected void lnk_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Subject_Wise_Class_Taken_Report.aspx", false);
        }
    }
}