using school_web.AppCode;
using school_web.AppCode.Exam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Examination_Admin.slip.ips.api
{
    /// <summary>
    /// Summary description for final_report_card_ix
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class final_report_card_ix : System.Web.Services.WebService
    {
        public class Fetch_Details_of_report_head
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
            public string Sub_level_count { get; set; }
            public string RowSpan { get; set; }
        }

        List<Fetch_Details_of_report_head> Show_of_report_head = new List<Fetch_Details_of_report_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_card_assesment(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string query = "select Sequence_No,Session_Id,Branch_Id,Exam_Term_Id,Assessment_Name,Grade_System_Id,Class_id,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
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
                    string rows_of_sub_level = get_rows_of_sub_level(dr["Session_Id"].ToString(), dr["Branch_Id"].ToString(), dr["Exam_Term_Id"].ToString(), dr["Grade_System_Id"].ToString(), dr["Assessment_Id"].ToString(), dr["Class_id"].ToString());
                    string rowSpan = ""; string colSpan = "";
                    if (dr["Sequence_No"].ToString() == "1")
                    {
                        rowSpan = "2";
                        rows_of_sub_level = "1";
                    }
                    string extra_marks = exam_ips_ix.get_max_marks_in_head(Session_id, Class_id, Branch_id, Term_id, dr["Assessment_Id"].ToString());

                    Show_of_report_head.Add(new Fetch_Details_of_report_head
                    {
                        Assessment_Name = dr["Assessment_Name"].ToString(),
                        Short_Name = dr["Short_Name"].ToString(),
                        Maximum_Marks = dr["Maximum_Marks"].ToString(),
                        Term_Maximum_Marks = dr["Term_Maximum_Marks"].ToString(),
                        Sub_level_count = rows_of_sub_level,
                        RowSpan = rowSpan,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_head));
            }
        }

        private string get_rows_of_sub_level(string Session_id, string Branch_id, string Term_id, string System_grade_id, string Assesment_id, string Class_id)
        {
            string query = "select DISTINCT Subject_Activity_Name from Exam_Subject_Sub_Level where Session_Id=" + Session_id + " and Branch_Id='" + Branch_id + "' and Istatus=1 and Exam_Term_Id=" + Term_id + " and Grade_System_Id=" + System_grade_id + " and Assessment_Id=" + Assesment_id + " and Class_id=" + Class_id + "";
            DataTable dt = My.dataTable(query);
            return dt.Rows.Count.ToString();
        }


        public class Fetch_Details_of_report_head_subj
        {
            public string Subject_Activity_Name { get; set; }
        }

        List<Fetch_Details_of_report_head_subj> Show_of_report_head_subj = new List<Fetch_Details_of_report_head_subj>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_card(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string querys = "select Sequence_No,Session_Id,Branch_Id,Exam_Term_Id,Assessment_Name,Grade_System_Id,Class_id,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id=" + Session_id + " and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            DataTable dts = My.dataTable(querys);
            foreach (DataRow drs in dts.Rows)
            {

                string query = "select DISTINCT Subject_Activity_Name from Exam_Subject_Sub_Level where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Istatus='1' and  Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_id + " and Assessment_Id='" + drs["Assessment_Id"].ToString() + "' and Grade_System_Id='" + drs["Grade_System_Id"].ToString() + "'";
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
                    if (drs["Sequence_No"].ToString() != "1")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Show_of_report_head_subj.Add(new Fetch_Details_of_report_head_subj
                            {
                                Subject_Activity_Name = dr["Subject_Activity_Name"].ToString(),
                            });
                        }
                    }
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_of_report_head_subj));
        }

        //======================================
        //=======================================================
        public class MySubjectMarksShow
        {
            public string Subject { get; set; }
            public string Subject_name { get; set; }
            public string Total_marks { get; set; }
            public string Grade { get; set; }

            public string Total_marksTII { get; set; }
            public string GradeTII { get; set; }
            public string TermI_termII_average_mark { get; set; }
            public string TermI_termII_average_grade { get; set; }

            public string Heighst_mrk { get; set; }

            public string Overall_obt_mark { get; set; }
            public string Overall_full_mark { get; set; }
            public string Overall_percent { get; set; }
            public string Is_attandance_show { get; set; }
            public string Total_no_of_class { get; set; }
            public string Present_class { get; set; }
            public string Percent_of_attandance { get; set; }
            public string grade_head_text { get; set; }
            public string qr_code_Show { get; set; }
            public string qr_div_true { get; set; }
            public string Remarkss { get; set; }
            public string SpecialNote { get; set; }
            public string Rank { get; set; }
            public string Instruction2 { get; set; }
            public string Ranksdv { get; set; }
            public string Graph { get; set; }
            public string GraphHeight { get; set; }
            public string Prcnt_remark { get; set; }
            public string Sign_top { get; set; }
            public string Sign_bottom { get; set; }
            public string Max_mark_show { get; set; }

            public string Is_estd_show { get; set; }
            public string Is_contact_no_show { get; set; }
            public string Is_email_show { get; set; }
            public string Is_class_text_show { get; set; }
            public string Height_dv { get; set; }
            public string Class_in_new_line { get; set; }

            public string Co_sch_area_margn { get; set; }
            public string Overall_area_margn { get; set; }
            public string Percent_remark_area_margn { get; set; }
            public string Graph_area_margn { get; set; }
            public string Ins1_area_margn { get; set; }
            public string Ins2_area_margn { get; set; }
            public string Toppers_area_margn { get; set; }
            public string Is_watermark_show { get; set; }

            public string Is_std_img_hide { get; set; }
            public string Is_std_section_hide { get; set; }
            public string Aff_text { get; set; }
            public string Father_name1 { get; set; }
            public string Father_name2 { get; set; }
            public string Is_subj_code_hide { get; set; }
            public List<MySubjectMarkDetails> MySubjectMarkItem { get; set; }
            public List<MySubjectMarkDetailsTII> MySubjectMarkItemTII { get; set; }
        }

        public class MySubjectMarkDetails
        {
            public string Marks { get; set; }
            public string Subject { get; set; }
            public string Total_marks { get; set; }
        }
        public class MySubjectMarkDetailsTII
        {
            public string Marks { get; set; }
            public string Subject { get; set; }
            public string Total_marks { get; set; }
        }

        List<MySubjectMarksShow> EMySubMark = new List<MySubjectMarksShow>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_subjects(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_idI, string Term_idII)
        {
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_Code,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_idI + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' order by t2.Subject_position asc";
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
                    List<MySubjectMarkDetails> MBdetails = findmyBookingProduct(dr["Subject"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idI);
                    List<MySubjectMarkDetailsTII> MBdetailsTII = findmyBookingProductTII(dr["Subject"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idII);

                    string Total_marks = Examination_P4.get_subject_total_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Term"].ToString(), dr["Subject"].ToString(), dr["Branch_id"].ToString(), "Scholastic");

                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);
                    string obt_subject_mark = arrs[0];
                    string subject_grade = arrs[1];
                    string overall_obt_marks = arrs[2];
                    string overall_full_marks = arrs[3];
                    string overall_percent = arrs[4];
                    string is_attandance_show = arrs[5];

                    string Total_no_of_class = arrs[6]; //Total Class
                    string present_class = arrs[7];  // Present Class
                    string percent_of_attandance = arrs[8];   // Percent of attendance
                    string grade_head_text = arrs[9];   // Head Text
                    string remarkss = arrs[10];   // REMARKSS

                    string specialNote = arrs[11];   // SpecialNote
                    string qrShow = arrs[12];   // QRS

                    string instruction2 = arrs[13];   // Instruction2
                    string ranks = arrs[14];   // Ranks
                    string graph = arrs[15];   // Graph
                    string graphHeight = arrs[16];   // GraphHeight
                    string prcnt_remark = arrs[17];   // PercentRemark

                    string sign_top = arrs[18];   // SignTop
                    string sign_bottom = arrs[19];   // SignBottom
                    string max_mark_show = arrs[20];   // IsMaxMarkShow

                    string is_estd_show = arrs[21];   // IsESTDShow
                    string is_contact_no_show = arrs[22];   // IsContactNoShow
                    string is_email_show = arrs[23];   // IsEmailId
                    string is_class_text_show = arrs[24];   // IsClassTextShow
                    string height_dv = arrs[25];   // ReHeightDV
                    string class_in_new_line = arrs[26];   // ReHeightDV

                    //MARGIN
                    string co_sch_area_margn = arrs[27];   // co_sch_area_margn
                    string overall_area_margn = arrs[28];   // overall_area_margn
                    string percent_remark_area_margn = arrs[29];   // percent_remark_area_margn
                    string graph_area_margn = arrs[30];   // graph_area_margn
                    string ins1_area_margn = arrs[31];   // instruction1_area_margn
                    string ins2_area_margn = arrs[32];   // instruction2_area_margn
                    string toppers_area_margn = arrs[33];   // toppers_area_margn
                    string is_watermark_show = arrs[34];   // Watermark Show

                    string is_std_img_hide = arrs[35];   // StudentImgHide
                    string is_std_section_hide = arrs[36];   // SectionHide
                    string aff_text = arrs[37];   // AffText
                    string father_name1 = arrs[38];   // FatherName1
                    string father_name2 = arrs[39];   // FatherName2
                    string is_subj_code_hide = arrs[40];   // SubjectCode
                    //QQQQQRRRR 
                    string url = "";
                    if (overall_obt_marks == "hidden")
                    {
                        url = "https://api.qrserver.com/v1/create-qr-code/?data=Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Overall Percentage: " + overall_percent + "%" + "&amp;size=110x110";
                    }
                    else
                    {
                        url = "https://api.qrserver.com/v1/create-qr-code/?data=Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Total Obtained Mark: " + overall_obt_marks + "/" + overall_full_marks + ", Overall Percentage: " + overall_percent + "%" + "&amp;size=110x110";
                    }

                    string rank = "0";

                    //================T2
                    string Total_marksTII = Examination_P4.get_subject_total_marksTII(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), Term_idII, dr["Subject"].ToString(), dr["Branch_id"].ToString(), "Scholastic");
                    string[] stringSeparatorssz = new string[] { "/" };
                    string[] arrsz = Total_marksTII.Split(stringSeparatorssz, StringSplitOptions.None);
                    string obt_subject_markTII = arrsz[0];
                    string subject_gradeTII = arrsz[1];
                    string Grade_head_text = arrsz[2];
                    string Grade_System_Id = arrsz[3];
                    double average_mark = (My.toDouble(obt_subject_mark) + My.toDouble(obt_subject_markTII)) / 2;
                    string average_mark_f = Math.Round(average_mark, 1).ToString("0.0");
                    string Average_grade = Exam_setting.get_grade_of_a_subject(Session_id, Branch_id, Grade_System_Id, Math.Round(average_mark), 100, Class_id);   // Grade of a subject

                    string heighst_mrk = get_highest_mark(Session_id, Class_id, Branch_id, dr["Subject"].ToString(), Admission_no);

                    EMySubMark.Add(new MySubjectMarksShow
                    {
                        Subject = dr["Subject_Code"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        Total_marks = obt_subject_mark,
                        Grade = subject_grade,

                        //T2
                        Total_marksTII = obt_subject_markTII,
                        GradeTII = subject_gradeTII,
                        TermI_termII_average_mark = average_mark_f,
                        TermI_termII_average_grade = Average_grade,

                        Heighst_mrk = heighst_mrk,

                        Overall_obt_mark = overall_obt_marks,
                        Overall_full_mark = overall_full_marks,
                        Overall_percent = overall_percent,
                        Is_attandance_show = is_attandance_show,





                        Total_no_of_class = Total_no_of_class,
                        Present_class = present_class,
                        Percent_of_attandance = percent_of_attandance,
                        grade_head_text = grade_head_text,
                        qr_code_Show = url,
                        qr_div_true = qrShow,
                        Remarkss = remarkss,
                        SpecialNote = specialNote,
                        Rank = rank,
                        Instruction2 = instruction2,
                        Ranksdv = ranks,
                        Graph = graph,
                        GraphHeight = graphHeight,
                        Prcnt_remark = prcnt_remark,

                        Sign_top = sign_top,
                        Sign_bottom = sign_bottom,
                        Max_mark_show = max_mark_show,

                        Is_estd_show = is_estd_show,
                        Is_contact_no_show = is_contact_no_show,
                        Is_email_show = is_email_show,
                        Is_class_text_show = is_class_text_show,
                        Height_dv = height_dv,
                        Class_in_new_line = class_in_new_line,

                        Co_sch_area_margn = co_sch_area_margn,
                        Overall_area_margn = overall_area_margn,
                        Percent_remark_area_margn = percent_remark_area_margn,
                        Graph_area_margn = graph_area_margn,
                        Ins1_area_margn = ins1_area_margn,
                        Ins2_area_margn = ins2_area_margn,
                        Toppers_area_margn = toppers_area_margn,
                        Is_watermark_show = is_watermark_show,

                        Is_std_img_hide = is_std_img_hide,
                        Is_std_section_hide = is_std_section_hide,
                        Aff_text = aff_text,
                        Father_name1 = father_name1,
                        Father_name2 = father_name2,
                        Is_subj_code_hide = is_subj_code_hide,

                        MySubjectMarkItem = MBdetails,
                        MySubjectMarkItemTII = MBdetailsTII
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }

        //==Session_id, Class_id, Branch_id, dr["Subject_id"].ToString(), Admission_no
        private string get_highest_mark(string Session_id, string Class_id, string Branch_id, string Subject_id, string Admission_no)
        {
            string ReturN = "0";
            DataTable dt = mycode.FillData("select Section from admission_registor where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and admissionserialnumber='" + Admission_no + "'");
            if (dt.Rows.Count > 0)
            {
                DataTable dts = mycode.FillData("select Marks from Exam_highest_mark_of_subject where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Section='" + dt.Rows[0][0].ToString() + "' and Subject_id='" + Subject_id + "'");
                if (dts.Rows.Count > 0)
                {
                    ReturN = dts.Rows[0]["Marks"].ToString();
                }
            }
            return ReturN;
        }


        private List<MySubjectMarkDetails> findmyBookingProduct(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            List<MySubjectMarkDetails> MySubjectMarkItem = new List<MySubjectMarkDetails>();
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Assessment_Name,t1.Sequence_No,t1.Term_assesment_unique_id  from Exam_Assessment_Details t1 where t1.Session_Id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Scholastic_Co_scholastic='Scholastic' order by t1.Sequence_No asc";
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
                    if (dr["Sequence_No"].ToString() != "1")
                    {
                        string marks = Examination_P4.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", "1");

                        string[] stringSeparatorss = new string[] { "/" };
                        string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                        string obt_marks = arrs[0];
                        string full_marks = arrs[1];
                        string marks_type = arrs[2];
                        string total_no = arrs[3];
                        string obt_marksss = arrs[5];

                        save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marksss, full_marks, marks_type, total_no, dr["Term_assesment_unique_id"].ToString());

                        MySubjectMarkItem.Add(new MySubjectMarkDetails
                        {
                            Marks = obt_marks,
                            Subject = Subject_id,
                            Total_marks = "0",
                        });


                        //=============================
                        string markss = Examination_P4.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", "2");

                        string[] stringSeparatorsss = new string[] { "/" };
                        string[] arrss = markss.Split(stringSeparatorsss, StringSplitOptions.None);
                        string obt_markss = arrss[0];
                        string full_markss = arrss[1];
                        string marks_types = arrss[2];
                        string total_nos = arrss[3];
                        string obt_markssss = arrss[5];

                        save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_markssss, full_markss, marks_types, total_no, dr["Term_assesment_unique_id"].ToString());

                        MySubjectMarkItem.Add(new MySubjectMarkDetails
                        {
                            Marks = obt_markss,
                            Subject = Subject_id,
                            Total_marks = "0",
                        });

                    }
                    else
                    {
                        string marks = Examination_P4.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", "0");

                        string[] stringSeparatorss = new string[] { "/" };
                        string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                        string obt_marks = arrs[0];
                        string full_marks = arrs[1];

                        string marks_type = arrs[2];
                        string total_no = arrs[3];


                        MySubjectMarkItem.Add(new MySubjectMarkDetails
                        {
                            Marks = obt_marks,
                            Subject = Subject_id,
                            Total_marks = "0",
                        });
                    }
                }
            }
            return MySubjectMarkItem;
        }


        private List<MySubjectMarkDetailsTII> findmyBookingProductTII(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            List<MySubjectMarkDetailsTII> MySubjectMarkItemTII = new List<MySubjectMarkDetailsTII>();
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Assessment_Name,t1.Sequence_No,t1.Term_assesment_unique_id from Exam_Assessment_Details t1 where t1.Session_Id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Scholastic_Co_scholastic='Scholastic' order by t1.Sequence_No asc";
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
                    if (dr["Sequence_No"].ToString() != "1")
                    {
                        string marks = Examination_P4.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", "1");

                        string[] stringSeparatorss = new string[] { "/" };
                        string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                        string obt_marks = arrs[0];
                        string full_marks = arrs[1];
                        string marks_type = arrs[2];
                        string total_no = arrs[3];
                        string obt_marksss = arrs[5];

                        save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marksss, full_marks, marks_type, total_no, dr["Term_assesment_unique_id"].ToString());

                        MySubjectMarkItemTII.Add(new MySubjectMarkDetailsTII
                        {
                            Marks = obt_marks,
                            Subject = Subject_id,
                            Total_marks = "0",
                        });


                        //=============================
                        string markss = Examination_P4.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", "2");

                        string[] stringSeparatorsss = new string[] { "/" };
                        string[] arrss = markss.Split(stringSeparatorsss, StringSplitOptions.None);
                        string obt_markss = arrss[0];
                        string full_markss = arrss[1];
                        string marks_types = arrss[2];
                        string total_nos = arrss[3];
                        string obt_markssss = arrss[5];

                        save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_markssss, full_markss, marks_types, total_no, dr["Term_assesment_unique_id"].ToString());

                        MySubjectMarkItemTII.Add(new MySubjectMarkDetailsTII
                        {
                            Marks = obt_markss,
                            Subject = Subject_id,
                            Total_marks = "0",
                        });

                    }
                    else
                    {
                        string marks = Examination_P4.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", "0");

                        string[] stringSeparatorss = new string[] { "/" };
                        string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                        string obt_marks = arrs[0];
                        string full_marks = arrs[1];

                        string marks_type = arrs[2];
                        string total_no = arrs[3];


                        MySubjectMarkItemTII.Add(new MySubjectMarkDetailsTII
                        {
                            Marks = obt_marks,
                            Subject = Subject_id,
                            Total_marks = "0",
                        });
                    }
                }
            }
            return MySubjectMarkItemTII;
        }


        My mycode = new My();
        private void save_marks(string session_id, string class_id, string admission_no, string term_id, string assesment_id, string subject_id, string Branch_id, string marks, string full_marks, string marks_type, string total_no, string term_assesment_unique_id)
        {
            try
            {
                bool tot = mycode.cheknumer_Double(marks);
                if (tot == false)
                {
                    marks = "0";
                }

            }
            catch
            {

            }
            try
            {
                bool tot1 = mycode.cheknumer_Double(total_no);
                if (tot1 == false)
                {
                    total_no = "0";
                }
            }
            catch
            {
            }

            string first_term = Exam_setting.get_first_term(session_id, Branch_id, class_id);
            if (first_term == term_id)
            {
                if (mycode.IsUserExist("select Id from Exam_temp_assesment_total_no where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'"))
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_temp_assesment_total_no (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Exam_termwise_assesment_id) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Exam_termwise_assesment_id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", session_id);
                    cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                    cmd.Parameters.AddWithValue("@Class_id", class_id);
                    cmd.Parameters.AddWithValue("@Term_id", term_id);
                    cmd.Parameters.AddWithValue("@Assessment_id", assesment_id);
                    cmd.Parameters.AddWithValue("@Subject_id", subject_id);
                    cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                    if (marks_type == "Marks")
                    {
                        cmd.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", term_assesment_unique_id);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Exam_temp_assesment_total_no set Marks=@Marks,Full_marks=@Full_marks,Exam_termwise_assesment_id=@Exam_termwise_assesment_id where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
                    cmd = new SqlCommand(query);
                    if (marks_type == "Marks")
                    {
                        cmd.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", term_assesment_unique_id);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
            else
            {
                if (mycode.IsUserExist("select Id from Exam_temp_assesment_total_no_term_II where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'"))
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_temp_assesment_total_no_term_II (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Exam_termwise_assesment_id) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Exam_termwise_assesment_id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", session_id);
                    cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                    cmd.Parameters.AddWithValue("@Class_id", class_id);
                    cmd.Parameters.AddWithValue("@Term_id", term_id);
                    cmd.Parameters.AddWithValue("@Assessment_id", assesment_id);
                    cmd.Parameters.AddWithValue("@Subject_id", subject_id);
                    cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                    if (marks_type == "Marks")
                    {
                        cmd.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", term_assesment_unique_id);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Exam_temp_assesment_total_no_term_II set Marks=@Marks,Full_marks=@Full_marks,Exam_termwise_assesment_id=@Exam_termwise_assesment_id where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
                    cmd = new SqlCommand(query);
                    if (marks_type == "Marks")
                    {
                        cmd.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", term_assesment_unique_id);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
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


        //=================================TOTAl NO.
        public class Fetch_Details_of_total
        {
            public string Overall_obt_marks { get; set; }
            public string Overall_full_marks { get; set; }
            public string Overall_percents { get; set; }
            public string Mark_type { get; set; }
            public string P_remark { get; set; }
            public string Grade { get; set; }
        }

        List<Fetch_Details_of_total> Show_of_total = new List<Fetch_Details_of_total>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_total_no(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string Total_marks = exam_ips_ix.get_overall_total_marks_final(Session_id, Class_id, Admission_no, Branch_id, Term_id);
            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);

            string overall_obt_marks = arrs[0];
            string overall_full_marks = arrs[1];
            string overall_percent = arrs[2];
            string MarkType = arrs[3];
            string grade = arrs[4];

            string percent_remark = exam_ips_ix.get_percent_remark(overall_percent);
            Show_of_total.Add(new Fetch_Details_of_total
            {
                Overall_obt_marks = overall_obt_marks,
                Overall_full_marks = overall_full_marks,
                Overall_percents = overall_percent,
                Mark_type = MarkType,
                P_remark = percent_remark,
                Grade = grade,
            });
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_of_total));
        }



        //=================================ATTENDANCE
        public class Fetch_Details_of_attendance
        {
            public string Total_class_of_term_I { get; set; }
            public string Total_Present_class_of_term_I { get; set; }
            public string Total_Precentage_class_of_term_I { get; set; }

            public string Total_class_of_term_II { get; set; }
            public string Total_Present_class_of_term_II { get; set; }
            public string Total_Precentage_class_of_term_II { get; set; }

            public string Final_working_days { get; set; }
            public string Final_persent_days { get; set; }
            public string Final_percent_days { get; set; }
        }

        List<Fetch_Details_of_attendance> Show_of_attendance = new List<Fetch_Details_of_attendance>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_attendance(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id, string Term_idII)
        {
            string attendance = exam_ips_ix.get_attendances(Session_id, Class_id, Admission_no, Branch_id, Term_id, Term_idII);

            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = attendance.Split(stringSeparatorss, StringSplitOptions.None);

            string Total_class_of_term_I = arrs[0];
            string Total_Present_class_of_term_I = arrs[1];
            string Total_Precentage_class_of_term_I = arrs[2];

            string Total_class_of_term_II = arrs[3];
            string Total_Present_class_of_term_II = arrs[4];
            string Total_Precentage_class_of_term_II = arrs[5];

            double final_working_days = My.toDouble(Total_class_of_term_I) + My.toDouble(Total_class_of_term_II);
            double final_persent_days = My.toDouble(Total_Present_class_of_term_I) + My.toDouble(Total_Present_class_of_term_II);
            double final_percent_days = (final_persent_days / final_working_days) * 100;

            Show_of_attendance.Add(new Fetch_Details_of_attendance
            {
                Total_class_of_term_I = Total_class_of_term_I,
                Total_Present_class_of_term_I = Total_Present_class_of_term_I,
                Total_Precentage_class_of_term_I = Math.Round(My.toDouble(Total_Precentage_class_of_term_I)).ToString(),

                Total_class_of_term_II = Total_class_of_term_II,
                Total_Present_class_of_term_II = Total_Present_class_of_term_II,
                Total_Precentage_class_of_term_II = Math.Round(My.toDouble(Total_Precentage_class_of_term_II)).ToString(),

                Final_working_days = final_working_days.ToString(),
                Final_persent_days = final_persent_days.ToString(),
                Final_percent_days = Math.Round(final_percent_days).ToString(),
            });
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_of_attendance));
        }


        //COSCHOLESTIC
        private void prep_final_term_coscholestic(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic' order by t2.Subject_position asc";

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
                    string marks = Examination.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Term"].ToString(), dr["Assessment"].ToString(), dr["Subject"].ToString(), dr["Branch_id"].ToString(), "Co-Scholastic");

                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                    string obt_marks = arrs[0];
                    string full_marks = arrs[1];
                    string maximum_mark = arrs[2];

                    save_coscholastic_areas(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Term"].ToString(), dr["Assessment"].ToString(), dr["Subject"].ToString(), dr["Branch_id"].ToString(), "Co-Scholastic", obt_marks, full_marks, maximum_mark);

                }
            }
        }


        private void save_coscholastic_areas(string session_id, string class_id, string admission_no, string term_id, string assesment_id, string subject_id, string Branch_id, string assesment_type, string obt_marks, string full_marks, string maximum_mark)
        {
            if (mycode.IsUserExist("select Id from Exam_coscholastic_and_activities_assesment_total_no where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "' and Entry_type='" + assesment_type + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Exam_coscholastic_and_activities_assesment_total_no (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Entry_type,Maximum_mark) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Entry_type,@Maximum_mark)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Term_id", term_id);
                cmd.Parameters.AddWithValue("@Assessment_id", assesment_id);
                cmd.Parameters.AddWithValue("@Subject_id", subject_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd.Parameters.AddWithValue("@Marks", obt_marks);
                cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                cmd.Parameters.AddWithValue("@Entry_type", assesment_type);
                cmd.Parameters.AddWithValue("@Maximum_mark", maximum_mark);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Exam_coscholastic_and_activities_assesment_total_no set Marks=@Marks,Full_marks=@Full_marks,Maximum_mark=@Maximum_mark where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Marks", obt_marks);
                cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                cmd.Parameters.AddWithValue("@Entry_type", assesment_type);
                cmd.Parameters.AddWithValue("@Maximum_mark", maximum_mark);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }



        //DECIPLINES
        private void prep_final_term_decipline(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_idI)
        {
            string query = "select ptt.*,pt.Activity_name from Exam_Personality_Traits_Term_Wise ptt join Exam_Personality_Traits pt on ptt.Activity_Id=pt.Activity_Id where ptt.Session_id=" + Session_id + " and ptt.Branch_id='" + Branch_id + "' and ptt.Class_id=" + Class_id + " and ptt.Exam_Term_Id=" + Term_idI + " and ptt.Admission_no='" + Admission_no + "'  order by pt.Position asc";

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_Personality_Traits_Term_Wise");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string grade = "";
                    bool isNum = valid_amount(dr["Term_grade"].ToString());
                    if (isNum == true)
                    {
                        grade = Examination.get_personality_grade(Session_id, Class_id, Branch_id, dr["Term_grade"].ToString());
                    }
                    else
                    {
                        grade = dr["Term_grade"].ToString();
                    }
                    save_coscholastic_areas(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Exam_Term_Id"].ToString(), dr["Activity_Id"].ToString(), "0", dr["Branch_id"].ToString(), "DISCIPLINE", grade, "0", "0");
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
    }
}
