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
    /// Summary description for consolidateReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class consolidateReport : System.Web.Services.WebService
    {
        public class MyReporConsolidate
        {
            public string Session_name { get; set; }
            public string Session_id { get; set; }
            public List<MyReporConsolFeeHd> MyReporConsolFeeHdList { get; set; }
            public List<MyReporConsolidateheadType> MyReporConsolidateheadTypeList { get; set; }
            public List<MyReporToatalRp> MyReporToatalRpList { get; set; }
        }
        public class MyReporConsolFeeHd
        {
            public string Content { get; set; }
        }
        public class MyReporToatalRp
        {
            public string modewiseAllAmt { get; set; }
        }

        public class MyReporConsolidateheadType
        {
            public string Content { get; set; }
            public string colSpan { get; set; }
            public List<MyReporConsolAmount> MyReporConsolAmountList { get; set; }
        }
        public class MyReporConsolAmount
        {
            public string Modewise_amt { get; set; }
        }
        List<MyReporConsolidate> MyReporConsolidateItem = new List<MyReporConsolidate>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_cnsolidt(string Class_id, string With_mode_type, string From_idate, string To_idate)
        {
            string qoute = "'";
            string Class_ids = "";
            string[] stringSeparatorss = new string[] { "," };
            string[] arr = Class_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                Class_ids = Class_ids + qoute + value + qoute + ",";
            }
            Class_ids = Class_ids.Remove(Class_ids.Length - 1);
            //=====================================================

            string qry = "select distinct Session,Session_id from (select distinct Session,(select top 1 session_id from session_details where Session=Student_Payment_History.Session)  as Session_id from Student_Payment_History where Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct (select top 1 Session from session_details where session_id=Special_fee_collection.Session_id)  as Session,Session_id from Special_fee_collection where Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Session,Session_id from Accumulated_fee_record where Idate>='" + From_idate + "' and Idate<='" + To_idate + "') t order by Session asc";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyReporConsolFeeHd> MBdetailsHd = findestdFeesHd(dr["Session"].ToString(), dr["Session_id"].ToString(), Class_ids, From_idate, To_idate);
                    List<MyReporConsolidateheadType> MBdetails = findestdFees(dr["Session"].ToString(), dr["Session_id"].ToString(), Class_ids, From_idate, To_idate);
                    List<MyReporToatalRp> MBModTtldetails = findestdFeesTotal(dr["Session"].ToString(), dr["Session_id"].ToString(), Class_ids, From_idate, To_idate);
                    MyReporConsolidateItem.Add(new MyReporConsolidate
                    {
                        Session_name = dr["Session"].ToString(),
                        Session_id = dr["Session_id"].ToString(),
                        MyReporConsolFeeHdList = MBdetailsHd,
                        MyReporConsolidateheadTypeList = MBdetails,
                        MyReporToatalRpList = MBModTtldetails,
                    });
                }

                if (dt.Rows.Count > 1)
                {
                    List<MyReporConsolFeeHd> MBdetailsHd = findestdFeesHdAllSession(Class_ids, From_idate, To_idate);
                    List<MyReporConsolidateheadType> MBdetails = findestdFeesAllSession(Class_ids, From_idate, To_idate);
                    List<MyReporToatalRp> MBModTtldetails = findestdFeesTotalAllSession(Class_ids, From_idate, To_idate);
                    MyReporConsolidateItem.Add(new MyReporConsolidate
                    {
                        Session_name = "All Session",
                        Session_id = "All Sesseion",
                        MyReporConsolFeeHdList = MBdetailsHd,
                        MyReporConsolidateheadTypeList = MBdetails,
                        MyReporToatalRpList = MBModTtldetails,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(MyReporConsolidateItem));
            }
        }


        private List<MyReporToatalRp> findestdFeesTotal(string Session, string Session_id, string Class_ids, string From_idate, string To_idate)
        {
            List<MyReporToatalRp> MyReporToatalRpList = new List<MyReporToatalRp>();
            string qryM = "select distinct mode from (select * from (select distinct mode from Student_Payment_History where Session='" + Session + "' and Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Payment_mode as mode from Special_fee_collection where Session_id='" + Session_id + "' and Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ")  union all select distinct Payment_mode as mode from Accumulated_fee_record where Session_id='" + Session_id + "' and Idate>='" + From_idate + "' and Idate<='" + To_idate + "') t) y order by mode asc";
            DataTable dt = My.dataTable(qryM);
            if (dt.Rows.Count > 0)
            {
                double ttlAMts = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    string modewiseAmount = getModewiseAmountOverAll(Session_id, Session, Class_ids, From_idate, To_idate, dr["mode"].ToString());
                    ttlAMts = ttlAMts + My.toDouble(modewiseAmount);
                    MyReporToatalRpList.Add(new MyReporToatalRp
                    {
                        modewiseAllAmt = modewiseAmount,
                    });
                }
                MyReporToatalRpList.Add(new MyReporToatalRp
                {
                    modewiseAllAmt = ttlAMts.ToString(),
                });
            }
            return MyReporToatalRpList;
        }


        private List<MyReporConsolidateheadType> findestdFees(string Session, string Session_id, string Class_ids, string From_idate, string To_idate)
        {
            List<MyReporConsolidateheadType> MyReporConsolidateheadTypeList = new List<MyReporConsolidateheadType>();
            string qry = "select * from (select distinct Content,'1' as Type from Monthly_Fee_Collection_Slip where session='" + Session + "' and class in (" + Class_ids + ") and Idate>='" + From_idate + "' and Idate<='" + To_idate + "' union all select distinct Fee_head,'2' as Type from Special_fee_collection where Session_id='" + Session_id + "' and Class_id in (" + Class_ids + ") and Idate>='" + From_idate + "' and Idate<='" + To_idate + "'  union all select distinct Fee_head,'1' as Type from Accumulated_fee_record where Session_id='" + Session_id + "' and Idate>='" + From_idate + "' and Idate<='" + To_idate + "') t order by Content asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyReporConsolAmount> MBdetails = findestdFeesAmts(Session, Session_id, Class_ids, From_idate, To_idate, dr["Content"].ToString(), dr["Type"].ToString());
                    MyReporConsolidateheadTypeList.Add(new MyReporConsolidateheadType
                    {
                        Content = dr["Content"].ToString(),
                        colSpan = "2",
                        MyReporConsolAmountList = MBdetails,
                    });
                }
            }
            return MyReporConsolidateheadTypeList;
        }

        private string getModewiseAmount(string session, string class_ids, string from_idate, string to_idate, string mode, string Content)
        {
            string amts = "0";
            DataTable dt = My.dataTable("select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + session + "' and Content='" + Content + "' and class in (" + class_ids + ") and Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and slipno in (select Slip_no from Student_Payment_History where session=Monthly_Fee_Collection_Slip.Session and Slip_no=Monthly_Fee_Collection_Slip.slipno and mode='" + mode + "') union all select isnull(sum(convert(float, Amount)),0) as Paid_amt from Accumulated_fee_record where Session='" + session + "' and Fee_head='" + Content + "' and  Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and Payment_mode='" + mode + "') t ");
            if (dt.Rows.Count > 0)
            {
                amts = dt.Rows[0]["Paid_amt"].ToString();
            }
            return amts;
        }

        private string getModewiseAmountOverAll(string Session_id, string session, string class_ids, string from_idate, string to_idate, string mode)
        {
            string amts = "0";
            DataTable dt = My.dataTable("select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where session='" + session + "' and class in (" + class_ids + ") and Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and slipno in (select Slip_no from Student_Payment_History where session=Monthly_Fee_Collection_Slip.Session and Slip_no=Monthly_Fee_Collection_Slip.slipno and mode='" + mode + "') union all select isnull(sum(convert(float, Paid_amount)),0) as Paid_amt from Special_fee_collection where Session_id='" + Session_id + "' and Class_id in (" + class_ids + ") and Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and Payment_mode='" + mode + "'  union all select isnull(sum(convert(float, Amount)),0) as Paid_amt from Accumulated_fee_record where Session_id='" + Session_id + "' and Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and Payment_mode='" + mode + "') t");
            if (dt.Rows.Count > 0)
            {
                amts = dt.Rows[0]["Paid_amt"].ToString();
            }
            return amts;
        }


        ///====================================
        private List<MyReporConsolAmount> findestdFeesAmts(string Session, string Session_id, string Class_ids, string From_idate, string To_idate, string Content, string Type)
        {
            List<MyReporConsolAmount> MyReporConsolAmountList = new List<MyReporConsolAmount>();
            string qryM = "select distinct mode from (select * from (select distinct mode from Student_Payment_History where Session='" + Session + "' and Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Payment_mode as mode from Special_fee_collection where Session_id='" + Session_id + "' and Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Payment_mode as mode from Accumulated_fee_record where Session_id='" + Session_id + "' and  Idate>='" + From_idate + "' and Idate<='" + To_idate + "') t) y order by mode asc";
            DataTable dt = My.dataTable(qryM);
            if (dt.Rows.Count > 0)
            {
                double ttlAMts = 0;
                if (Type == "1")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string modewiseAmount = getModewiseAmount(Session, Class_ids, From_idate, To_idate, dr["mode"].ToString(), Content);
                        ttlAMts = ttlAMts + My.toDouble(modewiseAmount);
                        MyReporConsolAmountList.Add(new MyReporConsolAmount
                        {
                            Modewise_amt = modewiseAmount,
                        });
                    }
                    MyReporConsolAmountList.Add(new MyReporConsolAmount
                    {
                        Modewise_amt = ttlAMts.ToString(),
                    });
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string modewiseAmount = getModewiseAmountSPF(Session_id, Class_ids, From_idate, To_idate, dr["mode"].ToString(), Content);
                        ttlAMts = ttlAMts + My.toDouble(modewiseAmount);
                        MyReporConsolAmountList.Add(new MyReporConsolAmount
                        {
                            Modewise_amt = modewiseAmount,
                        });
                    }
                    MyReporConsolAmountList.Add(new MyReporConsolAmount
                    {
                        Modewise_amt = ttlAMts.ToString(),
                    });
                }
            }
            return MyReporConsolAmountList;
        }

        private string getModewiseAmountSPF(string Session_id, string class_ids, string from_idate, string to_idate, string mode, string Content)
        {
            string amts = "0";
            DataTable dt = My.dataTable("select isnull(sum(convert(float, Paid_amount)),0) as Paid_amt from Special_fee_collection where Session_id='" + Session_id + "' and Fee_head='" + Content + "' and Class_id in (" + class_ids + ") and Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and Payment_mode='" + mode + "'");
            if (dt.Rows.Count > 0)
            {
                amts = dt.Rows[0]["Paid_amt"].ToString();
            }
            return amts;
        }


        ///=====================================
        private List<MyReporConsolFeeHd> findestdFeesHd(string Session, string Session_id, string Class_ids, string From_idate, string To_idate)
        {
            List<MyReporConsolFeeHd> MyReporConsolFeeHdList = new List<MyReporConsolFeeHd>();
            string qry = "select distinct mode from (select * from (select distinct mode from Student_Payment_History where Session='" + Session + "' and Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Payment_mode as mode from Special_fee_collection where Session_id='" + Session_id + "' and Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Payment_mode from Accumulated_fee_record where Session_id='" + Session_id + "' and Idate>='" + From_idate + "' and Idate<='" + To_idate + "') t) y order by mode asc";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MyReporConsolFeeHdList.Add(new MyReporConsolFeeHd
                    {
                        Content = dr["mode"].ToString(),
                    });
                }
            }
            return MyReporConsolFeeHdList;
        }



        ///OVERALL SESSION
        private List<MyReporConsolFeeHd> findestdFeesHdAllSession(string Class_ids, string From_idate, string To_idate)
        {
            List<MyReporConsolFeeHd> MyReporConsolFeeHdList = new List<MyReporConsolFeeHd>();
            string qry = "select distinct mode from (select * from (select distinct mode from Student_Payment_History where Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Payment_mode as mode from Special_fee_collection where Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Payment_mode from Accumulated_fee_record where Idate>='" + From_idate + "' and Idate<='" + To_idate + "') t) y order by mode asc";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MyReporConsolFeeHdList.Add(new MyReporConsolFeeHd
                    {
                        Content = dr["mode"].ToString(),
                    });
                }
            } 
            return MyReporConsolFeeHdList;
        }


        private List<MyReporConsolidateheadType> findestdFeesAllSession(string Class_ids, string From_idate, string To_idate)
        {
            List<MyReporConsolidateheadType> MyReporConsolidateheadTypeList = new List<MyReporConsolidateheadType>();
            string qry = "select distinct Content,Type from (select distinct Content,'1' as Type from Monthly_Fee_Collection_Slip where class in (" + Class_ids + ") and Idate>='" + From_idate + "' and Idate<='" + To_idate + "' union all select distinct Fee_head,'2' as Type from Special_fee_collection where Class_id in (" + Class_ids + ") and Idate>='" + From_idate + "' and Idate<='" + To_idate + "'  union all select distinct Fee_head,'1' as Type from Accumulated_fee_record where Idate>='" + From_idate + "' and Idate<='" + To_idate + "') t order by Content asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyReporConsolAmount> MBdetails = findestdFeesAmtsOVERALL(Class_ids, From_idate, To_idate, dr["Content"].ToString(), dr["Type"].ToString());
                    MyReporConsolidateheadTypeList.Add(new MyReporConsolidateheadType
                    {
                        Content = dr["Content"].ToString(),
                        colSpan = "2",
                        MyReporConsolAmountList = MBdetails,
                    });
                }
            }
            return MyReporConsolidateheadTypeList;
        }
        private List<MyReporConsolAmount> findestdFeesAmtsOVERALL(string Class_ids, string From_idate, string To_idate, string Content, string Type)
        {
            List<MyReporConsolAmount> MyReporConsolAmountList = new List<MyReporConsolAmount>();
            string qryM = "select distinct mode from (select distinct mode from Student_Payment_History where Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Payment_mode as mode from Special_fee_collection where Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Payment_mode as mode from Accumulated_fee_record where Idate>='" + From_idate + "' and Idate<='" + To_idate + "') t order by mode asc";
            DataTable dt = My.dataTable(qryM);
            if (dt.Rows.Count > 0)
            {
                double ttlAMts = 0;
                if (Type == "1")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string modewiseAmount = getModewiseAmountOverAllContent(Class_ids, From_idate, To_idate, dr["mode"].ToString(), Content);
                        ttlAMts = ttlAMts + My.toDouble(modewiseAmount);
                        MyReporConsolAmountList.Add(new MyReporConsolAmount
                        {
                            Modewise_amt = modewiseAmount,
                        });
                    }
                    MyReporConsolAmountList.Add(new MyReporConsolAmount
                    {
                        Modewise_amt = ttlAMts.ToString(),
                    });
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string modewiseAmount = getModewiseAmountSPFAllSessn(Class_ids, From_idate, To_idate, dr["mode"].ToString(), Content);
                        ttlAMts = ttlAMts + My.toDouble(modewiseAmount);
                        MyReporConsolAmountList.Add(new MyReporConsolAmount
                        {
                            Modewise_amt = modewiseAmount,
                        });
                    }
                    MyReporConsolAmountList.Add(new MyReporConsolAmount
                    {
                        Modewise_amt = ttlAMts.ToString(),
                    });
                }
            }
            return MyReporConsolAmountList;
        }

        private string getModewiseAmountSPFAllSessn(string class_ids, string from_idate, string to_idate, string mode, string Content)
        {
            string amts = "0";
            DataTable dt = My.dataTable("select isnull(sum(convert(float, Paid_amount)),0) as Paid_amt from Special_fee_collection where Fee_head='" + Content + "' and Class_id in (" + class_ids + ") and Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and Payment_mode='" + mode + "'");
            if (dt.Rows.Count > 0)
            {
                amts = dt.Rows[0]["Paid_amt"].ToString();
            }
            return amts;
        }


        private string getModewiseAmountOverAllContent(string class_ids, string from_idate, string to_idate, string mode, string Content)
        {
            string amts = "0";
            DataTable dt = My.dataTable("select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where Content='" + Content + "' and class in (" + class_ids + ") and Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and slipno in (select Slip_no from Student_Payment_History where session=Monthly_Fee_Collection_Slip.Session and Slip_no=Monthly_Fee_Collection_Slip.slipno and mode='" + mode + "') union all select isnull(sum(convert(float, Amount)),0) as Paid_amt from Accumulated_fee_record where Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and Fee_head='" + Content + "' and Payment_mode='" + mode + "') t ");
            if (dt.Rows.Count > 0)
            {
                amts = dt.Rows[0]["Paid_amt"].ToString();
            }
            return amts;
        }




        private List<MyReporToatalRp> findestdFeesTotalAllSession(string Class_ids, string From_idate, string To_idate)
        {
            List<MyReporToatalRp> MyReporToatalRpList = new List<MyReporToatalRp>();
            string qryM = "select distinct mode from (select * from (select distinct mode from Student_Payment_History where Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ") union all select distinct Payment_mode as mode from Special_fee_collection where Idate>='" + From_idate + "' and Idate<='" + To_idate + "' and Class_id in (" + Class_ids + ")  union all select distinct Payment_mode as mode from Accumulated_fee_record where Idate>='" + From_idate + "' and Idate<='" + To_idate + "') t) y order by mode asc";
            DataTable dt = My.dataTable(qryM);
            if (dt.Rows.Count > 0)
            {
                double ttlAMts = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    string modewiseAmount = getModewiseAmountOverAllAllSession(Class_ids, From_idate, To_idate, dr["mode"].ToString());
                    ttlAMts = ttlAMts + My.toDouble(modewiseAmount);
                    MyReporToatalRpList.Add(new MyReporToatalRp
                    {
                        modewiseAllAmt = modewiseAmount,
                    });
                }
                MyReporToatalRpList.Add(new MyReporToatalRp
                {
                    modewiseAllAmt = ttlAMts.ToString(),
                });
            }
            return MyReporToatalRpList;
        }


        private string getModewiseAmountOverAllAllSession(string class_ids, string from_idate, string to_idate, string mode)
        {
            string amts = "0";
            DataTable dt = My.dataTable("select isnull(sum(convert(float, Paid_amt)),0) as Paid_amt from (select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where class in (" + class_ids + ") and Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and slipno in (select Slip_no from Student_Payment_History where session=Monthly_Fee_Collection_Slip.Session and Slip_no=Monthly_Fee_Collection_Slip.slipno and mode='" + mode + "') union all select isnull(sum(convert(float, Paid_amount)),0) as Paid_amt from Special_fee_collection where Class_id in (" + class_ids + ") and Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and Payment_mode='" + mode + "' union all select isnull(sum(convert(float, Amount)),0) as Paid_amt from Accumulated_fee_record where Idate>='" + from_idate + "' and Idate<='" + to_idate + "' and Payment_mode='" + mode + "') t");
            if (dt.Rows.Count > 0)
            {
                amts = dt.Rows[0]["Paid_amt"].ToString();
            }
            return amts;
        }
    }
}
