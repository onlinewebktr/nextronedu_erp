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

namespace school_web.Admin.webServices
{
    /// <summary>
    /// Summary description for subjectReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class subjectReport : System.Web.Services.WebService
    {
        public class Fetch_subject_and_count
        {
            public string Class_Name { get; set; }
            public string Subject_Name { get; set; }
            public string Subject_type { get; set; }
            public string StdCount { get; set; }
            public string Class_id { get; set; }
            public string SubjId { get; set; }
            public string Session_id { get; set; }
            public string Branch_id { get; set; }
            public string Section { get; set; }
            public string Total_student { get; set; }
        }
        List<Fetch_subject_and_count> Show__subject_and_count = new List<Fetch_subject_and_count>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_classwise_allocated_subject(string Session_id, string Class_id, string Section, string Branch_id, string Subject_id)
        {
            string qry = "";
            if (Subject_id == "0")
            {
                qry = "select *,(select top 1 Course_Name from Add_course_table where course_id=Subject_Master.course_id and Branch_id=Subject_Master.Branch_id) as Class_name  from Subject_Master where course_id='" + Class_id + "' and Branch_id='" + Branch_id + "'  order by Subject_position asc";
            }
            else
            {
                qry = "select *,(select top 1 Course_Name from Add_course_table where course_id=Subject_Master.course_id and Branch_id=Subject_Master.Branch_id) as Class_name  from Subject_Master where course_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Subject_id='" + Subject_id + "' order by Subject_position asc";
            }

            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_period_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string ttl_std = get_no_of_std(Session_id, Class_id, Section, Branch_id);
                foreach (DataRow dr in dt.Rows)
                {
                    string no_of_student = get_no_of_std_mapped(Session_id, dr["course_id"].ToString(), Section, Branch_id, dr["Subject_id"].ToString());
                    Show__subject_and_count.Add(new Fetch_subject_and_count
                    {
                        Class_Name = dr["Class_name"].ToString(),
                        Subject_Name = dr["Subject_name"].ToString(),
                        Subject_type = dr["Subject_type"].ToString(),
                        StdCount = no_of_student,
                        SubjId = dr["Subject_id"].ToString(),
                        Class_id = dr["course_id"].ToString(),
                        Session_id = Session_id,
                        Branch_id = Branch_id,
                        Section = Section,
                        Total_student = ttl_std,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show__subject_and_count));
            }
        }

        private string get_no_of_std(string session_id, string class_id, string section, string branch_id)
        {
            string countStd = "0";
            DataTable dt = My.dataTable("select count(Id) as Total_std from admission_registor where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Section='" + section + "'  and Status=1");
            if (dt.Rows.Count > 0)
            {
                countStd = dt.Rows[0][0].ToString();
            }
            return countStd;
        }

        private string get_no_of_std_mapped(string session_id, string class_id, string section, string branch_id, string subject_id)
        {
            string countStd = "0";
            DataTable dt = My.dataTable("select count(Id) as Total_mapped_std from Subject_Mapping_New where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Sub_id='" + subject_id + "' and Admission_no in(select admissionserialnumber from admission_registor where admissionserialnumber=Subject_Mapping_New.Admission_no and Class_id=Subject_Mapping_New.Class_id and Section='" + section + "' and Status=1 and Session_id='" + session_id + "' and Transfer_Status in ('NT','New') and Class_id='"+ class_id + "')");
            if (dt.Rows.Count > 0)
            {
                countStd = dt.Rows[0][0].ToString();
            }
            return countStd;
        }



        public class Fetch_students
        {
            public string studentname { get; set; }
            public string admissionserialnumber { get; set; }
            public string rollnumber { get; set; }
            public string Section { get; set; }
            public string className { get; set; }
            public string Subject_name { get; set; }
        }
        List<Fetch_students> Show_students = new List<Fetch_students>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_mapped_students(string Session_ids, string Class_ids, string Sections, string Branch_ids, string Subject_Id)
        {
            string qry = "select t2.studentname,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.class,(select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Sub_id) as Subject_name from Subject_Mapping_New t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Section=t2.Section and t1.Branch_id=t2.Branch_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + Session_ids + "' and t1.Branch_id='" + Branch_ids + "' and t1.Class_id='" + Class_ids + "' and t1.Section='" + Sections + "' and t2.Section='" + Sections + "' and t1.Sub_id='" + Subject_Id + "' and t2.Status=1 order by t2.rollnumber";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Class_Routine_period_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_students.Add(new Fetch_students
                    {
                        studentname = dr["studentname"].ToString(),
                        admissionserialnumber = dr["admissionserialnumber"].ToString(),
                        rollnumber = dr["rollnumber"].ToString(),
                        Section = dr["Section"].ToString(),
                        className = dr["class"].ToString(),
                        Subject_name = dr["Subject_name"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_students));
            }
        }
    }
}
