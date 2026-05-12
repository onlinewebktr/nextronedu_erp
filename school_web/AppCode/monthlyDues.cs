using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class monthlyDues
    {
        internal static string get_month_fee_dues_till_months(string session_id, string class_id, string admission_no)
        {
            string ttl_dues = "0";
            string c_month_name = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MMMM");
            int month_position = My.toIntS(get_month_positions(c_month_name));
            Dictionary<string, object> dc1 = My.getstudentinfo(admission_no, session_id);
            string Name = (String)dc1["Name"];
            string Session_id = (String)dc1["Session_id"];
            string Session = (String)dc1["Session"];
            string category_id = (String)dc1["category_id"];
            string sub_category_id = (String)dc1["sub_category_id"];
            string Transfer_Status = (String)dc1["Transfer_Status"];
            string Admission_no = (String)dc1["Admission_no"];
            string classname = (String)dc1["classname"];

            string hostaltaken = (String)dc1["hosteltaken"];
            string Branch_id = (String)dc1["Branch_id"];
            string Section = (String)dc1["Section"];
            string Hostel_id = (String)dc1["Hostel_id"];
            bool day_bording = My.toBool((String)dc1["is_applied_dayboarding"]);
            bool day_bording_with_lunch = My.toBool((String)dc1["day_boarding_with_lunch"]);

            string parameter = hostaltaken.ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";


            string IsBoarding = "0";
            string parameteridS = "4";
            string LunchMnthId = "";
            string LunchMnthName = "";
            string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Class_id='" + class_id + "'";
            DataTable dts = My.dataTable(queryS);
            if (dts.Rows.Count != 0)
            {
                LunchMnthName = dts.Rows[0]["Month_name"].ToString();
                LunchMnthId = dts.Rows[0]["Month_id"].ToString();
                IsBoarding = "1";
            }


            My mycode = new My();
            Dictionary<string, object> dc12 = mycode.Bind_hostel_data_for_assined_student(session_id, class_id, admission_no);
            Hostel_id = (String)dc12["Hostel_id"];
            string Room_Category_id = (String)dc12["Room_Category_id"];
            string From_month_name = (String)dc12["From_month_name"];
            string From_month_id = (String)dc12["From_month_id"];
            string Assined_Year_Month = (String)dc12["Assined_Year_Month"];
            string Hostel_assign_id = (String)dc12["Hostel_assign_id"];



            Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(session_id, class_id, admission_no);
            string Transport_id = (String)dc2["Transport_id"];
            string TransportPath_id = (String)dc2["TransportPath_id"];
            string Boarding_Point_id = (String)dc2["Boarding_Point_id"];
            string Transport_Assigned_Id = (String)dc2["Transport_Assigned_Id"];
            string Month_id = (String)dc2["Month_id"];
            string Year_month = (String)dc2["Year_month"];
            string Sheet_Id = (String)dc2["Sheet_Id"];



            double total = 0;
            double fee_amount = 0, disc_amount = 0, paid_previously = 0;
            bool success = false;
            DataTable fdt = new DataTable();

            DataTable monthDT = My.dataTable("select * from Month_Index where Position<=" + month_position + " order by Position asc");
            if (monthDT.Rows.Count > 0)
            {
                foreach (DataRow drmnths in monthDT.Rows)
                {
                    string cunrt_session = Session;
                    string[] stringSeparators = new string[] { "-" };
                    string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                    string session_frst_year = arr[0];
                    string session_last_year = arr[1];
                    int session_s_year = My.toint(session_frst_year);
                    int s_year = My.toint(session_frst_year);
                    string Month_name = drmnths["Month"].ToString();
                    fee_amount = 0; disc_amount = 0; paid_previously = 0;
                    DataTable feedt = new DataTable();

                    if (IsBoarding == "1")
                    {
                        string current_year_month = session_last_year + My.tomonth_numberstring(Month_name);
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
                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + admission_no + "' and session='" + Session + "' and month='" + Month_name + "' and parameter='" + parameter + "' and transection!=''").Rows.Count > 0)
                    {
                        feedt = My.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter, feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + admission_no + "' and session='" + Session + "' and month='" + Month_name + "' and parameter='" + parameter + "' and content_id!='6121'  and transection!=''");
                        type = "Calculated";
                    }
                    else
                    {
                        //dr["bac_colour"] = "White";
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = admission_no;
                        dc["session_id"] = session_id;
                        dc["class"] = classname;
                        dc["session"] = Session;
                        dc["class_id"] = class_id;
                        dc["hosteltaken"] = hostaltaken.ToLower();
                        dc["months"] = Month_name;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = Hostel_id;
                        dc["Room_Category_id"] = Room_Category_id;
                        dc["Hostel_assig_id"] = Hostel_assign_id;
                        dc["day_boarding"] = day_bording;
                        dc["day_boarding_lunch"] = day_bording_with_lunch;
                        dc["category_id"] = category_id;
                        dc["sub_category_id"] = sub_category_id;
                        dc["TransportationPath_id"] = TransportPath_id;
                        dc["transportportation_id"] = Transport_id;
                        dc["Boarding_Point_id"] = Boarding_Point_id;
                        dc["parameter_id"] = parameteridS;
                        //new08/08/2022

                        string monthid = My.tomonth_numberstring(Month_name);
                        int pay_month = My.toint(monthid);
                        s_year = My.check_start_months(pay_month, s_year);
                        dc["monthid"] = s_year + monthid;

                        feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                        feedt.Columns.Add("previously_paid");
                        type = "NotCalculated";
                    }
                    feedt.Columns.Add("total_payable");

                    string month = "";
                    double fee = 0, disc = 0, paid_prev = 0;
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
            }

            return total.ToString("0.00");
        }


        private static string get_month_positions(string month_name)
        {
            string mnth_position = "0";
            DataTable dt = My.dataTable("select Position from Month_Index where Month='" + month_name + "'");
            if (dt.Rows.Count > 0)
            {
                mnth_position = dt.Rows[0]["Position"].ToString();
            }
            return mnth_position;
        }
    }
}