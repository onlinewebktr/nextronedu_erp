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

namespace school_web.Admin.slip.webServices
{
    /// <summary>
    /// Summary description for ScholarshipResult
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ScholarshipResult : System.Web.Services.WebService
    {

        public class MyAdmitCardStudent
        {

            public string sch_name { get; set; }
            public string Center_Address { get; set; }
            public string Student_name { get; set; }
            public string Subject_DOB { get; set; }
            public string Father_name { get; set; }
            public string Student_mob_no { get; set; }
            public string Registration_id { get; set; }
            public string Course_Name { get; set; }
            public string Session_name { get; set; }
            public string Student_img { get; set; }
            public string Remarks { get; set; }
            public string Mother_name { get; set; }
            public string Mother_mobile { get; set; }
            public string Print_for { get; set; }

            public string Roll_no { get; set; }
            public string Room_no { get; set; }

            public string Reporting_time { get; set; }
            public string Gate_close_time { get; set; }
            public string Persent_adress { get; set; }
            public string Is_img_show { get; set; }
            public string centername { get; set; }
            public string admissiondateandtime { get; set; }

            public List<MySchoolDetails> MySchoolDetailsItem { get; set; }

            public List<MyExamDetails> MyExamDetailsItem { get; set; }
            public List<MySigDetails> MySigDetailsItem { get; set; }
        }

        public class MySchoolDetails
        {
            public string School_name { get; set; }
            public string Affiliation_no { get; set; }
            public string Address { get; set; }
            public string Mobile_no_email { get; set; }
            public string Session { get; set; }
            public string LogoSchool { get; set; }
            public string Class_names { get; set; }
            public string Admit_card_title { get; set; }
        }
        public class MyExamDetails
        {




            public string Full_Marks { get; set; }
            public string Obtain_Marks { get; set; }
            public string Obtain_percentage { get; set; }
            public string Obtain_rank { get; set; }
            public string Exam_Result { get; set; }
            public string Attendance_Status { get; set; }




        }
        public class MySigDetails
        {
            public string Class_teacher_sig { get; set; }
            public string Principal_sig { get; set; }
            public string Examinee_sig { get; set; }
        }

        List<MyAdmitCardStudent> EMySubMark = new List<MyAdmitCardStudent>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_result_card_details(string scholarshipid, string Admission_no, string Coming_from)
        {



            string centername = "Select top 1 Centre_Name from Scholarship_Exam_Centre where Exam_Centre_Id=ore.Exam_Centre_Id and Test_id=ore.Test_id ";
            string Center_Address = "Select top 1 Centre_Address from Scholarship_Exam_Centre where Exam_Centre_Id=ore.Exam_Centre_Id and Test_id=ore.Test_id";
            string Scholorship_Benefit = "Select top 1 Scholorship_Benefit from Scholarship_Parameter_fees where  Test_id=ore.Test_id";
            string sch_name = "Select top 1 Test_name from Scholarship_Program where  Test_id=ore.Test_id";


            string query = "";
            if (Coming_from == "out" || Coming_from == "in_s")
            {
                if (Admission_no == "0")
                {
                    query = "Select oa.*,'0'  as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,CASE WHEN oa.Student_image is null THEN '/images/dummy-student.jpg'  WHEN oa.Student_image is not null THEN oa.Student_image END AS Student_img,ore.Remarks,'hidden' as print_for,ore.Roll_no,ore.Reporting_time,ore.Gate_close_time,(" + centername + ") as Center_Name,(" + Center_Address + ") as Center_Address,ore.Exam_Shift,ore.Room_no,er.Full_Marks,er.Obtain_Marks,er.Obtain_percentage,er.Obtain_rank,er.Attendance_Status,er.Exam_Result,er.Admission_date,er.Admission_time,(" + Scholorship_Benefit + ") as Scholorship_Benefit,(" + sch_name + ") as sch_name  from Scholarship_Admission oa join Scholarship_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id join Scholarship_Exam_Result er on er.Admission_no=ore.Admission_no and er.Test_id=ore.Test_id    where   ore.Test_id=" + scholarshipid + "   and oa.Payment_Status='Paid' order by er.Obtain_rank ";
                }
                else
                {
                    query = "Select oa.*,'0'  as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,CASE WHEN oa.Student_image is null THEN '/images/dummy-student.jpg'  WHEN oa.Student_image is not null THEN oa.Student_image END AS Student_img,(Select top 1 Terms from Online_Admit_Card_Term)as Terms,ore.Remarks,'hidden' as print_for,ore.Roll_no,ore.Reporting_time,ore.Gate_close_time,(" + centername + ") as Center_Name,(" + Center_Address + ") as Center_Address,ore.Exam_Shift,ore.Room_no,er.Full_Marks,er.Obtain_Marks,er.Obtain_percentage,er.Obtain_rank,er.Attendance_Status,er.Exam_Result,er.Admission_date,er.Admission_time,(" + Scholorship_Benefit + ") as Scholorship_Benefit,(" + sch_name + ") as sch_name  from Scholarship_Admission oa join Scholarship_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id join Scholarship_Exam_Result er on er.Admission_no=ore.Admission_no and er.Test_id=ore.Test_id   where  oa.Registration_id='" + Admission_no + "' and ore.Test_id=" + scholarshipid + "   and oa.Payment_Status='Paid'";

                }




            }
            else
            {
                query = "Select oa.*,'0'  as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,CASE WHEN oa.Student_image is null THEN '/images/dummy-student.jpg'  WHEN oa.Student_image is not null THEN oa.Student_image END AS Student_img,(Select top 1 Terms from Online_Admit_Card_Term)as Terms,'hidden' as print_for,ore.Roll_no,ore.Reporting_time,ore.Gate_close_time ,(" + centername + ") as Center_Name,(" + Center_Address + ") as Center_Address,ore.Exam_Shift,ore.Room_no,er.Full_Marks,er.Obtain_Marks,er.Obtain_percentage,er.Obtain_rank,er.Attendance_Status,er.Exam_Result,er.Admission_date,er.Admission_time,(" + Scholorship_Benefit + ") as Scholorship_Benefit,(" + sch_name + ") as sch_name from Scholarship_Admission oa join Scholarship_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id join Scholarship_Exam_Result er on er.Admission_no=ore.Admission_no and er.Test_id=ore.Test_id   where   and oa.Payment_Status='Paid' and ore.Test_id=" + scholarshipid + "  order by er.Obtain_rank ";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Scholarship_Admission");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {

                
                foreach (DataRow dr in dt.Rows)
                {
                    List<MySchoolDetails> MBdetails = findmyFirmDetails(dr["Course_Name"].ToString(), dr["Session_name"].ToString());
                    List<MyExamDetails> MBExamdetails = findmyExamresult(dr["Registration_id"].ToString(), dr["Test_id"].ToString());
                    List<MySigDetails> MBSigdetails = findmySigDetails(dr["Session_id"].ToString(), dr["Class_id"].ToString(), "1");


                    string motherMob = dr["Mother_mobile"].ToString();
                    if (motherMob == "")
                    {
                        motherMob = "hidden";
                    }
                    string Print_for = dr["print_for"].ToString(); //string evrcoma = "'";
                    if (Print_for == "Candidates Copy")
                    {
                        Print_for = "Candidates' Copy";
                    }
                    string is_image_show = "show";
                    //string is_image_show = "hidden";
                    //if (dr["Is_image_updated"].ToString() == "True")
                    //{
                    //    is_image_show = "show";
                    //}
                    string std_imgs = dr["Student_img"].ToString();
                    if (std_imgs == "")
                    {
                        is_image_show = "hidden";
                    }

                    EMySubMark.Add(new MyAdmitCardStudent
                    {
                        sch_name = dr["sch_name"].ToString(),


                        Student_name = dr["Name"].ToString(),
                        Subject_DOB = dr["DOB"].ToString(),
                        Father_name = dr["Father_name"].ToString(),
                        Student_mob_no = dr["Student_mob_no"].ToString(),
                        Registration_id = dr["Registration_id"].ToString(),
                        Course_Name = dr["Course_Name"].ToString(),
                        Session_name = dr["Session_name"].ToString(),
                        Student_img = dr["Student_img"].ToString(),
                        Remarks = dr["Scholorship_Benefit"].ToString(),
                        Is_img_show = is_image_show,
                        Roll_no = dr["Roll_no"].ToString(),
                        Reporting_time = dr["Reporting_time"].ToString(),
                        Gate_close_time = dr["Gate_close_time"].ToString(),
                        Persent_adress = dr["Persent_adress"].ToString(),

                        Mother_name = dr["Mother_name"].ToString(),
                        Mother_mobile = motherMob,
                        Print_for = Print_for,
                        MySchoolDetailsItem = MBdetails,
                        MyExamDetailsItem = MBExamdetails,
                        MySigDetailsItem = MBSigdetails,
                        centername = dr["Center_Name"].ToString(),
                        Center_Address = dr["Center_Address"].ToString(),
                        Room_no = dr["Room_no"].ToString(),

                        admissiondateandtime = dr["Admission_date"].ToString() + " " + dr["Admission_time"].ToString(),



                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }




        private List<MySchoolDetails> findmyFirmDetails(string class_name, string session_name)
        {
            List<MySchoolDetails> MySchoolDetailsItem = new List<MySchoolDetails>();

            string query = "select * from Firm_Details";
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
                    string aff_no = ""; string class_names = ""; string admit_card_title = ""; string Session = "";
                    try
                    {
                        if (dt.Rows[0]["firm_name"].ToString() == "SMILE School")
                        {
                            aff_no = "hidden";
                        }
                        else
                        {
                            if (dt.Rows[0]["Affiliation"].ToString() == "")
                            {
                                aff_no = "hidden";
                            }
                            else
                            {
                                aff_no = "CBSE Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();

                            }

                        }
                    }
                    catch
                    {
                        aff_no = "CBSE Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
                    }

                    if (dt.Rows[0]["firm_name"].ToString() == "SMILE School")
                    {
                        class_names = "SMILE STARS TEST-2023";
                        admit_card_title = "ADMIT CARD (APPLIED FOR) " + class_name;
                        Session = "hidden";
                    }
                    else
                    {
                        admit_card_title = "hidden";
                        class_names = "RESULT FOR " + class_name + " SCHOLARSHIP EXAMINATION";
                        Session = "ACADEMIC SESSION : " + session_name;
                    }


                    MySchoolDetailsItem.Add(new MySchoolDetails
                    {
                        School_name = dr["firm_name"].ToString(),
                        Affiliation_no = aff_no,
                        Address = dr["address1"].ToString(),
                        Mobile_no_email = "Telephone No : " + dr["contact_no"].ToString() + " , E-mail Address : " + dr["email"].ToString(),
                        Session = Session,
                        LogoSchool = dr["logo"].ToString(),
                        Class_names = class_names,
                        Admit_card_title = admit_card_title,
                    });
                }
            }
            return MySchoolDetailsItem;
        }

        //===============================
        private List<MyExamDetails> findmyExamresult(string admission_no, string Test_id)
        {
            List<MyExamDetails> MyExamDetailsItem = new List<MyExamDetails>();
            string query = "Select *  from Scholarship_Exam_Result where Admission_no='" + admission_no + "' and  Test_id=" + Test_id + "   ";
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
                    MyExamDetailsItem.Add(new MyExamDetails
                    {
                        Full_Marks = dr["Full_Marks"].ToString(),
                        Obtain_Marks = dr["Obtain_Marks"].ToString(),
                        Obtain_percentage = dr["Obtain_percentage"].ToString(),
                        Obtain_rank = dr["Obtain_rank"].ToString(),
                        Exam_Result = dr["Exam_Result"].ToString(),
                        Attendance_Status = dr["Attendance_Status"].ToString(),

                    });
                }
            }
            return MyExamDetailsItem;
        }

        //===============================
        private List<MySigDetails> findmySigDetails(string Session_id, string Class_id, string Branch_id)
        {
            List<MySigDetails> MySigDetailsItem = new List<MySigDetails>();

            string signatures = Examination.get_signature_admit_card(Session_id, Class_id, "A", Branch_id);
            string[] stringSeparatorss = new string[] { ">" };
            string[] arrs = signatures.Split(stringSeparatorss, StringSplitOptions.None);
            string class_teacher_sig = arrs[0];
            string principal_sig = arrs[1];
            string examinee_sig = arrs[2];

            MySigDetailsItem.Add(new MySigDetails
            {
                Class_teacher_sig = class_teacher_sig,
                Principal_sig = principal_sig,
                Examinee_sig = examinee_sig,
            });


            return MySigDetailsItem;
        }
    }
}
