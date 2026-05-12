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
    /// Summary description for result
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class result : System.Web.Services.WebService
    {
        public class MyAdmitCardStudent
        {
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
            public string Result_status { get; set; }
            public string Is_remark_show { get; set; }
            public string Admission_date { get; set; }
            public string Admission_fee { get; set; }
            public List<MySchoolDetails> MySchoolDetailsItem { get; set; }


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
        public void fetch_result_card_details(string Session_id, string Class_id, string Admission_no, string Coming_from, string Branch_id)
        {
            string query = "";
            if (Coming_from == "out")
            {
                query = "select oa.*,'0'  as admissionnumber,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,CASE WHEN oa.Student_image is null THEN '/images/dummy-student.jpg'  WHEN oa.Student_image is not null THEN oa.Student_image END AS Student_img,(Select top 1 Terms from Online_Admit_Card_Term) as Terms,'Candidates Copy' as print_for,(select top 1 Remarks from Online_exam_result_remarks) as Remarks,ore.Attendance_Status,ore.Exam_Result,ore.Admission_date,ore.Admission_time  from Online_Admission oa join Online_Reg_Exam_Result ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id and oa.Class_id=ore.Class_id where oa.Session_id=" + Session_id + " and oa.Registration_id='" + Admission_no + "' and oa.Payment_Status='Paid'  UNION all select oa.*,'0'  as admissionnumber,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,CASE WHEN oa.Student_image is null THEN '/images/dummy-student.jpg'  WHEN oa.Student_image is not null THEN oa.Student_image END AS Student_img,(Select top 1 Terms from Online_Admit_Card_Term) as Terms,'School Copy' as print_for,(select top 1 Remarks from Online_exam_result_remarks) as Remarks,ore.Attendance_Status,ore.Exam_Result,ore.Admission_date,ore.Admission_time  from Online_Admission oa join Online_Reg_Exam_Result ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id and oa.Class_id=ore.Class_id where oa.Session_id=" + Session_id + " and oa.Registration_id='" + Admission_no + "'  and oa.Payment_Status='Paid'";
            }
            else
            {
                query = "select oa.*,'0'  as admissionnumber,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,CASE WHEN oa.Student_image is null THEN '/images/dummy-student.jpg'  WHEN oa.Student_image is not null THEN oa.Student_image END AS Student_img,(Select top 1 Terms from Online_Admit_Card_Term) as Terms,'hidden' as print_for,(select top 1 Remarks from Online_exam_result_remarks) as Remarks,ore.Attendance_Status,ore.Exam_Result,ore.Admission_date,ore.Admission_time  from Online_Admission oa join Online_Reg_Exam_Result ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id and oa.Class_id=ore.Class_id where oa.Session_id=" + Session_id + " and oa.Class_id=" + Class_id + "  and oa.Payment_Status='Paid' order by oa.Name ";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Online_Admission");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string admission_fee = get_adm_fee(Session_id, Class_id, Branch_id);
                foreach (DataRow dr in dt.Rows)
                {
                    List<MySchoolDetails> MBdetails = findmyFirmDetails(dr["Course_Name"].ToString(), dr["Session_name"].ToString());

                    List<MySigDetails> MBSigdetails = findmySigDetails(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Branch_id);

                    string result_status = "";
                    string std_imgs = dr["Student_img"].ToString();
                    if (std_imgs == "")
                    {
                        std_imgs = "hidden";
                    }
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

                    if (dr["Attendance_Status"].ToString() == "Absent")
                    {
                        result_status = "Absent";
                    }
                    else
                    {
                        result_status = dr["Exam_Result"].ToString() + " FOR " + dr["Class"].ToString();
                    }

                    string remark_show = "hidden";
                    if (dr["Attendance_Status"].ToString() == "Present")
                    {
                        if (dr["Exam_Result"].ToString() == "Qualified")
                        {
                            remark_show = "showed";
                        }
                    }



                    EMySubMark.Add(new MyAdmitCardStudent
                    {
                        Student_name = dr["Name"].ToString(),
                        Subject_DOB = dr["DOB"].ToString(),
                        Father_name = dr["Father_name"].ToString(),
                        Student_mob_no = dr["Student_mob_no"].ToString(),
                        Registration_id = dr["Registration_id"].ToString(),
                        Course_Name = dr["Course_Name"].ToString(),
                        Session_name = dr["Session_name"].ToString(),
                        Student_img = std_imgs,
                        Remarks = dr["Remarks"].ToString(),
                        Mother_name = dr["Mother_name"].ToString(),
                        Mother_mobile = motherMob,
                        Print_for = Print_for,
                        Result_status = result_status,
                        Is_remark_show = remark_show,
                        Admission_date = dr["Admission_date"].ToString() + " - " + dr["Admission_time"].ToString(),
                        Admission_fee = admission_fee,
                        MySchoolDetailsItem = MBdetails,

                        MySigDetailsItem = MBSigdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }

        My mycode = new My();
        private string get_adm_fee(string Session_id, string Class_id, string Branch_id)
        {
            string returN = "0";
            SqlCommand cmd = new SqlCommand("select isnull(sum(convert(float, amount)),0) as Fee from dbo.[Fee_master_content_wise] where session_id='" + Session_id + "' and class_id='" + Class_id + "' and parameter='AdmissionFee' and content='Admission Fee'");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                returN = dt.Rows[0]["Fee"].ToString();
            }
            return returN;
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
                    string aff_no = ""; string class_names = "";
                    try
                    {
                        //aff_no = dt.Rows[0]["Affiliation_by"].ToString() + " Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
                        aff_no = "CISCE Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
                    }
                    catch
                    {
                        aff_no = "CBSE Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
                    }

                    if (dt.Rows[0]["firm_name"].ToString() == "SMILE School")
                    {
                        class_names = "ADMIT CARD FOR " + class_name + " SMILE STARS TEST-2023";
                    }
                    else
                    {
                        class_names = "RESULT CARD OF " + class_name + "  ADMISSION TEST";
                    }


                    MySchoolDetailsItem.Add(new MySchoolDetails
                    {
                        School_name = dr["firm_name"].ToString(),
                        Affiliation_no = aff_no,
                        Address = dr["address1"].ToString(),
                        Mobile_no_email = "Telephone No : " + dr["contact_no"].ToString() + " , E-mail Address : " + dr["email"].ToString(),
                        Session = "ACADEMIC SESSION : " + session_name,
                        LogoSchool = dr["logo"].ToString(),
                        Class_names = class_names,
                    });
                }
            }
            return MySchoolDetailsItem;
        }

        //===============================


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
