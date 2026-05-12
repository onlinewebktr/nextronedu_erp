using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Examination_Admin.slip
{
    /// <summary>
    /// Summary description for final_report_card
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class final_report_card : System.Web.Services.WebService
    {
        public class Fetch_Details_of_report_head
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
        }

        List<Fetch_Details_of_report_head> Show_of_report_head = new List<Fetch_Details_of_report_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_card(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string query = "select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_report_head.Add(new Fetch_Details_of_report_head
                    {
                        Assessment_Name = dr["Assessment_Name"].ToString(),
                        Short_Name = dr["Short_Name"].ToString(),
                        Maximum_Marks = dr["Maximum_Marks"].ToString(),
                        Term_Maximum_Marks = dr["Term_Maximum_Marks"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_head));
            }
        }



        //======================================
        //=======================================================
        public class MySubjectMarksShow
        {
            public string Subject { get; set; }
            public string Subject_name { get; set; }
            public string Total_mark_of_a_subject_for_termI { get; set; }
            public string Total_mark_of_a_subject_for_termII { get; set; }
            public string grade_of_a_subject_for_termI { get; set; }
            public string grade_of_a_subject_for_termII { get; set; }
            public string termI_termIi_average_percent { get; set; }
            public string termI_termII_grade { get; set; }
            public string ttl_marks_head_termI { get; set; }
            public string ttl_marks_head_termII { get; set; }
            public string TermI_grade_head { get; set; }
            public string TermII_grade_head { get; set; }
            public string If_is_mrk_hide { get; set; }
            public string If_is_grade_ttl_mark_hide { get; set; }
            public string ColspanFive { get; set; }
            public string Overall_ab_grade { get; set; }
            public string Overall_av_marks { get; set; }
            public string ColspanOneTwo { get; set; }

            public string Total_class_of_term_I { get; set; }
            public string Total_Present_class_of_term_I { get; set; }
            public string Total_class_of_term_II { get; set; }
            public string Total_Present_class_of_term_II { get; set; }
            public string Overall_final_percent { get; set; }
            public string Overall_final_grade { get; set; }
            public string Next_session_year { get; set; }
            //public string qr_code_Show { get; set; }
            //public string qr_div_true { get; set; }
            //public string Remarkss { get; set; }
            public List<MySubjectMarkDetails> MySubjectMarkItem { get; set; }
        }

        public class MySubjectMarkDetails
        {
            public string Marks_term_I { get; set; }
            public string Marks_term_II { get; set; }
            public string Subject_Name { get; set; }
            public string Term_i_full_mark { get; set; }
            public string Term_ii_full_mark { get; set; }
        }


        List<MySubjectMarksShow> EMySubMark = new List<MySubjectMarksShow>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_subjects(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_idI, string Term_idII)
        {
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term_id,t1.Subject_id,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position from Exam_temp_assesment_total_no t1 join Subject_Master t2 on t1.Subject_id=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term_id=" + Term_idI + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' order by t2.Subject_position asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MySubjectMarkDetails> MBdetails = findmyBookingProduct(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idI);

                    string ttl_mark_of_a_subject = Examination.get_ttl_mark_of_a_subject(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idI, Term_idII);
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = ttl_mark_of_a_subject.Split(stringSeparatorss, StringSplitOptions.None);
                    string Total_mark_of_a_subject_for_termI = arrs[0];
                    string Total_mark_of_a_subject_for_termII = arrs[1];
                    string grade_of_a_subject_for_termI = arrs[2];
                    string grade_of_a_subject_for_termII = arrs[3];
                    string termI_termIi_average_percent = arrs[4];
                    string termI_termII_grade = arrs[5];


                    string Total_class_of_term_I = arrs[6];
                    string Total_Present_class_of_term_I = arrs[7];

                    string Total_class_of_term_II = arrs[9];
                    string Total_Present_class_of_term_II = arrs[10];


                    string Overall_final_mark = arrs[12];
                    string Overall_final_percent = arrs[13];

                    //===========================
                    string system_grade_id = Examination.get_system_grade_id_final(Session_id, Branch_id, Class_id);
                    string marks_output = Examination.get_marks_output(Session_id, Branch_id, system_grade_id);

                    string Total_mark_of_a_subject_for_termI_final = "";
                    string Total_mark_of_a_subject_for_termII_final = "";
                    string ttl_marks_head_termI = "";
                    string ttl_marks_head_termII = "";
                    string termI_grade = "";
                    string termII_grade = "";
                    string if_is_mrk_hide = "";
                    string if_is_grade_ttl_mark_hide = "";
                    string colspanfiveandsix = "";
                    string overall_ab_grade = "";
                    string overall_av_marks = "";
                    string colspanOneTwo = "";
                    string overall_final_grade = Examination.get_grade_final(Session_id, Branch_id, system_grade_id, My.toDouble(Overall_final_percent));
                    string session_name = My.get_session();

                    string[] stringSeparatorssx = new string[] { "-" };
                    string[] arrsx = session_name.Split(stringSeparatorssx, StringSplitOptions.None);
                    string next_session_year = arrsx[1];

                    if (marks_output == "Marks")
                    {
                        Total_mark_of_a_subject_for_termI_final = Total_mark_of_a_subject_for_termI;
                        Total_mark_of_a_subject_for_termII_final = Total_mark_of_a_subject_for_termII;
                        ttl_marks_head_termI = "TOTAL (A)";
                        ttl_marks_head_termII = "TOTAL (B)";
                        termI_grade = "GRADE";
                        termII_grade = "GRADE";
                        if_is_mrk_hide = "hidden";
                        if_is_grade_ttl_mark_hide = "show";
                        colspanfiveandsix = "6";
                        overall_ab_grade = "GRADE";
                        overall_av_marks = "show";
                        colspanOneTwo = "2";
                    }
                    else
                    {
                        string Total_grade_of_a_subject_for_termI_final = Examination.get_grade_final(Session_id, Branch_id, system_grade_id, My.toDouble(Total_mark_of_a_subject_for_termI));
                        string Total_grade_of_a_subject_for_termII_final = Examination.get_grade_final(Session_id, Branch_id, system_grade_id, My.toDouble(Total_mark_of_a_subject_for_termII));

                        Total_mark_of_a_subject_for_termI_final = Total_grade_of_a_subject_for_termI_final;
                        Total_mark_of_a_subject_for_termII_final = Total_grade_of_a_subject_for_termII_final;
                        ttl_marks_head_termI = "TOTAL (A)";
                        ttl_marks_head_termII = "TOTAL (B)";
                        termI_grade = "TOTAL GRADE (A)";
                        termII_grade = "TOTAL GRADE (B)";
                        if_is_mrk_hide = "show";
                        if_is_grade_ttl_mark_hide = "hidden";
                        colspanfiveandsix = "5";
                        overall_ab_grade = "GRADE (A+B/2) (100)";
                        overall_av_marks = "hidden";
                        colspanOneTwo = "1";
                    }

                    EMySubMark.Add(new MySubjectMarksShow
                    {
                        Subject = dr["Subject_id"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        Total_mark_of_a_subject_for_termI = Total_mark_of_a_subject_for_termI_final,
                        Total_mark_of_a_subject_for_termII = Total_mark_of_a_subject_for_termII_final,

                        grade_of_a_subject_for_termI = grade_of_a_subject_for_termI,
                        grade_of_a_subject_for_termII = grade_of_a_subject_for_termII,

                        termI_termIi_average_percent = termI_termIi_average_percent,
                        termI_termII_grade = termI_termII_grade,


                        ttl_marks_head_termI = ttl_marks_head_termI,
                        ttl_marks_head_termII = ttl_marks_head_termII,
                        TermI_grade_head = termI_grade,
                        TermII_grade_head = termII_grade,

                        If_is_mrk_hide = if_is_mrk_hide,
                        If_is_grade_ttl_mark_hide = if_is_grade_ttl_mark_hide,
                        ColspanFive = colspanfiveandsix,
                        Overall_ab_grade = overall_ab_grade,
                        Overall_av_marks = overall_av_marks,
                        ColspanOneTwo = colspanOneTwo,
                        Total_class_of_term_I = Total_class_of_term_I,
                        Total_Present_class_of_term_I = Total_Present_class_of_term_I,
                        Total_class_of_term_II = Total_class_of_term_II,
                        Total_Present_class_of_term_II = Total_Present_class_of_term_II,
                        Overall_final_percent = Overall_final_percent + " %",
                        Overall_final_grade = overall_final_grade,
                        Next_session_year = next_session_year,
                        //Overall_full_mark = overall_full_marks,
                        //Overall_percent = overall_percent,
                        //Is_attandance_show = is_attandance_show,

                        //Total_no_of_class = Total_no_of_class,
                        //Present_class = present_class,
                        //Percent_of_attandance = percent_of_attandance,
                        //grade_head_text = grade_head_text,
                        //qr_code_Show = url,
                        //qr_div_true = qrShow,
                        //Remarkss = remarkss,
                        MySubjectMarkItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }


        private List<MySubjectMarkDetails> findmyBookingProduct(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            List<MySubjectMarkDetails> MySubjectMarkItem = new List<MySubjectMarkDetails>();
            string query = "select (select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and  Subject_id=t1.Subject_id) as Subject_Name,t1.Marks as Term_i_mark,t1.Full_marks as Term_i_full_mark,t2.Marks as Term_ii_mark,t2.Full_marks as Term_ii_full_mark, t1.Maximum_mark as Term_i_max_mark,t2.Maximum_mark as Term_ii_max_mark from Exam_temp_assesment_total_no t1 join Exam_temp_assesment_total_no_term_II t2 on t1.Session_id=t2.Session_id and t1.Branch_id=t2.Branch_id and t1.Class_id=t2.Class_id and t1.Subject_id=t2.Subject_id and t1.Admission_no=t2.Admission_no and t1.Exam_termwise_assesment_id=t2.Exam_termwise_assesment_id join Exam_Assessment_Details t3 on t1.Session_Id=t3.Session_Id and t1.Branch_Id=t3.Branch_Id and t1.Term_id=t3.Exam_Term_Id and t1.Class_id=t3.Class_id and t1.Assessment_id=t3.Assessment_Id where t1.Session_id=" + Session_id + " and  t1.Admission_no='" + Admission_no + "' and t1.Subject_id='" + Subject_id + "' and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " order by t3.Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Term_i_mark = "";
                    string Term_ii_mark = "";
                    string system_grade_id = Examination.get_system_grade_id_final(Session_id, Branch_id, Class_id);
                    string marks_output = Examination.get_marks_output(Session_id, Branch_id, system_grade_id);
                    if (marks_output == "Marks")
                    {
                        Term_i_mark = dr["Term_i_mark"].ToString();
                        Term_ii_mark = dr["Term_ii_mark"].ToString();
                    }
                    else
                    {
                        double term_i_ttl_mark = (My.toDouble(dr["Term_i_mark"].ToString()) * My.toDouble(dr["Term_i_max_mark"].ToString())) / My.toDouble(dr["Term_i_full_mark"].ToString());

                        double term_ii_ttl_mark = (My.toDouble(dr["Term_ii_mark"].ToString()) * My.toDouble(dr["Term_ii_max_mark"].ToString())) / My.toDouble(dr["Term_ii_full_mark"].ToString());

                        string term_I_grade = Examination.get_grade_final(Session_id, Branch_id, system_grade_id, term_i_ttl_mark);
                        string term_II_grade = Examination.get_grade_final(Session_id, Branch_id, system_grade_id, term_ii_ttl_mark);
                        Term_i_mark = term_I_grade;
                        Term_ii_mark = term_II_grade;
                    }

                    MySubjectMarkItem.Add(new MySubjectMarkDetails
                    {
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Marks_term_I = Term_i_mark,
                        Marks_term_II = Term_ii_mark,

                        Term_i_full_mark = dr["Term_i_full_mark"].ToString(),
                        Term_ii_full_mark = dr["Term_ii_full_mark"].ToString(),

                    });
                }
            }
            return MySubjectMarkItem;
        }



        //=======================================FETCH CO-SCHOLASTIC DATA
        //===========================================
        public class Fetch_Details_of_report_co_scholastic
        {
            public string Subject_name { get; set; }
            public string Term_I_grade { get; set; }
            public string Term_II_grade { get; set; }
            public string RowCount { get; set; }
        }

        List<Fetch_Details_of_report_co_scholastic> Show_of_report_co_scholastic = new List<Fetch_Details_of_report_co_scholastic>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_card_co_scholastic(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_idI, string Term_idII)
        {
            string query = "select DISTINCT t1.Subject_id,t2.Subject_name from Exam_coscholastic_and_activities_assesment_total_no t1 join Subject_Master t2 on t1.Class_id=t2.course_id and  t1.Subject_id=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Admission_no=" + Admission_no + " and t1.Entry_type='Co-Scholastic' and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_grade_master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string termI_grade = Examination.get_coscholastic_number(Session_id, Branch_id, Class_id, Admission_no, Term_idI, dr["Subject_id"].ToString());
                    string termII_grade = Examination.get_coscholastic_number(Session_id, Branch_id, Class_id, Admission_no, Term_idII, dr["Subject_id"].ToString());

                    Show_of_report_co_scholastic.Add(new Fetch_Details_of_report_co_scholastic
                    {
                        Subject_name = dr["Subject_name"].ToString(),
                        Term_I_grade = termI_grade,
                        Term_II_grade = termII_grade,
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_co_scholastic));
            }
        }

        //=======================================FETCH DESCIPLINE DATA
        //===========================================
        public class Fetch_Details_of_report_descipline
        {
            public string Subject_name { get; set; }
            public string Term_I_grade { get; set; }
            public string Term_II_grade { get; set; }
            public string RowCount { get; set; }
        }

        List<Fetch_Details_of_report_descipline> Show_of_report_descipline = new List<Fetch_Details_of_report_descipline>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_card_descipline(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_idI, string Term_idII)
        {
            string query = "select DISTINCT t1.Assessment_id,t2.Activity_name from Exam_coscholastic_and_activities_assesment_total_no t1 join Exam_Personality_Traits t2 on t1.Assessment_id=t2.Activity_Id where t1.Session_id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Admission_no=" + Admission_no + " and t1.Entry_type='DISCIPLINE'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_grade_master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string termI_grade = Examination.get_descipline_number(Session_id, Branch_id, Class_id, Admission_no, Term_idI, dr["Assessment_id"].ToString());
                    string termII_grade = Examination.get_descipline_number(Session_id, Branch_id, Class_id, Admission_no, Term_idII, dr["Assessment_id"].ToString());

                    Show_of_report_descipline.Add(new Fetch_Details_of_report_descipline
                    {
                        Subject_name = dr["Activity_name"].ToString(),
                        Term_I_grade = termI_grade,
                        Term_II_grade = termII_grade,
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_descipline));
            }
        }


        //=======================================FETCH ASSESMENT FULL NAME
        //===========================================
        public class Fetch_Details_of_report_assmnt_full_name
        {
            public string Short_name { get; set; }
            public string Full_name { get; set; }
        }

        List<Fetch_Details_of_report_assmnt_full_name> Show_of_report_assmnt_full_name = new List<Fetch_Details_of_report_assmnt_full_name>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_assesment_full_name()
        {
            string query = "select DISTINCT Short_Name,Assessment_Name from Exam_Assessment_Details where Scholastic_Co_scholastic='Scholastic'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_Assessment_Details");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_report_assmnt_full_name.Add(new Fetch_Details_of_report_assmnt_full_name
                    {
                        Short_name = dr["Short_Name"].ToString(),
                        Full_name = dr["Assessment_Name"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_assmnt_full_name));
            }
        }
    }
}
