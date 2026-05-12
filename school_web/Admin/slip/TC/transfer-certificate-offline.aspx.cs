using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip.TC
{
    public partial class transfer_certificate_offline : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["adm_no"] != null && Request.QueryString["clssid"] != null && Request.QueryString["sessnid"] != null && Request.QueryString["crtificateno"] != null)
                {
                    ViewState["admissionno"] = Request.QueryString["adm_no"];
                    ViewState["classid"] = Request.QueryString["clssid"];
                    ViewState["sessionid"] = Request.QueryString["sessnid"];
                    ViewState["crtificateno"] = Request.QueryString["crtificateno"];

                    Bind_crtificate_info();
                    Bind_schoolinfo();
                }
                Bind_schoolinfo();
            }
        }

        private void Bind_crtificate_info()
        {
            DataTable dt = mycode.FillData("select * from dbo.[Transfer_certificate] t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber and t1.Certificate_no='" + ViewState["crtificateno"] + "'  and t1.Session_id='" + ViewState["sessionid"] + "'   and t1.Class_id='" + ViewState["classid"] + "'   and t1.Admission_no='" + ViewState["admissionno"] + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_certificate_no.Text = dt.Rows[0]["Certificate_no"].ToString();
                lbl_admission_no.Text = dt.Rows[0]["Admission_no"].ToString();
                lbl_student_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_father_name.Text = dt.Rows[0]["fathername"].ToString();
                lbl_mother_name.Text = dt.Rows[0]["mothername"].ToString();
                lbl_religion.Text = dt.Rows[0]["religion"].ToString();
                lbl_nationality.Text = dt.Rows[0]["f_nationality"].ToString();

                lbl_belongs_to_sc_st.Text = dt.Rows[0]["Belongs_t_sc_st_obc"].ToString();
                lbl_date_of_admision.Text = dt.Rows[0]["Date_of_admission_and_class"].ToString();
                if (dt.Rows[0]["studentimagepath"].ToString() == "")
                {
                    Image2.Visible = false;
                }
                else
                {
                    Image2.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
                }

                lbl_class_in_last_studied.Text = dt.Rows[0]["class_in_last_studied"].ToString();
                lbl_school_board_exam_taken.Text = dt.Rows[0]["annual_exam_taken_with_result"].ToString();
                lbl_failed_in_same_class.Text = dt.Rows[0]["whether_failed_in_same_class"].ToString();
                lbl_compalsory_subject.Text = dt.Rows[0]["subject_studied"].ToString();
                lbl_optional_sub.Text = dt.Rows[0]["optional_subject"].ToString();
                lbl_qualified_for_promotion.Text = dt.Rows[0]["whether_qualified_for_promotion"].ToString();

                //=============
                lbl_paid_all_fees.Text = "YES";
                lbl_any_fee_concession_availed.Text = "No";
                //=============

                lbl_ttl_no_of_working.Text = dt.Rows[0]["ttl_working_days"].ToString();
                lbl_ttl_no_of_working_present.Text = dt.Rows[0]["present_days"].ToString();
                lbl_ncc_cadet.Text = dt.Rows[0]["ncc_cadet"].ToString();
                lbl_game_played_or_extra.Text = dt.Rows[0]["game_played_of_extra"].ToString();
                lbl_general_conduct.Text = dt.Rows[0]["general_conduct"].ToString();
                lbl_date_of_application.Text = dt.Rows[0]["date_of_application"].ToString();
                lbl_date_of_issue.Text = dt.Rows[0]["date_of_issue_certificate"].ToString();
                lbl_reason_for_leaving.Text = dt.Rows[0]["reason_for_leaving"].ToString();
                lbl_any_other_remark.Text = dt.Rows[0]["any_other_remarks"].ToString();


                lbl_board_roll_no.Text = dt.Rows[0]["Board_roll_no"].ToString();
                lbl_cbse_reg_no.Text = dt.Rows[0]["Cbse_reg_no"].ToString();

                try
                {
                    string dob = dt.Rows[0]["dob"].ToString();
                    string dday = dob.Substring(0, 2);
                    string dmonth = dob.Substring(3, 2);
                    string dyear = dob.Substring(6, 4);
                    string monthName = My.getMonthS_full_name(dmonth);

                    string inword_day = mycode.NumberToWords(Convert.ToInt32(dday));
                    string inword_year = mycode.NumberToWords(Convert.ToInt32(dyear));

                    lbl_dob.Text = dt.Rows[0]["dob"].ToString() + ", " + inword_day + " " + monthName + ", " + inword_year;
                }
                catch (Exception ex)
                {
                    lbl_dob.Text = dt.Rows[0]["dob"].ToString();
                }
            }
        }


        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                Image3.ImageUrl = dt.Rows[0]["logo"].ToString();
                Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_email.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString(); 
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();


                lbl_affiliation_no.Text = dt.Rows[0]["Affiliation"].ToString();
                lbl_school_code.Text = dt.Rows[0]["school_no"].ToString();

                if (dt.Rows[0]["Certificate_tagline1"].ToString() != "")
                {
                    lbl_cbse_aff.Text = dt.Rows[0]["Certificate_tagline1"].ToString();
                    cbseAffDV.Visible = true;
                }

                try
                {
                    Image3.Visible = true;
                    if (dt.Rows[0]["Is_certificate_watermark_hide"].ToString() == "True")
                    {
                        Image3.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../transfer-certificate-report.aspx", false);
        }
    }
}