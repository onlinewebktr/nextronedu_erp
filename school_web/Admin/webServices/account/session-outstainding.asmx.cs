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

namespace school_web.Admin.webServices.account
{
    /// <summary>
    /// Summary description for session_outstainding
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class session_outstainding : System.Web.Services.WebService
    {
        My mycode = new My();
        public class MyReporFeeHeadDues
        {
            public string ContentName { get; set; }
        }

        //月 
        List<MyReporFeeHeadDues> MyReporFeeHeadDuesItem = new List<MyReporFeeHeadDues>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_dues_head(string Session, string Session_id, string Class_id, string Fee_head_id)
        {
            string[] stringSeparatorss = new string[] { "月" };
            string[] arr = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                //feeid = feeid + qoute + value + qoute + ",";
                MyReporFeeHeadDuesItem.Add(new MyReporFeeHeadDues
                {
                    ContentName = value,
                });
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(MyReporFeeHeadDuesItem));
            //feeid = feeid.Remove(feeid.Length - 1);
            //string qry = "select distinct content from Fee_master_content_wise where content in (" + feeid + ") order by content asc";
            //DataTable dt = My.dataTable(qry);
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        MyReporFeeHeadDuesItem.Add(new MyReporFeeHeadDues
            //        {
            //            ContentName = dr["content"].ToString(),
            //        });
            //    }
            //    JavaScriptSerializer js = new JavaScriptSerializer();
            //    Context.Response.Write(js.Serialize(MyReporFeeHeadDuesItem));
            //}
        }


        public class MyReporEstdFee
        {
            public string className { get; set; }
            public string Section { get; set; }
            public List<MyestdRpTwoLevel> MyestdRpTwoLevelList { get; set; }
        }

        public class MyestdRpTwoLevel
        {
            public string AmountssS { get; set; }
            public string Total_dues { get; set; }
            public string Rowcount { get; set; }
        }
        List<MyReporEstdFee> MyReporEstdFeeItem = new List<MyReporEstdFee>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_ourstanding_fees(string Session, string Session_id, string Class_id, string Fee_head_id)
        {
            string qoute = "'";
            string classid_coma = "";
            string[] stringSeparatorss = new string[] { "," };
            string[] arr = Class_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                classid_coma = classid_coma + qoute + value + qoute + ",";
            }
            classid_coma = classid_coma.Remove(classid_coma.Length - 1);
            string qry = "select distinct Position,class,Section,t1.Class_id from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + Session_id + "' and t1.Class_id in (" + classid_coma + ") and Section not in ('NA') order by  Position,Section asc"; // and t1.admissionserialnumber='6652'
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyestdRpTwoLevel> MBdetails = findestdFees(Session, Session_id, dr["Class_id"].ToString(), dr["Section"].ToString(), Fee_head_id);
                    MyReporEstdFeeItem.Add(new MyReporEstdFee
                    {
                        className = dr["class"].ToString(),
                        Section = dr["Section"].ToString(),
                        MyestdRpTwoLevelList = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyReporEstdFeeItem));
            }
        }

        private List<MyestdRpTwoLevel> findestdFees(string Session, String Session_id, String Class_id, String Section, String Fee_head_id)
        {
            List<MyestdRpTwoLevel> MyestdRpTwoLevelList = new List<MyestdRpTwoLevel>();
            double ttl_dues = 0;
            int rowcount = -1;
            string qoute = "'";
            string[] stringSeparatorss = new string[] { "月" };
            string[] arr = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                //string parameter = "";
                //string feeid_coma = "";
                //string qry = "select distinct content_id,parameter from Fee_master_content_wise where content='" + value + "'";
                //DataTable dtf = My.dataTable(qry);
                //if (dtf.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dtf.Rows)
                //    {
                //        feeid_coma = feeid_coma + qoute + dr["content_id"].ToString() + qoute + ",";
                //        parameter = dr["parameter"].ToString();
                //    }
                //    feeid_coma = feeid_coma.Remove(feeid_coma.Length - 1);
                //}


                //string query = "select isnull(sum(convert(float, Total_dues)),0) as totalDues from (select t1.admissionserialnumber,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=12 and content in ('" + value + "'))) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + Session_id + "' and Status=1 and t1.Class_id='" + Class_id + "' and t1.Section='" + Section + "') t";
                string query = "SELECT ISNULL(SUM(CAST(d.Total_dues AS FLOAT)), 0) AS totalDues FROM (SELECT t1.admissionserialnumber, SUM(CAST(s.Dues_amt AS FLOAT)) AS Total_dues FROM admission_registor t1 JOIN Add_course_table t2 ON t1.Class_id = t2.course_id JOIN Student_wise_dues_amount s ON s.Session_id = t1.Session_id AND s.Class_id = t1.Class_id AND s.Admission_no = t1.admissionserialnumber AND s.Month_position <= 12 AND s.content IN ('" + value + "') WHERE t1.Session_id = '" + Session_id + "' AND t1.Class_id = '" + Class_id + "' AND t1.Section = '" + Section + "' AND t1.Status = 1 GROUP BY t1.admissionserialnumber) d";
                SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Exam_Subject_Sub_Level");
                DataTable dt = ds.Tables[0];
                int rowcount1 = dt.Rows.Count;
                if (rowcount1 > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ttl_dues = ttl_dues + My.toDouble(dr["totalDues"].ToString());
                        MyestdRpTwoLevelList.Add(new MyestdRpTwoLevel
                        {
                            AmountssS = dr["totalDues"].ToString(),
                            Total_dues = ttl_dues.ToString(),
                            Rowcount = rowcount.ToString(),
                        });
                        rowcount++;
                    }
                }
            }

            return MyestdRpTwoLevelList;
        }

        public class MyReporDuesFeeFooter
        {
            public string AmountssS { get; set; }
        }
        List<MyReporDuesFeeFooter> MyReporDuesFeeFooterItem = new List<MyReporDuesFeeFooter>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_total_dues_footer(string Session, string Session_id, string Class_id, string Fee_head_id)
        {
            string qoute = "'"; 
            string classid_coma = "";
            string[] stringSeparatorss = new string[] { "," };
            string[] arr = Class_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                classid_coma = classid_coma + qoute + value + qoute + ",";
            }
            classid_coma = classid_coma.Remove(classid_coma.Length - 1);
            //=========================
            string sectionS = "";
            string qryS = "select distinct Section,t1.Class_id from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + Session_id + "' and t1.Class_id in (" + classid_coma + ") and Section not in ('NA') order by Section asc";
            DataTable dtS = My.dataTable(qryS);
            if (dtS.Rows.Count > 0)
            {
                foreach (DataRow drS in dtS.Rows)
                {
                    sectionS = sectionS + qoute + drS["Section"].ToString() + qoute + ",";
                }
                sectionS = sectionS.Remove(sectionS.Length - 1);
            }

            string[] stringSeparator = new string[] { "月" };
            string[] arrs = Fee_head_id.Split(stringSeparator, StringSplitOptions.None);
            foreach (string valueFee in arrs)
            {
                string query = "SELECT ISNULL(SUM(CONVERT(float, d.Total_dues)), 0) AS totalDues FROM (SELECT t1.admissionserialnumber,SUM(CONVERT(float, d.Dues_amt)) AS Total_dues FROM admission_registor t1 JOIN Add_course_table t2 ON t1.Class_id = t2.course_id JOIN Student_wise_dues_amount d ON d.Session_id = t1.Session_id AND d.Class_id = t1.Class_id AND d.Admission_no = t1.admissionserialnumber AND d.Month_position <= 12  AND d.content = '" + valueFee + "' WHERE t1.Session_id = '" + Session_id + "' AND t1.Status = 1 AND t1.Class_id IN (" + classid_coma + ")  AND t1.Section in (" + sectionS + ") GROUP BY t1.admissionserialnumber) d";
                SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "admission_registor");
                DataTable dt = ds.Tables[0];
                int rowcount1 = dt.Rows.Count;
                if (rowcount1 > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MyReporDuesFeeFooterItem.Add(new MyReporDuesFeeFooter
                        {
                            AmountssS = dr["totalDues"].ToString(),
                        });
                    }
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(MyReporDuesFeeFooterItem));
        }
    }
}
