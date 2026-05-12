using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Feedback
{
    public partial class Feedback_one : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["admissionserialnumber"] = "";
                ViewState["sessionid"] = My.get_session_id();
                ViewState["session_name"] = My.get_session();
                fetch_data();
                try
                {
                    if (Session["msg2"].ToString() == "")
                    {

                    }
                    else
                    {
                        alert(Session["msg2"].ToString());

                    }
                    Session["msg2"] = "";
                }
                catch
                {

                }
            }

        }
        My mycode = new My();
        private void fetch_data()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {


            }
            else
            {
                schoollogo.Src = dt.Rows[0]["logo"].ToString();
                lbl_schoolname.Text = dt.Rows[0]["firm_name"].ToString();
                copyright1.Text = dt.Rows[0]["Footer_Copy_Right"].ToString();

            }
        }
        string scrpt;
        private void alert(string msg)
        {
            msg1.Text = msg;
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        #region find student
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            pnl_ownstudentdata.Visible = false;
            txt_own_name.Text = "";
            txt_mobile_own_mobile.Text = "";
            ViewState["admissionserialnumber"] = "";
            if (txt_admission_no.Text == "")
            {
                alert("Please enter admission no.");

            }
            else
            {
                string query = "Select * from admission_registor  where Session_id='" + ViewState["sessionid"].ToString() + "' and Transfer_Status in ('NT','New') and admissionserialnumber='" + txt_admission_no.Text.Trim() + "'";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    alert("Enter Admission no. is invalid");
                }
                else
                {
                    pnl_ownstudentdata.Visible = true;
                    txt_own_name.Text = dt.Rows[0]["studentname"].ToString();
                    txt_mobile_own_mobile.Text = dt.Rows[0]["father_mob"].ToString();
                    ViewState["admissionserialnumber"] = dt.Rows[0]["admissionserialnumber"].ToString();
                }
            }

        }
        #endregion

        protected void rd_student_CheckedChanged(object sender, EventArgs e)
        {
            pnl_login_other.Visible = false;
            pbl_own_student.Visible = true;
            txt_admission_no.Text = "";
            txt_own_name.Text = "";
            txt_mobile_own_mobile.Text = "";
            
            txt_othername.Text = "";
            txt_mobile_no.Text = "";
          
        }



        protected void rd_otherstudent_CheckedChanged(object sender, EventArgs e)
        {
            pnl_login_other.Visible = true;
            pbl_own_student.Visible = false;
            txt_admission_no.Text = "";
            txt_own_name.Text = "";
            txt_mobile_own_mobile.Text = "";
           
        }

        #region own student
        protected void btn_own_studentfeedback_Click(object sender, EventArgs e)
        {
            Session["Name"] = txt_own_name.Text;
            Session["Mobile_no"] = txt_mobile_own_mobile.Text;

            string isparents = "0";
            if (rd_parents_own.Checked == true)
            {
                isparents = "1";
            }
            string requestid = My.create_random_no_otp();

            string url = "Feedback_form.aspx?requestid=" + requestid + "&isparents=" + isparents + "&studentid=" + txt_admission_no.Text.Trim();
            Response.Redirect(url, false);
        }
        #endregion

        protected void btn_other_studentfeedback_Click(object sender, EventArgs e)
        {
            Session["Name"] = txt_othername.Text;
            Session["Mobile_no"] = txt_mobile_no.Text;
            string isparents = "0";
            if (rd_parentsother.Checked == true)
            {
                isparents = "1";
            }
            string requestid = My.create_random_no_otp();

            string url = "Feedback_form.aspx?requestid=" + requestid + "&isparents=" + isparents + "&studentid=No";
            Response.Redirect(url,false);
        }
    }
}