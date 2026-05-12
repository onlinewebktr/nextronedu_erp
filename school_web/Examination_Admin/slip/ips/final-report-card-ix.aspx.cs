using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin.slip.ips
{
    public partial class final_report_card_ix : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["admNo"] != null && Request.QueryString["clss_id"] != null && Request.QueryString["ssion_id"] != null && Request.QueryString["Branch_id"] != null)
                {
                    ViewState["admissionno"] = Request.QueryString["admNo"];
                    ViewState["classid"] = Request.QueryString["clss_id"];
                    ViewState["sessionid"] = Request.QueryString["ssion_id"];
                    ViewState["Branch_id"] = Request.QueryString["Branch_id"];


                    hd_admission_no.Value = Request.QueryString["admNo"];
                    hd_session_id.Value = Request.QueryString["ssion_id"];
                    hd_class_id.Value = Request.QueryString["clss_id"];
                    hd_branch_id.Value = Request.QueryString["Branch_id"];

                    find_terms();
                    bind_student_info();
                    Bind_schoolinfo();
                    //find_subject_marks();
                }

            }
        }

        private void find_terms()
        {
            string query = "select Short_Name,Grade_System_Id,Exam_Term_Id from Exam_Term_Details where Session_Id=" + hd_session_id.Value + " and Branch_Id='" + hd_branch_id.Value + "' and Class_id=" + hd_class_id.Value + " order by Sequence_No asc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 2)
            {
                hd_term1.Value = dt.Rows[0]["Exam_Term_Id"].ToString();
                hd_term2.Value = dt.Rows[1]["Exam_Term_Id"].ToString();

                //termITexts.Text = dt.Rows[0]["Short_Name"].ToString();
                //termIITexts.Text = dt.Rows[1]["Short_Name"].ToString();

                termIText1.Text = dt.Rows[0]["Short_Name"].ToString();
                termIText2.Text = dt.Rows[1]["Short_Name"].ToString();

                Examination.map_term_assesment(hd_session_id.Value, hd_branch_id.Value, hd_class_id.Value, hd_term1.Value, hd_term2.Value);
            }
        }


        private void bind_student_info()
        {
            DataTable dt = mycode.FillData("select * from admission_registor where admissionserialnumber='" + ViewState["admissionno"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "'");
            if (dt.Rows.Count != 0)
            {
                lbl_student_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_date_of_birth.Text = dt.Rows[0]["dob"].ToString();
                lbl_mother_name.Text = dt.Rows[0]["mothername"].ToString();
                lbl_father_name.Text = dt.Rows[0]["fathername"].ToString();
                lbl_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_section.Text = dt.Rows[0]["Section"].ToString();
                lbl_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
                lbl_for_class.Text = " " + dt.Rows[0]["class"].ToString();

                lbl_sessions.Text = dt.Rows[0]["session"].ToString();
                img_student_img.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
            }
        }

        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_email.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
                lbl_aff_no.Text = "CBSE Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
            }
        }
    }
}