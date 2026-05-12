using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Admin.slip.webServices
{
    /// <summary>
    /// Summary description for demandBill
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class demandBill : System.Web.Services.WebService
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
            public string Rollnumber { get; set; }
            public string Mobile_no { get; set; }
            public string PayDate { get; set; }
            public string Section { get; set; }
            public string Mother_name { get; set; }
            public string Mother_mobile { get; set; }
            public string Print_for { get; set; }
            public List<MySchoolDetails> MySchoolDetailsItem { get; set; }

            public List<MyFeeDetails> MyFeeDetailsItem { get; set; }
            //public List<MySigDetails> MySigDetailsItem { get; set; }
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
            public string ExamTypeText { get; set; }

            public string IsHeaderImageShow { get; set; }
            public string IsHeaderContentShow { get; set; }
            public string Header_images { get; set; }
        }
        public class MyFeeDetails
        {
            public string Months { get; set; }
            public string Content { get; set; }
            public string Amount { get; set; }
            public string Disc_amount { get; set; }
            public string Previously_paid { get; set; }
            public string Total_payable { get; set; }
            public string TotalAMt { get; set; }
            public string RowCount { get; set; }
            public string InWordAmt { get; set; }
            public string ZeroAmt { get; set; }
            public string rowsHidden { get; set; }
            public string Fine_amt { get; set; }
            public string Is_fine_amt { get; set; }
        }



        List<MyAdmitCardStudent> EMySubMark = new List<MyAdmitCardStudent>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_demand_bill(string Session_id, string Class_id, string Admission_no, string Branch_id, string Section, string Checked, string Month_name, string Paydate, string Session_name, string Bill_type)
        {
            My mycode = new My();
            string query = "";
            if (Checked == "1")
            {
                query = "Select * from admission_registor where Session_id=" + Session_id + " and Id in (" + Admission_no + ") order by Section,rollnumber asc";
            }
            else
            {
                if (Section == "0")
                {
                    query = "Select * from admission_registor where Session_id=" + Session_id + " and Class_id='" + Class_id + "' order by Section,rollnumber asc";
                }
                else
                {
                    query = "Select * from admission_registor where Session_id=" + Session_id + " and Class_id='" + Class_id + "' and Section='" + Section + "' order by Section,rollnumber asc";
                }
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string month_position = My.month_position(Month_name).ToString();
                string[] stringSeparators = new string[] { "-" };
                string[] arr = Session_name.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);
                foreach (DataRow dr in dt.Rows)
                {
                    string IsBoarding = "0"; string LunchMnthName = "0"; string LunchMnthId = "0";
                    string parameteridS = "4";
                    string parameter = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";

                    string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + Session_id + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        LunchMnthName = dts.Rows[0]["Month_name"].ToString();
                        LunchMnthId = dts.Rows[0]["Month_id"].ToString();
                        IsBoarding = "1";
                    }


                    ///========================HOSTEL  
                    string Hostel_id = "0";
                    string Room_Category_id = "0";
                    string From_month_name = "0";
                    string From_month_id = "0";
                    string Assined_Year_Month = "0";
                    string Hostel_assign_id = "0";
                    string Hostel_Bed_id = "0";
                    if (dr["hosteltaken"].ToString().ToUpper() == "YES")
                    {
                        Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(Session_id, Class_id, dr["admissionserialnumber"].ToString());
                        Hostel_id = (String)dc1["Hostel_id"];
                        Room_Category_id = (String)dc1["Room_Category_id"];
                        From_month_name = (String)dc1["From_month_name"];
                        From_month_id = (String)dc1["From_month_id"];
                        Assined_Year_Month = (String)dc1["Assined_Year_Month"];
                        Hostel_assign_id = (String)dc1["Hostel_assign_id"];
                        Hostel_Bed_id = (String)dc1["Bed_id"];
                    }

                    ///==========================
                    string Transport_id = "0";
                    string TransportPath_id = "0";
                    string Boarding_Point_id = "0";
                    if (dr["transportationtaken"].ToString().ToUpper() == "YES")
                    {
                        Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(Session_id, Class_id, dr["admissionserialnumber"].ToString());
                        Transport_id = (String)dc2["Transport_id"];
                        TransportPath_id = (String)dc2["TransportPath_id"];
                        Boarding_Point_id = (String)dc2["Boarding_Point_id"];
                    }



                    List<MySchoolDetails> MBdetails = findmyFirmDetails();
                    List<MyFeeDetails> MBExamdetails = findmyFeeDetails(dr["Class_id"].ToString(), dr["session"].ToString(), Session_id, dr["admissionserialnumber"].ToString(), Branch_id, Month_name, month_position, s_year, session_frst_year, session_last_year, LunchMnthName, LunchMnthId, IsBoarding, parameteridS, parameter, dr["class"].ToString(), Hostel_id, Room_Category_id, From_month_name, From_month_id, Assined_Year_Month, Hostel_assign_id, dr["Category_id"].ToString(), dr["SubCategory_id"].ToString(), dr["hosteltaken"].ToString(), My.toBool(dr["is_applied_dayboarding"]), My.toBool(dr["day_boarding_with_lunch"]), Transport_id, TransportPath_id, Boarding_Point_id, Paydate, Bill_type);
                    // List<MySigDetails> MBSigdetails = findmySigDetails(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Branch_id);

                    string std_imgs = dr["studentimagepath"].ToString();
                    if (std_imgs == "")
                    {
                        std_imgs = "hidden";
                    }
                    string motherMob = "";
                    string Print_for = "";
                    Print_for = "Candidates' Copy";


                    EMySubMark.Add(new MyAdmitCardStudent
                    {
                        Student_name = dr["studentname"].ToString(),
                        Subject_DOB = dr["dob"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Student_mob_no = dr["mobilenumber"].ToString(),
                        Registration_id = dr["admissionserialnumber"].ToString(),
                        Course_Name = dr["class"].ToString(),
                        Session_name = dr["session"].ToString(),
                        Section = dr["Section"].ToString(),
                        Rollnumber = dr["rollnumber"].ToString(),
                        Mobile_no = dr["mobilenumber"].ToString(),
                        Mother_name = dr["mothername"].ToString(),
                        Mother_mobile = motherMob,
                        Print_for = Print_for,
                        PayDate = Paydate,
                        MySchoolDetailsItem = MBdetails,
                        MyFeeDetailsItem = MBExamdetails,
                        //MySigDetailsItem = MBSigdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }




        private List<MySchoolDetails> findmyFirmDetails()
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
                    string isHeaderImageShow = "hidden"; string isHeaderContentShow = "showed";
                    if (dr["Is_slip_header"].ToString() == "True")
                    {
                        isHeaderImageShow = "showed";
                        isHeaderContentShow = "hidden";
                    }
                    MySchoolDetailsItem.Add(new MySchoolDetails
                    {
                        School_name = dr["firm_name"].ToString(),
                        Affiliation_no = "",
                        Address = dr["address1"].ToString(),
                        Mobile_no_email = "Telephone No : " + dr["contact_no"].ToString() + " , E-mail Address : " + dr["email"].ToString(),
                        Session = "ACADEMIC SESSION : ",
                        LogoSchool = dr["logo"].ToString(),
                        IsHeaderImageShow = isHeaderImageShow,
                        IsHeaderContentShow = isHeaderContentShow,
                        Header_images = dr["Header_images"].ToString(),

                    });
                }
            }
            return MySchoolDetailsItem;
        }

        //===============================
        private List<MyFeeDetails> findmyFeeDetails(string Class_id, string Session_name, string Session_id, string admission_no, string Branch_id, string Month_name, string month_position, int s_year, string session_frst_year, string session_last_year, string LunchMnthName, string LunchMnthId, string IsBoarding, string parameteridS, string parameter, string class_name, string Hostel_id, string Room_Category_id, string From_month_name, string From_month_id, string Assined_Year_Month, string Hostel_assign_id, string Category_id, string SubCategory_id, string hosteltaken, bool day_bording, bool day_bording_with_lunch, string Transport_id, string TransportPath_id, string Boarding_Point_id, string Paydate,string Bill_type)
        {
            My mycode = new My();
            DataTable fdt = new DataTable();
            List<MyFeeDetails> MyFeeDetailsItem = new List<MyFeeDetails>();
            string query = "select * from Month_Index where Position<=" + month_position + " order by Position asc";
            DataTable dtm = My.dataTable(query);
            if (dtm.Rows.Count > 0)
            {
                double fee_amount = 0, disc_amount = 0, paid_previously = 0;
                foreach (DataRow drm in dtm.Rows)
                {
                    DataTable feedt = new DataTable();
                    if (IsBoarding == "1")
                    {
                        string current_year_month = session_last_year + My.tomonth_numberstring(drm["Month"].ToString());
                        string lunch_taken_year_month = "";
                        if (My.toint(LunchMnthId) == 1 || My.toint(LunchMnthId) == 2 || My.toint(LunchMnthId) == 3)
                        {
                            lunch_taken_year_month = session_last_year + LunchMnthId;
                        }
                        else
                        {
                            lunch_taken_year_month = session_frst_year + LunchMnthId;
                        }


                        if (My.toint(lunch_taken_year_month) <= My.toint(current_year_month))
                        {
                            parameteridS = "44";
                        }
                        else
                        {
                            parameteridS = "4";
                        }
                    }
                    string type = "";
                    if (My.dataTable("select  * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + Session_name + "' and month='" + drm["Month"].ToString() + "' and parameter='" + parameter + "' and transection!=''").Rows.Count > 0)
                    {
                        feedt = My.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter, feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + admission_no + "' and session='" + Session_name + "' and month='" + drm["Month"].ToString() + "' and parameter='" + parameter + "' and content_id!='6121'  and transection!=''  and status='Dues'");
                        type = "Calculated";
                    }
                    else
                    {
                        //dr["bac_colour"] = "White";
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = admission_no;
                        dc["session_id"] = Session_id;
                        dc["class"] = class_name;
                        dc["session"] = Session_name;
                        dc["class_id"] = Class_id;
                        dc["hosteltaken"] = hosteltaken.ToLower();
                        dc["months"] = drm["Month"].ToString();
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = Hostel_id;
                        dc["Room_Category_id"] = Room_Category_id;
                        dc["Hostel_assig_id"] = Hostel_assign_id;
                        dc["day_boarding"] = day_bording;
                        dc["day_boarding_lunch"] = day_bording_with_lunch;
                        dc["category_id"] = Category_id;
                        dc["sub_category_id"] = SubCategory_id;
                        dc["TransportationPath_id"] = TransportPath_id;
                        dc["transportportation_id"] = Transport_id;
                        dc["Boarding_Point_id"] = Boarding_Point_id;
                        dc["parameter_id"] = parameteridS;

                        //new08/08/2022 
                        string monthid = My.tomonth_numberstring(drm["Month"].ToString());
                        int pay_month = My.toint(monthid);
                        s_year = My.check_start_months(pay_month, s_year);
                        dc["monthid"] = s_year + monthid;

                        feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                        feedt.Columns.Add("previously_paid");
                        type = "NotCalculated";
                    }
                    feedt.Columns.Add("total_payable");
                    string month = "";
                    double total = 0, fee = 0, disc = 0, paid_prev = 0;
                    foreach (DataRow dr in feedt.Rows)
                    {
                        month = dr["months"].ToString();
                        dr["total_payable"] = My.toDouble(dr["amount"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["previously_paid"]);
                        fee += My.toDouble(dr["amount"]);
                        disc += My.toDouble(dr["disc_amount"]);
                        paid_prev += My.toDouble(dr["previously_paid"]);

                        total += My.toDouble(dr["total_payable"]);
                    }

                    foreach (DataRow dr in feedt.Rows)
                    {
                        try
                        {
                            fdt.Rows.Add(dr.ItemArray);
                        }
                        catch
                        {
                            foreach (DataColumn dc in feedt.Columns)
                            {
                                fdt.Columns.Add(dc.ColumnName);
                            }
                            fdt.Rows.Add(dr.ItemArray);
                        }
                    }
                }


                //string lftRght = ""; int countOfStd = 1;
                string zeroAmt = "";
                if (fdt.Rows.Count > 0)
                {
                    double totalAMt = 0; int count = 1; int count1 = 1; double totalAMt1 = 0; int rowsconts = 1; string is_fine_amt = "showd";
                    foreach (DataRow dr in fdt.Rows)
                    { 
                        rowsconts++;
                        double ttlamtsSingle = totalAMt1 + (My.toDouble(dr["amount"].ToString()) - (My.toDouble(dr["disc_amount"].ToString()) + My.toDouble(dr["previously_paid"].ToString())));
                        totalAMt = totalAMt + ttlamtsSingle;

                        //======
                        string inword_number = ""; double fine_amt = 0;
                        if (count == fdt.Rows.Count)
                        {
                            fine_calculation(Class_id, Session_name, Session_id, admission_no, Branch_id, Month_name, month_position, s_year, session_frst_year, session_last_year, LunchMnthName, LunchMnthId, IsBoarding, parameteridS, parameter, class_name, Hostel_id, Room_Category_id, From_month_name, From_month_id, Assined_Year_Month, Hostel_assign_id, Category_id, SubCategory_id, hosteltaken, day_bording, day_bording_with_lunch, Transport_id, TransportPath_id, Boarding_Point_id, Paydate, "No");

                            DataTable dtFine = My.dataTable("select isnull(sum(convert(float, Fine_amount)),0) as Fine_amt from Temp_fine_monthwise where Admission_no='" + admission_no + "' and Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "'");
                            if (dtFine.Rows.Count > 0)
                            {
                                fine_amt = My.toDouble(dtFine.Rows[0]["Fine_amt"].ToString());
                            }
                            if (fine_amt <= 0)
                            {
                                is_fine_amt = "hidden";
                            }
                            if (totalAMt <= 0)
                            {
                                zeroAmt = "hidden";
                            }
                            totalAMt = totalAMt + fine_amt;
                            int number = (int)Convert.ToDouble(totalAMt);
                            inword_number = mycode.NumberToWords(number);
                        }

                        string rowsHidden = "showd";
                        if (rowsconts > 13)
                        {
                            rowsHidden = "hidden";
                        }


                        MyFeeDetailsItem.Add(new MyFeeDetails
                        {
                            Months = dr["months"].ToString(),
                            Content = dr["content"].ToString(),
                            Amount = ttlamtsSingle.ToString(),
                            Disc_amount = dr["disc_amount"].ToString(),
                            Previously_paid = dr["previously_paid"].ToString(),
                            Total_payable = dr["total_payable"].ToString(),
                            TotalAMt = totalAMt.ToString(),
                            RowCount = (fdt.Rows.Count - 1).ToString(),
                            InWordAmt = inword_number,
                            ZeroAmt = zeroAmt,
                            rowsHidden = rowsHidden,
                            Fine_amt = fine_amt.ToString(),
                            Is_fine_amt = is_fine_amt,
                        });
                        count++;
                    }
                }
                else
                {
                    MyFeeDetailsItem.Add(new MyFeeDetails
                    {
                        RowCount = "0",
                        ZeroAmt = "hidden",
                    });
                }
            }
            return MyFeeDetailsItem;
        }




        #region FINE CALCULATION
        private void fine_calculation(string class_id, string session_name, string session_id, string admission_no, string branch_id, string month_name, string month_position, int s_years, string session_frst_years, string session_last_year, string lunchMnthName, string lunchMnthId, string isBoarding, string parameteridS, string parameter, string class_name, string hostel_id, string room_Category_id, string from_month_name, string from_month_id, string assined_Year_Month, string hostel_assign_id, string category_id, string subCategory_id, string hosteltaken, bool day_bording, bool day_bording_with_lunch, string transport_id, string transportPath_id, string boarding_Point_id, string Paydate, string RepeatFine)
        {
            double fine_amts = 0; string FineType = "";
            My mycode = new My();
            DataTable dty = mycode.FillData("select Fine_mode from globle_data where Fine_mode='2'");
            if (dty.Rows.Count != 0)
            {
                string qry = "delete from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Branch_id='" + branch_id + "'";
                My.exeSql(qry);

                #region DayRanGEWise
                string pay_date = Paydate;
                int payidate = My.DateConvertToIdate(pay_date);


                //Advance Payment Check
                string crnt_year = mycode.year();
                string cunrt_session = session_name;
                string session_frst_year = cunrt_session.Substring(0, 4);
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);


                string CheckedMonth = "0"; string CheckedMonthN = "0";
                string query = "select * from Month_Index where Position<=" + month_position + " order by Position asc";
                DataTable dtm = My.dataTable(query);
                if (dtm.Rows.Count > 0)
                {
                    foreach (DataRow drm in dtm.Rows)
                    {
                        if (CheckedMonth == "0")
                        {
                            if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + admission_no + "' and session='" + session_name + "' and month='" + drm["Month"].ToString() + "' and parameter='" + parameter + "' and transection!=''").Rows.Count > 0)
                            {
                            }
                            else
                            {
                                CheckedMonthN = drm["Month"].ToString();
                                CheckedMonth = "1";
                            }
                        }
                    }
                }


                int mnth_idss = My.tomonth_number(CheckedMonthN);
                string pay_month_two_digit = My.getMonthS_twoDigit(mnth_idss.ToString());
                int pay_month = My.toint(pay_month_two_digit);
                s_year = My.check_start_months(pay_month, s_year);

                int pay_month_with_year = My.toint(s_year + pay_month_two_digit);
                int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                //Advance Payment Check



                if (crnt_month_with_year >= pay_month_with_year)
                {
                    DataTable dtz = mycode.FillData("select top 1 * from Fine_master_day_range");
                    if (dtz.Rows.Count != 0)
                    {
                        FineType = "DayWise";
                        string last_day_of_payments = "01" + "/" + pay_month_two_digit + "/" + s_year;


                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);


                        DataTable dt_fine = mycode.FillData("select top 1 * from Fine_master_day_range where No_of_day>" + totaldays + " order by No_of_day asc");
                        if (dt_fine.Rows.Count != 0)
                        {
                            save_fine_amount(session_id, admission_no, pay_month_two_digit, My.toDouble(dt_fine.Rows[0]["Fine_amount"].ToString()), branch_id);
                        }
                        else
                        {
                            DataTable dt_fines = mycode.FillData("select top 1 * from Fine_master_day_range order by No_of_day desc");
                            if (dt_fines.Rows.Count != 0)
                            {
                                save_fine_amount(session_id, admission_no, pay_month_two_digit, My.toDouble(dt_fines.Rows[0]["Fine_amount"].ToString()), branch_id);
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region FINES
                DataTable dt = mycode.FillData("select top 1 * from Fine_master where Status='1' and Session_id='" + session_id + "'");
                if (dt.Rows.Count != 0)
                {
                    string pay_date = Paydate;
                    int payidate = My.DateConvertToIdate(pay_date);
                    string fineType = dt.Rows[0]["Fine_type"].ToString();
                    if (fineType == "MonthWise") //===== MonthWise
                    {
                        #region MonthWise
                        string qry = "delete from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Branch_id='" + branch_id + "'";
                        My.exeSql(qry);
                        fine_amts = 0;


                        ///================================
                        string query = "select * from Month_Index where Position<=" + month_position + " order by Position asc";
                        DataTable dtm = My.dataTable(query);
                        if (dtm.Rows.Count > 0)
                        {
                            double fee_amount = 0, disc_amount = 0, paid_previously = 0;
                            foreach (DataRow drm in dtm.Rows)
                            {
                                string cunrt_session = session_name;
                                string session_frst_year = cunrt_session.Substring(0, 4);
                                int session_s_year = My.toint(session_frst_year);
                                int s_year = My.toint(session_frst_year);


                                if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + admission_no + "' and session='" + session_name + "' and month='" + drm["Month"].ToString() + "' and parameter='" + parameter + "' and transection!=''").Rows.Count > 0)
                                {
                                }
                                else
                                {
                                    int mnth_idss = My.tomonth_number(drm["Month"].ToString());
                                    string pay_month_two_digit = My.getMonthS_twoDigit(mnth_idss.ToString());

                                    int pay_month = My.toint(pay_month_two_digit);
                                    s_year = My.check_start_months(pay_month, s_year);



                                    FineType = "MonthWise";
                                    string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                                    string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");
                                    string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + pay_month_two_digit + "/" + s_year;
                                    DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    System.TimeSpan diff = enddate1.Subtract(startdate1);
                                    int totaldays = Convert.ToInt32(diff.Days);
                                    int totalmonths = 0;


                                    int pay_month_with_year = My.toint(s_year + pay_month_two_digit);
                                    int fine_aplicable_year = My.check_start_months(My.toInt(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString()), s_year);
                                    string fine_applicable_month = fine_aplicable_year + applicable_month;
                                    if (My.toint(fine_applicable_month) <= pay_month_with_year)
                                    {
                                        if (RepeatFine == "Yes")
                                        {
                                            if (totaldays > 0)
                                            {
                                                if (30 >= totaldays)
                                                {
                                                    totalmonths = 1;
                                                }
                                                else
                                                {
                                                    totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                                                    double monthdays = 31;
                                                    double reminder = My.toDouble(totaldays) % monthdays;
                                                    if (29 > Math.Round(reminder))
                                                    {
                                                        totalmonths++;
                                                    }
                                                }
                                            }


                                            string fineStartFromMonth = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                                            string fineStartFrom = s_year + fineStartFromMonth + dt.Rows[0]["Last_day_to_deposit_fees"].ToString();
                                            if (My.toIntS(fineStartFrom) > My.DateConvertToIdate(pay_date))
                                            {
                                                totalmonths = 0;
                                            }

                                            if (My.toDouble(totalmonths) > 0)
                                            {
                                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                                double ttl_fine_amt = My.toDouble(fine_amt) * totalmonths;
                                                save_fine_amount(session_id, admission_no, pay_month_two_digit, ttl_fine_amt, branch_id);
                                            }
                                            else
                                            {
                                                save_fine_amount(session_id, admission_no, pay_month_two_digit, 0, branch_id);
                                            }
                                        }
                                        else
                                        {
                                            if (totaldays > 0)
                                            {
                                                if (30 >= totaldays)
                                                {
                                                    totalmonths = 1;
                                                }
                                                else
                                                {
                                                    totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                                                }
                                            }

                                            if (My.toDouble(totalmonths) > 0)
                                            {
                                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                                double ttl_fine_amt = My.toDouble(fine_amt) * 1;
                                                save_fine_amount(session_id, admission_no, pay_month_two_digit, ttl_fine_amt, branch_id);
                                            }
                                            else
                                            {
                                                save_fine_amount(session_id, admission_no, pay_month_two_digit, 0, branch_id);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    if (fineType == "DayWise")
                    {
                        #region DayWise 
                        string qry = "delete from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Branch_id='" + branch_id + "'";
                        My.exeSql(qry);
                        fine_amts = 0;

                        string isCalculated = "0";
                        string query = "select * from Month_Index where Position<=" + month_position + " order by Position asc";
                        DataTable dtm = My.dataTable(query);
                        if (dtm.Rows.Count > 0)
                        {
                            foreach (DataRow drm in dtm.Rows)
                            {
                                if (isCalculated == "0")
                                {
                                    string cunrt_session = session_name;
                                    string session_frst_year = cunrt_session.Substring(0, 4);
                                    int session_s_year = My.toint(session_frst_year);
                                    int s_year = My.toint(session_frst_year);

                                    int mnth_idss = My.tomonth_number(drm["Month"].ToString());
                                    string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());

                                    int pay_month = My.toint(month_id_in_two_dgts);
                                    s_year = My.check_start_months(pay_month, s_year);


                                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + session_name + "' and month='" + drm["Month"].ToString() + "' and parameter='" + parameter + "' and transection!=''").Rows.Count > 0)
                                    {
                                    }
                                    else
                                    {
                                        DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        string paymentMonthno = enddate1q.ToString("MM");

                                        int pay_month_with_year = My.toint(s_year + month_id_in_two_dgts);
                                        int crnt_month_with_year = My.toint(mycode.year() + paymentMonthno);
                                        //Advance Payment Check 
                                        int fine_aplicable_year = My.check_start_months(My.toInt(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString()), s_year);

                                        if (crnt_month_with_year >= pay_month_with_year)
                                        {
                                            FineType = "DayWise";
                                            string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                                            string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;

                                            string fine_applicable_month = fine_aplicable_year + applicable_month;
                                            if (My.toint(fine_applicable_month) <= pay_month_with_year)
                                            {
                                                string last_day_of_payments = "";
                                                if (My.toint(fine_applicable_month) >= pay_month_with_year)
                                                {
                                                    last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;
                                                }
                                                else
                                                {
                                                    last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + month_id_in_two_dgts + "/" + s_year;
                                                }
                                                DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                                System.TimeSpan diff = enddate1.Subtract(startdate1);
                                                int totaldays = Convert.ToInt32(diff.Days);

                                                if (My.toDouble(totaldays) > 0)
                                                {
                                                    isCalculated = "1";
                                                    string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                                    double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                                    save_fine_amount(session_id, admission_no, month_id_in_two_dgts, ttl_fine_amt, branch_id);
                                                }
                                                else
                                                {
                                                    isCalculated = "1";
                                                    save_fine_amount(session_id, admission_no, month_id_in_two_dgts, 0, branch_id);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    if (fineType == "QuarterWise")
                    {
                        string qry = "delete from Temp_fine_monthwise where Session_id = '" + session_id + "' and Admission_no = '" + admission_no + "' and Branch_id = '" + branch_id + "'";
                        My.exeSql(qry);

                        #region QuarterWise
                        string uncheckd = "1"; int late_fine_no_of_day_month = 0; string flags1 = "0"; string fine_date_From = ""; string fine_date_To = "";
                        string query = "select * from Month_Index where Position<=" + month_position + " order by Position asc";
                        DataTable dtmm = My.dataTable(query);
                        if (dtmm.Rows.Count > 0)
                        {
                            foreach (DataRow drm in dtmm.Rows)
                            {
                                uncheckd = "0";
                                string cunrt_session = session_name;
                                string session_frst_year = cunrt_session.Substring(0, 4);
                                int session_s_year = My.toint(session_frst_year);
                                int s_year = My.toint(session_frst_year);
                                int mnth_idss = My.tomonth_number(drm["Month"].ToString());
                                string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());
                                int pay_month = My.toint(month_id_in_two_dgts);
                                s_year = My.check_start_months(pay_month, s_year);

                                if (My.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + session_name + "' and month='" + drm["Month"].ToString() + "' and parameter='" + parameter + "' and transection!=''").Rows.Count > 0)
                                {
                                }
                                else
                                {
                                    #region QuarterWise
                                    FineType = "QuarterWise";
                                    double fnl_fine_amt = 0;
                                    SqlDataAdapter ad = new SqlDataAdapter("select * from Fine_master where Status='1' and Session_id='" + session_id + "' and Q_start_month='" + month_id_in_two_dgts + "' and Q_start_year='" + s_year + "'  order by Q_start_month asc", My.conn);
                                    DataSet ds = new DataSet();
                                    ad.Fill(ds, "Fine_master");
                                    DataTable dtm = ds.Tables[0];
                                    int rowcount = ds.Tables[0].Rows.Count;
                                    if (rowcount == 0)
                                    {
                                    }
                                    else
                                    {
                                        foreach (DataRow dr in dtm.Rows)
                                        {
                                            string endmnth_in_two = My.getMonthS_twoDigit(dr["Q_start_month"].ToString());
                                            string last_day_of_payment_q = dr["Last_day_to_deposit_fees"].ToString() + "/" + endmnth_in_two + "/" + dr["Q_start_year"].ToString();


                                            DateTime startdate1q = DateTime.ParseExact(last_day_of_payment_q, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                            if (dr["Q_payment_mode"].ToString() == "Day")
                                            {
                                                System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                                int totaldays = Convert.ToInt32(diff.Days);
                                                late_fine_no_of_day_month = totaldays;

                                                if (flags1 == "0")
                                                {
                                                    fine_date_From = last_day_of_payment_q;
                                                    flags1 = "1";
                                                }
                                                fine_date_To = pay_date;

                                                if (My.toDouble(totaldays) > 0)
                                                {
                                                    string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                                    double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                                    fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                                }
                                                else
                                                {
                                                    fnl_fine_amt = fnl_fine_amt += 0;
                                                }
                                            }
                                            else
                                            {
                                                System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                                int totaldays = Convert.ToInt32(diff.Days);
                                                late_fine_no_of_day_month = dtm.Rows.Count;

                                                if (flags1 == "0")
                                                {
                                                    fine_date_From = last_day_of_payment_q;
                                                    flags1 = "1";
                                                }
                                                fine_date_To = pay_date;
                                                if (My.toDouble(totaldays) > 0)
                                                {
                                                    string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                                    double ttl_fine_amt = My.toDouble(fine_amt);
                                                    fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                                }
                                                else
                                                {
                                                    fnl_fine_amt = fnl_fine_amt += 0;
                                                }
                                            }
                                        }

                                        save_fine_amount(session_id, admission_no, month_id_in_two_dgts, fnl_fine_amt, branch_id);

                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion
            }
        }

        private void save_fine_amount(string session_id, string admission_no, string month_id_in_two_dgts, double ttl_fine_amt, string branch_id)
        {
            My mycode = new My();
            if (mycode.IsUserExist("select Id from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + branch_id + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Temp_fine_monthwise (Session_id,Admission_no,Month_id,Fine_amount,Branch_id) values (@Session_id,@Admission_no,@Month_id,@Fine_amount,@Branch_id);";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd.Parameters.AddWithValue("@Month_id", month_id_in_two_dgts);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Temp_fine_monthwise set Fine_amount=@Fine_amount  where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + branch_id + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }


        #endregion

        //===============================
        //private List<MySigDetails> findmySigDetails(string Session_id, string Class_id, string Branch_id)
        //{
        //    List<MySigDetails> MySigDetailsItem = new List<MySigDetails>();

        //    string signatures = Examination.get_signature_admit_card(Session_id, Class_id, "A", Branch_id);
        //    string[] stringSeparatorss = new string[] { ">" };
        //    string[] arrs = signatures.Split(stringSeparatorss, StringSplitOptions.None);
        //    string class_teacher_sig = arrs[0];
        //    string principal_sig = arrs[1];
        //    string examinee_sig = arrs[2];

        //    MySigDetailsItem.Add(new MySigDetails
        //    {
        //        Class_teacher_sig = class_teacher_sig,
        //        Principal_sig = principal_sig,
        //        Examinee_sig = examinee_sig,
        //    });


        //    return MySigDetailsItem;
        //}


    }
}
