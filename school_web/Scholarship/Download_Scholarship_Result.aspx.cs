using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Scholarship
{
    public partial class Download_Scholarship_Result : System.Web.UI.Page
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
                    

                    string query = "Select oa.*,oa.Registration_id as admissionnumber  from Scholarship_Admission oa join Scholarship_Exam_Result ore on oa.Registration_id=ore.Admission_no and oa.Test_id=ore.Test_id   where oa.Student_mob_no='" + txt_apply_id.Text + "'    and oa.DOB='" + txt_dob.Text + "'  and ore.Is_published=1   ";

                    SqlCommand cmd = new SqlCommand(query);//and oa.Payment_Verify='Verified'
                    DataTable dt = mycode.GetData(cmd);

                    if (dt.Rows.Count == 0)
                    {

                        alert("Sorry your admit card is not available");

                    }
                    else
                    {
               
                        string admissionnumber = dt.Rows[0]["admissionnumber"].ToString();
                   
                        string Test_id = dt.Rows[0]["Test_id"].ToString();

                        //string url = "Admin/slip/Print_admit_card_Online_Reg.aspx?session_Id=" + sessionid + "&classid=" + Class_id + "&admin=" + admissionnumber + "&type=out";


                        Response.Redirect("../Admin/slip/Print_Result_Scholarship_Reg.aspx?Scholorshipid=" + Test_id + "&admin=" + admissionnumber + "&type=out", false);

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
          
            Response.Redirect("Download_Scholarship_Result.aspx", false);

        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Scholarship_Apply.aspx", false);
        }
    }
}