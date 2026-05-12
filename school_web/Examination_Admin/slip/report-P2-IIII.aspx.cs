using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin.slip
{
    public partial class report_P2_IIII : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["admNo"] != null && Request.QueryString["clss_id"] != null && Request.QueryString["ssion_id"] != null && Request.QueryString["Branch_id"] != null && Request.QueryString["Term_id"] != null)
                {
                    ViewState["admissionno"] = Request.QueryString["admNo"];
                    ViewState["classid"] = Request.QueryString["clss_id"];
                    ViewState["sessionid"] = Request.QueryString["ssion_id"];
                    ViewState["Branch_id"] = Request.QueryString["Branch_id"];
                    ViewState["Term_id"] = Request.QueryString["Term_id"];
                    ViewState["RequestFrom"] = Request.QueryString["RequestFrom"];

                    hd_admission_no.Value = Request.QueryString["admNo"];
                    hd_session_id.Value = Request.QueryString["ssion_id"];
                    hd_class_id.Value = Request.QueryString["clss_id"];
                    hd_branch_id.Value = Request.QueryString["Branch_id"];
                    hd_term_id.Value = Request.QueryString["Term_id"];

                    bind_student_info();
                    Bind_schoolinfo();
                    //find_subject_marks(); 
                    if (ViewState["RequestFrom"].ToString() == "1")
                    {
                        printBtns.Visible = false;
                    }
                }
            }
        }



        private void bind_student_info()
        {
            DataTable dt = mycode.FillData("select *,isnull((select top 1 Rank from Exam_rank_master where Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id and Branch_id=admission_registor.Branch_id and Admission_no=admission_registor.admissionserialnumber and Term_id=" + hd_term_id.Value + "),'0') as Rank from admission_registor where admissionserialnumber='" + ViewState["admissionno"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "'");
            if (dt.Rows.Count != 0)
            {
                lbl_student_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_date_of_birth.Text = dt.Rows[0]["dob"].ToString();
                lbl_mother_name.Text = dt.Rows[0]["mothername"].ToString();
                lbl_father_name.Text = dt.Rows[0]["fathername"].ToString();
                lbl_father_name1.Text = dt.Rows[0]["fathername"].ToString();
                lbl_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_section.Text = dt.Rows[0]["Section"].ToString();
                lbl_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
                string term_name = Examination.get_term_name(hd_branch_id.Value, hd_term_id.Value);

                lbl_for_term.Text = term_name;
                lbl_for_class.Text = dt.Rows[0]["class"].ToString();


                lbl_sessions.Text = dt.Rows[0]["session"].ToString();
                img_student_img.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();


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
                // lbl_rank.Text = rank;


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
                ViewState["AffText"] = "Aff. No.";
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
                    img_extra_log.ImageUrl = dt.Rows[0]["Extra_logo"].ToString();
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../student-result.aspx", false);
        }

        protected void btn_back_Click1(object sender, EventArgs e)
        {
            Response.Redirect("../student-result.aspx", false);
        }
    }
}