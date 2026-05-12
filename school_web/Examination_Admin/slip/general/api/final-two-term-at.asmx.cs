using school_web.AppCode;
using school_web.AppCode.Exam;
using school_web.Examination_Admin.slip.final.api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Examination_Admin.slip.general.api
{
    /// <summary>
    /// Summary description for final_two_term_at
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class final_two_term_at : System.Web.Services.WebService
    {
        My mycode = new My();

        public class MyStudentsDetails
        {
            public string Admission_no { get; set; }
            public string Student_name { get; set; }
            public string Date_of_birth { get; set; }
            public string Mother_name { get; set; }
            public string Father_name { get; set; }
            public string Section { get; set; }
            public string Roll_no { get; set; }
            public string Term_name { get; set; }
            public string For_class { get; set; }
            public string Session { get; set; }
            public string Student_image { get; set; }
            public string Rank { get; set; }

            public List<Fetch_Details_of_Firm> MyFirmDetailData { get; set; }
            public List<Fetch_Details_of_report_head> MySubjectHeading { get; set; }
            public List<Fetch_Details_of_report_headII> MySubjectHeadingII { get; set; }
            public List<MySubjectMarkShow> MySubjectMarkShowItem { get; set; }
            public List<MyTotalsDetails> MyTotalsDetailsItem { get; set; }
            public List<MyCoScholesticDetails> MyCoScholesticDetailsItem { get; set; }

            public List<MyMarkRangeDetails> MyMarkRangeDetailsItem { get; set; }
            public List<MySignatureDetails> MySignatureDetailsItem { get; set; }
            public List<Fetch_Details_of_attendance> Fetch_Details_of_attendanceItem { get; set; }
            public List<Fetch_Details_of_Rank> MyRankDetailData { get; set; }
            //public List<Fetch_Details_of_attendance> Fetch_Details_of_attendance_Item { get; set; }

            public List<Fetch_Details_of_graph> MyGraphDetailData { get; set; }
        }

        public class Fetch_Details_of_graph
        {
            public string Subject_name { get; set; }
            public string Total_obtained_mark { get; set; }
            public string Total_full_mark { get; set; }
            public string Grph_width { get; set; }
            public string BlankHeight { get; set; }
            public string final_perc { get; set; }
            public string bg_color { get; set; }
        }

        public class Fetch_Details_of_Rank
        {
            public string Rank { get; set; }
            public string Student_name { get; set; }
            public string Total_obtained_mark { get; set; }
            public string Mark_percentage { get; set; }
            public string Grade { get; set; }
        }

        public class Fetch_Details_of_attendance
        {
            public string Exam_name { get; set; }
            public string Total_working_days { get; set; }
            public string Total_persent_days { get; set; }
            public string Attendance_percentage { get; set; }
            public string Attendance_class_style { get; set; }
        }

        public class Fetch_Details_of_Firm
        {
            public string Frim_logo { get; set; }
            public string Firm_address { get; set; }
            public string Firm_email { get; set; }
            public string Firm_name { get; set; }
            public string Firm_contact_no { get; set; }
            public string Frim_aff_no { get; set; }
            public string Affiliated_by_full_text { get; set; }
            public string Estd { get; set; }
            public string Watermar_image { get; set; }
            public string Website { get; set; }
            public string Extra_logo { get; set; }
            public string School_no { get; set; }
            public string Aff_text1 { get; set; }
            public string SchoolCode { get; set; }
            public string ExtraLogo { get; set; }
            public string AffYear { get; set; }
            public string Header_templete { get; set; }
            public string Content_header { get; set; }
            public string Issue_dateDIv { get; set; }
            public string Issue_date { get; set; }
            public string PromttoDV { get; set; }
            public string Teachrrmrk { get; set; } 
        }

        public class Fetch_Details_of_report_head
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
            public string TermColSpan { get; set; }
        }
        public class Fetch_Details_of_report_headII
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
            public string TermColSpan { get; set; }
        }


        public class MySubjectMarkShow
        {
            public string TermSubj_hlf_mark { get; set; }
            public string SubjFulmarks { get; set; }
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
            public string Total_Precentage_class_of_term_I { get; set; }

            public string Total_Present_class_of_term_II { get; set; }
            public string Total_Precentage_class_of_term_II { get; set; }
            public string Overall_final_percent { get; set; }

            public string Final_working_days { get; set; }
            public string Final_persent_days { get; set; }
            public string Final_percent_days { get; set; }

            public string Overall_final_grade { get; set; }
            public string Next_session_year { get; set; }


            public string Is_attandance_show { get; set; }
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
            public string Is_text_center { get; set; }
            public string Is_website_show { get; set; }

            public string Hdr_frst { get; set; }
            public string Hdr_scnd { get; set; }
            public string Subj_highest_mark { get; set; }
            public string ThemeColor { get; set; }
            public List<MySubjectMarkDetails> MySubjectMarkItem { get; set; }
            public List<MySubjectMarkDetailsII> MySubjectMarkItemII { get; set; }
        }
        public class MySubjectMarkDetails
        {
            public string Marks_term_I { get; set; }
            public string Marks_term_II { get; set; }
            public string Subject_Name { get; set; }
            public string Term_i_full_mark { get; set; }
            public string Term_ii_full_mark { get; set; }
        }
        public class MySubjectMarkDetailsII
        {
            public string Marks_term_I { get; set; }
            public string Marks_term_II { get; set; }
            public string Subject_Name { get; set; }
            public string Term_i_full_mark { get; set; }
            public string Term_ii_full_mark { get; set; }
        }
        public class MyTotalsDetails
        {
            public string Overall_obt_marks { get; set; }
            public string Overall_full_marks { get; set; }
            public string Overall_percents { get; set; }
            public string Mark_type { get; set; }
            public string P_remark { get; set; }
            public string Grade { get; set; }
            public string Promot_to_class { get; set; }
            public string School_reopen_on { get; set; }
        }

        public class MyCoScholesticDetails
        {
            public string Subject_Name { get; set; }
            public string Total_marks_t1 { get; set; }
            public string Total_marks_t2 { get; set; }
            public string Total_marks_t3 { get; set; }
            public string RowCount { get; set; }
        }
        public class MySignatureDetails
        {
            public string Signature_name { get; set; }
            public string Signature_image { get; set; }
        }
        public class MyMarkRangeDetails
        {
            public string Lower_Range { get; set; }
            public string Upper_Range { get; set; }
            public string Grade { get; set; }
            public string RowCount { get; set; }
        }


        List<MyStudentsDetails> ShowMyStudents = new List<MyStudentsDetails>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_bulks(string Session_id, string Class_id, string Section, string Branch_id, string Term_idI, string Term_idII, string Adm_no)
        {
            string query = "";
            if (Adm_no == "0")
            {
                query = "select DISTINCT  em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname,ar.dob,ar.mothername,ar.fathername,ar.studentimagepath,CASE WHEN ar.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN ar.studentimagepath is not null THEN ar.studentimagepath END AS Student_img,isnull((select top 1 Rank from Exam_rank_master_final_year where Session_id=ar.Session_id and Class_id=ar.Class_id and Branch_id=ar.Branch_id and Admission_no=ar.admissionserialnumber),'0') as Rank from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + Session_id + " and em.Class_id=" + Class_id + " and ar.Section='" + Section + "' and em.Branch_id=" + Branch_id + "  and  em.Term=" + Term_idII + " and ar.Status='1' order by ar.rollnumber asc";
            }
            else
            {
                query = "select DISTINCT  em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname,ar.dob,ar.mothername,ar.fathername,ar.studentimagepath,CASE WHEN ar.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN ar.studentimagepath is not null THEN ar.studentimagepath END AS Student_img,isnull((select top 1 Rank from Exam_rank_master_final_year where Session_id=ar.Session_id and Class_id=ar.Class_id and Branch_id=ar.Branch_id and Admission_no=ar.admissionserialnumber),'0') as Rank from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + Session_id + " and em.Class_id=" + Class_id + " and em.Branch_id=" + Branch_id + "  and  em.Term=" + Term_idII + " and ar.admissionserialnumber='" + Adm_no + "' and ar.Status='1' order by ar.rollnumber asc";
            }
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
                string settings_for_final_year = finalTwoTermN.get_final_year_setting(Session_id, Class_id, Branch_id);
                foreach (DataRow dr in dt.Rows)
                {
                    My.exeSql("delete from Exam_temp_assesment_total_no  where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and  Admission_no='" + dr["Admission_no"].ToString() + "';  delete from Exam_temp_assesment_total_no_term_II where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and  Admission_no='" + dr["Admission_no"].ToString() + "';INSERT INTO Exam_rp_bulk_print_adm (Session_id,Admission_no) values ('" + Session_id + "','" + dr["Admission_no"].ToString() + "')");
                    prep_final_term(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, Term_idI);
                    prep_final_term(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, Term_idII);
                    prep_final_term_coscholestic(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, Term_idI);
                    prep_final_term_coscholestic(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, Term_idII);



                    List<Fetch_Details_of_Firm> MBFirmDetails = findFirmDetails();
                    List<Fetch_Details_of_report_head> MBdetailsHeading = findSubjectHeading(Session_id, Class_id, Branch_id, Term_idI);
                    List<Fetch_Details_of_report_headII> MBdetailsHeadingII = findSubjectHeadingII(Session_id, Class_id, Branch_id, Term_idII);
                    List<MySubjectMarkShow> MBdetails = findSubjectMarkShow(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, dr["Section"].ToString());
                    List<MyTotalsDetails> MBtotalsdetails = findTotalsDT(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, dr["Section"].ToString());
                    List<MyCoScholesticDetails> MBCoScholesticdetails = findCoscholesticDT(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII);
                    List<MySignatureDetails> MBSigndetails = findSignatureDT(Session_id, Branch_id, dr["Section"].ToString(), Class_id);
                    List<MyMarkRangeDetails> MBMarkRangedetails = findMarkRangeDT(Session_id, Branch_id, Term_idI);
                    List<Fetch_Details_of_attendance> MBAttendanceDt = findAttendanceDT(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id);
                    List<Fetch_Details_of_Rank> MBRankdetails = findrankDT(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, dr["Section"].ToString());
                    List<Fetch_Details_of_graph> MBGraph = findGraphDetails(Session_id, dr["Admission_no"].ToString(), Class_id, Branch_id, dr["Section"].ToString(), Term_idI, Term_idII);


                    string sdtimgs = dr["studentimagepath"].ToString();
                    if (sdtimgs == "")
                    {
                        sdtimgs = "hidden";
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

                    ShowMyStudents.Add(new MyStudentsDetails
                    {
                        Admission_no = dr["Admission_no"].ToString(),
                        Student_name = dr["studentname"].ToString(),
                        Date_of_birth = dr["dob"].ToString(),
                        Mother_name = dr["mothername"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Section = dr["Section"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),
                        For_class = dr["class"].ToString(),
                        Session = dr["session"].ToString(),
                        Student_image = sdtimgs,
                        Rank = rank,

                        MyFirmDetailData = MBFirmDetails,
                        MySubjectHeading = MBdetailsHeading,
                        MySubjectHeadingII = MBdetailsHeadingII,
                        MySubjectMarkShowItem = MBdetails,
                        MyTotalsDetailsItem = MBtotalsdetails,
                        MyCoScholesticDetailsItem = MBCoScholesticdetails,
                        MySignatureDetailsItem = MBSigndetails,
                        MyMarkRangeDetailsItem = MBMarkRangedetails,
                        Fetch_Details_of_attendanceItem = MBAttendanceDt,
                        MyRankDetailData = MBRankdetails,
                        MyGraphDetailData = MBGraph,
                        //Fetch_Details_of_attendance_Item = MBAttenddetails,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(ShowMyStudents));
            }
        }

        private List<Fetch_Details_of_graph> findGraphDetails(string Session_id, string admission_no, string Class_id, string Branch_id, string Section, string Term_idI, string Term_idII)
        {
            List<Fetch_Details_of_graph> MyGraphDetailData = new List<Fetch_Details_of_graph>();
            string query = "SELECT Subject_Short_Name, Subject_position, (SUM(TRY_CONVERT(float, Obtt_mark)) / SUM(TRY_CONVERT(float, Full_marks)) * 100) AS total_persent, SUM(TRY_CONVERT(float, Full_marks)) AS Full_marks FROM (SELECT t2. Subject_Short_Name, t2.Subject_position, SUM(TRY_CONVERT(float, Marks)) AS Obtt_mark, isnull(SUM(TRY_CONVERT(float, Full_marks)),0) AS Full_marks FROM Exam_temp_assesment_total_no t1 JOIN Subject_Master t2 ON t1.Class_id = t2. course_id AND t1.Subject_id = t2.Subject_id WHERE t1.Session_id='" + Session_id + "' AND t1.Class_id='" + Class_id + "' AND t1.Term_id='" + Term_idI + "' AND t1.Admission_no='" + admission_no + "' GROUP BY t2.Subject_Short_Name, t2.Subject_id, t2.Subject_position UNION ALL SELECT t2.Subject_Short_Name, t2.Subject_position, SUM(TRY_CONVERT(float, Marks)) AS Obtt_mark, isnull(SUM(TRY_CONVERT(float, Full_marks)),0) AS Full_marks FROM  Exam_temp_assesment_total_no_term_II t1 JOIN Subject_Master t2 ON t1.Class_id = t2.course_id AND t1.Subject_id = t2.Subject_id WHERE t1.Session_id='" + Session_id + "' AND t1.Class_id='" + Class_id + "' AND t1.Term_id='" + Term_idII + "' AND t1.Admission_no='" + admission_no + "' GROUP BY t2. Subject_Short_Name, t2.Subject_id, t2.Subject_position) t GROUP BY Subject_Short_Name, Subject_position ORDER BY Subject_position ASC";
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
                double grphwidth = (100 / rowcount1);
                int c = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    c++;
                    double final_perc = My.toDouble(dr["total_persent"].ToString());
                    double blankHeight = 100 - final_perc;
                    string bg_color = "0075c2";
                    if (c == 1)
                    {
                        bg_color = "#0079c8";
                    }
                    if (c == 2)
                    {
                        bg_color = "#00ce5c";
                    }
                    if (c == 3)
                    {
                        bg_color = "#ee3cb5";
                    }
                    if (c == 4)
                    {
                        bg_color = "#75d3d7";
                    }
                    if (c == 5)
                    {
                        bg_color = "#51cda0";
                    }
                    if (c == 6)
                    {
                        bg_color = "#df7970";
                    }
                    if (c == 7)
                    {
                        bg_color = "#4c9ca0";
                    }
                    if (c == 8)
                    {
                        bg_color = "#ae7d99";
                    }
                    if (c == 9)
                    {
                        bg_color = "#c9d45c";
                    }
                    if (c == 10)
                    {
                        bg_color = "#51cda0";
                    }
                    if (c == 11)
                    {
                        bg_color = "#00ce5c";
                    }
                    if (c == 12)
                    {
                        bg_color = "#ee3cb5";
                    }
                    if (c == 13)
                    {
                        bg_color = "#df7970";
                    }
                    if (c == 14)
                    {
                        bg_color = "#c9d45c";
                    }
                    if (c == 15)
                    {
                        bg_color = "#0079c8";
                    }
                    if (c == 16)
                    {
                        bg_color = "#51cda0";
                    }
                    if (c == 17)
                    {
                        bg_color = "#c9d45c";
                    }
                    if (c == 18)
                    {
                        bg_color = "#4c9ca0";
                    }
                    if (c == 19)
                    {
                        bg_color = "#51cda0";
                    }
                    if (c == 20)
                    {
                        bg_color = "#c9d45c";
                    }
                    MyGraphDetailData.Add(new Fetch_Details_of_graph
                    {
                        Subject_name = dr["Subject_Short_Name"].ToString(),
                        Total_obtained_mark = dr["total_persent"].ToString(),
                        Total_full_mark = dr["Full_marks"].ToString(),
                        Grph_width = grphwidth.ToString("0.00"),
                        BlankHeight = blankHeight.ToString("0.00"),
                        final_perc = final_perc.ToString(),
                        bg_color = bg_color,
                    });
                }
            }
            return MyGraphDetailData;
        }
        private List<Fetch_Details_of_Rank> findrankDT(string Session_id, string Class_id, string Admission_no, string Branch_id, string section)
        {
            List<Fetch_Details_of_Rank> MyRankDetailData = new List<Fetch_Details_of_Rank>();
            string query = "select top 3 t2.class,t2.rollnumber,t2.session,t2.studentname,t1.*  from Exam_rank_master_final_year t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Section='" + section + "' order by Rank asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_Grade_System_Range_Grade");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                string system_grade_id = Examination.get_system_grade_id_final(Session_id, Branch_id, Class_id);
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

                    string grade = Examination.get_gp_final(Session_id, Branch_id, system_grade_id, My.toDouble(dr["Percentage"].ToString()));
                    MyRankDetailData.Add(new Fetch_Details_of_Rank
                    {
                        Rank = rank,
                        Student_name = dr["studentname"].ToString(),
                        Total_obtained_mark = dr["Total_obtained_mark_both_trm"].ToString(),
                        Mark_percentage = dr["Percentage"].ToString(),
                        Grade = grade,
                    });
                }
            }
            return MyRankDetailData;
        }



        private List<Fetch_Details_of_attendance> findAttendanceDT(string Session_id, string Class_id, string Admission_no, string Branch_id)
        {
            List<Fetch_Details_of_attendance> Fetch_Details_of_attendanceItem = new List<Fetch_Details_of_attendance>();
            string query = "select * from Exam_Exam_Wise_Attendance where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "'";
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
                double ttl_class = 0; double ttl_attendance_class = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ttl_class = ttl_class + My.toDouble(dr["Total_no_of_class"].ToString());
                    ttl_attendance_class = ttl_attendance_class + My.toDouble(dr["No_of_class_Attendance"].ToString());

                    double att_persent = (My.toDouble(dr["No_of_class_Attendance"].ToString()) / My.toDouble(dr["Total_no_of_class"].ToString())) * 100;
                    Fetch_Details_of_attendanceItem.Add(new Fetch_Details_of_attendance
                    {
                        Exam_name = dr["Exam_type"].ToString(),
                        Total_working_days = dr["Total_no_of_class"].ToString(),
                        Total_persent_days = dr["No_of_class_Attendance"].ToString(),
                        Attendance_percentage = att_persent.ToString("0.00"),
                        Attendance_class_style = "",
                    });
                }

                double att_persent_ttl = (My.toDouble(ttl_attendance_class) / My.toDouble(ttl_class)) * 100;
                Fetch_Details_of_attendanceItem.Add(new Fetch_Details_of_attendance
                {
                    Exam_name = "TOTAL",
                    Total_working_days = ttl_class.ToString(),
                    Total_persent_days = ttl_attendance_class.ToString(),
                    Attendance_percentage = att_persent_ttl.ToString("0.00"),
                    Attendance_class_style = "trdefferents",
                });
            }

            return Fetch_Details_of_attendanceItem;
        }

        private List<MyMarkRangeDetails> findMarkRangeDT(string Session_id, string Branch_id, string Term_idI)
        {
            List<MyMarkRangeDetails> MyMarkRangeDetailsItem = new List<MyMarkRangeDetails>();
            string query = "select gsrg.* from Exam_Grade_System_Range_Grade gsrg join Exam_Term_Details td on gsrg.Session_Id=td.Session_Id and gsrg.Branch_Id=td.Branch_Id and gsrg.Grade_System_Id=td.Grade_System_Id where td.Exam_Term_Id=" + Term_idI + " and gsrg.Session_Id=" + Session_id + " and gsrg.Branch_Id='" + Branch_id + "' order by Lower_Range desc";
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
                    MyMarkRangeDetailsItem.Add(new MyMarkRangeDetails
                    {
                        Lower_Range = dr["Lower_Range"].ToString(),
                        Upper_Range = dr["Upper_Range"].ToString(),
                        Grade = dr["Grade"].ToString(),
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
            }

            return MyMarkRangeDetailsItem;
        }


        private List<MySignatureDetails> findSignatureDT(string Session_id, string Branch_id, string Section, string Class_id)
        {
            List<MySignatureDetails> MySignatureDetailsItem = new List<MySignatureDetails>();
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
                foreach (DataRow dr in dt.Rows)
                {
                    string stgnature = "";
                    if (dr["Signature_type"].ToString() == "1")
                    {
                        stgnature = Examination.get_class_teacher_signature(Session_id, Class_id, Section, Branch_id);
                    }
                    if (dr["Signature_type"].ToString() == "2")
                    {
                        stgnature = Examination.get_principal_signature(Session_id, Branch_id);
                    }
                    if (dr["Signature_type"].ToString() == "3")
                    {
                        stgnature = Examination.get_examinee_or_parent_signature(Session_id, Branch_id, dr["Signature_type_e_p"].ToString());
                    }
                    MySignatureDetailsItem.Add(new MySignatureDetails
                    {
                        Signature_name = dr["Name"].ToString(),
                        Signature_image = stgnature,
                    });
                }
            }

            return MySignatureDetailsItem;
        }


        //=================================MyCoScholesticDetails
        private List<MyCoScholesticDetails> findCoscholesticDT(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII)
        {
            List<MyCoScholesticDetails> MyCoScholesticDetailsItem = new List<MyCoScholesticDetails>();

            string query = "select DISTINCT t1.Subject_id,t2.Subject_name from Exam_coscholastic_and_activities_assesment_total_no t1 join Subject_Master t2 on t1.Class_id=t2.course_id and  t1.Subject_id=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Admission_no='" + Admission_no + "' and t1.Entry_type='Co-Scholastic' and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic'  and t1.Term_id='" + Term_idI + "'";

            // string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_idI + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic' order by t2.Subject_position asc";
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

                    MyCoScholesticDetailsItem.Add(new MyCoScholesticDetails
                    {
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Total_marks_t1 = termI_grade,
                        Total_marks_t2 = termII_grade,
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
            }
            return MyCoScholesticDetailsItem;
        }


        private List<MyTotalsDetails> findTotalsDT(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Section)
        {
            List<MyTotalsDetails> MyTotalsDetailsItem = new List<MyTotalsDetails>();
            string Total_marks = finalTwoTermN.get_overall_total_marks_final(Session_id, Class_id, Admission_no, Branch_id, Term_idI);
            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);

            string overall_obt_marks = arrs[0];
            string overall_full_marks = arrs[1];
            string overall_percent = arrs[2];
            string overall_obt_marks_not_averge = arrs[3];
            string overall_full_marks_not_averge = arrs[4];

            string MarkType = arrs[5];
            string grade = arrs[6];


            string promot_to_class = get_promot_to_class(Session_id, Branch_id, Class_id, Admission_no, Section);
            string[] stringSeparator = new string[] { "月" };
            string[] arr = promot_to_class.Split(stringSeparator, StringSplitOptions.None);
            string promot_to_classss = arr[0];
            string school_reopen_on = arr[1];

            string percent_remark = get_term_two_remark(Admission_no, Session_id, Class_id, Branch_id, Term_idII);// finalTwoTermN.get_percent_remark(overall_percent);
            MyTotalsDetailsItem.Add(new MyTotalsDetails
            {
                Overall_obt_marks = overall_obt_marks_not_averge,
                Overall_full_marks = overall_full_marks_not_averge,
                Overall_percents = overall_percent,
                Mark_type = MarkType,
                P_remark = percent_remark,
                Grade = grade,
                Promot_to_class = promot_to_classss,
                School_reopen_on = school_reopen_on,
            });

            return MyTotalsDetailsItem;
        }

        private string get_promot_to_class(string session_id, string branch_id, string class_id, string admission_no, string section)
        {
            string rtrN = " 月 ";
            string query = "select Promoted_to_class,School_reopen_on from Exam_promoted_to_class where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Section='" + section + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                rtrN = dt.Rows[0]["Promoted_to_class"].ToString() + "月" + dt.Rows[0]["School_reopen_on"].ToString();
            }
            return rtrN;
        }

        private string get_term_two_remark(string admission_no, string session_id, string class_id, string branch_id, string term_idII)
        {
            string remarks = "hidden";
            DataTable dt = My.dataTable("select * from Exam_Commentary_Remark_Term_Wise_Entry where Session_id='" + session_id + "' and Branch_id='" + branch_id + "'  and Class_id='" + class_id + "' and Exam_Term_Id='" + term_idII + "' and Admission_no='" + admission_no + "'");
            if (dt.Rows.Count > 0)
            {
                remarks = dt.Rows[0]["Remarks"].ToString();
            }
            return remarks;
        }

        private List<MySubjectMarkShow> findSubjectMarkShow(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Section)
        {
            List<MySubjectMarkShow> MySubjectMarkShowItem = new List<MySubjectMarkShow>();
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term_id,t1.Subject_id,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section from Exam_temp_assesment_total_no t1 join Subject_Master t2 on t1.Subject_id=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term_id=" + Term_idI + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' and t2.Is_mandatory=1  and t1.Subject_id in (select Sub_id from Subject_Mapping_New where Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Session_id='" + Session_id + "') order by t2.Subject_position asc";
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
                string settings_for_final_year = finalTwoTermN.get_final_year_setting(Session_id, Class_id, Branch_id);
                foreach (DataRow dr in dt.Rows)
                {
                    List<MySubjectMarkDetails> MBdetails = findmyBookingProduct(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idI);
                    List<MySubjectMarkDetailsII> MBdetailsII = findmyBookingProductII(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idII);
                    string ttl_mark_of_a_subject = finalTwoTermN.get_ttl_mark_of_a_subject_final_year(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idI, Term_idII);

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
                    string Total_Precentage_class_of_term_I = arrs[8];


                    string Total_class_of_term_II = arrs[9];
                    string Total_Present_class_of_term_II = arrs[10];
                    string Total_Precentage_class_of_term_II = arrs[11];


                    string Overall_final_mark = arrs[12];
                    string Overall_final_percent = arrs[13];

                    //=====================================
                    double final_working_days = My.toDouble(Total_class_of_term_I) + My.toDouble(Total_class_of_term_II);
                    double final_persent_days = My.toDouble(Total_Present_class_of_term_I) + My.toDouble(Total_Present_class_of_term_II);
                    double final_percent_days = (final_persent_days / final_working_days) * 100;

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
                        if_is_grade_ttl_mark_hide = "showed";
                        colspanfiveandsix = "4";
                        overall_ab_grade = "GRADE";
                        overall_av_marks = "showed";
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
                        if_is_mrk_hide = "showed";
                        if_is_grade_ttl_mark_hide = "hidden";
                        colspanfiveandsix = "5";
                        overall_ab_grade = "GRADE";
                        overall_av_marks = "hidden";
                        colspanOneTwo = "1";
                    }

                    //SettingS
                    string[] stringSeparatorsss = new string[] { "/" };
                    string[] arr = settings_for_final_year.Split(stringSeparatorsss, StringSplitOptions.None);
                    string is_attandance_show = arr[0];
                    string specialNote = arr[1];   // SpecialNote
                    string qrShow = arr[2];   // QRS

                    string instruction2 = arr[3];   // Instruction2
                    string ranks = arr[4];   // Ranks
                    string graph = arr[5];   // Graph
                    string graphHeight = arr[6];   // GraphHeight
                    string prcnt_remark = arr[7];   // PercentRemark

                    string sign_top = arr[8];   // SignTop
                    string sign_bottom = arr[9];   // SignBottom
                    string max_mark_show = arr[10];   // IsMaxMarkShow

                    string is_estd_show = arr[11];   // IsESTDShow
                    string is_contact_no_show = arr[12];   // IsContactNoShow
                    string is_email_show = arr[13];   // IsEmailId
                    string is_class_text_show = arr[14];   // IsClassTextShow
                    string height_dv = arr[15];   // ReHeightDV
                    string class_in_new_line = arr[16];   // ReHeightDV

                    //MARGIN
                    string co_sch_area_margn = arr[17];   // co_sch_area_margn
                    string overall_area_margn = arr[18];   // overall_area_margn
                    string percent_remark_area_margn = arr[19];   // percent_remark_area_margn
                    string graph_area_margn = arr[20];   // graph_area_margn
                    string ins1_area_margn = arr[21];   // instruction1_area_margn
                    string ins2_area_margn = arr[22];   // instruction2_area_margn
                    string toppers_area_margn = arr[23];   // toppers_area_margn
                    string is_watermark_show = arr[24];   // Watermark Show 

                    string is_std_img_hide = arr[25];   // StudentImgHide
                    string is_std_section_hide = arr[26];   // SectionHide
                    string aff_text = arr[27];   // AffText
                    string father_name1 = arr[28];   // FatherName1
                    string father_name2 = arr[29];   // FatherName2
                    string is_subj_code_hide = arr[30];   // SubjectCode
                    string is_text_center = arr[31];   // IsTExtCenter
                    string Is_website_show = arr[32];   // IsWebsiteShow
                    string WhatHeadShow = arr[33];   // WhatHeadShow

                    string themeColor = arr[39];   // ThemeColor
                    string rank = "0";


                    string hdr_frst = "hidden"; string hdr_scnd = "hidden";
                    if (WhatHeadShow == "1")
                    {
                        hdr_frst = "show";
                        hdr_scnd = "hidden";
                    }
                    else
                    {
                        hdr_frst = "hidden";
                        hdr_scnd = "show";
                    }

                    //QQQQQRRRR 
                    string url = "";
                    string remarkss = "hidden";
                    string Total_marks = finalTwoTermN.get_overall_total_marks_final(Session_id, Class_id, Admission_no, Branch_id, Term_idI);
                    string[] stringSeparatorssxx = new string[] { "/" };
                    string[] arrsxx = Total_marks.Split(stringSeparatorssxx, StringSplitOptions.None);

                    string overall_obt_marks = arrsxx[0];
                    string overall_full_marks = arrsxx[1];
                    string overall_percent = arrsxx[2];
                    string overall_obt_marks_not_averge = arrsxx[3];
                    string overall_full_marks_not_averge = arrsxx[4];
                    string MarkType = arrsxx[5];
                    string grade = arrsxx[6];


                    if (MarkType == "GRADE")
                    {
                        string qr_txt = "Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Overall Grade: " + grade;
                        url = "https://quickchart.io/qr?text=" + qr_txt + "&dark=000&light=ffffff&ecLevel=Q&format=svg";
                    }
                    else
                    {
                        string qr_txt = "Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Total Obtained Mark: " + overall_obt_marks_not_averge + "/" + overall_full_marks_not_averge + ", Overall Percentage: " + overall_percent;
                        url = "https://quickchart.io/qr?text=" + qr_txt + "&dark=000&light=ffffff&ecLevel=Q&format=svg";
                    }

                    string TermSubj_hlf_mark = "50";
                    string SubjFulmarks = "100";
                    MySubjectMarkShowItem.Add(new MySubjectMarkShow
                    {
                        TermSubj_hlf_mark = TermSubj_hlf_mark,
                        SubjFulmarks = SubjFulmarks,
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
                        Total_Precentage_class_of_term_I = Total_Precentage_class_of_term_I,

                        Total_class_of_term_II = Total_class_of_term_II,
                        Total_Present_class_of_term_II = Total_Present_class_of_term_II,
                        Total_Precentage_class_of_term_II = Total_Precentage_class_of_term_II,

                        Final_working_days = final_working_days.ToString(),
                        Final_persent_days = final_persent_days.ToString(),
                        Final_percent_days = final_percent_days.ToString("0.00"),


                        Overall_final_percent = Overall_final_percent + " %",
                        Overall_final_grade = overall_final_grade,
                        Next_session_year = next_session_year,


                        Is_attandance_show = is_attandance_show,
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
                        Is_text_center = is_text_center,
                        Is_website_show = Is_website_show,
                        Hdr_frst = hdr_frst,
                        Hdr_scnd = hdr_scnd,
                        ThemeColor = themeColor,
                        MySubjectMarkItem = MBdetails,
                        MySubjectMarkItemII = MBdetailsII
                    });
                }
            }
            return MySubjectMarkShowItem;
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
                string system_grade_id = Examination.get_system_grade_id_final(Session_id, Branch_id, Class_id);
                string marks_output = Examination.get_marks_output(Session_id, Branch_id, system_grade_id);
                foreach (DataRow dr in dt.Rows)
                {
                    string Term_i_mark = "";
                    string Term_ii_mark = "";
                    if (marks_output == "Marks")
                    {
                        Term_i_mark = dr["Term_i_mark"].ToString();
                        Term_ii_mark = dr["Term_ii_mark"].ToString();


                        decimal mark;
                        if (decimal.TryParse(Term_i_mark, out mark))
                        {
                            // Check if the mark is a whole number
                            if (mark % 1 == 0)
                            {
                                // If it's a whole number, convert to an integer string
                                Term_i_mark = ((int)mark).ToString();
                            }
                            else
                            {
                                // If it's not a whole number, keep it as is
                                Term_i_mark = mark.ToString();
                            }
                        }
                    }
                    else
                    {
                        double term_i_ttl_mark = (My.toDouble(dr["Term_i_mark"].ToString()) / My.toDouble(dr["Term_i_full_mark"].ToString())) * 100;
                        double term_ii_ttl_mark = (My.toDouble(dr["Term_ii_mark"].ToString()) / My.toDouble(dr["Term_ii_full_mark"].ToString())) * 100;

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

        private List<MySubjectMarkDetailsII> findmyBookingProductII(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            List<MySubjectMarkDetailsII> MySubjectMarkItemII = new List<MySubjectMarkDetailsII>();
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
                string system_grade_id = Examination.get_system_grade_id_final(Session_id, Branch_id, Class_id);
                string marks_output = Examination.get_marks_output(Session_id, Branch_id, system_grade_id);
                foreach (DataRow dr in dt.Rows)
                {
                    string Term_i_mark = "";
                    string Term_ii_mark = "";
                    if (marks_output == "Marks")
                    {
                        Term_i_mark = dr["Term_i_mark"].ToString();
                        Term_ii_mark = dr["Term_ii_mark"].ToString();



                        decimal mark; 
                        if (decimal.TryParse(Term_ii_mark, out mark))
                        {
                            // Check if the mark is a whole number
                            if (mark % 1 == 0)
                            {
                                // If it's a whole number, convert to an integer string
                                Term_ii_mark = ((int)mark).ToString();
                            }
                            else
                            {
                                // If it's not a whole number, keep it as is
                                Term_ii_mark = mark.ToString();
                            }
                        }
                    }
                    else
                    {
                        double term_i_ttl_mark = (My.toDouble(dr["Term_i_mark"].ToString()) / My.toDouble(dr["Term_i_full_mark"].ToString())) * 100;
                        double term_ii_ttl_mark = (My.toDouble(dr["Term_ii_mark"].ToString()) / My.toDouble(dr["Term_ii_full_mark"].ToString())) * 100;

                        string term_I_grade = Examination.get_grade_final(Session_id, Branch_id, system_grade_id, term_i_ttl_mark);
                        string term_II_grade = Examination.get_grade_final(Session_id, Branch_id, system_grade_id, term_ii_ttl_mark);
                        Term_i_mark = term_I_grade;
                        Term_ii_mark = term_II_grade;
                    }


                    MySubjectMarkItemII.Add(new MySubjectMarkDetailsII
                    {
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Marks_term_I = Term_i_mark,
                        Marks_term_II = Term_ii_mark,

                        Term_i_full_mark = dr["Term_i_full_mark"].ToString(),
                        Term_ii_full_mark = dr["Term_ii_full_mark"].ToString(),

                    });
                }
            }
            return MySubjectMarkItemII;
        }


        private List<Fetch_Details_of_report_head> findSubjectHeading(string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            List<Fetch_Details_of_report_head> MySubjectHeading = new List<Fetch_Details_of_report_head>();
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
                int colspanS = 0;
                string system_grade_id = Examination.get_system_grade_id_final(Session_id, Branch_id, Class_id);
                string marks_output = Examination.get_marks_output(Session_id, Branch_id, system_grade_id);
                if (marks_output == "Marks")
                {
                    colspanS = rowcount1 + 2;
                }
                else
                {
                    colspanS = rowcount1 + 1;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string maxmarkS = dt.Rows[i]["Maximum_Marks"].ToString();
                    string extra_marks = "0";
                    if (i == 1)
                    {
                        extra_marks = dps_termI.get_extra_marks(Class_id, Branch_id);
                        if (extra_marks != "0")
                        {
                            maxmarkS = maxmarkS + "/" + extra_marks;
                        }
                    }

                    MySubjectHeading.Add(new Fetch_Details_of_report_head
                    {
                        Assessment_Name = dt.Rows[i]["Assessment_Name"].ToString(),
                        Short_Name = dt.Rows[i]["Short_Name"].ToString(),
                        Maximum_Marks = maxmarkS, //dt.Rows[i]["Maximum_Marks"].ToString(),
                        Term_Maximum_Marks = dt.Rows[i]["Term_Maximum_Marks"].ToString(),
                        TermColSpan = colspanS.ToString(),
                    });
                }
            }
            return MySubjectHeading;
        }
        private List<Fetch_Details_of_report_headII> findSubjectHeadingII(string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            List<Fetch_Details_of_report_headII> MySubjectHeadingII = new List<Fetch_Details_of_report_headII>();
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
                int colspanS = 0;
                string system_grade_id = Examination.get_system_grade_id_final(Session_id, Branch_id, Class_id);
                string marks_output = Examination.get_marks_output(Session_id, Branch_id, system_grade_id);
                if (marks_output == "Marks")
                {
                    colspanS = rowcount1 + 2;
                }
                else
                {
                    colspanS = rowcount1 + 1;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string maxmarkS = dt.Rows[i]["Maximum_Marks"].ToString();
                    string extra_marks = "0";
                    if (i == 1)
                    {
                        extra_marks = dps_termI.get_extra_marks(Class_id, Branch_id);
                        if (extra_marks != "0")
                        {
                            maxmarkS = maxmarkS + "/" + extra_marks;
                        }
                    }

                    MySubjectHeadingII.Add(new Fetch_Details_of_report_headII
                    {
                        Assessment_Name = dt.Rows[i]["Assessment_Name"].ToString(),
                        Short_Name = dt.Rows[i]["Short_Name"].ToString(),
                        Maximum_Marks = maxmarkS, //dt.Rows[i]["Maximum_Marks"].ToString(),
                        Term_Maximum_Marks = dt.Rows[i]["Term_Maximum_Marks"].ToString(),
                        TermColSpan = colspanS.ToString(),
                    });
                }
            }

            return MySubjectHeadingII;
        }

        private List<Fetch_Details_of_Firm> findFirmDetails()
        {
            List<Fetch_Details_of_Firm> MyFirmDetailData = new List<Fetch_Details_of_Firm>();
            string query = "select *,isnull((select top 1 Path from Header_templete where Status='1' and Type='Report_card'),'0') as header_templete from Firm_Details";
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
                    string promttoDV = "hidden"; string teachrrmrk = "showd";
                    string issue_datedv = "hidden";
                    string headertemplete = dr["header_templete"].ToString();
                    string content_header = "hidden";
                    if (headertemplete == "0")
                    {
                        headertemplete = "hidden";
                        content_header = "showd";
                    }
                    if (dr["firm_id"].ToString() == "MSAE-001")
                    {
                        issue_datedv = "showd";
                        promttoDV = "showd"; 
                        teachrrmrk = "hidden";
                    }
                    MyFirmDetailData.Add(new Fetch_Details_of_Firm
                    {
                        Frim_logo = dr["logo"].ToString(),
                        Firm_address = dr["address1"].ToString(),
                        Firm_email = dr["email"].ToString(),
                        Firm_name = dr["firm_name"].ToString(),
                        Firm_contact_no = "Contact Number : " + dr["contact_no"].ToString(),
                        Frim_aff_no = dr["Affiliation"].ToString(),
                        Affiliated_by_full_text = dr["Affiliated_by_full_text"].ToString(),
                        Estd = "ESTD : " + dr["estd"].ToString(),
                        Watermar_image = dr["Watermark_image"].ToString(),
                        Website = dr["Website_for_report_card"].ToString(),
                        Aff_text1 = "AFFILIATION NO. :  " + dt.Rows[0]["Affiliation"].ToString(),
                        SchoolCode = "School code  : " + dt.Rows[0]["school_no"].ToString(),
                        ExtraLogo = dt.Rows[0]["Extra_logo"].ToString(),
                        AffYear = "Aff. : " + dt.Rows[0]["Aff_year"].ToString(),
                        Header_templete = headertemplete,
                        Content_header = content_header,
                        Issue_dateDIv = issue_datedv,
                        Issue_date = mycode.date(),
                        PromttoDV= promttoDV,
                        Teachrrmrk= teachrrmrk,
                    });
                }
            }
            return MyFirmDetailData;
        }



        private void prep_final_term(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_Code,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section,isnull((select top 1 Rank from Exam_rank_master where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and Admission_no=t1.Admission_no and Term_id=" + Term_id + "),'0') as Rank from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Is_mandatory=1 and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' and t1.Subject in(select Sub_id from Subject_Mapping_New where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "') order by t2.Subject_position asc";
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
                    update_assesment_marks(dr["Subject"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_id);
                }
            }
        }

        private void update_assesment_marks(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            string querys = "select Session_Id,Branch_Id,Exam_Term_Id,Assessment_Name,Grade_System_Id,Class_id,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks,Term_assesment_unique_id from Exam_Assessment_Details where Session_Id=" + Session_id + " and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            DataTable dts = My.dataTable(querys);
            foreach (DataRow drs in dts.Rows)
            {
                string systm_grade_id = finalTwoTermN.get_systm_grd_id(Subject_id, Session_id, Class_id, Branch_id, Term_id, drs["Assessment_Id"].ToString());
                string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Subject_Activity_Name as Assessment_Name,t1.Sequence_No,t1.Assessment_Subject_Id,t1.Subject_Sub_Level_Id from Exam_Subject_Sub_Level t1 where t1.Session_Id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Exam_Term_Id=" + Term_id + "  and t1.Grade_System_Id=" + systm_grade_id + " and t1.Assessment_Id=" + drs["Assessment_Id"].ToString() + " and t1.Subject_id=" + Subject_id + "  order by t1.Sequence_No asc";
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
                        string marks = dps_termI.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic");

                        string[] stringSeparatorss = new string[] { "/" };
                        string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                        string obt_marks = arrs[0];
                        string full_marks = arrs[1];

                        string marks_type = arrs[2];
                        string total_no = arrs[3];
                        save_assesment_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marks, full_marks, marks_type, total_no, drs["Term_assesment_unique_id"].ToString());
                    }
                }
            }
        }





        private void save_assesment_marks(string session_id, string class_id, string admission_no, string term_id, string assesment_id, string subject_id, string Branch_id, string marks, string full_marks, string marks_type, string total_no, string Term_assesment_unique_id)
        {
            //try
            //{
            //    bool tot = mycode.cheknumer_Double(marks);
            //    if (tot == false)
            //    {
            //        marks = "0";
            //    }

            //}
            //catch
            //{

            //}
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
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
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
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
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
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
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
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
        }


        private void prep_final_term_coscholestic(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic' order by t2.Subject_position asc";

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_grade_master");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
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
