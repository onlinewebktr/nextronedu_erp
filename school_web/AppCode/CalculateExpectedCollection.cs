using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class CalculateExpectedCollection
    {
        internal static void calculateExpCollectionMonthwise(string session_id, string branch_id, string MonthName)
        {
            string query = "select * from admission_registor where Session_id=" + session_id + " and Transfer_Status in ('New','NT') and Branch_id='" + branch_id + "' and admissionserialnumber not in (select Admission_no from Graph_calculated_student_monthwise where Session_id=" + session_id + " and Branch_id='" + branch_id + "' and Month='" + MonthName + "' and Type='MonthlyFee')";
            //string query = "select * from admission_registor where Session_id=" + session_id + " and Transfer_Status in ('New','NT') and Branch_id='" + branch_id + "' and admissionserialnumber='1605'";

            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string class_id = dr["Class_id"].ToString();
                    string section = dr["Section"].ToString();
                    string roll_no = dr["rollnumber"].ToString();
                    string std_name = dr["studentname"].ToString();
                    string class_name = dr["class"].ToString();
                    string admission_no = dr["admissionserialnumber"].ToString();
                    string hosteltaken = dr["hosteltaken"].ToString();
                    string parameter = "MonthlyFee";
                    string parameter_id = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                    int hostel_id = My.toint(dr["Hostel_id"].ToString());

                    // confusion 
                    bool day_bording = My.toBool(dr["is_applied_dayboarding"]);
                    bool day_bording_with_lunch = My.toBool(dr["day_boarding_with_lunch"]);

                    string group_id = "3";
                    string category_id = dr["category_id"].ToString();
                    string sub_category_id = dr["SubCategory_id"].ToString();
                    string session_name = dr["session"].ToString();
                    string Transfer_Status = dr["Transfer_Status"].ToString();

                    string IsBoarding = "0";
                    string parameteridS = "4";
                    string LunchMnthName = "";
                    string LunchMnthId = "";

                    string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        LunchMnthName = dts.Rows[0]["Month_name"].ToString();
                        LunchMnthId = dts.Rows[0]["Month_id"].ToString();
                        IsBoarding = "1";
                    }

                    string transportID = "";
                    string transportationtaken = dr["transportationtaken"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        transportID = "0";
                    }
                    else
                    {
                        transportID = dr["Transportation_Id"].ToString();
                    }

                    find_estimated_collection(class_id, section, roll_no, std_name, class_name, admission_no, hosteltaken, parameter, parameter_id, hostel_id, day_bording, day_bording_with_lunch, group_id, category_id, sub_category_id, session_name, Transfer_Status, IsBoarding, parameteridS, LunchMnthName, LunchMnthId, transportID, transportationtaken, session_id, branch_id, MonthName);

                    save_calculated_student(session_id, class_id, branch_id, admission_no, MonthName, parameter);
                }
            }
        }



        private static void find_estimated_collection(string class_id, string section, string roll_no, string std_name, string class_name, string admission_no, string hosteltaken, string parameter, string parameter_id, int hostel_id, bool day_bording, bool day_bording_with_lunch, string group_id, string category_id, string sub_category_id, string session_name, string Transfer_Status, string IsBoarding, string parameteridS, string LunchMnthName, string LunchMnthId, string transportID, string transportationtaken, string session_id, string branch_id, string MonthName)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("value");
            dtDatas.Columns.Add("fee_amount");
            dtDatas.Columns.Add("discount_per");
            dtDatas.Columns.Add("paid_peev");
            dtDatas.Columns.Add("paid_status");
            dtDatas.Columns.Add("bac_colour");
            int temp;
            List<string> lst = new List<string>();
            string temp_month = MonthName;

            DataRow drNewRow = dtDatas.NewRow();
            drNewRow["Month"] = temp_month;
            drNewRow["value"] = false;
            drNewRow["paid_status"] = "NotCreated";
            dtDatas.Rows.Add(drNewRow);
            dtDatas.AcceptChanges();

            bind_monthly_fee(dtDatas, class_id, section, roll_no, std_name, class_name, admission_no, hosteltaken, parameter, parameter_id, hostel_id, day_bording, day_bording_with_lunch, group_id, category_id, sub_category_id, session_name, Transfer_Status, IsBoarding, parameteridS, LunchMnthName, LunchMnthId, transportID, transportationtaken, session_id, branch_id);
        }


        private static void bind_monthly_fee(DataTable dtDatas, string class_id, string section, string roll_no, string std_name, string class_name, string admission_no, string hosteltaken, string parameter, string parameter_id, int hostel_id, bool day_bording, bool day_bording_with_lunch, string group_id, string category_id, string sub_category_id, string session_name, string Transfer_Status, string IsBoarding, string parameteridS, string LunchMnthName, string LunchMnthId, string transportID, string transportationtaken, string session_id, string branch_id)
        {
            double fee_amount = 0, disc_amount = 0, paid_previously = 0;
            bool success = false;
            DataTable fdt = new DataTable();

            foreach (DataRow drMonth in dtDatas.Rows)
            {
                string cunrt_session = My.get_session();
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);
                string MonthName = drMonth["Month"].ToString();


                fee_amount = 0; disc_amount = 0; paid_previously = 0;
                DataTable feedt = new DataTable();
                if (IsBoarding == "1")
                {
                    int mnthids = My.tomonth_number(MonthName);
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
                if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + admission_no + "' and session='" + session_name + "' and month='" + MonthName + "' and parameter='" + parameter + "'").Rows.Count > 0)
                {
                    //feedt = My.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter, feetype content,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,'0' as disc_amount, '0'  previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + admission_no + "' and session='" + session_name + "' and month='" + MonthName + "' and parameter='" + parameter + "' and content_id!='6121' ");
                    //type = "Calculated";


                    feedt = My.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter, feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + admission_no + "' and session='" + session_name + "' and month='" + MonthName + "' and parameter='" + parameter + "' and content_id!='6121' ");
                    type = "Calculated";
                }
                else
                {
                    Dictionary<string, object> dc = new Dictionary<string, object>();
                    dc["admission_no"] = admission_no;
                    dc["session_id"] = session_id;
                    dc["class"] = class_name;
                    dc["session"] = session_name;
                    dc["class_id"] = class_id;
                    dc["hosteltaken"] = hosteltaken;
                    dc["months"] = MonthName;
                    dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                    dc["hostel_id"] = hosteltaken.ToLower() == "yes" ? My.toint(hostel_id) : 0;
                    dc["day_boarding"] = day_bording;
                    dc["day_boarding_lunch"] = day_bording_with_lunch;
                    dc["category_id"] = category_id;
                    dc["sub_category_id"] = sub_category_id;
                    dc["transportportation_id"] = transportID;
                    dc["parameter_id"] = parameteridS;
                    //new08/08/2022 
                    string monthid = My.tomonth_numberstring(MonthName);
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
                    dr["total_payable"] = My.toDouble(dr["amount"]) - My.toDouble(dr["disc_amount"]);
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
 
            if (fdt.Rows.Count > 0)
            {
                save_temp_fee(fdt, class_id, section, roll_no, std_name, class_name, admission_no, hosteltaken, parameter, parameter_id, hostel_id, day_bording, day_bording_with_lunch, group_id, category_id, sub_category_id, session_name, Transfer_Status, IsBoarding, parameteridS, LunchMnthName, LunchMnthId, transportID, transportationtaken, session_id, branch_id);
            }
        }

        My mycode = new My();
        private static void save_temp_fee(DataTable fdt, string class_id, string section, string roll_no, string std_name, string class_name, string admission_no, string hosteltaken, string parameter, string parameter_id, int hostel_id, bool day_bording, bool day_bording_with_lunch, string group_id, string category_id, string sub_category_id, string session_name, string Transfer_Status, string IsBoarding, string parameteridS, string LunchMnthName, string LunchMnthId, string transportID, string transportationtaken, string session_id, string branch_id)
        {
            DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtime.ToString("ddMMyyyy");
            string idate = dtime.ToString("yyyyMMdd");
            string time = dtime.ToString("hhmmss");

            //My.exeSql("delete from Temp_typewise_estimated_fee where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Parameter='" + parameter + "'");
            foreach (DataRow dr in fdt.Rows)
            {
                if (My.toDouble(dr["amount"].ToString()) > 0)
                {
                    if (IsUserExist("select Id from Temp_typewise_estimated_fee where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Parameter='" + parameter + "' and Fee_head_type='" + dr["content"].ToString() + "' and Month_name='" + dr["months"].ToString() + "'"))
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Temp_typewise_estimated_fee (Session_id,Session,Class_id,Class_name,Admission_no,Section,Parameter,Fee_head_type,Month_name,Amount,Disc_amount,Payable_amounts,Group_id,Updated_date,Updated_time,Updated_idate,Branch_id) values (@Session_id,@Session,@Class_id,@Class_name,@Admission_no,@Section,@Parameter,@Fee_head_type,@Month_name,@Amount,@Disc_amount,@Payable_amounts,@Group_id,@Updated_date,@Updated_time,@Updated_idate,@Branch_id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", session_id);
                        cmd.Parameters.AddWithValue("@Session", session_name);
                        cmd.Parameters.AddWithValue("@Class_id", class_id);
                        cmd.Parameters.AddWithValue("@Class_name", class_name);
                        cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                        cmd.Parameters.AddWithValue("@Section", section);
                        cmd.Parameters.AddWithValue("@Parameter", parameter);
                        cmd.Parameters.AddWithValue("@Fee_head_type", dr["content"].ToString());
                        cmd.Parameters.AddWithValue("@Month_name", dr["months"].ToString());
                        cmd.Parameters.AddWithValue("@Amount", dr["amount"].ToString());
                        cmd.Parameters.AddWithValue("@Disc_amount", dr["disc_amount"].ToString());
                        cmd.Parameters.AddWithValue("@Payable_amounts", dr["total_payable"].ToString());
                        cmd.Parameters.AddWithValue("@Group_id", "");
                        cmd.Parameters.AddWithValue("@Updated_date", date);
                        cmd.Parameters.AddWithValue("@Updated_time", time);
                        cmd.Parameters.AddWithValue("@Updated_idate", idate);
                        cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "Update Temp_typewise_estimated_fee set Amount=@Amount,Disc_amount=@Disc_amount,Payable_amounts=@Payable_amounts,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Parameter='" + parameter + "' and Fee_head_type='" + dr["content"].ToString() + "' and Month_name='" + dr["months"].ToString() + "'";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Amount", dr["amount"].ToString());
                        cmd.Parameters.AddWithValue("@Disc_amount", dr["disc_amount"].ToString());
                        cmd.Parameters.AddWithValue("@Payable_amounts", dr["total_payable"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date", date);
                        cmd.Parameters.AddWithValue("@Updated_time", time);
                        cmd.Parameters.AddWithValue("@Updated_idate", idate);
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
            }
        }

        private static bool IsUserExist(string query)
        {
            bool status = false;
            DataTable dtTemp = FillDatastatic(query);
            if (dtTemp.Rows.Count == 0)
            {
                status = true;
            }
            return status;
        }



        private static string find_month(string month)
        {
            string next = "nn";
            switch (month)
            {
                case "January":
                    next = "February";
                    return next;

                case "February":
                    next = "March";
                    return next;

                case "March":
                    next = "April";
                    return next;

                case "April":
                    next = "May";
                    return next;

                case "May":
                    next = "June";
                    return next;

                case "June":
                    next = "July";
                    return next;

                case "July":
                    next = "August";
                    return next;

                case "August":
                    next = "September";
                    return next;

                case "September":
                    next = "October";
                    return next;

                case "October":
                    next = "November";
                    return next;

                case "November":
                    next = "December";
                    return next;

                case "December":
                    next = "January";
                    return next;

            }
            return next;
        }


        private static DataTable FillDatastatic(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, My.conn);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }
            }
            catch (Exception ex)
            {
            }
            return dtc;
        }


        private static void save_calculated_student(string session_id, string class_id, string branch_id, string admission_no, string MonthName, string Type)
        {
            if (IsUserExist("select Id from Graph_calculated_student_monthwise where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id=" + class_id + " and Month='" + MonthName + "' and Admission_no='" + admission_no + "' and Type='" + Type + "'"))
            {
                SqlCommand cmds;
                string querys = "INSERT INTO Graph_calculated_student_monthwise (Session_id,Branch_id,Class_id,Admission_no,Month,Type) values (@Session_id,@Branch_id,@Class_id,@Admission_no,@Month,@Type)";
                cmds = new SqlCommand(querys);
                cmds.Parameters.AddWithValue("@Session_id", session_id);
                cmds.Parameters.AddWithValue("@Branch_id", branch_id);
                cmds.Parameters.AddWithValue("@Class_id", class_id);
                cmds.Parameters.AddWithValue("@Admission_no", admission_no);
                cmds.Parameters.AddWithValue("@Month", MonthName);
                cmds.Parameters.AddWithValue("@Type", Type);
                if (My.InsertUpdateData(cmds))
                {
                }
            }
        }



        //========================================Admission
        internal static void calculateExpCollectionAdmission(string session_id, string branch_id, string MonthName, string Type)
        {
            string query = "";
            if (Type == "AdmissionFee")
            {
                query = "select * from admission_registor where Session_id=" + session_id + " and Branch_id='" + branch_id + "'   and  Transfer_Status='New' and Status='1' and admissionserialnumber not in (select Admission_no from Graph_calculated_student_monthwise where Session_id=" + session_id + " and Branch_id='" + branch_id + "' and Type='" + Type + "')";
            }
            else
            {
                query = "select * from admission_registor where Session_id=" + session_id + " and Branch_id='" + branch_id + "'  and  Transfer_Status='NT' and Status='1' and admissionserialnumber not in (select Admission_no from Graph_calculated_student_monthwise where Session_id=" + session_id + " and Branch_id='" + branch_id + "' and Type='" + Type + "')";
            }

            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string class_id = dr["Class_id"].ToString();
                    string section = dr["Section"].ToString();
                    string roll_no = dr["rollnumber"].ToString();
                    string std_name = dr["studentname"].ToString();
                    string class_name = dr["class"].ToString();
                    string admission_no = dr["admissionserialnumber"].ToString();
                    string hosteltaken = dr["hosteltaken"].ToString();
                    string parameter = Type;
                    string parameter_id = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                    int hostel_id = My.toint(dr["Hostel_id"].ToString());

                    // confusion 
                    bool day_bording = My.toBool(dr["is_applied_dayboarding"]);
                    bool day_bording_with_lunch = My.toBool(dr["day_boarding_with_lunch"]);

                    string group_id = "3";
                    string category_id = dr["category_id"].ToString();
                    string sub_category_id = dr["SubCategory_id"].ToString();
                    string session_name = dr["session"].ToString();
                    string Transfer_Status = dr["Transfer_Status"].ToString();

                    string IsBoarding = "0";
                    string parameteridS = "4";
                    string LunchMnthName = "";
                    string LunchMnthId = "";

                    string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        LunchMnthName = dts.Rows[0]["Month_name"].ToString();
                        LunchMnthId = dts.Rows[0]["Month_id"].ToString();
                        IsBoarding = "1";
                    }

                    string transportID = "";
                    string transportationtaken = dr["transportationtaken"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        transportID = "0";
                    }
                    else
                    {
                        transportID = dr["Transportation_Id"].ToString();
                    }

                    find_estimated_collection_admission(class_id, section, roll_no, std_name, class_name, admission_no, hosteltaken, parameter, parameter_id, hostel_id, day_bording, day_bording_with_lunch, group_id, category_id, sub_category_id, session_name, Transfer_Status, IsBoarding, parameteridS, LunchMnthName, LunchMnthId, transportID, transportationtaken, session_id, branch_id, MonthName, Type);
                    save_calculated_student(session_id, class_id, branch_id, admission_no, MonthName, Type);
                }
            }
            else
            {
                DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dtime.ToString("ddMMyyyy");
                string idate = dtime.ToString("yyyyMMdd");
                string time = dtime.ToString("hhmmss");
                string session_names = My.get_session();
                string queryc = "select Course_Name,course_id from Add_course_table order by Position asc";
                DataTable dtc = FillDatastatic(queryc);
                foreach (DataRow drc in dtc.Rows)
                {
                    if (IsUserExist("select Id from Temp_typewise_estimated_fee where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + drc["course_id"].ToString() + "'  and Parameter='" + Type + "'  and Month_name='" + MonthName + "'"))
                    {
                        SqlCommand cmd;
                        string querys = "INSERT INTO Temp_typewise_estimated_fee (Session_id,Session,Class_id,Class_name,Admission_no,Section,Parameter,Fee_head_type,Month_name,Amount,Disc_amount,Payable_amounts,Group_id,Updated_date,Updated_time,Updated_idate,Branch_id) values (@Session_id,@Session,@Class_id,@Class_name,@Admission_no,@Section,@Parameter,@Fee_head_type,@Month_name,@Amount,@Disc_amount,@Payable_amounts,@Group_id,@Updated_date,@Updated_time,@Updated_idate,@Branch_id)";
                        cmd = new SqlCommand(querys);
                        cmd.Parameters.AddWithValue("@Session_id", session_id);
                        cmd.Parameters.AddWithValue("@Session", session_names);
                        cmd.Parameters.AddWithValue("@Class_id", drc["course_id"].ToString());
                        cmd.Parameters.AddWithValue("@Class_name", drc["Course_Name"].ToString());
                        cmd.Parameters.AddWithValue("@Admission_no", "");
                        cmd.Parameters.AddWithValue("@Section", "");
                        cmd.Parameters.AddWithValue("@Parameter", Type);
                        cmd.Parameters.AddWithValue("@Fee_head_type", "NA");
                        cmd.Parameters.AddWithValue("@Month_name", MonthName);
                        cmd.Parameters.AddWithValue("@Amount", "0");
                        cmd.Parameters.AddWithValue("@Disc_amount", "0");
                        cmd.Parameters.AddWithValue("@Payable_amounts", "0");
                        cmd.Parameters.AddWithValue("@Group_id", "");
                        cmd.Parameters.AddWithValue("@Updated_date", date);
                        cmd.Parameters.AddWithValue("@Updated_time", time);
                        cmd.Parameters.AddWithValue("@Updated_idate", idate);
                        cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
            }
        }


        private static void find_estimated_collection_admission(string class_id, string section, string roll_no, string std_name, string class_name, string admission_no, string hosteltaken, string parameter, string parameter_id, int hostel_id, bool day_bording, bool day_bording_with_lunch, string group_id, string category_id, string sub_category_id, string session_name, string Transfer_Status, string IsBoarding, string parameteridS, string LunchMnthName, string LunchMnthId, string transportID, string transportationtaken, string session_id, string branch_id, string MonthName, string Type)
        {
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and (parameter_id='1' or parameter_id='5') and session='" + session_name + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + class_id + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + session_name + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and parameter='" + parameter + "' and session='" + session_name + "')t";
            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and (parameter_id='1' or parameter_id='5') and session='" + session_name + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + class_id + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + session_name + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + session_name + "' and class_id='" + class_id + "')t";
                fee_dt = My.dataTable(qry);
            }
            DataTable dt = FillDatastatic(qry);

            save_temp_fee_admission(dt, class_id, section, roll_no, std_name, class_name, admission_no, hosteltaken, parameter, parameter_id, hostel_id, day_bording, day_bording_with_lunch, group_id, category_id, sub_category_id, session_name, Transfer_Status, IsBoarding, parameteridS, LunchMnthName, LunchMnthId, transportID, transportationtaken, session_id, branch_id, MonthName);
        }

        private static void save_temp_fee_admission(DataTable fdt, string class_id, string section, string roll_no, string std_name, string class_name, string admission_no, string hosteltaken, string parameter, string parameter_id, int hostel_id, bool day_bording, bool day_bording_with_lunch, string group_id, string category_id, string sub_category_id, string session_name, string Transfer_Status, string IsBoarding, string parameteridS, string LunchMnthName, string LunchMnthId, string transportID, string transportationtaken, string session_id, string branch_id, string MonthName)
        {
            DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtime.ToString("ddMMyyyy");
            string idate = dtime.ToString("yyyyMMdd");
            string time = dtime.ToString("hhmmss");

            if (fdt.Rows.Count > 0)
            {
                My.exeSql("delete from Temp_typewise_estimated_fee where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Parameter='" + parameter + "'");
                foreach (DataRow dr in fdt.Rows)
                {
                    if (My.toDouble(dr["net_payable"].ToString()) > 0)
                    {
                        if (IsUserExist("select Id from Temp_typewise_estimated_fee where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Parameter='" + parameter + "' and Fee_head_type='" + dr["feetype"].ToString() + "' and Month_name='" + MonthName + "'"))
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Temp_typewise_estimated_fee (Session_id,Session,Class_id,Class_name,Admission_no,Section,Parameter,Fee_head_type,Month_name,Amount,Disc_amount,Payable_amounts,Group_id,Updated_date,Updated_time,Updated_idate,Branch_id) values (@Session_id,@Session,@Class_id,@Class_name,@Admission_no,@Section,@Parameter,@Fee_head_type,@Month_name,@Amount,@Disc_amount,@Payable_amounts,@Group_id,@Updated_date,@Updated_time,@Updated_idate,@Branch_id)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", session_id);
                            cmd.Parameters.AddWithValue("@Session", session_name);
                            cmd.Parameters.AddWithValue("@Class_id", class_id);
                            cmd.Parameters.AddWithValue("@Class_name", class_name);
                            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                            cmd.Parameters.AddWithValue("@Section", section);
                            cmd.Parameters.AddWithValue("@Parameter", parameter);
                            cmd.Parameters.AddWithValue("@Fee_head_type", dr["feetype"].ToString());
                            cmd.Parameters.AddWithValue("@Month_name", MonthName);
                            cmd.Parameters.AddWithValue("@Amount", dr["payable"].ToString());
                            cmd.Parameters.AddWithValue("@Disc_amount", dr["disc_amount"].ToString());
                            cmd.Parameters.AddWithValue("@Payable_amounts", dr["net_payable"].ToString());
                            cmd.Parameters.AddWithValue("@Group_id", "");
                            cmd.Parameters.AddWithValue("@Updated_date", date);
                            cmd.Parameters.AddWithValue("@Updated_time", time);
                            cmd.Parameters.AddWithValue("@Updated_idate", idate);
                            cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            SqlCommand cmd;
                            string query = "Update Temp_typewise_estimated_fee set Amount=@Amount,Disc_amount=@Disc_amount,Payable_amounts=@Payable_amounts,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Parameter='" + parameter + "' and Fee_head_type='" + dr["feetype"].ToString() + "' and Month_name='" + MonthName + "'";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Amount", dr["payable"].ToString());
                            cmd.Parameters.AddWithValue("@Disc_amount", dr["disc_amount"].ToString());
                            cmd.Parameters.AddWithValue("@Payable_amounts", dr["net_payable"].ToString());
                            cmd.Parameters.AddWithValue("@Updated_date", date);
                            cmd.Parameters.AddWithValue("@Updated_time", time);
                            cmd.Parameters.AddWithValue("@Updated_idate", idate);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                    }
                }
            }
            else
            {
                string querycx = "select Course_Name,course_id from Add_course_table order by Position asc";
                DataTable dtc = FillDatastatic(querycx);
                foreach (DataRow drc in dtc.Rows)
                {
                    if (IsUserExist("select Id from Temp_typewise_estimated_fee where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + drc["course_id"].ToString() + "'  and Parameter='" + parameter + "'  and Month_name='" + MonthName + "'"))
                    {
                        SqlCommand cmd;
                        string querys = "INSERT INTO Temp_typewise_estimated_fee (Session_id,Session,Class_id,Class_name,Admission_no,Section,Parameter,Fee_head_type,Month_name,Amount,Disc_amount,Payable_amounts,Group_id,Updated_date,Updated_time,Updated_idate,Branch_id) values (@Session_id,@Session,@Class_id,@Class_name,@Admission_no,@Section,@Parameter,@Fee_head_type,@Month_name,@Amount,@Disc_amount,@Payable_amounts,@Group_id,@Updated_date,@Updated_time,@Updated_idate,@Branch_id)";
                        cmd = new SqlCommand(querys);
                        cmd.Parameters.AddWithValue("@Session_id", session_id);
                        cmd.Parameters.AddWithValue("@Session", session_name);
                        cmd.Parameters.AddWithValue("@Class_id", drc["course_id"].ToString());
                        cmd.Parameters.AddWithValue("@Class_name", drc["Course_Name"].ToString());
                        cmd.Parameters.AddWithValue("@Admission_no", "");
                        cmd.Parameters.AddWithValue("@Section", "");
                        cmd.Parameters.AddWithValue("@Parameter", parameter);
                        cmd.Parameters.AddWithValue("@Fee_head_type", "NA");
                        cmd.Parameters.AddWithValue("@Month_name", MonthName);
                        cmd.Parameters.AddWithValue("@Amount", "0");
                        cmd.Parameters.AddWithValue("@Disc_amount", "0");
                        cmd.Parameters.AddWithValue("@Payable_amounts", "0");
                        cmd.Parameters.AddWithValue("@Group_id", "");
                        cmd.Parameters.AddWithValue("@Updated_date", date);
                        cmd.Parameters.AddWithValue("@Updated_time", time);
                        cmd.Parameters.AddWithValue("@Updated_idate", idate);
                        cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
            }
        }



        ///*************************
        /////////////////////////////
        ///-------=========================
        //==================================OTHERFEEE
        internal static void calculateExpCollectionOtherFee(string session_id, string branch_id, string MonthName, string Type)
        {
            string query = "";
            query = "select * from admission_registor where Session_id=" + session_id + " and Branch_id='" + branch_id + "'  and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1'";

            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string class_id = dr["Class_id"].ToString();
                    string section = dr["Section"].ToString();
                    string roll_no = dr["rollnumber"].ToString();
                    string std_name = dr["studentname"].ToString();
                    string class_name = dr["class"].ToString();
                    string admission_no = dr["admissionserialnumber"].ToString();
                    string hosteltaken = dr["hosteltaken"].ToString();
                    string parameter = Type;
                    string parameter_id = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                    int hostel_id = My.toint(dr["Hostel_id"].ToString());

                    // confusion 
                    bool day_bording = My.toBool(dr["is_applied_dayboarding"]);
                    bool day_bording_with_lunch = My.toBool(dr["day_boarding_with_lunch"]);

                    string group_id = "3";
                    string category_id = dr["category_id"].ToString();
                    string sub_category_id = dr["SubCategory_id"].ToString();
                    string session_name = dr["session"].ToString();
                    string Transfer_Status = dr["Transfer_Status"].ToString();

                    string IsBoarding = "0";
                    string parameteridS = "4";
                    string LunchMnthName = "";
                    string LunchMnthId = "";

                    string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        LunchMnthName = dts.Rows[0]["Month_name"].ToString();
                        LunchMnthId = dts.Rows[0]["Month_id"].ToString();
                        IsBoarding = "1";
                    }

                    string transportID = "";
                    string transportationtaken = dr["transportationtaken"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        transportID = "0";
                    }
                    else
                    {
                        transportID = dr["Transportation_Id"].ToString();
                    }

                    find_estimated_collection_otherfee(class_id, section, roll_no, std_name, class_name, admission_no, hosteltaken, parameter, parameter_id, hostel_id, day_bording, day_bording_with_lunch, group_id, category_id, sub_category_id, session_name, Transfer_Status, IsBoarding, parameteridS, LunchMnthName, LunchMnthId, transportID, transportationtaken, session_id, branch_id, MonthName, Type);

                }
            }
            else
            {
                DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dtime.ToString("ddMMyyyy");
                string idate = dtime.ToString("yyyyMMdd");
                string time = dtime.ToString("hhmmss");
                string session_names = My.get_session();
                string queryc = "select Course_Name,course_id from Add_course_table order by Position asc";
                DataTable dtc = FillDatastatic(queryc);
                foreach (DataRow drc in dtc.Rows)
                {
                    if (IsUserExist("select Id from Temp_typewise_estimated_fee where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + drc["course_id"].ToString() + "'  and Parameter='" + Type + "'  and Month_name='" + MonthName + "'"))
                    {
                        SqlCommand cmd;
                        string querys = "INSERT INTO Temp_typewise_estimated_fee (Session_id,Session,Class_id,Class_name,Admission_no,Section,Parameter,Fee_head_type,Month_name,Amount,Disc_amount,Payable_amounts,Group_id,Updated_date,Updated_time,Updated_idate,Branch_id) values (@Session_id,@Session,@Class_id,@Class_name,@Admission_no,@Section,@Parameter,@Fee_head_type,@Month_name,@Amount,@Disc_amount,@Payable_amounts,@Group_id,@Updated_date,@Updated_time,@Updated_idate,@Branch_id)";
                        cmd = new SqlCommand(querys);
                        cmd.Parameters.AddWithValue("@Session_id", session_id);
                        cmd.Parameters.AddWithValue("@Session", session_names);
                        cmd.Parameters.AddWithValue("@Class_id", drc["course_id"].ToString());
                        cmd.Parameters.AddWithValue("@Class_name", drc["Course_Name"].ToString());
                        cmd.Parameters.AddWithValue("@Admission_no", "");
                        cmd.Parameters.AddWithValue("@Section", "");
                        cmd.Parameters.AddWithValue("@Parameter", Type);
                        cmd.Parameters.AddWithValue("@Fee_head_type", "NA");
                        cmd.Parameters.AddWithValue("@Month_name", MonthName);
                        cmd.Parameters.AddWithValue("@Amount", "0");
                        cmd.Parameters.AddWithValue("@Disc_amount", "0");
                        cmd.Parameters.AddWithValue("@Payable_amounts", "0");
                        cmd.Parameters.AddWithValue("@Group_id", "");
                        cmd.Parameters.AddWithValue("@Updated_date", date);
                        cmd.Parameters.AddWithValue("@Updated_time", time);
                        cmd.Parameters.AddWithValue("@Updated_idate", idate);
                        cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
            }
        }

        private static void find_estimated_collection_otherfee(string class_id, string section, string roll_no, string std_name, string class_name, string admission_no, string hosteltaken, string parameter, string parameter_id, int hostel_id, bool day_bording, bool day_bording_with_lunch, string group_id, string category_id, string sub_category_id, string session_name, string Transfer_Status, string IsBoarding, string parameteridS, string LunchMnthName, string LunchMnthId, string transportID, string transportationtaken, string session_id, string branch_id, string MonthName, string Type)
        {
            DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtime.ToString("ddMMyyyy");
            string idate = dtime.ToString("yyyyMMdd");
            string time = dtime.ToString("hhmmss");
            string queryc = "Select * from Other_Fee_For_Special_Condition where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' order by Content_Name asc";
            DataTable fdt = FillDatastatic(queryc);
            if (fdt.Rows.Count > 0)
            {
                //My.exeSql("delete from Temp_typewise_estimated_fee where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Parameter='" + parameter + "' and Month_name='" + MonthName + "'");
                foreach (DataRow dr in fdt.Rows)
                {
                    if (My.toDouble(dr["Content_Fee"].ToString()) > 0)
                    {
                        if (IsUserExist("select Id from Temp_typewise_estimated_fee where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Parameter='" + parameter + "' and Fee_head_type='" + dr["Content_Name"].ToString() + "'"))
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Temp_typewise_estimated_fee (Session_id,Session,Class_id,Class_name,Admission_no,Section,Parameter,Fee_head_type,Month_name,Amount,Disc_amount,Payable_amounts,Group_id,Updated_date,Updated_time,Updated_idate,Branch_id) values (@Session_id,@Session,@Class_id,@Class_name,@Admission_no,@Section,@Parameter,@Fee_head_type,@Month_name,@Amount,@Disc_amount,@Payable_amounts,@Group_id,@Updated_date,@Updated_time,@Updated_idate,@Branch_id)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", session_id);
                            cmd.Parameters.AddWithValue("@Session", session_name);
                            cmd.Parameters.AddWithValue("@Class_id", class_id);
                            cmd.Parameters.AddWithValue("@Class_name", class_name);
                            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                            cmd.Parameters.AddWithValue("@Section", section);
                            cmd.Parameters.AddWithValue("@Parameter", parameter);
                            cmd.Parameters.AddWithValue("@Fee_head_type", dr["Content_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Month_name", MonthName);
                            cmd.Parameters.AddWithValue("@Amount", dr["Content_Fee"].ToString());
                            cmd.Parameters.AddWithValue("@Disc_amount", "0");
                            cmd.Parameters.AddWithValue("@Payable_amounts", dr["Content_Fee"].ToString());
                            cmd.Parameters.AddWithValue("@Group_id", "");
                            cmd.Parameters.AddWithValue("@Updated_date", date);
                            cmd.Parameters.AddWithValue("@Updated_time", time);
                            cmd.Parameters.AddWithValue("@Updated_idate", idate);
                            cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            SqlCommand cmd;
                            string query = "Update Temp_typewise_estimated_fee set Amount=@Amount,Disc_amount=@Disc_amount,Payable_amounts=@Payable_amounts,Updated_date=@Updated_date,Updated_time=@Updated_time,Updated_idate=@Updated_idate where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Parameter='" + parameter + "' and Fee_head_type='" + dr["Content_Name"].ToString() + "' and Month_name='" + MonthName + "'";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Amount", dr["Content_Fee"].ToString());
                            cmd.Parameters.AddWithValue("@Disc_amount", "0");
                            cmd.Parameters.AddWithValue("@Payable_amounts", dr["Content_Fee"].ToString());
                            cmd.Parameters.AddWithValue("@Updated_date", date);
                            cmd.Parameters.AddWithValue("@Updated_time", time);
                            cmd.Parameters.AddWithValue("@Updated_idate", idate);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                    }
                }
                save_calculated_student(session_id, class_id, branch_id, admission_no, MonthName, Type);
            }
            else
            {
                string querycx = "select Course_Name,course_id from Add_course_table order by Position asc";
                DataTable dtc = FillDatastatic(querycx);
                foreach (DataRow drc in dtc.Rows)
                {
                    if (IsUserExist("select Id from Temp_typewise_estimated_fee where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + drc["course_id"].ToString() + "'  and Parameter='" + parameter + "'  and Month_name='" + MonthName + "'"))
                    {
                        SqlCommand cmd;
                        string querys = "INSERT INTO Temp_typewise_estimated_fee (Session_id,Session,Class_id,Class_name,Admission_no,Section,Parameter,Fee_head_type,Month_name,Amount,Disc_amount,Payable_amounts,Group_id,Updated_date,Updated_time,Updated_idate,Branch_id) values (@Session_id,@Session,@Class_id,@Class_name,@Admission_no,@Section,@Parameter,@Fee_head_type,@Month_name,@Amount,@Disc_amount,@Payable_amounts,@Group_id,@Updated_date,@Updated_time,@Updated_idate,@Branch_id)";
                        cmd = new SqlCommand(querys);
                        cmd.Parameters.AddWithValue("@Session_id", session_id);
                        cmd.Parameters.AddWithValue("@Session", session_name);
                        cmd.Parameters.AddWithValue("@Class_id", drc["course_id"].ToString());
                        cmd.Parameters.AddWithValue("@Class_name", drc["Course_Name"].ToString());
                        cmd.Parameters.AddWithValue("@Admission_no", "");
                        cmd.Parameters.AddWithValue("@Section", "");
                        cmd.Parameters.AddWithValue("@Parameter", parameter);
                        cmd.Parameters.AddWithValue("@Fee_head_type", "NA");
                        cmd.Parameters.AddWithValue("@Month_name", MonthName);
                        cmd.Parameters.AddWithValue("@Amount", "0");
                        cmd.Parameters.AddWithValue("@Disc_amount", "0");
                        cmd.Parameters.AddWithValue("@Payable_amounts", "0");
                        cmd.Parameters.AddWithValue("@Group_id", "");
                        cmd.Parameters.AddWithValue("@Updated_date", date);
                        cmd.Parameters.AddWithValue("@Updated_time", time);
                        cmd.Parameters.AddWithValue("@Updated_idate", idate);
                        cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
            }
        }

        private static string find_is_added(string session_id, string branch_id, string Type, string admission_no, string class_id, string Content_Name)
        {
            string status = "1";
            string query = "select * from Other_Fee_For_Special_Condition where Session_id=" + session_id + " and Branch_id='" + branch_id + "' and Class_id=" + class_id + " and Content_Name not in(select Fee_head_type from Temp_typewise_estimated_fee where Session_id=" + session_id + " and Branch_id='" + branch_id + "' and Class_id=" + class_id + " and Admission_no='" + admission_no + "' and Fee_head_type='" + Content_Name + "')";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count > 0)
            {
                status = "0";
            }
            return status;
        }
    }
}