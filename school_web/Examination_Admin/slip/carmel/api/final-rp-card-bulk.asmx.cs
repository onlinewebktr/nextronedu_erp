using Newtonsoft.Json;
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

namespace school_web.Examination_Admin.slip.carmel.api
{
    /// <summary>
    /// Summary description for final_rp_card_bulk
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class final_rp_card_bulk : System.Web.Services.WebService
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
            public List<MyDecplineDetails> MyDecplineDetailsItem { get; set; }
            public List<MyMarkRangeDetails> MyMarkRangeDetailsItem { get; set; }
            public List<MySignatureDetails> MySignatureDetailsItem { get; set; }
            //public List<Fetch_Details_of_Rank> MyRankDetailData { get; set; }
            //public List<Fetch_Details_of_attendance> Fetch_Details_of_attendance_Item { get; set; }
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
        }

        public class Fetch_Details_of_report_head
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
            public string TermColSpan { get; set; }
            public string TermColSpanbtms { get; set; }
            public string TermColSpanAtndce { get; set; }
        }
        public class Fetch_Details_of_report_headII
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
            public string TermColSpan { get; set; }
            public string TermColSpanbtms { get; set; }
        }

        public class MySubjectMarkShow
        {
            public string Subject { get; set; }
            public string Subject_name { get; set; }
            public string Total_mark_of_a_subject_for_termI { get; set; }
            public string Total_mark_of_a_subject_for_termII { get; set; }
            public string IsPassI { get; set; }
            public string IsPassII { get; set; }
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
            public string SubjectPersentage { get; set; }
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
            public string HighestMarks { get; set; }
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
            public string T1ObtMark { get; set; }
            public string T2ObtMark { get; set; }
            public string T1Persentage { get; set; }
            public string T2Persentage { get; set; }
            public string RanksT1 { get; set; }
            public string RanksT2 { get; set; }
            public string RanksFinal { get; set; }
            public string TermGradeT1 { get; set; }
            public string TermGradeT2 { get; set; }
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
        public class MyDecplineDetails
        {
            public string Activity_name { get; set; }
            public string Term_grade1 { get; set; }
            public string Term_grade2 { get; set; }
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
                query = "select DISTINCT  em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname,ar.dob,ar.mothername,ar.fathername,ar.studentimagepath,CASE WHEN ar.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN ar.studentimagepath is not null THEN ar.studentimagepath END AS Student_img,isnull((select top 1 Rank from Exam_rank_master_final_year where Session_id=ar.Session_id and Class_id=ar.Class_id and Branch_id=ar.Branch_id and Admission_no=ar.admissionserialnumber),'0') as Rank from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + Session_id + " and em.Class_id=" + Class_id + " and em.Branch_id=" + Branch_id + "  and  em.Term=" + Term_idII + " and ar.admissionserialnumber='" + Adm_no + "' and ar.Status='1'  order by ar.rollnumber asc";
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
                string settings_for_final_year = FinalRpN.get_final_year_setting(Session_id, Class_id, Branch_id);
                foreach (DataRow dr in dt.Rows)
                {
                    Section = dr["Section"].ToString();
                    My.exeSql("delete from Exam_temp_assesment_total_no  where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and  Admission_no='" + dr["Admission_no"].ToString() + "';  delete from Exam_temp_assesment_total_no_term_II where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and  Admission_no='" + dr["Admission_no"].ToString() + "'; delete from Exam_temp_ass_total_no_ccf  where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and  Admission_no='" + dr["Admission_no"].ToString() + "';  delete from Exam_temp_ass_total_no_term_II_ccf where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and  Admission_no='" + dr["Admission_no"].ToString() + "';INSERT INTO Exam_rp_bulk_print_adm (Session_id,Admission_no) values ('" + Session_id + "','" + dr["Admission_no"].ToString() + "')");
                    prep_final_term(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, Term_idI);
                    prep_final_term(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, Term_idII);
                    prep_final_term_coscholestic(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, Term_idI);
                    prep_final_term_coscholestic(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, Term_idII);
                    prep_final_term_decipline(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, Term_idI);
                    prep_final_term_decipline(Session_id, Class_id, dr["Admission_no"].ToString(), Branch_id, Term_idII);


                    List<Fetch_Details_of_Firm> MBFirmDetails = findFirmDetails();
                    List<Fetch_Details_of_report_head> MBdetailsHeading = findSubjectHeading(Session_id, Class_id, Branch_id, Term_idI);
                    List<Fetch_Details_of_report_headII> MBdetailsHeadingII = findSubjectHeadingII(Session_id, Class_id, Branch_id, Term_idII);
                    List<MySubjectMarkShow> MBdetails = findSubjectMarkShow(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, dr["Section"].ToString());
                    List<MyTotalsDetails> MBtotalsdetails = findTotalsDT(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, dr["Section"].ToString());
                    List<MyCoScholesticDetails> MBCoScholesticdetails = findCoscholesticDT(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII);
                    List<MyDecplineDetails> MBDecplinedetails = findDescplineDT(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII);
                    List<MySignatureDetails> MBSigndetails = findSignatureDT(Session_id, Branch_id, dr["Section"].ToString(), Class_id);
                    List<MyMarkRangeDetails> MBMarkRangedetails = findMarkRangeDT(Session_id, Branch_id, Term_idI);

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
                        MyDecplineDetailsItem = MBDecplinedetails,
                        MySignatureDetailsItem = MBSigndetails,
                        MyMarkRangeDetailsItem = MBMarkRangedetails,
                    });
                }
                string json = JsonConvert.SerializeObject(ShowMyStudents);
                Context.Response.Write(json);
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //Context.Response.Write(js.Serialize(ShowMyStudents));
            }
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

            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_idI + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic' order by t2.Subject_position asc";
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
                    string obt_marks_t1 = "0";
                    string full_marks_t1 = "0";
                    string obt_marks_t2 = "0";
                    string full_marks_t2 = "0";
                    string queryIII = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment,t3.Sequence_No from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id join Exam_Term_Details t3 on t1.Term=t3.Exam_Term_Id and t1.Session_id=t3.Session_id and t1.Class_id=t3.Class_id and t1.Branch_Id=t3.Branch_Id   where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic'  and Subject='" + dr["Subject"].ToString() + "' order by t3.Sequence_No asc";
                    DataTable dtIII = My.dataTable(queryIII);
                    if (dtIII.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIII.Rows.Count; i++)
                        {
                            string marks = Examination.get_marks(dtIII.Rows[i]["Session_id"].ToString(), dtIII.Rows[i]["Class_id"].ToString(), dtIII.Rows[i]["Admission_no"].ToString(), dtIII.Rows[i]["Term"].ToString(), dtIII.Rows[i]["Assessment"].ToString(), dtIII.Rows[i]["Subject"].ToString(), dtIII.Rows[i]["Branch_id"].ToString(), "Co-Scholastic");
                            string[] stringSeparatorss = new string[] { "/" };
                            string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                            if (i == 0)
                            {
                                obt_marks_t1 = arrs[0];
                                full_marks_t1 = arrs[1];
                            }
                            if (i == 1)
                            {
                                obt_marks_t2 = arrs[0];
                                full_marks_t2 = arrs[1];
                            }
                        }
                    }

                    MyCoScholesticDetailsItem.Add(new MyCoScholesticDetails
                    {
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Total_marks_t1 = obt_marks_t1,
                        Total_marks_t2 = obt_marks_t2,
                        RowCount = dtIII.Rows.Count.ToString(),
                    });
                }
            }
            return MyCoScholesticDetailsItem;
        }

        //=================================MyDecplineDetails
        private List<MyDecplineDetails> findDescplineDT(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII)
        {
            List<MyDecplineDetails> MyDecplineDetailsItem = new List<MyDecplineDetails>();
            string query = "select ptt.*,pt.Activity_name from Exam_Personality_Traits_Term_Wise ptt join Exam_Personality_Traits pt on ptt.Activity_Id=pt.Activity_Id where ptt.Session_id=" + Session_id + " and ptt.Branch_id='" + Branch_id + "' and ptt.Class_id=" + Class_id + " and ptt.Exam_Term_Id=" + Term_idII + " and ptt.Admission_no='" + Admission_no + "'  order by pt.Position asc";
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
                    string grade_t1 = "0";
                    string grade_t2 = "0";
                    string queryIII = "select ptt.*,pt.Activity_name from Exam_Personality_Traits_Term_Wise ptt join Exam_Personality_Traits pt on ptt.Activity_Id=pt.Activity_Id where ptt.Session_id=" + Session_id + " and ptt.Branch_id='" + Branch_id + "' and ptt.Class_id=" + Class_id + " and ptt.Activity_Id='" + dr["Activity_Id"].ToString() + "' and ptt.Admission_no='" + Admission_no + "' and ptt.Exam_Term_Id='" + Term_idII + "' order by pt.Position asc";
                    DataTable dtIII = My.dataTable(queryIII);
                    if (dtIII.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIII.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                bool isNum = valid_amount(dtIII.Rows[i]["Term_grade"].ToString());
                                if (isNum == true)
                                {
                                    grade_t1 = Examination.get_personality_grade(Session_id, Class_id, Branch_id, dtIII.Rows[i]["Term_grade"].ToString());
                                }
                                else
                                {
                                    grade_t1 = dtIII.Rows[i]["Term_grade"].ToString();
                                }
                            }


                            if (i == 1)
                            {
                                bool isNum = valid_amount(dtIII.Rows[i]["Term_grade"].ToString());
                                if (isNum == true)
                                {
                                    grade_t2 = Examination.get_personality_grade(Session_id, Class_id, Branch_id, dtIII.Rows[i]["Term_grade"].ToString());
                                }
                                else
                                {
                                    grade_t2 = dtIII.Rows[i]["Term_grade"].ToString();
                                }
                            }

                        }
                    }


                    MyDecplineDetailsItem.Add(new MyDecplineDetails
                    {
                        Activity_name = dr["Activity_name"].ToString(),
                        Term_grade1 = grade_t1,
                        Term_grade2 = grade_t2,
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
            }
            return MyDecplineDetailsItem;
        }

        private List<MyTotalsDetails> findTotalsDT(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Section)
        {
            List<MyTotalsDetails> MyTotalsDetailsItem = new List<MyTotalsDetails>();

            //string ranksT1 = get_rank(Admission_no, Session_id, Class_id, Branch_id, Term_idI);
            //string ranksT2 = get_rank(Admission_no, Session_id, Class_id, Branch_id, Term_idII);
            //string RanksFinal = get_rankFinal(Admission_no, Session_id, Class_id, Branch_id);

            string term_1_mark = getterm1Marks(Admission_no, Session_id, Branch_id, Class_id, Term_idI);
            string term_2_mark = getterm2Marks(Admission_no, Session_id, Branch_id, Class_id, Term_idII);

            //T1
            string[] stringSeparator = new string[] { "/" };
            string[] arrT1 = term_1_mark.Split(stringSeparator, StringSplitOptions.None);
            string T1ObtMark = arrT1[0];
            string T1FullMark = arrT1[1];
            double T1Persentage = My.toDouble(T1ObtMark) / My.toDouble(T1FullMark) * 100;

            //T2
            string[] arrT2 = term_2_mark.Split(stringSeparator, StringSplitOptions.None);
            string T2ObtMark = arrT2[0];
            string T2FullMark = arrT2[1];
            double T2Persentage = My.toDouble(T2ObtMark) / My.toDouble(T2FullMark) * 100;

            if (T1Persentage.ToString().ToUpper() == "NAN")
            {
                T1Persentage = 0;
            }
            if (T2Persentage.ToString().ToUpper() == "NAN")
            {
                T2Persentage = 0;
            }


            double overallObtMark = (My.toDouble(T1ObtMark) + My.toDouble(T2ObtMark));
            double overall_pers = ((My.toDouble(T1ObtMark) + My.toDouble(T2ObtMark)) / (My.toDouble(T1FullMark) + My.toDouble(T2FullMark))) * 100;
            if (overall_pers.ToString().ToUpper() == "NAN")
            {
                overall_pers = 0;
            }


            //string Total_marks = FinalRpN.get_overall_total_marks_final(Session_id, Class_id, Admission_no, Branch_id, Term_idI);
            //string[] stringSeparatorss = new string[] { "/" };
            //string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);
            //string overall_obt_marks = arrs[0];
            //string overall_full_marks = arrs[1];
            //string overall_percent = arrs[2];
            //string overall_obt_marks_not_averge = arrs[3];
            //string overall_full_marks_not_averge = arrs[4];
            //string MarkType = arrs[5];
            //string grade = arrs[6];


            string overall_pers1 = "";
            decimal termIMark = Convert.ToDecimal(overall_pers);
            if (termIMark % 1 == 0) // Check if the decimal part is 0
            {
                overall_pers1 = ((int)termIMark).ToString(); // Convert to int and then to string
            }
            else
            {
                overall_pers1 = termIMark.ToString("0.00"); // Keep the decimal value
            }


            //===============================
            string overallObtMark1 = "";
            decimal overallObtMarks = Convert.ToDecimal(overallObtMark.ToString("0.00"));
            if (overallObtMarks % 1 == 0) // Check if the decimal part is 0
            {
                overallObtMark1 = ((int)overallObtMarks).ToString(); // Convert to int and then to string
            }
            else
            {
                overallObtMark1 = overallObtMarks.ToString("0.00"); // Keep the decimal value
            }
            //===============================
            string T1ObtMark1 = "";
            decimal T1ObtMarks = Convert.ToDecimal(My.toDouble(T1ObtMark).ToString("0.00"));
            if (T1ObtMarks % 1 == 0) // Check if the decimal part is 0
            {
                T1ObtMark1 = ((int)T1ObtMarks).ToString(); // Convert to int and then to string
            }
            else
            {
                T1ObtMark1 = T1ObtMarks.ToString("0.00"); // Keep the decimal value
            }
            //===============================
            string T2ObtMark1 = "";
            decimal T2ObtMarks = Convert.ToDecimal(My.toDouble(T2ObtMark).ToString("0.00"));
            if (T2ObtMarks % 1 == 0) // Check if the decimal part is 0
            {
                T2ObtMark1 = ((int)T2ObtMarks).ToString(); // Convert to int and then to string
            }
            else
            {
                T2ObtMark1 = T2ObtMarks.ToString("0.00"); // Keep the decimal value
            }
            //===============================
            string T1Persentage1 = "";
            decimal T1Persentages = Convert.ToDecimal(T1Persentage.ToString("0.00"));
            if (T1Persentages % 1 == 0) // Check if the decimal part is 0
            {
                T1Persentage1 = ((int)T1Persentages).ToString(); // Convert to int and then to string
            }
            else
            {
                T1Persentage1 = T1Persentages.ToString("0.00"); // Keep the decimal value
            }
            //===============================
            string T2Persentage1 = "";
            decimal T2Persentages = Convert.ToDecimal(T2Persentage.ToString("0.00"));
            if (T2Persentages % 1 == 0) // Check if the decimal part is 0
            {
                T2Persentage1 = ((int)T2Persentages).ToString(); // Convert to int and then to string
            }
            else
            {
                T2Persentage1 = T2Persentages.ToString("0.00"); // Keep the decimal value
            }


            Dictionary<string, object> dc1 = get_term_marks_calculation(Session_id, Branch_id, Class_id, Term_idI);
            string Grade_System_Id = (String)dc1["Grade_System_Id"];

            string termGradeT1 = get_grade(Session_id, Branch_id, Class_id, Term_idI, My.toDouble(T1Persentage1), Grade_System_Id);
            string termGradeT2 = get_grade(Session_id, Branch_id, Class_id, Term_idI, My.toDouble(T2Persentage1), Grade_System_Id);
            string overallGrade = get_grade(Session_id, Branch_id, Class_id, Term_idI, My.toDouble(overall_pers1), Grade_System_Id);
            //string percent_remark = FinalRpN.get_percent_remark(overall_pers.ToString());

            string class_teacher_remark = get_class_teacher_remark(Session_id, Branch_id, Class_id, Admission_no, Term_idII, Section);
            string promot_to_class = get_promot_to_class(Session_id, Branch_id, Class_id, Admission_no, Section);
            string[] stringSeparatorss = new string[] { "月" };
            string[] arr = promot_to_class.Split(stringSeparatorss, StringSplitOptions.None);
            string promot_to_classss = arr[0];
            string school_reopen_on = arr[1];
            overallObtMark1 = (My.toDouble(overallObtMark1) / 2).ToString();
            MyTotalsDetailsItem.Add(new MyTotalsDetails
            {
                Overall_obt_marks = overallObtMark1,
                Overall_full_marks = "", //overall_full_marks_not_averge,
                Overall_percents = overall_pers1,
                Mark_type = "", ///MarkType,
                P_remark = class_teacher_remark,
                Grade = overallGrade, //grade,
                T1ObtMark = T1ObtMark1,
                T2ObtMark = T2ObtMark1,
                T1Persentage = T1Persentage1,
                T2Persentage = T2Persentage1,
                //RanksT1 = ranksT1,
                //RanksT2 = ranksT2,
                //RanksFinal = RanksFinal,
                 
                TermGradeT1 = termGradeT1,
                TermGradeT2 = termGradeT2,
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

        private string get_class_teacher_remark(string session_id, string branch_id, string class_id, string admission_no, string term_idIII, string Section)
        {
            string rtrN = "";
            string query = "select Remarks from Exam_Commentary_Remark_Term_Wise_Entry where Session_id='" + session_id + "' and Branch_id='1' and Class_id='" + class_id + "' and Exam_Term_Id='" + term_idIII + "' and Admission_no='" + admission_no + "' and Section='" + Section + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                rtrN = dt.Rows[0][0].ToString();
            }
            return rtrN;
        }
        private string get_grade(string session_id, string branch_id, string Class_id, string Term_id, double overall_percent, string Grade_System_Id)
        {
            string result = get_final_grade(session_id, branch_id, Grade_System_Id, overall_percent);
            return result;
        }

        private static Dictionary<string, object> get_term_marks_calculation(string session_id, string branch_id, string class_id, string term_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = " select Grade_System_Id from Exam_Term_Details where Session_Id='" + session_id + "' and Branch_Id='" + branch_id + "' and Istatus=1 and Exam_Term_Id=" + term_id + " and Class_id=" + class_id + "";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                dc["Grade_System_Id"] = "0";
            }
            else
            {
                dc["Grade_System_Id"] = dt.Rows[0]["Grade_System_Id"].ToString();
            }
            return dc;
        }

        private static string get_final_grade(string session_id, string branch_id, string Grade_System_Id, double final_marks)
        {
            final_marks = Math.Round(final_marks);
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

                    if (final_marks >= Lower_Range && final_marks <= Upper_Range)
                    {
                        returN = dt.Rows[i]["Grade"].ToString();
                    }
                }
                return returN;
            }
        }

        private string get_rank(string admission_no, string session_id, string class_id, string branch_id, string term_idI)
        {
            string rtnRNN = "NA";
            DataTable dt = My.dataTable("select * from Exam_rank_master where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Term_id='" + term_idI + "'");
            if (dt.Rows.Count > 0)
            {
                rtnRNN = dt.Rows[0]["Rank"].ToString();
            }
            return rtnRNN;
        }
        private string get_rankFinal(string admission_no, string session_id, string class_id, string branch_id)
        {
            string rtnRNN = "NA";
            DataTable dt = My.dataTable("select * from Exam_rank_master_final_year where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "'");
            if (dt.Rows.Count > 0)
            {
                rtnRNN = dt.Rows[0]["Rank"].ToString();
            }
            return rtnRNN;
        }

        private string getterm1Marks(string admission_no, string session_id, string Branch_id, string class_id, string term_idI)
        {
            string rtrnValue = "0/0";
            DataTable dt = My.dataTable("select ISNULL(SUM(TRY_CONVERT(float, Marks)), 0) as obt_mark,ISNULL(SUM(TRY_CONVERT(float, Full_marks)), 0) as full_mark from Exam_temp_ass_total_no_ccf where Admission_no='" + admission_no + "' and Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_idI + "'");
            if (dt.Rows.Count > 0)
            {
                rtrnValue = dt.Rows[0]["obt_mark"].ToString() + "/" + dt.Rows[0]["full_mark"].ToString();
            }
            return rtrnValue;
        }
        private string getterm2Marks(string admission_no, string session_id, string Branch_id, string class_id, string term_idI)
        {
            string rtrnValue = "0/0";
            DataTable dt = My.dataTable("select ISNULL(SUM(TRY_CONVERT(float, Marks)), 0) as obt_mark,ISNULL(SUM(TRY_CONVERT(float, Full_marks)), 0) as full_mark from Exam_temp_ass_total_no_term_II_ccf where Admission_no='" + admission_no + "' and Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_idI + "'");
            if (dt.Rows.Count > 0)
            {
                rtrnValue = dt.Rows[0]["obt_mark"].ToString() + "/" + dt.Rows[0]["full_mark"].ToString();
            }
            return rtrnValue;
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
                string settings_for_final_year = FinalRpN.get_final_year_setting(Session_id, Class_id, Branch_id);
                foreach (DataRow dr in dt.Rows) 
                {
                    List<MySubjectMarkDetails> MBdetails = findmyBookingProduct(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idI);
                    List<MySubjectMarkDetailsII> MBdetailsII = findmyBookingProductII(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idII);
                    string ttl_mark_of_a_subject = FinalRpN.get_ttl_mark_of_a_subject_final_year(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idI, Term_idII);

                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = ttl_mark_of_a_subject.Split(stringSeparatorss, StringSplitOptions.None);
                    string Total_mark_of_a_subject_for_termI = arrs[0];
                    string Total_mark_of_a_subject_for_termII = arrs[1];


                    ///==================================================
                    decimal termIMarkOM = Convert.ToDecimal(Total_mark_of_a_subject_for_termI);
                    if (termIMarkOM % 1 == 0) // Check if the decimal part is 0
                    {
                        Total_mark_of_a_subject_for_termI = ((int)termIMarkOM).ToString(); // Convert to int and then to string
                    }
                    else
                    {
                        Total_mark_of_a_subject_for_termI = termIMarkOM.ToString(); // Keep the decimal value
                    }

                    decimal termIIMarkOM = Convert.ToDecimal(Total_mark_of_a_subject_for_termII);
                    if (termIIMarkOM % 1 == 0) // Check if the decimal part is 0
                    {
                        Total_mark_of_a_subject_for_termII = ((int)termIIMarkOM).ToString(); // Convert to int and then to string
                    }
                    else
                    {
                        Total_mark_of_a_subject_for_termII = termIIMarkOM.ToString(); // Keep the decimal value
                    }




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
                    string SubjectPersentage = arrs[14];

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
                        ttl_marks_head_termI = "Marks Obtained";
                        ttl_marks_head_termII = "Marks Obtained";
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
                        ttl_marks_head_termI = "Marks Obtained";
                        ttl_marks_head_termII = "Marks Obtained";
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
                    string Total_marks = FinalRpN.get_overall_total_marks_final(Session_id, Class_id, Admission_no, Branch_id, Term_idI);
                    string[] stringSeparatorssxx = new string[] { "/" };
                    string[] arrsxx = Total_marks.Split(stringSeparatorssxx, StringSplitOptions.None);

                    string overall_obt_marks = arrsxx[0];
                    string overall_full_marks = arrsxx[1];
                    string overall_percent = arrsxx[2];
                    string overall_obt_marks_not_averge = arrsxx[3];
                    string overall_full_marks_not_averge = arrsxx[4];
                    string MarkType = arrsxx[5];
                    string grade = arrsxx[6];


                    //if (MarkType == "GRADE")
                    //{
                    //    string qr_txt = "Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Overall Grade: " + grade;
                    //    url = "https://quickchart.io/qr?text=" + qr_txt + "&dark=000&light=ffffff&ecLevel=Q&format=svg";
                    //}
                    //else
                    //{
                    //    string qr_txt = "Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Total Obtained Mark: " + overall_obt_marks_not_averge + "/" + overall_full_marks_not_averge + ", Overall Percentage: " + overall_percent;
                    //    url = "https://quickchart.io/qr?text=" + qr_txt + "&dark=000&light=ffffff&ecLevel=Q&format=svg";
                    //}



                    //string IsPassI = ""; string IsPassII = "";
                    //string isAbsentT1 = checkisAbsent(Session_id, Branch_id, Class_id, dr["Subject_id"].ToString(), Admission_no, Term_idI, "Exam_temp_assesment_total_no");
                    //string isAbsentT2 = checkisAbsent(Session_id, Branch_id, Class_id, dr["Subject_id"].ToString(), Admission_no, Term_idII, "Exam_temp_assesment_total_no_term_II");





                    string highestMarks = ""; // get_highest_mark(dr["Subject_id"].ToString(), Session_id, Class_id, dr["Section"].ToString());

                    MySubjectMarkShowItem.Add(new MySubjectMarkShow
                    {
                        Subject = dr["Subject_id"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        Total_mark_of_a_subject_for_termI = Total_mark_of_a_subject_for_termI_final,
                        Total_mark_of_a_subject_for_termII = Total_mark_of_a_subject_for_termII_final,

                        //IsPassI = IsPassI,
                        //IsPassII = IsPassII,

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
                        SubjectPersentage = SubjectPersentage,

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

                        HighestMarks = highestMarks,

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

        private string checkisAbsent(string session_id, string branch_id, string class_id, string Subject_id, string admission_no, string term_idI, string tableName)
        {
            string retuRn = "0";
            try
            {
                DataTable dt = My.dataTable("select sum(convert(float, Marks)) as ttobt from " + tableName + " where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_idI + "' and Subject_id='" + Subject_id + "' and Admission_no='" + admission_no + "'");
                retuRn = "1";
            }
            catch (Exception ex)
            {
            }
            return retuRn;

        }

        private string get_highest_mark(string Subject_id, string session_id, string class_id, string Section)
        {
            string rtrnValue = "0";
            DataTable dt = My.dataTable("select top 1 * from (select Subject_id,isnull(sum(convert(float, Marks)),0) as Total_marks from Exam_highest_mark where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Subject_id='" + Subject_id + "' and Section='" + Section + "' group by Admission_no,Subject_id) t order by Total_marks desc");
            if (dt.Rows.Count > 0)
            {
                rtrnValue = dt.Rows[0]["Total_marks"].ToString();
            }
            return rtrnValue;
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
                if (rowcount1 == 1)
                {
                    MySubjectMarkItem.Add(new MySubjectMarkDetails
                    {
                        Subject_Name = "-",
                        Marks_term_I = "-",
                        Marks_term_II = "-",
                        Term_i_full_mark = "-",
                        Term_ii_full_mark = "-",
                    });
                }
                foreach (DataRow dr in dt.Rows)
                {
                    string Term_i_mark = "";
                    string Term_ii_mark = "";
                    string system_grade_id = Examination.get_system_grade_id_final(Session_id, Branch_id, Class_id);
                    string marks_output = Examination.get_marks_output(Session_id, Branch_id, system_grade_id);
                    if (marks_output == "Marks")
                    {
                        bool tot1 = mycode.cheknumer_Double(dr["Term_i_mark"].ToString());
                        if (tot1 == false)
                        {
                            Term_i_mark = dr["Term_i_mark"].ToString();
                        }
                        else
                        {
                            decimal termIMark = Convert.ToDecimal(dr["Term_i_mark"]);
                            if (termIMark % 1 == 0) // Check if the decimal part is 0
                            {
                                Term_i_mark = ((int)termIMark).ToString(); // Convert to int and then to string
                            }
                            else
                            {
                                Term_i_mark = termIMark.ToString(); // Keep the decimal value
                            }
                        }
                        Term_ii_mark = dr["Term_ii_mark"].ToString();
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
                if (rowcount1 == 1)
                {
                    MySubjectMarkItemII.Add(new MySubjectMarkDetailsII
                    {
                        Subject_Name = "-",
                        Marks_term_I = "-",
                        Marks_term_II = "-",
                        Term_i_full_mark = "-",
                        Term_ii_full_mark = "-",

                    });
                }
                foreach (DataRow dr in dt.Rows)
                {
                    string Term_i_mark = "";
                    string Term_ii_mark = "";
                    string system_grade_id = Examination.get_system_grade_id_final(Session_id, Branch_id, Class_id);
                    string marks_output = Examination.get_marks_output(Session_id, Branch_id, system_grade_id);
                    if (marks_output == "Marks")
                    {
                        Term_i_mark = dr["Term_i_mark"].ToString();

                        bool tot1 = mycode.cheknumer_Double(dr["Term_ii_mark"].ToString());
                        if (tot1 == false)
                        {
                            Term_ii_mark = dr["Term_ii_mark"].ToString();
                        }
                        else
                        {
                            decimal termIMark = Convert.ToDecimal(dr["Term_ii_mark"]);
                            if (termIMark % 1 == 0) // Check if the decimal part is 0
                            {
                                Term_ii_mark = ((int)termIMark).ToString(); // Convert to int and then to string
                            }
                            else
                            {
                                Term_ii_mark = termIMark.ToString(); // Keep the decimal value
                            }
                        }

                        //Term_ii_mark = dr["Term_ii_mark"].ToString();
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
                    colspanS = rowcount1 + 2;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string maxmarkS = dt.Rows[i]["Maximum_Marks"].ToString();
                    string extra_marks = "0";
                    if (i == 1)
                    {
                        extra_marks = FinalRpN.get_extra_marks(Class_id, Branch_id);
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
                        TermColSpanbtms = (colspanS - 1).ToString(),
                        TermColSpanAtndce = (colspanS + 1).ToString(),
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
                    colspanS = rowcount1 + 2;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string maxmarkS = dt.Rows[i]["Maximum_Marks"].ToString();
                    string extra_marks = "0";
                    if (i == 1)
                    {
                        extra_marks = FinalRpN.get_extra_marks(Class_id, Branch_id);
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
                        TermColSpanbtms = (colspanS - 1).ToString(),
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
                    string headertemplete = dr["header_templete"].ToString();
                    string content_header = "hidden";
                    if (headertemplete == "0")
                    {
                        headertemplete = "hidden";
                        content_header = "showd";
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
                string systm_grade_id = FinalRpN.get_systm_grd_id(Subject_id, Session_id, Class_id, Branch_id, Term_id, drs["Assessment_Id"].ToString());
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
                        string marks = FinalRpN.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic");

                        string[] stringSeparatorss = new string[] { "/" };
                        string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                        string obt_marks = arrs[0];
                        string full_marks = arrs[4];

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



                    //============================================
                    if (marks.ToUpper() == "ML" || marks.ToUpper() == "SL")
                    {
                        DataTable dt = My.dataTable("select Obt_marks from Exam_m_leave_setup where Full_marks='" + full_marks + "'");
                        if (dt.Rows.Count > 0)
                        {
                            marks = dt.Rows[0]["Obt_marks"].ToString();
                        }
                    }

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

                    SqlCommand cmd1;
                    string query1 = "INSERT INTO Exam_temp_ass_total_no_ccf (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Exam_termwise_assesment_id) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Exam_termwise_assesment_id)";
                    cmd1 = new SqlCommand(query1);
                    cmd1.Parameters.AddWithValue("@Session_id", session_id);
                    cmd1.Parameters.AddWithValue("@Branch_id", Branch_id);
                    cmd1.Parameters.AddWithValue("@Class_id", class_id);
                    cmd1.Parameters.AddWithValue("@Term_id", term_id);
                    cmd1.Parameters.AddWithValue("@Assessment_id", assesment_id);
                    cmd1.Parameters.AddWithValue("@Subject_id", subject_id);
                    cmd1.Parameters.AddWithValue("@Admission_no", admission_no);
                    if (marks_type == "Marks")
                    {
                        cmd1.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd1.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd1.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd1.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
                    if (My.InsertUpdateData(cmd1))
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



                    //============================================
                    if (marks.ToUpper() == "ML" || marks.ToUpper() == "SL")
                    {
                        DataTable dt = My.dataTable("select Obt_marks from Exam_m_leave_setup where Full_marks='" + full_marks + "'");
                        if (dt.Rows.Count > 0)
                        {
                            marks = dt.Rows[0]["Obt_marks"].ToString();
                        }
                    }
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

                    SqlCommand cmd1;
                    string query1 = "Update Exam_temp_ass_total_no_ccf set Marks=@Marks,Full_marks=@Full_marks,Exam_termwise_assesment_id=@Exam_termwise_assesment_id where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
                    cmd1 = new SqlCommand(query1);
                    if (marks_type == "Marks")
                    {
                        cmd1.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd1.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd1.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd1.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
                    if (My.InsertUpdateData(cmd1))
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



                    ///===============================================  
                    if (marks.ToUpper() == "ML" || marks.ToUpper() == "SL")
                    {
                        DataTable dt = My.dataTable("select Obt_marks from Exam_m_leave_setup where Full_marks='" + full_marks + "'");
                        if (dt.Rows.Count > 0)
                        {
                            marks = dt.Rows[0]["Obt_marks"].ToString();
                        }
                    }

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
                    SqlCommand cmd1;
                    string query1 = "INSERT INTO Exam_temp_ass_total_no_term_II_ccf (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Exam_termwise_assesment_id) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Exam_termwise_assesment_id)";
                    cmd1 = new SqlCommand(query1);
                    cmd1.Parameters.AddWithValue("@Session_id", session_id);
                    cmd1.Parameters.AddWithValue("@Branch_id", Branch_id);
                    cmd1.Parameters.AddWithValue("@Class_id", class_id);
                    cmd1.Parameters.AddWithValue("@Term_id", term_id);
                    cmd1.Parameters.AddWithValue("@Assessment_id", assesment_id);
                    cmd1.Parameters.AddWithValue("@Subject_id", subject_id);
                    cmd1.Parameters.AddWithValue("@Admission_no", admission_no);
                    if (marks_type == "Marks")
                    {
                        cmd1.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd1.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd1.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd1.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
                    if (My.InsertUpdateData(cmd1))
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



                    ///===============================================  
                    if (marks.ToUpper() == "ML" || marks.ToUpper() == "SL")
                    {
                        DataTable dt = My.dataTable("select Obt_marks from Exam_m_leave_setup where Full_marks='" + full_marks + "'");
                        if (dt.Rows.Count > 0)
                        {
                            marks = dt.Rows[0]["Obt_marks"].ToString();
                        }
                    }

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

                    SqlCommand cmd1;
                    string query1 = "Update Exam_temp_ass_total_no_term_II_ccf set Marks=@Marks,Full_marks=@Full_marks,Exam_termwise_assesment_id=@Exam_termwise_assesment_id where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
                    cmd1 = new SqlCommand(query1);
                    if (marks_type == "Marks")
                    {
                        cmd1.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd1.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd1.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd1.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
                    if (My.InsertUpdateData(cmd1))
                    {
                    }
                }
            }
        }


        private void prep_final_term_coscholestic(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
        {
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic' and t2.Is_mandatory=1  order by t2.Subject_position asc";

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
