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

namespace school_web.Examination_Admin.slip.nni.api
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
            public string Rank { get; set; }

            public List<Fetch_Details_of_Firm> MyFirmDetailData { get; set; }
            public List<Fetch_Details_of_report_head> MySubjectHeading { get; set; }
            public List<MySubjectMarkShow> MySubjectMarkShowItem { get; set; }
            public List<MyTotalsDetails> MyTotalsDetailsItem { get; set; }
            public List<MyCoScholesticDetails> MyCoScholesticDetailsItem { get; set; }
            public List<MyDecplineDetails> MyDecplineDetailsItem { get; set; }
            public List<MyMarkRangeDetails> MyMarkRangeDetailsItem { get; set; }
            public List<MySignatureDetails> MySignatureDetailsItem { get; set; }
            public List<Fetch_Details_of_Rank> MyRankDetailData { get; set; }
            public List<Fetch_Details_of_attendance> Fetch_Details_of_attendance_Item { get; set; }
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
            public string Aff_text1 { get; set; }
            public string SchoolCode { get; set; }
            public string ExtraLogo { get; set; }
            public string AffYear { get; set; }
        }

        public class Fetch_Details_of_report_head
        {
            public string Assessment_Name { get; set; }
        }

        public class MySubjectMarkShow
        {
            public string Subject { get; set; }
            public string Subject_name { get; set; }
            public string Total_mark_of_a_subject { get; set; }
            public string grade_of_a_subject { get; set; }
            public string If_is_mrk_hide { get; set; }
            public string If_is_grade_ttl_mark_hide { get; set; }
            public string ColspanFive { get; set; }
            public string Overall_ab_grade { get; set; }
            public string Overall_av_marks { get; set; }
            public string ColspanOneTwo { get; set; }

            public string Total_class { get; set; }
            public string Total_Present_class { get; set; }
            public string Total_att_percent { get; set; }

            public string Overall_obtain_mark { get; set; }
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
            public string ThemeColor { get; set; }
            public List<MySubjectMarkDetails> MySubjectMarkItem { get; set; }
        }

        public class MySubjectMarkDetails
        {
            public string Marks { get; set; }
        }
        public class MyTotalsDetails
        {
            public string Overall_obt_marks { get; set; }
            public string Overall_full_marks { get; set; }
            public string Overall_percents { get; set; }
            public string Mark_type { get; set; }
            public string P_remark { get; set; }
            public string Grade { get; set; }
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
        public class MyMarkRangeDetails
        {
            public string Lower_Range { get; set; }
            public string Upper_Range { get; set; }
            public string Grade { get; set; }
            public string RowCount { get; set; }
        }
        public class MySignatureDetails
        {
            public string Signature_name { get; set; }
            public string Signature_image { get; set; }
        }
        public class Fetch_Details_of_attendance
        {
            public string Exam_name { get; set; }
            public string Total_working_days { get; set; }
            public string Total_persent_days { get; set; }
            public string Attendance_percentage { get; set; }
            public string Attendance_class_style { get; set; }
        }

        public class Fetch_Details_of_Rank
        {
            public string Rank { get; set; }
            public string Student_name { get; set; }
            public string Total_obtained_mark { get; set; }
            public string Mark_percentage { get; set; }
            public string Grade { get; set; }
        }

        List<MyStudentsDetails> ShowMyStudents = new List<MyStudentsDetails>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_rp_card_bulks(string Session_id, string Class_id, string Section, string Branch_id, string Term_idI, string Term_idII)
        {
            string query = "select DISTINCT  em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname,ar.dob,ar.mothername,ar.fathername,ar.studentimagepath,CASE WHEN ar.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN ar.studentimagepath is not null THEN ar.studentimagepath END AS Student_img,isnull((select top 1 Rank from Exam_rank_master_final_year where Session_id=ar.Session_id and Class_id=ar.Class_id and Branch_id=ar.Branch_id and Admission_no=ar.admissionserialnumber),'0') as Rank from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + Session_id + " and em.Class_id=" + Class_id + " and ar.Section='" + Section + "' and em.Branch_id=" + Branch_id + "  and  em.Term=" + Term_idII + " order by ar.rollnumber asc";
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
                My.exeSql("delete from Exam_temp_mark_groupwise where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "';delete from Exam_temp_mark_assessment_groupwise where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "';delete from Exam_overall_no_for_rank where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Branch_id='" + Branch_id + "' and Term_id='Final'");
                foreach (DataRow dr in dt.Rows)
                {
                    //My.exeSql("delete from Exam_temp_mark_groupwise where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Admission_no='" + dr["Admission_no"].ToString() + "';delete from Exam_temp_mark_assessment_groupwise where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Admission_no='" + dr["Admission_no"].ToString() + "';delete from Exam_overall_no_for_rank where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Admission_no='" + dr["Admission_no"].ToString() + "' and Section='" + Section + "' and Branch_id='" + Branch_id + "' and Term_id='Final'");
                    List<Fetch_Details_of_Firm> MBFirmDetails = findFirmDetails(); 
                    List<Fetch_Details_of_report_head> MBdetailsHeading = findSubjectHeadings(Session_id, Class_id, Branch_id, Term_idI);
                    List<MySubjectMarkShow> MBdetails = findSubjectMarkShow(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, Section);
                    List<MyTotalsDetails> MBtotalsdetails = findTotalsDT(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII, Section);
                    List<MyCoScholesticDetails> MBCoScholesticdetails = findCoscholesticDT(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII);
                    List<MyDecplineDetails> MBDecplinedetails = findDescplineDT(dr["Admission_no"].ToString(), Session_id, Class_id, Branch_id, Term_idI, Term_idII);
                    List<MyMarkRangeDetails> MBMarkRangedetails = findMarkRangeDT(Session_id, Branch_id, Term_idI);
                    List<MySignatureDetails> MBSigndetails = findSignatureDT(Session_id, Branch_id, Section, Class_id);

                    List<Fetch_Details_of_attendance> MBAttenddetails = findAttendanceDetails(Session_id, Branch_id, Section, Class_id, dr["Admission_no"].ToString());
                    //================ Ranks
                    List<Fetch_Details_of_Rank> MBRank = findRankDetails(Session_id, dr["Admission_no"].ToString(), Class_id, Branch_id, dr["Section"].ToString());

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
                        MySubjectMarkShowItem = MBdetails,
                        MyTotalsDetailsItem = MBtotalsdetails,
                        MyCoScholesticDetailsItem = MBCoScholesticdetails,
                        MyDecplineDetailsItem = MBDecplinedetails,
                        MyMarkRangeDetailsItem = MBMarkRangedetails,
                        MySignatureDetailsItem = MBSigndetails,
                        MyRankDetailData = MBRank,
                        Fetch_Details_of_attendance_Item = MBAttenddetails,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(ShowMyStudents));
            }
        }


        private List<MySubjectMarkShow> findSubjectMarkShow(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Section)
        {
            List<MySubjectMarkShow> MySubjectMarkShowItem = new List<MySubjectMarkShow>();
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject as Subject_id,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_Code,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section,(select top 1 Session from session_details where session_id='" + Session_id + "') as Session_name  from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_idI + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' and t1.Subject in (select Sub_id from Subject_Mapping_New where Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Session_id='" + Session_id + "') order by t2.Subject_position asc";
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
                string settings_for_final_year = final_rp.get_final_year_setting(Session_id, Class_id, Branch_id);
                foreach (DataRow dr in dt.Rows)
                {
                    List<MySubjectMarkDetails> MBdetails = findmyBookingProduct(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idI, Term_idII, Section);
                    string ttl_mark_of_a_subject = final_rp.get_ttl_mark_of_a_subject_final_year(dr["Subject_id"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_idI, Term_idII);

                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = ttl_mark_of_a_subject.Split(stringSeparatorss, StringSplitOptions.None);
                    string Total_mark_of_a_subject = arrs[0];
                    string grade_of_a_subject = arrs[1];

                    string Total_class_of_term_I = arrs[2];
                    string Total_Present_class_of_term_I = arrs[3];
                    string Total_Precent_class_of_term_I = arrs[4];

                    string Total_class_of_term_II = arrs[5];
                    string Total_Present_class_of_term_II = arrs[6];
                    string Total_Precent_class_of_term_II = arrs[7];
                    string Overall_final_mark = arrs[8];
                    string Overall_final_percent = arrs[9];
                    string Overall_final_grade = arrs[10];
                    string remarkss = arrs[11];

                    double ttl_no_of_class = My.toDouble(Total_class_of_term_I) + My.toDouble(Total_class_of_term_II);
                    double ttl_no_of_present_class = My.toDouble(Total_Present_class_of_term_I) + My.toDouble(Total_Present_class_of_term_II);
                    double ttl_att_percent = (ttl_no_of_present_class / ttl_no_of_class) * 100;

                    //===========================

                    string if_is_mrk_hide = "";
                    string if_is_grade_ttl_mark_hide = "";
                    string colspanfiveandsix = "";

                    string session_name = dr["Session_name"].ToString();
                    string[] stringSeparatorssx = new string[] { "-" };
                    string[] arrsx = session_name.Split(stringSeparatorssx, StringSplitOptions.None);
                    string next_session_year = arrsx[1];

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
                    string Total_marks = final_rp.get_overall_mrk(Session_id, Class_id, Admission_no, Branch_id, Term_idI);
                    string[] stringSeparatorssxx = new string[] { "/" };
                    string[] arrsxx = Total_marks.Split(stringSeparatorssxx, StringSplitOptions.None);
                    string overall_obt_marks = arrsxx[0];
                    string overall_percent = arrsxx[1];
                    string overall_full_marks = arrsxx[2];
                    //string overall_grade = arrsxx[3];

                    string qr_txt = "Adm. No. :" + Admission_no + ", Name : " + dr["Student_name"].ToString() + ",  Class: " + dr["Class_name"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["Roll_number"].ToString() + ", Total Obtained Mark: " + overall_obt_marks + "/" + overall_full_marks + ", Overall Percentage: " + overall_percent;
                    string url = "https://quickchart.io/qr?text=" + qr_txt + "&dark=000&light=ffffff&ecLevel=Q&format=svg";
                    if (remarkss == "")
                    {
                        remarkss = "hidden";
                    }



                    MySubjectMarkShowItem.Add(new MySubjectMarkShow
                    {
                        Subject = dr["Subject_id"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        Total_mark_of_a_subject = Total_mark_of_a_subject,
                        grade_of_a_subject = grade_of_a_subject,


                        If_is_mrk_hide = if_is_mrk_hide,
                        If_is_grade_ttl_mark_hide = if_is_grade_ttl_mark_hide,
                        ColspanFive = colspanfiveandsix,

                        Total_class = ttl_no_of_class.ToString(),
                        Total_Present_class = ttl_no_of_present_class.ToString(),
                        Total_att_percent = ttl_att_percent.ToString("0.0"),

                        Overall_obtain_mark = Overall_final_mark,
                        Overall_final_percent = Overall_final_percent + " %",
                        Overall_final_grade = Overall_final_grade,
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
                        Subj_highest_mark = "",
                        ThemeColor = themeColor,
                        MySubjectMarkItem = MBdetails
                    });
                }
            }
            return MySubjectMarkShowItem;
        }



        private List<MySubjectMarkDetails> findmyBookingProduct(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id, string Term_idII, string Section)
        {
            List<MySubjectMarkDetails> MySubjectMarkItem = new List<MySubjectMarkDetails>();

            double Ttl_A = 0;
            double Ttl_B = 0;
            double Ttl_C = 0;
            DataTable dtx = My.dataTable("select * from Exam_assessment_group_for_final order by Position asc");
            for (int i = 0; i < dtx.Rows.Count; i++)
            {
                string query = "select t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Assessment_Name,t1.Sequence_No from Exam_Assessment_Details t1 where t1.Session_Id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and (t1.Exam_Term_Id=" + Term_id + " or t1.Exam_Term_Id=" + Term_idII + ") and t1.Group_id=" + dtx.Rows[i]["Assessment_group_id"].ToString() + " and t1.Scholastic_Co_scholastic='Scholastic' order by t1.Sequence_No asc";
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
                    int a = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string marks = Examination.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic");

                        string[] stringSeparatorss = new string[] { "/" };
                        string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                        string obt_marks = arrs[0];
                        string full_marks = arrs[1];

                        string marks_type = arrs[2];
                        string total_no = arrs[3];


                        //====================Parodic Test
                        if (dtx.Rows[i]["Assessment_group_id"].ToString() == "1")
                        {
                            MySubjectMarkItem.Add(new MySubjectMarkDetails
                            {
                                Marks = obt_marks,
                            });

                            save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marks, full_marks, marks_type, total_no, dtx.Rows[i]["Assessment_group_id"].ToString(), Section);


                            if (rowcount1 == a)
                            {
                                string ttl_average_mark = get_average_mark(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), dtx.Rows[i]["Assessment_group_id"].ToString(), "2", Section);
                                Ttl_A = My.toDouble(ttl_average_mark);
                                MySubjectMarkItem.Add(new MySubjectMarkDetails
                                {
                                    Marks = ttl_average_mark,
                                });
                            }
                        }

                        //==================SUB Enrichment
                        if (dtx.Rows[i]["Assessment_group_id"].ToString() == "2")
                        {
                            save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marks, full_marks, marks_type, total_no, dtx.Rows[i]["Assessment_group_id"].ToString(), Section);


                            if (rowcount1 == a)
                            {
                                string ttl_average_mark = get_average_mark(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), dtx.Rows[i]["Assessment_group_id"].ToString(), "0", Section);
                                Ttl_B = My.toDouble(ttl_average_mark);
                                MySubjectMarkItem.Add(new MySubjectMarkDetails
                                {
                                    Marks = ttl_average_mark,
                                });


                                MySubjectMarkItem.Add(new MySubjectMarkDetails
                                {
                                    Marks = (Ttl_A + Ttl_B).ToString(),
                                });
                            }
                        }

                        //==================TERM HY
                        if (dtx.Rows[i]["Assessment_group_id"].ToString() == "3")
                        {
                            save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marks, full_marks, marks_type, total_no, dtx.Rows[i]["Assessment_group_id"].ToString(), Section);

                            MySubjectMarkItem.Add(new MySubjectMarkDetails
                            {
                                Marks = obt_marks,
                            });

                            save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marks, full_marks, marks_type, total_no, dtx.Rows[i]["Assessment_group_id"].ToString(), Section);

                            if (rowcount1 == a)
                            {
                                string ttl_average_mark = get_average_mark(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), dtx.Rows[i]["Assessment_group_id"].ToString(), "0", Section);
                                Ttl_C = My.toDouble(ttl_average_mark);
                                MySubjectMarkItem.Add(new MySubjectMarkDetails
                                {
                                    Marks = ttl_average_mark,
                                });
                            }
                        }

                        a++;
                    }
                }
            }
            return MySubjectMarkItem;
        }

        private string get_average_mark(string session_id, string class_id, string admission_no, string term_id, string assesment_id, string subject_id, string Branch_id, string ass_group_id, string top_best, string Section)
        {
            double average_mark = 0;
            string qry = "";
            if (top_best == "0")
            {
                qry = "select * from Exam_temp_mark_assessment_groupwise where Session_id=" + session_id + " and Branch_id=" + Branch_id + " and Class_id=" + class_id + " and Subject_id=" + subject_id + " and Admission_no='" + admission_no + "' and Assessment_group_id=" + ass_group_id + "  order by Marks desc";
            }
            else
            {
                qry = "select top " + top_best + " * from Exam_temp_mark_assessment_groupwise where Session_id=" + session_id + " and Branch_id=" + Branch_id + " and Class_id=" + class_id + " and Subject_id=" + subject_id + " and Admission_no='" + admission_no + "' and Assessment_group_id=" + ass_group_id + "  order by Marks desc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count > 0)
            {
                double ttl_marks = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ttl_marks = ttl_marks + My.toDouble(dr["Marks"].ToString());
                }



                average_mark = ttl_marks / 2;
                average_mark = Math.Ceiling(average_mark);

                string group_full_mark = "0";
                if (ass_group_id == "3")
                {
                    group_full_mark = "80";
                }
                else
                {
                    group_full_mark = "10";
                }
                save_average_mark_group_wise(session_id, class_id, admission_no, subject_id, Branch_id, ass_group_id, average_mark, group_full_mark, Section);
            }

            return average_mark.ToString();
        }

        private void save_average_mark_group_wise(string session_id, string class_id, string admission_no, string subject_id, string Branch_id, string ass_group_id, double average_mark, string group_full_mark, string Section)
        {
            if (mycode.IsUserExist("select Id from Exam_temp_mark_groupwise where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "' and Assessment_group_id='" + ass_group_id + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Exam_temp_mark_groupwise (Session_id,Branch_id,Class_id,Subject_id,Admission_no,Marks,Assessment_group_id,Full_mark,Section) values (@Session_id,@Branch_id,@Class_id,@Subject_id,@Admission_no,@Marks,@Assessment_group_id,@Full_mark,@Section)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Subject_id", subject_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd.Parameters.AddWithValue("@Marks", average_mark);
                cmd.Parameters.AddWithValue("@Assessment_group_id", ass_group_id);
                cmd.Parameters.AddWithValue("@Full_mark", group_full_mark);
                cmd.Parameters.AddWithValue("@Section", Section);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Exam_temp_mark_groupwise set Marks=@Marks,Full_mark=@Full_mark where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "' and Assessment_group_id='" + ass_group_id + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Marks", average_mark);
                cmd.Parameters.AddWithValue("@Full_mark", group_full_mark);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        //=================================HEADING
        private List<Fetch_Details_of_report_head> findSubjectHeadings(string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            List<Fetch_Details_of_report_head> MySubjectHeading = new List<Fetch_Details_of_report_head>();
            DataTable dtx = My.dataTable("select * from Exam_assessment_group_for_final order by Position asc");
            for (int i = 0; i < dtx.Rows.Count; i++)
            {
                string query = "select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Scholastic_Co_scholastic='Scholastic' and Group_id=" + dtx.Rows[i]["Assessment_group_id"].ToString() + " order by Sequence_No asc";
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
                    int a = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dtx.Rows[i]["Assessment_group_id"].ToString() == "1")
                        {
                            MySubjectHeading.Add(new Fetch_Details_of_report_head
                            {
                                Assessment_Name = dtx.Rows[i]["Assesment_name"].ToString(),
                            });

                            if (rowcount1 == a)
                            {
                                string Assessment_Name = dtx.Rows[i]["Assessment_final"].ToString();
                                MySubjectHeading.Add(new Fetch_Details_of_report_head
                                {
                                    Assessment_Name = Assessment_Name,
                                });
                            }
                        }

                        if (dtx.Rows[i]["Assessment_group_id"].ToString() == "2")
                        {
                            if (a == 1)
                            {
                                MySubjectHeading.Add(new Fetch_Details_of_report_head
                                {
                                    Assessment_Name = dtx.Rows[i]["Assesment_name"].ToString(),
                                });


                                string Assessment_Name = dtx.Rows[i]["Assessment_final"].ToString();
                                MySubjectHeading.Add(new Fetch_Details_of_report_head
                                {
                                    Assessment_Name = Assessment_Name,
                                });
                            }
                        }

                        if (dtx.Rows[i]["Assessment_group_id"].ToString() == "3")
                        {
                            string Assessment_Name = "";
                            if (a == 1)
                            {
                                Assessment_Name = "Term-I Exam (80)";
                            }
                            if (a == 2)
                            {
                                Assessment_Name = "Term-II Exam (80)";
                            }
                            MySubjectHeading.Add(new Fetch_Details_of_report_head
                            {
                                Assessment_Name = Assessment_Name,
                            });

                            if (rowcount1 == a)
                            {
                                MySubjectHeading.Add(new Fetch_Details_of_report_head
                                {
                                    Assessment_Name = dtx.Rows[i]["Assessment_final"].ToString(),
                                });
                            }
                        }
                        a++;
                    }
                }
            }

            return MySubjectHeading;
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
                        AffYear = "Aff. : " + dt.Rows[0]["Aff_year"].ToString()
                    });
                }
            }
            return MyFirmDetailData;
        }



        private void save_marks(string session_id, string class_id, string admission_no, string term_id, string assesment_id, string subject_id, string Branch_id, string marks, string full_marks, string marks_type, string total_no, string ass_group_id, string Section)
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


            if (mycode.IsUserExist("select Id from Exam_temp_mark_assessment_groupwise where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'  and Assessment_group_id='" + ass_group_id + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Exam_temp_mark_assessment_groupwise (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Assessment_group_id,Section) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Assessment_group_id,@Section)";
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
                cmd.Parameters.AddWithValue("@Assessment_group_id", ass_group_id);
                cmd.Parameters.AddWithValue("@Section", Section);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Exam_temp_mark_assessment_groupwise set Marks=@Marks,Full_marks=@Full_marks where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'  and Assessment_group_id='" + ass_group_id + "'";
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



        //=================================findTotalsDT
        private List<MyTotalsDetails> findTotalsDT(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_idI, string Term_idII, string Section)
        {
            List<MyTotalsDetails> MyTotalsDetailsItem = new List<MyTotalsDetails>();
            string Total_marks = final_rp.get_overall_mrk(Session_id, Class_id, Admission_no, Branch_id, Term_idI);
            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);

            string overall_obt_marks = arrs[0];
            string overall_percent = arrs[1];
            string overall_full_marks = arrs[2];
            string overall_grade = arrs[3];

            save_overall_mark(Admission_no, Session_id, Class_id, Branch_id, "Final", overall_full_marks, overall_obt_marks, overall_percent, Section);

            string percent_remark = Examination.get_percent_remark(overall_percent);
            MyTotalsDetailsItem.Add(new MyTotalsDetails
            {
                Overall_obt_marks = overall_obt_marks,
                Overall_full_marks = overall_full_marks,
                Overall_percents = My.toDouble(overall_percent).ToString("0.00"),
                P_remark = percent_remark,
                Grade = overall_grade,
            });
            return MyTotalsDetailsItem;
        }

        private void save_overall_mark(string admission_no, string session_id, string class_id, string branch_id, string type, string overall_full_marks, string overall_obt_marks, string overall_percent, string Section)
        {
            //delete from Exam_overall_no_for_rank where Session_id='' and Class_id='' and Admission_no='' and Section='' and Branch_id='' and Term_id=''
            SqlCommand cmd;
            string query = "INSERT INTO Exam_overall_no_for_rank (Session_id,Class_id,Admission_no,Branch_id,Term_id,Full_mark,Obtained_mark,Persentage,Created_date,Created_idate,Section) values (@Session_id,@Class_id,@Admission_no,@Branch_id,@Term_id,@Full_mark,@Obtained_mark,@Persentage,@Created_date,@Created_idate,@Section)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", session_id);
            cmd.Parameters.AddWithValue("@Class_id", class_id);
            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
            cmd.Parameters.AddWithValue("@Section", Section);
            cmd.Parameters.AddWithValue("@Branch_id", branch_id);
            cmd.Parameters.AddWithValue("@Term_id", type);
            cmd.Parameters.AddWithValue("@Full_mark", overall_full_marks);
            cmd.Parameters.AddWithValue("@Obtained_mark", overall_obt_marks);
            cmd.Parameters.AddWithValue("@Persentage", My.toDouble(overall_percent).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            if (My.InsertUpdateData(cmd))
            {
            }
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
                    string grade_t1 = "0";
                    string grade_t2 = "0";
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


                            if (i == 0)
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

        //=================================MyMarkRangeDetails
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



        //=================================MyMarkRangeDetails
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

        private List<Fetch_Details_of_Rank> findRankDetails(string Session_id, string admission_no, string Class_id, string Branch_id, string Section)
        {
            List<Fetch_Details_of_Rank> MyRankDetailData = new List<Fetch_Details_of_Rank>();
            string query = "select top 3 t2.class,t2.rollnumber,t2.session,t2.studentname,t1.*  from Exam_rank_master_final_year t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Section='" + Section + "' order by Rank asc";
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


        private List<Fetch_Details_of_attendance> findAttendanceDetails(string Session_id, string Branch_id, string Section, string Class_id, string Admission_no)
        {
            List<Fetch_Details_of_attendance> Fetch_Details_of_attendance_Item = new List<Fetch_Details_of_attendance>();
            string query = "select * from Exam_Exam_Wise_Attendance where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_Exam_Wise_Attendance");
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
                    Fetch_Details_of_attendance_Item.Add(new Fetch_Details_of_attendance
                    {
                        Exam_name = dr["Exam_type"].ToString(),
                        Total_working_days = dr["Total_no_of_class"].ToString(),
                        Total_persent_days = dr["No_of_class_Attendance"].ToString(),
                        Attendance_percentage = att_persent.ToString("0.00"),
                        Attendance_class_style = "",
                    });
                }

                double att_persent_ttl = (My.toDouble(ttl_attendance_class) / My.toDouble(ttl_class)) * 100;
                Fetch_Details_of_attendance_Item.Add(new Fetch_Details_of_attendance
                {
                    Exam_name = "TOTAL",
                    Total_working_days = ttl_class.ToString(),
                    Total_persent_days = ttl_attendance_class.ToString(),
                    Attendance_percentage = att_persent_ttl.ToString("0.00"),
                    Attendance_class_style = "trdefferents",
                });
            }

            return Fetch_Details_of_attendance_Item;
        }

    }
}
