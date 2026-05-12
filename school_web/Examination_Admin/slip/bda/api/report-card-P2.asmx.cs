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

namespace school_web.Examination_Admin.slip.bda.api
{
    /// <summary>
    /// Summary description for report_card_P2
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class report_card_P2 : System.Web.Services.WebService
    {
        My mycode = new My();
        public class Fetch_Details_of_report_head_assesment
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
        }

        List<Fetch_Details_of_report_head_assesment> Show_of_report_head_assesment = new List<Fetch_Details_of_report_head_assesment>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_card_assesment(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
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
                    Show_of_report_head_assesment.Add(new Fetch_Details_of_report_head_assesment
                    {
                        Assessment_Name = dr["Assessment_Name"].ToString(),
                        Short_Name = dr["Short_Name"].ToString(),
                        Maximum_Marks = dr["Maximum_Marks"].ToString(),
                        Term_Maximum_Marks = dr["Term_Maximum_Marks"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_head_assesment));
            }
        }


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
                        Short_Name = "Full Marks",
                    });
                    Show_of_report_head.Add(new Fetch_Details_of_report_head
                    {
                        Short_Name = "marks obtained",
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
            public string Total_marks { get; set; }
            public string Grade { get; set; }
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

            public string Marks_type { get; set; }
            public string P_remark { get; set; }
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
            public string Is_text_center { get; set; }
            public string Is_website_show { get; set; }
            public List<MySubjectMarkDetails> MySubjectMarkItem { get; set; }
        }

        public class MySubjectMarkDetails
        {
            public string Marks { get; set; }
            public string Subject_Name { get; set; }
            public string Subject { get; set; }
            public string Total_marks { get; set; }
            public string Full_marks { get; set; }
        }


        List<MySubjectMarksShow> EMySubMark = new List<MySubjectMarksShow>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_subjects(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            My.exeSql("delete from Exam_temp_assesment_total_no where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Term_id='" + Term_id + "' and Admission_no='" + Admission_no + "'; delete from Exam_temp_assesment_total_no_term_II where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Term_id='" + Term_id + "' and Admission_no='" + Admission_no + "'");
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_Code,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section,isnull((select top 1 Rank from Exam_rank_master where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and Admission_no=t1.Admission_no and Term_id=" + Term_id + "),'0') as Rank from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' and t1.Subject in (select Sub_id from Subject_Mapping_New where Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Session_id='" + Session_id + "') order by t2.Subject_position asc";
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
                    List<MySubjectMarkDetails> MBdetails = findmyBookingProduct(dr["Subject"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_id);

                    string Total_marks = bda_examination_P2.get_subject_total_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Term"].ToString(), dr["Subject"].ToString(), dr["Branch_id"].ToString(), "Scholastic");

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
                    string is_text_center = arrs[41];   // IsTExtCenter
                    string Is_website_show = arrs[42];   // IsWebsiteShow

                    string percent_remark = Examination.get_percent_remark(overall_percent);
                    //QQQQQRRRR 
                    string url = "";
                    string markType = "";
                    if (overall_obt_marks == "hidden")
                    {
                        markType = "GRADE";
                        url = "https://api.qrserver.com/v1/create-qr-code/?data=Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Overall Percentage: " + overall_percent + "%" + "&amp;size=110x110";
                    }
                    else
                    {
                        markType = "PERCENTAGE";
                        url = "https://api.qrserver.com/v1/create-qr-code/?data=Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Total Obtained Mark: " + overall_obt_marks + "/" + overall_full_marks + ", Overall Percentage: " + overall_percent + "%" + "&amp;size=110x110";
                    }

                    string rank = "0";
                    if (dr["Rank"].ToString() == "1")
                    {
                        rank = dr["Rank"].ToString() + "st";
                    }
                    else if (dr["Rank"].ToString() == "2")
                    {
                        rank = dr["Rank"].ToString() + "nd";
                    }
                    else if (dr["Rank"].ToString() == "3")
                    {
                        rank = dr["Rank"].ToString() + "rd";
                    }
                    else
                    {
                        rank = dr["Rank"].ToString() + "th";
                    }

                    EMySubMark.Add(new MySubjectMarksShow
                    {
                        Subject = dr["Subject_Code"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        Total_marks = obt_subject_mark,
                        Grade = subject_grade,
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

                        Marks_type = markType,
                        Prcnt_remark = prcnt_remark,
                        P_remark = percent_remark,

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
                        Is_text_center = is_text_center,
                        Is_website_show = Is_website_show,
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

            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Assessment_Name,t1.Sequence_No from Exam_Assessment_Details t1 where t1.Session_Id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Scholastic_Co_scholastic='Scholastic' order by t1.Sequence_No asc";
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
                string subject_name = bda_examination_P2.get_subject_name(Subject_id, Class_id, Branch_id);
                foreach (DataRow dr in dt.Rows)
                {
                    string marks = bda_examination_P2.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic");

                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                    string obt_marks = arrs[0];
                    string full_marks = arrs[1];

                    string marks_type = arrs[2];
                    string total_no = arrs[3];

                    if (obt_marks == "")
                    {
                        obt_marks = "-";
                    }
                    else
                    {
                        save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marks, full_marks, marks_type, total_no);
                    }

                    MySubjectMarkItem.Add(new MySubjectMarkDetails
                    {
                        Marks = full_marks,
                        Subject_Name = subject_name,
                        Subject = Subject_id,
                        Total_marks = "0",
                        Full_marks = full_marks,
                    });
                    MySubjectMarkItem.Add(new MySubjectMarkDetails
                    {
                        Marks = obt_marks,
                        Subject_Name = subject_name,
                        Subject = Subject_id,
                        Total_marks = "0",
                        Full_marks = full_marks,
                    });
                }
            }
            return MySubjectMarkItem;
        }


        private void save_marks(string session_id, string class_id, string admission_no, string term_id, string assesment_id, string subject_id, string Branch_id, string marks, string full_marks, string marks_type, string total_no)
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

            string first_term = bda_exam_setting.get_first_term(session_id, Branch_id, class_id);
            if (first_term == term_id)
            {
                if (mycode.IsUserExist("select Id from Exam_temp_assesment_total_no where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'"))
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_temp_assesment_total_no (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks)";
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
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Exam_temp_assesment_total_no set Marks=@Marks,Full_marks=@Full_marks where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
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
                    string query = "INSERT INTO Exam_temp_assesment_total_no_term_II (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks)";
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
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Exam_temp_assesment_total_no_term_II set Marks=@Marks,Full_marks=@Full_marks where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
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
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
        }


        public class Fetch_Details_of_report_marks_range
        {
            public string Lower_Range { get; set; }
            public string Upper_Range { get; set; }
            public string Grade { get; set; }
            public string RowCount { get; set; }
        }

        List<Fetch_Details_of_report_marks_range> Show_of_report_marks_range = new List<Fetch_Details_of_report_marks_range>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_marks_range(string Session_id, string Branch_id, string Term_id)
        {
            string query = "select gsrg.* from Exam_Grade_System_Range_Grade gsrg join Exam_Term_Details td on gsrg.Session_Id=td.Session_Id and gsrg.Branch_Id=td.Branch_Id and gsrg.Grade_System_Id=td.Grade_System_Id where td.Exam_Term_Id=" + Term_id + " and gsrg.Session_Id=" + Session_id + " and gsrg.Branch_Id='" + Branch_id + "' order by Lower_Range desc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_Grade_System_Range_Grade");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_report_marks_range.Add(new Fetch_Details_of_report_marks_range
                    {
                        Lower_Range = dr["Lower_Range"].ToString(),
                        Upper_Range = dr["Upper_Range"].ToString(),
                        Grade = dr["Grade"].ToString(),
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_marks_range));
            }
        }

        //===========================================
        public class Fetch_Details_of_report_grade_remark
        {
            public string Grade_name { get; set; }
            public string Grade_remark { get; set; }
            public string RowCount { get; set; }
        }

        List<Fetch_Details_of_report_grade_remark> Show_of_report_grade_remark = new List<Fetch_Details_of_report_grade_remark>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_grade_remark()
        {
            string query = "select * from Exam_grade_master";
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
                    Show_of_report_grade_remark.Add(new Fetch_Details_of_report_grade_remark
                    {
                        Grade_name = dr["Grade_name"].ToString(),
                        Grade_remark = dr["Grade_remark"].ToString(),
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_grade_remark));
            }
        }




        //===========================================SIGNATURES
        public class Fetch_Details_of_signature
        {
            public string Signature_name { get; set; }
            public string Signature_image { get; set; }
        }

        List<Fetch_Details_of_signature> Show_of_report_signature = new List<Fetch_Details_of_signature>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_signature(string Session_id, string Class_id, string Admission_no, string Branch_id)
        {
            string query = "select * from Exam_signature_setting where Branch_id='" + Branch_id + "' order by Position asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_signature_setting");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string Section = Examination_P2.get_student_section(Session_id, Admission_no, Branch_id, Class_id);
                foreach (DataRow dr in dt.Rows)
                {
                    string stgnature = "";
                    if (dr["Signature_type"].ToString() == "1")
                    {
                        stgnature = Examination_P2.get_class_teacher_signature(Session_id, Class_id, Section, Branch_id);
                    }
                    if (dr["Signature_type"].ToString() == "2")
                    {
                        stgnature = Examination_P2.get_principal_signature(Session_id, Branch_id);
                    }
                    if (dr["Signature_type"].ToString() == "3")
                    {
                        stgnature = Examination_P2.get_examinee_or_parent_signature(Session_id, Branch_id, dr["Signature_type_e_p"].ToString());
                    }
                    Show_of_report_signature.Add(new Fetch_Details_of_signature
                    {
                        Signature_name = dr["Name"].ToString(),
                        Signature_image = stgnature,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_signature));
            }
        }


        /////////////////////////////////////////////////////////////
        //====================================CoScholastic Data
        /// <summary>
        /// /////////////////////////////////////////////////////////
        /// </summary>


        public class Fetch_Details_of_report_CoScholastic
        {
            public string Subject_Name { get; set; }
            public string Total_marks { get; set; }
        }

        List<Fetch_Details_of_report_CoScholastic> Show_of_report_CoScholastic = new List<Fetch_Details_of_report_CoScholastic>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_card_coscholastic(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            //string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Assessment,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Assessment_Name,t2.Sequence_No,(select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and  Subject_id=t1.Subject) as Subject_Name from Exam_marks t1 join Exam_Assessment_Details t2 on t1.Assessment=t2.Assessment_Id where t1.Session_id=" + Session_id + " and  Admission_no='" + Admission_no + "' and t1.Subject='" + Subject_id + "' and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Term=" + Term_id + " order by t2.Sequence_No asc";


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
                    string marks = Examination_P2.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Term"].ToString(), dr["Assessment"].ToString(), dr["Subject"].ToString(), dr["Branch_id"].ToString(), "Co-Scholastic");

                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                    string obt_marks = arrs[0];
                    string full_marks = arrs[1];

                    Show_of_report_CoScholastic.Add(new Fetch_Details_of_report_CoScholastic
                    {
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Total_marks = obt_marks,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_CoScholastic));
            }
        }




        /////////////////////////////////////////////////////////////
        //====================================CoScholastic Data
        /// <summary>
        /// /////////////////////////////////////////////////////////
        /// </summary>
        /// 
        public class Fetch_Details_of_report_discipline_activities
        {
            public string Activity_name { get; set; }
            public string Term_grade { get; set; }
        }

        List<Fetch_Details_of_report_discipline_activities> Show_of_report_discipline_activities = new List<Fetch_Details_of_report_discipline_activities>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_card_discipline_activity(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {

            string query = "select ptt.*,pt.Activity_name from Exam_Personality_Traits_Term_Wise ptt join Exam_Personality_Traits pt on ptt.Activity_Id=pt.Activity_Id where ptt.Session_id=" + Session_id + " and ptt.Branch_id='" + Branch_id + "' and ptt.Class_id=" + Class_id + " and ptt.Exam_Term_Id=" + Term_id + " and ptt.Admission_no='" + Admission_no + "'  order by pt.Position asc";

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
                    Show_of_report_discipline_activities.Add(new Fetch_Details_of_report_discipline_activities
                    {
                        Activity_name = dr["Activity_name"].ToString(),
                        Term_grade = grade,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_discipline_activities));
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

        //SIGNATURES
        public class Fetch_Details_of_rank
        {
            public string Rank { get; set; }
            public string Student_name { get; set; }
            public string Total_obtained_mark { get; set; }
            public string Mark_percentage { get; set; }
            public string Grade { get; set; }
        }

        List<Fetch_Details_of_rank> Show_of_rank = new List<Fetch_Details_of_rank>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_rank(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string section = Examination_P2.get_section(Session_id, Class_id, Admission_no, Branch_id);
            string query = "select  t2.class,t2.rollnumber,t2.session,t2.studentname,t1.*,(select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=t1.Term_id) as Term_name from Exam_rank_master t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Section='" + section + "' and t1.Term_id=" + Term_id + " and Rank<=3 order by Rank asc";
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
                string system_grade_id = Examination_P2.get_system_grade_id_final(Session_id, Branch_id, Class_id);
                foreach (DataRow dr in dt.Rows)
                {
                    string rank = "0";
                    if (dr["Rank"].ToString() == "1")
                    {
                        rank = dr["Rank"].ToString() + "st";
                    }
                    else if (dr["Rank"].ToString() == "2")
                    {
                        rank = dr["Rank"].ToString() + "nd";
                    }
                    else if (dr["Rank"].ToString() == "3")
                    {
                        rank = dr["Rank"].ToString() + "rd";
                    }
                    else
                    {
                        rank = dr["Rank"].ToString() + "th";
                    }

                    string grade = Examination_P2.get_grade_final(Session_id, Branch_id, system_grade_id, My.toDouble(dr["Mark_percentage"].ToString()));
                    Show_of_rank.Add(new Fetch_Details_of_rank
                    {
                        Rank = rank,
                        Student_name = dr["studentname"].ToString(),
                        Total_obtained_mark = dr["Total_obtained_mark"].ToString(),
                        Mark_percentage = dr["Mark_percentage"].ToString(),
                        Grade = grade,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_rank));
            }
        }


        //========================================================
        public class Fetch_Details_of_graph
        {
            public string Subject_name { get; set; }
            public string Total_obtained_mark { get; set; }
            public string Total_full_mark { get; set; }
            public string Grph_width { get; set; }
            public string BlankHeight { get; set; }
        }

        List<Fetch_Details_of_graph> Show_of_graph = new List<Fetch_Details_of_graph>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_graph(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string tblName = "Exam_temp_assesment_total_no_term_II";
            string first_term = bda_exam_setting.get_first_term(Session_id, Branch_id, Class_id);
            if (first_term == Term_id)
            {
                tblName = "Exam_temp_assesment_total_no";
            }
            string query = "select t2.Subject_name,t2.Subject_Short_Name,isnull(sum(convert(float, Marks)),0) as Total_obtained_mark,isnull(sum(convert(float, Full_marks)),0) as Total_full_mark from " + tblName + " t1 join Subject_Master t2 on t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id and t1.Branch_id=t2.Branch_id where t1.Session_id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Term_id=" + Term_id + " and t1.Admission_no='" + Admission_no + "' group by t2.Subject_name,t2.Subject_Short_Name,t2.Subject_position order by t2.Subject_position asc";
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
                double grphwidth = (100 / rowcount1);
                foreach (DataRow dr in dt.Rows)
                {
                    double blankHeight = 100 - My.toDouble(dr["Total_obtained_mark"].ToString());
                    Show_of_graph.Add(new Fetch_Details_of_graph
                    {
                        Subject_name = dr["Subject_Short_Name"].ToString(),
                        Total_obtained_mark = dr["Total_obtained_mark"].ToString(),
                        Total_full_mark = dr["Total_full_mark"].ToString(),
                        Grph_width = grphwidth.ToString("0.00"),
                        BlankHeight = blankHeight.ToString("0.00"),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_graph));
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
            string Total_marks = bda_examination_P2.get_overall_total_marks(Session_id, Class_id, Admission_no, Branch_id, Term_id);
            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);

            string overall_obt_marks = arrs[0];
            string overall_full_marks = arrs[1];
            string overall_percent = arrs[2];
            string MarkType = arrs[3];
            string grade = arrs[4];

            string percent_remark = bda_examination_P2.get_percent_remark(overall_percent);
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
    }
}
