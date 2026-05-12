using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class demandBill
    {
        internal static void update_student_dues(string session_id, string class_id, string admission_no, string bill_date, string firm_id, string is_fine_rapeat, SqlConnection con)
        {
            string Discount_on = "";
            My mycode = new My();
            DataTable dt = payments.dataTable("select t1.Transfer_Status_Old,t1.Transfer_Status,t1.session,t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + session_id + "' and t1.Status='1' and t1.Class_id='" + class_id + "' and t1.admissionserialnumber='" + admission_no + "'", con);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    payments.exeSql("Delete from STUDENT_WISE_DUES_For_Demand_BIll where admission_no='" + admission_no + "' and Class_id='" + class_id + "' and Session_id='" + session_id + "'", con);
                    DataTable feedt = new DataTable();
                    string parameter = dr["hosteltaken"].ToString().ToLower() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    Dictionary<string, object> dc1 = get_transport_assined_student(dr["Session_id"].ToString(), dr["Class_id"].ToString(), admission_no, con);
                    string Transport_id = (String)dc1["Transport_id"];
                    string TransportPath_id = (String)dc1["TransportPath_id"];
                    string Boarding_Point_id = (String)dc1["Boarding_Point_id"];
                    string Transport_Assigned_Id = (String)dc1["Transport_Assigned_Id"];
                    string IsBoarding = "0";
                    string parameteridS = "4";
                    string LunchMnthName = "";
                    string LunchMnthId = "";
                    if (dr["Month_id"].ToString() != "")
                    {
                        LunchMnthName = dr["Month_name"].ToString();
                        LunchMnthId = dr["Month_id"].ToString();
                        IsBoarding = "1";
                    }


                    Dictionary<string, object> dc11 = get_hostel_assined_student(dr["Session_id"].ToString(), dr["Class_id"].ToString(), admission_no, con);
                    string Hostel_id = (String)dc11["Hostel_id"];
                    string Room_Category_id = (String)dc11["Room_Category_id"];
                    string From_month_name = (String)dc11["From_month_name"];
                    string From_month_id = (String)dc11["From_month_id"];
                    string Assined_Year_Month = (String)dc11["Assined_Year_Month"];
                    string Hostel_assign_id = (String)dc11["Hostel_assign_id"];


                    DataTable fdt = new DataTable();
                    DataTable dtMnth = payments.dataTable("select Month,Month_Id,Position from Month_Index order by Position asc", con);
                    if (dtMnth.Rows.Count > 0)
                    {
                        foreach (DataRow drMnth in dtMnth.Rows)
                        {
                            if (IsBoarding == "1")
                            {
                                int mnthids = My.toint(drMnth["Month_Id"].ToString());
                                if (My.toint(LunchMnthId) <= mnthids)
                                {
                                    parameteridS = "44";
                                }
                                else
                                {
                                    parameteridS = "4";
                                }
                            }


                            if (payments.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + dr["session"].ToString() + "' and month='" + drMnth["Month"].ToString() + "' and (parameter='" + parameter + "' or parameter='HostelMonthlyFee')", con).Rows.Count > 0)
                            {
                                feedt = payments.dataTable("select '0' as Id,admission_no,class_id Class_id, '" + dr["Session_id"].ToString() + "' as Session_id,parameter,month months,feetype as content,content_id,payable as amount,Disc as disc_amount,paid as Prev_paid,(convert(float, payable)-(convert(float, paid)+convert(float, Disc))) as Dues_amt,'" + drMnth["Position"].ToString() + "' as Month_position,'" + drMnth["Month_Id"].ToString() + "' as Month_id,'" + mycode.date() + "' as Updated_date,'" + mycode.idate() + "' as Updated_time from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + dr["session"].ToString() + "' and month='" + drMnth["Month"].ToString() + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee')", con);
                            }
                            else
                            {
                                fine_calculations(session_id, class_id, admission_no, dr["Session_id"].ToString(), dr["session"].ToString(), drMnth["Month"].ToString(), drMnth["Month_Id"].ToString(), "1", is_fine_rapeat, bill_date, con);
                                save_fine_amount(session_id, class_id, admission_no, dr["Session_id"].ToString(), dr["session"].ToString(), drMnth["Month"].ToString(), drMnth["Month_Id"].ToString(), "1", drMnth["Position"].ToString(), parameter, con);

                                Dictionary<string, object> dc = new Dictionary<string, object>();
                                dc["admission_no"] = admission_no;
                                dc["session_id"] = dr["Session_id"].ToString();
                                dc["class"] = dr["class"].ToString();
                                dc["session"] = dr["session"].ToString();
                                dc["class_id"] = dr["Class_id"].ToString();
                                dc["hosteltaken"] = dr["hosteltaken"].ToString().ToLower();
                                dc["months"] = drMnth["Month"].ToString();
                                dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                                dc["hostel_id"] = Hostel_id;
                                dc["Room_Category_id"] = Room_Category_id;
                                dc["Hostel_assig_id"] = Hostel_assign_id;
                                dc["day_boarding"] = My.toBool(dr["is_applied_dayboarding"]);
                                dc["day_boarding_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                                dc["category_id"] = dr["category_id"].ToString();
                                dc["sub_category_id"] = dr["SubCategory_id"].ToString();
                                dc["TransportationPath_id"] = TransportPath_id;
                                dc["transportportation_id"] = Transport_id;
                                dc["Boarding_Point_id"] = Boarding_Point_id;
                                dc["parameter_id"] = parameteridS;
                                dc["Month_position"] = drMnth["Position"].ToString();
                                dc["Month_id"] = drMnth["Month_Id"].ToString();
                                dc["Updated_date"] = mycode.date();
                                dc["Updated_time"] = mycode.time();
                                string cunrt_session = dr["session"].ToString();
                                string[] stringSeparators = new string[] { "-" };
                                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                                string session_frst_year = arr[0];
                                string session_last_year = arr[1];
                                int session_s_year = My.toint(session_frst_year);
                                int s_year = My.toint(session_frst_year);
                                string monthid = My.tomonth_numberstring(drMnth["Month"].ToString());
                                int pay_month = My.toint(monthid);
                                s_year = check_start_months(pay_month, s_year, con);
                                dc["monthid"] = s_year + monthid;
                                feedt = dataTableSP("sp_fetch_dues_of_student", dc, con);
                            }

                            foreach (DataRow drs in feedt.Rows)
                            {
                                try
                                {
                                    fdt.Rows.Add(drs.ItemArray);
                                }
                                catch
                                {
                                    foreach (DataColumn dc in feedt.Columns)
                                    {
                                        fdt.Columns.Add(dc.ColumnName);
                                    }
                                    fdt.Rows.Add(drs.ItemArray);
                                }
                            }
                        }

                        if (fdt.Rows.Count > 0)
                        {
                            SqlBulkCopy sbc = null;
                            try
                            {
                                sbc = new SqlBulkCopy(con);
                                sbc.DestinationTableName = "STUDENT_WISE_DUES_For_Demand_BIll"; 
                                sbc.BatchSize = 10000;
                                sbc.BulkCopyTimeout = 0;
                                sbc.NotifyAfter = 10;
                                sbc.WriteToServer(fdt);
                            }
                            catch (Exception ex)
                            {
                            }
                            finally
                            {

                                if (sbc != null)
                                {
                                    sbc.Close();
                                }
                            }
                        }
                    }



                    //===Admission Annual 
                    string Transfer_Status = dr["Transfer_Status"].ToString();
                    string studenttype = dr["Transfer_Status"].ToString();
                    if (dr["Transfer_Status"].ToString().ToUpper() == "TRANSFERRED")
                    {
                        studenttype = dr["Transfer_Status_Old"].ToString();
                        Transfer_Status = dr["Transfer_Status_Old"].ToString();
                    }

                    string parameterAd = ""; string parameter_idAd = "";
                    if (studenttype == "New")
                    {
                        parameterAd = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelAdmissionFee" : "AdmissionFee";
                        parameter_idAd = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "5" : "1";
                        Discount_on = "Admission";
                    }
                    else
                    {
                        parameterAd = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelAnnualFee" : "AnnualFee";
                        parameter_idAd = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "6" : "2";
                        Discount_on = "Annual";
                    }

                    string qry = "select '0' as Id,*,(amount - cast(disc_amount as float) - cast(Prev_paid as float)) Dues_amt,'1' as Month_position,'0' as Month_id,'" + mycode.date() + "' as Updated_date,'" + mycode.time() + "' as Updated_time from (select admission_no admission_no, Class_id class_id, '" + dr["Session_id"] + "' Session_id, parameter, 'April' as months, feetype content, content_id, cast(payable as float) amount, Disc as disc_amount, paid Prev_paid  from dbo.[Typewise_fee_collection] WHERE admission_no = '" + admission_no + "' and parameter = '" + parameterAd + "' and session = '" + dr["session"].ToString() + "') t";
                    DataTable fee_dt = payments.dataTable(qry, con);
                    if (fee_dt.Rows.Count == 0)
                    {
                        if (Hostel_id == "0")
                        {
                            qry = "select '0' as Id,Admission_No admission_no, '" + dr["Class_id"].ToString() + "' Class_id,Session_id,Type_Mode as parameter,'April' as months,Perticular as content,'ANN01' as content_id,Amount as amount, '0' disc_amount,'0' as Prev_paid, Amount as Dues_amt,'1' as Month_position,'0' as Month_id,'" + mycode.date() + "' as Updated_date,'" + mycode.time() + "' as Updated_time from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + dr["session"].ToString() + "' and Admission_No='" + admission_no + "' UNION ALL  select '0' as Id,*,(amount-cast(disc_amount as float)) Dues_amt,'1' as Month_position,'0' as Month_id,'" + mycode.date() + "' as Updated_date,'" + mycode.time() + "' as Updated_time from(select '" + admission_no + "' as admission_no,class_id Class_id,fmc.session_id Session_id,parameter,'April' as months,cm.content content,cm.content_id,cast(fmc.amount as float) amount,isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and parameter_id='" + parameter_idAd + "' and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + dr["Class_id"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_idAd + "' and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount,'0' Prev_paid from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameterAd + "'  and session='" + dr["session"].ToString() + "' and class_id='" + dr["Class_id"].ToString() + "')t";
                        }
                        else
                        {
                            qry = "select '0' as Id,Admission_No admission_no, '" + dr["Class_id"].ToString() + "' Class_id,Session_id,Type_Mode as parameter,'April' as months,Perticular as content,'ANN01' as content_id,Amount as amount, '0' disc_amount,'0' as Prev_paid, Amount as Dues_amt,'1' as Month_position,'0' as Month_id,'" + mycode.date() + "' as Updated_date,'" + mycode.time() + "' as Updated_time from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + dr["session"].ToString() + "' and Admission_No='" + admission_no + "' UNION ALL  select '0' as Id,*,(amount-cast(disc_amount as float)) Dues_amt,'1' as Month_position,'0' as Month_id,'" + mycode.date() + "' as Updated_date,'" + mycode.time() + "' as Updated_time from(select '" + admission_no + "' as admission_no,class_id Class_id,fmc.session_id Session_id,parameter,'April' as months,cm.content content,cm.content_id,cast(fmc.amount as float) amount,isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id='" + Hostel_id + "' and admission_no='" + admission_no + "'  and parameter_id='" + parameter_idAd + "' and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Hostel_id=4 and Class_id='" + dr["Class_id"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_idAd + "' and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount,'0' Prev_paid from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameterAd + "'  and session='" + dr["session"].ToString() + "' and class_id='" + dr["Class_id"].ToString() + "' and fmc.Hostel_Id='" + Hostel_id + "')t";
                        }
                        fee_dt = payments.dataTable(qry, con);
                    }



                    if (fee_dt.Rows.Count > 0)
                    {
                        SqlBulkCopy sbc = null;
                        try
                        {
                            sbc = new SqlBulkCopy(con);
                            sbc.DestinationTableName = "STUDENT_WISE_DUES_For_Demand_BIll";
                            sbc.BatchSize = 10000;
                            sbc.BulkCopyTimeout = 0;
                            sbc.NotifyAfter = 10;
                            sbc.WriteToServer(fee_dt);
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {

                            if (sbc != null)
                            {
                                sbc.Close();
                            }
                        }
                    }
                }
            }
        }



        private static Dictionary<string, object> get_transport_assined_student(string Session_id, string Class_id, string Admission_no, SqlConnection con)
        {
            string path = "Select top 1 Pathname from TransportationPath where TransportationPath_id=t1.TransportPath_id";
            string transportname = "Select top 1 transport_name from Transport_Master where transport_id=t1.transport_id";
            string Bus_no = "Select top 1 Bus_no from Transport_Master where transport_id=t1.transport_id";
            string seatname = "Select top 1 Sheet_No from Transport_Path_Mapping_With_Sheet where Transportation_Id=t1.transport_id and TransportationPath_id=t1.TransportPath_id  and Sheet_Id=t1.Sheet_Id";
            Dictionary<string, object> dc = new Dictionary<string, object>();
            DataTable dt = FillDatastatic("select top 1 *,(" + path + ") as path,(" + transportname + ") as transportname,(" + seatname + ") as seatname,(" + Bus_no + ") as Bus_no,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id = t1.TransportPath_id and Transportation_Id = t1.transport_id and Boarding_Point_id = t1.Boarding_Point_id and Session_Id = " + Session_id + " order by Id desc) as Boarding_Point  from Student_mapping_with_TransportPath t1 where t1.Session_id='" + Session_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Class_id='" + Class_id + "' order by t1.id desc", con);
            if (dt.Rows.Count == 0)
            {
                dc["Transport_id"] = "0";
                dc["TransportPath_id"] = "0";
                dc["Boarding_Point_id"] = "0";
                dc["Transport_Assigned_Id"] = "0";
                dc["Month_name"] = "0";
                dc["Month_id"] = "0";
                dc["Year_month"] = "0";
                dc["Sheet_Id"] = "0";
                dc["RowId"] = "0";
            }
            else
            {
                dc["Transport_id"] = dt.Rows[0]["transport_id"].ToString();
                dc["TransportPath_id"] = dt.Rows[0]["TransportPath_id"].ToString();
                dc["Boarding_Point_id"] = dt.Rows[0]["Boarding_Point_id"].ToString();
                dc["Transport_Assigned_Id"] = dt.Rows[0]["Transport_Assigned_Id"].ToString();
                dc["Month_name"] = dt.Rows[0]["Month_name"].ToString();
                dc["Month_id"] = dt.Rows[0]["Month_id"].ToString();
                dc["Year_month"] = dt.Rows[0]["Year_month"].ToString();
                dc["Sheet_Id"] = dt.Rows[0]["Sheet_Id"].ToString();
                dc["Transportpathpath"] = dt.Rows[0]["path"].ToString();
                dc["Transportname"] = dt.Rows[0]["transportname"].ToString() + "{ BUS No." + dt.Rows[0]["Bus_no"].ToString() + "}";
                dc["seatname"] = dt.Rows[0]["seatname"].ToString();
                dc["Boarding_Point"] = dt.Rows[0]["Boarding_Point"].ToString();
                dc["RowId"] = dt.Rows[0]["Id"].ToString();
            }
            return dc;
        }





        private static Dictionary<string, object> get_hostel_assined_student(string Session_id, string Class_id, string Admission_no, SqlConnection con)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            DataTable dt = FillDatastatic("select * from Hostel_assign_master where Session_id='" + Session_id + "' and Admission_no='" + Admission_no + "' and Class_id='" + Class_id + "' and Status=1", con);
            if (dt.Rows.Count == 0)
            {
                dc["Hostel_id"] = "0";
                dc["Room_Category_id"] = "0";
                dc["From_month_name"] = "0";
                dc["From_month_id"] = "0";
                dc["Assined_Year_Month"] = "0";
                dc["Hostel_assign_id"] = "0";
                dc["Bed_id"] = "0";
            }
            else
            {
                dc["Hostel_id"] = dt.Rows[0]["Hostel_id"].ToString();
                dc["Room_Category_id"] = dt.Rows[0]["Category_id"].ToString();
                dc["From_month_name"] = dt.Rows[0]["From_month_name"].ToString();
                dc["From_month_id"] = dt.Rows[0]["From_month_id"].ToString();
                dc["Assined_Year_Month"] = dt.Rows[0]["Assined_Year_Month"].ToString();
                dc["Hostel_assign_id"] = dt.Rows[0]["Hostel_assign_id"].ToString();
                dc["Bed_id"] = dt.Rows[0]["Bed_id"].ToString();
            }
            return dc;
        }

        private static DataTable FillDatastatic(string query, SqlConnection con)
        {
            var ds = dataSet(query, con);
            return ds.Tables[0];
        }

        public static DataSet dataSet(string query, SqlConnection con)
        {
            var ad = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            ad.Fill(ds);
            return ds;
        }



        public static DataTable dataTableSP(string sp_name, Dictionary<string, object> param, SqlConnection con)
        {
            SqlCommand comm = con.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = sp_name;
            if (param != null)
                foreach (KeyValuePair<string, object> kvp in param)
                    comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
            SqlDataAdapter ad = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return ds.Tables[0];
        }


        internal static int check_start_months(int pay_month, int s_year, SqlConnection con)
        {
            string pay_month_two_digit = My.getMonthS_twoDigit(pay_month.ToString());
            string sessionMonth = get_starting_month(con);
            if (sessionMonth == "02")
            {
                if (pay_month_two_digit == "01")
                {
                    s_year = s_year + 1;
                }
            }
            if (sessionMonth == "03")
            {
                if (pay_month_two_digit == "01" || pay_month_two_digit == "02")
                {
                    s_year = s_year + 1;
                }
            }
            if (sessionMonth == "04")
            {
                if (pay_month_two_digit == "01" || pay_month_two_digit == "02" || pay_month_two_digit == "03")
                {
                    s_year = s_year + 1;
                }
            }
            return s_year;
        }
        private static string get_starting_month(SqlConnection con)
        {
            string query = "select top 1 Month_Id from dbo.[Month_Index]  order by Position asc";
            DataTable dt = FillDatastatic(query, con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }



        private static void save_fine_amount(string session_id, string class_id, string admission_no, string Session_id, string session, string Month_name, string Month_Id, string Branch_id, string MonthPosition, string parameter, SqlConnection con)
        {
            My mycode = new My();
            DataTable dtFine = payments.dataTable("select isnull(sum(convert(float, Fine_amount)),0) as Fine_amount from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + Month_Id + "' and Branch_id='" + Branch_id + "'", con);
            if (dtFine.Rows.Count > 0)
            {
                if (My.toIntS(dtFine.Rows[0]["Fine_amount"].ToString()) > 0)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO STUDENT_WISE_DUES_For_Demand_BIll (admission_no,Class_id,Session_id,parameter,months,content,content_id,amount,disc_amount,Prev_paid,Dues_amt,Month_position,Month_id,Updated_date,Updated_time) values (@admission_no,@Class_id,@Session_id,@parameter,@months,@content,@content_id,@amount,@disc_amount,@Prev_paid,@Dues_amt,@Month_position,@Month_id,@Updated_date,@Updated_time);";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@admission_no", admission_no);
                    cmd.Parameters.AddWithValue("@Class_id", class_id);
                    cmd.Parameters.AddWithValue("@Session_id", session_id);
                    cmd.Parameters.AddWithValue("@parameter", parameter);
                    cmd.Parameters.AddWithValue("@months", Month_name);
                    cmd.Parameters.AddWithValue("@content", "Late Fine");
                    cmd.Parameters.AddWithValue("@content_id", "6121");
                    cmd.Parameters.AddWithValue("@amount", dtFine.Rows[0]["Fine_amount"].ToString());
                    cmd.Parameters.AddWithValue("@disc_amount", "0.00");
                    cmd.Parameters.AddWithValue("@Prev_paid", "0.00");
                    cmd.Parameters.AddWithValue("@Dues_amt", dtFine.Rows[0]["Fine_amount"].ToString());
                    cmd.Parameters.AddWithValue("@Month_position", MonthPosition);
                    cmd.Parameters.AddWithValue("@Month_id", Month_Id);
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                    if (payments.InsertUpdateData(cmd, con))
                    {
                    }
                }
            }
        }

        private static void fine_calculations(string session_id, string class_id, string admission_no, string Session_id, string session, string Month_name, string Month_Id, string Branch_id, string isFineRepeate, string pay_date, SqlConnection con)
        {
            string qry = "delete from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Branch_id='" + Branch_id + "' and Month_id='" + Month_Id + "'";
            payments.exeSql(qry, con);

            string Is_next_month_fine_calculate = "0";
            string flags1 = "0";
            My mycode = new My();
            DataTable dty = payments.dataTable("select Fine_mode from globle_data where Fine_mode='2'", con);
            if (dty.Rows.Count != 0)
            {
                #region DayRanGEWise 
                int payidate = My.DateConvertToIdate(pay_date);


                //Advance Payment Check
                string crnt_year = mycode.year();
                string cunrt_session = session;
                string session_frst_year = cunrt_session.Substring(0, 4);
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);



                string CheckedMonthN = Month_name;
                int mnth_idss = My.tomonth_number(CheckedMonthN);
                string pay_month_two_digit = My.getMonthS_twoDigit(mnth_idss.ToString());
                int pay_month = My.toint(pay_month_two_digit);
                s_year = check_start_months(pay_month, s_year, con);

                int pay_month_with_year = My.toint(s_year + pay_month_two_digit);
                int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                //Advance Payment Check



                if (crnt_month_with_year >= pay_month_with_year)
                {
                    DataTable dtz = payments.dataTable("select top 1 * from Fine_master_day_range", con);
                    if (dtz.Rows.Count != 0)
                    {
                        string FineType = "DayWise";
                        string last_day_of_payments = "01" + "/" + pay_month_two_digit + "/" + s_year;

                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);

                        string late_fine_between_day = startdate1 + " to " + enddate1;
                        string late_fine_no_of_day_month = totaldays.ToString();
                        string fine_date_From = last_day_of_payments;
                        string fine_date_To = pay_date;

                        DataTable dt_fine = payments.dataTable("select top 1 * from Fine_master_day_range where No_of_day>" + totaldays + " order by No_of_day asc", con);
                        if (dt_fine.Rows.Count != 0)
                        {
                            save_fine_amount(session_id, admission_no, pay_month_two_digit, My.toDouble(dt_fine.Rows[0]["Fine_amount"].ToString()), Branch_id, con);
                        }
                        else
                        {
                            DataTable dt_fines = payments.dataTable("select top 1 * from Fine_master_day_range order by No_of_day desc", con);
                            if (dt_fines.Rows.Count != 0)
                            {
                                save_fine_amount(session_id, admission_no, pay_month_two_digit, My.toDouble(dt_fines.Rows[0]["Fine_amount"].ToString()), Branch_id, con);
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region FINES
                DataTable dt = payments.dataTable("select top 1 * from Fine_master where Status='1' and Session_id='" + session_id + "'", con);
                if (dt.Rows.Count != 0)
                {
                    try
                    {
                         Is_next_month_fine_calculate = dt.Rows[0]["Is_next_month_fine_calculate"].ToString();

                    }
                    catch
                    {
                          Is_next_month_fine_calculate = "0";

                    }

                    int payidate = My.DateConvertToIdate(pay_date);
                    string fineType = dt.Rows[0]["Fine_type"].ToString();

                    if (fineType == "MonthWise") //===== MonthWise
                    {
                        #region MonthWise 
                        string cunrt_session = session;
                        string session_frst_year = cunrt_session.Substring(0, 4);
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);

                        string selectmonth = "";
                        string select_monthid = "0";
                        int mnth_idss = 0;
                        if (Is_next_month_fine_calculate == "1")
                        {
                            selectmonth = Month_name;
                            select_monthid = My.getMonthS_twoDigit(My.tomonth_number(selectmonth).ToString());
                            DateTime dtm = DateTime.ParseExact(Month_name, "MMMM", CultureInfo.InvariantCulture);
                            DateTime nextMonth = dtm.AddMonths(1);
                            mnth_idss = My.tomonth_number(nextMonth.ToString("MMMM"));
                        }
                        else
                        {
                            mnth_idss = My.tomonth_number(Month_name);
                            selectmonth = Month_name;
                            select_monthid = My.getMonthS_twoDigit(My.tomonth_number(selectmonth).ToString());
                        }

                        string pay_month_two_digit = My.getMonthS_twoDigit(mnth_idss.ToString());


                        int pay_month = My.toint(pay_month_two_digit);
                        s_year = check_start_months(pay_month, s_year, con);
                        string FineType = "MonthWise";
                        string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                        string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");

                        string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + pay_month_two_digit + "/" + s_year;
                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);
                        int totalmonths = 0;


                        int pay_month_with_year = My.toint(s_year + pay_month_two_digit);
                        int fine_aplicable_year = check_start_months(My.toInt(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString()), s_year, con);
                        string fine_applicable_month = fine_aplicable_year + applicable_month;
                        if (My.toint(fine_applicable_month) <= pay_month_with_year)
                        {
                            if (isFineRepeate.ToUpper() == "YES")
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

                                string late_fine_between_day = startdate1 + " to " + enddate1;
                                string late_fine_no_of_day_month = totalmonths.ToString();
                                string fine_date_From = last_day_of_payments;
                                string fine_date_To = pay_date;
                                if (My.toDouble(totalmonths) > 0)
                                {
                                    string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                    double ttl_fine_amt = My.toDouble(fine_amt) * totalmonths;
                                    save_fine_amount(session_id, admission_no, select_monthid, ttl_fine_amt, Branch_id, con);
                                }
                                else
                                {
                                    save_fine_amount(session_id, admission_no, select_monthid, 0, Branch_id, con);
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
                                string late_fine_between_day = startdate1 + " to " + enddate1;
                                string late_fine_no_of_day_month = totalmonths.ToString();
                                string fine_date_From = last_day_of_payment;
                                string fine_date_To = pay_date;
                                if (My.toDouble(totalmonths) > 0)
                                {
                                    string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                    double ttl_fine_amt = My.toDouble(fine_amt) * 1;
                                    save_fine_amount(session_id, admission_no, select_monthid, ttl_fine_amt, Branch_id, con);
                                }
                                else
                                {
                                    save_fine_amount(session_id, admission_no, select_monthid, 0, Branch_id, con);
                                }
                            }
                        }
                        #endregion
                    }
                    if (fineType == "DayWise")
                    {
                        #region DayWise 
                        string isCalculated = "0";
                        if (isCalculated == "0")
                        {
                            string cunrt_session = session;
                            string session_frst_year = cunrt_session.Substring(0, 4);
                            int session_s_year = My.toint(session_frst_year);
                            int s_year = My.toint(session_frst_year);

                            int mnth_idss = My.tomonth_number(Month_name);
                            string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());

                            int pay_month = My.toint(month_id_in_two_dgts);
                            s_year = check_start_months(pay_month, s_year, con);

                            DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string paymentMonthno = enddate1q.ToString("MM");

                            int pay_month_with_year = My.toint(s_year + month_id_in_two_dgts);
                            int crnt_month_with_year = My.toint(mycode.year() + paymentMonthno);
                            //Advance Payment Check 
                            int fine_aplicable_year = check_start_months(My.toInt(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString()), s_year, con);

                            if (crnt_month_with_year >= pay_month_with_year)
                            {
                                string FineType = "DayWise";
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

                                    string late_fine_between_day = startdate1 + " to " + enddate1;
                                    string late_fine_no_of_day_month = totaldays.ToString();
                                    string fine_date_From = last_day_of_payments;
                                    string fine_date_To = pay_date;
                                    if (My.toDouble(totaldays) > 0)
                                    {
                                        isCalculated = "1";
                                        string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                        double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                        save_fine_amount(session_id, admission_no, month_id_in_two_dgts, ttl_fine_amt, Branch_id, con);
                                    }
                                    else
                                    {
                                        isCalculated = "1";
                                        save_fine_amount(session_id, admission_no, month_id_in_two_dgts, 0, Branch_id, con);
                                    }
                                }
                            }
                        }


                        #endregion
                    }
                    if (fineType == "QuarterWise")
                    {
                        #region QuarterWise
                        string uncheckd = "1";

                        uncheckd = "0";
                        string cunrt_session = session;
                        string session_frst_year = cunrt_session.Substring(0, 4);
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);

                        int mnth_idss = My.tomonth_number(Month_name);
                        string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());

                        int pay_month = My.toint(month_id_in_two_dgts);
                        s_year = check_start_months(pay_month, s_year, con);

                        #region QuarterWise
                        string FineType = "QuarterWise";
                        double fnl_fine_amt = 0;

                        DataTable dtm = payments.dataTable("select * from Fine_master where Status='1' and Session_id='" + session_id + "' and Q_start_month='" + month_id_in_two_dgts + "' and Q_start_year='" + s_year + "'  order by Q_start_month asc", con);
                        if (dtm.Rows.Count > 0)
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
                                    string late_fine_no_of_day_month = totaldays.ToString();

                                    string fine_date_From = last_day_of_payment_q;
                                    flags1 = "1";
                                    string fine_date_To = pay_date;

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
                                    string late_fine_no_of_day_month = dtm.Rows.Count.ToString();

                                    if (flags1 == "0")
                                    {
                                        string fine_date_From = last_day_of_payment_q;
                                        flags1 = "1";
                                    }

                                    string fine_date_To = pay_date;
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

                            save_fine_amount(session_id, admission_no, month_id_in_two_dgts, fnl_fine_amt, Branch_id, con);
                        }
                        #endregion 
                        #endregion
                    }
                }
                #endregion
            }
        }



        private static void save_fine_amount(string session_id, string admission_no, string month_id_in_two_dgts, double ttl_fine_amt, string Branch_id, SqlConnection con)
        {
            if (payments.IsUserExistS("select Id from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + Branch_id + "'", con))
            {
                if (My.toDouble(ttl_fine_amt) > 0)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Temp_fine_monthwise (Session_id,Admission_no,Month_id,Fine_amount,Branch_id) values (@Session_id,@Admission_no,@Month_id,@Fine_amount,@Branch_id);";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", session_id);
                    cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                    cmd.Parameters.AddWithValue("@Month_id", month_id_in_two_dgts);
                    cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                    cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                    if (payments.InsertUpdateData(cmd, con))
                    {
                    }
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Temp_fine_monthwise set Fine_amount=@Fine_amount  where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + Branch_id + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
        }
    }
}