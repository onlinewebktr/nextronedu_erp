using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Web.Script.Serialization; 
using System.Net.Mime;
using school_web.AppCode;

namespace school_web.MyCode
{
    public class Class1
    {
    }

    public class HMS
    {
        public static string backup_dir = "";
        public static string sms_format = "";
        public static string sms_api, sender_id, sms_route, full_url, balanceapi;
        public static string Hopital_name = "Purnank Hospital Management System";
        public static string Hopital_email = "deewakar@purnanksoftware.com";
        public static string Hopital_mobile = "8877804016";
        public static string Hopital_address1 = "Bhagalpur,S.M College Road, Bihar(812001)";
        public static string Hopital_address2 = "";
        public static string Hospital_logo = "";
        public static string Hospital_GST_no = "";
        public static string company_name = "";
        public static string company_prefix = "";
        public static string Patient_Reg_no_length = "";
        public static string company_postfix = "";

        public static string Primary_Country = "";
        public static string Primary_Culture_info = "";
        public static string Primary_Rate = "";
        public static string Primary_Country_id = "";
        public static string Whatsapp_api_url = "";
        public static string whatsapp_mobile_no = ConfigurationManager.AppSettings["whatsapp_mobile_no"];
        public static string Is_registration_multiple = ConfigurationManager.AppSettings["Is_registration_multiple"];
        public static string Is_show_print_datetime = ConfigurationManager.AppSettings["Is_show_print_datetime"];
        public static string Is_regdate_billdate_same = ConfigurationManager.AppSettings["Is_regdate_billdate_same"];
        public static string sms_mobile_no = ConfigurationManager.AppSettings["sms_mobile_no"];
        public static string IS_emergencybed_charges_renewal = "No";
        public static string emergency_extra_bed_charges = "0";
        public static string OPD_Prefix = "";
        public static string OPD_postfix = "";

        public static string LAB_Prefix = "";
        public static string Is_send_text_message = "True";
        public static string Is_send_whatsapp_message = "False";

        internal bool Check_opt_require(string v, string userid, string name)
        {
            string qry = " select top 1 * from dbo.[HMS_OTP_Require] where Page_name='" + v + "'  ";
            DataTable dt = HMS.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                Send_OTP(userid, name);

                return true;
            }
            else
            {
                return false;
            }
        }

        private void Send_OTP(string userid, string name)
        {
            string otp = GetOTP(userid);
            fetch_sms_format("OTP REQUIRED FOR REPORT", "Save", userid, name, otp);
        }
        public string GetOTP(string userid)
        {
            string otp = "";
            bool duplicateid = false;
            Random rn = new Random();
            int i = 10000;
            int j = 99999;
            do
            {
                int k = rn.Next(i, j);
                otp = k.ToString();
                duplicateid = check_dauplicate_id(otp, userid);

                if (duplicateid == true)
                {

                }
            } while (duplicateid == false);

            return otp;

        }
        private bool check_dauplicate_id(string otp, string userid)
        {
            DateTime Valid_upto = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            Valid_upto = Valid_upto.AddMinutes(10);
            SqlDataAdapter ad_reg = new SqlDataAdapter("select * from HMS_OPT where OTP =" + otp + "", HMS.conn);
            DataSet ds = new DataSet();
            ad_reg.Fill(ds, "HMS_OPT");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                DataRow dr = dt.NewRow();
                dr["Userid"] = userid;
                dr["Valid_upto"] = Valid_upto;
                dr["Status"] = "Pending";
                dr["OTP"] = otp;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad_reg);
                ad_reg.Update(dt);
                return true;
            }
            else
            {
                return false;

            }
        }
        private static void fetch_sms_format(string sms_format_name, string button_name, string userid, string name, string otp)
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_SMS_format_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch");
            cmd.Parameters.AddWithValue("@sms_format_name", sms_format_name);
            cmd.Parameters.AddWithValue("@button_name", button_name);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {


                    HMS.sms_format = dr["sms_formate"].ToString();
                    string messageid = dr["message_id"].ToString();
                    string message = "";
                    string Variables = "";
                    message = Replace_string_containt.Inject_data(HMS.sms_format, new HMS_SMS
                    {
                        username = name,
                        OTP = otp,

                    });
                    if (HMS.full_url.Contains("fast2sms.com"))
                    {
                        Variables = name + "|" + otp;
                        Message_sending.send(message, messageid, HMS.sms_mobile_no, Variables, "English", "ADMIN");
                    }
                    else
                    {
                        Message_sending.send(message, HMS.sms_mobile_no, "Unicode", "ADMIN");
                    }


                }
            }
        }

        public static string LAB_postfix = "";

        public static string RADIO_Prefix = "";
        public static string RADIO_postfix = "";

        public static string PROC_Prefix = "";
        public static string PROC_postfix = "";

        public static string EMR_Prefix = "";
        public static string EMR_postfix = "";

        public static string IPD_Prefix = "";
        public static string IPD_postfix = "";

        public static string OT_Prefix = "";
        public static string OT_postfix = "";

        public static string MRD_No_postfix = "";
        public static string MRD_No_Prefix = "";

        public static string QUICK_Prefix = "";
        public static string QUICK_postfix = "";

        public static string OPD_No = "";
        public static string LAB_No = "";
        public static string PROC_No = "";
        public static string IPD_No = "";
        public static string EMR_No = "";
        public static string OT_No = "";
        public static string GEN_No = "";
        public static string Radio_BILL_No = "";
        public static string MRD_No = "";

        public static string OPD_No_length = "0";
        public static string LAB_No_length = "0";
        public static string PROC_No_length = "0";
        public static string EMR_No_length = "0";
        public static string IPD_No_length = "0";
        public static string MRD_No_length = "0";
        public static string OT_No_length = "0";
        public static string GEN_No_length = "0";
        public static string Radio_length = "0";
        public static string Daycare_No_length = "0";


        public static string Daycare_Prefix = "";
        public static string Daycare_postfix = "";

        public static string Healthcard_postfix = "";
        public static string Healthcard_Prefix = "";
        public static string Healthcard_No = "";
        public static string Healthcard_length = "0";

        public static string company_address1 = "";
        public static string company_address2 = "";
        public static string company_Provisional_Reg_No = "";
        public static string company_License_No = "";
        public static string is_registration_fee = "";
        public static string is_pathology_merge_with_hospital = "";
        public static string OTbill_is_individual_or_murge_with_ipd = "";
        public static string Prescription_format = "";
        public static string Prescription_page = "";
        public static string firm_id = "1";
        public static string Financial_Year = "";
        public static int Followup_days = 15;
        public static bool is_doctor_wise_serial_no = true;

        public static string state;
        public static string state_code;
        public static string Money_receipt_format;
        public static string ipd_bill_renewal_type = "Hourly";
        public static string ipd_bill_entry_type = "";
        public static string is_daily_opening_balance_zero = "";
        public static string Is_discharge_before_discharge_summary_prepration = "1";
        public static string Is_notdischarge_on_medicaldues = "False";
        public static string is_discharge_on_pathodues = "False";
        public static string is_patho_radio_bill_seperate_ = "True";
        public static string IS_pharmacy_bill_at_reception = "False";
        public static string is_usemarkup = "False";
        public bool IsUserExist(string query)
        {
            bool status = false;
            DataTable dtTemp = FillData(query);
            if (dtTemp.Rows.Count == 0)
            {
                status = true;
            }
            return status;
        }

        public static string renewal_hour = "24";
        public static string IPD_Patient_sticker = "";
        public static string OPD_Patient_sticker = "";
        public static DateTime start_date;
        public static DateTime end_date;

        public static string document_file_path = @"D:\important_docx\";
        public static Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();

        public static double Drpercentage = 0.8;
        public static Dictionary<string, object> data = new Dictionary<string, object>();

        public static String base_url = "http://crm.purnanksoftware.com/";
        public static String url = HMS.base_url + "/hospital_reg.aspx?";
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        internal static String generate_serial_key()
        {
            string adr = GetMACAddress2();
            string s = "";
            int sum = 0, index = 1;
            foreach (char c in adr)
            {
                if (char.IsDigit(c))
                {
                    sum += sum + (int)c * (index * 2);
                }
                else
                {
                    switch (c.ToString().ToUpper())
                    {
                        case "A":
                            sum += sum + 10 * (index * 2);
                            break;
                        case "B":
                            sum += sum + 11 * (index * 2);
                            break;
                        case "C":
                            sum += sum + 12 * (index * 2);
                            break;
                        case "D":
                            sum += sum + 13 * (index * 2);
                            break;
                        case "E":
                            sum += sum + 14 * (index * 2);
                            break;
                        case "F":
                            sum += sum + 15 * (index * 2);
                            break;

                    }
                }
                index++;
            }
            long x = sum + sum_of(HMS.assembly.FullName);

            String final = (x * x + 53 / x + 113 * (x / 4)).ToString();

            return generate(final);

        }
        private static string generate(string final)
        {
            string result = "";
            int count = 0;
            foreach (char c in final)
            {

                result += getchar(c);
                count++;
                if (count == 5)
                {
                    result += "-";
                    count = 0;
                }
            }
            return result + "XZ";
        }
        public static string getchar(char c)
        {
            switch (c)
            {
                case '0':
                    return "U";
                case '1':
                    return "W";
                case '2':
                    return "X";
                case '3':
                    return "F";
                case '4':
                    return "P";
                case '5':
                    return "Q";
                case '6':
                    return "M";
                case '7':
                    return "B";
                case '8':
                    return "Y";
                case '9':
                    return "E";
            }
            return "-";
        }



        private static int sum_of(string names)
        {
            string sname;
            if (names.Length < 5)
            {
                sname = names;
            }
            else
            {
                sname = names.Substring(0, 4);
            }
            int count = 0;
            foreach (char c in sname)
            {
                count += (int)c;
            }
            return count;
        }

        internal static string GetMACAddress2()
        {
            try
            {
                //ManagementClass managClass = new ManagementClass("win32_processor");
                //ManagementObjectCollection managCollec = managClass.GetInstances();

                //foreach (ManagementObject managObj in managCollec)
                //{
                //    return managObj.Properties["processorID"].Value.ToString() + System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                //}
                return "";
            }
            catch
            {
                return "";
            }
        }


        #region payroll_veriable

        internal static void init_payroll()
        {
            DataTable dt = HMS.dataTable("select * from Payroll_Setting");
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HMS.emp_code_prefix = dr["Emp_Code_Prefix"].ToString();
                    HMS.emp_code = dr["Emp_Code"].ToString();
                    HMS.emp_code_postfix = dr["Emp_Code_Postfix"].ToString();
                }
            }
        }



        public static string emp_code = "";
        public static string emp_code_postfix = "";
        public static string emp_code_prefix = "";

        public static string FormSale_allow = "";
        public static string start_month = "";
        public static string adno_prefix = "";
        public static string adno_postfix = "";
        public static string adno_formate = "";
        public static string Admission_no_auto = "";
        public static string mobile_mandatory = "";
        public static string email_mandatory = "";
        public static string section_auto = "";
        public static bool is_trans_services;
        public static string auto_formno = "";
        public static int paging = 500;
        public static string staff_con = "";

        public static string client_id = "";
        public static string user = "";
        public static string er = "false";
        public static string user_status = "";
        public static string user_name = "";
        //public static string firm = "";
        public static string ddd;
        public static bool negativesale = false;
        public static string address2;
        public static bool is_inclusive = true;
        public static double page_width = 8.5;
        public static double page_height = 11.7;
        public static string table;
        public static string sts;
        public static string printer_name;
        public static string current_session = "2021-2022";
        public static string address1 = "D S College Road Katihar-854105";

        public static string bill_type;
        public static bool show_discount;
        public static bool diff_barcode;
        public static string terms_and_condition;
        public static bool show_return_item_on_bill;
        public static bool print_header;
        public static string firm_name;
        public static string gstin;
        public static string opening_paid_amount = "";

        #endregion payroll_veriable

        public static string FinancialYear()
        {
            DateTime dateTime = DateTime.Now;
            return (dateTime.Month >= 4 ? dateTime.ToString("yyyy") + "-" + dateTime.AddYears(1).ToString("yyyy") : dateTime.AddYears(-1).ToString("yyyy") + "-" + dateTime.ToString("yyyy")); ;
        }

        internal static void exeSql(string qry, string source_con)
        {
            SqlConnection conn = new SqlConnection(source_con);
            SqlDataAdapter ad = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
        }

        public static string FinancialYearShort()
        {
            DateTime dateTime = DateTime.Now;
            return (dateTime.Month >= 4 ? dateTime.ToString("yy") + "-" + dateTime.AddYears(1).ToString("yy") : dateTime.AddYears(-1).ToString("yy") + "-" + dateTime.ToString("yy"));
        }
        public static string FinancialYearSemiShort()
        {
            DateTime dateTime = DateTime.Now;
            return (dateTime.Month >= 4 ? dateTime.ToString("yyyy") + "-" + dateTime.AddYears(1).ToString("yy") : dateTime.AddYears(-1).ToString("yyyy") + "-" + dateTime.ToString("yy"));
        }
        internal static string unique_id()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            string a = r.Next(2478, 9758) +
              DateTime.Now.Year.ToString("0000") +
              DateTime.Now.Day.ToString("00") +
              DateTime.Now.Month.ToString("00") +
              DateTime.Now.Hour.ToString("00") +
              DateTime.Now.Minute.ToString("00") +
              DateTime.Now.Second.ToString("00") +
              DateTime.Now.Millisecond.ToString("00") + r.Next(788, 999);
            return a;
        }
        public static string session_wise_view_auto_serial(string column, string Financial_Year, string firm)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data_session_wise where   session='" + Financial_Year + "' and firm='" + firm + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["session"] = Financial_Year;
                    dr["firm"] = firm;
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

                    HMS.exeSql("Alter table globle_data_session_wise add " + column + " varchar(50)");
                    result = session_wise_view_auto_serial(column, Financial_Year, firm);
                }
                else
                {

                }
            }
            return result;
        }
        public static ArrayList featch_year(DateTime today)
        {
            CultureInfo culutreInfo = System.Threading.Thread.CurrentThread.CurrentCulture.Clone() as CultureInfo;
            culutreInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = culutreInfo;

            ArrayList ar = new ArrayList();
            string preyear = "01/01/2019";
            DateTime date = Convert.ToDateTime(preyear);
            int totyear = today.Year - date.Year;

            if (date > today.AddYears(-totyear)) totyear--;
            totyear = totyear + 1;

            ar.Add(preyear.Substring(6, 4));

            for (int i = 1; i < totyear; i++)
            {
                string year = date.AddYears(i).ToString();
                ar.Add(year.Substring(6, 4));
            }
            return ar;
        }
        public static string session_wise_auto_serial(string column, string Financial_Year, string firm)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data_session_wise where   session='" + Financial_Year + "' and firm='" + firm + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["session"] = Financial_Year;
                    dr["firm"] = firm;
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
                    HMS.exeSql("Alter table globle_data_session_wise add " + column + " varchar(50)");
                    result = session_wise_auto_serial(column, Financial_Year, firm);
                }
                else
                {

                }
            }
            return result;
        }
        public static void exeSql(string query)
        {

            SqlConnection conn = new SqlConnection(HMS.conn);
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);

        }
        public static double toDouble(object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return 0;
            }

        }
        public static string toAmount(object obj)
        {
            try
            {
                return Convert.ToDouble(obj).ToString("0.00");
            }
            catch
            {
                return "0.00";
            }

        }



        internal static double Round(double value)
        {
            return Round(value, 2);
        }
        internal static double Round(double value, int f_point)
        {
            return Math.Round(value, f_point, MidpointRounding.AwayFromZero);
        }

        public static string conn = PayrollMy.con;
        //public static string conn2 = HMS.DecodeFrom64( ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        public static DataTable GetDatatable(SqlCommand cmd)
        {

            DataTable dt = new DataTable();
            String strConnString = HMS.conn;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }

            }
            catch (Exception ex)
            {
                HMS.submitexception(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dt;
        }

        internal static string get_patient_id_by_barcode(string barcode)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select Patient_id from dbo.[Hms_Patient_and_barcode_history] where barcode='" + barcode + "'", HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Hms_Patient_and_barcode_history");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount > 0)
                return dt.Rows[0]["Patient_id"].ToString();
            else
                return "0";
        }

        public static DataSet GetDataset(SqlCommand cmd)
        {

            DataSet ds = new DataSet();
            String strConnString = HMS.conn;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sda.SelectCommand = cmd;
                sda.Fill(ds);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }

            }
            catch (Exception ex)
            {
                HMS.submitexception(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return ds;
        }

        //----------------------------------------------System Date & Idate

        public static string date()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd") + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MMM") + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");
        }
        public static string display_date(DateTime date)
        {
            try
            {
                return date.ToString("dd") + "/" + date.ToString("MM") + "/" + date.ToString("yyyy");
            }
            catch
            {
                return "";
            }
        }
        public static string display_date(object date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date);
                return dt.ToString("dd") + "/" + dt.ToString("MMM") + "/" + dt.ToString("yyyy");
            }
            catch
            {
                return "";
            }
        }
        public static string valid_up_to(object date, int follow_up_date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date);
                dt = dt.AddDays(follow_up_date);
                return dt.ToString("dd") + "/" + dt.ToString("MMM") + "/" + dt.ToString("yyyy");
            }
            catch
            {
                return "";
            }
        }
        public static string display_date_time(object date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date);
                return dt.ToString("dd") + "/" + dt.ToString("MMM") + "/" + dt.ToString("yyyy hh:mm:ss tt");
            }
            catch
            {
                return "";
            }
        }
        public static string display_date_time1(object date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date);
                return dt.ToString("dd") + "/" + dt.ToString("MM") + "/" + dt.ToString("yyyy hh:mm tt");
            }
            catch
            {
                return "";
            }
        }
        public static string display_date_time2(object date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date);
                return dt.ToString("dd") + "/" + dt.ToString("MMM") + "/" + dt.ToString("yyyy hh:mm tt");
            }
            catch
            {
                return "";
            }
        }
        public static string idate()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
        }

        public static DateTime convert_to_datetime(string date)
        {
            DateTime d11 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return d11;
        }
        public static DateTime convert_to_datetime(string date, string format)
        {
            try
            {

                DateTime d11 = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
                return d11;
            }
            catch (Exception ex)
            {
                HMS.submitexception(ex.ToString() + "==" + date + "==" + format);
                return Convert.ToDateTime(date);
            }
        }

        public static DateTime convert_to_datetime(object date)
        {

            return Convert.ToDateTime(date);

        }
        public static string convertidate(string date)
        {
            try
            {
                DateTime d11 = DateTime.ParseExact(date, "dd/MMM/yyyy", CultureInfo.InvariantCulture);
                string idate = d11.ToString("yyyyMMdd");
                return idate;
            }
            catch (Exception ex)
            {
                HMS.submitexception(ex.ToString() + "==" + date);
                return HMS.idate();
            }

        }



        public static int DateConvertToIdate(string date)
        {
            DateTime d11 = DateTime.ParseExact(date, "dd/MMM/yyyy", CultureInfo.InvariantCulture);
            string idate = d11.ToString("yyyyMMdd");
            return Convert.ToInt32(idate);
        }
        public static string idatemy()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMM");
        }
        public static string iYearMonth()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMM");
        }
        public static string time()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt");
        }
        public static string time1()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm tt");
        }
        public static string itime()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HHmmss");
        }
        public static string datetime()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MM/dd/yyyy hh:mm:ss tt");
        }
        public static string printdatetime()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MMM/yyyy hh:mm:ss tt");
        }
        public static void submitexception(string ex)
        {
            try
            {

                SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_ExceptionDetails", HMS.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "ExceptionDetails");
                DataTable dt = ds.Tables[0];
                int rowcount = dt.Rows.Count;
                DataRow dr = dt.NewRow();
                dr["exception_message"] = ex;
                dr["date"] = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
            catch
            {
            }

        }
        public static void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();
                else if (ctrl is CheckBox)
                    ((CheckBox)ctrl).Checked = false;

                ClearInputs(ctrl.Controls);
            }
        }
        public string UniqueUserID()
        {
            int start = 100000; int end = 999999;  // Upto 6 digit.
            string id = GenerateRandomNumber(start, end);
            string fnl_id = HMS.company_prefix + "--" + id;
            bool ExistStatus = true;
            while (ExistStatus)
            {
                if (IsUserIDExist(fnl_id)) { break; }
                id = GenerateRandomNumber(start, end);
                fnl_id = HMS.company_prefix + "--" + id;
            }
            return fnl_id.ToString();
        }

        public bool IsUserIDExist(string Registration_no)
        {
            bool status = false;
            SqlCommand cmd = new SqlCommand("sp_Patient_registration");
            cmd.Parameters.AddWithValue("@sp_status", "Fetch2");
            cmd.Parameters.AddWithValue("@Patient_Registration_no", Registration_no);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0) { status = true; }
            return status;
        }
        public string GenerateRandomNumber(int start, int end)
        {
            Random random = new Random();
            int temp = random.Next(start, end);
            return temp.ToString();
        }
        public static string daily_hospital_serial_no(string Date, string Patient_id, string Patient_Status, string Billing_for)
        {
            string result = "";
            try
            {
                string qry = "";
                qry = "Select top 1 * from HMS_Datewise_serial_no where Date='" + Date + "' order by id desc";
                SqlDataAdapter ad = new SqlDataAdapter(qry, HMS.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["Date"] = Date;
                    dr["Hospital_Serial_no"] = 1;
                    dr["Patient_id"] = Patient_id;
                    dr["Patient_is_old"] = Patient_Status;
                    dr["Billing_for"] = Billing_for;
                    result = "1";
                    dt.Rows.Add(dr);

                }
                else
                {
                    double Hospital_Serial_no = Convert.ToDouble(dt.Rows[0]["Hospital_Serial_no"]) + 1;
                    result = Hospital_Serial_no.ToString();
                    DataRow dr = dt.NewRow();
                    dr["Date"] = Date;
                    dr["Hospital_Serial_no"] = Hospital_Serial_no;
                    dr["Patient_id"] = Patient_id;
                    dr["Patient_is_old"] = Patient_Status;
                    dr["Billing_for"] = Billing_for;
                    dt.Rows.Add(dr);

                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public static string daily_doctor_serial_no(string Date, string doctor_id, string Patient_id, string Patient_Status, string Billing_for)
        {
            string result = "";
            try
            {
                string qry = "";
                qry = "Select top 1 * from HMS_Datewise_Doctorwise_serial_no where Date='" + Date + "' and Doctor_id='" + doctor_id + "' order by id desc";
                SqlDataAdapter ad = new SqlDataAdapter(qry, HMS.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["Date"] = Date;
                    dr["Doctor_id"] = doctor_id;
                    dr["Doctor_Serial_no"] = 1;
                    dr["Patient_id"] = Patient_id;
                    dr["Patient_is_old"] = Patient_Status;
                    dr["Billing_for"] = Billing_for;
                    result = "1";
                    dt.Rows.Add(dr);

                }
                else
                {
                    double Doctor_Serial_no = Convert.ToDouble(dt.Rows[0]["Doctor_Serial_no"]) + 1;

                    result = Doctor_Serial_no.ToString();

                    DataRow dr = dt.NewRow();
                    dr["Date"] = Date;
                    dr["Doctor_id"] = doctor_id;
                    dr["Doctor_Serial_no"] = Doctor_Serial_no;
                    dr["Patient_id"] = Patient_id;
                    dr["Patient_is_old"] = Patient_Status;
                    dr["Billing_for"] = Billing_for;
                    dt.Rows.Add(dr);

                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public static string RemoveSpecialCharacters(string str)
        {
            //string result = Regex.Replace(str, "[:!@#$%^&*()}{|\":?><\\[\\]\\;'/.,~-]", "");
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static string global_id_generate(string columnname)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_Global_Master", HMS.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[columnname] = 2;
                    result = "1";
                    dt.Rows.Add(dr);

                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[columnname].ToString() == "")
                        {
                            result = "1";
                            dr[columnname] = "2";
                        }
                        else
                        {
                            result = dr[columnname].ToString();
                            dr[columnname] = Convert.ToDouble(dr[columnname]) + 1;
                        }

                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                if (ex.Message.EndsWith("does not belong to table Table."))
                {
                    HMS.exeSql("Alter table HMS_Global_Master add " + columnname + " varchar(500)");
                    result = global_id_generate(columnname);
                }
                else
                {

                }
            }
            return result;
        }

        public static string global_id_creation(string columnname)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_Global_Master", HMS.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[columnname] = 2;
                    result = "00001";
                    dt.Rows.Add(dr);

                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[columnname].ToString() == "")
                        {
                            result = "1";
                            dr[columnname] = "2";
                        }
                        else
                        {
                            result = dr[columnname].ToString();
                            dr[columnname] = Convert.ToDouble(dr[columnname]) + 1;
                        }

                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                if (ex.Message.EndsWith("does not belong to table Table."))
                {
                    HMS.exeSql("Alter table HMS_Global_Master add " + columnname + " varchar(500)");
                    result = global_id_creation(columnname);
                }
                else
                {

                }
            }
            return result;
        }
        internal static string generate_registration_no(string firstname)
        {
            string registration_no = "";
            if (firstname.Length < 4)
                registration_no = firstname.Substring(0, firstname.Length);
            else
                registration_no = firstname.Substring(0, 4);

            return registration_no;
        }
        internal static void send_to_account_ledger(string Main_account_name, string Alternet_Account, string Amount, string VoucherNo, string unique_entry_id, string Description, string Date, string IDate, string cash_payment, string bank_payment, string bank_account, string VoucherType, string firm, string Financial_Year, string userid, string Bill_from)
        {
            if (Convert.ToDouble(Amount) > 0)
            {
                SqlCommand cmd = new SqlCommand("sp_Account_Voucher_Details");
                cmd.Parameters.AddWithValue("@sp_status", "INSERT");
                cmd.Parameters.AddWithValue("@Main_account_name", Main_account_name);
                cmd.Parameters.AddWithValue("@Alternet_Account", Alternet_Account);
                cmd.Parameters.AddWithValue("@Description ", Description);
                cmd.Parameters.AddWithValue("@Amount ", Amount);

                if (cash_payment != "")
                {
                    cmd.Parameters.AddWithValue("@cash_payment ", cash_payment);
                    cmd.Parameters.AddWithValue("@bank_payment ", "0");
                }
                else if (bank_payment != "")
                {
                    cmd.Parameters.AddWithValue("@cash_payment ", "0");
                    cmd.Parameters.AddWithValue("@bank_payment ", bank_payment);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@cash_payment ", "0");
                    cmd.Parameters.AddWithValue("@bank_payment ", "0");
                }

                cmd.Parameters.AddWithValue("@bank_account ", bank_account);

                cmd.Parameters.AddWithValue("@Date ", Date);
                cmd.Parameters.AddWithValue("@IDate ", IDate);
                cmd.Parameters.AddWithValue("@unique_entry_id ", unique_entry_id);
                cmd.Parameters.AddWithValue("@VoucherNo ", VoucherNo);
                cmd.Parameters.AddWithValue("@VoucherType ", VoucherType);
                cmd.Parameters.AddWithValue("@firm ", firm);
                cmd.Parameters.AddWithValue("@Session ", Financial_Year);
                cmd.Parameters.AddWithValue("@Created_by ", userid);
                cmd.Parameters.AddWithValue("@Bill_from ", Bill_from);
                int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                if (rowsAffected > 0)
                {
                }
            }
        }
        internal static void send_to_Single_entry_Account_Voucher(string Main_account_name, string Alternet_Account, string Amount, string VoucherNo, string unique_entry_id, string Description, string Date, string IDate, string VoucherType, string firm, string Financial_Year, string Bill_from, string userid)
        {
            if (Convert.ToDouble(Amount) > 0)
            {
                SqlCommand cmd = new SqlCommand("sp_Account_Voucher_Details");
                cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
                cmd.Parameters.AddWithValue("@Main_account_name", Main_account_name);
                cmd.Parameters.AddWithValue("@Alternet_Account", Alternet_Account);
                cmd.Parameters.AddWithValue("@Description ", Description);
                cmd.Parameters.AddWithValue("@Amount ", Amount);

                cmd.Parameters.AddWithValue("@Date ", Date);
                cmd.Parameters.AddWithValue("@IDate ", IDate);
                cmd.Parameters.AddWithValue("@unique_entry_id ", unique_entry_id);
                cmd.Parameters.AddWithValue("@VoucherNo ", VoucherNo);
                cmd.Parameters.AddWithValue("@VoucherType ", VoucherType);
                cmd.Parameters.AddWithValue("@firm ", firm);
                cmd.Parameters.AddWithValue("@Session ", Financial_Year);
                cmd.Parameters.AddWithValue("@Created_by ", userid);
                cmd.Parameters.AddWithValue("@Bill_from ", Bill_from);
                int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                if (rowsAffected > 0)
                {
                }
            }
        }

        internal static string find_mrd_no(string Patient_id)
        {
            string qry = " select top 1 * from dbo.[HMS_patient_mrd_no_history] where Patient_id='" + Patient_id + "' order by Id desc";
            DataTable dt = HMS.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["MRD_NO"].ToString();
            }
            else
            {
                string MRD_no = HMS.global_id_creation("MRD_NO");
                if (HMS.MRD_No_length.Length > 0)
                {
                    MRD_no = HMS.toDouble(MRD_no).ToString(HMS.MRD_No_length);
                }
                MRD_no = HMS.MRD_No_Prefix + MRD_no + HMS.MRD_No_postfix;
                string insertqry = "insert into HMS_patient_mrd_no_history(Patient_id,MRD_NO) values ('" + Patient_id + "','" + MRD_no + "');";
                HMS.exeSql(insertqry);
                return MRD_no;
            }

        }

        internal static void send_to_Single_Account_Voucher(string Main_account_name, string Alternet_Account, string Amount, string VoucherNo, string unique_entry_id, string Description, string Date, string IDate, string VoucherType, string firm, string Financial_Year, string Bill_from, string userid)
        {
            if (Convert.ToDouble(Amount) > 0)
            {
                SqlCommand cmd = new SqlCommand("sp_Account_Voucher_Details");
                cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
                cmd.Parameters.AddWithValue("@Main_account_name", Main_account_name);
                cmd.Parameters.AddWithValue("@Alternet_Account", Alternet_Account);
                cmd.Parameters.AddWithValue("@Description ", Description);
                cmd.Parameters.AddWithValue("@Amount ", Amount);

                cmd.Parameters.AddWithValue("@Date ", Date);
                cmd.Parameters.AddWithValue("@IDate ", IDate);
                cmd.Parameters.AddWithValue("@unique_entry_id ", unique_entry_id);
                cmd.Parameters.AddWithValue("@VoucherNo ", VoucherNo);
                cmd.Parameters.AddWithValue("@VoucherType ", VoucherType);
                cmd.Parameters.AddWithValue("@firm ", firm);
                cmd.Parameters.AddWithValue("@Session ", Financial_Year);
                cmd.Parameters.AddWithValue("@Created_by ", userid);
                cmd.Parameters.AddWithValue("@Bill_from ", Bill_from);
                int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                if (rowsAffected > 0)
                {
                }
            }
        }

        internal static string encode(string text)
        {
            throw new NotImplementedException();
        }




        #region convert_amount_to_word
        public string AmountInWords(string Num)
        {
            string returnValue;
            //I have created this function for converting amount in indian rupees (INR).
            //You can manipulate as you wish like decimal setting, Doller (any currency) Prefix.


            string strNum;
            string strNumDec;
            string StrWord;
            //strNum = Num.ToString();
            strNum = Num;

            if (strNum.IndexOf(".") + 1 != 0)
            {
                strNumDec = strNum.Substring(strNum.IndexOf(".") + 2 - 1);

                if (strNumDec.Length == 1)
                {
                    strNumDec = strNumDec + "0";
                }
                if (strNumDec.Length > 2)
                {
                    strNumDec = strNumDec.Substring(0, 2);
                }

                strNum = strNum.Substring(0, strNum.IndexOf(".") + 0);
                //StrWord = ((double.Parse(strNum) == 1) ? " Rupee " : " Rupees ") + NumToWord((decimal)(double.Parse(strNum))) + ((double.Parse(strNumDec) > 0) ? (" and Paise" + cWord3((decimal)(double.Parse(strNumDec)))) : "");
                StrWord = NumToWord((decimal)(double.Parse(strNum))) + ((double.Parse(strNum) == 1) ? " Rupee " : " Rupees ") + ((double.Parse(strNumDec) > 0) ? (" and " + cWord3((decimal)(double.Parse(strNumDec))) + " Paise") : "");



            }
            else
            {
                StrWord = NumToWord((decimal)(double.Parse(strNum))) + ((double.Parse(strNum) == 1) ? " Rupee " : " Rupees ");
            }
            returnValue = StrWord + " Only";
            return returnValue;
        }
        static public string NumToWord(decimal Num)
        {
            string returnValue;


            //I divided this function in two part.
            //1. Three or less digit number.
            //2. more than three digit number.
            string strNum;
            string StrWord;
            strNum = Num.ToString();


            if (strNum.Length <= 3)
            {
                StrWord = cWord3((decimal)(double.Parse(strNum)));
            }
            else
            {
                StrWord = cWordG3((decimal)(double.Parse(strNum.Substring(0, strNum.Length - 3)))) + " " + cWord3((decimal)(double.Parse(strNum.Substring(strNum.Length - 2 - 1))));
            }
            returnValue = StrWord;
            return returnValue;
        }
        static public string cWordG3(decimal Num)
        {
            string returnValue;
            //2. more than three digit number.
            string strNum = "";
            string StrWord = "";
            string readNum = "";
            strNum = Num.ToString();
            if (strNum.Length % 2 != 0)
            {
                readNum = System.Convert.ToString(double.Parse(strNum.Substring(0, 1)));
                if (readNum != "0")
                {
                    StrWord = retWord(decimal.Parse(readNum));
                    readNum = System.Convert.ToString(double.Parse("1" + strReplicate("0", strNum.Length - 1) + "000"));
                    StrWord = StrWord + " " + retWord(decimal.Parse(readNum));
                }
                strNum = strNum.Substring(1);
            }
            while (!System.Convert.ToBoolean(strNum.Length == 0))
            {
                readNum = System.Convert.ToString(double.Parse(strNum.Substring(0, 2)));
                if (readNum != "0")
                {
                    StrWord = StrWord + " " + cWord3(decimal.Parse(readNum));
                    readNum = System.Convert.ToString(double.Parse("1" + strReplicate("0", strNum.Length - 2) + "000"));
                    StrWord = StrWord + " " + retWord(decimal.Parse(readNum));
                }
                strNum = strNum.Substring(2);
            }
            returnValue = StrWord;
            return returnValue;
        }
        static public string cWord3(decimal Num)
        {
            string returnValue;
            //1. Three or less digit number.
            string strNum = "";
            string StrWord = "";
            string readNum = "";
            if (Num < 0)
            {
                Num = Num * -1;
            }
            strNum = Num.ToString();


            if (strNum.Length == 3)
            {
                readNum = System.Convert.ToString(double.Parse(strNum.Substring(0, 1)));
                StrWord = retWord(decimal.Parse(readNum)) + " Hundred";
                strNum = strNum.Substring(1, strNum.Length - 1);
            }


            if (strNum.Length <= 2)
            {
                if (double.Parse(strNum) >= 0 && double.Parse(strNum) <= 20)
                {
                    StrWord = StrWord + " " + retWord((decimal)(double.Parse(strNum)));
                }
                else
                {
                    StrWord = StrWord + " " + retWord((decimal)(System.Convert.ToDouble(strNum.Substring(0, 1) + "0"))) + " " + retWord((decimal)(double.Parse(strNum.Substring(1, 1))));
                }
            }


            strNum = Num.ToString();
            returnValue = StrWord;
            return returnValue;
        }
        static public string retWord(decimal Num)
        {
            string returnValue;
            //This two dimensional array store the primary word convertion of number.
            returnValue = "";
            object[,] ArrWordList = new object[,] { { 0, "" }, { 1, "One" }, { 2, "Two" }, { 3, "Three" }, { 4, "Four" }, { 5, "Five" }, { 6, "Six" }, { 7, "Seven" }, { 8, "Eight" }, { 9, "Nine" }, { 10, "Ten" }, { 11, "Eleven" }, { 12, "Twelve" }, { 13, "Thirteen" }, { 14, "Fourteen" }, { 15, "Fifteen" }, { 16, "Sixteen" }, { 17, "Seventeen" }, { 18, "Eighteen" }, { 19, "Nineteen" }, { 20, "Twenty" }, { 30, "Thirty" }, { 40, "Forty" }, { 50, "Fifty" }, { 60, "Sixty" }, { 70, "Seventy" }, { 80, "Eighty" }, { 90, "Ninety" }, { 100, "Hundred" }, { 1000, "Thousand" }, { 100000, "Lakh" }, { 10000000, "Crore" } };


            int i;
            for (i = 0; i <= (ArrWordList.Length - 1); i++)
            {
                if (Num == System.Convert.ToDecimal(ArrWordList[i, 0]))
                {
                    returnValue = (string)(ArrWordList[i, 1]);
                    break;
                }
            }
            return returnValue;
        }
        static public string strReplicate(string str, int intD)
        {
            string returnValue;
            //This fucntion padded "0" after the number to evaluate hundred, thousand and on....
            //using this function you can replicate any Charactor with given string.
            int i;
            returnValue = "";
            for (i = 1; i <= intD; i++)
            {
                returnValue = returnValue + str;
            }
            return returnValue;
        }


        #endregion convert_amount_to_word



        public static string auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data ", conn);
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
                    HMS.exeSql("Alter table globle_data add " + column + " varchar(50)");
                    result = auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }


        public static string get_image_diamension(FileUpload file_thumb)
        {
            System.IO.Stream stream = file_thumb.PostedFile.InputStream;
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

            int height = image.Height;
            int width = image.Width;

            return "Height: " + height + "; Width: " + width;
        }


        internal static void delete_ledger(string Unique_entry_id, string Alternet_Account)
        {
            SqlCommand cmd = new SqlCommand("sp_Account_Voucher_Details");
            cmd.Parameters.AddWithValue("@sp_status ", "DELETE");
            cmd.Parameters.AddWithValue("@unique_entry_id ", Unique_entry_id);
            cmd.Parameters.AddWithValue("@firm ", HMS.firm_id);
            cmd.Parameters.AddWithValue("@Alternet_Account ", Alternet_Account);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
            }
        }

        internal static void update_account_ledger(string Main_account_name, string Alternet_Account, string Amount, string unique_entry_id, string Date, string IDate)
        {
            SqlCommand cmd = new SqlCommand("sp_Account_Voucher_Details");
            cmd.Parameters.AddWithValue("@sp_status ", "UPDATE");
            cmd.Parameters.AddWithValue("@Main_account_name", Main_account_name);
            cmd.Parameters.AddWithValue("@Alternet_Account", Alternet_Account);
            cmd.Parameters.AddWithValue("@Amount ", Amount);
            cmd.Parameters.AddWithValue("@Date ", Date);
            cmd.Parameters.AddWithValue("@IDate ", IDate);
            cmd.Parameters.AddWithValue("@unique_entry_id ", unique_entry_id);
            cmd.Parameters.AddWithValue("@firm ", HMS.firm_id);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
            }
        }

        internal static void update_account_ledger_for_step_down_up(string Amount, string unique_entry_id, string Accountname)
        {
            SqlCommand cmd = new SqlCommand("sp_Account_Voucher_Details");
            cmd.Parameters.AddWithValue("@sp_status ", "STEPDOWNUPUPDATE");
            cmd.Parameters.AddWithValue("@Amount ", Amount);
            cmd.Parameters.AddWithValue("@unique_entry_id ", unique_entry_id);
            cmd.Parameters.AddWithValue("@Main_account_name ", Accountname);
            cmd.Parameters.AddWithValue("@firm ", HMS.firm_id);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
            }
        }

        public static void send_to_cash_payment_Voucher_Details(string Main_account_name, string Alternet_Account, string Amount, string VoucherNo, string unique_entry_id, string Description, string Date, string IDate, string VoucherType, string firm, string Financial_Year, string userid, string Bill_from)
        {
            SqlCommand cmd = new SqlCommand("sp_cash_payment_Voucher_Details");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
            cmd.Parameters.AddWithValue("@Main_account_name", Main_account_name);
            cmd.Parameters.AddWithValue("@Alternet_Account", Alternet_Account);
            cmd.Parameters.AddWithValue("@Description ", Description);
            cmd.Parameters.AddWithValue("@Amount ", Amount);
            cmd.Parameters.AddWithValue("@Date ", Date);
            cmd.Parameters.AddWithValue("@IDate ", IDate);
            cmd.Parameters.AddWithValue("@unique_entry_id ", unique_entry_id);
            cmd.Parameters.AddWithValue("@VoucherNo ", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherType ", VoucherType);
            cmd.Parameters.AddWithValue("@firm ", firm);
            cmd.Parameters.AddWithValue("@Session ", Financial_Year);
            cmd.Parameters.AddWithValue("@Created_by ", userid);
            cmd.Parameters.AddWithValue("@Bill_from ", Bill_from);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
            }

        }

        public static void send_to_bank_payment_Voucher_Details(string Main_account_name, string Alternet_Account, string Amount, string VoucherNo, string unique_entry_id, string Description, string Date, string IDate, string VoucherType, string firm, string Financial_Year, string userid, string Bill_from)
        {
            SqlCommand cmd = new SqlCommand("sp_bank_payment_Voucher_Details");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
            cmd.Parameters.AddWithValue("@Main_account_name", Main_account_name);
            cmd.Parameters.AddWithValue("@Alternet_Account", Alternet_Account);
            cmd.Parameters.AddWithValue("@Description ", Description);
            cmd.Parameters.AddWithValue("@Amount ", Amount);

            cmd.Parameters.AddWithValue("@Date ", Date);
            cmd.Parameters.AddWithValue("@IDate ", IDate);
            cmd.Parameters.AddWithValue("@unique_entry_id ", unique_entry_id);
            cmd.Parameters.AddWithValue("@VoucherNo ", VoucherNo);
            cmd.Parameters.AddWithValue("@VoucherType ", VoucherType);
            cmd.Parameters.AddWithValue("@firm ", firm);
            cmd.Parameters.AddWithValue("@Session ", Financial_Year);
            cmd.Parameters.AddWithValue("@Created_by ", userid);
            cmd.Parameters.AddWithValue("@Bill_from ", Bill_from);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
            }
        }

        public static void Share_service_amount(string Service_group_id, string Service_id, string Main_account_name, string VoucherNo, string unique_entry_id, string Description, string Date, string IDate, string cash_payment, string bank_payment, string bank_accountid, string VoucherType, string firm, string Financial_Year, string userid, string billfrom)
        {
            SqlDataAdapter ad = new SqlDataAdapter(" select *,(select Account_Name from Account_Ledger_Details where Account_id=t1.Account_id) as Account_Name from dbo.[Share_service_amount] t1 where Service_group_id='" + Service_group_id + "' and Service_id='" + Service_id + "'", HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Amount = dr["Share_amount"].ToString();
                    string Account_Name = dr["Account_Name"].ToString();
                    HMS.send_to_account_ledger(Main_account_name, Account_Name, Amount, VoucherNo, unique_entry_id, Description, Date, IDate, cash_payment, bank_payment, bank_accountid, VoucherType, firm, Financial_Year, userid, billfrom);

                }

            }

        }


        //---------------------------Bind DDL----------------------------------
        public void bind_all_ddl_with_id(DropDownList ddl, string query)
        {

            SqlConnection conn = new SqlConnection(HMS.conn);
            ArrayList al = new ArrayList();
            ArrayList a2 = new ArrayList();

            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "tests");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Select";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = ds.Tables[0].Columns[0].ToString();
                ddl.DataValueField = ds.Tables[0].Columns[1].ToString();
            }
            ddl.DataSource = ds.Tables[0];
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }

        public static bool user_permission(string Username, string columnname)
        {
            bool toreturn = false;
            SqlCommand cmd = new SqlCommand("sp_Employee_Details");
            cmd.Parameters.AddWithValue("@sp_status", "USERPERMISSION");
            cmd.Parameters.AddWithValue("@Username", Username);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                if (dt.Rows[0][columnname].ToString() == "True")
                {
                    toreturn = true;
                }
            }
            else
            {
                toreturn = false;
            }
            return toreturn;
        }
        internal static void send_data_to_user_log_history(string description, string userid)
        {
            var list = new SqlParameter[] {
            new SqlParameter("Description", description),
            new SqlParameter("firm", HMS.firm_id),
            new SqlParameter("Date", HMS.datetime()),
            new SqlParameter("idate", HMS.idate()),
            new SqlParameter("user_id", userid),
            new SqlParameter("sp_status", "INSERT") };
            HMS.exeSP("sp_HMS_User_Log_History", list);

        }

        internal static void send_data_to_user_log_history(string description, string userid, string entry_id, string Billing_for)
        {
            var list = new SqlParameter[] {
            new SqlParameter("Description", description),
            new SqlParameter("firm", HMS.firm_id),
            new SqlParameter("Date", HMS.datetime()),
            new SqlParameter("idate", HMS.idate()),
            new SqlParameter("entry_id", entry_id),
            new SqlParameter("Billing_for", Billing_for),
            new SqlParameter("user_id", userid),
            new SqlParameter("sp_status", "INSERT") };
            HMS.exeSP("sp_HMS_User_Log_History", list);

        }
        public static void exeSP(string sp_name, SqlParameter[] list)
        {
            SqlConnection conn = new SqlConnection(HMS.conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = sp_name;
            comm.Parameters.AddRange(list);
            comm.ExecuteNonQuery();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public DataTable FillTable(string sqlQuery)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conn = new SqlConnection(HMS.conn);
            //string connstr = con.connect_method();
            SqlConnection con = new SqlConnection(HMS.conn);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            SqlDataAdapter ad = new SqlDataAdapter(sqlQuery, con);
            ad.Fill(dtTemp);
            if (con.State == ConnectionState.Open) { con.Close(); }
            return dtTemp;
        }
        internal static string exeUrl(string url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            String ss = sr.ReadToEnd();
            sr.Close();
            return ss;

        }
        public static string getdata(string session_name)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_System_setting where session_name='" + session_name + "' ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return "";
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        return dr["session_value"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                HMS.exeSql(@"Create Table dbo.[HMS_System_setting] ( 
    [id] int NOT NULL  Identity (1,1)  PRIMARY KEY ,
    [session_name] varchar(500)  NULL ,
    [session_value] varchar(500)  NULL  
    );");
                getdata(session_name);
            }
            return result;
        }
        public static void setdata(string session_name, string session_value)
        {

            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_System_setting where session_name='" + session_name + "' ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["session_name"] = session_name;
                    dr["session_value"] = session_value;
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dr["session_value"] = session_value;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);

            }
            catch (Exception e)
            {
                if (e.Message.Contains("Invalid object name"))
                {
                    try
                    {
                        HMS.exeSql(@"Create Table dbo.[HMS_System_setting] ( 
    [id] int NOT NULL  Identity (1,1)  PRIMARY KEY ,
    [session_name] varchar(500)  NULL ,
    [session_value] varchar(500)  NULL  
    );");
                        setdata(session_name, session_value);
                    }
                    catch (Exception e1)
                    {
                        HMS.submitexception(e1.ToString());

                    }
                }
                else
                {

                }
            }

        }
        internal static void session(string session_name, string session_value)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_System_setting where  session_name='" + session_name + "'", HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "HMS_System_setting");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["session_name"] = session_name;
                dr["session_value"] = session_value;
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["session_name"] = session_name;
                    dr["session_value"] = session_value;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            HMS.Session[session_name] = session_value;
        }

        internal static string session(string session_name)
        {
            string tureturn = "";
            //if (HMS.Session.Keys.Contains(session_name))
            //{
            //    return HMS.Session[session_name];
            //}
            SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_System_setting where  session_name='" + session_name + "'", HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "HMS_System_setting");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HMS.session(session_name, dr["session_value"].ToString());
                    tureturn = dr["session_value"].ToString();
                }
            }
            else { }

            return tureturn;
        }
        internal static string dir_log;
        public static Dictionary<string, string> Session = new Dictionary<string, string>();
        public static List<String> version_list = new List<string>();
        public static List<String> version_code_list = new List<string>();
        public static Dictionary<String, List<String>> version_query_list = new Dictionary<String, List<String>>();

        public static string get_table_data(string query)
        {

            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        public static DataSet getdataset(string query)
        {
            SqlConnection conn = new SqlConnection(HMS.conn);
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return ds;
        }
        public static ArrayList bindList(string query, string prefix)
        {
            ArrayList lst = new ArrayList();
            DataTable dt = dataTable(query);
            String[] ss = prefix.Split(',');
            lst.AddRange(ss);
            foreach (DataRow dr in dt.Rows)
            {

                lst.Add(dr[0].ToString());
            }
            return lst;
        }
        public static DataTable dataTable(string query)
        {
            SqlConnection conn = new SqlConnection(HMS.conn);
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return ds.Tables[0];
        }

        internal static string find_servicesid(string Bill_Groups_id, string Bill_Service_name, string Created_by)
        {
            Dictionary<string, object> dr = new Dictionary<string, object>();
            dr["Bill_Groups_id"] = Bill_Groups_id;
            dr["Bill_Service_name"] = Bill_Service_name;
            dr["Bill_Service_Status"] = "With Hospital";
            dr["firm"] = HMS.firm_id;
            dr["Istatus"] = "1";
            dr["Created_by"] = Created_by;
            dr["Created_idate"] = HMS.idate();
            dr["sp_status"] = "INSERT";
            DataTable it = HMS.dataTableSP("sp_Bill_Service_Master_Insert", dr);
            if (it.Rows.Count > 0)
            {
                return it.Rows[0][0].ToString();
            }
            else
            {
                return "0";

            }

        }

        public static DataTable dataTable(string query, string con)
        {

            SqlConnection conn = new SqlConnection(con);
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return ds.Tables[0];
        }
        public static string find_db_name()
        {
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("SELECT  DB_NAME() AS DataBaseName", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                return "";
            }

        }

        public static string firm_wise_radiology_auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Radio_globle_data_firm_wise where  firm='" + HMS.firm_id + "' ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["firm"] = HMS.firm_id;
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
                    HMS.exeSql("Alter table Radio_globle_data_firm_wise add " + column + " varchar(50)");
                    result = firm_wise_auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }
        public static string firm_wise_radiology_view_auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Radio_globle_data_firm_wise where  firm='" + HMS.firm_id + "' ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["firm"] = HMS.firm_id;
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
                    HMS.exeSql("Alter table Radio_globle_data_firm_wise add " + column + " varchar(50)");
                    result = firm_wise_view_auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }


        public static string firm_wise_auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data_firm_wise where  firm='" + HMS.firm_id + "' ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["firm"] = HMS.firm_id;
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
                    HMS.exeSql("Alter table globle_data_firm_wise add " + column + " varchar(50)");
                    result = firm_wise_auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }

        public static string firm_wise_view_auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data_firm_wise where  firm='" + HMS.firm_id + "' ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["firm"] = HMS.firm_id;
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
                    HMS.exeSql("Alter table globle_data_firm_wise add " + column + " varchar(50)");
                    result = firm_wise_view_auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }

        public static string session_wise_auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data_session_wise where  firm='" + HMS.firm_id + "' and session='" + HMS.Financial_Year + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["session"] = HMS.Financial_Year;
                    dr["firm"] = HMS.firm_id;
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
                    HMS.exeSql("Alter table globle_data_session_wise add " + column + " varchar(50)");
                    result = session_wise_auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }

        public static string session_wise_view_auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data_session_wise where  firm='" + HMS.firm_id + "' and session='" + HMS.Financial_Year + "' ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["session"] = HMS.Financial_Year;
                    dr["firm"] = HMS.firm_id;
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
                    HMS.exeSql("Alter table globle_data_session_wise add " + column + " varchar(50)");
                    result = session_wise_view_auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }



        internal static void save_hms_receipt_no_history(string unique_entry_id, string receipt_no, string desc)
        {
            try
            {
                string qry = "insert into hms_receipt_no_history(unique_entry_id,receipt_no,Desription) values ('" + unique_entry_id + "','" + receipt_no + "',N'" + desc + "');";
                exeSql(qry);
            }
            catch (Exception ex)
            {
            }
        }
        public static void default_menu_permission(string Employee_id, string Usertype)
        {

            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data_session_wise where  firm='" + HMS.firm_id + "' and session='" + HMS.Financial_Year + "' ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["session"] = HMS.Financial_Year;
                    dr["firm"] = HMS.firm_id;

                    dt.Rows.Add(dr);
                }

                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception e)
            {
            }
        }
        internal static string check_doctorwise_prescription(string usertype, string column, string doctor_id, string Financial_Year, string firm)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_Doctor_wise_Invoice_serial_no_setup where   Session='" + Financial_Year + "' and Firm_id='" + firm + "' and Doctor_id='" + doctor_id + "' and Billing_for='" + usertype + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    //Do Nothing
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[column].ToString() == "")
                        {
                            result = "";
                        }
                        else
                        {
                            if (dr["Invoice_Length"].ToString().Length > 0)
                            {

                                result = HMS.toDouble(dr[column].ToString()).ToString(dr["Invoice_Length"].ToString());
                            }
                            else
                            {
                                result = dr[column].ToString();
                            }

                            result = dr["Invoice_Prefix"].ToString() + result + dr["Invoice_Postfix"].ToString();
                        }
                        dr[column] = Convert.ToDouble(dr[column]) + 1;
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
                    HMS.exeSql("Alter table globle_data_session_wise add " + column + " varchar(50)");
                    result = session_wise_view_auto_serial(column, Financial_Year, firm);
                }
                else
                {
                }
            }
            return result;
        }

        internal static void submitException(Exception ex, string page)
        {
            SqlCommand cmd;
            string strQuery = @"INSERT INTO Exception_table (message,error_name,date) values (@message,@error_name,@date)";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@error_name", ex.ToString());
            cmd.Parameters.AddWithValue("@message", page);
            cmd.Parameters.AddWithValue("@date", DateTime.UtcNow.AddHours(5).AddMinutes(30));
            if (InsertUpdateData(cmd))
            {
            }
        }
        internal static bool valid_date(string date)
        {
            if (HMS.toDateTime(date) >= HMS.start_date && HMS.toDateTime(date) <= HMS.end_date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean InsertUpdateData(SqlCommand cmd)
        {
            bool status = false;

            SqlConnection con = new SqlConnection(HMS.conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            status = true;
            con.Close();
            con.Dispose();
            return status;
        }

        public DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = HMS.conn;
            SqlConnection con = new SqlConnection(strConnString);

            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
        public void bind_all_ddl_with_id_All(DropDownList ddl, string strQuery, string firstindex)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Select";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(firstindex, "0"));
        }


        public DataTable FillData(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, HMS.conn);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {
            }
            return dtc;
        }
        public static string create_random_no_otp()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random();
            int tempo = random.Next(100000, 999999);
            return tempo.ToString();
        }
        public static String Format_Sample { get { try { return "dd/MM/yyyy"; } catch { return "dd/MM/yyyy"; } } }
        public static String Working_shift { get { try { return "Single"; } catch { return "Single"; } } }
        public static String set_minimun_hour_to_calculate_present { get { try { return "4"; } catch { return "4"; } } }



        internal string get_acutal_empid(string empid)
        {
            SqlCommand cmd = new SqlCommand("Select Emp_Code from PRL_Employee_Master where Employee_id='" + empid + "'");
            DataTable dt = GetDataq(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Emp_Code"].ToString();

            }
        }
        public static DataSet executeReaderDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();

            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                scon.Open();
                da.Fill(ds);
                scon.Close();
            }

            return ds;
        }
        private static DataTable GetDataq(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = HMS.conn;
            SqlConnection con = new SqlConnection(strConnString);

            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
        //--------------------Payroll----------------

        public static double employer_pf = 13.36;
        public static double employer_esi = 3.25;

        internal static string getMonthS_name(string text)
        {
            try
            {
                if (text == "1" || text == "01")
                {
                    return "Jan";
                }
                else if (text == "2" || text == "02")
                {
                    return "Feb";
                }
                else if (text == "3" || text == "03")
                {
                    return "Mar";
                }
                else if (text == "4" || text == "04")
                {
                    return "Apr";
                }
                else if (text == "5" || text == "05")
                {
                    return "May";
                }
                else if (text == "6" || text == "06")
                {
                    return "Jun";
                }

                else if (text == "7" || text == "07")
                {
                    return "Jul";
                }
                else if (text == "8" || text == "08")
                {
                    return "Aug";
                }
                else if (text == "9" || text == "09")
                {
                    return "Sep";
                }
                else if (text == "10")
                {
                    return "Oct";
                }
                else if (text == "11")
                {
                    return "Nov";
                }
                else
                {
                    return "Dec";
                }
            }
            catch
            {
                return "0";
            }
        }



        internal static string getMonthS_twoDigit(string text)
        {
            try
            {
                if (text == "1")
                {
                    return "01";
                }
                else if (text == "2")
                {
                    return "02";
                }
                else if (text == "3")
                {
                    return "03";
                }
                else if (text == "4")
                {
                    return "04";
                }
                else if (text == "5")
                {
                    return "05";
                }
                else if (text == "6")
                {
                    return "06";
                }

                else if (text == "7")
                {
                    return "07";
                }
                else if (text == "8")
                {
                    return "08";
                }
                else if (text == "9")
                {
                    return "09";
                }
                else
                {
                    return text;
                }
            }
            catch
            {
                return "0";
            }
        }

        public string year()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");
        }

        internal static object toInt(string text)
        {
            try
            {
                return Convert.ToInt32(text);
            }
            catch
            {
                return 0;
            }

        }

        internal static int toIntS(object text)
        {
            try
            {
                return Convert.ToInt32(text);
            }
            catch
            {
                return 0;
            }
        }

        internal static string ddl_value(object selecteditem, string column_name)
        {
            try
            {
                return ((DataRowView)selecteditem).Row[column_name].ToString();
            }
            catch
            {
                return "";
            }
        }


        public static IEnumerable bindMonth()
        {
            List<string> lst = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                lst.Add(i.ToString("00"));
            }
            return lst;
        }
        public static ArrayList bindMonthName()
        {
            ArrayList lst = new ArrayList();
            lst.Add("January");
            lst.Add("February");
            lst.Add("March");
            lst.Add("April");
            lst.Add("May");
            lst.Add("June");
            lst.Add("July");
            lst.Add("August");
            lst.Add("September");
            lst.Add("October");
            lst.Add("November");
            lst.Add("December");
            return lst;
        }
        public static ArrayList bindMonthNameSort()
        {
            ArrayList lst = new ArrayList();
            lst.Add("Jan");
            lst.Add("Feb");
            lst.Add("Mar");
            lst.Add("Apr");
            lst.Add("May");
            lst.Add("Jun");
            lst.Add("Jul");
            lst.Add("Aug");
            lst.Add("Sep");
            lst.Add("Oct");
            lst.Add("Nov");
            lst.Add("Dec");
            return lst;
        }

        public static IEnumerable bindYear()
        {
            List<string> lst = new List<string>();
            for (int i = 2015; i <= DateTime.Now.Year; i++)
            {
                lst.Add(i.ToString());
            }
            return lst;
        }


        public static bool isDeviceConnected = false;
        public static DataTable dv_dt;
        public static ZkemClient objZkeeper;
        public static bool connect_device(string ip, string device_name)
        {
            try
            {
                string ipAddress = ip;// HMS.session("DeviceIP").Trim();
                string port = "4370"; //"4370";// HMS.session("Port").Trim();
                if (ipAddress == string.Empty || port == string.Empty)
                    throw new Exception("The Device IP Address and Port is mandatory !!");
                if (!UniversalStatic.PingTheDevice(ipAddress))
                {
                    //MessageBox.Show("Device ip not pinging");
                    return false;
                }
                int portNumber = 4370;
                if (!int.TryParse(port, out portNumber))
                    throw new Exception("Not a valid port number");

                bool isValidIpA = UniversalStatic.ValidateIP(ipAddress);
                if (!isValidIpA)
                    throw new Exception("The Device IP is invalid !!");
                isValidIpA = UniversalStatic.PingTheDevice(ipAddress);

                if (!isValidIpA)
                    throw new Exception("The device at " + ipAddress + ":" + port + " did not respond!!");

                HMS.objZkeeper = new ZkemClient(RaiseDeviceEvent);
                IsDeviceConnected = HMS.objZkeeper.Connect_Net(ipAddress, portNumber);
                if (IsDeviceConnected)
                {
                    manipulator = new DeviceManipulator();
                }
                return isDeviceConnected;


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            // win.Cursor = Cursors.Arrow;
            return false;
        }


        public static bool IsDeviceConnected
        {
            get { return HMS.isDeviceConnected; }
            set
            {
                HMS.isDeviceConnected = value;
                if (HMS.isDeviceConnected)
                {
                    // ShowStatusBar("The device is connected !!", true);
                    // btnConnect.Content = "Disconnect";
                    // ToggleControls(true);
                }
                else
                {
                    //  ShowStatusBar("The device is diconnected !!", true);
                    HMS.objZkeeper.Disconnect();
                    //   btnConnect.Content = "Connect";
                    //ToggleControls(false);
                }
            }
        }
        public static DeviceManipulator manipulator;
        public static void RaiseDeviceEvent(object sender, string actionType)
        {
            switch (actionType)
            {
                case UniversalStatic.acx_Disconnect:
                    {
                        //ShowStatusBar("The device is switched off", true);
                        //  DisplayEmpty();
                        //   btnConnect.Content = "Connect";
                        //   ToggleControls(false);
                        break;
                    }

                default:
                    break;
            }
        }



        internal static int toInt(object text)
        {
            try
            {
                return Convert.ToInt32(text);
            }
            catch
            {
                return 0;
            }
        }

        internal object get_employyenameand_code(string p)
        {
            SqlCommand cmd = new SqlCommand("Select Employee_Name,Emp_Code from PRL_Employee_Master where Employee_id='" + p + "'");
            DataTable dt = GetDataq(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Employee_Name"].ToString() + "-" + dt.Rows[0]["Emp_Code"].ToString();

            }
        }

        public static Dictionary<string, object> get_workinglimit()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from Payroll_Setting  ";
            SqlCommand cmd;

            cmd = new SqlCommand(query);
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                dc["limit_fullday"] = "0";
                dc["limit_halfday"] = "0";


            }
            else
            {
                dc["limit_fullday"] = dt.Rows[0]["limit_fullday"].ToString();
                dc["limit_halfday"] = dt.Rows[0]["limit_halfday"].ToString();



            }
            return dc;

        }

        internal static DateTime toDateTime(string p)
        {
            try
            {
                return Convert.ToDateTime(p);
            }
            catch
            {
                return DateTime.Now;
            }
        }
        public static void bind_ddl_select(DropDownList ddl, string query)
        {
            SqlDataAdapter ad = new SqlDataAdapter(query, HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            ArrayList ar = new ArrayList();
            ar.Add("Select");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }
        public void bind_ddl(DropDownList ddl, string strQuery)
        {

            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("Select");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }
        public void bind_ddlall(DropDownList ddl, string strQuery)
        {

            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("ALL");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }
        public static string data_(string query)
        {
            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        internal static bool toBool(object p)
        {
            try
            {
                return Convert.ToBoolean(p);
            }
            catch
            {
                return false;
            }
        }
        public void executequery(string query)
        {


            SqlCommand cmd;
            cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(HMS.conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();

            con.Close();
            con.Dispose();


        }
        public void insert_data_logfile(string userid, string firm, string msg, string Branchid)
        {
            string strQuery = " INSERT INTO User_Log_History (Date,Description,user_id,firm,idate,Branch_id) values (@Date,@Description,@user_id,@firm,@idate,@Branch_id)";
            string message = msg;

            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Date", HMS.getdate1());
            cmd.Parameters.AddWithValue("@Description", message);
            cmd.Parameters.AddWithValue("@user_id", userid);
            cmd.Parameters.AddWithValue("@firm", firm);
            cmd.Parameters.AddWithValue("@idate", idate());
            cmd.Parameters.AddWithValue("@Branch_id", Branchid);

            if (HMS.InsertUpdateData(cmd))
            {

            }
        }
        public int ConvertStringToiDate(string DateInString) //Format :: dd/MM/yyyy
        {
            DateInString = DateInString.Substring(6, 4) + DateInString.Substring(3, 2) + DateInString.Substring(0, 2);
            return Convert.ToInt32(DateInString);
        }

        public int ConvertStringToiDateup(string DateInString) //Format :: dd/MM/yyyy
        {
            try
            {
                DateInString = DateInString.Substring(6, 4) + DateInString.Substring(3, 2) + DateInString.Substring(0, 2);
                return Convert.ToInt32(DateInString);
            }
            catch
            {
                return Convert.ToInt32("0");
            }
        }
        public static string getdate1()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MMM/yyyy hh:mm:ss tt");

        }
        public static string view_auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data ", conn);
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
                    HMS.exeSql("Alter table globle_data add " + column + " varchar(50)");
                    result = view_auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }

        //this function Convert to Encord your Password 
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public static string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        public static DataTable dataTableSP(string sp_name, Dictionary<string, object> param)
        {
            SqlConnection conn = new SqlConnection(HMS.conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = sp_name;
            if (param != null)
                foreach (KeyValuePair<string, object> kvp in param)
                    comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
            SqlDataAdapter ad = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return ds.Tables[0];

        }
        internal static void save_Account_Ledger_Details(string account_name, string account_id, string grop_id)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Account_Ledger_Details where  Account_id ='" + account_id + "' and firm='" + HMS.firm_id + "'", HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Account_Ledger_Details");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["Account_Name"] = account_name;
                dr["Account_id"] = account_id;
                dr["Group_id"] = grop_id;
                dr["firm"] = HMS.firm_id;
                dt.Rows.Add(dr);
            }
            else
            {

                foreach (DataRow dr in dt.Rows)
                {
                    dr["Account_Name"] = account_name;
                    dr["Account_id"] = account_id;
                    dr["Group_id"] = grop_id;
                    dr["firm"] = HMS.firm_id;
                    break;
                }

            }

            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);

        }

        internal static string Unique_alphanumeric_code(int length = 8)
        {
            Random r = new Random(DateTime.Now.Millisecond);


            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            //string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = "";
            characters += alphabets + numbers;

            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp + r.Next(9, 99);
        }
        public static bool check_dauplicate_pin(string qry)
        {
            SqlDataAdapter ad = new SqlDataAdapter(qry, HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "tempo");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool check_attendence_device_connected()
        {
            bool tureturn = false;
            SqlConnection conn = new SqlConnection(HMS.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Device_Details", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (HMS.connect_device(dr["Device_Ip"].ToString(), dr["Device_Name"].ToString()))
                {
                    tureturn = true;
                }
            }
            return tureturn;
        }
        public static Dictionary<String, String> group = new Dictionary<String, String>();
        internal static bool landscape;

        public static void fetch_all_account()
        {
            group = new Dictionary<String, String>();
            DataTable dt = HMS.dataTable("select  * from dbo.[Account_Ledger_Details]  where  firm = '" + HMS.firm_id + "'");
            foreach (DataRow dr in dt.Rows)
            {
                group[dr["Account_id"].ToString()] = dr["Group_id"].ToString();
            }
        }
          public static void SendMail_with_attachfile(string strSubject, string strMSG, ArrayList list)
        {

            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_Mail_setup", HMS.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "HMS_Mail_setup");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string Sender_Email_ID = dr["Sender_Email_ID"].ToString();
                    string password = dr["Mail_Password"].ToString();
                    string Receiver_mail_id = dr["Receiver_mail_id"].ToString();

                    MailMessage message1 = new MailMessage();
                    message1.From = new MailAddress(Sender_Email_ID);

                    string[] allmailid = Receiver_mail_id.Split(',');
                    foreach (string emails in allmailid)
                    {
                        message1.To.Add(new MailAddress(emails));
                    }

                    message1.Subject = strSubject;
                    message1.Body = strMSG;
                    message1.IsBodyHtml = true;
                    message1.BodyEncoding = Encoding.UTF8;
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    NetworkCredential networkCredential = new NetworkCredential(Sender_Email_ID, password);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = (ICredentialsByHost)networkCredential;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    System.Net.Mail.Attachment attachment;
                    if (list.Count > 0)
                    {
                        foreach (string value in list)
                        {
                            attachment = new System.Net.Mail.Attachment(value);
                            message1.Attachments.Add(attachment);
                        }
                    }
                    smtpClient.Send(message1);
                }
            }
            catch (Exception ex)
            {
                HMS.submitexception(ex.ToString());
            }

            //try
            //{

            //    string to = strMail; //To address    
            //    string from = "Noreply@lokshikayatbihargov.in"; //From address    
            //    MailMessage message = new MailMessage(from, to);

            //    string mailbody = strMSG;
            //    message.Subject = strSubject;
            //    message.Body = mailbody;
            //    message.BodyEncoding = Encoding.UTF8;
            //    message.IsBodyHtml = true;
            //    SmtpClient client = new SmtpClient("smtp.office365.com", 587); //smtp.office365.com
            //    System.Net.NetworkCredential basicCredential1 = new
            //    System.Net.NetworkCredential("Noreply@lokshikayatbihargov.in", "Jofo8559");
            //    client.EnableSsl = true;
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = basicCredential1;

            //    System.Net.Mail.Attachment attachment;
            //    if (list.Count > 0)
            //    {
            //        foreach (string value in list)
            //        {
            //            attachment = new System.Net.Mail.Attachment(value);
            //            message.Attachments.Add(attachment);
            //        }
            //    }

            //    client.Send(message);

            //}
            //catch (Exception ex)
            //{
            //    submitException(ex.ToString(), "SendMail");

            //}
        }
        public static void SendMail_with_attachfile_email(string strSubject, string strMSG, ArrayList list, string Receiver_mail_id)
        {

            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from HMS_Mail_setup", HMS.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "HMS_Mail_setup");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string Sender_Email_ID = dr["Sender_Email_ID"].ToString();
                    string password = dr["Mail_Password"].ToString();

                    MailMessage message1 = new MailMessage();
                    message1.From = new MailAddress(Sender_Email_ID);

                    string[] allmailid = Receiver_mail_id.Split(',');
                    foreach (string emails in allmailid)
                    {
                        message1.To.Add(new MailAddress(emails));
                    }

                    message1.Subject = strSubject;
                    message1.Body = strMSG;
                    message1.IsBodyHtml = true;
                    message1.BodyEncoding = Encoding.UTF8;
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    NetworkCredential networkCredential = new NetworkCredential(Sender_Email_ID, password);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = (ICredentialsByHost)networkCredential;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    System.Net.Mail.Attachment attachment;
                    if (list.Count > 0)
                    {
                        foreach (string value in list)
                        {
                            attachment = new System.Net.Mail.Attachment(value);
                            message1.Attachments.Add(attachment);
                        }
                    }
                    smtpClient.Send(message1);
                }
            }
            catch (Exception ex)
            {
                HMS.submitexception(ex.ToString());
            }

            //try
            //{

            //    string to = strMail; //To address    
            //    string from = "Noreply@lokshikayatbihargov.in"; //From address    
            //    MailMessage message = new MailMessage(from, to);

            //    string mailbody = strMSG;
            //    message.Subject = strSubject;
            //    message.Body = mailbody;
            //    message.BodyEncoding = Encoding.UTF8;
            //    message.IsBodyHtml = true;
            //    SmtpClient client = new SmtpClient("smtp.office365.com", 587); //smtp.office365.com
            //    System.Net.NetworkCredential basicCredential1 = new
            //    System.Net.NetworkCredential("Noreply@lokshikayatbihargov.in", "Jofo8559");
            //    client.EnableSsl = true;
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = basicCredential1;

            //    System.Net.Mail.Attachment attachment;
            //    if (list.Count > 0)
            //    {
            //        foreach (string value in list)
            //        {
            //            attachment = new System.Net.Mail.Attachment(value);
            //            message.Attachments.Add(attachment);
            //        }
            //    }

            //    client.Send(message);

            //}
            //catch (Exception ex)
            //{
            //    submitException(ex.ToString(), "SendMail");

            //}
        }

        internal static double find_card_discount_service_wise(string health_card_id, string billing_for, string Bill_group_id, string service_id)
        {
            string qry = "select top 1 * from HMS_Healthcard_servicewise_discount where Health_Card_id='" + health_card_id + "' and Bill_type='" + billing_for + "' and Bill_group_id='" + Bill_group_id + "' and Bill_services_id='" + service_id + "' ";
            DataTable dt = HMS.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                //if bill wise discount not set then check services wise discount.
                return Convert.ToDouble(dt.Rows[0]["Discount_Percent"].ToString());
            }
            else
                return 0;
        }

        public static void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
            CopyAll(diSource, diTarget);
        }
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }
            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);

            }
        }
    }

    public class Age
    {
        public int Years;
        public int Months;
        public int Days;

        public Age(DateTime Bday)
        {
            this.Count(Bday);
        }

        public Age(DateTime Bday, DateTime Cday)
        {
            this.Count(Bday, Cday);
        }

        public Age Count(DateTime Bday)
        {
            return this.Count(Bday, DateTime.Today);
        }

        public Age Count(DateTime Bday, DateTime Cday)
        {

            if ((Cday.Year - Bday.Year) > 0 ||
                (((Cday.Year - Bday.Year) == 0) && ((Bday.Month < Cday.Month) ||
                  ((Bday.Month == Cday.Month) && (Bday.Day <= Cday.Day)))))
            {
                int DaysInBdayMonth = DateTime.DaysInMonth(Bday.Year, Bday.Month);
                int DaysRemain = Cday.Day + (DaysInBdayMonth - Bday.Day);

                if (Cday.Month > Bday.Month)
                {
                    this.Years = Cday.Year - Bday.Year;
                    this.Months = Cday.Month - (Bday.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
                else if (Cday.Month == Bday.Month)
                {
                    if (Cday.Day >= Bday.Day)
                    {
                        this.Years = Cday.Year - Bday.Year;
                        this.Months = 0;
                        this.Days = Cday.Day - Bday.Day;
                    }
                    else
                    {
                        this.Years = (Cday.Year - 1) - Bday.Year;
                        this.Months = 11;
                        this.Days = DateTime.DaysInMonth(Bday.Year, Bday.Month) - (Bday.Day - Cday.Day);
                    }
                }
                else
                {
                    this.Years = (Cday.Year - 1) - Bday.Year;
                    this.Months = Cday.Month + (11 - Bday.Month) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
            }
            else
            {
                throw new ArgumentException("Birthday date must be earlier than current date");
            }
            return this;


        }
    }

    public class HMS_SMS
    {
        public string PatientName { get; set; }
        public string PRegNo { get; set; }
        public string Age { get; set; }
        public string sex { get; set; }
        public string CDoctorName { get; set; }
        public string ReferredBy { get; set; }
        public string ReceptNo { get; set; }
        public string IPDNo { get; set; }
        public string Amount { get; set; }
        public string HospitalAddress { get; set; }
        public string HospitalName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string RoomNo { get; set; }
        public string BedNo { get; set; }
        public string OperationName { get; set; }
        public string username { get; set; }
        public string OTP { get; set; }
        public string cashreceivedamt { get; set; }
        public string bankreceivedamt { get; set; }
        public string cashrefundamt { get; set; }
        public string backrefundamt { get; set; }
        public string cashexpenseamt { get; set; }
        public string backexpenseamt { get; set; }
        public string cashinhandamt { get; set; }
    }


    public class InsertUpdate
    {
        public static Boolean InsertUpdateData(SqlCommand cmd)
        {

            bool result = false;
            SqlConnection con = new SqlConnection(HMS.conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            result = true;
            return result;
        }

        public static DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = HMS.conn;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        public static void submit_exception(Exception exception, string page_name)
        {

            SqlConnection con = new SqlConnection(HMS.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Exception", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exception");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            DataRow dr = dt.NewRow();
            dr[1] = exception;
            dr[2] = (DateTime.UtcNow.AddHours(5).AddMinutes(30)).ToString("dd/MM/yyyy");
            dr[3] = (DateTime.UtcNow.AddHours(5).AddMinutes(30)).ToString("yyyyMMdd");
            dr[4] = page_name;
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);

        }

        public static void fetch_year(DropDownList ddl)
        {
            ArrayList ar = new ArrayList();
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            int year = dt.AddYears(1).Year;
            for (int i = 2018; i <= year; i++)
            {
                ar.Add(i);
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }



        //public static Boolean InsertUpdateData_onlinetest(SqlCommand cmd)
        //{

        //    bool result = false;
        //    SqlConnection con = new SqlConnection(UsesCode.cononlinetest);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = con;
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    con.Dispose();
        //    result = true;
        //    return result;
        //}
    }
    public class Message_sending
    {
        public static void fetchsmsformat(string sms_format_name, string button_name, string messageto, string userid, string Patient_Name, string PReg_No, string CDoctor_Name, string Referred_By, string IPD_No, string Amount, string HospitalAddress, string HospitalName, string Date, string Time, string RoomNo, string BedNo)
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_SMS_format_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch");
            cmd.Parameters.AddWithValue("@sms_format_name", sms_format_name);
            cmd.Parameters.AddWithValue("@button_name", button_name);
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int send_to_admin = 0;
                    if (dr["Is_send_to_admin"].ToString() == "True")
                        send_to_admin = 1;



                    HMS.sms_format = dr["sms_formate"].ToString();
                    string message = Replace_string_containt.Inject_data(HMS.sms_format, new HMS_SMS
                    {
                        PatientName = Patient_Name,
                        PRegNo = PReg_No,
                        CDoctorName = CDoctor_Name,
                        ReferredBy = Referred_By,
                        IPDNo = IPD_No,
                        Amount = Amount,
                        HospitalAddress = HospitalAddress,
                        HospitalName = HospitalName,
                        Date = Date,
                        Time = Time,
                        RoomNo = RoomNo,
                        BedNo = BedNo,
                    });

                    Message_sending.SendMsg(messageto, userid, message, "English", send_to_admin);


                }
            }
        }
        public static void SendMsg(string messageto, string userid, string message, string content_type, int is_sendto_admin = 0)
        {
            try
            {
                string qry = "";
                if (messageto == "DOCTOR")
                    qry = "Select *,(Select top 1 Mobile_no from dbo.[HMS_Employee_Details] where Employee_id='" + userid + @"' order by id asc) as Mobile_no,(Select top 1 mobile from dbo.[HMS_user_details] where user_type='Admin' order by id asc) as admin_Mobile_no from message_config";
                if (messageto == "PATIENT")
                    qry = "Select *,(Select top 1 Mobile_no from dbo.[HMS_Patient_registration] where Patient_Registration_no='" + userid + @"' order by id asc) as Mobile_no,(Select top 1 mobile from dbo.[HMS_user_details] where user_type='Admin' order by id asc) as admin_Mobile_no from message_config";
                if (messageto == "REFERREDBY")
                    qry = "Select *,(Select top 1 Mobile_no from dbo.[HMS_Referral_doctor_details] where Doctorname='" + userid + @"' order by id asc) as Mobile_no,(Select top 1 mobile from dbo.[HMS_user_details] where user_type='Admin' order by id asc) as admin_Mobile_no from message_config";

                if (messageto == "ADMIN")
                    qry = "Select *,(Select top 1 mobile from dbo.[HMS_user_details] where user_type='Admin' order by id asc) as Mobile_no from message_config";

                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter(qry, conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "message_config");
                DataTable dt = ds.Tables[0];
                int rowcount = dt.Rows.Count;
                if (rowcount > 0)
                {

                    string SMS_API = dt.Rows[0]["SMS_API"].ToString();
                    string Sender_Id = dt.Rows[0]["Sender_Id"].ToString();
                    string SMS_Route = dt.Rows[0]["SMS_Route"].ToString();
                    string full_url = dt.Rows[0]["full_url"].ToString();
                    string url = dt.Rows[0]["url"].ToString();
                    string Mobile_no = dt.Rows[0]["Mobile_no"].ToString();
                    string originalsms = message;
                    message = Uri.EscapeDataString(message);
                    if (Mobile_no != "")
                    {
                        if (is_sendto_admin == 1)
                            Mobile_no = Mobile_no + "," + dt.Rows[0]["admin_Mobile_no"].ToString();
                        string _url = "";


                        if (url == "http://mysms.msgclub.net")
                        {
                            _url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + SMS_API + "&message=" + message + "&senderId=" + Sender_Id + "&routeId=" + SMS_Route + "&mobileNos=" + Mobile_no + "&smsContentType=" + content_type;
                        }
                        else if (url == "custom")
                        {
                            _url = String.Format(full_url, Mobile_no, message);
                        }
                        else
                        {
                            _url = url + "/smsapi.aspx?type=send_sms&client_id=" + SMS_API + "&message=" + message + "&mobile=" + Mobile_no;
                        }
                        //ServicePointManager.Expect100Continue = true;
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        send_message_details_in_Message_send_details(Mobile_no, originalsms, "SEND", userid, _url, messageto);
                        Message_sending.mesnd_message_to_whatsapp(message, Mobile_no);
                    }
                }
            }
            catch (Exception ex)
            {
                HMS.submitexception("Exception from Message =" + ex.ToString());

            }
        }
        public static void SendMsg(string messageto, string userid, string message, string messageid, string Variables, string content_type, int is_sendto_admin = 0)
        {
            try
            {
                string qry = "";
                if (messageto == "DOCTOR")
                    qry = "Select *,(Select top 1 Mobile_no from dbo.[HMS_Employee_Details] where Employee_id='" + userid + @"' order by id asc) as Mobile_no,(Select top 1 mobile from dbo.[HMS_user_details] where user_type='Admin' order by id asc) as admin_Mobile_no from message_config";
                if (messageto == "PATIENT")
                    qry = "Select *,(Select top 1 Mobile_no from dbo.[HMS_Patient_registration] where Patient_Registration_no='" + userid + @"' order by id asc) as Mobile_no,(Select top 1 mobile from dbo.[HMS_user_details] where user_type='Admin' order by id asc) as admin_Mobile_no from message_config";
                if (messageto == "REFERREDBY")
                    qry = "Select *,(Select top 1 Mobile_no from dbo.[HMS_Referral_doctor_details] where Doctorname='" + userid + @"' order by id asc) as Mobile_no,(Select top 1 mobile from dbo.[HMS_user_details] where user_type='Admin' order by id asc) as admin_Mobile_no from message_config";
                if (messageto == "ADMIN")
                    qry = "Select *,(Select top 1 mobile from dbo.[HMS_user_details] where user_type='Admin' order by id asc) as Mobile_no from message_config";


                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter(qry, conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "message_config");
                DataTable dt = ds.Tables[0];
                int rowcount = dt.Rows.Count;
                if (rowcount > 0)
                {

                    string SMS_API = dt.Rows[0]["SMS_API"].ToString();
                    string Sender_Id = dt.Rows[0]["Sender_Id"].ToString();
                    string SMS_Route = dt.Rows[0]["SMS_Route"].ToString();
                    string full_url = dt.Rows[0]["full_url"].ToString();
                    string url = dt.Rows[0]["url"].ToString();
                    string Mobile_no = dt.Rows[0]["Mobile_no"].ToString();
                    string originalsms = message;

                    message = Uri.EscapeDataString(message);
                    if (Mobile_no != "")
                    {
                        if (is_sendto_admin == 1)
                            Mobile_no = Mobile_no + "," + dt.Rows[0]["admin_Mobile_no"].ToString();
                        string _url = "";


                        if (url == "http://mysms.msgclub.net")
                        {
                            _url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + SMS_API + "&message=" + message + "&senderId=" + Sender_Id + "&routeId=" + SMS_Route + "&mobileNos=" + Mobile_no + "&smsContentType=" + content_type;
                        }
                        else if (url == "custom")
                        {

                            _url = String.Format(HMS.full_url, messageid, Variables, Mobile_no);
                        }
                        else
                        {
                            _url = url + "/smsapi.aspx?type=send_sms&client_id=" + SMS_API + "&message=" + message + "&mobile=" + Mobile_no;
                        }
                        //ServicePointManager.Expect100Continue = true;
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        send_message_details_in_Message_send_details(Mobile_no, originalsms, "SEND", userid, _url, messageto);

                    }
                }
            }
            catch (Exception ex)
            {
                HMS.submitexception("Exception from Message =" + ex.ToString());

            }
        }

        private static void send_message_details_in_Message_send_details(string mobileno, string message, string results, string membercode, string url, string messageto)
        {

            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("dd/MM/yyyy");
            string time = dtm.ToString("hh:mm:ss tt");
            SqlConnection conn6 = new SqlConnection(HMS.conn);
            SqlDataAdapter ad6 = new SqlDataAdapter("Select * from Message_send_details", conn6);
            DataSet ds6 = new DataSet();
            ad6.Fill(ds6, "Message_send_details");
            DataTable dt6 = ds6.Tables[0];
            DataRow dr6 = dt6.NewRow();
            dr6[1] = membercode;
            dr6[2] = mobileno;
            dr6[3] = date;
            dr6[4] = message;
            dr6[5] = results;
            dr6[6] = time;
            dr6[7] = url.ToString();
            dr6[8] = "";
            dr6[9] = messageto;
            dt6.Rows.Add(dr6);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad6);
            ad6.Update(dt6);
        }

        public static bool send(string message, string mobile, string content_type, string messageto, int is_sendto_admin = 0)
        {
            bool tureturn = false;
            try
            {
                string qry = "";
                qry = "Select *,(Select top 1 mobile from dbo.[HMS_user_details] where user_type='Admin' order by id asc) as Mobile_no from message_config";
                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter(qry, conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "message_config");
                DataTable dt = ds.Tables[0];
                int rowcount = dt.Rows.Count;
                if (rowcount > 0)
                {

                    string SMS_API = dt.Rows[0]["SMS_API"].ToString();
                    string Sender_Id = dt.Rows[0]["Sender_Id"].ToString();
                    string SMS_Route = dt.Rows[0]["SMS_Route"].ToString();
                    string full_url = dt.Rows[0]["full_url"].ToString();
                    string url = dt.Rows[0]["url"].ToString();
                    string originalsms = message;

                    message = Uri.EscapeDataString(message);
                    if (mobile != "")
                    {
                        if (is_sendto_admin == 1)
                            mobile = mobile + "," + dt.Rows[0]["Mobile_no"].ToString();
                        string _url = "";
                        if (HMS.url == "http://mysms.msgclub.net")
                        {
                            _url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + SMS_API + "&message=" + message + "&senderId=" + Sender_Id + "&routeId=" + SMS_Route + "&mobileNos=" + mobile + "&smsContentType=" + content_type;
                        }
                        else if (HMS.url == "custom")
                        {
                            _url = String.Format(HMS.full_url, mobile, message);
                        }
                        else
                        {
                            _url = HMS.url + "/smsapi.aspx?type=send_sms&client_id=" + SMS_API + "&message=" + message + "&mobile=" + mobile;
                        }

                        //ServicePointManager.Expect100Continue = true;
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        send_message_details_in_Message_send_details(mobile, originalsms, "SEND", mobile, _url, messageto);
                        Message_sending.mesnd_message_to_whatsapp(message, mobile);
                        tureturn = true;
                    }
                }
                return tureturn;
            }
            catch (Exception ex)
            {
                HMS.submitexception("Exception from Message =" + ex.ToString());
                return false;
            }
        }

        public static bool send(string message, string messageid, string mobile, string Variables, string content_type, string messageto, int is_sendto_admin = 0)
        {
            bool tureturn = false;
            try
            {
                string qry = "";
                qry = "Select *,(Select top 1 mobile from dbo.[HMS_user_details] where user_type='Admin' order by id asc) as Mobile_no from message_config";

                SqlConnection conn = new SqlConnection(HMS.conn);
                SqlDataAdapter ad = new SqlDataAdapter(qry, conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "message_config");
                DataTable dt = ds.Tables[0];
                int rowcount = dt.Rows.Count;
                if (rowcount > 0)
                {

                    string SMS_API = dt.Rows[0]["SMS_API"].ToString();
                    string Sender_Id = dt.Rows[0]["Sender_Id"].ToString();
                    string SMS_Route = dt.Rows[0]["SMS_Route"].ToString();
                    string full_url = dt.Rows[0]["full_url"].ToString();
                    string url = dt.Rows[0]["url"].ToString();
                    string originalsms = message;

                    message = Uri.EscapeDataString(message);
                    if (mobile != "")
                    {
                        if (is_sendto_admin == 1)
                            mobile = mobile + "," + dt.Rows[0]["Mobile_no"].ToString();
                        string _url = "";

                        if (HMS.url == "http://mysms.msgclub.net")
                        {
                            _url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + HMS.sms_api + "&message=" + message + "&senderId=" + HMS.sender_id + "&routeId=" + HMS.sms_route + "&mobileNos=" + mobile + "&smsContentType=" + content_type;
                        }
                        else if (HMS.url == "custom")
                        {
                            _url = String.Format(HMS.full_url, messageid, Variables, mobile);
                        }
                        else
                        {
                            _url = HMS.url + "/smsapi.aspx?type=send_sms&client_id=" + HMS.sms_api + "&message=" + message + "&mobile=" + mobile;
                        }
                        //ServicePointManager.Expect100Continue = true;
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        send_message_details_in_Message_send_details(mobile, originalsms, "SEND", mobile, _url, messageto);
                        Message_sending.mesnd_message_to_whatsapp(message, mobile);
                        tureturn = true;
                    }
                }
                return tureturn;
            }
            catch (Exception ex)
            {
                HMS.submitexception("Exception from Message =" + ex.ToString());
                return false;
            }
        }

        internal static void mesnd_message_to_whatsapp(string message, string mobile_no)
        {
            try
            {
                //"http://api4ws.com/sendMessage.php?AUTH_KEY=";

                string _url = HMS.Whatsapp_api_url + HMS.whatsapp_mobile_no + "&message=" + message + "&phone=91" + mobile_no;
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                string results = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                HMS.submitexception("Exception from Whatsapp Message =" + ex.ToString());
            }
        }

        internal static String send_whatsapp_message(string phoneNumber, string template_id, List<object> body_parameter)
        {
            var mbody = new
            {
                countryCode = "+91",
                phoneNumber = phoneNumber,
                callbackData = "success",
                type = "Template",
                template = new
                {
                    name = template_id,
                    languageCode = "en",
                    bodyValues = body_parameter
                    //new List<object>
                    //{
                    //    "Rajesh"
                    //}
                },
            };
            var serializer = new JavaScriptSerializer();
            var json_data = serializer.Serialize(mbody);
            string API_KEY = "SGhoNTN4QXhScS1Rdy03amlFV2RKdWFqdWdNM0lBM3J0VnVjdjVpUlFRTTo=";
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://api.interakt.ai/v1/public/message/");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: Basic {0}", API_KEY));
            Byte[] byteArray = Encoding.UTF8.GetBytes(json_data);
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
            return sResponseFromServer;
        }


    }
    public static class Replace_string_containt
    {
        public static string Inject_data(string formatString, object injectionObject)
        {
            Hashtable values = GetPropertyHash(injectionObject);

            string result = Inject_data2(formatString, values);
            return result;
        }

        private static string Inject_data2(string formatString, Hashtable attributes)
        {
            string result = formatString;
            if (attributes == null || formatString == null)
                return result;

            foreach (string attributeKey in attributes.Keys)
            {
                result = InjectSingleValue(result, attributeKey, attributes[attributeKey]);
            }
            return result;
        }

        private static Hashtable GetPropertyHash(object properties)
        {
            Hashtable values = null;
            if (properties != null)
            {
                values = new Hashtable();
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(properties);
                foreach (PropertyDescriptor prop in props)
                {
                    values.Add(prop.Name, prop.GetValue(properties));
                }
            }
            return values;
        }
        public static string InjectSingleValue(this string formatString, string key, object replacementValue)
        {
            string result = formatString;
            //regex replacement of key with value, where the generic key format is:
            //Regex foo = new Regex("{(foo)(?:}|(?::(.[^}]*)}))");
            Regex attributeRegex = new Regex("{(" + key + ")(?:}|(?::(.[^}]*)}))");  //for key = foo, matches {foo} and {foo:SomeFormat}

            //loop through matches, since each key may be used more than once (and with a different format string)
            foreach (Match m in attributeRegex.Matches(formatString))
            {
                string replacement = m.ToString();
                if (m.Groups[2].Length > 0) //matched {foo:SomeFormat}
                {
                    //do a double string.Format - first to build the proper format string, and then to format the replacement value
                    string attributeFormatString = string.Format(CultureInfo.InvariantCulture, "{{0:{0}}}", m.Groups[2]);
                    replacement = string.Format(CultureInfo.CurrentCulture, attributeFormatString, replacementValue);
                }
                else //matched {foo}
                {
                    replacement = (replacementValue ?? string.Empty).ToString();
                }
                //perform replacements, one match at a time
                result = result.Replace(m.ToString(), replacement);  //attributeRegex.Replace(result, replacement, 1);
            }
            return result;

        }


        //public static string Inject_data1(string formatString, object injectionObject)
        //{
        //    Hashtable values = GetPropertyHash1(injectionObject);
        //    string result = Inject_data3(values);
        //    return result;
        //}
        //private static string Inject_data3(Hashtable attributes)
        //{
        //    string result = "";
        //    if (attributes == null)
        //        return result;

        //    foreach (string attributeKey in attributes.Keys)
        //    {
        //        result = InjectSingleValue1(result, attributeKey, attributes[attributeKey]);
        //    }
        //    return result;
        //}
        //private static Hashtable GetPropertyHash1(object properties)
        //{
        //    Hashtable values = null;
        //    if (properties != null)
        //    {
        //        values = new Hashtable();
        //        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(properties);
        //        foreach (PropertyDescriptor prop in props)
        //        {
        //            if (prop.GetValue(properties) != null)
        //                values.Add(prop.Name, prop.GetValue(properties));
        //        }
        //    }
        //    return values;
        //}
        //public static string InjectSingleValue1(this string formatString, string key, object replacementValue)
        //{
        //    string result = formatString;
        //    if (result == "")
        //    {
        //        if (replacementValue != null)
        //        {
        //            result = (replacementValue ?? string.Empty).ToString();
        //        }
        //    }
        //    else
        //    {
        //        if (replacementValue != null)
        //        {
        //            result = result + "|" + (replacementValue ?? string.Empty).ToString();
        //        }

        //    }

        //    return result;

        //}

    }

    public class Store_procedure_code
    {
        /// <summary>
        /// loads states of the given country
        /// </summary>
        public static void LoadStates(DropDownList ddlState)
        {
            try
            {
                using (SqlConnection scon = new SqlConnection(HMS.conn))
                {
                    SqlCommand cmd = new SqlCommand("[PR_LOAD_STATES]", scon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddlState.DataTextField = "STATE";
                    ddlState.DataValueField = "STATE";
                    ddlState.DataSource = dr;
                    ddlState.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// binds date , month and year dropdown 
        /// </summary>
        /// <param name="dateDropDown"></param>
        public static void BindDateDropdown(DropDownList dateDropDown)
        {
            try
            {
                int minDatePart = 0;
                int maxDatePart = 0;

                switch (dateDropDown.ID.Substring(3).ToLower())
                {
                    case "date":
                        minDatePart = 1;
                        maxDatePart = 31;
                        break;
                    case "month":
                        minDatePart = 1;
                        maxDatePart = 12;
                        break;
                    case "year":
                        minDatePart = 1900;
                        maxDatePart = 2099;
                        break;
                    default:
                        break;
                }

                List<int> listOfDateParts = new List<int>();

                for (int datePart = minDatePart; datePart <= maxDatePart; datePart++)
                {
                    listOfDateParts.Add(datePart);
                }

                dateDropDown.DataSource = listOfDateParts;
                dateDropDown.DataBind();
                if (dateDropDown.ID.Substring(3).ToLower() == "year")
                {
                    dateDropDown.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// loads districts of the given state and bind it to the district dropdown list 
        /// </summary>
        public static void LoadDistrictsForState(DropDownList ddlDistrict, string state)
        {
            try
            {
                using (SqlConnection scon = new SqlConnection(HMS.conn))
                {
                    SqlCommand cmd = new SqlCommand("PR_LOAD_DISTRICTS", scon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STATE", state);
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddlDistrict.DataTextField = "DISTRICT";
                    ddlDistrict.DataValueField = "DISTRICT";
                    ddlDistrict.DataSource = dr;
                    ddlDistrict.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// executes sql query and return table of data
        /// </summary>
        public static DataTable executeReaderQuery(string query)
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection scon = new SqlConnection(HMS.conn))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = scon;
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    scon.Open();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }
            return dt;
        }


        /// <summary>
        /// executes sql query and return table of data
        /// </summary>
        public static DataTable executeReaderQuery(SqlCommand cmd)
        {
            DataTable dt = null;

            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                try
                {
                    cmd.Connection = scon;
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    scon.Open();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    string exception = ex.Message;
                }
                finally
                {
                    scon.Close();
                }
            }

            return dt;
        }
        /// <summary>
        /// executes sql stored procedure and return datatable i.e. table of data
        /// </summary>
        public static DataTable executeReaderProcedure(SqlCommand cmd)
        {
            DataTable dt = null;

            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {

                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = scon;
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    scon.Open();
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                        dt = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    string exception = ex.Message;
                }
                finally
                {
                    scon.Close();
                }
            }

            return dt;
        }

        /// <summary>
        /// executes sql stored procedure and return dataset i.e. multiple table of data
        /// </summary>
        public static DataSet executeReaderDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();

            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                scon.Open();
                da.Fill(ds);
                scon.Close();
            }

            return ds;
        }

        /// <summary>
        /// executes insert or update stored procedure and returns the number of rows affected
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static int executeNonQuery(SqlCommand cmd)
        {
            int rowsAffected = 0;

            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                scon.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                scon.Close();
            }

            return rowsAffected;
        }

        public static string executeScalar(SqlCommand cmd)
        {
            string result = "";
            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                scon.Open();
                result = Convert.ToString(cmd.ExecuteScalar());
                scon.Close();
            }

            return result;
        }

        /// <summary>
        /// executes sql query and return table of data
        /// </summary>
        public static DataSet executeReaderQueryDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection scon = new SqlConnection(HMS.conn))
                {
                    cmd.Connection = scon;
                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    scon.Open();
                    da.Fill(ds);
                    scon.Close();
                }
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }
            return ds;
        }
        /// <summary>
        /// bind_group
        /// </summary>
        public static void bind_group(DropDownList ddl, string procedure)
        {
            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(procedure, scon);
                    cmd.Parameters.AddWithValue("@sp_status ", "DDL");
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataTextField = "DataTextField";
                    ddl.DataValueField = "DataValueField";
                    ddl.DataSource = dr;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Primary", "0"));
                    scon.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scon.Close();
                }
            }
        }

        /// <summary>
        /// loads states of the given country
        /// </summary>
        public static void bind_dropdownlist(DropDownList ddl, string procedure)
        {
            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(procedure, scon);
                    cmd.Parameters.AddWithValue("@sp_status ", "DDL");
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataTextField = "DataTextField";
                    ddl.DataValueField = "DataValueField";
                    ddl.DataSource = dr;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", "0"));
                    scon.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scon.Close();
                }
            }
        }

        /// <summary>
        /// loads states of the given country
        /// </summary>
        public static void bind_dropdownlist_withall(DropDownList ddl, string procedure)
        {
            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(procedure, scon);
                    cmd.Parameters.AddWithValue("@sp_status ", "DDL");
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataTextField = "DataTextField";
                    ddl.DataValueField = "DataValueField";
                    ddl.DataSource = dr;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("All", "0"));
                    scon.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scon.Close();
                }
            }
        }

        /// <summary>
        /// loads states of the given country
        /// </summary>
        public static void bind_dropdownlist(DropDownList ddl, string procedure, string sp_status)
        {
            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(procedure, scon);
                    cmd.Parameters.AddWithValue("@sp_status ", sp_status);
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataTextField = "DataTextField";
                    ddl.DataValueField = "DataValueField";
                    ddl.DataSource = dr;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", "0"));
                    scon.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scon.Close();
                }
            }
        }
        /// <summary>
        /// loads states of the given country
        /// </summary>
        public static void bind_dropdownlist(DropDownList ddl, SqlCommand cmd)
        {
            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                try
                {
                    cmd.Connection = scon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataTextField = "DataTextField";
                    ddl.DataValueField = "DataValueField";
                    ddl.DataSource = dr;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", "0"));
                    scon.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scon.Close();
                }
            }
        }

    }
    public class UsesCode
    {

        public string get_firstyear(string session)
        {

            // Split authors separated by a comma followed by space  
            string[] stringSeparators = new string[] { "-" };
            string[] arr = session.Split(stringSeparators, StringSplitOptions.None);

            string second = arr[0];
            return second;
        }

        string sMonth = DateTime.Now.ToString("MM");
        public string getmonthval()
        {




            return DateTime.Now.ToString("MMM");

        }


        public string getdayname(string date)
        {

            DateTime d1 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            return d1.ToString("dddd");

        }
        public string ConvertStringTomonth(string DateInString) //Format ::  MM  
        {

            return DateInString.Substring(3, 2);
        }
        public string ConvertStringToday(string DateInString) //Format ::  DD  
        {

            return DateInString.Substring(0, 2);
        }
        public string ConvertStringToyear(string DateInString) //Format ::  Year  
        {

            return DateInString.Substring(6, 4);
        }


        public string time()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HH:mm:ss tt");
        }
        public DateTime datetime()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30);
        }
        public string itime()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HHmmss");
        }
        public string date()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
        }
        public string idate()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
        }
        public string geturl()
        {
            string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
            string[] New_originalPath1 = originalPath2.Split('?');
            return New_originalPath1[0].ToString();
        }
        public string ConvertStringToiDate(string DateInString) //Format :: dd/MM/yyyy
        {
            DateInString = DateInString.Substring(6, 4) + DateInString.Substring(3, 2) + DateInString.Substring(0, 2);
            return DateInString;
        }

        public int ConvertStringToiDateint(string DateInString) //Format :: dd/MM/yyyy
        {
            DateInString = DateInString.Substring(6, 4) + DateInString.Substring(3, 2) + DateInString.Substring(0, 2);
            return Convert.ToInt32(DateInString);
        }


        public string iMonthBackdate()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddMonths(-1).ToString("yyyyMMdd");
        }
        public string daysback15()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(-30).ToString("dd/MM/yyyy");
        }
        public string sevendaysback()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(-7).ToString("dd/MM/yyyy");
        }

        public string getdate1()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MMM/yyyy hh:mm:ss tt");

        }
        public DateTime getdate2(string date)
        {
            //return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MMM/yyyy hh:mm:ss tt");
            string mergeStartTime = date + " " + time();
            DateTime startTime = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);
            return startTime;
        }


        public DateTime getdate(string date)
        {

            DateTime d1 = DateTime.ParseExact(date, "dd/MMM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);


            return d1;

        }

        public void GrdData(DataTable sourcetable, GridView gridContainer)
        {
            try
            {
                if (sourcetable.Rows.Count > 0)
                {
                    gridContainer.DataSource = sourcetable;
                    gridContainer.DataBind();
                }
                else
                {
                    gridContainer.EmptyDataText = "No records found.";
                    gridContainer.DataSource = null;
                    gridContainer.DataBind();
                }

            }
            catch (Exception ex)
            {
            }


        }
        public void executequery(string query)
        {


            SqlCommand cmd;
            cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(HMS.conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();

            con.Close();
            con.Dispose();


        }

        public static void exeSql(string query)
        {

            SqlConnection conn = new SqlConnection(HMS.conn);
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);

        }
        public static string find_otp()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random(DateTime.Now.Millisecond);

            int tempo = random.Next(100000, 999999);
            return tempo.ToString();
        }
        public string Right(string text, int length)
        {
            string result = text.Substring(text.Length - length, length);
            return result;
        }

        public string GetDefaultImage(string Gender)
        {
            string imagePath = "";
            if (Gender == "Male") { imagePath = "/images/male.png"; }
            else { imagePath = "/images/female.png"; }
            return imagePath;
        }

        public string AlphaNumericPaswd(int KeyLength, bool IsAlphaNumeric)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            if (IsAlphaNumeric)
            {
                characters += alphabets + small_alphabets + numbers;
            }
            int length = KeyLength;
            string key = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (key.IndexOf(character) != -1);
                key += character;
            }
            return key;
        }
        public void Save(string message)
        {
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";

        }
        public string GenerateRandomNumber(int start, int end)
        {  //10000000  99999999
            Random random = new Random();
            int temp = random.Next(start, end);
            return temp.ToString();
        }
        public string Auto_generate_user_id(string query, int i, int j)
        {
            string user_id = "";
            bool duplicateid = false;
            Random rn = new Random();
            do
            {
                int k = rn.Next(i, j);
                user_id = k.ToString();
                duplicateid = check_duplicate_regid(query + user_id);

                if (duplicateid == true)
                {

                }
            }
            while (duplicateid == false);

            return user_id;
        }


        public static bool check_duplicate_regid(string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public static void submitexception(string ex)
        {

            SqlCommand cmd;
            string strQuery = @"INSERT INTO Exception_details (ExceptionMessage,Date,Idate,Time) values (@ExceptionMessage,@Date,@Idate,@Time)";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@ExceptionMessage", ex);
            cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow.AddHours(5).AddMinutes(30));
            cmd.Parameters.AddWithValue("@Idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss"));

            if (InsertUpdate.InsertUpdateData(cmd))
            {

            }
        }


        public static void submitexception1(Exception ex)
        {
            SqlCommand cmd;
            string strQuery = @"INSERT INTO Exception_details (ExceptionMessage,Date,Idate,Time) values (@ExceptionMessage,@Date,@Idate,@Time)";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@ExceptionMessage", ex);
            cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow.AddHours(5).AddMinutes(30));
            cmd.Parameters.AddWithValue("@Idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss"));

            if (InsertUpdate.InsertUpdateData(cmd))
            {

            }

        }
        public void bind_all_List_with_id(ListBox ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Select";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public void bind_all_ddl_with_id(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Select";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }

        public void bind_all_list_with_id(ListBox lst, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                lst.DataTextField = dt.Columns[0].ToString();
                lst.DataValueField = dt.Columns[1].ToString();
            }

            lst.DataSource = dt;
            lst.DataBind();
        }
        public void bind_all_ddl_with_all(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "ALL";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("ALL", "0"));
        }



        public void bind_ddl(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("Select");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }



        public void bind_ddl_month(DropDownList ddl)
        {
            for (int month = 1; month <= 12; month++)
            {
                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                ddl.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
            }
        }


        public void bind_txt(TextBox txt, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                txt.Text = "";
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    txt.Text = "";
                }
                else
                {
                    txt.Text = dt.Rows[0][0].ToString();
                }
            }

        }
        public static double converttodouble(string p)
        {
            try
            {
                return Convert.ToDouble(p);
            }
            catch
            {
                return 0;
            }
        }
        public bool ValidateNumber(string number)
        {
            try
            {
                double _num = Convert.ToDouble(number.Trim());
            }
            catch
            {
                return false;
            }
            return true;
        }


        public int ValidateNumberint(string number)
        {
            int _num = 0;
            try
            {
                _num = Convert.ToInt32(number.Trim());
            }
            catch
            {

            }
            return _num;
        }



        public void BindRepeater(string sql, Repeater rptr)
        {
            DataTable dt = FillTable(sql);
            if (dt.Rows.Count != 0)
            {
                rptr.DataSource = dt;
                rptr.DataBind();
            }
            else
            {
                rptr.DataSource = null;
                rptr.DataBind();
            }
        }

        public DataTable binddatatable(string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = InsertUpdate.GetData(cmd);
            return dt;
        }

        public void bind_gridview(GridView gridview, string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                //gridview.Visible = false;
                gridview.DataSource = null;
                gridview.EmptyDataText = "Data Not Available";
                gridview.DataBind();
            }
            else
            {
                //gridview.Visible = true;
                gridview.DataSource = dt;
                gridview.DataBind();
            }
        }




        public void bind_Datalist(DataList dl_list, string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                dl_list.DataSource = null;
                dl_list.DataBind();
            }
            else
            {
                dl_list.DataSource = dt;
                dl_list.DataBind();
            }
        }
        public string FindNameWithQuery(string Query, string requestData)
        {
            string Name = "";
            SqlCommand cmd = new SqlCommand(Query + requestData + "'");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                Name = " ";
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    Name = " ";
                }
                else
                {
                    Name = dt.Rows[0][0].ToString();
                }
            }
            return Name;
        }
        public string[] GetUploadList(string filesName)
        {
            string[] files = Directory.GetFiles(filesName);
            string[] fileNames = new string[files.Length];
            Array.Sort(files);

            for (int i = 0; i < files.Length; i++)
            {
                fileNames[i] = Path.GetFileName(files[i]);
            }

            return fileNames;
        }

        public string UploadImage(FileUpload fileName, string FolderPath)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HHmmss");
            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fileName.HasFile)
            {
                if (fileName.FileBytes.Length < 1000000)
                {
                    string FileExtension = Path.GetExtension(fileName.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {
                        string ServerPath = HttpContext.Current.Server.MapPath("~/" + FolderPath);
                        if (File.Exists(ServerPath))
                        {
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(ServerPath);

                        }
                        fileName.SaveAs(ServerPath + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    ImagePath = FolderPath + Path.GetFileName(rename);
                }
            }
            return ImagePath;
        }
        // user code creation
        public string code_creation()
        {
            string code = "";
            Random rn = new Random();
            int i = 10000000;
            int j = 99999999;

            int k = rn.Next(i, j);
            code = k.ToString();


            return code;
        }
        public string UploadAudio(FileUpload fileName, string FolderPath)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fileName.HasFile)
            {
                if (fileName.FileBytes.Length < 6000000)
                {
                    string FileExtension = Path.GetExtension(fileName.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".mp3", ".avi", ".mp4", ".wmv" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {
                        string ServerPath = HttpContext.Current.Server.MapPath("~/" + FolderPath);
                        fileName.SaveAs(ServerPath + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    ImagePath = FolderPath + Path.GetFileName(rename);
                }
            }
            return ImagePath;
        }

        public string UploadPDF(FileUpload fileName, string FolderPath)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fileName.HasFile)
            {
                if (fileName.FileBytes.Length < 10000000)
                {
                    string FileExtension = Path.GetExtension(fileName.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".pdf", ".PDF", ".doc", ".docx", ".ppt" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {
                        string ServerPath = HttpContext.Current.Server.MapPath("~/" + FolderPath);
                        fileName.SaveAs(ServerPath + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    ImagePath = FolderPath + Path.GetFileName(rename);
                }
            }
            return ImagePath;
        }

        public string Upload_doc_images(FileUpload fileName, string FolderPath)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fileName.HasFile)
            {
                if (fileName.FileBytes.Length < 5000000)
                {
                    string FileExtension = Path.GetExtension(fileName.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".pdf", ".PDF", ".doc", ".docx", ".ppt", ".jpg", ".jpeg", ".png", ".gif" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {
                        string ServerPath = HttpContext.Current.Server.MapPath("~/" + FolderPath);
                        if (File.Exists(ServerPath))
                        {
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(ServerPath);

                        }
                        fileName.SaveAs(ServerPath + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                    string[] New_originalPath1 = originalPath2.Split('?');
                    string originalPath1 = New_originalPath1[0].ToString();

                    ImagePath = originalPath1 + FolderPath + Path.GetFileName(rename);
                }
            }
            return ImagePath;
        }

        public void FileSave(FileUpload fl, string path)
        {
            if (fl.HasFile)
            {
                if (fl.FileBytes.Length < 1000000)
                {
                    string theFileName = Path.Combine(path, fl.FileName);
                    if (File.Exists(theFileName))
                    {
                        File.Delete(theFileName);
                    }
                    fl.SaveAs(theFileName);
                }
                else
                {
                }
            }
        }

        public string Auto_sl_id(string Query)
        {
            SqlCommand cmd = new SqlCommand(Query);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return "10001";
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    return "10001";
                }
                else
                {
                    return (Convert.ToInt32(dt.Rows[0][0].ToString()) + 1).ToString();
                }
            }
        }

        public string sl_id(string Query)
        {
            SqlCommand cmd = new SqlCommand(Query);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return "00001";

            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    return "00001";
                }
                else
                {
                    string a = Right(dt.Rows[0][0].ToString(), 5);
                    int rno = int.Parse(a) + 1;
                    return rno.ToString("00000");
                }
            }
        }
        public string Filesave(FileUpload fl, string path, string folder_name)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
            string dbfilepath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fl.HasFile)
            {
                if (fl.FileBytes.Length < 1000000)
                {
                    string FileExtension = Path.GetExtension(fl.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {

                        fl.SaveAs(path + "/" + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    string fileName = Path.GetFileName(rename);
                    dbfilepath = folder_name + "/" + fileName;
                }
            }
            return dbfilepath;
        }


        public bool IsExist(string Query)
        {
            SqlCommand cmd = new SqlCommand(Query);
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable FillTable(string sqlQuery)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection con = new SqlConnection(HMS.conn);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            SqlDataAdapter ad = new SqlDataAdapter(sqlQuery, con);
            ad.Fill(dtTemp);
            if (con.State == ConnectionState.Open) { con.Close(); }
            return dtTemp;
        }


        public bool login_status(string Query)
        {
            SqlCommand cmd = new SqlCommand(Query);
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable GetDatastore(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = HMS.conn;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        public void sendemail(string email, string subject, string message)
        {

            try
            {

                MailMessage Msg = new MailMessage();
                MailAddress fromMail = new MailAddress("noreply@rcsp.in");
                // Sender e-mail address.
                Msg.From = fromMail;

                // Recipient e-mail address.
                Msg.To.Add(new MailAddress(email));
                Msg.To.Add(new MailAddress("info@rcsp.in"));
                // Subject of e-mail
                Msg.Subject = subject;
                Msg.Body = message.ToString();
                Msg.IsBodyHtml = true;
                Msg.AlternateViews.Add(Mail_Body(message));
                SmtpClient mailClient = new SmtpClient("relay-hosting.secureserver.net", 25); // for online
                // SmtpClient mailClient = new SmtpClient("smtpout.secureserver.net", 25); // for offline

                // Change your gmail user id and password for send email
                NetworkCredential NetCrd = new NetworkCredential("integerpatna@gmail.com", "Ints@2017");
                mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = NetCrd;
                mailClient.EnableSsl = false;
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.Send(Msg);

            }
            catch (Exception exc)
            {
                InsertUpdate.submit_exception(exc, "Send Mail");

            }
        }

        public void sendemailwithattachment(string email, string subject, string msg, FileUpload f)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(email);// Email-ID of Receiver  
                message.Subject = subject;// Subject of Email  
                message.From = new System.Net.Mail.MailAddress("noreply@rcsp.in");// Email-ID of Sender  
                message.IsBodyHtml = true;
                message.Attachments.Add(new Attachment(f.FileContent, System.IO.Path.GetFileName(f.FileName)));
                SmtpClient SmtpMail = new SmtpClient();
                SmtpMail.Host = "relay-hosting.secureserver.net";//name or IP-Address of Host used for SMTP transactions  
                SmtpMail.Port = 25;//Port for sending the mail  
                SmtpMail.Credentials = new System.Net.NetworkCredential("integerpatna@gmail.com", "Ints@2017");//username/password of network, if apply  
                SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpMail.EnableSsl = false;
                SmtpMail.ServicePoint.MaxIdleTime = 0;
                SmtpMail.ServicePoint.SetTcpKeepAlive(true, 2000, 2000);
                message.BodyEncoding = Encoding.Default;
                message.Priority = MailPriority.High;
                SmtpMail.Send(message); //Smtpclient to send the mail message  
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        private AlternateView Mail_Body(string message)
        {
            string path = HttpContext.Current.Server.MapPath(@"Images/best.jpg");
            LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
            Img.ContentId = "MyImage";
            string str = @"  
            <table>  
                <tr>  
                    <td> '" + message + @"'  
                    </td>  
                </tr>  
                <tr>  
                    <td>  
                      <img src=cid:MyImage  id='img' alt='' width='810px' height='450px'/>   
                    </td>  
                </tr>
<tr><b>Regards</b></tr>
<tr> Rashtriya Computer Shiksha Pariyojna </tr>
<tr> Support Team </tr>
<tr>Chanda Complex Mirjanhat Bhagalpur</tr>
<tr> <b>Speak to us :- </b>+91 9031173621, +91 912277801, +91 7050668808 </tr>
</table>  
            ";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            AV.LinkedResources.Add(Img);
            return AV;
        }


        public string Find_Name(string Query)
        {
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand(Query);
                DataTable dt = InsertUpdate.GetData(cmd);
                if (dt.Rows.Count == 0)
                {
                    return "0";
                }
                else
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch
            {
                return "0";
            }

        }

        public void BindChecklist(CheckBoxList chkList, string Text, string Value, string ColumnName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select distinct " + Text + ", " + Value + " from " + ColumnName + " order by " + Text + "");
                DataTable dt = InsertUpdate.GetData(cmd);
                if (dt.Rows.Count != 0)
                {
                    chkList.DataValueField = "" + Value + "";
                    chkList.DataTextField = "" + Text + "";
                    chkList.DataSource = dt;
                    chkList.DataBind();
                }
            }
            catch (Exception ex)
            { }
        }

        public string auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from Global ", HMS.conn);
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

                    exeSql("Alter table Global add " + column + " varchar(50)");
                    result = auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }
        public static string SendNotification(string deviceId, Dictionary<String, String> data)
        {
            Dictionary<string, object> dc1 = get_pushsenderid();
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
            String Idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt");



            SqlCommand cmd = new SqlCommand(" INSERT INTO PushNotification_Details (notification_id,message,title,messagetype,User_Id,Sender_Id,Idate,Date,Time,ResponseFromServer) values (@notification_id,@message,@title,@messagetype,@User_Id,@Sender_Id,@Idate,@Date,@Time,@ResponseFromServer)");

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
            if (InsertUpdate.InsertUpdateData(cmd))
            {
            }
            //SqlDataAdapter ad1 = new SqlDataAdapter("select * from PushNotification_Details", HMS.conn);
            //DataSet ds1 = new DataSet();
            //ad1.Fill(ds1);
            //DataTable dt = ds1.Tables[0];
            //DataRow dr1 = dt.NewRow();
            //dr1["notification_id"] = data["notification_id"];
            //dr1["message"] = data["message"];
            //dr1["title"] = data["title"];
            //dr1["messagetype"] = data["messagetype"];
            //dr1["User_Id"] = data["UserId"];
            //dr1["Idate"] = Idate;
            //dr1["Date"] = date;
            //dr1["ResponseFromServer"] = sResponseFromServer;
            //dr1["Time"] = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt");
            //dt.Rows.Add(dr1);
            //SqlCommandBuilder cb = new SqlCommandBuilder(ad1);
            //ad1.Update(dt);
            return sResponseFromServer;
        }
        public static Dictionary<string, object> get_pushsenderid()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from Comapny_Profile  ";
            SqlCommand cmd;

            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);

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


        public void pushnotification(string Message, string Title, string FirebaseId, string ordrid, string userid, string messagetype)
        {
            string sendnotification = SendNotification1(FirebaseId, Message, Title, ordrid, userid, messagetype);
        }
        public static string SendNotification1(string deviceId, string msg, string tit, string bookingid, string userid, string messagetype)
        {
            Dictionary<string, object> dc1 = get_pushsenderid();
            string apikey = (String)dc1["SERVER_API_KEY"];
            string senderid = (String)dc1["SENDER_ID"];

            string notificationid = Guid.NewGuid().ToString();
            string msg1 = msg;
            //string message = Uri.EscapeDataString(msg);
            //string t = Uri.EscapeDataString(tit);
            string message = msg;
            string t = tit;

            //  string notification_id;
            var ddata = new
            {
                to = deviceId,
                data = new
                {
                    notification_id = notificationid,
                    message = message,
                    title = t,
                    tickerText = bookingid,
                    messagetype = messagetype
                }
            };
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(ddata);

            string SERVER_API_KEY = apikey;
            var SENDER_ID = senderid;
            var value = message;
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
            save_push_mesge(sResponseFromServer, deviceId, msg1, userid, messagetype, tit, bookingid, notificationid);
            return sResponseFromServer;


        }

        private static void save_push_mesge(string sResponseFromServer, string deviceId, string message, string userid, string messagetype, string title, string bookingid, string notification_id)
        {
            try
            {
                string date = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt");
                SqlCommand cmd;
                string strQuery = "INSERT INTO  PushNotification_Details (notification_id,message,title,messagetype,User_Id,Sender_Id,Idate,Date,ResponseFromServer,Time) values (@notification_id,@message,@title,@messagetype,@User_Id,@Sender_Id,@Idate,@Date,@ResponseFromServer,@Time)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@notification_id", notification_id);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@messagetype", messagetype);
                cmd.Parameters.AddWithValue("@User_Id", userid);
                cmd.Parameters.AddWithValue("@Sender_Id", bookingid);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Idate", idate);
                cmd.Parameters.AddWithValue("@ResponseFromServer", sResponseFromServer);
                cmd.Parameters.AddWithValue("@Time", time);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                }
            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }



        public static DataTable Getdata_sp(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                if (scon.State == ConnectionState.Closed)
                {
                    scon.Open();
                }
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
                if (scon.State == ConnectionState.Open)
                {
                    scon.Close();
                    scon.Dispose();
                }
                return dt;
            }
        }

        public void bind_all_ddl_with_Allid(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Select";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("All", "0"));
        }
        public static string tampid()
        {
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
            Random rn = new Random(DateTime.Now.Millisecond);
            int i = 0;
            int j = 1000;
            int k = rn.Next(i, j);
            return k.ToString() + idate + time;

        }
        public string Auto_generate_topic(string query, int i, int j)
        {
            string user_id = "";
            bool duplicateid = false;
            Random rn = new Random();
            do
            {
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
                int k = rn.Next(i, j);
                user_id = k.ToString() + idate + time;
                duplicateid = check_duplicate_regid(query + user_id);

                if (duplicateid == true)
                {

                }
            }
            while (duplicateid == false);

            return user_id;
        }
        public static string tempid()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random(DateTime.Now.Millisecond);

            int tempo = random.Next(100000, 999999);
            return tempo.ToString();
        }
        public static DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection con = new SqlConnection(HMS.conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                con.Close();
                con.Dispose();
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        public static string create_admission_id()
        {

            string result = "";
            SqlDataAdapter ad = new SqlDataAdapter("select * from Global", HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Globle_Master");
            DataTable dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["Online_regid"] = 1;
                result = "1";
                dt.Rows.Add(dr);

            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Online_regid"].ToString() == "")
                    {
                        dr["Online_regid"] = 1;
                        result = "1";
                    }
                    else
                    {
                        dr["Online_regid"] = Convert.ToDouble(dr["Online_regid"]) + 1;
                        result = dr["Online_regid"].ToString();
                    }
                    break;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(ds.Tables[0]);
            return "HAJ" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd") + result;
        }

        public static Boolean InsertUpdateData_sp(SqlCommand cmd)
        {
            using (SqlConnection scon = new SqlConnection(HMS.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                if (scon.State == ConnectionState.Closed)
                {
                    scon.Open();
                }
                int rowsAffected = cmd.ExecuteNonQuery();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Close();
                    scon.Dispose();
                }
                if (rowsAffected > 0)
                    return true;
                else
                    return false;
            }
        }


        //public static string cononlinetest = connection.onlinetest;

        //public static DataTable GetDataonlinedb(SqlCommand cmd)
        //{
        //    DataTable dt = new DataTable();
        //    SqlConnection con = new SqlConnection(cononlinetest);
        //    SqlDataAdapter sda = new SqlDataAdapter();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = con;
        //    con.Open();
        //    sda.SelectCommand = cmd;
        //    sda.Fill(dt);
        //    con.Close();
        //    con.Dispose();
        //    return dt;
        //}



        internal string password1()
        {

            string pwd = "";


            bool duplicateid = false;
            Random rn = new Random();
            int i = 10000;
            int j = 99999;
            do
            {
                int k = rn.Next(i, j);

                pwd = k.ToString();
                duplicateid = check_dauplicate_id(pwd);

                if (duplicateid == true)
                {

                }
            } while (duplicateid == false);

            return pwd;


        }

        private bool check_dauplicate_id(string pwd)
        {
            DataTable dt = FillTable("Select  Password  from admission_registor where Password='" + pwd + "' ");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }


        public int get_slid_max()
        {

            string coon = HMS.conn;
            SqlDataAdapter ad = new SqlDataAdapter(" Select MAX(sl_no) from Zoom_API where  (sl_no is not null  or sl_no!='')", coon);
            DataSet ds = new DataSet();
            ad.Fill(ds, "my");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                }
            }
        }
        public void bind_ddl_all(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("All");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }

        public void bind_ddl_all1(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("ALL");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }

        internal string get_class_name(string classid)
        {
            SqlCommand cmd = new SqlCommand("Select Course_Name from Add_course_table where course_id='" + classid + "'");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "ALL";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        public string getmontnumber(string monthname)
        {
            if (monthname == "Jan")
            {
                return "01";
            }
            else if (monthname == "Feb")
            {
                return "02";
            }
            else if (monthname == "Mar")
            {
                return "03";
            }
            else if (monthname == "Apr")
            {
                return "04";
            }
            else if (monthname == "May")
            {
                return "05";
            }
            else if (monthname == "Jun")
            {
                return "06";
            }
            else if (monthname == "Jul")
            {
                return "07";
            }
            else if (monthname == "Aug")
            {
                return "08";
            }
            else if (monthname == "Sep")
            {
                return "09";
            }
            else if (monthname == "Oct")
            {
                return "10";
            }
            else if (monthname == "Nov")
            {
                return "11";
            }
            else if (monthname == "Dec")
            {
                return "12";
            }
            else
            {
                return "0";
            }
        }

        public Dictionary<string, object> getseesion()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string quiry = "Select  top 1 *  from session_details where use_mode='1' order by id desc   ";


            SqlDataAdapter ad = new SqlDataAdapter(quiry, HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "session_details");
            DataTable dt = ds.Tables[0];


            if (dt.Rows.Count == 0)
            {


                dc["Session"] = "NO";
                dc["session_id"] = "NO";

            }
            else
            {

                dc["Session"] = dt.Rows[0]["Session"].ToString(); ;
                dc["session_id"] = dt.Rows[0]["session_id"].ToString();

            }

            return dc;
        }

        internal string get_student_send_password()
        {
            SqlCommand cmd = new SqlCommand("Select Send_student_userid_and_pwd_with_apk_link from Comapny_Profile");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }

        internal string get_teacher_send_password()
        {
            SqlCommand cmd = new SqlCommand("Select Send_teacher_userid_and_pwd_with_apk_link from Comapny_Profile");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }

        internal string get_sectionid(string Section_Name)
        {
            SqlCommand cmd = new SqlCommand("Select Section_Id from Section_Master   where Section_Name='" + Section_Name + "'");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }

        internal string get_sectionname(string setionid)
        {
            SqlCommand cmd = new SqlCommand("Select Section_Name from Section_Master   where Section_Id='" + setionid + "'");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "ALL";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }



        public bool syncdatastatusyes_or_no()
        {
            bool toreturn = false;
            bool chk_allowonlintest = get_onlintrest_enabled();

            if (chk_allowonlintest == true)
            {

                SqlCommand cmd1 = new SqlCommand("Select  top 1 Id from admission_registor where (sync_status is null or sync_status=0) ");
                DataTable dt = InsertUpdate.GetData(cmd1);
                if (dt.Rows.Count == 0)
                {

                    SqlCommand cmd3 = new SqlCommand("Select  top 1 Id from ClassMaster where (sync_status is null or sync_status=0) ");
                    DataTable dt3 = InsertUpdate.GetData(cmd3);
                    if (dt3.Rows.Count == 0)
                    {
                        SqlCommand cmd4 = new SqlCommand("Select  top 1 Id from Notice_Board_Details where Send_Status='Notsend' ");
                        DataTable dt4 = InsertUpdate.GetData(cmd4);
                        if (dt4.Rows.Count == 0)
                        {

                            SqlCommand cmd5 = new SqlCommand("Select  top 1 Id from Private_Messages where Send_Status='Notsend' ");
                            DataTable dt5 = InsertUpdate.GetData(cmd5);
                            if (dt5.Rows.Count == 0)
                            {

                                SqlCommand cmd6 = new SqlCommand("Select  top 1 Id from Private_Messages_For_Teacher where Send_Status='Notsend' ");
                                DataTable dt6 = InsertUpdate.GetData(cmd6);
                                if (dt6.Rows.Count == 0)
                                {
                                    SqlCommand cmd7 = new SqlCommand("Select  top 1 Id from Notice_Board_Details_Teacher where Send_Status='Notsend' ");
                                    DataTable dt7 = InsertUpdate.GetData(cmd7);
                                    if (dt7.Rows.Count == 0)
                                    {
                                        toreturn = true;


                                    }
                                    else
                                    {
                                        toreturn = false;
                                    }
                                }
                                else
                                {
                                    toreturn = false;
                                }

                            }
                            else
                            {
                                toreturn = false;
                            }
                        }
                        else
                        {
                            toreturn = false;
                        }

                    }
                    else
                    {
                        toreturn = false;
                    }


                }
                else
                {
                    toreturn = false;

                }

            }
            else
            {
                SqlCommand cmd4 = new SqlCommand("Select  Id from Notice_Board_Details where Send_Status='Notsend' ");
                DataTable dt4 = InsertUpdate.GetData(cmd4);
                if (dt4.Rows.Count == 0)
                {
                    SqlCommand cmd5 = new SqlCommand("Select  Id from Private_Messages where Send_Status='Notsend' ");
                    DataTable dt5 = InsertUpdate.GetData(cmd5);
                    if (dt5.Rows.Count == 0)
                    {
                        SqlCommand cmd6 = new SqlCommand("Select  top 1 Id from Private_Messages_For_Teacher where Send_Status='Notsend' ");
                        DataTable dt6 = InsertUpdate.GetData(cmd6);
                        if (dt6.Rows.Count == 0)
                        {
                            SqlCommand cmd7 = new SqlCommand("Select  top 1 Id from Notice_Board_Details_Teacher where Send_Status='Notsend' ");
                            DataTable dt7 = InsertUpdate.GetData(cmd7);
                            if (dt7.Rows.Count == 0)
                            {
                                toreturn = true;


                            }
                            else
                            {
                                toreturn = false;
                            }
                        }
                        else
                        {
                            toreturn = false;
                        }

                    }
                    else
                    {
                        toreturn = false;
                    }
                }
                else
                {
                    toreturn = false;
                }
            }


            return toreturn;

        }

        public bool get_onlintrest_enabled()
        {
            SqlCommand cmd1 = new SqlCommand("Select  Enable_Onine_Test from App_Setting  ");
            DataTable dt = InsertUpdate.GetData(cmd1);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    return false;
                }
                else
                {
                    if (Convert.ToBoolean(dt.Rows[0]["Enable_Onine_Test"].ToString()) == true)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public void RptrData(DataTable sourcetable, Repeater rptr)
        {
            try
            {
                if (sourcetable.Rows.Count > 0)
                {
                    rptr.DataSource = sourcetable;
                    rptr.DataBind();
                }
                else
                {

                    rptr.DataSource = null;
                    rptr.DataBind();
                }

            }
            catch (Exception ex)
            {
            }


        }


        public DataSet Fill_Data_set(string query)
        {

            string connectionstring = HMS.conn;
            DataSet dtc = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, connectionstring);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {


            }
            return dtc;
        }


        public Dictionary<string, object> getstudent(string admission_no)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string quiry = "Select  top 1 class,rollnumber,Section,session,studentname,Class_id,Session_id,Academic_Sem_or_Year_id,Academic_Sem_or_Year  from admission_registor where admissionserialnumber=" + admission_no + " order by id desc   ";
            SqlDataAdapter ad = new SqlDataAdapter(quiry, HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "session_details");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {


                dc["class"] = "NO";
                dc["rollnumber"] = "NO";
                dc["Section"] = "NO";
                dc["session"] = "NO";
                dc["studentname"] = "NO";
                dc["Class_id"] = "NO";
                dc["Academic_Sem_or_Year_id"] = "NO";
                dc["Academic_Sem_or_Year"] = "NO";

            }
            else
            {

                dc["class"] = dt.Rows[0]["class"].ToString();
                dc["rollnumber"] = dt.Rows[0]["rollnumber"].ToString();
                dc["Section"] = dt.Rows[0]["Section"].ToString();
                dc["session"] = dt.Rows[0]["session"].ToString();
                dc["studentname"] = dt.Rows[0]["studentname"].ToString();
                dc["Class_id"] = dt.Rows[0]["Class_id"].ToString();
                dc["Session_id"] = dt.Rows[0]["Session_id"].ToString();
                dc["Academic_Sem_or_Year_id"] = dt.Rows[0]["Academic_Sem_or_Year_id"].ToString();
                dc["Academic_Sem_or_Year"] = dt.Rows[0]["Academic_Sem_or_Year"].ToString();

            }
            return dc;
        }

        internal string get_teachername(string teacherid)
        {
            SqlCommand cmd5 = new SqlCommand("Select  name from user_details where user_id='" + teacherid + "' ");
            DataTable dt5 = InsertUpdate.GetData(cmd5);
            if (dt5.Rows.Count == 0)
            {
                return "";

            }
            else
            {
                return dt5.Rows[0]["name"].ToString();
            }
        }

        internal void send_push_to_student_study_material(string Class_id, string Section, string subject, string Topic, string subjectid, string academic_ses_type_name, string sessionid)
        {
            SqlCommand cmd = new SqlCommand(" select ar.studentname,ar.admissionserialnumber,ar.gcm_id from Subject_Mapping_New  smn left join dbo.[admission_registor] ar on smn.Class_id=ar.Class_id  and smn.Section=ar.Section and smn.Session=ar.session and smn.Admission_no=ar.admissionserialnumber where ar.Class_id='" + Class_id + "' and ar.Section='" + Section + "' and Sub_id='" + subjectid + "' and  Status='1' and ar.Session_id='" + sessionid + "' and ar.Academic_Sem_or_Year_id='" + academic_ses_type_name + "'");
            DataTable dt = InsertUpdate.GetData(cmd);

            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string message = "Dear " + dr["studentname"].ToString() + ", study material has been uploaded of the subject: " + subject + " & Topic: " + Topic + ", Please check now study material ";

                    if (dr["gcm_id"] != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = message;
                        ss["title"] = "Study Material";
                        ss["messagetype"] = "StudyMaterial";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = dr["admissionserialnumber"].ToString();
                        UsesCode.SendNotification(dr["gcm_id"].ToString(), ss);
                    }

                }

            }
        }

        internal void send_push_to_student_homework(string Class_id, string Section, string subject, string Topic, string subjectid, string academic_ses_type_name, string sessionid)
        {

            SqlCommand cmd = new SqlCommand(" select ar.studentname,ar.admissionserialnumber,ar.gcm_id from Subject_Mapping_New  smn left join dbo.[admission_registor] ar on smn.Class_id=ar.Class_id  and smn.Section=ar.Section and smn.Session=ar.session and smn.Admission_no=ar.admissionserialnumber where ar.Class_id='" + Class_id + "' and ar.Section='" + Section + "' and Sub_id='" + subjectid + "' and ar.Status='1' and  ar.Session_id='" + sessionid + "' and ar.Academic_Sem_or_Year_id='" + academic_ses_type_name + "' ");
            DataTable dt = InsertUpdate.GetData(cmd);

            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string message = "Dear " + dr["studentname"].ToString() + ", homework assigned of the subject: " + subject + " & Topic: " + Topic;

                    if (dr["gcm_id"] != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = message;
                        ss["title"] = "Homework Assignment";
                        ss["messagetype"] = "Homework";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = dr["admissionserialnumber"].ToString();
                        UsesCode.SendNotification(dr["gcm_id"].ToString(), ss);
                    }

                }

            }
        }

        internal void send_push_to_student_ebook(string Class_id, string Section, string subject, string Topic, string subjectid, string academic_ses_type_name, string sessioid)
        {
            SqlCommand cmd = new SqlCommand(" select ar.studentname,ar.admissionserialnumber,ar.gcm_id from Subject_Mapping_New  smn left join dbo.[admission_registor] ar on smn.Class_id=ar.Class_id  and smn.Section=ar.Section and smn.Session=ar.session and smn.Admission_no=ar.admissionserialnumber where ar.Class_id='" + Class_id + "' and ar.Section='" + Section + "' and Sub_id='" + subjectid + "' and  ar.Session_id='" + sessioid + "' and ar.Academic_Sem_or_Year_id='" + academic_ses_type_name + "' ");
            DataTable dt = InsertUpdate.GetData(cmd);

            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string message = "Dear " + dr["studentname"].ToString() + ", E-Book has been uploaded of the subject: " + subject + " & Topic: " + Topic + ", Please check now E-Book Section.";

                    if (dr["gcm_id"] != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = message;
                        ss["title"] = "E-Book";
                        ss["messagetype"] = "E-Book";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = dr["admissionserialnumber"].ToString();
                        UsesCode.SendNotification(dr["gcm_id"].ToString(), ss);
                    }

                }

            }
        }

        internal string get_class_routine(string session, string classid, string section)
        {
            string querymain = "Select  Day  ";

            //  string query = " Select  *,CONCAT('Period_',Class_period,'__',format(Start_Time, 'hh_mm_ss_tt'), 'TO', format(End_time, 'hh_mm_ss_tt')) as Period   from  Class_Routine_Master   where   Session_id=" + session + " and  Class_id=" + classid + " and  Section='" + section + "' ORDER BY  Class_period";
            string query = " Select  distinct Class_period,CONCAT('Period_',Class_period,'__',format(Start_Time, 'hh_mm_ss_tt'), 'TO', format(End_time, 'hh_mm_ss_tt')) as Period   from  Class_Routine_Master   where   Session_id=" + session + " and  Class_id=" + classid + " and  Section='" + section + "' ORDER BY  Class_period";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string subjectid = getsubjectid(dr["Class_period"].ToString(), session, classid, section);
                    // querymain += ",(Select csm.CourseName from Class_Routine_Master crm join Course_or_Subject_Master csm on crm.Subject_id=csm.CourseID and  crm.Class_id=csm.CategoryID and crm.Section=csm.section and  crm.Session_id=csm.session_id  where   crm.Subject_id='" + dr["Subject_id"] + "'  and crm.Class_id='" + dr["Class_id"] + "' and crm.Section='" + dr["Section"] + "' and crm.Session_id='" + dr["Session_id"] + "' and crm.Class_period='" + dr["Class_period"] + "' and crm.Day=Day_Master.Day   ) " + dr["Period"].ToString();

                    querymain += ",(Select csm.CourseName from Class_Routine_Master crm join Course_or_Subject_Master csm on crm.Subject_id=csm.CourseID and  crm.Class_id=csm.CategoryID and crm.Section=csm.section and  crm.Session_id=csm.session_id  where   crm.Subject_id=" + subjectid + " and  crm.Class_id='" + classid + "' and crm.Section='" + section + "' and crm.Session_id='" + session + "' and crm.Class_period='" + dr["Class_period"] + "' and crm.Day=Day_Master.Day   ) " + dr["Period"].ToString();
                }
            }
            querymain += " from    Day_Master  ";
            return querymain;
        }

        private string getsubjectid(string Class_period, string session, string classid, string section)
        {
            SqlCommand cmd = new SqlCommand("Select  Subject_id from Class_Routine_Master where Session_id='" + session + "' and Class_id='" + classid + "' and Section='" + section + "' and Class_period='" + Class_period + "'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal string get_session_id(string session)
        {
            SqlCommand cmd = new SqlCommand("Select  session_id from session_details where Session='" + session + "'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        internal string get_session_id_use()
        {
            SqlCommand cmd = new SqlCommand("Select  session_id from session_details where use_mode='1' ");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }


        internal string get_session()
        {
            SqlCommand cmd = new SqlCommand("Select  Session from session_details where use_mode='1'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "2020-2021";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal string get_teacher_type(object teacherid)
        {
            SqlCommand cmd = new SqlCommand("Select  CategoryID from Ptm_class_teacher_mapping where UserID='" + teacherid + "'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        public string getclassid(string teacherid)
        {

            string CategoryID = "0";
            SqlCommand cmd1 = new SqlCommand("Select  CategoryID from Ptm_class_teacher_mapping where UserID='" + teacherid + "'");
            DataTable dt = GetData(cmd1);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string CategoryID1 = dt.Rows[i]["CategoryID"].ToString();
                    if (CategoryID1 == "")
                    {
                        CategoryID = "'" + CategoryID1 + "'";
                    }
                    else
                    {
                        CategoryID = CategoryID + "," + "'" + CategoryID1 + "'";

                    }
                }
            }

            return CategoryID;



        }





        internal string get_sessionid()
        {
            SqlCommand cmd = new SqlCommand("Select  session_id from session_details where use_mode='1'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal string getval()
        {
            string monthname = DateTime.Now.ToString("MMMM");

            if (monthname == "Jan")
            {
                return "01";
            }
            else if (monthname == "February")
            {
                return "02";
            }
            else if (monthname == "March")
            {
                return "03";
            }
            else if (monthname == "April")
            {
                return "04";
            }
            else if (monthname == "May")
            {
                return "05";
            }
            else if (monthname == "June")
            {
                return "06";
            }
            else if (monthname == "July")
            {
                return "07";
            }
            else if (monthname == "August")
            {
                return "08";
            }
            else if (monthname == "September")
            {
                return "09";
            }
            else if (monthname == "October")
            {
                return "10";
            }
            else if (monthname == "November")
            {
                return "11";
            }
            else if (monthname == "December")
            {
                return "12";
            }
            else
            {
                return "0";
            }
        }

        internal static bool check_home_work_status(string homeworkid)
        {
            SqlCommand cmd = new SqlCommand("Select  Homework_id from ReplayHomework where Homework_id='" + homeworkid + "'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal string get_classsid_top1()
        {
            SqlCommand cmd = new SqlCommand("Select  top 1 CategoryID from ClassMaster order by id asc  ");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        public void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();

                ClearInputs(ctrl.Controls);
            }
        }


        public string registration_code()
        {


            string result = "";
            SqlDataAdapter ad = new SqlDataAdapter("select * from Global", HMS.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Globle_Master");
            DataTable dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["reg_no"] = 1;
                result = "1";
                dt.Rows.Add(dr);

            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["reg_no"].ToString() == "")
                    {
                        dr["reg_no"] = 1;
                        result = "1";
                    }
                    else
                    {
                        dr["reg_no"] = Convert.ToDouble(dr["reg_no"]) + 1;
                        result = dr["reg_no"].ToString();
                    }
                    break;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(ds.Tables[0]);
            return "SMS" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd") + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss") + result;

        }




    }






















}
