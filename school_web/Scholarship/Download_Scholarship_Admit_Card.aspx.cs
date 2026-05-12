using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;

namespace school_web.Scholarship
{
    public partial class Download_Scholarship_Admit_Card : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbl_session.Text = My.get_reg_apply_session();

                fetch_company_name();

            }
        }

        private void fetch_company_name()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {

                school_logo.Src = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        string scrpt;
        private void alert(string msg)
        {
            lblmessage.Text = msg;
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_apply_id.Text == "")
                {
                    alert("Please enter mobile no.");
                    txt_apply_id.Focus();
                }
                else if (txt_dob.Text == "")
                {
                    alert("Please enter date of birth");
                    txt_dob.Focus();
                }
                else
                {
                   // string query = "Select oa.*,oa.Registration_id as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,ore.Exam_Centre_Id,ore.Test_id  from Scholarship_Admission oa join Scholarship_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  where oa.Registration_id='" + txt_apply_id.Text + "'    and oa.DOB='" + txt_dob.Text + "'  and Is_published=1   ";

                    string query = "Select oa.*,oa.Registration_id as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,ore.Exam_Centre_Id,ore.Test_id  from Scholarship_Admission oa join Scholarship_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  where oa.Student_mob_no='" + txt_apply_id.Text + "'    and oa.DOB='" + txt_dob.Text + "'  and Is_published=1   ";

                    SqlCommand cmd = new SqlCommand(query);//and oa.Payment_Verify='Verified'
                    DataTable dt = mycode.GetData(cmd);

                    if (dt.Rows.Count == 0)
                    {

                        alert("Sorry your admit card is not available");

                    }
                    else
                    {
                        string sessionid = dt.Rows[0]["Session_id"].ToString();
                        string Class_id = dt.Rows[0]["Class_id"].ToString();
                        string admissionnumber = dt.Rows[0]["admissionnumber"].ToString();
                        string Exam_Centre_Id = dt.Rows[0]["Exam_Centre_Id"].ToString();
                        string Test_id = dt.Rows[0]["Test_id"].ToString();

                        //string url = "Admin/slip/Print_admit_card_Online_Reg.aspx?session_Id=" + sessionid + "&classid=" + Class_id + "&admin=" + admissionnumber + "&type=out";
                       

                        Response.Redirect("../Admin/slip/Print_admit_card_Scholarship_Reg.aspx?session_Id=" + sessionid + "&Scholorshipid=" + Test_id + "&classid=" + Class_id + "&Centreid=" + Exam_Centre_Id + "&admin=" + admissionnumber + "&type=out", false);

                        //Response.Redirect(url, false);


                    }
                }


            }
            catch
            {

            }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Download_Scholarship_Admit_Card.aspx", false);

        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Scholarship_Apply.aspx", false);
        }
    }
}