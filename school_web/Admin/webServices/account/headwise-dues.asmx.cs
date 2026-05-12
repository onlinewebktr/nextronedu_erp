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
    /// Summary description for headwise_dues
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class headwise_dues : System.Web.Services.WebService
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
            string[] stringSeparatorss = new string[] { "月" };
            string[] arr = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                feeid = feeid + qoute + value + qoute + ",";
            }
            feeid = feeid.Remove(feeid.Length - 1);
            string qry = "select distinct content from Fee_master_content_wise where content in (" + feeid + ") order by content asc";
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



        public class MyReporEstdFee
        {
            public string Student_name { get; set; }
            public string Father_name { get; set; }
            public string Admission_no { get; set; }
            public string Roll_no { get; set; }
            public string Mobile_no { get; set; }
            public string Class_name { get; set; }
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

            string qry = "select * from (select t2.Position,t1.Session_id,t1.Class_id,t1.session,t1.admissionserialnumber,t1.class,t1.Section,t1.rollnumber,t1.studentname,t1.fathername,t1.mobilenumber,t1.Transfer_Status from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + Session_id + "' and Status=1 and Class_id in (" + classid_coma + ") and t1.Section not in ('NA')) t order by Position,Section,rollnumber asc"; // and t1.admissionserialnumber='6652'
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyestdRpTwoLevel> MBdetails = findestdFees(dr["Transfer_Status"].ToString(), dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString(), Months, Fee_head_id);
                    MyReporEstdFeeItem.Add(new MyReporEstdFee
                    {
                        Student_name = dr["studentname"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Section = dr["Section"].ToString(),

                        Roll_no = dr["rollnumber"].ToString(),
                        Mobile_no = dr["mobilenumber"].ToString(),
                        MyestdRpTwoLevelList = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyReporEstdFeeItem));
            }
        }

        private List<MyestdRpTwoLevel> findestdFees(string Transfer_Status, string Session_id, string Class_id, string admission_no, string Months, string Fee_head_id)
        {
            List<MyestdRpTwoLevel> MyestdRpTwoLevelList = new List<MyestdRpTwoLevel>();
            string contentParameterAdm = "AnnualFee";
            string contentParameterMonth = "MonthlyFee";
            if (Transfer_Status.ToUpper() == "NEW")
            {
                contentParameterAdm = "AdmissionFee";
            }
            double ttl_dues = 0;
            int rowcount = -1;
            string feesid_coma = "";
            string qoute = "'";
            string[] stringSeparatorss = new string[] { "月" };
            string[] arr = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                string parameter = "";
                string feeid_coma = "";
                string qry = "select distinct content_id,parameter from Fee_master_content_wise where content='" + value + "'";
                DataTable dtf = My.dataTable(qry);
                if (dtf.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtf.Rows)
                    {
                        feeid_coma = feeid_coma + qoute + dr["content_id"].ToString() + qoute + ",";
                        parameter = dr["parameter"].ToString();
                    }
                    feeid_coma = feeid_coma.Remove(feeid_coma.Length - 1);
                }


                string query = "select (convert(float, FeeAmt)-(convert(float, disc_amount)+convert(float, PaidFeeAmt))) as Dues_amt from (select t2.Position,t1.Session_id,t1.Class_id,t1.session,t1.admissionserialnumber,t1.class,t1.Section,t1.rollnumber,t1.studentname,t1.fathername,t1.mobilenumber,isnull((select top 1 amount from Fee_master_content_wise where session_id=t1.Session_id and class_id=t1.Class_id and parameter='" + contentParameterAdm + "' and content_id in (" + feeid_coma + ")),0) as FeeAmt,isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no=t1.admissionserialnumber and session_id=t1.session_id and fee_head_id in (" + feeid_coma + ")),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id=t1.class_id and admission_no='All' and session_id=t1.session_id and fee_head_id in (" + feeid_coma + ") and category_id=t1.Category_id AND sub_category_id=t1.SubCategory_id))),0) disc_amount,isnull((select top 1 paid from Typewise_fee_collection where session=t1.session and class_id=t1.Class_id and admission_no=t1.admissionserialnumber and content_id in (" + feeid_coma + ")),0) as PaidFeeAmt from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + Session_id + "' and t1.Status=1 and t1.Class_id='" + Class_id + "' and t1.admissionserialnumber='" + admission_no + "') t";
                if (parameter == "MonthlyFee" || parameter == "HostelMonthlyFee")
                {
                    query = "select (convert(float, FeeAmt)-(convert(float, disc_amount)+convert(float, PaidFeeAmt))) as Dues_amt from (select t2.Position,t1.Session_id,t1.Class_id,t1.session,t1.admissionserialnumber,t1.class,t1.Section,t1.rollnumber,t1.studentname,t1.fathername,t1.mobilenumber,isnull((select top 1 amount from Fee_master_content_wise where session_id=t1.Session_id and class_id=t1.Class_id and content_id in (" + feeid_coma + ") and Month='" + Months + "'),0) as FeeAmt,isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no=t1.admissionserialnumber and month='" + Months + "' and session_id=t1.session_id and fee_head_id in (" + feeid_coma + ")),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id=t1.class_id and admission_no='All' and month='" + Months + "' and session_id=t1.session_id and fee_head_id in (" + feeid_coma + ") and category_id=t1.Category_id AND sub_category_id=t1.SubCategory_id))),0) disc_amount,isnull((select top 1 paid from Typewise_fee_collection where session=t1.session and class_id=t1.Class_id and admission_no=t1.admissionserialnumber and content_id in (" + feeid_coma + ") and Month='" + Months + "'),0) as PaidFeeAmt from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + Session_id + "' and t1.Status=1 and t1.Class_id='" + Class_id + "' and t1.admissionserialnumber='" + admission_no + "') t";
                }
                SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Exam_Subject_Sub_Level");
                DataTable dt = ds.Tables[0];
                int rowcount1 = dt.Rows.Count;
                if (rowcount1 > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ttl_dues = ttl_dues + My.toDouble(dr["Dues_amt"].ToString());
                        MyestdRpTwoLevelList.Add(new MyestdRpTwoLevel
                        {
                            AmountssS = dr["Dues_amt"].ToString(),
                            Total_dues = ttl_dues.ToString(),
                            Rowcount = rowcount.ToString(),
                        });
                        rowcount++;
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


        /////////////////////////////////COLLECTION HEADWISE
        ///
        public class MyReporCollectionFeeHead
        {
            public string ContentName { get; set; }
        }

        //月

        List<MyReporCollectionFeeHead> MyReporCollectionFeeHeadItem = new List<MyReporCollectionFeeHead>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_collection_head(string Session, string Session_id, string Class_id, string Fee_head_id, string From_idate, string To_idate)
        {
            string qoute = "'";
            string feeid = "";
            string[] stringSeparatorss = new string[] { "月" };
            string[] arr = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                feeid = feeid + qoute + value + qoute + ",";
            }
            feeid = feeid.Remove(feeid.Length - 1);
            string qry = "select distinct Content from Monthly_Fee_Collection_Slip where session='" + Session + "' and  Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Content in (" + feeid + ") order by Content asc";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MyReporCollectionFeeHeadItem.Add(new MyReporCollectionFeeHead
                    {
                        ContentName = dr["content"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyReporCollectionFeeHeadItem));
            }
        }



        public class MyReporCollectionFee
        {
            public string className { get; set; }
            public string Section { get; set; }
            public List<MyCollectiondRpTwoLevel> MyCollectiondRpTwoLevelList { get; set; }
        }

        public class MyCollectiondRpTwoLevel
        {
            public string Paid_amt { get; set; }
            public string Total_paid_amt { get; set; }
            public string Rowcount { get; set; }
        }
        List<MyReporCollectionFee> MyReporCollectionFeeItem = new List<MyReporCollectionFee>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_collection_fees(string Session, string Session_id, string Class_id, string Fee_head_id, string From_idate, string To_idate)
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
                    List<MyCollectiondRpTwoLevel> MBdetails = findCollectFees(Session, Session_id, dr["Class_id"].ToString(), dr["Section"].ToString(), Fee_head_id, From_idate, To_idate);
                    MyReporCollectionFeeItem.Add(new MyReporCollectionFee
                    {
                        className = dr["class"].ToString(),
                        Section = dr["Section"].ToString(),
                        MyCollectiondRpTwoLevelList = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyReporCollectionFeeItem));
            }
        }

        private List<MyCollectiondRpTwoLevel> findCollectFees(string Session, String Session_id, String Class_id, String Section, String Fee_head_id, String From_idate, String To_idate)
        {
            List<MyCollectiondRpTwoLevel> MyCollectiondRpTwoLevelList = new List<MyCollectiondRpTwoLevel>();
            double ttl_paid = 0;
            int rowcount = -1;
            string[] stringSeparatorss = new string[] { "月" };
            string[] arr = Fee_head_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                string query = "select isnull(sum(convert(float, paid)),0) as Paid_amt  from (select adno,paid from Monthly_Fee_Collection_Slip where session='" + Session + "' and Content='" + value + "' and class='" + Class_id + "' and Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and adno in (select admissionserialnumber from admission_registor where Session_id='" + Session_id + "' and admissionserialnumber=Monthly_Fee_Collection_Slip.adno and Section='" + Section + "'))t";
                SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Monthly_Fee_Collection_Slip");
                DataTable dt = ds.Tables[0];
                int rowcount1 = dt.Rows.Count;
                if (rowcount1 > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ttl_paid = ttl_paid + My.toDouble(dr["Paid_amt"].ToString());
                        MyCollectiondRpTwoLevelList.Add(new MyCollectiondRpTwoLevel
                        {
                            Paid_amt = dr["Paid_amt"].ToString(),
                            Total_paid_amt = ttl_paid.ToString(),
                            Rowcount = rowcount.ToString(),
                        });
                        rowcount++;
                    }
                }
            }

            return MyCollectiondRpTwoLevelList;
        }



        public class MyReporCollectedFeeFooter
        {
            public string AmountssS { get; set; }
        } 
        List<MyReporCollectedFeeFooter> MyReporCollectedFeeFooterItem = new List<MyReporCollectedFeeFooter>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_total_footer(string Session, string Session_id, string Class_id, string Fee_head_id, string From_idate, string To_idate)
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
            string[] stringSeparator = new string[] { "月" };
            string[] arrs = Fee_head_id.Split(stringSeparator, StringSplitOptions.None);
            foreach (string valueFee in arrs)
            {
                string query = "select isnull(sum(convert(float, paid)),0) as Paid_amt from (select adno,paid from Monthly_Fee_Collection_Slip where session='" + Session + "' and Content='" + valueFee + "' and class in ("+ classid_coma + ") and Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and adno in (select admissionserialnumber from admission_registor where Session_id='" + Session_id + "' and admissionserialnumber=Monthly_Fee_Collection_Slip.adno)) t";

                //string query = "select isnull(sum(convert(float, FeeAmt)),0) as TotalFee,isnull(sum(convert(float, disc_amount)),0) as TotalDisc,(isnull(sum(convert(float, FeeAmt)),0)-isnull(sum(convert(float, disc_amount)),0)) as Payable_Fee from (select t2.Position,t1.Session_id,t1.Class_id,t1.session,t1.admissionserialnumber,t1.class,t1.Section,t1.studentname,isnull((select top 1 amount from Fee_master_content_wise where session_id=t1.Session_id and class_id=t1.Class_id and content_id='" + valueFee + "' and Month='" + Months + "'),0) as FeeAmt,isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no=t1.admissionserialnumber and month='" + Months + "' and session_id=t1.session_id and fee_head_id='" + valueFee + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id=t1.class_id and admission_no='All' and month='" + Months + "' and session_id=t1.session_id and fee_head_id='" + valueFee + "' and category_id=t1.Category_id AND sub_category_id=t1.SubCategory_id))),0) disc_amount from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + Session_id + "' and Status=1 and Class_id in (" + classid_coma + ") and t1.Section not in ('NA')) t";
                SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Exam_Subject_Sub_Level");
                DataTable dt = ds.Tables[0];
                int rowcount1 = dt.Rows.Count;
                if (rowcount1 > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MyReporCollectedFeeFooterItem.Add(new MyReporCollectedFeeFooter
                        {
                            AmountssS = dr["Paid_amt"].ToString(),
                        }); 
                    }
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(MyReporCollectedFeeFooterItem));
        }

    }
}
