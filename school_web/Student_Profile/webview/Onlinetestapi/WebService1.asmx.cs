using school_web.AppCode;
using school_web.AppCode.Onlineexam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Student_Profile.webview.Onlinetestapi
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
   [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        Find_and_execute_code fec = new Find_and_execute_code();

        public class All_sections
        {
            public string id { get; set; }
            public string sec_name { get; set; }
            public string time { get; set; }
            public string time_type { get; set; }
            public string tot_sec { get; set; }
            public List<questiions> ques { get; set; }

        }
        public class questiions
        {
            public string qid { get; set; }
            public string question { get; set; }
            public string answer { get; set; }
            public string marks { get; set; }
            public string id { get; set; }
            public string Direction { get; set; }
            public string Di { get; set; }
            public string cnt_type { get; set; }

            public string rcount { get; set; }

            public string Direction_HN { get; set; }
            public string Question_name_HN { get; set; }
            public string answer_HN { get; set; }

            public string DI_HN { get; set; }
            public string Question_no { get; set; }

            public string Language_Itype { get; set; }
            public string negative_mrk { get; set; }

            public List<answers> answers { get; set; }

        }
        public class answers
        {
            public string opt_id { get; set; }
            public string opt1 { get; set; }

            public string opt_id_hn { get; set; }
            public string opt1_hn { get; set; }
        }
        List<All_sections> ALLS = new List<All_sections>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bind_top_heading_tabbing(string testid)
        {
            string qry = @" select t1.id,t1.Test_id,(select top 1 Subject_name from Subject_Master where Subject_id=t2.subjectname) as  'Section_name',t2.Exam_id as Section_id,t2.Exam_duration as Time,'Minutes' Type from Section_Arranging t1 join OlineTest_Exam_name t2 on t1.Test_id=t2.Exam_id  where t1.Test_id='"+ testid + "'  order by t1.Position asc";


            string sec_name = "";

            DataTable dt = new DataTable();
            dt = fec.featch_data(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if(dr["Section_name"].ToString()=="")
                    {
                        sec_name = "All Subject";
                    }
                    else
                    {
                        sec_name = dr["Section_name"].ToString();
                    }
                    List<questiions> qs = question_list(dr["Test_id"].ToString(), dr["Section_id"].ToString());
                    ALLS.Add(new All_sections
                    {
                        id = dr["id"].ToString(),

                        sec_name = sec_name,

                        time = dr["Time"].ToString(),
                        time_type = dr["Type"].ToString(),
                        tot_sec = dt.Rows.Count.ToString(),
                        ques = qs
                    });
                }
            }
            return new JavaScriptSerializer().Serialize(ALLS);
        }

        private List<questiions> question_list(string testid, string section)
        {
            List<questiions> qs = new List<questiions>();
            DataTable dt = new DataTable();
            dt = fec.featch_data("select *,( select top 1 Negative_Marking from dbo.[Add_Exam_Category] ) as Negative_Marking from question_info where test_id='" + testid + "' and Section='" + section + "' order by cast(Question_no as int) asc");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<answers> ans = find_answers(testid, section, dr["questionid"].ToString());
                    qs.Add(new questiions
                    {
                        qid = dr["questionid"].ToString(),
                        question = dr["Question_name"].ToString(),
                        Direction = dr["Direction"].ToString(),
                        answer = dr["ans"].ToString(),
                        marks = dr["marks"].ToString(),
                        id = dr["id"].ToString(),
                        cnt_type = dr["Type"].ToString(),
                        Di = dr["DI"].ToString(),

                        answers = ans,
                        rcount = dt.Rows.Count.ToString(),

                        Direction_HN = dr["Direction_HN"].ToString(),
                        Question_name_HN = dr["Question_name_HN"].ToString(),
                        answer_HN = dr["ans_HN"].ToString(),
                        DI_HN = dr["DI_HN"].ToString(),
                        Question_no = dr["Question_no"].ToString(),
                        Language_Itype = dr["Language_Itype"].ToString(),
                        negative_mrk = dr["Negative_Marking"].ToString(),
                    });
                }
            }

            return qs;
        }

        private List<answers> find_answers(string testid, string section, string quid)
        {
            List<answers> ans = new List<answers>();
            DataTable dt = new DataTable();
            dt = fec.featch_data("select opt_code,option_text,opetion_text_HN from question_answer_Master where test_code='" + testid + "' and Section='" + section + "'  and quest_code='" + quid + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ans.Add(new answers
                    {
                        opt_id = dr["opt_code"].ToString(),
                        opt1 = dr["option_text"].ToString(),
                        opt_id_hn = dr["opt_code"].ToString(),
                        opt1_hn = dr["opetion_text_HN"].ToString()


                    });
                }
            }
            return ans;
        }
    }
}
