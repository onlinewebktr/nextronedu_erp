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

namespace school_web.Examination_Admin.slip.webService
{
    /// <summary>
    /// Summary description for admit_card
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class admit_card : System.Web.Services.WebService
    {

        #region ONESHIFTS
        public class MyStudentShow
        {
            public string Student_name { get; set; }
            public string Subject_DOB { get; set; }
            public string Father_name { get; set; }
            public string Student_mob_no { get; set; }
            public string Registration_id { get; set; }
            public string Course_Name { get; set; }
            public string Class_Name { get; set; }
            public string Section_Name { get; set; }

            public string Session_name { get; set; }
            public string Student_img { get; set; }
            public string Std_dummy_imgs { get; set; }
            public string Remarks { get; set; }
            public string Mother_name { get; set; }
            public string Mother_mobile { get; set; }
            public string Roll_no { get; set; }
            public string House_name { get; set; }
            public string Father_mob { get; set; }
            public string Term_name { get; set; }
            public string Exam_name { get; set; }
            public string duesclassdesabled { get; set; }
            public string duesclassdesabled2 { get; set; }
            public string Desabletext { get; set; }
            public string PrintBTN { get; set; }

            public string Admitcard_no { get; set; }
            public string IsadmitcardNoShow { get; set; }
            public string WhatDiv { get; set; }
            public string WhatDivTB { get; set; }
            public string OneAdmitCardHeight { get; set; }
            public string stdInfoWdth { get; set; }
            public string stdInfoWdthLft { get; set; }
            public string stdInfoWdthRght { get; set; }
            public string MaiNpageWidth { get; set; }
            public string OneAdmitCardWdth { get; set; }
            public string Dues_message_show { get; set; }
            public string Dues_month_name { get; set; }
            public string DuesshowBottom { get; set; }
            public string Dues_amt { get; set; }
            public string Dues_year { get; set; }
            public string Class_teacher_sig { get; set; }
            public string Principal_sig { get; set; }
            public string Examinee_sig { get; set; }

            public string SignAuto { get; set; }
            public string SignWirhSetting { get; set; }
            public string LftSign { get; set; }
            public string LftSignName { get; set; }
            public string MdlSign { get; set; }
            public string MdlSignName { get; set; }
            public string RghtSign { get; set; }
            public string RghtSignName { get; set; }

            public List<MySchoolDetails> MySchoolDetailsItem { get; set; }
            public List<MyRoutineDetails> MyRoutineDetailsItem { get; set; }

        }

        public class MyRoutineDetails
        {
            public string Subject_name { get; set; }
            public string Day { get; set; }
            public string Exam_date { get; set; }
            public string Exam_time { get; set; }
        }
        public class MySchoolDetails
        {
            public string School_name { get; set; }
            public string Affiliation_no { get; set; }
            public string Address { get; set; }
            public string Mobile_no_email { get; set; }
            public string Programe { get; set; }
            public string PagesizE { get; set; }
            public string Session { get; set; }
            public string session_name { get; set; }
            public string LogoSchool { get; set; }
            public string Class_names { get; set; }
            public string UnderShow { get; set; }
            public string Header_templete { get; set; }
            public string Content_header { get; set; }
            public string isMiddl_sign_hide { get; set; }
            public string First_sign_text { get; set; }
        }

        List<MyStudentShow> EMySubMark = new List<MyStudentShow>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_admit_cards(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id, string Section, string Session_name, string Exam_id, string CheckedStd, string UserType, string PageNo)
        {
            string desabletext = "";
            try
            {
                desabletext = Exam_setting.get_dues_message_for_admit_card(Session_id, Class_id, Term_id, Exam_id);
            }
            catch
            {
                desabletext = " Contact to school";
            }


            string maiNpageWidth = "mainPagewidth-2x2"; string OneAdmitCardWdth = "width-sgle-page-2x2"; string OneAdmitCardHeight = "height-sgle-page-2x2";
            string stdInfoWdth = "studentinfOwdth-2x2"; string stdInfoWdthLft = "studentinfOwdthlft-2x2"; string stdInfoWdthRght = "studentinfOwdthRght-2x2";
            if (PageNo == "2")
            {
                maiNpageWidth = "mainPagewidth-1x2";
                OneAdmitCardWdth = "width-sgle-page-1x2";
                OneAdmitCardHeight = "height-sgle-page-1x2";
                stdInfoWdth = "studentinfOwdth-1x2";
                stdInfoWdthLft = "studentinfOwdthlft-1x2";
                stdInfoWdthRght = "studentinfOwdthRght-1x2";
            }

            string term_name = get_term_name(Session_id, Class_id, Term_id);
            string exam_name = get_exam_name(Session_id, Class_id, Term_id, Exam_id);
            string query = "";
            if (Admission_no == "0")
            {
                query = "Select ar.studentimagepath AS Student_img,ar.class,ar.Class_id,ar.studentname,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.dob,ar.mothername,ar.fathername,ar.father_mob,ar.mother_mob,ar.mobilenumber as selfmobileno,(Select top 1 house_name from house_master where house_id=ar.house) as house_name from admission_registor ar where ar.Status='1' and ar.Class_Id=" + Class_id + " and ar.Section='" + Section + "' and ar.Session_Id=" + Session_id + " order by ar.rollnumber";
            }
            else if (CheckedStd == "1")
            {
                query = "Select ar.studentimagepath AS Student_img,ar.class,ar.Class_id,ar.studentname,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.dob,ar.mothername,ar.fathername,ar.father_mob,ar.mother_mob,ar.mobilenumber as selfmobileno,(Select top 1 house_name from house_master where house_id=ar.house) as house_name from admission_registor ar where ar.Status='1' and ar.Class_Id=" + Class_id + " and ar.Section='" + Section + "' and ar.Session_Id=" + Session_id + "  and ar.Id in(" + Admission_no + ") order by ar.rollnumber";
            }
            else
            {
                query = "Select ar.studentimagepath AS Student_img,ar.class,ar.Class_id,ar.studentname,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.dob,ar.mothername,ar.fathername,ar.father_mob,ar.mother_mob,ar.mobilenumber as selfmobileno,(Select top 1 house_name from house_master where house_id=ar.house) as house_name from admission_registor ar where ar.Status='1' and ar.Class_Id=" + Class_id + " and ar.Section='" + Section + "' and ar.Session_Id=" + Session_id + " and ar.admissionserialnumber='" + Admission_no + "' order by ar.rollnumber";
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
                DataTable dtf = My.dataTable("select firm_id,Is_fine_repeat,Is_dues_admit_only_for_student from Firm_Details");
                string class_teacher_sig = "";
                string principal_sig = "";
                string examinee_sig = "";
                string duesshowBottom = "hidden";
                string signWirhSetting = "hidden"; string signAuto = "hidden";
                string lftSign = "hidden"; string lftSignName = "hidden";
                string mdlSign = "hidden"; string mdlSignName = "hidden";
                string rghtSign = "hidden"; string rghtSignName = "hidden";
                DataTable dtSign = My.dataTable("select top 3 * from Admit_card_sign_setting where Status='1' order by Position asc");
                if (dtSign.Rows.Count > 0)
                {
                    signAuto = "hidden";
                    signWirhSetting = "showd";
                    foreach (DataRow drSgn in dtSign.Rows)
                    {
                        if (drSgn["Position"].ToString() == "1")
                        {
                            lftSign = drSgn["Sign_path"].ToString();
                            lftSignName = drSgn["Sign_name"].ToString();
                        }
                        if (drSgn["Position"].ToString() == "2")
                        {
                            mdlSign = drSgn["Sign_path"].ToString();
                            mdlSignName = drSgn["Sign_name"].ToString();
                        }
                        if (drSgn["Position"].ToString() == "3")
                        {
                            rghtSign = drSgn["Sign_path"].ToString();
                            rghtSignName = drSgn["Sign_name"].ToString();
                        }
                    }
                }
                else
                {
                    signAuto = "showd";
                    signWirhSetting = "hidden";
                    string signatures = Examination.get_signature_admit_card(Session_id, Class_id, Section, Branch_id);
                    string[] stringSeparatorssign = new string[] { ">" };
                    string[] arrsign = signatures.Split(stringSeparatorssign, StringSplitOptions.None);
                    class_teacher_sig = arrsign[0];
                    principal_sig = arrsign[1];
                    examinee_sig = arrsign[2];

                    if (dtf.Rows[0]["firm_id"].ToString() == "MDA-01" || dtf.Rows[0]["firm_id"].ToString() == "MDA-02")
                    {
                        class_teacher_sig = get_sign("OFFICE IN CHARGE");
                    }
                    if (dtf.Rows[0]["firm_id"].ToString() == "TDA-001")
                    {
                        examinee_sig = get_sign("Accountant");
                    }

                }



                string isFineRepeate = "No";
                if (dtf.Rows[0]["Is_fine_repeat"].ToString() == "True")
                {
                    isFineRepeate = "Yes";
                }

                string isduesShowOnlyStd = "No";
                if (dtf.Rows[0]["Is_dues_admit_only_for_student"].ToString() == "True")
                {
                    isduesShowOnlyStd = "Yes";
                }

                string isDues_show = Exam_setting.is_calculate_dues_for_admit_card(Session_id, Class_id, Term_id, Exam_id);
                string[] stringSeparators = new string[] { "/" };
                string[] arr = isDues_show.Split(stringSeparators, StringSplitOptions.None);
                isDues_show = arr[0];
                string dues_month_id = arr[1];
                string dues_month_name = arr[2];
                string dues_cal_type = arr[3];
                string month_position = arr[4];
                int s_year = 0;
                if (isDues_show == "YES")
                {
                    string cunrt_session = Session_name;
                    string[] stringSeparatorsss = new string[] { "-" };
                    string[] arrss = cunrt_session.Split(stringSeparatorsss, StringSplitOptions.None);
                    string session_frst_year = arrss[0];
                    string session_last_year = arrss[1];
                    s_year = My.toint(session_frst_year);
                    s_year = My.check_start_months(My.toIntS(dues_month_id), s_year);
                }
                bool loops = false; int topM = 1; bool loopsTB = false;
                foreach (DataRow dr in dt.Rows)
                {
                    string dues_message_show = "hidden"; string dues_amt = "";
                    if (isduesShowOnlyStd == "No")
                    {
                        if (isDues_show == "YES")
                        {
                            dues_message_show = "showd";
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();
                            dues_update_headwise_transaction.update_student_dues(Session_id, Class_id, dr["admissionserialnumber"].ToString(), dtf.Rows[0]["firm_id"].ToString(), isFineRepeate, con);
                            con.Close();
                            dues_amt = Exam_setting.get_dues_amt(Session_id, Class_id, dr["admissionserialnumber"].ToString(), dues_month_id, dues_month_name, dues_cal_type, month_position);
                        }
                    }

                    List<MySchoolDetails> MBFirmdetails = findmyFirmDetails(dr["class"].ToString(), Session_name);
                    List<MyRoutineDetails> MBdetails = findmyRoutine(Session_id, Class_id, dr["admissionserialnumber"].ToString(), Branch_id, Term_id, Section, Session_name, Exam_id);

                    string std_imgs = dr["Student_img"].ToString(); string std_dummy_imgs = "hidden";
                    if (std_imgs == "")
                    {
                        std_imgs = "hidden";
                        std_dummy_imgs = "show";
                    }
                    string motherMob = dr["mother_mob"].ToString();
                    if (motherMob == "")
                    {
                        motherMob = "hidden";
                    }

                    string check_is_dues = "NO";
                    if (isduesShowOnlyStd == "No")
                    { 
                        check_is_dues = Exam_setting.check_dues_for_admit_card(Session_id, Class_id, dr["admissionserialnumber"].ToString(), Term_id, Exam_id);
                        if (UserType != "Admin")
                        { }
                    }


                    //========================
                    string isDues = "hidden"; string isDues2 = "hidden"; string printBTN = "";
                    if (isDues_show == "NO")
                    {
                        if (check_is_dues == "YES")
                        {
                            isDues = "desabledRP";
                            isDues2 = "duesclassdesabled2";


                            if (UserType != "Admin")
                            {
                                printBTN = "hidden";
                            }
                        }
                    }


                    string admitcard_no = ""; string isadmitcardNoShow = "hidden";
                    if (dtf.Rows[0]["firm_id"].ToString() == "EPIC-1")
                    {
                        string i_dob = My.dob_to_i_dob(dr["dob"].ToString());
                        string admNos = dr["admissionserialnumber"].ToString().Replace(@"/", string.Empty);
                        string admNos2 = admNos.Replace(@"-", string.Empty);
                        admitcard_no = i_dob + admNos2;

                        admitcard_no = admitcard_no.Substring(admitcard_no.Length - 8);
                        isadmitcardNoShow = "showd";
                    }

                    string whatDiv = ""; string whatDivTB = "";
                    if (PageNo == "4")   ///==============4PAGE
                    {
                        if (loops == false)
                        {
                            whatDiv = "lftdb";
                            loops = true;
                        }
                        else
                        {
                            loops = false;
                            whatDiv = "rghtdb";
                        }


                        if (loopsTB == false)
                        {
                            if (topM == 1)
                            {
                                topM++;
                                whatDivTB = "topdb";
                            }
                            else
                            {
                                loopsTB = true;
                                topM = 1;
                                whatDivTB = "topdb";
                            }
                        }
                        else
                        {
                            if (topM == 1)
                            {
                                topM++;
                                whatDivTB = "bottomdb";
                            }
                            else
                            {
                                loopsTB = false;
                                topM = 1;
                                whatDivTB = "bottomdb";
                            }
                        }
                    }
                    else //////============2PAGE
                    {
                        if (loops == false)
                        {
                            whatDiv = "lftdb1x2";
                            loops = true;
                        }
                        else
                        {
                            loops = false;
                            whatDiv = "rghtdb1x2";
                        }
                        whatDivTB = "topBtmdb1x2";

                    }
                    if (dtf.Rows[0]["firm_id"].ToString() == "MF-01")
                    {
                        dues_message_show = "hidden";
                        duesshowBottom = "showd";
                    }
                    if (lftSign == "")
                    {
                        lftSign = "hidden";
                    }
                    if (mdlSign == "")
                    {
                        mdlSign = "hidden";
                    }
                    if (rghtSign == "")
                    {
                        rghtSign = "hidden";
                    }


                    EMySubMark.Add(new MyStudentShow
                    {
                        Student_name = dr["studentname"].ToString(),
                        Subject_DOB = dr["dob"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Student_mob_no = dr["selfmobileno"].ToString(),
                        Registration_id = dr["admissionserialnumber"].ToString(),
                        Course_Name = dr["class"].ToString() + " / " + dr["Section"].ToString(),
                        Class_Name = dr["class"].ToString(),
                        Section_Name = dr["Section"].ToString(),
                        Session_name = dr["session"].ToString(),
                        Student_img = std_imgs,
                        Std_dummy_imgs = std_dummy_imgs,
                        Mother_name = dr["mothername"].ToString(),
                        Mother_mobile = motherMob,
                        Roll_no = dr["rollnumber"].ToString(),
                        House_name = dr["house_name"].ToString(),
                        Father_mob = dr["father_mob"].ToString(),
                        Term_name = term_name,
                        Exam_name = exam_name,
                        Dues_message_show = dues_message_show,
                        Dues_month_name = dues_month_name,
                        DuesshowBottom = duesshowBottom,
                        Dues_amt = dues_amt,
                        Dues_year = s_year.ToString(),
                        duesclassdesabled = isDues,
                        duesclassdesabled2 = isDues2,
                        Desabletext = desabletext,
                        PrintBTN = printBTN,

                        Admitcard_no = admitcard_no,
                        IsadmitcardNoShow = isadmitcardNoShow,

                        WhatDiv = whatDiv,
                        WhatDivTB = whatDivTB,
                        MaiNpageWidth = maiNpageWidth,
                        OneAdmitCardWdth = OneAdmitCardWdth,
                        OneAdmitCardHeight = OneAdmitCardHeight,
                        stdInfoWdth = stdInfoWdth,
                        stdInfoWdthLft = stdInfoWdthLft,
                        stdInfoWdthRght = stdInfoWdthRght,

                        Class_teacher_sig = class_teacher_sig,
                        Principal_sig = principal_sig,
                        Examinee_sig = examinee_sig,

                        SignAuto = signAuto,
                        SignWirhSetting = signWirhSetting,
                        LftSign = lftSign,
                        LftSignName = lftSignName,
                        MdlSign = mdlSign,
                        MdlSignName = mdlSignName,
                        RghtSign = rghtSign,
                        RghtSignName = rghtSignName,

                        MySchoolDetailsItem = MBFirmdetails,
                        MyRoutineDetailsItem = MBdetails,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }


        private string get_sign(string type)
        {
            string sigPath = "hidden";
            DataTable dt = My.dataTable("select Signature from user_details where User_Type='"+ type + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Signature"].ToString() == "")
                {
                    sigPath = "hidden";
                }
                else
                {
                    sigPath = dt.Rows[0]["Signature"].ToString();
                }
            }
            return sigPath;
        }


        private string get_exam_name(string session_id, string class_id, string term_id, string exam_id)
        {
            string exam_Name = "";
            DataTable dtt = My.dataTable("select top 1 Assessment_Name from Exam_Assessment_Details where Session_Id='" + session_id + "' and Class_id='" + class_id + "' and Exam_Term_Id='" + term_id + "' and Assessment_Id='" + exam_id + "'");
            if (dtt.Rows.Count > 0)
            {
                exam_Name = dtt.Rows[0]["Assessment_Name"].ToString();
            }
            return exam_Name;
        }

        private string get_term_name(string session_id, string class_id, string term_id)
        {
            string Term_Name = "";
            DataTable dtt = My.dataTable("select top 1 Term_Name from Exam_Term_Details where Session_Id='" + session_id + "' and Class_id='" + class_id + "' and Exam_Term_Id='" + term_id + "'");
            if (dtt.Rows.Count > 0)
            {
                Term_Name = dtt.Rows[0]["Term_Name"].ToString();
            }
            return Term_Name;
        }

        private List<MyRoutineDetails> findmyRoutine(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id, string Section, string Session_name, string Exam_id)
        {
            List<MyRoutineDetails> MyRoutineDetailsItem = new List<MyRoutineDetails>();
            string query = "Select DISTINCT format(es.Exam_Date_time, 'dd/MM/yyyy') as Exam_date,format(es.Exam_Date_time, 'yyyyMMdd') as Exam_idate from Subject_Master sm join Exam_Time_Table es on es.Subject_id=sm.Subject_id and es.Class_id=sm.course_id join   Subject_Mapping_New smn on smn.Class_id=es.Class_id and smn.Sub_id=es.Subject_id and smn.Session_id=es.Session_Id  where es.Class_id=" + Class_id + " and es.Branch_id='" + Branch_id + "' and es.Session_Id=" + Session_id + " and es.Section='" + Section + "' and smn.Admission_no='" + Admission_no + "' and es.Exam_Term_Id=" + Term_id + " and es.Exam_id='" + Exam_id + "' order by format(es.Exam_Date_time, 'yyyyMMdd') asc";
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
                    //First Setting
                    string date = "";
                    string day = "";
                    string time = "";
                    string subjFirstSHift = "";
                    string first_sitting = "";


                    string firstShiftSubject = "";
                    DataTable dtRF = My.dataTable("Select es.Day,sm.Subject_name,format(es.Exam_Date_time, 'dd/MM/yyyy') as Exam_datetime1,format(es.Exam_Date_time, 'hh:mm tt') as Exam_time1,format(es.Exam_E_Date_time, 'hh:mm tt') as Exam_E_time1,Shift from Subject_Master sm join Exam_Time_Table es on es.Subject_id=sm.Subject_id and es.Class_id=sm.course_id join   Subject_Mapping_New smn on smn.Class_id=es.Class_id and smn.Sub_id=es.Subject_id and smn.Session_id=es.Session_Id  where es.Class_id=" + Class_id + " and es.Branch_id='" + Branch_id + "' and es.Session_Id=" + Session_id + " and es.Section='" + Section + "' and smn.Admission_no='" + Admission_no + "' and es.Exam_Term_Id=" + Term_id + " and format(es.Exam_Date_time, 'yyyyMMdd')='" + dr["Exam_idate"].ToString() + "' and es.Exam_id='" + Exam_id + "' and Shift='1' order by Shift asc");
                    if (dtRF.Rows.Count > 0)
                    {
                        foreach (DataRow drRF in dtRF.Rows)
                        {
                            firstShiftSubject = firstShiftSubject + drRF["Subject_name"].ToString() + " / ";
                        }
                        firstShiftSubject = firstShiftSubject.Remove(firstShiftSubject.Length - 2);
                        subjFirstSHift = firstShiftSubject;

                        date = dtRF.Rows[0]["Exam_datetime1"].ToString();
                        day = dtRF.Rows[0]["Day"].ToString();
                        time = dtRF.Rows[0]["Exam_time1"].ToString() + "-" + dtRF.Rows[0]["Exam_E_time1"].ToString();
                    }
                    else
                    {
                        subjFirstSHift = "-";
                    }


                    MyRoutineDetailsItem.Add(new MyRoutineDetails
                    {
                        Subject_name = subjFirstSHift,
                        Day = day,
                        Exam_date = date,
                        Exam_time = time,
                    });
                }
            }
            return MyRoutineDetailsItem;
        }


        private List<MySchoolDetails> findmyFirmDetails(string class_name, string session_name)
        {
            List<MySchoolDetails> MySchoolDetailsItem = new List<MySchoolDetails>();
            string query = "select *,isnull((select top 1 Path from Header_templete where Status='1' and Type='Admit_card'),'0') as header_templete from Firm_Details";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Firm_Details");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string aff_no = dt.Rows[0]["Affiliation"].ToString(); string class_names = ""; string underR = "hidden";
                    if (dr["firm_name"].ToString() == "DELHI PUBLIC SCHOOL, NTPC FARAKKA")
                    {
                        underR = "showd";
                    }
                    if (aff_no.ToUpper() == "NA" || aff_no.ToUpper() == "")
                    {
                        aff_no = "hidden";
                    }
                    else
                    {
                        try
                        {
                            aff_no = dt.Rows[0]["Affiliation_by"].ToString() + " Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
                        }
                        catch
                        {
                            aff_no = "CBSE Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
                        }
                        if (dt.Rows[0]["firm_name"].ToString() == "Burdwan Holy Child School")
                        {
                            aff_no = "CISCE Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
                        }
                    }


                    if (dt.Rows[0]["firm_name"].ToString() == "SMILE School")
                    {
                        class_names = "ADMIT CARD FOR " + class_name + " SMILE STARS TEST-2023";
                    }

                    else
                    {
                        class_names = "ADMIT CARD FOR " + class_name + " ADMISSION TEST";
                    }

                    string pagesizE = "1215px;";


                    string Mobile_no_email = "Mobile No. : " + dr["contact_no"].ToString() + " , E-mail Address : " + dr["email"].ToString();
                    if (dr["email"].ToString() == "")
                    {
                        Mobile_no_email = "Mobile No. : " + dr["contact_no"].ToString();
                    }


                    string headertemplete = dr["header_templete"].ToString();
                    string content_header = "hidden";
                    if (headertemplete == "0")
                    {
                        headertemplete = "hidden";
                        content_header = "showd";
                    }
                    string middl_sign_hide = "showd"; string first_sign_text = "CLASS TEACHER";
                    if (dt.Rows[0]["firm_id"].ToString() == "MDA-01" || dt.Rows[0]["firm_id"].ToString() == "MDA-02")
                    {
                        middl_sign_hide = "hidden";
                        first_sign_text = "OFFICE INCHARGE";
                    }
                    if (dt.Rows[0]["firm_id"].ToString() == "EPIC-1")
                    {
                        middl_sign_hide = "hidden";
                    }

                    string programe = "showD";
                    if (dt.Rows[0]["Is_admit_card_programe_hide"].ToString() == "True")
                    {
                        programe = "hidden";
                    }

                    MySchoolDetailsItem.Add(new MySchoolDetails
                    {
                        School_name = dr["firm_name"].ToString(),
                        Affiliation_no = aff_no,
                        Address = dr["address1"].ToString(),
                        Mobile_no_email = Mobile_no_email,
                        Session = "ACADEMIC SESSION : " + session_name,
                        session_name = session_name,
                        LogoSchool = dr["logo"].ToString(),
                        Class_names = class_names,
                        UnderShow = underR,
                        Programe = programe,
                        PagesizE = pagesizE,
                        Header_templete = headertemplete,
                        Content_header = content_header,
                        isMiddl_sign_hide = middl_sign_hide,
                        First_sign_text = first_sign_text,
                    });
                }
            }
            return MySchoolDetailsItem;
        }


        #endregion




    }
}
