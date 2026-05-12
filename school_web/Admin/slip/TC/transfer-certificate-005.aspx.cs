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
    public partial class transfer_certificate_005 : System.Web.UI.Page
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
                    ViewState["signatureuserid"] = "";
                    Bind_crtificate_info();
                    // Bind_schoolinfo();
                    Bind_tegline();
                }
                Bind_schoolinfo();
                Bind_Transfer_certificate_setting();
                Bind_signature_setting();
            }
        }

        private void Bind_tegline()
        {
            DataTable dt = mycode.FillData("select * from Transfer_certificate_setting where Status=1");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_tegline1.Text = dt.Rows[0]["Tegline1"].ToString();
                lbl_tegline2.Text = dt.Rows[0]["Tegline2"].ToString();
                lbl_tegline3.Text = dt.Rows[0]["Tegline3"].ToString();
            }
        }
        private void Bind_signature_setting()
        {
            try
            {
                bool matchclassteacher = false;
                DataTable dt = mycode.FillData("Select  ssm.*,drm.Menu_name as Certificate_name,ud.name as user_name,CASE WHEN ssm.Is_class_teacher= 1 THEN 'Yes' WHEN ssm.Is_class_teacher = '0' THEN 'No'  END AS isclass_teacher,CASE WHEN ssm.Is_signature_display= 1 THEN 'Yes' WHEN ssm.Is_signature_display = '0' THEN 'No'  END AS issignature_display,CASE WHEN ssm.Istatus= 1 THEN 'ON' WHEN ssm.Istatus = '0' THEN 'OFF'  END AS Status from Setting_Signature_Master ssm join Dashboard_report_menu drm on ssm.Menu_id=drm.Menu_id join user_details ud on ssm.user_id=ud.user_id where drm.Menu_name='Transfer Certificate' and ssm.Istatus='1'  order by ssm.Position");
                if (dt.Rows.Count == 0)
                {
                    bydefult.Visible = true;
                    Sig_setting.Visible = false;
                }
                else
                {
                    bydefult.Visible = false;
                    Sig_setting.Visible = true;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string Position = dt.Rows[i]["Position"].ToString();
                        string Is_signature_display = dt.Rows[i]["Is_signature_display"].ToString();
                        string Is_class_teacher = dt.Rows[i]["Is_class_teacher"].ToString();
                        string Istatus = dt.Rows[i]["Istatus"].ToString();
                        string user_id = dt.Rows[i]["user_id"].ToString();
                        string Signature = dt.Rows[i]["Signature"].ToString();
                        string Designation_Name = dt.Rows[i]["Designation_Name"].ToString();

                        string placment = "";
                        if (Position == "1")
                        {
                            placment = "1";
                        }
                        else if (Position == "2")
                        {
                            placment = "2";
                        }
                        else if (Position == "3")
                        {
                            placment = "3";
                        }

                        if (Is_class_teacher == "1")
                        {
                            if (matchclassteacher == false)
                            {
                                if (ViewState["signatureuserid"].ToString() == user_id)
                                {
                                    if (placment == "1")
                                    {
                                        if (Is_signature_display == "1")
                                        {
                                            sign1.Src = Signature;
                                        }
                                        else
                                        {
                                            sign1.Visible = false;
                                            Position1.Visible = true;
                                        }
                                        lbl_deg1.Text = "Signature of Class Teacher";
                                        Position1.Visible = true;

                                    }
                                    else if (placment == "2")
                                    {
                                        if (Is_signature_display == "1")
                                        {
                                            sign2.Src = Signature;
                                        }
                                        else
                                        {
                                            sign2.Visible = false;
                                        }
                                        lbl_deg2.Text = "Signature of Class Teacher";
                                        Position1.Visible = true;

                                    }
                                    else if (placment == "3")
                                    {
                                        if (Is_signature_display == "1")
                                        {
                                            sign3.Src = Signature;
                                        }
                                        else
                                        {
                                            sign3.Visible = false;
                                        }
                                        lbl_deg3.Text = "Signature of Class Teacher";

                                        Position3.Visible = true;

                                    }
                                }
                                else
                                {
                                }
                            }

                        }
                        else
                        {
                            if (placment == "1")
                            {
                                if (Is_signature_display == "1")
                                {
                                    sign1.Src = Signature;
                                }
                                else
                                {
                                    sign1.Visible = false;
                                }
                                lbl_deg1.Text = "Signature of " + Designation_Name;
                                Position1.Visible = true;

                            }
                            else if (placment == "2")
                            {
                                if (Is_signature_display == "1")
                                {
                                    sign2.Src = Signature;
                                }
                                else
                                {
                                    sign2.Visible = false;
                                }
                                lbl_deg2.Text = "Signature of " + Designation_Name;
                                Position2.Visible = true;

                            }
                            else if (placment == "3")
                            {
                                if (Is_signature_display == "1")
                                {
                                    sign3.Src = Signature;
                                }
                                else
                                {
                                    sign3.Visible = false;
                                }
                                lbl_deg3.Text = "Signature of " + Designation_Name;
                                Position3.Visible = true;
                            }

                        }
                    }

                }

            }
            catch
            {
                bydefult.Visible = true;
                Sig_setting.Visible = false;
            }

        }

        private void Bind_Transfer_certificate_setting()
        {
            try
            {
                DataTable dt = mycode.FillData("select * from Header_templete where Module_type='Certificate'");
                if (dt.Rows.Count == 0)
                {
                    DataTable dt1 = mycode.FillData("select * from Transfer_certificate_setting where Tc_type_id='5'");
                    if (dt1.Rows.Count == 0)
                    {
                        header_txt.Visible = true;
                        header_img.Visible = false;
                    }
                    else
                    {
                        if (dt1.Rows[0]["Is_header"].ToString() == "1")
                        {
                            header_txt.Visible = false;
                            header_img.Visible = true;
                            img_header.ImageUrl = dt1.Rows[0]["Header_Path"].ToString();
                        }
                        else
                        {

                            header_txt.Visible = true;
                            header_img.Visible = false;
                        }
                    }
                }
                else
                {
                    if (dt.Rows[0]["Status"].ToString() == "1")
                    {
                        header_txt.Visible = false;
                        header_img.Visible = true;
                        img_header.ImageUrl = dt.Rows[0]["Path"].ToString();
                    }
                    else
                    {

                        header_txt.Visible = true;
                        header_img.Visible = false;
                    }
                }
            }
            catch
            {
                header_txt.Visible = true;
                header_img.Visible = false;

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
                string Section = dt.Rows[0]["Section"].ToString();
                string Class_id = dt.Rows[0]["Class_id"].ToString();
                string Session_id = dt.Rows[0]["Session_id"].ToString();

                ViewState["signatureuserid"] = My.get_user_id_class_teacher(Session_id, Class_id, Section);

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


                lbl_aadhar_no.Text = dt.Rows[0]["Aadhar_no"].ToString();
                txt_pen_no.Text = dt.Rows[0]["Student_PEN"].ToString();
                string dob = "";
                try
                {
                    dob = dt.Rows[0]["Date_of_births"].ToString();
                    if (dob == "")
                    {
                        dob = dt.Rows[0]["dob"].ToString();
                    }

                    string dday = dob.Substring(0, 2);
                    string dmonth = dob.Substring(3, 2);
                    string dyear = dob.Substring(6, 4);
                    string monthName = My.getMonthS_full_name(dmonth);

                    string inword_day = mycode.NumberToWords(Convert.ToInt32(dday));
                    string inword_year = mycode.NumberToWords(Convert.ToInt32(dyear));

                    lbl_dob.Text = dob + ", " + inword_day + " " + monthName + ", " + inword_year;
                }
                catch (Exception ex)
                {
                    lbl_dob.Text = dob;
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
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_school_no.Text = dt.Rows[0]["school_no"].ToString();
                lbl_aff_no.Text = dt.Rows[0]["Affiliation"].ToString();

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