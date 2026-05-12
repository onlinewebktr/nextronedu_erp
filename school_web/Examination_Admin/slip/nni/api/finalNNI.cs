using school_web.AppCode;
using school_web.AppCode.Exam;
using school_web.Examination_Admin.slip.final.api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.Examination_Admin.slip.nni.api
{
    public class finalNNI
    {
        public class finalRpClss
        {
            internal static string get_ttl_mark_of_a_subject_final_year(string subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII)
            {
                string system_grade = get_system_grade_id(Session_id, Branch_id, Class_id);
                string query = "select sum(isnull(TRY_CAST(t1.Marks AS float),0)) as Ttl_mark_for_term1,sum(isnull(TRY_CAST(t2.Marks AS float),0)) as Ttl_mark_for_termII from Exam_temp_assesment_total_no t1 join Exam_temp_assesment_total_no_term_II t2 on t1.Session_id=t2.Session_id and t1.Branch_id=t2.Branch_id and t1.Class_id=t2.Class_id and t1.Subject_id=t2.Subject_id and t1.Admission_no=t2.Admission_no and t1.Exam_termwise_assesment_id=t2.Exam_termwise_assesment_id join Exam_Assessment_Details t3 on t1.Session_Id=t3.Session_Id and t1.Branch_Id=t3.Branch_Id and t1.Term_id=t3.Exam_Term_Id and t1.Class_id=t3.Class_id and t1.Assessment_id=t3.Assessment_Id where t1.Session_id=" + Session_id + " and  t1.Admission_no='" + Admission_no + "' and t1.Subject_id=" + subject_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + "";
                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    return "0/0";
                }
                else
                {
                    Dictionary<string, object> dc2 = get_grade_logic(Session_id, Branch_id, system_grade);
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




                    string termI_grade = get_grade(Session_id, Branch_id, system_grade, My.toDouble(dt.Rows[0]["Ttl_mark_for_term1"].ToString()), Class_id);
                    string termII_grade = get_grade(Session_id, Branch_id, system_grade, My.toDouble(dt.Rows[0]["Ttl_mark_for_termII"].ToString()), Class_id);
                    double termI_termIi_average_percent = (My.toDouble(dt.Rows[0]["Ttl_mark_for_term1"].ToString()) + My.toDouble(dt.Rows[0]["Ttl_mark_for_termII"].ToString()));


                    double termI_termIi_average_percent_for_grade = (My.toDouble(dt.Rows[0]["Ttl_mark_for_term1"].ToString()) + My.toDouble(dt.Rows[0]["Ttl_mark_for_termII"].ToString())) / 2;
                    string termI_termII_grade = get_grade(Session_id, Branch_id, system_grade, termI_termIi_average_percent_for_grade, Class_id);

                    string TermIattandance = get_attandance_details(Session_id, Branch_id, Class_id, Term_idI, Admission_no);    //Total Class/Present Class/Present attendance percent
                    string TermIIattandance = get_attandance_details(Session_id, Branch_id, Class_id, Term_idII, Admission_no);    //Total Class/Present Class/Present attendance percent


                    string overall_mark_and_percent = get_overall_mrk(Session_id, Branch_id, Class_id, Admission_no);  //MARKS/PERCENT
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = overall_mark_and_percent.Split(stringSeparatorss, StringSplitOptions.None);
                    string Overall_mark = arrs[0];
                    string Overall_percent = arrs[1];


                    string round_off_value_full_persentage = get_round_off_value(With_Decimal, Without_Decimal, Round_up, Round_down, Half_Round_Up, Half_Round_Down, With_Decimal_Per, Without_Decimal_Per, Round_up_Per, Round_down_Per, Half_Round_Up_Per, Half_Round_Down_Per, Round_Percentage_Checked, Maximum_numbe_decimal, My.toDouble(Overall_percent));



                    double final_marks_term1 = My.toDouble(Math.Round(My.toDouble(dt.Rows[0]["Ttl_mark_for_term1"].ToString()), 0, MidpointRounding.AwayFromZero).ToString());
                    double final_marks_term2 = My.toDouble(Math.Round(My.toDouble(dt.Rows[0]["Ttl_mark_for_termII"].ToString()), 0, MidpointRounding.AwayFromZero).ToString());


                    return final_marks_term1.ToString() + "/" + final_marks_term2.ToString() + "/" + termI_grade + "/" + termII_grade + "/" + termI_termIi_average_percent + "/" + termI_termII_grade + "/" + TermIattandance + "/" + TermIIattandance + "/" + Overall_mark + "/" + round_off_value_full_persentage;
                }
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

            private static string get_g_p(string session_id, string branch_id, string Grade_System_Id, double final_marks, string Class_id)
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
                            returN = dt.Rows[i]["Credits"].ToString();
                        }
                    }
                    return returN;
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


            private static string get_attandance_details(string session_id, string branch_id, string class_id, string term_id, string admission_no)
            {
                string returN = "0/0/0";
                string querys = "select * from Exam_Term_Wise_Attendance where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Branch_id='" + branch_id + "' and Admission_no='" + admission_no + "' and Exam_Term_Id='" + term_id + "'";
                DataTable dt = FillDatastatic(querys);
                if (dt.Rows.Count == 0)
                {
                    return returN;
                }
                else
                {
                    try
                    {
                        if (dt.Rows[0]["Total_no_of_class"].ToString() == "")
                        {
                            string query = "select *,(select top 1 No_of_class_Attendance from Exam_Term_Wise_Attendance where Session_Id=td.Session_Id and Branch_Id=td.Branch_Id and Class_id=td.Class_id and Exam_Term_Id=td.Exam_Term_Id and Admission_no='" + admission_no + "') as Persent_attandance from Exam_Term_Details td where Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Class_id=" + class_id + " and Exam_Term_Id=" + term_id + "";
                            DataTable dtx = FillDatastatic(query);
                            if (dtx.Rows.Count == 0)
                            {
                                return returN;
                            }
                            else
                            {
                                if (dtx.Rows[0]["Persent_attandance"].ToString() == "")
                                {
                                    returN = dtx.Rows[0]["No_of_Class"].ToString() + "/0/0";
                                }
                                else
                                {
                                    double att_persent = (My.toDouble(dtx.Rows[0]["Persent_attandance"].ToString()) / My.toDouble(dtx.Rows[0]["No_of_Class"].ToString())) * 100;
                                    returN = dtx.Rows[0]["No_of_Class"].ToString() + "/" + dtx.Rows[0]["Persent_attandance"].ToString() + "/" + att_persent.ToString("0.00");
                                }

                                return returN;
                            }
                        }
                        else
                        {
                            if (dt.Rows[0]["No_of_class_Attendance"].ToString() == "")
                            {
                                returN = dt.Rows[0]["Total_no_of_class"].ToString() + "/0/0";
                            }
                            else
                            {
                                double att_persent = (My.toDouble(dt.Rows[0]["No_of_class_Attendance"].ToString()) / My.toDouble(dt.Rows[0]["Total_no_of_class"].ToString())) * 100;
                                returN = dt.Rows[0]["Total_no_of_class"].ToString() + "/" + dt.Rows[0]["No_of_class_Attendance"].ToString() + "/" + att_persent.ToString("0.00");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string query = "select *,(select top 1 No_of_class_Attendance from Exam_Term_Wise_Attendance where Session_Id=td.Session_Id and Branch_Id=td.Branch_Id and Class_id=td.Class_id and Exam_Term_Id=td.Exam_Term_Id and Admission_no='" + admission_no + "') as Persent_attandance from Exam_Term_Details td where Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Class_id=" + class_id + " and Exam_Term_Id=" + term_id + "";
                        DataTable dtxx = FillDatastatic(query);
                        if (dtxx.Rows.Count == 0)
                        {
                            return returN;
                        }
                        else
                        {
                            if (dtxx.Rows[0]["Persent_attandance"].ToString() == "")
                            {
                                returN = dtxx.Rows[0]["No_of_Class"].ToString() + "/0/0";
                            }
                            else
                            {
                                double att_persent = (My.toDouble(dtxx.Rows[0]["Persent_attandance"].ToString()) / My.toDouble(dtxx.Rows[0]["No_of_Class"].ToString())) * 100;
                                returN = dtxx.Rows[0]["No_of_Class"].ToString() + "/" + dtxx.Rows[0]["Persent_attandance"].ToString() + "/" + att_persent.ToString("0.00");
                            }
                            return returN;
                        }
                    }

                    return returN;
                }
            }

            private static string get_overall_mrk(string Session_id, string Branch_id, string Class_id, string Admission_no)
            {
                string overall_marks = "0/0";
                string querys = "select sum(isnull(convert(float, t1.Marks),0)) as Ttl_mark_for_termI,sum(isnull(convert(float, t2.Marks),0)) as Ttl_mark_for_termII,sum(isnull(convert(float, t1.Full_marks),0)) as Ttl_full_mark_for_termI,sum(isnull(convert(float, t2.Full_marks),0)) as Ttl_full_mark_for_termII from Exam_temp_assesment_total_no t1 join Exam_temp_assesment_total_no_term_II t2 on t1.Session_id=t2.Session_id and t1.Branch_id=t2.Branch_id and t1.Class_id=t2.Class_id and t1.Subject_id=t2.Subject_id and t1.Admission_no=t2.Admission_no and t1.Exam_termwise_assesment_id=t2.Exam_termwise_assesment_id join Exam_Assessment_Details t3 on t1.Session_Id=t3.Session_Id and t1.Branch_Id=t3.Branch_Id and t1.Term_id=t3.Exam_Term_Id and t1.Class_id=t3.Class_id and t1.Assessment_id=t3.Assessment_Id where t1.Session_id=" + Session_id + " and  t1.Admission_no='" + Admission_no + "' and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " ";
                DataTable dtx = FillDatastatic(querys);
                if (dtx.Rows.Count != 0)
                {
                    double overall_marks_f = (My.toDouble(dtx.Rows[0]["Ttl_mark_for_termI"].ToString()) + My.toDouble(dtx.Rows[0]["Ttl_mark_for_termII"].ToString())) / 2;
                    double overall_full_marks_f = (My.toDouble(dtx.Rows[0]["Ttl_full_mark_for_termI"].ToString()) + My.toDouble(dtx.Rows[0]["Ttl_full_mark_for_termII"].ToString())) / 2;
                    double overall_percent = (overall_marks_f / overall_full_marks_f * 100);


                    overall_marks = overall_marks_f + "/" + overall_percent;
                }
                return overall_marks;
            }

            internal static string get_final_year_setting(string session_id, string class_id, string branch_id)
            {
                string exam_setting = get_exam_setting(session_id, branch_id);  // Is Attendance Show 
                string exam_settings = examSettingFinal.get_exam_settings(session_id, branch_id, class_id);  // Settings 
                return exam_setting + "/" + exam_settings;
            }
            private static string get_exam_setting(string session_id, string branch_id)
            {
                string returN = "hidden";
                string query = "select * from Exam_report_card_setting where  Session_Id=" + session_id + " and Branch_Id='" + branch_id + "'";
                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    return returN;
                }
                else
                {
                    if (dt.Rows[0]["Is_attandance_show"].ToString() == "True")
                    {
                        returN = "show";
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
                //double dx135 = final_marks - 0.05;
                //double result = Math.Round(dx135, 1);
                //string results = result.ToString("0." + find_zero);
                //return results; 


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
            private static string get_system_grade_id(string session_id, string branch_id, string class_id)
            {
                string grade_id = "";
                string querys = "select Grade_System_Id from Exam_Term_Details where Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Class_id=" + class_id + " order by Sequence_No asc";
                DataTable dtx = FillDatastatic(querys);
                if (dtx.Rows.Count != 0)
                {
                    grade_id = dtx.Rows[0]["Grade_System_Id"].ToString();
                }
                return grade_id;
            }


            internal static string get_systm_grd_id(string Subject_id, string Session_id, string Class_id, string Branch_id, string Term_id, string Assessment_Id)
            {
                string gradeid = "0";
                try
                {
                    string query = "select top 1 Grade_System_Id from dbo.[Exam_Subject_Sub_Level] where Subject_id=" + Subject_id + " and Session_Id=" + Session_id + " and Branch_Id='" + Branch_id + "' and Exam_Term_Id='" + Term_id + "' and Assessment_Id='" + Assessment_Id + "' order by Grade_System_Id asc";
                    DataTable dt = FillDatastatic(query);
                    if (dt.Rows.Count == 0)
                    {
                        gradeid = "0";
                    }
                    else
                    {
                        gradeid = dt.Rows[0]["Grade_System_Id"].ToString();
                    }
                    return gradeid;
                }
                catch (Exception ex)
                {
                    return gradeid;
                }
            }

            internal static string get_percent_remark(string overall_percent)
            {
                string returN = "NA";
                string query = "select * from Exam_percentage_remark";
                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    return returN;
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double Lower_Range = My.toDouble(dt.Rows[i]["Low_Percent"].ToString());
                        double Upper_Range = My.toDouble(dt.Rows[i]["Upper_Percent"].ToString());
                        if (My.toDouble(overall_percent) >= Lower_Range && My.toDouble(overall_percent) <= Upper_Range)
                        {
                            returN = dt.Rows[i]["Remark"].ToString();
                        }
                    }
                    return returN;
                }
            }


            //====================================FinalYear
            internal static string get_overall_total_marks_final(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
            {
                Dictionary<string, object> dc1 = get_term_marks_calculation(Session_id, Branch_id, Class_id, Term_id);
                string Grade_System_Id = (String)dc1["Grade_System_Id"];
                Dictionary<string, object> dc2 = get_grade_logic(Session_id, Branch_id, Grade_System_Id);
                string Output = (String)dc2["Output"];
                string result = "";
                string marksType = "";
                string obt_and_full_mark = get_obt_and_full_marks_final_year(Session_id, Branch_id, Admission_no, Term_id, Class_id); // Obtain Mark/Full Mark/OverAll Percentage


                if (Output == "Marks")
                {
                    marksType = "PERCENTAGE";
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = obt_and_full_mark.Split(stringSeparatorss, StringSplitOptions.None);
                    string overall_obt_marks = arrs[0];
                    string overall_full_marks = arrs[1];
                    string overall_percent = arrs[2];

                    string overall_obt_marks_not_averge = arrs[3];
                    string overall_full_marks_not_averge = arrs[4];

                    string grade = get_grade(Session_id, Branch_id, Grade_System_Id, My.toDouble(overall_percent));   // Grade of a subject
                    result = obt_and_full_mark + "/" + marksType + "/" + grade + "/" + overall_obt_marks_not_averge + "/" + overall_full_marks_not_averge;
                }
                else
                {
                    marksType = "GRADE";
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = obt_and_full_mark.Split(stringSeparatorss, StringSplitOptions.None);
                    string overall_obt_marks = arrs[0];
                    string overall_full_marks = arrs[1];
                    string overall_percent = arrs[2];
                    string overall_obt_marks_not_averge = arrs[3];
                    string overall_full_marks_not_averge = arrs[4];
                    string grade = get_grade(Session_id, Branch_id, Grade_System_Id, My.toDouble(overall_percent));   // Grade of a subject
                    result = overall_obt_marks + "/" + overall_full_marks + "/" + grade + "/" + marksType + "/" + grade + "/" + overall_obt_marks_not_averge + "/" + overall_full_marks_not_averge;
                }

                return result;
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

            private static string get_obt_and_full_marks_final_year(string session_id, string branch_id, string admission_no, string term_id, string class_id)
            {
                string returN = "0/0/0";
                string query = "select ((isnull(sum(convert(float, Term_i_mark)),0) + isnull(sum(convert(float, Term_ii_mark)),0))/2) as total_obt_mark,((isnull(sum(convert(float, Term_i_full_mark)),0) + isnull(sum(convert(float, Term_ii_full_mark)),0))/2) as total_full_mark,((isnull(sum(convert(float, Term_i_mark)),0) + isnull(sum(convert(float, Term_ii_mark)),0))) as total_obt_mark_not_avrge,((isnull(sum(convert(float, Term_i_full_mark)),0) + isnull(sum(convert(float, Term_ii_full_mark)),0))) as total_full_mark_not_avrge from (select isnull(TRY_CAST(t1.Marks AS float),0) as Term_i_mark,t1.Full_marks as Term_i_full_mark,isnull(TRY_CAST(t2.Marks AS float),0) as Term_ii_mark,t2.Full_marks as Term_ii_full_mark from Exam_temp_assesment_total_no t1 join Exam_temp_assesment_total_no_term_II t2 on t1.Session_id=t2.Session_id and t1.Branch_id=t2.Branch_id and t1.Class_id=t2.Class_id and t1.Subject_id=t2.Subject_id and t1.Admission_no=t2.Admission_no and t1.Exam_termwise_assesment_id=t2.Exam_termwise_assesment_id join Exam_Assessment_Details t3 on t1.Session_Id=t3.Session_Id and t1.Branch_Id=t3.Branch_Id and t1.Term_id=t3.Exam_Term_Id and t1.Class_id=t3.Class_id and t1.Assessment_id=t3.Assessment_Id where t1.Session_id=" + session_id + " and  t1.Admission_no='" + admission_no + "' and t1.Branch_id='" + branch_id + "' and t1.Class_id=" + class_id + ") t";
                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    return returN;
                }
                else
                {
                    double overall_percentage = (My.toDouble(dt.Rows[0]["total_obt_mark"].ToString()) / My.toDouble(dt.Rows[0]["total_full_mark"].ToString())) * 100;
                    return dt.Rows[0]["total_obt_mark"].ToString() + "/" + dt.Rows[0]["total_full_mark"].ToString() + "/" + overall_percentage.ToString("0.00") + "/" + dt.Rows[0]["total_obt_mark_not_avrge"].ToString() + "/" + dt.Rows[0]["total_full_mark_not_avrge"].ToString();
                }
            }
            private static string get_grade(string session_id, string branch_id, string Grade_System_Id, double final_marks)
            {
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

            private static string get_gp_overall(string session_id, string branch_id, string Grade_System_Id, double final_marks)
            {
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
                            returN = dt.Rows[i]["Credits"].ToString();
                        }
                    }
                    return returN;
                }
            }

            internal static string get_overall_mrk(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
            {
                Dictionary<string, object> dc1 = get_term_marks_calculation(Session_id, Branch_id, Class_id, Term_id);
                string Grade_System_Id = (String)dc1["Grade_System_Id"];

                string overall_marks = "0/0/0";
                string querys = "select sum(isnull(convert(float, Marks),0)) as Ttl_mark,sum(isnull(convert(float, Full_mark),0)) as Ttl_full_mark from Exam_temp_mark_groupwise where Session_id=" + Session_id + " and Branch_id='" + Branch_id + "' and Class_id=" + Class_id + "  and Admission_no='" + Admission_no + "'";
                DataTable dtx = FillDatastatic(querys);
                if (dtx.Rows.Count != 0)
                {
                    double overall_marks_f = My.toDouble(dtx.Rows[0]["Ttl_mark"].ToString());
                    double overall_full_marks_f = My.toDouble(dtx.Rows[0]["Ttl_full_mark"].ToString());
                    double overall_percent = (overall_marks_f / overall_full_marks_f * 100);
                    string Overall_grade = get_grade(Session_id, Branch_id, Grade_System_Id, My.toDouble(overall_percent), Class_id);
                    overall_marks = overall_marks_f + "/" + overall_percent + "/" + overall_full_marks_f + "/" + Overall_grade;
                }
                return overall_marks; 
            } 
        }

         
    }
}