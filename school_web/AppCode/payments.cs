using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace school_web.AppCode
{
    public class payments
    {
        internal DataTable table(string query, SqlConnection con)
        {
            var ds = dataSet(query, con);
            return ds.Tables[0];
        }
        public void insert_data_logfile(string userid, string firm, string msg, string Branchid, SqlConnection con)
        {
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string strQuery = " INSERT INTO User_Log_History (Date,Description,user_id,firm,idate,Branch_id) values (@Date,@Description,@user_id,@firm,@idate,@Branch_id)";
            string message = msg;
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Date", My.getdate1());
            cmd.Parameters.AddWithValue("@Description", message);
            cmd.Parameters.AddWithValue("@user_id", userid);
            cmd.Parameters.AddWithValue("@firm", firm);
            cmd.Parameters.AddWithValue("@idate", idate);
            cmd.Parameters.AddWithValue("@Branch_id", Branchid);
            if (payments.InsertUpdateData(cmd, con))
            {
            }
        }
        public static void Insert(string tableName, object jsonData, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var js = new JavaScriptSerializer();
            var json = js.Serialize(jsonData);
            var table = js.Deserialize<Dictionary<string, object>>(json);
            Insert(tableName, table, conStr, sqlCon, ignoreColumn);
        }

        public Dictionary<string, object> get_auto_message_template(string messagetype, SqlConnection con)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select *,(select top 1 tny_app_link_android from Update_details) tny_app_link_android,(select top 1 tny_app_link_ios from Update_details) tny_app_link_ios,(select top 1 firm_name from Firm_Details) firm_name from SMS_Template_Setting where Send_From='" + messagetype + "'    ";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd, con);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                dc["SMS_Tempate"] = "";
                dc["VariableName"] = "0";
                dc["SMSType"] = "0";
                dc["Send_From"] = "0";
                dc["Is_Send_SMS"] = "0";
                dc["Is_Send_WhatsApp"] = "0";
                dc["tny_app_link_android"] = "0";
                dc["tny_app_link_ios"] = "0";
                dc["schoolname"] = "0";
            }
            else
            {
                dc["SMS_Tempate"] = dt.Rows[0]["SMS_Tempate"].ToString();
                dc["VariableName"] = dt.Rows[0]["VariableName"].ToString();
                dc["SMSType"] = dt.Rows[0]["SMSType"].ToString();
                dc["Send_From"] = dt.Rows[0]["Send_From"].ToString();
                dc["Is_Send_SMS"] = dt.Rows[0]["Is_Send_SMS"].ToString();
                dc["Is_Send_WhatsApp"] = dt.Rows[0]["Is_Send_WhatsApp"].ToString();
                dc["tny_app_link_android"] = dt.Rows[0]["tny_app_link_android"].ToString();
                dc["tny_app_link_ios"] = dt.Rows[0]["tny_app_link_ios"].ToString();
                dc["schoolname"] = dt.Rows[0]["firm_name"].ToString();
            }
            return dc;
        }

        internal static Dictionary<string, object> get_selected_studentinfo(string admission_no, string sessionid, string branchid, SqlConnection con)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from admission_registor   where admissionserialnumber='" + admission_no + "' and Session_Id=" + sessionid + " and Branch_id='" + branchid + "'";
            DataTable dt = FillDatastatic(query, con);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                dc["admissionserialnumber"] = "0";
                dc["rollnumber"] = "0";
                dc["session"] = "0";
                dc["Name"] = "0";
                dc["Admission_no"] = "0";
                dc["Session"] = "0";
                dc["Class_id"] = "0";
                dc["Session_id"] = "0";
                dc["Total_pay"] = "0";
                dc["status"] = "0";
                dc["Payment_type"] = "0";
                dc["category_id"] = "0";
                dc["sub_category_id"] = "0";
                dc["hosteltaken"] = "0";
                dc["Section"] = "0";
                dc["hosteltaken"] = "No";
                dc["Branch_id"] = "0";
                dc["Transfer_Status"] = "0";
                dc["classname"] = "0";
                dc["Category_id"] = "0";
                dc["SubCategory_id"] = "0";
                dc["session"] = "0";
                dc["Category_id"] = "0";
                dc["SubCategory_id"] = "0";
                dc["hosteltaken"] = "No";
                dc["Branch_id"] = "0";
                dc["Section"] = "0";
                dc["Transfer_Status"] = "0";
                dc["transportationtaken"] = "0";
                dc["is_applied_dayboarding"] = "0";
                dc["day_boarding_with_lunch"] = "0";
            }
            else
            {
                dc["rollnumber"] = dt.Rows[0]["rollnumber"].ToString();
                dc["Section"] = dt.Rows[0]["Section"].ToString();
                dc["Class_id"] = dt.Rows[0]["Class_id"].ToString();
                dc["classname"] = dt.Rows[0]["class"].ToString();
                dc["Name"] = dt.Rows[0]["studentname"].ToString();
                dc["Admission_no"] = dt.Rows[0]["admissionserialnumber"].ToString();
                dc["admissionserialnumber"] = dt.Rows[0]["admissionserialnumber"].ToString();
                dc["Session"] = dt.Rows[0]["session"].ToString();
                dc["session"] = dt.Rows[0]["session"].ToString();
                dc["Session_id"] = dt.Rows[0]["Session_id"].ToString();
                dc["category_id"] = dt.Rows[0]["Category_id"].ToString();
                dc["sub_category_id"] = dt.Rows[0]["SubCategory_id"].ToString();
                dc["Category_id"] = dt.Rows[0]["Category_id"].ToString();
                dc["SubCategory_id"] = dt.Rows[0]["SubCategory_id"].ToString();
                if (dt.Rows[0]["hosteltaken"].ToString().ToUpper() == "YES")
                {
                    dc["hosteltaken"] = "Yes";
                }
                else
                {
                    dc["hosteltaken"] = "No";
                }
                dc["Branch_id"] = dt.Rows[0]["Branch_id"].ToString();
                dc["Section"] = dt.Rows[0]["Section"].ToString();
                if (dt.Rows[0]["Transfer_Status"].ToString().ToUpper() == "TRANSFERRED")
                {
                    dc["Transfer_Status"] = dt.Rows[0]["Transfer_Status_Old"].ToString();
                }
                else
                {
                    dc["Transfer_Status"] = dt.Rows[0]["Transfer_Status"].ToString();
                }

                dc["transportationtaken"] = dt.Rows[0]["transportationtaken"].ToString();
                dc["is_applied_dayboarding"] = dt.Rows[0]["is_applied_dayboarding"].ToString();
                dc["day_boarding_with_lunch"] = dt.Rows[0]["day_boarding_with_lunch"].ToString();
            }
            return dc;
        }

        internal static Dictionary<string, object> Bind_Transport_data_for_assined_student(string Session_id, string Class_id, string Admission_no, SqlConnection con)
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

        internal static Dictionary<string, object> Bind_hostel_data_for_assined_student(string Session_id, string Class_id, string Admission_no, SqlConnection con)
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


        internal static string get_old_bill_no(string slip_no, string Branch_id, SqlConnection con)
        {
            DataTable dt = FillDatastatic("Select  Slip_no from admission_registor_Change_admission_no_history where Slip_no='" + slip_no + "' and Branch_id='" + Branch_id + "'", con);
            if (dt.Rows.Count == 0)
            {
                return " Sorry you entered slip is not exist in deleted bill history";
            }
            else
            {
                DataTable dt1 = FillDatastatic("Select  User_Id from Student_Payment_History where Slip_no='" + slip_no + "' and Branch='" + Branch_id + "'  ", con);
                if (dt1.Rows.Count == 0)
                {
                    return "Yes";
                }
                else
                {
                    return " Sorry you entered slip no. already exists student payment history";
                }
            }
        }

        internal static string check_manual_dup_bill_no(string slip_no, SqlConnection con)
        {
            DataTable dt = FillDatastatic("select Id from Student_Payment_History where Slip_no='" + slip_no + "'", con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }
        internal static string check_manual_dup_bill_no_with_session(string slip_no, string session, SqlConnection con)
        {
            DataTable dt = FillDatastatic("select Id from Student_Payment_History where Slip_no='" + slip_no + "'  and Session='" + session + "'", con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return "1";
            }
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

        //=================================
        internal static string invoice_monthly(string column_name, SqlConnection con)
        {
            string bill = auto_serial1(column_name, con);
            string pre_fix = get_slip_prifix_monthly(con);
            string session = get_session(con);

            if (pre_fix == "")
            {
                return bill;
            }
            else
            {
                string[] stringSeparatorss = new string[] { "-" };
                string[] arr = session.Split(stringSeparatorss, StringSplitOptions.None);
                string s_session = arr[0];
                string e_session = arr[1];

                s_session = s_session.Substring(2, 2);
                e_session = e_session.Substring(2, 2);
                return pre_fix + "/" + s_session + "-" + e_session + "/" + bill;
            }
        }

        internal static string get_single_column_data(string qry, SqlConnection con)
        {
            DataTable dt = FillDatastatic(qry, con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Column_Name"].ToString();
            }
        }

        public static string get_session(SqlConnection con)
        {
            DataTable dt = FillDatastatic("Select Session from session_details where Use_mode='1'", con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Session"].ToString();
            }
        }
        private static string get_slip_prifix_chnageclass(SqlConnection con)
        {
            DataTable dt = FillDatastatic("Select Change_class_Perifix from Firm_Details", con);
            if (dt.Rows.Count == 0)
            {
                return "CC";
            }
            else
            {
                return dt.Rows[0]["Change_class_Perifix"].ToString();
            }

        }
        private static string get_slip_prifix_monthly(SqlConnection con)
        {
            DataTable dt = FillDatastatic("Select Monthly_Slip_Prefix from Firm_Details", con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Monthly_Slip_Prefix"].ToString();
            }
        }

        private static string auto_serial1(string column, SqlConnection con)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data ", con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[column] = "2";
                    result = "1";
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[column].ToString() == "")
                        {

                            result = "1";
                            dr[column] = "2";
                        }
                        else
                        {
                            dr[column] = Convert.ToDouble(dr[column]) + 1;
                            result = dr[column].ToString();
                        }
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception e)
            {
                if (e.Message.EndsWith("does not belong to table Table."))
                {
                    payments.exeSql("Alter table globle_data add " + column + " varchar(50)", con);
                    result = auto_serial1(column, con);
                }
                else
                {
                }
            }
            return result;
        }


        public static DataTable dataTable(string query, SqlConnection con)
        {
            var ds = dataSet(query, con);
            return ds.Tables[0];
        }
        public static DataTable dataTableSP(string sp_name, Dictionary<string, object> param, SqlConnection conn)
        {
            SqlCommand comm = conn.CreateCommand();
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

        public static void exeSql(string query, SqlConnection con)
        {
            SqlDataAdapter ad = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
        }

        public static string auto_serialS(string column, SqlConnection con)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data ", con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[column] = "2";
                    result = "1";
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[column].ToString() == "")
                        {

                            result = "1";
                            dr[column] = "2";
                        }
                        else
                        {

                            dr[column] = Convert.ToDouble(dr[column]) + 1;
                            result = dr[column].ToString();
                        }
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception e)
            {
                if (e.Message.EndsWith("does not belong to table Table."))
                {
                    payments.exeSql("Alter table globle_data add " + column + " varchar(50)", con);
                    result = auto_serialS(column, con);
                }
                else
                {
                }
            }
            return result;
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

        internal static string get_TransportationPath(string TransportationPath_id, SqlConnection con)
        {
            string query = "select Rootname  from dbo.[TransportationPath] where TransportationPath_id='" + TransportationPath_id + "'";
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
        internal static void send_data_to_user_log_history(string description, string userid, SqlConnection con)
        {
            string strQuery = " INSERT INTO User_Log_History (Date,Description,user_id,firm,idate,Branch_id) values (@Date,@Description,@user_id,@firm,@idate,@Branch_id)";
            string message = description;
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Date", My.getdate1());
            cmd.Parameters.AddWithValue("@Description", message);
            cmd.Parameters.AddWithValue("@user_id", userid);
            cmd.Parameters.AddWithValue("@firm", 1);
            cmd.Parameters.AddWithValue("@idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Branch_id", "1");
            if (InsertUpdateData(cmd, con))
            {
            }
        }

        public static Boolean InsertUpdateData(SqlCommand cmd, SqlConnection con)
        {
            bool status = false;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            status = true;
            return status;
        }


        //==============================AdmissioN
        internal static string invoice_readmission(string column_name, SqlConnection con)
        {
            string bill = auto_serial1(column_name, con);
            string pre_fix = get_slip_prifix_readmission(con);
            string session = get_session(con);
            if (pre_fix == "")
            {
                return bill;
            }
            else
            {
                string[] stringSeparatorss = new string[] { "-" };
                string[] arr = session.Split(stringSeparatorss, StringSplitOptions.None);
                string s_session = arr[0];
                string e_session = arr[1];

                s_session = s_session.Substring(2, 2);
                e_session = e_session.Substring(2, 2);


                return pre_fix + "/" + s_session + "-" + e_session + "/" + bill;
            }
        }

        private static string get_slip_prifix_readmission(SqlConnection con)
        {
            DataTable dt = FillDatastatic("Select Readmison_Prefix from Firm_Details", con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Readmison_Prefix"].ToString();
            }
        }

        internal static void send_success_notice_fee(string Student_name, string class_name, string section, string roll_no, string admission_no, string amount, string notice_type, string notice_type_name, string session_id, SqlConnection con)
        {
            DataTable dtTemplate = FillDatastatic("select * from Auto_notice_template where Status='1' and Notice_type='" + notice_type + "'", con);
            if (dtTemplate.Rows.Count > 0)
            {
                string messageTemplate = dtTemplate.Rows[0]["Notice_message"].ToString();
                string cdate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
                string msgs = messageTemplate.Replace("#student_name#", Student_name).Replace("#class_name#", class_name).Replace("#section#", section).Replace("#roll_no#", roll_no).Replace("#amount#", amount).Replace("#admission_no#", admission_no).Replace("#date#", cdate);
                save_auto_send_notice_fee(admission_no, session_id, msgs, notice_type, notice_type_name, cdate, con);
            }
        }
        private static void save_auto_send_notice_fee(string admission_no, string session_id, string msgs, string notice_type_id, string notice_type_name, string cdate, SqlConnection con)
        {
            SqlCommand cmd;
            string query = "INSERT INTO Auto_notice_template_send_details (Notice_type,Notice_type_id,Admission_no,Session_id,Message,Date,idate,Time) values (@Notice_type,@Notice_type_id,@Admission_no,@Session_id,@Message,@Date,@idate,@Time)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Notice_type", notice_type_name);
            cmd.Parameters.AddWithValue("@Notice_type_id", notice_type_id);
            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
            cmd.Parameters.AddWithValue("@Session_id", session_id);
            cmd.Parameters.AddWithValue("@Message", msgs);
            cmd.Parameters.AddWithValue("@Date", cdate);
            cmd.Parameters.AddWithValue("@idate", DateConvertToIdate(cdate));
            cmd.Parameters.AddWithValue("@Time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HH:mm:ss tt"));
            if (payments.InsertUpdateData(cmd, con))
            {
                sendpushNotice(admission_no, session_id, notice_type_id, notice_type_name, msgs, con);
            }
        }

        public static int DateConvertToIdate(string date)
        {
            DateTime d11 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string idate = d11.ToString("yyyyMMdd");
            return Convert.ToInt32(idate);
        }
        private static void sendpushNotice(string admission_no, string session_id, string notice_type_id, string notice_type_name, string msgs, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(" select studentname,admissionserialnumber,gcm_id from admission_registor where admissionserialnumber='" + admission_no + "' and Session_id='" + session_id + "'");
            DataTable dt = GetData(cmd, con);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["gcm_id"].ToString() != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = msgs;
                        ss["title"] = notice_type_name;
                        ss["messagetype"] = notice_type_id;
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = dr["admissionserialnumber"].ToString();
                        SendNotification(dr["gcm_id"].ToString(), ss, con);
                    }
                }
            }
        }


        public static DataTable GetData(SqlCommand cmd, SqlConnection con)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string SendNotification(string deviceId, Dictionary<String, String> data, SqlConnection con)
        {
            Dictionary<string, object> dc1 = get_pushsenderid(con);
            string apikey = (String)dc1["SERVER_API_KEY"];
            string senderid = (String)dc1["SENDER_ID"];
            var ddata = new
            {
                to = deviceId,
                data
            };
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(ddata);
            string SERVER_API_KEY = apikey;//"AAAAb_f_Dbk:APA91bHTYtkKNNEAF49XcqPfxls6nXjV59Bw24AFaxHR6b36qMo60MbxWkRIuv4unhFVU96W6dJsYe_4vu26x4OPr2_lxUU0b1kPh65lXLzeYMDkz4FZ1AZ0mEuEhHjhw29y4PbOK1_e";
            var SENDER_ID = senderid;//"480902057401";
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            tRequest.ContentLength = byteArray.Length;
            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();
            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            System.Net.ServicePointManager.Expect100Continue = false;
            String date = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd-MM-yyyy");
            String Idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            SqlCommand cmd = new SqlCommand(" INSERT INTO PushNotification_Details (notification_id,message,title,messagetype,User_Id,Sender_Id,Idate,Date,Time,ResponseFromServer,Session_id) values (@notification_id,@message,@title,@messagetype,@User_Id,@Sender_Id,@Idate,@Date,@Time,@ResponseFromServer,@Session_id)");
            cmd.Parameters.AddWithValue("@notification_id", data["notification_id"]);
            cmd.Parameters.AddWithValue("@message", data["message"]);
            cmd.Parameters.AddWithValue("@title", data["title"]);
            cmd.Parameters.AddWithValue("@messagetype", data["messagetype"]);
            cmd.Parameters.AddWithValue("@User_Id", data["UserId"]);
            cmd.Parameters.AddWithValue("@Sender_Id", "");
            cmd.Parameters.AddWithValue("@Idate", Idate);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@Time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@ResponseFromServer", sResponseFromServer);
            cmd.Parameters.AddWithValue("@Session_id", payments.get_session_id(con));
            if (payments.InsertUpdateData(cmd, con))
            {
            }
            return sResponseFromServer;
        }
        public static string get_session_id(SqlConnection con)
        {
            DataTable dt = FillDatastatic("Select session_id from session_details where Use_mode='1'", con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["session_id"].ToString();
            }
        }
        public static Dictionary<string, object> get_pushsenderid(SqlConnection con)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from Comapny_Profile  ";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd, con);
            if (dt.Rows.Count == 0)
            {
                dc["SERVER_API_KEY"] = "0";
                dc["SENDER_ID"] = "0";
            }
            else
            {
                dc["SERVER_API_KEY"] = dt.Rows[0]["SERVER_API_KEY"].ToString();
                dc["SENDER_ID"] = dt.Rows[0]["SENDER_ID"].ToString();
            }
            return dc;
        }


        public string auto_serial(string column, SqlConnection con)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data ", con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[column] = "2";
                    result = "1";
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[column].ToString() == "")
                        {
                            result = "1";
                            dr[column] = "2";
                        }
                        else
                        {
                            result = dr[column].ToString();
                            dr[column] = Convert.ToDouble(dr[column]) + 1;
                        }
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception e)
            {
                if (e.Message.EndsWith("does not belong to table Table."))
                {
                    payments.exeSql("Alter table globle_data add " + column + " varchar(50)", con);
                    result = auto_serial(column, con);
                }
                else
                {
                }
            }
            return result;
        }


        internal static string invoice_format_admssion(string column_name, SqlConnection con)
        {
            string bill = payments.auto_serial1(column_name, con);
            string pre_fix = get_slip_prifix_admission(con);
            string session = get_session(con);
            if (pre_fix == "")
            {
                return bill;
            }
            else
            {
                string[] stringSeparatorss = new string[] { "-" };
                string[] arr = session.Split(stringSeparatorss, StringSplitOptions.None);
                string s_session = arr[0];
                string e_session = arr[1];

                s_session = s_session.Substring(2, 2);
                e_session = e_session.Substring(2, 2);

                return pre_fix + "/" + s_session + "-" + e_session + "/" + bill;
            }
        }


        internal static string invoice_no_for_bulk(string column_name, SqlConnection con)
        {
            string bill = payments.auto_serial1(column_name, con);
            return bill;
        }

        private static string get_slip_prifix_admission(SqlConnection con)
        {
            DataTable dt = FillDatastatic("Select Slip_Prefix from Firm_Details  ", con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Slip_Prefix"].ToString();
            }
        }

        public static string get_start_month(SqlConnection con)
        {
            DataTable dt = FillDatastatic("Select Month from Month_Index where Position='1'", con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Month"].ToString();
            }
        }

        internal static void insert_data_split_month(string Month, string fee, string feetype, string slip_no, string Admission_no, string Session_id, SqlConnection con)
        {
            string strQuery = "";
            double fee_amt = My.toDouble(fee);
            string Month_id = payments.get_month_id_from_month_name(Month, con);
            DataTable dt = FillDatastatic("Select * from Split_Month_Fee_Student where Admission_no='" + Admission_no + "' and Session_id='" + Session_id + "' and Month='" + Month + "'", con);
            if (dt.Rows.Count == 0)
            {
                strQuery = " INSERT INTO Split_Month_Fee_Student (Admission_no,Session_id,Month,Month_id,Amount,Date,Idate,slip_no) values (@Admission_no,@Session_id,@Month,@Month_id,@Amount,@Date,@Idate,@slip_no)";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Admission_no", Admission_no);
                cmd.Parameters.AddWithValue("@Session_id", Session_id);
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Month_id", Month_id);
                cmd.Parameters.AddWithValue("@Amount", fee_amt.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("@Idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@slip_no", slip_no);
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
            else
            {
                string Id = dt.Rows[0]["Id"].ToString();
                strQuery = "update Split_Month_Fee_Student set Amount=@Amount,Date=@Date,Idate=@Idate where Id=@Id";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("@Idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@Amount", fee_amt.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Id", Id);
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
        }

        internal static string get_month_id_from_month_name(string Monthname, SqlConnection con)
        {
            string query = "Select top 1 Month_Id from Month_Index where Month='" + Monthname + "'  ";
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

        internal static bool find_fee_taken_date(string table, int pay_idate, string adm_no, string session, SqlConnection con)
        {
            SqlCommand cmd;
            string strQuery = "select * from " + table + " where Admission_no=@Admission_no and session=@session and idate>" + pay_idate + "";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Admission_no", adm_no);
            cmd.Parameters.AddWithValue("@session", session);
            DataTable dt = payments.GetData(cmd, con);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        internal static string invoice_return_sale(string coulm, SqlConnection con)
        {
            string bill = auto_serial1(coulm, con);
            string session = get_session(con);
            return session + "/" + bill;
        }




        internal static bool IsUserExistS(string query, SqlConnection con)
        {
            bool status = false;
            DataTable dtTemp = FillDatastatic(query, con);
            if (dtTemp.Rows.Count == 0)
            {
                status = true;
            }
            return status;
        }


        internal static void delete_log(string session_id, string class_id, string admission_no, string type, string desc, string page, string created_by, SqlConnection con)
        {
            My mycode = new My();
            SqlCommand cmd;
            string query = "INSERT INTO Delete_log_history (Session_id,Admission_no,Class_id,Type,Description,Page,Created_by,Created_date,Created_time,Created_idate) values (@Session_id,@Admission_no,@Class_id,@Type,@Description,@Page,@Created_by,@Created_date,@Created_time,@Created_idate)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", session_id);
            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
            cmd.Parameters.AddWithValue("@Class_id", class_id);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Description", desc);
            cmd.Parameters.AddWithValue("@Page", page);
            cmd.Parameters.AddWithValue("@Created_by", created_by);
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Created_time", mycode.time());
            if (payments.InsertUpdateData(cmd, con))
            {
            }
        }

        internal static string view_admission_no_format(string column_name, SqlConnection con)
        {
            string adno = view_auto_serial(column_name, con);
            DateTime dtm = DateTime.Now;
            if (My.adno_formate.Length > 0)
            {
                adno = My.toDouble(adno).ToString(My.adno_formate);
            }
            string sm_sess = payments.get_session(con).Substring(2, 2) + '-' + payments.get_session(con).Substring(7, 2);
            string m_sess = payments.get_session(con).Substring(0, 4) + '-' + payments.get_session(con).Substring(7, 2);

            string pre_fix = My.adno_prefix.Replace("{dd}", dtm.ToString("dd")).Replace("{mm}", dtm.ToString("MM")).Replace("{ss}", sm_sess).Replace("{sss}", m_sess).Replace("{ssss}", payments.get_session(con));
            string post_fix = My.adno_postfix.Replace("{dd}", dtm.ToString("dd")).Replace("{mm}", dtm.ToString("MM")).Replace("{ss}", sm_sess).Replace("{sss}", m_sess).Replace("{ssss}", payments.get_session(con));
            return pre_fix + adno + post_fix;
        }



        public static string view_auto_serial(string column, SqlConnection con)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data ", con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[column] = "1";
                    result = "1";
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[column].ToString() == "")
                        {
                            result = "1";
                            dr[column] = "1";
                        }
                        else
                        {
                            result = dr[column].ToString();
                        }
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception e)
            {
                if (e.Message.EndsWith("does not belong to table Table."))
                {
                    My.exeSql("Alter table globle_data add " + column + " varchar(50)");
                    result = view_auto_serial(column, con);
                }
                else
                {
                }
            }
            return result;
        }

        public static void student_subject_mapping(string sessionid, string session, string class_id, string admission_no, string section, string Branch_id, SqlConnection con)
        {
            string qury = "select * from dbo.[Subject_Master] where course_id='" + class_id + "' and Is_mandatory=1 and Branch_id='" + Branch_id + "'";
            DataTable dt = FillDatastatic(qury, con);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string Subject_id = dt.Rows[i]["Subject_id"].ToString();
                        string query = "Select * from Subject_Mapping_New where Admission_no='" + admission_no + "' and Class_id='" + class_id + "' and Section='" + section + "' and Session_id='" + sessionid + "' and Branch_id='" + Branch_id + "' and Sub_id='" + Subject_id + "'";
                        DataTable dt1 = FillDatastatic(query, con);
                        if (dt1.Rows.Count == 0)
                        {
                            string strQuery = " INSERT INTO Subject_Mapping_New (Class_id,Section,Admission_no,Sub_id,Session,date,idate,type,delete_off,Type_id,Session_id,Branch_id,Send_Status) values (@Class_id,@Section,@Admission_no,@Sub_id,@Session,@date,@idate,@type,@delete_off,@Type_id,@Session_id,@Branch_id,@Send_Status)";
                            SqlCommand cmd = new SqlCommand(strQuery);
                            cmd.Parameters.AddWithValue("@Class_id", class_id);
                            cmd.Parameters.AddWithValue("@Section", section);
                            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                            cmd.Parameters.AddWithValue("@Sub_id", Subject_id);
                            cmd.Parameters.AddWithValue("@Session", session);
                            cmd.Parameters.AddWithValue("@date", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"));
                            cmd.Parameters.AddWithValue("@idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
                            cmd.Parameters.AddWithValue("@type", 0);
                            cmd.Parameters.AddWithValue("@delete_off", 0);
                            cmd.Parameters.AddWithValue("@Type_id", 0);
                            cmd.Parameters.AddWithValue("@Session_id", sessionid);
                            cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                            cmd.Parameters.AddWithValue("@Send_Status", "Send");
                            if (payments.InsertUpdateData(cmd, con))
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        public static void student_subject_mapping_no_transaction(string sessionid, string session, string class_id, string admission_no, string section, string Branch_id)
        {
            string qury = "select * from dbo.[Subject_Master] where course_id='" + class_id + "' and Is_mandatory=1 and Branch_id='" + Branch_id + "'";
            DataTable dt = My.dataTable(qury);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string Subject_id = dt.Rows[i]["Subject_id"].ToString();
                        string query = "Select * from Subject_Mapping_New where Admission_no='" + admission_no + "' and Class_id='" + class_id + "' and Section='" + section + "' and Session_id='" + sessionid + "' and Branch_id='" + Branch_id + "' and Sub_id='" + Subject_id + "'";
                        DataTable dt1 = My.dataTable(query);
                        if (dt1.Rows.Count == 0)
                        {
                            string strQuery = " INSERT INTO Subject_Mapping_New (Class_id,Section,Admission_no,Sub_id,Session,date,idate,type,delete_off,Type_id,Session_id,Branch_id,Send_Status) values (@Class_id,@Section,@Admission_no,@Sub_id,@Session,@date,@idate,@type,@delete_off,@Type_id,@Session_id,@Branch_id,@Send_Status)";
                            SqlCommand cmd = new SqlCommand(strQuery);
                            cmd.Parameters.AddWithValue("@Class_id", class_id);
                            cmd.Parameters.AddWithValue("@Section", section);
                            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                            cmd.Parameters.AddWithValue("@Sub_id", Subject_id);
                            cmd.Parameters.AddWithValue("@Session", session);
                            cmd.Parameters.AddWithValue("@date", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"));
                            cmd.Parameters.AddWithValue("@idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
                            cmd.Parameters.AddWithValue("@type", 0);
                            cmd.Parameters.AddWithValue("@delete_off", 0);
                            cmd.Parameters.AddWithValue("@Type_id", 0);
                            cmd.Parameters.AddWithValue("@Session_id", sessionid);
                            cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                            cmd.Parameters.AddWithValue("@Send_Status", "Send");
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        public Dictionary<string, object> get_start_month_and_month_id(SqlConnection con)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            DataTable dt = FillDatastatic("select * from Month_Index where Position='1'", con);
            if (dt.Rows.Count == 0)
            {
                dc["Month"] = "0";
                dc["Month_Id"] = "0";
            }
            else
            {
                dc["Month"] = dt.Rows[0]["Month"].ToString();
                dc["Month_Id"] = dt.Rows[0]["Month_Id"].ToString();
            }
            return dc;
        }

        internal static string get_transport_assigned_id(SqlConnection con)
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random(DateTime.Now.Millisecond);
            int tempo = random.Next(1, 999);
            string session = get_session(con);
            string bill = tempo.ToString() + date + session;
            return bill;
        }

        public static string get_session_by_id(string sessionid, SqlConnection con)
        {
            DataTable dt = FillDatastatic("Select Session from session_details where session_id='" + sessionid + "'", con);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal static void save_hostel_aggign_remove_history(string session_id, string class_id, string admission_no, string type, string month, string remark, string page, string user_id, SqlConnection con)
        {
            string strQuery = " INSERT INTO Hostel_assign_remove_history (Session_id,Admission_no,Class_id,Month,Type,Remark,Created_by,Created_date,Created_idate,Created_time) values (@Session_id,@Admission_no,@Class_id,@Month,@Type,@Remark,@Created_by,@Created_date,@Created_idate,@Created_time)";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Session_id", session_id);
            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
            cmd.Parameters.AddWithValue("@Class_id", class_id);
            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Remark", remark);
            cmd.Parameters.AddWithValue("@Created_by", user_id);
            cmd.Parameters.AddWithValue("@Created_date", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@Created_idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Created_time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HH:mm:ss tt"));
            if (payments.InsertUpdateData(cmd, con))
            {
            }
        }

        public static void studentsubject_mapping(string sessionid, string session, string class_id, string admission_no, string section, string Branch_id, SqlConnection con)
        {
            string qury = "select * from dbo.[Subject_Master] where course_id='" + class_id + "' and Is_mandatory=1 and Branch_id='" + Branch_id + "'";
            DataTable dt = FillDatastatic(qury, con);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string Subject_id = dt.Rows[i]["Subject_id"].ToString();
                        string query = "Select * from Subject_Mapping_New where Admission_no='" + admission_no + "' and Class_id='" + class_id + "' and Section='" + section + "' and Session_id='" + sessionid + "' and Branch_id='" + Branch_id + "' and Sub_id='" + Subject_id + "'";
                        DataTable dt1 = FillDatastatic(query, con);
                        if (dt1.Rows.Count == 0)
                        {
                            string strQuery = " INSERT INTO Subject_Mapping_New (Class_id,Section,Admission_no,Sub_id,Session,date,idate,type,delete_off,Type_id,Session_id,Branch_id,Send_Status) values (@Class_id,@Section,@Admission_no,@Sub_id,@Session,@date,@idate,@type,@delete_off,@Type_id,@Session_id,@Branch_id,@Send_Status)";
                            SqlCommand cmd = new SqlCommand(strQuery);
                            cmd.Parameters.AddWithValue("@Class_id", class_id);
                            cmd.Parameters.AddWithValue("@Section", section);
                            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                            cmd.Parameters.AddWithValue("@Sub_id", Subject_id);
                            cmd.Parameters.AddWithValue("@Session", session);
                            cmd.Parameters.AddWithValue("@date", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"));
                            cmd.Parameters.AddWithValue("@idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
                            cmd.Parameters.AddWithValue("@type", 0);
                            cmd.Parameters.AddWithValue("@delete_off", 0);
                            cmd.Parameters.AddWithValue("@Type_id", 0);
                            cmd.Parameters.AddWithValue("@Session_id", sessionid);
                            cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                            cmd.Parameters.AddWithValue("@Send_Status", "Send");
                            if (InsertUpdateData(cmd, con))
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
    }
}