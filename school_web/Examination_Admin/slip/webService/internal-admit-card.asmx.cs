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

namespace school_web.Examination_Admin.slip.webService
{
    /// <summary>
    /// Summary description for internal_admit_card
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class internal_admit_card : System.Web.Services.WebService
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


            public string Exam_date { get; set; }
            public string Exam_day { get; set; }
            public string Exam_start_time { get; set; }
            public string Exam_end_time { get; set; }


            public string Arrival_time { get; set; }
            public string IsarrTimeShow { get; set; } 

            public List<MySchoolDetails> MySchoolDetailsItem { get; set; }

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
        public void fetch_admit_cards(string Session_id, string Class_id, string Admission_no, string Section, string Session_name, string Exam_id, string CheckedStd, string UserType)
        {
            string exam_name = get_exam_name(Session_id, Class_id, Exam_id);
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
                    string signatures = Examination.get_signature_admit_card(Session_id, Class_id, Section, "1");
                    string[] stringSeparatorssign = new string[] { ">" };
                    string[] arrsign = signatures.Split(stringSeparatorssign, StringSplitOptions.None);
                    class_teacher_sig = arrsign[0];
                    principal_sig = arrsign[1];
                    examinee_sig = arrsign[2];
                }



                DataTable dtE = My.dataTable("select *, Format(convert(DateTime,Exam_date,103), 'dd MMM yyyy') as Exam_dates,Format(convert(DateTime,Exam_date,103), 'dddd') as Exam_day from Internal_exam_admit_card where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Exam_id='" + Exam_id + "'");
                bool loops = false; int topM = 1; bool loopsTB = false;
                foreach (DataRow dr in dt.Rows)
                {
                    List<MySchoolDetails> MBFirmdetails = findmyFirmDetails(dr["class"].ToString(), Session_name);

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



                    //========================
                    string desabletext = ""; string printBTN = "";
                    string admitcard_no = ""; string isadmitcardNoShow = "hidden";
                    string whatDiv = ""; string whatDivTB = "";
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


                    if (loops == false)
                    {
                        whatDiv = "topdb1x2";
                        loops = true;
                    }
                    else
                    {
                        loops = false;
                        whatDiv = "btmdb1x2";
                    }
                    whatDivTB = "topBtmdb1x2";

                    string isarrTimeShow = "hidden";
                    string isarrtime= dtE.Rows[0]["Is_arrival_time"].ToString();
                    if (isarrtime == "Yes")
                    {
                        isarrTimeShow = "showd";
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

                        Exam_name = exam_name,
                        Desabletext = desabletext,
                        PrintBTN = printBTN,

                        Admitcard_no = admitcard_no,
                        IsadmitcardNoShow = isadmitcardNoShow,

                        WhatDiv = whatDiv,
                        WhatDivTB = whatDivTB,

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


                        Exam_date = dtE.Rows[0]["Exam_dates"].ToString(),
                        Exam_day = dtE.Rows[0]["Exam_day"].ToString(),
                        Exam_start_time = dtE.Rows[0]["Exam_start_time"].ToString(),
                        Exam_end_time = dtE.Rows[0]["Exam_end_time"].ToString(),
                        Arrival_time = dtE.Rows[0]["Exam_arrival_time"].ToString(),
                        IsarrTimeShow= isarrTimeShow,
                        MySchoolDetailsItem = MBFirmdetails,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }

        private string get_exam_name(string session_id, string class_id, string exam_id)
        {
            string exam_Name = "";
            DataTable dtt = My.dataTable("select top 1 Exam_name from Internal_Exam_master where Session_id='" + session_id + "'  and Exam_id='" + exam_id + "'");
            if (dtt.Rows.Count > 0)
            {
                exam_Name = dtt.Rows[0]["Exam_name"].ToString();
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



                    string pagesizE = "1215px;";


                    string Mobile_no_email = "Mobile No. : " + dr["contact_no"].ToString() + ", E-mail Address : " + dr["email"].ToString();
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
