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
    /// Summary description for student_report
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class student_report : System.Web.Services.WebService
    {
        public class Fetch_Details_of_day_end_report_head
        {
            public string StdStatus { get; set; }
            public string Session_id { get; set; }
            public string Dateofadmission { get; set; }
            public string Admission_no { get; set; }
            public string Session { get; set; }
            public string Class_name { get; set; }
            public string Class_id { get; set; }
            public string Rollnumber { get; set; }
            public string Studentname { get; set; }
            public string Fathername { get; set; }
            public string Mobilenumber { get; set; }
            public string Section { get; set; }
            public string Payable_amount { get; set; }
            public string Paid_amount { get; set; }
            public string Dues_amount { get; set; }
            public string Payment_link { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head> Show_of_day_end_report_head = new List<Fetch_Details_of_day_end_report_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_of_students(string Session_id, string Class_id, string StdType)
        {
            string query = "";
            if (Class_id == "0")
            {
                if (StdType == "1") //New
                {
                    query = "select * from (select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Admission_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid')  and  ar.Transfer_Status='New' and ar.Status='1' UNION all  select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Annual_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and ar.Transfer_Status='New' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid') and ar.Transfer_Status='NT' and ar.Status='1') t order by class,Section,rollnumber asc";
                }
                else if (StdType == "2")   //OLD
                {
                    query = "select * from (select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Admission_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid')  and  ar.Transfer_Status='New' and ar.Status='1' UNION all  select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Annual_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "'  and ar.Transfer_Status='NT' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid') and ar.Transfer_Status='NT' and ar.Status='1') t order by class,Section,rollnumber asc";
                }
                else  ///ALL
                {
                    query = "select * from (select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Admission_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid')  and  ar.Transfer_Status='New' and ar.Status='1' UNION all  select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Annual_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid') and ar.Transfer_Status='NT' and ar.Status='1') t order by class,Section,rollnumber asc";
                }
            }
            else
            {
                if (StdType == "1") //New
                {
                    query = "select * from (select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Admission_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and ar.Class_id='" + Class_id + "' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid')  and  ar.Transfer_Status='New' and ar.Status='1' UNION all  select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Annual_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and ar.Class_id='" + Class_id + "' and  ar.Transfer_Status='New' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid') and ar.Transfer_Status='NT' and ar.Status='1') t order by class,Section,rollnumber asc";
                }
                else if (StdType == "1") //OLD
                {
                    query = "select * from (select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Admission_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and ar.Class_id='" + Class_id + "' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid')  and  ar.Transfer_Status='New' and ar.Status='1' UNION all  select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Annual_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and ar.Class_id='" + Class_id + "' and ar.Transfer_Status='NT' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid') and ar.Transfer_Status='NT' and ar.Status='1') t order by class,Section,rollnumber asc";
                }
                else //ALL
                {
                    query = "select * from (select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Admission_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and ar.Class_id='" + Class_id + "' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid')  and  ar.Transfer_Status='New' and ar.Status='1' UNION all  select ar.Transfer_Status,ar.Session_id,ar.dateofadmission,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,ar.email_id,ar.Class_id,ad.Slip_no,ad.Payable_amount,ad.Paid_amount,ad.Dues_amount,ar.father_mob from admission_registor ar left join Annual_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + Session_id + "' and ar.Class_id='" + Class_id + "' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid') and ar.Transfer_Status='NT' and ar.Status='1') t order by class,Section,rollnumber asc";
                }

            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Status = "";
                    string payment_link = "";
                    if (dr["Transfer_Status"].ToString() == "NT")
                    {
                        Status = "Old";
                        payment_link = "collect-annual-fees.aspx?admissionno=" + dr["admissionserialnumber"].ToString() + "&sessionid=" + dr["Session_id"].ToString() + "&classid=" + dr["Class_id"].ToString();
                    }
                    else
                    {
                        Status = "New";
                        payment_link = "collect-admission-fees.aspx?admissionno=" + dr["admissionserialnumber"].ToString() + "&sessionid=" + dr["Session_id"].ToString() + "&classid=" + dr["Class_id"].ToString();
                    }
                    Show_of_day_end_report_head.Add(new Fetch_Details_of_day_end_report_head
                    {
                        StdStatus = Status,
                        Session_id = dr["Session_id"].ToString(),
                        Dateofadmission = dr["dateofadmission"].ToString(),
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        Session = dr["session"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Class_id = dr["Class_id"].ToString(),
                        Section = dr["Section"].ToString(),
                        Rollnumber = dr["rollnumber"].ToString(),
                        Studentname = dr["studentname"].ToString(),
                        Fathername = dr["fathername"].ToString(),
                        Mobilenumber = dr["mobilenumber"].ToString(),

                        Payable_amount = dr["Payable_amount"].ToString(),
                        Paid_amount = dr["Paid_amount"].ToString(),
                        Dues_amount = dr["Dues_amount"].ToString(),
                        Payment_link = payment_link,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head));
            }
        }


        ///===========================================
        ///
        public class Fetch_Details_of_housewise_head
        {
            public string house_name { get; set; }
        }

        List<Fetch_Details_of_housewise_head> Show_of_housewise_head = new List<Fetch_Details_of_housewise_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_house_header(string Session, string Session_id, string Class_id, string Section)
        {
            string query = "select * from house_master order by house_name asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_housewise_head.Add(new Fetch_Details_of_housewise_head
                    {
                        house_name = dr["house_name"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_housewise_head));
            }
        }

        /// <summary>
        /// ===================================
        /// </summary>
        public class MyHouseSummary
        {
            public string Course_Name { get; set; }
            public string Class_id { get; set; }
            public string Section { get; set; }
            public List<MyHouseCountDetails> MyHouseCountItem { get; set; }
        }

        public class MyHouseCountDetails
        {
            public string totalStudent { get; set; }
        }

        List<MyHouseSummary> EMyBedBooking = new List<MyHouseSummary>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_classwise_house_summary(string Session, string Session_id, string Class_id, string Section)
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
            string Sections = "";
            string[] arrs = Section.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arrs)
            {
                Sections = Sections + qoute + value + qoute + ",";
            }
            Sections = Sections.Remove(Sections.Length - 1);

            string qry = "SELECT distinct t2.Course_Name,t1.Class_id,t1.Section,t2.Position FROM  admission_registor t1 JOIN  Add_course_table t2 ON t1.Class_id=t2.course_id WHERE t1.Session_id='" + Session_id + "' AND t1.Class_id in (" + Class_ids + ") AND t1.Section in (" + Sections + ") AND t1.Status=1 ORDER BY t2.Position, t1.Section ASC ";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyHouseCountDetails> MBdetails = findmyBedDetails(Session_id, dr["Class_id"].ToString(), dr["Section"].ToString());
                    EMyBedBooking.Add(new MyHouseSummary
                    {
                        Course_Name = dr["Course_Name"].ToString(),
                        Class_id = dr["Class_id"].ToString(),
                        Section = dr["Section"].ToString(),
                        MyHouseCountItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMyBedBooking));
            }
        }


        private List<MyHouseCountDetails> findmyBedDetails(string Session_id, string Class_id, string Section)
        {
            List<MyHouseCountDetails> MyHouseCountItem = new List<MyHouseCountDetails>();
            string qry = "select * from house_master order by house_name asc";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                double ttlStd = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    string no_ofStd = get_no_student(Session_id, Class_id, Section, dr["house_id"].ToString());
                    ttlStd = ttlStd + My.toDouble(no_ofStd);
                    MyHouseCountItem.Add(new MyHouseCountDetails
                    {
                        totalStudent = no_ofStd,
                    });
                }

                MyHouseCountItem.Add(new MyHouseCountDetails
                {
                    totalStudent = ttlStd.ToString(),
                });
            }
            return MyHouseCountItem;
        }

        private string get_no_student(string session_id, string class_id, string section, string house_id)
        {
            string ttlStd = "No";
            string qry = "select count(Id) as Count FROM  admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and house='" + house_id + "' and Status=1";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                ttlStd = dt.Rows[0]["Count"].ToString();
            }
            return ttlStd;
        }



        public class Fetch_Details_of_housewise_total
        {
            public string ttlStd { get; set; }
        }

        List<Fetch_Details_of_housewise_total> Show_of_housewise_total = new List<Fetch_Details_of_housewise_total>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_house_final_total(string Session, string Session_id, string Class_id, string Section)
        {
            string query = "select * from house_master order by house_name asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                double ttlStd = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    string no_ofStd = get_no_student_total(Session_id, Class_id, Section, dr["house_id"].ToString());
                    ttlStd = ttlStd + My.toDouble(no_ofStd);
                    Show_of_housewise_total.Add(new Fetch_Details_of_housewise_total
                    {
                        ttlStd = no_ofStd,
                    });
                }

                Show_of_housewise_total.Add(new Fetch_Details_of_housewise_total
                {
                    ttlStd = ttlStd.ToString(),
                });
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_housewise_total));
            }
        }

        private string get_no_student_total(string session_id, string Class_id, string Section, string house_id)
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
            string Sections = "";
            string[] arrs = Section.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arrs)
            {
                Sections = Sections + qoute + value + qoute + ",";
            }
            Sections = Sections.Remove(Sections.Length - 1);


            string ttlStd = "No";
            string qry = "select count(Id) as Count FROM  admission_registor where Session_id='" + session_id + "' and Class_id in (" + Class_ids + ") AND Section in (" + Sections + ") and house='" + house_id + "' and Status=1";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                ttlStd = dt.Rows[0]["Count"].ToString();
            }
            return ttlStd;
        }
    }
}
