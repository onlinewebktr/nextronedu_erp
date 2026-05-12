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
    /// Summary description for overall_collection
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class overall_collection : System.Web.Services.WebService
    {
        public class Fetch_fee_details
        {
            public string Content { get; set; }
            public string Paid_amount { get; set; }
            public string Ttl_amount { get; set; }
            public string Roucounts { get; set; }
        }
        List<Fetch_fee_details> Show_fee_details = new List<Fetch_fee_details>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_overall_fee_collection(string Class_id, string FromDate, string ToDate)
        {
            string qoute = "'";
            string Class_ids = "";
            string[] stringSeparatorss = new string[] { "月" };
            string[] arr = Class_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                Class_ids = Class_ids + qoute + value + qoute + ",";
            }
            Class_ids = Class_ids.Remove(Class_ids.Length - 1);


            string qry = "";
            int fromIdate = My.DateConvertToIdate(FromDate);
            int toIdate = My.DateConvertToIdate(ToDate);
            qry = "select distinct Content,isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where  class in (" + Class_ids + ") and Idate>='" + fromIdate + "' and Idate<='" + toIdate + "' and convert(float, paid) >0 group by Content order by Content asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                double ttl_amount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ttl_amount = ttl_amount + My.toDouble(dr["Paid_amt"].ToString());
                    Show_fee_details.Add(new Fetch_fee_details
                    {
                        Content = dr["Content"].ToString(),
                        Paid_amount = dr["Paid_amt"].ToString(),
                        Ttl_amount = ttl_amount.ToString(),
                        Roucounts = (rowcount1 - 1).ToString()
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_fee_details));
            }
        }


        public class Fetch_fee_details_by_mode
        {
            public string Content { get; set; }
            public string Paid_amount { get; set; }
            public string Ttl_amount { get; set; }
            public string Roucounts { get; set; }
        }
        List<Fetch_fee_details_by_mode> Show_fee_details_by_mode = new List<Fetch_fee_details_by_mode>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_overall_fee_collection_by_mode(string Class_id, string FromDate, string ToDate)
        {
            string qoute = "'";
            string Class_ids = "";
            string[] stringSeparatorss = new string[] { "月" };
            string[] arr = Class_id.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                Class_ids = Class_ids + qoute + value + qoute + ",";
            }
            Class_ids = Class_ids.Remove(Class_ids.Length - 1);


            int fromIdate = My.DateConvertToIdate(FromDate);
            int toIdate = My.DateConvertToIdate(ToDate);
            string qry = "select Payment_mode,isnull(sum(convert(float, paid)),0) as Paid_amt from (select *,(select top 1 mode from Student_Payment_History where Slip_no=Monthly_Fee_Collection_Slip.slipno) as Payment_mode from Monthly_Fee_Collection_Slip where  class in (" + Class_ids + ") and Idate>='" + fromIdate + "' and Idate<='" + toIdate + "' and convert(float, paid) >0)t group by Payment_mode order by Payment_mode asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                double ttl_amount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ttl_amount = ttl_amount + My.toDouble(dr["Paid_amt"].ToString());
                    Show_fee_details_by_mode.Add(new Fetch_fee_details_by_mode
                    {
                        Content = dr["Payment_mode"].ToString(),
                        Paid_amount = dr["Paid_amt"].ToString(),
                        Ttl_amount = ttl_amount.ToString(),
                        Roucounts = (rowcount1 - 1).ToString()
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_fee_details_by_mode));
            }
        }


        ///================================
        ///
        public class Student_payment_details
        {
            public string Admission_no { get; set; }
            public string Session { get; set; }
            public string Class_name { get; set; }
            public string Student_name { get; set; }
            public string Section { get; set; }
            public string Roll_no { get; set; }
            public string Father_name { get; set; }
            public string Mobile_no { get; set; }
            public string DuesFee { get; set; }
            public string Ttl_paid { get; set; }
            public string Ttl_amt { get; set; }
            public string Roucounts { get; set; }
            public string Final_payable { get; set; }
            public string Final_paid { get; set; }
            public string Final_dues { get; set; }

            // public List<Student_payment_details_dt> Student_payment_details_dtItem { get; set; }
        }

        //public class Student_payment_details_dt
        //{
        //    public string Sheet_No { get; set; }
        //    public string Sheet_Id { get; set; } 
        //}

        List<Student_payment_details> EMyBedBooking = new List<Student_payment_details>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_student_paid_fee(string Session_id, string Class_id, string Section, string Session_name)
        {
            string qry = "";
            if (Class_id == "0" && Section == "ALL")
            {
                qry = "select * from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + Session_id + "' order by ct.Position,rollnumber asc";
            }
            else if (Class_id != "0" && Section == "ALL")
            {
                qry = "select * from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' order by ct.Position,rollnumber asc";
            }
            else
            {
                qry = "select * from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Section='" + Section + "' order by ct.Position,rollnumber asc";
            }
            DataTable dt = My.dataTable(qry);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                double final_payable = 0; double final_paid = 0; double final_dues = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    string DuesFees = get_fee_details(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString());
                    string ttl_paid = get_total_paid_amt(dr["session"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString());
                    double ttl_amt = My.toDouble(ttl_paid) + My.toDouble(DuesFees);

                    final_payable = final_payable + ttl_amt;
                    final_paid = final_paid + My.toDouble(ttl_paid);
                    final_dues = final_dues + My.toDouble(DuesFees);


                    //string[] stringSeparatorss = new string[] { "/" };
                    //string[] arrs = seat_details.Split(stringSeparatorss, StringSplitOptions.None);
                    //string occupied = arrs[0];
                    //string vaccant = arrs[1];
                    //List<Student_payment_details_dt> MBdetails = findmyBedDetails(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString());
                    EMyBedBooking.Add(new Student_payment_details
                    {
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        Session = dr["session"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Student_name = dr["studentname"].ToString(),
                        Section = dr["Section"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Mobile_no = dr["mobilenumber"].ToString(),
                        DuesFee = DuesFees,
                        Ttl_paid = ttl_paid,
                        Ttl_amt = ttl_amt.ToString(),
                        Roucounts = (rowcount - 1).ToString(),

                        Final_payable = final_payable.ToString(),
                        Final_paid = final_paid.ToString(),
                        Final_dues = final_dues.ToString(),

                        //Student_payment_details_dtItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMyBedBooking));
            }
        }

        private string get_total_paid_amt(string Session, string Class_id, string admission_no)
        {
            string ttl_paid_amt = "0";
            DataTable dt = My.dataTable("select isnull(sum(convert(float, Amount)),0) as Paid_fee from Student_Payment_History where Session='" + Session + "' and Class_id='" + Class_id + "' and Addmission_no='" + admission_no + "' and Type='Monthly'");
            if (dt.Rows.Count > 0)
            {
                ttl_paid_amt = dt.Rows[0]["Paid_fee"].ToString();
            }
            return ttl_paid_amt;
        }

        private string get_fee_details(string Session_id, string Class_id, string admission_no)
        {
            My mycode = new My(); double totaldues_monthley = 0;
            string total_fee = "0"; string paid_fee = "0"; string dues_fee = "0";
            string query = "Select * from admission_registor where Session_id='" + Session_id + "' and Status='1' and class_id='" + Class_id + "' and admissionserialnumber='" + admission_no + "'";
            DataTable dt1 = My.dataTable(query);
            if (dt1.Rows.Count == 0)
            { }
            else
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string admissionserialnumber = dt1.Rows[i]["admissionserialnumber"].ToString();
                    string studentname = dt1.Rows[i]["studentname"].ToString();
                    string Section = dt1.Rows[i]["Section"].ToString();
                    string rollnumber = dt1.Rows[i]["rollnumber"].ToString();
                    string classdata = dt1.Rows[i]["class"].ToString();
                    string session = dt1.Rows[i]["session"].ToString();
                    string hostaltaken = "";
                    if (dt1.Rows[i]["hosteltaken"].ToString() == "")
                    {
                        hostaltaken = "No";
                    }
                    else if (dt1.Rows[i]["hosteltaken"].ToString().ToLower() == "no")
                    {
                        hostaltaken = "No";
                    }
                    else if (dt1.Rows[i]["hosteltaken"].ToString().ToLower() == "yes")
                    {
                        hostaltaken = "Yes";
                    }
                    else
                    {
                        hostaltaken = "No";
                    }
                    string hosteltaken = hostaltaken;

                    string Transfer_Status = "";
                    if (dt1.Rows[i]["Transfer_Status"].ToString() == "New")
                    {
                        Transfer_Status = "New";
                    }
                    else
                    {
                        Transfer_Status = "NT";
                    }


                    string transportationtaken = dt1.Rows[i]["transportationtaken"].ToString();
                    bool day_bording = My.toBool(dt1.Rows[i]["is_applied_dayboarding"].ToString());
                    bool day_bording_with_lunch = My.toBool(dt1.Rows[i]["day_boarding_with_lunch"].ToString());

                    string category_id = dt1.Rows[i]["category_id"].ToString();
                    string SubCategory_id = dt1.Rows[i]["SubCategory_id"].ToString();
                    string Transportation_Id = dt1.Rows[i]["Transportation_Id"].ToString();



                    Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(dt1.Rows[i]["Session_id"].ToString(), dt1.Rows[i]["Class_id"].ToString(), admissionserialnumber);
                    string Transport_id = (String)dc2["Transport_id"];
                    string TransportPath_id = (String)dc2["TransportPath_id"];
                    string Boarding_Point_id = (String)dc2["Boarding_Point_id"];
                    string Transport_Assigned_Id = (String)dc2["Transport_Assigned_Id"];
                    string Month_name = (String)dc2["Month_name"];
                    string Month_id = (String)dc2["Month_id"];
                    string Year_month = (String)dc2["Year_month"];
                    string Sheet_Id = (String)dc2["Sheet_Id"];


                    Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(dt1.Rows[i]["Session_id"].ToString(), dt1.Rows[i]["Class_id"].ToString(), admissionserialnumber);
                    string Hostel_id = (String)dc1["Hostel_id"];
                    string Room_Category_id = (String)dc1["Room_Category_id"];
                    string From_month_name = (String)dc1["From_month_name"];
                    string From_month_id = (String)dc1["From_month_id"];
                    string Assined_Year_Month = (String)dc1["Assined_Year_Month"];
                    string Hostel_assign_id = (String)dc1["Hostel_assign_id"];

                    string Branch_id = dt1.Rows[i]["Branch_id"].ToString();

                    string IsBoarding = "0";
                    string parameteridS = "4";
                    string LunchMnthName = "";
                    string LunchMnthId = "";

                    string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + Session_id + "' and Admission_no='" + admissionserialnumber + "' and Class_id='" + Class_id + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        LunchMnthName = dts.Rows[0]["Month_name"].ToString();
                        LunchMnthId = dts.Rows[0]["Month_id"].ToString();
                        IsBoarding = "1";
                    }
                    totaldues_monthley = find_monthley_dues(Session_id, hosteltaken, admissionserialnumber, session, Class_id, Transfer_Status, classdata, Hostel_id, transportationtaken, day_bording, day_bording_with_lunch, category_id, SubCategory_id, Transportation_Id, Section, Branch_id, Room_Category_id, Hostel_assign_id, IsBoarding, LunchMnthId, parameteridS, TransportPath_id, Transport_id, Boarding_Point_id);
                }
            }
            return totaldues_monthley.ToString();
        }

        private double find_monthley_dues(string session_id, string hosteltaken, string regid, string session, string class_id, string transfer_Status, string classname, string Hostel_id, string transportationtaken, bool day_bording, bool day_bording_with_lunch, string category_id, string SubCategory_id, string Transportation_Id, string Section, string Branch_id, string Room_Category_id, string Hostel_assign_id, string IsBoarding, string LunchMnthId, string parameteridS, string TransportPath_id, string Transport_id, string Boarding_Point_id)
        {
            My.exeSql("delete from Typewise_fee_collection_temp_dues_calculation where admission_no='" + regid + "' and session='" + session + "'");
            My mycode = new My();
            string parameter = ""; string parameter2 = "MonthlyFee"; string parameter_id = ""; string Discount_on = "";
            if (transfer_Status == "New")
            {
                parameter = hosteltaken.ToUpper() == "NO" ? "MonthlyFee" : "HostelMonthlyFee";
                if (parameter == "MonthlyFee")
                {
                    parameter2 = "HostelMonthlyFee";
                }
                Discount_on = "Admission";
                parameter_id = hosteltaken.ToUpper() == "NO" ? "1" : "5";
            }
            else
            {
                parameter = hosteltaken.ToUpper() == "NO" ? "MonthlyFee" : "HostelMonthlyFee";
                if (parameter == "MonthlyFee")
                {
                    parameter2 = "HostelMonthlyFee";
                }
                parameter_id = hosteltaken.ToUpper() == "NO" ? "2" : "6";
                Discount_on = "Annual";
            }

            double total = 0;
            List<string> month_lst = new List<string>();
            string slipno = "", entry_id = "";
            DataTable dt = mycode.FillData("Select Month,Month_Id from Month_Index order by Position asc ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int iS = 0; iS < dt.Rows.Count; iS++)
                {
                    DataTable feedt = new DataTable();
                    string monthname = dt.Rows[iS]["Month"].ToString();
                    if (IsBoarding == "1")
                    {
                        int mnthids = My.tomonth_number(monthname);
                        if (My.toint(LunchMnthId) <= mnthids)
                        {
                            parameteridS = "44";
                        }
                        else
                        {
                            parameteridS = "4";
                        }
                    }


                    string type = "";
                    month_lst.Add(monthname);
                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + regid + "' and session='" + session + "' and month='" + monthname + "' and (parameter='" + parameter + "' or parameter='" + parameter2 + "')").Rows.Count > 0)
                    {
                        feedt = My.dataTable("select id, admission_no,class, session,parameter, feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + regid + "' and session='" + session + "' and month='" + monthname + "' and (parameter='" + parameter + "' or parameter='" + parameter2 + "') and content_id!='6121' and status='Dues'");
                        type = "Calculated";
                        if (feedt.Rows.Count > 0)
                        {
                            send_in_typewise_fee(feedt, Section, regid, monthname, Branch_id, session_id);
                        }
                    }
                    else
                    {
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = regid;
                        dc["session_id"] = session_id;
                        dc["class"] = classname;
                        dc["session"] = session;
                        dc["class_id"] = class_id;
                        dc["hosteltaken"] = hosteltaken;
                        dc["months"] = monthname;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = Hostel_id;
                        dc["Room_Category_id"] = Room_Category_id;
                        dc["Hostel_assig_id"] = Hostel_assign_id;
                        dc["day_boarding"] = day_bording;
                        dc["day_boarding_lunch"] = day_bording_with_lunch;
                        dc["category_id"] = category_id;
                        dc["sub_category_id"] = SubCategory_id;
                        dc["TransportationPath_id"] = TransportPath_id;
                        dc["transportportation_id"] = Transport_id;
                        dc["Boarding_Point_id"] = Boarding_Point_id;
                        dc["parameter_id"] = parameteridS;
                        string cunrt_session = session;
                        string[] stringSeparators = new string[] { "-" };
                        string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                        string session_frst_year = arr[0];
                        string session_last_year = arr[1];
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);
                        string monthid = My.tomonth_numberstring(monthname);
                        int pay_month = My.toint(monthid);
                        s_year = My.check_start_months(pay_month, s_year);
                        dc["monthid"] = s_year + monthid;
                        feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                        feedt.Columns.Add("previously_paid");
                        send_in_typewise_fee(feedt, Section, regid, monthname, Branch_id, session_id);
                    }
                }

                string month = "";
                double fee = 0, disc = 0, paid_prev = 0;
                double total_payable = 0;
                string late_fine_month = "", month_position = "";
                string qry = " select *  from Typewise_fee_collection_temp_dues_calculation  where admission_no='" + regid + "' and session='" + session + "' and status='Dues' and parameter like '%" + parameter + "%' and branchid='" + Branch_id + "' order by cast(Position as float)";
                SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Typewise_fee_collection");
                DataTable tdt = ds.Tables[0];
                if (tdt.Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in tdt.Rows)
                    {
                        month = dr["month"].ToString();
                        total_payable = My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"]) - My.toDouble(dr["previously_paid"]);
                        total += My.toDouble(total_payable);
                    }
                }
            }
            return total;
        }


        private void send_in_typewise_fee(DataTable feedt, string Section, string regid, string monthname, string Branch_id, string Session_id)
        {
            My mycode = new My();
            if (feedt.Rows.Count > 0)
            {
                foreach (DataRow dr in feedt.Rows)
                {
                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + regid + "' and session='" + Session + "' and month='" + dr["months"].ToString() + "' and parameter='" + dr["parameter"].ToString() + "'").Rows.Count == 0)
                    {
                        double paidAmt = My.toDouble(dr["previously_paid"].ToString()) + My.toDouble(dr["disc_amount"].ToString());
                        if (My.toDouble(dr["amount"].ToString()) > paidAmt)
                        {
                            My.exeSql("insert into Typewise_fee_collection_temp_dues_calculation(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,previously_paid) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + dr["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["content"].ToString() + "','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + Section + "','" + regid + "','" + Branch_id + "','" + My.toDouble(dr["previously_paid"].ToString()) + "')");
                        }
                    }
                }
            }
        }


        //private List<Student_payment_details_dt> findmyBedDetails(string Session_id, string Class_id, string admission_no)
        //{
        //    List<Student_payment_details_dt> Student_payment_details_dtItem = new List<Student_payment_details_dt>();
        //    string qry = "select t3.Section,t3.rollnumber,t1.Sheet_Id,t1.Transportation_Id,t1.TransportationPath_id,t1.Sheet_No,t2.Session_id,t2.Class_id,t2.Admission_no,t2.Month_name,t2.Transport_Assigned_Id,(select count (id) from Student_mapping_with_TransportPath where transport_id=t1.Transportation_Id and TransportPath_id=t1.TransportationPath_id and Sheet_Id=t1.Sheet_No and Session_id='" + Session_id + "') as Is_seat_assigned,t2.Created_date as Assign_date,t3.session,t3.studentname,t3.class as Class_name from TRANSPORT_PATH_MAPPING_WITH_SHEET t1 LEFT JOIN Student_mapping_with_TransportPath t2 on t1.Transportation_Id=t2.transport_id and t1.TransportationPath_id=t2.TransportPath_id and t1.Sheet_No=t2.Sheet_Id and t2.Session_id='" + Session_id + "' LEFT JOIN admission_registor t3 on t2.Session_id=t3.Session_id and t2.Class_id=t3.Class_id and t2.Admission_no=t3.admissionserialnumber where t1.Transportation_Id='" + Transport_id + "' and t1.TransportationPath_id='" + TransportationPath_id + "' order by t1.Sheet_Id asc";
        //    DataTable dt = My.dataTable(qry);
        //    int rowcount = dt.Rows.Count;
        //    if (rowcount == 0)
        //    {
        //    }
        //    else
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            string avaltop = ""; string avalHidden = ""; string seat_asined_status = ""; string student_name = "NA"; string Student_class = "NA"; string session_name = "NA"; string current_sem_year = "NA"; string assign_date = "NA"; string backgrnds = "bedAvals";
        //            string isSeatAssigned = dr["Is_seat_assigned"].ToString();

        //            student_name = dr["studentname"].ToString();
        //            if (isSeatAssigned != "0")
        //            {
        //                avalHidden = "";
        //                seat_asined_status = "Occupied";
        //                avaltop = "";
        //            }
        //            else
        //            {
        //                avalHidden = "hidden";
        //                seat_asined_status = "Vacant";
        //                avaltop = "topminus50";
        //            }
        //            if (seat_asined_status == "Occupied")
        //            {
        //                student_name = dr["studentname"].ToString();
        //                Student_class = dr["Class_name"].ToString();
        //                session_name = dr["session"].ToString();
        //                current_sem_year = dr["Section"].ToString() + " Roll No." + dr["rollnumber"].ToString();
        //                assign_date = dr["Assign_date"].ToString();
        //                backgrnds = "bedNotAvals";
        //            }


        //            Student_payment_details_dtItem.Add(new Student_payment_details_dt
        //            {
        //                Sheet_No = dr["Sheet_No"].ToString(),
        //                Sheet_Id = dr["Sheet_Id"].ToString(),
        //                TransportationPath_id = dr["TransportationPath_id"].ToString(),
        //                Transportation_Id = dr["Transportation_Id"].ToString(),
        //                Admission_no = dr["Admission_no"].ToString(),
        //                Is_seat_assigned = seat_asined_status,
        //                studentname = student_name,
        //                session = session_name,
        //                Class_name = Student_class,
        //                Current_Semester_or_Year = current_sem_year,
        //                Assign_date = assign_date,
        //                BackgrounDS = backgrnds,
        //                AvalHidden = avalHidden,
        //                Avaltop = avaltop,
        //            });
        //        }
        //    }
        //    return Student_payment_details_dtItem;
        //}

    }
}
