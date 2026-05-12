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
    /// Summary description for estimate_fee
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class estimate_fee : System.Web.Services.WebService
    {
        My mycode = new My();
        public class MyReporFeeHead
        {
            public string ContentName { get; set; }
        }

        //月

        List<MyReporFeeHead> MyReporFeeHeadItem = new List<MyReporFeeHead>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_estimate_head(string Session_id, string Months, string Class_id, string Fee_head_id)
        {
            string qoute = "'";
            string feeid = "";
            string[] stringSeparatorss = new string[] { "," };
            string[] arr = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                feeid = feeid + qoute + value + qoute + ",";
            }
            feeid = feeid.Remove(feeid.Length - 1);
            string qry = "select distinct content,content_id from Fee_master_content_wise where content_id in (" + feeid + ") order by content asc";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MyReporFeeHeadItem.Add(new MyReporFeeHead
                    {
                        ContentName = dr["content"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyReporFeeHeadItem));
            }
        }

        public class MyReporFeeHeadSec
        {
            public string ContentNameFS { get; set; }
        }
        List<MyReporFeeHeadSec> MyReporFeeHeadSecItem = new List<MyReporFeeHeadSec>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_estimate_head_sec(string Session_id, string Months, string Class_id, string Fee_head_id)
        {
            string[] stringSeparatorss = new string[] { "," };
            string[] arr = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                string feesc = "Fee,Discount,Payable Fees";
                string[] arrs = feesc.Split(stringSeparatorss, StringSplitOptions.None);
                foreach (string values in arrs)
                {
                    MyReporFeeHeadSecItem.Add(new MyReporFeeHeadSec
                    {
                        ContentNameFS = values,
                    });
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(MyReporFeeHeadSecItem));
        }





        public class MyReporEstdFee
        {
            public string Class_name { get; set; }
            public string Section { get; set; }
            public List<MyestdRpTwoLevel> MyestdRpTwoLevelList { get; set; }
        }

        public class MyestdRpTwoLevel
        {
            public string AmountssS { get; set; }
        }
        List<MyReporEstdFee> MyReporEstdFeeItem = new List<MyReporEstdFee>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_estimate_fees(string Session_id, string Months, string Class_id, string Fee_head_id)
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
            //string[] arrs = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            //foreach (string value in arr)
            //{
            //    classid_coma = classid_coma + qoute + value + qoute + ",";
            //}
            //classid_coma = classid_coma.Remove(classid_coma.Length - 1);





            string qry = "select Position,Session_id,Class_id,session,class,Section from (select t2.Position,t1.Session_id,t1.Class_id,t1.session,t1.admissionserialnumber,t1.class,t1.Section,t1.studentname from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + Session_id + "' and t1.Status=1 and t1.Class_id in (" + classid_coma + ") and t1.Section not in ('NA')) t group by Position,Session_id,Class_id,session,class,Section order by Position,Section asc";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyestdRpTwoLevel> MBdetails = findestdFees(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Section"].ToString(), Months, Fee_head_id);
                    MyReporEstdFeeItem.Add(new MyReporEstdFee
                    {
                        Class_name = dr["class"].ToString(),
                        Section = dr["Section"].ToString(),
                        MyestdRpTwoLevelList = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyReporEstdFeeItem));
            }
        }

        private List<MyestdRpTwoLevel> findestdFees(string Session_id, string Class_id, string Section, string Months, string Fee_head_id)
        {
            List<MyestdRpTwoLevel> MyestdRpTwoLevelList = new List<MyestdRpTwoLevel>();
            string feesid_coma = "";
            string[] stringSeparatorss = new string[] { "," };
            string[] arr = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                string query = "select isnull(sum(convert(float, FeeAmt)),0) as TotalFee,isnull(sum(convert(float, disc_amount)),0) as TotalDisc,(isnull(sum(convert(float, FeeAmt)),0)-isnull(sum(convert(float, disc_amount)),0)) as Payable_Fee from (select t2.Position,t1.Session_id,t1.Class_id,t1.session,t1.admissionserialnumber,t1.class,t1.Section,t1.studentname,isnull((select top 1 amount from Fee_master_content_wise where session_id=t1.Session_id and class_id=t1.Class_id and content_id='" + value + "' and Month='" + Months + "'),0) as FeeAmt,isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no=t1.admissionserialnumber and month='" + Months + "' and session_id=t1.session_id and fee_head_id='" + value + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id=t1.class_id and admission_no='All' and month='" + Months + "' and session_id=t1.session_id and fee_head_id='" + value + "' and category_id=t1.Category_id AND sub_category_id=t1.SubCategory_id))),0) disc_amount from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + Session_id + "' and Status=1 and Class_id='" + Class_id + "' and Section='" + Section + "') t";
                SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Exam_Subject_Sub_Level");
                DataTable dt = ds.Tables[0];
                int rowcount1 = dt.Rows.Count;
                if (rowcount1 > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MyestdRpTwoLevelList.Add(new MyestdRpTwoLevel
                        {
                            AmountssS = dr["TotalFee"].ToString(),
                        });
                        MyestdRpTwoLevelList.Add(new MyestdRpTwoLevel
                        {
                            AmountssS = dr["TotalDisc"].ToString(),
                        });
                        MyestdRpTwoLevelList.Add(new MyestdRpTwoLevel
                        {
                            AmountssS = dr["Payable_Fee"].ToString(),
                        });
                    }
                }
            }

            return MyestdRpTwoLevelList;
        }




        ////====================================================
        ///
        public class MyReporEstdFeeFooter
        {
            public string AmountssS { get; set; } 
        }

        List<MyReporEstdFeeFooter> MyReporEstdFeeFooterItem = new List<MyReporEstdFeeFooter>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_estimate_total_footer(string Session_id, string Months, string Class_id, string Fee_head_id)
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

            string[] arrs = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string valueFee in arrs)
            {
                string query = "select isnull(sum(convert(float, FeeAmt)),0) as TotalFee,isnull(sum(convert(float, disc_amount)),0) as TotalDisc,(isnull(sum(convert(float, FeeAmt)),0)-isnull(sum(convert(float, disc_amount)),0)) as Payable_Fee from (select t2.Position,t1.Session_id,t1.Class_id,t1.session,t1.admissionserialnumber,t1.class,t1.Section,t1.studentname,isnull((select top 1 amount from Fee_master_content_wise where session_id=t1.Session_id and class_id=t1.Class_id and content_id='" + valueFee + "' and Month='" + Months + "'),0) as FeeAmt,isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no=t1.admissionserialnumber and month='" + Months + "' and session_id=t1.session_id and fee_head_id='" + valueFee + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id=t1.class_id and admission_no='All' and month='" + Months + "' and session_id=t1.session_id and fee_head_id='" + valueFee + "' and category_id=t1.Category_id AND sub_category_id=t1.SubCategory_id))),0) disc_amount from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + Session_id + "' and Status=1 and Class_id in (" + classid_coma + ") and t1.Section not in ('NA')) t";
                SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Exam_Subject_Sub_Level");
                DataTable dt = ds.Tables[0];
                int rowcount1 = dt.Rows.Count;
                if (rowcount1 > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MyReporEstdFeeFooterItem.Add(new MyReporEstdFeeFooter
                        {
                            AmountssS = dr["TotalFee"].ToString(),
                        });
                        MyReporEstdFeeFooterItem.Add(new MyReporEstdFeeFooter
                        {
                            AmountssS = dr["TotalDisc"].ToString(),
                        });
                        MyReporEstdFeeFooterItem.Add(new MyReporEstdFeeFooter
                        {
                            AmountssS = dr["Payable_Fee"].ToString(),
                        });
                    } 
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(MyReporEstdFeeFooterItem));
        } 
    }
}
