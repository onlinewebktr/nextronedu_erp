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

namespace school_web.Examination_Admin.slip.stmarry.api
{
    /// <summary>
    /// Summary description for bulk_final_report_card
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class bulk_final_report_card : System.Web.Services.WebService
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

            public List<Fetch_Details_of_Firm> MyFirmDetailData { get; set; }
            public List<Fetch_Details_of_report_head> MySubjectHeading { get; set; }
            public List<MySubjectMarkShow> MySubjectMarkShowItem { get; set; }
            public List<MyRpTotalCalculation> MyRpTotalCalculationItem { get; set; }
            public List<MyRpRanks> MyRpRanksItem { get; set; }
            public List<MyRpCoScholostic> MyRpCoScholosticItem { get; set; }
            public List<MyRpDescpline> MyRpDescplineItem { get; set; }
            public List<MyRpPromotTo> MyRpPromotToItem { get; set; }
            public List<MyRpMarksRange> MyRpMarksRangeItem { get; set; }
            public List<Fetch_Details_of_Signature> MySignatureDetailData { get; set; }
            public List<MyRpTermNumber> MyRpTermNumberItem { get; set; }
        }

        //========================FRIMDETAILS
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
        }
        public class Fetch_Details_of_report_head
        {
            public string Term_Name { get; set; }
            public string Total_Marks { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
        }
        public class MySubjectMarkShow
        {
            public string Subject { get; set; }
            public string Subject_name { get; set; }
            public string Total_mark_of_a_subject_for_termI { get; set; }
            public string Total_mark_of_a_subject_for_termII { get; set; }
            public string Total_mark_of_a_subject_for_termIII { get; set; }
            public string grade_of_a_subject_for_termI { get; set; }
            public string grade_of_a_subject_for_termII { get; set; }

            public string termI_termIi_average_percent { get; set; }
            public string termI_termII_grade { get; set; }
            public string ttl_marks_head_termI { get; set; }
            public string ttl_marks_head_termII { get; set; }
            public string ttl_marks_head_termIII { get; set; }
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
            public string Total_Precentage_class_of_term_I { get; set; }

            public string Total_class_of_term_II { get; set; }
            public string Total_Present_class_of_term_II { get; set; }
            public string Total_Precentage_class_of_term_II { get; set; }

            public string Total_class_of_term_III { get; set; }
            public string Total_Present_class_of_term_III { get; set; }
            public string Total_Precentage_class_of_term_III { get; set; }

            public string Final_working_days { get; set; }
            public string Final_persent_days { get; set; }
            public string Final_percent_days { get; set; }

            public string Overall_final_percent { get; set; }
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
            public string Total_marks_of_a_subject { get; set; }
            public string IsPassI { get; set; }
            public string IsPassII { get; set; }
            public string IsPassIII { get; set; }
            public string duesclassdesabled { get; set; }
            public string duesclassdesabled2 { get; set; }
            public string Desabletext { get; set; }
            public string PrintShow { get; set; }
        }

        public class MyRpTotalCalculation
        {
            public string Overall_obt_marks { get; set; }
            public string Overall_full_marks { get; set; }
            public string Overall_percents { get; set; }
            public string Mark_type { get; set; }
            public string P_remark { get; set; }
            public string Grade { get; set; }

            public string Obtained_Mark_TermI { get; set; }
            public string Obtained_Mark_TermII { get; set; }
            public string Obtained_Mark_TermIII { get; set; }

            public string Full_Mark_TermI { get; set; }
            public string Full_Mark_TermII { get; set; }
            public string Full_Mark_TermIII { get; set; }
            public string ObtMarkInWord { get; set; }
            public string TermI_perc { get; set; }
            public string TermII_perc { get; set; }
            public string TermIII_perc { get; set; }
            public string Comentory_remarks { get; set; }
        }
        public class MyRpRanks
        {
            public string Rank { get; set; }
        }
        public class MyRpCoScholostic
        {
            public string Subject_Name { get; set; }
            public string Total_marks_t1 { get; set; }
            public string Total_marks_t2 { get; set; }
            public string Total_marks_t3 { get; set; }
            public string RowCount { get; set; }
        }
        public class MyRpDescpline
        {
            public string Subject_Name { get; set; }
            public string Total_marks_t1 { get; set; }
            public string Total_marks_t2 { get; set; }
            public string Total_marks_t3 { get; set; }
            public string RowCount { get; set; }
        }



        public class MyRpPromotTo
        {
            public string Session { get; set; }
            public string Class_name { get; set; }
            public string Section { get; set; }
            public string ShowStatuS { get; set; }
        }
        public class MyRpMarksRange
        {
            public string Lower_Range { get; set; }
            public string Upper_Range { get; set; }
            public string Grade { get; set; }
            public string RowCount { get; set; }
            public string ColSpanPlus { get; set; }
        }
        public class Fetch_Details_of_Signature
        {
            public string Signature_name { get; set; }
            public string Signature_image { get; set; }
        }
        public class MyRpTermNumber
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
        }
        List<MyStudentsDetails> ShowMyStudents = new List<MyStudentsDetails>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_bulks(string Session_id, string Class_id, string Section, string Branch_id, string Term_idI, string Term_idII, string Term_idIII, string UserType)
        {
            string query = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname,ar.dob,ar.mothername,ar.fathername,ar.studentimagepath,CASE WHEN ar.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN ar.studentimagepath is not null THEN ar.studentimagepath END AS Student_img from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + Session_id + " and em.Class_id=" + Class_id + " and ar.Section='" + Section + "' and em.Branch_id=" + Branch_id + "  and  em.Term=" + Term_idIII + " and ar.Status='1' order by ar.rollnumber asc";
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
                    My.exeSql("delete from Exam_temp_assesment_total_no where Session_id='" + Session_id + "' and Admission_no='" + dr["Admission_no"].ToString() + "'; delete from Exam_temp_assesment_total_no_term_II where Session_id='" + Session_id + "' and Admission_no='" + dr["Admission_no"].ToString() + "'; delete from  Exam_marks  where Session_id='" + Session_id + "' and Admission_no='" + dr["Admission_no"].ToString() + "' and Created_by='AI'");
                    //================ GET FIRM DETAILS
                    List<Fetch_Details_of_Firm> MBFirmDetails = findFirmDetails();
                    List<Fetch_Details_of_report_head> MBdetailsHeading = findSubjectHeadings(Session_id, Class_id, Branch_id, Term_idI);
                    List<MySubjectMarkShow> MBdetails = findSubjectMarkShow(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, Term_idIII, UserType, Section);
                    List<MyRpTotalCalculation> MBTotCaldetails = findTotCalculation(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, Term_idIII);
                    List<MyRpRanks> MBRanksdetails = findRankCalculation(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, Term_idIII);
                    List<MyRpCoScholostic> MBCoScholesticdetails = findCosholestic(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, Term_idIII);
                    List<MyRpDescpline> MBDescpline = findDescpline(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, Term_idIII);
                    List<MyRpPromotTo> MBPromotdetails = findPromot(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, Term_idIII);
                    List<MyRpMarksRange> MBMarkRangedetails = findMarkRange(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, Term_idIII);
                    //================ SIgnatureS
                    List<Fetch_Details_of_Signature> MBSigDetails = findSigDetails(Session_id, dr["Admission_no"].ToString(), Class_id, Branch_id, dr["Section"].ToString());
                    List<MyRpTermNumber> MBTrmNumDetails = findTermNumDetails(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, Term_idIII);


                    string sdtimgs = dr["studentimagepath"].ToString();
                    if (sdtimgs == "")
                    {
                        sdtimgs = "hidden";
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

                        MyFirmDetailData = MBFirmDetails,
                        MySubjectHeading = MBdetailsHeading,
                        MySubjectMarkShowItem = MBdetails,
                        MyRpTotalCalculationItem = MBTotCaldetails,
                        MyRpRanksItem = MBRanksdetails,
                        MyRpCoScholosticItem = MBCoScholesticdetails,
                        MyRpDescplineItem = MBDescpline,
                        MyRpPromotToItem = MBPromotdetails,
                        MyRpMarksRangeItem = MBMarkRangedetails,
                        MySignatureDetailData = MBSigDetails,
                        MyRpTermNumberItem = MBTrmNumDetails,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(ShowMyStudents));
            }
        }


        private List<MySubjectMarkShow> findSubjectMarkShow(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Term_idIII, string UserType, string Section)
        {
            prep_final_term(Session_id, Class_id, Admission_no, Branch_id, Term_idI, Section);
            prep_final_term(Session_id, Class_id, Admission_no, Branch_id, Term_idII, Section);
            prep_final_term(Session_id, Class_id, Admission_no, Branch_id, Term_idIII, Section);

            List<MySubjectMarkShow> MySubjectMarkShowItem = new List<MySubjectMarkShow>();
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term_id,t1.Subject_id,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section from Exam_temp_assesment_total_no t1 join Subject_Master t2 on t1.Subject_id=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term_id=" + Term_idI + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' order by t2.Subject_position asc";
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
                My.exeSql("delete from Exam_fail_history_subjectwise where Session_Id=" + Session_id + " and Branch_id='" + Branch_id + "' and Class_id=" + Class_id + " and Admission_no='" + Admission_no + "' and Term_id=" + Term_idI + ";delete from Exam_fail_history_subjectwise where Session_Id=" + Session_id + " and Branch_id='" + Branch_id + "' and Class_id=" + Class_id + " and Admission_no='" + Admission_no + "' and Term_id=" + Term_idII + "; delete from Exam_fail_history_subjectwise where Session_Id=" + Session_id + " and Branch_id='" + Branch_id + "' and Class_id=" + Class_id + " and Admission_no='" + Admission_no + "' and Term_id=" + Term_idIII + "");
                string check_is_dues = "NO";
                if (UserType != "Admin")
                {
                    check_is_dues = Exam_setting.check_dues(Session_id, Class_id, Admission_no, Term_idIII);
                }
                string settings_for_final_year = exam_ips_I.get_final_year_setting(Session_id, Class_id, Branch_id);
                foreach (DataRow dr in dt.Rows)
                {
                    string ttl_mark_of_a_subject = stMarry_final.get_ttl_mark_of_a_subject_final_year(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idI, Term_idII, Term_idIII);

                    string sub_highest_mark = get_highest_mark(dr["Subject_id"].ToString(), Session_id, Class_id, dr["Section"].ToString());
                    //string sub_highest_mark = get_highest_mark(Session_id, Class_id, Branch_id, dr["Subject_id"].ToString(), Admission_no, Term_idI, Term_idII, Term_idIII);
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = ttl_mark_of_a_subject.Split(stringSeparatorss, StringSplitOptions.None);
                    string Total_mark_of_a_subject_for_termI = arrs[0];
                    string Total_mark_of_a_subject_for_termII = arrs[1];
                    string Total_mark_of_a_subject_for_termIII = arrs[2];
                    string grade_of_a_subject_for_termI = arrs[3];
                    string grade_of_a_subject_for_termII = arrs[4];
                    string grade_of_a_subject_for_termIII = arrs[5];

                    string termI_termIi_average_percent = arrs[6];
                    string termI_termII_grade = arrs[7];


                    string Total_class_of_term_I = arrs[8];
                    string Total_Present_class_of_term_I = arrs[9];
                    string Total_Present_attendance_of_term_I = arrs[10];


                    string Total_class_of_term_II = arrs[11];
                    string Total_Present_class_of_term_II = arrs[12];
                    string Total_Present_attendance_of_term_II = arrs[13];

                    string Total_class_of_term_III = arrs[14];
                    string Total_Present_class_of_term_III = arrs[15];
                    string Total_Present_attendance_of_term_III = arrs[16];


                    string Overall_final_mark = arrs[17];
                    string Overall_final_percent = arrs[18];
                    string Total_mark_of_a_subject = arrs[19];

                    string TermISubjPerc = arrs[20];
                    string TermIISubjPerc = arrs[21];
                    string TermIIISubjPerc = arrs[22];



                    string IsPassI = ""; string IsPassII = ""; string IsPassIII = "";
                    if (40 > My.toDouble(TermISubjPerc))
                    {
                        IsPassI = "SubjFail";
                        stMarry_final.save_fail_history(Session_id, Branch_id, Class_id, dr["Subject_id"].ToString(), Admission_no, Term_idI);
                    }
                    if (40 > My.toDouble(TermIISubjPerc))
                    {
                        IsPassII = "SubjFail";
                        stMarry_final.save_fail_history(Session_id, Branch_id, Class_id, dr["Subject_id"].ToString(), Admission_no, Term_idII);
                    }
                    if (40 > My.toDouble(TermIIISubjPerc))
                    {
                        IsPassIII = "SubjFail";
                        stMarry_final.save_fail_history(Session_id, Branch_id, Class_id, dr["Subject_id"].ToString(), Admission_no, Term_idIII);
                    }
                    //===========================
                    double final_percent_days = 0;
                    double final_working_days = My.toDouble(Total_class_of_term_I) + My.toDouble(Total_class_of_term_II) + My.toDouble(Total_class_of_term_III);
                    double final_persent_days = My.toDouble(Total_Present_class_of_term_I) + My.toDouble(Total_Present_class_of_term_II) + My.toDouble(Total_Present_class_of_term_III);


                    if (final_persent_days > 0)
                    {
                        final_percent_days = (final_persent_days / final_working_days) * 100;
                    }

                    //=========================== 
                    //===========================
                    string system_grade_id = Examination.get_system_grade_id_final(Session_id, Branch_id, Class_id);
                    string marks_output = Examination.get_marks_output(Session_id, Branch_id, system_grade_id);

                    string Total_mark_of_a_subject_for_termI_final = "";
                    string Total_mark_of_a_subject_for_termII_final = "";
                    string Total_mark_of_a_subject_for_termIII_final = "";
                    string ttl_marks_head_termI = "";
                    string ttl_marks_head_termII = "";
                    string ttl_marks_head_termIII = "";
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



                    if (My.toDouble(Total_mark_of_a_subject_for_termI) == 0)
                    {
                        Total_mark_of_a_subject_for_termI = "Ab";
                    }
                    if (My.toDouble(Total_mark_of_a_subject_for_termII) == 0)
                    {
                        Total_mark_of_a_subject_for_termII = "Ab";
                    }
                    if (My.toDouble(Total_mark_of_a_subject_for_termIII) == 0)
                    {
                        Total_mark_of_a_subject_for_termIII = "Ab";
                    }
                    if (marks_output == "Marks")
                    {
                        Total_mark_of_a_subject_for_termI_final = Total_mark_of_a_subject_for_termI;
                        Total_mark_of_a_subject_for_termII_final = Total_mark_of_a_subject_for_termII;
                        Total_mark_of_a_subject_for_termIII_final = Total_mark_of_a_subject_for_termIII;
                        ttl_marks_head_termI = "TOTAL (A)";
                        ttl_marks_head_termII = "TOTAL (B)";
                        ttl_marks_head_termII = "TOTAL (C)";
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
                        string Total_grade_of_a_subject_for_termIII_final = Examination.get_grade_final(Session_id, Branch_id, system_grade_id, My.toDouble(Total_mark_of_a_subject_for_termIII));

                        Total_mark_of_a_subject_for_termI_final = Total_grade_of_a_subject_for_termI_final;
                        Total_mark_of_a_subject_for_termII_final = Total_grade_of_a_subject_for_termII_final;
                        Total_mark_of_a_subject_for_termIII_final = Total_grade_of_a_subject_for_termIII_final;
                        ttl_marks_head_termI = "TOTAL (A)";
                        ttl_marks_head_termII = "TOTAL (B)";
                        ttl_marks_head_termII = "TOTAL (C)";
                        termI_grade = "TOTAL GRADE (A)";
                        termII_grade = "TOTAL GRADE (B)";
                        if_is_mrk_hide = "showed";
                        if_is_grade_ttl_mark_hide = "hidden";
                        colspanfiveandsix = "5";
                        overall_ab_grade = "GRADE (A+B/2) (100)";
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
                    string url = "hidden";
                    string remarkss = "hidden";

                    //QQQQQRRRR  

                    string Total_marks = stMarry_final.get_overall_total_marks_final(Session_id, Class_id, Admission_no, Branch_id, Term_idI, Term_idII, Term_idIII);
                    string[] stringSeparatorssxx = new string[] { "/" };
                    string[] arrsxx = Total_marks.Split(stringSeparatorssxx, StringSplitOptions.None);

                    string overall_obt_marks = arrsxx[0];
                    string overall_full_marks = arrsxx[1];
                    string overall_percent = arrsxx[2];


                    if (Overall_final_mark == "hidden")
                    {
                        url = "https://api.qrserver.com/v1/create-qr-code/?data=Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Overall Percentage: " + overall_percent + "%" + "&amp;size=110x110";
                    }
                    else
                    {
                        url = "https://api.qrserver.com/v1/create-qr-code/?data=Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Total Obtained Mark: " + overall_obt_marks + "/" + overall_full_marks + ", Overall Percentage: " + overall_percent + "%" + "&amp;size=110x110";
                    }

                    //========================
                    string isDues = "hidden"; string isDues2 = "hidden"; string desabletext = ""; string printShow = "";
                    if (check_is_dues == "YES")
                    {
                        isDues = "desabledRP";
                        isDues2 = "duesclassdesabled2";
                        desabletext = "Contact to school";
                        printShow = "showPrintfalse";
                    }
                    MySubjectMarkShowItem.Add(new MySubjectMarkShow
                    {
                        Subject = dr["Subject_id"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        Total_mark_of_a_subject_for_termI = Total_mark_of_a_subject_for_termI_final,
                        Total_mark_of_a_subject_for_termII = Total_mark_of_a_subject_for_termII_final,
                        Total_mark_of_a_subject_for_termIII = Total_mark_of_a_subject_for_termIII_final,

                        grade_of_a_subject_for_termI = grade_of_a_subject_for_termI,
                        grade_of_a_subject_for_termII = grade_of_a_subject_for_termII,

                        termI_termIi_average_percent = My.toDouble(termI_termIi_average_percent).ToString("0.0"),
                        termI_termII_grade = termI_termII_grade,


                        ttl_marks_head_termI = ttl_marks_head_termI,
                        ttl_marks_head_termII = ttl_marks_head_termII,
                        ttl_marks_head_termIII = ttl_marks_head_termIII,
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
                        Total_Precentage_class_of_term_I = Total_Present_attendance_of_term_I,


                        Total_class_of_term_II = Total_class_of_term_II,
                        Total_Present_class_of_term_II = Total_Present_class_of_term_II,
                        Total_Precentage_class_of_term_II = Total_Present_attendance_of_term_II,

                        Total_class_of_term_III = Total_class_of_term_III,
                        Total_Present_class_of_term_III = Total_Present_class_of_term_III,
                        Total_Precentage_class_of_term_III = Total_Present_attendance_of_term_III,

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
                        Subj_highest_mark = sub_highest_mark,
                        IsPassI = IsPassI,
                        IsPassII = IsPassII,
                        IsPassIII = IsPassIII,

                        duesclassdesabled = isDues,
                        duesclassdesabled2 = isDues2,
                        Desabletext = desabletext,
                        PrintShow = printShow,
                        Total_marks_of_a_subject = Total_mark_of_a_subject,
                    });
                }
            }
            return MySubjectMarkShowItem;
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

        private string get_highest_mark(string Session_id, string Class_id, string Branch_id, string Subject_id, string Admission_no, string Term_idI, string Term_idII, string Term_idIII)
        {
            double ttl_mark_of_subject = 0;
            DataTable dt = mycode.FillData("select Section from admission_registor where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and admissionserialnumber='" + Admission_no + "'");
            if (dt.Rows.Count > 0)
            {

                double marks = 0; double full_marks = 0;
                string sql = @"select top 1 * from (select isnull(sum(convert(float, Marks)),0) as Marks,isnull(sum(convert(float, Full_marks)),0) as Full_marks from Exam_temp_assesment_total_no t1  where t1.Session_id='" + Session_id + @"' and t1.Branch_id='" + Branch_id + @"' and t1.Class_id='" + Class_id + @"' and t1.Term_id='" + Term_idI + @"' and t1.Subject_id='" + Subject_id + @"' group by t1.Admission_no) t order by Marks desc;

                                       select top 1 * from (select isnull(sum(convert(float, Marks)),0) as Marks,isnull(sum(convert(float, Full_marks)),0) as Full_marks from Exam_temp_assesment_total_no_term_II t1   where t1.Session_id='" + Session_id + @"' and t1.Branch_id='" + Branch_id + @"' and t1.Class_id='" + Class_id + @"' and t1.Term_id='" + Term_idII + @"' and t1.Subject_id='" + Subject_id + @"'  group by t1.Admission_no) t order by Marks desc;

                                       select top 1 * from (select isnull(sum(convert(float, Marks)),0) as Marks,isnull(sum(convert(float, Full_marks)),0) as Full_marks from Exam_temp_assesment_total_no_term_II t1  where t1.Session_id='" + Session_id + @"' and t1.Branch_id='" + Branch_id + @"' and t1.Class_id='" + Class_id + @"' and t1.Term_id='" + Term_idIII + @"' and t1.Subject_id='" + Subject_id + @"'  group by t1.Admission_no) t order by Marks desc;";
                DataSet ds = mycode.Fill_Data_set(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[0];
                    if (dtTemp.Rows.Count != 0)
                    {
                        marks = marks + My.toDouble(dtTemp.Rows[0]["Marks"].ToString());
                        full_marks = full_marks + My.toDouble(dtTemp.Rows[0]["Full_marks"].ToString());
                    }
                    else
                    {
                        marks = marks + 0;
                        full_marks = full_marks + 0;
                    }
                }
                else
                {
                    marks = marks + 0;
                    full_marks = full_marks + 0;
                }
                //=====================
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[1];
                    if (dtTemp.Rows.Count != 0)
                    {
                        marks = marks + My.toDouble(dtTemp.Rows[0]["Marks"].ToString());
                        full_marks = full_marks + My.toDouble(dtTemp.Rows[0]["Full_marks"].ToString());
                    }
                    else
                    {
                        marks = marks + 0;
                        full_marks = full_marks + 0;
                    }
                }
                else
                {
                    marks = marks + 0;
                    full_marks = full_marks + 0;
                }
                //=====================
                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[2];
                    if (dtTemp.Rows.Count != 0)
                    {
                        marks = marks + My.toDouble(dtTemp.Rows[0]["Marks"].ToString());
                        full_marks = full_marks + My.toDouble(dtTemp.Rows[0]["Full_marks"].ToString());
                    }
                    else
                    {
                        marks = marks + 0;
                        full_marks = full_marks + 0;
                    }
                }
                else
                {
                    marks = marks + 0;
                    full_marks = full_marks + 0;
                }


                ttl_mark_of_subject = marks; //(marks / full_marks) * 100;
            }

            return Math.Round(ttl_mark_of_subject).ToString();
        }

        //================================= FIRM DETAILS
        private List<Fetch_Details_of_Firm> findFirmDetails()
        {
            List<Fetch_Details_of_Firm> MyFirmDetailData = new List<Fetch_Details_of_Firm>();
            string query = "select * from Firm_Details";
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
                    string frim_aff_no = dr["Affiliation"].ToString();
                    if (frim_aff_no == "" || frim_aff_no == "NA")
                    {
                        frim_aff_no = "hidden";
                    }
                    MyFirmDetailData.Add(new Fetch_Details_of_Firm
                    {
                        Frim_logo = dr["logo"].ToString(),
                        Firm_address = dr["address1"].ToString(),
                        Firm_email = dr["email"].ToString(),
                        Firm_name = dr["firm_name"].ToString(),
                        Firm_contact_no = "Contact Number : " + dr["contact_no"].ToString(),
                        Frim_aff_no = frim_aff_no,
                        Affiliated_by_full_text = dr["Affiliated_by_full_text"].ToString(),
                        Estd = "ESTD : " + dr["estd"].ToString(),
                        Watermar_image = dr["Watermark_image"].ToString(),
                        Website = dr["website"].ToString(),
                    });
                }
            }
            return MyFirmDetailData;
        }


        //=================================HEADING
        private List<Fetch_Details_of_report_head> findSubjectHeadings(string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            List<Fetch_Details_of_report_head> MySubjectHeading = new List<Fetch_Details_of_report_head>();
            string query = "select Short_Name,(select sum(isnull(convert(float, Maximum_Marks),0)) from Exam_Term_Details where Class_id='" + Class_id + "' and Session_Id='" + Session_id + "' and Branch_Id='" + Branch_id + "')  as Total_Marks from Exam_Term_Details where Class_id='" + Class_id + "' and Session_Id='" + Session_id + "' and Branch_Id='" + Branch_id + "' order by Sequence_No asc";
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MySubjectHeading.Add(new Fetch_Details_of_report_head
                    {
                        Term_Name = dt.Rows[i]["Short_Name"].ToString(),
                        Total_Marks = dt.Rows[i]["Total_Marks"].ToString(),
                    });
                }
            }
            return MySubjectHeading;
        }



        private List<MyRpTotalCalculation> findTotCalculation(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Term_idIII)
        {
            List<MyRpTotalCalculation> MyRpTotalCalculationItem = new List<MyRpTotalCalculation>();
            string Total_marks = stMarry_final.get_overall_total_marks_final(Session_id, Class_id, Admission_no, Branch_id, Term_idI, Term_idII, Term_idIII);
            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);

            string overall_obt_marks = arrs[0];
            string overall_full_marks = arrs[1];
            string overall_percent = arrs[2];

            string obtained_Mark_TermI = arrs[3];
            string obtained_Mark_TermII = arrs[4];
            string obtained_Mark_TermIII = arrs[5];
            string full_Mark_TermI = arrs[6];
            string full_Mark_TermII = arrs[7];
            string full_Mark_TermIII = arrs[8];


            double TermI_perc = (My.toDouble(obtained_Mark_TermI) / My.toDouble(full_Mark_TermI)) * 100;
            double TermII_perc = (My.toDouble(obtained_Mark_TermII) / My.toDouble(full_Mark_TermII)) * 100;
            double TermIII_perc = (My.toDouble(obtained_Mark_TermIII) / My.toDouble(full_Mark_TermIII)) * 100;

            string MarkType = arrs[9];
            string grade = arrs[10];

            string percent_remark = exam_ips_I.get_percent_remark(overall_percent);
            int number = (int)Convert.ToDouble(overall_obt_marks);
            string obtMarkInWord = mycode.NumberToWords(number);

            string comentory_remarks = stMarry_final.get_comentoryRemarks(Session_id, Class_id, Admission_no, Branch_id, Term_idIII);
            ////======================================
            double roundPrcT1 = Math.Round(TermI_perc);
            string finl_percT1 = TermI_perc.ToString("0.00");
            if (roundPrcT1 == My.toDouble(TermI_perc))
            {
                finl_percT1 = My.toDouble(TermI_perc).ToString();
            }

            double roundPrcT2 = Math.Round(TermII_perc);
            string finl_percT2 = TermII_perc.ToString("0.00");
            if (roundPrcT2 == My.toDouble(TermII_perc))
            {
                finl_percT2 = My.toDouble(TermII_perc).ToString();
            }

            double roundPrcT3 = Math.Round(TermIII_perc);
            string finl_percT3 = TermIII_perc.ToString("0.00");
            if (roundPrcT3 == My.toDouble(TermIII_perc))
            {
                finl_percT3 = My.toDouble(TermIII_perc).ToString();
            }

            MyRpTotalCalculationItem.Add(new MyRpTotalCalculation
            {
                Overall_obt_marks = overall_obt_marks,
                Overall_full_marks = overall_full_marks,
                Overall_percents = overall_percent,
                Mark_type = MarkType,
                P_remark = percent_remark,
                Grade = grade,

                Obtained_Mark_TermI = obtained_Mark_TermI,
                Obtained_Mark_TermII = obtained_Mark_TermII,
                Obtained_Mark_TermIII = obtained_Mark_TermIII,

                Full_Mark_TermI = full_Mark_TermI,
                Full_Mark_TermII = full_Mark_TermII,
                Full_Mark_TermIII = full_Mark_TermIII,
                ObtMarkInWord = obtMarkInWord,

                TermI_perc = finl_percT1,
                TermII_perc = finl_percT2,
                TermIII_perc = finl_percT3,
                Comentory_remarks = comentory_remarks,
            });

            return MyRpTotalCalculationItem;
        }

        private List<MyRpRanks> findRankCalculation(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Term_idIII)
        {
            List<MyRpRanks> MyRpRanksItem = new List<MyRpRanks>();
            string query = "select top 1 '1' as Term,isnull((select top 1 Rank from Exam_rank_master where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Term_id='" + Term_idI + "'),0) as Rank," + Term_idI + " as Term_id  from Exam_rank_master   UNION all select top 1 '2' as Term,isnull((select top 1 Rank from Exam_rank_master where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Term_id='" + Term_idII + "'),0) as Rank," + Term_idII + " as  Term_id  from Exam_rank_master UNION all select top 1 '3' as Term,isnull((select top 1 Rank from Exam_rank_master where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Term_id='" + Term_idIII + "'),0) as Rank," + Term_idIII + " as Term_id  from Exam_rank_master UNION all  select top 1 'Final' as Term,isnull((select top 1 Rank from Exam_rank_master_final_year where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "'),0) as Rank,'00' as Term_id  from Exam_rank_master_final_year";
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
                string final_check = "";
                foreach (DataRow dr in dt.Rows)
                {
                    string rank_data = dr["Rank"].ToString();
                    string checkFail = check_fail_in_subject(Session_id, Class_id, Admission_no, Branch_id, dr["Term_id"].ToString());
                    if (checkFail == "False")
                    {
                        rank_data = "NA";
                        final_check = "Fail";
                    }

                    if (dr["Term"].ToString() == "Final")
                    {
                        if (final_check == "Fail")
                        {
                            rank_data = "NA";
                        }
                    }

                    MyRpRanksItem.Add(new MyRpRanks
                    {
                        Rank = rank_data,
                    });
                }
            }
            return MyRpRanksItem;
        }
        private string check_fail_in_subject(string Session_id, string Class_id, string Admission_no, string Branch_id, string term_id)
        {
            string returN = "True";
            string query = "select Id from Exam_fail_history_subjectwise where  Session_Id=" + Session_id + " and Branch_id='" + Branch_id + "' and Class_id=" + Class_id + " and Term_id=" + term_id + " and Admission_no='" + Admission_no + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                returN = "False";
            }
            return returN;
        }


        private List<MyRpCoScholostic> findCosholestic(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Term_idIII)
        {
            List<MyRpCoScholostic> MyRpCoScholosticItem = new List<MyRpCoScholostic>();
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_idI + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic'  and Assessment in (select top 1 Assessment_Id from Exam_Assessment_Details where Session_Id=t1.Session_id and Exam_Term_Id=t1.Term and Assessment_Id=t1.Assessment and Class_id=t1.Class_id and Scholastic_Co_scholastic='Co-Scholastic') order by t2.Subject_position asc";
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
                    string obt_marks_t1 = "0";
                    string full_marks_t1 = "0";
                    string obt_marks_t2 = "0";
                    string full_marks_t2 = "0";
                    string obt_marks_t3 = "0";
                    string full_marks_t3 = "0";
                    string queryIII = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment,t3.Sequence_No from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id join Exam_Term_Details t3 on t1.Term=t3.Exam_Term_Id and t1.Session_id=t3.Session_id and t1.Class_id=t3.Class_id and t1.Branch_Id=t3.Branch_Id   where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic'  and Subject='" + dr["Subject"].ToString() + "' and Assessment in (select top 1 Assessment_Id from Exam_Assessment_Details where Session_Id=t1.Session_id and Exam_Term_Id=t1.Term and Assessment_Id=t1.Assessment and Class_id=t1.Class_id and Scholastic_Co_scholastic='Co-Scholastic') order by t3.Sequence_No asc";
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
                            if (i == 2)
                            {
                                obt_marks_t3 = arrs[0];
                                full_marks_t3 = arrs[1];
                            }
                        }
                    }

                    MyRpCoScholosticItem.Add(new MyRpCoScholostic
                    {
                        Subject_Name = dr["Subject_Name"].ToString(),
                        Total_marks_t1 = obt_marks_t1,
                        Total_marks_t2 = obt_marks_t2,
                        Total_marks_t3 = obt_marks_t3,
                        RowCount = dtIII.Rows.Count.ToString(),
                    });
                }
            }
            return MyRpCoScholosticItem;
        }


        //List<MyRpDescpline> MBDescpline = findDescpline
        private List<MyRpDescpline> findDescpline(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Term_idIII)
        {
            List<MyRpDescpline> MyRpDescplineItem = new List<MyRpDescpline>();
            string query = "select ptt.*,pt.Activity_name from Exam_Personality_Traits_Term_Wise ptt join Exam_Personality_Traits pt on ptt.Activity_Id=pt.Activity_Id where ptt.Session_id=" + Session_id + " and ptt.Branch_id='" + Branch_id + "' and ptt.Class_id=" + Class_id + " and ptt.Exam_Term_Id=" + Term_idI + " and ptt.Admission_no='" + Admission_no + "'  order by pt.Position asc";
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
                    string grade_t1 = "0";
                    string grade_t2 = "0";
                    string grade_t3 = "0";
                    string queryIII = "select ptt.*,pt.Activity_name from Exam_Personality_Traits_Term_Wise ptt join Exam_Personality_Traits pt on ptt.Activity_Id=pt.Activity_Id where ptt.Session_id=" + Session_id + " and ptt.Branch_id='" + Branch_id + "' and ptt.Class_id=" + Class_id + " and ptt.Activity_Id='" + dr["Activity_Id"].ToString() + "' and ptt.Admission_no='" + Admission_no + "'  order by pt.Position asc";
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
                            if (i == 2)
                            {
                                bool isNum = valid_amount(dtIII.Rows[i]["Term_grade"].ToString());
                                if (isNum == true)
                                {
                                    grade_t3 = Examination.get_personality_grade(Session_id, Class_id, Branch_id, dtIII.Rows[i]["Term_grade"].ToString());
                                }
                                else
                                {
                                    grade_t3 = dtIII.Rows[i]["Term_grade"].ToString();
                                }
                            }
                        }
                    }

                    MyRpDescplineItem.Add(new MyRpDescpline
                    {
                        Subject_Name = dr["Activity_name"].ToString(),
                        Total_marks_t1 = grade_t1,
                        Total_marks_t2 = grade_t2,
                        Total_marks_t3 = grade_t3,
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
            }
            return MyRpDescplineItem;
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
        private List<MyRpPromotTo> findPromot(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Term_idIII)
        {
            List<MyRpPromotTo> MyRpPromotToItem = new List<MyRpPromotTo>();
            string isStdTransfer = check_is_std_transfer(Session_id, Class_id, Admission_no, Branch_id);
            if (isStdTransfer == "Yes")
            {
                string query = "select top 1 * from admission_registor where Branch_id='" + Branch_id + "' and admissionserialnumber='" + Admission_no + "' and  (Transfer_Status='New' or Transfer_Status='NT') and Status='1' order by id desc";
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
                        MyRpPromotToItem.Add(new MyRpPromotTo
                        {
                            Session = dr["session"].ToString(),
                            Class_name = dr["class"].ToString(),
                            Section = dr["Section"].ToString(),
                            ShowStatuS = "show",
                        });
                    }
                }
            }
            else
            {
                MyRpPromotToItem.Add(new MyRpPromotTo
                {
                    Session = "hidden",
                    Class_name = "hidden",
                    Section = "hidden",
                    ShowStatuS = "hidden",
                });
            }
            return MyRpPromotToItem;
        }



        private string check_is_std_transfer(string Session_id, string Class_id, string Admission_no, string Branch_id)
        {
            string statuS = "No";
            string query = "select  * from admission_registor where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and admissionserialnumber='" + Admission_no + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][47].ToString() == "Transferred")
                {
                    statuS = "Yes";
                }
            }
            return statuS;
        }

        private List<MyRpMarksRange> findMarkRange(string p, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Term_idIII)
        {
            List<MyRpMarksRange> MyRpMarksRangeItem = new List<MyRpMarksRange>();
            string query = "select gsrg.* from Exam_Grade_System_Range_Grade gsrg join Exam_Term_Details td on gsrg.Session_Id=td.Session_Id and gsrg.Branch_Id=td.Branch_Id and gsrg.Grade_System_Id=td.Grade_System_Id where td.Exam_Term_Id=" + Term_idI + " and gsrg.Session_Id=" + Session_id + " and gsrg.Branch_Id='" + Branch_id + "' order by Lower_Range desc";
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
                int ColSpanPluss = rowcount1 + 1;
                foreach (DataRow dr in dt.Rows)
                {
                    MyRpMarksRangeItem.Add(new MyRpMarksRange
                    {
                        Lower_Range = dr["Lower_Range"].ToString(),
                        Upper_Range = dr["Upper_Range"].ToString(),
                        Grade = dr["Grade"].ToString(),
                        RowCount = dt.Rows.Count.ToString(),
                        ColSpanPlus = ColSpanPluss.ToString(),
                    });
                }
            }
            return MyRpMarksRangeItem;
        }

        private List<Fetch_Details_of_Signature> findSigDetails(string Session_id, string admission_no, string Class_id, string Branch_id, string Section)
        {
            List<Fetch_Details_of_Signature> MySignatureDetailData = new List<Fetch_Details_of_Signature>();
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

                    MySignatureDetailData.Add(new Fetch_Details_of_Signature
                    {
                        Signature_name = dr["Name"].ToString(),
                        Signature_image = stgnature,
                    });
                }
            }
            return MySignatureDetailData;
        }

        private List<MyRpTermNumber> findTermNumDetails(string p, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Term_idIII)
        {
            List<MyRpTermNumber> MyRpTermNumberItem = new List<MyRpTermNumber>();
            string query = "select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_idI + "  and Scholastic_Co_scholastic='Scholastic' UNION ALL select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_idII + "  and Scholastic_Co_scholastic='Scholastic' UNION ALL select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_idIII + "  and Scholastic_Co_scholastic='Scholastic'";
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MyRpTermNumberItem.Add(new MyRpTermNumber
                    {
                        Assessment_Name = dt.Rows[i]["Assessment_Name"].ToString(),
                        Short_Name = dt.Rows[i]["Short_Name"].ToString(),
                        Maximum_Marks = dt.Rows[i]["Maximum_Marks"].ToString(),
                        Term_Maximum_Marks = dt.Rows[i]["Term_Maximum_Marks"].ToString(),
                    });
                }
            }
            return MyRpTermNumberItem;
        }




        #region TempSaveMarks
        //====================================== 
        private void prep_final_term(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id, string Section)
        {
            update_if_absent(Session_id, Class_id, Admission_no, Branch_id, Term_id, Section);
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_Code,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section,isnull((select top 1 Rank from Exam_rank_master where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and Admission_no=t1.Admission_no and Term_id=" + Term_id + "),'0') as Rank from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' and t2.Is_mandatory=1 and t1.Subject in (select Sub_id from Subject_Mapping_New where Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Session_id='" + Session_id + "')  order by t2.Subject_position asc";
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

        private void update_if_absent(string session_id, string class_id, string admission_no, string branch_id, string term_id, string Section)
        {
            DataTable dtAssesment = My.dataTable("select * from dbo.[Exam_Assessment_Details] where Session_Id='" + session_id + "' and Exam_Term_Id='" + term_id + "' and Class_id='" + class_id + "' and Scholastic_Co_scholastic='Scholastic' and Istatus='1' order by Sequence_No asc");
            if (dtAssesment.Rows.Count > 0)
            {
                foreach (DataRow drs in dtAssesment.Rows)
                {
                    DataTable dtSubjSub = My.dataTable("select * from dbo.[Exam_Subject_Sub_Level] where Session_Id='" + session_id + "' and Class_id='" + class_id + "' and Exam_Term_Id='" + term_id + "' and Assessment_Id='" + drs["Assessment_Id"].ToString() + "' and Istatus='1' order by Sequence_No asc");
                    if (dtSubjSub.Rows.Count > 0)
                    {
                        foreach (DataRow drsubjS in dtSubjSub.Rows)
                        {
                            if (mycode.IsUserExist("select Id from Exam_marks where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Term='" + term_id + "' and Assessment='" + drs["Assessment_Id"].ToString() + "' and Subject='" + drsubjS["Subject_id"].ToString() + "' and Subject_activity='" + drsubjS["Subject_Sub_Level_Id"].ToString() + "' and Admission_no='" + admission_no + "'"))
                            {
                                if (mycode.IsUserExist("select Id from Subject_Mapping_New where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Sub_id='" + drsubjS["Subject_id"].ToString() + "' and Admission_no='" + admission_no + "'"))
                                {
                                }
                                else
                                {
                                    SqlCommand cmd;
                                    string query = "INSERT INTO Exam_marks (Session_id,Class_id,Section,Term,Assessment,Subject,Subject_activity,Admission_no,Marks,Branch_id,Created_by,Created_date,Created_idate,Mark_id,Is_character) values (@Session_id,@Class_id,@Section,@Term,@Assessment,@Subject,@Subject_activity,@Admission_no,@Marks,@Branch_id,@Created_by,@Created_date,@Created_idate,@Mark_id,@Is_character)";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@Session_id", session_id);
                                    cmd.Parameters.AddWithValue("@Class_id", class_id);
                                    cmd.Parameters.AddWithValue("@Section", Section);
                                    cmd.Parameters.AddWithValue("@Term", term_id);
                                    cmd.Parameters.AddWithValue("@Assessment", drs["Assessment_Id"].ToString());
                                    cmd.Parameters.AddWithValue("@Subject", drsubjS["Subject_id"].ToString());
                                    cmd.Parameters.AddWithValue("@Subject_activity", drsubjS["Subject_Sub_Level_Id"].ToString());
                                    cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                                    cmd.Parameters.AddWithValue("@Marks", "Ab");
                                    cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                                    cmd.Parameters.AddWithValue("@Created_by", "AI");
                                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                                    cmd.Parameters.AddWithValue("@Mark_id", "0");
                                    cmd.Parameters.AddWithValue("@Is_character", "1");
                                    if (My.InsertUpdateData(cmd))
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //========================================COSHOLASTIC
            DataTable CdtAssesment = My.dataTable("select * from Exam_Assessment_Details where Session_Id='" + session_id + "' and Exam_Term_Id='" + term_id + "' and Class_id='" + class_id + "' and Scholastic_Co_scholastic='Co-Scholastic' and Istatus='1' order by Sequence_No asc");
            if (CdtAssesment.Rows.Count > 0)
            {
                foreach (DataRow Cdrs in CdtAssesment.Rows)
                {
                    DataTable dtSubjSub = My.dataTable("select * from dbo.[Exam_Subject_Sub_Level] where Session_Id='" + session_id + "' and Class_id='" + class_id + "' and Exam_Term_Id='" + term_id + "' and Assessment_Id='" + Cdrs["Assessment_Id"].ToString() + "' and Istatus='1' order by Sequence_No asc");
                    if (dtSubjSub.Rows.Count > 0)
                    {
                        foreach (DataRow drsubjS in dtSubjSub.Rows)
                        {
                            if (mycode.IsUserExist("select Id from Exam_marks where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Term='" + term_id + "' and Assessment='" + Cdrs["Assessment_Id"].ToString() + "' and Subject='" + drsubjS["Subject_id"].ToString() + "' and Subject_activity='" + drsubjS["Subject_Sub_Level_Id"].ToString() + "' and Admission_no='" + admission_no + "'"))
                            {
                                if (mycode.IsUserExist("select Id from Subject_Mapping_New where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Sub_id='" + drsubjS["Subject_id"].ToString() + "' and Admission_no='" + admission_no + "'"))
                                {
                                }
                                else
                                {
                                    SqlCommand cmd;
                                    string query = "INSERT INTO Exam_marks (Session_id,Class_id,Section,Term,Assessment,Subject,Subject_activity,Admission_no,Marks,Branch_id,Created_by,Created_date,Created_idate,Mark_id,Is_character) values (@Session_id,@Class_id,@Section,@Term,@Assessment,@Subject,@Subject_activity,@Admission_no,@Marks,@Branch_id,@Created_by,@Created_date,@Created_idate,@Mark_id,@Is_character)";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@Session_id", session_id);
                                    cmd.Parameters.AddWithValue("@Class_id", class_id);
                                    cmd.Parameters.AddWithValue("@Section", Section);
                                    cmd.Parameters.AddWithValue("@Term", term_id);
                                    cmd.Parameters.AddWithValue("@Assessment", Cdrs["Assessment_Id"].ToString());
                                    cmd.Parameters.AddWithValue("@Subject", drsubjS["Subject_id"].ToString());
                                    cmd.Parameters.AddWithValue("@Subject_activity", drsubjS["Subject_Sub_Level_Id"].ToString());
                                    cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                                    cmd.Parameters.AddWithValue("@Marks", "Ab");
                                    cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                                    cmd.Parameters.AddWithValue("@Created_by", "AI");
                                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                                    cmd.Parameters.AddWithValue("@Mark_id", "0");
                                    cmd.Parameters.AddWithValue("@Is_character", "1");
                                    if (My.InsertUpdateData(cmd))
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void update_assesment_marks(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            string querys = "select Session_Id,Branch_Id,Exam_Term_Id,Assessment_Name,Grade_System_Id,Class_id,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks,Term_assesment_unique_id from Exam_Assessment_Details where Session_Id=" + Session_id + " and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            DataTable dts = My.dataTable(querys);
            foreach (DataRow drs in dts.Rows)
            {
                string systm_grade_id = exam_ips_I.get_systm_grd_id(Subject_id, Session_id, Class_id, Branch_id, Term_id);
                string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Subject_Activity_Name as Assessment_Name,t1.Sequence_No,t1.Assessment_Subject_Id,t1.Subject_Sub_Level_Id from Exam_Subject_Sub_Level t1 where t1.Session_Id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Assessment_Id=" + drs["Assessment_Id"].ToString() + " and t1.Subject_id=" + Subject_id + "  order by t1.Sequence_No asc";
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
                        if (systm_grade_id == "15")
                        {
                            string obt_markss = "0";
                            string full_markss = "0";
                            string marks_types = "0";
                            string total_nos = "0";

                            save_assesment_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_markss, full_markss, marks_types, total_nos, drs["Term_assesment_unique_id"].ToString());
                        }

                        string marks = exam_ips_I.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", dr["Subject_Sub_Level_Id"].ToString());

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
        #endregion
    }
}
