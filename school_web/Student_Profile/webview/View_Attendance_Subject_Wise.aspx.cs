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
    public partial class View_Attendance_Subject_Wise : System.Web.UI.Page
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
                        find_student_details();
                        Bind_data();
                    }
                }
                catch
                {

                }
            }
        }

        private void find_student_details()
        {
              string query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV'    and Transfer_Status in ('New','NT') order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Alert("Somthing is wrong");

                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ViewState["Session_id"] = dr["Session_id"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();





                }

                mycode.bind_all_ddl_with_id(ddl_subject, "Select   sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join  Subject_Mapping_New smn on sm.course_id=smn.Class_id and sm.Subject_id=smn.Sub_id   where smn.Admission_no='" + ViewState["regid"].ToString() + "' and smn.Class_id='" + ViewState["class_id"].ToString() + "' and smn.Session_id='" + ViewState["Session_id"].ToString() + "' and smn.Section='" + ViewState["Section"].ToString() + "' order by sm.Subject_position");

                ddl_subject.SelectedValue = My.get_top_one_subject(ViewState["class_id"].ToString(), ViewState["Section"].ToString(), ViewState["regid"].ToString());
            
            }
        }
        private void Alert(string msg)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        private void Bind_data()
        {

            if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
            {

                string query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section,(select top 1 CategoryName from ClassMaster where CategoryID=ar.Class_id) as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status from admission_registor ar join Student_Attendance_saved sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no    where ar.Class_id='" + ViewState["class_id"].ToString() + "' and ar.section='" + ViewState["Section"].ToString() + "' and ar.Session_id='" + ViewState["Session_id"].ToString() + "'   and sas.Subject_id='" + ddl_subject.SelectedValue + "' and sas.Attendance_IDate>=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) + " and sas.Attendance_IDate<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Status=1 and ar.admissionserialnumber='" + ViewState["regid"].ToString() + "'  order by sas.Attendance_IDate";
                SqlCommand cmd = new SqlCommand(query);
                DataTable dt = mycode.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                  
                    GrdView.DataSource = null;
                    GrdView.DataBind();
                    grid111.Visible = false;
                  
                }
                else
                {
                   
                    
                    GrdView.DataSource = dt;
                    GrdView.DataBind();
                    grid111.Visible = true;

                    



                }



            }
            else
            {
                Alert("Please select valid date ");
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Bind_data();
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (txt_date.Text == "")
            {
                Alert("Please select start date ");
            }
            else if (txt_enddate.Text == "")
            {
                Alert("Please select end date ");
            }
            else if (ddl_subject.SelectedItem.Text == "")
            {
                Alert("Please select subject date ");
            }
            else
            {
                Bind_data();
            }
        }
    }
}