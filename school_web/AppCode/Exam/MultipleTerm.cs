using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode.Exam
{
    public class MultipleTerm
    {
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


            double total_number = 0; string IsChar = ""; bool isNum = true; bool isNumTrue = false; double practical = 0; double theory = 0;
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
                    string query = "select em.Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "'  order by essl.Sequence_No asc";
                    SqlCommand cmd = new SqlCommand(query);
                    DataTable dt = FillDatastatic(query);
                    if (dt.Rows.Count == 0)
                    {
                        total_number = 0;
                    }
                    else
                    {
                        if (dt.Rows.Count == 1)
                        {
                            theory = My.toDouble(dt.Rows[0]["Marks"].ToString());
                        }
                        else
                        {
                            practical = My.toDouble(dt.Rows[0]["Marks"].ToString());
                            try
                            {
                                theory = My.toDouble(dt.Rows[1]["Marks"].ToString());
                            }
                            catch (Exception ex)
                            {
                            }
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

            string practicalMarks = get_round_off_value(With_Decimal, Without_Decimal, Round_up, Round_down, Half_Round_Up, Half_Round_Down, With_Decimal_Per, Without_Decimal_Per, Round_up_Per, Round_down_Per, Half_Round_Up_Per, Half_Round_Down_Per, Round_Percentage_Checked, Maximum_numbe_decimal, practical);

            string theoryMarks = get_round_off_value(With_Decimal, Without_Decimal, Round_up, Round_down, Half_Round_Up, Half_Round_Down, With_Decimal_Per, Without_Decimal_Per, Round_up_Per, Round_down_Per, Half_Round_Up_Per, Half_Round_Down_Per, Round_Percentage_Checked, Maximum_numbe_decimal, theory);

            string marks_type = "Marks";
            if (Subject_type == "Scholastic")
            {
                if (Output == "Marks")  //MARKS
                {

                }
            }
            return practicalMarks + "/" + assesment_weightage + "/" + marks_type + "/" + practicalMarks + "/" + Maximum_Marks + "/" + theoryMarks;

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
        #endregion



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




        //=========================================
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





            double total_number = 0;
            #region OVERALL CALCULATION

            string first_term = get_first_term(session_id, branch_id, class_id);
            string table_name = "Exam_temp_assesment_total_no";
            if (first_term == term_id)
            {
                table_name = "Exam_temp_assesment_total_no";
            }

            if (Calculation_Type == "Sum")
            {
                #region SUM CALCULATION
                if (Is_Advanced_Advanced_Setting == "True" || Is_Advanced_Advanced_Setting == "1")
                {
                    if (Pass_criteria == "None")
                    {
                        string query = "select top " + Consider_best + " eta.Marks from " + table_name + " eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'  and ad.Select_Data=1 order by cast(eta.Marks as float) desc";
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


                        string query = "select top " + Consider_best + " eta.Marks from " + table_name + " eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'  and ad.Select_Data=1 order by cast(eta.Marks as float) desc";
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
                    string query = "select eta.Marks from " + table_name + " eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "' ";
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
                #endregion
            }
            else
            {
                #region AVERAGE CALCULATION
                if (Is_Advanced_Advanced_Setting == "True" || Is_Advanced_Advanced_Setting == "1")
                {
                    if (Pass_criteria == "None")
                    {
                        string query = "select top " + Consider_best + " eta.Marks from " + table_name + " eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'  and ad.Select_Data=1 order by cast(eta.Marks as float) desc";
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


                        string query = "select top " + Consider_best + " eta.Marks,etd.Maximum_Marks,etd.Cut_Off_Percentage from " + table_name + " eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'  and ad.Select_Data=1 order by cast(eta.Marks as float) desc";
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

                    string query = "select  eta.Marks from " + table_name + " eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'";

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
                    string grade = get_grade(session_id, branch_id, Grade_System_Id, final_marks);   // Grade of a subject
                    string obt_and_full_mark = get_obt_and_full_marks(session_id, branch_id, admission_no, term_id, class_id); // Obtain Mark/Full Mark/OverAll Percentage

                    string[] stringSeparatorsss = new string[] { "/" };
                    string[] arrs1 = obt_and_full_mark.Split(stringSeparatorsss, StringSplitOptions.None);
                    string obt_mark = arrs1[0];
                    string full_mark = arrs1[1];
                    string overall_persentage = arrs1[2];

                    string round_off_value_full_persentage = get_round_off_value(With_Decimal, Without_Decimal, Round_up, Round_down, Half_Round_Up, Half_Round_Down, With_Decimal_Per, Without_Decimal_Per, Round_up_Per, Round_down_Per, Half_Round_Up_Per, Half_Round_Down_Per, Round_Percentage_Checked, Maximum_numbe_decimal, My.toDouble(overall_persentage));
                    obt_and_full_mark = obt_mark + "/" + full_mark + "/" + round_off_value_full_persentage;

                    string exam_setting = get_exam_setting(session_id, branch_id);  // Is Attendance Show
                    string attandance = get_attandance_details(session_id, branch_id, class_id, term_id, admission_no);    //Total Class/Present Class/Present attendance percent
                    string remarkss = get_remarks(session_id, branch_id, class_id, term_id, admission_no);  // REMARKS 
                    string exam_settings = Exam_setting.get_exam_settings(session_id, branch_id, class_id);  // Settings


                    return final_marks.ToString() + "/" + grade + "/" + obt_and_full_mark + "/" + exam_setting + "/" + attandance + "/" + Grade_head_text + "/" + remarkss + "/" + exam_settings;
                }
                else
                {
                    string Grade_head_text = "OVERALL GRADE";
                    string grade = get_grade(session_id, branch_id, Grade_System_Id, final_marks);   // Grade of a subject
                    string obt_and_full_mark = get_obt_and_full_marks(session_id, branch_id, admission_no, term_id, class_id); // Obtain Mark/Full Mark/OverAll Percentage

                    string[] stringSeparatorsss = new string[] { "/" };
                    string[] arrs1 = obt_and_full_mark.Split(stringSeparatorsss, StringSplitOptions.None);
                    string obt_mark = arrs1[0];
                    string full_mark = arrs1[1];
                    string overall_persentage = arrs1[2];

                    string Overall_grade = get_grade(session_id, branch_id, Grade_System_Id, My.toDouble(overall_persentage));   // Grade of a subject
                    overall_persentage = Overall_grade;
                    obt_mark = "hidden";

                    obt_and_full_mark = obt_mark + "/" + full_mark + "/" + overall_persentage;

                    string exam_setting = get_exam_setting(session_id, branch_id);  // Is Attendance Show
                    string attandance = get_attandance_details(session_id, branch_id, class_id, term_id, admission_no);    //Total Class/Present Class/Present attendance percent
                    string remarkss = get_remarks(session_id, branch_id, class_id, term_id, admission_no);  // REMARKS
                    string hide_last_grade_clumn = "hidden";
                    string exam_settings = Exam_setting.get_exam_settings(session_id, branch_id, class_id);  // Is SpecialNote & QR Show
                    return grade + "/" + hide_last_grade_clumn + "/" + obt_and_full_mark + "/" + exam_setting + "/" + attandance + "/" + Grade_head_text + "/" + remarkss + "/" + exam_settings;
                }
            }
            else
            {
                string Grade_head_text = "OVERALL GRADE";
                string grade = get_grade(session_id, branch_id, Grade_System_Id, final_marks);   // Grade of a subject
                string obt_and_full_mark = get_obt_and_full_marks(session_id, branch_id, admission_no, term_id, class_id); // Obtain Mark/Full Mark/OverAll Percentage

                string[] stringSeparatorsss = new string[] { "/" };
                string[] arrs1 = obt_and_full_mark.Split(stringSeparatorsss, StringSplitOptions.None);
                string obt_mark = arrs1[0];
                string full_mark = arrs1[1];
                string overall_persentage = arrs1[2];

                string Overall_grade = get_grade(session_id, branch_id, Grade_System_Id, My.toDouble(overall_persentage));   // Grade of a subject
                overall_persentage = Overall_grade;
                obt_mark = "hidden";

                obt_and_full_mark = obt_mark + "/" + full_mark + "/" + overall_persentage;
                string exam_setting = get_exam_setting(session_id, branch_id);  // Is Attendance Show
                string attandance = get_attandance_details(session_id, branch_id, class_id, term_id, admission_no);    //Total Class/Present Class/Present attendance percent
                string remarkss = get_remarks(session_id, branch_id, class_id, term_id, admission_no);  // REMARKS
                string hide_last_grade_clumn = "hidden";
                string exam_settings = Exam_setting.get_exam_settings(session_id, branch_id, class_id);  // Is SpecialNote & QR Show 
                return grade + "/" + hide_last_grade_clumn + "/" + obt_and_full_mark + "/" + exam_setting + "/" + attandance + "/" + Grade_head_text + "/" + remarkss + "/" + exam_settings;

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



        private static string get_first_term(string session_id, string branch_id, string class_id)
        {
            string trmsi = "";
            string querys = "select Short_Name,Grade_System_Id,Exam_Term_Id from Exam_Term_Details where Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Class_id=" + class_id + " order by Sequence_No asc";
            DataTable dtx = FillDatastatic(querys);
            if (dtx.Rows.Count != 0)
            {
                trmsi = dtx.Rows[0]["Exam_Term_Id"].ToString();
            }
            return trmsi;
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


        private static string get_attandance_details(string session_id, string branch_id, string class_id, string term_id, string admission_no)
        {
            string returN = "0/0/0";
            string query = "select *,(select top 1 No_of_class_Attendance from Exam_Term_Wise_Attendance where Session_Id=td.Session_Id and Branch_Id=td.Branch_Id and Class_id=td.Class_id and Exam_Term_Id=td.Exam_Term_Id and Admission_no='" + admission_no + "') as Persent_attandance from Exam_Term_Details td where Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Class_id=" + class_id + " and Exam_Term_Id=" + term_id + "";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                if (dt.Rows[0]["Persent_attandance"].ToString() == "")
                {
                    returN = dt.Rows[0]["No_of_Class"].ToString() + "/0/0";
                }
                else
                {
                    double att_persent = (My.toDouble(dt.Rows[0]["Persent_attandance"].ToString()) / My.toDouble(dt.Rows[0]["No_of_Class"].ToString())) * 100;
                    returN = dt.Rows[0]["No_of_Class"].ToString() + "/" + dt.Rows[0]["Persent_attandance"].ToString() + "/" + att_persent.ToString("0.00");
                }
                return returN;
            }
        }
        #endregion

        private static string get_remarks(string session_id, string branch_id, string class_id, string term_id, string admission_no)
        {
            string returN = "hidden";
            string query = "select * from Exam_Commentary_Remark_Term_Wise_Entry where Session_id=" + session_id + " and Branch_id='" + branch_id + "' and Class_id=" + class_id + " and Exam_Term_Id=" + term_id + " and Admission_no='" + admission_no + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = dt.Rows[0]["Remarks"].ToString();
                return returN;
            }
        }

        private static string get_grade_subj(string session_id, string branch_id, string Grade_System_Id, double final_marks, string Maximum_Marks, string assesment_weightage)
        {
            double final_markss = (My.toDouble(final_marks) / My.toDouble(Maximum_Marks)) * 100;
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

        My mycode = new My();
        private static string get_obt_and_full_marks(string session_id, string branch_id, string admission_no, string term_id, string class_id)
        {
            string first_term = get_first_term(session_id, branch_id, class_id);
            string table_name = "Exam_temp_assesment_total_no";
            if (first_term == term_id)
            {
                table_name = "Exam_temp_assesment_total_no";
            }

            double full_marks = 0;
            double obt_marks = 0;
            string returN = "0/0/0";
            string query = @"select sum(isnull(convert(float, Full_marks),0)) as total_full_mark from " + table_name + " where  Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Admission_no='" + admission_no + @"' and Marks_type='Theory' and Marks>0;

                                       select sum(isnull(convert(float, Marks),0)) as total_obt_mark from " + table_name + " where  Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Admission_no='" + admission_no + "'  and Marks>0";

            DataSet ds = Fill_Data_set(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[0];
                full_marks = My.toDouble(dtTemp.Rows[0]["total_full_mark"].ToString());
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[1];
                obt_marks = My.toDouble(dtTemp.Rows[0]["total_obt_mark"].ToString());
            }

            double overall_percentage = (obt_marks / full_marks) * 100;
            return obt_marks + "/" + full_marks + "/" + overall_percentage.ToString("0.00");




            // string query = "select sum(isnull(convert(float, Marks),0)) as total_obt_mark,sum(isnull(convert(float, Full_marks),0)) as total_full_mark from " + table_name + " where  Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Admission_no='" + admission_no + "' and Marks_type='Theory' and Marks>0";
            //DataTable dt = FillDatastatic(query);
            //if (dt.Rows.Count == 0)
            //{
            //    return returN;
            //}
            //else
            //{
            //    double overall_percentage = (My.toDouble(dt.Rows[0]["total_obt_mark"].ToString()) / My.toDouble(dt.Rows[0]["total_full_mark"].ToString())) * 100;
            //    return dt.Rows[0]["total_obt_mark"].ToString() + "/" + dt.Rows[0]["total_full_mark"].ToString() + "/" + overall_percentage.ToString("0.00");
            //}
        }

        private static DataSet Fill_Data_set(string query)
        {
            DataSet dtc = new DataSet();
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


        internal static string get_overall_total_marks(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            Dictionary<string, object> dc1 = get_term_marks_calculation(Session_id, Branch_id, Class_id, Term_id);
            string Grade_System_Id = (String)dc1["Grade_System_Id"];

            Dictionary<string, object> dc2 = get_grade_logic(Session_id, Branch_id, Grade_System_Id);
            string Output = (String)dc2["Output"];

            string result = "";
            string marksType = "";
            string obt_and_full_mark = get_obt_and_full_marks(Session_id, Branch_id, Admission_no, Term_id, Class_id); // Obtain Mark/Full Mark/OverAll Percentage


            if (Output == "Marks")
            {
                marksType = "PERCENTAGE";
                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = obt_and_full_mark.Split(stringSeparatorss, StringSplitOptions.None);
                string overall_obt_marks = arrs[0];
                string overall_full_marks = arrs[1];
                string overall_percent = arrs[2];
                string grade = get_grade(Session_id, Branch_id, Grade_System_Id, My.toDouble(overall_percent));   // Grade of a subject
                result = obt_and_full_mark + "/" + marksType + "/" + grade;
            }
            else
            {
                marksType = "GRADE";
                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = obt_and_full_mark.Split(stringSeparatorss, StringSplitOptions.None);
                string overall_obt_marks = arrs[0];
                string overall_full_marks = arrs[1];
                string overall_percent = arrs[2];
                string grade = get_grade(Session_id, Branch_id, Grade_System_Id, My.toDouble(overall_percent));   // Grade of a subject
                result = overall_obt_marks + "/" + overall_full_marks + "/" + grade + "/" + marksType + "/" + grade;
            }


            return result;
        }
    }
}