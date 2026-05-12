using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace school_web.Examination_Admin.slip.vvc.api
{
    public class vvc_exam_setting
    {
        internal static string get_exam_settings(string session_id, string branch_id, string class_id)
        {
            string returN = "hidden/hidden/hidden/hidden/hidden/160px/hidden/hidden/hidden/show/hidden/hidden/hidden/hidden/1250px/sameLine/2px/2px/2px/2px/2px/2px/2px/hidden/hidden";
            string query = "select * from Exam_report_card_setting where  Session_Id=" + session_id + " and Branch_Id='" + branch_id + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                string special_note = "hidden"; string Qr_code = "hidden"; string instruction2 = "hidden"; string Rank = "hidden"; string Graph = "hidden"; string GraphHeight = "160px"; string percent_remark = "hidden"; string sign_top = "show"; string sign_bottom = "hidden"; string is_max_mark_show = "show"; string is_estd_show = "hidden"; string is_contact_no_show = "hidden"; string is_email_show = "hidden"; string is_class_text_show = "hidden"; string height_dv = "1250px"; string is_class_new_line = "sameLine"; string co_sch_area_margn = "2px"; string overall_area_margn = "2px"; string percent_remark_area_margn = "2px"; string graph_area_margn = "2px"; string ins1_area_margn = "2px"; string ins2_area_margn = "2px"; string toppers_area_margn = "2px"; string is_watermark_Show = "hidden"; string is_std_img_hide = ""; string is_std_section_hide = ""; string aff_text = ""; string father_name1 = "show"; string father_name2 = "hidden"; string is_subj_code_hide = ""; string is_text_center = "txtleft"; string Is_website_show = "hidden"; string WhatHeadShow = "1"; string Is_aff_no_hide = "hidden"; string Is_overall_grade_hide = "show"; string Is_grace = "0"; string Is_aff_year = "hidden"; string Is_co_scholastic_hide = "showd";
                if (dt.Rows[0]["Is_special_note_show"].ToString() == "True")
                {
                    special_note = "show";
                }

                if (dt.Rows[0]["Is_qr_code_show"].ToString() == "True")
                {
                    Qr_code = "show";
                }

                if (dt.Rows[0]["Is_show_instruction2"].ToString() == "True")
                {
                    instruction2 = "show";
                }
                if (dt.Rows[0]["Is_show_rank"].ToString() == "True")
                {
                    Rank = "show";
                }
                if (dt.Rows[0]["Is_graph_show"].ToString() == "True")
                {
                    Graph = "show";
                }
                if (dt.Rows[0]["Graph_height"].ToString() != null || dt.Rows[0]["Graph_height"].ToString() != "")
                {
                    GraphHeight = dt.Rows[0]["Graph_height"].ToString();
                }
                if (dt.Rows[0]["Is_percent_remark_show"].ToString() == "True")
                {
                    percent_remark = "show";
                }

                if (dt.Rows[0]["Is_sign_show_in_bottom"].ToString() == "True")
                {
                    sign_top = "hidden";
                    sign_bottom = "show";
                }

                try
                {
                    if (dt.Rows[0]["Aff_text"].ToString() == "")
                    {
                        aff_text = "AFF NO.";
                    }
                    else
                    {
                        aff_text = dt.Rows[0]["Aff_text"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    aff_text = "AFF NO.";
                }

                try
                {
                    if (dt.Rows[0]["Is_text_center"].ToString() == "True")
                    {
                        is_text_center = "txtcenter";
                    }
                }
                catch (Exception ex)
                {
                    is_text_center = "txtleft";
                }



                //================Is Maximum Mark Show
                string querym = "select * from Exam_maximum_mark_is_show where  Class_id=" + class_id + " and Branch_id='" + branch_id + "'";
                DataTable dtm = My.dataTable(querym);
                if (dtm.Rows.Count > 0)
                {
                    if (dtm.Rows[0]["Is_hide"].ToString() == "True")
                    {
                        is_max_mark_show = "hidden";
                    }
                }

                //====================
                if (dt.Rows[0]["Is_estd_show"].ToString() == "True")
                {
                    is_estd_show = "show";
                }
                if (dt.Rows[0]["Is_contact_no_show"].ToString() == "True")
                {
                    is_contact_no_show = "show";
                }
                if (dt.Rows[0]["Is_email_show"].ToString() == "True")
                {
                    is_email_show = "show";
                }
                try
                {
                    if (dt.Rows[0]["Is_website_show"].ToString() == "True")
                    {
                        Is_website_show = "show";
                    }
                }
                catch (Exception ex)
                {
                    Is_website_show = "hidden";
                }
                if (dt.Rows[0]["Is_class_text_show"].ToString() == "True")
                {
                    is_class_text_show = "show";
                }
                height_dv = dt.Rows[0]["Rp_card_height"].ToString();
                if (height_dv == "")
                {
                    height_dv = "1250px";
                }
                if (dt.Rows[0]["Is_class_in_new_line"].ToString() == "True")
                {
                    is_class_new_line = "newLine";
                }


                //=======================
                co_sch_area_margn = dt.Rows[0]["Co_sch_area_margn"].ToString();
                overall_area_margn = dt.Rows[0]["Overall_area_margn"].ToString();
                percent_remark_area_margn = dt.Rows[0]["Percent_remark_area_margn"].ToString();
                graph_area_margn = dt.Rows[0]["Graph_area_margn"].ToString();
                ins1_area_margn = dt.Rows[0]["Ins1_area_margn"].ToString();
                ins2_area_margn = dt.Rows[0]["Ins2_area_margn"].ToString();
                toppers_area_margn = dt.Rows[0]["Toppers_area_margn"].ToString();
                if (co_sch_area_margn == "")
                {
                    co_sch_area_margn = "2px";
                }
                if (overall_area_margn == "")
                {
                    overall_area_margn = "2px";
                }
                if (percent_remark_area_margn == "")
                {
                    percent_remark_area_margn = "2px";
                }
                if (graph_area_margn == "")
                {
                    graph_area_margn = "2px";
                }
                if (ins1_area_margn == "")
                {
                    ins1_area_margn = "2px";
                }
                if (ins2_area_margn == "")
                {
                    ins2_area_margn = "2px";
                }
                if (toppers_area_margn == "")
                {
                    toppers_area_margn = "2px";
                }



                string queryw = "select Watermark_image from Firm_Details";
                DataTable dtw = My.dataTable(queryw);
                if (dtw.Rows.Count > 0)
                {
                    string check_path = dtw.Rows[0]["Watermark_image"].ToString();
                    if (check_path != "")
                    {
                        if (dt.Rows[0]["Is_watermark_show"].ToString() == "True")
                        {
                            is_watermark_Show = "showWaterMark";
                        }
                    }
                }




                try
                {
                    //================Is SectionHide
                    string queryms = "select * from Exam_report_card_setting_classwise where  Class_id=" + class_id + " and Branch_id='" + branch_id + "'";
                    DataTable dtms = My.dataTable(queryms);
                    if (dtms.Rows.Count > 0)
                    {
                        if (dtms.Rows[0]["Is_student_img_hide"].ToString() == "True")
                        {
                            is_std_img_hide = "hidden";
                        }
                        if (dtms.Rows[0]["Is_section_hide"].ToString() == "True")
                        {
                            is_std_section_hide = "hidden";
                            father_name1 = "hidden";
                            father_name2 = "show";
                        }
                        if (dtms.Rows[0]["Is_subject_code_hide"].ToString() == "True")
                        {
                            is_subj_code_hide = "hidden";
                        }


                        try
                        {
                            if (dtms.Rows[0]["Is_affiliation_no_show"].ToString() == "True")
                            {
                                Is_aff_no_hide = "show";
                            }
                        }
                        catch (Exception ex)
                        {
                            Is_aff_no_hide = "hidden";
                        }

                        try
                        {
                            if (dtms.Rows[0]["Is_overall_grade_hide"].ToString() == "True")
                            {
                                Is_overall_grade_hide = "hidden";
                            }
                        }
                        catch (Exception ex)
                        {
                            Is_overall_grade_hide = "show";
                        }


                        try
                        {
                            if (dtms.Rows[0]["Is_grace"].ToString() == "True")
                            {
                                Is_grace = "1";
                            }
                        }
                        catch (Exception ex)
                        {
                            Is_grace = "0";
                        }


                        try
                        {
                            if (dtms.Rows[0]["Is_aff_year_show"].ToString() == "True")
                            {
                                Is_aff_year = "showed";
                            }
                        }
                        catch (Exception ex)
                        {
                            Is_aff_year = "hidden";
                        }


                        ////=========Is_co_scholastic_show
                        try
                        {
                            if (dtms.Rows[0]["Is_co_scholastic_hide"].ToString() == "True")
                            {
                                Is_co_scholastic_hide = "hidden";
                            }
                        }
                        catch (Exception ex)
                        {
                            Is_co_scholastic_hide = "showed";
                        }

                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    //================Is WhatHeadShow
                    string queryhd = "select * from Exam_head_type_show where Is_show=1";
                    DataTable dthd = My.dataTable(queryhd);
                    if (dthd.Rows.Count > 0)
                    {
                        WhatHeadShow = dthd.Rows[0]["Head_type"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    WhatHeadShow = "1";
                }

                returN = special_note + "/" + Qr_code + "/" + instruction2 + "/" + Rank + "/" + Graph + "/" + GraphHeight + "/" + percent_remark + "/" + sign_top + "/" + sign_bottom + "/" + is_max_mark_show + "/" + is_estd_show + "/" + is_contact_no_show + "/" + is_email_show + "/" + is_class_text_show + "/" + height_dv + "/" + is_class_new_line + "/" + co_sch_area_margn + "/" + overall_area_margn + "/" + percent_remark_area_margn + "/" + graph_area_margn + "/" + ins1_area_margn + "/" + ins2_area_margn + "/" + toppers_area_margn + "/" + is_watermark_Show + "/" + is_std_img_hide + "/" + is_std_section_hide + "/" + aff_text + "/" + father_name1 + "/" + father_name2 + "/" + is_subj_code_hide + "/" + is_text_center + "/" + Is_website_show + "/" + WhatHeadShow + "/" + Is_aff_no_hide + "/" + Is_overall_grade_hide + "/" + Is_grace + "/" + Is_aff_year + "/" + Is_co_scholastic_hide;
                return returN;
            }
        }

        internal static string get_first_term(string session_id, string Branch_id, string class_id)
        {
            string trmsi = "";
            string querys = "select Short_Name,Grade_System_Id,Exam_Term_Id from Exam_Term_Details where Session_Id=" + session_id + " and Branch_Id='" + Branch_id + "' and Class_id=" + class_id + " order by Sequence_No asc";
            DataTable dtx = My.dataTable(querys);
            if (dtx.Rows.Count != 0)
            {
                trmsi = dtx.Rows[0]["Exam_Term_Id"].ToString();
            }
            return trmsi;
        }

        internal static string get_grade_of_a_subject(string session_id, string branch_id, string Grade_System_Id, double final_marks, double full_number, string class_id)
        {
            string is_calculate_100 = "1";
            try
            {
                string querys = "select Is_percent_calculate_not_100 from Exam_report_card_setting_classwise where Class_id='" + class_id + "' and Branch_id='" + branch_id + "'";
                DataTable dts = My.dataTable(querys);
                if (dts.Rows.Count > 0)
                {
                    if (dts.Rows[0]["Is_percent_calculate_not_100"].ToString() == "True")
                    {
                        is_calculate_100 = "0";
                    }
                }
            }
            catch (Exception ex)
            {
            }


            double final_perc_round = 0;
            if (is_calculate_100 == "1")
            {
                double final_perc = (final_marks / full_number) * 100;
                final_perc_round = Math.Round(final_perc);
            }
            else
            {
                final_perc_round = Math.Round(final_marks);
            }

            string returN = "FAIL";
            string query = "select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + Grade_System_Id + " and  Session_Id=" + session_id + " and Branch_Id='" + branch_id + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double Lower_Range = My.toDouble(dt.Rows[i]["Lower_Range"].ToString());
                    double Upper_Range = My.toDouble(dt.Rows[i]["Upper_Range"].ToString());

                    if (final_perc_round >= Lower_Range && final_perc_round <= Upper_Range)
                    {
                        returN = dt.Rows[i]["Grade"].ToString();
                    }
                }
                return returN;
            }
        }


        internal static string get_grace_marks(string session_id, string branch_id, string class_id, string term_id, string subject_id, string admission_no)
        {
            string returN = "0";
            string query = "select Grace_Marks from Exam_grace_mark_subj_wise where Session_id=" + session_id + " and Branch_id='" + branch_id + "' and Class_id=" + class_id + " and Term_id=" + term_id + " and Subject_id=" + subject_id + " and Admission_no='" + admission_no + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = dt.Rows[0]["Grace_Marks"].ToString();
                return returN;
            }
        }
    }
}