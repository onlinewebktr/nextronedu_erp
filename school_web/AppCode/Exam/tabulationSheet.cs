using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode.Exam
{
    public class tabulationSheet
    {
        #region  OVERALL SUBJECTS
        internal static string get_subject_total_marks(string session_id, string class_id, string admission_no, string term_id, string subject_id, string branch_id, string Type)
        {
            Dictionary<string, object> dc1 = get_term_marks_calculation(session_id, branch_id, class_id, term_id);
            string Grade_System_Id = (String)dc1["Grade_System_Id"];
            string Maximum_Marks = (String)dc1["Maximum_Marks"];
            string Cut_Off_Percentage = (String)dc1["Cut_Off_Percentage"];
            string Calculation_Type = (String)dc1["Calculation_Type"];
            string Is_Advanced_Advanced_Setting = (String)dc1["Is_Advanced_Advanced_Setting"];
            string Consider_best = (String)dc1["Consider_best"];
            string Pass_criteria = (String)dc1["Pass_criteria"];
            string Is_Mandatory_to_pass = (String)dc1["Is_Mandatory_to_pass"];


            double total_number = 0; double full_number = 0;
            #region OVERALL CALCULATION 

            if (Calculation_Type == "Sum")
            {
                #region SUM CALCULATION
                if (Is_Advanced_Advanced_Setting == "True" || Is_Advanced_Advanced_Setting == "1")
                {
                    if (Pass_criteria == "None")
                    {
                        string query = "select top " + Consider_best + " eta.Marks from Exam_temp_tabulation_total_no eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'  and ad.Select_Data=1 order by cast(eta.Marks as float) desc";
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                            }
                        }
                    }
                    else
                    {
                        string[] stringSeparatorss = new string[] { " OF " };
                        string[] arrs = Pass_criteria.Split(stringSeparatorss, StringSplitOptions.None);
                        string First = arrs[0];
                        string second = arrs[1];

                        string query = "select top " + Consider_best + " eta.Marks from Exam_temp_tabulation_total_no eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'  and ad.Select_Data=1 order by cast(eta.Marks as float) desc";
                        SqlCommand cmd = new SqlCommand(query);
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            double persent_of_obtain_mark = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                persent_of_obtain_mark = My.toDouble(dt.Rows[i]["Marks"].ToString()) / My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString()) * 100;
                                if (persent_of_obtain_mark >= My.toDouble(dt.Rows[i]["Cut_Off_Percentage"].ToString()))
                                {
                                    total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                }
                            }
                        }
                    }
                }
                else
                {
                    ///=========================IS  NOT ADVANCE
                    ///
                    string query = "select eta.Marks,eta.Full_marks from Exam_temp_tabulation_total_no eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "' ";
                    DataTable dt = FillDatastatic(query);
                    if (dt.Rows.Count == 0)
                    {
                        total_number = 0;
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                            full_number = full_number + My.toDouble(dt.Rows[i]["Full_marks"].ToString());
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region AVERAGE CALCULATION
                if (Is_Advanced_Advanced_Setting == "True" || Is_Advanced_Advanced_Setting == "1")
                {
                    if (Pass_criteria == "None")
                    {
                        string query = "select top " + Consider_best + " eta.Marks from Exam_temp_tabulation_total_no eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'  and ad.Select_Data=1 order by cast(eta.Marks as float) desc";
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                            }

                            total_number = total_number / dt.Rows.Count;
                        }
                    }
                    else
                    {
                        string[] stringSeparators1 = new string[] { " OF " };
                        string[] arr1 = Pass_criteria.Split(stringSeparators1, StringSplitOptions.None);
                        string First = arr1[0];
                        string second = arr1[1];


                        string query = "select top " + Consider_best + " eta.Marks,etd.Maximum_Marks,etd.Cut_Off_Percentage from Exam_temp_tabulation_total_no eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'  and ad.Select_Data=1 order by cast(eta.Marks as float) desc";
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            double persent_of_obtain_mark = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                persent_of_obtain_mark = My.toDouble(dt.Rows[i]["Marks"].ToString()) / My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString()) * 100;
                                if (persent_of_obtain_mark >= My.toDouble(dt.Rows[i]["Cut_Off_Percentage"].ToString()))
                                {
                                    total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                }
                            }
                            total_number = total_number / dt.Rows.Count;
                        }
                    }
                }
                else
                {
                    ///=========================IS  NOT ADVANCE
                    ///

                    string query = "select  eta.Marks,eta.Full_marks from Exam_temp_tabulation_total_no eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'";
                    DataTable dt = FillDatastatic(query);
                    if (dt.Rows.Count == 0)
                    {
                        total_number = 0;
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                            full_number = full_number + My.toDouble(dt.Rows[i]["Full_marks"].ToString());
                        }
                        total_number = total_number / dt.Rows.Count;
                    }
                }
                #endregion
            }
            #endregion

            Dictionary<string, object> dc2 = get_grade_logic(session_id, branch_id, Grade_System_Id);
            string With_Decimal = (String)dc2["With_Decimal"];
            string Without_Decimal = (String)dc2["Without_Decimal"];
            string Round_up = (String)dc2["Round_up"];
            string Round_down = (String)dc2["Round_down"];
            string Half_Round_Up = (String)dc2["Half_Round_Up"];
            string Half_Round_Down = (String)dc2["Half_Round_Down"];
            string With_Decimal_Per = (String)dc2["With_Decimal_Per"];
            string Without_Decimal_Per = (String)dc2["Without_Decimal_Per"];

            string Round_up_Per = (String)dc2["Round_up_Per"];
            string Round_down_Per = (String)dc2["Round_down_Per"];
            string Half_Round_Up_Per = (String)dc2["Half_Round_Up_Per"];
            string Half_Round_Down_Per = (String)dc2["Half_Round_Down_Per"];
            string Round_Percentage_Checked = (String)dc2["Round_Percentage_Checked"];
            string Maximum_numbe_decimal = (String)dc2["Maximum_numbe_decimal"];

            string Input_Type = (String)dc2["Input_Type"];
            string Output = (String)dc2["Output"];

            //============================================


            double final_marks = total_number;
            if (Type == "Scholastic")
            {
                if (Output == "Marks")
                {
                    string Grade_head_text = "TOTAL MARKS";
                    string grade = get_grade_of_a_subject(session_id, branch_id, Grade_System_Id, final_marks, full_number, class_id);   // Grade of a subject 
                    return final_marks.ToString() + "/" + grade + "/" + Grade_head_text + "/" + full_number;
                }
                else
                {
                    return "0/0/0/0";
                    //string Grade_head_text = "OVERALL GRADE";
                    //string grade = SdhrExmSettings.get_grade_of_a_subject(session_id, branch_id, Grade_System_Id, final_marks, full_number, class_id);   // Grade of a subject 
                    //string obt_and_full_mark = get_obt_and_full_marks(session_id, branch_id, admission_no, term_id, class_id); // Obtain Mark/Full Mark/OverAll Percentage

                    //string[] stringSeparatorsss = new string[] { "/" };
                    //string[] arrs1 = obt_and_full_mark.Split(stringSeparatorsss, StringSplitOptions.None);
                    //string obt_mark = arrs1[0];
                    //string full_mark = arrs1[1];
                    //string overall_persentage = arrs1[2];

                    //string Overall_grade = get_grade(session_id, branch_id, Grade_System_Id, My.toDouble(overall_persentage), class_id);   // Grade of a subject
                    //overall_persentage = Overall_grade;
                    //obt_mark = "hidden";

                    //obt_and_full_mark = obt_mark + "/" + full_mark + "/" + overall_persentage;

                    //string exam_setting = get_exam_setting(session_id, branch_id);  // Is Attendance Show
                    //string attandance = get_attandance_details(session_id, branch_id, class_id, term_id, admission_no);    //Total Class/Present Class/Present attendance percent
                    //string remarkss = get_remarks(session_id, branch_id, class_id, term_id, admission_no);  // REMARKS
                    //string hide_last_grade_clumn = "hidden";
                    //string exam_settings = SdhrExmSettings.get_exam_settings(session_id, branch_id, class_id);  // Is SpecialNote & QR Show
                    //return grade + "/" + hide_last_grade_clumn + "/" + obt_and_full_mark + "/" + exam_setting + "/" + attandance + "/" + Grade_head_text + "/" + remarkss + "/" + exam_settings;
                }
            }
            else
            {
                return "0/0/0/0";
                //string Grade_head_text = "OVERALL GRADE";
                //string grade = SdhrExmSettings.get_grade_of_a_subject(session_id, branch_id, Grade_System_Id, final_marks, full_number, class_id);   // Grade of a subject  
                //string obt_and_full_mark = get_obt_and_full_marks(session_id, branch_id, admission_no, term_id, class_id); // Obtain Mark/Full Mark/OverAll Percentage

                //string[] stringSeparatorsss = new string[] { "/" };
                //string[] arrs1 = obt_and_full_mark.Split(stringSeparatorsss, StringSplitOptions.None);
                //string obt_mark = arrs1[0];
                //string full_mark = arrs1[1];
                //string overall_persentage = arrs1[2];

                //string Overall_grade = get_grade(session_id, branch_id, Grade_System_Id, My.toDouble(overall_persentage), class_id);   // Grade of a subject
                //overall_persentage = Overall_grade;
                //obt_mark = "hidden";

                //obt_and_full_mark = obt_mark + "/" + full_mark + "/" + overall_persentage;
                //string exam_setting = get_exam_setting(session_id, branch_id);  // Is Attendance Show
                //string attandance = get_attandance_details(session_id, branch_id, class_id, term_id, admission_no);    //Total Class/Present Class/Present attendance percent
                //string remarkss = get_remarks(session_id, branch_id, class_id, term_id, admission_no);  // REMARKS
                //string hide_last_grade_clumn = "hidden";
                //string exam_settings = SdhrExmSettings.get_exam_settings(session_id, branch_id, class_id);  // Is SpecialNote & QR Show 
                //return grade + "/" + hide_last_grade_clumn + "/" + obt_and_full_mark + "/" + exam_setting + "/" + attandance + "/" + Grade_head_text + "/" + remarkss + "/" + exam_settings;
            }
        }


        private static Dictionary<string, object> get_term_marks_calculation(string session_id, string branch_id, string class_id, string term_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = " select * from Exam_Term_Details where Session_Id='" + session_id + "' and Branch_Id='" + branch_id + "' and Istatus=1 and Exam_Term_Id=" + term_id + " and Class_id=" + class_id + "";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = getdata(cmd);
            if (dt.Rows.Count == 0)
            {
                dc["Grade_System_Id"] = "0";
                dc["Maximum_Marks"] = "0";
                dc["Cut_Off_Percentage"] = "0";
                dc["Calculation_Type"] = "0";
                dc["Is_Advanced_Advanced_Setting"] = "0";
                dc["Consider_best"] = "0";
                dc["Pass_criteria"] = "0";
                dc["Is_Mandatory_to_pass"] = "0";
            }
            else
            {
                dc["Grade_System_Id"] = dt.Rows[0]["Grade_System_Id"].ToString();
                dc["Maximum_Marks"] = dt.Rows[0]["Maximum_Marks"].ToString();
                dc["Cut_Off_Percentage"] = dt.Rows[0]["Cut_Off_Percentage"].ToString();
                dc["Calculation_Type"] = dt.Rows[0]["Calculation_Type"].ToString();
                dc["Is_Advanced_Advanced_Setting"] = dt.Rows[0]["Is_Advanced_Advanced_Setting"].ToString();
                dc["Consider_best"] = dt.Rows[0]["Consider_best"].ToString();
                dc["Pass_criteria"] = dt.Rows[0]["Pass_criteria"].ToString();
                dc["Is_Mandatory_to_pass"] = dt.Rows[0]["Is_Mandatory_to_pass"].ToString();
            }
            return dc;
        }

        private static string get_grade(string session_id, string branch_id, string Grade_System_Id, double final_marks, string Class_id)
        {
            string is_calculate_50 = "0";
            try
            {
                string querys = "select Is_overall_percent_calculate_in_50 from Exam_report_card_setting_classwise where Class_id='" + Class_id + "' and Branch_id='" + branch_id + "'";
                DataTable dts = My.dataTable(querys);
                if (dts.Rows.Count > 0)
                {
                    if (dts.Rows[0]["Is_overall_percent_calculate_in_50"].ToString() == "True")
                    {
                        is_calculate_50 = "1";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (is_calculate_50 == "1")
            {
                final_marks = final_marks / 2;
            }


            final_marks = Math.Round(final_marks);
            string returN = "FAIL";
            string query = "select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + Grade_System_Id + " and  Session_Id=" + session_id + " and Branch_Id='" + branch_id + "'";
            DataTable dt = FillDatastatic(query);
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

                    if (final_marks >= Lower_Range && final_marks <= Upper_Range)
                    {
                        returN = dt.Rows[i]["Grade"].ToString();
                    }
                }
                return returN;
            }
        }




        #endregion
        private static Dictionary<string, object> get_grade_logic(string Session_id, string Branch_id, string grade_system_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = " select * from Exam_Grade_System where Session_Id='" + Session_id + "' and Branch_id='" + Branch_id + "'  and Grade_System_Id=" + grade_system_id + "";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = getdata(cmd);

            if (dt.Rows.Count == 0)
            {
                dc["With_Decimal"] = "0";
                dc["Without_Decimal"] = "0";
                dc["Round_up"] = "0";
                dc["Round_down"] = "0";
                dc["Half_Round_Up"] = "0";
                dc["Half_Round_Down"] = "0";

                dc["With_Decimal_Per"] = "0";
                dc["Without_Decimal_Per"] = "0";
                dc["Round_up_Per"] = "0";
                dc["Round_down_Per"] = "0";
                dc["Half_Round_Up_Per"] = "0";
                dc["Half_Round_Down_Per"] = "0";
                dc["Round_Percentage_Checked"] = "0";
                dc["Maximum_numbe_decimal"] = "0";

                dc["Input_Type"] = "0";
                dc["Output"] = "0";
            }
            else
            {
                dc["With_Decimal"] = dt.Rows[0]["With_Decimal"].ToString();
                dc["Without_Decimal"] = dt.Rows[0]["Without_Decimal"].ToString();
                dc["Round_up"] = dt.Rows[0]["Round_up"].ToString();
                dc["Round_down"] = dt.Rows[0]["Round_down"].ToString();
                dc["Half_Round_Up"] = dt.Rows[0]["Half_Round_Up"].ToString();
                dc["Half_Round_Down"] = dt.Rows[0]["Half_Round_Down"].ToString();


                dc["With_Decimal_Per"] = dt.Rows[0]["With_Decimal_Per"].ToString();
                dc["Without_Decimal_Per"] = dt.Rows[0]["Without_Decimal_Per"].ToString();
                dc["Round_up_Per"] = dt.Rows[0]["Round_up_Per"].ToString();
                dc["Round_down_Per"] = dt.Rows[0]["Round_down_Per"].ToString();
                dc["Half_Round_Up_Per"] = dt.Rows[0]["Half_Round_Up_Per"].ToString();
                dc["Half_Round_Down_Per"] = dt.Rows[0]["Half_Round_Down_Per"].ToString();
                dc["Round_Percentage_Checked"] = dt.Rows[0]["Round_Percentage_Checked"].ToString();
                dc["Maximum_numbe_decimal"] = dt.Rows[0]["Maximum_numbe_decimal"].ToString();

                dc["Input_Type"] = dt.Rows[0]["Input_Type"].ToString();
                dc["Output"] = dt.Rows[0]["Output"].ToString();
            }
            return dc;
        }
        private static DataTable getdata(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = My.conn;
            SqlConnection con = new SqlConnection(strConnString);

            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        private static DataTable FillDatastatic(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, My.conn);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }
            }
            catch (Exception ex)
            {
            }
            return dtc;
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



        #region ASSESMENT WISE
        //=================================
        internal static string get_marks(string Session_id, string Class_id, string Admission_no, string Term_id, string Assessment_id, string Subject_id, string Branch_id, string Subject_type)
        {
            Dictionary<string, object> dc1 = get_marks_calculation(Session_id, Branch_id, Class_id, Term_id, Assessment_id, Subject_id);
            string Grade_System_Id = (String)dc1["Grade_System_Id"];
            //string Maximum_Marks = (String)dc1["Maximum_Marks"];
            string Cut_Off_Percentage = (String)dc1["Cut_Off_Percentage"];
            string Calculation_Type = (String)dc1["Calculation_Type"];
            string Is_Advanced_Advanced_Setting = (String)dc1["Is_Advanced_Advanced_Setting"];
            string Consider_best = (String)dc1["Consider_best"];
            string Pass_criteria = (String)dc1["Pass_criteria"];
            string Is_Mandatory_to_pass = (String)dc1["Is_Mandatory_to_pass"];

            string assesment_weightage = get_assesment_weightage(Session_id, Branch_id, Class_id, Term_id, Assessment_id, Subject_id);
            string Maximum_Marks = get_assesment_max_marks(Session_id, Branch_id, Class_id, Term_id, Assessment_id, Subject_id);

            string[] stringSeparators = new string[] { "/" };
            string[] arr = assesment_weightage.Split(stringSeparators, StringSplitOptions.None);
            assesment_weightage = arr[0];
            string grade_system_id = arr[1];




            double total_number = 0; string IsChar = ""; bool isNum = true; bool isNumTrue = false;
            #region OVERALL CALCULATION

            if (Calculation_Type == "Sum")
            {
                #region SUM CALCULATION
                if (Is_Advanced_Advanced_Setting == "True" || Is_Advanced_Advanced_Setting == "1")
                {
                    if (Pass_criteria == "None")
                    {
                        string query = "select top " + Consider_best + " em.Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1 and Is_character=0 order by cast(em.Marks as float) desc";
                        SqlCommand cmd = new SqlCommand(query);
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                                if (isNum == true)
                                {
                                    total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                    isNumTrue = true;
                                }
                                else
                                {
                                    IsChar = dt.Rows[0]["Marks"].ToString();
                                }
                            }
                            if (isNumTrue == true)
                            {
                                total_number = total_number / dt.Rows.Count;
                            }
                        }
                    }
                    else
                    {
                        string[] stringSeparatorss = new string[] { " OF " };
                        string[] arrs = Pass_criteria.Split(stringSeparatorss, StringSplitOptions.None);
                        string First = arrs[0];
                        string second = arrs[1];



                        string query = "select top " + Consider_best + " em.Marks,essl.Maximum_Marks,essl.Cut_Off_Percentage from Exam_marks em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id and em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1  and Is_character=0 order by cast(em.Marks as float) desc";
                        SqlCommand cmd = new SqlCommand(query);
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            double persent_of_obtain_mark = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                                if (isNum == true)
                                {
                                    isNumTrue = true;
                                    persent_of_obtain_mark = My.toDouble(dt.Rows[i]["Marks"].ToString()) / My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString()) * 100;
                                    if (persent_of_obtain_mark >= My.toDouble(dt.Rows[i]["Cut_Off_Percentage"].ToString()))
                                    {
                                        total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                    }
                                }
                                else
                                {
                                    IsChar = dt.Rows[0]["Marks"].ToString();
                                }
                            }
                            if (isNumTrue == true)
                            {
                                total_number = total_number / dt.Rows.Count;
                            }
                        }
                    }
                }
                else
                {
                    ///=========================IS  NOT ADVANCE
                    ///
                    string query = "select em.Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "'";

                    SqlCommand cmd = new SqlCommand(query);
                    DataTable dt = FillDatastatic(query);
                    if (dt.Rows.Count == 0)
                    {
                        total_number = 0;
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                            if (isNum == true)
                            {
                                ///============================
                                total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                isNumTrue = true;
                            }
                            else
                            {
                                IsChar = dt.Rows[0]["Marks"].ToString();
                            }
                        }
                        if (isNumTrue == true)
                        {
                            total_number = total_number / dt.Rows.Count;
                        }
                    }
                }

                #endregion
            }
            else
            {
                #region AVERAGE CALCULATION
                if (Is_Advanced_Advanced_Setting == "True" || Is_Advanced_Advanced_Setting == "1")
                {
                    if (Pass_criteria == "None")
                    {
                        string query = "select top " + Consider_best + " em.Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1  and Is_character=0 order by cast(em.Marks as float) desc";
                        SqlCommand cmd = new SqlCommand(query);
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                                if (isNum == true)
                                {
                                    total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                    isNumTrue = true;
                                }
                                else
                                {
                                    IsChar = dt.Rows[0]["Marks"].ToString();
                                }
                            }
                            if (isNumTrue == true)
                            {
                                total_number = total_number / dt.Rows.Count;
                            }
                        }
                    }
                    else
                    {
                        string[] stringSeparators1 = new string[] { " OF " };
                        string[] arr1 = Pass_criteria.Split(stringSeparators1, StringSplitOptions.None);
                        string First = arr1[0];
                        string second = arr1[1];



                        string query = "select top " + Consider_best + " em.Marks,essl.Maximum_Marks,essl.Cut_Off_Percentage from Exam_marks em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id and em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1 and Is_character=0 order by cast(em.Marks as float) desc";
                        SqlCommand cmd = new SqlCommand(query);
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            double persent_of_obtain_mark = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                                if (isNum == true)
                                {
                                    isNumTrue = true;
                                    persent_of_obtain_mark = My.toDouble(dt.Rows[i]["Marks"].ToString()) / My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString()) * 100;
                                    if (persent_of_obtain_mark >= My.toDouble(dt.Rows[i]["Cut_Off_Percentage"].ToString()))
                                    {
                                        total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                    }
                                }
                                else
                                {
                                    IsChar = dt.Rows[0]["Marks"].ToString();
                                }
                            }
                            if (isNumTrue == true)
                            {
                                total_number = total_number / dt.Rows.Count;
                            }
                        }
                    }
                }
                else
                {
                    ///=========================IS  NOT ADVANCE
                    string query = "select em.Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "'";
                    SqlCommand cmd = new SqlCommand(query);
                    DataTable dt = FillDatastatic(query);
                    if (dt.Rows.Count == 0)
                    {
                        total_number = 0;
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                            if (isNum == true)
                            {
                                total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                isNumTrue = true;
                            }
                            else
                            {
                                IsChar = dt.Rows[0]["Marks"].ToString();
                            }
                        }
                        if (isNumTrue == true)
                        {
                            total_number = total_number / dt.Rows.Count;
                        }
                    }
                }
                #endregion
            }
            #endregion

            //=obt/mm*wtg
            Dictionary<string, object> dc2 = get_grade_logic(Session_id, Branch_id, grade_system_id);
            string With_Decimal = (String)dc2["With_Decimal"];
            string Without_Decimal = (String)dc2["Without_Decimal"];
            string Round_up = (String)dc2["Round_up"];
            string Round_down = (String)dc2["Round_down"];
            string Half_Round_Up = (String)dc2["Half_Round_Up"];
            string Half_Round_Down = (String)dc2["Half_Round_Down"];
            string With_Decimal_Per = (String)dc2["With_Decimal_Per"];
            string Without_Decimal_Per = (String)dc2["Without_Decimal_Per"];

            string Round_up_Per = (String)dc2["Round_up_Per"];
            string Round_down_Per = (String)dc2["Round_down_Per"];
            string Half_Round_Up_Per = (String)dc2["Half_Round_Up_Per"];
            string Half_Round_Down_Per = (String)dc2["Half_Round_Down_Per"];
            string Round_Percentage_Checked = (String)dc2["Round_Percentage_Checked"];
            string Maximum_numbe_decimal = (String)dc2["Maximum_numbe_decimal"];

            string Input_Type = (String)dc2["Input_Type"];
            string Output = (String)dc2["Output"];



            double final_marks = total_number / My.toDouble(Maximum_Marks) * My.toDouble(assesment_weightage);
            if (final_marks.ToString() == "∞" || final_marks.ToString() == "NaN")
            {
                final_marks = 0;
            }
            string round_off_value = Math.Round(My.toDouble(final_marks)).ToString(); //get_round_off_value(With_Decimal, Without_Decimal, Round_up, Round_down, Half_Round_Up, Half_Round_Down, With_Decimal_Per, Without_Decimal_Per, Round_up_Per, Round_down_Per, Half_Round_Up_Per, Half_Round_Down_Per, Round_Percentage_Checked, Maximum_numbe_decimal, final_marks);

            if (Subject_type == "Scholastic")
            {
                if (Output == "Marks")  //MARKS
                {
                    if (isNumTrue == true)
                    {
                        string marks_type = "Marks";
                        return round_off_value + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                    }
                    else
                    {
                        string marks_type = "Marks";
                        return IsChar + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                    }
                }
                else   // GRADE
                {
                    if (isNumTrue == true)
                    {
                        string marks_type = "Grade";
                        string grade = get_grade_subj(Session_id, Branch_id, grade_system_id, total_number, Maximum_Marks, assesment_weightage);   // Grade of a Assessment
                        return grade + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                    }
                    else
                    {
                        string marks_type = "Grade";
                        string grade = get_grade_subj(Session_id, Branch_id, grade_system_id, total_number, Maximum_Marks, assesment_weightage);   // Grade of a Assessment
                        return IsChar + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                    }
                }
            }
            else
            {
                if (isNumTrue == true)
                {
                    string marks_type = "Grade";
                    string grade = get_grade_subj(Session_id, Branch_id, grade_system_id, total_number, Maximum_Marks, assesment_weightage);   // Grade of a Assessment 
                    return grade + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                }
                else
                {
                    string marks_type = "Grade";
                    string grade = get_grade_subj(Session_id, Branch_id, grade_system_id, total_number, Maximum_Marks, assesment_weightage);   // Grade of a Assessment
                    return IsChar + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                }
            }
        }

        private static bool valid_amount(string p)
        {
            try
            {
                Convert.ToDouble(p);
                return true;
            }
            catch
            {
                return false;
            }
        }



        private static string get_assesment_max_marks(string Session_id, string Branch_id, string Class_id, string Term_id, string Assessment_id, string Subject_id)
        {
            string query = "select Maximum_Marks from Exam_Subject_Sub_Level where Session_Id='" + Session_id + "' and Branch_Id='" + Branch_id + "' and Istatus=1 and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + Assessment_id + " and Class_id=" + Class_id + " and Subject_id=" + Subject_id + "";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Maximum_Marks"].ToString();
            }
        }






        private static string get_assesment_weightage(string Session_id, string Branch_id, string Class_id, string Term_id, string Assessment_id, string Subject_id)
        {
            string query = "select Maximum_Marks,Grade_System_Id from Exam_Assessment_Details where Session_Id='" + Session_id + "' and Branch_Id='" + Branch_id + "' and Istatus=1 and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + Assessment_id + " and Class_id=" + Class_id + "";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Maximum_Marks"].ToString() + "/" + dt.Rows[0]["Grade_System_Id"].ToString();
            }
        }

        private static string get_assesment_weightage_ss(string Session_id, string Branch_id, string Class_id, string Term_id, string Assessment_id, string Subject_id, string Subject_Sub_Level_Id)
        {
            string query = "select Maximum_Marks,Grade_System_Id from Exam_Subject_Sub_Level where Session_Id='" + Session_id + "' and Branch_Id='" + Branch_id + "' and Istatus=1 and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + Assessment_id + " and Class_id=" + Class_id + " and Subject_Sub_Level_Id='" + Subject_Sub_Level_Id + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Maximum_Marks"].ToString() + "/" + dt.Rows[0]["Grade_System_Id"].ToString();
            }
        }

        public static Dictionary<string, object> get_marks_calculation(string Session_id, string Branch_id, string Class_id, string Term_id, string Assessment_id, string Subject_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = " select * from dbo.[Exam_Assessment_Subject_Mapping_Details] where Session_Id='" + Session_id + "' and Branch_Id='" + Branch_id + "' and Istatus=1 and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + Assessment_id + " and Subject_id=" + Subject_id + " and Class_id=" + Class_id + "";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = getdata(cmd);

            if (dt.Rows.Count == 0)
            {
                dc["Grade_System_Id"] = "0";
                dc["Maximum_Marks"] = "0";
                dc["Cut_Off_Percentage"] = "0";
                dc["Calculation_Type"] = "0";
                dc["Is_Advanced_Advanced_Setting"] = "0";
                dc["Consider_best"] = "0";
                dc["Pass_criteria"] = "0";
                dc["Is_Mandatory_to_pass"] = "0";
            }
            else
            {
                dc["Grade_System_Id"] = dt.Rows[0]["Grade_System_Id"].ToString();
                dc["Maximum_Marks"] = dt.Rows[0]["Maximum_Marks"].ToString();
                dc["Cut_Off_Percentage"] = dt.Rows[0]["Cut_Off_Percentage"].ToString();
                dc["Calculation_Type"] = dt.Rows[0]["Calculation_Type"].ToString();
                dc["Is_Advanced_Advanced_Setting"] = dt.Rows[0]["Is_Advanced_Advanced_Setting"].ToString();
                dc["Consider_best"] = dt.Rows[0]["Consider_best"].ToString();
                dc["Pass_criteria"] = dt.Rows[0]["Pass_criteria"].ToString();
                dc["Is_Mandatory_to_pass"] = dt.Rows[0]["Is_Mandatory_to_pass"].ToString();
            }
            return dc;

        }

        private static string get_grade_subj(string session_id, string branch_id, string Grade_System_Id, double final_marks, string Maximum_Marks, string assesment_weightage)
        {
            double final_markss = 0;
            if (100 > My.toDouble(Maximum_Marks))
            {
                final_markss = Math.Round(final_marks);
            }
            else
            {
                final_markss = (My.toDouble(final_marks) / My.toDouble(Maximum_Marks)) * 100;
            }
            final_marks = Math.Round(final_markss);
            string returN = "FAIL";
            string query = "select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + Grade_System_Id + " and  Session_Id=" + session_id + " and Branch_Id='" + branch_id + "'";
            DataTable dt = FillDatastatic(query);
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

                    if (final_marks >= Lower_Range && final_marks <= Upper_Range)
                    {
                        returN = dt.Rows[i]["Grade"].ToString();
                    }
                }
                return returN;
            }
        }

        #endregion

        internal static string get_personality_grade(string Session_id, string Class_id, string Branch_id, string final_marks)
        {
            double final_markss = Math.Round(My.toDouble(final_marks));
            string returN = "";
            string query = "select t3.* from Exam_Grade_System t1 join Exam_Grade_System_Mapping_with_Class t2 on t1.Grade_System_Id=t2.Grade_System_Id join Exam_Grade_System_Range_Grade t3 on t1.Grade_System_Id=t3.Grade_System_Id and t1.Session_Id=t3.Session_Id and t1.Branch_Id=t3.Branch_Id where t1.Scholastic_Co_scholastic='Co-Scholastic' and t3.Branch_Id='" + Branch_id + "' and t3.Session_Id='" + Session_id + "' and t2.Course_id='" + Class_id + "'";
            DataTable dt = FillDatastatic(query);
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

                    if (My.toDouble(final_markss) >= Lower_Range && My.toDouble(final_markss) <= Upper_Range)
                    {
                        returN = dt.Rows[i]["Grade"].ToString();
                    }
                }
                return returN;
            }
        }



        #region RoundOFF
        //============================================
        private static string get_round_off_value(string With_Decimal, string Without_Decimal, string Round_up, string Round_down, string Half_Round_Up, string Half_Round_Down, string With_Decimal_Per, string Without_Decimal_Per, string Round_up_Per, string Round_down_Per, string Half_Round_Up_Per, string Half_Round_Down_Per, string Round_Percentage_Checked, string Maximum_numbe_decimal, double final_marks)
        {
            string roundOff_value = "";
            #region WITHDECIMAL
            if (With_Decimal == "True" || With_Decimal == "1")
            {
                if (Round_up == "True" || Half_Round_Up == "True" || Round_up == "1" || Half_Round_Up == "1")
                {
                    roundOff_value = get_round_up(final_marks, Maximum_numbe_decimal);
                }
                else //if (Round_down == "True" || Half_Round_Down == "True")
                {
                    roundOff_value = get_round_down(final_marks, Maximum_numbe_decimal);
                }
            }
            else
            {
                if (Round_up == "True" || Half_Round_Up == "True" || Round_up == "1" || Half_Round_Up == "1")
                {
                    double celling_value = Math.Ceiling(final_marks);
                    roundOff_value = celling_value.ToString();
                }
                else //if (Round_down == "True" || Half_Round_Down == "True")
                {
                    double celling_value = Math.Floor(final_marks);
                    roundOff_value = celling_value.ToString();
                }
            }
            #endregion



            return roundOff_value;
        }



        private static string get_round_up(double final_marks, string Maximum_numbe_decimal)
        {
            string find_zero = "0";
            if (Maximum_numbe_decimal == "1")
            {
                find_zero = "0";
            }
            else if (Maximum_numbe_decimal == "2")
            {
                find_zero = "00";
            }
            else if (Maximum_numbe_decimal == "3")
            {
                find_zero = "000";
            }
            else if (Maximum_numbe_decimal == "4")
            {
                find_zero = "0000";
            }
            else if (Maximum_numbe_decimal == "5")
            {
                find_zero = "00000";
            }

            string[] stringSeparatorss = new string[] { "." };
            string[] arrs = final_marks.ToString("0.00").Split(stringSeparatorss, StringSplitOptions.None);
            string without_decimal = arrs[0];
            string dacimal_value = arrs[1];

            double result = 0;
            string results = "0";

            if (dacimal_value == "10" || dacimal_value == "20" || dacimal_value == "30" || dacimal_value == "40" || dacimal_value == "50" || dacimal_value == "60" || dacimal_value == "70" || dacimal_value == "80" || dacimal_value == "90" || dacimal_value == "100")
            {
                results = final_marks.ToString("0." + find_zero);
            }
            else
            {
                if (My.toDouble(dacimal_value) > 0)
                {
                    double dx135 = final_marks + 0.05;
                    result = Math.Round(dx135, 1);
                    results = result.ToString("0." + find_zero);
                }
                else
                {
                    results = final_marks.ToString("0." + find_zero);
                }
            }

            return results;
        }

        private static string get_round_down(double final_marks, string Maximum_numbe_decimal)
        {
            string find_zero = "0";
            if (Maximum_numbe_decimal == "1")
            {
                find_zero = "0";
            }
            else if (Maximum_numbe_decimal == "2")
            {
                find_zero = "00";
            }
            else if (Maximum_numbe_decimal == "3")
            {
                find_zero = "000";
            }
            else if (Maximum_numbe_decimal == "4")
            {
                find_zero = "0000";
            }
            else if (Maximum_numbe_decimal == "5")
            {
                find_zero = "00000";
            }


            string[] stringSeparatorss = new string[] { "." };
            string[] arrs = final_marks.ToString("0.00").Split(stringSeparatorss, StringSplitOptions.None);
            string without_decimal = arrs[0];
            string dacimal_value = arrs[1];

            double result = 0;
            string results = "0";

            if (dacimal_value == "10" || dacimal_value == "20" || dacimal_value == "30" || dacimal_value == "40" || dacimal_value == "50" || dacimal_value == "60" || dacimal_value == "70" || dacimal_value == "80" || dacimal_value == "90" || dacimal_value == "100")
            {
                results = final_marks.ToString("0." + find_zero);
            }
            else
            {
                if (My.toDouble(dacimal_value) > 0)
                {
                    double dx135 = final_marks - 0.05;
                    result = Math.Round(dx135, 2);
                    results = result.ToString("0." + find_zero);
                }
                else
                {
                    results = final_marks.ToString("0." + find_zero);
                }
            }
            return results;
        }
        #endregion






        #region MaskSSS
        internal static string get_marks_obt(string Session_id, string Class_id, string Admission_no, string Term_id, string Assessment_id, string Subject_id, string Branch_id, string Subject_type, string Subject_Sub_Level_Id)
        {
            Dictionary<string, object> dc1 = get_marks_calculation(Session_id, Branch_id, Class_id, Term_id, Assessment_id, Subject_id);
            string Grade_System_Id = (String)dc1["Grade_System_Id"];
            //string Maximum_Marks = (String)dc1["Maximum_Marks"];
            string Cut_Off_Percentage = (String)dc1["Cut_Off_Percentage"];
            string Calculation_Type = (String)dc1["Calculation_Type"];
            string Is_Advanced_Advanced_Setting = (String)dc1["Is_Advanced_Advanced_Setting"];
            string Consider_best = (String)dc1["Consider_best"];
            string Pass_criteria = (String)dc1["Pass_criteria"];
            string Is_Mandatory_to_pass = (String)dc1["Is_Mandatory_to_pass"];

            string assesment_weightage = get_assesment_weightage_ss(Session_id, Branch_id, Class_id, Term_id, Assessment_id, Subject_id, Subject_Sub_Level_Id);
            string Maximum_Marks = get_assesment_max_marks(Session_id, Branch_id, Class_id, Term_id, Assessment_id, Subject_id);

            string[] stringSeparators = new string[] { "/" };
            string[] arr = assesment_weightage.Split(stringSeparators, StringSplitOptions.None);
            assesment_weightage = arr[0];
            string grade_system_id = arr[1];




            double total_number = 0; string IsChar = ""; bool isNum = true; bool isNumTrue = false;
            #region OVERALL CALCULATION

            if (Calculation_Type == "Sum")
            {
                #region SUM CALCULATION
                if (Is_Advanced_Advanced_Setting == "True" || Is_Advanced_Advanced_Setting == "1")
                {
                    if (Pass_criteria == "None")
                    {
                        string query = "select top " + Consider_best + " em.Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1 and Is_character=0 order by cast(em.Marks as float) desc";
                        SqlCommand cmd = new SqlCommand(query);
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                                if (isNum == true)
                                {
                                    total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                    isNumTrue = true;
                                }
                                else
                                {
                                    IsChar = dt.Rows[0]["Marks"].ToString();
                                }
                            }
                            if (isNumTrue == true)
                            {
                                total_number = total_number / dt.Rows.Count;
                            }
                        }
                    }
                    else
                    {
                        string[] stringSeparatorss = new string[] { " OF " };
                        string[] arrs = Pass_criteria.Split(stringSeparatorss, StringSplitOptions.None);
                        string First = arrs[0];
                        string second = arrs[1];



                        string query = "select top " + Consider_best + " em.Marks,essl.Maximum_Marks,essl.Cut_Off_Percentage from Exam_marks em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id and em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1  and Is_character=0 order by cast(em.Marks as float) desc";
                        SqlCommand cmd = new SqlCommand(query);
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            double persent_of_obtain_mark = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                                if (isNum == true)
                                {
                                    isNumTrue = true;
                                    persent_of_obtain_mark = My.toDouble(dt.Rows[i]["Marks"].ToString()) / My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString()) * 100;
                                    if (persent_of_obtain_mark >= My.toDouble(dt.Rows[i]["Cut_Off_Percentage"].ToString()))
                                    {
                                        total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                    }
                                }
                                else
                                {
                                    IsChar = dt.Rows[0]["Marks"].ToString();
                                }
                            }
                            if (isNumTrue == true)
                            {
                                total_number = total_number / dt.Rows.Count;
                            }
                        }
                    }
                }
                else
                {
                    ///=========================IS  NOT ADVANCE
                    ///
                    string query = "select em.Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "' and em.Subject_activity='" + Subject_Sub_Level_Id + "'";
                    SqlCommand cmd = new SqlCommand(query);
                    DataTable dt = FillDatastatic(query);
                    if (dt.Rows.Count == 0)
                    {
                        total_number = 0;
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                            if (isNum == true)
                            {
                                ///============================
                                total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                isNumTrue = true;
                            }
                            else
                            {
                                IsChar = dt.Rows[0]["Marks"].ToString();
                            }
                        }
                        if (isNumTrue == true)
                        {
                            total_number = total_number / dt.Rows.Count;
                        }
                    }
                }

                #endregion
            }
            else
            {
                #region AVERAGE CALCULATION
                if (Is_Advanced_Advanced_Setting == "True" || Is_Advanced_Advanced_Setting == "1")
                {
                    if (Pass_criteria == "None")
                    {
                        string query = "select top " + Consider_best + " em.Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1  and Is_character=0 order by cast(em.Marks as float) desc";
                        SqlCommand cmd = new SqlCommand(query);
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                                if (isNum == true)
                                {
                                    total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                    isNumTrue = true;
                                }
                                else
                                {
                                    IsChar = dt.Rows[0]["Marks"].ToString();
                                }
                            }
                            if (isNumTrue == true)
                            {
                                total_number = total_number / dt.Rows.Count;
                            }
                        }
                    }
                    else
                    {
                        string[] stringSeparators1 = new string[] { " OF " };
                        string[] arr1 = Pass_criteria.Split(stringSeparators1, StringSplitOptions.None);
                        string First = arr1[0];
                        string second = arr1[1];



                        string query = "select top " + Consider_best + " em.Marks,essl.Maximum_Marks,essl.Cut_Off_Percentage from Exam_marks em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id and em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1 and Is_character=0 order by cast(em.Marks as float) desc";
                        SqlCommand cmd = new SqlCommand(query);
                        DataTable dt = FillDatastatic(query);
                        if (dt.Rows.Count == 0)
                        {
                            total_number = 0;
                        }
                        else
                        {
                            double persent_of_obtain_mark = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                                if (isNum == true)
                                {
                                    isNumTrue = true;
                                    persent_of_obtain_mark = My.toDouble(dt.Rows[i]["Marks"].ToString()) / My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString()) * 100;
                                    if (persent_of_obtain_mark >= My.toDouble(dt.Rows[i]["Cut_Off_Percentage"].ToString()))
                                    {
                                        total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                    }
                                }
                                else
                                {
                                    IsChar = dt.Rows[0]["Marks"].ToString();
                                }
                            }
                            if (isNumTrue == true)
                            {
                                total_number = total_number / dt.Rows.Count;
                            }
                        }
                    }
                }
                else
                {
                    ///=========================IS  NOT ADVANCE
                    string query = "select em.Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "' and em.Subject_activity='" + Subject_Sub_Level_Id + "'";
                    SqlCommand cmd = new SqlCommand(query);
                    DataTable dt = FillDatastatic(query);
                    if (dt.Rows.Count == 0)
                    {
                        total_number = 0;
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            isNum = valid_amount(dt.Rows[i]["Marks"].ToString());
                            if (isNum == true)
                            {
                                total_number = total_number + My.toDouble(dt.Rows[i]["Marks"].ToString());
                                isNumTrue = true;
                            }
                            else
                            {
                                IsChar = dt.Rows[0]["Marks"].ToString();
                            }
                        }
                        if (isNumTrue == true)
                        {
                            total_number = total_number / dt.Rows.Count;
                        }
                    }
                }
                #endregion
            }
            #endregion

            //=obt/mm*wtg
            Dictionary<string, object> dc2 = get_grade_logic(Session_id, Branch_id, grade_system_id);
            string With_Decimal = (String)dc2["With_Decimal"];
            string Without_Decimal = (String)dc2["Without_Decimal"];
            string Round_up = (String)dc2["Round_up"];
            string Round_down = (String)dc2["Round_down"];
            string Half_Round_Up = (String)dc2["Half_Round_Up"];
            string Half_Round_Down = (String)dc2["Half_Round_Down"];
            string With_Decimal_Per = (String)dc2["With_Decimal_Per"];
            string Without_Decimal_Per = (String)dc2["Without_Decimal_Per"];

            string Round_up_Per = (String)dc2["Round_up_Per"];
            string Round_down_Per = (String)dc2["Round_down_Per"];
            string Half_Round_Up_Per = (String)dc2["Half_Round_Up_Per"];
            string Half_Round_Down_Per = (String)dc2["Half_Round_Down_Per"];
            string Round_Percentage_Checked = (String)dc2["Round_Percentage_Checked"];
            string Maximum_numbe_decimal = (String)dc2["Maximum_numbe_decimal"];

            string Input_Type = (String)dc2["Input_Type"];
            string Output = (String)dc2["Output"];



            double final_marks = total_number / My.toDouble(Maximum_Marks) * My.toDouble(assesment_weightage);
            if (final_marks.ToString() == "∞" || final_marks.ToString() == "NaN")
            {
                final_marks = 0;
            }
            string round_off_value = Math.Round(final_marks).ToString(); // get_round_off_value(With_Decimal, Without_Decimal, Round_up, Round_down, Half_Round_Up, Half_Round_Down, With_Decimal_Per, Without_Decimal_Per, Round_up_Per, Round_down_Per, Half_Round_Up_Per, Half_Round_Down_Per, Round_Percentage_Checked, Maximum_numbe_decimal, final_marks);

            if (Subject_type == "Scholastic")
            {
                if (Output == "Marks")  //MARKS
                {
                    if (isNumTrue == true)
                    {
                        string marks_type = "Marks";
                        return round_off_value + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                    }
                    else
                    {
                        string marks_type = "Marks";
                        return IsChar + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                    }
                }
                else   // GRADE
                {
                    if (isNumTrue == true)
                    {
                        string marks_type = "Grade";
                        string grade = get_grade_subj(Session_id, Branch_id, grade_system_id, total_number, Maximum_Marks, assesment_weightage);   // Grade of a Assessment
                        return grade + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                    }
                    else
                    {
                        string marks_type = "Grade";
                        string grade = get_grade_subj(Session_id, Branch_id, grade_system_id, total_number, Maximum_Marks, assesment_weightage);   // Grade of a Assessment
                        return IsChar + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                    }
                }
            }
            else
            {
                if (isNumTrue == true)
                {
                    string marks_type = "Grade";
                    string grade = get_grade_subj(Session_id, Branch_id, grade_system_id, total_number, Maximum_Marks, assesment_weightage);   // Grade of a Assessment 
                    return grade + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                }
                else
                {
                    string marks_type = "Grade";
                    string grade = get_grade_subj(Session_id, Branch_id, grade_system_id, total_number, Maximum_Marks, assesment_weightage);   // Grade of a Assessment
                    return IsChar + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                }
            }
        }
        #endregion
    }
}