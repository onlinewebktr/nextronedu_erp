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

namespace school_web.Examination_Admin.slip.kes.api
{
    /// <summary>
    /// Summary description for _3rdTerm
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class _3rdTerm : System.Web.Services.WebService
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

            public string Class_teacher_sig { get; set; }
            public string Principal_sig { get; set; }
            public string Examinee_sig { get; set; }
            public string MyRank { get; set; }

            public string ParantSign { get; set; }
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
            public string Header_templete { get; set; }
            public string Content_header { get; set; }

            public List<MySubjectMarkShowII> MySubjectMarkShowItemII { get; set; }
            public List<Fetch_Details_of_report_head> MySubjectHeading { get; set; }
            public List<Fetch_Details_of_report_head_assesment> MySubjectHeadingAssesment { get; set; }
            public List<Fetch_Details_of_report_CoScholastic> MyCoScholasticData { get; set; }
            public List<Fetch_Details_of_report_descpline> MyDescplineData { get; set; }
            public List<Fetch_Details_of_report_marks_range> MyMarkRangeData { get; set; }

            public List<Fetch_Details_of_report_marks_range_coscholestic> MyMarkRangeData_coscholestic { get; set; }
            public List<Fetch_Details_of_report_grade_remark> MyGradeRemarkData { get; set; }
            public List<Fetch_Details_of_Signature> MySignatureDetailData { get; set; }
            public List<Fetch_Details_of_Rank> MyRankDetailData { get; set; }
            public List<Fetch_Details_of_graph> MyGraphDetailData { get; set; }
            public List<Fetch_Details_of_overall_no> MyOverallNoDetailData { get; set; }

            public List<MyRpTotalCalculation> MyRpTotalCalculationItem { get; set; }
        }

        public class Fetch_Details_of_report_head
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
        }

        public class Fetch_Details_of_report_head_assesment
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
        }
        public class Fetch_Details_of_report_CoScholastic
        {
            public string Subject_Name { get; set; }
            public string Total_marks { get; set; }
            public string Full_marks { get; set; }
            public string IsShow { get; set; }
        }
        public class Fetch_Details_of_report_descpline
        {
            public string Activity_name { get; set; }
            public string Term_grade { get; set; }
        }
        //========================Instructions
        public class Fetch_Details_of_report_marks_range
        {
            public string Lower_Range { get; set; }
            public string Upper_Range { get; set; }
            public string Grade { get; set; }
            public string Remarks { get; set; }
            public string RowCount { get; set; }
        }
        public class Fetch_Details_of_report_marks_range_coscholestic
        {
            public string Lower_Range { get; set; }
            public string Upper_Range { get; set; }
            public string Grade { get; set; }
            public string RowCount { get; set; }
            public string IsCoScholasticGrade { get; set; }
        }

        //========================InstructionsII
        public class Fetch_Details_of_report_grade_remark
        {
            public string Grade_name { get; set; }
            public string Grade_remark { get; set; }
            public string RowCount { get; set; }
        }

        public class MySubjectMarkShowII
        {
            public string Subject { get; set; }
            public string Subject_name { get; set; }
            public string Total_marks { get; set; }
            public string Grade { get; set; }
            public string Overall_obt_mark { get; set; }
            public string GradeBG { get; set; }
            public string Gbgclass { get; set; }
            public string SubjFulmarks { get; set; }
            public string TermSubj_hlf_mark { get; set; }
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
            public string AttendanceColor { get; set; }
            public string Roll_show_new_line { get; set; }
            public string Is_std_img_hide { get; set; }
            public string Is_std_section_hide { get; set; }
            public string Aff_text { get; set; }
            public string Father_name1 { get; set; }
            public string Father_name2 { get; set; }
            public string Is_subj_code_hide { get; set; }
            public string Is_text_center { get; set; }
            public string Is_website_show { get; set; }
            public List<MySubjectMarkDetailsIII> MySubjectMarkItemIII { get; set; }
        }

        public class MySubjectMarkDetailsIII
        {
            public string Marks { get; set; }
            public string Subject_Name { get; set; }
            public string Subject { get; set; }
            public string Total_marks { get; set; }
            public string Full_marks { get; set; }
        }

        public class Fetch_Details_of_Signature
        {
            public string Signature_name { get; set; }
            public string Signature_image { get; set; }
        }

        public class Fetch_Details_of_Rank
        {
            public string Rank { get; set; }
            public string Student_name { get; set; }
            public string Total_obtained_mark { get; set; }
            public string Mark_percentage { get; set; }
            public string Grade { get; set; }
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

        public class Fetch_Details_of_overall_no
        {
            public string Overall_obt_marks { get; set; }
            public string Overall_full_marks { get; set; }
            public string Overall_percents { get; set; }
            public string Mark_type { get; set; }
            public string P_remark { get; set; }
            public string Grade { get; set; }
            public string GradeBG { get; set; }
            public string Grade_bg_class { get; set; }
            public string IfbgColorL { get; set; }
            public string IfbgColorR { get; set; }
        }

        public class MyRpTotalCalculation
        {
            public string Obtained_Mark_TermI { get; set; }
            public string Obtained_Mark_TermII { get; set; }
            public string Obtained_Mark_TermIII { get; set; }

            public string Full_Mark_TermI { get; set; }
            public string Full_Mark_TermII { get; set; }
            public string Full_Mark_TermIII { get; set; }
            public string GrandTTlOBT { get; set; }
            public string GrandTTlFullMark { get; set; }
            public string GrandTTlPersentage { get; set; }
        }


        List<MyStudentsDetails> ShowMyStudents = new List<MyStudentsDetails>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_bulks(string Session_id, string Class_id, string Section, string Branch_id, string Term_id, string Adm_no, string Term_id2, string Term_id3)
        {
            string query = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname,ar.dob,ar.mothername,ar.fathername,ar.studentimagepath,CASE WHEN ar.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN ar.studentimagepath is not null THEN ar.studentimagepath END AS Student_img, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name,isnull((select top 1 Rank from Exam_rank_master_final_year where Session_id=ar.Session_id and Class_id=ar.Class_id and Branch_id=em.Branch_id and Admission_no=ar.admissionserialnumber),'0') as Rank from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + Session_id + " and em.Class_id=" + Class_id + " and em.Branch_id=" + Branch_id + "  and  em.Term=" + Term_id3 + " and ar.admissionserialnumber='" + Adm_no + "' order by ar.rollnumber asc";
            if (Adm_no.ToUpper() == "BULK")
            {
                query = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname,ar.dob,ar.mothername,ar.fathername,ar.studentimagepath,CASE WHEN ar.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN ar.studentimagepath is not null THEN ar.studentimagepath END AS Student_img, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name,isnull((select top 1 Rank from Exam_rank_master_final_year where Session_id=ar.Session_id and Class_id=ar.Class_id and Branch_id=em.Branch_id and Admission_no=ar.admissionserialnumber),'0') as Rank from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + Session_id + " and em.Class_id=" + Class_id + " and ar.Section='" + Section + "' and em.Branch_id=" + Branch_id + "  and  em.Term=" + Term_id3 + " order by ar.rollnumber asc";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                string queryF = "select *,isnull((select top 1 Path from Header_templete where Status='1' and Type='Report_card'),'0') as header_templete from Firm_Details";
                DataTable dtF = My.dataTable(queryF);
                string headertemplete = dtF.Rows[0]["header_templete"].ToString();
                string content_header = "hidden";
                if (headertemplete == "0")
                {
                    headertemplete = "hidden";
                    content_header = "showd";
                }

                string parantSign = "showd";
                if (dtF.Rows[0]["firm_id"].ToString() == "IPSP-01")
                {
                    parantSign = "hidden";
                }


                foreach (DataRow dr in dt.Rows)
                {
                    My.exeSql("delete from Exam_temp_assesment_total_no where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Term_id='" + Term_id3 + "' and Admission_no='" + dr["Admission_no"].ToString() + "'; delete from Exam_temp_assesment_total_no_term_II where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Term_id='" + Term_id3 + "' and Admission_no='" + dr["Admission_no"].ToString() + "'; delete from Exam_rp_bulk_print_adm where Session_id='" + Session_id + "' and Admission_no='" + dr["Admission_no"].ToString() + "';INSERT INTO Exam_rp_bulk_print_adm (Session_id,Admission_no,Class_id,Section) values ('" + Session_id + "','" + dr["Admission_no"].ToString() + "','" + Class_id + "','" + Section + "')");
                    List<MySubjectMarkShowII> MBdetails = findSubjectMarkShowII(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_id3, dtF.Rows[0]["firm_id"].ToString());

                    List<Fetch_Details_of_report_head> MBdetailsHeading = findSubjectHeadings(Session_id, Class_id, Branch_id, Term_id3);

                    List<Fetch_Details_of_report_head_assesment> MBdetailsHeadingAssement = findSubjectHeadingsAssesment(Session_id, Class_id, Branch_id, Term_id3);

                    //================ CoScholastic
                    List<Fetch_Details_of_report_CoScholastic> MBdetailsCoscholestic = findCoScholasticData(Session_id, dr["Admission_no"].ToString(), Class_id, Branch_id, Term_id3);

                    //================ DescplinE
                    List<Fetch_Details_of_report_descpline> MBdetailsDescplinE = findDescplineData(Session_id, dr["Admission_no"].ToString(), Class_id, Branch_id, Term_id3);

                    //================ Mark Range
                    List<Fetch_Details_of_report_marks_range> MBdetailsMarkRange = findMarkRangeData(Session_id, Branch_id, Term_id3);

                    // List<Fetch_Details_of_report_marks_range_coscholestic> MBdetailsMarkRange_coscholestic = findMarkRangeData_coscholestic(Session_id, Branch_id, Term_id3, Class_id);

                    //================ Grade Remarks
                    //List<Fetch_Details_of_report_grade_remark> MBdetailsGradeRemark = findGradeRemarkData();

                    List<MyRpTotalCalculation> MBTotCaldetails = findTotCalculation(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_id, Term_id2, Term_id3, dr["Section"].ToString());


                    //================ SIgnatureS
                    List<Fetch_Details_of_Signature> MBSigDetails = findSigDetails(Session_id, dr["Admission_no"].ToString(), Class_id, Branch_id, dr["Section"].ToString());

                    //================ Ranks
                    //List<Fetch_Details_of_Rank> MBRank = findRankDetails(Session_id, dr["Admission_no"].ToString(), Class_id, Branch_id, dr["Section"].ToString(), Term_id3);

                    //================ Graph
                    // List<Fetch_Details_of_graph> MBGraph = findGraphDetails(Session_id, dr["Admission_no"].ToString(), Class_id, Branch_id, dr["Section"].ToString(), Term_id3);

                    //================ OverAllNo
                    List<Fetch_Details_of_overall_no> MBOverALL = findOverallNoDetails(Session_id, dr["Admission_no"].ToString(), Class_id, Branch_id, dr["Section"].ToString(), Term_id3);

                    string signatures = kesFM.get_signature(Session_id, Class_id, Section, Branch_id);
                    string[] stringSeparatorss = new string[] { ">" };
                    string[] arrs = signatures.Split(stringSeparatorss, StringSplitOptions.None);
                    string class_teacher_sig = arrs[0];
                    string principal_sig = arrs[1];
                    string examinee_sig = arrs[2];
                    string term_name = kesFM.get_term_name(Branch_id, Term_id3);

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
                        Term_name = term_name,
                        For_class = dr["class"].ToString(),
                        Session = dr["session"].ToString(),
                        // Student_image = dr["Student_img"].ToString(),


                        Student_image = sdtimgs,
                        Class_teacher_sig = class_teacher_sig,
                        Principal_sig = principal_sig,
                        Examinee_sig = examinee_sig,
                        MyRank = rank,

                        ParantSign = parantSign,

                        //FIRM DETAILS
                        Frim_logo = dtF.Rows[0]["logo"].ToString(),
                        Firm_address = dtF.Rows[0]["address1"].ToString(),
                        Firm_email = dtF.Rows[0]["email"].ToString(),
                        Firm_name = dtF.Rows[0]["firm_name"].ToString(),
                        Firm_contact_no = "Contact Number : " + dtF.Rows[0]["contact_no"].ToString(),
                        Frim_aff_no = "AFF. No. : " + dtF.Rows[0]["Affiliation"].ToString(),
                        Affiliated_by_full_text = dtF.Rows[0]["Affiliated_by_full_text"].ToString(),
                        Estd = "ESTD : " + dtF.Rows[0]["estd"].ToString(),
                        Watermar_image = dtF.Rows[0]["Watermark_image"].ToString(),
                        Website = dtF.Rows[0]["website"].ToString(),
                        Header_templete = headertemplete,
                        Content_header = content_header,


                        MySubjectMarkShowItemII = MBdetails,
                        MySubjectHeading = MBdetailsHeading,
                        MySubjectHeadingAssesment = MBdetailsHeadingAssement,
                        MyCoScholasticData = MBdetailsCoscholestic,
                        MyDescplineData = MBdetailsDescplinE,
                        MyMarkRangeData = MBdetailsMarkRange,
                        // MyMarkRangeData_coscholestic = MBdetailsMarkRange_coscholestic,
                        // MyGradeRemarkData = MBdetailsGradeRemark,
                        MyRpTotalCalculationItem = MBTotCaldetails,

                        MySignatureDetailData = MBSigDetails,
                        // MyRankDetailData = MBRank,
                        // MyGraphDetailData = MBGraph,
                        MyOverallNoDetailData = MBOverALL
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(ShowMyStudents));
            }
        }

        private List<Fetch_Details_of_overall_no> findOverallNoDetails(string Session_id, string admission_no, string Class_id, string Branch_id, string Section, string Term_id)
        {
            List<Fetch_Details_of_overall_no> MyOverallNoDetailData = new List<Fetch_Details_of_overall_no>();
            string Total_marks = kesFM.get_overall_total_marks_bg(Session_id, Class_id, admission_no, Branch_id, Term_id);
            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);
            string overall_obt_marks = arrs[0];
            string overall_full_marks = arrs[1];
            string overall_percent = arrs[2];
            string MarkType = arrs[3];
            string grade = arrs[4];
            string gradeBG = arrs[5];

            string ifbgColorLft = "";
            string ifbgColorRght = "";

            string grade_bg_class = "";
            if (gradeBG.Length == 7)
            {
                gradeBG = "background:" + gradeBG;
                grade_bg_class = "gradeBGCls";

                ifbgColorLft = "bgColorPddL";
                ifbgColorRght = "bgColorPddR";
            }

            MyOverallNoDetailData.Add(new Fetch_Details_of_overall_no
            {
                Overall_obt_marks = overall_obt_marks,
                Overall_full_marks = overall_full_marks,
                Overall_percents = overall_percent,
                Mark_type = MarkType,
                Grade = grade,
                GradeBG = gradeBG,
                Grade_bg_class = grade_bg_class,

                IfbgColorL = ifbgColorLft,
                IfbgColorR = ifbgColorRght,
            });
            return MyOverallNoDetailData;
        }

        private List<MyRpTotalCalculation> findTotCalculation(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Term_idIII, string Section)
        {
            List<MyRpTotalCalculation> MyRpTotalCalculationItem = new List<MyRpTotalCalculation>();
            DataTable dt = My.dataTable("select ISNULL(SUM(TRY_CONVERT(float, Marks)), 0) AS total_obt_mark, ISNULL(SUM(TRY_CONVERT(float, Full_mark)), 0) AS total_Full_mark,'TI' as TermName from (SELECT TRY_CONVERT(float, Marks) AS Marks,  ( SELECT TOP 1 TRY_CONVERT(float, Maximum_Marks) FROM Exam_Subject_Sub_Level  WHERE Session_Id = Exam_marks.Session_id AND Admission_no = Exam_marks.Admission_no  AND Branch_id = '" + Branch_id + "'  AND Class_id = Exam_marks.Class_id  AND Exam_Term_Id = Exam_marks.Term AND  Assessment_Id = Exam_marks.Assessment  AND Subject_Sub_Level_Id = Exam_marks.Subject_activity) AS Full_mark from Exam_marks where Session_id='" + Session_id + "' and Admission_no='" + Admission_no + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Term='" + Term_idI + "' and Subject in (select Sub_id from Subject_Mapping_New where Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Session_id='" + Session_id + "') and Subject in (select Subject_id from Subject_Master where course_id='" + Class_id + "' and (Is_optional = 0 OR Is_optional IS NULL))) t  union all select ISNULL(SUM(TRY_CONVERT(float, Marks)), 0) AS total_obt_mark, ISNULL(SUM(TRY_CONVERT(float, Full_mark)), 0) AS total_Full_mark,'TII' as TermName from (SELECT TRY_CONVERT(float, Marks) AS Marks,  ( SELECT TOP 1 TRY_CONVERT(float, Maximum_Marks) FROM Exam_Subject_Sub_Level  WHERE Session_Id = Exam_marks.Session_id AND Admission_no = Exam_marks.Admission_no  AND Branch_id = '" + Branch_id + "'  AND Class_id = Exam_marks.Class_id  AND Exam_Term_Id = Exam_marks.Term AND  Assessment_Id = Exam_marks.Assessment  AND Subject_Sub_Level_Id = Exam_marks.Subject_activity) AS Full_mark from Exam_marks where Session_id = '" + Session_id + "' and Admission_no='" + Admission_no + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Term='" + Term_idII + "' and Subject in (select Sub_id from Subject_Mapping_New where Class_id = '" + Class_id + "' and Admission_no = '" + Admission_no + "' and Session_id = '" + Session_id + "')  and Subject in (select Subject_id from Subject_Master where course_id='" + Class_id + "' and (Is_optional = 0 OR Is_optional IS NULL))) t union all select ISNULL(SUM(TRY_CONVERT(float, Marks)), 0) AS total_obt_mark, ISNULL(SUM(TRY_CONVERT(float, Full_mark)), 0) AS total_Full_mark,'TIII' as TermName from (SELECT TRY_CONVERT(float, Marks) AS Marks,  ( SELECT TOP 1 TRY_CONVERT(float, Maximum_Marks) FROM Exam_Subject_Sub_Level  WHERE Session_Id = Exam_marks.Session_id AND Admission_no = Exam_marks.Admission_no  AND Branch_id = '" + Branch_id + "'  AND Class_id = Exam_marks.Class_id  AND Exam_Term_Id = Exam_marks.Term AND  Assessment_Id = Exam_marks.Assessment  AND Subject_Sub_Level_Id = Exam_marks.Subject_activity) AS Full_mark from Exam_marks where Session_id = '" + Session_id + "' and Admission_no = '" + Admission_no + "' and Branch_id = '" + Branch_id + "' and Class_id = '" + Class_id + "' and Term = '" + Term_idIII + "' and Subject in (select Sub_id from Subject_Mapping_New where Class_id = '" + Class_id + "' and Admission_no = '" + Admission_no + "' and Session_id = '" + Session_id + "')  and Subject in (select Subject_id from Subject_Master where course_id='" + Class_id + "' and (Is_optional = 0 OR Is_optional IS NULL))) t");
            if (dt.Rows.Count > 0)
            {
                string obtained_Mark_TermI = "0";
                string obtained_Mark_TermII = "0";
                string obtained_Mark_TermIII = "0";
                string full_Mark_TermI = "0";
                string full_Mark_TermII = "0";
                string full_Mark_TermIII = "0";
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["TermName"].ToString() == "TI")
                    {
                        obtained_Mark_TermI = dr["total_obt_mark"].ToString();
                        full_Mark_TermI = dr["total_Full_mark"].ToString();
                    }
                    if (dr["TermName"].ToString() == "TII")
                    {
                        obtained_Mark_TermII = dr["total_obt_mark"].ToString();
                        full_Mark_TermII = dr["total_Full_mark"].ToString();
                    }
                    if (dr["TermName"].ToString() == "TIII")
                    {
                        obtained_Mark_TermIII = dr["total_obt_mark"].ToString();
                        full_Mark_TermIII = dr["total_Full_mark"].ToString();
                    }
                }


                double grandTTlOBT = My.toDouble(obtained_Mark_TermI) + My.toDouble(obtained_Mark_TermII) + My.toDouble(obtained_Mark_TermIII);
                double grandTTlFull = My.toDouble(full_Mark_TermI) + My.toDouble(full_Mark_TermII) + My.toDouble(full_Mark_TermIII);
                double grandTTlPersentage = (My.toDouble(grandTTlOBT) / My.toDouble(grandTTlFull)) * 100;

                MyRpTotalCalculationItem.Add(new MyRpTotalCalculation
                {
                    Obtained_Mark_TermI = obtained_Mark_TermI,
                    Obtained_Mark_TermII = obtained_Mark_TermII,
                    Obtained_Mark_TermIII = obtained_Mark_TermIII,

                    Full_Mark_TermI = full_Mark_TermI,
                    Full_Mark_TermII = full_Mark_TermII,
                    Full_Mark_TermIII = full_Mark_TermIII,

                    GrandTTlOBT = grandTTlOBT.ToString(),
                    GrandTTlFullMark = grandTTlFull.ToString(),
                    GrandTTlPersentage = grandTTlPersentage.ToString("0.00")
                });
            }



            return MyRpTotalCalculationItem;
        }
        private string get_grade(string session_id, string branch_id, string Class_id, string Term_id, double overall_percent, string Grade_System_Id)
        {
            string result = get_final_grade(session_id, branch_id, Grade_System_Id, overall_percent);
            return result;
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
        private List<Fetch_Details_of_graph> findGraphDetails(string Session_id, string admission_no, string Class_id, string Branch_id, string Section, string Term_id)
        {
            List<Fetch_Details_of_graph> MyGraphDetailData = new List<Fetch_Details_of_graph>();
            string tableName = "Exam_temp_assesment_total_no_term_II";
            string first_term = Exam_setting.get_first_term(Session_id, Branch_id, Class_id);
            if (first_term == Term_id)
            {
                tableName = "Exam_temp_assesment_total_no";
            }
            string query = "select t2.Subject_name,t2.Subject_Short_Name,isnull(sum(convert(float, Marks)),0) as Total_obtained_mark,isnull(sum(convert(float, Full_marks)),0) as Total_full_mark from " + tableName + " t1 join Subject_Master t2 on t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id and t1.Branch_id=t2.Branch_id where t1.Session_id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Term_id=" + Term_id + " and t1.Admission_no='" + admission_no + "' group by t2.Subject_name,t2.Subject_position,t2.Subject_Short_Name order by t2.Subject_position asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_signature_setting");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                double grphwidth = (100 / rowcount1);
                int c = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    c++;
                    double final_perc = (My.toDouble(dr["Total_obtained_mark"].ToString()) / My.toDouble(dr["Total_full_mark"].ToString())) * 100;
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
                        Total_obtained_mark = dr["Total_obtained_mark"].ToString(),
                        Total_full_mark = dr["Total_full_mark"].ToString(),
                        Grph_width = grphwidth.ToString("0.00"),
                        BlankHeight = blankHeight.ToString("0.00"),
                        final_perc = final_perc.ToString(),
                        bg_color = bg_color,
                    });
                }
            }
            return MyGraphDetailData;
        }

        private List<Fetch_Details_of_Rank> findRankDetails(string Session_id, string admission_no, string Class_id, string Branch_id, string Section, string Term_id)
        {
            List<Fetch_Details_of_Rank> MyRankDetailData = new List<Fetch_Details_of_Rank>();
            string query = "select   t2.class,t2.rollnumber,t2.session,t2.studentname,t1.*,(select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=t1.Term_id) as Term_name from Exam_rank_master t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Section='" + Section + "' and t1.Term_id=" + Term_id + " and Rank<=3 order by Rank asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_signature_setting");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                string system_grade_id = kesFM.get_system_grade_id_final(Session_id, Branch_id, Class_id);
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
                    string grade = kesFM.get_grade_final(Session_id, Branch_id, system_grade_id, My.toDouble(dr["Mark_percentage"].ToString()));
                    MyRankDetailData.Add(new Fetch_Details_of_Rank
                    {
                        Rank = rank,
                        Student_name = dr["studentname"].ToString(),
                        Total_obtained_mark = dr["Total_obtained_mark"].ToString(),
                        Mark_percentage = dr["Mark_percentage"].ToString(),
                        Grade = grade,
                    });
                }
            }
            return MyRankDetailData;
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
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string stgnature = "";
                    if (dr["Signature_type"].ToString() == "1")
                    {
                        stgnature = kesFM.get_class_teacher_signature(Session_id, Class_id, Section, Branch_id);
                    }
                    if (dr["Signature_type"].ToString() == "2")
                    {
                        stgnature = kesFM.get_principal_signature(Session_id, Branch_id);
                    }
                    if (dr["Signature_type"].ToString() == "3")
                    {
                        stgnature = kesFM.get_examinee_or_parent_signature(Session_id, Branch_id, dr["Signature_type_e_p"].ToString());
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


        //=================================HEADING
        private List<Fetch_Details_of_report_head> findSubjectHeadings(string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            List<Fetch_Details_of_report_head> MySubjectHeading = new List<Fetch_Details_of_report_head>();
            string query = "select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' and Istatus=1 order by Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MySubjectHeading.Add(new Fetch_Details_of_report_head
                    {
                        Short_Name = "Maximum Marks",
                    });
                    MySubjectHeading.Add(new Fetch_Details_of_report_head
                    {
                        Short_Name = "Pass Marks",
                    });
                    MySubjectHeading.Add(new Fetch_Details_of_report_head
                    {
                        Short_Name = "Highest Marks",
                    });
                    MySubjectHeading.Add(new Fetch_Details_of_report_head
                    {
                        Short_Name = "Obtained Marks",
                    });
                }
            }
            return MySubjectHeading;
        }
        private List<Fetch_Details_of_report_head_assesment> findSubjectHeadingsAssesment(string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            List<Fetch_Details_of_report_head_assesment> MySubjectHeadingAssesment = new List<Fetch_Details_of_report_head_assesment>();
            string query = "select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' and Istatus=1 order by Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MySubjectHeadingAssesment.Add(new Fetch_Details_of_report_head_assesment
                    {
                        Assessment_Name = dr["Assessment_Name"].ToString(),
                        Short_Name = dr["Short_Name"].ToString(),
                        Maximum_Marks = dr["Maximum_Marks"].ToString(),
                        Term_Maximum_Marks = dr["Term_Maximum_Marks"].ToString(),
                    });
                }
            }
            return MySubjectHeadingAssesment;
        }

        //================================= CoScholastic
        private List<Fetch_Details_of_report_CoScholastic> findCoScholasticData(string Session_id, string Admission_no, string Class_id, string Branch_id, string Term_id)
        {
            List<Fetch_Details_of_report_CoScholastic> MyCoScholasticData = new List<Fetch_Details_of_report_CoScholastic>();
            string query = "select DISTINCT t1.Assessment,t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_Code,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic' order by t2.Subject_position asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string marks = kesFM.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), dr["Subject"].ToString(), dr["Branch_id"].ToString(), "Scholastic");

                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                    string obt_marks = arrs[0];
                    string full_marks = arrs[1];

                    string marks_type = arrs[2];
                    string total_no = arrs[3];
                    string PassMarks = arrs[5];



                    MyCoScholasticData.Add(new Fetch_Details_of_report_CoScholastic
                    {
                        Subject_Name = dr["Subject_name"].ToString(),
                        Total_marks = obt_marks,
                        Full_marks = full_marks,
                        IsShow = "",
                    });
                }
            }
            else
            {
                MyCoScholasticData.Add(new Fetch_Details_of_report_CoScholastic
                {
                    Subject_Name = "",
                    Total_marks = "",
                    Full_marks = "",
                    IsShow = "hidden",
                });
            }
            return MyCoScholasticData;
        }


        //================================= DescplinES
        private List<Fetch_Details_of_report_descpline> findDescplineData(string Session_id, string Admission_no, string Class_id, string Branch_id, string Term_id)
        {
            List<Fetch_Details_of_report_descpline> MyDescplineData = new List<Fetch_Details_of_report_descpline>();
            string query = "select ptt.*,pt.Activity_name from Exam_Personality_Traits_Term_Wise ptt join Exam_Personality_Traits pt on ptt.Activity_Id=pt.Activity_Id where ptt.Session_id=" + Session_id + " and ptt.Branch_id='" + Branch_id + "' and ptt.Class_id=" + Class_id + " and ptt.Exam_Term_Id=" + Term_id + " and ptt.Admission_no='" + Admission_no + "'  order by pt.Position asc";

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
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
                    MyDescplineData.Add(new Fetch_Details_of_report_descpline
                    {
                        Activity_name = dr["Activity_name"].ToString(),
                        Term_grade = grade,
                    });
                }
            }
            return MyDescplineData;
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

        //================================= MarkRange
        private List<Fetch_Details_of_report_marks_range> findMarkRangeData(string Session_id, string Branch_id, string Term_id)
        {
            List<Fetch_Details_of_report_marks_range> MyMarkRangeData = new List<Fetch_Details_of_report_marks_range>();
            string query = "select gsrg.* from Exam_Grade_System_Range_Grade gsrg join Exam_Term_Details td on gsrg.Session_Id=td.Session_Id and gsrg.Branch_Id=td.Branch_Id and gsrg.Grade_System_Id=td.Grade_System_Id where td.Exam_Term_Id=" + Term_id + " and gsrg.Session_Id=" + Session_id + " and gsrg.Branch_Id='" + Branch_id + "' order by Lower_Range desc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MyMarkRangeData.Add(new Fetch_Details_of_report_marks_range
                    {
                        Lower_Range = dr["Lower_Range"].ToString(),
                        Upper_Range = dr["Upper_Range"].ToString(),
                        Grade = dr["Grade"].ToString(),
                        Remarks = dr["Remarks"].ToString(),
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
            }
            return MyMarkRangeData;
        }

        private List<Fetch_Details_of_report_marks_range_coscholestic> findMarkRangeData_coscholestic(string Session_id, string Branch_id, string Term_id, string Class_id)
        {
            List<Fetch_Details_of_report_marks_range_coscholestic> MyMarkRangeData_coscholestic = new List<Fetch_Details_of_report_marks_range_coscholestic>();

            string grading_id = My.get_single_column_data("select Grade_System_Id as Column_Name from Exam_Assessment_Details where Scholastic_Co_scholastic='Co-Scholastic' and Session_Id='" + Session_id + "' and Istatus=1 and Exam_Term_Id='" + Term_id + "' and Class_id='" + Class_id + "'");
            string query = "select gsrg.* from Exam_Grade_System_Range_Grade gsrg where gsrg.Session_Id='" + Session_id + "' and gsrg.Branch_Id='1' and gsrg.Grade_System_Id='" + grading_id + "' order by Lower_Range desc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
                MyMarkRangeData_coscholestic.Add(new Fetch_Details_of_report_marks_range_coscholestic
                {
                    IsCoScholasticGrade = "hidden",
                    Lower_Range = "",
                    Upper_Range = "",
                    Grade = "",
                    RowCount = "",
                }); ;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MyMarkRangeData_coscholestic.Add(new Fetch_Details_of_report_marks_range_coscholestic
                    {
                        IsCoScholasticGrade = "showd",
                        Lower_Range = dr["Lower_Range"].ToString(),
                        Upper_Range = dr["Upper_Range"].ToString(),
                        Grade = dr["Grade"].ToString(),
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
            }
            return MyMarkRangeData_coscholestic;
        }

        //================================= GradeRemarkK
        private List<Fetch_Details_of_report_grade_remark> findGradeRemarkData()
        {
            List<Fetch_Details_of_report_grade_remark> MyGradeRemarkData = new List<Fetch_Details_of_report_grade_remark>();
            string query = "select * from Exam_grade_master";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MyGradeRemarkData.Add(new Fetch_Details_of_report_grade_remark
                    {
                        Grade_name = dr["Grade_name"].ToString(),
                        Grade_remark = dr["Grade_remark"].ToString(),
                        RowCount = dt.Rows.Count.ToString(),
                    });
                }
            }
            return MyGradeRemarkData;
        }

        private List<MySubjectMarkShowII> findSubjectMarkShowII(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id, string firm_id)
        {
            List<MySubjectMarkShowII> MySubjectMarkShowItemII = new List<MySubjectMarkShowII>();
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Is_optional,t2.Subject_Code,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' order by t2.Subject_position asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MySubjectMarkDetailsIII> MBdetailsIII = findmyMarksIII(dr["Subject"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_id, dr["Section"].ToString(), dr["Is_optional"].ToString());
                    string Total_marks = kesFM.get_subject_total_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Term"].ToString(), dr["Subject"].ToString(), dr["Branch_id"].ToString(), "Scholastic");

                    string[] stringSeparatorss = new string[] { "月" };
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
                    string class_in_new_line = arrs[26];   // IsClassinNewLine

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

                    string WhatHeadShow = arrs[43];   // WhatHeadShow
                    string Is_aff_no_hide = arrs[44];   // WhatHeadShow
                    string Is_overall_grade_hide = arrs[45];   // WhatHeadShow
                    string subjFulmarks = arrs[49];   // Full Marks
                    string gradeBG = arrs[50];   // Grade BG 
                    string Gbgclass = "";
                    if (gradeBG.Length == 7)
                    {
                        gradeBG = "background:" + gradeBG;
                        Gbgclass = "gradeBGCls";
                    }
                    double termSubj_hlf_mark = My.toDouble(subjFulmarks) / 2;


                    string percent_remark = kesFM.get_percent_remark(overall_percent);
                    //QQQQQRRRR
                    string url = "";
                    string markType = "";
                    if (overall_obt_marks == "hidden")
                    {
                        markType = "GRADE";
                        url = "https://api.qrserver.com/v1/create-qr-code/?data=Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Overall Grade: " + overall_percent + "%" + "&amp;size=110x110";
                    }
                    else
                    {
                        markType = "PERCENTAGE";
                        url = "https://api.qrserver.com/v1/create-qr-code/?data=Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Total Obtain Mark: " + overall_obt_marks + "/" + overall_full_marks + ", Overall Percentage: " + overall_percent + "%" + "&amp;size=110x110";
                    }
                    string roll_show_new_line = "hidden";
                    if (ranks == "hidden")
                    {
                        roll_show_new_line = "showd";
                    }


                    string attendanceColor = "";
                    if (firm_id == "IPSP-01")
                    {
                        if (My.toDouble(percent_of_attandance) >= 75)
                        {
                            attendanceColor = "greenColorSA";
                        }
                        else
                        {
                            attendanceColor = "redColorSA";
                        }
                    }

                    string Subject_names = dr["Subject_name"].ToString();
                    if (dr["Is_optional"].ToString() == "True")
                    {
                        Subject_names = Subject_names + " (Optional)";
                    }

                    MySubjectMarkShowItemII.Add(new MySubjectMarkShowII
                    {
                        Subject = dr["Subject_Code"].ToString(),
                        Subject_name = Subject_names,
                        Total_marks = obt_subject_mark,

                        GradeBG = gradeBG,
                        Gbgclass = Gbgclass,
                        SubjFulmarks = subjFulmarks,
                        TermSubj_hlf_mark = termSubj_hlf_mark.ToString(),

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
                        SpecialNote = specialNote,
                        Remarkss = remarkss,
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
                        Roll_show_new_line = roll_show_new_line,
                        Co_sch_area_margn = co_sch_area_margn,
                        Overall_area_margn = overall_area_margn,
                        Percent_remark_area_margn = percent_remark_area_margn,
                        Graph_area_margn = graph_area_margn,
                        Ins1_area_margn = ins1_area_margn,
                        Ins2_area_margn = ins2_area_margn,
                        Toppers_area_margn = toppers_area_margn,
                        Is_watermark_show = is_watermark_show,
                        AttendanceColor = attendanceColor,

                        Is_std_img_hide = is_std_img_hide,
                        Is_std_section_hide = is_std_section_hide,
                        Aff_text = aff_text,
                        Father_name1 = father_name1,
                        Father_name2 = father_name2,
                        Is_subj_code_hide = is_subj_code_hide,
                        Is_text_center = is_text_center,
                        Is_website_show = Is_website_show,
                        MySubjectMarkItemIII = MBdetailsIII
                    });
                }
            }
            return MySubjectMarkShowItemII;
        }


        private List<MySubjectMarkDetailsIII> findmyMarksIII(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id, string Section, string Is_optional)
        {
            List<MySubjectMarkDetailsIII> MySubjectMarkItemIII = new List<MySubjectMarkDetailsIII>();
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Assessment_Name,t1.Sequence_No from Exam_Assessment_Details t1 where t1.Session_Id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Scholastic_Co_scholastic='Scholastic' and t1.Istatus=1 order by t1.Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string marks = kesFM.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic");

                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                    string obt_marks = arrs[0];
                    string full_marks = arrs[1];

                    string marks_type = arrs[2];
                    string total_no = arrs[3];
                    string PassMarks = arrs[5];

                    //if (Is_optional == "True") { }
                    //else
                    //{
                    save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marks, full_marks, marks_type, total_no);
                    //}
                    string subjHighestMark = get_highestMark(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Term"].ToString(), Subject_id, dr["Branch_id"].ToString(), Section);

                    try
                    {
                        bool tot = mycode.cheknumer_Double(obt_marks);
                        if (tot == false) { }
                        else
                        {
                            obt_marks = Math.Truncate(My.toDouble(obt_marks)).ToString();
                        }
                    }
                    catch
                    {
                    }

                    MySubjectMarkItemIII.Add(new MySubjectMarkDetailsIII  // Max Mark
                    {
                        Marks = full_marks,
                        Subject_Name = "",
                        Subject = Subject_id,
                        Total_marks = "0",
                        Full_marks = full_marks,
                    });

                    MySubjectMarkItemIII.Add(new MySubjectMarkDetailsIII   // Pass Mark
                    {
                        Marks = PassMarks,
                        Subject_Name = "",
                        Subject = Subject_id,
                        Total_marks = "0",
                        Full_marks = full_marks,
                    });
                    MySubjectMarkItemIII.Add(new MySubjectMarkDetailsIII // Highest Mark
                    {
                        Marks = subjHighestMark,
                        Subject_Name = "",
                        Subject = Subject_id,
                        Total_marks = "0",
                        Full_marks = full_marks,
                    });

                    MySubjectMarkItemIII.Add(new MySubjectMarkDetailsIII  // OBT Marks
                    {
                        Marks = obt_marks,
                        Subject_Name = "",
                        Subject = Subject_id,
                        Total_marks = "0",
                        Full_marks = full_marks,
                    });
                }
            }
            return MySubjectMarkItemIII;
        }

        private string get_highestMark(string Session_id, string Class_id, string Term_id, string Subject_id, string Branch_id, string Section)
        {
            string returN = "0";
            DataTable dt = My.dataTable("select Marks from Exam_highest_mark_of_subject_termwise where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Term_id='" + Term_id + "' and Subject_id='" + Subject_id + "' and Section='" + Section + "'");
            if (dt.Rows.Count > 0)
            {
                returN = dt.Rows[0]["Marks"].ToString();
            }
            return returN;
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

            string first_term = kesExamSetting.get_first_term(session_id, Branch_id, class_id);
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
    }
}
