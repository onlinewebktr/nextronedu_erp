using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin.slip.nni
{
    public partial class final_report_card_up_to_viii : System.Web.UI.Page
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

                termITexts.Text = dt.Rows[0]["Short_Name"].ToString();
                termIITexts.Text = dt.Rows[1]["Short_Name"].ToString();

                //termITexts1.Text = dt.Rows[0]["Short_Name"].ToString();
                //termITexts2.Text = dt.Rows[1]["Short_Name"].ToString();

                termITexts3.Text = dt.Rows[0]["Short_Name"].ToString();
                termITexts4.Text = dt.Rows[1]["Short_Name"].ToString();

                termITexts5.Text = dt.Rows[0]["Short_Name"].ToString();
                termITexts6.Text = dt.Rows[1]["Short_Name"].ToString();

                Examination.map_term_assesment(hd_session_id.Value, hd_branch_id.Value, hd_class_id.Value, hd_term1.Value, hd_term2.Value);
            }
        }


        private void bind_student_info()
        {
            DataTable dt = mycode.FillData("select *,isnull((select top 1 Rank from Exam_rank_master_final_year where Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id and Branch_id=admission_registor.Branch_id and Admission_no=admission_registor.admissionserialnumber),'0') as Rank from admission_registor where admissionserialnumber='" + ViewState["admissionno"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "'");
            if (dt.Rows.Count != 0)
            {
                lbl_student_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_date_of_birth.Text = dt.Rows[0]["dob"].ToString();
                lbl_mother_name.Text = dt.Rows[0]["mothername"].ToString();
                lbl_father_name.Text = dt.Rows[0]["fathername"].ToString();
                lbl_father_name2.Text = dt.Rows[0]["fathername"].ToString();
                lbl_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_section.Text = dt.Rows[0]["Section"].ToString();
                lbl_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
                string term_name = Examination.get_term_name(hd_branch_id.Value, hd_term1.Value);

                lbl_for_term.Text = "FINAL";
                lbl_for_term1.Text = "FINAL";
                lbl_for_class.Text = dt.Rows[0]["class"].ToString();
                lbl_for_class1.Text = dt.Rows[0]["class"].ToString();
                lbl_classs.Text = dt.Rows[0]["class"].ToString();

                lbl_sessions.Text = dt.Rows[0]["session"].ToString();
                lbl_sessions1.Text = dt.Rows[0]["session"].ToString();
                img_student_img.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
                img_student_img1.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();



                string rank = "0";
                if (dt.Rows[0]["Rank"].ToString() == "1")
                {
                    rank = dt.Rows[0]["Rank"].ToString() + "st";
                }
                else if (dt.Rows[0]["Rank"].ToString() == "2")
                {
                    rank = dt.Rows[0]["Rank"].ToString() + "nd";
                }
                else if (dt.Rows[0]["Rank"].ToString() == "3")
                {
                    rank = dt.Rows[0]["Rank"].ToString() + "rd";
                }
                else
                {
                    rank = dt.Rows[0]["Rank"].ToString() + "th";
                }
                lbl_rank.Text = rank;


                string is_show_rank = is_show_ranks();
                if (is_show_rank == "1")
                {
                    ranksDV.Visible = true;
                }
                else
                {
                    ranksDV.Visible = false;
                }
                

                 
            }
        }


        private string is_show_ranks()
        {
            string returN = "0";
            string query = "select Is_show_rank,Aff_text from Exam_report_card_setting where  Session_Id=" + ViewState["sessionid"].ToString() + " and Branch_Id='" + hd_branch_id.Value + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                return returN;
            }
            else
            {
                if (dt.Rows[0]["Is_show_rank"].ToString() == "True")
                {
                    returN = "1";
                }
                else
                {
                    returN = "0";
                }
                //================
                if (dt.Rows[0]["Aff_text"].ToString() == "")
                {
                    ViewState["AffText"] = "Aff. No.";
                }
                else
                {
                    ViewState["AffText"] = dt.Rows[0]["Aff_text"].ToString();
                }
            }
            return returN;
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
                img_watermark.ImageUrl = dt.Rows[0]["Watermark_image"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_email.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
                lbl_estd.Text = "ESTD : " + dt.Rows[0]["estd"].ToString();
                lbl_aff_text.Text = dt.Rows[0]["Affiliated_by_full_text"].ToString();
                lbl_aff_no.Text = ViewState["AffText"].ToString() + "  : " + dt.Rows[0]["Affiliation"].ToString();
                try
                {
                    lbl_website.Text = dt.Rows[0]["Website_for_report_card"].ToString();
                }
                catch (Exception ex)
                {
                    lbl_website.Text = dt.Rows[0]["website"].ToString();
                }

                //====================
                Image2S.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address1.Text = dt.Rows[0]["address1"].ToString();
                lbl_email1.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name1.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no1.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
                lbl_estd1.Text = "ESTD : " + dt.Rows[0]["estd"].ToString();
                lbl_aff_text1.Text = dt.Rows[0]["Affiliated_by_full_text"].ToString();
                lbl_aff_no1.Text = ViewState["AffText"].ToString() + "  : " + dt.Rows[0]["Affiliation"].ToString();
                lbl_school_code.Text = "School code  : " + dt.Rows[0]["school_no"].ToString();


                try
                {
                    lbl_website1.Text = dt.Rows[0]["Website_for_report_card"].ToString();
                }
                catch (Exception ex)
                {
                    lbl_website1.Text = dt.Rows[0]["website"].ToString();
                }
                


                try
                {
                    lbl_aff_year.Text = "Aff. : " + dt.Rows[0]["Aff_year"].ToString();
                }
                catch (Exception ex)
                {
                }

                try
                {
                    img_extra_log.ImageUrl = dt.Rows[0]["Extra_logo"].ToString();
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../student-result-final.aspx", false);
        }
    }
}