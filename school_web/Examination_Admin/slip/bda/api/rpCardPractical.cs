using school_web.AppCode;
using school_web.Examination_Admin.slip.general.api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace school_web.Examination_Admin.slip.bda.api
{
    public class rpCardPractical
    {
        My mycode = new My();
        internal bool chk_grade_exist(string Grade_System_Id, string branchid, string sessionid, string grade)
        {
            DataTable dt = mycode.FillData("Select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + Grade_System_Id + " and Branch_Id=" + branchid + " and Session_Id=" + sessionid + "  and Grade='" + grade + "'");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool chk_grade_exist_edit(string Grade_System_Id, string branchid, string sessionid, string grade, string id)
        {
            DataTable dt = mycode.FillData("Select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + Grade_System_Id + " and Branch_Id=" + branchid + " and Session_Id=" + sessionid + "  and Grade='" + grade + "' and Id!=" + id + "");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal object get_class_id_from_examterm(string Exam_Term_Id, string branchid)
        {
            DataTable dt = mycode.FillData("Select  Class_id from Exam_Term_Details where Exam_Term_Id='" + Exam_Term_Id + "' and Branch_Id='" + branchid + "'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal string get_Assessment_Subject_Id(string sessionid, string branchid, string classid, string examterm, string assessment, string subjectid)
        {
            DataTable dt = mycode.FillData("Select  Assessment_Subject_Id from Exam_Assessment_Subject_Mapping_Details where Session_Id=" + sessionid + " and Branch_Id=" + branchid + " and Class_id=" + classid + " and Exam_Term_Id=" + examterm + " and Assessment_Id=" + assessment + " and Subject_id=" + subjectid + " ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }


        #region ASSESMENT WISE
        //=================================//
        internal static string get_marks(string Session_id, string Class_id, string Admission_no, string Term_id, string Assessment_id, string Subject_id, string Branch_id, string Subject_type, string isMaxMActivity)
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
                        assesment_weightage = "0";
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


            if (isMaxMActivity == "0")
            {
                if (total_number == 0)
                {
                    assesment_weightage = getSubWaitage(Session_id, Branch_id, Class_id, Term_id, Assessment_id, Subject_id);
                }
            }
            else
            {
                assesment_weightage = getSubSactWaitage(Session_id, Branch_id, Class_id, Term_id, Assessment_id, Subject_id);
            }

            double final_marks = total_number / My.toDouble(Maximum_Marks) * My.toDouble(assesment_weightage);
            if (final_marks.ToString() == "∞" || final_marks.ToString() == "NaN")
            {
                final_marks = 0;
            }
            string round_off_value = get_round_off_value(With_Decimal, Without_Decimal, Round_up, Round_down, Half_Round_Up, Half_Round_Down, With_Decimal_Per, Without_Decimal_Per, Round_up_Per, Round_down_Per, Half_Round_Up_Per, Half_Round_Down_Per, Round_Percentage_Checked, Maximum_numbe_decimal, final_marks);

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

        private static string getSubWaitage(string session_id, string branch_id, string class_id, string term_id, string assessment_id, string subject_id)
        {
            string query = "select Maximum_Marks from Exam_Assessment_Subject_Mapping_Details where Session_Id='" + session_id + "' and Branch_Id='" + branch_id + "' and Istatus=1 and Exam_Term_Id=" + term_id + " and Assessment_Id=" + assessment_id + " and Class_id=" + class_id + " and Subject_id=" + subject_id + "";
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
        private static string getSubSactWaitage(string session_id, string branch_id, string class_id, string term_id, string assessment_id, string subject_id)
        {
            string query = "select Maximum_Marks from Exam_Subject_Sub_Level where Session_Id='" + session_id + "' and Branch_Id='" + branch_id + "' and Istatus=1 and Exam_Term_Id=" + term_id + " and Assessment_Id=" + assessment_id + " and Class_id=" + class_id + " and Subject_id=" + subject_id + "";
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
                return "0/0";
            }
            else
            {
                return dt.Rows[0]["Maximum_Marks"].ToString() + "/" + dt.Rows[0]["Grade_System_Id"].ToString();
            }
        }

        public static Dictionary<string, object> get_marks_calculation(string Session_id, string Branch_id, string Class_id, string Term_id, string Assessment_id, string Subject_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = " select * from dbo.[Exam_Subject_Sub_Level] where Session_Id='" + Session_id + "' and Branch_Id='" + Branch_id + "' and Istatus=1 and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + Assessment_id + " and Subject_id=" + Subject_id + " and Class_id=" + Class_id + "";
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





            double total_number = 0; double full_number = 0;
            #region OVERALL CALCULATION

            string first_term = get_first_term(session_id, branch_id, class_id);
            string table_name = "Exam_temp_assesment_total_no_term_II";
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
                    string query = "select eta.Marks,eta.Full_marks from " + table_name + " eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "' ";
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

                    string query = "select  eta.Marks,eta.Full_marks from " + table_name + " eta join Exam_Term_Details etd on eta.Session_id=etd.Session_id and eta.Branch_id=etd.Branch_id and eta.Class_id=etd.Class_id and eta.Term_id=etd.Exam_Term_Id join Exam_Assessment_Details ad on eta.Session_id=ad.Session_id and eta.Branch_id=ad.Branch_id and eta.Class_id=ad.Class_id and eta.Term_id=ad.Exam_Term_Id and eta.Assessment_id=ad.Assessment_Id  where  eta.Session_Id='" + session_id + "' and eta.Branch_Id='" + branch_id + "' and eta.Term_id=" + term_id + " and eta.Class_id=" + class_id + "  and eta.Subject_id=" + subject_id + " and eta.Admission_no='" + admission_no + "'";
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
                        full_number = full_number / dt.Rows.Count;
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
                    string Grade_head_text = "TOTAL";
                    string grade = generalExamSetting.get_grade_of_a_subject_with_bg(session_id, branch_id, Grade_System_Id, final_marks, full_number, class_id);   // Grade of a subject
                    string[] stringSeparator = new string[] { "~" };
                    string[] arr = grade.Split(stringSeparator, StringSplitOptions.None);
                    grade = arr[0];
                    string gradeBG = arr[1];

                    string obt_and_full_mark = get_obt_and_full_marks(session_id, branch_id, admission_no, term_id, class_id); // Obtain Mark/Full Mark/OverAll Percentage

                    string[] stringSeparatorsss = new string[] { "/" };
                    string[] arrs1 = obt_and_full_mark.Split(stringSeparatorsss, StringSplitOptions.None);
                    string obt_mark = arrs1[0];
                    string full_mark = arrs1[1];
                    string overall_persentage = arrs1[2];

                    string round_off_value_full_persentage = get_round_off_value(With_Decimal, Without_Decimal, Round_up, Round_down, Half_Round_Up, Half_Round_Down, With_Decimal_Per, Without_Decimal_Per, Round_up_Per, Round_down_Per, Half_Round_Up_Per, Half_Round_Down_Per, Round_Percentage_Checked, Maximum_numbe_decimal, My.toDouble(overall_persentage));
                    obt_and_full_mark = obt_mark + "月" + full_mark + "月" + round_off_value_full_persentage;

                    string exam_setting = get_exam_setting(session_id, branch_id);  // Is Attendance Show
                    string attandance = get_attandance_details(session_id, branch_id, class_id, term_id, admission_no);    //Total Class/Present Class/Present attendance percent
                    string remarkss = get_remarks(session_id, branch_id, class_id, term_id, admission_no);  // REMARKS 
                    string exam_settings = generalExamSetting.get_exam_settings(session_id, branch_id, class_id);  // Settings

                    string[] stringSeparatorss = new string[] { "月" };
                    string[] arrs = exam_settings.Split(stringSeparatorss, StringSplitOptions.None);
                    string is_grace = arrs[35];

                    if (is_grace == "1")
                    {
                        return final_marks.ToString() + "月" + grade + "月" + obt_and_full_mark + "月" + exam_setting + "月" + attandance + "月" + Grade_head_text + "月" + remarkss + "月" + exam_settings + "月" + full_number + "月" + gradeBG;
                    }
                    else
                    {
                        return final_marks.ToString() + "月" + grade + "月" + obt_and_full_mark + "月" + exam_setting + "月" + attandance + "月" + Grade_head_text + "月" + remarkss + "月" + exam_settings + "月" + full_number + "月" + gradeBG;
                    }
                }
                else
                {
                    string Grade_head_text = "OVERALL GRADE";
                    string grade = generalExamSetting.get_grade_of_a_subject_with_bg(session_id, branch_id, Grade_System_Id, final_marks, full_number, class_id);   // Grade of a subject 
                    string[] stringSeparator = new string[] { "~" };
                    string[] arr = grade.Split(stringSeparator, StringSplitOptions.None);
                    grade = arr[0];
                    string gradeBG = arr[1];
                    string obt_and_full_mark = get_obt_and_full_marks(session_id, branch_id, admission_no, term_id, class_id); // Obtain Mark/Full Mark/OverAll Percentage

                    string[] stringSeparatorsss = new string[] { "/" };
                    string[] arrs1 = obt_and_full_mark.Split(stringSeparatorsss, StringSplitOptions.None);
                    string obt_mark = arrs1[0];
                    string full_mark = arrs1[1];
                    string overall_persentage = arrs1[2];

                    string Overall_grade = get_grade(session_id, branch_id, Grade_System_Id, My.toDouble(overall_persentage), class_id);   // Grade of a subject
                    overall_persentage = Overall_grade;
                    obt_mark = "hidden";

                    obt_and_full_mark = obt_mark + "月" + full_mark + "月" + overall_persentage;

                    string exam_setting = get_exam_setting(session_id, branch_id);  // Is Attendance Show
                    string attandance = get_attandance_details(session_id, branch_id, class_id, term_id, admission_no);    //Total Class/Present Class/Present attendance percent
                    string remarkss = get_remarks(session_id, branch_id, class_id, term_id, admission_no);  // REMARKS
                    string hide_last_grade_clumn = "hidden";
                    string exam_settings = generalExamSetting.get_exam_settings(session_id, branch_id, class_id);  // Is SpecialNote & QR Show
                    return grade + "月" + hide_last_grade_clumn + "月" + obt_and_full_mark + "月" + exam_setting + "月" + attandance + "月" + Grade_head_text + "月" + remarkss + "月" + exam_settings + "月" + gradeBG;
                }
            }
            else
            {
                string Grade_head_text = "OVERALL GRADE";
                string grade = generalExamSetting.get_grade_of_a_subject_with_bg(session_id, branch_id, Grade_System_Id, final_marks, full_number, class_id);   // Grade of a subject  
                string[] stringSeparator = new string[] { "~" };
                string[] arr = grade.Split(stringSeparator, StringSplitOptions.None);
                grade = arr[0];
                string gradeBG = arr[1];
                string obt_and_full_mark = get_obt_and_full_marks(session_id, branch_id, admission_no, term_id, class_id); // Obtain Mark/Full Mark/OverAll Percentage

                string[] stringSeparatorsss = new string[] { "/" };
                string[] arrs1 = obt_and_full_mark.Split(stringSeparatorsss, StringSplitOptions.None);
                string obt_mark = arrs1[0];
                string full_mark = arrs1[1];
                string overall_persentage = arrs1[2];

                string Overall_grade = get_grade(session_id, branch_id, Grade_System_Id, My.toDouble(overall_persentage), class_id);   // Grade of a subject
                overall_persentage = Overall_grade;
                obt_mark = "hidden";

                obt_and_full_mark = obt_mark + "月" + full_mark + "月" + overall_persentage;
                string exam_setting = get_exam_setting(session_id, branch_id);  // Is Attendance Show
                string attandance = get_attandance_details(session_id, branch_id, class_id, term_id, admission_no);    //Total Class/Present Class/Present attendance percent
                string remarkss = get_remarks(session_id, branch_id, class_id, term_id, admission_no);  // REMARKS
                string hide_last_grade_clumn = "hidden";
                string exam_settings = generalExamSetting.get_exam_settings(session_id, branch_id, class_id);  // Is SpecialNote & QR Show 
                return grade + "月" + hide_last_grade_clumn + "月" + obt_and_full_mark + "月" + exam_setting + "月" + attandance + "月" + Grade_head_text + "月" + remarkss + "月" + exam_settings + "月" + gradeBG;
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
            string returN = "0~0";
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
                        returN = dt.Rows[i]["Grade"].ToString() + "~" + dt.Rows[i]["Background_color"].ToString();
                    }
                }
                return returN;
            }
        }

        private static string get_obt_and_full_marks(string session_id, string branch_id, string admission_no, string term_id, string class_id)
        {
            string first_term = get_first_term(session_id, branch_id, class_id);
            string table_name = "Exam_temp_assesment_total_no_term_II";
            if (first_term == term_id)
            {
                table_name = "Exam_temp_assesment_total_no";
            }


            string returN = "0/0/0";
            string query = "select sum(isnull(convert(float, Marks),0)) as total_obt_mark,sum(isnull(convert(float, Full_marks),0)) as total_full_mark from " + table_name + " where  Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Admission_no='" + admission_no + "' and Term_id=" + term_id + "";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                double overall_percentage = (My.toDouble(dt.Rows[0]["total_obt_mark"].ToString()) / My.toDouble(dt.Rows[0]["total_full_mark"].ToString())) * 100;
                return dt.Rows[0]["total_obt_mark"].ToString() + "/" + dt.Rows[0]["total_full_mark"].ToString() + "/" + overall_percentage.ToString("0.00");
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
            string returN = "0月0月0";
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
                                returN = dtx.Rows[0]["No_of_Class"].ToString() + "月0月0";
                            }
                            else
                            {
                                double att_persent = (My.toDouble(dtx.Rows[0]["Persent_attandance"].ToString()) / My.toDouble(dtx.Rows[0]["No_of_Class"].ToString())) * 100;
                                returN = dtx.Rows[0]["No_of_Class"].ToString() + "月" + dtx.Rows[0]["Persent_attandance"].ToString() + "月" + att_persent.ToString("0.00");
                            }
                            return returN;
                        }
                    }
                    else
                    {
                        if (dt.Rows[0]["No_of_class_Attendance"].ToString() == "")
                        {
                            returN = dt.Rows[0]["Total_no_of_class"].ToString() + "月0月0";
                        }
                        else
                        {
                            double att_persent = (My.toDouble(dt.Rows[0]["No_of_class_Attendance"].ToString()) / My.toDouble(dt.Rows[0]["Total_no_of_class"].ToString())) * 100;
                            returN = dt.Rows[0]["Total_no_of_class"].ToString() + "月" + dt.Rows[0]["No_of_class_Attendance"].ToString() + "月" + att_persent.ToString("0.00");
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
                            returN = dtxx.Rows[0]["No_of_Class"].ToString() + "月0月0";
                        }
                        else
                        {
                            double att_persent = (My.toDouble(dtxx.Rows[0]["Persent_attandance"].ToString()) / My.toDouble(dtxx.Rows[0]["No_of_Class"].ToString())) * 100;
                            returN = dtxx.Rows[0]["No_of_Class"].ToString() + "月" + dtxx.Rows[0]["Persent_attandance"].ToString() + "月" + att_persent.ToString("0.00");
                        }
                        return returN;
                    }
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

        internal static string get_term_name(string branch_id, string term_id)
        {
            string query = "select Term_Name from Exam_Term_Details where Branch_Id='" + branch_id + "' and Exam_Term_Id=" + term_id + "";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Term_Name"].ToString();
            }
        }


        //===========================================TERM II===================================///////////
        internal static void map_term_assesment(string session_id, string branch_id, string class_id, string term1_id, string term2_id)
        {
            //====================TERMI
            DataTable dt1 = FillDatastatic("select * from Exam_Assessment_Details where Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and   Class_id=" + class_id + " and Exam_Term_Id=" + term1_id + " and Scholastic_Co_scholastic='Scholastic' and (Term_assesment_update_status is null or Term_assesment_update_status='') order by Sequence_No asc");


            //====================TERMII
            DataTable dt2 = FillDatastatic("select * from Exam_Assessment_Details where Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and   Class_id=" + class_id + " and Exam_Term_Id=" + term2_id + " and Scholastic_Co_scholastic='Scholastic' and (Term_assesment_update_status is null or Term_assesment_update_status='') order by Sequence_No asc");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dtime.ToString("ddMMyyyy");
                string time = dtime.ToString("hhmmss");
                String unique_id = date + time + My.create_random_no_otp();

                string qqrys = "update Exam_Assessment_Details set Term_assesment_unique_id='" + unique_id + "',Term_assesment_update_status=1 where Id=" + dt1.Rows[i]["Id"].ToString() + "";
                My.exeSql(qqrys);


                //=====================TERM2
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {
                    if (i == ii)
                    {
                        string qqryss = "update Exam_Assessment_Details set Term_assesment_unique_id='" + unique_id + "',Term_assesment_update_status=1 where Id=" + dt2.Rows[ii]["Id"].ToString() + "";
                        My.exeSql(qqryss);
                    }
                }
            }
        }

        internal static string get_ttl_mark_of_a_subject(string subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII)
        {
            string system_grade = get_system_grade_id(Session_id, Branch_id, Class_id);
            string query = "select sum(isnull(convert(float, t1.Marks),0)) as Ttl_mark_for_term1,sum(isnull(convert(float, t2.Marks),0)) as Ttl_mark_for_termII from Exam_temp_assesment_total_no t1 join Exam_temp_assesment_total_no_term_II t2 on t1.Session_id=t2.Session_id and t1.Branch_id=t2.Branch_id and t1.Class_id=t2.Class_id and t1.Subject_id=t2.Subject_id and t1.Admission_no=t2.Admission_no and t1.Exam_termwise_assesment_id=t2.Exam_termwise_assesment_id join Exam_Assessment_Details t3 on t1.Session_Id=t3.Session_Id and t1.Branch_Id=t3.Branch_Id and t1.Term_id=t3.Exam_Term_Id and t1.Class_id=t3.Class_id and t1.Assessment_id=t3.Assessment_Id where t1.Session_id=" + Session_id + " and  t1.Admission_no='" + Admission_no + "' and t1.Subject_id=" + subject_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + "";
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
                double termI_termIi_average_percent = (My.toDouble(dt.Rows[0]["Ttl_mark_for_term1"].ToString()) + My.toDouble(dt.Rows[0]["Ttl_mark_for_termII"].ToString())) / 2;
                string termI_termII_grade = get_grade(Session_id, Branch_id, system_grade, termI_termIi_average_percent, Class_id);

                string TermIattandance = get_attandance_details(Session_id, Branch_id, Class_id, Term_idI, Admission_no);    //Total Class/Present Class/Present attendance percent
                string TermIIattandance = get_attandance_details(Session_id, Branch_id, Class_id, Term_idII, Admission_no);    //Total Class/Present Class/Present attendance percent


                string overall_mark_and_percent = get_overall_mrk(Session_id, Branch_id, Class_id, Admission_no);  //MARKS/PERCENT
                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = overall_mark_and_percent.Split(stringSeparatorss, StringSplitOptions.None);
                string Overall_mark = arrs[0];
                string Overall_percent = arrs[1];


                string round_off_value_full_persentage = get_round_off_value(With_Decimal, Without_Decimal, Round_up, Round_down, Half_Round_Up, Half_Round_Down, With_Decimal_Per, Without_Decimal_Per, Round_up_Per, Round_down_Per, Half_Round_Up_Per, Half_Round_Down_Per, Round_Percentage_Checked, Maximum_numbe_decimal, My.toDouble(Overall_percent));


                return dt.Rows[0]["Ttl_mark_for_term1"].ToString() + "/" + dt.Rows[0]["Ttl_mark_for_termII"].ToString() + "/" + termI_grade + "/" + termII_grade + "/" + termI_termIi_average_percent + "/" + termI_termII_grade + "/" + TermIattandance + "/" + TermIIattandance + "/" + Overall_mark + "/" + round_off_value_full_persentage;
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

        internal static string get_system_grade_id_final(string session_id, string branch_id, string class_id)
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

        internal static string get_marks_output(string Session_id, string Branch_id, string system_grade_id)
        {
            string Output = "";
            string querys = "select Output from Exam_Grade_System where Session_Id='" + Session_id + "' and Branch_id='" + Branch_id + "'  and Grade_System_Id=" + system_grade_id + "";
            DataTable dtx = FillDatastatic(querys);
            if (dtx.Rows.Count != 0)
            {
                Output = dtx.Rows[0]["Output"].ToString();
            }
            return Output;
        }

        internal static string get_grade_final(string session_id, string branch_id, string Grade_System_Id, double final_marks)
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

        internal static string get_coscholastic_number(string Session_id, string Branch_id, string Class_id, string Admission_no, string Term_id, string subject_id)
        {
            string Output = "NA";
            string querys = "select Marks from Exam_coscholastic_and_activities_assesment_total_no  where Session_id=" + Session_id + " and Branch_id='" + Branch_id + "' and Class_id=" + Class_id + " and Entry_type='Co-Scholastic' and Admission_no='" + Admission_no + "' and Term_id=" + Term_id + " and Subject_id=" + subject_id + "";
            DataTable dtx = FillDatastatic(querys);
            if (dtx.Rows.Count != 0)
            {
                Output = dtx.Rows[0]["Marks"].ToString();
            }
            return Output;
        }

        internal static string get_descipline_number(string Session_id, string Branch_id, string Class_id, string Admission_no, string Term_id, string activity_id)
        {
            string Output = "NA";
            string querys = "select Marks from Exam_coscholastic_and_activities_assesment_total_no  where Session_id=" + Session_id + " and Branch_id='" + Branch_id + "' and Class_id=" + Class_id + " and Entry_type='DISCIPLINE' and Admission_no='" + Admission_no + "' and Term_id=" + Term_id + " and Assessment_id=" + activity_id + "";
            DataTable dtx = FillDatastatic(querys);
            if (dtx.Rows.Count != 0)
            {
                Output = dtx.Rows[0]["Marks"].ToString();
            }
            return Output;
        }

        internal string get_examtername_from_examid(string examterid, string Branch_Id)
        {
            string query = "select Term_Name from Exam_Term_Details where Exam_Term_Id=" + examterid + "  and Branch_Id='" + Branch_Id + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Term_Name"].ToString();
            }
        }

        internal static bool chek_create_or_no_cerate_admitcard(string admission_no, string branchid, string sessionid)
        {


            Dictionary<string, object> dc1 = My.get_selected_studentinfo(admission_no, sessionid, branchid);
            string Class_id = (String)dc1["Class_id"];
            string Section = (String)dc1["Section"];

            string query = "select top 1 Id from Exam_Time_Table where Session_Id=" + sessionid + "  and Branch_id='" + branchid + "' and Class_id=" + Class_id + " and Section='" + Section + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        internal string get_subject_heading_subjective(string Class_Id, string section, string termid, string assesment_id, string sessionid, string branchid)
        {
            string querymain = "Select  studentname as Student_Name,admissionserialnumber as Admission_No,rollnumber as Roll_No  ";
            string query = " Select   su.subject2,su.Subject_id,su.course_id    from   Subject_Master su join Exam_Assessment_Subject_Mapping_Details smc on smc.Class_id=su.course_id and smc.Subject_id=su.Subject_id and smc.Branch_Id=su.Branch_id where  smc.Exam_Term_Id=" + termid + " and  smc.Session_Id=" + sessionid + " and  smc.Branch_Id='" + branchid + "' and smc.Class_id=" + Class_Id + " and smc.Assessment_Id=" + assesment_id + " ORDER BY smc.Sequence_No";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {

                    querymain += ", isnull((Select Marks from Exam_marks where   Admission_no=ar.admissionserialnumber  and Session_id='" + sessionid + "' and Branch_id='" + branchid + "' and Class_id=" + Class_Id + " and Section='" + section + "' and Term=" + termid + " and Assessment=" + assesment_id + " and Subject='" + dr["Subject_id"].ToString() + "' ),0) " + dr["subject2"].ToString();
                }
            }
            querymain += ",  '0' TOTAL from    admission_registor ar   where   ar.Session_id=" + sessionid + " and ar.Branch_id='" + branchid + "'  and ar.Section='" + section + "' and Class_id=" + Class_Id + " and Status='1'  order by rollnumber asc";
            return querymain;
        }

        internal static string get_signature(string Session_id, string Class_id, string Section, string Branch_id)
        {
            string class_teacher_sig = get_class_teacher_sig(Session_id, Class_id, Section, Branch_id);  // Class Teacher Signature
            string principal_sig = get_principal_sig(Session_id, Branch_id);  // Principal Signature
            string examinee_sig = get_examinee_sig(Session_id, Branch_id);  // Examinee Signature


            return class_teacher_sig + ">" + principal_sig + ">" + examinee_sig;
        }



        private static string get_examinee_sig(string Session_id, string Branch_id)
        {
            string returN = "hidden>hidden";
            DataTable dsgdt = My.dataTable(" select Is_parent_signature_show from Exam_report_card_setting where Session_id ='" + Session_id + "' and Branch_id='" + Branch_id + "'");
            if (dsgdt.Rows[0][0].ToString() == "True")
            {
                returN = "hidden>Parent";
            }
            else
            {
                string query = "select Signature from user_details where User_Type='Examination Incharge' and Branch_id='" + Branch_id + "' and Istatus='1'";
                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    return returN + ">Examinee";
                }
                else
                {
                    if (dt.Rows[0]["Signature"].ToString() == "")
                    {
                        returN = "hidden>Examinee";
                    }
                    else
                    {
                        returN = dt.Rows[0]["Signature"].ToString() + ">Examinee";
                    }
                }
            }
            return returN;
        }

        private static string get_principal_sig(string Session_id, string Branch_id)
        {
            string returN = "hidden";
            string query = "select Signature from user_details where User_Type='Principal' and Branch_id='" + Branch_id + "' and Istatus='1'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                if (dt.Rows[0]["Signature"].ToString() == "")
                {
                    returN = "hidden";
                }
                else
                {
                    returN = dt.Rows[0]["Signature"].ToString();
                }
                return returN;
            }
        }

        private static string get_class_teacher_sig(string Session_id, string Class_id, string Section, string Branch_id)
        {
            string returN = "hidden";
            string query = "select *,(select top 1 Signature from user_details where user_id=Ptm_class_teacher_mapping.UserID) as Signature from Ptm_class_teacher_mapping where Session_Id='" + Session_id + "' and CategoryID='" + Class_id + "' and Section='" + Section + "' and Branch_id='" + Branch_id + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                if (dt.Rows[0]["Signature"].ToString() == "")
                {
                    returN = "hidden";
                }
                else
                {
                    returN = dt.Rows[0]["Signature"].ToString();
                }
                return returN;
            }
        }

        internal static string get_student_section(string Session_id, string Admission_no, string Branch_id, string Class_id)
        {
            string returN = "A";
            string query = "select Section from admission_registor where Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and admissionserialnumber='" + Admission_no + "' and Branch_id='" + Branch_id + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = dt.Rows[0]["Section"].ToString();
                return returN;
            }
        }

        ////////////////////=====================================
        internal static string get_signature_admit_card(string Session_id, string Class_id, string Section, string Branch_id)
        {
            string class_teacher_sig = get_class_teacher_sig(Session_id, Class_id, Section, Branch_id);  // Class Teacher Signature
            string principal_sig = get_principal_sig(Session_id, Branch_id);  // Principal Signature
            string examinee_sig = get_examinee_sig_admit(Session_id, Branch_id);  // Examinee Signature 
            return class_teacher_sig + ">" + principal_sig + ">" + examinee_sig;
        }



        private static string get_examinee_sig_admit(string Session_id, string Branch_id)
        {
            string returN = "hidden";
            string query = "select Signature from user_details where User_Type='Examination Incharge' and Branch_id='" + Branch_id + "' and Istatus='1'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                if (dt.Rows[0]["Signature"].ToString() == "")
                {
                    returN = "hidden";
                }
                else
                {
                    returN = dt.Rows[0]["Signature"].ToString();
                }
            }
            return returN;
        }

        //==============================================
        //NEWCODES//=====================================
        internal static string get_class_teacher_signature(string Session_id, string Class_id, string Section, string Branch_id)
        {
            string returN = "hidden";
            string query = "select *,(select top 1 Signature from user_details where user_id=Ptm_class_teacher_mapping.UserID) as Signature from Ptm_class_teacher_mapping where Session_Id='" + Session_id + "' and CategoryID='" + Class_id + "' and Section='" + Section + "' and Branch_id='" + Branch_id + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                if (dt.Rows[0]["Signature"].ToString() == "")
                {
                    returN = "hidden";
                }
                else
                {
                    returN = dt.Rows[0]["Signature"].ToString();
                }
                return returN;
            }
        }

        internal static string get_principal_signature(string Session_id, string Branch_id)
        {
            string returN = "hidden";
            string query = "select Signature from user_details where User_Type='Principal' and Branch_id='" + Branch_id + "' and Istatus='1'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                if (dt.Rows[0]["Signature"].ToString() == "")
                {
                    returN = "hidden";
                }
                else
                {
                    returN = dt.Rows[0]["Signature"].ToString();
                }
                return returN;
            }
        }

        internal static string get_examinee_or_parent_signature(string Session_id, string Branch_id, string subSignature)
        {
            string returN = "hidden";
            if (subSignature == "2") //Parents
            {
                returN = "hidden";
            }
            else
            {
                string query = "select Signature from user_details where User_Type='Examination Incharge' and Branch_id='" + Branch_id + "' and Istatus='1'";
                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    return returN;
                }
                else
                {
                    if (dt.Rows[0]["Signature"].ToString() == "")
                    {
                        returN = "hidden";
                    }
                    else
                    {
                        returN = dt.Rows[0]["Signature"].ToString();
                    }
                }
            }
            return returN;
        }

        internal static string get_subject_name(string Subject_id, string Class_id, string Branch_id)
        {
            string returN = "";
            string query = "select top 1 Subject_name from Subject_Master where course_id='" + Class_id + "' and  Subject_id='" + Subject_id + "' and Branch_id='" + Branch_id + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = dt.Rows[0]["Subject_name"].ToString();
            }
            return returN;
        }

        internal static string get_section(string Session_id, string Class_id, string Admission_no, string Branch_id)
        {
            string returN = "A";
            string query = "select top 1 Section from admission_registor where admissionserialnumber='" + Admission_no + "' and Class_id='" + Class_id + "' and  Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = dt.Rows[0]["Section"].ToString();
            }
            return returN;
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
                string grade = get_grade(Session_id, Branch_id, Grade_System_Id, My.toDouble(overall_percent), Class_id);   // Grade of a subject
                string[] stringSeparator = new string[] { "~" };
                string[] arr = grade.Split(stringSeparator, StringSplitOptions.None);
                grade = arr[0];
                string gradeBG = arr[1];
                result = obt_and_full_mark + "/" + marksType + "/" + grade + "/" + gradeBG;
            }
            else
            {
                marksType = "GRADE";
                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = obt_and_full_mark.Split(stringSeparatorss, StringSplitOptions.None);
                string overall_obt_marks = arrs[0];
                string overall_full_marks = arrs[1];
                string overall_percent = arrs[2];
                string grade = get_grade(Session_id, Branch_id, Grade_System_Id, My.toDouble(overall_percent), Class_id);   // Grade of a subject
                string[] stringSeparator = new string[] { "~" };
                string[] arr = grade.Split(stringSeparator, StringSplitOptions.None);
                grade = arr[0];
                string gradeBG = arr[1];
                result = overall_obt_marks + "/" + overall_full_marks + "/" + grade + "/" + marksType + "/" + grade + "/" + gradeBG;
            }


            return result;
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

        internal bool check_valid_date_examnation(string sesssion_id, string Branch_Id, string course_id, string examterm_id, string assesment_id, string subject_id, string exam_level_activaty)
        {

            int currentodate = Convert.ToInt32(mycode.idate());
            string query = "Select * from Exam_Subject_Sub_Level where    Start_IDate_Marks<=" + currentodate + " and End_IDate_Marks>=" + currentodate + " and Session_Id=" + sesssion_id + " and Branch_Id=" + Branch_Id + " and Class_id=" + course_id + " and Exam_Term_Id=" + examterm_id + " and Assessment_Id=" + assesment_id + " and Subject_id=" + subject_id + " and Subject_Sub_Level_Id=" + exam_level_activaty + " ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal bool check_marks_save(string sesssion_id, string Branch_Id, string course_id, string examterm_id, string assesment_id, string subject_id, string exam_level_activaty, string section)
        {

            int currentodate = Convert.ToInt32(mycode.idate());
            // string query = "Select * from Exam_Subject_Sub_Level where  Session_Id=" + sesssion_id + " and Branch_Id=" + Branch_Id + " and Class_id=" + course_id + " and Exam_Term_Id=" + examterm_id + " and Assessment_Id=" + assesment_id + " and Subject_id=" + subject_id + " and Subject_Sub_Level_Id=" + exam_level_activaty + " and Is_save_marks=0 ";

            string query = "Select * from Exam_marks where  Session_id=" + sesssion_id + " and Branch_id=" + Branch_Id + " and Class_id=" + course_id + " and Term=" + examterm_id + " and Assessment=" + assesment_id + " and Subject=" + subject_id + " and Subject_activity=" + exam_level_activaty + " and Section='" + section + "' and Is_save_marks=1 ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        internal static string get_marks_assiment(string assessments_Id, string Class_id, string session, string Branch_id, string tearmid)
        {
            string query = "select * from Exam_Assessment_Details where Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Assessment_Id='" + assessments_Id + "' and Session_Id=" + session + " and Exam_Term_Id=" + tearmid + " ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0/0";
            }
            else
            {
                return dt.Rows[0]["Maximum_Marks"].ToString() + "/" + dt.Rows[0]["Cut_Off_Percentage"].ToString();
            }
        }

        internal string get_fill_marks_date(string sesssion_id, string Branch_Id, string course_id, string examterm_id, string assesment_id, string subject_id, string exam_level_activaty)
        {
            string query = "select Start_Date_Marks,End_Date_Marks from Exam_Subject_Sub_Level where Session_Id='" + sesssion_id + "' and Branch_Id='" + Branch_Id + "' and  Exam_Term_Id=" + examterm_id + " and Assessment_Id=" + assesment_id + " and Class_id=" + course_id + " and Subject_id=" + subject_id + " and Subject_Sub_Level_Id=" + exam_level_activaty + "";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return "from Date -" + dt.Rows[0]["Start_Date_Marks"].ToString() + " till date -" + dt.Rows[0]["End_Date_Marks"].ToString();
            }

        }

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


        internal string get_subject_heading_subjective_new(string Class_Id, string section, string termid, string assesment_id, string sessionid, string branchid)
        {
            string querymain = "Select  studentname as Student_Name,admissionserialnumber as Admission_No,rollnumber as Roll_No  ";
            string query = " Select   su.subject2,su.Subject_id,su.course_id ,su.Subject_Short_Name   from   Subject_Master su join Exam_Assessment_Subject_Mapping_Details smc on smc.Class_id=su.course_id and smc.Subject_id=su.Subject_id and smc.Branch_Id=su.Branch_id where  smc.Exam_Term_Id=" + termid + " and  smc.Session_Id=" + sessionid + " and  smc.Branch_Id='" + branchid + "' and smc.Class_id=" + Class_Id + " and smc.Assessment_Id=" + assesment_id + " ORDER BY smc.Sequence_No";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    querymain += ", (select isnull(stuff((SELECT ', ' +  em.Marks  from Exam_marks em join  Exam_Subject_Sub_Level essl on em.Subject_activity=essl.Subject_Sub_Level_Id where   em.Admission_no=ar.admissionserialnumber  and em.Session_id='" + sessionid + "' and em.Branch_id='" + branchid + "' and em.Class_id=" + Class_Id + " and em.Section='" + section + "' and em.Term=" + termid + " and em.Assessment=" + assesment_id + " and em.Subject='" + dr["Subject_id"].ToString() + "' order by essl.Sequence_No asc  FOR XML PATH('')), 1, 1,''), 'N/A')) [" + dr["Subject_Short_Name"].ToString() + "]";
                }
            }
            querymain += " from admission_registor ar   where   ar.Session_id=" + sessionid + " and ar.Branch_id='" + branchid + "'  and ar.Section='" + section + "' and Class_id=" + Class_Id + " and Status='1'  order by rollnumber asc";
            return querymain;
        }

        internal string get_subject_heading_subjective_new_all(string Class_Id, string section, string termid, string sessionid, string branchid)
        {
            string querymain = "Select  studentname as Student_Name,admissionserialnumber as Admission_No,rollnumber as Roll_No  ";
            string query = " Select   DISTINCT  smc.Sequence_No,su.subject2,su.Subject_id,su.course_id ,su.Subject_Short_Name from Subject_Master su join Exam_Assessment_Subject_Mapping_Details smc on smc.Class_id=su.course_id and smc.Subject_id=su.Subject_id and smc.Branch_Id=su.Branch_id where  smc.Exam_Term_Id=" + termid + " and  smc.Session_Id=" + sessionid + " and  smc.Branch_Id='" + branchid + "' and smc.Class_id=" + Class_Id + " ORDER BY smc.Sequence_No";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    querymain += ", (select isnull(stuff((SELECT ', ' +  em.Marks  from Exam_marks em join  Exam_Subject_Sub_Level essl on em.Subject_activity=essl.Subject_Sub_Level_Id where   em.Admission_no=ar.admissionserialnumber  and em.Session_id='" + sessionid + "' and em.Branch_id='" + branchid + "' and em.Class_id=" + Class_Id + " and em.Section='" + section + "' and em.Term=" + termid + " and em.Subject='" + dr["Subject_id"].ToString() + "' order by essl.Sequence_No asc  FOR XML PATH('')), 1, 1,''), 'N/A')) [" + dr["Subject_Short_Name"].ToString() + "]";
                }
            }
            querymain += " from admission_registor ar   where   ar.Session_id=" + sessionid + " and ar.Branch_id='" + branchid + "'  and ar.Section='" + section + "' and Class_id=" + Class_Id + " and Status='1'  order by rollnumber asc";
            return querymain;
        }

        internal static string get_co_scholastic_system_grade_id(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string query = "select DISTINCT t3.Grade_System_Id,t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id join Exam_Subject_Sub_Level t3 on t3.Session_Id=t1.Session_id and t1.Class_id=t3.Class_id and  t1.Branch_Id=t3.Branch_Id and t1.Term=t3.Exam_Term_Id and t1.Subject=t3.Subject_id and t1.Assessment=t3.Assessment_Id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic' order by t2.Subject_position asc";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Grade_System_Id"].ToString();
            }
        }

        internal static string get_extra_marks(string Class_id, string Branch_id)
        {
            string extra_marks = "0";
            try
            {
                string query = "select * from Exam_report_card_setting_classwise where Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "'";
                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    extra_marks = "0";
                }
                else
                {
                    if (dt.Rows[0]["Is_extra_mark_show_in_head"].ToString() == "True")
                    {
                        extra_marks = dt.Rows[0]["Extra_marks"].ToString();
                    }
                    else
                    {
                        extra_marks = "0";
                    }
                }
                return extra_marks;
            }
            catch (Exception ex)
            {
                return extra_marks;
            }
        }



        internal static string get_gp_final(string session_id, string branch_id, string Grade_System_Id, double final_marks)
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


        internal static string is_calculate_dues_for_report_card(string session_id, string class_id, string term_id)
        {
            string returNV = "NO/0/0/0/0";
            DataTable dt = My.dataTable("select Paid_month_id,Month_name,Calculate_dues_type,(select top 1 Position from Month_Index where Month=VALIDATION_FOR_ADMIT_REPORT_CARD.Month_name) as MonthPosition from VALIDATION_FOR_ADMIT_REPORT_CARD where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Status='1' and Type='ReportCard'");
            if (dt.Rows.Count > 0)
            {
                returNV = "YES/" + dt.Rows[0]["Paid_month_id"].ToString() + "/" + dt.Rows[0]["Month_name"].ToString() + "/" + dt.Rows[0]["Calculate_dues_type"].ToString() + "/" + dt.Rows[0]["MonthPosition"].ToString();
            }
            return returNV;
        }

        internal static string is_calculate_dues_for_admit_card(string session_id, string class_id, string term_id)
        {
            string returNV = "NO/0/0/0/0";
            DataTable dt = My.dataTable("select Paid_month_id,Month_name,Calculate_dues_type,(select top 1 Position from Month_Index where Month=DUES_CALCULATE_FOR_ADMIT_CARD.Month_name) as MonthPosition from DUES_CALCULATE_FOR_ADMIT_CARD where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Status='1'");
            if (dt.Rows.Count > 0)
            {
                returNV = "YES/" + dt.Rows[0]["Paid_month_id"].ToString() + "/" + dt.Rows[0]["Month_name"].ToString() + "/" + dt.Rows[0]["Calculate_dues_type"].ToString() + "/" + dt.Rows[0]["MonthPosition"].ToString();
            }
            return returNV;
        }


        internal static string isAttendanceNeeded(string session_id, string class_id, string term_id, string Exam_id, string admission_no)
        {
            string section = My.get_single_column_data("Select top 1 Section as Column_Name from admission_registor where Session_id='" + session_id + "' and admissionserialnumber='" + admission_no + "'");
            string iSATTTRUE = "1";
            DataTable dtA = My.dataTable("select * from Exam_validation_for_admit_card_attendance where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Exam_id='" + Exam_id + "' ");
            if (dtA.Rows.Count > 0)
            {
                // fetch_no_of_days(adm_no, session_id, "0", "0", dt.Rows[0]["From_date"].ToString(), dt.Rows[0]["To_date"].ToString());

                DateTime fromDateTime = DateTime.ParseExact(dtA.Rows[0]["From_date"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime toDateTime = DateTime.ParseExact(dtA.Rows[0]["To_date"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                TimeSpan t = toDateTime.Subtract(fromDateTime);


                int daysinmonth = My.toIntS(t.TotalDays + 1);
                int total_no_of_days = My.toint(daysinmonth);
                int total_no_of_days_less_one = total_no_of_days - 1;
                int total_holiday_days = 0;
                int total_persent_days = 0;
                int total_absent_days = 0;
                int total_leave_days = 0;
                int total_working_days = 0;
                double attendance_perc = 0;
                int idates = 0;
                for (int i = 1; i <= daysinmonth; i++)
                {
                    idates = My.DateConvertToIdate(fromDateTime.ToString("dd/MM/yyyy"));
                    string dayName = fromDateTime.ToString("dddd");

                    string query = "select * from Student_Attendance_saved_Class_Wise where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Admission_no='" + admission_no + "' and Attendance_IDate='" + idates + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Class_Routine_period_Master");
                    DataTable dt = ds.Tables[0];
                    int rowcount1 = dt.Rows.Count;
                    if (rowcount1 == 0)
                    {
                        string daynameClass = "";
                        string AttendanceS_type = "";


                        //============================
                        string period_type_in_calender = "Class";
                        string querys = "select Type from School_Holiday_Calendar where Idate='" + idates + "' and Session_id='" + session_id + "' and Day='" + dayName + "' and Class_id='" + class_id + "'";
                        DataTable dts = My.dataTable(querys);
                        if (dts.Rows.Count > 0)
                        {
                            period_type_in_calender = dts.Rows[0]["Type"].ToString();
                        }
                        //============================



                        if (period_type_in_calender == "Class")
                        {
                            AttendanceS_type = "NA";
                            daynameClass = "notattendances";
                            total_absent_days++;
                        }
                        else if (period_type_in_calender == "Holiday")
                        {
                            AttendanceS_type = period_type_in_calender;
                            total_holiday_days++;
                        }
                        else
                        {
                            AttendanceS_type = period_type_in_calender;
                            if (dayName == "Sunday")
                            {
                                total_holiday_days++;
                            }
                            else
                            {
                                total_holiday_days++;
                            }
                        }

                        if (period_type_in_calender == "Holiday")
                        {
                            daynameClass = "daySunday";
                        }
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                        }
                        total_working_days = total_no_of_days - total_holiday_days;
                        attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                        ///========================
                        ///=========================== 
                    }
                    else
                    {
                        //============================
                        string period_type_in_calender = "Class";
                        string querys = "select Type from School_Holiday_Calendar where Idate='" + idates + "' and Session_id='" + session_id + "' and Day='" + dayName + "' and Class_id='" + class_id + "'";
                        DataTable dts = My.dataTable(querys);
                        if (dts.Rows.Count > 0)
                        {
                            period_type_in_calender = dts.Rows[0]["Type"].ToString();
                        }
                        //============================

                        if (period_type_in_calender == "Holiday")
                        {
                            dayName = "Sunday";
                        }


                        string daynameClass = "";
                        if (dayName == "Sunday")
                        {
                            daynameClass = "daySunday";
                            total_holiday_days++;
                        }
                        else
                        {
                            string attt_type = "";
                            if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                            {
                                attt_type = "P"; daynameClass = "daypresenT";
                                total_persent_days++;
                            }
                            if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                            {
                                attt_type = "A"; daynameClass = "dayabsenT";
                                total_absent_days++;
                            }
                            if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                            {
                                attt_type = "L"; daynameClass = "dayleavE";
                                total_leave_days++;
                            }
                        }
                        total_working_days = total_no_of_days - total_holiday_days;
                        attendance_perc = (My.toDouble(total_persent_days) / My.toDouble(total_working_days)) * 100;
                    }
                    string plusOne = fromDateTime.AddDays(+1).ToShortDateString();
                    fromDateTime = DateTime.ParseExact(plusOne, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                if (My.toDouble(dtA.Rows[0]["Attendance_percent"].ToString()) > attendance_perc)
                {
                    iSATTTRUE = "0";
                }
            }
            return iSATTTRUE;
        }
    }
}