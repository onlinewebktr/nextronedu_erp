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

namespace school_web.Examination_Admin.webService
{
    /// <summary>
    /// Summary description for teacher_mark_status
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class teacher_mark_status : System.Web.Services.WebService
    {
        My mycode = new My();
        public class MyReporTeacherMark
        {
            public string Teacher_name { get; set; }
            public string UserID { get; set; }
            public string Section { get; set; }
            public string Assessment_Name { get; set; }
            public string Class_name { get; set; }
            public string Subject_name { get; set; }

            public List<MySubjStatus> MysubjStstusList { get; set; }
        }

        public class MySubjStatus
        {
            public string Unit_name { get; set; }
            public string Status { get; set; }
            public string Colors { get; set; }
            public string Multile_unit { get; set; }
            public string Single_unit { get; set; }
        }
        List<MyReporTeacherMark> MyReporTeacherMarkItem = new List<MyReporTeacherMark>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_mark_status_of_teacher(string Session_id, string Class_id, string Term_id, string Exam_name)
        {
            string qry = "select eas.Exam_Term_Id,eas.Assessment_Id,eas.Assessment_Name,tsm.UserID,tsm.AssignCourseID as Subject_id,tsm.CategoryID as Class_id,tsm.section,(select top 1 name from user_details where user_id=tsm.UserID) as Teacher_name,cc.Course_Name as Class_name,(select top 1 Subject_name from Subject_Master where course_id=tsm.CategoryID and Subject_id=tsm.AssignCourseID) as Subject_name from TeacherCourseSubjectMaping tsm join Add_course_table cc on tsm.CategoryID=cc.course_id join Exam_Assessment_Details eas on tsm.Session_id=eas.Session_id and tsm.CategoryID=eas.Class_id join Subject_Master sm on tsm.CategoryID=sm.course_id and tsm.AssignCourseID=sm.Subject_id where eas.Session_id='" + Session_id + "' and eas.Class_id in (" + Class_id + ") and eas.Exam_Term_Id in (" + Term_id + ") and eas.Assessment_Name='" + Exam_name + "' and sm.Is_mandatory=1 and AssignCourseID in (select Subject_id from Exam_Assessment_Subject_Mapping_Details where Session_Id=eas.Session_Id and Class_id=eas.Class_id and Exam_Term_Id=eas.Exam_Term_Id and Assessment_Id=eas.Assessment_Id and Istatus=1 and Branch_Id=1) order by cc.Position,tsm.section,tsm.AssignCourseID asc";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MySubjStatus> MBdetails = findsubjMarks(Session_id, Class_id, dr["Exam_Term_Id"].ToString(), dr["section"].ToString(), dr["Subject_id"].ToString(), dr["Assessment_Id"].ToString());
                    MyReporTeacherMarkItem.Add(new MyReporTeacherMark
                    {
                        Teacher_name = dr["Teacher_name"].ToString(),
                        UserID = dr["UserID"].ToString(),
                        Section = dr["section"].ToString(),
                        Assessment_Name = dr["Assessment_Name"].ToString(),
                        Class_name = dr["Class_name"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                        MysubjStstusList = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyReporTeacherMarkItem));
            }
        }
        private List<MySubjStatus> findsubjMarks(string Session_id, string Class_id, string Term_Id, string Section, string Subject_id, string Assessment_Id)
        {
            List<MySubjStatus> MysubjStstusList = new List<MySubjStatus>();
            string total_std_subj_assiged = no_of_student_assign(Session_id, Class_id, Term_Id, Section, Subject_id);
            string query = "select Subject_Activity_Name,Subject_Sub_Level_Id,(select count(em.Id) as TotalStd from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Class_id=ar.Class_id and em.Section=ar.Section and em.Admission_no=ar.admissionserialnumber where em.Session_id='" + Session_id + "' and em.Class_id='" + Class_id + "' and em.Section='" + Section + "' and em.Term='" + Term_Id + "' and Subject='" + Subject_id + "' and Subject_activity=Exam_Subject_Sub_Level.Subject_Sub_Level_Id and em.Branch_id='1' and ar.Status=1) as TotalStd from Exam_Subject_Sub_Level where Session_Id='" + Session_id + "' and Branch_Id='1' and Exam_Term_Id='" + Term_Id + "' and Assessment_Id='" + Assessment_Id + "' and Class_id='" + Class_id + "' and Subject_id='" + Subject_id + "' order by Subject_Activity_Name asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_Subject_Sub_Level");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                if (dt.Rows.Count > 1)
                {
                    string multile_true = "showed";
                    string single_false = "hidden";
                    foreach (DataRow dr in dt.Rows)
                    {
                        string ttl_mrks_entred = dr["TotalStd"].ToString(); // get_ttl_marks_entred(Session_id, Class_id, Term_Id, Section, Subject_id, Assessment_Id, dr["Subject_Sub_Level_Id"].ToString());
                        string status = "Pending"; string colors = "redColor";
                        if (My.toDouble(ttl_mrks_entred) >= My.toDouble(total_std_subj_assiged))
                        {
                            status = "Completed";
                            colors = "greenColor";
                        }
                        MysubjStstusList.Add(new MySubjStatus
                        {
                            Unit_name = dr["Subject_Activity_Name"].ToString(),
                            Status = status,
                            Colors = colors,
                            Multile_unit = multile_true,
                            Single_unit = single_false,
                        });
                    }
                }
                else
                {
                    string multile_true = "hidden";
                    string single_false = "showed";
                    string ttl_mrks_entred = dt.Rows[0]["TotalStd"].ToString(); //get_ttl_marks_entred(Session_id, Class_id, Term_Id, Section, Subject_id, Assessment_Id, dt.Rows[0]["Subject_Sub_Level_Id"].ToString());
                    string status = "Pending"; string colors = "redColor";
                    if (My.toDouble(ttl_mrks_entred) >= My.toDouble(total_std_subj_assiged))
                    {
                        status = "Completed";
                        colors = "greenColor";
                    }

                    MysubjStstusList.Add(new MySubjStatus
                    {
                        Unit_name = dt.Rows[0]["Subject_Activity_Name"].ToString(),
                        Status = status,
                        Colors = colors,
                        Multile_unit = multile_true,
                        Single_unit = single_false,
                    });
                }
            }
            return MysubjStstusList;
        }

        private string get_ttl_marks_entred(string session_id, string class_id, string term_Id, string section, string subject_id, string assessment_Id, string Subject_Sub_Level_Id)
        {
            string returN = "0";
            DataTable dt = My.dataTable("select count(em.Id) as TotalStd from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Class_id=ar.Class_id and em.Section=ar.Section and em.Admission_no=ar.admissionserialnumber where em.Session_id='" + session_id + "' and em.Class_id='" + class_id + "' and em.Section='" + section + "' and em.Term='" + term_Id + "' and Subject='" + subject_id + "' and Subject_activity='" + Subject_Sub_Level_Id + "' and em.Branch_id='1'  and ar.Status=1");
            if (dt.Rows.Count > 0)
            {
                returN = dt.Rows[0]["TotalStd"].ToString();
            }
            return returN;
        }

        private string no_of_student_assign(string session_id, string class_id, string term_Id, string Section, string subject_id)
        {
            string returN = "0";
            DataTable dt = My.dataTable("select count(admissionserialnumber) as TotalStd from admission_registor ar join Subject_Mapping_New smp on ar.Session_id=smp.Session_id and ar.Class_id=smp.Class_id and ar.Section=smp.Section and ar.admissionserialnumber=smp.Admission_no where smp.Session_id='" + session_id + "' and smp.Class_id='" + class_id + "' and smp.Section='" + Section + "' and Sub_id='" + subject_id + "' and ar.Status=1");
            if (dt.Rows.Count > 0)
            {
                returN = dt.Rows[0]["TotalStd"].ToString();
            }
            return returN;
        }
    }
}
