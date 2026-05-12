using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode.Exam
{
    public class Auto_mark_update
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




            double total_number = 0; string IsChar = ""; bool isNum = true; bool isNumTrue = false; double max_number = 0;
            #region OVERALL CALCULATION

            if (Calculation_Type == "Sum")
            {
                #region SUM CALCULATION
                if (Is_Advanced_Advanced_Setting == "True" || Is_Advanced_Advanced_Setting == "1")
                {
                    if (Pass_criteria == "None")
                    {
                        string query = "select top " + Consider_best + " em.Marks,essl.Maximum_Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1 and Is_character=0 order by cast(em.Marks as float) desc";
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
                                    max_number = max_number + My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
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
                                max_number = max_number / dt.Rows.Count;
                                Maximum_Marks = max_number.ToString();
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
                    string query = "select em.Marks,essl.Maximum_Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "'";
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
                                max_number = max_number + My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
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
                            max_number = max_number / dt.Rows.Count;
                            Maximum_Marks = max_number.ToString();
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
                        string query = "select top " + Consider_best + " em.Marks,essl.Maximum_Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1  and Is_character=0 order by cast(em.Marks as float) desc";
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
                                    max_number = max_number + My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
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
                                max_number = max_number / dt.Rows.Count;
                                Maximum_Marks = max_number.ToString();
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
                    string query = "select em.Marks,essl.Maximum_Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "'";
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
                                max_number = max_number + My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
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
                            max_number = max_number / dt.Rows.Count;
                            Maximum_Marks = max_number.ToString();
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

            string round_off_value = final_marks.ToString("0.0");

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
                        string grade = get_grade_subj(Session_id, Branch_id, Grade_System_Id, total_number, Maximum_Marks, assesment_weightage);   // Grade of a Assessment
                        return grade + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                    }
                    else
                    {
                        string marks_type = "Grade";
                        string grade = get_grade_subj(Session_id, Branch_id, Grade_System_Id, total_number, Maximum_Marks, assesment_weightage);   // Grade of a Assessment
                        return IsChar + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                    }
                }
            }
            else
            {
                if (isNumTrue == true)
                {
                    string marks_type = "Grade";
                    string grade = get_grade(Session_id, Branch_id, Grade_System_Id, total_number);   // Grade of a Assessment
                    return grade + "/" + assesment_weightage + "/" + marks_type + "/" + round_off_value + "/" + Maximum_Marks;
                }
                else
                {
                    string marks_type = "Grade";
                    string grade = get_grade(Session_id, Branch_id, Grade_System_Id, total_number);   // Grade of a Assessment
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


        ////////////////=====================UPDATED


        internal static void update_auto_marks(string Session_id, string Class_id, string Admission_no, string Term_id, string Assessment_id, string Subject_id, string Branch_id, string Subject_type, double markpercent)
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




            double total_number = 0; string IsChar = ""; bool isNum = true; bool isNumTrue = false; double max_number = 0;
            #region OVERALL CALCULATION

            if (Calculation_Type == "Sum")
            {
                #region SUM CALCULATION
                if (Is_Advanced_Advanced_Setting == "True" || Is_Advanced_Advanced_Setting == "1")
                {
                    if (Pass_criteria == "None")
                    {
                        string query = "select top " + Consider_best + " em.Id,em.Marks,essl.Maximum_Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1 and Is_character=0 order by cast(em.Marks as float) desc";
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
                                    max_number = max_number + My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
                                    isNumTrue = true;
                                }
                                else
                                {
                                    IsChar = dt.Rows[0]["Marks"].ToString();
                                    if (IsChar != "AB")
                                    {
                                        max_number = My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
                                        double obtMarks = (My.toDouble(max_number) * markpercent) / 100;
                                        string updateqry = "update Exam_marks set Marks='" + obtMarks.ToString("0.0") + "',Old_marks='" + IsChar + "' where Id=" + dt.Rows[i]["Id"].ToString() + "";
                                        My.exeSql(updateqry);
                                    }
                                }
                            }
                            
                        }
                    }
                    else
                    {
                        string[] stringSeparatorss = new string[] { " OF " };
                        string[] arrs = Pass_criteria.Split(stringSeparatorss, StringSplitOptions.None);
                        string First = arrs[0];
                        string second = arrs[1];



                        string query = "select top " + Consider_best + " em.Id,em.Marks,essl.Maximum_Marks,essl.Cut_Off_Percentage from Exam_marks em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id and em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1  and Is_character=0 order by cast(em.Marks as float) desc";
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
                                    if (IsChar != "AB")
                                    {
                                        max_number = My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
                                        double obtMarks = (My.toDouble(max_number) * markpercent) / 100;
                                        string updateqry = "update Exam_marks set Marks='" + obtMarks.ToString("0.0") + "',Old_marks='" + IsChar + "' where Id=" + dt.Rows[i]["Id"].ToString() + "";
                                        My.exeSql(updateqry);
                                    }
                                }
                            } 
                        }
                    }
                }
                else
                {
                    ///=========================IS  NOT ADVANCE
                    string query = "select em.Id,em.Marks,essl.Maximum_Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "'";
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
                                max_number = max_number + My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
                                isNumTrue = true;
                            }
                            else
                            {
                                IsChar = dt.Rows[0]["Marks"].ToString();
                                if (IsChar != "AB")
                                {
                                    max_number = My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
                                    double obtMarks = (My.toDouble(max_number) * markpercent) / 100;
                                    string updateqry = "update Exam_marks set Marks='" + obtMarks.ToString("0.0") + "',Old_marks='" + IsChar + "' where Id=" + dt.Rows[i]["Id"].ToString() + "";
                                    My.exeSql(updateqry);
                                }
                            }
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
                        string query = "select top " + Consider_best + " em.Id,em.Marks,essl.Maximum_Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1  and Is_character=0 order by cast(em.Marks as float) desc";
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
                                    max_number = max_number + My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
                                    isNumTrue = true;
                                }
                                else
                                {
                                    IsChar = dt.Rows[0]["Marks"].ToString();
                                    if (IsChar != "AB")
                                    {
                                        max_number = My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
                                        double obtMarks = (My.toDouble(max_number) * markpercent) / 100;
                                        string updateqry = "update Exam_marks set Marks='" + obtMarks.ToString("0.0") + "',Old_marks='" + IsChar + "' where Id=" + dt.Rows[i]["Id"].ToString() + "";
                                        My.exeSql(updateqry);
                                    }
                                }
                            } 
                        }
                    }
                    else
                    {
                        string[] stringSeparators1 = new string[] { " OF " };
                        string[] arr1 = Pass_criteria.Split(stringSeparators1, StringSplitOptions.None);
                        string First = arr1[0];
                        string second = arr1[1];



                        string query = "select top " + Consider_best + " em.Id,em.Marks,essl.Maximum_Marks,essl.Cut_Off_Percentage from Exam_marks em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id and em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject" + Subject_id + " and em.Admission_no='" + Admission_no + "' and essl.Select_Data=1 and Is_character=0 order by cast(em.Marks as float) desc";
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
                                    if (IsChar != "AB")
                                    {
                                        max_number = My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
                                        double obtMarks = (My.toDouble(max_number) * markpercent) / 100;
                                        string updateqry = "update Exam_marks set Marks='" + obtMarks.ToString("0.0") + "',Old_marks='" + IsChar + "' where Id=" + dt.Rows[i]["Id"].ToString() + "";
                                        My.exeSql(updateqry);
                                    }
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
                    string query = "select em.Id,em.Marks,essl.Maximum_Marks from dbo.[Exam_marks] em join Exam_Subject_Sub_Level essl on em.Session_id=essl.Session_id and em.Branch_id=essl.Branch_id and em.Class_id=essl.Class_id and em.Term=essl.Exam_Term_Id and em.Assessment=essl.Assessment_Id and em.Subject=essl.Subject_id  and  em.Subject_activity=essl.Subject_Sub_Level_Id where  em.Session_Id='" + Session_id + "' and em.Branch_Id='" + Branch_id + "' and em.Term=" + Term_id + " and em.Assessment=" + Assessment_id + " and em.Class_id=" + Class_id + " and em.Subject=" + Subject_id + " and em.Admission_no='" + Admission_no + "'";
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
                                max_number = max_number + My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
                                isNumTrue = true;
                            }
                            else
                            {
                                IsChar = dt.Rows[0]["Marks"].ToString();
                                if (IsChar != "AB")
                                {
                                    max_number = My.toDouble(dt.Rows[i]["Maximum_Marks"].ToString());
                                    double obtMarks = (My.toDouble(max_number) * markpercent) / 100;
                                    string updateqry = "update Exam_marks set Marks='" + obtMarks.ToString("0.0") + "',Old_marks='" + IsChar + "' where Id=" + dt.Rows[i]["Id"].ToString() + "";
                                    My.exeSql(updateqry);
                                }
                            }
                        } 
                    }
                }
                #endregion
            }
            #endregion
        }
    }
}